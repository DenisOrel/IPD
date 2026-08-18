// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.RectanglePageElementUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Интерфейс пользователя прямоугольных элементов страницы</summary>
public class RectanglePageElementUI : PageElementUI
{
  private bool suspendDrawPreview;
  private int grabHandleCount = 9;
  private Rectangle clientBound;
  private Rectangle newBoundsFrame = Rectangle.Empty;
  private bool newBoundsFrameDrawed;
  private Cursor movingCursor = Cursors.SizeAll;
  private RectangleF elementBounds;
  private GrabHandlePoint? currentGrabPoint;

  /// <summary>Верхняя зона выбора включена</summary>
  protected virtual bool TopSelectionZoneEnabled
  {
    [DebuggerStepThrough] get => this.Element == null || !(this.Element is LabelElement);
  }

  /// <summary>Границы зоны для выбора элемента. Вверху над элементом.</summary>
  /// <returns></returns>
  protected Rectangle TopSelectionZone()
  {
    Rectangle bounds = this.Bounds;
    int left = bounds.Left;
    bounds = this.Bounds;
    int y = bounds.Top - this.GrabHandleSize.Height;
    bounds = this.Bounds;
    int width = bounds.Width;
    int height = this.GrabHandleSize.Height;
    return new Rectangle(left, y, width, height);
  }

  /// <summary>Количество областей захвата</summary>
  protected int GrabHandleCount
  {
    [DebuggerStepThrough] get => this.grabHandleCount;
  }

  /// <summary>Включена ли заданная область захвата</summary>
  /// <param name="ghp">Область захвата</param>
  /// <returns>Включена ли заданная область захвата</returns>
  protected virtual bool GetGrabHandleEnabled(GrabHandlePoint ghp)
  {
    return this.DocumentControl == null || this.DocumentControl.SelectedNodes.Count == 1 || this.IsActiveElement;
  }

  /// <summary>Получить курсор для заданной области захвата</summary>
  /// <param name="grabHandle">Область захвата</param>
  /// <returns>Курсор области захвата</returns>
  protected Cursor GetGrabHandleCursor(GrabHandlePoint grabHandle)
  {
    switch (grabHandle)
    {
      case GrabHandlePoint.LeftTop:
        return Cursors.SizeNWSE;
      case GrabHandlePoint.TopMiddle:
        return Cursors.SizeNS;
      case GrabHandlePoint.RightTop:
        return Cursors.SizeNESW;
      case GrabHandlePoint.RightMiddle:
        return Cursors.SizeWE;
      case GrabHandlePoint.BottomMiddle:
        return Cursors.SizeNS;
      case GrabHandlePoint.LeftBottom:
        return Cursors.SizeNESW;
      case GrabHandlePoint.LeftMiddle:
        return Cursors.SizeWE;
      case GrabHandlePoint.RightBottom:
        return Cursors.SizeNWSE;
      case GrabHandlePoint.Center:
        return Cursors.SizeAll;
      default:
        return Cursors.SizeAll;
    }
  }

  /// <summary>Получить положение заданной области захвата</summary>
  /// <param name="ghp">Область захвата</param>
  /// <returns>Положение заданной области захвата</returns>
  protected Point GetGrabHandleLocation(GrabHandlePoint ghp)
  {
    switch (ghp)
    {
      case GrabHandlePoint.LeftTop:
        return this.Bounds.Location;
      case GrabHandlePoint.TopMiddle:
        int left1 = this.Bounds.Left;
        Rectangle bounds1 = this.Bounds;
        int num1 = bounds1.Width / 2;
        int x1 = left1 + num1;
        bounds1 = this.Bounds;
        int top1 = bounds1.Top;
        return new Point(x1, top1);
      case GrabHandlePoint.RightTop:
        return new Point(this.Bounds.Right, this.Bounds.Top);
      case GrabHandlePoint.RightMiddle:
        int right = this.Bounds.Right;
        Rectangle bounds2 = this.Bounds;
        int top2 = bounds2.Top;
        bounds2 = this.Bounds;
        int num2 = bounds2.Height / 2;
        int y1 = top2 + num2;
        return new Point(right, y1);
      case GrabHandlePoint.BottomMiddle:
        int left2 = this.Bounds.Left;
        Rectangle bounds3 = this.Bounds;
        int num3 = bounds3.Width / 2;
        int x2 = left2 + num3;
        bounds3 = this.Bounds;
        int bottom = bounds3.Bottom;
        return new Point(x2, bottom);
      case GrabHandlePoint.LeftBottom:
        return new Point(this.Bounds.Left, this.Bounds.Bottom);
      case GrabHandlePoint.LeftMiddle:
        int left3 = this.Bounds.Left;
        Rectangle bounds4 = this.Bounds;
        int top3 = bounds4.Top;
        bounds4 = this.Bounds;
        int num4 = bounds4.Height / 2;
        int y2 = top3 + num4;
        return new Point(left3, y2);
      case GrabHandlePoint.RightBottom:
        return new Point(this.Bounds.Right, this.Bounds.Bottom);
      case GrabHandlePoint.Center:
        int x3 = this.Bounds.X;
        Rectangle bounds5 = this.Bounds;
        int num5 = bounds5.Width / 2;
        int x4 = x3 + num5;
        bounds5 = this.Bounds;
        int y3 = bounds5.Y;
        bounds5 = this.Bounds;
        int num6 = bounds5.Height / 2;
        int y4 = y3 + num6;
        return new Point(x4, y4);
      default:
        return Point.Empty;
    }
  }

