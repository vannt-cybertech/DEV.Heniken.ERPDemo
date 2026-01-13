namespace HenikenERP.Presentation.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ComboBox cboReportType;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblSummary;
        
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        
        private void InitializeComponent()
        {
            this.cboReportType = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblSummary = new System.Windows.Forms.Label();
            var pnlTop = new System.Windows.Forms.Panel();
            var pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            this.SuspendLayout();
            
            pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlTop.Height = 60;
            pnlTop.Padding = new System.Windows.Forms.Padding(10);
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.Items.AddRange(new[] { "Báo cáo doanh thu", "Báo cáo tồn kho", "Báo cáo khách hàng" });
            this.cboReportType.Location = new System.Drawing.Point(15, 15);
            this.cboReportType.Width = 150;
            this.dtpFrom.Location = new System.Drawing.Point(180, 15);
            this.dtpFrom.Width = 120;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(320, 15);
            this.dtpTo.Width = 120;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.btnGenerate.Location = new System.Drawing.Point(460, 13);
            this.btnGenerate.Text = "Tạo báo cáo";
            this.btnGenerate.Width = 100;
            pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] { new System.Windows.Forms.Label { Text = "Loại báo cáo:", Location = new System.Drawing.Point(15, 5), AutoSize = true },
                this.cboReportType, new System.Windows.Forms.Label { Text = "Từ:", Location = new System.Drawing.Point(180, 5), AutoSize = true }, this.dtpFrom,
                new System.Windows.Forms.Label { Text = "Đến:", Location = new System.Drawing.Point(320, 5), AutoSize = true }, this.dtpTo, this.btnGenerate });
            
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.ReadOnly = true;
            
            pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBottom.Height = 40;
            pnlBottom.Padding = new System.Windows.Forms.Padding(10);
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummary.Text = "Chọn loại báo cáo và nhấn Tạo báo cáo";
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            pnlBottom.Controls.Add(this.lblSummary);
            
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Text = "Báo cáo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
