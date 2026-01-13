namespace HenikenERP.Presentation.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox grpKPIs;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.Label lblProductsValue;
        private System.Windows.Forms.Panel pnlCustomers;
        private System.Windows.Forms.Label lblCustomersTitle;
        private System.Windows.Forms.Label lblCustomersValue;
        private System.Windows.Forms.Panel pnlWarehouses;
        private System.Windows.Forms.Label lblWarehousesTitle;
        private System.Windows.Forms.Label lblWarehousesValue;
        private System.Windows.Forms.Panel pnlInventory;
        private System.Windows.Forms.Label lblInventoryTitle;
        private System.Windows.Forms.Label lblInventoryValue;
        private System.Windows.Forms.GroupBox grpRecentOrders;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (_unitOfWork != null)
                    _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.grpKPIs = new System.Windows.Forms.GroupBox();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.lblProductsValue = new System.Windows.Forms.Label();
            this.pnlCustomers = new System.Windows.Forms.Panel();
            this.lblCustomersTitle = new System.Windows.Forms.Label();
            this.lblCustomersValue = new System.Windows.Forms.Label();
            this.pnlWarehouses = new System.Windows.Forms.Panel();
            this.lblWarehousesTitle = new System.Windows.Forms.Label();
            this.lblWarehousesValue = new System.Windows.Forms.Label();
            this.pnlInventory = new System.Windows.Forms.Panel();
            this.lblInventoryTitle = new System.Windows.Forms.Label();
            this.lblInventoryValue = new System.Windows.Forms.Label();
            this.grpRecentOrders = new System.Windows.Forms.GroupBox();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            
            this.grpKPIs.SuspendLayout();
            this.pnlProducts.SuspendLayout();
            this.pnlCustomers.SuspendLayout();
            this.pnlWarehouses.SuspendLayout();
            this.pnlInventory.SuspendLayout();
            this.grpRecentOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.layout.SuspendLayout();
            this.SuspendLayout();

            // layout
            this.layout.ColumnCount = 1;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowCount = 4;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Padding = new System.Windows.Forms.Padding(16);
            this.layout.Size = new System.Drawing.Size(1000, 700);
            this.layout.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(19, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(962, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bảng điều khiển";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWelcome.Location = new System.Drawing.Point(19, 66);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(962, 35);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Xin chào!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // grpKPIs
            this.grpKPIs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKPIs.Location = new System.Drawing.Point(19, 104);
            this.grpKPIs.Name = "grpKPIs";
            this.grpKPIs.Padding = new System.Windows.Forms.Padding(8);
            this.grpKPIs.Size = new System.Drawing.Size(962, 114);
            this.grpKPIs.TabIndex = 2;
            this.grpKPIs.TabStop = false;
            this.grpKPIs.Text = "Tổng quan";

            // pnlProducts
            this.pnlProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProducts.Location = new System.Drawing.Point(15, 25);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Size = new System.Drawing.Size(220, 75);
            this.pnlProducts.TabIndex = 0;
            this.lblProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductsTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProductsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblProductsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblProductsTitle.Name = "lblProductsTitle";
            this.lblProductsTitle.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.lblProductsTitle.Size = new System.Drawing.Size(218, 28);
            this.lblProductsTitle.TabIndex = 0;
            this.lblProductsTitle.Text = "Sản phẩm";
            this.lblProductsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductsValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblProductsValue.Location = new System.Drawing.Point(0, 28);
            this.lblProductsValue.Name = "lblProductsValue";
            this.lblProductsValue.Size = new System.Drawing.Size(218, 45);
            this.lblProductsValue.TabIndex = 1;
            this.lblProductsValue.Text = "0";
            this.lblProductsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlProducts.Controls.Add(this.lblProductsValue);
            this.pnlProducts.Controls.Add(this.lblProductsTitle);

            // pnlCustomers
            this.pnlCustomers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomers.Location = new System.Drawing.Point(250, 25);
            this.pnlCustomers.Name = "pnlCustomers";
            this.pnlCustomers.Size = new System.Drawing.Size(220, 75);
            this.pnlCustomers.TabIndex = 1;
            this.lblCustomersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomersTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomersTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomersTitle.Location = new System.Drawing.Point(0, 0);
            this.lblCustomersTitle.Name = "lblCustomersTitle";
            this.lblCustomersTitle.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.lblCustomersTitle.Size = new System.Drawing.Size(218, 28);
            this.lblCustomersTitle.TabIndex = 0;
            this.lblCustomersTitle.Text = "Khách hàng";
            this.lblCustomersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCustomersValue.Location = new System.Drawing.Point(0, 28);
            this.lblCustomersValue.Name = "lblCustomersValue";
            this.lblCustomersValue.Size = new System.Drawing.Size(218, 45);
            this.lblCustomersValue.TabIndex = 1;
            this.lblCustomersValue.Text = "0";
            this.lblCustomersValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlCustomers.Controls.Add(this.lblCustomersValue);
            this.pnlCustomers.Controls.Add(this.lblCustomersTitle);

            // pnlWarehouses
            this.pnlWarehouses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWarehouses.Location = new System.Drawing.Point(485, 25);
            this.pnlWarehouses.Name = "pnlWarehouses";
            this.pnlWarehouses.Size = new System.Drawing.Size(220, 75);
            this.pnlWarehouses.TabIndex = 2;
            this.lblWarehousesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWarehousesTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehousesTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehousesTitle.Location = new System.Drawing.Point(0, 0);
            this.lblWarehousesTitle.Name = "lblWarehousesTitle";
            this.lblWarehousesTitle.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.lblWarehousesTitle.Size = new System.Drawing.Size(218, 28);
            this.lblWarehousesTitle.TabIndex = 0;
            this.lblWarehousesTitle.Text = "Kho hàng";
            this.lblWarehousesValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarehousesValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWarehousesValue.Location = new System.Drawing.Point(0, 28);
            this.lblWarehousesValue.Name = "lblWarehousesValue";
            this.lblWarehousesValue.Size = new System.Drawing.Size(218, 45);
            this.lblWarehousesValue.TabIndex = 1;
            this.lblWarehousesValue.Text = "0";
            this.lblWarehousesValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlWarehouses.Controls.Add(this.lblWarehousesValue);
            this.pnlWarehouses.Controls.Add(this.lblWarehousesTitle);

            // pnlInventory
            this.pnlInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInventory.Location = new System.Drawing.Point(720, 25);
            this.pnlInventory.Name = "pnlInventory";
            this.pnlInventory.Size = new System.Drawing.Size(220, 75);
            this.pnlInventory.TabIndex = 3;
            this.lblInventoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInventoryTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInventoryTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblInventoryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblInventoryTitle.Name = "lblInventoryTitle";
            this.lblInventoryTitle.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.lblInventoryTitle.Size = new System.Drawing.Size(218, 28);
            this.lblInventoryTitle.TabIndex = 0;
            this.lblInventoryTitle.Text = "Tồn kho khả dụng";
            this.lblInventoryValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInventoryValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblInventoryValue.Location = new System.Drawing.Point(0, 28);
            this.lblInventoryValue.Name = "lblInventoryValue";
            this.lblInventoryValue.Size = new System.Drawing.Size(218, 45);
            this.lblInventoryValue.TabIndex = 1;
            this.lblInventoryValue.Text = "0";
            this.lblInventoryValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlInventory.Controls.Add(this.lblInventoryValue);
            this.pnlInventory.Controls.Add(this.lblInventoryTitle);

            this.grpKPIs.Controls.Add(this.pnlProducts);
            this.grpKPIs.Controls.Add(this.pnlCustomers);
            this.grpKPIs.Controls.Add(this.pnlWarehouses);
            this.grpKPIs.Controls.Add(this.pnlInventory);

            // grpRecentOrders
            this.grpRecentOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRecentOrders.Location = new System.Drawing.Point(19, 224);
            this.grpRecentOrders.Name = "grpRecentOrders";
            this.grpRecentOrders.Padding = new System.Windows.Forms.Padding(8);
            this.grpRecentOrders.Size = new System.Drawing.Size(962, 460);
            this.grpRecentOrders.TabIndex = 3;
            this.grpRecentOrders.TabStop = false;
            this.grpRecentOrders.Text = "Đơn hàng gần đây";

            // dgvRecentOrders
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AllowUserToDeleteRows = false;
            this.dgvRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentOrders.Location = new System.Drawing.Point(8, 21);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            this.dgvRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentOrders.Size = new System.Drawing.Size(946, 431);
            this.dgvRecentOrders.TabIndex = 0;

            this.grpRecentOrders.Controls.Add(this.dgvRecentOrders);
            this.layout.Controls.Add(this.lblTitle, 0, 0);
            this.layout.Controls.Add(this.lblWelcome, 0, 1);
            this.layout.Controls.Add(this.grpKPIs, 0, 2);
            this.layout.Controls.Add(this.grpRecentOrders, 0, 3);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.layout);
            this.Name = "DashboardForm";
            this.Text = "Bảng điều khiển";
            
            this.grpKPIs.ResumeLayout(false);
            this.pnlProducts.ResumeLayout(false);
            this.pnlCustomers.ResumeLayout(false);
            this.pnlWarehouses.ResumeLayout(false);
            this.pnlInventory.ResumeLayout(false);
            this.grpRecentOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

