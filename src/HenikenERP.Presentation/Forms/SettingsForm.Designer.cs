namespace HenikenERP.Presentation.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.TextBox txtCompanyName, txtCompanyAddress, txtCompanyPhone, txtCompanyTax;
        private System.Windows.Forms.NumericUpDown numLowStockThreshold, numTaxRate;
        private System.Windows.Forms.Button btnSave;
        
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        
        private void InitializeComponent()
        {
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtCompanyAddress = new System.Windows.Forms.TextBox();
            this.txtCompanyPhone = new System.Windows.Forms.TextBox();
            this.txtCompanyTax = new System.Windows.Forms.TextBox();
            this.numLowStockThreshold = new System.Windows.Forms.NumericUpDown();
            this.numTaxRate = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLowStockThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxRate)).BeginInit();
            this.SuspendLayout();
            
            var lbls = new[] {
                new System.Windows.Forms.Label { Text = "Tên công ty:", Location = new System.Drawing.Point(15, 23), Width = 120 },
                new System.Windows.Forms.Label { Text = "Địa chỉ:", Location = new System.Drawing.Point(15, 58), Width = 120 },
                new System.Windows.Forms.Label { Text = "Điện thoại:", Location = new System.Drawing.Point(15, 93), Width = 120 },
                new System.Windows.Forms.Label { Text = "Mã số thuế:", Location = new System.Drawing.Point(15, 128), Width = 120 },
                new System.Windows.Forms.Label { Text = "Ngưỡng tồn thấp:", Location = new System.Drawing.Point(15, 163), Width = 120 },
                new System.Windows.Forms.Label { Text = "Thuế VAT (%):", Location = new System.Drawing.Point(15, 198), Width = 120 }
            };
            
            this.txtCompanyName.Location = new System.Drawing.Point(145, 20);
            this.txtCompanyName.Width = 300;
            this.txtCompanyAddress.Location = new System.Drawing.Point(145, 55);
            this.txtCompanyAddress.Width = 300;
            this.txtCompanyPhone.Location = new System.Drawing.Point(145, 90);
            this.txtCompanyPhone.Width = 300;
            this.txtCompanyTax.Location = new System.Drawing.Point(145, 125);
            this.txtCompanyTax.Width = 300;
            this.numLowStockThreshold.Location = new System.Drawing.Point(145, 160);
            this.numLowStockThreshold.Width = 300;
            this.numLowStockThreshold.Maximum = 1000;
            this.numTaxRate.Location = new System.Drawing.Point(145, 195);
            this.numTaxRate.Width = 300;
            this.numTaxRate.Maximum = 100;
            this.numTaxRate.DecimalPlaces = 2;
            
            this.btnSave.Text = "Lưu cấu hình";
            this.btnSave.Location = new System.Drawing.Point(345, 240);
            this.btnSave.Width = 100;
            
            this.Controls.AddRange(lbls);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtCompanyName, this.txtCompanyAddress, this.txtCompanyPhone, this.txtCompanyTax, this.numLowStockThreshold, this.numTaxRate, this.btnSave });
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cấu hình hệ thống";
            ((System.ComponentModel.ISupportInitialize)(this.numLowStockThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
