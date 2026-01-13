using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly User _currentUser;
        private UnitOfWork _unitOfWork;

        public DashboardForm(User currentUser = null)
        {
            _currentUser = currentUser;
            InitializeComponent();
            this.Text = "Bảng điều khiển";
            this.Load += DashboardForm_Load;

            if (_currentUser != null)
            {
                lblWelcome.Text = $"Xin chào, {_currentUser.Username}!";
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);
            InitializeKpiCards();

            try
            {
                var context = new DatabaseContext();
                _unitOfWork = new UnitOfWork(context);
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading dashboard data", ex);
                MessageBox.Show($"Lỗi khi tải dữ liệu bảng điều khiển: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboardData()
        {
            // Load KPIs
            int productCount = _unitOfWork.Products.Count();
            int customerCount = _unitOfWork.Customers.Count();
            int warehouseCount = _unitOfWork.Warehouses.Count();

            lblProductsValue.Text = productCount.ToString("N0");
            lblCustomersValue.Text = customerCount.ToString("N0");
            lblWarehousesValue.Text = warehouseCount.ToString("N0");

            // Load inventory totals
            var inventoryRepo = _unitOfWork.Inventories as InventoryRepository;
            if (inventoryRepo != null)
            {
                var inventoryTotals = inventoryRepo.GetInventoryTotals();
                int availableInventory = inventoryTotals.ContainsKey("TotalAvailable") 
                    ? inventoryTotals["TotalAvailable"] : 0;
                lblInventoryValue.Text = availableInventory.ToString("N0");
            }

            // Load recent orders
            var orderRepo = _unitOfWork.Orders as OrderRepository;
            if (orderRepo != null)
            {
                var recentOrders = orderRepo.GetRecentOrders(10).ToList();
                
                // Configure DataGridView columns
                dgvRecentOrders.AutoGenerateColumns = false;
                dgvRecentOrders.Columns.Clear();
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Order_ID",
                    HeaderText = "Mã đơn hàng",
                    Name = "Order_ID",
                    FillWeight = 20
                });
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Order_Date",
                    HeaderText = "Ngày đặt",
                    Name = "Order_Date",
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                    FillWeight = 15
                });
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Customer.Customer_Name",
                    HeaderText = "Khách hàng",
                    Name = "Customer_Name",
                    FillWeight = 25
                });
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Warehouse.Location",
                    HeaderText = "Kho",
                    Name = "Warehouse_Location",
                    FillWeight = 20
                });
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Total_Amount",
                    HeaderText = "Tổng tiền",
                    Name = "Total_Amount",
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                    FillWeight = 15
                });
                
                dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Status",
                    HeaderText = "Trạng thái",
                    Name = "Status",
                    FillWeight = 15
                });

                // Bind data
                dgvRecentOrders.DataSource = recentOrders;

                // Format status column display
                foreach (DataGridViewRow row in dgvRecentOrders.Rows)
                {
                    if (row.Cells["Status"].Value != null)
                    {
                        string status = row.Cells["Status"].Value.ToString();
                        row.Cells["Status"].Value = GetStatusDisplayText(status);
                    }
                    
                    if (row.Cells["Customer_Name"].Value == null && row.DataBoundItem is Order order)
                    {
                        row.Cells["Customer_Name"].Value = order.Customer?.Customer_Name ?? "";
                    }
                    
                    if (row.Cells["Warehouse_Location"].Value == null && row.DataBoundItem is Order order2)
                    {
                        row.Cells["Warehouse_Location"].Value = order2.Warehouse?.Location ?? "";
                    }
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

        private void InitializeKpiCards()
        {
            try
            {
                // Card look
                StyleKpiPanel(pnlProducts, lblProductsTitle, lblProductsValue, ThemeColors.Primary);
                StyleKpiPanel(pnlCustomers, lblCustomersTitle, lblCustomersValue, ThemeColors.Info);
                StyleKpiPanel(pnlWarehouses, lblWarehousesTitle, lblWarehousesValue, ThemeColors.PrimaryDark);
                StyleKpiPanel(pnlInventory, lblInventoryTitle, lblInventoryValue, ThemeColors.Success);

                LayoutKpiCards();
                grpKPIs.Resize -= GrpKPIs_Resize;
                grpKPIs.Resize += GrpKPIs_Resize;
            }
            catch { /* ignore */ }
        }

        private void GrpKPIs_Resize(object sender, EventArgs e)
        {
            LayoutKpiCards();
        }

        private void LayoutKpiCards()
        {
            if (grpKPIs == null) return;

            var cards = new[] { pnlProducts, pnlCustomers, pnlWarehouses, pnlInventory };
            if (cards.Any(c => c == null)) return;

            int spacing = 12;
            int padding = 12;

            // Inside GroupBox padding is not accounted in ClientRectangle nicely; keep a safe inset.
            var area = grpKPIs.ClientRectangle;
            int x = padding;
            int y = 24; // leave space for groupbox title
            int height = Math.Max(72, area.Height - y - padding);
            int totalWidth = Math.Max(0, area.Width - padding * 2 - spacing * (cards.Length - 1));
            int cardWidth = Math.Max(160, totalWidth / cards.Length);

            foreach (var p in cards)
            {
                p.Location = new Point(x, y);
                p.Size = new Size(cardWidth, height);
                x += cardWidth + spacing;
            }
        }

        private void StyleKpiPanel(Panel panel, Label title, Label value, Color accent)
        {
            if (panel == null || title == null || value == null) return;

            panel.BorderStyle = BorderStyle.None;
            panel.BackColor = ThemeColors.Surface;
            panel.Padding = new Padding(12, 10, 12, 10);

            // Add left accent strip once
            if (!(panel.Tag is string tag) || tag != "kpi-styled")
            {
                var strip = new Panel
                {
                    Width = 5,
                    Dock = DockStyle.Left,
                    BackColor = accent
                };
                panel.Controls.Add(strip);
                strip.BringToFront();
                panel.Tag = "kpi-styled";
            }

            title.Dock = DockStyle.Top;
            title.AutoSize = false;
            title.Font = ThemeFonts.LabelBold;
            title.ForeColor = ThemeColors.TextSecondary;
            title.TextAlign = ContentAlignment.MiddleLeft;
            title.Height = Math.Max(24, title.PreferredHeight + 6);

            value.Dock = DockStyle.Fill;
            value.Font = ThemeFonts.TitleLarge;
            value.ForeColor = ThemeColors.Primary;
            value.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}

