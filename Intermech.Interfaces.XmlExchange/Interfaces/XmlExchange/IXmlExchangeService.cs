// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeService
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Интерфейс серверной службы IXmlExchangeService</summary>
public interface IXmlExchangeService
{
  /// <summary>Создание задачи экспорта данных</summary>
  /// <param name="sessionGuid">Идентификатор сессии</param>
  /// <returns>Задача экспорта</returns>
  IXmlExchangeExportTask CreateExportTask(Guid sessionGuid);

  /// <summary>Освобождение ресурсов задачи экспорта</summary>
  /// <param name="taskGuid">Уникальный идентификатор задания</param>
  void DisposeExportTask(Guid taskGuid);

  /// <summary>Создание задачи импорта данных</summary>
  /// <param name="sessionGuid">Идентификатор сессии</param>
  /// <returns>Задача импорта</returns>
  IXmlExchangeImportTask CreateImportTask(Guid sessionGuid);

  /// <summary>Освобождение ресурсов задачи импорта</summary>
  /// <param name="taskGuid">Уникальный идентификатор задания</param>
  void DisposeImportTask(Guid taskGuid);

  /// <summary>Список расширений для импорта</summary>
  IXmlExchangeImportExtension[] ImportExtList { get; }

  /// <summary>Список расширений для экспорта</summary>
  IXmlExchangeExportExtension[] ExportExtList { get; }

  /// <summary>Регистрация расширения экспорта/импорта данных</summary>
  /// <param name="dataExtension">Расширение экспорта/импорта</param>
  void RegisterExtension(IXmlExchangeExtension dataExtension);

  /// <summary>
  /// Удаление регистрации расширения экспорта/импорта данных
  /// </summary>
  /// <param name="dataExtension">Удаляемое расширение</param>
  void UnregisterExtension(IXmlExchangeExtension dataExtension);

  /// <summary>
  /// Получить упорядоченный по приоритету (в порядке убывания) список расширений импорта
  /// для выполнения указанного действия
  /// </summary>
  /// <param name="action">Выполняемое действие</param>
  /// <returns>Упорядоченный по приоритету (в порядке убывания) список расширений импорта для выполнения указанного действия</returns>
  List<IXmlExchangeImportExtension> GetImportExtensions(XmlImportExtAction action);

  /// <summary>
  /// Получить упорядоченный по приоритету (в порядке убывания) список расширений экспорта
  /// для выполнения указанного действия
  /// </summary>
  /// <param name="action">Выполняемое действие</param>
  /// <returns>Упорядоченный по приоритету (в порядке убывания) список расширений экспорта для выполнения указанного действия</returns>
  List<IXmlExchangeExportExtension> GetExportExtensions(XmlExportExtAction action);
}
