# Formula Notes – Project Pricing_Quotation Tool.xlsm

This file documents Excel formulas from the `.xlsm` that could not be directly translated to C# in `FormulaCalculationService.cs`, along with the rationale for each decision and recent app-side approximations added outside the core formula service.

---

## 1. Complex / Array Formulas in 2.Quote

### Column AH – Base Metal Cost per Length

**Excel (approximate):**
```
=IF(G12>0,
   INDEX(MetalRate_Table, MATCH(1,(D12=MetalRate_ProductType)*(G12<=MetalRate_Breakpoint),0)),
   0)
```
- This is a multi-criteria array formula (Ctrl+Shift+Enter in older Excel).
- `MetalRate_Table` is a named range on the `Pricing Matrix` sheet.
- **C# equivalent**: `LookupMetalTypicalPrice(decimal kgPerM)` in `FormulaCalculationService.cs`.
- **Status**: ✅ Translated, but simplified to a breakpoint lookup driven by seeded `MetalBreakpoints` data rather than a literal worksheet array formula.

---

### Column AI – Finishing Cost per Length

**Excel (approximate):**
```
=IF(OR(L12="Yes", CostType=0),
   0,
   IF(CostType=1,
      B42 * SurfaceArea,
      IF(CostType=2, B44 * SurfaceArea, 0)
   )
)
```
- `CostType` references cell `C25` (`3.Edit Pricing`).
- `B42`, `B44` are the POA Colour cost and Finishing Sell Price from Edit Pricing.
- Surface area depends on row geometry/perimeter data that is not fully represented per quote line in the current schema.
- **C# equivalent**: `CalculateFinishingCostPerLine(...)` in `FormulaCalculationService.cs`.
- **Related app update**: `EditPricingForm` now stores and displays `FinishingCostPricePerM2`, `TotalFinishingM2`, and summary margins.
- **Status**: ⚠️ Partially translated. The workbook’s geometry-driven surface-area approach is still approximated in the app.

---

### Column AS – IP (Ingress Protection) Upcharge

**Excel (approximate):**
```
=IF(IPGrade="IP1", G33*H12*G12,
   IF(IPGrade="IP2", H33*H12*G12,
      IF(IPGrade="IP3", I33*H12*G12, 0)
   )
)
```
- `G33`, `H33`, `I33` are IP rate cells on `3.Edit Pricing`.
- Source workbook rates:
  - IP1 = 1.00 $/kg
  - IP2 = 2.00 $/kg
  - IP3 = workbook-derived fixed panoramic rate ≈ 25.85 $/kg
- `IPGrade` is still not stored as a native per-line field in `QuoteLines`.
- **Related app update**: `EditPricing` now stores:
  - `IP1UpchargePerKg`
  - `IP2UpchargePerKg`
  - `IP3PanoramicFixedKgRate`
  - `IP1MarginPct`
  - `IP2MarginPct`
  - `IP3MarginPct`
- **UI support**: `EditPricingForm` exposes editable IP defaults, margin display, and reset-to-default actions.
- **Status**: ⚠️ Not fully translated per quote line. Rates and margins are now supported at the pricing-summary level, but line-level `IpGrade` persistence is still missing.

---

### Column AY – Subsill Cost

**Excel:**
```
=IF(V12=1, SubsillTierCost * H12 * G12, 0)
```
- `V12` = SubsillFlag (1 = yes, 0 = no).
- `SubsillTierCost` = tier lookup from `Pricing Matrix` rows 63–68.
- Tier is determined by extrusion type and is not fully inferred from workbook semantics in every case.
- **C# equivalent**: `CalculateSubsillCost(bool isSubsill, int tier, decimal lengthM, int qty)`.
- **Status**: ✅ Translated.

---

### Column AZ – Subsill Sell Price

**Excel:**
```
=IF(V12=1,
   IF(SubsillTier=4, (SubsillTierSell + SubsillTierSell*1.5) * H12,
      SubsillTierSell * H12),
   0)
```
- Tier 4 (Panoramic door) receives a 150% market factor on sell price.
- **C# equivalent**: `CalculateSubsillSellEach(bool isSubsill, int tier, decimal lengthM)`.
- **Status**: ✅ Translated.

---

### Column BA – Sundry Cost (each)

**Excel (named range `Sundry_Data` → DATA!N8:R58):**
```
=IFERROR(VLOOKUP(C12, Sundry_Data, 4, FALSE), 0)
```
- Looks up the Section Code in the DATA sheet sundry table.
- Column 4 of `Sundry_Data` = sundry unit cost.
- **Original limitation**: DATA-sheet extraction was incomplete.
- **Recent app update**:
  - New `SundryItems` SQL table
  - New `SundryItem` model
  - New `SundryItemsService`
  - New `SundryItemsForm`
  - `DatabaseContext` CRUD methods for project-level sundry items
- **Important distinction**: The app now supports **manual sundry item entry and storage**, but this is not the same as a true automatic VLOOKUP against complete extracted `Sundry_Data` workbook content.
- **Status**: ⚠️ Workbook formula not fully translated; replaced by manual/project-level sundry maintenance.

