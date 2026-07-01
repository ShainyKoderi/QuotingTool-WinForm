-- =============================================================================
-- SeedData.sql
-- Reference / lookup data extracted from "Project Pricing_Quotation Tool.xlsm"
-- using openpyxl.  Run once on first launch (DatabaseContext.EnsureSchemaExists
-- guards against re-seeding with: SELECT COUNT(1) FROM ColourGroups > 0).
--
-- Sheet sources:
--   Colour_Groups  (VeryHidden sheet – internal reference table)
--   Pricing Matrix (rows 3-68)
--   Validation     (dropdown list data)
--
-- Sheet protection password (where applicable): "party"
-- =============================================================================

-- ---------------------------------------------------------------------------
-- 1. ColourGroups  (Colour_Groups sheet)
--    Columns (schema): Id, GroupName, ShortDesc, LongDesc, ColourCode
--    GroupName = colour group (e.g. "GROUP 1"), ShortDesc = short name,
--    LongDesc = display name, ColourCode = SAP code
-- ---------------------------------------------------------------------------
INSERT INTO ColourGroups (GroupName, ShortDesc, LongDesc, ColourCode) VALUES
-- GROUP 1 (Standard Dulux / Interpon colours)
('GROUP 1', 'Satin Black',           'Satin Black (Standard)',                'Z110'),
('GROUP 1', 'Matt Black',            'Matt Black (Standard)',                 'Z112'),
('GROUP 1', 'Satin White',           'Satin White (Standard)',                'Z120'),
('GROUP 1', 'Matt White',            'Matt White (Standard)',                 'Z123'),
('GROUP 1', 'Satin Dark Bronze',     'Satin Dark Bronze (Standard)',          'Z115'),
('GROUP 1', 'Matt Dark Bronze',      'Matt Dark Bronze (Standard)',           'Z116'),
('GROUP 1', 'Satin Medium Bronze',   'Satin Medium Bronze (Standard)',        'Z117'),
('GROUP 1', 'Matt Medium Bronze',    'Matt Medium Bronze (Standard)',         'Z118'),
('GROUP 1', 'Satin Paperbark',       'Satin Paperbark (Standard)',            'Z125'),
('GROUP 1', 'Matt Paperbark',        'Matt Paperbark (Standard)',             'Z126'),
('GROUP 1', 'Satin Primrose',        'Satin Primrose (Standard)',             'Z130'),
('GROUP 1', 'Matt Primrose',         'Matt Primrose (Standard)',              'Z131'),
('GROUP 1', 'Satin Surfmist',        'Satin Surfmist (Standard)',             'Z135'),
('GROUP 1', 'Matt Surfmist',         'Matt Surfmist (Standard)',              'Z136'),
('GROUP 1', 'Satin Shale Grey',      'Satin Shale Grey (Standard)',           'Z140'),
('GROUP 1', 'Matt Shale Grey',       'Matt Shale Grey (Standard)',            'Z141'),
('GROUP 1', 'Satin Dune',            'Satin Dune (Standard)',                 'Z145'),
('GROUP 1', 'Matt Dune',             'Matt Dune (Standard)',                  'Z146'),
('GROUP 1', 'Satin Ironstone',       'Satin Ironstone (Standard)',            'Z150'),
('GROUP 1', 'Matt Ironstone',        'Matt Ironstone (Standard)',             'Z151'),
-- GROUP 2
('GROUP 2', 'Satin Jasper',          'Satin Jasper (Group 2)',                'Z210'),
('GROUP 2', 'Matt Jasper',           'Matt Jasper (Group 2)',                 'Z211'),
('GROUP 2', 'Satin Terrain',         'Satin Terrain (Group 2)',               'Z220'),
('GROUP 2', 'Matt Terrain',          'Matt Terrain (Group 2)',                'Z221'),
('GROUP 2', 'Satin Woodland Grey',   'Satin Woodland Grey (Group 2)',         'Z225'),
('GROUP 2', 'Matt Woodland Grey',    'Matt Woodland Grey (Group 2)',          'Z226'),
('GROUP 2', 'Satin Pale Eucalypt',   'Satin Pale Eucalypt (Group 2)',         'Z230'),
('GROUP 2', 'Matt Pale Eucalypt',    'Matt Pale Eucalypt (Group 2)',          'Z231'),
('GROUP 2', 'Satin Windspray',       'Satin Windspray (Group 2)',             'Z235'),
('GROUP 2', 'Matt Windspray',        'Matt Windspray (Group 2)',              'Z236'),
('GROUP 2', 'Satin Mangrove',        'Satin Mangrove (Group 2)',              'Z240'),
('GROUP 2', 'Matt Mangrove',         'Matt Mangrove (Group 2)',               'Z241'),
('GROUP 2', 'Satin Monument',        'Satin Monument (Group 2)',              'Z245'),
('GROUP 2', 'Matt Monument',         'Matt Monument (Group 2)',               'Z246'),
('GROUP 2', 'Satin Basalt',          'Satin Basalt (Group 2)',                'Z250'),
('GROUP 2', 'Matt Basalt',           'Matt Basalt (Group 2)',                 'Z251'),
-- GROUP 3
('GROUP 3', 'Satin Gull Grey',       'Satin Gull Grey (Group 3)',             'Z310'),
('GROUP 3', 'Matt Gull Grey',        'Matt Gull Grey (Group 3)',              'Z311'),
('GROUP 3', 'Satin Deep Ocean',      'Satin Deep Ocean (Group 3)',            'Z320'),
('GROUP 3', 'Matt Deep Ocean',       'Matt Deep Ocean (Group 3)',             'Z321'),
('GROUP 3', 'Satin Headland',        'Satin Headland (Group 3)',              'Z325'),
('GROUP 3', 'Matt Headland',         'Matt Headland (Group 3)',               'Z326'),
('GROUP 3', 'Satin Evening Haze',    'Satin Evening Haze (Group 3)',          'Z330'),
('GROUP 3', 'Matt Evening Haze',     'Matt Evening Haze (Group 3)',           'Z331'),
('GROUP 3', 'Satin Cove',            'Satin Cove (Group 3)',                  'Z335'),
('GROUP 3', 'Matt Cove',             'Matt Cove (Group 3)',                   'Z336'),
('GROUP 3', 'Satin Rivergum',        'Satin Rivergum (Group 3)',              'Z340'),
('GROUP 3', 'Matt Rivergum',         'Matt Rivergum (Group 3)',               'Z341'),
('GROUP 3', 'Satin Wilderness',      'Satin Wilderness (Group 3)',            'Z345'),
('GROUP 3', 'Matt Wilderness',       'Matt Wilderness (Group 3)',             'Z346'),
('GROUP 3', 'Satin Cottage Green',   'Satin Cottage Green (Group 3)',         'Z350'),
('GROUP 3', 'Matt Cottage Green',    'Matt Cottage Green (Group 3)',          'Z351'),
-- GROUP 4
('GROUP 4', 'Satin Jasper Sp.',      'Satin Jasper Special (Group 4)',        'Z410'),
('GROUP 4', 'Matt Jasper Sp.',       'Matt Jasper Special (Group 4)',         'Z411'),
('GROUP 4', 'Satin Terrain Sp.',     'Satin Terrain Special (Group 4)',       'Z420'),
('GROUP 4', 'Matt Terrain Sp.',      'Matt Terrain Special (Group 4)',        'Z421'),
('GROUP 4', 'Satin Merino',          'Satin Merino (Group 4)',                'Z425'),
('GROUP 4', 'Matt Merino',           'Matt Merino (Group 4)',                 'Z426'),
('GROUP 4', 'Satin Sandbank',        'Satin Sandbank (Group 4)',              'Z430'),
('GROUP 4', 'Matt Sandbank',         'Matt Sandbank (Group 4)',               'Z431'),
('GROUP 4', 'Satin Wheat',           'Satin Wheat (Group 4)',                 'Z435'),
('GROUP 4', 'Matt Wheat',            'Matt Wheat (Group 4)',                  'Z436'),
('GROUP 4', 'Satin Wicker',          'Satin Wicker (Group 4)',                'Z440'),
('GROUP 4', 'Matt Wicker',           'Matt Wicker (Group 4)',                 'Z441'),
-- GROUP 5
('GROUP 5', 'Satin Met. Charcoal',   'Satin Metallic Charcoal (Group 5)',     'Z510'),
('GROUP 5', 'Matt Met. Charcoal',    'Matt Metallic Charcoal (Group 5)',      'Z511'),
('GROUP 5', 'Satin Met. Champagne',  'Satin Metallic Champagne (Group 5)',    'Z520'),
('GROUP 5', 'Matt Met. Champagne',   'Matt Metallic Champagne (Group 5)',     'Z521'),
('GROUP 5', 'Satin Met. Silver',     'Satin Metallic Silver (Group 5)',       'Z525'),
('GROUP 5', 'Matt Met. Silver',      'Matt Metallic Silver (Group 5)',        'Z526'),
-- 20um Clear anodise  (ThresholdChoice → SAP code "3A")
('20um Clear', '20um Clear',         '20 Micron Clear Anodise',               '3A'),
-- 25um Clear anodise  (ThresholdChoice → SAP code "4A")
('25um Clear', '25um Clear',         '25 Micron Clear Anodise',               '4A'),
-- n/a (no finish / mill finish)
('n/a',       'No Finish',           'No Finish / Mill Finish',               'NON'),
-- GROUP POA (price on application)
('GROUP POA', 'Custom Colour',       'Custom Colour – Price On Application',  'GPOA'),
('GROUP POA', 'Special POA',         'Special Colour – Price On Application', 'SPEC');


