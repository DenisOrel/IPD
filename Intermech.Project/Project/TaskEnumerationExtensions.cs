// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskEnumerationExtensions
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project;

public static class TaskEnumerationExtensions
{
  /// <summary>Восстанавливает связи между взаимосвязями в ограниченной последовательности задач.
  /// Используется например после Paste группы задач, восстанавливает связи в этой ограниченной группе задач</summary>
  /// <param name="tasks">Перечисление задач</param>
  /// <remarks>Код основывается на коде из Project.ResolveDependencies()</remarks>
  public static void ResolveDependencies([NotNull, ItemNotNull] this IEnumerable<Task> tasks)
  {
    if (!(tasks is ICollection<Task> tasks1))
      tasks1 = (ICollection<Task>) tasks.ToList<Task>();
    ICollection<Task> tasks2 = tasks1;
    Dictionary<long, Task> dictionary1 = (Dictionary<long, Task>) null;
    foreach (Task task1 in (IEnumerable<Task>) tasks2)
    {
      bool flag = false;
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.Dependencies)
      {
        if (!dependency.Resolved || dependency.External)
        {
          Dictionary<long, Task> dictionary2 = dictionary1;
          if (dictionary2 == null)
          {
            ICollection<Task> source = tasks2;
            dictionary2 = dictionary1 = source.ToDictionary<Task, long>((Func<Task, long>) (t => t.ObjectID));
          }
          Task task2;
          if (dictionary2.TryGetValue(dependency.DependentOfTaskObjectID, out task2))
          {
            dependency.DependentOfTask = task2;
            flag = true;
          }
        }
      }
      if (flag)
      {
        task1.Dependencies._Modified = true;
        task1.Modified = true;
      }
    }
  }
}
