using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Business.Services;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;

namespace HenikenERP.Presentation.Forms
{
    public partial class InventoryForm : Form
    {
        private UnitOfWork _unitOfWork;

        public InventoryForm()
        {
            InitializeComponent();
            this.Load += InventoryForm_Load;
            this.Shown += InventoryForm_Shown;
            btnRefresh.Click += (s, e) => LoadData();
            dgvInventory.DataBindingComplete += DgvInventory_DataBindingComplete;
        }
        
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            _unitOfWork = new UnitOfWork(new DatabaseContext());
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
    }
}
