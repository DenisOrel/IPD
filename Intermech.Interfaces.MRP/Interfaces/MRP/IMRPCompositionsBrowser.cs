// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPCompositionsBrowser
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Серверная служба, позволяющая разобрать состав производственного заказа и сформировать
/// древовидную структуту с элементами и действиями, необходимыми для формирования
/// окончательного состава заказа. Данный интерфейс позволяет клиентской стороне
/// формировать очереди заданий и получать их состояние и результаты работы
/// </summary>
public interface IMRPCompositionsBrowser
{
  /// <summary>
  /// Запустить серверную задачу по формированию списка действий, требуемых для
  /// преобразования состава производственного заказа из исходного в требуемый
  /// согласно технического задания (состав изделий + состав документации &gt;&gt; состав экземпляров
  /// и партий + состав документации, с созданием/изменением связей, атрибутов, объектов)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии, в рамках которой будет выполняться задание</param>
  /// <param name="holder">Исходные данные, поступившие из мастера по созданию производственных заказов</param>
  /// <param name="threadsCount">Количество потоков для параллельного выполнения заданий (-1 - будет выбрано количество по умолчанию)</param>
  /// <param name="autoComplete">Самозавершающаяся очередь задач (по умолчанию очередь полностью освободит все ресурсы, как только будет выполнено последнее задание)</param>
  /// <returns>Уникальный идентификатор задания</returns>
  Guid StartActionsCreateJob(
    Guid sessionGuid,
    ManufactureOrderHolder holder,
    int threadsCount,
    bool autoComplete);

  /// <summary>Прервать выполнение указанной задачи</summary>
  /// <param name="jobID">Уникальный идентификатор задачи</param>
  void CancelJob(Guid jobID);

  /// <summary>
  /// Запустить серверную задачу по изучению состава экземпляров/партий, входящих в состав производственного заказа,
  /// после замены в них маршрутов обработки
  /// </summary>
  /// <param name="sessionGuid">Guid сессии, в рамках которой будет выполняться задание</param>
  /// <param name="rootObject">Корневой объект состава</param>
  /// <param name="rootObjectPath">Относительный путь от корневого объекта к обрабатываемым объектам</param>
  /// <param name="projObj">Корневой объект типа "Экземпляры и партии", состав которого требуется изучить</param>
  /// <param name="holder">Исходные данные, поступившие из мастера по созданию производственных заказов</param>
  /// <param name="threadsCount">Количество потоков для параллельного выполнения заданий (-1 - будет выбрано количество по умолчанию)</param>
  /// <param name="autoComplete">Самозавершающаяся очередь задач (по умолчанию очередь полностью освободит все ресурсы, как только будет выполнено последнее задание)</param>
  /// <returns>Уникальный идентификатор задания</returns>
  Guid StartTechRouteChangeJob(
    Guid sessionGuid,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    long projObj,
    ManufactureOrderHolder holder,
    int threadsCount,
    bool autoComplete);

  /// <summary>Получить состояние задачи</summary>
  /// <param name="jobID">Уникальный идентификатор задачи</param>
  MRPTasksQueueState GetJobState(Guid jobID);

  /// <summary>
  /// Получить список действий, сформированный выполненной задачей
  /// </summary>
  /// <param name="actionsID">Уникальный идентификатор списка действий (возвращается в MRPCompositionTaskResult)</param>
  /// <returns>Список действий, сформированный выполненной задачей</returns>
  LinkedList<IMRPAction> GetActions(Guid actionsID);
}
