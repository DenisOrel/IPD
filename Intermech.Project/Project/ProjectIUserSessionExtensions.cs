// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectIUserSessionExtensions
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

public static class ProjectIUserSessionExtensions
{
  /// <summary>Получить тип объектов "Проекты IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObjectType GetProjectType([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectType((int) (IpsMetadataEntityBase<int>) ObjectTypes.Project, true);
  }

  /// <summary>Получить тип объектов "Задачи IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObjectType GetTaskType([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectType((int) (IpsMetadataEntityBase<int>) ObjectTypes.Task, true);
  }

  /// <summary>Получить тип объектов "Сообщения IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObjectType GetProjectMessageType([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectType((int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage, true);
  }

  /// <summary>Получить тип объектов "Зависимости IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObjectType GetDependencyType([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectType((int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency, true);
  }

  /// <summary>Получить тип связей "Входимость задачи IMProject в проект IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBRelationType GetTaskInProjectRelationType([NotNull] this IUserSession userSession)
  {
    return userSession.GetRelationType((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition, true);
  }

  /// <summary>Получить интерфейс фабрики проектов IMProject</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectCollection GetProjectCollection([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectsCollection<IDBProjectCollection>((int) (IpsMetadataEntityBase<int>) ObjectTypes.Project);
  }

  /// <summary>Получить интерфейс фабрики задач IMProject</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectTaskCollection GetTaskCollection([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectsCollection<IDBProjectTaskCollection>((int) (IpsMetadataEntityBase<int>) ObjectTypes.Task);
  }

  /// <summary>Получить интерфейс фабрики задач IMProject</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectTaskCollection GetTaskCollection(
    [NotNull] this IUserSession userSession,
    [NotEmpty] int taskSubTypeID)
  {
    if (taskSubTypeID == (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task)
      return userSession.GetObjectsCollection<IDBProjectTaskCollection>((int) (IpsMetadataEntityBase<int>) ObjectTypes.Task);
    Helper.CheckTypeIsTask(taskSubTypeID);
    return userSession.GetObjectsCollection<IDBProjectTaskCollection>(taskSubTypeID);
  }

  /// <summary>Получить интерфейс фабрики почтовых сообщений IMProject</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectMessageCollection GetProjectMessageCollection(
    [NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectsCollection<IDBProjectMessageCollection>((int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage);
  }

  /// <summary>Получить интерфейс фабрики почтовых сообщений IMProject</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBDependencyCollection GetDependencyCollection([NotNull] this IUserSession userSession)
  {
    return userSession.GetObjectsCollection<IDBDependencyCollection>((int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency);
  }

  /// <summary>Получить интерфейс фабрики связей "Входимость задачи IMProject в проект IMProject"</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBRelationCollectionTaskInProject GetRelationCollectionTaskInProject(
    [NotNull] this IUserSession userSession,
    [CanBeNull, CanBeEmpty] string filtrationOwnerID = null)
  {
    return userSession.GetRelationCollection<IDBRelationCollectionTaskInProject>((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition, filtrationOwnerID);
  }

  /// <summary>Получить интерфейс проекта IMProject по идентификатору его версии</summary>
  /// <exception cref="T:Intermech.Project.ProjectNotFoundException">Если версия объекта с переданным идентификатором не найдена в БД
  /// или данный объект не проект IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IProject GetProject(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long projectVersionID,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IProject, ProjectNotFoundException>(projectVersionID, failIfNotFound);
  }

  /// <summary>Получить интерфейс проекта IMProject по Guid-у его версии</summary>
  /// <exception cref="T:Intermech.Project.ProjectNotFoundException">Если версия объекта с переданным Guid-ом не найдена в БД
  /// или данный объект не проект IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IProject GetProject(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid projectVersionGuid,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IProject, ProjectNotFoundException>(projectVersionGuid, failIfNotFound);
  }

  /// <summary>Попытаться получить интерфейс проекта IMProject по идентификатору его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetProject(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long projectVersionID,
    out IProject result)
  {
    return userSession.TryGetObject<IProject>(projectVersionID, out result);
  }

  /// <summary>Попытаться получить интерфейс проекта IMProject по Guid-у его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetProject(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid projectVersionGuid,
    out IProject result)
  {
    return userSession.TryGetObject<IProject>(projectVersionGuid, out result);
  }

  /// <summary>Получить интерфейс задачи IMProject по идентификатору его версии</summary>
  /// <exception cref="T:Intermech.Project.TaskNotFoundException">Если версия объекта с переданным идентификатором не найдена в БД
  /// или данный объект не задача IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectTask GetTask(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long taskVersionID,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBProjectTask, TaskNotFoundException>(taskVersionID, failIfNotFound);
  }

  /// <summary>Получить интерфейс задачи IMProject по Guid-у его версии</summary>
  /// <exception cref="T:Intermech.Project.TaskNotFoundException">Если версия объекта с переданным Guid-ом не найдена в БД
  /// или данный объект не задача IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectTask GetTask(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid taskVersionGuid,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBProjectTask, TaskNotFoundException>(taskVersionGuid, failIfNotFound);
  }

  /// <summary>Попытаться получить интерфейс задачи IMProject по идентификатору его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetTask(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long taskVersionID,
    out IDBProjectTask result)
  {
    return userSession.TryGetObject<IDBProjectTask>(taskVersionID, out result);
  }

  /// <summary>Попытаться получить интерфейс задачи IMProject по Guid-у его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetTask(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid taskVersionGuid,
    out IDBProjectTask result)
  {
    return userSession.TryGetObject<IDBProjectTask>(taskVersionGuid, out result);
  }

  /// <summary>Получить интерфейс сообщения IMProject по идентификатору его версии</summary>
  /// <exception cref="T:Intermech.Project.ProjectMessageNotFoundException">Если версия объекта с переданным идентификатором не найдена в БД
  /// или данный объект не сообщение IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectMessage GetProjectMessage(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long projectMessageVersionID,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBProjectMessage, ProjectMessageNotFoundException>(projectMessageVersionID, failIfNotFound);
  }

  /// <summary>Получить интерфейс сообщения IMProject по Guid-у его версии</summary>
  /// <exception cref="!:ProjectMessageWithVersionGuidNotFoundException">Если версия объекта с переданным Guid-ом не найдена в БД
  /// или данный объект не сообщение IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectMessage GetProjectMessage(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid projectMessageVersionGuid,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBProjectMessage, TaskNotFoundException>(projectMessageVersionGuid, failIfNotFound);
  }

  /// <summary>Попытаться получить интерфейс сообщения IMProject по идентификатору его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetProjectMessage(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long projectMessageVersionID,
    out IDBProjectMessage result)
  {
    return userSession.TryGetObject<IDBProjectMessage>(projectMessageVersionID, out result);
  }

  /// <summary>Попытаться получить интерфейс сообщения IMProject по Guid-у его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetProjectMessage(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid projectMessageVersionGuid,
    out IDBProjectMessage result)
  {
    return userSession.TryGetObject<IDBProjectMessage>(projectMessageVersionGuid, out result);
  }

  /// <summary>Получить интерфейс зависимости IMProject по идентификатору его версии</summary>
  /// <exception cref="T:Intermech.Project.DependencyNotFoundException">Если версия объекта с переданным идентификатором не найдена в БД
  /// или данный объект не зависимость IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBDependency GetDependency(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long dependencyVersionID,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBDependency, DependencyNotFoundException>(dependencyVersionID, failIfNotFound);
  }

  /// <summary>Получить интерфейс зависимости IMProject по Guid-у его версии</summary>
  /// <exception cref="T:Intermech.Project.DependencyNotFoundException">Если версия объекта с переданным Guid-ом не найдена в БД
  /// или данный объект не зависимость IMProject</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBDependency GetDependency(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid dependencyVersionGuid,
    bool failIfNotFound = true)
  {
    return userSession.GetObject<IDBDependency, DependencyNotFoundException>(dependencyVersionGuid, failIfNotFound);
  }

  /// <summary>Попытаться получить интерфейс зависимости IMProject по идентификатору его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetDependency(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long dependencyVersionID,
    out IDBDependency result)
  {
    return userSession.TryGetObject<IDBDependency>(dependencyVersionID, out result);
  }

  /// <summary>Попытаться получить интерфейс зависимости IMProject по Guid-у его версии</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetDependency(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid dependencyVersionGuid,
    out IDBDependency result)
  {
    return userSession.TryGetObject<IDBDependency>(dependencyVersionGuid, out result);
  }

  /// <summary>Получить интерфейс связи "входимость задачи IMProject в проект IMProject" по её идентификатору</summary>
  /// <exception cref="T:Intermech.Project.RelationTaskInProjectNotFoundException">Если связь с переданным идентификатором не найдена в БД
  /// или данный эта связь другого типа</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBRelationTaskInProject GetTaskInProjectRelation(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long relationID,
    bool failIfNotFound = true)
  {
    return userSession.GetRelation<IDBRelationTaskInProject, RelationTaskInProjectNotFoundException>(relationID, failIfNotFound);
  }

  /// <summary>Получить интерфейс связи "входимость задачи IMProject в проект IMProject" по её Guid-у</summary>
  /// <exception cref="T:Intermech.Project.RelationTaskInProjectNotFoundException">Если связь с переданным Guid-ом не найдена в БД
  /// или данный эта связь другого типа</exception>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBRelationTaskInProject GetTaskInProjectRelation(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid relationGuid,
    bool failIfNotFound = true)
  {
    return userSession.GetRelation<IDBRelationTaskInProject, RelationTaskInProjectNotFoundException>(relationGuid, failIfNotFound);
  }

  /// <summary>Попытаться получить интерфейс связи "входимость задачи IMProject в проект IMProject" по её идентификатору</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetTaskInProjectRelation(
    [NotNull] this IUserSession userSession,
    [NotEmpty] long relationID,
    out IDBRelationTaskInProject result)
  {
    return userSession.TryGetRelation<IDBRelationTaskInProject>(relationID, out result);
  }

  /// <summary>Попытаться получить интерфейс связи "входимость задачи IMProject в проект IMProject" по её Guid-у</summary>
  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetTaskInProjectRelation(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid relationGuid,
    out IDBRelationTaskInProject result)
  {
    return userSession.TryGetRelation<IDBRelationTaskInProject>(relationGuid, out result);
  }

  /// <summary>Получить состав проекта (задачи)</summary>
  /// <param name="session">usrSession - пользовательская сессия</param>
  /// <param name="projectVersionID">ID версии проекта</param>
  /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
  /// <param name="conditions">Условия для запроса</param>
  /// <param name="recursiveSubProjects">(по умолчанию false) Запрашивать ли рекурсивно задачи, входящие в подпроекты</param>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetProjectTasks(
    [NotNull] this IUserSession session,
    [NotEmpty] long projectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool recursiveSubProjects = false)
  {
    return session.GetObjectComposition(projectVersionID, (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project, columns, searchRelationTypes: (IReadOnlyCollection<int>) new int[1]
    {
      (int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition
    }, searchObjectTypes: (IReadOnlyCollection<int>) Helper.TasksTypeIDsArray, expandObjectTypes: (IReadOnlyCollection<int>) (recursiveSubProjects ? Helper.TasksTypeIDsArray : Helper.TasksNotProjectTypeIDsArray));
  }
}
