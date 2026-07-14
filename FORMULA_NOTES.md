# Formula Notes – Project Pricing_Quotation Tool.xlsm

This file documents Excel formulas from the `.xlsm` that could not be directly translated to C# in `FormulaCalculationService.cs`, along with the rationale for each decision and recent app-side approximations added outside the core formula service.

All formulas below were extracted from `Sample.xlsm` using openpyxl. Row 12 of `2.Quote` is the first data row. Sheet protection password: **"party"**.

---

## 1. Full Column-by-Column Formula Reference – 2.Quote (row 12)

### User-entered input columns

| Col | Header | Source |
|-----|--------|--------|
| B | Ln | Line number (manual) |
| C | Section | User selects from ExtrusionMaster dropdown |
| G | Qty | User-entered quantity |
| H | Length (m) | User-entered length in metres |
| L | Ignore Finish | "Yes"/"No" (user-entered) |
| T | Mill PC Over-ride | "Yes"/"No" (user-entered) |
| V | SS? | Subsill tier (0 = none, 1–4 = tier) |
| Q | Edge Code | User-entered EDGE product code |

---

### Column D – Description

**Excel:**
```
=IFERROR(
  IF(OR(P12="PDB",P12="PLS"),"*","") & VLOOKUP(C12, Table5[], 2, FALSE),
  ""
) & IF(L12="Yes"," **MILL FINISH**",)
```
- Prepends `*` when the section is a thermal-break section (PDB/PLS).
- Appends ` **MILL FINISH**` when `IgnoreFinish = "Yes"`.
- **C# equivalent**: `ApplySectionLookup()` in `QuoteForm.cs` reads `ExtrusionMaster.ThermalBreakType` (from DATA col M, now stored via `ExtrusionMasterUpdateSeed.sql`) to prepend `*` for PDB/PLS sections and appends ` **MILL FINISH**` when `IgnoreFinish = "Yes"`.
- **Status**: ✅ Translated.

---

### Column E – Alloy / Column F – Temper

**Excel:**
```
E12: =IF(C12="","",N12)
F12: =IF(C12="","",O12)
```
Where N12 and O12 come from the DATA sheet via Table5 VLOOKUP (auto-populated when section is entered).
- **C# equivalent**: `ApplySectionLookup()` fills `Alloy` / `Temper` from `ExtrusionMaster.DefaultAlloy` / `DefaultTemper`.
- **Status**: ✅ Translated.

---

### Column I – $ Each

**Excel:**
```
=IFERROR(AH12 + AI12 + AS12 + K12, "")
```
Components: **MF Each** (AH) + **IP Each** (AI) + **SF Sell Each** (AS) + **Slotting/Sundry Charge ea** (K)
- **C# equivalent**: `FormulaCalculationService.CalculatePriceEach()`. `ipEach` is now computed per line using `IpGrade` (auto-populated from `ExtrusionMaster.PhCode` via `IPMasterData`) and the project IP upcharge rates from `EditPricing`. `sfSellEach` remains `0m` (finishing area calculation requires `ApMm`/`PpMm` — not yet fully translated).
- **Status**: ⚠️ Structure translated; IP Each now computed. SF Sell Each (finishing) approximated as 0.

---

### Column J – $ Total

**Excel:**
```
=IFERROR(I12 * G12, "")
```
- **C# equivalent**: `FormulaCalculationService.CalculateSellTotal()`.
- **Status**: ✅ Translated.

---

### Column K – Slotting/Sundry Charge (ea)

**Excel:**
```
=IFERROR(AZ12 + BC12, "")
```
Components: **Slot Sell ea** (AZ) + **Sundry Sell ea** (BC)
- **C# equivalent**: `FormulaCalculationService.CalculateSlottingSundryEach()`.
- **Status**: ✅ Structure translated. Sundry sell (BC) is manual entry only – see Section 3.

---

### Column M – SAP Colour Code

**Excel:**
```
=IF(C12="","",
  IF(AND(BF12>0, NOT('1.Project Details'!$E$33="No")),
    VLOOKUP('1.Project Details'!$E$33, Threshold_Code, 2, FALSE),
    IF(L12="No", SAP_Colour_Code, "NON")
  )
)
```
Where `BF12` = 1 when the section is in the `THRESHOLDS` named range (see Column BF below).
- **C# equivalent**: `FormulaCalculationService.CalculateSapColourCode()`.
- **Status**: ✅ Translated.

---

### Column P – Thermal Break?

**Excel:**
```
=IFERROR(
  IF(LEFT(AX12,1)="7","PDB",
    VLOOKUP(C12, Table5[], 13, FALSE)
  ),
"")
```
- Returns `"PDB"` (pour-debridged), `"PLS"` (polyamide strip), or `"No"`.
- AX12 is the Capral code (from DATA col 8 via AU12), converted via the DATA_EDGE lookup.
- **C# equivalent**: `ExtrusionMaster.ThermalBreakType` now stores the thermal-break type from DATA col M (seeded via `ExtrusionMasterUpdateSeed.sql`). Used in `ApplySectionLookup()` to apply the `*` prefix to Description.
- **Status**: ✅ ThermalBreakType seeded in ExtrusionMaster and used for Description prefix.

