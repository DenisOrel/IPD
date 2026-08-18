// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RectangleElement
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Прямоугольный элемент</summary>
[Serializable]
public abstract class RectangleElement : PageElementNode
{
  /// <summary>Размер по умолчанию</summary>
  public static SizeF DefaultSize = new SizeF(20f, 5f);
  /// <summary>Минимальный размер</summary>
  public static SizeF MinimalSize = new SizeF(2f, 2f);
  /// <summary>Значение типа float соответствующее неустановленному значению (вместо null)</summary>
  public static readonly float EmptyFloatValue = float.MinValue;
  /// <summary>Значение типа int соответствующее неустановленному значению (вместо null)</summary>
  public static readonly int EmptyIntValue = int.MinValue;
  /// <summary>Значение типа PointF соответствующее неустановленному значению (вместо null)</summary>
  public static readonly PointF EmptyPointF = new PointF(RectangleElement.EmptyFloatValue, RectangleElement.EmptyFloatValue);
  /// <summary>Значение типа SizeF соответствующее неустановленному значению (вместо null)</summary>
  public static readonly SizeF EmptySizeF = new SizeF(RectangleElement.EmptyFloatValue, RectangleElement.EmptyFloatValue);
  /// <summary>Значение типа RectangleF соответствующее неустановленному значению (вместо null)</summary>
  public static readonly RectangleF EmptyRectangleF = new RectangleF(RectangleElement.EmptyFloatValue, RectangleElement.EmptyFloatValue, RectangleElement.EmptyFloatValue, RectangleElement.EmptyFloatValue);
  /// <summary>Размер полей по умолчанию</summary>
  protected static float DefaultBorderWidth = 0.0f;
  private static BorderLine _defaultBorderLine = new BorderLine();
  /// <summary>Цвет невидимых линий</summary>
  public static Color InvisibleLineColor = Color.LightGray;
  /// <summary>Словарь методов чтения полей из XML</summary>
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private static readonly float maxFloatMistake = 0.0005f;
  private static readonly float maxFloatMistakeDiv2 = 0.00025f;
  /// <summary>Ячейка продолжение</summary>
  protected RectangleElement nextCell;
  /// <summary>Предыдущая ячейка</summary>
  protected RectangleElement prevCell;
  private string _overrideTemplateId;
  /// <summary>Располагать элемент на одной странице с данным</summary>
  internal RectangleElement onOnePageWith;
  /// <summary>Положение в сетке по умолчанию</summary>
  private static TableGridPosition defaultGridPos = new TableGridPosition();
  internal bool fromNewPage;
  internal bool tryNotBreak;
  internal bool keepWithNext;
  internal int desiredPageNumber = -1;
  public float defaultRowSize;
  /// <summary>Позиция в сетке</summary>
  private TableGridPosition gridPos;
  /// <summary>Количество пропусков перед (может быть нецелым)</summary>
  protected float skipCellsBefore;
  /// <summary>Количество пропусков после (может быть нецелым)</summary>
  protected float skipCellsAfter;
  protected bool ignoreSkipOuterCells;
  protected bool drawEllipse;
  /// <summary>Собственное положение ячейки
  /// (положение без учета пропусков строк и столбцов)</summary>
  public RectangleF properBounds = RectangleElement.EmptyRectangleF;
  /// <summary>Границы, в миллиметрах</summary>
  public RectangleF bounds = RectangleElement.EmptyRectangleF;
  protected float minHeight;
  /// <summary>Максимальная высота ячейки. Если 0, то высота неограниченна сверху</summary>
  protected float maxHeight;
  protected float minWidth;
  internal float relativeWidth;
  internal float relativeHeight;
  /// <summary>Временное(?) поле. Отступы для ячеек импортированных из бланка</summary>
  public RectangleF cellMargins = RectangleF.Empty;
  private ElementHorizontalAlign horzAlign;
  private ElementVerticalAlign vertAlign;
  /// <summary>Линии границ прямоугольника</summary>
  public RectangleBorder borders;
  /// <summary>Цвет переднего плана</summary>
  private Color foreColor = Color.Empty;
  /// <summary>Цвет фона</summary>
  private Color backColor = Color.Empty;
  /// <summary>Тип ячейки таблицы</summary>
  private CellType tableCellType;
  /// <summary>Вариант отображения заголовка, если элемент является заголовком</summary>
  protected HeaderShowType headerShowType;
  /// <summary>Размер полей в миллиметрах</summary>
  protected float borderWidth = RectangleElement.DefaultBorderWidth;
  private DocumentTreeNode owner;
  /// <summary>Флаги для внутреннего пользования (вместо булевских полей)</summary>
  [NonSerialized]
  protected CellFlags cellFlags;

