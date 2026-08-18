
// Type: Intermech.Navigator.Controls.AdvRelationsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Генетически модифицированная закладка на основе ChildrenView -
/// показывает одноуровневые составы объектов в рамках определённых
/// настроек фильтрации состава, контекстах и по определённому типу связи
/// </summary>
public class AdvRelationsView : ChildrenView
{
  /// <summary>
  /// Кэш значений [(Int64)Идентификатор связи] = [(SubstitutesNodeID)Описание узла в гриде]
  /// </summary>
  private Dictionary<long, AdvRelationsNodeID> _items = new Dictionary<long, AdvRelationsNodeID>();
  /// <summary>Список связей, загруженных в грид</summary>
  private List<long> _relations = new List<long>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public AdvRelationsView() => this._editingModeButtonItem.Visible = true;

  /// <summary>Вернуть описание узла для указанной связи</summary>
  /// <param name="PrjLinkID">Идентификатор связи</param>
  /// <returns>Описание узла для указанной связи</returns>
  public virtual AdvRelationsNodeID this[long PrjLinkID]
  {
    get => !this._items.ContainsKey(PrjLinkID) ? (AdvRelationsNodeID) null : this._items[PrjLinkID];
  }

  /// <summary>Количество записей, загруженных в грид</summary>
  public virtual int ItemsCount
  {
    get
    {
      if (this._items == null)
        this._items = new Dictionary<long, AdvRelationsNodeID>();
      if (this._items.Count == 0 && this._grid.Rows.Count > 0)
        this.BuildRelations();
      return this._items == null ? 0 : this._items.Count;
    }
  }

  /// <summary>Получить описание узла с указанным индексом</summary>
  /// <param name="index">Индекс узла</param>
  /// <returns>Описание узла с указанным индексом</returns>
  public virtual INodeID this[int index]
  {
    get => this._relations == null ? (INodeID) null : (INodeID) this._items[this._relations[index]];
  }

