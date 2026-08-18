// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IPdmConfiguratorService
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Интерфейс службы конфигуратора составов IPS</summary>
public interface IPdmConfiguratorService
{
  /// <summary>
  /// Управление контекстом конфигуратора составов, соответствующим указанному ключу.
  /// Значение null для контекста означает его удаление из кэша
  /// </summary>
  /// <param name="usrSession">Guid сессии (вызов метода с клиента), UserSession - вызов метода с сервера</param>
  /// <param name="key">Ключ контекста конфигуратора составов IPS</param>
  /// <returns>Контекст конфигуратора составов, соответствующий указанному ключу или null</returns>
  PdmConfiguratorContext this[object usrSession, RelationPair key] { get; set; }

  /// <summary>
  /// Удалить из кэша всю информацию, касающуюся указанного соединения клиента IPS с сервером приложений
  /// (UserID и Handle будут получены у сессии)
  /// </summary>
  /// <param name="usrSession">Guid сессии (вызов метода с клиента), UserSession - вызов метода с сервера</param>
  void ResetSessionCache(object usrSession);

  /// <summary>Обновить все опции в кэше</summary>
  /// <param name="usrSession">Сессия (Guid или IUSerSession)</param>
  void LoadOptions(object usrSession);

  /// <summary>Обновить указанные опции в кэше</summary>
  /// <param name="usrSession">Сессия (Guid или IUSerSession)</param>
  /// <param name="options">Список идентификаторов версий обновляемых опций</param>
  void LoadOptions(object usrSession, IList<long> options);

  /// <summary>
  /// Заполнить описание в объектах и их дочерних элементах, если есть необходимость
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="objs">Обрабатываемые объекты</param>
  /// <returns>Линейный список всех загруженных описаний</returns>
  List<PdmAnalyzedOptionObject> LoadDescriptions(Guid sessionGuid, PdmAnalyzedOptionObjects objs);

  /// <summary>
  /// Загрузить опции всех обработанных объектов, которые найдены в коллекции
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="objs">Коллекция объектов, информация об опциях которых будет загружена</param>
  /// <returns>Опции обработанных объектов из указанной коллекции</returns>
  List<ObjectOptionsHolder> LoadObjectsOptions(Guid sessionGuid, PdmAnalyzedOptionObjects objs);

  /// <summary>
  /// Загрузить опции всех обработанных объектов, которые найдены в коллекции
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="items">Коллекция объектов, информация об опциях которых будет загружена</param>
  /// <returns>Опции обработанных объектов из указанной коллекции</returns>
  List<ObjectOptionsHolder> LoadObjectsOptions(
    Guid sessionGuid,
    List<PdmAnalyzedOptionObject> items);

  /// <summary>
  /// Возвращает таблицу с объектами,
  /// которым назначена опция c указаным id
  /// </summary>
  /// <param name="optionID">id опции</param>
  /// <param name="queryParams">Параметры выполнения запроса</param>
  /// <param name="sessionGuid"> Guid сессии </param>
  /// <returns></returns>
  DataTable GetDataTable(long optionID, DBRecordSetParams queryParams, Guid sessionGuid);

  /// <summary>
  /// Начать выполнение анализа опций указанных объектов, при необходимости добавить в граф
  /// дополнительные идентификаторы версий объектов, которые тоже требуется обработать.
  /// Служба опрашивает все зарегистрированные в ней анализаторы для выполнения данного анализа.
  /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="objs">Обрабатываемые объекты</param>
  /// <param name="options">Параметры</param>
  /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
  /// Значение Guid.Empty означает невозможность начать анализ</returns>
  Guid Analyze(Guid sessionGuid, PdmAnalyzedOptionObjects objs, PdmAnalyzerFlags options);

  /// <summary>
  /// Начать выполнение анализа опций указанных объектов, при необходимости добавить в граф
  /// дополнительные идентификаторы версий объектов, которые тоже требуется обработать.
  /// Служба опрашивает все зарегистрированные в ней анализаторы для выполнения данного анализа.
  /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="objs">Обрабатываемые объекты</param>
  /// <param name="options">Параметры</param>
  /// <param name="excludedObjects">Список идентификаторов версий объектов, которые должны быть проигнорированы анализаторами</param>
  /// <param name="excludedOptions">Список идентификаторов версий опций, которые должны быть проигнорированы анализаторами</param>
  /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
  /// Значение Guid.Empty означает невозможность начать анализ</returns>
  Guid Analyze(
    Guid sessionGuid,
    PdmAnalyzedOptionObjects objs,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions);

