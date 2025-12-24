using DevExpress.XtraBars.Ribbon;

namespace VEIOfflineClient
{
    public partial class Form1 : RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _device_id;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _device_id = DeviceId.Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving device ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _device_id = "UnknownDevice";
            }
        }
    }
}