using System;
using System.Windows.Forms;
using HenikenERP.Business.Services;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    public partial class SettingsForm : Form
    {
        private SettingsService _settingsService;

        public SettingsForm()
        {
            InitializeComponent();
            this.Load += SettingsForm_Load;
            btnSave.Click += (s, e) => SaveSettings();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);

            _settingsService = new SettingsService();
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtCompanyName.Text = _settingsService.GetSetting("CompanyName", "Heineken Vietnam");
            txtCompanyAddress.Text = _settingsService.GetSetting("CompanyAddress", "");
            txtCompanyPhone.Text = _settingsService.GetSetting("CompanyPhone", "");
            txtCompanyTax.Text = _settingsService.GetSetting("CompanyTaxCode", "");
            numLowStockThreshold.Value = int.Parse(_settingsService.GetSetting("LowStockThreshold", "10"));
            numTaxRate.Value = decimal.Parse(_settingsService.GetSetting("TaxRate", "10"));
        }

        private void SaveSettings()
        {
            _settingsService.SaveSetting("CompanyName", txtCompanyName.Text);
            _settingsService.SaveSetting("CompanyAddress", txtCompanyAddress.Text);
            _settingsService.SaveSetting("CompanyPhone", txtCompanyPhone.Text);
            _settingsService.SaveSetting("CompanyTaxCode", txtCompanyTax.Text);
            _settingsService.SaveSetting("LowStockThreshold", numLowStockThreshold.Value.ToString());
            _settingsService.SaveSetting("TaxRate", numTaxRate.Value.ToString());
            MessageBox.Show("Lưu cấu hình thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
