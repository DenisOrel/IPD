// Decompiled with JetBrains decompiler
// Type: Intermech.Project.AssignmentCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class AssignmentCollection : EnhCollection<Assignment>, ISerializable
{
  /// <summary>из RemoveNonChiefItems срабатывает OnChange и опять приходит в RemoveNonChiefItems</summary>
  private bool _inRemoveNonChiefItems;

  [CanBeNull]
  public Task Task { get; }

  public AssignmentCollection([CanBeNull] Task task) => this.Task = task;

  protected AssignmentCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this((Task) null)
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<Assignment>) info.GetValue<Assignment[]>("Items"));
    this.Task = info.GetValue<Task>(nameof (Task));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    int length = str.IndexOf(',');
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    info.AddValue("Items", (object) this.ToArray<Assignment>());
    info.AddValue("Task", (object) this.Task);
  }

  [CanBeNull]
  public Assignment FindByResourceObjectID(long objectID, bool? isChief)
  {
    return this.FirstOrDefault<Assignment>((System.Func<Assignment, bool>) (a =>
    {
      if (a.Resource == null || a.Resource.ObjectID != objectID)
        return false;
      if (!isChief.HasValue)
        return true;
      int num1 = a.IsChief ? 1 : 0;
      bool? nullable = isChief;
      int num2 = nullable.GetValueOrDefault() ? 1 : 0;
      return num1 == num2 & nullable.HasValue;
    }));
  }

  public bool ContainsID(long objectID)
  {
    return this.FindByResourceObjectID(objectID, new bool?(false)) != null;
  }

  /// <summary>Содержит результат изменений, сделанных в последнем сохранении (руководители не учитываются)</summary>
  [NotNull]
  internal IDDelta Delta { get; } = new IDDelta();

  internal void Save([NotNull] IUserSession session)
  {
    this.Delta.Clear();
    foreach (Assignment deletedItem in this._DeletedItems)
    {
      deletedItem.DeleteRelation(deletedItem.RelationID, session);
      if (!deletedItem.IsChief)
        this.Delta.Add(deletedItem.ResourceObjectID, false);
    }
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this)
    {
      assignment.Save(session, this.Task.ObjectID);
      if (!assignment.IsChief && assignment.JustCreated)
        this.Delta.Add(assignment.ResourceObjectID, true);
    }
  }

  internal void Commit()
  {
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this)
      assignment._PrevRelationID = 0L;
    this._Modified = false;
  }

  internal void Rollback()
  {
    foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this)
    {
      if (assignment.JustCreated)
        assignment.HackRelationID = 0L;
    }
  }

  public void Load([NotNull] IUserSession session, [NotEmpty] long objectID)
  {
    BulkData bulkData = (BulkData) null;
    DataRow[] dataRowArray = (DataRow[]) null;
    if (this.Task._UseBulkData)
    {
      if (this.Task is Intermech.Project.Project task && task._BulkData != null)
        bulkData = task._BulkData;
      if (bulkData == null && this.Task?.Project?._BulkData != null)
        bulkData = this.Task.Project._BulkData;
    }
    if (bulkData != null)
    {
      DataTable assignments = bulkData.Assignments;
      if (assignments != null)
      {
        string columnName = assignments.Columns[0].ColumnName;
        dataRowArray = assignments.Select($"[{columnName}] = {(object) objectID}");
      }
    }
    else
      dataRowArray = session.GetRelationCollection((int) (IpsMetadataEntityBase<int>) RelationTypes.Resources).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[8]
      {
        (object) -21,
        (object) -2,
        (object) -20,
        (object) -50,
        (object) -7,
        (object) Attributes.ResourceUnits.ID,
        (object) Attributes.ResourceIsChief.ID,
        (object) Intermech.Metadata.Attributes.Calendar.ID
      }, 0L, (object) null, -1), objectID).Select();
    this.Clear();
    if (dataRowArray != null)
    {
      foreach (DataRow row in dataRowArray)
      {
        Assignment assignment = new Assignment(this.Task);
        assignment.Load(row);
        this.Add(assignment);
      }
    }
    this._Modified = false;
  }

  public void Load()
  {
    if (this.Task == null)
      return;
    IUserSession session = this.Task.GetSession();
    try
    {
      this.Load(session, this.Task.ObjectID);
    }
    finally
    {
      this.Task.ReleaseSession();
    }
  }

  [NotNull]
  public List<long> UserIDs
  {
    get
    {
      return this.Where<Assignment>((System.Func<Assignment, bool>) (a => a.IsUser && !a.IsChief)).Select<Assignment, long>((System.Func<Assignment, long>) (a => a.Resource.ObjectID)).ToList<long>(this.Count);
    }
  }

  [NotNull]
  [ItemNotNull]
  public List<string> UserNames
  {
    get
    {
      return this.Where<Assignment>((System.Func<Assignment, bool>) (a => a.IsUser && !a.IsChief)).Select<Assignment, string>((System.Func<Assignment, string>) (a => a.Resource.Name)).ToList<string>(this.Count);
    }
  }

  [NotNull]
  public string UserNamesString
  {
    [DebuggerStepThrough] get => string.Join(", ", this.UserNames.ToArray());
  }

  public void RemoveNonChiefItems()
  {
    if (this._inRemoveNonChiefItems)
      return;
    this._inRemoveNonChiefItems = true;
    try
    {
      for (int index = this.Count - 1; index >= 0; --index)
      {
        if (!this[index].IsChief)
          this.RemoveAt(index);
      }
    }
    finally
    {
      this._inRemoveNonChiefItems = false;
    }
  }

  public int WorkResourceCount
  {
    get => this.Count<Assignment>((System.Func<Assignment, bool>) (a => a.MaxUnits > 0.0));
  }

  [CanBeNull]
  public Assignment AddUser(long userID, bool isChief = false)
  {
    Assignment assignment = (Assignment) null;
    if (this.Task != null)
    {
      IUserSession session = this.Task.GetSession();
      string name;
      try
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(userID);
        if (objectInfo.Empty)
          throw new ObjectNotFoundException(userID);
        name = objectInfo.Caption ?? "?";
      }
      finally
      {
        this.Task.ReleaseSession();
      }
      assignment = new Assignment(new Resource((ISessionProvider) this.Task, userID, name, (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User));
      assignment.IsChief = isChief;
      this.Add(assignment);
    }
    return assignment;
  }
}
