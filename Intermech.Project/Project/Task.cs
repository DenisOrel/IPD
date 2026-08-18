// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Task
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Project.Evaluator;
using Intermech.Project.Properties;
using Intermech.Remoting.Sponsors;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Intermech.Project;

[DebuggerDisplay("ObjectID={ObjectID} Name=\"{Name}\"")]
[Serializable]
public class Task : Entity, INotifyPropertyChanged, ISessionProvider
{
  [CanBeNull]
  [NonSerialized]
  protected Task.TaskCache _cache;
  [Intermech.Diagnostics.NotNull]
  [NullBefore("Initialize")]
  private AssignmentCollection _assignments;
  private bool _estimation = true;
  private DateTime _finishConstraint = DateTime.MaxValue;
  internal int _IndentLevel = -1;
  internal bool _Milestone;
  [Intermech.Diagnostics.NotNull]
  protected string _Name = string.Empty;
  [CanBeNull]
  private string _notes;
  private double _percentCompleted;
  private int _priority = 500;
  [CanBeNull]
  [NonSerialized]
  internal Intermech.Project.Project _Project;
  [CanBeNull]
  protected Schedule _Schedule;
  protected DateTime _Start = DateTime.MinValue;
  protected DateTime _Finish = DateTime.MinValue;
  protected DateTime _PrevSavedFinish = DateTime.MinValue;
  private DateTime _startConstraint = DateTime.MinValue;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [CanBeNull]
  private readonly string _wbsCode;
  internal double _Work;
  private bool _justCreated;
  [CanBeNull]
  private static Dictionary<Task, List<Task>> _processedDepTasks;
  [Intermech.Diagnostics.NotNull]
  public static readonly Task[] EmptyTasksArray = Array.Empty<Task>();
  [Intermech.Diagnostics.NotNull]
  private static readonly Regex _resRegex = new Regex("^(.*?)\\s*\\[([\\d.,]+)%?\\]$", RegexOptions.Compiled);
  private double _realWork;
  [CanBeNull]
  private WorkTimeUnit _durationUnit;
  [Intermech.Diagnostics.NotNull]
  public static string _DurationFormat = "{0:0.##}{1}{2}";
  [CanBeNull]
  protected Task _LatestTask;
  protected bool _HasNotLoadedSubTasks;
  [CanBeNull]
  protected Task _ParentTask;
  protected internal bool _UseBulkData = true;
  protected bool _RewriteTaskSortIndex;
  [CanBeNull]
  [NonSerialized]
  internal DataRow _DataRow;
  protected static int _LastIndentDx;
  public static bool _IndicateModifiedTasks = false;
  [Intermech.Diagnostics.NotNull]
  private static readonly CultureInfo _ruCultureInfo = CultureInfo.GetCultureInfo("ru-RU");
  [Intermech.Diagnostics.NotNull]
  private static readonly CultureInfo _frCultureInfo = CultureInfo.GetCultureInfo("fr-FR");
  [Intermech.Diagnostics.NotNull]
  private static readonly CultureInfo _usCultureInfo = CultureInfo.GetCultureInfo("en-US");
  [CanBeNull]
  [NonSerialized]
  private DependencyCollection _backDependencies;
  protected int _GetStartCounter;
  protected double _LevelingDelay;
  private bool _taskManualPlanning;
  protected bool _ManualPlanning;
  protected int _GetFinishCounter;
  [Intermech.Diagnostics.NotNull]
  private static readonly List<string> _milestoneRoProps = new List<string>((IEnumerable<string>) new string[3]
  {
    nameof (Work),
    nameof (Status),
    nameof (Assignments)
  });
  [Intermech.Diagnostics.NotNull]
  private static readonly List<string> _parentRoProps = new List<string>((IEnumerable<string>) new string[7]
  {
    nameof (Work),
    nameof (PercentCompleted),
    nameof (Start),
    nameof (Finish),
    nameof (Status),
    nameof (Estimation),
    nameof (ConstraintType)
  });
  [CanBeNull]
  private static Dictionary<string, List<double>> _propRanges;
  protected bool _SilentMode;
  public WorkType _WorkType = WorkType.FixedDuration;
  public bool IsProjectSummaryTask;
  private EditingMode _editingMode = EditingMode.Edit;
  [NonSerialized]
  protected bool? _WasCheckedOut;
  [NonSerialized]
  protected bool _PseudoCheckedOut;
  internal ConstraintType _ConstraintType = ConstraintType.Undefined;
  [CanBeNull]
  private static Regex _removeShortDayRegex;
  [CanBeNull]
  [NonSerialized]
  public ISessionProvider _SessionProvider;
  [CanBeNull]
  public static INotifier _GlobalNotifier = (INotifier) null;
  protected internal bool _Partial;
  [Intermech.Diagnostics.NotNull]
  private static readonly Dictionary<long, string> _userNameCache = new Dictionary<long, string>();
  [CanBeEmpty]
  private long _verifySchemeID;
  private Color? _taskColor;
  [CanBeNull]
  private static List<ColumnDescriptor> _fcColumns;
  [Intermech.Diagnostics.NotNull]
  private static Dictionary<int, int> _attr2ColumnIndex = new Dictionary<int, int>();
  private bool? _hiddenByFilter;
  protected bool _Uncommitted;
  public long? _CurrentUserObjectID;
  /// <summary>Устанавливается при отрисовке задачи на диаграмме Гантта</summary>
  [CanBeNull]
  [NonSerialized]
  public TaskVisualProps _VisualProps;
  [Intermech.Diagnostics.NotNull]
  private readonly Dictionary<int, AttrValue> _attributesCache = new Dictionary<int, AttrValue>();
  /// <summary>Для свойств из PropInfos.All, которые нужно отображать иначе, у нас есть специальный словарь замен</summary>
  [Intermech.Diagnostics.NotNull]
  private static readonly Dictionary<string, string> _propSubstitutes = new Dictionary<string, string>()
  {
    {
      nameof (PercentCompleted),
      nameof (PercentCompletedString)
    },
    {
      nameof (PlannedPercentCompleted),
      nameof (PlannedPercentCompletedString)
    }
  };
  [CanBeNull]
  protected internal string _Site;
  protected bool _PlanningConflict;
  private long _ownerID;
  /// <summary>True, когда запускаем проект на другом узле, на этом только устанавливается статус TaskStatus.Waiting</summary>
  protected bool _RemoteExec;
  [CanBeNull]
  private PrjAttachmentList _attachments;
  [CanBeNull]
  private AttachmentList _srcData;
  [CanBeNull]
  private AttachmentList _results;
  private bool _propagateResults;
  private bool _useActualScheme = true;
  protected TaskStatus _Status;
  protected long _ObjectID;
  [CanBeNull]
  [NotNullAfter("GetObject")]
  protected IDBObject _Object;
  private int _objectCounter;
  [Intermech.Diagnostics.NotNull]
  [NullBefore("Initialize")]
  [NonSerialized]
  private Stack<TaskState> _savedState;
  internal static int UIDCounter = 0;
  /// <summary>Вызывалась процедура сохранения для данной задачи, или нет</summary>
  protected internal bool _Saved = true;
  private bool _prevAttachmentsModified;
  private bool _prevAssignmentsModified;
  protected bool _IndexModified;
  internal TaskState _State;
  [Intermech.Diagnostics.NotNull]
  [NullBefore("Initialize")]
  [NonSerialized]
  private Dictionary<TaskState, int> _stateCounter;
  protected Guid _ImportedRootObjectVersionGuid = Guid.Empty;
  protected long _ImportedObjectVersion;
  protected Guid _ImportedRelationGuid = Guid.Empty;