---

### Column V – SS? (Subsill Tier)

**Excel:**
```
=IFERROR(VLOOKUP(C12, Table5[], 11, FALSE), "")
```
- Looks up subsill tier from `Table5` column 11 (DATA sheet column K = `subsill`).
- **C# equivalent**: `QuoteLine.SubsillFlag` / `SubsillSurcharge`. `SubsillFlag` is now auto-set to `true` when `ExtrusionMaster.SubsillTier > 0` (from DATA col K, stored via `ExtrusionMasterUpdateSeed.sql`). `SubsillSurcharge` (col K total) remains user-entered.
- **Status**: ✅ SubsillFlag auto-populated. Tier lookup seeded.

---

### Columns Y / Z / AA – kg/m, kg each, total kg

**Excel:**
```
Y12:  =IFERROR(VLOOKUP(C12, Table5[], 4, FALSE), "")   -- kg/m from DATA col D
Z12:  =IFERROR(Y12 * H12, "")                           -- kg each
AA12: =IFERROR(Z12 * G12, "")                           -- total kg
```
- **C# equivalent**: `FormulaCalculationService.CalculateKgEach()` / `CalculateTotalKg()`. `MassPerM` stored in `ExtrusionMaster`.
- **Status**: ✅ Translated.

---

### Columns AB / AC / AD – AP/m, AP each, AP total

**Excel:**
```
AB12: =IFERROR(VLOOKUP(C12, Table5[], 5, FALSE), "")   -- AP rate per metre (DATA col E, in mm units)
AC12: =IFERROR(AB12 * H12 / 1000, "")                  -- AP each
AD12: =IFERROR(AC12 * G12, "")                          -- AP total
```
- `AP/m` = Anodised Perimeter per metre in mm (DATA sheet column E).
- **Status**: ⚠️ `ExtrusionMaster` does not yet store `AP` or `PP` columns. These are needed for finishing cost/sell area calculations (columns AO–AT). **Action required**: add `ApMm` and `PpMm` columns to `ExtrusionMaster`.

---

### Columns AE / AF / AG – PP/m, PP each, PP total

**Excel:**
```
AE12: =IFERROR(VLOOKUP(C12, Table5[], 6, FALSE), "")   -- PP rate per metre (DATA col F, in mm units)
AF12: =IFERROR(AE12 * H12 / 1000, "")                  -- PP each
AG12: =IFERROR(AF12 * G12, "")                          -- PP total
```
- **Status**: ⚠️ Same gap as AB–AD. `ExtrusionMaster` does not yet store `PP`.

---

### Column AH – MF Each (Base Metal Cost per Length)

**Excel (array formula, Ctrl+Shift+Enter):**
```
=IFERROR(
  IF(AV12=3,
    IF(Y12<0.5, 1*Z12*2.5, 0),          -- IP3 panoramic: rolling factor for light sections
    IF(P12="PLS",
      VLOOKUP(C12,Table_TB_Cost[],11,FALSE) /
        VLOOKUP(C12,Table_TB_Cost[],3,FALSE) * H12,   -- PLS sell price per metre
      IF(P12="PDB",
        IFS(
          tb_calc_type="Margin",
            ((Z12 * 'Pricing Matrix'!$F$4) + (H12 * 'Pricing Matrix'!$F$5)) / (1-tb_calc_value),
          tb_calc_type="List less Discount",
            ((Z12*VLOOKUP(LEFT(VLOOKUP(C12,Table5[],10,FALSE),2),TB_MPG,2,FALSE)) +
              (H12 * 'Pricing Matrix'!$F$5 * 3)) * (1-tb_calc_value)
        ),
        Z12 * '3.Edit Pricing'!$B$33       -- standard mill finish: kg each x MetalSellPrice
      )
    )
  ),
"")
```
Named ranges / cell references:
- `tb_calc_type` = `3.Edit Pricing!B36` (`"Margin"` or `"List less Discount"`)
- `tb_calc_value` = `3.Edit Pricing!B37` (margin % or list-less-discount %)
- `Pricing Matrix!$F$4` = PDB metal cost base = **9.08 $/kg** (as of June 2026)
- `Pricing Matrix!$F$5` = ALF cost per metre = **6.46 $/m** (updated 04.02.2026)
- `Table_TB_Cost` = thermal-break section cost table (Pricing Matrix cols S–AB)
- `TB_MPG` = material price group rate lookup
- **C# equivalent**: `QuoteForm.CalculateMillFinishEach()` handles the standard path (`Z12 × B33`). PLS/PDB thermal-break and IP3 rolling paths are not yet computed per line.
- **Status**: ⚠️ Standard mill-finish path translated. Thermal-break (PLS/PDB) and IP3 rolling paths not fully translated.

---