---

### Column BC – Sundry Sell (each)

**Excel:**
```
=IFERROR(VLOOKUP(C12, Sundry_Data, 5, FALSE), 0)
```
- Same source table as BA, but column 5 = sundry sell price.
- **Status**: ⚠️ Same limitation as BA. The app has manual sundry management, not full automatic worksheet lookup translation.

---

## 2. Circular / Cross-Sheet References

### Pricing Matrix K5 – AP to PP Factor

**Named range**: `AP_PP_factor` = `Pricing Matrix!K5` = **1.9**

This factor converts aluminium price to production price and is reused in workbook pricing logic.

**C# equivalent**:
```csharp
public const decimal AP_PP_Factor = 1.9m;
```
- **Status**: ✅ Represented as a constant.

---

### 3.Edit Pricing – B57 (ThresholdCostPrice, read-only)

**Excel:**
```
=IF(ThresholdChoice="No", 0,
   IF(ThresholdChoice="20um Clear", AnodiseCost_20um,
      AnodiseCost_25um))
```
- `AnodiseCost_20um` and `AnodiseCost_25um` originate from DATA sheet values not fully extracted.
- **Previous app state**: read-only stub.
- **Recent app update**:
  - `EditPricing` now includes `AnodisedThresholdCostPerM2`
  - `EditPricingForm` displays/edit this value and derives threshold margin %
- **Important distinction**: this is still **not** a full automatic workbook evaluation of B57 from DATA-sheet dependencies.
- **Status**: ⚠️ Still partially stubbed/approximated. The app now stores and uses the threshold cost value, but it is not yet fully workbook-derived.

---

## 3. Summary / Quote_Data Style Calculations

The workbook’s summary areas and output sheets aggregate line-level, finishing, and sundry pricing into customer-facing totals.

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
- Several `QuoteSummary` fields exist in the schema for thermal-break and slotting/sundry margins, but not all are fully populated by the current implementation.
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
- PDF generation is now application-driven rather than HTML-file output, so the exported document format is closer to the workbook deliverable even though the layout is still a WinForms/QuestPDF reconstruction.
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

- product cost tables (`Table_Costs`)
- sundry items (`Sundry_Data`)
- alloy/temper tables
- threshold/anodising inputs
- IP grade source values

### Extraction limitation

The workbook uses XLOOKUP, LAMBDA, and dynamic array formulas that `openpyxl` cannot evaluate.

As a result:
- formula cells may read as `None`
- cached values are only available if Excel last saved the workbook with calculated results

### Impact on app translation

This limitation still affects:

- exact threshold cost derivation
- automatic sundry VLOOKUP translation
- some finish/anodising derived values
- any logic depending on workbook-evaluated dynamic formulas

### Action required for exact fidelity

If precise DATA-sheet values are needed:

1. Open the `.xlsm` in Excel
2. Recalculate/save the workbook
3. Re-run extraction using cached `data_only` values
4. Or manually seed the missing values into SQL reference tables

---

## 6. VeryHidden Sheets

The following workbook sheets are marked `xlSheetVeryHidden` in VBA:

| Sheet | Purpose |
|---|---|
| `Colour_Groups` | Colour code → group → SAP code lookup |
| `DATA_EDGE` | EDGE product pricing reference data |

Both remain relevant to workbook logic.

- `Colour_Groups` is seeded to the app and supports SAP colour code behaviour.
- `DATA_EDGE` remains only partially represented.

---

## 7. Items That Could Not Be Fully Extracted or Translated

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
| Full per-line IP grade translation | Rates supported, line-level grade persistence still missing |
| Full threshold cost derivation from DATA | Stored/used manually, still not fully workbook-driven |
| Dedicated MF ALT customer output | Not yet implemented as separate end-to-end flow |
| Exact CRE8 export schema fidelity | Import exists via best-effort `Cre8ImportService`, but no source CRE8 sample/spec is in the repo |

---

## 8. Sheet Protection Notes

- Password for most protected sheets: **"party"**
- `Revision Control` sheet is unprotected
- In the C# app, the same intent is enforced through UI read-only fields and database-backed state rather than worksheet protection

---

## 9. Practical Translation Summary

### Fully translated or strongly represented

- Metal breakpoint lookup
- Subsill cost/sell logic
- SAP colour code support
- Quote line total persistence
- Project/reference data persistence
- Summary persistence and calculated margins

### Partially translated / approximated

- Finishing cost calculations
- Threshold/anodised cost derivation
- Quote summary formulas from workbook output sheets
- Quote letter/no-pricing sheet reconstruction
- IP upcharge logic

### Not yet fully translated

- Automatic `Sundry_Data` VLOOKUP behaviour
- Full DATA-sheet dependent logic
- Per-line IP grade formula evaluation
- Full `DATA_EDGE`-driven behaviour
- Dedicated MF ALT output workflow
