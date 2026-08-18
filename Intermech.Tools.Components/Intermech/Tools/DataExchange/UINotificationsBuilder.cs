// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.UINotificationsBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class UINotificationsBuilder
{
  private static readonly BooleanSwitch traceErrors = new BooleanSwitch("UINotificationBuilder.TraceErrors", "", "0");
  private Dictionary<long, UINotificationsBuilder.DBObjectEntry> objectsTable;
  private Dictionary<long, UINotificationsBuilder.DBRelationEntry> relationsTable;

  public UINotificationsBuilder()
  {
    this.objectsTable = new Dictionary<long, UINotificationsBuilder.DBObjectEntry>();
    this.relationsTable = new Dictionary<long, UINotificationsBuilder.DBRelationEntry>();
  }

  public List<NotificationEventArgs> ToNotificationList()
  {
    List<NotificationEventArgs> list = new List<NotificationEventArgs>(8);
    this.TryAdd(list, (NotificationEventArgs) this.TryGetObjectsCreatedEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetObjectsChangedEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetObjectsRemovedEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetObjectsCheckedOutEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetObjectsCheckedInEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetRelationsCreatedEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetRelationsChangedEvent());
    this.TryAdd(list, (NotificationEventArgs) this.TryGetRelationsRemovedEvent());
    this.Clear();
    return list;
  }

  private void TryAdd(List<NotificationEventArgs> list, NotificationEventArgs eventArgs)
  {
    if (eventArgs == null)
      return;
    list.Add(eventArgs);
  }

  private DBObjectsEventArgs TryGetObjectsCreatedEvent()
  {
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBObjectEntry> keyValuePair in this.objectsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBObjectState.Created)
        objectIDs.Add(keyValuePair.Value.ObjectId);
    }
    return objectIDs.Count != 0 ? (DBObjectsEventArgs) new CreatedExternallyEventArgs("ObjectsCreated", (IList<long>) objectIDs) : (DBObjectsEventArgs) null;
  }

  private DBObjectsEventArgs TryGetObjectsChangedEvent()
  {
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBObjectEntry> keyValuePair in this.objectsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBObjectState.Modified)
        objectIDs.Add(keyValuePair.Value.ObjectId);
    }
    return objectIDs.Count != 0 ? new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs) : (DBObjectsEventArgs) null;
  }

  private DBObjectsEventArgs TryGetObjectsRemovedEvent()
  {
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBObjectEntry> keyValuePair in this.objectsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBObjectState.Removed)
        objectIDs.Add(keyValuePair.Value.ObjectId);
    }
    return objectIDs.Count != 0 ? new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs) : (DBObjectsEventArgs) null;
  }

  private DBObjectsEventArgs TryGetObjectsCheckedOutEvent()
  {
    List<long> objectIDs = new List<long>();
    List<long> newObjectIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBObjectEntry> keyValuePair in this.objectsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBObjectState.CheckedOut)
      {
        newObjectIDs.Add(keyValuePair.Value.ObjectId);
        objectIDs.Add(-keyValuePair.Value.ObjectId);
      }
    }
    return newObjectIDs.Count != 0 ? (DBObjectsEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs) : (DBObjectsEventArgs) null;
  }

  private DBObjectsEventArgs TryGetObjectsCheckedInEvent()
  {
    List<long> objectIDs = new List<long>();
    List<long> longList = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBObjectEntry> keyValuePair in this.objectsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBObjectState.CheckedIn)
      {
        longList.Add(keyValuePair.Value.ObjectId);
        objectIDs.Add(-keyValuePair.Value.ObjectId);
      }
    }
    return longList.Count != 0 ? new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs) : (DBObjectsEventArgs) null;
  }

  private DBRelationsEventArgs TryGetRelationsCreatedEvent()
  {
    List<long> relationIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> projIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBRelationEntry> keyValuePair in this.relationsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBRelationState.Created)
      {
        relationIDs.Add(keyValuePair.Value.RelationId);
        relTypeIDs.Add(keyValuePair.Value.RelationType);
        projIDs.Add(keyValuePair.Value.ProjectId);
      }
    }
    return relationIDs.Count != 0 ? new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs, NavigatorRelationCommand.Unknown) : (DBRelationsEventArgs) null;
  }

  private DBRelationsEventArgs TryGetRelationsChangedEvent()
  {
    List<long> relationIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> projIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBRelationEntry> keyValuePair in this.relationsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBRelationState.Modified)
      {
        relationIDs.Add(keyValuePair.Key);
        relTypeIDs.Add(keyValuePair.Value.RelationType);
        projIDs.Add(keyValuePair.Value.ProjectId);
      }
    }
    return relationIDs.Count != 0 ? new DBRelationsEventArgs("RelationsChanged", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs, NavigatorRelationCommand.Unknown) : (DBRelationsEventArgs) null;
  }

  private DBRelationsEventArgs TryGetRelationsRemovedEvent()
  {
    List<long> relationIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> projIDs = new List<long>();
    foreach (KeyValuePair<long, UINotificationsBuilder.DBRelationEntry> keyValuePair in this.relationsTable)
    {
      if (keyValuePair.Value.State == UINotificationsBuilder.DBRelationState.Removed)
      {
        relationIDs.Add(keyValuePair.Key);
        relTypeIDs.Add(keyValuePair.Value.RelationType);
        projIDs.Add(keyValuePair.Value.ProjectId);
      }
    }
    return relationIDs.Count != 0 ? new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs, NavigatorRelationCommand.Unknown) : (DBRelationsEventArgs) null;
  }

  public void Clear()
  {
    this.objectsTable.Clear();
    this.relationsTable.Clear();
  }

  public void AddCreatedObject(IDBObjectRef objectRef)
  {
    long num = objectRef != null ? objectRef.GetObjectId() : throw new ArgumentNullException(nameof (objectRef));
    UINotificationsBuilder.DBObjectEntry objectEntry;
    if (this.objectsTable.TryGetValue(num, out objectEntry))
      this.TraceObjectStateError(objectEntry, UINotificationsBuilder.DBObjectState.Created);
    this.objectsTable[num] = new UINotificationsBuilder.DBObjectEntry(UINotificationsBuilder.DBObjectState.Created, num);
  }

  public void AddModifiedObject(IDBObjectRef objectRef)
  {
    long num = objectRef != null ? objectRef.GetObjectId() : throw new ArgumentNullException(nameof (objectRef));
    UINotificationsBuilder.DBObjectEntry objectEntry;
    if (this.objectsTable.TryGetValue(num, out objectEntry))
    {
      if (objectEntry.State != UINotificationsBuilder.DBObjectState.Removed)
        return;
      this.TraceObjectStateError(objectEntry, UINotificationsBuilder.DBObjectState.Modified);
      objectEntry.State = UINotificationsBuilder.DBObjectState.Modified;
    }
    else
      this.objectsTable[num] = new UINotificationsBuilder.DBObjectEntry(UINotificationsBuilder.DBObjectState.Modified, num);
  }

  public void AddRemovedObject(IDBObjectRef objectRef)
  {
    long num = objectRef != null ? objectRef.GetObjectId() : throw new ArgumentNullException(nameof (objectRef));
    UINotificationsBuilder.DBObjectEntry objectEntry;
    if (this.objectsTable.TryGetValue(num, out objectEntry))
    {
      if (objectEntry.State == UINotificationsBuilder.DBObjectState.Removed)
        this.TraceObjectStateError(objectEntry, UINotificationsBuilder.DBObjectState.Removed);
      objectEntry.State = UINotificationsBuilder.DBObjectState.Removed;
    }
    else
      this.objectsTable[num] = new UINotificationsBuilder.DBObjectEntry(UINotificationsBuilder.DBObjectState.Removed, num);
  }

  public void AddCheckedOutObject(IDBObjectRef objectRef)
  {
    long num = objectRef != null ? objectRef.GetObjectId() : throw new ArgumentNullException(nameof (objectRef));
    if (num >= 0L)
      return;
    long key = -num;
    UINotificationsBuilder.DBObjectEntry objectEntry;
    if (this.objectsTable.TryGetValue(key, out objectEntry))
    {
      if (objectEntry.State == UINotificationsBuilder.DBObjectState.Removed || objectEntry.State == UINotificationsBuilder.DBObjectState.CheckedOut)
        this.TraceObjectStateError(objectEntry, UINotificationsBuilder.DBObjectState.CheckedOut);
      this.objectsTable.Remove(key);
      objectEntry.ObjectId = num;
      if (objectEntry.State != UINotificationsBuilder.DBObjectState.Created)
        objectEntry.State = UINotificationsBuilder.DBObjectState.CheckedOut;
      this.objectsTable[num] = objectEntry;
    }
    else
      this.objectsTable[num] = new UINotificationsBuilder.DBObjectEntry(UINotificationsBuilder.DBObjectState.CheckedOut, num);
  }

  public void AddCheckedInObject(IDBObjectRef objectRef)
  {
    long num = objectRef != null ? objectRef.GetObjectId() : throw new ArgumentNullException(nameof (objectRef));
    if (num < 0L)
      return;
    long key = -num;
    UINotificationsBuilder.DBObjectEntry objectEntry;
    if (this.objectsTable.TryGetValue(key, out objectEntry))
    {
      if (objectEntry.State == UINotificationsBuilder.DBObjectState.Removed || objectEntry.State == UINotificationsBuilder.DBObjectState.CheckedIn)
        this.TraceObjectStateError(objectEntry, UINotificationsBuilder.DBObjectState.CheckedIn);
      this.objectsTable.Remove(key);
      objectEntry.ObjectId = num;
      if (objectEntry.State != UINotificationsBuilder.DBObjectState.Created)
        objectEntry.State = UINotificationsBuilder.DBObjectState.CheckedIn;
      this.objectsTable[num] = objectEntry;
    }
    else
      this.objectsTable[num] = new UINotificationsBuilder.DBObjectEntry(UINotificationsBuilder.DBObjectState.CheckedIn, num);
  }

  public void AddCreatedRelation(IDBRelationRef relationRef)
  {
    long num = relationRef != null ? relationRef.GetRelationId() : throw new ArgumentNullException(nameof (relationRef));
    UINotificationsBuilder.DBRelationEntry relationEntry;
    if (this.relationsTable.TryGetValue(num, out relationEntry))
      this.TraceRelationStateError(relationEntry, UINotificationsBuilder.DBRelationState.Created);
    int relationType = relationRef.GetRelationType();
    long projectId = relationRef.GetProjectId();
    this.relationsTable[num] = new UINotificationsBuilder.DBRelationEntry(UINotificationsBuilder.DBRelationState.Created, num, relationType, projectId);
  }

  public void AddModifiedRelation(IDBRelationRef relationRef)
  {
    long num = relationRef != null ? relationRef.GetRelationId() : throw new ArgumentNullException(nameof (relationRef));
    UINotificationsBuilder.DBRelationEntry relationEntry;
    if (this.relationsTable.TryGetValue(num, out relationEntry))
    {
      if (relationEntry.State != UINotificationsBuilder.DBRelationState.Removed)
        return;
      this.TraceRelationStateError(relationEntry, UINotificationsBuilder.DBRelationState.Modified);
      relationEntry.State = UINotificationsBuilder.DBRelationState.Modified;
    }
    else
    {
      int relationType = relationRef.GetRelationType();
      long projectId = relationRef.GetProjectId();
      this.relationsTable[num] = new UINotificationsBuilder.DBRelationEntry(UINotificationsBuilder.DBRelationState.Modified, num, relationType, projectId);
    }
  }

  public void AddRemovedRelation(IDBRelationRef relationRef)
  {
    long num = relationRef != null ? relationRef.GetRelationId() : throw new ArgumentNullException(nameof (relationRef));
    UINotificationsBuilder.DBRelationEntry relationEntry;
    if (this.relationsTable.TryGetValue(num, out relationEntry))
    {
      if (relationEntry.State == UINotificationsBuilder.DBRelationState.Removed)
        this.TraceRelationStateError(relationEntry, UINotificationsBuilder.DBRelationState.Removed);
      relationEntry.State = UINotificationsBuilder.DBRelationState.Removed;
    }
    else
    {
      int relationType = relationRef.GetRelationType();
      long projectId = relationRef.GetProjectId();
      this.relationsTable[num] = new UINotificationsBuilder.DBRelationEntry(UINotificationsBuilder.DBRelationState.Removed, num, relationType, projectId);
    }
  }

  private void TraceObjectStateError(
    UINotificationsBuilder.DBObjectEntry objectEntry,
    UINotificationsBuilder.DBObjectState newState)
  {
    if (!UINotificationsBuilder.traceErrors.Enabled)
      return;
    Trace.WriteLine($"{this.GetType().Name}: invalid state change {objectEntry.State}->{newState} detected for object #{objectEntry.ObjectId}");
  }

  private void TraceRelationStateError(
    UINotificationsBuilder.DBRelationEntry relationEntry,
    UINotificationsBuilder.DBRelationState newState)
  {
    if (!UINotificationsBuilder.traceErrors.Enabled)
      return;
    Trace.WriteLine($"{this.GetType().Name}: invalid state change {relationEntry.State}->{newState} detected for relation #{relationEntry.RelationId}");
  }

  private sealed class DBObjectEntry
  {
    public DBObjectEntry(UINotificationsBuilder.DBObjectState state, long objectId)
    {
      this.State = state;
      this.ObjectId = objectId;
    }

    public UINotificationsBuilder.DBObjectState State { get; set; }

    public long ObjectId { get; set; }
  }

  private enum DBObjectState
  {
    Created,
    Modified,
    Removed,
    CheckedOut,
    CheckedIn,
  }

  private sealed class DBRelationEntry
  {
    public DBRelationEntry(
      UINotificationsBuilder.DBRelationState state,
      long relationId,
      int relationType,
      long projectId)
    {
      this.State = state;
      this.RelationId = relationId;
      this.RelationType = relationType;
      this.ProjectId = projectId;
    }

    public UINotificationsBuilder.DBRelationState State { get; set; }

    public long RelationId { get; private set; }

    public int RelationType { get; private set; }

    public long ProjectId { get; private set; }
  }

  private enum DBRelationState
  {
    Created,
    Modified,
    Removed,
  }
}
