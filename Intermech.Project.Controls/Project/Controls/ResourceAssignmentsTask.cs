// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourceAssignmentsTask
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project.Controls;

[Serializable]
public class ResourceAssignmentsTask : Task
{
  [NotNull]
  [NotEmpty]
  protected List<Task> _Subtasks = new List<Task>();

  [NotNull]
  [NotEmpty]
  public List<Task> ResourceAssignmentsTasks => this._Subtasks;

  [NotNull]
  [NotEmpty]
  protected override IReadOnlyList<Task> GetSubTasks() => (IReadOnlyList<Task>) this._Subtasks;

  public ResourceAssignmentsTask(long objectID)
    : base(objectID)
  {
  }

  public ResourceAssignmentsTask([NotNull] string name, [CanBeNull, ItemNotNull] List<Task> subtasks)
    : base(name)
  {
    if (subtasks == null)
      return;
    this._Subtasks = subtasks;
  }

  public override void LoadDependencies(IUserSession session)
  {
  }

  public override DateTime Start
  {
    get => this._Subtasks.Count == 0 && this.LeftToRight ? this._Start : base.Start;
  }

  public override DateTime Finish
  {
    get => this._Subtasks.Count == 0 && !this.LeftToRight ? this._Finish : base.Finish;
  }

  protected override void LoadSubTasksInternal(IUserSession session, Intermech.Project.Project project)
  {
    this._HasNotLoadedSubTasks = false;
    foreach (Task task in this._Subtasks.OfType<ResourceAssignmentsTask>())
      task.LoadAsSubTask((Task) this, project);
  }

  protected override bool IsSubTasksExist(IUserSession session)
  {
    return this._Subtasks != null && this._Subtasks.Count > 0;
  }

  public int AllSubTasksCount
  {
    get
    {
      return this._Subtasks.OfType<ResourceAssignmentsTask>().Sum<ResourceAssignmentsTask>((Func<ResourceAssignmentsTask, int>) (rt => 1 + rt.AllSubTasksCount));
    }
  }

  protected override IReadOnlyList<Task> GetAllSubTasks()
  {
    List<Task> allSubTasks = new List<Task>();
    foreach (Task subtask in this._Subtasks)
    {
      allSubTasks.Add(subtask);
      if (subtask is ResourceAssignmentsTask resourceAssignmentsTask)
        allSubTasks.AddRange((IEnumerable<Task>) resourceAssignmentsTask.AllSubTasks);
    }
    return (IReadOnlyList<Task>) allSubTasks;
  }

  public override bool Grayed => false;

  protected override void Load(Intermech.Project.Project project, bool? editingMode)
  {
    base.Load(project, editingMode);
    if (this.ObjectID != 0L)
      return;
    this.HasNotLoadedSubTasks = this.IsSubTasksExist((IUserSession) null);
    this.LoadSubTasks(project);
  }

  public override bool HasSubTasks => this._Subtasks.Count > 0;

  protected override void CheckInProjectBounds(ref DateTime dt, bool thisIsStart)
  {
  }

  protected internal override EditingMode DefaultEditingMode => EditingMode.None;
}
