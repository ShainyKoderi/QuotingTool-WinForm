# QuotingTool – Aluminium Extrusion Pricing Quotation Tool

A complete **C# WinForms desktop application** (.NET 8) with **SQL Server** that replicates the functionality of 14 Office Scripts converted from Excel VBA macros, and now includes quote letter generation, no-pricing quote output, sundry item management, SAP clipboard export, and expanded pricing/project data support.

---

## 📋 Project Description

This application is a **Project Pricing Quotation Tool** for managing aluminium extrusion quotes. It was migrated from a set of Excel VBA macros (converted to Office Scripts) into a maintainable, desktop-based C# application backed by SQL Server.

The latest updates add:

- **Quote Letter** preview/print/export workflow
- **Quote No Pricing** customer-facing output without prices
- **Sundry Items** maintenance with totals stored in the database
- **Excel-style Sundry master** (`SUNDRY-1` .. `SUNDRY-26+`) with Section, Mass kg/m, AP, PP used in Quote pricing
- **SAP export** clipboard formatting for quote lines
- **User authentication + role-based access** (General User, Sales Manager, Administrator)
- **Quote summary** persistence and margin calculations
- **Expanded project and edit pricing fields** extracted from the workbook
- **Improved quote line persistence** with total price and line numbering
- **Updated SQL connection configuration** for Windows-authenticated SQL Server use

---

## 🏗️ Architecture Overview

```text
UI Layer (WinForms)
    ↓
Service Layer (Business Logic)
    ↓
Data Layer (Dapper + SQL Server)
```

- **UI**: `WinForms` (.NET 8) — tabbed main window with 7 screens
- **Services**: One service class per original Office Script plus app-specific services for formulas, summaries, sundry items, and SAP export
- **Data**: `ADO.NET` + `Dapper` connected to SQL Server
- **Export**: `ClosedXML` for Excel (.xlsx) export; built-in CSV export; `QuestPDF`-based quote letter / no-pricing PDF output
- **BOM Import**: `CsvHelper` for CSV file parsing

---

## 📁 Project Structure

```text
QuotingTool.sln
QuotingTool/
  QuotingTool.csproj
  Program.cs
  appsettings.json
  Data/
    DatabaseContext.cs      ← Connection + repository methods + normalization helpers
    Models.cs               ← C# model classes (Project, QuoteLine, EditPricing, SundryItem, QuoteSummary, etc.)
    SqlSchema.sql           ← SQL Server schema + additive migration guards
    SeedData.sql            ← Reference/lookup data seeded on first launch
    ExtrusionMasterSeed.sql ← Extrusion section master data from DATA sheet Table5
    ExtrusionMasterUpdateSeed.sql ← ApMm/PpMm/PhCode/SubsillTier/ThermalBreakType column update seed
    DataEdgeMaterialsSeed.sql ← EDGE material → SAP material number cross-reference from DATA_EDGE sheet
  Services/
    FormulaCalculationService.cs ← Core workbook formula translations
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
    QuoteSummaryService.cs  ← App-specific summary and margin calculations
    SundryItemsService.cs   ← CRUD helper for sundry items
    SapExportService.cs     ← Clipboard export for SAP-friendly tabular data
    Cre8ImportService.cs    ← Best-effort CRE8 project-details CSV import
  Forms/
    MainForm.cs/.Designer.cs           ← Main tabbed window
    ProjectDetailsForm.cs/.Designer.cs ← Tab 1: Project details
    QuoteForm.cs/.Designer.cs          ← Tab 2: Quote lines grid
    EditPricingForm.cs/.Designer.cs    ← Tab 3: Pricing configuration
    QuoteLetterForm.cs/.Designer.cs    ← Tab 4: Quote letter preview/output
    QuoteNoPricingForm.cs/.Designer.cs ← Tab 5: No-pricing quote output
    BomImportForm.cs/.Designer.cs      ← Tab 6: BOM CSV import
    MonthlyUpdateForm.cs/.Designer.cs  ← Tab 7: Monthly rate update
    SundryItemsForm.cs/.Designer.cs    ← Dialog: sundry item maintenance
    OpenProjectDialog.cs               ← Project selection dialog
  Helpers/
    ExcelExporter.cs        ← Export to .xlsx / .csv (ClosedXML)
    ProtectionHelper.cs     ← Sheet lock/unlock (DB-backed)
```

