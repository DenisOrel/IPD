
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Search.iGrid;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryControl : 
  UserControl,
  ISupportInitialize,
  INavigatorContextSearch
{
  private const string AttributeColumnKey = "Attribute";
  private const string ObjectTypeRelationTypeColumnKey = "ObjectTypeRelationType";
  private const string ObjectRelationColumKey = "ObjectRelation";
  private const string ObjectIDRelationIDColumnKey = "ObjectIDRelationID";
  private const string DateColumnKey = "Date";
  private const string ValueColumnKey = "Value";
  private const string UserColumnKey = "User";
  private List<int> _attributeTypeIds = new List<int>();
  private List<int> _objectTypeIds = new List<int>();
  private List<int> _relationTypeIds = new List<int>();
  private List<long> _userVersionIds = new List<long>();
  private List<long> _objectVersionIds = new List<long>();
  private bool _preventReload;
  private List<AttributeChangeHistoryRecord> _records = new List<AttributeChangeHistoryRecord>();
  private List<AttributeChangeHistoryRecord> _selectedRecords = new List<AttributeChangeHistoryRecord>();
  private bool _canLoadMore = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip toolStrip1;
  private ToolStripComboBox _attributesToolStripComboBox;
  private ToolStripButton _applyAttributesFilterToolStripButton;
  private TenTec.Windows.iGridLib.iGrid _grid;
  private iGCellStyle _gridCol0CellStyle;
  private iGColHdrStyle _gridCol0ColHdrStyle;
  private iGCellStyle _gridCol1CellStyle;
  private iGColHdrStyle _gridCol1ColHdrStyle;
  private iGCellStyle _gridCol2CellStyle;
  private iGColHdrStyle _gridCol2ColHdrStyle;
  private iGCellStyle _gridCol3CellStyle;
  private iGColHdrStyle _gridCol3ColHdrStyle;
  private iGCellStyle _gridCol4CellStyle;
  private iGColHdrStyle _gridCol4ColHdrStyle;
  private iGCellStyle _gridCol5CellStyle;
  private iGColHdrStyle _gridCol5ColHdrStyle;
  private iGCellStyle _gridCol7CellStyle;
  private iGColHdrStyle _gridCol7ColHdrStyle;
  private ToolStripLabel toolStripLabel1;
  private ToolStripButton _addAttributesToolStripButton;
  private ToolStripButton _removeAttributeToolStripButton;
  private ToolStripLabel toolStripLabel2;
  private ToolStripComboBox _objectTypesToolStripComboBox;
  private ToolStripButton _applyObjectTypesFilterToolStripButton;
  private ToolStripButton _addObjectTypesToolStripButton;
  private ToolStripButton _removeObjectTypeToolStripButton;
  private ToolStripLabel toolStripLabel3;
  private ToolStripComboBox _relationTypesToolStripComboBox;
  private ToolStripButton _applyRelationTypesFilterToolStripButton;
  private ToolStripButton _addRelationTypesToolStripButton;
  private ToolStripButton _removeRelationTypeToolStripButton;
  private ToolStripLabel toolStripLabel4;
  private ToolStripComboBox _usersToolStripComboBox;
  private ToolStripButton _applyUsersFilterToolStripButton;
  private ToolStripButton _addUsersToolStripButton;
  private ToolStripButton _removeUserToolStripButton;
  private ToolStripLabel toolStripLabel5;
  private ToolStripDateTimePicker _fromToolStripDateTimePicker;
  private ToolStripLabel toolStripLabel6;
  private ToolStripDateTimePicker _toToolStripDateTimePicker;
  private ToolStripLabel toolStripLabel7;
  private ToolStripComboBox _objectsToolStripComboBox;
  private ToolStripButton _applyObjectsFilterToolStripButton;
  private ToolStripButton _addObjectsToolStripButton;
  private ToolStripButton _removeObjectToolStripButton;
  private ToolStripButton _clearAttributesToolStripButton;
  private ToolStripButton _clearObjectTypesToolStripButton;
  private ToolStripButton _clearRelationTypesToolStripButton;
  private ToolStripButton _clearUsersToolStripButton;
  private ToolStripButton _clearObjectsToolStripButton;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel toolStripStatusLabel1;
  private ToolStripStatusLabel _recordCountToolStripStatusLabel;
  private ContextMenuStrip _contextMenuStrip;
  private ToolStripMenuItem _showCardToolStripMenuItem;
  private ToolStripMenuItem _openInNewWindowToolStripMenuItem;
  private ToolStripMenuItem _showVersionsTreeToolStripMenuItem;
  private ToolStripMenuItem _findToolStripMenuItem;
  private ToolStripMenuItem _copyTextToolStripMenuItem;
  private ToolStripStatusLabel toolStripStatusLabel2;
  private ToolStripDropDownButton _loadMoreToolStripDropDownButton;
  private ToolStripMenuItem _reloadToolStripMenuItem;
  private Panel panel1;
  private ToolStrip toolStrip6;
  private ToolStrip toolStrip2;
  private ToolStrip toolStrip3;
  private ToolStrip toolStrip4;
  private ToolStrip toolStrip5;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;

  public AttributeChangeHistoryControl()
  {
    this.InitializeComponent();
    this._fromToolStripDateTimePicker.DateTimePicker.ShowCheckBox = true;
    this._fromToolStripDateTimePicker.DateTimePicker.Checked = false;
    this._fromToolStripDateTimePicker.DateTimePicker.ValueChanged += new EventHandler(this.FromDateTimePicker_ValueChanged);
    this._toToolStripDateTimePicker.DateTimePicker.ShowCheckBox = true;
    this._toToolStripDateTimePicker.DateTimePicker.Checked = false;
    this._toToolStripDateTimePicker.DateTimePicker.ValueChanged += new EventHandler(this.ToDateTimePicker_ValueChanged);
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long[] ObjectVersionIds
  {
    get => this._objectVersionIds.ToArray();
    set
    {
      this._objectVersionIds = value != null ? ((IEnumerable<long>) value).ToList<long>() : new List<long>();
      this._applyObjectsFilterToolStripButton.Checked = true;
      this.FillObjectsToolStripComboBox();
    }
  }

  public AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento GetMemento()
  {
    return new AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento()
    {
      AttributeTypeIds = this._attributeTypeIds.ToArray(),
      From = this._fromToolStripDateTimePicker.DateTimePicker.Checked ? this._fromToolStripDateTimePicker.DateTimePicker.Value : DateTime.MinValue,
      ObjectTypeIds = this._objectTypeIds.ToArray(),
      RelationTypeIds = this._relationTypeIds.ToArray(),
      To = this._toToolStripDateTimePicker.DateTimePicker.Checked ? this._toToolStripDateTimePicker.DateTimePicker.Value : DateTime.MinValue,
      UserVersionIds = this._userVersionIds.ToArray(),
      Columns = this._grid.Cols.Cast<iGCol>().OrderBy<iGCol, int>((System.Func<iGCol, int>) (o => o.Order)).Select<iGCol, Tuple<string, int>>((System.Func<iGCol, Tuple<string, int>>) (o => new Tuple<string, int>(o.Key, o.Width))).ToArray<Tuple<string, int>>(),
      ColumnsGroupObject = this.GetGridGroupObject(),
      ColumnsSortObject = this.GetGridSortObject()
    };
  }

  public void SetMemento(
    AttributeChangeHistoryControl.AttributeChangeHistoryControlMemento memento)
  {
    if (memento == null)
      throw new ArgumentNullException(nameof (memento));
    this._preventReload = true;
    try
    {
      this._attributeTypeIds.Clear();
      if (memento.AttributeTypeIds != null)
        this._attributeTypeIds.AddRange((IEnumerable<int>) memento.AttributeTypeIds);
      this.FillAttributesToolStripComboBox();
      this._objectTypeIds.Clear();
      if (memento.ObjectTypeIds != null)
        this._objectTypeIds.AddRange((IEnumerable<int>) memento.ObjectTypeIds);
      this.FillObjectTypesToolStripComboBox();
      this._relationTypeIds.Clear();
      if (memento.RelationTypeIds != null)
        this._relationTypeIds.AddRange((IEnumerable<int>) memento.RelationTypeIds);
      this.FillRelationTypesToolStripComboBox();
      this._userVersionIds.Clear();
      if (memento.UserVersionIds != null)
        this._userVersionIds.AddRange((IEnumerable<long>) memento.UserVersionIds);
      this.FillUsersToolStripComboBox();
      if (memento.From != DateTime.MinValue)
      {
        this._fromToolStripDateTimePicker.DateTimePicker.Value = memento.From;
        this._fromToolStripDateTimePicker.DateTimePicker.Checked = false;
      }
      if (memento.To != DateTime.MinValue)
      {
        this._toToolStripDateTimePicker.DateTimePicker.Value = memento.To;
        this._toToolStripDateTimePicker.DateTimePicker.Checked = false;
      }
      if (memento.Columns != null)
      {
        for (int index = 0; index < memento.Columns.Length; ++index)
        {
          Tuple<string, int> column = memento.Columns[index];
          iGCol col = this._grid.Cols[column.Item1];
          col.Order = index;
          col.Width = column.Item2;
        }
      }
      if (memento.ColumnsGroupObject != null)
      {
        this._grid.GroupObject.Clear();
        foreach (Tuple<string, iGSortOrder> tuple in memento.ColumnsGroupObject)
          this._grid.GroupObject.Add(tuple.Item1, tuple.Item2);
        this._grid.Group();
      }
      if (memento.ColumnsSortObject == null)
        return;
      this._grid.SortObject.Clear();
      foreach (Tuple<string, iGSortOrder> tuple in memento.ColumnsSortObject)
        this._grid.SortObject.Add(tuple.Item1, tuple.Item2);
      this._grid.Sort();
    }
    finally
    {
      this._preventReload = false;
      this.Reload();
      this.UpdateControls();
    }
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
    this._applyAttributesFilterToolStripButton.Image = categoryTypeIconService.ImageList.Images[categoryTypeIconService.IndexOf(3, 0)];
    this._applyObjectsFilterToolStripButton.Image = this._applyObjectTypesFilterToolStripButton.Image = categoryTypeIconService.ImageList.Images[categoryTypeIconService.IndexOf(4, 0)];
    this._applyRelationTypesFilterToolStripButton.Image = categoryTypeIconService.ImageList.Images[categoryTypeIconService.IndexOf(6, 0)];
    this._applyUsersFilterToolStripButton.Image = categoryTypeIconService.ImageList.Images[categoryTypeIconService.IndexOf(4, Constants.UserObjectTypeID)];
    IFactory factory = ServiceLocator.Get<IFactory>();
    this.InitializeToolStripMenuItem(this._reloadToolStripMenuItem, factory.ContextMenuTemplate["Refresh"]);
    this.InitializeToolStripMenuItem(this._copyTextToolStripMenuItem, factory.ContextMenuTemplate["CopyText"]);
    this.InitializeToolStripMenuItem(this._openInNewWindowToolStripMenuItem, factory.ContextMenuTemplate["OpenInNewWindow"]);
    this.InitializeToolStripMenuItem(this._showVersionsTreeToolStripMenuItem, factory.ContextMenuTemplate["ListVersions"]);
    this.InitializeToolStripMenuItem(this._showCardToolStripMenuItem, factory.ContextMenuTemplate["ParametersCard"]);
    this.InitializeToolStripMenuItem(this._findToolStripMenuItem, factory.ContextMenuTemplate["NavigatorContextSearch"]);
    this.Reload();
  }

  public event EventHandler CurrentColumnChanged;

  string INavigatorContextSearch.CurrentColumnText
  {
    get => iGridExtensions.GetCurrentColumnText(this._grid);
  }

  IEnumerable<Tuple<int, int, string>> INavigatorContextSearch.GetCellValues(
    bool currentColumnOnly,
    bool fromBeggining,
    bool backward)
  {
    return iGridExtensions.GetCellValues(this._grid, currentColumnOnly, fromBeggining, backward);
  }

  void INavigatorContextSearch.SelectCells(Tuple<int, int>[] cells)
  {
    iGridExtensions.SelectCells(this._grid, cells);
  }

  private void AttributesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ApplyAttributesFilterToolStripButton_Click(object sender, EventArgs e)
  {
    this._applyAttributesFilterToolStripButton.Checked = !this._applyAttributesFilterToolStripButton.Checked;
    this.Reload();
  }

  private void AddAttributesToolStripButton_Click(object sender, EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID == null || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      foreach (int num in attributesSelectDlg.SelectedAttributesID)
      {
        if (!this._attributeTypeIds.Contains(num))
          this._attributeTypeIds.Add(num);
      }
      this.FillAttributesToolStripComboBox(attributesSelectDlg.SelectedAttributesID.Last<int>());
      if (this._applyAttributesFilterToolStripButton.Checked)
        this.Reload();
      this.UpdateControls();
    }
  }

  private void RemoveAttributeToolStripButton_Click(object sender, EventArgs e)
  {
    int removingAttributeTypeId = this.GetMustBeSelectedAfterRemovingAttributeTypeID();
    foreach (int selectedAttributeTypeId in this.GetSelectedAttributeTypeIds())
      this._attributeTypeIds.Remove(selectedAttributeTypeId);
    this.FillAttributesToolStripComboBox(removingAttributeTypeId);
    if (this._applyAttributesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ClearAttributesToolStripButton_Click(object sender, EventArgs e)
  {
    this._attributeTypeIds.Clear();
    this.FillAttributesToolStripComboBox();
    if (this._applyAttributesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ObjectTypesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ApplyObjectTypesFilterToolStripButton_Click(object sender, EventArgs e)
  {
    this._applyObjectTypesFilterToolStripButton.Checked = !this._applyObjectTypesFilterToolStripButton.Checked;
    this.Reload();
  }

  private void AddObjectTypesToolStripButton_Click(object sender, EventArgs e)
  {
    int[] source = ObjectTypeClientHelper.SelectObjectTypes("Выбор типов объектов");
    if (source.Length == 0)
      return;
    foreach (int num in source)
    {
      if (!this._objectTypeIds.Contains(num))
        this._objectTypeIds.Add(num);
    }
    this.FillObjectTypesToolStripComboBox(((IEnumerable<int>) source).Last<int>());
    if (this._applyObjectTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void RemoveObjectTypeToolStripButton_Click(object sender, EventArgs e)
  {
    int removingObjectTypeId = this.GetMustBeSelectedAfterRemovingObjectTypeID();
    foreach (int selectedObjectTypeId in this.GetSelectedObjectTypeIds())
      this._objectTypeIds.Remove(selectedObjectTypeId);
    this.FillObjectTypesToolStripComboBox(removingObjectTypeId);
    if (this._applyObjectTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ClearObjectTypesToolStripButton_Click(object sender, EventArgs e)
  {
    this._objectTypeIds.Clear();
    this.FillObjectTypesToolStripComboBox();
    if (this._applyObjectTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void RelationTypesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ApplyRelationTypesFilterToolStripButton_Click(object sender, EventArgs e)
  {
    this._applyRelationTypesFilterToolStripButton.Checked = !this._applyRelationTypesFilterToolStripButton.Checked;
    this.Reload();
  }

  private void AddRelationTypesToolStripButton_Click(object sender, EventArgs e)
  {
    int[] source = RelationTypeClientHelper.SelectRelationTypes("Выбор типов связей");
    if (source.Length == 0)
      return;
    foreach (int num in source)
    {
      if (!this._relationTypeIds.Contains(num))
        this._relationTypeIds.Add(num);
    }
    this.FillRelationTypesToolStripComboBox(((IEnumerable<int>) source).Last<int>());
    if (this._applyRelationTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void RemoveRelationTypeToolStripButton_Click(object sender, EventArgs e)
  {
    int removingRelationTypeId = this.GetMustBeSelectedAfterRemovingRelationTypeID();
    foreach (int selectedRelationTypeId in this.GetSelectedRelationTypeIds())
      this._relationTypeIds.Remove(selectedRelationTypeId);
    this.FillRelationTypesToolStripComboBox(removingRelationTypeId);
    if (this._applyRelationTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ClearRelationTypesToolStripButton_Click(object sender, EventArgs e)
  {
    this._relationTypeIds.Clear();
    this.FillRelationTypesToolStripComboBox();
    if (this._applyRelationTypesFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void UsersToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ApplyUsersFilterToolStripButton_Click(object sender, EventArgs e)
  {
    this._applyUsersFilterToolStripButton.Checked = !this._applyUsersFilterToolStripButton.Checked;
    this.Reload();
  }

  private void AddUsersToolStripButton_Click(object sender, EventArgs e)
  {
    long[] source = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор пользователей или групп", "Выберите пользователей или группы, чьи правки нужно отобразить.", (IDescriptor) new UsersGroupsDescriptor(), (System.IServiceProvider) ServicesManager.ServiceContainer, SelectionOptions.SelectObjects);
    if (source == null || source.Length == 0)
      return;
    foreach (long num in source)
    {
      if (!this._userVersionIds.Contains(num))
        this._userVersionIds.Add(num);
    }
    this.FillUsersToolStripComboBox(((IEnumerable<long>) source).Last<long>());
    if (this._applyUsersFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void RemoveUserToolStripButton_Click(object sender, EventArgs e)
  {
    long removingUserVersionId = this.GetMustBeSelectedAfterRemovingUserVersionID();
    foreach (long selectedUserVersionId in this.GetSelectedUserVersionIds())
      this._userVersionIds.Remove(selectedUserVersionId);
    this.FillUsersToolStripComboBox(removingUserVersionId);
    if (this._applyUsersFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ClearUsersToolStripButton_Click(object sender, EventArgs e)
  {
    this._userVersionIds.Clear();
    this.FillUsersToolStripComboBox();
    if (this._applyUsersFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void FromDateTimePicker_ValueChanged(object sender, EventArgs e) => this.Reload();

  private void ToDateTimePicker_ValueChanged(object sender, EventArgs e) => this.Reload();

  private void ObjectsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ApplyObjectsFilterToolStripButton_Click(object sender, EventArgs e)
  {
    this._applyObjectsFilterToolStripButton.Checked = !this._applyObjectsFilterToolStripButton.Checked;
    this.Reload();
  }

  private void AddObjectsToolStripButton_Click(object sender, EventArgs e)
  {
    long[] source = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор объектов", "Выберите объекты, правки которых нужно отобразить.", (IDescriptor) new ObjectTypesNodeDescriptor(), (System.IServiceProvider) ServicesManager.ServiceContainer, SelectionOptions.SelectObjects);
    if (source == null || source.Length == 0)
      return;
    foreach (long num in source)
    {
      if (!this._objectVersionIds.Contains(num))
        this._objectVersionIds.Add(num);
      this.FillObjectsToolStripComboBox(((IEnumerable<long>) source).Last<long>());
      if (this._applyObjectsFilterToolStripButton.Checked)
        this.Reload();
      this.UpdateControls();
    }
  }

  private void RemoveObjectToolStripButton_Click(object sender, EventArgs e)
  {
    long removingObjectVersionId = this.GetMustBeSelectedAfterRemovingObjectVersionID();
    foreach (long selectedObjectVersionId in this.GetSelectedObjectVersionIds())
      this._objectVersionIds.Remove(selectedObjectVersionId);
    this.FillObjectsToolStripComboBox(removingObjectVersionId);
    if (this._applyObjectsFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void ClearObjectsToolStripButton_Click(object sender, EventArgs e)
  {
    this._objectVersionIds.Clear();
    this.FillObjectsToolStripComboBox();
    if (this._applyObjectsFilterToolStripButton.Checked)
      this.Reload();
    this.UpdateControls();
  }

  private void Grid_AfterContentsGrouped(object sender, EventArgs e) => this.Reload();

  private void Grid_AfterContentsSorted(object sender, EventArgs e) => this.Reload();

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    this._selectedRecords.Clear();
    this._selectedRecords.AddRange((IEnumerable<AttributeChangeHistoryRecord>) this.GetSelectedRecords());
    this.UpdateControls();
  }

  private void ReloadToolStripMenuItem_Click(object sender, EventArgs e) => this.Reload();

  private void CopyTextToolStripMenuItem_Click(object sender, EventArgs e)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
    {
      iGRow row = this._grid.Rows[this._grid.SelectedCells[index].RowIndex];
      for (int colIndex = 0; colIndex < row.Cells.Count; ++colIndex)
      {
        stringBuilder.Append(row.Cells[colIndex].Text ?? string.Empty);
        if (colIndex < row.Cells.Count - 1)
          stringBuilder.Append("\t");
      }
      if (index < this._grid.SelectedCells.Count - 1)
        stringBuilder.Append(Environment.NewLine);
    }
    Clipboard.SetText(stringBuilder.ToString());
  }

  private void OpenInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
  {
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(this.SelectOneObjectVersionID(this._selectedRecords[0])), (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  private void ShowVersionsTreeToolStripMenuItem_Click(object sender, EventArgs e)
  {
    ObjectCommands.ListVersions(SelectedItemsHelper.CreateSelectedItemsForObject(this._selectedRecords[0].ObjectVersionIds[0]), (System.IServiceProvider) ServicesManager.ServiceContainer, (object) null);
  }

  private void ShowCardToolStripMenuItem_Click(object sender, EventArgs e)
  {
    long ObjectID = this.SelectOneObjectVersionID(this._selectedRecords[0]);
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, ObjectID, true);
  }

  private void FindToolStripMenuItem_Click(object sender, EventArgs e)
  {
    using (NavigatorContextSearchForm contextSearchForm = new NavigatorContextSearchForm())
    {
      contextSearchForm.NavigatorContextSearch = (INavigatorContextSearch) this;
      int num = (int) contextSearchForm.ShowDialog();
    }
  }

  private void LoadMoreToolStripDropDownButton_Click(object sender, EventArgs e)
  {
    this._records.AddRange((IEnumerable<AttributeChangeHistoryRecord>) this.FindRecords(this._records.LastOrDefault<AttributeChangeHistoryRecord>()));
    this.FillGrid();
  }

  private void UpdateControls()
  {
    if (this._attributeTypeIds.Count == 0)
      this._applyAttributesFilterToolStripButton.Checked = false;
    this._applyAttributesFilterToolStripButton.Enabled = this._attributeTypeIds.Count > 0;
    this._removeAttributeToolStripButton.Enabled = this.GetSelectedAttributeTypeIds().Length != 0;
    this._clearAttributesToolStripButton.Enabled = this._attributeTypeIds.Count > 0;
    if (this._objectTypeIds.Count == 0)
      this._applyObjectTypesFilterToolStripButton.Checked = false;
    this._applyObjectTypesFilterToolStripButton.Enabled = this._objectTypeIds.Count > 0;
    this._removeObjectTypeToolStripButton.Enabled = this.GetSelectedObjectTypeIds().Length != 0;
    this._clearObjectTypesToolStripButton.Enabled = this._objectTypeIds.Count > 0;
    if (this._relationTypeIds.Count == 0)
      this._applyRelationTypesFilterToolStripButton.Checked = false;
    this._applyRelationTypesFilterToolStripButton.Enabled = this._relationTypeIds.Count > 0;
    this._removeRelationTypeToolStripButton.Enabled = this.GetSelectedRelationTypeIds().Length != 0;
    this._clearRelationTypesToolStripButton.Enabled = this._relationTypeIds.Count > 0;
    if (this._userVersionIds.Count == 0)
      this._applyUsersFilterToolStripButton.Checked = false;
    this._applyUsersFilterToolStripButton.Enabled = this._userVersionIds.Count > 0;
    this._removeUserToolStripButton.Enabled = this.GetSelectedUserVersionIds().Length != 0;
    this._clearUsersToolStripButton.Enabled = this._userVersionIds.Count > 0;
    if (this._objectVersionIds.Count == 0)
      this._applyObjectsFilterToolStripButton.Checked = false;
    this._applyObjectsFilterToolStripButton.Enabled = this._objectVersionIds.Count > 0;
    this._removeObjectToolStripButton.Enabled = this.GetSelectedObjectVersionIds().Length != 0;
    this._clearObjectsToolStripButton.Enabled = this._objectVersionIds.Count > 0;
    ToolStripMenuItem toolStripMenuItem1 = this._openInNewWindowToolStripMenuItem;
    ToolStripMenuItem toolStripMenuItem2 = this._showCardToolStripMenuItem;
    bool flag1;
    this._showVersionsTreeToolStripMenuItem.Enabled = flag1 = this._selectedRecords.Count == 1 && this._selectedRecords[0].ObjectVersionIds != null && this._selectedRecords[0].ObjectVersionIds.Length != 0;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    toolStripMenuItem2.Enabled = num1 != 0;
    int num2 = flag2 ? 1 : 0;
    toolStripMenuItem1.Enabled = num2 != 0;
    this._copyTextToolStripMenuItem.Enabled = this._selectedRecords.Count > 0;
    this._findToolStripMenuItem.Enabled = this._grid.Rows.Count > 0;
  }

  private void FillObjectsToolStripComboBox(long mustBeSelectedObjectVersionID = 0)
  {
    this.FillObjectsToolsStripComboBox(this._objectsToolStripComboBox, this._objectVersionIds.ToArray(), mustBeSelectedObjectVersionID);
  }

  private void FillObjectsToolsStripComboBox(
    ToolStripComboBox toolStripComboBox,
    long[] objectVersionIds,
    long mustBeSelectedObjectVersionID = 0)
  {
    this.FillToolStripComboBox(toolStripComboBox, ((IEnumerable<Tuple<long, string>>) this.FindObjects(objectVersionIds)).Select<Tuple<long, string>, Tuple<object, string>>((System.Func<Tuple<long, string>, Tuple<object, string>>) (o => new Tuple<object, string>((object) o.Item1, o.Item2))).ToArray<Tuple<object, string>>(), (object) mustBeSelectedObjectVersionID);
  }

  private Tuple<long, string>[] FindObjects(long[] objectVersionIds)
  {
    List<Tuple<long, string>> tupleList = new List<Tuple<long, string>>();
    if (objectVersionIds.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
        objectCollection.LocalTypesMode = true;
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        dbRecordSetParams.Columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.CAPTION
        };
        // ISSUE: explicit reference operation
        (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure()
          {
            Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
            RelationalOperator = RelationalOperators.In,
            Value = (object) objectVersionIds,
            SQL = string.Empty
          }
        };
        dbRecordSetParams.RecordCount = -1;
        DBRecordSetParams paramSet = dbRecordSetParams;
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
          string stringValue = DataSetProcessor.GetStringValue(row, 1, (string) null);
          tupleList.Add(new Tuple<long, string>(int64Value, stringValue));
        }
      }
    }
    return tupleList.ToArray();
  }

  private void FillToolStripComboBox(
    ToolStripComboBox toolStripComboBox,
    Tuple<object, string>[] items,
    object mustBeSelectedItemID = null)
  {
    toolStripComboBox.BeginUpdate();
    try
    {
      toolStripComboBox.ComboBox.Items.Clear();
      toolStripComboBox.ComboBox.DisplayMember = "Item2";
      toolStripComboBox.ComboBox.ValueMember = "Item1";
      toolStripComboBox.ComboBox.Items.AddRange((object[]) ((IEnumerable<Tuple<object, string>>) items).OrderBy<Tuple<object, string>, string>((System.Func<Tuple<object, string>, string>) (o => o.Item2)).ToArray<Tuple<object, string>>());
      toolStripComboBox.SelectedItem = (object) ((IEnumerable<Tuple<object, string>>) items).FirstOrDefault<Tuple<object, string>>((System.Func<Tuple<object, string>, bool>) (o => object.Equals(o.Item1, mustBeSelectedItemID)));
      if (toolStripComboBox.SelectedItem == null && toolStripComboBox.ComboBox.Items.Count > 0)
        toolStripComboBox.SelectedItem = toolStripComboBox.ComboBox.Items[0];
      if (toolStripComboBox.SelectedItem != null)
        return;
      toolStripComboBox.Text = (string) null;
    }
    finally
    {
      toolStripComboBox.EndUpdate();
    }
  }

  private void FillAttributesToolStripComboBox(int mustBeSelectedAttributeTypeID = 0)
  {
    this.FillToolStripComboBox(this._attributesToolStripComboBox, this._attributeTypeIds.Select<int, IMSAttributeType>((System.Func<int, IMSAttributeType>) (o => MetaDataHelper.GetAttributeType(o))).Where<IMSAttributeType>((System.Func<IMSAttributeType, bool>) (o => o != null)).Select<IMSAttributeType, Tuple<object, string>>((System.Func<IMSAttributeType, Tuple<object, string>>) (o => new Tuple<object, string>((object) o.AttributeID, o.Name))).ToArray<Tuple<object, string>>(), (object) mustBeSelectedAttributeTypeID);
  }

  private void FillObjectTypesToolStripComboBox(int mustBeSelectedObjectTypeID = -1)
  {
    this.FillToolStripComboBox(this._objectTypesToolStripComboBox, this._objectTypeIds.Select<int, IMSObjectType>((System.Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).Where<IMSObjectType>((System.Func<IMSObjectType, bool>) (o => o != null)).Select<IMSObjectType, Tuple<object, string>>((System.Func<IMSObjectType, Tuple<object, string>>) (o => new Tuple<object, string>((object) o.ObjectTypeID, o.ObjectTypeName))).ToArray<Tuple<object, string>>(), (object) mustBeSelectedObjectTypeID);
  }

  private void FillRelationTypesToolStripComboBox(int mustBeSelectedRelationTypeID = -1)
  {
    this.FillToolStripComboBox(this._relationTypesToolStripComboBox, this._relationTypeIds.Select<int, IMSRelationType>((System.Func<int, IMSRelationType>) (o => MetaDataHelper.GetRelationType(o))).Where<IMSRelationType>((System.Func<IMSRelationType, bool>) (o => o != null)).Select<IMSRelationType, Tuple<object, string>>((System.Func<IMSRelationType, Tuple<object, string>>) (o => new Tuple<object, string>((object) o.RelationTypeID, o.Text))).ToArray<Tuple<object, string>>(), (object) mustBeSelectedRelationTypeID);
  }

  private void FillUsersToolStripComboBox(long mustBeSelectedUserVersionID = 0)
  {
    this.FillObjectsToolsStripComboBox(this._usersToolStripComboBox, this._userVersionIds.ToArray(), mustBeSelectedUserVersionID);
  }

  private void Reload()
  {
    if (this._preventReload)
      return;
    this._records.Clear();
    this._records.AddRange((IEnumerable<AttributeChangeHistoryRecord>) this.FindRecords());
    this.FillGrid();
  }

  private void FillGrid()
  {
    this._grid.AfterContentsGrouped -= new EventHandler(this.Grid_AfterContentsGrouped);
    this._grid.AfterContentsSorted -= new EventHandler(this.Grid_AfterContentsSorted);
    try
    {
      this._grid.BeginUpdate();
      try
      {
        this._grid.Rows.Clear();
        ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
        foreach (AttributeChangeHistoryRecord record in this._records)
        {
          iGRow iGrow = this._grid.Rows.Add();
          iGrow.Tag = (object) record;
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(record.AttributeTypeID);
          if (attributeType != null)
          {
            iGrow.Cells["Attribute"].ImageList = categoryTypeIconService.ImageList;
            iGrow.Cells["Attribute"].ImageIndex = categoryTypeIconService.IndexOf(3, -1, (object) attributeType.FieldType);
            iGrow.Cells["Attribute"].Value = (object) attributeType.Name;
          }
          iGrow.Cells["ObjectTypeRelationType"].ImageList = categoryTypeIconService.ImageList;
          if (!ObjectTypeHelper.IsUnknownObjectTypeID(record.ObjectTypeID))
          {
            iGrow.Cells["ObjectTypeRelationType"].ImageIndex = categoryTypeIconService.IndexOf(4, record.ObjectTypeID);
            iGrow.Cells["ObjectTypeRelationType"].Value = (object) MetaDataHelper.GetObjectTypeName(record.ObjectTypeID);
          }
          else
          {
            iGrow.Cells["ObjectTypeRelationType"].ImageIndex = categoryTypeIconService.IndexOf(6, record.RelationTypeID);
            iGrow.Cells["ObjectTypeRelationType"].Value = (object) MetaDataHelper.GetRelationTypeName(record.RelationTypeID);
          }
          iGrow.Cells["ObjectRelation"].Value = (object) record.ObjectCaption;
          iGrow.Cells["ObjectIDRelationID"].Value = ObjectTypeHelper.IsUnknownObjectTypeID(record.ObjectTypeID) ? (object) record.RelationID : (object) record.ObjectID;
          iGrow.Cells["Date"].Value = (object) record.Date;
          iGrow.Cells["Value"].Value = record.Value;
          iGrow.Cells["User"].ImageList = categoryTypeIconService.ImageList;
          iGrow.Cells["User"].ImageIndex = categoryTypeIconService.IndexOf(4, Constants.UserObjectTypeID);
          iGrow.Cells["User"].Value = (object) record.UserName;
        }
      }
      finally
      {
        this._grid.EndUpdate();
      }
      this._grid.Group();
      this._grid.Sort();
    }
    finally
    {
      this._grid.AfterContentsGrouped += new EventHandler(this.Grid_AfterContentsGrouped);
      this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    }
    this._recordCountToolStripStatusLabel.Text = this._records.Count.ToString();
    this._loadMoreToolStripDropDownButton.Enabled = this._canLoadMore;
  }

  private AttributeChangeHistoryRecord[] FindRecords(AttributeChangeHistoryRecord lastRecord = null)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAttributeChangeHistoryServerService customService = (IAttributeChangeHistoryServerService) sessionKeeper.Session.GetCustomService(typeof (IAttributeChangeHistoryServerService));
      FindRecordsParams findRecordsParams1 = new FindRecordsParams();
      if (this._applyAttributesFilterToolStripButton.Checked)
        findRecordsParams1.AttributeTypeIds = this._attributeTypeIds.ToArray();
      if (this._applyObjectsFilterToolStripButton.Checked)
        findRecordsParams1.ObjectVersionIds = this._objectVersionIds.ToArray();
      if (this._applyObjectTypesFilterToolStripButton.Checked)
        findRecordsParams1.ObjectTypeIds = this._objectTypeIds.ToArray();
      if (this._applyRelationTypesFilterToolStripButton.Checked)
        findRecordsParams1.RelationTypeIds = this._relationTypeIds.ToArray();
      if (this._applyUsersFilterToolStripButton.Checked)
        findRecordsParams1.UserAndUserGroupsVersionIds = this._userVersionIds.ToArray();
      if (this._fromToolStripDateTimePicker.DateTimePicker.Checked)
        findRecordsParams1.From = this._fromToolStripDateTimePicker.DateTimePicker.Value;
      if (this._toToolStripDateTimePicker.DateTimePicker.Checked)
        findRecordsParams1.To = this._toToolStripDateTimePicker.DateTimePicker.Value;
      Tuple<string, iGSortOrder>[] gridSortObject = this.GetGridSortObject();
      findRecordsParams1.SortColumns = ((IEnumerable<Tuple<string, iGSortOrder>>) gridSortObject).Select<Tuple<string, iGSortOrder>, ObligatoryObjectAttributes>((System.Func<Tuple<string, iGSortOrder>, ObligatoryObjectAttributes>) (o => this.ConvertColumnKeyToObligatoryObjectAttribute(o.Item1))).ToArray<ObligatoryObjectAttributes>();
      findRecordsParams1.SortOrders = ((IEnumerable<Tuple<string, iGSortOrder>>) gridSortObject).Select<Tuple<string, iGSortOrder>, SortOrders>((System.Func<Tuple<string, iGSortOrder>, SortOrders>) (o => this.ConvertGridColumnSortOrderToSearchSortOrder(o.Item2))).ToArray<SortOrders>();
      if (lastRecord != null)
        findRecordsParams1.LastRecordKey = lastRecord.Key;
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      FindRecordsParams findRecordsParams2 = findRecordsParams1;
      AttributeChangeHistoryRecord[] records = customService.FindRecords(sessionGuid, findRecordsParams2);
      this._canLoadMore = records.Length >= sessionKeeper.Session.MaxRows;
      return records;
    }
  }

  private ObligatoryObjectAttributes ConvertColumnKeyToObligatoryObjectAttribute(string columnKey)
  {
    switch (columnKey)
    {
      case "Attribute":
        return ObligatoryObjectAttributes.F_ATTRIBUTE_ID;
      case "ObjectIDRelationID":
        return ObligatoryObjectAttributes.F_ID;
      case "Date":
        return ObligatoryObjectAttributes.F_SET_DATE;
      case "User":
        return ObligatoryObjectAttributes.F_USER_ID;
      default:
        throw new Exception();
    }
  }

  private SortOrders ConvertGridColumnSortOrderToSearchSortOrder(iGSortOrder sortOrder)
  {
    if (sortOrder == iGSortOrder.Ascending)
      return SortOrders.ASC;
    return sortOrder != iGSortOrder.Descending ? SortOrders.NONE : SortOrders.DESC;
  }

  private void InitializeToolStripMenuItem(
    ToolStripMenuItem toolStripMenuItem,
    MenuTemplateNode menuTemplateNode)
  {
    ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    if (menuTemplateNode.ImageListSource == ImageListSource.CategoryImageList)
      toolStripMenuItem.Image = categoryTypeIconService.ImageList.Images[menuTemplateNode.ImageIndex];
    else if (menuTemplateNode.ImageListSource == ImageListSource.NamedImageList)
      toolStripMenuItem.Image = namedImageList.ImageList.Images[menuTemplateNode.ImageIndex];
    toolStripMenuItem.Text = menuTemplateNode.Text;
    toolStripMenuItem.ShortcutKeys = menuTemplateNode.Shortcut;
  }

  private int[] GetSelectedAttributeTypeIds()
  {
    return this.GetSelectedToolStripComboBoxItemIds(this._attributesToolStripComboBox).Cast<int>().ToArray<int>();
  }

  private object[] GetSelectedToolStripComboBoxItemIds(ToolStripComboBox toolStripComboBox)
  {
    List<object> objectList = new List<object>();
    if (toolStripComboBox.SelectedItem != null)
      objectList.Add(((Tuple<object, string>) toolStripComboBox.SelectedItem).Item1);
    return objectList.ToArray();
  }

  private int GetMustBeSelectedAfterRemovingAttributeTypeID()
  {
    object stripComboBoxItemId = this.GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(this._attributesToolStripComboBox);
    return stripComboBoxItemId == null ? 0 : (int) stripComboBoxItemId;
  }

  private object GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(
    ToolStripComboBox toolStripComboBox)
  {
    object stripComboBoxItemId = (object) null;
    if (toolStripComboBox.SelectedItem != null)
    {
      int num = toolStripComboBox.ComboBox.Items.IndexOf(toolStripComboBox.SelectedItem);
      int index = num > 0 ? num - 1 : num + 1;
      if (index <= toolStripComboBox.ComboBox.Items.Count - 1)
        stripComboBoxItemId = ((Tuple<object, string>) toolStripComboBox.ComboBox.Items[index]).Item1;
    }
    return stripComboBoxItemId;
  }

  private int[] GetSelectedObjectTypeIds()
  {
    return this.GetSelectedToolStripComboBoxItemIds(this._objectTypesToolStripComboBox).Cast<int>().ToArray<int>();
  }

  private int GetMustBeSelectedAfterRemovingObjectTypeID()
  {
    object stripComboBoxItemId = this.GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(this._objectTypesToolStripComboBox);
    return stripComboBoxItemId == null ? -1 : (int) stripComboBoxItemId;
  }

  private int[] GetSelectedRelationTypeIds()
  {
    return this.GetSelectedToolStripComboBoxItemIds(this._relationTypesToolStripComboBox).Cast<int>().ToArray<int>();
  }

  private int GetMustBeSelectedAfterRemovingRelationTypeID()
  {
    object stripComboBoxItemId = this.GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(this._relationTypesToolStripComboBox);
    return stripComboBoxItemId == null ? -1 : (int) stripComboBoxItemId;
  }

  private long[] GetSelectedUserVersionIds()
  {
    return this.GetSelectedToolStripComboBoxItemIds(this._usersToolStripComboBox).Cast<long>().ToArray<long>();
  }

  private long GetMustBeSelectedAfterRemovingUserVersionID()
  {
    object stripComboBoxItemId = this.GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(this._usersToolStripComboBox);
    return stripComboBoxItemId == null ? 0L : (long) stripComboBoxItemId;
  }

  private long[] GetSelectedObjectVersionIds()
  {
    return this.GetSelectedToolStripComboBoxItemIds(this._objectsToolStripComboBox).Cast<long>().ToArray<long>();
  }

  private long GetMustBeSelectedAfterRemovingObjectVersionID()
  {
    object stripComboBoxItemId = this.GetMustBeSelectedAfterRemovingToolStripComboBoxItemID(this._objectsToolStripComboBox);
    return stripComboBoxItemId == null ? 0L : (long) stripComboBoxItemId;
  }

  private AttributeChangeHistoryRecord[] GetSelectedRecords()
  {
    List<AttributeChangeHistoryRecord> changeHistoryRecordList = new List<AttributeChangeHistoryRecord>();
    foreach (iGCell selectedCell in this._grid.SelectedCells)
    {
      if (selectedCell.Row.Tag is AttributeChangeHistoryRecord && !changeHistoryRecordList.Contains((AttributeChangeHistoryRecord) selectedCell.Row.Tag))
        changeHistoryRecordList.Add((AttributeChangeHistoryRecord) selectedCell.Row.Tag);
    }
    return changeHistoryRecordList.ToArray();
  }

  private long SelectOneObjectVersionID(AttributeChangeHistoryRecord record)
  {
    if (record.ObjectVersionIds.Length == 1)
      return record.ObjectVersionIds[0];
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор версии объекта", "Выбранный объект имеет несколько версий. Выберите версию объекта, для которой нужно выполнить команду.", (IDescriptor) new ListVersionsDescriptor(record.ObjectID, record.ObjectTypeID), (System.IServiceProvider) ServicesManager.ServiceContainer, SelectionOptions.HideTree | SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    return numArray != null && numArray.Length != 0 ? numArray[0] : 0L;
  }

  private Tuple<string, iGSortOrder>[] GetGridGroupObject()
  {
    List<Tuple<string, iGSortOrder>> tupleList = new List<Tuple<string, iGSortOrder>>();
    for (int index = 0; index < this._grid.GroupObject.Count; ++index)
    {
      iGSortItem iGsortItem = this._grid.GroupObject[index];
      tupleList.Add(new Tuple<string, iGSortOrder>(this._grid.Cols[iGsortItem.ColIndex].Key, iGsortItem.SortOrder));
    }
    return tupleList.ToArray();
  }

  private Tuple<string, iGSortOrder>[] GetGridSortObject()
  {
    List<Tuple<string, iGSortOrder>> tupleList = new List<Tuple<string, iGSortOrder>>();
    for (int index = 0; index < this._grid.SortObject.Count; ++index)
    {
      iGSortItem iGsortItem = this._grid.SortObject[index];
      tupleList.Add(new Tuple<string, iGSortOrder>(this._grid.Cols[iGsortItem.ColIndex].Key, iGsortItem.SortOrder));
    }
    return tupleList.ToArray();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributeChangeHistoryControl));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    iGColPattern iGcolPattern4 = new iGColPattern();
    iGColPattern iGcolPattern5 = new iGColPattern();
    iGColPattern iGcolPattern6 = new iGColPattern();
    iGColPattern iGcolPattern7 = new iGColPattern();
    this._gridCol0CellStyle = new iGCellStyle(true);
    this._gridCol0ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol1CellStyle = new iGCellStyle(true);
    this._gridCol1ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol2CellStyle = new iGCellStyle(true);
    this._gridCol2ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol3CellStyle = new iGCellStyle(true);
    this._gridCol3ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol4CellStyle = new iGCellStyle(true);
    this._gridCol4ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol5CellStyle = new iGCellStyle(true);
    this._gridCol5ColHdrStyle = new iGColHdrStyle(true);
    this._gridCol7CellStyle = new iGCellStyle(true);
    this._gridCol7ColHdrStyle = new iGColHdrStyle(true);
    this.toolStrip1 = new ToolStrip();
    this.toolStripLabel1 = new ToolStripLabel();
    this._attributesToolStripComboBox = new ToolStripComboBox();
    this._applyAttributesFilterToolStripButton = new ToolStripButton();
    this._addAttributesToolStripButton = new ToolStripButton();
    this._removeAttributeToolStripButton = new ToolStripButton();
    this._clearAttributesToolStripButton = new ToolStripButton();
    this.toolStripLabel2 = new ToolStripLabel();
    this._objectTypesToolStripComboBox = new ToolStripComboBox();
    this._applyObjectTypesFilterToolStripButton = new ToolStripButton();
    this._addObjectTypesToolStripButton = new ToolStripButton();
    this._removeObjectTypeToolStripButton = new ToolStripButton();
    this._clearObjectTypesToolStripButton = new ToolStripButton();
    this.toolStripLabel3 = new ToolStripLabel();
    this._relationTypesToolStripComboBox = new ToolStripComboBox();
    this._applyRelationTypesFilterToolStripButton = new ToolStripButton();
    this._addRelationTypesToolStripButton = new ToolStripButton();
    this._removeRelationTypeToolStripButton = new ToolStripButton();
    this._clearRelationTypesToolStripButton = new ToolStripButton();
    this.toolStripLabel4 = new ToolStripLabel();
    this._usersToolStripComboBox = new ToolStripComboBox();
    this._applyUsersFilterToolStripButton = new ToolStripButton();
    this._addUsersToolStripButton = new ToolStripButton();
    this._removeUserToolStripButton = new ToolStripButton();
    this._clearUsersToolStripButton = new ToolStripButton();
    this.toolStripLabel5 = new ToolStripLabel();
    this._fromToolStripDateTimePicker = new ToolStripDateTimePicker();
    this.toolStripLabel6 = new ToolStripLabel();
    this._toToolStripDateTimePicker = new ToolStripDateTimePicker();
    this.toolStripLabel7 = new ToolStripLabel();
    this._objectsToolStripComboBox = new ToolStripComboBox();
    this._applyObjectsFilterToolStripButton = new ToolStripButton();
    this._addObjectsToolStripButton = new ToolStripButton();
    this._removeObjectToolStripButton = new ToolStripButton();
    this._clearObjectsToolStripButton = new ToolStripButton();
    this._grid = new TenTec.Windows.iGridLib.iGrid();
    this._contextMenuStrip = new ContextMenuStrip(this.components);
    this._reloadToolStripMenuItem = new ToolStripMenuItem();
    this._copyTextToolStripMenuItem = new ToolStripMenuItem();
    this._openInNewWindowToolStripMenuItem = new ToolStripMenuItem();
    this._showVersionsTreeToolStripMenuItem = new ToolStripMenuItem();
    this._showCardToolStripMenuItem = new ToolStripMenuItem();
    this._findToolStripMenuItem = new ToolStripMenuItem();
    this.statusStrip1 = new StatusStrip();
    this.toolStripStatusLabel2 = new ToolStripStatusLabel();
    this.toolStripStatusLabel1 = new ToolStripStatusLabel();
    this._recordCountToolStripStatusLabel = new ToolStripStatusLabel();
    this._loadMoreToolStripDropDownButton = new ToolStripDropDownButton();
    this.panel1 = new Panel();
    this.toolStrip6 = new ToolStrip();
    this.toolStrip2 = new ToolStrip();
    this.toolStrip3 = new ToolStrip();
    this.toolStrip4 = new ToolStrip();
    this.toolStrip5 = new ToolStrip();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.toolStrip1.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    this._contextMenuStrip.SuspendLayout();
    this.statusStrip1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.toolStrip6.SuspendLayout();
    this.toolStrip2.SuspendLayout();
    this.toolStrip3.SuspendLayout();
    this.toolStrip4.SuspendLayout();
    this.toolStrip5.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.toolStrip1.CanOverflow = false;
    this.toolStrip1.Dock = DockStyle.None;
    this.toolStrip1.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.toolStripLabel1,
      (ToolStripItem) this._attributesToolStripComboBox,
      (ToolStripItem) this._applyAttributesFilterToolStripButton,
      (ToolStripItem) this._addAttributesToolStripButton,
      (ToolStripItem) this._removeAttributeToolStripButton,
      (ToolStripItem) this._clearAttributesToolStripButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(438, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(132, 22);
    this.toolStripLabel1.Text = "Фильтр по атрибутам: ";
    this._attributesToolStripComboBox.BackColor = SystemColors.Control;
    this._attributesToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._attributesToolStripComboBox.Name = "_attributesToolStripComboBox";
    this._attributesToolStripComboBox.Size = new Size(200, 25);
    this._attributesToolStripComboBox.SelectedIndexChanged += new EventHandler(this.AttributesToolStripComboBox_SelectedIndexChanged);
    this._applyAttributesFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._applyAttributesFilterToolStripButton.Image = (Image) componentResourceManager.GetObject("_applyAttributesFilterToolStripButton.Image");
    this._applyAttributesFilterToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
    this._applyAttributesFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._applyAttributesFilterToolStripButton.Name = "_applyAttributesFilterToolStripButton";
    this._applyAttributesFilterToolStripButton.Size = new Size(23, 22);
    this._applyAttributesFilterToolStripButton.Text = "Применить фильтр по атрибутам";
    this._applyAttributesFilterToolStripButton.Click += new EventHandler(this.ApplyAttributesFilterToolStripButton_Click);
    this._addAttributesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addAttributesToolStripButton.Image = (Image) Resources.AddStandart;
    this._addAttributesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addAttributesToolStripButton.Name = "_addAttributesToolStripButton";
    this._addAttributesToolStripButton.Size = new Size(23, 22);
    this._addAttributesToolStripButton.Text = "Добавить атрибуты";
    this._addAttributesToolStripButton.Click += new EventHandler(this.AddAttributesToolStripButton_Click);
    this._removeAttributeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeAttributeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeAttributeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeAttributeToolStripButton.Name = "_removeAttributeToolStripButton";
    this._removeAttributeToolStripButton.Size = new Size(23, 22);
    this._removeAttributeToolStripButton.Text = "Удалить атрибуты";
    this._removeAttributeToolStripButton.Click += new EventHandler(this.RemoveAttributeToolStripButton_Click);
    this._clearAttributesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._clearAttributesToolStripButton.Image = (Image) Resources.FormLink_Clean;
    this._clearAttributesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._clearAttributesToolStripButton.Name = "_clearAttributesToolStripButton";
    this._clearAttributesToolStripButton.Size = new Size(23, 22);
    this._clearAttributesToolStripButton.Text = "Удалить все атрибуты";
    this._clearAttributesToolStripButton.Click += new EventHandler(this.ClearAttributesToolStripButton_Click);
    this.toolStripLabel2.Name = "toolStripLabel2";
    this.toolStripLabel2.Size = new Size(162, 22);
    this.toolStripLabel2.Text = "Фильтр по типам объектов: ";
    this._objectTypesToolStripComboBox.BackColor = SystemColors.Control;
    this._objectTypesToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._objectTypesToolStripComboBox.Name = "_objectTypesToolStripComboBox";
    this._objectTypesToolStripComboBox.Size = new Size(200, 25);
    this._objectTypesToolStripComboBox.SelectedIndexChanged += new EventHandler(this.ObjectTypesToolStripComboBox_SelectedIndexChanged);
    this._applyObjectTypesFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._applyObjectTypesFilterToolStripButton.Image = (Image) componentResourceManager.GetObject("_applyObjectTypesFilterToolStripButton.Image");
    this._applyObjectTypesFilterToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
    this._applyObjectTypesFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._applyObjectTypesFilterToolStripButton.Name = "_applyObjectTypesFilterToolStripButton";
    this._applyObjectTypesFilterToolStripButton.Size = new Size(23, 22);
    this._applyObjectTypesFilterToolStripButton.Text = "Применить фильтр по типам объектов";
    this._applyObjectTypesFilterToolStripButton.Click += new EventHandler(this.ApplyObjectTypesFilterToolStripButton_Click);
    this._addObjectTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addObjectTypesToolStripButton.Image = (Image) Resources.AddStandart;
    this._addObjectTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addObjectTypesToolStripButton.Name = "_addObjectTypesToolStripButton";
    this._addObjectTypesToolStripButton.Size = new Size(23, 22);
    this._addObjectTypesToolStripButton.Text = "Добавить типы объектов";
    this._addObjectTypesToolStripButton.Click += new EventHandler(this.AddObjectTypesToolStripButton_Click);
    this._removeObjectTypeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeObjectTypeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeObjectTypeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeObjectTypeToolStripButton.Name = "_removeObjectTypeToolStripButton";
    this._removeObjectTypeToolStripButton.Size = new Size(23, 22);
    this._removeObjectTypeToolStripButton.Text = "Удалить типы объектов";
    this._removeObjectTypeToolStripButton.Click += new EventHandler(this.RemoveObjectTypeToolStripButton_Click);
    this._clearObjectTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._clearObjectTypesToolStripButton.Image = (Image) Resources.FormLink_Clean;
    this._clearObjectTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._clearObjectTypesToolStripButton.Name = "_clearObjectTypesToolStripButton";
    this._clearObjectTypesToolStripButton.Size = new Size(23, 22);
    this._clearObjectTypesToolStripButton.Text = "Удалить все типы объектов";
    this._clearObjectTypesToolStripButton.Click += new EventHandler(this.ClearObjectTypesToolStripButton_Click);
    this.toolStripLabel3.Name = "toolStripLabel3";
    this.toolStripLabel3.Size = new Size(147, 22);
    this.toolStripLabel3.Text = "Фильтр по типам связей: ";
    this._relationTypesToolStripComboBox.BackColor = SystemColors.Control;
    this._relationTypesToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._relationTypesToolStripComboBox.Name = "_relationTypesToolStripComboBox";
    this._relationTypesToolStripComboBox.Size = new Size(200, 25);
    this._relationTypesToolStripComboBox.SelectedIndexChanged += new EventHandler(this.RelationTypesToolStripComboBox_SelectedIndexChanged);
    this._applyRelationTypesFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._applyRelationTypesFilterToolStripButton.Image = (Image) componentResourceManager.GetObject("_applyRelationTypesFilterToolStripButton.Image");
    this._applyRelationTypesFilterToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
    this._applyRelationTypesFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._applyRelationTypesFilterToolStripButton.Name = "_applyRelationTypesFilterToolStripButton";
    this._applyRelationTypesFilterToolStripButton.Size = new Size(23, 22);
    this._applyRelationTypesFilterToolStripButton.Text = "Применить фильтр по типам связей";
    this._applyRelationTypesFilterToolStripButton.Click += new EventHandler(this.ApplyRelationTypesFilterToolStripButton_Click);
    this._addRelationTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addRelationTypesToolStripButton.Image = (Image) Resources.AddStandart;
    this._addRelationTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addRelationTypesToolStripButton.Name = "_addRelationTypesToolStripButton";
    this._addRelationTypesToolStripButton.Size = new Size(23, 22);
    this._addRelationTypesToolStripButton.Text = "Добавить типы связей";
    this._addRelationTypesToolStripButton.Click += new EventHandler(this.AddRelationTypesToolStripButton_Click);
    this._removeRelationTypeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeRelationTypeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeRelationTypeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeRelationTypeToolStripButton.Name = "_removeRelationTypeToolStripButton";
    this._removeRelationTypeToolStripButton.Size = new Size(23, 22);
    this._removeRelationTypeToolStripButton.Text = "Удалить типы связей";
    this._removeRelationTypeToolStripButton.Click += new EventHandler(this.RemoveRelationTypeToolStripButton_Click);
    this._clearRelationTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._clearRelationTypesToolStripButton.Image = (Image) Resources.FormLink_Clean;
    this._clearRelationTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._clearRelationTypesToolStripButton.Name = "_clearRelationTypesToolStripButton";
    this._clearRelationTypesToolStripButton.Size = new Size(23, 22);
    this._clearRelationTypesToolStripButton.Text = "Удалить все типы связей";
    this._clearRelationTypesToolStripButton.Click += new EventHandler(this.ClearRelationTypesToolStripButton_Click);
    this.toolStripLabel4.Name = "toolStripLabel4";
    this.toolStripLabel4.Size = new Size(158, 22);
    this.toolStripLabel4.Text = "Фильтр по пользователям: ";
    this._usersToolStripComboBox.BackColor = SystemColors.Control;
    this._usersToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._usersToolStripComboBox.Name = "_usersToolStripComboBox";
    this._usersToolStripComboBox.Size = new Size(200, 25);
    this._usersToolStripComboBox.SelectedIndexChanged += new EventHandler(this.UsersToolStripComboBox_SelectedIndexChanged);
    this._applyUsersFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._applyUsersFilterToolStripButton.Image = (Image) componentResourceManager.GetObject("_applyUsersFilterToolStripButton.Image");
    this._applyUsersFilterToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
    this._applyUsersFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._applyUsersFilterToolStripButton.Name = "_applyUsersFilterToolStripButton";
    this._applyUsersFilterToolStripButton.Size = new Size(23, 22);
    this._applyUsersFilterToolStripButton.Text = "Применить фильтр по пользователям";
    this._applyUsersFilterToolStripButton.Click += new EventHandler(this.ApplyUsersFilterToolStripButton_Click);
    this._addUsersToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addUsersToolStripButton.Image = (Image) Resources.AddStandart;
    this._addUsersToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addUsersToolStripButton.Name = "_addUsersToolStripButton";
    this._addUsersToolStripButton.Size = new Size(23, 22);
    this._addUsersToolStripButton.Text = "Добавить пользователя или группу";
    this._addUsersToolStripButton.Click += new EventHandler(this.AddUsersToolStripButton_Click);
    this._removeUserToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeUserToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeUserToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeUserToolStripButton.Name = "_removeUserToolStripButton";
    this._removeUserToolStripButton.Size = new Size(23, 22);
    this._removeUserToolStripButton.Text = "Удалить пользователя или группу";
    this._removeUserToolStripButton.Click += new EventHandler(this.RemoveUserToolStripButton_Click);
    this._clearUsersToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._clearUsersToolStripButton.Image = (Image) Resources.FormLink_Clean;
    this._clearUsersToolStripButton.ImageTransparentColor = Color.Magenta;
    this._clearUsersToolStripButton.Name = "_clearUsersToolStripButton";
    this._clearUsersToolStripButton.Size = new Size(23, 22);
    this._clearUsersToolStripButton.Text = "Удалить всех пользователей/группы";
    this._clearUsersToolStripButton.Click += new EventHandler(this.ClearUsersToolStripButton_Click);
    this.toolStripLabel5.Name = "toolStripLabel5";
    this.toolStripLabel5.Size = new Size(27, 23);
    this.toolStripLabel5.Text = "От: ";
    this._fromToolStripDateTimePicker.Name = "_fromToolStripDateTimePicker";
    this._fromToolStripDateTimePicker.Size = new Size(200, 23);
    this._fromToolStripDateTimePicker.Text = "Thursday, November 15, 2018";
    this.toolStripLabel6.Name = "toolStripLabel6";
    this.toolStripLabel6.Size = new Size(25, 23);
    this.toolStripLabel6.Text = "До:";
    this._toToolStripDateTimePicker.Name = "_toToolStripDateTimePicker";
    this._toToolStripDateTimePicker.Size = new Size(200, 23);
    this._toToolStripDateTimePicker.Text = "Thursday, November 15, 2018";
    this.toolStripLabel7.Name = "toolStripLabel7";
    this.toolStripLabel7.Size = new Size(124, 22);
    this.toolStripLabel7.Text = "Фильтр по объектам:";
    this._objectsToolStripComboBox.BackColor = SystemColors.Control;
    this._objectsToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._objectsToolStripComboBox.Name = "_objectsToolStripComboBox";
    this._objectsToolStripComboBox.Size = new Size(200, 25);
    this._objectsToolStripComboBox.SelectedIndexChanged += new EventHandler(this.ObjectsToolStripComboBox_SelectedIndexChanged);
    this._applyObjectsFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._applyObjectsFilterToolStripButton.Image = (Image) componentResourceManager.GetObject("_applyObjectsFilterToolStripButton.Image");
    this._applyObjectsFilterToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
    this._applyObjectsFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._applyObjectsFilterToolStripButton.Name = "_applyObjectsFilterToolStripButton";
    this._applyObjectsFilterToolStripButton.Size = new Size(23, 22);
    this._applyObjectsFilterToolStripButton.Text = "Применить фильтр по объектам";
    this._applyObjectsFilterToolStripButton.Click += new EventHandler(this.ApplyObjectsFilterToolStripButton_Click);
    this._addObjectsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addObjectsToolStripButton.Image = (Image) Resources.AddStandart;
    this._addObjectsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addObjectsToolStripButton.Name = "_addObjectsToolStripButton";
    this._addObjectsToolStripButton.Size = new Size(23, 22);
    this._addObjectsToolStripButton.Text = "Добавить объекты";
    this._addObjectsToolStripButton.Click += new EventHandler(this.AddObjectsToolStripButton_Click);
    this._removeObjectToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeObjectToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeObjectToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeObjectToolStripButton.Name = "_removeObjectToolStripButton";
    this._removeObjectToolStripButton.Size = new Size(23, 22);
    this._removeObjectToolStripButton.Text = "Удалить объекты";
    this._removeObjectToolStripButton.Click += new EventHandler(this.RemoveObjectToolStripButton_Click);
    this._clearObjectsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._clearObjectsToolStripButton.Image = (Image) Resources.FormLink_Clean;
    this._clearObjectsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._clearObjectsToolStripButton.Name = "_clearObjectsToolStripButton";
    this._clearObjectsToolStripButton.Size = new Size(23, 22);
    this._clearObjectsToolStripButton.Text = "Удалить все объекты";
    this._clearObjectsToolStripButton.Click += new EventHandler(this.ClearObjectsToolStripButton_Click);
    this._grid.AllowDrop = true;
    this._grid.AutoWidthColMode = iGAutoWidthColMode.Cells;
    this._grid.BackColorEvenRows = SystemColors.Window;
    this._grid.BackColorOddRows = SystemColors.Window;
    iGcolPattern1.CellStyle = this._gridCol0CellStyle;
    iGcolPattern1.ColHdrStyle = this._gridCol0ColHdrStyle;
    iGcolPattern1.Key = "Attribute";
    iGcolPattern1.SortType = iGSortType.ByCustomer;
    iGcolPattern1.Text = (object) "Атрибут";
    iGcolPattern1.Width = 250;
    iGcolPattern2.CellStyle = this._gridCol1CellStyle;
    iGcolPattern2.ColHdrStyle = this._gridCol1ColHdrStyle;
    iGcolPattern2.Key = "ObjectTypeRelationType";
    iGcolPattern2.SortType = iGSortType.None;
    iGcolPattern2.Text = (object) "Тип объекта/связи";
    iGcolPattern2.Width = 250;
    iGcolPattern3.CellStyle = this._gridCol2CellStyle;
    iGcolPattern3.ColHdrStyle = this._gridCol2ColHdrStyle;
    iGcolPattern3.Key = "ObjectRelation";
    iGcolPattern3.SortType = iGSortType.None;
    iGcolPattern3.Text = (object) "Объект/связь";
    iGcolPattern3.Width = 200;
    iGcolPattern4.CellStyle = this._gridCol3CellStyle;
    iGcolPattern4.ColHdrStyle = this._gridCol3ColHdrStyle;
    iGcolPattern4.Key = "ObjectIDRelationID";
    iGcolPattern4.SortType = iGSortType.ByCustomer;
    iGcolPattern4.Text = (object) "Идентификатор объекта/связи";
    iGcolPattern4.Width = 100;
    iGcolPattern5.CellStyle = this._gridCol4CellStyle;
    iGcolPattern5.ColHdrStyle = this._gridCol4ColHdrStyle;
    iGcolPattern5.Key = "Date";
    iGcolPattern5.SortOrder = iGSortOrder.Descending;
    iGcolPattern5.SortType = iGSortType.ByCustomer;
    iGcolPattern5.Text = (object) "Дата изменения";
    iGcolPattern5.Width = 150;
    iGcolPattern6.CellStyle = this._gridCol5CellStyle;
    iGcolPattern6.ColHdrStyle = this._gridCol5ColHdrStyle;
    iGcolPattern6.Key = "Value";
    iGcolPattern6.SortType = iGSortType.None;
    iGcolPattern6.Text = (object) "Значение";
    iGcolPattern6.Width = 150;
    iGcolPattern7.CellStyle = this._gridCol7CellStyle;
    iGcolPattern7.ColHdrStyle = this._gridCol7ColHdrStyle;
    iGcolPattern7.Key = "User";
    iGcolPattern7.SortType = iGSortType.ByCustomer;
    iGcolPattern7.Text = (object) "Пользователь";
    iGcolPattern7.Width = 200;
    this._grid.Cols.AddRange(new iGColPattern[7]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3,
      iGcolPattern4,
      iGcolPattern5,
      iGcolPattern6,
      iGcolPattern7
    });
    this._grid.ContextMenuStrip = this._contextMenuStrip;
    this._grid.Cursor = Cursors.Default;
    this._grid.DefaultAutoGroupRow.Height = 25;
    this._grid.DefaultCol.Width = 120;
    this._grid.DefaultRow.Height = 25;
    this._grid.DefaultRow.NormalCellHeight = 25;
    this._grid.Dock = DockStyle.Fill;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 19;
    this._grid.HighlightBackColorNoFocus = SystemColors.Highlight;
    this._grid.HighlightForeColorNoFocus = SystemColors.HighlightText;
    this._grid.HotTracking = false;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Location = new Point(3, 85);
    this._grid.Name = "_grid";
    this._grid.PageCapacity = 500;
    this._grid.PressedMouseMoveMode = iGPressedMouseMoveMode.Normal;
    this._grid.ProcessTab = false;
    this._grid.RowMode = true;
    this._grid.RowModeHasCurCell = true;
    this._grid.RowTextStartColNear = 211;
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.ShowControlsInAllCells = false;
    this._grid.Size = new Size(1078, 363);
    this._grid.TabIndex = 2;
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._grid.AfterContentsGrouped += new EventHandler(this.Grid_AfterContentsGrouped);
    this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    this._contextMenuStrip.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this._reloadToolStripMenuItem,
      (ToolStripItem) this._copyTextToolStripMenuItem,
      (ToolStripItem) this._openInNewWindowToolStripMenuItem,
      (ToolStripItem) this._showVersionsTreeToolStripMenuItem,
      (ToolStripItem) this._showCardToolStripMenuItem,
      (ToolStripItem) this._findToolStripMenuItem
    });
    this._contextMenuStrip.Name = "_contextMenuStrip";
    this._contextMenuStrip.Size = new Size(204, 136);
    this._reloadToolStripMenuItem.Name = "_reloadToolStripMenuItem";
    this._reloadToolStripMenuItem.Size = new Size(203, 22);
    this._reloadToolStripMenuItem.Text = "Обновить";
    this._reloadToolStripMenuItem.Click += new EventHandler(this.ReloadToolStripMenuItem_Click);
    this._copyTextToolStripMenuItem.Name = "_copyTextToolStripMenuItem";
    this._copyTextToolStripMenuItem.Size = new Size(203, 22);
    this._copyTextToolStripMenuItem.Text = "Скопировать текст";
    this._copyTextToolStripMenuItem.Click += new EventHandler(this.CopyTextToolStripMenuItem_Click);
    this._openInNewWindowToolStripMenuItem.Name = "_openInNewWindowToolStripMenuItem";
    this._openInNewWindowToolStripMenuItem.Size = new Size(203, 22);
    this._openInNewWindowToolStripMenuItem.Text = "Открыть в новом окне";
    this._openInNewWindowToolStripMenuItem.Click += new EventHandler(this.OpenInNewWindowToolStripMenuItem_Click);
    this._showVersionsTreeToolStripMenuItem.Name = "_showVersionsTreeToolStripMenuItem";
    this._showVersionsTreeToolStripMenuItem.Size = new Size(203, 22);
    this._showVersionsTreeToolStripMenuItem.Text = "Дерево версий объекта";
    this._showVersionsTreeToolStripMenuItem.Click += new EventHandler(this.ShowVersionsTreeToolStripMenuItem_Click);
    this._showCardToolStripMenuItem.Name = "_showCardToolStripMenuItem";
    this._showCardToolStripMenuItem.Size = new Size(203, 22);
    this._showCardToolStripMenuItem.Text = "Карточка";
    this._showCardToolStripMenuItem.Click += new EventHandler(this.ShowCardToolStripMenuItem_Click);
    this._findToolStripMenuItem.Name = "_findToolStripMenuItem";
    this._findToolStripMenuItem.Size = new Size(203, 22);
    this._findToolStripMenuItem.Text = "Найти текст";
    this._findToolStripMenuItem.Click += new EventHandler(this.FindToolStripMenuItem_Click);
    this.statusStrip1.Dock = DockStyle.Fill;
    this.statusStrip1.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.toolStripStatusLabel2,
      (ToolStripItem) this.toolStripStatusLabel1,
      (ToolStripItem) this._recordCountToolStripStatusLabel,
      (ToolStripItem) this._loadMoreToolStripDropDownButton
    });
    this.statusStrip1.Location = new Point(0, 451);
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.Size = new Size(1084, 22);
    this.statusStrip1.SizingGrip = false;
    this.statusStrip1.TabIndex = 3;
    this.statusStrip1.Text = "statusStrip1";
    this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
    this.toolStripStatusLabel2.Size = new Size(995, 17);
    this.toolStripStatusLabel2.Spring = true;
    this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
    this.toolStripStatusLabel1.Size = new Size(41, 17);
    this.toolStripStatusLabel1.Text = "Всего:";
    this._recordCountToolStripStatusLabel.Name = "_recordCountToolStripStatusLabel";
    this._recordCountToolStripStatusLabel.Size = new Size(13, 17);
    this._recordCountToolStripStatusLabel.Text = "0";
    this._loadMoreToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._loadMoreToolStripDropDownButton.Image = (Image) componentResourceManager.GetObject("_loadMoreToolStripDropDownButton.Image");
    this._loadMoreToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
    this._loadMoreToolStripDropDownButton.Name = "_loadMoreToolStripDropDownButton";
    this._loadMoreToolStripDropDownButton.ShowDropDownArrow = false;
    this._loadMoreToolStripDropDownButton.Size = new Size(20, 20);
    this._loadMoreToolStripDropDownButton.Text = "Загрузить следующие записи";
    this._loadMoreToolStripDropDownButton.Click += new EventHandler(this.LoadMoreToolStripDropDownButton_Click);
    this.panel1.Controls.Add((Control) this.tableLayoutPanel1);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(1084, 473);
    this.panel1.TabIndex = 4;
    this.toolStrip6.CanOverflow = false;
    this.toolStrip6.Dock = DockStyle.None;
    this.toolStrip6.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.toolStripLabel7,
      (ToolStripItem) this._objectsToolStripComboBox,
      (ToolStripItem) this._applyObjectsFilterToolStripButton,
      (ToolStripItem) this._addObjectsToolStripButton,
      (ToolStripItem) this._removeObjectToolStripButton,
      (ToolStripItem) this._clearObjectsToolStripButton
    });
    this.toolStrip6.Location = new Point(464, 50);
    this.toolStrip6.Name = "toolStrip6";
    this.toolStrip6.Size = new Size(430, 25);
    this.toolStrip6.TabIndex = 3;
    this.toolStrip6.Text = "toolStrip6";
    this.toolStrip2.CanOverflow = false;
    this.toolStrip2.Dock = DockStyle.None;
    this.toolStrip2.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.toolStripLabel2,
      (ToolStripItem) this._objectTypesToolStripComboBox,
      (ToolStripItem) this._applyObjectTypesFilterToolStripButton,
      (ToolStripItem) this._addObjectTypesToolStripButton,
      (ToolStripItem) this._removeObjectTypeToolStripButton,
      (ToolStripItem) this._clearObjectTypesToolStripButton
    });
    this.toolStrip2.Location = new Point(438, 0);
    this.toolStrip2.Name = "toolStrip2";
    this.toolStrip2.Size = new Size(468, 25);
    this.toolStrip2.TabIndex = 5;
    this.toolStrip2.Text = "toolStrip2";
    this.toolStrip3.CanOverflow = false;
    this.toolStrip3.Dock = DockStyle.None;
    this.toolStrip3.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.toolStripLabel3,
      (ToolStripItem) this._relationTypesToolStripComboBox,
      (ToolStripItem) this._applyRelationTypesFilterToolStripButton,
      (ToolStripItem) this._addRelationTypesToolStripButton,
      (ToolStripItem) this._removeRelationTypeToolStripButton,
      (ToolStripItem) this._clearRelationTypesToolStripButton
    });
    this.toolStrip3.Location = new Point(0, 25);
    this.toolStrip3.Name = "toolStrip3";
    this.toolStrip3.Size = new Size(453, 25);
    this.toolStrip3.TabIndex = 6;
    this.toolStrip3.Text = "toolStrip3";
    this.toolStrip4.CanOverflow = false;
    this.toolStrip4.Dock = DockStyle.None;
    this.toolStrip4.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.toolStripLabel4,
      (ToolStripItem) this._usersToolStripComboBox,
      (ToolStripItem) this._applyUsersFilterToolStripButton,
      (ToolStripItem) this._addUsersToolStripButton,
      (ToolStripItem) this._removeUserToolStripButton,
      (ToolStripItem) this._clearUsersToolStripButton
    });
    this.toolStrip4.Location = new Point(453, 25);
    this.toolStrip4.Name = "toolStrip4";
    this.toolStrip4.Size = new Size(464, 25);
    this.toolStrip4.TabIndex = 7;
    this.toolStrip4.Text = "toolStrip4";
    this.toolStrip5.CanOverflow = false;
    this.toolStrip5.Dock = DockStyle.None;
    this.toolStrip5.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.toolStripLabel5,
      (ToolStripItem) this._fromToolStripDateTimePicker,
      (ToolStripItem) this.toolStripLabel6,
      (ToolStripItem) this._toToolStripDateTimePicker
    });
    this.toolStrip5.Location = new Point(0, 50);
    this.toolStrip5.Name = "toolStrip5";
    this.toolStrip5.Size = new Size(464, 26);
    this.toolStrip5.TabIndex = 8;
    this.toolStrip5.Text = "toolStrip5";
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip1);
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip2);
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip3);
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip4);
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip5);
    this.flowLayoutPanel1.Controls.Add((Control) this.toolStrip6);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.Location = new Point(3, 3);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(1078, 76);
    this.flowLayoutPanel1.TabIndex = 4;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._grid, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.statusStrip1, 0, 2);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 3;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(1084, 473);
    this.tableLayoutPanel1.TabIndex = 5;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (AttributeChangeHistoryControl);
    this.Size = new Size(1084, 473);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    ((ISupportInitialize) this._grid).EndInit();
    this._contextMenuStrip.ResumeLayout(false);
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.toolStrip6.ResumeLayout(false);
    this.toolStrip6.PerformLayout();
    this.toolStrip2.ResumeLayout(false);
    this.toolStrip2.PerformLayout();
    this.toolStrip3.ResumeLayout(false);
    this.toolStrip3.PerformLayout();
    this.toolStrip4.ResumeLayout(false);
    this.toolStrip4.PerformLayout();
    this.toolStrip5.ResumeLayout(false);
    this.toolStrip5.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }

  [Serializable]
  public sealed class AttributeChangeHistoryControlMemento
  {
    public int[] AttributeTypeIds { get; set; }

    public int[] ObjectTypeIds { get; set; }

    public int[] RelationTypeIds { get; set; }

    public long[] UserVersionIds { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public Tuple<string, int>[] Columns { get; set; }

    public Tuple<string, iGSortOrder>[] ColumnsSortObject { get; set; }

    public Tuple<string, iGSortOrder>[] ColumnsGroupObject { get; set; }
  }
}
