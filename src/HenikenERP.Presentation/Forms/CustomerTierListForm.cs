using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Business.Services;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Presentation.Forms
{
    public partial class CustomerTierListForm : Form
    {
        private CustomerTierService _tierService;

        public CustomerTierListForm()
        {
            InitializeComponent();
            this.Text = "Hạng khách hàng";
            this.Load += CustomerTierListForm_Load;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvTiers.DoubleClick += (s, e) => BtnEdit_Click(s, e);
        }

        private void CustomerTierListForm_Load(object sender, EventArgs e)
        {
            try
            {
                _tierService = new CustomerTierService();
                LoadTiers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTiers()
        {
            var tiers = _tierService.GetAllTiers().ToList();
            dgvTiers.AutoGenerateColumns = false;
            dgvTiers.Columns.Clear();
            
            dgvTiers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tier_ID", HeaderText = "Mã hạng", Width = 120 });
            dgvTiers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tier_Name", HeaderText = "Tên hạng", Width = 200 });
            dgvTiers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Discount_Rate", HeaderText = "Tỷ lệ chiết khấu (%)", DefaultCellStyle = new DataGridViewCellStyle { Format = "P2" }, Width = 180 });
            
            dgvTiers.DataSource = tiers;
            lblTitle.Text = $"Hạng khách hàng ({tiers.Count} hạng)";
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadTiers();

        private void BtnAdd_Click(object sender, EventArgs e) => ShowTierDialog(null);

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTiers.SelectedRows.Count == 0) return;
            ShowTierDialog(dgvTiers.SelectedRows[0].DataBoundItem as CustomerTier);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTiers.SelectedRows.Count == 0) return;
            var tier = dgvTiers.SelectedRows[0].DataBoundItem as CustomerTier;
            if (tier == null) return;
            
            var customerCount = _tierService.GetCustomerCountByTier(tier.Tier_ID);
            if (customerCount > 0)
            {
                MessageBox.Show($"Không thể xóa hạng này vì có {customerCount} khách hàng đang sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (MessageBox.Show($"Xóa hạng '{tier.Tier_Name}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_tierService.DeleteTier(tier.Tier_ID))
                {
                    MessageBox.Show("Xóa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTiers();
                }
            }
        }

        private void ShowTierDialog(CustomerTier tier)
        {
            var dialog = new Form { Text = tier == null ? "Thêm hạng mới" : "Sửa hạng", Size = new System.Drawing.Size(450, 210), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
            
            var controls = new Control[]
            {
                new Label { Text = "Mã hạng:", Location = new System.Drawing.Point(15, 20), Width = 120 },
                new TextBox { Name = "txtId", Location = new System.Drawing.Point(145, 17), Width = 260, ReadOnly = tier != null, Text = tier?.Tier_ID ?? IDGenerator.GenerateIDWithTimestamp("TIER") },
                new Label { Text = "Tên hạng:", Location = new System.Drawing.Point(15, 55), Width = 120 },
                new TextBox { Name = "txtName", Location = new System.Drawing.Point(145, 52), Width = 260, Text = tier?.Tier_Name ?? "" },
                new Label { Text = "Chiết khấu (%):", Location = new System.Drawing.Point(15, 90), Width = 120 },
                new NumericUpDown { Name = "numDiscount", Location = new System.Drawing.Point(145, 87), Width = 260, DecimalPlaces = 2, Maximum = 100, Value = tier != null ? (decimal)(tier.Discount_Rate * 100) : 0 },
                new Button { Text = "Lưu", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(240, 130), Width = 75 },
                new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(325, 130), Width = 75 }
            };
            
            dialog.Controls.AddRange(controls);
            dialog.AcceptButton = controls[6] as Button;
            dialog.CancelButton = controls[7] as Button;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var tierToSave = new CustomerTier
                {
                    Tier_ID = (controls[1] as TextBox).Text.Trim(),
                    Tier_Name = (controls[3] as TextBox).Text.Trim(),
                    Discount_Rate = (decimal)(controls[5] as NumericUpDown).Value / 100
                };
                
                bool success = tier == null ? _tierService.CreateTier(tierToSave) : _tierService.UpdateTier(tierToSave);
                if (success)
                {
                    MessageBox.Show("Lưu thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTiers();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
