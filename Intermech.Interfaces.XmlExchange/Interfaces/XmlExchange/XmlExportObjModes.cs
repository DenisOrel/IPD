// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportObjModes
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Режимы экспорта/обработки объектов</summary>
[Flags]
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportObjModes
{
  /// <summary>Режим не определен</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_14")] None = 0,
  /// <summary>"Исключение" объекта из XML</summary>
  /// <remarks>Объекты вместе со связью с родительским объектом исключается их XML.
  /// Настроенные параметры объекта / связи записываются у дочерних объектов, при условии их наличия</remarks>
  [CustomDescription("Attribute.Interfaces.XmlExchange_15")] ExcludeObjOnly = 1,
  /// <summary>"Исключение" объекта из XML вместе с параметрами</summary>
  /// <remarks>Объекты вместе со связью с родительским объектом исключается их XML вместе с их параметрами.
  /// Настроенные параметры объекта / связи с родительским объектом в XML не пишутся</remarks>
  [CustomDescription("Attribute.Interfaces.XmlExchange_16")] ExcludeObjWithParams = 2,
}
