// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.UserSummaryTask
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Controls;

[Serializable]
public class UserSummaryTask : ResourceAssignmentsTask
{
  [NotEmpty]
  public readonly long ObjectID;
  private Schedule _userSchedule;

  public UserSummaryTask([NotEmpty] long objectID, [NotNull] string name, [CanBeNull, ItemNotNull] List<Task> subtasks)
    : base(name, subtasks)
  {
    this.ObjectID = objectID;
    if (this.HasSubTasks)
      return;
    this.Milestone = true;
  }

  public override bool Equals(object obj)
  {
    return obj is UserSummaryTask userSummaryTask && userSummaryTask.ObjectID == this.ObjectID;
  }

  public override int GetHashCode() => this.ObjectID.GetHashCode();

  [CanBeNull]
  public Schedule UserSchedule
  {
    get
    {
      if (this._userSchedule == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._userSchedule = Resource.GetSchedule(sessionKeeper.Session, this.ObjectID) ?? Schedule.Standard;
      }
      return this._userSchedule;
    }
  }
}
