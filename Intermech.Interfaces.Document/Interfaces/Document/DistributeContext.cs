// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeContext
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для передачи контекста разбивки и наследуемых параметров</summary>
public class DistributeContext : CellContext
{
  /// <summary>Родительский контекст разбивки</summary>
  public DistributeContext ParentContext;
  /// <summary>Разбивать игнорируя флаг NeedUpdateLayoutFlag</summary>
  public bool Force;
  /// <summary>Новый размер элемента</summary>
  public SizeF NewSize;
  /// <summary>Максимальный размер элемента</summary>
  public SizeF MaxSize;
  /// <summary>Попытка не разбивать элемент в этом и внутренних элементах была неудачной</summary>
  public bool TryNotBreak_Failed;
  /// <summary>Размера страницы недостаточно для разбивки элемента</summary>
  public bool InsufficientPageSize;
  /// <summary>Параметры столбцов сетки</summary>
  public List<RowColParams> colParams;
  /// <summary>Параметры строк сетки</summary>
  public List<RowColParams> rowParams;
  /// <summary>Параметры столбца</summary>
  public RowColParams cellCollParams;
  /// <summary>Параметры строки</summary>
  public RowColParams cellRowParams;
  /// <summary>Пытаться не разбивать элемент</summary>
  public bool TryNotBreak;
  /// <summary>Элемент должен быть первым на странице</summary>
  public bool FirstOnPage = true;
  /// <summary>Элемент должен быть первым из данных на странице</summary>
  public bool FirstDataOnPage = true;
  /// <summary>Перед этой таблицей идёт блок KeepWithNext и он начинается с начала страницы</summary>
  public bool KeepWithNext_IsFirstDataOnPage = true;
  /// <summary>Может ли разбиваться таблица верхнего уровня</summary>
  public bool CanDistributeTopTable;
  /// <summary>Размер пропуска строк текущий момент. Нужен для слежением за изменениями пропусков при переносах</summary>
  public float SkipSizeAfter;
  /// <summary>Первый проход разбивки</summary>
  public bool FirstPass = true;
  /// <summary>Узел владелец контекста</summary>
  public DocumentTreeNode OwnerNode;
  public int HeaderCount = -1;
  private int _currentCellIndex;

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец контекста</param>
  /// <param name="newSize">Новый размер</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="canDistributeTopTable">Может ли разбиваться таблица верхнего уровня</param>
  /// <param name="force">Разбивать игнорируя флаг NeedUpdateLayoutFlag</param>
  /// <param name="firstPass">Первый проход разбивки</param>
  public DistributeContext(
    DocumentTreeNode ownerNode,
    SizeF newSize,
    SizeF maxSize,
    bool canDistributeTopTable,
    bool force,
    bool firstPass)
  {
    this.OwnerNode = ownerNode;
    this.NewSize = newSize;
    this.MaxSize = maxSize;
    this.CanDistributeTopTable = canDistributeTopTable;
    this.Force = force;
    this.ParentContext = (DistributeContext) null;
    this.FirstPass = firstPass;
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец контекста</param>
  /// <param name="newSize">Новый размер</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="isFirstCell">Является ли текущая ячейка первой ячейкой</param>
  /// <param name="isFirstDataCell">Является ли текущая ячейка первой ячейкой данных</param>
  /// <param name="parentContext">Контекст разбивки родительского элемента</param>
  public DistributeContext(
    DocumentTreeNode ownerNode,
    SizeF newSize,
    SizeF maxSize,
    bool isFirstCell,
    bool isFirstDataCell,
    DistributeContext parentContext)
  {
    this.OwnerNode = ownerNode;
    this.NewSize = newSize;
    this.MaxSize = maxSize;
    this.IsFixedSizeRow = parentContext.IsFixedSizeRow;
    this.RowSize = parentContext.RowSize;
    if (parentContext.Margins != null)
      this.Margins = parentContext.Margins.Clone();
    this.TryNotBreak = parentContext.TryNotBreak;
    this.FirstOnPage = isFirstCell && parentContext.FirstOnPage;
    this.FirstDataOnPage = isFirstDataCell && parentContext.FirstDataOnPage;
    this.KeepWithNext_IsFirstDataOnPage = parentContext.KeepWithNext_IsFirstDataOnPage;
    this.CanDistributeTopTable = parentContext.CanDistributeTopTable;
    this.Force = parentContext.Force;
    this.FirstPass = parentContext.FirstPass;
    this.MoveTailToFinalPage = parentContext.MoveTailToFinalPage;
    this.ParentContext = parentContext;
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец контекста</param>
  /// <param name="force">Разбивать игнорируя флаг NeedUpdateLayoutFlag</param>
  public DistributeContext(DocumentTreeNode ownerNode, bool force)
  {
    this.OwnerNode = ownerNode;
    this.Force = force;
  }

  /// <summary>Конструктор</summary>
  public DistributeContext()
  {
  }

  /// <summary>Вторая итерация разбивки для динамических заголовков</summary>
  public bool SecondDynamicHeaderIteration { get; set; }

  /// <summary>
  /// Текущие границы таблицы без пропусков, на основе размещённых в ней ячеек
  /// </summary>
  public RectangleF CalculatedForCellsProperBounds { get; internal set; }

  public RectangleF PrevCellBounds { get; set; }

  public RectangleElement FirstKeepWithNext { get; internal set; }

  public int LastCellFromBuffer { get; internal set; } = -1;

  public int CurrentCellIndex
  {
    get => this._currentCellIndex;
    internal set
    {
      if (this._currentCellIndex == value)
        return;
      this._currentCellIndex = value;
    }
  }

  public DistributeContext PrevCellContext { get; set; }

  internal DistributeContextStateInPositionAtCell GetCurrentPositionState()
  {
    return new DistributeContextStateInPositionAtCell()
    {
      CalculatedProperTableBounds = this.CalculatedForCellsProperBounds,
      PrevCellBounds = this.PrevCellBounds,
      FirstKeepWithNext = this.FirstKeepWithNext,
      LastCellFromBuffer = this.LastCellFromBuffer,
      CurrentCellIndex = this.CurrentCellIndex,
      PrevCellContext = this.PrevCellContext,
      FreeSpace = this.OwnerNode is TableData ownerNode ? ownerNode.FreeSpace : SizeF.Empty
    };
  }

  internal void SetCurrentPositionState(DistributeContextStateInPositionAtCell state)
  {
    this.CalculatedForCellsProperBounds = state.CalculatedProperTableBounds;
    this.PrevCellBounds = state.PrevCellBounds;
    this.FirstKeepWithNext = state.FirstKeepWithNext;
    this.LastCellFromBuffer = state.LastCellFromBuffer;
    this.CurrentCellIndex = state.CurrentCellIndex;
    this.PrevCellContext = state.PrevCellContext;
    if (!(this.OwnerNode is TableData ownerNode))
      return;
    ownerNode.FreeSpace = state.FreeSpace;
  }

  public bool DistributeResultIsNeedUpdateLayout
  {
    get
    {
      return this.VertDistributed != DistributeResult.All && this.VertDistributed != DistributeResult.Part;
    }
  }

  public DistributeResult VertDistributed { get; set; } = DistributeResult.All;

  public bool CanVerticalDistribute { get; internal set; }

  public bool CanVerticalSplit { get; internal set; }

  public TableData ParentCell { get; internal set; }

  public List<RowColParams> ColParams { get; internal set; }

  public List<RowColParams> RowsParams { get; internal set; }

  public RectangleF DistributeBounds { get; internal set; }

  public float MinRowSize { get; internal set; }

  public bool HeaderCellsIsAvailable { get; internal set; }

  public int DataCellCount { get; internal set; }

  internal CellWithPosition CurrentDynamicHeaderPosition { get; set; }

  internal int DynamicHeaderGroupRowCount { get; set; }

  public RectangleF DistributePropperBounds { get; internal set; }

  /// <summary>Специальный проход для перемещения части данных на последний лист, чтобы он не оставался пустым</summary>
  public bool MoveTailToFinalPage { get; internal set; }
}
