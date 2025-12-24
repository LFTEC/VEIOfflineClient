namespace VEIOfflineClient {
    partial class DXItem1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            textEdit2 = new DevExpress.XtraEditors.TextEdit();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEdit2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(labelControl1);
            layoutControl1.Controls.Add(textEdit1);
            layoutControl1.Controls.Add(textEdit2);
            layoutControl1.Controls.Add(simpleButton1);
            layoutControl1.Controls.Add(simpleButton2);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(851, 221, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(483, 186);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            labelControl1.Appearance.ForeColor = Color.Red;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            labelControl1.LineColor = Color.Black;
            labelControl1.Location = new Point(12, 99);
            labelControl1.Name = "labelControl1";
            labelControl1.Padding = new Padding(0, 5, 0, 0);
            labelControl1.Size = new Size(459, 33);
            labelControl1.StyleController = layoutControl1;
            labelControl1.TabIndex = 5;
            labelControl1.Text = "当出现此画面时，请将授权码复制后发送给采购员激活。采购员激活系统后，会返回激活码。请将激活码填入激活码框中，点击激活。";
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(12, 34);
            textEdit1.MinimumSize = new Size(400, 0);
            textEdit1.Name = "textEdit1";
            textEdit1.Properties.ReadOnly = true;
            textEdit1.Size = new Size(459, 20);
            textEdit1.StyleController = layoutControl1;
            textEdit1.TabIndex = 0;
            // 
            // textEdit2
            // 
            textEdit2.Location = new Point(12, 75);
            textEdit2.Name = "textEdit2";
            textEdit2.Size = new Size(459, 20);
            textEdit2.StyleController = layoutControl1;
            textEdit2.TabIndex = 2;
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new Point(224, 152);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(121, 22);
            simpleButton1.StyleController = layoutControl1;
            simpleButton1.TabIndex = 3;
            simpleButton1.Text = "激活";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.Location = new Point(349, 152);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(122, 22);
            simpleButton2.StyleController = layoutControl1;
            simpleButton2.TabIndex = 4;
            simpleButton2.Text = "退出程序";
            simpleButton2.Click += simpleButton2_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1, layoutControlItem3, layoutControlItem2, layoutControlItem4, emptySpaceItem5, emptySpaceItem6, layoutControlItem5 });
            Root.Name = "Root";
            Root.Size = new Size(483, 186);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = textEdit1;
            layoutControlItem1.Location = new Point(0, 5);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(463, 41);
            layoutControlItem1.Text = "授权码";
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(0, 0);
            emptySpaceItem1.MinSize = new Size(200, 5);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(463, 5);
            emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = simpleButton1;
            layoutControlItem3.Location = new Point(212, 140);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(125, 26);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = textEdit2;
            layoutControlItem2.Location = new Point(0, 46);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(463, 41);
            layoutControlItem2.Text = "激活码";
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(36, 14);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = simpleButton2;
            layoutControlItem4.Location = new Point(337, 140);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(126, 26);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new Point(0, 140);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new Size(212, 26);
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.Location = new Point(0, 124);
            emptySpaceItem6.Name = "emptySpaceItem6";
            emptySpaceItem6.Size = new Size(463, 16);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = labelControl1;
            layoutControlItem5.Location = new Point(0, 87);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(463, 37);
            layoutControlItem5.TextVisible = false;
            // 
            // DXItem1
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 186);
            Controls.Add(layoutControl1);
            Name = "DXItem1";
            Text = "Form1";
            Load += DXItem1_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEdit2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
