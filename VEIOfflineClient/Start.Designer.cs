namespace VEIOfflineClient
{
    partial class Start
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
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(115, 95);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(181, 25);
            label1.TabIndex = 0;
            label1.Text = "Wanhua VEI单机版";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Bottom;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(0, 214);
            label2.Margin = new Padding(2, 0, 0, 2);
            label2.Name = "label2";
            label2.Padding = new Padding(2, 0, 0, 2);
            label2.Size = new Size(62, 22);
            label2.TabIndex = 1;
            label2.Text = "启动中...";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Start
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RoyalBlue;
            ClientSize = new Size(410, 236);
            ControlBox = false;
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Start";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Start";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
    }
}