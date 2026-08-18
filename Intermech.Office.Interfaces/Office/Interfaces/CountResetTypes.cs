// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.CountResetTypes
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Режим обнуления счетчика.</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum CountResetTypes
{
  /// <summary>Не обнулять</summary>
  [Description("Не обнулять")] None,
  /// <summary>Обнулять каждый год</summary>
  [Description("Обнулять каждый год")] PerYear,
  /// <summary>Обнулять каждый месяц</summary>
  [Description("Обнулять каждый месяц")] PerMonth,
}
