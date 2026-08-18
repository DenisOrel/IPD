// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Helper
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Статический класс-хелпер для IPS.Project</summary>
public abstract class Helper : Intermech.Extensions.Interfaces.Helper
{
  public new const double DoubleEqualityTolerance = 1E-09;
  public static bool ProjectsIsLocalType = true;
  [CanBeNull]
  private static HashSet<int> _projectsTypeIDs;
  [CanBeNull]
  private static HashSet<int> _projectsChildTypeIDs;
  [CanBeNull]
  private static HashSet<int> _tasksTypeIDs;
  private static int[] _tasksTypeIDsArray;
  private static int[] _tasksNotProjectTypeIDsArray;
  [CanBeNull]
  private static HashSet<int> _dependenceTypeIDs;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsProject([NotEmpty] int objectTypeID)
  {
    return objectTypeID == ObjectTypes.Project.ID || Helper.ProjectsChildTypeIDs.Contains(objectTypeID);
  }

  /// <exception cref="T:System.InvalidOperationException">Если переданный тип не является задачей IMProject</exception>
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CheckTypeIsProject([NotEmpty] int objectTypeID)
  {
    if (Helper.IsProject(objectTypeID))
      return;
    IMSObjectType objectType = MetaDataHelperService.Instance.GetObjectType(objectTypeID);
    if (objectType == null)
      throw new ObjectTypeNotFoundException(objectTypeID);
    throw new InvalidOperationException($"Тип объектов \"{objectType.ObjectTypeName}\" не является типом проекта IMProject!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsTask([NotEmpty] int objectTypeID)
  {
    return objectTypeID == ObjectTypes.Task.ID || objectTypeID == ObjectTypes.Project.ID || Helper.TasksTypeIDs.Contains(objectTypeID);
  }

  /// <exception cref="T:System.InvalidOperationException">Если переданный тип не является задачей IMProject</exception>
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CheckTypeIsTask([NotEmpty] int objectTypeID)
  {
    if (Helper.IsTask(objectTypeID))
      return;
    IMSObjectType objectType = MetaDataHelperService.Instance.GetObjectType(objectTypeID);
    if (objectType == null)
      throw new ObjectTypeNotFoundException(objectTypeID);
    throw new InvalidOperationException($"Тип объектов \"{objectType.ObjectTypeName}\" не является типом задачи IMProject!");
  }

  /// <summary>Идентификаторы типов проектов, включая идентификаторы дочерних от "Проект IMProject" типов</summary>
  [NotNull]
  public static HashSet<int> ProjectsTypeIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._projectsTypeIDs != null)
        return Helper._projectsTypeIDs;
      List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Project.ID);
      Helper._projectsTypeIDs = new HashSet<int>(childrenIdRecursive.Count);
      Helper._projectsTypeIDs.AddRange<int>((IEnumerable<int>) childrenIdRecursive);
      return Helper._projectsTypeIDs;
    }
  }

  /// <summary>Идентификаторы типов объектов, дочерних от "Проект IMProject" типов</summary>
  [NotNull]
  public static HashSet<int> ProjectsChildTypeIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._projectsChildTypeIDs != null)
        return Helper._projectsChildTypeIDs;
      List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Project.ID);
      Helper._projectsChildTypeIDs = new HashSet<int>(childrenIdRecursive.Count);
      Helper._projectsChildTypeIDs.AddRange<int>((IEnumerable<int>) childrenIdRecursive);
      return Helper._projectsChildTypeIDs;
    }
  }

  /// <summary>Идентификаторы типов задач, включая идентификаторы дочерних от "Задача IMProject" типов</summary>
  [NotNull]
  public static HashSet<int> TasksTypeIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._tasksTypeIDs == null)
      {
        List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Task.ID);
        Helper._tasksTypeIDs = new HashSet<int>(childrenIdRecursive.Count);
        Helper._tasksTypeIDs.AddRange<int>((IEnumerable<int>) childrenIdRecursive);
      }
      return Helper._tasksTypeIDs;
    }
  }

  /// <summary>Массив идентификаторов типов задач, включая идентификаторы дочерних от "Задача IMProject" и "Проект IMProject" типов</summary>
  [NotNull]
  public static int[] TasksTypeIDsArray
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._tasksTypeIDsArray == null)
      {
        List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Task.ID);
        Helper._tasksTypeIDsArray = new int[childrenIdRecursive.Count];
        childrenIdRecursive.CopyTo(Helper._tasksTypeIDsArray, 0);
      }
      return Helper._tasksTypeIDsArray;
    }
  }

  /// <summary>Массив идентификаторов типов задач, ИСКЛЮЧАЯ "Проект IMProject" и дочерних от него типов</summary>
  [NotNull]
  public static int[] TasksNotProjectTypeIDsArray
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._tasksNotProjectTypeIDsArray == null)
      {
        List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Task.ID);
        for (int index = childrenIdRecursive.Count - 1; index >= 0; --index)
        {
          if (Helper.IsProject(childrenIdRecursive[index]))
            childrenIdRecursive.RemoveAt(index);
        }
        Helper._tasksNotProjectTypeIDsArray = new int[childrenIdRecursive.Count];
        childrenIdRecursive.CopyTo(Helper._tasksNotProjectTypeIDsArray, 0);
      }
      return Helper._tasksNotProjectTypeIDsArray;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsDependence([NotEmpty] int objectTypeID)
  {
    return objectTypeID == ObjectTypes.Dependency.ID || Helper.DependenceTypeIDs.Contains(objectTypeID);
  }

  /// <exception cref="T:System.InvalidOperationException">Если переданный тип не является взаимосвязью проекта IMProject</exception>
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CheckTypeIsDependence([NotEmpty] int objectTypeID)
  {
    if (Helper.IsDependence(objectTypeID))
      return;
    IMSObjectType objectType = MetaDataHelperService.Instance.GetObjectType(objectTypeID);
    if (objectType == null)
      throw new ObjectTypeNotFoundException(objectTypeID);
    throw new InvalidOperationException($"Тип объектов \"{objectType.ObjectTypeName}\" не является типом взаимосвязи IMProject!");
  }

  /// <summary>Идентификаторы типов взаимосвязей, включая идентификаторы дочерних от "Взаимосвязь IMProject" типов</summary>
  [NotNull]
  public static HashSet<int> DependenceTypeIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (Helper._dependenceTypeIDs != null)
        return Helper._dependenceTypeIDs;
      List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(ObjectTypes.Dependency.ID);
      Helper._dependenceTypeIDs = new HashSet<int>(childrenIdRecursive.Count);
      Helper._dependenceTypeIDs.AddRange<int>((IEnumerable<int>) childrenIdRecursive);
      return Helper._dependenceTypeIDs;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int TaskStatusToLCStep([NotEmpty] TaskStatus status)
  {
    return LCStepVsTaskStatusConnection.TaskStatusToLCStep(status);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TaskStatus LCStepToTaskStatus([NotEmpty] int lcStep)
  {
    return LCStepVsTaskStatusConnection.LCStepToTaskStatus(lcStep);
  }
}