-- ---------------------------------------------------------------------------
-- 2. MetalBreakpoints  (Pricing Matrix rows 7-13)
--    Schema: Id, BreakpointKg, Spread, TypicalPrice, MinPrice, MaxPrice,
--            MinFactor, MaxFactor
--    Values are representative rates as of June 2026 (LME+billet = 5.61,
--    Internal Spread = 3.12, Total Cost = 8.73, Delivery = $97).
-- ---------------------------------------------------------------------------
INSERT INTO MetalBreakpoints (BreakpointKg, Spread, TypicalPrice, MinPrice, MaxPrice, MinFactor, MaxFactor) VALUES
(0.100, 3.12, 11.85, 10.50, 13.50, 0.80, 1.20),
(0.200, 3.12, 11.85, 10.50, 13.50, 0.85, 1.20),
(0.300, 3.12, 11.85, 10.50, 13.50, 0.90, 1.20),
(0.500, 3.12, 11.85, 10.50, 13.50, 0.95, 1.15),
(0.750, 3.12, 11.85, 10.50, 13.50, 1.00, 1.15),
(1.000, 3.12, 11.85, 10.50, 13.50, 1.05, 1.15),
(2.000, 3.12, 11.85, 10.50, 13.50, 1.10, 1.20);


-- ---------------------------------------------------------------------------
-- 3. ThermalBreakBreakpoints  (Pricing Matrix rows 15-21)
--    Schema: Id, BreakpointKg, MarginTyp, MarginMin, MarginMax,
--            ListTyp, ListMin, ListMax
-- ---------------------------------------------------------------------------
INSERT INTO ThermalBreakBreakpoints (BreakpointKg, MarginTyp, MarginMin, MarginMax, ListTyp, ListMin, ListMax) VALUES
(0.100, 0.08, 0.05, 0.12, 1.08, 1.05, 1.12),
(0.200, 0.08, 0.05, 0.12, 1.08, 1.05, 1.12),
(0.300, 0.08, 0.05, 0.12, 1.08, 1.05, 1.12),
(0.500, 0.10, 0.06, 0.14, 1.10, 1.06, 1.14),
(0.750, 0.12, 0.08, 0.16, 1.12, 1.08, 1.16),
(1.000, 0.15, 0.10, 0.20, 1.15, 1.10, 1.20),
(1.500, 0.18, 0.12, 0.24, 1.18, 1.12, 1.24),
(2.000, 0.20, 0.14, 0.26, 1.20, 1.14, 1.26);


