// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBProjectTask
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Project;

/// <summary>Интерфейс задачи IMProject</summary>
[DBObjectTypeHandler("cad00e92-306c-11d8-b4e9-00304f19f545", true)]
public interface IDBProjectTask : 
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBLifecycleLevel,
  IDBSecurityCollection,
  IDBSecurity,
  IRuntimeFlags
{
  /// <summary>Идентификатор проекта, в который входит задача</summary>
  new long ProjectID { get; }

  /// <summary>Получить интерфейс проекта, в который входит данная задача</summary>
  [CanBeNull]
  IProject GetDbProject();

  /// <summary>Интерфейс задачи в которую вложена данная</summary>
  [CanBeNull]
  IDBProjectTask ParentTask { get; }

  /// <summary>Статус задачи</summary>
  TaskStatus Status { get; }

  /// <summary>Плановое начало или null (если не задано)</summary>
  DateTime? PlanStartDateTime { get; }

  /// <summary>Плановое окончание рассчитанное как PlanStartDateTime + PlanDuration</summary>
  DateTime? PlanFinishDateTime { get; }

  /// <summary>Плановая продолжительность или null (если не задано)</summary>
  [CanBeNull]
  MeasuredValue PlanDuration { get; }

  /// <summary>Срок выполнения</summary>
  DateTime? DueDateID { get; }

  /// <summary>Описание задачи</summary>
  [NotNull]
  string Description { get; }

  /// <summary>Ответ руководителя отправившего задание с этапа проверки обратно исполнителям</summary>
  [CanBeNull]
  [NotWhitespace]
  string ManagerAnswer { get; set; }

  /// <summary>Ресурсы назначенные задаче</summary>
  [NotNull]
  IReadOnlyList<(long PrjLinkID, long ObjectID, int ObjectTypeID, string Caption, double Units, bool IsChief, long CalendarID)> Assignments { get; }

  /// <summary>Идентификатор руководителя задачи</summary>
  [CanBeEmpty]
  long ChiefID { get; }

  /// <summary>
  /// Информация по сообщениям, отправленным по задаче, содержит идентификаторы объекта и версии объекта сообщений,
  /// идентификатор получателя, начато, завершено, статус (см. <see cref="T:Intermech.Workflow.ActivityStatus" />)
  /// </summary>
  [CanBeNull]
  DataTable Messages { get; }

  /// <summary>Список исполнителей</summary>
  [NotNull]
  ParcipiantInfo[] Parcipiants { get; }
}
