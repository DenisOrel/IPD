// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IProject
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Workflow;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Project;

/// <summary>Интерфейс проекта IMProject</summary>
[DBObjectTypeHandler("cad00e91-306c-11d8-b4e9-00304f19f545", true)]
public interface IProject : 
  IDBProjectTask,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBLifecycleLevel,
  IDBSecurityCollection,
  IDBSecurity,
  IRuntimeFlags
{
  /// <summary>Только для внутреннего использования! </summary>
  void CheckOutChildren();

  /// <summary>Только для внутреннего использования! </summary>
  void CheckInChildren();

  void Sync();

  void Execute();

  /// <summary>Статус удаленного процесса (если проект передавался через портал через команду синхронизации)</summary>
  RemoteProcessStatus RemoteStatus { get; set; }

  /// <summary>Получить состав проекта (задачи)</summary>
  /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
  /// <param name="conditions">Условия для запроса</param>
  /// <param name="recursiveSubProjects">(по умолчанию false) Запрашивать ли рекурсивно задачи, входящие в подпроекты</param>
  [CanBeNull]
  DataTable GetProjectTasks(
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool recursiveSubProjects = false);
}
