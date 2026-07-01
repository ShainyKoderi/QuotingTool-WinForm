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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Maps to "2.Quote" sheet rows (6 page blocks × 40 rows)
    public class QuoteLine
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PageBlock { get; set; }      // 1-6
        public int RowInBlock { get; set; }     // 1-40
        public string ExtrusionCode { get; set; } = string.Empty;   // col C
        public string Description { get; set; } = string.Empty;     // col D
        public decimal Quantity { get; set; }   // col G
        public decimal LengthM { get; set; }    // col H (metres)
        public decimal Price { get; set; }      // col I
        public decimal SellPrice { get; set; }  // col J
        public bool SubsillFlag { get; set; }   // col V
        public decimal SubsillSurcharge { get; set; } // col K
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
    public class EditPricing
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CostType { get; set; }       // C25 (0=Standard,1=POA Colour,2=Custom Supplier,4=Bremer Mill Std,5=Bremer Mill POA)
        public int FinType { get; set; }        // C16 (0=Not Powder,1=Powder)
        public bool CostTypeOverride { get; set; }  // D25
        public decimal MetalPrice { get; set; }     // B33
        public decimal FinishingPrice { get; set; } // B43
        public decimal Price_B42 { get; set; } = 99;
        public decimal Price_B44 { get; set; } = 99;
        public decimal Price_B48 { get; set; } = 99;
        public decimal Price_B50 { get; set; } = 99;
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

    // Pricing matrix for colour/finish cost lookups
    public class PricingMatrix
    {
        public int Id { get; set; }
        public string ColourGroup { get; set; } = string.Empty;
        public string FinishType { get; set; } = string.Empty;
        public int CostType { get; set; }
        public decimal BaseRate { get; set; }
        public DateTime UpdatedAt { get; set; }
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
}
