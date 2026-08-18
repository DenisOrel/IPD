// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Dependency
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Project.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Dependency : Entity
{
  private DependencyType _dependencyType = DependencyType.FinishStart;
  [CanBeNull]
  [NonSerialized]
  private Task _dependentOfTask;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [CanBeNull]
  [NonSerialized]
  internal Task _Task;
  internal long _DependentOfTaskHash;
  internal DependencyState _State;
  internal long _DependentOfTaskObject;
  [NonSerialized]
  protected long _ObjectID;
  [NotNull]
  private static readonly Regex _extractShortRegex = new Regex("\\((.*?)\\)", RegexOptions.Compiled);
  [CanBeNull]
  private static Dictionary<DependencyType, string> _shortDepTypes;
  [NotNull]
  private static readonly Regex _shortRegex = new Regex("^\\s*(.*?)([+-][\\d.,]+\\s*.*?)\\s*$", RegexOptions.Compiled);
  private double _lag;
  [CanBeNull]
  private WorkTimeUnit _lagUnit;

  public Dependency()
  {
  }

  public Dependency([NotNull] Task dependentOfTask)
    : this()
  {
    this.DependentOfTask = dependentOfTask;
  }

  public Dependency([NotNull] Task dependentOfTask, DependencyType dependencyType)
    : this(dependentOfTask)
  {
    this.DependencyType = dependencyType;
  }

  public virtual void Delete()
  {
    if (this.Task == null)
      return;
    this.Task.Dependencies.Remove(this);
  }

  public virtual DependencyType DependencyType
  {
    get => this._dependencyType;
    set
    {
      this.OnPropertyChanging(nameof (DependencyType));
      this._dependencyType = value;
      this.OnPropertyChanged(nameof (DependencyType));
      this.OnPropertyChangeCompleted(nameof (DependencyType));
    }
  }

  public long DependentOfTaskObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._DependentOfTaskObject;
    }
  }

  [CanBeNull]
  public virtual Task DependentOfTask
  {
    [DebuggerStepThrough] get => this._dependentOfTask;
    set
    {
      this.Validate(value);
      this.OnPropertyChanging(nameof (DependentOfTask));
      if (this._dependentOfTask != null)
        this._dependentOfTask.BackDependencies.Remove(this);
      this._dependentOfTask = value;
      if (value != null)
        this._DependentOfTaskObject = value.ObjectID;
      if (this._dependentOfTask != null && this._Task != null)
        this._dependentOfTask.BackDependencies.Add(this);
      this.OnPropertyChanged(nameof (DependentOfTask));
      this.OnPropertyChangeCompleted(nameof (DependentOfTask));
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

  [CanBeNull]
  public virtual Task Task
  {
    get => this._Task;
    set
    {
      if (value == this.Task)
        return;
      if (this.Task != null)
        this.Task.Dependencies.Remove(this);
      this._Task = value;
      if (this.Task == null)
        return;
      this.Task.Dependencies.Add(this);
    }
  }

  public void Validate() => this.Validate((Task) null);

  public void Validate([CanBeNull] Task dependentOfTask)
  {
    if (dependentOfTask == null)
      dependentOfTask = this.DependentOfTask;
    if (dependentOfTask == null || this._Task == null)
      return;
    if (this._Task.Contains(dependentOfTask) || dependentOfTask.Contains(this._Task))
    {
      string str = $" ({dependentOfTask.Name}->{this._Task.Name}): ";
      throw new ArgumentException(Localization.GetString("CantAddDependency") + str + Localization.GetString("ErrParentChildDependency"));
    }
    if (dependentOfTask.DependsOf(this._Task))
    {
      string str = $" ({dependentOfTask.Name}->{this._Task.Name}): ";
      throw new ArgumentException(Localization.GetString("CantAddDependency") + str + Localization.GetString("ErrCircularDependency"));
    }
    if (dependentOfTask.AllSubTasks.Any<Task>((System.Func<Task, bool>) (task2 => task2.DependsOf(this._Task))))
    {
      string str = $" ({dependentOfTask.Name}->{this._Task.Name}): ";
      throw new ArgumentException(Localization.GetString("CantAddDependency") + str + Localization.GetString("ErrCircularDependency"));
    }
  }

  public long ObjectID
  {
    [DebuggerStepThrough] get => this._ObjectID;
  }

  internal long HackObjectID
  {
    [DebuggerStepThrough] set => this._ObjectID = value;
  }

  private void InternalSave([NotNull] IDBObject obj, bool saveAll)
  {
    obj.Caption = string.Format(Resources.DependencyCaption, (object) this.DependentOfTask.Name.Truncate(50), (object) this.Task.Name.Truncate(50));
    this.Task.ProjectNeeded();
    obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Project).AsInteger = this.Task.Project.ObjectID;
    if (saveAll)
    {
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.ToTask).AsInteger = this.Task.ObjectID;
      obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.FromTask).AsInteger = this.DependentOfTask.ObjectID;
    }
    obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.DependencyType).AsInteger = (long) this._dependencyType;
    IDBAttribute dbAttribute = obj.AttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.Lag);
    double lag = this.Lag;
    WorkTimeUnit lagUnit = this.LagUnit;
    long measureID = lagUnit != null ? lagUnit.MeasureID : MeasureUnit.Days.ID;
    MeasuredValue measuredValue = new MeasuredValue(lag, measureID);
    dbAttribute.Value = (object) measuredValue;
  }

  internal bool JustCreated { get; private set; }

  /// <summary>Сохраняет зависимость</summary>
  /// <returns>Возвращает true, если сохранение прошло успешно, в случае false требуется повторный вызов после сохранения остальных задач проекта (т.е. сохранение не
  /// прошло из-за того, что идентификатор связанной задачи не был известен)</returns>
  public bool Save([NotNull] IUserSession session)
  {
    if (this.DependentOfTask.ObjectID == 0L)
      return false;
    this.JustCreated = false;
    IDBObject dbObject;
    if (this.ObjectID == 0L)
    {
      dbObject = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency).Create();
      this.InternalSave(dbObject, false);
      dbObject.CommitCreation(true);
      this.JustCreated = true;
    }
    else
      dbObject = session.GetObject(this.ObjectID);
    if (this._Task != null)
      this.CopyLcStepFromTask(ref dbObject);
    this.InternalSave(dbObject, true);
    return true;
  }

  internal bool CheckIn([CanBeNull] ref IDBObject obj)
  {
    if (obj == null || obj.CheckoutBy != obj.Session.UserID)
      return false;
    obj.CheckIn();
    this._ObjectID *= -1L;
    obj = obj.Session.GetObject(this._ObjectID, false);
    return true;
  }

  internal bool CheckIn()
  {
    if (this._Task != null)
    {
      IUserSession session = this._Task.GetSession();
      try
      {
        IDBObject dbObject = session.GetObject(this.ObjectID, false);
        if (dbObject != null)
          return this.CheckIn(ref dbObject);
      }
      finally
      {
        this._Task.ReleaseSession();
      }
    }
    return false;
  }

  internal void CheckOut([CanBeNull] ref IDBObject obj)
  {
    if (obj == null || obj.CheckoutBy == obj.Session.UserID)
      return;
    obj = obj.CheckOut();
    this._ObjectID = obj.ObjectID;
  }

  internal void CheckOut()
  {
    if (this._Task == null)
      return;
    IUserSession session = this._Task.GetSession();
    try
    {
      IDBObject dbObject = session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return;
      this.CheckOut(ref dbObject);
    }
    finally
    {
      this._Task.ReleaseSession();
    }
  }

  internal void CopyLcStepFromTask()
  {
    if (this._Task == null)
      return;
    IUserSession session = this._Task.GetSession();
    try
    {
      IDBObject dbObject = session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return;
      this.CopyLcStepFromTask(ref dbObject);
    }
    finally
    {
      this._Task.ReleaseSession();
    }
  }

  internal void CopyLcStepFromTask([NotNull] ref IDBObject obj)
  {
    if (obj.LCStep != this._Task.LcStep)
    {
      this.CheckIn(ref obj);
      obj.LCStep = this._Task.LcStep;
    }
    if (!this._Task.CheckOutNeeded)
      return;
    this.CheckOut(ref obj);
  }

  public void Delete([NotNull] IUserSession session)
  {
    if (this.ObjectID == 0L)
      return;
    session.GetObject(this.ObjectID, false)?.Delete(0L);
  }

  public void Load([NotNull] Task task, [CanBeNull] Task dependentOfTask, [NotNull] DataRow row)
  {
    this._ObjectID = Convert.ToInt64(row[0]);
    if (dependentOfTask == null)
    {
      if (!DBNull.Value.Equals(row[1]))
      {
        this._DependentOfTaskObject = Convert.ToInt64(row[1]);
        dependentOfTask = task.RootProject.Tasks.FindByObjectID(this._DependentOfTaskObject);
      }
      else
        this._DependentOfTaskObject = 0L;
    }
    this.DependentOfTask = dependentOfTask;
    this.Task = task;
    if (!DBNull.Value.Equals(row[3]))
      this.DependencyType = (DependencyType) Convert.ToInt32(row[3]);
    string str = row.FieldAsStringDef(4, (string) null);
    if (str == null)
      return;
    this.LagString = str;
  }

  public void Load([NotNull] Task task, [NotNull] DataRow row) => this.Load(task, (Task) null, row);

  [NotNull]
  public string IndexString
  {
    get
    {
      if (this.DependentOfTask == null)
        return this.DependentOfTaskObjectID.ToString();
      string indexString = this.DependentOfTask.IndexString;
      if (this.CrossProject && indexString != string.Empty)
        indexString = $"{this.DependentOfTask.ProjectName}.{indexString}";
      return indexString;
    }
  }

  public bool CrossProject
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Task?.Project != this.DependentOfTask?.Project;
    }
  }

  public override string ToString()
  {
    if (this.DependentOfTask == null)
      return "?";
    string str = this.DependentOfTask.Name;
    if (this.CrossProject)
      str = $"{this.DependentOfTask.ProjectName}.{str}";
    return str;
  }

  public bool Resolved => this.DependentOfTask != null;

  public bool External => this.DependentOfTask is ExternalTask;

  [NotNull]
  public static Dictionary<DependencyType, string> ShortDepTypes
  {
    get
    {
      Dependency.LoadShortDepTypes();
      return Dependency._shortDepTypes;
    }
  }

  private static void LoadShortDepTypes()
  {
    if (Dependency._shortDepTypes != null)
      return;
    Dependency._shortDepTypes = new Dictionary<DependencyType, string>();
    foreach (DependencyType possibleValue in (IEnumerable<DependencyType>) EnumHelper.PossibleValues<DependencyType>())
    {
      string enumDescription = SimpleFuncs.GetEnumDescription((Enum) possibleValue);
      Match match = Dependency._extractShortRegex.Match(enumDescription);
      if (match.Success)
        enumDescription = match.Groups[1].Value;
      Dependency._shortDepTypes.Add(possibleValue, enumDescription);
    }
  }

  [NotNull]
  public string ShortName
  {
    get
    {
      string empty = string.Empty;
      string str = this.LagString;
      if (this.DependencyType != DependencyType.FinishStart || str != string.Empty)
        Dependency.ShortDepTypes.TryGetValue(this.DependencyType, out empty);
      if (str != string.Empty)
      {
        if (this.Lag > 0.0)
          str = "+" + str;
        empty += str;
      }
      return this.IndexString + empty;
    }
  }

  public static void ParseShortName([NotNull] ref string s, ref DependencyType depType, [CanBeNull] ref string lagString)
  {
    s = s.ToLower();
    Match match = Dependency._shortRegex.Match(s);
    if (match.Success)
    {
      s = match.Groups[1].Value;
      lagString = match.Groups[2].Value;
    }
    foreach (KeyValuePair<DependencyType, string> shortDepType in Dependency.ShortDepTypes)
    {
      DependencyType key;
      string str1;
      shortDepType.Deconstruct<DependencyType, string>(out key, out str1);
      DependencyType dependencyType = key;
      string str2 = str1;
      if (s.EndsWith(str2, StringComparison.OrdinalIgnoreCase))
      {
        s = s.Substring(0, s.Length - str2.Length).Trim();
        depType = dependencyType;
        break;
      }
    }
  }

  /// <summary>Запаздывание в единицах, указанных в LagUnit</summary>
  public double Lag
  {
    [DebuggerStepThrough] get => this._lag;
    set
    {
      if (this._lag == value)
        return;
      this.OnPropertyChanging(nameof (Lag));
      this._lag = value;
      this.OnPropertyChanged(nameof (Lag));
      this.OnPropertyChangeCompleted(nameof (Lag));
    }
  }

  /// <summary>Запаздывание в часах</summary>
  public double LagHours
  {
    get
    {
      WorkTimeUnit lagUnit = this.LagUnit;
      return lagUnit == null ? 0.0 : lagUnit.ToHours(this.Lag, this.Task?.ProjectSchedule);
    }
  }

  public bool HasLag => this.Lag != 0.0;

  /// <summary>Единицы измерения значения Lag</summary>
  [CanBeNull]
  public WorkTimeUnit LagUnit
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lagUnit;
    set
    {
      if (this._lagUnit == value)
        return;
      this.OnPropertyChanging(nameof (LagUnit));
      this._lagUnit = value;
      this.OnPropertyChanged(nameof (LagUnit));
      this.OnPropertyChangeCompleted(nameof (LagUnit));
    }
  }

  [NotNull]
  public string LagString
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Lag != 0.0 ? Intermech.Diagnostics.Check.Optional.NotNull<Task>(this.Task, "Task").FormatDurationNC(this.Lag, false, this.LagUnit) : string.Empty;
    }
    set
    {
      WorkTimeValue workTimeValue = WorkTimeUnits.Parse(value, WorkTimeUnits.Days, 0.0, true);
      this.Lag = workTimeValue != null ? workTimeValue.Value : throw new NotificationException(string.Format(Resources.ErrWrongDurationFormat, (object) value));
      this.LagUnit = workTimeValue.Unit;
    }
  }

  [NotNull]
  internal Schedule CurrentSchedule => this.Task.CurrentSchedule;
}
