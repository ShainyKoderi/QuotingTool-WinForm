using System;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the global Pricing Matrix constants
    /// (LME/billet premium, spreads, ALF cost, delivery charge, AP/PP factor).
    /// Mirrors Pricing Matrix rows 1-6 (B3-B5, F3-F5, H3, K5).
    /// </summary>
    public partial class PricingConstantsForm : Form
    {
        private readonly DatabaseContext _db;

        private NumericUpDown numLmeBilletPremium = null!;
        private NumericUpDown numInternalSpread = null!;
        private NumericUpDown numExtraSpread = null!;
        private NumericUpDown numAlfCostPerMetre = null!;
        private NumericUpDown numStandardDeliveryCharge = null!;
        private NumericUpDown numApToPpFactor = null!;
        private Label lblMetalCostPerKg = null!;
        private Label lblPdbMetalCostBase = null!;
        private TextBox txtNotes = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        public PricingConstantsForm(DatabaseContext db)
        {
            _db = db;
            InitializeComponent();
            LoadValues();
        }

        private void InitializeComponent()
        {
            Text = "Edit Pricing Constants";
            ClientSize = new System.Drawing.Size(420, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 9,
                Padding = new Padding(12),
                AutoSize = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));

            numLmeBilletPremium = MakeNumeric();
            numInternalSpread = MakeNumeric();
            numExtraSpread = MakeNumeric();
            numAlfCostPerMetre = MakeNumeric();
            numStandardDeliveryCharge = MakeNumeric();
            numApToPpFactor = MakeNumeric();
            lblMetalCostPerKg = new Label { AutoSize = true, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            lblPdbMetalCostBase = new Label { AutoSize = true, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            txtNotes = new TextBox { Width = 150, Multiline = true, Height = 50 };

            AddRow(layout, "LME + Billet Premium ($/kg):", numLmeBilletPremium);
            AddRow(layout, "Internal Spread ($/kg):", numInternalSpread);
            AddRow(layout, "Metal Cost ($/kg) = above two:", lblMetalCostPerKg);
            AddRow(layout, "Extra Spread ($/kg):", numExtraSpread);
            AddRow(layout, "PDB Metal Cost Base (Metal Cost + Extra Spread):", lblPdbMetalCostBase);
            AddRow(layout, "ALF Cost ($/metre):", numAlfCostPerMetre);
            AddRow(layout, "Standard Delivery Charge ($):", numStandardDeliveryCharge);
            AddRow(layout, "AP to PP Factor:", numApToPpFactor);
            AddRow(layout, "Notes:", txtNotes);

            numLmeBilletPremium.ValueChanged += (_, _) => RecalculateDerived();
            numInternalSpread.ValueChanged += (_, _) => RecalculateDerived();
            numExtraSpread.ValueChanged += (_, _) => RecalculateDerived();

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(140, 340), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(250, 340), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(layout);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private static NumericUpDown MakeNumeric() => new()
        {
            DecimalPlaces = 4,
            Maximum = 100000,
            Minimum = -100000,
            Increment = 0.01m,
            Width = 120
        };

        private static void AddRow(TableLayoutPanel layout, string label, Control control)
        {
            layout.Controls.Add(new Label { Text = label, AutoSize = true, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            layout.Controls.Add(control);
        }

        private void LoadValues()
        {
            var pc = _db.GetLatestPricingConstants() ?? new PricingConstants
            {
                LmeBilletPremium = 5.61m,
                InternalSpread = 3.12m,
                MetalCostPerKg = 8.73m,
                ExtraSpread = 0.35m,
                PdbMetalCostBase = 9.08m,
                AlfCostPerMetre = 6.46m,
                StandardDeliveryCharge = 97m,
                ApToPpFactor = 1.9m
            };

            numLmeBilletPremium.Value = pc.LmeBilletPremium;
            numInternalSpread.Value = pc.InternalSpread;
            numExtraSpread.Value = pc.ExtraSpread;
            numAlfCostPerMetre.Value = pc.AlfCostPerMetre;
            numStandardDeliveryCharge.Value = pc.StandardDeliveryCharge;
            numApToPpFactor.Value = pc.ApToPpFactor;
            txtNotes.Text = pc.Notes ?? string.Empty;

            RecalculateDerived();
        }

        private void RecalculateDerived()
        {
            var metalCost = numLmeBilletPremium.Value + numInternalSpread.Value;
            var pdbBase = metalCost + numExtraSpread.Value;
            lblMetalCostPerKg.Text = metalCost.ToString("N4");
            lblPdbMetalCostBase.Text = pdbBase.ToString("N4");
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var pc = new PricingConstants
            {
                EffectiveDate = DateTime.Today,
                LmeBilletPremium = numLmeBilletPremium.Value,
                InternalSpread = numInternalSpread.Value,
                MetalCostPerKg = numLmeBilletPremium.Value + numInternalSpread.Value,
                ExtraSpread = numExtraSpread.Value,
                PdbMetalCostBase = numLmeBilletPremium.Value + numInternalSpread.Value + numExtraSpread.Value,
                AlfCostPerMetre = numAlfCostPerMetre.Value,
                StandardDeliveryCharge = numStandardDeliveryCharge.Value,
                ApToPpFactor = numApToPpFactor.Value,
                Notes = txtNotes.Text
            };

            _db.InsertPricingConstants(pc);
        }
    }
}
