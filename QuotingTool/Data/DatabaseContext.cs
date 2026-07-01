using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuotingTool.Data
{
    /// <summary>
    /// Provides SQL Server connection management and all repository methods.
    /// Uses Dapper for clean, readable queries.
    /// </summary>
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);

        /// <summary>
        /// Runs SqlSchema.sql on first run to create all tables if they do not exist,
        /// then runs SeedData.sql to populate reference data if not already seeded.
        /// </summary>
        public void EnsureSchemaExists()
        {
            var schemaPath = Path.Combine(AppContext.BaseDirectory, "Data", "SqlSchema.sql");
            if (!File.Exists(schemaPath))
                schemaPath = Path.Combine(AppContext.BaseDirectory, "SqlSchema.sql");

            if (!File.Exists(schemaPath))
                return;

            var sql = File.ReadAllText(schemaPath);
            using var conn = GetConnection();
            conn.Open();
            // Split by GO statements if present
            foreach (var batch in sql.Split(new[] { "\nGO\n", "\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var trimmed = batch.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                    conn.Execute(trimmed);
            }

            // Run seed data on first launch (only if ColourGroups is empty)
            var colourGroupCount = conn.ExecuteScalar<int>("SELECT COUNT(1) FROM ColourGroups");
            if (colourGroupCount == 0)
            {
                var seedPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData.sql");
                if (!File.Exists(seedPath))
                    seedPath = Path.Combine(AppContext.BaseDirectory, "SeedData.sql");

                if (File.Exists(seedPath))
                {
                    var seedSql = File.ReadAllText(seedPath);
                    foreach (var batch in seedSql.Split(new[] { "\nGO\n", "\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var trimmed = batch.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed))
                            conn.Execute(trimmed);
                    }
                }
            }
        }

        // ── Projects ─────────────────────────────────────────────────────────

        public int CreateProject(Project p)
        {
            using var conn = GetConnection();
            return conn.ExecuteScalar<int>(
                @"INSERT INTO Projects (CustomerName, ProjectName, RevisionDate, LMERate, BilletPremium,
                  Region, QuotedBy, AccountManager, Attention, MarketSegment, ProductSegment,
                  ProjectNotes, LocalLogo, FinishType, FinishApplicator, FinishGroup,
                  FinishName, FinishCustomName, FinishSapCode, ThresholdChoice, GenerateMfAlt)
                  OUTPUT INSERTED.Id
                  VALUES (@CustomerName, @ProjectName, @RevisionDate, @LMERate, @BilletPremium,
                  @Region, @QuotedBy, @AccountManager, @Attention, @MarketSegment, @ProductSegment,
                  @ProjectNotes, @LocalLogo, @FinishType, @FinishApplicator, @FinishGroup,
                  @FinishName, @FinishCustomName, @FinishSapCode, @ThresholdChoice, @GenerateMfAlt)", p);
        }

        public Project? GetProject(int id)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<Project>("SELECT * FROM Projects WHERE Id = @id", new { id });
        }

        public IEnumerable<Project> GetAllProjects()
        {
            using var conn = GetConnection();
            return conn.Query<Project>("SELECT * FROM Projects ORDER BY UpdatedAt DESC");
        }

        public void UpdateProject(Project p)
        {
            p.UpdatedAt = DateTime.Now;
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE Projects SET CustomerName=@CustomerName, ProjectName=@ProjectName,
                  RevisionDate=@RevisionDate, LMERate=@LMERate, BilletPremium=@BilletPremium,
                  Region=@Region, QuotedBy=@QuotedBy, AccountManager=@AccountManager,
                  Attention=@Attention, MarketSegment=@MarketSegment, ProductSegment=@ProductSegment,
                  ProjectNotes=@ProjectNotes, LocalLogo=@LocalLogo, FinishType=@FinishType,
                  FinishApplicator=@FinishApplicator, FinishGroup=@FinishGroup,
                  FinishName=@FinishName, FinishCustomName=@FinishCustomName,
                  FinishSapCode=@FinishSapCode, ThresholdChoice=@ThresholdChoice,
                  GenerateMfAlt=@GenerateMfAlt, UpdatedAt=@UpdatedAt WHERE Id=@Id", p);
        }

        public void DeleteProject(int id)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM MasterFormulas WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM BomLines WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM QuoteLines WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM QuoteFooters WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM PrintSettings WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM EditPricing WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM SheetLocks WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM Projects WHERE Id=@id", new { id });
        }

        // ── QuoteLines ───────────────────────────────────────────────────────

        public IEnumerable<QuoteLine> GetQuoteLines(int projectId)
        {
            using var conn = GetConnection();
            return conn.Query<QuoteLine>(
                "SELECT * FROM QuoteLines WHERE ProjectId=@projectId ORDER BY PageBlock, RowInBlock",
                new { projectId });
        }

        public void UpsertQuoteLine(QuoteLine line)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM QuoteLines WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock",
                line);
            if (exists > 0)
                conn.Execute(
                    @"UPDATE QuoteLines SET ExtrusionCode=@ExtrusionCode, Description=@Description,
                      Quantity=@Quantity, LengthM=@LengthM, Price=@Price, SellPrice=@SellPrice,
                      SubsillFlag=@SubsillFlag, SubsillSurcharge=@SubsillSurcharge,
                      SapColourCode=@SapColourCode, IgnoreFinish=@IgnoreFinish,
                      Alloy=@Alloy, Temper=@Temper
                      WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock", line);
            else
                conn.Execute(
                    @"INSERT INTO QuoteLines (ProjectId,PageBlock,RowInBlock,ExtrusionCode,Description,
                      Quantity,LengthM,Price,SellPrice,SubsillFlag,SubsillSurcharge,
                      SapColourCode,IgnoreFinish,Alloy,Temper)
                      VALUES (@ProjectId,@PageBlock,@RowInBlock,@ExtrusionCode,@Description,
                      @Quantity,@LengthM,@Price,@SellPrice,@SubsillFlag,@SubsillSurcharge,
                      @SapColourCode,@IgnoreFinish,@Alloy,@Temper)", line);
        }

        public void DeleteQuoteLines(int projectId)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM QuoteLines WHERE ProjectId=@projectId", new { projectId });
        }

        public void UpdateQuoteLinePrice(int projectId, int pageBlock, int rowInBlock, decimal price)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE QuoteLines SET Price=@price WHERE ProjectId=@projectId AND PageBlock=@pageBlock AND RowInBlock=@rowInBlock",
                new { projectId, pageBlock, rowInBlock, price });
        }

        public void UpdateQuoteLineLengthM(int projectId, int pageBlock, int rowInBlock, decimal lengthM)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE QuoteLines SET LengthM=@lengthM WHERE ProjectId=@projectId AND PageBlock=@pageBlock AND RowInBlock=@rowInBlock",
                new { projectId, pageBlock, rowInBlock, lengthM });
        }

        // ── QuoteFooters ─────────────────────────────────────────────────────

        public IEnumerable<QuoteFooter> GetQuoteFooters(int projectId)
        {
            using var conn = GetConnection();
            return conn.Query<QuoteFooter>(
                "SELECT * FROM QuoteFooters WHERE ProjectId=@projectId ORDER BY PageNumber",
                new { projectId });
        }

        public void UpsertQuoteFooter(QuoteFooter footer)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM QuoteFooters WHERE ProjectId=@ProjectId AND PageNumber=@PageNumber",
                footer);
            if (exists > 0)
                conn.Execute(
                    "UPDATE QuoteFooters SET FontColor=@FontColor WHERE ProjectId=@ProjectId AND PageNumber=@PageNumber",
                    footer);
            else
                conn.Execute(
                    "INSERT INTO QuoteFooters (ProjectId,PageNumber,FontColor) VALUES (@ProjectId,@PageNumber,@FontColor)",
                    footer);
        }

        // ── PrintSettings ────────────────────────────────────────────────────

        public PrintSetting? GetPrintSetting(int projectId, string sheetName)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<PrintSetting>(
                "SELECT * FROM PrintSettings WHERE ProjectId=@projectId AND SheetName=@sheetName",
                new { projectId, sheetName });
        }

        public void UpsertPrintSetting(PrintSetting ps)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM PrintSettings WHERE ProjectId=@ProjectId AND SheetName=@SheetName",
                ps);
            if (exists > 0)
                conn.Execute(
                    @"UPDATE PrintSettings SET PageCount=@PageCount, LastRow=@LastRow, PrintAreaCols=@PrintAreaCols
                      WHERE ProjectId=@ProjectId AND SheetName=@SheetName", ps);
            else
                conn.Execute(
                    @"INSERT INTO PrintSettings (ProjectId,SheetName,PageCount,LastRow,PrintAreaCols)
                      VALUES (@ProjectId,@SheetName,@PageCount,@LastRow,@PrintAreaCols)", ps);
        }

        // ── EditPricing ──────────────────────────────────────────────────────

        public EditPricing? GetEditPricing(int projectId)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<EditPricing>(
                "SELECT * FROM EditPricing WHERE ProjectId=@projectId", new { projectId });
        }

        public void UpsertEditPricing(EditPricing ep)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM EditPricing WHERE ProjectId=@ProjectId", ep);
            if (exists > 0)
                conn.Execute(
                    @"UPDATE EditPricing SET CostType=@CostType, FinType=@FinType,
                      CostTypeOverride=@CostTypeOverride, MetalPrice=@MetalPrice,
                      FinishingPrice=@FinishingPrice, Price_B42=@Price_B42, Price_B44=@Price_B44,
                      Price_B48=@Price_B48, Price_B50=@Price_B50,
                      Price_B52=@Price_B52, Price_B54=@Price_B54,
                      Price_B57=@Price_B57, Price_B58=@Price_B58,
                      ThermalBreakCalcType=@ThermalBreakCalcType, ThermalBreakMargin=@ThermalBreakMargin
                      WHERE ProjectId=@ProjectId", ep);
            else
                conn.Execute(
                    @"INSERT INTO EditPricing (ProjectId,CostType,FinType,CostTypeOverride,MetalPrice,
                      FinishingPrice,Price_B42,Price_B44,Price_B48,Price_B50,
                      Price_B52,Price_B54,Price_B57,Price_B58,ThermalBreakCalcType,ThermalBreakMargin)
                      VALUES (@ProjectId,@CostType,@FinType,@CostTypeOverride,@MetalPrice,
                      @FinishingPrice,@Price_B42,@Price_B44,@Price_B48,@Price_B50,
                      @Price_B52,@Price_B54,@Price_B57,@Price_B58,@ThermalBreakCalcType,@ThermalBreakMargin)", ep);
        }

        // ── BomLines ─────────────────────────────────────────────────────────

        public IEnumerable<BomLine> GetBomLines(int projectId)
        {
            using var conn = GetConnection();
            return conn.Query<BomLine>(
                "SELECT * FROM BomLines WHERE ProjectId=@projectId ORDER BY RowNumber",
                new { projectId });
        }

        public void InsertBomLine(BomLine line)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO BomLines (ProjectId,RowNumber,ExtrusionCode,Description,Quantity,LengthM,Finish,ImportedPrice,SubsillFlag)
                  VALUES (@ProjectId,@RowNumber,@ExtrusionCode,@Description,@Quantity,@LengthM,@Finish,@ImportedPrice,@SubsillFlag)",
                line);
        }

        public void DeleteBomLines(int projectId)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM BomLines WHERE ProjectId=@projectId", new { projectId });
        }

        // ── RevisionControl ──────────────────────────────────────────────────

        public RevisionControl? GetLatestRevision()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<RevisionControl>(
                "SELECT TOP 1 * FROM RevisionControl ORDER BY UpdatedAt DESC");
        }

        public void InsertRevision(RevisionControl rev)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO RevisionControl (RevisionDate, LMERate, BilletPremium)
                  VALUES (@RevisionDate, @LMERate, @BilletPremium)", rev);
        }

        // ── PricingMatrix ────────────────────────────────────────────────────

        public IEnumerable<PricingMatrix> GetPricingMatrix()
        {
            using var conn = GetConnection();
            return conn.Query<PricingMatrix>("SELECT * FROM PricingMatrix ORDER BY Id");
        }

        public void UpsertPricingMatrix(PricingMatrix pm)
        {
            using var conn = GetConnection();
            if (pm.Id > 0)
                conn.Execute(
                    @"UPDATE PricingMatrix SET ColourGroup=@ColourGroup, FinishType=@FinishType,
                      CostType=@CostType, BaseRate=@BaseRate, UpdatedAt=GETDATE() WHERE Id=@Id", pm);
            else
                conn.Execute(
                    @"INSERT INTO PricingMatrix (ColourGroup,FinishType,CostType,BaseRate)
                      VALUES (@ColourGroup,@FinishType,@CostType,@BaseRate)", pm);
        }

        // ── MasterFormulas ───────────────────────────────────────────────────

        public IEnumerable<MasterFormula> GetMasterFormulas(int projectId)
        {
            using var conn = GetConnection();
            return conn.Query<MasterFormula>(
                "SELECT * FROM MasterFormulas WHERE ProjectId=@projectId ORDER BY PageBlock, RowInBlock",
                new { projectId });
        }

        public void UpsertMasterFormula(MasterFormula mf)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM MasterFormulas WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock",
                mf);
            if (exists > 0)
                conn.Execute(
                    "UPDATE MasterFormulas SET FormulaExpression=@FormulaExpression WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock",
                    mf);
            else
                conn.Execute(
                    "INSERT INTO MasterFormulas (ProjectId,PageBlock,RowInBlock,FormulaExpression) VALUES (@ProjectId,@PageBlock,@RowInBlock,@FormulaExpression)",
                    mf);
        }

        // ── SheetLocks ───────────────────────────────────────────────────────

        public bool IsSheetLocked(int projectId, string sheetName)
        {
            using var conn = GetConnection();
            var result = conn.QueryFirstOrDefault<bool?>(
                "SELECT IsLocked FROM SheetLocks WHERE ProjectId=@projectId AND SheetName=@sheetName",
                new { projectId, sheetName });
            return result ?? true; // default: locked
        }

        public void SetSheetLock(int projectId, string sheetName, bool isLocked)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM SheetLocks WHERE ProjectId=@projectId AND SheetName=@sheetName",
                new { projectId, sheetName });
            if (exists > 0)
                conn.Execute(
                    "UPDATE SheetLocks SET IsLocked=@isLocked, UpdatedAt=GETDATE() WHERE ProjectId=@projectId AND SheetName=@sheetName",
                    new { projectId, sheetName, isLocked });
            else
                conn.Execute(
                    "INSERT INTO SheetLocks (ProjectId,SheetName,IsLocked) VALUES (@projectId,@sheetName,@isLocked)",
                    new { projectId, sheetName, isLocked });
        }

        // ── ColourGroups ─────────────────────────────────────────────────────

        public IEnumerable<ColourGroup> GetColourGroups()
        {
            using var conn = GetConnection();
            return conn.Query<ColourGroup>("SELECT * FROM ColourGroups ORDER BY Id");
        }

        public IEnumerable<ColourGroup> GetColourGroupsByGroup(string groupName)
        {
            using var conn = GetConnection();
            return conn.Query<ColourGroup>(
                "SELECT * FROM ColourGroups WHERE GroupName=@groupName ORDER BY ShortDesc",
                new { groupName });
        }

        public ColourGroup? GetColourGroupByCode(string colourCode)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<ColourGroup>(
                "SELECT TOP 1 * FROM ColourGroups WHERE ColourCode=@colourCode",
                new { colourCode });
        }

        public void InsertColourGroup(ColourGroup cg)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO ColourGroups (GroupName,ShortDesc,LongDesc,ColourCode)
                  VALUES (@GroupName,@ShortDesc,@LongDesc,@ColourCode)", cg);
        }

        public void ClearColourGroups()
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM ColourGroups");
        }

        // ── ValidationData ───────────────────────────────────────────────────

        public IEnumerable<ValidationItem> GetValidationItems(string category)
        {
            using var conn = GetConnection();
            return conn.Query<ValidationItem>(
                "SELECT * FROM ValidationData WHERE Category=@category ORDER BY SortOrder",
                new { category });
        }

        public void InsertValidationItem(ValidationItem vi)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO ValidationData (Category,Value,SortOrder)
                  VALUES (@Category,@Value,@SortOrder)", vi);
        }

        public void ClearValidationData()
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM ValidationData");
        }

        // ── MetalBreakpoints ─────────────────────────────────────────────────

        public IEnumerable<MetalBreakpoint> GetMetalBreakpoints()
        {
            using var conn = GetConnection();
            return conn.Query<MetalBreakpoint>("SELECT * FROM MetalBreakpoints ORDER BY BreakpointKg");
        }

        // ── ThermalBreakBreakpoints ──────────────────────────────────────────

        public IEnumerable<ThermalBreakBreakpoint> GetThermalBreakBreakpoints()
        {
            using var conn = GetConnection();
            return conn.Query<ThermalBreakBreakpoint>("SELECT * FROM ThermalBreakBreakpoints ORDER BY BreakpointKg");
        }

        // ── PowderCostTable ──────────────────────────────────────────────────

        public IEnumerable<PowderCostRow> GetPowderCostTable()
        {
            using var conn = GetConnection();
            return conn.Query<PowderCostRow>("SELECT * FROM PowderCostTable ORDER BY Id");
        }

        // ── FinishBreakpoints ────────────────────────────────────────────────

        public IEnumerable<FinishBreakpoint> GetFinishBreakpoints()
        {
            using var conn = GetConnection();
            return conn.Query<FinishBreakpoint>("SELECT * FROM FinishBreakpoints ORDER BY BreakpointAP");
        }

        // ── SubsillTierPrices ────────────────────────────────────────────────

        public IEnumerable<SubsillTierPrice> GetSubsillTierPrices()
        {
            using var conn = GetConnection();
            return conn.Query<SubsillTierPrice>("SELECT * FROM SubsillTierPrices ORDER BY Tier");
        }

        public SubsillTierPrice? GetSubsillTierPrice(int tier)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<SubsillTierPrice>(
                "SELECT TOP 1 * FROM SubsillTierPrices WHERE Tier=@tier", new { tier });
        }
    }
}