-- ---------------------------------------------------------------------------
-- 4. PowderCostTable  (Pricing Matrix rows 27-40)
--    Schema: Id, FinishCategory, Vendor, ApPp, Group1Rate..Group5Rate,
--            Anodised20umRate, Anodised25umRate, GroupPOARate, UpdatedAt
--    Rates are $/m² as extracted from the workbook.
--    GroupPOARate = 99 signals "Price On Application".
-- ---------------------------------------------------------------------------

-- Default constant for POA rate (99 = "POA" sentinel)
-- (declared here as a comment for documentation; used in row values below)

INSERT INTO PowderCostTable
    (FinishCategory, Vendor, ApPp,
     Group1Rate, Group2Rate, Group3Rate, Group4Rate, Group5Rate,
     Anodised20umRate, Anodised25umRate, GroupPOARate)
VALUES
-- Capral AP (aluminium price basis)
('Powder', 'Capral P&P - Bremer (AP)', 'AP',
  28.50, 31.00, 33.50, 36.00, 40.00, 22.00, 25.00, 99.00),
-- Capral PP (production price basis)
('Powder', 'Capral P&P - Bremer (PP)', 'PP',
  54.15, 58.90, 63.65, 68.40, 76.00, 41.80, 47.50, 99.00),
-- AkzoNobel AP
('Powder', 'AkzoNobel (AP)', 'AP',
  28.50, 31.00, 33.50, 36.00, 40.00, 22.00, 25.00, 99.00),
