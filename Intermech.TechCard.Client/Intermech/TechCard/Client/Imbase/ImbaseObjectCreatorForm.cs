// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.ImbaseObjectCreatorForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Extensions;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Selection;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
public class ImbaseObjectCreatorForm : ImbaseFilterSelectionBaseWindow
{
  /// <summary>Элементы меню, требующие отключения</summary>
  private static readonly List<string> ImMenu2Suppress = new List<string>()
  {
    "mnCreateObject",
    "toolStripMenuItem2"
  };
  /// <summary>Перечень закладок, требующих отключения</summary>
  private static readonly string[] View2Suppress = new string[3]
  {
    "ImbaseIndexesView",
    "ObjectFiles",
    "ObjectSecurity"
  };
  /// <summary>
  /// Идентификаторы атрибутов - описателей для типа объекта
  /// </summary>
  private readonly IList<int> _objTypeCaptionAttrIds = (IList<int>) new List<int>();
  /// <summary>Вьюшка IMBASE</summary>
  private TableView _imbaseTableView;
  /// <summary>
  /// Таймер на обновление дерева Imbase (для обеспечения нормальной работы события DblClick на дереве)
  /// </summary>
  private System.Timers.Timer _timer;
  /// <summary>
  /// 
  /// </summary>
  private readonly ImageList _imageList = new ImageList();
  /// <summary>Флаг режима множественного выбора</summary>
  private bool _multiSelect = true;
  /// <summary>
  /// 
  /// </summary>
  private bool _createTreeMode;
  /// <summary>
  /// Идентификатор версии объекта, выделенного в дереве Imbase
  /// </summary>
  private long _focusedObjectId;
  /// <summary>
  /// Список выбранных, элементов (код папки дерева, код записи таблицы)
  /// </summary>
  private readonly List<ImbaseObjectCaptionItem> _selectedItems = new List<ImbaseObjectCaptionItem>();
  /// <summary>
  /// Список для хранения узлов, который присвоили StateIndex (чтобы не слетал StateIndex у отмеченных)
  /// </summary>
  private readonly Dictionary<TreeNode, int> _node2StateCache = new Dictionary<TreeNode, int>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip _cmsSelectedItems;
  private ToolStripSeparator toolStripSeparator1;
  private Panel pnlSelected;
  private ListView lvSelectedItems;
  private Panel pnlSelectedInfo;
  private Label lblSelectedInfo;
  private ColumnHeader columnHeader1;
  private ToolStripMenuItem tsmiSelectedDuplicate;
  private ToolStripMenuItem tsmiSeletedMove;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem tsmiSelectedDelete;
  private ToolStripMenuItem tsmiSelectedMoveTop;
  private ToolStripMenuItem tsmiSelectedMoveUp;
  private ToolStripMenuItem tsmiSelectedMoveDown;
  private ToolStripMenuItem tsmiSelectedMoveLast;
  private ToolStripMenuItem tsmiSelectedClear;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomComponents()
  {
    this.lvSelectedItems.SmallImageList = this._trv.ImageList;
    if (!this._splitContainerLeft.Panel2.Controls.Contains((Control) this.pnlSelected))
      this._splitContainerLeft.Panel2.Controls.Add((Control) this.pnlSelected);
    if (this.DesignMode)
      return;
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this._trv.StateImageList = this._imageList;
      this._imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgUnchecked")]);
      this._imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgChecked")]);
    }
    this._timer = new System.Timers.Timer()
    {
      Interval = 100.0,
      SynchronizingObject = (ISynchronizeInvoke) this._trv,
      AutoReset = false,
      Enabled = false
    };
    this._timer.Elapsed += new ElapsedEventHandler(this.OnTimerElapsed);
    this._treeBuilder.Selected += new SelectEventHandler(this.OnTreeBuilderSelected);
    this.FolderFilterItemChecked = this._tsBtnFolderFilter.DropDownItems[(int) TechCardParamsHelper.TechParams.Common.DefImbaseFilter] as ToolStripMenuItem;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private string GetConfigurationName()
  {
    string str = this.GetType().ToString();
    IEnumerable<long> imbaseCatalogIds = this.SelectionParams.ImbaseCatalogIds;
    if ((imbaseCatalogIds != null ? (imbaseCatalogIds.Any<long>() ? 1 : 0) : 0) != 0)
      return $"{str}_Catalogs_{string.Join<long>(":", (IEnumerable<long>) this.SelectionParams.ImbaseCatalogIds.ToHashSet<long>())}";
    IEnumerable<int> objectTypeIds = this.SelectionParams.ObjectTypeIds;
    return (objectTypeIds != null ? (objectTypeIds.Any<int>() ? 1 : 0) : 0) != 0 ? $"{str}_{string.Join<int>(":", (IEnumerable<int>) this.SelectionParams.ObjectTypeIds.ToHashSet<int>())}" : str;
  }

  /// <summary>Восстановим отмеченные ранее строки.</summary>
  private void Grid_RestoreSelection()
  {
    try
    {
      CheckedRecords.Clear();
      if (this._selectedItems == null)
        return;
      foreach (IGrouping<long, long> source in (IEnumerable<IGrouping<long, long>>) this._selectedItems.Where<ImbaseObjectCaptionItem>((Func<ImbaseObjectCaptionItem, bool>) (item => item.RecordId != -1L)).ToLookup<ImbaseObjectCaptionItem, long, long>((Func<ImbaseObjectCaptionItem, long>) (item => item.ObjectInfo.ItemID), (Func<ImbaseObjectCaptionItem, long>) (item => item.RecordId)))
        CheckedRecords.Add(source.Key, source.ToArray<long>());
    }
    finally
    {
      this.UpdateButtons();
    }
  }

