using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Velopack.Locators;

namespace VEIOfflineClient
{
    public partial class About : DevExpress.XtraEditors.XtraForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            var locator = VelopackLocator.Current.CurrentlyInstalledVersion;
            labelControl3.Text = $"版本号：{locator?.ToFullString() ?? "未知"}";
        }

        private void hyperlinkLabelControl1_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://vei.jcdev.cc",
                UseShellExecute = true // 必须设置为 true 才能在默认浏览器中打开
            });
        }
    }
}