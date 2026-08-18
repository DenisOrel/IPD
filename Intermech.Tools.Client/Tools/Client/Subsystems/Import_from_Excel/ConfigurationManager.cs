// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ConfigurationManager
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public class ConfigurationManager
{
  private List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> _userConfigurations = new List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>();
  private List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> _commonConfigurations = new List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>();
  private const string ConfigurationName = "ExcelImport";

  public ConfigurationManager(bool isAdmin)
  {
    this.IsAdmin = isAdmin;
    this.LoadConfigarations();
  }

  public List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> Configurations(
    bool isAdmin = true)
  {
    List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> configurationList = new List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>();
    if (this.IsAdmin | isAdmin)
      configurationList.AddRange((IEnumerable<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) this._commonConfigurations);
    configurationList.AddRange((IEnumerable<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) this._userConfigurations);
    return configurationList;
  }

  public bool IsAdmin { get; }

  private void LoadConfigarations()
  {
    List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> configurationList1 = this.LoadCommonConfigurations();
    if (configurationList1 != null)
      this._commonConfigurations = configurationList1;
    List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> configurationList2 = this.LoadUserConfigurations();
    if (configurationList2 != null)
      this._userConfigurations = configurationList2;
    this.ConvertObsoleteSettings();
  }

  private List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> LoadCommonConfigurations()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobInformation config_info;
        byte[] config_file;
        sessionKeeper.Session.Configurations.LoadConfigData("ExcelImport", out config_info, out config_file, 0L);
        if (config_info.RealFileSize == 0L || (long) config_file.Length < config_info.PackedFileSize)
          return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) null;
        using (MemoryStream memoryStream = new MemoryStream(config_file))
          return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) new XmlSerializer(typeof (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>)).Deserialize((Stream) memoryStream);
      }
    }
    catch (Exception ex)
    {
      return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) null;
    }
  }

  private List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> LoadUserConfigurations()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobInformation config_info;
        byte[] config_file;
        sessionKeeper.Session.Configurations.LoadConfigData("ExcelImport", out config_info, out config_file);
        if (config_info.RealFileSize == 0L || (long) config_file.Length < config_info.PackedFileSize)
          return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) null;
        using (MemoryStream memoryStream = new MemoryStream(config_file))
          return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) new XmlSerializer(typeof (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>)).Deserialize((Stream) memoryStream);
      }
    }
    catch (Exception ex)
    {
      return (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) null;
    }
  }

  private void ConvertObsoleteSettings()
  {
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Open(Consts.ConfigName);
    if (configuration1 == null || !configuration1.HasProperty(Consts.ConfigurationData))
      return;
    string property = configuration1.GetProperty(Consts.ConfigurationData);
    if (string.IsNullOrEmpty(property))
      return;
    try
    {
      object obj;
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(property)))
        obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
      if (!(obj is Dictionary<string, StoredSettings> dictionary))
        return;
      List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> collection = new List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>();
      foreach (KeyValuePair<string, StoredSettings> keyValuePair in dictionary)
      {
        KeyValuePair<string, StoredSettings> concreteConfig = keyValuePair;
        if (!this._userConfigurations.Any<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>((Func<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration, bool>) (x => x.Name == concreteConfig.Key)))
        {
          CommonImportOptions commonImportOptions = CommonImportOptions.None;
          StoredSettings storedSettings = concreteConfig.Value;
          ImportFlags importFlags = storedSettings.ImportFlags;
          if (importFlags.HasFlag((Enum) ImportFlags.SkipFirtsRow))
            commonImportOptions |= CommonImportOptions.SkipFirstRow;
          if (importFlags.HasFlag((Enum) ImportFlags.IgnoreExistingObjectErrs))
            commonImportOptions |= CommonImportOptions.IgnoreExistingObjectErrs;
          if (importFlags.HasFlag((Enum) ImportFlags.IgnoreExisitingRelationErrs))
            commonImportOptions |= CommonImportOptions.IgnoreExistingRelationErrs;
          Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration configuration2 = new Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration()
          {
            Type = ConfigurationType.Personal,
            Name = concreteConfig.Key,
            CommonImportOptions = commonImportOptions
          };
          storedSettings = concreteConfig.Value;
          SettingItem[] settingItems = storedSettings.SettingItems;
          for (int index = 0; index < settingItems.Length; ++index)
          {
            SettingItem settingItem = settingItems[index];
            ColumnConfiguration columnConfiguration = new ColumnConfiguration()
            {
              ItemType = settingItem.ItemType,
              AttributeValue = settingItem.AttributeValue,
              DataType = settingItem.DataType,
              Index = settingItem.Index != 0 || index <= 0 ? settingItem.Index : index,
              SettingItemAttributeBelongs = settingItem.SettingItemAttributeBelongs,
              SettingItemAttributeUpdateMode = settingItem.SettingItemAttributeUpdateMode,
              SyncImbase = settingItem.SyncImbase,
              TypeId = settingItem.TypeId,
              ValueKind = settingItem.ValueKind
            };
            configuration2.ColumnConfigurations.Add(columnConfiguration);
          }
          collection.Add(configuration2);
        }
      }
      if (collection.Count > 0)
      {
        this._userConfigurations.AddRange((IEnumerable<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) collection);
        this.SaveUserConfigurations(this._userConfigurations);
      }
      service.Delete(Consts.ConfigName);
    }
    catch
    {
      service.Delete(Consts.ConfigName);
    }
  }

  private void SaveCommonConfigurations(List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> commonConfigurations)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new XmlSerializer(typeof (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>)).Serialize((Stream) memoryStream, (object) commonConfigurations);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!sessionKeeper.Session.IsAdmin)
          return;
        sessionKeeper.Session.Configurations.WriteConfigData(new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "ExcelImport", ArcMethods.NotPacked, string.Empty), memoryStream.ToArray(), 0L);
      }
    }
  }

  private void SaveUserConfigurations(List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration> userConfigurations)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new XmlSerializer(typeof (List<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>)).Serialize((Stream) memoryStream, (object) userConfigurations);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.Configurations.WriteConfigData(new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "ExcelImport", ArcMethods.NotPacked, string.Empty), memoryStream.ToArray());
    }
  }

  public bool SaveConfiguration(Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration configuration, bool needConfirm = true)
  {
    if (configuration.Type == ConfigurationType.Common)
    {
      if (!this.IsAdmin)
        throw new Exception("Нет прав для сохранения общей конфигурации! Только пользователи с правами администратора могут сохранять общие конфигурации");
      int index = this._commonConfigurations.FindIndex((Predicate<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) (x => x.Type == configuration.Type && x.Name == configuration.Name));
      if (index != -1)
      {
        if (needConfirm && MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_233"), LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
          return false;
        this._commonConfigurations[index] = configuration;
        this.SaveCommonConfigurations(this._commonConfigurations);
        return true;
      }
      this._commonConfigurations.Add(configuration);
      this.SaveCommonConfigurations(this._commonConfigurations);
      return true;
    }
    int index1 = this._userConfigurations.FindIndex((Predicate<Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration>) (x => x.Type == configuration.Type && x.Name == configuration.Name));
    if (index1 != -1)
    {
      if (needConfirm && MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_233"), LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        return false;
      this._userConfigurations[index1] = configuration;
      this.SaveUserConfigurations(this._userConfigurations);
      return true;
    }
    this._userConfigurations.Add(configuration);
    this.SaveUserConfigurations(this._userConfigurations);
    return true;
  }

  public void Remove(Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration configuration, bool needConfirm = true)
  {
    if (this.IsAdmin)
    {
      int index = this._commonConfigurations.IndexOf(configuration);
      if (index != -1)
      {
        if (needConfirm && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Tools.Client_234"), (object) configuration.Name), LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
          return;
        this._commonConfigurations.RemoveAt(index);
        this.SaveCommonConfigurations(this._commonConfigurations);
        return;
      }
    }
    int index1 = this._userConfigurations.IndexOf(configuration);
    if (index1 == -1 || needConfirm && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Tools.Client_234"), (object) configuration.Name), LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
      return;
    this._userConfigurations.RemoveAt(index1);
    this.SaveUserConfigurations(this._userConfigurations);
  }

  public Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration LoadConfigurationFromFile(
    string filePath)
  {
    if (!File.Exists(filePath))
      throw new FileNotFoundException();
    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
      return (Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration) new XmlSerializer(typeof (Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration)).Deserialize((Stream) fileStream);
  }

  public void SaveConfigurationToFile(Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration configuration, string filePath)
  {
    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
      new XmlSerializer(typeof (Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration)).Serialize((Stream) fileStream, (object) configuration);
  }
}