---

## ✅ Prerequisites

- **Visual Studio 2022** (Community or higher)
- **.NET 8 SDK** (`net8.0-windows`)
- **SQL Server** reachable from the configured connection string
  - Current default connection string uses:
    - Server: `SLWSC16SQP01`
    - Database: `QuotingToolDb`
    - Windows Authentication
    - `TrustServerCertificate=True`
- If required, edit `QuotingTool/appsettings.json` for your own SQL Server instance

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

4. **Configure the database connection**
   - Review `QuotingTool/appsettings.json`
   - Update the SQL Server name if `SLWSC16SQP01` is not available in your environment

5. **Build and run**
   ```bash
   dotnet build
   dotnet run --project QuotingTool/QuotingTool.csproj
   ```
   - On first launch, the database schema is created or updated automatically (`SqlSchema.sql` is executed)

---

## 🗺️ Script → Service Mapping

| # | Original Office Script | Service Class | Description |
|---|---|---|---|
| 01 | `01_PageSetup_SetPageLayout.ts` | `PageSetupService` | Set active pages (1–6), update footer colours & print area |
| 02 | `02_LengthFix_mm_to_m.ts` | `LengthFixService` | Convert lengths > 1000 from mm to m |
| 03 | `03_ResetPriceFormulas.ts` | `ResetPriceService` | Restore prices from master formula backup |
| 04 | `04_ShowHideBulk.ts` | `ShowHideBulkService` | Toggle CostTypeOverride (show/hide bulk pricing rows) |
| 05 | `05_EditPricing_RowVisibility.ts` | `EditPricingRowVisibilityService` | Calculate visible/hidden rows in Edit Pricing |
| 06 | `06_MonthlyUpdate_Open.ts` | `MonthlyUpdateService.OpenMonthlyUpdate` | Unlock maintenance sheets |
| 07 | `07_MonthlyUpdate_Close.ts` | `MonthlyUpdateService.CloseMonthlyUpdate` | Save rates, reset prices, lock sheets |
| 08 | `08_ExportQuoteData_InWorkbook.ts` | `ExportQuoteService` | Validate pricing & collect all quote blocks |
| 09 | `09_BOM_UniqueAndMode.ts` | `BomService.GetUniqueFinishes / GetMostCommonFinish` | Analyse BOM finish values |
| 10 | `10_ImportBOM_Distribute.ts` | `BomService.ImportBomFromCsv / DistributeBomToQuote` | Import CSV and distribute to quote pages |
| 11 | `11_ImportPrices_Distribute.ts` | `ImportPricesService` | Copy BOM prices to quote lines |
| 12 | `12_RefreshPivots.ts` | `PivotRefreshService` | Recalculate totals per page & grand total |
| 13 | `13_IP_Reset.ts` | `IPResetService` | Reset IP calculation columns |
| 14 | `14_NavigateToSheet.ts` | `NavigationService` | Switch active tab on MainForm |

---

## 🧩 Current Application Features

### Main tabs

1. **Project Details**
2. **Quote**
3. **Edit Pricing**
4. **Quote Letter**
5. **No Pricing**
6. **BOM Import**
7. **Monthly Update**

### New user-facing capabilities