  /// <summary>Ячейка находится в подтаблице с фиксированной структурой</summary>
  internal virtual bool IsCellInFixedStructureTable
  {
    get
    {
      return this.element is RectangleElement element && element.ParentCell != null && element.ParentCell.IsFixedStructureArea;
    }
  }

  /// <summary>Получить область захвата в заданной точке</summary>
  /// <param name="point">Точка</param>
  /// <param name="grabHandle">Возвращает область захвата</param>
  /// <param name="verticalFullSide">Левые и правые грани полностью под области захвата</param>
  /// <returns>true, если область захвата найдена</returns>
  protected virtual bool GetGrabHandleAtPoint(
    Point point,
    out GrabHandlePoint grabHandle,
    bool verticalFullSide)
  {
    grabHandle = !this.currentGrabPoint.HasValue ? GrabHandlePoint.Center : this.currentGrabPoint.Value;
    for (int grabHandle1 = 0; grabHandle1 < this.GrabHandleCount; ++grabHandle1)
    {
      if (this.CalcGrabHandleBounds((GrabHandlePoint) grabHandle1, verticalFullSide).Contains(point))
      {
        grabHandle = (GrabHandlePoint) grabHandle1;
        return true;
      }
    }
    return false;
  }

  /// <summary>Нарисовать области захвата</summary>
  /// <param name="g">Graphics</param>
  protected override void PaintGrabHandles(Graphics g)
  {
    if (!this.GrabHandlesActive)
      return;
    int num = 8;
    for (int index = 0; index < num; ++index)
    {
      bool grabHandleEnabled = this.GetGrabHandleEnabled((GrabHandlePoint) index);
      this.GetGrabHandleLocation((GrabHandlePoint) index);
      Rectangle rectangle = this.CalcGrabHandleBounds((GrabHandlePoint) index);
      this.PaintGrabHandle(g, rectangle.Location, grabHandleEnabled);
    }
  }

  /// <summary>Вычислить границы заданной области захвата</summary>
  /// <param name="grabHandle">Область захвата</param>
  /// <returns>Границы заданной области захвата</returns>
  protected Rectangle CalcGrabHandleBounds(GrabHandlePoint grabHandle)
  {
    return this.CalcGrabHandleBounds(grabHandle, false);
  }

