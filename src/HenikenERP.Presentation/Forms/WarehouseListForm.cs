using System;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Business.Services;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Presentation.Forms
{
    public partial class WarehouseListForm : Form
    {
        private WarehouseService _warehouseService;

        public WarehouseListForm()
        {
            InitializeComponent();
            this.Text = "Danh sách kho";
            this.Load += WarehouseListForm_Load;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvWarehouses.DoubleClick += (s, e) => BtnEdit_Click(s, e);
        }

        private void WarehouseListForm_Load(object sender, EventArgs e)
        {
            _warehouseService = new WarehouseService();
            LoadWarehouses();
        }

        private void LoadWarehouses()
        {
            var warehouses = _warehouseService.GetAllWarehouses().ToList();
            dgvWarehouses.AutoGenerateColumns = false;
            dgvWarehouses.Columns.Clear();
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Warehouse_ID", HeaderText = "Mã kho", Width = 100 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Location", HeaderText = "Vị trí", Width = 300 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Capacity", HeaderText = "Sức chứa", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }, Width = 120 });
            dgvWarehouses.DataSource = warehouses;
            lblTitle.Text = $"Danh sách kho ({warehouses.Count} kho)";
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadWarehouses();
        private void BtnAdd_Click(object sender, EventArgs e) => ShowDialog(null);
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvWarehouses.SelectedRows.Count > 0)
                ShowDialog(dgvWarehouses.SelectedRows[0].DataBoundItem as Warehouse);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvWarehouses.SelectedRows.Count == 0) return;
            var wh = dgvWarehouses.SelectedRows[0].DataBoundItem as Warehouse;
            if (MessageBox.Show($"Xóa kho '{wh.Location}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes && _warehouseService.DeleteWarehouse(wh.Warehouse_ID))
            {
                MessageBox.Show("Xóa thành công");
                LoadWarehouses();
            }
        }

        private void ShowDialog(Warehouse wh)
        {
            var dialog = new Form { Text = wh == null ? "Thêm kho" : "Sửa kho", Size = new System.Drawing.Size(400, 200), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog };
            var txtId = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 240, ReadOnly = wh != null, Text = wh?.Warehouse_ID ?? IDGenerator.GenerateIDWithTimestamp("WH") };
            var txtLoc = new TextBox { Location = new System.Drawing.Point(120, 55), Width = 240, Text = wh?.Location ?? "" };
            var numCap = new NumericUpDown { Location = new System.Drawing.Point(120, 90), Width = 240, Maximum = 999999999, Value = wh?.Capacity ?? 0 };
            var btnOK = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(200, 130) };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(285, 130) };
            dialog.Controls.AddRange(new Control[] {
                new Label { Text = "Mã kho:", Location = new System.Drawing.Point(15, 23), Width = 100 }, txtId,
                new Label { Text = "Vị trí:", Location = new System.Drawing.Point(15, 58), Width = 100 }, txtLoc,
                new Label { Text = "Sức chứa:", Location = new System.Drawing.Point(15, 93), Width = 100 }, numCap,
                btnOK, btnCancel
            });
            dialog.AcceptButton = btnOK;
            dialog.CancelButton = btnCancel;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var whToSave = new Warehouse { Warehouse_ID = txtId.Text.Trim(), Location = txtLoc.Text.Trim(), Capacity = (int)numCap.Value };
                if ((wh == null ? _warehouseService.CreateWarehouse(whToSave) : _warehouseService.UpdateWarehouse(whToSave)))
                {
                    MessageBox.Show("Lưu thành công");
                    LoadWarehouses();
                }
            }
        }
    }
}
