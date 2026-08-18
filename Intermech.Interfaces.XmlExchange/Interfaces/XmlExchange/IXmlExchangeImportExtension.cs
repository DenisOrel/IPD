// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeImportExtension
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.IpsXmlViewer.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Интерфейс для расширений задачи импорта данных</summary>
public interface IXmlExchangeImportExtension : IXmlExchangeExtension
{
  /// <summary>
  /// Действия, которые может выполнять расширение задачи импорта
  /// </summary>
  XmlImportExtAction Actions { get; }

  /// <summary>
  /// Проверить, может ли расширение выполнить указанное действие
  /// </summary>
  /// <param name="action">Проверяемое действие</param>
  /// <returns>true - расширение может выполнить указанное действие</returns>
  bool CanProcess(XmlImportExtAction action);

  /// <summary>
  /// Выполнить действия по изменению метаданных после обработки ZIP-архива и записи в индекс
  /// </summary>
  /// <param name="action">Выполняемое действие</param>
  /// <param name="kernel">Микроядро XML (контейнер сервисов содержит все необходимые службы)</param>
  /// <param name="args">Дополнительные аргументы метода</param>
  /// <returns>Результат выполнения действия в виде ключей-значений</returns>
  Dictionary<string, object> Execute(
    XmlImportExtAction action,
    IKernel kernel,
    params object[] args);
}
