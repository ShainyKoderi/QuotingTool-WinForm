-- ============================================================
-- QuotingTool Database Schema
-- SQL Server / LocalDB
-- Maps to the Excel workbook structure used in the VBA macros
-- Source: Project Pricing_Quotation Tool.xlsm (password: "party" for protected sheets)
-- VeryHidden sheets in VBA: Colour_Groups, DATA_EDGE
-- ============================================================

-- Projects table (maps to "1.Project Details" sheet)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Projects')
CREATE TABLE Projects (
    Id INT IDENTITY PRIMARY KEY,
    CustomerName NVARCHAR(200),
    ProjectName NVARCHAR(200),
    RevisionDate DATE,
    LMERate DECIMAL(18,4),
    BilletPremium DECIMAL(18,4),
    -- Additional fields from 1.Project Details sheet
    Region NVARCHAR(50),                -- A4 / named range: region
    QuotedBy NVARCHAR(200),             -- A6 / named range: quoted_by
    AccountManager NVARCHAR(200),       -- A7
    Attention NVARCHAR(200),            -- B12 / named range: attention
    MarketSegment NVARCHAR(100),        -- A14 / named range: Market_Segment
    ProductSegment NVARCHAR(100),       -- A15 / named range: product_segment
    ProjectNotes NVARCHAR(MAX),         -- A17
    LocalLogo NVARCHAR(50),             -- B23 / named range: local_logo
    FinishType NVARCHAR(100),           -- B24 / named range: finish_type_value
    FinishApplicator NVARCHAR(200),     -- B26 / named range: finish_applicator_value
    FinishGroup NVARCHAR(100),          -- B28 / named range: finish_group_value
    FinishName NVARCHAR(200),           -- B30 / named range: finish_name_value
    FinishCustomName NVARCHAR(200),     -- B31 / named range: Finish_Custom_Name
    FinishSapCode NVARCHAR(50),         -- E30/E32 / named range: finish_code_value
    ThresholdChoice NVARCHAR(100),      -- E33 / named range: Threshold_Choice (e.g. "20um Clear", "No")
    GenerateMfAlt BIT DEFAULT 0,        -- E24 / named range: MF_ALT
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- QuoteLines table (maps to "2.Quote" sheet, 6 page blocks of 40 rows each)
-- Column headers from row 11: B=Ln, C=Section, D=Description, E=Alloy, F=Temper,
--   G=Qty, H=Length(m), I=$ Each, J=$ Total, K=Slotting/Sundry Charge(ea),
--   L=Ignore Finish, M=SAP Colour Code, V=SS? (SubSill flag)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QuoteLines')
CREATE TABLE QuoteLines (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    PageBlock INT NOT NULL,         -- 1-6
    RowInBlock INT NOT NULL,        -- 1-40
    ExtrusionCode NVARCHAR(100),    -- col C (Section)
    Description NVARCHAR(500),      -- col D (auto-filled via VLOOKUP from Table5)
    Quantity DECIMAL(18,4),         -- col G (Qty)
    LengthM DECIMAL(18,6),          -- col H (Length in metres)
    Price DECIMAL(18,4),            -- col I ($ Each = AH+AI+AS+K)
    SellPrice DECIMAL(18,4),        -- col J ($ Total = I*G)
    SubsillFlag BIT DEFAULT 0,      -- col V (SS?)
    SubsillSurcharge DECIMAL(18,4), -- col K (Slotting/Sundry Charge ea = AZ+BC)
    SapColourCode NVARCHAR(50),     -- col M: =IF(C="","", IF(AND(BF>0,NOT(threshold="No")), VLOOKUP(threshold,Threshold_Code,2), IF(L="No",SAP_Colour_Code,"NON")))
    IgnoreFinish NVARCHAR(10),      -- col L ("Yes"/"No") – when "Yes" shows "**MILL FINISH**" in description
    Alloy NVARCHAR(50),             -- col E (auto from N col via VLOOKUP)
    Temper NVARCHAR(50)             -- col F (auto from O col via VLOOKUP)
);

-- ExtrusionMaster table (maps to "DATA" sheet / "Table5" Excel table)
-- Used to populate the Section dropdown on 2.Quote, and to auto-fill
-- Description/Alloy/Temper when a Section is selected.
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ExtrusionMaster')
CREATE TABLE ExtrusionMaster (
    Section NVARCHAR(50) NOT NULL PRIMARY KEY, -- col A (Table5[Section])
    Description NVARCHAR(500),                 -- col B (Table5[Basic material])
    MassPerM DECIMAL(18,4) DEFAULT 0,           -- col D (Table5[Mass / M], kg/m)
    DefaultAlloy NVARCHAR(50) DEFAULT '6060',   -- standard alloy (Validation!H16)
    DefaultTemper NVARCHAR(50) DEFAULT 'T5'     -- standard temper (Validation!H27)
);

-- QuoteFooters table (maps to named ranges pg1_foot .. pg6_foot)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QuoteFooters')
CREATE TABLE QuoteFooters (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    PageNumber INT NOT NULL,        -- 1-6
    FontColor NVARCHAR(10) DEFAULT '#FFFFFF'
);

-- PrintSettings table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PrintSettings')
CREATE TABLE PrintSettings (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    SheetName NVARCHAR(100),
    PageCount INT DEFAULT 1,
    LastRow INT,
    PrintAreaCols NVARCHAR(20)
);

-- EditPricing table (maps to "3.Edit Pricing" sheet)
-- Key cells: C16=FinType, C25=CostType (sum), D25=Override,
--   B33=MetalSellPrice, B42=POA cost, B43=FinishingCostPrice(auto), B44=FinishingSellPrice,
--   B48=MillPOACost, B49=BulkCostPrice(auto), B50=BulkSellPrice,
--   B52=NonBulkPOACost, B53=NonBulkCostPrice(auto), B54=NonBulkSellPrice, B57=ThresholdCostPrice, B58=ThresholdSellPrice,
--   B36=TBCalcType, B37=TBMargin
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'EditPricing')
CREATE TABLE EditPricing (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    CostType INT DEFAULT 0,             -- C25: SUM(C22:C24) – 0=Standard,1=POA,2=Custom,4=Bremer Mill Std,5=Bremer Mill POA
    FinType INT DEFAULT 0,              -- C16: =IF(B16="Powder",1,0)
    CostTypeOverride BIT DEFAULT 0,     -- D25: manual override flag
    MetalPrice DECIMAL(18,4),           -- B33: Mill Finish Sell Price $/kg (user-entered)
    FinishingPrice DECIMAL(18,4),       -- B43: Finishing Cost Price $/m2 (auto from Pricing Matrix E44)
    Price_B42 DECIMAL(18,4) DEFAULT 99, -- B42: POA/Other Vendor Cost $/m2
    Price_B44 DECIMAL(18,4) DEFAULT 99, -- B44: Finishing Sell Price $/m2 (user-entered)
    Price_B48 DECIMAL(18,4) DEFAULT 99, -- B48: Mill POA Cost $/m2
    Price_B49 DECIMAL(18,4) DEFAULT 0,  -- B49: Finishing BULK Cost Price $/m2 (auto from PricingMatrix)
    Price_B50 DECIMAL(18,4) DEFAULT 99, -- B50: Mill BULK Sell Price $/m2 (user-entered)
    -- Additional fields revealed by .xlsm
    Price_B52 DECIMAL(18,4) DEFAULT 99, -- B52: Mill NON-Bulk POA Cost $/m2
    Price_B53 DECIMAL(18,4) DEFAULT 0,  -- B53: Finishing NON-Bulk Cost Price $/m2 (auto from PricingMatrix)
    Price_B54 DECIMAL(18,4) DEFAULT 99, -- B54: Mill NON-Bulk Sell Price $/m2 (user-entered)
    Price_B57 DECIMAL(18,4) DEFAULT 0,  -- B57: Anodised Threshold Cost Price $/m2 (auto from PricingMatrix E49)
    Price_B58 DECIMAL(18,4) DEFAULT 99, -- B58: Anodised Threshold Sell Price $/m2 (user-entered)
    ThermalBreakCalcType NVARCHAR(50) DEFAULT 'Margin', -- B36: "Margin" or "List less Discount"
    ThermalBreakMargin DECIMAL(18,4) DEFAULT 0.5        -- B37: TB margin/discount value
);

