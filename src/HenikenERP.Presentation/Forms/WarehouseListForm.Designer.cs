namespace HenikenERP.Presentation.Forms
{
    partial class WarehouseListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete, btnRefresh;
        private System.Windows.Forms.DataGridView dgvWarehouses;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvWarehouses = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouses)).BeginInit();
            this.SuspendLayout();

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Size = new System.Drawing.Size(800, 50);
            this.pnlTop.Padding = new System.Windows.Forms.Padding(10);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Text = "Danh sách kho";

            this.pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnRefresh, this.btnDelete, this.btnEdit, this.btnAdd });
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Size = new System.Drawing.Size(800, 50);
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);

            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);

            this.dgvWarehouses.AllowUserToAddRows = false;
            this.dgvWarehouses.AllowUserToDeleteRows = false;
            this.dgvWarehouses.ReadOnly = true;
            this.dgvWarehouses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWarehouses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.dgvWarehouses, this.pnlButtons, this.pnlTop });
            this.Name = "WarehouseListForm";
            this.Text = "Danh sách kho";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouses)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
