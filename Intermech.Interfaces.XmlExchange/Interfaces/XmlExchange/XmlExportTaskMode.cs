// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportTaskMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Режим выгрузки данных в XML</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportTaskMode
{
  /// <summary>Выгрузка исходных объектов в отдельном пакете XML</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_22")] SingleItem,
  /// <summary>Выгрузка исходных объектов в одном пакете XML</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_23")] MultiItems,
}
