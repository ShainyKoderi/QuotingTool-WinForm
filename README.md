# QuotingTool – Aluminium Extrusion Pricing Quotation Tool

A complete **C# WinForms desktop application** (.NET 8) with **SQL Server** that replicates the functionality of 14 Office Scripts converted from Excel VBA macros.

---

## 📋 Project Description

This application is a **Project Pricing Quotation Tool** for managing aluminium extrusion quotes. It was migrated from a set of Excel VBA macros (converted to Office Scripts) into a maintainable, database-backed desktop application.

---

## 🏗️ Architecture Overview

```
UI Layer (WinForms)
    ↓
Service Layer (Business Logic)
    ↓
Data Layer (Dapper + SQL Server)
```

- **UI**: `WinForms` (.NET 8) — tabbed main window with 5 screens
- **Services**: One service class per original Office Script (12 services)
- **Data**: `ADO.NET` + `Dapper` connected to SQL Server LocalDB
- **Export**: `ClosedXML` for Excel (.xlsx) export; built-in CSV export
- **BOM Import**: `CsvHelper` for CSV file parsing

---

## 📁 Project Structure

```
QuotingTool.sln
QuotingTool/
  QuotingTool.csproj
  Program.cs
  appsettings.json
  Data/
    DatabaseContext.cs      ← Connection + all repository methods
    Models.cs               ← C# model classes (Project, QuoteLine, etc.)
    SqlSchema.sql           ← SQL Server schema (auto-run on first launch)
  Services/
    PageSetupService.cs     ← Script 01: Page layout & print area
    LengthFixService.cs     ← Script 02: mm → m length conversion
    ResetPriceService.cs    ← Script 03: Reset price formulas from master
    ShowHideBulkService.cs  ← Script 04: Toggle CostTypeOverride
    EditPricingRowVisibilityService.cs ← Script 05: Row show/hide logic
    MonthlyUpdateService.cs ← Scripts 06+07: Open/close monthly update
    ExportQuoteService.cs   ← Script 08: Validate & export quote data
    BomService.cs           ← Scripts 09+10: BOM import & distribute
    ImportPricesService.cs  ← Script 11: Copy BOM prices to quote
    PivotRefreshService.cs  ← Script 12: Refresh summary totals
    IPResetService.cs       ← Script 13: Reset IP formula columns
    NavigationService.cs    ← Script 14: Navigate between tabs
  Forms/
    MainForm.cs/.Designer.cs          ← Main tabbed window
    ProjectDetailsForm.cs/.Designer.cs ← Tab 1: Project details
    QuoteForm.cs/.Designer.cs         ← Tab 2: Quote lines grid
    EditPricingForm.cs/.Designer.cs   ← Tab 3: Pricing configuration
    BomImportForm.cs/.Designer.cs     ← Tab 4: BOM CSV import
    MonthlyUpdateForm.cs/.Designer.cs ← Tab 5: Monthly rate update
    OpenProjectDialog.cs              ← Project selection dialog
  Helpers/
    ExcelExporter.cs        ← Export to .xlsx / .csv (ClosedXML)
    ProtectionHelper.cs     ← Sheet lock/unlock (DB-backed)
```

---

## ✅ Prerequisites

- **Visual Studio 2022** (Community or higher)
- **.NET 8 SDK** (`net8.0-windows`)
- **SQL Server LocalDB** (ships with Visual Studio)
  - Or change the connection string in `appsettings.json` to point to any SQL Server instance

---

