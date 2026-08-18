// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TableCellUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Интерфейс пользователя управления таблицей</summary>
public class TableCellUI : PageElementUI
{
  private static RectangleElement previewSelectionCells;
  private static Cursor oldEditorCursor;
  private static Cursor colSelectionCursor;
  private static Cursor rowSelectionCursor;
  private static Cursor cellSelectionCursor;
  private GrabHandleZone movingGrabHandleZone;
  private Cursor movingCursor = Cursors.SizeAll;
  private List<RectangleElement> resizingCells = new List<RectangleElement>();
  private int columnSelectionHeight = 5;
  private Rectangle previewBounds = Rectangle.Empty;
  private bool newBoundsPreviewDrawed;
  private RectangleF elementBounds;
  private GrabHandleZone currentGrabZone = GrabHandleZone.Right;

  /// <summary>Конструктор</summary>
  public TableCellUI() => this.CTRLMove = false;

  /// <summary>Перемещается ли весь элемент</summary>
  protected override bool MoveAll => false;

  /// <summary>Курсор выбора столбца</summary>
  public virtual Cursor ColumnSelectionCursor
  {
    [DebuggerStepThrough] get
    {
      if (TableCellUI.colSelectionCursor == (Cursor) null)
        TableCellUI.colSelectionCursor = this.LoadCursorFromResurces("Intermech.Document.Model.Resources.SelectColumn.cur");
      return TableCellUI.colSelectionCursor;
    }
  }

  /// <summary>Курсор выбора строки</summary>
  public virtual Cursor RowSelectionCursor
  {
    [DebuggerStepThrough] get
    {
      if (TableCellUI.rowSelectionCursor == (Cursor) null)
        TableCellUI.rowSelectionCursor = this.LoadCursorFromResurces("Intermech.Document.Model.Resources.SelectRow.cur");
      return TableCellUI.rowSelectionCursor;
    }
  }

  /// <summary>Курсор выбора ячейки</summary>
  public virtual Cursor CellSelectionCursor
  {
    [DebuggerStepThrough] get
    {
      if (TableCellUI.cellSelectionCursor == (Cursor) null)
        TableCellUI.cellSelectionCursor = this.LoadCursorFromResurces("Intermech.Document.Model.Resources.SelectCell.cur");
      return TableCellUI.cellSelectionCursor;
    }
  }

  /// <summary>Обявить недействительной всю область интерфейса пользователя</summary>
  public override void InvalidateUI()
  {
    Rectangle clipRec = this.Bounds;
    if (this.IsActiveElement)
    {
      clipRec = new Rectangle(clipRec.X - (this.FocusRectanlgeLineWidth + 1), clipRec.Y - (this.FocusRectanlgeLineWidth + 1), clipRec.Width + 2 * (this.FocusRectanlgeLineWidth + 1), clipRec.Height + 2 * (this.FocusRectanlgeLineWidth + 1));
    }
    else
    {
      ++clipRec.Width;
      ++clipRec.Height;
    }
    this.InvalidateUI(clipRec);
  }