- Generate a **Quote Letter** with customer/project metadata, price summary, delivery charge, and quote validity, then save it as a native PDF
- Generate a **No Pricing** quote view for customer-facing schedules without dollar values, then save it as a native PDF
- Use prominent per-form **Save** actions on Project Details, Quote, Edit Pricing, and Sundry Items instead of a shared MainForm save button
- Maintain **Sundry Items** in a dedicated dialog with running total calculation
- Edit global **Sundry master** rows with fixed section codes (`SUNDRY-1`..`SUNDRY-26`) plus **Edit Items 26+** for additional codes
- Use `SUNDRY-*` directly in Quote **Section**; Description auto-fills and pricing uses sundry Mass/AP/PP inputs
- Copy quote lines to clipboard in **SAP-ready tab-separated format**
- Import inferred **CRE8** project-details CSV data into the currently open project
- View richer **Edit Pricing** metrics including totals, IP upcharges, summary margins, and threshold data
- Use per-tab action buttons: **Fix Lengths**, **Reset Prices**, **Export Quote**, and **Copy for SAP** live on the **Quote** tab; **Refresh Summary** lives on the **Edit Pricing** tab near the summary section
- Manually maintain quote lines directly on the **Quote** tab using **Add Row** / **Delete Row** in addition to **BOM Import**
- Sign in through a **Login** form before entering the app:
  - **General User**: Project Details, Quote, Quote Letter, No Pricing, BOM Import, Sundry editing
  - **Sales Manager**: General access + pricing override authorisations
  - **Administrator**: Full access including Monthly Update and User Management

### Default administrator (first run)

- Username: `admin`
- Initial password:
  - `QUOTINGTOOL_DEFAULT_ADMIN_PASSWORD` value, if configured, or
  - a generated strong password shown in a first-run popup
- Change this password immediately from **Tools → User Management**

---

## 📐 2.Quote Sheet – Calculation Pipeline

The `$ Each` (col I) for each quote line is the sum of four components:

```
$ Each (I) = MF Each (AH) + IP Each (AI) + SF Sell Each (AS) + Slotting/Sundry ea (K)
$ Total (J) = $ Each (I) × Qty (G)
```

### Component breakdown

| Component | Excel Col | Formula | Key inputs |
|---|---|---|---|
| MF Each | AH | `kg each × MetalSellPrice (B33)` for standard; PLS/PDB use TB cost tables | `Y` (kg/m), `H` (length), `B33` ($/kg) |
| IP Each | AI | `kg each × IP rate` (G33/H33/I33) | `AV` (IP category 0–3), IP rates from `EditPricing` |
| SF Sell Each | AS | AP or PP area × SF Sell $/m² (AP12) | `AC`/`AF` (area), `AP` ($/m²), `AQ` (AP or PP path) |
| Slotting/Sundry ea | K | `AZ (SlotSellEach) + BC (SundrySellEach)` | Subsill tier from `SubsillTierPrices`, sundry from `Sundry_Data` |

### Metal price calculation chain

```
LME Rate + Billet Premium = B3   (from Table_Costs / RevisionControl)
B3 + Internal Spread       = B5  (metal cost total, e.g. 8.73 $/kg, June 2026)
User enters / VLOOKUP      = B33 (metal sell price, e.g. 9–12 $/kg depending on volume)
kg/m (from ExtrusionMaster) × Length (m) = kg each
kg each × B33 = MF Each (col AH, standard path)
```

---

## 🗃️ Database and Model Updates

### Expanded `Projects` fields

Recent updates added or normalized support for:

- `AttentionTo`
- `QuoteValidity`
- `AddLocAlLogos`
- `GenerateMillFinishAlternative`
- `DeliveryCharge`
- `CustomerSapDiscount`
- `AccountManager`
- `ProjectNotes`
- `FinishGroup`
- `FinishName`
- `FinishSapCode`

Normalization logic in `DatabaseContext` keeps legacy/new field pairs aligned, including:

- `Attention` ↔ `AttentionTo`
- `LocalLogo` ↔ `AddLocAlLogos`
- `GenerateMfAlt` ↔ `GenerateMillFinishAlternative`

### Expanded `QuoteLines` fields

- `TotalPrice`
- `LineNumber`

### Expanded `EditPricing` fields

Includes additional workbook-derived and app-derived fields such as:

