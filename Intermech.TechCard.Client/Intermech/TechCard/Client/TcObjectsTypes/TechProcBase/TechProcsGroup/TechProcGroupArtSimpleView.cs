// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupArtSimpleView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using ImSSP;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;
using Intermech.TechCard.Client.TcObjectsTypes.Process_Route;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>Summary description for TechProcGroupArtSimpleView.</summary>
public class TechProcGroupArtSimpleView : UserControl, IView
{
  private System.Windows.Forms.ContextMenu cmArts;
  private MenuItem miArtsAdd;
  private MenuItem miArtsEdit;
  private MenuItem miArtsDelete;
  private MenuItem menuItem4;
  private MenuItem miArtsUpdate;
  internal TreeList tlArts;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private TreeListColumn treeListColumn3;
  private TreeListColumn treeListColumn4;
  private TreeListColumn treeListColumn5;
  internal MenuItem miArtLinkMode;
  private MenuItem miArtSep2;
  private MenuItem miArtOpenEtp;
  private IContainer components;
  private MenuItem miArtExpandAll;
  private MenuItem miArtCollapse;
  private MenuItem miArtSep3;
  private MenuItem miArtSelectAll;
  private MenuItem miArtSep4;
  private MenuItem miArtSelectClear;
  private MenuItem miArtSelectInvert;
  private MenuItem miOpenInNewWindow;
  /// <summary>Ид. версии ГТП</summary>
  private ObjInfoItem _gtpObjectInfo;
  /// <summary>Ид. вида производства</summary>
  private long _productionId;
  /// <summary>Признак "Учитывать связь ТП с расцеховкой"</summary>
  private int _linkTpToRoute;
  /// <summary>Режим редактирования</summary>
  private bool _editMode;
  /// <summary>Режим привязки</summary>
  private bool _linkMode;
  /// <summary>View's caption</summary>
  private string _caption = string.Empty;
  /// <summary>
  /// 
  /// </summary>
  private INotificationService _notificationService;

  /// <summary>Initialize class data</summary>
  private void InitializeData()
  {
    this.InitializeServices();
    this.tlArts.ShowTreeListMenu += new TreeListMenuEventHandler(TechcardClientTreeListUtils.TreeList_ShowTreeListMenu);
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_280");
  }

  /// <summary>Initialize class services</summary>
  private void InitializeServices()
  {
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._notificationService.Subscribe(new NotificationEventHandler(this.NotifyEvent));
  }

