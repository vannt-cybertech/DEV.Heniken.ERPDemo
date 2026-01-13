namespace HenikenERP.Presentation.Forms
{
    partial class PriceListForm
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.DataGridView dgvPrices;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete, btnRefresh;
        
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        
        private void InitializeComponent()
        {
            this.dgvPrices = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            var pnl = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrices)).BeginInit();
            pnl.SuspendLayout();
            this.SuspendLayout();
            this.dgvPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrices.ReadOnly = true;
            this.dgvPrices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnl.Height = 50;
            pnl.Padding = new System.Windows.Forms.Padding(10);
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Width = 80;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEdit.Width = 80;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Width = 80;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Width = 80;
            pnl.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnRefresh, this.btnDelete, this.btnEdit, this.btnAdd });
            this.Controls.Add(this.dgvPrices);
            this.Controls.Add(pnl);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Text = "Bảng giá";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrices)).EndInit();
            pnl.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
