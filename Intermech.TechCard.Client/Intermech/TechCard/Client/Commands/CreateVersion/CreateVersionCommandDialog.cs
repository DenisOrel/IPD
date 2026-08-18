// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateVersion.CreateVersionCommandDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.ECO.Client;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.EditingContexts;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.NavigatorSupport.NodeFactories;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateVersion;

/// <summary>
/// Диалог создание версии технологического объекта
/// (выбор / создание соотв. извещения)
/// </summary>
internal class CreateVersionCommandDialog : Form
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IDictionary<long, bool> _ecoEditableCache = (IDictionary<long, bool>) new Dictionary<long, bool>();
  /// <summary>Ид. версии ИИ</summary>
  private ObjInfoItem _ecoObjInfo;
  /// <summary>
  /// Ид. версии техн. объекта, соотв. текущему focused узлу
  /// </summary>
  private ObjInfoItem _techСurObjInfo;
  /// <summary>Ид. версии техн. объекта</summary>
  private ObjInfoItem _techOrgObjInfo;
  /// <summary>
  /// Ид. версии созданной версии технологического объекта,
  /// </summary>
  private ObjInfoItem _techVerObjInfo;
  /// <summary>Признак уведомления навигатора об изменениях</summary>
  private bool _notifyNavigator = true;
  /// <summary>
  /// 
  /// </summary>
  private ServiceContainer _serviceContainer;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcTechEcoList;
  private Panel pnlButtons;
  private Button btnCancel;
  private Button btnApply;
  private Button btnEcoCreate;
  private MenuButton mbtnEcoLink;
  private ContextMenuStrip cmsLinkEcoType;
  private ToolStripMenuItem testToolStripMenuItem;
  private ToolStripMenuItem toolStripMenuItem2;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ecoObjectId"></param>
  /// <returns></returns>
  private bool GetEcoEditableMode(long ecoObjectId)
  {
    bool ecoEditableMode1;
    if (this._ecoEditableCache.TryGetValue(ecoObjectId, out ecoEditableMode1))
      return ecoEditableMode1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool ecoEditableMode2 = ServiceUtils.GetService<IEditingContextServerService>((object) sessionKeeper.Session, false).CheckEditingContextEditRights(sessionKeeper.Session.SessionGUID, ecoObjectId);
      this._ecoEditableCache[ecoObjectId] = ecoEditableMode2;
      return ecoEditableMode2;
    }
  }

  /// <summary>Обновление кнопок диалога</summary>
  private void UpdateButtons()
  {
    this.btnEcoCreate.Enabled = !ObjInfoItem.IsEmpty((ITypedInfoItem) this._techСurObjInfo);
    this.mbtnEcoLink.Enabled = this.btnApply.Enabled = !ObjInfoItem.IsEmpty((ITypedInfoItem) this._ecoObjInfo);
  }

  /// <summary>Инициализация сервисов</summary>
  private void InitializeServices()
  {
    this.tolcTechEcoList.Services = (System.IServiceProvider) (this._serviceContainer = new ServiceContainer());
  }

  /// <summary>Де-инициализация сервисов</summary>
  private void UnInitializeServices()
  {
    if (this.tolcTechEcoList != null)
      this.tolcTechEcoList.Services = (System.IServiceProvider) null;
    this._serviceContainer?.Dispose();
    this._serviceContainer = (ServiceContainer) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.InitializeServices();
    this.InitializeEcoControls();
    this.tolcTechEcoList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = "";
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, caption, (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.tolcTechEcoList.SetColumns(columns, descriptor);
    this.pnlButtons.Visible = true;
  }

  /// <summary>Initialize custom settings</summary>
  private void InitializeCustomSettings() => this.LoadSettings(true);

  /// <summary>
  /// 
  /// </summary>
  private void InitializeEcoControls()
  {
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, true);
      intList.AddRange((IEnumerable<int>) objectTypeCollection.GetVisibleList());
      intList.Sort();
    }
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"));
    this.cmsLinkEcoType.Items.Clear();
    this.cmsLinkEcoType.ImageList = service?.BigImageList;
    List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>();
    foreach (int objTypeID in childrenIdRecursive)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
      if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract && !MetaDataHelper.IsObjectTypeChildOf(objectType.ObjectTypeID, RevHelper.idObj_DI) && !MetaDataHelper.IsObjectTypeChildOf(objectType.ObjectTypeID, RevHelper.idObj_DPI) && intList.BinarySearch(objTypeID) >= 0)
        imsObjectTypeList.Add(objectType);
    }
    imsObjectTypeList.Sort((Comparison<IMSObjectType>) ((x, y) => string.Compare(x.ObjectName, y.ObjectName, StringComparison.Ordinal)));
    foreach (IMSObjectType imsObjectType in imsObjectTypeList)
    {
      ToolStripItem toolStripItem = this.cmsLinkEcoType.Items.Add(imsObjectType.ObjectName);
      toolStripItem.ImageIndex = service != null ? service.IndexOf(4, imsObjectType.ObjectTypeID) : -1;
      toolStripItem.Tag = (object) imsObjectType.ObjectTypeID;
      toolStripItem.Click += new EventHandler(this.cmiEcoLink_Click);
    }
    Icon icon = service?.GetIcon(4, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
    if (icon == null)
      return;
    this.Icon = icon;
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  private void LoadSettings(bool loadFormPosition)
  {
    string name = this.GetType().ToString();
    if (loadFormPosition)
      TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name);
    if (this.tolcTechEcoList == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.tolcTechEcoList);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  private void SaveSettings(bool saveFormPosition)
  {
    string name = this.GetType().ToString();
    if (saveFormPosition)
      TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.tolcTechEcoList);
  }

  /// <summary>Уведомление навигатора</summary>
  /// <param name="args">Список аргументов</param>
  protected bool DoNotifyNavigator(NotificationEventArgs[] args)
  {
    if (!this._notifyNavigator || args == null || args.Length == 0)
      return false;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service == null)
      return false;
    foreach (NotificationEventArgs e in args)
    {
      if (e != null)
        service.FireEvent((object) null, e);
    }
    return true;
  }

  /// <summary>Get selected Eco object id</summary>
  /// <param name="typedObjId"></param>
  /// <returns></returns>
  private bool GetSelectedEcoObj(out IDBTypedObjectID typedObjId)
  {
    typedObjId = (IDBTypedObjectID) null;
    ISelectedItems checkedItems = this.tolcTechEcoList.CheckedItems;
    if (checkedItems == null || checkedItems.Count == 0)
      return false;
    typedObjId = this.tolcTechEcoList.CheckedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545");
    if (typedObjId != null && MetaDataHelper.IsObjectTypeChildOf(typedObjId.ObjectType, objectTypeId))
      return true;
    typedObjId = (IDBTypedObjectID) null;
    return false;
  }

  /// <summary>Get selected proc Eco object id</summary>
  /// <returns></returns>
  private ObjInfoItem GetSelectedEcoObj()
  {
    IDBTypedObjectID typedObjId;
    this.GetSelectedEcoObj(out typedObjId);
    return typedObjId == null ? (ObjInfoItem) null : new ObjInfoItem(typedObjId.ObjectID, typedObjId.ObjectType);
  }

  /// <summary>Get tech object for selected Eco object</summary>
  /// <returns></returns>
  private ObjInfoItem GetSelectedTechObj()
  {
    ObjInfoItem selectedTechObj = (ObjInfoItem) null;
    ISelectedItems checkedItems = this.tolcTechEcoList.CheckedItems;
    if (checkedItems == null || checkedItems.Count == 0)
      return (ObjInfoItem) null;
    if (checkedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      if (TechCardConsts.Utils.IsTechcardObjectType((object) itemData.ObjectType))
      {
        selectedTechObj = new ObjInfoItem(itemData.ObjectID, itemData.ObjectType);
      }
      else
      {
        foreach (NavigatorTreeNode checkedNode in this.tolcTechEcoList.CheckedNodes)
        {
          if (checkedNode.CheckState == CheckState.Checked)
          {
            for (NavigatorTreeNode parent = checkedNode.Parent; parent != null; parent = parent.Parent)
            {
              NavigatorTreeNode navigatorTreeNode = parent;
              if (navigatorTreeNode.NodeID != null && navigatorTreeNode.NodeID.CategoryID == 1 && this.tolcTechEcoList.GetNodeHandler(parent).GetData(navigatorTreeNode.NodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data && TechCardConsts.Utils.IsTechcardObjectType((object) data.ObjectType))
              {
                selectedTechObj = (ObjInfoItem) new ObjInfoIDItem(data.ObjectID, data.ObjectType, data.ID);
                break;
              }
            }
            break;
          }
        }
      }
    }
    return selectedTechObj;
  }

  /// <summary>Get current / focused tech object</summary>
  /// <returns></returns>
  private ObjInfoItem GetFocusedTechObj()
  {
    ObjInfoIDItem focusedTechObj = (ObjInfoIDItem) null;
    IFocusedItem focusedItem = this.tolcTechEcoList.FocusedItem;
    if (focusedItem?.GetItemData(typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      if (TechCardConsts.Utils.IsTechcardObjectType((object) itemData.ObjectType))
        focusedTechObj = new ObjInfoIDItem(itemData.ObjectID, itemData.ObjectType, itemData.ID);
      else if (focusedItem.GetParentData(typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData && TechCardConsts.Utils.IsTechcardObjectType((object) parentData.ObjectType))
        focusedTechObj = new ObjInfoIDItem(parentData.ObjectID, parentData.ObjectType);
    }
    return (ObjInfoItem) focusedTechObj;
  }

  /// <summary>Создание нового ИИ</summary>
  private void CreateEcoObject()
  {
    ObjInfoItem focusedTechObj = this.GetFocusedTechObj();
    if ((TypedInfoItem) focusedTechObj == (TypedInfoItem) null)
      return;
    if (ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, false) == null)
    {
      string caption = LocalizationHolder.rm.GetString(sc_19282.ssp_techcard_19283());
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19282.ssp_techcard_19284()), (object) typeof (IObjectCreatorService)), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      ReqRevision revReq;
      int objectType1;
      List<ObjInfoIDItem> list;
      ObjInfoIDItem partObjInfo;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBObject dbObject = sessionKeeper.Session.GetObject(focusedTechObj.ObjectID);
        revReq = RevReqHelper.GetRevReq(dbObject.LCStep, dbObject.ObjectType);
        objectType1 = dbObject.ObjectType;
        int objectType2 = objectType1;
        long[] versionEx = session.GetObjectCollection(objectType2).CreateVersionEx(focusedTechObj.ObjectID);
        if (versionEx == null || versionEx.Length == 0)
          return;
        list = ((IEnumerable<long>) versionEx).Select<long, ObjInfoIDItem>((System.Func<long, ObjInfoIDItem>) (item => new ObjInfoIDItem(item))).ToList<ObjInfoIDItem>();
        partObjInfo = list[0];
        partObjInfo.ObjTypeID = objectType1;
        ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) list, sessionKeeper.Session);
      }
      bool flag = true;
      try
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(RevReqHelper.guidObj_II));
        switch (revReq)
        {
          case ReqRevision.NoRevision:
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(focusedTechObj.ObjectID);
              string caption = LocalizationHolder.rm.GetString(sc_19282.ssp_techcard_19285());
              int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19282.ssp_techcard_19286()), (object) objectInfo.Caption, (object) objectInfo.ObjectID), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
              break;
            }
        }
        ObjInfoItem objInfoItem = (ObjInfoItem) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          int count = list.Count;
          for (int index = 1; index < count; ++index)
          {
            ObjInfoIDItem objInfoIdItem = list[index];
            IDBObject dbObject = sessionKeeper.Session.GetObject(objInfoIdItem.ObjectID, false);
            if (dbObject != null)
            {
              objInfoIdItem.ObjTypeID = dbObject.ObjectType;
              if (MetaDataHelper.IsObjectTypeChildOf(objInfoIdItem.ObjTypeID, objectTypeId))
              {
                objInfoItem = (ObjInfoItem) objInfoIdItem;
                break;
              }
            }
          }
        }
        if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545"));
            ConditionStructure[] conditions = new ConditionStructure[1]
            {
              new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00348-306c-11d8-b4e9-00304f19f545")).ToArray(), LogicalOperators.NONE, 0, false)
            };
            List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree((ObjInfoItem) partObjInfo, sessionKeeper.Session, new int[1]
            {
              relationTypeId
            }, false, conditions, (Dictionary<string, ColumnDescriptor>) null);
            if (parentSostavTree != null)
            {
              if (parentSostavTree.Count != 0)
              {
                IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(parentSostavTree[0].ProjID, true);
                if (objectActualCopy != null)
                  objInfoItem = (ObjInfoItem) new ObjInfoIDItem(objectActualCopy);
              }
            }
          }
        }
        if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
        {
          if (revReq != ReqRevision.SuggestRevision && revReq != ReqRevision.SuggestRevisionOrCJ)
            return;
          this.DialogResult = DialogResult.OK;
          return;
        }
        this._ecoObjInfo = objInfoItem;
        this._techOrgObjInfo = focusedTechObj;
        this._techVerObjInfo = (ObjInfoItem) partObjInfo;
        List<long> objectIDs = new List<long>(list.Count);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (ObjInfoIDItem objInfoIdItem in list)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objInfoIdItem.ObjectID, false);
            if (dbObject != null)
            {
              if (MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, objectType1))
                TechCardClientConst.MarkObjectAsModified(dbObject);
              if (dbObject.IsCreationMode)
              {
                dbObject.CommitCreation(false);
                objInfoIdItem.ObjectID = dbObject.ObjectID;
              }
              objectIDs.Add(dbObject.ObjectID);
            }
          }
        }
        flag = false;
        INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
        if (service != null)
        {
          List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, sessionKeeper.Session);
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", (IList<long>) ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objectInfoList), (IList<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objectInfoList));
          service.FireEvent((object) null, (NotificationEventArgs) e);
        }
      }
      finally
      {
        if (flag)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            foreach (ObjInfoIDItem objInfoIdItem in list)
              sessionKeeper.Session.GetObject(objInfoIdItem.ObjectID, false)?.Delete(0L);
          }
        }
      }
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateLinkedEco()
  {
    this.CreateLinkedEco(MetaDataHelper.GetObjectTypeID("cad00349-306c-11d8-b4e9-00304f19f545"));
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateLinkedEco(int ecoObjType)
  {
    IObjectCreatorService service = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, true);
    ECOPlugin.RevObjectCreator.linkedNumber = this._ecoObjInfo.ObjectID;
    int aObjectTypeID = ecoObjType;
    OpenEditorMode openEditorMode;
    ref OpenEditorMode local = ref openEditorMode;
    long objectByTypeDialog = service.CreateObjectByTypeDialog(aObjectTypeID, out local, (IObjectCreatorParams) null);
    if (objectByTypeDialog == 0L)
      return;
    this._ecoObjInfo = new ObjInfoItem(objectByTypeDialog, ecoObjType);
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Конструктор</summary>
  public CreateVersionCommandDialog()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeCustomSettings();
  }

  /// <summary>Загрузка из базы</summary>
  public void LoadData(
    IEnumerable<ObjInfoItem> techObjList,
    IEnumerable<RelObjInfoItem> tech2EcoRelCache)
  {
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = this.tolcTechEcoList.CheckedNodesStates;
    string caption = LocalizationHolder.rm.GetString("TechCard.Client_540");
    DataTable dataTable = new DataTable();
    dataTable.Columns.Add("F_PROJ_ID", typeof (long));
    dataTable.Columns.Add(DataHelper.Consts.cnt_fld_PartObjID, typeof (long));
    dataTable.Columns.Add("F_OBJECT_TYPE", typeof (int));
    foreach (RelObjInfoItem relObjInfoItem in tech2EcoRelCache)
      dataTable.Rows.Add((object) relObjInfoItem.ProjInfo.ObjectID, (object) relObjInfoItem.PartInfo.ObjectID, (object) relObjInfoItem.PartInfo.ObjTypeID);
    this._serviceContainer.AddService(typeof (INodesFactorySupported), (object) new TechCompositionFromDataTableNodesFactorySupport(dataTable));
    this._serviceContainer.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
    ObjInfoItem objInfoItem = techObjList.FirstOrDefault<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => item.ObjTypeID != -1));
    int typeID = objInfoItem != null ? objInfoItem.ObjTypeID : TechCardConsts.ObjectTypes.TechBaseObjectID;
    this.tolcTechEcoList.Build((IDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, typeID, caption, ObjInfoHelper.GetObjectTypeCache(techObjList)));
    this.tolcTechEcoList.CheckedNodesStates = checkedNodesStates;
    if (this.tolcTechEcoList.RootNode?.Children != null)
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this.tolcTechEcoList.RootNode.Children)
        child.Expanded = true;
    }
    this.UpdateButtons();
  }

  /// <summary>Ид. версии ИИ</summary>
  public ObjInfoItem EcoObjInfo => this._ecoObjInfo;

  /// <summary>Ид. версии технологического объекта</summary>
  public ObjInfoItem TechOrgObjInfo => this._techOrgObjInfo;

  /// <summary>
  /// Ид. версии созданной версии технологического объекта / или текущего тех. объекта
  /// </summary>
  public ObjInfoItem TechVerObjInfo => this._techVerObjInfo;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmiEcoLink_Click(object sender, EventArgs e)
  {
    if (!(sender is ToolStripMenuItem toolStripMenuItem))
      return;
    this.CreateLinkedEco(Convert.ToInt32(toolStripMenuItem.Tag));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnEcoCreate_Click(object sender, EventArgs e) => this.CreateEcoObject();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnEcoLink_Click(object sender, EventArgs e) => this.CreateLinkedEco();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechCardBaseCreateVersionDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechCardBaseCreateVersionDialog_Activated(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechCardBaseCreateVersionDialog_Load(object sender, EventArgs e)
  {
    this.LoadSettings(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcTechEcoList_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (e == null)
      return;
    this._techOrgObjInfo = this._techVerObjInfo = this.GetSelectedTechObj();
    this._ecoObjInfo = this.GetSelectedEcoObj();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcTechEcoList_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if (e == null)
      return;
    this._techСurObjInfo = this.GetFocusedTechObj();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcTechEcoList_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (e.OldValue == CheckState.Indeterminate && e.OldValue != e.NewValue)
    {
      e.NewValue = e.OldValue;
      if (!(e.Node is TechcardNavTreeNode node))
        return;
      node.SetCheckStateInternal(e.OldValue);
    }
    else
    {
      if (e.NewValue != CheckState.Checked)
        return;
      foreach (NavigatorTreeNode checkedNode in this.tolcTechEcoList.CheckedNodes)
      {
        if (!checkedNode.Equals((object) e.Node) && checkedNode.CheckState == CheckState.Checked && checkedNode is TechcardNavTreeNode techcardNavTreeNode)
          techcardNavTreeNode.SetCheckStateInternal(CheckState.Unchecked);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcTechEcoList_AfterCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    if (node == null || navigatorTreeView == null || !(node is TechcardNavTreeNode techcardNavTreeNode))
      return;
    INode nodeHandler = navigatorTreeView.GetNodeHandler(node);
    if (nodeHandler == null)
      return;
    IDBTypedObjectID data = techcardNavTreeNode.NodeID is NodeID nodeId ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    if (data == null || TechCardConsts.Utils.IsTechcardObjectType((object) data.ObjectType))
      techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
    else if (!this.GetEcoEditableMode(data.ObjectID))
    {
      techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
      techcardNavTreeNode.SetCheckStateInternal(CheckState.Indeterminate);
    }
    else
      techcardNavTreeNode.SetCheckStateInternal(CheckState.Unchecked);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    this.UnInitializeServices();
    if (this.components != null)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateVersionCommandDialog));
    this.tolcTechEcoList = new TechCardNavTreeViewControl();
    this.pnlButtons = new Panel();
    this.mbtnEcoLink = new MenuButton();
    this.cmsLinkEcoType = new ContextMenuStrip(this.components);
    this.testToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripMenuItem();
    this.btnEcoCreate = new Button();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.tolcTechEcoList.BeginInit();
    this.pnlButtons.SuspendLayout();
    this.cmsLinkEcoType.SuspendLayout();
    this.SuspendLayout();
    this.tolcTechEcoList.AllowDrop = true;
    this.tolcTechEcoList.AllowMultiSelect = false;
    this.tolcTechEcoList.AllowUserPinnedColumns = false;
    this.tolcTechEcoList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.tolcTechEcoList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcTechEcoList.CheckedNodesStates");
    this.tolcTechEcoList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcTechEcoList.CheckRootNode = false;
    this.tolcTechEcoList.DisableCheckedOutColumn = true;
    this.tolcTechEcoList.DisableIMContextMenu = true;
    this.tolcTechEcoList.DisableKeyDownEvents = true;
    this.tolcTechEcoList.DisableKeyUpEvents = true;
    this.tolcTechEcoList.DisablePacketsReading = false;
    this.tolcTechEcoList.Dock = DockStyle.Fill;
    this.tolcTechEcoList.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.tolcTechEcoList.ImageList = (ImageList) null;
    this.tolcTechEcoList.LineStyle = LineStyle.Dot;
    this.tolcTechEcoList.Location = new Point(0, 0);
    this.tolcTechEcoList.Name = "tolcTechEcoList";
    this.tolcTechEcoList.RowEvenStyle.WordWrap = false;
    this.tolcTechEcoList.RowOddStyle.WordWrap = false;
    this.tolcTechEcoList.RowSelectedStyle.WordWrap = false;
    this.tolcTechEcoList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcTechEcoList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcTechEcoList.RowStyle.BorderWidth = 1;
    this.tolcTechEcoList.RowStyle.WordWrap = false;
    this.tolcTechEcoList.SelectBeforeEdit = true;
    this.tolcTechEcoList.ShowRootRow = false;
    this.tolcTechEcoList.Size = new Size(554, 227);
    this.tolcTechEcoList.SuppressErrorMessages = true;
    this.tolcTechEcoList.TabIndex = 2;
    this.tolcTechEcoList.Tag = (object) " ";
    this.tolcTechEcoList.AfterCreateNode += new EventHandler<NodeEventArgs>(this.tolcTechEcoList_AfterCreateNode);
    this.tolcTechEcoList.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.tolcTechEcoList_AfterFocusNode);
    this.tolcTechEcoList.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.tolcTechEcoList_CheckStateChanging);
    this.tolcTechEcoList.CheckStateChanged += new EventHandler<NodeEventArgs>(this.tolcTechEcoList_CheckStateChanged);
    this.pnlButtons.Controls.Add((Control) this.mbtnEcoLink);
    this.pnlButtons.Controls.Add((Control) this.btnEcoCreate);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    this.pnlButtons.Dock = DockStyle.Bottom;
    this.pnlButtons.Location = new Point(0, 227);
    this.pnlButtons.Name = "pnlButtons";
    this.pnlButtons.Size = new Size(554, 39);
    this.pnlButtons.TabIndex = 3;
    this.pnlButtons.Visible = false;
    this.mbtnEcoLink.Location = new Point(105, 6);
    this.mbtnEcoLink.Menu = this.cmsLinkEcoType;
    this.mbtnEcoLink.Name = "mbtnEcoLink";
    this.mbtnEcoLink.Size = new Size(117, 23);
    this.mbtnEcoLink.TabIndex = 4;
    this.mbtnEcoLink.Text = "Связанное ИИ";
    this.mbtnEcoLink.UseVisualStyleBackColor = true;
    this.mbtnEcoLink.Visible = false;
    this.mbtnEcoLink.Click += new EventHandler(this.btnEcoLink_Click);
    this.cmsLinkEcoType.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.testToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2
    });
    this.cmsLinkEcoType.Name = "cmsLinkEcoType";
    this.cmsLinkEcoType.Size = new Size(96 /*0x60*/, 48 /*0x30*/);
    this.testToolStripMenuItem.Name = "testToolStripMenuItem";
    this.testToolStripMenuItem.Size = new Size(95, 22);
    this.testToolStripMenuItem.Text = "Test";
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(95, 22);
    this.toolStripMenuItem2.Text = "\\";
    this.btnEcoCreate.Location = new Point(12, 6);
    this.btnEcoCreate.Name = "btnEcoCreate";
    this.btnEcoCreate.Size = new Size(87, 23);
    this.btnEcoCreate.TabIndex = 2;
    this.btnEcoCreate.Text = "Создать ИИ";
    this.btnEcoCreate.UseVisualStyleBackColor = true;
    this.btnEcoCreate.Click += new EventHandler(this.btnEcoCreate_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(470, 7);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Enabled = false;
    this.btnApply.ImeMode = ImeMode.NoControl;
    this.btnApply.Location = new Point(390, 7);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(75, 23);
    this.btnApply.TabIndex = 0;
    this.btnApply.Text = "ОК";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(554, 266);
    this.Controls.Add((Control) this.tolcTechEcoList);
    this.Controls.Add((Control) this.pnlButtons);
    this.Name = nameof (CreateVersionCommandDialog);
    this.Text = "Выберите извещение об изменении";
    this.Activated += new EventHandler(this.TechCardBaseCreateVersionDialog_Activated);
    this.FormClosed += new FormClosedEventHandler(this.TechCardBaseCreateVersionDialog_FormClosed);
    this.Load += new EventHandler(this.TechCardBaseCreateVersionDialog_Load);
    this.tolcTechEcoList.EndInit();
    this.pnlButtons.ResumeLayout(false);
    this.cmsLinkEcoType.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
