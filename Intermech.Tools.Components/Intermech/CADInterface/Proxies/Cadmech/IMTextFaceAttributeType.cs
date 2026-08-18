// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextFaceAttributeType
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// 
/// </summary>
public enum IMTextFaceAttributeType
{
  /// <summary>
  /// 
  /// </summary>
  None = -1, // 0xFFFFFFFF
  /// <summary>
  /// 
  /// </summary>
  [Description("Шероховатость")] St = 0,
  /// <summary>
  /// 
  /// </summary>
  [Description("Обозначение базы")] Base = 1,
  /// <summary>
  /// 
  /// </summary>
  [Description("Допуски формы и расположения")] Tlr = 2,
  /// <summary>
  /// 
  /// </summary>
  Des = 3,
  /// <summary>
  /// 
  /// </summary>
  Hardness = 4,
  /// <summary>
  /// 
  /// </summary>
  [Description("Покрытие")] Sc = 5,
  /// <summary>
  /// 
  /// </summary>
  [Description("Текстовая выноска")] Cl = 6,
  /// <summary>
  /// 
  /// </summary>
  [Description("Маркирование")] Ms = 7,
  Vl = 8,
  /// <summary>
  /// 
  /// </summary>
  [Description("Сшивание")] DjSeam = 9,
  /// <summary>
  /// 
  /// </summary>
  [Description("Пайка")] DjSolder = 10, // 0x0000000A
  /// <summary>
  /// 
  /// </summary>
  [Description("Клейка")] DjSplice = 11, // 0x0000000B
  /// <summary>
  /// 
  /// </summary>
  [Description("Сшивание скобами")] DjWirestich = 12, // 0x0000000C
  /// <summary>
  /// 
  /// </summary>
  [Description("Сварное соединение")] Wj = 13, // 0x0000000D
  Grid = 14, // 0x0000000E
  Tt = 15, // 0x0000000F
  Cogdata = 16, // 0x00000010
  Dimension = 17, // 0x00000011
  SpRecord = 18, // 0x00000012
  WjTable = 19, // 0x00000013
  /// <summary>
  /// 
  /// </summary>
  [Description("Параметр")] Parameter = 20, // 0x00000014
  Max = 23, // 0x00000017
}
