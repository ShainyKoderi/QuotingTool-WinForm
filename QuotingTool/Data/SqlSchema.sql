-- ============================================================
-- QuotingTool Database Schema
-- SQL Server / LocalDB
-- Maps to the Excel workbook structure used in the VBA macros
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
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- QuoteLines table (maps to "2.Quote" sheet, 6 page blocks of 40 rows each)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QuoteLines')
CREATE TABLE QuoteLines (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    PageBlock INT NOT NULL,         -- 1-6
    RowInBlock INT NOT NULL,        -- 1-40
    ExtrusionCode NVARCHAR(100),    -- col C
    Description NVARCHAR(200),      -- col D
    Quantity DECIMAL(18,4),         -- col G
    LengthM DECIMAL(18,6),          -- col H (metres)
    Price DECIMAL(18,4),            -- col I
    SellPrice DECIMAL(18,4),        -- col J
    SubsillFlag BIT DEFAULT 0,      -- col V
    SubsillSurcharge DECIMAL(18,4)  -- col K
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
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'EditPricing')
CREATE TABLE EditPricing (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    CostType INT DEFAULT 0,         -- C25
    FinType INT DEFAULT 0,          -- C16
    CostTypeOverride BIT DEFAULT 0, -- D25
    MetalPrice DECIMAL(18,4),       -- B33
    FinishingPrice DECIMAL(18,4),   -- B43
    Price_B42 DECIMAL(18,4) DEFAULT 99,
    Price_B44 DECIMAL(18,4) DEFAULT 99,
    Price_B48 DECIMAL(18,4) DEFAULT 99,
    Price_B50 DECIMAL(18,4) DEFAULT 99
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
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SheetLocks')
CREATE TABLE SheetLocks (
    Id INT IDENTITY PRIMARY KEY,
    ProjectId INT FOREIGN KEY REFERENCES Projects(Id),
    SheetName NVARCHAR(100) NOT NULL,
    IsLocked BIT DEFAULT 1,
    UpdatedAt DATETIME DEFAULT GETDATE()
);
