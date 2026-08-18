// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBRelationCollectionTaskInProject
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Project;

/// <summary>Интерфейс фабрики связей "Входимость задачи IMProject в проект IMProject"</summary>
[DBRelationTypeHandler("cad00e93-306c-11d8-b4e9-00304f19f545")]
public interface IDBRelationCollectionTaskInProject : 
  IDBRelationCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection
{
  /// <summary>Создает связь между объектами projectID (ид. версии проекта) и taskID (ид. версии
  /// задачи), которая начнет действовать с даты beginDate (в локальном времени)</summary>
  [NotNull]
  IDBRelationTaskInProject Create([NotEmpty] long projectID, [NotEmpty] long taskID, [CanBeEmpty] DateTime beginDate);

  /// <summary>То же, но связь начнет действовать с момента ее создания</summary>
  [NotNull]
  IDBRelationTaskInProject Create([NotEmpty] long projectID, [NotEmpty] long taskID, [CanBeNull] AttributeValues[] vals = null);

  /// <summary>Создает связь между объектами properties.ProjectObjectID (ид. версии проекта) и
  /// properties.PartID (ид. версии задачи), которая начнет действовать с даты properties.BeginDate (в
  /// локальном времени). Если properties.BeginDate == DateTime.MinValue, то связь начинает действовать с даты ее
  /// создания. Если properties.EndDate == DateTime.MaxValue, то время действия связи не ограничено. Тип связи задается
  /// при получении объекта IDBRelationCollection (если при создании тип связи меньше 0, то функция Create работать не
  /// будет). Если properties.PrototypeRelationID &gt; 0, то связь инициализируется атрибутами от связи прототипа с номером
  /// properties.PrototypeRelationID</summary>
  [NotNull]
  IDBRelationTaskInProject Create([NotEmpty] in NewRelationProperties properties);

  /// <summary>Получить состав проекта (задачи)</summary>
  /// <param name="projectVersionID">ID версии проекта</param>
  /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
  /// <param name="conditions">Условия для запроса</param>
  /// <param name="recursiveSubProjects">(по умолчанию false) Запрашивать ли рекурсивно задачи, входящие в подпроекты</param>
  [CanBeNull]
  DataTable GetProjectTasks(
    [NotEmpty] long projectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool recursiveSubProjects = false);
}
