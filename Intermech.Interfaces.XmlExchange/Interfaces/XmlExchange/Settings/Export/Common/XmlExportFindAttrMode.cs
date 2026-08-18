// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Common.XmlExportFindAttrMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Common;

/// <summary>Параметры атрибутов при поиске (объектов)</summary>
[Flags]
public enum XmlExportFindAttrMode
{
  /// <summary>Дополнительный режим не задан</summary>
  None = 0,
  /// <summary>Игнорировать условие, если атрибут не задан в XML</summary>
  SkipNotExists = 1,
  /// <summary>
  /// Игнорировать условие, если значение атрибута в XML не задано
  /// </summary>
  SkipEmpty = 2,
}
