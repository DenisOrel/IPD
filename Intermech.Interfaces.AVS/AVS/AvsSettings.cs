// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AvsSettings
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Класс для хранения параметров AVS</summary>
[Serializable]
public class AvsSettings
{
  /// <summary>Общие настройки AVS</summary>
  public AvsSettingsSection General { get; set; }

  /// <summary>Настройки подбора</summary>
  public AvsSettingsSection Podbor { get; set; }

  /// <summary>Настройки позиционного обозначения</summary>
  public AvsSettingsSection PosDesignation { get; set; }

  /// <summary>Настройки проверки спецификации на ошибки</summary>
  public AvsSettingsSection CheckSPec { get; set; }

  /// <summary>Настройки проверки спецификации на ошибки</summary>
  public AvsSettingsSection CheckEList { get; set; }
}