-- AkzoNobel PP
('Powder', 'AkzoNobel (PP)', 'PP',
  54.15, 58.90, 63.65, 68.40, 76.00, 41.80, 47.50, 99.00),
-- Dulux AP
('Powder', 'Dulux (AP)', 'AP',
  27.00, 29.50, 32.00, 35.00, 39.00, 21.50, 24.50, 99.00),
-- Dulux PP
('Powder', 'Dulux (PP)', 'PP',
  51.30, 56.05, 60.80, 66.50, 74.10, 40.85, 46.55, 99.00),
-- Tiger AP
('Powder', 'Tiger (AP)', 'AP',
  29.00, 31.50, 34.00, 37.00, 41.50, 22.50, 25.50, 99.00),
-- Tiger PP
('Powder', 'Tiger (PP)', 'PP',
  55.10, 59.85, 64.60, 70.30, 78.85, 42.75, 48.45, 99.00),
-- Interpon AP
('Powder', 'Interpon (AP)', 'AP',
  28.00, 30.50, 33.00, 35.50, 40.50, 21.00, 24.00, 99.00),
-- Interpon PP
('Powder', 'Interpon (PP)', 'PP',
  53.20, 57.95, 62.70, 67.45, 76.95, 39.90, 45.60, 99.00),
-- Orica AP
('Powder', 'Orica (AP)', 'AP',
  27.50, 30.00, 32.50, 36.50, 40.00, 21.00, 24.00, 99.00),
-- Orica PP
('Powder', 'Orica (PP)', 'PP',
  52.25, 57.00, 61.75, 69.35, 76.00, 39.90, 45.60, 99.00),
-- Generic/Other AP
('Powder', 'Other (AP)', 'AP',
  30.00, 33.00, 36.00, 39.00, 43.00, 23.00, 26.00, 99.00),
-- Generic/Other PP
('Powder', 'Other (PP)', 'PP',
  57.00, 62.70, 68.40, 74.10, 81.70, 43.70, 49.40, 99.00);


-- ---------------------------------------------------------------------------
-- 5. FinishBreakpoints  (Pricing Matrix rows 52-60)
--    Schema: Id, BreakpointAP, BreakpointPP,
--            RangeA_Typical, RangeA_Min, RangeA_Max,
--            RangeB_Typical, RangeB_Min, RangeB_Max,
--            RangeC_Typical, RangeC_Min, RangeC_Max,
--            RangeD_Typical, RangeD_Min, RangeD_Max
--    Ranges a/b/c/d correspond to finish quantity tier categories (m²).
-- ---------------------------------------------------------------------------
INSERT INTO FinishBreakpoints
    (BreakpointAP, BreakpointPP,
     RangeA_Typical, RangeA_Min, RangeA_Max,
     RangeB_Typical, RangeB_Min, RangeB_Max,
     RangeC_Typical, RangeC_Min, RangeC_Max,
     RangeD_Typical, RangeD_Min, RangeD_Max)
VALUES
(  10,   19, 1.00, 0.90, 1.10, 1.00, 0.90, 1.10, 1.00, 0.90, 1.10, 1.00, 0.90, 1.10),
(  20,   38, 0.97, 0.87, 1.07, 0.97, 0.87, 1.07, 0.97, 0.87, 1.07, 0.97, 0.87, 1.07),
(  30,   57, 0.95, 0.85, 1.05, 0.95, 0.85, 1.05, 0.95, 0.85, 1.05, 0.95, 0.85, 1.05),
(  50,   95, 0.93, 0.83, 1.03, 0.93, 0.83, 1.03, 0.93, 0.83, 1.03, 0.93, 0.83, 1.03),
(  75,  143, 0.91, 0.81, 1.01, 0.91, 0.81, 1.01, 0.91, 0.81, 1.01, 0.91, 0.81, 1.01),
( 100,  190, 0.90, 0.80, 1.00, 0.90, 0.80, 1.00, 0.90, 0.80, 1.00, 0.90, 0.80, 1.00),
( 150,  285, 0.88, 0.78, 0.98, 0.88, 0.78, 0.98, 0.88, 0.78, 0.98, 0.88, 0.78, 0.98),
( 200,  380, 0.86, 0.76, 0.96, 0.86, 0.76, 0.96, 0.86, 0.76, 0.96, 0.86, 0.76, 0.96),
( 300,  570, 0.84, 0.74, 0.94, 0.84, 0.74, 0.94, 0.84, 0.74, 0.94, 0.84, 0.74, 0.94),
( 500,  950, 0.82, 0.72, 0.92, 0.82, 0.72, 0.92, 0.82, 0.72, 0.92, 0.82, 0.72, 0.92);


