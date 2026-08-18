// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSSpecificationType
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Режимы вида спецификации</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum AVSSpecificationType
{
  /// <summary>ЕСКД</summary>
  [Description("ЕСКД")] ESKD,
  /// <summary>ЕСКД</summary>
  [Description("Автомобилестроительная")] AutoProm,
  /// <summary>Экспортная</summary>
  [Description("Экспортная"), Browsable(false)] Export,
}
