// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IRelVisObserverService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс серверной службы Визуализатора связей</summary>
public interface IRelVisObserverService
{
  /// <summary>
  /// Получить ВСЕ связи дочернего дерева (связи сгруппированы по слоям)
  /// </summary>
  /// <param name="projID">идентификатор версии объекта вершины дерева</param>
  /// <param name="filtrationOwnerId">гуид фильтрации состава</param>
  /// <param name="rule"></param>
  /// <param name="objType">идентийфикатор типа объекта</param>
  /// <param name="userSession">Guid сессии</param>
  /// <returns></returns>
  DataTable[] GetChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict);

  /// <summary>
  /// Получить ВСЕ связи дочернего дерева (связи сгруппированы по слоям)
  /// </summary>
  /// <param name="projID">идентификатор версии объекта вершины дерева</param>
  /// <param name="filtrationOwnerId">гуид фильтрации состава</param>
  /// <param name="rule"></param>
  /// <param name="objType">идентийфикатор типа объекта</param>
  /// <param name="userSession">Guid сессии</param>
  /// <param name="levels">Количество получаемых уровней</param>
  /// <returns></returns>
  DataTable[] GetChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    int levels,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict);

  /// <summary>
  /// Получить ВСЕ связи родительского дерева (связи сгруппированы по слоям)
  /// </summary>
  /// <param name="projVID">идентификатор версии объекта вершины дерева</param>
  /// <param name="projId">идентификатор объекта вершины дерева</param>
  /// <param name="filtrationOwnerId">гуид фильтрации состава</param>
  /// <param name="rule"></param>
  /// <param name="objType">идентийфикатор типа объекта</param>
  /// <param name="userSession">Guid сессии</param>
  /// <returns></returns>
  DataTable[] GetParentTree(
    long projVID,
    long projId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    HybridDictionary dict);

  /// <summary>Стартовать задачу получения дочернего дерева</summary>
  /// <param name="projID">идентификатор версии объекта вершины дерева</param>
  /// <param name="filtrationOwnerId">гуид фильтрации состава</param>
  /// <param name="rule"></param>
  /// <param name="objType">идентийфикатор типа объекта</param>
  /// <param name="userSession">Guid сессии</param>
  /// <param name="levels">Количество получаемых уровней (-1 если нужны все)</param>
  /// <returns>ИД задачи</returns>
  long StartBuildChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    int levels,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict);

  /// <summary>Стартовать задачу получения родительского дерева</summary>
  /// <param name="projVID">идентификатор версии объекта вершины дерева</param>
  /// <param name="projId">идентификатор объекта вершины дерева</param>
  /// <param name="filtrationOwnerId">гуид фильтрации состава</param>
  /// <param name="rule"></param>
  /// <param name="objType">идентийфикатор типа объекта</param>
  /// <param name="userSession">Guid сессии</param>
  /// <returns>ИД задачи</returns>
  long StartBuildParentTree(
    long projVID,
    long projId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    HybridDictionary dict);

  /// <summary>Получить статус</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <returns>Состояние указанной задачи</returns>
  RelVisState GetTaskStatus(long taskId);

  /// <summary>Прекратить выполнение указанной задачи</summary>
  /// <param name="taskId">ИД задачи</param>
  void KillTask(long taskId);

  /// <summary>Получить результат выполнения задачи</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <returns>Набор таблиц или null, если результата нет</returns>
  DataTable[] GetTaskResult(long taskId);
}
