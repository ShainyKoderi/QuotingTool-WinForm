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
            pnlButtons = new Panel();
            btnRefresh = new Button();
            pnlPreview = new Panel();
            lblBrand = new Label();
            lblCompanyInfo = new Label();
            lblTo = new Label();
            lblToValue = new Label();
            lblAttention = new Label();
            lblAttentionValue = new Label();
            lblDate = new Label();
            lblDateValue = new Label();
            lblRe = new Label();
            lblReValue = new Label();
            lblLetterBody = new Label();
            lblColour = new Label();
            lblColourValue = new Label();
            lblProjectPrice = new Label();
            lblProjectPriceValue = new Label();
            lblDelivery = new Label();
            lblDeliveryValue = new Label();
            lblFooterLeft = new Label();
            lblFooterRight = new Label();
            lblTerms = new Label();
            lblAccountManager = new Label();
            lblTitle = new Label();
            lblLocalLogoValue = new Label();
            pnlButtons.SuspendLayout();
            pnlPreview.SuspendLayout();
            SuspendLayout();
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(lblTitle);
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(900, 52);
            pnlButtons.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(12, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 28);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // pnlPreview
            // 
            pnlPreview.AutoScroll = true;
            pnlPreview.BackColor = Color.White;
            pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlPreview.Controls.Add(lblBrand);
            pnlPreview.Controls.Add(lblCompanyInfo);
            pnlPreview.Controls.Add(lblTo);
            pnlPreview.Controls.Add(lblToValue);
            pnlPreview.Controls.Add(lblAttention);
            pnlPreview.Controls.Add(lblAttentionValue);
            pnlPreview.Controls.Add(lblDate);
            pnlPreview.Controls.Add(lblDateValue);
            pnlPreview.Controls.Add(lblRe);
            pnlPreview.Controls.Add(lblReValue);
            pnlPreview.Controls.Add(lblLetterBody);
            pnlPreview.Controls.Add(lblColour);
            pnlPreview.Controls.Add(lblColourValue);
            pnlPreview.Controls.Add(lblProjectPrice);
            pnlPreview.Controls.Add(lblProjectPriceValue);
            pnlPreview.Controls.Add(lblDelivery);
            pnlPreview.Controls.Add(lblDeliveryValue);
            pnlPreview.Controls.Add(lblFooterLeft);
            pnlPreview.Controls.Add(lblFooterRight);
            pnlPreview.Controls.Add(lblTerms);
            pnlPreview.Controls.Add(lblAccountManager);
            pnlPreview.Controls.Add(lblLocalLogoValue);
            pnlPreview.Dock = DockStyle.Fill;
            pnlPreview.Location = new Point(0, 52);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(900, 748);
            pnlPreview.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBrand.ForeColor = Color.Teal;
            lblBrand.Location = new Point(32, 24);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(310, 30);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "CAPRAL ALUMINIUM";
            // 
            // lblCompanyInfo
            // 
            lblCompanyInfo.AutoSize = true;
            lblCompanyInfo.Location = new Point(520, 28);
            lblCompanyInfo.Name = "lblCompanyInfo";
            lblCompanyInfo.Size = new Size(216, 75);
            lblCompanyInfo.TabIndex = 1;
            lblCompanyInfo.Text = "Capral Limited\r\nABN 78 004 213 692\r\nBuilding System Group and Distribution\r\nT: 1300 366 517  F: 1300 133 975\r\nwww.capral.com.au";
            // 
            // lblTo
            // 
            lblTo.Location = new Point(32, 130);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(100, 23);
            lblTo.TabIndex = 2;
            // 
            // lblToValue
            // 
            lblToValue.Location = new Point(140, 130);
            lblToValue.Name = "lblToValue";
            lblToValue.Size = new Size(500, 23);
            lblToValue.TabIndex = 3;
            // 
            // lblAttention
            // 
            lblAttention.Location = new Point(32, 158);
            lblAttention.Name = "lblAttention";
            lblAttention.Size = new Size(100, 23);
            lblAttention.TabIndex = 4;
            // 
            // lblAttentionValue
            // 
            lblAttentionValue.Location = new Point(140, 158);
            lblAttentionValue.Name = "lblAttentionValue";
            lblAttentionValue.Size = new Size(500, 23);
            lblAttentionValue.TabIndex = 5;
            // 
            // lblDate
            // 
            lblDate.Location = new Point(32, 186);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(100, 23);
            lblDate.TabIndex = 6;
            // 
            // lblDateValue
            // 
            lblDateValue.Location = new Point(140, 186);
            lblDateValue.Name = "lblDateValue";
            lblDateValue.Size = new Size(500, 23);
            lblDateValue.TabIndex = 7;
            // 
            // lblRe
            // 
            lblRe.Location = new Point(32, 214);
            lblRe.Name = "lblRe";
            lblRe.Size = new Size(100, 23);
            lblRe.TabIndex = 8;
            // 
            // lblReValue
            // 
            lblReValue.Location = new Point(140, 214);
            lblReValue.Name = "lblReValue";
            lblReValue.Size = new Size(500, 23);
            lblReValue.TabIndex = 9;
            // 
            // lblLetterBody
            // 
            lblLetterBody.AutoSize = true;
            lblLetterBody.Location = new Point(32, 260);
            lblLetterBody.MaximumSize = new Size(760, 0);
            lblLetterBody.Name = "lblLetterBody";
            lblLetterBody.Size = new Size(0, 15);
            lblLetterBody.TabIndex = 10;
            // 
            // lblColour
            // 
            lblColour.Location = new Point(32, 520);
            lblColour.Name = "lblColour";
            lblColour.Size = new Size(100, 23);
            lblColour.TabIndex = 11;
            // 
            // lblColourValue
            // 
            lblColourValue.Location = new Point(180, 520);
            lblColourValue.Name = "lblColourValue";
            lblColourValue.Size = new Size(100, 23);
            lblColourValue.TabIndex = 12;
            // 
            // lblProjectPrice
            // 
            lblProjectPrice.Location = new Point(32, 548);
            lblProjectPrice.Name = "lblProjectPrice";
            lblProjectPrice.Size = new Size(100, 23);
            lblProjectPrice.TabIndex = 13;
            // 
            // lblProjectPriceValue
            // 
            lblProjectPriceValue.Location = new Point(180, 548);
            lblProjectPriceValue.Name = "lblProjectPriceValue";
            lblProjectPriceValue.Size = new Size(100, 23);
            lblProjectPriceValue.TabIndex = 14;
            // 
            // lblDelivery
            // 
            lblDelivery.Location = new Point(32, 576);
            lblDelivery.Name = "lblDelivery";
            lblDelivery.Size = new Size(120, 23);
            lblDelivery.TabIndex = 15;
            // 
            // lblDeliveryValue
            // 
            lblDeliveryValue.Location = new Point(180, 576);
            lblDeliveryValue.Name = "lblDeliveryValue";
            lblDeliveryValue.Size = new Size(100, 23);
            lblDeliveryValue.TabIndex = 16;
            // 
            // lblFooterLeft
            // 
            lblFooterLeft.AutoSize = true;
            lblFooterLeft.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFooterLeft.Location = new Point(32, 620);
            lblFooterLeft.Name = "lblFooterLeft";
            lblFooterLeft.Size = new Size(0, 15);
            lblFooterLeft.TabIndex = 17;
            // 
            // lblFooterRight
            // 
            lblFooterRight.AutoSize = true;
            lblFooterRight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFooterRight.Location = new Point(560, 620);
            lblFooterRight.Name = "lblFooterRight";
            lblFooterRight.Size = new Size(0, 15);
            lblFooterRight.TabIndex = 18;
            // 
            // lblTerms
            // 
            lblTerms.AutoSize = true;
            lblTerms.Location = new Point(32, 660);
            lblTerms.MaximumSize = new Size(760, 0);
            lblTerms.Name = "lblTerms";
            lblTerms.Size = new Size(0, 15);
            lblTerms.TabIndex = 19;
            // 
            // lblAccountManager
            // 
            lblAccountManager.AutoSize = true;
            lblAccountManager.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountManager.Location = new Point(32, 740);
            lblAccountManager.Name = "lblAccountManager";
            lblAccountManager.Size = new Size(0, 15);
            lblAccountManager.TabIndex = 20;
            // 
            // lblLocalLogoValue
            // 
            lblLocalLogoValue.AutoSize = true;
            lblLocalLogoValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLocalLogoValue.ForeColor = Color.DarkGreen;
            lblLocalLogoValue.Location = new Point(32, 64);
            lblLocalLogoValue.Name = "lblLocalLogoValue";
            lblLocalLogoValue.Size = new Size(0, 15);
            lblLocalLogoValue.TabIndex = 21;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(14, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 30);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "4. Quote Letter Preview";
            // 
            // QuoteLetterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 800);
            Controls.Add(pnlPreview);
            Controls.Add(pnlButtons);
            Name = "QuoteLetterForm";
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
        private Label lblTitle;
        private Label lblLocalLogoValue;
    }
}
