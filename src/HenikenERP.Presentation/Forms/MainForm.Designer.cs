namespace HenikenERP.Presentation.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDashboard;
        private System.Windows.Forms.ToolStripMenuItem mnuProducts;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.Windows.Forms.ToolStripMenuItem mnuPricing;
        private System.Windows.Forms.ToolStripMenuItem mnuTiers;
        private System.Windows.Forms.ToolStripMenuItem mnuPriceList;
        private System.Windows.Forms.ToolStripMenuItem mnuWarehouses;
        private System.Windows.Forms.ToolStripMenuItem mnuInventory;
        private System.Windows.Forms.ToolStripMenuItem mnuOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabaseSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPricing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTiers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPriceList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWarehouses = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabaseSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            
            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuDashboard,
                this.mnuProducts,
                this.mnuCustomers,
                this.mnuPricing,
                this.mnuWarehouses,
                this.mnuInventory,
                this.mnuOrders,
                this.mnuReports,
                this.mnuSettings
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            
            // mnuDashboard
            this.mnuDashboard.Name = "mnuDashboard";
            this.mnuDashboard.Size = new System.Drawing.Size(76, 20);
            this.mnuDashboard.Text = "Bảng điều khiển";
            this.mnuDashboard.Click += new System.EventHandler(this.mnuDashboard_Click);
            
            // mnuProducts
            this.mnuProducts.Name = "mnuProducts";
            this.mnuProducts.Size = new System.Drawing.Size(66, 20);
            this.mnuProducts.Text = "Sản phẩm";
            this.mnuProducts.Click += new System.EventHandler(this.mnuProducts_Click);
            
            // mnuCustomers
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(78, 20);
            this.mnuCustomers.Text = "Khách hàng";
            this.mnuCustomers.Click += new System.EventHandler(this.mnuCustomers_Click);
            
            // mnuPricing
            this.mnuPricing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuTiers,
                this.mnuPriceList
            });
            this.mnuPricing.Name = "mnuPricing";
            this.mnuPricing.Size = new System.Drawing.Size(60, 20);
            this.mnuPricing.Text = "Giá & Hạng";
            
            // mnuTiers
            this.mnuTiers.Name = "mnuTiers";
            this.mnuTiers.Size = new System.Drawing.Size(180, 22);
            this.mnuTiers.Text = "Hạng khách hàng";
            this.mnuTiers.Click += new System.EventHandler(this.mnuTiers_Click);
            
            // mnuPriceList
            this.mnuPriceList.Name = "mnuPriceList";
            this.mnuPriceList.Size = new System.Drawing.Size(180, 22);
            this.mnuPriceList.Text = "Bảng giá";
            this.mnuPriceList.Click += new System.EventHandler(this.mnuPriceList_Click);
            
            // mnuWarehouses
            this.mnuWarehouses.Name = "mnuWarehouses";
            this.mnuWarehouses.Size = new System.Drawing.Size(70, 20);
            this.mnuWarehouses.Text = "Kho hàng";
            this.mnuWarehouses.Click += new System.EventHandler(this.mnuWarehouses_Click);
            
            // mnuInventory
            this.mnuInventory.Name = "mnuInventory";
            this.mnuInventory.Size = new System.Drawing.Size(70, 20);
            this.mnuInventory.Text = "Tồn kho";
            this.mnuInventory.Click += new System.EventHandler(this.mnuInventory_Click);
            
            // mnuOrders
            this.mnuOrders.Name = "mnuOrders";
            this.mnuOrders.Size = new System.Drawing.Size(62, 20);
            this.mnuOrders.Text = "Đơn hàng";
            this.mnuOrders.Click += new System.EventHandler(this.mnuOrders_Click);
            
            // mnuReports
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(60, 20);
            this.mnuReports.Text = "Báo cáo";
            this.mnuReports.Click += new System.EventHandler(this.mnuReports_Click);
            
            // mnuSettings
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuDatabaseSettings,
                this.mnuExit
            });
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(67, 20);
            this.mnuSettings.Text = "Cài đặt";
            
            // mnuDatabaseSettings
            this.mnuDatabaseSettings.Name = "mnuDatabaseSettings";
            this.mnuDatabaseSettings.Size = new System.Drawing.Size(180, 22);
            this.mnuDatabaseSettings.Text = "Cấu hình CSDL";
            this.mnuDatabaseSettings.Click += new System.EventHandler(this.mnuDatabaseSettings_Click);
            
            // mnuExit
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(180, 22);
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Heniken ERP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

