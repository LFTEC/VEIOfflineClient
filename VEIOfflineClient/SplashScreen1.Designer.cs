namespace VEIOfflineClient
{
    partial class SplashScreen1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
        {
            labelCopyright = new DevExpress.XtraEditors.LabelControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peLogo = new DevExpress.XtraEditors.PictureEdit();
            progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).BeginInit();
            SuspendLayout();
            // 
            // labelCopyright
            // 
            labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelCopyright.Location = new Point(28, 110);
            labelCopyright.Margin = new Padding(4, 3, 4, 3);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(364, 17);
            labelCopyright.TabIndex = 6;
            labelCopyright.Text = "Copyright(c) 2025 万华化学集团股份有限公司 All rights reserved.";
            // 
            // labelStatus
            // 
            labelStatus.Location = new Point(28, 42);
            labelStatus.Margin = new Padding(4, 3, 4, 1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(45, 17);
            labelStatus.TabIndex = 7;
            labelStatus.Text = "更新中...";
            // 
            // peLogo
            // 
            peLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            peLogo.EditValue = VEIOfflineClient.Properties.Resources._10872bec_e4d8_4e66_ad4f_854b3e69b300;
            peLogo.Location = new Point(400, 93);
            peLogo.Margin = new Padding(4, 3, 4, 3);
            peLogo.Name = "peLogo";
            peLogo.Properties.AllowFocused = false;
            peLogo.Properties.Appearance.BackColor = Color.Transparent;
            peLogo.Properties.Appearance.Options.UseBackColor = true;
            peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peLogo.Properties.ShowMenu = false;
            peLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            peLogo.Size = new Size(97, 45);
            peLogo.TabIndex = 8;
            // 
            // progressBarControl1
            // 
            progressBarControl1.Location = new Point(28, 69);
            progressBarControl1.Name = "progressBarControl1";
            progressBarControl1.Size = new Size(469, 18);
            progressBarControl1.TabIndex = 10;
            // 
            // SplashScreen1
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 149);
            Controls.Add(progressBarControl1);
            Controls.Add(peLogo);
            Controls.Add(labelStatus);
            Controls.Add(labelCopyright);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SplashScreen1";
            Padding = new Padding(1);
            Text = "SplashScreen1";
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
    }
}
