// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityNode
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow;

public class ActivityNode
{
  public List<ActivityStatus> Statuses = new List<ActivityStatus>();
  public List<long> ObjectIDs = new List<long>();
  internal bool Checked;
  public readonly string Name = "";
  public readonly int ObjectType;
  public readonly long ParentActivityID;
  public List<ActivityLink> Next = new List<ActivityLink>();
  public List<ActivityLink> Prev = new List<ActivityLink>();
  public readonly WorkflowGraph Owner;
  public string ObjectGuid = Guid.Empty.ToString();
  protected internal ActivityGraphData _graphData;
  private long _processID;
  public bool IsParallelBlockFinish;

  public ActivityNode(
    WorkflowGraph owner,
    long objectID,
    string name,
    ActivityStatus status,
    int objectType,
    long parentActivityID,
    string objectGuid)
  {
    this.Owner = owner;
    this.ObjectIDs.Add(objectID);
    this.Name = name;
    this.Statuses.Add(status);
    this.ObjectType = objectType;
    this.ParentActivityID = parentActivityID;
    this.ObjectGuid = objectGuid;
  }

  public void AddLink(ActivityLink l, ActivityNode toact)
  {
    this.Next.Add(l);
    toact.Prev.Add(l);
  }

  public bool Completed
  {
    get
    {
      foreach (long objectId in this.ObjectIDs)
      {
        if (this.Owner.PreExecuted.Contains(objectId))
          return false;
      }
      foreach (ActivityStatus statuse in this.Statuses)
      {
        switch (statuse)
        {
          case ActivityStatus.OnApproach:
          case ActivityStatus.Terminated:
          case ActivityStatus.Completed:
          case ActivityStatus.Recalled:
            continue;
          default:
            return false;
        }
      }
      return true;
    }
  }

  public bool CompletedWithoutApproach
  {
    get
    {
      foreach (long objectId in this.ObjectIDs)
      {
        if (this.Owner.PreExecuted.Contains(objectId))
          return false;
      }
      foreach (ActivityStatus statuse in this.Statuses)
      {
        switch (statuse)
        {
          case ActivityStatus.Terminated:
          case ActivityStatus.Completed:
          case ActivityStatus.Recalled:
            continue;
          default:
            return false;
        }
      }
      return true;
    }
  }

  /// <summary>
  /// Первый идентификатор из списка ObjectIDs, идентификатор главного действия.
  /// </summary>
  public long ObjectID => this.ObjectIDs.Count <= 0 ? 0L : this.ObjectIDs[0];

  public List<long> CloneIDs
  {
    get
    {
      List<long> cloneIds = new List<long>((IEnumerable<long>) this.ObjectIDs);
      if (cloneIds.Count > 0)
        cloneIds.RemoveAt(0);
      if (cloneIds.Count == 0)
        cloneIds = (List<long>) null;
      return cloneIds;
    }
  }

  public ActivityGraphData GraphData => this._graphData;

  public long ProcessID
  {
    get => this._processID;
    internal set => this._processID = value;
  }
}
