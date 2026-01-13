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
    public partial class PriceListForm : Form
    {
        private PriceListService _priceService;
        private UnitOfWork _unitOfWork;

        public PriceListForm()
        {
            InitializeComponent();
            this.Load += (s, e) => { _unitOfWork = new UnitOfWork(new DatabaseContext()); _priceService = new PriceListService(_unitOfWork); LoadData(); };
            btnAdd.Click += (s, e) => ShowDialog(null);
            btnEdit.Click += (s, e) => { if (dgvPrices.SelectedRows.Count > 0) ShowDialog(dgvPrices.SelectedRows[0].DataBoundItem as PriceList); };
            btnDelete.Click += (s, e) => {
                if (dgvPrices.SelectedRows.Count == 0) return;
                var price = dgvPrices.SelectedRows[0].DataBoundItem as PriceList;
                if (MessageBox.Show($"Xóa bảng giá này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes && _priceService.DeletePrice(price.Price_ID))
                { MessageBox.Show("Xóa thành công"); LoadData(); }
            };
            btnRefresh.Click += (s, e) => LoadData();
        }

        private void LoadData()
        {
            var prices = _priceService.GetAllPrices().ToList();
            dgvPrices.AutoGenerateColumns = false;
            dgvPrices.Columns.Clear();
            dgvPrices.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Product.Product_Name", HeaderText = "Sản phẩm", Width = 200 });
            dgvPrices.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tier.Tier_Name", HeaderText = "Hạng KH", Width = 120 });
            dgvPrices.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Unit_Price", HeaderText = "Đơn giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }, Width = 120 });
            dgvPrices.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Effective_Date", HeaderText = "Từ ngày", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, Width = 100 });
            dgvPrices.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Expiry_Date", HeaderText = "Đến ngày", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, Width = 100 });
            dgvPrices.DataSource = prices;
            foreach (DataGridViewRow row in dgvPrices.Rows)
            {
                if (row.DataBoundItem is PriceList p)
                {
                    row.Cells[0].Value = p.Product?.Product_Name;
                    row.Cells[1].Value = p.Tier?.Tier_Name;
                }
            }
        }

        private void ShowDialog(PriceList price)
        {
            var dialog = new Form { Text = price == null ? "Thêm giá" : "Sửa giá", Size = new System.Drawing.Size(450, 280), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog };
            var cboProduct = new ComboBox { Location = new System.Drawing.Point(120, 20), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Product_Name", ValueMember = "Product_ID", DataSource = _unitOfWork.Products.GetAll().ToList() };
            var cboTier = new ComboBox { Location = new System.Drawing.Point(120, 55), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Tier_Name", ValueMember = "Tier_ID", DataSource = _unitOfWork.CustomerTiers.GetAll().ToList() };
            var numPrice = new NumericUpDown { Location = new System.Drawing.Point(120, 90), Width = 280, Maximum = 999999999, Value = price?.Unit_Price ?? 0 };
            var dtpFrom = new DateTimePicker { Location = new System.Drawing.Point(120, 125), Width = 280, Value = price?.Effective_Date ?? DateTime.Now };
            var dtpTo = new DateTimePicker { Location = new System.Drawing.Point(120, 160), Width = 280, Value = price?.Expiry_Date ?? DateTime.Now.AddYears(1), ShowCheckBox = true, Checked = price?.Expiry_Date != null };
            if (price != null) { cboProduct.SelectedValue = price.Product_ID; cboTier.SelectedValue = price.Tier_ID; }
            var btnOK = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(260, 205) };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(345, 205) };
            dialog.Controls.AddRange(new Control[] { new Label { Text = "Sản phẩm:", Location = new System.Drawing.Point(15, 23), Width = 100 }, cboProduct,
                new Label { Text = "Hạng KH:", Location = new System.Drawing.Point(15, 58), Width = 100 }, cboTier,
                new Label { Text = "Đơn giá:", Location = new System.Drawing.Point(15, 93), Width = 100 }, numPrice,
                new Label { Text = "Từ ngày:", Location = new System.Drawing.Point(15, 128), Width = 100 }, dtpFrom,
                new Label { Text = "Đến ngày:", Location = new System.Drawing.Point(15, 163), Width = 100 }, dtpTo, btnOK, btnCancel });
            dialog.AcceptButton = btnOK;
            dialog.CancelButton = btnCancel;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var priceToSave = new PriceList { Price_ID = price?.Price_ID ?? IDGenerator.GenerateIDWithTimestamp("PRC"), Product_ID = cboProduct.SelectedValue.ToString(), Tier_ID = cboTier.SelectedValue.ToString(), Unit_Price = numPrice.Value, Effective_Date = dtpFrom.Value, Expiry_Date = dtpTo.Checked ? (DateTime?)dtpTo.Value : null };
                if ((price == null ? _priceService.CreatePrice(priceToSave) : _priceService.UpdatePrice(priceToSave))) { MessageBox.Show("Lưu thành công"); LoadData(); }
            }
        }
    }
}
