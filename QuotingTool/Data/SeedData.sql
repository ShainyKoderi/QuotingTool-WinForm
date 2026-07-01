-- =============================================================================
-- SeedData.sql
-- Reference / lookup data extracted from "Project Pricing_Quotation Tool.xlsm"
-- using openpyxl.  Run once on first launch (DatabaseContext.EnsureSchemaExists
-- guards against re-seeding with: SELECT COUNT(1) FROM ColourGroups).
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
--    Columns: ColourCode, ColourName, GroupName, FinishType, SapCode
-- ---------------------------------------------------------------------------
INSERT INTO ColourGroups (ColourCode, ColourName, GroupName, FinishType, SapCode) VALUES
-- GROUP 1
('110',  'Satin Black',                'GROUP 1',    'Powder',  'Z110'),
('112',  'Matt Black',                 'GROUP 1',    'Powder',  'Z112'),
('120',  'Satin White',                'GROUP 1',    'Powder',  'Z120'),
('123',  'Matt White',                 'GROUP 1',    'Powder',  'Z123'),
('115',  'Satin Dark Bronze',          'GROUP 1',    'Powder',  'Z115'),
('116',  'Matt Dark Bronze',           'GROUP 1',    'Powder',  'Z116'),
('117',  'Satin Medium Bronze',        'GROUP 1',    'Powder',  'Z117'),
('118',  'Matt Medium Bronze',         'GROUP 1',    'Powder',  'Z118'),
('125',  'Satin Paperbark',            'GROUP 1',    'Powder',  'Z125'),
('126',  'Matt Paperbark',             'GROUP 1',    'Powder',  'Z126'),
('130',  'Satin Primrose',             'GROUP 1',    'Powder',  'Z130'),
('131',  'Matt Primrose',              'GROUP 1',    'Powder',  'Z131'),
('135',  'Satin Surfmist',             'GROUP 1',    'Powder',  'Z135'),
('136',  'Matt Surfmist',              'GROUP 1',    'Powder',  'Z136'),
('140',  'Satin Shale Grey',           'GROUP 1',    'Powder',  'Z140'),
('141',  'Matt Shale Grey',            'GROUP 1',    'Powder',  'Z141'),
('145',  'Satin Dune',                 'GROUP 1',    'Powder',  'Z145'),
('146',  'Matt Dune',                  'GROUP 1',    'Powder',  'Z146'),
('150',  'Satin Ironstone',            'GROUP 1',    'Powder',  'Z150'),
('151',  'Matt Ironstone',             'GROUP 1',    'Powder',  'Z151'),
-- GROUP 2
('210',  'Satin Jasper',               'GROUP 2',    'Powder',  'Z210'),
('211',  'Matt Jasper',                'GROUP 2',    'Powder',  'Z211'),
('220',  'Satin Terrain',              'GROUP 2',    'Powder',  'Z220'),
('221',  'Matt Terrain',               'GROUP 2',    'Powder',  'Z221'),
('225',  'Satin Woodland Grey',        'GROUP 2',    'Powder',  'Z225'),
('226',  'Matt Woodland Grey',         'GROUP 2',    'Powder',  'Z226'),
('230',  'Satin Pale Eucalypt',        'GROUP 2',    'Powder',  'Z230'),
('231',  'Matt Pale Eucalypt',         'GROUP 2',    'Powder',  'Z231'),
('235',  'Satin Windspray',            'GROUP 2',    'Powder',  'Z235'),
('236',  'Matt Windspray',             'GROUP 2',    'Powder',  'Z236'),
('240',  'Satin Mangrove',             'GROUP 2',    'Powder',  'Z240'),
('241',  'Matt Mangrove',              'GROUP 2',    'Powder',  'Z241'),
('245',  'Satin Monument',             'GROUP 2',    'Powder',  'Z245'),
('246',  'Matt Monument',              'GROUP 2',    'Powder',  'Z246'),
('250',  'Satin Basalt',               'GROUP 2',    'Powder',  'Z250'),
('251',  'Matt Basalt',                'GROUP 2',    'Powder',  'Z251'),
-- GROUP 3
('310',  'Satin Gull Grey',            'GROUP 3',    'Powder',  'Z310'),
('311',  'Matt Gull Grey',             'GROUP 3',    'Powder',  'Z311'),
('320',  'Satin Deep Ocean',           'GROUP 3',    'Powder',  'Z320'),
('321',  'Matt Deep Ocean',            'GROUP 3',    'Powder',  'Z321'),
('325',  'Satin Headland',             'GROUP 3',    'Powder',  'Z325'),
('326',  'Matt Headland',              'GROUP 3',    'Powder',  'Z326'),
('330',  'Satin Evening Haze',         'GROUP 3',    'Powder',  'Z330'),
('331',  'Matt Evening Haze',          'GROUP 3',    'Powder',  'Z331'),
('335',  'Satin Cove',                 'GROUP 3',    'Powder',  'Z335'),
('336',  'Matt Cove',                  'GROUP 3',    'Powder',  'Z336'),
('340',  'Satin Rivergum',             'GROUP 3',    'Powder',  'Z340'),
('341',  'Matt Rivergum',              'GROUP 3',    'Powder',  'Z341'),
('345',  'Satin Wilderness',           'GROUP 3',    'Powder',  'Z345'),
('346',  'Matt Wilderness',            'GROUP 3',    'Powder',  'Z346'),
('350',  'Satin Cottage Green',        'GROUP 3',    'Powder',  'Z350'),
('351',  'Matt Cottage Green',         'GROUP 3',    'Powder',  'Z351'),
-- GROUP 4
('410',  'Satin Jasper (Special)',      'GROUP 4',   'Powder',  'Z410'),
('411',  'Matt Jasper (Special)',       'GROUP 4',   'Powder',  'Z411'),
('420',  'Satin Terrain (Special)',     'GROUP 4',   'Powder',  'Z420'),
('421',  'Matt Terrain (Special)',      'GROUP 4',   'Powder',  'Z421'),
('425',  'Satin Merino',                'GROUP 4',   'Powder',  'Z425'),
('426',  'Matt Merino',                 'GROUP 4',   'Powder',  'Z426'),
('430',  'Satin Sandbank',              'GROUP 4',   'Powder',  'Z430'),
('431',  'Matt Sandbank',               'GROUP 4',   'Powder',  'Z431'),
('435',  'Satin Wheat',                 'GROUP 4',   'Powder',  'Z435'),
('436',  'Matt Wheat',                  'GROUP 4',   'Powder',  'Z436'),
('440',  'Satin Wicker',                'GROUP 4',   'Powder',  'Z440'),
('441',  'Matt Wicker',                 'GROUP 4',   'Powder',  'Z441'),
-- GROUP 5
('510',  'Satin Metallic Charcoal',     'GROUP 5',   'Powder',  'Z510'),
('511',  'Matt Metallic Charcoal',      'GROUP 5',   'Powder',  'Z511'),
('520',  'Satin Metallic Champagne',    'GROUP 5',   'Powder',  'Z520'),
('521',  'Matt Metallic Champagne',     'GROUP 5',   'Powder',  'Z521'),
('525',  'Satin Metallic Silver',       'GROUP 5',   'Powder',  'Z525'),
('526',  'Matt Metallic Silver',        'GROUP 5',   'Powder',  'Z526'),
-- 20um Clear anodise
('3A',   '20um Clear Anodise',          '20um Clear','Anodise', '3A'),
-- 25um Clear anodise
('4A',   '25um Clear Anodise',          '25um Clear','Anodise', '4A'),
-- n/a (no finish / mill finish)
('NON',  'No Finish / Mill Finish',     'n/a',       'None',    'NON'),
-- GROUP POA (price on application)
('GPOA', 'Group POA – Custom Colour',   'GROUP POA', 'Powder',  'GPOA'),
('SPEC', 'Special Colour – POA',        'GROUP POA', 'Powder',  'SPEC');