  /// <summary>Обновление контролов формы.</summary>
  private void UpdateControls()
  {
    this._splitContainerLeft.Panel2Collapsed = !this.MultiSelect;
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateSelectedStateControls(bool forceMode = false)
  {
    if (!this.MultiSelect | forceMode)
    {
      this._node2StateCache.Clear();
      this.UpdateTvStateIndex(true);
      this.Grid_RestoreSelection();
    }
    this.UpdateSelectedItems();
    this.UpdateButtons();
  }

  /// <summary>Обновление StateIndex у узлов дерева.</summary>
  private void UpdateTvStateIndex(bool recursiveMode)
  {
    this._trv.BeginUpdate();
    try
    {
      foreach (TreeNode node in this._trv.Nodes)
        this.UpdateNodeStateIndex(node, recursiveMode);
    }
    finally
    {
      this._trv.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateSelectedItems()
  {
    if (!this.MultiSelect)
      return;
    this.lvSelectedItems.BeginUpdate();
    try
    {
      this.lvSelectedItems.Items.Clear();
      this._selectedItems.ForEach((Action<ImbaseObjectCaptionItem>) (item => this.lvSelectedItems.Items.Add(new ListViewItem(Convert.ToString(item.ObjectInfo.Caption), TreeBuilder.GetIconIndex(item.ObjectInfo.ItemTypeID))
      {
        Tag = (object) item
      })));
    }
    finally
    {
      this.lvSelectedItems.EndUpdate();
      int count = this.lvSelectedItems.Items.Count;
      if (count > 0)
        this.lvSelectedItems.Items[count - 1].Selected = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private ImbaseObjectCaptionItem GetSelectedItem()
  {
    return this.lvSelectedItems.SelectedItems.Count == 0 ? (ImbaseObjectCaptionItem) null : this.lvSelectedItems.SelectedItems[0].Tag as ImbaseObjectCaptionItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private int GetSelectedItemIndex()
  {
    ImbaseObjectCaptionItem selectedItem = this.GetSelectedItem();
    return selectedItem == null ? -1 : this._selectedItems.FindIndex((Predicate<ImbaseObjectCaptionItem>) (item => item == selectedItem));
  }

  /// <summary>Move route element</summary>
  /// <param name="sourceIdx"></param>
  /// <param name="destinationIdx"></param>
  private void SelectedItemMove(int sourceIdx, int destinationIdx)
  {
    if (sourceIdx < 0 || sourceIdx == destinationIdx || destinationIdx >= this._selectedItems.Count)
      return;
    this.lvSelectedItems.BeginUpdate();
    try
    {
      int num = sourceIdx < destinationIdx ? 1 : -1;
      for (int index = sourceIdx; index != destinationIdx; index += num)
      {
        ImbaseObjectCaptionItem selectedItem = this._selectedItems[index];
        this._selectedItems[index] = this._selectedItems[index + num];
        this._selectedItems[index + num] = selectedItem;
      }
    }
    finally
    {
      this.lvSelectedItems.EndUpdate();
      this.UpdateSelectedStateControls();
      this.lvSelectedItems.Items[destinationIdx].Selected = true;
    }
  }

  /// <summary>Обновление StateIndex у узла.</summary>
  /// <param name="node"></param>
  /// <param name="recursiveMode"></param>
  private void UpdateNodeStateIndex(TreeNode node, bool recursiveMode)
  {
    if (this._createTreeMode || this._node2StateCache.ContainsKey(node))
      return;
    this._node2StateCache.Add(node, 0);
    if (node.Nodes.Count > 0)
    {
      node.StateImageIndex = -1;
    }
    else
    {
      NodeInfo nodeInfo = node.Tag as NodeInfo;
      if (nodeInfo == null)
      {
        node.StateImageIndex = -1;
        return;
      }
      node.StateImageIndex = nodeInfo.IsCatalog || nodeInfo.IsTableReference || nodeInfo.IsFavoritesFolder || nodeInfo.IsTableMix ? -1 : (this._selectedItems.Find((Predicate<ImbaseObjectCaptionItem>) (item => item.ObjectInfo.ItemID == nodeInfo.ObjectId)) != null ? 1 : 0);
    }
    if (!recursiveMode)
      return;
    foreach (TreeNode node1 in node.Nodes)
      this.UpdateNodeStateIndex(node1, true);
  }

  /// <summary>Регистрация Table View.</summary>
  /// <param name="imTableView"></param>
  private void RegisterTableView(TableView imTableView)
  {
    if (this._imbaseTableView != null)
      this.UnregisterTableView();
    if (imTableView == null)
      return;
    this._imbaseTableView = imTableView;
    this._imbaseTableView.FollowSelectMode = ImFollowSelectMode.imfsmAllRows;
    this._imbaseTableView.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    DataGridViewRow[] array = (DataGridViewRow[]) null;
    int count = this._imbaseTableView.Grid.SelectedRows.Count;
    if (count > 0)
    {
      array = new DataGridViewRow[count];
      this._imbaseTableView.Grid.SelectedRows.CopyTo(array, 0);
    }
    CheckedRecords.Active = true;
    this._imbaseTableView.ItemChecked += new CheckEventHandler(this.On_tableView_ItemChecked);
    if (count > 0 && array != null)
    {
      foreach (DataGridViewBand dataGridViewBand in array)
        dataGridViewBand.Selected = true;
    }
    this._imbaseTableView.ItemDoubleClick += new EventHandler(this.On_tableView_ItemDoubleClick);
    foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this._imbaseTableView.ImContextMenu.Items)
    {
      if (ImbaseObjectCreatorForm.ImMenu2Suppress.Contains(toolStripItem.Name))
        toolStripItem.Enabled = toolStripItem.Visible = false;
    }
  }

  /// <summary>Раз-регистрация.</summary>
  private void UnregisterTableView()
  {
    if (this._imbaseTableView == null)
      return;
    this._imbaseTableView.ItemDoubleClick -= new EventHandler(this.On_tableView_ItemDoubleClick);
    this._imbaseTableView.ItemChecked -= new CheckEventHandler(this.On_tableView_ItemChecked);
    this._imbaseTableView = (TableView) null;
  }

  /// <summary>Конструктор.</summary>
  public ImbaseObjectCreatorForm()
  {
    this.InitializeComponent();
    this.InitializeCustomComponents();
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objTypeId">Тип создаваемого объекта</param>
  [Obsolete("Use constructor with ImbaseSelectionParam instead", true)]
  public ImbaseObjectCreatorForm(int objTypeId)
    : this(objTypeId, 0L)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objectTypeId">Тип создаваемого объекта</param>
  /// <param name="ownerObjectId">Owner object id (Can be as created by Imbase as not)</param>
  [Obsolete("Use constructor with ImbaseSelectionParam instead", true)]
  public ImbaseObjectCreatorForm(int objectTypeId, long ownerObjectId)
  {
    long ownerObjectId1 = ownerObjectId;
    List<int> objectTypeIds;
    if (objectTypeId == -1)
    {
      objectTypeIds = (List<int>) null;
    }
    else
    {
      objectTypeIds = new List<int>();
      objectTypeIds.Add(objectTypeId);
    }
    // ISSUE: explicit constructor call
    this.\u002Ector(new ImbaseSelectionParam(ownerObjectId1, (IEnumerable<int>) objectTypeIds));
  }

  /// <summary>
  /// Конструктор окна для выбора из конкретного каталога и разными типами создаваемых объектов
  /// </summary>
  /// <param name="objTypeIds">Типы создаваемых объектов по каталогу</param>
  /// <param name="catalogId">Идентификатор каталога Imbase</param>
  /// <param name="ownerObjId">Owner object id (Can be as created by Imbase as not)</param>
  public ImbaseObjectCreatorForm(ImbaseSelectionParam objectSelectionParam)
    : base(objectSelectionParam)
  {
    this.InitializeComponent();
    this.InitializeCustomComponents();
    this.InitializeData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.UpdateSelectedStateControls();
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void LoadSettings()
  {
    TechCardFormUtils.LoadSettings((Control) this);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration configuration1 = service.Open(name);
    if (configuration1 != null)
    {
      int result;
      if (int.TryParse(configuration1.GetProperty("mainSplitterDistance"), out result) && result > 0)
        this._spltContainer.SplitterDistance = result;
      if (int.TryParse(configuration1.GetProperty("selectedSplitterDistance"), out result) && result > 0)
        this._splitContainerLeft.SplitterDistance = result;
    }
    IConfiguration configuration2 = service.Open(this.GetConfigurationName());
    if (configuration2 == null)
      return;
    ImbaseFilterSelectionBaseWindow.ImFilterMode imFilterMode = ImbaseFilterSelectionBaseWindow.ImFilterMode.None;
    try
    {
      imFilterMode = (ImbaseFilterSelectionBaseWindow.ImFilterMode) Convert.ToInt32(configuration2.GetProperty("FilterMode"));
    }
    catch (Exception ex)
    {
    }
    switch (imFilterMode)
    {
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder:
        string property = configuration2.GetProperty("FolderFilterOwnerGuid");
        this.FolderFilterItemChecked = property == this._userFilterGuid ? this._tsmiFolderFilterUser : (property == this._roleFilterGuid ? this._tsmiFolderFilterRole : (property == this._areaFilterGuid ? this._tsmiFolderFilterArea : this._tsmiFolderFilterCommon));
        break;
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Object:
        long objFilterId;
        if (!long.TryParse(configuration2.GetProperty("ObjectFilterID"), out objFilterId))
          objFilterId = 0L;
        List<ImbaseObjFilterInfo> objFilterList = this._objFilterList;
        if ((objFilterList != null ? objFilterList.FirstOrDefault<ImbaseObjFilterInfo>((Func<ImbaseObjFilterInfo, bool>) (x => x.ObjectID == objFilterId)) : (ImbaseObjFilterInfo) null) != null)
        {
          bool flag = true;
          for (int index = 2; index < 6 & flag; ++index)
          {
            if (this._tsBtnObjFilter.DropDownItems[index] is ToolStripMenuItem dropDownItem1 && dropDownItem1.DropDownItems.Count != 0)
            {
              foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) dropDownItem1.DropDownItems)
              {
                if ((dropDownItem is ToolStripMenuItem toolStripMenuItem ? toolStripMenuItem.Tag : (object) null) is ImbaseObjFilterInfo tag && tag.ObjectID == objFilterId)
                {
                  this._objFilterID = objFilterId;
                  this.ObjectFilterItemChecked = toolStripMenuItem;
                  flag = false;
                  break;
                }
              }
            }
          }
          break;
        }
        break;
    }
    long result1;
    if (!long.TryParse(configuration2.GetProperty("selectedObjectID"), out result1))
      return;
    this._prevSelectedObjID = result1;
  }

  /// <summary>Закрытие формы.</summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    CheckedRecords.Active = false;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void SaveSettings()
  {
    base.SaveSettings();
    TechCardFormUtils.SaveSettings((Control) this);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration configuration1 = service.Open(name) ?? service.Create(name);
    if (configuration1 != null)
    {
      configuration1.SetProperty("mainSplitterDistance", Convert.ToString(this.SplitterDistance));
      configuration1.SetProperty("selectedSplitterDistance", Convert.ToString(this._splitContainerLeft.SplitterDistance));
    }
    IConfiguration configuration2 = service.Create(this.GetConfigurationName());
    if (configuration2 == null)
      return;
    ImbaseFilterSelectionBaseWindow.ImFilterMode filterMode = this.FilterMode;
    switch (filterMode)
    {
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder:
        configuration2.SetProperty("FolderFilterOwnerGuid", this.GetFilterOwnerGuid() ?? string.Empty);
        break;
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Object:
        configuration2.SetProperty("ObjectFilterID", Convert.ToString(this._objFilterID));
        break;
    }
    configuration2.SetProperty("FilterMode", Convert.ToString((int) filterMode));
    long num = this.BaseObjectId;
    if (this.MultiSelect)
    {
      ImbaseObjectCaptionItem objectCaptionItem = this._selectedItems.LastOrDefault<ImbaseObjectCaptionItem>();
      if (objectCaptionItem != null)
        num = objectCaptionItem.ObjectInfo.ItemID;
    }
    configuration2.SetProperty("selectedObjectID", Convert.ToString(num));
  }

  /// <summary>
  /// 
  /// </summary>
  private new void InitializeData()
  {
    if (this.DesignMode)
      return;
    this.InitializeServices();
    this._objTypeCaptionAttrIds.Clear();
    if (this.SelectionParams.ObjectTypeIds != null)
      this._objTypeCaptionAttrIds.AddRange<int>(this.SelectionParams.ObjectTypeIds.Select<int, IMSObjectType>(new Func<int, IMSObjectType>(MetaDataHelper.GetObjectType)).Where<IMSObjectType>((Func<IMSObjectType, bool>) (item => item != null)).Select<IMSObjectType, int>((Func<IMSObjectType, int>) (item => item.CaptionAttribute)));
    this._objTypeCaptionAttrIds.Add(MetaDataHelper.GetAttributeID((object) "cad0001f-306c-11d8-b4e9-00304f19f545"));
    this._objTypeCaptionAttrIds.Add(MetaDataHelper.GetAttributeID((object) "cad00020-306c-11d8-b4e9-00304f19f545"));
    IEnumerable<long> imbaseCatalogIds = this.SelectionParams.ImbaseCatalogIds;
    if ((imbaseCatalogIds != null ? (imbaseCatalogIds.Any<long>() ? 1 : 0) : 0) != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.Text = string.Format(this.Text, (object) sessionKeeper.Session.GetObjectInfo(this.SelectionParams.ImbaseCatalogIds.FirstOrDefault<long>()).Caption);
    }
    else
    {
      IEnumerable<int> objectTypeIds = this.SelectionParams.ObjectTypeIds;
      if ((objectTypeIds != null ? (objectTypeIds.Any<int>() ? 1 : 0) : 0) == 0)
        return;
      this.Text = string.Format(this.Text, (object) MetaDataHelper.GetObjectTypeName(this.SelectionParams.ObjectTypeIds.FirstOrDefault<int>()));
    }
  }

  /// <summary>Инициализация сервисов.</summary>
  private void InitializeServices()
  {
  }

  /// <summary>Де-инициализация сервисов.</summary>
  private void ServicesFinalization()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  protected override bool FilterUpdate()
  {
    this._createTreeMode = true;
    int num = base.FilterUpdate() ? 1 : 0;
    this._createTreeMode = false;
    if (num == 0)
      return num != 0;
    this.UpdateTvStateIndex(true);
    return num != 0;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void SelectPreviousItemInTree()
  {
    if (this._selectedItems.Count != 0)
    {
      ImbaseObjectInfoItem imbaseObjectInfoItem = this.SelectedObjItems.FirstOrDefault<ImbaseObjectInfoItem>();
      this._prevSelectedObjID = imbaseObjectInfoItem != null ? imbaseObjectInfoItem.ObjectInfo.ItemID : 0L;
    }
    if (this._prevSelectedObjID == 0L)
      return;
    this._treeBuilder.SetSelectedNode(this._prevSelectedObjID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void SearchByName(object sender, EventArgs e)
  {
    if (!this.MultiSelect)
    {
      ImbaseFindByNameControl.Show((object) this._trv.SelectedNode, true, this.MultiSelect, (LocateNodeEventHandler) null);
    }
    else
    {
      ImbaseFindByNameControl control = new ImbaseFindByNameControl();
      control.MultiSelect = this.MultiSelect;
      control.SetData((object) this._trv.SelectedNode, (LocateNodeEventHandler) null);
      control.ItemFill += new EventHandler(this.SearchItem_Fill);
      control.ItemStatusChange += new EventHandler(this.SearchItem_StatusChange);
      ImbaseFindByNameControl.Show(control, true);
      control.ItemFill -= new EventHandler(this.SearchItem_Fill);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SearchItem_Fill(object sender, EventArgs e)
  {
    if (!(e is ImbaseFindByNameControl.ItemEventArgs itemEventArgs) || itemEventArgs.Item == null || !(itemEventArgs.Item.Tag is TreeNode tag))
      return;
    if (tag.Nodes.Count > 0)
    {
      itemEventArgs.Item.StateImageIndex = 3;
    }
    else
    {
      NodeInfo nodeInfo = tag.Tag as NodeInfo;
      if (nodeInfo == null || nodeInfo.IsCatalog || nodeInfo.IsTableReference || nodeInfo.IsFavoritesFolder)
        itemEventArgs.Item.StateImageIndex = 3;
      else
        itemEventArgs.Item.StateImageIndex = this._selectedItems.Find((Predicate<ImbaseObjectCaptionItem>) (item => item.ObjectInfo.ItemID == nodeInfo.ObjectId)) != null ? 1 : 0;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SearchItem_StatusChange(object sender, EventArgs e)
  {
    if (!(e is ImbaseFindByNameControl.ItemEventArgs itemEventArgs) || itemEventArgs.Item == null)
      return;
    if (itemEventArgs.Item.StateImageIndex == 2 || itemEventArgs.Item.StateImageIndex == 3)
    {
      itemEventArgs.State = itemEventArgs.Item.StateImageIndex;
    }
    else
    {
      if (!(itemEventArgs.Item.Tag is TreeNode tag1) || !(tag1.Tag is NodeInfo tag2))
        return;
      ImbaseObjectCaptionItem objectCaptionItem = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(tag2.ObjectId, tag2.TypeId, tag1.Text), -1L);
      switch (itemEventArgs.State)
      {
        case 0:
          this._selectedItems.Remove(objectCaptionItem);
          break;
        case 1:
          if (!this._selectedItems.Contains(objectCaptionItem))
          {
            this._selectedItems.Add(objectCaptionItem);
            break;
          }
          break;
      }
      TreeNode treeNode;
      this._treeBuilder.NodeCache.TryGetValue(tag2.ObjectId, out treeNode);
      if (treeNode != null)
        treeNode.StateImageIndex = itemEventArgs.State;
      this.UpdateSelectedStateControls();
    }
  }

  /// <summary>Настройка фильтров Imbase по папкам каталогов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void OnFolderFilterSetup_Click(object sender, EventArgs e)
  {
    IEnumerable<long> imbaseCatalogIds = this.SelectionParams.ImbaseCatalogIds;
    if ((imbaseCatalogIds != null ? (imbaseCatalogIds.Any<long>() ? 1 : 0) : 0) != 0)
    {
      int num1 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this.SelectionParams.ImbaseCatalogIds.FirstOrDefault<long>());
    }
    else
    {
      IEnumerable<int> objectTypeIds = this.SelectionParams.ObjectTypeIds;
      if ((objectTypeIds != null ? (objectTypeIds.Any<int>() ? 1 : 0) : 0) != 0)
      {
        int num2 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this.SelectionParams.ObjectTypeIds.FirstOrDefault<int>());
      }
    }
    if (this.FilterMode != ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder)
      return;
    this.FilterUpdate();
  }

  /// <summary>Настройка фильтров Imbase для объектов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void OnObjFilterSetup_Click(object sender, EventArgs e)
  {
    IEnumerable<long> imbaseCatalogIds = this.SelectionParams.ImbaseCatalogIds;
    if ((imbaseCatalogIds != null ? (imbaseCatalogIds.Any<long>() ? 1 : 0) : 0) != 0)
    {
      int num1 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this.SelectionParams.ImbaseCatalogIds.FirstOrDefault<long>(), false);
    }
    else
    {
      IEnumerable<int> objectTypeIds = this.SelectionParams.ObjectTypeIds;
      if ((objectTypeIds != null ? (objectTypeIds.Any<int>() ? 1 : 0) : 0) != 0)
      {
        int num2 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this.SelectionParams.ObjectTypeIds.FirstOrDefault<int>(), false);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.LoadObjectFiltersInfo(sessionKeeper.Session);
      this.FilterUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void UpdateButtons() => this._btnApply.Enabled = this._selectedItems.Count > 0;

  /// <summary>
  /// 
  /// </summary>
  public IServiceContainer Services => this._services;

  /// <summary>
  /// Идентификатор базового объекта ( папка, запись каталога, таблица или ссылка на таблицу Imbase.
  /// </summary>
  private long BaseObjectId => this._focusedObjectId;

  /// <summary>Multi select mode.</summary>
  public bool MultiSelect
  {
    get => this._multiSelect;
    set
    {
      if (this._multiSelect == value)
        return;
      this._multiSelect = value;
      this.UpdateSelectedStateControls(true);
      this.UpdateControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public long OwnerObjectId
  {
    get => this._ownerObjectId;
    set
    {
      if (this._ownerObjectId == value)
        return;
      this._ownerObjectId = value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.LoadContextInfo(sessionKeeper.Session, this._ownerObjectId);
      if (this.FilterMode == ImbaseFilterSelectionBaseWindow.ImFilterMode.None)
        return;
      this.FilterUpdate();
    }
  }

  /// <summary>
  /// Идентификатор записи таблицы если базовый объект - таблица или ссылка на таблицу IMBASE.
  /// </summary>
  private long RecordId
  {
    get
    {
      TableView imbaseTableView = this._imbaseTableView;
      return imbaseTableView == null ? -1L : imbaseTableView.RecordId;
    }
  }

  /// <summary>Список выбранный элементов.</summary>
  public IEnumerable<ImbaseObjectInfoItem> SelectedObjItems
  {
    get => (IEnumerable<ImbaseObjectInfoItem>) this._selectedItems;
    set
    {
      this._selectedItems.Clear();
      if (value != null && value.Any<ImbaseObjectInfoItem>())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (ImbaseObjectInfoItem imbaseObjectInfoItem in value)
          {
            if (!(imbaseObjectInfoItem is ImbaseObjectCaptionItem objectCaptionItem1))
              objectCaptionItem1 = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(imbaseObjectInfoItem.ObjectInfo as TypedInfoItem), imbaseObjectInfoItem.RecordId);
            ImbaseObjectCaptionItem objectCaptionItem2 = objectCaptionItem1;
            if (imbaseObjectInfoItem.ObjectInfo.ItemID != 0L && imbaseObjectInfoItem.ObjectInfo.ItemID != -1L)
            {
              if (objectCaptionItem2.ObjectInfo.ItemTypeID == -1 || objectCaptionItem2.RecordId != -1L && string.IsNullOrEmpty(objectCaptionItem2.ObjectInfo.Caption))
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(imbaseObjectInfoItem.ObjectInfo.ItemID);
                objectCaptionItem2.ObjectInfo.ItemTypeID = objectInfo.ObjectTypeID;
                if (objectCaptionItem2.RecordId != -1L)
                  objectCaptionItem2.ObjectInfo.Caption = objectInfo.Caption;
              }
              this._selectedItems.Add(objectCaptionItem2);
            }
          }
        }
        this._treeBuilder.LoadFullTree(value.Select<ImbaseObjectInfoItem, long>((Func<ImbaseObjectInfoItem, long>) (item => item.ObjectInfo.ItemID)).ToList<long>());
      }
      this.UpdateSelectedStateControls(true);
    }
  }

  /// <summary>Событие выбора объекта из справочника.</summary>
  public event ImbaseObjectCreatorForm.ImbaseObjectHandler ImbaseObjSelected;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeBuilderSelected(object sender, TreeViewSelectEventArgs e)
  {
    if (e.NodeInfo == null)
      return;
    this._focusedObjectId = e.NodeInfo.ObjectId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
  {
    this._timer.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTimerElapsed(object sender, ElapsedEventArgs e)
  {
    this._timer.Enabled = false;
    if (this.InvokeRequired)
      this.Invoke((Delegate) new ElapsedEventHandler(this.OnTimerElapsed), sender, (object) e);
    else
      this.OnTreeViewAfterSelectDelayed(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeViewAfterSelectDelayed(object sender, ElapsedEventArgs e)
  {
    try
    {
      if (!(this._trv.SelectedNode?.Tag is NodeInfo tag))
        return;
      IDescriptor rootDescriptor;
      if (!this._descrs.TryGetValue(tag.ObjectId, out rootDescriptor))
      {
        rootDescriptor = (IDescriptor) new ImbaseFilterDescriptor(tag.ObjectId);
        this._descrs.Add(tag.ObjectId, rootDescriptor);
      }
      NodeIDPath handlerPath = new NodeIDPath(rootDescriptor);
      INode handler = (INode) new EtherealNode(handlerPath.RootDescriptor);
      INodeQuery query = handler.GetQuery(ContentType.Folders);
      query.Execute((object) null, 1);
      NodeIDCollection nodeIDs = new NodeIDCollection()
      {
        query.GetRecordNodeID(0)
      };
      this._viewsMngr.SuppressedViews = ImbaseObjectCreatorForm.View2Suppress;
      this._viewsMngr.UpdateViews((ISelectedItems) new NodeItems(handlerPath, handler, nodeIDs, (System.IServiceProvider) this._services), true);
      this.Grid_RestoreSelection();
    }
    finally
    {
      this._tsBtnSearch.Enabled = this._cmmiSearch.Enabled = this._trv.SelectedNode != null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeViewAfterExpand(object sender, TreeViewEventArgs e)
  {
    if (e.Node == null)
      return;
    foreach (TreeNode node in e.Node.Nodes)
      this.UpdateNodeStateIndex(node, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeViewMouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.BaseObjectId == 0L || this._trv.SelectedNode == null || this._trv.SelectedNode.Nodes.Count != 0 || !(this._trv.SelectedNode.Tag is NodeInfo tag) || tag.IsCatalog || tag.IsTableReference)
      return;
    ImbaseObjectCreatorForm.ImbaseObjectHandler imbaseObjSelected = this.ImbaseObjSelected;
    if (imbaseObjSelected == null)
      return;
    long num = imbaseObjSelected(this.BaseObjectId, this.RecordId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (!(sender is TreeView treeView) || treeView.HitTest(e.X, e.Y).Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node = e.Node;
    NodeInfo tag = node.Tag as NodeInfo;
    if (node.StateImageIndex != 0 && node.StateImageIndex != 1 || tag == null || tag.IsCatalog || tag.IsTableReference || tag.IsFavoritesFolder)
      return;
    node.StateImageIndex = (node.StateImageIndex + 1) % 2;
    ImbaseObjectCaptionItem imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(tag.ObjectId, tag.TypeId, node.Text), -1L);
    if (node.StateImageIndex == 0)
    {
      this._selectedItems.Remove(imbaseObject);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
        {
          if (!ImbaseUsageHelper.CanUseImbaseObject(imbaseObject))
            return;
        }
      }
      if (!this.MultiSelect)
        this._selectedItems.Clear();
      this._selectedItems.Add(imbaseObject);
    }
    this.UpdateSelectedStateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tableView_ItemDoubleClick(object sender, EventArgs e)
  {
    if (this.BaseObjectId == 0L || this.RecordId <= -1L)
      return;
    if (this.ImbaseObjSelected != null)
    {
      ImbaseObjectCreatorForm.ImbaseObjectHandler imbaseObjSelected = this.ImbaseObjSelected;
      if (imbaseObjSelected == null)
        return;
      long num = imbaseObjSelected(this.BaseObjectId, this.RecordId);
    }
    else
    {
      if (this._imbaseTableView == null || !(e is DataGridViewCellEventArgs viewCellEventArgs))
        return;
      bool currentValue = this._imbaseTableView.CheckedRecord(viewCellEventArgs.RowIndex);
      TableView.CheckEventArgs ce = new TableView.CheckEventArgs(currentValue);
      this.On_tableView_ItemChecked((object) this, ce);
      if (ce.Cancel)
        return;
      this._imbaseTableView.CheckRecord(viewCellEventArgs.RowIndex, !currentValue);
    }
  }

  /// <summary>Item's checked event.</summary>
  /// <param name="sender"></param>
  /// <param name="ce"></param>
  private void On_tableView_ItemChecked(object sender, TableView.CheckEventArgs ce)
  {
    if (ce == null)
      return;
    ce.Cancel = true;
    if (this._imbaseTableView == null)
      return;
    string columnName = Convert.ToString(-2);
    try
    {
      if (this._imbaseTableView.Grid?.Columns == null || !this._imbaseTableView.Grid.Columns.Contains(columnName))
        return;
      int index1 = this._imbaseTableView.Grid.Columns[columnName].Index;
      int index2 = -1;
      foreach (int typeCaptionAttrId in (IEnumerable<int>) this._objTypeCaptionAttrIds)
      {
        DataGridViewColumn column = this._imbaseTableView.Grid.Columns[typeCaptionAttrId.ToString()];
        if (column != null)
        {
          index2 = column.Index;
          break;
        }
      }
      DataGridViewRow currentRow = this._imbaseTableView.Grid.CurrentRow;
      if (currentRow == null || this._imbaseTableView.DisabledRecord(currentRow) && !ce.Checked)
        return;
      object obj = currentRow.Cells[index1].Value;
      long result;
      if (obj == null || !long.TryParse(Convert.ToString(obj), out result) || result == -1L)
        return;
      ce.Cancel = false;
      string caption = index2 != -1 ? Convert.ToString(currentRow.Cells[index2].Value) : result.ToString();
      ImbaseObjectCaptionItem imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(this._focusedObjectId, Intermech.Imbase.Consts.ImbaseTableRefTypeID, caption), result);
      if (ce.Checked)
      {
        this._selectedItems.Remove(imbaseObject);
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
          {
            if (!ImbaseUsageHelper.CanUseImbaseObject(imbaseObject))
            {
              ce.Cancel = true;
              return;
            }
          }
        }
        if (!this.MultiSelect)
          this._selectedItems.Clear();
        this._selectedItems.Add(imbaseObject);
      }
    }
    finally
    {
      this.UpdateSelectedStateControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_viewsMgr_Enter(object sender, EventArgs e)
  {
    if (this._viewsMngr.ActiveViewPage == null)
      return;
    if (this._viewsMngr.ActiveViewPage.View is ImbaseTableView view1)
      this.RegisterTableView(view1.TblView);
    if (!(this._viewsMngr.ActiveViewPage.View is FormDesignerView view2))
      return;
    view2.ButtonsVisible(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_viewsMgr_Leave(object sender, EventArgs e) => this.UnregisterTableView();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvSelectedItems_Resize(object sender, EventArgs e)
  {
    if (this.lvSelectedItems.Columns.Count <= 0)
      return;
    this.lvSelectedItems.Columns[0].Width = this.lvSelectedItems.Width - 5;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvSelectedItems_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    ImbaseObjectCaptionItem selectedItem = this.GetSelectedItem();
    if (selectedItem == null)
      return;
    if (Control.ModifierKeys == Keys.Control)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
        {
          if (!ImbaseUsageHelper.CanUseImbaseObject(selectedItem))
            return;
        }
      }
      this._selectedItems.Add(new ImbaseObjectCaptionItem(selectedItem.ObjectInfo, selectedItem.RecordId));
      this.UpdateSelectedStateControls();
    }
    else
    {
      this._trv.Focus();
      this._treeBuilder.SetSelectedNode(selectedItem.ObjectInfo.ItemID);
      if (selectedItem.ObjectInfo.ItemTypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        return;
      SelectedRecords.Add(selectedItem.ObjectInfo.ItemID, new long[1]
      {
        selectedItem.RecordId
      });
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedDuplicate_Click(object sender, EventArgs e)
  {
    ImbaseObjectCaptionItem selectedItem = this.GetSelectedItem();
    if (selectedItem == null)
      return;
    this._selectedItems.Add(new ImbaseObjectCaptionItem(selectedItem.ObjectInfo, selectedItem.RecordId));
    this.UpdateSelectedStateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedMoveTop_Click(object sender, EventArgs e)
  {
    int selectedItemIndex = this.GetSelectedItemIndex();
    if (selectedItemIndex < 1)
      return;
    this.SelectedItemMove(selectedItemIndex, 0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedMoveUp_Click(object sender, EventArgs e)
  {
    int selectedItemIndex = this.GetSelectedItemIndex();
    if (selectedItemIndex < 1)
      return;
    this.SelectedItemMove(selectedItemIndex, selectedItemIndex - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedMoveDown_Click(object sender, EventArgs e)
  {
    int selectedItemIndex = this.GetSelectedItemIndex();
    if (selectedItemIndex == -1 || selectedItemIndex == this._selectedItems.Count - 1)
      return;
    this.SelectedItemMove(selectedItemIndex, selectedItemIndex + 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedMoveLast_Click(object sender, EventArgs e)
  {
    int selectedItemIndex = this.GetSelectedItemIndex();
    if (selectedItemIndex == -1 || selectedItemIndex == this._selectedItems.Count - 1)
      return;
    this.SelectedItemMove(selectedItemIndex, this._selectedItems.Count - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedDelete_Click(object sender, EventArgs e)
  {
    if (this.GetSelectedItem() == null)
      return;
    int index = this.lvSelectedItems.Items.IndexOf(this.lvSelectedItems.SelectedItems[0]);
    if (index < 0)
      return;
    this._selectedItems.RemoveAt(index);
    this.UpdateSelectedStateControls(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miSelectedClear_Click(object sender, EventArgs e)
  {
    this._selectedItems.Clear();
    this.UpdateSelectedStateControls(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _cmsSelectedItems_Opening(object sender, CancelEventArgs e)
  {
    ImbaseObjectCaptionItem selectedItem = this.GetSelectedItem();
    this.tsmiSelectedDelete.Enabled = this.tsmiSelectedDuplicate.Enabled = this.tsmiSelectedClear.Enabled = selectedItem != null;
    this.tsmiSelectedMoveTop.Enabled = this.tsmiSelectedMoveUp.Enabled = selectedItem != null && this.GetSelectedItemIndex() > 0;
    this.tsmiSelectedMoveDown.Enabled = this.tsmiSelectedMoveLast.Enabled = selectedItem != null && this.GetSelectedItemIndex() < this._selectedItems.Count - 1;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      this._timer?.Dispose();
      this._timer = (System.Timers.Timer) null;
    }
    this.ServicesFinalization();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseObjectCreatorForm));
    this._cmsSelectedItems = new ContextMenuStrip(this.components);
    this.tsmiSelectedDuplicate = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tsmiSeletedMove = new ToolStripMenuItem();
    this.tsmiSelectedMoveTop = new ToolStripMenuItem();
    this.tsmiSelectedMoveUp = new ToolStripMenuItem();
    this.tsmiSelectedMoveDown = new ToolStripMenuItem();
    this.tsmiSelectedMoveLast = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.tsmiSelectedDelete = new ToolStripMenuItem();
    this.tsmiSelectedClear = new ToolStripMenuItem();
    this.pnlSelected = new Panel();
    this.lvSelectedItems = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.pnlSelectedInfo = new Panel();
    this.lblSelectedInfo = new Label();
    ((ISupportInitialize) this._pbObject).BeginInit();
    this._spltContainer.BeginInit();
    this._spltContainer.Panel2.SuspendLayout();
    this._spltContainer.SuspendLayout();
    this._pnlTop.SuspendLayout();
    this._cmsSelectedItems.SuspendLayout();
    this.pnlSelected.SuspendLayout();
    this.pnlSelectedInfo.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._viewsMngr, "_viewsMngr");
    this._viewsMngr.ActiveViewPageChanged += new EventHandler(this.On_viewsMgr_Enter);
    this._viewsMngr.Enter += new EventHandler(this.On_viewsMgr_Enter);
    this._viewsMngr.Leave += new EventHandler(this.On_viewsMgr_Leave);
    this._trv.LineColor = Color.Black;
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.AfterExpand += new TreeViewEventHandler(this.OnTreeViewAfterExpand);
    this._trv.AfterSelect += new TreeViewEventHandler(this.OnTreeViewAfterSelect);
    this._trv.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.OnTreeViewNodeMouseClick);
    this._trv.MouseDoubleClick += new MouseEventHandler(this.OnTreeViewMouseDoubleClick);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.Images.SetKeyName(0, "filter_imbase.png");
    this._imgList.Images.SetKeyName(1, "filter_imbase_on.png");
    this._imgList.Images.SetKeyName(2, "filter_object.png");
    this._imgList.Images.SetKeyName(3, "filter_object_on.png");
    this._imgList.Images.SetKeyName(4, "filter_sett.bmp");
    componentResourceManager.ApplyResources((object) this._spltContainer, "_spltContainer");
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._cmsSelectedItems.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.tsmiSelectedDuplicate,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsmiSeletedMove,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.tsmiSelectedDelete,
      (ToolStripItem) this.tsmiSelectedClear
    });
    this._cmsSelectedItems.Name = "cmsImbaseTree";
    componentResourceManager.ApplyResources((object) this._cmsSelectedItems, "_cmsSelectedItems");
    this._cmsSelectedItems.Opening += new CancelEventHandler(this._cmsSelectedItems_Opening);
    this.tsmiSelectedDuplicate.Name = "tsmiSelectedDuplicate";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedDuplicate, "tsmiSelectedDuplicate");
    this.tsmiSelectedDuplicate.Click += new EventHandler(this.miSelectedDuplicate_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.tsmiSeletedMove.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiSelectedMoveTop,
      (ToolStripItem) this.tsmiSelectedMoveUp,
      (ToolStripItem) this.tsmiSelectedMoveDown,
      (ToolStripItem) this.tsmiSelectedMoveLast
    });
    this.tsmiSeletedMove.Name = "tsmiSeletedMove";
    componentResourceManager.ApplyResources((object) this.tsmiSeletedMove, "tsmiSeletedMove");
    this.tsmiSelectedMoveTop.Name = "tsmiSelectedMoveTop";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedMoveTop, "tsmiSelectedMoveTop");
    this.tsmiSelectedMoveTop.Click += new EventHandler(this.miSelectedMoveTop_Click);
    this.tsmiSelectedMoveUp.Name = "tsmiSelectedMoveUp";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedMoveUp, "tsmiSelectedMoveUp");
    this.tsmiSelectedMoveUp.Click += new EventHandler(this.miSelectedMoveUp_Click);
    this.tsmiSelectedMoveDown.Name = "tsmiSelectedMoveDown";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedMoveDown, "tsmiSelectedMoveDown");
    this.tsmiSelectedMoveDown.Click += new EventHandler(this.miSelectedMoveDown_Click);
    this.tsmiSelectedMoveLast.Name = "tsmiSelectedMoveLast";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedMoveLast, "tsmiSelectedMoveLast");
    this.tsmiSelectedMoveLast.Click += new EventHandler(this.miSelectedMoveLast_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.tsmiSelectedDelete.Name = "tsmiSelectedDelete";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedDelete, "tsmiSelectedDelete");
    this.tsmiSelectedDelete.Click += new EventHandler(this.miSelectedDelete_Click);
    this.tsmiSelectedClear.Name = "tsmiSelectedClear";
    componentResourceManager.ApplyResources((object) this.tsmiSelectedClear, "tsmiSelectedClear");
    this.tsmiSelectedClear.Click += new EventHandler(this.miSelectedClear_Click);
    this.pnlSelected.Controls.Add((Control) this.lvSelectedItems);
    this.pnlSelected.Controls.Add((Control) this.pnlSelectedInfo);
    componentResourceManager.ApplyResources((object) this.pnlSelected, "pnlSelected");
    this.pnlSelected.Name = "pnlSelected";
    this._splitContainerLeft.Panel2.Controls.Add((Control) this.pnlSelected);
    this.lvSelectedItems.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvSelectedItems.ContextMenuStrip = this._cmsSelectedItems;
    componentResourceManager.ApplyResources((object) this.lvSelectedItems, "lvSelectedItems");
    this.lvSelectedItems.FullRowSelect = true;
    this.lvSelectedItems.HideSelection = false;
    this.lvSelectedItems.Items.AddRange(new ListViewItem[3]
    {
      (ListViewItem) componentResourceManager.GetObject("lvSelectedItems.Items"),
      (ListViewItem) componentResourceManager.GetObject("lvSelectedItems.Items1"),
      (ListViewItem) componentResourceManager.GetObject("lvSelectedItems.Items2")
    });
    this.lvSelectedItems.MultiSelect = false;
    this.lvSelectedItems.Name = "lvSelectedItems";
    this.lvSelectedItems.UseCompatibleStateImageBehavior = false;
    this.lvSelectedItems.View = View.List;
    this.lvSelectedItems.MouseDoubleClick += new MouseEventHandler(this.lvSelectedItems_MouseDoubleClick);
    this.lvSelectedItems.Resize += new EventHandler(this.lvSelectedItems_Resize);
    this.pnlSelectedInfo.Controls.Add((Control) this.lblSelectedInfo);
    componentResourceManager.ApplyResources((object) this.pnlSelectedInfo, "pnlSelectedInfo");
    this.pnlSelectedInfo.Name = "pnlSelectedInfo";
    componentResourceManager.ApplyResources((object) this.lblSelectedInfo, "lblSelectedInfo");
    this.lblSelectedInfo.Name = "lblSelectedInfo";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ImbaseObjectCreatorForm);
    this.Controls.SetChildIndex((Control) this._pnlTop, 0);
    this.Controls.SetChildIndex((Control) this._spltContainer, 0);
    ((ISupportInitialize) this._pbObject).EndInit();
    this._spltContainer.Panel2.ResumeLayout(false);
    this._spltContainer.EndInit();
    this._spltContainer.ResumeLayout(false);
    this._pnlTop.ResumeLayout(false);
    this._cmsSelectedItems.ResumeLayout(false);
    this.pnlSelected.ResumeLayout(false);
    this.pnlSelectedInfo.ResumeLayout(false);
    this.pnlSelectedInfo.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Делегат выбора из справочника объекта Imbase.</summary>
  public delegate long ImbaseObjectHandler(long baseObjId, long tblLinkId);
}
