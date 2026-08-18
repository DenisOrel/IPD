
// Type: Intermech.Navigator.CompositionObjTypesFilterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator;

/// <summary>
/// Редактор фильтров по родительским и дочерним типам объектов
/// </summary>
public sealed class CompositionObjTypesFilterForm : Form
{
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  private Dictionary<int, Icon> _typesIcons = new Dictionary<int, Icon>();
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList _images;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _objtypesIcons;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>Контейнер настроек списка фильтров в XML</summary>
  private XMLSettingsStorage _settings = new XMLSettingsStorage();
  /// <summary>Текущие фильтры</summary>
  private CompositionByObjectTypesFilters _filters;
  /// <summary>Активный фильтр</summary>
  private ICompositionByObjectTypesFiltration _support;
  /// <summary>Ссылка на текущие настройки пользователя и его роли</summary>
  private ICurrentUserAndRole _userRole;
  /// <summary>Есть ли изменения в списке фильтров</summary>
  private bool _isChanged;
  /// <summary>Спрятана ли панель с хинтом</summary>
  private static bool _isHintHidden;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnOK;
  private SplitContainer panelsMain;
  private TableLayoutPanel tableLayoutPanel;
  private Label labelHint;
  private PictureBox pictureClose;
  private PictureBox pictureHint;
  private SplitContainer panelsChild;
  private MenuBar menuParentTypes;
  private ContextMenuBarItem contextMenuBarParentTypes;
  private MenuButtonItem mnpParentTypeUp;
  private MenuButtonItem mnpParentTypeDown;
  private MenuButtonItem mnpParentTypeAdd;
  private MenuButtonItem mnpParentTypeDelete;
  private MenuButtonItem mnpParentTypeRefresh;
  private Intermech.VirtualTreeView.VirtualTreeView treeParentObjects;
  protected Column columnParentObjects;
  private Intermech.Bars.ToolBar toolBarParObjTypes;
  private MenuBar menuChildTypes;
  private ContextMenuBarItem contextMenuChildTypes;
  private Intermech.VirtualTreeView.VirtualTreeView treeChildTypes;
  protected Column columnHiddenChilds;
  private Intermech.Bars.ToolBar toolBarObjTypes;
  private Intermech.Bars.ToolBar toolBar1;
  private iGrid listFilters;
  private ImageList imagesMenus;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private ButtonItem btnLoadFilters;
  private ButtonItem btnSaveFilters;
  private ButtonItem btnFilterAdd;
  private ButtonItem btnFilterUp;
  private ButtonItem btnFilterDown;
  private ButtonItem btnFilterDelete;
  private ToolTip toolTip;
  private ButtonItem btParentTypeAdd;
  private ButtonItem btParentTypeDelete;
  private ButtonItem btParentTypeUp;
  private ButtonItem btParentTypeDown;
  private ButtonItem btParentTypeRefresh;
  private MenuBar menuFilters;
  private ContextMenuBarItem contextMenuBarFilters;
  private MenuButtonItem mnpFilterAdd;
  private MenuButtonItem mnpFilterDelete;
  private MenuButtonItem mnpFilterUp;
  private MenuButtonItem mnpFilterDown;
  private MenuButtonItem mnpLoadFilters;
  private MenuButtonItem mnpSaveFilters;
  private Column columnChildsCheck;
  private CellEditor cellEditor1;
  private CheckBox checkBox1;
  private MenuButtonItem mnpChildTypesCheckAll;
  private MenuButtonItem mnpChildTypesUncheckAll;
  private ButtonItem btChildrenTypesCheckAll;
  private ButtonItem btChildrenTypesUncheckAll;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="filters">Текущие фильтры</param>
  /// <param name="support">Активный фильтр</param>
  public CompositionObjTypesFilterForm(
    ICompositionByObjectTypesFilters filters,
    ICompositionByObjectTypesFiltration support)
  {
    this.InitializeComponent();
    this.Init(filters, support);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 755);
  }

  /// <summary>Вызвать форму</summary>
  /// <param name="filters">Список фильтров</param>
  /// <param name="support">Активный фильтр</param>
  /// <returns>Результаты вызова формы</returns>
  public static DialogResult Execute(
    ICompositionByObjectTypesFilters filters,
    ICompositionByObjectTypesFiltration support)
  {
    using (CompositionObjTypesFilterForm objTypesFilterForm = new CompositionObjTypesFilterForm(filters, support))
    {
      int num = (int) objTypesFilterForm.ShowDialog();
      if (num == 1)
        filters.Assign((ICompositionByObjectTypesFilters) objTypesFilterForm._filters);
      return (DialogResult) num;
    }
  }

  private void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBar1.Renderer = renderer;
    this.toolBarObjTypes.Renderer = renderer;
    this.toolBarParObjTypes.Renderer = renderer;
    this.menuChildTypes.Renderer = renderer;
    this.menuFilters.Renderer = renderer;
    this.menuParentTypes.Renderer = renderer;
  }

  private void CompositionObjTypesFilterForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void CompositionObjTypesFilterForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void DoLoadFilters(object sender, EventArgs e)
  {
    if (this._filters != null && this._filters.Count > 0 && MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_560"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes || this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
    if (!xmlStorage.Load(this.openFileDialog.FileName))
      return;
    this._filters.Load(xmlStorage, (XmlNode) null);
    this._isChanged = true;
    this.FillFiltersList();
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_561"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void DoSaveFilters(object sender, EventArgs e)
  {
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
    this._filters.Save(xmlStorage, (XmlNode) null);
    if (!xmlStorage.Save(this.saveFileDialog.FileName))
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_562"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void DoCloseHint(object sender, EventArgs e)
  {
    this.tableLayoutPanel.RowStyles[0].SizeType = SizeType.Absolute;
    this.tableLayoutPanel.RowStyles[0].Height = 0.0f;
    CompositionObjTypesFilterForm._isHintHidden = true;
  }

  private void DoFilterAdd(object sender, EventArgs e)
  {
    iGRow iGrow = this.AddFilterRow(this._filters.Add());
    this.listFilters.CurRow = iGrow;
    iGrow.EnsureVisible();
    this._isChanged = true;
    this.UpdateControls();
  }

  private void DoFilterUp(object sender, EventArgs e)
  {
    iGRow curRow = this.listFilters.CurRow;
    ICompositionByObjectTypesFilter tag = curRow != null ? curRow.Tag as ICompositionByObjectTypesFilter : (ICompositionByObjectTypesFilter) null;
    if (curRow == null || curRow.Index == 0 || tag == null)
      return;
    int index = this._filters.IndexOf(tag);
    ICompositionByObjectTypesFilter filter = this._filters[index - 1];
    this._filters[index - 1] = tag;
    this._filters[index] = filter;
    curRow.Move(curRow.Index - 1);
    curRow.EnsureVisible();
    this._isChanged = true;
    this.UpdateControls();
  }

  private void DoFilterDown(object sender, EventArgs e)
  {
    iGRow curRow = this.listFilters.CurRow;
    ICompositionByObjectTypesFilter tag = curRow != null ? curRow.Tag as ICompositionByObjectTypesFilter : (ICompositionByObjectTypesFilter) null;
    if (curRow == null || curRow.Index >= this._filters.Count - 1 || tag == null)
      return;
    int index = this._filters.IndexOf(tag);
    ICompositionByObjectTypesFilter filter = this._filters[index + 1];
    this._filters[index + 1] = tag;
    this._filters[index] = filter;
    this.listFilters.Rows[curRow.Index + 1].Move(curRow.Index);
    this._isChanged = true;
    this.UpdateControls();
  }

  private void DoFilterDelete(object sender, EventArgs e)
  {
    iGRow curRow = this.listFilters.CurRow;
    int val1 = curRow != null ? curRow.Index : -1;
    ICompositionByObjectTypesFilter tag = curRow != null ? curRow.Tag as ICompositionByObjectTypesFilter : (ICompositionByObjectTypesFilter) null;
    if (tag == null || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_563"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this._filters.Remove(tag.GUID);
    this.listFilters.Rows.RemoveAt(curRow.Index);
    int index = Math.Min(val1, this.listFilters.Rows.Count - 1);
    if (index >= 0)
    {
      this.listFilters.CurRow = this.listFilters.Rows[index];
      this.listFilters.Rows[index].EnsureVisible();
    }
    this._isChanged = true;
    this.UpdateControls();
  }

  private void DoObjTypeAdd(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (currentFilter == null)
      return;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_564"), typeof (ObjectTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    Guid empty = Guid.Empty;
    List<Guid> guidList = new List<Guid>();
    int num1 = currentFilter.ParentTypesCount + selectorForm.IDList.Count > Intermech.Consts.FiltrationMaxObjTypes ? 1 : 0;
    int num2 = Intermech.Consts.FiltrationMaxObjTypes - currentFilter.ParentTypesCount;
    if (num1 != 0)
    {
      int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_565") + LocalizationHolder.rm.GetString("Client.Core_566") + LocalizationHolder.rm.GetString("Client.Core_567"), (object) Intermech.Consts.FiltrationMaxObjTypes, (object) num2), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    foreach (int id in selectorForm.IDList)
    {
      if (MetaDataHelper.ExistsObjectType(id))
      {
        if (currentFilter.ParentTypesCount < Intermech.Consts.FiltrationMaxObjTypes)
        {
          Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(id);
          if (currentFilter.Add(objectTypeGuid))
            guidList.Add(objectTypeGuid);
        }
        else
          break;
      }
    }
    this._isChanged = true;
    this.FillParentObjectTypesTree(false);
  }

  private void DoObjTypeDelete(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (currentFilter == null || this.treeParentObjects.SelectedRows.Count == 0 || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_568"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    for (int index = 0; index < this.treeParentObjects.SelectedRows.Count; ++index)
    {
      Row selectedRow = this.treeParentObjects.SelectedRows[index];
      if (selectedRow.Item != null)
      {
        Guid parentType = (Guid) selectedRow.Item;
        currentFilter.Remove(parentType);
      }
    }
    this._isChanged = true;
    this.FillParentObjectTypesTree(false);
  }

  private void DoObjectTypeRefresh(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (currentFilter == null)
      return;
    currentFilter.SyncMetaData();
    this._isChanged = true;
    this.FillParentObjectTypesTree(false);
  }

  private void DoParentObjectTypeUp(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    Guid currentParentType = this.GetCurrentParentType();
    if (currentFilter == null || currentParentType.Equals(Guid.Empty))
      return;
    int idx1 = currentFilter.IndexOf(currentParentType);
    int idx2 = idx1 - 1;
    if (!currentFilter.Swap(idx1, idx2))
      return;
    this._isChanged = true;
    this.FillParentObjectTypesTree(false);
  }

  private void DoParentObjectTypeDown(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    Guid currentParentType = this.GetCurrentParentType();
    if (currentFilter == null || currentParentType.Equals(Guid.Empty))
      return;
    int idx1 = currentFilter.IndexOf(currentParentType);
    int idx2 = idx1 + 1;
    if (!currentFilter.Swap(idx1, idx2))
      return;
    this._isChanged = true;
    this.FillParentObjectTypesTree(false);
  }

  private void DoCheckAllChildTypes(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    Guid currentParentType = this.GetCurrentParentType();
    if (currentFilter == null || currentParentType.Equals(Guid.Empty) || !currentFilter.Exists(this.GetCurrentParentType()))
      return;
    List<Guid> childObjectTypesGuid = MetaDataHelper.GetApplicabilityChildObjectTypesGuid(currentParentType, (IEnumerable<Guid>) this._userRole.Rule.GetObjectTypeVisibleRelationsGuids(currentParentType, true));
    for (int index = 0; index < childObjectTypesGuid.Count; ++index)
      currentFilter.Add(currentParentType, childObjectTypesGuid[index]);
    this.FillChildrenTypes();
    this._isChanged = true;
    this.UpdateControls();
  }

  private void DoUncheckAllChildTypes(object sender, EventArgs e)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    Guid currentParentType = this.GetCurrentParentType();
    if (currentFilter == null || currentParentType.Equals(Guid.Empty))
      return;
    List<Guid> childObjectTypesGuid = MetaDataHelper.GetApplicabilityChildObjectTypesGuid(currentParentType, (IEnumerable<Guid>) this._userRole.Rule.GetObjectTypeVisibleRelationsGuids(currentParentType, true));
    for (int index = 0; index < childObjectTypesGuid.Count; ++index)
      currentFilter.Remove(currentParentType, childObjectTypesGuid[index]);
    this.FillChildrenTypes();
    this._isChanged = true;
    this.UpdateControls();
  }

  private void listFilters_CurRowChanged(object sender, EventArgs e)
  {
    this.FillParentObjectTypesTree(true);
  }

  private void listFilters_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    ICompositionByObjectTypesFilter tag = this.listFilters.Rows[e.RowIndex].Tag as ICompositionByObjectTypesFilter;
    e.Text = tag.Name;
    e.DoDefault = e.ColIndex == 1;
  }

  private void listFilters_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    if (e.NewValue != null && !(e.NewValue.ToString() == string.Empty))
      return;
    e.Result = iGEditResult.Proceed;
  }

  private void listFilters_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    iGRow row = this.listFilters.Rows[e.RowIndex];
    ICompositionByObjectTypesFilter tag = row.Tag as ICompositionByObjectTypesFilter;
    if (e.ColIndex != 1 || row.Cells[1].Value == null)
      return;
    tag.Name = row.Cells[1].Value.ToString();
    this._isChanged = true;
    this.UpdateControls();
  }

  private void listFilters_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    iGCell iGcell = this.listFilters.Cells.FromPoint(e.X, e.Y);
    iGRow row = iGcell?.Row;
    if (row == null || this.listFilters.SelectedCells.Contains(iGcell))
      return;
    this.SelectGridRow(this.listFilters, row, true, true);
  }

  private void listFilters_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.contextMenuBarFilters.Show((Control) this.listFilters, e.Location);
  }

  private void treeParentObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is Guid))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType((Guid) e.Row.Item);
    if (objectType == null)
      return;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(objectType.ObjectTypeID, e.Row.Selected ? this.treeParentObjects.RowSelectedStyle.BackColor : this.treeParentObjects.RowStyle.BackColor);
  }

  private void treeParentObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is Guid))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType((Guid) e.Row.Item);
    if (e.Column != this.columnParentObjects)
      return;
    e.CellData.Value = objectType != null ? (object) objectType.ObjectTypeName : (object) LocalizationHolder.rm.GetString("Client.Core_236");
  }

  private void treeParentObjects_GetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    e.ChildPolicy = RowChildPolicy.Normal;
  }

  private void treeParentObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (!(e.Row.Item is ICompositionByObjectTypesFilter))
      return;
    e.Children = (IList) (e.Row.Item as ICompositionByObjectTypesFilter).ParentObjectTypes;
  }

  private void treeParentObjects_FocusRowChanged(object sender, EventArgs e)
  {
    this.FillChildrenTypes();
    this.UpdateControls();
  }

  private void treeParentObjects_SelectionChanged(object sender, EventArgs e)
  {
    this.FillChildrenTypes();
    this.UpdateControls();
  }

  private void treeParentObjects_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuBarParentTypes.Show((Control) this.treeParentObjects, e.Location);
  }

  private void treeChildTypes_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  private void treeChildTypes_FocusRowChanged(object sender, EventArgs e) => this.UpdateControls();

  private void treeChildTypes_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is int))
      return;
    int objTypeID = (int) e.Row.Item;
    if (e.Column == this.columnChildsCheck)
    {
      e.CellData.Value = (object) this.GetCurrentFilter().Exists(this.GetCurrentParentType(), MetaDataHelper.GetObjectTypeGuid(objTypeID));
    }
    else
    {
      if (e.Column != this.columnHiddenChilds)
        return;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
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
  }

  private void treeChildTypes_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is IMSObjectType)
    {
      IMSObjectType imsObjectType = e.Row.Item as IMSObjectType;
      e.Children = (IList) MetaDataHelper.GetApplicabilityChildObjectTypesID(imsObjectType.ObjectTypeID, (IEnumerable<int>) this._userRole.Rule.GetObjectTypeVisibleRelations(imsObjectType.ObjectTypeID, true));
    }
    else
    {
      if (!(e.Row.Item is int))
        return;
      int parentTypeID = (int) e.Row.Item;
      e.Children = (IList) MetaDataHelper.GetObjectTypeChildrenID(parentTypeID);
    }
  }

  private void treeChildTypes_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is int))
      return;
    int objTypeID = (int) e.Row.Item;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(objTypeID, e.Row.Selected ? this.treeChildTypes.RowSelectedStyle.BackColor : this.treeChildTypes.RowStyle.BackColor);
  }

  private void treeChildTypes_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuChildTypes.Show((Control) this.treeChildTypes, e.Location);
  }

  private void treeChildTypes_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!(e.Row.Item is int))
      return;
    int objectTypeID = (int) e.Row.Item;
    if (e.Column != this.columnChildsCheck)
      return;
    if ((bool) e.NewValue)
      this.MarkChildObjectType(objectTypeID);
    else
      this.UnmarkChildObjectType(objectTypeID);
    this.treeChildTypes.UpdateRows(false);
    this._isChanged = true;
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  private void UpdateControls()
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    int num1 = this._filters.IndexOf(currentFilter);
    Guid currentParentType = this.GetCurrentParentType();
    this.GetCurrentChildrenType();
    int num2 = currentFilter != null ? currentFilter.IndexOf(currentParentType) : -1;
    int parentTypesCount = currentFilter != null ? currentFilter.ParentTypesCount : 0;
    this.btnOK.Enabled = this._isChanged;
    this.btnCancel.Enabled = true;
    this.btnLoadFilters.Enabled = true;
    this.mnpLoadFilters.Enabled = this.btnLoadFilters.Enabled;
    this.btnSaveFilters.Enabled = true;
    this.mnpSaveFilters.Enabled = this.btnSaveFilters.Enabled;
    this.btnFilterAdd.Enabled = true;
    this.mnpFilterAdd.Enabled = this.btnFilterAdd.Enabled;
    this.btnFilterDelete.Enabled = currentFilter != null;
    this.mnpFilterDelete.Enabled = this.btnFilterDelete.Enabled;
    this.btnFilterUp.Enabled = num1 > 0;
    this.mnpFilterUp.Enabled = this.btnFilterUp.Enabled;
    this.btnFilterDown.Enabled = num1 >= 0 && num1 < this._filters.Count - 1;
    this.mnpFilterDown.Enabled = this.btnFilterDown.Enabled;
    this.btParentTypeAdd.Enabled = currentFilter != null;
    this.mnpParentTypeAdd.Enabled = this.btParentTypeAdd.Enabled;
    this.btParentTypeDelete.Enabled = currentFilter != null && !currentParentType.Equals(Guid.Empty);
    this.mnpParentTypeDelete.Enabled = this.btParentTypeDelete.Enabled;
    this.btParentTypeUp.Enabled = currentFilter != null && !currentParentType.Equals(Guid.Empty) && num2 > 0;
    this.mnpParentTypeUp.Enabled = this.btParentTypeUp.Enabled;
    this.btParentTypeDown.Enabled = currentFilter != null && !currentParentType.Equals(Guid.Empty) && num2 >= 0 && num2 < parentTypesCount - 1;
    this.mnpParentTypeDown.Enabled = this.btParentTypeDown.Enabled;
    this.btParentTypeRefresh.Enabled = currentFilter != null;
    this.mnpParentTypeRefresh.Enabled = this.btParentTypeRefresh.Enabled;
    this.btChildrenTypesCheckAll.Enabled = currentFilter != null && !currentParentType.Equals(Guid.Empty);
    this.mnpChildTypesCheckAll.Enabled = this.btChildrenTypesCheckAll.Enabled;
    this.btChildrenTypesUncheckAll.Enabled = currentFilter != null && !currentParentType.Equals(Guid.Empty);
    this.mnpChildTypesUncheckAll.Enabled = this.btChildrenTypesUncheckAll.Enabled;
  }

  /// <summary>Выделить или убрать выделение с указанной строки</summary>
  /// <param name="grid">Грид</param>
  /// <param name="row">Строка</param>
  /// <param name="select">Выделить или убрать выделение</param>
  /// <param name="makeCurr">При select = true можно ли сделать указанную строку текущей</param>
  private void SelectGridRow(iGrid grid, iGRow row, bool select, bool makeCurr)
  {
    for (int colIndex = 0; colIndex < grid.Cols.Count; ++colIndex)
      row.Cells[colIndex].Selected = select;
    if (!(select & makeCurr))
      return;
    grid.CurRow = row;
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="backColor"></param>
  /// <returns>Значок для указанного типа объекта</returns>
  private Icon GetObjTypeIcon(int objTypeID, Color backColor)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return (Icon) null;
    objTypeID = Math.Max(objTypeID, -1);
    if (this._typesIcons.ContainsKey(objTypeID))
      return this._typesIcons[objTypeID];
    if (this._objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Icon) null;
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this._objtypesIcons.GetIcon(4, objTypeID), backColor);
    this._typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeGuid">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  private Image GetObjTypeImage(Guid objTypeGuid)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeGuid))
      return (Image) null;
    int index = this._objtypesIcons.IndexOf(4, Math.Max(MetaDataHelper.GetObjectTypeID(objTypeGuid), -1));
    return index < 0 ? (Image) null : this._objtypesIcons.ImageList.Images[index];
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  private int GetObjTypeIconIndex(int objTypeID)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return -1;
    objTypeID = Math.Max(objTypeID, -1);
    return this._objtypesIcons.IndexOf(4, objTypeID);
  }

  /// <summary>Вернуть значок для указанного типа связи</summary>
  /// <param name="relTypeGuid">Идентификатор типа связи</param>
  /// <returns>Значок для указанного типа связи</returns>
  private int GetRelTypeIconIndex(Guid relTypeGuid)
  {
    return !MetaDataHelper.ExistsRelationType(relTypeGuid) ? -1 : this._objtypesIcons.IndexOf(6, Math.Max(MetaDataHelper.GetRelationTypeID(relTypeGuid), -1));
  }

  /// <summary>Добавить новый фильтр в список фильтров</summary>
  /// <param name="filter">Описание фильтра</param>
  /// <returns>Строка из списка</returns>
  private iGRow AddFilterRow(ICompositionByObjectTypesFilter filter)
  {
    iGRow iGrow = this.listFilters.Rows.Add();
    iGrow.Key = filter.GUID.ToString();
    iGrow.Cells[0].ImageIndex = this._support == null || !this._support.ActiveFilterGuid.Equals(filter.GUID) ? this._images.ImageIndex("imgFunnel") : this._images.ImageIndex("imgFunnelActive");
    iGrow.Cells[1].Value = (object) filter.Name;
    iGrow.Tag = (object) filter;
    return iGrow;
  }

  /// <summary>Заполнить список фильтров</summary>
  private void FillFiltersList()
  {
    try
    {
      this.listFilters.BeginUpdate();
      this.listFilters.Redraw = false;
      this.listFilters.Rows.Clear();
      this.listFilters.Cols[0].CellStyle.ReadOnly = iGBool.True;
      this.listFilters.Cols[1].CellStyle.ReadOnly = iGBool.False;
      this.listFilters.Cols[1].CellStyle.Type = iGCellType.Text;
      for (int index = 0; index < this._filters.Count; ++index)
      {
        iGRow row = this.AddFilterRow(this._filters[index]);
        if (this._support != null && this._support.ActiveFilterGuid.Equals(this._filters[index].GUID))
          this.SelectGridRow(this.listFilters, row, true, true);
      }
    }
    finally
    {
      if (this.listFilters.SelectedCells.Count == 0 && this.listFilters.Rows.Count > 0)
        this.SelectGridRow(this.listFilters, this.listFilters.Rows[0], true, true);
      this.listFilters.Redraw = true;
      this.listFilters.EndUpdate();
      this.listFilters.Update();
      this.listFilters_CurRowChanged((object) this, (EventArgs) null);
    }
  }

  /// <summary>
  /// Обновить (или добавить) строку с фильтром (GUID фильтра остался неизменным)
  /// </summary>
  /// <param name="filter">Фильтр</param>
  /// <returns>Строка с указанным фильтром</returns>
  private iGRow UpdateFilterRow(ICompositionByObjectTypesFilter filter)
  {
    if (filter == null)
      return (iGRow) null;
    iGRowCollection rows = this.listFilters.Rows;
    Guid guid = filter.GUID;
    string key = guid.ToString();
    iGRow iGrow = rows[key];
    if (iGrow == null)
      return this.AddFilterRow(filter);
    iGrow.Cells[1].Value = (object) filter.Name;
    iGCell cell = iGrow.Cells[0];
    int num;
    if (this._support != null)
    {
      guid = this._support.ActiveFilterGuid;
      if (guid.Equals(filter.GUID))
      {
        num = this._images.ImageIndex("imgFunnelActive");
        goto label_8;
      }
    }
    num = this._images.ImageIndex("imgFunnel");
label_8:
    cell.ImageIndex = num;
    return iGrow;
  }

  /// <summary>Заполнить дерево родительских типов объектов</summary>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  private void FillParentObjectTypesTree(bool resetDatasource)
  {
    int num = this.treeParentObjects.SelectedRow == null ? 0 : (!resetDatasource ? 1 : 0);
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (resetDatasource)
      this.treeParentObjects.DataSource = (object) currentFilter;
    this.treeParentObjects.UpdateRows(true);
    if (num == 0 && this.treeParentObjects.RootRow != null && this.treeParentObjects.RootRow.NumChildren > 0)
      this.treeParentObjects.SelectedRows.Add(this.treeParentObjects.RootRow.ChildRowByIndex(0));
    this.FillChildrenTypes();
    this.UpdateControls();
  }

  /// <summary>Заполнить список дочерних типов объектов</summary>
  private void FillChildrenTypes()
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    Guid currentParentType = this.GetCurrentParentType();
    IMSObjectType objectType = !currentParentType.Equals(Guid.Empty) ? MetaDataHelper.GetObjectType(currentParentType) : (IMSObjectType) null;
    try
    {
      this.treeChildTypes.DataSource = currentFilter == null || currentParentType.Equals(Guid.Empty) || objectType == null || !currentFilter.Exists(currentParentType) ? (object) (IMSObjectType) null : (object) objectType;
    }
    finally
    {
      if (this.treeChildTypes.RootRow != null && this.treeChildTypes.RootRow.NumChildren > 0)
        this.treeChildTypes.SelectedRows.Add(this.treeChildTypes.RootRow.ChildRowByIndex(0));
      this.treeChildTypes_SelectionChanged((object) this, (EventArgs) null);
    }
  }

  private void MarkChildObjectType(int objectTypeID)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (currentFilter == null)
      return;
    Guid currentParentType = this.GetCurrentParentType();
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeID);
    currentFilter.Add(currentParentType, objectType.Guid);
    if (objectType.VersionsMode != ObjectVersionModes.Abstract)
      return;
    foreach (Guid childrenType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(objectTypeID).Distinct<Guid>())
      currentFilter.Add(currentParentType, childrenType);
  }

  private void UnmarkChildObjectType(int objectTypeID)
  {
    ICompositionByObjectTypesFilter currentFilter = this.GetCurrentFilter();
    if (currentFilter == null)
      return;
    Guid currentParentType = this.GetCurrentParentType();
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeID);
    currentFilter.Remove(currentParentType, objectType.Guid);
    if (objectType.VersionsMode != ObjectVersionModes.Abstract)
      return;
    foreach (Guid childrenType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(objectTypeID).Distinct<Guid>())
      currentFilter.Remove(currentParentType, childrenType);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="filters">Текущие фильтры</param>
  /// <param name="support">Активный фильтр</param>
  private void Init(
    ICompositionByObjectTypesFilters filters,
    ICompositionByObjectTypesFiltration support)
  {
    for (int index = 0; index < this.listFilters.Cols.Count; ++index)
    {
      this.listFilters.Cols[index].SortOrder = iGSortOrder.None;
      this.listFilters.Cols[index].SortType = iGSortType.None;
    }
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this._filters = new CompositionByObjectTypesFilters();
    this._filters.Assign(filters);
    this._support = support;
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 60, primaryWorkingArea.Height / 100 * 70);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this._isChanged = false;
    this.listFilters.ImageList = this._images.ImageList;
    this.FillFiltersList();
    this.UpdateControls();
    if (!CompositionObjTypesFilterForm._isHintHidden)
      return;
    this.DoCloseHint((object) null, (EventArgs) null);
  }

  private ICompositionByObjectTypesFilter GetCurrentFilter()
  {
    iGRow curRow = this.listFilters.CurRow;
    return curRow == null ? (ICompositionByObjectTypesFilter) null : curRow.Tag as ICompositionByObjectTypesFilter;
  }

  private Guid GetCurrentParentType()
  {
    Row selectedRow = this.treeParentObjects.SelectedRow;
    return selectedRow == null || selectedRow.Item == null ? Guid.Empty : (Guid) selectedRow.Item;
  }

  private Guid GetCurrentChildrenType()
  {
    Row row = this.treeChildTypes.SelectedRow;
    while (row != null && !(row.Item is Guid))
      row = row.ParentRow;
    return row == null || row.Item == null ? Guid.Empty : (Guid) row.Item;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBar1.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarObjTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarParObjTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuChildTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuFilters.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuParentTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
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
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CompositionObjTypesFilterForm));
    this.panelsMain = new SplitContainer();
    this.panelsChild = new SplitContainer();
    this.listFilters = new iGrid();
    this.menuFilters = new MenuBar();
    this.imagesMenus = new ImageList(this.components);
    this.contextMenuBarFilters = new ContextMenuBarItem();
    this.mnpFilterAdd = new MenuButtonItem();
    this.mnpFilterDelete = new MenuButtonItem();
    this.mnpFilterUp = new MenuButtonItem();
    this.mnpFilterDown = new MenuButtonItem();
    this.mnpLoadFilters = new MenuButtonItem();
    this.mnpSaveFilters = new MenuButtonItem();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.btnFilterAdd = new ButtonItem();
    this.btnFilterDelete = new ButtonItem();
    this.btnFilterUp = new ButtonItem();
    this.btnFilterDown = new ButtonItem();
    this.btnLoadFilters = new ButtonItem();
    this.btnSaveFilters = new ButtonItem();
    this.treeParentObjects = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnParentObjects = new Column();
    this.menuParentTypes = new MenuBar();
    this.contextMenuBarParentTypes = new ContextMenuBarItem();
    this.mnpParentTypeAdd = new MenuButtonItem();
    this.mnpParentTypeDelete = new MenuButtonItem();
    this.mnpParentTypeUp = new MenuButtonItem();
    this.mnpParentTypeDown = new MenuButtonItem();
    this.mnpParentTypeRefresh = new MenuButtonItem();
    this.toolBarParObjTypes = new Intermech.Bars.ToolBar();
    this.btParentTypeAdd = new ButtonItem();
    this.btParentTypeDelete = new ButtonItem();
    this.btParentTypeUp = new ButtonItem();
    this.btParentTypeDown = new ButtonItem();
    this.btParentTypeRefresh = new ButtonItem();
    this.treeChildTypes = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnChildsCheck = new Column();
    this.cellEditor1 = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.columnHiddenChilds = new Column();
    this.menuChildTypes = new MenuBar();
    this.contextMenuChildTypes = new ContextMenuBarItem();
    this.mnpChildTypesCheckAll = new MenuButtonItem();
    this.mnpChildTypesUncheckAll = new MenuButtonItem();
    this.toolBarObjTypes = new Intermech.Bars.ToolBar();
    this.btChildrenTypesCheckAll = new ButtonItem();
    this.btChildrenTypesUncheckAll = new ButtonItem();
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.pictureHint = new PictureBox();
    this.pictureClose = new PictureBox();
    this.labelHint = new Label();
    this.tableLayoutPanel = new TableLayoutPanel();
    this.openFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.toolTip = new ToolTip(this.components);
    this.panelsMain.BeginInit();
    this.panelsMain.Panel1.SuspendLayout();
    this.panelsMain.Panel2.SuspendLayout();
    this.panelsMain.SuspendLayout();
    this.panelsChild.BeginInit();
    this.panelsChild.Panel1.SuspendLayout();
    this.panelsChild.Panel2.SuspendLayout();
    this.panelsChild.SuspendLayout();
    ((ISupportInitialize) this.listFilters).BeginInit();
    this.treeParentObjects.BeginInit();
    this.treeChildTypes.BeginInit();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.pictureHint).BeginInit();
    ((ISupportInitialize) this.pictureClose).BeginInit();
    this.tableLayoutPanel.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel.SetColumnSpan((Control) this.panelsMain, 3);
    componentResourceManager.ApplyResources((object) this.panelsMain, "panelsMain");
    this.panelsMain.Name = "panelsMain";
    this.panelsMain.Panel1.Controls.Add((Control) this.panelsChild);
    this.panelsMain.Panel2.Controls.Add((Control) this.treeChildTypes);
    this.panelsMain.Panel2.Controls.Add((Control) this.menuChildTypes);
    this.panelsMain.Panel2.Controls.Add((Control) this.toolBarObjTypes);
    componentResourceManager.ApplyResources((object) this.panelsChild, "panelsChild");
    this.panelsChild.FixedPanel = FixedPanel.Panel1;
    this.panelsChild.Name = "panelsChild";
    this.panelsChild.Panel1.Controls.Add((Control) this.listFilters);
    this.panelsChild.Panel1.Controls.Add((Control) this.menuFilters);
    this.panelsChild.Panel1.Controls.Add((Control) this.toolBar1);
    this.panelsChild.Panel2.Controls.Add((Control) this.treeParentObjects);
    this.panelsChild.Panel2.Controls.Add((Control) this.menuParentTypes);
    this.panelsChild.Panel2.Controls.Add((Control) this.toolBarParObjTypes);
    this.listFilters.AutoResizeCols = true;
    this.listFilters.BackColorEvenRows = Color.White;
    iGcolPattern1.AllowGrouping = false;
    iGcolPattern1.AllowMoving = false;
    iGcolPattern1.AllowSizing = false;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern1.SortOrder = iGSortOrder.None;
    iGcolPattern1.SortType = iGSortType.None;
    iGcolPattern2.AllowGrouping = false;
    iGcolPattern2.AllowMoving = false;
    iGcolPattern2.SortOrder = iGSortOrder.None;
    iGcolPattern2.SortType = iGSortType.None;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    this.listFilters.Cols.AddRange(new iGColPattern[2]
    {
      iGcolPattern1,
      iGcolPattern2
    });
    this.listFilters.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this.listFilters.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    componentResourceManager.ApplyResources((object) this.listFilters, "listFilters");
    this.listFilters.GridLines.Mode = iGGridLinesMode.None;
    this.listFilters.GroupBox.Text = componentResourceManager.GetString("listFilters.GroupBox.Text");
    this.listFilters.Header.Height = (int) componentResourceManager.GetObject("listFilters.Header.Height");
    this.listFilters.Name = "listFilters";
    this.listFilters.RowMode = true;
    this.listFilters.RowModeHasCurCell = true;
    this.listFilters.SelectInvisibleCells = true;
    this.listFilters.CurCellChanged += new EventHandler(this.listFilters_CurRowChanged);
    this.listFilters.RequestEdit += new iGRequestEditEventHandler(this.listFilters_RequestEdit);
    this.listFilters.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.listFilters_BeforeCommitEdit);
    this.listFilters.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.listFilters_AfterCommitEdit);
    this.listFilters.MouseDown += new MouseEventHandler(this.listFilters_MouseDown);
    this.listFilters.MouseUp += new MouseEventHandler(this.listFilters_MouseUp);
    componentResourceManager.ApplyResources((object) this.menuFilters, "menuFilters");
    this.menuFilters.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuFilters.Hidden = false;
    this.menuFilters.ImageList = this.imagesMenus;
    this.menuFilters.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarFilters
    });
    this.menuFilters.Name = "menuFilters";
    this.menuFilters.OwnerForm = (Form) null;
    this.imagesMenus.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesMenus.ImageStream");
    this.imagesMenus.TransparentColor = Color.Transparent;
    this.imagesMenus.Images.SetKeyName(0, "");
    this.imagesMenus.Images.SetKeyName(1, "");
    this.imagesMenus.Images.SetKeyName(2, "");
    this.imagesMenus.Images.SetKeyName(3, "");
    this.imagesMenus.Images.SetKeyName(4, "");
    this.imagesMenus.Images.SetKeyName(5, "");
    this.imagesMenus.Images.SetKeyName(6, "");
    this.imagesMenus.Images.SetKeyName(7, "");
    this.imagesMenus.Images.SetKeyName(8, "");
    this.imagesMenus.Images.SetKeyName(9, "");
    this.imagesMenus.Images.SetKeyName(10, "");
    this.imagesMenus.Images.SetKeyName(11, "");
    this.imagesMenus.Images.SetKeyName(12, "");
    this.imagesMenus.Images.SetKeyName(13, "");
    this.imagesMenus.Images.SetKeyName(14, "check_all.ico");
    this.imagesMenus.Images.SetKeyName(15, "uncheck_all.ico");
    componentResourceManager.ApplyResources((object) this.contextMenuBarFilters, "contextMenuBarFilters");
    this.contextMenuBarFilters.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpFilterAdd,
      (ToolbarItemBase) this.mnpFilterDelete,
      (ToolbarItemBase) this.mnpFilterUp,
      (ToolbarItemBase) this.mnpFilterDown,
      (ToolbarItemBase) this.mnpLoadFilters,
      (ToolbarItemBase) this.mnpSaveFilters
    });
    this.contextMenuBarFilters.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpFilterAdd, "mnpFilterAdd");
    this.mnpFilterAdd.ImageIndex = 0;
    this.mnpFilterAdd.ShowText = true;
    this.mnpFilterAdd.Click += new EventHandler(this.DoFilterAdd);
    componentResourceManager.ApplyResources((object) this.mnpFilterDelete, "mnpFilterDelete");
    this.mnpFilterDelete.ImageIndex = 4;
    this.mnpFilterDelete.ShowText = true;
    this.mnpFilterDelete.Click += new EventHandler(this.DoFilterDelete);
    this.mnpFilterUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpFilterUp, "mnpFilterUp");
    this.mnpFilterUp.ImageIndex = 2;
    this.mnpFilterUp.ShowText = true;
    this.mnpFilterUp.Click += new EventHandler(this.DoFilterUp);
    componentResourceManager.ApplyResources((object) this.mnpFilterDown, "mnpFilterDown");
    this.mnpFilterDown.ImageIndex = 3;
    this.mnpFilterDown.ShowText = true;
    this.mnpFilterDown.Click += new EventHandler(this.DoFilterDown);
    this.mnpLoadFilters.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpLoadFilters, "mnpLoadFilters");
    this.mnpLoadFilters.ImageIndex = 9;
    this.mnpLoadFilters.ShowText = true;
    this.mnpLoadFilters.Click += new EventHandler(this.DoLoadFilters);
    componentResourceManager.ApplyResources((object) this.mnpSaveFilters, "mnpSaveFilters");
    this.mnpSaveFilters.ImageIndex = 10;
    this.mnpSaveFilters.ShowText = true;
    this.mnpSaveFilters.Click += new EventHandler(this.DoSaveFilters);
    this.toolBar1.AddRemoveButtonsVisible = false;
    this.toolBar1.AllowHorizontalDock = false;
    this.toolBar1.Closable = false;
    this.toolBar1.DockLine = 3;
    this.toolBar1.DrawActionsButton = false;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.imagesMenus;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.btnFilterAdd,
      (ToolbarItemBase) this.btnFilterDelete,
      (ToolbarItemBase) this.btnFilterUp,
      (ToolbarItemBase) this.btnFilterDown,
      (ToolbarItemBase) this.btnLoadFilters,
      (ToolbarItemBase) this.btnSaveFilters
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.MinimumFloatingSize = new Size(250, 30);
    this.toolBar1.Movable = false;
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Overflow = ToolBarOverflow.Wrap;
    this.toolBar1.Stretch = true;
    this.toolBar1.Tearable = false;
    this.btnFilterAdd.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnFilterAdd, "btnFilterAdd");
    this.btnFilterAdd.ImageIndex = 0;
    this.btnFilterAdd.Click += new EventHandler(this.DoFilterAdd);
    componentResourceManager.ApplyResources((object) this.btnFilterDelete, "btnFilterDelete");
    this.btnFilterDelete.ImageIndex = 4;
    this.btnFilterDelete.Click += new EventHandler(this.DoFilterDelete);
    this.btnFilterUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnFilterUp, "btnFilterUp");
    this.btnFilterUp.ImageIndex = 2;
    this.btnFilterUp.Click += new EventHandler(this.DoFilterUp);
    componentResourceManager.ApplyResources((object) this.btnFilterDown, "btnFilterDown");
    this.btnFilterDown.ImageIndex = 3;
    this.btnFilterDown.Click += new EventHandler(this.DoFilterDown);
    this.btnLoadFilters.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnLoadFilters, "btnLoadFilters");
    this.btnLoadFilters.ImageIndex = 9;
    this.btnLoadFilters.Click += new EventHandler(this.DoLoadFilters);
    componentResourceManager.ApplyResources((object) this.btnSaveFilters, "btnSaveFilters");
    this.btnSaveFilters.ImageIndex = 10;
    this.btnSaveFilters.Click += new EventHandler(this.DoSaveFilters);
    this.treeParentObjects.AllowDrop = true;
    this.treeParentObjects.AllowIndividualRowResize = false;
    this.treeParentObjects.AllowMultiSelect = false;
    this.treeParentObjects.AllowRowResize = false;
    this.treeParentObjects.AllowUserPinnedColumns = false;
    this.treeParentObjects.AutoFitColumns = true;
    this.treeParentObjects.Columns.Add(this.columnParentObjects);
    this.treeParentObjects.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeParentObjects, "treeParentObjects");
    this.treeParentObjects.ImageList = (ImageList) null;
    this.treeParentObjects.LineStyle = LineStyle.Dot;
    this.treeParentObjects.MainColumn = this.columnParentObjects;
    this.treeParentObjects.Name = "treeParentObjects";
    this.treeParentObjects.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeParentObjects.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeParentObjects.SelectBeforeEdit = true;
    this.treeParentObjects.ShowRootRow = false;
    this.treeParentObjects.SuppressErrorMessages = true;
    this.treeParentObjects.ShowContextMenu += new MouseEventHandler(this.treeParentObjects_ShowContextMenu);
    this.treeParentObjects.FocusRowChanged += new EventHandler(this.treeParentObjects_FocusRowChanged);
    this.treeParentObjects.GetCellData += new GetCellDataHandler(this.treeParentObjects_GetCellData);
    this.treeParentObjects.GetChildPolicy += new GetChildPolicyHandler(this.treeParentObjects_GetChildPolicy);
    this.treeParentObjects.GetChildren += new GetChildrenHandler(this.treeParentObjects_GetChildren);
    this.treeParentObjects.GetRowData += new GetRowDataHandler(this.treeParentObjects_GetRowData);
    this.treeParentObjects.SelectionChanged += new EventHandler(this.treeParentObjects_SelectionChanged);
    this.columnParentObjects.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
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
      (ToolbarItemBase) this.contextMenuBarParentTypes
    });
    this.menuParentTypes.Name = "menuParentTypes";
    this.menuParentTypes.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuBarParentTypes, "contextMenuBarParentTypes");
    this.contextMenuBarParentTypes.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.mnpParentTypeAdd,
      (ToolbarItemBase) this.mnpParentTypeDelete,
      (ToolbarItemBase) this.mnpParentTypeUp,
      (ToolbarItemBase) this.mnpParentTypeDown,
      (ToolbarItemBase) this.mnpParentTypeRefresh
    });
    this.contextMenuBarParentTypes.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpParentTypeAdd, "mnpParentTypeAdd");
    this.mnpParentTypeAdd.ImageIndex = 6;
    this.mnpParentTypeAdd.ShowText = true;
    this.mnpParentTypeAdd.Click += new EventHandler(this.DoObjTypeAdd);
    componentResourceManager.ApplyResources((object) this.mnpParentTypeDelete, "mnpParentTypeDelete");
    this.mnpParentTypeDelete.ImageIndex = 7;
    this.mnpParentTypeDelete.ShowText = true;
    this.mnpParentTypeDelete.Click += new EventHandler(this.DoObjTypeDelete);
    this.mnpParentTypeUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpParentTypeUp, "mnpParentTypeUp");
    this.mnpParentTypeUp.ImageIndex = 2;
    this.mnpParentTypeUp.ShowText = true;
    this.mnpParentTypeUp.Click += new EventHandler(this.DoParentObjectTypeUp);
    componentResourceManager.ApplyResources((object) this.mnpParentTypeDown, "mnpParentTypeDown");
    this.mnpParentTypeDown.ImageIndex = 3;
    this.mnpParentTypeDown.ShowText = true;
    this.mnpParentTypeDown.Click += new EventHandler(this.DoParentObjectTypeDown);
    this.mnpParentTypeRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpParentTypeRefresh, "mnpParentTypeRefresh");
    this.mnpParentTypeRefresh.ImageIndex = 8;
    this.mnpParentTypeRefresh.ShowText = true;
    this.mnpParentTypeRefresh.Click += new EventHandler(this.DoObjectTypeRefresh);
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
      (ToolbarItemBase) this.btParentTypeAdd,
      (ToolbarItemBase) this.btParentTypeDelete,
      (ToolbarItemBase) this.btParentTypeUp,
      (ToolbarItemBase) this.btParentTypeDown,
      (ToolbarItemBase) this.btParentTypeRefresh
    });
    componentResourceManager.ApplyResources((object) this.toolBarParObjTypes, "toolBarParObjTypes");
    this.toolBarParObjTypes.MinimumFloatingSize = new Size(250, 30);
    this.toolBarParObjTypes.Movable = false;
    this.toolBarParObjTypes.Name = "toolBarParObjTypes";
    this.toolBarParObjTypes.Overflow = ToolBarOverflow.Wrap;
    this.toolBarParObjTypes.Stretch = true;
    this.toolBarParObjTypes.Tearable = false;
    componentResourceManager.ApplyResources((object) this.btParentTypeAdd, "btParentTypeAdd");
    this.btParentTypeAdd.ImageIndex = 6;
    this.btParentTypeAdd.Click += new EventHandler(this.DoObjTypeAdd);
    componentResourceManager.ApplyResources((object) this.btParentTypeDelete, "btParentTypeDelete");
    this.btParentTypeDelete.ImageIndex = 7;
    this.btParentTypeDelete.Click += new EventHandler(this.DoObjTypeDelete);
    this.btParentTypeUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btParentTypeUp, "btParentTypeUp");
    this.btParentTypeUp.ImageIndex = 2;
    this.btParentTypeUp.Click += new EventHandler(this.DoParentObjectTypeUp);
    componentResourceManager.ApplyResources((object) this.btParentTypeDown, "btParentTypeDown");
    this.btParentTypeDown.ImageIndex = 3;
    this.btParentTypeDown.Click += new EventHandler(this.DoParentObjectTypeDown);
    this.btParentTypeRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btParentTypeRefresh, "btParentTypeRefresh");
    this.btParentTypeRefresh.ImageIndex = 8;
    this.btParentTypeRefresh.Click += new EventHandler(this.DoObjectTypeRefresh);
    this.treeChildTypes.AllowDrop = true;
    this.treeChildTypes.AllowIndividualRowResize = false;
    this.treeChildTypes.AllowRowResize = false;
    this.treeChildTypes.AllowUserPinnedColumns = false;
    this.treeChildTypes.AutoFitColumns = true;
    this.treeChildTypes.Columns.Add(this.columnChildsCheck);
    this.treeChildTypes.Columns.Add(this.columnHiddenChilds);
    this.treeChildTypes.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeChildTypes, "treeChildTypes");
    this.treeChildTypes.Editors.Add(this.cellEditor1);
    this.treeChildTypes.ImageList = (ImageList) null;
    this.treeChildTypes.LineStyle = LineStyle.Dot;
    this.treeChildTypes.MainColumn = this.columnHiddenChilds;
    this.treeChildTypes.Name = "treeChildTypes";
    this.treeChildTypes.PrefixColumn = this.columnChildsCheck;
    this.treeChildTypes.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeChildTypes.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeChildTypes.SelectBeforeEdit = true;
    this.treeChildTypes.ShowRootRow = false;
    this.treeChildTypes.SuppressErrorMessages = true;
    this.treeChildTypes.ShowContextMenu += new MouseEventHandler(this.treeChildTypes_ShowContextMenu);
    this.treeChildTypes.FocusRowChanged += new EventHandler(this.treeChildTypes_FocusRowChanged);
    this.treeChildTypes.GetCellData += new GetCellDataHandler(this.treeChildTypes_GetCellData);
    this.treeChildTypes.GetChildren += new GetChildrenHandler(this.treeChildTypes_GetChildren);
    this.treeChildTypes.GetRowData += new GetRowDataHandler(this.treeChildTypes_GetRowData);
    this.treeChildTypes.SelectionChanged += new EventHandler(this.treeChildTypes_SelectionChanged);
    this.treeChildTypes.SetCellValue += new SetCellValueHandler(this.treeChildTypes_SetCellValue);
    componentResourceManager.ApplyResources((object) this.columnChildsCheck, "columnChildsCheck");
    this.columnChildsCheck.CellEditor = this.cellEditor1;
    this.columnChildsCheck.Movable = false;
    this.columnChildsCheck.Name = "columnChildsCheck";
    this.columnChildsCheck.Resizable = false;
    this.columnChildsCheck.Sortable = false;
    this.cellEditor1.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditor1.Control = (Control) this.checkBox1;
    this.cellEditor1.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditor1.UseCellHeight = false;
    this.cellEditor1.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    componentResourceManager.ApplyResources((object) this.columnHiddenChilds, "columnHiddenChilds");
    this.columnHiddenChilds.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnHiddenChilds.HeaderStyle.HorzAlignment");
    this.columnHiddenChilds.Movable = false;
    this.columnHiddenChilds.Name = "columnHiddenChilds";
    this.columnHiddenChilds.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menuChildTypes, "menuChildTypes");
    this.menuChildTypes.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuChildTypes.Hidden = false;
    this.menuChildTypes.ImageList = this.imagesMenus;
    this.menuChildTypes.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuChildTypes
    });
    this.menuChildTypes.Name = "menuChildTypes";
    this.menuChildTypes.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuChildTypes, "contextMenuChildTypes");
    this.contextMenuChildTypes.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.mnpChildTypesCheckAll,
      (ToolbarItemBase) this.mnpChildTypesUncheckAll
    });
    this.contextMenuChildTypes.ShowText = true;
    this.mnpChildTypesCheckAll.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpChildTypesCheckAll, "mnpChildTypesCheckAll");
    this.mnpChildTypesCheckAll.ImageIndex = 14;
    this.mnpChildTypesCheckAll.ShowText = true;
    this.mnpChildTypesCheckAll.Click += new EventHandler(this.DoCheckAllChildTypes);
    componentResourceManager.ApplyResources((object) this.mnpChildTypesUncheckAll, "mnpChildTypesUncheckAll");
    this.mnpChildTypesUncheckAll.ImageIndex = 15;
    this.mnpChildTypesUncheckAll.ShowText = true;
    this.mnpChildTypesUncheckAll.Click += new EventHandler(this.DoUncheckAllChildTypes);
    this.toolBarObjTypes.AddRemoveButtonsVisible = false;
    this.toolBarObjTypes.AllowHorizontalDock = false;
    this.toolBarObjTypes.Closable = false;
    this.toolBarObjTypes.DockLine = 3;
    this.toolBarObjTypes.DrawActionsButton = false;
    this.toolBarObjTypes.FullMenus = true;
    this.toolBarObjTypes.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarObjTypes.Hidden = false;
    this.toolBarObjTypes.ImageList = this.imagesMenus;
    this.toolBarObjTypes.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btChildrenTypesCheckAll,
      (ToolbarItemBase) this.btChildrenTypesUncheckAll
    });
    componentResourceManager.ApplyResources((object) this.toolBarObjTypes, "toolBarObjTypes");
    this.toolBarObjTypes.MinimumFloatingSize = new Size(250, 30);
    this.toolBarObjTypes.Movable = false;
    this.toolBarObjTypes.Name = "toolBarObjTypes";
    this.toolBarObjTypes.Overflow = ToolBarOverflow.Wrap;
    this.toolBarObjTypes.Stretch = true;
    this.toolBarObjTypes.Tearable = false;
    componentResourceManager.ApplyResources((object) this.btChildrenTypesCheckAll, "btChildrenTypesCheckAll");
    this.btChildrenTypesCheckAll.ImageIndex = 14;
    this.btChildrenTypesCheckAll.Click += new EventHandler(this.DoCheckAllChildTypes);
    componentResourceManager.ApplyResources((object) this.btChildrenTypesUncheckAll, "btChildrenTypesUncheckAll");
    this.btChildrenTypesUncheckAll.ImageIndex = 15;
    this.btChildrenTypesUncheckAll.Click += new EventHandler(this.DoUncheckAllChildTypes);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Cursor = Cursors.Default;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    componentResourceManager.ApplyResources((object) this.pictureHint, "pictureHint");
    this.pictureHint.Name = "pictureHint";
    this.pictureHint.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureClose, "pictureClose");
    this.pictureClose.Name = "pictureClose";
    this.pictureClose.TabStop = false;
    this.toolTip.SetToolTip((Control) this.pictureClose, componentResourceManager.GetString("pictureClose.ToolTip"));
    this.pictureClose.Click += new EventHandler(this.DoCloseHint);
    componentResourceManager.ApplyResources((object) this.labelHint, "labelHint");
    this.labelHint.Name = "labelHint";
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
    this.tableLayoutPanel.Controls.Add((Control) this.labelHint, 1, 0);
    this.tableLayoutPanel.Controls.Add((Control) this.pictureClose, 2, 0);
    this.tableLayoutPanel.Controls.Add((Control) this.pictureHint, 0, 0);
    this.tableLayoutPanel.Controls.Add((Control) this.panelsMain, 0, 1);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    this.openFileDialog.DefaultExt = "filters";
    componentResourceManager.ApplyResources((object) this.openFileDialog, "openFileDialog");
    this.openFileDialog.ShowReadOnly = true;
    this.openFileDialog.SupportMultiDottedExtensions = true;
    this.openFileDialog.RestoreDirectory = true;
    this.saveFileDialog.DefaultExt = "filters";
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.saveFileDialog.SupportMultiDottedExtensions = true;
    this.saveFileDialog.RestoreDirectory = true;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tableLayoutPanel);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CompositionObjTypesFilterForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.CompositionObjTypesFilterForm_FormClosed);
    this.Load += new EventHandler(this.CompositionObjTypesFilterForm_Load);
    this.panelsMain.Panel1.ResumeLayout(false);
    this.panelsMain.Panel2.ResumeLayout(false);
    this.panelsMain.EndInit();
    this.panelsMain.ResumeLayout(false);
    this.panelsChild.Panel1.ResumeLayout(false);
    this.panelsChild.Panel2.ResumeLayout(false);
    this.panelsChild.EndInit();
    this.panelsChild.ResumeLayout(false);
    ((ISupportInitialize) this.listFilters).EndInit();
    this.treeParentObjects.EndInit();
    this.treeChildTypes.EndInit();
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.pictureHint).EndInit();
    ((ISupportInitialize) this.pictureClose).EndInit();
    this.tableLayoutPanel.ResumeLayout(false);
    this.tableLayoutPanel.PerformLayout();
    this.ResumeLayout(false);
  }
}