### Column AI – IP Each

**Excel:**
```
=IF(AV12=1, Z12 * '3.Edit Pricing'!$G$33,
  IF(AV12=2, Z12 * '3.Edit Pricing'!$H$33,
    IF(AV12=3, Z12 * '3.Edit Pricing'!$I$33, 0)
  )
)
```
- `AV12` = IP category (0 = none, 1 = IP1, 2 = IP2, 3 = IP3 panoramic).
- `G33`, `H33`, `I33` = IP upcharge rates on `3.Edit Pricing` ($/kg).
- `G33` = IP1 upcharge (user-entered; default **1.00 $/kg**).
- `H33` = IP2 upcharge (user-entered; default **2.00 $/kg**).
- `I33` = IP3 rate derived via `=VLOOKUP("U3",Table_Costs[#All],2,FALSE)` from DATA sheet (workbook-evaluated value: **≈25.85 $/kg**).
- **Related app update**: `EditPricing` stores `IP1UpchargePerKg`, `IP2UpchargePerKg`, `IP3PanoramicFixedKgRate`. `ExtrusionMaster` now stores `PhCode` (DATA col H). `IPMasterData` table seeded from `5.Edit_PH_IP` maps PhCode → IpCategory. `QuoteLine.IpGrade` is auto-computed in `ApplySectionLookup()` and used in `CalculateIpEach()` within `QuoteForm.cs`.
- **Status**: ✅ Translated. IpGrade auto-populated per line; IP Each included in $ Each.

---

### Column AJ – MF Total

**Excel:**
```
=IFERROR((AH12 * G12) + (AI12 * G12), "")
```
- **C# equivalent**: `FormulaCalculationService.CalculateMfTotal()`.
- **Status**: ✅ Translated.

---

### Column AK – Extra/Sundry Total

**Excel:**
```
=IFERROR(K12 * G12, "")
```
- **C# equivalent**: `FormulaCalculationService.CalculateExtraSundryTotal()`.
- **Status**: ✅ Translated.

---

### Column AL – Thermal Break Total (no fin)

**Excel:**
```
=IFERROR(IF(OR(P12="PLS",P12="PDB"), AJ12, ""), "")
```
- Populated only for thermal-break sections.
- **Status**: ⚠️ Not translated per line.

---

### Column AM – TB Margin $

**Excel (array formula):**
```
=IFERROR(
  IFS(
    P12="PLS", AL12 - (VLOOKUP(C12,Table_TB_Cost[],10,FALSE) / VLOOKUP(C12,Table_TB_Cost[],3,FALSE) * G12 * H12),
    P12="PDB", AL12 - ((Z12 * 'Pricing Matrix'!$F$4) + (H12 * 'Pricing Matrix'!$F$5)) * G12
  ),
"")
```
- **Status**: ⚠️ Not translated per line.

---

### Column AN – MF Margin $

**Excel:**
```
=IFERROR(
  IF(OR(P12="PLS",P12="PDB"), 0,
    IF(AV12=3,
      (AI12*G12) - (AA12 * 'Pricing Matrix'!$B$5),
      (AI12*G12) + (AA12 * ('3.Edit Pricing'!$B$33 - 'Pricing Matrix'!$B$5))
    )
  ),
"")
```
- **Status**: ⚠️ Not translated per line. `Pricing Matrix!$B$5` = metal cost total = **8.73 $/kg** (LME + billet + spread).

---

### Column AO – SF Cost $/m²

**Excel:**
```
=IF(Y12="","",
  IF(AND(NOT('1.Project Details'!$E$33="No"), BF12>0),
    '3.Edit Pricing'!$B$57,                          -- threshold cost (anodised)
    IF('1.Project Details'!$B$24="Mill Finish", 0,
      IF('2.Quote'!$L12="Yes", 0,                    -- Ignore Finish
        IF(U12="Bulk",        FinBulk_Cost_Price,
          IF(OR(U12="Non-bulk",U12="Minfin"), FinNonBulk_Cost_Price,
            Fin_Cost_Price
          )
        )
      )
    )
  )
)
```
Named ranges:
- `FinBulk_Cost_Price` = `3.Edit Pricing!B49` (auto from PricingMatrix)
- `FinNonBulk_Cost_Price` = `3.Edit Pricing!B53` (auto from PricingMatrix)
- `Fin_Cost_Price` = `3.Edit Pricing!B43` (auto from PricingMatrix E44 = **10.50 $/m²**)
- **Status**: ⚠️ Not translated per line. The app uses `EditPricing.FinishingCostPricePerM2` as a project-level value.

---

### Column AP – SF Sell $/m²

