# Migration plan: WinForms (.NET 8) -> Blazor Web App (.NET 10)

Solution layout: `QuotingTool.Core` (data, services, models), `QuotingTool.Web` (Blazor Web App, interactive server),
`LegacyWinForms` (old UI kept as reference; not in the solution; delete in Phase 7).

| Phase | Scope | Status |
|---|---|---|
| 1 | Extract UI-free `QuotingTool.Core` (net10): Data, Services, Helpers, logos. Removed WinForms deps (clipboard, Bitmap resources, tab navigation) | Done |
| 2 | Scaffold `QuotingTool.Web`: DI, cookie auth (uses `AuthService`), login/logout, layout, home, project list | Done |
| 3 | Startup tasks: `EnsureSchemaExists` + default admin creation (without MessageBox), replace static `UserSession` with `AuthenticationState`, role policies | Todo |
| 4 | Project workflow: New/Open project, ProjectDetails, QuoteForm, QuoteNoPricing, QuoteLetter pages | Todo |
| 5 | Pricing: EditPricing, MillFinishQuote, Sundry items, BOM import, pricing/Cre8 import (file upload via `InputFile`) | Todo |
| 6 | Admin/master data pages: Users, breakpoints (metal/finish/thermal break), powder cost, subsill tiers, constants, monthly update | Todo |
| 7 | Exports (PDF/Excel/SAP text as downloads), tests, remove `LegacyWinForms`, deployment | Todo |

Notes: Connection string lives in `QuotingTool.Web/appsettings.json` (use user-secrets/env vars in production).
Open `QuotingTool.sln` in Visual Studio 2026.
