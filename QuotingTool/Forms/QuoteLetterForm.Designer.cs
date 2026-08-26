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
            lblTitle = new Label();
            btnRefresh = new Button();
            pnlPreview = new Panel();
            lblLocalLogoPlaceholder = new Label();
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
            lblLocalLogoValue = new Label();
            lblCapralLogoPlaceholder = new Label();
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
            pnlButtons.Margin = new Padding(4, 5, 4, 5);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1286, 87);
            pnlButtons.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(157, 17);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 50);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Quote Letter Preview";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(17, 20);
            btnRefresh.Margin = new Padding(4, 5, 4, 5);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(129, 47);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // pnlPreview
            // 
            pnlPreview.AutoScroll = true;
            pnlPreview.BackColor = Color.White;
            pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlPreview.Controls.Add(lblCapralLogoPlaceholder);
            pnlPreview.Controls.Add(lblLocalLogoPlaceholder);
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
            pnlPreview.Location = new Point(0, 87);
            pnlPreview.Margin = new Padding(4, 5, 4, 5);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(1286, 1083);
            pnlPreview.TabIndex = 0;
            // 
            // lblLocalLogoPlaceholder
            // 
            lblLocalLogoPlaceholder.BackColor = Color.White;
            lblLocalLogoPlaceholder.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblLocalLogoPlaceholder.ForeColor = Color.White;
            lblLocalLogoPlaceholder.Image = Properties.Resources.local_super_green;
            lblLocalLogoPlaceholder.Location = new Point(37, 123);
            lblLocalLogoPlaceholder.Margin = new Padding(4, 0, 4, 0);
            lblLocalLogoPlaceholder.Name = "lblLocalLogoPlaceholder";
            lblLocalLogoPlaceholder.Size = new Size(314, 60);
            lblLocalLogoPlaceholder.TabIndex = 23;
            lblLocalLogoPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCompanyInfo
            // 
            lblCompanyInfo.AutoSize = true;
            lblCompanyInfo.Location = new Point(971, 40);
            lblCompanyInfo.Margin = new Padding(4, 0, 4, 0);
            lblCompanyInfo.Name = "lblCompanyInfo";
            lblCompanyInfo.Size = new Size(326, 125);
            lblCompanyInfo.TabIndex = 1;
            lblCompanyInfo.Text = "Capral Limited\r\nABN 78 004 213 692\r\nBuilding System Group and Distribution\r\nT: 1300 366 517  F: 1300 133 975\r\nwww.capral.com.au";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTo.Location = new Point(46, 217);
            lblTo.Margin = new Padding(4, 0, 4, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(37, 25);
            lblTo.TabIndex = 2;
            lblTo.Text = "To:";
            // 
            // lblToValue
            // 
            lblToValue.Location = new Point(257, 217);
            lblToValue.Margin = new Padding(4, 0, 4, 0);
            lblToValue.Name = "lblToValue";
            lblToValue.Size = new Size(686, 38);
            lblToValue.TabIndex = 3;
            // 
            // lblAttention
            // 
            lblAttention.AutoSize = true;
            lblAttention.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAttention.Location = new Point(46, 263);
            lblAttention.Margin = new Padding(4, 0, 4, 0);
            lblAttention.Name = "lblAttention";
            lblAttention.Size = new Size(99, 25);
            lblAttention.TabIndex = 4;
            lblAttention.Text = "Attention:";
            // 
            // lblAttentionValue
            // 
            lblAttentionValue.Location = new Point(257, 263);
            lblAttentionValue.Margin = new Padding(4, 0, 4, 0);
            lblAttentionValue.Name = "lblAttentionValue";
            lblAttentionValue.Size = new Size(686, 38);
            lblAttentionValue.TabIndex = 5;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDate.Location = new Point(46, 310);
            lblDate.Margin = new Padding(4, 0, 4, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(57, 25);
            lblDate.TabIndex = 6;
            lblDate.Text = "Date:";
            // 
            // lblDateValue
            // 
            lblDateValue.Location = new Point(257, 310);
            lblDateValue.Margin = new Padding(4, 0, 4, 0);
            lblDateValue.Name = "lblDateValue";
            lblDateValue.Size = new Size(686, 38);
            lblDateValue.TabIndex = 7;
            // 
            // lblRe
            // 
            lblRe.AutoSize = true;
            lblRe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRe.Location = new Point(46, 357);
            lblRe.Margin = new Padding(4, 0, 4, 0);
            lblRe.Name = "lblRe";
            lblRe.Size = new Size(39, 25);
            lblRe.TabIndex = 8;
            lblRe.Text = "Re:";
            // 
            // lblReValue
            // 
            lblReValue.Location = new Point(257, 357);
            lblReValue.Margin = new Padding(4, 0, 4, 0);
            lblReValue.Name = "lblReValue";
            lblReValue.Size = new Size(686, 38);
            lblReValue.TabIndex = 9;
            // 
            // lblLetterBody
            // 
            lblLetterBody.AutoSize = true;
            lblLetterBody.Location = new Point(46, 433);
            lblLetterBody.Margin = new Padding(4, 0, 4, 0);
            lblLetterBody.MaximumSize = new Size(1171, 0);
            lblLetterBody.Name = "lblLetterBody";
            lblLetterBody.Size = new Size(0, 25);
            lblLetterBody.TabIndex = 10;
            // 
            // lblColour
            // 
            lblColour.AutoSize = true;
            lblColour.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblColour.Location = new Point(46, 883);
            lblColour.Margin = new Padding(4, 0, 4, 0);
            lblColour.Name = "lblColour";
            lblColour.Size = new Size(140, 25);
            lblColour.TabIndex = 11;
            lblColour.Text = "Colour / Finish:";
            // 
            // lblColourValue
            // 
            lblColourValue.Location = new Point(314, 883);
            lblColourValue.Margin = new Padding(4, 0, 4, 0);
            lblColourValue.Name = "lblColourValue";
            lblColourValue.Size = new Size(571, 38);
            lblColourValue.TabIndex = 12;
            // 
            // lblProjectPrice
            // 
            lblProjectPrice.AutoSize = true;
            lblProjectPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProjectPrice.Location = new Point(46, 930);
            lblProjectPrice.Margin = new Padding(4, 0, 4, 0);
            lblProjectPrice.Name = "lblProjectPrice";
            lblProjectPrice.Size = new Size(202, 25);
            lblProjectPrice.TabIndex = 13;
            lblProjectPrice.Text = "Project Price (ex GST):";
            // 
            // lblProjectPriceValue
            // 
            lblProjectPriceValue.Location = new Point(314, 930);
            lblProjectPriceValue.Margin = new Padding(4, 0, 4, 0);
            lblProjectPriceValue.Name = "lblProjectPriceValue";
            lblProjectPriceValue.Size = new Size(286, 38);
            lblProjectPriceValue.TabIndex = 14;
            // 
            // lblDelivery
            // 
            lblDelivery.AutoSize = true;
            lblDelivery.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDelivery.Location = new Point(46, 977);
            lblDelivery.Margin = new Padding(4, 0, 4, 0);
            lblDelivery.Name = "lblDelivery";
            lblDelivery.Size = new Size(88, 25);
            lblDelivery.TabIndex = 15;
            lblDelivery.Text = "Delivery:";
            // 
            // lblDeliveryValue
            // 
            lblDeliveryValue.Location = new Point(314, 977);
            lblDeliveryValue.Margin = new Padding(4, 0, 4, 0);
            lblDeliveryValue.Name = "lblDeliveryValue";
            lblDeliveryValue.Size = new Size(571, 38);
            lblDeliveryValue.TabIndex = 16;
            // 
            // lblFooterLeft
            // 
            lblFooterLeft.AutoSize = true;
            lblFooterLeft.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFooterLeft.Location = new Point(46, 1050);
            lblFooterLeft.Margin = new Padding(4, 0, 4, 0);
            lblFooterLeft.Name = "lblFooterLeft";
            lblFooterLeft.Size = new Size(0, 25);
            lblFooterLeft.TabIndex = 17;
            // 
            // lblFooterRight
            // 
            lblFooterRight.AutoSize = true;
            lblFooterRight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFooterRight.Location = new Point(800, 1050);
            lblFooterRight.Margin = new Padding(4, 0, 4, 0);
            lblFooterRight.Name = "lblFooterRight";
            lblFooterRight.Size = new Size(0, 25);
            lblFooterRight.TabIndex = 18;
            // 
            // lblTerms
            // 
            lblTerms.AutoSize = true;
            lblTerms.Location = new Point(46, 1117);
            lblTerms.Margin = new Padding(4, 0, 4, 0);
            lblTerms.MaximumSize = new Size(1171, 0);
            lblTerms.Name = "lblTerms";
            lblTerms.Size = new Size(0, 25);
            lblTerms.TabIndex = 19;
            // 
            // lblAccountManager
            // 
            lblAccountManager.AutoSize = true;
            lblAccountManager.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountManager.Location = new Point(46, 1267);
            lblAccountManager.Margin = new Padding(4, 0, 4, 0);
            lblAccountManager.Name = "lblAccountManager";
            lblAccountManager.Size = new Size(0, 25);
            lblAccountManager.TabIndex = 20;
            // 
            // lblLocalLogoValue
            // 
            lblLocalLogoValue.AutoSize = true;
            lblLocalLogoValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLocalLogoValue.ForeColor = Color.DarkGreen;
            lblLocalLogoValue.Location = new Point(46, 133);
            lblLocalLogoValue.Margin = new Padding(4, 0, 4, 0);
            lblLocalLogoValue.Name = "lblLocalLogoValue";
            lblLocalLogoValue.Size = new Size(0, 25);
            lblLocalLogoValue.TabIndex = 21;
            // 
            // lblCapralLogoPlaceholder
            // 
            lblCapralLogoPlaceholder.BackColor = Color.White;
            lblCapralLogoPlaceholder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCapralLogoPlaceholder.ForeColor = Color.White;
            lblCapralLogoPlaceholder.Image = Properties.Resources.capral_logo;
            lblCapralLogoPlaceholder.Location = new Point(46, 40);
            lblCapralLogoPlaceholder.Margin = new Padding(4, 0, 4, 0);
            lblCapralLogoPlaceholder.Name = "lblCapralLogoPlaceholder";
            lblCapralLogoPlaceholder.Size = new Size(200, 83);
            lblCapralLogoPlaceholder.TabIndex = 22;
            lblCapralLogoPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // QuoteLetterForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1286, 1170);
            Controls.Add(pnlPreview);
            Controls.Add(pnlButtons);
            Margin = new Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.Label lblLocalLogoPlaceholder;
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
        private Label lblCapralLogoPlaceholder;
    }
}