  /// <summary>Вызвает событие Paint</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    DocumentControl documentControl = this.DocumentControl;
    if (this.Element != null && !this.IsVisibleElementAndParents || !this.IsActiveElement || documentControl != null && (documentControl.ActivePage == null || this.PageControl != documentControl.PageControl))
      return;
    this.DrawFocusedRectangle(e.Graphics);
  }

  /// <summary>Получить курсор для заданной точки</summary>
  /// <param name="point">Точка</param>
  /// <returns>Курсор</returns>
  public override Cursor GetCursor(Point point)
  {
    GrabHandleZone grabZone = GrabHandleZone.Center;
    Cursor cursor = Cursors.Default;
    if (this.IsMoving)
      cursor = this.movingCursor;
    else if (this.PageControl != null && this.PageControl.IsTableRowsSelecting)
      cursor = this.RowSelectionCursor;
    else if (this.PageControl != null && this.PageControl.IsTableColumnsSelecting)
      cursor = this.ColumnSelectionCursor;
    else if (this.PageControl != null && this.PageControl.IsTableCellsSelecting)
    {
      cursor = this.CellSelectionCursor;
    }
    else
    {
      Rectangle rectangle;
      if (this.RowSelectionEnabled)
      {
        rectangle = this.RowSelectionZone();
        if (rectangle.Contains(point))
        {
          cursor = this.RowSelectionCursor;
          goto label_22;
        }
      }
      bool grabHandleZone;
      if ((grabHandleZone = this.GetGrabHandleZone(point, out grabZone)) && (grabZone != GrabHandleZone.Center || this.Element == null || this.Element is TableElement || !this.Element.CanActivateInPlaceEditor))
      {
        cursor = this.GetGrabHandleZoneCursor(grabZone);
      }
      else
      {
        if (this.CanSelectColumnCells())
        {
          rectangle = this.ColumnSelectionZone();
          if (rectangle.Contains(point))
          {
            cursor = this.ColumnSelectionCursor;
            goto label_22;
          }
        }
        if (this.CanSelectCell())
        {
          rectangle = this.CellSelectionZone();
          if (rectangle.Contains(point))
          {
            cursor = this.CellSelectionCursor;
            goto label_22;
          }
        }
        if (grabHandleZone)
        {
          cursor = this.GetGrabHandleZoneCursor(grabZone);
          if (grabZone == GrabHandleZone.Center && this.Element != null && !(this.Element is TableElement) && this.Element.CanActivateInPlaceEditor)
            cursor = Cursors.IBeam;
        }
      }
    }
label_22:
    return cursor;
  }

  /// <summary>Получить курсор курсор для заданной области захвата</summary>
  /// <param name="grabZone">Область захвата</param>
  /// <returns>Курсор для области захвата</returns>
  protected virtual Cursor GetGrabHandleZoneCursor(GrabHandleZone grabZone)
  {
    Cursor handleZoneCursor = Cursors.Default;
    switch (grabZone)
    {
      case GrabHandleZone.Center:
        handleZoneCursor = Cursors.Default;
        break;
      case GrabHandleZone.Top:
        handleZoneCursor = Cursors.SizeNS;
        break;
      case GrabHandleZone.Right:
        handleZoneCursor = Cursors.SizeWE;
        break;
      case GrabHandleZone.Bottom:
        handleZoneCursor = Cursors.SizeNS;
        break;
      case GrabHandleZone.Left:
        handleZoneCursor = Cursors.SizeWE;
        break;
    }
    return handleZoneCursor;
  }

  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой на котором находится найденный PageElementUI</param>
  /// <param name="recursive">Опрашивать все дочерние PageElementUI</param>
  /// <returns>Найденный PageElementUI</returns>
  public override PageElementUI GetPageElementUIAtPoint(
    Point point,
    ref int layer,
    bool recursive,
    bool ignoreGrabHandle)
  {
    if (this.element != null && !this.element.IsVisibleNow)
      return (PageElementUI) null;
    GrabHandleZone grabZone = GrabHandleZone.Center;
    PageElementUI elementUiAtPoint = (PageElementUI) null;
    int num = 0;
    if (this.DocumentControl != null && this.DocumentControl.NodeInSelection((DocumentTreeNode) this.element))
      num = 20;
    if (this.HasFixedStructureParent())
      num += 10;
    bool grabHandleZone = this.GetGrabHandleZone(point, out grabZone);
    if (!grabHandleZone || grabZone != GrabHandleZone.Right)
      elementUiAtPoint = base.GetPageElementUIAtPoint(point, ref layer, recursive, ignoreGrabHandle);
    if (layer < num + 5 & grabHandleZone && (grabZone == GrabHandleZone.Left || grabZone == GrabHandleZone.Right))
    {
      elementUiAtPoint = (PageElementUI) this;
      layer = num + 5;
    }
    if (layer < num + 4 && this.RowSelectionEnabled && this.RowSelectionZone().Contains(point))
    {
      layer = num + 4;
      elementUiAtPoint = (PageElementUI) this;
    }
    if (layer < num + 3 && this.ColumnSelectionZone().Contains(point) && this.CanSelectColumnCells())
    {
      layer = num + 3;
      elementUiAtPoint = (PageElementUI) this;
    }
    if (layer < num + 2 && this.CellSelectionZone().Contains(point) && this.CanSelectCell())
    {
      layer = num + 2;
      elementUiAtPoint = (PageElementUI) this;
    }
    if (layer < num + 1 & grabHandleZone)
    {
      if (grabZone == GrabHandleZone.Center)
      {
        if (layer < num)
        {
          elementUiAtPoint = (PageElementUI) this;
          layer = num;
        }
      }
      else
      {
        elementUiAtPoint = (PageElementUI) this;
        layer = num + 1;
      }
    }
    return elementUiAtPoint;
  }

  /// <summary>Получить строки пересекающиеся с прямоугольником</summary>
  /// <param name="tableUI">UI ячейки в которой ищем</param>
  /// <param name="rect">Прямоугольник в экранных координатах страницы</param>
  /// <param name="nodes">Найденные строки</param>
  internal void GetRowsInRectangle(
    PageElementUI tableUI,
    Rectangle rect,
    IList<DocumentTreeNode> nodes)
  {
    bool flag1 = false;
    Page page1 = this.PageControl.GetPageAtPoint(rect.Location);
    if (page1 == null && this.PageControl.FirstSelectedElem != null)
      page1 = this.PageControl.FirstSelectedElem.Page as Page;
    Page page2 = this.PageControl.GetPageAtPoint(new Point(rect.Right, rect.Bottom));
    if (page2 == null && this.PageControl.LastSelectedElem != null)
      page2 = this.PageControl.LastSelectedElem.Page as Page;
    if (page1 != page2 && page1 != null)
      flag1 = true;
    bool flag2 = !flag1 || page2 == null || page1 == null ? rect.Top < rect.Bottom : page2.Index >= page1.Index;
    while (tableUI != null)
    {
      int left = rect.Left;
      int right = rect.Right;
      int y1 = rect.Top;
      int y2 = rect.Bottom;
      Rectangle bounds;
      if (flag1 && tableUI.Page?.PageUI != null)
      {
        bounds = tableUI.Page.PageUI.Bounds;
        left = bounds.Left;
        bounds = tableUI.Page.PageUI.Bounds;
        right = bounds.Right;
      }
      if (flag1 && (page1 == tableUI.Element.Page & flag2 || page2 == tableUI.Element.Page && !flag2))
      {
        bounds = tableUI.Bounds;
        y2 = bounds.Bottom;
        if (!flag2)
          y1 = rect.Bottom;
      }
      if (flag1 && (page2 == tableUI.Element.Page & flag2 || page1 == tableUI.Element.Page && !flag2))
      {
        bounds = tableUI.Bounds;
        y1 = bounds.Top;
        if (!flag2)
          y2 = rect.Top;
      }
      if (flag1 && page2 != tableUI.Element.Page && page1 != tableUI.Element.Page)
      {
        bounds = tableUI.Bounds;
        y1 = bounds.Top;
        bounds = tableUI.Bounds;
        y2 = bounds.Bottom;
      }
      Rectangle rect1 = PageControl.NormalRectangle(new Point(left, y1), new Point(right, y2));
      if (rect1.Width == 0)
        rect1.Width = 1;
      if (tableUI.PageElementUIs != null)
      {
        for (int index = 0; index < tableUI.PageElementUIs.Count; ++index)
        {
          if (tableUI.PageElementUIs[index] is TableCellUI pageElementUi)
            pageElementUi.GetRowsInRectangle(rect1, nodes);
        }
      }
      if (!flag1 || tableUI.Element.Page == page2 || !(tableUI.Element is TableElement))
      {
        tableUI = (PageElementUI) null;
      }
      else
      {
        TableData element = tableUI.Element as TableData;
        TableData tableData = !flag2 ? element.PrevTable : element.NextTable;
        if (tableData == null)
        {
          tableUI = (PageElementUI) null;
        }
        else
        {
          (tableData.Page as Page).UpdateChildUIGeometry(true, false);
          tableUI = (PageElementUI) ((tableData as IPageElementWithInterface).PageUI as TableUI);
        }
      }
    }
    if (nodes == null || nodes.Count <= 0)
      return;
    this.PageControl.FirstSelectedElem = nodes[0] as RectangleElement;
    this.PageControl.LastSelectedElem = nodes[nodes.Count - 1] as RectangleElement;
  }

  /// <summary>Получить строки пересекающиеся с прямоугольником</summary>
  /// <param name="rect">Прямоугольник в экранных координатах страницы</param>
  /// <param name="nodes">Найденные строки</param>
  internal void GetRowsInRectangle(Rectangle rect, IList<DocumentTreeNode> nodes)
  {
    if (this.RowSelectionEnabled && this.Bounds.IntersectsWith(rect))
    {
      nodes.Add((DocumentTreeNode) this.Element);
    }
    else
    {
      if (this.PageElementUIs == null)
        return;
      for (int index = 0; index < this.PageElementUIs.Count; ++index)
      {
        if (this.PageElementUIs[index] is TableCellUI pageElementUi)
          pageElementUi.GetRowsInRectangle(rect, nodes);
      }
    }
  }

  /// <summary>Получить столбцы пересекающиеся с прямоугольником</summary>
  /// <param name="tableUI">UI всей таблицы</param>
  /// <param name="rect">Прямоугольник в экранных координатах страницы</param>
  /// <param name="nodes">Найденные столбцы</param>
  internal void GetColumnsInRectangle(
    TableUI tableUI,
    Rectangle rect,
    List<DocumentTreeNode> nodes)
  {
    if (tableUI.PageElementUIs == null)
      return;
    for (int index = 0; index < tableUI.PageElementUIs.Count; ++index)
    {
      if (tableUI.PageElementUIs[index] is TableCellUI pageElementUi)
        pageElementUi.GetColumnsInRectangle(rect, nodes);
    }
  }

  /// <summary>Получить столбцы пересекающиеся с прямоугольником</summary>
  /// <param name="rect">Прямоугольник в экранных координатах страницы</param>
  /// <param name="nodes">Найденные столбцы</param>
  internal void GetColumnsInRectangle(Rectangle rect, List<DocumentTreeNode> nodes)
  {
    if (this.CanSelectColumnCells() && this.Bounds.IntersectsWith(rect))
    {
      List<DocumentTreeNode> columnCells = this.GetColumnCells();
      if (columnCells == null || columnCells.Count <= 0)
        return;
      VirtualColumn virtualColumn = this.GetVirtualColumn((IList<DocumentTreeNode>) columnCells);
      if (virtualColumn != null)
        nodes.Add((DocumentTreeNode) virtualColumn);
      else
        nodes.AddRange((IEnumerable<DocumentTreeNode>) columnCells);
    }
    else
    {
      if (this.PageElementUIs == null)
        return;
      for (int index = 0; index < this.PageElementUIs.Count; ++index)
      {
        if (this.PageElementUIs[index] is TableCellUI pageElementUi)
          pageElementUi.GetColumnsInRectangle(rect, nodes);
      }
    }
  }

  /// <summary>Эта ячейка является единичной ячейкой без вложенных ячеек</summary>
  /// <returns>true, если ячейка является единичной</returns>
  internal bool IsSingleCell()
  {
    return !(this.Element is RectangleElement element) || element.IsSingleCell;
  }

  /// <summary>Получить ячейки пересекающиеся с прямоугольником</summary>
  /// <param name="tableUI">UI всей таблицы</param>
  /// <param name="rect">Прямоугольник в экранных координатах страницы левый верхний угол соответствует первой точке выделения, правый нижний последней</param>
  internal RectangleElement GetCellsInRectangle(
    TableUI tableUI,
    Rectangle rect,
    bool frameRowSelection)
  {
    if (tableUI.PageElementUIs == null || this.PageControl == null)
      return (RectangleElement) null;
    if (this.PageControl.FirstSelectedElem == null || this.PageControl.LastSelectedElem == null || this.PageControl.FirstSelectedElem.Page == null || this.PageControl.LastSelectedElem.Page == null)
      return (RectangleElement) null;
    if ((this.PageControl.FirstSelectedElem.Page as Page).PageUI == null || (this.PageControl.LastSelectedElem.Page as Page).PageUI == null)
      return (RectangleElement) null;
    if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
      this.Element.OwnerDocument.UndoManager.LockUndo();
    try
    {
      RectangleElement child = (RectangleElement) null;
      TableElement tableElement = (TableElement) null;
      bool flag1 = false;
      if (this.PageControl.FirstSelectedElem.Page != this.PageControl.LastSelectedElem.Page)
        flag1 = true;
      bool flag2;
      if (flag1)
      {
        flag2 = this.PageControl.LastSelectedElem.Page.Index >= this.PageControl.FirstSelectedElem.Page.Index;
      }
      else
      {
        RectangleF bounds = this.PageControl.LastSelectedElem.Bounds;
        double top1 = (double) bounds.Top;
        bounds = this.PageControl.FirstSelectedElem.Bounds;
        double top2 = (double) bounds.Top;
        flag2 = top1 > top2;
      }
      while (tableUI != null && tableUI.PageElementUIs != null)
      {
        for (int index = 0; index < tableUI.PageElementUIs.Count; ++index)
        {
          if ((tableUI.PageElementUIs[index] is TableCellUI pageElementUi ? pageElementUi.Page?.PageUI : (PageUI) null) != null)
          {
            int left1 = rect.Left;
            int right = rect.Right;
            int num1 = rect.Top;
            int y1 = rect.Bottom;
            Rectangle bounds = (this.PageControl.FirstSelectedElem.Page as Page).PageUI.Bounds;
            int left2 = bounds.Left;
            int num2 = left1 - left2;
            int num3 = right;
            bounds = (this.PageControl.LastSelectedElem.Page as Page).PageUI.Bounds;
            int left3 = bounds.Left;
            int num4 = num3 - left3;
            bounds = pageElementUi.Page.PageUI.Bounds;
            int x1 = bounds.Left + num2;
            bounds = pageElementUi.Page.PageUI.Bounds;
            int x2 = bounds.Left + num4;
            if (flag1 && (this.PageControl.FirstSelectedElem.Page == pageElementUi.Element.Page & flag2 || this.PageControl.LastSelectedElem.Page == pageElementUi.Element.Page && !flag2))
            {
              bounds = pageElementUi.Bounds;
              y1 = bounds.Bottom;
              if (!flag2)
                num1 = rect.Bottom;
            }
            if (flag1 && (this.PageControl.LastSelectedElem.Page == pageElementUi.Element.Page & flag2 || this.PageControl.FirstSelectedElem.Page == pageElementUi.Element.Page && !flag2))
            {
              bounds = pageElementUi.Bounds;
              num1 = bounds.Top;
              if (!flag2)
                y1 = rect.Top;
            }
            if (flag1 && this.PageControl.LastSelectedElem.Page != pageElementUi.Element.Page && this.PageControl.FirstSelectedElem.Page != pageElementUi.Element.Page)
            {
              bounds = pageElementUi.Bounds;
              num1 = bounds.Top;
              bounds = pageElementUi.Bounds;
              y1 = bounds.Bottom;
            }
            int y2 = num1;
            Rectangle rect1 = PageControl.NormalRectangle(new Point(x1, y2), new Point(x2, y1));
            RectangleElement cellsInRectangle = pageElementUi.GetCellsInRectangle(rect1, frameRowSelection);
            if (cellsInRectangle != null)
            {
              if (child == null)
              {
                child = cellsInRectangle;
              }
              else
              {
                if (tableElement == null)
                {
                  tableElement = TableElement.CreateVirtualTable((DocumentTreeNode) this.Element, (DocumentTreeNode) this.Element);
                  tableElement.AddChildNode((DocumentTreeNode) child, false, false);
                  if (child.IsVirtualNode)
                    child.SetParent((DocumentTreeNode) tableElement, false, false);
                  child = (RectangleElement) tableElement;
                }
                tableElement.AddChildNode((DocumentTreeNode) cellsInRectangle, false, false);
                if (cellsInRectangle.IsVirtualNode)
                  cellsInRectangle.SetParent((DocumentTreeNode) tableElement, false, false);
              }
            }
          }
        }
        if (!flag1 || tableUI.Element.Page == this.PageControl.LastSelectedElem.Page || !(tableUI.Element is TableElement))
        {
          tableUI = (TableUI) null;
        }
        else
        {
          TableData element = tableUI.Element as TableData;
          TableData tableData = !flag2 ? element.PrevTable : element.NextTable;
          if (tableData == null)
          {
            tableUI = (TableUI) null;
          }
          else
          {
            if (tableData.Page is Page page)
              page.UpdateChildUIGeometry(true, false);
            tableUI = (tableData as IPageElementWithInterface).PageUI as TableUI;
          }
        }
      }
      if (this.DocumentControl != null && this.DocumentControl.PageControl != null)
      {
        if (this.PageControl.LastSelectedElem.Page != this.DocumentControl.ActivePage)
        {
          this.DocumentControl.ActivePage = this.PageControl.LastSelectedElem.Page as Page;
          this.DocumentControl.PageControl.focusedElement = (PageElementUI) this;
        }
        if (this.PageControl.LastSelectedElem is IPageElementWithInterface && Control.MouseButtons != MouseButtons.None)
          this.DocumentControl.ScrollToViewRectangle((this.PageControl.LastSelectedElem as IPageElementWithInterface).PageUI.Bounds, true, false);
      }
      return child;
    }
    finally
    {
      this.Element.OwnerDocument?.UndoManager?.UnlockUndo();
    }
  }

  /// <summary>Получить ячейки пересекающиеся с прямоугольником</summary>
  /// <param name="rect">Прямоугольник в экранных координатах страницы</param>
  internal RectangleElement GetCellsInRectangle(Rectangle rect, bool frameRowSelection)
  {
    if (this.Bounds.IntersectsWith(rect))
    {
      if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
        this.Element.OwnerDocument.UndoManager.LockUndo();
      try
      {
        if (this.CanSelectCell() && this.IsSingleCell())
        {
          if (this.Element is RectangleElement)
          {
            RectangleElement element = this.Element as RectangleElement;
            return frameRowSelection && element.ParentCell != null && element.ParentCell.IsRow ? (RectangleElement) element.ParentCell : element;
          }
        }
        else if (this.PageElementUIs != null)
        {
          RectangleElement child = (RectangleElement) null;
          TableElement tableElement = (TableElement) null;
          for (int index = 0; index < this.PageElementUIs.Count; ++index)
          {
            if (this.PageElementUIs[index] is TableCellUI pageElementUi)
            {
              RectangleElement cellsInRectangle = pageElementUi.GetCellsInRectangle(rect, frameRowSelection);
              if (cellsInRectangle != null)
              {
                if (child == null)
                {
                  child = cellsInRectangle;
                }
                else
                {
                  if (tableElement == null)
                  {
                    tableElement = TableElement.CreateVirtualTable((DocumentTreeNode) this.Element, (DocumentTreeNode) this.Element);
                    tableElement.AddChildNode((DocumentTreeNode) child, false, false, false, false);
                    if (child.IsVirtualNode)
                      child.SetParent((DocumentTreeNode) tableElement, false, false);
                    child = (RectangleElement) tableElement;
                  }
                  tableElement.AddChildNode((DocumentTreeNode) cellsInRectangle, false, false);
                  if (cellsInRectangle.IsVirtualNode)
                    cellsInRectangle.SetParent((DocumentTreeNode) tableElement, false, false);
                }
              }
            }
          }
          return child;
        }
      }
      finally
      {
        if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
          this.Element.OwnerDocument.UndoManager.UnlockUndo();
      }
    }
    return (RectangleElement) null;
  }

  /// <summary>Получить прямоугольник при выделении с шифтом</summary>
  /// <returns></returns>
  internal Rectangle GetRectangle(Point mousePos) => this.GetRectangle(mousePos, new Keys?());

  /// <summary>Получить прямоугольник при выделении с шифтом</summary>
  /// <param name="mousePos"></param>
  /// <param name="key">клавиша, если выделение происходит с клавиатуры</param>
  /// <returns></returns>
  internal Rectangle GetRectangle(Point mousePos, Keys? key)
  {
    Rectangle empty = Rectangle.Empty;
    List<DocumentTreeNode> selectedNodes = this.DocumentControl?.SelectedNodes;
    Rectangle rectangle = Rectangle.Empty;
    Rectangle rect = Rectangle.Empty;
    RectangleElement rectangleElement1 = this.Element as RectangleElement;
    RectangleElement rectangleElement2 = (RectangleElement) null;
    if (rectangleElement1 != null && selectedNodes != null && selectedNodes.Count > 0)
    {
      rectangle = this.Page.PageUI.ConvertWorldToPixel(rectangleElement1.Bounds);
      TableData tableOwner = rectangleElement1.TableOwner;
      RectangleElement rectangleElement3 = selectedNodes[0] as RectangleElement;
      if (tableOwner != null && rectangleElement3 != null)
      {
        bool flag = true;
        for (int index = 0; index < selectedNodes.Count; ++index)
        {
          RectangleElement rectangleElement4 = selectedNodes[index] as RectangleElement;
          TableData tableData = selectedNodes[index] as TableData;
          PointF location = rectangleElement3.Location;
          double y1 = (double) location.Y;
          location = rectangleElement1.Location;
          double y2 = (double) location.Y;
          double num1 = (double) Math.Abs((float) (y1 - y2));
          location = rectangleElement4.Location;
          double y3 = (double) location.Y;
          location = rectangleElement1.Location;
          double y4 = (double) location.Y;
          double num2 = (double) Math.Abs((float) (y3 - y4));
          if (num1 < num2)
            rectangleElement3 = rectangleElement4;
          if (tableData == null || tableData != null && !tableData.IsRow)
          {
            flag = false;
            break;
          }
        }
        if (!flag)
        {
          for (int index = 0; index < selectedNodes.Count; ++index)
          {
            if (selectedNodes[index] is RectangleElement rectangleElement5 && rectangleElement5.TableOwner != null && rectangleElement5.TableOwner == tableOwner)
            {
              rectangleElement2 = rectangleElement5;
              break;
            }
          }
        }
        else
          rectangleElement2 = rectangleElement3;
      }
    }
    if (rectangleElement2 == null && rectangleElement1 != null)
    {
      RectangleElement rectangleElement6 = (RectangleElement) rectangleElement1.TableOwner;
      while (rectangleElement6 != null && !rectangleElement6.IsSingleCell)
      {
        if (rectangleElement6.NodesCount != 0)
          rectangleElement6 = rectangleElement6.Nodes[0] as RectangleElement;
      }
      rectangleElement2 = rectangleElement6;
    }
    if (rectangleElement2 != null && (!rectangleElement2.IsVirtualNode || rectangleElement2 is VirtualColumn))
    {
      this.PageControl.LastSelectedElem = rectangleElement2;
      this.PageControl.FirstSelectedElem = rectangleElement2;
    }
    else
      rectangleElement2 = this.PageControl.FirstSelectedElem;
    if (key.HasValue)
    {
      RectangleElement cell = this.PageControl.LastSelectedElem ?? rectangleElement1;
      RectangleElement Node = (RectangleElement) null;
      Point cursorPosition = Point.Empty;
      CanShiftSelect_EventArgs shiftSelectEventArgs = new CanShiftSelect_EventArgs((DocumentTreeNode) Node, true);
      CanShiftSelect_EventArgs e;
      do
      {
        Keys? nullable = key;
        if (nullable.HasValue)
        {
          switch (nullable.GetValueOrDefault())
          {
            case Keys.Left | Keys.Shift:
              Node = this.GetPrevSingleCell(cell);
              break;
            case Keys.Up | Keys.Shift:
              Node = this.GetUpSingleCell(cell, out cursorPosition);
              break;
            case Keys.Right | Keys.Shift:
              Node = this.GetNextSingleCell(cell);
              break;
            case Keys.Down | Keys.Shift:
              Node = this.GetDownSingleCell(cell, out cursorPosition);
              break;
          }
        }
        e = new CanShiftSelect_EventArgs((DocumentTreeNode) Node, true);
        this.DocumentControl.OnCanShiftSelect(e);
        if (!e.CanSelect)
        {
          if (Node != null)
            cell = Node;
          else
            e.CanSelect = true;
        }
      }
      while (!e.CanSelect);
      if (Node != null)
      {
        rectangleElement1 = Node;
        this.PageControl.LastSelectedElem = Node;
      }
      else
        rectangleElement1 = this.PageControl.LastSelectedElem;
      rectangleElement2 = this.PageControl.FirstSelectedElem;
    }
    if (rectangleElement2 != null && this.PageControl != null && rectangleElement1 != null)
    {
      this.PageControl.LastSelectedElem = rectangleElement1;
      Rectangle pixel1 = (rectangleElement1.Page as Page).PageUI.ConvertWorldToPixel(rectangleElement1.Bounds);
      Rectangle pagesCoorRectangle1 = (rectangleElement1.Page as Page).PageUI.GetPagesCoorRectangle(pixel1);
      Rectangle pixel2 = (rectangleElement2.Page as Page).PageUI.ConvertWorldToPixel(rectangleElement2.Bounds);
      Rectangle pagesCoorRectangle2 = (rectangleElement2.Page as Page).PageUI.GetPagesCoorRectangle(pixel2);
      rect = pixel1;
      int left = pixel2.Left;
      int right = rect.Right;
      int top = rect.Top;
      int bottom = rect.Bottom;
      Rectangle bounds;
      if (pagesCoorRectangle1.Left < pagesCoorRectangle2.Left)
      {
        bounds = (rectangleElement2.Page as Page).PageUI.Bounds;
        left = bounds.Left + pagesCoorRectangle1.Left;
      }
      if (pagesCoorRectangle2.Right > pagesCoorRectangle1.Right)
      {
        bounds = (rectangleElement1.Page as Page).PageUI.Bounds;
        right = bounds.Left + pagesCoorRectangle2.Right;
      }
      if (rectangleElement1.Page == rectangleElement2.Page)
      {
        if (pixel2.Top < pixel1.Top)
          top = pixel2.Top;
        if (pixel2.Bottom > pixel1.Bottom)
          bottom = pixel2.Bottom;
      }
      else if (rectangleElement1.Page.Index > rectangleElement2.Page.Index)
      {
        top = pixel2.Top;
        bottom = pixel1.Bottom;
      }
      else
      {
        top = pixel2.Bottom;
        bottom = pixel1.Top;
      }
      rect = Rectangle.FromLTRB(left, top, right, bottom);
    }
    return rectangleElement1 == null || rectangleElement2 == null || rectangleElement1.Page == rectangleElement2.Page ? PageControl.NormalRectangle(rect) : rect;
  }

  /// <summary>Получить ячейки, находящиеся внутри прямоугольника в выделенной таблице</summary>
  /// <param name="mousePos">Положение мышки</param>
  /// <param name="key">Нажатые клавиши</param>
  /// <param name="isFrameSelection">Выделение происходит рамкой или нет</param>
  /// <returns></returns>
  internal RectangleElement GetCellsInRectangleFromSelectedTable(
    Point mousePos,
    Keys? key,
    bool isFrameSelection)
  {
    PageControl pageControl = this.PageControl;
    if (pageControl == null)
      return (RectangleElement) null;
    Rectangle empty = Rectangle.Empty;
    Rectangle rect = !(Control.ModifierKeys != Keys.Shift | isFrameSelection) ? this.GetRectangle(mousePos, key) : Rectangle.FromLTRB(this.leftMouseDownPos.X, this.leftMouseDownPos.Y, mousePos.X, mousePos.Y);
    PageElementUI elementUiAtPoint = this.PageControl?.GetPageElementUIAtPoint(mousePos, true);
    if (!key.HasValue && this.PageControl != null && Control.ModifierKeys != Keys.Shift | isFrameSelection && elementUiAtPoint?.Element is RectangleElement element)
    {
      this.PageControl.FirstSelectedElem = this.Element as RectangleElement;
      this.PageControl.LastSelectedElem = element;
    }
    bool frameRowSelection = this.DocumentControl != null && this.DocumentControl.RowSelection && this.PageControl?.FirstSelectedElem != null && this.PageControl.LastSelectedElem != null && this.PageControl.FirstSelectedElem.Parent != this.PageControl.LastSelectedElem.Parent;
    if (rect.Width == 0)
      rect.Width = 1;
    if (rect.Height == 0)
      rect.Height = 1;
    RectangleElement fromSelectedTable = (RectangleElement) null;
    TableCellUI tableCellUi = (TableCellUI) null;
    if (pageControl.SelectedTable != null)
      tableCellUi = pageControl.SelectedTable.PageUI as TableCellUI;
    if (tableCellUi != null)
    {
      fromSelectedTable = tableCellUi.GetCellsInRectangle(rect, frameRowSelection);
    }
    else
    {
      TableUI tableUI = (TableUI) null;
      if (pageControl.SelectedTable != null)
        tableUI = pageControl.SelectedTable.PageUI as TableUI;
      if (tableUI == null)
        tableUI = ((TableElement) ((RectangleElement) this.element).TopLevelTable).PageUI as TableUI;
      if (tableUI != null)
        fromSelectedTable = this.GetCellsInRectangle(tableUI, rect, frameRowSelection);
    }
    return fromSelectedTable;
  }

  /// <summary>Получить ячейки, находящиеся внутри прямоугольника в выделенной таблице</summary>
  /// <param name="mousePos">Положение мышки</param>
  /// <param name="isFrameSelection">Выделение происходит рамкой или нет</param>
  /// <returns></returns>
  internal RectangleElement GetCellsInRectangleFromSelectedTable(
    Point mousePos,
    bool isFrameSelection)
  {
    return this.GetCellsInRectangleFromSelectedTable(mousePos, new Keys?(), isFrameSelection);
  }

  /// <summary>Получить ячейки относящиеся к тому же столбцу, что и этот элемент</summary>
  /// <remarks>Таблица должна иметь следующую структуру: ячейка находится в строке,
  /// строка находится в столбце</remarks>
  /// <returns>Возвращает ячейки относящиеся к тому же столбцу, что и этот элемент</returns>
  protected virtual List<DocumentTreeNode> GetColumnCells()
  {
    List<DocumentTreeNode> columnCells = new List<DocumentTreeNode>();
    TableElement parentRow = (TableElement) null;
    TableElement table = (TableElement) null;
    if (this.CanSelectColumnCells(out parentRow, out table))
    {
      int gridColIndex = (this.Element as RectangleElement).GridColIndex;
      table.GetGridColumnCells(gridColIndex, table.GridColumnsParams, (IList<DocumentTreeNode>) columnCells);
    }
    return columnCells;
  }

  private VirtualColumn GetVirtualColumn(IList<DocumentTreeNode> columnCells)
  {
    VirtualColumn virtualColumn = (VirtualColumn) null;
    if (this.Element != null && this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
      this.Element.OwnerDocument.UndoManager.LockUndo();
    try
    {
      if (columnCells != null)
      {
        if (columnCells.Count > 0)
        {
          if (columnCells[0] is RectangleElement columnCell)
          {
            TableData parentCell = columnCell.ParentCell;
            TableData paramsOwner = (TableData) null;
            RowColParams columnParams = (RowColParams) null;
            if (parentCell != null)
            {
              List<RowColParams> gridColumnsParams = parentCell.GetGridColumnsParams(out paramsOwner, out bool _, true, false);
              int gridColumnIndex = columnCell.GetGridColumnIndex();
              if (gridColumnsParams != null && gridColumnIndex < gridColumnsParams.Count)
                columnParams = gridColumnsParams[gridColumnIndex];
            }
            virtualColumn = new VirtualColumn(paramsOwner as TableElement, columnParams, columnCells);
          }
        }
      }
    }
    finally
    {
      if (this.Element != null && this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
        this.Element.OwnerDocument.UndoManager.UnlockUndo();
    }
    return virtualColumn;
  }

  /// <summary>Получить следуюущую для заданной единичную ячейку</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  public RectangleElement GetNextSingleCell(RectangleElement cell)
  {
    if (cell == null)
      return (RectangleElement) null;
    cell1 = (RectangleElement) null;
    if (cell is TableData tableData && tableData.NextTable != null)
    {
      cell1 = this.GetFirstSingleCell((RectangleElement) tableData.NextTable) ?? this.GetNextSingleCell((RectangleElement) tableData.NextTable);
    }
    else
    {
      TableData parentCell = cell.ParentCell;
      if (parentCell != null)
      {
        int index = cell.Index + 1;
        if (index < parentCell.Nodes.Count)
        {
          if (parentCell.Nodes[index] is RectangleElement cell1 && !cell1.IsSingleCell)
            cell1 = this.GetFirstSingleCell(cell1);
        }
        else if (Control.ModifierKeys != Keys.Shift)
          cell1 = this.GetNextSingleCell((RectangleElement) parentCell);
      }
    }
    return cell1;
  }

  /// <summary>Получить предыдущую для заданной единичную ячейку</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  public RectangleElement GetPrevSingleCell(RectangleElement cell)
  {
    if (cell == null)
      return (RectangleElement) null;
    cell1 = (RectangleElement) null;
    if (cell is TableData tableData && tableData.PrevTable != null)
    {
      cell1 = this.GetLastSingleCell((RectangleElement) tableData.PrevTable) ?? this.GetPrevSingleCell((RectangleElement) tableData.PrevTable);
    }
    else
    {
      TableData parentCell = cell.ParentCell;
      if (parentCell != null)
      {
        int index = cell.Index - 1;
        if (index >= 0)
        {
          if (parentCell.Nodes[index] is RectangleElement cell1 && !cell1.IsSingleCell)
            cell1 = this.GetLastSingleCell(cell1);
        }
        else if (Control.ModifierKeys != Keys.Shift)
          cell1 = this.GetPrevSingleCell((RectangleElement) parentCell);
      }
    }
    return cell1;
  }

  /// <summary>Получить единичную ячейку снизу для заданной ячейки</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <returns></returns>
  public RectangleElement GetDownSingleCell(RectangleElement cell, out Point cursorPosition)
  {
    cursorPosition = this.Bounds.Location;
    if (cell == null)
      return (RectangleElement) null;
    RectangleElement downSingleCell = (RectangleElement) null;
    TextBoxElement textBoxElement = cell as TextBoxElement;
    PageControl pageControl = this.PageControl;
    float x1;
    if (textBoxElement != null && textBoxElement.InPlaceEditorActive && pageControl != null)
    {
      Point textCursorCoor = textBoxElement.TextBox.GetTextCursorCoor();
      ref Point local = ref textCursorCoor;
      Rectangle bounds1 = this.Bounds;
      int x2 = bounds1.X + textCursorCoor.X;
      bounds1 = this.Bounds;
      int y = bounds1.Y + textCursorCoor.Y;
      local = new Point(x2, y);
      cursorPosition.X = textCursorCoor.X;
      x1 = this.Page.PageUI.ConvertPixelToWorld(textCursorCoor).X;
      RectangleF bounds2 = cell.Bounds;
      if ((double) x1 < (double) bounds2.X)
        x1 = bounds2.X;
      else if ((double) x1 > (double) bounds2.Right)
        x1 = bounds2.Right;
    }
    else
      x1 = cell.Location.X;
    if (cell is TableData tableData && tableData.NextTable != null)
      downSingleCell = this.GetFirstDownSingleCellOnLine(0, tableData.NextTable, x1);
    if (downSingleCell == null)
    {
      TableData parentCell = cell.ParentCell;
      if (parentCell == null)
        return (RectangleElement) null;
      downSingleCell = this.GetFirstDownSingleCellOnLine(cell.Index + 1, parentCell, x1);
    }
    return downSingleCell;
  }

  /// <summary>Получить единичную ячейку сверху для заданной ячейки</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <returns></returns>
  public RectangleElement GetUpSingleCell(RectangleElement cell, out Point cursorPosition)
  {
    ref Point local = ref cursorPosition;
    Rectangle bounds1 = this.Bounds;
    int left = bounds1.Left;
    bounds1 = this.Bounds;
    int top = bounds1.Top;
    Point point1 = new Point(left, top);
    local = point1;
    if (cell == null)
      return (RectangleElement) null;
    RectangleElement upSingleCell = (RectangleElement) null;
    TextBoxElement textBoxElement = cell as TextBoxElement;
    PageControl pageControl = this.PageControl;
    float x;
    if (textBoxElement != null && textBoxElement.InPlaceEditorActive && pageControl != null)
    {
      Point point2 = textBoxElement.TextBox.GetTextCursorCoor();
      point2 = new Point(this.Bounds.X + point2.X, this.Bounds.Y + point2.Y);
      cursorPosition.X = point2.X;
      x = this.Page.PageUI.ConvertPixelToWorld(point2).X;
      RectangleF bounds2 = cell.Bounds;
      if ((double) x < (double) bounds2.X)
        x = bounds2.X;
      else if ((double) x > (double) bounds2.Right)
        x = bounds2.Right;
    }
    else
      x = cell.Location.X;
    if (cell.PrevCell != null)
    {
      upSingleCell = cell.PrevCell;
      cursorPosition.Y = -1;
    }
    if (upSingleCell == null)
    {
      if (cell is TableData tableData && tableData.PrevTable != null)
        upSingleCell = this.GetFirstUpSingleCellOnLine(tableData.PrevTable.Nodes.Count - 1, tableData.PrevTable, x);
      if (upSingleCell == null)
      {
        TableData parentCell = cell.ParentCell;
        if (parentCell == null)
          return (RectangleElement) null;
        upSingleCell = this.GetFirstUpSingleCellOnLine(cell.Index - 1, parentCell, x);
      }
    }
    return upSingleCell;
  }

  /// <summary>Получить первую единичную ячейку строго вниз</summary>
  /// <param name="startIndex">Индекс с которого нужно просматривать ячейки в заданной таблице</param>
  /// <param name="table">Таблица начиная с которой нужно искать ячейку</param>
  /// <param name="x">Координата X, которая должна проходить через ячейку</param>
  /// <returns></returns>
  public RectangleElement GetFirstDownSingleCellOnLine(int startIndex, TableData table, float x)
  {
    if (table == null)
      return (RectangleElement) null;
    RectangleElement singleCellOnLine = (RectangleElement) null;
    RectangleF bounds = table.Bounds;
    if ((double) bounds.X <= (double) x && (double) x < (double) bounds.Right)
    {
      if (table.IsColumn)
      {
        int index = startIndex;
        for (int count = table.Nodes.Count; index < count; ++index)
        {
          if (table.Nodes[index] is RectangleElement node)
          {
            bounds = node.Bounds;
            if ((double) bounds.X <= (double) x && (double) x < (double) bounds.Right)
            {
              if (node.IsSingleCell)
                return node;
              singleCellOnLine = this.GetFirstDownSingleCellOnLine(0, node as TableData, x);
              if (singleCellOnLine != null)
                return singleCellOnLine;
            }
          }
        }
      }
      else
      {
        int index = startIndex;
        for (int count = table.Nodes.Count; index < count; ++index)
        {
          if (table.Nodes[index] is RectangleElement node)
          {
            bounds = node.Bounds;
            if ((double) bounds.X <= (double) x)
            {
              if ((double) x < (double) bounds.Right)
              {
                if (node.IsSingleCell)
                  return node;
                singleCellOnLine = this.GetFirstDownSingleCellOnLine(0, node as TableData, x);
                if (singleCellOnLine != null)
                  return singleCellOnLine;
              }
            }
            else
              break;
          }
        }
      }
    }
    if (singleCellOnLine == null && table.NextTable != null)
      singleCellOnLine = this.GetFirstDownSingleCellOnLine(0, table.NextTable, x);
    if (singleCellOnLine == null)
    {
      TableData parentCell = table.ParentCell;
      if (parentCell != null)
        singleCellOnLine = this.GetFirstDownSingleCellOnLine(table.Index + 1, parentCell, x);
    }
    return singleCellOnLine;
  }

  /// <summary>Получить первую единичную ячейку строго вверх</summary>
  /// <param name="startIndex">Индекс с которого нужно просматривать ячейки в заданной таблице</param>
  /// <param name="table">Таблица начиная с которой нужно искать ячейку</param>
  /// <param name="x">Координата X, которая должна проходить через ячейку</param>
  /// <returns></returns>
  public RectangleElement GetFirstUpSingleCellOnLine(int startIndex, TableData table, float x)
  {
    if (table == null)
      return (RectangleElement) null;
    RectangleElement singleCellOnLine = (RectangleElement) null;
    RectangleF bounds = table.Bounds;
    if ((double) bounds.X <= (double) x && (double) x < (double) bounds.Right)
    {
      if (table.IsColumn)
      {
        for (int index = startIndex; index >= 0; --index)
        {
          if (table.Nodes[index] is RectangleElement node)
          {
            bounds = node.Bounds;
            if ((double) bounds.X <= (double) x && (double) x < (double) bounds.Right)
            {
              if (node.IsSingleCell)
                return node;
              if (node is TableData table1)
                singleCellOnLine = this.GetFirstUpSingleCellOnLine(table1.Nodes.Count - 1, table1, x);
              if (singleCellOnLine != null)
                return singleCellOnLine;
            }
          }
        }
      }
      else
      {
        for (int index = startIndex; index >= 0; --index)
        {
          if (table.Nodes[index] is RectangleElement node)
          {
            bounds = node.Bounds;
            if ((double) x < (double) bounds.Right)
            {
              if ((double) bounds.X <= (double) x)
              {
                if (node.IsSingleCell)
                  return node;
                if (node is TableData table2)
                  singleCellOnLine = this.GetFirstDownSingleCellOnLine(table2.Nodes.Count - 1, table2, x);
                if (singleCellOnLine != null)
                  return singleCellOnLine;
              }
            }
            else
              break;
          }
        }
      }
    }
    if (singleCellOnLine == null && table.PrevTable != null)
      singleCellOnLine = this.GetFirstUpSingleCellOnLine(table.PrevTable.Nodes.Count - 1, table.PrevTable, x);
    if (singleCellOnLine == null)
    {
      TableData parentCell = table.ParentCell;
      if (parentCell != null)
        singleCellOnLine = this.GetFirstUpSingleCellOnLine(table.Index - 1, parentCell, x);
    }
    return singleCellOnLine;
  }

  /// <summary>Получить первую единичную ячейку внутри заданной</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  public RectangleElement GetFirstSingleCell(RectangleElement cell)
  {
    if (cell.IsSingleCell)
      return cell;
    DocumentTreeNodeCollection nodes = cell.Nodes;
    if (nodes != null)
    {
      int index = 0;
      for (int count = nodes.Count; index < count; ++index)
      {
        cell = nodes[index] as RectangleElement;
        if (cell != null)
          cell = this.GetFirstSingleCell(cell);
        if (cell != null)
          return cell;
      }
    }
    return (RectangleElement) null;
  }

  /// <summary>Получить последнюю единичную ячейку внутри заданной</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  public RectangleElement GetLastSingleCell(RectangleElement cell)
  {
    if (cell.IsSingleCell)
      return cell;
    DocumentTreeNodeCollection nodes = cell.Nodes;
    if (nodes != null)
    {
      for (int index = nodes.Count - 1; index >= 0; --index)
      {
        cell = nodes[index] as RectangleElement;
        if (cell != null)
          cell = this.GetLastSingleCell(cell);
        if (cell != null)
          return cell;
      }
    }
    return (RectangleElement) null;
  }

  /// <summary>Перейти к следующей единичной ячейке</summary>
  public void GotoNextSingleCell()
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null)
      return;
    RectangleElement nextSingleCell = this.GetNextSingleCell(this.Element as RectangleElement);
    if (nextSingleCell == null)
      return;
    Point cursorPosition = Point.Empty;
    if (nextSingleCell is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      cursorPosition = elementWithInterface.PageUI.Bounds.Location;
    documentControl.SetSelection((DocumentTreeNode) nextSingleCell, true, cursorPosition, false, false);
  }

  /// <summary>Перейти к предыдущей единичной ячейке</summary>
  public void GotoPrevSingleCell()
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null)
      return;
    RectangleElement prevSingleCell = this.GetPrevSingleCell(this.Element as RectangleElement);
    if (prevSingleCell == null)
      return;
    Point cursorPosition = Point.Empty;
    if (prevSingleCell is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
    {
      Rectangle bounds = elementWithInterface.PageUI.Bounds;
      cursorPosition = new Point(bounds.Right - 1, bounds.Bottom - 1);
    }
    documentControl.SetSelection((DocumentTreeNode) prevSingleCell, true, cursorPosition, false, false);
  }

  /// <summary>Перейти к ячеке снизу</summary>
  public void GotoDownSingleCell()
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null)
      return;
    Point cursorPosition = Point.Empty;
    RectangleElement downSingleCell = this.GetDownSingleCell(this.Element as RectangleElement, out cursorPosition);
    if (downSingleCell == null)
      return;
    documentControl.SetSelection((DocumentTreeNode) downSingleCell, true, cursorPosition, false, false);
  }

  /// <summary>Перейти к ячеке сверху</summary>
  public void GotoUpSingleCell()
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null)
      return;
    Point cursorPosition = Point.Empty;
    RectangleElement upSingleCell = this.GetUpSingleCell(this.Element as RectangleElement, out cursorPosition);
    if (upSingleCell == null)
      return;
    documentControl.SetSelection((DocumentTreeNode) upSingleCell, true, cursorPosition, false, false);
  }

  /// <summary>Получить область захвата под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="grabZone">Возвращает область захвата</param>
  /// <returns>true, если под точкой есть область захвата</returns>
  protected virtual bool GetGrabHandleZone(Point point, out GrabHandleZone grabZone)
  {
    grabZone = GrabHandleZone.Center;
    bool grabHandleZone = false;
    foreach (GrabHandleZone zone in Enum.GetValues(typeof (GrabHandleZone)))
    {
      Rectangle rectangle = PageElementUI.PixelRectangle(this.GetGrabHandleZoneBounds(zone));
      if (this.GrabHandleZoneActive(zone) && rectangle.Contains(point))
      {
        grabHandleZone = true;
        grabZone = zone;
        break;
      }
    }
    return grabHandleZone;
  }

  /// <summary>Активна ли заданнай область захвата</summary>
  /// <param name="zone">Область захвата</param>
  /// <returns>Активна ли заданнай область захвата</returns>
  protected virtual bool GrabHandleZoneActive(GrabHandleZone zone)
  {
    RectangleElement element = this.Element as RectangleElement;
    switch (zone)
    {
      case GrabHandleZone.Center:
        return true;
      case GrabHandleZone.Top:
        return false;
      case GrabHandleZone.Right:
        return this.Vertical && element != null && !this.CheckRightSideForBlockedGeometry(element);
      case GrabHandleZone.Bottom:
        return !this.Vertical && !this.GeometryChangingBlocked;
      case GrabHandleZone.Left:
        if (!this.Vertical || element == null || this.CheckLeftSideForBlockedGeometry(element) || element.TopLevelTable == null)
          return false;
        PointF location = element.Location;
        double x1 = (double) location.X;
        location = element.TopLevelTable.Location;
        double x2 = (double) location.X;
        return x1 == x2;
      default:
        return false;
    }
  }

  /// <summary>Проверить, может ли пользователь перетаскивать правую границу ячейки</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  private bool CheckRightSideForBlockedGeometry(RectangleElement cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    if (this.DocumentControl != null && this.DocumentControl.ReadOnlyGeometry || cell.GeometryChangingBlocked)
      return true;
    TableData parentCell = cell.ParentCell;
    if (parentCell == null)
      return false;
    if (parentCell.IsColumn)
      return this.CheckRightSideForBlockedGeometry((RectangleElement) parentCell);
    return cell.IsLastInParentCell && this.CheckRightSideForBlockedGeometry((RectangleElement) parentCell);
  }

  /// <summary>Проверить, может ли пользователь перетаскивать левую границу ячейки</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  private bool CheckLeftSideForBlockedGeometry(RectangleElement cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    if (cell.GeometryChangingBlocked)
      return true;
    TableData parentCell = cell.ParentCell;
    if (parentCell == null)
      return false;
    if (parentCell.IsColumn)
      return this.CheckLeftSideForBlockedGeometry((RectangleElement) parentCell);
    return !cell.IsFirstInParentCell || this.CheckLeftSideForBlockedGeometry((RectangleElement) parentCell);
  }

  /// <summary>Получить зону для выбора столбца</summary>
  /// <returns>Зону для выбора столбца</returns>
  protected virtual Rectangle ColumnSelectionZone()
  {
    Rectangle bounds = this.Bounds;
    int left = bounds.Left;
    bounds = this.Bounds;
    int top = bounds.Top;
    bounds = this.Bounds;
    int width = bounds.Width;
    int columnSelectionHeight = this.columnSelectionHeight;
    return new Rectangle(left, top, width, columnSelectionHeight);
  }

  /// <summary>Можно ли выбрать ячейки столбца</summary>
  /// <returns>Можно ли выбрать ячейки столбца</returns>
  protected virtual bool CanSelectColumnCells()
  {
    TableElement parentRow = (TableElement) null;
    TableElement table = (TableElement) null;
    return this.CanSelectColumnCells(out parentRow, out table);
  }

  /// <summary>Проверяет можно ли выделить столбец к которому относится эта ячейка</summary>
  /// <remarks>Таблица должна иметь следующую структру: ячейка находится в строке,
  /// строка находится в столбце</remarks>
  /// <param name="parentRow">Возвращает родительскую строку</param>
  /// <param name="table">Возвращает таблицу, в которой находится строка
  /// в которой находится ячейка</param>
  /// <returns>true если можно выделить столбец к которому относится эта ячейка</returns>
  protected virtual bool CanSelectColumnCells(out TableElement parentRow, out TableElement table)
  {
    bool flag = false;
    parentRow = (TableElement) null;
    table = (TableElement) null;
    if (this.Element is RectangleElement element)
    {
      parentRow = element.ParentCell as TableElement;
      if (parentRow != null && parentRow.IsRow)
      {
        TableData paramsOwner;
        parentRow.GetGridColumnsParams(out paramsOwner, out bool _, false, false);
        table = paramsOwner as TableElement;
        if (table != null)
        {
          PointF location = table.Location;
          double y1 = (double) location.Y;
          location = element.Location;
          double y2 = (double) location.Y;
          if (y1 == y2)
            flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>Получить зону для выбора ячейки</summary>
  /// <returns>Зону для выбора ячейки</returns>
  protected virtual Rectangle CellSelectionZone()
  {
    Rectangle bounds = this.Bounds;
    int x = bounds.Left + 2;
    bounds = this.Bounds;
    int top = bounds.Top;
    int minGrabHandleWidth = this.minGrabHandleWidth;
    bounds = this.Bounds;
    int height = bounds.Height;
    return new Rectangle(x, top, minGrabHandleWidth, height);
  }

  /// <summary>Область выбора строки включена</summary>
  public virtual bool CanSelectCell() => this.Element is RectangleElement;

  /// <summary>Получить зону для выбора строки</summary>
  /// <returns>Зону для выбора строки</returns>
  protected virtual Rectangle RowSelectionZone()
  {
    Rectangle bounds = this.Bounds;
    int x = bounds.Left - 3;
    bounds = this.Bounds;
    int top = bounds.Top;
    int width = this.minGrabHandleWidth + 5;
    bounds = this.Bounds;
    int height = bounds.Height;
    return new Rectangle(x, top, width, height);
  }

  /// <summary>Область выбора строки включена</summary>
  public virtual bool RowSelectionEnabled
  {
    [DebuggerStepThrough] get => this.Element is TableElement element && element.IsRow;
  }

  /// <summary>Получить границы области захвата</summary>
  /// <param name="zone">Область захвата</param>
  /// <returns>Границы области захвата</returns>
  protected virtual Rectangle GetGrabHandleZoneBounds(GrabHandleZone zone)
  {
    if (this.Element == null)
      return Rectangle.Empty;
    int val1 = this.minGrabHandleWidth;
    RectangleElement element = (RectangleElement) this.Element;
    Rectangle handleZoneBounds = Rectangle.Empty;
    RectangleBorder borders = element.Borders;
    switch (zone)
    {
      case GrabHandleZone.Center:
        handleZoneBounds = this.Bounds;
        if (this.GrabHandleZoneActive(GrabHandleZone.Top) || this.CanSelectColumnCells())
        {
          val1 = 0;
          if (this.GrabHandleZoneActive(GrabHandleZone.Top))
            val1 = this.minGrabHandleWidth;
          if (this.CanSelectColumnCells())
            val1 = Math.Max(val1, this.columnSelectionHeight);
          handleZoneBounds = new Rectangle(handleZoneBounds.Left, handleZoneBounds.Top + val1, handleZoneBounds.Width, handleZoneBounds.Height - val1);
        }
        if (this.GrabHandleZoneActive(GrabHandleZone.Right))
          handleZoneBounds = new Rectangle(handleZoneBounds.Left, handleZoneBounds.Top, handleZoneBounds.Width - val1, handleZoneBounds.Height);
        if (this.GrabHandleZoneActive(GrabHandleZone.Bottom))
          handleZoneBounds = new Rectangle(handleZoneBounds.Left, handleZoneBounds.Top, handleZoneBounds.Width, handleZoneBounds.Height - val1);
        if (this.GrabHandleZoneActive(GrabHandleZone.Left) || this.RowSelectionEnabled)
        {
          handleZoneBounds = new Rectangle(handleZoneBounds.Left + val1, handleZoneBounds.Top, handleZoneBounds.Width - val1, handleZoneBounds.Height);
          break;
        }
        break;
      case GrabHandleZone.Top:
        int num = 0;
        ref Rectangle local1 = ref handleZoneBounds;
        int left1 = this.Bounds.Left;
        Rectangle bounds1 = this.Bounds;
        int top1 = bounds1.Top;
        bounds1 = this.Bounds;
        int width1 = bounds1.Width;
        int height1 = num;
        local1 = new Rectangle(left1, top1, width1, height1);
        break;
      case GrabHandleZone.Right:
        if (element.Page != null)
        {
          val1 = ((Page) element.Page).ConvertWorldXToPixel(borders.Right.Width) / 2;
          if (val1 < this.minGrabHandleWidth)
            val1 = this.minGrabHandleWidth;
        }
        ref Rectangle local2 = ref handleZoneBounds;
        Rectangle bounds2 = this.Bounds;
        int x1 = bounds2.Right - val1;
        bounds2 = this.Bounds;
        int top2 = bounds2.Top;
        int width2 = val1;
        bounds2 = this.Bounds;
        int height2 = bounds2.Height;
        local2 = new Rectangle(x1, top2, width2, height2);
        break;
      case GrabHandleZone.Bottom:
        if (element.Page != null)
        {
          val1 = ((Page) element.Page).ConvertWorldYToPixel(borders.Bottom.Width) / 2;
          if (val1 < this.minGrabHandleWidth)
            val1 = this.minGrabHandleWidth;
        }
        ref Rectangle local3 = ref handleZoneBounds;
        Rectangle bounds3 = this.Bounds;
        int left2 = bounds3.Left;
        bounds3 = this.Bounds;
        int y = bounds3.Bottom - val1;
        bounds3 = this.Bounds;
        int width3 = bounds3.Width;
        int height3 = val1;
        local3 = new Rectangle(left2, y, width3, height3);
        break;
      case GrabHandleZone.Left:
        if (element.Page != null)
        {
          val1 = ((Page) element.Page).ConvertWorldXToPixel(borders.Left.Width) / 2;
          if (val1 < this.minGrabHandleWidth)
            val1 = this.minGrabHandleWidth;
        }
        ref Rectangle local4 = ref handleZoneBounds;
        Rectangle bounds4 = this.Bounds;
        int x2 = bounds4.Left - val1;
        bounds4 = this.Bounds;
        int top3 = bounds4.Top;
        int width4 = val1 + 2;
        bounds4 = this.Bounds;
        int height4 = bounds4.Height;
        local4 = new Rectangle(x2, top3, width4, height4);
        break;
    }
    return handleZoneBounds;
  }

  /// <summary>Можно ли начать перемещение элемента</summary>
  /// <param name="point">Начальная точка</param>
  /// <returns>Можно ли начать перемещение элемента</returns>
  protected override bool CanBeginMoving(Point point)
  {
    PageElementNode element = this.Element;
    GrabHandleZone grabZone;
    if (!this.GetGrabHandleZone(point, out grabZone) || grabZone == GrabHandleZone.Center)
      return false;
    this.movingGrabHandleZone = grabZone;
    return base.CanBeginMoving(point);
  }

  /// <summary>Начать процесс перемещения элемента страницы</summary>
  protected override void BeginMoving(MouseEventArgs mouseArgs, Keys modifierKeys)
  {
    if (this.DocumentControl != null)
      this.DocumentControl.DeactivateInPlaceEditor();
    base.BeginMoving(mouseArgs, modifierKeys);
    this.resizingCells.Clear();
    RectangleElement element = (RectangleElement) this.Element;
    RectangleF bounds = element.Bounds;
    if (this.Vertical)
    {
      if (this.movingGrabHandleZone == GrabHandleZone.Left)
      {
        element.TopLevelTable.FindResizableLeftSide(this.resizingCells, bounds.X);
        if (this.resizingCells.Count == 0)
          this.resizingCells.Add(this.Element as RectangleElement);
      }
      else if (element == element.TopLevelTable || (double) element.bounds.Right != (double) element.TopLevelTable.bounds.Right)
      {
        int gridColumnIndex = element.GetGridColumnIndex();
        if (element.GridPos != null && element.GridPos.SpanCount == 0 || gridColumnIndex == -1 || element.WidthOverrided || modifierKeys == Keys.Alt)
        {
          if (element.ParentCell?.GridColumnsParams == null || gridColumnIndex >= element.ParentCell.GridColumnsParams.Count)
          {
            element.TopLevelTable.FindResizableRightSide(this.resizingCells, bounds.Right);
            if (this.resizingCells.Count == 0)
              this.resizingCells.Add(this.Element as RectangleElement);
          }
          else
          {
            bool flag = false;
            if (element.WidthOverrided && modifierKeys != Keys.Alt && gridColumnIndex != -1)
            {
              List<RowColParams> rowColParamsList = element.ParentCell == null ? ((TableData) element).GridColumnsParams : element.ParentCell.GridColumnsParams;
              float size = rowColParamsList[gridColumnIndex].Size;
              if (!element.IsDefaultGridPos)
              {
                TableGridPosition gridPos = element.GridPos;
                int spanCount = gridPos != null ? gridPos.SpanCount : 0;
                for (int index = 1; index < spanCount - 1 && gridColumnIndex + index < rowColParamsList.Count; ++index)
                  size += rowColParamsList[gridColumnIndex + index].Size;
              }
              flag = (double) size == (double) element.bounds.Width;
            }
            if (!flag)
            {
              RectangleElement topLevelTable = (RectangleElement) element.TopLevelTable;
              if (modifierKeys != Keys.Alt)
                topLevelTable.FindResizableRightSide(this.resizingCells, bounds.Right);
              if (this.resizingCells.Count == 0)
                this.resizingCells.Add(this.Element as RectangleElement);
            }
          }
        }
      }
    }
    else if (element.GetGridRowIndex() == -1 || element.HeightOverrided)
    {
      element.TopLevelTable.FindResizableBottomSide(this.resizingCells, bounds.Bottom);
      if (this.resizingCells.Count == 0)
        this.resizingCells.Add(this.Element as RectangleElement);
    }
    this.DrawNewBoundsPreview((Graphics) null);
  }

  /// <summary>Отменить процесс перемещения элемента страницы</summary>
  public override void CancelMoving(Point start, bool erasePreview)
  {
    base.CancelMoving(start, erasePreview);
    this.startPoint = start;
    if (((this.PageControl == null ? 0 : (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize ? 1 : 0)) & (erasePreview ? 1 : 0)) != 0)
    {
      this.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
      this.PageControl.Invalidate(this.PageControl.RegionForInvalidate);
    }
    if (!erasePreview)
      return;
    this.EraseNewBoundsPreview();
  }

  /// <summary>Завершить процесс перемещения элемента страницы</summary>
  protected override void EndMoving(
    MouseEventArgs mouseArgs,
    Keys modifierKeys,
    Point startPoint,
    Point delta)
  {
    if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
      this.Element.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_588"));
    try
    {
      if (this.PageControl != null && ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
      {
        this.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
        this.PageControl.Invalidate(this.PageControl.RegionForInvalidate);
      }
      base.EndMoving(mouseArgs, modifierKeys, startPoint, delta);
      this.EraseNewBoundsPreview();
      RectangleElement element1 = (RectangleElement) this.Element;
      GrabHandleZone grabZone = GrabHandleZone.Center;
      this.GetGrabHandleZone(startPoint, out grabZone);
      if ((modifierKeys & Keys.Alt) != Keys.None && grabZone != GrabHandleZone.Left)
      {
        this.resizingCells.Clear();
        this.resizingCells.Add(this.element as RectangleElement);
      }
      int firstColIndex = -1;
      if (this.Vertical && this.resizingCells.Count == 0)
      {
        firstColIndex = element1.GetGridColumnIndex();
        if (firstColIndex != -1 && (element1.ParentCell.GridColumnsParams == null || firstColIndex >= element1.ParentCell.GridColumnsParams.Count))
        {
          firstColIndex = -1;
          this.resizingCells.Add(this.element as RectangleElement);
        }
      }
      RectangleF bounds1 = element1.Bounds;
      if (this.resizingCells.Count == 0 && !element1.IsDefaultGridPos && this.Vertical && (element1 == element1.TopLevelTable || (double) element1.bounds.Right != (double) element1.TopLevelTable.bounds.Right))
      {
        int spanCount = element1.GridPos.SpanCount;
        if (Math.Round((double) this.CalcGridColumnWidth(element1, firstColIndex, this.elementBounds.Width), 5) == 0.0)
        {
          firstColIndex = -1;
          this.resizingCells.Add(this.element as RectangleElement);
        }
      }
      if (this.resizingCells.Count > 0)
      {
        RectangleElement topLevelTable1 = (RectangleElement) element1.TopLevelTable;
        topLevelTable1.SuspendUpdateGeometryRefreshUI();
        topLevelTable1.SuspendUpdateLayout();
        try
        {
          RectangleF empty = RectangleF.Empty;
          PageData pageData = (PageData) null;
          if (this.element != null)
            pageData = this.element.Page;
          RectangleF rectangleF = this.CalcNewElementBounds(grabZone, startPoint, delta);
          PointF delta1 = PointF.Empty;
          switch (grabZone)
          {
            case GrabHandleZone.Center:
              delta1 = new PointF(rectangleF.X - bounds1.X, rectangleF.Y - bounds1.Y);
              break;
            case GrabHandleZone.Top:
              delta1 = new PointF(0.0f, rectangleF.Y - bounds1.Y);
              break;
            case GrabHandleZone.Right:
              delta1 = new PointF(rectangleF.Right - bounds1.Right, 0.0f);
              break;
            case GrabHandleZone.Bottom:
              delta1 = new PointF(0.0f, rectangleF.Bottom - bounds1.Bottom);
              break;
            case GrabHandleZone.Left:
              delta1 = new PointF(rectangleF.X - bounds1.X, 0.0f);
              break;
          }
          RectangleF newBounds;
          for (int index = 0; index < this.resizingCells.Count; ++index)
          {
            RectangleElement resizingCell1 = this.resizingCells[index];
            if (resizingCell1 != null)
            {
              if (pageData == null)
                pageData = resizingCell1.Page;
              IPageElementWithInterface resizingCell2 = this.resizingCells[index] as IPageElementWithInterface;
              TableCellUI tableCellUi = (TableCellUI) null;
              if (resizingCell2 != null)
                tableCellUi = resizingCell2.PageUI as TableCellUI;
              if (pageData != null && tableCellUi != null)
              {
                newBounds = tableCellUi.CalcNewElementBounds(grabZone, delta1);
                RectangleElement rectangleElement = (RectangleElement) null;
                if (this.Vertical && grabZone != GrabHandleZone.Left && (modifierKeys & Keys.Control) == Keys.None)
                  rectangleElement = !resizingCell1.ParentCell.IsRow ? (RectangleElement) null : resizingCell1.ParentCell.FindNextVisibleCellInThisTable(resizingCell1.Index);
                if (rectangleElement != null)
                {
                  RectangleF bounds2 = rectangleElement.Bounds;
                  RectangleF bounds3 = resizingCell1.Bounds;
                  if (this.Vertical)
                  {
                    float num = (float) Math.Round((double) bounds3.Width + (double) bounds2.Width, 5);
                    bounds2.Width = (float) Math.Round((double) num - (double) newBounds.Width, 5);
                    bounds2.X = newBounds.X + newBounds.Width;
                    if ((double) bounds2.Width < 0.0)
                    {
                      newBounds.Width = num;
                      bounds2.Width = 0.0f;
                    }
                    resizingCell1.WidthOverrided = true;
                    rectangleElement.WidthOverrided = true;
                    resizingCell1.SetCellSizes(newBounds, false, true, true, true);
                    rectangleElement.SetCellSizes(bounds2, false, true, true, true);
                  }
                  else
                  {
                    resizingCell1.SetHeightForUser(newBounds.Height, false, false);
                    rectangleElement.SetHeightForUser(bounds2.Height, false, false);
                  }
                }
                else
                {
                  bounds1 = resizingCell1.Bounds;
                  if (this.Vertical)
                  {
                    RowColParams gridColumnParams = resizingCell1.GetGridColumnParams();
                    resizingCell1.WidthOverrided = gridColumnParams == null || (double) gridColumnParams.Size != (double) newBounds.Width;
                    resizingCell1.SetCellSizes(newBounds, false, true, true, true);
                  }
                  else if ((double) bounds1.Height != (double) newBounds.Height)
                  {
                    resizingCell1.HeightOverrided = true;
                    resizingCell1.SetHeightForUser(newBounds.Height, false, false);
                  }
                }
              }
            }
          }
          if (grabZone == GrabHandleZone.Left)
          {
            if (this.resizingCells.Count > 0)
            {
              if (this.resizingCells[0] != null)
              {
                TableData topLevelTable2 = this.resizingCells[0].TopLevelTable;
                newBounds = this.CalcNewElementBounds(grabZone, topLevelTable2.bounds, startPoint, delta);
                topLevelTable2.SetCellSizes(newBounds, false, true, true, true, false);
              }
            }
          }
        }
        finally
        {
          topLevelTable1.ResumeUpdateLayout(false, true);
          topLevelTable1.ResumeUpdateRefreshUI(true, true);
        }
      }
      else
      {
        TableData parentCell = element1.ParentCell;
        if (parentCell != null)
        {
          RectangleF rectangleF = this.CalcNewElementBounds(grabZone, startPoint, delta);
          if (this.Vertical)
          {
            if (element1 != element1.TopLevelTable && (double) element1.bounds.Right == (double) element1.TopLevelTable.bounds.Right)
            {
              element1.TopLevelTable.SuspendUpdateGeometryRefreshUI();
              element1.TopLevelTable.SuspendUpdateLayout();
              try
              {
                RectangleF bounds4 = element1.TopLevelTable.bounds;
                bounds4.Width = rectangleF.Right - bounds4.Left;
                element1.TopLevelTable.SetCellSizes(bounds4, false, true, true, true, false);
              }
              finally
              {
                element1.TopLevelTable.ResumeUpdateLayout(false, true);
                element1.TopLevelTable.ResumeUpdateRefreshUI(true, true);
              }
            }
            else
            {
              int num1 = firstColIndex;
              if (firstColIndex != -1)
              {
                TableData paramsOwner;
                bool fromTemplate;
                List<RowColParams> rowColParamsList = parentCell.GetGridColumnsParams(out paramsOwner, out fromTemplate, true, true) ?? new List<RowColParams>();
                float width1 = rectangleF.Width;
                if (!element1.IsDefaultGridPos)
                {
                  int spanCount = element1.GridPos.SpanCount;
                  if (spanCount != 0)
                    num1 = firstColIndex + spanCount - 1;
                  if (num1 >= rowColParamsList.Count)
                    num1 = rowColParamsList.Count - 1;
                  width1 = (float) Math.Round((double) this.CalcGridColumnWidth(element1, firstColIndex, rectangleF.Width), 5);
                }
                if ((modifierKeys & Keys.Control) != Keys.None || grabZone == GrabHandleZone.Left)
                {
                  if (grabZone == GrabHandleZone.Left)
                    paramsOwner.SetGridColumnWidth(num1, width1, bounds1.X, false, true, true, true);
                  else
                    paramsOwner.SetGridColumnWidth(num1, width1, bounds1.Right, true, true, true, true);
                }
                else
                {
                  int num2 = num1 + 1;
                  if (rowColParamsList != null && num2 < rowColParamsList.Count && rowColParamsList[num2] != null)
                  {
                    float num3 = (float) Math.Round((double) rowColParamsList[num1].Size + (double) rowColParamsList[num2].Size, 5);
                    float width2 = (float) Math.Round((double) num3 - (double) width1, 5);
                    if ((double) width2 < 0.0)
                    {
                      width1 = num3;
                      width2 = 0.0f;
                    }
                    paramsOwner.SetGridColumnWidth(num1, width1, bounds1.Right, true, true, false, false);
                    paramsOwner.SetGridColumnWidth(num2, width2, bounds1.Right, false, true, true, true);
                  }
                  else
                  {
                    parentCell.GetGridColumnsParams(out paramsOwner, out fromTemplate, true, true);
                    if (paramsOwner.GridColumnsParams != null && num1 < paramsOwner.GridColumnsParams.Count)
                    {
                      paramsOwner.SetGridColumnWidth(num1, width1, bounds1.Right, true, true, true, true);
                    }
                    else
                    {
                      RectangleElement element2 = this.Element as RectangleElement;
                      RectangleF bounds5 = element2.Bounds with
                      {
                        Width = width1
                      };
                      element2.WidthOverrided = true;
                      element2.SetCellSizes(bounds5, false, true, true, true);
                    }
                  }
                }
                if (grabZone == GrabHandleZone.Left && this.resizingCells.Count > 0 && this.resizingCells[0] != null)
                {
                  TableData topLevelTable = this.resizingCells[0].TopLevelTable;
                  bounds1 = topLevelTable.bounds;
                  topLevelTable.SetCellSizes(this.CalcNewElementBounds(grabZone, bounds1, startPoint, delta), false, true, true, true, false);
                }
              }
            }
          }
          else
          {
            int gridRowIndex = element1.GetGridRowIndex();
            if (gridRowIndex != -1)
              parentCell.SetGridRowHeight(gridRowIndex, rectangleF.Height, true, true);
          }
        }
      }
      this.resizingCells.Clear();
      DocumentControl documentControl = this.DocumentControl;
      documentControl?.DocumentManager?.UpdateSelectedElementInfo();
      documentControl?.SetRulerBorders();
      documentControl?.HorzRuler.UpdateIdents();
      documentControl?.HorzRuler.Refresh();
      documentControl?.VertRuler.Refresh();
      documentControl?.ActivateInPlaceEditor();
    }
    finally
    {
      if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
        this.Element.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Вычислить новые границы исходя из перемещения области захвата</summary>
  /// <remarks>Изменяет значение NewBounds</remarks>
  /// <param name="grabZone">Область захвата</param>
  /// <param name="delta">Перемещение области захвата</param>
  public void CalcNewBounds(GrabHandleZone grabZone, Point delta)
  {
    this.NewBounds = this.Bounds;
    Rectangle newBounds = this.NewBounds;
    switch (grabZone)
    {
      case GrabHandleZone.Center:
        newBounds.Location = new Point(newBounds.X + delta.X, newBounds.Y + delta.Y);
        break;
      case GrabHandleZone.Top:
        newBounds.Location = new Point(newBounds.X, newBounds.Y + delta.Y);
        break;
      case GrabHandleZone.Right:
        newBounds.Size = new Size(newBounds.Width + delta.X, newBounds.Height);
        break;
      case GrabHandleZone.Bottom:
        newBounds.Size = new Size(newBounds.Width, newBounds.Height + delta.Y);
        break;
      case GrabHandleZone.Left:
        newBounds.Location = new Point(newBounds.X + delta.X, newBounds.Y);
        break;
    }
    if (newBounds.Width < 0)
      newBounds.Width = 0;
    if (newBounds.Height < 0)
      newBounds.Height = 0;
    this.NewBounds = newBounds;
  }

  private RectangleF CalcNewElementBounds(GrabHandleZone grabZone, PointF delta)
  {
    if (!(this.element is RectangleElement element))
      return RectangleF.Empty;
    RectangleF bounds = element.Bounds;
    return this.CalcNewElementBounds(grabZone, bounds, delta);
  }

  private RectangleF CalcNewElementBounds(
    GrabHandleZone grabZone,
    RectangleF oldBounds,
    PointF delta)
  {
    RectangleF rect = oldBounds;
    switch (grabZone)
    {
      case GrabHandleZone.Center:
        rect.Location = new PointF(oldBounds.X + delta.X, oldBounds.Y + delta.Y);
        break;
      case GrabHandleZone.Top:
        rect = new RectangleF(oldBounds.X, oldBounds.Y + delta.Y, oldBounds.Width, oldBounds.Bottom - (oldBounds.Y + delta.Y));
        break;
      case GrabHandleZone.Right:
        rect = new RectangleF(oldBounds.X, oldBounds.Y, oldBounds.Width + delta.X, oldBounds.Height);
        break;
      case GrabHandleZone.Bottom:
        rect = new RectangleF(oldBounds.X, oldBounds.Y, oldBounds.Width, oldBounds.Height + delta.Y);
        break;
      case GrabHandleZone.Left:
        rect = new RectangleF(oldBounds.X + delta.X, oldBounds.Y, oldBounds.Right - (oldBounds.X + delta.X), oldBounds.Height);
        break;
    }
    return UnitsConverter.RoundPectangle(PageControl.NormalRectangle(rect), 5);
  }

  private RectangleF CalcNewElementBounds(GrabHandleZone grabZone, Point startPoint, Point delta)
  {
    if (!(this.element is RectangleElement element))
      return RectangleF.Empty;
    RectangleF bounds = element.Bounds;
    return this.CalcNewElementBounds(grabZone, bounds, startPoint, delta);
  }

  private RectangleF CalcNewElementBounds(
    GrabHandleZone grabZone,
    RectangleF oldBounds,
    Point startPoint,
    Point delta)
  {
    if (this.PageControl == null)
      return oldBounds;
    RectangleF rect = oldBounds;
    PointF world = this.PixelToWorld(new Point(startPoint.X + delta.X, startPoint.Y + delta.Y), grabZone != 0, (VisualNode) null);
    switch (grabZone)
    {
      case GrabHandleZone.Center:
        Rectangle bounds1 = this.Bounds;
        ref Rectangle local = ref bounds1;
        Rectangle bounds2 = this.Bounds;
        int x = bounds2.X + delta.X;
        bounds2 = this.Bounds;
        int y = bounds2.Y + delta.Y;
        Point point = new Point(x, y);
        local.Location = point;
        rect = this.Page.PageUI.SnapRectangle(this.Page.PageUI.ConvertPixelToWorld(bounds1), (VisualNode) null);
        break;
      case GrabHandleZone.Top:
        PointF pointF1 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(oldBounds.X, pointF1.Y, oldBounds.Width, oldBounds.Bottom - pointF1.Y);
        break;
      case GrabHandleZone.Right:
        PointF pointF2 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(oldBounds.X, oldBounds.Y, pointF2.X - oldBounds.X, oldBounds.Height);
        break;
      case GrabHandleZone.Bottom:
        PointF pointF3 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(oldBounds.X, oldBounds.Y, oldBounds.Width, pointF3.Y - oldBounds.Y);
        break;
      case GrabHandleZone.Left:
        PointF pointF4 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(pointF4.X, oldBounds.Y, oldBounds.Right - pointF4.X, oldBounds.Height);
        break;
    }
    rect = UnitsConverter.RoundPectangle(PageControl.NormalRectangle(rect), 5);
    return rect;
  }

  /// <summary>Обработчик события изменения положения точки</summary>
  /// <param name="startPoint">Точка с которой началось движение</param>
  /// <param name="delta">Смещение от этой точки</param>
  public override void ChangingPoint(Point startPoint, Point delta)
  {
    if (this.GeometryChangingBlocked)
      return;
    GrabHandleZone grabZone = GrabHandleZone.Center;
    if (!this.GetGrabHandleZone(startPoint, out grabZone))
      return;
    PageControl pageControl = this.PageControl;
    if (this.Page == null || this.Page.PageUI == null)
      return;
    this.movingCursor = this.GetGrabHandleZoneCursor(grabZone);
    this.currentGrabZone = grabZone;
    RectangleF rectangleF = this.CalcNewElementBounds(grabZone, startPoint, delta);
    this.NewBounds = this.Page.PageUI.ConvertWorldToPixel(rectangleF);
    this.elementBounds = rectangleF;
    base.ChangingPoint(startPoint, delta);
    this.DrawNewBoundsPreview((Graphics) null);
    this.DocumentControl?.OnSelectedElementBoundsChanging(new BoundsChangingEventArgs((DocumentTreeNode) this.element, rectangleF));
    if (this.DocumentControl == null || this.DocumentControl.DocumentManager == null)
      return;
    RectangleF user = this.Page.PageUI.ConvertInternalToUser(rectangleF);
    if (this.Vertical)
      this.DocumentControl.DocumentManager.SetMessageText(LocalizationHolder.rm.GetString("Document.Model_146") + user.Width.ToString());
    else
      this.DocumentControl.DocumentManager.SetMessageText(LocalizationHolder.rm.GetString("Document.Model_147") + user.Height.ToString());
  }

  private float CalcGridColumnWidth(RectangleElement cell, int firstColIndex, float newCellWidth)
  {
    TableData parentCell = cell.ParentCell;
    if (parentCell == null || cell.IsDefaultGridPos)
      return newCellWidth;
    List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
    TableGridPosition gridPos = cell.GridPos;
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

  /// <summary>Вычислить новые границы элемента на основе значения NewBounds</summary>
  /// <returns>Новые границы элемента</returns>
  protected virtual RectangleF CalcNewElementBounds()
  {
    RectangleF rectangleF = RectangleF.Empty;
    if (this.Element is RectangleElement element && element.Page != null)
    {
      Rectangle newBounds = this.NewBounds;
      if (newBounds.Width < 0)
        newBounds.Width = 0;
      if (newBounds.Height < 0)
        newBounds.Height = 0;
      RectangleF world = ((Page) this.Element.Page).ConvertPixelToWorld(newBounds);
      RectangleF empty = RectangleF.Empty;
      RectangleF bounds = element.Bounds;
      Rectangle pixel = this.Element.Page.ConvertWorldToPixel(bounds);
      rectangleF = this.Page.PageUI.SnapToGrid(this.TrimChanges(bounds, world, pixel, newBounds));
    }
    return rectangleF;
  }

  /// <summary>Обновить геометрию</summary>
  public override void UpdateGeometry()
  {
    if (this.Element is RectangleElement element && element.Page != null)
    {
      this.Bounds = element.Page.ConvertWorldToPixel(element.Bounds);
      this.NewBounds = this.Bounds;
    }
    base.UpdateGeometry();
  }

  /// <summary>Обновить геометрию элемента страницы</summary>
  public override void UpdateElementGeometry()
  {
    if (this.Element is RectangleElement element && element.Page != null)
    {
      RectangleF bounds = element.Bounds;
      RectangleF rectangleF = this.CalcNewElementBounds();
      if ((double) bounds.Width != (double) rectangleF.Width)
        element.WidthOverrided = true;
      if ((double) bounds.Height != (double) rectangleF.Height)
        element.HeightOverrided = true;
      element.AssignBounds(rectangleF, true, true, true);
    }
    base.UpdateElementGeometry();
  }

  /// <summary>Стереть предпосмотр новых границ</summary>
  public virtual void EraseNewBoundsPreview()
  {
    if (!this.newBoundsPreviewDrawed)
      return;
    this.newBoundsPreviewDrawed = false;
    if (this.PageControl == null)
      return;
    this.PageControl.NeedDrawPopupBar = true;
    Rectangle previewBounds = this.previewBounds;
    --previewBounds.X;
    --previewBounds.Y;
    previewBounds.Width += 2;
    previewBounds.Height += 2;
    this.PageControl.Invalidate(previewBounds, true);
    this.PageControl.Update();
  }

  /// <summary>Нарисовать предпросмотр новых границ</summary>
  /// <param name="g">Graphics</param>
  public virtual void DrawNewBoundsPreview(Graphics g)
  {
    if (this.PageControl == null || this.Page.PageUI == null)
      return;
    PageControl pageControl = this.PageControl;
    bool flag = g == null;
    if (g == null)
    {
      if (this.newBoundsPreviewDrawed)
        this.EraseNewBoundsPreview();
      g = this.PageControl.CreateGraphics();
    }
    try
    {
      Point point = Point.Empty;
      Point end = Point.Empty;
      Point mousePosition = this.mousePosition;
      RectangleElement element = this.Element as RectangleElement;
      RectangleElement rectangleElement = (RectangleElement) null;
      if (element != null)
        rectangleElement = element.NextNode;
      Matrix userCoorMatrix = this.Page.PageUI.GetUserCoorMatrix();
      float? offsetFromLeft = new float?();
      float? offsetFromRight = new float?();
      float? leftCellSize = new float?();
      float? rightCellSize = new float?();
      if (this.Vertical)
      {
        if (this.movingGrabHandleZone == GrabHandleZone.Right)
        {
          point = new Point(this.NewBounds.Right, 0);
          end = new Point(this.NewBounds.Right, this.PageControl.Height);
        }
        else
        {
          point = new Point(this.NewBounds.X, 0);
          end = new Point(this.NewBounds.X, this.PageControl.Height);
        }
        this.previewBounds = new Rectangle(point, new Size(2, this.PageControl.Height));
        if (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
        {
          pageControl.IsPopupBarHorizontal = true;
          mousePosition.X = point.X;
          if (element != null)
          {
            RectangleF bounds;
            float num;
            if ((double) element.Bounds.Right != (double) this.elementBounds.Right)
            {
              double right1 = (double) this.elementBounds.Right;
              bounds = element.Bounds;
              double right2 = (double) bounds.Right;
              num = (float) (right1 - right2);
            }
            else
            {
              double left1 = (double) this.elementBounds.Left;
              bounds = element.Bounds;
              double left2 = (double) bounds.Left;
              num = (float) (left1 - left2);
            }
            if (this.movingGrabHandleZone == GrabHandleZone.Right)
            {
              ref float? local1 = ref leftCellSize;
              PageUI pageUi1 = this.Page.PageUI;
              bounds = element.Bounds;
              double distance1 = (double) bounds.Width + (double) num;
              Matrix m1 = userCoorMatrix;
              double user1 = (double) pageUi1.ConvertInternalDistanceToUser((float) distance1, m1);
              local1 = new float?((float) user1);
              ref float? local2 = ref offsetFromLeft;
              PageUI pageUi2 = this.Page.PageUI;
              bounds = element.Bounds;
              double distance2 = (double) bounds.Right + (double) num;
              Matrix m2 = userCoorMatrix;
              double user2 = (double) pageUi2.ConvertInternalDistanceToUser((float) distance2, m2);
              local2 = new float?((float) user2);
              if (this.Element.Page != null)
              {
                ref float? local3 = ref offsetFromRight;
                PageUI pageUi3 = this.Page.PageUI;
                double width = (double) this.Element.Page.Size.Width;
                bounds = element.Bounds;
                double right = (double) bounds.Right;
                double distance3 = width - right - (double) num;
                Matrix m3 = userCoorMatrix;
                double user3 = (double) pageUi3.ConvertInternalDistanceToUser((float) distance3, m3);
                local3 = new float?((float) user3);
              }
              if (rectangleElement != null)
              {
                ref float? local4 = ref rightCellSize;
                PageUI pageUi4 = this.Page.PageUI;
                bounds = rectangleElement.Bounds;
                double distance4 = (double) bounds.Width - (double) num;
                Matrix m4 = userCoorMatrix;
                double user4 = (double) pageUi4.ConvertInternalDistanceToUser((float) distance4, m4);
                local4 = new float?((float) user4);
              }
            }
            else
            {
              ref float? local5 = ref offsetFromLeft;
              PageUI pageUi5 = this.Page.PageUI;
              bounds = element.Bounds;
              double distance5 = (double) bounds.Left + (double) num;
              Matrix m5 = userCoorMatrix;
              double user5 = (double) pageUi5.ConvertInternalDistanceToUser((float) distance5, m5);
              local5 = new float?((float) user5);
              if (this.Element.Page != null)
              {
                ref float? local6 = ref offsetFromRight;
                PageUI pageUi6 = this.Page.PageUI;
                double width = (double) this.Element.Page.Size.Width;
                bounds = element.Bounds;
                double left = (double) bounds.Left;
                double distance6 = width - left + (double) num;
                Matrix m6 = userCoorMatrix;
                double user6 = (double) pageUi6.ConvertInternalDistanceToUser((float) distance6, m6);
                local6 = new float?((float) user6);
              }
              ref float? local7 = ref rightCellSize;
              PageUI pageUi7 = this.Page.PageUI;
              bounds = element.Bounds;
              double distance7 = (double) bounds.Width - (double) num;
              Matrix m7 = userCoorMatrix;
              double user7 = (double) pageUi7.ConvertInternalDistanceToUser((float) distance7, m7);
              local7 = new float?((float) user7);
            }
          }
        }
      }
      else
      {
        point = new Point(0, this.NewBounds.Bottom);
        end = new Point(pageControl.Width, this.NewBounds.Bottom);
        this.previewBounds = new Rectangle(point, new Size(pageControl.Width, 2));
        if (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
        {
          pageControl.IsPopupBarHorizontal = false;
          mousePosition.Y = point.Y;
          if (element != null)
          {
            RectangleF bounds;
            float num1;
            if ((double) element.Bounds.Top != (double) this.elementBounds.Top)
            {
              double top1 = (double) this.elementBounds.Top;
              bounds = element.Bounds;
              double top2 = (double) bounds.Top;
              num1 = (float) (top1 - top2);
            }
            else
            {
              double bottom1 = (double) this.elementBounds.Bottom;
              bounds = element.Bounds;
              double bottom2 = (double) bounds.Bottom;
              num1 = (float) (bottom1 - bottom2);
            }
            if (this.movingGrabHandleZone == GrabHandleZone.Bottom)
            {
              ref float? local8 = ref leftCellSize;
              bounds = element.Bounds;
              double num2 = (double) bounds.Height + (double) num1;
              local8 = new float?((float) num2);
              ref float? local9 = ref offsetFromLeft;
              bounds = element.Bounds;
              double num3 = (double) bounds.Bottom + (double) num1;
              local9 = new float?((float) num3);
              if (this.Element.Page != null)
              {
                ref float? local10 = ref offsetFromRight;
                double height = (double) this.Element.Page.Size.Height;
                bounds = element.Bounds;
                double bottom = (double) bounds.Bottom;
                double num4 = height - bottom - (double) num1;
                local10 = new float?((float) num4);
              }
              if (rectangleElement != null)
              {
                ref float? local11 = ref rightCellSize;
                bounds = rectangleElement.Bounds;
                double height = (double) bounds.Height;
                local11 = new float?((float) height);
              }
            }
            else
            {
              ref float? local12 = ref offsetFromLeft;
              bounds = element.Bounds;
              double num5 = (double) bounds.Top + (double) num1;
              local12 = new float?((float) num5);
              ref float? local13 = ref offsetFromRight;
              double height = (double) pageControl.Size.Height;
              bounds = element.Bounds;
              double top = (double) bounds.Top;
              double num6 = height - top + (double) num1;
              local13 = new float?((float) num6);
              ref float? local14 = ref rightCellSize;
              bounds = element.Bounds;
              double num7 = (double) bounds.Height - (double) num1;
              local14 = new float?((float) num7);
            }
          }
        }
      }
      if (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
      {
        pageControl.DrawLine = false;
        pageControl.PopupBarPosition = mousePosition;
        pageControl.SetBarValues(offsetFromLeft, offsetFromRight, leftCellSize, rightCellSize);
        pageControl.PreparePopupBar();
        pageControl.Invalidate(pageControl.RegionForInvalidate);
        pageControl.Update();
      }
      RubberBand.DrawXorLine(g, point, end, Color.White);
      this.newBoundsPreviewDrawed = true;
    }
    finally
    {
      if (flag && g != null)
        g.Dispose();
    }
  }

  /// <summary>Управляет вертикальной границей ячейки</summary>
  public bool Vertical
  {
    [DebuggerStepThrough] get
    {
      return this.Element is RectangleElement element && element.ParentCell != null && element.ParentCell.IsRow;
    }
  }

  internal override void PreprocessControlMouseDown(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    if (this.PageControl == null)
      return;
    if (sender is Control control)
    {
      Point pageCoor = this.ControlCoorToPageCoor(control, new Point(e.X, e.Y));
      if (e.Button == MouseButtons.Left)
        this.leftMouseDownPos = pageCoor;
      bool flag = false;
      if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.PageControl != null)
      {
        GrabHandleZone grabZone = GrabHandleZone.Center;
        Rectangle rectangle;
        if (this.RowSelectionEnabled)
        {
          rectangle = this.RowSelectionZone();
          if (rectangle.Contains(pageCoor))
          {
            flag = true;
            goto label_18;
          }
        }
        if (this.CanSelectColumnCells())
        {
          rectangle = this.ColumnSelectionZone();
          if (rectangle.Contains(pageCoor))
          {
            flag = true;
            goto label_18;
          }
        }
        if (this.CanSelectCell())
        {
          rectangle = this.CellSelectionZone();
          if (rectangle.Contains(pageCoor))
          {
            flag = true;
            goto label_18;
          }
        }
        if (this.GetGrabHandleZone(pageCoor, out grabZone))
        {
          flag = false;
        }
        else
        {
          rectangle = this.Bounds;
          if (rectangle.Contains(pageCoor))
            flag = true;
        }
      }
label_18:
      if (flag)
      {
        this.PageControl.elementAtCursor = (PageElementUI) this;
        cancelEventArgs.Cancel = true;
        this.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, pageCoor.X, pageCoor.Y, e.Delta));
      }
    }
    base.PreprocessControlMouseDown(sender, e, cancelEventArgs);
  }

  /// <summary>Выделить ячейки</summary>
  /// <param name="cells">ячейка</param>
  internal void SelectElements(RectangleElement cells)
  {
    if (cells == null)
      return;
    PageControl pageControl = this.PageControl;
    if (cells.IsVirtualNode || pageControl.ActiveElement != cells)
    {
      this.isActiveElement = pageControl.ActiveElement == this.element;
      this.SelectElement((DocumentTreeNode) cells, Control.ModifierKeys, false, Point.Empty, false, false);
      pageControl.elementAtCursor = (PageElementUI) this;
    }
    else
    {
      this.isActiveElement = pageControl.ActiveElement == this.element;
      this.isSelected = this.isActiveElement;
      if (!(((IPageElementWithInterface) this.Element).InPlaceEditorControl is ImRtfEditor placeEditorControl))
        return;
      if (TableCellUI.oldEditorCursor != (Cursor) null)
      {
        placeEditorControl.Cursor = TableCellUI.oldEditorCursor;
        TableCellUI.oldEditorCursor = (Cursor) null;
      }
      placeEditorControl.Visible = true;
    }
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  internal override void OnMouseDown(MouseEventArgs e)
  {
    Point point = new Point(e.X, e.Y);
    if (e.Button == MouseButtons.Left)
      this.leftMouseDownPos = point;
    PageElementNode element = this.element;
    if (this.PageControl == null)
      return;
    Point screen = this.PageControl.PointToScreen(point);
    if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
    {
      this.PageControl.LastSelectedElem = this.Element as RectangleElement;
      if (Control.ModifierKeys == Keys.Shift)
      {
        PageControl pageControl = this.PageControl;
        this.SelectElements(this.GetCellsInRectangleFromSelectedTable(point, false));
      }
      else
      {
        GrabHandleZone grabZone = GrabHandleZone.Center;
        if (this.RowSelectionEnabled && this.RowSelectionZone().Contains(point))
          this.SelectElement(Control.ModifierKeys, false, Point.Empty, false, false);
        else if (this.CanSelectColumnCells() && this.ColumnSelectionZone().Contains(point))
        {
          List<DocumentTreeNode> columnCells = this.GetColumnCells();
          if (columnCells != null && columnCells.Count > 0)
          {
            VirtualColumn virtualColumn = this.GetVirtualColumn((IList<DocumentTreeNode>) columnCells);
            if (virtualColumn != null)
            {
              columnCells.Clear();
              columnCells.Add((DocumentTreeNode) virtualColumn);
            }
            this.SelectElements(columnCells, Control.ModifierKeys, false, true, Point.Empty, false, false);
          }
        }
        else if (this.CanSelectCell() && this.CellSelectionZone().Contains(point))
          this.SelectElement(Control.ModifierKeys, false, Point.Empty, false, false);
        else if (this.GetGrabHandleZone(point, out grabZone))
        {
          if (grabZone == GrabHandleZone.Center)
          {
            bool flag1 = !this.Element.InPlaceEditorActive;
            bool inPlaceEditEnabled = Control.ModifierKeys == Keys.None && this.element.CanActivateInPlaceEditor;
            this.SelectElement(Control.ModifierKeys, inPlaceEditEnabled, e.Location, false, false);
            if (!element.InPlaceEditorActive & inPlaceEditEnabled && element is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI.IsSelected)
              elementWithInterface.ActivateInPlaceEditor(elementWithInterface.PageUI, e);
            bool flag2 = flag1 && element.InPlaceEditorActive;
            if (((IPageElementWithInterface) element).InPlaceEditorControl is ImRtfEditor placeEditorControl)
              placeEditorControl.Visible = true;
            if (flag2 && placeEditorControl != null)
            {
              Point client = placeEditorControl.PointToClient(screen);
              MouseEventArgs ev = new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
              placeEditorControl.Cursor = Cursors.IBeam;
              placeEditorControl.FireMouseDown(ev);
              placeEditorControl.Capture = true;
            }
          }
        }
        else if (this.Bounds.Contains(point))
          this.SelectElement(Control.ModifierKeys, false, Point.Empty, false, false);
      }
    }
    base.OnMouseDown(e);
  }

  internal override void PreprocessControlMouseMove(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    PageControl pageControl = this.PageControl;
    if (pageControl == null)
      return;
    bool flag = false;
    if (sender is Control control)
    {
      Point pageCoor = this.ControlCoorToPageCoor(control, new Point(e.X, e.Y));
      if (pageCoor == this.prevPoint)
        return;
      if (e.Button == MouseButtons.Left)
      {
        if (pageControl.IsTableRowsSelecting)
          flag = true;
        else if (pageControl.IsTableColumnsSelecting)
          flag = true;
        else if (pageControl.IsTableCellsSelecting)
        {
          if (this.CellSelectionZone().Contains(this.leftMouseDownPos))
            flag = true;
        }
        else if (!this.IsMoving && this.RowSelectionEnabled && this.RowSelectionZone().Contains(this.leftMouseDownPos))
          flag = true;
        else if (!this.IsMoving && this.CanSelectColumnCells() && this.ColumnSelectionZone().Contains(this.leftMouseDownPos))
          flag = true;
        else if (!this.IsMoving && this.CanSelectCell() && this.CellSelectionZone().Contains(this.leftMouseDownPos))
        {
          RectangleElement fromSelectedTable = this.GetCellsInRectangleFromSelectedTable(pageCoor, true);
          if (fromSelectedTable != null)
            flag = fromSelectedTable.IsVirtualNode || pageControl.ActiveElement == fromSelectedTable;
        }
      }
      else if (!this.IsMoving && this.RowSelectionEnabled && this.RowSelectionZone().Contains(pageCoor))
        flag = true;
      else if (!this.IsMoving && this.CanSelectColumnCells() && this.ColumnSelectionZone().Contains(pageCoor))
        flag = true;
      else if (!this.IsMoving && this.CanSelectCell() && this.CellSelectionZone().Contains(pageCoor))
        flag = true;
      if (!flag)
      {
        if (!this.Bounds.Contains(pageCoor))
        {
          RectangleElement fromSelectedTable = this.GetCellsInRectangleFromSelectedTable(pageCoor, true);
          if (fromSelectedTable != null)
            flag = fromSelectedTable.IsVirtualNode || pageControl.ActiveElement == fromSelectedTable;
        }
        else
        {
          if (sender is ImRtfEditor imRtfEditor && e.Button == MouseButtons.Left)
            imRtfEditor.Capture = true;
          pageControl.SetTableCellsSelectingMode(false, (TableElement) null);
        }
      }
      if (flag)
      {
        pageControl.elementAtCursor = (PageElementUI) this;
        TableCellUI.oldEditorCursor = !(sender is ImRtfEditor imRtfEditor) ? (Cursor) null : imRtfEditor.Cursor;
        Cursor cursor = this.GetCursor(pageCoor);
        if (control.Cursor != cursor)
          control.Cursor = cursor;
        cancelEventArgs.Cancel = true;
        this.OnMouseMove(new MouseEventArgs(e.Button, e.Clicks, pageCoor.X, pageCoor.Y, e.Delta));
      }
    }
    base.PreprocessControlMouseMove(sender, e, cancelEventArgs);
  }

  /// <summary>Вызвает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  internal override void OnMouseMove(MouseEventArgs e)
  {
    this.mousePosition = new Point(e.X, e.Y);
    PageControl pageControl = this.PageControl;
    if (pageControl == null)
      return;
    Point point = new Point(e.X, e.Y);
    if (point == this.prevPoint)
      return;
    bool flag1 = false;
    if (e.Button == MouseButtons.Left)
    {
      flag1 = true;
      if (!this.IsMoving && pageControl.IsTableRowsSelecting)
      {
        Rectangle rect = Rectangle.FromLTRB(this.leftMouseDownPos.X, this.leftMouseDownPos.Y, point.X, point.Y);
        if (rect.Width == 0)
          rect.Width = 1;
        if (rect.X < this.Bounds.X)
          rect.Width = this.Bounds.X - rect.X;
        if (rect.Height == 0)
          rect.Height = 1;
        List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
        PageElementUI elementUiAtPoint = this.PageControl.GetPageElementUIAtPoint(point, true);
        TableElement tableElement = (TableElement) null;
        if (elementUiAtPoint != null && elementUiAtPoint.Element is RectangleElement && (elementUiAtPoint.Element as RectangleElement).ParentCell != null)
          tableElement = (elementUiAtPoint.Element as RectangleElement).ParentCell as TableElement;
        while (tableElement != null && !tableElement.IsColumn)
          tableElement = tableElement.ParentCell as TableElement;
        if (tableElement != null)
          tableElement = (TableElement) tableElement.TopLevelTable;
        TableCellUI tableUI = (TableCellUI) null;
        if (tableElement != null)
          tableUI = tableElement.PageUI as TableCellUI;
        if (tableUI == null)
          tableUI = pageControl.SelectedTable.PageUI as TableCellUI;
        if (tableUI != null)
          tableUI.GetRowsInRectangle((PageElementUI) tableUI, rect, (IList<DocumentTreeNode>) documentTreeNodeList);
        else if (pageControl.SelectedTable.PageUI is TableUI pageUi)
          this.GetRowsInRectangle((PageElementUI) pageUi, rect, (IList<DocumentTreeNode>) documentTreeNodeList);
        this.SelectElements(documentTreeNodeList, Control.ModifierKeys, false, false, Point.Empty, false, false);
        pageControl.Capture = true;
        pageControl.elementAtCursor = (PageElementUI) this;
      }
      else if (!this.IsMoving && pageControl.IsTableColumnsSelecting)
      {
        Rectangle rect = PageControl.NormalRectangle(point, this.leftMouseDownPos);
        if (rect.Width == 0)
          rect.Width = 1;
        if (rect.Height == 0)
          rect.Height = 1;
        List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
        PageElementUI elementUiAtPoint = this.PageControl.GetPageElementUIAtPoint(point, true);
        TableElement tableElement = (TableElement) null;
        if (elementUiAtPoint != null && elementUiAtPoint.Element is RectangleElement && (elementUiAtPoint.Element as RectangleElement).ParentCell != null)
          tableElement = (elementUiAtPoint.Element as RectangleElement).ParentCell as TableElement;
        while (tableElement != null && !tableElement.IsColumn)
          tableElement = tableElement.ParentCell as TableElement;
        TableCellUI tableCellUi = (TableCellUI) null;
        if (tableElement != null)
          tableCellUi = tableElement.PageUI as TableCellUI;
        if (tableCellUi == null)
          tableCellUi = pageControl.SelectedTable.PageUI as TableCellUI;
        if (tableCellUi != null)
          tableCellUi.GetColumnsInRectangle(rect, documentTreeNodeList);
        else if (pageControl.SelectedTable.PageUI is TableUI pageUi)
          this.GetColumnsInRectangle(pageUi, rect, documentTreeNodeList);
        this.SelectElements(documentTreeNodeList, Control.ModifierKeys, false, false, Point.Empty, false, false);
        pageControl.Capture = true;
        pageControl.elementAtCursor = (PageElementUI) this;
      }
      else if (!this.IsMoving && pageControl.IsTableCellsSelecting)
      {
        if (TableCellUI.previewSelectionCells != null)
          DocumentControl.SetShowSelected((DocumentTreeNode) TableCellUI.previewSelectionCells, false, false);
        RectangleElement node = this.GetCellsInRectangleFromSelectedTable(point, true);
        if (node != null)
        {
          ImRtfEditor placeEditorControl = ((IPageElementWithInterface) this.Element).InPlaceEditorControl as ImRtfEditor;
          if (node.IsVirtualNode || pageControl.ActiveElement != node || this.CellSelectionZone().Contains(this.leftMouseDownPos))
          {
            pageControl.Capture = true;
            pageControl.elementAtCursor = (PageElementUI) this;
            if (placeEditorControl != null)
              placeEditorControl.Visible = false;
            this.InvalidateUI();
            this.isActiveElement = false;
          }
          else
          {
            this.isActiveElement = pageControl.ActiveElement == this.element;
            this.isSelected = this.isActiveElement;
            if (placeEditorControl != null)
              placeEditorControl.Capture = true;
            if (TableCellUI.oldEditorCursor != (Cursor) null)
            {
              if (placeEditorControl != null)
                placeEditorControl.Cursor = TableCellUI.oldEditorCursor;
              TableCellUI.oldEditorCursor = (Cursor) null;
            }
            if (placeEditorControl != null)
              placeEditorControl.Visible = true;
            pageControl.SetTableCellsSelectingMode(false, (TableElement) null);
            node = (RectangleElement) null;
          }
        }
        if (node != null)
          DocumentControl.SetShowSelected((DocumentTreeNode) node, true, false);
        if (TableCellUI.previewSelectionCells != null)
          DocumentControl.RefreshShowSelected((DocumentTreeNode) TableCellUI.previewSelectionCells, false);
        if (node != null)
          DocumentControl.RefreshShowSelected((DocumentTreeNode) node, true);
        TableCellUI.previewSelectionCells = node;
        pageControl.Update();
      }
      else if (!this.IsMoving && this.RowSelectionEnabled && this.RowSelectionZone().Contains(this.leftMouseDownPos))
      {
        pageControl.SetTableRowsSelectingMode(true, (TableElement) ((RectangleElement) this.Element).TopLevelTable);
        this.PageControl.FirstSelectedElem = (RectangleElement) null;
        this.PageControl.LastSelectedElem = (RectangleElement) null;
      }
      else if (!this.IsMoving && this.CanSelectColumnCells() && this.ColumnSelectionZone().Contains(this.leftMouseDownPos))
        pageControl.SetTableColumnsSelectingMode(true, (TableElement) ((RectangleElement) this.Element).TopLevelTable);
      else
        flag1 = false;
    }
    if (!flag1)
      base.OnMouseMove(e);
    if (e.Button == MouseButtons.Left && !flag1)
    {
      bool flag2 = false;
      if (!this.IsMoving && this.CanSelectCell() && this.DocumentControl.SelectedNodes.Count == 1)
      {
        TableData selectedNode = this.DocumentControl.SelectedNodes[0] as TableData;
        bool flag3 = false;
        if (this.DocumentControl.RowSelection && selectedNode != null && selectedNode.IsVirtualNode)
        {
          flag3 = true;
          foreach (DocumentTreeNode realCell in selectedNode.GetRealCells())
          {
            if (!(realCell is TableData) || !(realCell as TableData).IsRow)
            {
              flag3 = false;
              break;
            }
          }
        }
        if (!this.DocumentControl.MultiSelect)
          flag3 = true;
        if ((selectedNode == null || !selectedNode.IsRow) && !flag3)
        {
          flag2 = true;
          RectangleElement node = this.GetCellsInRectangleFromSelectedTable(point, true);
          if (node != null)
          {
            ImRtfEditor placeEditorControl = ((IPageElementWithInterface) this.Element).InPlaceEditorControl as ImRtfEditor;
            if (node.IsVirtualNode || pageControl.ActiveElement != node || this.CellSelectionZone().Contains(this.leftMouseDownPos))
            {
              pageControl.SetTableCellsSelectingMode(true, (TableElement) ((RectangleElement) this.Element).TopLevelTable);
              if (placeEditorControl != null)
                placeEditorControl.Visible = false;
              pageControl.Capture = true;
              pageControl.elementAtCursor = (PageElementUI) this;
              this.InvalidateUI();
              this.isActiveElement = false;
              this.isSelected = this.isActiveElement;
            }
            else
            {
              this.isActiveElement = pageControl.ActiveElement == this.element;
              this.isSelected = this.isActiveElement;
              if (placeEditorControl != null)
                placeEditorControl.Visible = true;
              node = (RectangleElement) null;
            }
            if (TableCellUI.previewSelectionCells != null)
              DocumentControl.SetShowSelected((DocumentTreeNode) TableCellUI.previewSelectionCells, false, false);
            if (node != null)
              DocumentControl.SetShowSelected((DocumentTreeNode) node, true, false);
            if (TableCellUI.previewSelectionCells != null)
              DocumentControl.RefreshShowSelected((DocumentTreeNode) TableCellUI.previewSelectionCells, false);
            if (node != null)
              DocumentControl.RefreshShowSelected((DocumentTreeNode) node, true);
            TableCellUI.previewSelectionCells = node;
            pageControl.Update();
          }
        }
      }
      if (!flag2)
      {
        BeforeDoDragDrop_EventArgs e1 = new BeforeDoDragDrop_EventArgs(false);
        this.PageControl.OnBeforeDoDragDrop(e1);
        if (e1.DoDragDrop)
        {
          int num1 = (int) this.PageControl.DoDragDrop(e1.ObjectToDrag, e1.Effect);
        }
        else if (this.DocumentControl.DocumentManager == null || this.DocumentControl.DocumentManager.GetType().Name == "DocumentEditorPlugin" || this.DocumentControl.DocumentManager.GetType().Name == "DocumentEditorMainForm")
        {
          bool flag4 = this.DocumentControl.SelectedNodes.Count > 0;
          if (flag4)
          {
            if (this.DocumentControl.SelectedNodes.Count > 1 || this.DocumentControl.SelectedNodes.Count == 1 && this.DocumentControl.SelectedNodes[0].IsVirtualNode)
            {
              List<DocumentTreeNode> documentTreeNodeList = this.DocumentControl.SelectedNodes.Count <= 1 ? (this.DocumentControl.SelectedNodes[0] as RectangleElement).GetRealCells() : this.DocumentControl.SelectedNodes;
              TableData tableData1 = (TableData) null;
              bool? nullable1 = new bool?();
              foreach (DocumentTreeNode documentTreeNode in documentTreeNodeList)
              {
                if (!(documentTreeNode is TableData tableData2) || !tableData2.IsRow)
                {
                  flag4 = false;
                  break;
                }
                if (nullable1.HasValue)
                {
                  bool? nullable2 = nullable1;
                  bool flag5 = tableData2.TableCellType == CellType.Header;
                  if (!(nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue))
                  {
                    flag4 = false;
                    break;
                  }
                }
                nullable1 = new bool?(tableData2.TableCellType == CellType.Header);
                if (tableData1 == null)
                  tableData1 = tableData2.OwnerSubTable;
                else if (tableData1 != tableData2.OwnerSubTable)
                {
                  flag4 = false;
                  break;
                }
              }
            }
            else if (!(this.DocumentControl.SelectedNodes[0] is TableData selectedNode) || !selectedNode.IsRow)
              flag4 = false;
          }
          if (flag4)
          {
            List<DocumentTreeNode> data = new List<DocumentTreeNode>();
            data.AddRange((IEnumerable<DocumentTreeNode>) this.DocumentControl.SelectedNodes);
            int num2 = (int) this.PageControl.DoDragDrop((object) data, DragDropEffects.Move);
          }
        }
      }
    }
    this.prevPoint = point;
  }

  internal override void PreprocessControlMouseUp(
    object sender,
    MouseEventArgs e,
    CancelEventArgs cancelEventArgs)
  {
    bool flag = false;
    Control control = sender as Control;
    PageControl pageControl = this.PageControl;
    if (control != null && this.PageControl != null)
    {
      Point pageCoor = this.ControlCoorToPageCoor(control, new Point(e.X, e.Y));
      if (e.Button == MouseButtons.Left)
      {
        if (this.IsMoving)
          flag = true;
        if (this.PageControl.IsTableRowsSelecting)
          flag = true;
        else if (this.PageControl.IsTableColumnsSelecting)
          flag = true;
        else if (this.PageControl.IsTableCellsSelecting)
        {
          if (this.CellSelectionZone().Contains(this.leftMouseDownPos))
            flag = true;
        }
        else if (this.RowSelectionEnabled && this.RowSelectionZone().Contains(pageCoor))
          flag = true;
        else if (this.CanSelectColumnCells() && this.ColumnSelectionZone().Contains(pageCoor))
          flag = true;
        else if (!this.IsMoving && this.CanSelectCell() && this.CellSelectionZone().Contains(this.leftMouseDownPos))
        {
          RectangleElement fromSelectedTable = this.GetCellsInRectangleFromSelectedTable(pageCoor, true);
          if (fromSelectedTable != null)
            flag = fromSelectedTable.IsVirtualNode || pageControl?.ActiveElement == fromSelectedTable;
        }
      }
      if (!flag && pageControl != null)
      {
        if (!this.Bounds.Contains(pageCoor))
        {
          RectangleElement fromSelectedTable = this.GetCellsInRectangleFromSelectedTable(pageCoor, true);
          if (fromSelectedTable != null)
            flag = fromSelectedTable.IsVirtualNode || pageControl.ActiveElement == fromSelectedTable;
        }
        else
        {
          if (sender is ImRtfEditor imRtfEditor && e.Button == MouseButtons.Left)
            imRtfEditor.Capture = true;
          pageControl.SetTableCellsSelectingMode(false, (TableElement) null);
        }
      }
      if (flag)
      {
        if (pageControl != null)
          pageControl.elementAtCursor = (PageElementUI) this;
        cancelEventArgs.Cancel = true;
        this.OnMouseUp(new MouseEventArgs(e.Button, e.Clicks, pageCoor.X, pageCoor.Y, e.Delta));
      }
    }
    base.PreprocessControlMouseUp(sender, e, cancelEventArgs);
  }

  /// <summary>Вызвает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  internal override void OnMouseUp(MouseEventArgs e)
  {
    Point mousePos = new Point(e.X, e.Y);
    PageControl pageControl = this.PageControl;
    if (pageControl == null)
      return;
    if (e.Button == MouseButtons.Left)
    {
      if (this.IsMoving)
      {
        Point delta = new Point(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
        this.EndMoving(e, Control.ModifierKeys, this.startPoint, delta);
      }
      else if (pageControl.IsTableCellsSelecting)
      {
        if (TableCellUI.previewSelectionCells != null)
          DocumentControl.SetShowSelected((DocumentTreeNode) TableCellUI.previewSelectionCells, false, false);
        TableCellUI.previewSelectionCells = (RectangleElement) null;
        RectangleElement fromSelectedTable = this.GetCellsInRectangleFromSelectedTable(mousePos, true);
        if (fromSelectedTable != null)
        {
          if (fromSelectedTable.IsVirtualNode || pageControl.ActiveElement != fromSelectedTable)
          {
            this.isActiveElement = pageControl.ActiveElement == this.element;
            this.SelectElement((DocumentTreeNode) fromSelectedTable, Control.ModifierKeys, false, Point.Empty, false, false);
            pageControl.elementAtCursor = (PageElementUI) this;
          }
          else
          {
            this.isActiveElement = pageControl.ActiveElement == this.element;
            this.isSelected = this.isActiveElement;
            if (((IPageElementWithInterface) this.Element).InPlaceEditorControl is ImRtfEditor placeEditorControl)
            {
              if (TableCellUI.oldEditorCursor != (Cursor) null)
              {
                placeEditorControl.Cursor = TableCellUI.oldEditorCursor;
                TableCellUI.oldEditorCursor = (Cursor) null;
              }
              placeEditorControl.Visible = true;
            }
          }
        }
      }
      if (this.Element.InPlaceEditorActive && ((IPageElementWithInterface) this.Element).InPlaceEditorControl is ImRtfEditor placeEditorControl1)
        placeEditorControl1.Capture = false;
      pageControl.SetTableRowsSelectingMode(false, (TableElement) null);
      pageControl.SetTableColumnsSelectingMode(false, (TableElement) null);
      pageControl.SetTableCellsSelectingMode(false, (TableElement) null);
    }
    if (this.DocumentControl == null || this.DocumentControl.DocumentManager == null)
      return;
    this.DocumentControl.DocumentManager.SetMessageText("");
  }

  internal override void OnKeyDown(KeyEventArgs e) => base.OnKeyDown(e);

  internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Left | Keys.Shift:
        if (this.Element is TextBoxElement element1 && element1.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element1.TextBox;
          if (textBox != null && !textBox.CursorInFirstPosition)
            return false;
        }
        if (Control.ModifierKeys != Keys.Shift)
          this.GotoPrevSingleCell();
        else
          this.SelectElements(this.GetCellsInRectangleFromSelectedTable(Point.Empty, new Keys?(keyData), false));
        return true;
      case Keys.Up:
      case Keys.Up | Keys.Shift:
        if (this.Element is TextBoxElement element2 && element2.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element2.TextBox;
          if (textBox != null && !textBox.CursorInFirstLine)
            return false;
        }
        if (Control.ModifierKeys != Keys.Shift)
          this.GotoUpSingleCell();
        else
          this.SelectElements(this.GetCellsInRectangleFromSelectedTable(Point.Empty, new Keys?(keyData), false));
        return true;
      case Keys.Right:
      case Keys.Right | Keys.Shift:
        if (this.Element is TextBoxElement element3 && element3.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element3.TextBox;
          if (textBox != null && !textBox.CursorInEndPosition)
            return false;
        }
        if (Control.ModifierKeys != Keys.Shift)
          this.GotoNextSingleCell();
        else
          this.SelectElements(this.GetCellsInRectangleFromSelectedTable(Point.Empty, new Keys?(keyData), false));
        return true;
      case Keys.Down:
      case Keys.Down | Keys.Shift:
        if (this.Element is TextBoxElement element4 && element4.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) element4.TextBox;
          if (textBox != null && !textBox.CursorInLastLine)
            return false;
        }
        if (Control.ModifierKeys != Keys.Shift)
          this.GotoDownSingleCell();
        else
          this.SelectElements(this.GetCellsInRectangleFromSelectedTable(Point.Empty, new Keys?(keyData), false));
        return true;
      case Keys.Delete:
        return false;
      default:
        return base.ProcessCmdKey(ref msg, keyData);
    }
  }
}
