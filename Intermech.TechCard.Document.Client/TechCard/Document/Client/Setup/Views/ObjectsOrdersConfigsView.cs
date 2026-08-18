// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.Views.ObjectsOrdersConfigsView
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup.Views;

public class ObjectsOrdersConfigsView : UserControl, IConfigView
{
  [NotNull]
  private IConfigViewController _controller;
  [NotNull]
  private IConfigViewSettings _settings;
  [NotNull]
  private IDocumentConfigElement _originConfig;
  [NotNull]
  private TPStructureObjectsConfigs _configEditableCache;
  private SortedList<string, TPStructureObjectConfig> _objectsConfigsTreeCache = new SortedList<string, TPStructureObjectConfig>();
  private Dictionary<int, Icon> _objectOrdersIcons = new Dictionary<int, Icon>();
  private IContainer components;
  private ContextMenuStrip ctxmenuObjectsOrdersTree;
  private ToolStripMenuItem miAddObjectType;
  private ToolStripMenuItem miInsertObjectType;
  private ToolStripMenuItem miRemoveObjectType;
  private ToolStripMenuItem miMoveObjectType;
  private ToolStripMenuItem miMakeFirstObjectType;
  private ToolStripMenuItem miMoveUpObjectType;
  private ToolStripMenuItem miMoveDownObjectType;
  private ToolStripMenuItem miMakeLastObjectType;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem miResetToDefault;
  private Intermech.VirtualTreeView.VirtualTreeView vtvObjectsOrders;
  private Column colObjectName;

  [CanBeNull]
  private Icon LoadIconByObjTypeId(int objTypeId, ICategoryTypeIconService iconService)
  {
    return iconService.GetIcon(4, objTypeId) ?? (Icon) null;
  }

  private void InitObjectOrdersTreeIcons()
  {
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    foreach (TPStructureObjectConfig config1 in this._configEditableCache.Configs)
    {
      if (!this._objectOrdersIcons.ContainsKey(config1.ObjectType.ObjectTypeID))
      {
        Icon icon = this.LoadIconByObjTypeId(config1.ObjectType.ObjectTypeID, service);
        if (icon != null)
          this._objectOrdersIcons.Add(config1.ObjectType.ObjectTypeID, icon);
      }
      foreach (ObjectOrderConfig config2 in config1.ChildsOrdersConfigs.Configs)
      {
        if (!this._objectOrdersIcons.ContainsKey(config2.ObjectType.ObjectTypeID))
        {
          Icon icon = this.LoadIconByObjTypeId(config2.ObjectType.ObjectTypeID, service);
          if (icon != null)
            this._objectOrdersIcons.Add(config2.ObjectType.ObjectTypeID, icon);
        }
      }
    }
  }

  private void BuildObjectsOrdersTree()
  {
    this.InitObjectOrdersTreeIcons();
    this._objectsConfigsTreeCache.Clear();
    foreach (TPStructureObjectConfig config in this._configEditableCache.Configs)
      this._objectsConfigsTreeCache.Add(config.ObjectType.ObjectTypeName, config);
    this.vtvObjectsOrders.DataSource = (object) null;
    this.vtvObjectsOrders.DataSource = (object) this._objectsConfigsTreeCache;
  }

