using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using HenikenERP.Core.DTOs;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;
using HenikenERP.Common.Constants;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Settings service for application configuration
    /// </summary>
    public class SettingsService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public SettingsService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        /// <summary>
        /// Save database connection string
        /// </summary>
        public bool SaveDatabaseConnection(DatabaseConnectionDTO connectionDTO)
        {
            try
            {
                string connectionString = connectionDTO.GetConnectionString();
                string encryptedConnectionString = EncryptionHelper.EncryptConnectionString(connectionString);
                
                // Save to .exe.config (runtime config file)
                var exeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (exeConfig.AppSettings.Settings["DatabaseConnectionString"] != null)
                {
                    exeConfig.AppSettings.Settings["DatabaseConnectionString"].Value = encryptedConnectionString;
                }
                else
                {
                    exeConfig.AppSettings.Settings.Add("DatabaseConnectionString", encryptedConnectionString);
                }
                exeConfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                
                // Also try to save to source App.config (so it persists after rebuild)
                // This is optional - if it fails, the .exe.config is still saved and will work
                try
                {
                    // Try to find App.config relative to current directory or solution root
                    string currentDir = Directory.GetCurrentDirectory();
                    string appConfigPath = null;
                    
                    // Try different possible paths
                    string[] possiblePaths = new string[]
                    {
                        Path.Combine(currentDir, "App.config"),
                        Path.Combine(currentDir, "..", "..", "..", "App.config"),
                        Path.Combine(currentDir, "..", "..", "..", "..", "..", "src", "HenikenERP.Presentation", "App.config"),
                        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "..", "src", "HenikenERP.Presentation", "App.config")
                    };
                    
                    foreach (string path in possiblePaths)
                    {
                        string fullPath = Path.GetFullPath(path);
                        if (File.Exists(fullPath) && fullPath.EndsWith("App.config", StringComparison.OrdinalIgnoreCase))
                        {
                            appConfigPath = fullPath;
                            break;
                        }
                    }
                    
                    if (appConfigPath != null)
                    {
                        var configXml = new System.Xml.XmlDocument();
                        configXml.Load(appConfigPath);
                        
                        var appSettingsNode = configXml.SelectSingleNode("//appSettings");
                        if (appSettingsNode != null)
                        {
                            var settingNode = appSettingsNode.SelectSingleNode("//add[@key='DatabaseConnectionString']");
                            if (settingNode != null)
                            {
                                settingNode.Attributes["value"].Value = encryptedConnectionString;
                            }
                            else
                            {
                                var newSetting = configXml.CreateElement("add");
                                newSetting.SetAttribute("key", "DatabaseConnectionString");
                                newSetting.SetAttribute("value", encryptedConnectionString);
                                appSettingsNode.AppendChild(newSetting);
                            }
                            configXml.Save(appConfigPath);
                        }
                    }
                }
                catch
                {
                    // If saving to source App.config fails, that's okay - at least .exe.config is saved
                    // The app will work fine with just .exe.config
                }
                
                // Also save to database
                var setting = _unitOfWork.AppSettings.GetById(AppConstants.DATABASE_CONNECTION_STRING_KEY);
                if (setting == null)
                {
                    setting = new AppSetting
                    {
                        Setting_Key = AppConstants.DATABASE_CONNECTION_STRING_KEY,
                        Setting_Value = encryptedConnectionString,
                        Description = "Encrypted database connection string"
                    };
                    _unitOfWork.AppSettings.Add(setting);
                }
                else
                {
                    setting.Setting_Value = encryptedConnectionString;
                    _unitOfWork.AppSettings.Update(setting);
                }
                _unitOfWork.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving database connection", ex);
                return false;
            }
        }
        
        /// <summary>
        /// Test database connection
        /// </summary>
        public bool TestConnection(DatabaseConnectionDTO connectionDTO)
        {
            try
            {
                string connectionString = connectionDTO.GetConnectionString();
                using (var context = new DatabaseContext())
                {
                    return context.TestConnection(connectionString);
                }
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Get database connection string from settings
        /// </summary>
        public string GetConnectionString()
        {
            try
            {
                string encryptedConnectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
                if (string.IsNullOrEmpty(encryptedConnectionString))
                {
                    return string.Empty;
                }
                return EncryptionHelper.DecryptConnectionString(encryptedConnectionString);
            }
            catch
            {
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Get a setting value by key
        /// </summary>
        public string GetSetting(string key, string defaultValue = "")
        {
            try
            {
                var setting = _unitOfWork.AppSettings.GetById(key);
                return setting?.Setting_Value ?? defaultValue;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting setting {key}", ex);
                return defaultValue;
            }
        }
        
        /// <summary>
        /// Save a setting value
        /// </summary>
        public bool SaveSetting(string key, string value)
        {
            try
            {
                var setting = _unitOfWork.AppSettings.GetById(key);
                if (setting == null)
                {
                    setting = new AppSetting
                    {
                        Setting_Key = key,
                        Setting_Value = value,
                        Description = ""
                    };
                    _unitOfWork.AppSettings.Add(setting);
                }
                else
                {
                    setting.Setting_Value = value;
                    _unitOfWork.AppSettings.Update(setting);
                }
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving setting {key}", ex);
                return false;
            }
        }
    }
}