**Excel (array formula):**
```
=IF(Y12="","",
  IFS(
    AND(BF12>0, NOT('1.Project Details'!$E$33="No")), '3.Edit Pricing'!$B$58,  -- threshold sell
    '1.Project Details'!$B$24="Mill Finish", 0,
    '1.Project Details'!$B$24="",            0,
    '2.Quote'!$L12="Yes",                    0,
    U12="Bulk",         FinBulk_Sell_Price,
    U12="Non-bulk",     FinNonBulk_Sell_Price,
    U12="Minfin",       FinNonBulk_Sell_Price,
    Y12<>"",            Fin_Sell_Price
  )
)
```
Named ranges:
- `Fin_Sell_Price` = `3.Edit Pricing!B44` (user-entered finishing sell $/m²)
- `FinBulk_Sell_Price` = `3.Edit Pricing!B50`
- `FinNonBulk_Sell_Price` = `3.Edit Pricing!B54`
- **Status**: ⚠️ Not translated per line.

---

### Column AQ – Costed/Priced As

**Excel:**
```
=IFERROR(
  IF(Y12="","",
    IF(T12="Yes", "Anodised Perimeter (AP)",
      '1.Project Details'!$B$33
    )
  ),
"")
```
- Returns the applicator name (e.g., `"Anodised Perimeter (AP)"`) or the project finish applicator.
- **Status**: ⚠️ Not stored per line. Used to select AP vs PP calculation path for finishing.

---

### Columns AR / AS / AT – SF Cost Total, SF Sell Each, SF Sell Total

**Excel:**
```
AR12: =IFERROR(
  IF($AQ12="Anodised Perimeter (AP)",
    IF(U12="Minfin", 40, AD12) * AO12,
    AG12 * AO12
  ), "")

AS12: =IFERROR(
  IF(AQ12="Anodised Perimeter (AP)",
    AC12 * AP12 + IF(U12="Minfin", (40-AD12)*AP12/G12, 0),
    AF12 * AP12
  ), "")

AT12: =IFERROR(AS12 * G12, "")
```
- SF Sell Each (AS12) feeds directly into **Column I ($ Each)** as the `sfSellEach` component.
- **Status**: ⚠️ Not translated per line. Requires AP/PP per-section data and per-line AQ classification.

---

### Column AU – PH (Product Hierarchy code)

**Excel:**
```
=IFERROR(VLOOKUP(C12, DATA!$A:$K, 8, FALSE), "")
```
- DATA col 8 = PH code (18-digit SAP product hierarchy string).
- **C# equivalent**: `ExtrusionMaster.PhCode` now stores the PH code from DATA col H (seeded via `ExtrusionMasterUpdateSeed.sql`). Used in `ApplySectionLookup()` to look up `IpGrade` from the `IPMasterData` table.
- **Status**: ✅ PhCode stored in ExtrusionMaster; IPMasterData table seeded from 5.Edit_PH_IP.

---

### Column AV – IP (Ingress Protection category)

**Excel:**
```
=IFERROR(VLOOKUP(AU12, '5.Edit_PH_IP'!$B:$D, 3, FALSE), "")
```
- Looks up the PH code in the `5.Edit_PH_IP` sheet, column D = IP category (0, 1, 2, or 3).
- **Status**: ⚠️ Not translated per line. The `5.Edit_PH_IP` sheet data (PH code → IP category mapping) is not yet in the database. **Action required**: create an `IPMasterData` table seeded from `5.Edit_PH_IP`.

---

### Columns AY / AZ – Slot Cost Total (est), Slot Sell ea

**Excel:**
```
AY12: =IFERROR(IF(V12>0, VLOOKUP(V12,SubSill_Tier_Price,3,FALSE)*H12*G12, 0), "")
AZ12: =IFERROR(IF(V12>0, VLOOKUP(V12,SubSill_Tier_Price,2,FALSE)*H12, 0),    "")
```
- `SubSill_Tier_Price` col 2 = `SellPricePerMetre`, col 3 = `CostPerMetre`.
- Tier 4 (Panoramic door): `SellPricePerMetre + SellPricePerMetre * 1.5` market factor applied.
- **C# equivalent**: `FormulaCalculationService.CalculateSlotCostTotal()` / `CalculateSlotSellEach()`.
- **Status**: ✅ Translated.

---

### Columns BA / BB / BC / BD – Sundry Cost and Sell

**Excel:**
```
BA12: =IFERROR(VLOOKUP(C12, Sundry_Data, 4, FALSE), 0)   -- Sundry Cost each
BB12: =BA12 * G12                                          -- Sundry Cost total
BC12: =IFERROR(VLOOKUP(C12, Sundry_Data, 5, FALSE), 0)   -- Sundry Sell each
BD12: =BC12 * G12                                          -- Sundry Sell total
```
- `Sundry_Data` = `DATA!N8:R58` (named range).  Col 4 = sundry unit cost, col 5 = sundry sell.
- **Status**: ⚠️ Workbook formula not fully translated. The app supports **manual sundry entry** via `SundryItemsForm` / `SundryItems` table, but does not perform automatic VLOOKUP against a complete extracted `Sundry_Data` table.

---

### Column BF – Threshold Flag

