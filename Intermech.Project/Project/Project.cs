// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Project
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Project : Task
{
  [NotNull]
  protected TaskCollection _Tasks = new TaskCollection(true);
  [NotNull]
  public ProjectProperties _Properties;
  [CanBeNull]
  [NonSerialized]
  private List<Task> _deletedTasks;
  [CanBeNull]
  [ItemNotNull]
  [NonSerialized]
  private List<Dependency> _deletedDependencies;
  private DateTime _prevStart;
  private DateTime _prevFinish;
  [CanBeNull]
  [NonSerialized]
  internal BulkData _BulkData;
  [CanBeNull]
  [NonSerialized]
  private XmlIni _projectData;
  internal bool _ModifiedWhileLoading;
  private int _updateCounter;
  private bool _showProjectTask;
  private PlanningType _planningType;
  [NotNull]
  private static readonly string[] _affectProcFinish = new string[4]
  {
    nameof (Work),
    nameof (Start),
    "Dependencies",
    nameof (Finish)
  };
  [NotNull]
  private static readonly string[] _affectProcStart = new string[4]
  {
    nameof (Work),
    nameof (Start),
    "Dependencies",
    nameof (Finish)
  };
  [CanBeNull]
  [NonSerialized]
  private Task.Graph _projectGraph;
  [CanBeNull]
  [NotNullAfter("ImportFromMsProjectXml")]
  private static Dictionary<string, Dictionary<string, Task>> _xmlUIDs;
  [CanBeNull]
  [NotNullAfter("ImportFromMsProjectXml")]
  private static XmlNamespaceManager _namespaceManager;
  protected bool _AutoLoadSubTasks = true;
  [CanBeNull]
  [NonSerialized]
  public IProgressNotifier ProgressNotifier;
  [NotNull]
  [NonSerialized]
  private List<Exception> _exceptionLog = new List<Exception>();
  private bool? _checkOutPossible;
  private bool? _checkInPossible;
  private int _lockRefreshMail;
  [CanBeNull]
  private Dictionary<Task, int> _copiedRootIndentDXs;
  [CanBeNull]
  private Task _currentCopiedRoot;
  [CanBeNull]
  [NonSerialized]
  private TaskFilter _filter;
  /// <summary>
  /// Здесь хранятся текст ошибки вычисления текущего фильтра, если она была
  /// </summary>
  [NotNull]
  public string _FilterError = string.Empty;
  /// <summary>Список дескрипторов импортированных в проект объектов
  /// Список импортированных объектов показывается при вызове команды "синхронизация с составом объекта"</summary>
  [CanBeNull]
  [ItemNotNull]
  private MutableCollection<ImportedObject> _importedObjects;
  /// <summary>Перечисление задач, импортированных из определённого объекта</summary>
  [CanBeNull]
  [NonSerialized]
  private Intermech.Project.Project.TasksImportedFromObjectClass _tasksImportedFromObject;
  [CanBeNull]
  private string _pendingSiteID;

  [CanBeNull]
  public Intermech.Project.Project.ProjectCache _Cache
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Intermech.Project.Project.ProjectCache) base._Cache;
  }

  [NotNull]
  public Intermech.Project.Project.ProjectCache Cache
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Intermech.Project.Project.ProjectCache) base.Cache;
  }

  [NotNull]
  protected override Task.TaskCache CreateCache() => (Task.TaskCache) new Intermech.Project.Project.ProjectCache();

  public Project()
    : this(0L)
  {
  }

  public Project(DateTime start)
    : this()
  {
    this.Start = start;
  }

  public Project([NotNull] string name)
    : this()
  {
    this._Name = name;
  }

  public Project([NotNull] string name, DateTime start)
    : this(name)
  {
    this.Start = start;
  }

  public Project([NotEmpty] long objectID)
    : base(objectID)
  {
    this._Properties = new ProjectProperties(this);
  }

  public Project([NotEmpty] long objectID, bool autoLoadSubTasks, bool autoLoadSubProjects)
    : this(objectID)
  {
    this._AutoLoadSubTasks = autoLoadSubTasks;
    this.AutoLoadSubProjects = autoLoadSubProjects;
  }

  private bool ArrangeTasks(
    [NotNull, ItemNotNull] List<List<Task>> priorityHierarchy,
    [NotNull, ItemNotNull] List<Assignment> stepResourceAssignments,
    [NotNull, ItemNotNull] List<Task> activeTasks)
  {
    List<Intermech.Project.Project.AssignmentArrangementInformation> arrangementInformationList = new List<Intermech.Project.Project.AssignmentArrangementInformation>();
    Dictionary<Assignment, List<Assignment>> currentAssignments = new Dictionary<Assignment, List<Assignment>>();
    Dictionary<Assignment, double> currentAssignmentUnits = new Dictionary<Assignment, double>();
    foreach (Assignment resourceAssignment in stepResourceAssignments)
      currentAssignments[resourceAssignment] = new List<Assignment>();
    this.ArrangeTasks(arrangementInformationList, currentAssignments, currentAssignmentUnits, priorityHierarchy, stepResourceAssignments, activeTasks, 0, DateTime.Now);
    if (arrangementInformationList.Count <= 0)
      return false;
    double maxValue = arrangementInformationList.Select<Intermech.Project.Project.AssignmentArrangementInformation, double>((System.Func<Intermech.Project.Project.AssignmentArrangementInformation, double>) (information => information.Cost)).Append<double>(double.MaxValue).Min();
    foreach (Intermech.Project.Project.AssignmentArrangementInformation arrangementInformation in arrangementInformationList.Where<Intermech.Project.Project.AssignmentArrangementInformation>((System.Func<Intermech.Project.Project.AssignmentArrangementInformation, bool>) (information2 => information2.Cost > maxValue)).ToList<Intermech.Project.Project.AssignmentArrangementInformation>())
      arrangementInformationList.Remove(arrangementInformation);
    DateTime finish = DateTime.MaxValue;
    foreach (Intermech.Project.Project.AssignmentArrangementInformation arrangementInformation in arrangementInformationList)
    {
      if (arrangementInformation.Finish < finish)
        finish = arrangementInformation.Finish;
    }
    foreach (Intermech.Project.Project.AssignmentArrangementInformation arrangementInformation in arrangementInformationList.Where<Intermech.Project.Project.AssignmentArrangementInformation>((System.Func<Intermech.Project.Project.AssignmentArrangementInformation, bool>) (information5 => information5.Finish > finish)).ToList<Intermech.Project.Project.AssignmentArrangementInformation>())
      arrangementInformationList.Remove(arrangementInformation);
    if (arrangementInformationList.Count <= 0)
      return false;
    Intermech.Project.Project.AssignmentArrangementInformation arrangementInformation1 = arrangementInformationList[0];
    foreach (Assignment key in arrangementInformation1.SelectedAssignments.Keys)
    {
      int num = key.Task.Assignments.IndexOf(key);
      foreach (Assignment assignment in arrangementInformation1.SelectedAssignments[key])
        key.Task.Assignments.Insert(num++, assignment);
    }
    foreach (Assignment key in arrangementInformation1.SelectedAssignmentUnits.Keys)
      key.Units = arrangementInformation1.SelectedAssignmentUnits[key];
    foreach (Assignment key in arrangementInformation1.SelectedResources.Keys)
      key.Resource = arrangementInformation1.SelectedResources[key];
    foreach (Task key in arrangementInformation1.SelectedTaskStarts.Keys)
      key.Start = arrangementInformation1.SelectedTaskStarts[key];
    return true;
  }

  private bool ArrangeTasks(
    [NotNull, ItemNotNull] List<Intermech.Project.Project.ResourceArrangementInformation> l,
    [NotNull, ItemNotNull] List<List<Task>> priorityHierarchy,
    [NotNull, ItemNotNull] List<Assignment> unknownResourceAssignments,
    int pos,
    DateTime startTime)
  {
    if (DateTime.Now.Subtract(startTime).TotalSeconds >= IMProject.LevelingTimeoutSeconds)
      return false;
    if (pos >= unknownResourceAssignments.Count)
    {
      Dictionary<Assignment, Resource> selectedResources = new Dictionary<Assignment, Resource>();
      foreach (Assignment resourceAssignment in unknownResourceAssignments)
        selectedResources[resourceAssignment] = resourceAssignment.Resource;
      double cost;
      DateTime finish;
      Dictionary<Task, DateTime> selectedTaskStarts = this.ArrangeTaskStarts(priorityHierarchy, out cost, out finish);
      l.Add(new Intermech.Project.Project.ResourceArrangementInformation(selectedResources, cost, finish, selectedTaskStarts));
    }
    else
    {
      Assignment resourceAssignment1 = unknownResourceAssignments[pos];
      UnknownResource resource1 = resourceAssignment1.Resource as UnknownResource;
      List<Resource> source = new List<Resource>((IEnumerable<Resource>) (resource1.CandidateResources ?? this.Resources));
      Dictionary<Resource, int> dictionary = new Dictionary<Resource, int>();
      foreach (Resource key in source)
        dictionary[key] = new List<Assignment>(key.Assignments).Count;
      List<Resource> resourceList = new List<Resource>();
      while (source.Count > 0)
      {
        int num = source.Select<Resource, int>((System.Func<Resource, int>) (resource3 => dictionary[resource3])).Append<int>(int.MaxValue).Min();
        foreach (Resource resource2 in source.Where<Resource>((System.Func<Resource, bool>) (resource4 => dictionary[resource4] == num)).ToList<Resource>())
        {
          resourceList.Add(resource2);
          source.Remove(resource2);
        }
      }
      foreach (Resource resource3 in resourceList)
      {
        bool flag1 = true;
        for (int index = 0; index < pos; ++index)
        {
          Assignment resourceAssignment2 = unknownResourceAssignments[index];
          if (resourceAssignment2.Task == resourceAssignment1.Task && resourceAssignment2.Resource == resource3)
          {
            flag1 = false;
            break;
          }
        }
        bool flag2 = flag1 && resourceAssignment1.Task != null && !Intermech.Project.Project.HasResource(resourceAssignment1.Task.Assignments, resource3);
        if (flag2)
          resourceAssignment1.Resource = resource3;
        bool flag3 = flag2 && this.ArrangeTasks(l, priorityHierarchy, unknownResourceAssignments, pos + 1, startTime);
        if (flag2)
          resourceAssignment1.Resource = (Resource) resource1;
        if (flag2 && !flag3)
          return false;
      }
    }
    return true;
  }

  private bool ArrangeTasks(
    [NotNull] List<Intermech.Project.Project.AssignmentArrangementInformation> la,
    [NotNull] Dictionary<Assignment, List<Assignment>> currentAssignments,
    [NotNull] Dictionary<Assignment, double> currentAssignmentUnits,
    [NotNull, ItemNotNull] List<List<Task>> priorityHierarchy,
    [NotNull, ItemNotNull] List<Assignment> stepResourceAssignments,
    [NotNull, ItemNotNull] List<Task> activeTasks,
    int pos,
    DateTime startTime)
  {
    if (DateTime.Now.Subtract(startTime).TotalSeconds >= IMProject.LevelingTimeoutSeconds)
      return false;
    bool flag1 = true;
    if (pos >= stepResourceAssignments.Count)
    {
      List<Intermech.Project.Project.ResourceArrangementInformation> arrangementInformationList = new List<Intermech.Project.Project.ResourceArrangementInformation>();
      this.ArrangeTasks(arrangementInformationList, priorityHierarchy, Intermech.Project.Project.GetUnknownResourceAssignments(activeTasks), 0, DateTime.Now);
      if (arrangementInformationList.Count <= 0)
        return false;
      double maxValue = arrangementInformationList.Select<Intermech.Project.Project.ResourceArrangementInformation, double>((System.Func<Intermech.Project.Project.ResourceArrangementInformation, double>) (information => information.Cost)).Append<double>(double.MaxValue).Min();
      List<Intermech.Project.Project.ResourceArrangementInformation> list1 = arrangementInformationList.Where<Intermech.Project.Project.ResourceArrangementInformation>((System.Func<Intermech.Project.Project.ResourceArrangementInformation, bool>) (information2 => information2.Cost > maxValue)).ToList<Intermech.Project.Project.ResourceArrangementInformation>();
      arrangementInformationList.RemoveRange<Intermech.Project.Project.ResourceArrangementInformation>((IEnumerable<Intermech.Project.Project.ResourceArrangementInformation>) list1);
      DateTime finish = DateTime.MaxValue;
      foreach (Intermech.Project.Project.ResourceArrangementInformation arrangementInformation in arrangementInformationList)
      {
        if (arrangementInformation.Finish < finish)
          finish = arrangementInformation.Finish;
      }
      List<Intermech.Project.Project.ResourceArrangementInformation> list2 = arrangementInformationList.Where<Intermech.Project.Project.ResourceArrangementInformation>((System.Func<Intermech.Project.Project.ResourceArrangementInformation, bool>) (information5 => information5.Finish > finish)).ToList<Intermech.Project.Project.ResourceArrangementInformation>();
      arrangementInformationList.RemoveRange<Intermech.Project.Project.ResourceArrangementInformation>((IEnumerable<Intermech.Project.Project.ResourceArrangementInformation>) list2);
      if (arrangementInformationList.Count <= 0)
        return false;
      Intermech.Project.Project.ResourceArrangementInformation arrangementInformation1 = arrangementInformationList[0];
      Dictionary<Assignment, List<Assignment>> selectedAssignments = new Dictionary<Assignment, List<Assignment>>();
      Dictionary<Assignment, double> selectedAssignmentUnits = new Dictionary<Assignment, double>();
      foreach (Assignment resourceAssignment in stepResourceAssignments)
        selectedAssignments[resourceAssignment] = new List<Assignment>();
      foreach (Assignment key in currentAssignments.Keys)
      {
        List<Assignment> currentAssignment = currentAssignments[key];
        foreach (Assignment assignment in currentAssignment)
          currentAssignment.Add(assignment);
      }
      foreach (Assignment key in currentAssignmentUnits.Keys)
        selectedAssignmentUnits[key] = currentAssignmentUnits[key];
      la.Add(new Intermech.Project.Project.AssignmentArrangementInformation(selectedAssignments, selectedAssignmentUnits, arrangementInformation1.SelectedResources, arrangementInformation1.Cost, arrangementInformation1.Finish, arrangementInformation1.SelectedTaskStarts));
      return true;
    }
    Assignment resourceAssignment1 = stepResourceAssignments[pos];
    double units = resourceAssignment1.Units;
    double maxUnits = resourceAssignment1.MaxUnits;
    for (double num1 = Math.Min(maxUnits, (double) this.Resources.Count); num1 >= units; num1 = Math.Ceiling((num1 - IMProject.LevelingAssignmentStep) / IMProject.LevelingAssignmentStep) * IMProject.LevelingAssignmentStep)
    {
      bool flag2 = resourceAssignment1.Resource is UnknownResource;
      List<Assignment> assignmentList = new List<Assignment>();
      double num2 = num1;
      if (flag2)
      {
        int num3 = resourceAssignment1.Task.Assignments.IndexOf(resourceAssignment1);
        for (; num2 > 1.0; --num2)
        {
          Assignment key = new Assignment(resourceAssignment1.Resource, 1.0);
          resourceAssignment1.Task.Assignments.Insert(num3++, key);
          assignmentList.Add(key);
          currentAssignments[resourceAssignment1].Add(key);
          currentAssignmentUnits[key] = 1.0;
        }
      }
      currentAssignmentUnits[resourceAssignment1] = resourceAssignment1.Units = num2;
      if (!this.ArrangeTasks(la, currentAssignments, currentAssignmentUnits, priorityHierarchy, stepResourceAssignments, activeTasks, pos + 1, DateTime.Now))
        flag1 = false;
      if (flag2)
      {
        foreach (Assignment assignment in assignmentList)
        {
          resourceAssignment1.Task.Assignments.Remove(assignment);
          currentAssignments[resourceAssignment1].Remove(assignment);
        }
      }
      if (!flag1)
        break;
    }
    resourceAssignment1.Units = units;
    resourceAssignment1.MaxUnits = maxUnits;
    return flag1;
  }

  [NotNull]
  private Dictionary<Task, DateTime> ArrangeTaskStarts(
    [NotNull, ItemNotNull] List<List<Task>> priorityHierarchy,
    out double cost,
    out DateTime finish)
  {
    Dictionary<Task, DateTime> dictionary1 = new Dictionary<Task, DateTime>();
    int num1 = 0;
    bool flag1;
    do
    {
      dictionary1.Clear();
      flag1 = false;
      Dictionary<Task, List<Assignment>> dictionary2 = new Dictionary<Task, List<Assignment>>();
      foreach (List<Task> taskList in priorityHierarchy)
      {
        foreach (Task key in taskList)
        {
          dictionary2[key] = new List<Assignment>((IEnumerable<Assignment>) key.Assignments);
          for (int index = 0; index < dictionary2[key].Count; ++index)
          {
            Assignment assignment1 = dictionary2[key][index];
            if (!(assignment1.Resource is UnknownResource))
            {
              Assignment assignment2 = new Assignment((Resource) new UnknownResource(), assignment1.Units);
              key.Assignments[index] = assignment2;
            }
          }
        }
      }
      Dictionary<Resource, DateTime> dictionary3 = new Dictionary<Resource, DateTime>();
      DateTime today = DateTime.Today;
      foreach (List<Task> taskList in priorityHierarchy)
      {
        foreach (Task task1 in taskList)
        {
          for (int index = 0; index < dictionary2[task1].Count; ++index)
          {
            Assignment assignment = dictionary2[task1][index];
            task1.Assignments[index] = assignment;
          }
          DateTime dateTime1 = today;
          if (dateTime1 < this.Start)
            dateTime1 = this.Start;
          for (Task task2 = task1; task2 != null; task2 = task2.Parent)
          {
            DateTime startConstraint = task2.StartConstraint;
            if (dateTime1 < startConstraint)
              dateTime1 = startConstraint;
          }
          DateTime dateTime2 = dateTime1;
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task1.Assignments)
          {
            if (!(assignment.Resource is UnknownResource) && assignment.Resource != null && dictionary3.ContainsKey(assignment.Resource))
            {
              DateTime dateTime3 = dictionary3[assignment.Resource];
              if (dateTime1 < dateTime3)
                dateTime1 = dateTime3;
            }
          }
          DateTime dateTime4 = dateTime2;
          DateTime dateTime5 = dateTime1.AddDays((double) IMProject.MaximumLevelingPostponeDays);
          double num2 = double.MaxValue;
          for (DateTime dateTime6 = dateTime1; dateTime6 < dateTime5; dateTime6 = task1.Start.AddDays(1.0))
          {
            task1.Start = dateTime6;
            if (!(task1.Start < dateTime6))
            {
              bool flag2 = true;
              foreach (Task dependenciesTask in Intermech.Project.Project.GetRelatedDependenciesTasks(task1))
              {
                if (!(dependenciesTask.Finish <= dependenciesTask.FinishConstraint))
                {
                  int num3 = 0;
                  DateTime dateTime7 = task1.Start;
                  while (dependenciesTask.Finish > dependenciesTask.FinishConstraint)
                  {
                    dateTime7 = dateTime7.AddDays(-1.0);
                    task1.Start = dateTime7;
                    if (task1.Start > dateTime7 && num3++ > IMProject.MaximumDateTryCount)
                      break;
                  }
                  flag2 = false;
                  if (dependenciesTask.Finish > dependenciesTask.FinishConstraint)
                    flag1 = true;
                }
              }
              if (flag2)
              {
                double num4 = 0.0;
                foreach (DateSchedule dateSchedule in (List<DateSchedule>) task1.WorkTime)
                  num4 += dateSchedule.TimeIntervalCollection.Duration;
                double num5 = 0.0;
                foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task1.Assignments)
                {
                  double num6 = num5;
                  double num7 = num4;
                  double units = assignment.Units;
                  Resource resource1 = assignment.Resource;
                  double num8 = resource1 != null ? resource1.WorkHourCost : 0.0;
                  double num9 = units * num8;
                  double num10 = Math.Max(assignment.Units - 1.0, 0.0);
                  Resource resource2 = assignment.Resource;
                  double num11 = resource2 != null ? resource2.OvertimeWorkSupplementalHourCost : 0.0;
                  double num12 = num10 * num11;
                  double num13 = num9 + num12;
                  double num14 = num7 * num13 / task1.Units;
                  num5 = num6 + num14;
                }
                double currentCost = Intermech.Project.Project.GetCurrentCost(task1, false);
                foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task1.Assignments)
                {
                  if (!(assignment.Resource is UnknownResource))
                  {
                    double duration = task1.CurrentSchedule.GetDayTimeIntervals(task1.Start).Duration;
                    double num15 = 0.0;
                    if (assignment.Resource != null)
                    {
                      foreach (DateSchedule dateSchedule in assignment.Resource.WorkTime)
                      {
                        if (dateSchedule.Date == task1.Start)
                        {
                          num15 += assignment.Units * dateSchedule.TimeIntervalCollection.Duration;
                          if (num15 > duration)
                          {
                            currentCost += Math.Abs(currentCost) * IMProject.MultiplyOverAllocationFactor;
                            break;
                          }
                        }
                      }
                    }
                  }
                }
                if (currentCost < num2)
                {
                  dateTime4 = task1.Start;
                  num2 = currentCost;
                }
                if (currentCost <= num5)
                  break;
              }
              else
                break;
            }
            else
              break;
          }
          task1.Start = dateTime4;
          dictionary1[task1] = task1.Start;
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task1.Assignments)
          {
            if (!(assignment.Resource is UnknownResource))
            {
              DateTime minValue = DateTime.MinValue;
              if (dictionary3.ContainsKey(assignment.Resource))
                minValue = dictionary3[assignment.Resource];
              DateTime start = task1.Start;
              if (start > minValue)
                dictionary3[assignment.Resource] = start;
            }
          }
        }
      }
      ++num1;
    }
    while (flag1 && num1 < IMProject.MaximumCompletionTryCount);
    bool flag3 = false;
    foreach (List<Task> taskList in priorityHierarchy)
    {
      double num16 = 1.0;
      DateTime dateTime = DateTime.MinValue;
      foreach (Task task in taskList)
      {
        if ((double) task.Priority < num16)
        {
          if (task.Start < dateTime)
          {
            flag3 = true;
            break;
          }
          num16 = (double) task.Priority;
        }
        if (!flag3)
        {
          if (task.Start > dateTime)
            dateTime = task.Start;
        }
        else
          break;
      }
    }
    cost = !flag1 ? (!flag3 ? this.Cost : this.Cost + Math.Abs(this.Cost * IMProject.MultiplyIncorrectPrioritiesFactor)) : double.MaxValue;
    finish = this.Finish;
    return dictionary1;
  }

  [NotNull]
  private List<Task> GetAppropriateLeafTasksAndUpdateUnassignedTasks()
  {
    List<Task> updateUnassignedTasks = new List<Task>();
    foreach (Task task1 in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
    {
      if (!task1.HasSubTasks && task1.CompletedWork == 0.0)
      {
        if (task1.Assignments.Count > 0)
        {
          updateUnassignedTasks.Add(task1);
        }
        else
        {
          DateTime today = DateTime.Today;
          DateTime dateTime = this.Start;
          for (Task task2 = task1; task2 != null; task2 = task2.Parent)
          {
            DateTime startConstraint = task2.StartConstraint;
            if (dateTime < startConstraint)
              dateTime = startConstraint;
          }
          task1.Start = today > dateTime ? today : dateTime;
        }
      }
    }
    return updateUnassignedTasks;
  }

  internal static double GetCurrentCost([NotNull] Task t, bool considerPreviousTasksOnly = true)
  {
    double currentCost = 0.0;
    foreach (Assignment assignment1 in (System.Collections.ObjectModel.Collection<Assignment>) t.Assignments)
    {
      Resource resource = assignment1.Resource;
      double workHourCost = resource.WorkHourCost;
      double supplementalHourCost = resource.OvertimeWorkSupplementalHourCost;
      if (supplementalHourCost == 0.0 || resource is UnknownResource)
      {
        currentCost += workHourCost * assignment1.Units / t.Units * t.Work;
      }
      else
      {
        List<Assignment> assignmentList = new List<Assignment>(resource.Assignments);
        Dictionary<DateTime, double> dictionary1 = new Dictionary<DateTime, double>();
        Dictionary<DateTime, double> dictionary2 = new Dictionary<DateTime, double>();
        foreach (Assignment assignment2 in assignmentList)
        {
          if (assignment2.Task != null && (!considerPreviousTasksOnly || assignment2.Task.Index <= t.Index))
          {
            foreach (DateSchedule dateSchedule in (List<DateSchedule>) assignment2.Task.WorkTime)
            {
              DateTime date = dateSchedule.Date;
              if (!dictionary1.ContainsKey(date))
                dictionary1[date] = 0.0;
              double num = assignment2.Units / assignment2.Task.Units * dateSchedule.TimeIntervalCollection.Duration;
              Dictionary<DateTime, double> dictionary3;
              DateTime key1;
              (dictionary3 = dictionary1)[key1 = date] = dictionary3[key1] + num;
              if (assignment2 == assignment1)
              {
                if (!dictionary2.ContainsKey(date))
                  dictionary2[date] = 0.0;
                Dictionary<DateTime, double> dictionary4;
                DateTime key2;
                (dictionary4 = dictionary2)[key2 = date] = dictionary4[key2] + num;
              }
            }
          }
        }
        double num1 = 0.0;
        double num2 = 0.0;
        foreach (DateTime key in dictionary2.Keys)
        {
          double duration = t.CurrentSchedule.GetDayTimeIntervals(key.Date).Duration;
          if (dictionary1[key] <= duration)
          {
            num1 += dictionary2[key];
          }
          else
          {
            num1 += Math.Max(0.0, dictionary2[key] - duration);
            num2 += dictionary2[key] - Math.Max(0.0, dictionary2[key] - duration);
          }
        }
        currentCost += workHourCost * (num1 + num2) + supplementalHourCost * num2;
      }
    }
    return currentCost;
  }

  [NotNull]
  private List<List<Task>> GetDependencyHierarchy([NotNull] List<Task> activeTasks)
  {
    List<List<Task>> dependencyHierarchy = new List<List<Task>>();
    DependencyType[] dependencyTypeArray = new DependencyType[1];
    Dictionary<Task, List<Task>> dictionary = new Dictionary<Task, List<Task>>();
    foreach (Task activeTask in activeTasks)
    {
      List<Task> taskList1 = new List<Task>();
      Task task1 = activeTask;
      do
      {
        taskList1.Add(task1);
        task1 = task1.Parent;
      }
      while (task1 != null);
      List<Task> taskList2 = new List<Task>();
      foreach (Task task2 in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        if (!taskList1.Contains(task2))
        {
          Task task3 = activeTask;
          do
          {
            if (task3.DependsOf(task2, (ICollection<DependencyType>) dependencyTypeArray))
            {
              foreach (Task allTask in (IEnumerable<Task>) task2.AllTasks)
              {
                if (allTask != activeTask && activeTasks.Contains(allTask) && !taskList2.Contains(allTask))
                  taskList2.Add(allTask);
              }
            }
            task3 = task3.Parent;
          }
          while (task3 != null);
        }
      }
      dictionary[activeTask] = taskList2;
    }
    List<Task> taskList3 = new List<Task>((IEnumerable<Task>) activeTasks);
    List<Task> list5 = new List<Task>();
    do
    {
      List<Task> taskList4 = new List<Task>();
      foreach (Task key in taskList3)
      {
        if (dictionary[key].All<Task>((System.Func<Task, bool>) (task7 => list5.Contains(task7))))
          taskList4.Add(key);
      }
      foreach (Task task in taskList4)
      {
        taskList3.Remove(task);
        list5.Add(task);
      }
      if (taskList4.Count > 0)
        dependencyHierarchy.Add(taskList4);
      else
        break;
    }
    while (taskList3.Count > 0);
    return dependencyHierarchy;
  }

  private static double GetEstimatedDelay([NotNull, ItemNotNull] IEnumerable<Task> tasks)
  {
    double estimatedDelay = 0.0;
    foreach (Task task in tasks)
    {
      double val1 = task.Finish > task.FinishConstraint ? task.Finish.Subtract(task.FinishConstraint).TotalDays : 0.0;
      double val2 = task.HasSubTasks ? Intermech.Project.Project.GetEstimatedDelay((IEnumerable<Task>) task.SubTasks) : 0.0;
      estimatedDelay += Math.Max(val1, val2);
    }
    return estimatedDelay;
  }

  private static double GetPriority([NotNull] Task t)
  {
    double priority = (double) t.Priority;
    if (t.Parent != null)
      priority = (priority + Intermech.Project.Project.GetPriority(t.Parent)) / 2.0;
    return priority;
  }

  [NotNull]
  private static List<List<Task>> GetPriorityHierarchy([NotNull] List<List<Task>> dependencyHierarchy)
  {
    List<List<Task>> priorityHierarchy = new List<List<Task>>();
    foreach (IEnumerable<Task> collection in dependencyHierarchy)
    {
      List<Task> source = new List<Task>(collection);
      List<Task> taskList = new List<Task>();
      while (source.Count > 0)
      {
        Dictionary<Task, double> dictionary = new Dictionary<Task, double>();
        double num = 0.0;
        foreach (Task task in source)
        {
          double priority = Intermech.Project.Project.GetPriority(task);
          dictionary.Add(task, priority);
          if (priority > num)
            num = priority;
        }
        List<Task> list = source.Where<Task>((System.Func<Task, bool>) (task2 => dictionary[task2] == num)).ToList<Task>();
        taskList.AddRange((IEnumerable<Task>) list);
        foreach (Task task in list)
          source.Remove(task);
      }
      priorityHierarchy.Add(taskList);
    }
    return priorityHierarchy;
  }

  [NotNull]
  private static List<Task> GetRelatedDependenciesTasks([NotNull] Task t)
  {
    List<Task> taskList = new List<Task>((IEnumerable<Task>) t.AllSubTasks);
    do
    {
      taskList.Insert(0, t);
      t = t.Parent;
    }
    while (t != null);
    List<Task> ret = new List<Task>();
    foreach (Task task1 in taskList)
    {
      if (!ret.Contains(task1))
      {
        ret.Add(task1);
        foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.RelatedDependencies)
        {
          foreach (Task task2 in Intermech.Project.Project.GetRelatedDependenciesTasks(relatedDependency.Task).Where<Task>((System.Func<Task, bool>) (t3 => !ret.Contains(t3))))
            ret.Add(task2);
        }
      }
    }
    return ret;
  }

  [NotNull]
  private static List<Assignment> GetStepResourceAssignments([NotNull] List<Task> activeTasks)
  {
    return new List<Assignment>(activeTasks.SelectMany<Task, Assignment>((System.Func<Task, IEnumerable<Assignment>>) (task => task.Assignments.Where<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource is UnknownResource && assignment.MaxUnits > 1.0)))));
  }

  [NotNull]
  private static List<Assignment> GetUnknownResourceAssignments([NotNull] List<Task> activeTasks)
  {
    return new List<Assignment>(activeTasks.SelectMany<Task, Assignment>((System.Func<Task, IEnumerable<Assignment>>) (task => task.Assignments.Where<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource is UnknownResource)))));
  }

  private static bool HasResource([NotNull] AssignmentCollection assignments, [NotNull] Resource resource)
  {
    return assignments.Any<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource == resource));
  }

  public virtual void ChangeIndent([NotNull, ItemNotNull] IEnumerable<Task> tasks, int dx)
  {
    Dictionary<Task, int> dictionary = new Dictionary<Task, int>();
    foreach (Task task in tasks)
    {
      if (!dictionary.ContainsKey(task))
        dictionary.Add(task, task.IndentLevel);
    }
    foreach (KeyValuePair<Task, int> keyValuePair in dictionary)
    {
      Task key;
      int num;
      keyValuePair.Deconstruct<Task, int>(out key, out num);
      key.IndentLevel = num + dx;
    }
  }

  protected override void Initialize()
  {
    base.Initialize();
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      task._Project = this;
    foreach (Resource resource in (System.Collections.ObjectModel.Collection<Resource>) this.Resources)
      resource.projects.Add(this);
    this.Tasks.ListChanged += new ListChangedEventHandler(this.Tasks_ListChanged);
    this.Tasks.ItemRemoving += new EventHandler<ItemEventArgs<Task>>(this.Tasks_ItemRemoving);
    this.Tasks.ItemRemoved += new EventHandler<ItemEventArgs<Task>>(this.Tasks_ItemRemoved);
    this.Tasks.ItemAdding += new EventHandler<ItemEventArgs<Task>>(this.Tasks_ItemAdding);
    this.Resources.ListChanged += new ListChangedEventHandler(this.Resources_ListChanged);
    if (this._Schedule == null)
      this.Schedule = Schedule.Standard ?? throw new NullReferenceException("Standard");
    this._Start = DateTime.Now.Date;
    this.ValidateInWorkTime(ref this._Start);
  }

  private void Tasks_ItemAdding([CanBeNull] object sender, [NotNull] ItemEventArgs<Task> e)
  {
    if (e.Item.HasState(TaskState.Loading))
      return;
    Intermech.Project.Project.PrevTaskInfo prevTaskInfo = this.GetPrevTaskInfo(e.Index);
    if (prevTaskInfo._Project == null)
      return;
    prevTaskInfo._Project.CanSetProperty("Tasks", (object) e.Item);
  }

  private void Tasks_ItemRemoving([CanBeNull] object sender, [NotNull] ItemEventArgs<Task> e)
  {
    Intermech.Project.Project project = e.Item.Project;
    if (project == null || this.DeletedTasks.Contains((Task) project))
      return;
    project.CanSetProperty("Tasks", (object) null);
  }

  private void _LevelResources()
  {
    List<Task> updateUnassignedTasks = this.GetAppropriateLeafTasksAndUpdateUnassignedTasks();
    if (updateUnassignedTasks.Count == 0)
      return;
    List<Task> activeTasks = this.RemoveNonFsDependentTasks(updateUnassignedTasks);
    if (activeTasks.Count == 0)
      return;
    Dictionary<Task, List<Dependency>> dictionary1 = new Dictionary<Task, List<Dependency>>();
    Dictionary<Task, List<Assignment>> dictionary2 = new Dictionary<Task, List<Assignment>>();
    DependencyType[] dependTypesFromStart = new DependencyType[3]
    {
      DependencyType.StartStart,
      DependencyType.FinishFinish,
      DependencyType.StartFinish
    };
    foreach (Task key in activeTasks)
    {
      if (key.Dependencies.Any<Dependency>((System.Func<Dependency, bool>) (dependency => ((IEnumerable<DependencyType>) dependTypesFromStart).Contains<DependencyType>(dependency.DependencyType))))
      {
        dictionary1.Add(key, new List<Dependency>());
        bool flag = true;
        List<Dependency> dependencyList = dictionary1[key];
        foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) key.Dependencies)
        {
          if (((IEnumerable<DependencyType>) dependTypesFromStart).Contains<DependencyType>(dependency.DependencyType))
            dependencyList.Add(dependency);
          else
            flag = false;
        }
        foreach (Dependency dependency in dependencyList)
          dependency.Delete();
        if (flag)
        {
          dictionary2.Add(key, new List<Assignment>());
          List<Assignment> assignmentList = dictionary2[key];
          assignmentList.AddRange((IEnumerable<Assignment>) key.Assignments);
          foreach (Assignment assignment in assignmentList)
            assignment.Delete();
        }
      }
    }
    foreach (Task key in dictionary1.Keys)
      activeTasks.Remove(key);
    this.ArrangeTasks(Intermech.Project.Project.GetPriorityHierarchy(this.GetDependencyHierarchy(activeTasks)), Intermech.Project.Project.GetStepResourceAssignments(activeTasks), activeTasks);
    foreach (Task key in dictionary1.Keys)
    {
      List<Dependency> items = dictionary1[key];
      key.Dependencies.AddRange((IEnumerable<Dependency>) items);
    }
    foreach (Task key in dictionary2.Keys)
    {
      List<Assignment> items = dictionary2[key];
      key.Assignments.AddRange((IEnumerable<Assignment>) items);
    }
  }

  public virtual void LevelResources() => this.LevelResources(false);

  public virtual void LevelResources(bool provideFeedback)
  {
    if (!provideFeedback)
    {
      Intermech.Project.Project project = (Intermech.Project.Project) null;
      try
      {
        using (MemoryStream serializationStream = new MemoryStream())
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          binaryFormatter.Serialize((Stream) serializationStream, (object) this);
          serializationStream.Seek(0L, SeekOrigin.Begin);
          project = (Intermech.Project.Project) binaryFormatter.Deserialize((Stream) serializationStream);
        }
      }
      catch (SecurityException ex)
      {
      }
      if (project == null)
        return;
      project._LevelResources();
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        Task task1 = this.Tasks[index];
        if (!task1.HasSubTasks)
        {
          Task task2 = project.Tasks[index];
          task1.Start = task2.Start;
          if (!task1.Milestone)
            task1.AssignmentsString = task2.AssignmentsString;
        }
      }
    }
    else
      this._LevelResources();
  }

  [NotNull]
  private List<Task> RemoveNonFsDependentTasks([NotNull] List<Task> leaveTasks)
  {
    DependencyType[] dependencyTypes = new DependencyType[3]
    {
      DependencyType.StartStart,
      DependencyType.FinishFinish,
      DependencyType.StartFinish
    };
    return leaveTasks.Where<Task>((System.Func<Task, bool>) (task => this.Tasks.All<Task>((System.Func<Task, bool>) (task2 => task2 == task || !task.DependsOf(task2, (ICollection<DependencyType>) dependencyTypes))))).ToList<Task>();
  }

  private void Resources_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType == ListChangedType.ItemAdded)
    {
      Resource resource = this.Resources[e.NewIndex];
      if (!resource.projects.Contains(this))
        resource.projects.Add(this);
    }
    if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
    {
      int newIndex = e.NewIndex;
      Resource resource = this.Resources[newIndex];
      string name = resource.Name;
      for (int index = 0; index < this.Resources.Count; ++index)
      {
        if (index != newIndex && this.Resources[index].Name == name)
          resource.Name += "'";
      }
    }
    if (e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemMoved || e.ListChangedType == ListChangedType.Reset)
    {
      if (e.ListChangedType != ListChangedType.ItemChanged)
      {
        for (int index1 = 0; index1 < this.Resources.Count; ++index1)
        {
          Resource resource = this.Resources[index1];
          if (!resource.projects.Contains(this))
            resource.projects.Add(this);
          string name = resource.Name;
          for (int index2 = 0; index2 < this.Resources.Count; ++index2)
          {
            if (index2 != index1 && this.Resources[index2].Name == name)
              resource.Name += "'";
          }
        }
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          List<Assignment> assignmentList = new List<Assignment>();
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task.Assignments)
          {
            if ((assignment.Resource is UnknownResource resource ? resource.CandidateResources : (ResourceCollection) null) != null)
            {
              foreach (Resource candidateResource in (System.Collections.ObjectModel.Collection<Resource>) resource.CandidateResources)
              {
                if (!this.Resources.Contains(candidateResource))
                {
                  if (resource.CandidateResources.Count > 2)
                  {
                    resource.CandidateResources.Remove(candidateResource);
                    break;
                  }
                  using (IEnumerator<Resource> enumerator = resource.CandidateResources.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      Resource current = enumerator.Current;
                      if (this.Resources.Contains(current))
                      {
                        assignment.Resource = current;
                        break;
                      }
                    }
                    break;
                  }
                }
              }
            }
            if (resource == null && !this.Resources.Contains(assignment.Resource))
              assignmentList.Add(assignment);
          }
          foreach (Assignment assignment in assignmentList)
            task.Assignments.Remove(assignment);
        }
      }
      else
      {
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          Resource resource7 = this.Resources[e.NewIndex];
          if (task.Assignments.Any<Assignment>((System.Func<Assignment, bool>) (assignment3 => assignment3.Resource == resource7)))
            task.PropertiesChanged();
        }
      }
    }
    if (e.ListChangedType != ListChangedType.ItemAdded)
    {
      foreach (Entity task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        task.OnPropertyChanged("AssignmentsString");
    }
    this.OnPropertyChanged("Resources");
  }

  [NotNull]
  internal List<Task> DeletedTasks
  {
    get
    {
      return this._deletedTasks ?? (this._deletedTasks = this.RootProject != this ? this.RootProject.DeletedTasks : new List<Task>());
    }
  }

  private void Tasks_ItemRemoved([CanBeNull] object sender, [NotNull] ItemEventArgs<Task> e)
  {
    Task task1 = e.Item;
    if (task1.ObjectID != 0L && !this.DeletedTasks.Contains(task1) && !this.DeletedTasks.Contains((Task) task1.MyProject) && !this.DeletedTasks.Contains(task1))
      this.DeletedTasks.Add(task1);
    task1.Dependencies.Clear();
    foreach (Dependency dependency in new List<Dependency>((IEnumerable<Dependency>) task1.BackDependencies))
    {
      if (dependency.Task != null)
        dependency.Task.Dependencies.Remove(dependency);
    }
    if (task1.Uncommitted)
      return;
    for (Task task2 = task1; task2 != null; task2 = task2.Parent)
    {
      task2.OnPropertyChanged("SubTasks");
      task2.OnPropertyChanged("AllSubTasks");
      task2.OnPropertyChanged("HasSubTasks");
      task2.PropertiesChanged(false);
    }
    this.TasksChanged();
    this.RecalcStartFinish();
  }

  [NotNull]
  [ItemNotNull]
  internal List<Dependency> DeletedDependencies
  {
    get
    {
      return this._deletedDependencies ?? (this._deletedDependencies = this.RootProject != this ? this.RootProject.DeletedDependencies : new List<Dependency>());
    }
  }

  [NotNull]
  private Intermech.Project.Project.PrevTaskInfo GetPrevTaskInfo(int index)
  {
    Task task = (Task) null;
    Intermech.Project.Project project1 = (Intermech.Project.Project) null;
    int index1 = index;
    if (index1 > 0)
    {
      do
      {
        --index1;
        task = this.Tasks[index1];
      }
      while (index1 > 0 && task.IsParentMinimized);
    }
    if (task != null)
    {
      if (task is Intermech.Project.Project project2 && !project2.Minimized && !project2.HasNotLoadedSubTasks)
        project1 = project2;
      else if (task.Project != null)
        project1 = task.Project;
    }
    return new Intermech.Project.Project.PrevTaskInfo(task, project1);
  }

  private void Tasks_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType == ListChangedType.Reset)
      return;
    bool flag = false;
    if (e.ListChangedType == ListChangedType.ItemAdded)
    {
      int newIndex = e.NewIndex;
      Task task1 = this.Tasks[newIndex];
      task1.ClearCache();
      flag = task1.Uncommitted;
      task1.EditingMode = task1.DefaultEditingMode;
      this.DeletedTasks.Remove(task1);
      if (task1.ObjectID != 0L)
      {
        for (int index = this.DeletedTasks.Count - 1; index >= 0; --index)
        {
          if (this.DeletedTasks[index].ObjectID == task1.ObjectID)
            this.DeletedTasks.RemoveAt(index);
        }
      }
      string str = this.CheckForDuplicates(task1, newIndex);
      if (str != null && this.HasState(TaskState.Loading))
      {
        task1.Name = str;
        task1.ErrorString = Intermech.Project.Properties.Resources.ErrDupSubProject;
      }
      if (task1._Project == null)
      {
        Intermech.Project.Project.PrevTaskInfo prevTaskInfo = this.GetPrevTaskInfo(newIndex);
        if (prevTaskInfo._Project != null)
          task1.Project = prevTaskInfo._Project;
        if (task1._Project == null)
          task1._Project = this;
        if (task1._IndentLevel == -1)
        {
          int num = 0;
          Task task2 = (Task) null;
          if (newIndex < this.Tasks.Count - 1)
            task2 = this.Tasks[newIndex + 1];
          if (task2 != null && task2.IndentLevel > num)
            num = task2.IndentLevel;
          if (prevTaskInfo._Task != null && prevTaskInfo._Task.IndentLevel > num)
            num = prevTaskInfo._Task.IndentLevel;
          task1._IndentLevel = num;
        }
        else
        {
          int num1;
          if (this._copiedRootIndentDXs.TryGetValue(task1, out num1))
            this._currentCopiedRoot = task1;
          else if (this._currentCopiedRoot != null)
            this._copiedRootIndentDXs.TryGetValue(this._currentCopiedRoot, out num1);
          if (num1 == 9999)
          {
            int indentLevel = task1._IndentLevel;
            Intermech.Project.Project rootProject = this.RootProject;
            int num2 = 0;
            for (int index = task1.RealIndex - 1; index >= 0; --index)
            {
              if (rootProject.Tasks[index].IndentLevel + 1 > num2)
                num2 = rootProject.Tasks[index].IndentLevel + 1;
            }
            num1 = indentLevel <= num2 ? 0 : num2 - indentLevel;
            if (this._currentCopiedRoot != null)
              this._copiedRootIndentDXs[this._currentCopiedRoot] = num1;
          }
          task1.SetIndentLevel(task1._IndentLevel + num1, false);
        }
      }
      if (task1._IndentLevel == -1)
        task1._IndentLevel = 0;
      task1.CheckInProjectBounds();
      if (task1.ConstraintType == ConstraintType.Undefined)
        task1._ConstraintType = this.ConstraintType;
      task1.ClearCache();
      foreach (Task task3 in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        if (task3.Dependencies.Count > 0)
          task3.OnPropertyChanged("DependenciesString", false);
      }
      if (!task1.Uncommitted)
        task1.PropertiesChanged();
    }
    else if (e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemMoved || e.ListChangedType == ListChangedType.Reset)
    {
      if (e.ListChangedType != ListChangedType.ItemDeleted || e.NewIndex < this.Tasks.Count)
      {
        for (int index1 = 0; index1 < this.Tasks.Count; ++index1)
        {
          Task task4 = this.Tasks[index1];
          if (!task4.IsProjectSummaryTask)
          {
            int num3 = -1;
            for (int index2 = index1 - 1; index2 >= 0; --index2)
            {
              Task task5 = this.Tasks[index2];
              if (num3 < 0 || task5.IndentLevel <= num3)
              {
                if (task5.IndentLevel > num3)
                  num3 = task5.IndentLevel;
              }
              else
                break;
            }
            if (task4.IndentLevel > num3 + 1)
            {
              int indentLevel = task4._IndentLevel;
              Dictionary<Task, int> dictionary = new Dictionary<Task, int>();
              int num4 = index1;
              while (num4 < this.Tasks.Count)
              {
                Task task6 = this.Tasks[num4++];
                if (task6.IndentLevel >= indentLevel)
                  dictionary.Add(task6, num3 + 1 + (task6.IndentLevel - indentLevel));
                if (task6.IndentLevel < indentLevel)
                  break;
              }
              foreach (Task key in dictionary.Keys)
                key._IndentLevel = dictionary[key];
              index1 = num4 - 1;
            }
          }
        }
      }
      flag = e.NewIndex >= this.Tasks.Count;
    }
    if (e.ListChangedType == ListChangedType.ItemMoved || e.ListChangedType == ListChangedType.Reset)
    {
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        task.ClearCache();
        task.OnPropertyChanged("Parent", false);
        task.OnPropertyChanged("Index", false);
        task.OnPropertyChanged("IndexString", false);
        task.OnPropertyChanged("WbsCode", false);
        task.OnPropertyChanged("SubTasks", false);
        task.OnPropertyChanged("AllSubTasks", false);
        task.OnPropertyChanged("HasSubTasks", false);
      }
    }
    else if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted)
    {
      for (int newIndex = e.NewIndex; newIndex < this.Tasks.Count; ++newIndex)
      {
        Task task = this.Tasks[newIndex];
        task.OnPropertyChanged("Parent", false);
        task.OnPropertyChanged("Index", false);
        task.OnPropertyChanged("IndexString", false);
        task.OnPropertyChanged("WbsCode", false);
        task.OnPropertyChanged("CurrentSchedule", false);
        if (task.Assignments.Count > 0)
        {
          task.OnPropertyChanged("Cost", false);
          task.OnPropertyChanged("CostString", false);
        }
      }
    }
    if (!flag && (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemMoved))
      this.TasksChanged();
    if (flag || e.ListChangedType != ListChangedType.ItemAdded)
      return;
    this.RecalcStartFinish();
  }

  /// <summary>
  /// Может это копия существующей задачи? Поищем, нет ли её уже здесь, если есть, сбросим ObjectID
  /// </summary>
  [CanBeNull]
  protected virtual string CheckForDuplicates([NotNull] Task task, int newIndex)
  {
    long objectId = task.ObjectID;
    if (objectId != 0L)
    {
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        if (index != newIndex && !(task is Intermech.Project.Project) && this.Tasks[index].ObjectID == objectId)
        {
          task.HackObjectID = 0L;
          return this.Tasks[index].Name;
        }
      }
    }
    return (string) null;
  }

  /// <summary>Создать копию проекта как задачи. Используется для конвертации в задачи подпроектов, вставляемых повторно (такие уже есть в проекте) из буфера обмена</summary>
  [NotNull]
  public Task GetCopyAsTask()
  {
    Task graph = new Task();
    graph.AssignProperties((Task) this.RootProject);
    foreach (FieldInfo field in typeof (Task).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField))
    {
      if (field.CustomAttributes != null && field.CustomAttributes.All<CustomAttributeData>((System.Func<CustomAttributeData, bool>) (customAttribute => customAttribute.AttributeType != typeof (NonSerializedAttribute))))
        field.SetValue((object) graph, field.GetValue((object) this));
    }
    graph.Duration = this.Duration;
    graph.DurationString = this.DurationString;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
      serializationStream.Seek(0L, SeekOrigin.Begin);
      object copyAsTask = binaryFormatter.Deserialize((Stream) serializationStream);
      if (copyAsTask.GetType() == typeof (Task))
        return (Task) copyAsTask;
    }
    throw new Exception("Can`t transform project to task!");
  }

  internal void TasksChanged()
  {
    for (Intermech.Project.Project project = this; project != null; project = project.Project)
    {
      project.OnPropertyChanged("Tasks");
      project.OnPropertyChanged("AllSubTasks");
    }
  }

  public override string CompletedWorkString
  {
    get => this.Work > 0.0 ? this.FormatDurationH(this.CompletedWork, false) : string.Empty;
  }

  public override double Cost
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Cost.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Cost.Value;
      }
      double cost = this.SubTasks.Aggregate<Task, double>(0.0, (Func<double, Task, double>) ((total, task) => total + task.Cost));
      this.Cache.Cost = new double?(cost);
      return cost;
    }
  }

  [NotNull]
  public override string CostString
  {
    get
    {
      return this.Work <= 0.0 ? string.Empty : $"{this.Cost:0.##}{(this.Estimation || this.Tasks.Any<Task>((System.Func<Task, bool>) (task => task.Assignments.Any<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource is UnknownResource)))) ? (object) IMProject.EstimationSymbol : (object) string.Empty)}";
    }
  }

  public virtual double EstimatedDelay
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.EstimatedDelay.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.EstimatedDelay.Value;
      }
      double estimatedDelay = Intermech.Project.Project.GetEstimatedDelay((IEnumerable<Task>) this.SubTasks);
      this.Cache.EstimatedDelay = new double?(estimatedDelay);
      return estimatedDelay;
    }
  }

  [NotNull]
  public virtual string EstimatedDelayString
  {
    get
    {
      return this.Work <= 0.0 ? string.Empty : this.FormatDuration(this.EstimatedDelay, false, WorkTimeUnits.Days);
    }
  }

  public override bool Estimation
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Estimation.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Estimation.Value;
      }
      if (this.SubTasks.Any<Task>((System.Func<Task, bool>) (task => task.Estimation)))
      {
        this.Cache.Estimation = new bool?(true);
        return true;
      }
      this.Cache.Estimation = new bool?(false);
      return false;
    }
    set
    {
      if (this.ManualPlanning)
        return;
      base.Estimation = value;
    }
  }

  public override DateTime Start
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Start.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Start.Value;
      }
      ++this._GetStartCounter;
      this.CheckForRecursion(this._GetStartCounter);
      try
      {
        if (this.IsSubProject && !this.ManualPlanning)
          return base.Start;
        this._prevStart = this._Start;
        if (!this.ManualPlanning && this.PlanningType == PlanningType.FromEnd)
        {
          Task.Graph projectGraph = this.ProjectGraph;
          this._Start = projectGraph != null ? projectGraph.RightDT : base.Start;
        }
        this.Cache.Start = new DateTime?(this._Start);
        this.Cache.Start = new DateTime?(this.AdjustByDependencies(this._Start, true));
      }
      finally
      {
        --this._GetStartCounter;
      }
      return this._Cache.Start.Value;
    }
    set
    {
      if (!this.ManualPlanning && this.PlanningType != PlanningType.FromStart || !(value != this.Start) || !this.CanSetProperty(nameof (Start), (object) value))
        return;
      this.OnPropertyChanging(nameof (Start));
      this._Start = value;
      Intermech.Project.Project.ProjectCache cache = this._Cache;
      if ((cache != null ? (cache.Start.HasValue ? 1 : 0) : 0) != 0)
        this._Cache.Start = new DateTime?();
      this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies | Task.CalcProps.ClearGraph, true);
      this.OnPropertyChangeCompleted(nameof (Start));
      if (!this.ManualPlanning || !(this._Finish != DateTime.MinValue) || !(value >= this._Finish))
        return;
      this.Finish = this.NextWorkingTime(value);
    }
  }

  public override DateTime Finish
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Finish.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Finish.Value;
      }
      ++this._GetFinishCounter;
      this.CheckForRecursion(this._GetFinishCounter);
      try
      {
        if (this.IsSubProject && !this.ManualPlanning && this.PlanningType != PlanningType.FromEnd)
          return base.Finish;
        this._prevFinish = this._Finish;
        if (!this.ManualPlanning && this.PlanningType == PlanningType.FromStart)
        {
          Task.Graph projectGraph = this.ProjectGraph;
          this._Finish = projectGraph != null ? projectGraph.RightDT : base.Finish;
        }
        this.Cache.Finish = new DateTime?(this._Finish);
        this.Cache.Finish = new DateTime?(this.AdjustByDependencies(this._Finish, false));
      }
      finally
      {
        --this._GetFinishCounter;
      }
      return this._Cache.Finish.Value;
    }
    set
    {
      if (!this.ManualPlanning && this.PlanningType != PlanningType.FromEnd || !(this._Finish != value) || !this.CanSetProperty(nameof (Finish), (object) value))
        return;
      this.OnPropertyChanging(nameof (Finish));
      this._Finish = value;
      Intermech.Project.Project.ProjectCache cache = this._Cache;
      if ((cache != null ? (cache.Finish.HasValue ? 1 : 0) : 0) != 0)
        this._Cache.Finish = new DateTime?();
      this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies | Task.CalcProps.ClearGraph, true);
      this.OnPropertyChangeCompleted(nameof (Finish));
      if (!this.ManualPlanning || !(value <= this._Start))
        return;
      this.Start = this.NextWorkingTime(value.AddDays(-1.0));
    }
  }

  internal bool CalcFinishChanged() => this.Finish != this._prevFinish;

  internal bool CalcStartChanged() => this.Start != this._prevStart;

  [NotNull]
  [ItemNotNull]
  internal virtual ResourceCollection Resources { get; } = new ResourceCollection();

  protected override IReadOnlyList<Task> GetSubTasks()
  {
    TaskCollection tasks = this.Tasks;
    List<Task> subTasks = new List<Task>(tasks.Count);
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) tasks)
    {
      if (!task.IsProjectSummaryTask && task.IndentLevel <= this.IndentLevel + 1)
        subTasks.Add(task);
    }
    return (IReadOnlyList<Task>) subTasks;
  }

  [CanBeNull]
  public override Schedule Schedule
  {
    get => base.Schedule;
    set => base.Schedule = value;
  }

  [CanBeNull]
  public override Schedule ProjectSchedule => this.Schedule;

  [NotNull]
  [ItemNotNull]
  public virtual TaskCollection Tasks
  {
    get
    {
      Intermech.Project.Project project = this.Project;
      if ((project != null ? (project.Partial ? 1 : 0) : 1) != 0)
        return this._Tasks;
      if (this.UseCache && this._Cache?.Tasks != null)
        return this._Cache.Tasks;
      TaskCollection taskCollection = new TaskCollection();
      foreach (Task allSubTask in (IEnumerable<Task>) this.AllSubTasks)
        taskCollection.Add(allSubTask);
      return this.Cache.Tasks = taskCollection;
    }
  }

  /// <summary>Список всех подпроектов проекта (не рекурсивно! подпроекты входящие в подпроекты не учитываются)</summary>
  [NotNull]
  [ItemNotNull]
  public IReadOnlyCollection<Intermech.Project.Project> SubProjects
  {
    get
    {
      if (this.UseCache && this._Cache?.SubProjects != null)
        return this._Cache.SubProjects;
      List<Intermech.Project.Project> projectList = (List<Intermech.Project.Project>) null;
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        if (task is Intermech.Project.Project project)
        {
          if (projectList == null)
            projectList = new List<Intermech.Project.Project>();
          projectList.Add(project);
        }
      }
      return this.Cache.SubProjects = (IReadOnlyCollection<Intermech.Project.Project>) ((object) projectList ?? (object) Array.Empty<Intermech.Project.Project>());
    }
  }

  protected override IReadOnlyList<Task> GetAllSubTasks()
  {
    Intermech.Project.Project project = this.Project;
    if ((project != null ? (project.Partial ? 1 : 0) : 1) != 0)
      return (IReadOnlyList<Task>) this._Tasks;
    if (this.UseCache && this._Cache?.AllSubTasks != null)
      return this._Cache.AllSubTasks;
    IReadOnlyList<Task> allSubTasks = base.GetAllSubTasks();
    this.Cache.AllSubTasks = allSubTasks;
    return allSubTasks;
  }

  public override double Work
  {
    get
    {
      if (!this.HasLoadedSubTasks)
        return base.Work;
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Work.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Work.Value;
      }
      double val1 = this.SubTasks.Aggregate<Task, double>(0.0, (Func<double, Task, double>) ((total, task) => total + task.Work));
      if (this.ManualPlanning)
        val1 = Math.Max(val1, this.GetWorkHours(this.Start, this.Finish));
      this.Cache.Work = new double?(val1);
      return val1;
    }
  }

  /// <summary>Реальная продолжительность проекта в часах, учитывая промежутки, вызванные ограничениями задач (начало не ранее и т.д.)</summary>
  public override double RealWork
  {
    get
    {
      double realWork = !this.HasLoadedSubTasks || this.ProjectGraph == null || this.ManualPlanning ? base.RealWork : this.ProjectGraph.FullWork;
      if (this.ManualPlanning)
      {
        double workHours = this.GetWorkHours(this.Start, this.Finish);
        this.PlanningConflict = realWork > workHours;
        realWork = workHours;
      }
      else
        this.PlanningConflict = false;
      return realWork;
    }
  }

  public override double Duration
  {
    get
    {
      if (this.UseCache)
      {
        Intermech.Project.Project.ProjectCache cache = this._Cache;
        if ((cache != null ? (cache.Duration.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Duration.Value;
      }
      double duration = this.RealWork / (this.CurrentSchedule.DayDuration * this.Units);
      this.Cache.Duration = new double?(duration);
      return duration;
    }
  }

  public override bool HasSubTasks => true;

  public override int ObjectTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project;

  [NotNull]
  public override Intermech.Project.Project RootProject => base.RootProject ?? this;

  public void Load(bool editingMode) => this.Load(this._ObjectID, new bool?(editingMode));

  [CanBeNull]
  protected override DataRow[] GetDbTasks(
    [CanBeNull] Intermech.Project.Project project,
    [NotNull] IUserSession session,
    int recordCount = -1,
    [CanBeNull] ConditionStructure[] conds = null)
  {
    if (this.ObjectID != 0L)
    {
      this._BulkData = new BulkData();
      ICompositionLoadService customService = session.GetCustomService<ICompositionLoadService>();
      List<ColumnDescriptor> compositionColumns = Task.FullCompositionColumns;
      this._BulkData.Tasks = customService.LoadComposition((object) session.SessionGUID, this.ObjectID, ObjectTypes.Project.ID, (IEnumerable<int>) new int[1]
      {
        (int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition
      }, (IEnumerable<int>) Helper.TasksTypeIDsArray, (IEnumerable<ColumnDescriptor>) compositionColumns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, string.Empty, (HybridDictionary) null, -1, (IEnumerable<int>) Helper.TasksTypeIDsArray);
      List<long> enumerable = new List<long>();
      if (this._BulkData.Tasks != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._BulkData.Tasks.Rows)
        {
          long num = row.FieldAsLong(this.Attr2Col(-2));
          enumerable.Add(num);
        }
      }
      if (enumerable.Count > 0)
      {
        IDBRelationCollection relationCollection = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.Resources);
        object[] columns = new object[8]
        {
          (object) -21,
          (object) -2,
          (object) -20,
          (object) -50,
          (object) -7,
          (object) Attributes.ResourceUnits.ID,
          (object) Attributes.ResourceIsChief.ID,
          (object) Intermech.Metadata.Attributes.Calendar.ID
        };
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-21, RelationalOperators.In, (object) enumerable.AsArray<long>(), LogicalOperators.AND, 0, false)
        }, columns, 0L, (object) null, -1);
        this._BulkData.Assignments = relationCollection.Select(paramSet);
      }
      else
        this._BulkData.Assignments = (DataTable) null;
      if (enumerable.Count > 0)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency);
        ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.Less, (object) 0, LogicalOperators.AND, 0, false);
        if (this.ObjectID > 0L && !this.PseudoCheckedOut)
          conditionStructure.RelationalOperator = RelationalOperators.Greater;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure((int) (IpsMetadataEntityBase<int>) Attributes.Project, RelationalOperators.Equal, (object) this.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
          conditionStructure
        }, new ColumnDescriptor[5]
        {
          new ColumnDescriptor((object) -2),
          new ColumnDescriptor((object) Attributes.FromTask.ID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
          new ColumnDescriptor((object) Attributes.ToTask.ID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
          new ColumnDescriptor((object) Attributes.DependencyType.ID),
          new ColumnDescriptor((object) Attributes.Lag.ID)
        });
        this._BulkData.Dependences = objectCollection.Select(paramSet);
      }
      else
        this._BulkData.Dependences = (DataTable) null;
    }
    return base.GetDbTasks(project, session, recordCount, conds);
  }

  protected override bool IsSubTasksExist(IUserSession session) => true;

  public void Load(long objectID, bool? editingMode)
  {
    try
    {
      this.StartProgress(1, string.Empty);
      this.ErrorLog = string.Empty;
      this.Loading();
      this.Tasks.Clear();
      this._ObjectID = objectID;
      this.Load(this, editingMode);
    }
    finally
    {
      this.Loaded();
      if (!this._ModifiedWhileLoading)
        this.Modified = false;
      this.StopProgress();
    }
  }

  public override void Load([CanBeNull] IDBObject obj, bool? editingMode)
  {
    this.Load(obj, this, editingMode);
  }

  [NotNull]
  protected XmlIni GetProjectData([CanBeNull] IDBAttribute attr, bool forceLoad = false)
  {
    forceLoad = forceLoad || this._projectData == null;
    if (this._projectData == null)
      this._projectData = new XmlIni();
    if (forceLoad)
      StreamHelper.LoadFromBlobStream(attr as IBlobReader, new ProcessStreamDelegate(this._projectData.Load));
    return this._projectData;
  }

  [NotNull]
  protected XmlIni ProjectData => this._projectData ?? (this._projectData = new XmlIni());

  protected virtual void CheckEditRights()
  {
    if (this.EditingMode.Any() && this.Status != TaskStatus.NotStarted && !this.IsChief)
      throw new ErrorMessageException(Intermech.Project.Properties.Resources.ErrOnlyChiefCanEdit, "Проверка прав");
  }

  protected override void LoadObject([NotNull] IDBObject obj, [CanBeNull] DataRow row)
  {
    base.LoadObject(obj, row);
    IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar);
    if (attributeById != null)
      this.Schedule = ScheduleList.GetSchedule(attributeById.AsInteger, obj.Session);
    this.LoadData(this.GetProjectData(obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ProjectData)));
  }

  [field: NonSerialized]
  public event CancelEventHandler Saving;

  [field: NonSerialized]
  public event EventHandler Saved;

  protected bool WasSaved { get; private set; }

  public override bool Save(IUserSession session)
  {
    if (this.Saving != null)
    {
      CancelEventArgs e = new CancelEventArgs(false);
      this.Saving((object) this, e);
      if (e.Cancel)
        return false;
    }
    this.StartProgress(this.Tasks.Count + 1, string.Empty, Localization.GetString("SavingProgress"));
    try
    {
      if (this.IsExecuted)
      {
        string str = this.Validate(true);
        if (str != string.Empty)
          throw new NotificationException($"{Localization.GetString("ErrProjectShouldBeValid", (object) this.Name, (object) this.StatusString)}\r\n\r\n{str}");
      }
      List<Task> list = this.Tasks.Where<Task>((System.Func<Task, bool>) (task => !task.IsProjectSummaryTask && task.Project == this)).ToList<Task>();
      this.StartTransaction();
      foreach (Task task in list)
        task.StartTransaction();
      try
      {
        if (Task._GlobalNotifier != null)
          Task._GlobalNotifier.Start();
        base.Save(session);
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          if (task.Modified && !task._Saved)
            task.Save(session);
        }
        foreach (Task task in list)
        {
          if (!task.Dependencies.AllSaved)
            task.Dependencies.Save(session, false);
        }
        if (this.Saved != null)
          this.Saved((object) this, (EventArgs) null);
        this.WasSaved = true;
        this.Commit();
        foreach (Task task in list)
          task.Commit();
        if (Task._GlobalNotifier != null)
        {
          this.StartProgress(0, Intermech.Project.Properties.Resources.WinUpdatingProgress);
          try
          {
            Task._GlobalNotifier.Commit();
          }
          finally
          {
            this.StopProgress();
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        if (Task._GlobalNotifier != null)
        {
          this.StartProgress(0, Intermech.Project.Properties.Resources.WinUpdatingProgress);
          try
          {
            Task._GlobalNotifier.Commit();
          }
          finally
          {
            this.StopProgress();
          }
        }
        throw;
      }
    }
    finally
    {
      this.StopProgress();
    }
  }

  internal override void Commit()
  {
    base.Commit();
    this.DeletedTasks.Clear();
    this.DeletedDependencies.Clear();
  }

  protected override void SaveObject(IUserSession session, [NotNull] IDBObject obj)
  {
    if (this.EditingMode.HasProperties())
    {
      IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar);
      if (attributeById == null && this.Schedule != Schedule.Standard)
        obj.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar, false, new object[1]
        {
          (object) this.Schedule.ObjectID
        });
      else if (attributeById != null)
        attributeById.AsInteger = this.Schedule.ObjectID;
    }
    IDBAttribute attributeById1 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ProjectData);
    XmlIni projectData = this.GetProjectData(attributeById1);
    this.SaveData(projectData);
    StreamHelper.SaveToBlobStream(attributeById1 as IBlobWriter, new ProcessStreamDelegate(projectData.Save), string.Empty);
    foreach (Task deletedTask in this.DeletedTasks)
    {
      if (!(deletedTask is Intermech.Project.Project))
        deletedTask.Delete(session);
    }
    foreach (Dependency deletedDependency in this.DeletedDependencies)
      deletedDependency.Delete(session);
    base.SaveObject(session, obj);
  }

  protected virtual void LoadData([NotNull] XmlIni ini) => this._Properties.Load(ini);

  protected virtual void SaveData([NotNull] XmlIni ini) => this._Properties.Save(ini);

  [NotNull]
  protected override TaskCollection ProjectTasks => base.ProjectTasks ?? this.Tasks;

  /// <summary>
  /// Вызывается после загрузки каждой задачи и после загрузки всего проекта
  /// После загрузки каждой задачи пытается найти зависимые от неё задачи и восстановить зависимость
  /// После полной загрузки проекта создает внешние задачи, где задача для зависимости найдена не была
  /// </summary>
  /// <param name="task"></param>
  private void ResolveDependencies([CanBeNull] Task task = null)
  {
    TaskCollection tasks = this.RootProject.Tasks;
    Intermech.Project.Project rootProject = this.RootProject;
    foreach (Task src in (System.Collections.ObjectModel.Collection<Task>) tasks)
    {
      bool flag1 = src.Dependencies._Modified;
      for (int index = src.Dependencies.Count - 1; index >= 0; --index)
      {
        Dependency dependency = src.Dependencies[index];
        if (!dependency.Resolved || dependency.External)
        {
          try
          {
            if (task != null)
            {
              if (Math.Abs(dependency.DependentOfTaskObjectID) == Math.Abs(task.ObjectID))
                dependency.DependentOfTask = task;
            }
            else
            {
              Task task1 = (Task) null;
              if (dependency._DependentOfTaskHash != 0L)
              {
                task1 = tasks.FindByHash(dependency._DependentOfTaskHash);
                dependency._DependentOfTaskHash = 0L;
              }
              else
              {
                if (dependency.DependentOfTaskObjectID != 0L)
                  task1 = tasks.FindByObjectID(dependency.DependentOfTaskObjectID);
                if (task1 == null)
                {
                  if (!dependency.External)
                  {
                    bool flag2 = false;
                    IUserSession session = this.GetSession();
                    try
                    {
                      IDBObject dbObject1 = session.GetObject(dependency.DependentOfTaskObjectID, false);
                      if (dbObject1 != null)
                      {
                        if (dbObject1.CheckoutBy == 0L)
                        {
                          IDBAttribute attributeById = dbObject1.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Project);
                          if (attributeById != null)
                          {
                            IDBObject dbObject2 = session.GetObject(attributeById.AsInteger, false);
                            if (dbObject2 != null)
                            {
                              if (dbObject2.CheckoutBy != 0L)
                                flag2 = true;
                            }
                          }
                        }
                      }
                    }
                    finally
                    {
                      this.ReleaseSession();
                    }
                    if (!flag2)
                      task1 = ExternalTask.Get(this._SessionProvider, dependency.DependentOfTaskObjectID);
                  }
                  else
                    continue;
                }
              }
              if (task1 == null)
              {
                src.Dependencies.RemoveAt(index);
                flag1 = true;
              }
              else
                dependency.DependentOfTask = task1;
            }
          }
          catch (Exception ex)
          {
            src.Dependencies.RemoveAt(index);
            flag1 = true;
            if (!rootProject.HandleError(src, ex))
              throw;
          }
        }
      }
      src.Dependencies._Modified = flag1;
      src.Modified |= flag1;
      if (flag1)
        rootProject._ModifiedWhileLoading = true;
    }
  }

  [field: NonSerialized]
  public event Intermech.Project.Project.TaskLoadedEventHandler TaskLoaded;

  public virtual void OnTaskLoaded([NotNull] Task task)
  {
    Intermech.Project.Project.TaskLoadedEventHandler taskLoaded = this.TaskLoaded;
    if (taskLoaded == null)
      return;
    taskLoaded(task);
  }

  [NotNull]
  public static List<int> GetApplicableResourceTypes([NotNull] IUserSession session)
  {
    return session.GetRelationsApplicabilityCollection().GetApplicabilitiesList((int) (IpsMetadataEntityBase<int>) RelationTypes.Resources, -1, (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task).Rows.Select<int>((System.Func<DataRow, int>) (row => row.FieldAsInt("F_OBJECT_TYPE"))).ToList<int>();
  }

  public void BeginUpdate()
  {
    ++this._updateCounter;
    Entity.GlobalBeginUpdate();
    this.SetState(TaskState.Loading);
  }

  public void EndUpdate()
  {
    --this._updateCounter;
    this.UnsetState(TaskState.Loading);
    Entity.GlobalEndUpdate();
    if (this._updateCounter != 0)
      return;
    this.DebugClearCache();
    this.Tasks.ResetBindings();
  }

  public bool InUpdate => this._updateCounter > 0;

  public bool ShowProjectTask
  {
    get => this._showProjectTask;
    set
    {
      if (this._showProjectTask == value)
        return;
      this._showProjectTask = value;
      this.Tasks.ListChanged -= new ListChangedEventHandler(this.Tasks_ListChanged);
      try
      {
        if (value)
        {
          this.IsProjectSummaryTask = true;
          this.Tasks.Insert(0, (Task) this);
        }
        else if (this.Tasks.Count > 0 && this.Tasks[0] == this)
        {
          this.Tasks.RemoveAt(0);
          this.IsProjectSummaryTask = false;
        }
        this.Tasks.ResetBindings();
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          task.OnPropertyChanged("Parent", false);
          task.OnPropertyChanged("IndentLevel", false);
        }
      }
      finally
      {
        this.Tasks.ListChanged += new ListChangedEventHandler(this.Tasks_ListChanged);
      }
    }
  }

  internal void ClearGraph() => this._projectGraph = (Task.Graph) null;

  internal override void OnPropertyChanged(string property, bool triggerModified)
  {
    bool flag = property == "Tasks";
    if (flag)
    {
      this.ClearGraph();
      if (this._Cache?.AllSubTasks != null)
        this._Cache.AllSubTasks = (IReadOnlyList<Task>) null;
    }
    if (this.IsProjectSummaryTask & flag)
      return;
    base.OnPropertyChanged(property, triggerModified);
  }

  public override double Units
  {
    get => 1.0;
    set => base.Units = value;
  }

  public override PlanningType PlanningType
  {
    get => this._planningType;
    set
    {
      if (this._planningType == value)
        return;
      this._planningType = value;
      this.OnPropertyChanged(nameof (PlanningType));
      this.RecalcStartFinish();
    }
  }

  public virtual void BeforeSetTaskProperty([NotNull] Task task, [NotNull, NotEmpty] string property, [CanBeNull] object value)
  {
  }

  public void TaskPropertyChangeCompleted([NotNull] Task task, [NotNull, NotEmpty] string property)
  {
    if (Entity.InGlobalUpdate || this.HasState(TaskState.Loading))
      return;
    Intermech.Project.Project project = task.RootProject ?? this;
    bool flag1 = project.LeftToRight ? ((IEnumerable<string>) Intermech.Project.Project._affectProcFinish).Contains<string>(property) : ((IEnumerable<string>) Intermech.Project.Project._affectProcStart).Contains<string>(property);
    bool flag2 = project.LeftToRight ? ((IEnumerable<string>) Intermech.Project.Project._affectProcStart).Contains<string>(property) : ((IEnumerable<string>) Intermech.Project.Project._affectProcFinish).Contains<string>(property);
    if (!project.ManualPlanning)
    {
      if (flag1)
        flag1 = this.CalcFinishChanged();
      if (flag2)
        flag2 = this.CalcStartChanged();
    }
    if (task is Intermech.Project.Project)
      flag2 = (flag1 |= flag2);
    if (!(flag1 | flag2))
      return;
    foreach (Task task1 in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
    {
      if (!task1.IsProjectSummaryTask)
      {
        bool flag3 = task1.ConstraintDate != DateTime.MinValue;
        if (flag1 && task1.ConstraintType == ConstraintType.AsLateAsPossible | flag3 || flag2 && task1.ConstraintType == ConstraintType.AsSoonAsPossible | flag3)
          task1.RecalcStartFinish();
      }
    }
  }

  public override bool LeftToRight => this.PlanningType == PlanningType.FromStart;

  public override void DebugClearCache()
  {
    base.DebugClearCache();
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
    {
      if (!task.IsProjectSummaryTask)
        task.DebugClearCache();
    }
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      task.PropertiesChanged(false);
    this.ClearGraph();
  }

  [NotNull]
  public string DebugPrintGraph() => this.ProjectGraph?.DebugPrint() ?? string.Empty;

  [NotNull]
  public string DebugGraphErrors() => this.ProjectGraph?.ErrorNodes ?? string.Empty;

  protected internal override void PropertiesChanged(Task.CalcProps props = Task.CalcProps.All, bool checkParent = true)
  {
    base.PropertiesChanged(props, checkParent);
    this.OnPropertyChanged("EstimatedDelay");
    this.OnPropertyChanged("EstimatedDelayString");
  }

  [CanBeNull]
  internal Task.Graph ProjectGraph
  {
    get
    {
      if (this.IsSubProject)
        return (Task.Graph) null;
      if (this._projectGraph == null && !this.HasNotLoadedSubTasks && !this.InUpdate)
        this._projectGraph = new Task.Graph(this);
      return this._projectGraph;
    }
  }

  public override ConstraintType ConstraintType
  {
    get
    {
      if (this.ManualPlanning)
        return ConstraintType.ManualPlanning;
      return this.PlanningType != PlanningType.FromEnd ? ConstraintType.AsSoonAsPossible : ConstraintType.AsLateAsPossible;
    }
    set
    {
      this.ManualPlanning = value == ConstraintType.ManualPlanning;
      base.ConstraintType = value;
    }
  }

  internal override bool LoadFromXml(
    [CanBeNull] XmlNode root,
    [CanBeNull] Intermech.Project.Project proj,
    [CanBeNull] Dictionary<Task, List<XmlPredecessor>> predecessors)
  {
    if (!(root.SelectSingleNode("mpp:Project", Intermech.Project.Project._namespaceManager) is XmlElement xmlElement1) || xmlElement1["Name"] == null)
      return false;
    this._showProjectTask = false;
    try
    {
      this.Loading();
      this.HasNotLoadedSubTasks = true;
      if (proj != null)
        this.Project = proj;
      this.Name = xmlElement1["Name"].InnerText.Replace(".xml", string.Empty);
      if (xmlElement1["ScheduleFromStart"] != null && xmlElement1["ScheduleFromStart"].InnerText == "0")
        this.PlanningType = PlanningType.FromEnd;
      else
        this.PlanningType = PlanningType.FromStart;
      if (this.LeftToRight)
        this.Start = MsProjectFuncs.StrToDateTime(Intermech.Diagnostics.Check.Optional.NotNull<XmlElement>(xmlElement1["StartDate"], "StartDate").InnerText);
      else
        this.Finish = MsProjectFuncs.StrToDateTime(Intermech.Diagnostics.Check.Optional.NotNull<XmlElement>(xmlElement1["FinishDate"], "FinishDate").InnerText);
      XmlNodeList xmlNodeList = xmlElement1.SelectNodes("mpp:Tasks/mpp:Task", Intermech.Project.Project._namespaceManager);
      this.StartProgress(xmlNodeList.Count, this.Name);
      string name = this.Name;
      Dictionary<string, Task> dictionary1;
      if (!Intermech.Project.Project._xmlUIDs.TryGetValue(name, out dictionary1))
      {
        dictionary1 = new Dictionary<string, Task>();
        Intermech.Project.Project._xmlUIDs.Add(name, dictionary1);
      }
      try
      {
        bool flag = true;
        foreach (XmlElement root1 in xmlNodeList)
        {
          if (flag)
          {
            flag = false;
          }
          else
          {
            XmlElement xmlElement2 = root1["IsSubproject"];
            Task task;
            if (xmlElement2 != null && xmlElement2.InnerText == "1")
            {
              task = (Task) new Intermech.Project.Project();
              if (proj != null)
                task.AssignProperties((Task) proj);
            }
            else
              task = new Task();
            if (task.LoadFromXml((XmlNode) root1, this, predecessors))
            {
              XmlElement xmlElement3 = root1["UID"];
              dictionary1.Add(xmlElement3.InnerText, task);
            }
            this.IncProgress();
          }
        }
        Dictionary<string, Resource> dictionary2 = new Dictionary<string, Resource>();
        IUserSession session = this.GetSession();
        try
        {
          foreach (XmlElement selectNode in xmlElement1.SelectNodes("mpp:Resources/mpp:Resource", Intermech.Project.Project._namespaceManager))
          {
            Resource resource = (Resource) null;
            string innerText = selectNode["UID"].InnerText;
            if (!dictionary2.ContainsKey(innerText))
            {
              string g = selectNode["ObjectGUID"]?.InnerText ?? string.Empty;
              if (g != string.Empty)
              {
                IDBObject dbObject = session.GetObject(new Guid(g), false);
                if (dbObject != null)
                  resource = new Resource((ISessionProvider) this, dbObject.ObjectID, dbObject.Caption, dbObject.ObjectType);
              }
              if (resource != null)
                dictionary2.Add(innerText, resource);
            }
          }
          foreach (XmlElement selectNode in xmlElement1.SelectNodes("mpp:Assignments/mpp:Assignment", Intermech.Project.Project._namespaceManager))
          {
            string innerText1 = selectNode["TaskUID"].InnerText;
            Task task;
            if (dictionary1.TryGetValue(innerText1, out task))
            {
              string innerText2 = selectNode["ResourceUID"].InnerText;
              Resource resource;
              if (dictionary2.TryGetValue(innerText2, out resource))
              {
                double result;
                double.TryParse(selectNode["Units"].InnerText, NumberStyles.Number, (IFormatProvider) CultureInfo.InvariantCulture, out result);
                Assignment assignment = new Assignment(resource, result);
                if (result == 0.0)
                  assignment.IsChief = true;
                task.Assignments.Add(assignment);
              }
            }
          }
        }
        finally
        {
          this.ReleaseSession();
        }
      }
      finally
      {
        this.StopProgress();
      }
      return true;
    }
    finally
    {
      this._HasNotLoadedSubTasks = false;
      this.Loaded();
    }
  }

  public void ImportFromMsProjectXml([NotNull] string fileName)
  {
    this.Loading();
    try
    {
      this.StartProgress(1, string.Empty);
      try
      {
        this.Tasks.Clear();
        XmlDocument root = new XmlDocument();
        root.Load(fileName);
        Intermech.Project.Project._namespaceManager = new XmlNamespaceManager(root.NameTable);
        Intermech.Project.Project._namespaceManager.AddNamespace("mpp", "http://schemas.microsoft.com/project");
        Dictionary<Task, List<XmlPredecessor>> predecessors = new Dictionary<Task, List<XmlPredecessor>>();
        Intermech.Project.Project._xmlUIDs = new Dictionary<string, Dictionary<string, Task>>();
        this.LoadFromXml((XmlNode) root, (Intermech.Project.Project) null, predecessors);
        foreach (KeyValuePair<Task, List<XmlPredecessor>> keyValuePair in predecessors)
        {
          Task key1;
          List<XmlPredecessor> xmlPredecessorList;
          keyValuePair.Deconstruct<Task, List<XmlPredecessor>>(out key1, out xmlPredecessorList);
          Task task = key1;
          foreach (XmlPredecessor xmlPredecessor in xmlPredecessorList)
          {
            string key2 = xmlPredecessor.ProjectName;
            if (key2 == string.Empty && task.Project != null)
              key2 = task.Project.Name;
            Dictionary<string, Task> dictionary;
            Task dependentOfTask;
            if (Intermech.Project.Project._xmlUIDs.TryGetValue(key2, out dictionary) && dictionary.TryGetValue(xmlPredecessor.UID, out dependentOfTask))
            {
              Dependency dependency = new Dependency(dependentOfTask, xmlPredecessor.Type);
              if (xmlPredecessor.Lag != 0.0)
              {
                dependency.Lag = xmlPredecessor.Lag;
                dependency.LagUnit = xmlPredecessor.LagUnit;
              }
              task.Dependencies.Add(dependency);
            }
          }
        }
      }
      finally
      {
        this.StopProgress();
      }
    }
    finally
    {
      this.Loaded();
    }
    this.OnPropertyChanged("Name");
  }

  internal override void SaveToXml(XmlTextWriter writer)
  {
    if (this.Project != null)
      this.SaveToXml(writer, true);
    writer.WriteStartElement(nameof (Project), "http://schemas.microsoft.com/project");
    writer.WriteElementString("Name", this.Name);
    writer.WriteElementString("StartDate", MsProjectFuncs.DateTimeToStr(this.Start));
    writer.WriteElementString("FinishDate", MsProjectFuncs.DateTimeToStr(this.Finish));
    writer.WriteElementString("ScheduleFromStart", this.LeftToRight ? "1" : "0");
    writer.WriteStartElement("Tasks");
    base.SaveToXml(writer);
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
    {
      if (!task.IsProjectSummaryTask && task.Project == this)
        task.SaveToXml(writer);
    }
    writer.WriteEndElement();
    if (this.Project == null)
    {
      writer.WriteStartElement("Resources");
      List<Resource> allResources = this.AllResources;
      int num1 = 1;
      IUserSession session = this.GetSession();
      try
      {
        foreach (Resource resource in allResources)
        {
          writer.WriteStartElement("Resource");
          writer.WriteElementString("UID", num1.ToString());
          writer.WriteElementString("ID", num1.ToString());
          writer.WriteElementString("Name", resource.ToString());
          writer.WriteElementString("Type", "1");
          IDBObject dbObject = session.GetObject(resource.ObjectID, false);
          if (dbObject != null)
            writer.WriteElementString("ObjectGUID", dbObject.ObjectGUID.ToString());
          writer.WriteEndElement();
          ++num1;
        }
      }
      finally
      {
        this.ReleaseSession();
      }
      writer.WriteEndElement();
      writer.WriteStartElement("Assignments");
      int num2 = 1;
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        if (!task.IsProjectSummaryTask)
        {
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task.Assignments)
          {
            writer.WriteStartElement("Assignment");
            writer.WriteElementString("UID", num2.ToString());
            writer.WriteElementString("TaskUID", task.UID.ToString());
            int num3 = allResources.IndexOf(assignment.Resource);
            writer.WriteElementString("ResourceUID", (num3 + 1).ToString());
            writer.WriteElementString("Units", assignment.Units.ToString((IFormatProvider) CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            ++num2;
          }
        }
      }
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
    if (this.Project == null)
      return;
    writer.WriteEndElement();
  }

  public void SaveToSimpleXml([NotNull] string filename)
  {
    using (XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
    {
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      Task.UIDCounter = 0;
      this.SaveToXml(writer);
    }
  }

  protected override void Loading()
  {
    Entity.GlobalBeginUpdate();
    base.Loading();
  }

  [field: NonSerialized]
  public event EventHandler OnLoaded;

  protected override void Loaded()
  {
    try
    {
      Entity.GlobalEndUpdate();
      base.Loaded();
      if (this.OnLoaded == null)
        return;
      this.OnLoaded((object) this, (EventArgs) null);
    }
    finally
    {
      if (!this.HasState(TaskState.Loading))
      {
        this.Tasks.ResetBindings();
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          task.OnPropertyChanged("IndentLevel", false);
          task.OnPropertyChanged("HasSubTasks", false);
        }
      }
    }
  }

  public bool AutoLoadSubTasks
  {
    [DebuggerStepThrough] get => this._AutoLoadSubTasks;
    [DebuggerStepThrough] set => this._AutoLoadSubTasks = value;
  }

  public bool AutoLoadSubProjects { get; set; }

  public void StartProgress([NotEmpty] int max, [NotNull] string msg, [CanBeNull] string caption)
  {
    if (this.ProgressNotifier == null)
      return;
    this.ProgressNotifier.Start(max, msg ?? string.Empty);
    if (caption == null)
      return;
    this.ProgressNotifier.Caption = caption;
  }

  public void StartProgress([NotEmpty] int max, [NotNull] string msg)
  {
    this.StartProgress(max, msg, (string) null);
  }

  public void IncProgress() => this.ProgressNotifier?.Inc();

  public virtual bool StopProgress()
  {
    IProgressNotifier progressNotifier = this.ProgressNotifier;
    return progressNotifier != null && progressNotifier.Stop();
  }

  [CanBeNull]
  public string ProgressCaption
  {
    get => this.ProgressNotifier?.Caption;
    set
    {
      if (this.ProgressNotifier == null)
        return;
      this.ProgressNotifier.Caption = value ?? string.Empty;
    }
  }

  public bool BreakOnError { get; set; }

  [NotNull]
  public string ErrorLog { [DebuggerStepThrough] get; private set; } = string.Empty;

  public virtual bool HandleError([NotNull] Task src, [NotNull] Exception e)
  {
    if (this.BreakOnError)
      throw e;
    if (this.ErrorLog != string.Empty)
      this.ErrorLog += "\r\n";
    this.ErrorLog = $"{this.ErrorLog}[{src.Name}] {e.Message}";
    this.ErrorLog = $"{this.ErrorLog}\r\n{e.StackTrace}\r\n";
    e.Source = src.Name;
    this._exceptionLog.Add(e);
    return true;
  }

  public void HandleSuspendedErrors()
  {
    if (this._exceptionLog.Count > 0)
    {
      CompositeException compositeException = new CompositeException(this._exceptionLog);
      this._exceptionLog = new List<Exception>();
      throw compositeException;
    }
  }

  public event Intermech.Project.Project.RequestEditHandler OnRequestEdit;

  public bool? RequestEdit([NotNull] Task task)
  {
    if (this.OnRequestEdit == null)
      return new bool?(false);
    bool? nullable1;
    bool? nullable2 = nullable1 = this.OnRequestEdit(task);
    bool flag = true;
    if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
      return nullable1;
    if (this.EditingMode == EditingMode.None)
      this.EditingMode = EditingMode.Edit;
    this.CheckOut();
    return nullable1;
  }

  protected internal override bool CheckOut(ref IDBObject obj)
  {
    this.CheckEditRights();
    return base.CheckOut(ref obj);
  }

  protected override bool DoCheckOut(ref IDBObject obj)
  {
    string progressCaption = this.ProgressCaption;
    this.ProgressCaption = Intermech.Project.Properties.Resources.CheckoutProgress;
    try
    {
      bool flag;
      if (this.RemoteStatus == RemoteProcessStatus.WaitingForPublish)
      {
        this.EditingMode = EditingMode.None;
        flag = false;
      }
      else
      {
        Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.SiteID);
        if ((int) siteId.Owner != (int) siteId.CurrentSite && (int) siteId.CompositionOwner == (int) siteId.CurrentSite)
        {
          (obj as IProject).CheckOutChildren();
          this._PseudoCheckedOut = true;
          this.EditingMode = EditingMode.Composition;
          this._WasCheckedOut = new bool?(false);
          flag = true;
        }
        else
        {
          if ((int) siteId.Owner != (int) siteId.CurrentSite)
            this.EditingMode &= ~EditingMode.Properties;
          if ((int) siteId.CompositionOwner != (int) siteId.CurrentSite)
            this.EditingMode &= ~EditingMode.Composition;
          flag = !this.EditingMode.ReadOnly() && base.DoCheckOut(ref obj);
        }
      }
      if ((!flag ? 0 : (this.EditingMode.HasComposition() ? 1 : 0)) != 0)
      {
        foreach (Task allSubTask in (IEnumerable<Task>) this.AllSubTasks)
        {
          if (!(allSubTask is Intermech.Project.Project) && allSubTask.Project == this)
          {
            if (allSubTask.ObjectID > 0L)
              allSubTask.HackObjectID = -allSubTask.ObjectID;
            allSubTask.EditingMode = EditingMode.Edit;
          }
        }
      }
      this.CheckOutPossible = !flag;
      return flag;
    }
    finally
    {
      this.ProgressCaption = progressCaption;
    }
  }

  public bool CheckOutPossible
  {
    get
    {
      if (this._ObjectID == 0L || this.PseudoCheckedOut || this.Status != TaskStatus.NotStarted)
        return false;
      if (!this._checkOutPossible.HasValue)
      {
        this.GetObject();
        try
        {
          this._checkOutPossible = new bool?(this._Object.CheckoutBy == 0L);
          if (this._checkOutPossible.Value)
          {
            Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.SiteID);
            if ((int) siteId.Owner != (int) siteId.CurrentSite)
            {
              if ((int) siteId.CompositionOwner != (int) siteId.CurrentSite)
                this._checkOutPossible = new bool?(false);
            }
          }
        }
        finally
        {
          this.ReleaseObject();
        }
      }
      return this._checkOutPossible.Value;
    }
    set
    {
      bool? checkOutPossible = this._checkOutPossible;
      bool flag = value;
      if (checkOutPossible.GetValueOrDefault() == flag & checkOutPossible.HasValue)
        return;
      this._checkOutPossible = new bool?(value);
      this._checkInPossible = new bool?(!value);
      this.RootProject.UpdateControls();
    }
  }

  public bool CheckInPossible
  {
    get
    {
      if (this._ObjectID == 0L || this.Status != TaskStatus.NotStarted)
        return false;
      if (!this._checkInPossible.HasValue)
      {
        this._checkInPossible = new bool?(this.PseudoCheckedOut);
        if (!this._checkInPossible.Value)
        {
          this.GetObject();
          try
          {
            this._checkInPossible = new bool?(this._Object.CheckoutBy == this.CurrentUserObjectID);
          }
          finally
          {
            this.ReleaseObject();
          }
        }
      }
      return this._checkInPossible.Value;
    }
  }

  [CanBeNull]
  internal Delegate[] RequestEditHandlers => this.OnRequestEdit?.GetInvocationList();

  /// <summary>
  /// Реализация раскрытия неподгруженных задач в редакторе, требуется при вставке задач в нераскрытый проект
  /// </summary>
  public event Intermech.Project.Project.TaskRequestHandler OnRequestExpand;

  public void RequestExpand([NotNull] Task task)
  {
    if (this.OnRequestExpand == null)
      throw new Exception("OnRequestExpand required!");
    this.OnRequestExpand(task);
  }

  public override void AssignProperties(Task srcTask)
  {
    base.AssignProperties(srcTask);
    if (!(srcTask is Intermech.Project.Project project))
      return;
    Intermech.Project.Project.RequestEditHandler onRequestEdit = project.OnRequestEdit;
    if (onRequestEdit != null)
    {
      foreach (Intermech.Project.Project.RequestEditHandler invocation in onRequestEdit.GetInvocationList())
        this.OnRequestEdit += invocation;
    }
    this._AutoLoadSubTasks = project.AutoLoadSubTasks;
  }

  private static void ValidateSiteCode(char code, [NotNull] ref string s)
  {
    if (RemoteSettings.SiteSchemes.ContainsKey(code))
      return;
    if (s != string.Empty)
      s += "\r\n";
    s += $"Шаблон публикации проектов для узла с кодом '{code}' не задан!";
  }

  [NotNull]
  public override string Validate(bool executing = false)
  {
    this.LoadSubTasks();
    string s = base.Validate(executing);
    if (executing && this.ManualPlanning)
    {
      if (s != string.Empty)
        s += "\r\n";
      s += "Режим ручного планирования предназначен только для проработки проектов, для запуска проекта его режим планирования должен быть переключен в автоматический.";
    }
    if (this.ObjectID != 0L && this.PendingSiteID != string.Empty)
    {
      Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.PendingSiteID);
      if ((int) siteId.Owner != (int) siteId.CurrentSite)
        Intermech.Project.Project.ValidateSiteCode(siteId.Owner, ref s);
      if ((int) siteId.CompositionOwner != (int) siteId.CurrentSite && (int) siteId.CompositionOwner != (int) siteId.Owner)
        Intermech.Project.Project.ValidateSiteCode(siteId.CompositionOwner, ref s);
    }
    if (s != string.Empty)
    {
      string str = new Regex("(\r\n)+", RegexOptions.Singleline).Replace(s, "$0   ");
      s = $"{string.Format(Intermech.Project.Properties.Resources.ProjectValidationErrors, (object) this.Name)}\r\n   {str}";
    }
    return s;
  }

  public override void Execute()
  {
    if (this.IsExecuted || this.IsCompleted)
      return;
    IUserSession session = this.GetSession();
    try
    {
      if (!this.IsSubProject)
      {
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          if (!task.IsProjectSummaryTask && task is Intermech.Project.Project project)
            project.CheckIn();
        }
      }
      session.DBObjectsCacheStart();
      try
      {
        this.GetObject();
        this.SetState(TaskState.Starting);
        try
        {
          if (!this.IsSubProject && session.UserID != Intermech.Metadata.User.System.ID)
            this.CheckChiefOnly();
          string message = this.Validate(true);
          if (message != string.Empty)
            throw new NotificationException(message);
          IDBTransactions customService = this._Object.Session.GetCustomService<IDBTransactions>();
          customService.StartTransaction();
          if (Task._GlobalNotifier != null)
            Task._GlobalNotifier.Start();
          try
          {
            char remoteSiteCode = this.RemoteSiteCode;
            this._RemoteExec = remoteSiteCode != ' ' && this.ChiefID != this.CurrentUserObjectID;
            if (this._RemoteExec)
            {
              if (!Portal.Enabled)
                throw new NotificationException(string.Format(Intermech.Project.Properties.Resources.ErrPortalNeededForExec, (object) this.Name, (object) remoteSiteCode));
              base.Execute();
              if (this.Status == TaskStatus.Waiting)
                (this._Object as IProject).Execute();
            }
            else
            {
              List<Task> collection = new List<Task>();
              foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
              {
                if (!task.IsProjectSummaryTask && task.Project == this && task.Dependencies.Count <= 0)
                  collection.SafeAdd<Task>(task);
              }
              base.Execute();
              TaskCollection taskCollection = new TaskCollection();
              taskCollection.Assign((IEnumerable<Task>) this.Tasks);
              foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) taskCollection)
              {
                if (!task.IsProjectSummaryTask && task.Project == this && (task.Status == TaskStatus.NotStarted || task.Status == TaskStatus.Terminated))
                {
                  task.CheckIn();
                  task.Status = TaskStatus.Waiting;
                }
              }
              foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) taskCollection)
              {
                if (!task.IsProjectSummaryTask && task.Project == this && (this._Properties.TaskStartingMode == TaskStartingMode.StartWithProject || collection.Contains(task)) && (!task.Milestone || collection.Contains(task)))
                  task.Execute();
              }
            }
            customService.Commit();
            if (Task._GlobalNotifier != null)
              Task._GlobalNotifier.Commit();
          }
          catch
          {
            customService.Rollback();
            if (Task._GlobalNotifier != null)
              Task._GlobalNotifier.Rollback();
            throw;
          }
        }
        finally
        {
          this.UnsetState(TaskState.Starting);
          this.ReleaseObject();
        }
        if (!this.HasState(TaskState.MailRefreshNeeded))
          return;
        this.UnsetState(TaskState.MailRefreshNeeded);
        this.RefreshMail();
      }
      finally
      {
        session.DBObjectsCacheStop();
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  public override void Abort()
  {
    IUserSession session = this.GetSession();
    try
    {
      if (!this.IsSubProject)
        this.CheckChiefOnly();
      IDBTransactions customService = session.GetCustomService<IDBTransactions>();
      customService.StartTransaction();
      if (Task._GlobalNotifier != null)
        Task._GlobalNotifier.Start();
      try
      {
        base.Abort();
        this.DeleteNotifications();
        this.RefreshMail();
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          if (!task.IsProjectSummaryTask)
            task.Abort();
        }
        customService.Commit();
        if (Task._GlobalNotifier == null)
          return;
        Task._GlobalNotifier.Commit();
      }
      catch
      {
        customService.Rollback();
        if (Task._GlobalNotifier != null)
          Task._GlobalNotifier.Rollback();
        throw;
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  [NotNull]
  [MustUseReturnValue]
  public IDisposable LockRefreshMailBlock()
  {
    ++this._lockRefreshMail;
    return (IDisposable) new CallOnDispose((Action) (() =>
    {
      if (--this._lockRefreshMail != 0 || !this.HasState(TaskState.MailRefreshNeeded))
        return;
      this.RefreshMail();
    }));
  }

  internal void RefreshMail()
  {
    if (this.HasState(TaskState.Starting) || this._lockRefreshMail > 0)
    {
      if (this.HasState(TaskState.MailRefreshNeeded))
        return;
      this.SetState(TaskState.MailRefreshNeeded);
    }
    else
      this.DoNotification(Task.EventKind.RefreshMail, 0L);
  }

  public bool VerifyTaskCompleted([NotNull] Task task)
  {
    if (task.Milestone || task.HasSubTasks || !this._Properties.RequireTaskVerification || task.Status != TaskStatus.Executed || this.IsChief)
      return true;
    task.Status = TaskStatus.Pending;
    return false;
  }

  internal void SendTaskNotification([NotNull] Task task, TaskStatus prevStatus, [NotNull, NotEmpty] long[] userIDs)
  {
    string format = Intermech.Project.Properties.Resources.TaskMailTemplate;
    string str = string.Empty;
    string subject;
    if (prevStatus == TaskStatus.Pending)
    {
      subject = string.Format(Intermech.Project.Properties.Resources.TaskVerifyRejected, (object) task.Name);
      str = $"<b>{Intermech.Project.Properties.Resources.OnceAgain}</b> ";
      string managerAnswer = task.ManagerAnswer;
      if (!string.IsNullOrWhiteSpace(managerAnswer))
        format = format + Environment.NewLine + string.Format(Intermech.Project.Properties.Resources.ManagerAnswer, (object) managerAnswer);
    }
    else
      subject = string.Format(Intermech.Project.Properties.Resources.TaskMailSubject, (object) task.Name);
    task.SendNotification(subject, string.Format(format, (object) task.ObjectID, (object) task.NameInMessages, (object) this.ObjectID, (object) this.NameInMessages, (object) this.ChiefID, (object) this.GetUserName(this.ChiefID), (object) str), userIDs);
  }

  internal void OnTaskStatusChanged([NotNull] Task task, TaskStatus status, TaskStatus prevStatus)
  {
    bool flag = task is Intermech.Project.Project;
    switch (status)
    {
      case TaskStatus.Sent:
      case TaskStatus.Executed:
        if (this.Status == TaskStatus.Sent && status == TaskStatus.Executed)
          this.Status = TaskStatus.Executed;
        if (task.Milestone)
          break;
        if (!flag)
        {
          if (status != TaskStatus.Executed || prevStatus != TaskStatus.Sent)
            task.DeleteNotifications();
          this.SendTaskNotification(task, prevStatus, task.Assignments.UserIDs.ToArray());
        }
        task.RegisterUncompletedTimer();
        break;
      case TaskStatus.Pending:
        if (flag)
          break;
        task.DeleteNotifications();
        task.SendNotification(string.Format(Intermech.Project.Properties.Resources.PendingTaskMailSubject, (object) task.Name), string.Format(Intermech.Project.Properties.Resources.PendingTaskMailTemplate, (object) task.Assignments.UserNamesString, (object) task.ObjectID, (object) task.NameInMessages, (object) this.ObjectID, (object) this.NameInMessages), new long[1]
        {
          task.ChiefID
        });
        break;
      case TaskStatus.Completed:
      case TaskStatus.Terminated:
        task.DeleteUncompletedTimer();
        if (status == TaskStatus.Terminated || flag)
          break;
        task.DeleteNotifications();
        this.RefreshMail();
        break;
    }
  }

  /// <summary>
  /// Загружает основные свойства проекта, вне зависимости, открыт проект в режиме редактирования, или нет
  /// </summary>
  /// <param name="obj">Откуда читать свойства</param>
  public void LoadProperties([NotNull] IDBObject obj)
  {
    this.SetState(TaskState.Copying);
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      task.SetState(TaskState.Copying);
    try
    {
      this.LoadMajorProperties(obj, (DataRow) null);
      this.LoadObject(obj, (DataRow) null);
    }
    finally
    {
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        task.UnsetState(TaskState.Copying);
      this.UnsetState(TaskState.Copying);
    }
  }

  public void CopyTo(long objectID)
  {
    this.SetState(TaskState.Copying);
    try
    {
      this._ObjectID = objectID;
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
        assignment.HackRelationID = 0L;
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        task.HackObjectID = 0L;
        task.SetState(TaskState.Copying);
      }
      this.Save();
      this.CheckIn();
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        task.UnsetState(TaskState.Copying);
    }
    finally
    {
      this.UnsetState(TaskState.Copying);
    }
  }

  internal override void RecalcStartFinish()
  {
    this.TaskPropertyChangeCompleted((Task) this, "Start");
  }

  public bool IsSubProject => this.Project != null;

  public virtual void Clear() => this.Tasks.Clear();

  protected override void LoadSubTasksInternal(IUserSession session, Intermech.Project.Project proj)
  {
    try
    {
      base.LoadSubTasksInternal(session, proj);
    }
    finally
    {
      this.ResolveDependencies();
    }
  }

  /// <summary>
  /// Заканчивает редактирование объекта
  /// Если до начала редактирования объект не был взят на изменение, то в зависимости от CancelChanges
  /// Или отменяет изменения, или сдает изменения
  /// </summary>
  /// <param name="cancelChanges"></param>
  public void EndEdit(bool cancelChanges)
  {
    if (this.ObjectID != 0L && this.EditingMode.Any())
    {
      this.GetObject(false);
      try
      {
        if (this._Object != null)
        {
          if (!this.PseudoCheckedOut)
          {
            if (!this.WasCheckedOut)
            {
              if (this._Object.CheckoutBy != this.CurrentUserObjectID)
                goto label_14;
            }
            else
              goto label_14;
          }
          this.StartProgress(1, this.Name, Intermech.Project.Properties.Resources.ClosingProgress);
          try
          {
            if (!this.WasSaved & cancelChanges && !this.PseudoCheckedOut)
            {
              this._Object.CancelChanges();
              this.DoNotification(Task.EventKind.CancelChanges, this.ObjectID);
            }
            else
            {
              int num = this.PseudoCheckedOut ? 1 : 0;
              this.CheckIn();
              if (num == 0)
                this.DoNotification(Task.EventKind.CheckIn, this.ObjectID);
            }
            this._ObjectID = -this.ObjectID;
          }
          finally
          {
            this.StopProgress();
          }
        }
      }
      finally
      {
        this.ReleaseObject();
      }
    }
label_14:
    foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
    {
      if (subTask is Intermech.Project.Project project)
        project.EndEdit(cancelChanges);
    }
  }

  /// <summary>
  /// Перерисовать связанные контролы, если имеются (таблица, диаграмма Гантта)
  /// </summary>
  public virtual void UpdateControls()
  {
  }

  public void InsertTasks(int pos, [NotNull] Task[] tasks)
  {
    this._copiedRootIndentDXs = new Dictionary<Task, int>();
    this._currentCopiedRoot = (Task) null;
    try
    {
      int num = 9999;
      foreach (Task task in tasks)
      {
        if (task.IndentLevel <= num)
          this._copiedRootIndentDXs.Add(task, 9999);
      }
      foreach (Task task in tasks)
        this.Tasks.Insert(pos++, task);
      this.ResolveDependencies();
    }
    finally
    {
      this._copiedRootIndentDXs = (Dictionary<Task, int>) null;
      this._currentCopiedRoot = (Task) null;
    }
  }

  [NotNull]
  public Intermech.Project.Project InsertProject(long objectID, int index)
  {
    TaskCollection tasks = this.RootProject.Tasks;
    long id = objectID != this.ObjectID ? objectID : throw new NotificationException(Intermech.Project.Properties.Resources.ErrCircularSubProject);
    if (tasks.FindByObjectID(id) != null)
      throw new NotificationException(Intermech.Project.Properties.Resources.SubProjectAlreadyExists);
    Intermech.Project.Project project = new Intermech.Project.Project();
    project.AssignProperties((Task) this);
    project.AutoLoadSubTasks = false;
    this.Tasks.Insert(index, (Task) project);
    project._UseBulkData = false;
    project.Load(objectID, new bool?());
    project.AutoLoadSubTasks = true;
    project.PropertiesChanged(Task.CalcProps.All, true);
    return project;
  }

  public void RemoveTasks([NotNull, ItemNotNull] IEnumerable<Task> tasks)
  {
    bool flag = false;
    HashSet<Task> taskSet = new HashSet<Task>();
    Entity.GlobalBeginUpdate();
    try
    {
      foreach (Task task in tasks)
      {
        Task parent = task.Parent;
        if (parent != null)
          taskSet.Add(parent);
        if (this.Tasks.Remove(task))
          flag = true;
      }
    }
    finally
    {
      Entity.GlobalEndUpdate();
      HashSet<Task> processed = new HashSet<Task>();
      foreach (Task task in taskSet)
        task.PropertiesChanged(Task.CalcProps.Dependencies, false, processed);
      if (flag && !this.Modified)
        this.Modified = true;
    }
  }

  [CanBeNull]
  public TaskFilter Filter
  {
    get => this._filter;
    set
    {
      this._FilterError = string.Empty;
      this._filter = value;
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        task.ClearHidden();
    }
  }

  [NotNull]
  [ItemNotNull]
  public List<Resource> AllResources
  {
    get
    {
      List<Resource> allResources = new List<Resource>();
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
      {
        foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) task.Assignments)
        {
          if (!allResources.Contains(assignment.Resource))
            allResources.Add(assignment.Resource);
        }
      }
      return allResources;
    }
  }

  public override long InheritedChiefID => this.OwnerID;

  public override string AssignmentsString
  {
    get
    {
      if (this.UseCache && this._Cache?.AssignmentsString != null)
        return this._Cache.AssignmentsString;
      if (this.Milestone)
        return string.Empty;
      string assignmentsString = this.ChiefID != 0L ? this.GetUserName(this.ChiefID) ?? string.Empty : string.Empty;
      this.Cache.AssignmentsString = assignmentsString;
      return assignmentsString;
    }
  }

  public void SaveMinimizedTasks([NotNull] Intermech.Project.Project.TasksStateSet set)
  {
    set.Clear();
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
    {
      if (task is Intermech.Project.Project && !task.HasNotLoadedSubTasks)
        set._LoadedProjects.Add(task.ObjectID);
      if (task.Minimized)
        set._Minimized.Add(task.ObjectID);
    }
  }

  public void RestoreMinimizedTasks([NotNull] Intermech.Project.Project.TasksStateSet set)
  {
    foreach (Task task in this.Tasks.ToList<Task>(this.Tasks.Count))
    {
      if (task is Intermech.Project.Project && set._LoadedProjects.Contains(task.ObjectID))
        task.LoadSubTasks();
      if (set._Minimized.Contains(task.ObjectID))
        task.Minimized = true;
    }
  }

  /// <summary>Получить дескриптор импортированного в проект объекта по идентификатору объекта</summary>
  [CanBeNull]
  public ImportedObject GetImportedObjectDescriptor(
    [NotEmpty] long objectVersionID,
    bool throwExceptionOnNotFound = true)
  {
    objectVersionID = Math.Abs(objectVersionID);
    ImportedObject importedObject1 = this.ImportedObjects.FirstOrDefault<ImportedObject>((System.Func<ImportedObject, bool>) (importedObject => importedObject.ObjectVersionID == objectVersionID));
    return !throwExceptionOnNotFound || importedObject1 != null ? importedObject1 : throw new ObjectVersionNotFoundException(objectVersionID);
  }

  /// <summary>Список дескрипторов импортированных в проект объектов
  /// Список импортированных объектов показывается при вызове команды "синхронизация с составом объекта"</summary>
  [NotNull]
  [ItemNotNull]
  public IList<ImportedObject> ImportedObjects
  {
    get
    {
      return (IList<ImportedObject>) this._importedObjects ?? (IList<ImportedObject>) (this._importedObjects = this.CreateImportedObjectsFromDb());
    }
  }

  /// <summary>Создание списка идентификаторов версий всех настроек импорта импортированных в проект объектов</summary>
  [NotNull]
  private MutableCollection<ImportedObject> CreateImportedObjectsFromDb()
  {
    if (this._ObjectID == 0L)
      return new MutableCollection<ImportedObject>();
    IUserSession session = this.GetSession();
    MutableCollection<ImportedObject> importedObjectsFromDb;
    try
    {
      importedObjectsFromDb = new MutableCollection<ImportedObject>((IEnumerable<ImportedObject>) session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.ImportedObjects).ConsistOf(this.ObjectID, DB.Columns(DB.RelationAttribute((int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject), DB.ObjectAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.IterationID), Intermech.Metadata.Attributes.PrjLinkID.RelationColumn)).Rows.Select<ImportedObject>((System.Func<DataRow, ImportedObject>) (dataRow => new ImportedObject(this, dataRow.FieldAsLong(0), dataRow.FieldAsLongDef(1), dataRow.FieldAsLong(2)))));
      importedObjectsFromDb.CollectionChangedFirstTime += new NotifyCollectionChangedEventHandler(this.ImportedObjects_CollectionChangedFirstTime);
    }
    finally
    {
      this.ReleaseSession();
    }
    if (importedObjectsFromDb.Count > 0)
    {
      for (int index = importedObjectsFromDb.Count - 1; index >= 0; --index)
      {
        ImportedObject checkImportedObject = importedObjectsFromDb[index];
        if (!this.Tasks.Any<Task>((System.Func<Task, bool>) (task => task.ImportedRootObjectVersionGuid.Equals(checkImportedObject.ObjectVersionGuid))))
          importedObjectsFromDb.RemoveAt(index);
      }
    }
    return importedObjectsFromDb;
  }

  /// <summary>Добавление в список дескрипторов импортированных в проект объектов новой записи</summary>
  public void AddImportedObjectInfo(
    [NotEmpty] long objectVersionID,
    long objectIterationID,
    [NotNull] ImportObjectSettings importSettings)
  {
    objectVersionID = Math.Abs(objectVersionID);
    Intermech.Diagnostics.Check.All<ImportedObject>((IEnumerable<ImportedObject>) this.ImportedObjects, "ImportedObjects", (System.Func<ImportedObject, bool>) (importedObject => importedObject.ObjectVersionID != objectVersionID));
    this.ImportedObjects.Add(new ImportedObject(this, objectVersionID, objectIterationID, importSettings));
  }

  /// <summary>Обработчик события изменения списка идентификаторов версий всех настроек импорта импортированных в проект объектов</summary>
  private void ImportedObjects_CollectionChangedFirstTime(
    [CanBeNull] object sender,
    [NotNull] NotifyCollectionChangedEventArgs e)
  {
    this.SaveDbObjectAttributes += new Task.SaveDbObjectAttributesDelegate(this.SaveRelationsWithImportedObjectsToDb);
  }

  /// <summary>Сохранение в БД изменений списка идентификаторов версий всех настроек импорта импортированных в проект объектов</summary>
  private void SaveRelationsWithImportedObjectsToDb([NotNull] IDBObject projectDbObject)
  {
    this.SaveDbObjectAttributes -= new Task.SaveDbObjectAttributesDelegate(this.SaveRelationsWithImportedObjectsToDb);
    IUserSession session = projectDbObject.Session;
    this._importedObjects.WasChanged = false;
    IDBRelationCollection relationCollection = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.ImportedObjects);
    long objectId = projectDbObject.ObjectID;
    DataTable dataTable1;
    if (this._ObjectID == 0L)
      dataTable1 = (DataTable) null;
    else
      dataTable1 = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -20
      }), this._ObjectID);
    DataTable dataTable2 = dataTable1;
    if (dataTable2 != null && dataTable2.Rows.Count > 0)
    {
      IEnumerable<DataRow> source = dataTable2.Rows.Cast<DataRow>();
      long[] array = (this._importedObjects.Count > 0 ? source.Where<DataRow>((System.Func<DataRow, bool>) (row => this._importedObjects.All<ImportedObject>((System.Func<ImportedObject, bool>) (importedObject => importedObject.RelationID != row.FieldAsLong(0))))) : source).Select<DataRow, long>((System.Func<DataRow, long>) (row => row.FieldAsLong(0))).ToArray<long>();
      if (array.Length != 0)
        relationCollection.Delete(array, false, 0L);
    }
    for (int index = this._importedObjects.Count - 1; index >= 0; --index)
    {
      ImportedObject importedObject = this._importedObjects[index];
      if (importedObject.RelationID == 0L)
      {
        if (!session.GetObjectInfo(importedObject.ObjectVersionID).Empty)
        {
          IDBRelation relation = relationCollection.Create(objectId, importedObject.ObjectVersionID, new AttributeValues[4]
          {
            new AttributeValues((int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject, (object) importedObject.ObjectVersionID),
            new AttributeValues((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.IterationID, (object) importedObject.ObjectIterationID),
            new AttributeValues((int) (IpsMetadataEntityBase<int>) Attributes.LastSyncDate, (object) DateTime.Now),
            new AttributeValues((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data)
          });
          importedObject.ImportSettings.SaveToDbRelation(relation);
          importedObject.RelationID = relation.RelationID;
        }
        else
          this._importedObjects.RemoveAt(index);
      }
    }
  }

  /// <summary>Удаление дескриптора импортированного объекта из списка дескрипторов объектов, импортированных в проект</summary>
  public void RemoveConnectionWithImportedObject(
    [NotEmpty] long objectVersionID,
    bool deleteTasksImportedFromObject,
    bool throwExceptionOnNotFound = true)
  {
    ImportedObject objectDescriptor = this.GetImportedObjectDescriptor(objectVersionID, throwExceptionOnNotFound);
    if (objectDescriptor == null)
      return;
    Guid deletedObjectGuid = this.InvokeSession<Guid>((Session.SessionHandler<Guid>) (session => session.GetObjectInfo(objectVersionID).VersionGuid));
    if (!deletedObjectGuid.Equals(Guid.Empty) & deleteTasksImportedFromObject)
      this.RemoveTasks((IEnumerable<Task>) this.Tasks.Where<Task>((System.Func<Task, bool>) (task => task.ImportedRootObjectVersionGuid.Equals(deletedObjectGuid))).ToList<Task>());
    this.ImportedObjects.Remove(objectDescriptor);
    this.Modified = true;
  }

  /// <summary>Поиск задачи импортированной в проект в контексте корневого объекта по глобальному идентификатору входимости (связи) объекта</summary>
  /// <param name="importedRootObjectVersionGuid">Глобальный идентификатор корневого импортированного объекта</param>
  /// <param name="objectCompositionRelationGuid">Глобальный идентификатор входимости (связи) объекта</param>
  /// <returns>Импортированная задача</returns>
  public bool IsRelationWasImportedAsTask(
    Guid importedRootObjectVersionGuid,
    Guid objectCompositionRelationGuid)
  {
    return this.GetTaskImportedByRelation(importedRootObjectVersionGuid, objectCompositionRelationGuid) != null;
  }

  /// <summary>Поиск задачи импортированной в проект в контексте корневого объекта по глобальному идентификатору входимости (связи) объекта</summary>
  /// <param name="importedRootObjectVersionGuid">Глобальный идентификатор корневого импортированного объекта</param>
  /// <param name="objectCompositionRelationGuid">Глобальный идентификатор входимости (связи) объекта</param>
  /// <returns>Импортированная задача</returns>
  [CanBeNull]
  public Task GetTaskImportedByRelation(
    [NotEmpty] Guid importedRootObjectVersionGuid,
    Guid objectCompositionRelationGuid)
  {
    return this.Tasks.FirstOrDefault<Task>((System.Func<Task, bool>) (task => task.ImportedRootObjectVersionGuid.Equals(importedRootObjectVersionGuid) && task.ImportedRelationGuid.Equals(objectCompositionRelationGuid)));
  }

  /// <summary>Поиск задачи импортированной в проект в контексте корневого объекта по идентификатору версии объекта</summary>
  /// <param name="importedRootObjectVersionGuid">Глобальный идентификатор корневого импортированного объекта</param>
  /// <param name="objectVersion">Идентификатор импортированного объекта</param>
  /// <returns>Импортированная задача</returns>
  public bool IsObjectWasImportedAsTask(Guid importedRootObjectVersionGuid, long objectVersion)
  {
    return this.GetTaskImportedByObject(importedRootObjectVersionGuid, objectVersion) != null;
  }

  /// <summary>Поиск задачи импортированной в проект в контексте корневого объекта по идентификатору версии объекта</summary>
  /// <param name="importedRootObjectVersionGuid">Глобальный идентификатор корневого импортированного объекта</param>
  /// <param name="objectVersionID">Идентификатор импортированного объекта</param>
  /// <returns>Импортированная задача</returns>
  [CanBeNull]
  public Task GetTaskImportedByObject([NotEmpty] Guid importedRootObjectVersionGuid, [NotEmpty] long objectVersionID)
  {
    return this.Tasks.FirstOrDefault<Task>((System.Func<Task, bool>) (task => task.ImportedObjectVersionID == Math.Abs(objectVersionID) && task.ImportedRootObjectVersionGuid.Equals(importedRootObjectVersionGuid)));
  }

  /// <summary>Перечисление задач, импортированных из определённого объекта</summary>
  [NotNull]
  public Intermech.Project.Project.TasksImportedFromObjectClass TasksImportedFromObject
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tasksImportedFromObject ?? (this._tasksImportedFromObject = new Intermech.Project.Project.TasksImportedFromObjectClass(this));
    }
  }

  public override bool ManualPlanning
  {
    get => this._ManualPlanning;
    set
    {
      if (this._ManualPlanning == value)
        return;
      if (!value)
        this.LoadSubTasks();
      this._ManualPlanning = value;
      this.OnPropertyChanged(nameof (ManualPlanning));
      this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies | Task.CalcProps.ClearGraph, true);
      this.RecalcStartFinish();
    }
  }

  [NotNull]
  [ItemNotNull]
  internal IReadOnlyList<Task> SubTasksForGraph
  {
    get
    {
      if (!this.ManualPlanning)
        return this.SubTasks;
      if (!(this.SubTasks is List<Task> subTasksForGraph))
        subTasksForGraph = this.SubTasks.AsList<Task>();
      subTasksForGraph.Insert(0, (Task) this);
      return (IReadOnlyList<Task>) subTasksForGraph;
    }
  }

  protected override DateTime SubtasksStart
  {
    get
    {
      DateTime subtasksStart = base.SubtasksStart;
      if (this.ManualPlanning && (this.Start < subtasksStart || subtasksStart == DateTime.MinValue))
        subtasksStart = this.Start;
      return subtasksStart;
    }
  }

  protected override DateTime SubtasksFinish
  {
    get
    {
      DateTime subtasksFinish = base.SubtasksFinish;
      if (this.ManualPlanning && this.Finish > subtasksFinish)
        subtasksFinish = this.Finish;
      return subtasksFinish;
    }
  }

  public override bool PlanningConflict
  {
    get => base.PlanningConflict;
    protected set
    {
      if (this.PlanningConflict == value)
        return;
      this._PlanningConflict = value;
      this.OnPropertyChanged(nameof (PlanningConflict), false);
    }
  }

  /// <summary>Идентификаторы узлов портала, временное значение. Если заполнено, означает, что синхронизация ещё не была выполнена</summary>
  [CanBeNull]
  public string PendingSiteID
  {
    get
    {
      if (this._pendingSiteID == null)
      {
        this._pendingSiteID = this.ProjectData.ReadString("Pending", "SiteID");
        if (this.SiteID == this.PendingSiteID)
          this.PendingSiteID = string.Empty;
        if (this.RemoteStatus == RemoteProcessStatus.WaitingForPublish)
          this._pendingSiteID = string.Empty;
      }
      return this._pendingSiteID;
    }
    set
    {
      value = value ?? string.Empty;
      bool flag = value == " ";
      value = value.Trim();
      if (!(this._pendingSiteID != value))
        return;
      this._pendingSiteID = value;
      this.ProjectData.WriteString("Pending", "SiteID", value);
      this.OnPropertyChanged(nameof (PendingSiteID), value != string.Empty | flag);
    }
  }

  /// <summary>Идентификаторы узлов портала</summary>
  [CanBeNull]
  public override string SiteID
  {
    get => base.SiteID;
    set
    {
      if (!(this.SiteID != value))
        return;
      this.PendingSiteID = value;
      this.OnPropertyChanged(nameof (SiteID));
    }
  }

  [CanBeNull]
  public string CurrentSiteID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !(this.PendingSiteID != string.Empty) ? this.SiteID : this.PendingSiteID;
    }
  }

  public bool SyncPending => this.PendingSiteID != string.Empty;

  public override bool CheckIn(bool throwNotFoundException)
  {
    this.GetObject(throwNotFoundException);
    try
    {
      bool flag = false;
      if (this._Object != null)
      {
        Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.SiteID);
        if ((int) siteId.Owner != (int) siteId.CurrentSite && (int) siteId.CompositionOwner == (int) siteId.CurrentSite)
        {
          (this._Object as IProject).CheckInChildren();
          this._PseudoCheckedOut = false;
          flag = true;
        }
        else
          flag = base.CheckIn(throwNotFoundException);
        if (flag)
        {
          this.EditingMode = EditingMode.None;
          foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
            task.EditingMode = EditingMode.None;
          this.CheckOutPossible = true;
        }
      }
      return flag;
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Синхронизирует проект с порталом. Для подпроектов запускает удаленные процессы, если узлы (владельцы свойств и состава) отличаются от текущего</summary>
  public void Sync()
  {
    this.StartProgress(0, string.Empty);
    try
    {
      if (!this.Save())
        return;
      string message = this.Validate(false);
      if (message != string.Empty)
        throw new NotificationException(message);
      List<Task> taskList = new List<Task>();
      this.StartProgress(20, "Сохранение изменений в архивной копии");
      try
      {
        if (this.CheckIn())
          taskList.Add((Task) this);
        this.IncProgress();
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          if (task is Intermech.Project.Project project)
          {
            project.CheckIn();
            this.IncProgress();
          }
        }
      }
      finally
      {
        this.StopProgress();
      }
      try
      {
        IDBObject dbObject = this.GetObject();
        try
        {
          IProject project = dbObject as IProject;
          this.StartProgress(30, "Синхронизация");
          try
          {
            project.Sync();
          }
          finally
          {
            this.StopProgress();
          }
        }
        finally
        {
          this.ReleaseObject();
        }
      }
      finally
      {
        this.StartProgress(98, "Взятие на изменение");
        try
        {
          foreach (Task task in taskList)
          {
            IDBObject dbObject = task.GetObject();
            try
            {
              task.EditingMode = EditingMode.Edit;
              task.CheckOut(ref dbObject);
            }
            finally
            {
              task.ReleaseObject();
            }
          }
        }
        finally
        {
          this.StopProgress();
        }
        this.StartProgress(99, "Загрузка изменений");
        try
        {
          this.ReloadPortalProps();
          foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
          {
            if (!task.IsProjectSummaryTask && task is Intermech.Project.Project project)
              project.ReloadPortalProps();
          }
        }
        finally
        {
          this.StopProgress();
        }
      }
    }
    finally
    {
      this.StopProgress();
    }
  }

  public RemoteProcessStatus RemoteStatus
  {
    get
    {
      IProject project = this.GetObject(false) as IProject;
      try
      {
        return project != null ? project.RemoteStatus : RemoteProcessStatus.None;
      }
      finally
      {
        this.ReleaseObject();
      }
    }
    set
    {
      IProject project = this.GetObject(false) as IProject;
      try
      {
        if (project == null)
          return;
        project.RemoteStatus = value;
      }
      finally
      {
        this.ReleaseObject();
      }
    }
  }

  internal void ReloadPortalProps()
  {
    IDBObject dbObject = this.GetObject();
    try
    {
      this._Site = (string) null;
      if (this.RemoteStatus == RemoteProcessStatus.WaitingForPublish)
      {
        this._pendingSiteID = string.Empty;
      }
      else
      {
        this.LoadData(this.GetProjectData(dbObject.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ProjectData), true));
        this._pendingSiteID = (string) null;
      }
      this.OnPropertyChanged("PendingSiteID", false);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Возвращает код узла, на котором должен выполняться данный проект. Если узел текущий, возвращает пробел</summary>
  public char RemoteSiteCode
  {
    get
    {
      IUserSession session = this.GetSession();
      try
      {
        Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(session.GetObject(this.ChiefID).SiteID);
        return (int) siteId.Owner != (int) siteId.CurrentSite ? siteId.Owner : ' ';
      }
      finally
      {
        this.ReleaseSession();
      }
    }
  }

  [Serializable]
  public class ProjectCache : Task.TaskCache, ISerializable
  {
    [CanBeNull]
    public double? EstimatedDelay;
    [CanBeNull]
    public TaskCollection Tasks;
    [CanBeNull]
    public IReadOnlyCollection<Intermech.Project.Project> SubProjects;
    [CanBeNull]
    public IReadOnlyList<Task> AllSubTasks;

    public ProjectCache()
    {
    }

    protected ProjectCache([NotNull] SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.EstimatedDelay = info.GetValue<double?>(nameof (EstimatedDelay));
    }

    public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("EstimatedDelay", (object) this.EstimatedDelay);
    }

    public override void Clear()
    {
      base.Clear();
      this.EstimatedDelay = new double?();
      this.Tasks = (TaskCollection) null;
      this.SubProjects = (IReadOnlyCollection<Intermech.Project.Project>) null;
      this.AllSubTasks = (IReadOnlyList<Task>) null;
    }

    public override bool ResetValue([NotNull, NotWhitespace] string valueName)
    {
      if (base.ResetValue(valueName))
        return true;
      switch (valueName)
      {
        case "EstimatedDelay":
          this.EstimatedDelay = new double?();
          return true;
        case "Tasks":
          this.Tasks = (TaskCollection) null;
          return true;
        case "SubProjects":
          this.SubProjects = (IReadOnlyCollection<Intermech.Project.Project>) null;
          return true;
        case "AllSubTasks":
          this.AllSubTasks = (IReadOnlyList<Task>) null;
          return true;
        default:
          return false;
      }
    }
  }

  private class PrevTaskInfo
  {
    [CanBeNull]
    public readonly Task _Task;
    [CanBeNull]
    public readonly Intermech.Project.Project _Project;

    public PrevTaskInfo([CanBeNull] Task task, [CanBeNull] Intermech.Project.Project project)
    {
      this._Task = task;
      this._Project = project;
    }
  }

  private class AssignmentArrangementInformation
  {
    public AssignmentArrangementInformation(
      [NotNull] Dictionary<Assignment, List<Assignment>> selectedAssignments,
      [NotNull] Dictionary<Assignment, double> selectedAssignmentUnits,
      [NotNull] Dictionary<Assignment, Resource> selectedResources,
      double cost,
      DateTime finish,
      [NotNull] Dictionary<Task, DateTime> selectedTaskStarts)
    {
      this.SelectedAssignments = selectedAssignments;
      this.SelectedAssignmentUnits = selectedAssignmentUnits;
      this.SelectedResources = selectedResources;
      this.Cost = cost;
      this.Finish = finish;
      this.SelectedTaskStarts = selectedTaskStarts;
    }

    public double Cost { get; }

    public DateTime Finish { get; }

    [NotNull]
    public Dictionary<Assignment, List<Assignment>> SelectedAssignments { get; }

    [NotNull]
    public Dictionary<Assignment, double> SelectedAssignmentUnits { get; }

    [NotNull]
    public Dictionary<Assignment, Resource> SelectedResources { get; }

    [NotNull]
    public Dictionary<Task, DateTime> SelectedTaskStarts { get; }
  }

  private class ResourceArrangementInformation
  {
    public ResourceArrangementInformation(
      [NotNull] Dictionary<Assignment, Resource> selectedResources,
      double cost,
      DateTime finish,
      [NotNull] Dictionary<Task, DateTime> selectedTaskStarts)
    {
      this.SelectedResources = selectedResources;
      this.Cost = cost;
      this.Finish = finish;
      this.SelectedTaskStarts = selectedTaskStarts;
    }

    public double Cost { get; }

    public DateTime Finish { get; }

    [NotNull]
    public Dictionary<Assignment, Resource> SelectedResources { get; }

    [NotNull]
    public Dictionary<Task, DateTime> SelectedTaskStarts { get; }
  }

  public delegate void TaskLoadedEventHandler([NotNull] Task task);

  public delegate bool? RequestEditHandler([NotNull] Task t);

  public delegate void TaskRequestHandler([NotNull] Task t);

  public class TasksStateSet
  {
    [NotNull]
    public HashSet<long> _Minimized = new HashSet<long>();
    [NotNull]
    public HashSet<long> _LoadedProjects = new HashSet<long>();

    public void Clear()
    {
      this._Minimized.Clear();
      this._LoadedProjects.Clear();
    }
  }

  /// <summary>Класс-оболочка для перечисления задач, импортированных из определённого объекта</summary>
  public class TasksImportedFromObjectClass
  {
    [NotNull]
    private readonly Intermech.Project.Project _project;

    public TasksImportedFromObjectClass([NotNull] Intermech.Project.Project project)
    {
      this._project = project;
    }

    /// <summary>Перечисление задач, импортированных из определённого объекта</summary>
    /// <param name="importedRootObjectVersionGuid">Глобальный идентификатор корневого импортированного объекта</param>
    [NotNull]
    public IEnumerable<Task> this[[NotEmpty] Guid importedRootObjectVersionGuid]
    {
      get
      {
        return this._project.Tasks.Where<Task>((System.Func<Task, bool>) (task => task.ImportedRootObjectVersionGuid.Equals(importedRootObjectVersionGuid)));
      }
    }
  }
}