  /// <summary>Список выделенных связей из состава</summary>
  public virtual List<long> SelectedRelationsFromComposition
  {
    get
    {
      List<long> relationsFromComposition = new List<long>();
      if (this.SelectedItems == null || this.SelectedItems.Count == 0)
        return relationsFromComposition;
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        if (this.SelectedItems.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData)
          relationsFromComposition.Add(itemData.Value);
      }
      return relationsFromComposition;
    }
  }

  /// <summary>Коллекция дополнительных атрибутов связей состава</summary>
  public virtual RelationAttributesPackage RelationsAttributes
  {
    get
    {
      if (this._path == null)
        return (RelationAttributesPackage) null;
      if (!(this._path.RootDescriptor is AdvRelationsDescriptor rootDescriptor))
        return (RelationAttributesPackage) null;
      RelationAttributesPackage relationsAttributes = new RelationAttributesPackage(rootDescriptor.Attributes);
      foreach (KeyValuePair<long, AdvRelationsNodeID> keyValuePair in this._items)
        relationsAttributes.Values.Add(keyValuePair.Key, keyValuePair.Value.Values);
      return relationsAttributes;
    }
  }

  /// <summary>
  /// Получить список связей, загруженных в грид (в том порядке, в котором записи располагаются в гриде)
  /// </summary>
  public virtual List<long> Relations
  {
    get
    {
      List<long> relations = new List<long>();
      int count = this._grid.Cols.Count;
      if (this._grid.Rows.Count == 0 || count < 2 || !(this.Node is AdvRelationsNode))
        return relations;
      for (int index = 0; index < this._grid.Rows.Count; ++index)
      {
        iGRow row = this._grid.Rows[index];
        if (row.Type == iGRowType.Normal && this.GetNodeIDForRow(row) is AdvRelationsNodeID nodeIdForRow)
          relations.Add(nodeIdForRow.PrjLinkID);
      }
      return relations;
    }
  }

  /// <summary>
  /// Перестроить списки связей на основании данных, загруженных в грид
  /// </summary>
  public virtual void BuildRelations()
  {
    this._items.Clear();
    this._relations.Clear();
    int count = this._grid.Cols.Count;
    if (this._grid.Rows.Count == 0 || count < 2 || !(this.Node is AdvRelationsNode))
      return;
    foreach (iGRow row in (IEnumerable) this._grid.Rows)
    {
      if (row.Type == iGRowType.Normal && this.GetNodeIDForRow(row) is AdvRelationsNodeID nodeIdForRow)
      {
        this._items.Add(nodeIdForRow.PrjLinkID, nodeIdForRow);
        this._relations.Add(nodeIdForRow.PrjLinkID);
      }
    }
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="services">Контейнер сервисов</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="parentPath">Путь к родительскому узлу</param>
  /// <param name="parentNode">Родительский узел</param>
  /// <param name="nodeId">Коревой узел</param>
  /// <param name="services">Контейнер сервисов</param>
  public override void Initialize(
    NodeIDPath parentPath,
    INode parentNode,
    INodeID nodeId,
    System.IServiceProvider services)
  {
    base.Initialize(parentPath, parentNode, nodeId, services);
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="rootDescriptor">Описание корневого узла</param>
  /// <param name="services">Контейнер сервисов</param>
  public override void Initialize(IDescriptor rootDescriptor, System.IServiceProvider services)
  {
    base.Initialize(rootDescriptor, services);
  }

  /// <summary>Состав читается полностью, пакетное чтение отсутвует</summary>
  protected override int FetchCount => -1;

  /// <summary>Название потока, в котором будут сохранены настройки</summary>
  public override string StateStreamPrefix => "AdvRelationsView_" + base.StateStreamPrefix;

  /// <summary>Обновить элементы управления</summary>
  protected override void UpdateControls()
  {
    base.UpdateControls();
    this._readNextToolStripDropDownButton.Visible = false;
    this._readAllToolStripDropDownButton.Visible = false;
    this._embeddedViewsDropDownMenuItem.Visible = false;
  }

  /// <summary>
  /// Выполнить синхронизацию состояния грида с источником данных и перечитать грид, если что-то изменилось
  /// </summary>
  protected override void GridReloadIfNeed()
  {
    base.GridReloadIfNeed();
    this.GridSaveState((Stream) null);
  }

  /// <summary>
  /// Произошло изменение в таблице источника данных для компонента
  /// </summary>
  protected internal override void RaiseDataTableChanged()
  {
    base.RaiseDataTableChanged();
    this.BuildRelations();
  }

  /// <summary>Пользовательская отрисовка фона в ячейках</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected override void CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (this.ShowCellCustomBackground != null)
    {
      iGCell cell = this._grid.Rows[e.RowIndex].Cells[e.ColIndex];
      INodeID nodeIdForRow = this.GetNodeIDForRow(e.RowIndex);
      this.ShowCellCustomBackground((object) this, new CustomCellBackgroundEventArgs(e, this._grid, cell, nodeIdForRow));
    }
    else
      base.CustomDrawCellBackground(sender, e);
  }

  /// <summary>
  /// Событие возникает в тот момент, когда грид может показать пользовательский фон в ячейке
  /// </summary>
  [CustomDescription("Attribute.Client.Core_78")]
  public event CustomCellBackgroundEventHandler ShowCellCustomBackground;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdvRelationsView));
    ((ISupportInitialize) this._grid).BeginInit();
    this.SuspendLayout();
    this._toolBar.AccessibleDescription = (string) null;
    this._toolBar.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._toolBar, "tbViewBar");
    this._toolBar.BackgroundImage = (Image) null;
    this._toolBar.Font = (Font) null;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._toolBar, componentResourceManager.GetString("tbViewBar.ToolTip"));
    componentResourceManager.ApplyResources((object) this._embeddedViewsDropDownMenuItem, "btViewNames");
    componentResourceManager.ApplyResources((object) this._toggleManualSortingButtonItem, "btClearSorting");
    this._grid.AccessibleDescription = (string) null;
    this._grid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this._grid.BackgroundImage = (Image) null;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.Key = (string) null;
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this._grid.Font = (Font) null;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, componentResourceManager.GetString("grid.ToolTip"));
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsButtonItem, "btCollapseAll");
    componentResourceManager.ApplyResources((object) this._expandAllGroupsButtonItem, "btExpandAll");
    this._pageViewsManager.AccessibleDescription = (string) null;
    this._pageViewsManager.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this._pageViewsManager.BackgroundImage = (Image) null;
    this._pageViewsManager.Font = (Font) null;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pageViewsManager, componentResourceManager.GetString("ViewsManager.ToolTip"));
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.Name = nameof (AdvRelationsView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
