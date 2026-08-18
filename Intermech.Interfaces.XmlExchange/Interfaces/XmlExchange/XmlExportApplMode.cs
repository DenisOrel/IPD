// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportApplMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Режим проверки применяемости/раскрытия состава для объектов
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportApplMode
{
  /// <summary>Раскрытие состава всех объектов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_1")] AllObjects,
  /// <summary>Раскрытие состава только головных объектов задачи</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_2")] RootObjectsOnly,
  /// <summary>Запрет раскрытия состава</summary>
  /// <remarks>Данный режим имеет наивысший приоритет</remarks>
  [CustomDescription("Attribute.Interfaces.XmlExchange_3")] NoObjects,
}