-- BomLines table (maps to "BOM_Import" sheet)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'BomLines')
CREATE TABLE BomLines (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    RowNumber INT,
    ExtrusionCode NVARCHAR(100),    -- col B
    Description NVARCHAR(200),      -- col C
    Quantity DECIMAL(18,4),         -- col D
    LengthM DECIMAL(18,6),          -- col E (metres, after mm->m conversion)
    Finish NVARCHAR(200),           -- col G
    ImportedPrice DECIMAL(18,4),    -- col I
    SubsillFlag BIT DEFAULT 0       -- col V
);

-- RevisionControl table (maps to "Revision Control" sheet)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'RevisionControl')
CREATE TABLE RevisionControl (
    Id INT IDENTITY PRIMARY KEY,
    RevisionDate DATE,
    LMERate DECIMAL(18,4),
    BilletPremium DECIMAL(18,4),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- PricingMatrix table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PricingMatrix')
CREATE TABLE PricingMatrix (
    Id INT IDENTITY PRIMARY KEY,
    ColourGroup NVARCHAR(100),
    FinishType NVARCHAR(100),
    CostType INT,
    BaseRate DECIMAL(18,4),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- MasterFormulas table (maps to col BA "master formulas" backup)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'MasterFormulas')
CREATE TABLE MasterFormulas (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    PageBlock INT,
    RowInBlock INT,
    FormulaExpression NVARCHAR(500)
);

-- SheetLocks table (used by ProtectionHelper)
--IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SheetLocks')
--CREATE TABLE SheetLocks (
--    Id INT IDENTITY PRIMARY KEY,
--    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
--    SheetName NVARCHAR(100) NOT NULL,
--    IsLocked BIT DEFAULT 1,
--    UpdatedAt DATETIME DEFAULT GETDATE()
--);

-- ============================================================
-- Reference / Lookup tables populated from .xlsm data
-- ============================================================

-- ColourGroups table (maps to "Colour_Groups" sheet – VeryHidden in VBA)
-- Columns: A=GROUP, B=SHORT_DESC, C=LONG DESC, D=CODE
-- Named ranges: COLOURS (A4:D94), colour_code (C4:D94), GRP1_Long..GrpPOA_Long
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ColourGroups')
CREATE TABLE ColourGroups (
    Id INT IDENTITY PRIMARY KEY,
    GroupName NVARCHAR(50),         -- col A e.g. "GROUP 1", "GROUP 2", "20um Clear", "25um Clear", "GROUP POA", "n/a"
    ShortDesc NVARCHAR(200),        -- col B
    LongDesc NVARCHAR(500),         -- col C (used in LONG_DESC named ranges)
    ColourCode NVARCHAR(20)         -- col D (SAP colour code, e.g. "GR", "AM", "NON")
);

-- ValidationData table (maps to "Validation" sheet – stores all dropdown/lookup lists)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ValidationData')
CREATE TABLE ValidationData (
    Id INT IDENTITY PRIMARY KEY,
    Category NVARCHAR(100) NOT NULL,  -- e.g. "MarketSegment", "Region", "FinishType", "PCApplicator"
    Value NVARCHAR(500) NOT NULL,
    SortOrder INT DEFAULT 0
);

-- MetalBreakpoints table (maps to "Pricing Matrix" rows 7-13, cols A-G)
-- Used for VLOOKUP in 3.Edit Pricing B33 (Table_Metal_BP)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'MetalBreakpoints')
CREATE TABLE MetalBreakpoints (
    Id INT IDENTITY PRIMARY KEY,
    BreakpointKg DECIMAL(18,2),     -- col A: kg threshold
    Spread DECIMAL(18,4),           -- col B: spread $/kg
    TypicalPrice DECIMAL(18,4),     -- col C: typical sell price $/kg
    MinPrice DECIMAL(18,4),         -- col D: min sell price $/kg
    MaxPrice DECIMAL(18,4),         -- col E: max sell price $/kg
    MinFactor DECIMAL(18,4),        -- col F
    MaxFactor DECIMAL(18,4)         -- col G
);