**Excel:**
```
=IF(ISNA(VLOOKUP(C12, THRESHOLDS, 1, FALSE)), 0, 1)
```
- Returns 1 if the section code exists in the `THRESHOLDS` named range; otherwise 0.
- `THRESHOLDS` = a named range on the `DATA` sheet listing all section codes that are anodised threshold sections.
- **Status**: ❌ Not translated. The `THRESHOLDS` list is not currently extracted or stored in the database. **Action required**: create a `ThresholdSections` table seeded with the THRESHOLDS named range contents from `DATA`.

---

## 2. Circular / Cross-Sheet References

### Pricing Matrix B5 – Metal Cost Total

**Named range**: `MetalCost` = `Pricing Matrix!B5`

Actual formula:
```
B3: =Table_Costs[[#This Row],[Value]]   -- LME + billet premium (e.g. 5.61 $/kg as of June 2026)
B4: =Table_Costs[[#This Row],[Value]]   -- Internal Spread (e.g. 3.12 $/kg as of June 2026)
B5: =B3+B4                              -- Total cost = 8.73 $/kg as of June 2026
```
- `Table_Costs` values come from the `DATA` sheet and are updated monthly.
- **C# equivalent**: `FormulaCalculationService.CalculateMetalCostPerKg()`. Actual monthly values are stored in `RevisionControl` (`LMERate`, `BilletPremium`) plus `EditPricing.MetalPrice`.
- **Status**: ✅ Represented. Monthly update workflow uses `MonthlyUpdateService`.

---

### Pricing Matrix K5 – AP to PP Factor

**Named range**: `AP_PP_factor` = `Pricing Matrix!K5` = **1.9**

Converts aluminium price (AP basis) to production price (PP basis).

**C# equivalent**:
```csharp
public const decimal AP_PP_Factor = 1.9m;
```
- **Status**: ✅ Represented as a constant.

---

### Pricing Matrix F4 / F5 – PDB Metal Base / ALF Cost

| Cell | Value (June 2026) | Description |
|------|-------------------|-------------|
| `F4` | **9.08 $/kg** | PDB metal costing base = B5 + F3 (extra spread 0.35) |
| `F5` | **6.46 $/m** | ALF cost per metre (updated 04.02.2026) |

These are used directly in the AH12 thermal-break (PDB) formula.
- **Status**: ⚠️ Not stored as database constants. Would need to be added to a `PricingConstants` table or as monthly-update fields.

---

### 3.Edit Pricing – B57 (ThresholdCostPrice, read-only)

**Excel:**
```
=IFERROR('Pricing Matrix'!$E$49, 0)
```
- `Pricing Matrix!E49` = anodised threshold cost $/m² = **10.65 $/m²** (Capral/AAF QLD anodising, 20um Clear).
- **Previous app state**: read-only stub.
- **Recent app update**: `EditPricing.AnodisedThresholdCostPerM2` stores/uses this value (default 10.65).
- **Status**: ⚠️ Stored and used manually, not yet derived from `PowderCostTable` query in code.

---

### 3.Edit Pricing – B43 (FinishingCostPrice, read-only)

**Excel:**
```
=IFERROR('Pricing Matrix'!$E$44, 0)
```
- `Pricing Matrix!E44` = finishing cost $/m² for the selected applicator/group = **10.50 $/m²** (Capral P&P Campbellfield, Group POA).
- **Status**: ⚠️ Stored in `EditPricing.FinishingCostPricePerM2`. The automatic PricingMatrix lookup for the selected applicator/group is not yet implemented.

---

## 3. Summary / Quote_Data Style Calculations

The workbook's summary area on `3.Edit Pricing` and `Quote_Data` sheet aggregates line-level, finishing, and sundry pricing.

### App-side summary implementation

Recent updates introduced `QuoteSummaryService.cs` and a persisted `QuoteSummary` table.

Current summary logic includes:

- `MillFinishTotal = SUM(QuoteLines.TotalPrice)`
- `MillFinishCost = MillFinishTotal * 0.87`
- `MillFinishMarginDollar = MillFinishTotal - MillFinishCost`
- `MillFinishMarginPct = margin / total`
- `FinishingTotal = EditPricing.FinishingPrice * EditPricing.TotalFinishingM2`
- `FinishingCost = EditPricing.FinishingCostPricePerM2 * EditPricing.TotalFinishingM2`
- `SlottingSundryTotal = SUM(SundryItems.TotalPrice)`
- `GrandTotal = MillFinishTotal + FinishingTotal + SundryTotal`

### Notes on fidelity

- The **0.87 mill-finish cost ratio** is an app approximation retained to mimic workbook behaviour.
- Several `QuoteSummary` fields exist in the schema for thermal-break and slotting/sundry margins, but not all are fully populated.
- **Status**: ⚠️ Summary logic exists and is persisted, but it is an application approximation rather than a one-to-one extraction of every `Quote_Data` workbook formula.

---

## 4. Quote Letter and No-Pricing Output Sheets

The workbook contains customer-facing output sheets:

- `4.Quote Letter`
- `4b. Quote no pricing`
- `4c.Quote MF ALT`

