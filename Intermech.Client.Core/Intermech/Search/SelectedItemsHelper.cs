
// Type: Intermech.Search.SelectedItemsHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Search.ObjectGroups;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search;

public static class SelectedItemsHelper
{
  public static bool TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeIDRelationIDAndRelationTypeID(
    ISelectedItems selectedItems,
    out NodeID nodeID)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeID(selectedItems, out nodeID) && !RelationHelper.IsUnknownRelationID(nodeID.PrjLinkID) && !RelationTypeHelper.IsUnknownRelationTypeID(nodeID.RelationTypeID))
      return true;
    nodeID = (NodeID) null;
    return false;
  }

  public static bool TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeID(
    ISelectedItems selectedItems,
    out NodeID nodeID)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    if (selectedItems.Count == 1)
    {
      nodeID = selectedItems.GetItemID(0) as NodeID;
      if (nodeID != null && !(nodeID is SelectionNodeID) && !ObjectHelper.IsUnknownObjectVersionID(nodeID.ObjectID) && !ObjectTypeHelper.IsUnknownObjectTypeID(nodeID.ObjectTypeID))
        return true;
    }
    nodeID = (NodeID) null;
    return false;
  }

  public static bool TryGetObjectNodeIdsWithObjectVersionIDAndObjectTypeID(
    ISelectedItems selectedItems,
    out NodeID[] nodeIds)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    List<NodeID> nodeIdList = new List<NodeID>();
    int index = 0;
    for (int count = selectedItems.Count; index < count; ++index)
    {
      if (!(selectedItems.GetItemID(index) is NodeID itemId) || itemId is SelectionNodeID || ObjectHelper.IsUnknownObjectVersionID(itemId.ObjectID) || ObjectTypeHelper.IsUnknownObjectTypeID(itemId.ObjectTypeID))
      {
        nodeIds = (NodeID[]) null;
        return false;
      }
      nodeIdList.Add(itemId);
    }
    nodeIds = nodeIdList.ToArray();
    return true;
  }

  public static bool TryGetSingleNavigatorTreeNode(
    ISelectedItems selectedItems,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    if (selectedItems.Count == 1)
    {
      navigatorTreeNode = selectedItems.GetItemData(0, typeof (NavigatorTreeNode)) as NavigatorTreeNode;
      return navigatorTreeNode != null;
    }
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  public static bool TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(
    ISelectedItems selectedItems,
    out IDBTypedObjectID typedObjectID)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    if (selectedItems.Count != 1)
    {
      typedObjectID = (IDBTypedObjectID) null;
      return false;
    }
    typedObjectID = selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (typedObjectID != null && !ObjectHelper.IsUnknownObjectVersionID(typedObjectID.ObjectID) && !ObjectTypeHelper.IsUnknownObjectTypeID(typedObjectID.ObjectType))
      return true;
    typedObjectID = (IDBTypedObjectID) null;
    return false;
  }

  public static bool TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(
    ISelectedItems selectedItems,
    out IDBTypedObjectID[] typedObjectIds)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
    int index = 0;
    for (int count = selectedItems.Count; index < count; ++index)
    {
      if (!(selectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || ObjectHelper.IsUnknownObjectVersionID(itemData.ObjectID) || ObjectTypeHelper.IsUnknownObjectTypeID(itemData.ObjectType))
      {
        typedObjectIds = (IDBTypedObjectID[]) null;
        return false;
      }
      dbTypedObjectIdList.Add(itemData);
    }
    typedObjectIds = dbTypedObjectIdList.ToArray();
    return true;
  }

  public static bool TryGetSingleRelationIDWithRelationIDAndRelationTypeID(
    ISelectedItems selectedItems,
    out IDBRelationID relationID)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    relationID = (IDBRelationID) null;
    if (selectedItems.Count != 1)
      return false;
    relationID = selectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    return relationID != null && !RelationHelper.IsUnknownRelationID(relationID.Value) && !RelationTypeHelper.IsUnknownRelationTypeID(relationID.RelationType);
  }

  public static bool TryGetRelationIdsWithRelationIdsAndRelationTypeIds(
    ISelectedItems selectedItems,
    out IDBRelationID[] relationIds)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    List<IDBRelationID> dbRelationIdList = new List<IDBRelationID>();
    int index = 0;
    for (int count = selectedItems.Count; index < count; ++index)
    {
      if (!(selectedItems.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData) || RelationHelper.IsUnknownRelationID(itemData.Value) || RelationTypeHelper.IsUnknownRelationTypeID(itemData.RelationType))
      {
        relationIds = new IDBRelationID[0];
        return false;
      }
      dbRelationIdList.Add(itemData);
    }
    relationIds = dbRelationIdList.ToArray();
    return true;
  }

  public static bool TryGetRelationIdsWithRelationIdsAndRelationTypeIdsAndCommonNotUnknownProjectID(
    ISelectedItems selectedItems,
    out IDBRelationID[] relationIds)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    relationIds = (IDBRelationID[]) null;
    return SelectedItemsHelper.TryGetRelationIdsWithRelationIdsAndRelationTypeIds(selectedItems, out relationIds) && ((IEnumerable<IDBRelationID>) relationIds).GroupBy<IDBRelationID, long>((Func<IDBRelationID, long>) (o => o.ProjID)).Count<IGrouping<long, IDBRelationID>>() <= 1 && !ObjectHelper.IsAnyUnknownObjectVersionID(((IEnumerable<IDBRelationID>) relationIds).Select<IDBRelationID, long>((Func<IDBRelationID, long>) (o => o.ProjID)));
  }

  public static ISelectedItems CreateSelectedItemsForCompositionPart(
    long relationID,
    long objectVersionID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    return !ObjectHelper.IsUnknownObjectID(objectVersionID) ? (ISelectedItems) new SelectedItemsHelper.CompositionPartSelectedItems(relationID, objectVersionID) : throw new ArgumentException();
  }

  public static ISelectedItems CreateSelectedItemsForObject(long objectVersionID)
  {
    return !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? (ISelectedItems) new SelectedItemsHelper.ObjectSelectedItems(objectVersionID) : throw new ArgumentException();
  }

  public static ISelectedItems CreateSelectedItemsForObjects(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException(nameof (objectVersionIds));
    return !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds) ? (ISelectedItems) new SelectedItemsHelper.ObjectsSelectedItems(objectVersionIds) : throw new ArgumentException();
  }

  public static int GetProjectTypeID(ISelectedItems selectedItems)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    int projectTypeId = -1;
    if (selectedItems.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData)
    {
      projectTypeId = parentData.ObjectType;
    }
    else
    {
      NodeIDPath parentPath = selectedItems.GetParentPath(0);
      if (parentPath.LastID is ObjectGroupNodeID && parentPath.Length > 1 && parentPath[parentPath.Length - 2] is NodeID)
        projectTypeId = ((NodeID) parentPath[parentPath.Length - 2]).ObjectTypeID;
    }
    return projectTypeId;
  }

  /// <summary>Метод получения выделенных в Навигаторе объектов</summary>
  public static ISelectedItems GetNavigatorSelection()
  {
    ICurrentNavWindow service = ServiceUtils.GetService<ICurrentNavWindow>((object) ServicesManager.ServiceContainer, false);
    if (service != null)
    {
      ISelectedItemsHost selectedItemsHost1 = (ISelectedItemsHost) null;
      if (service.TreeView is NavigatorTreeView treeView && treeView.TreeFocused)
        selectedItemsHost1 = service.TreeView as ISelectedItemsHost;
      else if (service.ViewsManagers is IViewsManager viewsManagers && viewsManagers.ActiveViewPage != null)
      {
        if (!(viewsManagers.ActiveViewPage.Control is ISelectedItemsHost selectedItemsHost2))
          selectedItemsHost2 = (ISelectedItemsHost) treeView;
        selectedItemsHost1 = selectedItemsHost2;
      }
      if (selectedItemsHost1 != null)
        return selectedItemsHost1.SelectedItems;
    }
    return (ISelectedItems) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private sealed class CompositionPartSelectedItems : ISelectedItems, ISimpleSelectedItems
  {
    private NodeID _nodeID;
    private IDBRelationID _dbRelationID;
    private IDBTypedObjectID _typedObjectID;
    private IDBTypedObjectID _typedParentObjId;
    private INode _node;
    private NodeIDPath _parentNodeIDPath;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="relationId">Ид-р связи</param>
    /// <param name="objectVersionId">Ид-о версии дочернего объекта</param>
    public CompositionPartSelectedItems(long relationId, long objectVersionId)
    {
      if (RelationHelper.IsUnknownRelationID(relationId))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectID(objectVersionId))
        throw new ArgumentException();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(relationId);
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
        this._nodeID = new NodeID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, relation.RelationID, dbObject.LCStep, dbObject.Caption, relation.TypeID, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, relation.ProjID, relation.GUID, dbObject.ModificationID);
        this._dbRelationID = (IDBRelationID) new DBRelationID(relation.RelationID, relation.PartID, relation.TypeID, 0L, relation.GUID, relation.ProjID);
        this._typedObjectID = (IDBTypedObjectID) new DBTypedObjectID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
        this._node = (INode) new SelectedItemsHelper.CompositionPartNode(this._nodeID, this._dbRelationID, this._typedObjectID);
        this._parentNodeIDPath = new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(relation.ProjID));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsCollage => false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public INodeID GetItemID(int index)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      return (INodeID) this._nodeID;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="dataFormat"></param>
    /// <returns></returns>
    public object GetParentData(int index, Type dataFormat)
    {
      if (!(dataFormat == typeof (IDBTypedObjectID)) && !(dataFormat == typeof (IDBObjectID)))
        return (object) null;
      if (this._dbRelationID != null && this._typedParentObjId == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._dbRelationID.ProjID);
          this._typedParentObjId = (IDBTypedObjectID) new DBTypedObjectID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
        }
      }
      return (object) this._typedParentObjId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public NodeIDPath GetParentPath(int index) => this._parentNodeIDPath;

    /// <summary>
    /// 
    /// </summary>
    public int Count => 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="dataFormat"></param>
    /// <returns></returns>
    public object GetItemData(int index, Type dataFormat)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      if (dataFormat == typeof (INodeID))
        return (object) this._nodeID;
      if (dataFormat == typeof (IDBRelationID))
        return (object) this._dbRelationID;
      if (dataFormat == typeof (IDBTypedObjectID) || dataFormat == typeof (IDBObjectID))
        return (object) this._typedObjectID;
      return dataFormat == typeof (INode) ? (object) this._node : (object) null;
    }
  }

  private sealed class CompositionPartNode : ObjectNode
  {
    private NodeID _nodeID;
    private IDBRelationID _relationID;
    private IDBTypedObjectID _typedObjectID;

    public CompositionPartNode(
      NodeID nodeID,
      IDBRelationID relationID,
      IDBTypedObjectID typedObjectID)
      : base(nodeID.ObjectTypeID, nodeID.ObjectID)
    {
      if (nodeID == null)
        throw new ArgumentNullException(nameof (nodeID));
      if (relationID == null)
        throw new ArgumentNullException(nameof (relationID));
      if (typedObjectID == null)
        throw new ArgumentNullException(nameof (typedObjectID));
      this._nodeID = nodeID;
      this._relationID = relationID;
      this._typedObjectID = typedObjectID;
    }

    public override object GetData(INodeID nodeID, Type dataFormat)
    {
      if (this._nodeID == nodeID)
      {
        if (dataFormat == typeof (INodeID))
          return (object) this._nodeID;
        if (dataFormat == typeof (IDBRelationID))
          return (object) this._relationID;
        if (dataFormat == typeof (IDBTypedObjectID))
          return (object) this._typedObjectID;
      }
      return (object) null;
    }
  }

  private sealed class ObjectSelectedItems : ISelectedItems, ISimpleSelectedItems
  {
    private long _objectVersionID;
    private NodeIDPath _parentPath;
    private NodeID _nodeID;
    private IDBObjectID _dbObjectID;
    private IDBTypedObjectID _typedObjectID;
    private SelectedItemsHelper.ObjectSelectedItems.Node _node;

    public ObjectSelectedItems(long objectVersionID)
    {
      this._objectVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectVersionID);
        this._parentPath = new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor());
        this._nodeID = new NodeID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, 0L, dbObject.LCStep, dbObject.Caption, -1, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, 0L, Guid.Empty, dbObject.ModificationID);
        this._dbObjectID = (IDBObjectID) new DBObjectID(dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID);
        this._typedObjectID = (IDBTypedObjectID) new DBTypedObjectID(dbObject.TypeID, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
        this._node = new SelectedItemsHelper.ObjectSelectedItems.Node(this);
      }
    }

    public bool IsCollage => false;

    public INodeID GetItemID(int index)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      return (INodeID) this._nodeID;
    }

    public object GetParentData(int index, Type dataFormat) => (object) null;

    public NodeIDPath GetParentPath(int index)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      return this._parentPath;
    }

    public int Count => 1;

    public object GetItemData(int index, Type dataFormat)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      if (dataFormat == typeof (INodeID))
        return (object) this._nodeID;
      if (dataFormat == typeof (IDBObjectID))
        return (object) this._dbObjectID;
      if (dataFormat == typeof (IDBTypedObjectID))
        return (object) this._typedObjectID;
      return dataFormat == typeof (INode) ? (object) this._node : (object) null;
    }

    public sealed class Node : ObjectNode
    {
      private SelectedItemsHelper.ObjectSelectedItems _objectSelectedItems;

      public Node(
        SelectedItemsHelper.ObjectSelectedItems objectSelectedItems)
        : base(objectSelectedItems._typedObjectID.ObjectType, objectSelectedItems._typedObjectID.ObjectID)
      {
        this._objectSelectedItems = objectSelectedItems != null ? objectSelectedItems : throw new ArgumentNullException(nameof (objectSelectedItems));
      }

      public override object GetData(INodeID nodeID, Type dataFormat)
      {
        return this._objectSelectedItems.GetItemData(0, dataFormat);
      }
    }
  }

  private sealed class ObjectsSelectedItems : ISelectedItems, ISimpleSelectedItems
  {
    private SelectedItemsHelper.ObjectSelectedItems[] _objectSelectedItems;

    public ObjectsSelectedItems(long[] objectVersionIds)
    {
      if (objectVersionIds == null)
        throw new ArgumentNullException(nameof (objectVersionIds));
      if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
        throw new ArgumentException();
      List<SelectedItemsHelper.ObjectSelectedItems> objectSelectedItemsList = new List<SelectedItemsHelper.ObjectSelectedItems>();
      foreach (long objectVersionID in ((IEnumerable<long>) objectVersionIds).Distinct<long>())
      {
        try
        {
          objectSelectedItemsList.Add(new SelectedItemsHelper.ObjectSelectedItems(objectVersionID));
        }
        catch
        {
        }
      }
      this._objectSelectedItems = objectSelectedItemsList.ToArray();
    }

    public bool IsCollage => true;

    public int Count => this._objectSelectedItems.Length;

    public object GetItemData(int index, Type dataFormat)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException();
      return this._objectSelectedItems[index].GetItemData(0, dataFormat);
    }

    public INodeID GetItemID(int index)
    {
      return this.CheckIndex(index) ? this._objectSelectedItems[index].GetItemID(0) : throw new IndexOutOfRangeException();
    }

    public object GetParentData(int index, Type dataFormat)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException();
      return this._objectSelectedItems[index].GetParentData(0, dataFormat);
    }

    public NodeIDPath GetParentPath(int index)
    {
      return this.CheckIndex(index) ? this._objectSelectedItems[index].GetParentPath(0) : throw new IndexOutOfRangeException();
    }

    private bool CheckIndex(int index)
    {
      return index >= 0 && index <= this._objectSelectedItems.Length - 1;
    }
  }
}