-- ThermalBreakBreakpoints table (maps to "Pricing Matrix" rows 15-21, cols A-G)
-- Used for VLOOKUP in 3.Edit Pricing B37 (Table_TB_BP)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ThermalBreakBreakpoints')
CREATE TABLE ThermalBreakBreakpoints (
    Id INT IDENTITY PRIMARY KEY,
    BreakpointKg DECIMAL(18,2),     -- col A: kg threshold
    MarginTyp DECIMAL(18,4),        -- col B: typical margin
    MarginMin DECIMAL(18,4),        -- col C: min margin
    MarginMax DECIMAL(18,4),        -- col D: max margin
    ListTyp DECIMAL(18,4),          -- col E: typical list price $/m
    ListMin DECIMAL(18,4),          -- col F: min list price $/m
    ListMax DECIMAL(18,4)           -- col G: max list price $/m
);

-- PowderCostTable (maps to "Pricing Matrix" rows 27-40)
-- Cost rates per m2 by vendor, AP/PP type, and colour group
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PowderCostTable')
CREATE TABLE PowderCostTable (
    Id INT IDENTITY PRIMARY KEY,
    FinishCategory NVARCHAR(50),    -- col A: "Powder" or "Anodised" or "Powder/Anodised"
    Vendor NVARCHAR(200),           -- col B: vendor name (e.g. "Capral P&P - Bremer (AP)")
    ApPp NVARCHAR(5),               -- col C: "AP" or "PP"
    Group1Rate DECIMAL(18,4),       -- col D: Group 1 cost $/m2
    Group2Rate DECIMAL(18,4),       -- col E: Group 2 cost $/m2
    Group3Rate DECIMAL(18,4),       -- col F: Group 3 cost $/m2
    Group4Rate DECIMAL(18,4),       -- col G: Group 4 cost $/m2
    Group5Rate DECIMAL(18,4),       -- col H: Group 5 cost $/m2
    Anodised20umRate DECIMAL(18,4), -- col I: 20um Clear rate $/m2
    Anodised25umRate DECIMAL(18,4), -- col J: 25um Clear rate $/m2
    GroupPOARate DECIMAL(18,4),     -- col K: Group POA (99 = POA)
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- FinishBreakpoints table (maps to "Pricing Matrix" rows 52-60)
-- Margin/price breakpoints by m2 quantity for AP/PP powder coating
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'FinishBreakpoints')
CREATE TABLE FinishBreakpoints (
    Id INT IDENTITY PRIMARY KEY,
    BreakpointAP DECIMAL(18,2),     -- col A: AP breakpoint (m2)
    BreakpointPP DECIMAL(18,2),     -- col B: PP breakpoint (m2)
    -- Breakpoint range 'a' columns (C-E)
    RangeA_Typical DECIMAL(18,4),
    RangeA_Min DECIMAL(18,4),
    RangeA_Max DECIMAL(18,4),
    -- Breakpoint range 'b' columns (F-H)
    RangeB_Typical DECIMAL(18,4),
    RangeB_Min DECIMAL(18,4),
    RangeB_Max DECIMAL(18,4),
    -- Breakpoint range 'c' columns (I-K)
    RangeC_Typical DECIMAL(18,4),
    RangeC_Min DECIMAL(18,4),
    RangeC_Max DECIMAL(18,4),
    -- Breakpoint range 'd' columns (L-N)
    RangeD_Typical DECIMAL(18,4),
    RangeD_Min DECIMAL(18,4),
    RangeD_Max DECIMAL(18,4)
);

