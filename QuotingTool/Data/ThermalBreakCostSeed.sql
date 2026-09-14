-- ============================================================
-- ThermalBreakCostSeed.sql
-- Seeds Table_TB_Cost (ThermalBreakCost) and TB_MPG (ThermalBreakMaterialPriceGroup)
-- reference data extracted from the "Pricing Matrix" sheet of the .xlsm workbook.
--
-- NOTE: This file currently contains NO rows because the actual Table_TB_Cost /
-- TB_MPG values have not yet been extracted from Sample.xlsm. Once available,
-- add rows below following the pattern:
--
--   INSERT INTO #TbCostStaging (Section, MetalCost, ReferenceLengthM, MaterialPriceGroupCode, MetalSellPrice)
--   VALUES ('ETB0001', 157.138, 6.5, 'XX', 253.4484);
--
--   INSERT INTO #TbMpgStaging (GroupCode, RatePerKg)
--   VALUES ('XX', 12.34);
--
-- This script is safe to re-run: it replaces the full contents of both tables
-- with whatever rows are staged below, under a single new BatchId, and pins any
-- projects that don't yet have a batch assigned to that new batch.
-- ============================================================

IF OBJECT_ID('tempdb..#TbCostStaging') IS NOT NULL DROP TABLE #TbCostStaging;
CREATE TABLE #TbCostStaging (
    Section NVARCHAR(50) NOT NULL,
    MetalCost DECIMAL(18,4) NOT NULL,
    ReferenceLengthM DECIMAL(18,4) NOT NULL,
    MaterialPriceGroupCode NVARCHAR(10) NULL,
    MetalSellPrice DECIMAL(18,4) NOT NULL
);

IF OBJECT_ID('tempdb..#TbMpgStaging') IS NOT NULL DROP TABLE #TbMpgStaging;
CREATE TABLE #TbMpgStaging (
    GroupCode NVARCHAR(10) NOT NULL,
    RatePerKg DECIMAL(18,4) NOT NULL
);

-- ?? Staged rows (add real Table_TB_Cost / TB_MPG data here) ?????????????????
-- INSERT INTO #TbCostStaging (Section, MetalCost, ReferenceLengthM, MaterialPriceGroupCode, MetalSellPrice) VALUES
--     ('ETB0001', 157.138, 6.5, 'XX', 253.4484);
--
-- INSERT INTO #TbMpgStaging (GroupCode, RatePerKg) VALUES
--     ('XX', 12.34);

-- Only replace existing data if staging actually has rows, so running this
-- script accidentally with no data does not wipe out previously seeded values.
IF EXISTS (SELECT 1 FROM #TbCostStaging)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'PricingBatchSeq')
        CREATE SEQUENCE PricingBatchSeq AS INT START WITH 1 INCREMENT BY 1;

    DECLARE @tbCostBatchId INT = NEXT VALUE FOR PricingBatchSeq;

    DELETE FROM ThermalBreakCost;
    INSERT INTO ThermalBreakCost (Section, MetalCost, ReferenceLengthM, MaterialPriceGroupCode, MetalSellPrice, BatchId, EffectiveDate, UpdatedAt)
    SELECT Section, MetalCost, ReferenceLengthM, MaterialPriceGroupCode, MetalSellPrice, @tbCostBatchId, GETDATE(), GETDATE()
    FROM #TbCostStaging;

    UPDATE Projects
    SET ThermalBreakCostBatchId = @tbCostBatchId
    WHERE ThermalBreakCostBatchId IS NULL;
END

IF EXISTS (SELECT 1 FROM #TbMpgStaging)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'PricingBatchSeq')
        CREATE SEQUENCE PricingBatchSeq AS INT START WITH 1 INCREMENT BY 1;

    DECLARE @tbMpgBatchId INT = NEXT VALUE FOR PricingBatchSeq;

    DELETE FROM ThermalBreakMaterialPriceGroup;
    INSERT INTO ThermalBreakMaterialPriceGroup (GroupCode, RatePerKg, BatchId, EffectiveDate, UpdatedAt)
    SELECT GroupCode, RatePerKg, @tbMpgBatchId, GETDATE(), GETDATE()
    FROM #TbMpgStaging;

    UPDATE Projects
    SET ThermalBreakMaterialPriceGroupBatchId = @tbMpgBatchId
    WHERE ThermalBreakMaterialPriceGroupBatchId IS NULL;
END

DROP TABLE #TbCostStaging;
DROP TABLE #TbMpgStaging;
