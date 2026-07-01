# Formula Notes – Project Pricing_Quotation Tool.xlsm

This file documents Excel formulas from the `.xlsm` that could not be directly
translated to C# in `FormulaCalculationService.cs`, along with the rationale for
each decision.

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
- **C# equivalent**: `LookupMetalTypicalPrice(decimal kgPerM)` in
  `FormulaCalculationService.cs` — uses the `MetalBreakpoints` table seeded
  from `SeedData.sql`.
- **Status**: ✅ Translated (simplified to single-axis VLOOKUP-style lookup).

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
- `CostType` references cell `C25` (Edit Pricing → `3.Edit Pricing`).
- `B42`, `B44` are the POA Colour cost and Finishing Sell Price from `EditPricing`.
- Surface area = `H12 * G12 * (perimeter_m)` where `perimeter_m` is an implicit
  column not stored per-row in the current schema.
- **C# equivalent**: `CalculateFinishingCostPerLine(...)` in
  `FormulaCalculationService.cs` — approximated without `perimeter_m`.
- **Status**: ⚠️ Partially translated — perimeter data not available per quote line.
  Finishing cost is approximated as `kgPerM * lengthM * qty * finishRate`.

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
- `G33`, `H33`, `I33` are IP rate cells on `3.Edit Pricing`:
  - G33 = IP1 rate = 1 $/kg
  - H33 = IP2 rate = 2 $/kg
  - I33 = IP3 rate = VLOOKUP("U3", Table_Costs, 2) ≈ 25.85 $/kg
- `IPGrade` is a per-row flag (not currently in `QuoteLines` schema — add if needed).
- **Status**: ⚠️ Not yet translated.  Add `IpGrade` column to `QuoteLines` if required.

---

### Column AY – Subsill Cost

**Excel:**
```
=IF(V12=1, SubsillTierCost * H12 * G12, 0)
```
- `V12` = SubsillFlag (1 = yes, 0 = no).
- `SubsillTierCost` = row-specific tier lookup (Pricing Matrix rows 63-68).
- Tier is determined by extrusion type (not stored per-row; defaults to Tier 1).
- **C# equivalent**: `CalculateSubsillCost(bool isSubsill, int tier, decimal lengthM, int qty)`.
- **Status**: ✅ Translated in `FormulaCalculationService.cs`.

---

### Column AZ – Subsill Sell Price

**Excel:**
```
=IF(V12=1,
   IF(SubsillTier=4, (SubsillTierSell + SubsillTierSell*1.5) * H12,
      SubsillTierSell * H12),
   0)
```
- Tier 4 (Panoramic door) receives a 150 % market factor on sell price.
- **C# equivalent**: `CalculateSubsillSellEach(bool isSubsill, int tier, decimal lengthM)`.
- **Status**: ✅ Translated in `FormulaCalculationService.cs`.

---

### Column BA – Sundry Cost (each)

**Excel (named range `Sundry_Data` → DATA!N8:R58):**
```
=IFERROR(VLOOKUP(C12, Sundry_Data, 4, FALSE), 0)
```
- Looks up the Section Code (col C) in the DATA sheet's sundry table.
- Column 4 of `Sundry_Data` = sundry unit cost.
- **C# equivalent**: Requires a `SundryItems` table seeded from `DATA` sheet.
- **Status**: ⚠️ DATA sheet extraction is incomplete (see Section 5 below).

---

### Column BC – Sundry Sell (each)

**Excel:**
```
=IFERROR(VLOOKUP(C12, Sundry_Data, 5, FALSE), 0)
```
- Same as BA but column 5 = sundry sell price.
- **Status**: ⚠️ Same limitation as BA.

---

## 2. Circular / Cross-Sheet References

### Pricing Matrix K5 – AP to PP Factor

**Named range**: `AP_PP_factor` = `Pricing Matrix!K5` = **1.9**  
This factor converts the "AP" (aluminium price) to "PP" (production price) and is
used in multiple cells on `3.Edit Pricing`.  It is stored as a constant in
`FormulaCalculationService.cs`:
```csharp
public const decimal AP_PP_Factor = 1.9m;
```

---

### 3.Edit Pricing – B57 (ThresholdCostPrice, read-only)

**Excel:**
```
=IF(ThresholdChoice="No", 0,
   IF(ThresholdChoice="20um Clear", AnodiseCost_20um,
      AnodiseCost_25um))
```
- `AnodiseCost_20um` and `AnodiseCost_25um` are sourced from `DATA` sheet cells
  (not yet extracted).
- **C# equivalent**: B57 is a **read-only computed field** in `EditPricingForm`.
  Currently stubbed — set manually until DATA sheet is fully extracted.
- **Status**: ⚠️ Read-only stub. Populate from `DataEdge` table when available.

---

## 3. Formulas Involving the DATA Sheet

The `DATA` sheet and `DATA_EDGE` sheet contain extensive reference tables:
- Product cost tables (`Table_Costs`)
- Sundry items (`Sundry_Data` = N8:R58)
- Alloy/temper tables
- IP grade rate table (G33, H33, I33 source values)

**openpyxl limitation**: The DATA sheet contains many XLOOKUP, LAMBDA and dynamic
array formulas that openpyxl cannot evaluate. Cell values read back as `None` for
formula cells unless `data_only=True` is used (cached values from last Excel save).

**Action required**: If precise DATA sheet values are needed, open the `.xlsm` in
Excel and save with `data_only` mode, then re-run the extraction script, or manually
enter the values into a `DataItems` SQL table.

---

## 4. VeryHidden Sheets

The following sheets are marked `xlSheetVeryHidden` in VBA and cannot be seen by
end users even when un-protecting the workbook:

| Sheet          | Purpose                               |
|----------------|---------------------------------------|
| `Colour_Groups`| Colour code → group → SAP code lookup |
| `DATA_EDGE`    | EDGE product pricing reference data   |

Both are extracted and seeded via `SeedData.sql` / `SqlSchema.sql` respectively.

---

## 5. Items That Could Not Be Extracted

| Item | Reason |
|------|--------|
| VBA Macro code | openpyxl cannot read VBA modules from `.xlsm` |
| Pivot cache data | Pivot tables not supported by openpyxl |
| DATA sheet formula values | XLOOKUP/LAMBDA not evaluated without Excel engine |
| DATA_EDGE full structure | Partial – only column headers extracted |
| Image/logo assets | Binary embedded objects, not extractable |
| Conditional formatting rules | Not needed for C# app |
| Data Validation extension | openpyxl warning: removed on read |
| Print area pixel settings | Page layout settings not parsed |

---

## 6. Sheet Protection Notes

- Password for most protected sheets: **"party"**
- `Revision Control` sheet is unprotected (no password).
- Protection prevents end-users from editing formula cells.
- The C# app enforces the same intent by making computed fields **read-only**
  (`ReadOnly = true` on TextBox controls) in `EditPricingForm`.
