// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.View.TechCardBaseGroupArtView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Collections;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.NavigatorSupport.NodeFactories;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.View;

/// <summary>
/// Закладка для отображения списка привязанных изделий / МО для групповых объектов
/// </summary>
public class TechCardBaseGroupArtView : TechCardBaseView
{
  /// <summary>Заголовок текущего объекта</summary>
  private string _objCaption = string.Empty;
  /// <summary>
  /// 
  /// </summary>
  private bool _canEdit;
  /// <summary>Признак загрузки данных</summary>
  private bool _dataLoaded;
  /// <summary>
  /// Отдельный список ДСЕ - для сохранения сортировки элементов
  /// </summary>
  private readonly List<ObjInfoItem> _artObjectInfoList = new List<ObjInfoItem>();
  /// <summary>
  /// Список связей для построения "дерева привязок" объектов к изделиям
  /// </summary>
  /// <remarks></remarks>
  private readonly IList<RelObjInfoItem> _relObjInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
  /// <summary>
  /// 
  /// </summary>
  private BackgroundWorker _backgroundWorker;
  /// <summary>
  /// 
  /// </summary>
  private IServiceContainer _viewServices;
  /// <summary>Типы привязываемых объектов</summary>
  protected readonly IList<int> _linkObjectTypes = (IList<int>) new List<int>();
  /// <summary>
  /// 
  /// </summary>
  internal static readonly string IconImageName = "imgGroupArtView";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar toolBarTop;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl techTreeViewArt2Mo;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem1;
  private MenuButtonItem mbiRefresh;
  private MenuButtonItem mbiOpenInNewWindow;
  private MenuButtonItem mbiProperty;
  private MenuButtonItem mbiSearch;
  private MenuButtonItem mbiAddTechGroupLink;
  private MenuButtonItem mbiChangeTechGroupLink;
  private MenuButtonItem mbiDeleteTechGroupLink;
  protected MenuButtonItem mbiOpenLinkedObject;
  protected MenuButtonItem mbiProcRouteLinkMode;
  private MenuButtonItem mbiExpandTree;
  private MenuButtonItem mbiCollapseTree;
  private MenuButtonItem mbiSetupColumns;

