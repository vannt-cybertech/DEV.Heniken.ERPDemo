using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Business.Services;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Presentation.Forms
{
    public partial class ProductListForm : Form
    {
        private ProductService _productService;

        public ProductListForm()
        {
            InitializeComponent();
            this.Text = "Danh sách sản phẩm";
            this.Load += ProductListForm_Load;
            
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSearch.Click += BtnSearch_Click;
            btnClearSearch.Click += BtnClearSearch_Click;
            dgvProducts.DoubleClick += DgvProducts_DoubleClick;
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            try
            {
                _productService = new ProductService();
                LoadProducts();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading product list", ex);
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts(string searchText = null)
        {
            try
            {
                var products = _productService.GetAllProducts().ToList();
                
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    products = products.Where(p => 
                        p.Product_Name.ToLower().Contains(searchText.ToLower())).ToList();
                }
                
                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.Columns.Clear();
                
                dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Product_ID",
                    HeaderText = "Mã sản phẩm",
                    Name = "Product_ID",
                    Width = 150
                });
                
                dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Product_Name",
                    HeaderText = "Tên sản phẩm",
                    Name = "Product_Name",
                    Width = 300
                });
                
                dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Unit",
                    HeaderText = "Đơn vị",
                    Name = "Unit",
                    Width = 100
                });
                
                dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Category_ID",
                    HeaderText = "Danh mục",
                    Name = "Category_ID",
                    Width = 150
                });
                
                dgvProducts.DataSource = products;
                lblTitle.Text = $"Danh sách sản phẩm ({products.Count} sản phẩm)";
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading products", ex);
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadProducts();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ShowProductDialog(null);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var product = dgvProducts.SelectedRows[0].DataBoundItem as Product;
            if (product != null)
            {
                ShowProductDialog(product);
            }
        }

        private void DgvProducts_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var product = dgvProducts.SelectedRows[0].DataBoundItem as Product;
            if (product == null) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm '{product.Product_Name}'?", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                if (_productService.DeleteProduct(product.Product_ID))
                {
                    MessageBox.Show("Xóa sản phẩm thành công", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Không thể xóa sản phẩm. Có thể sản phẩm đang được sử dụng.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowProductDialog(Product product)
        {
            var dialog = new Form();
            dialog.Text = product == null ? "Thêm sản phẩm mới" : "Sửa sản phẩm";
            dialog.Size = new System.Drawing.Size(450, 250);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;
            
            var lblId = new Label { Text = "Mã sản phẩm:", Location = new System.Drawing.Point(15, 20), Width = 100 };
            var txtId = new TextBox { Location = new System.Drawing.Point(125, 17), Width = 280, ReadOnly = product != null };
            if (product != null) txtId.Text = product.Product_ID;
            else txtId.Text = IDGenerator.GenerateIDWithTimestamp("PRD");
            
            var lblName = new Label { Text = "Tên sản phẩm:", Location = new System.Drawing.Point(15, 55), Width = 100 };
            var txtName = new TextBox { Location = new System.Drawing.Point(125, 52), Width = 280 };
            if (product != null) txtName.Text = product.Product_Name;
            
            var lblUnit = new Label { Text = "Đơn vị:", Location = new System.Drawing.Point(15, 90), Width = 100 };
            var txtUnit = new TextBox { Location = new System.Drawing.Point(125, 87), Width = 280 };
            if (product != null) txtUnit.Text = product.Unit;
            
            var lblCategory = new Label { Text = "Danh mục:", Location = new System.Drawing.Point(15, 125), Width = 100 };
            var txtCategory = new TextBox { Location = new System.Drawing.Point(125, 122), Width = 280 };
            if (product != null) txtCategory.Text = product.Category_ID;
            
            var btnOK = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(240, 165), Width = 75 };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(325, 165), Width = 75 };
            
            dialog.Controls.AddRange(new Control[] { lblId, txtId, lblName, txtName, lblUnit, txtUnit, lblCategory, txtCategory, btnOK, btnCancel });
            dialog.AcceptButton = btnOK;
            dialog.CancelButton = btnCancel;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var productToSave = new Product
                {
                    Product_ID = txtId.Text.Trim(),
                    Product_Name = txtName.Text.Trim(),
                    Unit = txtUnit.Text.Trim(),
                    Category_ID = string.IsNullOrWhiteSpace(txtCategory.Text) ? null : txtCategory.Text.Trim()
                };
                
                bool success;
                if (product == null)
                {
                    success = _productService.CreateProduct(productToSave);
                }
                else
                {
                    success = _productService.UpdateProduct(productToSave);
                }
                
                if (success)
                {
                    MessageBox.Show(
                        product == null ? "Thêm sản phẩm thành công" : "Cập nhật sản phẩm thành công", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu sản phẩm", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
