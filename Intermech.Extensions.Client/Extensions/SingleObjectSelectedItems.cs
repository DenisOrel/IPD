// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SingleObjectSelectedItems
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

#nullable disable
namespace Intermech.Extensions;

internal class SingleObjectSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  [ObjectExist]
  private readonly long _objectVersionID;
  [CanBeNull]
  private readonly IServiceProvider _serviceProvider;
  [CanBeNull]
  private NodeIDPath _parentPath;
  [CanBeNull]
  private IDBTypedObjectID _typedObjectID;
  [CanBeNull]
  private NodeID _nodeID;
  [CanBeNull]
  private INode _node;

  public SingleObjectSelectedItems([ObjectExist] long objectVersionID, [CanBeNull] IServiceProvider serviceProvider = null)
  {
    Intermech.Check.ArgumentObjectIdNotEmpty(objectVersionID, nameof (objectVersionID));
    this._objectVersionID = objectVersionID;
    this._serviceProvider = serviceProvider;
  }

  [NotNull]
  private NodeIDPath ParentPath
  {
    get => this._parentPath ?? (this._parentPath = new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor()));
  }

  [NotNull]
  private IDBTypedObjectID TypedObjectID
  {
    get
    {
      IDBTypedObjectID typedObjectId = this._typedObjectID;
      if (typedObjectId == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectVersionID, true);
          this._typedObjectID = (IDBTypedObjectID) new DBTypedObjectID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
          typedObjectId = this._typedObjectID;
        }
      }
      return typedObjectId;
    }
  }

  [NotNull]
  private NodeID NodeID
  {
    get
    {
      NodeID nodeId = this._nodeID;
      if (nodeId == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectVersionID, true);
          this._nodeID = new NodeID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, 0L, dbObject.LCStep, dbObject.Caption, -1, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, 0L, Guid.Empty, dbObject.ModificationID);
          nodeId = this._nodeID;
        }
      }
      return nodeId;
    }
  }

  [NotNull]
  private INode Node
  {
    get => this._node ?? (this._node = (INode) new SingleObjectSelectedItems.InternalNode(this));
  }

  public bool IsCollage => false;

  [NotNull]
  public INodeID GetItemID(int index)
  {
    Intermech.Diagnostics.Check.ArgumentInRange(index != 0, nameof (index));
    return (INodeID) this.NodeID;
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
    Intermech.Diagnostics.Check.ArgumentInRange(index != 0, nameof (index));
    if (dataFormat == typeof (INodeID))
      return (object) this.NodeID;
    if (dataFormat == typeof (IDBObjectID))
      return (object) this.TypedObjectID;
    if (dataFormat == typeof (IDBTypedObjectID))
      return (object) this.TypedObjectID;
    if (dataFormat == typeof (INode))
      return (object) this.Node;
    return this._serviceProvider?.GetService(dataFormat);
  }

  public sealed class InternalNode : ObjectNode
  {
    [NotNull]
    private readonly SingleObjectSelectedItems _objectSelectedItems;

    public InternalNode([NotNull] SingleObjectSelectedItems objectSelectedItems)
      : base(objectSelectedItems.TypedObjectID.ObjectType, objectSelectedItems.TypedObjectID.ObjectID)
    {
      this._objectSelectedItems = objectSelectedItems;
    }

    [CanBeNull]
    public override object GetData([NotNull] INodeID nodeID, [NotNull] Type dataFormat)
    {
      return this._objectSelectedItems.GetItemData(0, dataFormat);
    }
  }
}