- `PricingNotes`
- `TotalKg`
- `TotalFinishingM2`
- `TotalPieces`
- `IP1UpchargePerKg` – default **1.00 $/kg** (workbook `3.Edit Pricing!G33`)
- `IP2UpchargePerKg` – default **2.00 $/kg** (workbook `3.Edit Pricing!H33`)
- `IP3PanoramicFixedKgRate` – default **25.85 $/kg** (workbook `3.Edit Pricing!I33`, derived from `Table_Costs`)
- `IP1MarginPct`, `IP2MarginPct`, `IP3MarginPct`
- `MillFinishMarginPct`
- `FinishingMarginPct`
- `FinishingCostPricePerM2` – default **10.50 $/m²** (workbook `Pricing Matrix!E44`)
- `AnodisedThresholdCostPerM2` – default **10.65 $/m²** (workbook `Pricing Matrix!E49`)
- `ThermalBreakMarginPct`

### Current tables

| Table | Source sheet | Purpose |
|---|---|---|
| `Projects` | 1.Project Details | Project metadata |
| `QuoteLines` | 2.Quote | Line items (section, qty, length, price) |
| `ExtrusionMaster` | DATA – Table5 | Section code lookup with kg/m |
| `EditPricing` | 3.Edit Pricing | Pricing configuration per project |
| `BomLines` | BOM_Import | Imported BOM CSV rows |
| `RevisionControl` | Revision Control* | Monthly LME/billet rate history |
| `SundryItems` | Manual entry | Sundry charges per project |
| `QuoteSummary` | Quote_Data / 3.Edit Pricing | Calculated margin summary per project |
| `MetalBreakpoints` | Pricing Matrix rows 8–13 | Metal sell price tier lookup |
| `ThermalBreakBreakpoints` | Pricing Matrix rows 16–21 | TB margin/list price tier lookup |
| `PowderCostTable` | Pricing Matrix rows 28–40 | Finishing cost $/m² by vendor and group |
| `FinishBreakpoints` | Pricing Matrix rows 54–60 | Finishing margin tier lookup |
| `SubsillTierPrices` | Pricing Matrix rows 65–68 | Subsill cost and sell $/m |
| `PricingConstants` | Pricing Matrix rows 1–6 | Global metal/delivery constants (LME+billet, spreads, PDB/ALF cost, delivery, AP↔PP factor) feeding MetalPricePerKg / $ Each |
| `ColourGroups` | Colour_Groups (VeryHidden) | SAP colour code master |
| `ValidationData` | Validation | Dropdown list data |
| `DataEdgeMaterials` | DATA_EDGE (VeryHidden) | EDGE material reference → SAP material number cross-reference |
| `QuoteFooters` | Named ranges pg1_foot…pg6_foot | Per-page footer colour |
| `PrintSettings` | — | Page count / print area per project |
| `MasterFormulas` | col BA backup | Formula expression backup |
| `SheetLocks` | — | Sheet lock state |

\* The workbook's `Revision Control` sheet is a plain version changelog (Date / Change Description / Implemented by / New Version Number) with no rate data, so it is not itself a calculation input. The `RevisionControl` table instead persists the monthly LME/billet rate history needed by `MonthlyUpdateService`, mirroring the same real-world workflow the sheet's changelog entries describe.

### Missing tables / fields required for full calculation fidelity

The following items have been **implemented** in this update:

| Item | Status |
|---|---|
| `ThresholdSections` table (col BF threshold flag) | ✅ Added; seeded from Validation!$N:$N |
| `ExtrusionMaster.PhCode` (DATA col H) | ✅ Added; seeded via `ExtrusionMasterUpdateSeed.sql` |
| `IPMasterData` table (PH → IP category) | ✅ Added; seeded from `5.Edit_PH_IP` |
| `ExtrusionMaster.SubsillTier` (DATA col K) | ✅ Added; seeded; auto-fills `SubsillFlag` on Quote tab |
| `ExtrusionMaster.ThermalBreakType` (DATA col M) | ✅ Added; seeded; drives `*` prefix in Description |
| `QuoteLines.IpGrade` | ✅ Added; auto-computed from PhCode → IPMasterData |
| `ExtrusionMaster.ApMm` (DATA col E) | ✅ Added; seeded (used by future SF Sell Each calculation) |
| `ExtrusionMaster.PpMm` (DATA col F) | ✅ Added; seeded (used by future SF Sell Each calculation) |
| Description `*` prefix for PDB/PLS sections | ✅ Implemented in `ApplySectionLookup()` |
| Description ` **MILL FINISH**` suffix | ✅ Implemented in `ApplySectionLookup()` |
| IP Each per quote line (col AI) | ✅ Implemented in `CalculateIpEach()` |

