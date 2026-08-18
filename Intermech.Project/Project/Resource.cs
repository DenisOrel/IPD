// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Resource
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Resource : Entity
{
  [CanBeNull]
  private string _functions;
  [CanBeNull]
  [NonSerialized]
  private ProjectCollection _internalProjects;
  [NotNull]
  private string _name;
  [CanBeNull]
  private string _notes;
  private double _overtimeWorkSupplementalHourCost;
  [CanBeNull]
  private Schedule _schedule;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  private double _workHourCost;
  protected long _ObjectID;
  protected int _ObjectType;

  [CanBeNull]
  public static Schedule GetSchedule([NotNull] IUserSession session, [NotEmpty] long objectID)
  {
    return Resource.GetSchedule(session, objectID, 0L);
  }

  [CanBeNull]
  private static Schedule GetSchedule([NotNull] IUserSession session, [NotEmpty] long objectID, long calendarID)
  {
    if (calendarID == 0L)
    {
      IDBAttribute attributeById = session.GetObject(objectID).GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar);
      if (attributeById != null)
        calendarID = attributeById.AsInteger;
    }
    Schedule schedule = (Schedule) null;
    if (calendarID != 0L)
      schedule = ScheduleList.GetSchedule(calendarID, session);
    return schedule;
  }

  public Resource(
    [CanBeNull] ISessionProvider provider,
    long objectID,
    [NotNull] string name,
    [NotEmpty] int objectType,
    long calendarID = 0)
  {
    this._workHourCost = IMProject.DefaultWorkHourCost;
    this._overtimeWorkSupplementalHourCost = IMProject.DefaultOvertimeWorkSupplementalHourCost;
    this._ObjectID = objectID;
    this._name = name;
    this._ObjectType = objectType;
    if (provider == null)
      return;
    IUserSession session = provider.GetSession();
    try
    {
      this._schedule = Resource.GetSchedule(session, objectID, calendarID);
    }
    finally
    {
      provider.ReleaseSession();
    }
  }

  private void Schedule_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    this.OnPropertyChanged("Schedule");
  }

  [NotNull]
  public virtual IEnumerable<Assignment> Assignments
  {
    get
    {
      return (IEnumerable<Assignment>) this.Projects.SelectMany<Intermech.Project.Project, Task>((Func<Intermech.Project.Project, IEnumerable<Task>>) (project => (IEnumerable<Task>) project.Tasks)).SelectMany<Task, Assignment>((Func<Task, IEnumerable<Assignment>>) (task => task.Assignments.Where<Assignment>((Func<Assignment, bool>) (assignment => assignment.Resource == this)))).ToList<Assignment>();
    }
  }

  [CanBeNull]
  internal string Functions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._functions;
    set
    {
      if (!(value != this.Functions))
        return;
      if (value != null)
      {
        if (value.StartsWith(IMProject.Unknown))
          throw new ArgumentException("Function value must not start with the Unknown string representation.", "Name");
        if (value.Contains(IMProject.UnitPreSymbol) || value.Contains(IMProject.UnitPostSymbol) || value.Contains(IMProject.CandidatesPreSymbol) || value.Contains(IMProject.CandidatesPostSymbol) || value.Contains(IMProject.PercentSymbol))
          throw new ArgumentException("Function value must not contain special unit or candidate list definition symbols, list separator symbols, or percent symbols.", "Name");
      }
      if (value == null)
        value = string.Empty;
      this.OnPropertyChanging(nameof (Functions));
      this._functions = string.Join(IMProject.ListSeparatorSymbol + " ", ((IEnumerable<string>) value.Split(new string[1]
      {
        IMProject.ListSeparatorSymbol
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (str => str.Trim())).ToArray<string>());
      this.OnPropertyChanged(nameof (Functions));
      this.OnPropertyChangeCompleted(nameof (Functions));
    }
  }

  [NotNull]
  public virtual string Name
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._name;
    set
    {
      if (!(value != this.Name))
        return;
      if (value != null)
      {
        if (value.StartsWith(IMProject.Unknown))
          throw new ArgumentException("Name value must not start with the Unknown string representation.", nameof (Name));
        if (value.Contains(IMProject.UnitPreSymbol) || value.Contains(IMProject.UnitPostSymbol) || value.Contains(IMProject.CandidatesPreSymbol) || value.Contains(IMProject.CandidatesPostSymbol) || value.Contains(IMProject.ListSeparatorSymbol) || value.Contains(IMProject.PercentSymbol))
          throw new ArgumentException("Name value must not contain special unit or candidate list definition symbols, list separator symbols, or percent symbols.", nameof (Name));
      }
      this.OnPropertyChanging(nameof (Name));
      this._name = value;
      this.OnPropertyChanged(nameof (Name));
      this.OnPropertyChangeCompleted(nameof (Name));
    }
  }

  [CanBeNull]
  public virtual string Notes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._notes;
    set
    {
      if (!(value != this.Notes))
        return;
      this.OnPropertyChanging(nameof (Notes));
      this._notes = value;
      this.OnPropertyChanged(nameof (Notes));
      this.OnPropertyChanged("NotesString");
      this.OnPropertyChangeCompleted(nameof (Notes));
    }
  }

  [NotNull]
  public virtual string NotesString
  {
    get => this.Notes?.Replace("\r\n", " ").Trim() ?? string.Empty;
    set => this.Notes = value;
  }

  public virtual double OvertimeWorkSupplementalHourCost
  {
    get => this._overtimeWorkSupplementalHourCost;
    set
    {
      if (value == this.OvertimeWorkSupplementalHourCost)
        return;
      this.OnPropertyChanging(nameof (OvertimeWorkSupplementalHourCost));
      this._overtimeWorkSupplementalHourCost = value;
      this.OnPropertyChanged(nameof (OvertimeWorkSupplementalHourCost));
      this.OnPropertyChangeCompleted(nameof (OvertimeWorkSupplementalHourCost));
    }
  }

  [NotNull]
  internal ProjectCollection projects
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._internalProjects ?? (this._internalProjects = new ProjectCollection());
    }
  }

  [NotNull]
  public virtual IReadOnlyCollection<Intermech.Project.Project> Projects
  {
    get => (IReadOnlyCollection<Intermech.Project.Project>) this.projects;
  }

  [CanBeNull]
  public virtual Schedule Schedule
  {
    get => this._schedule;
    set
    {
      if (value == this.Schedule)
        return;
      this.OnPropertyChanging(nameof (Schedule));
      if (this.Schedule != null)
        this.Schedule.PropertyChanged -= new PropertyChangedEventHandler(this.Schedule_PropertyChanged);
      this._schedule = value;
      if (this.Schedule != null)
        this.Schedule.PropertyChanged += new PropertyChangedEventHandler(this.Schedule_PropertyChanged);
      this.OnPropertyChanged(nameof (Schedule));
      this.OnPropertyChangeCompleted(nameof (Schedule));
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

  [NotNull]
  public virtual List<Task> Tasks
  {
    get
    {
      return this.Projects.SelectMany<Intermech.Project.Project, Task>((Func<Intermech.Project.Project, IEnumerable<Task>>) (p => p.Tasks.Where<Task>((Func<Task, bool>) (t => t.Assignments.Any<Assignment>((Func<Assignment, bool>) (a => a.Resource == this)))))).ToList<Task>();
    }
  }

  public virtual double WorkHourCost
  {
    get => this._workHourCost;
    set
    {
      if (value == this.WorkHourCost)
        return;
      this.OnPropertyChanging(nameof (WorkHourCost));
      this._workHourCost = value;
      this.OnPropertyChanged(nameof (WorkHourCost));
      this.OnPropertyChangeCompleted(nameof (WorkHourCost));
    }
  }

  [NotNull]
  public virtual IEnumerable<DateSchedule> WorkTime
  {
    get
    {
      return this.Tasks.SelectMany<Task, DateSchedule>((Func<Task, IEnumerable<DateSchedule>>) (t => (IEnumerable<DateSchedule>) t.WorkTime));
    }
  }

  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._ObjectID;
  }

  [NotEmpty]
  public int ObjectType
  {
    [DebuggerStepThrough] get => this._ObjectType;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is Resource resource && resource.ObjectID == this.ObjectID && resource.ObjectType == this.ObjectType;
  }

  public override int GetHashCode() => (this.ObjectID, this.ObjectType).GetHashCode();

  public override string ToString() => this.Name;
}
