namespace HenikenERP.Presentation.Forms
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnRefresh;
        
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        
        private void InitializeComponent()
        {
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            var pnl = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            pnl.SuspendLayout();
            this.SuspendLayout();
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnl.Height = 50;
            pnl.Padding = new System.Windows.Forms.Padding(10);
            pnl.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            pnl.WrapContents = false;
            pnl.AutoScroll = true;

            this.btnStockIn.Text = "Nhập kho";
            this.btnStockIn.Width = 120;
            this.btnStockIn.Height = 32;
            this.btnStockIn.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.btnAdjust.Text = "Điều chỉnh";
            this.btnAdjust.Width = 120;
            this.btnAdjust.Height = 32;
            this.btnAdjust.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Width = 100;
            this.btnRefresh.Height = 32;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);

            pnl.Controls.Add(this.btnStockIn);
            pnl.Controls.Add(this.btnAdjust);
            pnl.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(pnl);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Text = "Quản lý tồn kho";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            pnl.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