-- SubsillTierPrices table (maps to "Pricing Matrix" rows 63-68)
-- Named range: SubSill_Tier_Price (A64:C68)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SubsillTierPrices')
CREATE TABLE SubsillTierPrices (
    Id INT IDENTITY PRIMARY KEY,
    Tier INT NOT NULL,              -- col A: tier number (1-4)
    SellPricePerMetre DECIMAL(18,4), -- col B: sell price $/m
    CostPerMetre DECIMAL(18,4),     -- col C: estimated cost $/m
    Notes NVARCHAR(500)             -- col D: notes (e.g. Panoramic door rule)
);

-- Add missing columns to Projects
IF COL_LENGTH('Projects','AccountManager') IS NULL ALTER TABLE Projects ADD AccountManager NVARCHAR(200);
IF COL_LENGTH('Projects','AttentionTo') IS NULL ALTER TABLE Projects ADD AttentionTo NVARCHAR(200) DEFAULT 'To whom it may concern';
IF COL_LENGTH('Projects','QuoteValidity') IS NULL ALTER TABLE Projects ADD QuoteValidity NVARCHAR(50) DEFAULT '30 days';
IF COL_LENGTH('Projects','ProjectNotes') IS NULL ALTER TABLE Projects ADD ProjectNotes NVARCHAR(2000);
IF COL_LENGTH('Projects','AddLocAlLogos') IS NULL ALTER TABLE Projects ADD AddLocAlLogos NVARCHAR(50) DEFAULT 'None';
IF COL_LENGTH('Projects','FinishGroup') IS NULL ALTER TABLE Projects ADD FinishGroup NVARCHAR(100);
IF COL_LENGTH('Projects','FinishSapCode') IS NULL ALTER TABLE Projects ADD FinishSapCode NVARCHAR(50);
IF COL_LENGTH('Projects','FinishName') IS NULL ALTER TABLE Projects ADD FinishName NVARCHAR(200);
IF COL_LENGTH('Projects','GenerateMillFinishAlternative') IS NULL ALTER TABLE Projects ADD GenerateMillFinishAlternative BIT DEFAULT 0;
IF COL_LENGTH('Projects','CustomerSapDiscount') IS NULL ALTER TABLE Projects ADD CustomerSapDiscount DECIMAL(18,4) DEFAULT 0;

-- Add missing columns to QuoteLines
IF COL_LENGTH('QuoteLines','TotalPrice') IS NULL ALTER TABLE QuoteLines ADD TotalPrice DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('QuoteLines','LineNumber') IS NULL ALTER TABLE QuoteLines ADD LineNumber INT DEFAULT 0;
IF COL_LENGTH('QuoteLines','IpGrade') IS NULL ALTER TABLE QuoteLines ADD IpGrade INT DEFAULT 0;
IF COL_LENGTH('QuoteLines','ManualPriceOverride') IS NULL ALTER TABLE QuoteLines ADD ManualPriceOverride DECIMAL(18,4) NULL;

-- Add missing columns to EditPricing
IF COL_LENGTH('EditPricing','PricingNotes') IS NULL ALTER TABLE EditPricing ADD PricingNotes NVARCHAR(2000);
IF COL_LENGTH('EditPricing','TotalKg') IS NULL ALTER TABLE EditPricing ADD TotalKg DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','TotalFinishingM2') IS NULL ALTER TABLE EditPricing ADD TotalFinishingM2 DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','TotalPieces') IS NULL ALTER TABLE EditPricing ADD TotalPieces INT DEFAULT 0;
IF COL_LENGTH('EditPricing','IP1UpchargePerKg') IS NULL ALTER TABLE EditPricing ADD IP1UpchargePerKg DECIMAL(18,4) DEFAULT 1.00;
IF COL_LENGTH('EditPricing','IP2UpchargePerKg') IS NULL ALTER TABLE EditPricing ADD IP2UpchargePerKg DECIMAL(18,4) DEFAULT 2.00;
IF COL_LENGTH('EditPricing','IP3PanoramicFixedKgRate') IS NULL ALTER TABLE EditPricing ADD IP3PanoramicFixedKgRate DECIMAL(18,4) DEFAULT 25.85;
IF COL_LENGTH('EditPricing','IP1MarginPct') IS NULL ALTER TABLE EditPricing ADD IP1MarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','IP2MarginPct') IS NULL ALTER TABLE EditPricing ADD IP2MarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','IP3MarginPct') IS NULL ALTER TABLE EditPricing ADD IP3MarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','MillFinishMarginPct') IS NULL ALTER TABLE EditPricing ADD MillFinishMarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','FinishingMarginPct') IS NULL ALTER TABLE EditPricing ADD FinishingMarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','FinishingCostPricePerM2') IS NULL ALTER TABLE EditPricing ADD FinishingCostPricePerM2 DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','AnodisedThresholdCostPerM2') IS NULL ALTER TABLE EditPricing ADD AnodisedThresholdCostPerM2 DECIMAL(18,4) DEFAULT 10.65;
IF COL_LENGTH('EditPricing','ThermalBreakMarginPct') IS NULL ALTER TABLE EditPricing ADD ThermalBreakMarginPct DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','Price_B49') IS NULL ALTER TABLE EditPricing ADD Price_B49 DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('EditPricing','Price_B53') IS NULL ALTER TABLE EditPricing ADD Price_B53 DECIMAL(18,4) DEFAULT 0;