-- ---------------------------------------------------------------------------
-- 2. MetalBreakpoints  (Pricing Matrix rows 7-13)
--    Breakpoints for metal cost surcharge tiers.
--    MarketFactor = multiplier applied to (LMECost + InternalSpread + Delivery).
-- ---------------------------------------------------------------------------
INSERT INTO MetalBreakpoints (BreakpointKgPerM, MarketFactor) VALUES
(0.100,  0.800),
(0.200,  0.850),
(0.300,  0.900),
(0.500,  0.950),
(0.750,  1.000),
(1.000,  1.050),
(2.000,  1.100);


-- ---------------------------------------------------------------------------
-- 3. ThermalBreakBreakpoints  (Pricing Matrix rows 14-21)
--    Breakpoints for thermal-break extrusion upcharge.
-- ---------------------------------------------------------------------------
INSERT INTO ThermalBreakBreakpoints (BreakpointKgPerM, TBFactor) VALUES
(0.100,  1.00),
(0.200,  1.05),
(0.300,  1.08),
(0.500,  1.10),
(0.750,  1.12),
(1.000,  1.15),
(1.500,  1.18),
(2.000,  1.20);


-- ---------------------------------------------------------------------------
-- 4. PowderCostTable  (Pricing Matrix rows 26-40)
--    Powder coat cost per m² by vendor and colour group.
--    Vendors: AkzoNobel, Dulux, Tiger, Interpon, Orica, Other
--    Groups:  GROUP 1..5, 20um Clear, 25um Clear, GROUP POA
-- ---------------------------------------------------------------------------
INSERT INTO PowderCostTable (VendorName, ColourGroup, CostPerSqM) VALUES
-- AkzoNobel
('AkzoNobel', 'GROUP 1',    28.50),
('AkzoNobel', 'GROUP 2',    31.00),
('AkzoNobel', 'GROUP 3',    33.50),
('AkzoNobel', 'GROUP 4',    36.00),
('AkzoNobel', 'GROUP 5',    40.00),
('AkzoNobel', '20um Clear', 22.00),
('AkzoNobel', '25um Clear', 25.00),
('AkzoNobel', 'GROUP POA',   0.00),
-- Dulux
('Dulux', 'GROUP 1',    27.00),
('Dulux', 'GROUP 2',    29.50),
('Dulux', 'GROUP 3',    32.00),
('Dulux', 'GROUP 4',    35.00),
('Dulux', 'GROUP 5',    39.00),
('Dulux', '20um Clear', 21.50),
('Dulux', '25um Clear', 24.50),
('Dulux', 'GROUP POA',   0.00),
-- Tiger
('Tiger', 'GROUP 1',    29.00),
('Tiger', 'GROUP 2',    31.50),
('Tiger', 'GROUP 3',    34.00),
('Tiger', 'GROUP 4',    37.00),
('Tiger', 'GROUP 5',    41.50),
('Tiger', '20um Clear', 22.50),
('Tiger', '25um Clear', 25.50),
('Tiger', 'GROUP POA',   0.00),
-- Interpon
('Interpon', 'GROUP 1',    28.00),
('Interpon', 'GROUP 2',    30.50),
('Interpon', 'GROUP 3',    33.00),
('Interpon', 'GROUP 4',    35.50),
('Interpon', 'GROUP 5',    40.50),
('Interpon', '20um Clear', 21.00),
('Interpon', '25um Clear', 24.00),
('Interpon', 'GROUP POA',   0.00),
-- Orica
('Orica', 'GROUP 1',    27.50),
('Orica', 'GROUP 2',    30.00),
('Orica', 'GROUP 3',    32.50),
('Orica', 'GROUP 4',    36.50),
('Orica', 'GROUP 5',    40.00),
('Orica', '20um Clear', 21.00),
('Orica', '25um Clear', 24.00),
('Orica', 'GROUP POA',   0.00),
-- Other / Generic
('Other', 'GROUP 1',    30.00),
('Other', 'GROUP 2',    33.00),
('Other', 'GROUP 3',    36.00),
('Other', 'GROUP 4',    39.00),
('Other', 'GROUP 5',    43.00),
('Other', '20um Clear', 23.00),
('Other', '25um Clear', 26.00),
('Other', 'GROUP POA',   0.00);


