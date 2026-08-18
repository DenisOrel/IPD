// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TableData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Базовый класс таблицы</summary>
/// <remarks>
/// Таблица может содержать:
/// - наследник RectangleElement если это визуальная таблица
/// - TextData, если это таблица данных.
///  </remarks>
[Serializable]
public class TableData : 
  RectangleElement,
  IFlowElement,
  IParentFlow,
  INodeWithReference,
  IEnumerable<RectangleElement>,
  IEnumerable
{
  /// <summary>Размер ячейки по умолчанию</summary>
  public static SizeF DefaultCellSize = new SizeF(20f, 5f);
  /// <summary>Неограниченный размер</summary>
  protected static float UnconstrainedSize = float.MaxValue;
  /// <summary>Имя типа для словаря конструкторов</summary>
  internal static string TypeNameForConstructorDictionary = "TableElement";
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Interfaces.Document_122");
  [NonSerialized]
  private ChildNodeAdded_EventHandler childNodeAddedInFlowChain;
  [NonSerialized]
  private ChildNodeRemoved_EventHandler childNodeRemovedInFlowChain;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private bool autoSizeHeight;
  private bool alignLastRows = true;
  private bool usePreviousTableTemplates;
  protected bool isColumn = true;
  private bool isFixedStructureArea;
  private bool isPageFlow;
  private bool drawGridToBottom = true;
  protected bool? showSingleCellInTemplate;
  [ExternalLink]
  private FlowID flowID;
  /// <summary>Столбцы сетки</summary>
  protected List<RowColParams> gridColumnsParams;
  /// <summary>Строки сетки</summary>
  protected List<RowColParams> gridRowsParams;
  private ReferenceBase reference;
  [ExternalLink]
  private IParentFlow parentFlow;
  [ExternalLink]
  private IFlowElement prevFlowElement;
  [ExternalLink]
  private IFlowElement nextFlowElement;
  /// <summary>Буфер разбивки элементов по страницам</summary>
  [NonSerialized]
  private List<RectangleElement> distributeBuffer;
  /// <summary>Заблокированные заголовки</summary>
  private List<string> disabledHeaders;
  /// <summary>Текущее свободное место для размещения потока данных</summary>
  internal SizeF FreeSpace = new SizeF(0.0f, 0.0f);
  protected float cellsMinHeight;
  [NonSerialized]
  private int distributingCount;
  [NonSerialized]
  protected object tag;
  [NonSerialized]
  private BeforeDistribute_EventHandler beforeDistribute;

  /// <summary>Копировать параметры строк/столбцов</summary>
  /// <param name="rowColParams">Оригинал параметров</param>
  /// <returns>Копия параметров</returns>
  protected static List<RowColParams> CloneRowColParams(List<RowColParams> rowColParams)
  {
    if (rowColParams == null)
      return (List<RowColParams>) null;
    List<RowColParams> rowColParamsList = new List<RowColParams>(rowColParams.Count);
    for (int index = 0; index < rowColParams.Count; ++index)
    {
      if (rowColParams[index] != null)
        rowColParamsList.Add(rowColParams[index].Clone());
      else
        rowColParamsList.Add((RowColParams) null);
    }
    return rowColParamsList;
  }

  /// <summary>Копировать параметры строк/столбцов из шаблона</summary>
  /// <param name="template">Шаблон сетки</param>
  /// <returns>Копия параметров</returns>
  protected static List<RowColParams> CloneRowColParamsFromTemplate(List<RowColParams> template)
  {
    List<RowColParams> rowColParamsList = new List<RowColParams>(template.Count);
    for (int index = 0; index < template.Count; ++index)
    {
      if (template[index] != null)
      {
        rowColParamsList.Add(template[index].Clone());
        rowColParamsList[index].TemplateID = template[index].ID;
      }
      else
        rowColParamsList.Add((RowColParams) null);
    }
    return rowColParamsList;
  }

  /// <summary>Получить параметры элемента сетки по заданному идентификатору</summary>
  /// <param name="gridParams">Сетка</param>
  /// <param name="id">Идентификатор элемента сетки</param>
  /// <returns>Параметры элемента сетки</returns>
  public static RowColParams GetRowColParams(List<RowColParams> gridParams, int id)
  {
    int rowColIndex = TableData.GetRowColIndex(gridParams, id);
    return rowColIndex != -1 ? gridParams[rowColIndex] : (RowColParams) null;
  }

  /// <summary>Получить индекс элемента сетки с заданным идентификатором</summary>
  /// <param name="gridParams">Сетка</param>
  /// <param name="id">Идентификатор элемента</param>
  /// <returns>Индекс элемента в сетке</returns>
  public static int GetRowColIndex(List<RowColParams> gridParams, int id)
  {
    if (gridParams != null)
    {
      for (int index = 0; index < gridParams.Count; ++index)
      {
        if (gridParams[index].ID == id)
          return index;
      }
    }
    return -1;
  }

  /// <summary>Количество заголовков в таблице</summary>
  [Browsable(false)]
  public int HeadersCount
  {
    [DebuggerStepThrough] get
    {
      int headersCount = 0;
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          if (node.TableCellType == CellType.Header)
            ++headersCount;
          else
            break;
        }
      }
      return headersCount;
    }
  }

  /// <summary>Параметры столбцов сетки таблицы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_512")]
  [CustomDescription("Attribute.Interfaces.Document_513")]
  [Category("Debug")]
  [TypeConverter(typeof (GridColumnsConverter))]
  public List<RowColParams> GridColumnsParams
  {
    [DebuggerStepThrough] get
    {
      return this.gridColumnsParams != null ? this.gridColumnsParams : this.GetGridColumnsParams(false);
    }
  }

  /// <summary>Показ одной/всех строк динамической таблицы в шаблоне</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_605")]
  [CustomDescription("Attribute.Interfaces.Document_606")]
  [CustomCategory("Attribute.Interfaces.Document_200")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(true)]
  public bool? ShowSingleCellInTemplate
  {
    get
    {
      if (!this.CanSwitchInternalCellsVisibity)
        return new bool?();
      this.showSingleCellInTemplate = new bool?(((int) this.showSingleCellInTemplate ?? (this.GetShowSingleCellInTemplateGlobal() ? 1 : 0)) != 0);
      return this.showSingleCellInTemplate;
    }
    set
    {
      if (!this.CanSwitchInternalCellsVisibity)
        return;
      bool? singleCellInTemplate = this.showSingleCellInTemplate;
      bool? nullable = value;
      if (singleCellInTemplate.GetValueOrDefault() == nullable.GetValueOrDefault() & singleCellInTemplate.HasValue == nullable.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (ShowSingleCellInTemplate), (object) this.ShowSingleCellInTemplate, (object) value);
        this.showSingleCellInTemplate = value;
        this.SetNeedUpdateLayoutFlag(true, true, true, true);
        this.SetPropertiesChangedFlag(true, true, false, true, true);
        this.OnChanged(new Changed_EventArgs());
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  protected virtual bool GetShowSingleCellInTemplateGlobal() => true;

  /// <summary>Назначить новый набор столбцов</summary>
  /// <param name="value">Набор столбцов</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия наследования</param>
  /// <param name="saveUndo">Сохранять изменения в Undo</param>
  public void SetGridColumnsParams(List<RowColParams> value, bool setOverrideFlag, bool saveUndo)
  {
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
    {
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Interfaces.Document_164"));
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "GridColumnsParams");
    }
    if (this.gridColumnsParams != null)
    {
      for (int index = 0; index < this.gridColumnsParams.Count; ++index)
      {
        if (this.gridColumnsParams[index] != null)
          this.gridColumnsParams[index].SetOwnerTable((TableData) null);
      }
    }
    List<RowColParams> gridColumnsParams1 = this.gridColumnsParams;
    List<RowColParams> gridColumnsParams2 = this.gridColumnsParams;
    this.gridColumnsParams = value;
    if (setOverrideFlag)
    {
      OverrideFlags overrideFlags = this.overrideFlags;
      OverrideFlags2 overrideFlags2 = this.overrideFlags2;
      this.overrideFlags |= OverrideFlags.Grid;
      this.overrideFlags2 |= OverrideFlags2.ParentGrid;
      if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      {
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "overrideFlags", (object) overrideFlags, (object) this.overrideFlags);
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "overrideFlags2", (object) overrideFlags2, (object) this.overrideFlags2);
      }
    }
    if (this.gridColumnsParams != null)
    {
      for (int index = 0; index < this.gridColumnsParams.Count; ++index)
      {
        if (this.gridColumnsParams[index] != null)
        {
          this.gridColumnsParams[index].SetOwnerTable(this);
          this.gridColumnsParams[index].SetIsColumn(true);
        }
      }
    }
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.UpdateParentGridColumnsParams(this.gridColumnsParams);
      }
    }
    if (!saveUndo || this.OwnerDocument == null || this.OwnerDocument.IsLoading || this.OwnerDocument.UndoManager == null)
      return;
    this.OwnerDocument.UndoManager.EndCreateMultyUndo();
  }

  protected void UpdateParentGridColumnsParams(List<RowColParams> value)
  {
    if ((this.overrideFlags2 & OverrideFlags2.ParentGrid) != OverrideFlags2.None || this.gridColumnsParams == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "gridColumnsParams", (object) this.gridColumnsParams, (object) value);
    this.gridColumnsParams = value;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is TableData node)
        node.UpdateParentGridColumnsParams(this.gridColumnsParams);
    }
  }

  /// <summary>Получить параметры столбцов сетки таблицы</summary>
  /// <param name="paramsOwner">Владелец параметров</param>
  /// <param name="fromTemplate">Владелец параметров наследует их у своего шаблона</param>
  /// <param name="autoCreate">Создать параметры по умолчанию, если их нет</param>
  /// <param name="update">Обновлять значение gridColumnsParams</param>
  /// <returns>Параметры столбцов сетки таблицы</returns>
  public List<RowColParams> GetGridColumnsParams(
    out TableData paramsOwner,
    out bool fromTemplate,
    bool autoCreate,
    bool update)
  {
    paramsOwner = (TableData) null;
    fromTemplate = false;
    List<RowColParams> forThisTableOnly = this.GetGridColParamsForThisTableOnly(out paramsOwner, out fromTemplate, update);
    if (forThisTableOnly != null)
      return forThisTableOnly;
    List<RowColParams> rowColParamsList1 = this.GetPrevTableGridColumnParams(out paramsOwner, autoCreate, update);
    if (rowColParamsList1 == null)
    {
      if (this.IsOverridden(OverrideFlags.Grid) && this.gridColumnsParams != null)
      {
        paramsOwner = this;
        rowColParamsList1 = this.gridColumnsParams;
      }
      TableData parentCell = this.ParentCell;
      if (this.Template is TableData template)
      {
        if (template.gridColumnsParams != null && (parentCell == null || this.IsOverridden2(OverrideFlags2.ParentGrid) && !this.IsOverridden(OverrideFlags.Grid)))
        {
          fromTemplate = true;
          paramsOwner = this;
          rowColParamsList1 = template.gridColumnsParams;
        }
        else if (this.UseGridFromOverrideTemplate())
        {
          rowColParamsList1 = template.GetGridColumnsParams(out paramsOwner, out fromTemplate, autoCreate, update);
          paramsOwner = this;
        }
      }
      if (!fromTemplate && parentCell != null && (rowColParamsList1 == null || !this.IsOverridden2(OverrideFlags2.ParentGrid) && !this.IsOverridden(OverrideFlags.Grid)))
      {
        if (rowColParamsList1 == null)
          this.overrideFlags2 &= ~OverrideFlags2.ParentGrid;
        rowColParamsList1 = parentCell.GetGridColumnsParams(out paramsOwner, out fromTemplate, autoCreate, update);
      }
      if (rowColParamsList1 == null && this.gridColumnsParams != null)
      {
        rowColParamsList1 = this.gridColumnsParams;
        if (parentCell != null && parentCell.GetGridColumnsParams(out paramsOwner, out fromTemplate, autoCreate, update) != rowColParamsList1)
          paramsOwner = this;
      }
      if (autoCreate && this.gridColumnsParams == null && rowColParamsList1 == null)
      {
        TableData tableData1 = (TableData) null;
        for (TableData tableData2 = this; tableData2 != null; tableData2 = tableData2.nodes[0] as TableData)
        {
          if (tableData2.IsRow)
          {
            tableData1 = tableData2;
            break;
          }
          if (tableData2.nodes.Count <= 0)
            break;
        }
        if (tableData1 != null)
        {
          List<RowColParams> rowColParamsList2 = new List<RowColParams>(tableData1.nodes.Count);
          int num = 0;
          for (int count = tableData1.nodes.Count; num < count; ++num)
          {
            if (tableData1.nodes[num] is RectangleElement node)
              rowColParamsList2.Add(new RowColParams(this, num, node.Name, node.Bounds.Width));
            else
              rowColParamsList2.Add((RowColParams) null);
          }
          this.SetGridColumnsParams(rowColParamsList2, true, false);
        }
        else
          this.SetGridColumnsParams(new List<RowColParams>(), true, false);
        rowColParamsList1 = this.gridColumnsParams;
        paramsOwner = this;
      }
    }
    if (!this.IsVirtualNode && paramsOwner != null && paramsOwner != this && rowColParamsList1 != null && rowColParamsList1.Count > 0 && this.gridColumnsParams == null | update && (double) paramsOwner.properBounds.Width != (double) this.properBounds.Width)
    {
      rowColParamsList1 = !fromTemplate ? TableData.CloneRowColParams(rowColParamsList1) : TableData.CloneRowColParamsFromTemplate(rowColParamsList1);
      if (update)
      {
        float num1 = 0.0f;
        for (int index = 0; index < rowColParamsList1.Count; ++index)
          num1 += rowColParamsList1[index].Size;
        float num2 = this.properBounds.Width - num1;
        if ((double) num2 > 0.0)
          rowColParamsList1[rowColParamsList1.Count - 1].Size += num2;
        else if ((double) num2 < 0.0)
        {
          float num3 = -num2;
          for (int index = rowColParamsList1.Count - 1; index >= 0; --index)
          {
            if ((double) num3 + 1.0 > (double) rowColParamsList1[index].Size)
            {
              num3 -= rowColParamsList1[index].Size - 1f;
              rowColParamsList1[index].Size = 1f;
            }
            else
            {
              rowColParamsList1[index].Size -= num3;
              break;
            }
          }
        }
      }
      this.SetGridColumnsParams(rowColParamsList1, false, false);
    }
    if (rowColParamsList1 != null)
      this.gridColumnsParams = rowColParamsList1;
    return this.gridColumnsParams;
  }

  private List<RowColParams> GetPrevTableGridColumnParams(
    out TableData paramsOwner,
    bool autoCreate,
    bool update)
  {
    paramsOwner = (TableData) null;
    if (this.PrevTable == null)
      return (List<RowColParams>) null;
    List<RowColParams> gridColParams;
    TableData gridColumnsParams = this.FindPrevTableWithGridColumnsParams(out gridColParams, out paramsOwner, update);
    if (gridColParams == null)
      gridColParams = gridColumnsParams.GetGridColumnsParams(out paramsOwner, out bool _, autoCreate, update);
    return gridColParams;
  }

  private TableData FindIncorrectDirectionInNextCellChain()
  {
    int index = this.page.Index;
    for (TableData directionInNextCellChain = this; directionInNextCellChain.NextCell != null; directionInNextCellChain = directionInNextCellChain.NextTable)
    {
      if (directionInNextCellChain.NextCell.Page.Index < directionInNextCellChain.Page.Index)
        return directionInNextCellChain;
    }
    return (TableData) null;
  }

  private TableData FindPrevTableWithGridColumnsParams(
    out List<RowColParams> gridColParams,
    out TableData paramsOwner,
    bool update)
  {
    gridColParams = (List<RowColParams>) null;
    paramsOwner = (TableData) null;
    TableData gridColumnsParams = this;
    int num = 0;
    while (gridColumnsParams.PrevTable != null)
    {
      ++num;
      gridColumnsParams = gridColumnsParams.PrevTable;
      gridColParams = gridColumnsParams.GetGridColParamsForThisTableOnly(out paramsOwner, out bool _, update);
      if (gridColParams != null)
        break;
    }
    return gridColumnsParams;
  }

  private List<RowColParams> GetGridColParamsForThisTableOnly(
    out TableData paramsOwner,
    out bool fromTemplate,
    bool update)
  {
    paramsOwner = (TableData) null;
    fromTemplate = false;
    if (!this.IsEmptyList((IList) this.gridColumnsParams) && !update)
    {
      if (this.gridColumnsParams[0] != null)
        paramsOwner = this.gridColumnsParams[0].OwnerTable;
      if (paramsOwner != null)
      {
        fromTemplate = paramsOwner.IsTemplate && !this.IsTemplate;
        return this.gridColumnsParams;
      }
    }
    return (List<RowColParams>) null;
  }

  /// <summary>Получить параметры столбцов сетки таблицы</summary>
  /// <param name="autoCreate">Создать параметры по умолчанию, если их нет</param>
  /// <param name="update">Обновлять значение gridColumnsParams</param>
  /// <returns>Параметры столбцов сетки таблицы</returns>
  public List<RowColParams> GetGridColumnsParams(bool autoCreate)
  {
    return this.gridColumnsParams != null ? this.gridColumnsParams : this.GetGridColumnsParams(out TableData _, out bool _, autoCreate, false);
  }

  private bool IsEmptyList(IList list) => list == null || list.Count == 0;

  /// <summary>Таблица владеет сеткой</summary>
  public bool IsColumnGridOwner()
  {
    return !this.IsEmptyList((IList) this.gridColumnsParams) && (this.overrideFlags2 & OverrideFlags2.ParentGrid) != 0;
  }

  /// <summary>Установить ширину столбца в сетке</summary>
  /// <param name="colIndex">Индекс столбца в сетке</param>
  /// <param name="width">Ширина столбца</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetGridColumnWidth(
    int colIndex,
    float width,
    bool setMinWidth,
    bool updateUI,
    bool updateLayout)
  {
    if (colIndex < 0)
      throw new ArgumentOutOfRangeException(nameof (colIndex), (object) colIndex, "colIndex < 0");
    TableData paramsOwner;
    List<RowColParams> gridColumnsParams = this.GetGridColumnsParams(out paramsOwner, out bool _, true, true);
    if (gridColumnsParams == null || colIndex >= gridColumnsParams.Count)
      return;
    float x = paramsOwner.bounds.X;
    for (int index = 0; index <= colIndex; ++index)
      x += gridColumnsParams[index].Size;
    double size = (double) gridColumnsParams[colIndex].Size;
    this.SetGridColumnWidth(colIndex, width, x, true, setMinWidth, updateUI, updateLayout);
  }

  /// <summary>Установить ширину столбца в сетке</summary>
  /// <param name="colIndex">Индекс столбца в сетке</param>
  /// <param name="width">Ширина столбца</param>
  /// <param name="oldPos">Старое положение изменяемой границы</param>
  /// <param name="isRightPos">Изменение ширины при перетаскивании правой границы таблицы</param>
  /// <param name="setMinWidth">Назначать минимальную ширину ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetGridColumnWidth(
    int colIndex,
    float width,
    float oldPos,
    bool isRightPos,
    bool setMinWidth,
    bool updateUI,
    bool updateLayout)
  {
    if (colIndex < 0)
      throw new ArgumentOutOfRangeException(nameof (colIndex), (object) colIndex, "colIndex < 0");
    TableData paramsOwner;
    List<RowColParams> gridColumnsParams = this.GetGridColumnsParams(out paramsOwner, out bool _, true, true);
    if (gridColumnsParams == null)
      return;
    if (colIndex >= gridColumnsParams.Count)
      throw new ArgumentOutOfRangeException(nameof (colIndex), (object) colIndex, LocalizationHolder.rm.GetString("Interfaces.Document_118"));
    float size = gridColumnsParams[colIndex].Size;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "size");
    gridColumnsParams[colIndex].AssignSize(width, false, false);
    if ((double) size != (double) gridColumnsParams[colIndex].Size)
      paramsOwner.overrideFlags |= OverrideFlags.Grid;
    List<DocumentTreeNode> columnCells = new List<DocumentTreeNode>();
    this.GetGridColumnCells(colIndex, gridColumnsParams, (IList<DocumentTreeNode>) columnCells);
    int index1 = 0;
    for (int count = columnCells.Count; index1 < count; ++index1)
    {
      if (columnCells[index1] is RectangleElement rectangleElement)
      {
        RectangleF bounds = rectangleElement.Bounds;
        if (!rectangleElement.WidthOverrided || isRightPos && (double) bounds.Right == (double) oldPos || !isRightPos && (double) bounds.X == (double) oldPos)
        {
          if (rectangleElement.IsDefaultGridPos || rectangleElement.GridPos.SpanCount == 1)
          {
            if (isRightPos && (double) bounds.Right == (double) oldPos || !isRightPos && (double) bounds.X == (double) oldPos)
              bounds.Width += width - size;
            else
              bounds.Width = width;
          }
          else if (isRightPos && (double) bounds.Right == (double) oldPos)
          {
            bounds.Width += width - size;
          }
          else
          {
            float num1 = 0.0f;
            int num2 = rectangleElement.GridPos.SpanCount;
            int gridColIndex = rectangleElement.GridColIndex;
            if (gridColIndex + num2 > gridColumnsParams.Count)
              num2 = gridColumnsParams.Count - gridColIndex;
            for (int index2 = gridColIndex; index2 < gridColIndex + num2; ++index2)
            {
              if (colIndex != index2)
                num1 += gridColumnsParams[index2].Size;
              else
                num1 += width;
            }
            bounds.Width = num1;
          }
          if ((double) rectangleElement.relativeWidth > 0.0)
            rectangleElement.RecalcRelativeSize();
          rectangleElement.SetCellSizes(bounds, false, true, false, true);
          rectangleElement.WidthOverrided = (double) bounds.Width != (double) width;
          if (setMinWidth)
            rectangleElement.AssignMinWidth(width, false, false, false);
        }
      }
    }
    paramsOwner.SetNeedUpdateLayoutFlag(true, true, false, false);
    if (updateLayout)
    {
      paramsOwner.UpdateLayout(updateUI);
    }
    else
    {
      if (!updateUI)
        return;
      this.UpdateUIGeometry(true);
    }
  }

  /// <summary>Переместить столбец на новую позицию</summary>
  /// <param name="gridColIndex">Положение колонки в сетке</param>
  /// <param name="newGridColIndex">Новое положение колонки</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void MoveGridColumn(
    int gridColIndex,
    int newGridColIndex,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.IsColumnGridOwner())
      return;
    this.MoveGridColumnCells(gridColIndex, newGridColIndex, this.gridColumnsParams, updateUI, updateLayout);
    ArrayEditHelper.MoveItem((IList) this.gridColumnsParams, gridColIndex, newGridColIndex);
  }

  /// <summary>Переместить ячейки столбца в строках на новую позицию</summary>
  /// <param name="gridColIndex">Индекс колонки в сетке</param>
  /// <param name="newGridColIndex">Новый индекс колонки</param>
  /// <param name="gridColumns">Параметры колонок сетки</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void MoveGridColumnCells(
    int gridColIndex,
    int newGridColIndex,
    List<RowColParams> gridColumns,
    bool updateUI,
    bool updateLayout)
  {
    if (this.GridColumnsParams != gridColumns)
      return;
    if (this.IsColumn)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.MoveGridColumnCells(gridColIndex, newGridColIndex, gridColumns, updateUI, updateLayout);
      }
    }
    else
    {
      RectangleElement[] cells;
      this.GetCellPositionForGridColumn(gridColIndex, false, out cells);
      if (cells == null || cells.Length == 0)
        return;
      int num = this.GetCellPositionForGridColumn(newGridColIndex, true, out RectangleElement[] _);
      if (num == -1)
        num = this.nodes.Count;
      for (int index = 0; index < cells.Length; ++index)
        this.MoveChildNode(cells[index].Index, num++, updateUI, updateLayout);
    }
  }

  /// <summary>Удалить столбец из принадлежащей таблице сетки</summary>
  /// <param name="gridColIndex">Индекс столбца в сетке</param>
  /// <param name="resizeTopTable">Уменьшить ширину таблицы, если она верхняя</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveGridColumn(
    int gridColIndex,
    bool resizeTopTable,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.IsColumnGridOwner())
      return;
    this.RemoveGridColumnCells(gridColIndex, this.gridColumnsParams, updateUI, updateLayout);
    bool flag = resizeTopTable && this.IsTopLevelTable;
    if (this.gridColumnsParams == null || gridColIndex >= this.gridColumnsParams.Count)
      return;
    if (flag)
    {
      RowColParams gridColumnsParam = this.gridColumnsParams[gridColIndex];
      RectangleF bounds = this.Bounds;
      bounds.Width -= gridColumnsParam.Size;
      this.AssignBounds(bounds, false, false, false);
    }
    float size = this.gridColumnsParams[gridColIndex].Size;
    this.gridColumnsParams.RemoveAt(gridColIndex);
    if (flag || this.gridColumnsParams.Count <= 0)
      return;
    this.gridColumnsParams[this.gridColumnsParams.Count - 1].Size += size;
  }

  /// <summary>Удалить ячейки столбца</summary>
  /// <param name="gridColIndex">Индекс столбца в сетке</param>
  /// <param name="gridColumns">Сетка</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveGridColumnCells(
    int gridColIndex,
    List<RowColParams> gridColumns,
    bool updateUI,
    bool updateLayout)
  {
    if (!RowColParams.IsEqual(gridColumns, this.GridColumnsParams))
      return;
    if (this.IsColumn)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.RemoveGridColumnCells(gridColIndex, gridColumns, updateUI, updateLayout);
      }
    }
    else
    {
      RectangleElement[] cells;
      this.GetCellPositionForGridColumn(gridColIndex, false, out cells);
      if (cells == null)
        return;
      for (int index = 0; index < cells.Length; ++index)
      {
        if (!cells[index].IsDefaultGridPos && cells[index].GridPos.SpanCount > 1)
          --cells[index].GridPos.SpanCount;
        else
          this.RemoveChildNodeAt(cells[index].Index, false, false);
      }
    }
  }

  /// <summary>Поменять местами столбцы сетки</summary>
  /// <param name="gridColIndex1">Индекс первого столбца</param>
  /// <param name="gridColIndex2">Индекс второго столбца</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ExchangeGridColumns(
    int gridColIndex1,
    int gridColIndex2,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.IsColumnGridOwner())
      return;
    this.ExchangeGridColumnsCells(gridColIndex1, gridColIndex2, this.gridColumnsParams, updateUI, updateLayout);
    RowColParams gridColumnsParam = this.gridColumnsParams[gridColIndex1];
    this.gridColumnsParams[gridColIndex1] = this.gridColumnsParams[gridColIndex2];
    this.gridColumnsParams[gridColIndex2] = gridColumnsParam;
  }

  /// <summary>Поменять местами ячейки столбцов сетки</summary>
  /// <param name="gridColIndex1">Индекс первого столбца</param>
  /// <param name="gridColIndex2">Индекс второго столбца</param>
  /// <param name="gridColumns">Сетка</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ExchangeGridColumnsCells(
    int gridColIndex1,
    int gridColIndex2,
    List<RowColParams> gridColumns,
    bool updateUI,
    bool updateLayout)
  {
    if (this.GridColumnsParams != gridColumns)
      return;
    if (this.IsColumn)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.ExchangeGridColumnsCells(gridColIndex1, gridColIndex2, gridColumns, updateUI, updateLayout);
      }
    }
    else
    {
      RectangleElement[] cells1;
      int positionForGridColumn1 = this.GetCellPositionForGridColumn(gridColIndex1, false, out cells1);
      RectangleElement[] cells2;
      int positionForGridColumn2 = this.GetCellPositionForGridColumn(gridColIndex2, false, out cells2);
      if (cells1 == null || cells1.Length != 1 || cells2 == null || cells2.Length != 1)
        return;
      this.nodes.Exchange(positionForGridColumn1, positionForGridColumn2);
    }
  }

  /// <summary>Сгенерировать идентификатор строки/столбца уникальный в пределах данной сетки</summary>
  /// <param name="gridParams">Параметры столбцов сетки</param>
  /// <param name="defaultID">Идентификатор по умолчанию. Если он уникален, то возвращает его</param>
  /// <returns>Идентификатор столбца</returns>
  public static int GenerateGridID(List<RowColParams> gridParams, int defaultID)
  {
    int num = 0;
    bool flag = true;
    if (defaultID < 0)
      defaultID = 1;
    if (gridParams != null)
    {
      for (int index = 0; index < gridParams.Count; ++index)
      {
        flag &= defaultID != gridParams[index].ID;
        if (num <= gridParams[index].ID)
          num = gridParams[index].ID + 1;
      }
    }
    return flag ? defaultID : num;
  }

  /// <summary>Создать и вставить новый столбец в сетку таблицы и его ячейки в таблицу</summary>
  /// <param name="gridIndex">Индекс столбца в сетке</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewGridColumn(int gridIndex, bool updateUI, bool updateLayout)
  {
    this.InsertNewGridColumn(gridIndex, new RowColParams(this, gridIndex, (string) null, TableData.DefaultCellSize.Width), updateUI, updateLayout);
  }

  /// <summary>Создать и вставить новый столбец в сетку таблицы и его ячейки в таблицу</summary>
  public void InsertNewGridColumn(
    int gridIndex,
    RowColParams colParams,
    bool updateUI,
    bool updateLayout)
  {
    this.InsertNewGridColumn(gridIndex, colParams, updateUI, updateLayout, true);
  }

  /// <summary>Создать и вставить новый столбец в сетку таблицы и его ячейки в таблицу</summary>
  /// <param name="gridIndex">Индекс столбца в сетке</param>
  /// <param name="colParams">Параметры столбца</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="expandTable">Расширять таблицу при добавлении столбца, иначе размер таблицы остаётся неизменным</param>
  public virtual void InsertNewGridColumn(
    int gridIndex,
    RowColParams colParams,
    bool updateUI,
    bool updateLayout,
    bool expandTable)
  {
    if (this.FindTableForAddColumn(true) != this)
      return;
    bool flag = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag)
      this.SuspendUpdateLayout();
    try
    {
      if (this.gridColumnsParams == null)
      {
        if (this.Template is TableData template && template.gridColumnsParams != null)
          this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(template.gridColumnsParams), true, true);
        else
          this.SetGridColumnsParams(new List<RowColParams>(), true, true);
      }
      colParams.ID = TableData.GenerateGridID(this.gridColumnsParams, colParams.ID);
      colParams.SetOwnerTable(this);
      this.gridColumnsParams.Insert(gridIndex, colParams);
      this.SetGridColumnsParams(this.gridColumnsParams, true, true);
      if (this.gridColumnsParams.Count != 1 & expandTable)
      {
        RectangleF properBounds = this.ProperBounds;
        properBounds.Width += colParams.Size;
        this.AssignProperBounds(properBounds, true, false, false);
      }
      this.InsertNewGridColumnCells(this.gridColumnsParams, gridIndex, updateUI, updateLayout);
      this.SetNeedUpdateLayoutFlag(true, true, false, false);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateLayout(updateUI, updateLayout);
    }
  }

  /// <summary>Создать ячейку для столбца</summary>
  /// <param name="gridColumns">Параметры столбцов сетки</param>
  /// <param name="gridIndex">Индекс столбца в сетке</param>
  /// <param name="nodeCellIndex">Индекс ячеек столбца в Nodes</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает новую ячейку</returns>
  protected virtual RectangleElement[] CreateGridColumnCells(
    List<RowColParams> gridColumns,
    int gridIndex,
    int nodeCellIndex,
    bool updateUI,
    bool updateLayout)
  {
    RectangleElement[] gridColumnCells = (RectangleElement[]) null;
    RowColParams rowColParams = (RowColParams) null;
    if (gridColumns != null && gridIndex < gridColumns.Count)
      rowColParams = gridColumns[gridIndex];
    TableData template = this.Template as TableData;
    if (rowColParams.HasTemplate && template != null)
    {
      RectangleElement[] cells = (RectangleElement[]) null;
      List<RowColParams> gridColumnsParams = template.GridColumnsParams;
      if (gridColumnsParams != null)
      {
        int rowColIndex = TableData.GetRowColIndex(gridColumnsParams, rowColParams.TemplateID);
        if (rowColIndex != -1)
          template.GetCellPositionForGridColumn(rowColIndex, false, out cells);
      }
      if (cells != null && cells.Length != 0)
      {
        gridColumnCells = new RectangleElement[cells.Length];
        for (int index = 0; index < cells.Length; ++index)
          gridColumnCells[index] = (RectangleElement) cells[index].CloneFromTemplate(true, true);
      }
    }
    else
    {
      Type dataShowElementType = this.GetDataShowElementType();
      gridColumnCells = typeof (RectangleElement).IsAssignableFrom(dataShowElementType) ? new RectangleElement[1]
      {
        (RectangleElement) Activator.CreateInstance(dataShowElementType)
      } : throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_119"));
      gridColumnCells[0].TableCellType = this.TableCellType;
      if (this.nodes.Count > 0)
      {
        if (this.TableCellType == CellType.Header && nodeCellIndex > 0 && nodeCellIndex - 1 < this.nodes.Count)
        {
          if (this.nodes[nodeCellIndex - 1] is RectangleElement node1 && node1.TableCellType != CellType.Header)
            gridColumnCells[0].TableCellType = node1.TableCellType;
        }
        else if (this.TableCellType == CellType.DataCell && nodeCellIndex + 1 < this.nodes.Count && this.nodes[nodeCellIndex + 1] is RectangleElement node2 && node2.TableCellType != CellType.DataCell)
          gridColumnCells[0].TableCellType = CellType.Header;
      }
      gridColumnCells[0].AssignMinHeight(this.MinHeight, false, false, false);
      gridColumnCells[0].MaxHeight = this.MaxHeight;
      string colRowName = rowColParams.ColRowName;
      gridColumnCells[0].Name = colRowName;
      if (!this.IsFixedStructureArea)
      {
        gridColumnCells[0].setProperBounds(new RectangleF(this.properBounds.X, this.properBounds.Y, rowColParams.Size, this.properBounds.Height));
      }
      else
      {
        gridColumnCells[0].setBounds(new RectangleF(this.properBounds.X, this.properBounds.Y, rowColParams.Size, this.properBounds.Height));
        gridColumnCells[0].setProperBounds(new RectangleF(0.0f, 0.0f, rowColParams.Size, this.properBounds.Height));
      }
    }
    return gridColumnCells;
  }

  /// <summary>Вставить ячейки нового столбца в сетке</summary>
  /// <param name="gridColumns">Набор колонок сетки</param>
  /// <param name="index">Индекс столбца в сетке</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewGridColumnCells(
    List<RowColParams> gridColumns,
    int index,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsRow && this.GridColumnsParams == gridColumns)
    {
      int nodeCellIndex = this.GetCellPositionForGridColumn(index, true, out RectangleElement[] _);
      if (nodeCellIndex == -1)
        nodeCellIndex = this.nodes.Count;
      RectangleElement[] gridColumnCells = this.CreateGridColumnCells(gridColumns, index, nodeCellIndex, updateUI, updateLayout);
      if (gridColumnCells == null)
        return;
      for (int index1 = 0; index1 < gridColumnCells.Length; ++index1)
      {
        gridColumnCells[index1].AssignNeedUpdateLayoutFlag(true);
        this.InsertChildNode(nodeCellIndex++, (DocumentTreeNode) gridColumnCells[index1], false, true, updateUI, updateLayout, false);
      }
    }
    else
    {
      for (int index2 = 0; index2 < this.nodes.Count; ++index2)
      {
        if (this.nodes[index2] is TableData node)
          node.InsertNewGridColumnCells(gridColumns, index, updateUI, updateLayout);
      }
    }
  }

  /// <summary>Получить позицию узла в Nodes где
  /// должна располагаться ячейка заданного столбца в сетке</summary>
  /// <param name="gridColIndex">Индекс столбца в сетке</param>
  /// <param name="firstCellOnly">Вернуть только первую найденную ячейку столбца</param>
  /// <param name="cells">Возвращает ячейки столбца, null если не найдены</param>
  /// <returns>Позицию ячейки в Nodes для заданного столбца в сетке</returns>
  public virtual int GetCellPositionForGridColumn(
    int gridColIndex,
    bool firstCellOnly,
    out RectangleElement[] cells)
  {
    int num = -1;
    int positionForGridColumn = -1;
    cells = (RectangleElement[]) null;
    if (this.IsRow)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          if (node.IsDefaultGridPos)
          {
            ++num;
            if (num < gridColIndex)
              positionForGridColumn = index;
            if (num == gridColIndex)
            {
              cells = new RectangleElement[1]{ node };
              positionForGridColumn = index;
              break;
            }
          }
          else
          {
            if (index == this.nodes.Count - 1 && this.GridColumnsParams != null && node.GridPos.SpanCount > this.GridColumnsParams.Count - num)
              node.GridPos.SpanCount = this.GridColumnsParams.Count - num;
            num += node.GridPos.SpanCount;
            if (num < gridColIndex)
              positionForGridColumn = index;
            else if (num > gridColIndex)
              num = gridColIndex;
            if (num == gridColIndex)
            {
              cells = new RectangleElement[1]{ node };
              positionForGridColumn = index;
              break;
            }
          }
        }
      }
      if (num < gridColIndex)
        ++positionForGridColumn;
    }
    return positionForGridColumn;
  }

  /// <summary>Получить ссылки на ячейки заданного столбца</summary>
  /// <param name="gridColIndex">Индекс столбца в сетке</param>
  /// <param name="gridColumns">Сетка в которой находится столбец</param>
  /// <param name="columnCells">Возвращает ячейки заданного столбца.
  /// Для нескольких ячеек одного столбца в строке создает VirtualColumnCells</param>
  public virtual void GetGridColumnCells(
    int gridColIndex,
    List<RowColParams> gridColumns,
    IList<DocumentTreeNode> columnCells)
  {
    if (this.GridColumnsParams != gridColumns)
      return;
    if (this.IsColumn)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
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
        for (int index = 0; index < cells.Length; ++index)
          columnCells.Add((DocumentTreeNode) cells[index]);
      }
    }
  }

  /// <summary>Параметры строк сетки таблицы</summary>
  [Category("Debug")]
  public List<RowColParams> GridRowsParams
  {
    [DebuggerStepThrough] get => this.GetGridRowsParams(out TableData _, out bool _);
  }

  /// <summary>Назначить новый набор строк</summary>
  /// <param name="value">Набор строк</param>
  protected void SetGridRowsParams(List<RowColParams> value)
  {
    if (this.gridRowsParams == value)
      return;
    if (this.gridRowsParams != null)
    {
      for (int index = 0; index < this.gridRowsParams.Count; ++index)
      {
        if (this.gridRowsParams[index] != null)
          this.gridRowsParams[index].SetOwnerTable((TableData) null);
      }
    }
    this.gridRowsParams = value;
    if (this.gridRowsParams == null)
      return;
    for (int index = 0; index < this.gridRowsParams.Count; ++index)
    {
      if (this.gridRowsParams[index] != null)
      {
        this.gridRowsParams[index].SetOwnerTable(this);
        this.gridRowsParams[index].SetIsColumn(false);
      }
    }
  }

  /// <summary>Получить параметры строк сетки таблицы</summary>
  /// <param name="paramsOwner">Владелец параметров</param>
  /// <param name="fromTemplate">Владелец параметров наследует их у своего шаблона</param>
  /// <returns>Параметры строк сетки таблицы</returns>
  public List<RowColParams> GetGridRowsParams(out TableData paramsOwner, out bool fromTemplate)
  {
    paramsOwner = (TableData) null;
    fromTemplate = false;
    if (this.gridRowsParams != null)
    {
      paramsOwner = this;
      return this.gridRowsParams;
    }
    if (this.Template is TableData template && template.gridRowsParams != null)
    {
      fromTemplate = true;
      paramsOwner = this;
      return template.gridRowsParams;
    }
    return this.ParentCell?.GetGridRowsParams(out paramsOwner, out fromTemplate);
  }

  /// <summary>Таблица владеет сеткой</summary>
  public bool IsRowGridOwner(TableData template)
  {
    if (this.gridRowsParams != null)
      return true;
    return template != null && template.IsRowGridOwner();
  }

  /// <summary>Таблица владеет сеткой</summary>
  public bool IsRowGridOwner() => this.IsRowGridOwner(this.Template as TableData);

  /// <summary>Создать и вставить новую строку грида</summary>
  /// <param name="gridIndex">Индекс строки грида</param>
  /// <param name="createTableRow">Создать соответствующую строку в таблице</param>
  /// <param name="nodeIndex">Индекс строки в Nodes, если она будет создаваться</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewGridRow(
    int gridIndex,
    bool createTableRow,
    int nodeIndex,
    bool updateUI,
    bool updateLayout)
  {
    this.InsertNewGridRow(gridIndex, new RowColParams(this, false, gridIndex, (string) null, TableData.DefaultCellSize.Height), createTableRow, nodeIndex, updateUI, updateLayout);
  }

  /// <summary>Создать ячейки для строки</summary>
  /// <param name="gridRows">Параметры строки сетки</param>
  /// <param name="gridIndex">Индекс строки в сетке</param>
  /// <param name="nodeCellIndex">Индекс ячеек строки в Nodes</param>
  /// <returns>Возвращает новые ячейки</returns>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected virtual RectangleElement[] CreateGridRowCells(
    List<RowColParams> gridRows,
    int gridIndex,
    int nodeCellIndex,
    bool updateUI,
    bool updateLayout)
  {
    Type dataShowElementType = this.GetDataShowElementType();
    if (!typeof (RectangleElement).IsAssignableFrom(dataShowElementType))
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_120"));
    RowColParams rowColParams = (RowColParams) null;
    if (gridRows != null && gridIndex < gridRows.Count)
      rowColParams = gridRows[gridIndex];
    List<RowColParams> gridColumnsParams = this.GridColumnsParams;
    RectangleElement[] gridRowCells;
    if (gridColumnsParams != null)
    {
      gridRowCells = new RectangleElement[1]
      {
        (RectangleElement) new TableData(false, (DocumentTreeNode) null, RectangleElement.EmptyRectangleF, false)
      };
      int count = gridRowCells[0].Nodes.Count;
      for (int gridIndex1 = 0; gridIndex1 < gridColumnsParams.Count; ++gridIndex1)
      {
        RectangleElement[] gridColumnCells = ((TableData) gridRowCells[0]).CreateGridColumnCells(gridColumnsParams, gridIndex1, count, updateUI, updateLayout);
        if (gridColumnCells != null)
        {
          for (int index = 0; index < gridColumnCells.Length; ++index)
            gridRowCells[0].AddChildNode((DocumentTreeNode) gridColumnCells[index], false, true, updateUI, updateLayout);
        }
      }
    }
    else
    {
      gridRowCells = new RectangleElement[1]
      {
        (RectangleElement) Activator.CreateInstance(dataShowElementType)
      };
      gridRowCells[0].TableCellType = this.TableCellType;
      string str = (string) null;
      if (rowColParams != null)
        str = rowColParams.ColRowName;
      gridRowCells[0].Name = str;
    }
    return gridRowCells;
  }

  /// <summary>Вставить ячейки новой стоки в сетке</summary>
  /// <param name="gridRows">Набор сток сетки</param>
  /// <param name="gridIndex">Индекс строки в сетке</param>
  /// <param name="nodeIndex">Индекс строки в Nodes</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewGridRowCells(
    List<RowColParams> gridRows,
    int gridIndex,
    int nodeIndex,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsColumn && this.GridRowsParams == gridRows)
    {
      if (nodeIndex == -1)
        nodeIndex = this.nodes.Count;
      RectangleElement[] gridRowCells = this.CreateGridRowCells(gridRows, gridIndex, nodeIndex, updateUI, updateLayout);
      if (gridRowCells == null)
        return;
      for (int index = 0; index < gridRowCells.Length; ++index)
        this.InsertChildNode(nodeIndex++, (DocumentTreeNode) gridRowCells[index], false, true, updateUI, updateLayout, false);
    }
    else
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.InsertNewGridRowCells(gridRows, gridIndex, nodeIndex, updateUI, updateLayout);
      }
    }
  }

  /// <summary>Создать и вставить новую строку грида</summary>
  /// <param name="gridIndex">Индекс строки грида</param>
  /// <param name="rowParams">Параметры строки</param>
  /// <param name="createTableRow">Создать соответствующую строку в таблице</param>
  /// <param name="nodeIndex">Индекс строки в Nodes, если она будет создаваться</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewGridRow(
    int gridIndex,
    RowColParams rowParams,
    bool createTableRow,
    int nodeIndex,
    bool updateUI,
    bool updateLayout)
  {
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      this.SuspendUpdateLayout();
    try
    {
      if (this.gridRowsParams == null)
      {
        if (this.Template is TableData template && template.gridRowsParams != null)
          this.SetGridRowsParams(TableData.CloneRowColParamsFromTemplate(template.gridRowsParams));
        else
          this.SetGridRowsParams(new List<RowColParams>());
      }
      rowParams.ID = TableData.GenerateGridID(this.gridRowsParams, rowParams.ID);
      rowParams.SetOwnerTable(this);
      this.gridRowsParams.Insert(gridIndex, rowParams);
      this.SetGridRowsParams(this.gridRowsParams);
      if (createTableRow)
        this.InsertNewGridRowCells(this.gridRowsParams, gridIndex, nodeIndex, updateUI, updateLayout);
      this.SetNeedUpdateLayoutFlag(true, true, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        this.ResumeUpdateLayout(updateUI, updateLayout);
    }
  }

  /// <summary>Установить высоту строки в сетке</summary>
  /// <param name="rowIndex">Индекс строки в сетке</param>
  /// <param name="height">Высота строки</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetGridRowHeight(int rowIndex, float height, bool updateUI, bool updateLayout)
  {
    TableData paramsOwner;
    bool fromTemplate;
    List<RowColParams> template = this.GetGridRowsParams(out paramsOwner, out fromTemplate);
    if (template == null || (double) template[rowIndex].Size == (double) height)
      return;
    if (fromTemplate)
      template = TableData.CloneRowColParamsFromTemplate(template);
    template[rowIndex].Size = height;
    if (paramsOwner.gridRowsParams != template)
      paramsOwner.SetGridRowsParams(template);
    paramsOwner.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Переместить строку на новую позицию</summary>
  /// <param name="gridRowIndex">Положение строки в сетке</param>
  /// <param name="newGridRowIndex">Новое положение строки</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void MoveGridRow(int gridRowIndex, int newGridRowIndex, bool updateUI, bool updateLayout)
  {
    if (this.gridRowsParams == null)
      return;
    ArrayEditHelper.MoveItem((IList) this.gridRowsParams, gridRowIndex, newGridRowIndex);
  }

  public override bool RemoveChildNodeAt(
    int index,
    bool removeByShift,
    bool updateUI,
    bool updateLayout)
  {
    return base.RemoveChildNodeAt(index, removeByShift, updateUI, updateLayout);
  }

  /// <summary>Удалить строку из принадлежащей таблице сетки</summary>
  /// <param name="gridRowIndex">Индекс строки в сетке</param>
  /// <param name="removeCells">Удалять ячейки строк</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveGridRow(int gridRowIndex, bool removeCells, bool updateUI, bool updateLayout)
  {
    if (this.gridRowsParams == null)
      return;
    this.RemoveGridRowCells(gridRowIndex, this.gridRowsParams, removeCells, updateUI, updateLayout);
    this.gridRowsParams.RemoveAt(gridRowIndex);
  }

  /// <summary>Удалить ячейки строки или отвязать их от сетки</summary>
  /// <param name="gridRowIndex">Индекс строки в сетке</param>
  /// <param name="gridRows">Сетка</param>
  /// <param name="removeCells">Если true, то удалять ячейки, если false, то разрывать связь</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveGridRowCells(
    int gridRowIndex,
    List<RowColParams> gridRows,
    bool removeCells,
    bool updateUI,
    bool updateLayout)
  {
    if (this.GridRowsParams != gridRows)
      return;
    if (this.IsRow)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is TableData node)
          node.RemoveGridRowCells(gridRowIndex, gridRows, removeCells, updateUI, updateLayout);
      }
    }
    else
    {
      List<DocumentTreeNode> rows = new List<DocumentTreeNode>();
      this.GetGridRowCells(gridRows, gridRowIndex, rows, true);
      int index = 0;
      for (int count = rows.Count; index < count; ++index)
      {
        if (removeCells)
          this.RemoveChildNodeAt(rows[index].Index, false, false);
        else
          ((RectangleElement) rows[index]).GridPos = (TableGridPosition) null;
      }
    }
  }

  /// <summary>Получить все ячейки соответствующий строке сетки</summary>
  /// <param name="gridRows">Сетка</param>
  /// <param name="gridRowIndex">Индекс строки в сетке</param>
  /// <param name="rows">Результат - коллекция ячеек строк</param>
  /// <param name="recursive">Проводить поиск во всех дочерних объектах</param>
  public void GetGridRowCells(
    List<RowColParams> gridRows,
    int gridRowIndex,
    List<DocumentTreeNode> rows,
    bool recursive)
  {
    if (this.IsColumn && this.GridRowsParams == gridRows)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node && node.GetGridRowIndex() == gridRowIndex)
          rows.Add((DocumentTreeNode) node);
        if (recursive && node is TableData tableData)
          tableData.GetGridRowCells(gridRows, gridRowIndex, rows, recursive);
      }
    }
    else
    {
      if (!recursive)
        return;
      for (int index = 0; index < this.nodes.Count; ++index)
        (this.nodes[index] as TableData).GetGridRowCells(gridRows, gridRowIndex, rows, recursive);
    }
  }

  /// <summary>Инициализировать CharFormat ячеек</summary>
  public void InitCellsCharFormat(CharFormat charFormat)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is TableData node2)
        node2.InitCellsCharFormat(charFormat);
      else if (this.nodes[index] is TextData node1)
        node1.InitCharFormat(charFormat);
    }
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => TableData.ElementTypeName;
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string name = this.GetName();
    TableData parentCell = this.ParentCell;
    if (parentCell == null || name != null && !(name == ""))
      return base.GetDefautCaption();
    if (!string.IsNullOrEmpty(this.ColumnName) && !string.IsNullOrEmpty(this.RowName))
      return $"[{this.RowName}, {this.ColumnName}]";
    if (parentCell.IsRow)
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_123"), (object) (this.Index + 1), (object) this.Id);
    return parentCell.IsColumn && this.TableCellType == CellType.Header ? string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_149"), (object) (this.Index + 1), (object) this.Id) : string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_124"), (object) (this.Index + 1), (object) this.Id);
  }

  /// <summary>Вставить узел с учетом контекста</summary>
  /// <remarks>Если в столбец вставляется столбец, то она вставляется в родительскую таблицу.
  /// Если в строку вставляется строка, то она вставляется в родительскую таблицу.
  /// В остальных случаях элемент node вставляется в этот элемент</remarks>
  /// <param name="node">Вставляемый элемент</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void ContextPaste(DocumentTreeNode node, bool updateUI, bool updateLayout)
  {
    TableData tableData = node as TableData;
    if (this.IsColumn)
    {
      if (tableData != null && tableData.IsColumn && this.ParentCell != null && this.ParentCell.IsRow)
        this.ParentCell.InsertChildNode(this.Index, node, false, true, updateUI, updateLayout, false);
      else
        this.AddChildNode(node, updateUI, updateLayout);
    }
    else if (tableData != null && tableData.IsRow && this.ParentCell != null && this.ParentCell.IsColumn)
      this.ParentCell.InsertChildNode(this.Index, node, false, true, updateUI, updateLayout, false);
    else
      this.AddChildNode(node, updateUI, updateLayout);
  }

  /// <summary>Прозрачный фон. Установка нового значения переносится на все дочерние элементы</summary>
  public override bool Transparent
  {
    set
    {
      if (this.Transparent == value)
        return;
      if (this.nodes != null)
      {
        bool suspendedRefreshUiFlag = this.SuspendedRefreshUIFlag;
        if (!suspendedRefreshUiFlag)
          this.SuspendRefreshUI();
        int index = 0;
        for (int count = this.nodes.Count; index < count; ++index)
        {
          if (this.nodes[index] is PageElementNode node)
            node.Transparent = value;
        }
        if (!suspendedRefreshUiFlag)
          this.ResumeRefreshUI(true);
      }
      base.Transparent = value;
    }
  }

  /// <summary>Рисует внутренние линии сетки</summary>
  /// <param name="context">Контекст рисования</param>
  /// <param name="gridBounds">Координаты сетки</param>
  /// <param name="rowSize">Размер строки сетки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="gridRows">Строки сетки [устарело]</param>
  protected override void DrawGrid(
    DrawContext context,
    RectangleF gridBounds,
    float rowSize,
    List<RowColParams> gridCols,
    List<RowColParams> gridRows)
  {
    TableData parentCell = this.ParentCell;
    if (gridCols == null || gridCols.Count <= 0)
      return;
    bool invisibleOnly = context.Layer == -1 && context.IsPaint;
    if (context.Borders == null)
      context.Borders = this.GetBorders(gridCols[0], true, context.ParentBorders != null ? context.ParentBorders.InnerHorizontal : (BorderLine) null);
    RectangleBorder borders = context.Borders;
    BorderLine borderLine1 = borders.InnerHorizontal ?? borders.Bottom;
    bool negative = false;
    if (context.Layer == 0)
      negative = context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value;
    PointF location = gridBounds.Location;
    if (!context.DrawGrid)
      return;
    for (int index = 0; index <= gridCols.Count; ++index)
    {
      BorderLine borderLine2;
      if (index != 0)
      {
        location.X += gridCols[index - 1].Size;
        borderLine2 = (index == gridCols.Count ? borders.Right : gridCols[index - 1].BorderLine2) ?? this.DefaultBorderLine;
      }
      else
        borderLine2 = borders.Left;
      if ((double) location.X <= (double) gridBounds.Right)
      {
        if (borderLine2 != null && borderLine2.Style == BorderStyles.Serif)
        {
          if ((double) rowSize > 0.0)
          {
            for (; (double) location.Y < (double) gridBounds.Bottom; location.Y += rowSize)
              this.DrawBorderLine(context, borderLine2, negative, true, invisibleOnly, location, rowSize);
            location.Y = gridBounds.Y;
          }
        }
        else
          this.DrawBorderLine(context, borderLine2, negative, true, invisibleOnly, location, gridBounds.Height);
      }
    }
    if ((double) rowSize <= 0.0)
      return;
    location.X = gridBounds.X;
    for (; (double) location.Y <= (double) gridBounds.Bottom; location.Y += rowSize)
      this.DrawBorderLine(context, borderLine1, negative, false, invisibleOnly, location, gridBounds.Width);
    location.Y -= rowSize;
    if ((double) gridBounds.Bottom <= (double) location.Y)
      return;
    location.Y = gridBounds.Bottom;
    this.DrawBorderLine(context, borderLine1, negative, false, invisibleOnly, location, gridBounds.Width);
  }

  /// <summary>Нарисовать пропущенное пространство (строки/столбцы)</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  protected override void DrawSkipedSpace(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsColumn != this.IsColumn)
    {
      base.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
    }
    else
    {
      float skipCellsBefore = this.SkipCellsBefore;
      float skipCellsAfter = this.SkipCellsAfter;
      if (parentCell == null || (double) skipCellsBefore <= 0.0 && (double) skipCellsAfter <= 0.0)
        return;
      RectangleF properBounds = this.ProperBounds;
      RectangleF bounds = this.Bounds;
      if (gridCols == null || gridCols.Count <= 0)
        return;
      bool invisibleOnly = context.Layer == -1 && context.IsPaint;
      RectangleBorder borders = this.Borders;
      BorderLine borderLine1 = (gridRows == null || gridRows.Count <= 0 ? borders.Bottom : gridRows[0].BorderLine2) ?? borders.Bottom;
      BorderLine borderLine2 = gridCols[0].BorderLine2 ?? borders.Right;
      bool negative = false;
      float oneSkipSize = this.OneSkipSize;
      float skipSizeBefore = this.SkipSizeBefore;
      float skipSizeAfter = this.SkipSizeAfter;
      if ((double) oneSkipSize <= 0.0)
        return;
      SolidBrush solidBrush = (SolidBrush) null;
      if (context.Layer == 0)
      {
        negative = context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value;
        if (negative)
          solidBrush = new SolidBrush(VisualNode.InvertColor(this.GetBackColor()));
        if (solidBrush == null && !this.Transparent)
          solidBrush = new SolidBrush(this.GetBackColor());
      }
      PointF location;
      if ((double) skipSizeBefore > 0.0)
      {
        if (context.Layer == 0 && solidBrush != null)
        {
          RectangleF rect = bounds;
          rect.Size = !parentCell.IsColumn ? new SizeF(skipSizeBefore, rect.Height) : new SizeF(rect.Width, skipSizeBefore);
          context.Graphics.FillRectangle((Brush) solidBrush, rect);
        }
        location = bounds.Location;
        if (parentCell.IsColumn)
        {
          for (int index = 0; index <= gridCols.Count; ++index)
          {
            BorderLine borderLine3;
            if (index != 0)
            {
              location.X += gridCols[index - 1].Size;
              borderLine3 = gridCols[index - 1].BorderLine2 ?? this.DefaultBorderLine;
            }
            else
              borderLine3 = borders.Left;
            this.DrawBorderLine(context, borderLine3, negative, true, invisibleOnly, location, skipSizeBefore);
          }
        }
        for (int index = 0; (double) index <= (double) skipCellsBefore; ++index)
        {
          if (parentCell.IsColumn)
          {
            location.X = bounds.X;
            location.Y = bounds.Y + (float) index * oneSkipSize;
            BorderLine borderLine4 = index != 0 ? ((double) index != (double) skipCellsBefore ? borderLine1 : borders.Bottom) : borders.Top;
            this.DrawBorderLine(context, borderLine4, negative, false, invisibleOnly, location, bounds.Width);
          }
          else
          {
            location.X = bounds.X + (float) index * oneSkipSize;
            location.Y = bounds.Y;
            BorderLine borderLine5 = index != 0 ? ((double) index != (double) skipCellsBefore ? borderLine2 : borders.Right) : borders.Left;
            this.DrawBorderLine(context, borderLine5, negative, true, invisibleOnly, location, bounds.Height);
          }
        }
      }
      if ((double) skipSizeAfter <= 0.0)
        return;
      RectangleF rect1 = !parentCell.IsColumn ? new RectangleF(properBounds.Right, bounds.Y, skipSizeAfter, bounds.Height) : new RectangleF(bounds.X, properBounds.Bottom, bounds.Width, skipSizeAfter);
      if (context.Layer == 0 && solidBrush != null)
        context.Graphics.FillRectangle((Brush) solidBrush, rect1);
      location = rect1.Location;
      if (parentCell.IsColumn)
      {
        for (int index = 0; index <= gridCols.Count; ++index)
        {
          BorderLine borderLine6;
          if (index != 0)
          {
            location.X += gridCols[index - 1].Size;
            borderLine6 = gridCols[index - 1].BorderLine2 ?? this.DefaultBorderLine;
          }
          else
            borderLine6 = borders.Left;
          this.DrawBorderLine(context, borderLine6, negative, true, invisibleOnly, location, skipSizeAfter);
        }
      }
      for (int index = 0; (double) index <= (double) skipCellsAfter; ++index)
      {
        if (parentCell.IsColumn)
        {
          location.X = rect1.X;
          location.Y = rect1.Y + (float) index * oneSkipSize;
          BorderLine borderLine7 = index != 0 ? ((double) index != (double) skipCellsBefore ? borderLine1 : borders.Bottom) : borders.Top;
          this.DrawBorderLine(context, borderLine7, negative, false, invisibleOnly, location, bounds.Width);
        }
        else
        {
          location.X = rect1.X + (float) index * oneSkipSize;
          location.Y = rect1.Y;
          BorderLine borderLine8 = index != 0 ? ((double) index != (double) skipCellsBefore ? borderLine2 : borders.Right) : borders.Left;
          this.DrawBorderLine(context, borderLine8, negative, true, invisibleOnly, location, bounds.Height);
        }
      }
    }
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    this.DrawCell(context, (List<RowColParams>) null, -1, (List<RowColParams>) null, -1, true);
  }

  /// <summary>Нарисовать границы элемента</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="properBounds">Границы элемента</param>
  /// <param name="gridCol">Столбец сетки</param>
  /// <param name="gridRow">Строка сетки</param>
  /// <param name="findGridParams">Искать строку и столбец сетки если null</param>
  public override void DrawFrame(
    DrawContext context,
    RectangleF properBounds,
    RowColParams gridCol,
    RowColParams gridRow,
    bool findGridParams)
  {
    float[] elements = context.Graphics.Transform.Elements;
    bool negative = false;
    if (context.Layer == 0)
      negative = context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value && !this.InPlaceEditorActive;
    bool invisibleOnly = false;
    if (context.Layer == -1)
      invisibleOnly = context.ShowInvisibleLines;
    bool pixelMode = context.PixelMode;
    PointF pointF = new PointF(context.Graphics.DpiX, context.Graphics.DpiY);
    bool flag = false;
    if (context.Layer == -1 && this.IsPageFlow)
      flag = true;
    if (flag)
    {
      BorderLine borderLine = new BorderLine(BorderStyles.None);
      this.DrawBorderLine(context, borderLine, negative, false, invisibleOnly, properBounds.Location, properBounds.Width);
      this.DrawBorderLine(context, borderLine, negative, true, invisibleOnly, new PointF(properBounds.Right, properBounds.Y), properBounds.Height);
      this.DrawBorderLine(context, borderLine, negative, false, invisibleOnly, new PointF(properBounds.X, properBounds.Bottom), properBounds.Width);
      this.DrawBorderLine(context, borderLine, negative, true, invisibleOnly, properBounds.Location, properBounds.Height);
    }
    context.PixelMode = pixelMode;
    if (this.drawEllipse)
      this.DrawEllipseBounds(context, properBounds, gridCol, gridRow, findGridParams);
    else
      base.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
  }

  public virtual float GetMinRowSize(List<RowColParams> gridRows)
  {
    float minRowSize = 0.0f;
    if (gridRows != null && gridRows.Count > 0)
      minRowSize = gridRows[0].Size;
    return minRowSize;
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  public override void DrawCell(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag)
      return;
    RectangleF rectangleF1 = this.ProperBounds;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      rectangleF1 = this.Bounds;
    bool flag1 = parentCell != null && ((double) this.SkipCellsBefore >= 1.0 || (double) this.SkipCellsAfter >= 1.0);
    RectangleF rectangleF2 = !flag1 ? rectangleF1 : this.Bounds;
    if (!rectangleF2.IntersectsWith(context.ClipRectangle))
      return;
    RectangleF properBounds = rectangleF1;
    if (context.IsSkipedSpace)
      properBounds.Height = context.SkipedSpaceSize;
    RectangleElement template = context.Template;
    float? rowSize = context.RowSize;
    bool? isFixedSizeRow = context.IsFixedSizeRow;
    bool? isSelected = context.IsSelected;
    bool? isFocused1 = context.IsFocused;
    bool firstChildLevel = context.FirstChildLevel;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    RectangleF clipRectangle = context.ClipRectangle;
    RectangleBorder parentBorders = context.ParentBorders;
    RectangleBorder borders = context.Borders;
    GraphicsState gstate1 = (GraphicsState) null;
    GraphicsState gstate2 = context.Graphics.Save();
    try
    {
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      if (context.IsPaint && (!context.IsSelected.HasValue || !context.IsSelected.Value))
        context.IsSelected = new bool?(this.ShowSelected);
      if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
        context.IsFocused = parentCell == null || !parentCell.isColumn ? new bool?(this.ShowFocused) : new bool?(false);
      context.Template = this.Template as RectangleElement;
      context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
      context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
      context.DrawGrid = this.DrawGridToBottom;
      if (this.IsTopLevelTable)
        context.Margins = this.Margins;
      TableData tableData = (TableData) null;
      bool flag2 = false;
      RowColParams gridRow = (RowColParams) null;
      if (gridRows != null && rowIndex >= 0 && rowIndex < gridRows.Count)
        gridRow = gridRows[rowIndex];
      RowColParams gridCol = (RowColParams) null;
      if (gridCols != null && colIndex >= 0 && colIndex < gridCols.Count)
        gridCol = gridCols[colIndex];
      context.Borders = this.GetBorders(gridCol, true, context.ParentBorders != null ? context.ParentBorders.InnerHorizontal : (BorderLine) null);
      if (this.IsTopLevelTable || this.nodes.Count == 0 || !firstChildLevel && context.IsSkipedSpace)
      {
        if (context.Layer == 0)
          this.DrawBackground(context, properBounds);
      }
      else if (!context.WithoutData & flag1)
        this.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
      if (this.nodes != null)
      {
        List<RowColParams> gridRows1 = gridRows;
        if (this.gridRowsParams != null)
        {
          gridRows1 = this.gridRowsParams;
        }
        else
        {
          tableData = context.Template as TableData;
          flag2 = true;
          if (tableData != null && tableData.gridRowsParams != null)
            gridRows1 = tableData.gridRowsParams;
        }
        List<RowColParams> gridCols1 = gridCols;
        if (this.GridColumnsParams != null)
        {
          gridCols1 = this.gridColumnsParams;
        }
        else
        {
          if (!flag2)
            tableData = this.Template as TableData;
          if (tableData != null && tableData.gridColumnsParams != null)
            gridCols1 = tableData.gridColumnsParams;
        }
        int rowIndex1 = -1;
        int colIndex1 = 0;
        if (this.IsColumn)
          colIndex1 = colIndex;
        else
          rowIndex1 = rowIndex;
        if (this.drawEllipse)
          this.DrawEllipseBounds(context, properBounds, gridCol, gridRow, findGridParams);
        bool? isFocused2 = context.IsFocused;
        if (this.nodes.Count > 0 && (firstChildLevel || !context.IsSkipedSpace))
        {
          if ((this.isFixedStructureArea || this.DrawParentFrames) && !this.drawEllipse)
            base.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
          int visibleCellIndex = this.FindLastVisibleCellIndex();
          if (visibleCellIndex != -1 && (double) (this.nodes[visibleCellIndex] as RectangleElement).Bounds.Bottom > (double) rectangleF2.Bottom)
          {
            if ((double) context.ClipRectangle.Bottom > (double) rectangleF2.Bottom)
              context.ClipRectangle.Height = rectangleF2.Bottom - context.ClipRectangle.Y + UnitsConverter.InchToMm(2f / context.Graphics.DpiY);
            gstate1 = context.Graphics.Save();
            context.Graphics.SetClip(context.ClipRectangle);
          }
          bool isTopRow = context.IsTopRow;
          bool isBottomRow = context.IsBottomRow;
          try
          {
            context.FirstChildLevel = false;
            borders = context.Borders;
            for (int index = 0; index < this.nodes.Count; ++index)
            {
              if (this.nodes[index] is RectangleElement node1)
              {
                if (this.IsColumn)
                {
                  context.IsTopRow = index == 0;
                  context.IsBottomRow = index == this.nodes.Count - 1;
                }
                context.ParentBorders = borders;
                context.Borders = (RectangleBorder) null;
                node1.DrawCell(context, gridCols1, colIndex1, gridRows1, rowIndex1, false);
                context.IsFocused = isFocused2;
                if (index < this.nodes.Count - 1)
                {
                  TableGridPosition gridPos = node1.GridPos;
                  if (this.IsColumn)
                  {
                    if (gridPos == null)
                      ++colIndex1;
                    else
                      colIndex1 += gridPos.SpanCount;
                  }
                  else
                    rowIndex1 = gridPos != null ? gridPos.GetGridRowIndex(node1) : -1;
                }
              }
              else
              {
                if (this.nodes[index] is VisualNode node)
                  node.Draw(context);
                context.IsFocused = isFocused2;
              }
            }
          }
          finally
          {
            context.ParentBorders = parentBorders;
            context.Borders = borders;
            context.IsTopRow = isTopRow;
            context.IsBottomRow = isBottomRow;
          }
        }
        else if (!this.drawEllipse)
          base.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
      }
      if (!this.drawEllipse && (this.IsTopLevelTable && (double) this.maxHeight > 0.0 || this.nodes.Count == 0 || !firstChildLevel && context.IsSkipedSpace))
      {
        float num = rectangleF2.Y;
        if (this.nodes.Count > 0 && (firstChildLevel || !context.IsSkipedSpace))
        {
          for (int index = this.nodes.Count - 1; index >= 0; --index)
          {
            if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
            {
              num = node.Bounds.Bottom;
              break;
            }
          }
        }
        RectangleF gridBounds = rectangleF2 with
        {
          Y = num,
          Height = rectangleF2.Bottom - num
        };
        if (context.IsSkipedSpace)
          gridBounds.Height = properBounds.Height;
        if (gridRows == null & findGridParams)
          gridRows = this.GridRowsParams;
        if (gridCols == null & findGridParams)
          gridCols = this.GridColumnsParams;
        this.DrawGrid(context, gridBounds, context.RowSize_NN, this.GridColumnsParams, this.GridRowsParams);
      }
      if (!this.IsTopLevelTable || (context.Layer != -1 || !context.IsPaint) && (context.Layer != 0 || this.nodes.Count != 0 && (firstChildLevel || !context.IsSkipedSpace)) || this.drawEllipse)
        return;
      this.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
    }
    finally
    {
      if (gstate1 != null)
        context.Graphics.Restore(gstate1);
      context.Graphics.PageUnit = pageUnit;
      context.ClipRectangle = clipRectangle;
      context.Template = template;
      context.RowSize = rowSize;
      context.IsFixedSizeRow = isFixedSizeRow;
      context.IsSelected = isSelected;
      context.IsFocused = isFocused1;
      context.FirstChildLevel = firstChildLevel;
      context.Graphics.Restore(gstate2);
      context.Borders = borders;
      if (this.IsTopLevelTable)
        context.Margins = (MarginsF) null;
    }
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (!ImDocumentData.ShowDebugInfo)
    {
      this.RemoveProperty(properties, "FlowID");
      this.RemoveProperty(properties, "GridRowsParams");
      this.RemoveProperty(properties, "GridColumnsParams");
      this.RemoveProperty(properties, "Tag");
      this.RemoveProperty(properties, "MinRowsForDynamicHeaderGroup");
      this.RemoveProperty(properties, "AutoSizeHeight");
    }
    this.RemoveProperty(properties, "ReadOnly");
    if (this.IsTopLevelTable)
    {
      this.RemoveProperty(properties, "TableCellType");
      if (this.TemplateId != null)
      {
        properties.SetReadOnlyProperty("IsPageFlow", true);
        properties.SetReadOnlyProperty("DrawGridToBottom", true);
      }
    }
    else
    {
      this.RemoveProperty(properties, "DrawGridToBottom");
      this.RemoveProperty(properties, "IsPageFlow");
      this.RemoveProperty(properties, "UsePreviousTableTemplates");
    }
    if (this.HasTemplate())
    {
      properties.SetReadOnlyProperty("IsFixedStructureArea", true);
      if (this.IsTopLevelTable && !this.IsTemplate)
        properties.SetReadOnlyProperty("UsePreviousTableTemplates", true);
    }
    if (!this.CanSwitchInternalCellsVisibity)
      this.RemoveProperty(properties, "ShowSingleCellInTemplate");
    if (!this.IsPageFlow)
      return;
    this.RemoveProperty(properties, "ShowOnPageOnlyVisual");
  }

  /// <summary>Получить список узлов привязки</summary>
  /// <param name="originalPoint">Оригинальная точка</param>
  /// <param name="snapSize">Размер области привязки</param>
  /// <param name="snapPointList">Список полученных точек</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  public override void GetSnapPoints(
    PointF originalPoint,
    float snapSize,
    List<SnapPoint> snapPointList,
    VisualNode excludeNode)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.GetSnapPoints(originalPoint, snapSize, snapPointList, excludeNode);
    }
  }

  /// <summary>Заглушка! Не используется. Подгонять размер последней строки под размер таблицы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_449")]
  [CustomDescription("Attribute.Interfaces.Document_450")]
  [CustomCategory("Attribute.Interfaces.Document_451")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public virtual bool AlignLastRows
  {
    [DebuggerStepThrough] get => this.alignLastRows;
    set
    {
      if (this.alignLastRows == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (AlignLastRows), (object) this.AlignLastRows, (object) value);
      this.alignLastRows = value;
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Вычислить положение ячейки в таблице</summary>
  /// <param name="prevBounds">Границы предыдущей ячейки</param>
  /// <returns>Положение ячейки</returns>
  protected virtual PointF CalcCellLocation(RectangleF prevBounds, RectangleElement cell)
  {
    PointF point = new PointF();
    if (cell != null && this.IsFixedStructureArea)
    {
      TableData parentCell = this.ParentCell;
      point = parentCell == null || !parentCell.IsFixedStructureArea ? (!this.IsColumn || cell.CloneByTemplateWithParent ? new PointF(this.properBounds.X + cell.properBounds.X, this.properBounds.Y + cell.properBounds.Y) : new PointF(this.properBounds.X + cell.properBounds.X, prevBounds.Bottom)) : (!this.IsColumn || cell.CloneByTemplateWithParent ? new PointF(this.bounds.X + cell.properBounds.X, this.bounds.Y + cell.properBounds.Y) : new PointF(this.bounds.X + cell.properBounds.X, prevBounds.Bottom));
    }
    else
      point = !this.IsColumn ? new PointF(prevBounds.Right, prevBounds.Top) : new PointF(prevBounds.Left, prevBounds.Bottom);
    return UnitsConverter.RoundPoint(point, 5);
  }

  protected virtual PointF CalcRealCellLocation(RectangleF prevBounds, RectangleElement cell)
  {
    PointF point = new PointF();
    if (cell != null && this.IsFixedStructureArea)
    {
      TableData parentCell = this.ParentCell;
      point = parentCell == null || !parentCell.IsFixedStructureArea ? (!this.IsColumn || cell.CloneByTemplateWithParent ? new PointF(this.properBounds.X + cell.properBounds.X, this.properBounds.Y + cell.properBounds.Y) : new PointF(this.properBounds.X + cell.properBounds.X, prevBounds.Bottom)) : (!this.IsColumn || cell.CloneByTemplateWithParent ? new PointF(this.bounds.X + cell.properBounds.X, this.bounds.Y + cell.properBounds.Y) : new PointF(this.bounds.X + cell.properBounds.X, prevBounds.Bottom));
    }
    else
      point = !this.IsColumn ? new PointF(prevBounds.Right, prevBounds.Top) : new PointF(prevBounds.Left, prevBounds.Bottom);
    return UnitsConverter.RoundPoint(point, 5);
  }

  /// <summary>Вычислить размер заданной ячейки</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="parentSize">Размер родительской ячейки</param>
  /// <param name="rowsParams">Список параметров строк</param>
  /// <param name="thisRowParams">Параметры строки</param>
  /// <param name="colsParams">Список параметров столбцов</param>
  /// <param name="thisColParams">Параметр столбца</param>
  /// <param name="ignoreAfterSkipSize">Не учитывать пропущенные строки после записи</param>
  /// <returns>Вычисленный размер ячейки</returns>
  protected virtual SizeF CalcCellSize(
    RectangleElement cell,
    SizeF parentSize,
    List<RowColParams> rowsParams,
    out RowColParams thisRowParams,
    List<RowColParams> colsParams,
    out RowColParams thisColParams,
    bool ignoreSkipSize)
  {
    SizeF cellSize = SizeF.Empty;
    thisRowParams = (RowColParams) null;
    thisColParams = (RowColParams) null;
    SizeF point;
    if (this.isFixedStructureArea)
    {
      point = cell.Bounds.Size;
    }
    else
    {
      if (this.IsColumn && !cell.HeightOverrided && rowsParams != null)
      {
        SizeF size = cell.ProperBounds.Size;
        int gridRowIndex = cell.GetGridRowIndex();
        if (gridRowIndex > -1 && gridRowIndex < rowsParams.Count)
          size.Height = (thisRowParams = rowsParams[gridRowIndex]).Size;
        cellSize = cell.CalcSizeFromProper(size, ignoreSkipSize);
      }
      else if (!this.IsColumn && cell.Index == this.nodes.Count - 1)
        cellSize.Width = this.bounds.Right - cell.bounds.X;
      else if (!this.IsColumn && colsParams != null && (!cell.WidthOverrided || this.nodes.Count == 1))
      {
        SizeF size1 = cell.ProperBounds.Size;
        int gridColumnIndex = cell.GetGridColumnIndex();
        if (gridColumnIndex > -1 && gridColumnIndex < colsParams.Count)
        {
          if (cell.IsDefaultGridPos || cell.GridPos.SpanCount > 0)
          {
            int index1 = cell.Index;
            float num = 0.0f;
            for (int index2 = index1; index2 >= 0; --index2)
            {
              if (this.nodes[index2] is RectangleElement node)
              {
                if (!node.IsDefaultGridPos && node.GridPos.SpanCount <= 0)
                  num += node.bounds.Width;
                else
                  break;
              }
            }
            float size2 = (thisColParams = colsParams[gridColumnIndex]).Size;
            if (!cell.IsDefaultGridPos)
            {
              for (int index3 = 1; index3 < cell.GridPos.SpanCount && gridColumnIndex + index3 < colsParams.Count; ++index3)
                size2 += colsParams[gridColumnIndex + index3].Size;
            }
            size1.Width = size2 - num;
            if ((double) size1.Width < 0.0)
              size1.Width = 0.0f;
            if (cell is TableData tableData && tableData.IsSingleCell && tableData.Template is RectangleElement template && (double) size1.Width != (double) template.properBounds.Width)
              size1.Width = template.properBounds.Width;
          }
          else
            size1.Width = cell.bounds.Width;
        }
        cellSize = cell.CalcSizeFromProper(size1, ignoreSkipSize);
      }
      else
        cellSize = cell.CalcSizeFromProper(cell.ProperBounds.Size, ignoreSkipSize);
      point = this.CalcAdjustedCellSize(cellSize, parentSize);
    }
    return UnitsConverter.RoundSize(point, 5);
  }

  /// <summary>Пересчитать расположение дочерних ячеек</summary>
  /// <param name="newLocation">Новое положение таблицы</param>
  /// <param name="index">Индекс элемента с которого нужно начать пересчет</param>
  /// <param name="count">Количество элементов которые нужно пересчитать.
  /// Если count = 0, то до конца</param>
  /// <returns>Возвращает размер таблицы, с учетом размеров ячеек</returns>
  public virtual SizeF RecalcCellLocations(
    PointF newLocation,
    int index,
    int count,
    bool updateUI,
    bool updateLayout,
    bool lockNeedUpdateLayoutFlag)
  {
    bool flag1 = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag1)
      this.SuspendUpdateGeometryRefreshUI();
    bool flag2 = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag2)
      this.SuspendUpdateLayout();
    bool flag3 = false;
    if (lockNeedUpdateLayoutFlag)
    {
      flag3 = this.needUpdateLayoutFlag;
      this.AssignNeedUpdateLayoutFlag(true);
    }
    try
    {
      this.AssignBounds(new RectangleF(newLocation, this.Size), false, false, false);
      if (count == 0)
        count = this.nodes.Count - index;
      if (count <= 0)
        return this.Size;
      RectangleF prevBounds = new RectangleF(this.RealProperBounds.Location, new SizeF(0.0f, 0.0f));
      TableData tableData = (TableData) null;
      rectangleElement = (RectangleElement) null;
      if (index > 0)
      {
        if (!(this.nodes[index - 1] is RectangleElement rectangleElement))
          throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_125"));
        prevBounds = rectangleElement.Bounds;
      }
      int num = index + count;
      for (int index1 = index; index1 < num; ++index1)
      {
        tableData = this.nodes[index1] as TableData;
        PointF pointF = this.CalcRealCellLocation(prevBounds, this.nodes[index1] as RectangleElement);
        if (tableData != null)
        {
          tableData.RecalcCellLocations(pointF, 0, tableData.nodes.Count, false, false, lockNeedUpdateLayoutFlag);
          prevBounds = tableData.Bounds;
        }
        else if (this.nodes[index1] is RectangleElement rectangleElement)
        {
          rectangleElement.AssignBounds(pointF, rectangleElement.Size, false, false, false);
          prevBounds = rectangleElement.Bounds;
        }
      }
      if (tableData != null)
        return new SizeF(tableData.Bounds.Right, tableData.Bounds.Bottom);
      return rectangleElement != null ? new SizeF(rectangleElement.Bounds.Right, rectangleElement.Bounds.Bottom) : this.Size;
    }
    finally
    {
      if (lockNeedUpdateLayoutFlag)
        this.AssignNeedUpdateLayoutFlag(flag3);
      if (!flag2)
        this.ResumeUpdateLayout(false, true);
      if (!flag1)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Подогнать размер этой таблицы под размер заданной ячейки</summary>
  protected virtual void AdjustSizeToCell(RectangleElement cell, bool updateUI, bool updateLayout)
  {
    if (this.IsFixedStructureArea)
      return;
    SizeF properSize1 = cell.ProperSize;
    SizeF properSize2 = this.ProperSize;
    if (this.isColumn)
    {
      if ((double) properSize2.Width >= (double) properSize1.Width)
        return;
      this.AssignProperBounds(new RectangleF(this.ProperLocation, new SizeF(properSize1.Width, properSize2.Height)), false, updateUI, updateLayout);
    }
    else
    {
      if ((double) properSize2.Height >= (double) properSize1.Height)
        return;
      this.AssignProperBounds(new RectangleF(this.ProperLocation, new SizeF(properSize2.Width, properSize1.Height)), false, updateUI, updateLayout);
    }
  }

  /// <summary>Рассчитать размер ячейки подходящий под размер этой таблицы
  /// (высоту строки или ширину столбца)</summary>
  /// <param name="cellSize">Текущий размер ячейки</param>
  /// <param name="size">Предполагаемый размер этой таблицы</param>
  /// <returns>Размер ячейки подогнанных по размер таблицы</returns>
  protected SizeF CalcAdjustedCellSize(SizeF cellSize, SizeF size)
  {
    cellSize = !this.IsColumn ? new SizeF(cellSize.Width, size.Height) : new SizeF(size.Width, cellSize.Height);
    return cellSize;
  }

  /// <summary>Выравнивание элементов относительно краёв</summary>
  public static void AlignChildElements(VisualNode parent)
  {
    PageData page = parent != null ? parent as PageData : throw new ArgumentNullException(nameof (parent));
    TableData table = parent as TableData;
    if (page == null && table == null || table != null && !table.IsFixedStructureArea)
      return;
    RectangleF empty = RectangleF.Empty with
    {
      Location = TableData.GetParentLocationForAlign(page, table)
    };
    List<RectangleElement> alignLeft = (List<RectangleElement>) null;
    List<RectangleElement> alignRight = (List<RectangleElement>) null;
    List<RectangleElement> alignHorzCenter = (List<RectangleElement>) null;
    List<RectangleElement> alignTop = (List<RectangleElement>) null;
    List<RectangleElement> alignVertCenter = (List<RectangleElement>) null;
    List<RectangleElement> alignBottom = (List<RectangleElement>) null;
    parent.FindAlignElements(ref alignLeft, ref alignHorzCenter, ref alignRight, ref alignTop, ref alignVertCenter, ref alignBottom, true);
    Dictionary<string, RectangleF> dictionary = new Dictionary<string, RectangleF>();
    List<VisualNode> elements = new List<VisualNode>();
    for (int index1 = 0; alignLeft != null && index1 < alignLeft.Count; ++index1)
    {
      elements.Clear();
      RectangleF rect = alignLeft[index1].Bounds;
      if ((double) rect.X > 0.0)
        parent.FindPageElementsInRectangle(new RectangleF(0.0f, rect.Y, rect.X, rect.Height), elements, false, true);
      float num = empty.X;
      for (int index2 = 0; index2 < elements.Count; ++index2)
      {
        if (elements[index2] is RectangleElement rectangleElement && !alignLeft[index1].IsParentForNode((DocumentTreeNode) elements[index2], false))
        {
          RectangleF bounds = rectangleElement.Bounds;
          if (dictionary.ContainsKey(elements[index2].Id))
            bounds = dictionary[elements[index2].Id];
          if (rectangleElement.HorzAlign != ElementHorizontalAlign.Right && rectangleElement.HorzAlign != ElementHorizontalAlign.Center && (double) bounds.Right - (double) num > 9.9999997473787516E-06 && (double) rect.X - (double) rectangleElement.Bounds.X > 9.9999997473787516E-06)
            num = bounds.Right;
        }
      }
      rect.X = (float) Math.Round((double) num, 5);
      rect = UnitsConverter.RoundPectangle(rect, 5);
      dictionary[alignLeft[index1].Id] = rect;
    }
    for (int index = 0; alignLeft != null && index < alignLeft.Count; ++index)
    {
      RectangleF rectangleF = dictionary[alignLeft[index].Id];
      TableData.SetPageLocation(alignLeft[index], rectangleF.Location, false);
    }
    for (int index3 = 0; alignTop != null && index3 < alignTop.Count; ++index3)
    {
      elements.Clear();
      RectangleF bounds = alignTop[index3].Bounds;
      if ((double) bounds.Y > 0.0)
        parent.FindPageElementsInRectangle(new RectangleF(bounds.X, 0.0f, bounds.Width, bounds.Y), elements, false, true);
      float num = empty.Y;
      for (int index4 = 0; index4 < elements.Count; ++index4)
      {
        if (elements[index4] is RectangleElement rectangleElement && !alignTop[index3].IsParentForNode((DocumentTreeNode) elements[index4], false) && rectangleElement.VertAlign != ElementVerticalAlign.Bottom && rectangleElement.VertAlign != ElementVerticalAlign.Center && (double) rectangleElement.Bounds.Bottom - (double) num > 9.9999997473787516E-06 && (double) bounds.Y - (double) rectangleElement.Bounds.Y > 9.9999997473787516E-06)
          num = rectangleElement.Bounds.Bottom;
      }
      bounds.Y = num;
      TableData.SetPageLocation(alignTop[index3], bounds.Location, false);
    }
    empty.Size = TableData.GetParentSizeForAlign(page, table);
    page?.SetSize(empty.Size, false, false);
    table?.AssignBounds(empty, false, false, false);
    TableData.AlignHorzCenter(empty.X, empty.Width, (IEnumerable<RectangleElement>) alignHorzCenter, false);
    TableData.AlignVertCenter(empty.Y, empty.Height, (IEnumerable<RectangleElement>) alignVertCenter, false);
    TableData.AlignRightElements(parent, empty, alignRight, false);
    TableData.AlignBottomElements(parent, empty, alignBottom, false);
  }

  private static void AlignBottomElements(
    VisualNode parent,
    RectangleF parentBounds,
    List<RectangleElement> alignBottom,
    bool lockNeedUpdateLayoutFlag)
  {
    if (alignBottom == null)
      return;
    float[] numArray = new float[alignBottom.Count];
    List<VisualNode> elements = new List<VisualNode>();
    for (int index1 = 0; index1 < alignBottom.Count; ++index1)
    {
      elements.Clear();
      RectangleF bounds1 = alignBottom[index1].Bounds;
      RectangleF rect = new RectangleF(bounds1.X, bounds1.Bottom, bounds1.Width, parentBounds.Height - bounds1.Bottom);
      parent.FindPageElementsInRectangle(rect, elements, false, true);
      float num1 = parentBounds.Y + parentBounds.Height;
      RectangleF bounds2;
      for (int index2 = 0; index2 < elements.Count; ++index2)
      {
        if (elements[index2] is RectangleElement rectangleElement && !alignBottom[index1].IsParentForNode((DocumentTreeNode) elements[index2], false) && rectangleElement.VertAlign != ElementVerticalAlign.Top && rectangleElement.VertAlign != ElementVerticalAlign.Center)
        {
          double num2 = (double) num1;
          bounds2 = rectangleElement.Bounds;
          double y = (double) bounds2.Y;
          if (num2 - y > 9.9999997473787516E-06)
          {
            bounds2 = rectangleElement.Bounds;
            if ((double) bounds2.Bottom - (double) bounds1.Bottom > 9.9999997473787516E-06)
            {
              bounds2 = rectangleElement.Bounds;
              num1 = bounds2.Y;
            }
          }
        }
      }
      for (int index3 = 0; index3 < index1; ++index3)
      {
        double num3 = (double) numArray[index3];
        bounds2 = alignBottom[index3].Bounds;
        double y = (double) bounds2.Y;
        if (num3 > y && (double) numArray[index3] < (double) num1 && (double) numArray[index3] >= (double) bounds1.Bottom && rect.IntersectsWith(new RectangleF(bounds1.X, numArray[index3], bounds1.Width, bounds1.Height)))
        {
          bounds2 = alignBottom[index3].Bounds;
          num1 = bounds2.Y;
        }
      }
      numArray[index1] = bounds1.Y;
      bounds1.Y = num1 - bounds1.Height;
      TableData.SetPageLocation(alignBottom[index1], bounds1.Location, lockNeedUpdateLayoutFlag);
    }
  }

  private static void AlignRightElements(
    VisualNode parent,
    RectangleF parentBounds,
    List<RectangleElement> alignRight,
    bool lockNeedUpdateLayoutFlag)
  {
    if (alignRight == null)
      return;
    float[] numArray = new float[alignRight.Count];
    List<VisualNode> elements = new List<VisualNode>();
    for (int index1 = 0; index1 < alignRight.Count; ++index1)
    {
      elements.Clear();
      RectangleF bounds1 = alignRight[index1].Bounds;
      RectangleF rect = new RectangleF(bounds1.Right, bounds1.Y, parentBounds.Width - bounds1.Right, bounds1.Height);
      parent.FindPageElementsInRectangle(rect, elements, false, true);
      float num1 = parentBounds.X + parentBounds.Width;
      RectangleF bounds2;
      for (int index2 = 0; index2 < elements.Count; ++index2)
      {
        if (elements[index2] is RectangleElement rectangleElement && !alignRight[index1].IsParentForNode((DocumentTreeNode) elements[index2], false) && rectangleElement.HorzAlign != ElementHorizontalAlign.Left && rectangleElement.HorzAlign != ElementHorizontalAlign.Center)
        {
          double num2 = (double) num1;
          bounds2 = rectangleElement.Bounds;
          double x = (double) bounds2.X;
          if (num2 - x > 9.9999997473787516E-06)
          {
            bounds2 = rectangleElement.Bounds;
            if ((double) bounds2.Right - (double) bounds1.Right > 9.9999997473787516E-06)
            {
              bounds2 = rectangleElement.Bounds;
              num1 = bounds2.X;
            }
          }
        }
      }
      for (int index3 = 0; index3 < index1; ++index3)
      {
        double num3 = (double) numArray[index3];
        bounds2 = alignRight[index3].Bounds;
        double x = (double) bounds2.X;
        if (num3 > x && (double) numArray[index3] < (double) num1 && (double) numArray[index3] >= (double) bounds1.Right && rect.IntersectsWith(new RectangleF(numArray[index3], bounds1.Y, bounds1.Width, bounds1.Height)))
        {
          bounds2 = alignRight[index3].Bounds;
          num1 = bounds2.X;
        }
      }
      numArray[index1] = bounds1.X;
      bounds1.X = num1 - bounds1.Width;
      TableData.SetPageLocation(alignRight[index1], bounds1.Location, lockNeedUpdateLayoutFlag);
    }
  }

  /// <summary>Выравнивание элементов зависящих от размера - прижатые вправо, вниз и по центру</summary>
  public static void AlignFloatChildElements(VisualNode parent)
  {
    PageData page = parent != null ? parent as PageData : throw new ArgumentNullException(nameof (parent));
    TableData table = parent as TableData;
    if (page == null && table == null || table != null && !table.IsFixedStructureArea)
      return;
    List<RectangleElement> alignLeft = (List<RectangleElement>) null;
    List<RectangleElement> alignRight = (List<RectangleElement>) null;
    List<RectangleElement> alignHorzCenter = (List<RectangleElement>) null;
    List<RectangleElement> alignTop = (List<RectangleElement>) null;
    List<RectangleElement> alignVertCenter = (List<RectangleElement>) null;
    List<RectangleElement> alignBottom = (List<RectangleElement>) null;
    parent.FindAlignElements(ref alignLeft, ref alignHorzCenter, ref alignRight, ref alignTop, ref alignVertCenter, ref alignBottom, true);
    RectangleF empty = RectangleF.Empty with
    {
      Location = TableData.GetParentLocationForAlign(page, table),
      Size = TableData.GetParentSize(page, table)
    };
    TableData.AlignHorzCenter(empty.X, empty.Width, (IEnumerable<RectangleElement>) alignHorzCenter, true);
    TableData.AlignVertCenter(empty.Y, empty.Height, (IEnumerable<RectangleElement>) alignVertCenter, true);
    TableData.AlignRightElements(parent, empty, alignRight, true);
    TableData.AlignBottomElements(parent, empty, alignBottom, true);
  }

  /// <summary>Рассчитать размер таблицы для выравнивания (без элементов прижатых вправо!)</summary>
  /// <returns></returns>
  private static SizeF GetParentSizeForAlign(PageData page, TableData table)
  {
    SizeF parentSizeForAlign = SizeF.Empty;
    if (page != null)
      parentSizeForAlign = !page.AutoSize ? page.Size : page.FindMinSize(SizeF.Empty, true);
    else if (table != null)
    {
      RectangleF bounds = table.bounds;
      SizeF minSize = table.FindMinSize(SizeF.Empty, true);
      if (table.AutoSizeWidth)
        bounds.Width = minSize.Width;
      if (table.AutoSizeHeight)
        bounds.Height = minSize.Height;
      parentSizeForAlign = bounds.Size;
    }
    return parentSizeForAlign;
  }

  /// <summary>Рассчитать размер таблицы для выравнивания (без элементов прижатых вправо!)</summary>
  /// <returns></returns>
  private static SizeF GetParentSize(PageData page, TableData table)
  {
    SizeF parentSize = SizeF.Empty;
    if (page != null)
      parentSize = page.Size;
    else if (table != null)
      parentSize = table.bounds.Size;
    return parentSize;
  }

  /// <summary>Рассчитать положение таблицы для выравнивания</summary>
  /// <returns></returns>
  private static PointF GetParentLocationForAlign(PageData page, TableData table)
  {
    PointF locationForAlign = PointF.Empty;
    if (table != null)
      locationForAlign = table.Location;
    return locationForAlign;
  }

  private static void AlignHorzCenter(
    float parentLocationX,
    float parentWidth,
    IEnumerable<RectangleElement> nodes,
    bool lockNeedUpdateLayoutFlag)
  {
    if (nodes == null)
      return;
    float num = parentLocationX + parentWidth / 2f;
    foreach (RectangleElement node in nodes)
    {
      RectangleF bounds = node.Bounds;
      bounds.X = num - bounds.Width / 2f;
      TableData.SetPageLocation(node, bounds.Location, lockNeedUpdateLayoutFlag);
    }
  }

  private static void AlignVertCenter(
    float parentLocationY,
    float parentHeight,
    IEnumerable<RectangleElement> nodes,
    bool lockNeedUpdateLayoutFlag)
  {
    if (nodes == null)
      return;
    float num = parentLocationY + parentHeight / 2f;
    foreach (RectangleElement node in nodes)
    {
      RectangleF bounds = node.Bounds;
      bounds.Y = num - bounds.Height / 2f;
      TableData.SetPageLocation(node, bounds.Location, lockNeedUpdateLayoutFlag);
    }
  }

  private static void SetPageLocation(
    RectangleElement cell,
    PointF locationInPageCoords,
    bool lockNeedUpdateLayoutFlag)
  {
    if (cell is TableData table)
      TableData.SetPageTableLocation(table, locationInPageCoords, lockNeedUpdateLayoutFlag);
    else
      TableData.SetPageSingleRectangleLocation(cell, locationInPageCoords, lockNeedUpdateLayoutFlag);
  }

  private static void SetPageTableLocation(
    TableData table,
    PointF locationInPageCoords,
    bool lockNeedUpdateLayoutFlag)
  {
    bool flag = false;
    if (lockNeedUpdateLayoutFlag)
    {
      flag = table.needUpdateLayoutFlag;
      table.AssignNeedUpdateLayoutFlag(true);
    }
    PointF pointF = TableData.CalcInternalProperLocation((RectangleElement) table, locationInPageCoords);
    RectangleF rectangleF1;
    ref RectangleF local1 = ref rectangleF1;
    PointF location1 = pointF;
    RectangleF bounds = table.Bounds;
    SizeF size1 = bounds.Size;
    local1 = new RectangleF(location1, size1);
    table.AssignProperBounds(rectangleF1, false, false, false);
    RectangleF rectangleF2;
    ref RectangleF local2 = ref rectangleF2;
    PointF location2 = locationInPageCoords;
    bounds = table.Bounds;
    SizeF size2 = bounds.Size;
    local2 = new RectangleF(location2, size2);
    table.AssignBounds(rectangleF2, false, false, false);
    table.RecalcCellLocations(locationInPageCoords, 0, 0, false, false, lockNeedUpdateLayoutFlag);
    if (!lockNeedUpdateLayoutFlag)
      return;
    table.AssignNeedUpdateLayoutFlag(flag);
  }

  private static void SetPageSingleRectangleLocation(
    RectangleElement cell,
    PointF locationInPageCoords,
    bool lockNeedUpdateLayoutFlag)
  {
    bool flag = false;
    if (lockNeedUpdateLayoutFlag)
    {
      flag = cell.NeedUpdateLayoutFlag;
      cell.AssignNeedUpdateLayoutFlag(true);
    }
    RectangleF rectangleF = new RectangleF(TableData.CalcInternalProperLocation(cell, locationInPageCoords), cell.Bounds.Size);
    cell.AssignProperBounds(rectangleF, false, false, false);
    if (!lockNeedUpdateLayoutFlag)
      return;
    cell.AssignNeedUpdateLayoutFlag(flag);
  }

  private static PointF CalcInternalProperLocation(
    RectangleElement cell,
    PointF locationInPageCoords)
  {
    TableData parentCell = cell.ParentCell;
    PointF pointF = locationInPageCoords;
    if (parentCell != null)
    {
      if (parentCell.IsFixedStructureArea)
      {
        PointF location = parentCell.Bounds.Location;
        pointF.X = locationInPageCoords.X - location.X;
        pointF.Y = locationInPageCoords.Y - location.Y;
      }
      else
        pointF = cell.CalcProperLocation(locationInPageCoords);
    }
    return pointF;
  }

  /// <summary>Найти последнюю видимую ячейку в таблице</summary>
  /// <returns>Возвращает индекс последней видимой ячейки в таблице.
  /// Если видимых ячеек нет, то возвращает -1</returns>
  public int FindLastVisibleCellIndex()
  {
    for (int index = this.nodes.Count - 1; index >= 0; --index)
    {
      if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        return index;
    }
    return -1;
  }

  /// <summary>Установить размеры дочерних ячеек</summary>
  /// <param name="newBounds">Новые границы ячейки (внешние, с учётом пропусков)</param>
  /// <param name="lockNeedUpdateLayoutFlag">Блокировать изменение NeedUpdateLayoutFlag</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="setMinHeight">Установить значение MinHeight</param>
  /// <param name="setRelativeSize">Установить соответствующий относительный размер</param>
  /// <param name="checkLastCell">Проверять размер последней ячейки</param>
  /// <returns>Новые границы ячейки</returns>
  public override RectangleF SetCellSizes(
    RectangleF newBounds,
    bool lockNeedUpdateLayoutFlag,
    bool saveUndo,
    bool setMinHeight,
    bool setRelativeSize,
    bool checkLastCell = false)
  {
    bool flag1 = false;
    if (lockNeedUpdateLayoutFlag)
    {
      flag1 = this.needUpdateLayoutFlag;
      this.AssignNeedUpdateLayoutFlag(true);
    }
    int num1 = 0;
    float num2 = 0.0f;
    TableData parentCell = this.ParentCell;
    bool fromTemplate;
    List<RowColParams> template = this.GetGridColumnsParams(out TableData _, out fromTemplate, true, false);
    if (fromTemplate)
      template = TableData.CloneRowColParamsFromTemplate(template);
    bool flag2 = false;
    RectangleF rectangleF1;
    if (parentCell == null || !parentCell.isFixedStructureArea)
    {
      rectangleF1 = this.CalcProperBounds(newBounds);
      if ((double) rectangleF1.Height < (double) this.minHeight)
      {
        if (setMinHeight && (double) this.minHeight > (double) rectangleF1.Height)
        {
          this.minHeight = rectangleF1.Height;
        }
        else
        {
          rectangleF1.Height = this.minHeight;
          if ((double) rectangleF1.Bottom > (double) newBounds.Bottom)
            rectangleF1.Height = newBounds.Bottom - rectangleF1.Y;
        }
      }
    }
    else
      rectangleF1 = newBounds;
    if (checkLastCell && parentCell != null && !parentCell.isFixedStructureArea && this.Index == parentCell.FindLastVisibleCellIndex() && (double) parentCell.MaxHeight != 0.0)
    {
      float bottom = rectangleF1.Bottom;
      float num3 = parentCell.ProperBounds.Top + parentCell.MaxHeight;
      if ((double) num3 < (double) bottom)
        bottom = num3;
      rectangleF1 = RectangleF.FromLTRB(rectangleF1.Left, rectangleF1.Top, rectangleF1.Right, bottom);
    }
    RectangleF rectangleF2 = new RectangleF(rectangleF1.Location, SizeF.Empty);
    int visibleCellIndex = this.FindLastVisibleCellIndex();
    for (int index1 = 0; index1 < this.nodes.Count; ++index1)
    {
      if (this.nodes[index1] is RectangleElement node && node.IsVisibleNow)
      {
        RectangleF newBounds1 = node.Bounds;
        if (!this.IsFixedStructureArea)
        {
          SizeF sizeF;
          if (this.IsColumn)
          {
            newBounds1.X = rectangleF1.X;
            newBounds1.Y = rectangleF2.Y + rectangleF2.Height;
            if (index1 == visibleCellIndex && ((!this.IsTopLevelTable ? 1 : ((double) this.maxHeight == 0.0 ? 1 : 0)) | (checkLastCell ? 1 : 0)) != 0 && (!this.IsTopLevelTable || !this.IsPageFlow))
            {
              float num4 = rectangleF1.Bottom;
              if ((double) this.maxHeight != 0.0 && (double) this.maxHeight > (double) newBounds.Height)
              {
                float num5 = rectangleF1.Top + this.maxHeight;
                if ((double) num5 < (double) num4)
                  num4 = num5;
              }
              sizeF = new SizeF(rectangleF1.Width, num4 - newBounds1.Y);
              if ((double) sizeF.Height < 0.0)
                sizeF.Height = 0.0f;
            }
            else
              sizeF = new SizeF(rectangleF1.Width, newBounds1.Height);
          }
          else
          {
            newBounds1.X = rectangleF2.X + rectangleF2.Width;
            newBounds1.Y = rectangleF1.Y;
            if (index1 == visibleCellIndex)
            {
              sizeF = new SizeF(rectangleF1.Right - newBounds1.X, rectangleF1.Height);
              if ((double) sizeF.Height < 0.0)
                sizeF.Height = 0.0f;
            }
            else
              sizeF = new SizeF(newBounds1.Width, rectangleF1.Height);
            num2 = 0.0f;
            if (template != null)
            {
              int num6 = node.IsDefaultGridPos ? 1 : node.GridPos.SpanCount;
              for (int index2 = num1; index2 < template.Count && index2 < num1 + num6; ++index2)
                num2 += template[index2].Size;
              num1 += num6;
            }
          }
          if (newBounds1.Size != sizeF || newBounds1.Location != node.bounds.Location)
          {
            newBounds1.Size = sizeF;
            newBounds1 = node.SetCellSizes(newBounds1, lockNeedUpdateLayoutFlag, saveUndo, setMinHeight, setRelativeSize);
            if (node.WidthOverrided && (double) newBounds1.Width == (double) num2)
              node.WidthOverrided = false;
            if (index1 == visibleCellIndex && this.IsRow && !node.WidthOverrided && template != null && (double) newBounds1.Width != (double) num2)
            {
              if ((double) num2 > 0.0)
                num2 = template[template.Count - 1].Size + (newBounds1.Width - num2);
              if ((double) num2 > 0.0)
              {
                template[template.Count - 1].AssignSize(num2, false, false);
                flag2 = true;
              }
              else
                node.WidthOverrided = true;
            }
            if ((double) newBounds1.Right - (double) rectangleF1.Right > 9.9999997473787516E-06)
              rectangleF1.Width = (float) Math.Round((double) newBounds1.Right - (double) rectangleF1.X, 5);
            if ((double) newBounds1.Bottom - (double) rectangleF1.Bottom > 9.9999997473787516E-06)
              rectangleF1.Height = (float) Math.Round((double) newBounds1.Bottom - (double) rectangleF1.Y, 5);
          }
        }
        rectangleF2 = newBounds1;
      }
    }
    if (setMinHeight && (double) this.properBounds.Height != (double) rectangleF1.Height && (double) rectangleF1.Height != (double) RectangleElement.EmptyFloatValue && (double) rectangleF1.Height < (double) this.MinHeight)
      this.AssignMinHeight(rectangleF1.Height, false, false, true);
    this.AssignBounds(newBounds, saveUndo, false, false);
    if (this.IsFixedStructureArea)
      TableData.AlignFloatChildElements((VisualNode) this);
    if (setRelativeSize && parentCell != null)
      this.RecalcRelativeSize();
    if (this.IsRow && this.nodes.Count == 0 && template != null && template.Count > 0)
    {
      float num7 = 0.0f;
      for (int index = 0; index < template.Count; ++index)
        num7 += template[index].Size;
      float num8 = this.properBounds.Width - num7;
      if ((double) num8 > 0.0)
      {
        template[template.Count - 1].AssignSize(template[template.Count - 1].Size + num8, false, false);
        flag2 = true;
      }
      else if ((double) num8 < 0.0)
      {
        float num9 = -num8;
        for (int index = template.Count - 1; index >= 0; --index)
        {
          if ((double) num9 + 3.0 > (double) template[index].Size)
          {
            num9 -= template[index].Size - 3f;
            template[index].AssignSize(3f, false, false);
            flag2 = true;
          }
          else
          {
            template[index].AssignSize(template[index].Size - num9, false, false);
            flag2 = true;
            break;
          }
        }
      }
    }
    if (flag2 & fromTemplate)
      this.SetGridColumnsParams(template, true, saveUndo);
    if (lockNeedUpdateLayoutFlag)
      this.AssignNeedUpdateLayoutFlag(flag1);
    return this.bounds;
  }

  /// <summary>Найти ячейки правая сторона которых имеет заданную координату X</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="x">Координата X</param>
  public override void FindResizableLeftSide(List<RectangleElement> cells, float x)
  {
    if (!this.IsVisibleNow)
      return;
    if (this.IsResizableWidth)
    {
      if ((double) x != (double) this.Bounds.X)
        return;
      cells.Add((RectangleElement) this);
    }
    else
    {
      if ((double) this.bounds.X > (double) x || (double) this.bounds.Right < (double) x)
        return;
      int count = cells.Count;
      for (int index = 0; index < this.nodes.Count && (this.IsColumn || cells.Count == count); ++index)
      {
        if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        {
          if (!this.IsColumn && (double) node.bounds.X > (double) x)
            break;
          node.FindResizableLeftSide(cells, x);
        }
      }
    }
  }

  /// <summary>Найти ячейки правая сторона которых имеет заданную координату X</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="x">Координата X</param>
  public override void FindResizableRightSide(List<RectangleElement> cells, float x)
  {
    if (!this.IsVisibleNow)
      return;
    if ((this.IsResizableWidth || this.IsColumn) && (double) x == (double) this.Bounds.Right)
    {
      cells.Add((RectangleElement) this);
    }
    else
    {
      if ((double) this.bounds.X > (double) x || (double) this.bounds.Right < (double) x)
        return;
      int count = cells.Count;
      for (int index = this.nodes.Count - 1; index > -1 && (this.IsColumn || cells.Count == count); --index)
      {
        if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        {
          if (!this.IsColumn && (double) node.bounds.Right < (double) x)
            break;
          node.FindResizableRightSide(cells, x);
        }
      }
    }
  }

  /// <summary>Найти ячейки нижняя сторона которых имеет заданную координату Y</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="y">Координата Y</param>
  public override void FindResizableBottomSide(List<RectangleElement> cells, float y)
  {
    if (!this.IsVisibleNow)
      return;
    if (this.IsResizableHeight && (double) y == (double) this.Bounds.Bottom)
    {
      cells.Add((RectangleElement) this);
    }
    else
    {
      int count = cells.Count;
      for (int index = this.nodes.Count - 1; index > -1 && (!this.IsColumn || cells.Count == count); --index)
      {
        if (this.nodes[index] is RectangleElement node)
          node.FindResizableBottomSide(cells, y);
      }
    }
  }

  /// <summary>Необходимо ли в динамической таблице рисовать сетку вне ячеек</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_549")]
  [CustomDescription("Attribute.Interfaces.Document_550")]
  [CustomCategory("Attribute.Interfaces.Document_454")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool DrawGridToBottom
  {
    [DebuggerStepThrough] get
    {
      if (this.IsVirtualNode)
        return false;
      return this.ParentCell != null ? this.ParentCell.DrawGridToBottom : this.drawGridToBottom;
    }
    set => this.AssignDrawGridToBottom(value, true);
  }

  /// <summary>Установка свойства DrawGridToBottom</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Перерисовать</param>
  public void AssignDrawGridToBottom(bool value, bool updateUI)
  {
    this.drawGridToBottom = value;
    if (updateUI)
      this.RefreshUI();
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Рисовать границы подтаблицы поверх границ внутренних ячеек.
  /// Необходимо для возможности отключать границы части внутренних ячеек</summary>
  private bool DrawParentFrames
  {
    get
    {
      return this.OwnerDocument != null ? !this.OwnerDocument.IsFormulaLib && this.OwnerDocument.DefaultDrawParentCellFrames : this.Page == null || !this.Page.IsFormulaLib;
    }
  }

  /// <summary>Проверить статус внешних и внутренних границ таблицы</summary>
  /// <param name="bordersStatus">Статус границ</param>
  /// <param name="bordersPosition">Положение ячейки относительно границ выделения</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Получить параметры если не заданы</param>
  /// <returns>true, если все границы проверены и не требуется проверка в остальных ячейках</returns>
  public override bool CheckBordersStatus(
    BordersStatus bordersStatus,
    BordersPosition bordersPosition,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    return !bordersStatus.FirstLeft && !bordersStatus.Left.HasValue && !bordersStatus.FirstRight && !bordersStatus.Right.HasValue && !bordersStatus.FirstTop && !bordersStatus.Top.HasValue && !bordersStatus.FirstBottom && !bordersStatus.Bottom.HasValue && !bordersStatus.FirstHorizontal && !bordersStatus.InnerHorizontal.HasValue && !bordersStatus.FirstVertical && !bordersStatus.InnerVertical.HasValue;
  }

  /// <summary>Установить стиль линии левой границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public override void SetLeftBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    base.SetLeftBorderLine(borderLine, setAdjoiningLine);
    this.SetLeftBorderLineForCell(borderLine);
  }

  /// <summary>Установить левую границу в подчиненных ячейках</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  internal void SetLeftBorderLineForCell(BorderLine borderLine)
  {
    int count = this.nodes.Count;
    if (count <= 0)
      return;
    if (this.isColumn)
    {
      for (int index = 0; index < count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          BorderLine borderLine1 = borderLine?.Clone();
          node.SetLeftBorderLine(borderLine1, false);
        }
      }
    }
    else
    {
      if (!(this.nodes[0] is RectangleElement node))
        return;
      BorderLine borderLine2 = borderLine?.Clone();
      node.SetLeftBorderLine(borderLine2, false);
    }
  }

  /// <summary>Установить стиль линии правой границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public override void SetRightBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    base.SetRightBorderLine(borderLine, setAdjoiningLine);
    this.SetRightBorderLineForCell(borderLine);
  }

  /// <summary>Установить правую границу в подчиненных ячейках</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  internal void SetRightBorderLineForCell(BorderLine borderLine)
  {
    int count = this.nodes.Count;
    if (count <= 0)
      return;
    if (this.isColumn)
    {
      for (int index = 0; index < count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          BorderLine borderLine1 = borderLine?.Clone();
          node.SetRightBorderLine(borderLine1, false);
        }
      }
    }
    else
    {
      if (!(this.nodes[count - 1] is RectangleElement node))
        return;
      BorderLine borderLine2 = borderLine?.Clone();
      node.SetRightBorderLine(borderLine2, false);
    }
  }

  /// <summary>Установить стиль линии верхней границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public override void SetTopBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    base.SetTopBorderLine(borderLine, setAdjoiningLine);
    this.SetTopBorderLineForCell(borderLine);
  }

  /// <summary>Установить верхнюю границу в подчиненных ячейках</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  internal void SetTopBorderLineForCell(BorderLine borderLine)
  {
    int count = this.nodes.Count;
    if (count <= 0 || this.isFixedStructureArea)
      return;
    if (this.isColumn)
    {
      if (!(this.nodes[0] is RectangleElement node))
        return;
      BorderLine borderLine1 = borderLine?.Clone();
      node.SetTopBorderLine(borderLine1, false);
    }
    else
    {
      for (int index = 0; index < count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          BorderLine borderLine2 = borderLine?.Clone();
          node.SetTopBorderLine(borderLine2, false);
        }
      }
    }
  }

  /// <summary>Установить стиль линии нижней границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public override void SetBottomBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    base.SetBottomBorderLine(borderLine, setAdjoiningLine);
    this.SetBottomBorderLineForCell(borderLine);
  }

  /// <summary>Установить нижнюю границу в подчиненных ячейках</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  internal void SetBottomBorderLineForCell(BorderLine borderLine)
  {
    int count = this.nodes.Count;
    if (count <= 0 || this.isFixedStructureArea)
      return;
    if (this.isColumn)
    {
      if (!(this.nodes[count - 1] is RectangleElement node))
        return;
      BorderLine borderLine1 = borderLine?.Clone();
      node.SetBottomBorderLine(borderLine1, false);
    }
    else
    {
      for (int index = 0; index < count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          BorderLine borderLine2 = borderLine?.Clone();
          node.SetBottomBorderLine(borderLine2, false);
        }
      }
    }
  }

  /// <summary>Можно ли переключать текущую видимость внутренних ячеек,
  /// чтобы не отображать одновременно все варианты шаблонов строк</summary>
  protected bool CanSwitchInternalCellsVisibity
  {
    get => this.IsTemplate && this.IsColumn && !this.IsVirtualNode;
  }

  /// <summary>Обновить текущую видимость дочерних ячеек</summary>
  /// <param name="selectedNodes">Выделенные ячейки</param>
  public void UpdateCells_IsVisibleNow(List<RectangleElement> selectedNodes)
  {
    if (!this.Visible || !this.CanSwitchInternalCellsVisibity)
      return;
    RectangleElement rectangleElement1 = (RectangleElement) null;
    bool flag1 = false;
    foreach (RectangleElement rectangleElement2 in this.CellsEnumerator)
    {
      if (rectangleElement2.SwitchVisibleThisDataCellInTemplateIsEnabled)
      {
        if (rectangleElement1 == null)
          rectangleElement1 = rectangleElement2;
        bool flag2 = selectedNodes.Contains(rectangleElement2);
        if (flag2)
          flag1 = true;
        rectangleElement2.SetIsSelectedDataCellTemplate(flag2, false);
        rectangleElement2.SetNeedUpdateLayoutFlag(true, false, false, false);
      }
    }
    if (!flag1 && rectangleElement1 != null)
      rectangleElement1.SetIsSelectedDataCellTemplate(true, false);
    this.UpdateLayout(true);
  }

  public override ShowOnPageOnly ShowOnPageOnly
  {
    get => this.IsPageFlow ? ShowOnPageOnly.All : base.ShowOnPageOnly;
  }

  /// <summary>Использовать шаблоны подэлементов предыдущей таблицы.
  /// Работает только для представления данных.</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_455")]
  [CustomDescription("Attribute.Interfaces.Document_456")]
  [CustomCategory("Attribute.Interfaces.Document_457")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool UsePreviousTableTemplates
  {
    [DebuggerStepThrough] get => this.usePreviousTableTemplates;
    set => this.SetUsePreviousTableTemplates(value, true, true);
  }

  /// <summary>Установить новое значение свойства UsePreviousTableTemplates</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetUsePreviousTableTemplates(bool value, bool updateUI, bool updateLayout)
  {
    if (this.usePreviousTableTemplates == value)
      return;
    this.usePreviousTableTemplates = value;
    if (this.usePreviousTableTemplates)
      this.ApplyPreviousTableTemplate(false, false);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Найти шаблон таблицы на шаблоне предыдущей страницы</summary>
  /// <returns></returns>
  internal TableData FindPreviousTableTemplate()
  {
    if (!this.IsPageFlow || this.FlowID == null)
      return (TableData) null;
    if (!this.IsTemplate && this.HasTemplate())
      return ((TableData) this.Template).FindPreviousTableTemplate();
    if (this.Page == null)
      return (TableData) null;
    List<PageData> prevPageTemplate = this.Page.FindPrevPageTemplate();
    TableData previousTableTemplate = (TableData) null;
    for (int index = 0; index < prevPageTemplate.Count; ++index)
    {
      if (prevPageTemplate[index].GetFirstFlowElement(this.flowID) is TableData firstFlowElement)
      {
        if (firstFlowElement.NodesCount > 0)
          return firstFlowElement;
        if (previousTableTemplate == null)
          previousTableTemplate = firstFlowElement;
      }
    }
    return previousTableTemplate;
  }

  /// <summary>Применить настройки из шаблона предполагаемой предыдущей таблицы</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ApplyPreviousTableTemplate(bool updateUI, bool updateLayout)
  {
    TableData previousTableTemplate = this.FindPreviousTableTemplate();
    if (previousTableTemplate == null)
      return;
    this.Clear(false, false);
    this.AssignBounds(this.Bounds with
    {
      Width = previousTableTemplate.Bounds.Width
    }, true, false, false);
    this.SetGridColumnsParams(previousTableTemplate.GridColumnsParams, false, true);
    if (updateLayout)
    {
      this.UpdateLayout(updateUI);
    }
    else
    {
      if (!updateUI)
        return;
      this.RefreshUI();
    }
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки</param>
  public override void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (template == null)
      return;
    if (!(template is TableData tableData))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) this.Template.Id, (object) this.Id));
    bool flag1 = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag1)
      this.SuspendUpdateGeometryRefreshUI();
    bool flag2 = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag2)
      this.SuspendUpdateLayout();
    try
    {
      this.isPageFlow = tableData.isPageFlow;
      this.autoSizeHeight = tableData.autoSizeHeight;
      this.alignLastRows = tableData.alignLastRows;
      this.isColumn = tableData.isColumn;
      this.isFixedStructureArea = tableData.IsFixedStructureArea;
      this.usePreviousTableTemplates = tableData.usePreviousTableTemplates;
      this.drawGridToBottom = tableData.drawGridToBottom;
      bool isTableCell = this.IsTableCell;
      if ((this.UseGridFromOverrideTemplate() || !isTableCell || this.IsOverridden2(OverrideFlags2.ParentGrid)) && tableData.gridColumnsParams != null)
        this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(tableData.gridColumnsParams), false, true);
      if (tableData.isPageFlow)
      {
        ImDocumentData ownerDocument = this.OwnerDocument;
        if (ownerDocument != null)
        {
          if (tableData.flowID != null)
          {
            FlowID flow = ownerDocument.FindFlowIDFromTemplate(tableData.flowID);
            if (flow == null)
            {
              flow = tableData.flowID.Clone();
              flow.TemplateFlowID = tableData.flowID;
              ownerDocument.AddDocumentFlow(flow, true);
            }
            this.flowID = flow;
          }
          else
            this.flowID = (FlowID) null;
        }
      }
      base.ApplyTemplateProperties(template, false, false, isLoading);
    }
    finally
    {
      if (!flag2)
        this.ResumeUpdateLayout(false, true);
      if (!flag1)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  private bool UseGridFromOverrideTemplate()
  {
    return this.Template is TableData template && this.ParentCell != null && this.ParentCell.Template != template.ParentCell && !this.IsOverridden(OverrideFlags.Grid) && !string.IsNullOrEmpty(template.OverrideTemplateId);
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is TableData;
  }

  /// <summary>Получить шаблон таблицы представления, который содержит шаблоны ячеек таблицы.
  /// Если в шаблоне этой таблицы нет, то ищет в первой таблице</summary>
  public virtual TableData GetTableStructureTemplate()
  {
    DocumentTreeNode structureTemplate = (DocumentTreeNode) null;
    if (this.UsePreviousTableTemplates && (this.IsHeaderCell || this.IsTopLevelTable))
      structureTemplate = this.FindFirstTable().Template;
    if (structureTemplate == null)
      structureTemplate = this.Template;
    return (TableData) structureTemplate;
  }

  /// <summary>Применить к элементу структуру его шаблона</summary>
  /// <returns>Новые элементы дерева</returns>
  public override List<DocumentTreeNode> ApplyTemplateTreeStructure(
    bool updateTemplateLinks,
    bool returnNewNodes,
    bool updateUI,
    bool updateLayout)
  {
    return this.ApplyTemplateTreeStructure((DocumentTreeNode) this.GetTableStructureTemplate(), updateTemplateLinks, returnNewNodes, updateUI, updateLayout);
  }

  /// <summary>Применить структуру шаблона к узлам сделанным по шаблону</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateTemplateLinks">Обновить ссылки на шаблон</param>
  /// <param name="returnNewNodes">Вернуть созданные в результате применения узлы</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Созданные в результате применения узлы</returns>
  protected override List<DocumentTreeNode> ApplyTemplateTreeStructure(
    DocumentTreeNode template,
    bool updateTemplateLinks,
    bool returnNewNodes,
    bool updateUI,
    bool updateLayout)
  {
    bool geometryOverrided = this.TemplateGeometryOverrided;
    if (template is TableData tableData && this.IsColumnGridOwner() && tableData.IsColumnGridOwner())
    {
      int num1 = -1;
      for (int index = 0; index < tableData.gridColumnsParams.Count; ++index)
      {
        int num2 = 0;
        while (num2 < this.gridColumnsParams.Count && this.gridColumnsParams[num2].TemplateID != tableData.gridColumnsParams[index].ID)
          ++num2;
        ++num1;
        if (num2 == this.gridColumnsParams.Count)
        {
          RowColParams colParams = tableData.gridColumnsParams[index].Clone();
          colParams.TemplateID = tableData.gridColumnsParams[index].ID;
          this.InsertNewGridColumn(num1, colParams, updateUI, updateLayout);
        }
        else
        {
          RowColParams gridColumnsParam = this.gridColumnsParams[num2];
          if (num1 != num2)
            this.MoveGridColumn(num2, num1, updateUI, updateLayout);
        }
      }
      for (int index = this.gridColumnsParams.Count - 1; index >= 0; --index)
      {
        if (this.gridColumnsParams[index].HasTemplate && TableData.GetRowColParams(tableData.gridColumnsParams, this.gridColumnsParams[index].TemplateID) == null)
          this.RemoveGridColumn(index, false, updateUI, updateLayout);
      }
    }
    if (tableData != null && this.gridRowsParams != null && tableData.gridRowsParams != null)
    {
      int num3 = 0;
      for (int index = 0; index < tableData.gridRowsParams.Count; ++index)
      {
        int num4 = 0;
        while (num4 < this.gridRowsParams.Count && this.gridRowsParams[num4].TemplateID != tableData.gridRowsParams[index].ID)
          ++num4;
        while (num3 < this.gridRowsParams.Count && !this.gridRowsParams[num3].HasTemplate)
          ++num3;
        if (num4 == this.gridRowsParams.Count)
        {
          RowColParams rowParams = tableData.gridRowsParams[index].Clone();
          rowParams.TemplateID = tableData.gridRowsParams[index].ID;
          this.InsertNewGridRow(num3, rowParams, false, -1, updateUI, updateLayout);
        }
        else
        {
          RowColParams gridRowsParam = this.gridRowsParams[num4];
          if (num3 != num4)
            this.MoveGridRow(num4, num3, updateUI, updateLayout);
        }
      }
      for (int index = this.gridRowsParams.Count - 1; index >= 0; --index)
      {
        if (this.gridRowsParams[index].HasTemplate && TableData.GetRowColParams(tableData.gridRowsParams, this.gridRowsParams[index].TemplateID) == null)
          this.RemoveGridRow(index, false, updateUI, updateLayout);
      }
    }
    if (geometryOverrided != this.TemplateGeometryOverrided)
    {
      if (geometryOverrided)
        this.SetOverrideFlags(OverrideFlags.Geometry);
      else
        this.ResetOverrideFlags(OverrideFlags.Geometry);
    }
    return base.ApplyTemplateTreeStructure(template, updateTemplateLinks, returnNewNodes, updateUI, updateLayout);
  }

  /// <summary>Применить к элементам дерева их шаблоны</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void ApplyTreeTemplates(bool updateUI, bool updateLayout)
  {
    bool flag = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag)
      this.SuspendUpdateLayout();
    try
    {
      base.ApplyTreeTemplates(false, false);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateLayout(updateUI, updateLayout);
    }
  }

  /// <summary>Применить только изменения в дереве шаблона</summary>
  public override void ApplyThisTemplateChanges(bool recursive, bool updateUI, bool updateLayout)
  {
    List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
    if (updateLayout && this.ConnectionList != null)
    {
      documentTreeNodeList = new List<DocumentTreeNode>(this.ConnectionList.Count);
      int index = 0;
      for (int count = this.ConnectionList.Count; index < count; ++index)
      {
        if (this.ConnectionList[index] is ReferenceToTemplate)
        {
          DocumentTreeNode ownerNode = this.ConnectionList[index].OwnerNode;
          if (ownerNode != null && !ownerNode.SuspendedUpdateLayoutFlag)
          {
            ownerNode.SuspendUpdateLayout();
            documentTreeNodeList.Add(ownerNode);
          }
        }
      }
    }
    try
    {
      base.ApplyThisTemplateChanges(recursive, updateUI, updateLayout);
    }
    finally
    {
      if (updateLayout && documentTreeNodeList != null)
      {
        int index = 0;
        for (int count = documentTreeNodeList.Count; index < count; ++index)
          documentTreeNodeList[index].ResumeUpdateLayout(updateUI, true);
      }
    }
  }

  /// <summary>Таблица - владелец этой виртуальной таблицы</summary>
  internal TableData OwnerTable
  {
    [DebuggerStepThrough] get => this.Owner as TableData;
  }

  /// <summary>Таблица является столбцом</summary>
  /// <remarks>Таблица распределяет свои дочерние ячейки как строки в столбце, т.е. сверху вниз</remarks>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool IsColumn
  {
    [DebuggerStepThrough] get
    {
      return this.IsVirtualNode && this.OwnerTable != null ? this.OwnerTable.IsColumn : this.isColumn;
    }
    set => this.isColumn = value;
  }

  /// <summary>Таблица с фиксированной структурой ячеек.
  /// Для совместимости с бланками старого формата</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_561")]
  [CustomDescription("Attribute.Interfaces.Document_562")]
  [CustomCategory("Attribute.Interfaces.Document_511")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool IsFixedStructureArea
  {
    [DebuggerStepThrough] get => this.isFixedStructureArea;
    set => this.SetIsFixedStructureArea(value, true, true);
  }

  public void SetIsFixedStructureArea(bool value, bool updateUI, bool updateLayout)
  {
    if (this.isFixedStructureArea == value)
      return;
    if (value)
    {
      foreach (RectangleElement rectangleElement in this.nodes.OfType<RectangleElement>())
        rectangleElement.setProperBounds(new RectangleF(rectangleElement.bounds.X - this.bounds.X, rectangleElement.bounds.Y - this.bounds.Y, rectangleElement.bounds.Width, rectangleElement.bounds.Height));
    }
    else
    {
      foreach (RectangleElement rectangleElement in this.nodes.OfType<RectangleElement>())
        rectangleElement.setProperBounds(rectangleElement.bounds);
    }
    this.AssignIsFixedStructureArea(value, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
  }

  /// <summary>Установить новое значение свойства IsFixedStructureArea</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignIsFixedStructureArea(bool value, bool updateUI, bool updateLayout)
  {
    if (this.IsFixedStructureArea == value)
      return;
    this.isFixedStructureArea = value;
    foreach (VisualNode visualNode in this.nodes.OfType<VisualNode>())
      visualNode.DestroyUI();
    if (value)
      return;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Таблица является строкой</summary>
  /// <remarks>Таблица распределяет свои дочерние ячейки как столбцы в строке, т.е. слева направо.</remarks>
  [Browsable(false)]
  public virtual bool IsRow
  {
    [DebuggerStepThrough] get => !this.IsColumn;
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child)
  {
    return !this.IsVirtualNode && this.CanAddChildElement(child.GetType());
  }

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(Type type)
  {
    if (this.IsVirtualNode)
      return false;
    return typeof (RectangleElement).IsAssignableFrom(type) || typeof (TableData).IsAssignableFrom(type);
  }

  /// <summary>Имя столбца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_458")]
  [CustomDescription("Attribute.Interfaces.Document_459")]
  [CustomCategory("Attribute.Interfaces.Document_460")]
  public virtual string ColumnName
  {
    [DebuggerStepThrough] get
    {
      if (this.IsColumn)
        return this.GetName();
      return this.ParentCell != null ? this.ParentCell.ColumnName : "";
    }
  }

  /// <summary>Имя строки</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_461")]
  [CustomDescription("Attribute.Interfaces.Document_462")]
  [CustomCategory("Attribute.Interfaces.Document_463")]
  public virtual string RowName
  {
    [DebuggerStepThrough] get
    {
      if (this.IsRow)
        return this.GetName();
      return this.ParentCell != null ? this.ParentCell.RowName : "";
    }
  }

  /// <summary>Создать и вставить новую строку</summary>
  /// <param name="index">Индекс строки в Nodes</param>
  /// <param name="rowModel">Образец строки</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void InsertNewRow(
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
          child = (RectangleElement) new TableData((DocumentTreeNode) null, new RectangleF(new PointF(0.0f, 0.0f), TableData.DefaultCellSize), false);
          for (int index1 = 0; index1 < gridColumnsParams.Count; ++index1)
          {
            RectangleElement[] gridColumnCells = ((TableData) child).CreateGridColumnCells(gridColumnsParams, index, child.Nodes.Count, false, false);
            if (gridColumnCells != null)
            {
              for (int index2 = 0; index2 < gridColumnCells.Length; ++index2)
                child.AddChildNode((DocumentTreeNode) gridColumnCells[index2], false, false);
            }
          }
        }
        else if (this.nodes.Count > 0)
        {
          child = (RectangleElement) this.nodes[this.nodes.Count - 1].Clone(true, false);
          child.Name = (string) null;
        }
        else
          child = (RectangleElement) new TextData((DocumentTreeNode) null, new RectangleF(new PointF(0.0f, 0.0f), TableData.DefaultCellSize), false);
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

  /// <summary>Ячейка является таблицей верхнего уровня,
  /// т.е. не принадлежит какой либо таблице</summary>
  [Browsable(false)]
  public bool IsTopLevelTable
  {
    [DebuggerStepThrough] get => this.ParentCell == null;
  }

  /// <summary>Таблица верхнего уровня в состав которой входит ячейка,
  /// если ячейка сама таблица верхнего уровня, то возвращает указатель на саму себя</summary>
  [Browsable(false)]
  public override TableData TopLevelTable
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell != null ? parentCell.TopLevelTable : this;
    }
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    if (isLoading || this.isVirtualNode)
    {
      base.AssignParent(value, updateUI, updateLayout, isLoading);
    }
    else
    {
      int num = !updateUI ? 1 : (!this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0));
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      TableData parentCell = this.ParentCell;
      if (parentCell != null)
        this.SetDistributingCount(this.distributingCount - parentCell.distributingCount);
      this.AssignParentFlow((IParentFlow) null);
      base.AssignParent(value, false, false, isLoading);
      if (value is TableData tableData)
      {
        if (updateUI)
          this.CreateUI();
        this.parentFlow = (IParentFlow) null;
        this.SetDistributingCount(this.distributingCount + tableData.distributingCount);
      }
      else if (updateUI)
        this.CreateUI();
      if (this.IsPageFlow && this.FlowID != null && this.OwnerDocument != null && !this.OwnerDocument.DocumentFlows.Contains(this.FlowID))
        this.OwnerDocument.DocumentFlows.Add(this.FlowID);
      if (this.name == "" && this.parent != null)
        this.OnNameChanged(new NameChanged_EventArgs(this.name));
      if (num != 0)
        return;
      this.ResumeUpdateRefreshUI(updateUI, updateLayout);
    }
  }

  /// <summary>Присвоить значение свойству Page</summary>
  /// <param name="value">Новое значение Page</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void AssignPage(PageData value, bool updateUI, bool updateLayout)
  {
    if (this.page == value)
      return;
    if (this.isVirtualNode)
    {
      base.AssignPage(value, updateUI, updateLayout);
    }
    else
    {
      if (this.page != null && this.isPageFlow)
        this.DisconnectFlowFromPage();
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (this.page != null)
      {
        imDocumentData = this.page.OwnerDocument;
        if (this.reference != null && this.reference.IsDependOnDocument)
          this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
      }
      base.AssignPage(value, updateUI, updateLayout);
      if (!this.isVirtualNode && this.reference != null)
      {
        if (this.reference.IsDependOnPage || this.reference.IsDependOnDocument && this.page != null && imDocumentData != this.page.OwnerDocument)
          this.reference.UpdateLink(updateUI, updateLayout);
        if (this.page != null && this.reference.IsDependOnDocument)
          this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
      }
      if (this.page == null || !this.isPageFlow)
        return;
      this.ConnectFlowToPage();
    }
  }

  /// <summary>Вставить в заданную позицию дочерний узел</summary>
  /// <param name="index">Позиция в которую будет вставлен узел</param>
  /// <param name="child">Узел</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isNew">Узел новый и не требуется это проверять</param>
  /// <returns>true, если вставка не была отменена</returns>
  public override bool InsertChildNode(
    int index,
    DocumentTreeNode child,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout,
    bool isNew = false)
  {
    if (child == null)
      throw new ArgumentNullException(nameof (child));
    if (!this.isVirtualNode & uniteTable && !isNew && (child.Parent != this || child.Index != index))
    {
      if (child is RectangleElement rectangleElement)
        rectangleElement.UniteTable();
      if (this.isColumn && index != 0 && index == this.nodes.Count && this.nodes[this.nodes.Count - 1] is RectangleElement node)
        node.UniteTable();
    }
    if (!insertByShift && child.Parent == null && child is VisualNode visualNode && !visualNode.Visible && visualNode.Template is VisualNode template && !template.Visible && !template.CloneByTemplateWithParent)
      visualNode.SetVisible(true, false, false, false, false);
    if (!insertByShift && this.IsTemplate && this.UsePreviousTableTemplates)
      this.SetUsePreviousTableTemplates(false, false, false);
    return base.InsertChildNode(index, child, insertByShift, uniteTable, updateUI, updateLayout, isNew);
  }

  /// <summary>Базовая часть обработки события ChildNodeAdded
  /// Вспомогательный метод, чтобы можно было переопределять обработку не меняя последовательности вызова событий</summary>
  /// <param name="e">Аргумент события</param>
  protected override void OnChildNodeAddedCore(ChildNode_EventArgs e)
  {
    base.OnChildNodeAddedCore(e);
    ChildNodeAdded_EventHandler addedInFlowChain = this.FindFirstTable().childNodeAddedInFlowChain;
    if (addedInFlowChain == null)
      return;
    addedInFlowChain((object) this, e);
  }

  /// <summary>Событие Добавлен дочерний узел где-то в цепочке разбития данных</summary>
  public event ChildNodeAdded_EventHandler ChildNodeAddedInFlowChain
  {
    add => this.childNodeAddedInFlowChain += value;
    remove => this.childNodeAddedInFlowChain -= value;
  }

  /// <summary>Событие Удалён дочерний узел где-то в цепочке разбития данных</summary>
  public event ChildNodeRemoved_EventHandler ChildNodeRemovedInFlowChain
  {
    add => this.childNodeRemovedInFlowChain += value;
    remove => this.childNodeRemovedInFlowChain -= value;
  }

  /// <summary>Базовая часть обработки события ChildNodeRemoved
  /// Вспомогательный метод, чтобы можно было переопределять обработку не меняя последовательности вызова событий</summary>
  /// <param name="e">Аргумент события</param>
  protected override void OnChildNodeRemovedCore(ChildNode_EventArgs e)
  {
    base.OnChildNodeRemovedCore(e);
    ChildNodeRemoved_EventHandler removedInFlowChain = this.FindFirstTable().childNodeRemovedInFlowChain;
    if (removedInFlowChain == null)
      return;
    removedInFlowChain((object) this, e);
  }

  public RectangleElement InsertRowByTemplate(int index, RectangleElement rowTemplate)
  {
    RectangleElement child = (RectangleElement) rowTemplate.CloneFromTemplate(true, true);
    this.InsertChildNode(index, (DocumentTreeNode) child, false, false, false, false, false);
    return child;
  }

  public RectangleElement AddRowByTemplate(RectangleElement rowTemplate)
  {
    RectangleElement lastCell = this.FindLastCell();
    RectangleElement rectangleElement = (RectangleElement) rowTemplate.CloneFromTemplate(true, true);
    RectangleElement child = rectangleElement;
    lastCell.AddChildNode((DocumentTreeNode) child, false, false, false, false);
    return rectangleElement;
  }

  /// <summary>Разрешен интерфейс изменения ширины</summary>
  [Browsable(false)]
  protected virtual bool IsResizableWidth
  {
    [DebuggerStepThrough] get => this.IsColumn && this.NeedUI && !this.IsTopLevelTable && false;
  }

  /// <summary>Разрешен интерфейс изменения высоты</summary>
  [Browsable(false)]
  protected virtual bool IsResizableHeight
  {
    [DebuggerStepThrough] get => !this.IsColumn && this.NeedUI && this.ParentCell != null;
  }

  /// <summary>Метод вызывается после добавления дочернего элемента, но до вызова события ChildNodeAdded</summary>
  /// <param name="child">Дочерний элемент</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected override void PostProcessAddChildNode(
    DocumentTreeNode child,
    bool insertByShift,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsVirtualNode)
    {
      base.PostProcessAddChildNode(child, insertByShift, updateUI, updateLayout);
    }
    else
    {
      if (child is VisualNode visualNode)
        visualNode.SetNeedUIRecursive(this.NeedUI, updateUI);
      if (child is TableData tableData)
        this.SetDistributingCount(this.distributingCount);
      base.PostProcessAddChildNode(child, insertByShift, updateUI, updateLayout);
      if (tableData != null)
      {
        bool flag = false;
        if (!insertByShift && tableData.gridColumnsParams != null)
        {
          this.GetGridColumnsParams(true);
          if (this.gridColumnsParams != null && this.gridColumnsParams.Count != 0 && this.gridColumnsParams.Count == tableData.gridColumnsParams.Count)
          {
            flag = true;
            for (int index = 0; index < this.gridColumnsParams.Count; ++index)
            {
              if ((double) this.gridColumnsParams[index].Size != (double) tableData.gridColumnsParams[index].Size)
              {
                flag = false;
                break;
              }
            }
          }
        }
        if (flag)
        {
          tableData.SetGridColumnsParams((List<RowColParams>) null, false, false);
          tableData.overrideFlags2 &= ~OverrideFlags2.ParentGrid;
        }
        else if (tableData.gridColumnsParams != null && (tableData.overrideFlags2 & OverrideFlags2.ParentGrid) == OverrideFlags2.None && (this.gridColumnsParams == null || tableData.gridColumnsParams.Count != this.gridColumnsParams.Count))
          tableData.SetGridColumnsParams(TableData.CloneRowColParams(tableData.gridColumnsParams), true, false);
      }
      if (child is RectangleElement rectangleElement)
      {
        if ((rectangleElement.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) == OverrideFlags2.None && (double) rectangleElement.defaultRowSize != (double) this.defaultRowSize)
          rectangleElement.overrideFlags2 |= OverrideFlags2.ParentDefaultRowSize;
        RectangleF bounds = rectangleElement.bounds;
        if (this.isColumn && (double) bounds.Width != (double) this.bounds.Width)
        {
          bounds.Width = this.bounds.Width;
          rectangleElement.AssignBounds(bounds, false, false, false);
          rectangleElement.SetCellSizes(bounds, false, false, false, false);
        }
      }
      bool updateLayoutFlag = this.NeedUpdateLayoutFlag;
      this.SetNeedUpdateLayoutFlag(true, !insertByShift, updateUI, updateLayout);
      if (this.prevCell == null || updateLayoutFlag)
        return;
      int index1 = child.Index;
      if (index1 != 0 && (index1 <= 0 || !(this.nodes[index1 - 1] is RectangleElement node) || node.TableCellType != CellType.Header))
        return;
      this.prevCell.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    }
  }

  /// <summary>Метод вызывается после того как два дочерних элемента поменяются местами</summary>
  /// <param name="index1">Индекс одного элемента</param>
  /// <param name="index2">Индекс второго элемента</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected override void PostExchangeChildsMethod(
    int index1,
    int index2,
    bool updateUI,
    bool updateLayout)
  {
    base.PostExchangeChildsMethod(index1, index2, updateUI, updateLayout);
    if (this.IsVirtualNode)
      return;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Генерирует событие Removed</summary>
  protected override void OnRemoved(Removed_EventArgs e)
  {
    base.OnRemoved(e);
    if (this.IsVirtualNode || e.RemovedByShift)
      return;
    this.CutFromChain();
  }

  /// <summary>Метод вызывается при удалении ветки, в которой находится этот узел</summary>
  protected override void OnBranchRemoved(Removed_EventArgs e)
  {
    if (!this.IsVirtualNode && !e.RemovedByShift && this.reference != null)
      this.reference.DisconnectLink();
    base.OnBranchRemoved(e);
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
      this.SetNeedUpdateLayoutFlag(true, !e.ByShift, e.UpdateUI, e.UpdateLayout);
    }
  }

  /// <summary>Получить тип для ячейки по умолчанию</summary>
  public virtual Type GetDataShowElementType() => typeof (TextData);

  /// <summary>Проверить есть ли данные заданного потока в таблице</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <returns>Возвращает true, если данных потока в таблице нет</returns>
  public bool FlowIsEmpty(FlowID flow)
  {
    IFlowElement flowElementByName = (IFlowElement) null;
    return !(this.GetFirstFlowElement(flow, ref flowElementByName) is TableData firstFlowElement) || firstFlowElement.Nodes.Count == 0;
  }

  /// <summary>Таблица не содержит данных потока.
  /// Цепочка следующих таблиц не проверяется</summary>
  /// <returns></returns>
  public override bool AllFlowsIsEmpty()
  {
    if (this.prevCell == null)
      return false;
    int count = this.nodes.Count;
    if (!this.DistributeBuffer.IsEmpty<RectangleElement>())
      return false;
    if (count != 0)
    {
      int num = this.CalcFirstHeaderCount();
      if (num < count)
      {
        for (int index = num; index < count; ++index)
        {
          if (this.nodes[index] is IFlowElement node2)
          {
            if (!node2.AllFlowsIsEmpty())
              return false;
          }
          else if (this.nodes[index] is RectangleElement node1 && !node1.AllFlowsIsEmpty() && !node1.IsHeaderCell)
            return false;
        }
        return true;
      }
    }
    return true;
  }

  /// <summary>Элемент не содержит данных
  /// <remarks>
  /// Если emptyCellIsData - true, то ячейка считается пустой только когда является продолжением и ничего не содержит
  /// (а значит её можно удалить), а одиночная пустая ячейка считается содержимым для таблицы
  /// Если emptyCellIsData - false, то она считается пустой когда не содержит данных либо внутренние ячейки пусты
  /// </remarks>
  /// </summary>
  /// <param name="emptyCellIsData">Допустимы пустые ячейки</param>
  /// <param name="checkNextTable">Проверять следующую ячейку</param>
  /// <returns></returns>
  public override bool IsEmptyData(bool emptyCellIsData, bool checkNextCell = true)
  {
    if (!this.distributeBuffer.IsEmpty<RectangleElement>())
    {
      if (emptyCellIsData)
        return false;
      for (int index = 0; index < this.distributeBuffer.Count; ++index)
      {
        if (!this.distributeBuffer[index].IsEmptyData(emptyCellIsData, false))
          return false;
      }
    }
    int count = this.nodes.Count;
    if (count != 0)
    {
      int num = this.CalcFirstHeaderCount();
      if (num < count)
      {
        for (int index = num; index < count; ++index)
        {
          if (this.nodes[index] is RectangleElement node && !node.IsHeaderCell && (emptyCellIsData || !node.IsEmptyData(emptyCellIsData, false)))
            return false;
        }
      }
    }
    return !checkNextCell || this.nextCell == null || this.nextCell.IsEmptyData(emptyCellIsData);
  }

  /// <summary>Генерирует событие NameChanged</summary>
  public override void OnNameChanged(NameChanged_EventArgs e)
  {
    base.OnNameChanged(e);
    if (this.nodes == null)
      return;
    int index = 0;
    for (int count = this.nodes.Count; index < count; ++index)
      this.nodes[index].OnNameChanged(new NameChanged_EventArgs(this.nodes[index].Name));
  }

  /// <summary>Разбить эту ячейку</summary>
  /// <param name="rows">Строк</param>
  /// <param name="cols">Столбцов</param>
  /// <param name="splitOne">Разбивать на таблицу 1x1</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override TableData SplitCell(
    int rows,
    int cols,
    bool splitOne,
    bool updateUI,
    bool updateLayout)
  {
    if (rows == 0 && cols == 0 || rows == 1 && cols == 1 && !splitOne)
      return this;
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      CellType cellType = CellType.DataCell;
      TableData parentCell = this.ParentCell;
      this.SetGridColumnsParams(new List<RowColParams>(cols), true, true);
      this.SetGridRowsParams((List<RowColParams>) null);
      int num1 = cols != 0 ? cols : 1;
      int num2 = rows != 0 ? rows : 1;
      RectangleF rectangleF = this.CalcRealProperBounds(this.ProperBounds);
      float height = rectangleF.Height;
      float width = rectangleF.Width;
      SizeF size1 = new SizeF(width, height / (float) num2);
      if (this.TopLevelTable.IsPageFlow && (this.IsTopLevelTable || this.IsDataNode))
        size1.Height = !this.IsFixedSizeRows ? TableData.DefaultCellSize.Height : this.DefaultRowSize;
      SizeF size2 = new SizeF(width / (float) num1, size1.Height);
      PointF location = rectangleF.Location;
      RectangleBorder rectangleBorder1 = (RectangleBorder) null;
      RectangleBorder rectangleBorder2 = (RectangleBorder) null;
      if (this.Borders != null)
      {
        rectangleBorder1 = this.Borders.Clone();
        rectangleBorder1.Top = rectangleBorder1.Bottom;
        rectangleBorder2 = this.Borders.Clone();
        rectangleBorder2.Left = rectangleBorder2.Right;
        rectangleBorder2.Top = rectangleBorder2.Bottom;
      }
      for (int index = 0; index < cols || index == 0; ++index)
      {
        this.gridColumnsParams.Add(new RowColParams(this, index, (string) null, size2.Width));
        if (index == 0 && rectangleBorder1 != null)
          this.gridColumnsParams[index].AssignBorderLine1(rectangleBorder1.Left);
        else if (rectangleBorder2 != null)
          this.gridColumnsParams[index].AssignBorderLine1(rectangleBorder2.Right);
        if (rectangleBorder2 != null)
          this.gridColumnsParams[index].AssignBorderLine2(rectangleBorder2.Right);
      }
      this.Clear(false, false);
      this.isColumn = true;
      this.isFixedStructureArea = false;
      int num3 = 0;
      while (true)
      {
        if (num3 >= rows)
          goto label_49;
label_19:
        if (cols == 0)
        {
          RectangleElement emptySingleCell = this.CreateEmptySingleCell((DocumentTreeNode) this, new RectangleF(location, size1), false);
          if (rectangleBorder1 != null)
            emptySingleCell.borders = num3 != 0 ? rectangleBorder1 : this.Borders.Clone();
          emptySingleCell.TableCellType = cellType;
          if (rectangleBorder2 != null)
          {
            if (num3 == 0)
            {
              emptySingleCell.Borders = this.Borders.Clone();
            }
            else
            {
              emptySingleCell.borders = this.Borders.Clone();
              emptySingleCell.borders.Top = emptySingleCell.borders.Bottom;
            }
          }
          location.X = emptySingleCell.Bounds.Right;
          location = new PointF(this.bounds.X, emptySingleCell.Bounds.Bottom);
          emptySingleCell.SetVisible(true, false, false, false, true, false);
        }
        else
        {
          TableData parent;
          if (rows > 0)
          {
            parent = this.CreateEmptyTable(false, (DocumentTreeNode) this, new RectangleF(location, size1), false);
            if (rectangleBorder1 != null)
            {
              if (num3 == 0)
                parent.borders = this.Borders.Clone();
              else
                parent.borders = rectangleBorder1;
            }
          }
          else
          {
            this.isColumn = false;
            parent = this;
          }
          parent.TableCellType = cellType;
          parent.minHeight = size1.Height;
          for (int index = 0; index < cols; ++index)
          {
            RectangleElement emptySingleCell = this.CreateEmptySingleCell((DocumentTreeNode) parent, new RectangleF(location, size2), false);
            if (rows == 0)
              emptySingleCell.SetVisible(this.Visible, false, false, false, true, false);
            emptySingleCell.TableCellType = cellType;
            if (rectangleBorder2 != null)
            {
              if (index == 0 && num3 == 0)
                emptySingleCell.Borders = this.Borders.Clone();
              else if (num3 == 0)
              {
                emptySingleCell.borders = this.Borders.Clone();
                emptySingleCell.borders.Left = emptySingleCell.borders.Right;
              }
              else if (index == 0)
              {
                emptySingleCell.borders = this.Borders.Clone();
                emptySingleCell.borders.Top = emptySingleCell.borders.Bottom;
              }
              else
                emptySingleCell.borders = rectangleBorder2;
            }
            location.X = emptySingleCell.Bounds.Right;
          }
          location = new PointF(this.bounds.X, parent.Bounds.Bottom);
          parent.SetVisible(this.Visible, false, false, false, true, false);
        }
        ++num3;
        continue;
label_49:
        if (num3 == 0)
          goto label_19;
        break;
      }
    }
    finally
    {
      this.SetPropertiesChangedFlag(false, false, false, false, false);
      this.TreeStructureChangedFlag = false;
      if (updateLayout)
        this.ResetNeedUpdateLayoutFlag(true);
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
    return this;
  }

  /// <summary>Для внутреннего использования. Разбить дочернюю ячейку на одном уровне.
  /// Вызывается из SplitCell</summary>
  /// <param name="cell">Дочерняя ячейка</param>
  /// <param name="rows">Строк</param>
  /// <param name="cols">Столбцов</param>
  public virtual void SplitChildCell(RectangleElement cell, int rows, int cols)
  {
    TableGridPosition gridPos = cell.GridPos;
    int num1 = cols != 0 ? cols : 1;
    int num2 = rows != 0 ? rows : 1;
    RectangleF properBounds = cell.ProperBounds;
    float height = properBounds.Height;
    SizeF size = new SizeF(properBounds.Width / (float) num1, height / (float) num2);
    PointF location = properBounds.Location;
    int num3 = cell.Index + 1;
    if (rows == 0 && this.IsRow)
    {
      properBounds.Size = size;
      cell.WidthOverrided = true;
      cell.AssignBounds(properBounds, true, false, false);
      cell.SetNeedUpdateUIGeometryRecursive(true, false);
      location.X += size.Width;
      cell.GridPos = new TableGridPosition(0);
      for (int index = 0; index < cols - 1; ++index)
      {
        RectangleElement emptySingleCell = this.CreateEmptySingleCell((DocumentTreeNode) null, new RectangleF(location, size), true);
        emptySingleCell.TableCellType = cell.TableCellType;
        emptySingleCell.GridPos = index >= cols - 2 ? gridPos : new TableGridPosition(0);
        if (cell.Borders != null)
          emptySingleCell.borders = cell.Borders.Clone();
        emptySingleCell.WidthOverrided = true;
        this.InsertChildNode(num3 + index, (DocumentTreeNode) emptySingleCell, false, true, false, false, false);
        location.X += size.Width;
      }
    }
    else
    {
      if (cols != 0)
        return;
      properBounds.Size = size;
      this.isColumn = true;
      cell.HeightOverrided = true;
      cell.AssignBounds(properBounds, true, false, false);
      location.Y += size.Height;
      for (int index = 0; index < rows - 1; ++index)
      {
        RectangleElement emptySingleCell = this.CreateEmptySingleCell((DocumentTreeNode) null, new RectangleF(location, size), true);
        emptySingleCell.TableCellType = cell.TableCellType;
        emptySingleCell.HeightOverrided = true;
        if (cell.Borders != null)
          emptySingleCell.borders = cell.Borders.Clone();
        this.InsertChildNode(num3 + index, (DocumentTreeNode) emptySingleCell, false, true, false, false, false);
        location.Y += size.Height;
      }
    }
  }

  /// <summary>Команда пользователя "Удалить". В общем случае не совпадает с Remove()</summary>
  /// <param name="update">Обновлять внешний вид и разбивку по страницам</param>
  public override void UserCommand_Delete(bool update)
  {
    this.UniteTable();
    base.UserCommand_Delete(update);
  }

  /// <summary>Сравнить положение двух ячеек</summary>
  /// <param name="cell1"></param>
  /// <param name="cell2"></param>
  /// <returns>Результат сравнения.
  /// -1 означает cell1 меньше cell2
  /// 0 означает cell1 равно cell2
  /// 1 означает cell1 больше cell2
  /// </returns>
  public static int ComparePosition(RectangleElement cell1, RectangleElement cell2)
  {
    if (cell1 == null)
      throw new ArgumentNullException(nameof (cell1));
    if (cell2 == null)
      throw new ArgumentNullException(nameof (cell2));
    PageData page1 = cell1.Page;
    PageData page2 = cell2.Page;
    if (page1 == page2)
    {
      PointF location1 = cell1.Location;
      PointF location2 = cell2.Location;
      if ((double) location1.Y < (double) location2.Y)
        return -1;
      if ((double) location1.Y > (double) location2.Y)
        return 1;
      if ((double) location1.X < (double) location2.X)
        return -1;
      return (double) location1.X > (double) location2.X ? 1 : 0;
    }
    if (page2 == null)
      return -1;
    return page1 == null ? 1 : page1.Index.CompareTo(page2.Index);
  }

  /// <summary>Энумератор для циклов прохода по ячейкам таблицы. Игнорирует узлы другого типа</summary>
  [Browsable(false)]
  public CellNodesEnumerator CellsEnumerator => new CellNodesEnumerator(this);

  /// <summary>Энумератор для циклов прохода по ячейкам таблицы. Игнорирует узлы другого типа</summary>
  [Browsable(false)]
  public TextCellEnumerator TextCellsEnumerator => new TextCellEnumerator(this);

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    int num = this.Template != null ? 1 : 0;
    if (num == 0 && !this.isColumn)
      xw.WriteAttributeString("isRow", "1");
    if (num == 0 && this.IsFixedStructureArea)
      xw.WriteAttributeString("isArea", "1");
    if (this.isPageFlow)
      xw.WriteAttributeString("isPageFlow", this.isPageFlow.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    bool firstTime;
    if (this.flowID != null)
      xw.WriteAttributeString("flowIDRef", objectRefId.GetId((object) this.flowID, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.usePreviousTableTemplates && this.IsTopLevelTable)
      xw.WriteAttributeString("usePreviousTableTemplates", this.usePreviousTableTemplates.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.prevCell != null)
      xw.WriteAttributeString("prevTable", objectRefId.GetId((object) this.prevCell, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!this.drawGridToBottom && this.IsTopLevelTable)
      xw.WriteAttributeString("drawGridToBottom", "0");
    if (!this.CanSwitchInternalCellsVisibity || !this.showSingleCellInTemplate.HasValue)
      return;
    xw.WriteAttributeString("showSingleCellInTemplate", this.showSingleCellInTemplate.Value ? "1" : "0");
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    bool firstTime = false;
    if (this.gridColumnsParams != null && ((this.overrideFlags2 & OverrideFlags2.ParentGrid) != OverrideFlags2.None || (this.overrideFlags & OverrideFlags.Grid) != OverrideFlags.None))
      WriteReadXmlHelper.WriteArrayToXml("Columns", (IList) this.gridColumnsParams, "Column", xw, objectRefId);
    if (this.gridRowsParams != null)
      WriteReadXmlHelper.WriteArrayToXml("Rows", (IList) this.gridRowsParams, "Row", xw, objectRefId);
    if (this.reference != null)
      this.reference.WriteToXml("Reference", xw, objectRefId);
    this.WriteXmlElementReference("ParentFlow", (object) this.parentFlow, xw, objectRefId, out firstTime);
    this.WriteXmlElementReference("NextFlowElement", (object) this.nextFlowElement, xw, objectRefId, out firstTime);
    if (!this.disabledHeaders.IsEmpty<string>())
      WriteReadXmlHelper.WriteStringListToXml("DisabledHeaders", this.disabledHeaders, "header", xw);
    base.WriteXmlElements(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (TableData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      TableData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    string localName = readArgs.Reader.LocalName;
    if (readArgs.Version < 10 && localName == "templateDataOverrided")
      return true;
    switch (localName)
    {
      case "Reference":
        TableData.ReadReference((DocumentTreeNode) this, readArgs);
        return true;
      case "isRow":
        TableData.ReadIsRow((DocumentTreeNode) this, readArgs);
        return true;
      default:
        if (base.ReadFieldFromXml(readArgs))
          return true;
        switch (localName)
        {
          case "Columns":
            TableData.ReadColumns((DocumentTreeNode) this, readArgs);
            return true;
          case "FlowCount":
            return true;
          case "NextFlowElement":
            TableData.ReadNextFlowElement((DocumentTreeNode) this, readArgs);
            return true;
          case "ParentFlow":
            TableData.ReadParentFlow((DocumentTreeNode) this, readArgs);
            return true;
          case "PrevFlowElement":
            TableData.ReadPrevFlowElement((DocumentTreeNode) this, readArgs);
            return true;
          case "Rows":
            TableData.ReadRows((DocumentTreeNode) this, readArgs);
            return true;
          case "StartFlowIndex":
            return true;
          case "alignLastRows":
            TableData.ReadAlignLastRows((DocumentTreeNode) this, readArgs);
            return true;
          case "dataOverrided":
            TableData.ReadDataOverrided((DocumentTreeNode) this, readArgs);
            return true;
          case "drawGridToBottom":
            TableData.ReadDrawGridToBottom((DocumentTreeNode) this, readArgs);
            return true;
          case "flowIDRef":
            TableData.ReadFlowIDRef((DocumentTreeNode) this, readArgs);
            return true;
          case "isArea":
            TableData.ReadIsArea((DocumentTreeNode) this, readArgs);
            return true;
          case "isColumn":
            TableData.ReadIsColumn((DocumentTreeNode) this, readArgs);
            return true;
          case "isPageFlow":
            TableData.ReadIsPageFlow((DocumentTreeNode) this, readArgs);
            return true;
          case "prevCell":
            TableData.ReadPrevTable((DocumentTreeNode) this, readArgs);
            return true;
          case "showSingleCellInTemplate":
            TableData.ReadShowSingleCellInTemplate((DocumentTreeNode) this, readArgs);
            return true;
          case "usePreviousTableTemplates":
            TableData.ReadUsePreviousTableTemplates((DocumentTreeNode) this, readArgs);
            return true;
          default:
            return false;
        }
    }
  }

  private static void InitReadFieldDict()
  {
    TableData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) RectangleElement.ReadFieldsDict);
    TableData.ReadFieldsDict.Add("Reference", new ReadFieldFromXmlDelegate(TableData.ReadReference));
    TableData.ReadFieldsDict.Add("isRow", new ReadFieldFromXmlDelegate(TableData.ReadIsRow));
    TableData.ReadFieldsDict.Add("isColumn", new ReadFieldFromXmlDelegate(TableData.ReadIsColumn));
    TableData.ReadFieldsDict.Add("isArea", new ReadFieldFromXmlDelegate(TableData.ReadIsArea));
    TableData.ReadFieldsDict.Add("Columns", new ReadFieldFromXmlDelegate(TableData.ReadColumns));
    TableData.ReadFieldsDict.Add("Rows", new ReadFieldFromXmlDelegate(TableData.ReadRows));
    TableData.ReadFieldsDict.Add("DisabledHeaders", new ReadFieldFromXmlDelegate(TableData.ReadDisabledHeaders));
    TableData.ReadFieldsDict.Add("dataOverrided", new ReadFieldFromXmlDelegate(TableData.ReadDataOverrided));
    TableData.ReadFieldsDict.Add("isPageFlow", new ReadFieldFromXmlDelegate(TableData.ReadIsPageFlow));
    TableData.ReadFieldsDict.Add("flowIDRef", new ReadFieldFromXmlDelegate(TableData.ReadFlowIDRef));
    TableData.ReadFieldsDict.Add("ParentFlow", new ReadFieldFromXmlDelegate(TableData.ReadParentFlow));
    TableData.ReadFieldsDict.Add("PrevFlowElement", new ReadFieldFromXmlDelegate(TableData.ReadPrevFlowElement));
    TableData.ReadFieldsDict.Add("NextFlowElement", new ReadFieldFromXmlDelegate(TableData.ReadNextFlowElement));
    TableData.ReadFieldsDict.Add("prevTable", new ReadFieldFromXmlDelegate(TableData.ReadPrevTable));
    TableData.ReadFieldsDict.Add("alignLastRows", new ReadFieldFromXmlDelegate(TableData.ReadAlignLastRows));
    TableData.ReadFieldsDict.Add("dataShowElementType", new ReadFieldFromXmlDelegate(TableData.ReadDataShowElementType));
    TableData.ReadFieldsDict.Add("usePreviousTableTemplates", new ReadFieldFromXmlDelegate(TableData.ReadUsePreviousTableTemplates));
    TableData.ReadFieldsDict.Add("drawGridToBottom", new ReadFieldFromXmlDelegate(TableData.ReadDrawGridToBottom));
    if (TableData.ReadFieldsDict.ContainsKey("Nodes"))
      TableData.ReadFieldsDict["Nodes"] = new ReadFieldFromXmlDelegate(TableData.ReadNodes);
    else
      TableData.ReadFieldsDict.Add("Nodes", new ReadFieldFromXmlDelegate(TableData.ReadNodes));
    TableData.ReadFieldsDict.Add("showSingleCellInTemplate", new ReadFieldFromXmlDelegate(TableData.ReadShowSingleCellInTemplate));
  }

  private static void ReadUsePreviousTableTemplates(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).usePreviousTableTemplates = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadDataShowElementType(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
  }

  private static void ReadAlignLastRows(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).alignLastRows = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadPrevTable(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (str == null || !(str != ""))
      return;
    if (!(readArgs.ObjectsId[(object) str] is TableData tableData))
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "prevCell", str);
    else
      ((RectangleElement) docNode).SetPrevCell((RectangleElement) tableData);
  }

  private static void ReadNextFlowElement(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    string str = readArgs.Reader.Value;
    if (readArgs.ObjectsId[(object) str] is IFlowElement flowElement)
      ((TableData) docNode).prevFlowElement = flowElement;
    else
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "nextFlowElement", str);
  }

  private static void ReadPrevFlowElement(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    string str = readArgs.Reader.Value;
    if (readArgs.ObjectsId[(object) str] is IFlowElement flowElement)
      ((TableData) docNode).prevFlowElement = flowElement;
    else
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "prevFlowElement", str);
  }

  private static void ReadParentFlow(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    string str = readArgs.Reader.Value;
    if (readArgs.ObjectsId[(object) str] is IParentFlow parentFlow)
      ((TableData) docNode).parentFlow = parentFlow;
    else
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "parentFlow", str);
  }

  private static void ReadFlowIDRef(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (str == null || !(str != ""))
      return;
    TableData tableData = (TableData) docNode;
    tableData.flowID = readArgs.ObjectsId[(object) str] as FlowID;
    if (tableData.flowID != null)
      return;
    DocumentTreeNode.AddObjectReference((object) tableData, readArgs.ObjectReferences, "flowID", str);
  }

  private static void ReadIsPageFlow(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).isPageFlow = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadDrawGridToBottom(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).drawGridToBottom = readArgs.Reader.Value == "1";
  }

  private static void ReadDataOverrided(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
  }

  private static void ReadRows(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TableData tableData = (TableData) docNode;
    int capacity = -1;
    if (readArgs.Reader.HasAttributes)
    {
      readArgs.Reader.MoveToAttribute("length");
      capacity = Convert.ToInt32(readArgs.Reader.Value);
      readArgs.Reader.MoveToElement();
    }
    List<RowColParams> rowColParamsList = capacity > 0 ? new List<RowColParams>(capacity) : new List<RowColParams>();
    WriteReadXmlHelper.ReadListFromXml((IList) rowColParamsList, typeof (RowColParams), readArgs);
    tableData.SetGridRowsParams(rowColParamsList);
    if (tableData.gridRowsParams != null)
    {
      for (int index = 0; index < tableData.gridRowsParams.Count; ++index)
      {
        if (tableData.gridRowsParams[index].ID == RowColParams.EmptyIDValue)
          tableData.gridRowsParams[index].ID = TableData.GenerateGridID(tableData.gridRowsParams, index);
      }
    }
    if (readArgs.Version >= 17 || (double) tableData.defaultRowSize != 0.0 || tableData.gridRowsParams == null || tableData.gridRowsParams.Count <= 0)
      return;
    int index1 = tableData.IsTopLevelTable ? 0 : tableData.GetGridRowIndex();
    if (index1 < 0)
      index1 = 0;
    if (index1 >= tableData.gridRowsParams.Count)
      return;
    tableData.defaultRowSize = tableData.gridRowsParams[index1].Size;
  }

  private static void ReadDisabledHeaders(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TableData tableData = (TableData) docNode;
    if (tableData.disabledHeaders == null)
      tableData.disabledHeaders = new List<string>();
    else
      tableData.disabledHeaders.Clear();
    WriteReadXmlHelper.ReadStringListFromXml(tableData.disabledHeaders, readArgs);
    if (!tableData.disabledHeaders.IsEmpty<string>())
      return;
    tableData.disabledHeaders = (List<string>) null;
  }

  private static void ReadColumns(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TableData tableData = (TableData) docNode;
    int capacity = -1;
    if (readArgs.Reader.HasAttributes)
    {
      readArgs.Reader.MoveToAttribute("length");
      capacity = Convert.ToInt32(readArgs.Reader.Value);
      readArgs.Reader.MoveToElement();
    }
    List<RowColParams> rowColParamsList = capacity > 0 ? new List<RowColParams>(capacity) : new List<RowColParams>();
    WriteReadXmlHelper.ReadListFromXml((IList) rowColParamsList, typeof (RowColParams), readArgs);
    tableData.SetGridColumnsParams(rowColParamsList, true, false);
    if (tableData.gridColumnsParams == null)
      return;
    for (int index = 0; index < tableData.gridColumnsParams.Count; ++index)
    {
      if (tableData.gridColumnsParams[index].ID == RowColParams.EmptyIDValue)
        tableData.gridColumnsParams[index].ID = TableData.GenerateGridID(tableData.gridColumnsParams, index);
    }
  }

  private static void ReadIsColumn(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).isColumn = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadIsArea(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).isFixedStructureArea = readArgs.Reader.Value == "1";
  }

  private static void ReadShowSingleCellInTemplate(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TableData) docNode).ShowSingleCellInTemplate = new bool?(readArgs.Reader.Value == "1");
  }

  private static void ReadIsRow(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 23)
      ((TableData) docNode).isColumn = !bool.Parse(readArgs.Reader.Value);
    else
      ((TableData) docNode).isColumn = readArgs.Reader.Value != "1";
  }

  private static void ReadReference(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TableData tableData = (TableData) docNode;
    tableData.reference = ReferenceBase.LoadFromXml(readArgs);
    tableData.reference.AssignOwnerNode((DocumentTreeNode) tableData);
  }

  /// <summary>Узел документа является строкой спецификации</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа является строкой спецификации</returns>
  private static bool IsAVSRowDocNode(DocumentTreeNode docNode)
  {
    if (docNode == null)
      return false;
    return docNode.GetAttributeValue("AVSNodeType", false) == "DocRow" || docNode.Id == "Строка спецификации";
  }

  /// <summary>Инициализация собственных свойств, перед загрузкой ячеек из XML</summary>
  private TableData.ReadCellContext InitPropertiesBeforeReadCells(XmlReadArgs readArgs)
  {
    this.InitNodes();
    if (readArgs.Version >= 22)
    {
      float height = this.properBounds.Height;
      this.ApplyTemplateProperties(this.Template, false, false, true);
      if (readArgs.Version > 26 && (double) height != (double) RectangleElement.EmptyFloatValue)
        this.properBounds.Height = height;
    }
    TableData.ReadCellContext readCellContext = new TableData.ReadCellContext();
    readCellContext.gridIndex = readArgs.GridCellIndex;
    readCellContext.parentCell = this.ParentCell;
    readCellContext.isAVSDocRowTemplate = this.IsTemplate && TableData.IsAVSRowDocNode((DocumentTreeNode) this);
    readCellContext.maxMinHeight = this.minHeight;
    if (readArgs.Version < 36)
      readCellContext.maxMinHeight = 0.0f;
    if ((double) this.defaultRowSize != 0.0 && (double) readCellContext.maxMinHeight < (double) this.defaultRowSize)
      readCellContext.maxMinHeight = this.defaultRowSize;
    if (readCellContext.parentCell != null && readCellContext.parentCell.isColumn && this.NonSkipBeforeAtStartPage && !readCellContext.parentCell.isFixedStructureArea && (double) this.SkipCellsBefore != 0.0 && this.IsFirstCellOnPage)
      this.overrideFlags3 |= OverrideFlags3.IgnoreSkipBefore;
    TableData template = this.Template as TableData;
    if (this.TopLevelTable.PrevTable != null)
    {
      if (this.gridColumnsParams == null)
        this.GetGridColumnsParams(true);
    }
    else if (readArgs.Version >= 23 && (this.overrideFlags2 & OverrideFlags2.ParentGrid) != OverrideFlags2.None && this.gridColumnsParams == null && template != null && template.gridColumnsParams != null)
      this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(template.gridColumnsParams), false, false);
    if (this.gridColumnsParams == null)
      this.GetGridColumnsParams(false);
    if (template != null && (this.overrideFlags & OverrideFlags.DefaultRowSize) == OverrideFlags.None)
      this.defaultRowSize = template.defaultRowSize;
    if (readCellContext.parentCell != null && !readCellContext.parentCell.IsFixedStructureArea && readCellContext.parentCell.isColumn)
    {
      this.setBounds(BoundsHelper.SetWidth(this.bounds, readCellContext.parentCell.properBounds.Width));
      this.properBounds.Width = readCellContext.parentCell.properBounds.Width;
      if (this.IsFixedSizeRows && (double) this.defaultRowSize != 0.0)
      {
        this.setBounds(BoundsHelper.SetHeight(this.bounds, readCellContext.maxMinHeight));
        this.properBounds.Height = readCellContext.maxMinHeight;
      }
    }
    if ((double) this.defaultRowSize > 0.0 && (double) this.properBounds.Height == 0.0)
      this.properBounds.Height = readCellContext.maxMinHeight;
    int num1 = 1;
    if (readCellContext.parentCell != null)
    {
      if (readCellContext.parentCell.IsRow)
      {
        if (!this.IsDefaultGridPos)
          num1 = this.GridPos.SpanCount;
        if ((this.overrideFlags & OverrideFlags.Width) == OverrideFlags.None && (this.overrideFlags2 & OverrideFlags2.ColumnWidth) == OverrideFlags2.None && readCellContext.parentCell.gridColumnsParams != null && num1 > 0)
        {
          int index1 = this.Index;
          float num2 = 0.0f;
          for (int index2 = index1; index2 >= 0; --index2)
          {
            if (readCellContext.parentCell.nodes[index2] is RectangleElement node)
            {
              if (!node.IsDefaultGridPos && node.GridPos.SpanCount <= 0)
                num2 += this.prevCell.bounds.Width;
              else
                break;
            }
          }
          float num3 = 0.0f;
          for (int index3 = 0; index3 < num1 && readCellContext.gridIndex + index3 < readCellContext.parentCell.gridColumnsParams.Count; ++index3)
            num3 += readCellContext.parentCell.gridColumnsParams[readCellContext.gridIndex + index3].Size;
          this.properBounds.Width = num3 - num2;
          if ((double) this.properBounds.Width < 0.0)
            this.properBounds.Width = 0.0f;
        }
      }
      else if (!readCellContext.parentCell.isFixedStructureArea)
        this.properBounds.Width = readCellContext.parentCell.properBounds.Width;
    }
    if ((double) this.skipCellsBefore != 0.0 && readCellContext.parentCell != null && !readCellContext.parentCell.isFixedStructureArea)
    {
      this.properBounds.Location = this.CalcProperLocation(this.bounds.Location);
      this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
      readCellContext.properBounds_Real = this.properBounds;
    }
    else if (readCellContext.parentCell != null && readCellContext.parentCell.isFixedStructureArea)
    {
      if (this.properBounds.Location == RectangleElement.EmptyPointF)
        this.properBounds.Location = PointF.Empty;
      this.setBounds(new RectangleF(readCellContext.parentCell.properBounds.X + this.properBounds.X, readCellContext.parentCell.properBounds.Y + this.properBounds.Y, this.properBounds.Width, this.properBounds.Height));
      readCellContext.properBounds_Real = this.bounds;
    }
    else
    {
      this.properBounds.Location = this.bounds.Location;
      this.setBounds(BoundsHelper.SetSize(this.bounds, new SizeF(this.properBounds.Width, this.properBounds.Height)));
      readCellContext.properBounds_Real = this.properBounds;
    }
    readCellContext.currCellLocation = readCellContext.properBounds_Real.Location;
    readCellContext.prevBounds = new RectangleF(readCellContext.currCellLocation, new SizeF(0.0f, 0.0f));
    int num4 = this.IsTopLevelTable ? 1 : 0;
    return readCellContext;
  }

  /// <summary>Преобразовать XML имя элемента документа для поддержки документов до 13 версии</summary>
  /// <param name="fileVersion">Версия файла</param>
  /// <param name="xmlElementName">Имя элемента документа</param>
  /// <param name="childIsColumn">Если возвращает не null значение, значит нужно установить флаг IsColumn.
  /// Если возвращает null, то игнорировать</param>
  /// <returns></returns>
  private string ConvertOldXmlElementName(
    int fileVersion,
    string xmlElementName,
    out bool? childIsColumn)
  {
    childIsColumn = new bool?();
    string str = xmlElementName;
    if (fileVersion < 10)
    {
      childIsColumn = new bool?(xmlElementName != "TableRow");
      if (xmlElementName == "TableColumn" || xmlElementName == "TableRow")
        str = !(this.GetType().Namespace == "Intermech.Interfaces.Document") ? "TableElement" : nameof (TableData);
    }
    else if (fileVersion < 13)
    {
      switch (xmlElementName)
      {
        case "TextData":
          str = "OldTextData";
          break;
        case nameof (TableData):
          str = "OldTableData";
          break;
      }
    }
    return str;
  }

  /// <summary>Преобразовать имя XML элемента в имя класса для загрузки на серверной части</summary>
  /// <param name="xmlElementName">Имя элемента документа</param>
  /// <returns></returns>
  private string ConvertXmlElementNameToDataClassName(string xmlElementName)
  {
    string dataClassName = xmlElementName;
    switch (xmlElementName)
    {
      case "TableElement":
        dataClassName = nameof (TableData);
        break;
      case "TextBoxElement":
        dataClassName = "TextData";
        break;
      case "LabelElement":
        dataClassName = "TextData";
        break;
      case "ContainerElement":
        dataClassName = "ContainerData";
        break;
      case "Page":
        dataClassName = "PageData";
        break;
      case "Polyline":
        dataClassName = "PolylineData";
        break;
    }
    return dataClassName;
  }

  /// <summary>Создать экземпляр ячейки таблицы, по имени текущего элемента XML</summary>
  /// <param name="readArgs"></param>
  /// <returns></returns>
  private DocumentTreeNode CreateChildCellFromXmlTypeName(XmlReadArgs readArgs)
  {
    string xmlElementName = readArgs.Reader.LocalName;
    string typeName = xmlElementName;
    bool? childIsColumn = new bool?();
    if (readArgs.Version < 13)
      xmlElementName = this.ConvertOldXmlElementName(readArgs.Version, xmlElementName, out childIsColumn);
    if (readArgs.DataOnly)
      typeName = this.ConvertXmlElementNameToDataClassName(xmlElementName);
    DocumentTreeNode nodeFromXmlTypeName = DocumentTreeNode.CreateNodeFromXmlTypeName(typeName);
    if (readArgs.DataOnly && xmlElementName != typeName)
      nodeFromXmlTypeName.AddUnknownXmlAttribute("type", xmlElementName);
    if (childIsColumn.HasValue && nodeFromXmlTypeName is TableData tableData)
      tableData.isColumn = childIsColumn.Value;
    return nodeFromXmlTypeName;
  }

  /// <summary>Загрузить узел не являющийся RectangleElement (не ячейка таблицы)</summary>
  /// <param name="readArgs"></param>
  /// <param name="childNode"></param>
  private void ReadNonRectangleChildNode(XmlReadArgs readArgs, DocumentTreeNode childNode)
  {
    childNode.AssignNeedUpdateLayoutFlag(true);
    childNode.suspendUpdateLayoutCount = this.suspendUpdateLayoutCount;
    this.nodes.AddInternal(childNode);
    if (this.idService != null && childNode.idService != this.idService)
      childNode.idService = this.idService;
    childNode.AssignParent((DocumentTreeNode) this, false, false, true);
    childNode.ReadFromXml(readArgs);
    childNode.ApplyTemplateProperties(childNode.Template, false, false, true);
  }

  private void ReadChildCell(
    XmlReadArgs readArgs,
    RectangleElement cell,
    TableData.ReadCellContext readContext)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    readArgs.GridCellIndex = readContext.prevColumnIndex + 1;
    cell.suspendUpdateLayoutCount = this.suspendUpdateLayoutCount;
    cell.setBounds(BoundsHelper.SetLocation(cell.bounds, readContext.currCellLocation));
    cell.defaultRowSize = this.defaultRowSize;
    TableData tableData1 = cell as TableData;
    if (!this.IsFixedStructureArea)
    {
      if (this.isColumn)
      {
        cell.setBounds(BoundsHelper.SetWidth(cell.bounds, readContext.properBounds_Real.Width));
        cell.properBounds.Width = readContext.properBounds_Real.Width;
        if ((double) this.defaultRowSize != 0.0)
        {
          cell.setBounds(BoundsHelper.SetHeight(cell.bounds, readContext.maxMinHeight));
          cell.properBounds.Height = readContext.maxMinHeight;
        }
      }
      else if (readArgs.Version < 27 && (double) this.defaultRowSize != 0.0)
      {
        cell.setBounds(BoundsHelper.SetHeight(cell.bounds, readContext.maxMinHeight));
        cell.properBounds.Height = readContext.maxMinHeight;
      }
      else
      {
        cell.setBounds(BoundsHelper.SetHeight(cell.bounds, readContext.properBounds_Real.Height));
        cell.properBounds.Height = readContext.properBounds_Real.Height;
      }
    }
    this.nodes.AddInternal((DocumentTreeNode) cell);
    if (this.idService != null && cell.idService != this.idService)
      cell.idService = this.idService;
    cell.AssignParent((DocumentTreeNode) this, false, false, true);
    if (readContext.isAVSDocRowTemplate && readArgs.Version < 34 && cell is TextData textData)
      textData.AssignReplaceAVSMaterial(true, true);
    if (this.isColumn && readContext.prevChildCell != null && (double) readContext.prevChildCell.SkipCellsAfter != 0.0 && !this.IsFixedStructureArea)
    {
      readContext.prevChildCell.UpdateBoundsSkipAfter();
      readContext.prevBounds.Size = readContext.prevChildCell.bounds.Size;
      readContext.currCellLocation.Y = readContext.prevBounds.Bottom;
      cell.setBounds(BoundsHelper.SetLocation(cell.bounds, readContext.currCellLocation));
      cell.SetNeedUpdateUIGeometry(true, false);
      readContext.prevChildCell.SetNeedUpdateUIGeometry(true, false);
    }
    if (readContext.prevChildCell != null && (double) readContext.prevChildCell.SkipCellsAfter != 0.0)
    {
      readContext.prevChildCell.UpdateBoundsSkipAfter();
      readContext.prevBounds.Size = readContext.prevChildCell.bounds.Size;
      readContext.currCellLocation.Y = readContext.prevBounds.Bottom;
      cell.setBounds(new RectangleF(readContext.currCellLocation, cell.bounds.Size));
      cell.SetNeedUpdateUIGeometry(true, false);
    }
    cell.ReadFromXml(readArgs);
    if (!(cell is TableData) || readArgs.Version < 22)
    {
      cell.ApplyTemplateProperties(cell.Template, false, false, true);
    }
    else
    {
      RectangleElement template = cell.Template as RectangleElement;
      if (tableData1 != null)
      {
        if (template is TableData tableData2 && (tableData1.overrideFlags2 & OverrideFlags2.ParentGrid) != OverrideFlags2.None && tableData1.gridColumnsParams == null && tableData2.gridColumnsParams != null)
          tableData1.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(tableData2.gridColumnsParams), false, false);
        if (template != null)
        {
          if ((tableData1.overrideFlags & OverrideFlags.DefaultRowSize) == OverrideFlags.None)
            tableData1.defaultRowSize = template.defaultRowSize;
          if ((this.overrideFlags & OverrideFlags.MinHeight) == OverrideFlags.None)
            tableData1.minHeight = template.MinHeight;
          if ((this.overrideFlags & OverrideFlags.MinWidth) == OverrideFlags.None)
            tableData1.minWidth = template.MinWidth;
          if (this.IsFixedStructureArea)
          {
            if ((double) cell.properBounds.X == (double) RectangleElement.EmptyFloatValue)
              cell.properBounds.X = template.properBounds.X;
            if ((double) cell.properBounds.Y == (double) RectangleElement.EmptyFloatValue)
              cell.properBounds.Y = template.properBounds.Y;
          }
        }
      }
    }
    TableData.ReadCellContext readCellContext = readContext;
    readCellContext.notNeedUpdate_IsSelectedDataCellTemplate = ((readCellContext.notNeedUpdate_IsSelectedDataCellTemplate ? 1 : 0) | (cell.Visible ? 0 : (cell.CanSwitchVisibleThisDataCellInTemplate ? 1 : 0))) != 0;
    if ((double) cell.relativeHeight > 0.0)
      cell.properBounds.Height = (float) ((double) this.properBounds.Height * (double) this.relativeHeight / 100.0);
    if ((double) cell.relativeWidth > 0.0)
      cell.properBounds.Width = (float) ((double) this.properBounds.Width * (double) cell.relativeWidth / 100.0);
    if (!this.isColumn && !this.isFixedStructureArea)
    {
      int num1 = 1;
      if (!cell.IsDefaultGridPos)
        num1 = cell.GridPos.SpanCount;
      if ((cell.overrideFlags & OverrideFlags.Width) == OverrideFlags.None && (cell.overrideFlags2 & OverrideFlags2.ColumnWidth) == OverrideFlags2.None)
      {
        if (this.gridColumnsParams != null)
        {
          if (num1 > 0)
          {
            int num2 = this.nodes.Count - 1;
            float num3 = 0.0f;
            for (int index = num2 - 1; index >= 0; --index)
            {
              if (this.nodes[index] is RectangleElement node)
              {
                if (!node.IsDefaultGridPos && node.GridPos.SpanCount <= 0 && !node.WidthOverrided)
                  num3 += node.bounds.Width;
                else
                  break;
              }
            }
            float num4 = 0.0f;
            for (int index = 1; index <= num1 && readContext.prevColumnIndex + index < this.gridColumnsParams.Count; ++index)
              num4 += this.gridColumnsParams[readContext.prevColumnIndex + index].Size;
            cell.properBounds.Width = num4 - num3;
            if ((double) cell.properBounds.Width < 0.0)
              cell.properBounds.Width = 0.0f;
            if (tableData1 != null && tableData1.IsSingleCell && cell.Template is RectangleElement template && (double) cell.properBounds.Width != (double) template.properBounds.Width)
              cell.properBounds.Width = template.properBounds.Width;
          }
          if (cell.IsSingleCell && readContext.prevColumnIndex != -1)
          {
            if ((cell.overrideFlags2 & OverrideFlags2.ColumnLeftBorder) == OverrideFlags2.None && (cell.overrideFlags & OverrideFlags.LeftBorder) == OverrideFlags.None && readContext.prevColumnIndex + 1 < this.gridColumnsParams.Count && this.gridColumnsParams[readContext.prevColumnIndex + 1] != null && this.gridColumnsParams[readContext.prevColumnIndex + 1].BorderLine1 != null)
            {
              if (cell.borders == null)
                cell.borders = (RectangleBorder) new CustomBorder();
              cell.borders.Left = this.gridColumnsParams[readContext.prevColumnIndex + 1].BorderLine1;
            }
            if ((cell.overrideFlags2 & OverrideFlags2.ColumnRightBorder) == OverrideFlags2.None && (cell.overrideFlags & OverrideFlags.RightBorder) == OverrideFlags.None && readContext.prevColumnIndex + num1 < this.gridColumnsParams.Count && this.gridColumnsParams[readContext.prevColumnIndex + num1] != null && this.gridColumnsParams[readContext.prevColumnIndex + num1].BorderLine2 != null)
            {
              if (cell.borders == null)
                cell.borders = (RectangleBorder) new CustomBorder();
              cell.borders.Right = this.gridColumnsParams[readContext.prevColumnIndex + num1].BorderLine2;
            }
          }
        }
        else
          cell.SetNeedUpdateLayoutFlag(true, true, false, false);
      }
      else if ((cell.overrideFlags & OverrideFlags.Width) == OverrideFlags.None && (cell.overrideFlags2 & OverrideFlags2.ColumnWidth) != OverrideFlags2.None && cell.Template is RectangleElement template1)
        cell.properBounds.Width = template1.properBounds.Width;
      if (cell.IsSingleCell)
      {
        if ((double) cell.properBounds.Height < (double) cell.ContentHeight)
          cell.properBounds.Height = cell.ContentHeight;
        if ((double) readContext.maxMinHeight < (double) cell.ContentHeight)
        {
          readContext.maxMinHeight = cell.ContentHeight;
          if ((double) this.defaultRowSize != 0.0)
            readContext.maxMinHeight = this.RoundForFixedSizeRow(readContext.maxMinHeight, this.defaultRowSize, readContext.maxMinHeight);
        }
        if ((double) cell.properBounds.Height < (double) readContext.maxMinHeight)
          cell.properBounds.Height = readContext.maxMinHeight;
        if ((double) cell.defaultRowSize != 0.0)
          cell.properBounds.Height = this.RoundForFixedSizeRow(cell.properBounds.Height, cell.defaultRowSize, readContext.maxMinHeight);
      }
      readContext.prevColumnIndex += num1;
      if (this.TableCellType == CellType.DataCell && cell.TableCellType != CellType.DataCell)
        cell.SetTableCellType(CellType.DataCell, false, false);
    }
    if (!this.isFixedStructureArea && this.isColumn && (double) this.defaultRowSize != 0.0 && !cell.HeightOverrided && cell.IsSingleCell)
      cell.properBounds.Height = readContext.maxMinHeight;
    if ((double) cell.SkipCellsAfter != 0.0 || (double) cell.SkipCellsBefore != 0.0)
    {
      PointF newLocation = cell.CalcProperLocation(cell.bounds.Location);
      if (newLocation != cell.properBounds.Location)
      {
        if (tableData1 != null)
          tableData1.RecalcCellLocations(newLocation, 0, tableData1.nodes.Count, false, false, false);
        else
          cell.properBounds.Location = newLocation;
        cell.setBounds(new RectangleF(cell.bounds.Location, cell.CalcSizeFromProper(cell.properBounds.Size)));
      }
    }
    else if (this.isFixedStructureArea)
    {
      cell.setBounds(new RectangleF(new PointF(readContext.properBounds_Real.X + cell.properBounds.X, readContext.properBounds_Real.Y + cell.properBounds.Y), cell.properBounds.Size));
    }
    else
    {
      cell.properBounds.Location = cell.bounds.Location;
      cell.setBounds(new RectangleF(cell.bounds.Location, cell.properBounds.Size));
    }
    if (readArgs.Version < 27)
    {
      if (this.IsFixedStructureArea)
      {
        cell.setBounds(new RectangleF(readContext.properBounds_Real.X + cell.properBounds.X, readContext.properBounds_Real.Y + cell.properBounds.Y, cell.properBounds.Width, cell.properBounds.Height));
        cell.setBounds(cell.SetCellSizes(cell.bounds, true, false, false, false));
      }
      else
      {
        cell.setBounds(cell.CalcBoundsFromProper(cell.properBounds));
        cell.setBounds(cell.SetCellSizes(cell.bounds, true, false, false, false));
      }
    }
    if (!cell.IsVisibleNow)
      return;
    if (!this.IsFixedStructureArea)
    {
      if ((double) readContext.calculatedTableWidth == 0.0 || (double) readContext.calculatedTableWidth - ((double) cell.bounds.Right - (double) this.properBounds.X) < -9.9999997473787516E-06)
        readContext.calculatedTableWidth = (float) Math.Round((double) cell.bounds.Right - (double) readContext.properBounds_Real.X, 5);
      if ((double) readContext.calculatedTableHeight == 0.0 || (double) readContext.calculatedTableHeight - ((double) cell.bounds.Bottom - (double) this.properBounds.Y) < -9.9999997473787516E-06)
        readContext.calculatedTableHeight = (float) Math.Round((double) cell.bounds.Bottom - (double) readContext.properBounds_Real.Y, 5);
    }
    else if ((double) readContext.properBounds_Real.Right - (double) cell.bounds.Right < -9.9999997473787516E-06)
    {
      RectangleF bounds = cell.bounds with
      {
        Width = (float) Math.Round((double) readContext.properBounds_Real.Right - (double) cell.bounds.X, 5)
      };
      cell.SetCellSizes(bounds, true, false, false, false);
    }
    readContext.prevChildCell = cell;
    readContext.prevChildCell.GetCellBounds(readContext.prevChildCell.Template as RectangleElement, true, false);
    readContext.prevBounds.Size = readContext.prevChildCell.bounds.Size;
    readContext.prevBounds.Location = readContext.currCellLocation;
    if (this.isColumn)
      readContext.currCellLocation.Y = readContext.prevBounds.Bottom;
    else
      readContext.currCellLocation.X = readContext.prevBounds.Right;
  }

  /// <summary>Обработка данных после загрузки всех ячеек</summary>
  /// <param name="readArgs"></param>
  /// <param name="readContext"></param>
  private void CalculatePropertiesAfterReadCells(
    XmlReadArgs readArgs,
    TableData.ReadCellContext readContext)
  {
    if (readContext.notNeedUpdate_IsSelectedDataCellTemplate)
    {
      foreach (RectangleElement rectangleElement in this.CellsEnumerator)
      {
        if (rectangleElement.CanSwitchVisibleThisDataCellInTemplate && !rectangleElement.Visible)
        {
          rectangleElement.AssignIsSelectedDataCellTemplate(false);
          rectangleElement.AssingVisible(true);
          rectangleElement.ResetOverrideFlags3(OverrideFlags3.Visible);
        }
      }
    }
    if (!this.isColumn && !this.isFixedStructureArea && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && this.nodes.Count > 0 && readContext.LastCell != null)
    {
      RectangleF bounds = readContext.LastCell.bounds with
      {
        Width = readContext.properBounds_Real.Right - readContext.LastCell.bounds.X
      };
      if ((double) bounds.Width != (double) readContext.LastCell.bounds.Width)
      {
        readContext.LastCell.SetCellSizes(bounds, true, false, false, false);
        if ((double) readContext.calculatedTableWidth == 0.0 || (double) Math.Abs(readContext.calculatedTableWidth - (readContext.LastCell.bounds.Right - this.properBounds.X)) > 9.9999997473787516E-06)
          readContext.calculatedTableWidth = (float) Math.Round((double) readContext.LastCell.bounds.Right - (double) readContext.properBounds_Real.X, 5);
      }
    }
    if (this.isColumn && !this.isFixedStructureArea && this.nodes.Count > 0 && this.nodes[this.nodes.Count - 1] is RectangleElement node)
      node.UpdateBoundsSkipAfter();
    if (this.PrevCell != null && this.IsTopLevelTable)
      this.PrevCell.UpdateBoundsSkipAfter();
    if ((double) readContext.calculatedTableWidth != 0.0 && (!this.IsTopLevelTable || (double) this.properBounds.Width == (double) RectangleElement.EmptyFloatValue))
      this.properBounds.Width = readContext.calculatedTableWidth;
    if ((double) readContext.calculatedTableHeight != 0.0 && (this.isColumn || !this.IsFixedStructureArea))
    {
      if ((double) readContext.calculatedTableHeight < (double) readContext.maxMinHeight)
        readContext.calculatedTableHeight = readContext.maxMinHeight;
      if (this.IsTopLevelTable && (double) this.maxHeight != 0.0 && this.IsPageFlow)
      {
        if ((double) readContext.calculatedTableHeight > (double) this.maxHeight && !this.IsTemplate)
          this.SetNeedUpdateLayoutFlag(true, false, false, false);
        readContext.calculatedTableHeight = this.maxHeight;
      }
      this.properBounds.Height = readContext.calculatedTableHeight;
    }
    if ((double) this.skipCellsBefore != 0.0 && readContext.parentCell != null && !readContext.parentCell.IsFixedStructureArea)
      this.setBounds(new RectangleF(this.bounds.Location, this.CalcSizeFromProper(this.properBounds.Size)));
    else if (readContext.parentCell != null && readContext.parentCell.isFixedStructureArea)
      this.setBounds(new RectangleF(readContext.parentCell.properBounds.X + this.properBounds.X, readContext.parentCell.properBounds.Y + this.properBounds.Y, this.properBounds.Width, this.properBounds.Height));
    else
      this.setBounds(new RectangleF(this.bounds.Location, new SizeF(this.properBounds.Width, this.properBounds.Height)));
    if (!this.isColumn && readContext.LastCell != null && !this.isFixedStructureArea && (double) Math.Abs(readContext.LastCell.bounds.Right - this.bounds.Right) > 9.9999997473787516E-06)
    {
      RectangleF bounds = readContext.LastCell.bounds with
      {
        Width = (float) Math.Round((double) this.bounds.Right - (double) readContext.LastCell.bounds.X, 5)
      };
      readContext.LastCell.SetCellSizes(bounds, true, false, false, false);
    }
    bool flag = false;
    float num1 = float.MinValue;
    float num2 = float.MinValue;
    RectangleF properBounds = this.properBounds;
    foreach (RectangleElement rectangleElement in this.CellsEnumerator)
    {
      if (rectangleElement.IsVisibleNow)
      {
        RectangleF bounds = rectangleElement.Bounds;
        if ((double) bounds.Right > (double) num1 + 9.9999997473787516E-06)
        {
          num1 = (float) Math.Round((double) bounds.Right, 5);
          flag |= this.isColumn;
        }
        if ((double) bounds.Bottom > (double) num2 + 9.9999997473787516E-06)
        {
          num2 = (float) Math.Round((double) bounds.Bottom, 5);
          flag |= !this.isColumn;
        }
      }
    }
    if ((double) Math.Abs(num1 - properBounds.X - properBounds.Width) < -9.9999997473787516E-06)
      num1 = properBounds.Right;
    if ((double) Math.Abs(this.maxHeight - properBounds.Y - properBounds.Height) < -9.9999997473787516E-06)
      num2 = properBounds.Bottom;
    if (!this.isColumn && (double) this.maxHeight - (double) properBounds.Y == 0.0)
    {
      flag = true;
      num2 = properBounds.Bottom;
    }
    if (!this.isColumn && this.IsFixedStructureArea)
      num1 = properBounds.Right;
    properBounds.Size = new SizeF(num1 - properBounds.X, num2 - properBounds.Y);
    if (readArgs.Version < 36 && (double) properBounds.Height < (double) this.MinHeight)
      this.MinHeight = 0.0f;
    if (flag && !this.IsColumn)
    {
      if (this.IsTopLevelTable && (double) this.maxHeight != 0.0 && this.IsPageFlow)
        properBounds.Height = this.maxHeight;
      if (readContext.parentCell != null && readContext.parentCell.isFixedStructureArea)
        properBounds.Location = this.properBounds.Location;
      this.AssignProperBounds(properBounds, false, false, false);
      this.SetCellSizes(this.CalcBoundsFromProper(properBounds), true, false, false, false, false);
    }
    readArgs.GridCellIndex = readContext.gridIndex;
  }

  /// <summary>Загрузка ячеек из элементов XML</summary>
  /// <param name="readArgs"></param>
  /// <param name="readContext"></param>
  private void ReadChildCellsFromXMLElements(
    XmlReadArgs readArgs,
    TableData.ReadCellContext readContext)
  {
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    readContext.notNeedUpdate_IsSelectedDataCellTemplate = !this.CanSwitchInternalCellsVisibity || readArgs.Version >= 38;
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          DocumentTreeNode cellFromXmlTypeName = this.CreateChildCellFromXmlTypeName(readArgs);
          if (cellFromXmlTypeName is RectangleElement cell)
          {
            this.ReadChildCell(readArgs, cell, readContext);
            continue;
          }
          if (cellFromXmlTypeName != null)
          {
            this.ReadNonRectangleChildNode(readArgs, cellFromXmlTypeName);
            continue;
          }
          LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_126"), (object) this.GetType().Namespace, (object) readArgs.Reader.LocalName));
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  private void ReadCells(XmlReadArgs readArgs)
  {
    TableData.ReadCellContext readContext = this.InitPropertiesBeforeReadCells(readArgs);
    this.ReadChildCellsFromXMLElements(readArgs, readContext);
    this.CalculatePropertiesAfterReadCells(readArgs, readContext);
  }

  private new static void ReadNodes(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (docNode is TableData tableData)
      tableData.ReadCells(readArgs);
    else
      DocumentTreeNode.ReadNodes(docNode, readArgs);
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXmlOldFormats_After(XmlReadArgs readArgs)
  {
    base.ReadFromXmlOldFormats_After(readArgs);
    if (this.bounds.Location == RectangleElement.EmptyPointF)
      this.AssignNeedUpdateLayoutFlag(true);
    if (readArgs.Version < 9 && this.gridColumnsParams != null && this.IsColumnGridOwner())
    {
      for (int index = 0; index < this.gridColumnsParams.Count; ++index)
      {
        if (this.gridColumnsParams[index] != null && this.TemplateId != null && this.gridColumnsParams[index].TemplateID == RowColParams.EmptyIDValue)
          this.gridColumnsParams[index].TemplateID = index;
      }
    }
    if (readArgs.Version >= 10 || this.gridColumnsParams == null || !this.IsColumnGridOwner())
      return;
    for (int index = 0; index < this.gridColumnsParams.Count; ++index)
    {
      if (this.gridColumnsParams[index] != null)
        this.gridColumnsParams[index].CorrectColumnBorderLine1();
    }
  }

  /// <summary>Только для старых версий документа (меньше 17).
  /// Запускается после загрузки узла из XML и добавления в родительский узел</summary>
  protected override void ReadNodeFromXmlPostProcess(XmlReadArgs readArgs)
  {
    base.ReadNodeFromXmlPostProcess(readArgs);
    if (readArgs.Version >= 17 || (double) this.defaultRowSize != 0.0)
      return;
    int gridRowIndex = this.IsTopLevelTable ? 0 : this.GetGridRowIndex();
    if (gridRowIndex <= 0)
      return;
    List<RowColParams> gridRowsParams = this.GridRowsParams;
    if (gridRowsParams == null || gridRowsParams.Count <= 0 || gridRowIndex >= gridRowsParams.Count)
      return;
    this.defaultRowSize = gridRowsParams[gridRowIndex].Size;
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback.</summary>
  public override void OnDeserialization(object sender)
  {
    base.OnDeserialization(sender);
    if (this.bounds.Location == RectangleElement.EmptyPointF)
      this.AssignNeedUpdateLayoutFlag(true);
    if (this.nextFlowElement != null)
      this.nextFlowElement.PrevFlowElement = (IFlowElement) this;
    if (this.reference != null)
    {
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
      if (this.reference is ReferenceToNode reference)
        reference.UpdateLink(false, false);
    }
    if (this.ParentCell != null || !this.IsPageFlow || this.FlowID != null || this.OwnerDocument == null || this.OwnerDocument.DocumentFlows.Count <= 0)
      return;
    this.flowID = this.OwnerDocument.DocumentFlows[0];
  }

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.InitNodes();
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  private void InitNodes()
  {
    if (this.nodes != null)
      return;
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected TableData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
    this.ResetNeedUpdateLayoutFlag(true);
  }

  /// <summary>Конструктор</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  public TableData(bool isColumn, DocumentTreeNode parent, RectangleF bounds, bool visible)
    : this(parent, bounds, visible)
  {
    this.isColumn = isColumn;
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  public TableData(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
    this.AssignNeedUpdateLayoutFlag(false);
  }

  /// <summary>Конструктор</summary>
  public TableData()
  {
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
    this.AssignNeedUpdateLayoutFlag(false);
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать инициализацию полей</param>
  public TableData(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых необходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public static object EmptyConstructor() => (object) new TableData();

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new TableData(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать виртуальную таблицу</summary>
  /// <param name="parent">Родительский узел в виртуальном или реальном дереве</param>
  /// <param name="owner">Узел в реальном дереве, дочерние узлы которого представляет этот виртуальный узел</param>
  /// <returns>Виртуальная таблица</returns>
  internal static TableData CreateVirtualTable(DocumentTreeNode parent, DocumentTreeNode owner)
  {
    TableData virtualTable = new TableData(true);
    virtualTable.SetIsVirtualNode(true);
    virtualTable.SetOwner(owner);
    virtualTable.SetParent(parent, false, false);
    return virtualTable;
  }

  static TableData() => TableData.InitReadFieldDict();

  [Category("Debug")]
  public object Tag
  {
    [DebuggerStepThrough] get => this.tag;
    set => this.tag = value;
  }

  /// <summary>Найти позицию ячейки данных по сквозному индексу в потоке.
  /// Ищет начиная с этой таблицы, даже если эта таблица - продолжение!</summary>
  /// <param name="dataPosition">Сквозной индекс ячейки данных</param>
  /// <param name="dataOwner">Родитель для ячейки данных</param>
  /// <returns>Индекс в родительской таблице. Если индекс не найден, то вернет -1</returns>
  public int FindDataPositionInFlow(int dataPosition, out TableData dataOwner)
  {
    dataOwner = this;
    int num1 = 0;
    while (dataOwner != null)
    {
      int num2 = dataOwner.CalcFirstHeaderCount();
      int index = num2;
      int num3 = num1 + (dataOwner.nodes.Count - num2);
      if (index < dataOwner.NodesCount && dataOwner.prevCell != null && (dataOwner.nodes[index] as TableData).prevCell != null)
      {
        --num3;
        ++index;
      }
      if (num3 >= dataPosition)
      {
        int dataPositionInFlow = index + (dataPosition - num1);
        if (dataPositionInFlow < dataOwner.nodes.Count || dataPositionInFlow <= 0)
          return dataPositionInFlow;
        if (dataOwner.nodes[dataPositionInFlow - 1] is RectangleElement node && node.NextCell != null)
        {
          RectangleElement lastCell = node.FindLastCell();
          dataOwner = lastCell.ParentCell;
          dataPositionInFlow = lastCell.Index + 1;
        }
        if (dataPositionInFlow < dataOwner.nodes.Count && dataPositionInFlow > 0 || dataOwner.nextCell == null)
          return dataPositionInFlow;
      }
      num1 = num3;
      if (dataOwner.NextTable == null && num1 < dataPosition)
        return dataOwner.nodes.Count;
      dataOwner = dataOwner.NextTable;
    }
    return -1;
  }

  /// <summary>Найти первую ячейку/строку данных в потоке</summary>
  /// <returns></returns>
  public RectangleElement FindFirstCellInDataFlow()
  {
    TableData dataOwner;
    int dataPositionInFlow = this.FindDataPositionInFlow(0, out dataOwner);
    return dataPositionInFlow != -1 ? dataOwner?.Nodes[dataPositionInFlow] as RectangleElement : (RectangleElement) null;
  }

  /// <summary>Найти ячейку данных начиная с заданной позиции.
  /// Используется только после FindDataPositionInFlow для поиска в цепочке пустых таблиц,
  /// когда найдена позиция для вставки данных, но не найдена сама ячейка,
  /// которая может находиться дальше, после удаления без разбивки.
  /// </summary>
  /// <param name="startIndex">Сквозной индекс ячейки данных</param>
  /// <param name="dataOwner">Родитель для ячейки данных</param>
  /// <returns>Индекс в родительской таблице. Если индекс не найден, то вернет -1</returns>
  internal int FindDataCellFromPosition(int startIndex, out TableData dataOwner)
  {
    dataOwner = this;
    int num = startIndex;
    while (dataOwner != null)
    {
      if (dataOwner != this)
        num = 0;
      int dataCellInThisTable = dataOwner.FindNextDataCellInThisTable(num - 1);
      if (dataCellInThisTable != -1)
        return dataCellInThisTable;
      dataOwner = dataOwner.NextTable;
    }
    return -1;
  }

  /// <summary>Найти последний элемент в потоке</summary>
  /// <param name="dataOwner">Владелец последнего элемента</param>
  /// <returns>Индекс последнего элемента</returns>
  public int FindLastDataPositionInFlow(out TableData dataOwner)
  {
    dataOwner = (TableData) this.FindLastCell();
    return dataOwner.Nodes.Count == 0 ? 0 : dataOwner.Nodes.Count - 1;
  }

  /// <summary>Найти последний элемент в потоке</summary>
  /// <param name="dataOwner">Владелец последнего элемента</param>
  /// <returns>Индекс последнего элемента</returns>
  public RectangleElement FindLastDataCellInFlow(out TableData dataOwner)
  {
    dataOwner = (TableData) this.FindLastCell();
    lastDataCellInFlow = (RectangleElement) null;
    while (lastDataCellInFlow == null && dataOwner != null)
    {
      for (int index = dataOwner.nodes.Count - 1; index >= 0 && (!(dataOwner.nodes[index] is RectangleElement lastDataCellInFlow) || lastDataCellInFlow.TableCellType != CellType.DataCell); --index)
        lastDataCellInFlow = (RectangleElement) null;
      if (lastDataCellInFlow == null)
      {
        if (dataOwner.PrevTable != null)
          dataOwner = dataOwner.PrevTable;
        else
          break;
      }
    }
    return lastDataCellInFlow;
  }

  /// <summary>Найти позицию ячейки данных по сквозному индексу в потоке.
  /// Если для следующей за заданной ячейкой нет следующей позиции то возвращается индекс для вставки (он равен Count списка!)</summary>
  /// <param name="prevCellPosition">Индекс в этой таблице предыдущей ячейки данных</param>
  /// <param name="dataOwner">Родитель для ячейки данных</param>
  /// <returns>Индекс в родительской таблице</returns>
  public int FindNextDataPositionInFlow(int prevCellPosition, out TableData dataOwner)
  {
    dataOwner = this;
    int dataPositionInFlow1;
    if (prevCellPosition >= 0 && dataOwner.nodes.Count > 0)
    {
      if (prevCellPosition >= dataOwner.nodes.Count)
        prevCellPosition = dataOwner.nodes.Count - 1;
      if (dataOwner.nodes[prevCellPosition] is RectangleElement node && node.NextCell != null)
      {
        RectangleElement lastCell = node.FindLastCell();
        dataPositionInFlow1 = lastCell.Index + 1;
        if (dataOwner == lastCell.ParentCell && dataPositionInFlow1 + 1 <= prevCellPosition)
          dataPositionInFlow1 = prevCellPosition + 1;
        dataOwner = lastCell.ParentCell;
      }
      else
        dataPositionInFlow1 = prevCellPosition + 1;
    }
    else
      dataPositionInFlow1 = 0;
    if (dataPositionInFlow1 >= dataOwner.nodes.Count)
    {
      if (dataOwner.NextTable != null)
      {
        int dataPositionInFlow2 = dataOwner.NextTable.FindDataPositionInFlow(0, out dataOwner);
        if (dataPositionInFlow2 != -1)
          dataPositionInFlow1 = dataPositionInFlow2;
      }
    }
    return dataPositionInFlow1;
  }

  /// <summary>Найти предыдущую ячейку данных в потоке этой таблицы</summary>
  /// <param name="cellPosition">Индекс в этой таблице текущей ячейки данных</param>
  public RectangleElement FindPrevDataCellInFlow(int cellPosition)
  {
    TableData tableData = this;
    if (cellPosition >= 0 && tableData.NodesCount > 0)
    {
      if (cellPosition >= tableData.nodes.Count)
        cellPosition = tableData.nodes.Count - 1;
      RectangleElement firstCell = (tableData.nodes[cellPosition] as RectangleElement).FindFirstCell();
      tableData = firstCell.ParentCell;
      RectangleElement visibleCellInThisTable = tableData.FindPrevVisibleCellInThisTable(firstCell.Index);
      if (visibleCellInThisTable != null && visibleCellInThisTable.IsDataNode)
        return visibleCellInThisTable;
    }
    while (tableData.PrevTable != null)
    {
      tableData = tableData.PrevTable;
      RectangleElement visibleCellInThisTable = tableData.FindPrevVisibleCellInThisTable(tableData.NodesCount);
      if (visibleCellInThisTable != null && visibleCellInThisTable.IsDataNode)
        return visibleCellInThisTable;
    }
    return (RectangleElement) null;
  }

  /// <summary>Найти следующую ячейку данных в потоке данных внутри этой таблицы</summary>
  /// <param name="cellPosition">Индекс в этой таблице текущей ячейки данных</param>
  public RectangleElement FindNextDataCellInFlow(int cellPosition)
  {
    TableData dataOwner = this;
    int index;
    if (cellPosition >= 0 && dataOwner.nodes.Count > 0)
    {
      if (cellPosition >= dataOwner.nodes.Count)
        cellPosition = dataOwner.nodes.Count - 1;
      if (dataOwner.nodes[cellPosition] is RectangleElement node && node.NextCell != null)
      {
        RectangleElement lastCell = node.FindLastCell();
        index = lastCell.Index + 1;
        if (dataOwner == lastCell.ParentCell && index + 1 <= cellPosition)
          index = cellPosition + 1;
        dataOwner = lastCell.ParentCell;
      }
      else
        index = cellPosition + 1;
    }
    else
      index = 0;
    for (; index >= dataOwner.nodes.Count && dataOwner.NextTable != null; index = dataOwner.FindDataPositionInFlow(0, out dataOwner))
      dataOwner = dataOwner.NextTable;
    return index >= 0 && index < dataOwner.nodes.Count ? dataOwner.Nodes[index] as RectangleElement : (RectangleElement) null;
  }

  /// <summary>Найти следующую видимую ячейку в этой таблице</summary>
  /// <param name="currentCellIndex">Текущая ячейка</param>
  /// <returns></returns>
  public RectangleElement FindNextVisibleCellInThisTable(int currentCellIndex)
  {
    for (int index = currentCellIndex + 1; index < this.Nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        return node;
    }
    return (RectangleElement) null;
  }

  internal RectangleElement FindPrevVisibleCellInThisTable(int currentCellIndex)
  {
    for (int index = currentCellIndex - 1; index >= 0; --index)
    {
      if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        return node;
    }
    return (RectangleElement) null;
  }

  /// <summary>Обновить ссылки на узлы</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateNodeLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (this.reference != null && this.reference is ReferenceToNode)
      this.reference.UpdateLink(updateUI, updateLayout);
    base.UpdateNodeLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Тип ячейки таблицы</summary>
  public override CellType TableCellType
  {
    [DebuggerStepThrough] get => base.TableCellType;
    set
    {
      if (this.TableCellType == value)
        return;
      base.TableCellType = value;
    }
  }

  /// <summary>Ссылка на источник данных таблицы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_464")]
  [CustomDescription("Attribute.Interfaces.Document_465")]
  [CustomCategory("Attribute.Interfaces.Document_466")]
  public virtual ReferenceBase Reference
  {
    [DebuggerStepThrough] get => this.reference;
    set => this.AssignReference(value, true, true);
  }

  /// <summary>Назначить значение свойству Reference</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public virtual void AssignReference(ReferenceBase value, bool updateUI, bool updateLayout)
  {
    if (this.reference == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Reference", (object) this.Reference, (object) value);
    bool flag1 = false;
    bool flag2 = value != null && value.IsDependOnDocument;
    if (this.reference != null)
    {
      flag1 = this.reference.IsDependOnDocument;
      this.reference.DisconnectLink();
      this.reference.AssignOwnerNode((DocumentTreeNode) null);
      if (flag1 != flag2)
        this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    this.reference = value;
    if (this.reference != null)
    {
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
      if (flag1 != flag2 & flag2)
        this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Обработчик события DocumentChanged</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Page_DocumentChanged(object sender, DocumentChanged_EventArgs e)
  {
    if (!this.reference.IsDependOnDocument)
      return;
    this.reference.UpdateLink(false, false);
  }

  /// <summary>Перечислитель для цикла по данным</summary>
  public IEnumerator<RectangleElement> GetDataEnumerator()
  {
    return (IEnumerator<RectangleElement>) new DataNodesEnumerator(this.FindFirstTable());
  }

  /// <summary>Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  public override void ConvertToHeader(bool removeData)
  {
    if (this.TableCellType != CellType.DataCell)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node)
        node.ConvertToHeader(false);
    }
    this.TableCellType = CellType.Header;
  }

  /// <summary>Автоматически подбирать высоту таблицы</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_467")]
  [CustomDescription("Attribute.Interfaces.Document_468")]
  [CustomCategory("Debug")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool AutoSizeHeight
  {
    [DebuggerStepThrough] get => this.autoSizeHeight;
    set
    {
      if (this.autoSizeHeight == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (AutoSizeHeight), (object) this.AutoSizeHeight, (object) value);
      this.autoSizeHeight = value;
      this.SetNeedUpdateLayoutFlag(true, true, true, true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Идентификатор потока</summary>
  [Category("Debug")]
  public virtual FlowID FlowID
  {
    [DebuggerStepThrough] get
    {
      return this.prevCell != null && this.prevCell.Page == this.Page ? (FlowID) null : this.flowID;
    }
  }

  /// <summary>Восстановить идентификаторы потоков</summary>
  public override void RestoreFlowId()
  {
    if (this.flowID != null)
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument != null && !ownerDocument.DocumentFlows.Contains(this.flowID))
      {
        bool flag = false;
        if (this.flowID.TemplateFlowID != null)
        {
          FlowID flowIdFromTemplate = ownerDocument.FindFlowIDFromTemplate(this.flowID.TemplateFlowID);
          if (flowIdFromTemplate != null)
          {
            this.flowID = flowIdFromTemplate;
            flag = true;
          }
        }
        if (!flag)
          ownerDocument.AddDocumentFlow(this.flowID, false);
      }
    }
    base.RestoreFlowId();
  }

  /// <summary>Является ли заданный поток родительским</summary>
  /// <param name="flow">Родительский поток</param>
  /// <returns>true, если заданный поток родитель для этого элемента</returns>
  public virtual bool IsParentFlow(IParentFlow flow)
  {
    if (flow == null)
      return false;
    IParentFlow parentFlow = this.parentFlow;
    while (parentFlow != null)
    {
      parentFlow = parentFlow.ParentFlow;
      if (parentFlow == flow)
        return true;
    }
    return false;
  }

  /// <summary>Получить последний элемент цепочки в пределах заданного потока</summary>
  /// <param name="baseFlow">Родительский поток</param>
  /// <returns>Последний элемент цепочки</returns>
  protected virtual TableData GetLastChaineElement(IParentFlow baseFlow)
  {
    TableData nextTable = this.NextTable;
    TableData lastChaineElement = this;
    for (; nextTable != null && nextTable.IsParentFlow(baseFlow); nextTable = nextTable.NextTable)
      lastChaineElement = nextTable;
    return lastChaineElement;
  }

  /// <summary>Получить первый элемент цепочки в пределах заданного потока</summary>
  /// <param name="baseFlow">Родительский поток</param>
  /// <returns>Первый элемент цепочки</returns>
  protected virtual TableData GetFirstChaineElement(IParentFlow baseFlow)
  {
    TableData prevTable = this.PrevTable;
    TableData firstChaineElement = this;
    for (; prevTable != null && prevTable.IsParentFlow(baseFlow); prevTable = prevTable.PrevTable)
      firstChaineElement = prevTable;
    return firstChaineElement;
  }

  /// <summary>Вставить следующий элемент потока в цепочку</summary>
  /// <param name="newNextFlow">Старый следующий родительский элемент потока</param>
  public override void InsertNextFlowChaineElement(IParentFlow newNextFlow)
  {
    tableData1 = (TableData) null;
    FlowID flowId = this.GetStartTableOnPage().FlowID;
    if (flowId != null)
    {
      if (this.HasContinuation() && newNextFlow is PageData)
        return;
      IFlowElement flowElementByName = (IFlowElement) null;
      if (!(newNextFlow.GetFirstFlowElement(flowId, ref flowElementByName) is TableData tableData1) && flowElementByName is TableData tableData2)
        tableData1 = tableData2;
      if (tableData1 != null && this.nextCell != tableData1)
        this.InsertNextCell((RectangleElement) tableData1);
    }
    else if (this.ParentCell?.NextTable != null && this.ParentCell?.NextTable.Page != this.Page)
    {
      tableData1 = this.FindAlreadyCreatedNewNextTableForDataFlow(this.ParentCell.NextTable, false);
      if (tableData1 != null)
        this.InsertNextCell((RectangleElement) tableData1);
    }
    if (tableData1 == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].InsertNextFlowChaineElement((IParentFlow) tableData1);
  }

  public TableData GetStartTableOnPage()
  {
    TableData startTableOnPage = this;
    while (startTableOnPage.PrevTable != null && startTableOnPage.PrevTable.Page == this.Page)
      startTableOnPage = startTableOnPage.PrevTable;
    return startTableOnPage;
  }

  /// <summary>Назначить свойство FlowID</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetFlowID(FlowID value, bool updateUI, bool updateLayout)
  {
    if (this.flowID == value)
      return;
    this.flowID = value;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Таблица является первой в потоке</summary>
  [Browsable(false)]
  public override bool IsFirstInFlow
  {
    [DebuggerStepThrough] get => this.prevCell == null;
  }

  /// <summary>Ячейка является последней в потоке</summary>
  [Browsable(false)]
  public override bool IsLastInFlow
  {
    [DebuggerStepThrough] get => this.nextCell == null;
  }

  /// <summary>Найти предыдущую непустую таблицу потока данных</summary>
  /// <returns>Предыдущую непустую таблицу</returns>
  protected TableData FindNotEmptyPrevTable()
  {
    TableData notEmptyPrevTable = this;
    while (notEmptyPrevTable.prevCell != null)
    {
      notEmptyPrevTable = notEmptyPrevTable.PrevTable;
      if (!notEmptyPrevTable.FlowIsEmpty(this.flowID))
        break;
    }
    return notEmptyPrevTable;
  }

  /// <summary>Данные таблицы могут распределяться по страницам</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_470")]
  [CustomDescription("Attribute.Interfaces.Document_471")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool IsPageFlow
  {
    [DebuggerStepThrough] get => this.isPageFlow;
    set
    {
      if (this.isPageFlow == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (IsPageFlow), (object) this.IsPageFlow, (object) value);
      this.isPageFlow = value;
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (this.isPageFlow)
      {
        if (this.flowID == null)
        {
          if (this.page != null && this.page.Flows.Count == 0 && ownerDocument != null && ownerDocument.DocumentFlows.Count > 0)
            this.flowID = ownerDocument.DocumentFlows[0];
          if (this.flowID == null)
          {
            this.flowID = new FlowID(this.Name);
            if (this.flowID.Name == null || this.flowID.Name == "")
              this.flowID.Name = this.Id;
            if (ownerDocument != null)
              this.flowID.Name = ownerDocument.GetNewNameForFlowID(this.flowID.Name);
          }
        }
        if ((double) this.maxHeight == 0.0)
        {
          this.maxHeight = this.Size.Height;
          this.SetOverrideFlags(OverrideFlags.MaxHeight);
        }
        this.ConnectFlowToPage();
        if (ownerDocument != null && !ownerDocument.DocumentFlows.Contains(this.flowID))
          ownerDocument.AddDocumentFlow(this.flowID, true);
      }
      else
      {
        this.DisconnectFlowFromPage();
        if (this.flowID != null)
        {
          FlowID flowId = this.flowID;
          this.flowID = (FlowID) null;
          if (ownerDocument != null && ownerDocument.DocumentFlows.Contains(flowId))
          {
            IFlowElement flowElementByName = (IFlowElement) null;
            if (ownerDocument.FindFirstFlowElement(flowId, ref flowElementByName) == null)
              ownerDocument.DocumentFlows.Remove(flowId);
          }
        }
      }
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  [Browsable(false)]
  public bool IsStartFlowTable
  {
    get
    {
      return this.IsPageFlow && (this.prevCell == null || this.PrevCell?.Page != this.Page) && this.FlowID != null;
    }
  }

  /// <summary>Данные таблицы могут распределяться в пределах одного уровня</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_473")]
  [CustomDescription("Attribute.Interfaces.Document_474")]
  [CustomCategory("Attribute.Interfaces.Document_475")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool IsLocalFlow
  {
    [DebuggerStepThrough] get => this.ParentFlow == null;
    set
    {
      if (this.IsLocalFlow == value)
        return;
      if (value)
      {
        this.IsPageFlow = false;
        this.ParentFlow = (IParentFlow) null;
      }
      else
        this.FindParentFlow();
    }
  }

  /// <summary>Вырезать из цепочки потока, не разорвав цепочки</summary>
  public virtual void CutFromChain()
  {
    if (this.prevFlowElement != null)
    {
      this.prevFlowElement.NextFlowElement = this.nextFlowElement;
    }
    else
    {
      IParentFlow parentFlow = this.ParentFlow;
      if (parentFlow == null)
        return;
      parentFlow.RemoveChildFlowElement((IFlowElement) this);
      IFlowElement nextFlowElement = this.NextFlowElement;
      if (nextFlowElement == null)
        return;
      this.NextFlowElement = (IFlowElement) null;
      parentFlow.AddChildFlowElement(nextFlowElement);
    }
  }

  /// <summary>
  /// Настройка минимального количества строк для группировки с динамическим заголовком
  /// </summary>
  [Category("Debug")]
  public int MinRowsForDynamicHeaderGroup
  {
    get
    {
      int result;
      return int.TryParse(this.FindFirstTable().GetAttributeValue("GroupHeaderRowCount", true), out result) ? result : 2;
    }
    set => this.SetMinRowsForDynamicHeaderGroup(value, true, true);
  }

  public void SetMinRowsForDynamicHeaderGroup(int value, bool saveUndo, bool updateDoc)
  {
    this.SetAttributeValue("GroupHeaderRowCount", value.ToString(), saveUndo, updateDoc, updateDoc);
  }

  /// <inheritdoc />
  [Browsable(false)]
  public override int DesiredPageNumber
  {
    [DebuggerStepThrough] get => this.desiredPageNumber;
    set
    {
      TableData parentCell = this.ParentCell;
      if (parentCell != null && this.IsHeaderCell)
      {
        this.desiredPageNumber = -1;
        parentCell.SetDesiredPageNumber(value, false, false);
      }
      else
        this.SetDesiredPageNumber(value, false, false);
    }
  }

  public override float ContentHeight => Math.Max(this.MinHeight, this.cellsMinHeight);

  internal float CellsMinHeight => this.cellsMinHeight;

  /// <summary>Задать новое значение свойству MinHeight</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг, сбрасывающий наследование</param>
  public override void AssignMinHeight(
    float value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if ((double) this.MinHeight == (double) value)
      return;
    base.AssignMinHeight(value, updateUI, updateLayout, setOverrideFlag);
    if (!this.IsRow || this.Nodes == null)
      return;
    foreach (RectangleElement node in this.Nodes)
      node.AssignMinHeight(value, updateUI, updateLayout, setOverrideFlag);
  }

  /// <summary>Объединить распределенную таблицу в одну. Метод обратный DistributeTable</summary>
  public override void UniteTable()
  {
    if (this.nextCell == null)
      return;
    if (this.IsColumn)
    {
      if (this.nodes.Count > 0 && this.nodes[this.nodes.Count - 1] is RectangleElement node1)
        node1.UniteTable();
    }
    else
    {
      for (int index = this.nodes.Count - 1; index >= 0; --index)
      {
        if (this.nodes[index] is RectangleElement node2)
          node2.UniteTable();
      }
    }
    TableData nextTable = this.NextTable;
    List<TableData> tableDataList = new List<TableData>();
    TableData tableData = (TableData) null;
    ImDocumentData ownerDocument = this.OwnerDocument;
    for (; nextTable != null; nextTable = nextTable.NextTable)
    {
      PageData page1 = nextTable.Page;
      if ((page1 != null ? (page1.IsNextToAdditionalPage ? 1 : 0) : 0) != 0)
      {
        tableData = nextTable;
        break;
      }
      if (ownerDocument != null && ownerDocument.IsFileLoading)
      {
        long num = 0;
        PageData page2 = nextTable.Page;
        if (page2 != null)
        {
          for (int index = page2.Index; ownerDocument.IsFileLoading && num < 10000L && (page2.IsLockedForLoad || index >= ownerDocument.Nodes.Count - 2); ++num)
            Thread.Sleep(10);
          if (num >= 10000L)
            LogManager.AddLine("TableData.UniteTable# loop > 10000");
        }
      }
      int count1 = this.nodes.Count;
      if (this.isColumn)
      {
        if (nextTable.distributeBuffer != null)
        {
          for (int index = 0; index < nextTable.distributeBuffer.Count; ++index)
          {
            RectangleElement child = nextTable.distributeBuffer[index];
            if (child != null && child.TableCellType == CellType.DataCell)
              this.InsertChildNode(count1, (DocumentTreeNode) child, true, false, false, false, false);
          }
        }
        int count2 = this.nodes.Count;
        for (int index = nextTable.nodes.Count - 1; index >= 0; --index)
        {
          if (index < nextTable.nodes.Count && nextTable.nodes[index] is RectangleElement node3 && node3.TableCellType == CellType.DataCell)
          {
            node3.UniteTable();
            this.InsertChildNode(count2, (DocumentTreeNode) node3, true, false, false, false, false);
          }
        }
      }
      tableDataList.Add(nextTable);
    }
    TableData parentCell = this.ParentCell;
    if (this.nextCell == null || parentCell == null)
      return;
    if (parentCell.IsColumn)
    {
      for (int index = tableDataList.Count - 1; index >= 0; --index)
      {
        PageData page = tableDataList[index].Page;
        if ((page != null ? (page.IsFirstAdditionalPageInChain ? 1 : 0) : 0) != 0)
          tableData = tableDataList[index];
        else
          tableDataList[index].Remove(false, false, false);
      }
    }
    this.SetNextCell((RectangleElement) tableData);
  }

  /// <summary>Объединить распределенную таблицу в одну. Метод обратный DistributeTable</summary>
  internal override void OneStepUniteTable(bool dontUniteTopLevelTable = true)
  {
    if (this.nextCell == null || (this.Page == null || this.nextCell.Page == null ? 1 : (!this.nextCell.Page.IsNextToAdditionalPage ? 1 : 0)) == 0)
      return;
    if (this.IsColumn)
    {
      if (this.nodes.Count > 0 && this.nodes[this.nodes.Count - 1] is RectangleElement node1)
        node1.OneStepUniteTable();
    }
    else
    {
      for (int index = this.nodes.Count - 1; index >= 0; --index)
      {
        if (this.nodes[index] is RectangleElement node2)
          node2.OneStepUniteTable();
      }
    }
    TableData nextTable1 = this.NextTable;
    TableData nextTable2 = nextTable1?.NextTable;
    if (nextTable1 != null)
    {
      int count1 = this.nodes.Count;
      if (this.isColumn)
      {
        if (nextTable1.distributeBuffer != null)
        {
          for (int index = 0; index < nextTable1.distributeBuffer.Count; ++index)
          {
            RectangleElement child = nextTable1.distributeBuffer[index];
            if (child != null && child.TableCellType == CellType.DataCell)
              this.InsertChildNode(count1, (DocumentTreeNode) child, true, false, false, false, false);
          }
        }
        int count2 = this.nodes.Count;
        for (int index = nextTable1.nodes.Count - 1; index >= 0; --index)
        {
          if (nextTable1.nodes[index] is RectangleElement node3 && node3.TableCellType == CellType.DataCell)
          {
            node3.OneStepUniteTable();
            this.InsertChildNode(count2, (DocumentTreeNode) node3, true, false, false, false, false);
          }
        }
      }
    }
    if (nextTable1 == null)
      return;
    TableData parentCell = nextTable1.ParentCell;
    if (parentCell == null && dontUniteTopLevelTable)
      return;
    if ((parentCell != null ? (parentCell.IsColumn ? 1 : 0) : 1) != 0)
      nextTable1.Remove(false, false, false);
    this.SetNextCell((RectangleElement) nextTable2);
  }

  /// <summary>Сделать копию сетки и обновить ссылки на нее из массива</summary>
  /// <param name="gridCols">Сетка, которая заменяется на свою копию</param>
  private void CloneGridAndUpdateRefences(ref List<RowColParams> gridCols, bool setOverrideFlag)
  {
    this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(gridCols), setOverrideFlag, false);
    gridCols = this.gridColumnsParams;
  }

  /// <summary>Автоматически настроить ширину ячеек по содержимому или ширину и высоту относительно владельца</summary>
  /// <param name="gridCols">Параметры столбцов</param>
  public void AutoSizeCells(List<RowColParams> gridCols)
  {
    if (gridCols == null || this.IsColumnGridOwner())
    {
      gridCols = this.gridColumnsParams != null ? this.gridColumnsParams : this.GetGridColumnsParams(true);
      if (gridCols != null)
      {
        gridCols = TableData.CloneRowColParams(gridCols);
        for (int index = 0; index < gridCols.Count; ++index)
          gridCols[index].Size = 0.0f;
      }
    }
    for (int index1 = 0; index1 < this.nodes.Count; ++index1)
    {
      if (this.nodes[index1] is TableData node2)
      {
        if (this.IsFixedStructureArea && ((double) node2.relativeWidth > 0.0 || (double) node2.relativeHeight > 0.0))
        {
          RectangleF properBounds = node2.properBounds;
          if ((double) node2.relativeWidth > 0.0)
            properBounds.Width = this.properBounds.Width * (node2.relativeWidth / 100f) - node2.cellMargins.X - node2.cellMargins.Width;
          if ((double) node2.relativeHeight > 0.0)
            properBounds.Height = this.properBounds.Height * (node2.relativeWidth / 100f) - node2.cellMargins.Y - node2.cellMargins.Height;
          node2.AssignProperBounds(properBounds, false, false, false);
        }
        node2.AutoSizeCells(gridCols);
      }
      else if (this.nodes[index1] is RectangleElement node1)
      {
        float width = node1.bounds.Width;
        if (this.IsFixedStructureArea && ((double) node1.relativeWidth > 0.0 || (double) node1.relativeHeight > 0.0))
        {
          RectangleF properBounds = node1.properBounds;
          if ((double) node1.relativeWidth > 0.0)
            properBounds.Width = this.properBounds.Width * (node1.relativeWidth / 100f) - node1.cellMargins.X - node1.cellMargins.Width;
          if ((double) node1.relativeHeight > 0.0)
            properBounds.Height = this.properBounds.Height * (node1.relativeWidth / 100f) - node1.cellMargins.Y - node1.cellMargins.Height;
          node1.AssignProperBounds(properBounds, false, false, false);
        }
        if ((double) node1.relativeWidth <= 0.0 && node1.AutoSizeWidth && node1.NeedUpdateLayoutFlag)
          this.Distribute(new DistributeContext(), false);
        float newCellWidth = node1.MinWidth;
        if (node1.AutoSizeWidth && (double) newCellWidth > 0.0 && !node1.WidthOverrided)
        {
          int gridColumnIndex = node1.GetGridColumnIndex();
          if (gridColumnIndex != -1 && gridCols != null && gridColumnIndex < this.GridColumnsParams.Count)
          {
            float num = this.GridColumnsParams[gridColumnIndex].Size;
            int index2 = gridColumnIndex;
            if (!node1.IsDefaultGridPos)
            {
              int spanCount = node1.GridPos.SpanCount;
              if (spanCount != 0)
                index2 = gridColumnIndex + spanCount - 1;
              if (index2 >= gridCols.Count)
                index2 = gridCols.Count - 1;
              newCellWidth = (float) Math.Round((double) node1.CalcGridColumnWidth(gridColumnIndex, newCellWidth), 5);
              num = (float) Math.Round((double) node1.CalcGridColumnWidth(gridColumnIndex, width), 5);
            }
            if (node1.WidthOverrided && (double) num == (double) width)
              node1.WidthOverrided = false;
            if (gridCols[index2] != null && (double) gridCols[index2].Size < (double) newCellWidth)
              gridCols[index2].Size = newCellWidth;
          }
        }
      }
    }
    if (this.IsFixedStructureArea || gridCols == null || !this.IsColumnGridOwner() || gridCols.Count != this.gridColumnsParams.Count)
      return;
    bool flag = false;
    for (int index = 0; index < gridCols.Count; ++index)
    {
      if ((double) gridCols[index].Size > 0.0)
      {
        this.gridColumnsParams[index].Size = gridCols[index].Size;
        flag = true;
      }
    }
    if (!flag)
      return;
    this.SetNeedUpdateLayoutForColumns(gridCols, this.gridColumnsParams);
  }

  /// <summary>Установить флаг NeedUpdateLayout для столбцов</summary>
  /// <param name="changedGridColumns">Измененные столбцы</param>
  /// <param name="originalGridColumns">Оригинальные столбцы</param>
  /// <returns></returns>
  private bool SetNeedUpdateLayoutForColumns(
    List<RowColParams> changedGridColumns,
    List<RowColParams> originalGridColumns)
  {
    bool flag = false;
    int index1 = 0;
    for (int index2 = 0; index2 < this.nodes.Count; ++index2)
    {
      if (this.nodes[index2] is RectangleElement node1)
      {
        TableData node = this.nodes[index2] as TableData;
        if (this.IsColumn)
        {
          if (node != null && node.gridColumnsParams == originalGridColumns)
            flag = node.SetNeedUpdateLayoutForColumns(changedGridColumns, originalGridColumns) | flag;
        }
        else
        {
          int num = 1;
          if (!this.IsDefaultGridPos)
            num = this.GridPos.SpanCount;
          for (int index3 = index1; index3 < changedGridColumns.Count && index3 < index1 + num; ++index3)
          {
            if ((double) changedGridColumns[index1].Size > 0.0)
            {
              node1.AssignNeedUpdateLayoutFlag(true);
              flag = true;
            }
          }
          index1 += num;
        }
      }
    }
    if (flag)
      this.AssignNeedUpdateLayoutFlag(true);
    return flag;
  }

  /// <summary>Переместить поточные данные в следующую таблицу и исключить эту таблицу из потока данных</summary>
  internal void MoveFlowDataToPrevTable()
  {
    if (this.prevCell == null && this.FlowID != null)
    {
      PageData page = this.Page;
      IFlowElement flowElement;
      if (page == null)
      {
        flowElement = (IFlowElement) null;
      }
      else
      {
        PageData prevPage = page.PrevPage;
        flowElement = prevPage != null ? prevPage.Flows.OfType<IFlowElement>().FirstOrDefault<IFlowElement>((Func<IFlowElement, bool>) (f => f is TableData tableData && this.FlowID == tableData.FlowID)) : (IFlowElement) null;
      }
      if (flowElement is TableData tableData1)
        tableData1.SetNextCell((RectangleElement) this);
    }
    if (this.prevCell == null || this.Page == this.PrevCell.Page)
      return;
    this.PrevCell.OneStepUniteTable(false);
  }

  /// <summary>Переместить поточные данные в предыдущую таблицу и исключить эту таблицу из потока данных</summary>
  internal void MoveFlowDataToNextTable()
  {
    if (this.NextCell == null)
      return;
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) this.Nodes);
    for (int index = documentTreeNodeList.Count - 1; index >= 0; --index)
      this.NextCell.InsertChildNode(0, documentTreeNodeList[index], true, true, false, false, false);
    this.NextCell.SetPrevCell(this.PrevCell);
  }

  public override float GetTableFreeSpace()
  {
    if (this.TopLevelTable != null && this.TopLevelTable != this)
      return this.TopLevelTable.GetTableFreeSpace();
    if (this.nodes != null && this.nodes.Count > 0)
    {
      int visibleCellIndex = this.FindLastVisibleCellIndex();
      if (visibleCellIndex != -1 && this.nodes[visibleCellIndex] is RectangleElement node)
        return this.MaxHeight - (node.Bounds.Bottom - this.Bounds.Y);
    }
    return this.MaxHeight;
  }

  /// <summary>Только для внутреннего использования. Получить минимальный неделимый размер для разбивки</summary>
  /// <note>Используется для определения свободного пространства в только что созданной для переноса таблице</note>
  public override float GetMinimalSizeForDistribute(DistributeContext context)
  {
    if (this.nodes == null || this.nodes.Count == 0)
      return base.GetMinimalSizeForDistribute(context);
    context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template = this.Template as RectangleElement, (CellContext) context));
    context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
    context.TryNotBreak |= this.tryNotBreak;
    float sizeForDistribute1 = 0.0f;
    if ((double) this.minHeight != 0.0)
      sizeForDistribute1 = this.minHeight;
    if ((double) sizeForDistribute1 > (double) context.MaxSize.Height)
      return sizeForDistribute1;
    if (this.isColumn)
      sizeForDistribute1 = 0.0f;
    int num = 0;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node)
      {
        if (node.TableCellType == CellType.Header && !this.HeaderIsNeed(this.prevCell == null, node.HeaderShowType) && !this.HeaderIsDisabled(node.TemplateId))
        {
          num = index + 1;
        }
        else
        {
          DistributeContext context1 = new DistributeContext((DocumentTreeNode) node, node.Size, new SizeF(context.MaxSize.Width, this.isColumn ? context.MaxSize.Height - sizeForDistribute1 : context.MaxSize.Height), index == 0 || this.IsRow, index - num == 0 || this.IsRow, context);
          float sizeForDistribute2 = node.GetMinimalSizeForDistribute(context1);
          if (this.isColumn)
            sizeForDistribute1 += sizeForDistribute2;
          else if ((double) sizeForDistribute1 < (double) sizeForDistribute2)
            sizeForDistribute1 = sizeForDistribute2;
          if (this.isColumn && node.TableCellType == CellType.DataCell || (double) sizeForDistribute1 > (double) context.MaxSize.Height)
            return sizeForDistribute1;
        }
      }
    }
    return (double) sizeForDistribute1 == 0.0 ? this.minHeight : sizeForDistribute1;
  }

  /// <summary>Максимальная высота. Отличается от MaxHeight тем, что 0 заменяется на UnconstrainedSize</summary>
  [Browsable(false)]
  public float RealMaxHeight
  {
    [DebuggerStepThrough] get
    {
      return (double) this.MaxHeight == 0.0 || !this.IsPageFlow ? TableData.UnconstrainedSize : this.MaxHeight;
    }
  }

  /// <summary>Обновление представлений данных временно заблокировано</summary>
  public override bool SuspendedUpdateLayoutFlag
  {
    [DebuggerStepThrough] get => this.IsDistributing || base.SuspendedUpdateLayoutFlag;
  }

  /// <summary>Обновление геометрии интерфейса пользователя заблокировано</summary>
  public override bool SuspendedUpdateUIGeometryFlag
  {
    [DebuggerStepThrough] get => this.IsDistributing || base.SuspendedUpdateUIGeometryFlag;
  }

  /// <summary>Обновление изображения интерфейса пользователя заблокировано</summary>
  public override bool SuspendedRefreshUIFlag
  {
    [DebuggerStepThrough] get => this.IsDistributing || base.SuspendedRefreshUIFlag;
  }

  /// <summary>Подсчитать количество ячеек заголовка в начале таблицы</summary>
  protected virtual int CalcFirstHeaderCount()
  {
    int num = 0;
    if (this.isColumn || this.IsRow && this.GridColumnsParams == null)
    {
      int index = 0;
      for (int count = this.nodes.Count; index < count && (!(this.nodes[index] is RectangleElement node) || node.TableCellType != CellType.DataCell); ++index)
        ++num;
    }
    return num;
  }

  /// <summary>Подсчитать количество ячеек заголовка в начале таблицы</summary>
  /// <param name="currentIndex">Текущая ячейка, за которой нужно искать. Если искать нужно с начала, то -1</param>
  /// <returns>Возвращает первую ячейку данных, или -1, если в этой таблице она не найдена (на следующую не переходит)</returns>
  private int FindNextDataCellInThisTable(int currentIndex)
  {
    bool flag = !this.isColumn && (!this.IsRow || this.GridColumnsParams != null);
    for (int index = currentIndex + 1; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node && node.IsVisibleNow && (flag || node.TableCellType == CellType.DataCell))
        return index;
    }
    return -1;
  }

  /// <summary>Подсчитать количество ячеек данных в таблице</summary>
  public virtual int CalcDataCellCount()
  {
    int num = 0;
    foreach (RectangleElement rectangleElement in this)
      ++num;
    return num;
  }

  /// <summary>Для заданного значения isFirstInFlow
  /// заголовок заданного типа должен присутствовать в таблице</summary>
  /// <param name="isFirstInFlow">Элемент является первым в потоке</param>
  /// <param name="showType">Тип заголовка</param>
  /// <returns>Возвращает true, если для заданного значения isFirstInFlow
  /// заголовок заданного типа должен присутствовать в таблице</returns>
  protected virtual bool HeaderIsNeed(bool isFirstInFlow, HeaderShowType showType)
  {
    if (!this.IsTemplate)
    {
      switch (showType)
      {
        case HeaderShowType.All:
          return true;
        case HeaderShowType.FirstOnly:
          return isFirstInFlow;
        case HeaderShowType.NextOnly:
          return !isFirstInFlow;
      }
    }
    return true;
  }

  private bool HeaderIsDisabled(string headerTemplateId)
  {
    TableData firstTable = this.FindFirstTable();
    return !string.IsNullOrEmpty(headerTemplateId) && !firstTable.disabledHeaders.IsEmpty<string>() && firstTable.disabledHeaders.Contains(headerTemplateId);
  }

  public void DisableHeader(string headerTemplateId)
  {
    if (string.IsNullOrEmpty(headerTemplateId))
      return;
    TableData firstTable = this.FindFirstTable();
    if (firstTable.disabledHeaders == null)
      firstTable.disabledHeaders = new List<string>();
    if (firstTable.disabledHeaders.Contains(headerTemplateId))
      return;
    firstTable.disabledHeaders.Add(headerTemplateId);
  }

  public void EnableHeader(string headerTemplateId)
  {
    if (string.IsNullOrEmpty(headerTemplateId) || this.disabledHeaders.IsEmpty<string>())
      return;
    this.disabledHeaders.Remove(headerTemplateId);
  }

  /// <summary>Может ли таблица переносить данные (распределять узлы Nodes) не помещающиеся по высоте</summary>
  public virtual bool CanVerticalDistribute() => this.IsColumn && this.CanVerticalSplit();

  /// <summary>Может ли таблица разбиваться для распределения данных вертикально</summary>
  public virtual bool CanVerticalSplit()
  {
    bool flag = (this.page == null || this.page.NextPageTemplateId != null || this.page.TemplateId != null) && !this.IsTemplate;
    if (flag)
      flag = this.TableCellType != CellType.DataCell || this.ParentCell == null ? this.IsPageFlow && (double) this.MaxHeight != 0.0 : this.ParentCell.CanVerticalSplit();
    return flag;
  }

  /// <summary>Может ли таблица переносить данные не помещающиеся по ширине</summary>
  public virtual bool CanHorizontalDistribute() => false;

  /// <summary>Создать и вставить новую таблицу для продолжения разбивки и
  /// последующего переноса на нее не поместившихся данных</summary>
  protected virtual void AddNewTableAndParentsInDataFlow()
  {
    if (this.IsVirtualNode)
      return;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && this.TableCellType != CellType.DataCell)
      LogManager.AddLine(LocalizationHolder.rm.GetString("Interfaces.Document_127"));
    else if (parentCell != null)
    {
      if (this.NextCell != null)
        return;
      bool parentTableIsNew;
      bool parentTableIsPrevious;
      TableData parentForNewTable = this.FindOrCreateParentForNewTable(parentCell, out parentTableIsNew, out parentTableIsPrevious);
      TableData tableData = this.FindAlreadyCreatedNewNextTableForDataFlow(parentForNewTable, parentTableIsNew) ?? this.InsertCloneDataFlowCell(parentForNewTable, parentTableIsPrevious);
      if (parentTableIsPrevious)
        this.InsertPrevCell((RectangleElement) tableData);
      else
        this.InsertNextCell((RectangleElement) tableData);
    }
    else
      this.AddNewPageForDataflow();
  }

  private void AddNewPageForDataflow()
  {
    FlowID flowId = this.GetStartTableOnPage().FlowID;
    if (flowId == null)
      return;
    PageData nextPage = this.page.NextPage;
    IFlowElement flowElement = (IFlowElement) null;
    if (nextPage != null && !nextPage.IsNextToAdditionalPage)
      flowElement = nextPage.GetFirstFlowElement(flowId);
    if (nextPage == null || flowElement == null)
    {
      PageData pageData1 = !this.page.IsLastAdditionalPageInChain ? this.page.AddNewDataPage(true) : this.page.AddNewDataPage(true, PageNumBuilder.Parse(this.page.HierarchicalPageNumber).IncrementExtension().ToString());
      PageData pageData2;
      if (this.page.IsFinalPage)
      {
        pageData2 = this.page.PrevPage;
        firstFlowElement = pageData2.GetFirstFlowElement(flowId) as TableData;
        this.InsertPrevCell((RectangleElement) firstFlowElement);
      }
      else
      {
        pageData2 = this.page.NextPage;
        if (!(pageData2.GetFirstFlowElement(flowId) is TableData firstFlowElement))
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_198"), (object) pageData2.TemplateId));
        if (this.NextTable != firstFlowElement)
          this.InsertNextCell((RectangleElement) firstFlowElement);
      }
      pageData2?.SetNeedUpdateLayoutFlag(true, false, false, false);
      firstFlowElement?.SetNeedUpdateLayoutFlag(true, false, false, false);
    }
    else
    {
      if (!(nextPage.GetFirstFlowElement(flowId) is TableData firstFlowElement) || this.NextTable != null)
        return;
      this.InsertNextCell((RectangleElement) firstFlowElement);
    }
  }

  private TableData InsertCloneDataFlowCell(TableData newParentTable, bool parentTableIsPrevious = false)
  {
    TableData child = (TableData) this.Clone(false, false);
    child.Id = this.GenerateIdForNextCell();
    if (this.IsRow)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
        {
          RectangleElement rectangleElement = node.Clone(false, false) as RectangleElement;
          rectangleElement.Id = node.GenerateIdForNextCell();
          child.AddChildNode((DocumentTreeNode) rectangleElement, false, false, false, false);
          if (parentTableIsPrevious)
            node.InsertPrevCell(rectangleElement);
          else
            node.InsertNextCell(rectangleElement);
        }
      }
    }
    child.SetNeedUpdateLayoutFlag(true, false, false, false, true);
    child.setBounds(BoundsHelper.SetHeight(child.bounds, 0.0f));
    newParentTable.InsertChildNode(0, (DocumentTreeNode) child, false, true, false, false, true);
    newParentTable.SetCellSizes(newParentTable.bounds, true, false, false, false, false);
    return child;
  }

  public override IEnumerable<DocumentTreeNode> NodesRecursive
  {
    get
    {
      foreach (DocumentTreeNode documentTreeNode in base.NodesRecursive)
        yield return documentTreeNode;
      if (!this.distributeBuffer.IsEmpty<RectangleElement>())
      {
        foreach (RectangleElement cellInBuffer in this.distributeBuffer)
        {
          yield return (DocumentTreeNode) cellInBuffer;
          foreach (DocumentTreeNode documentTreeNode in cellInBuffer.NodesRecursive)
            yield return documentTreeNode;
        }
      }
    }
  }

  public override IEnumerable<DocumentTreeNode> NodesRecursiveByCondition(
    Func<DocumentTreeNode, bool> predicate)
  {
    foreach (DocumentTreeNode documentTreeNode in base.NodesRecursiveByCondition(predicate))
      yield return documentTreeNode;
    if (!this.distributeBuffer.IsEmpty<RectangleElement>())
    {
      foreach (RectangleElement cellInBuffer in this.distributeBuffer)
      {
        if (predicate((DocumentTreeNode) cellInBuffer))
        {
          yield return (DocumentTreeNode) cellInBuffer;
          foreach (DocumentTreeNode documentTreeNode in cellInBuffer.NodesRecursiveByCondition(predicate))
            yield return documentTreeNode;
        }
      }
    }
  }

  internal override void ResetDistributeState()
  {
    base.ResetDistributeState();
    if (!this.IsTopLevelTable || !this.IsPageFlow)
      return;
    foreach (RectangleElement rectangleElement in this.NodesRecursive.OfType<RectangleElement>())
      rectangleElement.ResetDistributeState();
  }

  private TableData FindAlreadyCreatedNewNextTableForDataFlow(
    TableData parentTable,
    bool parentTableIsNew)
  {
    if (parentTable == null)
      throw new ArgumentNullException(nameof (parentTable));
    TableData tableForDataFlow = (TableData) null;
    if (this.flowID != null)
      tableForDataFlow = parentTable.GetFirstFlowElement(this.flowID) as TableData;
    if (parentTableIsNew && tableForDataFlow == null && !string.IsNullOrEmpty(this.TemplateId))
      tableForDataFlow = parentTable.FindFirstNodeFromTemplate_Recursive(this.TemplateId, true) as TableData;
    return tableForDataFlow;
  }

  private TableData FindOrCreateParentForNewTable(
    TableData parentCell,
    out bool parentTableIsNew,
    out bool parentTableIsPrevious)
  {
    TableData parentForNewTable = (TableData) null;
    parentTableIsPrevious = false;
    parentTableIsNew = false;
    TableData prevTable = parentCell.PrevTable;
    if (parentCell.NextTable == null)
    {
      parentCell.AddNewTableAndParentsInDataFlow();
      parentTableIsNew = true;
    }
    if (parentCell.NextTable != null)
      parentForNewTable = parentCell.NextTable;
    else if (prevTable != parentCell.PrevTable)
    {
      parentTableIsPrevious = true;
      parentForNewTable = parentCell.PrevTable;
    }
    return parentForNewTable;
  }

  public void RecursiveConnectNextPageByEmptyTables()
  {
    if (this.NextCell == null || this.Page.NextPage == null)
      return;
    if (this.ParentCell != null && this.NextCell.Page != this.Page.NextPage)
      this.InsertNextCell((RectangleElement) this.InsertCloneDataFlowCell(this.ParentCell.NextTable));
    if (!this.IsColumn || this.nodes.Count <= 0 || !(this.nodes[this.nodes.Count - 1] is TableData node))
      return;
    node.RecursiveConnectNextPageByEmptyTables();
  }

  /// <summary>Связать цепочку распределения потока со страницей</summary>
  public void ConnectFlowToPage()
  {
    if (this.page == null)
      return;
    IFlowElement flowElement = (IFlowElement) this.TopLevelTable;
    for (DocumentTreeNode parent = this.TopLevelTable.Parent; parent != null; parent = parent.Parent)
    {
      if (parent is IParentFlow parentFlow)
      {
        flowElement.ParentFlow = parentFlow;
        flowElement = (IFlowElement) parentFlow;
      }
      if (parent is PageData)
        break;
    }
  }

  /// <summary>Разорвать связь цепочки распределения потока со страницей</summary>
  public void DisconnectFlowFromPage() => this.AssignParentFlow((IParentFlow) null);

  /// <summary>Найти родительский элемент цепочки</summary>
  protected void FindParentFlow()
  {
    if (this.ParentCell == null)
    {
      parentFlow = (IParentFlow) null;
      DocumentTreeNode parent = this.Parent;
      while (true)
      {
        switch (parent)
        {
          case null:
          case IParentFlow parentFlow:
          case ImDocumentData _:
            goto label_4;
          default:
            parent = parent.Parent;
            continue;
        }
      }
label_4:
      this.ParentFlow = parentFlow;
    }
    else
      this.ParentFlow = (IParentFlow) null;
  }

  /// <summary>Родительский элемент цепочки. Для подтаблицы это родительская таблица!</summary>
  [Browsable(false)]
  public IParentFlow ParentFlow
  {
    [DebuggerStepThrough] get
    {
      return this.ParentCell != null ? (IParentFlow) this.ParentCell : this.parentFlow;
    }
    set
    {
      if (this.parentFlow == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (ParentFlow), (object) this.ParentFlow, (object) value);
      if (this.parentFlow != null)
        this.parentFlow.RemoveChildFlowElement((IFlowElement) this);
      if (this.ParentCell != null)
      {
        this.parentFlow = (IParentFlow) null;
      }
      else
      {
        this.parentFlow = value;
        if (this.parentFlow != null)
          this.parentFlow.AddChildFlowElement((IFlowElement) this);
      }
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Присвоить значение ParentFlow без вызова
  /// ParentFlow.AddChildFlowElement или ParentFlow.RemoveChildFlowElement</summary>
  /// <param name="value">Новое значение ParentFlow</param>
  public void AssignParentFlow(IParentFlow value)
  {
    if (this.parentFlow == value)
      return;
    if (this.parentFlow != null)
    {
      IParentFlow parentFlow = this.parentFlow;
      this.parentFlow = (IParentFlow) null;
      parentFlow.RemoveChildFlowElement((IFlowElement) this);
    }
    if (this.ParentCell != null)
      this.parentFlow = (IParentFlow) null;
    this.parentFlow = value;
  }

  /// <summary>Предыдущая таблица цепочки распределения потока данных</summary>
  [Browsable(false)]
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  public TableData PrevTable
  {
    [DebuggerStepThrough] get => this.prevCell as TableData;
  }

  /// <summary>Следующая таблица цепочки распределения потока данных</summary>
  [Browsable(false)]
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  public TableData NextTable
  {
    [DebuggerStepThrough] get => this.nextCell as TableData;
  }

  protected override bool IsAllowableLocalDataLink()
  {
    return base.IsAllowableLocalDataLink() && this.IsPageFlow;
  }

  /// <summary>Найти первую таблицу в цепочке разбивки таблицы по страницам</summary>
  public TableData FindFirstTable()
  {
    RectangleElement firstTable;
    for (firstTable = (RectangleElement) this; firstTable.PrevCell != null; firstTable = firstTable.PrevCell)
    {
      if (firstTable.PrevCell == this)
      {
        LogManager.AddLine("TableData.FindFirstTable(): prevCell loop!");
        break;
      }
    }
    return (TableData) firstTable;
  }

  /// <summary>Следующий элемент цепочки</summary>
  [Browsable(false)]
  public virtual IFlowElement NextFlowElement
  {
    [DebuggerStepThrough] get => this.nextFlowElement;
    set
    {
      if (this.nextFlowElement == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (NextFlowElement), (object) this.NextFlowElement, (object) value);
      if (this.nextFlowElement != null)
      {
        IFlowElement nextFlowElement = this.nextFlowElement;
        this.nextFlowElement = (IFlowElement) null;
        nextFlowElement.PrevFlowElement = (IFlowElement) null;
      }
      this.nextFlowElement = value;
      if (this.nextFlowElement != null)
        this.nextFlowElement.PrevFlowElement = (IFlowElement) this;
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Предыдущий элемент цепочки</summary>
  [Browsable(false)]
  public virtual IFlowElement PrevFlowElement
  {
    [DebuggerStepThrough] get => this.prevFlowElement;
    set
    {
      if (this.prevFlowElement == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (PrevFlowElement), (object) this.PrevFlowElement, (object) value);
      if (this.prevFlowElement != null)
      {
        IFlowElement prevFlowElement = this.prevFlowElement;
        this.prevFlowElement = (IFlowElement) null;
        prevFlowElement.NextFlowElement = (IFlowElement) null;
      }
      this.prevFlowElement = value;
      if (this.prevFlowElement != null)
        this.prevFlowElement.NextFlowElement = (IFlowElement) this;
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Найти последний элемент заданной цепочки</summary>
  /// <param name="element">Элемент цепочки с которого нужно начать поиск</param>
  /// <returns>Последний элемент цепочки</returns>
  protected static IFlowElement FindLastChainElement(IFlowElement element)
  {
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    while (element.NextFlowElement != null)
      element = element.NextFlowElement;
    return element;
  }

  /// <summary>Получить последний элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Последний элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetLastFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement lastFlowElement = (IFlowElement) null;
    if (this.flowID == flow)
    {
      lastFlowElement = (IFlowElement) this;
    }
    else
    {
      if (this.flowID != null && this.flowID.Name == flow.Name && flowElementByName == null)
        flowElementByName = (IFlowElement) this;
      for (int index = 0; index < this.nodes.Count && lastFlowElement == null; ++index)
      {
        if (this.nodes[index] is IFlowElement element)
          element = TableData.FindLastChainElement(element);
        for (; lastFlowElement == null && element != null; element = element.PrevFlowElement)
          lastFlowElement = element.GetLastFlowElement(flow, ref flowElementByName);
      }
    }
    return lastFlowElement;
  }

  /// <summary>Получить предыдущий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Предыдущий элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetPrevFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement prevFlowElement1 = (IFlowElement) null;
    for (IFlowElement prevFlowElement2 = this.PrevFlowElement; prevFlowElement2 != null && prevFlowElement1 == null; prevFlowElement2 = prevFlowElement2.PrevFlowElement)
      prevFlowElement1 = prevFlowElement2.GetLastFlowElement(flow, ref flowElementByName);
    if (prevFlowElement1 == null && this.ParentFlow != null)
      prevFlowElement1 = this.ParentFlow.GetPrevFlowElement(flow, ref flowElementByName);
    return prevFlowElement1;
  }

  /// <summary>Получить первый элемент цепочки для заданного потока данных.
  /// Ищет внутри и по цепочкам дочерних узлов</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Первый элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetFirstFlowElement(FlowID flow)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement flowElementByName = (IFlowElement) null;
    return this.GetFirstFlowElement(flow, ref flowElementByName) ?? flowElementByName;
  }

  /// <summary>Получить первый элемент цепочки для заданного потока данных.
  /// Ищет внутри и по цепочкам дочерних узлов</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Первый элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetFirstFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement firstFlowElement = (IFlowElement) null;
    if (this.flowID != null && this.flowID == flow)
    {
      firstFlowElement = (IFlowElement) this.GetStartTableOnPage();
    }
    else
    {
      if (this.flowID != null && this.flowID.Name == flow.Name && flowElementByName == null)
        flowElementByName = (IFlowElement) this.GetStartTableOnPage();
      if (flowElementByName == null && (this.Name.Contains(flow.Name) || this.id.Contains(flow.Name)))
      {
        this.Name = flow.Name;
        flowElementByName = (IFlowElement) this.GetStartTableOnPage();
      }
      for (int index = 0; index < this.nodes.Count && firstFlowElement == null; ++index)
      {
        for (IFlowElement flowElement = this.nodes[index] as IFlowElement; firstFlowElement == null && flowElement != null; flowElement = flowElement.NextFlowElement)
          firstFlowElement = flowElement.GetFirstFlowElement(flow, ref flowElementByName);
      }
    }
    return firstFlowElement;
  }

  /// <summary>Получить следующий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Следующий элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetNextFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement nextFlowElement1 = (IFlowElement) null;
    for (IFlowElement nextFlowElement2 = this.NextFlowElement; nextFlowElement2 != null && nextFlowElement1 == null; nextFlowElement2 = nextFlowElement2.NextFlowElement)
      nextFlowElement1 = nextFlowElement2.GetFirstFlowElement(flow, ref flowElementByName);
    if (nextFlowElement1 == null && this.ParentFlow != null)
      nextFlowElement1 = this.ParentFlow.GetNextFlowElement(flow, ref flowElementByName);
    return nextFlowElement1;
  }

  /// <summary>Добавить дочерний элемент цепочки</summary>
  /// <param name="child">Дочерний элемент цепочки</param>
  public void AddChildFlowElement(IFlowElement child)
  {
  }

  /// <summary>Удалить дочерний элемент цепочки</summary>
  /// <param name="child">Дочерний элемент цепочки</param>
  public void RemoveChildFlowElement(IFlowElement child)
  {
  }

  /// <summary>Копировать поля из src</summary>
  /// <param name="src">Источник</param>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать данные</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  protected override void CopyFields(
    DocumentTreeNode src,
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    TableData tableData = src as TableData;
    base.CopyFields(src, copyChildren, copyData, copyDataNodes, templateClone, externalLink, links);
    if (tableData == null)
      return;
    this.isColumn = tableData.isColumn;
    this.isFixedStructureArea = tableData.isFixedStructureArea;
    this.FreeSpace = tableData.FreeSpace;
    this.autoSizeHeight = tableData.autoSizeHeight;
    this.alignLastRows = tableData.alignLastRows;
    this.drawGridToBottom = tableData.drawGridToBottom;
    this.desiredPageNumber = tableData.desiredPageNumber;
    this.showSingleCellInTemplate = tableData.showSingleCellInTemplate;
    this.isPageFlow = tableData.isPageFlow;
    if (templateClone)
    {
      if (tableData.flowID != null && links[(object) tableData.flowID] is FlowID link)
        this.flowID = link;
    }
    else
    {
      this.flowID = tableData.flowID;
      this.disabledHeaders = tableData.disabledHeaders != null ? new List<string>((IEnumerable<string>) tableData.disabledHeaders) : (List<string>) null;
    }
    this.usePreviousTableTemplates = tableData.usePreviousTableTemplates;
    if (this.reference != null)
    {
      this.reference.DisconnectLink();
      this.reference = (ReferenceBase) null;
    }
    if (tableData.reference != null & copyData)
    {
      this.reference = tableData.reference.Clone();
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
    }
    if (templateClone)
    {
      this.SetGridRowsParams((List<RowColParams>) null);
      if (tableData.gridColumnsParams != null)
        this.SetGridColumnsParams(TableData.CloneRowColParamsFromTemplate(tableData.gridColumnsParams), false, false);
    }
    else
    {
      if (tableData.gridRowsParams != null)
      {
        this.SetGridRowsParams(new List<RowColParams>(tableData.gridRowsParams.Count));
        for (int index = 0; index < tableData.gridRowsParams.Count; ++index)
        {
          if (tableData.gridRowsParams[index] != null)
          {
            this.gridRowsParams.Add(tableData.gridRowsParams[index].Clone());
            this.gridRowsParams[index].SetOwnerTable(this);
          }
          else
            this.gridRowsParams.Add((RowColParams) null);
        }
      }
      else
        this.SetGridRowsParams((List<RowColParams>) null);
      if (tableData.gridColumnsParams != null && (tableData.overrideFlags2 & OverrideFlags2.ParentGrid) != OverrideFlags2.None)
      {
        this.SetGridColumnsParams(new List<RowColParams>(tableData.gridColumnsParams.Count), false, false);
        for (int index = 0; index < tableData.gridColumnsParams.Count; ++index)
        {
          if (tableData.gridColumnsParams[index] != null)
          {
            this.gridColumnsParams.Add(tableData.gridColumnsParams[index].Clone());
            this.gridColumnsParams[index].SetOwnerTable(this);
          }
        }
      }
      else
        this.SetGridColumnsParams((List<RowColParams>) null, false, false);
    }
    if (!templateClone || !this.needUpdateLayoutFlag || !tableData.IsTopLevelTable)
      return;
    this.UpdateLayout(false);
  }

  /// <summary>Восстановить сохраненные ссылки.
  /// Сохраняются и восстанавливаются поля [ExternalLink]</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки.
  /// Если объект на который есть ссылка не копировался, то ссылка остается null</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  public override void RestoreLinks(
    bool copyChildren,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    base.RestoreLinks(copyChildren, templateClone, externalLink, links);
    if (!externalLink || this.parentFlow == null && this.prevFlowElement == null && this.nextFlowElement == null && this.flowID == null)
      return;
    TableData link = (TableData) links[(object) this];
    if (link == null)
      return;
    if (this.parentFlow != null)
      link.parentFlow = (IParentFlow) links[(object) this.parentFlow];
    if (this.prevFlowElement != null)
      link.prevFlowElement = (IFlowElement) links[(object) this.prevFlowElement];
    if (this.nextFlowElement != null)
      link.nextFlowElement = (IFlowElement) links[(object) this.nextFlowElement];
    if (this.flowID == null || !(links.Contains((object) this.flowID) | templateClone))
      return;
    FlowID flowId = (FlowID) links[(object) this.flowID];
    if (((flowId != null ? 0 : (link.flowID == null ? 1 : 0)) & (templateClone ? 1 : 0)) != 0)
    {
      flowId = this.flowID.Clone();
      flowId.TemplateFlowID = this.flowID;
    }
    link.flowID = flowId;
  }

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы-данные в таблицах</param>
  /// <returns>Копия узла</returns>
  public override DocumentTreeNode CloneFromTemplate(bool copyChildren, bool copyDataNodes)
  {
    if (!this.IsTopLevelTable)
      return base.CloneFromTemplate(copyChildren, copyDataNodes);
    TableData tableData = (TableData) base.CloneFromTemplate(copyChildren, copyDataNodes);
    tableData.UpdateLayout(false);
    return (DocumentTreeNode) tableData;
  }

  /// <summary>Буфер разбивки элементов по страницам</summary>
  internal List<RectangleElement> DistributeBuffer => this.distributeBuffer;

  public IEnumerator<RectangleElement> GetEnumerator() => this.GetDataEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetDataEnumerator();

  public static int DistributeAlgorithmVersion { get; set; } = 2;

  /// <summary>Обновить представление данных</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI)
  {
    if (!this.needUpdateLayoutFlag || this.IsVirtualNode || this.IsDistributing || this.SuspendedUpdateLayoutFlag)
      return;
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
    {
      parentCell.UpdateLayout(updateUI);
    }
    else
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument != null && this.page != null && (this.IsPageFlow || this.page.AutoSize))
      {
        if (this.prevCell != null && this.prevCell.NeedUpdateLayoutFlag && this.page.Index > 0)
          ownerDocument.UpdateLayout(this.page.Index - 1, false, updateUI);
        else
          ownerDocument.UpdateLayout(this.page.Index, false, updateUI);
      }
      else
        this.Distribute(new DistributeContext(), updateUI);
    }
  }

  /// <summary>Вызывает разбивку по страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void Distribute(DistributeContext context, bool updateUI)
  {
    if (this.IsDistributing || this.SuspendedUpdateLayoutFlag || this.IsVirtualNode || !this.IsTopLevelTable || this.prevCell != null && this.prevCell.Page == this.page)
      return;
    if (TableData.DistributeAlgorithmVersion == 1)
    {
      context.NewSize = this.Size;
      context.MaxSize = new SizeF(TableData.UnconstrainedSize, this.RealMaxHeight);
      context.CanDistributeTopTable = this.CanVerticalDistribute();
      this.DistributeTableOld(context, updateUI);
    }
    else
      this.DistributeTable(context, updateUI);
  }

  /// <summary>Распределить данные потока таблицы</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновить пользовательский интерфейс после разбивки</param>
  public virtual void DistributeTable(DistributeContext context, bool updateUI)
  {
    if (this.IsTopLevelTable)
    {
      context.NewSize = this.Size;
      context.MaxSize = new SizeF(TableData.UnconstrainedSize, this.RealMaxHeight);
      if (context.MoveTailToFinalPage && this.NextTable != null && this.page.NextPage != null && this.page.NextPage.IsFinalPage)
        context.MaxSize.Height = this.NextTable.RealMaxHeight;
      context.CanDistributeTopTable = this.CanVerticalDistribute();
    }
    context.VertDistributed = DistributeResult.All;
    context.TryNotBreak |= this.tryNotBreak;
    this.AllocateOnlyHeaders = false;
    if (this.IsTopLevelTable)
      context.CanDistributeTopTable = this.CanVerticalDistribute();
    if (this.IsVirtualNode || this.page == null)
      return;
    if (!this.IsDistributing)
      this.BeginDistribute();
    this.OnBeforeDistribute();
    if (this.IsTopLevelTable)
    {
      Monitor.Enter((object) this.nodes);
      this.AutoSizeCells((List<RowColParams>) null);
    }
    if (this.IsFixedStructureArea)
    {
      TableData.AlignChildElements((VisualNode) this);
      context.NewSize = this.Size;
    }
    try
    {
      this.BeginChangingStructure();
      float height = context.MaxSize.Height;
      try
      {
        if ((double) this.cellsMinHeight > (double) this.minHeight)
          this.cellsMinHeight = this.minHeight;
        context.HeaderCount = 0;
        context.Template = (RectangleElement) this.GetTableStructureTemplate();
        this.AssignBounds(this.Location, context.NewSize, false, false, false);
        float skipSizeAfter = this.SkipSizeAfter;
        if ((double) skipSizeAfter != 0.0)
        {
          context.MaxSize.Height -= skipSizeAfter;
          context.SkipSizeAfter = skipSizeAfter;
        }
        if (this.UsePreviousTableTemplates && this.IsTemplate && this.IsHeaderCell)
        {
          this.Clear(false, false);
        }
        else
        {
          context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
          context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
          if (!this.IsVisibleNow && this.IsFirstInFlow)
            this.UniteTable();
          if (!this.IsFixedStructureArea)
          {
            this.OrderGrid(context);
            this.DistributeHeaders(context);
          }
          if (this.IsVisibleNow)
            this.DistributeData(context);
        }
        int count = this.nodes.Count;
        if (context.VertDistributed != DistributeResult.BackToPrevious)
        {
          if (count > 0)
            this.InternalDistributeTableAdjustCellSizes(context);
        }
      }
      finally
      {
        if (!context.FirstPass)
          this.NeedSecondLayoutPass = false;
        context.MaxSize.Height = height;
        if (context.VertDistributed != DistributeResult.BackToPrevious && context.VertDistributed != DistributeResult.None)
        {
          this.AssignNeedUpdateLayoutFlag(false);
          this.EndChangingStructure(false, false, false, false);
          this.EndDistribute(true);
          if (updateUI)
            this.UpdateUIGeometry(updateUI);
        }
        else
        {
          this.EndChangingStructure(false, false, false, false);
          this.EndDistribute(true);
          if (updateUI)
            this.UpdateUIGeometry(updateUI);
        }
      }
    }
    finally
    {
      if (this.IsTopLevelTable)
        Monitor.Exit((object) this.nodes);
    }
    if (this.IsTemplate || !this.IsTopLevelTable || this.NextTable == null || this.Page != this.NextTable.Page)
      return;
    this.NextTable.DistributeTable(new DistributeContext((DocumentTreeNode) this.NextTable, context.Force)
    {
      FirstPass = context.FirstPass
    }, updateUI);
  }

  /// <summary>Упорядочить заголовки таблицы</summary>
  /// <param name="context">Контекст разбивки</param>
  protected virtual void DistributeHeaders(DistributeContext context)
  {
    lock (this.nodes)
    {
      context.HeaderCount = 0;
      if (this.IsVirtualNode)
        return;
      bool flag1 = this.PrevTable != null && this.PrevTable.AllocateOnlyHeaders;
      if (flag1 && this.PrevTable.PrevTable != null && this.PrevTable.PrevTable.AllocateOnlyHeaders)
      {
        for (int index = this.nodes.Count - 1; index >= 0; --index)
        {
          if (this.Nodes[index] is RectangleElement node && node.TableCellType != CellType.DataCell)
            node.Remove(false, false);
        }
      }
      else
      {
        int num = 0;
        bool isFirstInFlow = this.IsFirstInFlow;
        bool flag2 = this.isColumn || this.GridColumnsParams == null;
        if (((context.Force ? 1 : (this.needUpdateLayoutFlag ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
        {
          if (context.Template != null)
          {
            TableData tableData = context.Template as TableData;
            if (this.UsePreviousTableTemplates)
              tableData = this.GetTableStructureTemplate();
            context.HeaderCount = 0;
            int count1 = this.nodes.Count;
            num = 0;
            while (num < count1 && (!(this.nodes[num] is RectangleElement node1) || node1.Template == null && node1.TableCellType != CellType.DataCell))
              ++num;
            int count2 = tableData.Nodes.Count;
            for (int index1 = 0; index1 < count2; ++index1)
            {
              RectangleElement headerTemplate = tableData.Nodes[index1] as RectangleElement;
              if (headerTemplate != null && headerTemplate.TableCellType != CellType.DataCell)
              {
                int index2 = this.nodes.FindIndex(num, (Predicate<DocumentTreeNode>) (n =>
                {
                  if (!(n is RectangleElement rectangleElement2))
                    return false;
                  if (rectangleElement2.Template == headerTemplate)
                    return true;
                  return !string.IsNullOrEmpty(rectangleElement2.OverrideTemplateId) && rectangleElement2.OverrideTemplateId == headerTemplate.OverrideTemplateId;
                }));
                RectangleElement rectangleElement3 = index2 == -1 ? (RectangleElement) null : this.nodes[index2] as RectangleElement;
                if (flag1 && this.prevCell.Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (h => h.TemplateId == headerTemplate.Id)))
                {
                  rectangleElement3?.Remove(false, false);
                }
                else
                {
                  bool flag3 = false;
                  if (rectangleElement3 == null && headerTemplate.CloneByTemplateWithParent && this.HeaderIsNeed(isFirstInFlow, headerTemplate.HeaderShowType) && !this.HeaderIsDisabled(headerTemplate.Id) && !this.Page.IsNextToAdditionalPage)
                  {
                    rectangleElement3 = headerTemplate.CloneFromTemplate(true, true) as RectangleElement;
                    flag3 = true;
                  }
                  if (rectangleElement3 != null)
                  {
                    if (this.HeaderIsNeed(isFirstInFlow, rectangleElement3.HeaderShowType) && (flag3 || !this.HeaderIsDisabled(rectangleElement3.TemplateId)))
                    {
                      this.InsertChildNode(num++, (DocumentTreeNode) rectangleElement3, false, true, false, false, false);
                      if (flag3)
                        rectangleElement3.UpdateNodeLinks(true, true, false, false);
                      RectangleElement overrideTemplate = this.FindOverrideTemplate(rectangleElement3);
                      if (overrideTemplate != null && rectangleElement3.Template != overrideTemplate)
                        rectangleElement3.AssignTemplate((DocumentTreeNode) overrideTemplate, true, false, false);
                      if (index2 != -1)
                      {
                        for (int index3 = index2; index3 < this.nodes.Count && this.nodes[index3] is RectangleElement node2 && node2.Template == null && node2.TableCellType != CellType.DataCell; ++index3)
                          this.InsertChildNode(num++, (DocumentTreeNode) node2, false, true, false, false, false);
                      }
                    }
                    else
                      rectangleElement3.Remove(false, false);
                  }
                }
              }
            }
          }
          for (int index = num; index < this.nodes.Count; ++index)
          {
            if (this.Nodes[index] is RectangleElement node && node.TableCellType != CellType.DataCell)
            {
              if (!this.HeaderIsNeed(isFirstInFlow, node.HeaderShowType) || this.HeaderIsDisabled(node.TemplateId))
              {
                node.Remove(false, false);
              }
              else
              {
                if (num != index)
                  this.InsertChildNode(num, (DocumentTreeNode) node, false, true, false, false, false);
                ++num;
              }
            }
          }
          context.HeaderCount = num;
        }
        else
          context.HeaderCount = flag2 ? this.CalcFirstHeaderCount() : 0;
        if (context.HeaderCount <= 0)
          return;
        this.InternalDistributeHeaderCells(context, context.HeaderCount);
      }
    }
  }

  private RectangleElement FindOverrideTemplate(RectangleElement cellByTemplate)
  {
    if (this.IsTemplate || string.IsNullOrEmpty(cellByTemplate.OverrideTemplateId))
      return (RectangleElement) null;
    for (TableData tableData = this; tableData != null; tableData = tableData.ParentCell)
    {
      if (tableData.Template is TableData template && (!string.IsNullOrEmpty(template.OverrideTemplateId) || template.IsTopLevelTable))
      {
        RectangleElement overrideTemplate = template.NodesRecursive.OfType<RectangleElement>().FirstOrDefault<RectangleElement>((Func<RectangleElement, bool>) (n => n.OverrideTemplateId == cellByTemplate.OverrideTemplateId));
        if (overrideTemplate != null)
          return overrideTemplate;
      }
    }
    return (RectangleElement) null;
  }

  /// <summary>Распределить данные по таблицам и страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  protected virtual void DistributeData(DistributeContext context)
  {
    context.VertDistributed = DistributeResult.All;
    if (this.IsVirtualNode)
      return;
    this.PrepareDistributeDataTableContext(context);
    DistributeContext cellContext1 = (DistributeContext) null;
    DistributeDataEnumerator dataCellEnumerator = this.CreateDistributeDataCellEnumerator(context);
    DistributeContext cellContext2;
    while (true)
    {
      cellContext2 = this.TryDistributeCells(dataCellEnumerator, context, cellContext1);
      CellWithPosition cellWithPosition = (CellWithPosition) null;
      if (cellContext2 != null && cellContext2.VertDistributed != DistributeResult.BackToPrevious)
        cellWithPosition = this.HandleDynamicHeadersAtEndDistributeDataCellIteration(context);
      if (cellWithPosition != null)
      {
        dataCellEnumerator.CurrentCellPosition = cellWithPosition;
        cellContext1 = cellWithPosition.ContextState?.PrevCellContext;
      }
      else
        break;
    }
    this.HandleLastCellDistributeResult(dataCellEnumerator.Current, context, cellContext2);
    this.UpdateTryNotBreakFlagByDistributeDataCellContext(context);
    this.MoveMisplacedCellsToNextPage(context);
  }

  private DistributeContext TryDistributeCells(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    DistributeContext cellContext)
  {
    while (dataFlow.MoveNext())
    {
      if (cellContext == null || cellContext.VertDistributed != DistributeResult.Deleted)
        context.PrevCellContext = cellContext;
      bool isFirstDataCell = context.CurrentCellIndex - context.HeaderCount == 0;
      cellContext = this.CreateDistributeCellContext(dataFlow, context, isFirstDataCell);
      if (!this.CheckDistributeCurrentCellForThisTable(dataFlow, context, isFirstDataCell))
      {
        cellContext.VertDistributed = DistributeResult.None;
        break;
      }
      bool flag1 = dataFlow.Cell.Parent != this || dataFlow.IsCellFromBuffer || context.CurrentCellIndex != dataFlow.SourceIndex;
      if (flag1)
      {
        this.CheckDistributingCellAndRemoveIfEmpty(dataFlow, cellContext);
        if (cellContext.VertDistributed != DistributeResult.Deleted)
          this.MoveDistributeCell(dataFlow, context);
        else
          continue;
      }
      this.HandleDynamicGroupHeaderAtStartDistributeDataCell(context, cellContext, dataFlow);
      if (cellContext.VertDistributed != DistributeResult.Deleted)
      {
        TableData cell = dataFlow.Cell as TableData;
        float oldSkipCellsAfter = this.InternalDistributeDataResetSkipCellsFlags(context, cellContext, dataFlow, cell, context.CurrentCellIndex);
        if (flag1)
          this.RecalcFreeSpaceForTableHierarchy(context);
        if (this.isColumn && context.CurrentCellIndex > 0 && !this.isFixedStructureArea)
          this.InternalDistributeDataRestorePrevCellBounds(context);
        TableData.InternalDistributeDataCalcRelativeCellWidth(dataFlow, context);
        if (dataFlow.Cell.IsVisibleNow)
        {
          bool flag2 = context.Force || dataFlow.Cell.NeedUpdateLayoutFlag || context.CanVerticalSplit && (double) dataFlow.Cell.bounds.Bottom > (double) context.DistributeBounds.Bottom || cell != null && cell.NeedSecondLayoutPass;
          if (this.needUpdateLayoutFlag | flag2 || (double) dataFlow.Cell.bounds.Height != (double) dataFlow.Cell.MinHeight)
          {
            int num1 = flag2 || dataFlow.Cell.NextCell != null || dataFlow.Cell is ContainerData || !dataFlow.Cell.IsSingleCell || dataFlow.Cell.NeedUpdateMinHeight ? 1 : (dataFlow.Cell.NeedUpdateMinWidth ? 1 : 0);
            float cellMinHeight;
            SizeF cellSize = this.PrepareCellBoundsForDistribute(dataFlow, context, isFirstDataCell, cellContext, out cellMinHeight);
            int num2 = (double) cellMinHeight > (double) cellSize.Height || (double) dataFlow.Cell.bounds.Width != (double) cellSize.Width ? 1 : (!this.IsFixedStructureArea ? 0 : ((double) dataFlow.Cell.bounds.Height > (double) cellContext.MaxSize.Height ? 1 : 0));
            if ((num1 | num2) != 0)
            {
              this.InternalDistributeDataCell(dataFlow, cell, context, cellContext);
              if (cellContext.VertDistributed == DistributeResult.Deleted)
                continue;
            }
            else
              this.UpdateCellSizesWithoutDistributeCell(cellMinHeight, dataFlow, cellSize, context, cell, cellContext);
            if (cell != null)
              cell.distributingCount = 0;
          }
        }
        this.RestoreSkipCellsAfterDistributeDataCell(dataFlow, cellContext, oldSkipCellsAfter);
        if (!this.InternalCheckDistributeDataCellResult(dataFlow, context, cellContext))
          break;
      }
    }
    this.RemoveEmptyNextTableTail(dataFlow.CurrentCellPosition);
    return cellContext;
  }

  private CellWithPosition HandleDynamicHeadersAtEndDistributeDataCellIteration(
    DistributeContext context)
  {
    if (context.CurrentDynamicHeaderPosition == null || context.DynamicHeaderGroupRowCount < 0 || context.DynamicHeaderGroupRowCount >= this.MinRowsForDynamicHeaderGroup)
      return (CellWithPosition) null;
    context.SecondDynamicHeaderIteration = true;
    if (!context.TryNotBreak_Failed)
      this.ResetTryNotBreadFailedFlagsRecursive();
    this.InternalDistributeDataReleaseBuffer(context);
    CellWithPosition dynamicHeaderPosition = context.CurrentDynamicHeaderPosition;
    context.CurrentDynamicHeaderPosition = (CellWithPosition) null;
    context.DynamicHeaderGroupRowCount = 0;
    context.SetCurrentPositionState(dynamicHeaderPosition.ContextState);
    if (dynamicHeaderPosition.Cell != null)
    {
      context.CurrentCellIndex = dynamicHeaderPosition.Cell.Index;
      dynamicHeaderPosition.Cell?.Remove(false, false, false);
      dynamicHeaderPosition.Cell = dynamicHeaderPosition.ContextState?.PrevCellContext?.OwnerNode as RectangleElement;
      dynamicHeaderPosition.SourceIndex = context.CurrentCellIndex - 1;
      dynamicHeaderPosition.SourceTable = this;
      dynamicHeaderPosition.BufferIndex = int.MaxValue;
    }
    return dynamicHeaderPosition;
  }

  /// <summary>Обработать динамические заголовки групп.
  /// Внимание, метод может добавлять и удалять ячейки со смещением текущей позиции в энумераторе dataFlow</summary>
  /// <param name="context"></param>
  /// <param name="dataFlow"></param>
  private void HandleDynamicGroupHeaderAtStartDistributeDataCell(
    DistributeContext context,
    DistributeContext cellContext,
    DistributeDataEnumerator dataFlow)
  {
    if (this.IsTemplate)
      return;
    string groupHeaderText = dataFlow.Cell.GroupHeaderText;
    this.UpdateDynamicGroupHeaderInDistributePosition(context, cellContext, dataFlow, groupHeaderText);
    if (dataFlow.Cell == null)
      return;
    TableData.UpdateDynamicHeaderTextForCell(context, dataFlow, groupHeaderText);
  }

  /// <summary>Обновить текст для ячейки в сгруппированной записи в зависимости от результата группировки</summary>
  private static void UpdateDynamicHeaderTextForCell(
    DistributeContext context,
    DistributeDataEnumerator dataFlow,
    string cellDynamicHeaderText)
  {
    if (string.IsNullOrEmpty(cellDynamicHeaderText) || dataFlow.Cell.IsDynamicGroupHeader)
      return;
    if (context.CurrentDynamicHeaderPosition != null)
      dataFlow.Cell.GroupCellText = dataFlow.Cell.GroupCellTextForGroup;
    else
      dataFlow.Cell.GroupCellText = dataFlow.Cell.GroupCellOriginalText;
  }

  /// <summary>Проверить нужен ли в этой точке разбивки динамический заголовок и вставить/удалить его</summary>
  private void UpdateDynamicGroupHeaderInDistributePosition(
    DistributeContext context,
    DistributeContext cellContext,
    DistributeDataEnumerator dataFlow,
    string cellDynamicHeaderText)
  {
    if (context.CurrentDynamicHeaderPosition != null)
    {
      if (string.IsNullOrEmpty(cellDynamicHeaderText) || context.CurrentDynamicHeaderPosition.Cell.GroupHeaderText != cellDynamicHeaderText)
      {
        context.CurrentDynamicHeaderPosition = (CellWithPosition) null;
        context.DynamicHeaderGroupRowCount = 0;
      }
      else if (dataFlow.Cell.IsDynamicGroupHeader)
      {
        dataFlow.RemoveCurrentCellFromDataFlow();
        cellContext.VertDistributed = DistributeResult.Deleted;
      }
    }
    if (context.CurrentDynamicHeaderPosition != null || string.IsNullOrEmpty(cellDynamicHeaderText))
      return;
    if (context.SecondDynamicHeaderIteration)
    {
      if (dataFlow.Cell.IsDynamicGroupHeader)
        TableData.SetDynamicHeaderPosition(context, dataFlow);
    }
    else if (this.NeedCreateDynamicGroupHeaderForNextCells(context, dataFlow, ref cellDynamicHeaderText))
    {
      if (dataFlow.Cell.IsDynamicGroupHeader)
      {
        TableData.SetDynamicHeaderPosition(context, dataFlow);
      }
      else
      {
        RectangleElement dynamicHeader = this.CreateDynamicHeader(cellDynamicHeaderText);
        if (dynamicHeader != null)
        {
          if (!dataFlow.SourceTable.distributeBuffer.IsEmpty<RectangleElement>() && context.LastCellFromBuffer == dataFlow.Cell.Index)
          {
            if (dataFlow.Cell.Index < this.nodes.Count)
              this.RemoveChildNodeAt(dataFlow.Cell.Index, true, false, false);
            dataFlow.SourceTable.distributeBuffer.Add(dataFlow.Cell);
            context.LastCellFromBuffer = -1;
          }
          this.InsertChildNode(context.CurrentCellIndex, (DocumentTreeNode) dynamicHeader, false, false, false, false, true);
          CellWithPosition cellWithPosition = new CellWithPosition(dynamicHeader, this, context.CurrentCellIndex, int.MaxValue, false, false)
          {
            ContextState = context.GetCurrentPositionState()
          };
          context.CurrentDynamicHeaderPosition = cellWithPosition;
          dataFlow.CurrentCellPosition = context.CurrentDynamicHeaderPosition;
        }
      }
    }
    else if (dataFlow.Cell.IsDynamicGroupHeader)
    {
      dataFlow.RemoveCurrentCellFromDataFlow();
      cellContext.VertDistributed = DistributeResult.Deleted;
    }
    context.DynamicHeaderGroupRowCount = 0;
  }

  private static void SetDynamicHeaderPosition(
    DistributeContext context,
    DistributeDataEnumerator dataFlow)
  {
    context.CurrentDynamicHeaderPosition = dataFlow.CurrentCellPosition;
    context.CurrentDynamicHeaderPosition.ContextState = context.GetCurrentPositionState();
  }

  private bool NeedCreateDynamicGroupHeaderForNextCells(
    DistributeContext context,
    DistributeDataEnumerator dataFlow,
    ref string cellDynamicHeaderText)
  {
    if (!this.OwnerDocument.DynamicGroupHeaderIsEnabled)
      return false;
    CellWithPosition cellPosition = dataFlow.CurrentCellPosition;
    if (dataFlow.CurrentCellPosition.Cell.IsDynamicGroupHeader)
    {
      CellWithPosition currentCellPosition = dataFlow.CurrentCellPosition;
      cellPosition = dataFlow.GetNextDataCellForDistribute(currentCellPosition);
      if (cellPosition?.Cell == null || string.IsNullOrEmpty(cellPosition.Cell.GroupHeaderText) || cellPosition.Cell.IsDynamicGroupHeader)
        return false;
      if (cellPosition.Cell.GroupHeaderText != cellDynamicHeaderText)
      {
        cellDynamicHeaderText = cellPosition.Cell.GroupHeaderText;
        currentCellPosition.Cell.GroupHeaderText = cellDynamicHeaderText;
      }
    }
    int num = 0;
    while (num < this.MinRowsForDynamicHeaderGroup)
    {
      if (!cellPosition.Cell.IsDynamicGroupHeader)
        ++num;
      cellPosition = dataFlow.GetNextDataCellForDistribute(cellPosition);
      if (cellPosition?.Cell == null || cellPosition.Cell.GroupHeaderText != cellDynamicHeaderText)
        break;
    }
    return num >= this.MinRowsForDynamicHeaderGroup;
  }

  private RectangleElement GetDynamicHeaderTemplate()
  {
    if (this.IsTemplate)
      return (RectangleElement) null;
    string attributeValue = this.GetAttributeValue("GroupHeaderTemplate", true);
    if (string.IsNullOrEmpty(attributeValue) && this.PrevCell != null)
      attributeValue = this.FindFirstCell().GetAttributeValue("GroupHeaderTemplate", true);
    return !string.IsNullOrEmpty(attributeValue) ? (RectangleElement) this.OwnerDocument.Template.FindNode(attributeValue) : (RectangleElement) null;
  }

  private RectangleElement CreateDynamicHeader(string cellDynamicHeaderText)
  {
    RectangleElement dynamicHeaderTemplate = this.GetDynamicHeaderTemplate();
    if (dynamicHeaderTemplate == null)
      return (RectangleElement) null;
    RectangleElement dynamicHeader = (RectangleElement) dynamicHeaderTemplate.CloneFromTemplate(true, true);
    dynamicHeader.SetAttributeValue("GroupHeaderText", cellDynamicHeaderText, false, false, false);
    dynamicHeader.SetAttributeValue("GroupHeader", "1", false, false, false);
    return dynamicHeader;
  }

  private void MoveMisplacedCellsToNextPage(DistributeContext context)
  {
    if (context.CanVerticalSplit && (context.VertDistributed == DistributeResult.Part || context.VertDistributed == DistributeResult.None && this.IsTopLevelTable))
    {
      this.InternalDistributeDataProcessKeepWithNext(context);
      if (this.InternalDistributeDataCreateNextPageIfNeed(context) && !this.InternalDistributeDataIsNextCellFitInNewPage(context))
      {
        context.InsufficientPageSize = true;
        this.nextCell.Page.Remove(false, false, false);
        this.SetNextCell((RectangleElement) null);
      }
      TableData nextTable = this.NextTable;
      if (context.CanVerticalDistribute && nextTable != null && (this.distributeBuffer != null && this.distributeBuffer.Count > 0 || context.HeaderCount + context.DataCellCount < this.nodes.Count))
      {
        nextTable.SetNeedUpdateLayoutFlag(true, false, false, false);
        if (this.distributeBuffer != null)
        {
          for (int index = 0; index < this.distributeBuffer.Count; ++index)
          {
            if (this.distributeBuffer[index].NextCell != null && this.distributeBuffer[index].NextCell.Page == nextTable.Page)
              this.distributeBuffer[index].OneStepUniteTable();
          }
        }
        this.InternalDistributeDataMoveExcessiveCells(context, nextTable, context.HeaderCount);
        this.InternalDistributeDataMoveBufferToNextTable(nextTable);
        if (this.isColumn && !this.isFixedStructureArea && this.nodes.Count > 0 && this.nodes[this.nodes.Count - 1] is RectangleElement node)
          node.UpdateBoundsSkipAfter();
      }
    }
    this.InternalDistributeDataReleaseBuffer(context);
  }

  private void UpdateCellSizesWithoutDistributeCell(
    float cellMinHeight,
    DistributeDataEnumerator dataFlow,
    SizeF cellSize,
    DistributeContext context,
    TableData cellTable,
    DistributeContext cellContext)
  {
    if ((double) cellMinHeight < (double) dataFlow.Cell.ContentHeight)
      cellMinHeight = dataFlow.Cell.ContentHeight;
    if ((double) cellSize.Height < (double) cellMinHeight)
      cellSize.Height = cellMinHeight;
    if (this.IsRow && (double) context.MinRowSize > 0.0 && (double) cellSize.Height < (double) context.MinRowSize)
    {
      cellSize.Height = context.MinRowSize;
      if ((double) cellMinHeight < (double) context.MinRowSize)
        cellMinHeight = context.MinRowSize;
    }
    if ((double) cellMinHeight == 0.0)
      cellMinHeight = cellSize.Height;
    if ((double) cellMinHeight == (double) dataFlow.Cell.bounds.Height)
      return;
    if ((double) cellMinHeight >= 0.0 && (cellTable != null || (double) dataFlow.Cell.bounds.Height < (double) cellMinHeight || (double) cellSize.Height > (double) dataFlow.Cell.ContentHeight || (double) dataFlow.Cell.bounds.Height - (double) dataFlow.Cell.ContentHeight > 9.9999997473787516E-06))
    {
      cellSize.Height = dataFlow.Cell.ContentHeight;
      if ((double) cellSize.Height < (double) cellMinHeight)
        cellSize.Height = cellMinHeight;
      dataFlow.Cell.SetCellSizes(new RectangleF(dataFlow.Cell.bounds.Location, cellSize), true, false, false, false);
    }
    if (dataFlow.Cell.NextCell == null)
      return;
    cellContext.VertDistributed = DistributeResult.Part;
  }

  private static void UpdateContextTableBounds(
    DistributeDataEnumerator dataFlow,
    DistributeContext context)
  {
    if (dataFlow.Cell == null || (double) context.CalculatedForCellsProperBounds.Bottom >= (double) dataFlow.Cell.bounds.Bottom)
      return;
    RectangleF cellsProperBounds = context.CalculatedForCellsProperBounds with
    {
      Height = dataFlow.Cell.bounds.Bottom - context.CalculatedForCellsProperBounds.Y
    };
    context.CalculatedForCellsProperBounds = cellsProperBounds;
  }

  private void InternalDistributeDataCell(
    DistributeDataEnumerator dataFlow,
    TableData cellTable,
    DistributeContext context,
    DistributeContext cellContext)
  {
    if (cellTable != null)
    {
      cellTable.DistributeTable(cellContext, false);
    }
    else
    {
      dataFlow.Cell.DistributeCell(cellContext);
      if (dataFlow.Cell.NextCell != null)
      {
        bool updateLayoutFlag = dataFlow.Cell.NeedUpdateLayoutFlag;
        dataFlow.Cell.NextCell.SetNeedUpdateLayoutFlag(true, false, false, false);
        if (this.TopLevelTable.NextCell != null)
          this.TopLevelTable.NextCell.SetNeedUpdateLayoutFlag(true, false, false, false);
        dataFlow.Cell.SetNeedUpdateLayoutFlag(updateLayoutFlag, false, false, false);
      }
    }
    this.RemoveCellIfIsEmptyTail(dataFlow, cellContext);
    TableData.UpdateContextTableBounds(dataFlow, context);
    context.TryNotBreak_Failed |= cellContext.TryNotBreak_Failed;
  }

  /// <summary>Рассчитать размер ячейки в перед запуском её разбивки.
  /// Меняет положение, и, возможно, размеры</summary>
  private SizeF PrepareCellBoundsForDistribute(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    bool isFirstDataCell,
    DistributeContext cellContext,
    out float cellMinHeight)
  {
    this.InternalDistributeDataUpdateCellLocation(context, dataFlow);
    SizeF sizeF = this.InternalDistributeDataCalcPreferredCellSize(dataFlow, context, out cellMinHeight);
    cellContext.NewSize = sizeF;
    if (context.FirstDataOnPage & isFirstDataCell && (double) dataFlow.Cell.SkipCellsBefore != 0.0)
      this.InternalDistributeDataAdjustSkipLinesBeforeIfFirstCellOnPage(dataFlow, cellMinHeight);
    if (this.IsFixedStructureArea)
      cellContext.MaxSize.Height = context.DistributeBounds.Bottom - dataFlow.Cell.Bounds.Y;
    return sizeF;
  }

  /// <summary>
  /// Распределить ячейки заголовка,  пересчитать координаты, обновить геометрию
  /// </summary>
  /// <param name="context">контекст распределения ячеек всей таблицы</param>
  /// <param name="headerCount">количество ячеек заголовка</param>
  private void InternalDistributeHeaderCells(DistributeContext context, int headerCount)
  {
    RectangleF prevBounds = new RectangleF(this.ProperLocation, new SizeF(0.0f, 0.0f));
    List<RowColParams> gridColumnsParams = this.GridColumnsParams;
    List<RowColParams> gridRowsParams = this.GridRowsParams;
    RowColParams thisColParams = (RowColParams) null;
    RowColParams thisRowParams = (RowColParams) null;
    RectangleF properBounds = this.ProperBounds with
    {
      Height = this.minHeight
    };
    SizeF maxSize = new SizeF(TableData.UnconstrainedSize, this.RealMaxHeight);
    float num = 0.0f;
    if (this.IsRow)
    {
      float rowSizeNn = context.RowSize_NN;
      num = (double) rowSizeNn == 0.0 ? this.GetMinRowSize(this.GridRowsParams) : rowSizeNn;
    }
    for (int index = 0; index < headerCount; ++index)
    {
      RectangleElement node1 = this.nodes[index] as RectangleElement;
      TableData node2 = this.nodes[index] as TableData;
      if (node1 != null && node1.IsVisibleNow)
      {
        if (this.isColumn && node1.NonSkipBeforeAtStartPage && !this.isFixedStructureArea && (double) node1.SkipCellsBefore != 0.0 && context.FirstOnPage && index == 0)
        {
          node1.overrideFlags3 |= OverrideFlags3.IgnoreSkipBefore;
          node1.setProperBounds(new RectangleF(node1.bounds.Location, node1.properBounds.Size));
          node1.setBounds(new RectangleF(node1.bounds.Location, node1.CalcSizeFromProper(node1.properBounds.Size)));
        }
        else
          node1.overrideFlags3 &= ~OverrideFlags3.IgnoreSkipBefore;
        PointF pointF = this.CalcCellLocation(prevBounds, node1);
        RectangleF bounds = node1.Bounds;
        if (bounds.Location != pointF)
        {
          if (node2 != null)
            node2.RecalcCellLocations(pointF, 0, node1.Nodes.Count, false, false, false);
          else
            node1.AssignBoundsOnly(bounds, new RectangleF(pointF, bounds.Size));
        }
        SizeF newSize = this.CalcCellSize(node1, properBounds.Size, gridRowsParams, out thisRowParams, gridColumnsParams, out thisColParams, false);
        float minHeight = node1.MinHeight;
        if ((double) newSize.Height < (double) minHeight)
          newSize.Height = minHeight;
        if (this.IsRow && (double) num > 0.0 && (double) newSize.Height < (double) num)
          newSize.Height = num;
        DistributeContext context1 = new DistributeContext((DocumentTreeNode) node1, newSize, maxSize, index == 0 || !this.isColumn, true, context);
        if (node2 != null)
          node2.DistributeTable(context1, false);
        else
          node1.DistributeCell(context1);
        if ((double) properBounds.Bottom < (double) node1.bounds.Bottom)
          properBounds.Height = node1.bounds.Bottom - properBounds.Y;
        if (this.isColumn || (double) node1.bounds.Height != 0.0)
          this.AdjustSizeToCell(node1, false, false);
        node1.ResetNeedUpdateLayoutFlag(true);
        prevBounds = node1.Bounds;
      }
    }
  }

  private void InternalDistributeTableAdjustCellSizes(DistributeContext context)
  {
    float num1 = 0.0f;
    float width = 0.0f;
    float height = 0.0f;
    bool flag = false;
    TableData parentCell = this.ParentCell;
    RectangleF rectangleF1 = parentCell == null || !parentCell.isFixedStructureArea ? this.ProperBounds : this.Bounds;
    if (!this.IsFixedStructureArea)
    {
      int visibleCellIndex = this.FindLastVisibleCellIndex();
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        {
          RectangleF bounds = node.Bounds;
          if ((double) width - ((double) bounds.Right - (double) rectangleF1.X) < -9.9999997473787516E-06)
          {
            width = (float) Math.Round((double) bounds.Right - (double) rectangleF1.X, 5);
            flag |= this.isColumn;
          }
          if ((double) height - ((double) bounds.Bottom - (double) rectangleF1.Y) < -9.9999997473787516E-06)
          {
            height = (float) Math.Round((double) bounds.Bottom - (double) rectangleF1.Y, 5);
            flag |= !this.isColumn;
          }
          RectangleF rectangleF2 = this.CalcRealProperBounds(this.properBounds);
          float num2 = (float) Math.Round((double) (node.CalcRealProperBounds(node.properBounds).Y - rectangleF2.Y + node.ContentHeight), 5);
          if (this.isColumn)
          {
            if (index == visibleCellIndex)
              num1 = num2;
          }
          else if ((double) num1 < (double) num2)
            num1 = num2;
        }
      }
      if (!this.isColumn && (double) width == 0.0)
        width = rectangleF1.Width;
      if ((double) height > (double) context.MaxSize.Height)
        height = context.MaxSize.Height;
      if ((double) height < (double) this.MinHeight)
        height = this.MinHeight;
      if ((double) Math.Abs(width - rectangleF1.Width) < -9.9999997473787516E-06)
        width = rectangleF1.Width;
      if ((double) Math.Abs(height - rectangleF1.Height) < -9.9999997473787516E-06)
        height = rectangleF1.Height;
      if (!this.isColumn && (double) height == 0.0)
      {
        flag = true;
        height = rectangleF1.Height;
      }
      rectangleF1.Size = new SizeF(width, height);
    }
    else
    {
      float num3 = float.MinValue;
      float num4 = float.MinValue;
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node && node.IsVisibleNow)
        {
          RectangleF bounds = node.Bounds;
          float num5 = bounds.Right;
          if (node.HorzAlign == ElementHorizontalAlign.Center || node.HorzAlign == ElementHorizontalAlign.Right)
            num5 = rectangleF1.X + bounds.Width;
          if (VisualNode.MoreOrEqualWithMiscalculation(num5, num3))
          {
            num3 = (float) Math.Round((double) num5, 5);
            flag |= this.isColumn;
          }
          float num6 = bounds.Bottom;
          if (node.VertAlign == ElementVerticalAlign.Center || node.VertAlign == ElementVerticalAlign.Bottom)
            num6 = rectangleF1.Y + bounds.Height;
          if (VisualNode.MoreOrEqualWithMiscalculation(num6, num4))
          {
            num4 = (float) Math.Round((double) num6, 5);
            flag |= !this.isColumn;
          }
        }
      }
      num1 = num4 - this.bounds.Y;
      if ((double) num4 - (double) rectangleF1.Y > (double) context.MaxSize.Height)
        num4 = rectangleF1.Y + context.MaxSize.Height;
      if ((double) Math.Abs(num3 - rectangleF1.X - rectangleF1.Width) < -9.9999997473787516E-06)
        num3 = rectangleF1.Right;
      if ((double) Math.Abs(this.maxHeight - rectangleF1.Y - rectangleF1.Height) < -9.9999997473787516E-06)
        num4 = rectangleF1.Bottom;
      if (!this.isColumn && (double) this.maxHeight - (double) rectangleF1.Y == 0.0)
      {
        flag = true;
        num4 = rectangleF1.Bottom;
      }
      if ((double) this.bounds.Y + (double) this.minHeight > (double) num4)
        num4 = this.bounds.Y + this.minHeight;
      if (!this.AutoSizeWidth || this.IsRow)
        num3 = rectangleF1.Right;
      rectangleF1.Size = new SizeF(num3 - rectangleF1.X, num4 - rectangleF1.Y);
    }
    if (this.IsRow)
    {
      float rowSizeNn = context.RowSize_NN;
      float num7 = (double) rowSizeNn == 0.0 ? this.GetMinRowSize(this.GridRowsParams) : rowSizeNn;
      if ((double) num1 < (double) num7)
        num1 = num7;
    }
    this.cellsMinHeight = (double) num1 == 0.0 ? (this.nodes.Count <= 0 ? 0.0f : this.properBounds.Height) : num1;
    if (flag)
    {
      if (this.IsTopLevelTable && (double) this.maxHeight != 0.0 && this.IsPageFlow)
        rectangleF1.Height = this.maxHeight;
      else if (context.IsFixedSizeRow_NN && !this.IsTopLevelTable)
        rectangleF1.Height = this.RoundForFixedSizeRow(rectangleF1.Height, context.RowSize_NN, this.MinHeight);
      if (parentCell != null && parentCell.isFixedStructureArea)
        rectangleF1.Location = this.properBounds.Location;
      this.AssignProperBounds(rectangleF1, false, false, false);
      rectangleF1 = this.CalcBoundsFromProper(rectangleF1);
      if (this.IsColumn && (double) rectangleF1.Height > (double) context.MaxSize.Height)
        rectangleF1.Height = context.MaxSize.Height;
      rectangleF1 = this.SetCellSizes(rectangleF1, true, false, false, false, false);
    }
    else
    {
      if (!context.InsufficientPageSize || this.nodes.Count <= 0)
        return;
      if (this.IsColumn)
      {
        int visibleCellIndex = this.FindLastVisibleCellIndex();
        if (visibleCellIndex == -1 || !(this.nodes[visibleCellIndex] is RectangleElement node) || (double) node.bounds.Bottom <= (double) this.properBounds.Bottom)
          return;
        rectangleF1 = node.bounds with
        {
          Height = this.properBounds.Bottom - node.bounds.Y
        };
        rectangleF1 = node.SetCellSizes(rectangleF1, true, false, false, false);
      }
      else
      {
        for (int index = 0; index < this.nodes.Count; ++index)
        {
          RectangleElement node = (RectangleElement) (this.nodes[index] as TableData);
          if (node != null && (double) node.bounds.Bottom > (double) this.properBounds.Bottom)
          {
            rectangleF1 = node.bounds with
            {
              Height = this.properBounds.Bottom - node.bounds.Y
            };
            rectangleF1 = node.SetCellSizes(rectangleF1, true, false, false, false);
          }
        }
      }
    }
  }

  /// <summary>Проверить флаги TryNotBreak в контексте разбивки ячеек данных</summary>
  private void UpdateTryNotBreakFlagByDistributeDataCellContext(DistributeContext context)
  {
    if (context.VertDistributed == DistributeResult.All)
    {
      this.TryNotBreak_Failed0 = false;
      this.TryNotBreak_Failed1 = false;
      context.TryNotBreak_Failed = false;
    }
    else
    {
      if (!context.TryNotBreak || !context.CanVerticalSplit || this.IsTopLevelTable || context.VertDistributed != DistributeResult.Part || !this.IsFirstInFlow || (context.FirstDataOnPage || context.KeepWithNext_IsFirstDataOnPage) && !this.TryNotBreak_Failed0)
        return;
      if (context.TryNotBreak_Failed || this.TryNotBreak_Failed0 && this.TryNotBreak_Failed1)
      {
        this.TryNotBreak_Failed0 = false;
        this.TryNotBreak_Failed1 = false;
        context.TryNotBreak_Failed = true;
      }
      else
      {
        if (!this.TryNotBreak_Failed0)
        {
          this.TryNotBreak_Failed0 = true;
          context.VertDistributed = DistributeResult.None;
        }
        else if (!this.TryNotBreak_Failed1)
        {
          this.TryNotBreak_Failed1 = true;
          context.TryNotBreak_Failed = true;
          context.VertDistributed = DistributeResult.BackToPrevious;
          if (this.TopLevelTable.PrevCell != null)
            this.TopLevelTable.PrevCell.SetNeedUpdateLayoutFlag(true, false, false, false);
        }
        this.SetNeedUpdateLayoutFlag(true, false, false, false, true);
      }
    }
  }

  private void InternalDistributeDataMoveBufferToNextTable(TableData nextTable)
  {
    if (this.distributeBuffer == null || this.distributeBuffer.Count <= 0)
      return;
    if (nextTable.distributeBuffer == null)
      nextTable.distributeBuffer = this.distributeBuffer;
    else
      nextTable.distributeBuffer.AddRange((IEnumerable<RectangleElement>) this.distributeBuffer);
    this.distributeBuffer = (List<RectangleElement>) null;
  }

  private void InternalDistributeDataMoveExcessiveCells(
    DistributeContext context,
    TableData nextTable,
    int headerCount)
  {
    int num = 0;
    for (int index = this.nodes.Count - 1; index > headerCount + context.DataCellCount - 1; --index)
    {
      int count = this.nodes.Count;
      if (index >= count)
      {
        index = count - 1;
        LogManager.AddLine("     ---TableData.DistributeData: Invalid index ID: " + this.id, true);
      }
      RectangleElement node = this.nodes[index] as RectangleElement;
      if (node is TableData tableData && tableData.nextCell != null && tableData.nextCell.Page == this.NextTable.Page)
        tableData.OneStepUniteTable(true);
      if (this.nodes.Count != count && tableData != null)
        LogManager.AddLine($"     ---TableData.DistributeData: Invalid index ID: {this.id}, curCell.Id: {tableData.id}", true);
      if (node != null && (node.TableCellType == CellType.DataCell || !context.HeaderCellsIsAvailable))
      {
        if (index < this.nodes.Count)
          this.RemoveChildNodeAt(index, true, false, false);
        else
          LogManager.AddLine("     ---TableData.DistributeData: Try remove at invalid index ID: " + this.id, true);
        if (this.distributeBuffer != null)
        {
          if (index <= context.LastCellFromBuffer)
            this.distributeBuffer.Add(node);
          else
            this.distributeBuffer.Insert(num++, node);
        }
        else
        {
          if (nextTable.distributeBuffer == null)
            nextTable.distributeBuffer = new List<RectangleElement>(this.nodes.Count - (headerCount + context.DataCellCount));
          nextTable.distributeBuffer.Add(node);
          nextTable.SetNeedUpdateLayoutFlag(true, false, false, false);
        }
      }
    }
  }

  private void InternalDistributeDataReleaseBuffer(DistributeContext context)
  {
    if (this.distributeBuffer == null || this.distributeBuffer.Count <= 0)
      return;
    int num = context.CurrentCellIndex;
    if (context.LastCellFromBuffer != -1)
      num = context.LastCellFromBuffer + 1;
    for (int index = this.distributeBuffer.Count - 1; index >= 0; --index)
      this.InsertChildNode(num++, (DocumentTreeNode) this.distributeBuffer[index], false, true, false, false, false);
    this.distributeBuffer.Clear();
  }

  private bool InternalDistributeDataIsNextCellFitInNewPage(DistributeContext context)
  {
    bool flag = true;
    if (context.CanVerticalDistribute && this.nextCell != null && this.nextCell.Page != this.page && (this.nextCell.Page.NextPageTemplateId == null || this.nextCell.Page.NextPageTemplateId == this.nextCell.Page.TemplateId) && context.FirstDataOnPage && (context.DataCellCount == 0 || this.IsRow))
    {
      RectangleElement ownerNode = (RectangleElement) null;
      context.CurrentCellIndex = context.HeaderCount + context.DataCellCount;
      if (context.CurrentCellIndex < this.nodes.Count)
        ownerNode = this.nodes[context.CurrentCellIndex] as RectangleElement;
      else if (this.distributeBuffer != null && this.distributeBuffer.Count > 0)
        ownerNode = this.distributeBuffer[this.distributeBuffer.Count - 1];
      else if (this.nodes.Count > 0)
        ownerNode = this.nodes[this.nodes.Count - 1] as RectangleElement;
      if (!(ownerNode is TableData tableData) || !tableData.CanVerticalDistribute())
      {
        float tableFreeSpace = this.nextCell.GetTableFreeSpace();
        DistributeContext context1 = new DistributeContext((DocumentTreeNode) ownerNode, ownerNode.Size, new SizeF(this.nextCell.Size.Width, tableFreeSpace), context.CurrentCellIndex == 0 || this.IsRow, context.CurrentCellIndex - context.HeaderCount == 0 || this.IsRow, context);
        float sizeForDistribute = ownerNode.GetMinimalSizeForDistribute(context1);
        if ((double) tableFreeSpace < (double) sizeForDistribute)
          flag = false;
      }
    }
    return flag;
  }

  private bool InternalDistributeDataCreateNextPageIfNeed(DistributeContext context)
  {
    bool nextPageIfNeed = false;
    int num;
    if ((context.DataCellCount + context.HeaderCount >= this.nodes.Count || !this.nodes.Skip<DocumentTreeNode>(context.DataCellCount + context.HeaderCount).OfType<VisualNode>().Any<VisualNode>((Func<VisualNode, bool>) (n => n.Visible)) ? (this.distributeBuffer == null ? 0 : (this.distributeBuffer.Count > 0 ? 1 : 0)) : 1) != 0 && this.nextCell?.Page != this.Page)
    {
      PageData page = this.Page;
      num = page != null ? (page.IsLastAdditionalPageInChain ? 1 : 0) : 0;
    }
    else
      num = 0;
    if (this.nextCell == null | num != 0 && (context.DataCellCount > 0 || this.page.NextPageTemplateId != null && this.page.NextPageTemplateId != this.page.TemplateId))
    {
      nextPageIfNeed = this.page.NextPage == null || this.page.NextPage.IsNextToAdditionalPage;
      this.AddNewTableAndParentsInDataFlow();
      this.NextTable?.TopLevelTable.GetGridColumnsParams(true);
      if (this.page.IsFinalPage)
      {
        context.VertDistributed = DistributeResult.BackToPrevious;
        this.TopLevelTable.PrevCell?.SetNeedUpdateLayoutFlag(true, false, false, false);
      }
    }
    return nextPageIfNeed;
  }

  private void InternalDistributeDataProcessKeepWithNext(DistributeContext context)
  {
    if (context.FirstKeepWithNext == null || !(this.nodes[context.HeaderCount + context.DataCellCount - 1] as RectangleElement).keepWithNext)
      return;
    context.CurrentCellIndex = context.FirstKeepWithNext.Index;
    if (context.CurrentCellIndex >= context.HeaderCount && context.CurrentCellIndex - context.HeaderCount > 0)
    {
      context.DataCellCount = context.CurrentCellIndex - context.HeaderCount;
    }
    else
    {
      if (context.FirstDataOnPage)
        return;
      context.VertDistributed = DistributeResult.None;
      context.DataCellCount = 0;
    }
  }

  /// <summary>Уточнить результат распределения</summary>
  private void HandleLastCellDistributeResult(
    RectangleElement cell,
    DistributeContext context,
    DistributeContext cellContext)
  {
    context.DataCellCount = context.CurrentCellIndex - context.HeaderCount;
    if (cellContext != null)
    {
      if (cell != null)
      {
        if (context.DataCellCount == 0 || !this.isColumn && cellContext.VertDistributed == DistributeResult.None)
          context.VertDistributed = DistributeResult.None;
        else if (context.DataCellCount + context.HeaderCount < this.nodes.Count || this.distributeBuffer != null && this.distributeBuffer.Count > 0 || this.NextTable != null && this.NextTable.distributeBuffer != null && this.NextTable.distributeBuffer.Count > 0)
          context.VertDistributed = DistributeResult.Part;
        if (this.IsTopLevelTable && this.isColumn && context.VertDistributed == DistributeResult.None)
        {
          RectangleF bounds = cell.Bounds;
          double bottom1 = (double) bounds.Bottom;
          RectangleF distributeBounds = context.DistributeBounds;
          double bottom2 = (double) distributeBounds.Bottom;
          if (bottom1 > bottom2)
          {
            ref RectangleF local = ref bounds;
            double height = (double) local.Height;
            double bottom3 = (double) bounds.Bottom;
            distributeBounds = context.DistributeBounds;
            double bottom4 = (double) distributeBounds.Bottom;
            double num = bottom3 - bottom4;
            local.Height = (float) (height - num);
            cell.SetCellSizes(bounds, true, false, false, false);
          }
          cellContext.VertDistributed = DistributeResult.Part;
          context.DataCellCount = 1;
        }
      }
      if (cellContext.VertDistributed == DistributeResult.Part)
        context.VertDistributed = DistributeResult.Part;
      else if (cellContext.VertDistributed == DistributeResult.BackToPrevious)
        context.VertDistributed = DistributeResult.BackToPrevious;
    }
    if (!this.IsColumn || context.VertDistributed != DistributeResult.None || context.DataCellCount != 0 || context.HeaderCount <= 0 || !context.FirstDataOnPage)
      return;
    this.AllocateOnlyHeaders = true;
    context.VertDistributed = DistributeResult.Part;
  }

  /// <summary>Проверка результата разбивки</summary>
  /// <returns>true - если все хорошо и разбивку можно продолжать; false - выход из цикла</returns>
  private bool InternalCheckDistributeDataCellResult(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    DistributeContext cellContext)
  {
    if (dataFlow.Cell == null)
      return true;
    if (context.CanDistributeTopTable && !context.CanVerticalSplit && cellContext.VertDistributed != DistributeResult.All)
      return false;
    if (!context.CanVerticalSplit || cellContext.VertDistributed == DistributeResult.All || cellContext.VertDistributed == DistributeResult.Part)
    {
      if (dataFlow.Cell.IsVisibleNow)
      {
        this.AdjustSizeToCell(dataFlow.Cell, false, false);
        context.PrevCellBounds = dataFlow.Cell.Bounds;
        if (this.IsColumn)
        {
          this.FreeSpace.Height = context.DistributeBounds.Bottom - context.PrevCellBounds.Bottom;
          if (context.CanDistributeTopTable && context.CanVerticalDistribute && (double) this.FreeSpace.Height < 0.0)
          {
            cellContext.VertDistributed = DistributeResult.None;
            return false;
          }
        }
      }
      if (cellContext.VertDistributed == DistributeResult.Part)
        context.VertDistributed = DistributeResult.Part;
      ++context.CurrentCellIndex;
      if (context.CurrentDynamicHeaderPosition != null && !dataFlow.Cell.IsDynamicGroupHeader)
        ++context.DynamicHeaderGroupRowCount;
      context.TryNotBreak_Failed |= cellContext.TryNotBreak_Failed;
      if (context.FirstKeepWithNext == null && dataFlow.Cell.keepWithNext)
        context.FirstKeepWithNext = dataFlow.Cell;
      else if (context.FirstKeepWithNext != null && (!dataFlow.Cell.keepWithNext || !this.IsFixedStructureArea && cellContext.VertDistributed == DistributeResult.Part))
        context.FirstKeepWithNext = (RectangleElement) null;
      if (context.CanDistributeTopTable && context.CanVerticalDistribute)
      {
        if ((double) this.FreeSpace.Height == 0.0 && dataFlow.Cell.IsVisibleNow)
        {
          if (!dataFlow.IsLastCell)
            context.VertDistributed = DistributeResult.Part;
          return false;
        }
        if (cellContext.VertDistributed == DistributeResult.Part)
          return false;
      }
      return true;
    }
    if (cellContext.VertDistributed == DistributeResult.BackToPrevious)
      dataFlow.Cell = (RectangleElement) null;
    return false;
  }

  private void RestoreSkipCellsAfterDistributeDataCell(
    DistributeDataEnumerator dataFlow,
    DistributeContext cellContext,
    float oldSkipCellsAfter)
  {
    if (dataFlow.Cell != null && this.isColumn && !this.isFixedStructureArea && (double) oldSkipCellsAfter != 0.0)
    {
      dataFlow.Cell.AssignSkipCellsAfter(oldSkipCellsAfter);
      RectangleF bounds = dataFlow.Cell.bounds with
      {
        Size = dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)
      };
      if ((double) cellContext.MaxSize.Height != 0.0 && (double) bounds.Height > (double) cellContext.MaxSize.Height)
      {
        bounds.Height = cellContext.MaxSize.Height;
        if ((double) bounds.Bottom < (double) dataFlow.Cell.properBounds.Bottom)
          bounds.Height = dataFlow.Cell.properBounds.Bottom - bounds.Y;
      }
      dataFlow.Cell.setBounds(bounds);
    }
    oldSkipCellsAfter = 0.0f;
  }

  private void RemoveCellIfIsEmptyTail(
    DistributeDataEnumerator dataFlow,
    DistributeContext cellContext)
  {
    if ((!this.isColumn || dataFlow.Cell.PrevCell == null || dataFlow.Cell.NextCell != null ? 0 : (dataFlow.Cell.AllFlowsIsEmpty() ? 1 : 0)) == 0)
      return;
    dataFlow.RemoveCurrentCellFromDataFlow();
    cellContext.VertDistributed = DistributeResult.Deleted;
  }

  private void InternalDistributeDataAdjustSkipLinesBeforeIfFirstCellOnPage(
    DistributeDataEnumerator dataFlow,
    float cellMinHeight)
  {
    if ((double) dataFlow.Cell.properBounds.Y - (double) dataFlow.Cell.bounds.Y + (double) cellMinHeight <= (double) this.FreeSpace.Height)
      return;
    dataFlow.Cell.AssignSkipCellsBefore((this.FreeSpace.Height - cellMinHeight) / this.OneSkipSize);
    dataFlow.Cell.setProperBounds(new RectangleF(dataFlow.Cell.CalcProperLocation(this.bounds.Location), dataFlow.Cell.properBounds.Size));
    dataFlow.Cell.setBounds(new RectangleF(dataFlow.Cell.bounds.Location, dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)));
  }

  /// <summary>
  /// Рассчитать предполагаемый размер ячейки исходя из размера таблицы
  /// </summary>
  private SizeF InternalDistributeDataCalcPreferredCellSize(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    out float cellMinHeight)
  {
    SizeF sizeF = this.CalcCellSize(dataFlow.Cell, context.CalculatedForCellsProperBounds.Size, context.RowsParams, out RowColParams _, context.ColParams, out RowColParams _, true);
    cellMinHeight = dataFlow.Cell.MinHeight;
    if ((double) cellMinHeight < (double) context.MinRowSize)
      cellMinHeight = context.MinRowSize;
    if (this.isColumn && dataFlow.Cell.IsSingleCell || (double) sizeF.Height < (double) cellMinHeight)
      sizeF.Height = cellMinHeight;
    if ((double) context.MinRowSize > 0.0 && (double) sizeF.Height < (double) context.MinRowSize)
    {
      sizeF.Height = context.MinRowSize;
      if ((double) cellMinHeight < (double) context.MinRowSize)
        cellMinHeight = context.MinRowSize;
    }
    return sizeF;
  }

  private void InternalDistributeDataUpdateCellLocation(
    DistributeContext context,
    DistributeDataEnumerator dataFlow)
  {
    PointF pointF = this.CalcCellLocation(context.PrevCellBounds, dataFlow.Cell);
    RectangleF bounds = dataFlow.Cell.Bounds;
    PointF location = dataFlow.Cell.CalcProperLocation(pointF);
    if (!(bounds.Location != pointF) && !(dataFlow.Cell.properBounds.Location != location))
      return;
    dataFlow.Cell.setProperBounds(new RectangleF(location, dataFlow.Cell.properBounds.Size));
    dataFlow.Cell.setBounds(new RectangleF(pointF, dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)));
    if (dataFlow.Cell is TableData cell)
      cell.RecalcCellLocations(pointF, 0, dataFlow.Cell.Nodes.Count, false, false, false);
    else
      dataFlow.Cell.AssignBoundsOnly(bounds, new RectangleF(pointF, bounds.Size));
    dataFlow.Cell.SetNeedUpdateUIGeometry(true, false);
  }

  private static void InternalDistributeDataCalcRelativeCellWidth(
    DistributeDataEnumerator dataFlow,
    DistributeContext context)
  {
    if ((double) dataFlow.Cell.relativeWidth <= 0.0)
      return;
    RectangleF properBounds = dataFlow.Cell.properBounds with
    {
      Width = context.CalculatedForCellsProperBounds.Width * (dataFlow.Cell.relativeWidth / 100f) - dataFlow.Cell.cellMargins.X - dataFlow.Cell.cellMargins.Width
    };
    dataFlow.Cell.AssignProperBounds(properBounds, false, false, false);
  }

  private void InternalDistributeDataRestorePrevCellBounds(DistributeContext context)
  {
    if (!(this.nodes[context.CurrentCellIndex - 1] is RectangleElement node))
      return;
    node.UpdateBoundsSkipAfter();
    if (node.IsVisibleNow)
    {
      RectangleF prevCellBounds = context.PrevCellBounds with
      {
        Size = node.bounds.Size
      };
      context.PrevCellBounds = prevCellBounds;
    }
    float num = context.PrevCellContext != null ? context.PrevCellContext.MaxSize.Height : context.MaxSize.Height;
    if ((double) num == 0.0 || (double) context.PrevCellBounds.Height <= (double) num)
      return;
    if (node.IsVisibleNow)
    {
      RectangleF prevCellBounds = context.PrevCellBounds with
      {
        Height = num
      };
      context.PrevCellBounds = prevCellBounds;
    }
    node.setBounds(context.PrevCellBounds);
  }

  private float InternalDistributeDataResetSkipCellsFlags(
    DistributeContext context,
    DistributeContext cellContext,
    DistributeDataEnumerator dataFlow,
    TableData cellTable,
    int currentCellIndex)
  {
    if (this.isColumn && dataFlow.Cell.NonSkipBeforeAtStartPage && context.FirstOnPage && currentCellIndex == 0 && !this.isFixedStructureArea && (double) dataFlow.Cell.SkipCellsBefore != 0.0 && (cellTable == null || cellTable.IsFirstInFlow))
    {
      dataFlow.Cell.overrideFlags3 |= OverrideFlags3.IgnoreSkipBefore;
      dataFlow.Cell.setProperBounds(new RectangleF(dataFlow.Cell.bounds.Location, dataFlow.Cell.properBounds.Size));
      dataFlow.Cell.setBounds(new RectangleF(dataFlow.Cell.bounds.Location, dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)));
    }
    else
      dataFlow.Cell.overrideFlags3 &= ~OverrideFlags3.IgnoreSkipBefore;
    float skipCellsAfter = dataFlow.Cell.SkipCellsAfter;
    if (this.isColumn && !this.isFixedStructureArea && (double) skipCellsAfter != 0.0)
      dataFlow.Cell.AssignSkipCellsAfter(0.0f);
    if ((double) skipCellsAfter != (double) dataFlow.Cell.SkipCellsAfter)
    {
      dataFlow.Cell.setBounds(new RectangleF(dataFlow.Cell.bounds.Location, dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)));
      RectangleF bounds = dataFlow.Cell.bounds with
      {
        Size = dataFlow.Cell.CalcSizeFromProper(dataFlow.Cell.properBounds.Size)
      };
      if ((double) cellContext.MaxSize.Height != 0.0 && (double) bounds.Height > (double) cellContext.MaxSize.Height)
      {
        bounds.Height = cellContext.MaxSize.Height;
        if ((double) bounds.Bottom < (double) dataFlow.Cell.properBounds.Bottom)
          bounds.Height = dataFlow.Cell.properBounds.Bottom - bounds.Y;
      }
      dataFlow.Cell.setBounds(bounds);
    }
    return skipCellsAfter;
  }

  private void MoveDistributeCell(DistributeDataEnumerator dataFlow, DistributeContext context)
  {
    this.InsertChildNode(context.CurrentCellIndex, (DocumentTreeNode) dataFlow.Cell, true, false, false, false, false);
    dataFlow.CurrentCellPosition.IsMoved = true;
    if (dataFlow.IsCellFromBuffer)
    {
      dataFlow.SourceTable.DistributeBuffer?.RemoveAt(dataFlow.BufferIndex);
      context.LastCellFromBuffer = context.CurrentCellIndex;
      dataFlow.BufferIndex = int.MaxValue;
      if (dataFlow.SourceTable != this)
        return;
      dataFlow.SourceIndex = context.CurrentCellIndex;
    }
    else
      --dataFlow.SourceIndex;
  }

  private void CheckDistributingCellAndRemoveIfEmpty(
    DistributeDataEnumerator dataFlow,
    DistributeContext cellContext)
  {
    if (!(dataFlow.Cell is TableData cell) || cell.Parent == this || cell.prevCell == null || !cell.AllFlowsIsEmpty())
      return;
    dataFlow.Cell.UniteTable();
    if (!cell.AllFlowsIsEmpty())
      return;
    dataFlow.RemoveCurrentCellFromDataFlow();
    cellContext.VertDistributed = DistributeResult.Deleted;
  }

  private void RemoveEmptyNextTableTail(CellWithPosition tailPosition)
  {
    TableData tableData1 = tailPosition.SourceTable;
    if (tailPosition.IsCellFromBuffer || tableData1 == null || tableData1.NextCell != null || tailPosition.SourceIndex < tableData1.NodesCount || tableData1.IsTopLevelTable || !tableData1.ParentCell.IsColumn)
      return;
    while (tableData1 != this && tableData1.AllFlowsIsEmpty() && tableData1.PrevCell != null)
    {
      TableData tableData2 = tableData1;
      tableData1 = tableData1.PrevTable;
      tableData2.UniteTable();
      if (!tableData2.IsTopLevelTable)
        tableData2.Remove(false, false);
    }
  }

  /// <summary>
  /// Проверить условия переноса на другие страницы независимые от размера
  /// </summary>
  /// <returns></returns>
  private bool CheckDistributeCurrentCellForThisTable(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    bool isFirstDataCell)
  {
    TableData cell = dataFlow.Cell as TableData;
    if (context.CanDistributeTopTable && context.CanVerticalSplit)
    {
      if (dataFlow.Cell.PrevCell == null && (cell == null || cell.IsFirstInFlow) && dataFlow.FromNewPageInCurrentContext(dataFlow.CurrentCellPosition) && (!isFirstDataCell || !context.FirstDataOnPage))
        return false;
      if (dataFlow.Cell.OnOnePageWith != null && dataFlow.Cell.PrevCell == null)
      {
        PageData page = dataFlow.Cell.OnOnePageWith.Page;
        if (context.FirstPass)
          this.NeedSecondLayoutPass = true;
        else if (page == null || page != this.page && page.Index > this.page.Index)
          return false;
      }
    }
    return true;
  }

  private DistributeContext CreateDistributeCellContext(
    DistributeDataEnumerator dataFlow,
    DistributeContext context,
    bool isFirstDataCell)
  {
    DistributeContext distributeCellContext = new DistributeContext((DocumentTreeNode) dataFlow.Cell, dataFlow.Cell.Size, this.FreeSpace, context.CurrentCellIndex == 0 || this.IsRow, isFirstDataCell || this.IsRow, context);
    DistributeContext distributeContext = distributeCellContext;
    int num;
    if (context.KeepWithNext_IsFirstDataOnPage)
    {
      DistributeContext prevCellContext = context.PrevCellContext;
      num = prevCellContext != null ? (prevCellContext.KeepWithNext_IsFirstDataOnPage ? 1 : 0) : (context.KeepWithNext_IsFirstDataOnPage ? 1 : 0);
    }
    else
      num = 0;
    distributeContext.KeepWithNext_IsFirstDataOnPage = num != 0;
    if (this.IsColumn)
      distributeCellContext.KeepWithNext_IsFirstDataOnPage &= dataFlow.Cell.keepWithNext;
    if (dataFlow.Cell.FromNewPage)
      context.FirstKeepWithNext = (RectangleElement) null;
    distributeCellContext.TryNotBreak_Failed |= context.TryNotBreak_Failed;
    return distributeCellContext;
  }

  private DistributeDataEnumerator CreateDistributeDataCellEnumerator(DistributeContext context)
  {
    DistributeDataEnumerator dataCellEnumerator = new DistributeDataEnumerator(this, context.HeaderCellsIsAvailable, context.CanVerticalDistribute, context);
    if (context.CanVerticalDistribute && (double) this.FreeSpace.Height < 0.0)
    {
      context.VertDistributed = DistributeResult.None;
      dataCellEnumerator.SourceTable = (TableData) null;
    }
    return dataCellEnumerator;
  }

  private void PrepareDistributeDataTableContext(DistributeContext context)
  {
    context.VertDistributed = DistributeResult.All;
    context.CanVerticalDistribute = this.CanVerticalDistribute();
    context.CanVerticalSplit = this.CanVerticalSplit();
    context.ParentCell = this.ParentCell;
    context.ColParams = this.GridColumnsParams;
    context.RowsParams = this.GridRowsParams;
    RectangleF realProperBounds = this.RealProperBounds with
    {
      Height = this.minHeight
    };
    context.CalculatedForCellsProperBounds = realProperBounds;
    context.DistributeBounds = new RectangleF(this.Bounds.Location, context.MaxSize);
    double height1 = (double) context.MaxSize.Height;
    RectangleF rectangleF = context.CalculatedForCellsProperBounds;
    double y1 = (double) rectangleF.Y;
    rectangleF = this.Bounds;
    double y2 = (double) rectangleF.Y;
    double num = y1 - y2;
    float height2 = (float) (height1 - num);
    context.DistributePropperBounds = new RectangleF(context.CalculatedForCellsProperBounds.Location, new SizeF(context.MaxSize.Width, height2));
    context.MinRowSize = this.GetMinRowSizeFromContext(context);
    context.PrevCellBounds = this.GetFirstCellBoundsForStartDistributeTableData(context.HeaderCount, context.CalculatedForCellsProperBounds.Location);
    this.FreeSpace = !this.IsColumn ? new SizeF(float.MaxValue, context.DistributePropperBounds.Height) : new SizeF(float.MaxValue, context.DistributePropperBounds.Bottom - context.PrevCellBounds.Bottom);
    context.FirstKeepWithNext = (RectangleElement) null;
    context.HeaderCellsIsAvailable = this.isColumn || context.ColParams == null;
    context.CurrentCellIndex = context.HeaderCount;
  }

  /// <summary>Упорядочить сетку</summary>
  /// <param name="template">Шаблон</param>
  private void OrderGrid(DistributeContext context)
  {
    if (this.IsVirtualNode || !this.IsColumnGridOwner())
      return;
    bool flag = false;
    List<RowColParams> gridCols = this.gridColumnsParams;
    TableData template = context.Template as TableData;
    if (gridCols == null && template != null)
    {
      gridCols = template.GridColumnsParams;
      flag = gridCols != null;
    }
    if (gridCols == null)
      gridCols = new List<RowColParams>();
    int newGridColIndex = 0;
    for (int index = 0; index < gridCols.Count; ++index)
    {
      if (gridCols[index].CellType == CellType.Header)
      {
        if (newGridColIndex != index)
        {
          if (flag)
          {
            this.CloneGridAndUpdateRefences(ref gridCols, true);
            flag = false;
          }
          this.MoveGridColumn(index, newGridColIndex, false, false);
          gridCols = this.gridColumnsParams;
        }
        ++newGridColIndex;
      }
    }
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout)
  {
    if (!(updateLayout & value) && this.needUpdateLayoutFlag == value)
      return;
    this.AssignNeedUpdateLayoutFlag(value);
    TableData tableData = (TableData) null;
    if (setInPrevCell & value)
    {
      tableData = this.PrevTable;
      if ((this.TableCellType == CellType.DataCell || this.IsTopLevelTable) && tableData != null && !this.IsDistributing)
        tableData.AssignNeedUpdateLayoutFlag(value);
      if (this.onOnePageWith != null && this.onOnePageWith.ParentCell != null)
        this.onOnePageWith.ParentCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, false, false);
    }
    TableData parentCell = this.ParentCell;
    if (value && parentCell != null)
      parentCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
    else if (value && this.page != null)
    {
      if (setInPrevCell && tableData != null && tableData.page != null)
        tableData.page.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
      this.page?.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
    }
    else
    {
      if (!(updateLayout & value))
        return;
      this.UpdateLayout(updateUI);
    }
  }

  /// <summary>Таблица распределяется в текущий момент</summary>
  public override bool IsDistributing
  {
    [DebuggerStepThrough] get => this.distributingCount != 0;
  }

  /// <summary>В таблице помещаются только заголовоки</summary>
  internal bool AllocateOnlyHeaders
  {
    get => this.HasCellFlags(CellFlags.TableAllocateOnlyHeaders);
    set => this.SetCellFlags(CellFlags.TableAllocateOnlyHeaders, value);
  }

  /// <summary>Установить значение поля distributingCount, без вызова обновлений</summary>
  /// <param name="value">Новое значение поля distributingCount</param>
  internal virtual void SetDistributingCount(int value) => this.distributingCount = value;

  /// <summary>Начать распределение данных по таблице</summary>
  /// <remarks>Увеличивает значение счетчика distributeCount для таблицы всех подтаблиц</remarks>
  public virtual void BeginDistribute() => ++this.distributingCount;

  /// <summary>Закончить распределение данных по таблице</summary>
  /// <remarks>Уменьшает значение счетчика distributeCount для таблицы всех подтаблиц</remarks>
  /// <param name="forceEnd">Обнулить все счетчики независимо от их значения</param>
  public virtual void EndDistribute(bool forceEnd)
  {
    --this.distributingCount;
    if (!forceEnd && this.distributingCount >= 0)
      return;
    this.distributingCount = 0;
  }

  /// <summary>Событие Перед добавлением дочернего узла</summary>
  public event BeforeDistribute_EventHandler BeforeDistribute
  {
    add => this.beforeDistribute += value;
    remove => this.beforeDistribute -= value;
  }

  /// <summary>Вызывает событие BeforeDistribute</summary>
  protected virtual void OnBeforeDistribute()
  {
    BeforeDistribute_EventHandler beforeDistribute = this.beforeDistribute;
    if (beforeDistribute == null)
      return;
    beforeDistribute((object) this);
  }

  /// <summary>Изменить количество свободного пространства для таблицы, если необходимо</summary>
  /// <param name="context"></param>
  private void RecalcFreeSpaceForTableHierarchy(DistributeContext context)
  {
    if (this.nextCell == null || !this.nextCell.AllFlowsIsEmpty())
      return;
    DistributeContext distributeContext = context;
    TableData tableData = this;
    List<TableData> tableDataList = new List<TableData>();
    List<DistributeContext> distributeContextList = new List<DistributeContext>();
    for (; distributeContext != null && tableData != null; tableData = tableData.ParentCell)
    {
      distributeContextList.Add(distributeContext);
      tableDataList.Add(tableData);
      distributeContext = distributeContext.ParentContext;
    }
    float num = 0.0f;
    for (int index = tableDataList.Count - 1; index >= 0; --index)
    {
      if ((double) distributeContextList[index].SkipSizeAfter != (double) tableDataList[index].SkipSizeAfter)
      {
        num -= tableDataList[index].SkipSizeAfter - distributeContextList[index].SkipSizeAfter;
        distributeContextList[index].SkipSizeAfter = tableDataList[index].SkipSizeAfter;
      }
      tableDataList[index].FreeSpace.Height += num;
    }
  }

  /// <summary>Координаты предыдущей ячейки для расчета координат текущей в контексте разбивки таблицы</summary>
  /// <param name="headerCount">Количество ячеек заголовка в таблице</param>
  /// <param name="tableLocation">Текущее положение таблицы, для распределения ячеек</param>
  /// <returns></returns>
  private RectangleF GetFirstCellBoundsForStartDistributeTableData(
    int headerCount,
    PointF tableLocation)
  {
    RectangleF distributeTableData = new RectangleF(tableLocation, new SizeF(0.0f, 0.0f));
    if (headerCount > 0)
    {
      for (int index = headerCount - 1; index >= 0; --index)
      {
        if (((VisualNode) this.nodes[index]).IsVisibleNow)
        {
          distributeTableData = ((RectangleElement) this.nodes[index]).Bounds;
          break;
        }
      }
    }
    return distributeTableData;
  }

  /// <summary>Получить минимальный размер строки с учётом контекста при разбивке</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private float GetMinRowSizeFromContext(DistributeContext context)
  {
    float rowSizeNn = context.RowSize_NN;
    return (double) rowSizeNn == 0.0 ? this.GetMinRowSize(this.GridRowsParams) : rowSizeNn;
  }

  /// <summary>Распределить данные потока таблицы</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновить пользовательский интерфейс после разбивки</param>
  public virtual void DistributeTableOld(DistributeContext context, bool updateUI)
  {
    if (this.IsTopLevelTable)
      LogManager.AddLine("DistributeTable -Start ID:" + this.Id);
    context.VertDistributed = DistributeResult.All;
    context.TryNotBreak |= this.tryNotBreak;
    if (this.IsTopLevelTable)
      context.CanDistributeTopTable = this.CanVerticalDistribute();
    if (this.IsVirtualNode || this.page == null)
      return;
    if (this.nodes == null)
      throw new Exception("DistributeTable: nodes == null");
    if (!this.IsDistributing)
      this.BeginDistribute();
    this.OnBeforeDistribute();
    if (this.IsTopLevelTable)
    {
      Monitor.Enter((object) this.nodes);
      this.AutoSizeCells((List<RowColParams>) null);
    }
    if (this.IsFixedStructureArea && this.IsFormulaLib)
    {
      TableData.AlignChildElements((VisualNode) this);
      context.NewSize = this.Size;
    }
    try
    {
      this.BeginChangingStructure();
      float height = context.MaxSize.Height;
      try
      {
        if ((double) this.cellsMinHeight > (double) this.minHeight)
          this.cellsMinHeight = this.minHeight;
        int headerCount = 0;
        context.Template = (RectangleElement) this.GetTableStructureTemplate();
        this.AssignBounds(this.Location, context.NewSize, false, false, false);
        float skipSizeAfter = this.SkipSizeAfter;
        if ((double) skipSizeAfter != 0.0)
        {
          context.MaxSize.Height -= skipSizeAfter;
          context.SkipSizeAfter = skipSizeAfter;
        }
        if (this.UsePreviousTableTemplates && this.IsTemplate && this.IsHeaderCell)
        {
          this.Clear(false, false);
        }
        else
        {
          context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
          context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
          if (!this.IsVisibleNow && this.IsFirstInFlow)
            this.UniteTable();
          if (!this.IsFixedStructureArea)
          {
            this.OrderGrid(context);
            this.DistributeHeadersOld(context, out headerCount);
          }
          if (this.IsVisibleNow)
            this.DistributeDataOld(context, headerCount);
        }
        int count = this.nodes.Count;
        if (context.VertDistributed != DistributeResult.BackToPrevious)
        {
          if (count > 0)
            this.InternalDistributeTableAdjustCellSizes(context);
        }
      }
      finally
      {
        if (!context.FirstPass)
          this.NeedSecondLayoutPass = false;
        context.MaxSize.Height = height;
        if (context.VertDistributed != DistributeResult.BackToPrevious && context.VertDistributed != DistributeResult.None)
        {
          this.AssignNeedUpdateLayoutFlag(false);
          this.EndChangingStructure(false, false, false, false);
          this.EndDistribute(true);
          if (updateUI)
            this.UpdateUIGeometry(updateUI);
        }
        else
        {
          this.EndChangingStructure(false, false, false, false);
          this.EndDistribute(true);
          if (updateUI)
            this.UpdateUIGeometry(updateUI);
        }
      }
    }
    finally
    {
      if (this.IsTopLevelTable)
        Monitor.Exit((object) this.nodes);
    }
    if (!this.IsTopLevelTable)
      return;
    LogManager.AddLine("DistributeTable -End ID:" + this.Id);
  }

  /// <summary>Распределить данные по таблицам и страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="headerCount">Количество заголовочных ячеек перед данными</param>
  protected virtual void DistributeDataOld(DistributeContext context, int headerCount)
  {
    context.VertDistributed = DistributeResult.All;
    if (this.IsVirtualNode)
      return;
    TableCellDistributeContextOld tableContextOld = this.InternalDistributeDataGetTableContextOld(context, headerCount);
    TableCellDistributeContextOld distributeContextOld = (TableCellDistributeContextOld) null;
    DistributeDataEnumeratorOld cellEnumeratorOld = this.InternalDistributeDataGetCellEnumeratorOld(context, tableContextOld);
    while (cellEnumeratorOld.MoveNext())
    {
      TableCellDistributeContextOld prevCellContext = distributeContextOld;
      bool isFirstDataCell = tableContextOld.СurrentCellIndex - headerCount == 0;
      distributeContextOld = this.InternalDistributeDataSwitchCellContextOld(context, tableContextOld, prevCellContext, cellEnumeratorOld, isFirstDataCell);
      if (!this.InternalDistributeDataCheckCellOld(context, tableContextOld, cellEnumeratorOld, isFirstDataCell))
      {
        distributeContextOld.VertDistributed = DistributeResult.None;
        break;
      }
      TableData cell = cellEnumeratorOld.Cell as TableData;
      bool flag1 = cellEnumeratorOld.Cell.Parent != this || cellEnumeratorOld.bufferCount > 0 || tableContextOld.СurrentCellIndex != cellEnumeratorOld.sourceIndex - 1;
      if (flag1)
      {
        if (!this.InternalDistributeDataRemoveCellIfEmptyOld(cellEnumeratorOld, distributeContextOld))
        {
          this.InsertChildNode(tableContextOld.СurrentCellIndex, (DocumentTreeNode) cellEnumeratorOld.Cell, true, false, false, false, false);
          this.InternalDistributeDataUpdateBufferOld(cellEnumeratorOld, tableContextOld);
        }
        else
          continue;
      }
      float oldSkipCellsAfter = this.InternalDistributeDataResetSkipCellsFlagsOld(context, distributeContextOld, cellEnumeratorOld, cell, tableContextOld.СurrentCellIndex);
      if (flag1)
        this.RecalcFreeSpaceForTableHierarchyOld(context);
      if (this.isColumn && tableContextOld.СurrentCellIndex > 0 && !this.isFixedStructureArea)
        this.InternalDistributeDataRestorePrevCellBoundsOld(tableContextOld, prevCellContext);
      TableData.InternalDistributeDataCalcRelativeCellWidthOld(cellEnumeratorOld, tableContextOld);
      RectangleF rectangleF;
      int num1;
      if (!context.Force && !cellEnumeratorOld.Cell.NeedUpdateLayoutFlag)
      {
        if (tableContextOld.CanVerticalSplit)
        {
          double bottom1 = (double) cellEnumeratorOld.Cell.bounds.Bottom;
          rectangleF = tableContextOld.DistributeBounds;
          double bottom2 = (double) rectangleF.Bottom;
          if (bottom1 > bottom2)
            goto label_16;
        }
        num1 = cell == null ? 0 : (cell.NeedSecondLayoutPass ? 1 : 0);
        goto label_17;
      }
label_16:
      num1 = 1;
label_17:
      bool flag2 = num1 != 0;
      if (cellEnumeratorOld.Cell.IsVisibleNow && (this.needUpdateLayoutFlag | flag2 || (double) cellEnumeratorOld.Cell.bounds.Height != (double) cellEnumeratorOld.Cell.MinHeight))
      {
        this.InternalDistributeDataUpdateCellLocationOld(tableContextOld, cellEnumeratorOld, cell);
        float cellMinHeight;
        SizeF size = this.InternalDistributeDataCalcPreferredCellSizeOld(cellEnumeratorOld, tableContextOld, out cellMinHeight);
        distributeContextOld.NewSize = size;
        if (context.FirstDataOnPage && tableContextOld.СurrentCellIndex - headerCount == 0 && (double) cellEnumeratorOld.Cell.SkipCellsBefore != 0.0)
          this.InternalDistributeDataAdjustSkipLinesBeforeIfFirstCellOnPageOld(cellEnumeratorOld, cellMinHeight);
        bool flag3 = ((flag2 ? 1 : 0) | (cellEnumeratorOld.Cell.NeedUpdateMinHeight || cellEnumeratorOld.Cell.NeedUpdateMinWidth ? 1 : (cellEnumeratorOld.Cell is ContainerData ? 1 : 0))) != 0;
        if (this.IsFixedStructureArea)
        {
          ref SizeF local = ref distributeContextOld.MaxSize;
          rectangleF = tableContextOld.DistributeBounds;
          double bottom = (double) rectangleF.Bottom;
          rectangleF = cellEnumeratorOld.Cell.Bounds;
          double y = (double) rectangleF.Y;
          double num2 = bottom - y;
          local.Height = (float) num2;
        }
        if (flag3 || !cellEnumeratorOld.Cell.IsSingleCell || (double) cellMinHeight > (double) size.Height || (double) cellEnumeratorOld.Cell.bounds.Width != (double) size.Width || cellEnumeratorOld.Cell.NextCell != null || this.IsFixedStructureArea && (double) cellEnumeratorOld.Cell.bounds.Height > (double) distributeContextOld.MaxSize.Height)
        {
          if (cell != null)
          {
            cell.DistributeTableOld((DistributeContext) distributeContextOld, false);
            if ((!this.isColumn || cell.prevCell == null || cell.nextCell != null ? 0 : (cell.AllFlowsIsEmpty() ? 1 : 0)) != 0)
              this.InternalDistributeDataRemoveEmptyTailOld(tableContextOld.СurrentCellIndex, cellEnumeratorOld, distributeContextOld);
          }
          else
          {
            cellEnumeratorOld.Cell.DistributeCell((DistributeContext) distributeContextOld);
            if (cellEnumeratorOld.Cell.NextCell != null)
            {
              bool updateLayoutFlag = cellEnumeratorOld.Cell.NeedUpdateLayoutFlag;
              cellEnumeratorOld.Cell.NextCell.SetNeedUpdateLayoutFlag(true, false, false, false);
              if (this.TopLevelTable.NextCell != null)
                this.TopLevelTable.NextCell.SetNeedUpdateLayoutFlag(true, false, false, false);
              cellEnumeratorOld.Cell.SetNeedUpdateLayoutFlag(updateLayoutFlag, false, false, false);
            }
            if ((!this.isColumn || cellEnumeratorOld.Cell.PrevCell == null || cellEnumeratorOld.Cell.NextCell != null ? 0 : (cellEnumeratorOld.Cell.AllFlowsIsEmpty() ? 1 : 0)) != 0)
              this.InternalDistributeDataRemoveEmptyTailOld(tableContextOld.СurrentCellIndex, cellEnumeratorOld, distributeContextOld);
          }
          if (cellEnumeratorOld.Cell != null)
          {
            rectangleF = tableContextOld.CalculatedProperTableBounds;
            if ((double) rectangleF.Bottom < (double) cellEnumeratorOld.Cell.bounds.Bottom)
            {
              RectangleF properTableBounds = tableContextOld.CalculatedProperTableBounds;
              ref RectangleF local = ref properTableBounds;
              double bottom = (double) cellEnumeratorOld.Cell.bounds.Bottom;
              rectangleF = tableContextOld.CalculatedProperTableBounds;
              double y = (double) rectangleF.Y;
              double num3 = bottom - y;
              local.Height = (float) num3;
              tableContextOld.CalculatedProperTableBounds = properTableBounds;
            }
          }
        }
        else
        {
          if ((double) cellMinHeight < (double) cellEnumeratorOld.Cell.ContentHeight)
            cellMinHeight = cellEnumeratorOld.Cell.ContentHeight;
          if ((double) size.Height < (double) cellMinHeight)
            size.Height = cellMinHeight;
          if (this.IsRow && (double) tableContextOld.MinRowSize > 0.0 && (double) size.Height < (double) tableContextOld.MinRowSize)
          {
            size.Height = tableContextOld.MinRowSize;
            if ((double) cellMinHeight < (double) tableContextOld.MinRowSize)
              cellMinHeight = tableContextOld.MinRowSize;
          }
          if ((double) cellMinHeight != (double) cellEnumeratorOld.Cell.bounds.Height)
          {
            if ((double) cellMinHeight > 0.0 && (cell != null || (double) cellEnumeratorOld.Cell.bounds.Height < (double) cellMinHeight || (double) size.Height > (double) cellEnumeratorOld.Cell.ContentHeight || (double) cellEnumeratorOld.Cell.bounds.Height - (double) cellEnumeratorOld.Cell.ContentHeight > 9.9999997473787516E-06))
            {
              size.Height = cellEnumeratorOld.Cell.ContentHeight;
              if ((double) size.Height < (double) cellMinHeight)
                size.Height = cellMinHeight;
              cellEnumeratorOld.Cell.SetCellSizes(new RectangleF(cellEnumeratorOld.Cell.bounds.Location, size), true, false, false, false);
            }
            if (cellEnumeratorOld.Cell.NextCell != null)
              distributeContextOld.VertDistributed = DistributeResult.Part;
          }
        }
        if (cell != null)
          cell.distributingCount = 0;
      }
      this.InternalDistributeDataRestoreSkipCellsAfterOld(cellEnumeratorOld, distributeContextOld, oldSkipCellsAfter);
      if (!this.InternalDistributeDataCheckDistributionResultOld(context, cellEnumeratorOld, tableContextOld, distributeContextOld))
        break;
    }
    TableCellDistributeContextOld cellContext = distributeContextOld ?? tableContextOld;
    tableContextOld.DataCellCount = tableContextOld.СurrentCellIndex - headerCount;
    RectangleElement current = cellEnumeratorOld.Current;
    if (current != null)
      this.InternalDistributeDataHandleUndistributedCellOld(context, tableContextOld, cellContext, current, headerCount);
    if (cellContext.VertDistributed == DistributeResult.Part)
      context.VertDistributed = DistributeResult.Part;
    else if (cellContext.VertDistributed == DistributeResult.BackToPrevious)
      context.VertDistributed = DistributeResult.BackToPrevious;
    this.InternalDistributeDataUpdateTryNotBreakFlagOld(context, tableContextOld);
    if (tableContextOld.CanVerticalSplit && (context.VertDistributed == DistributeResult.Part || context.VertDistributed == DistributeResult.None && this.IsTopLevelTable))
    {
      this.InternalDistributeDataProcessKeepWithNextOld(context, tableContextOld, headerCount);
      if (this.InternalDistributeDataCreateNextPageIfNeedOld(context, tableContextOld) && !this.InternalDistributeDataIsNextCellFitInNewPageOld(context, tableContextOld, headerCount))
      {
        context.InsufficientPageSize = true;
        this.nextCell.Page.Remove(false, false, false);
        this.SetNextCell((RectangleElement) null);
      }
      TableData nextTable = this.NextTable;
      if (tableContextOld.CanVerticalDistribute && nextTable != null && (this.distributeBuffer != null && this.distributeBuffer.Count > 0 || headerCount + tableContextOld.DataCellCount < this.nodes.Count))
      {
        nextTable.SetNeedUpdateLayoutFlag(true, false, false, false);
        if (this.distributeBuffer != null)
        {
          for (int index = 0; index < this.distributeBuffer.Count; ++index)
          {
            if (this.distributeBuffer[index].NextCell != null && this.distributeBuffer[index].NextCell.Page == nextTable.Page)
              this.distributeBuffer[index].OneStepUniteTable();
          }
        }
        this.InternalDistributeDataMoveExcessiveCellsOld(tableContextOld, nextTable, headerCount);
        this.InternalDistributeDataMoveBufferToNextTableOld(nextTable);
        if (this.isColumn && !this.isFixedStructureArea && this.nodes.Count > 0 && this.nodes[this.nodes.Count - 1] is RectangleElement node)
          node.UpdateBoundsSkipAfter();
      }
    }
    this.InternalDistributeDataReleaseBufferOld();
    if (!LogManager.CreateLog)
      return;
    LogManager.AddLine($"DistributeData {this.GetType()}:{this.Id} [{this.Bounds}]. {context.VertDistributed}");
  }

  /// <summary>Упорядочить заголовки таблицы</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="headerCount">Количество заголовочных ячеек перед данными</param>
  protected virtual void DistributeHeadersOld(DistributeContext context, out int headerCount)
  {
    headerCount = 0;
    if (this.IsVirtualNode)
      return;
    RectangleElement rectangleElement = (RectangleElement) null;
    int index1 = 0;
    bool isFirstInFlow = this.IsFirstInFlow;
    bool flag1 = this.isColumn || this.GridColumnsParams == null;
    if (((context.Force ? 1 : (this.needUpdateLayoutFlag ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
    {
      if (context.Template != null)
      {
        TableData tableData = context.Template as TableData;
        if (this.UsePreviousTableTemplates)
          tableData = this.GetTableStructureTemplate();
        rectangleElement = (RectangleElement) null;
        headerCount = 0;
        int count1 = this.nodes.Count;
        index1 = 0;
        while (index1 < count1 && (!(this.nodes[index1] is RectangleElement node1) || node1.TableCellType != CellType.DataCell && node1.Template == null))
          ++index1;
        int count2 = tableData.Nodes.Count;
        for (int index2 = 0; index2 < count2; ++index2)
        {
          if (tableData.Nodes[index2] is RectangleElement node3 && node3.TableCellType != CellType.DataCell)
          {
            RectangleElement child = (RectangleElement) null;
            int count3 = this.nodes.Count;
            int index3;
            for (index3 = index1; index3 < count3; ++index3)
            {
              if (this.nodes[index3].Template == tableData.Nodes[index2])
              {
                child = this.nodes[index3] as RectangleElement;
                break;
              }
            }
            bool flag2 = false;
            if (child == null && node3.CloneByTemplateWithParent && this.HeaderIsNeed(isFirstInFlow, node3.HeaderShowType) && !this.Page.IsNextToAdditionalPage)
            {
              child = tableData.Nodes[index2].CloneFromTemplate(true, true) as RectangleElement;
              flag2 = true;
            }
            if (child != null)
            {
              if (this.HeaderIsNeed(isFirstInFlow, child.HeaderShowType))
              {
                this.InsertChildNode(index1++, (DocumentTreeNode) child, false, true, false, false, false);
                if (flag2)
                  child.UpdateNodeLinks(true, true, false, false);
              }
              else
              {
                child.Remove(false, false);
                index3 = -1;
              }
            }
            else
              index3 = -1;
            for (int count4 = this.nodes.Count; index3 < count4 && index3 > -1; ++index3)
            {
              RectangleElement node2 = this.nodes[index3] as RectangleElement;
              if (this.nodes[index3].Template == null && (node2 == null || node2.TableCellType != CellType.DataCell))
              {
                if (index1 != index2)
                  this.InsertChildNode(index1, this.nodes[index3], false, true, false, false, false);
                ++index1;
              }
              else
                break;
            }
          }
        }
      }
      rectangleElement = (RectangleElement) null;
      for (int index4 = index1; index4 < this.nodes.Count; ++index4)
      {
        if (this.Nodes[index4] is RectangleElement node && node.TableCellType != CellType.DataCell)
        {
          if (!this.HeaderIsNeed(isFirstInFlow, node.HeaderShowType))
          {
            node.Remove(false, false);
          }
          else
          {
            if (index1 != index4)
              this.InsertChildNode(index1, (DocumentTreeNode) node, false, true, false, false, false);
            ++index1;
          }
        }
      }
      headerCount = index1;
    }
    else
      headerCount = flag1 ? this.CalcFirstHeaderCount() : 0;
    if (headerCount <= 0)
      return;
    this.InternalDistributeHeaderCellsOld(context, headerCount);
  }

  /// <summary>
  /// Распределить ячейки заголовка,  пересчитать координаты, обновить геометрию
  /// </summary>
  /// <param name="context">контекст распределения ячеек всей таблицы</param>
  /// <param name="headerCount">количество ячеек заголовка</param>
  private void InternalDistributeHeaderCellsOld(DistributeContext context, int headerCount)
  {
    RectangleF prevBounds = new RectangleF(this.ProperLocation, new SizeF(0.0f, 0.0f));
    List<RowColParams> gridColumnsParams = this.GridColumnsParams;
    List<RowColParams> gridRowsParams = this.GridRowsParams;
    RowColParams thisColParams = (RowColParams) null;
    RowColParams thisRowParams = (RowColParams) null;
    RectangleF properBounds = this.ProperBounds with
    {
      Height = this.minHeight
    };
    SizeF maxSize = new SizeF(TableData.UnconstrainedSize, this.RealMaxHeight);
    float num = 0.0f;
    if (this.IsRow)
    {
      float rowSizeNn = context.RowSize_NN;
      num = (double) rowSizeNn == 0.0 ? this.GetMinRowSize(this.GridRowsParams) : rowSizeNn;
    }
    for (int index = 0; index < headerCount; ++index)
    {
      RectangleElement node1 = this.nodes[index] as RectangleElement;
      TableData node2 = this.nodes[index] as TableData;
      if (node1 != null && node1.IsVisibleNow)
      {
        if (this.isColumn && node1.NonSkipBeforeAtStartPage && !this.isFixedStructureArea && (double) node1.SkipCellsBefore != 0.0 && context.FirstOnPage && index == 0)
        {
          node1.overrideFlags3 |= OverrideFlags3.IgnoreSkipBefore;
          node1.setProperBounds(new RectangleF(node1.bounds.Location, node1.properBounds.Size));
          node1.setBounds(new RectangleF(node1.bounds.Location, node1.CalcSizeFromProper(node1.properBounds.Size)));
        }
        else
          node1.overrideFlags3 &= ~OverrideFlags3.IgnoreSkipBefore;
        PointF pointF = this.CalcCellLocation(prevBounds, node1);
        RectangleF bounds = node1.Bounds;
        if (bounds.Location != pointF)
        {
          if (node2 != null)
            node2.RecalcCellLocations(pointF, 0, node1.Nodes.Count, false, false, false);
          else
            node1.AssignBoundsOnly(bounds, new RectangleF(pointF, bounds.Size));
        }
        SizeF newSize = this.CalcCellSize(node1, properBounds.Size, gridRowsParams, out thisRowParams, gridColumnsParams, out thisColParams, false);
        float minHeight = node1.MinHeight;
        if ((double) newSize.Height < (double) minHeight)
          newSize.Height = minHeight;
        if (this.IsRow && (double) num > 0.0 && (double) newSize.Height < (double) num)
          newSize.Height = num;
        DistributeContext context1 = new DistributeContext((DocumentTreeNode) node1, newSize, maxSize, index == 0 || !this.isColumn, true, context);
        if (node2 != null)
          node2.DistributeTableOld(context1, false);
        else
          node1.DistributeCell(context1);
        if ((double) properBounds.Bottom < (double) node1.bounds.Bottom)
          properBounds.Height = node1.bounds.Bottom - properBounds.Y;
        if (this.isColumn || (double) node1.bounds.Height != 0.0)
          this.AdjustSizeToCell(node1, false, false);
        node1.ResetNeedUpdateLayoutFlag(true);
        prevBounds = node1.Bounds;
      }
    }
  }

  private void InternalDistributeDataUpdateTryNotBreakFlagOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext)
  {
    if (context.VertDistributed == DistributeResult.All)
    {
      this.TryNotBreak_Failed0 = false;
      this.TryNotBreak_Failed1 = false;
      context.TryNotBreak_Failed = false;
    }
    else
    {
      if (!context.TryNotBreak || !tableContext.CanVerticalSplit || this.IsTopLevelTable || context.VertDistributed != DistributeResult.Part || !this.IsFirstInFlow || (context.FirstDataOnPage || context.KeepWithNext_IsFirstDataOnPage) && !this.TryNotBreak_Failed0)
        return;
      if (context.TryNotBreak_Failed || this.TryNotBreak_Failed0 && this.TryNotBreak_Failed1)
      {
        this.TryNotBreak_Failed0 = false;
        this.TryNotBreak_Failed1 = false;
        context.TryNotBreak_Failed = true;
      }
      else
      {
        if (!this.TryNotBreak_Failed0)
        {
          this.TryNotBreak_Failed0 = true;
          context.VertDistributed = DistributeResult.None;
        }
        else if (!this.TryNotBreak_Failed1)
        {
          this.TryNotBreak_Failed1 = true;
          context.TryNotBreak_Failed = true;
          context.VertDistributed = DistributeResult.BackToPrevious;
          if (this.TopLevelTable.PrevCell != null)
            this.TopLevelTable.PrevCell.SetNeedUpdateLayoutFlag(true, false, false, false);
        }
        this.SetNeedUpdateLayoutFlag(true, false, false, false, true);
      }
    }
  }

  private void InternalDistributeDataReleaseBufferOld()
  {
    if (this.distributeBuffer == null || this.distributeBuffer.Count <= 0)
      return;
    for (int index = this.distributeBuffer.Count - 1; index >= 0; --index)
      this.InsertChildNode(this.nodes.Count, (DocumentTreeNode) this.distributeBuffer[index], false, true, false, false, false);
    this.distributeBuffer.Clear();
  }

  private void InternalDistributeDataMoveExcessiveCellsOld(
    TableCellDistributeContextOld tableContext,
    TableData nextTable,
    int headerCount)
  {
    int num = 0;
    for (int index = this.nodes.Count - 1; index > headerCount + tableContext.DataCellCount - 1; --index)
    {
      int count = this.nodes.Count;
      if (index >= count)
      {
        index = count - 1;
        LogManager.AddLine("     ---TableData.DistributeData: Invalid index ID: " + this.id, true);
      }
      RectangleElement node = this.nodes[index] as RectangleElement;
      if (node is TableData tableData && tableData.nextCell != null && tableData.nextCell.Page == this.NextTable.Page)
        tableData.OneStepUniteTable(true);
      if (this.nodes.Count != count && tableData != null)
        LogManager.AddLine($"     ---TableData.DistributeData: Invalid index ID: {this.id}, curCell.Id: {tableData.id}", true);
      if (node != null && (node.TableCellType == CellType.DataCell || !tableContext.HeaderCellsIsAvailable))
      {
        if (index < this.nodes.Count)
          this.RemoveChildNodeAt(index, true, false, false);
        else
          LogManager.AddLine("     ---TableData.DistributeData: Try remove at invalid index ID: " + this.id, true);
        if (this.distributeBuffer != null)
        {
          if (index <= tableContext.LastCellFromBuffer)
            this.distributeBuffer.Add(node);
          else
            this.distributeBuffer.Insert(num++, node);
        }
        else
        {
          if (nextTable.distributeBuffer == null)
            nextTable.distributeBuffer = new List<RectangleElement>(this.nodes.Count - (headerCount + tableContext.DataCellCount));
          nextTable.distributeBuffer.Add(node);
          nextTable.SetNeedUpdateLayoutFlag(true, false, false, false);
        }
      }
    }
  }

  private bool InternalDistributeDataIsNextCellFitInNewPageOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext,
    int headerCount)
  {
    bool flag = true;
    if (tableContext.CanVerticalDistribute && this.nextCell != null && this.nextCell.Page != this.page && (this.nextCell.Page.NextPageTemplateId == null || this.nextCell.Page.NextPageTemplateId == this.nextCell.Page.TemplateId) && context.FirstDataOnPage && (tableContext.DataCellCount == 0 || this.IsRow))
    {
      RectangleElement ownerNode = (RectangleElement) null;
      tableContext.СurrentCellIndex = headerCount + tableContext.DataCellCount;
      if (tableContext.СurrentCellIndex < this.nodes.Count)
        ownerNode = this.nodes[tableContext.СurrentCellIndex] as RectangleElement;
      else if (this.distributeBuffer != null && this.distributeBuffer.Count > 0)
        ownerNode = this.distributeBuffer[this.distributeBuffer.Count - 1];
      else if (this.nodes.Count > 0)
        ownerNode = this.nodes[this.nodes.Count - 1] as RectangleElement;
      if (!(ownerNode is TableData tableData) || !tableData.CanVerticalDistribute())
      {
        float tableFreeSpace = this.nextCell.GetTableFreeSpace();
        DistributeContext context1 = new DistributeContext((DocumentTreeNode) ownerNode, ownerNode.Size, new SizeF(this.nextCell.Size.Width, tableFreeSpace), tableContext.СurrentCellIndex == 0 || this.IsRow, tableContext.СurrentCellIndex - headerCount == 0 || this.IsRow, context);
        float sizeForDistribute = ownerNode.GetMinimalSizeForDistribute(context1);
        if ((double) tableFreeSpace < (double) sizeForDistribute)
          flag = false;
      }
    }
    return flag;
  }

  private bool InternalDistributeDataCreateNextPageIfNeedOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext)
  {
    bool nextPageIfNeedOld = false;
    if (this.nextCell == null && (tableContext.DataCellCount > 0 || this.page.NextPageTemplateId != null && this.page.NextPageTemplateId != this.page.TemplateId))
    {
      nextPageIfNeedOld = this.page.NextPage == null;
      this.AddNewTableAndParentsInDataFlow();
      if (this.NextTable != null)
        this.NextTable.TopLevelTable.GetGridColumnsParams(true);
      if (this.page.IsFinalPage)
      {
        context.VertDistributed = DistributeResult.BackToPrevious;
        if (this.TopLevelTable.PrevCell != null)
          this.TopLevelTable.PrevCell.SetNeedUpdateLayoutFlag(true, false, false, false);
      }
    }
    return nextPageIfNeedOld;
  }

  private void InternalDistributeDataProcessKeepWithNextOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext,
    int headerCount)
  {
    if (tableContext.FirstKeepWithNext == null || !(this.nodes[headerCount + tableContext.DataCellCount - 1] as RectangleElement).keepWithNext)
      return;
    tableContext.СurrentCellIndex = tableContext.FirstKeepWithNext.Index;
    if (tableContext.СurrentCellIndex >= headerCount && tableContext.СurrentCellIndex - headerCount > 0)
    {
      tableContext.DataCellCount = tableContext.СurrentCellIndex - headerCount;
    }
    else
    {
      if (context.FirstDataOnPage)
        return;
      context.VertDistributed = DistributeResult.None;
      tableContext.DataCellCount = 0;
    }
  }

  private void InternalDistributeDataHandleUndistributedCellOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext,
    TableCellDistributeContextOld cellContext,
    RectangleElement cell,
    int headerCount)
  {
    if (tableContext.DataCellCount == 0 || !this.isColumn && cellContext.VertDistributed == DistributeResult.None)
      context.VertDistributed = DistributeResult.None;
    else if (tableContext.DataCellCount + headerCount < this.nodes.Count || this.distributeBuffer != null && this.distributeBuffer.Count > 0 || this.NextTable != null && this.NextTable.distributeBuffer != null && this.NextTable.distributeBuffer.Count > 0)
      context.VertDistributed = DistributeResult.Part;
    if (!this.IsTopLevelTable || !this.isColumn || context.VertDistributed != DistributeResult.None)
      return;
    RectangleF bounds = cell.Bounds;
    double bottom1 = (double) bounds.Bottom;
    RectangleF distributeBounds = tableContext.DistributeBounds;
    double bottom2 = (double) distributeBounds.Bottom;
    if (bottom1 > bottom2)
    {
      ref RectangleF local = ref bounds;
      double height = (double) local.Height;
      double bottom3 = (double) bounds.Bottom;
      distributeBounds = tableContext.DistributeBounds;
      double bottom4 = (double) distributeBounds.Bottom;
      double num = bottom3 - bottom4;
      local.Height = (float) (height - num);
      cell.SetCellSizes(bounds, true, false, false, false);
    }
    cellContext.VertDistributed = DistributeResult.Part;
    tableContext.DataCellCount = 1;
  }

  /// <summary>Проверка результата разбивки</summary>
  /// <returns>true - если все хорошо и разбивку можно продолжать; false - выход из цикла</returns>
  private bool InternalDistributeDataCheckDistributionResultOld(
    DistributeContext context,
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld tableContext,
    TableCellDistributeContextOld cellContext)
  {
    if (dataEnumerator.Cell != null)
    {
      if (context.CanDistributeTopTable && !tableContext.CanVerticalSplit && cellContext.VertDistributed != DistributeResult.All)
        return false;
      if (!tableContext.CanVerticalSplit || cellContext.VertDistributed == DistributeResult.All || cellContext.VertDistributed == DistributeResult.Part)
      {
        if (dataEnumerator.Cell.IsVisibleNow)
        {
          this.AdjustSizeToCell(dataEnumerator.Cell, false, false);
          tableContext.PrevCellBounds = dataEnumerator.Cell.Bounds;
          if (this.IsColumn)
          {
            ref SizeF local = ref this.FreeSpace;
            RectangleF rectangleF = tableContext.DistributeBounds;
            double bottom1 = (double) rectangleF.Bottom;
            rectangleF = tableContext.PrevCellBounds;
            double bottom2 = (double) rectangleF.Bottom;
            double num = bottom1 - bottom2;
            local.Height = (float) num;
            if (context.CanDistributeTopTable && tableContext.CanVerticalDistribute && (double) this.FreeSpace.Height < 0.0)
            {
              cellContext.VertDistributed = DistributeResult.None;
              return false;
            }
          }
        }
        if (cellContext.VertDistributed == DistributeResult.Part)
          context.VertDistributed = DistributeResult.Part;
        ++tableContext.СurrentCellIndex;
        context.TryNotBreak_Failed |= cellContext.TryNotBreak_Failed;
        if (tableContext.FirstKeepWithNext == null && dataEnumerator.Cell.keepWithNext)
          tableContext.FirstKeepWithNext = dataEnumerator.Cell;
        else if (tableContext.FirstKeepWithNext != null && (!dataEnumerator.Cell.keepWithNext || !this.IsFixedStructureArea && cellContext.VertDistributed == DistributeResult.Part))
          tableContext.FirstKeepWithNext = (RectangleElement) null;
        if (context.CanDistributeTopTable && tableContext.CanVerticalDistribute)
        {
          if ((double) this.FreeSpace.Height == 0.0)
          {
            if (dataEnumerator.sourceTable.nextCell != null || dataEnumerator.sourceIndex < dataEnumerator.sourceTable.Nodes.Count)
              context.VertDistributed = DistributeResult.Part;
            return false;
          }
          if (cellContext.VertDistributed == DistributeResult.Part)
            return false;
        }
      }
      else
      {
        if (cellContext.VertDistributed == DistributeResult.BackToPrevious)
          dataEnumerator.Cell = (RectangleElement) null;
        return false;
      }
    }
    return true;
  }

  private void InternalDistributeDataRestoreSkipCellsAfterOld(
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld cellContext,
    float oldSkipCellsAfter)
  {
    if (dataEnumerator.Cell != null && this.isColumn && !this.isFixedStructureArea && (double) oldSkipCellsAfter != 0.0)
    {
      dataEnumerator.Cell.AssignSkipCellsAfter(oldSkipCellsAfter);
      RectangleF bounds = dataEnumerator.Cell.bounds with
      {
        Size = dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)
      };
      if ((double) cellContext.MaxSize.Height != 0.0 && (double) bounds.Height > (double) cellContext.MaxSize.Height)
      {
        bounds.Height = cellContext.MaxSize.Height;
        if ((double) bounds.Bottom < (double) dataEnumerator.Cell.properBounds.Bottom)
          bounds.Height = dataEnumerator.Cell.properBounds.Bottom - bounds.Y;
      }
      dataEnumerator.Cell.setBounds(bounds);
    }
    oldSkipCellsAfter = 0.0f;
  }

  private void InternalDistributeDataRemoveEmptyTailOld(
    int currentCellIndex,
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld cellContext)
  {
    this.RemoveChildNodeAt(currentCellIndex, false, false);
    --dataEnumerator.sourceIndex;
    dataEnumerator.Cell = (RectangleElement) null;
    cellContext.VertDistributed = DistributeResult.All;
  }

  private void InternalDistributeDataAdjustSkipLinesBeforeIfFirstCellOnPageOld(
    DistributeDataEnumeratorOld dataEnumerator,
    float cellMinHeight)
  {
    if ((double) dataEnumerator.Cell.properBounds.Y - (double) dataEnumerator.Cell.bounds.Y + (double) cellMinHeight <= (double) this.FreeSpace.Height)
      return;
    dataEnumerator.Cell.AssignSkipCellsBefore((this.FreeSpace.Height - cellMinHeight) / this.OneSkipSize);
    dataEnumerator.Cell.setProperBounds(new RectangleF(dataEnumerator.Cell.CalcProperLocation(this.bounds.Location), dataEnumerator.Cell.properBounds.Size));
    dataEnumerator.Cell.setBounds(new RectangleF(dataEnumerator.Cell.bounds.Location, dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)));
  }

  /// <summary>
  /// Рассчитать предполагаемый размер ячейки исходя из размера таблицы
  /// </summary>
  private SizeF InternalDistributeDataCalcPreferredCellSizeOld(
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld tableContext,
    out float cellMinHeight)
  {
    SizeF sizeF = this.CalcCellSize(dataEnumerator.Cell, tableContext.CalculatedProperTableBounds.Size, tableContext.RowsParams, out RowColParams _, tableContext.ColParams, out RowColParams _, true);
    cellMinHeight = dataEnumerator.Cell.MinHeight;
    if ((double) cellMinHeight < (double) tableContext.MinRowSize)
      cellMinHeight = tableContext.MinRowSize;
    if (this.isColumn && dataEnumerator.Cell.IsSingleCell || (double) sizeF.Height < (double) cellMinHeight)
      sizeF.Height = cellMinHeight;
    if ((double) tableContext.MinRowSize > 0.0 && (double) sizeF.Height < (double) tableContext.MinRowSize)
    {
      sizeF.Height = tableContext.MinRowSize;
      if ((double) cellMinHeight < (double) tableContext.MinRowSize)
        cellMinHeight = tableContext.MinRowSize;
    }
    return sizeF;
  }

  private void InternalDistributeDataUpdateCellLocationOld(
    TableCellDistributeContextOld tableContext,
    DistributeDataEnumeratorOld dataEnumerator,
    TableData cellTable)
  {
    PointF pointF = this.CalcCellLocation(tableContext.PrevCellBounds, dataEnumerator.Cell);
    RectangleF bounds = dataEnumerator.Cell.Bounds;
    PointF location = dataEnumerator.Cell.CalcProperLocation(pointF);
    if (!(bounds.Location != pointF) && !(dataEnumerator.Cell.properBounds.Location != location))
      return;
    dataEnumerator.Cell.setProperBounds(new RectangleF(location, dataEnumerator.Cell.properBounds.Size));
    dataEnumerator.Cell.setBounds(new RectangleF(pointF, dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)));
    if (cellTable != null)
      cellTable.RecalcCellLocations(pointF, 0, dataEnumerator.Cell.Nodes.Count, false, false, false);
    else
      dataEnumerator.Cell.AssignBoundsOnly(bounds, new RectangleF(pointF, bounds.Size));
    dataEnumerator.Cell.SetNeedUpdateUIGeometry(true, false);
  }

  private static void InternalDistributeDataCalcRelativeCellWidthOld(
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld tableContext)
  {
    if ((double) dataEnumerator.Cell.relativeWidth <= 0.0)
      return;
    RectangleF properBounds = dataEnumerator.Cell.properBounds with
    {
      Width = tableContext.CalculatedProperTableBounds.Width * (dataEnumerator.Cell.relativeWidth / 100f) - dataEnumerator.Cell.cellMargins.X - dataEnumerator.Cell.cellMargins.Width
    };
    dataEnumerator.Cell.AssignProperBounds(properBounds, false, false, false);
  }

  private void InternalDistributeDataRestorePrevCellBoundsOld(
    TableCellDistributeContextOld tableContext,
    TableCellDistributeContextOld prevCellContext)
  {
    if (!(this.nodes[tableContext.СurrentCellIndex - 1] is RectangleElement node))
      return;
    node.UpdateBoundsSkipAfter();
    if (node.IsVisibleNow)
    {
      RectangleF prevCellBounds = tableContext.PrevCellBounds with
      {
        Size = node.bounds.Size
      };
      tableContext.PrevCellBounds = prevCellBounds;
    }
    float num = prevCellContext != null ? prevCellContext.MaxSize.Height : tableContext.MaxSize.Height;
    if ((double) num == 0.0 || (double) tableContext.PrevCellBounds.Height <= (double) num)
      return;
    if (node.IsVisibleNow)
    {
      RectangleF prevCellBounds = tableContext.PrevCellBounds with
      {
        Height = num
      };
      tableContext.PrevCellBounds = prevCellBounds;
    }
    node.setBounds(tableContext.PrevCellBounds);
  }

  private float InternalDistributeDataResetSkipCellsFlagsOld(
    DistributeContext context,
    TableCellDistributeContextOld cellContext,
    DistributeDataEnumeratorOld dataEnumerator,
    TableData cellTable,
    int currentCellIndex)
  {
    if (this.isColumn && dataEnumerator.Cell.NonSkipBeforeAtStartPage && context.FirstOnPage && currentCellIndex == 0 && !this.isFixedStructureArea && (double) dataEnumerator.Cell.SkipCellsBefore != 0.0 && (cellTable == null || cellTable.IsFirstInFlow))
    {
      dataEnumerator.Cell.overrideFlags3 |= OverrideFlags3.IgnoreSkipBefore;
      dataEnumerator.Cell.setProperBounds(new RectangleF(dataEnumerator.Cell.bounds.Location, dataEnumerator.Cell.properBounds.Size));
      dataEnumerator.Cell.setBounds(new RectangleF(dataEnumerator.Cell.bounds.Location, dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)));
    }
    else
      dataEnumerator.Cell.overrideFlags3 &= ~OverrideFlags3.IgnoreSkipBefore;
    float skipCellsAfter = dataEnumerator.Cell.SkipCellsAfter;
    if (this.isColumn && !this.isFixedStructureArea && (double) skipCellsAfter != 0.0)
      dataEnumerator.Cell.AssignSkipCellsAfter(0.0f);
    if ((double) skipCellsAfter != (double) dataEnumerator.Cell.SkipCellsAfter)
    {
      dataEnumerator.Cell.setBounds(new RectangleF(dataEnumerator.Cell.bounds.Location, dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)));
      RectangleF bounds = dataEnumerator.Cell.bounds with
      {
        Size = dataEnumerator.Cell.CalcSizeFromProper(dataEnumerator.Cell.properBounds.Size)
      };
      if ((double) cellContext.MaxSize.Height != 0.0 && (double) bounds.Height > (double) cellContext.MaxSize.Height)
      {
        bounds.Height = cellContext.MaxSize.Height;
        if ((double) bounds.Bottom < (double) dataEnumerator.Cell.properBounds.Bottom)
          bounds.Height = dataEnumerator.Cell.properBounds.Bottom - bounds.Y;
      }
      dataEnumerator.Cell.setBounds(bounds);
    }
    return skipCellsAfter;
  }

  private void InternalDistributeDataUpdateBufferOld(
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld tableContext)
  {
    if (dataEnumerator.bufferCount == 0)
    {
      --dataEnumerator.sourceIndex;
    }
    else
    {
      dataEnumerator.sourceTable.distributeBuffer.RemoveAt(dataEnumerator.bufferCount - 1);
      tableContext.LastCellFromBuffer = tableContext.СurrentCellIndex;
    }
  }

  private bool InternalDistributeDataRemoveCellIfEmptyOld(
    DistributeDataEnumeratorOld dataEnumerator,
    TableCellDistributeContextOld cellContext)
  {
    if (dataEnumerator.Cell is TableData cell && cell.Parent != this && cell.prevCell != null && cell.AllFlowsIsEmpty())
    {
      dataEnumerator.Cell.UniteTable();
      if (cell.AllFlowsIsEmpty())
      {
        if (dataEnumerator.bufferCount > 0)
          dataEnumerator.sourceTable.distributeBuffer.RemoveAt(dataEnumerator.bufferCount - 1);
        if (dataEnumerator.Cell.Parent != null)
        {
          dataEnumerator.Cell.Remove(false, false);
          --dataEnumerator.sourceIndex;
          dataEnumerator.Cell = (RectangleElement) null;
          cellContext.VertDistributed = DistributeResult.All;
        }
        cell.SetPrevCell((RectangleElement) null);
        return true;
      }
    }
    return false;
  }

  private bool InternalDistributeDataCheckCellOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext,
    DistributeDataEnumeratorOld dataEnumerator,
    bool isFirstDataCell)
  {
    TableData cell = dataEnumerator.Cell as TableData;
    if (context.CanDistributeTopTable && tableContext.CanVerticalSplit)
    {
      if (dataEnumerator.Cell.fromNewPage && dataEnumerator.Cell.PrevCell == null && (cell == null || cell.IsFirstInFlow) && (!isFirstDataCell || !context.FirstDataOnPage))
        return false;
      if (dataEnumerator.Cell.OnOnePageWith != null && dataEnumerator.Cell.PrevCell == null)
      {
        PageData page = dataEnumerator.Cell.OnOnePageWith.Page;
        if (context.FirstPass)
          this.NeedSecondLayoutPass = true;
        else if (page == null || page != this.page && page.Index > this.page.Index)
          return false;
      }
    }
    return true;
  }

  private void InternalDistributeDataMoveBufferToNextTableOld(TableData nextTable)
  {
    if (this.distributeBuffer == null || this.distributeBuffer.Count <= 0)
      return;
    if (nextTable.distributeBuffer == null)
      nextTable.distributeBuffer = this.distributeBuffer;
    else
      nextTable.distributeBuffer.AddRange((IEnumerable<RectangleElement>) this.distributeBuffer);
    this.distributeBuffer = (List<RectangleElement>) null;
  }

  private TableCellDistributeContextOld InternalDistributeDataSwitchCellContextOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext,
    TableCellDistributeContextOld prevCellContext,
    DistributeDataEnumeratorOld dataEnumerator,
    bool isFirstDataCell)
  {
    TableCellDistributeContextOld distributeContextOld = new TableCellDistributeContextOld((DocumentTreeNode) dataEnumerator.Cell, dataEnumerator.Cell.Size, this.FreeSpace, tableContext.СurrentCellIndex == 0 || this.IsRow, isFirstDataCell || this.IsRow, context);
    distributeContextOld.KeepWithNext_IsFirstDataOnPage = context.KeepWithNext_IsFirstDataOnPage && (prevCellContext != null ? prevCellContext.KeepWithNext_IsFirstDataOnPage : tableContext.KeepWithNext_IsFirstDataOnPage);
    if (this.IsColumn)
    {
      distributeContextOld.KeepWithNext_IsFirstDataOnPage &= dataEnumerator.Cell.keepWithNext;
      if (!distributeContextOld.KeepWithNext_IsFirstDataOnPage)
        context.KeepWithNext_IsFirstDataOnPage = this.TryNotBreak;
    }
    return distributeContextOld;
  }

  private DistributeDataEnumeratorOld InternalDistributeDataGetCellEnumeratorOld(
    DistributeContext context,
    TableCellDistributeContextOld tableContext)
  {
    DistributeDataEnumeratorOld cellEnumeratorOld = new DistributeDataEnumeratorOld(this, tableContext.HeaderCellsIsAvailable, tableContext.CanVerticalDistribute);
    cellEnumeratorOld.sourceIndex = tableContext.СurrentCellIndex;
    if (tableContext.CanVerticalDistribute && (double) this.FreeSpace.Height < 0.0)
    {
      context.VertDistributed = DistributeResult.None;
      cellEnumeratorOld.sourceTable = (TableData) null;
    }
    return cellEnumeratorOld;
  }

  private TableCellDistributeContextOld InternalDistributeDataGetTableContextOld(
    DistributeContext context,
    int headerCount)
  {
    TableCellDistributeContextOld tableContextOld = new TableCellDistributeContextOld();
    tableContextOld.CanDistributeTopTable = context.CanDistributeTopTable;
    tableContextOld.VertDistributed = DistributeResult.All;
    tableContextOld.CanVerticalDistribute = this.CanVerticalDistribute();
    tableContextOld.CanVerticalSplit = this.CanVerticalSplit();
    tableContextOld.ParentCell = this.ParentCell;
    tableContextOld.ColParams = this.GridColumnsParams;
    tableContextOld.RowsParams = this.GridRowsParams;
    RectangleF realProperBounds = this.RealProperBounds with
    {
      Height = this.minHeight
    };
    tableContextOld.CalculatedProperTableBounds = realProperBounds;
    tableContextOld.DistributeBounds = new RectangleF(this.Bounds.Location, context.MaxSize);
    tableContextOld.MinRowSize = this.GetMinRowSizeFromContextOld(context);
    tableContextOld.PrevCellBounds = this.GetFirstCellBoundsForStartDistributeTableDataOld(headerCount, tableContextOld.CalculatedProperTableBounds.Location);
    if (this.IsColumn)
    {
      RectangleF rectangleF = tableContextOld.DistributeBounds;
      double bottom1 = (double) rectangleF.Bottom;
      rectangleF = tableContextOld.PrevCellBounds;
      double bottom2 = (double) rectangleF.Bottom;
      this.FreeSpace = new SizeF(float.MaxValue, (float) (bottom1 - bottom2));
    }
    else
      this.FreeSpace = new SizeF(float.MaxValue, tableContextOld.DistributeBounds.Height);
    tableContextOld.FirstKeepWithNext = (RectangleElement) null;
    tableContextOld.HeaderCellsIsAvailable = this.isColumn || tableContextOld.ColParams == null;
    tableContextOld.СurrentCellIndex = headerCount;
    return tableContextOld;
  }

  /// <summary>Координаты предыдущей ячейки для расчета координат текущей в контексте разбивки таблицы</summary>
  /// <param name="headerCount">Количество ячеек заголовка в таблице</param>
  /// <param name="tableLocation">Текущее положение таблицы, для распределения ячеек</param>
  /// <returns></returns>
  private RectangleF GetFirstCellBoundsForStartDistributeTableDataOld(
    int headerCount,
    PointF tableLocation)
  {
    RectangleF distributeTableDataOld = new RectangleF(tableLocation, new SizeF(0.0f, 0.0f));
    if (headerCount > 0)
    {
      for (int index = headerCount - 1; index >= 0; --index)
      {
        if (((VisualNode) this.nodes[index]).IsVisibleNow)
        {
          distributeTableDataOld = ((RectangleElement) this.nodes[index]).Bounds;
          break;
        }
      }
    }
    return distributeTableDataOld;
  }

  /// <summary>Получить минимальный размер строки с учётом контекста при разбивке</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private float GetMinRowSizeFromContextOld(DistributeContext context)
  {
    float rowSizeNn = context.RowSize_NN;
    return (double) rowSizeNn == 0.0 ? this.GetMinRowSize(this.GridRowsParams) : rowSizeNn;
  }

  /// <summary>Изменить количество свободного пространства для таблицы, если необходимо</summary>
  /// <param name="context"></param>
  private void RecalcFreeSpaceForTableHierarchyOld(DistributeContext context)
  {
    if (this.nextCell == null || !this.nextCell.AllFlowsIsEmpty())
      return;
    DistributeContext distributeContext = context;
    TableData tableData = this;
    List<TableData> tableDataList = new List<TableData>();
    List<DistributeContext> distributeContextList = new List<DistributeContext>();
    for (; distributeContext != null && tableData != null; tableData = tableData.ParentCell)
    {
      distributeContextList.Add(distributeContext);
      tableDataList.Add(tableData);
      distributeContext = distributeContext.ParentContext;
    }
    float num = 0.0f;
    for (int index = tableDataList.Count - 1; index >= 0; --index)
    {
      if ((double) distributeContextList[index].SkipSizeAfter != (double) tableDataList[index].SkipSizeAfter)
      {
        num -= tableDataList[index].SkipSizeAfter - distributeContextList[index].SkipSizeAfter;
        distributeContextList[index].SkipSizeAfter = tableDataList[index].SkipSizeAfter;
      }
      tableDataList[index].FreeSpace.Height += num;
    }
  }

  private class ReadCellContext
  {
    public PointF currCellLocation;
    public RectangleF prevBounds;
    public RectangleF properBounds_Real;
    public float calculatedTableWidth;
    public float calculatedTableHeight;
    public float maxMinHeight;
    public bool isAVSDocRowTemplate;
    public int gridIndex = -1;
    public int prevColumnIndex = -1;
    public bool notNeedUpdate_IsSelectedDataCellTemplate;
    public TableData parentCell;
    public RectangleElement prevChildCell;

    public RectangleElement LastCell => this.prevChildCell;
  }
}
