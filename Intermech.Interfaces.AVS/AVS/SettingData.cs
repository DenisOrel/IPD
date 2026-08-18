// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SettingData
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Универсальный класс для хранения данных параметра системы
/// </summary>
[Serializable]
public class SettingData : TypedValueContainer
{
  public bool IsAdmin { get; set; }

  public SettingData(object value, Type type)
    : base(value, type)
  {
  }

  public SettingData(object value, Type type, bool isAdmin)
    : base(value, type)
  {
    this.IsAdmin = isAdmin;
  }
}
