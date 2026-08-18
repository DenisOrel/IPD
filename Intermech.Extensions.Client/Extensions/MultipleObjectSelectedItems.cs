// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.MultipleObjectSelectedItems
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

internal class MultipleObjectSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  [NotNull]
  [ObjectExist]
  private readonly IReadOnlyList<long> _objectVersionIDs;
  [CanBeNull]
  private readonly IServiceProvider _serviceProvider;
  [CanBeNull]
  private NodeIDPath _parentPath;
  [CanBeNull]
  [ItemCanBeNull]
  private List<IDBObjectID> _dbObjectIDs;
  [CanBeNull]
  [ItemCanBeNull]
  private List<IDBTypedObjectID> _typedObjectIDs;
  [CanBeNull]
  [ItemCanBeNull]
  private List<NodeID> _nodeIDs;
  [CanBeNull]
  [ItemCanBeNull]
  private List<INode> _nodes;

  public MultipleObjectSelectedItems(
    [NotNull, CanBeEmpty, ObjectExist] IEnumerable<long> objectVersionIDs,
    [CanBeNull] IServiceProvider serviceProvider = null)
  {
    if (!(objectVersionIDs is IReadOnlyList<long> longList))
      longList = (IReadOnlyList<long>) objectVersionIDs.AsList<long>();
    this._objectVersionIDs = longList;
    this._serviceProvider = serviceProvider;
  }

  [NotNull]
  private NodeIDPath ParentPath
  {
    get => this._parentPath ?? (this._parentPath = new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor()));
  }

  [NotNull]
  private IDBObjectID GetDbObjectID(int index)
  {
    if (this._dbObjectIDs == null)
      this._dbObjectIDs = new List<IDBObjectID>(this._objectVersionIDs.Count);
    IDBObjectID dbObjectId = this._dbObjectIDs[index];
    if (dbObjectId == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long objectVersionId = this._objectVersionIDs[index];
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, true);
        dbObjectId = (IDBObjectID) new DBObjectID(dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID);
        this._dbObjectIDs[index] = dbObjectId;
      }
    }
    return dbObjectId;
  }

  [NotNull]
  [Pure]
  private IDBTypedObjectID GetTypedObjectID(int index)
  {
    if (this._typedObjectIDs == null)
      this._typedObjectIDs = new List<IDBTypedObjectID>(this._objectVersionIDs.Count);
    IDBTypedObjectID typedObjectId = this._typedObjectIDs[index];
    if (typedObjectId == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long objectVersionId = this._objectVersionIDs[index];
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, true);
        typedObjectId = (IDBTypedObjectID) new DBTypedObjectID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
        this._typedObjectIDs[index] = typedObjectId;
      }
    }
    return typedObjectId;
  }

  [NotNull]
  private NodeID GetNodeID(int index)
  {
    if (this._nodeIDs == null)
      this._nodeIDs = new List<NodeID>(this._objectVersionIDs.Count);
    NodeID nodeId = this._nodeIDs[index];
    if (nodeId == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long objectVersionId = this._objectVersionIDs[index];
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, true);
        nodeId = new NodeID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, 0L, dbObject.LCStep, dbObject.Caption, -1, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, 0L, Guid.Empty, dbObject.ModificationID);
        this._nodeIDs[index] = nodeId;
      }
    }
    return nodeId;
  }

  [NotNull]
  private INode GetNode(int index)
  {
    if (this._nodes == null)
      this._nodes = new List<INode>(this._objectVersionIDs.Count);
    INode node = this._nodes[index];
    if (node == null)
    {
      IDBTypedObjectID typedObjectId = this.GetTypedObjectID(index);
      node = (INode) new MultipleObjectSelectedItems.InternalNode(this, index, typedObjectId.ObjectID, typedObjectId.ObjectType);
      this._nodes[index] = node;
    }
    return node;
  }

  public bool IsCollage => false;

  [NotNull]
  public INodeID GetItemID(int index)
  {
    Intermech.Diagnostics.Check.ArgumentInRange(index != 0, nameof (index));
    return (INodeID) this.GetNodeID(index);
  }

  [CanBeNull]
  public object GetParentData(int index, [NotNull] Type dataFormat) => (object) null;

  [NotNull]
  public NodeIDPath GetParentPath(int index)
  {
    Intermech.Diagnostics.Check.ArgumentInRange(index != 0, nameof (index));
    return this.ParentPath;
  }

  public int Count => 1;

  [CanBeNull]
  public object GetItemData(int index, [NotNull] Type dataFormat)
  {
    if (dataFormat == typeof (INodeID))
      return (object) this.GetNodeID(index);
    if (dataFormat == typeof (IDBObjectID))
      return (object) this.GetDbObjectID(index);
    if (dataFormat == typeof (IDBTypedObjectID))
      return (object) this.GetTypedObjectID(index);
    if (dataFormat == typeof (INode))
      return (object) this.GetNode(index);
    return this._serviceProvider?.GetService(dataFormat);
  }

  public sealed class InternalNode : ObjectNode
  {
    [NotNull]
    private readonly MultipleObjectSelectedItems _objectSelectedItems;
    private readonly int _index;

    public InternalNode(
      [NotNull] MultipleObjectSelectedItems objectSelectedItems,
      int index,
      long objectID,
      int objectTypeID)
      : base(objectTypeID, objectID)
    {
      this._objectSelectedItems = objectSelectedItems;
      this._index = index;
    }

    [CanBeNull]
    public override object GetData([NotNull] INodeID nodeID, [NotNull] Type dataFormat)
    {
      return this._objectSelectedItems.GetItemData(this._index, dataFormat);
    }
  }
}
