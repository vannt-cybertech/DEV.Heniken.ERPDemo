using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    public partial class ReportForm : Form
    {
        private UnitOfWork _unitOfWork;

        public ReportForm()
        {
            InitializeComponent();
            this.Load += ReportForm_Load;
            btnGenerate.Click += (s, e) => GenerateReport();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);

            _unitOfWork = new UnitOfWork(new DatabaseContext());
            cboReportType.SelectedIndex = 0;
        }

        private void GenerateReport()
        {
            dgvReport.DataSource = null;
            dgvReport.Columns.Clear();
            
            switch (cboReportType.SelectedIndex)
            {
                case 0: // Sales Report
                    var orders = _unitOfWork.Orders.GetAll().Where(o => o.Order_Date >= dtpFrom.Value && o.Order_Date <= dtpTo.Value).ToList();
                    dgvReport.AutoGenerateColumns = true;
                    dgvReport.DataSource = orders.Select(o => new {
                        OrderID = o.Order_ID,
                        Customer = o.Customer?.Customer_Name,
                        Date = o.Order_Date,
                        Total = o.Total_Amount,
                        Status = o.Status
                    }).ToList();
                    lblSummary.Text = $"Tổng doanh thu: {orders.Sum(o => o.Total_Amount):N0} VNĐ";
                    break;
                case 1: // Inventory Report
                    var inv = _unitOfWork.Inventories.GetAll().ToList();
                    dgvReport.AutoGenerateColumns = true;
                    dgvReport.DataSource = inv.Select(i => new {
                        Warehouse = i.Warehouse_ID,
                        Product = i.Product_ID,
                        OnHand = i.Quantity_On_Hand,
                        Reserved = i.Quantity_Reserved,
                        Available = i.Available_Quantity
                    }).ToList();
                    lblSummary.Text = $"Tổng tồn kho: {inv.Sum(i => i.Quantity_On_Hand):N0}";
                    break;
                case 2: // Customer Report
                    var customers = _unitOfWork.Customers.GetAll().ToList();
                    dgvReport.AutoGenerateColumns = true;
                    dgvReport.DataSource = customers.Select(c => new {
                        ID = c.Customer_ID,
                        Name = c.Customer_Name,
                        Phone = c.Phone_Number,
                        Tier = c.Tier?.Tier_Name,
                        Active = c.Is_Active
                    }).ToList();
                    lblSummary.Text = $"Tổng số KH: {customers.Count}, Đang hoạt động: {customers.Count(c => c.Is_Active)}";
                    break;
            }
        }
    }
}
