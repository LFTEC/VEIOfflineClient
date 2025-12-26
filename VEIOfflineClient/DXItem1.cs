using DevExpress.XtraEditors;
using Microsoft.Extensions.Options;

namespace VEIOfflineClient;

public partial class DXItem1 : XtraForm
{
    private readonly IOptionsSnapshot<ActivateInfo> _options;
    private readonly ConfigService _configService;
    private readonly SecurityConfigurationProvider _security;
    public DXItem1(IOptionsSnapshot<ActivateInfo> options, SecurityConfigurationProvider security, ConfigService configService)
    {
        InitializeComponent();
        _options = options;
        _configService = configService;
        _security = security;
    }

    private void DXItem1_Load(object sender, EventArgs e)
    {
        textEdit1.Text = _options.Value.DeviceId;
    }

    private void simpleButton2_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void simpleButton1_Click(object sender, EventArgs e)
    {
        if(!String.IsNullOrEmpty(textEdit2.Text))
        {
            try
            {
                _security.SetDecryptedValue(_options.Value.DeviceId, textEdit2.Text.Trim());
                _configService.UpdateActivateInfo(info => info.ActivateCode = textEdit2.Text.Trim());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "激活错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            XtraMessageBox.Show("激活成功！");
            this.Close();
        }
        else
        {
            XtraMessageBox.Show("请将激活码复制到文本框中后再激活");
        }
    }
}