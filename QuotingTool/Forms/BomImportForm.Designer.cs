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
            lblTitle = new Label();
            lblCsvPath = new Label();
            txtCsvPath = new TextBox();
            btnBrowseCsv = new Button();
            btnImportDistribute = new Button();
            btnGetFinishes = new Button();
            btnImportPrices = new Button();
            lblRowCount = new Label();
            lblPagesNeeded = new Label();
            lblMostCommonFinish = new Label();
            gridBom = new DataGridView();
            pnlTop = new Panel();
            ((System.ComponentModel.ISupportInitialize)gridBom).BeginInit();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BOM Import";
            // 
            // lblCsvPath
            // 
            lblCsvPath.AutoSize = true;
            lblCsvPath.Location = new Point(10, 50);
            lblCsvPath.Name = "lblCsvPath";
            lblCsvPath.Size = new Size(52, 15);
            lblCsvPath.TabIndex = 1;
            lblCsvPath.Text = "CSV File:";
            // 
            // txtCsvPath
            // 
            txtCsvPath.Location = new Point(80, 47);
            txtCsvPath.Name = "txtCsvPath";
            txtCsvPath.ReadOnly = true;
            txtCsvPath.Size = new Size(450, 23);
            txtCsvPath.TabIndex = 2;
            // 
            // btnBrowseCsv
            // 
            btnBrowseCsv.Location = new Point(540, 47);
            btnBrowseCsv.Name = "btnBrowseCsv";
            btnBrowseCsv.Size = new Size(110, 25);
            btnBrowseCsv.TabIndex = 3;
            btnBrowseCsv.Text = "Browse CSV...";
            btnBrowseCsv.Click += btnBrowseCsv_Click;
            // 
            // btnImportDistribute
            // 
            btnImportDistribute.Location = new Point(10, 82);
            btnImportDistribute.Name = "btnImportDistribute";
            btnImportDistribute.Size = new Size(200, 28);
            btnImportDistribute.TabIndex = 4;
            btnImportDistribute.Text = "Import & Distribute to Quote";
            btnImportDistribute.Click += btnImportDistribute_Click;
            // 
            // btnGetFinishes
            // 
            btnGetFinishes.Location = new Point(220, 82);
            btnGetFinishes.Name = "btnGetFinishes";
            btnGetFinishes.Size = new Size(180, 28);
            btnGetFinishes.TabIndex = 5;
            btnGetFinishes.Text = "Get Unique Finishes";
            btnGetFinishes.Click += btnGetFinishes_Click;
            // 
            // btnImportPrices
            // 
            btnImportPrices.Location = new Point(410, 82);
            btnImportPrices.Name = "btnImportPrices";
            btnImportPrices.Size = new Size(180, 28);
            btnImportPrices.TabIndex = 6;
            btnImportPrices.Text = "Import Prices from BOM";
            btnImportPrices.Click += btnImportPrices_Click;
            // 
            // lblRowCount
            // 
            lblRowCount.AutoSize = true;
            lblRowCount.Location = new Point(10, 118);
            lblRowCount.Name = "lblRowCount";
            lblRowCount.Size = new Size(53, 15);
            lblRowCount.TabIndex = 7;
            lblRowCount.Text = "Rows: —";
            // 
            // lblPagesNeeded
            // 
            lblPagesNeeded.AutoSize = true;
            lblPagesNeeded.Location = new Point(120, 118);
            lblPagesNeeded.Name = "lblPagesNeeded";
            lblPagesNeeded.Size = new Size(98, 15);
            lblPagesNeeded.TabIndex = 8;
            lblPagesNeeded.Text = "Pages needed: —";
            // 
            // lblMostCommonFinish
            // 
            lblMostCommonFinish.AutoSize = true;
            lblMostCommonFinish.Location = new Point(260, 118);
            lblMostCommonFinish.Name = "lblMostCommonFinish";
            lblMostCommonFinish.Size = new Size(136, 15);
            lblMostCommonFinish.TabIndex = 9;
            lblMostCommonFinish.Text = "Most common finish: —";
            // 
            // gridBom
            // 
            gridBom.AllowUserToAddRows = false;
            gridBom.AllowUserToDeleteRows = false;
            gridBom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridBom.Dock = DockStyle.Fill;
            gridBom.Location = new Point(0, 150);
            gridBom.Name = "gridBom";
            gridBom.ReadOnly = true;
            gridBom.Size = new Size(1150, 550);
            gridBom.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblCsvPath);
            pnlTop.Controls.Add(txtCsvPath);
            pnlTop.Controls.Add(btnBrowseCsv);
            pnlTop.Controls.Add(btnImportDistribute);
            pnlTop.Controls.Add(btnGetFinishes);
            pnlTop.Controls.Add(btnImportPrices);
            pnlTop.Controls.Add(lblRowCount);
            pnlTop.Controls.Add(lblPagesNeeded);
            pnlTop.Controls.Add(lblMostCommonFinish);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1150, 150);
            pnlTop.TabIndex = 1;
            // 
            // BomImportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 700);
            Controls.Add(gridBom);
            Controls.Add(pnlTop);
            Name = "BomImportForm";
            Text = "BOM Import";
            ((System.ComponentModel.ISupportInitialize)gridBom).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
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