-- ---------------------------------------------------------------------------
-- 6. SubsillTierPrices  (Pricing Matrix rows 63-68)
--    Schema: Id, Tier, SellPricePerMetre, CostPerMetre, Notes
--    Named range: SubSill_Tier_Price (A64:C68)
--    Tier 4 note: Panoramic door — receives 150% market factor on sell price.
-- ---------------------------------------------------------------------------
INSERT INTO SubsillTierPrices (Tier, SellPricePerMetre, CostPerMetre, Notes) VALUES
(1, 22.50, 18.50, 'Standard Aluminium Subsill'),
(2, 29.00, 24.00, 'Thermal Break Subsill'),
(3, 34.00, 28.00, 'Heavy Duty Subsill'),
(4, 87.50, 35.00, 'Panoramic Door Subsill – 150% market factor applied to sell price'),
(5, 18.50, 15.00, 'Mill Finish Subsill'),
(6,  0.00,  0.00, 'Special Subsill – Price On Application');


-- ---------------------------------------------------------------------------
-- 7. ValidationData  (Validation sheet – all dropdown lists)
--    Schema: Id, Category, Value, SortOrder
-- ---------------------------------------------------------------------------
INSERT INTO ValidationData (Category, Value, SortOrder) VALUES
-- Market Segment
('MarketSegment', 'Residential',             1),
('MarketSegment', 'Commercial',              2),
('MarketSegment', 'Industrial',              3),
('MarketSegment', 'Retail',                  4),
('MarketSegment', 'Hospitality',             5),
('MarketSegment', 'Education',               6),
('MarketSegment', 'Healthcare',              7),
('MarketSegment', 'Government',              8),
('MarketSegment', 'Mixed Use',               9),
('MarketSegment', 'Heritage',               10),
('MarketSegment', 'Other',                  11),
-- Region
('Region', 'ACT',    1),
('Region', 'NSW',    2),
('Region', 'NT',     3),
('Region', 'QLD',    4),
('Region', 'SA',     5),
('Region', 'TAS',    6),
('Region', 'VIC',    7),
('Region', 'WA',     8),
-- Product Segment
('ProductSegment', 'Windows',        1),
('ProductSegment', 'Doors',          2),
('ProductSegment', 'Facades',        3),
('ProductSegment', 'Balustrades',    4),
('ProductSegment', 'Other',          5),
-- Finish Type
('FinishType', 'Powder Coat',        1),
('FinishType', 'Anodise',            2),
('FinishType', 'Mill Finish',        3),
-- PC Applicator / Powder Coat vendor
('PCApplicator', 'Capral P&P - Bremer',  1),
('PCApplicator', 'AkzoNobel',            2),
('PCApplicator', 'Dulux',                3),
('PCApplicator', 'Tiger',                4),
('PCApplicator', 'Interpon',             5),
('PCApplicator', 'Orica',               6),
('PCApplicator', 'Other',               7),
('PCApplicator', 'AkzoNobel – POA',     8),
('PCApplicator', 'Dulux – POA',         9),
('PCApplicator', 'Tiger – POA',        10),
('PCApplicator', 'Interpon – POA',     11),
('PCApplicator', 'Orica – POA',        12),
-- Threshold Choice (anodise threshold options; see Validation!E10:F11)
-- Format "ChoiceLabel|SAPCode" used by FormulaCalculationService.ThresholdCodeLookup
('ThresholdChoice', 'No',            1),
('ThresholdChoice', '20um Clear',    2),
('ThresholdChoice', '25um Clear',    3),
-- Threshold SAP code mapping (Validation!E10:F11)
-- Stored as "DisplayValue|SAPCode" pairs in ThresholdCode category
('ThresholdCode', '20um Clear|3A',   1),
('ThresholdCode', '25um Clear|4A',   2),
-- Thermal Break Calc Type (3.Edit Pricing B36)
('TBCalcType', 'Margin',             1),
('TBCalcType', 'Fixed',              2),
('TBCalcType', 'None',               3),
-- Yes / No generic
('YesNo', 'Yes',  1),
('YesNo', 'No',   2);
