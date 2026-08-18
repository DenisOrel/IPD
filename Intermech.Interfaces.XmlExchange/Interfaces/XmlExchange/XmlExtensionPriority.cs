// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExtensionPriority
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Приоритет расширения для задач экспорта / импорта из XML
/// </summary>
[Serializable]
public enum XmlExtensionPriority
{
  /// <summary>Приоритет по умолчанию</summary>
  Default = 0,
  /// <summary>Высокий приоритет</summary>
  High = 1024, // 0x00000400
  /// <summary>Наивысший приоритет</summary>
  Highest = 1048576, // 0x00100000
  /// <summary>Критический приоритет</summary>
  Critical = 2147483647, // 0x7FFFFFFF
}
