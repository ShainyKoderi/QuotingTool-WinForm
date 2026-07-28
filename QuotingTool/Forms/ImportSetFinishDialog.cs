using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// "Imported Quote – Set Finish" dialog.
    /// Allows the user to set Finish Type, Finish Applicator, Finish Group, Finish Name,
    /// and Finish SAP Code after a quote import.
    /// </summary>
    public sealed class ImportSetFinishDialog : Form
    {
        private readonly ComboBox _cmbFinishType;
        private readonly ComboBox _cmbFinishApplicator;
        private readonly ComboBox _cmbFinishGroup;
        private readonly ComboBox _cmbFinishName;
        private readonly TextBox  _txtFinishSapCode;
        private readonly List<ColourGroup> _colourGroups;

        public string FinishType      => _cmbFinishType.Text.Trim();
        public string FinishApplicator => _cmbFinishApplicator.Text.Trim();
        public string FinishGroup     => _cmbFinishGroup.Text.Trim();
        public string FinishName      => _cmbFinishName.Text.Trim();
        public string FinishSapCode   => _txtFinishSapCode.Text.Trim();

        public ImportSetFinishDialog(
            IReadOnlyList<ColourGroup> colourGroups,
            string currentFinishType,
            string currentFinishApplicator,
            string currentFinishGroup,
            string currentFinishName,
            string currentFinishSapCode)
        {
            _colourGroups = colourGroups?.ToList() ?? new List<ColourGroup>();

            Text = "Imported Quote – Set Finish";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width  = 460;
            Height = 295;

            const int labelLeft    = 20;
            const int controlLeft  = 165;
            const int controlWidth = 255;
            int y = 20;

            // Finish Type
            Controls.Add(new Label { Left = labelLeft, Top = y + 3, Width = 140, Text = "Finish Type:" });
            _cmbFinishType = new ComboBox { Left = controlLeft, Top = y, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            _cmbFinishType.Items.AddRange(new object[] { "Mill Finish", "Powder", "Anodised" });
            _cmbFinishType.Text = currentFinishType ?? string.Empty;
            Controls.Add(_cmbFinishType);
            y += 35;

            // Finish Applicator
            Controls.Add(new Label { Left = labelLeft, Top = y + 3, Width = 140, Text = "Finish Applicator:" });
            _cmbFinishApplicator = new ComboBox { Left = controlLeft, Top = y, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            _cmbFinishApplicator.Items.AddRange(new object[]
            {
                "n/a",
                "Capral P&P - Bremer (AP)",
                "Capral P&P - Campbellfield (AP)",
                "Capral P&P - Canningvale (PP)",
                "Capral P&P - HWood Non-Bulk (AP)",
                "Capral P&P - HWood Bulk (AP)",
                "Capral Mill (AP)",
                "AAF powdercoat",
                "Powderline WA",
                "Capral/AAF QLD anodising",
                "WA Anodisers",
                "Other - pls check pricing"
            });
            _cmbFinishApplicator.Text = currentFinishApplicator ?? string.Empty;
            Controls.Add(_cmbFinishApplicator);
            y += 35;

            // Finish Group
            Controls.Add(new Label { Left = labelLeft, Top = y + 3, Width = 140, Text = "Finish Group:" });
            _cmbFinishGroup = new ComboBox { Left = controlLeft, Top = y, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            _cmbFinishGroup.Items.Add(string.Empty);
            foreach (var group in _colourGroups
                         .Select(c => c.GroupName)
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .OrderBy(g => g, StringComparer.OrdinalIgnoreCase))
            {
                _cmbFinishGroup.Items.Add(group);
            }
            _cmbFinishGroup.Text = currentFinishGroup ?? string.Empty;
            _cmbFinishGroup.SelectedIndexChanged += (_, _) => RefreshFinishNameOptions();
            Controls.Add(_cmbFinishGroup);
            y += 35;

            // Finish Name
            Controls.Add(new Label { Left = labelLeft, Top = y + 3, Width = 140, Text = "Finish Name:" });
            _cmbFinishName = new ComboBox { Left = controlLeft, Top = y, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            RefreshFinishNameOptions();
            _cmbFinishName.Text = currentFinishName ?? string.Empty;
            _cmbFinishName.SelectedIndexChanged += (_, _) => AutoFillSapCode();
            Controls.Add(_cmbFinishName);
            y += 35;

            // Finish SAP Code
            Controls.Add(new Label { Left = labelLeft, Top = y + 3, Width = 140, Text = "Finish SAP Code:" });
            _txtFinishSapCode = new TextBox { Left = controlLeft, Top = y, Width = controlWidth };
            _txtFinishSapCode.Text = currentFinishSapCode ?? "NON";
            Controls.Add(_txtFinishSapCode);
            y += 42;

            // Done button
            var btnDone = new Button
            {
                Text          = "Done",
                Left          = controlLeft + controlWidth - 80,
                Top           = y,
                Width         = 80,
                DialogResult  = DialogResult.OK
            };
            Controls.Add(btnDone);
            AcceptButton = btnDone;
        }

        private void RefreshFinishNameOptions()
        {
            if (_cmbFinishName == null) return;
            var selected = _cmbFinishName.Text;
            _cmbFinishName.Items.Clear();
            _cmbFinishName.Items.Add(string.Empty);

            var selectedGroup = _cmbFinishGroup?.Text.Trim() ?? string.Empty;
            foreach (var name in _colourGroups
                         .Where(c => string.Equals(c.GroupName, selectedGroup, StringComparison.OrdinalIgnoreCase))
                         .Select(c => c.LongDesc)
                         .Where(v => !string.IsNullOrWhiteSpace(v))
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .OrderBy(v => v, StringComparer.OrdinalIgnoreCase))
            {
                _cmbFinishName.Items.Add(name);
            }

            _cmbFinishName.Text = selected;
        }

        private void AutoFillSapCode()
        {
            if (_cmbFinishName == null || _txtFinishSapCode == null) return;
            var finishName    = _cmbFinishName.Text.Trim();
            var selectedGroup = _cmbFinishGroup?.Text.Trim() ?? string.Empty;

            var colour = _colourGroups.FirstOrDefault(c =>
                string.Equals(c.GroupName, selectedGroup, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.LongDesc,  finishName,    StringComparison.OrdinalIgnoreCase));

            if (colour != null)
                _txtFinishSapCode.Text = colour.ColourCode;
        }
    }
}