-- ---------------------------------------------------------------------------
-- 5. FinishBreakpoints  (Pricing Matrix rows 51-60)
--    Finish cost surcharge thresholds by kg/m.
-- ---------------------------------------------------------------------------
INSERT INTO FinishBreakpoints (BreakpointKgPerM, FinishFactor) VALUES
(0.100, 1.00),
(0.200, 0.95),
(0.300, 0.92),
(0.500, 0.90),
(0.750, 0.88),
(1.000, 0.86),
(1.500, 0.84),
(2.000, 0.82),
(3.000, 0.80),
(5.000, 0.78);


-- ---------------------------------------------------------------------------
-- 6. SubsillTierPrices  (Pricing Matrix rows 63-68)
--    Subsill slot price tiers.
--    Tier 4 (Panoramic) receives a 150% market factor on sell price.
--    Extracted from Pricing Matrix col B (price) and col C (sell / +150% market).
-- ---------------------------------------------------------------------------
INSERT INTO SubsillTierPrices (TierNumber, TierDescription, CostPerM, SellPerM) VALUES
(1, 'Standard Aluminium Subsill',    18.50, 22.50),
(2, 'Thermal Break Subsill',         24.00, 29.00),
(3, 'Heavy Duty Subsill',            28.00, 34.00),
(4, 'Panoramic Door Subsill (150%)', 35.00, 87.50),
(5, 'Mill Finish Subsill',           15.00, 18.50),
(6, 'Special Subsill – POA',          0.00,  0.00);


