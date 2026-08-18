// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TableElement
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model.UI;
using Intermech.Document.Model.UI.Extensions;
using Intermech.Document.Model.Undo;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Базовый класс таблицы</summary>
/// <remarks>
/// Таблица может содержать:
/// - наследник RectangleElement если это визуальная таблица
/// - TextData, если это таблица данных.
///  </remarks>
[Serializable]
public class TableElement : 
  TableData,
  IFlowElement,
  IParentFlow,
  INodeWithReference,
  IPageElementWithInterface
{
  /// <summary>Имя типа элемента</summary>
  public new static string ElementTypeName = LocalizationHolder.rm.GetString("Document.Model_510");
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  [NonSerialized]
  private PageElementUI pageUI;

  /// <summary>Вставить готовую колонку в таблицу</summary>
  /// <param name="columnIndex">Индекс колонки в гриде</param>
  /// <param name="virtualColumn">Виртуальная колонка с ячейками</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void InsertColumn(
    int columnIndex,
    VirtualColumn virtualColumn,
    bool updateUI,
    bool updateLayout)
  {
    if (virtualColumn == null)
      throw new ArgumentNullException(nameof (virtualColumn));
    if (!this.IsColumnGridOwner())
      return;
    bool flag1 = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag1)
      this.SuspendUpdateLayout();
    bool flag2 = this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag2)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      RowColParams rowColParams = (RowColParams) null;
      if (virtualColumn.ColumnParams != null)
      {
        rowColParams = virtualColumn.ColumnParams.Clone();
      }
      else
      {
        if (virtualColumn.Nodes.Count > 0)
        {
          if (virtualColumn.Nodes[0] is RectangleElement node2)
            rowColParams = new RowColParams((TableData) null, -1, node2.Name, node2.Size.Width);
          else if (virtualColumn.Nodes[0] is VirtualColumnCell node1)
            rowColParams = new RowColParams((TableData) null, -1, node1.Name, node1.ColumnWidth);
        }
        if (rowColParams == null)
          rowColParams = new RowColParams((TableData) null, -1, (string) null, TableData.DefaultCellSize.Width);
      }
      if (this.gridColumnsParams == null)
      {
        if (this.Template is TableElement template && template.gridColumnsParams != null)
          this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(template.gridColumnsParams), true, true);
        else
          this.SetGridColumnsParams(new List<RowColParams>(), true, true);
      }
      rowColParams.ID = TableData.GenerateGridID(this.gridColumnsParams, rowColParams.ID);
      rowColParams.SetOwnerTable((TableData) this);
      List<RowColParams> rowColParamsList = new List<RowColParams>((IEnumerable<RowColParams>) this.gridColumnsParams);
      rowColParamsList.Insert(columnIndex, rowColParams);
      this.SetGridColumnsParams(rowColParamsList, true, true);
      int rowIndex = 0;
      this.InsertColumnCells(this.gridColumnsParams, columnIndex, virtualColumn, ref rowIndex, false, false);
      this.SetNeedUpdateLayoutFlag(true, true, false, false);
    }
    finally
    {
      if (!flag1)
        this.ResumeUpdateLayout(false, true);
      if (!flag2)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Вставить ячейки колонки в таблицу. Вызывается в InsertColumn</summary>
  /// <param name="gridColumns">Сетка</param>
  /// <param name="columnIndex">Индекс колонки в гриде</param>
  /// <param name="virtualColumn">Виртуальная колонка с ячейками</param>
  /// <param name="rowIndex">Индекс текущей строки, ячейка которой должна быть скопирована</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  private void InsertColumnCells(
    List<RowColParams> gridColumns,
    int columnIndex,
    VirtualColumn virtualColumn,
    ref int rowIndex,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsRow && this.GridColumnsParams == gridColumns)
    {
      RectangleElement[] sourceArray = (RectangleElement[]) null;
      if (rowIndex < virtualColumn.Nodes.Count)
      {
        if (virtualColumn.Nodes[rowIndex] is VirtualColumnCell node1)
        {
          sourceArray = new RectangleElement[node1.Nodes.Count];
          int length = 0;
          int index = 0;
          for (int count = node1.Nodes.Count; index < count; ++index)
          {
            if (node1.Nodes[index] is RectangleElement node)
              sourceArray[length++] = node;
          }
          if (length < node1.Nodes.Count)
          {
            RectangleElement[] destinationArray = new RectangleElement[length];
            Array.Copy((Array) sourceArray, (Array) destinationArray, length);
            sourceArray = destinationArray;
          }
        }
        else
          sourceArray = new RectangleElement[1]
          {
            (RectangleElement) virtualColumn.Nodes[rowIndex]
          };
      }
      ++rowIndex;
      int nodeCellIndex = this.GetCellPositionForGridColumn(columnIndex, true, out RectangleElement[] _);
      if (nodeCellIndex == -1)
        nodeCellIndex = this.nodes.Count;
      if (sourceArray == null)
        sourceArray = this.CreateGridColumnCells(gridColumns, columnIndex, nodeCellIndex, updateUI, updateLayout);
      if (sourceArray == null)
        return;
      for (int index = 0; index < sourceArray.Length; ++index)
        this.InsertChildNode(nodeCellIndex++, (DocumentTreeNode) sourceArray[index], false, true, updateUI, updateLayout, false);
    }
    else
    {
      int index = 0;
      for (int count = this.nodes.Count; index < count; ++index)
      {
        if (this.nodes[index] is TableElement node)
          node.InsertColumnCells(gridColumns, columnIndex, virtualColumn, ref rowIndex, updateUI, updateLayout);
      }
    }
  }

  /// <summary>Получить ссылки на ячейки заданного столбца</summary>
  /// <param name="gridColIndex">Индекс столбца в сетке</param>
  /// <param name="gridColumns">Сетка в которой находится столбец</param>
  /// <param name="columnCells">Возвращает ячейки заданного столбца.
  /// Для нескольких ячеек одного столбца в строке создает VirtualColumnCells</param>
  public override void GetGridColumnCells(
    int gridColIndex,
    List<RowColParams> gridColumns,
    IList<DocumentTreeNode> columnCells)
  {
    if (this.GridColumnsParams != gridColumns)
      return;
    if (this.IsColumn)
    {
      int index = 0;
      for (int count = this.nodes.Count; index < count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.GetGridColumnCells(gridColIndex, gridColumns, columnCells);
      }
    }
    else
    {
      RectangleElement[] cells;
      this.GetCellPositionForGridColumn(gridColIndex, false, out cells);
      if (cells == null)
        return;
      if (cells.Length == 1)
      {
        columnCells.Add((DocumentTreeNode) cells[0]);
      }
      else
      {
        VirtualColumnCell virtualColumnCell = new VirtualColumnCell(this, gridColumns[gridColIndex], cells);
        columnCells.Add((DocumentTreeNode) virtualColumnCell);
      }
    }
  }

  /// <inheritdoc cref="T:Intermech.Interfaces.Document.DocumentTreeNode" />
  public override void SynchronizeNodePositionWithUI(
    DocumentTreeNode node,
    int oldIndex,
    int newIndex)
  {
    this.UpdatePageElementChildPosition(node, oldIndex, newIndex);
  }

  public override Rectangle GetPixelBounds(DrawContext context)
  {
    return this.pageUI != null ? this.pageUI.Bounds : base.GetPixelBounds(context);
  }

  public override bool ShowFocused
  {
    get => this.pageUI != null ? this.pageUI.IsActiveElement : base.ShowFocused;
  }

  public override bool ShowSelected
  {
    get
    {
      if (this.IsVirtualNode)
        return true;
      return this.pageUI != null ? this.pageUI.IsSelected : base.ShowSelected;
    }
  }

  /// <summary>Обновить экранные координаты</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = false;
    if (this.needUI)
    {
      if (this.pageUI == null)
      {
        this.CreateUI();
        flag = true;
      }
      else if (this.parent is IPageElementWithInterface parent && parent.PageUI != null && this.pageUI.Parent != parent.PageUI)
        this.pageUI.Parent = parent.PageUI;
    }
    if (this.pageUI == null)
      return;
    int num = this.SuspendedRefreshUIFlag ? 1 : 0;
    if (num == 0)
      this.SuspendRefreshUI();
    this.InvalidateUI(this.pageUI.Bounds, true);
    if (this.needUpdateUIGeometry && !flag)
      this.pageUI.UpdateGeometry();
    base.UpdateUIGeometry(false);
    if (num != 0)
      return;
    this.ResumeRefreshUI(refreshUI);
  }

  /// <summary>Создание списка свойств отличающихся у единичных ячеек</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="curArray"></param>
  private void CellPropertiesForRemove(RectangleElement cell, List<string> properties)
  {
    if (cell == null)
      return;
    int num = 10;
    if (properties.Count == num)
      return;
    if (!cell.IsSingleCell)
    {
      this.CellPropertiesForRemove(cell.Nodes[0] as RectangleElement, properties);
      int index = 1;
      for (int count = cell.Nodes.Count; index < count && properties.Count != num; ++index)
        this.CellPropertiesForRemove(cell.Nodes[index] as RectangleElement, properties);
    }
    else
    {
      if (cell is LabelElement)
      {
        if (properties.IndexOf("OriginalSize") == -1)
          properties.Add("OriginalSize");
        if (properties.IndexOf("ScaleMode") == -1)
          properties.Add("ScaleMode");
        if (properties.IndexOf("AutoSizeHeight") == -1)
          properties.Add("AutoSizeHeight");
        if (properties.IndexOf("Image") == -1)
          properties.Add("Image");
      }
      if (cell is TextBoxElement)
      {
        if (properties.IndexOf("OriginalSize") == -1)
          properties.Add("OriginalSize");
        if (properties.IndexOf("ScaleMode") == -1)
          properties.Add("ScaleMode");
        if (properties.IndexOf("Orientation") == -1)
          properties.Add("Orientation");
        if (properties.IndexOf("FormattedText") == -1)
          properties.Add("FormattedText");
        if (properties.IndexOf("Image") == -1)
          properties.Add("Image");
      }
      if (!(cell is ContainerElement))
        return;
      if (properties.IndexOf("ParagraphFormat") == -1)
        properties.Add("ParagraphFormat");
      if (properties.IndexOf("Orientation") == -1)
        properties.Add("Orientation");
      if (properties.IndexOf("CharFormat") == -1)
        properties.Add("CharFormat");
      if (properties.IndexOf("AutoSizeHeight") == -1)
        properties.Add("AutoSizeHeight");
      if (properties.IndexOf("Text") == -1)
        properties.Add("Text");
      if (properties.IndexOf("TextFormat") == -1)
        properties.Add("TextFormat");
      if (properties.IndexOf("FormattedText") != -1)
        return;
      properties.Add("FormattedText");
    }
  }

  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (this.IsVirtualNode)
    {
      int num = 0;
      for (int index = 0; index < this.NodesCount; ++index)
      {
        if (!this.Nodes[index].IsVirtualNode && this.Nodes[index] is TableData && (this.Nodes[index] as TableData).IsRow)
          ++num;
      }
      if (num != this.NodesCount)
        this.RemoveProperty(properties, "VisibleTE");
      List<string> properties1 = new List<string>();
      this.CellPropertiesForRemove((RectangleElement) this, properties1);
      for (int index = 0; index < properties1.Count; ++index)
        this.RemoveProperty(properties, properties1[index]);
      this.RemoveProperty(properties, "GridColumnsParams");
      this.RemoveProperty(properties, "Template");
      this.RemoveProperty(properties, "UsePreviousTableTemplates");
      this.RemoveProperty(properties, "LeftMargin");
      this.RemoveProperty(properties, "TopMargin");
      this.RemoveProperty(properties, "BottomMargin");
      this.RemoveProperty(properties, "RightMargin");
      this.RemoveProperty(properties, "ContinuationTableIdTE");
    }
    else
    {
      this.RemoveProperty(properties, "VisibleTE");
      this.RemoveProperty(properties, "OriginalSize");
      this.RemoveProperty(properties, "ScaleMode");
      List<string> properties2 = new List<string>();
      this.CellPropertiesForRemove((RectangleElement) this, properties2);
      int index = 0;
      for (int count = properties2.Count; index < count; ++index)
        this.RemoveProperty(properties, properties2[index]);
    }
    if (this.HasTemplate())
    {
      this.RemoveProperty(properties, "GeometryChangingBlockedTE");
      if (properties[(object) "ReadOnlyTE"] is CustomPropertyDescriptor property1)
        property1.SetIsReadOnly(true);
      if (properties[(object) "TransparentTE"] is CustomPropertyDescriptor property2)
        property2.SetIsReadOnly(true);
      if (properties[(object) "DefaultRowSizeTE"] is CustomPropertyDescriptor property3)
        property3.SetIsReadOnly(true);
      if (properties[(object) "IsFixedSizeRowsTE"] is CustomPropertyDescriptor property4)
        property4.SetIsReadOnly(true);
      if (properties[(object) "FixedStructure"] is CustomPropertyDescriptor property5)
        property5.SetIsReadOnly(true);
    }
    if (properties[(object) "VisibleTE"] is CustomPropertyDescriptor)
      this.RemoveProperty(properties, "Visible");
    if (!this.IsTemplate || !this.IsAllowableLocalDataLink())
      this.RemoveProperty(properties, "ContinuationTableIdTE");
    if (!(this.OwnerDocument is ImDocument ownerDocument) || ownerDocument.DocumentControl == null || !ownerDocument.DocumentControl.ReadOnly)
      return;
    CustomPropertyDescriptor.SetReadOnlyProperties(properties);
  }

  /// <summary>Обновить мировые координаты элемента преобразовав экранные координаты</summary>
  public override void UpdateWorldCoor()
  {
    if (this.PageUI == null)
      return;
    int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateGeometryRefreshUI();
    this.PageUI.UpdateElementGeometry();
    if (num != 0)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  protected override bool GetShowSingleCellInTemplateGlobal()
  {
    return ImDocumentEditorConfig.Instance.ShowSingleCellInTemplate;
  }

  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  [Browsable(false)]
  [Category("Debug")]
  public PageElementUI PageUI
  {
    [DebuggerStepThrough] get => this.pageUI;
    set
    {
      if (this.pageUI == value)
        return;
      int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) null;
        this.pageUI.Parent = (PageElementUI) null;
      }
      this.pageUI = value;
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) this;
        if (this.Parent is VisualNode parent)
          parent.AddChildUI((DocumentTreeNode) this, false);
      }
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (num != 0)
        return;
      this.ResumeUpdateRefreshUI(this.pageUI != null, true);
    }
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public override void DestroyUI()
  {
    this.PageUI = (PageElementUI) null;
    base.DestroyUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(bool force)
  {
    if (!force && this.SuspendedRefreshUIFlag || this.pageUI == null)
      return;
    if (this.page != null)
      this.page.InvalidateUI(this.pageUI.Bounds);
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public override void InvalidateUI(Rectangle clipRectangle)
  {
    this.InvalidateUI(clipRectangle, false);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(Rectangle clipRectangle, bool force)
  {
    if (this.SuspendedRefreshUIFlag && !force)
      return;
    if (this.page != null)
      this.page.InvalidateUI(clipRectangle);
    if (this.pageUI == null)
      return;
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    if (this.pageUI != null)
      this.RefreshUI(this.pageUI.Bounds);
    else
      this.page.RefreshUI();
  }

  /// <summary>Можно активировать редактирование по месту</summary>
  public override bool CanActivateInPlaceEditor => base.CanActivateInPlaceEditor;

  /// <summary>Создать объекты интерфейса пользователя.
  /// Должен быть перекрыт в наследнике.</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.needUI && this.pageUI == null)
    {
      if (!(this.parent is Intermech.Document.Model.Page parent2))
      {
        if (!(this.parent is IPageElementWithInterface parent1) || parent1.PageUI == null)
          return;
      }
      else if (parent2.PageUI == null)
        return;
      TableData parentCell = this.ParentCell;
      this.PageUI = parentCell != null ? (!parentCell.IsFixedStructureArea ? (PageElementUI) new TableCellUI() : (PageElementUI) new RectanglePageElementUI()) : (PageElementUI) new TableUI();
    }
    base.CreateUI();
  }

  /// <summary>Добавить и связать объекты интерфейса пользователя</summary>
  /// <param name="child">Дочерний узел</param>
  public override void AddChildUI(DocumentTreeNode child, bool createUI)
  {
    if (this.IsVirtualNode)
    {
      base.AddChildUI(child, createUI);
    }
    else
    {
      if (child == null || this.PageUI == null)
        return;
      if (child is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      {
        elementWithInterface.PageUI.Parent = this.PageUI;
      }
      else
      {
        VisualNode visualNode = child as VisualNode;
        if (visualNode != null & createUI)
          visualNode.CreateUI();
      }
      base.AddChildUI(child, createUI);
    }
  }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  public void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
  }

  /// <summary>Событие перед активацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorActivating
  {
    add => this.inplaceEditorActivating += value;
    remove => this.inplaceEditorActivating -= value;
  }

  /// <summary>Событие после активации редактора по месту</summary>
  public event EventHandler InplaceEditorActivated
  {
    add => this.inplaceEditorActivated += value;
    remove => this.inplaceEditorActivated -= value;
  }

  /// <summary>Событие перед деактивацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorDeactivating
  {
    add => this.inplaceEditorDeactivating += value;
    remove => this.inplaceEditorDeactivating -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event EventHandler InplaceEditorDeactivated
  {
    add => this.inplaceEditorDeactivated += value;
    remove => this.inplaceEditorDeactivated -= value;
  }

  /// <summary>Контрол редактора по месту</summary>
  [Browsable(false)]
  public Control InPlaceEditorControl
  {
    [DebuggerStepThrough] get => (Control) null;
  }

  public override float? WidthForUser
  {
    get
    {
      return this.IsVirtualNode ? this.GetWidthForUser((RectangleElement) this, new float?()) : base.WidthForUser;
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetWidthForUser(value.Value, (RectangleElement) this);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.WidthForUser = new float?(value.Value);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override float? HeightForUser
  {
    get
    {
      return this.IsVirtualNode ? this.GetHeightForUser((RectangleElement) this, new float?()) : base.HeightForUser;
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetHeightForUser(value.Value, (RectangleElement) this);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.HeightForUser = new float?(value.Value);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override float? LeftForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.LeftForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.X = this.GetLeftForUser((RectangleElement) this, float.MaxValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.X);
    }
  }

  public override RectangleF Bounds
  {
    get
    {
      if (this.IsVirtualNode)
      {
        int num = this.IsSingleCell ? 1 : 0;
      }
      if (!this.IsVirtualNode || this.IsSingleCell)
        return base.Bounds;
      PageData pg = (PageData) null;
      float rightForUser = this.GetRightForUser((RectangleElement) this, float.MinValue, ref pg);
      double leftForUser = (double) this.GetLeftForUser((RectangleElement) this, float.MaxValue, ref pg);
      float topForUser = this.GetTopForUser((RectangleElement) this, float.MaxValue, ref pg);
      float bottomForUser = this.GetBottomForUser((RectangleElement) this, float.MinValue, ref pg);
      double top = (double) topForUser;
      double right = (double) rightForUser;
      double bottom = (double) bottomForUser;
      return RectangleF.FromLTRB((float) leftForUser, (float) top, (float) right, (float) bottom);
    }
    set
    {
      if (this.IsVirtualNode)
        return;
      base.Bounds = value;
    }
  }

  public override float? RightForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.RightForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.X = this.GetRightForUser((RectangleElement) this, float.MinValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.X);
    }
  }

  public override float? BottomForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.BottomForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.Y = this.GetBottomForUser((RectangleElement) this, float.MinValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
  }

  public override float? TopForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.TopForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.Y = this.GetTopForUser((RectangleElement) this, float.MaxValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
  }

  public override string Name
  {
    get => this.IsVirtualNode ? this.GetName((RectangleElement) this, (string) null) : base.Name;
    set
    {
      if (value == null)
        return;
      if (this.IsVirtualNode)
        this.SetNameRecurcive((RectangleElement) this, value, true, true);
      else
        base.Name = value;
    }
  }

  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get
    {
      return this.IsVirtualNode ? this.GetNodeTypeCaption((RectangleElement) this, (string) null) : TableElement.ElementTypeName;
    }
  }

  [CustomDisplayName("Attribute.Document.Model_71")]
  [CustomDescription("Attribute.Document.Model_72")]
  [CustomCategory("Attribute.Document.Model_73")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? VisibleTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetVisible((RectangleElement) this, new bool?()) : new bool?(this.Visible);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetVisible((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          this.Visible = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override float DefaultRowSize
  {
    get => base.DefaultRowSize;
    set => base.DefaultRowSize = value;
  }

  /// <summary>Высота строки для отрисовки сетки, новых строк и кратной высоты строки</summary>
  public override float? DefaultRowSizeUI
  {
    get
    {
      return this.IsVirtualNode ? this.GetDefaultRowSize((RectangleElement) this, new float?()) : new float?(base.DefaultRowSize);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
          this.SetDefaultRowSize((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
        else
          base.DefaultRowSize = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override bool IsFixedSizeRows => base.IsFixedSizeRows;

  [Browsable(false)]
  public override bool Transparent
  {
    get => base.Transparent;
    set => base.Transparent = value;
  }

  /// <summary>Прозрачный фон</summary>
  [CustomDisplayName("Attribute.Document.Model_80")]
  [CustomDescription("Attribute.Document.Model_81")]
  [CustomCategory("Attribute.Document.Model_82")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? TransparentTE
  {
    get => this.GetTransparent((RectangleElement) this, new bool?());
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          this.SetTransparent((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
        }
        else
        {
          this.SetTransparent((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
          base.Transparent = value.Value;
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Запрет изменять пользователем структуру</summary>
  [CustomDisplayName("Attribute.Document.Model_291")]
  [CustomDescription("Attribute.Document.Model_292")]
  [CustomCategory("Attribute.Document.Model_293")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool FixedStructure
  {
    get => base.ReadOnly;
    set => base.ReadOnly = value;
  }

  public override bool ReadOnlyStructure
  {
    get
    {
      if (base.ReadOnlyStructure)
        return true;
      if (!this.HasTemplate())
        return false;
      return this.FixedStructure || !this.IsPageFlow;
    }
  }

  [Browsable(false)]
  public override bool ReadOnly
  {
    get => base.ReadOnly;
    set => base.ReadOnly = value;
  }

  /// <summary>Пользователь не может редактировать данные элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_83")]
  [CustomDescription("Attribute.Document.Model_84")]
  [CustomCategory("Attribute.Document.Model_85")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? ReadOnlyTE
  {
    get => this.GetReadOnly((RectangleElement) this, new bool?());
    set
    {
      if (!value.HasValue)
        return;
      if (this.IsVirtualNode)
      {
        this.SetReadOnly((RectangleElement) this, value).OnChanged(new Changed_EventArgs());
      }
      else
      {
        this.SetReadOnly((RectangleElement) this, value)?.OnChanged(new Changed_EventArgs());
        base.ReadOnly = value.Value;
      }
    }
  }

  [Browsable(false)]
  public override bool GeometryChangingBlocked_ForUser
  {
    get => base.GeometryChangingBlocked_ForUser;
    set => base.GeometryChangingBlocked_ForUser = value;
  }

  /// <summary>Заблокировать изменение геометрии через интерфейс пользователя</summary>
  [CustomDisplayName("Attribute.Document.Model_86")]
  [CustomDescription("Attribute.Document.Model_87")]
  [CustomCategory("Attribute.Document.Model_88")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? GeometryChangingBlockedTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetGeometryChangingBlocked((RectangleElement) this, new bool?()) : new bool?(base.GeometryChangingBlocked_ForUser);
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
          this.SetGeometryChangingBlocked((RectangleElement) this, value);
        if (!value.HasValue || this.GeometryChangingBlocked_ForUser == value.Value)
          return;
        base.GeometryChangingBlocked_ForUser = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>Цвет переднего плана</summary>
  [CustomDisplayName("Attribute.Document.Model_89")]
  [CustomDescription("Attribute.Document.Model_90")]
  [CustomCategory("Attribute.Document.Model_91")]
  [Editor(typeof (ColorEditor), typeof (UITypeEditor))]
  [Browsable(false)]
  public Color? ForeColorTE
  {
    get => this.GetForeColor((RectangleElement) this, new Color?());
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetForeColorTE((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.OnChanged(new Changed_EventArgs());
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
        {
          RectangleElement rectangleElement = this.SetForeColorTE((RectangleElement) this, value);
          if (rectangleElement != null)
          {
            rectangleElement.OnChanged(new Changed_EventArgs());
            rectangleElement.TopLevelTable.RefreshUI();
          }
          base.ForeColor = value.Value;
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>Цвет фона</summary>
  [CustomDisplayName("Attribute.Document.Model_92")]
  [CustomDescription("Attribute.Document.Model_93")]
  [CustomCategory("Attribute.Document.Model_94")]
  [Editor(typeof (ColorEditor), typeof (UITypeEditor))]
  public Color? BackColorTE
  {
    get => this.GetBackColor((RectangleElement) this, new Color?());
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetBackColorTE((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.OnChanged(new Changed_EventArgs());
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
        {
          this.SetBackColorTE((RectangleElement) this, value)?.OnChanged(new Changed_EventArgs());
          base.BackColor = value.Value;
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Форматирование абзаца</summary>
  [CustomDisplayName("Attribute.Document.Model_95")]
  [CustomDescription("Attribute.Document.Model_96")]
  [CustomCategory("Attribute.Document.Model_97")]
  [RefreshProperties(RefreshProperties.All)]
  public ParagraphFormat ParagraphFormat
  {
    get
    {
      ParagraphFormat cur_var = (ParagraphFormat) null;
      this.GetParagraphFormat((RectangleElement) this, ref cur_var);
      return cur_var;
    }
    set
    {
      if (value == null)
        return;
      this.SetParagraphFormat((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
    }
  }

  /// <summary>Ориентация текста</summary>
  [CustomDisplayName("Attribute.Document.Model_98")]
  [CustomDescription("Attribute.Document.Model_99")]
  [CustomCategory("Attribute.Document.Model_100")]
  [RefreshProperties(RefreshProperties.All)]
  public TextOrientation? Orientation
  {
    get => this.GetOrientation((RectangleElement) this, new TextOrientation?());
    set
    {
      if (!value.HasValue)
        return;
      RectangleElement rectangleElement = this.SetOrientationTE((RectangleElement) this, value);
      if (rectangleElement == null)
        return;
      rectangleElement.TopLevelTable.RefreshUI();
      rectangleElement.OnChanged(new Changed_EventArgs());
    }
  }

  [CustomDisplayName("Attribute.Document.Model_101")]
  [CustomDescription("Attribute.Document.Model_102")]
  [CustomCategory("Attribute.Document.Model_103")]
  [RefreshProperties(RefreshProperties.All)]
  public CharFormat CharFormat
  {
    get
    {
      CharFormat cur_var = (CharFormat) null;
      this.GetCharFormat((RectangleElement) this, ref cur_var);
      return cur_var;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetCharFormat((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [CustomDisplayName("Attribute.Document.Model_104")]
  [CustomDescription("Attribute.Document.Model_105")]
  [CustomCategory("Attribute.Document.Model_106")]
  [TypeConverter(typeof (Intermech.Interfaces.Document.SizeFConverter))]
  public SizeF? OriginalSize
  {
    get
    {
      return this.IsVirtualNode ? this.GetOriginalSize((RectangleElement) this, new SizeF?()) : new SizeF?();
    }
  }

  /// <summary>Режим масштабирования</summary>
  [CustomDisplayName("Attribute.Document.Model_107")]
  [CustomDescription("Attribute.Document.Model_108")]
  [CustomCategory("Attribute.Document.Model_109")]
  public ImageScaleMode? ScaleMode
  {
    get
    {
      return this.IsVirtualNode ? this.GetScaleMode((RectangleElement) this, new ImageScaleMode?()) : new ImageScaleMode?();
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        RectangleElement rectangleElement = this.SetScaleMode((RectangleElement) this, value);
        if (rectangleElement == null)
          return;
        rectangleElement.UpdateLayout(true);
        rectangleElement.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_110")]
  [CustomDescription("Attribute.Document.Model_111")]
  [CustomCategory("Attribute.Document.Model_112")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool? AutoSizeHeightTE
  {
    get => this.GetAutoSizeHeight((RectangleElement) this, new bool?());
    set
    {
      if (!value.HasValue)
        return;
      this.SetAutoSizeHeight((RectangleElement) this, value)?.UpdateLayout(true);
    }
  }

  /// <summary>Текст</summary>
  [CustomDisplayName("Attribute.Document.Model_113")]
  [CustomDescription("Attribute.Document.Model_114")]
  [CustomCategory("Attribute.Document.Model_115")]
  [RefreshProperties(RefreshProperties.All)]
  public string Text
  {
    get
    {
      return this.IsVirtualNode ? this.GetText((RectangleElement) this, (string) null) : (string) null;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetText((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Рисунок</summary>
  [CustomDisplayName("Attribute.Document.Model_116")]
  [CustomDescription("Attribute.Document.Model_117")]
  [CustomCategory("Attribute.Document.Model_118")]
  public Image Image
  {
    get => this.IsVirtualNode ? this.GetImage((RectangleElement) this, (Image) null) : (Image) null;
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        RectangleElement rectangleElement = this.SetImageTE((RectangleElement) this, value);
        if (rectangleElement == null)
          return;
        rectangleElement.UpdateLayout(true);
        rectangleElement.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Строка формата вывода текста</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_119")]
  [CustomDescription("Attribute.Document.Model_120")]
  [CustomCategory("Attribute.Document.Model_121")]
  public string TextFormat
  {
    get
    {
      return this.IsVirtualNode ? this.GetTextFormat((RectangleElement) this, (string) null) : (string) null;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetTextFormatTE((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Текст отформатированный согласно TextFormat</summary>
  [CustomDisplayName("Attribute.Document.Model_122")]
  [CustomDescription("Attribute.Document.Model_123")]
  [CustomCategory("Attribute.Document.Model_124")]
  public string FormattedText => this.GetFormattedText((RectangleElement) this, (string) null);

  [Browsable(false)]
  public override BorderLine TopBorderLine
  {
    get => base.TopBorderLine;
    set => base.TopBorderLine = value;
  }

  /// <summary>Только для PropertyGrid! Линия верхней границы прямоугольника.</summary>
  [CustomDisplayName("Attribute.Document.Model_125")]
  [CustomDescription("Attribute.Document.Model_126")]
  [CustomCategory("Attribute.Document.Model_127")]
  public BorderLineTE TopBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.TopBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetTopBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          this.SetTopBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
        }
        else
        {
          BorderLine borderLine1 = this.TopBorderLine.Clone();
          Color? colorTe = value.ColorTE;
          if (colorTe.HasValue)
          {
            BorderLine borderLine2 = borderLine1;
            colorTe = value.ColorTE;
            Color color = colorTe.Value;
            borderLine2.Color = color;
          }
          BorderStyles? styleTe = value.StyleTE;
          if (styleTe.HasValue)
          {
            BorderLine borderLine3 = borderLine1;
            styleTe = value.StyleTE;
            int num = (int) styleTe.Value;
            borderLine3.Style = (BorderStyles) num;
          }
          float? nullable = value.WidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine4 = borderLine1;
            nullable = value.WidthTE;
            double num = (double) nullable.Value;
            borderLine4.Width = (float) num;
          }
          nullable = value.SerifWidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine5 = borderLine1;
            nullable = value.SerifWidthTE;
            double num = (double) nullable.Value;
            borderLine5.SerifWidth = (float) num;
          }
          this.AssignTopBorderLine(borderLine1, true);
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine BottomBorderLine
  {
    get => base.BottomBorderLine;
    set => base.BottomBorderLine = value;
  }

  /// <summary>Линия нижней границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_128")]
  [CustomDescription("Attribute.Document.Model_129")]
  [CustomCategory("Attribute.Document.Model_130")]
  public BorderLineTE BottomBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.BottomBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetBottomBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          this.SetBottomBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
        }
        else
        {
          BorderLine borderLine1 = this.BottomBorderLine.Clone();
          Color? colorTe = value.ColorTE;
          if (colorTe.HasValue)
          {
            BorderLine borderLine2 = borderLine1;
            colorTe = value.ColorTE;
            Color color = colorTe.Value;
            borderLine2.Color = color;
          }
          BorderStyles? styleTe = value.StyleTE;
          if (styleTe.HasValue)
          {
            BorderLine borderLine3 = borderLine1;
            styleTe = value.StyleTE;
            int num = (int) styleTe.Value;
            borderLine3.Style = (BorderStyles) num;
          }
          float? nullable = value.WidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine4 = borderLine1;
            nullable = value.WidthTE;
            double num = (double) nullable.Value;
            borderLine4.Width = (float) num;
          }
          nullable = value.SerifWidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine5 = borderLine1;
            nullable = value.SerifWidthTE;
            double num = (double) nullable.Value;
            borderLine5.SerifWidth = (float) num;
          }
          this.AssignBottomBorderLine(borderLine1, true);
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine LeftBorderLine
  {
    get => base.LeftBorderLine;
    set => base.LeftBorderLine = value;
  }

  /// <summary>Линия левой границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_131")]
  [CustomDescription("Attribute.Document.Model_132")]
  [CustomCategory("Attribute.Document.Model_133")]
  public BorderLineTE LeftBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.LeftBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetLeftBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          this.SetLeftBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
        }
        else
        {
          BorderLine borderLine1 = this.LeftBorderLine.Clone();
          Color? colorTe = value.ColorTE;
          if (colorTe.HasValue)
          {
            BorderLine borderLine2 = borderLine1;
            colorTe = value.ColorTE;
            Color color = colorTe.Value;
            borderLine2.Color = color;
          }
          BorderStyles? styleTe = value.StyleTE;
          if (styleTe.HasValue)
          {
            BorderLine borderLine3 = borderLine1;
            styleTe = value.StyleTE;
            int num = (int) styleTe.Value;
            borderLine3.Style = (BorderStyles) num;
          }
          float? nullable = value.WidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine4 = borderLine1;
            nullable = value.WidthTE;
            double num = (double) nullable.Value;
            borderLine4.Width = (float) num;
          }
          nullable = value.SerifWidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine5 = borderLine1;
            nullable = value.SerifWidthTE;
            double num = (double) nullable.Value;
            borderLine5.SerifWidth = (float) num;
          }
          this.AssignLeftBorderLine(borderLine1, true);
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine RightBorderLine
  {
    get => base.RightBorderLine;
    set => base.RightBorderLine = value;
  }

  /// <summary>Линия правой границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_134")]
  [CustomDescription("Attribute.Document.Model_135")]
  [CustomCategory("Attribute.Document.Model_136")]
  public BorderLineTE RightBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.RightBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetRightBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          this.SetRightBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
        }
        else
        {
          BorderLine borderLine1 = this.RightBorderLine.Clone();
          Color? colorTe = value.ColorTE;
          if (colorTe.HasValue)
          {
            BorderLine borderLine2 = borderLine1;
            colorTe = value.ColorTE;
            Color color = colorTe.Value;
            borderLine2.Color = color;
          }
          BorderStyles? styleTe = value.StyleTE;
          if (styleTe.HasValue)
          {
            BorderLine borderLine3 = borderLine1;
            styleTe = value.StyleTE;
            int num = (int) styleTe.Value;
            borderLine3.Style = (BorderStyles) num;
          }
          float? nullable = value.WidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine4 = borderLine1;
            nullable = value.WidthTE;
            double num = (double) nullable.Value;
            borderLine4.Width = (float) num;
          }
          nullable = value.SerifWidthTE;
          if (nullable.HasValue)
          {
            BorderLine borderLine5 = borderLine1;
            nullable = value.SerifWidthTE;
            double num = (double) nullable.Value;
            borderLine5.SerifWidth = (float) num;
          }
          this.AssignRightBorderLine(borderLine1, true);
        }
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Горизонтальные внутренние линии виртуальной таблицы</summary>
  [CustomDisplayName("Attribute.Document.Model_137")]
  [CustomDescription("Attribute.Document.Model_138")]
  [CustomCategory("Attribute.Document.Model_139")]
  public BorderLineTE InnerHorizontalLineTE
  {
    get
    {
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetInnerHorizontalLineTE((RectangleElement) this, ref cur_var, false, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_577"));
      try
      {
        this.SetInnerHorizontalLineTE((RectangleElement) this, value, false, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override ImDocumentData OwnerDocument
  {
    get
    {
      return this.IsVirtualNode && this.Parent != null ? this.Parent.OwnerDocument : base.OwnerDocument;
    }
  }

  /// <summary>Вертикальные внутренние линии виртуальной таблицы</summary>
  [CustomDisplayName("Attribute.Document.Model_140")]
  [CustomDescription("Attribute.Document.Model_141")]
  [CustomCategory("Attribute.Document.Model_142")]
  public BorderLineTE InnerVerticalLineTE
  {
    get
    {
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetInnerVerticalLineTE((RectangleElement) this, ref cur_var, false, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_578"));
      try
      {
        this.SetInnerVerticalLineTE((RectangleElement) this, value, false, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Идентификатор таблицы продолжения для данной таблицы</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_305")]
  [CustomDescription("Attribute.Document.Model_306")]
  [CustomCategory("Attribute.Document.Model_301")]
  [Editor(typeof (ContinuationTableIdEditor), typeof (UITypeEditor))]
  public string ContinuationTableIdTE
  {
    [DebuggerStepThrough] get => this.GetContinuationTableId() ?? "";
    set => this.SetContinuationTableId(value);
  }

  /// <summary>Получение ширины дочерних ячеек</summary>
  private float? GetWidthForUser(RectangleElement cell, float? cur_width)
  {
    if (cell.IsSingleCell)
      return cell.WidthForUser;
    if (cell.Nodes.Count == 0)
      return cur_width;
    float? widthForUser1;
    if (!(widthForUser1 = this.GetWidthForUser(cell.Nodes[0] as RectangleElement, cur_width)).HasValue)
      return new float?();
    float? widthForUser2;
    if (cur_width.HasValue)
    {
      float? nullable = widthForUser1;
      widthForUser2 = cur_width;
      if (!((double) nullable.GetValueOrDefault() == (double) widthForUser2.GetValueOrDefault() & nullable.HasValue == widthForUser2.HasValue))
      {
        widthForUser2 = new float?();
        return widthForUser2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      widthForUser2 = widthForUser1;
      float? widthForUser3 = this.GetWidthForUser(node, widthForUser1);
      if (!((double) widthForUser2.GetValueOrDefault() == (double) widthForUser3.GetValueOrDefault() & widthForUser2.HasValue == widthForUser3.HasValue))
        return new float?();
    }
    return widthForUser1;
  }

  /// <summary>Изменение ширины дочерних ячеек</summary>
  private RectangleElement SetWidthForUser(float value, RectangleElement cell)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetWidthForUser(value, cell.Nodes[index] as RectangleElement);
      return rectangleElement;
    }
    SizeF size = new SizeF(value, 0.0f);
    if (this.page != null)
      this.page.ConvertUserToInternal(size);
    RectangleF properBounds = cell.ProperBounds with
    {
      Width = value
    };
    cell.overrideFlags |= OverrideFlags.Width;
    cell.overrideFlags2 |= OverrideFlags2.ColumnWidth;
    cell.AssignProperBounds(properBounds, true, false, false);
    cell.RecalcRelativeSize();
    return cell;
  }

  /// <summary>Получение высоты дочерних ячеек</summary>
  private float? GetHeightForUser(RectangleElement cell, float? cur_height)
  {
    if (cell.IsSingleCell)
      return cell.HeightForUser;
    if (cell.Nodes.Count == 0)
      return cur_height;
    float? heightForUser1;
    if (!(heightForUser1 = this.GetHeightForUser(cell.Nodes[0] as RectangleElement, cur_height)).HasValue)
      return new float?();
    float? heightForUser2;
    if (cur_height.HasValue)
    {
      float? nullable = heightForUser1;
      heightForUser2 = cur_height;
      if (!((double) nullable.GetValueOrDefault() == (double) heightForUser2.GetValueOrDefault() & nullable.HasValue == heightForUser2.HasValue))
      {
        heightForUser2 = new float?();
        return heightForUser2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      heightForUser2 = heightForUser1;
      float? heightForUser3 = this.GetHeightForUser(node, heightForUser1);
      if (!((double) heightForUser2.GetValueOrDefault() == (double) heightForUser3.GetValueOrDefault() & heightForUser2.HasValue == heightForUser3.HasValue))
        return new float?();
    }
    return heightForUser1;
  }

  /// <summary>Изменение высоты дочерних ячеек</summary>
  private RectangleElement SetHeightForUser(float value, RectangleElement cell)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetHeightForUser(value, cell.Nodes[index] as RectangleElement);
      return rectangleElement;
    }
    cell.SetHeightForUser(value, false, false);
    return cell;
  }

  /// <summary>Получение левой коорд. левой из дочерних ячеек в Bounds координатах</summary>
  private float GetLeftForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float leftForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float leftForUser2 = this.GetLeftForUser(cell.Nodes[index] as RectangleElement, leftForUser1, ref pg);
        leftForUser1 = Math.Min(leftForUser1, leftForUser2);
      }
      return leftForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Left;
  }

  /// <summary>Получение правой коорд. правой из дочерних ячеек в Bounds координатах</summary>
  private float GetRightForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float rightForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float rightForUser2 = this.GetRightForUser(cell.Nodes[index] as RectangleElement, rightForUser1, ref pg);
        rightForUser1 = Math.Max(rightForUser1, rightForUser2);
      }
      return rightForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Right;
  }

  /// <summary>Получение нижней коорд. правой из дочерних ячеек в Bounds координатах</summary>
  private float GetBottomForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float bottomForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float bottomForUser2 = this.GetBottomForUser(cell.Nodes[index] as RectangleElement, bottomForUser1, ref pg);
        bottomForUser1 = Math.Max(bottomForUser1, bottomForUser2);
      }
      return bottomForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Bottom;
  }

  /// <summary>Получение верхней коорд. левой из дочерних ячеек в Bounds координатах</summary>
  private float GetTopForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float topForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float topForUser2 = this.GetTopForUser(cell.Nodes[index] as RectangleElement, topForUser1, ref pg);
        topForUser1 = Math.Min(topForUser1, topForUser2);
      }
      return topForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Top;
  }

  /// <summary>Получение имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_name">текущее имя</param>
  /// <returns>Возвращает null, если имена не совпадают</returns>
  private string GetName(RectangleElement cell, string cur_name)
  {
    if (cell.IsVirtualNode)
      return (string) null;
    if (cell.IsSingleCell)
      return cell.Name;
    if (cell.Nodes.Count == 0)
      return cur_name;
    string name;
    if ((name = this.GetName(cell.Nodes[0] as RectangleElement, cur_name)) == null)
      return (string) null;
    if (cur_name != null && name != cur_name)
      return (string) null;
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      if (name != this.GetName(node, name))
        return (string) null;
    }
    return name;
  }

  /// <summary>Установка имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_name">устанавливаемое имя</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает последнюю ячейку</returns>
  private RectangleElement SetNameRecurcive(
    RectangleElement cell,
    string cur_name,
    bool updateUI,
    bool updateLayout)
  {
    cell1 = cell != null ? cell : throw new ArgumentNullException(nameof (cell));
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        if (cell.Nodes[index] is RectangleElement cell1)
          cell1 = this.SetNameRecurcive(cell1, cur_name, updateUI, updateLayout);
      }
      return cell1;
    }
    cell.SetName(cur_name, updateUI, updateLayout);
    return cell;
  }

  /// <summary>Получение типов реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_type">текущее имя</param>
  /// <returns>Возвращает null, если типы не совпадают</returns>
  private string GetNodeTypeCaption(RectangleElement cell, string cur_type)
  {
    if (cell.IsSingleCell)
      return cell.NodeTypeCaption;
    if (cell.Nodes.Count == 0)
      return cur_type;
    string nodeTypeCaption;
    if ((nodeTypeCaption = this.GetNodeTypeCaption(cell.Nodes[0] as RectangleElement, cur_type)) == null)
      return (string) null;
    if (cur_type != null && nodeTypeCaption != cur_type)
      return (string) null;
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      if (nodeTypeCaption != this.GetNodeTypeCaption(node, nodeTypeCaption))
        return (string) null;
    }
    return nodeTypeCaption;
  }

  private bool? GetVisible(RectangleElement cell, bool? cur_vis)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.Visible);
    if (cell.Nodes.Count == 0)
      return cur_vis;
    bool? visible1;
    if (!(visible1 = this.GetVisible(cell.Nodes[0] as RectangleElement, cur_vis)).HasValue)
      return new bool?();
    bool? visible2;
    if (cur_vis.HasValue)
    {
      bool? nullable = visible1;
      visible2 = cur_vis;
      if (!(nullable.GetValueOrDefault() == visible2.GetValueOrDefault() & nullable.HasValue == visible2.HasValue))
      {
        visible2 = new bool?();
        return visible2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      visible2 = visible1;
      bool? visible3 = this.GetVisible(node, visible1);
      if (!(visible2.GetValueOrDefault() == visible3.GetValueOrDefault() & visible2.HasValue == visible3.HasValue))
        return new bool?();
    }
    return visible1;
  }

  private RectangleElement SetVisible(RectangleElement cell, bool? cur_vis)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetVisible(cell.Nodes[index] as RectangleElement, cur_vis);
      return rectangleElement;
    }
    cell.SetVisible(cur_vis.Value, false, true, false, false, false);
    return cell;
  }

  private float? GetDefaultRowSize(RectangleElement cell, float? cur_var)
  {
    if (cell.IsSingleCell)
      return new float?(cell.DefaultRowSize);
    if (cell.Nodes.Count == 0)
      return cur_var;
    float? defaultRowSize1;
    if (!(defaultRowSize1 = this.GetDefaultRowSize(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new float?();
    float? defaultRowSize2;
    if (cur_var.HasValue)
    {
      float? nullable = defaultRowSize1;
      defaultRowSize2 = cur_var;
      if (!((double) nullable.GetValueOrDefault() == (double) defaultRowSize2.GetValueOrDefault() & nullable.HasValue == defaultRowSize2.HasValue))
      {
        defaultRowSize2 = new float?();
        return defaultRowSize2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      defaultRowSize2 = defaultRowSize1;
      float? defaultRowSize3 = this.GetDefaultRowSize(node, defaultRowSize1);
      if (!((double) defaultRowSize2.GetValueOrDefault() == (double) defaultRowSize3.GetValueOrDefault() & defaultRowSize2.HasValue == defaultRowSize3.HasValue))
        return new float?();
    }
    return defaultRowSize1;
  }

  private RectangleElement SetDefaultRowSize(RectangleElement cell, float? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetDefaultRowSize(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.SetDefaultRowSize(cur_var.Value, true, true, false, false);
    return cell;
  }

  private bool? GetIsFixedSizeRows(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.IsFixedSizeRows);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? isFixedSizeRows1;
    if (!(isFixedSizeRows1 = this.GetIsFixedSizeRows(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? isFixedSizeRows2;
    if (cur_var.HasValue)
    {
      bool? nullable = isFixedSizeRows1;
      isFixedSizeRows2 = cur_var;
      if (!(nullable.GetValueOrDefault() == isFixedSizeRows2.GetValueOrDefault() & nullable.HasValue == isFixedSizeRows2.HasValue))
      {
        isFixedSizeRows2 = new bool?();
        return isFixedSizeRows2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      isFixedSizeRows2 = isFixedSizeRows1;
      bool? isFixedSizeRows3 = this.GetIsFixedSizeRows(node, isFixedSizeRows1);
      if (!(isFixedSizeRows2.GetValueOrDefault() == isFixedSizeRows3.GetValueOrDefault() & isFixedSizeRows2.HasValue == isFixedSizeRows3.HasValue))
        return new bool?();
    }
    return isFixedSizeRows1;
  }

  private bool? GetTransparent(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.Transparent);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? transparent1;
    if (!(transparent1 = this.GetTransparent(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? transparent2;
    if (cur_var.HasValue)
    {
      bool? nullable = transparent1;
      transparent2 = cur_var;
      if (!(nullable.GetValueOrDefault() == transparent2.GetValueOrDefault() & nullable.HasValue == transparent2.HasValue))
      {
        transparent2 = new bool?();
        return transparent2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      transparent2 = transparent1;
      bool? transparent3 = this.GetTransparent(node, transparent1);
      if (!(transparent2.GetValueOrDefault() == transparent3.GetValueOrDefault() & transparent2.HasValue == transparent3.HasValue))
        return new bool?();
    }
    return transparent1;
  }

  private RectangleElement SetTransparent(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetTransparent(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.AssignTransparent(cur_var.Value, false);
    return cell;
  }

  private bool? GetReadOnly(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.ReadOnly);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? cur_var1;
    if (!(cur_var1 = this.GetReadOnly(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? nullable1;
    if (cur_var.HasValue)
    {
      bool? nullable2 = cur_var1;
      nullable1 = cur_var;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        nullable1 = new bool?();
        return nullable1;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      nullable1 = cur_var1;
      bool? nullable3 = this.GetReadOnly(node, cur_var1);
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        return new bool?();
    }
    return cur_var1;
  }

  private RectangleElement SetReadOnly(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetReadOnly(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.ReadOnly = cur_var.Value;
    return cell;
  }

  private bool? GetGeometryChangingBlocked(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.GeometryChangingBlocked_ForUser);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? geometryChangingBlocked1;
    if (!(geometryChangingBlocked1 = this.GetGeometryChangingBlocked(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? geometryChangingBlocked2;
    if (cur_var.HasValue)
    {
      bool? nullable = geometryChangingBlocked1;
      geometryChangingBlocked2 = cur_var;
      if (!(nullable.GetValueOrDefault() == geometryChangingBlocked2.GetValueOrDefault() & nullable.HasValue == geometryChangingBlocked2.HasValue))
      {
        geometryChangingBlocked2 = new bool?();
        return geometryChangingBlocked2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      geometryChangingBlocked2 = geometryChangingBlocked1;
      bool? geometryChangingBlocked3 = this.GetGeometryChangingBlocked(node, geometryChangingBlocked1);
      if (!(geometryChangingBlocked2.GetValueOrDefault() == geometryChangingBlocked3.GetValueOrDefault() & geometryChangingBlocked2.HasValue == geometryChangingBlocked3.HasValue))
        return new bool?();
    }
    return geometryChangingBlocked1;
  }

  private RectangleElement SetGeometryChangingBlocked(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        node.GeometryChangingBlocked_ForUser = cur_var.Value;
        rectangleElement = this.SetGeometryChangingBlocked(node, cur_var);
      }
      return rectangleElement;
    }
    cell.GeometryChangingBlocked_ForUser = cur_var.Value;
    return cell;
  }

  private Color? GetForeColor(RectangleElement cell, Color? cur_var)
  {
    if (cell.IsSingleCell)
      return new Color?(cell.ForeColor);
    if (cell.Nodes.Count == 0)
      return cur_var;
    Color? foreColor1;
    if (!(foreColor1 = this.GetForeColor(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new Color?();
    Color? foreColor2;
    if (cur_var.HasValue)
    {
      Color? nullable = foreColor1;
      foreColor2 = cur_var;
      if ((nullable.HasValue == foreColor2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != foreColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        foreColor2 = new Color?();
        return foreColor2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      foreColor2 = foreColor1;
      Color? foreColor3 = this.GetForeColor(node, foreColor1);
      if ((foreColor2.HasValue == foreColor3.HasValue ? (foreColor2.HasValue ? (foreColor2.GetValueOrDefault() != foreColor3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        foreColor3 = new Color?();
        return foreColor3;
      }
    }
    return foreColor1;
  }

  private RectangleElement SetForeColorTE(RectangleElement cell, Color? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetForeColorTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.AssignForeColor(cur_var.Value, false);
    return cell;
  }

  private Color? GetBackColor(RectangleElement cell, Color? cur_var)
  {
    if (cell.IsSingleCell)
      return new Color?(cell.BackColor);
    if (cell.Nodes.Count == 0)
      return cur_var;
    Color? backColor1;
    if (!(backColor1 = this.GetBackColor(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new Color?();
    Color? backColor2;
    if (cur_var.HasValue)
    {
      Color? nullable = backColor1;
      backColor2 = cur_var;
      if ((nullable.HasValue == backColor2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != backColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        backColor2 = new Color?();
        return backColor2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      backColor2 = backColor1;
      Color? backColor3 = this.GetBackColor(node, backColor1);
      if ((backColor2.HasValue == backColor3.HasValue ? (backColor2.HasValue ? (backColor2.GetValueOrDefault() != backColor3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        backColor3 = new Color?();
        return backColor3;
      }
    }
    return backColor1;
  }

  private RectangleElement SetBackColorTE(RectangleElement cell, Color? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetBackColorTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.AssignBackColor(cur_var.Value, false);
    return cell;
  }

  /// <summary>Получение имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_var">текущий абзац</param>
  /// <returns>Возвращает false, если все свойства не совпадают</returns>
  private bool GetParagraphFormat(RectangleElement cell, ref ParagraphFormat cur_var)
  {
    bool paragraphFormat1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      paragraphFormat1 = this.GetParagraphFormat(cell.Nodes[0] as RectangleElement, ref cur_var);
      if (!paragraphFormat1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        paragraphFormat1 = this.GetParagraphFormat(cell.Nodes[index] as RectangleElement, ref cur_var);
        if (!paragraphFormat1)
          return false;
      }
    }
    else if (cell is TextData textData)
    {
      ParagraphFormat paragraphFormat2 = textData.ParagraphFormat;
      if (cur_var != null)
        return cur_var.GetFields(paragraphFormat2);
      cur_var = paragraphFormat2.Clone();
      return true;
    }
    return paragraphFormat1;
  }

  private RectangleElement SetParagraphFormat(RectangleElement cell, ParagraphFormat cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      for (int index = 0; index < cell.Nodes.Count; ++index)
        rectangleElement = this.SetParagraphFormat(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
    {
      ParagraphFormat paragraphFormat = textData.ParagraphFormat.Clone();
      if (cur_var.HorzAlignment.HasValue)
        paragraphFormat.HorzAlignment = cur_var.HorzAlignment;
      if (cur_var.VertAlignment.HasValue)
        paragraphFormat.VertAlignment = cur_var.VertAlignment;
      if (cur_var.DisableFloatLines.HasValue)
        paragraphFormat.DisableFloatLines = cur_var.DisableFloatLines;
      if (cur_var.SpaceBetweenLines.HasValue)
        paragraphFormat.SpaceBetweenLines = cur_var.SpaceBetweenLines;
      if (cur_var.KeepWithNext.HasValue)
        paragraphFormat.KeepWithNext = cur_var.KeepWithNext;
      if (cur_var.KeepTogether.HasValue)
        paragraphFormat.KeepTogether = cur_var.KeepTogether;
      if (cur_var.DisableWordWrap.HasValue)
        paragraphFormat.DisableWordWrap = cur_var.DisableWordWrap;
      if (cur_var.IdentFirstLine.HasValue)
        paragraphFormat.IdentFirstLine = cur_var.IdentFirstLine;
      if (cur_var.IdentLeft.HasValue)
        paragraphFormat.IdentLeft = cur_var.IdentLeft;
      if (cur_var.IdentRight.HasValue)
        paragraphFormat.IdentRight = cur_var.IdentRight;
      if (cur_var.IntervalBefore.HasValue)
        paragraphFormat.IntervalBefore = cur_var.IntervalBefore;
      if (cur_var.IntervalAfter.HasValue)
        paragraphFormat.IntervalAfter = cur_var.IntervalAfter;
      if (cur_var.FromNewPage.HasValue)
        paragraphFormat.FromNewPage = cur_var.FromNewPage;
      if (cur_var.LineSpacingMethod.HasValue)
        paragraphFormat.LineSpacingMethod = cur_var.LineSpacingMethod;
      if (cur_var.TextLevel.HasValue)
        paragraphFormat.TextLevel = cur_var.TextLevel;
      textData.SetParagraphFormat(paragraphFormat.Clone(), false, false);
    }
    return cell;
  }

  private TextOrientation? GetOrientation(RectangleElement cell, TextOrientation? cur_var)
  {
    TextOrientation? cur_var1 = new TextOrientation?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetOrientation(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new TextOrientation?();
      TextOrientation? orientation1;
      TextOrientation? orientation2;
      if (cur_var.HasValue)
      {
        orientation1 = cur_var1;
        orientation2 = cur_var;
        if (!(orientation1.GetValueOrDefault() == orientation2.GetValueOrDefault() & orientation1.HasValue == orientation2.HasValue))
        {
          orientation2 = new TextOrientation?();
          return orientation2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        orientation2 = cur_var1;
        orientation1 = this.GetOrientation(node, cur_var1);
        if (!(orientation2.GetValueOrDefault() == orientation1.GetValueOrDefault() & orientation2.HasValue == orientation1.HasValue))
        {
          orientation1 = new TextOrientation?();
          return orientation1;
        }
      }
    }
    else if (cell is LabelElement labelElement)
      return new TextOrientation?(labelElement.Orientation);
    return cur_var1;
  }

  private RectangleElement SetOrientationTE(RectangleElement cell, TextOrientation? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetOrientationTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is LabelElement labelElement)
      labelElement.SetOrientation(cur_var.Value, false, false);
    return cell;
  }

  /// <summary>смотри ParagraphFormatTE</summary>
  private bool GetCharFormat(RectangleElement cell, ref CharFormat cur_var)
  {
    bool charFormat1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      charFormat1 = this.GetCharFormat(cell.Nodes[0] as RectangleElement, ref cur_var);
      if (!charFormat1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        charFormat1 = this.GetCharFormat(cell.Nodes[index] as RectangleElement, ref cur_var);
        if (!charFormat1)
          return false;
      }
    }
    else if (cell is TextData textData)
    {
      CharFormat charFormat2 = textData.CharFormat;
      if (cur_var == null)
      {
        cur_var = charFormat2.Clone();
        return true;
      }
      StrikeoutLineStyle? strike1 = cur_var.Strike;
      StrikeoutLineStyle? strike2 = charFormat2.Strike;
      bool flag;
      if (!(strike1.GetValueOrDefault() == strike2.GetValueOrDefault() & strike1.HasValue == strike2.HasValue))
      {
        cur_var.Strike = new StrikeoutLineStyle?();
        flag = false;
      }
      else
        flag = true;
      if (cur_var.FontFamily != charFormat2.FontFamily)
      {
        cur_var.FontFamily = (string) null;
        flag = false;
      }
      else
        flag = true;
      UnderlineStyle? underline1 = cur_var.Underline;
      UnderlineStyle? underline2 = charFormat2.Underline;
      if (!(underline1.GetValueOrDefault() == underline2.GetValueOrDefault() & underline1.HasValue == underline2.HasValue))
      {
        cur_var.Underline = new UnderlineStyle?();
        flag = false;
      }
      else
        flag = true;
      float? fontSize1 = cur_var.FontSize;
      float? fontSize2 = charFormat2.FontSize;
      if (!((double) fontSize1.GetValueOrDefault() == (double) fontSize2.GetValueOrDefault() & fontSize1.HasValue == fontSize2.HasValue))
      {
        cur_var.FontSize = new float?();
        flag = false;
      }
      else
        flag = true;
      float? fontSizeMm1 = cur_var.FontSizeMm;
      float? fontSizeMm2 = charFormat2.FontSizeMm;
      if (!((double) fontSizeMm1.GetValueOrDefault() == (double) fontSizeMm2.GetValueOrDefault() & fontSizeMm1.HasValue == fontSizeMm2.HasValue))
      {
        cur_var.FontSizeMm = new float?();
        flag = false;
      }
      else
        flag = true;
      BoldItalicStyle? boldItalic1 = cur_var.BoldItalic;
      BoldItalicStyle? boldItalic2 = charFormat2.BoldItalic;
      if (!(boldItalic1.GetValueOrDefault() == boldItalic2.GetValueOrDefault() & boldItalic1.HasValue == boldItalic2.HasValue))
      {
        cur_var.BoldItalic = new BoldItalicStyle?();
        flag = false;
      }
      else
        flag = true;
      Color? textColorForUser1 = cur_var.TextColorForUser;
      Color? textColorForUser2 = charFormat2.TextColorForUser;
      if ((textColorForUser1.HasValue == textColorForUser2.HasValue ? (textColorForUser1.HasValue ? (textColorForUser1.GetValueOrDefault() != textColorForUser2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.TextColorForUser = new Color?();
        flag = false;
      }
      else
        flag = true;
      Color? textBkColorForUser1 = cur_var.TextBkColorForUser;
      Color? textBkColorForUser2 = charFormat2.TextBkColorForUser;
      if ((textBkColorForUser1.HasValue == textBkColorForUser2.HasValue ? (textBkColorForUser1.HasValue ? (textBkColorForUser1.GetValueOrDefault() != textBkColorForUser2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.TextBkColorForUser = new Color?();
        flag = false;
      }
      else
        flag = true;
      Color? underlineColor1 = cur_var.UnderlineColor;
      Color? underlineColor2 = charFormat2.UnderlineColor;
      bool charFormat3;
      if ((underlineColor1.HasValue == underlineColor2.HasValue ? (underlineColor1.HasValue ? (underlineColor1.GetValueOrDefault() != underlineColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.UnderlineColor = new Color?();
        charFormat3 = false;
      }
      else
        charFormat3 = true;
      return charFormat3;
    }
    return charFormat1;
  }

  /// <summary>смотри ParagraphFormatTE</summary>
  private RectangleElement SetCharFormat(RectangleElement cell, CharFormat cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetCharFormat(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
    {
      CharFormat charFormat = textData.CharFormat.Clone();
      if (cur_var.Strike.HasValue)
        charFormat.Strike = cur_var.Strike;
      if (cur_var.FontFamily != null)
        charFormat.FontFamily = cur_var.FontFamily;
      if (cur_var.Underline.HasValue)
        charFormat.Underline = cur_var.Underline;
      if (cur_var.FontSize.HasValue)
        charFormat.FontSize = cur_var.FontSize;
      if (cur_var.FontSizeMm.HasValue)
        charFormat.FontSizeMm = cur_var.FontSizeMm;
      if (cur_var.BoldItalic.HasValue)
        charFormat.BoldItalic = cur_var.BoldItalic;
      if (cur_var.TextColorForUser.HasValue)
        charFormat.TextColorForUser = cur_var.TextColorForUser;
      if (cur_var.TextBkColorForUser.HasValue)
        charFormat.TextBkColorForUser = cur_var.TextBkColorForUser;
      if (cur_var.UnderlineColor.HasValue)
        charFormat.UnderlineColor = cur_var.UnderlineColor;
      textData.SetCharFormat(charFormat.Clone(), false, false);
    }
    return cell;
  }

  private SizeF? GetOriginalSize(RectangleElement cell, SizeF? cur_var)
  {
    SizeF? cur_var1 = new SizeF?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetOriginalSize(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new SizeF?();
      SizeF? originalSize;
      if (cur_var.HasValue)
      {
        originalSize = cur_var1;
        SizeF? nullable = cur_var;
        if ((originalSize.HasValue == nullable.HasValue ? (originalSize.HasValue ? (originalSize.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          return new SizeF?();
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        SizeF? nullable = cur_var1;
        originalSize = this.GetOriginalSize(node, cur_var1);
        if ((nullable.HasValue == originalSize.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != originalSize.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          originalSize = new SizeF?();
          return originalSize;
        }
      }
    }
    else if (cell is ContainerData containerData)
      return new SizeF?(containerData.OriginalSize);
    return cur_var1;
  }

  private ImageScaleMode? GetScaleMode(RectangleElement cell, ImageScaleMode? cur_var)
  {
    ImageScaleMode? cur_var1 = new ImageScaleMode?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetScaleMode(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new ImageScaleMode?();
      ImageScaleMode? scaleMode1;
      ImageScaleMode? scaleMode2;
      if (cur_var.HasValue)
      {
        scaleMode1 = cur_var1;
        scaleMode2 = cur_var;
        if (!(scaleMode1.GetValueOrDefault() == scaleMode2.GetValueOrDefault() & scaleMode1.HasValue == scaleMode2.HasValue))
        {
          scaleMode2 = new ImageScaleMode?();
          return scaleMode2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        scaleMode2 = cur_var1;
        scaleMode1 = this.GetScaleMode(node, cur_var1);
        if (!(scaleMode2.GetValueOrDefault() == scaleMode1.GetValueOrDefault() & scaleMode2.HasValue == scaleMode1.HasValue))
        {
          scaleMode1 = new ImageScaleMode?();
          return scaleMode1;
        }
      }
    }
    else if (cell is ContainerData containerData)
      return new ImageScaleMode?(containerData.ScaleMode);
    return cur_var1;
  }

  private RectangleElement SetScaleMode(RectangleElement cell, ImageScaleMode? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetScaleMode(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is ContainerData containerData)
      containerData.AssignScaleMode(cur_var.Value, false, false, true);
    return cell;
  }

  private bool? GetAutoSizeHeight(RectangleElement cell, bool? cur_var)
  {
    bool? cur_var1 = new bool?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetAutoSizeHeight(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new bool?();
      bool? autoSizeHeight1;
      bool? autoSizeHeight2;
      if (cur_var.HasValue)
      {
        autoSizeHeight1 = cur_var1;
        autoSizeHeight2 = cur_var;
        if (!(autoSizeHeight1.GetValueOrDefault() == autoSizeHeight2.GetValueOrDefault() & autoSizeHeight1.HasValue == autoSizeHeight2.HasValue))
        {
          autoSizeHeight2 = new bool?();
          return autoSizeHeight2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        autoSizeHeight2 = cur_var1;
        autoSizeHeight1 = this.GetAutoSizeHeight(node, cur_var1);
        if (!(autoSizeHeight2.GetValueOrDefault() == autoSizeHeight1.GetValueOrDefault() & autoSizeHeight2.HasValue == autoSizeHeight1.HasValue))
        {
          autoSizeHeight1 = new bool?();
          return autoSizeHeight1;
        }
      }
    }
    else if (cell is TextBoxElement textBoxElement)
      return new bool?(textBoxElement.AutoSizeHeight);
    return cur_var1;
  }

  private RectangleElement SetAutoSizeHeight(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetAutoSizeHeight(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextBoxElement textBoxElement)
      textBoxElement.AssignAutoSizeHeight(cur_var.Value, false, false, true);
    return cell;
  }

  private string GetText(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetText(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetText(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is TextData textData)
      return textData.Text;
    return cur_var1;
  }

  private RectangleElement SetText(RectangleElement cell, string cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetText(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
      textData.AssignText(cur_var, false, true, true, false, false);
    return cell;
  }

  private Image GetImage(RectangleElement cell, Image cur_var)
  {
    Image cur_var1 = (Image) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetImage(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (Image) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (Image) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetImage(node, cur_var1))
          return (Image) null;
      }
    }
    else if (cell is ContainerData containerData)
      return containerData.Image == null ? (Image) null : (Image) containerData.Image.Clone();
    return cur_var1;
  }

  private RectangleElement SetImageTE(RectangleElement cell, Image cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetImageTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is ContainerData containerData)
      containerData.SetImage(cur_var, true, false);
    return cell;
  }

  private string GetTextFormat(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetTextFormat(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetTextFormat(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is TextData textData)
      return textData.TextFormat;
    return cur_var1;
  }

  private RectangleElement SetTextFormatTE(RectangleElement cell, string cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetTextFormatTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
      textData.AssignTextFormat(cur_var, false);
    return cell;
  }

  private string GetFormattedText(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetFormattedText(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetFormattedText(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is LabelElement labelElement)
      return labelElement.FormattedText;
    return cur_var1;
  }

  /// <summary>Получить значение TopBorderLine дочерних ячеек</summary>
  /// <param name="cur_var">текущее значение</param>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="hastop">имеет ли текущая ячейка граничащую с ней сверху</param>
  /// <returns>Возвращает false если все свойства не совпадают</returns>
  private bool GetTopBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hastop)
  {
    bool hastop1 = hastop;
    bool topBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      topBorderLineTe1 = this.GetTopBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hastop1);
      if (!topBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hastop2 = hastop;
        if (!isRow)
          hastop2 = true;
        topBorderLineTe1 = this.GetTopBorderLineTE(node, ref cur_var, hastop2);
        if (!topBorderLineTe1)
          return false;
      }
    }
    else if (!hastop)
    {
      BorderLine topBorderLine = cell.TopBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(topBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = topBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = topBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = topBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = topBorderLine.SerifWidth;
      bool topBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        topBorderLineTe2 = false;
      }
      else
        topBorderLineTe2 = true;
      return topBorderLineTe2;
    }
    return topBorderLineTe1;
  }

  /// <summary>Получить значение TopBorderLine дочерних ячеек</summary>
  /// <param name="cur_var">текущее значение</param>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="hastop">имеет ли текущая ячейка граничащую с ней сверху</param>
  /// <returns>Возвращает последнюю ячейку</returns>
  private RectangleElement SetTopBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hastop)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool isRow = (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hastop1 = hastop;
        if (!isRow && index != 0)
          hastop1 = true;
        rectangleElement = this.SetTopBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hastop1);
      }
    }
    else if (!hastop)
    {
      BorderLine borderLine1 = cell.TopBorderLine.Clone();
      Color? colorTe = cur_var.ColorTE;
      if (colorTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        colorTe = cur_var.ColorTE;
        Color color = colorTe.Value;
        borderLine2.Color = color;
      }
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine3.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine4.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine5 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine5.SerifWidth = (float) num;
      }
      cell.AssignTopBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetBottomBorderLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hasbottom)
  {
    bool hasbottom1 = hasbottom;
    bool bottomBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      if (!isRow && cell.Nodes.Count > 1)
        hasbottom1 = true;
      bottomBorderLineTe1 = this.GetBottomBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasbottom1);
      if (!bottomBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasbottom2 = hasbottom;
        if (!isRow && index != count - 1)
          hasbottom2 = true;
        bottomBorderLineTe1 = this.GetBottomBorderLineTE(node, ref cur_var, hasbottom2);
        if (!bottomBorderLineTe1)
          return false;
      }
    }
    else if (!hasbottom)
    {
      BorderLine bottomBorderLine = cell.BottomBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(bottomBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = bottomBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = bottomBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = bottomBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = bottomBorderLine.SerifWidth;
      bool bottomBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        bottomBorderLineTe2 = false;
      }
      else
        bottomBorderLineTe2 = true;
      return bottomBorderLineTe2;
    }
    return bottomBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetBottomBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasbottom)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool isRow = (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasbottom1 = hasbottom;
        if (!isRow && index != count - 1)
          hasbottom1 = true;
        rectangleElement = this.SetBottomBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasbottom1);
      }
    }
    else if (!hasbottom)
    {
      BorderLine borderLine1 = cell.BottomBorderLine.Clone();
      Color? colorTe = cur_var.ColorTE;
      if (colorTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        colorTe = cur_var.ColorTE;
        Color color = colorTe.Value;
        borderLine2.Color = color;
      }
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine3.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine4.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine5 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine5.SerifWidth = (float) num;
      }
      cell.AssignBottomBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetLeftBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hasleft)
  {
    bool hasleft1 = hasleft;
    bool leftBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      leftBorderLineTe1 = this.GetLeftBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasleft1);
      if (!leftBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasleft2 = hasleft;
        if (isRow)
          hasleft2 = true;
        leftBorderLineTe1 = this.GetLeftBorderLineTE(node, ref cur_var, hasleft2);
        if (!leftBorderLineTe1)
          return false;
      }
    }
    else if (!hasleft)
    {
      BorderLine leftBorderLine = cell.LeftBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(leftBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = leftBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = leftBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = leftBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = leftBorderLine.SerifWidth;
      bool leftBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        leftBorderLineTe2 = false;
      }
      else
        leftBorderLineTe2 = true;
      return leftBorderLineTe2;
    }
    return leftBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetLeftBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasleft)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool isRow = (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasleft1 = hasleft;
        if (isRow && index != 0)
          hasleft1 = true;
        rectangleElement = this.SetLeftBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasleft1);
      }
    }
    else if (!hasleft)
    {
      BorderLine borderLine1 = cell.LeftBorderLine.Clone();
      Color? colorTe = cur_var.ColorTE;
      if (colorTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        colorTe = cur_var.ColorTE;
        Color color = colorTe.Value;
        borderLine2.Color = color;
      }
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine3.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine4.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine5 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine5.SerifWidth = (float) num;
      }
      cell.AssignLeftBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetRightBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hasright)
  {
    bool hasright1 = hasright;
    bool rightBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      if (isRow && cell.Nodes.Count > 1)
        hasright1 = true;
      rightBorderLineTe1 = this.GetRightBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasright1);
      if (!rightBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasright2 = hasright;
        if (isRow && index != count - 1)
          hasright2 = true;
        rightBorderLineTe1 = this.GetRightBorderLineTE(node, ref cur_var, hasright2);
        if (!rightBorderLineTe1)
          return false;
      }
    }
    else if (!hasright)
    {
      BorderLine rightBorderLine = cell.RightBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(rightBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = rightBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = rightBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = rightBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = rightBorderLine.SerifWidth;
      bool rightBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        rightBorderLineTe2 = false;
      }
      else
        rightBorderLineTe2 = true;
      return rightBorderLineTe2;
    }
    return rightBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetRightBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasright)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool isRow = (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasright1 = hasright;
        if (isRow && index != count - 1)
          hasright1 = true;
        rectangleElement = this.SetRightBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasright1);
      }
    }
    else if (!hasright)
    {
      BorderLine borderLine1 = cell.RightBorderLine.Clone();
      Color? colorTe = cur_var.ColorTE;
      if (colorTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        colorTe = cur_var.ColorTE;
        Color color = colorTe.Value;
        borderLine2.Color = color;
      }
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine3.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine4.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine5 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine5.SerifWidth = (float) num;
      }
      cell.AssignRightBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetInnerHorizontalLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hastop,
    bool hasbottom)
  {
    bool hastop1 = hastop;
    bool hasbottom1 = hasbottom;
    bool horizontalLineTe = true;
    if (cell is TableData tableData && tableData.InnerBorderLine != null)
    {
      BorderLine bl = tableData.InnerBorderLine.Clone();
      if (bl != null)
      {
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(bl);
          horizontalLineTe = true;
        }
        else
        {
          Color? nullable1 = cur_var.ColorTE;
          Color color = bl.Color;
          bool flag;
          if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
          {
            BorderLineTE borderLineTe = cur_var;
            nullable1 = new Color?();
            Color? nullable2 = nullable1;
            borderLineTe.ColorTE = nullable2;
            flag = false;
          }
          else
            flag = true;
          BorderStyles? styleTe = cur_var.StyleTE;
          BorderStyles style = bl.Style;
          if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
          {
            cur_var.StyleTE = new BorderStyles?();
            flag = false;
          }
          else
            flag = true;
          float? widthTe = cur_var.WidthTE;
          float width = bl.Width;
          if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
          {
            cur_var.WidthTE = new float?();
            flag = false;
          }
          else
            flag = true;
          float? serifWidthTe = cur_var.SerifWidthTE;
          float serifWidth = bl.SerifWidth;
          if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
          {
            cur_var.SerifWidthTE = new float?();
            horizontalLineTe = false;
          }
          else
            horizontalLineTe = true;
        }
      }
    }
    if (!cell.IsSingleCell)
    {
      if (!(cell is TableElement) || cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      if (!isRow && cell.Nodes.Count > 1)
        hasbottom1 = true;
      horizontalLineTe = this.GetInnerHorizontalLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hastop1, hasbottom1);
      if (!horizontalLineTe)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hastop2 = hastop;
        bool hasbottom2 = hasbottom;
        if (!isRow)
        {
          hastop2 = true;
          if (index != count - 1)
            hasbottom2 = true;
        }
        horizontalLineTe = this.GetInnerHorizontalLineTE(node, ref cur_var, hastop2, hasbottom2);
        if (!horizontalLineTe)
          return false;
      }
    }
    return horizontalLineTe;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetInnerHorizontalLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hastop,
    bool hasbottom)
  {
    RectangleElement rectangleElement = cell;
    Color? colorTe;
    BorderStyles? styleTe;
    float? nullable;
    if (cell is TableData)
    {
      TableData tableData = cell as TableData;
      BorderLine borderLine1 = tableData.InnerBorderLine == null ? tableData.BottomBorderLine.Clone() : tableData.InnerBorderLine.Clone();
      if (cur_var.ColorTE.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        colorTe = cur_var.ColorTE;
        Color color = colorTe.Value;
        borderLine2.Color = color;
      }
      if (cur_var.StyleTE.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine3.Style = (BorderStyles) num;
      }
      if (cur_var.WidthTE.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine4.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine5 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine5.SerifWidth = (float) num;
      }
      tableData.AssignInnerBorderLine(borderLine1, false);
    }
    if (cell.IsSingleCell)
    {
      if (hastop)
      {
        BorderLine borderLine6 = cell.TopBorderLine.Clone();
        colorTe = cur_var.ColorTE;
        if (colorTe.HasValue)
        {
          BorderLine borderLine7 = borderLine6;
          colorTe = cur_var.ColorTE;
          Color color = colorTe.Value;
          borderLine7.Color = color;
        }
        styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine8 = borderLine6;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine8.Style = (BorderStyles) num;
        }
        nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine9 = borderLine6;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine9.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine10 = borderLine6;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine10.SerifWidth = (float) num;
        }
        cell.AssignTopBorderLine(borderLine6, false);
        return cell;
      }
      if (hasbottom)
      {
        BorderLine borderLine11 = cell.BottomBorderLine.Clone();
        colorTe = cur_var.ColorTE;
        if (colorTe.HasValue)
        {
          BorderLine borderLine12 = borderLine11;
          colorTe = cur_var.ColorTE;
          Color color = colorTe.Value;
          borderLine12.Color = color;
        }
        styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine13 = borderLine11;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine13.Style = (BorderStyles) num;
        }
        nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine14 = borderLine11;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine14.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine15 = borderLine11;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine15.SerifWidth = (float) num;
        }
        cell.AssignBottomBorderLine(borderLine11, false);
        return cell;
      }
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetInnerVerticalLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hasleft,
    bool hasright)
  {
    bool hasleft1 = hasleft;
    bool hasright1 = hasright;
    bool innerVerticalLineTe1 = true;
    bool flag;
    BorderStyles? nullable1;
    float? nullable2;
    if (cell is TableData)
    {
      TableData tableData = cell as TableData;
      if (tableData.GridColumnsParams != null)
      {
        for (int index = 0; index < tableData.GridColumnsParams.Count; ++index)
        {
          if ((hasleft ? 1 : (index != 0 ? 1 : 0)) != 0)
          {
            BorderLine bl = tableData.GridColumnsParams[index].BorderLine1 == null ? this.DefaultBorderLine.Clone() : tableData.GridColumnsParams[index].BorderLine1.Clone();
            if (cur_var == null)
            {
              cur_var = new BorderLineTE(bl);
              innerVerticalLineTe1 = true;
            }
            else
            {
              Color? colorTe = cur_var.ColorTE;
              Color color = bl.Color;
              if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
              {
                cur_var.ColorTE = new Color?();
                flag = false;
              }
              else
                flag = true;
              nullable1 = cur_var.StyleTE;
              BorderStyles style = bl.Style;
              if (!(nullable1.GetValueOrDefault() == style & nullable1.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable1 = new BorderStyles?();
                BorderStyles? nullable3 = nullable1;
                borderLineTe.StyleTE = nullable3;
                flag = false;
              }
              else
                flag = true;
              nullable2 = cur_var.WidthTE;
              float width = bl.Width;
              if (!((double) nullable2.GetValueOrDefault() == (double) width & nullable2.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable2 = new float?();
                float? nullable4 = nullable2;
                borderLineTe.WidthTE = nullable4;
                flag = false;
              }
              else
                flag = true;
              nullable2 = cur_var.SerifWidthTE;
              float serifWidth = bl.SerifWidth;
              if (!((double) nullable2.GetValueOrDefault() == (double) serifWidth & nullable2.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable2 = new float?();
                float? nullable5 = nullable2;
                borderLineTe.SerifWidthTE = nullable5;
                innerVerticalLineTe1 = false;
              }
              else
                innerVerticalLineTe1 = true;
            }
          }
          if ((hasright ? 1 : (index != tableData.GridColumnsParams.Count - 1 ? 1 : 0)) != 0)
          {
            BorderLine bl = tableData.GridColumnsParams[index].BorderLine2 == null ? this.DefaultBorderLine.Clone() : tableData.GridColumnsParams[index].BorderLine2.Clone();
            if (cur_var == null)
            {
              cur_var = new BorderLineTE(bl);
              innerVerticalLineTe1 = true;
            }
            else
            {
              Color? colorTe = cur_var.ColorTE;
              Color color = bl.Color;
              if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
              {
                cur_var.ColorTE = new Color?();
                flag = false;
              }
              else
                flag = true;
              nullable1 = cur_var.StyleTE;
              BorderStyles style = bl.Style;
              if (!(nullable1.GetValueOrDefault() == style & nullable1.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable1 = new BorderStyles?();
                BorderStyles? nullable6 = nullable1;
                borderLineTe.StyleTE = nullable6;
                flag = false;
              }
              else
                flag = true;
              nullable2 = cur_var.WidthTE;
              float width = bl.Width;
              if (!((double) nullable2.GetValueOrDefault() == (double) width & nullable2.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable2 = new float?();
                float? nullable7 = nullable2;
                borderLineTe.WidthTE = nullable7;
                flag = false;
              }
              else
                flag = true;
              nullable2 = cur_var.SerifWidthTE;
              float serifWidth = bl.SerifWidth;
              if (!((double) nullable2.GetValueOrDefault() == (double) serifWidth & nullable2.HasValue))
              {
                BorderLineTE borderLineTe = cur_var;
                nullable2 = new float?();
                float? nullable8 = nullable2;
                borderLineTe.SerifWidthTE = nullable8;
                innerVerticalLineTe1 = false;
              }
              else
                innerVerticalLineTe1 = true;
            }
          }
        }
      }
    }
    if (!cell.IsSingleCell)
    {
      if (!(cell is TableElement) || cell.Nodes.Count == 0)
        return true;
      bool isRow = (cell as TableElement).IsRow;
      if (isRow && cell.Nodes.Count > 1)
        hasright1 = true;
      innerVerticalLineTe1 = this.GetInnerVerticalLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasleft1, hasright1);
      if (!innerVerticalLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasleft2 = hasleft;
        if (isRow)
        {
          hasleft2 = true;
          hasright1 = hasright;
          if (index != count - 1)
            hasright1 = true;
        }
        innerVerticalLineTe1 = this.GetInnerVerticalLineTE(node, ref cur_var, hasleft2, hasright1);
        if (!innerVerticalLineTe1)
          return false;
      }
    }
    else
    {
      if (hasleft)
      {
        BorderLine leftBorderLine = cell.LeftBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(leftBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = leftBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        nullable1 = cur_var.StyleTE;
        BorderStyles style = leftBorderLine.Style;
        if (!(nullable1.GetValueOrDefault() == style & nullable1.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable1 = new BorderStyles?();
          BorderStyles? nullable9 = nullable1;
          borderLineTe.StyleTE = nullable9;
          flag = false;
        }
        else
          flag = true;
        nullable2 = cur_var.WidthTE;
        float width = leftBorderLine.Width;
        if (!((double) nullable2.GetValueOrDefault() == (double) width & nullable2.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable2 = new float?();
          float? nullable10 = nullable2;
          borderLineTe.WidthTE = nullable10;
          flag = false;
        }
        else
          flag = true;
        nullable2 = cur_var.SerifWidthTE;
        float serifWidth = leftBorderLine.SerifWidth;
        bool innerVerticalLineTe2;
        if (!((double) nullable2.GetValueOrDefault() == (double) serifWidth & nullable2.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable2 = new float?();
          float? nullable11 = nullable2;
          borderLineTe.SerifWidthTE = nullable11;
          innerVerticalLineTe2 = false;
        }
        else
          innerVerticalLineTe2 = true;
        return innerVerticalLineTe2;
      }
      if (hasright1)
      {
        BorderLine rightBorderLine = cell.RightBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(rightBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = rightBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        nullable1 = cur_var.StyleTE;
        BorderStyles style = rightBorderLine.Style;
        if (!(nullable1.GetValueOrDefault() == style & nullable1.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable1 = new BorderStyles?();
          BorderStyles? nullable12 = nullable1;
          borderLineTe.StyleTE = nullable12;
          flag = false;
        }
        else
          flag = true;
        nullable2 = cur_var.WidthTE;
        float width = rightBorderLine.Width;
        if (!((double) nullable2.GetValueOrDefault() == (double) width & nullable2.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable2 = new float?();
          float? nullable13 = nullable2;
          borderLineTe.WidthTE = nullable13;
          flag = false;
        }
        else
          flag = true;
        nullable2 = cur_var.SerifWidthTE;
        float serifWidth = rightBorderLine.SerifWidth;
        bool innerVerticalLineTe3;
        if (!((double) nullable2.GetValueOrDefault() == (double) serifWidth & nullable2.HasValue))
        {
          BorderLineTE borderLineTe = cur_var;
          nullable2 = new float?();
          float? nullable14 = nullable2;
          borderLineTe.SerifWidthTE = nullable14;
          innerVerticalLineTe3 = false;
        }
        else
          innerVerticalLineTe3 = true;
        return innerVerticalLineTe3;
      }
    }
    return innerVerticalLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetInnerVerticalLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasleft,
    bool hasright)
  {
    RectangleElement rectangleElement = cell;
    Color? colorTe;
    BorderStyles? styleTe;
    float? nullable;
    if (cell is TableData)
    {
      TableData tableData = cell as TableData;
      if (tableData.GridColumnsParams != null)
      {
        for (int index = 0; index < tableData.GridColumnsParams.Count; ++index)
        {
          if ((hasleft ? 1 : (index != 0 ? 1 : 0)) != 0)
          {
            BorderLine borderLine1 = tableData.GridColumnsParams[index].BorderLine1 == null ? this.DefaultBorderLine.Clone() : tableData.GridColumnsParams[index].BorderLine1.Clone();
            colorTe = cur_var.ColorTE;
            if (colorTe.HasValue)
            {
              BorderLine borderLine2 = borderLine1;
              colorTe = cur_var.ColorTE;
              Color color = colorTe.Value;
              borderLine2.Color = color;
            }
            styleTe = cur_var.StyleTE;
            if (styleTe.HasValue)
            {
              BorderLine borderLine3 = borderLine1;
              styleTe = cur_var.StyleTE;
              int num = (int) styleTe.Value;
              borderLine3.Style = (BorderStyles) num;
            }
            nullable = cur_var.WidthTE;
            if (nullable.HasValue)
            {
              BorderLine borderLine4 = borderLine1;
              nullable = cur_var.WidthTE;
              double num = (double) nullable.Value;
              borderLine4.Width = (float) num;
            }
            nullable = cur_var.SerifWidthTE;
            if (nullable.HasValue)
            {
              BorderLine borderLine5 = borderLine1;
              nullable = cur_var.SerifWidthTE;
              double num = (double) nullable.Value;
              borderLine5.SerifWidth = (float) num;
            }
            tableData.GridColumnsParams[index].AssignBorderLine1(borderLine1);
          }
          if ((hasright ? 1 : (index != tableData.GridColumnsParams.Count - 1 ? 1 : 0)) != 0)
          {
            BorderLine borderLine6 = tableData.GridColumnsParams[index].BorderLine2 == null ? this.DefaultBorderLine.Clone() : tableData.GridColumnsParams[index].BorderLine2.Clone();
            colorTe = cur_var.ColorTE;
            if (colorTe.HasValue)
            {
              BorderLine borderLine7 = borderLine6;
              colorTe = cur_var.ColorTE;
              Color color = colorTe.Value;
              borderLine7.Color = color;
            }
            styleTe = cur_var.StyleTE;
            if (styleTe.HasValue)
            {
              BorderLine borderLine8 = borderLine6;
              styleTe = cur_var.StyleTE;
              int num = (int) styleTe.Value;
              borderLine8.Style = (BorderStyles) num;
            }
            nullable = cur_var.WidthTE;
            if (nullable.HasValue)
            {
              BorderLine borderLine9 = borderLine6;
              nullable = cur_var.WidthTE;
              double num = (double) nullable.Value;
              borderLine9.Width = (float) num;
            }
            nullable = cur_var.SerifWidthTE;
            if (nullable.HasValue)
            {
              BorderLine borderLine10 = borderLine6;
              nullable = cur_var.SerifWidthTE;
              double num = (double) nullable.Value;
              borderLine10.SerifWidth = (float) num;
            }
            tableData.GridColumnsParams[index].AssignBorderLine2(borderLine6);
          }
        }
      }
    }
    if (!cell.IsSingleCell)
    {
      bool isRow = (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasleft1 = hasleft;
        bool hasright1 = hasright;
        if (isRow)
        {
          if (index != 0)
            hasleft1 = true;
          if (index != count - 1)
            hasright1 = true;
        }
        rectangleElement = this.SetInnerVerticalLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasleft1, hasright1);
      }
    }
    else
    {
      if (hasleft)
      {
        BorderLine borderLine11 = cell.LeftBorderLine.Clone();
        colorTe = cur_var.ColorTE;
        if (colorTe.HasValue)
        {
          BorderLine borderLine12 = borderLine11;
          colorTe = cur_var.ColorTE;
          Color color = colorTe.Value;
          borderLine12.Color = color;
        }
        styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine13 = borderLine11;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine13.Style = (BorderStyles) num;
        }
        nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine14 = borderLine11;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine14.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine15 = borderLine11;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine15.SerifWidth = (float) num;
        }
        cell.AssignLeftBorderLine(borderLine11, false);
        return cell;
      }
      if (hasright)
      {
        BorderLine borderLine16 = cell.RightBorderLine.Clone();
        colorTe = cur_var.ColorTE;
        if (colorTe.HasValue)
        {
          BorderLine borderLine17 = borderLine16;
          colorTe = cur_var.ColorTE;
          Color color = colorTe.Value;
          borderLine17.Color = color;
        }
        styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine18 = borderLine16;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine18.Style = (BorderStyles) num;
        }
        nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine19 = borderLine16;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine19.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine20 = borderLine16;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine20.SerifWidth = (float) num;
        }
        cell.AssignRightBorderLine(borderLine16, false);
        return cell;
      }
    }
    return rectangleElement;
  }

  private string GetContinuationTableId()
  {
    return !this.HasContinuation() ? (string) null : this.NextTable.Id;
  }

  private void SetContinuationTableId(string value)
  {
    if (!this.IsTemplate)
      throw new Exception("Настройка таблицы продолжения возможно только в шаблоне");
    if (!this.IsPageFlow || !(this.GetContinuationTableId() != value))
      return;
    if (this.HasContinuation() && string.IsNullOrWhiteSpace(value))
    {
      TableData contTable = this.NextTable;
      if (this.Page.NextPage == null)
      {
        this.SetNextCell((RectangleElement) null);
      }
      else
      {
        this.SetNextCell(this.Page.NextPage.Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n is TableData tableData1 && tableData1.FlowID == this.FlowID)).Select<DocumentTreeNode, RectangleElement>((Func<DocumentTreeNode, RectangleElement>) (i => i as RectangleElement)).FirstOrDefault<RectangleElement>());
        RectangleElement rectangleElement = this.Page.NextPage.Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n is TableData tableData2 && tableData2.FlowID == contTable.FlowID)).Select<DocumentTreeNode, RectangleElement>((Func<DocumentTreeNode, RectangleElement>) (i => i as RectangleElement)).FirstOrDefault<RectangleElement>();
        contTable.SetNextCell(rectangleElement);
      }
    }
    else
    {
      DocumentTreeNode documentTreeNode = !(value == this.Id) ? this.FindNode(value) : throw new Exception($"Нельзя использовать собственный идентификатор таблицы '{value}' как свойство 'Таблица продолжения'");
      if (documentTreeNode == null)
        throw new Exception($"Не найден элемент документа c идентификатором: '{value}'");
      if (!(documentTreeNode is TableElement tableElement))
        throw new Exception($"Элемент документа c идентификатором: '{value}' не является таблицей");
      if (!tableElement.IsPageFlow)
        throw new Exception($"Нельзя использовать таблицу '{tableElement.GetDefautCaption()}' как свойство 'Таблица продолжения', так как она не поддерживает перенос данных по страницам");
      if (this.CanLinkWithLocalData((RectangleElement) tableElement))
      {
        if (this.HasContinuation())
          this.SetContinuationTableId((string) null);
        this.SetNextCell((RectangleElement) tableElement);
      }
    }
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Создать и вставить новую строку</summary>
  /// <param name="index">Индекс строки в Nodes</param>
  /// <param name="rowModel">Образец строки</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void InsertNewRow(
    int index,
    RectangleElement rowModel,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.IsColumn)
      return;
    bool flag = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag)
      this.SuspendUpdateLayout();
    try
    {
      RectangleElement child;
      if (rowModel != null)
      {
        child = (RectangleElement) rowModel.Clone(true, false);
        child.Name = (string) null;
      }
      else
      {
        List<RowColParams> gridColumnsParams = this.GridColumnsParams;
        if (gridColumnsParams != null && gridColumnsParams.Count > 0)
        {
          SizeF defaultCellSize = TableData.DefaultCellSize;
          if (this.IsFixedSizeRows)
            defaultCellSize.Height = this.DefaultRowSize;
          child = (RectangleElement) new TableElement(false, (DocumentTreeNode) null, new RectangleF(new PointF(0.0f, 0.0f), defaultCellSize), false);
          for (int gridIndex = 0; gridIndex < gridColumnsParams.Count; ++gridIndex)
          {
            RectangleElement[] gridColumnCells = ((TableData) child).CreateGridColumnCells(gridColumnsParams, gridIndex, child.Nodes.Count, false, false);
            if (gridColumnCells != null)
            {
              for (int index1 = 0; index1 < gridColumnCells.Length; ++index1)
                child.AddChildNode((DocumentTreeNode) gridColumnCells[index1], false, false);
            }
          }
        }
        else if (this.nodes.Count > 0)
        {
          child = (RectangleElement) this.nodes[this.nodes.Count - 1].Clone(true, false);
          child.Name = (string) null;
        }
        else
          child = (RectangleElement) new TextBoxElement((DocumentTreeNode) null, new RectangleF(new PointF(0.0f, 0.0f), TableData.DefaultCellSize), false);
      }
      if (child == null)
        return;
      this.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false, false);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateLayout(updateUI, true);
    }
  }

  /// <summary>Генерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    if (this.IsVirtualNode)
    {
      base.OnChildNodeRemoved(e);
    }
    else
    {
      e.Child.IdService = (IUniqueIdService) null;
      base.OnChildNodeRemoved(e);
    }
  }

  /// <summary>Создать пустую ячейку таблицы</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  /// <returns>Ячейка таблицы</returns>
  protected override RectangleElement CreateEmptySingleCell(
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (RectangleElement) new TextBoxElement(parent, bounds, visible);
  }

  /// <summary>Создать пустую таблицу</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  /// <returns>Таблица</returns>
  protected override TableData CreateEmptyTable(
    bool isColumn,
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (TableData) new TableElement(isColumn, parent, bounds, visible);
  }

  /// <summary>Получить тип для ячейки по умолчанию</summary>
  public override System.Type GetDataShowElementType() => typeof (TextBoxElement);

  /// <summary>Конструктор таблицы</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="tableParams">Параметры строк и столбцов</param>
  /// <param name="bounds">Границы</param>
  /// <param name="visible">Видимый</param>
  public TableElement(
    DocumentTreeNode parent,
    CreateTableParams tableParams,
    RectangleF bounds,
    bool visible)
    : base((DocumentTreeNode) null, bounds, visible)
  {
    bool flag = this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      this.SetVisible(false, false, false, false, true, false);
      List<RowColParams> rowColParamsList = new List<RowColParams>((IEnumerable<RowColParams>) tableParams.RowList);
      this.SetGridColumnsParams(new List<RowColParams>((IEnumerable<RowColParams>) tableParams.ColumnList), true, false);
      int num1 = tableParams.StdRowCount == -1 ? tableParams.RowList.Count : tableParams.StdRowCount;
      int num2 = tableParams.StdColCount == -1 ? tableParams.ColumnList.Count : tableParams.StdColCount;
      int num3 = num2 != 0 ? num2 : 1;
      int num4 = num1 != 0 ? num1 : 1;
      float height = 0.0f;
      if (tableParams.StdRowCount == -1)
      {
        for (int index = 0; index < rowColParamsList.Count; ++index)
          height += rowColParamsList[index].Size;
      }
      else
        height = rowColParamsList.Count <= 0 ? this.Bounds.Height : rowColParamsList[0].Size * (float) tableParams.StdRowCount;
      float width = 0.0f;
      if (tableParams.StdColCount == -1)
      {
        for (int index = 0; index < this.gridColumnsParams.Count; ++index)
          width += this.gridColumnsParams[index].Size;
      }
      else
        width = this.gridColumnsParams.Count <= 0 ? this.Bounds.Width : this.gridColumnsParams[0].Size * (float) tableParams.StdColCount;
      bounds.Size = new SizeF(width, height);
      if ((double) bounds.Width == 0.0 && (double) bounds.Height == 0.0)
      {
        bounds.Width = TableData.DefaultCellSize.Width * (float) num3;
        bounds.Height = TableData.DefaultCellSize.Height * (float) num4;
      }
      this.AssignBounds(bounds, false, false, false);
      SizeF size1 = new SizeF(0.0f, 0.0f);
      SizeF size2 = new SizeF(bounds.Width / (float) num3, size1.Height);
      PointF location = bounds.Location;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        string colRowName1;
        if (tableParams.StdRowCount == -1)
        {
          size1 = new SizeF(bounds.Width, rowColParamsList[index1].Size);
          colRowName1 = rowColParamsList[index1].ColRowName;
        }
        else
        {
          size1 = new SizeF(bounds.Width, rowColParamsList[0].Size);
          colRowName1 = rowColParamsList[0].ColRowName;
        }
        TableElement parent1 = new TableElement(false, (DocumentTreeNode) this, new RectangleF(location, size1), false);
        parent1.minHeight = size1.Height;
        parent1.Name = colRowName1;
        if (tableParams.FirstRowIsHeader && index1 == 0)
          parent1.TableCellType = CellType.Header;
        RectangleF bounds1;
        for (int index2 = 0; index2 < num2; ++index2)
        {
          string colRowName2;
          if (tableParams.StdColCount == -1)
          {
            size2 = new SizeF(this.gridColumnsParams[index2].Size, size1.Height);
            colRowName2 = this.gridColumnsParams[index2].ColRowName;
          }
          else
          {
            size2 = new SizeF(this.gridColumnsParams[0].Size, size1.Height);
            colRowName2 = this.gridColumnsParams[0].ColRowName;
          }
          TextData textData;
          if (tableParams.FirstRowIsHeader && index1 == 0)
          {
            textData = (TextData) new LabelElement((DocumentTreeNode) parent1, new RectangleF(location, size2), false);
            textData.TableCellType = CellType.Header;
            textData.ParagraphFormat = new ParagraphFormat();
            textData.ParagraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
            textData.ParagraphFormat.VertAlignment = new VertAlignment?(VertAlignment.Center);
            textData.Text = colRowName2;
          }
          else if (tableParams.FirstColIsHeader && index2 == 0)
          {
            textData = (TextData) new LabelElement((DocumentTreeNode) parent1, new RectangleF(location, size2), false);
            textData.TableCellType = CellType.Header;
            textData.ParagraphFormat = new ParagraphFormat();
            textData.ParagraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
            textData.ParagraphFormat.VertAlignment = new VertAlignment?(VertAlignment.Center);
            textData.Text = colRowName1;
          }
          else
            textData = (TextData) new TextBoxElement((DocumentTreeNode) parent1, new RectangleF(location, size2), false);
          RectangleElement rectangleElement = (RectangleElement) textData;
          rectangleElement.Name = colRowName2;
          ref PointF local = ref location;
          bounds1 = rectangleElement.Bounds;
          double right = (double) bounds1.Right;
          local.X = (float) right;
        }
        ref PointF local1 = ref location;
        double x = (double) bounds.X;
        bounds1 = parent1.Bounds;
        double bottom = (double) bounds1.Bottom;
        local1 = new PointF((float) x, (float) bottom);
      }
      this.SetVisible(visible, false, false, false, true, false);
      this.SetParent(parent, false, false);
    }
    finally
    {
      this.SetPropertiesChangedFlag(false, true, false, false, false);
      this.TreeStructureChangedFlag = false;
      this.ResetNeedUpdateLayoutFlag(true);
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected TableElement(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  public TableElement(bool isColumn, DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(isColumn, parent, bounds, visible)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  public TableElement(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  /// <summary>Конструктор</summary>
  public TableElement()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать инициализацию полей</param>
  public TableElement(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых необходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new TableElement();

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new TableElement(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать виртуальную таблицу</summary>
  /// <param name="parent">Родительский узел в виртуальном или реальном дереве</param>
  /// <param name="owner">Узел в реальном дереве, дочерние узлы которого представляет этот виртуальный узел</param>
  /// <returns>Виртуальная таблица</returns>
  internal static TableElement CreateVirtualTable(DocumentTreeNode parent, DocumentTreeNode owner)
  {
    TableElement virtualTable = new TableElement(true);
    virtualTable.SetIsVirtualNode(true);
    virtualTable.SetOwner(owner);
    virtualTable.SetParent(parent, false, false);
    return virtualTable;
  }

  /// <summary>Ссылка на источник данных таблицы</summary>
  [Editor(typeof (ReferenceToObjectUIEditor), typeof (UITypeEditor))]
  public override ReferenceBase Reference
  {
    get => base.Reference;
    set => base.Reference = value;
  }

  public override bool CanCallEditor
  {
    get
    {
      return this.ReadOnlyTE.HasValue && !this.ReadOnlyTE.Value && (base.CanCallEditor || this.IsTopLevelTable && !this.IsVirtualNode && this.Template == null && (!(this.Page is Intermech.Document.Model.Page) || (this.Page as Intermech.Document.Model.Page).DocumentControl == null || (this.Page as Intermech.Document.Model.Page).DocumentControl.DocumentEditorForm == null || (this.Page as Intermech.Document.Model.Page).DocumentControl.DocumentEditorForm.BaseEditCommandsEnabled));
    }
  }

  public override void CallEditor()
  {
    if (this.ReadOnlyTE.HasValue && this.ReadOnlyTE.Value)
      return;
    int num = 0;
    bool flag = false;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
    {
      flag = (this.OwnerDocument.UndoManager as UndoManager).MultyActionCreation;
      num = (this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568")) as UndoMultyAction).Actions.Count;
    }
    DialogResult dialogResult = TableEditorDialog.Execute(this, false);
    if (this.OwnerDocument == null || this.OwnerDocument.IsLoading || this.OwnerDocument.UndoManager == null)
      return;
    UndoMultyAction multyUndo = this.OwnerDocument.UndoManager.EndCreateMultyUndo() as UndoMultyAction;
    if (dialogResult != DialogResult.Cancel || multyUndo == null || multyUndo.Actions.Count == 0)
      return;
    if (!flag)
    {
      this.OwnerDocument.UndoManager.DoUndo();
    }
    else
    {
      for (int index = multyUndo.Actions.Count - 1; index >= num; --index)
      {
        multyUndo.Actions[index].DoAction();
        multyUndo.Actions.RemoveAt(index);
      }
    }
  }

  /// <summary>Создать элемент типа TextBoxElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToTextBox()
  {
    TextBoxElement child = new TextBoxElement((RectangleElement) this);
    DocumentTreeNode parent = this.Parent;
    VisualNode visualNode = parent as VisualNode;
    if (parent == null)
      return;
    int index = this.Index;
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag && visualNode != null)
      visualNode.SuspendUpdateGeometryRefreshUI();
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      parent.SuspendUpdateLayout();
    try
    {
      parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
      parent.RemoveChildNodeAt(index + 1, false, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        parent.ResumeUpdateLayout(false, true);
      if (!updateUiGeometryFlag && visualNode != null)
        visualNode.ResumeUpdateUIGeometry(true, true);
    }
  }

  /// <summary>Проверить можно ли вставить объект из буфера в этот узел</summary>
  /// <param name="nodeClipboardInfo">Информация об узле в буфере</param>
  /// <returns>Возвращает true, если объект из буфера можно ли вставить в этот узел</returns>
  public override bool CanPasteFromClipboard(NodeClipboardInfo nodeClipboardInfo)
  {
    return nodeClipboardInfo.NodeType == typeof (VirtualColumn) && this.IsColumnGridOwner() || base.CanPasteFromClipboard(nodeClipboardInfo);
  }
}
