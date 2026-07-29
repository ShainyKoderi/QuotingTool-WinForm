using System;
using System.Collections.Generic;

namespace QuotingTool.Data
{
    // Maps to "1.Project Details" sheet
    public class Project
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public DateTime? RevisionDate { get; set; }
        public decimal LMERate { get; set; }
        public decimal BilletPremium { get; set; }
        // Additional fields from 1.Project Details sheet (extracted from .xlsm)
        public string Region { get; set; } = string.Empty;             // named range: region (A4)
        public string QuotedBy { get; set; } = string.Empty;           // named range: quoted_by (A6)
        public string AccountManager { get; set; } = string.Empty;     // A7
        public string Attention { get; set; } = string.Empty;          // named range: attention (B12)
        public string AttentionTo { get; set; } = "";
        public string QuoteValidity { get; set; } = "30 days";
        public string MarketSegment { get; set; } = string.Empty;      // named range: Market_Segment
        public string ProductSegment { get; set; } = string.Empty;     // named range: product_segment
        public string ProjectNotes { get; set; } = string.Empty;       // A17
        public string AddLocAlLogos { get; set; } = "None";
        public string LocalLogo { get; set; } = "None";                // named range: local_logo (B23)
        public string FinishType { get; set; } = "Mill Finish";        // named range: finish_type_value (B24)
        public string FinishApplicator { get; set; } = string.Empty;   // named range: finish_applicator_value (B26)
        public string FinishGroup { get; set; } = string.Empty;        // named range: finish_group_value (B28)
        public string FinishName { get; set; } = string.Empty;         // named range: finish_name_value (B30)
        public string FinishCustomName { get; set; } = string.Empty;   // named range: Finish_Custom_Name (B31)
        public string FinishSapCode { get; set; } = "NON";             // named range: finish_code_value (E30/E32)
        public string ThresholdChoice { get; set; } = "No";            // named range: Threshold_Choice (E33)
        public bool GenerateMillFinishAlternative { get; set; }
        public bool GenerateMfAlt { get; set; } = false;               // named range: MF_ALT (E24)
        public decimal DeliveryCharge { get; set; } = 97m;
        public decimal CustomerSapDiscount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Maps to "2.Quote" sheet rows (6 page blocks × 40 rows)
    // Column headers (row 11): B=Ln, C=Section, D=Description, E=Alloy, F=Temper,
    //   G=Qty, H=Length(m), I=$ Each, J=$ Total, K=Slotting/Sundry Charge(ea),
    //   L=Ignore Finish, M=SAP Colour Code, V=SS?
    public class QuoteLine
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PageBlock { get; set; }      // 1-6
        public int RowInBlock { get; set; }     // 1-40
        public string ExtrusionCode { get; set; } = string.Empty;   // col C (Section)
        public string Description { get; set; } = string.Empty;     // col D (auto via VLOOKUP)
        public decimal Quantity { get; set; }   // col G
        public decimal LengthM { get; set; }    // col H (metres)
        public decimal Price { get; set; }      // col I: =IFERROR(AH+AI+AS+K,"")
        public decimal SellPrice { get; set; }  // col J: =IFERROR(I*G,"")
        public bool SubsillFlag { get; set; }   // col V (SS?)
        public decimal SubsillSurcharge { get; set; } // col K: =IFERROR(AZ+BC,"")
        // Column M – SAP Colour Code (extracted from .xlsm row 12):
        // =IF(C="","", IF(AND(BF>0, NOT(ThresholdChoice="No")),
        //   VLOOKUP(ThresholdChoice, Threshold_Code, 2, FALSE),
        //   IF(L="No", SAP_Colour_Code, "NON")))
        public string SapColourCode { get; set; } = string.Empty;   // col M
        public string IgnoreFinish { get; set; } = "No";            // col L
        public string Alloy { get; set; } = string.Empty;           // col E (auto)
        public string Temper { get; set; } = string.Empty;          // col F (auto)
        public decimal TotalPrice { get; set; }
        public int LineNumber { get; set; }
        public decimal? ManualPriceOverride { get; set; }
        // Col AV: IP (Ingress Protection) grade for this line (0 = none, 1-3 = IP1/IP2/IP3 panoramic).
        // Auto-computed from the section's PH code via IPMasterData; can be overridden by the user.
        public int IpGrade { get; set; }
    }

    // Maps to named ranges pg1_foot .. pg6_foot
    public class QuoteFooter
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PageNumber { get; set; }     // 1-6
        public string FontColor { get; set; } = "#FFFFFF";
    }

    // Print settings per project/sheet
    public class PrintSetting
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string SheetName { get; set; } = string.Empty;
        public int PageCount { get; set; } = 1;
        public int LastRow { get; set; }
        public string PrintAreaCols { get; set; } = "A:J";
    }

    // Maps to "3.Edit Pricing" sheet
    // Key cells: C16=FinType, C25=CostType, D25=Override,
    //   B33=MetalSellPrice, B42=POACost, B43=FinishingCostPrice(auto),
    //   B44=FinishingSellPrice, B48=MillPOACost, B50=BulkSellPrice,
    //   B52=NonBulkPOACost, B54=NonBulkSellPrice, B57=ThresholdCostPrice(auto), B58=ThresholdSellPrice,
    //   B36=TBCalcType, B37=TBMargin
    public class EditPricing
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CostType { get; set; }           // C25: 0=Standard,1=POA,2=Custom,4=Bremer Mill Std,5=Bremer Mill POA
        public int FinType { get; set; }            // C16: 0=Not Powder, 1=Powder
        public bool CostTypeOverride { get; set; }  // D25
        public decimal MetalPrice { get; set; }     // B33: Mill Finish Sell Price $/kg
        public decimal FinishingPrice { get; set; } // B43: Finishing Cost Price $/m2 (auto from PricingMatrix)
        public decimal Price_B42 { get; set; } = 99; // B42: POA/Other Vendor Cost $/m2
        public decimal Price_B44 { get; set; } = 99; // B44: Finishing Sell Price $/m2
        public decimal Price_B48 { get; set; } = 99; // B48: Mill POA Cost $/m2
        public decimal Price_B50 { get; set; } = 99; // B50: Mill BULK Sell Price $/m2
        // Additional fields from .xlsm
        public decimal Price_B52 { get; set; } = 99;  // B52: Mill NON-Bulk POA Cost $/m2
        public decimal Price_B54 { get; set; } = 99;  // B54: Mill NON-Bulk Sell Price $/m2
        public decimal Price_B57 { get; set; } = 0m;  // B57: Anodised Threshold Cost Price $/m2 (auto from PricingMatrix)
        public decimal Price_B58 { get; set; } = 99;  // B58: Anodised Threshold Sell Price $/m2
        public string ThermalBreakCalcType { get; set; } = "Margin"; // B36: "Margin" or "List less Discount"
        public decimal ThermalBreakMargin { get; set; } = 0.5m;      // B37
        public string PricingNotes { get; set; } = string.Empty;
        public decimal TotalKg { get; set; }
        public decimal TotalFinishingM2 { get; set; }
        public int TotalPieces { get; set; }
        public decimal IP1UpchargePerKg { get; set; } = 1.00m;
        public decimal IP2UpchargePerKg { get; set; } = 2.00m;
        public decimal IP3PanoramicFixedKgRate { get; set; } = 25.85m;
        public decimal IP1MarginPct { get; set; }
        public decimal IP2MarginPct { get; set; }
        public decimal IP3MarginPct { get; set; }
        public decimal MillFinishMarginPct { get; set; }
        public decimal FinishingMarginPct { get; set; }
        public decimal FinishingCostPricePerM2 { get; set; }
        public decimal AnodisedThresholdCostPerM2 { get; set; } = 10.65m;
        public decimal ThermalBreakMarginPct { get; set; }
    }

    public class SundryItem
    {
        public int Id { get; set; }
        public string SectionCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MassKgPerM { get; set; }
        public int Ap { get; set; }
        public int Pp { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum UserRole
    {
        GeneralUser = 0,
        SalesManager = 1,
        Administrator = 2
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public UserRole Role { get; set; } = UserRole.GeneralUser;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }

    public class QuoteSummary
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public decimal MillFinishTotal { get; set; }
        public decimal MillFinishMarginDollar { get; set; }
        public decimal MillFinishMarginPct { get; set; }
        public decimal ThermalBreakMetalTotal { get; set; }
        public decimal ThermalBreakMarginDollar { get; set; }
        public decimal ThermalBreakMarginPct { get; set; }
        public decimal SlottingSundryTotal { get; set; }
        public decimal SlottingSundryMarginDollar { get; set; }
        public decimal SlottingSundryMarginPct { get; set; }
        public decimal FinishingTotal { get; set; }
        public decimal FinishingMarginDollar { get; set; }
        public decimal FinishingMarginPct { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal GrandMarginDollar { get; set; }
        public decimal GrandMarginPct { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Maps to "BOM_Import" sheet
    public class BomLine
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int RowNumber { get; set; }
        public string ExtrusionCode { get; set; } = string.Empty;   // col B
        public string Description { get; set; } = string.Empty;     // col C
        public decimal Quantity { get; set; }   // col D
        public decimal LengthM { get; set; }    // col E (metres, after mm->m conversion)
        public string Finish { get; set; } = string.Empty;          // col G
        public decimal ImportedPrice { get; set; } // col I
        public bool SubsillFlag { get; set; }   // col V
    }

    // Maps to "Revision Control" sheet
    public class RevisionControl
    {
        public int Id { get; set; }
        public DateTime? RevisionDate { get; set; }
        public decimal LMERate { get; set; }
        public decimal BilletPremium { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Pricing matrix for colour/finish cost lookups (legacy – kept for compatibility)
    public class PricingMatrix
    {
        public int Id { get; set; }
        public string ColourGroup { get; set; } = string.Empty;
        public string FinishType { get; set; } = string.Empty;
        public int CostType { get; set; }
        public decimal BaseRate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Maps to "DATA" sheet / "Table5" Excel table (Section master lookup)
    // Used to populate the Section dropdown on the Quote tab and to
    // auto-fill Description/Alloy/Temper when a Section is selected.
    public class ExtrusionMaster
    {
        public string Section { get; set; } = string.Empty;       // col A
        public string Description { get; set; } = string.Empty;   // col B (Basic material)
        public decimal MassPerM { get; set; }                     // col D (Mass / M, kg/m)
        public string DefaultAlloy { get; set; } = "6060";
        public string DefaultTemper { get; set; } = "T5";
        public decimal ApMm { get; set; }                         // col E (AP per metre, mm units)
        public decimal PpMm { get; set; }                         // col F (PP per metre, mm units)
        public int SubsillTier { get; set; }                      // col K (0 = none, 1-4 = subsill tier)
        public string ThermalBreakType { get; set; } = "No";      // col M ("No", "PLS", or "PDB")
        public string? PhCode { get; set; }                       // col H (18-digit SAP PH code)
    }

    // Maps the SAP PH (Product Hierarchy) code to its Ingress Protection (IP) category.
    // Source: 5.Edit_PH_IP sheet cols B (PH) and D (IP Category: 0=none,1=IP1,2=IP2,3=IP3).
    // Used on the Quote tab to auto-determine the IP upcharge grade per section.
    public class IPMasterData
    {
        public string PhCode { get; set; } = string.Empty;   // 18-digit SAP PH code
        public int IpCategory { get; set; }                  // 0 = none, 1 = IP1, 2 = IP2, 3 = IP3
    }

    // Section codes that appear in the THRESHOLDS named range (Validation!$N:$N).
    // A section in this list triggers the anodised-threshold SAP colour code logic (col BF = 1).
    public class ThresholdSection
    {
        public string Section { get; set; } = string.Empty;
    }

    // Master formula backup (col BA in original Excel)
    public class MasterFormula
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PageBlock { get; set; }
        public int RowInBlock { get; set; }
        public string FormulaExpression { get; set; } = string.Empty;
    }

    // Sheet lock state (used by ProtectionHelper)
    public class SheetLock
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string SheetName { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = true;
        public DateTime UpdatedAt { get; set; }
    }

    // ── Reference / Lookup models (from .xlsm) ───────────────────────────────

    // Maps to "Colour_Groups" sheet (VeryHidden in VBA)
    // Named ranges: COLOURS (A4:D94), colour_code (C4:D94), GRP1_Long..GrpPOA_Long
    public class ColourGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;   // col A: e.g. "GROUP 1", "n/a", "GROUP POA"
        public string ShortDesc { get; set; } = string.Empty;   // col B
        public string LongDesc { get; set; } = string.Empty;    // col C (used in dropdown long name)
        public string ColourCode { get; set; } = string.Empty;  // col D: SAP code e.g. "GR", "NON"
    }

    // Maps to rows from the "Validation" sheet (dropdown list data)
    public class ValidationItem
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;   // e.g. "MarketSegment", "Region"
        public string Value { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }

    // Maps to "Pricing Matrix" rows 7-13 – metal cost breakpoints (Table_Metal_BP)
    public class MetalBreakpoint
    {
        public int Id { get; set; }
        public decimal BreakpointKg { get; set; }
        public decimal Spread { get; set; }
        public decimal TypicalPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinFactor { get; set; }
        public decimal MaxFactor { get; set; }
    }

    // Maps to "Pricing Matrix" rows 15-21 – thermal break margin breakpoints (Table_TB_BP)
    public class ThermalBreakBreakpoint
    {
        public int Id { get; set; }
        public decimal BreakpointKg { get; set; }
        public decimal MarginTyp { get; set; }
        public decimal MarginMin { get; set; }
        public decimal MarginMax { get; set; }
        public decimal ListTyp { get; set; }
        public decimal ListMin { get; set; }
        public decimal ListMax { get; set; }
    }

    // Maps to "Pricing Matrix" rows 27-40 – powder/anodised cost table per vendor/group
    public class PowderCostRow
    {
        public int Id { get; set; }
        public string FinishCategory { get; set; } = string.Empty; // "Powder" or "Anodised"
        public string Vendor { get; set; } = string.Empty;
        public string ApPp { get; set; } = string.Empty;           // "AP" or "PP"
        public decimal Group1Rate { get; set; }
        public decimal Group2Rate { get; set; }
        public decimal Group3Rate { get; set; }
        public decimal Group4Rate { get; set; }
        public decimal Group5Rate { get; set; }
        public decimal Anodised20umRate { get; set; }
        public decimal Anodised25umRate { get; set; }
        public decimal GroupPOARate { get; set; } = 99m;
        public DateTime UpdatedAt { get; set; }
    }

    // Maps to "Pricing Matrix" rows 52-60 – finish margin/price breakpoints
    public class FinishBreakpoint
    {
        public int Id { get; set; }
        public decimal BreakpointAP { get; set; }   // AP m2 threshold
        public decimal BreakpointPP { get; set; }   // PP m2 threshold
        public decimal RangeA_Typical { get; set; }
        public decimal RangeA_Min { get; set; }
        public decimal RangeA_Max { get; set; }
        public decimal RangeB_Typical { get; set; }
        public decimal RangeB_Min { get; set; }
        public decimal RangeB_Max { get; set; }
        public decimal RangeC_Typical { get; set; }
        public decimal RangeC_Min { get; set; }
        public decimal RangeC_Max { get; set; }
        public decimal RangeD_Typical { get; set; }
        public decimal RangeD_Min { get; set; }
        public decimal RangeD_Max { get; set; }
    }

    // Maps to "Pricing Matrix" rows 63-68 – SubSill tier prices (SubSill_Tier_Price A64:C68)
    public class SubsillTierPrice
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public decimal SellPricePerMetre { get; set; }
        public decimal CostPerMetre { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    // Maps to "Pricing Matrix" rows 1-6 – global metal/delivery constants that feed
    // the MetalPricePerKg / $ Each calculation chain (B3-B5, F3-F5, H3, K5)
    public class PricingConstants
    {
        public int Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal LmeBilletPremium { get; set; }      // B3
        public decimal InternalSpread { get; set; }        // B4
        public decimal MetalCostPerKg { get; set; }         // B5 = B3+B4
        public decimal ExtraSpread { get; set; }           // F3
        public decimal PdbMetalCostBase { get; set; }        // F4 = B5+F3
        public decimal AlfCostPerMetre { get; set; }         // F5
        public decimal StandardDeliveryCharge { get; set; } = 97m; // H3
        public decimal ApToPpFactor { get; set; } = 1.9m;    // K5
        public string? Notes { get; set; }
    }

    // Maps to "DATA_EDGE" sheet – EDGE material reference to SAP material number cross-reference
    public class DataEdgeMaterial
    {
        public int Id { get; set; }
        public string MatRefNo { get; set; } = string.Empty;
        public string MatRefDescription { get; set; } = string.Empty;
        public string SapMatNo { get; set; } = string.Empty;
    }
}
