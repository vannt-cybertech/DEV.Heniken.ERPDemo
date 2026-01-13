namespace HenikenERP.Presentation.Forms
{
    partial class OrderListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvOrders;
        
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpFilters = new System.Windows.Forms.GroupBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            
            this.pnlTop.SuspendLayout();
            this.grpFilters.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTop.Size = new System.Drawing.Size(1000, 50);
            this.pnlTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Danh sách đơn hàng";

            // grpFilters
            this.grpFilters.Controls.Add(this.btnClearFilter);
            this.grpFilters.Controls.Add(this.btnFilter);
            this.grpFilters.Controls.Add(this.cboStatus);
            this.grpFilters.Controls.Add(this.lblStatus);
            this.grpFilters.Controls.Add(this.dtpEndDate);
            this.grpFilters.Controls.Add(this.lblEndDate);
            this.grpFilters.Controls.Add(this.dtpStartDate);
            this.grpFilters.Controls.Add(this.lblStartDate);
            this.grpFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilters.Location = new System.Drawing.Point(0, 50);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Padding = new System.Windows.Forms.Padding(10);
            this.grpFilters.Size = new System.Drawing.Size(1000, 80);
            this.grpFilters.TabIndex = 1;
            this.grpFilters.TabStop = false;
            this.grpFilters.Text = "Lọc dữ liệu";

            // lblStartDate
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(15, 30);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(56, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Từ ngày:";

            // dtpStartDate
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(15, 45);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(120, 20);
            this.dtpStartDate.TabIndex = 1;

            // lblEndDate
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(150, 30);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(62, 13);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "Đến ngày:";

            // dtpEndDate
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(150, 45);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 20);
            this.dtpEndDate.TabIndex = 3;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(285, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Trạng thái:";

            // cboStatus
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(285, 45);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 21);
            this.cboStatus.TabIndex = 5;

            // btnFilter
            this.btnFilter.Location = new System.Drawing.Point(450, 43);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseVisualStyleBackColor = true;

            // btnClearFilter
            this.btnClearFilter.Location = new System.Drawing.Point(535, 43);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilter.TabIndex = 7;
            this.btnClearFilter.Text = "Xóa lọc";
            this.btnClearFilter.UseVisualStyleBackColor = true;

            // pnlButtons
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnChangeStatus);
            this.pnlButtons.Controls.Add(this.btnViewDetails);
            this.pnlButtons.Controls.Add(this.btnNewOrder);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 550);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Size = new System.Drawing.Size(1000, 50);
            this.pnlButtons.TabIndex = 3;

            // btnNewOrder
            this.btnNewOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNewOrder.Location = new System.Drawing.Point(10, 10);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(120, 30);
            this.btnNewOrder.TabIndex = 0;
            this.btnNewOrder.Text = "Tạo đơn hàng mới";
            this.btnNewOrder.UseVisualStyleBackColor = true;

            // btnViewDetails
            this.btnViewDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnViewDetails.Location = new System.Drawing.Point(140, 10);
            this.btnViewDetails.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 30);
            this.btnViewDetails.TabIndex = 1;
            this.btnViewDetails.Text = "Xem chi tiết";
            this.btnViewDetails.UseVisualStyleBackColor = true;

            // btnChangeStatus
            this.btnChangeStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnChangeStatus.Location = new System.Drawing.Point(250, 10);
            this.btnChangeStatus.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(120, 30);
            this.btnChangeStatus.TabIndex = 2;
            this.btnChangeStatus.Text = "Đổi trạng thái";
            this.btnChangeStatus.UseVisualStyleBackColor = true;

            // btnRefresh
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(380, 10);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;

            // dgvOrders
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 130);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1000, 420);
            this.dgvOrders.TabIndex = 2;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpFilters);
            this.Controls.Add(this.pnlTop);
            this.Name = "OrderListForm";
            this.Text = "Danh sách đơn hàng";
            
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

