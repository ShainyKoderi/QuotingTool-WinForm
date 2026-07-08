# QuotingTool – Aluminium Extrusion Pricing Quotation Tool

A complete **C# WinForms desktop application** (.NET 8) with **SQL Server** that replicates the functionality of 14 Office Scripts converted from Excel VBA macros, and now includes quote letter generation, no-pricing quote output, sundry item management, SAP clipboard export, and expanded pricing/project data support.

---

## 📋 Project Description

This application is a **Project Pricing Quotation Tool** for managing aluminium extrusion quotes. It was migrated from a set of Excel VBA macros (converted to Office Scripts) into a maintainable, desktop-based C# application backed by SQL Server.

The latest updates add:

- **Quote Letter** preview/print/export workflow
- **Quote No Pricing** customer-facing output without prices
- **Sundry Items** maintenance with totals stored in the database
- **SAP export** clipboard formatting for quote lines
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

- **UI**: `WinForms` (.NET 8) — tabbed main window with 7 screens
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
| 01 | `01_PageSetup_SetPageLayout.ts` | `PageSetupService` | Set active pages (1-6), update footer colours & print area |
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
- Copy quote lines to clipboard in **SAP-ready tab-separated format**
- Import inferred **CRE8** project-details CSV data into the currently open project
- View richer **Edit Pricing** metrics including totals, IP upcharges, summary margins, and threshold data
- Use per-tab action buttons: **Fix Lengths**, **Reset Prices**, **Export Quote**, and **Copy for SAP** live on the **Quote** tab; **Refresh Summary** lives on the **Edit Pricing** tab near the summary section

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
- `IP1UpchargePerKg`
- `IP2UpchargePerKg`
- `IP3PanoramicFixedKgRate`
- `IP1MarginPct`
- `IP2MarginPct`
- `IP3MarginPct`
- `MillFinishMarginPct`
- `FinishingMarginPct`
- `FinishingCostPricePerM2`
- `AnodisedThresholdCostPerM2`
- `ThermalBreakMarginPct`

### New tables

| Table | Purpose |
|---|---|
| `SundryItems` | Stores manual sundry charges by project |
| `QuoteSummary` | Stores calculated pricing/margin summary totals by project |

`SqlSchema.sql` now includes additive `IF COL_LENGTH(...) IS NULL` and `IF NOT EXISTS` guards so schema upgrades can run safely on existing databases.

---

## 📐 Key Constants and Defaults

| Constant / Default | Value |
|---|---|
| Page block row ranges | [12,51], [68,107], [123,162], [178,217], [233,272], [288,327] |
| LastRow per page count (1-6) | 56, 111, 166, 221, 276, 331 |
| Page count thresholds | > 40 rows → 2 pages; > 80 → 3; > 120 → 4; > 160 → 5; > 200 → 6 |
| Default delivery charge | 97.00 |
| Default quote validity | 30 days |
| IP1 default upcharge | 1.00 $/kg |
| IP2 default upcharge | 2.00 $/kg |
| IP3 panoramic fixed rate | 25.85 $/kg |
| AP → PP factor | 1.9 |
| Quote summary mill-finish cost ratio | 0.87 |

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

See `FORMULA_NOTES.md` for workbook formulas that are fully translated, approximated, stubbed, or still blocked by missing workbook data.

---

## ⚠️ Known Limitations

Compared to the original Excel workbook:

1. **No live Excel event model** — recalculation is driven by WinForms actions and save/update flows
2. **Some DATA-sheet formulas remain unresolved** — especially values requiring Excel evaluation of XLOOKUP/LAMBDA/dynamic arrays
3. **Threshold cost and some finishing logic are approximate or partially stubbed**
4. **CRE8 import uses an inferred/best-effort column mapping** until a real CRE8 sample/export specification is available
5. **VBA macro code, pivot cache data, embedded workbook assets, and some hidden-sheet structures are still not directly extractable**

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
