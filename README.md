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
