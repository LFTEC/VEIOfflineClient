using DevExpress.XtraEditors;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace VEIOfflineClient;

public partial class DXItem1 : XtraForm
{
    private readonly IOptionsSnapshot<ActivateInfo> _options;
    private readonly ConfigService _configService;
    private readonly SecurityConfigurationProvider _security;
    private readonly SecurityInfo _securityInfo;
    public DXItem1(IOptionsSnapshot<ActivateInfo> options, SecurityConfigurationProvider security, ConfigService configService, IOptions<SecurityInfo> securityInfo)
    {
        InitializeComponent();
        _options = options;
        _configService = configService;
        _security = security;
        _securityInfo = securityInfo.Value;
    }

    private void DXItem1_Load(object sender, EventArgs e)
    {
        textEdit1.Text = _options.Value.DeviceId;
        if(string.IsNullOrEmpty(_securityInfo.Vendor))
        {
            simpleButton2.Text = "退出程序";
        }
        else
        {
            simpleButton2.Text = "关闭";
        }
    }

    private void simpleButton2_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void simpleButton1_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(textEdit2.Text))
        {
            try
            {
                _security.SetDecryptedValue(_options.Value.DeviceId, textEdit2.Text.Trim());
                _configService.UpdateActivateInfo(info => info.ActivateCode = textEdit2.Text.Trim());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this.Owner, ex.Message, "激活错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XtraMessageBox.Show(this.Owner, "激活成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }
        else
        {
            XtraMessageBox.Show(this.Owner, "请将激活码复制到文本框中后再激活","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void DXItem1_FormClosed(object sender, FormClosedEventArgs e)
    {
        

    }

    private void DXItem1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (string.IsNullOrEmpty(_securityInfo.Vendor))
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
        
    }
}