using System;
using System.Windows.Forms;
using HenikenERP.Business.Services;
using HenikenERP.Core.DTOs;
using HenikenERP.Core.Entities;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    /// <summary>
    /// Login form for user authentication
    /// </summary>
    public partial class LoginForm : Form
    {
        private AuthenticationService _authService;
        public User CurrentUser { get; private set; }
        
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            this.Load += LoginForm_Load;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var loginDTO = new LoginDTO
                {
                    Username = txtUsername.Text.Trim(),
                    // Trim to avoid accidental whitespace from copy/paste
                    Password = (txtPassword.Text ?? string.Empty).Trim()
                };
                
                CurrentUser = _authService.Authenticate(loginDTO);
                
                if (CurrentUser != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var msg = !string.IsNullOrWhiteSpace(_authService.LastErrorMessage)
                        ? _authService.LastErrorMessage
                        : "Tên đăng nhập hoặc mật khẩu không đúng.";

                    MessageBox.Show(msg, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                
                // Check if it's a database connection error
                if (ex.Message.Contains("Database connection string") || 
                    ex.Message.Contains("kết nối database") ||
                    (ex.InnerException != null && ex.InnerException.Message.Contains("connection")))
                {
                    var result = MessageBox.Show(
                        "Chưa cấu hình kết nối database.\n\nBạn có muốn cấu hình ngay không?",
                        "Cấu hình Database",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        btnDatabaseSettings_Click(sender, e);
                    }
                }
                else
                {
                    if (ex.InnerException != null)
                    {
                        errorMessage += $"\n\nChi tiết: {ex.InnerException.Message}";
                    }
                    MessageBox.Show(errorMessage, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void btnDatabaseSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dbSettingsForm = new DatabaseSettingsForm())
                {
                    if (dbSettingsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload authentication service after database settings changed
                        _authService?.Dispose();
                        _authService = new AuthenticationService();
                        MessageBox.Show("Cấu hình database đã được cập nhật. Vui lòng thử đăng nhập lại.", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở cấu hình database: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _authService?.Dispose();
        }
    }
}

