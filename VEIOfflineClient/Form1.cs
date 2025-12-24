using DevExpress.XtraBars.Ribbon;
using Microsoft.Extensions.Options;

namespace VEIOfflineClient
{
    public partial class Form1 : RibbonForm
    {
        private readonly IOptionsSnapshot<ActivateInfo> _options;
        private readonly DXItem1 _activateForm;
        public Form1(IOptionsSnapshot<ActivateInfo> options, DXItem1 activateForm)
        {
            InitializeComponent();
            _options = options;
            _activateForm = activateForm;
        }

        private string _device_id = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(_options.Value.ActivateCode))
            {
                _activateForm.ShowDialog();
            }
        }
    }
}