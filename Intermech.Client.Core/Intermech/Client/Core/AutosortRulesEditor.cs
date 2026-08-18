
// Type: Intermech.Client.Core.AutosortRulesEditor
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
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.Navigator;
using Intermech.Search.ObjectListFilters;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core;

/// <summary>
/// Редактор правил автоматической сортировки и отображения составов
/// </summary>
public sealed class AutosortRulesEditor : UserControl
{
  /// <summary>Права доступа в редакторе правил сортировки составов</summary>
  private RulesEditorAccessRights _rulesEditorAccessRights;
  private INavigatorColumnsService _navigatorColumnsService;
  private INamedImageList _namedImageList;
  private ICategoryTypeIconService _categoryTypeIconService;
  private INavGraphicsCache _navGraphicsCache;
  private ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Редактируемая копия правила</summary>
  private CompositionsAutosortRule _compositionsAutosortRule;
  private Dictionary<NavigatorColumnsKey, NavigatorColumns> _navigatorColumnsDictionary;
  /// <summary>
  /// Есть ли изменения в коллекции правил сортировки составов
  /// </summary>
  private bool _isChanged;
  private ColumnPack _defaultColumnPack;
  private IRoleConfigurationManager _roleConfigurationManager;
  private List<DefaultCommandSettings> _defaultCommandsSettings;
  private bool _isDefaultCommandsSettingsLoaded;
  private LazyService<IDefaultCommands4ObjTypes> _defaultCommandsForObjectTypes = new LazyService<IDefaultCommands4ObjTypes>();
  /// <summary>Экземпляр класса для сравнения строк в дереве</summary>
  private AutosortRulesEditor.CompareRows _rowsComparer = new AutosortRulesEditor.CompareRows();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesMenus;
  private OpenFileDialog _openFileDialog;
  private SaveFileDialog _saveFileDialog;
  private TableLayoutPanel tableLayoutPanel;
  private Intermech.Bars.ToolBar toolBarObjTypes;
  private LabelItem lbRelTypeNameHint;
  private ButtonItem btnRelationType;
  private LabelItem lbObjectType;
  private ButtonItem _refreshPartTypesButtonItem;
  private Intermech.Bars.ToolBar toolBarPositions;
  private ButtonItem _moveUpPartTypeButtonItem;
  private ButtonItem _moveDownPartTypeButtonItem;
  private ButtonItem _moveTopPartTypeButtonItem;
  private ButtonItem _moveBottomPartTypeButtonItem;
  private SplitContainer panelsChild;
  private RoleConfigsEditor _roleConfigurationsEditorControl;
  private Intermech.VirtualTreeView.VirtualTreeView _objectTypesTree;
  private Column columnCheck;
  private CellEditor cellEditor1;
  private CheckBox checkBox1;
  private Column columnParentObjects;
  private Intermech.Bars.ToolBar toolBarParTypesPositions;
  private ButtonItem _moveUpObjectTypeButtonItem;
  private ButtonItem _moveDownObjectTypeButtonItem;
  private Intermech.Bars.ToolBar toolBarParObjTypes;
  private ButtonItem _addObjectTypeButtonItem;
  private ButtonItem _removeObjectTypeButtonItem;
  private LabelItem lbObjType;
  private ButtonItem _refreshObjectTypesButtonItem;
  private SplitContainer panelsMain;
  private MenuBar menuChildTypes;
  private ContextMenuBarItem _partTypesContextMenuBarItem;
  private MenuBar menuParentTypes;
  private ContextMenuBarItem _objectTypesContextMenuBarItem;
  private MenuButtonItem _addObjectTypeMenuButtonItem;
  private MenuButtonItem _removeObjectTypeMenuButtonItem;
  private MenuButtonItem _moveUpObjectTypeMenuButtonItem;
  private MenuButtonItem _moveDownObjectTypeMenuButtonItem;
  private MenuButtonItem _refreshObjectTypesMenuButtonItem;
  private MenuButtonItem _moveUpPartTypeMenuButtonItem;
  private MenuButtonItem _moveDownPartTypeMenuButtonItem;
  private MenuButtonItem _moveTopPartTypeMenuButtonItem;
  private MenuButtonItem _moveBottomPartTypeMenuButtonItem;
  private MenuButtonItem _refreshPartTypesMenuButtonItems;
  private Intermech.VirtualTreeView.VirtualTreeView _partTypesTree;
  private Column _objectTypeNamePartTypesTreeColumn;
  private MenuButtonItem _changeCompositionsViewSettingsMenuButtonItem;
  private MenuButtonItem _removeCompositionsViewSettingsMenuButtonItem;
  private ButtonItem _sortObjectTypesButtonItem;
  private MenuButtonItem _sortObjectTypesMenuButtonItem;
  private MenuButtonItem _changeObjectsViewSettingsMenuButtonItem;
  private MenuButtonItem _removeObjectsViewSettingsMenuButtonItem;
  private MenuButtonItem _showSelectorsAndClassifiersMenuButtonItem;
  private MenuButtonItem _changeDefaultColumnsSettingsMenuButtonItem;
  private MenuButtonItem _removeDefaultColumnsSettingsMenuButtonItem;
  private MenuButtonItem _changeDefaultCommandsSettingsMenuButtonItem;
  private MenuButtonItem _removeDefaultCommandsSettingsMenuButtonItem;
  private Column _visibilityPartTypesTreeColumn;
  private Column _groupingPartTypesTreeColumn;
  private MenuButtonItem _defaultObjectListFilterMenuButtonItem;

  public AutosortRulesEditor()
  {
    this.InitializeComponent();
    if (!(ServicesManager.GetService(typeof (BarManager)) is BarManager service))
      return;
    service.RendererChanged += new EventHandler(this.BarManager_RendererChanged);
    this.BarManager_RendererChanged((object) service, EventArgs.Empty);
  }

  public event EventHandler Changed;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      this.OnChanged();
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Права доступа пользователя к коллекции правил сортировки составов
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public RulesEditorAccessRights RulesEditorAccessRights
  {
    get => this._rulesEditorAccessRights;
    set
    {
      this._rulesEditorAccessRights = value;
      this.UpdateControls();
    }
  }

  /// <summary>Выполнить инициализацию компонента</summary>
  public void Init()
  {
    this._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._navigatorColumnsService = ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService;
    this._categoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._roleConfigurationManager = ServicesManager.GetService(typeof (IRoleConfigurationManager)) as IRoleConfigurationManager;
    this._changeCompositionsViewSettingsMenuButtonItem.Image = Holder.NamedImageList != null ? Holder.NamedImageList.ImageList.Images[Holder.NamedImageList.ImageIndex("imgViewSettings")] : this._changeCompositionsViewSettingsMenuButtonItem.Image;
    this._roleConfigurationsEditorControl.Init();
    this._isChanged = false;
    this.UpdateControls();
  }

