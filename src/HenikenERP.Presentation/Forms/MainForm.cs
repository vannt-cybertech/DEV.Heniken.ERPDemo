using System;
using System.Drawing;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Presentation.Forms;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    /// <summary>
    /// Main MDI form container
    /// </summary>
    public partial class MainForm : Form
    {
        public User CurrentUser { get; set; }
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        public MainForm(User user) : this()
        {
            CurrentUser = user;
            this.Text = $"Heniken ERP - Người dùng: {user.Username}";
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Apply theme to main form
            ThemeHelper.ApplyTheme(this);

            // Add brand/logo to menu strip (once)
            TryAddBrandToMenu();

            // Initialize menu items
            InitializeMenu();

            // Open default screen
            OpenForm(new DashboardForm(CurrentUser));
        }

        private void TryAddBrandToMenu()
        {
            try
            {
                if (menuStrip1 == null) return;

                // Remove old brand item (if any) to avoid duplication in MDI menubar.
                var found = menuStrip1.Items.Find("lblBrand", false);
                if (found != null && found.Length > 0)
                {
                    menuStrip1.Items.Remove(found[0]);
                }

                // In MDI, the active child icon already appears on the menubar (left side).
                // Keep brand as TEXT only (no image) to reduce icon clutter.
                var brand = new ToolStripLabel
                {
                    Name = "lblBrand",
                    Text = "Heniken ERP",
                    ForeColor = ThemeColors.TextOnPrimary,
                    Margin = new Padding(8, 0, 12, 0)
                };

                menuStrip1.Items.Insert(0, brand);
            }
            catch
            {
                // ignore branding failures
            }
        }
        
        private void InitializeMenu()
        {
            // Menu structure will be added here
        }
        
        private void mnuDashboard_Click(object sender, EventArgs e)
        {
            OpenForm(new DashboardForm(CurrentUser));
        }
        
        private void mnuProducts_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductListForm());
        }
        
        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerListForm());
        }
        
        private void mnuTiers_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerTierListForm());
        }
        
        private void mnuPriceList_Click(object sender, EventArgs e)
        {
            OpenForm(new PriceListForm());
        }
        
        private void mnuWarehouses_Click(object sender, EventArgs e)
        {
            OpenForm(new WarehouseListForm());
        }
        
        private void mnuInventory_Click(object sender, EventArgs e)
        {
            OpenForm(new InventoryForm());
        }
        
        private void mnuOrders_Click(object sender, EventArgs e)
        {
            OpenForm(new OrderListForm(CurrentUser));
        }
        
        private void mnuReports_Click(object sender, EventArgs e)
        {
            OpenForm(new ReportForm());
        }
        
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
        
        private void mnuDatabaseSettings_Click(object sender, EventArgs e)
        {
            var dbSettingsForm = new DatabaseSettingsForm();
            dbSettingsForm.ShowDialog();
        }
        
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void OpenForm(Form form)
        {
            // Close existing MDI children so the user only sees one screen at a time
            foreach (Form child in this.MdiChildren)
            {
                try { child.Close(); } catch { /* ignore */ }
            }

            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }
}