  /// <summary>Инициализация TreeView</summary>
  private void InitializeTreeView()
  {
    this.techTreeViewArt2Mo.DisableIMContextMenu = true;
    this.techTreeViewArt2Mo.DisableKeyUpEvents = false;
    if (this.DesignMode)
      return;
    this._viewServices = (IServiceContainer) new ServiceContainer(this._services);
    this._viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    this._viewServices.AddService(typeof (ICommandManager), (object) ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false));
    this._viewServices.AddService(typeof (INotificationService), (object) ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false));
    this._viewServices.AddService(typeof (INodesFactorySupported), (object) new TechCompositionFromRelObjInfoItemFactorySupport((IEnumerable<RelObjInfoItem>) this._relObjInfoItems));
    this.techTreeViewArt2Mo.Services = (System.IServiceProvider) this._viewServices;
    this.techTreeViewArt2Mo.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = "";
    int marshrObrabId = TechCardConsts.ObjectTypes.MarshrObrabID;
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, marshrObrabId, caption, (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.techTreeViewArt2Mo.SetColumns(columns, descriptor);
  }

  /// <summary>Инициализация меню</summary>
  private void InitializeContextMenu()
  {
    if (this.DesignMode)
      return;
    INamedImageList service1 = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    AdjustableMenuCommands service2 = ServiceUtils.GetService<AdjustableMenuCommands>((object) ApplicationServices.Container, false);
    if (service2 == null)
      return;
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.contextMenuBarItem1.Items)
    {
      AdjustableMenuCommand commandFromRoot = service2.FindCommandFromRoot(menuButtonItem.CommandName);
      if (commandFromRoot != null && commandFromRoot.ImageIndex != -1 && service1 != null)
        menuButtonItem.Image = service1.ImageList.Images[commandFromRoot.ImageIndex];
    }
  }

  /// <summary>
  /// Проверка объекта на доступность редактирования для тек. закладки
  /// </summary>
  private void CheckObjectEditableMode()
  {
    this._canEdit = false;
    IEnumerable<IMSApplicability> list = (IEnumerable<IMSApplicability>) this._linkObjectTypes.Select<int, IMSApplicability>((System.Func<int, IMSApplicability>) (linkObjectType => MetaDataHelper.GetApplicability(this._objTypeID, linkObjectType, TechCardConsts.RelTypes.TechLinkGTPObjRelationID))).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (imsApplicability => imsApplicability != null)).ToList<IMSApplicability>();
    if (!list.Any<IMSApplicability>())
      return;
    if (list.Any<IMSApplicability>((System.Func<IMSApplicability, bool>) (imsApplicability => !imsApplicability.IsContent)))
    {
      this._canEdit = true;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objID, false);
        this._canEdit = dbObject != null && !dbObject.ReadOnly;
      }
    }
  }

  /// <summary>Обновление команд контекстного меню</summary>
  protected virtual void UpdateContextCommands()
  {
    bool flag1 = this._canEdit && this._dataLoaded;
    bool flag2 = false;
    bool flag3 = false;
    IDBTypedObjectID typedObjectId;
    this.GetCurrentTypedObjectID(out typedObjectId);
    if (flag1)
    {
      flag3 = this._linkObjectTypes.Contains(typedObjectId != null ? typedObjectId.ObjectType : -1);
      if (flag3)
        flag2 = true;
    }
    this.mbiAddTechGroupLink.Enabled = this.mbiAddTechGroupLink.Visible = flag1;
    this.mbiChangeTechGroupLink.Enabled = this.mbiChangeTechGroupLink.Visible = flag1 & flag2;
    this.mbiDeleteTechGroupLink.Enabled = this.mbiDeleteTechGroupLink.Visible = flag1 && typedObjectId != null;
    this.mbiProcRouteLinkMode.Enabled = this.mbiProcRouteLinkMode.Visible = flag1 & flag3;
    this.mbiOpenLinkedObject.Enabled = this.mbiOpenLinkedObject.Visible = flag3;
    this.mbiOpenInNewWindow.Enabled = this.mbiOpenInNewWindow.Visible = this.mbiProperty.Enabled = this.mbiProperty.Visible = this.mbiSearch.Enabled = this.mbiSearch.Visible = typedObjectId != null;
    this.mbiExpandTree.Enabled = this.mbiCollapseTree.Enabled = this.techTreeViewArt2Mo.RootNode?.Children != null && this.techTreeViewArt2Mo.RootNode.Children.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateTreeViewData()
  {
    string caption = string.Format(LocalizationHolder.rm.GetString("TechCard.TechCardBaseGroupArtView_Articles_List"), (object) this._objCaption);
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (ObjInfoItem artObjectInfo in this._artObjectInfoList)
    {
      Intermech.Navigator.DBObjects.Descriptor descriptor1 = new Intermech.Navigator.DBObjects.Descriptor(artObjectInfo.ObjectID);
      descriptor1.Services = (System.IServiceProvider) this._viewServices;
      Intermech.Navigator.DBObjects.Descriptor descriptor2 = descriptor1;
      descriptors.Add((IDescriptor) descriptor2);
    }
    IDescriptor rootDescriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, caption, descriptors);
    NodeIDPath focusedPath = this.techTreeViewArt2Mo.FocusedPath;
    foreach (NodeColumn column in (List<NodeColumn>) this.techTreeViewArt2Mo.GetColumns())
    {
      if (column.SortOrder != NodeColumnSortOrder.None)
        column.SortOrder = NodeColumnSortOrder.None;
    }
    this.techTreeViewArt2Mo.Build(rootDescriptor);
    if (focusedPath != null && focusedPath.Length > 1)
    {
      NodeIDPath nodeIDPath = new NodeIDPath(rootDescriptor);
      foreach (INodeID NodeID in focusedPath)
        nodeIDPath.Add(NodeID);
      this.techTreeViewArt2Mo.TryBrowse(nodeIDPath);
    }
    else
    {
      if (this.techTreeViewArt2Mo.RootNode == null || this.techTreeViewArt2Mo.RootNode.Children != null && this.techTreeViewArt2Mo.RootNode.Children.Count != 0 || descriptors.Count <= 0)
        return;
      this.techTreeViewArt2Mo.TryBrowse(new NodeIDPath(rootDescriptor)
      {
        rootDescriptor.GetRecordNodeID()
      });
    }
  }

  /// <summary>Get current object info</summary>
  /// <param name="typedObjId"></param>
  /// <returns></returns>
  private bool GetCurrentTypedObjectID(out IDBTypedObjectID typedObjectId)
  {
    typedObjectId = (IDBTypedObjectID) null;
    IFocusedItem focusedItem = this.techTreeViewArt2Mo.FocusedItem;
    if (focusedItem == null)
      return false;
    typedObjectId = focusedItem.GetItemData(typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    return typedObjectId != null;
  }

  /// <summary>Get current article object</summary>
  /// <param name="articleTypedObjectId"></param>
  /// <returns></returns>
  private bool GetCurrentArticleTypeObjectID(out IDBTypedObjectID articleTypedObjectId)
  {
    articleTypedObjectId = (IDBTypedObjectID) null;
    NavigatorTreeNode node = this.techTreeViewArt2Mo.FocusedNode;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    for (; node != null; node = node.Parent)
    {
      IDBTypedObjectID data = node.GetData<IDBTypedObjectID>(node.NodeID);
      if (data != null && childrenIdRecursive.Contains(data.ObjectType))
      {
        articleTypedObjectId = data;
        break;
      }
    }
    return articleTypedObjectId != null;
  }

  /// <summary>Загрузка информации о текущем объекте</summary>
  private void DoLoadObjInfo()
  {
    List<int> items = new List<int>();
    foreach (ApplicabilitiesKey applicabilitiesKey in MetaDataHelper.GetObjectTypeApplicabilities(this._objTypeID).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (a => a.RelationTypeID == TechCardConsts.RelTypes.TechLinkGTPObjRelationID)).ToList<IMSApplicability>().GetEnableChildApplicabilitiesKey())
      items.Add(applicabilitiesKey.ChildType);
    this._linkObjectTypes.Clear();
    this._linkObjectTypes.AddRange<int>((IEnumerable<int>) items);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._objCaption = sessionKeeper.Session.GetObjectInfo(this._objID).Caption;
  }

  /// <summary>
  /// 
  /// </summary>
  private void DoLoadData()
  {
    if (this._backgroundWorker == null)
      return;
    if (this._backgroundWorker.IsBusy)
    {
      int num = 0;
      while (this._backgroundWorker.CancellationPending)
      {
        Thread.Sleep(100);
        Application.DoEvents();
        ++num;
        if (num >= 5)
          break;
      }
    }
    if (this._backgroundWorker.IsBusy)
      return;
    this._dataLoaded = false;
    this._artObjectInfoList.Clear();
    this._relObjInfoItems.Clear();
    this._backgroundWorker.RunWorkerAsync();
    this.UpdateTreeViewData();
    StatusPopup.Show(ResourceHolder.LoadingImage, LocalizationHolder.rm.GetString("TechCard.Client_481"), (Control) this.techTreeViewArt2Mo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandName"></param>
  private bool NavigatorContextMenuInvoke(string commandName)
  {
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.techTreeViewArt2Mo.SelectedItems, this.techTreeViewArt2Mo.Services);
    if (commandsTable == null || !commandsTable.Contains(commandName))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, this.techTreeViewArt2Mo.Services);
    return true;
  }

  /// <summary>
  /// Добавление изделия в групповой объект (точнее привязка единичного)
  /// </summary>
  private void AddTechGroupLink()
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
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.AddRange((IEnumerable<IDescriptor>) new HiveDescriptor[1]
    {
      new HiveDescriptor(SelectProductionCopyFromProductionReportWizardControl.RootCategoryNodeId, MRP2Consts.objtypeIdProductionLists, "Из состава производственной ведомости")
    });
    Intermech.Navigator.CustomNode.Descriptor descriptor = new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, MRP2Consts.objtypeIdProductionLists, "Выбор из мастера", descriptors);
    List<long> itemIDs = TechCardClientConst.SelectObjectsDlg((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes, LocalizationHolder.rm.GetString("TechCard.Client_281"), new IDescriptor[1]
    {
      (IDescriptor) descriptor
    }, LocalizationHolder.rm.GetString("TechCard.Client_505"));
    if (itemIDs == null || itemIDs.Count == 0)
      return;
    TechProcGroupSelectLinkingObjectDialog linkingObjectDialog = new TechProcGroupSelectLinkingObjectDialog()
    {
      LinkedObjInfoItems = (IList<ObjInfoItem>) this._relObjInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => this._linkObjectTypes.Contains(item.PartInfo.ObjTypeID))).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo)).ToList<ObjInfoItem>()
    };
    if (!linkingObjectDialog.ShowDialog((IEnumerable<ObjInfoItem>) SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) itemIDs), (IEnumerable<int>) this._linkObjectTypes) || !this.AddTechGroupLink(linkingObjectDialog.SelectedLinkedObjectInfo))
      return;
    this.DoLoadData();
  }

  /// <summary>
  /// Добавление изделия в групповой объект (точнее привязка единичных объектов)
  /// </summary>
  /// <param name="procRoute2ArtList">Словарь содержащий информацию об объектах для привязки единичных и об изделии для него</param>
  private bool AddTechGroupLink(
    IDictionary<ObjInfoItem, ObjInfoItem> linkedObjectInfoCache)
  {
    if (linkedObjectInfoCache == null || linkedObjectInfoCache.Count == 0)
      return false;
    IList<CategoryValue> categoryValueList = (IList<CategoryValue>) new List<CategoryValue>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int linkObjectType in (IEnumerable<int>) this._linkObjectTypes)
        TechCardUtils.CheckRelationApplicability(this._objTypeID, linkObjectType, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._objID);
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
      sessionKeeper.Session.StartLogHistory();
      TechcardClientUtils.StartCreateRelations(new ObjInfoItem(dbObject1), sessionKeeper.Session);
      try
      {
        foreach (KeyValuePair<ObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<ObjInfoItem, ObjInfoItem>>) linkedObjectInfoCache)
        {
          ObjInfoItem linkedObjectInfoItem = keyValuePair.Key;
          ObjInfoItem artObjInfo = keyValuePair.Value;
          objectCollection.ObjectTypeID = this._linkObjectTypes.FirstOrDefault<int>((System.Func<int, bool>) (item =>
          {
            IMSApplicability applicability = MetaDataHelper.GetApplicability(linkedObjectInfoItem.ObjTypeID, item, TechCardConsts.RelTypes.TechRelationID);
            return (applicability != null ? (int) applicability.ApplicabilityMode : -1) != -1;
          }));
          IDBObject dbObject2 = (IDBObject) null;
          IMSApplicability applicability1 = MetaDataHelper.GetApplicability(linkedObjectInfoItem.ObjTypeID != -1 ? linkedObjectInfoItem.ObjTypeID : TechCardConsts.ObjectTypes.MarshrObrabID, objectCollection.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID);
          bool flag = applicability1 != null && applicability1.IsContent;
          if (flag || TechCardParamsHelper.TechParams.ProcessRoute.AutoCheckIn && MetaDataHelper.IsObjectTypeChildOf(linkedObjectInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.MarshrObrabID))
          {
            dbObject2 = sessionKeeper.Session.GetObject(linkedObjectInfoItem.ObjectID);
            if (flag && (dbObject2.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject2.ObjectModifyMode == ObjectModifyModes.CreateVersion) && dbObject2.CheckoutBy == 0L)
            {
              dbObject2 = dbObject2.CheckOut();
              linkedObjectInfoItem.ObjectID = dbObject2.ObjectID;
            }
          }
          IDBObject dbObject3 = (IDBObject) null;
          try
          {
            dbObject3 = objectCollection.Create();
            dbObject3.Attributes.Assign(dbObject1.Attributes);
            dbObject3.Attributes.AddAttribute(TechCardConsts.AttributeTypes.GtpContextAttrID, false, new object[1]
            {
              (object) true
            });
            TechProcGroupUtils.RenameEtpProcess(dbObject3, dbObject1, artObjInfo, linkedObjectInfoItem, sessionKeeper.Session);
            TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechLinkGTPObjRelationID, sessionKeeper.Session, dbObject1, dbObject3);
            if (sessionKeeper.Session.GetRelation(linkedObjectInfoItem.ObjectID, dbObject3.ObjectID, TechCardConsts.RelTypes.TechRelationID, true) == null)
              relationCollection.Create(linkedObjectInfoItem.ObjectID, dbObject3.ObjectID);
            if (dbObject2 != null)
            {
              dbObject2.CheckIn();
              linkedObjectInfoItem.ObjectID = dbObject2.ObjectID;
            }
            dbObject3.CommitCreation(false);
            categoryValueList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) sessionKeeper.Session.GetModificationsHistoryList());
          }
          catch
          {
            if (dbObject3 != null)
            {
              try
              {
                dbObject3.Delete(0L);
              }
              catch (Exception ex)
              {
              }
            }
            throw;
          }
        }
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
    }
    if (categoryValueList.Any<CategoryValue>())
    {
      NotificationQueue notificationQueue = new NotificationQueue();
      foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents(categoryValueList))
        notificationQueue.QueueEvent(notificationEvent);
      notificationQueue.FlushQueue();
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ChangeTechGroupLink()
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
    ISelectedItems selectedItems = this.techTreeViewArt2Mo.SelectedItems;
    IDBTypedObjectID articleTypedObjectId;
    IDBTypedObjectID typedObjectId;
    if (selectedItems == null || selectedItems.Count == sc_19635.ssp_techcard_19636(1371062455) || !this.GetCurrentTypedObjectID(out typedObjectId) || !this._linkObjectTypes.Contains(typedObjectId.ObjectType) || !this.GetCurrentArticleTypeObjectID(out articleTypedObjectId))
      return;
    TechProcGroupSelectLinkingObjectDialog linkingObjectDialog = new TechProcGroupSelectLinkingObjectDialog()
    {
      LinkedObjInfoItems = (IList<ObjInfoItem>) this._relObjInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => this._linkObjectTypes.Contains(item.PartInfo.ObjTypeID))).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo)).ToList<ObjInfoItem>()
    };
    if (!linkingObjectDialog.ShowDialog((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(articleTypedObjectId.ObjectID, articleTypedObjectId.ObjectType)
    }, (IEnumerable<int>) this._linkObjectTypes))
      return;
    ObjInfoItem objInfoItem1 = linkingObjectDialog.SelectedLinkedObjectInfo.Keys.FirstOrDefault<ObjInfoItem>();
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem1))
      return;
    RelObjInfoItem relObjInfoItem = this._relObjInfoItems.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => item.PartInfo.ObjectID == typedObjectId.ObjectID)) ?? throw new Exception($"Не найден контекст связи для объекта (ObjectId = {typedObjectId.ObjectID})");
    ObjInfoItem projInfo = relObjInfoItem.ProjInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relObjInfoItem.RelationID, true);
      if (relation == null)
        return;
      ObjInfoItem[] objInfoItemArray = new ObjInfoItem[2]
      {
        projInfo,
        objInfoItem1
      };
      List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>(2);
      foreach (ObjInfoItem objInfoItem2 in objInfoItemArray)
      {
        IMSApplicability applicability = MetaDataHelper.GetApplicability(objInfoItem2.ObjTypeID != -1 ? objInfoItem2.ObjTypeID : TechCardConsts.ObjectTypes.MarshrObrabID, typedObjectId.ObjectType, TechCardConsts.RelTypes.TechRelationID);
        if ((applicability == null ? 0 : (applicability.IsContent ? 1 : 0)) != 0)
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(objInfoItem2.ObjectID);
          if ((dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject1.ObjectModifyMode == ObjectModifyModes.CreateVersion) && dbObject1.CheckoutBy == 0L)
          {
            IDBObject dbObject2 = dbObject1.CheckOut();
            objInfoItem2.ObjectID = dbObject2.ObjectID;
            objInfoItemList.Add(objInfoItem2);
          }
        }
      }
      relation.ProjID = objInfoItem1.ObjectID;
      foreach (ObjInfoItem objInfoItem3 in objInfoItemList)
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objInfoItem3.ObjectID, false);
        if (objectActualCopy != null)
        {
          if (objectActualCopy.CheckoutBy == sessionKeeper.Session.UserID)
            objectActualCopy.CheckIn();
          objInfoItem3.ObjectID = objectActualCopy.ObjectID;
        }
      }
    }
    this.DoLoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  private void DeleteTechGroupLink()
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
    ISelectedItems selectedItems = this.techTreeViewArt2Mo.SelectedItems;
    if (selectedItems == null || selectedItems.Count == sc_19635.ssp_techcard_19637(166016714))
      return;
    List<IDBTypedObjectID> source1 = new List<IDBTypedObjectID>();
    foreach (NavigatorTreeNode selectedNode in this.techTreeViewArt2Mo.SelectedNodes)
      source1.AddRange(selectedNode.GetDescendantsAndSelf(true).Select<NavigatorTreeNode, IDBTypedObjectID>((System.Func<NavigatorTreeNode, IDBTypedObjectID>) (node => node.GetData<IDBTypedObjectID>(node.NodeID))).Where<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (typedItem => this._linkObjectTypes.Contains(typedItem.ObjectType))));
    if (!source1.Any<IDBTypedObjectID>())
      return;
    HashSet<long> etpTypedObjectHashSet = source1.Select<IDBTypedObjectID, long>((System.Func<IDBTypedObjectID, long>) (item => item.ObjectID)).ToHashSet<long>();
    RelObjInfoItem[] array = this._relObjInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => etpTypedObjectHashSet.Contains(item.PartInfo.ObjectID))).ToArray<RelObjInfoItem>();
    if (!((IEnumerable<RelObjInfoItem>) array).Any<RelObjInfoItem>())
      return;
    List<ObjInfoItem> source2 = new List<ObjInfoItem>();
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (RelObjInfoItem relObjInfoItem in array)
      {
        ObjInfoItem partInfo = relObjInfoItem.PartInfo;
        ObjInfoItem projInfo = relObjInfoItem.ProjInfo;
        IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(partInfo.ObjectID, false);
        if (objectActualCopy1 != null)
        {
          if (objectActualCopy1.ObjectID != relObjInfoItem.PartInfo.ObjectID)
            relObjInfoItem.PartInfo.ObjectID = objectActualCopy1.ObjectID;
          source2.Add(relObjInfoItem.PartInfo);
          IMSApplicability applicability = MetaDataHelper.GetApplicability(projInfo.ObjTypeID != -1 ? projInfo.ObjTypeID : TechCardConsts.ObjectTypes.MarshrObrabID, partInfo.ObjTypeID, TechCardConsts.RelTypes.TechRelationID);
          if ((applicability == null ? 0 : (applicability.IsContent ? 1 : 0)) != 0)
          {
            IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(projInfo.ObjectID, true);
            if (objectActualCopy2 != null && (objectActualCopy2.ObjectModifyMode == ObjectModifyModes.Checkout || objectActualCopy2.ObjectModifyMode == ObjectModifyModes.CreateVersion) && objectActualCopy2.CheckoutBy == 0L)
            {
              IDBObject dbObject = objectActualCopy2.CheckOut();
              projInfo.ObjectID = dbObject.ObjectID;
              objInfoItemList.Add(projInfo);
            }
          }
        }
      }
    }
    try
    {
      DeleteCommand deleteCommand = new DeleteCommand();
      deleteCommand.DeleteOptions = DeleteAnalyzerOptions.None;
      deleteCommand.Init(Intermech.Navigator.ContextMenu.ObjectExtensions.GetItems(source2.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)).ToArray<long>()), this.techTreeViewArt2Mo.Services, (object) null);
      deleteCommand.Execute();
    }
    finally
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (ObjInfoItem objInfoItem in objInfoItemList)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objInfoItem.ObjectID, false);
          if (objectActualCopy != null && objectActualCopy.CheckoutBy != 0L)
            objectActualCopy.CheckIn();
        }
      }
      this.DoLoadData();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ProcRouteLinkMode()
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
    ISelectedItems selectedItems = this.techTreeViewArt2Mo.SelectedItems;
    IDBTypedObjectID articleTypedObjectId;
    if (selectedItems == null || selectedItems.Count == 0 || !this.GetCurrentTypedObjectID(out IDBTypedObjectID _) || !this.GetCurrentArticleTypeObjectID(out articleTypedObjectId))
      return;
    TechProcGroupLinkArt2ObjDialog linkArt2ObjDialog = new TechProcGroupLinkArt2ObjDialog();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      linkArt2ObjDialog.Text += $" ({TechCardConsts.Utils.GetObjectString(articleTypedObjectId.ObjectID, sessionKeeper.Session)})";
    NavigatorTreeNode parent = this.techTreeViewArt2Mo.FocusedNode.Parent;
    IDBTypedObjectID data = parent.GetData<IDBTypedObjectID>(parent.NodeID);
    linkArt2ObjDialog.ShowDialog(new ObjInfoItem(data.ObjectID, data.ObjectType), new ObjInfoItem(this._objID, this._objTypeID));
  }

  /// <summary>
  /// 
  /// </summary>
  private void OpenTechProc()
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
    ISelectedItems selectedItems = this.techTreeViewArt2Mo.SelectedItems;
    IDBTypedObjectID typedObjectId;
    if (selectedItems == null || selectedItems.Count == sc_19635.ssp_techcard_19638(683305337) || !this.GetCurrentTypedObjectID(out typedObjectId))
      return;
    TechCardClientConst.OpenObjectInNewWindow(typedObjectId.ObjectID);
  }

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitResources()
  {
    base.InitResources();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this._imageIndex = service != null ? service.ImageIndex(TechCardBaseGroupArtView.IconImageName) : -1;
    this._backgroundWorker = new BackgroundWorker()
    {
      WorkerReportsProgress = true,
      WorkerSupportsCancellation = true
    };
    this._backgroundWorker.DoWork += new DoWorkEventHandler(this.bw_DoWork);
    this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
  }

  /// <summary>Инициализация сервисов</summary>
  protected override void ReleaseResources()
  {
    base.ReleaseResources();
    if (this._backgroundWorker == null)
      return;
    this._backgroundWorker.Dispose();
    this._backgroundWorker = (BackgroundWorker) null;
  }

  /// <summary>Выполнить де-инициализацию сервисов закладки</summary>
  protected override void ReleaseServices()
  {
    base.ReleaseServices();
    if (this._backgroundWorker != null && this._backgroundWorker.WorkerSupportsCancellation && this._backgroundWorker.IsBusy)
      this._backgroundWorker.CancelAsync();
    StatusPopup.Hide((Control) this.techTreeViewArt2Mo);
  }

  /// <summary>Инициализация контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeComponent();
    base.InitializeCustomControls();
    this.InitializeTreeView();
    this.InitializeContextMenu();
    this.pnButtons.Visible = false;
  }

  /// <summary>Инициализация сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_280");
  }

  /// <summary>Загрузка информации</summary>
  protected override void LoadData()
  {
    this.DoLoadObjInfo();
    this.CheckObjectEditableMode();
    this.DoLoadData();
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  /// <param name="sendNotifications"></param>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    base.SaveData(sendNotifications);
  }

  /// <summary>Загрузка настроек</summary>
  protected override void LoadSettings()
  {
    base.LoadSettings();
    string name = this.GetType().ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name);
    if (this.techTreeViewArt2Mo == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.techTreeViewArt2Mo);
  }

  /// <summary>Сохранение настроек</summary>
  protected override void SaveSettings()
  {
    base.SaveSettings();
    string name = this.GetType().ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.techTreeViewArt2Mo);
  }

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  /// <summary>Can modifying flag</summary>
  public override bool CanModify => base.CanModify && this._canEdit;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void contextMenuBarItem1_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.UpdateContextCommands();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiRefresh_Click(object sender, EventArgs e) => this.LoadData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiExpandTree_Click(object sender, EventArgs e)
  {
    if (this.techTreeViewArt2Mo.RootNode == null || !this.techTreeViewArt2Mo.RootNode.HasChildren)
      return;
    ((INavigatorTreeViewClientService) ServicesManager.GetService(typeof (INavigatorTreeViewClientService))).ExpandAll(this.techTreeViewArt2Mo.RootNode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiCollapseTree_Click(object sender, EventArgs e)
  {
    if (this.techTreeViewArt2Mo?.RootNode == null || !(this.techTreeViewArt2Mo.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.CollapseNode(true);
    rootNode.ExpandNode(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiCommonButton_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem))
      return;
    this.NavigatorContextMenuInvoke(menuButtonItem.CommandName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiOpenInNewWindow_Click(object sender, EventArgs e)
  {
    IDBTypedObjectID typedObjectId;
    if (!(sender is MenuButtonItem menuButtonItem) || this.NavigatorContextMenuInvoke(menuButtonItem.CommandName) || !this.GetCurrentTypedObjectID(out typedObjectId))
      return;
    TechCardClientConst.OpenObjectInNewWindow(typedObjectId.ObjectID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiAddTechGroupLink_Click(object sender, EventArgs e) => this.AddTechGroupLink();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiChangeTechGroupLink_Click(object sender, EventArgs e)
  {
    this.ChangeTechGroupLink();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiDeleteTechGroupLink_Click(object sender, EventArgs e)
  {
    this.DeleteTechGroupLink();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiOpenLinkedObject_Click(object sender, EventArgs e) => this.OpenTechProc();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiProcRouteLinkMode_Click(object sender, EventArgs e) => this.ProcRouteLinkMode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    StatusPopup.Hide((Control) this.techTreeViewArt2Mo);
    if (e.Cancelled || e.Error != null)
      return;
    this._dataLoaded = true;
    this.UpdateTreeViewData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bw_DoWork(object sender, DoWorkEventArgs e)
  {
    if (!(sender is BackgroundWorker backgroundWorker))
      return;
    if (backgroundWorker.CancellationPending)
    {
      e.Cancel = true;
    }
    else
    {
      List<ObjInfoItem> objects = new List<ObjInfoItem>();
      List<int> articleObjectTypeIds = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        List<int> list = this._linkObjectTypes.ToList<int>();
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(ObjInfoDbScheme.GetSourceTableColumns())
        {
          new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0)
        };
        ConditionStructure[] conditions1 = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false)
        };
        CompositionLoadingParams loadingParams1 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
        {
          new ObjInfoItem(this._objID, this._objTypeID)
        }, (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechLinkGTPObjRelationID
        }, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<ConditionStructure>) conditions1, true, false, 1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
        DataTable source1 = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams1);
        new ObjInfoDbScheme().ParseItems(source1 != null ? (IEnumerable<DataRow>) source1.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objects);
        if (backgroundWorker.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        List<int> collection = new List<int>();
        List<int> intList = new List<int>();
        collection.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.MarshrObrabID));
        collection.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
        foreach (int linkObjectType in (IEnumerable<int>) this._linkObjectTypes)
          collection.AddRange(MetaDataHelper.GetObjectTypeParentApplicabilities(linkObjectType).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.ApplicabilityMode != ApplicabilityModes.Disabled && item.RelationTypeID == TechCardConsts.RelTypes.TechRelationID)).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.InObjectType)));
        intList.AddRange((IEnumerable<int>) list);
        intList.AddRange((IEnumerable<int>) collection);
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
        intList.AddRange((IEnumerable<int>) childrenIdRecursive);
        ConditionStructure[] conditions2 = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
        };
        columnDescriptorList.Clear();
        columnDescriptorList.AddRange(RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns());
        columnDescriptorList.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProductionObjectUIDAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        CompositionLoadingParams loadingParams2 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) objects, (IEnumerable<int>) null, (IEnumerable<int>) collection.ToArray(), (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<ConditionStructure>) conditions2, false, false, -1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
        DataTable source2 = service.LoadComplexCompositions((object) sessionKeeper.Session, loadingParams2);
        if (source2 == null)
          return;
        if (backgroundWorker.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        HashSet<string> stringSet = new HashSet<string>(source2.Rows.Count);
        int columnIndex = source2.Columns.IndexOf(TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid.ToString());
        foreach (DataRow row in source2.Select())
        {
          string stringValue = DataSetProcessor.GetStringValue(row, columnIndex, string.Empty);
          if (!string.IsNullOrEmpty(stringValue))
          {
            if (stringSet.Contains(stringValue))
              source2.Rows.Remove(row);
            else
              stringSet.Add(stringValue);
          }
        }
        new RelObjInfoDbScheme<ObjInfoItem>(false).ParseInfoItems(sessionKeeper.Session, (IEnumerable<DataRow>) source2.AsEnumerable(), (ICollection<RelObjInfoItem>) this._relObjInfoItems);
      }
      Dictionary<ObjInfoItem, List<RelObjInfoItem>> dictionary = this._relObjInfoItems.GroupBy<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (relObjInfoItem => relObjInfoItem.PartInfo)).ToDictionary<IGrouping<ObjInfoItem, RelObjInfoItem>, ObjInfoItem, List<RelObjInfoItem>>((System.Func<IGrouping<ObjInfoItem, RelObjInfoItem>, ObjInfoItem>) (group => group.Key), (System.Func<IGrouping<ObjInfoItem, RelObjInfoItem>, List<RelObjInfoItem>>) (group => group.ToList<RelObjInfoItem>()));
      foreach (ObjInfoItem objInfoItem1 in objects)
      {
        List<ObjInfoItem> source3 = new List<ObjInfoItem>();
        List<ObjInfoItem> source4 = new List<ObjInfoItem>()
        {
          objInfoItem1
        };
        List<ObjInfoItem> source5 = new List<ObjInfoItem>();
        HashSet<ObjInfoItem> allProObjInfoItems = new HashSet<ObjInfoItem>();
        while (source4.Any<ObjInfoItem>())
        {
          source5.Clear();
          foreach (ObjInfoItem key in source4)
          {
            allProObjInfoItems.Add(key);
            List<RelObjInfoItem> source6;
            if (dictionary.TryGetValue(key, out source6))
              source5.AddRange(source6.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)));
          }
          source3.AddRange(source5.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => articleObjectTypeIds.Contains(item.ObjTypeID))));
          if (!source3.Any<ObjInfoItem>())
          {
            source4.Clear();
            source4.AddRange(source5.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => !allProObjInfoItems.Contains(item))));
          }
          else
            break;
        }
        if (source3.Any<ObjInfoItem>())
        {
          foreach (ObjInfoItem objInfoItem2 in source3)
          {
            if (!this._artObjectInfoList.Contains(objInfoItem2))
              this._artObjectInfoList.Add(objInfoItem2);
          }
        }
      }
    }
  }

  private void techTreeViewArt2Mo_Resize(object sender, EventArgs e)
  {
    if (this._backgroundWorker == null || !this._backgroundWorker.IsBusy)
      return;
    StatusPopup.Hide((Control) this.techTreeViewArt2Mo);
    StatusPopup.Show(ResourceHolder.LoadingImage, LocalizationHolder.rm.GetString("TechCard.Client_481"), (Control) this.techTreeViewArt2Mo);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechCardBaseGroupArtView));
    this.toolBarTop = new Intermech.Bars.ToolBar();
    this.techTreeViewArt2Mo = new TechCardNavTreeViewControl();
    this.menuBar1 = new MenuBar();
    this.contextMenuBarItem1 = new ContextMenuBarItem();
    this.mbiRefresh = new MenuButtonItem();
    this.mbiOpenInNewWindow = new MenuButtonItem();
    this.mbiProperty = new MenuButtonItem();
    this.mbiSearch = new MenuButtonItem();
    this.mbiAddTechGroupLink = new MenuButtonItem();
    this.mbiChangeTechGroupLink = new MenuButtonItem();
    this.mbiDeleteTechGroupLink = new MenuButtonItem();
    this.mbiOpenLinkedObject = new MenuButtonItem();
    this.mbiProcRouteLinkMode = new MenuButtonItem();
    this.mbiExpandTree = new MenuButtonItem();
    this.mbiCollapseTree = new MenuButtonItem();
    this.mbiSetupColumns = new MenuButtonItem();
    this.pnButtons.SuspendLayout();
    this.techTreeViewArt2Mo.BeginInit();
    this.SuspendLayout();
    this.pnButtons.Location = new Point(2, 378);
    this.pnButtons.Size = new Size(527, 40);
    this.pnButtons.Visible = false;
    this.btApply.Location = new Point(880, 7);
    this.btCancel.Location = new Point(1007, 7);
    this.toolBarTop.FullMenus = true;
    this.toolBarTop.Guid = new Guid("23d2f349-f5dc-4e8a-aadd-bca49151fb1e");
    this.toolBarTop.Hidden = false;
    this.toolBarTop.Location = new Point(2, 2);
    this.toolBarTop.Name = "toolBarTop";
    this.toolBarTop.Size = new Size(527, 18);
    this.toolBarTop.TabIndex = 2;
    this.toolBarTop.Text = "toolBarToop";
    this.techTreeViewArt2Mo.AllowDrop = true;
    this.techTreeViewArt2Mo.AllowMultiSelect = false;
    this.techTreeViewArt2Mo.AllowUserPinnedColumns = false;
    this.techTreeViewArt2Mo.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("techTreeViewArt2Mo.CheckedNodesStates");
    this.techTreeViewArt2Mo.CheckoutMode = TechCheckoutMode.Auto;
    this.techTreeViewArt2Mo.CheckRootNode = false;
    this.techTreeViewArt2Mo.DisableCheckedOutColumn = true;
    this.techTreeViewArt2Mo.DisableIMContextMenu = true;
    this.techTreeViewArt2Mo.DisableKeyUpEvents = true;
    this.techTreeViewArt2Mo.Dock = DockStyle.Fill;
    this.techTreeViewArt2Mo.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.techTreeViewArt2Mo.LineStyle = LineStyle.Dot;
    this.techTreeViewArt2Mo.Location = new Point(2, 20);
    this.techTreeViewArt2Mo.Name = "techTreeViewArt2Mo";
    this.techTreeViewArt2Mo.ContextMenuBarItem = this.contextMenuBarItem1;
    this.techTreeViewArt2Mo.RowEvenStyle.WordWrap = false;
    this.techTreeViewArt2Mo.RowOddStyle.WordWrap = false;
    this.techTreeViewArt2Mo.RowSelectedStyle.WordWrap = false;
    this.techTreeViewArt2Mo.RowStyle.BorderColor = SystemColors.Control;
    this.techTreeViewArt2Mo.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.techTreeViewArt2Mo.RowStyle.BorderWidth = 1;
    this.techTreeViewArt2Mo.RowStyle.WordWrap = false;
    this.techTreeViewArt2Mo.SelectBeforeEdit = true;
    this.techTreeViewArt2Mo.ShowRootRow = false;
    this.techTreeViewArt2Mo.Size = new Size(527, 398);
    this.techTreeViewArt2Mo.SuppressErrorMessages = true;
    this.techTreeViewArt2Mo.TabIndex = 1;
    this.techTreeViewArt2Mo.Tag = (object) " ";
    this.techTreeViewArt2Mo.Resize += new EventHandler(this.techTreeViewArt2Mo_Resize);
    this.menuBar1.Guid = new Guid("4287165a-32c8-49f9-a71f-0696e541cb31");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem1
    });
    this.menuBar1.Location = new Point(2, 20);
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    this.menuBar1.Size = new Size(527, 26);
    this.menuBar1.TabIndex = 4;
    this.menuBar1.Text = "menuBar1";
    this.menuBar1.Visible = false;
    this.contextMenuBarItem1.CommandName = "contextMenuBarItem";
    this.contextMenuBarItem1.Items.AddRange(new ToolbarItemBase[12]
    {
      (ToolbarItemBase) this.mbiRefresh,
      (ToolbarItemBase) this.mbiOpenInNewWindow,
      (ToolbarItemBase) this.mbiProperty,
      (ToolbarItemBase) this.mbiSearch,
      (ToolbarItemBase) this.mbiAddTechGroupLink,
      (ToolbarItemBase) this.mbiChangeTechGroupLink,
      (ToolbarItemBase) this.mbiDeleteTechGroupLink,
      (ToolbarItemBase) this.mbiOpenLinkedObject,
      (ToolbarItemBase) this.mbiProcRouteLinkMode,
      (ToolbarItemBase) this.mbiExpandTree,
      (ToolbarItemBase) this.mbiCollapseTree,
      (ToolbarItemBase) this.mbiSetupColumns
    });
    this.contextMenuBarItem1.ShowText = true;
    this.contextMenuBarItem1.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem1_BeforePopup);
    this.mbiRefresh.CommandName = "Refresh";
    this.mbiRefresh.Shortcut = Shortcut.CtrlR;
    this.mbiRefresh.ShowText = true;
    this.mbiRefresh.Text = "Обновить";
    this.mbiRefresh.Click += new EventHandler(this.mbiRefresh_Click);
    this.mbiOpenInNewWindow.CommandName = "OpenInNewWindow";
    this.mbiOpenInNewWindow.ShowText = true;
    this.mbiOpenInNewWindow.Text = "Открыть в новом окне";
    this.mbiOpenInNewWindow.Click += new EventHandler(this.mbiOpenInNewWindow_Click);
    this.mbiProperty.CommandName = "ParametersCard";
    this.mbiProperty.Shortcut = Shortcut.F4;
    this.mbiProperty.ShowText = true;
    this.mbiProperty.Text = "Свойства (Карточка)";
    this.mbiProperty.Click += new EventHandler(this.mbiCommonButton_Click);
    this.mbiSearch.CommandName = "SeekInTree";
    this.mbiSearch.Shortcut = Shortcut.CtrlF;
    this.mbiSearch.ShowText = true;
    this.mbiSearch.Text = "Найти в списке";
    this.mbiSearch.Click += new EventHandler(this.mbiCommonButton_Click);
    this.mbiAddTechGroupLink.BeginGroup = true;
    this.mbiAddTechGroupLink.CommandName = "AddTechGroupLink";
    this.mbiAddTechGroupLink.Shortcut = Shortcut.CtrlIns;
    this.mbiAddTechGroupLink.ShowText = true;
    this.mbiAddTechGroupLink.Text = "Добавить";
    this.mbiAddTechGroupLink.Click += new EventHandler(this.mbiAddTechGroupLink_Click);
    this.mbiChangeTechGroupLink.CommandName = "ChangeTechGroupLink";
    this.mbiChangeTechGroupLink.ShowText = true;
    this.mbiChangeTechGroupLink.Text = "Изменить";
    this.mbiChangeTechGroupLink.Click += new EventHandler(this.mbiChangeTechGroupLink_Click);
    this.mbiDeleteTechGroupLink.CommandName = "DeleteTechGroupLink";
    this.mbiDeleteTechGroupLink.Shortcut = Shortcut.CtrlDel;
    this.mbiDeleteTechGroupLink.ShowText = true;
    this.mbiDeleteTechGroupLink.Text = "Удалить";
    this.mbiDeleteTechGroupLink.Click += new EventHandler(this.mbiDeleteTechGroupLink_Click);
    this.mbiOpenLinkedObject.BeginGroup = true;
    this.mbiOpenLinkedObject.CommandName = "OpenLinkedObject";
    this.mbiOpenLinkedObject.ShowText = true;
    this.mbiOpenLinkedObject.Text = "Открыть единичный объект";
    this.mbiOpenLinkedObject.Click += new EventHandler(this.mbiOpenLinkedObject_Click);
    this.mbiProcRouteLinkMode.CommandName = "ProcRouteLinkMode";
    this.mbiProcRouteLinkMode.ShowText = true;
    this.mbiProcRouteLinkMode.Text = "Режим привязки";
    this.mbiProcRouteLinkMode.Click += new EventHandler(this.mbiProcRouteLinkMode_Click);
    this.mbiExpandTree.BeginGroup = true;
    this.mbiExpandTree.CommandName = "ExpandNode";
    this.mbiExpandTree.Shortcut = Shortcut.AltDownArrow;
    this.mbiExpandTree.ShowText = true;
    this.mbiExpandTree.Text = "Развернуть все";
    this.mbiExpandTree.Click += new EventHandler(this.mbiExpandTree_Click);
    this.mbiCollapseTree.CommandName = "CollapseNode";
    this.mbiCollapseTree.ShowText = true;
    this.mbiCollapseTree.Text = "Свернуть все";
    this.mbiCollapseTree.Click += new EventHandler(this.mbiCollapseTree_Click);
    this.mbiSetupColumns.BeginGroup = true;
    this.mbiSetupColumns.CommandName = "SetupColumns";
    this.mbiSetupColumns.ShowText = true;
    this.mbiSetupColumns.Text = "Настройка отображения ...";
    this.mbiSetupColumns.Click += new EventHandler(this.mbiCommonButton_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.techTreeViewArt2Mo);
    this.Controls.Add((Control) this.toolBarTop);
    this.Name = nameof (TechCardBaseGroupArtView);
    this.Size = new Size(531, 420);
    this.Controls.SetChildIndex((Control) this.toolBarTop, 0);
    this.Controls.SetChildIndex((Control) this.techTreeViewArt2Mo, 0);
    this.Controls.SetChildIndex((Control) this.menuBar1, 0);
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.pnButtons.ResumeLayout(false);
    this.techTreeViewArt2Mo.EndInit();
    this.ResumeLayout(false);
  }
}
