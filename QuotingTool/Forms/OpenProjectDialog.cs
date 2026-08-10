using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Simple dialog for selecting an existing project to open.
    /// </summary>
    public class OpenProjectDialog : Form
    {
        public int SelectedProjectId { get; private set; }

        private readonly DatabaseContext _db;
        private readonly ListBox lstProjects;
        private readonly TextBox txtFilter;
        private readonly List<Project> _projects;
        private List<Project> _filtered;

        public OpenProjectDialog(DatabaseContext db)
        {
            _db = db;
            _projects = new List<Project>(_db.GetAllProjects());
            _filtered = _projects;

            Text = "Open Project";
            Size = new System.Drawing.Size(500, 430);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblInstruction = new Label
            {
                Text = "Select a project to open:",
                AutoSize = true,
                Location = new System.Drawing.Point(12, 12)
            };

            var lblFilter = new Label
            {
                Text = "Filter:",
                AutoSize = true,
                Location = new System.Drawing.Point(12, 39)
            };

            txtFilter = new TextBox
            {
                Location = new System.Drawing.Point(56, 36),
                Size = new System.Drawing.Size(416, 23),
                PlaceholderText = "Search by customer, project, region or ID..."
            };
            txtFilter.TextChanged += (_, __) => ApplyFilter();

            lstProjects = new ListBox
            {
                Location = new System.Drawing.Point(12, 66),
                Size = new System.Drawing.Size(460, 280)
            };
            lstProjects.DoubleClick += (_, __) => OpenSelected();

            PopulateList();

            var btnOk = new Button
            {
                Text = "Open",
                Location = new System.Drawing.Point(300, 356),
                Size = new System.Drawing.Size(80, 28)
            };
            btnOk.Click += (_, __) => OpenSelected();

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(390, 356),
                Size = new System.Drawing.Size(80, 28),
                DialogResult = DialogResult.Cancel
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;
            Controls.AddRange(new Control[] { lblInstruction, lblFilter, txtFilter, lstProjects, btnOk, btnCancel });
        }

        private void ApplyFilter()
        {
            var term = txtFilter.Text.Trim();
            _filtered = string.IsNullOrEmpty(term)
                ? _projects
                : _projects.FindAll(p =>
                    p.Id.ToString().Contains(term, StringComparison.OrdinalIgnoreCase)
                    || (p.CustomerName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (p.ProjectName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (p.Region?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false));
            PopulateList();
        }

        private void PopulateList()
        {
            lstProjects.BeginUpdate();
            lstProjects.Items.Clear();
            foreach (var p in _filtered)
                lstProjects.Items.Add($"[{p.Id}] {p.CustomerName} – {p.ProjectName}  ({p.Region})  ({p.UpdatedAt:d})");
            lstProjects.EndUpdate();
        }

        private void OpenSelected()
        {
            int idx = lstProjects.SelectedIndex;
            if (idx < 0 || idx >= _filtered.Count)
                return;
            SelectedProjectId = _filtered[idx].Id;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
