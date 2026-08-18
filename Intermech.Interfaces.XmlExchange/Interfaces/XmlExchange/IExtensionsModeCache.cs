// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IExtensionsModeCache
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Интерфейс кэша, который хранит Guid-ы расширений и флажки, указывающие на разрешение
/// либо на запрет запуска данных расширений
/// </summary>
public interface IExtensionsModeCache
{
  /// <summary>
  /// Проверить, разрешено ли расширение с указанным идентификатором
  /// </summary>
  /// <param name="extGuid">Уникальный идентификатор расширения</param>
  /// <returns>true - расширение разрешено использовать</returns>
  bool IsEnabled(Guid extGuid);

  /// <summary>
  /// Проверить, разрешено ли указанное расширение расширение
  /// (учитывается конфигурация импорта, а также свойство IsSystem самого расширения
  /// </summary>
  /// <param name="ext">Проверяемое расширение</param>
  /// <returns>true - расширение разрешено использовать</returns>
  bool IsEnabled(IXmlExchangeImportExtension ext);
}