### App-side implementation status

Recent updates added:

- `QuoteLetterForm`
- `QuoteNoPricingForm`

These forms use persisted project and quote data to generate:

- a printable/previewable quote letter
- native PDF export via QuestPDF for the quote letter and no-pricing schedule
- a no-pricing schedule showing lines without dollar values

### Notes on fidelity

- These forms are **app-level reconstructions**, not direct formula translations from workbook cells.
- They rely on persisted project data such as:
  - `AttentionTo`
  - `QuoteValidity`
  - `DeliveryCharge`
  - `FinishName` / `FinishType`
  - `QuotedBy` / `AccountManager`
- `4c.Quote MF ALT` is still not explicitly implemented as a dedicated separate form/output workflow.
- **Status**: ⚠️ Functionally represented in the app, but not a literal sheet/cell formula translation.

---

### CRE8 project-details import

`ProjectDetailsForm` now calls `Cre8ImportService.cs` to import project metadata from a CRE8 CSV into the currently open project record.

- **Important caveat**: no CRE8 sample/specification exists in this repository, so the service uses inferred header aliases and fallback column positions based on the Project Details fields.
- **Status**: ⚠️ Implemented as a best-effort CSV mapping and may need adjustment once a real CRE8 export sample is available.

---

## 5. Formulas Involving the DATA Sheet

The `DATA` and `DATA_EDGE` sheets contain extensive reference tables, including:

- product cost tables (`Table_Costs` and `Table5`)
- sundry items (`Sundry_Data` = `DATA!N8:R58`)
- alloy/temper tables
- threshold/anodising inputs
- IP grade source values
- AP and PP per-metre values per section (DATA cols E, F)
- subsill tier per section (DATA col K)
- thermal-break type per section (DATA col M)
- PH product hierarchy code per section (DATA col H)

### Extraction limitation

The workbook uses XLOOKUP, LAMBDA, and dynamic array formulas that `openpyxl` cannot evaluate.

As a result:
- formula cells may read as `None`
- cached values are only available if Excel last saved the workbook with calculated results

### Impact on app translation

This limitation affects:

- automatic sundry VLOOKUP translation (BA/BC columns)
- AP/PP column values per section
- some finish/anodising derived values
- any logic depending on workbook-evaluated dynamic formulas

### Action required for exact fidelity

If precise DATA-sheet values are needed:

1. Open the `.xlsm` in Excel
2. Recalculate/save the workbook
3. Re-run extraction using cached `data_only` values
4. Or manually seed the missing values into SQL reference tables

---

## 6. Seeded Reference Data vs Actual Workbook Values

> **Important**: The values in `SeedData.sql` were estimated during initial development. The actual values extracted from `Sample.xlsm` (June 2026 rates) differ from the seeded data. The tables below show the actual workbook values.

### MetalBreakpoints (Pricing Matrix rows 8–13)

Actual workbook values (as of June 2026, `B5` cost = **8.73 $/kg**):

| BreakpointKg | Spread | Typical ($/kg) | Min ($/kg) | Max ($/kg) | MinFactor | MaxFactor |
|---|---|---|---|---|---|---|
| 1 | 3.50 | 12.23 | 11.88 | 12.73 | 0.35 | 0.50 |
| 2,000 | 1.42 | 10.15 | 9.80 | 10.65 | 0.35 | 0.50 |
| 3,000 | 1.37 | 10.10 | 9.75 | 10.60 | 0.35 | 0.50 |
| 5,000 | 1.32 | 10.05 | 9.70 | 10.55 | 0.35 | 0.50 |
| 7,000 | 1.12 | 9.85 | 9.70 | 10.35 | 0.15 | 0.50 |
| 10,000 | 1.02 | 9.75 | 9.60 | 10.25 | 0.15 | 0.50 |

> Note: BreakpointKg is the **minimum total project kg** for that rate. Workbook uses only 6 tiers (not 7). The `SeedData.sql` values are placeholder estimates and should be replaced.

---

### ThermalBreakBreakpoints (Pricing Matrix rows 16–21)

Actual workbook values:

| BreakpointKg | MarginTyp | MarginMin | MarginMax | ListTyp | ListMin | ListMax |
|---|---|---|---|---|---|---|
| 1,000 | 0.40 | 0.38 | 0.43 | 0.47 | 0.44 | 0.50 |
| 2,000 | 0.35 | 0.33 | 0.38 | 0.52 | 0.48 | 0.54 |
| 3,000 | 0.32 | 0.30 | 0.35 | 0.54 | 0.50 | 0.56 |
| 5,000 | 0.29 | 0.27 | 0.32 | 0.56 | 0.52 | 0.58 |
| 7,000 | 0.27 | 0.25 | 0.30 | 0.58 | 0.53 | 0.59 |
| 10,000 | 0.25 | 0.23 | 0.28 | 0.59 | 0.54 | 0.60 |

