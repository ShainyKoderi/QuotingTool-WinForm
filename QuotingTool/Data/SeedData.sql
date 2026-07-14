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

-- ---------------------------------------------------------------------------
-- 8. ThresholdSections (THRESHOLDS named range from Validation!$N:$N)
--    Section codes that trigger the anodised-threshold SAP colour code (col BF = 1)
-- ---------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM ThresholdSections)
INSERT INTO ThresholdSections (Section) VALUES ('EP2366'), ('EP5796'), ('EP15939'), ('E35737'), ('EP2212'), ('EP2365'), ('EP20049'), ('EP13539'), ('EP13540'), ('EP22082'), ('EP22083'), ('E01854'), ('EP20050');

-- ---------------------------------------------------------------------------
-- 9. IPMasterData (5.Edit_PH_IP sheet, cols B=PH code, D=IP category 0-3)
--    Maps 18-digit SAP PH product hierarchy code → IP upcharge grade per section
-- ---------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM IPMasterData)
INSERT INTO IPMasterData (PhCode, IpCategory) VALUES
('100012120031200020', 0),
('100012120031200074', 0),
('100012122431224000', 1),
('100012130031300034', 0),
('100012130031300021', 0),
('100012130031300015', 0),
('100012130031300010', 0),
('100012130031300019', 0),
('100012130031300016', 0),
('100012130031300013', 0),
('100012130031300014', 0),
('100012130031300033', 0),
('100012130931309000', 0),
('100012130031300018', 0),
('100012120031200013', 0),
('100012120031200004', 0),
('100012120031200026', 0),
('100012120031200038', 0),
('100012130131301051', 0),
('100012130031300039', 0),
('100012150031500001', 0),
('100012130031301091', 0),
('100012120131201000', 0),
('100012120031200031', 0),
('100012130031300037', 0),
('100012130131301001', 0),
('100012130131301053', 0),
('100012130031300022', 0),
('100012150731507000', 0),
('100012130531305001', 0),
('100012150031500002', 0),
('100012130531305000', 0),
('100012120031200017', 0),
('100012120031200003', 0),
('100012150531505006', 0),
('100012120031200016', 0),
('100012120031200024', 0),
('100012120031200034', 0),
('100012120031200047', 0),
('100012120031200029', 0),
('100012130031300028', 0),
('100012120031200027', 0),
('100012125031202055', 0),
('100012130331203000', 0),
('100012120031200018', 0),
('100012120031200008', 0),
('100012120031200098', 0),
('100012120031200030', 0),
('100012125031202040', 0),
('100012120031200900', 0),
('100012150031500005', 0),
('100012180031700025', 0),
('100012130031300035', 0),
('100012150431504000', 0),
('100012150531505003', 0),
('100012150031500025', 0),
('100012180031700024', 0),
('100012150531505002', 0),
('100012150031500055', 0),
('100012130731307000', 0),
('100012150531505000', 0),
('100012150031500004', 0),
('100012150031500015', 0),
('100012120031200073', 0),
('100012150031500006', 0),
('100012120031200000', 0),
('100012120031200107', 0),
('100012120031200039', 0),
('100012150031500003', 0),
('100012120131201002', 0),
('100012120031200400', 0),
('100012135031304010', 0),
('100012130031300032', 0),
('100012180131800000', 0),
('100012130031300036', 0),
('100012120031200103', 0),
('100012130831308000', 0),
('100012120031200102', 0),
('100012160031602011', 0),
('100012120031200420', 0),
('100012120031200101', 0),
('100012120031200097', 0),
('100012120031200032', 0),
('100012120031200033', 0),
('100012120031200007', 0),
('100012160031600002', 0),
('100012150031500070', 0),
('100012130031300017', 0),
('100012160031602012', 0),
('100012160031601018', 0),
('100012160031601015', 0),
('100012160531605000', 0),
('100012160031601016', 0),
('100012150531505007', 0),
('100012150531505004', 0),
('100012125031250001', 1),
('100012150031500017', 0),
('100012120031200012', 0),
('100012161531615001', 0),
('100012130131301002', 0),
('100012130131301003', 0),
('100012125031202140', 0),
('100012125031202130', 0),
('100012125031202065', 0),
('100012120131201005', 0),
('100012150031500007', 0),
('100022270032700020', 0),
('100012150031500014', 0),
('100012160031600007', 0),
('100012150031500008', 0),
('100012125031202035', 0),
('100012125031202250', 2),
('100012160031602010', 0),
('100032395033950002', 0),
('100012120031200043', 1),
('100012120031200045', 0),
('100012120031200028', 0),
('100012120031200040', 1),
('100012120031200015', 0),
('100012125031250004', 1),
('100012125031250002', 1),
('100012125031250003', 1),
('100012120031200104', 0),
('100012120031200023', 0),
('100012120031200048', 0),
('100012130131301005', 0),
('100012120131201001', 0),
('100012125031202190', 0),
('100012130231302000', 0),
('100012160031600005', 0),
('100012130131301018', 0),
('100012170031700022', 0),
('100012120031200010', 0),
('100012120031200001', 0),
('100012130131301020', 0),
('100012125031202020', 0),
('100012125031202195', 0),
('100012125031202015', 0),
('100012125031202030', 0),
('100012125031202080', 0),
('100012125031202005', 0),
('100012125031202075', 0),
('100012160531605001', 0),
('100012125031202105', 0),
('100012125031202045', 0),
('100012160031601014', 0),
('100012160031600008', 0),
('100012125031202050', 2),
('100012160031601013', 0),
('100012160031601012', 0),
('100012125031202245', 0),
('100012150131501002', 0),
('100012150131501050', 0),
('100012125031202010', 0),
('100012160031601019', 0),
('100012125031202230', 2),
('100012125031202235', 0),
('100012160031601017', 0),
('100012125031202210', 0),
('100012160031602014', 0),
('100012160031601011', 0),
('100012126031212050', 0),
('100012125031202000', 2),
('100012125031202090', 2),
('100012150631506000', 0),
('100012150031500060', 0),
('100012125031202290', 0),
('100012120231202003', 1),
('100012125031202185', 0),
('100012125031202155', 0),
('100012120131201003', 0),
('100012130631306000', 0),
('100012120031200096', 0),
('100012120031200021', 0),
('100012120031200014', 0),
('100012150031500016', 0),
('100012120031200111', 0),
('100012120031200022', 0),
('100012130131301012', 0),
('100012120031200005', 0),
('100012125031250005', 1),
('100012131131311000', 0),
('100012120031200041', 1),
('100012120031200019', 0),
('100012120031200042', 1),
('100012120031200052', 0),
('100012160031600004', 0),
('100012120031200109', 0),
('100012120031200006', 0),
('100012150231502030', 0),
('100012150231502010', 0),
('100012120231202004', 1),
('100012120031200112', 0),
('100012125031250013', 0),
('100012130031300038', 0),
('100012125031201085', 0),
('100012120031200025', 0),
('100012120031200201', 0),
('100012120031200095', 0),
('100012130031300041', 0),
('100012160031600016', 0),
('100012120231202006', 0),
('100012120031200037', 0),
('100012120031200106', 0),
('100012120031200202', 1),
('100012120031200105', 0),
('100012125031250012', 0),
('100012125031250008', 0),
('100012125031202160', 2),
('100012125031202150', 2),
('100012123031204150', 0),
('100012123031204065', 0),
('100012123031204265', 0),
('100012123031204050', 0),
('100012123031204275', 0),
('100012120031200099', 0),
('100012123031204139', 0),
('100012170031700067', 0),
('100012121031200520', 0),
('100012120031200115', 0),
('100012150331500080', 0),
('100012150331502050', 0),
('100012150331500081', 0),
('100012123031204355', 0),
('100012161531615000', 0),
('100012125031250000', 2),
('100012123031204170', 0),
('100012120031200110', 0),
('100012125031250006', 0),
('100012123031204350', 0),
('100012150131501000', 0),
('100012120031200203', 1),
('100012120131201004', 0),
('100012121031200524', 0),
('100012121031200526', 0),
('100012150231502000', 0),
('100012130031300040', 0),
('100012121031200525', 0),
('100012123031204360', 0),
('100012120031200044', 0),
('100012150231502038', 0),
('100012120031200046', 0),
('100012120031200002', 0),
('100012120031200510', 0),
('100012120231202001', 1),
('100012120031200049', 0),
('100012131231312000', 0),
('100012125031250007', 0),
('100012125031250010', 0),
('100012125031201086', 0),
('100012120231202002', 1),
('100012120031200083', 0),
('100012120031200080', 0),
('100012125031250009', 0),
('100012120031200082', 0),
('100012120031200300', 0),
('100012125031250011', 0),
('100012120031200117', 0),
('100012150731507001', 0),
('100012120031200077', 0),
('100012123031204280', 0),
('100012123031204070', 0),
('100012123031204060', 0),
('100012123031204014', 0),
('100012123031204270', 0),
('100012123031204075', 0),
('100012123031204090', 0),
('100012123031204177', 0),
('100012123031204335', 0),
('100012123031204400', 0),
('100012123031204290', 0),
('100012123031204012', 0),
('100012150031500050', 0),
('100012160031601000', 0),
('100012121031210001', 0),
('100012121031210000', 0),
('100012121031200523', 0),
('100012121031200527', 0),
('100012120031200009', 0),
('100012120031200011', 0),
('100012160031600009', 0),
('100012130131301004', 0),
('100012130131301052', 0),
('100012180131800030', 0),
('100012150031500022', 0),
('100012130431304001', 0),
('100012130131301030', 0),
('100012130131301006', 0),
('100012130031300020', 0),
('100012121531215001', 3);