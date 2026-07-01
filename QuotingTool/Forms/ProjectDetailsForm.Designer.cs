namespace QuotingTool.Forms
{
    partial class ProjectDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle       = new System.Windows.Forms.Label();
            this.lblCustomer    = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblProject     = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.lblRevDate     = new System.Windows.Forms.Label();
            this.dtpRevisionDate = new System.Windows.Forms.DateTimePicker();
            this.lblLME         = new System.Windows.Forms.Label();
            this.txtLMERate     = new System.Windows.Forms.TextBox();
            this.lblBillet      = new System.Windows.Forms.Label();
            this.txtBilletPremium = new System.Windows.Forms.TextBox();
            this.btnSave        = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 30);
            this.lblTitle.Text = "1. Project Details";

            // Customer Name
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(20, 70);
            this.lblCustomer.Text = "Customer Name:";

            this.txtCustomerName.Location = new System.Drawing.Point(160, 67);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(350, 23);

            // Project Name
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(20, 110);
            this.lblProject.Text = "Project Name:";

            this.txtProjectName.Location = new System.Drawing.Point(160, 107);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(350, 23);

            // Revision Date
            this.lblRevDate.AutoSize = true;
            this.lblRevDate.Location = new System.Drawing.Point(20, 150);
            this.lblRevDate.Text = "Revision Date:";

            this.dtpRevisionDate.Location = new System.Drawing.Point(160, 147);
            this.dtpRevisionDate.Name = "dtpRevisionDate";
            this.dtpRevisionDate.Size = new System.Drawing.Size(200, 23);
            this.dtpRevisionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // LME Rate (read-only)
            this.lblLME.AutoSize = true;
            this.lblLME.Location = new System.Drawing.Point(20, 190);
            this.lblLME.Text = "LME Rate (read-only):";

            this.txtLMERate.Location = new System.Drawing.Point(160, 187);
            this.txtLMERate.Name = "txtLMERate";
            this.txtLMERate.ReadOnly = true;
            this.txtLMERate.BackColor = System.Drawing.SystemColors.Control;
            this.txtLMERate.Size = new System.Drawing.Size(150, 23);

            // Billet Premium (read-only)
            this.lblBillet.AutoSize = true;
            this.lblBillet.Location = new System.Drawing.Point(20, 230);
            this.lblBillet.Text = "Billet Premium (read-only):";

            this.txtBilletPremium.Location = new System.Drawing.Point(160, 227);
            this.txtBilletPremium.Name = "txtBilletPremium";
            this.txtBilletPremium.ReadOnly = true;
            this.txtBilletPremium.BackColor = System.Drawing.SystemColors.Control;
            this.txtBilletPremium.Size = new System.Drawing.Size(150, 23);

            // Save button
            this.btnSave.Location = new System.Drawing.Point(160, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblCustomer, this.txtCustomerName,
                this.lblProject, this.txtProjectName,
                this.lblRevDate, this.dtpRevisionDate,
                this.lblLME, this.txtLMERate,
                this.lblBillet, this.txtBilletPremium,
                this.btnSave
            });
            this.Name = "ProjectDetailsForm";
            this.Text = "Project Details";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblRevDate;
        private System.Windows.Forms.DateTimePicker dtpRevisionDate;
        private System.Windows.Forms.Label lblLME;
        private System.Windows.Forms.TextBox txtLMERate;
        private System.Windows.Forms.Label lblBillet;
        private System.Windows.Forms.TextBox txtBilletPremium;
        private System.Windows.Forms.Button btnSave;
    }
}
