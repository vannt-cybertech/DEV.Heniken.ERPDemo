namespace HenikenERP.Presentation.Forms
{
    partial class OrderCreateForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.GroupBox grpOrderDetails;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Panel pnlDetailButtons;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.grpOrderDetails = new System.Windows.Forms.GroupBox();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.pnlDetailButtons = new System.Windows.Forms.Panel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            
            this.grpOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlDetailButtons.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tạo đơn hàng mới";

            // lblCustomer
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(15, 60);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(74, 13);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Khách hàng:";

            // cboCustomer
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(105, 57);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(300, 21);
            this.cboCustomer.TabIndex = 2;

            // lblWarehouse
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(430, 60);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(30, 13);
            this.lblWarehouse.TabIndex = 3;
            this.lblWarehouse.Text = "Kho:";

            // cboWarehouse
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(470, 57);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(250, 21);
            this.cboWarehouse.TabIndex = 4;

            // lblOrderDate
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(15, 95);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(59, 13);
            this.lblOrderDate.TabIndex = 5;
            this.lblOrderDate.Text = "Ngày đặt:";

            // dtpOrderDate
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOrderDate.Location = new System.Drawing.Point(105, 92);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(120, 20);
            this.dtpOrderDate.TabIndex = 6;

            // grpOrderDetails
            this.grpOrderDetails.Controls.Add(this.pnlDetailButtons);
            this.grpOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.grpOrderDetails.Location = new System.Drawing.Point(15, 130);
            this.grpOrderDetails.Name = "grpOrderDetails";
            this.grpOrderDetails.Padding = new System.Windows.Forms.Padding(8);
            this.grpOrderDetails.Size = new System.Drawing.Size(750, 320);
            this.grpOrderDetails.TabIndex = 7;
            this.grpOrderDetails.TabStop = false;
            this.grpOrderDetails.Text = "Chi tiết đơn hàng";

            // dgvOrderDetails
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(8, 21);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(734, 251);
            this.dgvOrderDetails.TabIndex = 0;

            // pnlDetailButtons
            this.pnlDetailButtons.Controls.Add(this.btnRemoveProduct);
            this.pnlDetailButtons.Controls.Add(this.btnAddProduct);
            this.pnlDetailButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetailButtons.Location = new System.Drawing.Point(8, 272);
            this.pnlDetailButtons.Name = "pnlDetailButtons";
            this.pnlDetailButtons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlDetailButtons.Size = new System.Drawing.Size(734, 40);
            this.pnlDetailButtons.TabIndex = 1;

            // btnAddProduct
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddProduct.Location = new System.Drawing.Point(5, 5);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(120, 30);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "Thêm sản phẩm";
            this.btnAddProduct.UseVisualStyleBackColor = true;

            // btnRemoveProduct
            this.btnRemoveProduct.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRemoveProduct.Location = new System.Drawing.Point(135, 5);
            this.btnRemoveProduct.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRemoveProduct.Name = "btnRemoveProduct";
            this.btnRemoveProduct.Size = new System.Drawing.Size(120, 30);
            this.btnRemoveProduct.TabIndex = 1;
            this.btnRemoveProduct.Text = "Xóa sản phẩm";
            this.btnRemoveProduct.UseVisualStyleBackColor = true;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(500, 465);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(81, 19);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Tổng tiền:";

            // txtTotal
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTotal.Location = new System.Drawing.Point(590, 462);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(175, 25);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // btnSave
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(570, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(670, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 560);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grpOrderDetails);
            this.Controls.Add(this.dtpOrderDate);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.cboWarehouse);
            this.Controls.Add(this.lblWarehouse);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo đơn hàng mới";
            
            this.grpOrderDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlDetailButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

