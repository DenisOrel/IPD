// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Common.XmlExportAttributeMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Common;

/// <summary>Режимы экспорта/обработки атрибута</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportAttributeMode
{
  /// <summary>Режим не определен</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_24")] None,
  /// <summary>"Исключение" атрибута из XML</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_25")] Exclude,
}
