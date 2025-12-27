using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VEIOfflineClient
{
    public partial class SplashScreen1 : SplashScreen
    {
        public SplashScreen1()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 1998-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            var command = (SplashScreenCommand)cmd;
            if(command == SplashScreenCommand.SetProgress)
            {
                int progress = (int)arg;
                progressBarControl1.Position = progress;

            }
        }

        #endregion

        public enum SplashScreenCommand
        {
            SetProgress
        }
    }
}