  /// <summary>Стиль линии по умолчанию</summary>
  [Browsable(false)]
  public BorderLine DefaultBorderLine
  {
    get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null ? ownerDocument.DefaultBorderLine : RectangleElement._defaultBorderLine;
    }
  }

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.AssignIsSelectedDataCellTemplate(true);
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected RectangleElement(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public RectangleElement(DocumentTreeNode parent, RectangleF bounds, bool visible)
  {
    int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateGeometryRefreshUI();
    this.SetParent(parent, visible, visible);
    this.SetVisible(visible, false, false, false, true, false);
    if ((double) bounds.Width == 0.0 && (double) bounds.Height == 0.0)
      bounds.Size = RectangleElement.DefaultSize;
    this.minHeight = bounds.Height;
    this.minWidth = bounds.Width;
    this.AssignBounds(bounds, false, false, false);
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
    {
      RectangleF rectangleF1 = bounds;
      RectangleF rectangleF2 = parentCell.CalcRealProperBounds(this.ProperBounds);
      rectangleF1.Location = new PointF(bounds.X - rectangleF2.X, bounds.Y - rectangleF2.Y);
      this.AssignProperBounds(rectangleF1, false, false, false);
    }
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
    if (num != 0)
      return;
    this.ResumeUpdateRefreshUI(visible, visible);
  }

  /// <summary>Конструктор</summary>
  public RectangleElement()
  {
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод InitFields()</param>
  public RectangleElement(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Статический конструктор</summary>
  static RectangleElement() => RectangleElement.InitReadFieldDict();

  /// <summary>Сбросить кэш изображения в TextBoxElement</summary>
  public virtual void ResetTextBoxPaintCache()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node)
        node.ResetTextBoxPaintCache();
    }
  }

  /// <summary>Показывать на экране, что узел выбран</summary>
  public override bool ShowSelected
  {
    [DebuggerStepThrough] get => this.IsVirtualNode || base.ShowSelected;
  }

  /// <summary>Видимый в данный момент.
  /// В некоторых условиях элемент может не отображаться в текущий момент.
  /// Например, невыбранные варианты строк данных в шаблоне таблицы</summary>
  [Browsable(false)]
  public override bool IsVisibleNow
  {
    get
    {
      if (!base.IsVisibleNow)
        return false;
      return !this.SwitchVisibleThisDataCellInTemplateIsEnabled || this.IsSelectedDataCellTemplate;
    }
  }

  public override ShowOnPageOnly ShowOnPageOnly
  {
    get => this.ParentCell != null ? ShowOnPageOnly.All : base.ShowOnPageOnly;
  }

  /// <summary>Для этой ячейки можно переключать видимость в шаблоне таблицы.
  /// Возможно в принципе, но может  быть отключено настройками см. SwitchVisibleThisDataCellInTemplateIsEnabled</summary>
  [Browsable(false)]
  public virtual bool CanSwitchVisibleThisDataCellInTemplate
  {
    get => this.IsTemplate && this.IsTableCell && !this.CloneByTemplateWithParent;
  }

  /// <summary>Для этой ячейки можно переключать видимость в шаблоне таблицы. Зависит от настроек</summary>
  [Browsable(false)]
  public bool SwitchVisibleThisDataCellInTemplateIsEnabled
  {
    get
    {
      return ((int) this.ParentCell?.ShowSingleCellInTemplate ?? 0) != 0 && this.CanSwitchVisibleThisDataCellInTemplate;
    }
  }

  /// <summary>Найти в иерархии элемент, который может переключать свою видимость в шаблоне</summary>
  /// <returns></returns>
  public RectangleElement FindSwitchableDataCellInHierarchy()
  {
    if (this.CanSwitchVisibleThisDataCellInTemplate)
      return this;
    return this.ParentCell != null ? this.ParentCell.FindSwitchableDataCellInHierarchy() : (RectangleElement) null;
  }

  /// <summary>Ячейка выбрана для показа в шаблоне таблицы</summary>
  [Browsable(false)]
  public virtual bool IsSelectedDataCellTemplate
  {
    get => this.HasCellFlags(CellFlags.SelectedDataCellTemplate);
    set => this.SetIsSelectedDataCellTemplate(value, true);
  }

  /// <summary>Установить значение IsSelectedDataCellTemplate</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updateUI">Обновить внешний вид</param>
  public void AssignIsSelectedDataCellTemplate(bool value)
  {
    if (this.IsSelectedDataCellTemplate == value)
      return;
    if (value)
      this.SetCellFlags(CellFlags.SelectedDataCellTemplate);
    else
      this.ResetCellFlags(CellFlags.SelectedDataCellTemplate);
  }

  /// <summary>Установить значение IsSelectedDataCellTemplate</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updateUI">Обновить внешний вид</param>
  public void SetIsSelectedDataCellTemplate(bool value, bool updateUI)
  {
    if (this.IsSelectedDataCellTemplate == value)
      return;
    this.AssignIsSelectedDataCellTemplate(value);
    this.SetNeedUpdateLayoutFlag(true, false, updateUI, updateUI);
    this.OnVisibleChanged(new VisibleChanged_EventArgs());
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
    if (excludeNode == this)
      return;
    SnapPoint snapPoint = (SnapPoint) null;
    float num1 = 0.0f;
    RectangleF properBounds = this.ProperBounds;
    PointF location1 = properBounds.Location;
    PointF pointF1 = new PointF(properBounds.X, properBounds.Bottom);
    PointF pointF2 = new PointF(properBounds.Right, properBounds.Y);
    PointF pointF3 = new PointF(properBounds.Right, properBounds.Bottom);
    float num2 = UnitsConverter.LineLength(location1, originalPoint);
    if ((double) num2 <= (double) snapSize && (double) num2 < (double) num1)
    {
      snapPoint = new SnapPoint(location1, SnapPointType.Node);
      num1 = num2;
    }
    float num3 = UnitsConverter.LineLength(pointF1, originalPoint);
    if ((double) num3 <= (double) snapSize && (snapPoint == null || (double) num3 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF1, SnapPointType.Node);
      num1 = num3;
    }
    float num4 = UnitsConverter.LineLength(pointF2, originalPoint);
    if ((double) num4 <= (double) snapSize && (snapPoint == null || (double) num4 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF2, SnapPointType.Node);
      num1 = num4;
    }
    float num5 = UnitsConverter.LineLength(pointF3, originalPoint);
    if ((double) num5 <= (double) snapSize && (snapPoint == null || (double) num5 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF3, SnapPointType.Node);
      num1 = num5;
    }
    RectangleF bounds = this.Bounds;
    if (properBounds != bounds)
    {
      PointF location2 = bounds.Location;
      PointF pointF4 = new PointF(bounds.X, bounds.Bottom);
      PointF pointF5 = new PointF(bounds.Right, bounds.Y);
      PointF pointF6 = new PointF(bounds.Right, bounds.Bottom);
      float num6;
      if (location2 != location1 && (double) (num6 = UnitsConverter.LineLength(location2, originalPoint)) <= (double) snapSize && (snapPoint == null || (double) num6 < (double) num1))
      {
        snapPoint = new SnapPoint(location2, SnapPointType.Node);
        num1 = num6;
      }
      float num7;
      if (pointF4 != pointF1 && (double) (num7 = UnitsConverter.LineLength(pointF4, originalPoint)) <= (double) snapSize && (snapPoint == null || (double) num7 < (double) num1))
      {
        snapPoint = new SnapPoint(pointF4, SnapPointType.Node);
        num1 = num7;
      }
      float num8;
      if (pointF5 != pointF2 && (double) (num8 = UnitsConverter.LineLength(pointF5, originalPoint)) <= (double) snapSize && (snapPoint == null || (double) num8 < (double) num1))
      {
        snapPoint = new SnapPoint(pointF5, SnapPointType.Node);
        num1 = num8;
      }
      float num9;
      if (pointF6 != pointF3 && (double) (num9 = UnitsConverter.LineLength(pointF6, originalPoint)) <= (double) snapSize && (snapPoint == null || (double) num9 < (double) num1))
      {
        snapPoint = new SnapPoint(pointF6, SnapPointType.Node);
        num1 = num9;
      }
    }
    if (snapPoint == null)
    {
      float num10 = Math.Abs(originalPoint.X - bounds.X);
      if ((double) num10 < (double) snapSize && (double) num10 < (double) num1)
      {
        snapPoint = new SnapPoint(new PointF(bounds.X, originalPoint.Y), SnapPointType.LineX);
        num1 = num10;
      }
      float num11 = Math.Abs(originalPoint.X - bounds.Right);
      if ((double) num11 < (double) snapSize && (snapPoint == null || (double) num11 < (double) num1))
      {
        snapPoint = new SnapPoint(new PointF(bounds.Right, originalPoint.Y), SnapPointType.LineX);
        num1 = num11;
      }
      float num12 = Math.Abs(originalPoint.Y - bounds.Y);
      if ((double) num12 < (double) snapSize && (snapPoint == null || (double) num12 < (double) num1))
      {
        snapPoint = new SnapPoint(new PointF(originalPoint.X, bounds.Y), SnapPointType.LineY);
        num1 = num12;
      }
      float num13 = Math.Abs(originalPoint.Y - bounds.Bottom);
      if ((double) num13 < (double) snapSize && (snapPoint == null || (double) num13 < (double) num1))
      {
        snapPoint = new SnapPoint(new PointF(originalPoint.X, bounds.Bottom), SnapPointType.LineY);
        num1 = num13;
      }
      TableData parentCell = this.ParentCell;
      if (parentCell != null)
      {
        float skipCellsBefore = this.SkipCellsBefore;
        float skipCellsAfter = this.SkipCellsAfter;
        if (parentCell.IsColumn)
        {
          float num14 = Math.Abs(originalPoint.Y - properBounds.Y);
          if ((double) num14 < (double) snapSize && (snapPoint == null || (double) num14 < (double) num1))
          {
            snapPoint = new SnapPoint(new PointF(originalPoint.X, properBounds.Y), SnapPointType.LineY);
            num1 = num14;
          }
          float num15 = Math.Abs(originalPoint.Y - properBounds.Bottom);
          if ((double) num15 < (double) snapSize && (snapPoint == null || (double) num15 < (double) num1))
            snapPoint = new SnapPoint(new PointF(originalPoint.X, properBounds.Bottom), SnapPointType.LineY);
        }
        else
        {
          if ((double) skipCellsBefore > 0.0)
          {
            float num16 = Math.Abs(originalPoint.X - properBounds.X);
            if ((double) num16 < (double) snapSize && (snapPoint == null || (double) num16 < (double) num1))
            {
              snapPoint = new SnapPoint(new PointF(properBounds.X, originalPoint.Y), SnapPointType.LineX);
              num1 = num16;
            }
          }
          if ((double) skipCellsAfter > 0.0)
          {
            float num17 = Math.Abs(originalPoint.X - properBounds.Right);
            if ((double) num17 < (double) snapSize && (snapPoint == null || (double) num17 < (double) num1))
              snapPoint = new SnapPoint(new PointF(properBounds.Right, originalPoint.Y), SnapPointType.LineX);
          }
        }
      }
    }
    if (snapPoint == null)
      return;
    snapPointList.Add(snapPoint);
  }

  /// <summary>Получить все одиночные ячейки виртуальной ячейки</summary>
  /// <returns></returns>
  public List<DocumentTreeNode> GetSingleCells()
  {
    List<DocumentTreeNode> cur_var = new List<DocumentTreeNode>();
    this.GetSingleCells(this, ref cur_var);
    return cur_var;
  }

  /// <summary>Получить все одиночные ячейки виртуальной ячейки</summary>
  /// <param name="cell"></param>
  /// <param name="cur_var"></param>
  /// <param name="hasleft"></param>
  private void GetSingleCells(RectangleElement cell, ref List<DocumentTreeNode> cur_var)
  {
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return;
      this.GetSingleCells(cell.Nodes[0] as RectangleElement, ref cur_var);
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
        this.GetSingleCells(cell.Nodes[index] as RectangleElement, ref cur_var);
    }
    else
      cur_var.Add((DocumentTreeNode) cell);
  }

  /// <summary>Получить все реальные ячейки виртуальной ячейки</summary>
  /// <returns></returns>
  public List<DocumentTreeNode> GetRealCells()
  {
    List<DocumentTreeNode> cur_var = new List<DocumentTreeNode>();
    this.GetRealCells(this, ref cur_var);
    return cur_var;
  }

  /// <summary>Получить все реальные ячейки виртуальной ячейки</summary>
  /// <param name="cell"></param>
  /// <param name="cur_var"></param>
  /// <param name="hasleft"></param>
  private void GetRealCells(RectangleElement cell, ref List<DocumentTreeNode> cur_var)
  {
    if (!cell.IsSingleCell)
    {
      if (!cell.IsVirtualNode)
      {
        cur_var.Add((DocumentTreeNode) cell);
      }
      else
      {
        if (cell.Nodes.Count == 0)
          return;
        this.GetRealCells(cell.Nodes[0] as RectangleElement, ref cur_var);
        int index = 1;
        for (int count = cell.Nodes.Count; index < count; ++index)
          this.GetRealCells(cell.Nodes[index] as RectangleElement, ref cur_var);
      }
    }
    else
      cur_var.Add((DocumentTreeNode) cell);
  }

  /// <summary>Получить параметры строки для этой ячейки</summary>
  public RowColParams GetGridRowParams()
  {
    RowColParams gridRowParams = (RowColParams) null;
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
    {
      if (parentCell.IsColumn)
      {
        int gridRowIndex = this.GetGridRowIndex();
        if (gridRowIndex != -1)
        {
          List<RowColParams> gridRowsParams = parentCell.GridRowsParams;
          if (gridRowsParams != null && gridRowIndex < gridRowsParams.Count)
            gridRowParams = gridRowsParams[gridRowIndex];
        }
      }
      else
        gridRowParams = parentCell.GetGridRowParams();
    }
    return gridRowParams;
  }

  /// <summary>Получить параметры столбца для этой ячейки</summary>
  public RowColParams GetGridColumnParams()
  {
    RowColParams gridColumnParams = (RowColParams) null;
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
    {
      if (parentCell.IsRow)
      {
        int gridColumnIndex = this.GetGridColumnIndex();
        if (gridColumnIndex != -1)
        {
          List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
          if (gridColumnsParams != null && gridColumnIndex < gridColumnsParams.Count)
            gridColumnParams = gridColumnsParams[gridColumnIndex];
        }
      }
      else
        gridColumnParams = parentCell.GetGridColumnParams();
    }
    return gridColumnParams;
  }

  /// <summary>Ширина ячейки перекрыта (ни от кого не наследуется)</summary>
  [Category("Debug")]
  public bool WidthOverrided
  {
    [DebuggerStepThrough] get
    {
      return (this.overrideFlags & OverrideFlags.Width) != OverrideFlags.None || (this.overrideFlags2 & OverrideFlags2.ColumnWidth) != 0;
    }
    set
    {
      if (value)
      {
        this.overrideFlags |= OverrideFlags.Width;
        this.overrideFlags2 |= OverrideFlags2.ColumnWidth;
      }
      else
      {
        this.overrideFlags &= ~OverrideFlags.Width;
        this.overrideFlags2 &= ~OverrideFlags2.ColumnWidth;
      }
    }
  }

  /// <summary>Высота ячейки перекрыта (ни от кого не наследуется)</summary>
  [Category("Debug")]
  public bool HeightOverrided
  {
    [DebuggerStepThrough] get
    {
      return this.IsOverridden(OverrideFlags.Height) || this.IsOverridden2(OverrideFlags2.RowHeight);
    }
    set
    {
      if (value)
      {
        this.SetOverrideFlags(OverrideFlags.Height);
        this.SetOverrideFlags2(OverrideFlags2.RowHeight);
      }
      else
      {
        this.ResetOverrideFlags(OverrideFlags.Height);
        this.ResetOverrideFlags2(OverrideFlags2.RowHeight);
      }
    }
  }

  /// <summary>Получить имя элемента</summary>
  /// <returns>Имя элемента</returns>
  public override string GetName()
  {
    string str = base.GetName();
    if (DocumentTreeNode.IsEmptyString(str))
    {
      RowColParams gridColumnParams = this.GetGridColumnParams();
      if (gridColumnParams != null)
        str = gridColumnParams.ColRowName;
    }
    return str;
  }

  /// <summary>Имя узла</summary>
  public override string Name
  {
    [DebuggerStepThrough] get => base.Name;
    set
    {
      if (!(this.Name != value))
        return;
      base.Name = value;
    }
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("Interfaces.Document_82");
    set => base.NodeTypeCaption = value;
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (this.HasTemplate())
      properties.SetReadOnlyProperty("DrawEllipse", true);
    TableData parentCell = this.ParentCell;
    if (!ImDocumentData.ShowDebugInfo)
    {
      this.RemoveProperty(properties, "MinHeight");
      this.RemoveProperty(properties, "ContentHeight");
      this.RemoveProperty(properties, "MinWidth");
      this.RemoveProperty(properties, "GridColIndex");
      this.RemoveProperty(properties, "GridRowIndex");
      this.RemoveProperty(properties, "GridPos");
      this.RemoveProperty(properties, "IsDefaultGridPos");
      this.RemoveProperty(properties, "IsFixedSizeRows");
      this.RemoveProperty(properties, "WidthOverrided");
      this.RemoveProperty(properties, "HeightOverrided");
      this.RemoveProperty(properties, "NextCell");
      this.RemoveProperty(properties, "PrevCell");
      this.RemoveProperty(properties, "Bounds");
      this.RemoveProperty(properties, "ProperBounds");
      this.RemoveProperty(properties, "RealProperBounds");
      this.RemoveProperty(properties, "OnOnePageWith");
      this.RemoveProperty(properties, "NeedUpdateFormulas");
      this.RemoveProperty(properties, "TryNotBreak_Failed0");
      this.RemoveProperty(properties, "TryNotBreak_Failed1");
      this.RemoveProperty(properties, "IsDynamicGroupHeader");
      this.RemoveProperty(properties, "GroupCellText");
      this.RemoveProperty(properties, "GroupCellOriginalText");
      this.RemoveProperty(properties, "GroupHeaderText");
      this.RemoveProperty(properties, "HasGroupHeaderText");
      this.RemoveProperty(properties, "GroupCellTextForGroup");
      this.RemoveProperty(properties, "IsDistributing");
      if (parentCell != null || !(this is TableData))
        this.RemoveProperty(properties, "MaxHeight");
      if (parentCell == null && !this.IsFormulaLib)
      {
        this.RemoveProperty(properties, "VertAlign");
        this.RemoveProperty(properties, "HorzAlign");
      }
    }
    if (this is TableData)
      this.RemoveProperty(properties, "InnerBorderLine");
    if (!ImDocumentData.ShowDebugInfo && !this.IsTemplate || parentCell == null || parentCell.IsRow || !this.IsHeaderCell)
      this.RemoveProperty(properties, "OverrideTemplateIdForUI");
    if (parentCell != null)
    {
      if (!parentCell.IsFixedStructureArea)
      {
        this.RemoveProperty(properties, "VertAlign");
        this.RemoveProperty(properties, "HorzAlign");
      }
      this.RemoveProperty(properties, "LeftMargin");
      this.RemoveProperty(properties, "TopMargin");
      this.RemoveProperty(properties, "BottomMargin");
      this.RemoveProperty(properties, "RightMargin");
      if (parentCell.IsRow || parentCell.IsFixedStructureArea)
      {
        this.RemoveProperty(properties, "IgnoreSkipOuterCells");
        this.RemoveProperty(properties, "NonSkipBeforeAtStartPage");
        this.RemoveProperty(properties, "TableCellType");
        this.RemoveProperty(properties, "SkipCellsAfter");
        this.RemoveProperty(properties, "SkipCellsBefore");
        if (parentCell.IsFixedStructureArea)
          this.RemoveProperty(properties, "KeepWithNext");
      }
      else if (!this.IsTemplate && this.Template == null)
        properties.SetReadOnlyProperty("TableCellType", true);
      if (!parentCell.IsFixedStructureArea)
      {
        properties.SetReadOnlyProperty("LeftForUser", true);
        properties.SetReadOnlyProperty("TopForUser", true);
      }
      if (parentCell.IsRow)
        properties.SetReadOnlyProperty("Visible", true);
      this.RemoveProperty(properties, "ShowOnPageOnlyVisual");
    }
    else
    {
      if (this.HasTemplate())
      {
        properties.SetReadOnlyProperty("HorzAlign", true);
        properties.SetReadOnlyProperty("VertAlign", true);
      }
      this.RemoveProperty(properties, "TableCellType");
      this.RemoveProperty(properties, "IgnoreSkipOuterCells");
      this.RemoveProperty(properties, "NonSkipBeforeAtStartPage");
      this.RemoveProperty(properties, "SkipCellsAfter");
      this.RemoveProperty(properties, "SkipCellsBefore");
      this.RemoveProperty(properties, "KeepWithNext");
      this.RemoveProperty(properties, "TryNotBreak");
      this.RemoveProperty(properties, "FromNewPage");
    }
    TableData topLevelTable = this.TopLevelTable;
    if (topLevelTable != null && !topLevelTable.IsPageFlow)
    {
      this.RemoveProperty(properties, "FromNewPage");
      this.RemoveProperty(properties, "KeepWithNext");
      this.RemoveProperty(properties, "TryNotBreak");
    }
    if (this.IsVirtualNode)
    {
      this.RemoveProperty(properties, "IsPageFlow");
      this.RemoveProperty(properties, "Reference");
      this.RemoveProperty(properties, "MaxHeight");
      this.RemoveProperty(properties, "Id");
      this.RemoveProperty(properties, "ColumnName");
      this.RemoveProperty(properties, "RowName");
      properties.SetReadOnlyProperty("LeftForUser", true);
      properties.SetReadOnlyProperty("RightForUser", true);
      properties.SetReadOnlyProperty("TopForUser", true);
      properties.SetReadOnlyProperty("BottomForUser", true);
    }
    if (this.HasTemplate())
    {
      properties.SetReadOnlyProperty("NodeTypeCaption", true);
      properties.SetReadOnlyProperty("MaxHeight", true);
      properties.SetReadOnlyProperty("BottomForUser", true);
      properties.SetReadOnlyProperty("LeftForUser", true);
      properties.SetReadOnlyProperty("RightForUser", true);
      properties.SetReadOnlyProperty("TopForUser", true);
      properties.SetReadOnlyProperty("HeightForUser", true);
      properties.SetReadOnlyProperty("WidthForUser", true);
      properties.SetReadOnlyProperty("DefaultRowSize", true);
      properties.SetReadOnlyProperty("IsFixedSizeRows", true);
      properties.SetReadOnlyProperty("TableCellType", true);
    }
    if (this.IsDynamicGroupHeader)
      properties.SetReadOnlyProperty("FromNewPage", true);
    if (this.IsHeaderCell && this.IsTableCell)
      return;
    this.RemoveProperty(properties, "HeaderShowType");
  }

  /// <summary>Имеет ли элементы шаблон</summary>
  public override bool HasTemplate()
  {
    if (this.IsVirtualNode)
    {
      List<DocumentTreeNode> realCells = this.GetRealCells();
      for (int index = 0; index < realCells.Count; ++index)
      {
        if (realCells[index].TemplateId != null)
          return true;
      }
    }
    else if (this.TemplateId != null)
      return true;
    return false;
  }

  /// <summary>Установить стиль рамки для элемента и всех подячеек</summary>
  /// <param name="top">Стиль верней линии рамки</param>
  /// <param name="left">Стиль левой линии рамки</param>
  /// <param name="bottom">Стиль нижней линии рамки</param>
  /// <param name="right">Стиль правой линии рамки</param>
  /// <param name="copy">Делать копии стилей</param>
  public virtual void SetFrameStyleRecursive(
    BorderLine top,
    BorderLine left,
    BorderLine bottom,
    BorderLine right,
    bool copy)
  {
    bool flag = this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    if (copy)
    {
      this.SetTopBorderLine(top.Clone(), false);
      this.SetLeftBorderLine(left.Clone(), false);
      this.SetBottomBorderLine(bottom.Clone(), false);
      this.SetRightBorderLine(right.Clone(), false);
    }
    if (this.nodes != null)
    {
      for (int index = 0; index < this.Nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
          node.SetFrameStyleRecursive(top, left, bottom, right, true);
      }
    }
    if (flag)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary>Pen для невидимых линий</summary>
  public static Pen InvisibleLinePen
  {
    [DebuggerStepThrough] get
    {
      return new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth)
      {
        DashStyle = DashStyle.Dot
      };
    }
  }

  /// <summary>Положение в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_292")]
  [CustomDescription("Attribute.Interfaces.Document_293")]
  [CustomCategory("Attribute.Interfaces.Document_294")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? LeftForUser
  {
    [DebuggerStepThrough] get
    {
      PointF point = this.ProperLocation;
      TableData parentCell = this.ParentCell;
      if (parentCell != null && parentCell.IsFixedStructureArea)
        point = this.bounds.Location;
      if (this.page != null)
        point = this.page.ConvertInternalToUser(point);
      return new float?(point.X);
    }
    set
    {
      if (!value.HasValue)
        return;
      PointF point = new PointF(value.Value, 0.0f);
      if (this.page != null)
        point = this.page.ConvertUserToInternal(point);
      TableData parentCell1 = this.ParentCell;
      if (parentCell1 != null && parentCell1.IsFixedStructureArea)
      {
        RectangleF bounds = this.bounds with { X = point.X };
        TableData parentCell2 = parentCell1.ParentCell;
        RectangleF rectangleF = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
        this.AssignProperBounds(new RectangleF(bounds.X - rectangleF.X, bounds.Y - rectangleF.Y, bounds.Width, bounds.Height), true, true, true);
        this.RecalcRelativeSize();
      }
      else
      {
        RectangleF properBounds = this.ProperBounds with
        {
          X = point.X
        };
        if (this is TableData)
        {
          this.SetCellSizes(properBounds, true, false, false, true);
          this.UpdateLayout(true);
        }
        else
        {
          this.AssignProperBounds(properBounds, true, true, true);
          this.RecalcRelativeSize();
        }
      }
    }
  }

  /// <summary>Положение в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_295")]
  [CustomDescription("Attribute.Interfaces.Document_296")]
  [CustomCategory("Attribute.Interfaces.Document_297")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? RightForUser
  {
    [DebuggerStepThrough] get
    {
      RectangleF rectangleF = this.ProperBounds;
      TableData parentCell = this.ParentCell;
      if (parentCell != null && parentCell.IsFixedStructureArea)
        rectangleF = this.bounds;
      PointF point = new PointF(rectangleF.Right, 0.0f);
      if (this.page != null)
        point = this.page.ConvertInternalToUser(point);
      return new float?(point.X);
    }
    set
    {
      if (!value.HasValue)
        return;
      PointF point = new PointF(value.Value, 0.0f);
      if (this.page != null)
        point = this.page.ConvertUserToInternal(point);
      RectangleF newBounds = this.ProperBounds;
      TableData parentCell1 = this.ParentCell;
      if (parentCell1 != null && !parentCell1.IsFixedStructureArea)
      {
        newBounds = new RectangleF(newBounds.X, newBounds.Y, point.X - newBounds.X, newBounds.Height);
        if ((double) newBounds.Width < 0.0)
          newBounds.Width = 0.0f;
        newBounds.Size = this.CalcSizeFromProper(newBounds.Size);
        this.WidthOverrided = true;
        if (parentCell1.IsRow)
        {
          this.SetCellSizes(newBounds, false, true, false, true);
          this.UpdateLayout(true);
        }
        else
        {
          RectangleF bounds = parentCell1.Bounds;
          bounds.Size = parentCell1.CalcSizeFromProper(new SizeF(newBounds.Width, bounds.Height));
          parentCell1.SetCellSizes(bounds, false, true, false, true, false);
          parentCell1.UpdateLayout(true);
        }
      }
      else if (parentCell1 == null)
      {
        newBounds = new RectangleF(point.X - newBounds.Width, newBounds.Y, newBounds.Width, newBounds.Height);
        if (this is TableData)
        {
          this.SetCellSizes(newBounds, false, true, false, true);
          this.UpdateLayout(true);
        }
        else
        {
          this.AssignProperBounds(newBounds, true, true, true);
          this.RecalcRelativeSize();
        }
      }
      else
      {
        if (!parentCell1.IsFixedStructureArea)
          return;
        newBounds = new RectangleF(point.X - this.bounds.Width, this.bounds.Y, this.bounds.Width, this.bounds.Height);
        TableData parentCell2 = parentCell1.ParentCell;
        RectangleF rectangleF = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
        this.AssignProperBounds(new RectangleF(newBounds.X - rectangleF.X, newBounds.Y - rectangleF.Y, newBounds.Width, newBounds.Height), true, true, true);
        this.RecalcRelativeSize();
      }
    }
  }

  /// <summary>Положение в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_298")]
  [CustomDescription("Attribute.Interfaces.Document_299")]
  [CustomCategory("Attribute.Interfaces.Document_300")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? TopForUser
  {
    [DebuggerStepThrough] get
    {
      PointF point = this.ProperLocation;
      TableData parentCell = this.ParentCell;
      if (parentCell != null && parentCell.IsFixedStructureArea)
        point = this.bounds.Location;
      if (this.page != null)
        point = this.page.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
    set
    {
      if (!value.HasValue)
        return;
      PointF point = new PointF(0.0f, value.Value);
      if (this.page != null)
        point = this.page.ConvertUserToInternal(point);
      TableData parentCell1 = this.ParentCell;
      if (parentCell1 != null && parentCell1.IsFixedStructureArea)
      {
        RectangleF bounds = this.bounds with { Y = point.Y };
        TableData parentCell2 = parentCell1.ParentCell;
        RectangleF rectangleF = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
        this.AssignProperBounds(new RectangleF(bounds.X - rectangleF.X, bounds.Y - rectangleF.Y, bounds.Width, bounds.Height), true, true, true);
        this.RecalcRelativeSize();
      }
      else
      {
        RectangleF properBounds = this.ProperBounds with
        {
          Y = point.Y
        };
        if ((double) this.properBounds.Height != (double) properBounds.Height)
        {
          this.SetOverrideFlags(OverrideFlags.Height);
          this.SetOverrideFlags2(OverrideFlags2.RowHeight);
          this.minHeight = properBounds.Height;
        }
        this.AssignProperBounds(properBounds, true, true, true);
        this.RecalcRelativeSize();
      }
    }
  }

  /// <summary>Положение в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_301")]
  [CustomDescription("Attribute.Interfaces.Document_302")]
  [CustomCategory("Attribute.Interfaces.Document_303")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? BottomForUser
  {
    [DebuggerStepThrough] get
    {
      RectangleF rectangleF = this.ProperBounds;
      TableData parentCell = this.ParentCell;
      if (parentCell != null && parentCell.IsFixedStructureArea)
        rectangleF = this.bounds;
      PointF point = new PointF(0.0f, rectangleF.Bottom);
      if (this.page != null)
        point = this.page.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
    set
    {
      if (!value.HasValue)
        return;
      PointF point = new PointF(0.0f, value.Value);
      if (this.page != null)
        point = this.page.ConvertUserToInternal(point);
      RectangleF newBounds = this.ProperBounds;
      TableData parentCell1 = this.ParentCell;
      if (parentCell1 != null && !parentCell1.IsFixedStructureArea)
      {
        newBounds = new RectangleF(newBounds.X, newBounds.Y, newBounds.Width, point.Y - newBounds.Y);
        if ((double) newBounds.Height < 0.0)
          newBounds.Height = 0.0f;
        if ((double) this.properBounds.Height != (double) newBounds.Height)
          parentCell1.AssignMinHeight(newBounds.Height, false, false, true);
        newBounds.Size = this.CalcSizeFromProper(newBounds.Size);
        if (parentCell1.IsColumn)
        {
          this.SetCellSizes(newBounds, false, true, true, true);
          this.UpdateLayout(true);
        }
        else
        {
          RectangleF bounds = parentCell1.Bounds;
          bounds.Size = parentCell1.CalcSizeFromProper(new SizeF(bounds.Width, newBounds.Height));
          parentCell1.SetCellSizes(bounds, false, true, false, true, false);
          parentCell1.UpdateLayout(true);
        }
      }
      else if (parentCell1 == null)
      {
        newBounds = new RectangleF(newBounds.X, point.Y - newBounds.Height, newBounds.Width, newBounds.Height);
        if ((double) this.properBounds.Height != (double) newBounds.Height)
        {
          this.SetOverrideFlags(OverrideFlags.Height);
          this.SetOverrideFlags2(OverrideFlags2.RowHeight);
          this.minHeight = newBounds.Height;
        }
        this.AssignProperBounds(newBounds, true, true, true);
        this.RecalcRelativeSize();
      }
      else
      {
        if (!parentCell1.IsFixedStructureArea)
          return;
        newBounds = new RectangleF(this.bounds.X, point.Y - this.bounds.Height, this.bounds.Width, this.bounds.Height);
        TableData parentCell2 = parentCell1.ParentCell;
        RectangleF rectangleF = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
        this.AssignProperBounds(new RectangleF(newBounds.X - rectangleF.X, newBounds.Y - rectangleF.Y, newBounds.Width, newBounds.Height), true, true, true);
        this.RecalcRelativeSize();
      }
    }
  }

  /// <summary>Размер в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_304")]
  [CustomDescription("Attribute.Interfaces.Document_305")]
  [CustomCategory("Attribute.Interfaces.Document_306")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? WidthForUser
  {
    [DebuggerStepThrough] get
    {
      return this.page != null ? new float?(this.page.ConvertInternalToUser(this.ProperSize).Width) : new float?(this.ProperSize.Width);
    }
    set
    {
      if (!value.HasValue)
        return;
      float? widthForUser = this.WidthForUser;
      float? nullable = value;
      if ((double) widthForUser.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & widthForUser.HasValue == nullable.HasValue)
        return;
      SizeF size = new SizeF(value.Value, 0.0f);
      PageCoorSystem pageCoorSystem = PageCoorSystem.TopLeft;
      if (this.page != null)
      {
        size = this.page.ConvertUserToInternal(size);
        pageCoorSystem = this.page.UserCoorSystem;
      }
      RectangleF rectangleF1 = this.ProperBounds;
      TableData parentCell1 = this.ParentCell;
      if (parentCell1 != null && !parentCell1.IsFixedStructureArea)
      {
        rectangleF1 = new RectangleF(rectangleF1.Location, new SizeF(size.Width, rectangleF1.Height));
        RectangleF newBounds = this.CalcBoundsFromProper(rectangleF1);
        this.WidthOverrided = true;
        this.SetCellSizes(newBounds, false, true, false, true);
        if (parentCell1.IsColumn)
        {
          RectangleF bounds = parentCell1.Bounds;
          bounds.Size = parentCell1.CalcSizeFromProper(new SizeF(newBounds.Width, bounds.Height));
          parentCell1.SetCellSizes(bounds, false, true, false, true, false);
          parentCell1.UpdateLayout(true);
        }
        else
          this.UpdateLayout(true);
      }
      else
      {
        RectangleF rectangleF2 = rectangleF1;
        if (parentCell1 != null && parentCell1.IsFixedStructureArea)
          rectangleF2 = rectangleF1 = this.bounds;
        switch (pageCoorSystem)
        {
          case PageCoorSystem.BottomLeft:
          case PageCoorSystem.TopLeft:
          case PageCoorSystem.Custom:
            rectangleF1 = new RectangleF(rectangleF1.Location, new SizeF(size.Width, rectangleF1.Height));
            break;
          case PageCoorSystem.TopRight:
          case PageCoorSystem.BottomRight:
            rectangleF1 = new RectangleF(new PointF(rectangleF1.Right - size.Width, rectangleF1.Y), new SizeF(size.Width, rectangleF1.Height));
            break;
        }
        if (!(rectangleF2 != rectangleF1))
          return;
        if ((double) rectangleF2.Width != (double) rectangleF1.Width)
        {
          this.overrideFlags |= OverrideFlags.Width;
          this.overrideFlags2 |= OverrideFlags2.ColumnWidth;
          this.minWidth = rectangleF1.Width;
        }
        if (parentCell1 != null && parentCell1.IsFixedStructureArea)
        {
          TableData parentCell2 = parentCell1.ParentCell;
          RectangleF rectangleF3 = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
          if (this is TableData)
          {
            this.SetCellSizes(new RectangleF(rectangleF1.X - rectangleF3.X, rectangleF1.Y - rectangleF3.Y, rectangleF1.Width, rectangleF1.Height), false, true, false, true);
            this.UpdateLayout(true);
          }
          else
          {
            this.AssignProperBounds(new RectangleF(rectangleF1.X - rectangleF3.X, rectangleF1.Y - rectangleF3.Y, rectangleF1.Width, rectangleF1.Height), true, true, true);
            this.RecalcRelativeSize();
          }
        }
        else if (this is TableData)
        {
          this.SetCellSizes(rectangleF1, false, true, false, true);
          this.UpdateLayout(true);
        }
        else
        {
          this.AssignProperBounds(rectangleF1, true, true, true);
          this.RecalcRelativeSize();
        }
      }
    }
  }

  /// <summary>Размер в формате выбранном пользователем</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_307")]
  [CustomDescription("Attribute.Interfaces.Document_308")]
  [CustomCategory("Attribute.Interfaces.Document_309")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? HeightForUser
  {
    get
    {
      return this.page != null ? new float?(this.page.ConvertInternalToUser(this.ProperSize).Height) : new float?(this.ProperSize.Height);
    }
    set => this.SetHeightForUser((float) ((double) value ?? 0.0), true, true);
  }

  /// <summary>Установить новое значение свойства HeightForUser</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetHeightForUser(float value, bool updateUI, bool updateLayout)
  {
    float? heightForUser = this.HeightForUser;
    float num = value;
    if ((double) heightForUser.GetValueOrDefault() == (double) num & heightForUser.HasValue)
      return;
    RectangleF newProperBounds = this.ProperBounds;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      newProperBounds = this.bounds;
    SizeF size = new SizeF(0.0f, value);
    PageCoorSystem pageCoorSystem = PageCoorSystem.TopLeft;
    PageData page = this.Page;
    if (page != null)
    {
      size = page.ConvertUserToInternal(size);
      pageCoorSystem = page.UserCoorSystem;
    }
    RectangleF rectangleF1 = newProperBounds;
    if (parentCell == null || !parentCell.IsFixedStructureArea)
    {
      newProperBounds = new RectangleF(newProperBounds.Location, new SizeF(newProperBounds.Width, size.Height));
      if ((double) rectangleF1.Height != (double) newProperBounds.Height)
      {
        this.SetOverrideFlags(OverrideFlags.Height);
        this.SetOverrideFlags2(OverrideFlags2.RowHeight);
        this.AssignMinHeight(newProperBounds.Height, false, false, true);
        if (this.ParentCell == null || (double) this.MaxHeight < (double) newProperBounds.Height)
          this.AssignMaxHeight(newProperBounds.Height, false, false, true);
      }
      if (this.ParentCell != null)
      {
        RectangleElement parentWithBottomSide = this.FindParentWithBottomSide(this.properBounds.Bottom);
        RectangleF properBounds = parentWithBottomSide.ProperBounds;
        properBounds.Height = newProperBounds.Bottom - properBounds.Y;
        parentWithBottomSide.SetCellSizes(this.CalcBoundsFromProper(properBounds), false, true, true, true);
      }
      else
        this.SetCellSizes(this.CalcBoundsFromProper(newProperBounds), false, true, true, true, true);
    }
    else
    {
      switch (pageCoorSystem)
      {
        case PageCoorSystem.BottomLeft:
        case PageCoorSystem.BottomRight:
        case PageCoorSystem.Custom:
          newProperBounds = new RectangleF(new PointF(newProperBounds.X, newProperBounds.Bottom - size.Height), new SizeF(newProperBounds.Width, size.Height));
          break;
        case PageCoorSystem.TopLeft:
        case PageCoorSystem.TopRight:
          newProperBounds = new RectangleF(newProperBounds.Location, new SizeF(newProperBounds.Width, size.Height));
          break;
      }
      if (rectangleF1 != newProperBounds)
      {
        if ((double) rectangleF1.Height != (double) newProperBounds.Height)
        {
          this.SetOverrideFlags(OverrideFlags.Height);
          this.SetOverrideFlags2(OverrideFlags2.RowHeight);
          this.AssignMinHeight(newProperBounds.Height, false, false, true);
          if ((double) this.MaxHeight < (double) newProperBounds.Height)
            this.AssignMaxHeight(newProperBounds.Height, false, false, true);
        }
        RectangleF rectangleF2 = parentCell.ParentCell == null || !parentCell.ParentCell.IsFixedStructureArea ? parentCell.properBounds : parentCell.bounds;
        newProperBounds = new RectangleF(newProperBounds.X - rectangleF2.X, newProperBounds.Y - rectangleF2.Y, newProperBounds.Width, newProperBounds.Height);
        if (this is TableData tableData)
        {
          RectangleF newBounds = this.CalcBoundsFromProper(newProperBounds);
          if (newBounds.Location != this.bounds.Location)
            tableData.RecalcCellLocations(newBounds.Location, 0, 0, false, false, false);
          tableData.SetCellSizes(newBounds, false, true, true, true, false);
        }
        else
        {
          this.AssignProperBounds(newProperBounds, true, false, false);
          this.RecalcRelativeSize();
        }
      }
    }
    if (!updateLayout)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Положение в формате выбранном пользователем</summary>
  [TypeConverter(typeof (PointFConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_310")]
  [CustomDescription("Attribute.Interfaces.Document_311")]
  [CustomCategory("Attribute.Interfaces.Document_312")]
  [RefreshProperties(RefreshProperties.All)]
  [Browsable(false)]
  public virtual PointF LocationForUser
  {
    [DebuggerStepThrough] get
    {
      RectangleF rectangle = this.ProperBounds;
      if (this.Page != null)
        rectangle = this.Page.ConvertInternalToUser(rectangle);
      return rectangle.Location;
    }
    set
    {
      RectangleF properBounds = this.ProperBounds;
      if (this.page != null)
        properBounds.Location = this.page.ConvertUserToInternal(value);
      this.AssignProperBounds(properBounds, true, true, true);
      this.RecalcRelativeSize();
    }
  }

  /// <summary> Собственно положение ячейки (положение без учета пропусков строк и столбцов)</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual PointF ProperLocation
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.properBounds.X != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Y != (double) RectangleElement.EmptyFloatValue)
        return this.properBounds.Location;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.properBounds.Location;
    }
  }

  /// <summary>Положение, в миллиметрах</summary>
  [Browsable(false)]
  public virtual PointF Location
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.bounds.X != (double) RectangleElement.EmptyFloatValue && (double) this.bounds.Y != (double) RectangleElement.EmptyFloatValue)
        return this.bounds.Location;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.bounds.Location;
    }
  }

  /// <summary>Размер в формате выбранном пользователем</summary>
  [TypeConverter(typeof (SizeFConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_313")]
  [CustomDescription("Attribute.Interfaces.Document_314")]
  [CustomCategory("Attribute.Interfaces.Document_315")]
  [RefreshProperties(RefreshProperties.All)]
  [Browsable(false)]
  public SizeF SizeForUser
  {
    [DebuggerStepThrough] get
    {
      return this.Page != null ? this.Page.ConvertInternalToUser(this.ProperSize) : this.ProperSize;
    }
    set
    {
      RectangleF rectangle = new RectangleF(this.LocationForUser, value);
      if (this.Page != null)
        rectangle = this.Page.ConvertUserToInternal(rectangle);
      RectangleF properBounds = this.ProperBounds;
      if (!(properBounds != rectangle))
        return;
      if ((double) properBounds.Width != (double) rectangle.Width)
      {
        this.SetOverrideFlags(OverrideFlags.Width);
        this.SetOverrideFlags2(OverrideFlags2.ColumnWidth);
      }
      if ((double) properBounds.Height != (double) rectangle.Height)
      {
        this.SetOverrideFlags(OverrideFlags.Height);
        this.SetOverrideFlags2(OverrideFlags2.RowHeight);
      }
      this.AssignProperBounds(rectangle, true, true, true);
      this.RecalcRelativeSize();
    }
  }

  /// <summary>Собственный размер ячейки
  /// (размер без учета пропусков строк и столбцов)</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual SizeF ProperSize
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue)
        return this.properBounds.Size;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.properBounds.Size;
    }
  }

  /// <summary>Размеры, в миллиметрах</summary>
  [Browsable(false)]
  public virtual SizeF Size
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.bounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.bounds.Height != (double) RectangleElement.EmptyFloatValue)
        return this.bounds.Size;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.bounds.Size;
    }
  }

  /// <summary>Получить все границы ячейки</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="getTemplatePWidth">Обновить значение ширины из шаблона, если наследуюется</param>
  /// <param name="getTemplatePHeight">Обновить значение высоты из шаблона, если наследуюется</param>
  public virtual void GetCellBounds(
    RectangleElement template,
    bool getTemplatePWidth,
    bool getTemplatePHeight)
  {
    SizeF size = this.properBounds.Size;
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
    {
      if (parentCell.IsColumn)
      {
        if (getTemplatePHeight && (!this.IsOverridden2(OverrideFlags2.RowHeight) && !this.IsOverridden(OverrideFlags.Height) || template == null && (double) size.Height == (double) RectangleElement.EmptyFloatValue))
        {
          if ((double) this.defaultRowSize != 0.0)
          {
            size.Height = this.defaultRowSize;
            getTemplatePHeight = false;
          }
          else
          {
            int gridRowIndex = this.GetGridRowIndex();
            if (gridRowIndex != -1)
            {
              List<RowColParams> gridRowsParams = parentCell.GridRowsParams;
              if (gridRowsParams != null && gridRowIndex < gridRowsParams.Count)
              {
                if (gridRowsParams[gridRowIndex] != null)
                  size.Height = gridRowsParams[gridRowIndex].Size;
                else
                  LogManager.AddLine("RectangleElement.GetCellBounds().  rowParams[rowIndex] == null");
                getTemplatePHeight = false;
              }
            }
          }
        }
      }
      else if (!this.IsOverridden2(OverrideFlags2.ColumnWidth) && !this.IsOverridden(OverrideFlags.Width) || template == null && (double) size.Width == (double) RectangleElement.EmptyFloatValue)
      {
        int gridColumnIndex = this.GetGridColumnIndex();
        if (gridColumnIndex != -1)
        {
          List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
          if (gridColumnsParams != null && gridColumnIndex < gridColumnsParams.Count)
          {
            if (gridColumnsParams[gridColumnIndex] != null)
            {
              size.Width = gridColumnsParams[gridColumnIndex].Size;
              if (!this.IsDefaultGridPos)
              {
                int num = 1;
                for (int spanCount = this.GridPos.SpanCount; num < spanCount && gridColumnIndex + num < gridColumnsParams.Count; ++num)
                {
                  if (gridColumnsParams[gridColumnIndex + num] != null)
                    size.Width += gridColumnsParams[gridColumnIndex + num].Size;
                  else
                    LogManager.AddLine("RectangleElement.GetCellBounds().  colParams[colIndex + i] == null");
                }
              }
              getTemplatePWidth = false;
            }
            else
              LogManager.AddLine("RectangleElement.GetCellBounds().  colParams[colIndex] == null");
          }
        }
      }
    }
    if (this.IsSingleCell && this is TableData && this.Template is RectangleElement template1 && (double) size.Width != (double) template1.properBounds.Width)
      size.Width = template1.properBounds.Width;
    RectangleF properBounds = this.properBounds with
    {
      Size = size
    };
    if (!this.IsTableCell && template != null)
    {
      if (!this.IsOverridden(OverrideFlags.Geometry) || (double) this.bounds.X == (double) RectangleElement.EmptyFloatValue)
        this.setBounds(BoundsHelper.SetX(this.bounds, template.bounds.X));
      if (!this.IsOverridden(OverrideFlags.Geometry) || (double) this.bounds.Y == (double) RectangleElement.EmptyFloatValue)
        this.setBounds(BoundsHelper.SetY(this.bounds, template.bounds.Y));
    }
    else
    {
      if ((double) this.bounds.X == (double) RectangleElement.EmptyFloatValue)
        this.setBounds(BoundsHelper.SetX(this.bounds, 0.0f));
      if ((double) this.bounds.Y == (double) RectangleElement.EmptyFloatValue)
        this.setBounds(BoundsHelper.SetY(this.bounds, 0.0f));
      if (template != null && parentCell != null && parentCell.IsFixedStructureArea)
      {
        if (!this.IsOverridden(OverrideFlags.Geometry) || (double) properBounds.X == (double) RectangleElement.EmptyFloatValue)
          properBounds.X = template.properBounds.X;
        if (!this.IsOverridden(OverrideFlags.Geometry) || (double) properBounds.Y == (double) RectangleElement.EmptyFloatValue)
          properBounds.Y = template.properBounds.Y;
      }
    }
    if (getTemplatePWidth && template != null)
    {
      if ((this.overrideFlags & OverrideFlags.Width) == OverrideFlags.None || (double) properBounds.Width == (double) RectangleElement.EmptyFloatValue)
        properBounds.Width = template.properBounds.Width;
    }
    else if ((double) properBounds.Width == (double) RectangleElement.EmptyFloatValue)
      properBounds.Width = RectangleElement.MinimalSize.Width;
    if (getTemplatePHeight && template != null)
    {
      if (!this.IsOverridden(OverrideFlags.Height) || (double) properBounds.Height == (double) RectangleElement.EmptyFloatValue)
        properBounds.Height = template.properBounds.Height;
    }
    else if ((double) properBounds.Height == (double) RectangleElement.EmptyFloatValue)
      properBounds.Height = RectangleElement.MinimalSize.Height;
    if (parentCell == null || !parentCell.IsFixedStructureArea)
      properBounds.Location = (double) this.skipCellsBefore == 0.0 || this.IgnoreSkipBefore() ? this.bounds.Location : this.CalcProperLocation(this.bounds.Location);
    this.setProperBounds(properBounds);
    if ((double) this.skipCellsBefore == 0.0 && (double) this.skipCellsAfter == 0.0 || parentCell == null || this.IgnoreSkipBefore())
      this.setBounds(BoundsHelper.SetSize(this.bounds, this.properBounds.Size));
    else
      this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
  }

  /// <summary>Границы, в миллиметрах</summary>
  [Category("Debug")]
  public virtual RectangleF Bounds
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.bounds.X != (double) RectangleElement.EmptyFloatValue && (double) this.bounds.Y != (double) RectangleElement.EmptyFloatValue && (double) this.bounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.bounds.Height != (double) RectangleElement.EmptyFloatValue)
        return this.bounds;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.bounds;
    }
    set => this.AssignBounds(value, true, true, true);
  }

  /// <summary>Назначить границы</summary>
  /// <param name="location">Положение</param>
  /// <param name="size">Размеры</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignBounds(
    PointF location,
    SizeF size,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignBounds(new RectangleF(location, size), saveUndo, updateUI, updateLayout);
  }

  /// <summary>Установить размеры дочерних ячеек</summary>
  /// <param name="newBounds">Новые границы ячейки (внешние, с учётом пропусков)</param>
  /// <param name="lockNeedUpdateLayoutFlag">Блокировать изменение флага NeedUpdateLayoutFlag</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="setMinHeight">Установить MinHeight</param>
  /// <param name="setRelativeSize">Установить соответствующий относительный размер</param>
  /// <param name="checkLastCell">Проверять размер последней ячейки</param>
  /// <returns>Новые границы ячейки</returns>
  public virtual RectangleF SetCellSizes(
    RectangleF newBounds,
    bool lockNeedUpdateLayoutFlag,
    bool saveUndo,
    bool setMinHeight,
    bool setRelativeSize,
    bool checkLastCell = false)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (lockNeedUpdateLayoutFlag)
    {
      flag1 = this.needUpdateLayoutFlag;
      this.AssignNeedUpdateLayoutFlag(true);
      if (this.OwnerDocument != null)
      {
        flag2 = this.OwnerDocument.NeedUpdateLayoutFlag;
        this.OwnerDocument.SetNeedUpdateLayoutFlag(true, true, false, false);
      }
    }
    double height = (double) this.properBounds.Height;
    float width = this.properBounds.Width;
    RectangleF rectangleF = this.CalcProperBounds(newBounds);
    if (setMinHeight && (double) rectangleF.Height < (double) this.MinHeight)
      this.AssignMinHeight(rectangleF.Height, false, false, true);
    this.AssignBounds(newBounds, saveUndo, false, false);
    if (setRelativeSize)
      this.RecalcRelativeSize();
    if (setMinHeight && (double) this.properBounds.Width != (double) width && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue)
      this.AssignMinWidth(this.properBounds.Width, false, false, true);
    if (lockNeedUpdateLayoutFlag)
    {
      this.AssignNeedUpdateLayoutFlag(flag1);
      if (this.OwnerDocument != null)
        this.OwnerDocument.SetNeedUpdateLayoutFlag(flag2, true, false, false);
    }
    return this.bounds;
  }

  /// <summary>Пересчитать относительные размеры по текущим размерам ячейки</summary>
  public void RecalcRelativeSize()
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return;
    if ((double) this.relativeWidth > 0.0)
      this.AssignRelativeWidth((float) ((double) this.properBounds.Width / (double) parentCell.properBounds.Width * 100.0), false, false);
    if ((double) this.relativeHeight <= 0.0)
      return;
    this.AssignRelativeHeight((float) ((double) this.properBounds.Height / (double) parentCell.properBounds.Height * 100.0), false, false);
  }

  /// <summary>Назначить границы</summary>
  /// <param name="value">Границы</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  public virtual void AssignBounds(
    RectangleF value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    RectangleF bounds = this.Bounds;
    if (bounds != value || !this.IsFirstInFlow && bounds.Location != this.properBounds.Location)
    {
      bool needUpdateLayout = bounds.Size != value.Size || this is TableData || bounds.Location != value.Location && this.HorzAlign != ElementHorizontalAlign.None && this.VertAlign != 0;
      this.SetOverrideFlags(OverrideFlags.Geometry);
      if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "Bounds", (object) this.Bounds, (object) value);
      this.AssignBoundsOnly(bounds, value);
      this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, needUpdateLayout, true, true);
    }
    else
    {
      if (!(this.bounds != value))
        return;
      if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "Bounds", (object) this.Bounds, (object) value);
      TableData parentCell = this.ParentCell;
      if (parentCell != null || this.bounds.Location != value.Location)
      {
        this.setBounds(BoundsHelper.SetLocation(this.bounds, value.Location));
        if (parentCell == null || !parentCell.IsFixedStructureArea)
          this.setProperBounds(new RectangleF(this.CalcProperLocation(this.bounds.Location), this.properBounds.Size));
        this.SetOverrideFlags(OverrideFlags.Geometry);
      }
      if (!(this.bounds.Size != value.Size))
        return;
      this.SetOverrideFlags(OverrideFlags.Geometry);
      this.setBounds(BoundsHelper.SetSize(this.bounds, value.Size));
      this.setProperBounds(new RectangleF(this.properBounds.Location, this.CalcProperSize(this.bounds.Size)));
    }
  }

  /// <summary>Только назначить новое значение границ полям</summary>
  /// <param name="realBounds">Текущие границы (кэш)</param>
  /// <param name="value">Новые границы</param>
  public virtual void AssignBoundsOnly(RectangleF realBounds, RectangleF value)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell != null || realBounds.Location != value.Location || !this.IsFirstInFlow && realBounds.Location != this.properBounds.Location)
    {
      this.setBounds(BoundsHelper.SetLocation(this.bounds, value.Location));
      if (parentCell == null || !parentCell.IsFixedStructureArea)
        this.setProperBounds(new RectangleF(this.CalcProperLocation(this.bounds.Location), this.properBounds.Size));
      this.SetOverrideFlags(OverrideFlags.Geometry);
      this.needUpdateUIGeometry = true;
    }
    if (!(realBounds.Size != value.Size))
      return;
    this.SetOverrideFlags(OverrideFlags.Geometry);
    this.setBounds(BoundsHelper.SetSize(this.bounds, value.Size));
    SizeF size = this.CalcProperSize(this.bounds.Size);
    if ((double) this.ContentHeight > (double) size.Height && !(this is ContainerData))
    {
      size.Height = this.ContentHeight;
      if ((double) this.bounds.Bottom < (double) this.properBounds.Y + (double) size.Height)
        this.setBounds(new RectangleF(this.bounds.X, this.bounds.Y, this.bounds.Width, this.properBounds.Y - this.bounds.Y + size.Height));
    }
    this.setProperBounds(new RectangleF(this.properBounds.Location, size));
    this.needUpdateUIGeometry = true;
    if ((parentCell == null || this.IsOverridden(OverrideFlags.Width)) && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue)
    {
      this.SetOverrideFlags(OverrideFlags.Width);
      this.SetOverrideFlags2(OverrideFlags2.ColumnWidth);
    }
    else
    {
      this.ResetOverrideFlags(OverrideFlags.Width);
      if ((double) this.properBounds.Width == (double) RectangleElement.EmptyFloatValue)
        this.ResetOverrideFlags2(OverrideFlags2.ColumnWidth);
    }
    if ((parentCell == null || this.IsOverridden(OverrideFlags.Height)) && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue)
    {
      this.SetOverrideFlags(OverrideFlags.Height);
      this.SetOverrideFlags2(OverrideFlags2.RowHeight);
    }
    else
    {
      this.ResetOverrideFlags(OverrideFlags.Height);
      this.ResetOverrideFlags2(OverrideFlags2.RowHeight);
    }
  }

  /// <summary>Вычислить собственное положение ячейки (без пропущенных строк и столбцов)</summary>
  /// <param name="location">Положение  всей ячейки (с пропущенными строками и столбцами)</param>
  /// <returns>Собственное положение</returns>
  public virtual PointF CalcProperLocation(PointF location)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return location;
    if (parentCell.IsFixedStructureArea)
      return this.properBounds.Location;
    return parentCell.IsColumn ? new PointF(location.X, location.Y + this.SkipSizeBefore) : new PointF(location.X + this.SkipSizeBefore, location.Y);
  }

  /// <summary>Вычислить собственный размер ячейки (без пропущенных строк и столбцов)</summary>
  /// <param name="size">Полный размер</param>
  /// <returns>Собственный размер</returns>
  public virtual SizeF CalcProperSize(SizeF size)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null || parentCell.IsFixedStructureArea)
      return size;
    SizeF sizeF = size;
    float num = 0.0f;
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore())
      num = this.SkipSizeBefore;
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore() || (double) this.skipCellsAfter != 0.0 && !this.IgnoreSkipAfter())
    {
      if (parentCell.IsColumn)
      {
        if ((double) num + (double) this.SkipSizeAfter < (double) sizeF.Height)
          sizeF.Height -= num + this.SkipSizeAfter;
        else if ((double) num < (double) sizeF.Height)
          sizeF.Height -= num;
        else
          sizeF.Height = 0.0f;
      }
      else
        sizeF.Width -= num + this.SkipSizeAfter;
    }
    return sizeF;
  }

  /// <summary>Вычислить собственный границы ячейки (без пропущенных строк и столбцов)</summary>
  /// <param name="bounds">Полные границы</param>
  /// <returns>Собственне границы ячейки (без пропущенных строк и столбцов)</returns>
  public virtual RectangleF CalcProperBounds(RectangleF bounds)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return bounds;
    if (parentCell.IsFixedStructureArea)
      return this.properBounds;
    RectangleF rectangleF = bounds;
    float num = 0.0f;
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore())
    {
      num = this.SkipSizeBefore;
      if (parentCell.IsColumn)
        rectangleF.Y += num;
      else
        rectangleF.X += num;
    }
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore() || (double) this.skipCellsAfter != 0.0 && !this.IgnoreSkipAfter())
    {
      if (parentCell.IsColumn)
      {
        if ((double) num + (double) this.SkipSizeAfter < (double) rectangleF.Height)
          rectangleF.Height -= num + this.SkipSizeAfter;
        else if ((double) num < (double) rectangleF.Height)
          rectangleF.Height -= num;
        else
          rectangleF.Height = 0.0f;
      }
      else
        rectangleF.Width -= num + this.SkipSizeAfter;
    }
    return rectangleF;
  }

  /// <summary>Вычислить положение с пропусками на основе собственного положения</summary>
  /// <param name="properLocation">Собственное положение</param>
  /// <returns>Вычисленное положение с пропусками</returns>
  public virtual PointF CalcLocationFromProper(PointF properLocation)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return properLocation;
    if (parentCell.IsFixedStructureArea)
      return this.CalcRealProperBounds(this.ProperBounds with
      {
        Location = properLocation
      }).Location;
    return parentCell.IsColumn ? new PointF(properLocation.X, properLocation.Y - this.SkipSizeBefore) : new PointF(properLocation.X - this.SkipSizeBefore, properLocation.Y);
  }

  /// <summary>Вычислить размер с пропусками на основе собственного размера</summary>
  /// <param name="properSize">Собственный размер</param>
  /// <returns>Вычисленный размер с пропусками</returns>
  public virtual SizeF CalcSizeFromProper(SizeF properSize)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return properSize;
    return parentCell.IsColumn ? new SizeF(properSize.Width, properSize.Height + (this.SkipSizeBefore + this.SkipSizeAfter)) : new SizeF(properSize.Width + (this.SkipSizeBefore + this.SkipSizeAfter), properSize.Height);
  }

  /// <summary>Вычислить размер с пропусками на основе собственного размера</summary>
  /// <param name="properSize">Собственный размер</param>
  /// <param name="ignoreAfterSkipSize">Не учитывать пропущенные строки после записи</param>
  /// <returns>Вычисленный размер с пропусками</returns>
  public virtual SizeF CalcSizeFromProper(SizeF properSize, bool ignoreAfterSkipSize)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return properSize;
    return parentCell.IsColumn ? new SizeF(properSize.Width, properSize.Height + (this.SkipSizeBefore + (ignoreAfterSkipSize ? 0.0f : this.SkipSizeAfter))) : new SizeF(properSize.Width + (this.SkipSizeBefore + (ignoreAfterSkipSize ? 0.0f : this.SkipSizeAfter)), properSize.Height);
  }

  /// <summary>Вычислить границы с пропусками на основе собственного размера</summary>
  /// <param name="newProperBounds">Собственные границы</param>
  /// <returns>Вычисленный размер с пропусками</returns>
  public virtual RectangleF CalcBoundsFromProper(RectangleF newProperBounds)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return newProperBounds;
    if (parentCell.IsFixedStructureArea)
      return this.CalcRealProperBounds(newProperBounds);
    RectangleF rectangleF = newProperBounds;
    float num1 = 0.0f;
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore() && this.IsFirstInFlow)
    {
      num1 = this.SkipSizeBefore;
      if (parentCell.IsColumn)
        rectangleF.Y -= num1;
      else
        rectangleF.X -= num1;
    }
    if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore() || (double) this.skipCellsAfter != 0.0 && !this.IgnoreSkipAfter())
    {
      float num2 = 0.0f;
      if ((double) this.skipCellsAfter != 0.0 && !this.IgnoreSkipAfter() && this.IsLastInFlow)
        num2 = this.SkipSizeAfter;
      if (parentCell.IsColumn)
        rectangleF.Height += num1 + num2;
      else
        rectangleF.Width += num1 + num2;
    }
    return rectangleF;
  }

  /// <summary>Назначить собственные границы (без пропусков)</summary>
  /// <param name="location">Соственное положение (без пропусков)</param>
  /// <param name="size">Собственный размер (без пропусков)</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignProperBounds(
    PointF location,
    SizeF size,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignProperBounds(new RectangleF(location, size), saveUndo, updateUI, updateLayout);
  }

  /// <summary>Назначить собственные границы (без пропусков)</summary>
  /// <param name="value">Собственные границы (без пропусков)</param>
  /// <param name="saveUndo">Сохранять действие для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignProperBounds(
    RectangleF value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    RectangleF properBounds = this.ProperBounds;
    if (!(properBounds != value))
      return;
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ProperBounds", (object) this.ProperBounds, (object) value);
    bool needUpdateLayout = false;
    if (this.IsTableCell || properBounds.Location != value.Location)
    {
      this.setProperBounds(new RectangleF(value.Location, this.properBounds.Size));
      this.setBounds(BoundsHelper.SetLocation(this.bounds, this.CalcLocationFromProper(this.properBounds.Location)));
      this.needUpdateUIGeometry = true;
      needUpdateLayout = this is TableData;
    }
    if (properBounds.Size != value.Size)
    {
      this.setProperBounds(new RectangleF(this.properBounds.Location, value.Size));
      if (this.IsOverridden(OverrideFlags.Width) && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue)
      {
        this.SetOverrideFlags(OverrideFlags.Width);
        this.SetOverrideFlags2(OverrideFlags2.ColumnWidth);
      }
      else
      {
        this.ResetOverrideFlags(OverrideFlags.Width);
        if ((double) this.properBounds.Width == (double) RectangleElement.EmptyFloatValue)
          this.ResetOverrideFlags2(OverrideFlags2.ColumnWidth);
      }
      if (this.IsOverridden(OverrideFlags.Height) && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue)
      {
        this.SetOverrideFlags(OverrideFlags.Height);
        this.SetOverrideFlags2(OverrideFlags2.RowHeight);
      }
      else
      {
        this.ResetOverrideFlags(OverrideFlags.Height);
        this.ResetOverrideFlags2(OverrideFlags2.RowHeight);
      }
      this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
      this.needUpdateUIGeometry = true;
      needUpdateLayout = true;
    }
    this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, needUpdateLayout, true, true);
  }

  /// <summary>Выравнивание элемента страницы по горизонтали</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_514")]
  [CustomDescription("Attribute.Interfaces.Document_515")]
  [CustomCategory("Attribute.Interfaces.Document_306")]
  public virtual ElementHorizontalAlign HorzAlign
  {
    [DebuggerStepThrough] get => this.horzAlign;
    set => this.AssignHorzAlign(value, true, true);
  }

  /// <summary>Назначить значение свойству HorzAlign</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять внешний вид</param>
  /// <param name="updateLayout">Обновлять расположение элементов</param>
  public virtual void AssignHorzAlign(
    ElementHorizontalAlign value,
    bool updateUI,
    bool updateLayout)
  {
    if (this.horzAlign == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
    try
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "HorzAlign", (object) this.HorzAlign, (object) value);
      this.horzAlign = value;
      if (this.horzAlign != ElementHorizontalAlign.None)
        this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
    }
    finally
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Выравнивание элемента страницы по горизонтали</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_543")]
  [CustomDescription("Attribute.Interfaces.Document_544")]
  [CustomCategory("Attribute.Interfaces.Document_306")]
  public virtual ElementVerticalAlign VertAlign
  {
    [DebuggerStepThrough] get => this.vertAlign;
    set => this.AssignVertAlign(value, true, true);
  }

  /// <summary>Назначить значение свойству VertAlign</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять внешний вид</param>
  /// <param name="updateLayout">Обновлять расположение элементов</param>
  public virtual void AssignVertAlign(ElementVerticalAlign value, bool updateUI, bool updateLayout)
  {
    if (this.vertAlign == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
    try
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "VertAlign", (object) this.VertAlign, (object) value);
      this.vertAlign = value;
      if (this.vertAlign != ElementVerticalAlign.None)
        this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
    }
    finally
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Ширина относительно родительского элемента</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_545")]
  [CustomDescription("Attribute.Interfaces.Document_546")]
  [CustomCategory("Attribute.Interfaces.Document_306")]
  public virtual float? RelativeWidth
  {
    [DebuggerStepThrough] get
    {
      return (double) this.relativeWidth == 0.0 ? new float?() : new float?(this.relativeWidth);
    }
    set => this.AssignRelativeWidth(value.HasValue ? value.Value : 0.0f, true, true);
  }

  /// <summary>Назначить значение свойству RelativeWidth</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять внешний вид</param>
  /// <param name="updateLayout">Обновлять расположение элементов</param>
  public virtual void AssignRelativeWidth(float value, bool updateUI, bool updateLayout)
  {
    if ((double) this.relativeWidth == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "RelativeWidth", (object) this.RelativeWidth, (object) value);
    this.relativeWidth = value;
    this.overrideFlags3 |= OverrideFlags3.RelativeWidth;
    if ((double) this.relativeWidth != 0.0)
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Ширина относительно родительского элемента</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_547")]
  [CustomDescription("Attribute.Interfaces.Document_548")]
  [CustomCategory("Attribute.Interfaces.Document_306")]
  public virtual float? RelativeHeight
  {
    [DebuggerStepThrough] get
    {
      return (double) this.relativeHeight == 0.0 ? new float?() : new float?(this.relativeHeight);
    }
    set => this.AssignRelativeHeight(value.HasValue ? value.Value : 0.0f, true, true);
  }

  /// <summary>Назначить значение свойству RelativeWidth</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять внешний вид</param>
  /// <param name="updateLayout">Обновлять расположение элементов</param>
  public virtual void AssignRelativeHeight(float value, bool updateUI, bool updateLayout)
  {
    if ((double) this.relativeHeight == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "RelativeHeight", (object) this.RelativeHeight, (object) value);
    this.relativeHeight = value;
    this.overrideFlags3 |= OverrideFlags3.RelativeHeight;
    if ((double) this.relativeHeight != 0.0)
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Автоматически подбирать размер</summary>
  [Browsable(false)]
  public virtual bool AutoSizeWidth
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  /// <summary>Вписывать текст в размеры полей</summary>
  [Browsable(false)]
  public virtual bool FontAutoSize
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  /// <summary>Задать новое значение свойству AutoSize</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Перекрывать наследование параметра по шаблону</param>
  public virtual void AssignAutoSize(
    AutoSizeDirection value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
  }

  public RectangleF RealProperBounds
  {
    get
    {
      TableData parentCell = this.ParentCell;
      return parentCell == null || !parentCell.IsFixedStructureArea ? this.ProperBounds : this.Bounds;
    }
  }

  /// <summary>Собственные (внутренние) границы ячейки для размещения содержимого.
  /// <Remarks>Если у ячейки есть пропуски строк снизу и сверху, то это внутренние границы ячейки без пропусков,
  /// а внешние границы с учётом пропусков находятся в свойстве Bounds.
  /// Если ячейка находится внутри таблицы с флагом IsFixedStructureArea,
  /// то в свойстве хранятся локальные координаты в пространстве ячейки владельца,
  /// а реальные координаты находятся в свойстве Bounds</Remarks></summary>
  [Category("Debug")]
  public virtual RectangleF ProperBounds
  {
    [DebuggerStepThrough] get
    {
      if ((double) this.properBounds.X != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Y != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue)
        return this.properBounds;
      this.GetCellBounds(this.Template as RectangleElement, true, true);
      return this.properBounds;
    }
    set => this.AssignProperBounds(value, true, true, true);
  }

  /// <summary>Рассчитать ProperBounds используя относительные координаты и координаты родительского элемента</summary>
  /// <returns></returns>
  public RectangleF CalcRealProperBounds(RectangleF cellProperBounds)
  {
    RectangleF rectangleF1 = cellProperBounds;
    TableData parentCell1 = this.ParentCell;
    if (parentCell1 != null && parentCell1.IsFixedStructureArea)
    {
      TableData parentCell2 = parentCell1.ParentCell;
      RectangleF rectangleF2 = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.Bounds;
      rectangleF1.X = rectangleF2.X + cellProperBounds.X;
      rectangleF1.Y = rectangleF2.Y + cellProperBounds.Y;
    }
    return rectangleF1;
  }

  /// <summary>Кратная высота строки</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_316")]
  [CustomDescription("Attribute.Interfaces.Document_317")]
  [Category("Debug")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public virtual bool IsFixedSizeRows
  {
    [DebuggerStepThrough] get => (double) this.DefaultRowSize != 0.0;
  }

  /// <summary>Получить значение IsFixedSizeRows</summary>
  /// <param name="template">Шаблон</param>
  public bool GetIsFixedSizeRows(RectangleElement template, CellContext context)
  {
    return (double) this.GetDefaultRowSize(template, context) != 0.0;
  }

  /// <summary>Высота строки для отрисовки сетки, новых строк и кратной высоты строки</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_319")]
  [CustomDescription("Attribute.Interfaces.Document_320")]
  [CustomCategory("Attribute.Interfaces.Document_321")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? DefaultRowSizeUI
  {
    [DebuggerStepThrough] get
    {
      float defaultRowSize = this.GetDefaultRowSize(this.Template as RectangleElement, (CellContext) null);
      return (double) defaultRowSize == 0.0 ? new float?() : new float?(defaultRowSize);
    }
    set
    {
      float? defaultRowSizeUi = this.DefaultRowSizeUI;
      float? nullable = value;
      if ((double) defaultRowSizeUi.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & defaultRowSizeUi.HasValue == nullable.HasValue)
        return;
      float num = 0.0f;
      if (value.HasValue)
        num = value.Value;
      this.SetDefaultRowSize(num, true, true, true, true);
    }
  }

  /// <summary>Высота строки для отрисовки сетки, новых строк и кратной высоты строки</summary>
  [Browsable(false)]
  public virtual float DefaultRowSize
  {
    [DebuggerStepThrough] get
    {
      return this.GetDefaultRowSize(this.Template as RectangleElement, (CellContext) null);
    }
    set => this.SetDefaultRowSize(value, true, true, true, true);
  }

  /// <summary>Получить значение DefaultRowSize, учитывая наследование</summary>
  /// <returns></returns>
  public float GetDefaultRowSize()
  {
    return this.GetDefaultRowSize(this.Template as RectangleElement, (CellContext) null);
  }

  /// <summary>Получить значение DefaultRowSize, учитывая наследование</summary>
  public virtual float GetDefaultRowSize(RectangleElement template, CellContext context)
  {
    if ((this.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) != OverrideFlags2.None || (this.overrideFlags & OverrideFlags.DefaultRowSize) != OverrideFlags.None)
      return this.defaultRowSize;
    if ((this.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) != OverrideFlags2.None && (this.overrideFlags & OverrideFlags.DefaultRowSize) == OverrideFlags.None && template != null)
      return this.defaultRowSize = template.defaultRowSize;
    if (context != null && (double) context.RowSize_NN != 0.0)
      return context.RowSize_NN;
    TableData parentCell = this.ParentCell;
    return parentCell != null ? parentCell.DefaultRowSize : this.defaultRowSize;
  }

  /// <summary>Назначить новое значение DefaultRowSize</summary>
  public virtual void SetDefaultRowSize(
    float value,
    bool recursive,
    bool setOverrideFlags,
    bool updateUI,
    bool updateLayout)
  {
    if ((double) this.DefaultRowSize == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DefaultRowSize", (object) this.DefaultRowSize, (object) value);
    this.defaultRowSize = (double) value >= 0.0 ? value : throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_83"));
    if (setOverrideFlags)
    {
      this.overrideFlags |= OverrideFlags.DefaultRowSize;
      this.overrideFlags2 |= OverrideFlags2.ParentDefaultRowSize;
    }
    if (recursive && this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is RectangleElement node && (node.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) == OverrideFlags2.None && (node.overrideFlags & OverrideFlags.DefaultRowSize) == OverrideFlags.None)
          node.SetDefaultRowSize(value, true, false, false, false);
      }
    }
    this.ResetTextBoxPaintCache();
    bool needUpdateLayout = (double) this.defaultRowSize != 0.0;
    this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, needUpdateLayout, true, true);
  }

  [Browsable(false)]
  public virtual float OneSkipSize
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      if (parentCell == null)
        return this.properBounds.Height;
      if (parentCell.IsColumn)
      {
        if ((double) this.defaultRowSize != 0.0)
          return this.defaultRowSize;
        List<RowColParams> gridRowsParams = parentCell.GridRowsParams;
        if (gridRowsParams != null && gridRowsParams.Count > 0)
        {
          int gridRowIndex = this.GetGridRowIndex();
          return gridRowIndex != -1 && gridRowIndex < gridRowsParams.Count ? gridRowsParams[gridRowIndex].Size : gridRowsParams[0].Size;
        }
        if ((double) this.MinHeight != 0.0)
          return this.MinHeight;
        return this.Template is RectangleElement template ? template.properBounds.Height : this.properBounds.Height;
      }
      List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
      if (gridColumnsParams == null || gridColumnsParams.Count <= 0)
        return this.properBounds.Width;
      int gridColumnIndex = this.GetGridColumnIndex();
      return gridColumnIndex != -1 && gridColumnIndex < gridColumnsParams.Count ? gridColumnsParams[gridColumnIndex].Size : gridColumnsParams[0].Size;
    }
  }

  /// <summary>Размер всех пропусков перед</summary>
  protected virtual float SkipSizeBefore
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsFixedStructureArea || this.IgnoreSkipOuterCells && this.Index == 0 || (double) this.skipCellsBefore == 0.0 || (this.overrideFlags3 & OverrideFlags3.IgnoreSkipBefore) != OverrideFlags3.None || !this.IsFirstInFlow ? 0.0f : this.skipCellsBefore * this.OneSkipSize;
    }
  }

  /// <summary>Размер всех пропусков после</summary>
  protected virtual float SkipSizeAfter
  {
    [DebuggerStepThrough] get
    {
      return (double) this.skipCellsAfter == 0.0 || !this.IsLastInFlow && (this.nextCell == null || !this.nextCell.AllFlowsIsEmpty()) || this.IgnoreSkipOuterCells && this.parent != null && this.Index == this.parent.NodesCount - 1 ? 0.0f : this.skipCellsAfter * this.OneSkipSize;
    }
  }

  /// <summary>Обновить размеры с учётом пропусков строк после ячейки для этой ячейки и всех наследников в конце таблиц</summary>
  public void UpdateBoundsSkipAfter()
  {
    if (this.nodes != null && this.nodes.Count > 0)
    {
      if (!(this.nodes[this.nodes.Count - 1] is RectangleElement node))
        return;
      float height = node.bounds.Height;
      node.UpdateBoundsSkipAfter();
      TableData tableData = this as TableData;
      if ((double) height != (double) node.bounds.Height && tableData != null && !tableData.IsFixedStructureArea && (!tableData.IsTopLevelTable || (double) tableData.MaxHeight == 0.0 || !tableData.IsPageFlow))
      {
        this.setProperBounds(new RectangleF(this.properBounds.X, this.properBounds.Y, this.properBounds.Width, this.properBounds.Height + node.bounds.Height - height));
        this.SetCellSizes(this.bounds with
        {
          Size = this.CalcSizeFromProper(this.properBounds.Size)
        }, true, false, false, false);
      }
      else
      {
        if ((double) this.skipCellsAfter == 0.0)
          return;
        this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
      }
    }
    else
    {
      if ((double) this.skipCellsAfter == 0.0)
        return;
      this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
    }
  }

  /// <summary>Количество пропусков перед (может быть нецелым)</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_322")]
  [CustomDescription("Attribute.Interfaces.Document_323")]
  [CustomCategory("Attribute.Interfaces.Document_324")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float SkipCellsBefore
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsFixedStructureArea ? 0.0f : this.skipCellsBefore;
    }
    set => this.SetSkipCellsBefore(value, false, true, true);
  }

  /// <summary>Назначить значение SkipCellsBefore</summary>
  /// <param name="value">Значение</param>
  /// <param name="forPlugin">Выставлять флаг SkipBeforeForPlugin</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public virtual void SetSkipCellsBefore(
    float value,
    bool forPlugin,
    bool updateUI,
    bool updateLayout)
  {
    if (forPlugin)
      this.overrideFlags2 |= OverrideFlags2.SkipBeforeForPlugin;
    else
      this.overrideFlags2 &= ~OverrideFlags2.SkipBeforeForPlugin;
    if ((double) this.skipCellsBefore == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "SkipCellsBefore", (object) this.SkipCellsBefore, (object) value);
    this.skipCellsBefore = value;
    this.overrideFlags |= OverrideFlags.SkipBefore;
    if (this.IsTableCell)
    {
      RectangleF bounds = this.Bounds;
      this.setProperBounds(new RectangleF(this.CalcProperLocation(bounds.Location), this.properBounds.Size));
      SizeF size = this.CalcSizeFromProper(this.properBounds.Size);
      if (bounds.Size != size)
      {
        this.setBounds(BoundsHelper.SetSize(this.bounds, size));
        this.needUpdateUIGeometry = true;
      }
    }
    this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, true, true, true);
  }

  /// <summary>Игнорировать пропуск перед строкой вначале страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_575")]
  [CustomDescription("Attribute.Interfaces.Document_576")]
  [CustomCategory("Attribute.Interfaces.Document_489")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool NonSkipBeforeAtStartPage
  {
    [DebuggerStepThrough] get
    {
      if (!this.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage))
      {
        if (this.Template is RectangleElement template)
          return template.NonSkipBeforeAtStartPage;
        if (this.OwnerDocument != null)
          return this.OwnerDocument.DefaultNonSkipAtStartPage;
      }
      return this.CheckFlags((byte) 64 /*0x40*/);
    }
    set => this.SetNonSkipBeforeAtStartPage(value, true, true, true);
  }

  /// <summary>Задать новое значение свойству NonSkipBeforeAtStartPage без вызова обработчиков</summary>
  /// <param name="value">Значение</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignNonSkipBeforeAtStartPage(bool value, bool setOverrideFlag)
  {
    this.SetFlags((byte) 64 /*0x40*/, value);
    if (!setOverrideFlag)
      return;
    this.overrideFlags3 |= OverrideFlags3.NonSkipBeforeAtStartPage;
  }

  /// <summary>Задать новое значение свойству NonSkipBeforeAtStartPage</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void SetNonSkipBeforeAtStartPage(
    bool value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (this.NonSkipBeforeAtStartPage == value)
      return;
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "NonSkipBeforeAtStartPage", (object) this.NonSkipBeforeAtStartPage, (object) value);
    this.SetFlags((byte) 64 /*0x40*/, value);
    this.overrideFlags3 |= OverrideFlags3.NonSkipBeforeAtStartPage;
    if (this.IsFirstCellOnPage)
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Только для внутреннего использования. Назначить значение SkipCellsBefore без запуска обработчиков</summary>
  /// <param name="value">Значение</param>
  internal void AssignSkipCellsBefore(float value) => this.skipCellsBefore = value;

  /// <summary>Количество пропусков после (может быть нецелым)</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_325")]
  [CustomDescription("Attribute.Interfaces.Document_326")]
  [CustomCategory("Attribute.Interfaces.Document_327")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float SkipCellsAfter
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsFixedStructureArea ? 0.0f : this.skipCellsAfter;
    }
    set => this.SetSkipCellsAfter(value, false, true, true);
  }

  /// <summary>Игнорировать пропуск строк на крайних ячейках</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_559")]
  [CustomDescription("Attribute.Interfaces.Document_560")]
  [CustomCategory("Attribute.Interfaces.Document_327")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool IgnoreSkipOuterCells
  {
    [DebuggerStepThrough] get => this.ignoreSkipOuterCells;
    set => this.SetIgnoreSkipOuterCells(value, true, true);
  }

  /// <summary>Игнорировать пропуск строк перед таблицей</summary>
  /// <returns></returns>
  protected bool IgnoreSkipBefore()
  {
    if (this.Parent == null)
      return false;
    if ((this.overrideFlags3 & OverrideFlags3.IgnoreSkipBefore) != OverrideFlags3.None)
      return true;
    return this.IgnoreSkipOuterCells && this.Index == 0;
  }

  /// <summary>Игнорировать пропуск строк после таблицы</summary>
  /// <returns></returns>
  protected bool IgnoreSkipAfter()
  {
    return this.Parent != null && this.IgnoreSkipOuterCells && this.Index == this.Parent.NodesCount - 1;
  }

  /// <summary>Назначить значение SkipCellsAfter</summary>
  /// <param name="value">Значение</param>
  /// <param name="forPlugin">Выставлять флаг SkipBeforeForPlugin</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public virtual void SetIgnoreSkipOuterCells(bool value, bool updateUI, bool updateLayout)
  {
    if (this.ignoreSkipOuterCells == value)
      return;
    this.ignoreSkipOuterCells = value;
    this.overrideFlags3 |= OverrideFlags3.IgnoreSkipOuterCells;
    if (this.IsTableCell)
    {
      RectangleF bounds = this.Bounds;
      this.setProperBounds(new RectangleF(this.CalcProperLocation(bounds.Location), this.properBounds.Size));
      SizeF size = this.CalcSizeFromProper(this.properBounds.Size);
      if (bounds.Size != size)
      {
        this.setBounds(BoundsHelper.SetSize(this.bounds, size));
        this.needUpdateUIGeometry = true;
      }
    }
    this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, true, true, true);
  }

  /// <summary>Назначить значение SkipCellsAfter</summary>
  /// <param name="value">Значение</param>
  /// <param name="forPlugin">Выставлять флаг SkipBeforeForPlugin</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public virtual void SetSkipCellsAfter(
    float value,
    bool forPlugin,
    bool updateUI,
    bool updateLayout)
  {
    if (forPlugin)
      this.overrideFlags2 |= OverrideFlags2.SkipAfterForPlugin;
    else
      this.overrideFlags2 &= ~OverrideFlags2.SkipAfterForPlugin;
    if ((double) this.skipCellsAfter == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "SkipCellsAfter", (object) this.SkipCellsAfter, (object) value);
    this.skipCellsAfter = value;
    this.overrideFlags |= OverrideFlags.SkipAfter;
    if (forPlugin)
      this.overrideFlags2 |= OverrideFlags2.SkipAfterForPlugin;
    else
      this.overrideFlags2 &= ~OverrideFlags2.SkipAfterForPlugin;
    if (this.IsTableCell)
    {
      SizeF size = this.CalcSizeFromProper(this.ProperBounds.Size);
      if (this.Bounds.Size != size)
      {
        this.setBounds(BoundsHelper.SetSize(this.bounds, size));
        this.needUpdateUIGeometry = true;
      }
    }
    this.UpdateAfterChangeProperties(updateUI, updateUI, updateLayout, true, true, true);
  }

  /// <summary>Только для внутреннего использования. Назначить значение SkipCellsAfter без запуска обработчиков</summary>
  /// <param name="value">Значение</param>
  internal void AssignSkipCellsAfter(float value) => this.skipCellsAfter = value;

  /// <summary>Стандартная процедура обновлений после изменения свойства</summary>
  /// <param name="refreshUI">Обновлять изображение в интерфейсе пользователя</param>
  /// <param name="updateUIGeometry">Обновлять геометрию в интерфейсе пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="needUpdateLayout">Выставить флаг NeedUpdateLayout в true</param>
  /// <param name="updateTemplate">Обновлять элементы сделанные по этому шаблону</param>
  /// <param name="raiseOnChanged">Вызвать OnChanged</param>
  public virtual void UpdateAfterChangeProperties(
    bool refreshUI,
    bool updateUIGeometry,
    bool updateLayout,
    bool needUpdateLayout,
    bool updateTemplate,
    bool raiseOnChanged)
  {
    bool flag = false;
    if (refreshUI | updateUIGeometry && !(flag = this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag))
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      if (refreshUI | updateUIGeometry && this.SuspendedRefreshUIFlag)
        this.InvalidateUI(true);
      if (updateUIGeometry)
        this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (updateLayout & needUpdateLayout)
      {
        flag = true;
        this.ResumeUpdateRefreshUI(false, false);
      }
      if (needUpdateLayout)
        this.SetNeedUpdateLayoutFlag(true, true, refreshUI | updateUIGeometry, updateLayout);
    }
    finally
    {
      if (refreshUI | updateUIGeometry && !flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
    if (updateTemplate)
      this.SetPropertiesChangedFlag(true, true, false, false, false);
    if (!raiseOnChanged || this.IsDistributing)
      return;
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Рисовать эллипс вписанный в границы элемента</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_523")]
  [CustomDescription("Attribute.Interfaces.Document_524")]
  [CustomCategory("Attribute.Interfaces.Document_525")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool DrawEllipse
  {
    [DebuggerStepThrough] get => this.drawEllipse;
    set => this.AssignDrawEllipse(value, true);
  }

  /// <summary>Назначить значение DrawEllipse</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить изображение</param>
  public virtual void AssignDrawEllipse(bool value, bool updateUI)
  {
    if (this.drawEllipse == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DrawEllipse", (object) this.DrawEllipse, (object) value);
    this.drawEllipse = value;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, false);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Границы в формате выбранном пользователем</summary>
  [Browsable(false)]
  public RectangleF BoundsForUser
  {
    [DebuggerStepThrough] get
    {
      return this.Page != null ? this.Page.ConvertInternalToUser(this.ProperBounds) : this.ProperBounds;
    }
    set
    {
      if (this.Page != null)
        this.AssignProperBounds(this.Page.ConvertUserToInternal(value), true, true, true);
      else
        this.AssignProperBounds(value, true, true, true);
      this.RecalcRelativeSize();
    }
  }

  /// <summary>Получить границы внутреннего элемента таблицы</summary>
  /// <param name="bounds">Ячейки</param>
  /// <returns>Границы внутреннего элемента</returns>
  public virtual RectangleF CalcClientBounds(RectangleF bounds)
  {
    RectangleF rectangleF = bounds;
    ref RectangleF local = ref rectangleF;
    PointF location = rectangleF.Location;
    double x = (double) location.X + (double) this.BorderWidth;
    location = rectangleF.Location;
    double y = (double) location.Y + (double) this.BorderWidth;
    PointF pointF = new PointF((float) x, (float) y);
    local.Location = pointF;
    rectangleF.Size = this.CalcClientSize(rectangleF.Size);
    return rectangleF;
  }

  /// <summary>Получить границы внутреннего элемента таблицы</summary>
  /// <param name="size">Ячейки</param>
  /// <returns>Границы внутреннего элемента</returns>
  public virtual SizeF CalcClientSize(SizeF size)
  {
    return new SizeF(size.Width - this.BordersWidth, size.Height - this.BordersHeigth);
  }

  /// <summary>Расчитать границы ячейки для заданных границ текста или изображения</summary>
  /// <param name="clientBounds">Границы текста или изображения</param>
  /// <returns></returns>
  public virtual RectangleF CalcBoundsFromClientBounds(RectangleF clientBounds)
  {
    RectangleF rectangleF = clientBounds;
    ref RectangleF local = ref rectangleF;
    PointF location = rectangleF.Location;
    double x = (double) location.X - (double) this.BorderWidth;
    location = rectangleF.Location;
    double y = (double) location.Y - (double) this.BorderWidth;
    PointF pointF = new PointF((float) x, (float) y);
    local.Location = pointF;
    rectangleF.Size = this.CalcSizeFromClientSize(rectangleF.Size);
    return rectangleF;
  }

  /// <summary>Получить границы внутреннего элемента таблицы</summary>
  /// <param name="size">Ячейки</param>
  /// <returns>Границы внутреннего элемента</returns>
  public virtual SizeF CalcSizeFromClientSize(SizeF size)
  {
    return new SizeF(size.Width + this.BordersWidth, size.Height + this.BordersHeigth);
  }

  /// <summary>Границы внутреннего элемента таблицы</summary>
  [Browsable(false)]
  public virtual RectangleF ClientBounds
  {
    [DebuggerStepThrough] get => this.CalcClientBounds(this.Bounds);
  }

  /// <summary>Суммарная высота полей сверху и снизу</summary>
  protected virtual float BordersHeigth
  {
    [DebuggerStepThrough] get => 2f * this.BorderWidth;
  }

  /// <summary>Суммарная ширина полей слева и справа</summary>
  protected virtual float BordersWidth
  {
    [DebuggerStepThrough] get => 2f * this.BorderWidth;
  }

  /// <summary>Поле слева</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_328")]
  [CustomDescription("Attribute.Interfaces.Document_329")]
  [CustomCategory("Attribute.Interfaces.Document_330")]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public virtual float LeftMargin
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null ? ownerDocument.DefaultLeftRightMargin : 0.0f;
    }
  }

  /// <summary>Поле справа</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_331")]
  [CustomDescription("Attribute.Interfaces.Document_332")]
  [CustomCategory("Attribute.Interfaces.Document_333")]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public virtual float RightMargin
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null ? ownerDocument.DefaultLeftRightMargin : 0.0f;
    }
  }

  /// <summary>Поле сверху</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_334")]
  [CustomDescription("Attribute.Interfaces.Document_335")]
  [CustomCategory("Attribute.Interfaces.Document_336")]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public virtual float TopMargin
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null ? ownerDocument.DefaultTopBottomMargin : 0.0f;
    }
  }

  /// <summary>Поле снизу</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_337")]
  [CustomDescription("Attribute.Interfaces.Document_338")]
  [CustomCategory("Attribute.Interfaces.Document_339")]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public virtual float BottomMargin
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null ? ownerDocument.DefaultTopBottomMargin : 0.0f;
    }
  }

  /// <summary>Поля ячейки</summary>
  [Browsable(false)]
  public virtual MarginsF Margins
  {
    [DebuggerStepThrough] get
    {
      return new MarginsF(this.LeftMargin, this.RightMargin, this.TopMargin, this.BottomMargin);
    }
  }

  /// <summary>Минимальная базовая высота ячейки (без учёта содержимого)</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_587")]
  [CustomDescription("Attribute.Interfaces.Document_588")]
  [CustomCategory("Attribute.Interfaces.Document_309")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float MinHeight
  {
    [DebuggerStepThrough] get => this.minHeight;
    set => this.AssignMinHeight(value, true, true, true);
  }

  /// <summary>Высота содержимого ячейки</summary>
  [Category("Debug")]
  public virtual float ContentHeight
  {
    [DebuggerStepThrough] get => this.minHeight;
  }

  /// <summary>Задать новое значение свойству MinHeight</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг, сбрасывающий наследование</param>
  public virtual void AssignMinHeight(
    float value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if ((double) this.minHeight == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "MinBaseHeight", (object) this.MinHeight, (object) value);
    this.minHeight = value;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.MinHeight;
    this.BeginChanges(true);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.EndChanges(true);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Максимальная высота ячейки. Если 0, то высота неограничена сверху</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_340")]
  [CustomDescription("Attribute.Interfaces.Document_341")]
  [CustomCategory("Attribute.Interfaces.Document_309")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float MaxHeight
  {
    [DebuggerStepThrough] get => this.maxHeight;
    set => this.AssignMaxHeight(value, true, true, true);
  }

  /// <summary>Задать новое значение свойству MaxHeight</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг, сбрасывающий наследование</param>
  public virtual void AssignMaxHeight(
    float value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if ((double) this.maxHeight == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "MaxHeight", (object) this.MaxHeight, (object) value);
    this.maxHeight = value;
    if (setOverrideFlag)
      this.SetOverrideFlags(OverrideFlags.MaxHeight);
    this.BeginChanges(true);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.EndChanges(true);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Минимальная ширина ячейки</summary>
  [Category("Debug")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float MinWidth
  {
    [DebuggerStepThrough] get => this.minWidth;
    set => this.AssignMinWidth(value, true, true, true);
  }

  /// <summary>Задать новое значение свойству MinWidth</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг, сбрасывающий наследование</param>
  public virtual void AssignMinWidth(
    float value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if ((double) this.minWidth == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "MinWidth", (object) this.MinWidth, (object) value);
    this.minWidth = value;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.MinWidth;
    this.BeginChanges(true);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.EndChanges(true);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
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
  public virtual bool CheckBordersStatus(
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

  /// <summary>Размер полей в миллиметрах</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_343")]
  [CustomDescription("Attribute.Interfaces.Document_344")]
  [CustomCategory("Attribute.Interfaces.Document_345")]
  [Browsable(false)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float BorderWidth
  {
    [DebuggerStepThrough] get => this.borderWidth;
    set => this.SetBorderWidth(value, true, true);
  }

  /// <summary>Назначить значение свойству BorderWidth</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void SetBorderWidth(float value, bool updateUI, bool updateLayout)
  {
    if ((double) this.borderWidth == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "BorderWidth", (object) this.BorderWidth, (object) value);
    this.borderWidth = value;
    this.SetNeedUpdateUIGeometryRecursive(true, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить стили линий границы</summary>
  /// <returns>Стили линий границы</returns>
  /// <param name="gridCol">Параметры столбцов</param>
  /// <param name="findGridParams">Использовать параметры столбцов</param>
  /// <param name="parentInnerHorizontalLine">Внутренняя горизонтальная линия родительской таблицы</param>
  public virtual RectangleBorder GetBorders(
    RowColParams gridCol,
    bool findGridParams,
    BorderLine parentInnerHorizontalLine)
  {
    RectangleElement template = this.Template as RectangleElement;
    CustomBorder borders = new CustomBorder();
    borders.Top = this.GetTopBorderLine(template);
    borders.Bottom = this.GetBottomBorderLine(template);
    borders.InnerHorizontal = this.GetInnerHorizontalLine(template, parentInnerHorizontalLine);
    borders.Left = this.GetLeftBorderLine(template, findGridParams, ref gridCol);
    borders.Right = this.GetRightBorderLine(template, findGridParams, ref gridCol);
    return (RectangleBorder) borders;
  }

  /// <summary>Получить стиль линии верхней границы</summary>
  /// <param name="template">Шаблон</param>
  public virtual BorderLine GetTopBorderLine(RectangleElement template)
  {
    BorderLine topBorderLine = (BorderLine) null;
    if (template != null && template.borders != null && !this.IsOverridden(OverrideFlags.TopBorder))
      topBorderLine = template.borders.Top;
    if (topBorderLine == null && this.borders != null)
      topBorderLine = this.borders.Top;
    if (topBorderLine == null)
      topBorderLine = this.DefaultBorderLine;
    return topBorderLine;
  }

  /// <summary>Получить стиль линии внутренней горизонтальной границы</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="parentInnerHorizontalLine">Внутренняя горизонтальная линия родительской таблицы</param>
  public virtual BorderLine GetInnerHorizontalLine(
    RectangleElement template,
    BorderLine parentInnerHorizontalLine)
  {
    BorderLine innerHorizontalLine = (BorderLine) null;
    if (!this.IsOverridden2(OverrideFlags2.ParentInnerHorizontalLine))
    {
      if (parentInnerHorizontalLine != null)
        innerHorizontalLine = parentInnerHorizontalLine;
      else if (this.ParentCell != null)
        innerHorizontalLine = this.ParentCell.GetInnerHorizontalLine(this.Parent.Template as RectangleElement, (BorderLine) null);
    }
    if (!this.IsOverridden3(OverrideFlags3.InnerHorizontalLine) && template != null && template.borders != null)
      innerHorizontalLine = template.borders.InnerHorizontal;
    if (innerHorizontalLine == null && this.borders != null)
      innerHorizontalLine = this.borders.InnerHorizontal;
    if (innerHorizontalLine == null && this.borders != null)
      innerHorizontalLine = this.borders.Bottom;
    if (innerHorizontalLine == null)
      innerHorizontalLine = this.DefaultBorderLine;
    return innerHorizontalLine;
  }

  /// <summary>Получить стиль линии нижней границы</summary>
  /// <param name="template">Шаблон</param>
  public virtual BorderLine GetBottomBorderLine(RectangleElement template)
  {
    BorderLine bottomBorderLine = (BorderLine) null;
    if (template != null && template.borders != null && !this.IsOverridden(OverrideFlags.BottomBorder))
      bottomBorderLine = template.borders.Bottom;
    if (bottomBorderLine == null && this.borders != null)
      bottomBorderLine = this.borders.Bottom;
    if (bottomBorderLine == null)
      bottomBorderLine = this.DefaultBorderLine;
    return bottomBorderLine;
  }

  /// <summary>Получить стиль линии левой границы</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="useGridParams">Использовать сетку столбцов таблицы</param>
  /// <param name="gridColumns">Столбцы таблицы</param>
  public virtual BorderLine GetLeftBorderLine(
    RectangleElement template,
    bool useGridParams,
    ref RowColParams gridColumns)
  {
    BorderLine leftBorderLine = (BorderLine) null;
    if (!this.IsOverridden(OverrideFlags.LeftBorder) && !this.IsOverridden2(OverrideFlags2.ColumnLeftBorder))
    {
      if (useGridParams && gridColumns == null)
        gridColumns = this.GetGridColumnParams();
      if (gridColumns != null)
        leftBorderLine = gridColumns.BorderLine1;
    }
    else if (!this.IsOverridden(OverrideFlags.LeftBorder) && this.IsOverridden2(OverrideFlags2.ColumnLeftBorder) && template != null && template.borders != null)
      leftBorderLine = template.borders.Left;
    if (leftBorderLine == null && this.borders != null)
      leftBorderLine = this.borders.Left;
    if (leftBorderLine == null)
      leftBorderLine = this.DefaultBorderLine;
    return leftBorderLine;
  }

  /// <summary>Получить стиль линии правой границы</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="useGridParams">Использовать сетку столбцов таблицы</param>
  /// <param name="gridColumns">Столбцы таблицы</param>
  public virtual BorderLine GetRightBorderLine(
    RectangleElement template,
    bool useGridParams,
    ref RowColParams gridColumns)
  {
    BorderLine rightBorderLine = (BorderLine) null;
    if (!this.IsOverridden(OverrideFlags.RightBorder) && !this.IsOverridden2(OverrideFlags2.ColumnRightBorder))
    {
      if (useGridParams && gridColumns == null)
        gridColumns = this.GetGridColumnParams();
      if (gridColumns != null)
        rightBorderLine = gridColumns.BorderLine2;
    }
    else if (!this.IsOverridden(OverrideFlags.RightBorder) && this.IsOverridden2(OverrideFlags2.ColumnRightBorder) && template != null && template.borders != null)
      rightBorderLine = template.borders.Right;
    if (rightBorderLine == null && this.borders != null)
      rightBorderLine = this.borders.Right;
    if (rightBorderLine == null)
      rightBorderLine = this.DefaultBorderLine;
    return rightBorderLine;
  }

  /// <summary>Линии границ прямоугольника</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_346")]
  [CustomDescription("Attribute.Interfaces.Document_347")]
  [CustomCategory("Attribute.Interfaces.Document_348")]
  [RefreshProperties(RefreshProperties.All)]
  [Browsable(false)]
  public virtual RectangleBorder Borders
  {
    [DebuggerStepThrough] get
    {
      return this.borders == null || this.borders.InnerHorizontal == null || this.borders.Bottom == null || this.borders.Top == null || this.borders.Left == null || this.borders.Right == null ? this.GetBorders((RowColParams) null, true, (BorderLine) null) : this.borders;
    }
    set
    {
      if (this.borders == value)
        return;
      bool flag = false;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (Borders), (object) this.Borders, (object) value);
      if (value == null)
      {
        this.borders = value;
        this.SetAdjoiningTopCellBorder((BorderLine) null);
        this.SetAdjoiningRightCellBorder((BorderLine) null);
        this.SetAdjoiningBottomCellBorder((BorderLine) null);
        this.SetAdjoiningLeftCellBorder((BorderLine) null);
        flag = true;
      }
      else
      {
        RectangleElement template = (RectangleElement) this.Template;
        BorderLine topBorderLine = this.GetTopBorderLine(template);
        BorderLine bottomBorderLine = this.GetBottomBorderLine(template);
        BorderLine innerHorizontalLine = this.GetInnerHorizontalLine(template, (BorderLine) null);
        RowColParams gridColumns = (RowColParams) null;
        BorderLine leftBorderLine = this.GetLeftBorderLine(template, true, ref gridColumns);
        BorderLine rightBorderLine = this.GetRightBorderLine(template, true, ref gridColumns);
        RectangleBorder borders = this.borders;
        TableData tableData = this as TableData;
        this.borders = value.Clone();
        BorderLine top = value.Top;
        if (topBorderLine != top)
        {
          this.overrideFlags |= OverrideFlags.TopBorder;
          this.SetAdjoiningTopCellBorder(this.borders.Top);
          tableData?.SetTopBorderLineForCell(this.borders.Top);
          flag = true;
        }
        else if (borders == null || borders.Top == null)
          this.borders.Top = (BorderLine) null;
        if (bottomBorderLine != value.Bottom)
        {
          this.overrideFlags |= OverrideFlags.BottomBorder;
          this.SetAdjoiningBottomCellBorder(this.borders.Bottom);
          tableData?.SetBottomBorderLineForCell(this.borders.Bottom);
          flag = true;
        }
        else if (borders == null || borders.Bottom == null)
          this.borders.Bottom = (BorderLine) null;
        if (innerHorizontalLine != value.InnerHorizontal)
        {
          this.overrideFlags3 |= OverrideFlags3.InnerHorizontalLine;
          this.overrideFlags2 |= OverrideFlags2.ParentInnerHorizontalLine;
          flag = true;
        }
        else if (borders == null || borders.InnerHorizontal == null)
          this.borders.InnerHorizontal = (BorderLine) null;
        if (leftBorderLine != value.Left)
        {
          this.overrideFlags |= OverrideFlags.LeftBorder;
          this.overrideFlags2 |= OverrideFlags2.ColumnLeftBorder;
          this.SetAdjoiningLeftCellBorder(this.borders.Left);
          tableData?.SetLeftBorderLineForCell(this.borders.Left);
          flag = true;
        }
        else if (borders == null || borders.Left == null)
          this.borders.Left = (BorderLine) null;
        if (rightBorderLine != value.Right)
        {
          this.overrideFlags |= OverrideFlags.RightBorder;
          this.overrideFlags2 |= OverrideFlags2.ColumnRightBorder;
          this.SetAdjoiningRightCellBorder(this.borders.Right);
          tableData?.SetRightBorderLineForCell(this.borders.Right);
          flag = true;
        }
        else if (borders == null || borders.Right == null)
          this.borders.Right = (BorderLine) null;
      }
      if (!flag)
        return;
      this.RefreshUI();
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Только для PropertyGrid! Линия верхней границы прямоугольника.</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_349")]
  [CustomDescription("Attribute.Interfaces.Document_350")]
  [CustomCategory("Attribute.Interfaces.Document_351")]
  public virtual BorderLine TopBorderLine
  {
    [DebuggerStepThrough] get => this.GetTopBorderLine(this.Template as RectangleElement).Clone();
    set => this.AssignTopBorderLine(value, true);
  }

  /// <summary>Назначить значение TopBorderLine</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  public void AssignTopBorderLine(BorderLine value, bool updateUI)
  {
    if (this.TopBorderLine == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "TopBorderLine", (object) this.TopBorderLine, (object) value);
    if (this.borders == null)
    {
      CustomBorder customBorder = new CustomBorder();
      customBorder.Top = value;
      this.borders = (RectangleBorder) customBorder;
    }
    else
      this.borders = (RectangleBorder) new CustomBorder(value, this.borders.InnerHorizontal, this.borders.Bottom, this.borders.Left, this.borders.Right);
    this.SetAdjoiningTopCellBorder(value);
    if (this is TableData tableData)
      tableData.SetTopBorderLineForCell(value);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Линия верхней границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_352")]
  [CustomDescription("Attribute.Interfaces.Document_353")]
  [CustomCategory("Attribute.Interfaces.Document_354")]
  public virtual BorderLine BottomBorderLine
  {
    [DebuggerStepThrough] get
    {
      return this.GetBottomBorderLine(this.Template as RectangleElement).Clone();
    }
    set => this.AssignBottomBorderLine(value, true);
  }

  /// <summary>Назначить значение BottomBorderLine</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  public void AssignBottomBorderLine(BorderLine value, bool updateUI)
  {
    if (this.BottomBorderLine == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "BottomBorderLine", (object) this.BottomBorderLine, (object) value);
    if (this.borders == null)
    {
      CustomBorder customBorder = new CustomBorder();
      customBorder.Bottom = value;
      this.borders = (RectangleBorder) customBorder;
    }
    else
      this.borders = (RectangleBorder) new CustomBorder(this.borders.Top, this.borders.InnerHorizontal, value, this.borders.Left, this.borders.Right);
    this.SetAdjoiningBottomCellBorder(value);
    if (this is TableData tableData)
      tableData.SetBottomBorderLineForCell(value);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Внутренняя линия прямоугольника</summary>
  [DisplayName("Внутренняя граница")]
  [Description("Настройки линии внутренней границы")]
  [Category("Границы")]
  public virtual BorderLine InnerBorderLine
  {
    [DebuggerStepThrough] get
    {
      return this.GetInnerHorizontalLine(this.Template as RectangleElement, (BorderLine) null).Clone();
    }
    set => this.AssignInnerBorderLine(value, true);
  }

  /// <summary>Назначить значение InnerBorderLine</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  public void AssignInnerBorderLine(BorderLine value, bool updateUI)
  {
    if (this.InnerBorderLine == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "InnerBorderLine", (object) this.InnerBorderLine, (object) value);
    if (this.borders == null)
    {
      CustomBorder customBorder = new CustomBorder();
      customBorder.InnerHorizontal = value;
      this.borders = (RectangleBorder) customBorder;
    }
    else
      this.borders = (RectangleBorder) new CustomBorder(this.borders.Top, value, this.borders.Bottom, this.borders.Left, this.borders.Right);
    this.overrideFlags3 |= OverrideFlags3.InnerHorizontalLine;
    this.overrideFlags2 |= OverrideFlags2.ParentInnerHorizontalLine;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Линия верхней границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_355")]
  [CustomDescription("Attribute.Interfaces.Document_356")]
  [CustomCategory("Attribute.Interfaces.Document_357")]
  public virtual BorderLine LeftBorderLine
  {
    [DebuggerStepThrough] get
    {
      RowColParams gridColumns = (RowColParams) null;
      BorderLine leftBorderLine = this.GetLeftBorderLine(this.Template as RectangleElement, true, ref gridColumns);
      if (leftBorderLine != null)
        leftBorderLine = leftBorderLine.Clone();
      return leftBorderLine;
    }
    set => this.AssignLeftBorderLine(value, true);
  }

  /// <summary>Назначить значение LeftBorderLine</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  public void AssignLeftBorderLine(BorderLine value, bool updateUI)
  {
    if (this.LeftBorderLine == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "LeftBorderLine", (object) this.LeftBorderLine, (object) value);
    if (this.borders == null)
    {
      CustomBorder customBorder = new CustomBorder();
      customBorder.Left = value;
      this.borders = (RectangleBorder) customBorder;
    }
    else
      this.borders = (RectangleBorder) new CustomBorder(this.borders.Top, this.borders.InnerHorizontal, this.borders.Bottom, value, this.borders.Right);
    this.overrideFlags2 |= OverrideFlags2.ColumnLeftBorder;
    this.overrideFlags |= OverrideFlags.LeftBorder;
    this.SetAdjoiningLeftCellBorder(value);
    if (this is TableData tableData)
      tableData.SetLeftBorderLineForCell(value);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Линия верхней границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_358")]
  [CustomDescription("Attribute.Interfaces.Document_359")]
  [CustomCategory("Attribute.Interfaces.Document_360")]
  public virtual BorderLine RightBorderLine
  {
    [DebuggerStepThrough] get
    {
      RowColParams gridColumns = (RowColParams) null;
      BorderLine rightBorderLine = this.GetRightBorderLine(this.Template as RectangleElement, true, ref gridColumns);
      if (rightBorderLine != null)
        rightBorderLine = rightBorderLine.Clone();
      return rightBorderLine;
    }
    set => this.AssignRightBorderLine(value, true);
  }

  /// <summary>Назначить значение RightBorderLine</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  public void AssignRightBorderLine(BorderLine value, bool updateUI)
  {
    if (this.RightBorderLine == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "RightBorderLine", (object) this.RightBorderLine, (object) value);
    if (this.borders == null)
    {
      CustomBorder customBorder = new CustomBorder();
      customBorder.Right = value;
      this.borders = (RectangleBorder) customBorder;
    }
    else
      this.borders = (RectangleBorder) new CustomBorder(this.borders.Top, this.borders.InnerHorizontal, this.borders.Bottom, this.borders.Left, value);
    this.overrideFlags2 |= OverrideFlags2.ColumnRightBorder;
    this.overrideFlags |= OverrideFlags.RightBorder;
    this.SetAdjoiningRightCellBorder(value);
    if (this is TableData tableData)
      tableData.SetRightBorderLineForCell(value);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Установить стиль линии верхней границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public virtual void SetTopBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "Borders");
    int overrideFlags = (int) this.overrideFlags;
    if (this.borders == null)
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    else if (!(this.borders is CustomBorder customBorder))
      this.borders = (RectangleBorder) (customBorder = (CustomBorder) this.borders.Clone());
    customBorder.SetTopLine(borderLine);
    this.overrideFlags |= OverrideFlags.TopBorder;
    this.OnChanged(new Changed_EventArgs());
    if (!setAdjoiningLine)
      return;
    this.SetAdjoiningTopCellBorder(borderLine?.Clone());
  }

  /// <summary>Установить нижнюю границу смежной ячейки сверху</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  public virtual void SetAdjoiningTopCellBorder(BorderLine borderLine)
  {
    TableData parentCell1 = this.ParentCell;
    if (parentCell1 == null)
      return;
    if (parentCell1.IsColumn)
    {
      int index = this.Index;
      if (index <= 0 || !(parentCell1.Nodes[index - 1] is RectangleElement node))
        return;
      if (borderLine != null)
        borderLine = borderLine.Clone();
      node.SetBottomBorderLine(borderLine, false);
    }
    else
    {
      TableData parentCell2 = parentCell1.ParentCell;
      int index1;
      if (parentCell2 == null || !parentCell2.IsColumn || (index1 = parentCell1.Index) <= 0 || !(parentCell2.Nodes[index1 - 1] is TableData node))
        return;
      int gridColumnIndex = this.GetGridColumnIndex();
      if (gridColumnIndex == -1)
        return;
      RectangleElement[] cells = (RectangleElement[]) null;
      node.GetCellPositionForGridColumn(gridColumnIndex, false, out cells);
      if (cells == null)
        return;
      for (int index2 = 0; index2 < cells.Length; ++index2)
      {
        if (borderLine != null)
          borderLine = borderLine.Clone();
        cells[index2].SetBottomBorderLine(borderLine, false);
      }
    }
  }

  /// <summary>Установить стиль линии нижней границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public virtual void SetBottomBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "Borders");
    int overrideFlags = (int) this.overrideFlags;
    if (this.borders == null)
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    else if (!(this.borders is CustomBorder customBorder))
      this.borders = (RectangleBorder) (customBorder = (CustomBorder) this.borders.Clone());
    customBorder.SetBottomLine(borderLine);
    this.overrideFlags |= OverrideFlags.BottomBorder;
    this.OnChanged(new Changed_EventArgs());
    if (!setAdjoiningLine)
      return;
    this.SetAdjoiningBottomCellBorder(borderLine?.Clone());
  }

  /// <summary>Установить верхнюю границу смежной ячейки снизу</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  public virtual void SetAdjoiningBottomCellBorder(BorderLine borderLine)
  {
    TableData parentCell1 = this.ParentCell;
    if (parentCell1 == null)
      return;
    if (parentCell1.IsColumn)
    {
      int index = this.Index;
      if (index >= parentCell1.Nodes.Count - 1 || !(parentCell1.Nodes[index + 1] is RectangleElement node))
        return;
      if (borderLine != null)
        borderLine = borderLine.Clone();
      node.SetTopBorderLine(borderLine, false);
    }
    else
    {
      TableData parentCell2 = parentCell1.ParentCell;
      int index1;
      if (parentCell2 == null || !parentCell2.IsColumn || (index1 = parentCell1.Index) >= parentCell2.Nodes.Count - 1 || !(parentCell2.Nodes[index1 + 1] is TableData node))
        return;
      int gridColumnIndex = this.GetGridColumnIndex();
      if (gridColumnIndex == -1)
        return;
      RectangleElement[] cells = (RectangleElement[]) null;
      node.GetCellPositionForGridColumn(gridColumnIndex, false, out cells);
      if (cells == null)
        return;
      for (int index2 = 0; index2 < cells.Length; ++index2)
      {
        if (borderLine != null)
          borderLine = borderLine.Clone();
        cells[index2].SetTopBorderLine(borderLine, false);
      }
    }
  }

  /// <summary>Установить стиль линии левой границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public virtual void SetLeftBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "Borders");
    int overrideFlags = (int) this.overrideFlags;
    if (this.borders == null)
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    else if (!(this.borders is CustomBorder customBorder))
      this.borders = (RectangleBorder) (customBorder = (CustomBorder) this.borders.Clone());
    customBorder.SetLeftLine(borderLine);
    this.overrideFlags |= OverrideFlags.LeftBorder;
    this.overrideFlags2 |= OverrideFlags2.ColumnLeftBorder;
    this.OnChanged(new Changed_EventArgs());
    if (!setAdjoiningLine)
      return;
    this.SetAdjoiningLeftCellBorder(borderLine?.Clone());
  }

  /// <summary>Установить правую границу смежной ячейки слева</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  public virtual void SetAdjoiningLeftCellBorder(BorderLine borderLine)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null || !parentCell.IsRow || this.Index <= 0 || !(parentCell.Nodes[this.Index - 1] is RectangleElement node))
      return;
    if (borderLine != null)
      borderLine = borderLine.Clone();
    node.SetRightBorderLine(borderLine, false);
  }

  /// <summary>Установить стиль линии правой границы.
  /// При этом настройки по умолчанию больше не будут действовать.</summary>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="setAdjoiningLine">Установить стиль смежной линии в смежной ячейке</param>
  public virtual void SetRightBorderLine(BorderLine borderLine, bool setAdjoiningLine)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((DocumentTreeNode) this, "Borders");
    int overrideFlags = (int) this.overrideFlags;
    if (this.borders == null)
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    else if (!(this.borders is CustomBorder customBorder))
      this.borders = (RectangleBorder) (customBorder = (CustomBorder) this.borders.Clone());
    customBorder.SetRightLine(borderLine);
    this.overrideFlags |= OverrideFlags.RightBorder;
    this.overrideFlags2 |= OverrideFlags2.ColumnRightBorder;
    this.OnChanged(new Changed_EventArgs());
    if (!setAdjoiningLine)
      return;
    this.SetAdjoiningRightCellBorder(borderLine?.Clone());
  }

  /// <summary>Установить левую границу смежной ячейки справа</summary>
  /// <param name="borderLine">Стиль линии границы</param>
  public virtual void SetAdjoiningRightCellBorder(BorderLine borderLine)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null || !parentCell.IsRow || this.Index >= parentCell.Nodes.Count - 1 || !(parentCell.Nodes[this.Index + 1] is RectangleElement node))
      return;
    if (borderLine != null)
      borderLine = borderLine.Clone();
    node.SetLeftBorderLine(borderLine, false);
  }

  /// <summary>Назначить один стиль всем линиям границы</summary>
  /// <param name="value">Стиль линий границы</param>
  /// <param name="setAdjoiningLines">Установить стиль смежной линии в смежной ячейке</param>
  public virtual void SetOneTypeBorderLine(BorderLine value, bool setAdjoiningLines)
  {
    if (this.borders == null)
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    else if (!(this.borders is CustomBorder customBorder))
      this.borders = (RectangleBorder) (customBorder = new CustomBorder());
    if (value != null)
      customBorder.SetLines(value.Clone(), value.Clone(), value.Clone(), value.Clone(), value.Clone());
    this.overrideFlags |= OverrideFlags.TopBorder;
    this.overrideFlags |= OverrideFlags.BottomBorder;
    this.overrideFlags3 |= OverrideFlags3.InnerHorizontalLine;
    this.overrideFlags2 |= OverrideFlags2.ParentInnerHorizontalLine;
    this.overrideFlags |= OverrideFlags.LeftBorder;
    this.overrideFlags2 |= OverrideFlags2.ColumnLeftBorder;
    this.overrideFlags |= OverrideFlags.RightBorder;
    this.overrideFlags2 |= OverrideFlags2.ColumnRightBorder;
    if (setAdjoiningLines)
    {
      this.SetAdjoiningTopCellBorder(value?.Clone());
      this.SetAdjoiningRightCellBorder(value?.Clone());
      this.SetAdjoiningBottomCellBorder(value?.Clone());
      this.SetAdjoiningLeftCellBorder(value?.Clone());
    }
    this.RefreshUI();
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Найти ячейки правая сторона которых имеет заданную координату X</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="x">Координата X</param>
  public virtual void FindResizableRightSide(List<RectangleElement> cells, float x)
  {
    if (!this.IsVisibleNow || (double) x != (double) this.Bounds.Right)
      return;
    cells.Add(this);
  }

  /// <summary>Найти ячейки правая сторона которых имеет заданную координату X</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="x">Координата X</param>
  public virtual void FindResizableLeftSide(List<RectangleElement> cells, float x)
  {
    if (!this.IsVisibleNow || (double) x != (double) this.Bounds.X)
      return;
    cells.Add(this);
  }

  /// <summary>Найти ячейки нижняя сторона которых имеет заданную координату Y</summary>
  /// <param name="cells">Список найденных ячеек</param>
  /// <param name="y">Координата Y</param>
  public virtual void FindResizableBottomSide(List<RectangleElement> cells, float y)
  {
    if (!this.IsVisibleNow || (double) y != (double) this.Bounds.Bottom)
      return;
    cells.Add(this);
  }

  /// <summary>Найти верхнюю родительскую ячейку, нижняя сторона которой имеет заданную координату Y</summary>
  /// <param name="y">Координата Y</param>
  internal RectangleElement FindParentWithBottomSide(float y)
  {
    return this.ParentCell != null && !this.ParentCell.IsFixedStructureArea && (double) this.ParentCell.ProperBounds.Bottom == (double) y ? this.ParentCell.FindParentWithBottomSide(y) : this;
  }

  /// <summary>Получить цвет переднего плана</summary>
  /// <returns>Цвет переднего плана</returns>
  public virtual Color GetForeColor()
  {
    if (this.foreColor.IsEmpty)
    {
      Color color = this.foreColor;
      if ((this.overrideFlags3 & OverrideFlags3.ForeColor) == OverrideFlags3.None)
      {
        RectangleElement template = (RectangleElement) this.Template;
        if (template != null)
          color = template.foreColor;
      }
      if (color.IsEmpty)
        color = PageElementNode.DefaultForeColor;
      this.foreColor = color;
    }
    return this.foreColor;
  }

  /// <summary>Цвет переднего плана</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_361")]
  [CustomDescription("Attribute.Interfaces.Document_362")]
  [CustomCategory("Attribute.Interfaces.Document_363")]
  [Browsable(false)]
  public virtual Color ForeColor
  {
    [DebuggerStepThrough] get => this.GetForeColor();
    set => this.AssignForeColor(value, true);
  }

  /// <summary>Назначить значение ForeColor</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить изображение</param>
  public void AssignForeColor(Color value, bool updateUI)
  {
    if (!(this.foreColor != value))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ForeColor", (object) this.ForeColor, (object) value);
    this.foreColor = value;
    this.overrideFlags3 |= OverrideFlags3.ForeColor;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить цвет фона</summary>
  /// <returns>Цвет фона</returns>
  public virtual Color GetBackColor()
  {
    if (this.HighlightColor != Color.Empty)
      return this.HighlightColor;
    if (this.backColor.IsEmpty)
    {
      Color color = this.backColor;
      if ((this.overrideFlags & OverrideFlags.BackColor) == OverrideFlags.None)
      {
        RectangleElement template = (RectangleElement) this.Template;
        if (template != null)
          color = template.backColor;
      }
      if (color.IsEmpty)
        color = PageElementNode.DefaultBackColor;
      this.backColor = color;
    }
    return this.backColor;
  }

  /// <summary>Цвет фона</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_364")]
  [CustomDescription("Attribute.Interfaces.Document_365")]
  [CustomCategory("Attribute.Interfaces.Document_366")]
  public virtual Color BackColor
  {
    [DebuggerStepThrough] get => this.GetBackColor();
    set => this.AssignBackColor(value, true);
  }

  /// <summary>Назначить значение BackColor</summary>
  /// <param name="value"></param>
  /// <param name="updateUI"></param>
  public void AssignBackColor(Color value, bool updateUI)
  {
    if (!(this.backColor != value))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
    try
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "BackColor", (object) this.BackColor, (object) value);
      this.backColor = value;
      this.overrideFlags |= OverrideFlags.BackColor;
      this.Transparent = this.backColor == Color.Transparent;
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
      if (!updateUI)
        return;
      this.RefreshUI();
      this.OnChanged(new Changed_EventArgs());
    }
    finally
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Нарисовать линию границы согласно заданному стилю</summary>
  /// <param name="g">Объект Graphics выполняющий отрисовку</param>
  /// <param name="borderLine">Стиль линии</param>
  /// <param name="negative">Негатив (инвертированные цвета)</param>
  /// <param name="vertical">true - вертикальная линия, false - горизонтальная</param>
  /// <param name="invisibleOnly">Рисовать только, если невидимая линия</param>
  /// <param name="p">Начало линии</param>
  /// <param name="length">Длина линии</param>
  protected virtual void DrawBorderLine(
    DrawContext context,
    BorderLine borderLine,
    bool negative,
    bool vertical,
    bool invisibleOnly,
    PointF p,
    float length)
  {
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    if (borderLine == null)
      throw new ArgumentNullException(nameof (borderLine));
    ImGraphics graphics = context.Graphics;
    float num1 = PageElementNode.DefaultLineWidth;
    if (this.Page.ContainsAttribute("BLN.ID"))
      num1 = 0.2f;
    Pen pen = (Pen) null;
    bool flag1 = false;
    if (invisibleOnly)
    {
      if (borderLine.Style != BorderStyles.None)
        return;
      pen = negative ? new Pen(VisualNode.InvertColor(RectangleElement.InvisibleLineColor), PageElementNode.DefaultLineWidth) : new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth);
    }
    if (pen == null)
    {
      pen = borderLine.GetPen();
      if (negative && pen != null)
      {
        flag1 = true;
        pen = (Pen) pen.Clone();
        pen.Color = VisualNode.InvertColor(pen.Color);
      }
    }
    if (pen == null)
      return;
    if ((double) pen.Width == (double) PageElementNode.DefaultLineWidth)
      pen.Width = num1;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    Matrix transform = context.Graphics.Transform;
    bool pixelMode = context.PixelMode;
    bool flag2 = context.IsPaint && this.Page != null;
    if (flag2)
      context.Graphics.PageUnit = GraphicsUnit.Pixel;
    if (!context.PixelMode & flag2)
      context.Graphics.Transform = new Matrix();
    PointF pointF1 = new PointF(graphics.DpiX, graphics.DpiY);
    PointF empty = PointF.Empty;
    PointF pointF2 = PointF.Empty;
    PointF pointF3 = p;
    if (borderLine.Style != BorderStyles.Serif)
    {
      pointF2 = !vertical ? new PointF(p.X + length, p.Y) : new PointF(p.X, p.Y + length);
    }
    else
    {
      float num2 = (double) borderLine.SerifWidth > (double) length ? length : borderLine.SerifWidth;
      pointF2 = !vertical ? new PointF(p.X + num2, p.Y) : new PointF(p.X, p.Y + num2);
    }
    if (flag2 && this.Page != null)
    {
      MatrixWrapper m = new MatrixWrapper(transform);
      pointF3 = (PointF) this.Page.ConvertWorldToPixel(pointF3, m);
      pointF2 = (PointF) this.Page.ConvertWorldToPixel(pointF2, m);
    }
    if (flag2)
    {
      pen = (Pen) pen.Clone();
      flag1 = true;
      pen.Width = (float) UnitsConverter.MmToPixels(pen.Width, pointF1.X, true);
    }
    graphics.DrawLine(pen, pointF3, pointF2);
    if (flag1)
      pen.Dispose();
    context.Graphics.PageUnit = pageUnit;
    context.Graphics.Transform = transform;
    context.PixelMode = pixelMode;
  }

  /// <summary>Нарисовать фон элемента</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="properBounds">Границы элемента</param>
  protected virtual void DrawBackground(DrawContext context, RectangleF properBounds)
  {
    if (context.Layer != 0)
      return;
    SolidBrush solidBrush = (SolidBrush) null;
    if (context.IsPaint && !context.IsSelected.HasValue)
      context.IsSelected = new bool?(this.ShowSelected);
    if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
      context.IsFocused = new bool?(this.ShowFocused);
    if ((!context.IsPaint || !context.IsSelected.Value ? 0 : (!context.IsFocused.Value ? 1 : 0)) != 0)
    {
      Color c = this.GetBackColor();
      if ((c == Color.Transparent || this.Transparent) && this.page != null)
        c = Color.White;
      solidBrush = new SolidBrush(VisualNode.InvertColor(c));
    }
    if (solidBrush == null && (!this.Transparent || this.HighlightColor != Color.Empty))
      solidBrush = new SolidBrush(this.GetBackColor());
    if (solidBrush == null)
      return;
    context.Graphics.FillRectangle((Brush) solidBrush, properBounds);
    solidBrush.Dispose();
  }

  /// <summary>Рисует внутренние линии сетки</summary>
  /// <param name="context">Контекст рисования</param>
  /// <param name="gridBounds">Координаты сетки</param>
  /// <param name="rowSize">Размер строки сетки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="gridRows">Строки сетки [устарело]</param>
  protected virtual void DrawGrid(
    DrawContext context,
    RectangleF gridBounds,
    float rowSize,
    List<RowColParams> gridCols,
    List<RowColParams> gridRows)
  {
    PointF location = gridBounds.Location;
    int num = 0;
    bool flag = false;
    bool invisibleOnly = context.Layer == -1 && context.ShowInvisibleLines;
    bool negative = context.Layer == 0 && context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value && !this.InPlaceEditorActive;
    for (; (double) location.Y < (double) gridBounds.Bottom; location.Y += rowSize)
    {
      if (!flag && context.MaterialList != null && context.MaterialList.IndexOf(num - 1) != -1)
      {
        flag = true;
        --num;
      }
      else
      {
        BorderLine borderLine = context.Borders.InnerHorizontal;
        if (borderLine == null || (double) location.Y == (double) gridBounds.Top)
          borderLine = context.Borders.Top;
        this.DrawBorderLine(context, borderLine, negative, false, invisibleOnly, location, gridBounds.Width);
        flag = false;
      }
      ++num;
    }
  }

  /// <summary>Нарисовать границы элемента</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="properBounds">Границы элемента</param>
  /// <param name="gridCol">Столбец сетки</param>
  /// <param name="gridRow">Строка сетки</param>
  /// <param name="findGridParams">Искать строку и столбец сетки если null</param>
  public virtual void DrawFrame(
    DrawContext context,
    RectangleF properBounds,
    RowColParams gridCol,
    RowColParams gridRow,
    bool findGridParams)
  {
    float[] elements = context.Graphics.Transform.Elements;
    bool negative = context.Layer == 0 && context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value && !this.InPlaceEditorActive;
    bool invisibleOnly = context.Layer == -1 && context.ShowInvisibleLines;
    bool pixelMode = context.PixelMode;
    PointF pointF = new PointF(context.Graphics.DpiX, context.Graphics.DpiY);
    if (context.Borders == null)
      context.Borders = this.GetBorders(gridCol, true, context.ParentBorders != null ? context.ParentBorders.InnerHorizontal : (BorderLine) null);
    RectangleBorder borders = context.Borders;
    RectangleF gridBounds = properBounds;
    if (context.IsSkipedSpace)
      gridBounds.Height = context.SkipedSpaceSize;
    if (this.Id == "110")
    {
      int layer = context.Layer;
    }
    if (context.Layer == 0 || context.Layer == -1 && borders.Top.Style == BorderStyles.None)
    {
      int num = context.IsFixedSizeRow_NN ? 1 : 0;
      float rowSize = num != 0 ? context.RowSize_NN : 0.0f;
      if (num == 0 || (double) rowSize == 0.0 || context.IsPaint && this.InPlaceEditorActive || this is TableData && this.NodesCount > 0 && !context.IsSkipedSpace)
        this.DrawBorderLine(context, borders.Top, negative, false, invisibleOnly, gridBounds.Location, gridBounds.Width);
      else if (this.ParentCell != null || context.DrawGrid)
        this.DrawGrid(context, gridBounds, rowSize, (List<RowColParams>) null, (List<RowColParams>) null);
    }
    if ((double) properBounds.Height <= (double) gridBounds.Height && context.Layer == 0 || context.Layer == -1 && borders.Bottom.Style == BorderStyles.None)
      this.DrawBorderLine(context, borders.Bottom, negative, false, invisibleOnly, new PointF(properBounds.X, properBounds.Bottom), properBounds.Width);
    if (context.Layer == 0 || context.Layer == -1 && borders.Right.Style == BorderStyles.None)
      this.DrawBorderLine(context, borders.Right, negative, true, invisibleOnly, new PointF(gridBounds.Right, gridBounds.Y), gridBounds.Height);
    if (context.Layer == 0 || context.Layer == -1 && borders.Left.Style == BorderStyles.None)
      this.DrawBorderLine(context, borders.Left, negative, true, invisibleOnly, gridBounds.Location, gridBounds.Height);
    context.PixelMode = pixelMode;
  }

  /// <summary>Получить границы в пикселях</summary>
  /// <param name="context">Контекст отрисовки элемента</param>
  /// <returns></returns>
  public virtual Rectangle GetPixelBounds(DrawContext context) => Rectangle.Empty;

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    this.DrawCell(context, (List<RowColParams>) null, -1, (List<RowColParams>) null, -1, true);
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  public virtual void DrawCell(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag)
      return;
    TableData parentCell = this.ParentCell;
    RectangleF properBounds = this.ProperBounds;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      properBounds = this.Bounds;
    float num1 = this.skipCellsBefore;
    if (this.IgnoreSkipBefore() || !this.IsFirstInFlow)
      num1 = 0.0f;
    float num2 = this.skipCellsAfter;
    if (this.IgnoreSkipAfter() || !this.IsLastInFlow)
      num2 = 0.0f;
    bool flag = parentCell != null && ((double) num1 >= 1.0 || (double) num2 >= 1.0);
    if (!(!flag ? properBounds : this.Bounds).IntersectsWith(context.ClipRectangle))
      return;
    RectangleElement template = context.Template;
    float? rowSize = context.RowSize;
    bool? isFixedSizeRow = context.IsFixedSizeRow;
    bool? isSelected = context.IsSelected;
    bool? isFocused = context.IsFocused;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    GraphicsState gstate = context.Graphics.Save();
    context.Graphics.PageUnit = GraphicsUnit.Millimeter;
    try
    {
      if (context.IsPaint && (!context.IsSelected.HasValue || !context.IsSelected.Value))
        context.IsSelected = new bool?(this.ShowSelected);
      if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
        context.IsFocused = parentCell == null || !parentCell.IsColumn ? new bool?(this.ShowFocused) : new bool?(false);
      context.Template = this.Template as RectangleElement;
      context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
      context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
      if (context.IsSkipedSpace && (parentCell == null || !parentCell.IsFixedStructureArea))
        properBounds.Height = context.SkipedSpaceSize;
      if (context.Layer == 0)
        this.DrawBackground(context, properBounds);
      RowColParams gridRow = (RowColParams) null;
      if (gridRows != null && rowIndex >= 0 && rowIndex < gridRows.Count)
        gridRow = gridRows[rowIndex];
      RowColParams gridCol = (RowColParams) null;
      if (gridCols != null && colIndex >= 0 && colIndex < gridCols.Count)
        gridCol = gridCols[colIndex];
      if (this.drawEllipse)
        this.DrawEllipseBounds(context, properBounds, gridCol, gridRow, findGridParams);
      else
        this.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
      base.Draw(context);
      if (!(!context.IsSkipedSpace & flag))
        return;
      this.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
    }
    finally
    {
      context.Graphics.PageUnit = pageUnit;
      context.Template = template;
      context.RowSize = rowSize;
      context.IsFixedSizeRow = isFixedSizeRow;
      context.IsSelected = isSelected;
      context.IsFocused = isFocused;
      context.MaterialList = (List<int>) null;
      context.Graphics.Restore(gstate);
    }
  }

  /// <summary>Нарисовать границы элемента</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="properBounds">Границы элемента</param>
  /// <param name="gridCol">Столбец сетки</param>
  /// <param name="gridRow">Строка сетки</param>
  /// <param name="findGridParams">Искать строку и столбец сетки если null</param>
  public virtual void DrawEllipseBounds(
    DrawContext context,
    RectangleF properBounds,
    RowColParams gridCol,
    RowColParams gridRow,
    bool findGridParams)
  {
    bool flag1 = false;
    if (context.Layer == 0)
      flag1 = context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value && !this.InPlaceEditorActive;
    bool flag2 = false;
    if (context.Layer == -1)
      flag2 = context.ShowInvisibleLines;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    context.Graphics.PageUnit = GraphicsUnit.Millimeter;
    RectangleBorder rectangleBorder = this.borders == null || this.borders.Top == null || this.borders.Bottom == null || this.borders.Left == null || this.borders.Right == null ? this.GetBorders(gridCol, findGridParams, (BorderLine) null) : this.Borders;
    if (context.Layer == 0 || context.Layer == -1 && rectangleBorder.Top.Style == BorderStyles.None)
    {
      Pen pen = (Pen) null;
      if (flag2)
      {
        if (rectangleBorder.Top.Style == BorderStyles.None)
          pen = flag1 ? new Pen(VisualNode.InvertColor(RectangleElement.InvisibleLineColor), PageElementNode.DefaultLineWidth) : new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth);
      }
      else
      {
        pen = rectangleBorder.Top.GetPen();
        if (flag1 && pen != null)
          pen.Color = VisualNode.InvertColor(pen.Color);
      }
      if (pen != null && (double) properBounds.Width != 0.0 && (double) properBounds.Height != 0.0)
        context.Graphics.DrawArc(pen, properBounds, 225f, 90f);
    }
    if (context.Layer == 0 || context.Layer == -1 && rectangleBorder.Right.Style == BorderStyles.None)
    {
      Pen pen = (Pen) null;
      if (flag2)
      {
        if (rectangleBorder.Right.Style == BorderStyles.None)
          pen = flag1 ? new Pen(VisualNode.InvertColor(RectangleElement.InvisibleLineColor), PageElementNode.DefaultLineWidth) : new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth);
      }
      else
      {
        pen = rectangleBorder.Right.GetPen();
        if (flag1 && pen != null)
          pen.Color = VisualNode.InvertColor(pen.Color);
      }
      if (pen != null && (double) properBounds.Width != 0.0 && (double) properBounds.Height != 0.0)
        context.Graphics.DrawArc(pen, properBounds, 315f, 90f);
    }
    if (context.Layer == 0 || context.Layer == -1 && rectangleBorder.Bottom.Style == BorderStyles.None)
    {
      Pen pen = (Pen) null;
      if (flag2)
      {
        if (rectangleBorder.Bottom.Style == BorderStyles.None)
          pen = flag1 ? new Pen(VisualNode.InvertColor(RectangleElement.InvisibleLineColor), PageElementNode.DefaultLineWidth) : new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth);
      }
      else
      {
        pen = rectangleBorder.Bottom.GetPen();
        if (flag1 && pen != null)
          pen.Color = VisualNode.InvertColor(pen.Color);
      }
      if (pen != null && (double) properBounds.Width != 0.0 && (double) properBounds.Height != 0.0)
        context.Graphics.DrawArc(pen, properBounds, 45f, 90f);
    }
    if (context.Layer == 0 || context.Layer == -1 && rectangleBorder.Left.Style == BorderStyles.None)
    {
      Pen pen = (Pen) null;
      if (flag2)
      {
        if (rectangleBorder.Left.Style == BorderStyles.None)
          pen = flag1 ? new Pen(VisualNode.InvertColor(RectangleElement.InvisibleLineColor), PageElementNode.DefaultLineWidth) : new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth);
      }
      else
      {
        pen = rectangleBorder.Left.GetPen();
        if (flag1 && pen != null)
          pen.Color = VisualNode.InvertColor(pen.Color);
      }
      if (pen != null && (double) properBounds.Width != 0.0 && (double) properBounds.Height != 0.0)
        context.Graphics.DrawArc(pen, properBounds, 135f, 90f);
    }
    if (!context.PixelMode)
      return;
    context.Graphics.PageUnit = pageUnit;
  }

  /// <summary>Нарисовать пропущенное пространство (строки/столбцы)</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  protected virtual void DrawSkipedSpace(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      return;
    bool withoutData = context.WithoutData;
    context.WithoutData = true;
    bool isSkipedSpace = context.IsSkipedSpace;
    context.IsSkipedSpace = true;
    context.FirstChildLevel = true;
    float skipedSpaceSize = context.SkipedSpaceSize;
    try
    {
      float num1 = this.skipCellsBefore;
      if (this.IgnoreSkipBefore() || !this.IsFirstInFlow)
        num1 = 0.0f;
      float num2 = this.skipCellsAfter;
      if (this.IgnoreSkipAfter() || !this.IsLastInFlow)
        num2 = 0.0f;
      if (parentCell == null || (double) num1 < 1.0 && (double) num2 < 1.0)
        return;
      GraphicsState gstate1 = context.Graphics.Save();
      Matrix transform = context.Graphics.Transform;
      float oneSkipSize = this.OneSkipSize;
      RectangleF clipRectangle = context.ClipRectangle;
      context.SkipedSpaceSize = oneSkipSize;
      for (int index = 0; (double) index < (double) num1; ++index)
      {
        context.ClipRectangle.Y += oneSkipSize;
        if (parentCell.IsColumn)
          context.Graphics.TranslateTransform(0.0f, -oneSkipSize);
        else
          context.Graphics.TranslateTransform(-oneSkipSize, 0.0f);
        this.DrawCell(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
      }
      context.Graphics.Transform = transform;
      context.ClipRectangle = clipRectangle;
      context.Graphics.Restore(gstate1);
      GraphicsState gstate2 = context.Graphics.Save();
      float bottom = this.properBounds.Bottom;
      for (int index = 0; (double) index < (double) num2; ++index)
      {
        context.ClipRectangle.Y -= oneSkipSize;
        bottom += oneSkipSize;
        if ((double) bottom <= (double) this.bounds.Bottom)
        {
          if (index == 0)
          {
            if (parentCell.IsColumn && (double) this.properBounds.Height - (double) oneSkipSize > 0.0)
              context.Graphics.TranslateTransform(0.0f, this.properBounds.Height - oneSkipSize);
            else if (!parentCell.IsColumn && (double) this.properBounds.Width - (double) oneSkipSize > 0.0)
              context.Graphics.TranslateTransform(this.properBounds.Width - oneSkipSize, 0.0f);
          }
          if (parentCell.IsColumn)
            context.Graphics.TranslateTransform(0.0f, oneSkipSize);
          else
            context.Graphics.TranslateTransform(oneSkipSize, 0.0f);
          this.DrawCell(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
        }
        else
          break;
      }
      context.ClipRectangle = clipRectangle;
      context.Graphics.Transform = transform;
      context.Graphics.Restore(gstate2);
    }
    finally
    {
      context.WithoutData = withoutData;
      context.IsSkipedSpace = isSkipedSpace;
      context.SkipedSpaceSize = skipedSpaceSize;
    }
  }

  /// <summary>Найти элемент страницы под данной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой</param>
  /// <param name="firstOnly">Найти первый попавшийся элемент</param>
  public override VisualNode FindPageElementAtPoint(PointF point, ref int layer, bool firstOnly)
  {
    VisualNode pageElementAtPoint = (VisualNode) null;
    if (this.IsVirtualNode)
      return base.FindPageElementAtPoint(point, ref layer, firstOnly);
    if (!this.IsVisibleNow)
      return (VisualNode) null;
    if (this.Bounds.Contains(point))
    {
      if (!firstOnly && this.nodes != null)
      {
        for (int index = this.nodes.Count - 1; index > -1; --index)
        {
          if (this.nodes[index] is VisualNode visualNode)
            visualNode = visualNode.FindPageElementAtPoint(point, ref layer, firstOnly);
          if (visualNode != null)
            pageElementAtPoint = visualNode;
        }
      }
      if (layer < 0)
      {
        layer = 0;
        pageElementAtPoint = (VisualNode) this;
      }
    }
    return pageElementAtPoint;
  }

  /// <summary>Получить элементы страницы в заданном прямоугольнике</summary>
  /// <param name="rect">Прямоугольник</param>
  /// <param name="elements">Возвращает элементы</param>
  /// <param name="containsOnly">Выбирать только те элементы, которые полностью попадают в прямоугольник</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public override void FindPageElementsInRectangle(
    RectangleF rect,
    List<VisualNode> elements,
    bool containsOnly,
    bool childOnly = false)
  {
    if (elements == null)
      throw new ArgumentNullException(nameof (elements));
    if (!this.IsVisibleNow)
      return;
    if (!childOnly)
    {
      if (containsOnly)
      {
        if (rect.Contains(this.Bounds))
        {
          elements.Add((VisualNode) this);
          return;
        }
      }
      else if (rect.IntersectsWith(this.Bounds))
      {
        elements.Add((VisualNode) this);
        return;
      }
    }
    base.FindPageElementsInRectangle(rect, elements, containsOnly);
  }

  /// <summary>Определить занимаемый размер для AutoSize родителя</summary>
  /// <param name="currSize">Текущий размер (начальное значение 0)</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public override SizeF FindMinSize(SizeF currSize, bool childOnly = false)
  {
    if (!childOnly)
    {
      RectangleF bounds1 = this.Bounds;
      RectangleF bounds2;
      if (this.HorzAlign == ElementHorizontalAlign.Right || this.HorzAlign == ElementHorizontalAlign.Center)
      {
        if ((double) currSize.Width < (double) bounds1.Width)
          currSize.Width = bounds1.Width;
      }
      else
      {
        double num1;
        if (this.ParentCell != null)
        {
          bounds2 = this.ParentCell.Bounds;
          num1 = (double) bounds2.X;
        }
        else
          num1 = 0.0;
        float num2 = (float) num1;
        float num3 = bounds1.Right - num2;
        if ((double) currSize.Width < (double) num3)
          currSize.Width = num3;
      }
      if (this.VertAlign == ElementVerticalAlign.Bottom || this.VertAlign == ElementVerticalAlign.Center)
      {
        if ((double) currSize.Height < (double) bounds1.Height)
          currSize.Height = bounds1.Height;
      }
      else if ((double) currSize.Height < (double) bounds1.Bottom)
      {
        if (this.ParentCell != null)
        {
          bounds2 = this.ParentCell.Bounds;
          0.0 = (double) bounds2.Y;
        }
        double right = (double) bounds1.Right;
        currSize.Height = bounds1.Bottom;
      }
    }
    return base.FindMinSize(currSize);
  }

  /// <summary>Погрешность округления кратной высоты текста</summary>
  [Browsable(false)]
  protected float FixedRowSizeTrancateFraction
  {
    get => this.OwnerDocument != null ? this.OwnerDocument.FixedRowSizeTrancateFraction : 0.2f;
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
    if (!(template is RectangleElement rectangleElement))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) template.Id, (object) this.id));
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
      if (!this.IsOverridden(OverrideFlags.MaxHeight))
        this.maxHeight = rectangleElement.maxHeight;
      if ((this.overrideFlags & OverrideFlags.MinHeight) == OverrideFlags.None)
        this.minHeight = rectangleElement.minHeight;
      if ((this.overrideFlags & OverrideFlags.MinWidth) == OverrideFlags.None)
        this.minWidth = rectangleElement.minWidth;
      if ((this.overrideFlags & OverrideFlags.KeepWithNext) == OverrideFlags.None)
        this.keepWithNext = rectangleElement.keepWithNext;
      this.horzAlign = rectangleElement.horzAlign;
      this.vertAlign = rectangleElement.vertAlign;
      if ((this.overrideFlags3 & OverrideFlags3.RelativeWidth) == OverrideFlags3.None)
        this.relativeWidth = rectangleElement.relativeWidth;
      if ((this.overrideFlags3 & OverrideFlags3.RelativeHeight) == OverrideFlags3.None)
        this.relativeHeight = rectangleElement.relativeHeight;
      this.cellMargins = rectangleElement.cellMargins;
      this.borderWidth = rectangleElement.BorderWidth;
      if ((this.overrideFlags & OverrideFlags.DefaultRowSize) == OverrideFlags.None && (this.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) != OverrideFlags2.None)
        this.defaultRowSize = rectangleElement.defaultRowSize;
      if ((this.overrideFlags & OverrideFlags.SkipBefore) == OverrideFlags.None)
        this.skipCellsBefore = rectangleElement.skipCellsBefore;
      if ((this.overrideFlags & OverrideFlags.SkipAfter) == OverrideFlags.None)
        this.skipCellsAfter = rectangleElement.skipCellsAfter;
      if ((this.overrideFlags3 & OverrideFlags3.IgnoreSkipOuterCells) == OverrideFlags3.None)
        this.ignoreSkipOuterCells = rectangleElement.ignoreSkipOuterCells;
      if (!this.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage) && rectangleElement.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage))
        this.AssignNonSkipBeforeAtStartPage(rectangleElement.NonSkipBeforeAtStartPage, false);
      if (!this.IsOverridden3(OverrideFlags3.Visible) && !rectangleElement.IsDataNode)
        this.SetVisible(rectangleElement.Visible, false, true, false, false, false);
      if ((this.overrideFlags & OverrideFlags.FromNewPage) == OverrideFlags.None)
        this.fromNewPage = rectangleElement.fromNewPage;
      RectangleF properBounds = this.properBounds;
      TableData parentCell = this.ParentCell;
      if (parentCell != null)
      {
        SizeF properSize = rectangleElement.ProperSize;
        if ((this.overrideFlags & OverrideFlags.Width) == OverrideFlags.None && (this.overrideFlags2 & OverrideFlags2.ColumnWidth) != OverrideFlags2.None || (double) properBounds.Width == (double) RectangleElement.EmptyFloatValue)
          properBounds.Width = properSize.Width;
        if (parentCell.IsColumn && (!this.IsOverridden(OverrideFlags.Height) || this.GeometryChangingBlocked_ForUser) && this.IsOverridden2(OverrideFlags2.RowHeight) || (double) properBounds.Height == (double) RectangleElement.EmptyFloatValue)
          properBounds.Height = properSize.Height;
        if ((double) this.bounds.X == (double) RectangleElement.EmptyFloatValue)
          this.setBounds(BoundsHelper.SetX(this.bounds, 0.0f));
        if ((double) this.bounds.Y == (double) RectangleElement.EmptyFloatValue)
          this.setBounds(BoundsHelper.SetY(this.bounds, 0.0f));
        if (parentCell.IsFixedStructureArea)
        {
          if ((double) properBounds.X == (double) RectangleElement.EmptyFloatValue)
            properBounds.X = rectangleElement.properBounds.X;
          if ((double) properBounds.Y == (double) RectangleElement.EmptyFloatValue)
            properBounds.Y = rectangleElement.properBounds.Y;
        }
      }
      else
      {
        if (!this.IsOverridden(OverrideFlags.Geometry))
        {
          this.setBounds(BoundsHelper.SetLocation(this.bounds, rectangleElement.bounds.Location));
        }
        else
        {
          if ((double) this.bounds.X == (double) RectangleElement.EmptyFloatValue)
            this.setBounds(BoundsHelper.SetX(this.bounds, rectangleElement.bounds.X));
          if ((double) this.bounds.Y == (double) RectangleElement.EmptyFloatValue)
            this.setBounds(BoundsHelper.SetY(this.bounds, rectangleElement.bounds.Y));
        }
        SizeF properSize = rectangleElement.ProperSize;
        if (!this.IsOverridden(OverrideFlags.Width) || (double) properBounds.Width == (double) RectangleElement.EmptyFloatValue)
          properBounds.Width = properSize.Width;
        if ((!this.IsOverridden(OverrideFlags.Height) || (double) properBounds.Height == (double) RectangleElement.EmptyFloatValue) && (!(this is TableData tableData) || tableData.NodesCount == 0 || tableData.IsPageFlow && tableData.IsTopLevelTable))
          properBounds.Height = properSize.Height;
      }
      if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore())
      {
        PointF pointF = this.CalcProperLocation(this.bounds.Location);
        if (this is TableData tableData)
        {
          if (isLoading && this.nodes != null)
          {
            tableData.RecalcCellLocations(this.bounds.Location, 0, this.nodes.Count, false, false, false);
          }
          else
          {
            properBounds.Location = pointF;
            this.SetNeedUpdateLayoutFlag(true, true, false, false);
          }
        }
        else
          properBounds.Location = pointF;
      }
      else
        properBounds.Location = parentCell == null || !parentCell.IsFixedStructureArea ? this.bounds.Location : rectangleElement.properBounds.Location;
      this.setProperBounds(properBounds);
      if ((double) this.skipCellsBefore != 0.0 && !this.IgnoreSkipBefore() || (double) this.skipCellsAfter != 0.0 && !this.IgnoreSkipAfter())
        this.setBounds(BoundsHelper.SetSize(this.bounds, this.CalcSizeFromProper(this.properBounds.Size)));
      else
        this.setBounds(BoundsHelper.SetSize(this.bounds, this.properBounds.Size));
      CustomBorder customBorder = (CustomBorder) null;
      if ((this.overrideFlags & (OverrideFlags.TopBorder | OverrideFlags.LeftBorder | this.overrideFlags & OverrideFlags.RightBorder | OverrideFlags.BottomBorder)) == OverrideFlags.None || (this.overrideFlags3 & OverrideFlags3.InnerHorizontalLine) == OverrideFlags3.None)
      {
        if (((this.overrideFlags & OverrideFlags.TopBorder) == OverrideFlags.None || this.borders == null || this.borders.Top == null) && rectangleElement.borders != null)
        {
          customBorder = new CustomBorder();
          customBorder.Top = rectangleElement.borders.Top;
        }
        if (((this.overrideFlags & OverrideFlags.BottomBorder) == OverrideFlags.None || this.borders == null || this.borders.Bottom == null) && rectangleElement.borders != null)
        {
          if (customBorder == null)
            customBorder = new CustomBorder();
          customBorder.Bottom = rectangleElement.borders.Bottom;
        }
        if (((this.overrideFlags3 & OverrideFlags3.InnerHorizontalLine) == OverrideFlags3.None && (this.overrideFlags2 & OverrideFlags2.ParentInnerHorizontalLine) != OverrideFlags2.None || this.ParentCell == null || this.borders == null || this.borders.InnerHorizontal == null) && rectangleElement.borders != null)
        {
          if (customBorder == null)
            customBorder = new CustomBorder();
          customBorder.InnerHorizontal = rectangleElement.borders.InnerHorizontal;
        }
        if (rectangleElement.borders != null && ((this.overrideFlags2 & OverrideFlags2.ColumnLeftBorder) != OverrideFlags2.None && (this.overrideFlags & OverrideFlags.LeftBorder) == OverrideFlags.None || this.borders == null || this.borders.Left == null))
        {
          if (customBorder == null)
            customBorder = new CustomBorder();
          customBorder.Left = rectangleElement.borders.Left;
        }
        if (rectangleElement.borders != null && ((this.overrideFlags2 & OverrideFlags2.ColumnRightBorder) != OverrideFlags2.None && (this.overrideFlags & OverrideFlags.RightBorder) == OverrideFlags.None || this.borders == null || this.borders.Right == null))
        {
          if (customBorder == null)
            customBorder = new CustomBorder();
          customBorder.Right = rectangleElement.borders.Right;
        }
      }
      if (customBorder != null)
        this.borders = (RectangleBorder) customBorder;
      if ((this.overrideFlags & OverrideFlags.BackColor) == OverrideFlags.None)
        this.backColor = rectangleElement.backColor;
      else if (this.backColor.IsEmpty)
        this.backColor = rectangleElement.backColor;
      if ((this.overrideFlags3 & OverrideFlags3.ForeColor) == OverrideFlags3.None)
        this.foreColor = rectangleElement.foreColor;
      else if (this.foreColor.IsEmpty)
        this.foreColor = rectangleElement.foreColor;
      this.headerShowType = rectangleElement.HeaderShowType;
      this.tableCellType = rectangleElement.TableCellType;
      this.tryNotBreak = rectangleElement.tryNotBreak;
      this.drawEllipse = rectangleElement.drawEllipse;
      if (rectangleElement.NextCell != null && rectangleElement.CanLinkWithLocalData(rectangleElement.NextCell))
      {
        if ((this.nextCell == null || this.nextCell.Template != rectangleElement.NextCell) && this.Page?.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) rectangleElement.NextCell) is RectangleElement templateRecursive)
          this.ChangeNextCell(templateRecursive);
      }
      else if (this.nextCell != null && this.nextCell.Page == this.Page)
        this.UniteTable();
      if (isLoading)
        return;
      this.SetNeedUpdateLayoutFlag(true, true, false, false);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateRefreshUI(updateUI, updateUI);
    }
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is RectangleElement;
  }

  /// <summary>Сбросить наследование параметров от родителей</summary>
  public override void ResetInheritance()
  {
    base.ResetInheritance();
    this.overrideFlags2 |= OverrideFlags2.ColumnWidth | OverrideFlags2.RowHeight | OverrideFlags2.ParentDefaultRowSize | OverrideFlags2.ColumnLeftBorder | OverrideFlags2.ColumnRightBorder | OverrideFlags2.ParentGrid | OverrideFlags2.ParentInnerHorizontalLine;
  }

  /// <summary>Свойство только для UI</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_609")]
  [CustomDescription("Attribute.Interfaces.Document_610")]
  [CustomCategory("Attribute.Interfaces.Document_112")]
  public string OverrideTemplateIdForUI
  {
    get => this.OverrideTemplateId;
    set => this.SetOverrideTemplateId(value, true);
  }

  [Browsable(false)]
  public string OverrideTemplateId
  {
    get
    {
      if (this.IsTemplate)
        return this._overrideTemplateId;
      return this.Template is RectangleElement template ? template.OverrideTemplateId : (string) null;
    }
  }

  public void SetOverrideTemplateId(string value, bool update)
  {
    if (!(this._overrideTemplateId != value) || !this.IsTemplate)
      return;
    this._overrideTemplateId = value;
    this.UpdateAfterChangeProperties(update, update, update, true, true, true);
  }

  /// <summary>Предыдущая ячейка</summary>
  [Category("Debug")]
  public RectangleElement PrevCell
  {
    [DebuggerStepThrough] get => this.prevCell;
  }

  /// <summary>Назначить новое значение свойству PrevCell</summary>
  /// <param name="value">Значение</param>
  public void SetPrevCell(RectangleElement value)
  {
    if (this.prevCell == value)
      return;
    RectangleElement prevCell = this.prevCell;
    this.prevCell = value;
    prevCell?.SetNextCell((RectangleElement) null);
    this.prevCell = value;
    if (this.prevCell == null)
      return;
    this.prevCell.SetNextCell(this);
  }

  /// <summary>Вставить следующий элемент потока в цепочку Prev/Next</summary>
  /// <param name="newNext">Новый следующий элемент цепочки</param>
  public void InsertNextCell(RectangleElement newNext)
  {
    if (newNext == null)
      throw new ArgumentNullException(nameof (newNext));
    if (newNext == this.nextCell)
      return;
    RectangleElement nextCell = this.nextCell;
    this.SetNextCellInternal(newNext);
    newNext.prevCell = this;
    RectangleElement lastCell = newNext.FindLastCell();
    lastCell.SetNextCellInternal(nextCell);
    if (nextCell == null)
      return;
    nextCell.prevCell = lastCell;
  }

  /// <summary>Вставить предыдущий элемент потока в цепочку Prev/Next</summary>
  /// <param name="newPrev">Новый предыдущий элемент цепочки</param>
  public void InsertPrevCell(RectangleElement newPrev)
  {
    if (newPrev == null)
      throw new ArgumentNullException(nameof (newPrev));
    if (newPrev == this.prevCell)
      return;
    RectangleElement prevCell = this.prevCell;
    this.prevCell = newPrev;
    newPrev.FindLastCell().SetNextCellInternal(this);
    newPrev.prevCell = prevCell;
    prevCell?.SetNextCellInternal(newPrev);
  }

  public override void MoveDataElementUp(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    if (this.ParentCell == null)
    {
      base.MoveDataElementUp(updateLayoutAndUI);
    }
    else
    {
      RectangleElement firstCell = this.FindFirstCell();
      RectangleElement prevDataCell = firstCell.FindPrevDataCell();
      if (prevDataCell == null)
        return;
      TableData parentCell = prevDataCell.ParentCell;
      int index = prevDataCell.Index;
      if (index < 0 || index > parentCell.Nodes.Count)
        return;
      parentCell.InsertChildNode(index, (DocumentTreeNode) firstCell, false, true, false, false, false);
      if (!updateLayoutAndUI)
        return;
      parentCell.UpdateLayout(true);
    }
  }

  public override void MoveDataElementDown(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    if (this.ParentCell == null)
    {
      base.MoveDataElementDown(updateLayoutAndUI);
    }
    else
    {
      RectangleElement lastCell = this.FindNextDataCell()?.FindLastCell();
      if (lastCell == null)
        return;
      RectangleElement firstCell = this.FindFirstCell();
      firstCell.UniteTable();
      TableData parentCell = lastCell.ParentCell;
      int index = parentCell != this.Parent ? lastCell.Index + 1 : lastCell.Index;
      if (index >= 0 && index <= parentCell.Nodes.Count)
        parentCell.InsertChildNode(index, (DocumentTreeNode) firstCell, false, true, false, false, false);
      if (!updateLayoutAndUI)
        return;
      parentCell.UpdateLayout(true);
    }
  }

  public override void MoveDataElementToBegin(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    if (this.ParentCell == null)
    {
      base.MoveDataElementToBegin(updateLayoutAndUI);
    }
    else
    {
      if (this.IsFirstCellInParentDataFlow)
        return;
      RectangleElement firstCell1 = this.FindFirstCell();
      TableData firstCell2 = firstCell1.ParentCell.FindFirstCell() as TableData;
      firstCell2.InsertChildNode(0, (DocumentTreeNode) firstCell1, false, true, false, false, false);
      if (!updateLayoutAndUI)
        return;
      firstCell2.UpdateLayout(true);
    }
  }

  public override void MoveDataElementToEnd(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    if (this.ParentCell == null)
    {
      base.MoveDataElementToEnd(updateLayoutAndUI);
    }
    else
    {
      if (this.IsLastCellInParentDataFlow)
        return;
      RectangleElement firstCell = this.FindFirstCell();
      TableData lastCell = firstCell.ParentCell.FindLastCell() as TableData;
      firstCell.UniteTable();
      int index = lastCell != firstCell.Parent ? lastCell.NodesCount : lastCell.NodesCount - 1;
      lastCell.InsertChildNode(index, (DocumentTreeNode) firstCell, false, true, false, false, false);
      if (!updateLayoutAndUI)
        return;
      lastCell.UpdateLayout(true);
    }
  }

  /// <summary>Ячейка продолжение</summary>
  [Category("Debug")]
  public RectangleElement NextCell
  {
    [DebuggerStepThrough] get => this.nextCell;
  }

  private void SetNextCellInternal(RectangleElement value)
  {
    if (this.nextCell == value)
      return;
    this.nextCell = value != this ? value : throw new ArgumentException($"Ошибка #10003! Попытка назначить следующим элементом для элемента саму элемент '{this.Id}'", nameof (value));
  }

  private void ValidateNextCellAndThowException(RectangleElement value)
  {
    if (value == null)
      return;
    if (!this.ValidateNextCellOrder(value))
      throw new ArgumentException($"Ошибка #10001! Попытка назначить следующим элемент '{value.Id}', находящийся до элемента '{this.Id}'", nameof (value));
    if (!this.ValidateNextCellParent(value))
      throw new ArgumentException($"Ошибка #10002! Попытка назначить следующим элемент '{value.Id}', являющийся родительским для '{this.Id}'", nameof (value));
  }

  protected virtual bool IsAllowableLocalDataLink() => this.ParentCell == null;

  protected bool CanLinkWithLocalData(RectangleElement rectangleElement)
  {
    return this.IsAllowableLocalDataLink() && rectangleElement.IsAllowableLocalDataLink() && this.Page == rectangleElement.Page && this.GetType() == rectangleElement.GetType();
  }

  /// <summary>Назначить новое значение свойству NextCell</summary>
  /// <param name="value">Значение</param>
  public void SetNextCell(RectangleElement value)
  {
    if (this.nextCell == value)
      return;
    RectangleElement nextCell = this.nextCell;
    if (this.nextCell != null)
      this.nextCell.SetPrevCell((RectangleElement) null);
    this.SetNextCellInternal(value);
    if (this.nextCell != null)
      this.nextCell.SetPrevCell(this);
    if (nextCell != null && value != null || (double) this.skipCellsAfter == 0.0 || this.ParentCell == null || this.ParentCell.IsFixedStructureArea)
      return;
    this.AssignBounds(this.Bounds with
    {
      Size = this.CalcSizeFromProper(this.properBounds.Size)
    }, false, false, false);
  }

  /// <summary>Заменить NextCell не теряя данных в потоке и сохраняя следующие за ним цепочки</summary>
  /// <param name="value">Значение</param>
  public void ChangeNextCell(RectangleElement value)
  {
    if (this.nextCell == value)
      return;
    if (this.nextCell != null && value != null && value.Page == this.Page && this.nextCell.Page != this.Page)
    {
      this.InsertNextCell(value);
    }
    else
    {
      RectangleElement nextCell1 = this.nextCell;
      RectangleElement nextCell2 = nextCell1?.NextCell;
      if (value != null)
        this.OneStepUniteTable();
      else
        this.UniteTable();
      if (this.nextCell == value)
        return;
      if (nextCell1 != null)
      {
        nextCell1.SetPrevCell((RectangleElement) null);
        if (value != null)
          nextCell1.SetNextCell((RectangleElement) null);
      }
      this.SetNextCellInternal(value);
      if (this.nextCell != null)
      {
        this.nextCell.SetPrevCell(this);
        this.nextCell.SetNextCell(nextCell2);
      }
      if (nextCell1 != null && value != null || (double) this.skipCellsAfter == 0.0 || this.ParentCell == null || this.ParentCell.IsFixedStructureArea)
        return;
      this.AssignBounds(this.Bounds with
      {
        Size = this.CalcSizeFromProper(this.properBounds.Size)
      }, false, false, false);
    }
  }

  private bool ValidateNextCellParent(RectangleElement nextCellNewValue)
  {
    if (nextCellNewValue?.Parent == null)
      return true;
    for (TableData parentCell = this.ParentCell; parentCell != null; parentCell = parentCell.ParentCell)
    {
      if (parentCell == nextCellNewValue)
        return false;
    }
    return true;
  }

  private bool ValidateNextCellOrder(RectangleElement nextCellNewValue)
  {
    return nextCellNewValue.Parent == null || (this.ParentCell == null || this.Parent != nextCellNewValue.Parent || nextCellNewValue.Index >= this.Index) && (this.Page == null || nextCellNewValue.Page == null || nextCellNewValue.Page.Index == -1 || this.Page == nextCellNewValue.Page || nextCellNewValue.Page.Index >= this.Page.Index);
  }

  /// <summary>Ячейка является первой в потоке</summary>
  [Browsable(false)]
  public virtual bool IsFirstInFlow
  {
    [DebuggerStepThrough] get => this.prevCell == null;
  }

  /// <summary>Ячейка является последней в потоке</summary>
  [Browsable(false)]
  public virtual bool IsLastInFlow
  {
    [DebuggerStepThrough] get => this.nextCell == null;
  }

  /// <summary>Ячейка является первой в потоке данных родительской таблицы</summary>
  [Browsable(false)]
  public bool IsFirstDataInParentDataFlow
  {
    get
    {
      if (this.IsHeaderCell || !(this.ParentCell?.FindFirstCell() is TableData firstCell))
        return true;
      TableData dataOwner;
      int dataPositionInFlow = firstCell.FindDataPositionInFlow(0, out dataOwner);
      return this == dataOwner.Nodes[dataPositionInFlow];
    }
  }

  /// <summary>Ячейка является первой в потоке данных родительской таблицы, включая заголовки</summary>
  [Browsable(false)]
  public override bool IsFirstCellInParentDataFlow
  {
    get
    {
      if (this.ParentCell == null)
        return base.IsFirstCellInParentDataFlow;
      RectangleElement firstCell = this.FindFirstCell();
      return firstCell.ParentCell.FindFirstCell() == firstCell.Parent && firstCell.Index == 0;
    }
  }

  /// <summary>Ячейка является последней в потоке данных родительской таблицы, включая заголовки</summary>
  [Browsable(false)]
  public override bool IsLastCellInParentDataFlow
  {
    get
    {
      if (this.ParentCell == null)
        return base.IsLastCellInParentDataFlow;
      RectangleElement lastCell1 = this.FindLastCell();
      RectangleElement lastCell2 = lastCell1.ParentCell.FindLastCell();
      return lastCell1.Parent == lastCell2 && lastCell1.Index == lastCell2.NodesCount - 1;
    }
  }

  /// <summary>Найти предыдущий элемент данных
  /// Возвращает первую ячейку из предыдущей цепочки (распределённой по страницам через NextCell)</summary>
  public RectangleElement FindPrevDataCell()
  {
    return this.ParentCell?.FindPrevDataCellInFlow(this.Index)?.FindFirstCell();
  }

  /// <summary>Найти следующий элемент данных
  /// Возвращает первую ячейку из следующей цепочки (распределённой по страницам через NextCell)</summary>
  /// <returns></returns>
  public RectangleElement FindNextDataCell() => this.ParentCell?.FindNextDataCellInFlow(this.Index);

  /// <summary>Найти первую ячейку в цепочке разбитых ячеек</summary>
  public RectangleElement FindFirstCell()
  {
    RectangleElement firstCell;
    for (firstCell = this; firstCell.prevCell != null; firstCell = firstCell.prevCell)
    {
      if (firstCell.prevCell == this)
      {
        LogManager.AddLine("RectangleElement.FindFirstCell(): prevCell loop!");
        break;
      }
    }
    return firstCell;
  }

  /// <summary>Найти последнюю ячейку в цепочке разбитых ячеек</summary>
  public RectangleElement FindLastCell()
  {
    RectangleElement lastCell;
    for (lastCell = this; lastCell.nextCell != null; lastCell = lastCell.nextCell)
    {
      if (lastCell.nextCell == this)
      {
        LogManager.AddLine("RectangleElement.FindLastCell(): nextCell loop!");
        break;
      }
    }
    return lastCell;
  }

  /// <summary>Для внутреннего использования. Необходимо обновить минимальный размер</summary>
  [Browsable(false)]
  public virtual bool NeedUpdateMinHeight
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Для внутреннего использования. Необходимо обновить минимальный размер</summary>
  [Browsable(false)]
  public virtual bool NeedUpdateMinWidth
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Необходим второй проход разбивки по таблице.
  /// Второй проход используется для параллельных таблиц элементы которых должны перемещаться по страницам синхронно.
  /// Как в экспортной СП</summary>
  [Browsable(false)]
  internal bool NeedSecondLayoutPass
  {
    get => this.HasCellFlags(CellFlags.NeedSecondLayoutPass);
    set
    {
      if (value)
      {
        this.SetCellFlags(CellFlags.NeedSecondLayoutPass);
        if (this.ParentCell == null)
          return;
        this.ParentCell.NeedSecondLayoutPass = true;
      }
      else
        this.ResetCellFlags(CellFlags.NeedSecondLayoutPass);
    }
  }

  /// <summary>Результат попытки оставить ячейку целой в родной (первой) таблице</summary>
  [Category("Debug")]
  public bool TryNotBreak_Failed0
  {
    get => this.HasCellFlags(CellFlags.TryNotBreak_Failed0);
    set
    {
      if (value)
        this.SetCellFlags(CellFlags.TryNotBreak_Failed0);
      else
        this.ResetCellFlags(CellFlags.TryNotBreak_Failed0);
    }
  }

  /// <summary>Попытка оставить ячейку целой в следующей (второй) таблице</summary>
  [Category("Debug")]
  public bool TryNotBreak_Failed1
  {
    get => this.HasCellFlags(CellFlags.TryNotBreak_Failed1);
    set
    {
      if (value)
        this.SetCellFlags(CellFlags.TryNotBreak_Failed1);
      else
        this.ResetCellFlags(CellFlags.TryNotBreak_Failed1);
    }
  }

  [Category("Debug")]
  public bool NeedUpdateFormulas
  {
    get => this.HasCellFlags(CellFlags.NeedUpdateFormulas);
    set => this.SetCellFlags(CellFlags.NeedUpdateFormulas, value);
  }

  /// <summary>С новой страницы</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_528")]
  [CustomDescription("Attribute.Interfaces.Document_529")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool FromNewPage
  {
    get
    {
      TableData parentCell = this.ParentCell;
      return parentCell == null || parentCell.IsColumn ? this.fromNewPage : parentCell.FromNewPage;
    }
    set => this.SetFromNewPage(value, true, true);
  }

  /// <summary>Назначить значение свойству FromNewPage</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetFromNewPage(bool value, bool updateUI, bool updateLayout)
  {
    if (this.FromNewPage == value || this.IsDynamicGroupHeader)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "FromNewPage", (object) this.FromNewPage, (object) value);
    TableData parentCell = this.ParentCell;
    if (parentCell == null || parentCell.IsColumn)
    {
      this.fromNewPage = value;
      this.overrideFlags |= OverrideFlags.FromNewPage;
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
    }
    else
      parentCell.SetFromNewPage(value, updateUI, updateLayout);
  }

  /// <summary>Не разбивать по страницам</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_530")]
  [CustomDescription("Attribute.Interfaces.Document_531")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool TryNotBreak
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell == null || parentCell.IsColumn ? this.tryNotBreak : parentCell.TryNotBreak;
    }
    set => this.SetTryNotBreak(value, true, true);
  }

  /// <summary>Назначить значение свойству TryNotBreak</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetTryNotBreak(bool value, bool updateUI, bool updateLayout)
  {
    if (this.TryNotBreak == value)
      return;
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "TryNotBreak", (object) this.TryNotBreak, (object) value);
    if (parentCell == null || parentCell.IsColumn)
    {
      this.tryNotBreak = value;
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
    }
    else
      parentCell.SetTryNotBreak(value, updateUI, updateLayout);
  }

  /// <summary>Не разбивать по страницам</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_534")]
  [CustomDescription("Attribute.Interfaces.Document_535")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool KeepWithNext
  {
    get
    {
      TableData parentCell = this.ParentCell;
      if (parentCell == null)
        return false;
      if (!parentCell.IsColumn)
        return parentCell.KeepWithNext;
      if ((this.overrideFlags & OverrideFlags.KeepWithNext) == OverrideFlags.None)
      {
        RectangleElement template = (RectangleElement) this.Template;
        if (template != null)
          return template.keepWithNext;
      }
      return this.keepWithNext;
    }
    set
    {
      TableData parentCell = this.ParentCell;
      if (parentCell == null)
        return;
      if (parentCell.IsColumn)
        this.SetKeepWithNext(value, true, true);
      else
        parentCell.KeepWithNext = value;
    }
  }

  /// <summary>Назначить значение свойству KeepWithNext</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetKeepWithNext(bool value, bool updateUI, bool updateLayout)
  {
    if (this.keepWithNext == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "KeepWithNext", (object) this.KeepWithNext, (object) value);
    this.keepWithNext = value;
    this.overrideFlags |= OverrideFlags.KeepWithNext;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Подогнать высоту строки под кратный размер сетки</summary>
  /// <param name="rowSize">Высота строки</param>
  /// <param name="lineSize">Размер строки сетки</param>
  /// <returns>Размер кратный размеру строки сетки</returns>
  public float RoundForFixedSizeRow(float rowSize, float lineSize, float minSize)
  {
    float d = rowSize / lineSize;
    float single = Convert.ToSingle(Math.Truncate((double) d));
    return ((double) d - (double) single > (double) this.FixedRowSizeTrancateFraction || (double) single * (double) lineSize < (double) minSize || (double) single <= 0.0 ? single + 1f : single) * lineSize;
  }

  /// <summary>Таблица распределяется в текущий момент</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool IsDistributing
  {
    [DebuggerStepThrough] get
    {
      TableData topLevelTable = this.TopLevelTable;
      return topLevelTable != null && topLevelTable.IsDistributing;
    }
  }

  /// <summary>Распределить данные по ячейке представления</summary>
  /// <param name="context">Контекст разбивки</param>
  public virtual void DistributeCell(DistributeContext context)
  {
    context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
    context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
    context.TryNotBreak |= this.tryNotBreak;
    if ((double) this.MinHeight > 0.0)
      context.NewSize.Height = this.MinHeight;
    if (context.IsFixedSizeRow_NN)
      context.NewSize.Height = this.RoundForFixedSizeRow(context.NewSize.Height, context.RowSize_NN, this.MinHeight);
    this.AssignBounds(this.Location, context.NewSize, false, false, false);
    context.VertDistributed = DistributeResult.All;
    TableData parentCell = this.ParentCell;
    SizeF size = this.Size;
    if ((double) size.Height > (double) context.MaxSize.Height || (double) size.Width > (double) context.MaxSize.Width)
      context.VertDistributed = DistributeResult.None;
    if (context.FirstDataOnPage && context.VertDistributed == DistributeResult.None)
      context.VertDistributed = DistributeResult.All;
    this.AssignNeedUpdateLayoutFlag(context.DistributeResultIsNeedUpdateLayout);
  }

  internal void ResetTryNotBreadFailedFlagsRecursive()
  {
    this.TryNotBreak_Failed0 = false;
    this.TryNotBreak_Failed1 = false;
    if (this.nodes.IsEmpty<DocumentTreeNode>())
      return;
    foreach (RectangleElement rectangleElement in this.nodes.OfType<RectangleElement>())
      rectangleElement.ResetTryNotBreadFailedFlagsRecursive();
  }

  /// <summary>Получить свободное пространство для распределения данных в таблице</summary>
  public virtual float GetTableFreeSpace()
  {
    return this.TopLevelTable != null && this.TopLevelTable != this ? this.TopLevelTable.GetTableFreeSpace() : 0.0f;
  }

  /// <summary>Только для внутреннего использования. Получить минимальный неделимый размер для разбивки</summary>
  /// <note>Используется для определения свободного пространства в только что созданной для переноса таблице</note>
  public virtual float GetMinimalSizeForDistribute(DistributeContext context)
  {
    float sizeForDistribute = this.bounds.Height;
    if ((double) this.MinHeight > 0.0)
      sizeForDistribute = this.MinHeight;
    if ((double) sizeForDistribute > (double) context.MaxSize.Height || !context.IsFixedSizeRow_NN)
      return sizeForDistribute;
    if ((double) context.RowSize_NN > (double) context.MaxSize.Height)
      return context.RowSize_NN;
    double num1 = Math.Ceiling((double) sizeForDistribute / (double) context.RowSize_NN);
    float? nullable1 = context.RowSize;
    double? nullable2 = nullable1.HasValue ? new double?((double) nullable1.GetValueOrDefault()) : new double?();
    float num2 = (float) (nullable2.HasValue ? new double?(num1 * nullable2.GetValueOrDefault()) : new double?()).Value;
    double num3 = (double) num2 - (double) sizeForDistribute;
    float? rowSize1 = context.RowSize;
    float trancateFraction = this.FixedRowSizeTrancateFraction;
    nullable1 = rowSize1.HasValue ? new float?(rowSize1.GetValueOrDefault() * trancateFraction) : new float?();
    double valueOrDefault = (double) nullable1.GetValueOrDefault();
    if (num3 > valueOrDefault & nullable1.HasValue)
    {
      float num4 = num2;
      float? rowSize2 = context.RowSize;
      nullable1 = rowSize2.HasValue ? new float?(num4 - rowSize2.GetValueOrDefault()) : new float?();
      float minHeight = this.MinHeight;
      if ((double) nullable1.GetValueOrDefault() > (double) minHeight & nullable1.HasValue)
        num2 -= context.RowSize_NN;
    }
    sizeForDistribute = num2;
    return sizeForDistribute;
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя, после обновления разбивки</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout)
  {
    if (!(updateLayout & value) && this.needUpdateLayoutFlag == value && (!value || this.OwnerDocument == null || this.OwnerDocument.NeedUpdateLayoutFlag))
      return;
    TableData parentCell = this.ParentCell;
    if (value && parentCell != null)
    {
      this.AssignNeedUpdateLayoutFlag(value);
      if (setInPrevCell && this.onOnePageWith != null && this.onOnePageWith.ParentCell != null)
        this.onOnePageWith.ParentCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, false, false);
      parentCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
    }
    else if (value && this.page != null && (this.HorzAlign != ElementHorizontalAlign.None || this.VertAlign != ElementVerticalAlign.None))
    {
      this.AssignNeedUpdateLayoutFlag(value);
      this.page.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
    }
    else
      base.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout && !this.SuspendedUpdateLayoutFlag);
  }

  internal virtual void ResetDistributeState()
  {
    if (this.TryNotBreak_Failed0)
      this.TryNotBreak_Failed0 = false;
    if (this.TryNotBreak_Failed1)
      this.TryNotBreak_Failed1 = false;
    if (!this.NeedSecondLayoutPass)
      return;
    this.NeedSecondLayoutPass = false;
  }

  /// <summary>Обновить разбивку по страницам.
  /// Вызывает UpdateLayout для вышестоящих узлов или Distribute для себя.
  /// Вызов UpdateLayout для дочерних узлов недопустим!</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI)
  {
    if (this.IsVirtualNode || this.SuspendedUpdateLayoutFlag)
      return;
    if (this.needUpdateLayoutFlag)
    {
      TableData parentCell = this.ParentCell;
      if (parentCell != null)
        parentCell.UpdateLayout(updateUI);
      else if (this.page != null && (this.HorzAlign != ElementHorizontalAlign.None || this.VertAlign != ElementVerticalAlign.None))
      {
        this.page.UpdateLayout(updateUI);
      }
      else
      {
        base.UpdateLayout(false);
        if (!updateUI)
          return;
        if (this.needUpdateUIGeometry)
          this.UpdateUIGeometry(true);
        else
          this.RefreshUI();
      }
    }
    else
    {
      if (!updateUI)
        return;
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
  }

  /// <summary>Обновление представлений данных временно заблокировано</summary>
  public override bool SuspendedUpdateLayoutFlag
  {
    [DebuggerStepThrough] get
    {
      if (base.SuspendedUpdateLayoutFlag)
        return true;
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsDistributing;
    }
  }

  /// <summary>Обновление изображения интерфейса пользователя заблокировано</summary>
  public override bool SuspendedRefreshUIFlag
  {
    [DebuggerStepThrough] get
    {
      if (base.SuspendedRefreshUIFlag)
        return true;
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsDistributing;
    }
  }

  /// <summary>Обновление геометрии интерфейса пользователя заблокировано</summary>
  public override bool SuspendedUpdateUIGeometryFlag
  {
    [DebuggerStepThrough] get
    {
      if (base.SuspendedUpdateUIGeometryFlag)
        return true;
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsDistributing;
    }
  }

  /// <summary>Тип ячейки таблицы</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_367")]
  [CustomDescription("Attribute.Interfaces.Document_368")]
  [CustomCategory("Attribute.Interfaces.Document_369")]
  public virtual CellType TableCellType
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsColumn ? this.tableCellType : CellType.DataCell;
    }
    set => this.SetTableCellType(value, true, true);
  }

  /// <summary>Последний заголовок</summary>
  /// <returns></returns>
  public bool IsLastHeader()
  {
    if (this.TableCellType != CellType.Header)
      return false;
    RectangleElement rectangleElement = (RectangleElement) null;
    if (this.Parent != null && this.Index < this.Parent.NodesCount - 1)
    {
      rectangleElement = this.Parent.Nodes[this.Index + 1] as RectangleElement;
    }
    else
    {
      TableData tableData = this.OwnerSubTable;
      if (tableData != null)
        tableData = tableData.NextTable;
      if (tableData != null && tableData.NodesCount > 0)
        rectangleElement = tableData.Nodes[0] as RectangleElement;
    }
    return rectangleElement == null || rectangleElement.TableCellType != CellType.Header;
  }

  /// <summary>Назначить значение свойству TableCellType</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetTableCellType(CellType value, bool updateUI, bool updateLayout)
  {
    if (this.TableCellType != value)
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "TableCellType", (object) this.TableCellType, (object) value);
      this.tableCellType = value;
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
    }
    else
      this.tableCellType = value;
  }

  /// <summary>Ячейка является шапкой</summary>
  internal virtual bool IsHeaderCell
  {
    [DebuggerStepThrough] get => this.TableCellType == CellType.Header;
  }

  /// <summary>Привязка к странице</summary>
  [Browsable(false)]
  public virtual int DesiredPageNumber
  {
    get => this.desiredPageNumber;
    set => this.SetDesiredPageNumber(value, true, true);
  }

  /// <summary>Назначить значение свойству DesiredPageNumber</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetDesiredPageNumber(int value, bool updateUI, bool updateLayout)
  {
    if (this.desiredPageNumber == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DesiredPageNumber", (object) this.desiredPageNumber, (object) value);
    this.desiredPageNumber = value;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  /// <param name="removeData">Удалить данные</param>
  public abstract void ConvertToHeader(bool removeData);

  /// <summary>Объединить распределенную таблицу в одну. Метод обратный DistributeTable</summary>
  public virtual void UniteTable()
  {
    if (this.nextCell == null)
      return;
    List<RectangleElement> rectangleElementList = new List<RectangleElement>();
    for (RectangleElement nextCell = this.nextCell; nextCell != null; nextCell = nextCell.nextCell)
      rectangleElementList.Add(nextCell);
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
    {
      if (parentCell.IsColumn)
      {
        for (int index = rectangleElementList.Count - 1; index >= 0; --index)
          rectangleElementList[index].Remove(false, false, false);
      }
      this.SetNextCell((RectangleElement) null);
    }
    this.SetNeedUpdateLayoutFlag(true, true, false, false);
  }

  /// <summary>Убрать следующую ячейку. Для внутреннего пользования</summary>
  internal virtual void OneStepUniteTable(bool dontUniteTopLevelTable = true)
  {
    if (this.nextCell == null)
      return;
    List<RectangleElement> rectangleElementList = new List<RectangleElement>();
    RectangleElement nextCell1 = this.nextCell;
    TableData parentCell = this.ParentCell;
    if (parentCell != null || !dontUniteTopLevelTable)
    {
      RectangleElement nextCell2 = this.nextCell.nextCell;
      if ((parentCell != null ? (parentCell.IsColumn ? 1 : 0) : 1) != 0)
        this.nextCell.Remove(false, false, false);
      this.SetNextCell(nextCell2);
    }
    this.SetNeedUpdateLayoutFlag(true, true, false, false);
  }

  /// <summary>Поток пустой</summary>
  /// <param name="allowEmptyCell">Допустимы пустые ячейки</param>
  /// <returns></returns>
  public virtual bool AllFlowsIsEmpty() => this.prevCell != null;

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
  public virtual bool IsEmptyData(bool emptyCellIsData, bool checkNextCell = true) => true;

  /// <summary>Можно ли распределять данные по страницам</summary>
  /// <returns></returns>
  public virtual bool CanSplitData() => false;

  /// <summary>Сгенерировать идентификатор для следующей ячейки</summary>
  /// <returns></returns>
  internal string GenerateIdForNextCell()
  {
    RectangleElement rectangleElement = this;
    int num = 1;
    while (rectangleElement.PrevCell != null)
    {
      rectangleElement = rectangleElement.PrevCell;
      ++num;
    }
    string id = rectangleElement.Id;
    int startIndex = id.LastIndexOf(".");
    string idForNextCell;
    if (startIndex == -1)
    {
      idForNextCell = $"{id}.{num.ToString((IFormatProvider) CultureInfo.InvariantCulture)}";
    }
    else
    {
      int result = 0;
      string str1 = id.Substring(0, startIndex + 1);
      if (startIndex < id.Length - 1 && !int.TryParse(id.Substring(startIndex), out result))
        result = 0;
      string str2 = (result + num).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      idForNextCell = str1 + str2;
    }
    return idForNextCell;
  }

  /// <summary>Установить битовые флаги ячеек таблиц</summary>
  /// <param name="flags">Флаги, которые нужно установить</param>
  /// <param name="flags">Значение битовых флагов, которое нужно установить</param>
  protected void SetCellFlags(CellFlags flags, bool value)
  {
    if (value)
      this.SetCellFlags(flags);
    else
      this.ResetCellFlags(flags);
  }

  /// <summary>Установить битовые флаги ячеек таблиц</summary>
  /// <param name="flags">Флаги, которые нужно установить</param>
  protected void SetCellFlags(CellFlags flags) => this.cellFlags |= flags;

  /// <summary>Сбросить битовые флаги ячеек таблиц</summary>
  /// <param name="flags">Флаги, которые нужно сбросить</param>
  protected void ResetCellFlags(CellFlags flags) => this.cellFlags &= ~flags;

  /// <summary>Проверить битовые флаги ячеек таблиц</summary>
  /// <param name="flags">Флаги, которые нужно проверить</param>
  /// <returns>Возвращает true, если все заданные флаги установлены</returns>
  protected bool HasCellFlags(CellFlags flags) => (this.cellFlags & flags) == flags;

  /// <summary>Текст заголовка группы, по которому группируются записи</summary>
  [Category("Debug")]
  public string GroupHeaderText
  {
    get => this.GetAttributeValue(nameof (GroupHeaderText), true);
    set => this.SetAttributeValue(nameof (GroupHeaderText), value, false, false, false);
  }

  /// <summary>Запись имеет заголовок для динамической группировки и может группироваться </summary>
  [Category("Debug")]
  public bool HasGroupHeaderText => !string.IsNullOrEmpty(this.GroupHeaderText);

  [Category("Debug")]
  public bool IsDynamicGroupHeader
  {
    get => this.GetAttributeValue("GroupHeader", true) == "1";
    set
    {
      if (this.IsDynamicGroupHeader == value)
        return;
      if (value)
        this.SetAttributeValue("GroupHeader", "1", false, false, false);
      else
        this.RemoveAttribute("GroupHeader", false, false);
    }
  }

  /// <summary>Текущее значение текста для вывода в записи, которая может быть сгруппирована</summary>
  [Category("Debug")]
  public string GroupCellText
  {
    get => this.GetAttributeValue("GroupHeaderCellText", true);
    set
    {
      if (!(this.GroupCellText != value))
        return;
      this.SetAttributeValue("GroupHeaderCellText", value, false, false, false);
    }
  }

  /// <summary>Значение текста для вывода в несгруппированной записи</summary>
  [Category("Debug")]
  public string GroupCellOriginalText
  {
    get => this.GetAttributeValue("GroupHeaderCellOriginalText", true);
    set => this.SetAttributeValue("GroupHeaderCellOriginalText", value, false, false, false);
  }

  /// <summary>Значение текста для вывода в сгруппированной записи</summary>
  [Category("Debug")]
  public string GroupCellTextForGroup
  {
    get => this.GetAttributeValue("GroupHeaderCellTextForGroup", true);
    set => this.SetAttributeValue("GroupHeaderCellTextForGroup", value, false, false, false);
  }

  /// <summary>Записать данные о геометрии в XML</summary>
  /// <param name="xw">XmlWriter</param>
  public virtual void WriteGeometryToXml(XmlWriter xw)
  {
    bool flag = this.Template != null;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea && this.properBounds.Location != RectangleElement.EmptyPointF && (!flag || this.TemplateGeometryOverrided))
      xw.WriteAttributeString("orgPos", new PointFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.properBounds.Location));
    else if (parentCell == null && this.bounds.Location != RectangleElement.EmptyPointF && (!flag || this.TemplateGeometryOverrided))
      xw.WriteAttributeString("pos", new PointFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.bounds.Location));
    if (!(this.properBounds.Size != RectangleElement.EmptySizeF))
      return;
    if (parentCell != null)
    {
      if (parentCell.IsFixedStructureArea && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue && (!flag || this.HeightOverrided && this.WidthOverrided))
      {
        xw.WriteAttributeString("size", new SizeFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.properBounds.Size));
      }
      else
      {
        if (parentCell.IsRow && (double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (this.IsOverridden(OverrideFlags.Width) || !flag && (this.IsOverridden2(OverrideFlags2.ColumnWidth) || this.gridPos != null && this.gridPos.SpanCount < 1 || parentCell.GridColumnsParams == null || parentCell.GridColumnsParams.Count == 0 || this.GetGridColumnIndex() == -1)))
          xw.WriteAttributeString("w", this.properBounds.Width.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (parentCell.IsColumn && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue && (this.IsOverridden(OverrideFlags.Height) || !flag && (this.IsOverridden2(OverrideFlags2.RowHeight) || (double) this.defaultRowSize == 0.0)))
        {
          xw.WriteAttributeString("h", this.properBounds.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        }
        else
        {
          if (((!(this is TableData tableData) ? 0 : (tableData.IsRow ? 1 : 0)) & (flag ? 1 : 0)) == 0)
            return;
          xw.WriteAttributeString("rh", this.properBounds.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        }
      }
    }
    else if ((double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue && (!flag || this.IsOverridden(OverrideFlags.Height) && this.IsOverridden(OverrideFlags.Width)))
    {
      xw.WriteAttributeString("size", new SizeFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.properBounds.Size));
    }
    else
    {
      if ((double) this.properBounds.Width != (double) RectangleElement.EmptyFloatValue && (!flag || this.IsOverridden(OverrideFlags.Width)))
        xw.WriteAttributeString("w", this.properBounds.Width.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if ((double) this.properBounds.Height != (double) RectangleElement.EmptyFloatValue && (!flag || this.IsOverridden(OverrideFlags.Height)))
      {
        xw.WriteAttributeString("h", this.properBounds.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      }
      else
      {
        if (((!(this is TableData tableData) ? 0 : (tableData.IsRow ? 1 : 0)) & (flag ? 1 : 0)) == 0)
          return;
        xw.WriteAttributeString("rh", this.properBounds.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      }
    }
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    bool isTableCell = this.IsTableCell;
    bool firstTime;
    long id;
    if (this.prevCell != null && !(this is TableData))
    {
      XmlWriter xmlWriter = xw;
      id = objectRefId.GetId((object) this.prevCell, out firstTime);
      string str = id.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("prevCell", str);
    }
    if (this.onOnePageWith != null)
    {
      XmlWriter xmlWriter = xw;
      id = objectRefId.GetId((object) this.onOnePageWith, out firstTime);
      string str = id.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("onePageW", str);
    }
    this.WriteGeometryToXml(xw);
    if ((double) this.minHeight != 0.0 | flag && (!flag || this.IsOverridden(OverrideFlags.MinHeight)))
      xw.WriteAttributeString("minHeight", this.minHeight.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((double) this.maxHeight != 0.0 | flag && (!flag || this.IsOverridden(OverrideFlags.MaxHeight)))
      xw.WriteAttributeString("maxHeight", this.maxHeight.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((double) this.minWidth != 0.0 | flag && (!flag || this.IsOverridden(OverrideFlags.MinWidth)))
      xw.WriteAttributeString("minWidth", this.minWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((double) this.borderWidth != 0.0)
      xw.WriteAttributeString("borderWidth", this.borderWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((!flag || (this.overrideFlags & OverrideFlags.BackColor) != OverrideFlags.None) && (flag || !this.backColor.IsEmpty && this.backColor != PageElementNode.DefaultBackColor))
      xw.WriteAttributeString("backColor", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.backColor));
    if ((!flag || (this.overrideFlags3 & OverrideFlags3.ForeColor) != OverrideFlags3.None) && (flag || !this.foreColor.IsEmpty && this.foreColor != PageElementNode.DefaultForeColor))
      xw.WriteAttributeString("foreColor", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.foreColor));
    if (!flag)
      xw.WriteAttributeString("cellType", ((int) this.tableCellType).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((!flag || (this.overrideFlags & OverrideFlags.FromNewPage) != OverrideFlags.None) && this.fromNewPage)
      xw.WriteAttributeString("fromNewPage", "1");
    if (!flag && this.tryNotBreak)
      xw.WriteAttributeString("tryNotBreak", "1");
    if (this.keepWithNext | flag && (!flag || (this.overrideFlags & OverrideFlags.KeepWithNext) != OverrideFlags.None))
      xw.WriteAttributeString("keepWithNext", this.keepWithNext ? "1" : "0");
    if (!flag && this.drawEllipse)
      xw.WriteAttributeString("drawEllipse", "1");
    if (!flag && this.headerShowType != HeaderShowType.All)
      xw.WriteAttributeString("headerType", this.headerShowType.ToString());
    if (isTableCell && (flag || (double) this.skipCellsBefore != 0.0 && (double) this.skipCellsBefore != (double) RectangleElement.EmptyFloatValue) && (!flag || (this.overrideFlags & OverrideFlags.SkipBefore) != OverrideFlags.None))
      xw.WriteAttributeString("skipBefore", this.skipCellsBefore.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (isTableCell && (flag || (double) this.skipCellsAfter != 0.0 && (double) this.skipCellsAfter != (double) RectangleElement.EmptyFloatValue) && (!flag || (this.overrideFlags & OverrideFlags.SkipAfter) != OverrideFlags.None))
      xw.WriteAttributeString("skipAfter", this.skipCellsAfter.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (isTableCell && (flag || this.ignoreSkipOuterCells) && (!flag || (this.overrideFlags3 & OverrideFlags3.IgnoreSkipOuterCells) != OverrideFlags3.None))
      xw.WriteAttributeString("ignoreSkipOuter", this.ignoreSkipOuterCells.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    GridIdPosition gridPos = this.gridPos as GridIdPosition;
    if (isTableCell && gridPos != null && gridPos.GridID != -1)
      xw.WriteAttributeString("gridID", gridPos.GridID.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (isTableCell && this.gridPos != null)
    {
      if (this.gridPos.SpanCount != 1)
        xw.WriteAttributeString("span", this.gridPos.SpanCount.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (this.gridPos.StartMerge)
        xw.WriteAttributeString("startMerge", "1");
      if (this.gridPos.MergeWithCell != null)
      {
        XmlWriter xmlWriter = xw;
        id = objectRefId.GetId((object) this.gridPos.MergeWithCell, out firstTime);
        string str = id.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        xmlWriter.WriteAttributeString("mergeCell", str);
      }
    }
    if (isTableCell && (this.overrideFlags2 & OverrideFlags2.ParentDefaultRowSize) != OverrideFlags2.None && (!flag || (this.overrideFlags & OverrideFlags.DefaultRowSize) != OverrideFlags.None) || !isTableCell && (!flag || (this.overrideFlags & OverrideFlags.DefaultRowSize) != OverrideFlags.None))
      xw.WriteAttributeString("rowSize", this.defaultRowSize.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag && this.horzAlign != ElementHorizontalAlign.None)
      xw.WriteAttributeString("horzAlign", ((int) this.horzAlign).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag && this.vertAlign != ElementVerticalAlign.None)
      xw.WriteAttributeString("vertAlign", ((int) this.vertAlign).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((!flag || this.IsOverridden3(OverrideFlags3.RelativeWidth)) && (double) this.relativeWidth != 0.0)
      xw.WriteAttributeString("relWidth", this.relativeWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((!flag || this.IsOverridden3(OverrideFlags3.RelativeHeight)) && (double) this.relativeHeight != 0.0)
      xw.WriteAttributeString("relHeight", this.relativeHeight.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag)
    {
      if ((double) this.cellMargins.X != 0.0)
        xw.WriteAttributeString("cellMarginLeft", this.cellMargins.X.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if ((double) this.cellMargins.Width != 0.0)
        xw.WriteAttributeString("cellMarginRight", this.cellMargins.Width.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if ((double) this.cellMargins.Y != 0.0)
        xw.WriteAttributeString("cellMarginTop", this.cellMargins.Y.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if ((double) this.cellMargins.Height != 0.0)
        xw.WriteAttributeString("cellMarginBottom", this.cellMargins.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
    if ((this.overrideFlags3 & OverrideFlags3.NonSkipBeforeAtStartPage) != OverrideFlags3.None)
      xw.WriteAttributeString("nSkipAtSPg", this.NonSkipBeforeAtStartPage ? "1" : "0");
    if (this.IsTemplate && !this.IsSelectedDataCellTemplate)
      xw.WriteAttributeString("isSelectedCell", "0");
    if (this.desiredPageNumber > 0)
      xw.WriteAttributeString("desiredPageNumber", this.desiredPageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!(this.IsTemplate & this.TopLevelTable != null) || string.IsNullOrEmpty(this._overrideTemplateId))
      return;
    xw.WriteAttributeString("overrideTemplateId", this._overrideTemplateId);
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.borders != null)
    {
      int num = this.Template != null ? 1 : 0;
      bool isTableCell = this.IsTableCell;
      CustomBorder element = new CustomBorder(this.borders.Top, this.borders.InnerHorizontal, this.borders.Bottom, this.borders.Left, this.borders.Right);
      if (num != 0 && (this.overrideFlags & OverrideFlags.TopBorder) == OverrideFlags.None)
        element.Top = (BorderLine) null;
      if (num != 0 && (this.overrideFlags & OverrideFlags.BottomBorder) == OverrideFlags.None)
        element.Bottom = (BorderLine) null;
      if (num != 0 && (this.overrideFlags3 & OverrideFlags3.InnerHorizontalLine) == OverrideFlags3.None || isTableCell && (this.overrideFlags2 & OverrideFlags2.ParentInnerHorizontalLine) == OverrideFlags2.None)
        element.InnerHorizontal = (BorderLine) null;
      if (num != 0 && (this.overrideFlags & OverrideFlags.LeftBorder) == OverrideFlags.None || isTableCell && (this.overrideFlags2 & OverrideFlags2.ColumnLeftBorder) == OverrideFlags2.None)
        element.Left = (BorderLine) null;
      if (num != 0 && (this.overrideFlags & OverrideFlags.RightBorder) == OverrideFlags.None || isTableCell && (this.overrideFlags2 & OverrideFlags2.ColumnRightBorder) == OverrideFlags2.None)
        element.Right = (BorderLine) null;
      if (!element.IsEmpty)
        WriteReadXmlHelper.WriteXmlElement("Borders", (IWriteReadXml) element, true, xw, objectRefId);
    }
    base.WriteXmlElements(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    string localName = readArgs.Reader.LocalName;
    if (RectangleElement.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      RectangleElement.ReadFieldsDict.TryGetValue(localName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (readArgs.Version < 10 && localName == "stdParamsIndex")
    {
      int gridID = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
      if (gridID != -1)
      {
        GridIdPosition gridIdPosition = new GridIdPosition(gridID);
        if (this.gridPos != null)
          gridIdPosition.SetCellSpan(this.gridPos.SpanCount);
        this.gridPos = (TableGridPosition) gridIdPosition;
        gridIdPosition.stdGridPosition = true;
      }
      return true;
    }
    switch (localName)
    {
      case "Borders":
        RectangleElement.ReadBorders((DocumentTreeNode) this, readArgs);
        return true;
      case "cellType":
      case "tableCellType":
        RectangleElement.ReadTableCellType((DocumentTreeNode) this, readArgs);
        return true;
      case "desiredPageNumber":
        RectangleElement.ReadDesiredPageNumber((DocumentTreeNode) this, readArgs);
        return true;
      case "drawEllipse":
        RectangleElement.ReadDrawEllipse((DocumentTreeNode) this, readArgs);
        return true;
      case "fromNewPage":
        RectangleElement.ReadFromNewPage((DocumentTreeNode) this, readArgs);
        return true;
      case "gridID":
        RectangleElement.ReadGridID((DocumentTreeNode) this, readArgs);
        return true;
      case "headerShowType":
      case "headerType":
        RectangleElement.ReadHeaderShowType((DocumentTreeNode) this, readArgs);
        return true;
      case "ignoreSkipOuter":
        RectangleElement.ReadIgnoreSkipCells((DocumentTreeNode) this, readArgs);
        return true;
      case "keepWithNext":
        RectangleElement.ReadKeepWithNext((DocumentTreeNode) this, readArgs);
        return true;
      case "mergeCell":
        RectangleElement.ReadMergeCell((DocumentTreeNode) this, readArgs);
        return true;
      case "nSkipAtSPg":
        RectangleElement.ReadNonSkipBeforeAtStartPage((DocumentTreeNode) this, readArgs);
        return true;
      case "skipAfter":
      case "skipCellsAfter":
        RectangleElement.ReadSkipCellsAfter((DocumentTreeNode) this, readArgs);
        return true;
      case "skipBefore":
      case "skipCellsBefore":
        RectangleElement.ReadSkipCellsBefore((DocumentTreeNode) this, readArgs);
        return true;
      case "span":
        RectangleElement.ReadSpan((DocumentTreeNode) this, readArgs);
        return true;
      case "startMerge":
        RectangleElement.ReadStartMerge((DocumentTreeNode) this, readArgs);
        return true;
      case "tryNotBreak":
        RectangleElement.ReadTryNotBreak((DocumentTreeNode) this, readArgs);
        return true;
      default:
        if (base.ReadFieldFromXml(readArgs))
          return true;
        switch (localName)
        {
          case "backColor":
            RectangleElement.ReadBackColor((DocumentTreeNode) this, readArgs);
            return true;
          case "borderWidth":
            RectangleElement.ReadBorderWidth((DocumentTreeNode) this, readArgs);
            return true;
          case "cellMarginBottom":
            RectangleElement.ReadCellMarginBottom((DocumentTreeNode) this, readArgs);
            return true;
          case "cellMarginLeft":
            RectangleElement.ReadCellMarginLeft((DocumentTreeNode) this, readArgs);
            return true;
          case "cellMarginRight":
            RectangleElement.ReadCellMarginRight((DocumentTreeNode) this, readArgs);
            return true;
          case "cellMarginTop":
            RectangleElement.ReadCellMarginTop((DocumentTreeNode) this, readArgs);
            return true;
          case "foreColor":
            RectangleElement.ReadForeColor((DocumentTreeNode) this, readArgs);
            return true;
          case "fxdRows":
            RectangleElement.ReadFxdRows((DocumentTreeNode) this, readArgs);
            return true;
          case "h":
          case "height":
            RectangleElement.ReadHeight((DocumentTreeNode) this, readArgs);
            return true;
          case "horzAlign":
            RectangleElement.ReadHorzAlign((DocumentTreeNode) this, readArgs);
            return true;
          case "location":
          case "pos":
            RectangleElement.ReadLocation((DocumentTreeNode) this, readArgs);
            return true;
          case "maxHeight":
            RectangleElement.ReadMaxHeight((DocumentTreeNode) this, readArgs);
            return true;
          case "minHeight":
            RectangleElement.ReadMinHeight((DocumentTreeNode) this, readArgs);
            return true;
          case "minWidth":
            RectangleElement.ReadMinWidth((DocumentTreeNode) this, readArgs);
            return true;
          case "onePageW":
            RectangleElement.ReadOnOnePageWith((DocumentTreeNode) this, readArgs);
            return true;
          case "orgPos":
            RectangleElement.ReadProperLocation((DocumentTreeNode) this, readArgs);
            return true;
          case "prevCell":
            RectangleElement.ReadPrevCell((DocumentTreeNode) this, readArgs);
            return true;
          case "relHeight":
            RectangleElement.ReadRelativeHeight((DocumentTreeNode) this, readArgs);
            return true;
          case "relWidth":
            RectangleElement.ReadRelativeWidth((DocumentTreeNode) this, readArgs);
            return true;
          case "rh":
            RectangleElement.ReadRowHeight((DocumentTreeNode) this, readArgs);
            return true;
          case "rowSize":
            RectangleElement.ReadRowSize((DocumentTreeNode) this, readArgs);
            return true;
          case "size":
            RectangleElement.ReadSize((DocumentTreeNode) this, readArgs);
            return true;
          case "vertAlign":
            RectangleElement.ReadVertAlign((DocumentTreeNode) this, readArgs);
            return true;
          case "w":
          case "width":
            RectangleElement.ReadWidth((DocumentTreeNode) this, readArgs);
            return true;
          default:
            return false;
        }
    }
  }

  public override void ReadFromXml(XmlReadArgs readArgs)
  {
    base.ReadFromXml(readArgs);
    if (readArgs.Version >= 40 || this.headerShowType != HeaderShowType.All)
      return;
    this.headerShowType = HeaderShowType.FirstOnly;
  }

  private static void InitReadFieldDict()
  {
    RectangleElement.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) PageElementNode.ReadFieldsDict);
    RectangleElement.ReadFieldsDict.Add("cellType", new ReadFieldFromXmlDelegate(RectangleElement.ReadTableCellType));
    RectangleElement.ReadFieldsDict.Add("fromNewPage", new ReadFieldFromXmlDelegate(RectangleElement.ReadFromNewPage));
    RectangleElement.ReadFieldsDict.Add("tryNotBreak", new ReadFieldFromXmlDelegate(RectangleElement.ReadTryNotBreak));
    RectangleElement.ReadFieldsDict.Add("keepWithNext", new ReadFieldFromXmlDelegate(RectangleElement.ReadKeepWithNext));
    RectangleElement.ReadFieldsDict.Add("tableCellType", new ReadFieldFromXmlDelegate(RectangleElement.ReadTableCellType));
    RectangleElement.ReadFieldsDict.Add("Borders", new ReadFieldFromXmlDelegate(RectangleElement.ReadBorders));
    RectangleElement.ReadFieldsDict.Add("headerShowType", new ReadFieldFromXmlDelegate(RectangleElement.ReadHeaderShowType));
    RectangleElement.ReadFieldsDict.Add("headerType", new ReadFieldFromXmlDelegate(RectangleElement.ReadHeaderShowType));
    RectangleElement.ReadFieldsDict.Add("skipCellsBefore", new ReadFieldFromXmlDelegate(RectangleElement.ReadSkipCellsBefore));
    RectangleElement.ReadFieldsDict.Add("skipBefore", new ReadFieldFromXmlDelegate(RectangleElement.ReadSkipCellsBefore));
    RectangleElement.ReadFieldsDict.Add("skipCellsAfter", new ReadFieldFromXmlDelegate(RectangleElement.ReadSkipCellsAfter));
    RectangleElement.ReadFieldsDict.Add("skipAfter", new ReadFieldFromXmlDelegate(RectangleElement.ReadSkipCellsAfter));
    RectangleElement.ReadFieldsDict.Add("ignoreSkipOuter", new ReadFieldFromXmlDelegate(RectangleElement.ReadIgnoreSkipCells));
    RectangleElement.ReadFieldsDict.Add("isSelectedCell", new ReadFieldFromXmlDelegate(RectangleElement.ReadIsSelectedCell));
    RectangleElement.ReadFieldsDict.Add("nSkipAtSPg", new ReadFieldFromXmlDelegate(RectangleElement.ReadNonSkipBeforeAtStartPage));
    RectangleElement.ReadFieldsDict.Add("gridID", new ReadFieldFromXmlDelegate(RectangleElement.ReadGridID));
    RectangleElement.ReadFieldsDict.Add("span", new ReadFieldFromXmlDelegate(RectangleElement.ReadSpan));
    RectangleElement.ReadFieldsDict.Add("startMerge", new ReadFieldFromXmlDelegate(RectangleElement.ReadStartMerge));
    RectangleElement.ReadFieldsDict.Add("mergeCell", new ReadFieldFromXmlDelegate(RectangleElement.ReadMergeCell));
    RectangleElement.ReadFieldsDict.Add("location", new ReadFieldFromXmlDelegate(RectangleElement.ReadLocation));
    RectangleElement.ReadFieldsDict.Add("pos", new ReadFieldFromXmlDelegate(RectangleElement.ReadLocation));
    RectangleElement.ReadFieldsDict.Add("orgPos", new ReadFieldFromXmlDelegate(RectangleElement.ReadProperLocation));
    RectangleElement.ReadFieldsDict.Add("size", new ReadFieldFromXmlDelegate(RectangleElement.ReadSize));
    RectangleElement.ReadFieldsDict.Add("w", new ReadFieldFromXmlDelegate(RectangleElement.ReadWidth));
    RectangleElement.ReadFieldsDict.Add("width", new ReadFieldFromXmlDelegate(RectangleElement.ReadWidth));
    RectangleElement.ReadFieldsDict.Add("h", new ReadFieldFromXmlDelegate(RectangleElement.ReadHeight));
    RectangleElement.ReadFieldsDict.Add("height", new ReadFieldFromXmlDelegate(RectangleElement.ReadHeight));
    RectangleElement.ReadFieldsDict.Add("rh", new ReadFieldFromXmlDelegate(RectangleElement.ReadRowHeight));
    RectangleElement.ReadFieldsDict.Add("minHeight", new ReadFieldFromXmlDelegate(RectangleElement.ReadMinHeight));
    RectangleElement.ReadFieldsDict.Add("maxHeight", new ReadFieldFromXmlDelegate(RectangleElement.ReadMaxHeight));
    RectangleElement.ReadFieldsDict.Add("minWidth", new ReadFieldFromXmlDelegate(RectangleElement.ReadMinWidth));
    RectangleElement.ReadFieldsDict.Add("borderWidth", new ReadFieldFromXmlDelegate(RectangleElement.ReadBorderWidth));
    RectangleElement.ReadFieldsDict.Add("foreColor", new ReadFieldFromXmlDelegate(RectangleElement.ReadForeColor));
    RectangleElement.ReadFieldsDict.Add("backColor", new ReadFieldFromXmlDelegate(RectangleElement.ReadBackColor));
    RectangleElement.ReadFieldsDict.Add("fxdRows", new ReadFieldFromXmlDelegate(RectangleElement.ReadFxdRows));
    RectangleElement.ReadFieldsDict.Add("rowSize", new ReadFieldFromXmlDelegate(RectangleElement.ReadRowSize));
    RectangleElement.ReadFieldsDict.Add("horzAlign", new ReadFieldFromXmlDelegate(RectangleElement.ReadHorzAlign));
    RectangleElement.ReadFieldsDict.Add("vertAlign", new ReadFieldFromXmlDelegate(RectangleElement.ReadVertAlign));
    RectangleElement.ReadFieldsDict.Add("relWidth", new ReadFieldFromXmlDelegate(RectangleElement.ReadRelativeWidth));
    RectangleElement.ReadFieldsDict.Add("relHeight", new ReadFieldFromXmlDelegate(RectangleElement.ReadRelativeHeight));
    RectangleElement.ReadFieldsDict.Add("cellMarginLeft", new ReadFieldFromXmlDelegate(RectangleElement.ReadCellMarginLeft));
    RectangleElement.ReadFieldsDict.Add("cellMarginRight", new ReadFieldFromXmlDelegate(RectangleElement.ReadCellMarginRight));
    RectangleElement.ReadFieldsDict.Add("cellMarginTop", new ReadFieldFromXmlDelegate(RectangleElement.ReadCellMarginTop));
    RectangleElement.ReadFieldsDict.Add("cellMarginBottom", new ReadFieldFromXmlDelegate(RectangleElement.ReadCellMarginBottom));
    RectangleElement.ReadFieldsDict.Add("prevCell", new ReadFieldFromXmlDelegate(RectangleElement.ReadPrevCell));
    RectangleElement.ReadFieldsDict.Add("onePageW", new ReadFieldFromXmlDelegate(RectangleElement.ReadOnOnePageWith));
    RectangleElement.ReadFieldsDict.Add("desiredPageNumber", new ReadFieldFromXmlDelegate(RectangleElement.ReadDesiredPageNumber));
    RectangleElement.ReadFieldsDict.Add("overrideTemplateId", new ReadFieldFromXmlDelegate(RectangleElement.ReadOverrideTemplateId));
    RectangleElement.ReadFieldsDict.Add("drawEllipse", new ReadFieldFromXmlDelegate(RectangleElement.ReadDrawEllipse));
  }

  private static void ReadRowSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (readArgs.Version < 35 && rectangleElement.IsTemplate && rectangleElement.Page != null && rectangleElement.TopLevelTable != null && rectangleElement.OwnerDocument != null && (rectangleElement.IsBadFixedRowSize_OldSpecFormB() || rectangleElement.IsBadFixedRowSize_OldSpecFormV() || rectangleElement.IsBadFixedRowSize_OldSpec_AutoIndustry_Single() || rectangleElement.IsBadFixedRowSize_OldSpec_AutoIndustry_Mirror() || rectangleElement.IsBadFixedRowSize_OldSpec_AutoIndustry_FormB()))
      return;
    rectangleElement.defaultRowSize = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    rectangleElement.overrideFlags |= OverrideFlags.DefaultRowSize;
    rectangleElement.overrideFlags2 |= OverrideFlags2.ParentDefaultRowSize;
  }

  /// <summary>Вспомогательный метод для идентификации таблицы с ошибкой в размере строки сетки</summary>
  /// <returns></returns>
  private bool IsBadFixedRowSize_OldSpecFormB()
  {
    if (!(this.OwnerDocument.Name == "Шаблон групповой спецификации формы Б"))
      return false;
    if (this.Page.Name == "Заглавный лист. ГОСТ 2.113-75 Форма 1" && this.TopLevelTable.Id == "Заголовок спецификации" && this.Id == "Заголовок исполнений #4" || this.Page.Name == "Лист продолжения. ГОСТ 2.113-75 Форма 1а" && this.TopLevelTable.Id == "Заголовок спецификации #2" && this.Id == "Заголовок исполнений #5")
      return true;
    return this.Page.Name == "Следующий блок исполнений. ГОСТ 2.113-75 Форма 1в" && this.TopLevelTable.Id == "Заголовок спецификации #3" && this.Id == "Заголовок исполнений #2";
  }

  /// <summary>Вспомогательный метод для идентификации таблицы с ошибкой в размере строки сетки</summary>
  /// <returns></returns>
  private bool IsBadFixedRowSize_OldSpecFormV()
  {
    if (!(this.OwnerDocument.Name == "Шаблон групповой спецификации формы В"))
      return false;
    if (this.Page.Id == "ГОСТ 2.113-75. Форма 1" && this.TopLevelTable.Id == "Заголовок спецификации #3" && this.Id == "Заголовок исполнений #4" || this.Page.Id == "ГОСТ 2.113-75. Форма 1в" && this.TopLevelTable.Id == "Заголовок спецификации #8" && this.Id == "Заголовок исполнений #2")
      return true;
    return this.Page.Id == "ГОСТ 2.113-75. Форма 1а" && this.TopLevelTable.Id == "Заголовок спецификации #6" && this.Id == "Заголовок исполнений #5";
  }

  /// <summary>Вспомогательный метод для идентификации таблицы с ошибкой в размере строки сетки</summary>
  /// <returns></returns>
  private bool IsBadFixedRowSize_OldSpec_AutoIndustry_Single()
  {
    if (!(this.OwnerDocument.Name == "Шаблон автомобильной спецификации"))
      return false;
    if (this.Page.Name == "Заглавный лист. ГОСТ 2.113-75 Форма 1" && this.TopLevelTable.Id == "Заголовок спецификации" && this.Id == "Заголовок исполнений #4" || this.Page.Name == "Лист продолжения. ГОСТ 2.113-75 Форма 1а" && this.TopLevelTable.Id == "Заголовок спецификации #2" && this.Id == "Заголовок исполнений #3")
      return true;
    return this.Page.Name == "Следующий блок исполнений. ГОСТ 2.113-75 Форма 1в" && this.TopLevelTable.Id == "Заголовок спецификации #3" && this.Id == "Заголовок исполнений #2";
  }

  /// <summary>Вспомогательный метод для идентификации таблицы с ошибкой в размере строки сетки</summary>
  /// <returns></returns>
  private bool IsBadFixedRowSize_OldSpec_AutoIndustry_Mirror()
  {
    if (!(this.OwnerDocument.Name == "Шаблон автомобильной зеркальной спецификации"))
      return false;
    if (this.Page.Name == "Заглавный лист. ГОСТ 2.113-75 Форма 1" && this.TopLevelTable.Id == "Заголовок спецификации" && this.Id == "Заголовок исполнений #4" || this.Page.Name == "Лист продолжения. ГОСТ 2.113-75 Форма 1а" && this.TopLevelTable.Id == "Заголовок спецификации #2" && this.Id == "Заголовок исполнений #3")
      return true;
    return this.Page.Name == "Следующий блок исполнений. ГОСТ 2.113-75 Форма 1в" && this.TopLevelTable.Id == "Заголовок спецификации #3" && this.Id == "Заголовок исполнений #2";
  }

  /// <summary>Вспомогательный метод для идентификации таблицы с ошибкой в размере строки сетки</summary>
  /// <returns></returns>
  private bool IsBadFixedRowSize_OldSpec_AutoIndustry_FormB()
  {
    if (!(this.OwnerDocument.Name == "Шаблон автомобильной групповой спецификации формы Б"))
      return false;
    if (this.Page.Name == "Заглавный лист. ГОСТ 2.113-75 Форма 1" && this.TopLevelTable.Id == "Заголовок спецификации" && this.Id == "Заголовок исполнений #4" || this.Page.Name == "Лист продолжения. ГОСТ 2.113-75 Форма 1а" && this.TopLevelTable.Id == "Заголовок спецификации #2" && this.Id == "Заголовок исполнений #5")
      return true;
    return this.Page.Name == "Следующий блок исполнений. ГОСТ 2.113-75 Форма 1в" && this.TopLevelTable.Id == "Заголовок спецификации #3" && this.Id == "Заголовок исполнений #2";
  }

  private static void ReadFxdRows(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    int num = bool.Parse(readArgs.Reader.Value) ? 1 : 0;
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (num != 0)
      return;
    if ((double) rectangleElement.minHeight < (double) rectangleElement.defaultRowSize)
      rectangleElement.minHeight = rectangleElement.defaultRowSize;
    rectangleElement.defaultRowSize = 0.0f;
    rectangleElement.overrideFlags |= OverrideFlags.DefaultRowSize;
    rectangleElement.overrideFlags2 |= OverrideFlags2.ParentDefaultRowSize;
  }

  private static void ReadBackColor(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.overrideFlags |= OverrideFlags.BackColor;
    if (readArgs.Version < 11)
      ((RectangleElement) docNode).backColor = Color.FromName(readArgs.Reader.Value);
    else
      ((RectangleElement) docNode).backColor = (Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
  }

  private static void ReadForeColor(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.overrideFlags3 |= OverrideFlags3.ForeColor;
    if (readArgs.Version < 11)
      ((RectangleElement) docNode).foreColor = Color.FromName(readArgs.Reader.Value);
    else
      ((RectangleElement) docNode).foreColor = (Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
  }

  private static void ReadBorderWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 18)
      ((RectangleElement) docNode).borderWidth = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    else
      ((RectangleElement) docNode).borderWidth = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadMaxHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 18)
      ((RectangleElement) docNode).maxHeight = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    else
      ((RectangleElement) docNode).maxHeight = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.SetOverrideFlags(OverrideFlags.MaxHeight);
  }

  private static void ReadMinHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = docNode as RectangleElement;
    rectangleElement.minHeight = readArgs.Version >= 18 ? float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture) : float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    if ((readArgs.Version < 31 /*0x1F*/ && rectangleElement is TableData || rectangleElement.ParentCell == null) && (double) rectangleElement.minHeight > (double) rectangleElement.properBounds.Height)
      rectangleElement.minHeight = rectangleElement.properBounds.Height;
    docNode.overrideFlags |= OverrideFlags.MinHeight;
  }

  private static void ReadMinWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).minWidth = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags |= OverrideFlags.MinWidth;
  }

  private static void ReadHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    float height = readArgs.Version >= 18 ? float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture) : float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    rectangleElement.setProperBounds(new RectangleF(rectangleElement.properBounds.X, rectangleElement.properBounds.Y, rectangleElement.properBounds.Width, height));
    if ((readArgs.Version < 30 && docNode is TableData || rectangleElement.ParentCell == null) && (double) rectangleElement.minHeight > (double) rectangleElement.properBounds.Height)
      rectangleElement.minHeight = rectangleElement.properBounds.Height;
    rectangleElement.SetOverrideFlags(OverrideFlags.Height);
    rectangleElement.SetOverrideFlags2(OverrideFlags2.RowHeight);
  }

  private static void ReadRowHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleF properBounds = ((RectangleElement) docNode).properBounds;
    ((RectangleElement) docNode).setProperBounds(new RectangleF(properBounds.X, properBounds.Y, properBounds.Width, float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture)));
  }

  private static void ReadWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    float width = readArgs.Version >= 18 ? float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture) : float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    rectangleElement.setProperBounds(new RectangleF(rectangleElement.properBounds.X, rectangleElement.properBounds.Y, width, rectangleElement.properBounds.Height));
    rectangleElement.overrideFlags |= OverrideFlags.Width;
    rectangleElement.overrideFlags2 |= OverrideFlags2.ColumnWidth;
  }

  private static void ReadSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    SizeF size = readArgs.Version >= 16 /*0x10*/ ? (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value) : (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, DocumentTreeNode.ReplaceDS(readArgs.Reader.Value));
    rectangleElement.SetOverrideFlags(OverrideFlags.Width);
    rectangleElement.SetOverrideFlags2(OverrideFlags2.ColumnWidth);
    rectangleElement.SetOverrideFlags(OverrideFlags.Height);
    rectangleElement.SetOverrideFlags2(OverrideFlags2.RowHeight);
    rectangleElement.setProperBounds(new RectangleF(rectangleElement.properBounds.Location, size));
  }

  private static void ReadLocation(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (readArgs.Version < 16 /*0x10*/)
      rectangleElement.setBounds(BoundsHelper.SetLocation(rectangleElement.bounds, (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, DocumentTreeNode.ReplaceDS(readArgs.Reader.Value))));
    else
      rectangleElement.setBounds(BoundsHelper.SetLocation(rectangleElement.bounds, (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value)));
    rectangleElement.SetOverrideFlags(OverrideFlags.Geometry);
  }

  private static void ReadProperLocation(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    rectangleElement.setProperBounds(new RectangleF((PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value), rectangleElement.properBounds.Size));
    rectangleElement.SetOverrideFlags(OverrideFlags.Geometry);
  }

  private static void ReadSpan(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    int num = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    if (rectangleElement.gridPos == null)
      rectangleElement.gridPos = new TableGridPosition();
    rectangleElement.gridPos.SetCellSpan(num);
  }

  private static void ReadStartMerge(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    string str = readArgs.Reader.Value;
    if (rectangleElement.gridPos == null)
      rectangleElement.gridPos = new TableGridPosition();
    rectangleElement.gridPos.StartMerge = str == "1";
  }

  private static void ReadMergeCell(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (str == null || !(str != ""))
      return;
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (rectangleElement.gridPos == null)
      rectangleElement.gridPos = new TableGridPosition();
    rectangleElement.gridPos.MergeWithCell = readArgs.ObjectsId[(object) str] as RectangleElement;
    if (rectangleElement.gridPos.MergeWithCell != null)
      return;
    DocumentTreeNode.AddObjectReference((object) rectangleElement.gridPos, readArgs.ObjectReferences, "mergeWithCell", str);
  }

  private static void ReadGridID(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    int gridID = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    if (gridID == -1)
      return;
    ((RectangleElement) docNode).gridPos = (TableGridPosition) new GridIdPosition(gridID);
  }

  private static void ReadSkipCellsAfter(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 18)
      ((RectangleElement) docNode).skipCellsAfter = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    else
      ((RectangleElement) docNode).skipCellsAfter = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags |= OverrideFlags.SkipAfter;
  }

  private static void ReadIgnoreSkipCells(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).ignoreSkipOuterCells = bool.Parse(readArgs.Reader.Value);
    ((RectangleElement) docNode).overrideFlags3 |= OverrideFlags3.IgnoreSkipOuterCells;
  }

  private static void ReadIsSelectedCell(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).AssignIsSelectedDataCellTemplate(readArgs.Reader.Value != "0");
  }

  private static void ReadNonSkipBeforeAtStartPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).AssignNonSkipBeforeAtStartPage(readArgs.Reader.Value == "1", true);
  }

  private static void ReadSkipCellsBefore(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 18)
      ((RectangleElement) docNode).skipCellsBefore = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    else
      ((RectangleElement) docNode).skipCellsBefore = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags |= OverrideFlags.SkipBefore;
    if (readArgs.Version >= 22)
      return;
    docNode.SetNeedUpdateLayoutFlag(true, true, false, false);
  }

  private static void ReadHeaderShowType(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).headerShowType = (HeaderShowType) Enum.Parse(typeof (HeaderShowType), readArgs.Reader.Value);
  }

  private static void ReadBorders(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    rectangleElement.borders = (RectangleBorder) new CustomBorder();
    rectangleElement.borders.ReadFromXml(readArgs);
    if (rectangleElement.borders.Top != null)
      rectangleElement.overrideFlags |= OverrideFlags.TopBorder;
    if (rectangleElement.borders.Bottom != null)
      rectangleElement.overrideFlags |= OverrideFlags.BottomBorder;
    if (rectangleElement.borders.InnerHorizontal != null)
    {
      rectangleElement.overrideFlags3 |= OverrideFlags3.InnerHorizontalLine;
      rectangleElement.overrideFlags2 |= OverrideFlags2.ParentInnerHorizontalLine;
    }
    if (rectangleElement.borders.Left != null)
    {
      rectangleElement.overrideFlags |= OverrideFlags.LeftBorder;
      rectangleElement.overrideFlags2 |= OverrideFlags2.ColumnLeftBorder;
    }
    if (rectangleElement.borders.Right == null)
      return;
    rectangleElement.overrideFlags |= OverrideFlags.RightBorder;
    rectangleElement.overrideFlags2 |= OverrideFlags2.ColumnRightBorder;
  }

  private static void ReadTableCellType(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    if (readArgs.Version < 13)
    {
      if (!(s == "ParentTableDataCell"))
        ;
    }
    else if (readArgs.Version < 21)
      ((RectangleElement) docNode).tableCellType = (CellType) Enum.Parse(typeof (CellType), s);
    else
      ((RectangleElement) docNode).tableCellType = (CellType) int.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadFromNewPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    RectangleElement rectangleElement = (RectangleElement) docNode;
    rectangleElement.fromNewPage = readArgs.Reader.Value == "1";
    rectangleElement.overrideFlags |= OverrideFlags.FromNewPage;
  }

  private static void ReadTryNotBreak(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).tryNotBreak = readArgs.Reader.Value == "1";
  }

  private static void ReadKeepWithNext(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).keepWithNext = readArgs.Reader.Value == "1";
    docNode.overrideFlags |= OverrideFlags.KeepWithNext;
  }

  private static void ReadHorzAlign(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ElementHorizontalAlign elementHorizontalAlign = (ElementHorizontalAlign) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (readArgs.Version < 44 && (!rectangleElement.IsFormulaLib || rectangleElement.ParentCell != null))
      elementHorizontalAlign = ElementHorizontalAlign.None;
    rectangleElement.horzAlign = elementHorizontalAlign;
  }

  private static void ReadVertAlign(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ElementVerticalAlign elementVerticalAlign = (ElementVerticalAlign) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    RectangleElement rectangleElement = (RectangleElement) docNode;
    if (readArgs.Version < 44 && (!docNode.IsFormulaLib || rectangleElement.ParentCell != null))
      elementVerticalAlign = ElementVerticalAlign.None;
    rectangleElement.vertAlign = elementVerticalAlign;
  }

  private static void ReadRelativeWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).relativeWidth = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags3 |= OverrideFlags3.RelativeWidth;
  }

  private static void ReadCellMarginLeft(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).cellMargins.X = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadCellMarginRight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).cellMargins.Width = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadCellMarginTop(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).cellMargins.Y = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadCellMarginBottom(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).cellMargins.Height = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadRelativeHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string s = readArgs.Reader.Value;
    ((RectangleElement) docNode).relativeHeight = float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags3 |= OverrideFlags3.RelativeHeight;
  }

  private static void ReadPrevCell(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (str == null || !(str != ""))
      return;
    if (!(readArgs.ObjectsId[(object) str] is RectangleElement rectangleElement))
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "prevCell", str);
    else
      ((RectangleElement) docNode).SetPrevCell(rectangleElement);
  }

  private static void ReadOnOnePageWith(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (str == null || !(str != ""))
      return;
    if (!(readArgs.ObjectsId[(object) str] is RectangleElement rectangleElement))
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "onOnePageWith", str);
    else
      ((RectangleElement) docNode).SetOnOnePageWith(rectangleElement, false, false);
  }

  private static void ReadDesiredPageNumber(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    int result;
    if (!int.TryParse(readArgs.Reader.Value, out result))
      return;
    ((RectangleElement) docNode).desiredPageNumber = result;
  }

  private static void ReadOverrideTemplateId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode)._overrideTemplateId = readArgs.Reader.Value;
  }

  protected static void ReadDrawEllipse(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((RectangleElement) docNode).drawEllipse = readArgs.Reader.Value == "1";
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXmlOldFormats_Before(XmlReadArgs readArgs)
  {
    if (readArgs.Version < 18)
      this.tableCellType = CellType.Header;
    base.ReadFromXmlOldFormats_Before(readArgs);
  }

  /// <summary>Восстановить сохраненные ссылки</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  public override void RestoreLinks(
    bool copyChildren,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    base.RestoreLinks(copyChildren, templateClone, externalLink, links);
    RectangleElement link1 = (RectangleElement) links[(object) this];
    if (link1 == null || !externalLink)
      return;
    if (this.nextCell != null)
      link1.SetNextCellInternal((RectangleElement) links[(object) this.nextCell]);
    if (this.prevCell != null)
      link1.prevCell = (RectangleElement) links[(object) this.prevCell];
    if (this.onOnePageWith != null)
      link1.onOnePageWith = (RectangleElement) links[(object) this.onOnePageWith];
    if (this.gridPos == null || this.gridPos.MergeWithCell == null)
      return;
    RectangleElement link2 = (RectangleElement) links[(object) this.gridPos.MergeWithCell];
    if (link2 == null)
      return;
    if (link1.gridPos == null)
      link1.gridPos = new TableGridPosition();
    link1.gridPos.MergeWithCell = link2;
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public override void OnDeserialization(object sender)
  {
    base.OnDeserialization(sender);
    if (this.prevCell != null)
      this.prevCell.SetNextCell(this);
    if (this.onOnePageWith != null)
      this.onOnePageWith.SetOnOnePageWith(this, false, false);
    TableData parentCell = this.ParentCell;
    if (this.TemplateId == null)
    {
      RectangleF rectangleF;
      if (parentCell != null && !parentCell.IsFixedStructureArea)
      {
        rectangleF = this.Bounds;
        this.setProperBounds(new RectangleF(this.CalcProperLocation(rectangleF.Location), this.properBounds.Size));
      }
      RectangleF bounds = this.bounds;
      rectangleF = this.ProperBounds;
      SizeF size = this.CalcSizeFromProper(rectangleF.Size);
      this.setBounds(BoundsHelper.SetSize(bounds, size));
    }
    if (parentCell == null || parentCell.GetGridColumnsParams(false) != null)
      return;
    this.overrideFlags |= OverrideFlags.Width;
    this.overrideFlags2 |= OverrideFlags2.ColumnWidth;
  }

  public override void ClearExternalLinks(IEnumerable<DocumentTreeNode> rootNodes)
  {
    base.ClearExternalLinks(rootNodes);
    if (this.IsExternalFieldInContextNodes("prevCell", rootNodes))
      this.SetPrevCell((RectangleElement) null);
    if (this.IsExternalFieldInContextNodes("nextCell", rootNodes))
      this.SetNextCell((RectangleElement) null);
    if (this.nodes == null)
      return;
    foreach (DocumentTreeNode node in this.nodes)
      node.ClearExternalLinks(rootNodes);
  }

  private bool IsExternalFieldInContextNodes(
    string fieldName,
    IEnumerable<DocumentTreeNode> contextNodes)
  {
    return this.prevCell != null && fieldName == "prevCell" && !contextNodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (p => p == this.prevCell || p.IsParentForNode((DocumentTreeNode) this.prevCell, false))) || this.nextCell != null && fieldName == "nextCell" && !contextNodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (p => p == this.nextCell || p.IsParentForNode((DocumentTreeNode) this.nextCell, false)));
  }

  protected override bool IsExternalLinkField(
    MemberInfo mi,
    IEnumerable<DocumentTreeNode> rootNodes)
  {
    if (base.IsExternalLinkField(mi, rootNodes))
      return true;
    return rootNodes != null && this.IsExternalFieldInContextNodes(mi.Name, rootNodes);
  }

  /// <summary>Узел являющийся переносимыми данными в таблице.
  /// Только для внутреннего использования!</summary>
  internal override bool IsDataNode
  {
    [DebuggerStepThrough] get
    {
      if (this.TableCellType != CellType.DataCell)
        return false;
      TableData topLevelTable = this.TopLevelTable;
      if (topLevelTable != null && (topLevelTable == this || !topLevelTable.IsPageFlow))
        return false;
      TableData parentCell = this.ParentCell;
      return parentCell != null && parentCell.IsColumn;
    }
  }

  /// <summary>
  /// Ячейка принадлежит таблице, которая разбивается по страницам.
  /// Если является такой таблицей, также вернёт true.
  /// </summary>
  [Browsable(false)]
  public bool IsCellInDataFlowTable
  {
    get
    {
      TableData topLevelTable = this.TopLevelTable;
      return topLevelTable != null && topLevelTable.IsPageFlow;
    }
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
    bool visible = this.Visible;
    base.CopyFields(src, copyChildren, copyData, copyDataNodes, templateClone, externalLink, links);
    if (!(src is RectangleElement rectangleElement))
      return;
    if (!templateClone)
    {
      this.owner = rectangleElement.owner;
      this.SetIsSelectedDataCellTemplate(rectangleElement.IsSelectedDataCellTemplate, false);
      this._overrideTemplateId = rectangleElement._overrideTemplateId;
    }
    if (rectangleElement.borders != null)
      this.borders = rectangleElement.borders.Clone();
    this.defaultRowSize = rectangleElement.defaultRowSize;
    this.setBounds(rectangleElement.bounds);
    this.setProperBounds(rectangleElement.properBounds);
    this.foreColor = rectangleElement.foreColor;
    this.backColor = rectangleElement.backColor;
    this.skipCellsBefore = rectangleElement.skipCellsBefore;
    this.skipCellsAfter = rectangleElement.skipCellsAfter;
    this.ignoreSkipOuterCells = rectangleElement.ignoreSkipOuterCells;
    this.AssignNonSkipBeforeAtStartPage(rectangleElement.NonSkipBeforeAtStartPage, false);
    this.borderWidth = rectangleElement.borderWidth;
    this.tableCellType = rectangleElement.tableCellType;
    this.headerShowType = rectangleElement.headerShowType;
    this.minHeight = rectangleElement.minHeight;
    this.maxHeight = rectangleElement.maxHeight;
    this.minWidth = rectangleElement.minWidth;
    this.horzAlign = rectangleElement.horzAlign;
    this.vertAlign = rectangleElement.vertAlign;
    this.relativeWidth = rectangleElement.relativeWidth;
    this.relativeHeight = rectangleElement.relativeHeight;
    this.cellMargins = rectangleElement.cellMargins;
    this.fromNewPage = rectangleElement.fromNewPage;
    this.tryNotBreak = rectangleElement.tryNotBreak;
    this.keepWithNext = rectangleElement.keepWithNext;
    this.desiredPageNumber = rectangleElement.desiredPageNumber;
    this.drawEllipse = rectangleElement.drawEllipse;
    this.gridPos = rectangleElement.gridPos == null || templateClone ? (TableGridPosition) null : rectangleElement.gridPos.Clone();
    if (!templateClone || !rectangleElement.IsDataNode)
      return;
    this.AssingVisible(visible);
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Действие в контексте загрузки документа</param>
  public override void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    if (isLoading || this.isVirtualNode)
      base.AssignParent(value, updateUI, updateLayout, isLoading);
    else
      base.AssignParent(value, false, updateLayout, isLoading);
    TableData parentCell = this.ParentCell;
    if (parentCell == null || !this.IsDataNode || isLoading)
      return;
    if (this.GetAttributeValue("ignoreSetVisible", false) == null)
    {
      this.SetVisible(parentCell.Visible, false, false, updateLayout, false, false);
    }
    else
    {
      int num1 = parentCell.Visible ? 1 : 0;
      int num2 = this.Visible ? 1 : 0;
    }
    this.SetNeedUpdateUIGeometryRecursive(true, updateUI);
  }

  /// <summary>Владелец виртуального узла. Для реального узла смысла не имеет.</summary>
  [Browsable(false)]
  internal DocumentTreeNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Установить значение свойства Owner</summary>
  /// <param name="owner">Новое значение свойства Owner</param>
  public virtual void SetOwner(DocumentTreeNode owner) => this.owner = owner;

  /// <summary>Это ячейка таблицы</summary>
  [Browsable(false)]
  public virtual bool IsTableCell
  {
    [DebuggerStepThrough] get => this.ParentCell != null;
  }

  /// <summary>Ячейка является последней в строке/столбце</summary>
  [Browsable(false)]
  public bool IsLastInParentCell
  {
    [DebuggerStepThrough] get
    {
      return this.Parent == null || this.Parent.Nodes[this.Parent.Nodes.Count - 1] == this;
    }
  }

  /// <summary>Ячейка является первой в строке/столбце</summary>
  [Browsable(false)]
  public bool IsFirstInParentCell
  {
    [DebuggerStepThrough] get => this.Parent == null || this.Parent.Nodes[0] == this;
  }

  /// <summary>Ячейка находится в начале таблицы на странице</summary>
  [Browsable(false)]
  public bool IsFirstCellOnPage
  {
    [DebuggerStepThrough] get
    {
      bool isFirstCellOnPage = true;
      TableData parentCell = this.ParentCell;
      RectangleElement rectangleElement = this;
      for (; isFirstCellOnPage && parentCell != null; parentCell = parentCell.ParentCell)
      {
        isFirstCellOnPage = parentCell.Nodes[0] == rectangleElement;
        rectangleElement = (RectangleElement) parentCell;
      }
      return isFirstCellOnPage;
    }
  }

  /// <summary>Эта ячейка является единичной ячейкой без вложенных ячеек</summary>
  /// <returns>true, если ячейка является единичной</returns>
  [Browsable(false)]
  public bool IsSingleCell
  {
    [DebuggerStepThrough] get => this.nodes == null || this.nodes.Count == 0;
  }

  /// <summary>Получить ближайшую таблицу- столбец в иерархии, владеющую элементом</summary>
  [Browsable(false)]
  public TableData OwnerSubTable
  {
    [DebuggerStepThrough] get
    {
      if (this is TableData)
      {
        TableData ownerSubTable = this as TableData;
        if (ownerSubTable.IsColumn)
          return ownerSubTable;
        if (ownerSubTable.IsRow)
          return ownerSubTable.ParentCell;
      }
      else if (this.IsTableCell && this.ParentCell != null)
        return this.ParentCell.ParentCell;
      return (TableData) null;
    }
  }

  /// <summary>Получить ближайшую строку в иерархии, владеющую одиночным элементом</summary>
  [Browsable(false)]
  public TableData OwnerRow
  {
    [DebuggerStepThrough] get
    {
      if (this is TableData)
      {
        TableData tableData = this as TableData;
        if (tableData.IsColumn)
          return (TableData) null;
        if (tableData.IsRow)
          return this as TableData;
      }
      else if (this.IsTableCell && this.ParentCell != null)
        return this.ParentCell;
      return (TableData) null;
    }
  }

  /// <summary>Таблица владеющая элементом</summary>
  [Browsable(false)]
  public TableData TableOwner
  {
    [DebuggerStepThrough] get => this.GetTableOwner(this) as TableData;
  }

  /// <summary>Получить реальную таблицу владельца (таблицу столбец или вернюю таблицу)</summary>
  /// <param name="cell"></param>
  /// <returns></returns>
  private RectangleElement GetTableOwner(RectangleElement cell)
  {
    if (!cell.IsSingleCell && cell.IsVirtualNode)
      return cell.Nodes.Count == 0 ? (RectangleElement) null : this.GetTableOwner(cell.Nodes[0] as RectangleElement);
    RectangleElement tableOwner;
    for (tableOwner = cell; tableOwner.ParentCell != null; tableOwner = (RectangleElement) tableOwner.ParentCell)
    {
      if (tableOwner.ParentCell.IsColumn)
        return (RectangleElement) tableOwner.ParentCell;
    }
    return tableOwner;
  }

  /// <summary>Корневая таблица, т.е. таблица владеющая этим элементом
  /// и не принадлежащая другим таблицам</summary>
  [Browsable(false)]
  public virtual TableData TopLevelTable
  {
    [DebuggerStepThrough] get
    {
      return this.ParentCell != null ? this.ParentCell.TopLevelTable : this as TableData;
    }
  }

  /// <summary>Родительская ячейка</summary>
  [Browsable(false)]
  public virtual TableData ParentCell
  {
    [DebuggerStepThrough] get => this.Parent as TableData;
  }

  /// <summary>Получить следующую ячейку</summary>
  [Browsable(false)]
  public RectangleElement NextNode
  {
    [DebuggerStepThrough] get
    {
      TableData parentCell = this.ParentCell;
      RectangleElement nextNode = (RectangleElement) null;
      int index = this.Index + 1;
      if (parentCell != null && index < parentCell.Nodes.Count)
        nextNode = parentCell.Nodes[index] as RectangleElement;
      return nextNode;
    }
  }

  /// <summary>Получить предыдущую ячейку</summary>
  [Browsable(false)]
  public RectangleElement PrevNode
  {
    get
    {
      TableData parentCell = this.ParentCell;
      RectangleElement prevNode = (RectangleElement) null;
      int index = this.Index - 1;
      if (parentCell != null && index >= 0)
        prevNode = parentCell.Nodes[index] as RectangleElement;
      return prevNode;
    }
  }

  /// <summary>Располагать элемент на одной странице с заданным в параллельной таблице.
  /// Пока только для внутреннего пользования в экспортной СП</summary>
  [Category("Debug")]
  public RectangleElement OnOnePageWith
  {
    [DebuggerStepThrough] get => this.onOnePageWith;
    set => this.SetOnOnePageWith(value, true, true);
  }

  /// <summary>Назначить новое значение свойству OnOnePageWith</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetOnOnePageWith(RectangleElement value, bool updateUI, bool updateLayout)
  {
    if (this.onOnePageWith == value)
      return;
    RectangleElement onOnePageWith = this.onOnePageWith;
    this.onOnePageWith = (RectangleElement) null;
    onOnePageWith?.SetOnOnePageWith((RectangleElement) null, false, false);
    this.onOnePageWith = value;
    if (this.onOnePageWith != null)
      this.onOnePageWith.SetOnOnePageWith(this, false, false);
    TableData parentCell = this.ParentCell;
    if (parentCell != null)
      parentCell.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    else
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Вариант отображения заголовка, если элемент является заголовком</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_370")]
  [CustomDescription("Attribute.Interfaces.Document_371")]
  [CustomCategory("Attribute.Interfaces.Document_372")]
  public virtual HeaderShowType HeaderShowType
  {
    [DebuggerStepThrough] get => this.headerShowType;
    set => this.SetHeaderShowType(value, true, true);
  }

  /// <summary>Установить новое значение HeaderShowType</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetHeaderShowType(HeaderShowType value, bool updateUI, bool updateLayout)
  {
    if (this.headerShowType == value)
      return;
    this.headerShowType = value;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    if (!this.IsHeaderCell || this.IsTemplate)
      return;
    this.SetNeedUpdateLayoutFlag(true, true, true, true);
  }

  /// <summary>Получить индекс (для Nodes) строки в которой находится ячейка</summary>
  /// <param name="rowParent">Элемент владеющий этой строкой</param>
  public virtual int GetRowIndex(out TableData rowParent)
  {
    rowParent = (TableData) null;
    if (this is TableData tableData && tableData.IsColumn)
    {
      rowParent = tableData;
      return -1;
    }
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return -1;
    if (!parentCell.IsColumn)
      return parentCell.GetRowIndex(out rowParent);
    rowParent = parentCell;
    return this.Index;
  }

  /// <summary>Найти таблицу владеющую сеткой, которая должна производить добавление столбца</summary>
  /// <param name="autoCreateGrid">Создать грид, если он не существует</param>
  /// <returns>Таблицу владеющую сеткой</returns>
  public virtual TableData FindTableForAddColumn(bool autoCreateGrid)
  {
    TableData paramsOwner;
    bool fromTemplate;
    if (this is TableData tableForAddColumn)
    {
      if (tableForAddColumn.GetGridColumnsParams(out paramsOwner, out fromTemplate, autoCreateGrid, false) != null)
        return paramsOwner;
      if (tableForAddColumn.IsFixedStructureArea)
        return tableForAddColumn;
    }
    if (this.ParentCell != null)
    {
      if (this.ParentCell.GetGridColumnsParams(out paramsOwner, out fromTemplate, autoCreateGrid, false) != null)
        return paramsOwner;
      if (this.ParentCell.IsFixedStructureArea)
        return this.ParentCell;
    }
    return this.TopLevelTable;
  }

  /// <summary>Найти строку владеющую этой ячейкой</summary>
  /// <param name="checkThisCell">Проверять саму ячейку</param>
  /// <returns>Строку</returns>
  public RectangleElement FindParentRow(bool checkThisCell)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return (RectangleElement) null;
    return checkThisCell && parentCell.IsColumn ? this : parentCell.FindParentRow(true);
  }

  /// <summary>Найти столбец владеющий этой ячейкой</summary>
  /// <param name="checkThisCell">Проверять саму ячейку</param>
  /// <returns>Столбец</returns>
  public RectangleElement FindParentColumn(bool checkThisCell)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null)
      return (RectangleElement) null;
    return checkThisCell && parentCell.IsRow ? this : parentCell.FindParentColumn(true);
  }

  /// <summary> Сравнивает два float значения с некоторой точностью (==) </summary>
  /// <param name="float1"></param>
  /// <param name="float2"></param>
  /// <returns></returns>
  public bool FloatsAreEqual(float float1, float float2)
  {
    return (double) Math.Abs(float1 - float2) < (double) RectangleElement.maxFloatMistake;
  }

  /// <summary> Сравнивает два float значения с некоторой точностью (Б=) </summary>
  /// <param name="float1"></param>
  /// <param name="float2"></param>
  /// <returns></returns>
  public bool FloatAreMoreOrEqual(float float1, float float2)
  {
    return (double) float1 + (double) RectangleElement.maxFloatMistakeDiv2 > (double) float2;
  }

  /// <summary> Сравнивает два float значения с некоторой точностью (Б=) </summary>
  /// <param name="float1"></param>
  /// <param name="float2"></param>
  /// <returns></returns>
  public bool FloatAreLessOrEqual(float float1, float float2)
  {
    return (double) float1 - (double) RectangleElement.maxFloatMistakeDiv2 < (double) float2;
  }

  /// <summary> Получить коллекцию ячеек таблицы, граничащих с данной слева </summary>
  /// <param name="includeOverSizedCells">
  /// Включить в коллекцию ячейки верхняя граница которой находиться выше верхней границы данной ячейки или
  /// нижняя граница которой находиться ниже нижней границы данной
  /// </param>
  /// <returns> Коллекция ячеек таблицы, граничащих с данной слева </returns>
  public Collection<RectangleElement> GetLeftCells(bool includeOverSizedCells)
  {
    Collection<RectangleElement> result = new Collection<RectangleElement>();
    if (this.TopLevelTable != this)
      this.FindCellsWithRightBorderCollisionInTable(ref result, (RectangleElement) this.TopLevelTable, this.Bounds, includeOverSizedCells);
    return result;
  }

  private void FindCellsWithRightBorderCollisionInTable(
    ref Collection<RectangleElement> result,
    RectangleElement scanRectangleElement,
    RectangleF searchBounds,
    bool includeOverSizedCells)
  {
    if (!this.IsVisibleNow || result == null || scanRectangleElement == null)
      return;
    if (scanRectangleElement.IsSingleCell)
    {
      if (!this.FloatsAreEqual(scanRectangleElement.Bounds.Right, searchBounds.Left))
        return;
      if (includeOverSizedCells)
      {
        if ((!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom)) && (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Bottom)))
          return;
        bool flag = true;
        if (this.FloatsAreEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || this.FloatsAreEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom))
          flag = false;
        if (!flag)
          return;
        result.Add(scanRectangleElement);
      }
      else
      {
        if (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom) || !this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Bottom))
          return;
        result.Add(scanRectangleElement);
      }
    }
    else
    {
      foreach (DocumentTreeNode node in scanRectangleElement.Nodes)
      {
        if (node is RectangleElement)
          this.FindCellsWithRightBorderCollisionInTable(ref result, node as RectangleElement, searchBounds, includeOverSizedCells);
      }
    }
  }

  /// <summary> Получить коллекцию ячеек таблицы, граничащих с данной сверху </summary>
  /// <param name="includeOverSizedCells">
  /// Включить в коллекцию ячейки левая граница которой находиться левее левой границы данной ячейки или
  /// правая граница которой находиться правее правой границы данной
  /// </param>
  /// <returns> Коллекция ячеек таблицы, граничащих с данной сверху </returns>
  public Collection<RectangleElement> GetTopCells(bool includeOverSizedCells)
  {
    Collection<RectangleElement> result = new Collection<RectangleElement>();
    if (this.TopLevelTable != this)
      this.FindCellsWithBottomBorderCollisionInTable(ref result, (RectangleElement) this.TopLevelTable, this.Bounds, includeOverSizedCells);
    return result;
  }

  private void FindCellsWithBottomBorderCollisionInTable(
    ref Collection<RectangleElement> result,
    RectangleElement scanRectangleElement,
    RectangleF searchBounds,
    bool includeOverSizedCells)
  {
    if (!this.IsVisibleNow || result == null || scanRectangleElement == null)
      return;
    if (scanRectangleElement.IsSingleCell)
    {
      if (!this.FloatsAreEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top))
        return;
      if (includeOverSizedCells)
      {
        if ((!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Right)) && (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Right)))
          return;
        bool flag = true;
        if (this.FloatsAreEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || this.FloatsAreEqual(scanRectangleElement.Bounds.Left, searchBounds.Right))
          flag = false;
        if (!flag)
          return;
        result.Add(scanRectangleElement);
      }
      else
      {
        if (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Right) || !this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Right))
          return;
        result.Add(scanRectangleElement);
      }
    }
    else
    {
      foreach (DocumentTreeNode node in scanRectangleElement.Nodes)
      {
        if (node is RectangleElement)
          this.FindCellsWithBottomBorderCollisionInTable(ref result, node as RectangleElement, searchBounds, includeOverSizedCells);
      }
    }
  }

  /// <summary> Получить коллекцию ячеек таблицы, граничащих с данной справа </summary>
  /// <param name="includeOverSizedCells">
  /// Включить в коллекцию ячейки верхняя граница которой находиться выше верхней границы данной ячейки или
  /// нижняя граница которой находиться ниже нижней границы данной
  /// </param>
  /// <returns> Коллекция ячеек таблицы, граничащих с данной справа </returns>
  public Collection<RectangleElement> GetRightCells(bool includeOverSizedCells)
  {
    Collection<RectangleElement> result = new Collection<RectangleElement>();
    if (this.TopLevelTable != this)
      this.FindCellsWithLeftBorderCollisionInTable(ref result, (RectangleElement) this.TopLevelTable, this.Bounds, includeOverSizedCells);
    return result;
  }

  private void FindCellsWithLeftBorderCollisionInTable(
    ref Collection<RectangleElement> result,
    RectangleElement scanRectangleElement,
    RectangleF searchBounds,
    bool includeOverSizedCells)
  {
    if (!this.IsVisibleNow || result == null || scanRectangleElement == null)
      return;
    if (scanRectangleElement.IsSingleCell)
    {
      if (!this.FloatsAreEqual(scanRectangleElement.Bounds.Left, searchBounds.Right))
        return;
      if (includeOverSizedCells)
      {
        if ((!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom)) && (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Bottom)))
          return;
        bool flag = true;
        if (this.FloatsAreEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || this.FloatsAreEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom))
          flag = false;
        if (!flag)
          return;
        result.Add(scanRectangleElement);
      }
      else
      {
        if (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom) || !this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Top) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Bottom, searchBounds.Bottom))
          return;
        result.Add(scanRectangleElement);
      }
    }
    else
    {
      foreach (DocumentTreeNode node in scanRectangleElement.Nodes)
      {
        if (node is RectangleElement)
          this.FindCellsWithLeftBorderCollisionInTable(ref result, node as RectangleElement, searchBounds, includeOverSizedCells);
      }
    }
  }

  /// <summary> Получить коллекцию ячеек таблицы, граничащих с данной снизу </summary>
  /// <param name="includeOverSizedCells">
  /// Включить в коллекцию ячейки левая граница которой находиться левее левой границы данной ячейки или
  /// правая граница которой находиться правее правой границы данной
  /// </param>
  /// <returns> Коллекция ячеек таблицы, граничащих с данной снизу </returns>
  public Collection<RectangleElement> GetBottomCells(bool includeOverSizedCells)
  {
    Collection<RectangleElement> result = new Collection<RectangleElement>();
    if (this.TopLevelTable != this)
      this.FindCellsWithTopBorderCollisionInTable(ref result, (RectangleElement) this.TopLevelTable, this.Bounds, includeOverSizedCells);
    return result;
  }

  private void FindCellsWithTopBorderCollisionInTable(
    ref Collection<RectangleElement> result,
    RectangleElement scanRectangleElement,
    RectangleF searchBounds,
    bool includeOverSizedCells)
  {
    if (!this.IsVisibleNow || result == null || scanRectangleElement == null)
      return;
    if (scanRectangleElement.IsSingleCell)
    {
      if (!this.FloatsAreEqual(scanRectangleElement.Bounds.Top, searchBounds.Bottom))
        return;
      if (includeOverSizedCells)
      {
        if ((!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Right)) && (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Right)) || this.FloatsAreEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || this.FloatsAreEqual(scanRectangleElement.Bounds.Left, searchBounds.Right))
          return;
        result.Add(scanRectangleElement);
      }
      else
      {
        if (!this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Left, searchBounds.Right) || !this.FloatAreMoreOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Left) || !this.FloatAreLessOrEqual(scanRectangleElement.Bounds.Right, searchBounds.Right))
          return;
        result.Add(scanRectangleElement);
      }
    }
    else
    {
      foreach (DocumentTreeNode node in scanRectangleElement.Nodes)
      {
        if (node is RectangleElement)
          this.FindCellsWithTopBorderCollisionInTable(ref result, node as RectangleElement, searchBounds, includeOverSizedCells);
      }
    }
  }

  /// <summary>Метод вызывается при удалении ветки, в которой находится этот узел</summary>
  protected override void OnBranchRemoved(Removed_EventArgs e)
  {
    base.OnBranchRemoved(e);
    if (this.IsVirtualNode || e.RemovedByShift)
      return;
    this.SetNextCell((RectangleElement) null);
    this.SetPrevCell((RectangleElement) null);
  }

  private DocumentTreeNode FindNearestNodeByTemplateFromFixedTableOnPrevPage(
    DocumentTreeNode nodeTemplate)
  {
    if (nodeTemplate == null)
      throw new ArgumentNullException(nameof (nodeTemplate));
    if (!(nodeTemplate is RectangleElement rectangleElement))
      return (DocumentTreeNode) null;
    if (nodeTemplate.connectionList == null || nodeTemplate.connectionList.Count == 0)
      return (DocumentTreeNode) null;
    DocumentTreeNode fixedTableOnPrevPage = (DocumentTreeNode) null;
    TableData topLevelTable = rectangleElement.TopLevelTable;
    if (topLevelTable != null && !topLevelTable.IsPageFlow)
    {
      PageData page = topLevelTable.Page;
      PageData currPageParent = this.Page;
      while (currPageParent != null && currPageParent.Template != page)
        currPageParent = ImDocumentData.GetPrevPage((DocumentTreeNode) currPageParent, currPageParent.Index, true);
      if (currPageParent != null)
      {
        for (int index = 0; index < nodeTemplate.connectionList.Count; ++index)
        {
          if (nodeTemplate.connectionList[index].OwnerPage == currPageParent)
          {
            fixedTableOnPrevPage = nodeTemplate.connectionList[index].OwnerNode;
            break;
          }
        }
      }
    }
    return fixedTableOnPrevPage;
  }

  /// <summary>Получить индекс столбца в сетке для этой ячейки</summary>
  /// <returns>Индекс столбца</returns>
  public virtual int GetGridColumnIndex()
  {
    return this.GridPos == null || this.IsDefaultGridPos ? RectangleElement.defaultGridPos.GetGridColumnIndex(this) : this.GridPos.GetGridColumnIndex(this);
  }

  /// <summary>Получить индекс столбца сетки для данной ячейки</summary>
  /// <param name="prevCellNodeIndex">Идекс предыдущей известной ячейки в nodes</param>
  /// <param name="prevCellGridIndex">Идекс предыдущей известной ячейки в сетке</param>
  /// <returns>Индекс столбца в сетке</returns>
  internal virtual int GetGridColumnIndex(int prevCellNodeIndex, int prevCellGridIndex)
  {
    return this.gridPos == null || this.IsDefaultGridPos ? RectangleElement.defaultGridPos.GetGridColumnIndex(this, prevCellNodeIndex, prevCellGridIndex) : this.gridPos.GetGridColumnIndex(this, prevCellNodeIndex, prevCellGridIndex);
  }

  /// <summary>Получить индекс строки в сетке для этой ячейки</summary>
  /// <returns>Индекс строки</returns>
  public virtual int GetGridRowIndex()
  {
    return this.GridPos == null || this.IsDefaultGridPos ? RectangleElement.defaultGridPos.GetGridRowIndex(this) : this.GridPos.GetGridRowIndex(this);
  }

  /// <summary>Только для отладки</summary>
  [Category("Debug")]
  public int GridColIndex
  {
    [DebuggerStepThrough] get => this.GetGridColumnIndex();
  }

  /// <summary>Только для отладки</summary>
  [Category("Debug")]
  public int GridRowIndex
  {
    [DebuggerStepThrough] get => this.GetGridRowIndex();
  }

  /// <summary>Позиция в сетке. Если возвращает null, то нужно использовать defaultGridPos</summary>
  [System.ComponentModel.ReadOnly(true)]
  [Category("Debug")]
  public virtual TableGridPosition GridPos
  {
    [DebuggerStepThrough] get
    {
      if (this.gridPos != null)
        return this.gridPos;
      return this.Template is RectangleElement template ? template.GridPos : (TableGridPosition) null;
    }
    set
    {
      if (this.GridPos == value)
        return;
      this.gridPos = value;
    }
  }

  /// <summary>Положение в сетке по умолчанию</summary>
  [Category("Debug")]
  public bool IsDefaultGridPos
  {
    [DebuggerStepThrough] get => this.GridPos == null;
  }

  /// <summary>Создать пустую ячейку таблицы</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  /// <returns>Ячейка таблицы</returns>
  protected virtual RectangleElement CreateEmptySingleCell(
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (RectangleElement) new TextData(parent, bounds, visible);
  }

  /// <summary>Создать пустую таблицу</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  /// <returns>Таблица</returns>
  protected virtual TableData CreateEmptyTable(
    bool isColumn,
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return new TableData(isColumn, parent, bounds, visible);
  }

  /// <summary>Разбить ячейку</summary>
  /// <param name="rows">Количество строк</param>
  /// <param name="cols">Количесвто столбцов</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public TableData SplitCell(int rows, int cols, bool updateUI, bool updateLayout)
  {
    return this.SplitCell(rows, cols, false, updateUI, updateLayout);
  }

  /// <summary>Разбить ячейку</summary>
  /// <param name="rows">Количество строк</param>
  /// <param name="cols">Количесвто столбцов</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual TableData SplitCell(
    int rows,
    int cols,
    bool splitOne,
    bool updateUI,
    bool updateLayout)
  {
    if (this.parent == null)
      return (TableData) null;
    TableData parentCell = this.ParentCell;
    TableData tableData = this as TableData;
    try
    {
      if (tableData == null)
      {
        if (rows == 0 && parentCell != null && parentCell.IsRow && !splitOne)
          parentCell.SplitChildCell(this, rows, cols);
        else if (cols == 0 && parentCell != null && parentCell.IsColumn && !splitOne)
        {
          parentCell.SplitChildCell(this, rows, cols);
        }
        else
        {
          int index1 = this.Index;
          tableData = this.CreateEmptyTable(true, (DocumentTreeNode) null, this.ProperBounds, false);
          tableData.SetVisible(true, false, false, false, false, false);
          tableData.setBounds(this.Bounds);
          tableData.Id = this.Id;
          tableData.Name = this.Name;
          if (this.Borders != null)
            tableData.borders = this.Borders.Clone();
          if (this.gridPos != null)
            tableData.gridPos = this.gridPos.Clone();
          tableData.overrideFlags = this.overrideFlags & (OverrideFlags.Width | OverrideFlags.Height | OverrideFlags.Geometry | OverrideFlags.DefaultRowSize | OverrideFlags.TopBorder | OverrideFlags.BottomBorder | OverrideFlags.LeftBorder | OverrideFlags.RightBorder | OverrideFlags.MinHeight | OverrideFlags.MaxHeight | OverrideFlags.SkipBefore | OverrideFlags.SkipAfter | OverrideFlags.MinWidth | OverrideFlags.FromNewPage | OverrideFlags.KeepWithNext);
          tableData.overrideFlags2 = this.overrideFlags2 & (OverrideFlags2.ColumnWidth | OverrideFlags2.RowHeight | OverrideFlags2.ParentDefaultRowSize | OverrideFlags2.ColumnLeftBorder | OverrideFlags2.ColumnRightBorder | OverrideFlags2.ParentInnerHorizontalLine);
          tableData.overrideFlags3 = this.overrideFlags3 & (OverrideFlags3.InnerHorizontalLine | OverrideFlags3.RelativeHeight | OverrideFlags3.RelativeWidth);
          tableData.fromNewPage = this.fromNewPage;
          tableData.tryNotBreak = this.tryNotBreak;
          tableData.keepWithNext = this.keepWithNext;
          tableData.defaultRowSize = this.defaultRowSize;
          tableData.skipCellsBefore = this.skipCellsBefore;
          tableData.skipCellsAfter = this.skipCellsAfter;
          tableData.ignoreSkipOuterCells = this.ignoreSkipOuterCells;
          tableData.AssignNonSkipBeforeAtStartPage(this.NonSkipBeforeAtStartPage, false);
          tableData.maxHeight = this.maxHeight;
          tableData.minWidth = this.minWidth;
          tableData.AssignMinHeight(this.MinHeight, false, false, true);
          tableData.foreColor = this.foreColor;
          tableData.backColor = this.backColor;
          tableData.tableCellType = this.tableCellType;
          tableData.headerShowType = this.headerShowType;
          tableData.borderWidth = this.borderWidth;
          tableData.horzAlign = this.horzAlign;
          tableData.vertAlign = this.vertAlign;
          tableData.relativeWidth = this.relativeWidth;
          tableData.relativeHeight = this.relativeHeight;
          tableData.cellMargins = this.cellMargins;
          tableData.desiredPageNumber = this.desiredPageNumber;
          tableData.drawEllipse = this.drawEllipse;
          DocumentTreeNode parent = this.parent;
          this.Remove(false, false);
          int index2 = index1;
          TableData child = tableData;
          parent.InsertChildNode(index2, (DocumentTreeNode) child, false, true, false, false);
          tableData.SetVisible(true, false, false, false, true, false);
        }
      }
      tableData?.SplitCell(rows, cols, splitOne, false, false);
      if (tableData != null)
      {
        if (this is TextData)
        {
          List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
          tableData.FindNodes(typeof (TextData), foundNodes);
          foreach (DocumentTreeNode documentTreeNode in foundNodes)
          {
            if (documentTreeNode is TextData textData)
            {
              textData.SetParagraphFormat((this as TextData).ParagraphFormat.Clone(), false, false);
              textData.SetCharFormat((this as TextData).CharFormat.Clone(), false, false);
            }
          }
        }
      }
    }
    finally
    {
      if (tableData != null)
      {
        if (updateLayout)
        {
          tableData.UpdateLayout(updateUI);
        }
        else
        {
          this.ResetNeedUpdateLayoutFlag(true);
          if (updateUI)
            tableData.UpdateUIGeometry(true);
        }
      }
    }
    return tableData;
  }

  /// <summary>Для внутреннего пользования. Рассчитать ширину столбца сетки для ячейки (с учётом объединения ячеек)</summary>
  /// <param name="firstColIndex">Первый столбец соответствующий ячейке</param>
  /// <param name="newCellWidth">Общая ширина ячейки под которую подгоняется ширина столбца</param>
  /// <returns></returns>
  public float CalcGridColumnWidth(int firstColIndex, float newCellWidth)
  {
    TableData parentCell = this.ParentCell;
    if (parentCell == null || this.IsDefaultGridPos)
      return newCellWidth;
    List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
    TableGridPosition gridPos = this.GridPos;
    float num1 = 0.0f;
    int num2 = gridPos.SpanCount;
    if (firstColIndex + num2 > gridColumnsParams.Count)
      num2 = gridColumnsParams.Count - firstColIndex;
    for (int index = firstColIndex; index < firstColIndex + num2 - 1; ++index)
      num1 += gridColumnsParams[index].Size;
    float num3 = newCellWidth - num1;
    if ((double) num3 < 0.0)
      num3 = 0.0f;
    return num3;
  }

  /// <summary>Для внутреннего пользования. Только установка значения properBounds, чтобы можно было отловить через breakpoint</summary>
  public void setProperBounds(RectangleF value)
  {
    if (!(this.properBounds != value))
      return;
    this.properBounds = value;
  }

  /// <summary>Для внутреннего пользования. Только установка значения bounds, чтобы можно было отловить через breakpoint</summary>
  public void setBounds(RectangleF value)
  {
    if (!(this.bounds != value))
      return;
    this.bounds = value;
  }
}
