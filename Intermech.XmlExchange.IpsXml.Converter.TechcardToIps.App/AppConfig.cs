// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.AppConfig
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Resources;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal static class AppConfig
{
  private static void AddConfigItem(
    KeyValueConfigurationCollection settings,
    string key,
    string value)
  {
    if (settings[key] == null)
      settings.Add(key, value);
    else
      settings[key].Value = value;
  }

  public static InputParams ReadConfig()
  {
    InputParams inputParams = new InputParams();
    NameValueCollection appSettings = ConfigurationManager.AppSettings;
    inputParams.UserName = appSettings["UserName"] ?? string.Empty;
    inputParams.UserPassword = appSettings["UserPassword"] ?? string.Empty;
    inputParams.UserRole = appSettings["UserRole"] ?? string.Empty;
    inputParams.ConfigFile = appSettings["ConfigFile"] ?? string.Empty;
    inputParams.InputFile = appSettings["InputFile"] ?? string.Empty;
    inputParams.WorkDir = appSettings["WorkDir"] ?? string.Empty;
    bool result;
    if (bool.TryParse(appSettings["ShowProgress"] ?? string.Empty, out result))
      inputParams.ShowProgress = new bool?(result);
    inputParams.RolsynScriptCompiler = appSettings["RoslynScriptCompiler"] ?? string.Empty;
    return inputParams;
  }

  public static void SaveConfig(InputParams inputParams)
  {
    System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
    AppConfig.AddConfigItem(settings, "UserName", inputParams.UserName);
    AppConfig.AddConfigItem(settings, "UserPassword", inputParams.UserPassword);
    AppConfig.AddConfigItem(settings, "UserRole", inputParams.UserRole);
    AppConfig.AddConfigItem(settings, "ConfigFile", inputParams.ConfigFile);
    AppConfig.AddConfigItem(settings, "InputFile", inputParams.InputFile);
    AppConfig.AddConfigItem(settings, "WorkDir", inputParams.WorkDir);
    AppConfig.AddConfigItem(settings, "ShowProgress", inputParams.ShowProgress.ToString());
    AppConfig.AddConfigItem(settings, "RoslynScriptCompiler", inputParams.RolsynScriptCompiler);
    configuration.Save(ConfigurationSaveMode.Modified);
    ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
  }

  public static bool CheckInputParams(InputParams inputParams, bool showReason = false)
  {
    bool flag1 = string.IsNullOrEmpty(inputParams.UserName);
    if (flag1 & showReason)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgEmptyUserName"));
    }
    if (flag1)
      return false;
    bool flag2 = string.IsNullOrEmpty(inputParams.UserPassword);
    if (flag2 & showReason)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgEmptyUserPassword"));
    }
    if (flag2)
      return false;
    bool flag3 = string.IsNullOrEmpty(inputParams.UserRole);
    if (flag3 & showReason)
    {
      int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgEmptyUserRole"));
    }
    if (flag3)
      return false;
    bool flag4 = string.IsNullOrEmpty(inputParams.ConfigFile);
    if (!flag4)
      flag4 = !File.Exists(inputParams.ConfigFile);
    if (flag4 & showReason)
    {
      int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgConfigFileEmptyOrNotExists"));
    }
    if (flag4)
      return false;
    bool flag5 = string.IsNullOrEmpty(inputParams.InputFile);
    if (!flag5)
      flag5 = !File.Exists(inputParams.InputFile);
    if (flag5 & showReason)
    {
      int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgEmptyInputFileEmptyOrNotExists"));
    }
    if (flag5)
      return false;
    bool flag6 = string.IsNullOrEmpty(inputParams.RolsynScriptCompiler);
    if (!flag6)
      flag6 = !File.Exists(inputParams.RolsynScriptCompiler);
    if (flag6 & showReason)
    {
      int num6 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgRoslynCompilerEmptyOrNotExists"));
    }
    if (flag6)
      return false;
    bool flag7 = string.IsNullOrEmpty(inputParams.WorkDir);
    if (!flag7)
      flag7 = !Directory.Exists(inputParams.WorkDir);
    if (flag7 & showReason)
    {
      int num7 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("msgWorkDirEmptyOrNotExists"));
    }
    return !flag7;
  }
}
