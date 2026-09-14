using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private static void NormalizeProject(Project project, bool isNew = false)
        {
            project.AttentionTo = string.IsNullOrWhiteSpace(project.AttentionTo)
                ? (string.IsNullOrWhiteSpace(project.Attention) ? "To whom it may concern" : project.Attention)
                : project.AttentionTo;
            project.Attention = string.IsNullOrWhiteSpace(project.Attention)
                ? project.AttentionTo
                : project.Attention;
            project.QuoteValidity = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;
            project.AddLocAlLogos = string.IsNullOrWhiteSpace(project.AddLocAlLogos)
                ? (string.IsNullOrWhiteSpace(project.LocalLogo) ? "None" : project.LocalLogo)
                : project.AddLocAlLogos;
            project.LocalLogo = string.IsNullOrWhiteSpace(project.LocalLogo) ? project.AddLocAlLogos : project.LocalLogo;
            project.GenerateMillFinishAlternative = project.GenerateMillFinishAlternative || project.GenerateMfAlt;
            project.GenerateMfAlt = project.GenerateMfAlt || project.GenerateMillFinishAlternative;
        }

        private static void NormalizeQuoteLine(QuoteLine line)
        {
            if (line.LineNumber <= 0)
                line.LineNumber = line.RowInBlock > 0 ? line.RowInBlock : 1;

            if (line.TotalPrice == 0m && line.Quantity != 0m && line.Price != 0m)
                line.TotalPrice = line.Quantity * line.Price;
        }

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
            {
                // Schema script not deployed alongside the executable – still make sure the
                // SundryItems columns exist so older databases don't throw
                // "Invalid column name 'UnitCost' / 'UnitSell'" at runtime.
                using var fallbackConn = GetConnection();
                fallbackConn.Open();
                EnsureSundryItemColumns(fallbackConn);
                return;
            }

            var sql = File.ReadAllText(schemaPath);
            using var conn = GetConnection();
            conn.Open();
            EnsureSundryItemColumns(conn);
            EnsureQuoteLineColumns(conn);
            EnsureProjectColumns(conn);
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

            // Run extrusion (Section) master seed separately, guarded on its own table so
            // it also seeds for existing databases that already have ColourGroups populated.
            var extrusionMasterHasRows = conn.ExecuteScalar<int?>(
                "SELECT TOP 1 1 FROM ExtrusionMaster") != null;
            if (!extrusionMasterHasRows)
            {
                var extrusionSeedPath = Path.Combine(AppContext.BaseDirectory, "Data", "ExtrusionMasterSeed.sql");
                if (!File.Exists(extrusionSeedPath))
                    extrusionSeedPath = Path.Combine(AppContext.BaseDirectory, "ExtrusionMasterSeed.sql");

                if (File.Exists(extrusionSeedPath))
                {
                    var extrusionSeedSql = File.ReadAllText(extrusionSeedPath);
                    foreach (var batch in extrusionSeedSql.Split(new[] { "\nGO\n", "\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var trimmed = batch.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed))
                            conn.Execute(trimmed);
                    }
                }
            }

            // Run the ExtrusionMaster column-update seed when the new columns (ApMm, PpMm, PhCode,
            // SubsillTier, ThermalBreakType) are not yet populated. This covers both fresh installs
            // (rows just inserted above with NULL values) and existing installs that had the old schema.
            var needsColumnSeed = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM ExtrusionMaster WHERE ApMm IS NULL") > 0;
            if (needsColumnSeed)
            {
                var updateSeedPath = Path.Combine(AppContext.BaseDirectory, "Data", "ExtrusionMasterUpdateSeed.sql");
                if (!File.Exists(updateSeedPath))
                    updateSeedPath = Path.Combine(AppContext.BaseDirectory, "ExtrusionMasterUpdateSeed.sql");

                if (File.Exists(updateSeedPath))
                {
                    var updateSeedSql = File.ReadAllText(updateSeedPath);
                    // Execute as a single batch – the file uses a #temp table pattern that must
                    // run in one connection/batch, not split by GO.
                    conn.Execute(updateSeedSql, commandTimeout: 300);
                }
            }

            // Run the DATA_EDGE material cross-reference seed, guarded on its own table so it
            // also seeds for existing databases that already have ColourGroups/ExtrusionMaster populated.
            var dataEdgeHasRows = conn.ExecuteScalar<int?>(
                "SELECT TOP 1 1 FROM DataEdgeMaterials") != null;
            if (!dataEdgeHasRows)
            {
                var dataEdgeSeedPath = Path.Combine(AppContext.BaseDirectory, "Data", "DataEdgeMaterialsSeed.sql");
                if (!File.Exists(dataEdgeSeedPath))
                    dataEdgeSeedPath = Path.Combine(AppContext.BaseDirectory, "DataEdgeMaterialsSeed.sql");

                if (File.Exists(dataEdgeSeedPath))
                {
                    var dataEdgeSeedSql = File.ReadAllText(dataEdgeSeedPath);
                    foreach (var batch in dataEdgeSeedSql.Split(new[] { "\nGO\n", "\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var trimmed = batch.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed))
                            conn.Execute(trimmed, commandTimeout: 120);
                    }
                }
            }
            // Run the Table_TB_Cost / TB_MPG seed (ThermalBreakCost / ThermalBreakMaterialPriceGroup).
            // The script itself is a no-op until real rows are staged in the file, so it is safe
            // to run unconditionally on every startup.
            var tbCostSeedPath = Path.Combine(AppContext.BaseDirectory, "Data", "ThermalBreakCostSeed.sql");
            if (!File.Exists(tbCostSeedPath))
                tbCostSeedPath = Path.Combine(AppContext.BaseDirectory, "ThermalBreakCostSeed.sql");

            if (File.Exists(tbCostSeedPath))
            {
                var tbCostSeedSql = File.ReadAllText(tbCostSeedPath);
                // Uses #temp tables, so must run as a single batch, not split by GO.
                conn.Execute(tbCostSeedSql, commandTimeout: 120);
            }

            EnsureSundryMasterSeed(conn);
            EnsureReferenceDataUpToDate(conn);
        }

        private static void EnsureSundryItemColumns(SqlConnection conn)
        {
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='SundryItems')
BEGIN
    IF COL_LENGTH('SundryItems','UnitCost') IS NULL ALTER TABLE SundryItems ADD UnitCost DECIMAL(18,4) DEFAULT 0;
    IF COL_LENGTH('SundryItems','UnitSell') IS NULL ALTER TABLE SundryItems ADD UnitSell DECIMAL(18,4) DEFAULT 0;
END");
        }

        private static void EnsureQuoteLineColumns(SqlConnection conn)
        {
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='QuoteLines')
BEGIN
    IF COL_LENGTH('QuoteLines','MillPCOverride') IS NULL ALTER TABLE QuoteLines ADD MillPCOverride BIT NOT NULL DEFAULT 0;
END");
        }

        private static void EnsureProjectColumns(SqlConnection conn)
        {
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='Projects')
BEGIN
    IF COL_LENGTH('Projects','DeliveryCharge') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN DeliveryCharge;
    IF COL_LENGTH('Projects','PricingConstantsId') IS NULL
        ALTER TABLE Projects ADD PricingConstantsId INT NULL;
    IF COL_LENGTH('Projects','MetalBreakpointsBatchId') IS NULL
        ALTER TABLE Projects ADD MetalBreakpointsBatchId INT NULL;
    IF COL_LENGTH('Projects','ThermalBreakBreakpointsBatchId') IS NULL
        ALTER TABLE Projects ADD ThermalBreakBreakpointsBatchId INT NULL;
    IF COL_LENGTH('Projects','PowderCostTableBatchId') IS NULL
        ALTER TABLE Projects ADD PowderCostTableBatchId INT NULL;
    IF COL_LENGTH('Projects','FinishBreakpointsBatchId') IS NULL
        ALTER TABLE Projects ADD FinishBreakpointsBatchId INT NULL;
    IF COL_LENGTH('Projects','SubsillTierPricesBatchId') IS NULL
        ALTER TABLE Projects ADD SubsillTierPricesBatchId INT NULL;
    IF COL_LENGTH('Projects','ThermalBreakCostBatchId') IS NULL
        ALTER TABLE Projects ADD ThermalBreakCostBatchId INT NULL;
    IF COL_LENGTH('Projects','ThermalBreakMaterialPriceGroupBatchId') IS NULL
        ALTER TABLE Projects ADD ThermalBreakMaterialPriceGroupBatchId INT NULL;

    -- Superseded by the *BatchId columns above; drop the now-unused legacy date columns.
    IF COL_LENGTH('Projects','MetalBreakpointsDate') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN MetalBreakpointsDate;
    IF COL_LENGTH('Projects','ThermalBreakBreakpointsDate') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN ThermalBreakBreakpointsDate;
    IF COL_LENGTH('Projects','PowderCostTableDate') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN PowderCostTableDate;
    IF COL_LENGTH('Projects','FinishBreakpointsDate') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN FinishBreakpointsDate;
    IF COL_LENGTH('Projects','SubsillTierPricesDate') IS NOT NULL
        ALTER TABLE Projects DROP COLUMN SubsillTierPricesDate;
END");

            // Ensure the shared batch-id sequence and BatchId columns exist on the five
            // Pricing Matrix reference tables, even if SqlSchema.sql was not deployed
            // alongside the executable (fallback/older-install safety net).
            conn.Execute(@"
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'PricingBatchSeq')
    CREATE SEQUENCE PricingBatchSeq AS INT START WITH 1 INCREMENT BY 1;");

            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='MetalBreakpoints') AND COL_LENGTH('MetalBreakpoints','BatchId') IS NULL
    ALTER TABLE MetalBreakpoints ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakBreakpoints') AND COL_LENGTH('ThermalBreakBreakpoints','BatchId') IS NULL
    ALTER TABLE ThermalBreakBreakpoints ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='PowderCostTable') AND COL_LENGTH('PowderCostTable','BatchId') IS NULL
    ALTER TABLE PowderCostTable ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='FinishBreakpoints') AND COL_LENGTH('FinishBreakpoints','BatchId') IS NULL
    ALTER TABLE FinishBreakpoints ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='SubsillTierPrices') AND COL_LENGTH('SubsillTierPrices','BatchId') IS NULL
    ALTER TABLE SubsillTierPrices ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakCost') AND COL_LENGTH('ThermalBreakCost','BatchId') IS NULL
    ALTER TABLE ThermalBreakCost ADD BatchId INT NULL;
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakMaterialPriceGroup') AND COL_LENGTH('ThermalBreakMaterialPriceGroup','BatchId') IS NULL
    ALTER TABLE ThermalBreakMaterialPriceGroup ADD BatchId INT NULL;");

            // Backfill BatchId for any pre-existing rows that predate this column, grouping
            // all rows currently in a table under one freshly-issued BatchId (mirrors the
            // one-time backfill also performed by SqlSchema.sql).
            BackfillBatchId(conn, "MetalBreakpoints");
            BackfillBatchId(conn, "ThermalBreakBreakpoints");
            BackfillBatchId(conn, "PowderCostTable");
            BackfillBatchId(conn, "FinishBreakpoints");
            BackfillBatchId(conn, "SubsillTierPrices");
            BackfillBatchId(conn, "ThermalBreakCost");
            BackfillBatchId(conn, "ThermalBreakMaterialPriceGroup");

            // One-time freeze: any existing project that has never been linked to a
            // Pricing Matrix constants snapshot is pinned to whichever snapshot is
            // currently the latest. This preserves current behaviour for existing quotes
            // while ensuring future changes to PricingConstants do NOT silently affect
            // them - the user must click Recalculate All to re-link to newer constants.
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='Projects') AND EXISTS (SELECT 1 FROM sys.tables WHERE name='PricingConstants')
BEGIN
    UPDATE Projects
    SET PricingConstantsId = (SELECT TOP 1 Id FROM PricingConstants ORDER BY EffectiveDate DESC, Id DESC)
    WHERE PricingConstantsId IS NULL
      AND EXISTS (SELECT 1 FROM PricingConstants);
