// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageElementUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Контейнер для управления ЭУ на макете</summary>
[TypeConverter(typeof (LocalizedExpandableObjectConverter))]
public class PageElementUI
{
  private Page page;
  protected int FocusRectanlgeLineWidth = 2;
  /// <summary>Позиция курсора при нажатии левой клавиши мыши</summary>
  protected Point _leftMouseDownPos = Point.Empty;
  /// <summary>Выбать элемент после щелчка мыши</summary>
  protected bool SelectElementAfterClick = true;
  /// <summary>Размеры области захвата</summary>
  public Size grabHandleSize = new Size(7, 7);
  private bool transparentForMouse;
  private PageElementUICollection pageElementUIs;
  private PageElementUI parent;
  protected Point mousePosition;
  /// <summary>Курсор режима перемещения элемента</summary>
  protected Cursor moveCursor = Cursors.SizeAll;
  private bool ctrlMove;
  /// <summary>Новые границы UI</summary>
  public Rectangle newBounds;
  private Rectangle bounds = new Rectangle(0, 0, 10, 20);
  /// <summary>Флаг режима перетаскивания</summary>
  private bool isMoving;
  /// <summary>Начальная точка перемещения</summary>
  protected Point startPoint = Point.Empty;
  /// <summary>Предыдущая точка перемещения</summary>
  protected Point prevPoint = Point.Empty;
  protected bool IsFirstStep;
  /// <summary>Элемент страницы</summary>
  protected PageElementNode element;
  private bool geometryChangingBlocked;
  protected bool isSelected;
  protected bool isActiveElement;
  protected int minGrabHandleWidth = 5;

  /// <summary>Конструктор</summary>
  public PageElementUI()
  {
    this.pageElementUIs = new PageElementUICollection(this);
    this.NewBounds = this.Bounds;
  }

  /// <summary>Родитель</summary>
  public PageElementUI Parent
  {
    [DebuggerStepThrough] get => this.parent;
    set
    {
      if (this.parent == value)
        return;
      if (value != null)
        value.PageElementUIs.Add(this);
      else
        this.parent.PageElementUIs.Remove(this);
    }
  }

  /// <summary>Вызывает событие ChildElementAdded</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnChildElementAdded(PageElementUI_EventArgs e)
  {
  }

  /// <summary>Вызывает событие ChildElementRemoved</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnChildElementRemoved(PageElementUI_EventArgs e)
  {
  }

  /// <summary>Вызывает событие ParentChanged</summary>
  protected virtual void OnParentChanged()
  {
  }

  /// <summary>Назначить значение свойству Parent</summary>
  /// <param name="value">Значение</param>
  internal virtual void AssignParent(PageElementUI value)
  {
    if (this.parent == value)
      return;
    this.parent = value;
    this.isSelected = this.parent != null && this.parent.isSelected;
    this.OnParentChanged();
  }

  /// <summary>Коллекция дочерних PageElementUI</summary>
  public PageElementUICollection PageElementUIs
  {
    [DebuggerStepThrough] get => this.pageElementUIs;
    set => this.pageElementUIs = value;
  }

  /// <summary>Контрол страницы</summary>
  public virtual PageControl PageControl
  {
    [DebuggerStepThrough] get => this.Page != null ? this.Page.PageControl : (PageControl) null;
  }

