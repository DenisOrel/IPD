// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IVisualizerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces.Expert;
using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс новой серверной службы визуализатора</summary>
public interface IVisualizerService
{
  /// <param name="relFilter">Фильтрация связей (структурные, ассоциативные или все)</param>
  /// <param name="dict">Словарь с дополнительными параметрами</param>
  /// <param name="levelsOverride">Количество уровней разворота (перекрывает заданное в схеме)</param>
  /// <param name="previewMode">Режим показа превью: 0 - нет, 1 - только выбранные типы, 2 - все типы</param>
  /// <returns>ИД новой задачи (которая уже запущена!)</returns>
  long StartBuildChildTree(
    long projVId,
    long schemeId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    Guid userSession,
    HiddenCompositionFiltrationMode hcfm,
    RelFilter relFilter,
    HybridDictionary dict,
    int levelsOverride = -1,
    int previewMode = 1);

  /// <param name="relFilter">Фильтрация связей (структурные, ассоциативные или все)</param>
  /// <param name="dict">Словарь с дополнительными параметрами</param>
  /// <param name="levelsOverride">Количество уровней разворота (перекрывает заданное в схеме)</param>
  /// <param name="previewMode">Режим показа превью: 0 - нет, 1 - только выбранные типы, 2 - все типы</param>
  /// <returns>ИД новой задачи (которая уже запущена!)</returns>
  long StartBuildParentTree(
    long projVId,
    long projId,
    long schemeId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    HiddenCompositionFiltrationMode hcfm,
    Guid userSession,
    RelFilter relFilter,
    HybridDictionary dict,
    int levelsOverride = -1,
    int previewMode = 1);

  /// <summary>Стартовать задачу получения превью (дозагрузка)</summary>
  /// <param name="objIds">Список объектов, для которых надо получить превью</param>
  /// <param name="userSession">Guid сессии</param>
  /// <param name="schemeId">ИД схемы сбора данных (для получения типов) или -1, если нужны превью для всех типов</param>
  /// <returns>ИД новой задачи (которая уже запущена!)</returns>
  long StartCollectPreviews(long[] objIds, Guid userSession, long schemeId = 0);

  /// <summary>Получить статус задачи</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <returns>Состояние указанной задачи</returns>
  RelVisState GetTaskStatus(long taskId);

  /// <summary>Прекратить выполнение указанной задачи</summary>
  /// <param name="taskId">ИД задачи</param>
  void KillTask(long taskId);

  /// <summary>
  /// Получение результата задачи. Должно вызываться ТОЛЬКО после завершения задачи
  /// </summary>
  /// <param name="taskId">ИД задачи</param>
  /// <returns>Таблица с данными результата</returns>
  HybridTableExp GetTaskResult(long taskId);

  /// <summary>Получить Exception указанной задачи(может быть null)</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <returns>Exception или null</returns>
  Exception GetError(long taskId);
}