  /// <summary>Применить изменения из редактора</summary>
  public void ApplyChanges()
  {
    if (this._currentUserAndRole == null || !this._currentUserAndRole.IsAdmin)
      return;
    if (ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
    {
      this._isChanged = false;
      this.UpdateControls();
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._compositionsAutosortRule != null)
          this._compositionsAutosortRule.Save(sessionKeeper.Session, this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, true);
        this._navigatorColumnsService.SaveToObject(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, MetaDataHelper.GetAttributeTypeID("cad01487-306c-11d8-b4e9-00304f19f545"), this._navigatorColumnsDictionary);
        if (this._roleConfigurationsEditorControl.GetRuleRoles(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID).IndexOf(this._currentUserAndRole.RoleID) >= 0)
          this._currentUserAndRole.RoleNavStreams = this._navigatorColumnsDictionary;
      }
      this.ApplyDefaultColumnsChanges();
      this.SaveDefaultCommandsSettings();
      this._isChanged = false;
      this.UpdateControls();
      if (this._currentUserAndRole.Rule.ObjectID != this._compositionsAutosortRule.ObjectID)
        return;
      bool useEvents = this._compositionsAutosortRule.UseEvents;
      this._compositionsAutosortRule.UseEvents = this._currentUserAndRole.UseRuleEvents;
      this._currentUserAndRole.Rule = this._compositionsAutosortRule;
      this._compositionsAutosortRule.UseEvents = useEvents;
    }
  }

  /// <summary>Отменить изменения в редакторе (без выдачи запроса)</summary>
  public void CancelChanges() => this.SetCurrentRoleConfigurationVersionID();

  private void BarManager_RendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBarObjTypes.Renderer = renderer;
    this.toolBarParObjTypes.Renderer = renderer;
    this.toolBarParTypesPositions.Renderer = renderer;
    this.toolBarPositions.Renderer = renderer;
    this.menuChildTypes.Renderer = renderer;
    this.menuParentTypes.Renderer = renderer;
  }

  private void RoleConfigurationsEditorControl_LoadRoleConfigurationFromFile(
    object sender,
    EventArgs e)
  {
    this.LoadRoleConfigurationFromFile();
  }

  private void RoleConfigurationsEditorControl_SaveRoleConfigurationToFile(
    object sender,
    EventArgs e)
  {
    this.SaveRoleConfigurationToFile();
  }

  private void AddObjectTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.AddObjectType();
  }

  private void RemoveObjectTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RemoveObjectType();
  }

  private void MoveUpObjectTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveUpObjectType();
  }

  private void MoveDownObjectTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveDownObjectType();
  }

  private void RefreshObjectTypesMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RefreshObjectTypes();
  }

  private void SortObjectTypesMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.SortObjectTypes();
  }

  private void ChangeCompositionsViewSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.ChangeCompositionsViewSettings();
  }

  private void RemoveCompositionsViewSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RemoveCompositionsViewSettings();
  }

  private void ChangeObjectsViewSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.ChangeObjectsViewSettings();
  }

  private void RemoveObjectsViewSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RemoveObjectsViewSettings();
  }

  private void ChangeDefaultColumnsSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.ChangeDefaultColumnsSettings();
  }

  private void RemoveDefaultColumnsSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RemoveDefaultColumnsSettings();
  }

  private void ChangeDefaultCommandsSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.ChangeDefaultCommandsSettings();
  }

  private void RemoveDefaultCommandsSettingsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.RemoveDefaultCommandsSettings();
  }

  private void ShowSelectorsAndClassifiersMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.ShowSelectorsAndClassifiers();
  }

  private void DefaultObjectListFilterMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.SetDefaultObjectListFilter();
  }

  private void ObjectTypesTree_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Item is ParentObjectType)
    {
      ParentObjectType parentObjectType = (ParentObjectType) e.Row.Item;
      e.RowData.ImageSize = 32 /*0x20*/;
      e.RowData.ImageList = this._categoryTypeIconService.ImageList;
      e.RowData.ImageIndex = this._categoryTypeIconService.IndexOf(4, parentObjectType.ObjectTypeID);
      e.RowData.ShowPrefixColumn = false;
    }
    else
    {
      if (!(e.Row.Item is ChildRelationType))
        return;
      ChildRelationType childRelationType = (ChildRelationType) e.Row.Item;
      e.RowData.ImageSize = 32 /*0x20*/;
      e.RowData.ImageList = this._categoryTypeIconService.ImageList;
      e.RowData.ImageIndex = this._categoryTypeIconService.IndexOf(6, childRelationType.RelationTypeID);
      e.RowData.ShowPrefixColumn = this._currentUserAndRole != null && this._currentUserAndRole.IsAdmin;
    }
  }

  private void ObjectTypesTree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Item is ParentObjectType)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType((e.Row.Item as ParentObjectType).ObjectTypeID);
      if (e.Column == this.columnParentObjects)
        e.CellData.Value = objectType != null ? (object) objectType.ObjectTypeName : (object) LocalizationHolder.rm.GetString("Client.Core_236");
    }
    if (!(e.Row.Item is ChildRelationType))
      return;
    IMSRelationType relationType = MetaDataHelper.GetRelationType((e.Row.Item as ChildRelationType).RelationTypeID);
    ChildRelationType childRelationType = e.Row.Item as ChildRelationType;
    if (e.Column == this.columnParentObjects)
      e.CellData.Value = relationType != null ? (object) relationType.Description : (object) LocalizationHolder.rm.GetString("Client.Core_1096");
    if (e.Column != this.columnCheck)
      return;
    e.CellData.Value = (object) childRelationType.Visible;
  }

  private void ObjectTypesTree_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is CompositionsAutosortRule)
      e.Children = (IList) (e.Row.Item as CompositionsAutosortRule).ParentObjectTypes;
    if (!(e.Row.Item is ParentObjectType))
      return;
    e.Children = (IList) (e.Row.Item as ParentObjectType).ChildRelationTypes;
  }

  private void ObjectTypesTree_SelectionChanged(object sender, EventArgs e)
  {
    this.FillChildrenTypes();
    this.UpdateControls();
  }

  private void ObjectTypesTree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (this._currentUserAndRole == null || !this._currentUserAndRole.IsAdmin || !(e.Row.Item is ChildRelationType) || e.Column != this.columnCheck || !(e.Row.Item is ChildRelationType childRelationType))
      return;
    childRelationType.Visible = (bool) e.NewValue;
    this._isChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  private void ObjectTypesTree_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this._objectTypesContextMenuBarItem.Show((Control) this._objectTypesTree, e.Location);
  }

  private void RefreshPartTypesMenuButtonItems_Click(object sender, EventArgs e)
  {
    this.RefreshPartTypes();
  }

  private void MoveUpPartTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveUpPartType();
  }

  private void MoveDownPartTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveDownPartType();
  }

  private void MoveTopPartTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveTopPartType();
  }

  private void MoveBottomPartTypeMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.MoveBottomPartType();
  }

  private void PartTypesTree_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  private void PartTypesTree_FocusRowChanged(object sender, EventArgs e) => this.UpdateControls();

  private void PartTypesTree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is ChildObjectType))
      return;
    ChildObjectType childObjectType = (ChildObjectType) e.Row.Item;
    if (e.Column == this._objectTypeNamePartTypesTreeColumn)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(childObjectType.ObjectTypeID);
      e.CellData.Value = (object) objectType.ObjectTypeName;
      if (objectType.VersionsMode != ObjectVersionModes.Abstract)
        return;
      e.CellData.EvenStyle = new Style(e.CellData.EvenStyle, new StyleDelta()
      {
        ForeColor = Color.Gray
      });
      e.CellData.OddStyle = new Style(e.CellData.OddStyle, new StyleDelta()
      {
        ForeColor = Color.Gray
      });
    }
    else if (e.Column == this._visibilityPartTypesTreeColumn)
    {
      e.CellData.Value = (object) childObjectType.Visible;
    }
    else
    {
      if (e.Column != this._groupingPartTypesTreeColumn)
        return;
      e.CellData.Value = (object) childObjectType.Grouping;
    }
  }

  private void PartTypesTree_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is ChildRelationType)
    {
      ChildRelationType childRelationType = (ChildRelationType) e.Row.Item;
      e.Children = (IList) childRelationType.ChildObjectTypes;
    }
    else
    {
      if (!(e.Row.Item is ChildObjectType))
        return;
      ChildObjectType childObjectType = (ChildObjectType) e.Row.Item;
      e.Children = (IList) childObjectType.Children;
    }
  }

  private void PartTypesTree_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is ChildObjectType))
      return;
    ChildObjectType childObjectType = e.Row.Item as ChildObjectType;
    e.RowData.ImageSize = 32 /*0x20*/;
    e.RowData.ImageList = this._categoryTypeIconService.ImageList;
    e.RowData.ImageIndex = this._categoryTypeIconService.IndexOf(4, childObjectType.ObjectTypeID);
  }

  private void PartTypesTree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (this._currentUserAndRole == null || !this._currentUserAndRole.IsAdmin || !(e.Row.Item is ChildObjectType))
      return;
    ChildObjectType childObjectType = (ChildObjectType) e.Row.Item;
    if (e.Column == this._visibilityPartTypesTreeColumn)
    {
      if (MetaDataHelper.GetObjectType(childObjectType.ObjectTypeID).VersionsMode == ObjectVersionModes.Abstract)
      {
        List<int> intList = new List<int>();
        this.SetChildObjectTypeVisibleRecursive(childObjectType, (bool) e.NewValue, intList);
        foreach (int objectTypeID in intList.Distinct<int>())
          this.SetChildObjectTypeVisible(objectTypeID, (bool) e.NewValue);
      }
      else
      {
        childObjectType.Visible = (bool) e.NewValue;
        this.SetChildObjectTypeVisible(childObjectType.ObjectTypeID, (bool) e.NewValue);
        this.SetAbstractChildObjectTypesVisible();
      }
      this._isChanged = true;
      this.OnChanged();
      this._partTypesTree.UpdateRows(false);
      this.UpdateControls();
    }
    else
    {
      if (e.Column != this._groupingPartTypesTreeColumn)
        return;
      childObjectType.Grouping = (bool) e.NewValue;
      this._isChanged = true;
      this.OnChanged();
      this._partTypesTree.UpdateRows(false);
      this.UpdateControls();
    }
  }

  private void PartTypesTree_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this._partTypesContextMenuBarItem.Show((Control) this._partTypesTree, e.Location);
  }

  private void RoleConfigurationsEditorControl_CurrentRoleConfigurationChanged(
    object sender,
    EventArgs e)
  {
    this.SetCurrentRoleConfigurationVersionID();
  }

  private void RoleConfigurationsEditorControl_CurrentRoleConfigurationChanging(
    object sender,
    CancelEventArgs e)
  {
    if (!this.IsChanged)
      return;
    switch (MessageBox.Show("Конфигурация роли была изменена. Сохранить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        this.ApplyChanges();
        break;
    }
  }

  private void LoadRoleConfigurationFromFile()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly || this._openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this._compositionsAutosortRule.Load(new XMLSettingsStorage(this._openFileDialog.FileName), (XmlNode) null);
    this.FillParentObjectTypesTree(true);
    this.IsChanged = true;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_3259.ssp_imclient_3260()), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void SaveRoleConfigurationToFile()
  {
    if (this._saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    XMLSettingsStorage storage = new XMLSettingsStorage();
    this._compositionsAutosortRule.Save(storage, (XmlNode) null);
    if (!storage.Save(this._saveFileDialog.FileName))
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_3259.ssp_imclient_3261()), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void AddObjectType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (compositionsAutosortRule == null)
      return;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_237"), typeof (ObjectTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    Guid empty = Guid.Empty;
    foreach (int id in selectorForm.IDList)
    {
      if (MetaDataHelper.ExistsObjectType(id) && compositionsAutosortRule.IndexOfParentObjectType(id, false) == -1)
        compositionsAutosortRule.ParentObjectTypes.Add(new ParentObjectType(id));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      compositionsAutosortRule.SyncMetadata(sessionKeeper.Session);
    this.FillParentObjectTypesTree(false);
    this._isChanged = true;
    this.OnChanged();
  }

  private void RemoveObjectType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (compositionsAutosortRule == null)
      return;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    if (parentObjectType == null || MessageBox.Show(LocalizationHolder.rm.GetString(sc_3259.ssp_imclient_3262()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    compositionsAutosortRule.ParentObjectTypes.Remove(parentObjectType);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      compositionsAutosortRule.SyncMetadata(sessionKeeper.Session);
    this.FillParentObjectTypesTree(false);
    this._isChanged = true;
    this.OnChanged();
  }

  private void MoveUpObjectType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (selectedRow == null || compositionsAutosortRule == null)
      return;
    if (selectedRow.Item is ChildRelationType)
    {
      this.DoChildRelationTypeUp();
    }
    else
    {
      ParentObjectType parentObjectType1 = this.GetCurrentParentObjectType();
      if (parentObjectType1 == null)
        return;
      int index1 = compositionsAutosortRule.ParentObjectTypes.IndexOf(parentObjectType1);
      int index2 = index1 - 1;
      if (index1 <= 0)
        return;
      ParentObjectType parentObjectType2 = compositionsAutosortRule.ParentObjectTypes[index2];
      compositionsAutosortRule.ParentObjectTypes[index2] = parentObjectType1;
      compositionsAutosortRule.ParentObjectTypes[index1] = parentObjectType2;
      this.FillParentObjectTypesTree(false);
      this._isChanged = true;
      this.OnChanged();
    }
  }

  private void MoveDownObjectType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (selectedRow == null || compositionsAutosortRule == null)
      return;
    if (selectedRow.Item is ChildRelationType)
    {
      this.DoChildRelationTypeDown();
    }
    else
    {
      ParentObjectType parentObjectType1 = this.GetCurrentParentObjectType();
      if (parentObjectType1 == null)
        return;
      int index1 = compositionsAutosortRule.ParentObjectTypes.IndexOf(parentObjectType1);
      int index2 = index1 + 1;
      if (index1 >= compositionsAutosortRule.ParentObjectTypes.Count - 1)
        return;
      ParentObjectType parentObjectType2 = compositionsAutosortRule.ParentObjectTypes[index2];
      compositionsAutosortRule.ParentObjectTypes[index2] = parentObjectType1;
      compositionsAutosortRule.ParentObjectTypes[index1] = parentObjectType2;
      this.FillParentObjectTypesTree(false);
      this._isChanged = true;
      this.OnChanged();
    }
  }

  private void RefreshObjectTypes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
      if (this._compositionsAutosortRule != null)
        this._compositionsAutosortRule.SyncMetadata(sessionKeeper.Session);
    }
    this.FillParentObjectTypesTree(false);
  }

  private void SortObjectTypes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
      if (this._compositionsAutosortRule != null)
        this._compositionsAutosortRule.SyncMetadata(sessionKeeper.Session);
    }
    if (this._compositionsAutosortRule == null || this._compositionsAutosortRule.ParentObjectTypes.Count <= 1)
      return;
    this._compositionsAutosortRule.ParentObjectTypes.Sort();
    this.FillParentObjectTypesTree(false);
    this._isChanged = true;
    this.OnChanged();
  }

  private void ChangeCompositionsViewSettings() => this.DoEditCategoryViews();

  private void RemoveCompositionsViewSettings()
  {
    this.DoResetCategoryViews(1, LocalizationHolder.rm.GetString("Client.Core_1316"), LocalizationHolder.rm.GetString("Client.Core_1318"));
  }

  private void ChangeObjectsViewSettings() => this.DoEditCategoryViews(4);

  private void RemoveObjectsViewSettings()
  {
    this.DoResetCategoryViews(4, LocalizationHolder.rm.GetString("Client.Core_1316a"), LocalizationHolder.rm.GetString("Client.Core_1318a"));
  }

  private void ShowSelectorsAndClassifiers()
  {
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    this.GetSelectedChildRelationType();
    this._showSelectorsAndClassifiersMenuButtonItem.Enabled = (this._rulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) != 0 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    if (!this._showSelectorsAndClassifiersMenuButtonItem.Enabled || parentObjectType == null || parentObjectType.EnableSelectionsAndClassifiers == this._showSelectorsAndClassifiersMenuButtonItem.Checked)
      return;
    parentObjectType.EnableSelectionsAndClassifiers = this._showSelectorsAndClassifiersMenuButtonItem.Checked;
    this.IsChanged = true;
  }

  private void SetDefaultObjectListFilter()
  {
    using (TreeViewWithButtonsForm viewWithButtonsForm1 = new TreeViewWithButtonsForm())
    {
      viewWithButtonsForm1.ShowCheckBoxes = false;
      viewWithButtonsForm1.Text = "Выберите фильтр списка объектов по умолчанию";
      IObjectListFiltersClientService filtersClientService = ServiceLocator.Get<IObjectListFiltersClientService>();
      ParentObjectType parentObjectType1 = this.GetCurrentParentObjectType();
      int objectTypeId = parentObjectType1.ObjectTypeID;
      foreach (ObjectListFilter objectListFilter in filtersClientService.GetFiltersForObjectType(objectTypeId))
        viewWithButtonsForm1.TreeView.Nodes.Add(this.CreateTreeNodeFromObjectListFilter(objectListFilter));
      Guid? nullable1;
      if (parentObjectType1.DefaultObjectListFilter.HasValue)
      {
        TreeViewWithButtonsForm viewWithButtonsForm2 = viewWithButtonsForm1;
        nullable1 = parentObjectType1.DefaultObjectListFilter;
        // ISSUE: variable of a boxed type
        __Boxed<Guid> local = (ValueType) nullable1.Value;
        viewWithButtonsForm2.SelectedTag = (object) local;
      }
      if (viewWithButtonsForm1.ShowDialog() != DialogResult.OK)
        return;
      ParentObjectType parentObjectType2 = parentObjectType1;
      Guid? nullable2;
      if (!(viewWithButtonsForm1.SelectedTag is Guid))
      {
        nullable1 = new Guid?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Guid?((Guid) viewWithButtonsForm1.SelectedTag);
      parentObjectType2.DefaultObjectListFilter = nullable2;
      this.IsChanged = true;
    }
  }

  private TreeNode CreateTreeNodeFromObjectListFilter(ObjectListFilter objectListFilter)
  {
    TreeNode objectListFilter1 = new TreeNode(objectListFilter.Name);
    objectListFilter1.Tag = (object) objectListFilter.Guid;
    if (objectListFilter.IsSystem)
      objectListFilter1.ForeColor = Color.DarkBlue;
    return objectListFilter1;
  }

  private void MoveTopPartType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyChildObjects) == RulesEditorAccessRights.ReadOnly)
      return;
    this.GetSelectedChildObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    if (childRelationType == null)
      return;
    List<Row> selectedTreeRows = this.GetSelectedTreeRows(this._partTypesTree, 1);
    if (selectedTreeRows.Count == 0 || selectedTreeRows[0].ChildIndex == 0)
      return;
    int childIndex = selectedTreeRows[0].ChildIndex;
    List<ChildObjectType> childObjectTypeList = new List<ChildObjectType>(selectedTreeRows.Count);
    for (int index = 0; index < selectedTreeRows.Count; ++index)
      childObjectTypeList.Add(selectedTreeRows[index].Item as ChildObjectType);
    for (int index = 0; index < childObjectTypeList.Count; ++index)
    {
      ChildObjectType childObjectType = childObjectTypeList[index];
      if (childRelationType.ChildObjectTypes.IndexOf(childObjectType) <= 0)
        return;
      childRelationType.ChildObjectTypes.Remove(childObjectType);
      childRelationType.ChildObjectTypes.Insert(index, childObjectType);
    }
    this._partTypesTree.UpdateRows(true);
    this._isChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  private void MoveUpPartType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyChildObjects) == RulesEditorAccessRights.ReadOnly)
      return;
    this.GetSelectedChildObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    if (childRelationType == null)
      return;
    List<Row> selectedTreeRows = this.GetSelectedTreeRows(this._partTypesTree, 1);
    if (selectedTreeRows.Count == 0 || selectedTreeRows[0].ChildIndex == 0)
      return;
    List<ChildObjectType> childObjectTypeList = new List<ChildObjectType>(selectedTreeRows.Count);
    for (int index = 0; index < selectedTreeRows.Count; ++index)
      childObjectTypeList.Add(selectedTreeRows[index].Item as ChildObjectType);
    for (int index1 = 0; index1 < childObjectTypeList.Count; ++index1)
    {
      ChildObjectType childObjectType1 = childObjectTypeList[index1];
      int index2 = childRelationType.ChildObjectTypes.IndexOf(childObjectType1);
      int index3 = index2 - 1;
      if (index2 <= 0)
        return;
      ChildObjectType childObjectType2 = childRelationType.ChildObjectTypes[index3];
      childRelationType.ChildObjectTypes[index3] = childObjectType1;
      childRelationType.ChildObjectTypes[index2] = childObjectType2;
    }
    this._partTypesTree.UpdateRows(true);
    this._isChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  private void MoveDownPartType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyChildObjects) == RulesEditorAccessRights.ReadOnly)
      return;
    this.GetSelectedChildObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    if (childRelationType == null)
      return;
    List<Row> selectedTreeRows = this.GetSelectedTreeRows(this._partTypesTree, 1);
    if (selectedTreeRows.Count == 0 || selectedTreeRows[selectedTreeRows.Count - 1].ChildIndex >= childRelationType.ChildObjectTypes.Count - 1)
      return;
    List<ChildObjectType> childObjectTypeList = new List<ChildObjectType>(selectedTreeRows.Count);
    for (int index = 0; index < selectedTreeRows.Count; ++index)
      childObjectTypeList.Add(selectedTreeRows[index].Item as ChildObjectType);
    for (int index1 = childObjectTypeList.Count - 1; index1 >= 0; --index1)
    {
      ChildObjectType childObjectType1 = childObjectTypeList[index1];
      int index2 = childRelationType.ChildObjectTypes.IndexOf(childObjectType1);
      int index3 = index2 + 1;
      if (index2 >= childRelationType.ChildObjectTypes.Count - 1)
        return;
      ChildObjectType childObjectType2 = childRelationType.ChildObjectTypes[index3];
      childRelationType.ChildObjectTypes[index3] = childObjectType1;
      childRelationType.ChildObjectTypes[index2] = childObjectType2;
    }
    this._partTypesTree.UpdateRows(true);
    this._isChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  private void MoveBottomPartType()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyChildObjects) == RulesEditorAccessRights.ReadOnly)
      return;
    this.GetSelectedChildObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    if (childRelationType == null)
      return;
    List<Row> selectedTreeRows = this.GetSelectedTreeRows(this._partTypesTree, 1);
    if (selectedTreeRows.Count == 0 || selectedTreeRows[selectedTreeRows.Count - 1].ChildIndex >= childRelationType.ChildObjectTypes.Count - 1)
      return;
    List<ChildObjectType> childObjectTypeList = new List<ChildObjectType>(selectedTreeRows.Count);
    for (int index = 0; index < selectedTreeRows.Count; ++index)
      childObjectTypeList.Add(selectedTreeRows[index].Item as ChildObjectType);
    int num1 = childRelationType.ChildObjectTypes.Count - childObjectTypeList.Count;
    for (int index = childObjectTypeList.Count - 1; index >= 0; --index)
    {
      ChildObjectType childObjectType = childObjectTypeList[index];
      int num2 = childRelationType.ChildObjectTypes.IndexOf(childObjectType);
      if (num2 >= num2 + num1)
        return;
      childRelationType.ChildObjectTypes.Remove(childObjectType);
      childRelationType.ChildObjectTypes.Insert(index + num1, childObjectType);
    }
    this._partTypesTree.UpdateRows(true);
    this._isChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  private void RefreshPartTypes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
      this.GetSelectedChildRelationType()?.SyncMetadata(sessionKeeper.Session);
    }
    this.FillChildrenTypes();
  }

  private void ChangeDefaultColumnsSettings()
  {
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    int num = parentObjectType != null ? parentObjectType.ObjectTypeID : -1;
    ObjectTypeNode objectTypeNode = new ObjectTypeNode(num, AccessRights.Enabled);
    NodeColumnCollection supportedColumns = objectTypeNode.GetSupportedColumns(ContentType.NonFolders, string.Empty);
    NodeColumnCollection defaultColumns = this.GetDefaultColumns(num);
    if (AppearanceTuningForm.Execute((INode) objectTypeNode, ContentType.NonFolders, supportedColumns, defaultColumns) != DialogResult.OK)
      return;
    this.SetDefaultColumns(num, defaultColumns);
    this.IsChanged = true;
  }

  private void RemoveDefaultColumnsSettings()
  {
    this.LoadDefaultColumnPack();
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    this._defaultColumnPack.Remove(new NavigatorColumnsKey(4, parentObjectType != null ? parentObjectType.ObjectTypeID : -1, (string) null));
    this.IsChanged = true;
  }

  private NodeColumnCollection GetDefaultColumns(int objectTypeID)
  {
    this.LoadDefaultColumnPack();
    return this._defaultColumnPack[new NavigatorColumnsKey(4, objectTypeID, (string) null)] ?? new NodeColumnCollection();
  }

  private void SetDefaultColumns(int objectTypeID, NodeColumnCollection columns)
  {
    this.LoadDefaultColumnPack();
    this._defaultColumnPack[new NavigatorColumnsKey(4, objectTypeID, (string) null)] = columns;
  }

  private void LoadDefaultColumnPack()
  {
    if (this._defaultColumnPack != null)
      return;
    if (!ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
      this._defaultColumnPack = this._roleConfigurationManager.LoadNavigatorDefaultColumnPack(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID);
    else
      this._defaultColumnPack = new ColumnPack();
  }

  private void ApplyDefaultColumnsChanges()
  {
    if (this._defaultColumnPack == null)
      return;
    if (!ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
      this._roleConfigurationManager.SaveNavigatorDefaultColumnPack(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, this._defaultColumnPack);
    this._defaultColumnPack = (ColumnPack) null;
  }

  private void CancelDefaultColumnsChanges() => this._defaultColumnPack = (ColumnPack) null;

  private void ChangeDefaultCommandsSettings()
  {
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    if (parentObjectType == null)
      return;
    using (TreeViewWithButtonsForm viewWithButtonsForm = new TreeViewWithButtonsForm())
    {
      viewWithButtonsForm.Text = "Выберите команду";
      viewWithButtonsForm.ShowCheckBoxes = false;
      viewWithButtonsForm.Nodes.AddRange(this.CreateTreeSelectDialogNodes());
      viewWithButtonsForm.SelectedTag = (object) null;
      DefaultCommandSettings defaultCommandSettings = this.GetDefaultCommandSettings(parentObjectType.ObjectTypeID);
      viewWithButtonsForm.SelectedTag = defaultCommandSettings == null ? (object) string.Empty : (object) defaultCommandSettings.CommandName;
      if (viewWithButtonsForm.ShowDialog() != DialogResult.OK || object.Equals(viewWithButtonsForm.SelectedTag, (object) string.Empty))
        return;
      if (defaultCommandSettings != null)
        this._defaultCommandsSettings.Remove(defaultCommandSettings);
      this._defaultCommandsSettings.Add(new DefaultCommandSettings(parentObjectType.ObjectTypeID, viewWithButtonsForm.SelectedTag as string));
      this.IsChanged = true;
    }
  }

  private void RemoveDefaultCommandsSettings()
  {
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    if (parentObjectType == null)
      return;
    DefaultCommandSettings defaultCommandSettings = this.GetDefaultCommandSettings(parentObjectType.ObjectTypeID);
    if (defaultCommandSettings == null)
      return;
    this._defaultCommandsSettings.Remove(defaultCommandSettings);
    this.IsChanged = true;
  }

  private void SaveDefaultCommandsSettings()
  {
    if (this._defaultCommandsSettings == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDefaultCommandsSettingsServerService customService = (IDefaultCommandsSettingsServerService) sessionKeeper.Session.GetCustomService(typeof (IDefaultCommandsSettingsServerService));
      if (!ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
        customService.SaveDefaultCommandsSettingsToRoleConfiguration(sessionKeeper.Session.SessionGUID, this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, this._defaultCommandsSettings.ToArray());
      this._defaultCommandsForObjectTypes.Value.ReloadDefaultCommandsSettings();
    }
  }

  private DefaultCommandSettings GetDefaultCommandSettings(int objectTypeID)
  {
    return ((IEnumerable<DefaultCommandSettings>) this.GetDefaultCommandsSettings()).FirstOrDefault<DefaultCommandSettings>((Func<DefaultCommandSettings, bool>) (o => o.ObjectTypeID == objectTypeID));
  }

  private DefaultCommandSettings[] GetDefaultCommandsSettings()
  {
    this.LoadDefaultCommandsSettings();
    return this._defaultCommandsSettings.ToArray();
  }

  private void LoadDefaultCommandsSettings()
  {
    if (this._isDefaultCommandsSettingsLoaded)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDefaultCommandsSettingsServerService customService = (IDefaultCommandsSettingsServerService) sessionKeeper.Session.GetCustomService(typeof (IDefaultCommandsSettingsServerService));
      if (!ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
        this._defaultCommandsSettings = ((IEnumerable<DefaultCommandSettings>) customService.GetDefaultCommandsSettingsFromRoleConfiguration(sessionKeeper.Session.SessionGUID, this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID)).ToList<DefaultCommandSettings>();
    }
    this._isDefaultCommandsSettingsLoaded = true;
  }

  private TreeNode[] CreateTreeSelectDialogNodes()
  {
    return new TreeNode[7]
    {
      new TreeNode()
      {
        Text = "<<Команда не назначена>>",
        Tag = (object) string.Empty
      },
      new TreeNode()
      {
        Text = "Открыть",
        Tag = (object) "OpenDocument"
      },
      new TreeNode()
      {
        Text = "Открыть c помощью...",
        Tag = (object) "OpenWith"
      },
      new TreeNode()
      {
        Text = "Открыть в новом окне",
        Tag = (object) "OpenInNewWindow"
      },
      new TreeNode()
      {
        Text = "Редактировать",
        Tag = (object) "EditDocument"
      },
      new TreeNode()
      {
        Text = "Свойства (Карточка)",
        Tag = (object) "ParametersCard"
      },
      new TreeNode()
      {
        Text = "Смотреть",
        Tag = (object) "ViewDocument"
      }
    };
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Собрать у указанного дерева список выделенных строк указанного уровня
  /// </summary>
  /// <param name="tree">Дерево</param>
  /// <param name="level">Уровень строк</param>
  /// <returns>Список выделенных строк указанного уровня</returns>
  private List<Row> GetSelectedTreeRows(Intermech.VirtualTreeView.VirtualTreeView tree, int level)
  {
    List<Row> selectedTreeRows = new List<Row>();
    int count = tree.SelectedRows.Count;
    if (tree == null || count == 0 || level < 0)
      return selectedTreeRows;
    for (int index = 0; index < count; ++index)
    {
      Row row = tree.SelectedRows[index];
      if (row != null && row.Level >= level)
      {
        while (row != null && row.Level > level)
          row = row.ParentRow;
        if (row != null && !selectedTreeRows.Contains(row))
          selectedTreeRows.Add(row);
      }
    }
    selectedTreeRows.Sort((IComparer<Row>) this._rowsComparer);
    return selectedTreeRows;
  }

  /// <summary>Установить статус всех контролов</summary>
  private void UpdateControls()
  {
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    ChildObjectType selectedChildObjectType = this.GetSelectedChildObjectType();
    int count1 = compositionsAutosortRule != null ? compositionsAutosortRule.ParentObjectTypes.Count : 0;
    int num1 = compositionsAutosortRule != null ? compositionsAutosortRule.ParentObjectTypes.IndexOf(parentObjectType) : -1;
    int count2 = parentObjectType != null ? parentObjectType.ChildRelationTypes.Count : 0;
    int num2 = parentObjectType == null || childRelationType == null ? -1 : parentObjectType.ChildRelationTypes.IndexOf(childRelationType);
    List<Row> selectedTreeRows = this.GetSelectedTreeRows(this._partTypesTree, 1);
    bool flag1 = (this._rulesEditorAccessRights & RulesEditorAccessRights.CanModifyChildObjects) != 0;
    bool flag2 = (this._rulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) != 0;
    int editorAccessRights1 = (int) this._rulesEditorAccessRights;
    int editorAccessRights2 = (int) this._rulesEditorAccessRights;
    this._objectTypesTree.Enabled = ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._sortObjectTypesButtonItem.Enabled = flag2 && compositionsAutosortRule != null && compositionsAutosortRule.ParentObjectTypes.Count > 1 && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._sortObjectTypesMenuButtonItem.Enabled = this._sortObjectTypesButtonItem.Enabled;
    this._addObjectTypeButtonItem.Enabled = flag2 && compositionsAutosortRule != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._addObjectTypeMenuButtonItem.Enabled = this._addObjectTypeButtonItem.Enabled;
    this._removeObjectTypeButtonItem.Enabled = flag2 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._removeObjectTypeMenuButtonItem.Enabled = this._removeObjectTypeButtonItem.Enabled;
    this._changeCompositionsViewSettingsMenuButtonItem.Enabled = flag2 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && childRelationType == null;
    this._changeObjectsViewSettingsMenuButtonItem.Enabled = this._changeCompositionsViewSettingsMenuButtonItem.Enabled;
    this._removeCompositionsViewSettingsMenuButtonItem.Enabled = this._changeCompositionsViewSettingsMenuButtonItem.Enabled;
    this._removeObjectsViewSettingsMenuButtonItem.Enabled = this._removeCompositionsViewSettingsMenuButtonItem.Enabled;
    this._showSelectorsAndClassifiersMenuButtonItem.Enabled = flag2 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._showSelectorsAndClassifiersMenuButtonItem.Checked = parentObjectType != null && parentObjectType.EnableSelectionsAndClassifiers;
    this._moveUpObjectTypeButtonItem.Enabled = flag2 && compositionsAutosortRule != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && (parentObjectType != null && selectedRow != null && selectedRow.Item is ParentObjectType && count1 > 1 && num1 > 0 || parentObjectType != null && selectedRow != null && selectedRow.Item is ChildRelationType && count2 > 1 && num2 > 0);
    this._moveUpObjectTypeMenuButtonItem.Enabled = this._moveUpObjectTypeButtonItem.Enabled;
    this._moveDownObjectTypeButtonItem.Enabled = flag2 && compositionsAutosortRule != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && (selectedRow != null && parentObjectType != null && selectedRow.Item is ParentObjectType && count1 > 1 && num1 < count1 - 1 || selectedRow != null && parentObjectType != null && selectedRow.Item is ChildRelationType && count2 > 1 && num2 < count2 - 1);
    this._moveDownObjectTypeMenuButtonItem.Enabled = this._moveDownObjectTypeButtonItem.Enabled;
    this._refreshObjectTypesButtonItem.Enabled = flag2 && compositionsAutosortRule != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this._refreshObjectTypesMenuButtonItem.Enabled = this._refreshObjectTypesButtonItem.Enabled;
    this._partTypesTree.Enabled = ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID);
    this.btnRelationType.Enabled = true;
    this._moveUpPartTypeButtonItem.Enabled = flag1 && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && selectedTreeRows.Count > 0 && selectedTreeRows[0].ChildIndex > 0;
    this._moveUpPartTypeMenuButtonItem.Enabled = this._moveUpPartTypeButtonItem.Enabled;
    this._moveTopPartTypeButtonItem.Enabled = this._moveUpPartTypeButtonItem.Enabled;
    this._moveTopPartTypeMenuButtonItem.Enabled = this._moveTopPartTypeButtonItem.Enabled;
    this._moveDownPartTypeButtonItem.Enabled = flag1 && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && childRelationType != null && selectedTreeRows.Count > 0 && selectedTreeRows[selectedTreeRows.Count - 1].ChildIndex < childRelationType.ChildObjectTypes.Count - 1;
    this._moveDownPartTypeMenuButtonItem.Enabled = this._moveDownPartTypeButtonItem.Enabled;
    this._moveBottomPartTypeButtonItem.Enabled = this._moveDownPartTypeButtonItem.Enabled;
    this._moveBottomPartTypeMenuButtonItem.Enabled = this._moveBottomPartTypeButtonItem.Enabled;
    this._refreshPartTypesButtonItem.Enabled = flag2 && compositionsAutosortRule != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && selectedChildObjectType != null;
    this._refreshPartTypesMenuButtonItems.Enabled = this._refreshPartTypesButtonItem.Enabled;
    this.toolBarParObjTypes.Visible = flag2;
    this.toolBarParTypesPositions.Visible = flag2;
    this.toolBarObjTypes.Visible = flag2 & flag1;
    this.toolBarPositions.Visible = flag1;
    this._defaultObjectListFilterMenuButtonItem.Enabled = this._changeDefaultColumnsSettingsMenuButtonItem.Enabled = this._removeDefaultColumnsSettingsMenuButtonItem.Enabled = this._changeDefaultCommandsSettingsMenuButtonItem.Enabled = this._removeDefaultCommandsSettingsMenuButtonItem.Enabled = this.GetCurrentParentObjectType() != null;
  }

  /// <summary>Вернуть значок для указанного типа связи</summary>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Значок для указанного типа связи</returns>
  private Image GetRelationTypeImage(int relTypeID)
  {
    if (!MetaDataHelper.ExistsRelationType(relTypeID))
      return (Image) null;
    int index = this._categoryTypeIconService.IndexOf(6, relTypeID);
    return index < 0 ? (Image) null : this._categoryTypeIconService.ImageList.Images[index];
  }

  /// <summary>Заполнить дерево родительских типов объектов</summary>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  private void FillParentObjectTypesTree(bool resetDatasource)
  {
    try
    {
      if (resetDatasource)
        this._objectTypesTree.DataSource = (object) this._compositionsAutosortRule;
      this._objectTypesTree.UpdateRows(true);
      this._objectTypesTree.FocusRow = this._objectTypesTree.SelectedRow = this._objectTypesTree.TopRow;
    }
    catch
    {
    }
    this.UpdateControls();
    this.FillChildrenTypes();
  }

  /// <summary>
  /// Нажата кнопка "Переместить допустимый тип связи вверх"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoChildRelationTypeUp()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (selectedRow == null || compositionsAutosortRule == null)
      return;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType1 = this.GetSelectedChildRelationType();
    if (parentObjectType == null || childRelationType1 == null || !(selectedRow.Item is ChildRelationType))
      return;
    int index1 = parentObjectType.ChildRelationTypes.IndexOf(childRelationType1);
    int index2 = index1 - 1;
    if (index1 <= 0)
      return;
    ChildRelationType childRelationType2 = parentObjectType.ChildRelationTypes[index2];
    parentObjectType.ChildRelationTypes[index2] = childRelationType1;
    parentObjectType.ChildRelationTypes[index1] = childRelationType2;
    this.FillParentObjectTypesTree(false);
    this._isChanged = true;
    this.OnChanged();
  }

  /// <summary>Нажата кнопка "Переместить допустимый тип связи вниз"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoChildRelationTypeDown()
  {
    if ((this.RulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) == RulesEditorAccessRights.ReadOnly)
      return;
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    if (selectedRow == null || compositionsAutosortRule == null)
      return;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType1 = this.GetSelectedChildRelationType();
    if (parentObjectType == null || childRelationType1 == null || !(selectedRow.Item is ChildRelationType))
      return;
    int index1 = parentObjectType.ChildRelationTypes.IndexOf(childRelationType1);
    int index2 = index1 + 1;
    if (index1 >= parentObjectType.ChildRelationTypes.Count - 1)
      return;
    ChildRelationType childRelationType2 = parentObjectType.ChildRelationTypes[index2];
    parentObjectType.ChildRelationTypes[index2] = childRelationType1;
    parentObjectType.ChildRelationTypes[index1] = childRelationType2;
    this.FillParentObjectTypesTree(false);
    this._isChanged = true;
    this.OnChanged();
  }

  /// <summary>Заполнить список дочерних типов объектов</summary>
  private void FillChildrenTypes()
  {
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    IMSRelationType relationType = childRelationType != null ? MetaDataHelper.GetRelationType(childRelationType.RelationTypeID) : (IMSRelationType) null;
    IMSObjectType objectType = parentObjectType != null ? MetaDataHelper.GetObjectType(parentObjectType.ObjectTypeID) : (IMSObjectType) null;
    if (relationType != null && objectType != null)
    {
      this.btnRelationType.Image = this.GetRelationTypeImage(childRelationType.RelationTypeID);
      this.lbObjectType.Text = relationType.Description;
      this.lbObjectType.ToolTipText = string.Format(LocalizationHolder.rm.GetString("Client.Core_239"), (object) objectType.ObjectTypeName, (object) relationType.Description);
    }
    else
    {
      this.btnRelationType.Image = (Image) null;
      this.lbObjectType.Text = string.Empty;
      this.lbObjectType.ToolTipText = string.Empty;
    }
    try
    {
      this._partTypesTree.DataSource = (object) childRelationType;
      this._partTypesTree.FocusRow = this._partTypesTree.SelectedRow = this._partTypesTree.TopRow;
    }
    catch
    {
    }
    finally
    {
      this.PartTypesTree_SelectionChanged((object) this, (EventArgs) null);
    }
  }

  /// <summary>Настройка отображения для указанной категории</summary>
  /// <param name="category">Категория</param>
  private void DoEditCategoryViews(int category = 1)
  {
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    this._changeCompositionsViewSettingsMenuButtonItem.Enabled = (this._rulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) != 0 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && childRelationType == null;
    this._changeObjectsViewSettingsMenuButtonItem.Enabled = this._changeCompositionsViewSettingsMenuButtonItem.Enabled;
    if (!this._changeCompositionsViewSettingsMenuButtonItem.Enabled)
      return;
    NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(category, parentObjectType.ObjectTypeID, string.Empty, true, this._navigatorColumnsDictionary);
    int num = navigatorColumns == null ? 1 : 0;
    if (navigatorColumns == null)
      navigatorColumns = this._navigatorColumnsService.CreateNavigatorColumns(category, parentObjectType.ObjectTypeID, string.Empty, this._navigatorColumnsDictionary);
    ObjectTypeNode objectTypeNode = new ObjectTypeNode(parentObjectType.ObjectTypeID, AccessRights.Enabled);
    NodeColumnCollection supportedColumns = objectTypeNode.GetSupportedColumns(ContentType.NonFolders, string.Empty);
    if (num != 0)
      navigatorColumns.Columns = objectTypeNode.GetDefaultColumns(ContentType.NonFolders);
    NodeColumnCollection columns = navigatorColumns.Columns.Clone() as NodeColumnCollection;
    if (AppearanceTuningForm.Execute((INode) objectTypeNode, ContentType.NonFolders, supportedColumns, columns) != DialogResult.OK)
      return;
    navigatorColumns.Columns = columns;
    NavigatorColumnsKey key = new NavigatorColumnsKey(category, parentObjectType.ObjectTypeID, "");
    if (!this._navigatorColumnsDictionary.ContainsKey(key))
      this._navigatorColumnsDictionary.Add(key, (NavigatorColumns) null);
    this._navigatorColumnsDictionary[key] = navigatorColumns;
    this.IsChanged = true;
  }

  /// <summary>
  /// Сбросить настройки отображения для указанной категории
  /// </summary>
  private void DoResetCategoryViews(int category, string infoMsg, string questMsg)
  {
    Row selectedRow = this._objectTypesTree.SelectedRow;
    CompositionsAutosortRule compositionsAutosortRule = this._compositionsAutosortRule;
    ParentObjectType parentObjectType = this.GetCurrentParentObjectType();
    ChildRelationType childRelationType = this.GetSelectedChildRelationType();
    this._removeCompositionsViewSettingsMenuButtonItem.Enabled = (this._rulesEditorAccessRights & RulesEditorAccessRights.CanModifyCurrentRule) != 0 && compositionsAutosortRule != null && parentObjectType != null && ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.SelectedRoleVersionID) && childRelationType == null;
    this._removeObjectsViewSettingsMenuButtonItem.Enabled = this._removeCompositionsViewSettingsMenuButtonItem.Enabled;
    if (!this._removeCompositionsViewSettingsMenuButtonItem.Enabled)
      return;
    NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(category, parentObjectType.ObjectTypeID, string.Empty, false, this._navigatorColumnsDictionary);
    if (navigatorColumns == null)
    {
      int num = (int) MessageBox.Show(infoMsg, LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (MessageBox.Show(questMsg, LocalizationHolder.rm.GetString("Client.Core_1319"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this._navigatorColumnsService.RemoveNavigatorColumns(navigatorColumns.Category, navigatorColumns.Type, navigatorColumns.Suffix, this._navigatorColumnsDictionary);
      this.IsChanged = true;
    }
  }

  private ParentObjectType GetCurrentParentObjectType()
  {
    Row row = this._objectTypesTree.SelectedRow;
    while (row != null && !(row.Item is ParentObjectType))
      row = row.ParentRow;
    return row == null ? (ParentObjectType) null : row.Item as ParentObjectType;
  }

  private ChildRelationType GetSelectedChildRelationType()
  {
    Row selectedRow = this._objectTypesTree.SelectedRow;
    return selectedRow == null ? (ChildRelationType) null : selectedRow.Item as ChildRelationType;
  }

  private ChildObjectType GetSelectedChildObjectType()
  {
    Row row = this._partTypesTree.FocusRow ?? this._partTypesTree.SelectedRow;
    while (row != null && row.Level > 1)
      row = row.ParentRow;
    return row == null ? (ChildObjectType) null : row.Item as ChildObjectType;
  }

  private void SetCurrentRoleConfigurationVersionID()
  {
    if (!ObjectHelper.IsUnknownObjectVersionID(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._compositionsAutosortRule = new CompositionsAutosortRule(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID);
        this._compositionsAutosortRule.Load(sessionKeeper.Session, this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, true);
        this._navigatorColumnsDictionary = this._navigatorColumnsService.LoadFromObject(this._roleConfigurationsEditorControl.CurrentRoleConfigurationVersionID, MetaDataHelper.GetAttributeTypeID("cad01487-306c-11d8-b4e9-00304f19f545"));
        this._defaultColumnPack = (ColumnPack) null;
        this._defaultCommandsSettings = (List<DefaultCommandSettings>) null;
        this._isDefaultCommandsSettingsLoaded = false;
      }
    }
    this.IsChanged = false;
    this.FillParentObjectTypesTree(true);
    this.UpdateControls();
  }

  private void SetChildObjectTypeVisibleRecursive(
    ChildObjectType childObjectType,
    bool visible,
    List<int> changedChildObjectTypes)
  {
    childObjectType.Visible = visible;
    changedChildObjectTypes.Add(childObjectType.ObjectTypeID);
    if (MetaDataHelper.GetObjectType(childObjectType.ObjectTypeID).VersionsMode != ObjectVersionModes.Abstract)
      return;
    foreach (ChildObjectType child in childObjectType.Children)
      this.SetChildObjectTypeVisibleRecursive(child, visible, changedChildObjectTypes);
  }

  private void SetChildObjectTypeVisible(int objectTypeID, bool visible)
  {
    if (!(this._objectTypesTree.SelectedItem is ChildRelationType))
      return;
    foreach (ChildObjectType childObjectType in ((ChildRelationType) this._objectTypesTree.SelectedItem).ChildObjectTypes)
      this.SetChildObjectTypeVisibleRecursive(childObjectType, objectTypeID, visible);
  }

  private void SetChildObjectTypeVisibleRecursive(
    ChildObjectType childObjectType,
    int objectTypeID,
    bool visible)
  {
    if (childObjectType.ObjectTypeID == objectTypeID)
      childObjectType.Visible = visible;
    foreach (ChildObjectType child in childObjectType.Children)
      this.SetChildObjectTypeVisibleRecursive(child, objectTypeID, visible);
  }

  private void SetAbstractChildObjectTypesVisible()
  {
    if (!(this._objectTypesTree.SelectedItem is ChildRelationType))
      return;
    foreach (ChildObjectType childObjectType in ((ChildRelationType) this._objectTypesTree.SelectedItem).ChildObjectTypes)
      this.SetAbstractChildObjectTypesVisible(childObjectType);
  }

  private void SetAbstractChildObjectTypesVisible(ChildObjectType childObjectType)
  {
    if (MetaDataHelper.GetObjectType(childObjectType.ObjectTypeID).VersionsMode == ObjectVersionModes.Abstract)
    {
      childObjectType.Visible = this.IsChildObjectTypeVisible(childObjectType);
    }
    else
    {
      foreach (ChildObjectType child in childObjectType.Children)
        this.SetAbstractChildObjectTypesVisible(child);
    }
  }

  private bool IsChildObjectTypeVisible(ChildObjectType childObjectType)
  {
    return MetaDataHelper.GetObjectType(childObjectType.ObjectTypeID).VersionsMode == ObjectVersionModes.Abstract && childObjectType.Children.Count > 0 ? (childObjectType.Visible = childObjectType.Children.Any<ChildObjectType>((Func<ChildObjectType, bool>) (o => this.IsChildObjectTypeVisible(o)))) : childObjectType.Visible;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarObjTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarParObjTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarParTypesPositions.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarPositions.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuChildTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuParentTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.BarManager_RendererChanged);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutosortRulesEditor));
    this.panelsMain = new SplitContainer();
    this.panelsChild = new SplitContainer();
    this._roleConfigurationsEditorControl = new RoleConfigsEditor();
    this._objectTypesTree = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnCheck = new Column();
    this.cellEditor1 = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.columnParentObjects = new Column();
    this.menuParentTypes = new MenuBar();
    this.imagesMenus = new ImageList(this.components);
    this._objectTypesContextMenuBarItem = new ContextMenuBarItem();
    this._moveUpObjectTypeMenuButtonItem = new MenuButtonItem();
    this._moveDownObjectTypeMenuButtonItem = new MenuButtonItem();
    this._addObjectTypeMenuButtonItem = new MenuButtonItem();
    this._removeObjectTypeMenuButtonItem = new MenuButtonItem();
    this._sortObjectTypesMenuButtonItem = new MenuButtonItem();
    this._refreshObjectTypesMenuButtonItem = new MenuButtonItem();
    this._changeCompositionsViewSettingsMenuButtonItem = new MenuButtonItem();
    this._removeCompositionsViewSettingsMenuButtonItem = new MenuButtonItem();
    this._changeObjectsViewSettingsMenuButtonItem = new MenuButtonItem();
    this._removeObjectsViewSettingsMenuButtonItem = new MenuButtonItem();
    this._changeDefaultColumnsSettingsMenuButtonItem = new MenuButtonItem();
    this._removeDefaultColumnsSettingsMenuButtonItem = new MenuButtonItem();
    this._changeDefaultCommandsSettingsMenuButtonItem = new MenuButtonItem();
    this._removeDefaultCommandsSettingsMenuButtonItem = new MenuButtonItem();
    this._showSelectorsAndClassifiersMenuButtonItem = new MenuButtonItem();
    this.toolBarParTypesPositions = new Intermech.Bars.ToolBar();
    this._moveUpObjectTypeButtonItem = new ButtonItem();
    this._moveDownObjectTypeButtonItem = new ButtonItem();
    this.toolBarParObjTypes = new Intermech.Bars.ToolBar();
    this._addObjectTypeButtonItem = new ButtonItem();
    this._removeObjectTypeButtonItem = new ButtonItem();
    this._sortObjectTypesButtonItem = new ButtonItem();
    this.lbObjType = new LabelItem();
    this._refreshObjectTypesButtonItem = new ButtonItem();
    this._partTypesTree = new Intermech.VirtualTreeView.VirtualTreeView();
    this._visibilityPartTypesTreeColumn = new Column();
    this._objectTypeNamePartTypesTreeColumn = new Column();
    this._groupingPartTypesTreeColumn = new Column();
    this.menuChildTypes = new MenuBar();
    this._partTypesContextMenuBarItem = new ContextMenuBarItem();
    this._moveTopPartTypeMenuButtonItem = new MenuButtonItem();
    this._moveUpPartTypeMenuButtonItem = new MenuButtonItem();
    this._moveDownPartTypeMenuButtonItem = new MenuButtonItem();
    this._moveBottomPartTypeMenuButtonItem = new MenuButtonItem();
    this._refreshPartTypesMenuButtonItems = new MenuButtonItem();
    this.toolBarPositions = new Intermech.Bars.ToolBar();
    this._moveTopPartTypeButtonItem = new ButtonItem();
    this._moveUpPartTypeButtonItem = new ButtonItem();
    this._moveDownPartTypeButtonItem = new ButtonItem();
    this._moveBottomPartTypeButtonItem = new ButtonItem();
    this.toolBarObjTypes = new Intermech.Bars.ToolBar();
    this.lbRelTypeNameHint = new LabelItem();
    this.btnRelationType = new ButtonItem();
    this.lbObjectType = new LabelItem();
    this._refreshPartTypesButtonItem = new ButtonItem();
    this._openFileDialog = new OpenFileDialog();
    this._saveFileDialog = new SaveFileDialog();
    this.tableLayoutPanel = new TableLayoutPanel();
    this._defaultObjectListFilterMenuButtonItem = new MenuButtonItem();
    this.panelsMain.BeginInit();
    this.panelsMain.Panel1.SuspendLayout();
    this.panelsMain.Panel2.SuspendLayout();
    this.panelsMain.SuspendLayout();
    this.panelsChild.BeginInit();
    this.panelsChild.Panel1.SuspendLayout();
    this.panelsChild.Panel2.SuspendLayout();
    this.panelsChild.SuspendLayout();
    this._objectTypesTree.BeginInit();
    this._partTypesTree.BeginInit();
    this.tableLayoutPanel.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel.SetColumnSpan((Control) this.panelsMain, 3);
    componentResourceManager.ApplyResources((object) this.panelsMain, "panelsMain");
    this.panelsMain.Name = "panelsMain";
    this.panelsMain.Panel1.Controls.Add((Control) this.panelsChild);
    this.panelsMain.Panel2.Controls.Add((Control) this._partTypesTree);
    this.panelsMain.Panel2.Controls.Add((Control) this.menuChildTypes);
    this.panelsMain.Panel2.Controls.Add((Control) this.toolBarPositions);
    this.panelsMain.Panel2.Controls.Add((Control) this.toolBarObjTypes);
    componentResourceManager.ApplyResources((object) this.panelsChild, "panelsChild");
    this.panelsChild.FixedPanel = FixedPanel.Panel1;
    this.panelsChild.Name = "panelsChild";
    this.panelsChild.Panel1.Controls.Add((Control) this._roleConfigurationsEditorControl);
    this.panelsChild.Panel2.Controls.Add((Control) this._objectTypesTree);
    this.panelsChild.Panel2.Controls.Add((Control) this.menuParentTypes);
    this.panelsChild.Panel2.Controls.Add((Control) this.toolBarParTypesPositions);
    this.panelsChild.Panel2.Controls.Add((Control) this.toolBarParObjTypes);
    this._roleConfigurationsEditorControl.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this._roleConfigurationsEditorControl, "_roleConfigurationsEditorControl");
    this._roleConfigurationsEditorControl.Name = "_roleConfigurationsEditorControl";
    this._roleConfigurationsEditorControl.Tag = (object) "  ";
    this._roleConfigurationsEditorControl.LoadRoleConfigurationFromFile += new EventHandler(this.RoleConfigurationsEditorControl_LoadRoleConfigurationFromFile);
    this._roleConfigurationsEditorControl.SaveRoleConfigurationToFile += new EventHandler(this.RoleConfigurationsEditorControl_SaveRoleConfigurationToFile);
    this._roleConfigurationsEditorControl.CurrentRoleConfigurationChanged += new EventHandler(this.RoleConfigurationsEditorControl_CurrentRoleConfigurationChanged);
    this._roleConfigurationsEditorControl.CurrentRoleConfigurationChanging += new EventHandler<CancelEventArgs>(this.RoleConfigurationsEditorControl_CurrentRoleConfigurationChanging);
    this._objectTypesTree.AllowDrop = true;
    this._objectTypesTree.AllowIndividualRowResize = false;
    this._objectTypesTree.AllowMultiSelect = false;
    this._objectTypesTree.AllowRowResize = false;
    this._objectTypesTree.AllowUserPinnedColumns = false;
    this._objectTypesTree.AutoFitColumns = true;
    this._objectTypesTree.Columns.Add(this.columnCheck);
    this._objectTypesTree.Columns.Add(this.columnParentObjects);
    this._objectTypesTree.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this._objectTypesTree, "_objectTypesTree");
    this._objectTypesTree.Editors.Add(this.cellEditor1);
    this._objectTypesTree.ImageList = (ImageList) null;
    this._objectTypesTree.LineStyle = LineStyle.Dot;
    this._objectTypesTree.MainColumn = this.columnParentObjects;
    this._objectTypesTree.Name = "_objectTypesTree";
    this._objectTypesTree.PrefixColumn = this.columnCheck;
    this._objectTypesTree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._objectTypesTree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._objectTypesTree.SelectBeforeEdit = true;
    this._objectTypesTree.ShowRootRow = false;
    this._objectTypesTree.SuppressErrorMessages = true;
    this._objectTypesTree.ShowContextMenu += new MouseEventHandler(this.ObjectTypesTree_ShowContextMenu);
    this._objectTypesTree.FocusRowChanged += new EventHandler(this.ObjectTypesTree_SelectionChanged);
    this._objectTypesTree.GetCellData += new GetCellDataHandler(this.ObjectTypesTree_GetCellData);
    this._objectTypesTree.GetChildren += new GetChildrenHandler(this.ObjectTypesTree_GetChildren);
    this._objectTypesTree.GetRowData += new GetRowDataHandler(this.ObjectTypesTree_GetRowData);
    this._objectTypesTree.SelectionChanged += new EventHandler(this.ObjectTypesTree_SelectionChanged);
    this._objectTypesTree.SetCellValue += new SetCellValueHandler(this.ObjectTypesTree_SetCellValue);
    componentResourceManager.ApplyResources((object) this.columnCheck, "columnCheck");
    this.columnCheck.CellEditor = this.cellEditor1;
    this.columnCheck.Movable = false;
    this.columnCheck.Name = "columnCheck";
    this.columnCheck.Resizable = false;
    this.columnCheck.Sortable = false;
    this.cellEditor1.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditor1.Control = (Control) this.checkBox1;
    this.cellEditor1.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditor1.UseCellHeight = false;
    this.cellEditor1.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    componentResourceManager.ApplyResources((object) this.columnParentObjects, "columnParentObjects");
    this.columnParentObjects.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnParentObjects.HeaderStyle.HorzAlignment");
    this.columnParentObjects.Movable = false;
    this.columnParentObjects.Name = "columnParentObjects";
    this.columnParentObjects.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menuParentTypes, "menuParentTypes");
    this.menuParentTypes.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuParentTypes.Hidden = false;
    this.menuParentTypes.ImageList = this.imagesMenus;
    this.menuParentTypes.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._objectTypesContextMenuBarItem
    });
    this.menuParentTypes.Name = "menuParentTypes";
    this.menuParentTypes.OwnerForm = (Form) null;
    this.imagesMenus.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesMenus.ImageStream");
    this.imagesMenus.TransparentColor = Color.Transparent;
    this.imagesMenus.Images.SetKeyName(0, "");
    this.imagesMenus.Images.SetKeyName(1, "сохранить.png");
    this.imagesMenus.Images.SetKeyName(2, "обновить.png");
    this.imagesMenus.Images.SetKeyName(3, "arrow_up_blue.ico");
    this.imagesMenus.Images.SetKeyName(4, "arrow_down_blue.ico");
    this.imagesMenus.Images.SetKeyName(5, "arrow_top_blue.ico");
    this.imagesMenus.Images.SetKeyName(6, "arrow_bottom_blue.ico");
    this.imagesMenus.Images.SetKeyName(7, "");
    this.imagesMenus.Images.SetKeyName(8, "");
    this.imagesMenus.Images.SetKeyName(9, "");
    this.imagesMenus.Images.SetKeyName(10, "");
    this.imagesMenus.Images.SetKeyName(11, "");
    this.imagesMenus.Images.SetKeyName(12, "");
    this.imagesMenus.Images.SetKeyName(13, "удалить.png");
    this.imagesMenus.Images.SetKeyName(14, "folder.png");
    this.imagesMenus.Images.SetKeyName(15, "folder_ok.png");
    this.imagesMenus.Images.SetKeyName(16 /*0x10*/, "вырезать.png");
    this.imagesMenus.Images.SetKeyName(17, "копировать.png");
    this.imagesMenus.Images.SetKeyName(18, "вставить.png");
    this.imagesMenus.Images.SetKeyName(19, "manual_sort_eng.ico");
    componentResourceManager.ApplyResources((object) this._objectTypesContextMenuBarItem, "_objectTypesContextMenuBarItem");
    this._objectTypesContextMenuBarItem.Items.AddRange(new ToolbarItemBase[16 /*0x10*/]
    {
      (ToolbarItemBase) this._moveUpObjectTypeMenuButtonItem,
      (ToolbarItemBase) this._moveDownObjectTypeMenuButtonItem,
      (ToolbarItemBase) this._addObjectTypeMenuButtonItem,
      (ToolbarItemBase) this._removeObjectTypeMenuButtonItem,
      (ToolbarItemBase) this._sortObjectTypesMenuButtonItem,
      (ToolbarItemBase) this._refreshObjectTypesMenuButtonItem,
      (ToolbarItemBase) this._changeCompositionsViewSettingsMenuButtonItem,
      (ToolbarItemBase) this._removeCompositionsViewSettingsMenuButtonItem,
      (ToolbarItemBase) this._changeObjectsViewSettingsMenuButtonItem,
      (ToolbarItemBase) this._removeObjectsViewSettingsMenuButtonItem,
      (ToolbarItemBase) this._changeDefaultColumnsSettingsMenuButtonItem,
      (ToolbarItemBase) this._removeDefaultColumnsSettingsMenuButtonItem,
      (ToolbarItemBase) this._changeDefaultCommandsSettingsMenuButtonItem,
      (ToolbarItemBase) this._removeDefaultCommandsSettingsMenuButtonItem,
      (ToolbarItemBase) this._showSelectorsAndClassifiersMenuButtonItem,
      (ToolbarItemBase) this._defaultObjectListFilterMenuButtonItem
    });
    this._objectTypesContextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._moveUpObjectTypeMenuButtonItem, "_moveUpObjectTypeMenuButtonItem");
    this._moveUpObjectTypeMenuButtonItem.ImageIndex = 3;
    this._moveUpObjectTypeMenuButtonItem.ShowText = true;
    this._moveUpObjectTypeMenuButtonItem.Click += new EventHandler(this.MoveUpObjectTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveDownObjectTypeMenuButtonItem, "_moveDownObjectTypeMenuButtonItem");
    this._moveDownObjectTypeMenuButtonItem.ImageIndex = 4;
    this._moveDownObjectTypeMenuButtonItem.ShowText = true;
    this._moveDownObjectTypeMenuButtonItem.Click += new EventHandler(this.MoveDownObjectTypeMenuButtonItem_Click);
    this._addObjectTypeMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addObjectTypeMenuButtonItem, "_addObjectTypeMenuButtonItem");
    this._addObjectTypeMenuButtonItem.ImageIndex = 7;
    this._addObjectTypeMenuButtonItem.ShowText = true;
    this._addObjectTypeMenuButtonItem.Click += new EventHandler(this.AddObjectTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeObjectTypeMenuButtonItem, "_removeObjectTypeMenuButtonItem");
    this._removeObjectTypeMenuButtonItem.ImageIndex = 8;
    this._removeObjectTypeMenuButtonItem.ShowText = true;
    this._removeObjectTypeMenuButtonItem.Click += new EventHandler(this.RemoveObjectTypeMenuButtonItem_Click);
    this._sortObjectTypesMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._sortObjectTypesMenuButtonItem, "_sortObjectTypesMenuButtonItem");
    this._sortObjectTypesMenuButtonItem.ImageIndex = 19;
    this._sortObjectTypesMenuButtonItem.ShowText = true;
    this._sortObjectTypesMenuButtonItem.Click += new EventHandler(this.SortObjectTypesMenuButtonItem_Click);
    this._refreshObjectTypesMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshObjectTypesMenuButtonItem, "_refreshObjectTypesMenuButtonItem");
    this._refreshObjectTypesMenuButtonItem.ImageIndex = 2;
    this._refreshObjectTypesMenuButtonItem.ShowText = true;
    this._refreshObjectTypesMenuButtonItem.Click += new EventHandler(this.RefreshObjectTypesMenuButtonItem_Click);
    this._changeCompositionsViewSettingsMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._changeCompositionsViewSettingsMenuButtonItem, "_changeCompositionsViewSettingsMenuButtonItem");
    this._changeCompositionsViewSettingsMenuButtonItem.ShowText = true;
    this._changeCompositionsViewSettingsMenuButtonItem.Click += new EventHandler(this.ChangeCompositionsViewSettingsMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeCompositionsViewSettingsMenuButtonItem, "_removeCompositionsViewSettingsMenuButtonItem");
    this._removeCompositionsViewSettingsMenuButtonItem.ShowText = true;
    this._removeCompositionsViewSettingsMenuButtonItem.Click += new EventHandler(this.RemoveCompositionsViewSettingsMenuButtonItem_Click);
    this._changeObjectsViewSettingsMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._changeObjectsViewSettingsMenuButtonItem, "_changeObjectsViewSettingsMenuButtonItem");
    this._changeObjectsViewSettingsMenuButtonItem.ShowText = true;
    this._changeObjectsViewSettingsMenuButtonItem.Click += new EventHandler(this.ChangeObjectsViewSettingsMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeObjectsViewSettingsMenuButtonItem, "_removeObjectsViewSettingsMenuButtonItem");
    this._removeObjectsViewSettingsMenuButtonItem.ShowText = true;
    this._removeObjectsViewSettingsMenuButtonItem.Click += new EventHandler(this.RemoveObjectsViewSettingsMenuButtonItem_Click);
    this._changeDefaultColumnsSettingsMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._changeDefaultColumnsSettingsMenuButtonItem, "_changeDefaultColumnsSettingsMenuButtonItem");
    this._changeDefaultColumnsSettingsMenuButtonItem.ShowText = true;
    this._changeDefaultColumnsSettingsMenuButtonItem.Click += new EventHandler(this.ChangeDefaultColumnsSettingsMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeDefaultColumnsSettingsMenuButtonItem, "_removeDefaultColumnsSettingsMenuButtonItem");
    this._removeDefaultColumnsSettingsMenuButtonItem.ShowText = true;
    this._removeDefaultColumnsSettingsMenuButtonItem.Click += new EventHandler(this.RemoveDefaultColumnsSettingsMenuButtonItem_Click);
    this._changeDefaultCommandsSettingsMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._changeDefaultCommandsSettingsMenuButtonItem, "_changeDefaultCommandsSettingsMenuButtonItem");
    this._changeDefaultCommandsSettingsMenuButtonItem.ShowText = true;
    this._changeDefaultCommandsSettingsMenuButtonItem.Click += new EventHandler(this.ChangeDefaultCommandsSettingsMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeDefaultCommandsSettingsMenuButtonItem, "_removeDefaultCommandsSettingsMenuButtonItem");
    this._removeDefaultCommandsSettingsMenuButtonItem.ShowText = true;
    this._removeDefaultCommandsSettingsMenuButtonItem.Click += new EventHandler(this.RemoveDefaultCommandsSettingsMenuButtonItem_Click);
    this._showSelectorsAndClassifiersMenuButtonItem.AutoToggle = AutoToggleType.Single;
    this._showSelectorsAndClassifiersMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._showSelectorsAndClassifiersMenuButtonItem, "_showSelectorsAndClassifiersMenuButtonItem");
    this._showSelectorsAndClassifiersMenuButtonItem.ShowText = true;
    this._showSelectorsAndClassifiersMenuButtonItem.Click += new EventHandler(this.ShowSelectorsAndClassifiersMenuButtonItem_Click);
    this.toolBarParTypesPositions.AddRemoveButtonsVisible = false;
    this.toolBarParTypesPositions.AllowHorizontalDock = false;
    this.toolBarParTypesPositions.Closable = false;
    componentResourceManager.ApplyResources((object) this.toolBarParTypesPositions, "toolBarParTypesPositions");
    this.toolBarParTypesPositions.DockLine = 3;
    this.toolBarParTypesPositions.DrawActionsButton = false;
    this.toolBarParTypesPositions.Flow = ToolBarLayout.Vertical;
    this.toolBarParTypesPositions.FullMenus = true;
    this.toolBarParTypesPositions.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarParTypesPositions.Hidden = false;
    this.toolBarParTypesPositions.ImageList = this.imagesMenus;
    this.toolBarParTypesPositions.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this._moveUpObjectTypeButtonItem,
      (ToolbarItemBase) this._moveDownObjectTypeButtonItem
    });
    this.toolBarParTypesPositions.MinimumFloatingSize = new Size(250, 30);
    this.toolBarParTypesPositions.Movable = false;
    this.toolBarParTypesPositions.Name = "toolBarParTypesPositions";
    this.toolBarParTypesPositions.Overflow = ToolBarOverflow.Wrap;
    this.toolBarParTypesPositions.Stretch = true;
    this.toolBarParTypesPositions.Tearable = false;
    this._moveUpObjectTypeButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._moveUpObjectTypeButtonItem, "_moveUpObjectTypeButtonItem");
    this._moveUpObjectTypeButtonItem.Enabled = false;
    this._moveUpObjectTypeButtonItem.ImageIndex = 3;
    this._moveUpObjectTypeButtonItem.Click += new EventHandler(this.MoveUpObjectTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveDownObjectTypeButtonItem, "_moveDownObjectTypeButtonItem");
    this._moveDownObjectTypeButtonItem.Enabled = false;
    this._moveDownObjectTypeButtonItem.ImageIndex = 4;
    this._moveDownObjectTypeButtonItem.Click += new EventHandler(this.MoveDownObjectTypeMenuButtonItem_Click);
    this.toolBarParObjTypes.AddRemoveButtonsVisible = false;
    this.toolBarParObjTypes.AllowHorizontalDock = false;
    this.toolBarParObjTypes.Closable = false;
    this.toolBarParObjTypes.DockLine = 3;
    this.toolBarParObjTypes.DrawActionsButton = false;
    this.toolBarParObjTypes.FullMenus = true;
    this.toolBarParObjTypes.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarParObjTypes.Hidden = false;
    this.toolBarParObjTypes.ImageList = this.imagesMenus;
    this.toolBarParObjTypes.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this._addObjectTypeButtonItem,
      (ToolbarItemBase) this._removeObjectTypeButtonItem,
      (ToolbarItemBase) this._sortObjectTypesButtonItem,
      (ToolbarItemBase) this.lbObjType,
      (ToolbarItemBase) this._refreshObjectTypesButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBarParObjTypes, "toolBarParObjTypes");
    this.toolBarParObjTypes.MinimumFloatingSize = new Size(250, 30);
    this.toolBarParObjTypes.Movable = false;
    this.toolBarParObjTypes.Name = "toolBarParObjTypes";
    this.toolBarParObjTypes.Overflow = ToolBarOverflow.Wrap;
    this.toolBarParObjTypes.Stretch = true;
    this.toolBarParObjTypes.Tearable = false;
    this._addObjectTypeButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addObjectTypeButtonItem, "_addObjectTypeButtonItem");
    this._addObjectTypeButtonItem.Enabled = false;
    this._addObjectTypeButtonItem.ImageIndex = 7;
    this._addObjectTypeButtonItem.Click += new EventHandler(this.AddObjectTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeObjectTypeButtonItem, "_removeObjectTypeButtonItem");
    this._removeObjectTypeButtonItem.Enabled = false;
    this._removeObjectTypeButtonItem.ImageIndex = 8;
    this._removeObjectTypeButtonItem.Click += new EventHandler(this.RemoveObjectTypeMenuButtonItem_Click);
    this._sortObjectTypesButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._sortObjectTypesButtonItem, "_sortObjectTypesButtonItem");
    this._sortObjectTypesButtonItem.Enabled = false;
    this._sortObjectTypesButtonItem.ImageIndex = 19;
    this._sortObjectTypesButtonItem.Click += new EventHandler(this.SortObjectTypesMenuButtonItem_Click);
    this.lbObjType.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.lbObjType, "lbObjType");
    this.lbObjType.Enabled = false;
    this.lbObjType.Stretch = true;
    this._refreshObjectTypesButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshObjectTypesButtonItem, "_refreshObjectTypesButtonItem");
    this._refreshObjectTypesButtonItem.Enabled = false;
    this._refreshObjectTypesButtonItem.ImageIndex = 2;
    this._refreshObjectTypesButtonItem.Click += new EventHandler(this.RefreshObjectTypesMenuButtonItem_Click);
    this._partTypesTree.AllowDrop = true;
    this._partTypesTree.AllowIndividualRowResize = false;
    this._partTypesTree.AllowRowResize = false;
    this._partTypesTree.AllowUserPinnedColumns = false;
    this._partTypesTree.AutoFitColumns = true;
    this._partTypesTree.Columns.Add(this._visibilityPartTypesTreeColumn);
    this._partTypesTree.Columns.Add(this._objectTypeNamePartTypesTreeColumn);
    this._partTypesTree.Columns.Add(this._groupingPartTypesTreeColumn);
    this._partTypesTree.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this._partTypesTree, "_partTypesTree");
    this._partTypesTree.ImageList = (ImageList) null;
    this._partTypesTree.LineStyle = LineStyle.Dot;
    this._partTypesTree.MainColumn = this._objectTypeNamePartTypesTreeColumn;
    this._partTypesTree.Name = "_partTypesTree";
    this._partTypesTree.PrefixColumn = this._visibilityPartTypesTreeColumn;
    this._partTypesTree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._partTypesTree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._partTypesTree.SelectBeforeEdit = true;
    this._partTypesTree.ShowRootRow = false;
    this._partTypesTree.SuppressErrorMessages = true;
    this._partTypesTree.ShowContextMenu += new MouseEventHandler(this.PartTypesTree_ShowContextMenu);
    this._partTypesTree.FocusRowChanged += new EventHandler(this.PartTypesTree_FocusRowChanged);
    this._partTypesTree.GetCellData += new GetCellDataHandler(this.PartTypesTree_GetCellData);
    this._partTypesTree.GetChildren += new GetChildrenHandler(this.PartTypesTree_GetChildren);
    this._partTypesTree.GetRowData += new GetRowDataHandler(this.PartTypesTree_GetRowData);
    this._partTypesTree.SelectionChanged += new EventHandler(this.PartTypesTree_SelectionChanged);
    this._partTypesTree.SetCellValue += new SetCellValueHandler(this.PartTypesTree_SetCellValue);
    componentResourceManager.ApplyResources((object) this._visibilityPartTypesTreeColumn, "_visibilityPartTypesTreeColumn");
    this._visibilityPartTypesTreeColumn.CellEditor = this.cellEditor1;
    this._visibilityPartTypesTreeColumn.Movable = false;
    this._visibilityPartTypesTreeColumn.Name = "_visibilityPartTypesTreeColumn";
    this._visibilityPartTypesTreeColumn.Resizable = false;
    this._visibilityPartTypesTreeColumn.Sortable = false;
    componentResourceManager.ApplyResources((object) this._objectTypeNamePartTypesTreeColumn, "_objectTypeNamePartTypesTreeColumn");
    this._objectTypeNamePartTypesTreeColumn.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("_objectTypeNamePartTypesTreeColumn.HeaderStyle.HorzAlignment");
    this._objectTypeNamePartTypesTreeColumn.Movable = false;
    this._objectTypeNamePartTypesTreeColumn.Name = "_objectTypeNamePartTypesTreeColumn";
    this._objectTypeNamePartTypesTreeColumn.Sortable = false;
    componentResourceManager.ApplyResources((object) this._groupingPartTypesTreeColumn, "_groupingPartTypesTreeColumn");
    this._groupingPartTypesTreeColumn.CellEditor = this.cellEditor1;
    this._groupingPartTypesTreeColumn.Name = "_groupingPartTypesTreeColumn";
    this._groupingPartTypesTreeColumn.Resizable = false;
    this._groupingPartTypesTreeColumn.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menuChildTypes, "menuChildTypes");
    this.menuChildTypes.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuChildTypes.Hidden = false;
    this.menuChildTypes.ImageList = this.imagesMenus;
    this.menuChildTypes.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._partTypesContextMenuBarItem
    });
    this.menuChildTypes.Name = "menuChildTypes";
    this.menuChildTypes.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this._partTypesContextMenuBarItem, "_partTypesContextMenuBarItem");
    this._partTypesContextMenuBarItem.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this._moveTopPartTypeMenuButtonItem,
      (ToolbarItemBase) this._moveUpPartTypeMenuButtonItem,
      (ToolbarItemBase) this._moveDownPartTypeMenuButtonItem,
      (ToolbarItemBase) this._moveBottomPartTypeMenuButtonItem,
      (ToolbarItemBase) this._refreshPartTypesMenuButtonItems
    });
    this._partTypesContextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._moveTopPartTypeMenuButtonItem, "_moveTopPartTypeMenuButtonItem");
    this._moveTopPartTypeMenuButtonItem.ImageIndex = 5;
    this._moveTopPartTypeMenuButtonItem.ShowText = true;
    this._moveTopPartTypeMenuButtonItem.Click += new EventHandler(this.MoveTopPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveUpPartTypeMenuButtonItem, "_moveUpPartTypeMenuButtonItem");
    this._moveUpPartTypeMenuButtonItem.ImageIndex = 3;
    this._moveUpPartTypeMenuButtonItem.ShowText = true;
    this._moveUpPartTypeMenuButtonItem.Click += new EventHandler(this.MoveUpPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveDownPartTypeMenuButtonItem, "_moveDownPartTypeMenuButtonItem");
    this._moveDownPartTypeMenuButtonItem.ImageIndex = 4;
    this._moveDownPartTypeMenuButtonItem.ShowText = true;
    this._moveDownPartTypeMenuButtonItem.Click += new EventHandler(this.MoveDownPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveBottomPartTypeMenuButtonItem, "_moveBottomPartTypeMenuButtonItem");
    this._moveBottomPartTypeMenuButtonItem.ImageIndex = 6;
    this._moveBottomPartTypeMenuButtonItem.ShowText = true;
    this._moveBottomPartTypeMenuButtonItem.Click += new EventHandler(this.MoveBottomPartTypeMenuButtonItem_Click);
    this._refreshPartTypesMenuButtonItems.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshPartTypesMenuButtonItems, "_refreshPartTypesMenuButtonItems");
    this._refreshPartTypesMenuButtonItems.ImageIndex = 2;
    this._refreshPartTypesMenuButtonItems.ShowText = true;
    this._refreshPartTypesMenuButtonItems.Click += new EventHandler(this.RefreshPartTypesMenuButtonItems_Click);
    this.toolBarPositions.AddRemoveButtonsVisible = false;
    this.toolBarPositions.AllowHorizontalDock = false;
    this.toolBarPositions.Closable = false;
    componentResourceManager.ApplyResources((object) this.toolBarPositions, "toolBarPositions");
    this.toolBarPositions.DockLine = 3;
    this.toolBarPositions.DrawActionsButton = false;
    this.toolBarPositions.Flow = ToolBarLayout.Vertical;
    this.toolBarPositions.FullMenus = true;
    this.toolBarPositions.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarPositions.Hidden = false;
    this.toolBarPositions.ImageList = this.imagesMenus;
    this.toolBarPositions.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this._moveTopPartTypeButtonItem,
      (ToolbarItemBase) this._moveUpPartTypeButtonItem,
      (ToolbarItemBase) this._moveDownPartTypeButtonItem,
      (ToolbarItemBase) this._moveBottomPartTypeButtonItem
    });
    this.toolBarPositions.MinimumFloatingSize = new Size(250, 30);
    this.toolBarPositions.Movable = false;
    this.toolBarPositions.Name = "toolBarPositions";
    this.toolBarPositions.Overflow = ToolBarOverflow.Wrap;
    this.toolBarPositions.Stretch = true;
    this.toolBarPositions.Tearable = false;
    componentResourceManager.ApplyResources((object) this._moveTopPartTypeButtonItem, "_moveTopPartTypeButtonItem");
    this._moveTopPartTypeButtonItem.Enabled = false;
    this._moveTopPartTypeButtonItem.ImageIndex = 5;
    this._moveTopPartTypeButtonItem.Click += new EventHandler(this.MoveTopPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveUpPartTypeButtonItem, "_moveUpPartTypeButtonItem");
    this._moveUpPartTypeButtonItem.Enabled = false;
    this._moveUpPartTypeButtonItem.ImageIndex = 3;
    this._moveUpPartTypeButtonItem.Click += new EventHandler(this.MoveUpPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveDownPartTypeButtonItem, "_moveDownPartTypeButtonItem");
    this._moveDownPartTypeButtonItem.Enabled = false;
    this._moveDownPartTypeButtonItem.ImageIndex = 4;
    this._moveDownPartTypeButtonItem.Click += new EventHandler(this.MoveDownPartTypeMenuButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._moveBottomPartTypeButtonItem, "_moveBottomPartTypeButtonItem");
    this._moveBottomPartTypeButtonItem.Enabled = false;
    this._moveBottomPartTypeButtonItem.ImageIndex = 6;
    this._moveBottomPartTypeButtonItem.Click += new EventHandler(this.MoveBottomPartTypeMenuButtonItem_Click);
    this.toolBarObjTypes.AddRemoveButtonsVisible = false;
    this.toolBarObjTypes.AllowHorizontalDock = false;
    this.toolBarObjTypes.Closable = false;
    this.toolBarObjTypes.DockLine = 3;
    this.toolBarObjTypes.DrawActionsButton = false;
    this.toolBarObjTypes.FullMenus = true;
    this.toolBarObjTypes.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarObjTypes.Hidden = false;
    this.toolBarObjTypes.ImageList = this.imagesMenus;
    this.toolBarObjTypes.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.lbRelTypeNameHint,
      (ToolbarItemBase) this.btnRelationType,
      (ToolbarItemBase) this.lbObjectType,
      (ToolbarItemBase) this._refreshPartTypesButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBarObjTypes, "toolBarObjTypes");
    this.toolBarObjTypes.MinimumFloatingSize = new Size(250, 30);
    this.toolBarObjTypes.Movable = false;
    this.toolBarObjTypes.Name = "toolBarObjTypes";
    this.toolBarObjTypes.Overflow = ToolBarOverflow.Wrap;
    this.toolBarObjTypes.Stretch = true;
    this.toolBarObjTypes.Tearable = false;
    componentResourceManager.ApplyResources((object) this.lbRelTypeNameHint, "lbRelTypeNameHint");
    componentResourceManager.ApplyResources((object) this.btnRelationType, "btnRelationType");
    this.btnRelationType.Enabled = false;
    this.btnRelationType.MinimumSize = 26;
    componentResourceManager.ApplyResources((object) this.lbObjectType, "lbObjectType");
    this.lbObjectType.MinimumSize = 26;
    this.lbObjectType.Stretch = true;
    this._refreshPartTypesButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._refreshPartTypesButtonItem, "_refreshPartTypesButtonItem");
    this._refreshPartTypesButtonItem.Enabled = false;
    this._refreshPartTypesButtonItem.ImageIndex = 2;
    this._refreshPartTypesButtonItem.Click += new EventHandler(this.RefreshPartTypesMenuButtonItems_Click);
    this._openFileDialog.DefaultExt = "autosort";
    componentResourceManager.ApplyResources((object) this._openFileDialog, "_openFileDialog");
    this._openFileDialog.ShowReadOnly = true;
    this._openFileDialog.SupportMultiDottedExtensions = true;
    this._openFileDialog.RestoreDirectory = true;
    this._saveFileDialog.DefaultExt = "autosort";
    componentResourceManager.ApplyResources((object) this._saveFileDialog, "_saveFileDialog");
    this._saveFileDialog.SupportMultiDottedExtensions = true;
    this._saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
    this.tableLayoutPanel.Controls.Add((Control) this.panelsMain, 0, 1);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    this._defaultObjectListFilterMenuButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._defaultObjectListFilterMenuButtonItem, "_defaultObjectListFilterMenuButtonItem");
    this._defaultObjectListFilterMenuButtonItem.ShowText = true;
    this._defaultObjectListFilterMenuButtonItem.Click += new EventHandler(this.DefaultObjectListFilterMenuButtonItem_Click);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.tableLayoutPanel);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (AutosortRulesEditor);
    this.Tag = (object) "  ";
    this.panelsMain.Panel1.ResumeLayout(false);
    this.panelsMain.Panel2.ResumeLayout(false);
    this.panelsMain.EndInit();
    this.panelsMain.ResumeLayout(false);
    this.panelsChild.Panel1.ResumeLayout(false);
    this.panelsChild.Panel2.ResumeLayout(false);
    this.panelsChild.EndInit();
    this.panelsChild.ResumeLayout(false);
    this._objectTypesTree.EndInit();
    this._partTypesTree.EndInit();
    this.tableLayoutPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Сравнить две строки по их позиции в дереве</summary>
  private class CompareRows : IComparer<Row>
  {
    /// <summary>Сравнить две строки по позициям в дереве</summary>
    /// <param name="x">Первая строка</param>
    /// <param name="y">Вторая строка</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(Row x, Row y)
    {
      return x == null || y == null ? 0 : x.ChildIndex.CompareTo(y.ChildIndex);
    }
  }

  /// <summary>Класс для сравнения положения дочерних типов объектов</summary>
  private class ChildObjectTypesComparer : IComparer<ChildObjectType>
  {
    /// <summary>Допустимый тип связи</summary>
    private ChildRelationType _relType;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="relType">Допустимый тип связи</param>
    public ChildObjectTypesComparer(ChildRelationType relType) => this._relType = relType;

    /// <summary>Сравнить два дочерних типа объекта</summary>
    /// <param name="x">Первый дочерний тип объекта</param>
    /// <param name="y">Второй дочерний тип объекта</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(ChildObjectType x, ChildObjectType y)
    {
      return x == null || y == null ? 0 : this._relType.ChildObjectTypes.IndexOf(x).CompareTo(this._relType.ChildObjectTypes.IndexOf(y));
    }
  }
}