-- SundryItems table (global workbook-level sundry section master)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='SundryItems')
CREATE TABLE SundryItems (
    Id INT IDENTITY PRIMARY KEY,
    SectionCode NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(300),
    MassKgPerM DECIMAL(18,4) DEFAULT 0,
    Ap INT DEFAULT 0,
    Pp INT DEFAULT 0,
    UnitCost DECIMAL(18,4) DEFAULT 0,
    UnitSell DECIMAL(18,4) DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);

IF COL_LENGTH('SundryItems','SectionCode') IS NULL ALTER TABLE SundryItems ADD SectionCode NVARCHAR(50) NULL;
IF COL_LENGTH('SundryItems','MassKgPerM') IS NULL ALTER TABLE SundryItems ADD MassKgPerM DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('SundryItems','Ap') IS NULL ALTER TABLE SundryItems ADD Ap INT DEFAULT 0;
IF COL_LENGTH('SundryItems','Pp') IS NULL ALTER TABLE SundryItems ADD Pp INT DEFAULT 0;
IF COL_LENGTH('SundryItems','UnitCost') IS NULL ALTER TABLE SundryItems ADD UnitCost DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('SundryItems','UnitSell') IS NULL ALTER TABLE SundryItems ADD UnitSell DECIMAL(18,4) DEFAULT 0;

IF COL_LENGTH('SundryItems','SectionCode') IS NOT NULL
BEGIN
    UPDATE SundryItems
    SET SectionCode = CONCAT('SUNDRY-', Id)
    WHERE (SectionCode IS NULL OR LTRIM(RTRIM(SectionCode)) = '');
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_SundryItems_SectionCode'
      AND object_id = OBJECT_ID('SundryItems'))
BEGIN
    CREATE UNIQUE INDEX UX_SundryItems_SectionCode
    ON SundryItems(SectionCode)
    WHERE SectionCode IS NOT NULL;
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
CREATE TABLE Users (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    DisplayName NVARCHAR(200) NOT NULL,
    PasswordHash VARBINARY(256) NOT NULL,
    PasswordSalt VARBINARY(128) NOT NULL,
    Role INT NOT NULL DEFAULT 0,
    Email NVARCHAR(200) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- QuoteSummary table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='QuoteSummary')
CREATE TABLE QuoteSummary (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    MillFinishTotal DECIMAL(18,4) DEFAULT 0,
    MillFinishMarginDollar DECIMAL(18,4) DEFAULT 0,
    MillFinishMarginPct DECIMAL(18,4) DEFAULT 0,
    ThermalBreakMetalTotal DECIMAL(18,4) DEFAULT 0,
    ThermalBreakMarginDollar DECIMAL(18,4) DEFAULT 0,
    ThermalBreakMarginPct DECIMAL(18,4) DEFAULT 0,
    SlottingSundryTotal DECIMAL(18,4) DEFAULT 0,
    SlottingSundryMarginDollar DECIMAL(18,4) DEFAULT 0,
    SlottingSundryMarginPct DECIMAL(18,4) DEFAULT 0,
    FinishingTotal DECIMAL(18,4) DEFAULT 0,
    FinishingMarginDollar DECIMAL(18,4) DEFAULT 0,
    FinishingMarginPct DECIMAL(18,4) DEFAULT 0,
    GrandTotal DECIMAL(18,4) DEFAULT 0,
    GrandMarginDollar DECIMAL(18,4) DEFAULT 0,
    GrandMarginPct DECIMAL(18,4) DEFAULT 0,
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Add new columns to ExtrusionMaster (DATA sheet cols E, F, H, K, M)
IF COL_LENGTH('ExtrusionMaster','ApMm') IS NULL ALTER TABLE ExtrusionMaster ADD ApMm DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('ExtrusionMaster','PpMm') IS NULL ALTER TABLE ExtrusionMaster ADD PpMm DECIMAL(18,4) DEFAULT 0;
IF COL_LENGTH('ExtrusionMaster','SubsillTier') IS NULL ALTER TABLE ExtrusionMaster ADD SubsillTier INT DEFAULT 0;
IF COL_LENGTH('ExtrusionMaster','ThermalBreakType') IS NULL ALTER TABLE ExtrusionMaster ADD ThermalBreakType NVARCHAR(10) DEFAULT 'No';
IF COL_LENGTH('ExtrusionMaster','PhCode') IS NULL ALTER TABLE ExtrusionMaster ADD PhCode NVARCHAR(50) NULL;

-- ThresholdSections table: section codes in the THRESHOLDS named range (Validation col N)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='ThresholdSections')
CREATE TABLE ThresholdSections (
    Section NVARCHAR(50) NOT NULL PRIMARY KEY
);

-- IPMasterData table: PH product hierarchy code → IP ingress protection category (0-3)
-- Source: 5.Edit_PH_IP sheet, cols B (PH) and D (IP Category)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='IPMasterData')
CREATE TABLE IPMasterData (
    PhCode NVARCHAR(50) NOT NULL PRIMARY KEY,
    IpCategory INT NOT NULL DEFAULT 0
);

