using System;
using System.Windows.Forms;

namespace QuotingTool.Forms
{
    public sealed class NewQuoteDialog : Form
    {
        private readonly TextBox _txtCustomerName;
        private readonly TextBox _txtProjectName;
        private readonly ComboBox _cmbRegion;
        private readonly DateTimePicker _dtpQuoteDate;

        public string CustomerName => _txtCustomerName.Text.Trim();
        public string ProjectName => _txtProjectName.Text.Trim();
        public string SelectedRegion => _cmbRegion.Text.Trim();
        public DateTime QuoteDate => _dtpQuoteDate.Value.Date;

        public NewQuoteDialog()
        {
            Text = "Start New Quote";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 420;
            Height = 230;

            Controls.Add(new Label { Left = 20, Top = 20, Width = 120, Text = "Customer Name" });
            Controls.Add(new Label { Left = 20, Top = 55, Width = 120, Text = "Project Name" });
            Controls.Add(new Label { Left = 20, Top = 90, Width = 120, Text = "Region" });
            Controls.Add(new Label { Left = 20, Top = 125, Width = 120, Text = "Quote Date" });

            _txtCustomerName = new TextBox { Left = 150, Top = 18, Width = 220 };
            _txtProjectName = new TextBox { Left = 150, Top = 53, Width = 220 };
            _cmbRegion = new ComboBox { Left = 150, Top = 88, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbRegion.Items.AddRange(new object[] { "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" });
            _cmbRegion.SelectedIndex = 0;
            _dtpQuoteDate = new DateTimePicker { Left = 150, Top = 123, Width = 220, Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            var btnOk = new Button { Text = "Create", Left = 210, Top = 160, Width = 75, DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancel", Left = 295, Top = 160, Width = 75, DialogResult = DialogResult.Cancel };
            btnOk.Click += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(ProjectName))
                {
                    MessageBox.Show(this, "Customer name and project name are required.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                }
            };

            Controls.AddRange(new Control[] { _txtCustomerName, _txtProjectName, _cmbRegion, _dtpQuoteDate, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
