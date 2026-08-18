// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AvsSettingFlagsSection
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Базовый класс для хранения набора исключительно булевых параметров
/// </summary>
[Serializable]
public class AvsSettingFlagsSection : AvsSettingsSection
{
  private const string keyFlags = "Flags";

  public override object this[string settingName]
  {
    get
    {
      SettingData settingData;
      return this._settings.TryGetValue("Flags", out settingData) ? settingData.Value : (object) null;
    }
    set
    {
      if (value is SettingData settingData1 && settingData1.Type == typeof (int))
        this._settings["Flags"] = settingData1;
      else if (value is int num1)
      {
        SettingData settingData;
        if (this._settings.TryGetValue("Flags", out settingData))
        {
          if (!(settingData.Type == typeof (int)))
            return;
          settingData.Value = (object) num1;
        }
        else
          this._settings["Flags"] = new SettingData((object) num1, typeof (int));
      }
      else
      {
        object obj;
        if (!((obj = value) is bool))
          return;
        int num = (bool) obj ? 1 : 0;
      }
    }
  }
}
