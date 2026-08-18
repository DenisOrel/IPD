// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Document.UI;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

public class VirtualTreeList : Infralution.Controls.VirtualTree.VirtualTree
{
  private int updateCount;
  private List<IVirtualTreeItem> expandedList = new List<IVirtualTreeItem>();
  private ArrayList selectedList = new ArrayList();
  private int lockSelection;
  private AVSTreeCheckBox checkBox = new AVSTreeCheckBox();
  private AVSUniversalEditBox avsuniversalEditBox = new AVSUniversalEditBox();
  /// <summary>Список редакторов</summary>
  private Dictionary<string, CellEditor> editors = new Dictionary<string, CellEditor>();
  private Rectangle dragBoxFromMouseDown;
  private AVSWindow avsWindow;

  public VirtualTreeList()
  {
    if (!this.IsDesignerHosted())
      this.UpdateEditors();
    this.ShowRootRow = true;
    this.ShowRowHeaders = false;
    this.AllowDrop = true;
    this.HorzScrollBar.LocationChanged += new EventHandler(this.HorzScrollBar_LocationChanged);
  }

  public void CopySelectedToExcel()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (object selectedItem in (IEnumerable) this.SelectedItems)
    {
      if (selectedItem is IVirtualTreeItem)
      {
        foreach (AVSColumn column in this.Columns)
        {
          if (!(column.Name == "AVS.Status"))
          {
            CellData data = new CellData((Column) column);
            (selectedItem as IVirtualTreeItem).GetCellData(column, data);
            object obj = data.Value;
            if (data.TypeConverter != null)
              obj = (object) data.TypeConverter.ConvertToString(obj);
            string source = Convert.ToString(obj);
            if ((selectedItem as IVirtualTreeItem).HeaderRow && (column.Tag == null || column.Tag.AttributeID != AvsIDCache.Attr_Name))
              source = "";
            if (source.All<char>((Func<char, bool>) (x => char.IsDigit(x) || x == '.')) && source.Contains<char>('.'))
              stringBuilder.AppendFormat("=\"{0}\"\t", (object) source);
            else
              stringBuilder.AppendFormat("\"{0}\"\t", (object) source);
          }
        }
        stringBuilder.AppendFormat("\n");
      }
    }
    string text = stringBuilder.ToString();
    if (string.IsNullOrWhiteSpace(text))
      return;
    Clipboard.SetText(text, TextDataFormat.UnicodeText);
  }

  private void HorzScrollBar_LocationChanged(object sender, EventArgs e) => this.UpdateScrolls();

  public override bool CompleteEdit() => base.CompleteEdit();

  protected override CellWidget EditWidget
  {
    get => base.EditWidget;
    set
    {
      CellWidget editWidget = this.EditWidget;
      if (editWidget != value && value != null)
      {
        if (value.CellData.Editor.Control is AVSTreeCheckBox)
          this.SelectedRow = value.Row;
        this.FocusRow = value.Row;
      }
      if (value != base.EditWidget)
      {
        base.EditWidget = value;
        if (this.EditWidget != null)
        {
          AVSTreeCheckBox control = this.EditWidget.CellData.Editor.Control as AVSTreeCheckBox;
        }
      }
      if (editWidget == value)
        return;
      this.OnFocusedCellChanged(EventArgs.Empty);
    }
  }

  public override ContextMenuStrip CreateHeaderContextMenu(bool addToContainer)
  {
    ContextMenuStrip headerContextMenu = base.CreateHeaderContextMenu(addToContainer);
    if (headerContextMenu != null)
    {
      foreach (object obj in (ArrangedElementCollection) headerContextMenu.Items)
      {
        if (obj is ToolStripItem)
          (obj as ToolStripItem).Visible = false;
      }
    }
    return headerContextMenu;
  }

  public override ContextMenuStrip HeaderContextMenu
  {
    get => base.HeaderContextMenu;
    set => base.HeaderContextMenu = value;
  }

  public override void ShowHeaderContextMenu(Column column)
  {
    if (this.PinnedMenuItem != null && column != null)
    {
      AVSColumn avsColumn = column as AVSColumn;
      this.PinnedMenuItem.Visible = avsColumn.Tag != null && avsColumn.Tag.SpecRowAttributeInfo != null;
    }
    base.ShowHeaderContextMenu(column);
  }

  protected override void HookHeaderContextMenuItems()
  {
    if (this.PinnedMenuItem != null)
      this.PinnedMenuItem.Click -= new EventHandler(((Infralution.Controls.VirtualTree.VirtualTree) this).OnPinnedMenuClicked);
    this.PinnedMenuItem = this.FindHeaderMenuItem("pinnedMenuItem");
    if (this.PinnedMenuItem == null)
      return;
    this.PinnedMenuItem.Text = "Закреплен";
    this.PinnedMenuItem.Visible = true;
    this.PinnedMenuItem.Click += new EventHandler(((Infralution.Controls.VirtualTree.VirtualTree) this).OnPinnedMenuClicked);
  }

  protected override void OnPinnedMenuClicked(object sender, EventArgs e)
  {
    AVSColumn contextMenuColumn = this.ContextMenuColumn as AVSColumn;
    if (contextMenuColumn.Tag == null || contextMenuColumn.Tag.SpecRowAttributeInfo == null)
      return;
    base.OnPinnedMenuClicked(sender, e);
    AvsRowAttributeInfo rowAttributeInfo = contextMenuColumn.Tag.SpecRowAttributeInfo;
    rowAttributeInfo.Pinned = this.ContextMenuColumn.Pinned;
    if (rowAttributeInfo.AttributeId != AvsIDCache.Attr_Count)
      return;
    foreach (AVSColumn column in this.Columns)
    {
      if (column.Tag != null && column.Tag.SpecRowAttributeInfo != null && column.Tag.SpecRowAttributeInfo.AttributeId == AvsIDCache.Attr_Count)
        column.Pinned = this.ContextMenuColumn.Pinned;
    }
  }

  protected override void OnColumnsChanged(object sender, ListChangedEventArgs e)
  {
    base.OnColumnsChanged(sender, e);
  }

  /// <summary>Очистить колонки</summary>
  public void ClearColumns() => this.Columns.Clear();

  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    this.UpdateScrolls();
  }

  protected override void OnLocationChanged(EventArgs e)
  {
    base.OnLocationChanged(e);
    this.UpdateScrolls();
  }

  protected override void OnLayout(LayoutEventArgs e) => base.OnLayout(e);

  /// <summary>Обновляем положение скроллбаров</summary>
  private void UpdateScrolls()
  {
    if (this.AVSWindow == null)
      return;
    int height1 = this.ClientSize.Height;
    int width = this.ClientSize.Width;
    if (this.ShowHorzScroll)
    {
      int height2 = this.HorzScrollBar.Height;
    }
    int num = this.ShowVertScroll ? width - this.VertScrollBar.Width : width;
    int right = this.AVSWindow.viewSwitch1.Right;
    Rectangle rectangle = this.RtlTranslateRect(new Rectangle(right, height1 - this.HorzScrollBar.Height, num - right, this.HorzScrollBar.Height));
    this.HorzScrollBar.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  /// <summary>Активный редактор</summary>
  public Control Editor
  {
    get
    {
      return this.EditWidget is SpecRow_CellWidget ? (this.EditWidget as SpecRow_CellWidget).Editor : (Control) null;
    }
  }

  /// <summary>Активный TextBox редактор</summary>
  public TextBox TextEditor
  {
    get
    {
      return this.Editor is AVSUniversalEditBox ? (this.Editor as AVSUniversalEditBox).TextBox : (TextBox) null;
    }
  }

  /// <summary>Обновить содержимое строки</summary>
  /// <param name="item"></param>
  public void RefreshRow(IVirtualTreeItem item)
  {
    Row row = this.FindRow((object) item);
    if (row == null)
      return;
    this.UpdateRowData(row);
  }

  /// <summary>Обновить строку и все дочерние</summary>
  /// <param name="item"></param>
  public void RefreshRows(IVirtualTreeItem item)
  {
    if (this.updateCount == 0)
    {
      this.SaveExpanded();
      this.SaveSelection();
    }
    Row row = this.FindRow((object) item);
    if (row != null)
      row.UpdateChildren(true, true);
    else if (this.RootRow != null)
      this.RootRow.UpdateChildren(true, true);
    if (this.updateCount != 0)
      return;
    this.RestoreExpanded();
    this.RestoreSelection();
  }

  /// <summary>Начать обновление</summary>
  public void BeginUpdate()
  {
    if (this.updateCount == 0)
    {
      this.SaveExpanded();
      this.SaveSelection();
      this.BeginInit();
    }
    ++this.updateCount;
  }

  /// <summary>Закончить обновление</summary>
  public void EndUpdate()
  {
    if (this.updateCount > 0)
      --this.updateCount;
    if (this.updateCount != 0)
      return;
    this.EndInit();
    this.RestoreExpanded();
    this.RestoreSelection();
    this.TopRow = this.SelectedRow;
  }

  /// <summary>Сфокусированная строка</summary>
  public object FocusedItem
  {
    get => this.SelectedItem;
    set => this.SelectedItem = value;
  }

  /// <summary>Выделенные строки</summary>
  public IList Selection
  {
    get => this.SelectedItems;
    set => this.SelectedItems = value;
  }

  /// <summary>Сохраняем список развернутых строк</summary>
  public void SaveExpanded()
  {
    if (this.RootRow == null || this.expandedList != null && this.Initializing)
      return;
    this.expandedList = this.GetExpandedItems(this, this.RootRow);
  }

  /// <summary>Раскрыть к строке</summary>
  /// <param name="item"></param>
  public void ExpandTo(IVirtualTreeItem item)
  {
    for (Row row = this.FindRow((object) item); row != null; row = row.ParentRow)
      row.Expand();
  }

  /// <summary>Восстанавливаем раскрытые узлы</summary>
  public void RestoreExpanded()
  {
    if (this.Initializing)
      return;
    if (this.expandedList != null)
    {
      for (int index = 0; index < this.expandedList.Count; ++index)
        this.ExpandTo(this.expandedList[index]);
    }
    else
      this.ExpandAll();
    this.SaveExpanded();
  }

  public event EventHandler FocusedCellChanged;

  public virtual void OnFocusedCellChanged(EventArgs e)
  {
    EventHandler focusedCellChanged = this.FocusedCellChanged;
    if (focusedCellChanged == null)
      return;
    focusedCellChanged((object) this, e);
  }

  /// <summary>Выделенаая колонка (Пока заглушка)</summary>
  public AVSColumn FocusedColumn
  {
    get
    {
      return this.EditWidget is SpecRow_CellWidget editWidget ? editWidget.Column as AVSColumn : (AVSColumn) null;
    }
  }

  /// <summary>Запоминаем выделение</summary>
  public void SaveSelection()
  {
    this.selectedList = new ArrayList();
    foreach (object selectedItem in (IEnumerable) this.SelectedItems)
    {
      if (selectedItem is IVirtualTreeItem)
        this.selectedList.Add((object) (selectedItem as IVirtualTreeItem));
    }
  }

  protected override void OnSelectionChanged()
  {
    if (this.lockSelection != 0 || this.updateCount != 0)
      return;
    base.OnSelectionChanged();
  }

  public override IList SelectedItems
  {
    get => base.SelectedItems;
    set
    {
      bool flag = this.SelectedItems != null && value != null;
      if (flag)
      {
        flag = this.SelectedItems.Count == value.Count;
        if (flag)
        {
          for (int index = 0; index < this.SelectedItems.Count; ++index)
          {
            if (this.SelectedItems[index] != value[index])
              flag = false;
          }
        }
      }
      if (!flag)
      {
        foreach (object obj in (IEnumerable) value)
          this.ExpandTo(obj as IVirtualTreeItem);
        base.SelectedItems = value;
      }
      else
      {
        ++this.lockSelection;
        base.SelectedItems = value;
        --this.lockSelection;
      }
    }
  }

  /// <summary>Восстанавливаем выделение</summary>
  public void RestoreSelection()
  {
    if (this.selectedList == null)
      return;
    this.SelectedItems = (IList) this.selectedList;
    this.selectedList = (ArrayList) null;
  }

  /// <summary>Очистить дерево</summary>
  public void ClearAll() => this.DataSource = (object) null;

  /// <summary>Получить все дочерние развернутые IVirtualTreeItem текущего IVirtualTreeItem</summary>
  /// <param name="item">Текущий элемент</param>
  /// <returns></returns>
  private List<IVirtualTreeItem> GetExpandedItems(Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList tree, Row item)
  {
    List<IVirtualTreeItem> expandedItems1 = new List<IVirtualTreeItem>();
    if (item.ChildItems != null && item.ChildItems.Count != 0)
    {
      for (int index = 0; index < item.ChildItems.Count; ++index)
      {
        Row row = tree.FindRow(item.ChildItems[index]);
        if (row != null)
        {
          List<IVirtualTreeItem> expandedItems2 = this.GetExpandedItems(tree, row);
          if (expandedItems2.Count != 0)
            expandedItems1.AddRange((IEnumerable<IVirtualTreeItem>) expandedItems2);
        }
      }
      if (item.Expanded)
      {
        expandedItems1.Add(item.Item as IVirtualTreeItem);
        if (item.Item is AVSRow && (item.Item as AVSRow).IsDynamicGroupHeaderRow && item.ChildItems.Count > 0 && item.ChildItems[0] is IVirtualTreeItem)
          expandedItems1.Add(item.ChildItems[0] as IVirtualTreeItem);
      }
    }
    return expandedItems1;
  }

  /// <summary>Показать редактор (Заглушка)</summary>
  /// <param name="col"></param>
  public void ShowEditor(AVSColumn col)
  {
  }

  /// <summary>Обновить данные</summary>
  /// <param name="item">Корневой элемент дерева</param>
  public void UpdateData(IVirtualTreeItem item)
  {
    this.SaveExpanded();
    this.DataSource = (object) item;
    if (this.expandedList.Count != 0)
      this.RestoreExpanded();
    else
      this.ExpandAll();
  }

  public override void UpdateRows(bool reloadChildren) => base.UpdateRows(reloadChildren);

  protected override CellWidget CreateCellWidget(RowWidget rowWidget, Column column)
  {
    switch (rowWidget)
    {
      case TreeHeaderRowWidget _:
        return (CellWidget) new TreeHeaderCellWidget(rowWidget, column);
      case SpecRow_RowWidget _:
        return column.Name == "AVS.Status" ? (CellWidget) new SpecRowStatus_CellWidget(rowWidget, column) : (CellWidget) new SpecRow_CellWidget(rowWidget, column);
      default:
        return base.CreateCellWidget(rowWidget, column);
    }
  }

  /// <summary>Развернуть все</summary>
  public void ExpandAll()
  {
    if (this.RootRow == null)
      return;
    this.RootRow.ExpandChildren(true);
  }

  protected override RowWidget CreateRowWidget(PanelWidget panelWidget, Row row)
  {
    return !(row.Item as IVirtualTreeItem).HeaderRow ? (RowWidget) new SpecRow_RowWidget(panelWidget, row) : (RowWidget) new TreeHeaderRowWidget(panelWidget, row);
  }

  public void UpdateEditors()
  {
    this.editors["AVSUniversalEditBox"] = (CellEditor) new AVSCellEditor((Control) this.avsuniversalEditBox);
    this.checkBox.Size = new Size(14, 14);
    this.checkBox.BackColor = Color.Transparent;
    this.checkBox.Width = 13;
    this.checkBox.Height = 13;
    this.checkBox.FlatStyle = FlatStyle.System;
    AVSCellEditor avsCellEditor = new AVSCellEditor((Control) this.checkBox);
    avsCellEditor.ValuePropertyName = "Checked";
    avsCellEditor.UseCellWidth = false;
    avsCellEditor.UseCellHeight = false;
    avsCellEditor.UseCellFont = false;
    avsCellEditor.UseCellColors = false;
    avsCellEditor.CellAlignment = ContentAlignment.MiddleCenter;
    avsCellEditor.DisplayMode = CellEditorDisplayMode.Always;
    this.editors["CheckEdit"] = (CellEditor) avsCellEditor;
  }

  private void cchEdit_SetControlValue(object sender, CellEditorSetValueEventArgs e)
  {
    this.checkBox.Checked = Convert.ToBoolean(e.Value);
  }

  private void cchEdit_GetControlValue(object sender, CellEditorGetValueEventArgs e)
  {
    e.Value = (object) (e.Control as CheckBox).Checked;
  }

  protected override bool SetValueForCell(
    Row row,
    Column column,
    object oldValue,
    object newValue)
  {
    try
    {
      if (row.Item is AVSRow)
      {
        ColumnTag tag = (column as AVSColumn).Tag;
        TreeSpecRowConverter specRowConverter = new TreeSpecRowConverter(tag.SpecRowAttributeInfo.AttributeId)
        {
          AVSWindow = this.AVSWindow,
          Tag = tag
        };
        if (specRowConverter.CanConvertFrom(row.Item as AVSRow))
        {
          specRowConverter.SetValueToSpecificationRow(row.Item as AVSRow, newValue);
          this.RefreshRow((IVirtualTreeItem) (row.Item as AVSRow));
        }
        else
          this.RefreshRow((IVirtualTreeItem) (row.Item as AVSRow));
        return true;
      }
    }
    catch (Exception ex)
    {
      return false;
    }
    return false;
  }

  protected override void OnGetCellData(Row row, Column column, CellData cellData)
  {
    try
    {
      if (row.Item is IVirtualTreeItem)
        (row.Item as IVirtualTreeItem).GetCellData(column as AVSColumn, cellData);
      if (!(row.Item is AVSRow avsRow))
        return;
      ColumnTag tag = (column as AVSColumn).Tag;
      if (tag == null || avsRow.IsDynamicGroupHeaderRow)
        return;
      AvsRowAttributeInfo rowAttributeInfo = tag.SpecRowAttributeInfo;
      if (rowAttributeInfo == null)
        return;
      if (rowAttributeInfo.FieldType != FieldTypes.ftBoolean)
      {
        cellData.TypeConverter = (TypeConverter) new TreeSpecRowConverter(rowAttributeInfo.AttributeId)
        {
          AVSWindow = this.AVSWindow,
          Tag = tag
        };
        cellData.TypeEditor = (UITypeEditor) new TreeSpecRowEditor(rowAttributeInfo.AttributeId)
        {
          AVSWindow = this.AVSWindow,
          Tag = tag
        };
        cellData.Editor = this.editors["AVSUniversalEditBox"];
      }
      else
      {
        cellData.TypeConverter = (TypeConverter) new TreeSpecRowConverter(rowAttributeInfo.AttributeId)
        {
          AVSWindow = this.AVSWindow,
          Tag = tag
        };
        cellData.TypeEditor = (UITypeEditor) new TreeSpecRowEditor(rowAttributeInfo.AttributeId)
        {
          AVSWindow = this.AVSWindow,
          Tag = tag
        };
        if (cellData.Value == (object) "см. по исполнениям")
          return;
        cellData.Editor = this.editors["CheckEdit"];
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected override void OnGetRowData(Row row, RowData rowData)
  {
    if (!(row.Item is IVirtualTreeItem))
      return;
    (row.Item as IVirtualTreeItem).GetRowData(rowData);
  }

  protected override IList GetChildrenForRow(Row row)
  {
    if (row.Item is IVirtualTreeItem)
    {
      List<IVirtualTreeItem> treeChildren = (row.Item as IVirtualTreeItem).GetTreeChildren();
      if (treeChildren != null)
      {
        List<IVirtualTreeItem> childrenForRow = new List<IVirtualTreeItem>();
        foreach (IVirtualTreeItem virtualTreeItem in treeChildren)
        {
          if (virtualTreeItem.CanTreeShow())
            childrenForRow.Add(virtualTreeItem);
        }
        return (IList) childrenForRow;
      }
    }
    return (IList) null;
  }

  protected override object GetParentForItem(object item)
  {
    return item is IVirtualTreeItem ? (object) (item as IVirtualTreeItem).ParentItem : (object) null;
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    Size dragSize = SystemInformation.DragSize;
    this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    int num = this.MouseCaptureWidget is ColumnDividerWidget ? 1 : 0;
    base.OnMouseUp(e);
    this.dragBoxFromMouseDown = Rectangle.Empty;
    if (num == 0)
      return;
    this.PerformLayout();
  }

  internal bool CanShowContextMenu()
  {
    return !(this.MouseDownWidget is ColumnHeaderWidget) && !(this.MouseDownWidget is ColumnDividerWidget);
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (e.Button != MouseButtons.Left || !(this.FocusedItem is AVSRow) || this.Selection.Count != 1 || !(this.MouseDownWidget is SpecRow_CellWidget) && !(this.MouseDownWidget is SpecRow_RowWidget) && !(this.MouseDownWidget is SpecRowStatus_CellWidget) || !(this.dragBoxFromMouseDown != Rectangle.Empty) || this.dragBoxFromMouseDown.Contains(e.X, e.Y))
      return;
    List<IVirtualTreeItem> data = new List<IVirtualTreeItem>();
    foreach (object obj in (IEnumerable) this.Selection)
    {
      if (obj is IVirtualTreeItem)
        data.Add(obj as IVirtualTreeItem);
    }
    int num = (int) this.DoDragDrop((object) data, DragDropEffects.Move);
  }

  protected override void OnDragDrop(DragEventArgs e) => base.OnDragDrop(e);

  protected override void OnDragOver(DragEventArgs e) => base.OnDragOver(e);

  protected override RowDropLocation AllowedRowDropLocations(Row row, IDataObject data)
  {
    return row.Item is AVSRow ? RowDropLocation.AboveRow | RowDropLocation.BelowRow : RowDropLocation.OnRow;
  }

  protected override bool AllowRowDrag(Row row) => base.AllowRowDrag(row);

  protected override DragDropEffects RowDropEffect(
    Row row,
    RowDropLocation dropLocation,
    IDataObject data)
  {
    List<IVirtualTreeItem> data1 = data.GetData(typeof (List<IVirtualTreeItem>)) as List<IVirtualTreeItem>;
    Row row1 = row;
    if (row1 == null || row1.Item == null)
      return DragDropEffects.None;
    IOSource data2 = data.GetData(typeof (IOSource)) as IOSource;
    data.GetData(typeof (DragNotesWrapper));
    DragDropEffects dragDropEffects = DragDropEffects.None;
    if (data1 != null)
    {
      if (data1[0] is AVSRow)
      {
        AVSRow avsRow1 = data1[0] as AVSRow;
        if (!(row1.Item is SpecificationSection section))
        {
          if (row1.ParentRow != null)
            section = row1.ParentRow.Item as SpecificationSection;
          else if (row1.Item is AVSRow avsRow2)
            section = avsRow2.Section;
        }
        if (!AvsConfig.General.AutoSort || avsRow1.IsNoteRow)
        {
          if (this.AVSWindow.AVSDocument.IsElementList)
            dragDropEffects = DragDropEffects.Move;
          else if (section != null && section == avsRow1.Section)
            dragDropEffects = DragDropEffects.Move;
        }
      }
    }
    else if (data2 != null && data2.SelectedItems != null && data2.SelectedItems.Count > 0)
      dragDropEffects = !this.AVSWindow.CanAddNodes(this.AVSWindow.AVSDocument.GetDocNode(row1.Item as IVirtualTreeItem), data2.SelectedItems) ? DragDropEffects.None : DragDropEffects.Copy;
    return dragDropEffects;
  }

  protected override void OnRowDrop(
    Row row,
    RowDropLocation dropLocation,
    IDataObject data,
    DragDropEffects dropEffect)
  {
    PageControl pageControl = this.AVSWindow.DocumentControl.PageControl;
    List<IVirtualTreeItem> data1 = data.GetData(typeof (List<IVirtualTreeItem>)) as List<IVirtualTreeItem>;
    Row row1 = row;
    if (row1 == null || row1.Item == null)
      return;
    IOSource data2 = data.GetData(typeof (IOSource)) as IOSource;
    data.GetData(typeof (DragNotesWrapper));
    if (data1 != null)
    {
      List<DocumentTreeNode> selectedDocNodes = (List<DocumentTreeNode>) null;
      if (!(row1.Item is SpecificationSection section))
      {
        if (row1.ParentRow != null && row1.ParentRow.Item is SpecificationSection)
          section = row1.ParentRow.Item as SpecificationSection;
        else if (row1.Item is AVSRow avsRow)
          section = avsRow.Section;
      }
      bool setAfter = dropLocation == RowDropLocation.BelowRow;
      if (data1[0] is AVSRow row2 && section != null)
      {
        AVSRow toRow = row1.Item as AVSRow;
        if (toRow != row2)
        {
          section.MoveRow(row2, toRow, setAfter);
          selectedDocNodes = new List<DocumentTreeNode>();
          selectedDocNodes.Add((DocumentTreeNode) row2.DocNode);
        }
      }
      if (selectedDocNodes != null && section != null && section.DocNode != null)
      {
        int index1 = 1;
        DocumentTreeNode documentTreeNode = (DocumentTreeNode) section.DocNode;
        for (int index2 = selectedDocNodes.Count - 1; index2 >= 0; --index2)
        {
          DocumentTreeNode child = selectedDocNodes[index2];
          if (row1.Item is SpecificationSection)
          {
            index1 = 1;
          }
          else
          {
            DocumentTreeNode docNode = this.AVSWindow.AVSDocument.GetDocNode(row1.Item as IVirtualTreeItem);
            if (docNode != null)
            {
              index1 = docNode.Index;
              documentTreeNode = docNode.Parent;
            }
          }
          documentTreeNode?.InsertChildNode(index1, child, false, true, false, false);
        }
        this.AVSWindow.Document.UpdateLayout(false, true);
        this.AVSWindow.AVSDocument.IndexAVSDocument(true);
        this.AVSWindow.AVSDocument.UpdateViewNodes(false, true, false, false, false, EmptyRowUpdateMode.DontChange);
        if (selectedDocNodes.Count > 0)
          this.AVSWindow.RestoreListSelection(selectedDocNodes, selectedDocNodes[0]);
      }
    }
    else if (data2 != null && data2.SelectedItems != null && data2.SelectedItems.Count > 0)
    {
      DocumentTreeNode docNode = this.AVSWindow.AVSDocument.GetDocNode(row1.Item as IVirtualTreeItem);
      pageControl.DocumentControl.SetSelection(docNode, false, false);
      List<object> objectList = new List<object>();
      for (int index = 0; index < data2.SelectedItems.Count; ++index)
        objectList.Add((object) data2.SelectedItems.GetItemID(index));
      this.AVSWindow.ContextAddSpecRow(docNode, AvsIDCache.Relation_Project, objectList.ToArray());
      this.AVSWindow.AVSDocument.IndexAVSDocument(true);
      this.AVSWindow.AVSDocument.UpdateViewNodes(false, true, false, false, false, EmptyRowUpdateMode.DontChange);
    }
    if (AVSPlugin.Instance.CommandManager == null)
      return;
    AVSPlugin.Instance.CommandManager.QueryStatus();
  }

  public AVSWindow AVSWindow
  {
    get => this.avsWindow;
    set => this.avsWindow = value;
  }

  public void ReCreateGridColumns()
  {
  }
}