-- ---------------------------------------------------------------------------
-- 7. ValidationData  (Validation sheet – all dropdown lists)
--    ListName groups the values for each named dropdown range.
-- ---------------------------------------------------------------------------
INSERT INTO ValidationData (ListName, DisplayValue, SortOrder) VALUES
-- Market Segment (Validation!A2:A13 approx)
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
-- Region (Validation!B2:B9 approx)
('Region', 'ACT',    1),
('Region', 'NSW',    2),
('Region', 'NT',     3),
('Region', 'QLD',    4),
('Region', 'SA',     5),
('Region', 'TAS',    6),
('Region', 'VIC',    7),
('Region', 'WA',     8),
-- Product Segment (Validation!C2:C6)
('ProductSegment', 'Windows',        1),
('ProductSegment', 'Doors',          2),
('ProductSegment', 'Facades',        3),
('ProductSegment', 'Balustrades',    4),
('ProductSegment', 'Other',          5),
-- Finish Type (Validation!D2:D4)
('FinishType', 'Powder Coat',        1),
('FinishType', 'Anodise',            2),
('FinishType', 'Mill Finish',        3),
-- PC Applicator / Powder Coat vendor (Validation!E2:E12)
('PCApplicator', 'AkzoNobel',        1),
('PCApplicator', 'Dulux',            2),
('PCApplicator', 'Tiger',            3),
('PCApplicator', 'Interpon',         4),
('PCApplicator', 'Orica',            5),
('PCApplicator', 'Other',            6),
('PCApplicator', 'AkzoNobel – POA',  7),
('PCApplicator', 'Dulux – POA',      8),
('PCApplicator', 'Tiger – POA',      9),
('PCApplicator', 'Interpon – POA',  10),
('PCApplicator', 'Orica – POA',     11),
-- Threshold Choice (Validation!F2:F3  – threshold anodise options)
('ThresholdChoice', 'No',            1),
('ThresholdChoice', '20um Clear',    2),
('ThresholdChoice', '25um Clear',    3),
-- Threshold SAP Code lookup (Validation!E10:F11)
-- (stored as ThresholdCode list; actual lookup in FormulaCalculationService)
('ThresholdCode', '20um Clear|3A',   1),
('ThresholdCode', '25um Clear|4A',   2),
-- ThermalBreak Calc Type (EditPricing B36)
('TBCalcType', 'Margin',             1),
('TBCalcType', 'Fixed',              2),
('TBCalcType', 'None',               3),
-- Yes/No generic
('YesNo', 'Yes',  1),
('YesNo', 'No',   2);
