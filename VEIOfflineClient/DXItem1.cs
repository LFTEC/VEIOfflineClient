using DevExpress.XtraEditors;
using Microsoft.Extensions.Options;

namespace VEIOfflineClient;

public partial class DXItem1 : XtraForm
{
    private readonly IOptionsSnapshot<ActivateInfo> _options;
    private readonly ConfigService _configService;
    public DXItem1(IOptionsSnapshot<ActivateInfo> options, ConfigService configService)
    {
        InitializeComponent();
        _options = options;
        _configService = configService;
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
            _configService.UpdateActivateInfo(info => info.ActivateCode = textEdit2.Text.Trim());
            XtraMessageBox.Show("激活成功！");
        }
        else
        {
            XtraMessageBox.Show("请将激活码复制到文本框中后再激活");
        }
    }
}