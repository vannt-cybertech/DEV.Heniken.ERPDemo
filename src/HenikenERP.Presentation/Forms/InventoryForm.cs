using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Business.Services;
using HenikenERP.Core.DTOs;
using HenikenERP.Core.Enums;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    public partial class InventoryForm : Form
    {
        private UnitOfWork _unitOfWork;
        private InventoryService _inventoryService;

        public InventoryForm()
        {
            InitializeComponent();
            this.Load += InventoryForm_Load;
            this.Shown += InventoryForm_Shown;
            btnRefresh.Click += (s, e) => LoadData();
            btnStockIn.Click += (s, e) => ShowStockTransactionDialog(StockTransactionType.StockIn);
            btnAdjust.Click += (s, e) => ShowStockTransactionDialog(StockTransactionType.Adjustment);
            dgvInventory.DataBindingComplete += DgvInventory_DataBindingComplete;
        }
        
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);

            _unitOfWork = new UnitOfWork(new DatabaseContext());
            _inventoryService = new InventoryService();
        }
        
        private void InventoryForm_Shown(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void DgvInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PopulateNames();
        }

        private void LoadData()
        {
            var inventoryRepo = _unitOfWork.Inventories as InventoryRepository;
            var inventory = inventoryRepo?.GetInventoryWithProducts().ToList();
            
            if (inventory == null || inventory.Count == 0)
            {
                inventory = _unitOfWork.Inventories.GetAll().ToList();
            }
            
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.Columns.Clear();
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Kho", Name = "WarehouseLocation", Width = 180 });
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sản phẩm", Name = "ProductName", Width = 280 });
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity_On_Hand", HeaderText = "Tồn kho", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity_Reserved", HeaderText = "Đã đặt", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Available_Quantity", HeaderText = "Khả dụng", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            
            dgvInventory.DataSource = inventory;
        }
        
        private void PopulateNames()
        {
            // Manually populate warehouse and product names after data binding is complete
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.DataBoundItem is Core.Entities.Inventory inv)
                {
                    // Get warehouse name
                    if (inv.Warehouse != null)
                    {
                        row.Cells["WarehouseLocation"].Value = inv.Warehouse.Location;
                    }
                    else if (!string.IsNullOrEmpty(inv.Warehouse_ID))
                    {
                        var warehouse = _unitOfWork.Warehouses.GetById(inv.Warehouse_ID);
                        row.Cells["WarehouseLocation"].Value = warehouse?.Location ?? inv.Warehouse_ID;
                    }
                    
                    // Get product name
                    if (inv.Product != null)
                    {
                        row.Cells["ProductName"].Value = inv.Product.Product_Name;
                    }
                    else if (!string.IsNullOrEmpty(inv.Product_ID))
                    {
                        var product = _unitOfWork.Products.GetById(inv.Product_ID);
                        row.Cells["ProductName"].Value = product?.Product_Name ?? inv.Product_ID;
                    }
                    
                    // Highlight low stock items
                    if (inv.Available_Quantity < 10)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                    }
                }
            }
        }

        private void ShowStockTransactionDialog(StockTransactionType type)
        {
            // Preselect from grid if available
            Inventory selectedInv = null;
            if (dgvInventory?.SelectedRows != null && dgvInventory.SelectedRows.Count > 0)
            {
                selectedInv = dgvInventory.SelectedRows[0].DataBoundItem as Inventory;
            }

            var dialog = new Form
            {
                Text = type == StockTransactionType.StockIn ? "Nhập kho" : "Điều chỉnh tồn kho",
                Size = new Size(520, 280),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ThemeHelper.ApplyTheme(dialog);

            var lblWarehouse = new Label { Text = "Kho:", AutoSize = true, Location = new Point(16, 20) };
            var cboWarehouse = new ComboBox
            {
                Location = new Point(140, 16),
                Width = 340,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "Location",
                ValueMember = "Warehouse_ID"
            };

            var lblProduct = new Label { Text = "Sản phẩm:", AutoSize = true, Location = new Point(16, 62) };
            var cboProduct = new ComboBox
            {
                Location = new Point(140, 58),
                Width = 340,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "Product_Name",
                ValueMember = "Product_ID"
            };

            var lblQty = new Label
            {
                Text = type == StockTransactionType.StockIn ? "Số lượng nhập:" : "Tồn kho (mới):",
                AutoSize = true,
                Location = new Point(16, 104)
            };
            var numQty = new NumericUpDown
            {
                Location = new Point(140, 100),
                Width = 160,
                Minimum = 0,
                Maximum = 1000000000,
                ThousandsSeparator = true
            };

            var lblNote = new Label { Text = "Ghi chú:", AutoSize = true, Location = new Point(16, 146) };
            var txtNote = new TextBox
            {
                Location = new Point(140, 142),
                Width = 340
            };

            var btnOk = new Button { Text = "Xác nhận", DialogResult = DialogResult.OK, Width = 110, Height = 32, Location = new Point(250, 190) };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90, Height = 32, Location = new Point(370, 190) };

            dialog.Controls.AddRange(new Control[]
            {
                lblWarehouse, cboWarehouse,
                lblProduct, cboProduct,
                lblQty, numQty,
                lblNote, txtNote,
                btnOk, btnCancel
            });
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            // Load dropdown data
            var warehouses = _unitOfWork.Warehouses.GetAll().OrderBy(w => w.Location).ToList();
            var products = _unitOfWork.Products.GetAll().OrderBy(p => p.Product_Name).ToList();
            cboWarehouse.DataSource = warehouses;
            cboProduct.DataSource = products;

            // Preselect values
            if (selectedInv != null)
            {
                if (!string.IsNullOrWhiteSpace(selectedInv.Warehouse_ID))
                    cboWarehouse.SelectedValue = selectedInv.Warehouse_ID;
                if (!string.IsNullOrWhiteSpace(selectedInv.Product_ID))
                    cboProduct.SelectedValue = selectedInv.Product_ID;

                if (type == StockTransactionType.Adjustment)
                {
                    numQty.Value = Math.Max(0, selectedInv.Quantity_On_Hand);
                }
            }

            if (dialog.ShowDialog() != DialogResult.OK) return;

            if (cboWarehouse.SelectedValue == null || cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn kho và sản phẩm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int qty = (int)numQty.Value;
            if (type == StockTransactionType.StockIn && qty <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0.", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tx = new StockTransactionDTO
            {
                TransactionType = type,
                Warehouse_ID = cboWarehouse.SelectedValue.ToString(),
                Product_ID = cboProduct.SelectedValue.ToString(),
                Quantity = qty,
                Notes = string.IsNullOrWhiteSpace(txtNote.Text) ? null : txtNote.Text.Trim()
            };

            bool ok = _inventoryService.ProcessStockTransaction(tx);
            if (!ok)
            {
                MessageBox.Show("Thao tác tồn kho thất bại. Vui lòng kiểm tra lại số lượng/kho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadData();
        }
    }
}