  [CanBeNull]
  protected Task.TaskCache _Cache
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._cache;
  }

  [Intermech.Diagnostics.NotNull]
  public Task.TaskCache Cache
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cache ?? (this._cache = this.CreateCache());
    }
  }

  [Intermech.Diagnostics.NotNull]
  protected virtual Task.TaskCache CreateCache() => new Task.TaskCache();

  public void ClearCache()
  {
    if (this._Cache == null)
      return;
    this._Cache.Clear();
  }

  public Task()
  {
  }

  public Task(DateTime start)
    : this()
  {
    this.Start = start;
  }

  public Task([Intermech.Diagnostics.NotNull] string name)
    : this()
  {
    this.Name = name;
  }

  public Task([Intermech.Diagnostics.NotNull] string name, DateTime start)
    : this(name)
  {
    this.Start = start;
  }

  private void Assignments_ListChanged([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType == ListChangedType.ItemAdded)
    {
      int newIndex = e.NewIndex;
      Assignment assignment = this._assignments[newIndex];
      if (this.Milestone)
      {
        this.Assignments.RemoveAt(newIndex);
        throw new ArgumentException(Resources.ErrMilestoneResource);
      }
      assignment._Task = this;
    }
    if (e.ListChangedType == ListChangedType.Reset)
    {
      for (int index = 0; index < this.Assignments.Count; ++index)
      {
        Assignment assignment = this._assignments[index];
        if (assignment._Task != this)
          assignment._Task = this;
      }
    }
    this.PropertiesChanged(Task.CalcProps.Assignment);
  }

  internal void CheckParent(bool clearAll = false)
  {
    Task task = this;
    task.OnPropertyChanged("Tasks");
    task.OnPropertyChanged("SubTasks");
    task.OnPropertyChanged("AllSubTasks");
    task.OnPropertyChanged("HasSubTasks");
    IReadOnlyList<Task> allSubTasks = task.AllSubTasks;
    List<Dependency> dependencyList = new List<Dependency>();
    foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task.Dependencies)
    {
      if (dependency.DependentOfTask != null)
      {
        if (allSubTasks.Contains<Task>(dependency.DependentOfTask))
          dependencyList.Add(dependency);
        dependency.DependentOfTask.CheckParent();
      }
    }
    foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) task.RelatedDependencies)
    {
      if (allSubTasks.Contains<Task>(relatedDependency.Task))
        dependencyList.Add(relatedDependency);
    }
    foreach (Dependency dependency in dependencyList)
      dependency.Delete();
    dependencyList.Clear();
    foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) task.RelatedDependencies)
    {
      if (relatedDependency.Task != null)
      {
        Task depTask = relatedDependency.Task;
        if (!allSubTasks.Contains<Task>(depTask) && task.AllTasks.Any<Task>((System.Func<Task, bool>) (otherTask => otherTask.DependsOf(depTask))))
          dependencyList.Add(relatedDependency);
      }
    }
    foreach (Dependency dependency in dependencyList)
      dependency.Delete();
    if (!clearAll)
      return;
    foreach (Task allSubTask in (IEnumerable<Task>) this.AllSubTasks)
    {
      allSubTask.OnPropertyChanged("Parent");
      allSubTask.PropertiesChanged(false);
    }
  }

  public virtual bool Contains([Intermech.Diagnostics.NotNull] Task task)
  {
    return this == task || this.SubTasks.Any<Task>((System.Func<Task, bool>) (task2 => task2.Contains(task)));
  }

  public virtual void IncreaseIndent() => ++this.IndentLevel;

  public virtual void DecreaseIndent() => --this.IndentLevel;

  public virtual void Remove()
  {
    if (this.Project == null)
      return;
    this.Project.Tasks.Remove(this);
  }

  public void Delete([Intermech.Diagnostics.NotNull] IUserSession session)
  {
    if (this.ObjectID == 0L)
      return;
    foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
      dependency.Delete(session);
    if (this.IsExecuted)
    {
      this.DeleteNotifications();
      this.DeleteUncompletedTimer();
    }
    session.GetObject(this.ObjectID, false)?.Delete(0L);
  }

  private void DependenciesChanged([CanBeNull] Dependency dependency)
  {
    this.OnPropertyChanged("Dependencies");
    this.OnPropertyChanged("DependenciesString");
    List<Task> taskList = new List<Task>((IEnumerable<Task>) this.AllTasks);
    for (Task parent = this.Parent; parent != null; parent = parent.Parent)
    {
      if (!taskList.Contains(parent))
        taskList.Add(parent);
    }
    if (dependency != null)
    {
      for (Task task = dependency.DependentOfTask; task != null; task = task.Parent)
      {
        if (!taskList.Contains(task))
          taskList.Add(task);
      }
    }
    HashSet<Task> processed = new HashSet<Task>();
    foreach (Task task in taskList)
      task.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies, true, processed);
    this.OnPropertyChangeCompleted("Dependencies");
  }

  public virtual bool DependsOf([Intermech.Diagnostics.NotNull] Task task)
  {
    return this.DependsOf(task, (ICollection<DependencyType>) null);
  }

  public bool DependsOf([Intermech.Diagnostics.NotNull] Task task, [CanBeNull] ICollection<DependencyType> dependencyTypes)
  {
    Task._processedDepTasks = new Dictionary<Task, List<Task>>();
    int num = this._dependsOf(task, dependencyTypes) ? 1 : 0;
    Task._processedDepTasks = (Dictionary<Task, List<Task>>) null;
    return num != 0;
  }

  private bool _dependsOf([Intermech.Diagnostics.NotNull] Task task, [CanBeNull] ICollection<DependencyType> dependencyTypes)
  {
    if (this == task)
      return true;
    List<Task> taskList;
    if (Task._processedDepTasks.TryGetValue(this, out taskList) && taskList.Contains(task))
      return false;
    if (taskList != null)
      taskList.Add(task);
    else
      Task._processedDepTasks.Add(this, new List<Task>()
      {
        task
      });
    foreach (Dependency dependency1 in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
    {
      Dependency dependency = dependency1;
      if (dependency.Resolved)
      {
        if (dependencyTypes != null)
        {
          if (dependencyTypes.Any<DependencyType>((System.Func<DependencyType, bool>) (type => dependency.DependentOfTask == task && dependency.DependencyType == type)))
            return true;
        }
        else if (dependency.DependentOfTask == task)
          return true;
        foreach (Task allTask in (IEnumerable<Task>) task.AllTasks)
        {
          Task t = allTask;
          Task dependentOfTask = dependency.DependentOfTask;
          if ((dependentOfTask != null ? (dependentOfTask.AllTasks.Any<Task>((System.Func<Task, bool>) (dt => dt._dependsOf(t, dependencyTypes))) ? 1 : 0) : 0) != 0)
            return true;
        }
      }
    }
    Task parent = this.Parent;
    return parent != null && !parent.IsProjectSummaryTask && parent._dependsOf(task, dependencyTypes);
  }

  private DateTime GetFinish(DateTime start)
  {
    DateTime finish = start;
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) this.GetWorkTime(start, this.HasAnySubTasks ? this.RealWork : this.Work))
    {
      if (dateSchedule.FinishTime > finish)
        finish = dateSchedule.FinishTime;
    }
    return finish;
  }

  private DateTime GetStart(DateTime finish)
  {
    DateTime start = finish;
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) this.GetWorkTime(finish, -(this.HasAnySubTasks ? this.RealWork : this.Work)))
    {
      if (dateSchedule.StartTime < start)
        start = dateSchedule.StartTime;
    }
    return start;
  }

  public double CalcDuration(DateTime finish)
  {
    return this.GetWorkHours(finish) / this.CurrentSchedule.DayDuration;
  }

  protected override void Initialize()
  {
    if (this.Dependencies == null)
      this.Dependencies = new DependencyCollection(this, false);
    if (this._assignments == null)
      this._assignments = new AssignmentCollection(this);
    base.Initialize();
    foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
      dependency._Task = this;
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
      assignment._Task = this;
    this.Dependencies.ListChanged += new ListChangedEventHandler(this.Dependencies_ListChanged);
    this.Dependencies.ItemAdded += new EventHandler<ItemEventArgs<Dependency>>(this.Dependencies_ItemAdded);
    this.Dependencies.ItemRemoved += new EventHandler<ItemEventArgs<Dependency>>(this.Dependencies_ItemRemoved);
    this.Assignments.ListChanged += new ListChangedEventHandler(this.Assignments_ListChanged);
    this._savedState = new Stack<TaskState>();
    this._stateCounter = new Dictionary<TaskState, int>();
  }

  private void Dependencies_ListChanged([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType != ListChangedType.ItemChanged)
      return;
    this.DependenciesChanged(this.Dependencies[e.NewIndex]);
  }

  private void Dependencies_ItemAdded([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] ItemEventArgs<Dependency> e)
  {
    Dependency dependency = e.Item;
    dependency._Task = this;
    try
    {
      dependency.Validate();
    }
    catch
    {
      this.Dependencies.Remove(dependency);
      throw;
    }
    this.DependenciesChanged(dependency);
  }

  private void Dependencies_ItemRemoved([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] ItemEventArgs<Dependency> e)
  {
    this.DependenciesChanged(e.Item);
  }

  public bool IsCritical
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Project.Project rootProject = this.RootProject;
      return rootProject?.ProjectGraph != null && rootProject.ProjectGraph.CriticalTasks.IndexOf(this) != -1;
    }
  }

  private void Schedule_PropertyChanged([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] PropertyChangedEventArgs e)
  {
    this.ScheduleChanged();
  }

  [Intermech.Diagnostics.NotNull]
  public virtual Task SplitRemainingWork()
  {
    if (this.HasSubTasks)
      throw new InvalidOperationException("Cannot split remaining work: not allowed for parent tasks.");
    if (this.Milestone)
      throw new InvalidOperationException("Cannot split remaining work: not allowed for milestones.");
    if (this.CompletedWork == 0.0)
      throw new InvalidOperationException("Cannot split remaining work: not allowed for not started tasks.");
    if (this.RemainingWork == 0.0)
      throw new InvalidOperationException("Cannot split remaining work: not allowed for completed tasks.");
    Task task = new Task(this.Name, DateTime.Today);
    this.RootProject.Tasks.Insert(this.RealIndex + 1, task);
    while (task.IndentLevel < this.IndentLevel)
      task.IncreaseIndent();
    task.Work = this.RemainingWork;
    task.Notes = this.Notes;
    task.Estimation = this.Estimation;
    task.FinishConstraint = this.FinishConstraint;
    task.Priority = this.Priority;
    task.Start = DateTime.Today;
    this.FinishConstraint = DateTime.MaxValue;
    this.UpdateWork(this.CompletedWork, 0.0);
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
    {
      if (assignment.Resource != null)
        task.Assignments.Add(new Assignment(assignment.Resource, assignment.Units));
    }
    foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
    {
      switch (dependency.DependencyType)
      {
        case DependencyType.FinishFinish:
        case DependencyType.StartFinish:
          dependency.Task = task;
          continue;
        case DependencyType.FinishStart:
        case DependencyType.StartStart:
          if (dependency.DependentOfTask != null)
          {
            task.Dependencies.Add(new Dependency(dependency.DependentOfTask, dependency.DependencyType));
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) this.RelatedDependencies)
    {
      switch (relatedDependency.DependencyType)
      {
        case DependencyType.FinishFinish:
        case DependencyType.FinishStart:
          relatedDependency.DependentOfTask = task;
          continue;
        default:
          continue;
      }
    }
    return task;
  }

  public virtual void UpdateWork(double completedWork, double remainingWork)
  {
    this.RemainingWork = remainingWork;
    this.CompletedWork = completedWork;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  protected virtual IReadOnlyList<Task> GetSubTasks()
  {
    if (this.RootProject == null)
      return (IReadOnlyList<Task>) Task.EmptyTasksArray;
    int num = this.IndentLevel + 1;
    TaskCollection tasks = this.RootProject.Tasks;
    int count = tasks.Count;
    List<Task> subTasks = new List<Task>(Math.Max(count, 30));
    for (int index = this.RealIndex + 1; index < count; ++index)
    {
      Task task = tasks[index];
      if (!task.IsProjectSummaryTask)
      {
        if (task.IndentLevel >= num)
        {
          if (task.IndentLevel == num)
            subTasks.Add(task);
        }
        else
          break;
      }
    }
    return (IReadOnlyList<Task>) subTasks;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<Task> SubTasks => this.GetSubTasks();

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  protected virtual IReadOnlyList<Task> GetAllSubTasks()
  {
    if (this.Project == null)
      return (IReadOnlyList<Task>) Task.EmptyTasksArray;
    int num = this.IndentLevel + 1;
    TaskCollection tasks = this.RootProject.Tasks;
    int count = tasks.Count;
    List<Task> allSubTasks = new List<Task>(count);
    for (int index = this.RealIndex + 1; index < count && tasks[index].IndentLevel >= num; ++index)
    {
      Task task = tasks[index];
      if (!task.IsProjectSummaryTask)
        allSubTasks.Add(task);
    }
    return (IReadOnlyList<Task>) allSubTasks;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<Task> AllSubTasks => this.GetAllSubTasks();

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual IReadOnlyList<Task> AllTasks
  {
    get
    {
      IReadOnlyList<Task> allSubTasks = this.AllSubTasks;
      List<Task> allTasks = new List<Task>(allSubTasks.Count + 1);
      allTasks.Add(this);
      allTasks.AddRange((IEnumerable<Task>) allSubTasks);
      return (IReadOnlyList<Task>) allTasks;
    }
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [NullBefore("Initialize")]
  public virtual AssignmentCollection Assignments => this._assignments;

  [Intermech.Diagnostics.NotNull]
  public virtual string AssignmentsString
  {
    get
    {
      if (this.UseCache && this._Cache?.AssignmentsString != null)
        return this._Cache.AssignmentsString;
      if (this.Milestone)
        return string.Empty;
      string assignmentsString;
      if (!this.HasAnySubTasks)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
        {
          StringBuilder stringBuilder2 = stringBuilder1;
          string str1 = stringBuilder1.Length > 0 ? IMProject.ListSeparatorSymbol + " " : string.Empty;
          string str2 = assignment.Resource?.Name ?? string.Empty;
          string str3;
          if (assignment.MaxUnits == 0.0 || assignment.Units == 1.0 && assignment.MaxUnits == 1.0)
            str3 = string.Empty;
          else
            str3 = $" {IMProject.UnitPreSymbol}{assignment.Units * 100.0:0.##}{IMProject.PercentSymbol}{(assignment.MaxUnits > assignment.Units ? (object) $"{IMProject.UnitSeparatorSymbol}{assignment.MaxUnits * 100.0:0.##}{IMProject.PercentSymbol}" : (object) string.Empty)}{IMProject.UnitPostSymbol}";
          stringBuilder2.AppendFormat("{0}{1}{2}", (object) str1, (object) str2, (object) str3);
        }
        assignmentsString = stringBuilder1.ToString();
      }
      else
        assignmentsString = this.ChiefID != -1L ? this.GetUserName(this.ChiefID) ?? string.Empty : string.Empty;
      this.Cache.AssignmentsString = assignmentsString;
      return assignmentsString;
    }
    set
    {
      if (!(value != this.AssignmentsString))
        return;
      if (value == null)
        value = string.Empty;
      value = value.Trim();
      AssignmentCollection assignmentCollection = new AssignmentCollection((Task) null);
      List<Resource> allResources = this.RootProject.AllResources;
      string str1 = value;
      string[] separator = new string[1]
      {
        IMProject.ListSeparatorSymbol
      };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        string input = str2.Trim();
        double result = 1.0;
        Match match = Task._resRegex.Match(input);
        if (match.Success)
        {
          input = match.Groups[1].Value;
          if (Task.TryParseDouble(match.Groups[2].Value, out result))
            result /= 100.0;
          else
            result = 1.0;
        }
        string str3 = input;
        string lower = input.ToLower();
        Assignment assignment = (Assignment) null;
        foreach (Resource resource in allResources)
        {
          if (resource.Name.ToLower() == lower)
          {
            assignment = assignmentCollection.FindByResourceObjectID(resource.ObjectID, new bool?(false));
            if (assignment == null)
            {
              assignment = new Assignment(resource);
              assignmentCollection.Add(assignment);
            }
            assignment.Units = result;
            break;
          }
        }
        if (assignment == null)
          throw new NotificationException(Localization.GetString("ErrResByNameNotFound", (object) str3));
      }
      for (int index1 = this._assignments.Count - 1; index1 >= 0; --index1)
      {
        bool flag = false;
        Assignment assignment = this._assignments[index1];
        for (int index2 = 0; index2 < assignmentCollection.Count - 1; ++index2)
        {
          if (assignment.ResourceObjectID == assignmentCollection[index2].ResourceObjectID)
          {
            assignment.Units = assignmentCollection[index2].Units;
            flag = true;
            assignmentCollection.RemoveAt(index2);
            break;
          }
        }
        if (!flag)
          this._assignments.RemoveAt(index1);
      }
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) assignmentCollection)
        this._assignments.Add(assignment);
    }
  }

  public virtual bool Completed
  {
    get => Math.Round(this.PercentCompleted) == 100.0;
    set
    {
      if (value == this.Completed)
        return;
      this.PercentCompleted = value ? 100.0 : 0.0;
    }
  }

  public virtual double CompletedWork
  {
    get => this.PercentCompleted / 100.0 * this.Work;
    set
    {
      if (value == this.CompletedWork)
        return;
      this.PercentCompleted = value >= 0.0 && value <= this.Work ? Math.Round(value / this.Work * 100.0) : throw new ArgumentOutOfRangeException(nameof (CompletedWork));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string CompletedWorkString
  {
    get => this.FormatDurationH(this.CompletedWork, false);
    set
    {
      if (!(value != this.CompletedWorkString) || value == null)
        return;
      this.CompletedWork = this.ParseDurationH(value);
    }
  }

  public virtual double Cost
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Cost.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Cost.Value;
      }
      if (!this.HasLoadedSubTasks)
      {
        if (this.Project != null)
        {
          double currentCost = Intermech.Project.Project.GetCurrentCost(this);
          this.Cache.Cost = new double?(currentCost);
          return currentCost;
        }
        this.Cache.Cost = new double?(0.0);
        return 0.0;
      }
      double cost = this.SubTasks.Sum<Task>((System.Func<Task, double>) (task => task.Cost));
      this.Cache.Cost = new double?(cost);
      return cost;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string CostString
  {
    get
    {
      if (this.UseCache && this._Cache?.CostString != null)
        return this._Cache.CostString;
      string costString = this.Work > 0.0 ? $"{this.Cost:0.##}{(this.Estimation || this.AllTasks.Any<Task>((System.Func<Task, bool>) (task => task.Assignments.Any<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource is UnknownResource)))) ? (object) IMProject.EstimationSymbol : (object) string.Empty)}" : string.Empty;
      this.Cache.CostString = costString;
      return costString;
    }
  }

  [Intermech.Diagnostics.NotNull]
  private Schedule GetSchedule(long resourceID = 0)
  {
    Schedule schedule = this.Schedule;
    Schedule projectSchedule = this.ProjectSchedule;
    if (schedule == null)
      schedule = projectSchedule;
    List<Schedule> schedules = new List<Schedule>();
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
    {
      Schedule baseSchedule = (Schedule) null;
      if (resourceID != 0L)
      {
        Resource resource = assignment.Resource;
        if ((resource != null ? (resource.ObjectID != resourceID ? 1 : 0) : 1) != 0)
          continue;
      }
      if (assignment.MaxUnits > 0.0)
        baseSchedule = assignment.Resource?.Schedule ?? schedule;
      if (baseSchedule != null)
      {
        if (assignment.MaxUnits != 1.0)
          baseSchedule = (Schedule) new RatioSchedule(baseSchedule, assignment.MaxUnits);
        schedules.Add(baseSchedule);
      }
    }
    if (schedules.Count > 0 && (schedules.Count != 1 || schedules[0] != projectSchedule))
      schedule = (Schedule) MergedSchedule.Get(projectSchedule, schedules);
    return schedule;
  }

  [Intermech.Diagnostics.NotNull]
  public virtual Schedule CurrentSchedule => this.GetSchedule();

  [CanBeNull]
  public virtual Schedule ProjectSchedule
  {
    get
    {
      Schedule schedule = this.Project?.Schedule;
      if (schedule != null)
        return schedule;
      return Schedule.Standard ?? throw new NullReferenceException(nameof (ProjectSchedule));
    }
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [NullBefore("Initialize")]
  public DependencyCollection Dependencies { get; private set; }

  [Intermech.Diagnostics.NotNull]
  public virtual string DependenciesString
  {
    get
    {
      if (this.UseCache && this._Cache?.DependenciesString != null)
        return this._Cache.DependenciesString;
      string[] array = new string[this.Dependencies.Count];
      int num = 0;
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
        array[num++] = dependency.ShortName;
      Array.Sort<string>(array);
      string dependenciesString = string.Join(IMProject.ListSeparatorSymbol + " ", array);
      this.Cache.DependenciesString = dependenciesString;
      return dependenciesString;
    }
    set
    {
      if (!(value != this.DependenciesString) || !this.CanSetProperty(nameof (DependenciesString), (object) value))
        return;
      value = value ?? string.Empty;
      value = value.Trim();
      List<Dependency> dependencyList1 = new List<Dependency>();
      string str1 = value;
      string[] separator = new string[1]
      {
        IMProject.ListSeparatorSymbol
      };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        string s = str2.Trim();
        DependencyType depType = DependencyType.FinishStart;
        string empty = string.Empty;
        Dependency.ParseShortName(ref s, ref depType, ref empty);
        string lStr = s.ToLower();
        dependencyList1.Add(new Dependency((this.Project.Tasks.FirstOrDefault<Task>((System.Func<Task, bool>) (t => t.IndexString == lStr)) ?? this.Project.Tasks.FirstOrDefault<Task>((System.Func<Task, bool>) (t => t.WbsCode.ToLower() == lStr)) ?? this.Project.Tasks.FirstOrDefault<Task>((System.Func<Task, bool>) (t => t.Name.ToLower() == lStr))) ?? throw new NotificationException(Localization.GetString("ErrDepTaskNotFound", (object) str2.Trim())), depType)
        {
          _Task = this,
          LagString = empty
        });
      }
      List<Dependency> dependencyList2 = new List<Dependency>();
      foreach (Dependency dependency1 in dependencyList1)
      {
        Dependency dependency2 = this.Dependencies.FindByTask(dependency1.DependentOfTask);
        if (dependency2 == null)
        {
          dependency1._Task = (Task) null;
          dependency1.Task = this;
          dependency2 = dependency1;
        }
        else
        {
          dependency2.DependencyType = dependency1.DependencyType;
          dependency2.LagString = dependency1.LagString;
        }
        dependencyList2.Add(dependency2);
      }
      for (int index = this.Dependencies.Count - 1; index >= 0; --index)
      {
        if (!dependencyList2.Contains(this.Dependencies[index]))
          this.Dependencies.RemoveAt(index);
      }
    }
  }

  /// <summary>
  /// Для суммарных задач возвращает трудозатраты самого длинного участка
  /// Для несуммарных задач == Work
  /// Если подзадачи суммарной задачи ещё не загружены, тогда значение хранится в переменной,
  /// потому что не всегда длительность суммарной задачи напрямую связана с трудозатратами
  /// </summary>
  public virtual double RealWork
  {
    get
    {
      if (this.HasLoadedSubTasks)
        return this.GetWorkHours(this.SubtasksStart, this.SubtasksFinish);
      if (!this.HasNotLoadedSubTasks)
      {
        this._realWork = this.Work;
        int workResourceCount = this.Assignments.WorkResourceCount;
        if (workResourceCount > 0)
        {
          double w = this._realWork / (double) workResourceCount;
          this._realWork = this.Assignments.Where<Assignment>((System.Func<Assignment, bool>) (a => a.MaxUnits > 0.0)).Select<Assignment, double>((System.Func<Assignment, double>) (a => w / a.MaxUnits)).Max();
        }
      }
      return this._realWork;
    }
    set
    {
      this._realWork = value;
      double num1 = value;
      double element = 1.0;
      int workResourceCount = this.Assignments.WorkResourceCount;
      if (workResourceCount > 0)
      {
        double num2 = this.Assignments.Where<Assignment>((System.Func<Assignment, bool>) (a => a.MaxUnits > 0.0)).Select<Assignment, double>((System.Func<Assignment, double>) (a => a.MaxUnits)).Append<double>(element).Min();
        num1 = this._realWork * (double) workResourceCount * num2;
      }
      if (this.HasNotLoadedSubTasks)
        return;
      this.Work = num1;
    }
  }

  public virtual double Duration
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Duration.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Duration.Value;
      }
      double duration = this.GetWorkHours(this.Start, this.Finish) / this.CurrentSchedule.DayDuration;
      this.Cache.Duration = new double?(duration);
      return duration;
    }
    set
    {
      if (value == this.Duration)
        return;
      this.RealWork = this.CurrentSchedule.DayDuration * value;
      if (this.HasState(TaskState.Loading))
        return;
      this.DurationLock = nameof (Duration);
    }
  }

  [Intermech.Diagnostics.NotNull]
  protected internal string FormatDurationNC(double value, bool estimation, [CanBeNull] WorkTimeUnit unit)
  {
    return string.Format(Task._DurationFormat, (object) value, (object) (unit?.ShortName ?? "?"), estimation ? (object) IMProject.EstimationSymbol : (object) string.Empty);
  }

  [Intermech.Diagnostics.NotNull]
  protected internal string FormatDuration(double days, bool estimation, [CanBeNull] WorkTimeUnit unit)
  {
    if (unit == null)
      unit = WorkTimeUnits.Days;
    days = unit.Convert(days, this.CurrentSchedule);
    return this.FormatDurationNC(days, estimation, unit);
  }

  [Intermech.Diagnostics.NotNull]
  protected string FormatDurationH(double hours, bool estimation)
  {
    return this.FormatDurationNC(hours, estimation, WorkTimeUnits.Hours);
  }

  internal double ParseDuration([Intermech.Diagnostics.NotNull] string value, [CanBeNull] WorkTimeUnit defaultUnit)
  {
    WorkTimeValue workTimeValue = WorkTimeUnits.Parse(value, defaultUnit);
    if (workTimeValue != null)
    {
      if (defaultUnit == WorkTimeUnits.Days)
        this._durationUnit = workTimeValue.Unit;
      this.Estimation = workTimeValue.Estimation;
      WorkTimeUnit unit = workTimeValue.Unit;
      return unit == null ? 0.0 : unit.ToDays(workTimeValue.Value, this.CurrentSchedule);
    }
    if (this._SilentMode)
      return 0.0;
    throw new NotificationException(string.Format(Resources.ErrWrongDurationFormat, (object) value));
  }

  protected double ParseDurationH([Intermech.Diagnostics.NotNull] string value)
  {
    return this.ParseDuration(value, WorkTimeUnits.Hours) * this.CurrentSchedule.DayDuration;
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string DurationString
  {
    get => this.FormatDuration(this.Duration, this.Estimation, this._durationUnit);
    set
    {
      if (!(value != this.DurationString) || value == null)
        return;
      this.Duration = this.ParseDuration(value, WorkTimeUnits.Days);
    }
  }

  public int WeekDaysCount(DateTime startDT, DateTime endDT)
  {
    if (startDT == endDT)
      return 0;
    if (startDT > endDT)
    {
      DateTime dateTime = endDT;
      endDT = startDT;
      startDT = dateTime;
    }
    startDT = startDT.AddDays(1.0);
    int num = 0;
    for (; startDT <= endDT; startDT = startDT.AddDays(1.0))
    {
      if (this.CurrentSchedule.IsNonWorkingTime(startDT))
        ++num;
    }
    return num;
  }

  public virtual bool Estimation
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Estimation.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Estimation.Value;
      }
      if (this.HasLoadedSubTasks)
      {
        if (this.SubTasks.Any<Task>((System.Func<Task, bool>) (task => !task.Uncommitted && task.Estimation)))
        {
          this.Cache.Estimation = new bool?(true);
          return true;
        }
        this.Cache.Estimation = new bool?(false);
        return false;
      }
      bool flag = this.Assignments.Any<Assignment>((System.Func<Assignment, bool>) (assignment => assignment.Resource is UnknownResource && assignment.MaxUnits > 1.0));
      bool estimation = !this.Milestone && !this.Completed && this._estimation | flag;
      this.Cache.Estimation = new bool?(estimation);
      return estimation;
    }
    set
    {
      if (value == this.Estimation || !this.CanSetProperty(nameof (Estimation), (object) value))
        return;
      this.OnPropertyChanging(nameof (Estimation));
      this._estimation = value;
      this.PropertiesChanged(Task.CalcProps.Estimation);
      this.OnPropertyChangeCompleted(nameof (Estimation));
    }
  }

  protected virtual void CheckInProjectBounds(ref DateTime dt, bool thisIsStart)
  {
    DateTime dateTime = dt;
    if (this.Project == null || this.Uncommitted || this.Project.IsSubProject)
      return;
    if (this.Project.PlanningType == PlanningType.FromStart && !this.Project.HasState(TaskState.GraphCalculating))
    {
      if (!thisIsStart)
        dateTime = this.GetStart(dt);
      if (dateTime < this.Project.Start)
        dt = thisIsStart ? this.Project.Start : this.GetFinish(this.Project.Start);
    }
    if (this.Project.PlanningType != PlanningType.FromEnd || this.Project.HasState(TaskState.GraphCalculating) || !((thisIsStart ? this.GetFinish(dt) : dt) > this.Project.Finish))
      return;
    dt = thisIsStart ? this.GetStart(this.Project.Finish) : this.Project.Finish;
  }

  internal void CheckInProjectBounds()
  {
    if (this.Project == null)
      return;
    DateTime start = this.Project.Start;
    if (this._Start < start)
      this._Start = start;
    if (!(this._Finish < start))
      return;
    this._Finish = start;
  }

  public virtual DateTime FinishConstraint
  {
    get => this._finishConstraint;
    set
    {
      if (!(value != this.FinishConstraint) || !this.CanSetProperty(nameof (FinishConstraint), (object) value))
        return;
      this.OnPropertyChanging(nameof (FinishConstraint));
      this._finishConstraint = value;
      if (value == DateTime.MaxValue)
        this._ConstraintType = this.DefaultConstraintType;
      this.PropertiesChanged(Task.CalcProps.FinishConstraint);
      this.OnPropertyChangeCompleted(nameof (FinishConstraint));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string FinishConstraintString
  {
    get
    {
      return !(this.FinishConstraint >= DateTime.MaxValue) ? this.FormatDateTime(this.FinishConstraint) : string.Empty;
    }
    set
    {
      if (!(value != this.FinishConstraintString) || value == null)
        return;
      if (value.Length == 0)
        value = (string) null;
      this.FinishConstraint = value != null ? this.ParseDateTime(value) : DateTime.MaxValue;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string FinishString
  {
    get
    {
      return this.FormatDateTime(this.Finish) + (this.Estimation ? IMProject.EstimationSymbol : string.Empty);
    }
    set
    {
      if (!(value != this.FinishString) || value == null)
        return;
      bool flag = value.EndsWith(IMProject.EstimationSymbol);
      this.Estimation = flag;
      if (flag)
        value = value.Substring(0, value.Length - IMProject.EstimationSymbol.Length);
      this.Finish = this.ParseDateTime(value);
    }
  }

  public bool HasNotLoadedSubTasks
  {
    [DebuggerStepThrough] get => this._HasNotLoadedSubTasks;
    set
    {
      if (this._HasNotLoadedSubTasks == value)
        return;
      this._HasNotLoadedSubTasks = value;
      this.OnPropertyChanged("HasSubTasks");
    }
  }

  public virtual bool HasLoadedSubTasks
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !this._HasNotLoadedSubTasks && this.HasSubTasks;
    }
  }

  protected bool HasAnySubTasks => this.HasNotLoadedSubTasks || this.HasSubTasks;

  public virtual bool HasSubTasks
  {
    get
    {
      if (this.Project != null)
      {
        if (this._HasNotLoadedSubTasks)
          return true;
        if (this.UseCache)
        {
          Task.TaskCache cache = this._Cache;
          if ((cache != null ? (cache.HasSubTasks.HasValue ? 1 : 0) : 0) != 0)
            return this._Cache.HasSubTasks.Value;
        }
        int num = this.IndentLevel + 1;
        Intermech.Project.Project rootProject = this.RootProject;
        for (int index = this.RealIndex + 1; index < rootProject.Tasks.Count; ++index)
        {
          Task task = rootProject.Tasks[index];
          if (task.IndentLevel == num)
          {
            this.Cache.HasSubTasks = new bool?(true);
            this.Assignments.RemoveNonChiefItems();
            return true;
          }
          if (task.IndentLevel < num)
          {
            this.Cache.HasSubTasks = new bool?(false);
            return false;
          }
        }
        this.Cache.HasSubTasks = new bool?(false);
      }
      return false;
    }
  }

  [Intermech.Diagnostics.NotNull]
  protected virtual Task CreateSubTask([Intermech.Diagnostics.NotNull] DataRow row)
  {
    long int64 = Convert.ToInt64(row[0]);
    return Convert.ToInt32(row[2]) != (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project ? new Task(int64) : (Task) new Intermech.Project.Project(int64);
  }

  public void LoadAsSubTask([Intermech.Diagnostics.NotNull] Task parent, [Intermech.Diagnostics.NotNull] Intermech.Project.Project project)
  {
    try
    {
      this.Loading();
      if (parent is Intermech.Project.Project project1)
        project = project1;
      this._ParentTask = parent;
      try
      {
        this._IndentLevel = parent.IndentLevel + 1;
        this.Project = project;
      }
      finally
      {
        this._ParentTask = (Task) null;
      }
      bool? editingMode = new bool?(parent.EditingMode.Any());
      if (this is Intermech.Project.Project project2)
      {
        project2.AssignProperties((Task) project);
        if (editingMode.Value)
          editingMode = new bool?();
      }
      this.Load(project, editingMode);
    }
    finally
    {
      this.Loaded();
    }
  }

  protected virtual void LoadSubTasksInternal([Intermech.Diagnostics.NotNull] IUserSession session, [Intermech.Diagnostics.NotNull] Intermech.Project.Project project)
  {
    try
    {
      DataRow[] dbTasks = this.GetDbTasks(project, session);
      if (dbTasks == null)
        return;
      project.StartProgress(dbTasks.Length, string.Empty);
      try
      {
        foreach (DataRow row in dbTasks)
        {
          Task subTask = this.CreateSubTask(row);
          if (project._BulkData?.Tasks != null)
            subTask._DataRow = row;
          subTask.LoadAsSubTask(this, project);
          if (DBNull.Value.Equals(row[3]))
            subTask._RewriteTaskSortIndex = true;
          else if (Convert.ToInt64(row[3]) != (long) subTask.SortIndex)
            subTask._RewriteTaskSortIndex = true;
        }
      }
      finally
      {
        project.StopProgress();
      }
    }
    finally
    {
      this._HasNotLoadedSubTasks = false;
    }
  }

  public bool LoadSubTasks([CanBeNull] Intermech.Project.Project project, bool requestEdit = false, bool recursive = false)
  {
    if (project != null && this.HasNotLoadedSubTasks && !this.HasState(TaskState.LoadingSubtasks))
    {
      if (requestEdit && this is Intermech.Project.Project && project.EditingMode.Any() && !this.RequestEdit(EditingMode.Composition, false))
        return false;
      try
      {
        for (Intermech.Project.Project project1 = project; project1 != null; project1 = project1.Project)
          project1.Loading();
        this.Loading();
        IUserSession session = this.GetSession();
        this.SetState(TaskState.LoadingSubtasks);
        try
        {
          this.LoadSubTasksInternal(session, project);
          this.Minimized = false;
        }
        finally
        {
          this.UnsetState(TaskState.LoadingSubtasks);
          this.ReleaseSession();
        }
      }
      finally
      {
        this.Loaded();
        for (Intermech.Project.Project project2 = project; project2 != null; project2 = project2.Project)
          project2.Loaded();
      }
    }
    if (recursive)
    {
      foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
      {
        if (subTask.HasNotLoadedSubTasks)
          subTask.LoadSubTasks(requestEdit, true);
      }
    }
    return true;
  }

  public bool LoadSubTasks(bool requestEdit = false, bool recursive = false)
  {
    return this.LoadSubTasks(this.Project, requestEdit, recursive);
  }

  internal void SetIndentLevel(int value, bool checkParent)
  {
    if (!this.CanSetProperty("IndentLevel", (object) value))
      return;
    List<Task> taskList1 = new List<Task>();
    List<Task> taskList2 = new List<Task>();
    IReadOnlyList<Task> allSubTasks = this.AllSubTasks;
    Intermech.Project.Project rootProject = this.RootProject;
    if (checkParent)
    {
      Task task = (Task) null;
      for (int index = this.RealIndex - 1; index >= 0; --index)
      {
        if (value > this.IndentLevel && rootProject.Tasks[index].IndentLevel >= this.IndentLevel || value < this.IndentLevel && rootProject.Tasks[index].IndentLevel < this.IndentLevel)
        {
          task = rootProject.Tasks[index];
          break;
        }
        if (value > this.IndentLevel && rootProject.Tasks[index].IndentLevel < this.IndentLevel)
          break;
      }
      if (task == null || !task.CanSetProperty("null", (object) null))
        return;
      if (task.Minimized)
        rootProject.RequestExpand(task);
      taskList1.Add(task);
    }
    Task parent1 = this.Parent;
    int num = value - this.IndentLevel;
    foreach (Task task in (IEnumerable<Task>) allSubTasks)
      task._IndentLevel += num;
    this._IndentLevel += num;
    taskList2.Add(this);
    taskList2.AddRange((IEnumerable<Task>) allSubTasks);
    if (parent1 != null && !taskList1.Contains(parent1))
      taskList1.Add(parent1);
    this.OnPropertyChanged("Parent");
    this._Cache?.ResetValue("Parent");
    Task parent2 = this.Parent;
    if (parent2 != null && !taskList1.Contains(parent2))
      taskList1.Add(parent2);
    foreach (Entity task in (System.Collections.ObjectModel.Collection<Task>) rootProject.Tasks)
      task.OnPropertyChanged("WbsCode", false);
    foreach (Task task in taskList2)
      task.PropertiesChanged(Task.CalcProps.Indent | Task.CalcProps.BackDependencies);
    this.OnPropertyChanged("SubTasks");
    if (this.HasSubTasks)
      taskList1.Add(this);
    taskList1.Add((Task) rootProject);
    bool flag = (rootProject.State & TaskState.Loading) != 0;
    foreach (Task task in taskList1)
    {
      task.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.Work | Task.CalcProps.Dependencies, false);
      if (task.HasSubTasks)
        task.CheckParent();
      if (task.ConstraintType != rootProject.ConstraintType && !flag)
      {
        task.ConstraintDate = DateTime.MinValue;
        task._ConstraintType = rootProject.ConstraintType;
      }
      foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) rootProject.RelatedDependencies)
      {
        try
        {
          relatedDependency.Validate();
        }
        catch
        {
          relatedDependency.Task?.Dependencies.Remove(relatedDependency);
        }
      }
    }
    this.UpdateProjectFromParent();
  }

  public virtual int IndentLevel
  {
    [DebuggerStepThrough] get => this._IndentLevel;
    set
    {
      if (value == this.IndentLevel)
        return;
      this.SetIndentLevel(value, true);
    }
  }

  private void UpdateProjectFromParent()
  {
    Task task = this;
    do
    {
      task = task.Parent;
    }
    while (task != null && !(task is Intermech.Project.Project));
    if (!(task is Intermech.Project.Project project))
      project = this.RootProject;
    this.Project = project;
  }

  public int RealIndentLevel
  {
    get
    {
      int num = 0;
      Intermech.Project.Project project = this.RootProject ?? this as Intermech.Project.Project;
      if ((project != null ? (project.ShowProjectTask ? 1 : 0) : 0) != 0)
        num = 1;
      return this.IndentLevel + num;
    }
  }

  public int RealIndex => base.Index;

  public int MaxPossibleIndentLevel
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.MaxPossibleIndentLevel.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.MaxPossibleIndentLevel.Value;
      }
      Intermech.Project.Project rootProject = this.RootProject;
      Task task = (Task) null;
      for (int index = this.RealIndex - 1; index >= 0; --index)
      {
        if (rootProject.Tasks[index].IndentLevel >= this.IndentLevel)
        {
          task = rootProject.Tasks[index];
          break;
        }
        if (rootProject.Tasks[index].IndentLevel < this.IndentLevel)
          break;
      }
      int possibleIndentLevel = this.IndentLevel;
      if (task != null)
        possibleIndentLevel = task.IndentLevel + 1;
      this.Cache.MaxPossibleIndentLevel = new int?(possibleIndentLevel);
      return possibleIndentLevel;
    }
  }

  public new virtual int Index
  {
    get => !this.RootProject.ShowProjectTask ? this.RealIndex : this.RealIndex - 1;
    set
    {
      int index1 = this.Index;
      Intermech.Project.Project rootProject = this.RootProject;
      if (rootProject.ShowProjectTask)
        --value;
      if (index1 == -1)
        return;
      if (value == -1)
        value = rootProject.Tasks.Count;
      if (index1 == value || index1 >= rootProject.Tasks.Count)
        return;
      this.SetState(TaskState.IndexChanging);
      try
      {
        if (rootProject.ShowProjectTask)
        {
          ++index1;
          ++value;
          if (value == 0)
            ++value;
        }
        int num1 = value - index1;
        List<(Task, int, int)> list = this.AllSubTasks.Select<Task, (Task, int, int)>((System.Func<Task, (Task, int, int)>) (t => (t, t.RealIndex, t.IndentLevel))).ToList<(Task, int, int)>(this.AllSubTasks.Count);
        Task parent = this.Parent;
        rootProject.Tasks.RaiseItemEvents = false;
        rootProject.Tasks.RaiseListChangedEvents = false;
        try
        {
          rootProject.Tasks.RemoveAt(index1);
          if (num1 > 0)
            --num1;
          if (value > rootProject.Tasks.Count)
            value = rootProject.Tasks.Count;
          int num2 = 0;
          if (rootProject.Tasks.Count > value)
            num2 = rootProject.Tasks[value].IndentLevel;
          if (!this.HasState(TaskState.SubtaskIndexChanging))
          {
            Task._LastIndentDx = num2 - this._IndentLevel;
            this._IndentLevel = num2;
          }
          else
            this._IndentLevel += Task._LastIndentDx;
          rootProject.Tasks.Insert(value, this);
        }
        finally
        {
          rootProject.Tasks.RaiseListChangedEvents = true;
          rootProject.Tasks.RaiseItemEvents = true;
          rootProject.Tasks.RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, index1, value));
        }
        if (this.HasState(TaskState.SubtaskIndexChanging))
          return;
        foreach ((Task task, int num3, int num4) in list)
        {
          task.SetState(TaskState.SubtaskIndexChanging);
          try
          {
            task._IndentLevel = num4;
            task.Index = num3 + num1;
            if (num1 > 0)
              --num1;
          }
          finally
          {
            task.UnsetState(TaskState.SubtaskIndexChanging);
          }
        }
        value = Math.Min(index1, value);
        for (int index2 = value; index2 < rootProject.Tasks.Count; ++index2)
          rootProject.Tasks[index2]._Cache?.Clear();
        this.UpdateProjectFromParent();
        parent?.PropertiesChanged(Task.CalcProps.Dependencies, false);
        if (this.Parent != null)
          this.Parent.PropertiesChanged(Task.CalcProps.Dependencies, false);
        if (parent != this._Project && this._Project != null)
          this._Project._Cache?.Clear();
        for (int index3 = this.Dependencies.Count - 1; index3 >= 0; --index3)
        {
          Dependency dependency = this.Dependencies[index3];
          try
          {
            dependency.Validate();
          }
          catch
          {
            this.Dependencies.Remove(dependency);
          }
        }
      }
      finally
      {
        this.UnsetState(TaskState.IndexChanging);
      }
    }
  }

  public int LocalIndex
  {
    get
    {
      int localIndex = -1;
      if (this.Project != null)
      {
        localIndex = this.Project.Tasks.IndexOf(this);
        if (this.Project.ShowProjectTask)
          --localIndex;
      }
      return localIndex;
    }
  }

  protected int SortIndex
  {
    [DebuggerStepThrough] get => this.LocalIndex;
  }

  public int DispIndex
  {
    [DebuggerStepThrough] get => this.Index + 1;
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string IndexString
  {
    get
    {
      string indexString = this.DispIndex.ToString();
      if (Task._IndicateModifiedTasks && this.Modified)
        indexString += "*";
      return indexString;
    }
  }

  public virtual bool Milestone
  {
    get => this._Milestone;
    set
    {
      if (value == this.Milestone || !this.CanSetProperty(nameof (Milestone), (object) value))
        return;
      if (value && this.HasSubTasks)
        throw new NotificationException(Resources.SummaryCantBeMilestone);
      this.OnPropertyChanging(nameof (Milestone));
      this._Milestone = value;
      if (this.Milestone && this.Assignments.Count > 0)
        this.Assignments.Clear();
      this.PropertiesChanged();
      this.OnPropertyChangeCompleted(nameof (Milestone));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string Name
  {
    get => this._Name;
    set
    {
      value = value ?? string.Empty;
      if (!(value != this.Name) || !this.CanSetProperty(nameof (Name), (object) value))
        return;
      this.OnPropertyChanging(nameof (Name));
      this._Name = value;
      this.OnPropertyChanged(nameof (Name));
      this.OnPropertyChangeCompleted(nameof (Name));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string NotesString
  {
    get => this.Notes?.Replace("\r\n", " ").Trim() ?? string.Empty;
    set
    {
      if (!(this.Notes != value) || !this.CanSetProperty("Notes", (object) value))
        return;
      this.Notes = value;
    }
  }

  [CanBeNull]
  public virtual Task Parent
  {
    get
    {
      if (this.UseCache && this._Cache != null && this._Cache.Parent.HasValue)
        return this._Cache.Parent.Value;
      if (this.Project != null)
      {
        Intermech.Project.Project rootProject = this.RootProject;
        int indentLevel = this.IndentLevel;
        for (int index = Math.Min(this.RealIndex, rootProject.Tasks.Count - 1); index >= 0; --index)
        {
          Task task = rootProject.Tasks[index];
          if (task.IndentLevel < indentLevel)
          {
            this.Cache.Parent = (Maybe<Task>) task;
            return task;
          }
        }
        this.Cache.Parent = (Maybe<Task>) (Task) null;
      }
      return (Task) null;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string Notes
  {
    get
    {
      if (this._notes == null && this.ObjectID != 0L)
      {
        this.GetObject();
        try
        {
          IDBAttribute attributeById = this._Object.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Description);
          if (attributeById != null)
            this._notes = attributeById.AsString;
        }
        finally
        {
          this.ReleaseObject();
        }
      }
      return this._notes ?? (this._notes = string.Empty);
    }
    set
    {
      if (!(value != this.Notes) || !this.CanSetProperty(nameof (Notes), (object) value))
        return;
      this.OnPropertyChanging(nameof (Notes));
      this._notes = value;
      this.OnPropertyChanged(nameof (Notes));
      this.OnPropertyChangeCompleted(nameof (Notes));
    }
  }

  public virtual double PercentCompleted
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.PercentCompleted.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.PercentCompleted.Value;
      }
      if (!this.HasLoadedSubTasks)
        return this._percentCompleted;
      double num = this.SubTasks.Sum<Task>((System.Func<Task, double>) (task => task.CompletedWork));
      double percentCompleted = (this.Work > 0.0 ? num / this.Work : 1.0) * 100.0;
      this.Cache.PercentCompleted = new double?(percentCompleted);
      return percentCompleted;
    }
    set
    {
      if (value > 100.0)
        value = 100.0;
      if (value == this.PercentCompleted || !this.CanSetProperty(nameof (PercentCompleted), (object) value))
        return;
      this.OnPropertyChanging(nameof (PercentCompleted));
      int num = this._percentCompleted >= 100.0 ? 0 : (value >= 100.0 ? 1 : 0);
      this._percentCompleted = value;
      this.PropertiesChanged(Task.CalcProps.PercentCompleted);
      if (num != 0)
      {
        this.OnPropertyChanging("StatusString");
        this.OnPropertyChangeCompleted("StatusString");
      }
      this.OnPropertyChangeCompleted(nameof (PercentCompleted));
    }
  }

  public static bool TryParseDouble([Intermech.Diagnostics.NotNull] string s, out double result)
  {
    return double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.CurrentCulture, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._ruCultureInfo, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._frCultureInfo, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._usCultureInfo, out result);
  }

  public static double ParseDouble([Intermech.Diagnostics.NotNull] string s, bool throwExceptionIfFail = false)
  {
    double result;
    if (((double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.CurrentCulture, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._ruCultureInfo, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result) || double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._frCultureInfo, out result) ? 0 : (!double.TryParse(s, NumberStyles.Float, (IFormatProvider) Task._usCultureInfo, out result) ? 1 : 0)) & (throwExceptionIfFail ? 1 : 0)) != 0)
      throw new FormatException(s);
    return result;
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string PercentCompletedString
  {
    get => $"{this.PercentCompleted:0.##}{IMProject.PercentSymbol}";
    set
    {
      if (!(value != this.PercentCompletedString) || value == null)
        return;
      if (value.EndsWith(IMProject.PercentSymbol))
        value = value.Substring(0, value.Length - IMProject.PercentSymbol.Length);
      this.PercentCompleted = Task.ParseDouble(value);
    }
  }

  public virtual int Priority
  {
    get => this._priority;
    set
    {
      if (value == this.Priority || !this.CanSetProperty(nameof (Priority), (object) value))
        return;
      this.OnPropertyChanging(nameof (Priority));
      this._priority = value;
      this.OnPropertyChanged(nameof (Priority));
      this.OnPropertyChanged("PriorityString");
      this.OnPropertyChangeCompleted(nameof (Priority));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string PriorityString
  {
    get => $"{this.Priority:0.##}";
    set
    {
      if (!(value != this.PriorityString) || value == null)
        return;
      this.Priority = int.Parse(value);
    }
  }

  [CanBeNull]
  [NotNullAfter("ProjectNeeded")]
  public virtual Intermech.Project.Project Project
  {
    get => this._Project;
    set
    {
      if (value == this.Project)
        return;
      if (value == this)
        throw new Exception($"Wrong project assignment detected ({this.Name})");
      Intermech.Project.Project project = this._Project;
      this._Project = value;
      if (this.Project != null)
      {
        if (this._ParentTask == null)
          this._ParentTask = (Task) value;
        try
        {
          Intermech.Project.Project rootProject = this.RootProject;
          if (rootProject != null)
          {
            if (!rootProject.Tasks.Contains(this))
            {
              int index1 = 0;
              if (this._ParentTask != null)
              {
                int indentLevel = this._ParentTask._IndentLevel;
                int num = -1;
                for (int index2 = 0; index2 < rootProject.Tasks.Count; ++index2)
                {
                  if (rootProject.Tasks[index2] == this._ParentTask)
                  {
                    num = index2;
                    break;
                  }
                }
                if (num != -1)
                {
                  for (int index3 = num + 1; index3 < rootProject.Tasks.Count; ++index3)
                  {
                    if (rootProject.Tasks[index3].IndentLevel <= indentLevel)
                    {
                      index1 = index3;
                      break;
                    }
                  }
                }
              }
              if (index1 == 0 || index1 > rootProject.Tasks.Count - 1)
                rootProject.Tasks.Add(this);
              else
                rootProject.Tasks.Insert(index1, this);
            }
          }
        }
        finally
        {
          this._ParentTask = (Task) null;
        }
        if (!this.HasSubTasks && this.Start < this.Project.Start)
          this._Start = this.Project.Start;
      }
      if (!(this is Intermech.Project.Project))
      {
        foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
          subTask.Project = this.Project;
      }
      if (project != null && project != this._Project)
        project.TasksChanged();
      if (this.Uncommitted)
        return;
      this.Project.TasksChanged();
    }
  }

  [Intermech.Diagnostics.NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DependencyCollection BackDependencies
  {
    get
    {
      return this._backDependencies ?? (this._backDependencies = new DependencyCollection(this, true));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public DependencyCollection RelatedDependencies => this.BackDependencies;

  public virtual double RemainingWork
  {
    get => this.Work - this.CompletedWork;
    set
    {
      if (value == this.RemainingWork)
        return;
      if (value < 0.0)
        throw new ArgumentOutOfRangeException(nameof (RemainingWork));
      double completedWork = this.CompletedWork;
      this.Work = completedWork + value;
      if (this.Work <= 0.0)
        return;
      this.PercentCompleted = completedWork / this.Work * 100.0;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string RemainingWorkString
  {
    get => this.FormatDurationH(this.RemainingWork, this.Estimation);
    set
    {
      if (!(value != this.RemainingWorkString) || value == null)
        return;
      this.RemainingWork = this.ParseDurationH(value);
    }
  }

  public virtual void ScheduleChanged()
  {
    foreach (Task allSubTask in (IEnumerable<Task>) this.AllSubTasks)
    {
      if (allSubTask.Schedule == null)
        allSubTask.PropertiesChanged(Task.CalcProps.Schedule, false);
    }
    this.PropertiesChanged(Task.CalcProps.Schedule);
  }

  [CanBeNull]
  public virtual Schedule Schedule
  {
    get => this._Schedule;
    set
    {
      if (value == this.Schedule)
        return;
      this.OnPropertyChanging(nameof (Schedule));
      if (this.Schedule != null)
        this.Schedule.PropertyChanged -= new PropertyChangedEventHandler(this.Schedule_PropertyChanged);
      this._Schedule = value;
      if (this.Schedule != null)
        this.Schedule.PropertyChanged += new PropertyChangedEventHandler(this.Schedule_PropertyChanged);
      this.ScheduleChanged();
      this.OnPropertyChangeCompleted(nameof (Schedule));
    }
  }

  protected void CheckForRecursion(int counter)
  {
    if (counter > 100)
      throw new Exception($"Recursion found: {counter}");
  }

  public double LevelingDelay
  {
    get => this._LevelingDelay;
    set
    {
      if (this._LevelingDelay == value)
        return;
      this._LevelingDelay = value;
      this.PropertiesChanged();
    }
  }

  protected bool IsSubprojectWithoutTasks
  {
    get => this is Intermech.Project.Project project && project.IsSubProject && !this.SubTasks.Any<Task>();
  }

  /// <summary>Возвращает минимальный Start подзадач. При отсутствии подзадач возвращает DateTime.MinValue!</summary>
  protected virtual DateTime SubtasksStart
  {
    get
    {
      DateTime subtasksStart = DateTime.MaxValue;
      foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
      {
        if (subTask.Start < subtasksStart)
          subtasksStart = subTask.Start;
      }
      if (subtasksStart == DateTime.MaxValue)
        subtasksStart = DateTime.MinValue;
      return subtasksStart;
    }
  }

  protected virtual bool TaskManualPlanning
  {
    get => this._taskManualPlanning;
    set => this._taskManualPlanning = value;
  }

  /// <summary>Ручной режим планирования (реализован только для проектов)</summary>
  public virtual bool ManualPlanning
  {
    get => this._ManualPlanning;
    set => throw new NotImplementedException();
  }

  /// <summary>
  /// Содержит имя последнего свойства [Start,Finish,Duration], которое изменялось пользователем, это влияет на расчет длительности,
  /// когда пользователь через окно свойств одновременно устанавливает несколько свойств попарно (Start + Duration, Start + Finish) и т.д.
  /// </summary>
  [Intermech.Diagnostics.NotNull]
  public string DurationLock { get; set; } = nameof (Start);

  protected bool AutoDurationCalcAllowed
  {
    get
    {
      return this.ProjectLeftToRight ? this.ConstraintType == ConstraintType.StartNoEarlierThan || this.ConstraintType == ConstraintType.FinishNoEarlierThan : this.ConstraintType == ConstraintType.StartNoLaterThan || this.ConstraintType == ConstraintType.FinishNoLaterThan;
    }
  }

  public virtual DateTime Start
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Start.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Start.Value;
      }
      if (this.TaskManualPlanning)
        return this._Start;
      ++this._GetStartCounter;
      this.CheckForRecursion(this._GetStartCounter);
      try
      {
        if (this.HasLoadedSubTasks)
        {
          this._Start = this.SubtasksStart;
          if (this._Start == DateTime.MinValue)
          {
            Intermech.Project.Project rootProject = this.RootProject;
            if (rootProject != null)
              this._Start = rootProject.Start;
          }
          this.Cache.Start = new DateTime?(this._Start = this.AdjustByDependencies(this._Start, true));
        }
        else if (this.ProjectLeftToRight)
        {
          Intermech.Project.Project project = this.Project;
          Intermech.Project.Project rootProject = this.RootProject;
          if (project != null && rootProject != null && !rootProject.Partial && rootProject != this)
          {
            this.Cache.Start = new DateTime?(this._Start);
            Task.Graph projectGraph = rootProject.ProjectGraph;
            if (projectGraph != null)
            {
              Task.GraphNode graphNode = projectGraph.GetNode(this);
              if (graphNode != null)
              {
                bool flag = false;
                if (graphNode.StartNode != null)
                {
                  flag = true;
                  graphNode = (Task.GraphNode) graphNode.StartNode;
                }
                this.Cache.Start = new DateTime?(this._Start = graphNode.Date[this.LeftToRight]);
                if (!this.LeftToRight && !flag)
                  this.Cache.Start = new DateTime?(this._Start = this.AddWorkTime(this._Start, -this.Work));
              }
            }
          }
          this.Cache.Start = new DateTime?(this._Start = this.AdjustByDependencies(this._Start, true));
        }
        else
          this._Start = this.Milestone ? this.Finish : this.GetStart(this.Finish);
        this.CheckInProjectBounds(ref this._Start, true);
        this.Cache.Start = new DateTime?(this._Start);
      }
      finally
      {
        --this._GetStartCounter;
      }
      if (this.LeftToRight && this.LevelingDelay != 0.0)
        this.Cache.Start = new DateTime?(this._Start = this._Start.AddDays(this.LevelingDelay));
      return this._Start;
    }
    set
    {
      if (value == DateTime.MinValue)
        value = this.Project.Start;
      else if (value == DateTime.MaxValue)
        value = this.GetStart(this.Project.Finish);
      value = this.ApplyWorkingTime(value, true);
      if (!this.CanSetProperty(nameof (Start), (object) value))
        return;
      if (this.Project != null)
        this.Project.BeforeSetTaskProperty(this, nameof (Start), (object) value);
      DateTime start = this.Start;
      if (!(value != start))
        return;
      this.OnPropertyChanging(nameof (Start));
      this._Start = value;
      if (!this.HasState(TaskState.SettingConstraint) && !this.HasState(TaskState.Loading))
      {
        if (this.DurationLock == "Finish")
          this.DurationLock = string.Empty;
        if (this.DurationLock == string.Empty)
        {
          if (this.AutoDurationCalcAllowed)
          {
            try
            {
              this.TaskManualPlanning = true;
              double workHours = this.GetWorkHours(this._Start, this.Finish);
              if (workHours > 0.0)
              {
                string durationLock = this.DurationLock;
                this.Duration = workHours / this.CurrentSchedule.DayDuration;
                this.DurationLock = durationLock;
              }
            }
            finally
            {
              this.TaskManualPlanning = false;
            }
          }
        }
        this._ConstraintType = this.PlanningType == PlanningType.FromStart ? ConstraintType.StartNoEarlierThan : ConstraintType.StartNoLaterThan;
        this._startConstraint = DateTime.MinValue;
        this._finishConstraint = DateTime.MaxValue;
        this.ConstraintDate = this._Start;
        if (this.DurationLock == "Duration")
          this.DurationLock = nameof (Start);
      }
      this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies);
      this.OnPropertyChangeCompleted(nameof (Start));
    }
  }

  /// <summary>Возвращает максимальный Start подзадач. При отсутствии подзадач возвращает DateTime.MinValue!</summary>
  protected virtual DateTime SubtasksFinish
  {
    get
    {
      DateTime subtasksFinish = DateTime.MinValue;
      foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
      {
        if (subTask.Finish > subtasksFinish)
        {
          subtasksFinish = subTask.Finish;
          this._LatestTask = subTask;
        }
      }
      return subtasksFinish;
    }
  }

  public virtual DateTime Finish
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Finish.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Finish.Value;
      }
      if (this.TaskManualPlanning)
        return this._Finish;
      ++this._GetFinishCounter;
      this.CheckForRecursion(this._GetFinishCounter);
      try
      {
        if (this.HasLoadedSubTasks)
        {
          this.Cache.Finish = new DateTime?(this._Finish = this.SubtasksFinish);
          if (this._Finish == DateTime.MinValue)
          {
            Intermech.Project.Project rootProject = this.RootProject;
            if (rootProject != null)
              this._Finish = rootProject.Start;
          }
          this.Cache.Finish = new DateTime?(this._Finish = this.AdjustByDependencies(this._Finish, false));
        }
        else if (!this.ProjectLeftToRight)
        {
          Intermech.Project.Project project = this.Project;
          Intermech.Project.Project rootProject = this.RootProject;
          if (project != null && rootProject != null && !rootProject.Partial && rootProject != this)
          {
            this.Cache.Finish = new DateTime?(this._Finish);
            Task.Graph projectGraph = rootProject.ProjectGraph;
            if (projectGraph != null)
            {
              Task.GraphNode graphNode = projectGraph.GetNode(this);
              if (graphNode != null)
              {
                bool flag = false;
                if (graphNode.StartNode != null)
                {
                  flag = true;
                  graphNode = (Task.GraphNode) graphNode.StartNode;
                }
                this.Cache.Finish = new DateTime?(this._Finish = graphNode.Date[!this.LeftToRight]);
                if (this.LeftToRight && !flag)
                  this.Cache.Finish = new DateTime?(this._Finish = this.AddWorkTime(this._Finish, this.Work));
              }
            }
          }
          this.Cache.Finish = new DateTime?(this._Finish = this.AdjustByDependencies(this._Finish, false));
        }
        else
          this.Cache.Finish = !this.Milestone ? new DateTime?(this._Finish = this.GetFinish(this.Start)) : new DateTime?(this._Finish = this.Start);
        this.CheckInProjectBounds(ref this._Finish, false);
        this.Cache.Finish = new DateTime?(this._Finish);
      }
      finally
      {
        --this._GetFinishCounter;
      }
      if (!this.LeftToRight && this.LevelingDelay != 0.0)
        this.Cache.Finish = new DateTime?(this._Finish = this._Finish.AddDays(this.LevelingDelay));
      return this._Finish;
    }
    set
    {
      if (value.TimeOfDay.Ticks == 0L)
        value = value.AddDays(1.0).AddSeconds(-1.0);
      value = this.ApplyWorkingTime(value, false);
      if (!this.CanSetProperty(nameof (Finish), (object) value))
        return;
      if (this.Project != null)
        this.Project.BeforeSetTaskProperty(this, nameof (Finish), (object) value);
      DateTime finish = this.Finish;
      if (value != finish)
      {
        this.OnPropertyChanging(nameof (Finish));
        this._Finish = value;
        if (!this.HasState(TaskState.SettingConstraint) && !this.HasState(TaskState.Loading))
        {
          if (this.DurationLock == "Start")
            this.DurationLock = string.Empty;
          if (this.DurationLock == string.Empty)
          {
            if (this.AutoDurationCalcAllowed)
            {
              try
              {
                this.TaskManualPlanning = true;
                double num = this.CalcDuration(this._Finish);
                if (num > 0.0)
                {
                  string durationLock = this.DurationLock;
                  this.Duration = num;
                  this.DurationLock = durationLock;
                }
              }
              finally
              {
                this.TaskManualPlanning = false;
              }
            }
          }
          this._ConstraintType = this.PlanningType == PlanningType.FromStart ? ConstraintType.FinishNoEarlierThan : ConstraintType.FinishNoLaterThan;
          this._startConstraint = DateTime.MinValue;
          this._finishConstraint = DateTime.MinValue;
          this.ConstraintDate = this._Finish;
          if (this.DurationLock == "Duration")
            this.DurationLock = nameof (Finish);
        }
        this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies);
        this.OnPropertyChangeCompleted(nameof (Finish));
      }
      else
      {
        if (!this.HasState(TaskState.Loading))
          return;
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Start.HasValue ? 1 : 0) : 0) == 0)
          return;
        this._Cache.Start = new DateTime?();
      }
    }
  }

  public virtual DateTime StartConstraint
  {
    get => this._startConstraint;
    set
    {
      if (!(value != this.StartConstraint) || !this.CanSetProperty(nameof (StartConstraint), (object) value))
        return;
      this.OnPropertyChanging(nameof (StartConstraint));
      this._startConstraint = value;
      if (value == DateTime.MinValue)
        this._ConstraintType = this.DefaultConstraintType;
      this.PropertiesChanged(Task.CalcProps.StartConstraint);
      this.OnPropertyChangeCompleted(nameof (StartConstraint));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string StartConstraintString
  {
    get
    {
      return !(this.StartConstraint <= DateTime.MinValue) ? this.FormatDateTime(this.StartConstraint) : string.Empty;
    }
    set
    {
      if (!(value != this.StartConstraintString) || value == null)
        return;
      if (value.Length == 0)
        value = (string) null;
      this.StartConstraint = value != null ? this.ParseDateTime(value) : DateTime.MinValue;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string StartString
  {
    get => this.FormatDateTime(this.Start);
    set
    {
      if (!(value != this.StartString) || value == null)
        return;
      this.Start = this.ParseDateTime(value);
    }
  }

  [CanBeNull]
  public virtual object Tag
  {
    get => this._tag;
    set
    {
      if (value == this.Tag)
        return;
      this.OnPropertyChanging(nameof (Tag));
      this._tag = value;
      this.OnPropertyChanged(nameof (Tag));
      this.OnPropertyChangeCompleted(nameof (Tag));
    }
  }

  public virtual double Units
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Units.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Units.Value;
      }
      double num1 = 0.0;
      double num2 = !this.HasLoadedSubTasks ? num1 + this.Assignments.Sum<Assignment>((System.Func<Assignment, double>) (assignment => assignment.Units)) : 1.0;
      double units = num2 > 0.0 ? num2 : 1.0;
      this.Cache.Units = new double?(units);
      return units;
    }
    set
    {
      foreach (Task allTask in (IEnumerable<Task>) this.AllTasks)
      {
        if (!allTask.HasSubTasks)
        {
          Dictionary<Assignment, double> dictionary = new Dictionary<Assignment, double>();
          double num = 0.0;
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) allTask.Assignments)
          {
            double units;
            dictionary[assignment] = units = assignment.Units;
            num += units;
          }
          foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) allTask.Assignments)
            assignment.Units = dictionary[assignment] * value / num;
        }
      }
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string WbsCode
  {
    get
    {
      Task task = this.Parent;
      if (task != null && task.IsProjectSummaryTask)
        task = (Task) null;
      string str1 = string.Empty;
      if (task != null)
        str1 = task.WbsCode + IMProject.WbsCodeSeparator;
      string str2 = this._wbsCode == null ? (this.Project != null ? (new List<Task>((IEnumerable<Task>) (task?.SubTasks ?? this.Project.SubTasks)).IndexOf(this) + 1).ToString() : "0") : this._wbsCode;
      return str1 + str2;
    }
  }

  public bool RequestEdit(EditingMode mode, bool throwException)
  {
    if (this.EditingMode.HasFlag((Enum) mode))
      return true;
    bool flag = this.Status != 0;
    if (this is Intermech.Project.Project)
      flag = false;
    string str = string.Empty;
    if (!flag)
    {
      Intermech.Project.Project myProject = this.MyProject;
      if (myProject != null)
      {
        bool? nullable = new bool?(false);
        try
        {
          nullable = myProject.RequestEdit(this);
        }
        catch (Exception ex)
        {
          str = ex.Message;
        }
        if (!nullable.HasValue)
        {
          throwException = false;
        }
        else
        {
          flag = this.EditingMode.HasFlag((Enum) mode);
          if (!throwException && !flag)
            return true;
        }
      }
    }
    if (!flag & throwException)
    {
      if (str == string.Empty && !this.EditingMode.HasProperties() && mode == EditingMode.Properties)
        str = Resources.ErrNotPropertiesOwner;
      if (str != string.Empty)
        str = "\r\n" + str;
      throw new NotInEditModeException(string.Format(Resources.CheckoutTaskNeeded, (object) this.NameInMessages) + str);
    }
    return flag;
  }

  public virtual bool CanSetProperty([Intermech.Diagnostics.NotNull, NotEmpty] string name, [CanBeNull] object value, bool silent)
  {
    if (this.HasState(TaskState.Loading))
      return true;
    if (this.EditingLocked)
      return false;
    EditingMode editingMode = EditingMode.Properties;
    if (name == "Tasks")
      editingMode = EditingMode.Composition;
    if (silent)
    {
      if (!this.EditingMode.HasFlag((Enum) editingMode))
        return false;
    }
    else if (!this.RequestEdit(editingMode, true))
      return false;
    Dictionary<string, List<double>> dictionary = Task._propRanges;
    if (dictionary == null)
      dictionary = new Dictionary<string, List<double>>()
      {
        {
          "Work",
          new List<double>((IEnumerable<double>) new double[2]
          {
            0.0,
            IMProject.MaximumTaskWork
          })
        },
        {
          "PercentCompleted",
          new List<double>((IEnumerable<double>) new double[2]
          {
            0.0,
            100.0
          })
        },
        {
          "Priority",
          new List<double>((IEnumerable<double>) new double[2]
          {
            0.0,
            1000.0
          })
        }
      };
    Task._propRanges = dictionary;
    string str = Localization.GetString("TaskParam" + name);
    if (this.Milestone && Task._milestoneRoProps.Contains(name))
    {
      if (silent)
        return false;
      throw new NotificationException(string.Format(Resources.CantSetMilestoneProperty, (object) str));
    }
    bool flag = this.HasSubTasks;
    if (flag && this is Intermech.Project.Project)
      flag = false;
    if (flag && Task._parentRoProps.Contains(name))
    {
      if (name == "Work" && this.HasState(TaskState.Loading) || this.HasState(TaskState.Loading) && this.Partial)
        return true;
      if (silent)
        return false;
      throw new NotificationException(string.Format(Resources.CantSetSummaryProperty, (object) str));
    }
    List<double> doubleList;
    if (Task._propRanges.TryGetValue(name, out doubleList))
    {
      double num = Convert.ToDouble(value);
      if (num < doubleList[0] || num > doubleList[1])
      {
        if (silent)
          return false;
        throw new ArgumentOutOfRangeException(name, $"{Resources.AllowedParameterRange} \"{str}\": [{(object) doubleList[0]}..{(object) doubleList[1]}]");
      }
    }
    if (name == "TaskColor" && object.Equals(value, (object) Color.Empty))
      throw new ArgumentOutOfRangeException("TaskColor", "Пустой цвет нельзя назначать!");
    return true;
  }

  public bool CanSetProperty([Intermech.Diagnostics.NotNull] string name, [CanBeNull] object value)
  {
    return this.CanSetProperty(name, value, this._SilentMode);
  }

  internal void PropertiesChanged(bool checkParent)
  {
    this.PropertiesChanged(Task.CalcProps.All, checkParent);
  }

  protected internal virtual void PropertiesChanged(Task.CalcProps props = Task.CalcProps.All, bool checkParent = true)
  {
    HashSet<Task> processed = new HashSet<Task>();
    this.PropertiesChanged(props, checkParent, processed);
  }

  internal virtual void PropertiesChanged(
    Task.CalcProps props,
    bool checkParent,
    [Intermech.Diagnostics.NotNull] HashSet<Task> processed)
  {
    int num;
    if (this.RaisePropertyChangedEvents && !this.HasState(TaskState.Loading))
    {
      Intermech.Project.Project project = this.Project;
      num = project != null ? (project.HasState(TaskState.Loading) ? 1 : 0) : 0;
    }
    else
      num = 1;
    bool flag1 = num != 0;
    if (processed.Contains(this))
      return;
    UniqueList<string> uniqueList = new UniqueList<string>();
    bool flag2 = false;
    if ((props & Task.CalcProps.FinishConstraint) != (Task.CalcProps) 0)
    {
      uniqueList.Add("FinishConstraint");
      uniqueList.Add("FinishConstraintString");
      props |= Task.CalcProps.Position;
    }
    if ((props & Task.CalcProps.StartConstraint) != (Task.CalcProps) 0)
    {
      uniqueList.Add("StartConstraint");
      uniqueList.Add("StartConstraintString");
      props |= Task.CalcProps.Position;
    }
    if ((props & Task.CalcProps.Assignment) != (Task.CalcProps) 0)
    {
      uniqueList.Add("AssignmentsString");
      uniqueList.Add("ChiefString");
      uniqueList.Add("Units");
      props |= Task.CalcProps.Work;
      props |= Task.CalcProps.BackDependencies;
    }
    if ((props & Task.CalcProps.Work) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Work");
      uniqueList.Add("WorkString");
      uniqueList.Add("CompletedWork");
      uniqueList.Add("CompletedWorkString");
      uniqueList.Add("RemainingWork");
      uniqueList.Add("RemainingWorkString");
      uniqueList.Add("PercentCompleted");
      uniqueList.Add("PercentCompletedString");
      flag2 = true;
      props |= Task.CalcProps.Position;
      props |= Task.CalcProps.Cost;
    }
    if ((props & Task.CalcProps.Schedule) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Schedule");
      props |= Task.CalcProps.Position;
      props |= Task.CalcProps.Cost;
    }
    if ((props & Task.CalcProps.Indent) == Task.CalcProps.Indent)
    {
      uniqueList.Add("IndentLevel");
      uniqueList.Add("Schedule");
      props |= Task.CalcProps.Dependencies;
      props |= Task.CalcProps.Position;
    }
    if ((props & Task.CalcProps.Position) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Start");
      uniqueList.Add("StartString");
      uniqueList.Add("Duration");
      uniqueList.Add("DurationString");
      uniqueList.Add("Finish");
      uniqueList.Add("FinishString");
      uniqueList.Add("WorkTime");
      flag2 = true;
    }
    if ((props & Task.CalcProps.Cost) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Cost");
      uniqueList.Add("CostString");
    }
    if ((props & Task.CalcProps.Estimation) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Estimation");
      uniqueList.Add("WorkString");
      uniqueList.Add("RemainingWorkString");
      uniqueList.Add("DurationString");
      uniqueList.Add("FinishString");
      uniqueList.Add("CostString");
    }
    if ((props & Task.CalcProps.Other) != (Task.CalcProps) 0)
      uniqueList.Add("WbsCode");
    if ((props & Task.CalcProps.Dependencies) == Task.CalcProps.Dependencies)
    {
      uniqueList.Add("Tasks");
      uniqueList.Add("SubTasks");
      uniqueList.Add("AllSubTasks");
      uniqueList.Add("HasSubTasks");
    }
    if ((props & Task.CalcProps.PercentCompleted) != (Task.CalcProps) 0)
    {
      uniqueList.Add("Estimation");
      uniqueList.Add("PercentCompleted");
      uniqueList.Add("PercentCompletedString");
      uniqueList.Add("Status");
      uniqueList.Add("StatusString");
      uniqueList.Add("Completed");
      uniqueList.Add("CompletedWork");
      uniqueList.Add("CompletedWorkString");
      uniqueList.Add("RemainingWork");
      uniqueList.Add("RemainingWorkString");
    }
    if (flag1)
    {
      if (this._Cache == null)
        return;
      foreach (string valueName in (List<string>) uniqueList)
      {
        if (valueName != null)
          this._Cache.ResetValue(valueName);
      }
    }
    else
    {
      if (flag2)
      {
        Intermech.Project.Project rootProject = this.RootProject;
        if (rootProject != null && (rootProject != this || (props & Task.CalcProps.Position) != (Task.CalcProps) 0))
          rootProject.ClearGraph();
      }
      List<Task> taskList = new List<Task>();
      Task task1 = this;
      do
      {
        if (!processed.Contains(task1))
        {
          processed.Add(task1);
          bool propertyChangedEvents = task1.RaisePropertyChangedEvents;
          task1.RaisePropertyChangedEvents = false;
          try
          {
            foreach (string property in (List<string>) uniqueList)
              task1.OnPropertyChanged(property);
            if (!taskList.Contains(task1))
              taskList.Add(task1);
          }
          finally
          {
            task1.RaisePropertyChangedEvents = propertyChangedEvents;
          }
          if ((props & Task.CalcProps.Dependencies) != (Task.CalcProps) 0)
          {
            foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.Dependencies)
            {
              if (dependency.DependentOfTask != null)
              {
                foreach (Task allTask in (IEnumerable<Task>) dependency.DependentOfTask.AllTasks)
                {
                  if (!processed.Contains(allTask))
                    allTask.PropertiesChanged(props, checkParent, processed);
                }
              }
            }
          }
          if ((props & Task.CalcProps.BackDependencies) != (Task.CalcProps) 0)
          {
            foreach (Dependency backDependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.BackDependencies)
            {
              if (backDependency.Task != null)
              {
                foreach (Task allTask in (IEnumerable<Task>) backDependency.Task.AllTasks)
                {
                  if (!processed.Contains(allTask))
                    allTask.PropertiesChanged(props, checkParent, processed);
                }
              }
            }
          }
        }
        task1 = checkParent ? task1.Parent ?? (Task) task1.Project : (Task) null;
      }
      while (task1 != null);
      if (Entity.InGlobalUpdate)
        return;
      foreach (Task task2 in taskList)
      {
        foreach (string property in (List<string>) uniqueList)
          task2.FirePropertyChanged(property);
      }
    }
  }

  public virtual double Work
  {
    get
    {
      if (this.UseCache)
      {
        Task.TaskCache cache = this._Cache;
        if ((cache != null ? (cache.Work.HasValue ? 1 : 0) : 0) != 0)
          return this._Cache.Work.Value;
      }
      if (this.Milestone)
      {
        this.Cache.Work = new double?(0.0);
        return 0.0;
      }
      if (!this.HasLoadedSubTasks)
      {
        if (this._Work > 0.0)
        {
          this.Cache.Work = new double?(this._Work);
          return this._Work;
        }
        return !(this is Intermech.Project.Project) ? IMProject.DefaultWorkDuration : 0.0;
      }
      double work = this.SubTasks.Sum<Task>((System.Func<Task, double>) (task => task.Work));
      this.Cache.Work = new double?(work);
      return work;
    }
    set
    {
      if (value == this.Work || !this.CanSetProperty(nameof (Work), (object) value))
        return;
      double completedWork = this.CompletedWork;
      this.OnPropertyChanging(nameof (Work));
      this._Work = value;
      if (this.PercentCompleted != 100.0)
        this.CompletedWork = Math.Min(completedWork, this._Work);
      this.PropertiesChanged(Task.CalcProps.Work | Task.CalcProps.BackDependencies);
      this.OnPropertyChangeCompleted(nameof (Work));
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string WorkString
  {
    get => this.FormatDurationH(this.Work, this.Estimation);
    set
    {
      if (!(value != this.WorkString) || value == null)
        return;
      this.Work = this.ParseDurationH(value);
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual DateScheduleList WorkTime
  {
    get
    {
      if (this.UseCache && this._Cache?.WorkTime != null)
        return this._Cache.WorkTime;
      DateScheduleList workTime = !this.HasLoadedSubTasks ? (this.LeftToRight ? this.GetWorkTime(this.Start, this.Work) : this.GetWorkTime(this.Finish, -this.Work)) : new DateScheduleList();
      if (this.HasLoadedSubTasks)
      {
        foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
        {
          foreach (DateSchedule dateSchedule in (List<DateSchedule>) subTask.WorkTime)
            workTime.Add(dateSchedule);
        }
      }
      this.Cache.WorkTime = workTime;
      return workTime;
    }
  }

  public override string ToString() => this.Name;

  public EditingMode EditingMode
  {
    get => !this.HasState(TaskState.Copying) ? this._editingMode : EditingMode.Edit;
    set => this._editingMode = value;
  }

  /// <summary>Устанавливается задачам при их добавлении в проект</summary>
  protected internal virtual EditingMode DefaultEditingMode
  {
    [DebuggerStepThrough] get => EditingMode.Edit;
  }

  /// <summary>Был ли объект взят на изменение перед началом редактирования</summary>
  protected bool WasCheckedOut => true;

  internal bool CheckOutNeeded
  {
    get
    {
      return (this.EditingMode.Any() && !this.PseudoCheckedOut || this.HasState(TaskState.Copying)) && this.Status == TaskStatus.NotStarted;
    }
  }

  /// <summary>Режим, когда объект с другого узла, сам не может быть взят на изменение, но его подзадачи берутся и сдаются вместо него</summary>
  protected bool PseudoCheckedOut
  {
    [DebuggerStepThrough] get => this._PseudoCheckedOut;
  }

  public void CheckOut()
  {
    this.GetObject(true);
    try
    {
      this.EditingMode = EditingMode.Edit;
      this.CheckOut(ref this._Object);
    }
    catch
    {
      this.EditingMode = EditingMode.None;
      throw;
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  protected internal virtual bool CheckOut([Intermech.Diagnostics.NotNull] ref IDBObject obj)
  {
    if (!this.CheckOutNeeded)
    {
      this._WasCheckedOut = new bool?(false);
      return false;
    }
    if (obj.CheckoutBy != this.CurrentUserObjectID)
      return this.DoCheckOut(ref obj);
    Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.SiteID);
    if ((int) siteId.CompositionOwner != (int) siteId.CurrentSite)
      this.EditingMode &= ~EditingMode.Composition;
    if (!this._WasCheckedOut.HasValue)
      this._WasCheckedOut = new bool?(true);
    return false;
  }

  protected virtual bool DoCheckOut([Intermech.Diagnostics.NotNull] ref IDBObject obj)
  {
    if (obj.LCStep == (int) (IpsMetadataEntityBase<int>) LCStep.Imported)
      return true;
    obj = obj.CheckOut();
    this._ObjectID = obj.ObjectID;
    if (!this._WasCheckedOut.HasValue)
      this._WasCheckedOut = new bool?(false);
    this.DoNotification(Task.EventKind.CheckOut, -this.ObjectID, this.ObjectID);
    return true;
  }

  public void DoNotification(Task.EventKind kind, long objectID)
  {
    this.DoNotification(kind, objectID, 0L);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void DoNotification(Task.EventKind kind, long objectID, long newObjectID)
  {
    Task._GlobalNotifier?.Notify((object) this, kind, objectID, newObjectID);
  }

  public DateTime ConstraintDate
  {
    get
    {
      switch (this._ConstraintType)
      {
        case ConstraintType.StartNoEarlierThan:
        case ConstraintType.StartNoLaterThan:
          return this.StartConstraint;
        case ConstraintType.FinishNoEarlierThan:
        case ConstraintType.FinishNoLaterThan:
          return this.FinishConstraint;
        default:
          return DateTime.MinValue;
      }
    }
    set
    {
      switch (this._ConstraintType)
      {
        case ConstraintType.StartNoEarlierThan:
        case ConstraintType.StartNoLaterThan:
          this.StartConstraint = value;
          break;
        case ConstraintType.FinishNoEarlierThan:
        case ConstraintType.FinishNoLaterThan:
          this.FinishConstraint = value;
          break;
      }
    }
  }

  protected ConstraintType DefaultConstraintType
  {
    get
    {
      Intermech.Project.Project project = this.Project;
      return project == null ? ConstraintType.AsSoonAsPossible : project.ConstraintType;
    }
  }

  public virtual ConstraintType ConstraintType
  {
    get => this._ConstraintType;
    set
    {
      if (this.HasState(TaskState.SettingConstraint))
        return;
      this._State |= TaskState.SettingConstraint;
      try
      {
        if (this.HasSubTasks || this._ConstraintType == value || !this.CanSetProperty(nameof (ConstraintType), (object) value))
          return;
        this.OnPropertyChanging(nameof (ConstraintType));
        this._ConstraintType = value;
        switch (this._ConstraintType)
        {
          case ConstraintType.AsSoonAsPossible:
            this._startConstraint = DateTime.MinValue;
            this._finishConstraint = DateTime.MaxValue;
            this._Start = DateTime.MinValue;
            break;
          case ConstraintType.AsLateAsPossible:
            this._startConstraint = DateTime.MinValue;
            this._finishConstraint = DateTime.MaxValue;
            this._Start = DateTime.MaxValue;
            break;
          case ConstraintType.StartNoEarlierThan:
          case ConstraintType.StartNoLaterThan:
            this._finishConstraint = DateTime.MaxValue;
            this._startConstraint = this.Start;
            break;
          case ConstraintType.FinishNoEarlierThan:
          case ConstraintType.FinishNoLaterThan:
            this._startConstraint = DateTime.MinValue;
            this._finishConstraint = this.Finish;
            break;
        }
        this.OnPropertyChanged(nameof (ConstraintType));
        this.OnPropertyChanged("ConstraintDate");
        this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.FinishConstraint | Task.CalcProps.StartConstraint | Task.CalcProps.BackDependencies);
      }
      finally
      {
        this._State ^= TaskState.SettingConstraint;
      }
    }
  }

  public bool ConstraintMet
  {
    get
    {
      switch (this._ConstraintType)
      {
        case ConstraintType.StartNoEarlierThan:
          return this.Start >= this.StartConstraint;
        case ConstraintType.StartNoLaterThan:
          return this.Start <= this.StartConstraint;
        case ConstraintType.FinishNoEarlierThan:
          return this.Finish >= this.LastWorkingTime(this.FinishConstraint);
        case ConstraintType.FinishNoLaterThan:
          return this.Finish <= this.FinishConstraint;
        default:
          return false;
      }
    }
  }

  public virtual PlanningType PlanningType
  {
    get
    {
      Intermech.Project.Project project = this.Project;
      return project == null ? PlanningType.FromStart : project.PlanningType;
    }
    set
    {
    }
  }

  protected internal override void OnPropertyChangeCompleted(string property)
  {
    base.OnPropertyChangeCompleted(property);
    Intermech.Project.Project rootProject = this.RootProject;
    rootProject?.TaskPropertyChangeCompleted(this, property);
    for (Intermech.Project.Project project = this.MyProject; project != null; project = project.Project)
    {
      if (project != rootProject)
        project.TaskPropertyChangeCompleted(this, property);
    }
  }

  public virtual bool LeftToRight
  {
    get
    {
      switch (this.ConstraintType)
      {
        case ConstraintType.AsSoonAsPossible:
          return true;
        case ConstraintType.AsLateAsPossible:
          return false;
        default:
          return this.ProjectLeftToRight;
      }
    }
  }

  public bool ProjectLeftToRight
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Project.Project rootProject = this.RootProject;
      return rootProject == null || rootProject.LeftToRight;
    }
  }

  protected DateTime NextWorkingTime(DateTime dt)
  {
    double num = (double) dt.Hour + (double) dt.Minute / 60.0;
    DateTime dateTime1 = dt.AddDays(1.0);
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) this.GetWorkTime(dt, 1.0))
    {
      DayTimeIntervalCollection intervalCollection = dateSchedule.TimeIntervalCollection;
      if (num < intervalCollection.Start)
        num = intervalCollection.Start;
      if (dateSchedule.Date.Date > dt.Date)
      {
        DateTime date = dateSchedule.Date;
        date = date.Date;
        dateTime1 = date.AddHours(intervalCollection.Start);
        break;
      }
      if (num >= intervalCollection.Start && num < intervalCollection.Finish)
      {
        DateTime dateTime2 = dt.Date.AddHours(num);
        if (dateTime2 < dateTime1)
        {
          dateTime1 = dateTime2;
          break;
        }
      }
    }
    return dateTime1;
  }

  protected DateTime LastWorkingTime(DateTime dt)
  {
    double num = (double) dt.Hour + (double) dt.Minute / 60.0;
    DateTime dateTime1 = dt;
    if (dt.Date > new DateTime(1, 1, 1))
      dateTime1 = dt.AddDays(-1.0);
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) this.GetWorkTime(dt, -1.0))
    {
      DayTimeIntervalCollection intervalCollection = dateSchedule.TimeIntervalCollection;
      double finish = intervalCollection.Finish;
      if (num == 0.0 || num > finish)
        num = finish;
      if (dateSchedule.Date.Date < dt.Date)
      {
        DateTime date = dateSchedule.Date;
        date = date.Date;
        dateTime1 = date.AddHours(finish);
        break;
      }
      if (num > intervalCollection.Start && num <= finish)
      {
        DateTime dateTime2 = dt.Date.AddHours(num);
        if (dateTime2 > dateTime1)
        {
          dateTime1 = dateTime2;
          break;
        }
        break;
      }
    }
    return dateTime1;
  }

  public void ValidateInWorkTime(ref DateTime dt) => dt = this.NextWorkingTime(dt);

  [Intermech.Diagnostics.NotNull]
  public virtual string DateFormat
  {
    get
    {
      Intermech.Project.Project rootProject = this.RootProject;
      return rootProject != null && rootProject != this ? rootProject.DateFormat : "dd.MM.yy H:mm";
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string FormatDateTime(DateTime dt)
  {
    return StringFuncs.UCFirst(dt.ToString(this.DateFormat));
  }

  [Intermech.Diagnostics.NotNull]
  protected static Regex RemoveShortDayRegex
  {
    get
    {
      return Task._removeShortDayRegex ?? (Task._removeShortDayRegex = new Regex("^[^0-9\\s]* ", RegexOptions.IgnoreCase | RegexOptions.Compiled));
    }
  }

  public DateTime ParseDateTime([Intermech.Diagnostics.NotNull] string value)
  {
    string format = this.DateFormat;
    if (format.StartsWith("ddd "))
    {
      value = Task.RemoveShortDayRegex.Replace(value, string.Empty);
      format = format.Substring(4);
    }
    try
    {
      return DateTime.ParseExact(value, format, (IFormatProvider) CultureInfo.CurrentCulture);
    }
    catch (FormatException ex)
    {
      return DateTime.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string NameInMessages
  {
    get
    {
      string nameInMessages = this.Name;
      if (nameInMessages == string.Empty)
        nameInMessages = $"(ID={this.ObjectID})";
      Task task = this.Parent;
      if (task != null)
      {
        if (task.IsProjectSummaryTask)
          task = (Task) null;
        if (task != null)
          nameInMessages = $"{task.NameInMessages}\\{nameInMessages}";
      }
      return nameInMessages;
    }
  }

  public virtual void AssignProperties([Intermech.Diagnostics.NotNull] Task src)
  {
    this._SessionProvider = src._SessionProvider;
  }

  public virtual IUserSession GetSession()
  {
    if (this._SessionProvider != null)
      return this._SessionProvider.GetSession();
    return this.Project != null ? this.Project.GetSession() : throw new Exception("Session needed!");
  }

  public virtual bool ReleaseSession()
  {
    ISessionProvider sessionProvider = this._SessionProvider;
    if (sessionProvider != null)
      return sessionProvider.ReleaseSession();
    Intermech.Project.Project project = this.Project;
    return project != null && project.ReleaseSession();
  }

  /// <summary>Задача не была полностью загружена (без проекта или проект без подзадач)</summary>
  public bool Partial
  {
    [DebuggerStepThrough] get => this._Partial;
  }

  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
  [CanBeNull]
  public string GetUserName([NotEmpty] long id, bool throwExceptionIfNotFound = false)
  {
    string caption;
    if (!Task._userNameCache.TryGetValue(id, out caption))
    {
      IUserSession session = this.GetSession();
      try
      {
        IDBObject dbObject = session.GetObject(id, false);
        if (dbObject != null)
        {
          caption = dbObject.Caption;
          Task._userNameCache.Add(id, caption);
        }
      }
      finally
      {
        this.ReleaseSession();
      }
    }
    return !(caption == null & throwExceptionIfNotFound) ? caption : throw new InvalidOperationException($"User with id={id} not found");
  }

  internal void RegisterUncompletedTimer()
  {
    DateTime date = this.Finish;
    IUserSession session = this.GetSession();
    try
    {
      IProjectTimers customService = session.GetCustomService<IProjectTimers>(false);
      if (customService == null)
        return;
      customService.Remove(session.SessionGUID, this.ObjectID, ProjectTimerKind.FinishNotification);
      if (date <= DateTime.Now)
      {
        this.SendOverdueNotification();
      }
      else
      {
        date = date.ToUniversalTime();
        customService.Add(session.SessionGUID, this.ObjectID, date, ProjectTimerKind.FinishNotification);
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  internal void DeleteUncompletedTimer()
  {
    IUserSession session = this.GetSession();
    try
    {
      session.GetCustomService<IProjectTimers>(false)?.Remove(session.SessionGUID, this.ObjectID, ProjectTimerKind.FinishNotification);
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  public bool ConvertTo<T>() where T : Task
  {
    if (!this.CanSetProperty("null", (object) null))
      return false;
    Type type = typeof (T);
    if (this is Intermech.Project.Project && this.HasNotLoadedSubTasks)
      this.LoadSubTasks();
    IUserSession session = this.GetSession();
    try
    {
      Intermech.Project.Project rootProject = this.RootProject;
      if ((rootProject != null ? (!rootProject.Save(session) ? 1 : 0) : 0) != 0)
        return false;
      this.GetObject();
      try
      {
        List<long> longList = new List<long>();
        if (this is Intermech.Project.Project project1)
          longList.AddRange(project1.Tasks.SelectMany<Task, long>((System.Func<Task, IEnumerable<long>>) (t => Enumeration.Create<long>(t.ObjectID).Concat<long>(t.Dependencies.Select<Dependency, long>((System.Func<Dependency, long>) (d => d.ObjectID))))));
        this.CheckIn();
        foreach (long num in longList)
          session.GetObject(Math.Abs(num)).CheckOut();
        this.GetObject(true);
        try
        {
          Intermech.Project.Project project2 = (Intermech.Project.Project) null;
          int lcStep1 = this._Object.LCStep;
          if (type == typeof (Intermech.Project.Project) || type.IsSubclassOf(typeof (Intermech.Project.Project)))
          {
            project2 = new Intermech.Project.Project(this.ObjectID);
            project2._Project = this.Project;
            this._Object.ObjectType = (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project;
          }
          else if (type == typeof (Task) || type.IsSubclassOf(typeof (Task)))
          {
            project2 = this.Project;
            this._Object.ObjectType = (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task;
          }
          int lcStep2 = this._Object.LCStep;
          if (lcStep1 != lcStep2)
            this._Object.LCStep = lcStep1;
          if (project2 != null)
          {
            this.Dependencies._Modified = true;
            this.Modified = true;
            this.EditingMode = EditingMode.Edit;
            foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
            {
              subTask.Project = project2;
              subTask.Dependencies._Modified = true;
              subTask.Modified = true;
            }
            this.ProjectNeeded();
            this.Project.Save(session);
          }
          return true;
        }
        finally
        {
          this.ReleaseObject();
        }
      }
      finally
      {
        this.ReleaseObject();
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  internal virtual void RecalcStartFinish()
  {
    this.PropertiesChanged(Task.CalcProps.Position | Task.CalcProps.BackDependencies);
  }

  public DateTime FactStart { get; private set; } = DateTime.MinValue;

  public DateTime FactFinish { get; private set; } = DateTime.MinValue;

  [CanBeEmpty]
  public long VerifySchemeID
  {
    [DebuggerStepThrough] get => this._verifySchemeID;
    set
    {
      if (this._verifySchemeID == value || !this.CanSetProperty(nameof (VerifySchemeID), (object) value))
        return;
      this._verifySchemeID = value;
      this.OnPropertyChanged(nameof (VerifySchemeID));
    }
  }

  /// <summary>Цвет задачи если он указан в карточке задачи. Если null - используется алгоритм подбора цвета по-умолчанию.</summary>
  [CanBeNull]
  public Color? TaskColor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._taskColor;
    set
    {
      Color? taskColor = this._taskColor;
      Color? nullable = value;
      if ((taskColor.HasValue == nullable.HasValue ? (taskColor.HasValue ? (taskColor.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 || !this.CanSetProperty(nameof (TaskColor), (object) value))
        return;
      this._taskColor = value;
      this.OnPropertyChanged(nameof (TaskColor));
    }
  }

  public virtual void DebugClearCache() => this._Cache?.Clear();

  /// <summary>В отличие от Project, для проекта возвратит самого себя</summary>
  [CanBeNull]
  internal Intermech.Project.Project MyProject => this is Intermech.Project.Project project ? project : this.Project;

  public bool EditingLocked => this.Status == TaskStatus.Completed;

  public bool ReadOnly => !this.EditingMode.HasProperties() || this.EditingLocked;

  public virtual bool Grayed => !this.EditingMode.Any() || this.EditingLocked;

  internal virtual void StartTransaction()
  {
  }

  /// <summary>Вызывается после успешного сохранения транзакции сохранения проекта</summary>
  internal virtual void Commit()
  {
    this.Modified = false;
    this.Dependencies.Commit();
    this.Assignments.Commit();
    if (this._attachments != null)
      this.Attachments.Modified = false;
    if (!this._justCreated)
      return;
    this._justCreated = false;
  }

  /// <summary>Вызывается при откате транзакции сохранения проекта</summary>
  internal virtual void Rollback()
  {
    if (this._justCreated)
      this._ObjectID = 0L;
    this.Dependencies.Rollback();
    this.Assignments.Rollback();
    if (!this._prevAttachmentsModified)
      return;
    this.Attachments.Modified = this._prevAttachmentsModified;
  }

  [Intermech.Diagnostics.NotNull]
  public static List<ColumnDescriptor> FullCompositionColumns
  {
    get
    {
      if (Task._fcColumns == null)
      {
        Task._fcColumns = new List<ColumnDescriptor>();
        Task._fcColumns.Add(new ColumnDescriptor((object) -2));
        Task._fcColumns.Add(new ColumnDescriptor((object) -20));
        Task._fcColumns.Add(new ColumnDescriptor((object) -7));
        Task._fcColumns.Add(new ColumnDescriptor((object) Intermech.Metadata.Attributes.SortIndex.ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1));
        Task._fcColumns.Add(new ColumnDescriptor((object) -50));
        Task._fcColumns.Add(new ColumnDescriptor((object) -4));
        Task._fcColumns.Add(new ColumnDescriptor((object) -21));
        Task._fcColumns.Add(new ColumnDescriptor((object) -6));
        Task._fcColumns.Add(new ColumnDescriptor((object) -8));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.ConstraintType.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.ConstraintDate.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.PlanStart.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.PlanFinish.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.PlanDuration.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.PlanWork.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Intermech.Metadata.Attributes.Description.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.PercentCompleted.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.Flags.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.TaskPriority.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.FactStart.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.FactFinish.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.VerifyScheme.ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.TaskColor.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.ImportedRootObjectGuid.ID, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.ImportedObject.ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._fcColumns.Add(new ColumnDescriptor((object) Attributes.ImportedRelationGuid.ID, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Default, SortOrders.NONE, -1));
        Task._attr2ColumnIndex = new Dictionary<int, int>();
        for (int index = 0; index < Task._fcColumns.Count; ++index)
          Task._attr2ColumnIndex.Add(Convert.ToInt32(Task._fcColumns[index].AttributeID), index);
      }
      return Task._fcColumns;
    }
  }

  protected int Attr2Col(int attributeID)
  {
    int num;
    return !Task._attr2ColumnIndex.TryGetValue(attributeID, out num) ? -1 : num;
  }

  protected int Attr2Col(ObligatoryObjectAttributes attributeID)
  {
    int num;
    return !Task._attr2ColumnIndex.TryGetValue((int) attributeID, out num) ? -1 : num;
  }

  private bool CalcHidden()
  {
    this._hiddenByFilter = new bool?(false);
    Intermech.Project.Project rootProject = this.RootProject;
    if (rootProject?.Filter != null)
    {
      if (rootProject.Filter.HasFlag(FilterFlags.ShowSummaryTasks) && this.HasSubTasks && this.AllSubTasks.Any<Task>())
      {
        this._hiddenByFilter = new bool?(false);
        return this._hiddenByFilter.Value;
      }
      try
      {
        this._hiddenByFilter = new bool?(!Intermech.Project.Evaluator.Evaluator.Eval(this, rootProject.Filter));
      }
      catch (Exception ex)
      {
        rootProject._FilterError = ex.InnerException?.Message ?? ex.Message ?? string.Empty;
      }
    }
    else
      this._hiddenByFilter = new bool?(false);
    return this._hiddenByFilter.Value;
  }

  public bool HiddenByFilter
  {
    get
    {
      if (!this._hiddenByFilter.HasValue)
      {
        if (Intermech.Project.Evaluator.Evaluator.Busy)
          return false;
        this.CalcHidden();
      }
      return this._hiddenByFilter ?? false;
    }
  }

  internal void ClearHidden() => this._hiddenByFilter = new bool?();

  public bool Uncommitted
  {
    [DebuggerStepThrough] get => this._Uncommitted;
    set
    {
      if (this._Uncommitted == value)
        return;
      this._Uncommitted = value;
      if (this._Uncommitted)
        return;
      Entity.GlobalBeginUpdate();
      try
      {
        this.PropertiesChanged();
      }
      finally
      {
        Entity.GlobalEndUpdate();
      }
    }
  }

  public bool Minimized { get; set; }

  internal bool IsParentMinimized
  {
    get
    {
      for (Task parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent.Minimized || parent.IsParentMinimized)
          return true;
      }
      return false;
    }
  }

  /// <summary>Возвращает True, если задача скрыта фильтром, или находится в minimizedTasks (включая родительские задачи))</summary>
  public bool IsHidden
  {
    get
    {
      bool isHidden = this.HiddenByFilter;
      if (!isHidden)
      {
        for (Task parent = this.Parent; parent != null; parent = parent.Parent)
        {
          if (parent.Minimized)
          {
            isHidden = true;
            break;
          }
        }
      }
      return isHidden;
    }
  }

  public int RowHeight { get; set; }

  /// <summary>Определяет, все ли задачи, от которых зависит текущая задача, выполнены</summary>
  public bool DependenciesCompleted
  {
    get
    {
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
      {
        Task dependentOfTask = dependency.DependentOfTask;
        switch (dependency.DependencyType)
        {
          case DependencyType.FinishStart:
            if (dependentOfTask == null || dependentOfTask.Status == TaskStatus.Completed)
              continue;
            break;
          case DependencyType.StartStart:
            if (dependentOfTask == null || dependentOfTask.Status >= TaskStatus.Executed)
              continue;
            break;
          default:
            continue;
        }
        return false;
      }
      return true;
    }
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  protected bool IsChief
  {
    [DebuggerStepThrough] get => this.CurrentUserObjectID == this.ChiefID;
  }

  protected void CheckChiefOnly()
  {
    this.GetObject();
    try
    {
      if (!this.IsChief)
        throw new NotificationException(Localization.GetString("ErrChiefNeeded", (object) this.ChiefName));
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  public bool IsChildOf([Intermech.Diagnostics.NotNull] Task t) => t.AllTasks.Contains<Task>(this);

  [Intermech.Diagnostics.NotNull]
  public string ErrorString { get; internal set; } = string.Empty;

  /// <summary>Вычисляет плановый процент выполнения задачи на текущий момент времени</summary>
  public virtual double PlannedPercentCompleted
  {
    get
    {
      DateTime now = DateTime.Now;
      if (now > this.Finish)
        return 100.0;
      if (now < this.Start)
        return 0.0;
      double num = this.GetWorkTime(this.Start, this.Finish).Sum<DateSchedule>((System.Func<DateSchedule, double>) (ds => ds.Work));
      if (num != 0.0)
        return Math.Min(100.0, this.GetWorkTime(this.Start, now).Sum<DateSchedule>((System.Func<DateSchedule, double>) (ds => ds.Work)) * 100.0 / num);
      TimeSpan timeSpan = this.Finish - this.Start;
      long ticks = timeSpan.Ticks;
      if (ticks <= 0L)
        return 0.0;
      timeSpan = now - this.Start;
      return Math.Min(100.0, 100.0 * (double) timeSpan.Ticks / (double) ticks);
    }
  }

  [Intermech.Diagnostics.NotNull]
  public string PlannedPercentCompletedString
  {
    get => $"{this.PlannedPercentCompleted:0.##}{IMProject.PercentSymbol}";
  }

  public void SetRuntimeFlag([Intermech.Diagnostics.NotNull] IDBObject obj, RuntimeFlags flag, bool set = true)
  {
    if (!(obj is IRuntimeFlags runtimeFlags))
      return;
    if (set)
      runtimeFlags.Set(flag);
    else
      runtimeFlags.Unset(flag);
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  public long CurrentUserObjectID
  {
    [DebuggerStepThrough] get
    {
      Intermech.Project.Project project = this.Project;
      if (project != null && this != project)
        return project.CurrentUserObjectID;
      if (this._CurrentUserObjectID.HasValue)
        return this._CurrentUserObjectID.Value;
      IUserSession session = this.GetSession();
      try
      {
        return (this._CurrentUserObjectID = new long?(session.UserID)).Value;
      }
      finally
      {
        this.ReleaseSession();
      }
    }
  }

  /// <summary>Метод применим только для классов Project и StandaloneTask, полученных при помощи StandaloneTask.Get()</summary>
  public void UpdateParentPercentCompleted(double prevValue)
  {
    switch (this)
    {
      case Intermech.Project.Project _:
      case StandaloneTask _:
        IUserSession session = this.GetSession();
        try
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition);
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) -2
          }, (object[]) null, (SortOrders[]) null);
          long num1 = this.ObjectID;
          do
          {
            relationCollection.ChildObjectTypes = (IList<int>) new int[2]
            {
              (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task,
              (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project
            };
            DataTable dataTable = relationCollection.EntersInVersion(paramSet, num1);
            if (dataTable.Rows.Count > 0)
            {
              num1 = Convert.ToInt64(dataTable.Rows[0][0]);
              IDBObject iDbAttributable = session.GetObject(num1, false);
              if (iDbAttributable != null)
              {
                double num2 = 0.0;
                double num3 = 0.0;
                if (iDbAttributable.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanWork).Value is MeasuredValue mValue)
                {
                  MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, MeasureUnit.Hours.ID);
                  if (measuredValue != null)
                    num3 = measuredValue.Value;
                }
                IDBAttribute attributeById = iDbAttributable.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted);
                if (attributeById != null)
                  num2 = attributeById.AsDouble;
                if (num2 < 0.0)
                  num2 = 0.0;
                double num4 = num2 - this.Work / num3 * prevValue;
                if (num4 < 0.0)
                  num4 = 0.0;
                double a = num4 + this.Work / num3 * this.PercentCompleted;
                if (attributeById != null)
                  attributeById.AsDouble = a;
                else
                  iDbAttributable.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted, false, new object[1]
                  {
                    (object) a
                  });
                if (iDbAttributable.ObjectType != (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project)
                {
                  TaskStatus taskStatus = Helper.LCStepToTaskStatus(iDbAttributable.LCStep);
                  if (a > 0.0)
                  {
                    if (taskStatus == TaskStatus.Sent)
                      this.SetLcStep(iDbAttributable, (int) (IpsMetadataEntityBase<int>) LCStep.Executing);
                    if (Math.Round(a) == 100.0)
                    {
                      this.SetRuntimeFlag(iDbAttributable, RuntimeFlags.Summary);
                      this.SetLcStep(iDbAttributable, (int) (IpsMetadataEntityBase<int>) LCStep.Completed);
                      this.DeleteNotifications(session, this.MyProjectID, num1);
                    }
                  }
                }
              }
            }
            else
              num1 = 0L;
          }
          while (num1 != 0L);
          break;
        }
        finally
        {
          this.ReleaseSession();
        }
      default:
        throw new MethodAccessException();
    }
  }

  [System.Runtime.Serialization.OnSerializing]
  private void OnSerializing(StreamingContext context)
  {
    this.Attachments.NoOp<PrjAttachmentList>();
    this.Notes.NoOp<string>();
  }

  [CanBeNull]
  public object GetAttributeValue(int attrID)
  {
    AttrValue attrValue;
    if (this._attributesCache.TryGetValue(attrID, out attrValue))
      return attrValue.Value;
    if (this.ObjectID != 0L)
    {
      IDBObject dbObject = this.GetObject();
      try
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(attrID);
        if (attributeById != null)
        {
          this._attributesCache[attrID] = new AttrValue(attributeById.Value);
          return attributeById.Value;
        }
      }
      finally
      {
        this.ReleaseObject();
      }
    }
    return (object) null;
  }

  public void SetAttributeValue([NotEmpty] int attrID, [CanBeNull] object value)
  {
    object attributeValue = this.GetAttributeValue(attrID);
    if (value is DBNull)
      value = (object) null;
    if ((attributeValue == null || attributeValue.Equals(value)) && (attributeValue != null || value == null))
      return;
    AttrValue attrValue;
    if (this._attributesCache.TryGetValue(attrID, out attrValue))
    {
      attrValue.Value = value;
    }
    else
    {
      attrValue = new AttrValue(value);
      this._attributesCache.Add(attrID, attrValue);
    }
    attrValue.Modified = true;
    this.SetModified(true);
  }

  internal void WriteCachedAttributes([Intermech.Diagnostics.NotNull] IDBObject obj, bool clearModified = true, bool writeNotModified = false)
  {
    foreach (KeyValuePair<int, AttrValue> keyValuePair in this._attributesCache)
    {
      int key;
      AttrValue attrValue1;
      keyValuePair.Deconstruct<int, AttrValue>(out key, out attrValue1);
      int attributeID = key;
      AttrValue attrValue2 = attrValue1;
      if (writeNotModified || attrValue2.Modified)
      {
        IDBAttribute attributeById = obj.GetAttributeByID(attributeID);
        if (attributeById != null)
        {
          attributeById.Value = attrValue2.Value;
          if (clearModified)
            attrValue2.Modified = false;
        }
      }
    }
  }

  [Intermech.Diagnostics.NotNull]
  internal string GetPropString([Intermech.Diagnostics.NotNull] PropInfo pi)
  {
    string name = pi.Name;
    if (!Task._propSubstitutes.TryGetValue(name, out name))
      name = pi.Name;
    PropertyInfo property = this.GetType().GetProperty(name);
    if (property != (PropertyInfo) null)
    {
      object obj1 = property.GetValue((object) this, (object[]) null);
      if (obj1 != null)
      {
        if (obj1.GetType().IsEnum)
          return SimpleFuncs.GetEnumDescription((Enum) obj1, false);
        if (obj1 is DateTime dt)
        {
          if (DateTime.MinValue.Equals(obj1))
            return string.Empty;
          if (this.Project != null)
            return this.Project.FormatDateTime(dt);
          return !(this is Intermech.Project.Project project) ? string.Empty : project.FormatDateTime(dt);
        }
        object obj2;
        return (obj2 = obj1) is bool ? ((bool) obj2 ? Resources.ValTrue : Resources.ValFalse) ?? string.Empty : obj1.ToString();
      }
    }
    return string.Empty;
  }

  internal bool AllowDragDrop => !this.ReadOnly && !this.HasSubTasks;

  /// <summary>Идентификаторы узлов портала</summary>
  /// <exception cref="T:System.Data.ReadOnlyException">Thrown when a Read Only error condition occurs</exception>
  [CanBeNull]
  public virtual string SiteID
  {
    get
    {
      if (this._Site == null)
      {
        if (this.ObjectID == 0L)
          return (string) null;
        IDBObject dbObject = this.GetObject(false);
        try
        {
          this._Site = dbObject?.SiteID ?? string.Empty;
        }
        finally
        {
          this.ReleaseObject();
        }
      }
      return this._Site;
    }
    set => throw new ReadOnlyException();
  }

  private DateTime ApplyWorkingTime(DateTime dt, bool isStart)
  {
    if (dt == DateTime.MinValue)
      return dt;
    return !isStart ? this.LastWorkingTime(dt) : this.NextWorkingTime(dt);
  }

  protected bool AdjustByConstraint(ref DateTime date, bool isStart)
  {
    return this.AdjustByConstraint(ref date, isStart, this.ConstraintDate);
  }

  protected bool AdjustByConstraint(ref DateTime date, bool isStart, DateTime constraintDate)
  {
    bool flag = false;
    DateTime dateTime = date;
    if (!this.HasLoadedSubTasks)
    {
      switch (this.ConstraintType)
      {
        case ConstraintType.StartNoEarlierThan:
          if (!isStart)
            dateTime = this.GetStart(dateTime);
          if (dateTime < constraintDate)
          {
            dateTime = constraintDate;
            if (!isStart)
              dateTime = this.GetFinish(dateTime);
            flag = true;
            break;
          }
          break;
        case ConstraintType.StartNoLaterThan:
          if (!isStart)
            dateTime = this.GetStart(dateTime);
          if (constraintDate != DateTime.MinValue && dateTime > constraintDate)
          {
            dateTime = constraintDate;
            if (!isStart)
              dateTime = this.GetFinish(dateTime);
            flag = true;
            break;
          }
          break;
        case ConstraintType.FinishNoEarlierThan:
          if (isStart)
            dateTime = this.GetFinish(dateTime);
          if (dateTime < constraintDate)
          {
            dateTime = constraintDate;
            if (isStart)
              dateTime = this.GetStart(dateTime);
            flag = true;
            break;
          }
          break;
        case ConstraintType.FinishNoLaterThan:
          if (isStart)
            dateTime = this.GetFinish(dateTime);
          if (constraintDate != DateTime.MinValue && dateTime > constraintDate)
          {
            dateTime = constraintDate;
            if (isStart)
              dateTime = this.GetStart(dateTime);
            flag = true;
            break;
          }
          break;
      }
    }
    if (flag)
      date = dateTime;
    return flag;
  }

  /// <summary>Есть ли конфликт планирования между зависимостями и датой ограничения</summary>
  /// <exception cref="T:System.ArgumentException">Thrown when one or more arguments have unsupported or illegal values</exception>
  public virtual bool PlanningConflict
  {
    get => this._PlanningConflict;
    protected set => throw new ArgumentException();
  }

  protected DateTime AdjustByDependencies(DateTime dt, bool isStart)
  {
    return this.AdjustByDependencies(dt, isStart, false);
  }

  private void ConvertToStartFinish(ref DateTime dt, ref bool isStart, bool isStartNeeded)
  {
    if (isStartNeeded)
    {
      if (!isStart)
        dt = this.GetStart(dt);
    }
    else if (isStart)
      dt = this.GetFinish(dt);
    isStart = isStartNeeded;
  }

  private void AddDepLag([Intermech.Diagnostics.NotNull] Dependency d, ref DateTime depDateTime, bool invert = false)
  {
    double lagHours = d.LagHours;
    if (invert)
      lagHours *= -1.0;
    if (!d.HasLag)
      return;
    depDateTime = this.AddWorkTime(depDateTime, lagHours);
  }

  protected DateTime AdjustByDependencies(DateTime dt, bool isStart, bool externalOnly)
  {
    if (dt == DateTime.MinValue)
      return dt;
    bool isStartNeeded = isStart;
    DateTime dateTime = dt;
    bool flag = this.AdjustByConstraint(ref dt, isStart);
    Task task1 = this;
    if (this.ProjectLeftToRight)
    {
      do
      {
        foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.Dependencies)
        {
          if (dependency._State != DependencyState.Processed && (!externalOnly || dependency.External))
          {
            switch (dependency.DependencyType)
            {
              case DependencyType.FinishFinish:
                this.ConvertToStartFinish(ref dt, ref isStart, false);
                if (dependency.DependentOfTask != null)
                {
                  DateTime finish = dependency.DependentOfTask.Finish;
                  this.AddDepLag(dependency, ref finish);
                  if (dt < finish)
                  {
                    dt = this.ApplyWorkingTime(finish, false);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              case DependencyType.StartFinish:
                this.ConvertToStartFinish(ref dt, ref isStart, false);
                if (dependency.DependentOfTask != null)
                {
                  DateTime start = dependency.DependentOfTask.Start;
                  this.AddDepLag(dependency, ref start);
                  if (dt < start)
                  {
                    dt = this.ApplyWorkingTime(start, false);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
        task1 = task1.Parent;
      }
      while (task1 != null);
    }
    else
    {
      do
      {
        foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) task1.RelatedDependencies)
        {
          if (relatedDependency._State != DependencyState.Processed && relatedDependency.Resolved && (!externalOnly || relatedDependency.Task is ExternalTask))
          {
            switch (relatedDependency.DependencyType)
            {
              case DependencyType.FinishFinish:
                this.ConvertToStartFinish(ref dt, ref isStart, false);
                if (relatedDependency.Task != null)
                {
                  DateTime finish = relatedDependency.Task.Finish;
                  this.AddDepLag(relatedDependency, ref finish, true);
                  if (dt > finish)
                  {
                    dt = this.ApplyWorkingTime(finish, false);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              case DependencyType.FinishStart:
                this.ConvertToStartFinish(ref dt, ref isStart, false);
                if (relatedDependency.Task != null)
                {
                  DateTime start = relatedDependency.Task.Start;
                  this.AddDepLag(relatedDependency, ref start, true);
                  if (dt > start)
                  {
                    dt = this.ApplyWorkingTime(start, false);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
        task1 = task1.Parent;
      }
      while (task1 != null);
      Task task2 = this;
      do
      {
        foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task2.Dependencies)
        {
          if (dependency._State != DependencyState.Processed && dependency.Resolved && (!externalOnly || dependency.Task is ExternalTask) && dependency.DependencyType == DependencyType.StartFinish)
          {
            this.ConvertToStartFinish(ref dt, ref isStart, false);
            if (dependency.DependentOfTask != null)
            {
              DateTime start = dependency.DependentOfTask.Start;
              this.AddDepLag(dependency, ref start);
              if (dt < start)
              {
                dt = this.ApplyWorkingTime(start, false);
                flag = true;
              }
            }
          }
        }
        task2 = task2.Parent;
      }
      while (task2 != null);
    }
    if (this.ProjectLeftToRight)
    {
      Task task3 = this;
      do
      {
        foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task3.Dependencies)
        {
          if (dependency.Resolved && (!externalOnly || dependency.External))
          {
            switch (dependency.DependencyType)
            {
              case DependencyType.FinishStart:
                this.ConvertToStartFinish(ref dt, ref isStart, true);
                if (dependency.DependentOfTask != null)
                {
                  DateTime finish = dependency.DependentOfTask.Finish;
                  this.AddDepLag(dependency, ref finish);
                  if (dt <= finish)
                  {
                    dt = this.ApplyWorkingTime(finish, true);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              case DependencyType.StartStart:
                this.ConvertToStartFinish(ref dt, ref isStart, true);
                if (dependency.DependentOfTask != null)
                {
                  DateTime start = dependency.DependentOfTask.Start;
                  this.AddDepLag(dependency, ref start);
                  if (dt < start)
                  {
                    dt = this.ApplyWorkingTime(start, true);
                    flag = true;
                    continue;
                  }
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
        task3 = task3.Parent;
      }
      while (task3 != null);
    }
    else
    {
      Task task4 = this;
      do
      {
        foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) task4.RelatedDependencies)
        {
          if ((!externalOnly || relatedDependency.Task is ExternalTask) && relatedDependency.DependencyType == DependencyType.StartStart)
          {
            this.ConvertToStartFinish(ref dt, ref isStart, true);
            if (relatedDependency.Task != null)
            {
              DateTime start = relatedDependency.Task.Start;
              this.AddDepLag(relatedDependency, ref start, true);
              if (dt > start)
              {
                dt = start;
                flag = true;
              }
            }
          }
        }
        task4 = task4.Parent;
      }
      while (task4 != null);
    }
    if (flag)
      this.ConvertToStartFinish(ref dt, ref isStart, isStartNeeded);
    else
      dt = dateTime;
    return dt;
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к сессии с помощью внешней функции с установкой/снятия текущего статуса задаче</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeSession(TaskState state, [Intermech.Diagnostics.NotNull, InstantHandle] Session.SessionHandler sessionHandler)
  {
    IUserSession session = this.GetSession();
    this.SetState(state);
    try
    {
      sessionHandler(session);
    }
    finally
    {
      this.UnsetState(state);
      this.ReleaseSession();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к сессии с помощью внешней функции с установкой/снятия текущего статуса задаче</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeSession<T>(TaskState state, [Intermech.Diagnostics.NotNull, InstantHandle] Session.SessionHandler<T> sessionHandler)
  {
    IUserSession session = this.GetSession();
    this.SetState(state);
    try
    {
      return sessionHandler(session);
    }
    finally
    {
      this.UnsetState(state);
      this.ReleaseSession();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeDbObject([Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler handler, bool throwNotFoundException = true)
  {
    IDBObject dbObject = this.GetObject(throwNotFoundException);
    try
    {
      if (dbObject == null)
        return;
      handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObject<T>([Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler<T> handler, bool throwNotFoundException = true)
  {
    IDBObject dbObject = this.GetObject(throwNotFoundException);
    try
    {
      if (dbObject != null)
        return handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
    return default (T);
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeDbObject(
    [Intermech.Diagnostics.NotNull] IUserSession session,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler handler,
    bool throwNotFoundException = true)
  {
    IDBObject dbObject = this.GetObject(session, throwNotFoundException);
    try
    {
      if (dbObject == null)
        return;
      handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObject<T>(
    [Intermech.Diagnostics.NotNull] IUserSession session,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler<T> handler,
    bool throwNotFoundException = true)
  {
    IDBObject dbObject = this.GetObject(session, throwNotFoundException);
    try
    {
      if (dbObject != null)
        return handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
    return default (T);
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObjectNotNull<T>([Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandlerNotNull<T> handler) where T : class
  {
    IDBObject dbObject = this.GetObject(true);
    try
    {
      return handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObjectNotNull<T>(
    [Intermech.Diagnostics.NotNull] IUserSession session,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandlerNotNull<T> handler,
    bool throwNotFoundException = true)
    where T : class
  {
    IDBObject dbObject = this.GetObject(session);
    try
    {
      return handler(dbObject);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeDbObject(
    TaskState state,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler handler,
    bool throwNotFoundException = true)
  {
    IUserSession session = this.GetSession();
    this.SetState(state);
    try
    {
      this.InvokeDbObject(session, handler, throwNotFoundException);
    }
    finally
    {
      this.UnsetState(state);
      this.ReleaseSession();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [ContractAnnotation("throwNotFoundException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObject<T>(
    TaskState state,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler<T> handler,
    bool throwNotFoundException = true)
  {
    IUserSession session = this.GetSession();
    this.SetState(state);
    try
    {
      return this.InvokeDbObject<T>(session, handler, throwNotFoundException);
    }
    finally
    {
      this.UnsetState(state);
      this.ReleaseSession();
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeDbObject(
    TaskState state,
    [Intermech.Diagnostics.NotNull] IUserSession session,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler handler,
    bool throwNotFoundException = true)
  {
    this.SetState(state);
    try
    {
      this.InvokeDbObject(session, handler, throwNotFoundException);
    }
    finally
    {
      this.UnsetState(state);
    }
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к объекту БД задачи с помощью внешней функции</summary>
  [ContractAnnotation("throwNotFoundException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T InvokeDbObject<T>(
    TaskState state,
    [Intermech.Diagnostics.NotNull] IUserSession session,
    [Intermech.Diagnostics.NotNull, InstantHandle] Task.DbObjectHandler<T> handler,
    bool throwNotFoundException = true)
  {
    this.SetState(state);
    try
    {
      return this.InvokeDbObject<T>(session, handler, throwNotFoundException);
    }
    finally
    {
      this.UnsetState(state);
    }
  }

  public long OwnerID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._ownerID != 0L ? this._ownerID : this.CurrentUserObjectID;
    }
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  public long ChiefID
  {
    [DebuggerStepThrough] get
    {
      this.ChiefIsInherited = false;
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
      {
        if (assignment.IsChief && assignment.Resource != null)
          return assignment.Resource.ObjectID;
      }
      this.ChiefIsInherited = true;
      return this.InheritedChiefID;
    }
    set
    {
      Assignment assignment = this.Assignments.FirstOrDefault<Assignment>((System.Func<Assignment, bool>) (t => t.IsChief));
      if (value == 0L)
      {
        if (assignment == null)
          return;
        this.Assignments.Remove(assignment);
      }
      else
      {
        Resource resource = new Resource((ISessionProvider) this, value, this.GetUserName(value, true), (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User);
        if (assignment == null)
        {
          this.Assignments.Add(new Assignment(resource)
          {
            Units = 0.0,
            IsChief = true
          });
        }
        else
        {
          assignment.Resource = resource;
          assignment.Units = 0.0;
          assignment.IsChief = true;
        }
        foreach (Task allSubTask in (IEnumerable<Task>) this.AllSubTasks)
          allSubTask.PropertiesChanged(Task.CalcProps.Assignment);
      }
    }
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  public string ChiefString
  {
    [DebuggerStepThrough] get
    {
      if (this.UseCache && this._Cache?.ChiefString != null)
        return this._Cache.ChiefString;
      StringBuilder stringBuilder1 = new StringBuilder();
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
      {
        if (assignment.IsChief)
        {
          StringBuilder stringBuilder2 = stringBuilder1;
          string str1 = stringBuilder1.Length > 0 ? IMProject.ListSeparatorSymbol + " " : string.Empty;
          string name = assignment.Resource?.Name;
          if (name == null)
            throw new NullReferenceException("Resource?.Name");
          string str2;
          if (assignment.MaxUnits == 0.0 || assignment.Units == 1.0 && assignment.MaxUnits == 1.0)
            str2 = string.Empty;
          else
            str2 = $" {IMProject.UnitPreSymbol}{assignment.Units * 100.0:0.##}{IMProject.PercentSymbol}{(assignment.MaxUnits > assignment.Units ? (object) $"{IMProject.UnitSeparatorSymbol}{assignment.MaxUnits * 100.0:0.##}{IMProject.PercentSymbol}" : (object) string.Empty)}{IMProject.UnitPostSymbol}";
          stringBuilder2.AppendFormat("{0}{1}{2}", (object) str1, (object) name, (object) str2);
        }
      }
      string userName = stringBuilder1.ToString();
      if (userName == string.Empty)
        userName = this.GetUserName(this.InheritedChiefID);
      string chiefString = userName ?? string.Empty;
      this.Cache.ChiefString = chiefString;
      return chiefString;
    }
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  public virtual long InheritedChiefID
  {
    [DebuggerStepThrough] get
    {
      if (!this.Partial)
      {
        Task parent = this.Parent;
        if (parent != null)
          return parent.ChiefID;
      }
      else
      {
        IUserSession session = this.GetSession();
        try
        {
          IDBRelationCollection relationCollection1 = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition);
          object[] columns = new object[2]
          {
            (object) -2,
            (object) -7
          };
          DBRecordSetParams paramSet1 = new DBRecordSetParams((ConditionStructure[]) null, columns, (object[]) null, (SortOrders[]) null);
          IDBRelationCollection relationCollection2 = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.Resources);
          DBRecordSetParams paramSet2 = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure((int) (IpsMetadataEntityBase<int>) Attributes.ResourceIsChief, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, false)
          }, columns, (object[]) null, (SortOrders[]) null);
          long num1 = this.ObjectID;
          long num2 = 0;
          long inheritedChiefId = 0;
          while (num2 != (long) (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project)
          {
            relationCollection1.ChildObjectTypes = (IList<int>) new int[2]
            {
              (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task,
              (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project
            };
            DataTable dataTable1 = relationCollection1.EntersInVersion(paramSet1, num1);
            if (dataTable1.Rows.Count > 0)
            {
              num1 = Convert.ToInt64(dataTable1.Rows[0][0]);
              num2 = (long) Convert.ToInt32(dataTable1.Rows[0][1]);
              DataTable dataTable2 = relationCollection2.ConsistFrom(paramSet2, num1);
              if (dataTable2.Rows.Count > 0)
              {
                inheritedChiefId = Convert.ToInt64(dataTable2.Rows[0][0]);
                break;
              }
            }
            else
              num1 = 0L;
            if (num1 == 0L)
              break;
          }
          if (inheritedChiefId != 0L)
            return inheritedChiefId;
        }
        finally
        {
          this.ReleaseSession();
        }
      }
      Task myProject = (Task) this.MyProject;
      return myProject != null && myProject != this ? myProject.ChiefID : this.OwnerID;
    }
  }

  public bool ChiefIsInherited { get; private set; }

  [CanBeNull]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  [DebuggerDisplay("Hidden in debugger")]
  public string ChiefName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetUserName(this.ChiefID);
    }
  }

  protected void SetLcStep([Intermech.Diagnostics.NotNull] IDBObject obj, int value)
  {
    if (value != (int) (IpsMetadataEntityBase<int>) LCStep.Designing && obj.LCStep == (int) (IpsMetadataEntityBase<int>) LCStep.Imported)
      obj.LCStep = (int) (IpsMetadataEntityBase<int>) LCStep.Designing;
    obj.LCStep = value;
    this.DoNotification(Task.EventKind.Changed, obj.ObjectID);
  }

  internal int LcStep
  {
    get
    {
      this.GetObject(true);
      try
      {
        return this._Object.LCStep;
      }
      finally
      {
        this.ReleaseObject();
      }
    }
    set
    {
      this.GetObject(true);
      try
      {
        if (this._Object.LCStep != value)
          this.SetLcStep(this._Object, value);
        int status1 = (int) this._Status;
        this._Status = Helper.LCStepToTaskStatus(this._Object.LCStep);
        int status2 = (int) this._Status;
        if (status1 == status2)
          return;
        foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
          dependency.CopyLcStepFromTask();
      }
      finally
      {
        this.ReleaseObject();
      }
    }
  }

  public virtual bool CheckIn(bool throwNotFoundException)
  {
    this.GetObject(throwNotFoundException);
    try
    {
      if (this._Object == null || this._Object.CheckoutBy == 0L)
        return false;
      this._Object.CheckIn();
      this._ObjectID *= -1L;
      this._Object = (IDBObject) null;
      this.DoNotification(Task.EventKind.CheckIn, -this._ObjectID);
      return true;
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  public bool CheckIn() => this.CheckIn(true);

  public void CancelChanges(bool throwNotFoundException)
  {
    this.GetObject(throwNotFoundException);
    try
    {
      if (this._Object == null || this._Object.CheckoutBy == 0L)
        return;
      this._Object.CancelChanges();
      this.DoNotification(Task.EventKind.CancelChanges, this._ObjectID);
      this._ObjectID *= -1L;
      this._Object = (IDBObject) null;
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  public void CancelChanges() => this.CancelChanges(true);

  public bool HasWorkResources => this.Assignments.WorkResourceCount != 0;

  [Intermech.Diagnostics.NotNull]
  public virtual string Validate(bool executing = false)
  {
    string errorString = this.ErrorString;
    Intermech.Project.Project project = this.Project;
    if ((project != null ? (!project._Properties.AllowStartTasksWithNoResources ? 1 : 0) : 0) != 0 && !this.HasSubTasks && !this.Milestone && !this.HasWorkResources)
    {
      if (errorString != string.Empty)
        errorString += "\r\n";
      errorString += Localization.GetString("ErrNoTaskResources", (object) this.Name);
    }
    if (this.HasSubTasks)
    {
      TaskCollection taskCollection = new TaskCollection();
      taskCollection.Assign((IEnumerable<Task>) this.SubTasks);
      foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) taskCollection)
      {
        if (!task.IsProjectSummaryTask)
        {
          string str = task.Validate(executing);
          if (str != string.Empty)
          {
            if (errorString != string.Empty)
              errorString += "\r\n";
            errorString += str;
          }
        }
      }
    }
    return errorString;
  }

  public virtual void Execute()
  {
    this.GetObject(true);
    try
    {
      switch (Helper.LCStepToTaskStatus(this._Object.LCStep))
      {
        case TaskStatus.Executed:
          break;
        case TaskStatus.Completed:
          break;
        default:
          this.CheckIn();
          this.GetObject(true);
          this.SetState(TaskState.Starting);
          try
          {
            if (this.Status == TaskStatus.NotStarted || this.Status == TaskStatus.Terminated || this.Status == TaskStatus.Waiting)
            {
              if (this.Milestone)
                this._percentCompleted = 100.0;
              this.Status = this._RemoteExec || !this.HasSubTasks && !this.Milestone && !this.HasWorkResources ? TaskStatus.Waiting : TaskStatus.Sent;
              if (!this._RemoteExec && this._Object.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted) == null)
                this._Object.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted, false, new object[1]
                {
                  (object) 0
                });
            }
            if (this.PercentCompleted != 100.0)
              break;
            this.SetRuntimeFlag(this._Object, RuntimeFlags.AutoComplete);
            this.Status = TaskStatus.Executed;
            this.ProjectNeeded();
            Intermech.Project.Project project = this.Project;
            if ((project != null ? (project.VerifyTaskCompleted(this) ? 1 : 0) : 0) == 0)
              break;
            this.Status = TaskStatus.Completed;
            break;
          }
          finally
          {
            this.UnsetState(TaskState.Starting);
            this.ReleaseObject();
          }
      }
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  public virtual void Abort()
  {
    if (!this.IsExecuted && this.Status != TaskStatus.Waiting)
      return;
    this.Status = TaskStatus.Terminated;
  }

  [Intermech.Diagnostics.NotNull]
  public PrjAttachmentList Attachments
  {
    get
    {
      if (this._attachments == null)
      {
        this._attachments = new PrjAttachmentList();
        this._attachments.OnModified += new EventHandler(this.Attachments_OnModified);
        if (this.ObjectID != 0L)
        {
          this.GetObject();
          try
          {
            this._attachments.Load(this._Object);
          }
          finally
          {
            this.ReleaseObject();
          }
        }
      }
      return this._attachments;
    }
  }

  private void Attachments_OnModified([CanBeNull] object sender, [Intermech.Diagnostics.NotNull] EventArgs e)
  {
    this._srcData = (AttachmentList) null;
    this._results = (AttachmentList) null;
  }

  [Intermech.Diagnostics.NotNull]
  private AttachmentList _getAttachments([Intermech.Diagnostics.NotNull] ref AttachmentList destination, PrjAttachKind kind)
  {
    return destination ?? (destination = (AttachmentList) this.Attachments.Filter(kind));
  }

  private bool _setAttachments([Intermech.Diagnostics.NotNull] AttachmentList list, PrjAttachKind kind)
  {
    bool flag = false;
    AttachmentList attachmentList = (AttachmentList) this.Attachments.Filter(kind);
    foreach (Attachment att in (List<Attachment>) list)
    {
      int index = attachmentList.IndexOfID(att.ObjectID);
      if (index == -1)
      {
        PrjAttachment prjAttachment = new PrjAttachment();
        prjAttachment.Assign(att);
        this.Attachments.Add((Attachment) prjAttachment);
        flag = true;
      }
      else
        attachmentList.RemoveAt(index);
    }
    foreach (Attachment attachment in (List<Attachment>) attachmentList)
    {
      this.Attachments.Remove(attachment);
      flag = true;
    }
    if (flag)
      this.Modified = true;
    return flag;
  }

  [Intermech.Diagnostics.NotNull]
  public AttachmentList SrcData
  {
    get => this._getAttachments(ref this._srcData, PrjAttachKind.SrcData);
    set
    {
      if (!this._setAttachments(value, PrjAttachKind.SrcData))
        return;
      this._srcData = (AttachmentList) null;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public AttachmentList Results
  {
    get => this._getAttachments(ref this._results, PrjAttachKind.Result);
    set
    {
      if (!this._setAttachments(value, PrjAttachKind.Result))
        return;
      this._results = (AttachmentList) null;
    }
  }

  public void CheckResults()
  {
    if (this.VerifySchemeID == 0L)
      return;
    if (this.Results.Count == 0)
      throw new NotificationException(Resources.MustHaveResultsErr);
    IUserSession session = this.GetSession();
    try
    {
      Lazy<IDBObject> verifyScheme = new Lazy<IDBObject>((Func<IDBObject>) (() =>
      {
        IDBObject dbObject;
        if (this.UseActualScheme)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(this.VerifySchemeID);
          dbObject = !objectInfo.Empty ? session.GetObjectBaseVersionByID(objectInfo.ID, false) ?? session.GetObject(this.VerifySchemeID) : (IDBObject) null;
        }
        else
          dbObject = session.GetObject(this.VerifySchemeID, false);
        if (dbObject == null)
          this.VerifySchemeID = 0L;
        return dbObject;
      }));
      Lazy<long> lazy = new Lazy<long>((Func<long>) (() =>
      {
        if (!this.UseActualScheme || this.VerifySchemeID == 0L)
          return this.VerifySchemeID;
        IDBObject dbObject = verifyScheme.Value;
        return dbObject == null ? 0L : dbObject.ObjectID;
      }));
      List<long> enumerable1 = new List<long>();
      List<long> enumerable2 = new List<long>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Attachment result in (List<Attachment>) this.Results)
      {
        long objectId = result.ObjectID;
        ConditionStructure[] conds = new ConditionStructure[1]
        {
          new ConditionStructure(Intermech.Workflow.Attributes.ActivityStatus.ID, RelationalOperators.Equal, (object) 6, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
        };
        foreach (DataRow row in (InternalDataCollectionBase) AttachmentFuncs.GetAttachmentUsage(session, objectId, conds).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          enumerable2.Add(int64);
        }
        DBRecordSetParams paramSet;
        if (enumerable2.Count > 0)
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Workflow.ObjectTypes.Activity.ID);
          ColumnDescriptor[] columns = new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) Intermech.Metadata.Attributes.Process.ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
          };
          paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-2, RelationalOperators.In, (object) enumerable2.AsArray<long>(), LogicalOperators.NONE, 0, false)
          }, columns);
          if (paramSet.Tags == null)
            paramSet.Tags = new HybridDictionary();
          paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectIDsSelector(enumerable2.ToArray());
          foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
          {
            try
            {
              long int64 = Convert.ToInt64(row[0]);
              if (!enumerable1.Contains(int64))
                enumerable1.Add(int64);
            }
            catch
            {
            }
          }
        }
        bool flag = false;
        if (enumerable1.Count > 0)
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Workflow.ObjectTypes.Process.ID);
          paramSet = new DBRecordSetParams(new ConditionStructure[3]
          {
            new ConditionStructure(-2, RelationalOperators.In, (object) enumerable1.AsArray<long>(), LogicalOperators.AND, 0, false),
            new ConditionStructure(Intermech.Workflow.Attributes.PrototypeProcess.ID, RelationalOperators.Equal, (object) lazy.Value, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
            new ConditionStructure(Intermech.Workflow.Attributes.ActivityStatus.ID, RelationalOperators.Equal, (object) 6, LogicalOperators.AND, 0, false)
          }, new object[1]{ (object) -2 }, 0L, (object) null, 1);
          flag = objectCollection.Select(paramSet).Rows.Count != 0;
        }
        if (!flag)
        {
          string text = "???";
          IDBObject dbObject = session.GetObject(objectId, false);
          if (dbObject != null)
            text = dbObject.NameInMessages;
          stringBuilder.AppendWithDelimiter(text, ",\r\n");
        }
      }
      if (stringBuilder.Length > 0)
      {
        string str = verifyScheme.Value?.NameInMessages ?? string.Empty;
        stringBuilder.Insert(0, string.Format(Resources.MustBeVerifiedByProcess + "\r\n", (object) str, (object) "\r\n\r\n"));
        throw new NotificationException(stringBuilder.ToString());
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  public bool PropagateResults
  {
    [DebuggerStepThrough] get => this._propagateResults;
    set
    {
      if (value == this._propagateResults || !this.CanSetProperty(nameof (PropagateResults), (object) value))
        return;
      this.OnPropertyChanging(nameof (PropagateResults));
      this._propagateResults = value;
      this.OnPropertyChanged(nameof (PropagateResults));
      this.OnPropertyChangeCompleted(nameof (PropagateResults));
    }
  }

  public bool UseActualScheme
  {
    get => this._useActualScheme;
    set
    {
      if (value == this._useActualScheme || !this.CanSetProperty(nameof (UseActualScheme), (object) value))
        return;
      this.OnPropertyChanging(nameof (UseActualScheme));
      this._useActualScheme = value;
      this.OnPropertyChanged(nameof (UseActualScheme));
      this.OnPropertyChangeCompleted(nameof (UseActualScheme));
    }
  }

  public virtual TaskStatus Status
  {
    get => this._Status;
    set
    {
      if (this._Status == value)
        return;
      int lcStep = Helper.TaskStatusToLCStep(value);
      if (lcStep == 0)
        throw new Exception("Could not set task status: " + (object) value);
      this.GetObject(true);
      try
      {
        TaskStatus status = this._Status;
        if (this.HasSubTasks)
          this.SetRuntimeFlag(this._Object, RuntimeFlags.Summary);
        this.LcStep = lcStep;
        this.ProjectNeeded();
        if (this.Project == null)
          return;
        this.Project.OnTaskStatusChanged(this, this._Status, status);
      }
      finally
      {
        this.ReleaseObject();
      }
    }
  }

  public bool IsExecuted
  {
    get
    {
      return this.Status == TaskStatus.Sent || this.Status == TaskStatus.Executed || this.Status == TaskStatus.Pending;
    }
  }

  public bool IsCompleted => this.Status == TaskStatus.Completed;

  [Intermech.Diagnostics.NotNull]
  public static string GetStatusString(int lcStep)
  {
    return Task.GetStatusString(Helper.LCStepToTaskStatus(lcStep));
  }

  [Intermech.Diagnostics.NotNull]
  public static string GetStatusString(TaskStatus status)
  {
    return SimpleFuncs.GetEnumDescription((Enum) status);
  }

  [Intermech.Diagnostics.NotNull]
  public string StatusString => Task.GetStatusString(this.Status);

  public long ProjectID
  {
    get
    {
      if (this.Project != null)
        return this.Project.ObjectID;
      this.GetObject(true);
      try
      {
        return this._Object.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Project).AsInteger;
      }
      finally
      {
        this.ReleaseObject();
      }
    }
  }

  internal long MyProjectID
  {
    get
    {
      Intermech.Project.Project myProject = this.MyProject;
      return myProject == null ? this.ProjectID : myProject.ObjectID;
    }
  }

  [Intermech.Diagnostics.NotNull]
  public virtual string ProjectName => this.Project?.Name ?? "?";

  public void ProjectNeeded()
  {
    if (this.Project != null)
      return;
    long projectId = this.ProjectID;
    if (projectId == 0L)
      return;
    this._Project = new Intermech.Project.Project();
    this._Project._SessionProvider = this._SessionProvider;
    this._Project._Partial = true;
    this._Project.Load(projectId, new bool?(false));
  }

  /// <summary>Событие, вызываемое перед коммитом создания объекта задачи в БД (первое сохранение)</summary>
  protected event Task.SaveDbObjectAttributesDelegate SaveDbObjectAttributes;

  public long ObjectID
  {
    [DebuggerStepThrough] get => this._ObjectID;
  }

  protected internal long HackObjectID
  {
    set
    {
      this._ObjectID = value;
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
        dependency.HackObjectID = 0L;
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this.Assignments)
        assignment.HackRelationID = 0L;
      if (value != 0L)
        return;
      this._Status = TaskStatus.NotStarted;
      this._percentCompleted = 0.0;
    }
  }

  public long ID
  {
    get
    {
      if (this.ObjectID == 0L)
        return 0;
      this.GetObject();
      try
      {
        return this._Object.ID;
      }
      finally
      {
        this.ReleaseObject();
      }
    }
  }

  public virtual int ObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task;
    }
  }

  [CanBeNull]
  protected virtual TaskCollection ProjectTasks
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Project?.Tasks;
  }

  [CanBeNull]
  public virtual Intermech.Project.Project RootProject
  {
    get
    {
      Intermech.Project.Project project = this.Project;
      while (project != null && !project.IsProjectSummaryTask && project.Project != null && project.Project != project && !project.Project.Partial)
        project = project.Project;
      return project;
    }
  }

  public Task(long objectID)
    : this()
  {
    this._ObjectID = objectID;
  }

  [CanBeNull]
  protected virtual DataRow[] GetDbTasks(
    [CanBeNull] Intermech.Project.Project project,
    [Intermech.Diagnostics.NotNull] IUserSession session,
    int recordCount = -1,
    [CanBeNull] ConditionStructure[] conds = null)
  {
    BulkData bulkData = (BulkData) null;
    if (this is Intermech.Project.Project project1 && project1._BulkData != null)
      bulkData = project1._BulkData;
    if (bulkData == null && this.Project?._BulkData != null)
      bulkData = this.Project._BulkData;
    if (bulkData == null && project?._BulkData != null)
      bulkData = project._BulkData;
    if (bulkData != null)
    {
      DataTable tasks = bulkData.Tasks;
      if (tasks == null)
        return (DataRow[]) null;
      string columnName1 = tasks.Columns[this.Attr2Col(-21)].ColumnName;
      string columnName2 = tasks.Columns[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.SortIndex)].ColumnName;
      return tasks.Select($"[{columnName1}] = {(object) this.ObjectID}", $"[{columnName2}]");
    }
    IDBRelationCollection relationCollection = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition);
    object[] columns = new object[4]
    {
      (object) -2,
      (object) -20,
      (object) -7,
      (object) Intermech.Metadata.Attributes.SortIndex.ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conds, columns, (object[]) null, (SortOrders[]) null);
    paramSet.RecordCount = recordCount;
    if (Helper.ProjectsIsLocalType)
    {
      relationCollection.ObjectTypeID = (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task;
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, this.ObjectID);
      relationCollection.ObjectTypeID = (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project;
      DataTable table = relationCollection.ConsistFrom(paramSet, this.ObjectID);
      dataTable.Merge(table, false);
      string str = dataTable.Columns[3].ToString();
      return dataTable.Select(string.Empty, str + " ASC");
    }
    paramSet.SortColumns = new object[1]
    {
      (object) Intermech.Metadata.Attributes.SortIndex.ID
    };
    paramSet.Orders = new SortOrders[1]{ SortOrders.ASC };
    relationCollection.ChildObjectTypes = (IList<int>) new int[2]
    {
      (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task,
      (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project
    };
    return relationCollection.ConsistFrom(paramSet, this.ObjectID).Select();
  }

  [ContractAnnotation("throwNotFoundException:false => CanBeNull; => NotNull")]
  [CanBeNull]
  public IDBObject GetObject([Intermech.Diagnostics.NotNull] IUserSession session, bool throwNotFoundException = true)
  {
    if (this.ObjectID == 0L)
    {
      if (throwNotFoundException)
        throw new Exception($"У \"{this.Name}\" отсутствует объект!");
      return (IDBObject) null;
    }
    try
    {
      this._Object = session.GetObjectActualCopy(this.ObjectID, throwNotFoundException);
    }
    catch (ObjectNotFoundException ex)
    {
      if (this.ObjectID < 0L)
      {
        this._Object = session.GetObject(-this.ObjectID, throwNotFoundException);
        if (this._Object != null)
          this._ObjectID = -this.ObjectID;
      }
    }
    return this._Object;
  }

  [ContractAnnotation("throwNotFoundException:false => CanBeNull; => NotNull")]
  [CanBeNull]
  public virtual IDBObject GetObject(bool throwNotFoundException)
  {
    IUserSession session = this.GetSession();
    ++this._objectCounter;
    if (this._Object != null)
      return this._Object;
    try
    {
      return this.GetObject(session, throwNotFoundException);
    }
    catch
    {
      this.ReleaseObject();
      throw;
    }
  }

  [CanBeNull]
  public string ManagerAnswer
  {
    get
    {
      IUserSession session = this.GetSession();
      try
      {
        return session.GetTask(this.ObjectID).ManagerAnswer;
      }
      finally
      {
        this.ReleaseSession();
      }
    }
  }

  [Intermech.Diagnostics.NotNull]
  public IDBObject GetObject() => this.GetObject(true);

  public virtual void ReleaseObject()
  {
    --this._objectCounter;
    if (this._objectCounter == 0)
      this._Object = (IDBObject) null;
    this.ReleaseSession();
  }

  protected virtual bool IsSubTasksExist([Intermech.Diagnostics.NotNull] IUserSession session)
  {
    DataRow[] dbTasks = this.GetDbTasks(this.RootProject, session, 1);
    return dbTasks != null && dbTasks.Length != 0;
  }

  public virtual void Load([CanBeNull] IDBObject obj, bool? editingMode)
  {
    this.Load(obj, (Intermech.Project.Project) null, editingMode);
  }

  protected virtual void Load([Intermech.Diagnostics.NotNull] Intermech.Project.Project project, bool? editingMode)
  {
    if (this.ObjectID == 0L)
      return;
    this.GetObject(true);
    try
    {
      this.Load(this._Object, project, editingMode);
    }
    finally
    {
      this.ReleaseObject();
    }
  }

  protected virtual void LoadMajorProperties([Intermech.Diagnostics.NotNull] IDBObject obj, [CanBeNull] DataRow row)
  {
    if (row != null)
    {
      this._Name = row.FieldAsString(this.Attr2Col(-50));
      this._Status = Helper.LCStepToTaskStatus(Convert.ToInt32(row[this.Attr2Col(-4)]));
    }
    else
    {
      this._Name = obj.Caption;
      this._Status = Helper.LCStepToTaskStatus(obj.LCStep);
    }
  }

  private bool HandleException([Intermech.Diagnostics.NotNull] Exception e)
  {
    bool flag = false;
    Intermech.Project.Project project = this.RootProject;
    if (project == this && (e is KernelExceptionID kernelExceptionId ? (kernelExceptionId.ErrorID == 63 /*0x3F*/ ? 1 : 0) : 0) != 0)
      project = (Intermech.Project.Project) null;
    if (project != null)
      flag = project.HandleError(this, e);
    return flag;
  }

  protected virtual void Load([CanBeNull] IDBObject obj, [CanBeNull] Intermech.Project.Project project, bool? editingMode)
  {
    if (obj == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    try
    {
      this.Loading();
      try
      {
        this._ObjectID = obj.ObjectID;
        this.LoadMajorProperties(obj, this._DataRow);
        if (obj.ObjectType == (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.IncompleteObject)
          return;
        if (project != null)
        {
          project.StartProgress(0, this.Name);
          flag2 = true;
        }
        this.Assignments.Load();
        if (!editingMode.HasValue)
          editingMode = new bool?(this.Status == TaskStatus.NotStarted && (obj.CheckoutBy == this.CurrentUserObjectID || this.IsChief));
        if (editingMode.Value && !(this is Intermech.Project.Project))
        {
          Intermech.Project.SiteID siteId = new Intermech.Project.SiteID(this.SiteID);
          if ((int) siteId.Owner != (int) siteId.CurrentSite)
            editingMode = new bool?(false);
        }
        this.EditingMode = editingMode.Value ? EditingMode.Edit : EditingMode.None;
        this.CheckOut(ref obj);
        if (this is Intermech.Project.Project)
          this.HasNotLoadedSubTasks = true;
        this.LoadObject(obj, this._DataRow);
      }
      catch (Exception ex)
      {
        int num;
        switch (ex)
        {
          case AbortException _:
label_18:
            throw;
          case KernelExceptionID kernelExceptionId:
            num = kernelExceptionId.ErrorID == 346 ? 1 : 0;
            break;
          default:
            num = 0;
            break;
        }
        if (num == 0)
        {
          flag1 = true;
          if (!this.HandleException(ex))
            throw;
        }
        else
          goto label_18;
      }
      try
      {
        IUserSession session = obj.Session;
        if (this._Partial)
          return;
        Intermech.Project.Project project1 = project;
        if (this is Intermech.Project.Project project2 && project1 != null && project2 != project1)
        {
          project1 = project2;
          project1.AutoLoadSubProjects = project.AutoLoadSubProjects;
          project1.AutoLoadSubTasks = project.AutoLoadSubProjects;
        }
        if (this != project)
          this.HasNotLoadedSubTasks = this.IsSubTasksExist(session);
        if (project1 != null && project1.AutoLoadSubTasks)
          this.LoadSubTasks(project);
        this.LoadDependencies(session);
      }
      catch (Exception ex)
      {
        flag1 = true;
        if (this.HandleException(ex))
          return;
        throw;
      }
    }
    finally
    {
      this.Loaded();
      if (!flag1)
        this.Modified = false;
      if (project != null)
      {
        try
        {
          project.OnTaskLoaded(this);
        }
        finally
        {
          if (flag2)
            project.StopProgress();
          project.IncProgress();
        }
      }
    }
  }

  protected virtual void Loading()
  {
    this._savedState.Push(this._State);
    this._State |= TaskState.Loading;
    this.RaisePropertyChangedEvents = false;
  }

  protected virtual void AfterLoaded()
  {
    this.ClearCache();
    if (!this.HasLoadedSubTasks)
      return;
    foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
      subTask.AfterLoaded();
  }

  protected virtual void Loaded()
  {
    TaskState taskState = this._savedState.Pop();
    if ((taskState & TaskState.Loading) == TaskState.Loading)
      return;
    this.AfterLoaded();
    this._State = taskState;
    this.RaisePropertyChangedEvents = true;
    this.ResetBindings();
  }

  [ContractAnnotation("val:null => halt")]
  private static bool NotNull([CanBeNull] object val) => val != null && !DBNull.Value.Equals(val);

  protected virtual void LoadObject([Intermech.Diagnostics.NotNull] IDBObject obj, [CanBeNull] DataRow row)
  {
    bool silentMode = this._SilentMode;
    this._SilentMode = true;
    try
    {
      ConstraintType constraintType = ConstraintType.AsSoonAsPossible;
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintType)];
        if (Task.NotNull(val))
          constraintType = (ConstraintType) Convert.ToInt32(val);
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintType);
        if (attributeById != null)
          constraintType = (ConstraintType) attributeById.AsInteger;
      }
      if (this is Intermech.Project.Project project)
      {
        project.PlanningType = constraintType == ConstraintType.AsLateAsPossible ? PlanningType.FromEnd : PlanningType.FromStart;
        project.ManualPlanning = constraintType == ConstraintType.ManualPlanning;
      }
      else
      {
        this._ConstraintType = constraintType;
        if (row != null)
        {
          object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate)];
          if (Task.NotNull(val) && Convert.ToDateTime(val) != DateTime.MinValue)
            this.ConstraintDate = Convert.ToDateTime(val);
        }
        else
        {
          IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate);
          if (attributeById != null && attributeById.AsDateTime != DateTime.MinValue)
            this.ConstraintDate = attributeById.AsDateTime;
        }
        if (this._ConstraintType == ConstraintType.ManualPlanning)
          this._ConstraintType = ConstraintType.AsSoonAsPossible;
      }
      this._PrevSavedFinish = DateTime.MinValue;
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.PlanFinish)];
        if (Task.NotNull(val))
          this._PrevSavedFinish = Convert.ToDateTime(val);
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanFinish);
        if (attributeById != null && !DBNull.Value.Equals(attributeById.Value))
          this._PrevSavedFinish = attributeById.AsDateTime;
      }
      bool flag = constraintType == ConstraintType.ManualPlanning;
      if (this.LeftToRight | flag)
      {
        if (row != null)
        {
          object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.PlanStart)];
          if (Task.NotNull(val))
            this.Start = Convert.ToDateTime(val);
        }
        else
        {
          IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanStart);
          if (attributeById != null && !DBNull.Value.Equals(attributeById.Value))
            this.Start = attributeById.AsDateTime;
        }
      }
      if (!this.LeftToRight | flag && this._PrevSavedFinish != DateTime.MinValue)
        this.Finish = this._PrevSavedFinish;
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.PlanDuration)];
        if (Task.NotNull(val))
          this.DurationString = val.ToString();
      }
      else
        this.DurationString = obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanDuration).AsString ?? string.Empty;
      if (obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanWork).Value is MeasuredValue mValue)
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, MeasureUnit.Hours.ID);
        if (measuredValue != null)
          this.Work = measuredValue.Value;
      }
      if (!this.HasLoadedSubTasks || this.Partial)
      {
        if (row != null)
        {
          object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted)];
          if (Task.NotNull(val))
            this.PercentCompletedString = val.ToString();
          else
            this.PercentCompleted = 0.0;
        }
        else
        {
          IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted);
          if (attributeById != null)
            this.PercentCompletedString = attributeById.AsString ?? string.Empty;
          else
            this.PercentCompleted = 0.0;
        }
      }
      TaskFlags taskFlags = (TaskFlags) 0;
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.Flags)];
        if (Task.NotNull(val))
          taskFlags = (TaskFlags) Convert.ToInt32(val);
      }
      else
        taskFlags = (TaskFlags) obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Flags).AsInteger;
      this.Estimation = (taskFlags & TaskFlags.Estimation) == TaskFlags.Estimation;
      this.Milestone = (taskFlags & TaskFlags.Milestone) == TaskFlags.Milestone;
      this.PropagateResults = (taskFlags & TaskFlags.PropagateResults) == TaskFlags.PropagateResults;
      this.UseActualScheme = (taskFlags & TaskFlags.UseActualScheme) == TaskFlags.UseActualScheme;
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.TaskPriority)];
        if (Task.NotNull(val))
          this.Priority = Convert.ToInt32(val);
      }
      else
        this.Priority = (int) obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.TaskPriority).AsInteger;
      this._ownerID = obj.OwnerID;
      if (row != null)
      {
        object val1 = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.FactStart)];
        if (Task.NotNull(val1))
          this.FactStart = Convert.ToDateTime(val1);
        object val2 = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.FactFinish)];
        if (Task.NotNull(val2))
          this.FactFinish = Convert.ToDateTime(val2);
      }
      else
      {
        IDBAttribute attributeById1 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.FactStart);
        if (attributeById1 != null)
          this.FactStart = attributeById1.AsDateTime;
        IDBAttribute attributeById2 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.FactFinish);
        if (attributeById2 != null)
          this.FactFinish = attributeById2.AsDateTime;
      }
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.VerifyScheme)];
        if (Task.NotNull(val))
          this.VerifySchemeID = Convert.ToInt64(val);
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.VerifyScheme);
        if (attributeById != null)
          this.VerifySchemeID = attributeById.AsInteger;
      }
      this.TaskColor = (row != null ? row.FieldAsStringDef(this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.TaskColor), (string) null) : obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.TaskColor)?.AsString).ConvertToColorOrNullFromHEX();
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRootObjectGuid)];
        if (Task.NotNull(val))
          this._ImportedRootObjectVersionGuid = new Guid(val.ToString());
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRootObjectGuid);
        if (attributeById != null)
          this._ImportedRootObjectVersionGuid = new Guid(attributeById.AsString);
      }
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject)];
        if (Task.NotNull(val))
          this._ImportedObjectVersion = Convert.ToInt64(val);
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject);
        if (attributeById != null)
          this._ImportedObjectVersion = attributeById.AsInteger;
      }
      if (row != null)
      {
        object val = row[this.Attr2Col((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRelationGuid)];
        if (!Task.NotNull(val))
          return;
        this._ImportedRelationGuid = new Guid(val.ToString());
      }
      else
      {
        IDBAttribute attributeById = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRelationGuid);
        if (attributeById == null)
          return;
        this._ImportedRelationGuid = new Guid(attributeById.AsString);
      }
    }
    finally
    {
      this._SilentMode = silentMode;
    }
  }

  [Intermech.Diagnostics.NotNull]
  private static string DepToString(DependencyType dt) => ((int) dt).ToString();

  internal int OutlineLevel
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      int indentLevel = this.IndentLevel;
      int? outlineLevel = this.Project?.OutlineLevel;
      return (outlineLevel.HasValue ? new int?(indentLevel - outlineLevel.GetValueOrDefault()) : new int?()) ?? 1;
    }
  }

  internal long UID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long) (this.LocalIndex + 1);
  }

  internal virtual void SaveToXml([Intermech.Diagnostics.NotNull] XmlTextWriter writer)
  {
    this.SaveToXml(writer, false);
  }

  protected void SaveToXml([Intermech.Diagnostics.NotNull] XmlTextWriter writer, bool subProjectTask)
  {
    writer.WriteStartElement(nameof (Task));
    writer.WriteElementString("UID", this.UID.ToString());
    if (!(this is Intermech.Project.Project) || this.Project != null)
      writer.WriteElementString("Name", this.Name);
    writer.WriteElementString("Type", this is Intermech.Project.Project ? "1" : "2");
    writer.WriteElementString("Start", MsProjectFuncs.DateTimeToStr(this.Start));
    writer.WriteElementString("Finish", MsProjectFuncs.DateTimeToStr(this.Finish));
    writer.WriteElementString("Duration", MsProjectFuncs.HoursToString(this.Work));
    writer.WriteElementString("Work", MsProjectFuncs.HoursToString(this.Work));
    writer.WriteElementString("PercentComplete", this.PercentCompleted.ToString("0.##", (IFormatProvider) CultureInfo.InvariantCulture));
    XmlTextWriter xmlTextWriter1 = writer;
    int num;
    string str1;
    if (!(this is Intermech.Project.Project) || subProjectTask)
    {
      int indentLevel1 = this.IndentLevel;
      Intermech.Project.Project project = this.Project;
      int indentLevel2 = project != null ? project.IndentLevel : 0;
      num = indentLevel1 - indentLevel2;
      str1 = num.ToString();
    }
    else
      str1 = "0";
    xmlTextWriter1.WriteElementString("OutlineLevel", str1);
    writer.WriteElementString("OutlineNumber", this.UID.ToString());
    writer.WriteElementString("Summary", this.HasSubTasks ? "1" : "0");
    writer.WriteElementString("IsPublished", this is Intermech.Project.Project ? "0" : "1");
    writer.WriteElementString("Estimated", this.Estimation ? "1" : "0");
    writer.WriteElementString("Milestone", this.Milestone ? "1" : "0");
    if (!string.IsNullOrWhiteSpace(this.Notes))
      writer.WriteElementString("Notes", this.Notes);
    XmlTextWriter xmlTextWriter2 = writer;
    num = Convert.ToInt32((object) this.ConstraintType);
    string str2 = num.ToString();
    xmlTextWriter2.WriteElementString("ConstraintType", str2);
    if (this.ConstraintDate != DateTime.MinValue)
      writer.WriteElementString("ConstraintDate", MsProjectFuncs.DateTimeToStr(this.ConstraintDate));
    writer.WriteElementString("IsSubproject", subProjectTask ? "1" : "0");
    if (this.Dependencies.Count > 0)
    {
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this.Dependencies)
      {
        Task dependentOfTask = dependency.DependentOfTask;
        writer.WriteStartElement("PredecessorLink");
        writer.WriteElementString("PredecessorUID", dependentOfTask.UID.ToString());
        writer.WriteElementString("Type", Task.DepToString(dependency.DependencyType));
        if (dependentOfTask.Project != this.Project)
        {
          writer.WriteElementString("CrossProject", "1");
          writer.WriteElementString("CrossProjectName", MsProjectFuncs.ProjectNameToString(dependentOfTask.ProjectName));
        }
        else
          writer.WriteElementString("CrossProject", "0");
        if (dependency.Lag > 0.0)
        {
          string str3 = "7";
          string str4;
          if (dependency.LagUnit == WorkTimeUnits.Days)
          {
            num = Convert.ToInt32(dependency.Lag * 4800.0);
            str4 = num.ToString();
          }
          else
          {
            num = Convert.ToInt32(dependency.LagHours * 600.0);
            str4 = num.ToString();
            str3 = "5";
          }
          writer.WriteElementString("LinkLag", str4);
          writer.WriteElementString("LagFormat", str3);
        }
        writer.WriteEndElement();
      }
    }
    if (subProjectTask)
      return;
    writer.WriteEndElement();
  }

  internal virtual bool LoadFromXml(
    [CanBeNull] XmlNode root,
    [CanBeNull] Intermech.Project.Project project,
    [CanBeNull] Dictionary<Task, List<XmlPredecessor>> predecessors)
  {
    if (root?["Name"] == null)
      return false;
    try
    {
      this.Loading();
      if (project != null)
        this.Project = project;
      this.HasNotLoadedSubTasks = false;
      this.Name = root["Name"].InnerText;
      project?.StartProgress(0, this.Name);
      try
      {
        if (root["ConstraintType"] != null)
        {
          this._ConstraintType = (ConstraintType) Convert.ToInt32(root["ConstraintType"].InnerText);
          if (root["ConstraintDate"] != null)
            this.ConstraintDate = MsProjectFuncs.StrToDateTime(root["ConstraintDate"].InnerText);
        }
        if (this.LeftToRight)
          this.Start = MsProjectFuncs.StrToDateTime(root["Start"].InnerText);
        else
          this.Finish = MsProjectFuncs.StrToDateTime(root["Finish"].InnerText);
        if (root["Work"] != null)
        {
          double hours1 = MsProjectFuncs.StringToHours(root["Work"].InnerText);
          if (hours1 > 0.0)
          {
            this.Work = hours1;
          }
          else
          {
            double hours2 = MsProjectFuncs.StringToHours(root["Duration"].InnerText);
            if (hours2 > 0.0)
              this.Work = hours2;
          }
        }
        double result;
        if (root["PercentComplete"] != null && double.TryParse(root["PercentComplete"].InnerText, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          this.PercentCompleted = result;
        XmlNode xmlNode1 = (XmlNode) root["Notes"];
        if (xmlNode1 != null)
          this.Notes = xmlNode1.InnerText;
        this.Estimation = false;
        int indentLevel = project != null ? project.IndentLevel : 0;
        this.IndentLevel = Convert.ToInt32(root["OutlineLevel"].InnerText) + indentLevel;
        if (predecessors != null)
        {
          List<XmlPredecessor> xmlPredecessorList = (List<XmlPredecessor>) null;
          foreach (XmlNode xmlNode2 in (root as XmlElement).GetElementsByTagName("PredecessorLink"))
          {
            if (xmlNode2["Type"] != null)
            {
              DependencyType int32 = (DependencyType) Convert.ToInt32(xmlNode2["Type"].InnerText);
              string projectName = string.Empty;
              if (xmlNode2["CrossProject"] != null && xmlNode2["CrossProject"].InnerText == "1" && xmlNode2["CrossProjectName"] != null)
                projectName = MsProjectFuncs.StringToProjectName(xmlNode2["CrossProjectName"].InnerText);
              if (xmlPredecessorList == null)
                xmlPredecessorList = new List<XmlPredecessor>();
              XmlPredecessor xmlPredecessor = new XmlPredecessor(xmlNode2["PredecessorUID"].InnerText, int32, projectName);
              xmlPredecessorList.Add(xmlPredecessor);
              if (xmlNode2["LinkLag"] != null)
              {
                string innerText1 = xmlNode2["LagFormat"].InnerText;
                string innerText2 = xmlNode2["LinkLag"].InnerText;
                switch (innerText1)
                {
                  case "5":
                    xmlPredecessor.Lag = (double) Convert.ToInt32(innerText2) / 600.0;
                    xmlPredecessor.LagUnit = WorkTimeUnits.Hours;
                    continue;
                  case "7":
                    xmlPredecessor.Lag = (double) Convert.ToInt32(innerText2) / 4800.0;
                    xmlPredecessor.LagUnit = WorkTimeUnits.Days;
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
          if (xmlPredecessorList != null)
            predecessors.Add(this, xmlPredecessorList);
        }
      }
      finally
      {
        project?.StopProgress();
      }
    }
    finally
    {
      this.Loaded();
      this.Modified = true;
    }
    return true;
  }

  public virtual void LoadDependencies([Intermech.Diagnostics.NotNull] IUserSession session)
  {
    BulkData bulkData = (BulkData) null;
    DataRow[] dataRowArray = (DataRow[]) null;
    if (this is Intermech.Project.Project project && project._BulkData != null)
      bulkData = project._BulkData;
    if (bulkData == null && this.Project?._BulkData != null)
      bulkData = this.Project._BulkData;
    if (bulkData != null)
    {
      DataTable dependences = bulkData.Dependences;
      if (dependences != null)
      {
        string columnName = dependences.Columns[2].ColumnName;
        dataRowArray = dependences.Select($"[{columnName}] = {(object) Math.Abs(this.ObjectID)}");
      }
    }
    else
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency);
      ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.Less, (object) 0, LogicalOperators.AND, 0, false);
      if (this.ObjectID > 0L)
        conditionStructure.RelationalOperator = RelationalOperators.Greater;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure((int) (IpsMetadataEntityBase<int>) Attributes.ToTask, RelationalOperators.Equal, (object) this.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        conditionStructure
      }, new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) -2),
        new ColumnDescriptor((object) Attributes.FromTask.ID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) Attributes.ToTask.ID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) Attributes.DependencyType.ID),
        new ColumnDescriptor((object) Attributes.Lag.ID)
      });
      dataRowArray = objectCollection.Select(paramSet).Select();
    }
    bool flag1 = false;
    if (dataRowArray != null)
    {
      foreach (DataRow row in dataRowArray)
      {
        Dependency dependency = new Dependency();
        try
        {
          dependency.Load(this, row);
        }
        catch (Exception ex)
        {
          flag1 = true;
          bool flag2 = false;
          Intermech.Project.Project rootProject = this.RootProject;
          if (rootProject != null)
          {
            flag2 = rootProject.HandleError(this, ex);
            if (dependency.ObjectID != 0L)
              rootProject.DeletedDependencies.Add(dependency);
          }
          if (!flag2)
            throw;
        }
      }
    }
    if (flag1)
    {
      Intermech.Project.Project rootProject = this.RootProject;
      if (rootProject == null)
        return;
      rootProject._ModifiedWhileLoading = true;
    }
    else
      this.Dependencies._Modified = false;
  }

  [CanBeNull]
  private Task FindSubTaskByObjectID(long id)
  {
    return this.SubTasks.FirstOrDefault<Task>((System.Func<Task, bool>) (t => t.ObjectID == id));
  }

  protected virtual void AfterSave([Intermech.Diagnostics.NotNull] IUserSession session, [Intermech.Diagnostics.NotNull] IDBObject obj)
  {
    if (this.IsExecuted)
    {
      if (this._PrevSavedFinish != this.Finish)
      {
        this.RegisterUncompletedTimer();
        this._PrevSavedFinish = this.Finish;
      }
      if (this._prevAssignmentsModified)
      {
        if (this.Assignments.Delta.Deleted.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) this.ListNotifications(session).Rows)
          {
            if (this.Assignments.Delta.Deleted.Contains((long) Convert.ToInt32(row[2])))
              session.GetObject(Convert.ToInt64(row[0])).Delete(0L);
          }
        }
        if (this.Assignments.Delta.Added.Count > 0)
          this.Project?.SendTaskNotification(this, TaskStatus.Sent, this.Assignments.Delta.Added.ToArray());
      }
    }
    Intermech.Project.Project project = this.Project;
    if ((project != null ? (project.IsExecuted ? 1 : 0) : 0) == 0)
      return;
    bool flag = false;
    if (this.Status == TaskStatus.NotStarted)
      flag = true;
    else if (this.Status == TaskStatus.Waiting)
    {
      if (this.Completed)
        flag = true;
      else if (this.Assignments.Count > 0 && this.Assignments.Delta.Deleted.Count == 0 && this.Assignments.Delta.Added.Count == this.Assignments.Count)
        flag = true;
    }
    else if (this.Status == TaskStatus.Sent && this.HasSubTasks && this.Completed)
      flag = true;
    if (!flag)
      return;
    this.Execute();
  }

  public virtual bool Save([Intermech.Diagnostics.NotNull] IUserSession session)
  {
    Intermech.Project.Project myProject = this.MyProject;
    try
    {
      if (!this.EditingMode.Any() && !this.HasState(TaskState.Copying))
        return false;
      if (this.ObjectID == 0L)
        this.Modified = true;
      bool flag = this.Modified || this.HasState(TaskState.Copying) || this.Uncommitted;
      if (flag || this.HasState(TaskState.ChildrenModified))
      {
        try
        {
          myProject?.StartProgress(0, this.Name);
          this._State |= TaskState.Saving;
          IDBObject objToLock = (IDBObject) null;
          if (this.ObjectID != 0L)
          {
            objToLock = session.GetObject(this.ObjectID, false);
            if (objToLock == null && this.ObjectID < 0L)
            {
              this._ObjectID = -this._ObjectID;
              objToLock = session.GetObject(this.ObjectID, false);
            }
          }
          if (objToLock != null)
            this.CheckOut(ref objToLock);
          else
            objToLock = session.GetObjectCollection(this.ObjectTypeID).Create();
          using (RemoteLock remoteLock = new RemoteLock())
          {
            remoteLock.Add((object) objToLock);
            if (flag && this.Status == TaskStatus.Completed)
              flag = false;
            if (flag)
              this.SaveObject(session, objToLock);
            this._Saved = true;
            if (this.EditingMode.HasFlag((Enum) EditingMode.Composition))
            {
              this.SaveChildren(session, objToLock);
              this._State ^= TaskState.ChildrenModified;
            }
            if (flag)
              this.AfterSave(session, objToLock);
          }
        }
        finally
        {
          this._State ^= TaskState.Saving;
          myProject?.StopProgress();
        }
      }
      return true;
    }
    finally
    {
      myProject?.IncProgress();
    }
  }

  public bool Save()
  {
    IUserSession session = this.GetSession();
    try
    {
      return this.Save(session);
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  protected virtual void SaveObject([Intermech.Diagnostics.NotNull] IUserSession session, [Intermech.Diagnostics.NotNull] IDBObject obj)
  {
    if (this.Name == "gen@error")
      throw new Exception(this.Name);
    if (this.EditingMode.HasProperties())
    {
      obj.Caption = this.Name;
      Intermech.Project.Project project = this.Project;
      long objectId = project != null ? project.ObjectID : 0L;
      if (objectId != 0L)
        obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Project).AsInteger = objectId;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanStart).AsDateTime = this.Start;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanFinish).AsDateTime = this.Finish;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanDuration).Value = this._durationUnit != null ? (object) new MeasuredValue(this._durationUnit.Convert(this.Duration, this.CurrentSchedule), this._durationUnit.MeasureID) : (object) new MeasuredValue(this.Duration, MeasureUnit.Days.ID);
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PlanWork).Value = (object) new MeasuredValue(this.Work, MeasureUnit.Hours.ID);
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Description).AsString = this.Notes;
      IDBAttribute attributeById1 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted);
      if (this.PercentCompleted != 0.0)
      {
        if (attributeById1 == null)
          obj.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted, false, new object[1]
          {
            (object) this.PercentCompleted
          });
        else
          attributeById1.AsDouble = this.PercentCompleted;
      }
      else
        attributeById1?.Delete(0L);
      TaskFlags taskFlags = (TaskFlags) 0;
      if (this.Estimation)
        taskFlags |= TaskFlags.Estimation;
      if (this.Milestone)
        taskFlags |= TaskFlags.Milestone;
      if (this.PropagateResults)
        taskFlags |= TaskFlags.PropagateResults;
      if (this.UseActualScheme)
        taskFlags |= TaskFlags.UseActualScheme;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Flags).AsInteger = (long) taskFlags;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.TaskPriority).AsInteger = (long) this.Priority;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintType).AsInteger = (long) this.ConstraintType;
      if (!(this is Intermech.Project.Project))
      {
        if (this.ConstraintDate != DateTime.MinValue)
          obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate).AsDateTime = this.ConstraintDate;
        else
          obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate).Clear();
      }
      IDBAttribute attributeById2 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.VerifyScheme);
      if (this.VerifySchemeID != 0L)
      {
        if (attributeById2 == null)
          obj.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.VerifyScheme, false, new object[1]
          {
            (object) this.VerifySchemeID
          });
        else
          attributeById2.AsInteger = this.VerifySchemeID;
      }
      else
        attributeById2?.Delete(0L);
      if (this.TaskColor.HasValue && this.TaskColor.Value != Color.Empty)
        obj.SetAttrStrValue((int) (IpsMetadataEntityBase<int>) Attributes.TaskColor, this.TaskColor.Value.ToHexString());
      else
        obj.DeleteAttribute((int) (IpsMetadataEntityBase<int>) Attributes.TaskColor);
      this.WriteCachedAttributes(obj);
      if (this.SaveDbObjectAttributes != null)
        this.SaveDbObjectAttributes(obj);
      if (obj.IsCreationMode)
      {
        obj.CommitCreation(true);
        this._ObjectID = obj.ObjectID;
        this._justCreated = true;
        this._Status = Helper.LCStepToTaskStatus(obj.LCStep);
        this.CheckOut(ref obj);
        this.DoNotification(Task.EventKind.Created, this._ObjectID);
      }
      if (this.Dependencies._Modified || this.HasState(TaskState.Copying))
        this.Dependencies.Save(session, true);
    }
    this._prevAssignmentsModified = false;
    this._prevAttachmentsModified = false;
    if (!this.EditingMode.HasComposition())
      return;
    if (this.Assignments._Modified || this.HasState(TaskState.Copying))
    {
      this.Assignments.Save(session);
      this._prevAssignmentsModified = true;
    }
    if (this._attachments == null || !this.Attachments.Modified && !this.HasState(TaskState.Copying))
      return;
    this.Attachments.Save(obj);
    this._prevAttachmentsModified = true;
  }

  protected virtual void SaveChildren([Intermech.Diagnostics.NotNull] IUserSession session, [Intermech.Diagnostics.NotNull] IDBObject obj)
  {
    if (this.HasNotLoadedSubTasks)
      return;
    Dictionary<long, long> dictionary1 = new Dictionary<long, long>();
    Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
    DataRow[] dbTasks = this.GetDbTasks((Intermech.Project.Project) null, session);
    if (dbTasks != null)
    {
      foreach (DataRow dataRow in dbTasks)
      {
        long int64 = Convert.ToInt64(dataRow[0]);
        if (this.FindSubTaskByObjectID(int64) == null)
        {
          if (!dictionary1.ContainsKey(int64))
            dictionary1.Add(int64, Convert.ToInt64(dataRow[1]));
        }
        else if (!dictionary2.ContainsKey(int64))
          dictionary2.Add(int64, Convert.ToInt64(dataRow[1]));
      }
    }
    IDBRelationCollection objToLock = (IDBRelationCollection) null;
    using (RemoteLock remoteLock = new RemoteLock())
    {
      foreach (Task subTask in (IEnumerable<Task>) this.SubTasks)
      {
        if (subTask.Project == null || !subTask.Project.EditingLocked && subTask.Project.EditingMode.HasComposition())
        {
          bool flag1 = subTask.IndexModified || subTask._RewriteTaskSortIndex;
          long objectId1 = subTask.ObjectID;
          subTask.Save(session);
          IDBRelation dbRelation = (IDBRelation) null;
          long aRelationID = 0;
          if (objectId1 != subTask.ObjectID)
            dictionary2.TryGetValue(objectId1, out aRelationID);
          if (aRelationID == 0L && !dictionary2.TryGetValue(subTask.ObjectID, out aRelationID))
          {
            if (objToLock == null)
            {
              objToLock = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition);
              remoteLock.Add((object) objToLock);
            }
            try
            {
              try
              {
                dbRelation = objToLock.Create(this.ObjectID, subTask.ObjectID);
              }
              catch (Exception ex)
              {
                long? nullable = ex is ObjectNotFoundException notFoundException ? new long?(notFoundException.ObjectID) : new long?();
                long objectId2 = subTask._ObjectID;
                if (nullable.GetValueOrDefault() == objectId2 & nullable.HasValue)
                {
                  subTask._ObjectID = -subTask._ObjectID;
                  dbRelation = objToLock.Create(this.ObjectID, subTask.ObjectID);
                }
                else
                  throw;
              }
            }
            catch (Exception ex)
            {
              bool flag2 = false;
              if (ex.InnerException is KernelExceptionID innerException && innerException.ErrorID == 47)
                flag2 = true;
              if (!flag2)
                throw;
            }
          }
          else if (flag1)
            dbRelation = session.GetRelation(aRelationID, false);
          if (dbRelation != null)
          {
            IDBAttribute attributeById = dbRelation.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.SortIndex);
            if (attributeById != null)
              attributeById.AsInteger = (long) subTask.SortIndex;
            if (subTask._RewriteTaskSortIndex)
              subTask._RewriteTaskSortIndex = false;
          }
        }
      }
    }
    if (dictionary1.Count <= 0)
      return;
    foreach (long aRelationID in dictionary1.Values)
      session.GetRelation(aRelationID).Delete(0L);
  }

  public bool IndexModified
  {
    [DebuggerStepThrough] get => this._IndexModified;
  }

  internal override void OnPropertyChanged(string property, bool triggerModified)
  {
    this._Cache?.ResetValue(property);
    if (property == "Index")
      this._IndexModified = true;
    if (property == "Index" || property == "IndentLevel")
    {
      Task.TaskCache cache = this._Cache;
      if ((cache != null ? (cache.MaxPossibleIndentLevel.HasValue ? 1 : 0) : 0) != 0)
        this._Cache.MaxPossibleIndentLevel = new int?();
    }
    base.OnPropertyChanged(property, triggerModified);
  }

  protected override void SetModified(bool value)
  {
    if (this.HasState(TaskState.Loading) || this.Uncommitted || value && !this.EditingMode.Any())
      return;
    if (!value)
      this._IndexModified = false;
    base.SetModified(value);
    this._Saved = !value;
    if (!(this.Project != null & value))
      return;
    this.Project.SetModified(true);
  }

  public TaskState State
  {
    [DebuggerStepThrough] get => this._State;
  }

  public void SetState(TaskState state)
  {
    this._State |= state;
    int num;
    if (this._stateCounter.TryGetValue(state, out num))
      this._stateCounter[state] = num + 1;
    else
      this._stateCounter.Add(state, 1);
  }

  public void UnsetState(TaskState state)
  {
    int num;
    if (this._stateCounter.TryGetValue(state, out num))
    {
      --num;
      this._stateCounter[state] = num;
    }
    if (num != 0)
      return;
    this._State &= ~state;
  }

  public bool HasState(TaskState state) => (this._State & state) == state;

  public void SendOverdueNotification()
  {
    this.ProjectNeeded();
    this.SendNotification(string.Format(Resources.OverdueTaskMailSubject, (object) this.Name), string.Format(Resources.OverdueTaskMailTemplate, (object) this.ObjectID, (object) this.NameInMessages, (object) this.Assignments.UserNamesString, (object) this.ProjectID, (object) this.Project.NameInMessages), new long[1]
    {
      this.Project.ChiefID
    });
  }

  public void SendCanStartNotification()
  {
    this.ProjectNeeded();
    this.SendNotification(string.Format(Resources.CanStartTaskSubject, (object) this.Name), string.Format(Resources.CanStartTaskTemplate, (object) this.ObjectID, (object) this.NameInMessages, (object) this.ProjectID, (object) this.Project.NameInMessages, (object) this.Project.ChiefID, (object) this.GetUserName(this.Project.ChiefID)), this.Assignments.UserIDs.ToArray(), false);
  }

  internal void SendNotification([Intermech.Diagnostics.NotNull] string subject, [Intermech.Diagnostics.NotNull] string text, [Intermech.Diagnostics.NotNull] long[] userIDs, bool highPriority = true)
  {
    this.ProjectNeeded();
    if (!this.Project._Properties.EnableMailNotifications)
      return;
    IUserSession session = this.GetSession();
    try
    {
      IRouterService customService = session.GetCustomService<IRouterService>(false);
      if (customService == null)
        throw new Exception(Resources.ErrWorkflowServiceNotFound);
      foreach (long userId in userIDs)
      {
        if (!this.IsNotificationSent(subject, userId))
        {
          IDBObject message = customService.CreateMessage(session.SessionGUID, (int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage, userId, subject, text, this.CurrentUserObjectID);
          message.AttributeByID(Intermech.Metadata.Attributes.Process.ID).AsInteger = this.ProjectID;
          message.AttributeByID(Intermech.Workflow.Attributes.Activity.ID).AsInteger = this.ObjectID;
          if (highPriority)
            message.AttributeByID(Intermech.Workflow.Attributes.ProcessPriority.ID).AsInteger = message.AttributeByID(Intermech.Workflow.Attributes.ProcessPriority.ID).AsInteger = 1L;
        }
      }
      if (this.Project == null)
        return;
      this.Project.RefreshMail();
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  [Intermech.Diagnostics.NotNull]
  private DataTable ListNotifications()
  {
    IUserSession session = this.GetSession();
    try
    {
      return this.ListNotifications(session);
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  [Intermech.Diagnostics.NotNull]
  private DataTable ListNotifications([Intermech.Diagnostics.NotNull] IUserSession session)
  {
    long taskID = 0;
    if (!(this is Intermech.Project.Project))
      taskID = this.ObjectID;
    return Task.ListNotifications(session, this.MyProjectID, taskID);
  }

  [Intermech.Diagnostics.NotNull]
  private static DataTable ListNotifications([Intermech.Diagnostics.NotNull] IUserSession session, long projectID, long taskID)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage);
    ConditionStructure[] array = new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Metadata.Attributes.Process.ID, RelationalOperators.Equal, (object) projectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
    if (taskID != 0L)
    {
      Array.Resize<ConditionStructure>(ref array, 2);
      array[1] = new ConditionStructure(Intermech.Workflow.Attributes.Activity.ID, RelationalOperators.Equal, (object) taskID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
    }
    return objectCollection.Select(new DBRecordSetParams(array, new object[3]
    {
      (object) -2,
      (object) Intermech.Workflow.Attributes.ProcessSubject.ID,
      (object) Intermech.Workflow.Attributes.Recipient.ID
    }, 0L, (object) null, -1)
    {
      Contents = new ColumnContents[3]
      {
        ColumnContents.ID,
        ColumnContents.String,
        ColumnContents.ID
      }
    });
  }

  internal bool IsNotificationSent([Intermech.Diagnostics.NotNull] string subject, long userID)
  {
    return this.ListNotifications().Any((System.Func<DataRow, bool>) (row => userID.Equals(row.FieldAsLong(2)) && subject.Equals(row.FieldAsString(1))));
  }

  protected internal void DeleteNotifications()
  {
    IUserSession session = this.GetSession();
    try
    {
      foreach (IDBObject dbObject in (IEnumerable<IDBObject>) this.ListNotifications().Select<IDBObject>((System.Func<DataRow, IDBObject>) (row => session.GetObject(row.FieldAsLong(0)))))
        dbObject.Delete(0L);
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  protected internal void DeleteNotifications([Intermech.Diagnostics.NotNull] IUserSession session, long projectID, long taskID)
  {
    foreach (IDBObject dbObject in (IEnumerable<IDBObject>) Task.ListNotifications(session, projectID, taskID).Select<IDBObject>((System.Func<DataRow, IDBObject>) (row => session.GetObject(row.FieldAsLong(0)))))
      dbObject.Delete(0L);
  }

  /// <summary>Guid версии импортированного в проект "корневого" объекта</summary>
  public Guid ImportedRootObjectVersionGuid
  {
    [DebuggerStepThrough] get => this._ImportedRootObjectVersionGuid;
  }

  /// <summary>Идентификатор версии импортированного объекта</summary>
  public long ImportedObjectVersionID
  {
    [DebuggerStepThrough] get => this._ImportedObjectVersion;
  }

  /// <summary>GUID импортированной связи</summary>
  public Guid ImportedRelationGuid
  {
    [DebuggerStepThrough] get => this._ImportedRelationGuid;
  }

  /// <summary>Связать задачу с импортированным объектом</summary>
  public void LinkWithImportedObject(
    [NotEmpty] Guid importedRootObjectVersionGuid,
    [NotEmpty] long importedObjectVersionID,
    [NotEmpty] Guid importedRelationGuid)
  {
    this._ImportedRootObjectVersionGuid = importedRootObjectVersionGuid;
    this._ImportedObjectVersion = Math.Abs(importedObjectVersionID);
    this._ImportedRelationGuid = importedRelationGuid;
    this.SaveDbObjectAttributes += new Task.SaveDbObjectAttributesDelegate(this.SaveImportInfo);
  }

  /// <summary>Создание связи между создаваемым в БД объектом задачи и объектом типа "Настройки импорта"</summary>
  private void SaveImportInfo([Intermech.Diagnostics.NotNull] IDBObject taskDbObject)
  {
    this.SaveDbObjectAttributes -= new Task.SaveDbObjectAttributesDelegate(this.SaveImportInfo);
    taskDbObject.SetAttrGuidValue((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRootObjectGuid, this._ImportedRootObjectVersionGuid, autoDelAttrIfEmpty: true);
    this._ImportedObjectVersion = taskDbObject.UpdateLinkToObjectWithCheck((int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject, this._ImportedObjectVersion);
    taskDbObject.SetAttrGuidValue((int) (IpsMetadataEntityBase<int>) Attributes.ImportedRelationGuid, this._ImportedRelationGuid, autoDelAttrIfEmpty: true);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  public DateScheduleList GetWorkTime([Intermech.Diagnostics.NotNull] Schedule schedule, DateTime start, double work)
  {
    return schedule.GetWorkTime(start, work);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  public DateScheduleList GetWorkTime([Intermech.Diagnostics.NotNull] Schedule schedule, DateTime start, DateTime finish)
  {
    return schedule.GetWorkTime(start, finish);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  public DateScheduleList GetWorkTime(long resourceID, DateTime start, DateTime finish)
  {
    return this.GetWorkTime(this.GetSchedule(resourceID), start, finish);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  public DateScheduleList GetWorkTime(DateTime start, double work)
  {
    return this.GetWorkTime(this.CurrentSchedule, start, work);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  public DateScheduleList GetWorkTime(DateTime start, DateTime finish)
  {
    return this.GetWorkTime(this.CurrentSchedule, start, finish);
  }

  /// <summary>Вернуть количество рабочих часов в заданном периоде для текущих исполнителей</summary>
  /// <param name="start">Начало периода</param>
  /// <param name="finish">Конец периода</param>
  /// <param name="fullWork">true: функция вернет суммарное количество рабочих часов всех исполнителей, false: количество рабочих часов в
  /// этом периоде для одного исполнителя (для вычисления длительности)</param>
  /// <returns>The work hours</returns>
  public double GetWorkHours(DateTime start, DateTime finish, bool fullWork = false)
  {
    if (start == DateTime.MinValue || finish == DateTime.MinValue)
      return 0.0;
    int num = 1;
    if (start > finish)
    {
      DateTime dateTime = start;
      start = finish;
      finish = dateTime;
      num = -1;
    }
    return this.GetWorkTime(start, finish).Sum<DateSchedule>((System.Func<DateSchedule, double>) (schedule => !fullWork ? schedule.Duration : schedule.Work)) * (double) num;
  }

  private double GetWorkHours(DateTime finish) => this.GetWorkHours(this.Start, finish);

  protected DateTime AddWorkTime(DateTime dt, double hours)
  {
    if (hours == 0.0)
      return dt;
    DateTime dateTime = hours > 0.0 ? DateTime.MinValue : DateTime.MaxValue;
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) this.GetWorkTime(dt, hours))
    {
      if (hours > 0.0)
      {
        if (dateSchedule.FinishTime > dateTime)
          dateTime = dateSchedule.FinishTime;
      }
      else if (dateSchedule.StartTime < dateTime)
        dateTime = dateSchedule.StartTime;
    }
    return dateTime;
  }

  [Flags]
  protected internal enum CalcProps
  {
    Position = 1,
    Work = 2,
    Cost = 4,
    Schedule = 8,
    Indent = 16, // 0x00000010
    Estimation = 32, // 0x00000020
    Assignment = 64, // 0x00000040
    Other = 128, // 0x00000080
    FinishConstraint = 256, // 0x00000100
    Dependencies = 1024, // 0x00000400
    StartConstraint = 512, // 0x00000200
    Tasks = Dependencies, // 0x00000400
    PercentCompleted = 2048, // 0x00000800
    BackDependencies = 4096, // 0x00001000
    ClearGraph = 8192, // 0x00002000
    All = 1048575, // 0x000FFFFF
  }

  public enum EventKind
  {
    Created,
    CheckOut,
    CheckIn,
    CancelChanges,
    Changed,
    RefreshMail,
  }

  [Serializable]
  public class TaskCache : ISerializable
  {
    [CanBeNull]
    public string AssignmentsString;
    [CanBeNull]
    public double? Cost;
    [CanBeNull]
    public string CostString;
    [CanBeNull]
    public string DependenciesString;
    [CanBeNull]
    public double? Duration;
    [CanBeNull]
    public bool? Estimation;
    [CanBeNull]
    public bool? HasSubTasks;
    [CanBeNull]
    public int? MaxPossibleIndentLevel;
    public Maybe<Task> Parent;
    [CanBeNull]
    public double? PercentCompleted;
    [CanBeNull]
    public DateTime? Start;
    [CanBeNull]
    public DateTime? Finish;
    [CanBeNull]
    public double? Units;
    [CanBeNull]
    public double? Work;
    [CanBeNull]
    public DateScheduleList WorkTime;
    [CanBeNull]
    public string ChiefString;

    public TaskCache()
    {
    }

    protected TaskCache([Intermech.Diagnostics.NotNull] SerializationInfo info, StreamingContext context)
    {
      this.AssignmentsString = info.GetString(nameof (AssignmentsString));
      this.Cost = info.GetValue<double?>(nameof (Cost));
      this.CostString = info.GetString(nameof (CostString));
      this.DependenciesString = info.GetString(nameof (DependenciesString));
      this.Duration = info.GetValue<double?>(nameof (Duration));
      this.Estimation = info.GetValue<bool?>(nameof (Estimation));
      this.HasSubTasks = info.GetValue<bool?>(nameof (HasSubTasks));
      this.MaxPossibleIndentLevel = info.GetValue<int?>(nameof (MaxPossibleIndentLevel));
      this.PercentCompleted = info.GetValue<double?>(nameof (PercentCompleted));
      this.Start = info.GetValue<DateTime?>(nameof (Start));
      this.Finish = info.GetValue<DateTime?>(nameof (Finish));
      this.Units = info.GetValue<double?>(nameof (Units));
      this.Work = info.GetValue<double?>(nameof (Work));
      this.ChiefString = info.GetString(nameof (ChiefString));
    }

    public virtual void GetObjectData([Intermech.Diagnostics.NotNull] SerializationInfo info, StreamingContext context)
    {
      info.AddValue("AssignmentsString", (object) this.AssignmentsString);
      info.AddValue("Cost", (object) this.Cost);
      info.AddValue("CostString", (object) this.CostString);
      info.AddValue("DependenciesString", (object) this.DependenciesString);
      info.AddValue("Duration", (object) this.Duration);
      info.AddValue("Estimation", (object) this.Estimation);
      info.AddValue("HasSubTasks", (object) this.HasSubTasks);
      info.AddValue("MaxPossibleIndentLevel", (object) this.MaxPossibleIndentLevel);
      info.AddValue("PercentCompleted", (object) this.PercentCompleted);
      info.AddValue("Start", (object) this.Start);
      info.AddValue("Finish", (object) this.Finish);
      info.AddValue("Units", (object) this.Units);
      info.AddValue("Work", (object) this.Work);
      info.AddValue("ChiefString", (object) this.ChiefString);
    }

    public virtual void Clear()
    {
      this.AssignmentsString = (string) null;
      this.Cost = new double?();
      this.CostString = (string) null;
      this.DependenciesString = (string) null;
      this.Duration = new double?();
      this.Estimation = new bool?();
      this.HasSubTasks = new bool?();
      this.MaxPossibleIndentLevel = new int?();
      this.Parent = Maybe<Task>.Empty;
      this.PercentCompleted = new double?();
      this.Start = new DateTime?();
      this.Finish = new DateTime?();
      this.Units = new double?();
      this.Work = new double?();
      this.WorkTime = (DateScheduleList) null;
      this.ChiefString = (string) null;
    }

    public virtual bool ResetValue([Intermech.Diagnostics.NotNull, NotWhitespace] string valueName)
    {
      switch (valueName)
      {
        case "AssignmentsString":
          this.AssignmentsString = (string) null;
          return true;
        case "ChiefString":
          this.ChiefString = (string) null;
          return true;
        case "Cost":
          this.Cost = new double?();
          return true;
        case "CostString":
          this.CostString = (string) null;
          return true;
        case "DependenciesString":
          this.DependenciesString = (string) null;
          return true;
        case "Duration":
          this.Duration = new double?();
          return true;
        case "Estimation":
          this.Estimation = new bool?();
          return true;
        case "Finish":
          this.Finish = new DateTime?();
          return true;
        case "HasSubTasks":
          this.HasSubTasks = new bool?();
          return true;
        case "MaxPossibleIndentLevel":
          this.MaxPossibleIndentLevel = new int?();
          return true;
        case "Parent":
          this.Parent = Maybe<Task>.Empty;
          return true;
        case "PercentCompleted":
          this.PercentCompleted = new double?();
          return true;
        case "Start":
          this.Start = new DateTime?();
          return true;
        case "Units":
          this.Units = new double?();
          return true;
        case "Work":
          this.Work = new double?();
          return true;
        case "WorkTime":
          this.WorkTime = (DateScheduleList) null;
          return true;
        default:
          return false;
      }
    }
  }

  /// <summary>Делегат метода обработки объекта БД не возвращающий результата</summary>
  public delegate void DbObjectHandler([Intermech.Diagnostics.NotNull] IDBObject dbObject);

  /// <summary>Делегат метода обработки объекта БД возвращающий типизированный результат</summary>
  public delegate T DbObjectHandler<T>([Intermech.Diagnostics.NotNull] IDBObject dbObject);

  /// <summary>Делегат метода обработки объекта БД возвращающий типизированный результат</summary>
  [Intermech.Diagnostics.NotNull]
  public delegate T DbObjectHandlerNotNull<T>([Intermech.Diagnostics.NotNull] IDBObject dbObject) where T : class;

  internal enum GraphLinkDirection
  {
    Forward,
    Backward,
  }

  internal class GraphLink
  {
    [CanBeNull]
    public object Tag;

    public Task.GraphLinkDirection Direction { get; }

    [CanBeNull]
    public Task.GraphNode Node { get; internal set; }

    public GraphLink([Intermech.Diagnostics.NotNull] Task.GraphNode node, Task.GraphLinkDirection direction)
    {
      this.Node = node;
      this.Direction = direction;
    }

    public override string ToString()
    {
      return (this.Direction == Task.GraphLinkDirection.Forward ? " => " : " <= ") + (this.Node?.ToString() ?? "null");
    }
  }

  internal enum SummaryNodeKind
  {
    None,
    Start,
    End,
  }

  internal class GraphLinkList : List<Task.GraphLink>
  {
    private bool HasDirection(Task.GraphLinkDirection dir)
    {
      return this.Any<Task.GraphLink>((System.Func<Task.GraphLink, bool>) (link => link.Direction == dir));
    }

    public bool HasInbound => this.HasDirection(Task.GraphLinkDirection.Backward);

    public bool HasOutbound => this.HasDirection(Task.GraphLinkDirection.Forward);

    public bool Contains([CanBeNull] Task.GraphNode node)
    {
      return this.Any<Task.GraphLink>((System.Func<Task.GraphLink, bool>) (link => link.Node == node));
    }
  }

  internal class GraphNode
  {
    private Task.SummaryNodeKind _summaryNodeKind;
    private double _workSpan;

    [Intermech.Diagnostics.NotNull]
    [ItemNotNull]
    public Task.GraphLinkList Links { get; } = new Task.GraphLinkList();

    [CanBeNull]
    public Task Task { get; protected set; }

    [CanBeNull]
    public Task.StartNode StartNode { get; internal set; }

    [CanBeNull]
    public Task.EndNode EndNode { get; internal set; }

    [Intermech.Diagnostics.NotNull]
    public Dictionary<bool, double> Value { get; } = new Dictionary<bool, double>();

    [Intermech.Diagnostics.NotNull]
    public HashedList<Task.GraphNode> CriticalNodes { get; } = new HashedList<Task.GraphNode>();

    [CanBeNull]
    public Task.Graph Graph { get; }

    internal DateTime CurrentDt { get; set; }

    [Intermech.Diagnostics.NotNull]
    public Dictionary<bool, DateTime> Date { get; } = new Dictionary<bool, DateTime>();

    public Task.SummaryNodeKind SummaryNodeKind
    {
      [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return this._summaryNodeKind;
      }
      internal set
      {
        if (this._summaryNodeKind == value)
          return;
        this._summaryNodeKind = value;
      }
    }

    public int ChainID { get; internal set; }

    public GraphNode([CanBeNull] Task.Graph graph, [CanBeNull] Task task, Task.SummaryNodeKind kind = Task.SummaryNodeKind.None)
    {
      this.Value.Add(true, -1.0);
      this.Value.Add(false, -1.0);
      this.Task = task;
      this.Date.Add(true, DateTime.MinValue);
      this.Date.Add(false, DateTime.MinValue);
      this._summaryNodeKind = kind;
      if (graph == null)
        return;
      this.Graph = graph;
      graph.Add(this);
    }

    [Intermech.Diagnostics.NotNull]
    public string TaskName => this.Task?.Name ?? "?";

    public override string ToString()
    {
      string str = this.TaskName;
      if (this.Graph?.Left == this)
        str = "Left";
      if (this.Graph?.Right == this)
        str = "Right";
      if (this.SummaryNodeKind != Task.SummaryNodeKind.None)
        str = $"{str} {(object) this.SummaryNodeKind}";
      return $"{$"{(object) this.Index}.  {str}"} /{(object) this.ChainID}";
    }

    public virtual double GetWeight(bool forward, double prevValue)
    {
      this.Date[forward] = this.CurrentDt;
      if (this._workSpan == 0.0 && this.Task is Intermech.Project.Project && (this.Task.HasNotLoadedSubTasks || this.Task.ManualPlanning) && this.SummaryNodeKind != Task.SummaryNodeKind.End)
      {
        DateTime finish;
        if (forward)
        {
          Task.Graph graph = this.Graph;
          if ((graph != null ? (graph.LeftToRight ? 1 : 0) : 1) != 0)
          {
            finish = this.Task._Start;
            goto label_5;
          }
        }
        finish = this.Task._Finish;
label_5:
        this._workSpan = this.Task.GetWorkHours(this.CurrentDt, finish);
        if (this._workSpan < 0.0)
        {
          Task.Graph graph = this.Graph;
          if ((graph != null ? (graph.LeftToRight ? 1 : 0) : 1) != 0)
          {
            if (!forward)
              this._workSpan *= -1.0;
          }
          else if (forward)
            this._workSpan *= -1.0;
        }
      }
      return this.Work;
    }

    public virtual double Work
    {
      get
      {
        double num = 0.0;
        if (this.Task != null && this.SummaryNodeKind == Task.SummaryNodeKind.None)
          num = !(this.Task is Intermech.Project.Project) || !this.Task.ManualPlanning ? (!this.Task.HasNotLoadedSubTasks ? this.Task.Work : this.Task.RealWork) : this.Task.GetWorkHours(this.Task._Start, this.Task._Finish);
        return this._workSpan + num;
      }
    }

    [CanBeNull]
    public Task.GraphLink FindLink([Intermech.Diagnostics.NotNull] Task.GraphNode node, Task.GraphLinkDirection direction)
    {
      return this.Links.FirstOrDefault<Task.GraphLink>((System.Func<Task.GraphLink, bool>) (link => link.Node == node && link.Direction == direction));
    }

    public bool IsLinkedTo([Intermech.Diagnostics.NotNull] Task.GraphNode node, Task.GraphLinkDirection direction)
    {
      return this.FindLink(node, direction) != null;
    }

    internal virtual void ClearCache(bool goForward)
    {
      this.Value[goForward] = -1.0;
      this.Date[goForward] = DateTime.MinValue;
    }

    public int Index { get; internal set; }

    private void InternalInsert(
      [Intermech.Diagnostics.NotNull] Task.GraphNode node,
      Task.GraphLinkDirection linkDirection,
      bool replaceLinks)
    {
      Task.GraphLinkDirection direction = linkDirection == Task.GraphLinkDirection.Forward ? Task.GraphLinkDirection.Backward : Task.GraphLinkDirection.Forward;
      bool flag = false;
      for (int index = node.Links.Count - 1; index >= 0; --index)
      {
        if (node.Links[index].Node == this && node.Links[index].Direction == linkDirection)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index = this.Links.Count - 1; index >= 0; --index)
        {
          if (this.Links[index].Node == node && this.Links[index].Direction == direction)
            return;
        }
      }
      if (replaceLinks)
      {
        for (int index = node.Links.Count - 1; index >= 0; --index)
        {
          Task.GraphLink link = node.Links[index];
          if (link.Direction == linkDirection)
          {
            if (this.Links.Contains(link.Node))
              return;
            this.Links.Add(link);
            node.Links.RemoveAt(index);
          }
        }
        foreach (Task.GraphLink link1 in (List<Task.GraphLink>) this.Links)
        {
          Task.GraphLink link2 = link1.Node?.FindLink(node, direction);
          if (link2 != null)
            link2.Node = this;
        }
      }
      this.Links.Add(new Task.GraphLink(node, direction));
      node.Links.Add(new Task.GraphLink(this, linkDirection));
    }

    public void InsertBefore([Intermech.Diagnostics.NotNull] Task.GraphNode node, bool replaceLinks)
    {
      this.InternalInsert(node, Task.GraphLinkDirection.Backward, replaceLinks);
    }

    public void InsertAfter([Intermech.Diagnostics.NotNull] Task.GraphNode node, bool replaceLinks)
    {
      this.InternalInsert(node, Task.GraphLinkDirection.Forward, replaceLinks);
    }

    [Intermech.Diagnostics.NotNull]
    internal List<List<Task.GraphNode>> DirectiveNodes { get; } = new List<List<Task.GraphNode>>();

    public bool? IsTaskLeftToRight
    {
      get
      {
        this.DirectiveNodes.Clear();
        bool? isTaskLeftToRight1 = new bool?();
        if (this.Task != null)
        {
          if (this.Task.ConstraintType == ConstraintType.AsSoonAsPossible)
          {
            Task.Graph graph = this.Graph;
            if ((graph != null ? (graph.LeftToRight ? 1 : 0) : 1) != 0)
              return new bool?();
            isTaskLeftToRight1 = new bool?(true);
          }
          else if (this.Task.ConstraintType == ConstraintType.AsLateAsPossible)
          {
            Task.Graph graph = this.Graph;
            if ((graph != null ? (!graph.LeftToRight ? 1 : 0) : 1) != 0)
              return new bool?();
            isTaskLeftToRight1 = new bool?(false);
          }
          else
          {
            Task.GraphLinkDirection graphLinkDirection = Task.GraphLinkDirection.Forward;
            Task.Graph graph1 = this.Graph;
            if ((graph1 != null ? (!graph1.LeftToRight ? 1 : 0) : 1) != 0)
              graphLinkDirection = Task.GraphLinkDirection.Backward;
            bool? nullable = new bool?();
            Task.Graph graph2 = this.Graph;
            bool flag1 = graph2 == null || graph2.LeftToRight;
            foreach (Task.GraphLink link in (List<Task.GraphLink>) this.Links)
            {
              if (link.Direction == graphLinkDirection)
              {
                bool? isTaskLeftToRight2 = (bool?) link.Node?.IsTaskLeftToRight;
                if (isTaskLeftToRight2.HasValue && isTaskLeftToRight2.Value == flag1)
                {
                  nullable = isTaskLeftToRight2;
                  foreach (IEnumerable<Task.GraphNode> directiveNode in link.Node.DirectiveNodes)
                    this.DirectiveNodes.Add(new List<Task.GraphNode>(directiveNode));
                }
              }
            }
            bool flag2 = !flag1;
            if (this.DirectiveNodes.Count == 0)
            {
              foreach (Task.GraphLink link in (List<Task.GraphLink>) this.Links)
              {
                if (link.Direction == graphLinkDirection)
                {
                  bool? isTaskLeftToRight3 = (bool?) link.Node?.IsTaskLeftToRight;
                  if (isTaskLeftToRight3.HasValue && isTaskLeftToRight3.Value == flag2)
                  {
                    nullable = isTaskLeftToRight3;
                    foreach (IEnumerable<Task.GraphNode> directiveNode in link.Node.DirectiveNodes)
                      this.DirectiveNodes.Add(new List<Task.GraphNode>(directiveNode));
                  }
                }
              }
            }
            foreach (List<Task.GraphNode> directiveNode in this.DirectiveNodes)
              directiveNode.Insert(0, this);
            isTaskLeftToRight1 = nullable;
          }
        }
        if (this.DirectiveNodes.Count == 0)
          this.DirectiveNodes.Add(new List<Task.GraphNode>((IEnumerable<Task.GraphNode>) new Task.GraphNode[1]
          {
            this
          }));
        return isTaskLeftToRight1;
      }
    }

    public bool TaskLeftToRight
    {
      get
      {
        bool? isTaskLeftToRight = this.IsTaskLeftToRight;
        bool taskLeftToRight;
        if (!isTaskLeftToRight.HasValue)
        {
          Task.Graph graph = this.Graph;
          taskLeftToRight = graph == null || graph.LeftToRight;
        }
        else
          taskLeftToRight = isTaskLeftToRight.Value;
        Task.Graph graph1 = this.Graph;
        if ((graph1 != null ? (!graph1.LeftToRight ? 1 : 0) : 1) != 0)
          taskLeftToRight = !taskLeftToRight;
        return taskLeftToRight;
      }
    }

    /// <summary>ориентация нода относительно начала графа. Учитывается, что граф сам может быть справа-налево</summary>
    internal bool? LTR
    {
      get
      {
        bool? ltr = new bool?();
        if (this.Task != null)
        {
          if (this.Task.ConstraintType == ConstraintType.AsSoonAsPossible)
            ltr = new bool?(false);
          else if (this.Task.ConstraintType == ConstraintType.AsLateAsPossible)
            ltr = new bool?(true);
          if (ltr.HasValue)
          {
            Task.Graph graph = this.Graph;
            if ((graph != null ? (!graph.LeftToRight ? 1 : 0) : 1) != 0)
              ltr = new bool?(!ltr.Value);
          }
        }
        return ltr;
      }
    }

    [Intermech.Diagnostics.NotNull]
    internal List<Task.GraphNode> GetGluedNodes(bool ltr)
    {
      Task.GraphLinkDirection graphLinkDirection = Task.GraphLinkDirection.Forward;
      if (!ltr)
        graphLinkDirection = Task.GraphLinkDirection.Backward;
      List<Task.GraphNode> gluedNodes1 = new List<Task.GraphNode>();
      foreach (Task.GraphLink link in (List<Task.GraphLink>) this.Links)
      {
        if (link.Direction == graphLinkDirection)
        {
          Task.GraphNode node1 = link.Node;
          if ((node1 != null ? (node1.SummaryNodeKind != 0 ? 1 : 0) : 1) == 0)
          {
            if (link.Node is Task.ConstraintNode && !gluedNodes1.Contains(link.Node))
              gluedNodes1.Add(link.Node);
            Task.GraphNode node2 = link.Node;
            bool? nullable1;
            bool? nullable2;
            if (node2 == null)
            {
              nullable1 = new bool?();
              nullable2 = nullable1;
            }
            else
              nullable2 = node2.LTR;
            bool? nullable3 = nullable2;
            nullable1 = nullable3.HasValue ? new bool?(!nullable3.GetValueOrDefault()) : new bool?();
            bool flag = ltr;
            if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
            {
              List<Task.GraphNode> gluedNodes2 = link.Node.GetGluedNodes(ltr);
              if (gluedNodes2.Count > 0)
              {
                foreach (Task.GraphNode graphNode in gluedNodes2)
                {
                  if (!gluedNodes1.Contains(graphNode))
                    gluedNodes1.Add(graphNode);
                }
              }
              else if (!gluedNodes1.Contains(link.Node))
                gluedNodes1.Add(link.Node);
            }
          }
          else
            break;
        }
      }
      return gluedNodes1;
    }

    [Intermech.Diagnostics.NotNull]
    internal List<Task.GraphNode> GetNEGluedNodes(bool ltr)
    {
      List<Task.GraphNode> gluedNodes = this.GetGluedNodes(ltr);
      if (gluedNodes.Count == 0)
        gluedNodes.Add(this);
      return gluedNodes;
    }

    public override bool Equals([CanBeNull] object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return obj is Task.GraphNode graphNode && this.SummaryNodeKind == graphNode.SummaryNodeKind && object.Equals((object) this.Task, (object) graphNode.Task);
    }

    public override int GetHashCode()
    {
      return this.GetType() == typeof (Task.GraphNode) ? (this.Task != null ? this.Task.GetHashCode() : 0) + 17 * (int) this.SummaryNodeKind : base.GetHashCode();
    }

    public void LinkTo([Intermech.Diagnostics.NotNull] Task.GraphNode node)
    {
      this.Links.Add(new Task.GraphLink(node, Task.GraphLinkDirection.Forward));
      node.Links.Add(new Task.GraphLink(this, Task.GraphLinkDirection.Backward));
    }
  }

  internal class LeftNode([Intermech.Diagnostics.NotNull] Task.Graph graph) : Task.GraphNode(graph, (Task) null)
  {
  }

  internal class RightNode([Intermech.Diagnostics.NotNull] Task.Graph graph) : Task.GraphNode(graph, (Task) null)
  {
  }

  internal class EndNode([Intermech.Diagnostics.NotNull] Task.Graph graph, [CanBeNull] Task task) : 
    Task.GraphNode(graph, task)
  {
    public override double Work => 0.0;

    public override string ToString() => $"{base.ToString()} [{this.GetType().Name}]";
  }

  internal class StartNode([Intermech.Diagnostics.NotNull] Task.Graph graph, [CanBeNull] Task task) : 
    Task.EndNode(graph, task)
  {
  }

  internal class LagNode : Task.GraphNode
  {
    [CanBeNull]
    public Dependency Dependency { get; }

    public LagNode([Intermech.Diagnostics.NotNull] Task.Graph graph, [Intermech.Diagnostics.NotNull] Task task, [CanBeNull] Dependency dep)
      : base(graph, task)
    {
      this.Dependency = dep;
    }

    public override double Work
    {
      get
      {
        return this.Dependency?.LagUnit?.ToHours(this.Dependency.Lag, this.Task?.ProjectSchedule) ?? 0.0;
      }
    }

    public override double GetWeight(bool forward, double prevValue)
    {
      this.Date[forward] = this.CurrentDt;
      return this.Work;
    }

    public override string ToString()
    {
      return $"{base.ToString()} [Lag:{this.Dependency?.LagString ?? "NoDependence"}]";
    }
  }

  internal class ConstraintNode : Task.GraphNode
  {
    private bool _bad = true;
    private DateTime _validForDT = DateTime.MinValue;
    private double _work;

    [Intermech.Diagnostics.NotNull]
    [ItemNotNull]
    public List<Task.GraphNode> Nodes { get; } = new List<Task.GraphNode>();

    internal bool HasExternal { get; set; }

    public ConstraintNode([Intermech.Diagnostics.NotNull] Task.Graph graph, bool forward)
      : base(graph, (Task) null)
    {
      this.Forward = forward;
    }

    public bool Bad => this.Task != null && this._bad;

    public bool Forward { get; private set; }

    public override double GetWeight(bool forward, double prevValue)
    {
      bool flag = this.Forward ? !forward : forward;
      if (!flag && !this._validForDT.Equals(DateTime.MinValue) && this.CurrentDt != this._validForDT)
        this._bad = true;
      if (!this._bad)
        return this._work;
      this._bad = flag;
      this.Date[forward] = this.CurrentDt;
      if (this.Nodes.Count > 0 && !this._bad)
      {
        this._validForDT = this.CurrentDt;
        this._work = 0.0;
        if (this.Task != null)
        {
          DateTime dateTime = this.CurrentDt;
          Task.Graph graph1 = this.Graph;
          bool isStart = (graph1 != null ? (graph1.LeftToRight ? 1 : 0) : 1) != 0 ? forward : !forward;
          Task.GraphNode node = this.Nodes[0];
          this.Task = node.Task;
          foreach (Task.GraphNode graphNode in this.Graph?.NodesBetween((Task.GraphNode) this, node) ?? new List<Task.GraphNode>())
          {
            if (graphNode.Task != null && graphNode != this)
            {
              double work = graphNode.Work;
              int num = graphNode.Index > this.Index ? 1 : -1;
              Task.Graph graph2 = this.Graph;
              if ((graph2 != null ? (!graph2.LeftToRight ? 1 : 0) : 1) != 0)
                num = -num;
              double hours = work * (double) num;
              dateTime = graphNode.Task.AddWorkTime(dateTime, hours);
            }
          }
          DateTime date = dateTime;
          if (this.Task.LevelingDelay != 0.0)
            date = date.AddDays(this.Task.LevelingDelay);
          if (this.HasExternal)
            date = this.Task.AdjustByDependencies(date, isStart, true);
          else
            this.Task.AdjustByConstraint(ref date, isStart);
          if (date != dateTime)
          {
            double workHours = this.Task.GetWorkHours(dateTime, date, true);
            if (workHours < 0.0)
            {
              Task.Graph graph3 = this.Graph;
              if ((graph3 != null ? (graph3.LeftToRight ? 1 : 0) : 1) != 0)
              {
                if (!forward)
                  workHours *= -1.0;
              }
              else if (forward)
                workHours *= -1.0;
            }
            double num;
            if (workHours >= 0.0)
            {
              double val1 = double.MaxValue;
              if (this.Value[!forward] != -1.0)
                val1 = prevValue - this.Value[!forward];
              num = Math.Min(val1, workHours);
            }
            else
            {
              this.Forward = !this.Forward;
              this._bad = true;
              num = 0.0;
            }
            this._work += num;
          }
        }
      }
      return this._work;
    }

    public override double Work => this._work;

    public override string ToString()
    {
      string str1 = "-->";
      if (!this.Forward)
        str1 = "<--";
      string str2 = string.Empty;
      if (this.Nodes.Count > 0 && this.Nodes[0] != null)
        str2 = this.Nodes[0].TaskName;
      return $"{(object) this.Index}. Constraint for \"{str2}\" {base.ToString()} {str1}";
    }
  }

  internal class ConstraintSpacerNode([Intermech.Diagnostics.NotNull] Task.Graph graph, [Intermech.Diagnostics.NotNull] Task task) : 
    Task.GraphNode(graph, task)
  {
    public override string ToString() => "Constraint spacer for " + base.ToString();
  }

  internal class Graph : HashedList<Task.GraphNode>
  {
    internal static int ChainCounter;
    [Intermech.Diagnostics.NotNull]
    public string ErrorNodes = string.Empty;
    [Intermech.Diagnostics.NotNull]
    private readonly Intermech.Project.Project _project;
    [CanBeNull]
    private List<Task.GraphPath> _criticalPaths;
    [CanBeNull]
    private HashedList<Task> _criticalTasks;
    [CanBeNull]
    private List<Task.GraphNode> _debugPrinted;
    [Intermech.Diagnostics.NotNull]
    private string _debugString = string.Empty;
    private int _debugIndentLevel;
    [Intermech.Diagnostics.NotNull]
    private readonly List<Task.GraphNode> _nbAntiLoop = new List<Task.GraphNode>();

    [CanBeNull]
    public Task.GraphNode Left { get; }

    [CanBeNull]
    public Task.GraphNode Right { get; }

    [Intermech.Diagnostics.NotNull]
    public new Task.GraphNode this[[ZeroOrPositiveNumber] int index]
    {
      [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => base[index];
      [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] set => base[index] = value;
    }

    public DateTime Start { get; }

    public bool LeftToRight { get; }

    public new void Add([Intermech.Diagnostics.NotNull] Task.GraphNode item) => base.Add(item);

    [CanBeNull]
    internal Task.GraphNode FindByTask([Intermech.Diagnostics.NotNull] Task t)
    {
      return this.FindByTask(t, Task.SummaryNodeKind.None);
    }

    [CanBeNull]
    internal Task.GraphNode FindByTask([Intermech.Diagnostics.NotNull] Task t, Task.SummaryNodeKind kind)
    {
      return this.FindByHash(new Task.GraphNode((Task.Graph) null, t, kind));
    }

    [Intermech.Diagnostics.NotNull]
    protected List<Task.GraphNode> AddTask([Intermech.Diagnostics.NotNull] Task.GraphNode parent, [Intermech.Diagnostics.NotNull] Task t)
    {
      return this.AddTask(parent, t, -1);
    }

    [Intermech.Diagnostics.NotNull]
    protected List<Task.GraphNode> AddTask([Intermech.Diagnostics.NotNull] Task.GraphNode parent, [Intermech.Diagnostics.NotNull] Task t, int chainID)
    {
      return this.AddTask(parent, t, chainID, (Dependency) null);
    }

    [Intermech.Diagnostics.NotNull]
    protected List<Task.GraphNode> AddTask(
      [Intermech.Diagnostics.NotNull] Task.GraphNode parent,
      [Intermech.Diagnostics.NotNull] Task t,
      int chainID,
      [CanBeNull] Dependency dependency,
      [CanBeNull] HashedList<Task.GraphNode> chainedNodes = null,
      bool getFullChain = false)
    {
      HashedList<Task.GraphNode> chainedNodes1 = new HashedList<Task.GraphNode>();
      chainedNodes1.SkipDuplicates = true;
      Task.SummaryNodeKind kind1 = Task.SummaryNodeKind.None;
      if (t.HasLoadedSubTasks)
        kind1 = Task.SummaryNodeKind.Start;
      Task.GraphNode graphNode1 = this.FindByTask(t, kind1);
      if (graphNode1 != null && chainedNodes != null && chainedNodes.Contains(graphNode1))
        return (List<Task.GraphNode>) chainedNodes1;
      bool flag1 = graphNode1 == null;
      if (graphNode1 == null)
      {
        Task.SummaryNodeKind kind2 = t.HasLoadedSubTasks ? Task.SummaryNodeKind.Start : Task.SummaryNodeKind.None;
        graphNode1 = new Task.GraphNode(this, t, kind2);
        if (chainID == -1)
        {
          ++Task.Graph.ChainCounter;
          graphNode1.ChainID = Task.Graph.ChainCounter;
        }
        else
          graphNode1.ChainID = chainID;
      }
      chainedNodes1.Add(graphNode1);
      DependencyType dependencyType = dependency != null ? dependency.DependencyType : DependencyType.FinishStart;
      bool flag2 = dependencyType == DependencyType.FinishFinish || dependencyType == DependencyType.StartFinish;
      bool flag3 = dependencyType == DependencyType.StartStart || dependencyType == DependencyType.StartFinish;
      if (!this.LeftToRight)
      {
        int num = flag2 ? 1 : 0;
        flag2 = flag3;
        flag3 = num != 0;
      }
      double num1 = dependency != null ? dependency.Lag : 0.0;
      if (flag3)
      {
        if (parent.StartNode == null)
        {
          parent.StartNode = new Task.StartNode(this, parent.Task);
          parent.StartNode.ChainID = graphNode1.ChainID;
          chainedNodes1.Add((Task.GraphNode) parent.StartNode);
          parent.StartNode.Links.Add(new Task.GraphLink(parent, Task.GraphLinkDirection.Forward));
          parent.Links.Add(new Task.GraphLink((Task.GraphNode) parent.StartNode, Task.GraphLinkDirection.Backward));
          if (this.OuterNode != null)
          {
            Task.GraphLink link1 = parent.FindLink(this.OuterNode, Task.GraphLinkDirection.Backward);
            if (link1 != null)
            {
              Task.GraphLink link2 = link1.Node?.FindLink(parent, Task.GraphLinkDirection.Forward);
              if (link2 != null)
              {
                link1.Node.Links.Remove(link2);
                parent.Links.Remove(link1);
              }
            }
          }
        }
        parent = (Task.GraphNode) parent.StartNode;
      }
      if (num1 != 0.0)
      {
        Task.LagNode node = new Task.LagNode(this, t, dependency);
        node.ChainID = graphNode1.ChainID;
        chainedNodes1.Add((Task.GraphNode) node);
        parent.Links.Add(new Task.GraphLink((Task.GraphNode) node, Task.GraphLinkDirection.Forward));
        node.Links.Add(new Task.GraphLink(parent, Task.GraphLinkDirection.Backward));
        parent = (Task.GraphNode) node;
      }
      if (flag2)
      {
        if (graphNode1.EndNode == null)
        {
          graphNode1.EndNode = new Task.EndNode(this, t);
          graphNode1.EndNode.ChainID = graphNode1.ChainID;
          chainedNodes1.Add((Task.GraphNode) graphNode1.EndNode);
          graphNode1.Links.Add(new Task.GraphLink((Task.GraphNode) graphNode1.EndNode, Task.GraphLinkDirection.Forward));
          graphNode1.EndNode.Links.Add(new Task.GraphLink(graphNode1, Task.GraphLinkDirection.Backward));
        }
        graphNode1 = (Task.GraphNode) graphNode1.EndNode;
      }
      if (!this.LeftToRight && dependencyType == DependencyType.StartFinish)
      {
        Task.GraphNode graphNode2 = parent;
        parent = graphNode1;
        graphNode1 = graphNode2;
      }
      if (!parent.IsLinkedTo(graphNode1, Task.GraphLinkDirection.Forward))
        parent.Links.Add(new Task.GraphLink(graphNode1, Task.GraphLinkDirection.Forward));
      if (!graphNode1.IsLinkedTo(parent, Task.GraphLinkDirection.Backward))
        graphNode1.Links.Add(new Task.GraphLink(parent, Task.GraphLinkDirection.Backward));
      if (!this.LeftToRight && dependencyType == DependencyType.StartFinish)
        graphNode1 = parent;
      if (flag1 && t.HasLoadedSubTasks)
      {
        Task[] array = t.SubTasks.ToArray<Task>();
        int chainId = graphNode1.ChainID;
        HashSet<Task.GraphNode> graphNodeSet = new HashSet<Task.GraphNode>();
        foreach (Task t1 in array)
        {
          if (!this.HasIncomingLinksInsideSubtask(t1, array))
          {
            foreach (Task.GraphNode graphNode3 in this.AddTask(graphNode1, t1, chainId, (Dependency) null, getFullChain: true))
            {
              if (!chainedNodes1.Contains(graphNode3) && graphNodeSet.Add(graphNode3))
                chainedNodes1.Add(graphNode3);
            }
          }
        }
        Task.GraphNode node1 = this.FindByTask(t, Task.SummaryNodeKind.End);
        if (node1 == null)
        {
          node1 = new Task.GraphNode(this, t, Task.SummaryNodeKind.End);
          node1.ChainID = graphNode1.ChainID;
        }
        chainedNodes1.Add(node1);
        List<int> intList = new List<int>();
        for (int index = chainedNodes1.Count - 1; index >= 0; --index)
        {
          Task.GraphNode node2 = chainedNodes1[index];
          if (!intList.Contains(node2.ChainID) && node2 != node1 && ((IEnumerable<Task>) array).Contains<Task>(node2.Task))
          {
            node1.InsertAfter(node2, false);
            intList.Add(node2.ChainID);
          }
        }
        if (intList.Count == 0)
          node1.InsertAfter(graphNode1, false);
        if (t is Intermech.Project.Project && t.ManualPlanning)
        {
          Task.GraphNode node3 = new Task.GraphNode(this, t);
          node3.ChainID = graphNode1.ChainID;
          graphNode1.LinkTo(node3);
          node3.LinkTo(node1);
          chainedNodes1.Add(node3);
        }
        graphNode1 = node1;
      }
      if (flag1 | getFullChain)
      {
        int chainID1 = graphNode1.ChainID;
        if (this.LeftToRight)
        {
          foreach (Dependency relatedDependency in (System.Collections.ObjectModel.Collection<Dependency>) t.RelatedDependencies)
          {
            if (!relatedDependency.External)
            {
              chainedNodes1.AddRange((IEnumerable<Task.GraphNode>) this.AddTask(graphNode1, relatedDependency.Task, chainID1, relatedDependency, chainedNodes1, getFullChain));
              chainID1 = -1;
            }
          }
        }
        else
        {
          foreach (Dependency dependency1 in (IEnumerable<Dependency>) t.Dependencies)
          {
            if (!dependency1.External)
            {
              chainedNodes1.AddRange((IEnumerable<Task.GraphNode>) this.AddTask(graphNode1, dependency1.DependentOfTask, chainID1, dependency1, chainedNodes1, getFullChain));
              chainID1 = -1;
            }
          }
        }
      }
      return (List<Task.GraphNode>) chainedNodes1;
    }

    private void NormalizeOrder()
    {
      this.Clear();
      int num = 0;
      HashedList<Task.GraphNode> hashedList = new HashedList<Task.GraphNode>();
      hashedList.Add(this.Left);
      while (hashedList.Count > 0)
      {
        bool flag = true;
        for (int index = 0; index < hashedList.Count; ++index)
        {
          Task.GraphNode node = hashedList[index];
          if (!this.Contains(node) && !node.Links.Any<Task.GraphLink>((System.Func<Task.GraphLink, bool>) (link => link.Direction == Task.GraphLinkDirection.Backward && link.Tag != DBNull.Value)))
          {
            this.Add(node);
            node.Index = num;
            hashedList.RemoveAt(index);
            --index;
            ++num;
            foreach (Task.GraphLink link1 in (List<Task.GraphLink>) node.Links)
            {
              if (link1.Direction == Task.GraphLinkDirection.Forward)
              {
                Task.GraphLink link2 = link1.Node.FindLink(node, Task.GraphLinkDirection.Backward);
                if (link2 != null)
                  link2.Tag = (object) DBNull.Value;
                if (!this.Contains(link1.Node) && !hashedList.Contains(link1.Node))
                  hashedList.Add(link1.Node);
              }
            }
            flag = false;
          }
        }
        if (flag)
        {
          using (List<Task.GraphNode>.Enumerator enumerator = hashedList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Task.GraphNode current = enumerator.Current;
              string empty = string.Empty;
              foreach (Task.GraphLink link in (List<Task.GraphLink>) current.Links)
              {
                if (link.Direction == Task.GraphLinkDirection.Backward && link.Tag != DBNull.Value)
                {
                  if (empty != string.Empty)
                    empty += " | ";
                  empty += link.Node?.ToString() ?? "No node";
                }
              }
              if (empty != string.Empty)
              {
                if (this.ErrorNodes != string.Empty)
                  this.ErrorNodes += "\r\n";
                this.ErrorNodes = $"{this.ErrorNodes}{(object) current} <= {empty}";
              }
            }
            break;
          }
        }
      }
    }

    protected bool IsStartTask([Intermech.Diagnostics.NotNull] Task t)
    {
      foreach (Dependency dependency in this.LeftToRight ? (IEnumerable<Dependency>) t.Dependencies : (IEnumerable<Dependency>) t.RelatedDependencies)
      {
        if (!dependency.External)
          return false;
      }
      return true;
    }

    protected bool HasIncomingLinksInsideSubtask([Intermech.Diagnostics.NotNull] Task t, [Intermech.Diagnostics.NotNull] Task[] siblingTasks)
    {
      foreach (Dependency dependency in this.LeftToRight ? (IEnumerable<Dependency>) t.Dependencies : (IEnumerable<Dependency>) t.RelatedDependencies)
      {
        Task t1 = this.LeftToRight ? dependency.DependentOfTask : dependency.Task;
        if (((IEnumerable<Task>) siblingTasks).Contains<Task>(t1) || t1 != null && this.HasIncomingLinksInsideSubtask(t1, siblingTasks))
          return true;
      }
      return false;
    }

    public Graph([Intermech.Diagnostics.NotNull] Intermech.Project.Project project)
      : this((IEnumerable<Task>) project.SubTasksForGraph, project.LeftToRight ? project.Start : project.Finish, project.LeftToRight)
    {
      this._project = project;
    }

    private Graph([Intermech.Diagnostics.NotNull] IEnumerable<Task> tasks, DateTime start, bool leftToRight)
    {
      this.Left = (Task.GraphNode) new Task.LeftNode(this);
      this.Right = (Task.GraphNode) new Task.RightNode(this);
      this.Start = start;
      this.LeftToRight = leftToRight;
      Task.Graph.ChainCounter = 0;
      foreach (Task task in tasks)
      {
        if (task != null && this.IsStartTask(task))
          this.AddTask(this.Left, task);
      }
      foreach (Task.GraphNode node1 in (List<Task.GraphNode>) this)
      {
        if (node1.StartNode != null)
        {
          for (int index = node1.Links.Count - 1; index >= 0; --index)
          {
            Task.GraphLink link1 = node1.Links[index];
            if (link1.Direction == Task.GraphLinkDirection.Backward && link1.Node != node1.StartNode)
            {
              Task.GraphNode node2 = link1.Node;
              Task.GraphLink link2 = node2?.FindLink(node1, Task.GraphLinkDirection.Forward);
              if (link2 != null)
              {
                node1.Links.RemoveAt(index);
                if (!node1.StartNode.IsLinkedTo(node2, Task.GraphLinkDirection.Forward))
                {
                  link2.Node = (Task.GraphNode) node1.StartNode;
                  node1.StartNode.Links.Add(new Task.GraphLink(node2, Task.GraphLinkDirection.Backward));
                }
                else
                  node2.Links.Remove(link2);
              }
            }
          }
        }
        if (this.Right != null && node1 != this.Right && !node1.Links.HasOutbound)
        {
          if (!node1.IsLinkedTo(this.Right, Task.GraphLinkDirection.Forward))
            node1.Links.Add(new Task.GraphLink(this.Right, Task.GraphLinkDirection.Forward));
          if (!this.Right.IsLinkedTo(node1, Task.GraphLinkDirection.Backward))
            this.Right.Links.Add(new Task.GraphLink(node1, Task.GraphLinkDirection.Backward));
        }
      }
      if (this.Left != null)
      {
        foreach (Task.GraphNode node in (List<Task.GraphNode>) this)
        {
          if (node != this.Left && !node.Links.HasInbound)
          {
            if (!node.IsLinkedTo(this.Left, Task.GraphLinkDirection.Backward))
              node.Links.Add(new Task.GraphLink(this.Left, Task.GraphLinkDirection.Backward));
            if (!this.Left.IsLinkedTo(node, Task.GraphLinkDirection.Forward))
              this.Left.Links.Add(new Task.GraphLink(node, Task.GraphLinkDirection.Forward));
          }
        }
      }
      for (int index1 = this.Count - 1; index1 >= 0; --index1)
      {
        Task.GraphNode graphNode = this[index1];
        Task task = graphNode.Task;
        bool flag = task != null && task.Dependencies.HasExternal;
        if (graphNode.Task != null && ((graphNode.Task.ConstraintType <= ConstraintType.AsLateAsPossible ? 0 : (graphNode.Task.ConstraintType < ConstraintType.ManualPlanning ? 1 : 0)) | (flag ? 1 : 0)) != 0)
        {
          int num = graphNode.TaskLeftToRight ? 1 : 0;
          List<Task.GraphNode> neGluedNodes1 = graphNode.GetNEGluedNodes(true);
          int count = neGluedNodes1.Count;
          List<Task.GraphNode> neGluedNodes2 = graphNode.GetNEGluedNodes(false);
          neGluedNodes1.AddRange((IEnumerable<Task.GraphNode>) neGluedNodes2);
          for (int index2 = 0; index2 < neGluedNodes1.Count; ++index2)
          {
            Task.ConstraintNode constraintNode = new Task.ConstraintNode(this, index2 >= count);
            constraintNode.HasExternal = flag;
            Task.GraphNode node = neGluedNodes1[index2];
            if (index2 < count)
              constraintNode.InsertAfter(node, true);
            else
              constraintNode.InsertBefore(node, true);
            constraintNode.Nodes.Add(graphNode);
          }
        }
      }
      this.NormalizeOrder();
    }

    private void CalcTime(bool goForward)
    {
      int count = this.Count;
      int index = 0;
      if (!goForward)
        index = count - 1;
      while (goForward && index < count || !goForward && index >= 0)
      {
        this.CalcTime(this[index], goForward);
        if (goForward)
          ++index;
        else
          --index;
      }
    }

    private bool CalcTime([Intermech.Diagnostics.NotNull] Task.GraphNode node, bool goForward)
    {
      if (node.Value[goForward] != -1.0)
        return true;
      Task.GraphLinkDirection graphLinkDirection = goForward ? Task.GraphLinkDirection.Backward : Task.GraphLinkDirection.Forward;
      double num1 = 0.0;
      if (!goForward)
        num1 = double.MaxValue;
      List<Task.GraphNode> graphNodeList = new List<Task.GraphNode>();
      double hours = 0.0;
      Task.GraphNode graphNode1 = (Task.GraphNode) null;
      foreach (Task.GraphLink link in (List<Task.GraphLink>) node.Links)
      {
        if (link.Direction == graphLinkDirection)
        {
          Task.GraphNode node1 = link.Node;
          double prevValue = node1.Value[goForward];
          if (prevValue == -1.0)
            return false;
          node.CurrentDt = node1.CurrentDt;
          double num2 = goForward ? node1.GetWeight(true, prevValue) : -node.GetWeight(false, prevValue);
          double num3 = prevValue + num2;
          if (goForward && num3 >= num1 || !goForward && num3 < num1)
          {
            hours = num2;
            graphNode1 = node1;
            if (goForward)
            {
              if (num3 != num1)
                graphNodeList.Clear();
              if (node1.Task != null && !graphNodeList.Contains(node1))
                graphNodeList.Add(node1);
            }
            num1 = num3;
          }
        }
      }
      if (graphNode1 != null)
      {
        if (!this.LeftToRight)
          hours = -hours;
        if (goForward)
        {
          if (graphNode1.Task != null && hours != 0.0)
          {
            node.CurrentDt = graphNode1.Task.AddWorkTime(graphNode1.CurrentDt, hours);
            node.CurrentDt = graphNode1.Task.ApplyWorkingTime(node.CurrentDt, true);
          }
          else
            node.CurrentDt = graphNode1.CurrentDt;
        }
        else
          node.CurrentDt = node.Task == null || hours == 0.0 ? graphNode1.CurrentDt : node.Task.AddWorkTime(graphNode1.CurrentDt, hours);
      }
      foreach (Task.GraphNode graphNode2 in graphNodeList)
        node.CriticalNodes.SafeAdd<Task.GraphNode>(graphNode2);
      node.Value[goForward] = num1;
      return true;
    }

    public double FullWork
    {
      get
      {
        return this.CriticalPaths.Select<Task.GraphPath, double>((System.Func<Task.GraphPath, double>) (path => path.FullWork)).FirstOrDefault<double>();
      }
    }

    private void FindCriticalTasksLTR([Intermech.Diagnostics.NotNull] Task.GraphNode node, [Intermech.Diagnostics.NotNull] Task.GraphPath list)
    {
      bool flag = false;
      if (list.Contains(node))
        return;
      foreach (Task.GraphLink link in (List<Task.GraphLink>) node.Links)
      {
        if (link.Direction == Task.GraphLinkDirection.Forward)
        {
          Task.GraphNode node1 = link.Node;
          double work = node.Work;
          if (node.Value[true] == node.Value[false] && node1.Value[true] - node.Value[true] == work && node1.Value[false] - node.Value[false] == work)
          {
            if (this._criticalTasks != null)
            {
              if (node1.Task != null && !this._criticalTasks.Contains(node1.Task))
                this._criticalTasks.Add(node1.Task);
              if (node.Task != null && !this._criticalTasks.Contains(node.Task))
                this._criticalTasks.Add(node.Task);
            }
            if (!flag && !list.Contains(node))
            {
              list.Add(node);
              flag = true;
            }
          }
          this.FindCriticalTasksLTR(node1, list);
        }
      }
    }

    internal void CheckCalculated()
    {
      if (this._criticalPaths != null)
        return;
      this.CalculateCriticalPaths();
    }

    private void CalculateCriticalPaths()
    {
      this._project.SetState(TaskState.GraphCalculating);
      try
      {
        this._criticalPaths = new List<Task.GraphPath>();
        this._criticalTasks = new HashedList<Task>();
        bool hasBadNodes;
        do
        {
          foreach (Task.GraphNode graphNode in (List<Task.GraphNode>) this)
          {
            graphNode.ClearCache(true);
            graphNode.ClearCache(false);
          }
          this.Left.CurrentDt = this.Start;
          this.CalcTime(true);
          this.Right.Date[true] = this.Right.CurrentDt;
          this.Right.Date[false] = this.Right.CurrentDt;
          hasBadNodes = this.HasBadNodes;
          this.Right.Value[false] = this.Right.Value[true];
          this.CalcTime(false);
        }
        while (hasBadNodes | this.HasBadNodes);
        Task.GraphPath list = new Task.GraphPath();
        if (this._criticalPaths != null)
          this._criticalPaths.Add(list);
        this.FindCriticalTasksLTR(this.Left, list);
      }
      finally
      {
        this._project.UnsetState(TaskState.GraphCalculating);
      }
    }

    private bool HasBadNodes
    {
      get
      {
        return this.Any<Task.GraphNode>((System.Func<Task.GraphNode, bool>) (node => node is Task.ConstraintNode constraintNode && constraintNode.Bad));
      }
    }

    [Intermech.Diagnostics.NotNull]
    public List<Task.GraphPath> CriticalPaths
    {
      get
      {
        this.CheckCalculated();
        return this._criticalPaths;
      }
    }

    [Intermech.Diagnostics.NotNull]
    public List<Task> CriticalTasks
    {
      get
      {
        this.CheckCalculated();
        return (List<Task>) this._criticalTasks;
      }
    }

    public double GetTasksDistance([CanBeNull] Task.GraphNode from, [CanBeNull] Task.GraphNode to)
    {
      this.CheckCalculated();
      return from != null && to != null ? (to.Index <= from.Index ? from.Value[true] - to.Value[true] : to.Value[false] - from.Value[false] - from.Work) : 0.0;
    }

    public double GetTasksDistance([Intermech.Diagnostics.NotNull] Task from, [Intermech.Diagnostics.NotNull] Task.GraphNode to)
    {
      this.CheckCalculated();
      return this.GetTasksDistance(this.FindByTask(from), to);
    }

    public double GetTasksDistance([Intermech.Diagnostics.NotNull] Task from, [Intermech.Diagnostics.NotNull] Task to)
    {
      this.CheckCalculated();
      Task.GraphNode byTask = this.FindByTask(to);
      return this.GetTasksDistance(from, byTask);
    }

    [CanBeNull]
    public Task.GraphNode GetNode([Intermech.Diagnostics.NotNull] Task task)
    {
      this.CheckCalculated();
      return this.FindByTask(task);
    }

    [CanBeNull]
    public Task.GraphNode OuterNode => !this.LeftToRight ? this.Left : this.Right;

    [CanBeNull]
    public Task.GraphNode InnerNode => !this.LeftToRight ? this.Right : this.Left;

    private void DebugIndent(bool increase)
    {
      if (increase)
        ++this._debugIndentLevel;
      else
        --this._debugIndentLevel;
    }

    [Intermech.Diagnostics.NotNull]
    private string DebugIndentString => new string(' ', this._debugIndentLevel * 2);

    private void DebugWriteLine([Intermech.Diagnostics.NotNull] string s)
    {
      this._debugString = $"{this._debugString}{this.DebugIndentString}{s}\r\n";
    }

    private void DebugPrint([Intermech.Diagnostics.NotNull] Task.GraphNode node)
    {
      double num1;
      node.Value.TryGetValue(true, out num1);
      double num2;
      node.Value.TryGetValue(false, out num2);
      Task.GraphNode graphNode = node;
      string str = $" {num1}/{num2}, w: {node.Work}, dt: {node.Date[true]}/{node.Date[false]}";
      string s = graphNode.ToString() + str;
      if (this._debugPrinted != null)
      {
        if (this._debugPrinted.Contains(node))
        {
          this.DebugWriteLine(s + " REPEAT, LOOP POSSIBLE");
          return;
        }
        this._debugPrinted.Add(node);
      }
      this.DebugWriteLine(s);
      this.DebugIndent(true);
      foreach (Task.GraphLink link in (List<Task.GraphLink>) node.Links)
      {
        if (link.Direction == Task.GraphLinkDirection.Forward && link.Node != null)
          this.DebugPrint(link.Node);
      }
      this.DebugIndent(false);
    }

    [Intermech.Diagnostics.NotNull]
    public string DebugPrint()
    {
      this._debugString = $"Nodes: {this.Count}, constraints: {this.OfType<Task.ConstraintNode>().Count<Task.ConstraintNode>()}\r\n\r\n";
      this._debugPrinted = new List<Task.GraphNode>();
      if (this.Left != null)
        this.DebugPrint(this.Left);
      return this._debugString;
    }

    public void DebugOut()
    {
    }

    public bool ForwardDirection => this.LeftToRight;

    [CanBeNull]
    private List<Task.GraphNode> InternalNodesBetween([Intermech.Diagnostics.NotNull] Task.GraphNode from, [Intermech.Diagnostics.NotNull] Task.GraphNode to)
    {
      if (this._nbAntiLoop.Contains(from))
        return (List<Task.GraphNode>) null;
      this._nbAntiLoop.Add(from);
      Task.GraphLinkDirection graphLinkDirection = Task.GraphLinkDirection.Forward;
      if (from.Index > to.Index)
        graphLinkDirection = Task.GraphLinkDirection.Backward;
      List<Task.GraphNode> graphNodeList = new List<Task.GraphNode>();
      foreach (Task.GraphLink link in (List<Task.GraphLink>) from.Links)
      {
        if (link.Direction == graphLinkDirection)
        {
          if (link.Node == to)
            return new List<Task.GraphNode>(Enumeration.Create<Task.GraphNode>(from));
          if (link.Node != null)
          {
            List<Task.GraphNode> collection = this.InternalNodesBetween(link.Node, to);
            if (collection != null)
              graphNodeList.AddRange((IEnumerable<Task.GraphNode>) collection);
          }
        }
      }
      return graphNodeList.Count <= 0 ? (List<Task.GraphNode>) null : graphNodeList;
    }

    [CanBeNull]
    internal List<Task.GraphNode> NodesBetween([Intermech.Diagnostics.NotNull] Task.GraphNode from, [Intermech.Diagnostics.NotNull] Task.GraphNode to)
    {
      this._nbAntiLoop.Clear();
      return this.InternalNodesBetween(from, to);
    }

    internal DateTime RightDT
    {
      get
      {
        this.CheckCalculated();
        return this.Right.CurrentDt;
      }
    }
  }

  internal class GraphPath : HashedList<Task.GraphNode>
  {
    public double FullWork
    {
      get => this.Sum<Task.GraphNode>((System.Func<Task.GraphNode, double>) (node => node.Work));
    }
  }

  /// <summary>Делегат для события, вызываемого перед коммитом создания объекта задачи в БД (первое сохранение)</summary>
  protected delegate void SaveDbObjectAttributesDelegate([Intermech.Diagnostics.NotNull] IDBObject taskDbObject);
}