-- PricingConstants table: single-row-per-effective-date table of the global
-- $/kg and $/m constants from "Pricing Matrix" rows 1-6 that feed the
-- MetalPricePerKg / $ Each calculation chain but were previously only
-- documented (README/FORMULA_NOTES) and never persisted in the database.
-- Source cells (Pricing Matrix):
--   B3=LmeBilletPremium, B4=InternalSpread, B5=MetalCostPerKg (B3+B4)
--   F3=ExtraSpread, F4=PdbMetalCostBase (B5+F3), F5=AlfCostPerMetre
--   H3=StandardDeliveryCharge, K5=ApToPpFactor
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='PricingConstants')
CREATE TABLE PricingConstants (
    Id INT IDENTITY PRIMARY KEY,
    EffectiveDate DATE NOT NULL DEFAULT GETDATE(),
    LmeBilletPremium DECIMAL(18,4) NOT NULL DEFAULT 0,   -- Pricing Matrix B3 ($/kg)
    InternalSpread DECIMAL(18,4) NOT NULL DEFAULT 0,     -- Pricing Matrix B4 ($/kg)
    MetalCostPerKg DECIMAL(18,4) NOT NULL DEFAULT 0,     -- Pricing Matrix B5 = B3+B4 ($/kg)
    ExtraSpread DECIMAL(18,4) NOT NULL DEFAULT 0,        -- Pricing Matrix F3 ($/kg)
    PdbMetalCostBase DECIMAL(18,4) NOT NULL DEFAULT 0,   -- Pricing Matrix F4 = B5+F3 ($/kg)
    AlfCostPerMetre DECIMAL(18,4) NOT NULL DEFAULT 0,    -- Pricing Matrix F5 ($/m)
    StandardDeliveryCharge DECIMAL(18,4) NOT NULL DEFAULT 97, -- Pricing Matrix H3 ($)
    ApToPpFactor DECIMAL(18,4) NOT NULL DEFAULT 1.9,     -- Pricing Matrix K5
    Notes NVARCHAR(500),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- DataEdgeMaterials table (maps to "DATA_EDGE" sheet – VeryHidden in VBA)
-- Cross-reference of internal EDGE material reference codes to SAP material
-- numbers, used for BOM/SAP export mapping of EDGE-system products.
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='DataEdgeMaterials')
CREATE TABLE DataEdgeMaterials (
    Id INT IDENTITY PRIMARY KEY,
    MatRefNo NVARCHAR(50) NOT NULL,      -- col A: Mat.Ref.No
    MatRefDescription NVARCHAR(300),     -- col B: Mat.Ref.Description
    SapMatNo NVARCHAR(50)                -- col C: SAP.Mat.No
);

-- ============================================================
-- Batch linking for Pricing Matrix reference tables (MetalBreakpoints,
-- ThermalBreakBreakpoints, PowderCostTable, FinishBreakpoints,
-- SubsillTierPrices). Each "Save" from the corresponding maintenance form
-- inserts a brand new batch of rows sharing one BatchId (from a shared
-- SQL Server SEQUENCE, guaranteeing uniqueness across all five tables).
-- EffectiveDate/UpdatedAt remain purely for display/audit purposes.
-- Projects pin to a specific BatchId per table so that existing projects
-- keep the batch values they were created/last-saved with, while new
-- projects default to the latest batch.
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'PricingBatchSeq')
    CREATE SEQUENCE PricingBatchSeq AS INT START WITH 1 INCREMENT BY 1;

IF COL_LENGTH('MetalBreakpoints','BatchId') IS NULL ALTER TABLE MetalBreakpoints ADD BatchId INT NULL;
IF COL_LENGTH('MetalBreakpoints','EffectiveDate') IS NULL ALTER TABLE MetalBreakpoints ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('MetalBreakpoints','UpdatedAt') IS NULL ALTER TABLE MetalBreakpoints ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

IF COL_LENGTH('ThermalBreakBreakpoints','BatchId') IS NULL ALTER TABLE ThermalBreakBreakpoints ADD BatchId INT NULL;
IF COL_LENGTH('ThermalBreakBreakpoints','EffectiveDate') IS NULL ALTER TABLE ThermalBreakBreakpoints ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('ThermalBreakBreakpoints','UpdatedAt') IS NULL ALTER TABLE ThermalBreakBreakpoints ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

IF COL_LENGTH('PowderCostTable','BatchId') IS NULL ALTER TABLE PowderCostTable ADD BatchId INT NULL;
IF COL_LENGTH('PowderCostTable','EffectiveDate') IS NULL ALTER TABLE PowderCostTable ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();

IF COL_LENGTH('FinishBreakpoints','BatchId') IS NULL ALTER TABLE FinishBreakpoints ADD BatchId INT NULL;
IF COL_LENGTH('FinishBreakpoints','EffectiveDate') IS NULL ALTER TABLE FinishBreakpoints ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('FinishBreakpoints','UpdatedAt') IS NULL ALTER TABLE FinishBreakpoints ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

IF COL_LENGTH('SubsillTierPrices','BatchId') IS NULL ALTER TABLE SubsillTierPrices ADD BatchId INT NULL;
IF COL_LENGTH('SubsillTierPrices','EffectiveDate') IS NULL ALTER TABLE SubsillTierPrices ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('SubsillTierPrices','UpdatedAt') IS NULL ALTER TABLE SubsillTierPrices ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

