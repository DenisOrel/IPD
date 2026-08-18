// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskFilters
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Project.Evaluator;
using Intermech.Project.Properties;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Project;

public class TaskFilters : List<TaskFilter>
{
  [CanBeNull]
  private static TaskFilters _all;
  private const string SectionName = "ImProject.Filters";
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();
  public bool Modified;

  [NotNull]
  public static TaskFilters All
  {
    get
    {
      if (TaskFilters._all == null)
      {
        TaskFilters._all = new TaskFilters();
        TaskFilters.AddStandardFilters((List<TaskFilter>) TaskFilters._all);
      }
      return TaskFilters._all;
    }
  }

  private static void _add([NotNull] TaskFilter tf, [NotNull] List<TaskFilter> list, FilterFlags flags = FilterFlags.ShowInMenu | FilterFlags.Global, bool replace = false)
  {
    int index = replace ? list.FindIndex((Predicate<TaskFilter>) (f => tf.Name == f.Name)) : -1;
    if (index != -1)
      list[index] = tf;
    else
      list.Add(tf);
    tf.Flags = flags;
  }

  public static void AddStandardFilters([NotNull, ItemNotNull] List<TaskFilter> list)
  {
    TaskFilters._add(new TaskFilter(TaskFilter.AllTasksFilterName), list);
    TaskFilters._add(new TaskFilter(Resources.FilterSummary, new Expression("HasSubTasks", "==", (object) true)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterMilestones, new Expression("Milestone", "==", (object) true)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterExecuted, new Expression("IsExecuted", "==", (object) true)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterCompleted, new Expression("Status", "==", (object) TaskStatus.Completed)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterOverdueTasks, new ExpressionList((IEnumerable<Expression>) new Expression[2]
    {
      new Expression("Finish", "<", (object) "@DateTime.Now"),
      new Expression("Status", "!=", (object) TaskStatus.Completed)
    })), list);
    TaskFilters._add(new TaskFilter(Resources.FilterWithSrcData, new Expression("SrcData.Count", ">", (object) 0)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterWithResults, new Expression("Results.Count", ">", (object) 0)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterWithConstraints, new Expression("ConstraintDate", "!=", (object) "@DateTime.MinValue")), list);
    TaskFilters._add(new TaskFilter(Resources.FilterEstimation, new Expression("Estimation", "==", (object) true)), list);
    TaskFilters._add(new TaskFilter(Resources.FilterResource, new Expression("Assignments", "in", (object) $"\"{Resources.FilterResourceText}\"?")), list);
    TaskFilters._add(new TaskFilter(Resources.FilterCritical, new Expression("IsCritical", "==", (object) true)), list);
  }

  public void Load([NotNull] XmlIni ini)
  {
    this.Clear();
    long num = ini.ReadInteger(string.Empty, "Count");
    for (int index = 1; (long) index <= num; ++index)
    {
      TaskFilter tf = new TaskFilter();
      tf.Load(index, ini);
      this.Add(tf);
    }
  }

  public static void Init([CanBeNull] IUserSession session)
  {
    TaskFilters._initOnce.Invoke(ref session, (Action) (() =>
    {
      IDBConfigurations configurations = session.Configurations;
      byte[] config_file = Array.Empty<byte>();
      try
      {
        configurations.LoadConfigData("ImProject.Filters", out BlobInformation _, out config_file, 0L);
      }
      catch
      {
      }
      if (config_file.Length == 0)
        return;
      using (MemoryStream memoryStream = new MemoryStream(config_file))
      {
        memoryStream.Position = 0L;
        XmlIni ini = new XmlIni();
        ini.Load((Stream) memoryStream);
        if (TaskFilters._all == null)
          TaskFilters._all = new TaskFilters();
        TaskFilters._all.Load(ini);
      }
    }));
  }

  public void Save([NotNull] XmlIni ini, [NotNull] Predicate<TaskFilter> match)
  {
    int index = 1;
    foreach (TaskFilter taskFilter in (List<TaskFilter>) this)
    {
      if (match(taskFilter))
      {
        taskFilter.Save(index, ini);
        ++index;
      }
    }
    ini.WriteInteger(string.Empty, "Count", (long) (index - 1));
  }

  public static void Save([NotNull] IUserSession session)
  {
    if (TaskFilters._all == null)
      return;
    IDBConfigurations configurations = session.Configurations;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      XmlIni ini = new XmlIni();
      TaskFilters.All.Save(ini, (Predicate<TaskFilter>) (tf => tf.HasFlag(FilterFlags.Global)));
      ini.Save((Stream) memoryStream);
      BlobInformation config_info = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "ImProject.Filters", ArcMethods.NotPacked, string.Empty);
      configurations.WriteConfigData(config_info, memoryStream.ToArray(), 0L);
      memoryStream.Close();
    }
  }

  [NotNull]
  public List<TaskFilter> Select(FilterFlags condFlags)
  {
    return this.FindAll((Predicate<TaskFilter>) (tf => tf.HasFlag(condFlags)));
  }

  [NotNull]
  public List<TaskFilter> Select(bool selectPaintFilters)
  {
    return this.FindAll((Predicate<TaskFilter>) (tf => selectPaintFilters == tf.IsPaintFilter));
  }

  public override int GetHashCode()
  {
    int count = this.Count;
    foreach (TaskFilter taskFilter in (List<TaskFilter>) this)
    {
      count *= 17;
      if (taskFilter != null)
        count += taskFilter.GetHashCode();
    }
    return count;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj || !(obj is List<TaskFilter> taskFilterList))
      return true;
    if (this.Count != taskFilterList.Count)
      return false;
    for (int index = 0; index < this.Count; ++index)
    {
      if (!this[index].Equals((object) taskFilterList[index]))
        return false;
    }
    return true;
  }

  public new void Add([NotNull] TaskFilter tf)
  {
    base.Add(tf);
    this.Modified = true;
  }

  public new void AddRange([NotNull] IEnumerable<TaskFilter> filters)
  {
    using (IEnumerator<TaskFilter> enumerator = filters.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      base.Add(enumerator.Current);
      this.Modified = true;
      while (enumerator.MoveNext())
        base.Add(enumerator.Current);
    }
  }

  public void Remove([NotNull] TaskFilter tf)
  {
    base.Remove(tf);
    this.Modified = true;
  }

  public void RemoveRange([NotNull] IEnumerable<TaskFilter> filters)
  {
    using (IEnumerator<TaskFilter> enumerator = filters.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      base.Remove(enumerator.Current);
      this.Modified = true;
      while (enumerator.MoveNext())
        base.Remove(enumerator.Current);
    }
  }

  public new void Clear() => base.Clear();
}