> Note: These are **margin %** values (e.g. 0.40 = 40%) used in the `IFS(tb_calc_type="Margin", ...)` path. The `SeedData.sql` values (0.08–0.20 range) are incorrect estimates.

---

### FinishBreakpoints (Pricing Matrix rows 54–60)

Actual workbook values (`BreakpointAP` in m²):

| BreakpointAP | BreakpointPP | RangeA Typ | RangeA Min | RangeA Max | RangeB Typ | RangeB Min | RangeB Max | RangeC Typ | RangeC Min | RangeC Max | RangeD Typ | RangeD Min | RangeD Max |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 0 | 0 | 0.40 | 0.35 | 0.45 | 0.50 | 0.45 | 0.55 | 0.40 | 0.35 | 0.45 | 0.50 | 0.45 | 0.55 |
| 100 | 60 | 0.25 | 0.23 | 0.30 | 0.27 | 0.25 | 0.32 | 0.25 | 0.23 | 0.30 | 0.30 | 0.28 | 0.35 |
| 500 | 270 | 0.22 | 0.20 | 0.27 | 0.24 | 0.22 | 0.29 | 0.22 | 0.20 | 0.27 | 0.25 | 0.23 | 0.30 |
| 700 | 370 | 0.20 | 0.17 | 0.25 | 0.22 | 0.19 | 0.27 | 0.20 | 0.17 | 0.25 | 0.22 | 0.20 | 0.27 |
| 1,000 | 530 | 0.17 | 0.15 | 0.20 | 0.20 | 0.17 | 0.23 | 0.17 | 0.15 | 0.20 | 0.20 | 0.18 | 0.25 |
| 2,000 | 1,060 | 0.15 | 0.135 | 0.18 | 0.18 | 0.155 | 0.20 | 0.15 | 0.135 | 0.18 | 0.17 | 0.15 | 0.22 |
| 4,000 | 2,110 | 0.13 | 0.12 | 0.15 | 0.16 | 0.14 | 0.18 | 0.13 | 0.12 | 0.15 | 0.15 | 0.13 | 0.18 |

> Note: These are **margin %** values applied on top of the base finish cost. The `SeedData.sql` values near 1.0 are incorrect (those were price multiplier estimates, not margin %).

---

### SubsillTierPrices (Pricing Matrix rows 65–68)

Actual workbook values:

| Tier | SellPricePerMetre ($/m) | CostPerMetre ($/m) | Notes |
|---|---|---|---|
| 1 | 2.00 | 1.25 | Standard subsill |
| 2 | 3.00 | 2.25 | — |
| 3 | 5.00 | 4.50 | — |
| 4 | 16.15 | 4.52 | Panoramic door – 150% market factor applied to sell price |

> Note: The `SeedData.sql` tier prices (e.g. $22.50/m sell) are incorrect placeholder values that should be replaced with the above actual values.

---

### PowderCostTable (Pricing Matrix rows 28–40, actual costs $/m²)

Actual workbook values:

| Vendor | AP/PP | Group 1 | Group 2 | Group 3 | Group 4 | Group 5 | 20um Clear | 25um Clear | Group POA |
|---|---|---|---|---|---|---|---|---|---|
| Capral P&P – Bremer | AP | 6.22 | 6.89 | 7.44 | 11.15 | 12.64 | — | — | 10.50 |
| Capral P&P – Campbellfield | AP | 6.45 | 6.89 | 9.42 | 11.45 | 12.64 | — | — | 10.50 |
| Capral P&P – Canningvale | PP | 6.89 | 9.45 | 10.95 | 16.43 | 18.62 | — | — | 10.50 |
| AAF powdercoat | AP | 5.06 | 6.20 | 7.27 | — | — | — | — | 10.50 |
| Powderline WA | PP | 6.75 | 10.15 | 11.00 | — | — | — | — | 10.50 |
| Other – pls check pricing | AP | 10.50 | 10.50 | 10.50 | 10.50 | 10.50 | 10.50 | 10.50 | 10.50 |
| Capral/AAF QLD anodising | AP | — | — | — | — | — | 10.65 | 13.02 | 10.50 |
| WA Anodisers | AP | — | — | — | — | — | 10.69 | 12.98 | 10.50 |
| Capral Mill – non-Bulk | AP | 6.22 | 6.89 | 7.44 | 11.15 | 12.64 | — | — | 99 |
| Capral Mill | AP | 3.81 | 4.47 | 5.34 | 6.02 | 7.67 | — | — | 99 |
| Capral P&P – HWood Non-Bulk | AP | 7.60 | 9.36 | 11.00 | 17.63 | 18.75 | — | — | 10.50 |
| Capral P&P – HWood Bulk | AP | 5.44 | 6.72 | 7.91 | 12.71 | 13.83 | — | — | 10.50 |

> Note: The `SeedData.sql` powder cost values (e.g. Group 1 AP = 28.50 $/m²) are order-of-magnitude incorrect. The workbook values are ~5–13 $/m² for internal cost. The sell prices are different and computed via the finishing margin breakpoints.

