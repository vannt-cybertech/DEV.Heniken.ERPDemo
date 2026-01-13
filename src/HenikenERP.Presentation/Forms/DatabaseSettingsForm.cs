using System;
using System.Windows.Forms;
using System.Configuration;
using HenikenERP.Business.Services;
using HenikenERP.Core.DTOs;
using HenikenERP.Common.Helpers;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    /// <summary>
    /// Database settings form
    /// </summary>
    public partial class DatabaseSettingsForm : Form
    {
        private SettingsService _settingsService;
        
        public DatabaseSettingsForm()
        {
            InitializeComponent();
            _settingsService = new SettingsService();
            this.Load += DatabaseSettingsForm_Load;
            LoadCurrentSettings();
        }

        private void DatabaseSettingsForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);
        }
        
        /// <summary>
        /// Load current database connection settings if available
        /// </summary>
        private void LoadCurrentSettings()
        {
            try
            {
                string encryptedConnectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
                
                if (!string.IsNullOrEmpty(encryptedConnectionString))
                {
                    string connectionString = EncryptionHelper.DecryptConnectionString(encryptedConnectionString);
                    
                    // Parse connection string
                    var connectionDTO = ParseConnectionString(connectionString);
                    if (connectionDTO != null)
                    {
                        txtServer.Text = connectionDTO.Server;
                        txtPort.Text = connectionDTO.Port.ToString();
                        txtDatabase.Text = connectionDTO.Database;
                        txtUsername.Text = connectionDTO.Username;
                        txtPassword.Text = connectionDTO.Password;
                    }
                }
            }
            catch
            {
                // If loading fails, use default values (already set in Designer)
            }
        }
        
        /// <summary>
        /// Parse MySQL connection string to DTO
        /// </summary>
        private DatabaseConnectionDTO ParseConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return null;
                
            try
            {
                var dto = new DatabaseConnectionDTO();
                var parts = connectionString.Split(';');
                
                foreach (var part in parts)
                {
                    if (string.IsNullOrWhiteSpace(part))
                        continue;
                        
                    var keyValue = part.Split('=');
                    if (keyValue.Length != 2)
                        continue;
                        
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();
                    
                    switch (key.ToLower())
                    {
                        case "server":
                            dto.Server = value;
                            break;
                        case "port":
                            if (int.TryParse(value, out int port))
                                dto.Port = port;
                            break;
                        case "database":
                            dto.Database = value;
                            break;
                        case "uid":
                        case "user id":
                        case "username":
                            dto.Username = value;
                            break;
                        case "pwd":
                        case "password":
                            dto.Password = value;
                            break;
                    }
                }
                
                return dto;
            }
            catch
            {
                return null;
            }
        }
        
        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionDTO = GetConnectionDTO();
                if (_settingsService.TestConnection(connectionDTO))
                {
                    MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionDTO = GetConnectionDTO();
                if (_settingsService.SaveDatabaseConnection(connectionDTO))
                {
                    MessageBox.Show("Lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lưu cấu hình thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private DatabaseConnectionDTO GetConnectionDTO()
        {
            return new DatabaseConnectionDTO
            {
                Server = txtServer.Text.Trim(),
                Port = int.Parse(txtPort.Text.Trim()),
                Database = txtDatabase.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };
        }
    }
}

