
// Type: Intermech.Client.Core.RoleConfigsEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Управление списком объектов типа "Конфигурации ролей"</summary>
public sealed class RoleConfigsEditor : UserControl
{
  /// <summary>Идентификатор настроек роли по умолчанию</summary>
  private long _defaultRoleConfigurationVersionID;
  /// <summary>Идентификатор типа объектов "Конфигурации ролей"</summary>
  private int _roleConfigurationObjectTypeID = -1;
  /// <summary>Идентификатор типа объектов "Роль"</summary>
  private int _roleObjectTypeID = -1;
  /// <summary>Идентификатор типа атрибута "Конфигурации ролей"</summary>
  private int _roleConfigurationAttributeTypeID = -10000;
  /// <summary>Корневой узел в дереве</summary>
  private List<object> _rootItem = new List<object>();
  /// <summary>
  /// Список [ID правила] =&gt; [Список ролей, которым это правило назначено]
  /// </summary>
  private Dictionary<long, List<RoleConfigsEditor.KeyRole>> _roles = new Dictionary<long, List<RoleConfigsEditor.KeyRole>>();
  /// <summary>
  /// Список правил сотрировки и отображения составов - объектов типа "Конфигурации роли"
  /// </summary>
  private List<CompositionsAutosortRule> _compositionsAutosortRules = new List<CompositionsAutosortRule>();
  private ICategoryTypeIconService _categoryTypeIconService;
  private INotificationService _notificationService;
  private IObjectCreatorService _objectCreatorService;
  private ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar toolBarRules;
  private ButtonItem _addRoleConfiguration;
  private ButtonItem _deleteButtonItem;
  private LabelItem lbSplitter;
  private ButtonItem _refreshButtonItem;
  private ImageList imagesMenus;
  private Intermech.VirtualTreeView.VirtualTreeView _roleConfigurationsTree;
  protected Column columnParentObjects;
  private ButtonItem _addRoleButtonItem;
  private ButtonItem _deleteRoleButtonItem;
  private MenuBar menuObjects;
  private ContextMenuBarItem _contextMenuBarItem;
  private MenuButtonItem _addRoleConfigurationMenuButtonItem;
  private MenuButtonItem _deleteRoleConfigurationMenuButtonItem;
  private MenuButtonItem _addRoleMenuButtonItem;
  private MenuButtonItem _deleteRoleMenuButtonItem;
  private MenuButtonItem _refreshMenuButtonItem;
  private ButtonItem _loadRoleConfigurationFromFileButtonItem;
  private ButtonItem _saveRoleConfigurationToFileButtonItem;

  public RoleConfigsEditor()
  {
    this.InitializeComponent();
    if (!(ServicesManager.GetService(typeof (BarManager)) is BarManager service))
      return;
    service.RendererChanged += new EventHandler(this.BarManager_RendererChanged);
    this.BarManager_RendererChanged((object) service, EventArgs.Empty);
  }

  public event EventHandler LoadRoleConfigurationFromFile;

  public event EventHandler SaveRoleConfigurationToFile;

  public event EventHandler CurrentRoleConfigurationChanged;