  /// <summary>Вычислить границы заданной области захвата</summary>
  /// <param name="grabHandle">Область захвата</param>
  /// <param name="verticalFullSide">Левые и правые грани полностью под области захвата</param>
  /// <returns>Границы заданной области захвата</returns>
  protected virtual Rectangle CalcGrabHandleBounds(
    GrabHandlePoint grabHandle,
    bool verticalFullSide)
  {
    Point location = this.GetGrabHandleLocation(grabHandle);
    Size grabHandleSize = this.GrabHandleSize;
    switch (grabHandle)
    {
      case GrabHandlePoint.LeftTop:
        location = new Point(location.X - grabHandleSize.Width + 1, location.Y - grabHandleSize.Height + 1);
        break;
      case GrabHandlePoint.TopMiddle:
        location = new Point(location.X - grabHandleSize.Width / 2, location.Y - grabHandleSize.Height + 1);
        break;
      case GrabHandlePoint.RightTop:
        location = new Point(location.X, location.Y - grabHandleSize.Height + 1);
        break;
      case GrabHandlePoint.RightMiddle:
        if (verticalFullSide)
        {
          Rectangle rectangle;
          if (this.GrabHandlesActive)
          {
            Point grabHandleLocation1 = this.GetGrabHandleLocation(GrabHandlePoint.RightTop);
            Point grabHandleLocation2 = this.GetGrabHandleLocation(GrabHandlePoint.RightBottom);
            rectangle = new Rectangle(grabHandleLocation1.X, grabHandleLocation1.Y + grabHandleSize.Height, grabHandleSize.Width, grabHandleLocation2.Y - grabHandleLocation1.Y - grabHandleSize.Height);
          }
          else
            rectangle = new Rectangle(this.Bounds.Right - grabHandleSize.Width / 2, this.Bounds.Y, grabHandleSize.Width, this.Bounds.Height);
          return rectangle;
        }
        location = new Point(location.X, location.Y - grabHandleSize.Height / 2);
        break;
      case GrabHandlePoint.BottomMiddle:
        location = new Point(location.X - grabHandleSize.Width / 2, location.Y);
        break;
      case GrabHandlePoint.LeftBottom:
        location = new Point(location.X - grabHandleSize.Width + 1, location.Y);
        break;
      case GrabHandlePoint.LeftMiddle:
        if (verticalFullSide)
        {
          Rectangle rectangle;
          if (this.GrabHandlesActive)
          {
            Point grabHandleLocation3 = this.GetGrabHandleLocation(GrabHandlePoint.LeftTop);
            Point grabHandleLocation4 = this.GetGrabHandleLocation(GrabHandlePoint.LeftBottom);
            rectangle = new Rectangle(grabHandleLocation3.X, grabHandleLocation3.Y + grabHandleSize.Height, grabHandleSize.Width, grabHandleLocation4.Y - grabHandleLocation3.Y - grabHandleSize.Height);
          }
          else
            rectangle = new Rectangle(this.Bounds.X - grabHandleSize.Width / 2, this.Bounds.Y, grabHandleSize.Width, this.Bounds.Height);
          return rectangle;
        }
        location = new Point(location.X - grabHandleSize.Width + 1, location.Y - grabHandleSize.Height / 2);
        break;
      case GrabHandlePoint.RightBottom:
        location = new Point(location.X, location.Y);
        break;
      case GrabHandlePoint.Center:
        if (!this.TopSelectionZoneEnabled)
          return this.Bounds;
        Rectangle bounds = this.Bounds;
        int x = bounds.X;
        bounds = this.Bounds;
        int y = bounds.Y - this.GrabHandleSize.Height;
        bounds = this.Bounds;
        int width = bounds.Width;
        bounds = this.Bounds;
        int height = bounds.Height + this.GrabHandleSize.Height;
        return new Rectangle(x, y, width, height);
    }
    return new Rectangle(location, this.GrabHandleSize);
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
    PageElementUI elementUiAtPoint = base.GetPageElementUIAtPoint(point, ref layer, recursive, ignoreGrabHandle);
    int num = 0;
    if (this.DocumentControl != null && this.DocumentControl.NodeInSelection((DocumentTreeNode) this.element))
      num = 20;
    if (this.HasFixedStructureParent())
      num += 10;
    GrabHandlePoint grabHandle;
    if (!ignoreGrabHandle && layer < num + 1 && (this.GrabHandlesActive || this.IsCellInFixedStructureTable) && this.GetGrabHandleAtPoint(point, out grabHandle, this.IsCellInFixedStructureTable) && (this.GrabHandlesActive || grabHandle == GrabHandlePoint.LeftMiddle || grabHandle == GrabHandlePoint.RightMiddle))
    {
      if (grabHandle == GrabHandlePoint.Center)
      {
        if (layer < num)
        {
          layer = num;
          elementUiAtPoint = (PageElementUI) this;
        }
      }
      else
      {
        layer = num + 6;
        return (PageElementUI) this;
      }
    }
    if (layer >= num + 1 || !this.TopSelectionZoneEnabled || !this.TopSelectionZone().Contains(point))
      return elementUiAtPoint;
    layer = num + 1;
    return (PageElementUI) this;
  }

  /// <summary>Получить курсор для заданной точки</summary>
  /// <param name="point">Точка</param>
  /// <returns>Курсор</returns>
  public override Cursor GetCursor(Point point)
  {
    if (this.IsMoving)
    {
      GrabHandlePoint grabHandle;
      return Control.ModifierKeys == Keys.Control && (!this.GetGrabHandleAtPoint(this.startPoint, out grabHandle, this.IsCellInFixedStructureTable) ? 1 : (grabHandle == GrabHandlePoint.Center ? 1 : 0)) != 0 ? PageElementUI.CopyCursor : this.movingCursor;
    }
    if (Control.MouseButtons == MouseButtons.Left)
      point = this.startPoint;
    DocumentControl documentControl = this.DocumentControl;
    bool flag = false;
    if (documentControl != null)
      flag = documentControl.SelectedNodes != null && documentControl.SelectedNodes.Count > 1;
    if (flag)
      return this.GeometryChangingBlocked ? Cursors.Default : Cursors.SizeAll;
    Cursor cursor = (Cursor) null;
    if (this.GrabHandlesActive || this.IsCellInFixedStructureTable)
    {
      GrabHandlePoint grabHandle = GrabHandlePoint.RightBottom;
      if (this.GetGrabHandleAtPoint(point, out grabHandle, this.IsCellInFixedStructureTable) && this.GetGrabHandleEnabled(grabHandle))
      {
        cursor = this.GetGrabHandleCursor(grabHandle);
        if (grabHandle != GrabHandlePoint.Center)
          return cursor;
      }
    }
    if (!this.GeometryChangingBlocked && this.TopSelectionZoneEnabled && this.TopSelectionZone().Contains(point))
      return Cursors.SizeAll;
    if (this.Element != null && this.Element.CanActivateInPlaceEditor)
    {
      if (PageElementUI.PixelRectangle(this.clientBound).Contains(point))
        return Cursors.IBeam;
    }
    else if (cursor != (Cursor) null)
      return cursor;
    return base.GetCursor(point);
  }

  /// <summary>Обявить недействительной всю область интерфейса пользователя</summary>
  public override void InvalidateUI()
  {
    Point location = this.CalcGrabHandleBounds(GrabHandlePoint.LeftTop).Location;
    Rectangle rectangle = this.CalcGrabHandleBounds(GrabHandlePoint.RightBottom);
    Point point = new Point(rectangle.Right, rectangle.Bottom);
    this.InvalidateUI(Rectangle.FromLTRB(location.X, location.Y, point.X + 1, point.Y + 1));
  }