  /// <summary>Контрол документа</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.PageControl?.DocumentControl;
  }

  /// <summary>Перенести на передний план</summary>
  public virtual void BringToFront()
  {
    if (this.parent == null)
      return;
    this.parent.PageElementUIs.Exchange(this.parent.PageElementUIs.IndexOf(this), this.parent.PageElementUIs.Count - 1);
  }

  /// <summary>Перенести на задний план</summary>
  public virtual void BringToBack()
  {
    if (this.parent == null)
      return;
    this.parent.PageElementUIs.Exchange(this.parent.PageElementUIs.IndexOf(this), 0);
  }

  /// <summary>Собственно основной элемент макета с данными</summary>
  public virtual PageElementNode Element
  {
    [DebuggerStepThrough] get => this.element;
    set
    {
      if (this.element == value)
        return;
      this.element = value;
      if (this.element != null && !this.element.SuspendedRefreshUIFlag)
        this.Refresh();
      if (this.element != null || this.PageControl == null || this.PageControl.focusedElement != this)
        return;
      this.PageControl.focusedElement = (PageElementUI) null;
    }
  }

  public virtual Page Page
  {
    get
    {
      if (this.page != null)
        return this.page;
      return this.Element != null ? this.Element.Page as Page : (Page) null;
    }
    set => this.page = value;
  }

  /// <summary>Преобразовать экранные координаы в координаты страницы</summary>
  /// <param name="point">Точка в экранных координатах</param>
  /// <param name="snap">Испольовать привязку к узлам</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns>Точка в координатах страницы</returns>
  public virtual PointF PixelToWorld(Point point, bool snap, VisualNode excludeNode)
  {
    PageControl pageControl = this.PageControl;
    PointF point1;
    if (this.Page != null && this.Page.PageUI != null)
    {
      point1 = this.Page.PageUI.ConvertPixelToWorld(point);
      if (snap)
        point1 = this.Page.PageUI.SnapPoint(point1, excludeNode);
    }
    else
      point1 = UnitsConverter.PixelsToMm(point, this.DispayDpi);
    return point1;
  }

  /// <summary>Разрешение экрана для рассчета координат элементов управления</summary>
  public virtual PointF DispayDpi
  {
    [DebuggerStepThrough] get
    {
      return this.PageControl != null ? this.PageControl.DisplayDpi : new PointF(96f, 96f);
    }
  }

  /// <summary>Границы элемента управления</summary>
  public virtual Rectangle Bounds
  {
    [DebuggerStepThrough] get => this.bounds;
    set => this.bounds = value;
  }

  /// <summary>Обработчик события изменения положения точки</summary>
  /// <param name="startPoint">Точка с которой началось движение</param>
  /// <param name="delta">Смещение от этой точки</param>
  public virtual void ChangingPoint(Point startPoint, Point delta)
  {
  }

  /// <summary>Обновить геометрию</summary>
  public virtual void UpdateGeometry()
  {
  }

  /// <summary>Обновить геометрию элемента страницы</summary>
  public virtual void UpdateElementGeometry()
  {
  }

  /// <summary>Обновить свойство Bounds</summary>
  /// <param name="newBounds">Новое значение</param>
  public virtual void UpdateBounds(Rectangle newBounds)
  {
    this.NewBounds = newBounds;
    this.Bounds = newBounds;
  }

  /// <summary>Начать процесс перемещения элемента страницы</summary>
  protected virtual void BeginMoving(MouseEventArgs mouseArgs, Keys modifierKeys)
  {
    this.isMoving = true;
  }

  /// <summary>Завершить процесс перемещения элемента страницы</summary>
  protected virtual void EndMoving(
    MouseEventArgs mouseArgs,
    Keys modifierKeys,
    Point startPoint,
    Point delta)
  {
    this.isMoving = false;
  }

  /// <summary>Отменить процесс перемещения элемента страницы</summary>
  public virtual void CancelMoving(Point startPoint, bool erasePreview) => this.isMoving = false;

  /// <summary>Идет процесс перемещения элемента страницы</summary>
  public bool IsMoving
  {
    [DebuggerStepThrough] get => this.isMoving;
  }

  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="uiList">Список PageElementUI под заданной точкой</param>
  /// <param name="recursive">Опрашивать все дочерние PageElementUI</param>
  public virtual void GetPageElementUIAtPoint(
    Point point,
    List<PageElementUI> uiList,
    bool recursive)
  {
    if (this.element != null && !this.element.IsVisibleNow || !PageElementUI.PixelRectangle(this.Bounds).Contains(point))
      return;
    uiList.Add(this);
    if (!recursive || this.pageElementUIs == null)
      return;
    for (int index = this.pageElementUIs.Count - 1; index > -1; --index)
      this.pageElementUIs[index].GetPageElementUIAtPoint(point, uiList, recursive);
  }

  /// <summary>Элемент принадлежит ячейки с фиксированной структурой</summary>
  /// <returns></returns>
  internal bool HasFixedStructureParent()
  {
    if (this.element == null || !(this.element is RectangleElement))
      return false;
    bool flag = false;
    for (TableData parentCell = ((RectangleElement) this.element).ParentCell; parentCell != null && !flag; parentCell = parentCell.ParentCell)
      flag |= parentCell.IsFixedStructureArea;
    return flag;
  }

  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой на котором находится найденный PageElementUI</param>
  /// <param name="recursive">Опрашивать все дочерние PageElementUI</param>
  /// <returns>Найденный PageElementUI</returns>
  public virtual PageElementUI GetPageElementUIAtPoint(
    Point point,
    ref int layer,
    bool recursive,
    bool ignoreGrabHandle)
  {
    if (this.element != null && !this.element.IsVisibleNow)
      return (PageElementUI) null;
    PageElementUI elementUiAtPoint1 = (PageElementUI) null;
    Rectangle rectangle = PageElementUI.PixelRectangle(this.Bounds);
    if (!ignoreGrabHandle)
      rectangle = new Rectangle(rectangle.X - this.minGrabHandleWidth, rectangle.Y - this.minGrabHandleWidth, rectangle.Width + 2 * this.minGrabHandleWidth, rectangle.Height + 2 * this.minGrabHandleWidth);
    if (rectangle.Contains(point))
    {
      if (recursive && this.pageElementUIs != null)
      {
        int num = -1;
        for (int index = this.pageElementUIs.Count - 1; index > -1; --index)
        {
          PageElementUI elementUiAtPoint2 = this.pageElementUIs[index].GetPageElementUIAtPoint(point, ref layer, recursive, ignoreGrabHandle);
          if (elementUiAtPoint2 != null && layer > num)
            elementUiAtPoint1 = elementUiAtPoint2;
          num = layer;
        }
      }
      int num1 = 0;
      if (this.DocumentControl != null && this.DocumentControl.NodeInSelection((DocumentTreeNode) this.element))
        num1 = 20;
      if (this.HasFixedStructureParent())
        num1 += 10;
      if (!this.TransparentForMouse && layer < num1)
      {
        layer = num1;
        elementUiAtPoint1 = this;
      }
    }
    return elementUiAtPoint1;
  }

  /// <summary>Получить элементы страницы в заданном прямоугольнике</summary>
  /// <param name="rect">Прямоугольник</param>
  /// <param name="nodes">Возвращает элементы</param>
  /// <param name="containsOnly">Выбирать только те элементы, которые полностью попадают в прямоугольник</param>
  public virtual void GetPageElementsInRectangle(
    Rectangle rect,
    IList<DocumentTreeNode> nodes,
    bool containsOnly)
  {
    if (this.element == null || !this.element.IsVisibleNow)
      return;
    Rectangle rect1 = PageElementUI.PixelRectangle(this.Bounds);
    if (containsOnly)
    {
      if (!rect.Contains(rect1))
        return;
      nodes.Add((DocumentTreeNode) this.Element);
    }
    else
    {
      if (!rect.IntersectsWith(rect1))
        return;
      nodes.Add((DocumentTreeNode) this.Element);
    }
  }

  /// <summary>Изменение геометрии заблокировано</summary>
  public bool GeometryChangingBlocked
  {
    get
    {
      PageElementNode element = this.element;
      if (element != null && element.GeometryChangingBlocked || this.geometryChangingBlocked)
        return true;
      return this.DocumentControl != null && this.DocumentControl.ReadOnlyGeometry;
    }
  }

  /// <summary>Обрезать изменения координат</summary>
  /// <param name="oldBounds">Старые границы</param>
  /// <param name="newBounds">Новые границы</param>
  /// <param name="oldUI">Старые границы UI</param>
  /// <param name="newUI">Новые границы UI</param>
  /// <returns>Границы</returns>
  public RectangleF TrimChanges(
    RectangleF oldBounds,
    RectangleF newBounds,
    Rectangle oldUI,
    Rectangle newUI)
  {
    RectangleF rectangle = newBounds;
    if (this.Page != null && this.Page.PageUI != null)
      newBounds = this.Page.PageUI.SnapToGrid(rectangle);
    if (newUI.Width == oldUI.Width)
      newBounds.Width = oldBounds.Width;
    if (newUI.Height == oldUI.Height)
      newBounds.Height = oldBounds.Height;
    if (newUI.X == oldUI.X)
      newBounds.X = oldBounds.X;
    if (newUI.Y == oldUI.Y)
      newBounds.Y = oldBounds.Y;
    if (newUI.Bottom == oldUI.Bottom)
    {
      float num = oldBounds.Bottom - newBounds.Y;
      newBounds.Height = (float) Math.Round((double) num, 5);
    }
    if (newUI.Right == oldUI.Right)
    {
      float num = oldBounds.Right - newBounds.X;
      newBounds.Width = (float) Math.Round((double) num, 5);
    }
    return newBounds;
  }

  /// <summary>Перемещать с нажатой клавишей CTRL</summary>
  public bool CTRLMove
  {
    [DebuggerStepThrough] get => this.ctrlMove;
    set => this.ctrlMove = value;
  }

  /// <summary>Выбрать элемент</summary>
  /// <param name="modifierKeys">Нажатые клавиши</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  protected void SelectElement(
    Keys modifierKeys,
    bool inPlaceEditEnabled,
    Point cursorPosition,
    bool showFull,
    bool showLeftTop)
  {
    this.SelectElement((DocumentTreeNode) this.Element, modifierKeys, inPlaceEditEnabled, cursorPosition, showFull, showLeftTop);
  }

  /// <summary>
  /// Получить строку ячейки или реальные строки в виртуальной ячейке при необходимости
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  private List<DocumentTreeNode> GetRowFromCells(DocumentTreeNode element, Keys modifierKeys)
  {
    List<DocumentTreeNode> rowFromCells = new List<DocumentTreeNode>();
    if (this.DocumentControl.RowSelection && element is RectangleElement rectangleElement1)
    {
      if (rectangleElement1.IsVirtualNode && rectangleElement1.NodesCount != 0)
      {
        List<DocumentTreeNode> singleCells = rectangleElement1.GetSingleCells();
        bool flag = false;
        if (singleCells != null)
        {
          RectangleElement parentCell = (RectangleElement) (singleCells[0] as RectangleElement).ParentCell;
          if (singleCells.Count > 1)
          {
            for (int index = 1; index < singleCells.Count; ++index)
            {
              RectangleElement rectangleElement = singleCells[index] as RectangleElement;
              if (rectangleElement.ParentCell != null && rectangleElement.ParentCell != parentCell)
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            for (int index = 0; index < singleCells.Count; ++index)
            {
              if (singleCells[index] is RectangleElement rectangleElement && rectangleElement.ParentCell != null && !rowFromCells.Contains((DocumentTreeNode) rectangleElement.ParentCell))
                rowFromCells.Add((DocumentTreeNode) rectangleElement.ParentCell);
            }
          }
          else
            rowFromCells.Add(element);
        }
      }
      else if (rectangleElement1.ParentCell != null && modifierKeys == Keys.Control)
        rowFromCells.Add((DocumentTreeNode) rectangleElement1.ParentCell);
      else
        rowFromCells.Add((DocumentTreeNode) rectangleElement1);
    }
    else if (element != null)
      rowFromCells.Add(element);
    return rowFromCells;
  }

  /// <summary>Выбрать элемент</summary>
  /// <param name="element">Элемент, который нужно выбрать</param>
  /// <param name="modifierKeys">Нажатые клавиши</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  protected virtual void SelectElement(
    DocumentTreeNode element,
    Keys modifierKeys,
    bool inPlaceEditEnabled,
    Point cursorPosition,
    bool showFull,
    bool showLeftTop)
  {
    if (Control.MouseButtons != MouseButtons.None && this.PageControl != null)
      this.PageControl.IsMouseDownSelecting = true;
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    documentTreeNodeList.Add(element);
    if (this.DocumentControl == null)
      return;
    bool flag = true;
    if (modifierKeys == Keys.Control)
    {
      if (this.DocumentControl.SelectedNodes != null && this.DocumentControl.SelectedNodes.Count == 1 && this.DocumentControl.OnRowSelection(this.DocumentControl.SelectedNodes) && this.DocumentControl.SelectedNodes[0] is RectangleElement selectedNode && selectedNode.ParentCell != null && selectedNode.IsSingleCell)
      {
        this.DocumentControl.ToggleNodeSelection((DocumentTreeNode) selectedNode, inPlaceEditEnabled, cursorPosition);
        this.DocumentControl.ToggleNodeSelection((DocumentTreeNode) selectedNode.ParentCell, inPlaceEditEnabled, cursorPosition);
      }
      if (this.DocumentControl.OnRowSelection(new List<DocumentTreeNode>()
      {
        element
      }) && element is RectangleElement)
      {
        RectangleElement rectangleElement = element as RectangleElement;
        if (rectangleElement.IsSingleCell && rectangleElement.IsTableCell)
        {
          RectangleElement ownerRow = (RectangleElement) rectangleElement.OwnerRow;
          documentTreeNodeList.Remove(element);
          documentTreeNodeList.Add((DocumentTreeNode) ownerRow);
        }
      }
      this.DocumentControl.ToggleNodesSelection((IList<DocumentTreeNode>) documentTreeNodeList, inPlaceEditEnabled, cursorPosition);
      flag = false;
    }
    if (!flag)
      return;
    this.DocumentControl.SetSelection(documentTreeNodeList, inPlaceEditEnabled, cursorPosition, showFull, showLeftTop);
  }

  /// <summary>Выбрать элемент</summary>
  /// <param name="elements">Элементы которые нужно выделить</param>
  /// <param name="modifierKeys">Нажатые клавиши</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="toggleSelection">Переключать выделение</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  protected virtual void SelectElements(
    List<DocumentTreeNode> elements,
    Keys modifierKeys,
    bool inPlaceEditEnabled,
    bool toggleSelection,
    Point cursorPosition,
    bool showFull,
    bool showLeftTop)
  {
    if (elements == null || this.DocumentControl == null)
      return;
    if (modifierKeys == Keys.Control)
    {
      if (toggleSelection)
        this.DocumentControl.ToggleNodesSelection((IList<DocumentTreeNode>) elements, inPlaceEditEnabled, cursorPosition);
      else
        this.DocumentControl.AddNodesToSelection((IList<DocumentTreeNode>) elements, inPlaceEditEnabled, cursorPosition);
    }
    else
      this.DocumentControl.SetSelection(elements, inPlaceEditEnabled, cursorPosition, showFull, showLeftTop);
  }

  /// <summary>Можно ли начать перемещение элемента</summary>
  /// <param name="point">Начальная точка</param>
  /// <returns>Можно ли начать перемещение элемента</returns>
  protected virtual bool CanBeginMoving(Point point) => !this.GeometryChangingBlocked;

  internal Point ControlCoorToPageCoor(Control control, Point point)
  {
    return this.PageControl != null ? this.PageControl.PointToClient(control.PointToScreen(point)) : point;
  }

  /// <summary>Размеры областей захвата</summary>
  public Size GrabHandleSize
  {
    [DebuggerStepThrough] get => this.grabHandleSize;
    set => this.grabHandleSize = value;
  }

  /// <summary>Активны ли области захвата</summary>
  public bool GrabHandlesActive
  {
    [DebuggerStepThrough] get
    {
      return !this.GeometryChangingBlocked && this.element != null && this.IsVisibleElementAndParents && this.IsActiveElement;
    }
  }

  /// <summary>Нарисовать области захвата</summary>
  /// <param name="g">Graphics</param>
  protected virtual void PaintGrabHandles(Graphics g)
  {
  }

  /// <summary>Нарисовать область захвата</summary>
  /// <param name="g">Graphics</param>
  /// <param name="point">Точка которую контролирует область</param>
  /// <param name="enabled">Включена ли область</param>
  protected virtual void PaintGrabHandle(Graphics g, Point point, bool enabled)
  {
    Rectangle rectangle = new Rectangle(point, this.GrabHandleSize);
    ControlPaint.DrawGrabHandle(g, rectangle, true, enabled);
  }

  public virtual void DrawFocusedRectangle(Graphics g)
  {
    Pen pen1 = new Pen(Color.White, (float) this.FocusRectanlgeLineWidth);
    Pen pen2 = new Pen(Color.DodgerBlue, (float) this.FocusRectanlgeLineWidth);
    Graphics graphics1 = g;
    Pen pen3 = pen1;
    Rectangle bounds1 = this.Bounds;
    int x1 = bounds1.X - this.FocusRectanlgeLineWidth;
    bounds1 = this.Bounds;
    int y1 = bounds1.Y - this.FocusRectanlgeLineWidth;
    bounds1 = this.Bounds;
    int width1 = bounds1.Width + 2 * this.FocusRectanlgeLineWidth + 1;
    bounds1 = this.Bounds;
    int height1 = bounds1.Height + 2 * this.FocusRectanlgeLineWidth + 1;
    graphics1.DrawRectangle(pen3, x1, y1, width1, height1);
    Graphics graphics2 = g;
    Pen pen4 = pen2;
    Rectangle bounds2 = this.Bounds;
    int x2 = bounds2.X - this.FocusRectanlgeLineWidth;
    bounds2 = this.Bounds;
    int y2 = bounds2.Y - this.FocusRectanlgeLineWidth;
    bounds2 = this.Bounds;
    int width2 = bounds2.Width + 2 * this.FocusRectanlgeLineWidth + 1;
    bounds2 = this.Bounds;
    int height2 = bounds2.Height + 2 * this.FocusRectanlgeLineWidth + 1;
    graphics2.DrawRectangle(pen4, x2, y2, width2, height2);
  }

  public virtual void EraseNewGeometryPreview(bool update)
  {
  }

  public virtual void DrawNewGeometryPreview(Graphics g)
  {
  }

  /// <summary>Элемент и его родители видны</summary>
  public bool IsVisibleElementAndParents
  {
    [DebuggerStepThrough] get
    {
      VisualNode visualNode = (VisualNode) this.Element;
      if (visualNode == null)
        return false;
      while (visualNode != null && visualNode.IsVisibleNow)
        visualNode = visualNode.Parent as VisualNode;
      return visualNode == null || visualNode.IsVisibleNow;
    }
  }

  public bool IsSelected
  {
    [DebuggerStepThrough] get => this.isSelected;
    set => this.SetSelected(value, false);
  }

  public virtual void SetSelected(bool value, bool invalidate)
  {
    if (this.isSelected != value)
    {
      this.isSelected = value;
      if (invalidate)
        this.InvalidateUI();
    }
    if (this.pageElementUIs == null)
      return;
    for (int index = 0; index < this.pageElementUIs.Count; ++index)
      this.pageElementUIs[index].SetSelected(value, false);
  }

  /// <summary>Этот элемент активен</summary>
  public bool IsActiveElement
  {
    [DebuggerStepThrough] get => this.isActiveElement;
    set => this.SetIsActiveElement(value);
  }

  public void SetIsActiveElement(bool value)
  {
    if (this.isActiveElement == value)
      return;
    this.isActiveElement = value;
    if (this.element == null)
      return;
    this.DocumentControl?.SetActiveElement((DocumentTreeNode) this.element, false, Point.Empty);
  }

  /// <summary>Загрузить курсор из ресурса</summary>
  /// <param name="resourceName">Имя ресурса</param>
  /// <returns>Курсор</returns>
  protected Cursor LoadCursorFromResurces(string resourceName)
  {
    Cursor cursor = (Cursor) null;
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
    {
      if (manifestResourceStream != null)
        cursor = new Cursor(manifestResourceStream);
    }
    return cursor;
  }

  /// <summary>Этот элемент прозрачен для мыши</summary>
  public bool TransparentForMouse
  {
    [DebuggerStepThrough] get => this.transparentForMouse;
    set => this.transparentForMouse = value;
  }

  /// <summary>Обновить инерфейс пользователя</summary>
  public void Refresh()
  {
    if (this.PageControl == null || (this.element == null || !this.element.SuspendedRefreshUIFlag ? (this.PageControl.Document == null ? 1 : (!this.PageControl.Document.SuspendedRefreshUIFlag ? 1 : 0)) : 0) == 0)
      return;
    this.PageControl.Refresh();
  }

  /// <summary>Обявить недействительной заданную область интерфейса пользователя</summary>
  /// <param name="clipRec">Прямоугольная область</param>
  public virtual void InvalidateUI(Rectangle clipRec)
  {
    if (this.PageControl == null)
      return;
    this.PageControl.InvokeInvalidate(PageElementUI.PixelRectangle(clipRec), true);
  }

  /// <summary>Обявить недействительной всю область интерфейса пользователя</summary>
  public virtual void InvalidateUI()
  {
  }

  public static Cursor CopyCursor
  {
    get
    {
      using (Stream manifestResourceStream = typeof (PageElementUI).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.CopyCursor.cur"))
        return manifestResourceStream != null ? new Cursor(manifestResourceStream) : Cursors.Default;
    }
  }

  /// <summary>Получить курсор для заданной точки</summary>
  /// <param name="point">Точка</param>
  /// <returns>Курсор</returns>
  public virtual Cursor GetCursor(Point point)
  {
    if (Control.ModifierKeys == Keys.Control && this.IsMoving)
      return PageElementUI.CopyCursor;
    Rectangle rectangle = PageElementUI.PixelRectangle(this.Bounds);
    return !this.GeometryChangingBlocked && rectangle.Contains(point) ? this.moveCursor : Cursors.Default;
  }

  /// <summary>Переводит прямоугольник из непрерывных единиц измерения в пиксели,
  /// для правильного определения вхождения точки в прямоугольник на экране.</summary>
  protected static Rectangle PixelRectangle(Rectangle rectangle)
  {
    ++rectangle.Width;
    ++rectangle.Height;
    return rectangle;
  }

  /// <summary>Можно активировать редактирование по месту</summary>
  internal virtual bool CanActivateInPlaceEditor
  {
    [DebuggerStepThrough] get => this.element != null && this.element.CanActivateInPlaceEditor;
  }

  /// <summary>Редактор для редактирования по месту активен</summary>
  internal virtual bool InPlaceEditorActive
  {
    [DebuggerStepThrough] get => this.element != null && this.element.InPlaceEditorActive;
  }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  internal virtual void ActivateInPlaceEditor(MouseEventArgs mouseEventArgs)
  {
    if (!(this.element is IPageElementWithInterface element))
      return;
    element.ActivateInPlaceEditor(this, mouseEventArgs);
  }

  /// <summary>Деактивировать радактор на месте</summary>
  internal virtual void DeactivateInPlaceEditor()
  {
    if (this.element == null)
      return;
    this.element.DeactivateInPlaceEditor();
  }

  /// <summary>Получить фокус для элемента управления</summary>
  public virtual void FocusUI()
  {
    PageControl pageControl = this.PageControl;
    if (pageControl == null)
      return;
    pageControl.focusedElement = this;
  }

  internal virtual void OnValidating(CancelEventArgs e)
  {
    if (!(this.element is TextBoxElement element) || !element.InPlaceEditorActive || element.TextBox == null)
      return;
    element.TextBox.Editor_Validating((object) this, e);
  }

  internal virtual void PreprocessControlMouseWheel(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    cancelEventArgs.Cancel = true;
  }

  internal virtual void OnMouseWheel(MouseEventArgs e)
  {
  }

  internal virtual void PreprocessControlMouseEnter(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnMouseEnter(EventArgs e)
  {
  }

  internal virtual void PreprocessControlMouseLeave(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnMouseLeave(EventArgs e)
  {
  }

  internal virtual void PreprocessControlMouseMove(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  /// <summary>Вызывает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnMouseMove(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    this.PageControl.GetPageAtPoint(e.Location);
    this.mousePosition = new Point(e.X, e.Y);
    Point point = new Point(e.X, e.Y);
    Keys modifierKeys = Control.ModifierKeys;
    if (point == this.prevPoint)
      return;
    if (e.Button == MouseButtons.Left && (!this.ctrlMove || modifierKeys == Keys.Control))
    {
      if (!this.IsMoving && (this.CanBeginMoving(this.startPoint) || this.PageControl != null && this.PageControl.IsPasting))
        this.BeginMoving(e, modifierKeys);
      if (this.IsMoving)
      {
        Point delta = new Point(point.X - this.startPoint.X, point.Y - this.startPoint.Y);
        if (!this.GeometryChangingBlocked)
          this.ChangingPoint(this.startPoint, delta);
      }
    }
    this.prevPoint = point;
  }

  internal virtual void PreprocessControlMouseDown(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  /// <summary>Вызывает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnMouseDown(MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left)
    {
      this.leftMouseDownPos = new Point(e.X, e.Y);
      if (!this.ctrlMove || Control.ModifierKeys == Keys.Control)
      {
        this.startPoint = this.leftMouseDownPos;
        this.prevPoint = this.startPoint;
      }
    }
    if (e.Button == MouseButtons.Right && this.IsMoving)
    {
      Point delta = new Point(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
      this.EndMoving(e, Control.ModifierKeys, this.startPoint, delta);
    }
    Keys modifierKeys = Control.ModifierKeys;
    if (e.Button != MouseButtons.Left || this.ctrlMove && modifierKeys != Keys.Control)
      return;
    if (!this.IsMoving && !this.MoveAll && (this.CanBeginMoving(this.startPoint) || this.PageControl != null && this.PageControl.IsPasting))
      this.BeginMoving(e, modifierKeys);
    if (!this.IsMoving)
      return;
    this.IsFirstStep = true;
    Point point = new Point(e.X, e.Y);
    Point delta1 = new Point(point.X - this.startPoint.X, point.Y - this.startPoint.Y);
    if (this.GeometryChangingBlocked)
      return;
    this.ChangingPoint(this.startPoint, delta1);
  }

  /// <summary>Перемещается ли весь элемент</summary>
  protected virtual bool MoveAll => true;

  internal virtual void PreprocessControlMouseUp(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  /// <summary>Вызывает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnMouseUp(MouseEventArgs e)
  {
    if (this.PageControl != null)
      this.PageControl.IsMouseDownSelecting = false;
    if (e.Button == MouseButtons.Left && this.IsMoving)
    {
      Point delta = new Point(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
      this.EndMoving(e, Control.ModifierKeys, this.startPoint, delta);
    }
    if (this.DocumentControl == null || this.DocumentControl.DocumentManager == null)
      return;
    this.DocumentControl.DocumentManager.SetMessageText("");
  }

  internal virtual void PreprocessControlEnter(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnEnter(EventArgs e)
  {
  }

  internal virtual void PreprocessControlLeave(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnLeave(EventArgs e)
  {
    if (this.Element == null || !this.Element.IsInPlaceEditor || !this.Element.InPlaceEditorActive)
      return;
    this.Element.DeactivateInPlaceEditor();
  }

  internal virtual void PreprocessControlClick(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  /// <summary>Вызывает событие Click</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnClick(EventArgs e)
  {
  }

  internal virtual void PreprocessControlDoubleClick(
    object sender,
    EventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    if (this.element == null)
      return;
    if (this.element.CanCallEditor && (this.element.ReadOnlyNow || !this.InPlaceEditorActive))
    {
      this.element.CallEditor();
    }
    else
    {
      if (this.DocumentControl == null || this.DocumentControl.DocumentManager == null || this.DocumentControl.DocumentManager.CommandManager == null)
        return;
      ICommandState command = this.DocumentControl.DocumentManager.CommandManager.FindCommand("CallEditor");
      if (command == null || !command.Enabled)
        return;
      this.DocumentControl.DocumentManager.CommandManager.Execute(command);
      cancelEventArgs.Cancel = true;
    }
  }

  /// <summary>Вызывает событие DoubleClick</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnDoubleClick(EventArgs e)
  {
    if (this.element == null)
      return;
    if (this.element.CanCallEditor && (this.element.ReadOnlyNow || !this.InPlaceEditorActive))
    {
      this.element.CallEditor();
    }
    else
    {
      if (!this.element.Id.StartsWith("Лист") || this.PerformDocumentCommand("AVS.ChangePageNumberingStyle"))
        return;
      this.PerformDocumentCommand("DocEditor.ChangePageNumberingStyle");
    }
  }

  /// <summary>Вызывает событие Paint</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnPaint(PaintEventArgs e) => this.PaintGrabHandles(e.Graphics);

  internal virtual void PreprocessControlKeyDown(
    object sender,
    KeyEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    if (e.KeyCode != Keys.Return || this.element == null || !this.element.CanCallEditor || !this.element.ReadOnlyNow && this.InPlaceEditorActive)
      return;
    this.element.CallEditor();
  }

  internal virtual void OnKeyDown(KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return || this.element == null || !this.element.CanCallEditor || !this.element.ReadOnlyNow && this.InPlaceEditorActive)
      return;
    this.element.CallEditor();
  }

  internal virtual void PreprocessControlKeyUp(
    object sender,
    KeyEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnKeyUp(KeyEventArgs e)
  {
  }

  internal virtual void PreprocessControlKeyPress(
    object sender,
    KeyPressEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
  }

  internal virtual void OnKeyPress(KeyPressEventArgs ev)
  {
  }

  internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (this.element == null)
      return false;
    switch (keyData)
    {
      case Keys.Return:
        if ((this.element.ReadOnlyNow || !this.InPlaceEditorActive) && this.element.CanCallEditor && this.PageControl.FocusedElement == this)
        {
          this.element.CallEditor();
          return true;
        }
        break;
      case Keys.Escape:
        if (this.InPlaceEditorActive)
        {
          CancelEventArgs e = new CancelEventArgs();
          this.OnValidating(e);
          if (e.Cancel)
            return true;
          this.DeactivateInPlaceEditor();
          return true;
        }
        if (this.IsMoving)
        {
          this.CancelMoving(this.mousePosition, true);
          return true;
        }
        break;
      case Keys.Delete:
        if (this.InPlaceEditorActive)
          return false;
        if (this.PerformDocumentCommand("Delete") || (this.DocumentControl == null ? 0 : (this.DocumentControl.ReadOnly ? 1 : 0)) != 0 || !this.element.CanRemove())
          return true;
        this.element.Remove(true, true);
        return true;
    }
    if (this.element is TextBoxElement && this.InPlaceEditorActive && keyData == Keys.Back && this.element is TextBoxElement element1)
      element1.TextBox.EmptyEditorBackspace();
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Left | Keys.Shift:
        if (this.Element is TextBoxElement element2 && element2.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element2.TextBox;
          if (textBox != null && !textBox.CursorInFirstPosition)
            return false;
          break;
        }
        break;
      case Keys.Up:
      case Keys.Up | Keys.Shift:
        if (this.Element is TextBoxElement element3 && element3.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element3.TextBox;
          if (textBox != null && !textBox.CursorInFirstLine)
            return false;
          break;
        }
        break;
      case Keys.Right:
      case Keys.Right | Keys.Shift:
        if (this.Element is TextBoxElement element4 && element4.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element4.TextBox;
          if (textBox != null && !textBox.CursorInEndPosition)
            return false;
          break;
        }
        break;
      case Keys.Down:
      case Keys.Down | Keys.Shift:
        if (this.Element is TextBoxElement element5 && element5.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element5.TextBox;
          if (textBox != null && !textBox.CursorInLastLine)
            return false;
          break;
        }
        break;
    }
    return this.parent != null && this.parent.ProcessCmdKey(ref msg, keyData);
  }

  private bool PerformDocumentCommand(string documentCommandName)
  {
    if (string.IsNullOrWhiteSpace(documentCommandName))
      return false;
    ICommandManager commandManager = this.DocumentControl?.DocumentManager?.CommandManager;
    ICommandState command = commandManager?.FindCommand(documentCommandName);
    if (command == null)
      return false;
    if (command.Enabled)
      commandManager.Execute(command);
    return true;
  }

  public override string ToString()
  {
    return this.Element != null ? $"{base.ToString()} {this.Element.ToString()}" : base.ToString();
  }

  internal Point leftMouseDownPos
  {
    get => this._leftMouseDownPos;
    set => this._leftMouseDownPos = value;
  }

  public Rectangle NewBounds
  {
    get => this.newBounds;
    set => this.newBounds = value;
  }

  internal Point StartPoint
  {
    get => this.startPoint;
    set => this.startPoint = value;
  }
}
