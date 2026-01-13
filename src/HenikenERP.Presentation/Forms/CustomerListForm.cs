using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Business.Services;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Presentation.Forms
{
    public partial class CustomerListForm : Form
    {
        private CustomerService _customerService;
        private UnitOfWork _unitOfWork;

        public CustomerListForm()
        {
            InitializeComponent();
            this.Text = "Danh sách khách hàng";
            this.Load += CustomerListForm_Load;
            
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnFilter.Click += BtnFilter_Click;
            btnClearFilter.Click += BtnClearFilter_Click;
            dgvCustomers.DoubleClick += DgvCustomers_DoubleClick;
        }

        private void CustomerListForm_Load(object sender, EventArgs e)
        {
            try
            {
                var context = new DatabaseContext();
                _unitOfWork = new UnitOfWork(context);
                _customerService = new CustomerService(_unitOfWork);
                
                InitializeFilters();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading customer list", ex);
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeFilters()
        {
            // Tier filter
            var tiers = _unitOfWork.CustomerTiers.GetAll().ToList();
            cboTier.Items.Clear();
            cboTier.Items.Add(new ComboBoxItem { Text = "-- Tất cả --", Value = null });
            foreach (var tier in tiers)
            {
                cboTier.Items.Add(new ComboBoxItem { Text = tier.Tier_Name, Value = tier.Tier_ID });
            }
            cboTier.SelectedIndex = 0;
            
            // Status filter
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new ComboBoxItem { Text = "-- Tất cả --", Value = null });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Đang hoạt động", Value = "active" });
            cboStatus.Items.Add(new ComboBoxItem { Text = "Ngừng hoạt động", Value = "inactive" });
            cboStatus.SelectedIndex = 0;
        }

        private void LoadCustomers(string searchText = null, string tierId = null, bool? isActive = null)
        {
            try
            {
                var customers = _customerService.GetAllCustomers().ToList();
                
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    customers = customers.Where(c => 
                        c.Customer_Name.ToLower().Contains(searchText.ToLower()) ||
                        (!string.IsNullOrEmpty(c.Phone_Number) && c.Phone_Number.Contains(searchText))).ToList();
                }
                
                if (!string.IsNullOrWhiteSpace(tierId))
                {
                    customers = customers.Where(c => c.Tier_ID == tierId).ToList();
                }
                
                if (isActive.HasValue)
                {
                    customers = customers.Where(c => c.Is_Active == isActive.Value).ToList();
                }
                
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.Columns.Clear();
                
                dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Customer_ID",
                    HeaderText = "Mã KH",
                    Name = "Customer_ID",
                    Width = 100
                });
                
                dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Customer_Name",
                    HeaderText = "Tên khách hàng",
                    Name = "Customer_Name",
                    Width = 250
                });
                
                dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Phone_Number",
                    HeaderText = "Số điện thoại",
                    Name = "Phone_Number",
                    Width = 120
                });
                
                dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Tier.Tier_Name",
                    HeaderText = "Hạng",
                    Name = "Tier_Name",
                    Width = 120
                });
                
                dgvCustomers.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = "Is_Active",
                    HeaderText = "Hoạt động",
                    Name = "Is_Active",
                    Width = 80
                });
                
                dgvCustomers.DataSource = customers;
                
                foreach (DataGridViewRow row in dgvCustomers.Rows)
                {
                    if (row.DataBoundItem is Customer customer)
                    {
                        row.Cells["Tier_Name"].Value = customer.Tier?.Tier_Name ?? "";
                        
                        if (!customer.Is_Active)
                        {
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
                
                lblTitle.Text = $"Danh sách khách hàng ({customers.Count} khách hàng)";
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading customers", ex);
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            string tierId = cboTier.SelectedItem is ComboBoxItem tierItem ? tierItem.Value : null;
            bool? isActive = null;
            if (cboStatus.SelectedItem is ComboBoxItem statusItem && statusItem.Value != null)
            {
                isActive = statusItem.Value == "active";
            }
            
            LoadCustomers(txtSearch.Text, tierId, isActive);
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cboTier.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            LoadCustomers();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ShowCustomerDialog(null);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var customer = dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
            if (customer != null)
            {
                ShowCustomerDialog(customer);
            }
        }

        private void DgvCustomers_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var customer = dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
            if (customer == null) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng '{customer.Customer_Name}'?", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                if (_customerService.DeleteCustomer(customer.Customer_ID))
                {
                    MessageBox.Show("Xóa khách hàng thành công", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Không thể xóa khách hàng. Có thể khách hàng đang có đơn hàng.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowCustomerDialog(Customer customer)
        {
            var dialog = new Form();
            dialog.Text = customer == null ? "Thêm khách hàng mới" : "Sửa thông tin khách hàng";
            dialog.Size = new System.Drawing.Size(450, 280);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;
            
            var lblId = new Label { Text = "Mã khách hàng:", Location = new System.Drawing.Point(15, 20), Width = 100 };
            var txtId = new TextBox { Location = new System.Drawing.Point(125, 17), Width = 280, ReadOnly = customer != null };
            if (customer != null) txtId.Text = customer.Customer_ID;
            else txtId.Text = IDGenerator.GenerateIDWithTimestamp("CUST");
            
            var lblName = new Label { Text = "Tên khách hàng:", Location = new System.Drawing.Point(15, 55), Width = 100 };
            var txtName = new TextBox { Location = new System.Drawing.Point(125, 52), Width = 280 };
            if (customer != null) txtName.Text = customer.Customer_Name;
            
            var lblPhone = new Label { Text = "Số điện thoại:", Location = new System.Drawing.Point(15, 90), Width = 100 };
            var txtPhone = new TextBox { Location = new System.Drawing.Point(125, 87), Width = 280 };
            if (customer != null) txtPhone.Text = customer.Phone_Number;
            
            var lblTier = new Label { Text = "Hạng khách hàng:", Location = new System.Drawing.Point(15, 125), Width = 100 };
            var cboDialogTier = new ComboBox { Location = new System.Drawing.Point(125, 122), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            var tiers = _unitOfWork.CustomerTiers.GetAll().ToList();
            cboDialogTier.DisplayMember = "Tier_Name";
            cboDialogTier.ValueMember = "Tier_ID";
            cboDialogTier.DataSource = tiers;
            if (customer != null && customer.Tier_ID != null)
            {
                cboDialogTier.SelectedValue = customer.Tier_ID;
            }
            
            var chkActive = new CheckBox { Text = "Đang hoạt động", Location = new System.Drawing.Point(125, 160), Width = 150 };
            if (customer != null) chkActive.Checked = customer.Is_Active;
            else chkActive.Checked = true;
            
            var btnOK = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(240, 200), Width = 75 };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(325, 200), Width = 75 };
            
            dialog.Controls.AddRange(new Control[] { lblId, txtId, lblName, txtName, lblPhone, txtPhone, lblTier, cboDialogTier, chkActive, btnOK, btnCancel });
            dialog.AcceptButton = btnOK;
            dialog.CancelButton = btnCancel;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var customerToSave = new Customer
                {
                    Customer_ID = txtId.Text.Trim(),
                    Customer_Name = txtName.Text.Trim(),
                    Phone_Number = txtPhone.Text.Trim(),
                    Tier_ID = cboDialogTier.SelectedValue?.ToString(),
                    Is_Active = chkActive.Checked
                };
                
                bool success;
                if (customer == null)
                {
                    success = _customerService.CreateCustomer(customerToSave);
                }
                else
                {
                    success = _customerService.UpdateCustomer(customerToSave);
                }
                
                if (success)
                {
                    MessageBox.Show(
                        customer == null ? "Thêm khách hàng thành công" : "Cập nhật khách hàng thành công", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu khách hàng", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