-- Backfill BatchId for any pre-existing rows (e.g. seeded data) that predate
-- this column, grouping by EffectiveDate so all rows from the same original
-- seed/save share one BatchId.
IF EXISTS (SELECT 1 FROM MetalBreakpoints WHERE BatchId IS NULL)
BEGIN
    DECLARE @mbBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE MetalBreakpoints SET BatchId = @mbBatch WHERE BatchId IS NULL;
END
IF EXISTS (SELECT 1 FROM ThermalBreakBreakpoints WHERE BatchId IS NULL)
BEGIN
    DECLARE @tbBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE ThermalBreakBreakpoints SET BatchId = @tbBatch WHERE BatchId IS NULL;
END
IF EXISTS (SELECT 1 FROM PowderCostTable WHERE BatchId IS NULL)
BEGIN
    DECLARE @pcBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE PowderCostTable SET BatchId = @pcBatch WHERE BatchId IS NULL;
END
IF EXISTS (SELECT 1 FROM FinishBreakpoints WHERE BatchId IS NULL)
BEGIN
    DECLARE @fbBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE FinishBreakpoints SET BatchId = @fbBatch WHERE BatchId IS NULL;
END
IF EXISTS (SELECT 1 FROM SubsillTierPrices WHERE BatchId IS NULL)
BEGIN
    DECLARE @stBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE SubsillTierPrices SET BatchId = @stBatch WHERE BatchId IS NULL;
END

-- Projects pin to a BatchId of each reference table.
IF COL_LENGTH('Projects','MetalBreakpointsBatchId') IS NULL ALTER TABLE Projects ADD MetalBreakpointsBatchId INT NULL;
IF COL_LENGTH('Projects','ThermalBreakBreakpointsBatchId') IS NULL ALTER TABLE Projects ADD ThermalBreakBreakpointsBatchId INT NULL;
IF COL_LENGTH('Projects','PowderCostTableBatchId') IS NULL ALTER TABLE Projects ADD PowderCostTableBatchId INT NULL;
IF COL_LENGTH('Projects','FinishBreakpointsBatchId') IS NULL ALTER TABLE Projects ADD FinishBreakpointsBatchId INT NULL;
IF COL_LENGTH('Projects','SubsillTierPricesBatchId') IS NULL ALTER TABLE Projects ADD SubsillTierPricesBatchId INT NULL;

-- Superseded by the *BatchId columns above (surrogate-key link is more robust than
-- matching on EffectiveDate). Drop the now-unused legacy date columns from Projects.
IF COL_LENGTH('Projects','MetalBreakpointsDate') IS NOT NULL ALTER TABLE Projects DROP COLUMN MetalBreakpointsDate;
IF COL_LENGTH('Projects','ThermalBreakBreakpointsDate') IS NOT NULL ALTER TABLE Projects DROP COLUMN ThermalBreakBreakpointsDate;
IF COL_LENGTH('Projects','PowderCostTableDate') IS NOT NULL ALTER TABLE Projects DROP COLUMN PowderCostTableDate;
IF COL_LENGTH('Projects','FinishBreakpointsDate') IS NOT NULL ALTER TABLE Projects DROP COLUMN FinishBreakpointsDate;
IF COL_LENGTH('Projects','SubsillTierPricesDate') IS NOT NULL ALTER TABLE Projects DROP COLUMN SubsillTierPricesDate;

-- One-time freeze: any existing project not yet pinned to a reference-table
-- batch is pinned to whichever batch is currently the latest for each table.
-- This preserves current behaviour for existing quotes while ensuring future
-- batch saves do NOT silently affect them.
UPDATE Projects
SET MetalBreakpointsBatchId = (SELECT TOP 1 BatchId FROM MetalBreakpoints ORDER BY Id DESC)
WHERE MetalBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM MetalBreakpoints);

UPDATE Projects
SET ThermalBreakBreakpointsBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakBreakpoints ORDER BY Id DESC)
WHERE ThermalBreakBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakBreakpoints);

UPDATE Projects
SET PowderCostTableBatchId = (SELECT TOP 1 BatchId FROM PowderCostTable ORDER BY Id DESC)
WHERE PowderCostTableBatchId IS NULL AND EXISTS (SELECT 1 FROM PowderCostTable);

UPDATE Projects
SET FinishBreakpointsBatchId = (SELECT TOP 1 BatchId FROM FinishBreakpoints ORDER BY Id DESC)
WHERE FinishBreakpointsBatchId IS NULL AND EXISTS (SELECT 1 FROM FinishBreakpoints);

UPDATE Projects
SET SubsillTierPricesBatchId = (SELECT TOP 1 BatchId FROM SubsillTierPrices ORDER BY Id DESC)
WHERE SubsillTierPricesBatchId IS NULL AND EXISTS (SELECT 1 FROM SubsillTierPrices);

-- RegionAddresses table – contact / address details per region code (a/b)
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
);