END");

            // One-time freeze: any existing project not yet pinned to a reference-table batch
            // is pinned to whichever batch is currently the latest for each table.
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='Projects')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='MetalBreakpoints')
        UPDATE Projects SET MetalBreakpointsBatchId = (SELECT TOP 1 BatchId FROM MetalBreakpoints ORDER BY Id DESC)
        WHERE MetalBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM MetalBreakpoints);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakBreakpoints')
        UPDATE Projects SET ThermalBreakBreakpointsBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakBreakpoints ORDER BY Id DESC)
        WHERE ThermalBreakBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakBreakpoints);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='PowderCostTable')
        UPDATE Projects SET PowderCostTableBatchId = (SELECT TOP 1 BatchId FROM PowderCostTable ORDER BY Id DESC)
        WHERE PowderCostTableBatchId IS NULL AND EXISTS (SELECT 1 FROM PowderCostTable);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='FinishBreakpoints')
        UPDATE Projects SET FinishBreakpointsBatchId = (SELECT TOP 1 BatchId FROM FinishBreakpoints ORDER BY Id DESC)
        WHERE FinishBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM FinishBreakpoints);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='SubsillTierPrices')
        UPDATE Projects SET SubsillTierPricesBatchId = (SELECT TOP 1 BatchId FROM SubsillTierPrices ORDER BY Id DESC)
        WHERE SubsillTierPricesBatchId IS NULL AND EXISTS (SELECT 1 FROM SubsillTierPrices);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakCost')
        UPDATE Projects SET ThermalBreakCostBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakCost ORDER BY Id DESC)
        WHERE ThermalBreakCostBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakCost);

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name='ThermalBreakMaterialPriceGroup')
        UPDATE Projects SET ThermalBreakMaterialPriceGroupBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakMaterialPriceGroup ORDER BY Id DESC)
        WHERE ThermalBreakMaterialPriceGroupBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakMaterialPriceGroup);
