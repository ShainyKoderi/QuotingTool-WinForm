namespace QuotingTool.Forms
{
    partial class BomImportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle           = new System.Windows.Forms.Label();
            this.lblCsvPath         = new System.Windows.Forms.Label();
            this.txtCsvPath         = new System.Windows.Forms.TextBox();
            this.btnBrowseCsv       = new System.Windows.Forms.Button();
            this.btnImportDistribute = new System.Windows.Forms.Button();
            this.btnGetFinishes     = new System.Windows.Forms.Button();
            this.btnImportPrices    = new System.Windows.Forms.Button();
            this.lblRowCount        = new System.Windows.Forms.Label();
            this.lblPagesNeeded     = new System.Windows.Forms.Label();
            this.lblMostCommonFinish = new System.Windows.Forms.Label();
            this.gridBom            = new System.Windows.Forms.DataGridView();
            this.pnlTop             = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)this.gridBom).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.Text = "4. BOM Import";

            // CSV path
            this.lblCsvPath.AutoSize = true;
            this.lblCsvPath.Location = new System.Drawing.Point(10, 50);
            this.lblCsvPath.Text = "CSV File:";

            this.txtCsvPath.Location = new System.Drawing.Point(80, 47);
            this.txtCsvPath.Name = "txtCsvPath";
            this.txtCsvPath.ReadOnly = true;
            this.txtCsvPath.Size = new System.Drawing.Size(450, 23);

            this.btnBrowseCsv.Location = new System.Drawing.Point(540, 47);
            this.btnBrowseCsv.Name = "btnBrowseCsv";
            this.btnBrowseCsv.Size = new System.Drawing.Size(110, 25);
            this.btnBrowseCsv.Text = "Browse CSV...";
            this.btnBrowseCsv.Click += new System.EventHandler(this.btnBrowseCsv_Click);

            // Action buttons
            this.btnImportDistribute.Location = new System.Drawing.Point(10, 82);
            this.btnImportDistribute.Name = "btnImportDistribute";
            this.btnImportDistribute.Size = new System.Drawing.Size(200, 28);
            this.btnImportDistribute.Text = "Import & Distribute to Quote";
            this.btnImportDistribute.Click += new System.EventHandler(this.btnImportDistribute_Click);

            this.btnGetFinishes.Location = new System.Drawing.Point(220, 82);
            this.btnGetFinishes.Name = "btnGetFinishes";
            this.btnGetFinishes.Size = new System.Drawing.Size(180, 28);
            this.btnGetFinishes.Text = "Get Unique Finishes";
            this.btnGetFinishes.Click += new System.EventHandler(this.btnGetFinishes_Click);

            this.btnImportPrices.Location = new System.Drawing.Point(410, 82);
            this.btnImportPrices.Name = "btnImportPrices";
            this.btnImportPrices.Size = new System.Drawing.Size(180, 28);
            this.btnImportPrices.Text = "Import Prices from BOM";
            this.btnImportPrices.Click += new System.EventHandler(this.btnImportPrices_Click);

            // Info labels
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point(10, 118);
            this.lblRowCount.Text = "Rows: —";

            this.lblPagesNeeded.AutoSize = true;
            this.lblPagesNeeded.Location = new System.Drawing.Point(120, 118);
            this.lblPagesNeeded.Text = "Pages needed: —";

            this.lblMostCommonFinish.AutoSize = true;
            this.lblMostCommonFinish.Location = new System.Drawing.Point(260, 118);
            this.lblMostCommonFinish.Text = "Most common finish: —";

            // pnlTop
            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblCsvPath, this.txtCsvPath, this.btnBrowseCsv,
                this.btnImportDistribute, this.btnGetFinishes, this.btnImportPrices,
                this.lblRowCount, this.lblPagesNeeded, this.lblMostCommonFinish
            });
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 150;
            this.pnlTop.Name = "pnlTop";

            // gridBom
            this.gridBom.AllowUserToAddRows = false;
            this.gridBom.AllowUserToDeleteRows = false;
            this.gridBom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBom.Name = "gridBom";
            this.gridBom.ReadOnly = true;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 700);
            this.Controls.Add(this.gridBom);
            this.Controls.Add(this.pnlTop);
            this.Name = "BomImportForm";
            this.Text = "BOM Import";

            ((System.ComponentModel.ISupportInitialize)this.gridBom).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCsvPath;
        private System.Windows.Forms.TextBox txtCsvPath;
        private System.Windows.Forms.Button btnBrowseCsv;
        private System.Windows.Forms.Button btnImportDistribute;
        private System.Windows.Forms.Button btnGetFinishes;
        private System.Windows.Forms.Button btnImportPrices;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label lblPagesNeeded;
        private System.Windows.Forms.Label lblMostCommonFinish;
        private System.Windows.Forms.DataGridView gridBom;
        private System.Windows.Forms.Panel pnlTop;
    }
}