  /// <summary>
  /// Запросить статус указанного задания на сервере. Если задание успешно или ошибочно
  /// завершено, вернётся полный пакет со статусом задания, а само задание будет
  /// удалено на серверной стороне вместе со своим потоком
  /// </summary>
  /// <param name="jobID">Задание</param>
  /// <returns>Статус указанного задания на сервере</returns>
  PdmOptionsAnalyzerJobStatus QueryJobStatus(Guid jobID);

  /// <summary>Прервать указанное задание</summary>
  /// <param name="jobID">Задание</param>
  /// <returns>true, если задание было найдено и остановлено</returns>
  bool CancelJob(Guid jobID);

  /// <summary>Выполнить регистрацию анализатора в службе</summary>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true, если регистрация выполнена успешно</returns>
  bool RegisterAnalyzer(IPdmOptionsAnalyzer analyzer);

  /// <summary>Выполнить удаление анализатора из службы</summary>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true, если удаление выполнено успешно</returns>
  bool UnregisterAnalyzer(IPdmOptionsAnalyzer analyzer);

  /// <summary>Выполнить удаление анализатора по его Guid из службы</summary>
  /// <param name="analyzerGuid">Guid анализатора</param>
  /// <returns>true, если удаление выполнено успешно</returns>
  bool UnregisterAnalyzer(Guid analyzerGuid);

  /// <summary>
  /// Начать выполнение рекурсивной раскрутки конфигурируемых составов объектов, при необходимости добавить в граф
  /// дополнительные идентификаторы версий объектов, которые тоже требуется обработать.
  /// Служба опрашивает все зарегистрированные в ней анализаторы для выполнения данного анализа.
  /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
  /// <param name="rootObject">Информация о корневом объекте конфигурируемого состава</param>
  /// <param name="rootObjectPath">Относительный путь от корневого объекта к обрабатываемым объектам</param>
  /// <param name="objs">Обрабатываемые объекты</param>
  /// <param name="args">Аргументы для вызова службы</param>
  /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
  /// Значение Guid.Empty означает невозможность начать анализ</returns>
  Guid Browse(
    Guid sessionGuid,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    CompositionObjects objs,
    PdmCompositionBrowserEventArgs args);

  /// <summary>
  /// Запросить статус указанного задания по раскрутке состава на сервере. Если задание успешно или ошибочно
  /// завершено, вернётся полный пакет со статусом задания, а само задание будет
  /// удалено на серверной стороне вместе со своим потоком
  /// </summary>
  /// <param name="jobID">Задание по раскрутке составов</param>
  /// <returns>Статус указанного задания на сервере</returns>
  PdmCompositionBrowserJobStatus QueryBrowserStatus(Guid jobID);

  /// <summary>Прервать указанное задание по раскрутке составов</summary>
  /// <param name="jobID">Задание по раскрутке составов</param>
  /// <returns>true, если задание было найдено и остановлено</returns>
  bool CancelBrowse(Guid jobID);

  /// <summary>
  /// Выполнить регистрацию анализатора по раскрутке составов в службе
  /// </summary>
  /// <param name="analyzer">Анализатор по раскрутке составов</param>
  /// <returns>true, если регистрация выполнена успешно</returns>
  bool RegisterBrowser(IPdmCompositionBrowser analyzer);

  /// <summary>
  /// Выполнить удаление анализатора по раскрутке составов из службы
  /// </summary>
  /// <param name="analyzer">Анализатор по раскрутке составов</param>
  /// <returns>true, если удаление выполнено успешно</returns>
  bool UnregisterBrowser(IPdmCompositionBrowser analyzer);

  /// <summary>
  /// Выполнить удаление анализатора по раскрутке составов по его Guid из службы
  /// </summary>
  /// <param name="analyzerGuid">Guid анализатора по раскрутке составов</param>
  /// <returns>true, если удаление выполнено успешно</returns>
  bool UnregisterBrowser(Guid analyzerGuid);
}