END");
        }

        /// <summary>
        /// Assigns a single fresh BatchId (from PricingBatchSeq) to every row of the given
        /// table that currently has a NULL BatchId, so pre-existing/seeded rows are grouped
        /// into one coherent batch rather than being left unpinned.
        /// </summary>
        private static void BackfillBatchId(SqlConnection conn, string tableName)
        {
            var hasNullBatch = conn.ExecuteScalar<int?>(
                $"IF EXISTS (SELECT 1 FROM sys.tables WHERE name='{tableName}') SELECT TOP 1 1 FROM {tableName} WHERE BatchId IS NULL");
            if (hasNullBatch == null) return;

            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            conn.Execute($"UPDATE {tableName} SET BatchId=@batchId WHERE BatchId IS NULL", new { batchId });
        }

        private static void EnsureSundryMasterSeed(SqlConnection conn)
        {
            for (int i = 1; i <= 26; i++)
            {
                var sectionCode = $"SUNDRY-{i}";
                conn.Execute(
                    @"IF NOT EXISTS (SELECT 1 FROM SundryItems WHERE SectionCode=@sectionCode)
                      INSERT INTO SundryItems (SectionCode, Description, MassKgPerM, Ap, Pp, UnitCost, UnitSell)
                      VALUES (@sectionCode, @description, 0, 0, 0, 0, 0)",
                    new
                    {
                        sectionCode,
                        description = $"Sundry description - {i}"
                    });
            }
        }

        private static void EnsureReferenceDataUpToDate(SqlConnection conn)
        {
            // Add Region column to Users if it does not yet exist (migration for existing DBs).
            conn.Execute(@"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name='Users')
    IF COL_LENGTH('Users','Region') IS NULL
        ALTER TABLE Users ADD Region NVARCHAR(50) NOT NULL DEFAULT '';");

            // Add RegionAddresses table if it does not yet exist (migration for existing DBs).
            // Ensure FinishType validation items exist (not in original SeedData.sql).
            conn.Execute(@"
IF NOT EXISTS (SELECT 1 FROM ValidationData WHERE Category = 'FinishType')
INSERT INTO ValidationData (Category, Value, SortOrder) VALUES
    ('FinishType', 'Mill Finish', 1),
    ('FinishType', 'Powder',      2),
    ('FinishType', 'Anodised',    3);");

            // Ensure PCApplicator entries that were missing from SeedData.sql exist.
            conn.Execute(@"
IF NOT EXISTS (SELECT 1 FROM ValidationData WHERE Category = 'PCApplicator' AND Value = 'Capral/AAF QLD anodising')
    INSERT INTO ValidationData (Category, Value, SortOrder) VALUES ('PCApplicator', 'Capral/AAF QLD anodising', 11);
IF NOT EXISTS (SELECT 1 FROM ValidationData WHERE Category = 'PCApplicator' AND Value = 'WA Anodisers')
    INSERT INTO ValidationData (Category, Value, SortOrder) VALUES ('PCApplicator', 'WA Anodisers', 12);");

            conn.Execute(@"
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='RegionAddresses')
CREATE TABLE RegionAddresses (
    Id INT IDENTITY PRIMARY KEY,
    RegionCode NVARCHAR(20) NOT NULL UNIQUE,
    OfficeName NVARCHAR(200) NOT NULL DEFAULT '',
    AddressLine1 NVARCHAR(200) NOT NULL DEFAULT '',
    AddressLine2 NVARCHAR(200) NOT NULL DEFAULT '',
    Suburb NVARCHAR(100) NOT NULL DEFAULT '',
    State NVARCHAR(50) NOT NULL DEFAULT '',
    Postcode NVARCHAR(10) NOT NULL DEFAULT '',
    Phone NVARCHAR(50) NOT NULL DEFAULT '1300 366 517',
    Fax NVARCHAR(50) NOT NULL DEFAULT '1300 133 975',
    Website NVARCHAR(100) NOT NULL DEFAULT 'www.capral.com.au'
);");

            // Seed default region address details for Australian states (idempotent MERGE).
            conn.Execute(@"
MERGE RegionAddresses AS target
USING (VALUES
    ('NQ',  'Capral Limited – North Queensland',  '10 Telford Street',  '',               'Garbutt',            'QLD', '4814', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('NSW', 'Capral Limited – New South Wales',   '10 Williamson Road', '',               'Ingleburn',          'NSW', '2565', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('NT',  'Capral Limited – Northern Territory','5 Totem Road',       '',               'Coconut Grove',      'NT',  '0810', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('SA',  'Capral Limited – South Australia',   '38 Cormack Road',    '',               'Wingfield',          'SA',  '5013', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('SEQ', 'Capral Limited – South East Queensland','110 Cobalt Street','',              'Carole Park',        'QLD', '4300', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('TAS', 'Capral Limited – Tasmania',          '69 Gormanston Road', '',               'Moonah',             'TAS', '7009', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('VIC', 'Capral Limited – Victoria',          '70–80 South Park Drive','',            'Campbellfield',      'VIC', '3061', '1300 366 517', '1300 133 975', 'www.capral.com.au'),
    ('WA',  'Capral Limited – Western Australia', '4 Sudlow Road',      '',               'Bibra Lake',         'WA',  '6163', '1300 366 517', '1300 133 975', 'www.capral.com.au')
) AS source (RegionCode, OfficeName, AddressLine1, AddressLine2, Suburb, State, Postcode, Phone, Fax, Website)
ON target.RegionCode = source.RegionCode
WHEN NOT MATCHED THEN
    INSERT (RegionCode, OfficeName, AddressLine1, AddressLine2, Suburb, State, Postcode, Phone, Fax, Website)
    VALUES (source.RegionCode, source.OfficeName, source.AddressLine1, source.AddressLine2, source.Suburb, source.State, source.Postcode, source.Phone, source.Fax, source.Website);");

            conn.Execute(@"
DECLARE @mergeBatchId INT = NEXT VALUE FOR PricingBatchSeq;
MERGE MetalBreakpoints AS target
USING (VALUES
    (CAST(1 AS DECIMAL(18,2)),     CAST(3.50 AS DECIMAL(18,4)), CAST(12.23 AS DECIMAL(18,4)), CAST(11.88 AS DECIMAL(18,4)), CAST(12.73 AS DECIMAL(18,4)), CAST(0.35 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4))),
    (CAST(2000 AS DECIMAL(18,2)),  CAST(1.42 AS DECIMAL(18,4)), CAST(10.15 AS DECIMAL(18,4)), CAST(9.80 AS DECIMAL(18,4)),  CAST(10.65 AS DECIMAL(18,4)), CAST(0.35 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4))),
    (CAST(3000 AS DECIMAL(18,2)),  CAST(1.37 AS DECIMAL(18,4)), CAST(10.10 AS DECIMAL(18,4)), CAST(9.75 AS DECIMAL(18,4)),  CAST(10.60 AS DECIMAL(18,4)), CAST(0.35 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4))),
    (CAST(5000 AS DECIMAL(18,2)),  CAST(1.32 AS DECIMAL(18,4)), CAST(10.05 AS DECIMAL(18,4)), CAST(9.70 AS DECIMAL(18,4)),  CAST(10.55 AS DECIMAL(18,4)), CAST(0.35 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4))),
    (CAST(7000 AS DECIMAL(18,2)),  CAST(1.12 AS DECIMAL(18,4)), CAST(9.85 AS DECIMAL(18,4)),  CAST(9.70 AS DECIMAL(18,4)),  CAST(10.35 AS DECIMAL(18,4)), CAST(0.15 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4))),
    (CAST(10000 AS DECIMAL(18,2)), CAST(1.02 AS DECIMAL(18,4)), CAST(9.75 AS DECIMAL(18,4)),  CAST(9.60 AS DECIMAL(18,4)),  CAST(10.25 AS DECIMAL(18,4)), CAST(0.15 AS DECIMAL(18,4)), CAST(0.50 AS DECIMAL(18,4)))
) AS source (BreakpointKg, Spread, TypicalPrice, MinPrice, MaxPrice, MinFactor, MaxFactor)
ON target.BreakpointKg = source.BreakpointKg
WHEN MATCHED AND (
    target.Spread <> source.Spread OR
    target.TypicalPrice <> source.TypicalPrice OR
    target.MinPrice <> source.MinPrice OR
    target.MaxPrice <> source.MaxPrice OR
    target.MinFactor <> source.MinFactor OR
    target.MaxFactor <> source.MaxFactor
)
THEN UPDATE SET
    Spread = source.Spread,
    TypicalPrice = source.TypicalPrice,
    MinPrice = source.MinPrice,
    MaxPrice = source.MaxPrice,
    MinFactor = source.MinFactor,
    MaxFactor = source.MaxFactor
WHEN NOT MATCHED THEN
INSERT (BreakpointKg, Spread, TypicalPrice, MinPrice, MaxPrice, MinFactor, MaxFactor, BatchId, EffectiveDate, UpdatedAt)
VALUES (source.BreakpointKg, source.Spread, source.TypicalPrice, source.MinPrice, source.MaxPrice, source.MinFactor, source.MaxFactor,
        @mergeBatchId, GETDATE(), GETDATE());");
    }

        // ── Projects ─────────────────────────────────────────────────────────

        public int CreateProject(Project p)
        {
            NormalizeProject(p, isNew: true);
            using var conn = GetConnection();
            // New projects always start pinned to the latest Pricing Matrix constants snapshot.
            if (p.PricingConstantsId == null)
            {
                p.PricingConstantsId = conn.QueryFirstOrDefault<int?>(
                    "SELECT TOP 1 Id FROM PricingConstants ORDER BY EffectiveDate DESC, Id DESC");
            }
            // New projects always start pinned to the latest batch of each Pricing Matrix
            // reference table (by BatchId), mirroring the PricingConstants behaviour.
            p.MetalBreakpointsBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM MetalBreakpoints ORDER BY Id DESC");
            p.ThermalBreakBreakpointsBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakBreakpoints ORDER BY Id DESC");
            p.PowderCostTableBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM PowderCostTable ORDER BY Id DESC");
            p.FinishBreakpointsBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM FinishBreakpoints ORDER BY Id DESC");
            p.SubsillTierPricesBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM SubsillTierPrices ORDER BY Id DESC");
            p.ThermalBreakCostBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakCost ORDER BY Id DESC");
            p.ThermalBreakMaterialPriceGroupBatchId ??= conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakMaterialPriceGroup ORDER BY Id DESC");
            return conn.ExecuteScalar<int>(
                @"INSERT INTO Projects (CustomerName, ProjectName, RevisionDate, LMERate, BilletPremium,
                  Region, QuotedBy, AccountManager, Attention, AttentionTo, QuoteValidity,
                  MarketSegment, ProductSegment, ProjectNotes, AddLocAlLogos, LocalLogo,
                  FinishType, FinishApplicator, FinishGroup, FinishName, FinishCustomName,
                  FinishSapCode, ThresholdChoice, GenerateMfAlt, GenerateMillFinishAlternative,
                  CustomerSapDiscount, PricingConstantsId,
                  MetalBreakpointsBatchId, ThermalBreakBreakpointsBatchId, PowderCostTableBatchId,
                  FinishBreakpointsBatchId, SubsillTierPricesBatchId,
                  ThermalBreakCostBatchId, ThermalBreakMaterialPriceGroupBatchId)
                  OUTPUT INSERTED.Id
                  VALUES (@CustomerName, @ProjectName, @RevisionDate, @LMERate, @BilletPremium,
                  @Region, @QuotedBy, @AccountManager, @Attention, @AttentionTo, @QuoteValidity,
                  @MarketSegment, @ProductSegment, @ProjectNotes, @AddLocAlLogos, @LocalLogo,
                  @FinishType, @FinishApplicator, @FinishGroup, @FinishName, @FinishCustomName,
                  @FinishSapCode, @ThresholdChoice, @GenerateMfAlt, @GenerateMillFinishAlternative,
                  @CustomerSapDiscount, @PricingConstantsId,
                  @MetalBreakpointsBatchId, @ThermalBreakBreakpointsBatchId, @PowderCostTableBatchId,
                  @FinishBreakpointsBatchId, @SubsillTierPricesBatchId,
                  @ThermalBreakCostBatchId, @ThermalBreakMaterialPriceGroupBatchId)", p);
        }

        public Project? GetProject(int id)
        {
            using var conn = GetConnection();
            var project = conn.QueryFirstOrDefault<Project>("SELECT * FROM Projects WHERE Id = @id", new { id });
            if (project != null)
                NormalizeProject(project);
            return project;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            using var conn = GetConnection();
            return conn.Query<Project>("SELECT * FROM Projects ORDER BY UpdatedAt DESC");
        }

        public void UpdateProject(Project p)
        {
            NormalizeProject(p);
            p.UpdatedAt = DateTime.Now;
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE Projects SET CustomerName=@CustomerName, ProjectName=@ProjectName,
                  RevisionDate=@RevisionDate, LMERate=@LMERate, BilletPremium=@BilletPremium,
                  Region=@Region, QuotedBy=@QuotedBy, AccountManager=@AccountManager,
                  Attention=@Attention, AttentionTo=@AttentionTo, QuoteValidity=@QuoteValidity,
                  MarketSegment=@MarketSegment, ProductSegment=@ProductSegment,
                  ProjectNotes=@ProjectNotes, AddLocAlLogos=@AddLocAlLogos, LocalLogo=@LocalLogo,
                  FinishType=@FinishType, FinishApplicator=@FinishApplicator, FinishGroup=@FinishGroup,
                  FinishName=@FinishName, FinishCustomName=@FinishCustomName,
                  FinishSapCode=@FinishSapCode, ThresholdChoice=@ThresholdChoice,
                  GenerateMfAlt=@GenerateMfAlt, GenerateMillFinishAlternative=@GenerateMillFinishAlternative,
                  CustomerSapDiscount=@CustomerSapDiscount, PricingConstantsId=@PricingConstantsId,
                  MetalBreakpointsBatchId=@MetalBreakpointsBatchId, ThermalBreakBreakpointsBatchId=@ThermalBreakBreakpointsBatchId,
                  PowderCostTableBatchId=@PowderCostTableBatchId, FinishBreakpointsBatchId=@FinishBreakpointsBatchId,
                  SubsillTierPricesBatchId=@SubsillTierPricesBatchId,
                  ThermalBreakCostBatchId=@ThermalBreakCostBatchId, ThermalBreakMaterialPriceGroupBatchId=@ThermalBreakMaterialPriceGroupBatchId,
                  UpdatedAt=@UpdatedAt WHERE Id=@Id", p);
        }

        /// <summary>
        /// Creates a full duplicate of a project (project details, edit pricing and quote lines)
        /// with the finish forced to Mill Finish. Used by the "Generate Mill Finish Alternative"
        /// option so a Mill Finish version of the quote can be produced alongside the original.
        /// </summary>
        public int DuplicateProjectAsMillFinish(int sourceProjectId)
        {
            var source = GetProject(sourceProjectId)
                ?? throw new InvalidOperationException($"Project {sourceProjectId} was not found.");

            var copy = new Project
            {
                CustomerName = source.CustomerName,
                ProjectName = $"{source.ProjectName} (Mill Finish)",
                RevisionDate = source.RevisionDate,
                LMERate = source.LMERate,
                BilletPremium = source.BilletPremium,
                Region = source.Region,
                QuotedBy = source.QuotedBy,
                AccountManager = source.AccountManager,
                Attention = source.Attention,
                AttentionTo = source.AttentionTo,
                QuoteValidity = source.QuoteValidity,
                MarketSegment = source.MarketSegment,
                ProductSegment = source.ProductSegment,
                ProjectNotes = source.ProjectNotes,
                AddLocAlLogos = source.AddLocAlLogos,
                LocalLogo = source.LocalLogo,
                FinishType = "Mill Finish",
                FinishApplicator = string.Empty,
                FinishGroup = string.Empty,
                FinishName = string.Empty,
                FinishCustomName = string.Empty,
                FinishSapCode = "NON",
                ThresholdChoice = source.ThresholdChoice,
                GenerateMillFinishAlternative = false,
                GenerateMfAlt = false,
                CustomerSapDiscount = source.CustomerSapDiscount
            };

            int newId = CreateProject(copy);

            var pricing = GetEditPricing(sourceProjectId);
            if (pricing != null)
            {
                pricing.Id = 0;
                pricing.ProjectId = newId;
                UpsertEditPricing(pricing);
            }
            else
            {
                UpsertEditPricing(new EditPricing { ProjectId = newId });
            }

            foreach (var line in GetQuoteLines(sourceProjectId))
            {
                line.Id = 0;
                line.ProjectId = newId;
                line.SapColourCode = "NON";
                UpsertQuoteLine(line);
            }

            return newId;
        }

        public void DeleteProject(int id)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM MasterFormulas WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM BomLines WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM QuoteLines WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM QuoteSummary WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM QuoteFooters WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM PrintSettings WHERE ProjectId=@id", new { id });
            conn.Execute("DELETE FROM EditPricing WHERE ProjectId=@id", new { id });
            //conn.Execute("DELETE FROM SheetLocks WHERE ProjectId=@id", new { id });
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
            NormalizeQuoteLine(line);
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
                      Alloy=@Alloy, Temper=@Temper, TotalPrice=@TotalPrice, LineNumber=@LineNumber,
                      ManualPriceOverride=@ManualPriceOverride,
                      IpGrade=@IpGrade,
                      MillPCOverride=@MillPCOverride
                      WHERE ProjectId=@ProjectId AND PageBlock=@PageBlock AND RowInBlock=@RowInBlock", line);
            else
                conn.Execute(
                    @"INSERT INTO QuoteLines (ProjectId,PageBlock,RowInBlock,ExtrusionCode,Description,
                      Quantity,LengthM,Price,SellPrice,SubsillFlag,SubsillSurcharge,
                      SapColourCode,IgnoreFinish,Alloy,Temper,TotalPrice,LineNumber,ManualPriceOverride,IpGrade,MillPCOverride)
                      VALUES (@ProjectId,@PageBlock,@RowInBlock,@ExtrusionCode,@Description,
                      @Quantity,@LengthM,@Price,@SellPrice,@SubsillFlag,@SubsillSurcharge,
                      @SapColourCode,@IgnoreFinish,@Alloy,@Temper,@TotalPrice,@LineNumber,@ManualPriceOverride,@IpGrade,@MillPCOverride)", line);
        }

        public void DeleteQuoteLines(int projectId)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM QuoteLines WHERE ProjectId=@projectId", new { projectId });
        }

        public void DeleteQuoteLine(int id)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM QuoteLines WHERE Id=@id", new { id });
        }

        // ── ExtrusionMaster (Section lookup) ────────────────────────────────

        public IEnumerable<ExtrusionMaster> GetExtrusionMasterList()
        {
            using var conn = GetConnection();
            return conn.Query<ExtrusionMaster>("SELECT * FROM ExtrusionMaster ORDER BY Section");
        }

        public ExtrusionMaster? GetExtrusionMasterBySection(string section)
        {
            if (string.IsNullOrWhiteSpace(section)) return null;
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<ExtrusionMaster>(
                "SELECT * FROM ExtrusionMaster WHERE Section=@section", new { section });
        }

        /// <summary>
        /// Returns all IP master data as a dictionary keyed by PH code (case-sensitive),
        /// for use by the Quote form to auto-populate the IP grade per quote line.
        /// </summary>
        public Dictionary<string, int> GetIPMasterByCodes()
        {
            using var conn = GetConnection();
            return conn.Query<IPMasterData>("SELECT PhCode, IpCategory FROM IPMasterData")
                .ToDictionary(r => r.PhCode, r => r.IpCategory, StringComparer.Ordinal);
        }

        /// <summary>
        /// Returns the set of section codes that appear in the THRESHOLDS named range,
        /// for use in the BF column threshold-flag calculation.
        /// </summary>
        public HashSet<string> GetThresholdSections()
        {
            using var conn = GetConnection();
            return new HashSet<string>(
                conn.Query<string>("SELECT Section FROM ThresholdSections"),
                StringComparer.OrdinalIgnoreCase);
        }

        public void UpdateQuoteLinePrice(int projectId, int pageBlock, int rowInBlock, decimal price)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE QuoteLines SET Price=@price WHERE ProjectId=@projectId AND PageBlock=@pageBlock AND RowInBlock=@rowInBlock",
                new { projectId, pageBlock, rowInBlock, price });
        }

        public void UpdateQuoteLineManualPriceOverride(int projectId, int pageBlock, int rowInBlock, decimal? manualPriceOverride)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE QuoteLines SET ManualPriceOverride=@manualPriceOverride WHERE ProjectId=@projectId AND PageBlock=@pageBlock AND RowInBlock=@rowInBlock",
                new { projectId, pageBlock, rowInBlock, manualPriceOverride });
        }

        public void ClearAllManualPriceOverrides(int projectId)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE QuoteLines SET ManualPriceOverride=NULL WHERE ProjectId=@projectId",
                new { projectId });
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
                      Price_B48=@Price_B48, Price_B49=@Price_B49, Price_B50=@Price_B50,
                      Price_B52=@Price_B52, Price_B53=@Price_B53, Price_B54=@Price_B54,
                      Price_B57=@Price_B57, Price_B58=@Price_B58,
                      ThermalBreakCalcType=@ThermalBreakCalcType, ThermalBreakMargin=@ThermalBreakMargin,
                      PricingNotes=@PricingNotes, TotalKg=@TotalKg, TotalFinishingM2=@TotalFinishingM2,
                      TotalPieces=@TotalPieces, IP1UpchargePerKg=@IP1UpchargePerKg,
                      IP2UpchargePerKg=@IP2UpchargePerKg, IP3PanoramicFixedKgRate=@IP3PanoramicFixedKgRate,
                      IP1MarginPct=@IP1MarginPct, IP2MarginPct=@IP2MarginPct, IP3MarginPct=@IP3MarginPct,
                      MillFinishMarginPct=@MillFinishMarginPct, FinishingMarginPct=@FinishingMarginPct,
                      FinishingCostPricePerM2=@FinishingCostPricePerM2,
                      AnodisedThresholdCostPerM2=@AnodisedThresholdCostPerM2,
                      ThermalBreakMarginPct=@ThermalBreakMarginPct
                      WHERE ProjectId=@ProjectId", ep);
            else
                conn.Execute(
                    @"INSERT INTO EditPricing (ProjectId,CostType,FinType,CostTypeOverride,MetalPrice,
                      FinishingPrice,Price_B42,Price_B44,Price_B48,Price_B49,Price_B50,
                      Price_B52,Price_B53,Price_B54,Price_B57,Price_B58,ThermalBreakCalcType,ThermalBreakMargin,
                      PricingNotes,TotalKg,TotalFinishingM2,TotalPieces,IP1UpchargePerKg,IP2UpchargePerKg,
                      IP3PanoramicFixedKgRate,IP1MarginPct,IP2MarginPct,IP3MarginPct,MillFinishMarginPct,
                      FinishingMarginPct,FinishingCostPricePerM2,AnodisedThresholdCostPerM2,
                      ThermalBreakMarginPct)
                      VALUES (@ProjectId,@CostType,@FinType,@CostTypeOverride,@MetalPrice,
                      @FinishingPrice,@Price_B42,@Price_B44,@Price_B48,@Price_B49,@Price_B50,
                      @Price_B52,@Price_B53,@Price_B54,@Price_B57,@Price_B58,@ThermalBreakCalcType,@ThermalBreakMargin,
                      @PricingNotes,@TotalKg,@TotalFinishingM2,@TotalPieces,@IP1UpchargePerKg,@IP2UpchargePerKg,
                      @IP3PanoramicFixedKgRate,@IP1MarginPct,@IP2MarginPct,@IP3MarginPct,@MillFinishMarginPct,
                      @FinishingMarginPct,@FinishingCostPricePerM2,@AnodisedThresholdCostPerM2,
                      @ThermalBreakMarginPct)", ep);
        }

        // ── SundryItems ───────────────────────────────────────────────────────

        public IEnumerable<SundryItem> GetSundryItems()
        {
            using var conn = GetConnection();
            return conn.Query<SundryItem>(
                "SELECT * FROM SundryItems ORDER BY Id");
        }

        public void InsertSundryItem(SundryItem item)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO SundryItems (SectionCode,Description,MassKgPerM,Ap,Pp,UnitCost,UnitSell)
                  VALUES (@SectionCode,@Description,@MassKgPerM,@Ap,@Pp,@UnitCost,@UnitSell)",
                item);
        }

        public void UpdateSundryItem(SundryItem item)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE SundryItems SET SectionCode=@SectionCode, Description=@Description,
                  MassKgPerM=@MassKgPerM, Ap=@Ap, Pp=@Pp, UnitCost=@UnitCost, UnitSell=@UnitSell
                  WHERE Id=@Id",
                item);
        }

        public void DeleteSundryItem(int id)
        {
            using var conn = GetConnection();
            conn.Execute("DELETE FROM SundryItems WHERE Id=@id", new { id });
        }

        public decimal GetSundryTotal(int projectId)
        {
            using var conn = GetConnection();
            return conn.ExecuteScalar<decimal?>(
                @"SELECT SUM(TotalPrice)
                  FROM QuoteLines
                  WHERE ProjectId=@projectId
                    AND ExtrusionCode LIKE 'SUNDRY-%'",
                new { projectId }) ?? 0m;
        }

        // ── Users / Authentication ───────────────────────────────────────────

        public IEnumerable<User> GetUsers()
        {
            using var conn = GetConnection();
            return conn.Query<User>("SELECT * FROM Users ORDER BY Username");
        }

        public User? GetUserByUsername(string username)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Username=@username",
                new { username });
        }

        public User? GetUserById(int id)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id=@id",
                new { id });
        }

        public int InsertUser(User user)
        {
            using var conn = GetConnection();
            return conn.ExecuteScalar<int>(
                @"INSERT INTO Users (Username,DisplayName,PasswordHash,PasswordSalt,Role,Email,Region,IsActive)
                  OUTPUT INSERTED.Id
                  VALUES (@Username,@DisplayName,@PasswordHash,@PasswordSalt,@Role,@Email,@Region,@IsActive)",
                user);
        }

        public void UpdateUser(User user)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE Users SET DisplayName=@DisplayName, Role=@Role, Email=@Email, Region=@Region, IsActive=@IsActive
                  WHERE Id=@Id",
                user);
        }

        public void UpdateUserPassword(int userId, byte[] passwordHash, byte[] passwordSalt)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE Users SET PasswordHash=@passwordHash, PasswordSalt=@passwordSalt
                  WHERE Id=@userId",
                new { userId, passwordHash, passwordSalt });
        }

        // ── RegionAddresses ───────────────────────────────────────────────────

        public IEnumerable<RegionAddress> GetAllRegionAddresses()
        {
            using var conn = GetConnection();
            return conn.Query<RegionAddress>("SELECT * FROM RegionAddresses ORDER BY RegionCode");
        }

        public RegionAddress? GetRegionAddress(string regionCode)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<RegionAddress>(
                "SELECT * FROM RegionAddresses WHERE RegionCode=@regionCode",
                new { regionCode });
        }

        public void UpsertRegionAddress(RegionAddress ra)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM RegionAddresses WHERE RegionCode=@RegionCode", ra);
            if (exists > 0)
                conn.Execute(
                    @"UPDATE RegionAddresses SET OfficeName=@OfficeName, AddressLine1=@AddressLine1,
                      AddressLine2=@AddressLine2, Suburb=@Suburb, State=@State, Postcode=@Postcode,
                      Phone=@Phone, Fax=@Fax, Website=@Website
                      WHERE RegionCode=@RegionCode", ra);
            else
                conn.Execute(
                    @"INSERT INTO RegionAddresses (RegionCode,OfficeName,AddressLine1,AddressLine2,Suburb,State,Postcode,Phone,Fax,Website)
                      VALUES (@RegionCode,@OfficeName,@AddressLine1,@AddressLine2,@Suburb,@State,@Postcode,@Phone,@Fax,@Website)", ra);
        }

        // ── QuoteSummary ──────────────────────────────────────────────────────

        public QuoteSummary? GetQuoteSummary(int projectId)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<QuoteSummary>(
                "SELECT TOP 1 * FROM QuoteSummary WHERE ProjectId=@projectId",
                new { projectId });
        }

        public void UpsertQuoteSummary(QuoteSummary summary)
        {
            using var conn = GetConnection();
            var exists = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM QuoteSummary WHERE ProjectId=@ProjectId",
                summary);
            if (exists > 0)
                conn.Execute(
                    @"UPDATE QuoteSummary SET MillFinishTotal=@MillFinishTotal,
                      MillFinishMarginDollar=@MillFinishMarginDollar,
                      MillFinishMarginPct=@MillFinishMarginPct,
                      ThermalBreakMetalTotal=@ThermalBreakMetalTotal,
                      ThermalBreakMarginDollar=@ThermalBreakMarginDollar,
                      ThermalBreakMarginPct=@ThermalBreakMarginPct,
                      SlottingSundryTotal=@SlottingSundryTotal,
                      SlottingSundryMarginDollar=@SlottingSundryMarginDollar,
                      SlottingSundryMarginPct=@SlottingSundryMarginPct,
                      FinishingTotal=@FinishingTotal,
                      FinishingMarginDollar=@FinishingMarginDollar,
                      FinishingMarginPct=@FinishingMarginPct,
                      GrandTotal=@GrandTotal,
                      GrandMarginDollar=@GrandMarginDollar,
                      GrandMarginPct=@GrandMarginPct,
                      UpdatedAt=@UpdatedAt
                      WHERE ProjectId=@ProjectId",
                    summary);
            else
                conn.Execute(
                    @"INSERT INTO QuoteSummary (ProjectId,MillFinishTotal,MillFinishMarginDollar,
                      MillFinishMarginPct,ThermalBreakMetalTotal,ThermalBreakMarginDollar,
                      ThermalBreakMarginPct,SlottingSundryTotal,SlottingSundryMarginDollar,
                      SlottingSundryMarginPct,FinishingTotal,FinishingMarginDollar,
                      FinishingMarginPct,GrandTotal,GrandMarginDollar,GrandMarginPct,UpdatedAt)
                      VALUES (@ProjectId,@MillFinishTotal,@MillFinishMarginDollar,
                      @MillFinishMarginPct,@ThermalBreakMetalTotal,@ThermalBreakMarginDollar,
                      @ThermalBreakMarginPct,@SlottingSundryTotal,@SlottingSundryMarginDollar,
                      @SlottingSundryMarginPct,@FinishingTotal,@FinishingMarginDollar,
                      @FinishingMarginPct,@GrandTotal,@GrandMarginDollar,@GrandMarginPct,@UpdatedAt)",
                    summary);
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

        /// <summary>Latest BatchId seeded in MetalBreakpoints, or null if empty.</summary>
        public int? GetLatestMetalBreakpointsBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM MetalBreakpoints ORDER BY Id DESC");
        }

        /// <summary>Returns the MetalBreakpoints rows for the given batch, or the latest batch if null.</summary>
        public IEnumerable<MetalBreakpoint> GetMetalBreakpointsByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestMetalBreakpointsBatchId();
            if (batch == null) return Enumerable.Empty<MetalBreakpoint>();
            return conn.Query<MetalBreakpoint>(
                "SELECT * FROM MetalBreakpoints WHERE BatchId=@batch ORDER BY BreakpointKg", new { batch });
        }

        /// <summary>Inserts a new batch of MetalBreakpoints rows sharing a fresh BatchId (from PricingBatchSeq).</summary>
        public int InsertMetalBreakpointsBatch(IEnumerable<MetalBreakpoint> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO MetalBreakpoints (BreakpointKg,Spread,TypicalPrice,MinPrice,MaxPrice,MinFactor,MaxFactor,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@BreakpointKg,@Spread,@TypicalPrice,@MinPrice,@MaxPrice,@MinFactor,@MaxFactor,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        // ── ThermalBreakBreakpoints ──────────────────────────────────────────

        public IEnumerable<ThermalBreakBreakpoint> GetThermalBreakBreakpoints()
        {
            using var conn = GetConnection();
            return conn.Query<ThermalBreakBreakpoint>("SELECT * FROM ThermalBreakBreakpoints ORDER BY BreakpointKg");
        }

        public int? GetLatestThermalBreakBreakpointsBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakBreakpoints ORDER BY Id DESC");
        }

        public IEnumerable<ThermalBreakBreakpoint> GetThermalBreakBreakpointsByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestThermalBreakBreakpointsBatchId();
            if (batch == null) return Enumerable.Empty<ThermalBreakBreakpoint>();
            return conn.Query<ThermalBreakBreakpoint>(
                "SELECT * FROM ThermalBreakBreakpoints WHERE BatchId=@batch ORDER BY BreakpointKg", new { batch });
        }

        public int InsertThermalBreakBreakpointsBatch(IEnumerable<ThermalBreakBreakpoint> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO ThermalBreakBreakpoints (BreakpointKg,MarginTyp,MarginMin,MarginMax,ListTyp,ListMin,ListMax,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@BreakpointKg,@MarginTyp,@MarginMin,@MarginMax,@ListTyp,@ListMin,@ListMax,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        // ── PowderCostTable ──────────────────────────────────────────────────

        public IEnumerable<PowderCostRow> GetPowderCostTable()
        {
            using var conn = GetConnection();
            return conn.Query<PowderCostRow>("SELECT * FROM PowderCostTable ORDER BY Id");
        }

        public int? GetLatestPowderCostTableBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM PowderCostTable ORDER BY Id DESC");
        }

        public IEnumerable<PowderCostRow> GetPowderCostTableByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestPowderCostTableBatchId();
            if (batch == null) return Enumerable.Empty<PowderCostRow>();
            return conn.Query<PowderCostRow>(
                "SELECT * FROM PowderCostTable WHERE BatchId=@batch ORDER BY Id", new { batch });
        }

        public int InsertPowderCostTableBatch(IEnumerable<PowderCostRow> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO PowderCostTable (FinishCategory,Vendor,ApPp,Group1Rate,Group2Rate,Group3Rate,Group4Rate,Group5Rate,
                      Anodised20umRate,Anodised25umRate,GroupPOARate,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@FinishCategory,@Vendor,@ApPp,@Group1Rate,@Group2Rate,@Group3Rate,@Group4Rate,@Group5Rate,
                      @Anodised20umRate,@Anodised25umRate,@GroupPOARate,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        // ── FinishBreakpoints ────────────────────────────────────────────────

        public IEnumerable<FinishBreakpoint> GetFinishBreakpoints()
        {
            using var conn = GetConnection();
            return conn.Query<FinishBreakpoint>("SELECT * FROM FinishBreakpoints ORDER BY BreakpointAP");
        }

        public int? GetLatestFinishBreakpointsBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM FinishBreakpoints ORDER BY Id DESC");
        }

        public IEnumerable<FinishBreakpoint> GetFinishBreakpointsByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestFinishBreakpointsBatchId();
            if (batch == null) return Enumerable.Empty<FinishBreakpoint>();
            return conn.Query<FinishBreakpoint>(
                "SELECT * FROM FinishBreakpoints WHERE BatchId=@batch ORDER BY BreakpointAP", new { batch });
        }

        public int InsertFinishBreakpointsBatch(IEnumerable<FinishBreakpoint> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO FinishBreakpoints (BreakpointAP,BreakpointPP,
                      RangeA_Typical,RangeA_Min,RangeA_Max,RangeB_Typical,RangeB_Min,RangeB_Max,
                      RangeC_Typical,RangeC_Min,RangeC_Max,RangeD_Typical,RangeD_Min,RangeD_Max,
                      BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@BreakpointAP,@BreakpointPP,
                      @RangeA_Typical,@RangeA_Min,@RangeA_Max,@RangeB_Typical,@RangeB_Min,@RangeB_Max,
                      @RangeC_Typical,@RangeC_Min,@RangeC_Max,@RangeD_Typical,@RangeD_Min,@RangeD_Max,
                      @BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
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

        public int? GetLatestSubsillTierPricesBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM SubsillTierPrices ORDER BY Id DESC");
        }

        public IEnumerable<SubsillTierPrice> GetSubsillTierPricesByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestSubsillTierPricesBatchId();
            if (batch == null) return Enumerable.Empty<SubsillTierPrice>();
            return conn.Query<SubsillTierPrice>(
                "SELECT * FROM SubsillTierPrices WHERE BatchId=@batch ORDER BY Tier", new { batch });
        }

        public int InsertSubsillTierPricesBatch(IEnumerable<SubsillTierPrice> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO SubsillTierPrices (Tier,SellPricePerMetre,CostPerMetre,Notes,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@Tier,@SellPricePerMetre,@CostPerMetre,@Notes,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        /// <summary>
        /// Re-links a project to specific batches (BatchId) of all five Pricing Matrix reference
        /// tables (MetalBreakpoints, ThermalBreakBreakpoints, PowderCostTable, FinishBreakpoints,
        /// SubsillTierPrices). Called only when the user explicitly saves new batches / clicks
        /// "Recalculate All" – projects are never auto-updated otherwise.
        /// </summary>
        public void UpdateProjectReferenceBatchIds(int projectId,
            int? metalBreakpointsBatchId, int? thermalBreakBreakpointsBatchId,
            int? powderCostTableBatchId, int? finishBreakpointsBatchId, int? subsillTierPricesBatchId)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"UPDATE Projects SET
                    MetalBreakpointsBatchId=@metalBreakpointsBatchId,
                    ThermalBreakBreakpointsBatchId=@thermalBreakBreakpointsBatchId,
                    PowderCostTableBatchId=@powderCostTableBatchId,
                    FinishBreakpointsBatchId=@finishBreakpointsBatchId,
                    SubsillTierPricesBatchId=@subsillTierPricesBatchId
                  WHERE Id=@projectId",
                new { projectId, metalBreakpointsBatchId, thermalBreakBreakpointsBatchId, powderCostTableBatchId, finishBreakpointsBatchId, subsillTierPricesBatchId });
        }

        // ── PricingConstants ─────────────────────────────────────────────────

        public PricingConstants? GetLatestPricingConstants()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<PricingConstants>(
                "SELECT TOP 1 * FROM PricingConstants ORDER BY EffectiveDate DESC, Id DESC");
        }

        public PricingConstants? GetPricingConstantsById(int id)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<PricingConstants>(
                "SELECT * FROM PricingConstants WHERE Id=@id", new { id });
        }

        /// <summary>
        /// Re-links a project to the latest Pricing Matrix constants snapshot.
        /// Called only when the user explicitly clicks "Recalculate All" on the
        /// Edit Pricing tab – projects are never auto-updated to newer constants.
        /// </summary>
        public void UpdateProjectPricingConstants(int projectId, int pricingConstantsId)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE Projects SET PricingConstantsId=@pricingConstantsId WHERE Id=@projectId",
                new { projectId, pricingConstantsId });
        }

        public void InsertPricingConstants(PricingConstants pc)
        {
            using var conn = GetConnection();
            conn.Execute(
                @"INSERT INTO PricingConstants
                    (EffectiveDate, LmeBilletPremium, InternalSpread, MetalCostPerKg,
                     ExtraSpread, PdbMetalCostBase, AlfCostPerMetre,
                     StandardDeliveryCharge, ApToPpFactor, Notes)
                  VALUES
                    (@EffectiveDate, @LmeBilletPremium, @InternalSpread, @MetalCostPerKg,
                     @ExtraSpread, @PdbMetalCostBase, @AlfCostPerMetre,
                     @StandardDeliveryCharge, @ApToPpFactor, @Notes)", pc);
        }

        // ── ThermalBreakCost (Table_TB_Cost) ─────────────────────────────────

        public IEnumerable<ThermalBreakCost> GetThermalBreakCosts()
        {
            using var conn = GetConnection();
            return conn.Query<ThermalBreakCost>("SELECT * FROM ThermalBreakCost ORDER BY Section");
        }

        /// <summary>
        /// Case-insensitive exact-match lookup for a single section code against
        /// ThermalBreakCost (Table_TB_Cost), matching the VLOOKUP behaviour used
        /// in the 2.Quote!AH PLS/PDB formulas.
        /// </summary>
        public ThermalBreakCost? GetThermalBreakCostBySection(string section)
        {
            if (string.IsNullOrWhiteSpace(section)) return null;
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<ThermalBreakCost>(
                "SELECT TOP 1 * FROM ThermalBreakCost WHERE Section=@section", new { section });
        }

        public int? GetLatestThermalBreakCostBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakCost ORDER BY Id DESC");
        }

        public IEnumerable<ThermalBreakCost> GetThermalBreakCostsByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestThermalBreakCostBatchId();
            if (batch == null) return Enumerable.Empty<ThermalBreakCost>();
            return conn.Query<ThermalBreakCost>(
                "SELECT * FROM ThermalBreakCost WHERE BatchId=@batch ORDER BY Section", new { batch });
        }

        /// <summary>Inserts a new batch of ThermalBreakCost rows sharing a fresh BatchId (from PricingBatchSeq).</summary>
        public int InsertThermalBreakCostBatch(IEnumerable<ThermalBreakCost> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO ThermalBreakCost (Section,MetalCost,ReferenceLengthM,MaterialPriceGroupCode,MetalSellPrice,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@Section,@MetalCost,@ReferenceLengthM,@MaterialPriceGroupCode,@MetalSellPrice,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        /// <summary>
        /// Replaces all ThermalBreakCost rows with the given set, keeping them in a single
        /// batch (used by admin/import tooling that maintains the whole table as one unit
        /// rather than the incremental batch-versioning pattern used elsewhere).
        /// </summary>
        public int ReplaceThermalBreakCosts(IEnumerable<ThermalBreakCost> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            using var tx = conn.BeginTransaction();
            conn.Execute("DELETE FROM ThermalBreakCost", transaction: tx);
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq", transaction: tx);
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO ThermalBreakCost (Section,MetalCost,ReferenceLengthM,MaterialPriceGroupCode,MetalSellPrice,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@Section,@MetalCost,@ReferenceLengthM,@MaterialPriceGroupCode,@MetalSellPrice,@BatchId,@EffectiveDate,@UpdatedAt)", row, transaction: tx);
            }
            tx.Commit();
            return batchId;
        }

        /// <summary>
        /// Re-links a project to a specific batch (BatchId) of the ThermalBreakCost
        /// (Table_TB_Cost) reference table. Called when the user explicitly saves a new
        /// batch via the Monthly Update "Edit Thermal Break Cost" dialog.
        /// </summary>
        public void UpdateProjectThermalBreakCostBatchId(int projectId, int? thermalBreakCostBatchId)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE Projects SET ThermalBreakCostBatchId=@thermalBreakCostBatchId WHERE Id=@projectId",
                new { projectId, thermalBreakCostBatchId });
        }

        /// <summary>
        /// Re-links a project to a specific batch (BatchId) of the
        /// ThermalBreakMaterialPriceGroup (TB_MPG) reference table. Called when the user
        /// explicitly saves a new batch via the Monthly Update "Edit TB Material Price Grp" dialog.
        /// </summary>
        public void UpdateProjectThermalBreakMaterialPriceGroupBatchId(int projectId, int? thermalBreakMaterialPriceGroupBatchId)
        {
            using var conn = GetConnection();
            conn.Execute(
                "UPDATE Projects SET ThermalBreakMaterialPriceGroupBatchId=@thermalBreakMaterialPriceGroupBatchId WHERE Id=@projectId",
                new { projectId, thermalBreakMaterialPriceGroupBatchId });
        }

        // ── ThermalBreakMaterialPriceGroup (TB_MPG) ──────────────────────────

        public IEnumerable<ThermalBreakMaterialPriceGroup> GetThermalBreakMaterialPriceGroups()
        {
            using var conn = GetConnection();
            return conn.Query<ThermalBreakMaterialPriceGroup>(
                "SELECT * FROM ThermalBreakMaterialPriceGroup ORDER BY GroupCode");
        }

        public ThermalBreakMaterialPriceGroup? GetThermalBreakMaterialPriceGroupByCode(string groupCode)
        {
            if (string.IsNullOrWhiteSpace(groupCode)) return null;
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<ThermalBreakMaterialPriceGroup>(
                "SELECT TOP 1 * FROM ThermalBreakMaterialPriceGroup WHERE GroupCode=@groupCode", new { groupCode });
        }

        public int? GetLatestThermalBreakMaterialPriceGroupBatchId()
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<int?>(
                "SELECT TOP 1 BatchId FROM ThermalBreakMaterialPriceGroup ORDER BY Id DESC");
        }

        public IEnumerable<ThermalBreakMaterialPriceGroup> GetThermalBreakMaterialPriceGroupsByBatch(int? batchId)
        {
            using var conn = GetConnection();
            var batch = batchId ?? GetLatestThermalBreakMaterialPriceGroupBatchId();
            if (batch == null) return Enumerable.Empty<ThermalBreakMaterialPriceGroup>();
            return conn.Query<ThermalBreakMaterialPriceGroup>(
                "SELECT * FROM ThermalBreakMaterialPriceGroup WHERE BatchId=@batch ORDER BY GroupCode", new { batch });
        }

        public int InsertThermalBreakMaterialPriceGroupBatch(IEnumerable<ThermalBreakMaterialPriceGroup> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq");
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO ThermalBreakMaterialPriceGroup (GroupCode,RatePerKg,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@GroupCode,@RatePerKg,@BatchId,@EffectiveDate,@UpdatedAt)", row);
            }
            return batchId;
        }

        /// <summary>
        /// Replaces all ThermalBreakMaterialPriceGroup rows with the given set, keeping them
        /// in a single batch.
        /// </summary>
        public int ReplaceThermalBreakMaterialPriceGroups(IEnumerable<ThermalBreakMaterialPriceGroup> rows, DateTime effectiveDate)
        {
            using var conn = GetConnection();
            using var tx = conn.BeginTransaction();
            conn.Execute("DELETE FROM ThermalBreakMaterialPriceGroup", transaction: tx);
            var batchId = conn.ExecuteScalar<int>("SELECT NEXT VALUE FOR PricingBatchSeq", transaction: tx);
            foreach (var row in rows)
            {
                row.BatchId = batchId;
                row.EffectiveDate = effectiveDate;
                row.UpdatedAt = DateTime.Now;
                conn.Execute(
                    @"INSERT INTO ThermalBreakMaterialPriceGroup (GroupCode,RatePerKg,BatchId,EffectiveDate,UpdatedAt)
                      VALUES (@GroupCode,@RatePerKg,@BatchId,@EffectiveDate,@UpdatedAt)", row, transaction: tx);
            }
            tx.Commit();
            return batchId;
        }

        // ── DataEdgeMaterials ────────────────────────────────────────────────

        public IEnumerable<DataEdgeMaterial> GetDataEdgeMaterials()
        {
            using var conn = GetConnection();
            return conn.Query<DataEdgeMaterial>("SELECT * FROM DataEdgeMaterials ORDER BY MatRefNo");
        }

        public DataEdgeMaterial? GetDataEdgeMaterial(string matRefNo)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<DataEdgeMaterial>(
                "SELECT TOP 1 * FROM DataEdgeMaterials WHERE MatRefNo=@matRefNo", new { matRefNo });
        }
    }
}
