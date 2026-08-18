// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.StoredSettings
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Obsolete]
[Serializable]
public struct StoredSettings
{
  public StoredSettings(SettingItem[] settingItems, ImportFlags importFlags)
    : this()
  {
    this.SettingItems = settingItems;
    this.ImportFlags = importFlags;
  }

  public SettingItem[] SettingItems { get; set; }

  public ImportFlags ImportFlags { get; set; }
}
