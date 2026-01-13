namespace HenikenERP.Presentation.Forms
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Button btnRefresh;
        
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        
        private void InitializeComponent()
        {
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            var pnl = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            pnl.SuspendLayout();
            this.SuspendLayout();
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnl.Height = 50;
            pnl.Padding = new System.Windows.Forms.Padding(10);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Width = 100;
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