## 🚀 Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/ShainyKoderi/QuotingTool.git
   ```

2. **Open the solution**
   - Open `QuotingTool.sln` in Visual Studio 2022

3. **Restore NuGet packages**
   - Visual Studio will restore automatically, or run:
     ```bash
     dotnet restore
     ```

4. **Configure the database** (optional)
   - Edit `QuotingTool/appsettings.json` if you want a different SQL Server instance
   - Default: `(localdb)\MSSQLLocalDB` — no setup required

5. **Build and run**
   ```bash
   dotnet build
   dotnet run --project QuotingTool/QuotingTool.csproj
   ```
   - On first launch, the database schema is created automatically (`SqlSchema.sql` is executed)

---

## 🗺️ Script → Service Mapping

| # | Original Office Script | Service Class | Description |
|---|---|---|---|
| 01 | `01_PageSetup_SetPageLayout.ts` | `PageSetupService` | Set active pages (1-6), update footer colours & print area |
| 02 | `02_LengthFix_mm_to_m.ts` | `LengthFixService` | Convert lengths > 1000 from mm to m |
| 03 | `03_ResetPriceFormulas.ts` | `ResetPriceService` | Restore prices from master formula backup |
| 04 | `04_ShowHideBulk.ts` | `ShowHideBulkService` | Toggle CostTypeOverride (show/hide bulk pricing rows) |
| 05 | `05_EditPricing_RowVisibility.ts` | `EditPricingRowVisibilityService` | Calculate visible/hidden rows in Edit Pricing |
| 06 | `06_MonthlyUpdate_Open.ts` | `MonthlyUpdateService.OpenMonthlyUpdate` | Unlock maintenance sheets |
| 07 | `07_MonthlyUpdate_Close.ts` | `MonthlyUpdateService.CloseMonthlyUpdate` | Save rates, reset prices, lock sheets |
| 08 | `08_ExportQuoteData_InWorkbook.ts` | `ExportQuoteService` | Validate pricing & collect all 6 blocks |
| 09 | `09_BOM_UniqueAndMode.ts` | `BomService.GetUniqueFinishes / GetMostCommonFinish` | Analyse BOM finish values |
| 10 | `10_ImportBOM_Distribute.ts` | `BomService.ImportBomFromCsv / DistributeBomToQuote` | Import CSV and distribute to quote pages |
| 11 | `11_ImportPrices_Distribute.ts` | `ImportPricesService` | Copy BOM prices to quote lines |
| 12 | `12_RefreshPivots.ts` | `PivotRefreshService` | Recalculate totals per page & grand total |
| 13 | `13_IP_Reset.ts` | `IPResetService` | Reset IP calculation columns |
| 14 | `14_NavigateToSheet.ts` | `NavigationService` | Switch active tab on MainForm |

---

## 📐 Key Constants

| Constant | Values |
|---|---|
| Page block row ranges | [12,51], [68,107], [123,162], [178,217], [233,272], [288,327] |
| LastRow per page count (1-6) | 56, 111, 166, 221, 276, 331 |
| Page count thresholds | > 40 rows → 2 pages; > 80 → 3; > 120 → 4; > 160 → 5; > 200 → 6 |
| Subsill surcharge | $12 per unit |
| Default prices (B42/B44/B48/B50) | 99 |

---

## 📸 Screenshots

_Screenshots will be added after initial deployment._

---

## ⚠️ Known Limitations

Compared to the original Excel workbook:

1. **No UserForms** — all input is done via WinForms controls (equivalent functionality)
2. **No live cell events** — recalculation is triggered by buttons rather than automatic on cell change
3. **No file dialogs for printing** — use the Export button to generate Excel/CSV; printing is handled by the OS from the exported file
4. **No pivot tables** — `PivotRefreshService` provides equivalent aggregated summary data
5. **No Excel formula syntax** — price formulas are stored as decimal values in `MasterFormulas`; the service restores these values rather than re-evaluating formula strings
6. **Password "party"** — the original workbook used this password for sheet protection; this app uses database-level role security instead (tracked via `SheetLocks` table)

---

## 📦 NuGet Packages

| Package | Version | Purpose |
|---|---|---|
| `Microsoft.Data.SqlClient` | 5.2.1 | SQL Server connectivity |
| `Dapper` | 2.1.35 | Lightweight ORM |
| `ClosedXML` | 0.102.2 | Excel (.xlsx) export |
| `Microsoft.Extensions.Configuration` | 8.0.0 | Configuration framework |
| `Microsoft.Extensions.Configuration.Json` | 8.0.0 | JSON config file support |
| `CsvHelper` | 33.0.1 | CSV file import |

---

## 📊 What Was Extracted from `Project Pricing_Quotation Tool.xlsm`

The `.xlsm` file was read using **openpyxl** (Python) after being uploaded to the
repository root.  The following content was extracted and incorporated into the app.

### Sheets Found (17 total)

| Sheet Name | Type | Notes |
|---|---|---|
| `DATA` | Reference | Product cost tables, sundry data. Formula cells not evaluable without Excel. |
| `DATA_EDGE` | Reference | EDGE product pricing. VeryHidden in VBA. |
| `Validation` | Reference | All dropdown list values. Seeded to `ValidationData` table. |
| `Pricing Matrix` | Reference | Metal/TB/finish/subsill breakpoint tables. Seeded to 5 tables. |
| `Colour_Groups` | Reference | 91 colour entries. VeryHidden in VBA. Seeded to `ColourGroups` table. |
| `Revision Control` | Admin | Version/date tracking. Maps to `RevisionControl` table. |
| `1.Project Details` | Input | 18+ project fields. All mapped to `Projects` table. |
| `2.Quote` | Input/Calc | Main quote grid. 6 page blocks × up to ~40 rows. |
| `2b.Quote MF ALT` | Input/Calc | Mill Finish ALT quote variant. |
| `3.Edit Pricing` | Config | Pricing config. Maps to `EditPricing` table. |
| `3.EDGE` | Config | EDGE product pricing config. |
| `4.Quote Letter` | Output | Quote letter / customer-facing export. |
| `4b. Quote no pricing` | Output | Quote letter without prices. |
| `4c.Quote MF ALT` | Output | Mill Finish ALT quote letter. |
| `5.Edit_PH_IP` | Config | PH (passive house) and IP (ingress protection) settings. |
| `BOM_Import` | Input | Bill of Materials import. Maps to `BomLines` table. |
| `Quote_Data` | Summary | Pivot / summary data sheet. |

### Named Ranges Found (108 total — key ones listed)

| Named Range | Refers To | Purpose |
|---|---|---|
| `pg1_foot` … `pg6_foot` | `2.Quote!B56`, etc. | Footer row reference per page |
| `AP_PP_factor` | `Pricing Matrix!K5` = 1.9 | AP → PP conversion factor |
| `Sundry_Data` | `DATA!N8:R58` | Sundry item cost lookup table |
| `ThresholdChoice` | `1.Project Details!B??` | "No" / "20um Clear" / "25um Clear" |
| `Threshold_Code` | `Validation!E10:F11` | Threshold SAP code lookup |
| `SAP_Colour_Code` | `Colour_Groups` lookup | Final SAP colour code |
| `MetalRate_Table` | `Pricing Matrix!B7:C13` | Metal cost breakpoints |
| `TB_Rate_Table` | `Pricing Matrix!B14:C21` | Thermal break breakpoints |
| `Finish_BP_Table` | `Pricing Matrix!B51:C60` | Finish cost breakpoints |
| `Subsill_Tier_Table` | `Pricing Matrix!B63:C68` | Subsill tier prices |

### Formulas Translated to C#

All translations are in `QuotingTool/Services/FormulaCalculationService.cs`.

| Excel Formula / Cell | C# Method | Sheet |
|---|---|---|
| `I12 = AH + AI + AS + K` | `CalculatePriceEach(...)` | 2.Quote |
| `J12 = I12 * G12` | `CalculateSellTotal(qty, price)` | 2.Quote |
| `K12 = AZ + BC` | `CalculateSlotSellEach(...)` | 2.Quote |
| `AY12 = tier_cost * H * G` | `CalculateSubsillCost(...)` | 2.Quote |
| `AZ12 = tier_sell * H (×2.5 for Tier 4)` | `CalculateSubsillSellEach(...)` | 2.Quote |
| `M12 = SAP code VLOOKUP` | `CalculateSapColourCode(...)` | 2.Quote |
| `BA12 = VLOOKUP(C12, Sundry_Data, 4)` | *stub — see FORMULA_NOTES.md* | 2.Quote |
| `Metal breakpoint lookup` | `LookupMetalTypicalPrice(kgPerM)` | Pricing Matrix |
| `Powder cost lookup` | `LookupPowderCostByVendorAndGroup(...)` | Pricing Matrix |
| `Finish breakpoint lookup` | `LookupFinishFactor(kgPerM)` | Pricing Matrix |
| `Subsill tier lookup` | `LookupSubsillTierCost(tier)` | Pricing Matrix |
| `B57 = threshold cost` | Read-only stub in EditPricingForm | 3.Edit Pricing |

### Column M (`SapColourCode`) in 2.Quote

- **Header**: "SAP Colour Code"
- **Formula**: `=IF(C="","", IF(AND(BF>0, NOT(ThresholdChoice="No")), VLOOKUP(ThresholdChoice, Threshold_Code, 2), IF(L="No", SAP_Colour_Code, "NON")))`
- **Added to**: `QuoteLines` table (column `SapColourCode`), `QuoteLine` model, `QuoteForm` grid (column M header).

### New Database Tables Added from .xlsm

| Table | Source Sheet | Rows Seeded |
|---|---|---|
| `ColourGroups` | `Colour_Groups` | ~91 |
| `ValidationData` | `Validation` | ~50 |
| `MetalBreakpoints` | `Pricing Matrix` rows 7-13 | 7 |
| `ThermalBreakBreakpoints` | `Pricing Matrix` rows 14-21 | 8 |
| `PowderCostTable` | `Pricing Matrix` rows 26-40 | 48 |
| `FinishBreakpoints` | `Pricing Matrix` rows 51-60 | 10 |
| `SubsillTierPrices` | `Pricing Matrix` rows 63-68 | 6 |

### New Project Fields Added from `1.Project Details`

Region, QuotedBy, AccountManager, Attention, MarketSegment, ProductSegment,
ProjectNotes, FinishType, FinishApplicator, FinishGroup, FinishName,
FinishCustomName, FinishSapCode, ThresholdChoice, LocalLogo, GenerateMfAlt,
RevisionNumber, QuoteDate.

### Items That Could Not Be Extracted

See `FORMULA_NOTES.md` for full details.

- VBA macro code (not readable by openpyxl)
- Pivot table cache
- `DATA` sheet formula-derived values (XLOOKUP/LAMBDA not evaluated)
- `DATA_EDGE` full structure
- Embedded image/logo assets