The following items are still outstanding:

| Required item | Used by formula | Suggested location |
|---|---|---|
| SF Sell Each (AS) per line | Col I ($ Each) | Requires `ApMm`/`PpMm` + finishing rate logic |

`SqlSchema.sql` includes additive `IF COL_LENGTH(...) IS NULL` and `IF NOT EXISTS` guards so schema upgrades can run safely on existing databases. The new `ExtrusionMasterUpdateSeed.sql` file is executed automatically on first launch when `ApMm IS NULL`, updating both new and existing installations.

### PDB metal cost base / ALF cost per metre / DATA_EDGE (resolved)

`PricingConstants` and `DataEdgeMaterials` tables were added to fully cover the reference sheets in `Project Pricing_Quotation Tool_v1.xlsx`:

- **`PricingConstants`** – global metal/delivery constants from `Pricing Matrix` rows 1-6 (`LmeBilletPremium`, `InternalSpread`, `MetalCostPerKg`, `ExtraSpread`, `PdbMetalCostBase`, `AlfCostPerMetre`, `StandardDeliveryCharge`, `ApToPpFactor`). These values previously existed only as documentation constants below and are now persisted in the database and read via `FormulaCalculationService.GetPricingConstants()`.
- **`DataEdgeMaterials`** – full 1,190-row EDGE material reference → SAP material number cross-reference from the `DATA_EDGE` sheet, seeded via `DataEdgeMaterialsSeed.sql`.
- The placeholder/estimated seed values previously in `SeedData.sql` for `MetalBreakpoints`, `ThermalBreakBreakpoints`, `PowderCostTable`, `FinishBreakpoints`, `SubsillTierPrices`, `ColourGroups`, and `ValidationData` have all been replaced with the actual values extracted directly from `Project Pricing_Quotation Tool_v1.xlsx`.

---

## 📐 Key Constants and Defaults

### Workbook-derived constants (June 2026 rates from Sample.xlsm)

| Constant | Value | Workbook source |
|---|---|---|
| LME + billet premium | 5.61 $/kg | Pricing Matrix B3 |
| Internal spread | 3.12 $/kg | Pricing Matrix B4 |
| Metal cost total | 8.73 $/kg | Pricing Matrix B5 = B3+B4 |
| Extra spread (F3) | 0.35 $/kg | Pricing Matrix F3 |
| PDB metal cost base | 9.08 $/kg | Pricing Matrix F4 = B5+F3 |
| ALF cost per metre | 6.46 $/m | Pricing Matrix F5 |
| AP to PP factor | 1.9 | Pricing Matrix K5 |
| Standard delivery charge | $97 | Pricing Matrix H3 |
| Finishing cost $/m² (standard) | 10.50 | Pricing Matrix E44 |
| Anodised threshold cost $/m² | 10.65 | Pricing Matrix E49 |
| IP1 upcharge default | 1.00 $/kg | 3.Edit Pricing G33 |
| IP2 upcharge default | 2.00 $/kg | 3.Edit Pricing H33 |
| IP3 panoramic rate (approx) | ≈25.85 $/kg | 3.Edit Pricing I33 via Table_Costs |

### Application layout constants

