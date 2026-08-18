// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteListControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>Контрол списка маршрутов обработки</summary>
public class ProcRouteListControl : UserControl
{
  private ContextMenuStrip cmsProcRoute;
  private ToolStripMenuItem tsmiProcRouteAdd;
  private ToolStripMenuItem tsmiProcRouteEdit;
  private ToolStripMenuItem tsmiProcRouteDelete;
  private ToolStripSeparator tsmiProcRouteSep;
  private ToolStripMenuItem tsmiProcRouteDefault;
  private ToolStripSeparator tsmiProcRouteSep1;
  private ToolStripMenuItem tsmiProcRouteExpandAll;
  private ToolStripMenuItem tsmiProcRouteCollapseAll;
  private ToolStripMenuItem tsmiProcRouteCheckIn;
  private ToolStripMenuItem tsmiProcRouteUndoChanges;
  private ToolStripMenuItem tsmiProcRouteCheckOut;
  private ToolStripSeparator tsmiProcRouteSep3;
  private ToolStripMenuItem tsmiSelectAll;
  private ToolStripMenuItem tsmiClearAll;
  private ToolStripMenuItem tsmiInvertAll;
  private ToolStripSeparator tsmiProcRouteSep2;
  private IContainer components;
  /// <summary>Ид. текущего пользователя</summary>
  private long _currentUserId;
  /// <summary>Multi-select mode</summary>
  protected internal bool _multiSelect;
  /// <summary>Признак уведомления навигатора об изменениях</summary>
  private bool _notifyNavigator = true;
  /// <summary>Category guid for root descriptor</summary>
  private Guid _rootCategoryGuid = Guid.Empty;
  /// <summary>Category id for root descriptor</summary>
  private int _rootCategoryId;
  /// <summary>Ид. версий изделия</summary>
  private List<long> _artObjList;
  /// <summary>Ид. версий маршрутов обработки</summary>
  private readonly List<long> _procRouteList;
  /// <summary>Кэш шагов ЖЦ</summary>
  private IObjectLCStepsCache _cache;
  /// <summary>TechCard custom navigator tree view</summary>
  protected internal TechCardNavTreeViewControl _tolcProcRouteList;
  /// <summary>Панель с кнопками</summary>
  public Panel pnlButtons;
  /// <summary>Кнопка "Применить"</summary>
  public Button btnApply;
  /// <summary>Кнопка "Отмена"</summary>
  public Button btnCancel;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcRouteListControl));
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this._tolcProcRouteList = new TechCardNavTreeViewControl();
    this.cmsProcRoute = new ContextMenuStrip(this.components);
    this.tsmiProcRouteAdd = new ToolStripMenuItem();
    this.tsmiProcRouteEdit = new ToolStripMenuItem();
    this.tsmiProcRouteDelete = new ToolStripMenuItem();
    this.tsmiProcRouteSep = new ToolStripSeparator();
    this.tsmiProcRouteDefault = new ToolStripMenuItem();
    this.tsmiProcRouteSep1 = new ToolStripSeparator();
    this.tsmiProcRouteCheckOut = new ToolStripMenuItem();
    this.tsmiProcRouteUndoChanges = new ToolStripMenuItem();
    this.tsmiProcRouteCheckIn = new ToolStripMenuItem();
    this.tsmiProcRouteSep3 = new ToolStripSeparator();
    this.tsmiSelectAll = new ToolStripMenuItem();
    this.tsmiClearAll = new ToolStripMenuItem();
    this.tsmiInvertAll = new ToolStripMenuItem();
    this.tsmiProcRouteSep2 = new ToolStripSeparator();
    this.tsmiProcRouteExpandAll = new ToolStripMenuItem();
    this.tsmiProcRouteCollapseAll = new ToolStripMenuItem();
    this.pnlButtons.SuspendLayout();
    this._tolcProcRouteList.BeginInit();
    this.cmsProcRoute.SuspendLayout();
    this.SuspendLayout();
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this._tolcProcRouteList.AllowDrop = true;
    this._tolcProcRouteList.AllowMultiSelect = false;
    this._tolcProcRouteList.AllowUserPinnedColumns = false;
    this._tolcProcRouteList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this._tolcProcRouteList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("_tolcProcRouteList.CheckedNodesStates");
    this._tolcProcRouteList.CheckoutMode = TechCheckoutMode.Manual;
    this._tolcProcRouteList.CheckRootNode = false;
    this._tolcProcRouteList.ContextMenuBarItem = (ContextMenuBarItem) null;
    this._tolcProcRouteList.ContextMenuStrip = this.cmsProcRoute;
    this._tolcProcRouteList.DisableCheckedOutColumn = true;
    this._tolcProcRouteList.DisableIMContextMenu = true;
    this._tolcProcRouteList.DisableKeyDownEvents = true;
    this._tolcProcRouteList.DisableKeyUpEvents = true;
    this._tolcProcRouteList.DisablePacketsReading = false;
    componentResourceManager.ApplyResources((object) this._tolcProcRouteList, "_tolcProcRouteList");
    this._tolcProcRouteList.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("_tolcProcRouteList.HeaderStyle.HorzAlignment");
    this._tolcProcRouteList.ImageList = (ImageList) null;
    this._tolcProcRouteList.LineStyle = LineStyle.Dot;
    this._tolcProcRouteList.Name = "_tolcProcRouteList";
    this._tolcProcRouteList.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("_tolcProcRouteList.RowEvenStyle.WordWrap");
    this._tolcProcRouteList.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("_tolcProcRouteList.RowOddStyle.WordWrap");
    this._tolcProcRouteList.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("_tolcProcRouteList.RowSelectedStyle.WordWrap");
    this._tolcProcRouteList.RowStyle.BorderColor = SystemColors.Control;
    this._tolcProcRouteList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._tolcProcRouteList.RowStyle.BorderWidth = 1;
    this._tolcProcRouteList.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("_tolcProcRouteList.RowStyle.WordWrap");
    this._tolcProcRouteList.SelectBeforeEdit = true;
    this._tolcProcRouteList.ShowRootRow = false;
    this._tolcProcRouteList.SuppressErrorMessages = true;
    this._tolcProcRouteList.Tag = (object) " ";
    this._tolcProcRouteList.AfterCreateNode += new EventHandler<NodeEventArgs>(this._tolcProcRouteList_AfterCreateNode);
    this._tolcProcRouteList.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this._tolcProcRouteList_CheckStateChanging);
    this._tolcProcRouteList.CheckStateChanged += new EventHandler<NodeEventArgs>(this._tolcProcRouteList_CheckStateChanged);
    this.cmsProcRoute.Items.AddRange(new ToolStripItem[16 /*0x10*/]
    {
      (ToolStripItem) this.tsmiProcRouteAdd,
      (ToolStripItem) this.tsmiProcRouteEdit,
      (ToolStripItem) this.tsmiProcRouteDelete,
      (ToolStripItem) this.tsmiProcRouteSep,
      (ToolStripItem) this.tsmiProcRouteDefault,
      (ToolStripItem) this.tsmiProcRouteSep1,
      (ToolStripItem) this.tsmiProcRouteCheckOut,
      (ToolStripItem) this.tsmiProcRouteUndoChanges,
      (ToolStripItem) this.tsmiProcRouteCheckIn,
      (ToolStripItem) this.tsmiProcRouteSep3,
      (ToolStripItem) this.tsmiSelectAll,
      (ToolStripItem) this.tsmiClearAll,
      (ToolStripItem) this.tsmiInvertAll,
      (ToolStripItem) this.tsmiProcRouteSep2,
      (ToolStripItem) this.tsmiProcRouteExpandAll,
      (ToolStripItem) this.tsmiProcRouteCollapseAll
    });
    this.cmsProcRoute.Name = "cmsProcRoute";
    componentResourceManager.ApplyResources((object) this.cmsProcRoute, "cmsProcRoute");
    this.cmsProcRoute.Opening += new CancelEventHandler(this.cmsProcRoute_Opening);
    this.tsmiProcRouteAdd.Name = "tsmiProcRouteAdd";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteAdd, "tsmiProcRouteAdd");
    this.tsmiProcRouteAdd.Click += new EventHandler(this.tsmiProcRouteAdd_Click);
    this.tsmiProcRouteEdit.Name = "tsmiProcRouteEdit";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteEdit, "tsmiProcRouteEdit");
    this.tsmiProcRouteEdit.Click += new EventHandler(this.tsmiProcRouteEdit_Click);
    this.tsmiProcRouteDelete.Name = "tsmiProcRouteDelete";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteDelete, "tsmiProcRouteDelete");
    this.tsmiProcRouteDelete.Click += new EventHandler(this.tsmiProcRouteDelete_Click);
    this.tsmiProcRouteSep.Name = "tsmiProcRouteSep";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteSep, "tsmiProcRouteSep");
    this.tsmiProcRouteDefault.Name = "tsmiProcRouteDefault";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteDefault, "tsmiProcRouteDefault");
    this.tsmiProcRouteDefault.Click += new EventHandler(this.tsmiProcRouteDefault_Click);
    this.tsmiProcRouteSep1.Name = "tsmiProcRouteSep1";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteSep1, "tsmiProcRouteSep1");
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteCheckOut, "tsmiProcRouteCheckOut");
    this.tsmiProcRouteCheckOut.Name = "tsmiProcRouteCheckOut";
    this.tsmiProcRouteCheckOut.Click += new EventHandler(this.tsmiProcRouteCheckOut_Click);
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteUndoChanges, "tsmiProcRouteUndoChanges");
    this.tsmiProcRouteUndoChanges.Name = "tsmiProcRouteUndoChanges";
    this.tsmiProcRouteUndoChanges.Click += new EventHandler(this.tsmiProcRouteUndoChanges_Click);
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteCheckIn, "tsmiProcRouteCheckIn");
    this.tsmiProcRouteCheckIn.Name = "tsmiProcRouteCheckIn";
    this.tsmiProcRouteCheckIn.Click += new EventHandler(this.tsmiProcRouteCheckIn_Click);
    this.tsmiProcRouteSep3.Name = "tsmiProcRouteSep3";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteSep3, "tsmiProcRouteSep3");
    this.tsmiSelectAll.Name = "tsmiSelectAll";
    componentResourceManager.ApplyResources((object) this.tsmiSelectAll, "tsmiSelectAll");
    this.tsmiSelectAll.Click += new EventHandler(this.tsmiSelectAll_Click);
    this.tsmiClearAll.Name = "tsmiClearAll";
    componentResourceManager.ApplyResources((object) this.tsmiClearAll, "tsmiClearAll");
    this.tsmiClearAll.Click += new EventHandler(this.tsmiClearAll_Click);
    this.tsmiInvertAll.Name = "tsmiInvertAll";
    componentResourceManager.ApplyResources((object) this.tsmiInvertAll, "tsmiInvertAll");
    this.tsmiInvertAll.Click += new EventHandler(this.tsmiInvertAll_Click);
    this.tsmiProcRouteSep2.Name = "tsmiProcRouteSep2";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteSep2, "tsmiProcRouteSep2");
    this.tsmiProcRouteExpandAll.Name = "tsmiProcRouteExpandAll";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteExpandAll, "tsmiProcRouteExpandAll");
    this.tsmiProcRouteExpandAll.Click += new EventHandler(this.tsmiProcRouteExpandAll_Click);
    this.tsmiProcRouteCollapseAll.Name = "tsmiProcRouteCollapseAll";
    componentResourceManager.ApplyResources((object) this.tsmiProcRouteCollapseAll, "tsmiProcRouteCollapseAll");
    this.tsmiProcRouteCollapseAll.Click += new EventHandler(this.tsmiProcRouteCollapseAll_Click);
    this.Controls.Add((Control) this._tolcProcRouteList);
    this.Controls.Add((Control) this.pnlButtons);
    this.Name = nameof (ProcRouteListControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.pnlButtons.ResumeLayout(false);
    this._tolcProcRouteList.EndInit();
    this.cmsProcRoute.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Call create node event</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DoCreateCustomNodeEvent(object sender, NodeEventArgs e)
  {
    EventHandler<NodeEventArgs> createCustomNodeEvent = this.CreateCustomNodeEvent;
    if (createCustomNodeEvent == null)
      return;
    createCustomNodeEvent(sender, e);
  }

  /// <summary>Обновление команд контекстного меню</summary>
  protected virtual void UpdateContextCommands()
  {
    IDBTypedObjectID typedObjId;
    int num1 = this.GetCurrentProcRoute(out typedObjId) ? 1 : 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (num1 != 0)
    {
      flag1 = true;
      IFocusedItem focusedItem = this._tolcProcRouteList.FocusedItem;
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      IDBLifecycleStepInfo lcStep = focusedItem.GetItemData(typeof (IDBLCStepID)) is IDBLCStepID itemData1 ? service.GetLCStep(itemData1.LCStepID) : (IDBLifecycleStepInfo) null;
      if (lcStep != null)
      {
        switch (lcStep.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
          case ObjectModifyModes.CreateVersion:
            if (focusedItem.GetItemData(typeof (IDBCheckedOutByID)) is IDBCheckedOutByID itemData2)
            {
              flag1 = flag2 = typedObjId.ObjectID < 0L && itemData2.CheckedOutBy == this._currentUserId;
              flag3 = typedObjId.ObjectID > 0L && itemData2.CheckedOutBy == 0L;
              break;
            }
            break;
          case ObjectModifyModes.CantModify:
            flag1 = false;
            break;
        }
      }
      if (flag1)
        flag1 = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ObjectExtensions.GetItems(typedObjId.Value), (System.IServiceProvider) new ServiceContainer()).Contains("ParametersCard");
    }
    this.tsmiProcRouteEdit.Enabled = this.tsmiProcRouteEdit.Visible = flag1;
    this.tsmiProcRouteDelete.Enabled = this.tsmiProcRouteDelete.Visible = flag1;
    this.tsmiProcRouteDefault.Enabled = this.tsmiProcRouteDefault.Visible = flag1;
    this.tsmiProcRouteCheckIn.Enabled = this.tsmiProcRouteCheckIn.Visible = flag2;
    this.tsmiProcRouteUndoChanges.Enabled = this.tsmiProcRouteUndoChanges.Visible = flag2;
    this.tsmiProcRouteCheckOut.Enabled = this.tsmiProcRouteCheckOut.Visible = flag3;
    this.tsmiProcRouteSep1.Visible = flag1;
    this.tsmiProcRouteSep3.Visible = flag3 | flag2;
    bool flag4 = this._tolcProcRouteList.RootNode?.Children != null && this._tolcProcRouteList.RootNode.Children.Count > 0;
    this.tsmiProcRouteExpandAll.Enabled = this.tsmiProcRouteCollapseAll.Enabled = flag4;
    ToolStripSeparator tsmiProcRouteSep2 = this.tsmiProcRouteSep2;
    ToolStripMenuItem tsmiSelectAll1 = this.tsmiSelectAll;
    ToolStripMenuItem tsmiSelectAll2 = this.tsmiSelectAll;
    ToolStripMenuItem tsmiClearAll1 = this.tsmiClearAll;
    ToolStripMenuItem tsmiClearAll2 = this.tsmiClearAll;
    ToolStripMenuItem tsmiInvertAll = this.tsmiInvertAll;
    bool flag5;
    this.tsmiInvertAll.Visible = flag5 = flag4 && this.MultiObjMode;
    int num2;
    bool flag6 = (num2 = flag5 ? 1 : 0) != 0;
    tsmiInvertAll.Enabled = num2 != 0;
    int num3;
    bool flag7 = (num3 = flag6 ? 1 : 0) != 0;
    tsmiClearAll2.Visible = num3 != 0;
    int num4;
    bool flag8 = (num4 = flag7 ? 1 : 0) != 0;
    tsmiClearAll1.Enabled = num4 != 0;
    int num5;
    bool flag9 = (num5 = flag8 ? 1 : 0) != 0;
    tsmiSelectAll2.Visible = num5 != 0;
    int num6;
    bool flag10 = (num6 = flag9 ? 1 : 0) != 0;
    tsmiSelectAll1.Enabled = num6 != 0;
    int num7 = flag10 ? 1 : 0;
    tsmiProcRouteSep2.Visible = num7 != 0;
  }

  /// <summary>Обновление кнопок диалога</summary>
  protected virtual void UpdateButtons()
  {
    Dictionary<long, long> procRoute2ArtIds = this.ProcRoute2ArtIDs;
    this.btnApply.Enabled = procRoute2ArtIds != null && procRoute2ArtIds.Count > 0;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.UnInitializeServices();
      this.UnregisterCategory();
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Инициализация сервисов</summary>
  protected void InitializeServices()
  {
    this._tolcProcRouteList.Services = (System.IServiceProvider) new ServiceContainer();
    this._cache = CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache;
  }

  /// <summary>Де-инициализация сервисов</summary>
  protected void UnInitializeServices()
  {
    if (this._tolcProcRouteList != null)
      this._tolcProcRouteList.Services = (System.IServiceProvider) null;
    this._cache = (IObjectLCStepsCache) null;
  }

  /// <summary>Регистрация категории</summary>
  private void RegisterCategory()
  {
    this._rootCategoryGuid = Guid.NewGuid();
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._rootCategoryId = service.Register(this._rootCategoryGuid);
  }

  /// <summary>Раз-регистрация категории</summary>
  private void UnregisterCategory()
  {
    ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false)?.Unregister(this._rootCategoryId);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.RegisterCategory();
    this.InitializeServices();
    this._tolcProcRouteList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(this._rootCategoryId, TechCardConsts.ObjectTypes.ProcRoutingID, "", (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service != null)
      columns.Add(service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID));
    this._tolcProcRouteList.SetColumns(columns, descriptor);
  }

  /// <summary>Create new proc route</summary>
  /// <returns></returns>
  protected virtual bool ProcRouteAdd()
  {
    long currentArticle = this.GetCurrentArticle();
    if (currentArticle == 0L)
      return false;
    long objectByTypeDialog = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, true).CreateObjectByTypeDialog(TechCardConsts.ObjectTypes.ProcRoutingID, new ObjectRelationLink[1]
    {
      new ObjectRelationLink(currentArticle, TechCardConsts.RelTypes.TechRelationID)
    });
    return objectByTypeDialog != 0L && objectByTypeDialog != -1L;
  }

  /// <summary>Edit current proc route</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteEdit(ref long objectId)
  {
    bool flag1 = false;
    long objectID = objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null || dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
        return false;
      if (dbObject.ObjectModifyMode != ObjectModifyModes.Checkout)
      {
        if (dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          goto label_10;
      }
      if (dbObject.CheckoutBy == 0L)
      {
        flag1 = true;
        objectID = dbObject.CheckOut().ObjectID;
      }
    }
label_10:
    bool flag2 = false;
    ISelectedItems items = ObjectExtensions.GetItems(objectID);
    ServiceContainer viewServices1 = new ServiceContainer();
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    if (commandsTable.Contains("ParametersCard"))
    {
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("ParametersCard", commandsTable, (System.IServiceProvider) viewServices1);
      flag2 = true;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null)
        return false;
      if (flag2)
      {
        long num = objectId;
        if (flag1)
          objectId = dbObject.ObjectID;
        NotificationEventArgs[] args = new NotificationEventArgs[1];
        DBObjectsEventArgs objectsEventArgs;
        if (!flag1)
          objectsEventArgs = (DBObjectsEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
          {
            objectId
          }, (IList<long>) new long[1]{ num });
        else
          objectsEventArgs = new DBObjectsEventArgs("ObjectsChanged", objectId);
        args[0] = (NotificationEventArgs) objectsEventArgs;
        this.DoNotifyNavigator(args);
      }
      else if (flag1)
        dbObject.CancelChanges();
    }
    return flag2;
  }

  /// <summary>Delete current proc route</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteDelete(long objectId)
  {
    if (objectId == 0L)
      return false;
    DeleteCommand deleteCommand1 = new DeleteCommand();
    deleteCommand1.DeleteOptions = DeleteAnalyzerOptions.Defaults;
    DeleteCommand deleteCommand2 = deleteCommand1;
    deleteCommand2.Init(ObjectExtensions.GetItems(objectId), TechCardClient.ServiceProvider, (object) null);
    deleteCommand2.Execute();
    return true;
  }

  /// <summary>Set current proc route as default</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteDefault(long objectId)
  {
    if (objectId == 0L)
      return false;
    List<long> objectIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeValues[] valuesList = new AttributeValues[1];
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrGuid);
      object defaultAttrValue = ProcRouteHelper.RouteProcDefaultAttrValue;
      DataTable objectData = DataHelper.GetObjectData(TechCardConsts.ObjectTypes.ProcRoutingID, sessionKeeper.Session, (IEnumerable<ConditionStructure>) new List<ConditionStructure>()
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) this._procRouteList.ToArray(), LogicalOperators.NONE, 0, false)
      }.ToArray(), (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      }.ToArray(), (IEnumerable<long>) this._procRouteList);
      if (objectData == null || objectData.Rows.Count == 0)
        return false;
      foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        object obj1 = row[1];
        valuesList[0] = int64 == objectId ? new AttributeValues(attributeTypeId, defaultAttrValue) : new AttributeValues(attributeTypeId, (object) DBNull.Value);
        object obj2 = valuesList[0].Values[0];
        if (obj1 != obj2)
        {
          objectIDs.Add(int64);
          IDBObject dbObject = sessionKeeper.Session.GetObject(int64, false);
          if (dbObject != null)
          {
            bool flag = false;
            if ((dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion || dbObject.ObjectModifyMode == ObjectModifyModes.Checkout) && dbObject.CheckoutBy == 0L)
            {
              flag = true;
              dbObject = dbObject.CheckOut();
            }
            dbObject.SetAttributesValues(valuesList);
            if (flag)
              dbObject.CheckIn();
          }
        }
      }
    }
    if (objectIDs.Count > 0)
      this.DoNotifyNavigator(new NotificationEventArgs[1]
      {
        (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs)
      });
    return true;
  }

  /// <summary>Check current proc route</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteCheckIn(long objectId)
  {
    if (objectId == 0L || objectId > 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject == null || dbObject.ObjectModifyMode != ObjectModifyModes.Checkout && dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion || dbObject.CheckoutBy != this._currentUserId)
        return false;
      dbObject.CheckIn();
      List<long> objectIDs = new List<long>(1);
      List<long> newObjectIDs = new List<long>(1);
      objectIDs.Add(objectId);
      newObjectIDs.Add(dbObject.ObjectID);
      this.DoNotifyNavigator(new NotificationEventArgs[1]
      {
        (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs, (IList<long>) newObjectIDs)
      });
    }
    return true;
  }

  /// <summary>Check out current proc route</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteCheckOut(long objectId)
  {
    if (objectId == 0L || objectId < 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject1 == null || dbObject1.ObjectModifyMode != ObjectModifyModes.Checkout && dbObject1.ObjectModifyMode != ObjectModifyModes.CreateVersion || dbObject1.CheckoutBy != 0L)
        return false;
      IDBObject dbObject2 = dbObject1.CheckOut();
      List<long> objectIDs = new List<long>(1);
      List<long> newObjectIDs = new List<long>(1);
      objectIDs.Add(objectId);
      newObjectIDs.Add(dbObject2.ObjectID);
      this.DoNotifyNavigator(new NotificationEventArgs[1]
      {
        (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs)
      });
    }
    return true;
  }

  /// <summary>Undo current proc route</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool ProcRouteUndoChanges(long objectId)
  {
    if (objectId == 0L || objectId > 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject == null || dbObject.ObjectModifyMode != ObjectModifyModes.Checkout && dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion || dbObject.CheckoutBy != this._currentUserId)
        return false;
      dbObject.CancelChanges();
      List<long> objectIDs = new List<long>(1);
      List<long> newObjectIDs = new List<long>(1);
      objectIDs.Add(objectId);
      newObjectIDs.Add(dbObject.ObjectID);
      this.DoNotifyNavigator(new NotificationEventArgs[1]
      {
        (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs, (IList<long>) newObjectIDs)
      });
    }
    return true;
  }

  /// <summary>Get current proc route object id</summary>
  /// <param name="typedObjId"></param>
  /// <returns></returns>
  protected virtual bool GetCurrentProcRoute(out IDBTypedObjectID typedObjId)
  {
    typedObjId = (IDBTypedObjectID) null;
    IFocusedItem focusedItem = this._tolcProcRouteList.FocusedItem;
    if (focusedItem == null)
      return false;
    typedObjId = focusedItem.GetItemData(typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (typedObjId != null && MetaDataHelper.IsObjectTypeChildOf(typedObjId.ObjectType, TechCardConsts.ObjectTypes.ProcRoutingID))
      return true;
    typedObjId = (IDBTypedObjectID) null;
    return false;
  }

  /// <summary>Get current proc route object id</summary>
  /// <returns></returns>
  protected virtual long GetCurrentProcRoute()
  {
    IDBTypedObjectID typedObjId;
    this.GetCurrentProcRoute(out typedObjId);
    return typedObjId == null ? 0L : typedObjId.ObjectID;
  }

  /// <summary>Get current article</summary>
  /// <returns></returns>
  protected virtual long GetCurrentArticle()
  {
    long currentArticle = 0;
    if (!this.MultiObjMode)
      return this._artObjList[0];
    IFocusedItem focusedItem = this._tolcProcRouteList.FocusedItem;
    if (focusedItem?.GetItemData(typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      if (childrenIdRecursive.Contains(itemData.ObjectType))
        currentArticle = itemData.ObjectID;
      else if (focusedItem.GetParentData(typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData && childrenIdRecursive.Contains(parentData.ObjectType))
        currentArticle = parentData.ObjectID;
    }
    return currentArticle;
  }

  /// <summary>Уведомление навигатора</summary>
  /// <param name="args">Список аргументов</param>
  protected virtual bool DoNotifyNavigator(NotificationEventArgs[] args)
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

  /// <summary>Конструктор</summary>
  /// <param name="artObjList">Ид. версий изделия</param>
  /// <param name="multiSelect"></param>
  public ProcRouteListControl(List<long> artObjList, bool multiSelect)
  {
    this._artObjList = new List<long>((IEnumerable<long>) artObjList);
    this._multiSelect = multiSelect;
    this.InitializeComponent();
    if (!this.DesignMode)
      this.InitializeCustomControls();
    this._procRouteList = new List<long>();
  }

  /// <summary>Загрузка из базы</summary>
  public virtual void LoadData()
  {
    this._procRouteList.Clear();
    int procRoutingId = TechCardConsts.ObjectTypes.ProcRoutingID;
    int techRelationId = TechCardConsts.RelTypes.TechRelationID;
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) new int[1]
      {
        procRoutingId
      }, LogicalOperators.NONE, 0, false)
    };
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = this._tolcProcRouteList.CheckedNodesStates;
    IDescriptor rootDescriptor;
    if (!this.MultiObjMode)
    {
      string caption = LocalizationHolder.rm.GetString("TechCard.Client_209");
      long currentArticle = this.GetCurrentArticle();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._currentUserId = sessionKeeper.Session.UserID;
        DataTable childSostavData = DataHelper.GetChildSostavData(currentArticle, sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          techRelationId
        }, false, (IEnumerable<ConditionStructure>) conditions);
        if (childSostavData != null && childSostavData.Rows.Count > 0)
        {
          for (int index = 0; index <= childSostavData.Rows.Count - 1; ++index)
          {
            long int64 = Convert.ToInt64(childSostavData.Rows[index]["F_OBJECT_ID"]);
            if (int64 != 0L)
              this._procRouteList.Add(int64);
          }
        }
        caption += TechCardConsts.Utils.GetObjectString(currentArticle, sessionKeeper.Session);
      }
      rootDescriptor = (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, procRoutingId, caption, (IList) this._procRouteList);
    }
    else
    {
      List<TechCardUtils.SostavSortedTreeItem> childSostavTree;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._currentUserId = sessionKeeper.Session.UserID;
        childSostavTree = TechCardUtils.GetChildSostavTree((IList<long>) this._artObjList, sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          techRelationId
        }, false, conditions, (Dictionary<string, ColumnDescriptor>) null);
      }
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in childSostavTree)
      {
        if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.ObjectTypeID, procRoutingId))
          this._procRouteList.Add(sostavSortedTreeItem.PartID);
      }
      string caption = this.Text = LocalizationHolder.rm.GetString("TechCard.Client_209");
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (long artObj in this._artObjList)
      {
        TechCompositionSostavTreeFilter sostavTreeFilter = new TechCompositionSostavTreeFilter(RelatedObjectsRole.Composition, (IList<TechCardUtils.SostavSortedTreeItem>) childSostavTree);
        IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryId, 0, artObj, procRoutingId, techRelationId, caption, RelatedObjectsRole.Composition, (ITechCompositionFilter) sostavTreeFilter);
        descriptors.Add(descriptor);
      }
      rootDescriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ProcRoutingID, caption, descriptors);
    }
    this._tolcProcRouteList.Build(rootDescriptor);
    this._tolcProcRouteList.CheckedNodesStates = checkedNodesStates;
    if (this.MultiObjMode && this._tolcProcRouteList.RootNode?.Children != null)
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._tolcProcRouteList.RootNode.Children)
        child.Expanded = true;
    }
    this.UpdateButtons();
  }

  /// <summary>Get attribute value flag for default proc route</summary>
  /// <returns></returns>
  [Obsolete("Use ProcRouteThroughHelper instead", true)]
  public static object RouteProcDefaultAttrValue() => ProcRouteHelper.RouteProcDefaultAttrValue;

  /// <summary>Ид. версий изделия</summary>
  public List<long> ArtObjList => this._artObjList;

  /// <summary>
  /// Список ид. версий выбранных маршрутов обработки и изделий к который они относятся
  /// Key = ид. версии МО, валуе - ид. версии изделия
  /// </summary>
  public Dictionary<long, long> ProcRoute2ArtIDs
  {
    get
    {
      Dictionary<long, long> procRoute2ArtIds = new Dictionary<long, long>();
      NavigatorTreeNode[] checkedNodes = this._tolcProcRouteList.CheckedNodes;
      int procRoutingId = TechCardConsts.ObjectTypes.ProcRoutingID;
      if (checkedNodes != null)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
        foreach (NavigatorTreeNode treeNode in checkedNodes)
        {
          long objectId1;
          int objTypeId1;
          if (treeNode != null && treeNode.CheckState == CheckState.Checked && TechcardClientControlsUtils.GetObjectInfo(treeNode, out objectId1, out objTypeId1) && objTypeId1 == procRoutingId)
          {
            long key = objectId1;
            long objectId2;
            if (this._tolcProcRouteList.RootDescriptor is TechObjectListDescriptor)
            {
              objectId2 = this._artObjList[0];
            }
            else
            {
              int objTypeId2;
              if (TechcardClientControlsUtils.GetObjectInfo(treeNode.Parent, out objectId2, out objTypeId2))
                objectId2 = childrenIdRecursive.Contains(objTypeId2) ? objectId2 : 0L;
            }
            procRoute2ArtIds.Add(key, objectId2);
          }
        }
      }
      return procRoute2ArtIds;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool MultiSelect => this._multiSelect;

  /// <summary>Multi object mode</summary>
  public bool MultiObjMode => this._artObjList.Count > 1;

  /// <summary>Режим уведомления навигатора об изменениях</summary>
  public bool NotifyNavigator
  {
    get => this._notifyNavigator;
    set => this._notifyNavigator = value;
  }

  /// <summary>Событие на создания элемента списка маршрутов</summary>
  public event EventHandler<NodeEventArgs> CreateCustomNodeEvent;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsProcRoute_Opening(object sender, CancelEventArgs e)
  {
    this.UpdateContextCommands();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteAdd_Click(object sender, EventArgs e)
  {
    if (!this.ProcRouteAdd())
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteEdit_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteEdit(ref currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteDelete_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteDelete(currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteDefault_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteDefault(currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteExpandAll_Click(object sender, EventArgs e)
  {
    if (!(this._tolcProcRouteList?.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.ExpandNode(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteCollapseAll_Click(object sender, EventArgs e)
  {
    if (!(this._tolcProcRouteList?.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.CollapseNode(true);
    rootNode.ExpandNode(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteCheckOut_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteCheckOut(currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteUndoChanges_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteUndoChanges(currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiProcRouteCheckIn_Click(object sender, EventArgs e)
  {
    long currentProcRoute = this.GetCurrentProcRoute();
    if (currentProcRoute == 0L || !this.ProcRouteCheckIn(currentProcRoute))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiSelectAll_Click(object sender, EventArgs e)
  {
    if (this._tolcProcRouteList?.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._tolcProcRouteList.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Select, true);
    }
    this._tolcProcRouteList.UpdateRows();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClearAll_Click(object sender, EventArgs e)
  {
    if (this._tolcProcRouteList?.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._tolcProcRouteList.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Clear, true);
    }
    this._tolcProcRouteList.UpdateRows();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInvertAll_Click(object sender, EventArgs e)
  {
    if (this._tolcProcRouteList?.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._tolcProcRouteList.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Invert, true);
    }
    this._tolcProcRouteList.UpdateRows();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _tolcProcRouteList_AfterCreateNode(object sender, NodeEventArgs e)
  {
    this.DoCreateCustomNodeEvent(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _tolcProcRouteList_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (e == null)
      return;
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _tolcProcRouteList_CheckStateChanging(object sender, CheckStateEventArgs e)
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
      if (this.MultiSelect || e.NewValue != CheckState.Checked)
        return;
      bool flag = false;
      foreach (NavigatorTreeNode checkedNode in this._tolcProcRouteList.CheckedNodes)
      {
        if (checkedNode != e.Node && checkedNode.CheckState == CheckState.Checked && checkedNode is TechcardNavTreeNode techcardNavTreeNode)
        {
          techcardNavTreeNode.SetCheckStateInternal(CheckState.Unchecked);
          flag = true;
        }
      }
      int num = flag ? 1 : 0;
    }
  }
}
