using System;
using System.Windows.Forms;

namespace HenikenERP.Presentation
{
    /// <summary>
    /// Main entry point for the application
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Set Vietnamese culture for localization
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            
            // Show login form first
            using (var loginForm = new Forms.LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Login successful, show main form
                    Application.Run(new Forms.MainForm(loginForm.CurrentUser));
                }
            }
        }
    }
}