  private void vtvObjectsOrders_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item == this._objectsConfigsTreeCache)
    {
      e.Children = (IList) this._objectsConfigsTreeCache.Values.ToList<TPStructureObjectConfig>();
    }
    else
    {
      object obj = e.Row.Item;
      if (obj == null || !(obj is TPStructureObjectConfig structureObjectConfig))
        return;
      List<ObjectOrderConfig> list = structureObjectConfig.ChildsOrdersConfigs.Configs.ToList<ObjectOrderConfig>();
      list.Sort((Comparison<ObjectOrderConfig>) ((left, right) => left.Order - right.Order));
      e.Children = (IList) list;
    }
  }

  private void vtvObjectsOrders_GetRowData(object sender, GetRowDataEventArgs e)
  {
    Icon icon = (Icon) null;
    switch (e.Row.Item)
    {
      case TPStructureObjectConfig structureObjectConfig:
        if (!this._objectOrdersIcons.TryGetValue(structureObjectConfig.ObjectType.ObjectTypeID, out icon))
          break;
        int width1 = icon.Size.Width;
        Size size1 = icon.Size;
        int height1 = size1.Height;
        if (width1 != height1)
        {
          RowData rowData = e.RowData;
          size1 = icon.Size;
          int width2 = size1.Width;
          rowData.IconSize = width2;
        }
        e.RowData.Icon = icon;
        break;
      case ObjectOrderConfig objectOrderConfig:
        if (!this._objectOrdersIcons.TryGetValue(objectOrderConfig.ObjectType.ObjectTypeID, out icon))
          break;
        int width3 = icon.Size.Width;
        Size size2 = icon.Size;
        int height2 = size2.Height;
        if (width3 != height2)
        {
          RowData rowData = e.RowData;
          size2 = icon.Size;
          int width4 = size2.Width;
          rowData.IconSize = width4;
        }
        e.RowData.Icon = icon;
        break;
    }
  }

  private void vtvObjectsOrders_GetCellData(object sender, GetCellDataEventArgs e)
  {
    switch (e.Row.Item)
    {
      case TPStructureObjectsConfigs _:
        e.CellData.Value = (object) string.Empty;
        break;
      case TPStructureObjectConfig structureObjectConfig:
        e.CellData.Value = (object) structureObjectConfig.ObjectType.ObjectTypeName;
        break;
      case ObjectOrderConfig objectOrderConfig:
        e.CellData.Value = (object) objectOrderConfig.ObjectType.ObjectTypeName;
        break;
    }
  }

  private void vtvObjectsOrders_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateMenuCommands();
  }

  private void ctxmenuObjectsOrdersTree_Opening(object sender, CancelEventArgs e)
  {
    this.UpdateMenuCommands();
  }

  private void InitMenuCommands()
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this.ctxmenuObjectsOrdersTree.ImageList = service.ImageList;
    this.miMoveObjectType.DropDown.ImageList = service.ImageList;
    this.miAddObjectType.ImageIndex = service.ImageIndex("imgAdd");
    this.miRemoveObjectType.ImageIndex = service.ImageIndex("imgDelete");
    this.miMoveDownObjectType.ImageIndex = service.ImageIndex("imgMoveDown");
    this.miMoveUpObjectType.ImageIndex = service.ImageIndex("imgMoveUp");
    this.miMakeFirstObjectType.ImageIndex = service.ImageIndex("imgMoveFirst");
    this.miMakeLastObjectType.ImageIndex = service.ImageIndex("imgMoveLast");
  }

  private void UpdateMenuCommands()
  {
    this.miAddObjectType.Enabled = !this._settings.ReadOnly;
    this.miInsertObjectType.Enabled = !this._settings.ReadOnly && this.vtvObjectsOrders.SelectedItem is TPStructureObjectConfig;
    this.miRemoveObjectType.Enabled = !this._settings.ReadOnly && this.vtvObjectsOrders.SelectedItem != null;
    this.miResetToDefault.Enabled = !this._settings.ReadOnly;
    this.miMoveObjectType.Enabled = this.miMoveObjectType.Visible = this.vtvObjectsOrders.SelectedItem is ObjectOrderConfig;
    this.miMakeFirstObjectType.Enabled = this.CanMakeObjectOrderFirst();
    this.miMakeLastObjectType.Enabled = this.CanMakeObjectOrderLast();
    this.miMoveUpObjectType.Enabled = this.CanMoveObjectOrderUp();
    this.miMoveDownObjectType.Enabled = this.CanMoveObjectOrderDown();
  }

  private bool CanMakeObjectOrderFirst()
  {
    return !this._settings.ReadOnly && this.vtvObjectsOrders.SelectedRow?.Item is ObjectOrderConfig && this.vtvObjectsOrders.SelectedRow.ChildIndex > 0;
  }

  private bool CanMakeObjectOrderLast()
  {
    return !this._settings.ReadOnly && this.vtvObjectsOrders.SelectedRow?.Item is ObjectOrderConfig && this.vtvObjectsOrders.SelectedRow.ChildIndex < this.vtvObjectsOrders.SelectedRow.ParentRow.NumChildren - 1;
  }

  private bool CanMoveObjectOrderUp() => this.CanMakeObjectOrderFirst();

  private bool CanMoveObjectOrderDown() => this.CanMakeObjectOrderLast();

  private bool ObjTypeForObjectConfigFilter(int typeId)
  {
    return this._configEditableCache[typeId] == null && TechCardConsts.Utils.IsTechcardObjectType((object) typeId);
  }

  private bool ObjTypeForObjectOrderConfigFilter(int typeId)
  {
    TPStructureObjectConfig structureObjectConfig = this.vtvObjectsOrders.SelectedRow?.Item is ObjectOrderConfig ? this.vtvObjectsOrders.SelectedRow.ParentRow?.Item as TPStructureObjectConfig : this.vtvObjectsOrders.SelectedRow.Item as TPStructureObjectConfig;
    if (structureObjectConfig == null)
      return false;
    bool flag1 = structureObjectConfig?.ChildsOrdersConfigs[typeId, TechCardConsts.RelTypes.TechRelationID] == null && structureObjectConfig.ObjectType.ObjectTypeID != typeId;
    if (!flag1)
      return flag1;
    bool flag2 = MetaDataHelper.HasApplicability(structureObjectConfig.ObjectType.ObjectTypeID, typeId, TechCardConsts.RelTypes.TechRelationID);
    if (flag2)
      return flag2;
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(typeId);
    if (objectTypeChildrenId.Count > 0)
    {
      foreach (int childObjTypeID in objectTypeChildrenId)
      {
        flag2 = MetaDataHelper.HasApplicability(structureObjectConfig.ObjectType.ObjectTypeID, childObjTypeID, TechCardConsts.RelTypes.TechRelationID);
        if (flag2)
          break;
      }
    }
    else
      flag2 = TechCardConsts.Utils.IsTechcardObjectType((object) typeId) && MetaDataHelper.IsObjectTypeChildOf(structureObjectConfig.ObjectType.ObjectTypeID, typeId);
    return flag2;
  }

  private void miAddObjectType_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly)
      return;
    bool flag = false;
    if (this.vtvObjectsOrders.SelectedItem == null || this.vtvObjectsOrders.SelectedItem is TPStructureObjectConfig)
    {
      IMSObjectType selectedObjectType;
      if (!ObjectTypeSelector.Select(new Func<int, bool>(this.ObjTypeForObjectConfigFilter), out selectedObjectType))
        return;
      if (this._configEditableCache[selectedObjectType.ObjectTypeID] == null)
      {
        TPStructureObjectConfig structureObjectConfig = this._configEditableCache.Add(selectedObjectType.ObjectTypeID);
        if (structureObjectConfig != null)
        {
          this._objectsConfigsTreeCache.Add(structureObjectConfig.ObjectType.ObjectTypeName, structureObjectConfig);
          this.vtvObjectsOrders.RootRow.UpdateChildren(true, false);
          this.vtvObjectsOrders.SelectedRow = this.vtvObjectsOrders.RootRow.ChildRow((object) structureObjectConfig);
          flag = true;
        }
      }
    }
    else if (this.vtvObjectsOrders.SelectedItem is ObjectOrderConfig)
    {
      IMSObjectType selectedObjectType;
      if (!ObjectTypeSelector.Select(new Func<int, bool>(this.ObjTypeForObjectOrderConfigFilter), out selectedObjectType))
        return;
      ObjectOrderConfig objectOrderConfig = this.AddObjectOrderConfig(this.vtvObjectsOrders.SelectedRow.ParentRow.Item as TPStructureObjectConfig, selectedObjectType);
      if (objectOrderConfig != null)
      {
        this.vtvObjectsOrders.SelectedRow.ParentRow.UpdateChildren(true, false);
        this.vtvObjectsOrders.SelectedRow = this.vtvObjectsOrders.SelectedRow.ParentRow.ChildRow((object) objectOrderConfig);
        flag = true;
      }
    }
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  [CanBeNull]
  private ObjectOrderConfig AddObjectOrderConfig(
    [NotNull] TPStructureObjectConfig ownerObjectConfig,
    [NotNull] IMSObjectType objType)
  {
    if (ownerObjectConfig.ChildsOrdersConfigs[objType.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID] != null)
      return (ObjectOrderConfig) null;
    ObjectOrderConfig objectOrderConfig = ownerObjectConfig.ChildsOrdersConfigs.Configs.LastOrDefault<ObjectOrderConfig>();
    int? nullable = objectOrderConfig != null ? new int?(objectOrderConfig.Order + 100) : new int?();
    return ownerObjectConfig.ChildsOrdersConfigs.Add(objType.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID, nullable ?? 0);
  }

  private void miInsertObjectType_Click(object sender, EventArgs e)
  {
    IMSObjectType selectedObjectType;
    if (this._settings.ReadOnly || !(this.vtvObjectsOrders.SelectedItem is TPStructureObjectConfig selectedItem) || !ObjectTypeSelector.Select(new Func<int, bool>(this.ObjTypeForObjectOrderConfigFilter), out selectedObjectType))
      return;
    bool flag = false;
    if (this.AddObjectOrderConfig(selectedItem, selectedObjectType) != null)
    {
      this.vtvObjectsOrders.SelectedRow.UpdateChildren(true, false);
      flag = true;
    }
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  private void miRemoveObjectType_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly)
      return;
    bool flag = false;
    if (this.vtvObjectsOrders.SelectedItem is TPStructureObjectConfig selectedItem2)
    {
      if (this._configEditableCache.Remove(selectedItem2.ObjectType.ObjectTypeID))
        this._objectsConfigsTreeCache.Remove(selectedItem2.ObjectType.ObjectTypeName);
      this.vtvObjectsOrders.RootRow.UpdateChildren(true, false);
      flag = true;
    }
    else if (this.vtvObjectsOrders.SelectedItem is ObjectOrderConfig selectedItem1)
    {
      (this.vtvObjectsOrders.SelectedRow.ParentRow.Item as TPStructureObjectConfig).ChildsOrdersConfigs.Remove(selectedItem1.ObjectType.ObjectTypeID, selectedItem1.RelationType.RelationTypeID);
      this.vtvObjectsOrders.SelectedRow.ParentRow.UpdateChildren(true, false);
      flag = true;
    }
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag);
  }

  private void miResetToDefault_Click(object sender, EventArgs e)
  {
    if (this._settings.ReadOnly || this._configEditableCache.Count != 0 && MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Document_182"), LocalizationHolder.rm.GetString("TechCard.Document_181"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this._configEditableCache.InitDefault();
    this.BuildObjectsOrdersTree();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miMakeFirstObjectType_Click(object sender, EventArgs e)
  {
    if (!this.CanMakeObjectOrderFirst())
      return;
    ObjectOrderConfig selectedItem = this.vtvObjectsOrders.SelectedItem as ObjectOrderConfig;
    selectedItem.Order = 0;
    foreach (ObjectOrderConfig config in (this.vtvObjectsOrders.SelectedRow.ParentRow.Item as TPStructureObjectConfig).ChildsOrdersConfigs.Configs)
    {
      if (config != selectedItem)
        config.Order += 100;
    }
    this.vtvObjectsOrders.SelectedRow.ParentRow.UpdateChildren(true, false);
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miMoveUpObjectType_Click(object sender, EventArgs e)
  {
    if (!this.CanMoveObjectOrderUp())
      return;
    ObjectOrderConfig selectedItem = this.vtvObjectsOrders.SelectedItem as ObjectOrderConfig;
    ObjectOrderConfig objectOrderConfig = this.vtvObjectsOrders.SelectedRow.ParentRow.ChildRowByIndex(this.vtvObjectsOrders.SelectedRow.ChildIndex - 1).Item as ObjectOrderConfig;
    int order = selectedItem.Order;
    selectedItem.Order = objectOrderConfig.Order;
    objectOrderConfig.Order = order;
    this.vtvObjectsOrders.SelectedRow.ParentRow.UpdateChildren(true, false);
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miMoveDownObjectType_Click(object sender, EventArgs e)
  {
    if (!this.CanMoveObjectOrderDown())
      return;
    ObjectOrderConfig selectedItem = this.vtvObjectsOrders.SelectedItem as ObjectOrderConfig;
    ObjectOrderConfig objectOrderConfig = this.vtvObjectsOrders.SelectedRow.ParentRow.ChildRowByIndex(this.vtvObjectsOrders.SelectedRow.ChildIndex + 1).Item as ObjectOrderConfig;
    int order = selectedItem.Order;
    selectedItem.Order = objectOrderConfig.Order;
    objectOrderConfig.Order = order;
    this.vtvObjectsOrders.SelectedRow.ParentRow.UpdateChildren(true, false);
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  private void miMakeLastObjectType_Click(object sender, EventArgs e)
  {
    if (!this.CanMakeObjectOrderLast())
      return;
    Row parentRow = this.vtvObjectsOrders.SelectedRow.ParentRow;
    ObjectOrderConfig selectedItem = this.vtvObjectsOrders.SelectedItem as ObjectOrderConfig;
    selectedItem.Order = (parentRow.ChildRowByIndex(parentRow.NumChildren - 1).Item as ObjectOrderConfig).Order;
    foreach (ObjectOrderConfig config in (parentRow.Item as TPStructureObjectConfig).ChildsOrdersConfigs.Configs)
    {
      if (config != selectedItem)
        config.Order -= 100;
    }
    parentRow.UpdateChildren(true, false);
    this.UpdateMenuCommands();
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, true);
  }

  public bool ApplyChanges(out IDocumentConfigElement config)
  {
    config = this._originConfig;
    if (this._settings.ReadOnly)
      return false;
    (this._originConfig as TPStructureObjectsConfigs).Assign((object) this._configEditableCache);
    return true;
  }

  public void CancelChanges()
  {
    if (this._settings.ReadOnly)
      return;
    this._configEditableCache = (this._originConfig as TPStructureObjectsConfigs).Clone() as TPStructureObjectsConfigs;
    this.BuildObjectsOrdersTree();
  }

  public void SetupView(IConfigViewSettings settings)
  {
    this._settings = settings;
    bool flag1 = false;
    bool flag2 = false;
    if (this._settings.ConfigElement != this._originConfig)
    {
      this._originConfig = this._settings.ConfigElement;
      this._configEditableCache = (this._originConfig as TPStructureObjectsConfigs).Clone() as TPStructureObjectsConfigs;
      flag1 = true;
    }
    if (this._configEditableCache.Count == 0 && MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Document_180"), LocalizationHolder.rm.GetString("TechCard.Document_181"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      this._configEditableCache.InitDefault();
      flag1 = true;
      flag2 = true;
    }
    if (flag1)
      this.BuildObjectsOrdersTree();
    if (!flag2)
      return;
    Action<IConfigViewController, bool> onDataChanged = this._settings.OnDataChanged;
    if (onDataChanged == null)
      return;
    onDataChanged(this._controller, flag2);
  }

  public ObjectsOrdersConfigsView([NotNull] IConfigViewController controller, System.IServiceProvider services)
  {
    this.InitializeComponent();
    this.InitMenuCommands();
    this._controller = controller;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.ctxmenuObjectsOrdersTree = new ContextMenuStrip(this.components);
    this.miAddObjectType = new ToolStripMenuItem();
    this.miInsertObjectType = new ToolStripMenuItem();
    this.miRemoveObjectType = new ToolStripMenuItem();
    this.miMoveObjectType = new ToolStripMenuItem();
    this.miMakeFirstObjectType = new ToolStripMenuItem();
    this.miMoveUpObjectType = new ToolStripMenuItem();
    this.miMoveDownObjectType = new ToolStripMenuItem();
    this.miMakeLastObjectType = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.miResetToDefault = new ToolStripMenuItem();
    this.vtvObjectsOrders = new Intermech.VirtualTreeView.VirtualTreeView();
    this.colObjectName = new Column();
    this.ctxmenuObjectsOrdersTree.SuspendLayout();
    this.vtvObjectsOrders.BeginInit();
    this.SuspendLayout();
    this.ctxmenuObjectsOrdersTree.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.miAddObjectType,
      (ToolStripItem) this.miInsertObjectType,
      (ToolStripItem) this.miRemoveObjectType,
      (ToolStripItem) this.miMoveObjectType,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.miResetToDefault
    });
    this.ctxmenuObjectsOrdersTree.Name = "ctxmenuObjectsOrdersTree";
    this.ctxmenuObjectsOrdersTree.Size = new Size(332, 120);
    this.ctxmenuObjectsOrdersTree.Opening += new CancelEventHandler(this.ctxmenuObjectsOrdersTree_Opening);
    this.miAddObjectType.Name = "miAddObjectType";
    this.miAddObjectType.ShortcutKeys = Keys.Insert | Keys.Control;
    this.miAddObjectType.Size = new Size(331, 22);
    this.miAddObjectType.Text = "Добавить";
    this.miAddObjectType.Click += new EventHandler(this.miAddObjectType_Click);
    this.miInsertObjectType.Name = "miInsertObjectType";
    this.miInsertObjectType.ShortcutKeys = Keys.Insert | Keys.Shift | Keys.Control;
    this.miInsertObjectType.Size = new Size(331, 22);
    this.miInsertObjectType.Text = "Вставить настройку сортировки";
    this.miInsertObjectType.Click += new EventHandler(this.miInsertObjectType_Click);
    this.miRemoveObjectType.Name = "miRemoveObjectType";
    this.miRemoveObjectType.ShortcutKeys = Keys.Delete | Keys.Control;
    this.miRemoveObjectType.Size = new Size(331, 22);
    this.miRemoveObjectType.Text = "Удалить";
    this.miRemoveObjectType.Click += new EventHandler(this.miRemoveObjectType_Click);
    this.miMoveObjectType.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miMakeFirstObjectType,
      (ToolStripItem) this.miMoveUpObjectType,
      (ToolStripItem) this.miMoveDownObjectType,
      (ToolStripItem) this.miMakeLastObjectType
    });
    this.miMoveObjectType.Name = "miMoveObjectType";
    this.miMoveObjectType.Size = new Size(331, 22);
    this.miMoveObjectType.Text = "Переместить";
    this.miMakeFirstObjectType.Name = "miMakeFirstObjectType";
    this.miMakeFirstObjectType.ShortcutKeys = Keys.H | Keys.Control;
    this.miMakeFirstObjectType.Size = new Size(240 /*0xF0*/, 22);
    this.miMakeFirstObjectType.Text = "в начало";
    this.miMakeFirstObjectType.Click += new EventHandler(this.miMakeFirstObjectType_Click);
    this.miMoveUpObjectType.Name = "miMoveUpObjectType";
    this.miMoveUpObjectType.ShortcutKeys = Keys.U | Keys.Control;
    this.miMoveUpObjectType.Size = new Size(240 /*0xF0*/, 22);
    this.miMoveUpObjectType.Text = "на один уровень вверх";
    this.miMoveUpObjectType.Click += new EventHandler(this.miMoveUpObjectType_Click);
    this.miMoveDownObjectType.Name = "miMoveDownObjectType";
    this.miMoveDownObjectType.ShortcutKeys = Keys.D | Keys.Control;
    this.miMoveDownObjectType.Size = new Size(240 /*0xF0*/, 22);
    this.miMoveDownObjectType.Text = "на один уровень вниз";
    this.miMoveDownObjectType.Click += new EventHandler(this.miMoveDownObjectType_Click);
    this.miMakeLastObjectType.Name = "miMakeLastObjectType";
    this.miMakeLastObjectType.ShortcutKeys = Keys.L | Keys.Control;
    this.miMakeLastObjectType.Size = new Size(240 /*0xF0*/, 22);
    this.miMakeLastObjectType.Text = "в конец";
    this.miMakeLastObjectType.Click += new EventHandler(this.miMakeLastObjectType_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(328, 6);
    this.miResetToDefault.Name = "miResetToDefault";
    this.miResetToDefault.Size = new Size(331, 22);
    this.miResetToDefault.Text = "Сбросить настройки вывода по умолчанию";
    this.miResetToDefault.ToolTipText = "Сбросить настройки вывода объектов по умолчанию";
    this.miResetToDefault.Click += new EventHandler(this.miResetToDefault_Click);
    this.vtvObjectsOrders.AllowDrop = true;
    this.vtvObjectsOrders.AllowIndividualRowResize = false;
    this.vtvObjectsOrders.AllowMultiSelect = false;
    this.vtvObjectsOrders.AllowRowResize = false;
    this.vtvObjectsOrders.AllowUserPinnedColumns = false;
    this.vtvObjectsOrders.Columns.Add(this.colObjectName);
    this.vtvObjectsOrders.ContextMenuStrip = this.ctxmenuObjectsOrdersTree;
    this.vtvObjectsOrders.DisableHeaderContextMenu = false;
    this.vtvObjectsOrders.Dock = DockStyle.Fill;
    this.vtvObjectsOrders.ImageList = (ImageList) null;
    this.vtvObjectsOrders.Location = new Point(0, 0);
    this.vtvObjectsOrders.MainColumn = this.colObjectName;
    this.vtvObjectsOrders.Name = "vtvObjectsOrders";
    this.vtvObjectsOrders.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.MainCellText;
    this.vtvObjectsOrders.ShowColumnHeaders = false;
    this.vtvObjectsOrders.ShowRootRow = false;
    this.vtvObjectsOrders.Size = new Size(811, 620);
    this.vtvObjectsOrders.TabIndex = 0;
    this.vtvObjectsOrders.UseThemedHeaders = false;
    this.vtvObjectsOrders.GetCellData += new GetCellDataHandler(this.vtvObjectsOrders_GetCellData);
    this.vtvObjectsOrders.GetChildren += new GetChildrenHandler(this.vtvObjectsOrders_GetChildren);
    this.vtvObjectsOrders.GetRowData += new GetRowDataHandler(this.vtvObjectsOrders_GetRowData);
    this.colObjectName.AutoSizePolicy = ColumnAutoSizePolicy.AutoIncrease;
    this.colObjectName.Caption = (string) null;
    this.colObjectName.Movable = false;
    this.colObjectName.Name = "colObjectName";
    this.colObjectName.Resizable = false;
    this.colObjectName.Sortable = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.vtvObjectsOrders);
    this.Name = nameof (ObjectsOrdersConfigsView);
    this.Size = new Size(811, 620);
    this.ctxmenuObjectsOrdersTree.ResumeLayout(false);
    this.vtvObjectsOrders.EndInit();
    this.ResumeLayout(false);
  }
}
