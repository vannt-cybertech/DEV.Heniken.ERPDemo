using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Business.Services;
using HenikenERP.Common.Helpers;
using HenikenERP.Common.Constants;

namespace HenikenERP.Presentation.Forms
{
    public partial class OrderListForm : Form
    {
        private UnitOfWork _unitOfWork;
        private OrderService _orderService;
        private User _currentUser;

        public OrderListForm(User currentUser = null)
        {
            InitializeComponent();
            this.Text = "Danh sách đơn hàng";
            _currentUser = currentUser;
            this.Load += OrderListForm_Load;
            
            // Wire up event handlers
            btnNewOrder.Click += BtnNewOrder_Click;
            btnViewDetails.Click += BtnViewDetails_Click;
            btnChangeStatus.Click += BtnChangeStatus_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnFilter.Click += BtnFilter_Click;
            btnClearFilter.Click += BtnClearFilter_Click;
            dgvOrders.DoubleClick += DgvOrders_DoubleClick;
        }

        private void OrderListForm_Load(object sender, EventArgs e)
        {
            try
            {
                var context = new DatabaseContext();
                _unitOfWork = new UnitOfWork(context);
                _orderService = new OrderService(_unitOfWork);
                
                InitializeFilters();
                LoadOrders();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading order list", ex);
                MessageBox.Show($"Lỗi khi tải danh sách đơn hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeFilters()
        {
            // Initialize status filter
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new ComboBoxItem { Text = "-- Tất cả --", Value = "" });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Đã tạo", Value = AppConstants.ORDER_STATUS_CREATED });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Chờ duyệt", Value = AppConstants.ORDER_STATUS_PENDING_APPROVAL });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Đã duyệt", Value = AppConstants.ORDER_STATUS_APPROVED });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Đang giao", Value = AppConstants.ORDER_STATUS_DELIVERING });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Hoàn thành", Value = AppConstants.ORDER_STATUS_COMPLETED });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Đã hủy", Value = AppConstants.ORDER_STATUS_CANCELLED });
            cboStatus.SelectedIndex = 0;
            
            // Set default date range (last 30 days)
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now.AddDays(-30);
        }

        private void LoadOrders(DateTime? startDate = null, DateTime? endDate = null, string status = null)
        {
            try
            {
                var orderRepo = _unitOfWork.Orders as OrderRepository;
                if (orderRepo == null) return;
                
                var orders = orderRepo.GetOrdersWithFilters(startDate, endDate, status, null).ToList();
                
                // Configure DataGridView
                dgvOrders.AutoGenerateColumns = false;
                dgvOrders.Columns.Clear();
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Order_ID",
                    HeaderText = "Mã đơn hàng",
                    Name = "Order_ID",
                    Width = 120
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Order_Date",
                    HeaderText = "Ngày đặt",
                    Name = "Order_Date",
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" },
                    Width = 130
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Customer.Customer_Name",
                    HeaderText = "Khách hàng",
                    Name = "Customer_Name",
                    Width = 200
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Warehouse.Location",
                    HeaderText = "Kho",
                    Name = "Warehouse_Location",
                    Width = 150
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Total_Amount",
                    HeaderText = "Tổng tiền",
                    Name = "Total_Amount",
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Format = "N0", 
                        Alignment = DataGridViewContentAlignment.MiddleRight 
                    },
                    Width = 120
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Status",
                    HeaderText = "Trạng thái",
                    Name = "Status",
                    Width = 120
                });
                
                dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Created_By",
                    HeaderText = "Người tạo",
                    Name = "Created_By",
                    Width = 100
                });
                
                dgvOrders.DataSource = orders;
                
                // Format data in rows
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    if (row.DataBoundItem is Order order)
                    {
                        row.Cells["Status"].Value = GetStatusDisplayText(order.Status);
                        row.Cells["Customer_Name"].Value = order.Customer?.Customer_Name ?? "";
                        row.Cells["Warehouse_Location"].Value = order.Warehouse?.Location ?? "";
                    }
                }
                
                lblTitle.Text = $"Danh sách đơn hàng ({orders.Count} đơn)";
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading orders", ex);
                MessageBox.Show($"Lỗi khi tải danh sách đơn hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            DateTime? startDate = dtpStartDate.Checked ? (DateTime?)dtpStartDate.Value.Date : null;
            DateTime? endDate = dtpEndDate.Checked ? (DateTime?)dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1) : null;
            string status = cboStatus.SelectedItem is ComboBoxItem item ? item.Value : "";
            
            LoadOrders(startDate, endDate, string.IsNullOrEmpty(status) ? null : status);
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Now.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadOrders();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            var createForm = new OrderCreateForm(_currentUser);
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                LoadOrders();
            }
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            ViewSelectedOrder();
        }

        private void DgvOrders_DoubleClick(object sender, EventArgs e)
        {
            ViewSelectedOrder();
        }

        private void ViewSelectedOrder()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var order = dgvOrders.SelectedRows[0].DataBoundItem as Order;
            if (order != null)
            {
                MessageBox.Show($"Chi tiết đơn hàng: {order.Order_ID}\n" +
                    $"Khách hàng: {order.Customer?.Customer_Name}\n" +
                    $"Kho: {order.Warehouse?.Location}\n" +
                    $"Tổng tiền: {order.Total_Amount:N0} VNĐ\n" +
                    $"Trạng thái: {GetStatusDisplayText(order.Status)}", 
                    "Chi tiết đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnChangeStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var order = dgvOrders.SelectedRows[0].DataBoundItem as Order;
            if (order == null) return;
            
            // Show status selection dialog
            var statusForm = new Form();
            statusForm.Text = "Đổi trạng thái đơn hàng";
            statusForm.Size = new System.Drawing.Size(350, 180);
            statusForm.StartPosition = FormStartPosition.CenterParent;
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            statusForm.MaximizeBox = false;
            statusForm.MinimizeBox = false;
            
            var lblCurrent = new Label();
            lblCurrent.Text = $"Trạng thái hiện tại: {GetStatusDisplayText(order.Status)}";
            lblCurrent.Location = new System.Drawing.Point(15, 15);
            lblCurrent.Size = new System.Drawing.Size(300, 20);
            
            var lblNew = new Label();
            lblNew.Text = "Trạng thái mới:";
            lblNew.Location = new System.Drawing.Point(15, 45);
            lblNew.Size = new System.Drawing.Size(100, 20);
            
            var cboNewStatus = new ComboBox();
            cboNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNewStatus.Location = new System.Drawing.Point(15, 68);
            cboNewStatus.Size = new System.Drawing.Size(300, 21);
            cboNewStatus.Items.AddRange(new object[] {
                new ComboBoxItem { Text = "Đã tạo", Value = AppConstants.ORDER_STATUS_CREATED },
                new ComboBoxItem { Text = "Chờ duyệt", Value = AppConstants.ORDER_STATUS_PENDING_APPROVAL },
                new ComboBoxItem { Text = "Đã duyệt", Value = AppConstants.ORDER_STATUS_APPROVED },
                new ComboBoxItem { Text = "Đang giao", Value = AppConstants.ORDER_STATUS_DELIVERING },
                new ComboBoxItem { Text = "Hoàn thành", Value = AppConstants.ORDER_STATUS_COMPLETED },
                new ComboBoxItem { Text = "Đã hủy", Value = AppConstants.ORDER_STATUS_CANCELLED }
            });
            
            var btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new System.Drawing.Point(160, 105);
            btnOK.Size = new System.Drawing.Size(75, 23);
            
            var btnCancel = new Button();
            btnCancel.Text = "Hủy";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(240, 105);
            btnCancel.Size = new System.Drawing.Size(75, 23);
            
            statusForm.Controls.AddRange(new Control[] { lblCurrent, lblNew, cboNewStatus, btnOK, btnCancel });
            statusForm.AcceptButton = btnOK;
            statusForm.CancelButton = btnCancel;
            
            if (statusForm.ShowDialog() == DialogResult.OK && cboNewStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                var result = _orderService.UpdateOrderStatus(order.Order_ID, selectedItem.Value);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
                else
                {
                    MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetStatusDisplayText(string status)
        {
            switch (status)
            {
                case "Created": return "Đã tạo";
                case "Pending_Approval": return "Chờ duyệt";
                case "Approved": return "Đã duyệt";
                case "Delivering": return "Đang giao";
                case "Completed": return "Hoàn thành";
                case "Cancelled": return "Đã hủy";
                default: return status;
            }
        }

        // Helper class for ComboBox items
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }
    }
}

