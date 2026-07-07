namespace QuotingTool.Forms
{
    partial class QuoteLetterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlButtons = new System.Windows.Forms.Panel();
            btnPrintLetter = new System.Windows.Forms.Button();
            btnSavePdf = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            pnlPreview = new System.Windows.Forms.Panel();
            lblBrand = new System.Windows.Forms.Label();
            lblCompanyInfo = new System.Windows.Forms.Label();
            lblTo = new System.Windows.Forms.Label();
            lblToValue = new System.Windows.Forms.Label();
            lblAttention = new System.Windows.Forms.Label();
            lblAttentionValue = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            lblDateValue = new System.Windows.Forms.Label();
            lblRe = new System.Windows.Forms.Label();
            lblReValue = new System.Windows.Forms.Label();
            lblLetterBody = new System.Windows.Forms.Label();
            lblColour = new System.Windows.Forms.Label();
            lblColourValue = new System.Windows.Forms.Label();
            lblProjectPrice = new System.Windows.Forms.Label();
            lblProjectPriceValue = new System.Windows.Forms.Label();
            lblDelivery = new System.Windows.Forms.Label();
            lblDeliveryValue = new System.Windows.Forms.Label();
            lblFooterLeft = new System.Windows.Forms.Label();
            lblFooterRight = new System.Windows.Forms.Label();
            lblTerms = new System.Windows.Forms.Label();
            lblAccountManager = new System.Windows.Forms.Label();
            pnlButtons.SuspendLayout();
            pnlPreview.SuspendLayout();
            SuspendLayout();

            pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { btnPrintLetter, btnSavePdf, btnRefresh });
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            pnlButtons.Height = 48;

            btnPrintLetter.Location = new System.Drawing.Point(12, 10);
            btnPrintLetter.Size = new System.Drawing.Size(100, 28);
            btnPrintLetter.Text = "Print Letter";
            btnPrintLetter.Click += new System.EventHandler(btnPrintLetter_Click);

            btnSavePdf.Location = new System.Drawing.Point(118, 10);
            btnSavePdf.Size = new System.Drawing.Size(100, 28);
            btnSavePdf.Text = "Save as PDF";
            btnSavePdf.Click += new System.EventHandler(btnSavePdf_Click);

            btnRefresh.Location = new System.Drawing.Point(224, 10);
            btnRefresh.Size = new System.Drawing.Size(90, 28);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += new System.EventHandler(btnRefresh_Click);

            pnlPreview.AutoScroll = true;
            pnlPreview.BackColor = System.Drawing.Color.White;
            pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlPreview.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblBrand, lblCompanyInfo, lblTo, lblToValue, lblAttention, lblAttentionValue,
                lblDate, lblDateValue, lblRe, lblReValue, lblLetterBody, lblColour, lblColourValue,
                lblProjectPrice, lblProjectPriceValue, lblDelivery, lblDeliveryValue,
                lblFooterLeft, lblFooterRight, lblTerms, lblAccountManager
            });

            lblBrand.AutoSize = true;
            lblBrand.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblBrand.ForeColor = System.Drawing.Color.Teal;
            lblBrand.Location = new System.Drawing.Point(32, 24);
            lblBrand.Text = "CAPRAL ALUMINIUM";

            lblCompanyInfo.AutoSize = true;
            lblCompanyInfo.Location = new System.Drawing.Point(520, 28);
            lblCompanyInfo.Text = "Capral Limited\r\nABN 78 004 213 692\r\nBuilding System Group and Distribution\r\nT: 1300 366 517  F: 1300 133 975\r\nwww.capral.com.au";

            ConfigureField(lblTo, lblToValue, "To:", 32, 140);
            lblTo.Location = new System.Drawing.Point(32, 130);
            lblToValue.Location = new System.Drawing.Point(140, 130);
            ConfigureField(lblAttention, lblAttentionValue, "Attention:", 32, 140);
            lblAttention.Location = new System.Drawing.Point(32, 158);
            lblAttentionValue.Location = new System.Drawing.Point(140, 158);
            ConfigureField(lblDate, lblDateValue, "Date:", 32, 140);
            lblDate.Location = new System.Drawing.Point(32, 186);
            lblDateValue.Location = new System.Drawing.Point(140, 186);
            ConfigureField(lblRe, lblReValue, "Re:", 32, 140);
            lblRe.Location = new System.Drawing.Point(32, 214);
            lblReValue.Location = new System.Drawing.Point(140, 214);

            lblLetterBody.AutoSize = true;
            lblLetterBody.Location = new System.Drawing.Point(32, 260);
            lblLetterBody.MaximumSize = new System.Drawing.Size(760, 0);
            lblLetterBody.Text = string.Empty;

            ConfigureField(lblColour, lblColourValue, "Colour:", 32, 180);
            lblColour.Location = new System.Drawing.Point(32, 520);
            lblColourValue.Location = new System.Drawing.Point(180, 520);
            ConfigureField(lblProjectPrice, lblProjectPriceValue, "Project Price $:", 32, 180);
            lblProjectPrice.Location = new System.Drawing.Point(32, 548);
            lblProjectPriceValue.Location = new System.Drawing.Point(180, 548);
            ConfigureField(lblDelivery, lblDeliveryValue, "Delivery $:", 32, 180);
            lblDelivery.Location = new System.Drawing.Point(32, 576);
            lblDeliveryValue.Location = new System.Drawing.Point(180, 576);

            lblFooterLeft.AutoSize = true;
            lblFooterLeft.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFooterLeft.Location = new System.Drawing.Point(32, 620);

            lblFooterRight.AutoSize = true;
            lblFooterRight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFooterRight.Location = new System.Drawing.Point(560, 620);

            lblTerms.AutoSize = true;
            lblTerms.Location = new System.Drawing.Point(32, 660);
            lblTerms.MaximumSize = new System.Drawing.Size(760, 0);

            lblAccountManager.AutoSize = true;
            lblAccountManager.Location = new System.Drawing.Point(32, 740);
            lblAccountManager.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 800);
            Controls.Add(pnlPreview);
            Controls.Add(pnlButtons);
            Text = "Quote Letter";
            pnlButtons.ResumeLayout(false);
            pnlPreview.ResumeLayout(false);
            pnlPreview.PerformLayout();
            ResumeLayout(false);
        }

        private static void ConfigureField(System.Windows.Forms.Label label, System.Windows.Forms.Label valueLabel, string text, int x1, int x2)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(x1, 0);
            label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label.Text = text;
            valueLabel.AutoSize = true;
            valueLabel.Location = new System.Drawing.Point(x2, 0);
        }

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnPrintLetter;
        private System.Windows.Forms.Button btnSavePdf;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblCompanyInfo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblToValue;
        private System.Windows.Forms.Label lblAttention;
        private System.Windows.Forms.Label lblAttentionValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblRe;
        private System.Windows.Forms.Label lblReValue;
        private System.Windows.Forms.Label lblLetterBody;
        private System.Windows.Forms.Label lblColour;
        private System.Windows.Forms.Label lblColourValue;
        private System.Windows.Forms.Label lblProjectPrice;
        private System.Windows.Forms.Label lblProjectPriceValue;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.Label lblDeliveryValue;
        private System.Windows.Forms.Label lblFooterLeft;
        private System.Windows.Forms.Label lblFooterRight;
        private System.Windows.Forms.Label lblTerms;
        private System.Windows.Forms.Label lblAccountManager;
    }
}
