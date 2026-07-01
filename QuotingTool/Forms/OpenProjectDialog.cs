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
        private readonly List<Project> _projects;

        public OpenProjectDialog(DatabaseContext db)
        {
            _db = db;
            _projects = new List<Project>(_db.GetAllProjects());

            Text = "Open Project";
            Size = new System.Drawing.Size(500, 400);
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

            lstProjects = new ListBox
            {
                Location = new System.Drawing.Point(12, 36),
                Size = new System.Drawing.Size(460, 280)
            };

            foreach (var p in _projects)
                lstProjects.Items.Add($"[{p.Id}] {p.CustomerName} – {p.ProjectName}  ({p.UpdatedAt:d})");

            var btnOk = new Button
            {
                Text = "Open",
                Location = new System.Drawing.Point(300, 330),
                Size = new System.Drawing.Size(80, 28),
                DialogResult = DialogResult.OK
            };
            btnOk.Click += (_, __) =>
            {
                int idx = lstProjects.SelectedIndex;
                if (idx >= 0 && idx < _projects.Count)
                    SelectedProjectId = _projects[idx].Id;
                Close();
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(390, 330),
                Size = new System.Drawing.Size(80, 28),
                DialogResult = DialogResult.Cancel
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;
            Controls.AddRange(new Control[] { lblInstruction, lstProjects, btnOk, btnCancel });
        }
    }
}