  /// <summary>Перекрытый метод отрисовки OnPaint</summary>
  public override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    DocumentControl documentControl = this.DocumentControl;
    if ((this.Element == null || this.IsVisibleElementAndParents) && this.IsActiveElement && (documentControl == null || documentControl.ActivePage != null && this.PageControl == documentControl.PageControl))
      this.DrawFocusedRectangle(e.Graphics);
    if (!this.newBoundsFrameDrawed || this.PageControl == null || this.PageControl.suspendDrawMovingPreview || !this.NewBounds.IntersectsWith(e.ClipRectangle))
      return;
    this.DrawNewGeometryPreview(e.Graphics);
  }

  /// <summary>Стереть предпросмотр новых границ</summary>
  public override void EraseNewGeometryPreview(bool update)
  {
    if (!this.newBoundsFrameDrawed)
      return;
    this.newBoundsFrameDrawed = false;
    if (this.PageControl == null)
      return;
    GrabHandlePoint grabHandle;
    if ((!this.GetGrabHandleAtPoint(this.startPoint, out grabHandle, this.IsCellInFixedStructureTable) ? 1 : (grabHandle == GrabHandlePoint.Center ? 1 : 0)) == 0)
      this.PageControl.NeedDrawPopupBar = true;
    this.PageControl.Invalidate(PageElementUI.PixelRectangle(this.newBoundsFrame), true);
    this.PageControl.Invalidate(this.PageControl.RegionForInvalidate);
    if (!update)
      return;
    this.PageControl.Update();
  }

  /// <summary>Нарисовать предпросмотр новых границ</summary>
  /// <param name="g">Graphics</param>
  public override void DrawNewGeometryPreview(Graphics g)
  {
    try
    {
      PageControl pageControl = this.PageControl;
      if (pageControl == null || this.suspendDrawPreview)
        return;
      this.suspendDrawPreview = true;
      try
      {
        bool flag = g == null;
        if (g == null)
        {
          if (this.newBoundsFrameDrawed)
            this.EraseNewGeometryPreview(true);
          g = this.PageControl.CreateGraphics();
        }
        try
        {
          this.newBoundsFrame = this.NewBounds;
          GrabHandlePoint grabHandle;
          if ((!this.GetGrabHandleAtPoint(this.startPoint, out grabHandle, this.IsCellInFixedStructureTable) ? 1 : (grabHandle == GrabHandlePoint.Center ? 1 : 0)) == 0 && ImDocumentEditorConfig.Instance.ShowPopupBarOnResize && this.currentGrabPoint.HasValue)
          {
            GrabHandlePoint? currentGrabPoint1 = this.currentGrabPoint;
            GrabHandlePoint grabHandlePoint1 = GrabHandlePoint.BottomMiddle;
            if (!(currentGrabPoint1.GetValueOrDefault() == grabHandlePoint1 & currentGrabPoint1.HasValue))
            {
              GrabHandlePoint? currentGrabPoint2 = this.currentGrabPoint;
              GrabHandlePoint grabHandlePoint2 = GrabHandlePoint.LeftMiddle;
              if (!(currentGrabPoint2.GetValueOrDefault() == grabHandlePoint2 & currentGrabPoint2.HasValue))
              {
                GrabHandlePoint? currentGrabPoint3 = this.currentGrabPoint;
                GrabHandlePoint grabHandlePoint3 = GrabHandlePoint.RightMiddle;
                if (!(currentGrabPoint3.GetValueOrDefault() == grabHandlePoint3 & currentGrabPoint3.HasValue))
                {
                  GrabHandlePoint? currentGrabPoint4 = this.currentGrabPoint;
                  GrabHandlePoint grabHandlePoint4 = GrabHandlePoint.TopMiddle;
                  if (!(currentGrabPoint4.GetValueOrDefault() == grabHandlePoint4 & currentGrabPoint4.HasValue))
                    goto label_26;
                }
              }
            }
            float? offsetFromLeft = new float?();
            float? offsetFromRight = new float?();
            float? leftCellSize = new float?();
            PageElementNode element = this.Element;
            Matrix userCoorMatrix = this.Page.PageUI.GetUserCoorMatrix();
            Point mousePosition = this.mousePosition;
            GrabHandlePoint? currentGrabPoint5 = this.currentGrabPoint;
            GrabHandlePoint grabHandlePoint5 = GrabHandlePoint.LeftMiddle;
            if (currentGrabPoint5.GetValueOrDefault() == grabHandlePoint5 & currentGrabPoint5.HasValue)
            {
              mousePosition.X = this.newBoundsFrame.Left;
              pageControl.IsPopupBarHorizontal = true;
              offsetFromLeft = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Left, userCoorMatrix));
              leftCellSize = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Width, userCoorMatrix));
              if (this.Element.Page != null)
                offsetFromRight = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.Element.Page.Size.Width - this.elementBounds.Left, userCoorMatrix));
            }
            GrabHandlePoint? currentGrabPoint6 = this.currentGrabPoint;
            GrabHandlePoint grabHandlePoint6 = GrabHandlePoint.RightMiddle;
            if (currentGrabPoint6.GetValueOrDefault() == grabHandlePoint6 & currentGrabPoint6.HasValue)
            {
              mousePosition.X = this.newBoundsFrame.Right;
              pageControl.IsPopupBarHorizontal = true;
              offsetFromLeft = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Right, userCoorMatrix));
              leftCellSize = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Width, userCoorMatrix));
              if (this.Element.Page != null)
                offsetFromRight = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.Element.Page.Size.Width - this.elementBounds.Right, userCoorMatrix));
            }
            GrabHandlePoint? currentGrabPoint7 = this.currentGrabPoint;
            GrabHandlePoint grabHandlePoint7 = GrabHandlePoint.TopMiddle;
            if (currentGrabPoint7.GetValueOrDefault() == grabHandlePoint7 & currentGrabPoint7.HasValue)
            {
              mousePosition.Y = this.newBoundsFrame.Top;
              pageControl.IsPopupBarHorizontal = false;
              offsetFromLeft = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Top, userCoorMatrix));
              leftCellSize = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Height, userCoorMatrix));
              if (this.Element.Page != null)
                offsetFromRight = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.Element.Page.Size.Height - this.elementBounds.Top, userCoorMatrix));
            }
            GrabHandlePoint? currentGrabPoint8 = this.currentGrabPoint;
            GrabHandlePoint grabHandlePoint8 = GrabHandlePoint.BottomMiddle;
            if (currentGrabPoint8.GetValueOrDefault() == grabHandlePoint8 & currentGrabPoint8.HasValue)
            {
              mousePosition.Y = this.newBoundsFrame.Bottom;
              pageControl.IsPopupBarHorizontal = false;
              offsetFromLeft = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Bottom, userCoorMatrix));
              leftCellSize = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.elementBounds.Height, userCoorMatrix));
              if (this.Element.Page != null)
                offsetFromRight = new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.Element.Page.Size.Height - this.elementBounds.Bottom, userCoorMatrix));
            }
            this.PageControl.DrawLine = false;
            this.PageControl.PopupBarPosition = mousePosition;
            this.PageControl.SetBarValues(offsetFromLeft, offsetFromRight, leftCellSize, new float?());
            this.PageControl.PreparePopupBar();
            this.PageControl.Invalidate(this.PageControl.RegionForInvalidate);
            this.PageControl.Update();
          }
