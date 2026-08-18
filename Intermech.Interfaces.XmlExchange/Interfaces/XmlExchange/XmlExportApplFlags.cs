// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportApplFlags
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Дополнительные флаги применяемости/состава для объектов
/// </summary>
[Flags]
public enum XmlExportApplFlags
{
  /// <summary>Доп. флаги не установлены</summary>
  None = 0,
  /// <summary>
  /// Пометить полученный объект для XmlExchangeExportAppl как головной
  /// </summary>
  /// <remarks>Используется для дальнейшего разворота применяемости / состава объекта с признаком XmlExportApplMode.RootObjectsOnly</remarks>
  MarkAsRoot = 1,
}
