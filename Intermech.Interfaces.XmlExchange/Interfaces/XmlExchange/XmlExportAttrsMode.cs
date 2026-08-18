// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportAttrsMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Режим выгрузки атрибутов объектов/связей</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportAttrsMode
{
  /// <summary>
  /// Дополнительный режим не задан - выгружаются только заданные атрибуты у типов
  /// </summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_4")] None,
  /// <summary>
  /// Выгрузка предопределенных атрибутов для типа объекта/связи
  /// </summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_5")] DefinedAttributes,
  /// <summary>Выгрузка всех атрибутов объекта/связи</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_6")] AllAttributes,
}