label_26:
          if (!this.IsFirstStep)
            RubberBand.DrawXorRectangle(g, this.newBoundsFrame, Color.White);
          this.IsFirstStep = false;
          this.newBoundsFrameDrawed = true;
        }
        finally
        {
          if (flag && g != null)
            g.Dispose();
        }
      }
      finally
      {
        this.suspendDrawPreview = false;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private RectangleF CalcNewElementBounds(
    GrabHandlePoint grabHandle,
    Point startPoint,
    Point delta)
  {
    if (!(this.element is RectangleElement element))
      return RectangleF.Empty;
    RectangleF bounds1 = element.Bounds;
    if (this.PageControl == null)
      return bounds1;
    RectangleF rect = bounds1;
    PointF world = this.PixelToWorld(new Point(startPoint.X + delta.X, startPoint.Y + delta.Y), grabHandle != GrabHandlePoint.Center, (VisualNode) element);
    switch (grabHandle)
    {
      case GrabHandlePoint.LeftTop:
        PointF pointF1 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(pointF1.X, pointF1.Y, bounds1.Right - pointF1.X, bounds1.Bottom - pointF1.Y);
        break;
      case GrabHandlePoint.TopMiddle:
        PointF pointF2 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(bounds1.X, pointF2.Y, bounds1.Width, bounds1.Bottom - pointF2.Y);
        break;
      case GrabHandlePoint.RightTop:
        PointF pointF3 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(bounds1.X, pointF3.Y, pointF3.X - bounds1.X, bounds1.Bottom - pointF3.Y);
        break;
      case GrabHandlePoint.RightMiddle:
        PointF pointF4 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(bounds1.X, bounds1.Y, pointF4.X - bounds1.X, bounds1.Height);
        break;
      case GrabHandlePoint.BottomMiddle:
        PointF pointF5 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(bounds1.X, bounds1.Y, bounds1.Width, pointF5.Y - bounds1.Y);
        break;
      case GrabHandlePoint.LeftBottom:
        PointF pointF6 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(pointF6.X, bounds1.Y, bounds1.Right - pointF6.X, pointF6.Y - bounds1.Y);
        break;
      case GrabHandlePoint.LeftMiddle:
        PointF pointF7 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(pointF7.X, bounds1.Y, bounds1.Right - pointF7.X, bounds1.Height);
        break;
      case GrabHandlePoint.RightBottom:
        PointF pointF8 = this.Page.PageUI.SnapPoint(world, (VisualNode) null);
        rect = new RectangleF(bounds1.X, bounds1.Y, pointF8.X - bounds1.X, pointF8.Y - bounds1.Y);
        break;
      case GrabHandlePoint.Center:
        Rectangle bounds2 = this.Bounds;
        ref Rectangle local = ref bounds2;
        Rectangle bounds3 = this.Bounds;
        int x = bounds3.X + delta.X;
        bounds3 = this.Bounds;
        int y = bounds3.Y + delta.Y;
        Point point = new Point(x, y);
        local.Location = point;
        rect.Location = this.Page.PageUI.ConvertPixelToWorld(bounds2.Location);
        if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        {
          if ((double) Math.Abs(rect.X - bounds1.X) < (double) Math.Abs(rect.Y - bounds1.Y))
            rect.X = bounds1.X;
          else
            rect.Y = bounds1.Y;
        }
        rect = this.Page.PageUI.SnapRectangle(rect, (VisualNode) null);
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
    if (this.GeometryChangingBlocked || this.PageControl == null || this.Page?.PageUI == null)
      return;
    GrabHandlePoint grabHandle;
    bool flag = !this.GetGrabHandleAtPoint(startPoint, out grabHandle, this.IsCellInFixedStructureTable) || grabHandle == GrabHandlePoint.Center;
    if (this.PageControl != null && this.PageControl.IsPasting)
    {
      flag = true;
      grabHandle = GrabHandlePoint.Center;
    }
    this.currentGrabPoint = new GrabHandlePoint?(grabHandle);
    RectangleF rectangleF = this.CalcNewElementBounds(grabHandle, startPoint, delta);
    this.elementBounds = rectangleF;
    Rectangle pixel = this.Page.PageUI.ConvertWorldToPixel(rectangleF);
    if (!flag)
      this.Page = (Page) null;
    this.movingCursor = flag ? Cursors.SizeAll : this.GetGrabHandleCursor(grabHandle);
    DocumentControl documentControl = this.DocumentControl;
    if (this.NewBounds != pixel || ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
    {
      this.NewBounds = pixel;
      this.DrawNewGeometryPreview((Graphics) null);
      documentControl?.OnSelectedElementBoundsChanging(new BoundsChangingEventArgs((DocumentTreeNode) this.element, rectangleF));
    }
    base.ChangingPoint(startPoint, delta);
    if (documentControl?.DocumentManager == null)
      return;
    PointF user1 = this.Page.PageUI.ConvertInternalToUser(rectangleF.Location);
    PointF user2 = this.Page.PageUI.ConvertInternalToUser(new PointF(rectangleF.Right, rectangleF.Bottom));
    SizeF user3 = this.Page.PageUI.ConvertInternalToUser(rectangleF.Size);
    string text = string.Format(LocalizationHolder.rm.GetString("Document.Model_96"), (object) user1.X, (object) user3.Width, (object) user2.X, (object) user2.Y, (object) user3.Height, (object) user1.Y);
    documentControl.DocumentManager.SetMessageText(text);
  }

  /// <summary>Начать процесс перемещения элемента страницы</summary>
  protected override void BeginMoving(MouseEventArgs mouseArgs, Keys modifierKeys)
  {
    this.DocumentControl?.DeactivateInPlaceEditor();
    this.NewBounds = this.Bounds;
    GrabHandlePoint grabHandle;
    this.GetGrabHandleAtPoint(this.startPoint, out grabHandle, this.IsCellInFixedStructureTable);
    this.currentGrabPoint = new GrabHandlePoint?(grabHandle);
    base.BeginMoving(mouseArgs, modifierKeys);
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
    if (this.newBoundsFrameDrawed & erasePreview)
      this.EraseNewGeometryPreview(true);
    PageElementNode element = this.Element;
    DocumentControl documentControl = this.DocumentControl;
    this.currentGrabPoint = new GrabHandlePoint?();
    documentControl?.ActivateInPlaceEditor();
  }

  /// <summary>Завершить процесс перемещения элемента страницы</summary>
  protected override void EndMoving(
    MouseEventArgs mouseArgs,
    Keys modifierKeys,
    Point startPoint,
    Point delta)
  {
    this.Element.OwnerDocument?.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_588"));
    try
    {
      if (this.PageControl != null && ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
      {
        this.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
        this.PageControl.Invalidate(this.PageControl.RegionForInvalidate);
      }
      if (this.newBoundsFrameDrawed)
        this.EraseNewGeometryPreview(true);
      PageControl pageControl = this.PageControl;
      RectangleElement child = this.Element as RectangleElement;
      if (pageControl != null && child != null && this.Page?.PageUI != null)
      {
        GrabHandlePoint grabHandle;
        bool flag = !this.GetGrabHandleAtPoint(startPoint, out grabHandle, this.IsCellInFixedStructureTable) || grabHandle == GrabHandlePoint.Center;
        if (this.PageControl != null && this.PageControl.IsPasting)
        {
          flag = true;
          grabHandle = GrabHandlePoint.Center;
        }
        Page page = this.PageControl?.GetPageAtPoint(mouseArgs.Location) ?? this.Page;
        if (page != null && this.Page != null && page != this.Page && this.Element != null && (!flag || Control.ModifierKeys != Keys.Control))
        {
          PageElementNode element = this.Element;
          element.SuspendRefreshUI();
          this.Page.RemoveChildNode((DocumentTreeNode) this.Element, false, false);
          page.AddChildNode((DocumentTreeNode) element, false, false);
          this.Element = element;
          DocumentControl.SetShowSelected((DocumentTreeNode) this.Element, true, false);
          element.ResumeRefreshUI(false);
        }
        RectangleF bounds = child.Bounds;
        this.Page = page;
        RectangleF newBounds = this.CalcNewElementBounds(grabHandle, startPoint, delta);
        this.Page = (Page) null;
        if (bounds != newBounds)
        {
          TableData tableData = child as TableData;
          if (Control.ModifierKeys == Keys.Control & flag)
          {
            child = (RectangleElement) child.Clone(true, true);
            tableData = child as TableData;
          }
          if (tableData != null)
          {
            if (newBounds.Location != tableData.Location)
            {
              TableData parentCell1 = tableData.ParentCell;
              if (parentCell1 != null && parentCell1.IsFixedStructureArea)
              {
                TableData parentCell2 = parentCell1.ParentCell;
                RectangleF rectangleF = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
                tableData.AssignProperBounds(new RectangleF(newBounds.X - rectangleF.X, newBounds.Y - rectangleF.Y, newBounds.Width, newBounds.Height), true, false, false);
                tableData.RecalcRelativeSize();
              }
              tableData.RecalcCellLocations(newBounds.Location, 0, tableData.Nodes.Count, false, false, false);
            }
            if (tableData.IsTopLevelTable && (double) tableData.MaxHeight != 0.0)
              tableData.AssignMaxHeight(newBounds.Height, false, false, true);
            tableData.SetCellSizes(newBounds, false, true, true, true, true);
          }
          else
          {
            if ((double) bounds.Width != (double) newBounds.Width)
              child.AssignMinWidth(newBounds.Width, false, false, true);
            if ((double) bounds.Height != (double) newBounds.Height)
              child.AssignMinHeight(newBounds.Height, false, false, true);
            child.SetCellSizes(newBounds, false, true, false, true);
            TableData parentCell3 = child.ParentCell;
            if (parentCell3 != null && parentCell3.IsFixedStructureArea)
            {
              TableData parentCell4 = parentCell3.ParentCell;
              RectangleF rectangleF = parentCell4 == null || !parentCell4.IsFixedStructureArea ? parentCell3.properBounds : parentCell3.bounds;
              child.AssignProperBounds(new RectangleF(newBounds.X - rectangleF.X, newBounds.Y - rectangleF.Y, newBounds.Width, newBounds.Height), true, false, false);
              child.RecalcRelativeSize();
            }
          }
          if (Control.ModifierKeys == Keys.Control & flag && page != null)
            page.AddChildNode((DocumentTreeNode) child, false, false);
          this.PageControl.Document.UpdateLayout(true);
          this.PageControl.Document.SuspendRefreshUI();
          try
          {
            if (this.DocumentControl != null)
            {
              List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
              this.DocumentControl.SetSelection(new List<DocumentTreeNode>(), false, false);
              this.DocumentControl.SetSelection(selectedNodes, false, false);
            }
          }
          finally
          {
            this.PageControl.Document.ResumeRefreshUI(true);
          }
        }
      }
      this.currentGrabPoint = new GrabHandlePoint?();
      base.EndMoving(mouseArgs, modifierKeys, startPoint, delta);
      PageElementNode element1 = this.Element;
      DocumentControl documentControl = this.DocumentControl;
      if (element1 != null && !this.IsCellInFixedStructureTable)
      {
        this.FocusUI();
        documentControl?.SetActiveElement((DocumentTreeNode) element1, element1.InPlaceEditorActive, Point.Empty);
      }
      documentControl?.DocumentManager?.UpdateSelectedElementInfo();
      if (documentControl != null)
      {
        documentControl.SetRulerBorders();
        documentControl.HorzRuler.UpdateIdents();
        documentControl.HorzRuler.Refresh();
        documentControl.VertRuler.Refresh();
      }
      documentControl?.ActivateInPlaceEditor();
    }
    finally
    {
      this.Element.OwnerDocument?.UndoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Обновить геометрию</summary>
  public override void UpdateGeometry()
  {
    if ((this.Element is RectangleElement element ? element.Page : (PageData) null) != null)
    {
      Rectangle bounds = this.Bounds;
      Rectangle pixel = element.Page.ConvertWorldToPixel(element.Bounds);
      Rectangle rectangle1 = pixel;
      if (bounds != rectangle1)
      {
        Point location = this.CalcGrabHandleBounds(GrabHandlePoint.LeftTop).Location;
        Rectangle rectangle2 = this.CalcGrabHandleBounds(GrabHandlePoint.RightBottom);
        Point point = new Point(rectangle2.Right, rectangle2.Bottom);
        this.PageControl?.AddToInvalidateRegion(Rectangle.FromLTRB(location.X, location.Y, point.X + 1, point.Y + 1));
      }
      this.Bounds = pixel;
      this.NewBounds = this.Bounds;
      this.clientBound = element.Page.ConvertWorldToPixel(element.ClientBounds);
    }
    base.UpdateGeometry();
  }

  /// <summary>Вычислить новые границы элемента</summary>
  /// <returns>Новые границы элемента</returns>
  protected virtual RectangleF CalcNewElementBounds()
  {
    RectangleF empty1 = RectangleF.Empty;
    if (this.Element == null)
      return empty1;
    if (this.PageControl == null && this.element is RectangleElement element)
      return element.Bounds;
    Rectangle newBounds = this.NewBounds;
    if (newBounds.Width < 0)
      newBounds.Width = 0;
    if (newBounds.Height < 0)
      newBounds.Height = 0;
    RectangleF world = this.Page.PageUI.ConvertPixelToWorld(newBounds);
    RectangleF empty2 = RectangleF.Empty;
    RectangleF bounds = ((RectangleElement) this.Element).Bounds;
    Rectangle pixel = this.Page.PageUI.ConvertWorldToPixel(bounds);
    return this.TrimChanges(bounds, world, pixel, newBounds);
  }

  /// <summary>Обновить геометрию элемента страницы</summary>
  public override void UpdateElementGeometry()
  {
    if ((this.Element is RectangleElement element ? element.Page : (PageData) null) == null)
      return;
    RectangleF bounds = element.Bounds;
    RectangleF rectangleF = this.CalcNewElementBounds();
    if ((double) bounds.Width != (double) rectangleF.Width)
      element.WidthOverrided = true;
    if ((double) bounds.Height != (double) rectangleF.Height)
      element.HeightOverrided = true;
    element.AssignBounds(rectangleF, true, true, true);
  }

  /// <summary>Границы клиентской области</summary>
  public Rectangle ClientBound
  {
    [DebuggerStepThrough] get => this.clientBound;
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  internal override void OnMouseDown(MouseEventArgs e)
  {
    Point point = new Point(e.X, e.Y);
    if (e.Button == MouseButtons.Left)
      this.leftMouseDownPos = point;
    if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.PageControl != null && !this.PageControl.IsPasting)
    {
      bool flag1 = false;
      GrabHandlePoint grabHandle = GrabHandlePoint.Center;
      bool fixedStructureTable = this.IsCellInFixedStructureTable;
      bool flag2 = false;
      if (fixedStructureTable)
        flag2 = this.GetGrabHandleAtPoint(point, out grabHandle, fixedStructureTable);
      if (!fixedStructureTable || !flag2 || grabHandle != GrabHandlePoint.LeftMiddle && grabHandle != GrabHandlePoint.RightMiddle)
      {
        bool flag3 = !this.Element.InPlaceEditorActive;
        bool inPlaceEditEnabled = Control.ModifierKeys == Keys.None && this.element.CanActivateInPlaceEditor;
        this.SelectElement(Control.ModifierKeys, inPlaceEditEnabled, e.Location, false, false);
        if (!this.Element.InPlaceEditorActive & inPlaceEditEnabled && this.element is IPageElementWithInterface element)
          element.ActivateInPlaceEditor(element.PageUI, e);
        flag1 = flag3 && this.Element.InPlaceEditorActive;
      }
      if (flag1 && ((IPageElementWithInterface) this.Element).InPlaceEditorControl is ImRtfEditor placeEditorControl)
      {
        Point screen = this.PageControl.PointToScreen(point);
        Point client = placeEditorControl.PointToClient(screen);
        MouseEventArgs ev = new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
        placeEditorControl.Cursor = Cursors.IBeam;
        placeEditorControl.FireMouseDown(ev);
        placeEditorControl.Capture = true;
      }
    }
    base.OnMouseDown(e);
  }

  /// <summary>Перемещается ли весь элемент</summary>
  protected override bool MoveAll
  {
    get
    {
      GrabHandlePoint grabHandle;
      return !this.GetGrabHandleAtPoint(this.startPoint, out grabHandle, this.IsCellInFixedStructureTable) || grabHandle == GrabHandlePoint.Center;
    }
  }

  internal override void OnMouseUp(MouseEventArgs e)
  {
    Point point = new Point(e.X, e.Y);
    if (e.Button == MouseButtons.Left)
    {
      if (this.IsMoving)
      {
        Point delta = new Point(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
        if (!delta.IsEmpty)
          this.EndMoving(e, Control.ModifierKeys, this.startPoint, delta);
      }
      else if (this.Element.InPlaceEditorActive && ((IPageElementWithInterface) this.Element).InPlaceEditorControl is ImRtfEditor placeEditorControl)
        placeEditorControl.Capture = false;
    }
    this.DocumentControl?.DocumentManager?.SetMessageText("");
  }

  /// <summary>Можно ли начать перемещение элемента</summary>
  /// <param name="point">Начальная точка</param>
  /// <returns>Можно ли начать перемещение элемента</returns>
  protected override bool CanBeginMoving(Point point)
  {
    bool flag = base.CanBeginMoving(point);
    if (flag && !this.GetGrabHandleAtPoint(point, out GrabHandlePoint _, this.IsCellInFixedStructureTable))
    {
      Rectangle rectangle;
      int num;
      if (!PageElementUI.PixelRectangle(this.Bounds).Contains(point))
      {
        if (this.TopSelectionZoneEnabled)
        {
          rectangle = this.TopSelectionZone();
          num = !rectangle.Contains(point) ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      if (num != 0)
        flag = false;
      if (num == 0 && this.Element != null && this.Element.CanActivateInPlaceEditor)
      {
        rectangle = PageElementUI.PixelRectangle(this.clientBound);
        if (rectangle.Contains(point))
          flag = false;
      }
    }
    return flag;
  }
}
