using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 1 – Project Details
    /// Displays and edits Customer Name, Project Name, Revision Date.
    /// Shows LME Rate and Billet Premium as read-only (set during Monthly Update).
    /// </summary>
    public partial class ProjectDetailsForm : Form
    {
        private readonly DatabaseContext _db;
        private int _projectId;

        public ProjectDetailsForm(DatabaseContext db)
        {
            _db = db;
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var project = _db.GetProject(projectId);
            if (project == null) return;

            txtCustomerName.Text = project.CustomerName;
            txtProjectName.Text  = project.ProjectName;
            dtpRevisionDate.Value = project.RevisionDate ?? DateTime.Today;
            txtLMERate.Text      = project.LMERate.ToString("F4");
            txtBilletPremium.Text = project.BilletPremium.ToString("F4");
        }

        public void SaveProject()
        {
            if (_projectId <= 0) return;

            var project = _db.GetProject(_projectId) ?? new Project { Id = _projectId };
            project.CustomerName = txtCustomerName.Text.Trim();
            project.ProjectName  = txtProjectName.Text.Trim();
            project.RevisionDate = dtpRevisionDate.Value.Date;
            _db.UpdateProject(project);

            MessageBox.Show("Project details saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveProject();
    }
}
