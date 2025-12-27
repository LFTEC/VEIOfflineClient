using DevExpress.Spreadsheet;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace VEIOfflineClient
{
    public partial class Form1 : RibbonForm
    {
        private readonly SecurityInfo _options;
        private readonly ICallApiService _apiService;
        private readonly DXItem1 _activateForm;
        public Form1(IOptionsMonitor<SecurityInfo> options, DXItem1 activateForm, ICallApiService apiService)
        {
            InitializeComponent();
            _options = options.CurrentValue;
            _activateForm = activateForm;
            _apiService = apiService;

            stocks = new List<StockData>();
            bindingStocks = new BindingList<StockData>(stocks);
            Init_Sheet();
            RefreshData();
        }

        private string _device_id = string.Empty;
        private List<StockData> stocks;
        private BindingList<StockData> bindingStocks;

        private async void Form1_Load(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(_options.Vendor))
            {
                barButtonItem1.PerformClick();
                
            }

            barButtonItem2.PerformClick();


        }

        private void RefreshData()
        {
            // Adjust the column width.
            var sheet = spreadsheetControl.Document.Worksheets[0];

            var range = sheet.Tables[0].Range;
            //var range = sheet.GetUsedRange();
            range.Font.Name = "微软雅黑";
            range.AutoFitColumns();
            spreadsheetControl.WorksheetDisplayArea.SetSize(sheet.Index, range.ColumnCount, range.RowCount);
        }

        private void Init_Sheet()
        {
            var sheet = spreadsheetControl.ActiveWorksheet;
            BindStockDataToRange(bindingStocks, sheet.Range["A1"]);

            if (!sheet.IsProtected)
                sheet.Protect("test", WorksheetProtectionPermissions.Default | WorksheetProtectionPermissions.Sort | WorksheetProtectionPermissions.AutoFilters );

            var table = sheet.Tables[0];
            foreach (var column in table.Columns)
            {
                if (column.Index == 3)
                {
                    column.Range.Protection.Locked = false;
                }
                else
                    column.Range.Protection.Locked = true;
            }
        }

        private void BindStockDataToRange(BindingList<StockData> list, DevExpress.Spreadsheet.CellRange bindingRange)
        {
            var sheet = spreadsheetControl.ActiveWorksheet;
            sheet.DataBindings.Remove(list);

            ExternalDataSourceOptions options = new ExternalDataSourceOptions();
            options.ImportHeaders = true;

            options.CellValueConverter = new StockDataConverter();
            options.SkipHiddenRows = true;

            try
            {
                WorksheetTableDataBinding sheetDataBinding = sheet.DataBindings.BindTableToDataSource(list, bindingRange, options);
                sheetDataBinding.Table.Style = spreadsheetControl.Document.TableStyles[BuiltInTableStyleId.TableStyleMedium2];

            }
            catch (Exception e)
            {
                XtraMessageBox.Show(this, e.Message, "Binding Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //保存
        private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(XtraMessageBox.Show(this, "确定要保存备库库存到万华系统吗？\n 注意：所有未维护备库量的物资备库库存都将被清空！","请注意",MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            if(spreadsheetControl.IsCellEditorActive)
            {
                spreadsheetControl.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default);
            }
            if (stocks.Any(s => s.Quantity < 0))
            {
                XtraMessageBox.Show(this, "备库数量维护不正确，必须为正数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var items = new List<StockMoveItem>();
                items.AddRange(stocks.Select(s =>
                {
                    return new StockMoveItem(s.Material, s.MatType, "full", s.Quantity, s.Unit, "库存全量同步");

                }));

                var response = await _apiService.SendStockInfoAsync(items);

                XtraMessageBox.Show(this,"库存调整请求发送成功.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"发送库存调整请求时发生错误: {ex.Message}","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //刷新数据
        private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (spreadsheetControl.IsCellEditorActive)
            {
                spreadsheetControl.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default);
            }

            try
            {
                var data = await _apiService.GetMasterDataAsync();
                bindingStocks.Clear();
                foreach (var item in data.Where(s => s.deleted == false))
                {
                    bindingStocks.Add(new StockData
                    {
                        Material = item.material,
                        MaterialDesc = item.materialDesc,
                        MatType = item.matType,
                        LongText = item.longText,
                        PurLongText = item.purLongText,
                        Quantity = 0m,
                        Unit = item.unit

                    });
                }

                RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"获取备库清单时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //重新激活
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _activateForm.ShowDialog(this);
                barButtonItem2.PerformClick();
            }
            catch (Exception)
            {
                XtraMessageBox.Show(this, "未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }

    public class StockData
    {
        [DisplayName("物料号")]
        public string Material { get; set; } = string.Empty;
        [DisplayName("物料描述")]
        public string MaterialDesc {  get; set; } = string.Empty;
        [DisplayName("物料类型")]
        public string MatType { get; set; } = string.Empty;
        [DisplayName("备货数量")]
        public decimal Quantity { get; set; }
        [DisplayName("单位")]
        public string Unit { get; set; } = string.Empty;
        [DisplayName("物料长文本")]
        public string LongText { get; set; } = string.Empty;
        [DisplayName("采购长文本")]
        public string PurLongText { get; set; } = string.Empty;
    }


    public class StockDataConverter : IBindingRangeValueConverter
    {
        public object ConvertToObject(CellValue value, Type requiredType, int columnIndex)
        {
            if (requiredType == typeof(decimal))
                return Convert.ToDecimal(value.NumericValue);
            else if (requiredType == typeof(string) && columnIndex == 2)
                return value.TextValue == "成品" ? "prod" : "semi";
            return value.TextValue;
        }

        public CellValue TryConvertFromObject(object value)
        {
            if (value is string strValue)
            {
                if (strValue == "prod")
                    return "成品";
                else if (strValue == "semi")
                    return "半成品";
                else
                    return strValue;
            }
            return CellValue.TryCreateFromObject(value);
        }
    }
}