-- ============================================================
-- ThermalBreakCost / ThermalBreakMaterialPriceGroup
-- Maps to "Pricing Matrix" Table_TB_Cost (cols S-AB) and the TB_MPG named
-- range. Used by the 2.Quote!AH column PLS/PDB "List less Discount" formulas.
-- Follows the same BatchId-linking pattern as the other Pricing Matrix
-- reference tables (MetalBreakpoints, ThermalBreakBreakpoints, etc.).
-- ============================================================

-- ThermalBreakCost table (maps to "Pricing Matrix" Table_TB_Cost, cols S-AB)
-- Per-section thermal-break (PLS/PDB) metal cost reference table used by the
-- 2.Quote!AH column formula's PLS/PDB branches.
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ThermalBreakCost')
CREATE TABLE ThermalBreakCost (
    Id INT IDENTITY PRIMARY KEY,
    Section NVARCHAR(50) NOT NULL,             -- col 1: Section code (matches ExtrusionMaster.Section)
    MetalCost DECIMAL(18,4) DEFAULT 0,         -- col 2: base metal cost for the reference length ($)
    ReferenceLengthM DECIMAL(18,4) DEFAULT 0,  -- col 3: reference length (m)
    MaterialPriceGroupCode NVARCHAR(10),       -- col 10: 2-char code looked up in TB_MPG
    MetalSellPrice DECIMAL(18,4) DEFAULT 0,    -- col 11: metal sell price for the reference length ($)
    BatchId INT NULL,
    EffectiveDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- ThermalBreakMaterialPriceGroup table (maps to TB_MPG named range)
-- Material price group $/kg rate lookup, used by the "List less Discount" PDB path.
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ThermalBreakMaterialPriceGroup')
CREATE TABLE ThermalBreakMaterialPriceGroup (
    Id INT IDENTITY PRIMARY KEY,
    GroupCode NVARCHAR(10) NOT NULL,      -- col 1: 2-char material price group code
    RatePerKg DECIMAL(18,4) DEFAULT 0,    -- col 2: $/kg rate for this group
    BatchId INT NULL,
    EffectiveDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_ThermalBreakCost_Section' AND object_id = OBJECT_ID('ThermalBreakCost'))
    CREATE INDEX IX_ThermalBreakCost_Section ON ThermalBreakCost(Section);

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_ThermalBreakMaterialPriceGroup_GroupCode' AND object_id = OBJECT_ID('ThermalBreakMaterialPriceGroup'))
    CREATE INDEX IX_ThermalBreakMaterialPriceGroup_GroupCode ON ThermalBreakMaterialPriceGroup(GroupCode);

-- Ensure BatchId column + backfill exist even for older installs where the
-- tables above might already exist without these columns (defensive parity
-- with the other Pricing Matrix reference tables).
IF COL_LENGTH('ThermalBreakCost','BatchId') IS NULL ALTER TABLE ThermalBreakCost ADD BatchId INT NULL;
IF COL_LENGTH('ThermalBreakCost','EffectiveDate') IS NULL ALTER TABLE ThermalBreakCost ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('ThermalBreakCost','UpdatedAt') IS NULL ALTER TABLE ThermalBreakCost ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

IF COL_LENGTH('ThermalBreakMaterialPriceGroup','BatchId') IS NULL ALTER TABLE ThermalBreakMaterialPriceGroup ADD BatchId INT NULL;
IF COL_LENGTH('ThermalBreakMaterialPriceGroup','EffectiveDate') IS NULL ALTER TABLE ThermalBreakMaterialPriceGroup ADD EffectiveDate DATETIME NOT NULL DEFAULT GETDATE();
IF COL_LENGTH('ThermalBreakMaterialPriceGroup','UpdatedAt') IS NULL ALTER TABLE ThermalBreakMaterialPriceGroup ADD UpdatedAt DATETIME NOT NULL DEFAULT GETDATE();

IF EXISTS (SELECT 1 FROM ThermalBreakCost WHERE BatchId IS NULL)
BEGIN
    DECLARE @tbcBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE ThermalBreakCost SET BatchId = @tbcBatch WHERE BatchId IS NULL;
END
IF EXISTS (SELECT 1 FROM ThermalBreakMaterialPriceGroup WHERE BatchId IS NULL)
BEGIN
    DECLARE @tbmpgBatch INT = NEXT VALUE FOR PricingBatchSeq;
    UPDATE ThermalBreakMaterialPriceGroup SET BatchId = @tbmpgBatch WHERE BatchId IS NULL;
END

-- Projects pin to a BatchId of each of these two reference tables as well.
IF COL_LENGTH('Projects','ThermalBreakCostBatchId') IS NULL ALTER TABLE Projects ADD ThermalBreakCostBatchId INT NULL;
IF COL_LENGTH('Projects','ThermalBreakMaterialPriceGroupBatchId') IS NULL ALTER TABLE Projects ADD ThermalBreakMaterialPriceGroupBatchId INT NULL;

UPDATE Projects
SET ThermalBreakCostBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakCost ORDER BY Id DESC)
WHERE ThermalBreakCostBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakCost);

UPDATE Projects
SET ThermalBreakMaterialPriceGroupBatchId = (SELECT TOP 1 BatchId FROM ThermalBreakMaterialPriceGroup ORDER BY Id DESC)
WHERE ThermalBreakMaterialPriceGroupBatchId IS NULL AND EXISTS (SELECT 1 FROM ThermalBreakMaterialPriceGroup);
