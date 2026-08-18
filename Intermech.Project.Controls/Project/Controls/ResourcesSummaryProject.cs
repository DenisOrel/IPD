// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourcesSummaryProject
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project.Controls;

public class ResourcesSummaryProject : ResourceAssignmentsProject
{
  private bool _loaded;

  public ResourcesSummaryProject([NotNull] List<long> userIDs)
    : base(userIDs)
  {
    this._GroupByProject = false;
  }

  [CanBeNull]
  public ResourceAssignmentsProject InnerProject { get; }

  internal void Assign([NotNull] ResourceAssignmentsProject source)
  {
    this._ObjectIDs = source.ObjectIDs;
    foreach (UserSummaryTask userSummaryTask1 in source.SubTasks.OfType<UserSummaryTask>())
    {
      UserSummaryTask userSummaryTask2 = new UserSummaryTask(userSummaryTask1.ObjectID, userSummaryTask1.Name, (List<Task>) null);
      foreach (Task task in userSummaryTask1.AllSubTasks.Where<Task>((Func<Task, bool>) (sub => !(sub is ResourceAssignmentsSubProject))))
        userSummaryTask2.ResourceAssignmentsTasks.Add(task);
      this._ResourceAssignmentsTasks.Add((Task) userSummaryTask2);
    }
    this.UserInfos = new List<ResourceAssignmentsProject.UserInfo>();
    foreach (ResourceAssignmentsProject.UserInfo userInfo1 in source.UserInfos)
    {
      ResourceAssignmentsProject.UserInfo ui = userInfo1;
      ResourceAssignmentsProject.UserInfo userInfo2 = new ResourceAssignmentsProject.UserInfo(ui.ObjectID, ui.ID, ui.Name);
      foreach (UserSummaryTask userSummaryTask in this._ResourceAssignmentsTasks.OfType<UserSummaryTask>().Where<UserSummaryTask>((Func<UserSummaryTask, bool>) (ut => ui.ObjectID == ut.ObjectID)))
        userInfo2.Task = userSummaryTask;
      this.UserInfos.Add(userInfo2);
    }
    this._loaded = true;
    this.Sort();
    this.Start = source.Start;
    this.Finish = source.Finish;
  }

  public ResourcesSummaryProject([NotNull] ResourceAssignmentsProject source)
    : base(source.ObjectIDs)
  {
    this.InnerProject = source;
    source.SummaryProject = this;
  }

  public override void Load()
  {
    if (this._loaded)
      return;
    base.Load();
    this.Sort();
  }

  private static int CompareTasksByStart([NotNull] Task t1, [NotNull] Task t2)
  {
    DateTime start = t1.Start;
    long ticks1 = start.Ticks;
    start = t2.Start;
    long ticks2 = start.Ticks;
    long num = ticks1 - ticks2;
    if (num < 0L)
      return -1;
    return num <= 0L ? 0 : 1;
  }

  private void Sort()
  {
    foreach (ResourceAssignmentsTask resourceAssignmentsTask in this._ResourceAssignmentsTasks.OfType<ResourceAssignmentsTask>())
      resourceAssignmentsTask.ResourceAssignmentsTasks.Sort(new Comparison<Task>(ResourcesSummaryProject.CompareTasksByStart));
  }

  public override DateTime Finish
  {
    get => this._Finish;
    set => this._Finish = value;
  }
}
