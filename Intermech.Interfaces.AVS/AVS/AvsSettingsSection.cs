// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AvsSettingsSection
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Базовый класс для хранения параметров</summary>
[Serializable]
public class AvsSettingsSection : AppSettingsBase
{
  internal readonly IDictionary<string, SettingData> _settings = (IDictionary<string, SettingData>) new Dictionary<string, SettingData>();

  /// <summary>Имя секции параметров</summary>
  public string SectionName { get; set; } = string.Empty;

  public List<string> Names => this._settings.Keys.ToList<string>();

  public virtual object this[string settingName]
  {
    get
    {
      SettingData settingData;
      return !this._settings.TryGetValue(settingName, out settingData) ? (object) null : settingData.Value;
    }
    set
    {
      if (value is SettingData settingData1)
      {
        if (this._settings.ContainsKey(settingName))
        {
          SettingData setting = this._settings[settingName];
        }
        this._settings[settingName] = settingData1;
      }
      else
      {
        SettingData settingData;
        if (this._settings.TryGetValue(settingName, out settingData))
        {
          settingData.Value = value;
        }
        else
        {
          Type type1;
          switch (value)
          {
            case Enum _:
              type1 = typeof (Enum);
              break;
            case int _:
              type1 = typeof (int);
              break;
            case bool _:
              type1 = typeof (bool);
              break;
            case float _:
              type1 = typeof (float);
              break;
            case DateTime _:
              type1 = typeof (DateTime);
              break;
            case string _:
              type1 = typeof (string);
              break;
            default:
              type1 = value.GetType();
              break;
          }
          Type type2 = type1;
          this._settings[settingName] = new SettingData(value, type2);
        }
      }
    }
  }

  public Type TypeOf(string settingName)
  {
    SettingData settingData;
    return !this._settings.TryGetValue(settingName, out settingData) ? typeof (object) : settingData.Type;
  }

  public bool IsAdmin(string settingName)
  {
    SettingData settingData;
    return this._settings.TryGetValue(settingName, out settingData) && settingData.IsAdmin;
  }

  public void Exclude(string settingName)
  {
    if (!this._settings.ContainsKey(settingName))
      return;
    ((ICollection<KeyValuePair<string, SettingData>>) this._settings).Remove(new KeyValuePair<string, SettingData>(settingName, this._settings[settingName]));
  }
}
