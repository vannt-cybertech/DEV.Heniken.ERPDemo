namespace HenikenERP.Presentation.Forms
{
    partial class DatabaseSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        
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
            this.lblServer = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // Labels and textboxes setup
            this.lblServer.Location = new System.Drawing.Point(20, 20);
            this.lblServer.Size = new System.Drawing.Size(100, 20);
            this.lblServer.Text = "Máy chủ:";
            
            this.txtServer.Location = new System.Drawing.Point(130, 18);
            this.txtServer.Size = new System.Drawing.Size(250, 20);
            this.txtServer.Text = "localhost";
            
            this.lblPort.Location = new System.Drawing.Point(20, 50);
            this.lblPort.Size = new System.Drawing.Size(100, 20);
            this.lblPort.Text = "Cổng:";
            
            this.txtPort.Location = new System.Drawing.Point(130, 48);
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.Text = "3306";
            
            this.lblDatabase.Location = new System.Drawing.Point(20, 80);
            this.lblDatabase.Size = new System.Drawing.Size(100, 20);
            this.lblDatabase.Text = "Cơ sở dữ liệu:";
            
            this.txtDatabase.Location = new System.Drawing.Point(130, 78);
            this.txtDatabase.Size = new System.Drawing.Size(250, 20);
            this.txtDatabase.Text = "heniken_erp";
            
            this.lblUsername.Location = new System.Drawing.Point(20, 110);
            this.lblUsername.Size = new System.Drawing.Size(100, 20);
            this.lblUsername.Text = "Tên đăng nhập:";
            
            this.txtUsername.Location = new System.Drawing.Point(130, 108);
            this.txtUsername.Size = new System.Drawing.Size(250, 20);
            this.txtUsername.Text = "heniken_user";
            
            this.lblPassword.Location = new System.Drawing.Point(20, 140);
            this.lblPassword.Size = new System.Drawing.Size(100, 20);
            this.lblPassword.Text = "Mật khẩu:";
            
            this.txtPassword.Location = new System.Drawing.Point(130, 138);
            this.txtPassword.Size = new System.Drawing.Size(250, 20);
            this.txtPassword.PasswordChar = '*';
            
            this.btnTest.Location = new System.Drawing.Point(130, 180);
            this.btnTest.Size = new System.Drawing.Size(80, 30);
            this.btnTest.Text = "Kiểm tra";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            
            this.btnSave.Location = new System.Drawing.Point(220, 180);
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            this.btnCancel.Location = new System.Drawing.Point(310, 180);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            this.ClientSize = new System.Drawing.Size(420, 230);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblServer, this.txtServer,
                this.lblPort, this.txtPort,
                this.lblDatabase, this.txtDatabase,
                this.lblUsername, this.txtUsername,
                this.lblPassword, this.txtPassword,
                this.btnTest, this.btnSave, this.btnCancel
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Cấu hình cơ sở dữ liệu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