  public event EventHandler<CancelEventArgs> CurrentRoleConfigurationChanging;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long CurrentRoleConfigurationVersionID
  {
    get
    {
      CompositionsAutosortRule roleConfiguration = this.GetCurrentRoleConfiguration();
      return roleConfiguration == null ? 0L : roleConfiguration.ObjectID;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long SelectedRoleVersionID
  {
    get
    {
      return this._roleConfigurationsTree.SelectedItem is RoleConfigsEditor.KeyRole ? ((RoleConfigsEditor.KeyRole) this._roleConfigurationsTree.SelectedItem).ObjectID : 0L;
    }
  }

  /// <summary>Получить список ролей, связанных с указанным правилом</summary>
  /// <param name="ruleID">Идентификатор правила</param>
  /// <returns>Список ролей</returns>
  public List<long> GetRuleRoles(long ruleID)
  {
    List<long> ruleRoles = new List<long>();
    if (!this._roles.ContainsKey(ruleID))
      return ruleRoles;
    List<RoleConfigsEditor.KeyRole> role = this._roles[ruleID];
    for (int index = 0; index < role.Count; ++index)
    {
      if (ruleRoles.IndexOf(role[index].ObjectID) < 0)
        ruleRoles.Add(role[index].ObjectID);
    }
    return ruleRoles;
  }

  /// <summary>Выполнить инициализацию компонента</summary>
  public void Init()
  {
    this._categoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._objectCreatorService = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    if (this._notificationService != null)
    {
      this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotificationService_EventFired));
      this._notificationService.Subscribe(new NotificationEventHandler(this.NotificationService_EventFired));
    }
    if (this._objectCreatorService != null)
    {
      this._objectCreatorService.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this.ObjectCreatorService_AfterObjectCreatedEvent);
      this._objectCreatorService.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.ObjectCreatorService_AfterObjectCreatedEvent);
    }
    this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.LoadData();
    this._rootItem.Clear();
    this._rootItem.Add((object) this._compositionsAutosortRules);
    this.FillTree(true);
    this.UpdateControls();
  }

  private void BarManager_RendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBarRules.Renderer = renderer;
    this.menuObjects.Renderer = renderer;
  }

  private void LoadRoleConfigurationFromFileButtonItem_Click(object sender, EventArgs e)
  {
    this.OnLoadRoleConfigurationFromFile();
  }

  private void SaveRoleConfigurationToFileButtonItem_Click(object sender, EventArgs e)
  {
    this.OnSaveRoleConfigurationToFile();
  }

  private void AddRoleConfiguration_Click(object sender, EventArgs e)
  {
    this.AddRoleConfiguration();
  }

  private void DeleteButtonItem_Click(object sender, EventArgs e) => this.Delete();

  private void AddRoleButtonItem_Click(object sender, EventArgs e) => this.AddRole();

  private void DeleteRoleButtonItem_Click(object sender, EventArgs e) => this.DeleteRole();

  private void RefreshButtonItem_Click(object sender, EventArgs e)
  {
    object roleConfigurationsTreeSelectedItem = this._roleConfigurationsTree.SelectedItem;
    this._roleConfigurationsTree.SelectionChanged -= new EventHandler(this.RoleConfigurationsTree_SelectionChanged);
    this._roleConfigurationsTree.SelectionChanging -= new SelectionChangingHandler(this.RoleConfigurationsTree_SelectionChanging);
    try
    {
      this.Init();
      if (!(roleConfigurationsTreeSelectedItem is CompositionsAutosortRule))
        return;
      this._roleConfigurationsTree.SelectedItem = (object) this._compositionsAutosortRules.FirstOrDefault<CompositionsAutosortRule>((System.Func<CompositionsAutosortRule, bool>) (o => o.ObjectID == ((CompositionsAutosortRule) roleConfigurationsTreeSelectedItem).ObjectID));
    }
    finally
    {
      this._roleConfigurationsTree.SelectionChanged += new EventHandler(this.RoleConfigurationsTree_SelectionChanged);
      this._roleConfigurationsTree.SelectionChanging += new SelectionChangingHandler(this.RoleConfigurationsTree_SelectionChanging);
    }
  }

  private void RoleConfigurationsTree_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    e.AllowedDropLocations = RowDropLocation.AboveRow | RowDropLocation.BelowRow | RowDropLocation.OnRow;
  }

  private void RoleConfigurationsTree_GetAllowRowDrag(object sender, GetAllowRowDragEventArgs e)
  {
    e.AllowDrag = e.Row.Item is RoleConfigsEditor.KeyRole;
  }

  private void RoleConfigurationsTree_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    e.DropEffect = DragDropEffects.Move;
  }

  private void RoleConfigurationsTree_RowDrop(object sender, RowDropEventArgs e)
  {
    if (!e.Data.GetDataPresent(typeof (Row[])))
      return;
    Row row = !(e.Data.GetData(typeof (Row[])) is Row[] data) || data.Length == 0 ? (Row) null : data[0];
    if (row == null || e.Row == null || row == e.Row || e.Row.Item is CompositionsAutosortRule && (e.Row.Item as CompositionsAutosortRule).ObjectID == (row.ParentRow.Item as CompositionsAutosortRule).ObjectID)
      return;
    CompositionsAutosortRule compositionsAutosortRule = e.Row.Item as CompositionsAutosortRule;
    if (e.Row.Item is RoleConfigsEditor.KeyRole && e.Row.ParentRow != null)
      compositionsAutosortRule = e.Row.ParentRow.Item as CompositionsAutosortRule;
    if (compositionsAutosortRule == null || !(row.Item is RoleConfigsEditor.KeyRole keyRole))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(keyRole.ObjectID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(this._roleConfigurationAttributeTypeID);
      if (attributeById == null || attributeById.ReadOnly)
        return;
      this.RemoveRole(keyRole.ObjectID);
      attributeById.Value = (object) compositionsAutosortRule.ObjectID;
      this._roles[compositionsAutosortRule.ObjectID].Add(new RoleConfigsEditor.KeyRole(dbObject.ObjectID, dbObject.Caption));
      if (this._currentUserAndRole.RoleID == dbObject.ObjectID)
        this._currentUserAndRole.Rule = compositionsAutosortRule;
    }
    this.FillTree(false);
    this.UpdateControls();
  }

  private void RoleConfigurationsTree_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Item is CompositionsAutosortRule)
    {
      e.RowData.ImageSize = 32 /*0x20*/;
      e.RowData.ImageList = this._categoryTypeIconService.ImageList;
      e.RowData.ImageIndex = this._categoryTypeIconService.IndexOf(4, this._roleConfigurationObjectTypeID);
    }
    else
    {
      if (!(e.Row.Item is RoleConfigsEditor.KeyRole))
        return;
      e.RowData.ImageSize = 32 /*0x20*/;
      e.RowData.ImageList = this._categoryTypeIconService.ImageList;
      e.RowData.ImageIndex = this._categoryTypeIconService.IndexOf(4, this._roleObjectTypeID);
    }
  }

  private void RoleConfigurationsTree_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is List<object>)
      e.Children = (IList) (e.Row.Item as List<object>)[e.Row.ChildIndex];
    if (!(e.Row.Item is CompositionsAutosortRule))
      return;
    CompositionsAutosortRule compositionsAutosortRule = e.Row.Item as CompositionsAutosortRule;
    e.Children = (IList) this._roles[compositionsAutosortRule.ObjectID];
  }

  private void RoleConfigurationsTree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Item is CompositionsAutosortRule)
    {
      CompositionsAutosortRule compositionsAutosortRule = e.Row.Item as CompositionsAutosortRule;
      e.CellData.Value = (object) compositionsAutosortRule.Name;
      if (compositionsAutosortRule.ObjectID == this._defaultRoleConfigurationVersionID)
      {
        e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, new StyleDelta()
        {
          Font = new Font(e.Row.Tree.RowOddStyle.Font, FontStyle.Bold)
        });
        e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, new StyleDelta()
        {
          Font = new Font(e.Row.Tree.RowEvenStyle.Font, FontStyle.Bold)
        });
      }
    }
    if (!(e.Row.Item is RoleConfigsEditor.KeyRole))
      return;
    RoleConfigsEditor.KeyRole keyRole = e.Row.Item as RoleConfigsEditor.KeyRole;
    e.CellData.Value = (object) keyRole.Caption;
  }

  private void RoleConfigurationsTree_SelectionChanged(object sender, EventArgs e)
  {
    this.OnCurrentRoleConfigurationChanged();
    this.UpdateControls();
  }

  private void RoleConfigurationsTree_SelectionChanging(object sender, SelectionChangingEventArgs e)
  {
    e.Cancel = this.OnCurrentRoleConfigurationChanging();
    this.UpdateControls();
  }

  private void RoleConfigurationsTree_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this._contextMenuBarItem.Show((Control) this._roleConfigurationsTree, e.Location);
  }

  private void ObjectCreatorService_AfterObjectCreatedEvent(
    object sender,
    AfterObjectCreatedEventArgs ea)
  {
    if (this.IsDisposed || ea == null || ea.ObjectTypeID != this._roleConfigurationObjectTypeID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      CompositionsAutosortRule compositionsAutosortRule = new CompositionsAutosortRule();
      compositionsAutosortRule.Load(sessionKeeper.Session, ea.ObjectID, true);
      if (compositionsAutosortRule.ObjectID != ea.ObjectID)
        return;
      this._compositionsAutosortRules.Add(compositionsAutosortRule);
      List<RoleConfigsEditor.KeyRole> keyRoleList = new List<RoleConfigsEditor.KeyRole>();
      this._roles[compositionsAutosortRule.ObjectID] = keyRoleList;
    }
    this.FillTree(false);
    this.UpdateControls();
  }

  private void NotificationService_EventFired(object sender, NotificationEventArgs e)
  {
    if (!this.IsDisposed)
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || !(objectsEventArgs.EventName == "ObjectsRemoved"))
        return;
      for (int index = this._compositionsAutosortRules.Count - 1; index >= 0; --index)
      {
        if (objectsEventArgs.ObjectIDs.Contains(this._compositionsAutosortRules[index].ObjectID))
        {
          this._roles.Remove(this._compositionsAutosortRules[index].ObjectID);
          this._compositionsAutosortRules.Remove(this._compositionsAutosortRules[index]);
        }
      }
      this.FillTree(false);
      this.UpdateControls();
    }
    else
    {
      if (this._notificationService == null)
        return;
      this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotificationService_EventFired));
    }
  }

  private void OnLoadRoleConfigurationFromFile()
  {
    EventHandler configurationFromFile = this.LoadRoleConfigurationFromFile;
    if (configurationFromFile == null)
      return;
    configurationFromFile((object) this, EventArgs.Empty);
  }

  private void OnSaveRoleConfigurationToFile()
  {
    EventHandler configurationToFile = this.SaveRoleConfigurationToFile;
    if (configurationToFile == null)
      return;
    configurationToFile((object) this, EventArgs.Empty);
  }

  private void AddRoleConfiguration()
  {
    long roleSettingsObject = this.CreateRoleSettingsObject();
    if (roleSettingsObject < 0L)
      return;
    this.Init();
    for (int index = 0; index < this._compositionsAutosortRules.Count; ++index)
    {
      if (this._compositionsAutosortRules[index].ObjectID == roleSettingsObject)
        this._roleConfigurationsTree.SelectedRow = this._roleConfigurationsTree.RootRow.ChildRow((object) this._compositionsAutosortRules[index]);
    }
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", roleSettingsObject, this._roleConfigurationObjectTypeID));
  }

  private void Delete()
  {
    CompositionsAutosortRule roleConfiguration = this.GetCurrentRoleConfiguration();
    if (roleConfiguration == null || roleConfiguration.ObjectID == this._currentUserAndRole.Rule.ObjectID || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_3264.ssp_imclient_3265()), (object) roleConfiguration.Name), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(roleConfiguration.ObjectID, false)?.Delete(0L);
    this._compositionsAutosortRules.Remove(roleConfiguration);
    this._roles.Remove(roleConfiguration.ObjectID);
    if (this._currentUserAndRole.Rule.ObjectID == roleConfiguration.ObjectID)
      this._currentUserAndRole.Rule = (CompositionsAutosortRule) null;
    this.FillTree(false);
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) new List<long>()
    {
      roleConfiguration.ObjectID
    }));
  }

  private void AddRole()
  {
    CompositionsAutosortRule roleConfiguration = this.GetCurrentRoleConfiguration();
    if (roleConfiguration == null)
      return;
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_242"), LocalizationHolder.rm.GetString("Client.Core_243"), this._roleObjectTypeID, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < numArray.Length; ++index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[index]);
        IDBAttribute attributeById = dbObject.GetAttributeByID(this._roleConfigurationAttributeTypeID);
        if (attributeById != null && !attributeById.ReadOnly)
        {
          this.RemoveRole(numArray[index]);
          attributeById.Value = (object) roleConfiguration.ObjectID;
          this._roles[roleConfiguration.ObjectID].Add(new RoleConfigsEditor.KeyRole(dbObject.ObjectID, dbObject.Caption));
          if (this._currentUserAndRole.RoleID == dbObject.ObjectID)
            this._currentUserAndRole.Rule = roleConfiguration;
        }
      }
    }
    this.FillTree(false);
    this.UpdateControls();
  }

  private void DeleteRole()
  {
    if (ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID) || ObjectHelper.IsUnknownObjectVersionID(this.SelectedRoleVersionID) || this._defaultRoleConfigurationVersionID == 0L || this.CurrentRoleConfigurationVersionID == this._defaultRoleConfigurationVersionID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.SelectedRoleVersionID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(this._roleConfigurationAttributeTypeID);
      if (attributeById == null || attributeById.ReadOnly)
        return;
      this.RemoveRole(this.SelectedRoleVersionID);
      attributeById.Value = (object) this._defaultRoleConfigurationVersionID;
      this._roles[this._defaultRoleConfigurationVersionID].Add(new RoleConfigsEditor.KeyRole(dbObject.ObjectID, dbObject.Caption));
      if (this._currentUserAndRole.RoleID == dbObject.ObjectID)
        this._currentUserAndRole.Rule = (CompositionsAutosortRule) null;
    }
    this.FillTree(false);
    this.UpdateControls();
  }

  private void OnCurrentRoleConfigurationChanged()
  {
    EventHandler configurationChanged = this.CurrentRoleConfigurationChanged;
    if (configurationChanged == null)
      return;
    configurationChanged((object) this, EventArgs.Empty);
  }

  private bool OnCurrentRoleConfigurationChanging()
  {
    EventHandler<CancelEventArgs> configurationChanging = this.CurrentRoleConfigurationChanging;
    if (configurationChanging == null)
      return false;
    CancelEventArgs e = new CancelEventArgs();
    configurationChanging((object) this, e);
    return e.Cancel;
  }

  private void FillTree(bool resetDatasource)
  {
    if (resetDatasource)
      this._roleConfigurationsTree.DataSource = (object) this._rootItem;
    this._roleConfigurationsTree.UpdateRows(true);
    this._roleConfigurationsTree.FocusRow = this._roleConfigurationsTree.SelectedRow = this._roleConfigurationsTree.TopRow;
    this.UpdateControls();
  }

  /// <summary>Удалить указанную роль из всех правил</summary>
  /// <param name="roleID">Удаляемая роль</param>
  private void RemoveRole(long roleID)
  {
    foreach (KeyValuePair<long, List<RoleConfigsEditor.KeyRole>> role in this._roles)
      role.Value.Remove(new RoleConfigsEditor.KeyRole(roleID, string.Empty));
  }

  /// <summary>
  /// Получить из базы данных список ролей, которым назначен указанный объект типа "Конфигурации ролей"
  /// </summary>
  /// <param name="session">Сессия, в рамках которой происходит работа с базой данных</param>
  /// <param name="ruleObjID">Идентификатор объекта типа "Конфигурации ролей"</param>
  /// <returns>Список ролей, которым назначен указанный объект типа "Конфигурации ролей"</returns>
  private List<RoleConfigsEditor.KeyRole> LoadRoles(IUserSession session, long ruleObjID)
  {
    List<RoleConfigsEditor.KeyRole> keyRoleList = new List<RoleConfigsEditor.KeyRole>();
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
    };
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) ruleObjID, (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, columns);
    DataTable dataTable;
    try
    {
      dataTable = session.ObjectsSelect(this._roleObjectTypeID, dbRecordSetParams);
    }
    catch
    {
      dataTable = (DataTable) null;
    }
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return keyRoleList;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      DataRow row = dataTable.Rows[index];
      long int64 = Convert.ToInt64(row[0]);
      keyRoleList.Add(new RoleConfigsEditor.KeyRole(int64, row[1].ToString()));
    }
    return keyRoleList;
  }

  private void LoadData()
  {
    this._roles.Clear();
    this._compositionsAutosortRules.Clear();
    this._roleConfigurationObjectTypeID = this._roleConfigurationObjectTypeID == -1 ? MetaDataHelper.GetObjectTypeID(new Guid("cad00690-306c-11d8-b4e9-00304f19f545")) : this._roleConfigurationObjectTypeID;
    this._roleObjectTypeID = this._roleObjectTypeID == -1 ? MetaDataHelper.GetObjectTypeID(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")) : this._roleObjectTypeID;
    this._roleConfigurationAttributeTypeID = this._roleConfigurationAttributeTypeID == -10000 ? MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545") : this._roleConfigurationAttributeTypeID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._defaultRoleConfigurationVersionID == 0L)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00693-306c-11d8-b4e9-00304f19f545"), false);
        this._defaultRoleConfigurationVersionID = dbObject != null ? dbObject.ObjectID : 0L;
      }
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
        new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
      });
      DataTable dataTable1 = (DataTable) null;
      DataTable dataTable2;
      try
      {
        dataTable2 = sessionKeeper.Session.ObjectsSelect(this._roleConfigurationObjectTypeID, dbRecordSetParams);
      }
      catch
      {
        dataTable2 = (DataTable) null;
      }
      if (dataTable2 == null || dataTable2.Rows.Count <= 0)
        return;
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        DataRow row = dataTable2.Rows[index];
        long int64 = Convert.ToInt64(row[0]);
        CompositionsAutosortRule compositionsAutosortRule = new CompositionsAutosortRule(int64);
        compositionsAutosortRule.Name = DataSetProcessor.GetStringValue(row, 1, (string) null);
        if (compositionsAutosortRule.ObjectID == int64)
        {
          if (compositionsAutosortRule.ObjectID == this._defaultRoleConfigurationVersionID)
            this._compositionsAutosortRules.Insert(0, compositionsAutosortRule);
          else
            this._compositionsAutosortRules.Add(compositionsAutosortRule);
          List<RoleConfigsEditor.KeyRole> keyRoleList = new List<RoleConfigsEditor.KeyRole>();
          this._roles.Add(compositionsAutosortRule.ObjectID, keyRoleList);
        }
      }
      dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
        new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, -1),
        new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
      });
      dataTable1 = (DataTable) null;
      DataTable dataTable3;
      try
      {
        dataTable3 = sessionKeeper.Session.ObjectsSelect(this._roleObjectTypeID, dbRecordSetParams);
      }
      catch
      {
        dataTable3 = (DataTable) null;
      }
      if (dataTable3 == null || dataTable3.Rows.Count <= 0)
        return;
      for (int index = 0; index < dataTable3.Rows.Count; ++index)
      {
        DataRow row = dataTable3.Rows[index];
        long int64 = Convert.ToInt64(row[0]);
        object obj = row[1];
        long result = 0;
        if (obj == null || obj == DBNull.Value || !long.TryParse(obj.ToString(), out result))
          result = 0L;
        if (this._roles.ContainsKey(result))
          this._roles[result].Add(new RoleConfigsEditor.KeyRole(int64, row[2].ToString()));
      }
    }
  }

  private long CreateRoleSettingsObject()
  {
    this._roleConfigurationObjectTypeID = this._roleConfigurationObjectTypeID == -1 ? MetaDataHelper.GetObjectTypeID("cad00690-306c-11d8-b4e9-00304f19f545") : this._roleConfigurationObjectTypeID;
    this._roleObjectTypeID = this._roleObjectTypeID == -1 ? MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545") : this._roleObjectTypeID;
    this._roleConfigurationAttributeTypeID = this._roleConfigurationAttributeTypeID == -10000 ? MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545") : this._roleConfigurationAttributeTypeID;
    return (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).CreateObjectByTypeDialog(this._roleConfigurationObjectTypeID);
  }

  private void UpdateControls()
  {
    this._addRoleConfiguration.Enabled = this._currentUserAndRole != null && this._currentUserAndRole.IsAdmin;
    this._addRoleConfigurationMenuButtonItem.Enabled = this._addRoleConfiguration.Enabled;
    this._deleteButtonItem.Enabled = this._addRoleConfiguration.Enabled && !ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID) && this.CurrentRoleConfigurationVersionID != this._defaultRoleConfigurationVersionID && ObjectHelper.IsUnknownObjectVersionID(this.SelectedRoleVersionID) && this.CurrentRoleConfigurationVersionID != this._currentUserAndRole.Rule.ObjectID;
    this._deleteRoleConfigurationMenuButtonItem.Enabled = this._deleteButtonItem.Enabled;
    this._addRoleButtonItem.Enabled = this._addRoleConfiguration.Enabled && !ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID) && ObjectHelper.IsUnknownObjectVersionID(this.SelectedRoleVersionID);
    this._addRoleMenuButtonItem.Enabled = this._addRoleButtonItem.Enabled;
    this._deleteRoleButtonItem.Enabled = this._addRoleConfiguration.Enabled && !ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID) && !ObjectHelper.IsUnknownObjectVersionID(this.SelectedRoleVersionID) && this._defaultRoleConfigurationVersionID != 0L && this.CurrentRoleConfigurationVersionID != this._defaultRoleConfigurationVersionID;
    this._deleteRoleMenuButtonItem.Enabled = this._deleteRoleButtonItem.Enabled;
    this._refreshButtonItem.Enabled = true;
    this._refreshMenuButtonItem.Enabled = this._refreshButtonItem.Enabled;
    this._saveRoleConfigurationToFileButtonItem.Enabled = this._addRoleConfiguration.Enabled && this.SaveRoleConfigurationToFile != null && !ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID);
    this._loadRoleConfigurationFromFileButtonItem.Enabled = this._addRoleConfiguration.Enabled && this.LoadRoleConfigurationFromFile != null && !ObjectHelper.IsUnknownObjectVersionID(this.CurrentRoleConfigurationVersionID);
  }

  private CompositionsAutosortRule GetCurrentRoleConfiguration()
  {
    Row row = this._roleConfigurationsTree.SelectedRow;
    while (row != null && !(row.Item is CompositionsAutosortRule))
      row = row.ParentRow;
    return row == null ? (CompositionsAutosortRule) null : row.Item as CompositionsAutosortRule;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._notificationService != null)
        this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotificationService_EventFired));
      if (this._objectCreatorService != null)
        this._objectCreatorService.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this.ObjectCreatorService_AfterObjectCreatedEvent);
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        this.toolBarRules.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        this.menuObjects.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged -= new EventHandler(this.BarManager_RendererChanged);
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RoleConfigsEditor));
    this.toolBarRules = new Intermech.Bars.ToolBar();
    this.imagesMenus = new ImageList(this.components);
    this._loadRoleConfigurationFromFileButtonItem = new ButtonItem();
    this._saveRoleConfigurationToFileButtonItem = new ButtonItem();
    this._addRoleConfiguration = new ButtonItem();
    this._deleteButtonItem = new ButtonItem();
    this._addRoleButtonItem = new ButtonItem();
    this._deleteRoleButtonItem = new ButtonItem();
    this.lbSplitter = new LabelItem();
    this._refreshButtonItem = new ButtonItem();
    this._roleConfigurationsTree = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnParentObjects = new Column();
    this.menuObjects = new MenuBar();
    this._contextMenuBarItem = new ContextMenuBarItem();
    this._addRoleConfigurationMenuButtonItem = new MenuButtonItem();
    this._deleteRoleConfigurationMenuButtonItem = new MenuButtonItem();
    this._addRoleMenuButtonItem = new MenuButtonItem();
    this._deleteRoleMenuButtonItem = new MenuButtonItem();
    this._refreshMenuButtonItem = new MenuButtonItem();
    this._roleConfigurationsTree.BeginInit();
    this.SuspendLayout();
    this.toolBarRules.AddRemoveButtonsVisible = false;
    this.toolBarRules.AllowHorizontalDock = false;
    this.toolBarRules.Closable = false;
    this.toolBarRules.DockLine = 3;
    this.toolBarRules.DrawActionsButton = false;
    this.toolBarRules.FullMenus = true;
    this.toolBarRules.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarRules.Hidden = false;
    this.toolBarRules.ImageList = this.imagesMenus;
    this.toolBarRules.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this._loadRoleConfigurationFromFileButtonItem,
      (ToolbarItemBase) this._saveRoleConfigurationToFileButtonItem,
      (ToolbarItemBase) this._addRoleConfiguration,
      (ToolbarItemBase) this._deleteButtonItem,
      (ToolbarItemBase) this._addRoleButtonItem,
      (ToolbarItemBase) this._deleteRoleButtonItem,
      (ToolbarItemBase) this.lbSplitter,
      (ToolbarItemBase) this._refreshButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBarRules, "toolBarRules");
    this.toolBarRules.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRules.Movable = false;
    this.toolBarRules.Name = "toolBarRules";
    this.toolBarRules.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRules.Stretch = true;
    this.toolBarRules.Tearable = false;
    this.imagesMenus.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesMenus.ImageStream");
    this.imagesMenus.TransparentColor = Color.Transparent;
    this.imagesMenus.Images.SetKeyName(0, "");
    this.imagesMenus.Images.SetKeyName(1, "удалить.png");
    this.imagesMenus.Images.SetKeyName(2, "roles_add.ico");
    this.imagesMenus.Images.SetKeyName(3, "roles_delete.ico");
    this.imagesMenus.Images.SetKeyName(4, "обновить.png");
    this.imagesMenus.Images.SetKeyName(5, "users_back.png");
    this.imagesMenus.Images.SetKeyName(6, "folder_out.png");
    this.imagesMenus.Images.SetKeyName(7, "сохранить.png");
    componentResourceManager.ApplyResources((object) this._loadRoleConfigurationFromFileButtonItem, "_loadRoleConfigurationFromFileButtonItem");
    this._loadRoleConfigurationFromFileButtonItem.Enabled = false;
    this._loadRoleConfigurationFromFileButtonItem.ImageIndex = 6;
    this._loadRoleConfigurationFromFileButtonItem.Click += new EventHandler(this.LoadRoleConfigurationFromFileButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._saveRoleConfigurationToFileButtonItem, "_saveRoleConfigurationToFileButtonItem");
    this._saveRoleConfigurationToFileButtonItem.Enabled = false;
    this._saveRoleConfigurationToFileButtonItem.ImageIndex = 7;
    this._saveRoleConfigurationToFileButtonItem.Click += new EventHandler(this.SaveRoleConfigurationToFileButtonItem_Click);
    this._addRoleConfiguration.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addRoleConfiguration, "_addRoleConfiguration");
    this._addRoleConfiguration.Enabled = false;
    this._addRoleConfiguration.ImageIndex = 0;
    this._addRoleConfiguration.Click += new EventHandler(this.AddRoleConfiguration_Click);
    componentResourceManager.ApplyResources((object) this._deleteButtonItem, "_deleteButtonItem");
    this._deleteButtonItem.Enabled = false;
    this._deleteButtonItem.ImageIndex = 1;
    this._deleteButtonItem.Click += new EventHandler(this.DeleteButtonItem_Click);
    this._addRoleButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addRoleButtonItem, "_addRoleButtonItem");
    this._addRoleButtonItem.Enabled = false;
    this._addRoleButtonItem.ImageIndex = 2;
    this._addRoleButtonItem.Click += new EventHandler(this.AddRoleButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._deleteRoleButtonItem, "_deleteRoleButtonItem");
    this._deleteRoleButtonItem.Enabled = false;
    this._deleteRoleButtonItem.ImageIndex = 3;
    this._deleteRoleButtonItem.Click += new EventHandler(this.DeleteRoleButtonItem_Click);
    this.lbSplitter.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.lbSplitter, "lbSplitter");
    this.lbSplitter.Enabled = false;
    this.lbSplitter.Stretch = true;
    this._refreshButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshButtonItem, "_refreshButtonItem");
    this._refreshButtonItem.Enabled = false;
    this._refreshButtonItem.ImageIndex = 4;
    this._refreshButtonItem.Click += new EventHandler(this.RefreshButtonItem_Click);
    this._roleConfigurationsTree.AllowDrop = true;
    this._roleConfigurationsTree.AllowIndividualRowResize = false;
    this._roleConfigurationsTree.AllowMultiSelect = false;
    this._roleConfigurationsTree.AllowRowResize = false;
    this._roleConfigurationsTree.AllowUserPinnedColumns = false;
    this._roleConfigurationsTree.AutoFitColumns = true;
    this._roleConfigurationsTree.Columns.Add(this.columnParentObjects);
    this._roleConfigurationsTree.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this._roleConfigurationsTree, "_roleConfigurationsTree");
    this._roleConfigurationsTree.ImageList = (ImageList) null;
    this._roleConfigurationsTree.LineStyle = LineStyle.Dot;
    this._roleConfigurationsTree.Name = "_roleConfigurationsTree";
    this._roleConfigurationsTree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._roleConfigurationsTree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._roleConfigurationsTree.SelectBeforeEdit = true;
    this._roleConfigurationsTree.ShowRootRow = false;
    this._roleConfigurationsTree.SuppressErrorMessages = true;
    this._roleConfigurationsTree.ShowContextMenu += new MouseEventHandler(this.RoleConfigurationsTree_ShowContextMenu);
    this._roleConfigurationsTree.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.RoleConfigurationsTree_GetAllowedRowDropLocations);
    this._roleConfigurationsTree.GetAllowRowDrag += new GetAllowRowDragHandler(this.RoleConfigurationsTree_GetAllowRowDrag);
    this._roleConfigurationsTree.GetCellData += new GetCellDataHandler(this.RoleConfigurationsTree_GetCellData);
    this._roleConfigurationsTree.GetChildren += new GetChildrenHandler(this.RoleConfigurationsTree_GetChildren);
    this._roleConfigurationsTree.GetRowData += new GetRowDataHandler(this.RoleConfigurationsTree_GetRowData);
    this._roleConfigurationsTree.GetRowDropEffect += new GetRowDropEffectHandler(this.RoleConfigurationsTree_GetRowDropEffect);
    this._roleConfigurationsTree.RowDrop += new RowDropHandler(this.RoleConfigurationsTree_RowDrop);
    this._roleConfigurationsTree.SelectionChanged += new EventHandler(this.RoleConfigurationsTree_SelectionChanged);
    this._roleConfigurationsTree.SelectionChanging += new SelectionChangingHandler(this.RoleConfigurationsTree_SelectionChanging);
    componentResourceManager.ApplyResources((object) this.columnParentObjects, "columnParentObjects");
    this.columnParentObjects.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnParentObjects.HeaderStyle.HorzAlignment");
    this.columnParentObjects.Movable = false;
    this.columnParentObjects.Name = "columnParentObjects";
    this.columnParentObjects.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menuObjects, "menuObjects");
    this.menuObjects.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuObjects.Hidden = false;
    this.menuObjects.ImageList = this.imagesMenus;
    this.menuObjects.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._contextMenuBarItem
    });
    this.menuObjects.Name = "menuObjects";
    this.menuObjects.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this._contextMenuBarItem, "_contextMenuBarItem");
    this._contextMenuBarItem.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this._addRoleConfigurationMenuButtonItem,
      (ToolbarItemBase) this._deleteRoleConfigurationMenuButtonItem,
      (ToolbarItemBase) this._addRoleMenuButtonItem,
      (ToolbarItemBase) this._deleteRoleMenuButtonItem,
      (ToolbarItemBase) this._refreshMenuButtonItem
    });
    this._contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._addRoleConfigurationMenuButtonItem, "_addRoleConfigurationMenuButtonItem");
    this._addRoleConfigurationMenuButtonItem.ImageIndex = 0;
    this._addRoleConfigurationMenuButtonItem.ShowText = true;
    this._addRoleConfigurationMenuButtonItem.Click += new EventHandler(this.AddRoleConfiguration_Click);
    componentResourceManager.ApplyResources((object) this._deleteRoleConfigurationMenuButtonItem, "_deleteRoleConfigurationMenuButtonItem");
    this._deleteRoleConfigurationMenuButtonItem.ImageIndex = 1;
    this._deleteRoleConfigurationMenuButtonItem.ShowText = true;
    this._deleteRoleConfigurationMenuButtonItem.Click += new EventHandler(this.DeleteButtonItem_Click);
    this._addRoleMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addRoleMenuButtonItem, "_addRoleMenuButtonItem");
    this._addRoleMenuButtonItem.ImageIndex = 2;
    this._addRoleMenuButtonItem.ShowText = true;
    this._addRoleMenuButtonItem.Click += new EventHandler(this.AddRoleButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._deleteRoleMenuButtonItem, "_deleteRoleMenuButtonItem");
    this._deleteRoleMenuButtonItem.ImageIndex = 3;
    this._deleteRoleMenuButtonItem.ShowText = true;
    this._deleteRoleMenuButtonItem.Click += new EventHandler(this.DeleteRoleButtonItem_Click);
    this._refreshMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshMenuButtonItem, "_refreshMenuButtonItem");
    this._refreshMenuButtonItem.ImageIndex = 4;
    this._refreshMenuButtonItem.ShowText = true;
    this._refreshMenuButtonItem.Click += new EventHandler(this.RefreshButtonItem_Click);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this._roleConfigurationsTree);
    this.Controls.Add((Control) this.menuObjects);
    this.Controls.Add((Control) this.toolBarRules);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (RoleConfigsEditor);
    this.Tag = (object) "  ";
    this._roleConfigurationsTree.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Внутренний класс - идентификатор и название роли</summary>
  protected internal class KeyRole
  {
    /// <summary>Идентификатор версии объекта типа "Роль"</summary>
    public long ObjectID;
    /// <summary>Заголовок роли</summary>
    public string Caption;

    /// <summary>Создать экземпляр объекта</summary>
    /// <param name="objectID">Идентификатор версии объекта типа "Роль"</param>
    /// <param name="caption">Заголовок роли</param>
    public KeyRole(long objectID, string caption)
    {
      this.ObjectID = objectID;
      this.Caption = caption;
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>true, если объекты равны</returns>
    public override bool Equals(object obj)
    {
      return !(obj is RoleConfigsEditor.KeyRole keyRole) ? base.Equals(obj) : this.ObjectID == keyRole.ObjectID;
    }

    /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
    /// <returns>32-битный хэш-код экземпляра класса</returns>
    public override int GetHashCode() => this.ObjectID.GetHashCode();
  }
}
