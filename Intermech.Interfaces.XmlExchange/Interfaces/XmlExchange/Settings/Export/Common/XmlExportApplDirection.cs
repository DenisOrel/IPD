// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Common.XmlExportApplDirection
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Common;

/// <summary>
/// Направления действия правила применяемости / раскрытия составов  (вверх / вниз / в обоих направлениях)
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportApplDirection
{
  /// <summary>Вниз (состав объекта)</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_17")] Down,
  /// <summary>Вверх (применяемость объекта)</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_18")] Up,
  /// <summary>Оба направления (вверх и вниз)</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_19")] UpAndDown,
}