---

## 7. VeryHidden Sheets

The following workbook sheets are marked `xlSheetVeryHidden` in VBA:

| Sheet | Purpose |
|---|---|
| `Colour_Groups` | Colour code → group → SAP code lookup |
| `DATA_EDGE` | EDGE product pricing reference data |

Both remain relevant to workbook logic.

- `Colour_Groups` is seeded to the app and supports SAP colour code behaviour.
- `DATA_EDGE` remains only partially represented.

---

## 8. Missing Tables / Fields Required for Full Calculation Fidelity

The following data items are required for the `2.Quote` calculations but are not yet in the database:

| Item | Required For | Suggested Table / Column |
|---|---|---|
| `THRESHOLDS` named range (list of threshold section codes) | Column BF formula | New `ThresholdSections` table |
| PH code per section (DATA col H) | Column AU formula | `ExtrusionMaster.PhCode` |
| IP category per PH code (`5.Edit_PH_IP` col D) | Column AV formula | New `IPMasterData` table |
| AP per metre per section (DATA col E, mm units) | Columns AB–AD, AO, AR, AS | `ExtrusionMaster.ApMm` |
| PP per metre per section (DATA col F, mm units) | Columns AE–AG, AO, AR, AS | `ExtrusionMaster.PpMm` |
| Subsill tier per section (DATA col K) | Column V auto-fill | `ExtrusionMaster.SubsillTier` |
| Thermal-break type per section (DATA col M: No/PLS/PDB) | Column P formula | `ExtrusionMaster.ThermalBreakType` |
| IpGrade per quote line | Column AI formula | `QuoteLines.IpGrade` |
| Correct breakpoint seed values (all lookup tables) | All VLOOKUP-based pricing | Replace placeholder data in `SeedData.sql` with actual workbook values documented in Section 6 |
| PDB metal cost base (`Pricing Matrix F4`) | Column AH PDB path | New `PricingConstants` table or `EditPricing` field |
| ALF cost per metre (`Pricing Matrix F5`) | Column AH PDB path | Same as above |

---

## 9. Items That Could Not Be Fully Extracted or Translated

| Item | Reason / Current state |
|---|---|
| VBA macro code | `openpyxl` cannot read VBA modules from `.xlsm` |
| Pivot cache data | Pivot tables not supported by `openpyxl` |
| DATA sheet formula values | XLOOKUP/LAMBDA not evaluated without Excel engine |
| DATA_EDGE full structure | Partial only |
| Embedded image/logo assets | Binary workbook assets not extracted |
| Conditional formatting rules | Not needed for current C# app logic |
| Data validation extension metadata | openpyxl warning/removal on read |
| Print area pixel/page layout fidelity | Not fully workbook-native in WinForms |
| Full automatic sundry workbook lookup | Replaced with manual `SundryItems` workflow |
| Full per-line IP grade translation | Rates supported, line-level grade persistence and AV lookup still missing |
| Full threshold cost derivation from DATA | Stored/used manually, still not fully workbook-driven |
| Dedicated MF ALT customer output | Not yet implemented as separate end-to-end flow |
| Exact CRE8 export schema fidelity | Import exists via best-effort `Cre8ImportService`, but no source CRE8 sample/spec is in the repo |
| Correct seeded pricing reference data | Placeholder values in `SeedData.sql` differ from actual workbook values – see Section 6 |

---

## 10. Sheet Protection Notes

- Password for most protected sheets: **"party"**
- `Revision Control` sheet is unprotected
- In the C# app, the same intent is enforced through UI read-only fields and database-backed state rather than worksheet protection

---

## 11. Practical Translation Summary

### Fully translated or strongly represented

- Metal cost total formula (B5 = LME + Billet + Spread)
- Standard mill-finish MF Each calculation (Z12 × B33)
- Subsill cost/sell logic (AY/AZ)
- SAP colour code logic (M)
- Quote line kg/total kg calculations (Y/Z/AA/J)
- Quote line total price persistence
- Project/reference data persistence
- Summary persistence and calculated margins

### Partially translated / approximated

- Finishing cost/sell calculations (AO–AT) – project-level only, not per-line
- Threshold/anodised cost derivation – manually entered, not auto-derived
- Quote summary formulas from workbook output sheets
- Quote letter/no-pricing sheet reconstruction
- IP upcharge logic – rates stored, but per-line grade not persisted
- Description prefix/suffix for thermal-break and ignore-finish lines

### Not yet fully translated

- Automatic `Sundry_Data` VLOOKUP behaviour (BA/BC columns)
- AP/PP per-metre section data (→ AB–AG and AO–AT columns)
- Per-line IP grade formula evaluation (→ AV column)
- THRESHOLDS named range lookup (→ BF column)
- Thermal-break type per section (→ P column)
- PDB/PLS-path AH formula
- Full `DATA_EDGE`-driven behaviour
- Dedicated MF ALT output workflow
- Correct seeded values for all pricing reference tables (Section 6)