| Constant / Default | Value |
|---|---|
| Page block row ranges | [12,51], [68,107], [123,162], [178,217], [233,272], [288,327] |
| LastRow per page count (1–6) | 56, 111, 166, 221, 276, 331 |
| Page count thresholds | > 40 rows → 2 pages; > 80 → 3; > 120 → 4; > 160 → 5; > 200 → 6 |
| Default quote validity | 30 days |
| Quote summary mill-finish cost ratio | 0.87 (approximation) |

---

## 📊 Workbook Extraction and Translation Status

### Sheets supported in the app

The app now covers workbook content across:

- `1.Project Details`
- `2.Quote`
- `3.Edit Pricing`
- `4.Quote Letter`
- `4b. Quote no pricing`
- `BOM_Import`
- `Quote_Data`
- workbook reference sheets such as `Validation`, `Pricing Matrix`, `Colour_Groups`, and partial `DATA`/`DATA_EDGE`

### Formula translation location

Core translations are in:

- `QuotingTool/Services/FormulaCalculationService.cs`

Additional summary logic is in:

- `QuotingTool/Services/QuoteSummaryService.cs`

See `FORMULA_NOTES.md` for:
- the full column-by-column formula reference for `2.Quote`
- actual workbook values for all pricing reference tables (Section 6)
- identified gaps between current seed data and actual workbook values
- missing tables/fields needed for full calculation fidelity

---

## ⚠️ Known Limitations

Compared to the original Excel workbook:

1. **No live Excel event model** — recalculation is driven by WinForms actions and save/update flows
2. **Some DATA-sheet formulas remain unresolved** — especially values requiring Excel evaluation of XLOOKUP/LAMBDA/dynamic arrays
3. **Threshold cost and finishing logic are approximate or partially stubbed** — the per-line AO/AP/AS calculations are not yet computed; finishing is project-level only
4. **IP grade not persisted per line** — `QuoteLines.IpGrade` column does not yet exist; IP upcharge (`$ Each` AI component) is currently passed as `0m`
5. ~~Seed data in `SeedData.sql` uses placeholder/estimated values~~ — **resolved**: `MetalBreakpoints`, `ThermalBreakBreakpoints`, `FinishBreakpoints`, `SubsillTierPrices`, `PowderCostTable`, `ColourGroups`, and `ValidationData` are now seeded with the actual values extracted from `Project Pricing_Quotation Tool_v1.xlsx`; global `Pricing Matrix` constants (LME/billet, spreads, PDB/ALF cost, delivery, AP↔PP factor) are seeded into the new `PricingConstants` table
6. ~~`ExtrusionMaster` missing columns for full per-line calculation~~ — **resolved**: `ApMm`, `PpMm`, `SubsillTier`, `ThermalBreakType`, `PhCode` are all seeded via `ExtrusionMasterUpdateSeed.sql`
7. **CRE8 import uses an inferred/best-effort column mapping** until a real CRE8 sample/export specification is available
8. **VBA macro code, pivot cache data, embedded workbook assets, and some hidden-sheet structures are still not directly extractable** — `Colour_Groups` and `DATA_EDGE` (both VeryHidden) are now fully seeded into `ColourGroups` and `DataEdgeMaterials`

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
| `QuestPDF` | 2024.10.3 | PDF generation for quote letter and no-pricing export |

---

## 📎 Notes

- The repository default branch is `main`.
- The repository language composition is currently **100% C#**.
- The **MainForm toolbar** contains only **New Project** and **Open Project** — these are the only project-lifecycle actions not tied to a specific tab.
- The **MainForm Tools menu** contains only **Sundry Items** and **Copy for SAP**.
- Per-tab action buttons: **Fix Lengths**, **Reset Prices**, **Export Quote**, and **Copy for SAP** are on the **Quote** tab; **Refresh Summary** (triggers `PivotRefreshService` and updates the on-screen summary grid) is on the **Edit Pricing** tab.
- Save actions now live on the relevant forms/tabs (Project Details, Quote, Edit Pricing, Sundry Items).
- The File menu no longer includes a shared Save command for the same reason.
- The README reflects changes through the latest repository updates, including the SQL connection string change committed after the major quote-letter/sundry feature merge.