  /// <summary>Load / check object's edit mode</summary>
  private void CheckEditMode()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._gtpObjectInfo.ObjectID, false);
      if (dbObject == null)
        this._editMode = false;
      else
        this._editMode = dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy == sessionKeeper.Session.UserID;
    }
  }

  /// <summary>Load proccess info</summary>
  /// <param name="gtpObjInfo">Инфрмация о ГТП / ТТП</param>
  private void DataLoad(ObjInfoItem gtpObjInfo)
  {
    this.tlArts.BeginUpdate();
    try
    {
      this.tlArts.Nodes.Clear();
      if ((TypedInfoItem) gtpObjInfo == (TypedInfoItem) null || gtpObjInfo.ObjectID == 0L)
        return;
      this.DoBeforeLoadData();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
        int procRoutingId = TechCardConsts.ObjectTypes.ProcRoutingID;
        this.ProductionLoad(gtpObjInfo.ObjectID, sessionKeeper.Session);
        ConditionStructure[] conditions1 = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechProcEdinID).ToArray(), LogicalOperators.NONE, 0, false)
        };
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
        {
          gtpObjInfo
        }, (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechLinkGTPObjRelationID
        }, ObjInfoDbScheme.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) conditions1, true, false, 1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
        DataTable source = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
        List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
        if (source != null)
          new ObjInfoDbScheme().ParseItems(source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList);
        List<int> intList = new List<int>();
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechProcEdinID));
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ArticleBaseID));
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.MarshrObrabID));
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
        intList.AddRange((IEnumerable<int>) childrenIdRecursive);
        intList.Add(procRoutingId);
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(procRoutingId));
        ConditionStructure[] conditions2 = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
        };
        Dictionary<string, ColumnDescriptor> columns = new Dictionary<string, ColumnDescriptor>();
        columns.Add("cad00020-306c-11d8-b4e9-00304f19f545", new ColumnDescriptor((object) TechCardConsts.AttributeTypes.NameAttrTypeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        columns.Add("cad0001f-306c-11d8-b4e9-00304f19f545", new ColumnDescriptor((object) TechCardConsts.AttributeTypes.DesignationAttrTypeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        string key1 = TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrGUID.ToString();
        columns.Add(key1, new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        string key2 = TechCardConsts.AttributeTypes.MemberOfZakazObjectAttrGUID.ToString();
        columns.Add(key2, new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfZakazObjectAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(objInfoItemList, sessionKeeper.Session, new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, true, conditions2, columns);
        if (parentSostavTree == null || parentSostavTree.Count == 0)
          return;
        Dictionary<long, ObjInfoItem> dictionary1 = new Dictionary<long, ObjInfoItem>(parentSostavTree.Count);
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
          dictionary1[sostavTreeItem.ProjID] = new ObjInfoItem(sostavTreeItem.ProjID, sostavTreeItem.ObjectTypeID);
        Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
        Dictionary<long, TechCardUtils.SostavTreeItem> dictionary3 = new Dictionary<long, TechCardUtils.SostavTreeItem>();
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
        {
          ObjInfoItem objInfoItem;
          if (childrenIdRecursive.Contains(sostavTreeItem.ObjectTypeID) && dictionary1.TryGetValue(sostavTreeItem.PartID, out objInfoItem) && MetaDataHelper.IsObjectTypeChildOf(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.MarshrObrabID))
          {
            if (!dictionary3.ContainsKey(sostavTreeItem.ProjID))
              dictionary3.Add(sostavTreeItem.ProjID, sostavTreeItem);
            if (!dictionary2.ContainsKey(sostavTreeItem.PartID))
              dictionary2.Add(sostavTreeItem.PartID, sostavTreeItem.ProjID);
          }
        }
        foreach (TechCardUtils.SostavTreeItem objItem in dictionary3.Values)
          this.AppendNode(objItem, (TreeListNode) null);
        foreach (TechCardUtils.SostavTreeItem objItem in parentSostavTree)
        {
          if (dictionary2.ContainsKey(objItem.ProjID))
          {
            long num = dictionary2[objItem.ProjID];
            objItem.LinkID = dictionary3[num].LinkID;
            this.AppendNode(objItem, this.GetArtNode(num));
          }
        }
      }
    }
    finally
    {
      this.DoAfterLoadData();
      this.tlArts.EndUpdate();
    }
  }

  /// <summary>Load techprocess production info</summary>
  /// <param name="objID"></param>
  /// <param name="session"></param>
  private void ProductionLoad(long objID, IUserSession session)
  {
    this._productionId = 0L;
    if (objID == 0L || session == null)
      return;
    IDBObject dbObject = session.GetObject(objID);
    if (dbObject == null)
      return;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.ProductionAttrGUID, false);
    if (attributeByGuid == null)
      return;
    this._productionId = attributeByGuid.AsInteger;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
    if (e == null)
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBObjectsCheckOutEventArgs checkOutEventArgs = e as DBObjectsCheckOutEventArgs;
    switch (e.EventName)
    {
      case "ObjectsChanged":
        if (objectsEventArgs == null)
          break;
        objectsEventArgs.ObjectIDs.Contains(this._gtpObjectInfo.ObjectID);
        break;
      case "ObjectsChangesCancelled":
      case "ObjectsCheckedIn":
        if (objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._gtpObjectInfo.ObjectID))
          break;
        this._gtpObjectInfo.ObjectID = Math.Abs(this._gtpObjectInfo.ObjectID);
        this.CheckEditMode();
        break;
      case "ObjectsCheckedOut":
        if (checkOutEventArgs == null || !checkOutEventArgs.ObjectIDs.Contains(this._gtpObjectInfo.ObjectID))
          break;
        int index = checkOutEventArgs.ObjectIDs.IndexOf(this._gtpObjectInfo.ObjectID);
        if (index == -1)
          break;
        this._gtpObjectInfo.ObjectID = checkOutEventArgs.NewObjectIDs[index];
        this.Activate((IView) null);
        break;
    }
  }

  /// <summary>Fire "before" event</summary>
  private void DoBeforeLoadData()
  {
    LoadEventHandler beforeLoadData = this.BeforeLoadData;
    if (beforeLoadData == null)
      return;
    beforeLoadData((object) this.tlArts);
  }

  private void DoAfterLoadData()
  {
    LoadEventHandler afterLoadData = this.AfterLoadData;
    if (afterLoadData == null)
      return;
    afterLoadData((object) this.tlArts);
  }

  /// <summary>Find node for article</summary>
  /// <param name="artObjID"></param>
  /// <returns></returns>
  private TreeListNode GetArtNode(long artObjID)
  {
    for (int index = 0; index <= this.tlArts.Nodes.Count - 1; ++index)
    {
      if (this.tlArts.Nodes[index].Tag != null)
      {
        ArtViewNode tag = (ArtViewNode) this.tlArts.Nodes[index].Tag;
        if ((TypedInfoItem) tag.ObjArtInfo != (TypedInfoItem) null && tag.ObjArtInfo.ObjectID == artObjID && ((TypedInfoItem) tag.ObjProcRouteInfo == (TypedInfoItem) null || tag.ObjProcRouteInfo.ObjectID == 0L))
          return this.tlArts.Nodes[index];
      }
    }
    return (TreeListNode) null;
  }

  /// <summary>Append object node</summary>
  /// <param name="objItem"></param>
  /// <param name="parentNode"></param>
  /// <returns></returns>
  private TreeListNode AppendNode(TechCardUtils.SostavTreeItem objItem, TreeListNode parentNode)
  {
    TreeListNode treeListNode1 = (TreeListNode) null;
    if (objItem == null)
      return treeListNode1;
    ArtViewNode artViewNode = new ArtViewNode();
    artViewNode.LinkProcRoute2ArtInfo = parentNode != null ? new RelInfoItem(objItem.LinkID, objItem.LinkTypeID) : (RelInfoItem) null;
    artViewNode.ObjArtInfo = parentNode == null ? new ObjInfoItem(objItem.ProjID) : ((EtpProcRoute2ArtInfo) parentNode.Tag).ObjArtInfo;
    artViewNode.ObjProcRouteInfo = parentNode == null ? (ObjInfoItem) null : new ObjInfoItem(objItem.ProjID);
    artViewNode.ObjEtpInfo = parentNode == null ? (ObjInfoItem) null : new ObjInfoItem(objItem.PartID, objItem.ObjectTypeID);
    TreeListNode treeListNode2 = this.tlArts.AppendNode((object) null, parentNode);
    treeListNode2.Tag = (object) artViewNode;
    string key1 = new Guid("cad00020-306c-11d8-b4e9-00304f19f545").ToString();
    if (objItem.Values.ContainsKey(key1))
      treeListNode2.SetValue((object) 0, objItem.Values[key1]);
    else
      treeListNode2.SetValue((object) 0, (object) "");
    Guid guid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
    string key2 = guid.ToString();
    if (objItem.Values.ContainsKey(key2))
      treeListNode2.SetValue((object) 1, objItem.Values[key2]);
    else
      treeListNode2.SetValue((object) 1, (object) "");
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objItem.ObjectTypeID);
    if (objectType != null)
      treeListNode2.SetValue((object) 2, (object) objectType.ObjectTypeName);
    guid = TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrGUID;
    string key3 = guid.ToString();
    if (objItem.Values.ContainsKey(key3))
      treeListNode2.SetValue((object) 3, objItem.Values[key3]);
    else
      treeListNode2.SetValue((object) 3, (object) "");
    guid = TechCardConsts.AttributeTypes.MemberOfZakazObjectAttrGUID;
    string key4 = guid.ToString();
    if (objItem.Values.ContainsKey(key4))
      treeListNode2.SetValue((object) 4, objItem.Values[key4]);
    else
      treeListNode2.SetValue((object) 4, (object) "");
    return treeListNode2;
  }

  /// <summary>Добавление изделия в ГТП (точнее маршрута обработки)</summary>
  /// <param name="procRoute2ArtList">Словарь содержащий информацию о МО и об изделии для него</param>
  /// <param name="routeElemNodeList"></param>
  public void ProcRouteAdd(
    Dictionary<ObjInfoItem, ObjInfoItem> procRoute2ArtList,
    RouteElemClassList routeElemNodeList)
  {
    if (procRoute2ArtList == null || procRoute2ArtList.Count == 0)
      return;
    List<long> relationIDs = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._gtpObjectInfo.ObjectID);
      TechCardUtils.CheckRelationApplicability(dbObject1.ObjectType, TechCardConsts.ObjectTypes.TechProcEdinID, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
      TechCardUtils.CheckRelationApplicability(TechCardConsts.ObjectTypes.ProcRoutingID, TechCardConsts.ObjectTypes.TechProcEdinID, TechCardConsts.RelTypes.TechRelationID);
      List<int> intList1 = new List<int>();
      if (this._linkTpToRoute != 0)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(TechCardConsts.ObjectTypes.ElemRouteGUID);
        Dictionary<int, IMSAttribute4ObjectType> dictionary = new Dictionary<int, IMSAttribute4ObjectType>();
        foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
          dictionary.Add(attribute4ObjectType.AttributeID, attribute4ObjectType);
        foreach (IMSAttribute4ObjectType attribute4ObjectType in MetaDataHelper.GetAttribute4ObjectTypeList(TechCardConsts.ObjectTypes.ElemRouteGUID))
        {
          if (dictionary.ContainsKey(attribute4ObjectType.AttributeID))
            intList1.Add(attribute4ObjectType.AttributeID);
        }
        if (intList1.Count != 0)
        {
          DataTable dataTable = sessionKeeper.Session.GetAttributesGroup(TechCardConsts.AttributeTypes.TechcardAttrGroupGuid).Attributes.Select("", (object[]) null);
          List<int> intList2 = new List<int>();
          if (dataTable != null && dataTable.Rows.Count != 0)
          {
            int num = dataTable.Columns.IndexOf("F_ATTRIBUTE_ID");
            if (num != -1)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                int result = 0;
                int columnIndex = num;
                if (int.TryParse(row[columnIndex].ToString(), out result))
                  intList2.Add(result);
              }
            }
          }
          if (intList2.Count == 0)
          {
            intList1.Clear();
          }
          else
          {
            for (int index = intList1.Count - 1; index >= 0; --index)
            {
              int num = intList1[index];
              if (!intList2.Contains(num))
                intList1.RemoveAt(index);
            }
          }
        }
      }
      TechcardClientUtils.StartCreateRelations(this._gtpObjectInfo, sessionKeeper.Session);
      try
      {
        IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.TechProcEdinID);
        IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.CehZahodObjectID);
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
        foreach (KeyValuePair<ObjInfoItem, ObjInfoItem> procRoute2Art in procRoute2ArtList)
        {
          ObjInfoItem key = procRoute2Art.Key;
          IMSApplicability applicability = MetaDataHelper.GetApplicability(key.ObjTypeID != -1 ? key.ObjTypeID : TechCardConsts.ObjectTypes.ProcRoutingID, objectCollection1.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID);
          if ((applicability == null ? 0 : (applicability.IsContent ? 1 : 0)) != 0)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(key.ObjectID);
            if ((dbObject2.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject2.ObjectModifyMode == ObjectModifyModes.CreateVersion) && dbObject2.CheckoutBy == 0L)
            {
              IDBObject dbObject3 = dbObject2.CheckOut();
              key.ObjectID = dbObject3.ObjectID;
            }
          }
          IDBObject dbObject4 = (IDBObject) null;
          try
          {
            dbObject4 = objectCollection1.Create();
            dbObject4.Attributes.Assign(dbObject1.Attributes);
            dbObject4.Attributes.AddAttribute(TechCardConsts.AttributeTypes.GtpContextAttrID, false, new object[1]
            {
              (object) true
            });
            TechProcGroupUtils.RenameEtpProcess(dbObject4, dbObject1, procRoute2Art.Value, procRoute2Art.Key, sessionKeeper.Session);
            IDBRelation relation1 = TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechLinkGTPObjRelationID, sessionKeeper.Session, dbObject1, dbObject4);
            if (relation1 != null)
            {
              relationIDs.Add(relation1.RelationID);
              projIDs.Add(relation1.ProjID);
              relTypeIDs.Add(relation1.RelationType);
            }
            if ((sessionKeeper.Session.GetRelation(key.ObjectID, dbObject4.ObjectID, TechCardConsts.RelTypes.TechRelationID, true) ?? sessionKeeper.Session.GetRelation(dbObject4.ObjectID, key.ObjectID, TechCardConsts.RelTypes.TechRelationID, true)) == null)
              relationCollection.Create(key.ObjectID, dbObject4.ObjectID);
            if (this._linkTpToRoute != 0)
            {
              TechcardClientUtils.StartCreateRelations(dbObject4.ObjectID, sessionKeeper.Session);
              try
              {
                List<long> longList = new List<long>();
                foreach (RouteElemClass routeElemNode in (List<RouteElemClass>) routeElemNodeList)
                {
                  if (routeElemNode.ProcRouteID == key.ObjectID)
                  {
                    if (!longList.Contains(routeElemNode.CehRouteID))
                    {
                      longList.Add(routeElemNode.CehRouteID);
                      TechcardClientUtils.CreateRelations(sessionKeeper.Session, dbObject4.ObjectID, new int[1]
                      {
                        TechCardConsts.RelTypes.TechRouteRelationID
                      }, new long[1]
                      {
                        routeElemNode.CehRouteID
                      }, DateTime.Now, TechCreateRelMode.tcrmContains);
                    }
                    IDBObject partDbObject = objectCollection2.Create();
                    IDBObject projDbObject = sessionKeeper.Session.GetObject(routeElemNode.ObjID);
                    AttributeValues[] valuesList1 = new AttributeValues[intList1.Count];
                    for (int index = 0; index < intList1.Count; ++index)
                    {
                      int attributeID = intList1[index];
                      IDBAttribute attributeById = projDbObject.GetAttributeByID(attributeID);
                      valuesList1[index] = attributeById != null ? new AttributeValues(attributeID, attributeById.Value) : new AttributeValues(attributeID, (object) null);
                    }
                    partDbObject.SetAttributesValues(valuesList1);
                    TechcardClientUtils.CreateRelations(sessionKeeper.Session, partDbObject.ObjectID, new int[1]
                    {
                      TechCardConsts.RelTypes.TechRelationID
                    }, new long[1]{ dbObject4.ObjectID }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
                    List<IDBRelation> dbRelationList = new List<IDBRelation>();
                    TechcardClientUtils.StartCreateRelations(projDbObject.ObjectID, sessionKeeper.Session);
                    try
                    {
                      dbRelationList.Clear();
                      dbRelationList.Add(TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechRouteRelationID, sessionKeeper.Session, projDbObject, partDbObject));
                    }
                    finally
                    {
                      TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
                    }
                    if (dbRelationList.Count > 0)
                    {
                      AttributeValues[] valuesList2 = new AttributeValues[1];
                      IDBRelation relation2 = sessionKeeper.Session.GetRelation(routeElemNode.LinkID);
                      int attributeID = TechCardConsts.Utils.AttributeTypeByGuid(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid, sessionKeeper.Session);
                      valuesList2[0] = new AttributeValues(attributeID, (object) (relation2 as IDBGuid).GUID);
                      dbRelationList[0].SetAttributesValues(valuesList2);
                    }
                    partDbObject.CommitCreation(true);
                  }
                }
              }
              finally
              {
                TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
              }
            }
            dbObject4.CommitCreation(true);
          }
          catch
          {
            dbObject4?.Delete(0L);
            throw;
          }
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
    }
    if (relationIDs.Count <= 0)
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
  }

  /// <summary>Add articles</summary>
  /// <returns></returns>
  private bool ArtsAdd()
  {
    List<long> objArtList = TechCardClientConst.SelectObjectsDlg((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes, LocalizationHolder.rm.GetString("TechCard.Client_281"));
    if (objArtList.Count == 0)
      return false;
    Dictionary<ObjInfoItem, ObjInfoItem> procRoute2ArtList1 = new Dictionary<ObjInfoItem, ObjInfoItem>();
    RouteElemClassList routeElemNodes = new RouteElemClassList();
    if (this._linkTpToRoute == 0)
    {
      Dictionary<long, long> procRoute2ArtList2;
      if (ProcRouteListViewDlg.ShowDialog(objArtList, this._gtpObjectInfo.ObjectID, (long[]) null, out procRoute2ArtList2) && procRoute2ArtList2 != null)
      {
        procRoute2ArtList1.Clear();
        foreach (KeyValuePair<long, long> keyValuePair in procRoute2ArtList2)
          procRoute2ArtList1.Add(new ObjInfoItem(keyValuePair.Key), new ObjInfoItem(keyValuePair.Value));
      }
    }
    else if (CehRoutesElemsListDlg.ShowDialog(objArtList[0], this._productionId, ref routeElemNodes))
    {
      foreach (RouteElemClass routeElemClass in (List<RouteElemClass>) routeElemNodes)
      {
        ObjInfoItem key = new ObjInfoItem(routeElemClass.ProcRouteID);
        if (!procRoute2ArtList1.ContainsKey(key))
          procRoute2ArtList1.Add(key, new ObjInfoItem(objArtList[0]));
      }
    }
    if (procRoute2ArtList1.Count == 0)
      return false;
    this.ProcRouteAdd(procRoute2ArtList1, routeElemNodes);
    return procRoute2ArtList1.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="artViewNode"></param>
  /// <returns></returns>
  private bool ArtsEdit(ArtViewNode artViewNode)
  {
    if (artViewNode == null || (TypedInfoItem) artViewNode.LinkProcRoute2ArtInfo == (TypedInfoItem) null || artViewNode.LinkProcRoute2ArtInfo.RelationID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long procRouteId = 0;
      if (!ProcRouteListViewDlg.ShowDialog(artViewNode.ObjArtInfo.ObjectID, this._gtpObjectInfo.ObjectID, ref procRouteId) || procRouteId == 0L)
        return false;
      Gtp2EtpRefData etpObjIdList = TechProcGroupUtils.GetEtpObjIDList(this._gtpObjectInfo, TechCardConsts.ObjectTypes.TechProcEdinGUID, sessionKeeper.Session);
      if (etpObjIdList == null || etpObjIdList.ObjRefIDs.Count == 0)
        return false;
      List<TypedInfoItem> typedInfoItemList = new List<TypedInfoItem>((IEnumerable<TypedInfoItem>) etpObjIdList.ObjRefIDs.Values);
      ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) typedInfoItemList.ToArray(), LogicalOperators.AND, 0, false),
        new ConditionStructure(-7, RelationalOperators.Equal, (object) TechCardConsts.ObjectTypes.TechProcEdinID, LogicalOperators.NONE, 0, false)
      };
      DataTable childSostavData = DataHelper.GetChildSostavData(artViewNode.ObjProcRouteInfo, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
      if (childSostavData != null && childSostavData.Rows.Count > 0)
      {
        for (int index = 0; index <= childSostavData.Rows.Count - 1; ++index)
        {
          DataRow row = childSostavData.Rows[index];
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
          if (int32 == TechCardConsts.ObjectTypes.TechProcEdinID)
          {
            ObjInfoItem objInfoItem = new ObjInfoItem(int64, int32);
            if (typedInfoItemList.Contains((TypedInfoItem) objInfoItem))
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(Convert.ToInt64(row["F_PRJLINK_ID"]));
              if (relation != null)
              {
                relation.Delete(0L);
                IDBObject dbObject = sessionKeeper.Session.GetObject(procRouteId);
                relationCollection.Create(dbObject.ObjectID, int64);
                return true;
              }
            }
          }
        }
      }
      return false;
    }
  }

  /// <summary>Remove article (procRoute) from gtp</summary>
  /// <param name="procRouteIDs"></param>
  /// <param name="gtpObjInfo"></param>
  /// <returns></returns>
  private bool ArtsDelete(List<ObjInfoItem> procRouteIDs, ObjInfoItem gtpObjInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (procRouteIDs == null || procRouteIDs.Count == sc_19689.ssp_techcard_19690(1982078817) || (TypedInfoItem) gtpObjInfo == (TypedInfoItem) null || gtpObjInfo.ObjectID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int techRelationId = TechCardConsts.RelTypes.TechRelationID;
      Gtp2EtpRefData etpObjIdList = TechProcGroupUtils.GetEtpObjIDList(gtpObjInfo, TechCardConsts.ObjectTypes.TechProcEdinGUID, sessionKeeper.Session);
      if (etpObjIdList == null || etpObjIdList.ObjRefIDs.Count == 0)
        return false;
      List<long> itemIds = SomeTypedInfoHelper<TypedInfoItem>.GetItemIDs((IEnumerable<TypedInfoItem>) etpObjIdList.ObjRefIDs.Values);
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (itemIds.Count == 1)
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.Equal, (object) itemIds[0], LogicalOperators.AND, 0, false));
      else
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.In, (object) itemIds.ToArray(), LogicalOperators.AND, 0, false));
      List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(procRouteIDs, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        techRelationId
      }, false, conditionStructureList.ToArray(), (Dictionary<string, ColumnDescriptor>) null);
      List<long> longList = new List<long>();
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in childSostavTree)
      {
        if (itemIds.Contains(sostavTreeItem.PartID))
          longList.Add(sostavTreeItem.PartID);
      }
      DeleteCommand deleteCommand = new DeleteCommand();
      deleteCommand.DeleteOptions = DeleteAnalyzerOptions.None;
      deleteCommand.Init(ObjectExtensions.GetItems(longList.ToArray()), TechCardClient.ServiceProvider, (object) null);
      deleteCommand.Execute();
      return true;
    }
  }

  /// <summary>Remove article nodes</summary>
  /// <param name="artViewNodes">Nodes to remove</param>
  /// <returns></returns>
  private bool ArtsDelete(List<ArtViewNode> artViewNodes)
  {
    if (artViewNodes == null || artViewNodes.Count == 0)
      return false;
    List<ObjInfoItem> procRouteIDs = new List<ObjInfoItem>();
    for (int index1 = 0; index1 < artViewNodes.Count; ++index1)
    {
      ArtViewNode artViewNode = artViewNodes[index1];
      if ((TypedInfoItem) artViewNode.LinkProcRoute2ArtInfo == (TypedInfoItem) null || artViewNode.LinkProcRoute2ArtInfo.RelationID == 0L)
      {
        TreeListNode treeListNode = (TreeListNode) null;
        for (int index2 = 0; index2 < this.tlArts.Nodes.Count; ++index2)
        {
          if (this.tlArts.Nodes[index2].Tag == artViewNode)
          {
            treeListNode = this.tlArts.Nodes[index2];
            break;
          }
        }
        if (treeListNode != null)
        {
          foreach (TreeListNode node in treeListNode.Nodes)
            procRouteIDs.Add(((EtpProcRoute2ArtInfo) node.Tag).ObjProcRouteInfo);
        }
      }
      else
        procRouteIDs.Add(artViewNode.ObjProcRouteInfo);
    }
    return this.ArtsDelete(procRouteIDs, this._gtpObjectInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="artViewNode"></param>
  /// <returns></returns>
  private bool ArtsLinkMode(ArtViewNode artViewNode)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (artViewNode == null)
      return false;
    using (TechProcGroupLinkArt2ObjDialog linkArt2ObjDialog = new TechProcGroupLinkArt2ObjDialog())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        linkArt2ObjDialog.Text += $" ({TechCardConsts.Utils.GetObjectString(artViewNode.ObjArtInfo.ObjectID, sessionKeeper.Session)} / {TechCardConsts.Utils.GetObjectString(artViewNode.ObjProcRouteInfo.ObjectID, sessionKeeper.Session)})";
      return linkArt2ObjDialog.ShowDialog(artViewNode.ObjProcRouteInfo, this._gtpObjectInfo);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="artViewNode"></param>
  /// <returns></returns>
  private bool OpenETP(ArtViewNode artViewNode)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (artViewNode == null)
      return false;
    ObjInfoItem objEtpInfo = artViewNode.ObjEtpInfo;
    if ((TypedInfoItem) objEtpInfo == (TypedInfoItem) null || objEtpInfo.ObjectID == 0L)
      return false;
    TechCardClientConst.OpenObjectInNewWindow(objEtpInfo.ObjectID);
    return true;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcGroupArtSimpleView));
    this.cmArts = new System.Windows.Forms.ContextMenu();
    this.miArtsAdd = new MenuItem();
    this.miArtsEdit = new MenuItem();
    this.miArtsDelete = new MenuItem();
    this.miArtSep4 = new MenuItem();
    this.miArtSelectAll = new MenuItem();
    this.miArtSelectClear = new MenuItem();
    this.miArtSelectInvert = new MenuItem();
    this.menuItem4 = new MenuItem();
    this.miArtOpenEtp = new MenuItem();
    this.miArtLinkMode = new MenuItem();
    this.miArtSep2 = new MenuItem();
    this.miArtExpandAll = new MenuItem();
    this.miArtCollapse = new MenuItem();
    this.miArtSep3 = new MenuItem();
    this.miOpenInNewWindow = new MenuItem();
    this.miArtsUpdate = new MenuItem();
    this.tlArts = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumn3 = new TreeListColumn();
    this.treeListColumn4 = new TreeListColumn();
    this.treeListColumn5 = new TreeListColumn();
    this.tlArts.BeginInit();
    this.SuspendLayout();
    this.cmArts.MenuItems.AddRange(new MenuItem[16 /*0x10*/]
    {
      this.miArtsAdd,
      this.miArtsEdit,
      this.miArtsDelete,
      this.miArtSep4,
      this.miArtSelectAll,
      this.miArtSelectClear,
      this.miArtSelectInvert,
      this.menuItem4,
      this.miArtOpenEtp,
      this.miArtLinkMode,
      this.miArtSep2,
      this.miArtExpandAll,
      this.miArtCollapse,
      this.miArtSep3,
      this.miOpenInNewWindow,
      this.miArtsUpdate
    });
    this.cmArts.Popup += new EventHandler(this.cmArts_Popup);
    this.miArtsAdd.Index = 0;
    componentResourceManager.ApplyResources((object) this.miArtsAdd, "miArtsAdd");
    this.miArtsAdd.Click += new EventHandler(this.miArtsAdd_Click);
    this.miArtsEdit.Index = 1;
    componentResourceManager.ApplyResources((object) this.miArtsEdit, "miArtsEdit");
    this.miArtsEdit.Click += new EventHandler(this.miArtsEdit_Click);
    this.miArtsDelete.Index = 2;
    componentResourceManager.ApplyResources((object) this.miArtsDelete, "miArtsDelete");
    this.miArtsDelete.Click += new EventHandler(this.miArtsDelete_Click);
    this.miArtSep4.Index = 3;
    componentResourceManager.ApplyResources((object) this.miArtSep4, "miArtSep4");
    componentResourceManager.ApplyResources((object) this.miArtSelectAll, "miArtSelectAll");
    this.miArtSelectAll.Index = 4;
    this.miArtSelectAll.Click += new EventHandler(this.miArtSelectAll_Click);
    componentResourceManager.ApplyResources((object) this.miArtSelectClear, "miArtSelectClear");
    this.miArtSelectClear.Index = 5;
    this.miArtSelectClear.Click += new EventHandler(this.miArtSelectClear_Click);
    componentResourceManager.ApplyResources((object) this.miArtSelectInvert, "miArtSelectInvert");
    this.miArtSelectInvert.Index = 6;
    this.miArtSelectInvert.Click += new EventHandler(this.miArtSelectInvert_Click);
    this.menuItem4.Index = 7;
    componentResourceManager.ApplyResources((object) this.menuItem4, "menuItem4");
    this.miArtOpenEtp.Index = 8;
    componentResourceManager.ApplyResources((object) this.miArtOpenEtp, "miArtOpenEtp");
    this.miArtOpenEtp.Click += new EventHandler(this.miArtOpenEtp_Click);
    this.miArtLinkMode.Index = 9;
    componentResourceManager.ApplyResources((object) this.miArtLinkMode, "miArtLinkMode");
    this.miArtLinkMode.Click += new EventHandler(this.miArtLinkMode_Click);
    this.miArtSep2.Index = 10;
    componentResourceManager.ApplyResources((object) this.miArtSep2, "miArtSep2");
    this.miArtExpandAll.Index = 11;
    componentResourceManager.ApplyResources((object) this.miArtExpandAll, "miArtExpandAll");
    this.miArtExpandAll.Click += new EventHandler(this.miArtExpand_Click);
    this.miArtCollapse.Index = 12;
    componentResourceManager.ApplyResources((object) this.miArtCollapse, "miArtCollapse");
    this.miArtCollapse.Click += new EventHandler(this.miArtCollapse_Click);
    this.miArtSep3.Index = 13;
    componentResourceManager.ApplyResources((object) this.miArtSep3, "miArtSep3");
    this.miOpenInNewWindow.Index = 14;
    componentResourceManager.ApplyResources((object) this.miOpenInNewWindow, "miOpenInNewWindow");
    this.miOpenInNewWindow.Click += new EventHandler(this.miOpenInNewWindow_Click);
    this.miArtsUpdate.Index = 15;
    componentResourceManager.ApplyResources((object) this.miArtsUpdate, "miArtsUpdate");
    this.miArtsUpdate.Click += new EventHandler(this.miArtsUpdate_Click);
    componentResourceManager.ApplyResources((object) this.tlArts, "tlArts");
    this.tlArts.Columns.AddRange(new TreeListColumn[5]
    {
      this.treeListColumn1,
      this.treeListColumn2,
      this.treeListColumn3,
      this.treeListColumn4,
      this.treeListColumn5
    });
    this.tlArts.ContextMenu = this.cmArts;
    this.tlArts.Name = "tlArts";
    this.tlArts.CheckStateChanging += new CheckStateChangingEventHandler(this.tlArts_CheckStateChanging);
    this.tlArts.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlArts_FocusedNodeChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    componentResourceManager.ApplyResources((object) this.treeListColumn3, "treeListColumn3");
    this.treeListColumn3.Name = "treeListColumn3";
    componentResourceManager.ApplyResources((object) this.treeListColumn4, "treeListColumn4");
    this.treeListColumn4.Name = "treeListColumn4";
    componentResourceManager.ApplyResources((object) this.treeListColumn5, "treeListColumn5");
    this.treeListColumn5.Name = "treeListColumn5";
    this.ContextMenu = this.cmArts;
    this.Controls.Add((Control) this.tlArts);
    this.Name = nameof (TechProcGroupArtSimpleView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.tlArts.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Конструктор</summary>
  /// <param name="linkMode">Режим привязки</param>
  public TechProcGroupArtSimpleView(bool linkMode)
  {
    this.InitializeComponent();
    this.InitializeData();
    this._linkMode = linkMode;
  }

  /// <summary>Конструктор</summary>
  public TechProcGroupArtSimpleView()
    : this(false)
  {
  }

  /// <summary>Initialize</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null || items.Count == 0)
    {
      this._gtpObjectInfo = (ObjInfoItem) null;
    }
    else
    {
      IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      this._gtpObjectInfo = new ObjInfoItem(itemData.ObjectID, itemData.ObjectType);
    }
  }

  /// <summary>Activate</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.CheckEditMode();
    this.DataLoad(this._gtpObjectInfo);
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
  }

  /// <summary>Caption</summary>
  public string Caption => this._caption;

  /// <summary>ImageIndex</summary>
  public int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public int OrderID => 0;

  /// <summary>Ид. версии ГТП</summary>
  public ObjInfoItem GtpObjectInfo
  {
    get => this._gtpObjectInfo;
    set
    {
      if (!((TypedInfoItem) this._gtpObjectInfo == (TypedInfoItem) null) && this._gtpObjectInfo.Equals(value))
        return;
      this._gtpObjectInfo = value;
      this.DataLoad(this._gtpObjectInfo);
      this.CheckEditMode();
    }
  }

  /// <summary>DataLoadCheck</summary>
  /// <param name="gtpObjId"></param>
  public void DataLoadCheck(long gtpObjId)
  {
    if (gtpObjId == 0L)
      return;
    foreach (TreeListNode node1 in this.tlArts.Nodes)
    {
      node1.CheckState = CheckState.Indeterminate;
      foreach (TreeListNode node2 in node1.Nodes)
        node2.CheckState = CheckState.Indeterminate;
    }
  }

  /// <summary>Before Load Data Event</summary>
  public event LoadEventHandler BeforeLoadData;

  /// <summary>After Load Data Event</summary>
  public event LoadEventHandler AfterLoadData;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmArts_Popup(object sender, EventArgs e)
  {
    bool flag1 = this.tlArts.Selection.Count == 1 && (TypedInfoItem) (this.tlArts.Selection[0].Tag as ArtViewNode).LinkProcRoute2ArtInfo != (TypedInfoItem) null && (this.tlArts.Selection[0].Tag as ArtViewNode).LinkProcRoute2ArtInfo.RelationID != 0L;
    bool flag2 = this._editMode && !this._linkMode;
    this.miArtsAdd.Visible = this.miArtsEdit.Visible = this.miArtsDelete.Visible = this.miArtLinkMode.Visible = this.miArtOpenEtp.Visible = this.miArtSep2.Visible = !this._linkMode;
    this.miArtSelectAll.Visible = this.miArtSelectClear.Visible = this.miArtSelectInvert.Visible = this._linkMode;
    this.miArtsAdd.Enabled = flag2;
    this.miArtsEdit.Enabled = this.miArtLinkMode.Enabled = flag2 & flag1;
    this.miArtOpenEtp.Enabled = flag1;
    this.miArtsDelete.Enabled = flag2 && this.tlArts.Selection.Count > 0;
    this.miArtCollapse.Enabled = this.miArtExpandAll.Enabled = this.tlArts.Nodes.Count > 0;
    this.miOpenInNewWindow.Enabled = this.tlArts.Nodes.Count > 0;
    this.miArtSelectAll.Enabled = this.miArtSelectClear.Enabled = this.miArtSelectInvert.Enabled = this._linkMode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlArts_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtsUpdate_Click(object sender, EventArgs e) => this.DataLoad(this._gtpObjectInfo);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtsAdd_Click(object sender, EventArgs e)
  {
    if (!this.ArtsAdd())
      return;
    this.DataLoad(this._gtpObjectInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtsEdit_Click(object sender, EventArgs e)
  {
    if (this.tlArts.Selection.Count != 1 || !this.ArtsEdit((ArtViewNode) this.tlArts.Selection[0].Tag))
      return;
    this.DataLoad(this._gtpObjectInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtsDelete_Click(object sender, EventArgs e)
  {
    if (this.tlArts.Selection.Count == sc_19689.ssp_techcard_19691(344688903))
      return;
    List<ArtViewNode> artViewNodes = new List<ArtViewNode>(this.tlArts.Selection.Count);
    foreach (TreeListNode treeListNode in (CollectionBase) this.tlArts.Selection)
      artViewNodes.Add((ArtViewNode) treeListNode.Tag);
    if (!this.ArtsDelete(artViewNodes))
      return;
    this.DataLoad(this._gtpObjectInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtLinkMode_Click(object sender, EventArgs e)
  {
    if (this.tlArts.Selection.Count == 0)
      return;
    foreach (TreeListNode treeListNode in (CollectionBase) this.tlArts.Selection)
      this.ArtsLinkMode((ArtViewNode) treeListNode.Tag);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtOpenEtp_Click(object sender, EventArgs e)
  {
    if (this.tlArts.Selection.Count != sc_19689.ssp_techcard_19692(1272327469))
      return;
    this.OpenETP((ArtViewNode) this.tlArts.Selection[0].Tag);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtExpand_Click(object sender, EventArgs e) => this.tlArts.FullExpand();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtCollapse_Click(object sender, EventArgs e) => this.tlArts.FullCollapse();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtSelectAll_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node1 in this.tlArts.Nodes)
    {
      foreach (TreeListNode node2 in node1.Nodes)
        node2.CheckState = CheckState.Checked;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtSelectClear_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node1 in this.tlArts.Nodes)
    {
      foreach (TreeListNode node2 in node1.Nodes)
        node2.CheckState = CheckState.Unchecked;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miArtSelectInvert_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node1 in this.tlArts.Nodes)
    {
      foreach (TreeListNode node2 in node1.Nodes)
        node2.CheckState = node2.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miOpenInNewWindow_Click(object sender, EventArgs e)
  {
    if (this.tlArts.Selection.Count != sc_19689.ssp_techcard_19693(49983721) || !(this.tlArts.Selection[0].Tag is ArtViewNode tag))
      return;
    ObjInfoItem objArtInfo = tag.ObjArtInfo;
    if ((TypedInfoItem) objArtInfo == (TypedInfoItem) null || objArtInfo.ObjectID == 0L)
      return;
    TechCardClientConst.OpenObjectInNewWindow(objArtInfo.ObjectID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlArts_CheckStateChanging(object sender, DevExpress.IM.XtraTreeList.CheckStateEventArgs e)
  {
  }
}
