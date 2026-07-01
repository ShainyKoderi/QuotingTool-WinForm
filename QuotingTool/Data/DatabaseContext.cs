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
        /// Runs SqlSchema.sql on first run to create all tables if they do not exist.
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
        }

        // ── Projects ─────────────────────────────────────────────────────────

        public int CreateProject(Project p)
        {
            using var conn = GetConnection();
            return conn.ExecuteScalar<int>(
                @"INSERT INTO Projects (CustomerName, ProjectName, RevisionDate, LMERate, BilletPremium)
                  OUTPUT INSERTED.Id
                  VALUES (@CustomerName, @ProjectName, @RevisionDate, @LMERate, @BilletPremium)", p);
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
                  UpdatedAt=@UpdatedAt WHERE Id=@Id", p);
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
                      SubsillFlag=@SubsillFlag, SubsillSurcharge=@SubsillSurcharge
                      WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock", line);
            else
                conn.Execute(
                    @"INSERT INTO QuoteLines (ProjectId,PageBlock,RowInBlock,ExtrusionCode,Description,
                      Quantity,LengthM,Price,SellPrice,SubsillFlag,SubsillSurcharge)
                      VALUES (@ProjectId,@PageBlock,@RowInBlock,@ExtrusionCode,@Description,
                      @Quantity,@LengthM,@Price,@SellPrice,@SubsillFlag,@SubsillSurcharge)", line);
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
                      Price_B48=@Price_B48, Price_B50=@Price_B50 WHERE ProjectId=@ProjectId", ep);
            else
                conn.Execute(
                    @"INSERT INTO EditPricing (ProjectId,CostType,FinType,CostTypeOverride,MetalPrice,
                      FinishingPrice,Price_B42,Price_B44,Price_B48,Price_B50)
                      VALUES (@ProjectId,@CostType,@FinType,@CostTypeOverride,@MetalPrice,
                      @FinishingPrice,@Price_B42,@Price_B44,@Price_B48,@Price_B50)", ep);
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
    }
}
