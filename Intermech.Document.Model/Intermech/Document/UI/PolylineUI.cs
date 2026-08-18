// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PolylineUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Интерфейс пользователя для полилинии</summary>
public class PolylineUI : PageElementUI
{
  private Rectangle newBoundsFrame = Rectangle.Empty;
  /// <summary>Контур пути полилинии</summary>
  protected GraphicsPath contourPath;
  /// <summary>Полилиния</summary>
  private float displayLineWidth;
  private GraphicsPath displayPath;
  /// <summary>Новый контур пути с экранными координатами</summary>
  public GraphicsPath NewDisplayPath;
  private GraphicsPath newPathPreview;

  /// <summary>Отображаемая полилиния</summary>
  public Polyline Polyline
  {
    [DebuggerStepThrough] get => (Polyline) this.Element;
    set => this.Element = (PageElementNode) value;
  }

  /// <summary>Область захвата включена</summary>
  /// <param name="pointIndex">Индекс точки</param>
  /// <returns>Включена ли область захвата</returns>
  protected virtual bool GrabHandleEnabled(int pointIndex)
  {
    return (this.DocumentControl == null || this.DocumentControl.SelectedNodes.Count == 1 || this.IsActiveElement) && this.element != null && this.IsVisibleElementAndParents;
  }

  /// <summary>Нарисовать области захвата</summary>
  /// <param name="g">Graphics</param>
  protected override void PaintGrabHandles(Graphics g)
  {
    bool flag = false;
    List<DocumentTreeNode> selectedNodes = this.DocumentControl?.SelectedNodes;
    if (this.DocumentControl != null && this.Element != null && selectedNodes != null && selectedNodes.Contains((DocumentTreeNode) this.Element) && selectedNodes.Count > 1)
      flag = true;
    if (!this.GrabHandlesActive && !flag || this.displayPath == null)
      return;
    PointF[] pathPoints = this.displayPath.PathPoints;
    for (int pointIndex = 0; pointIndex < pathPoints.Length; ++pointIndex)
    {
      if (flag)
      {
        Rectangle rect = new Rectangle(this.CalcGrabHandleBounds(Point.Round(pathPoints[pointIndex])).Location, this.GrabHandleSize);
        using (SolidBrush solidBrush = new SolidBrush(Color.Black))
          g.FillRectangle((Brush) solidBrush, rect);
      }
      else
      {
        Rectangle rectangle = this.CalcGrabHandleBounds(Point.Round(pathPoints[pointIndex]));
        bool enabled = this.GrabHandleEnabled(pointIndex);
        this.PaintGrabHandle(g, rectangle.Location, enabled);
      }
    }
  }

  /// <summary>Получить индекс области захвата для заданной точки</summary>
  /// <param name="point">Точка</param>
  /// <returns>Индекс области захвата</returns>
  protected virtual int GetGrabHandleAtPoint(Point point)
  {
    int grabHandleAtPoint = -1;
    if (this.DisplayPath == null)
      return grabHandleAtPoint;
    PointF[] pathPoints = this.DisplayPath.PathPoints;
    for (int index = 0; index < pathPoints.Length; ++index)
    {
      if (this.CalcGrabHandleBounds(Point.Round(pathPoints[index])).Contains(point))
      {
        grabHandleAtPoint = index;
        break;
      }
    }
    return grabHandleAtPoint;
  }

  /// <summary>Вычислить границы области захвата</summary>
  /// <param name="point">Точка</param>
  /// <returns>Границы</returns>
  public virtual Rectangle CalcGrabHandleBounds(Point point)
  {
    return new Rectangle(new Point(point.X - this.grabHandleSize.Width / 2, point.Y - this.grabHandleSize.Height / 2), this.GrabHandleSize);
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
    if (layer < 1 && this.GrabHandlesActive && this.GetGrabHandleAtPoint(point) != -1)
    {
      layer = 1;
      return (PageElementUI) this;
    }
    if (layer >= 0 || !PageElementUI.PixelRectangle(this.Bounds).Contains(point) || this.TransparentForMouse || this.contourPath == null || !this.contourPath.IsVisible(point))
      return (PageElementUI) null;
    layer = 0;
    return (PageElementUI) this;
  }

  /// <summary>Перекрытый метод отрисовки OnPaint</summary>
  public override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (this.newPathPreview == null)
      return;
    Rectangle rectangle = Rectangle.Round(this.newPathPreview.GetBounds(new Matrix(), new Pen(Color.Black, PageElementNode.DefaultLineWidth)
    {
      DashStyle = DashStyle.Dash
    }));
    rectangle.Size = new Size(rectangle.Width + 1, rectangle.Height + 1);
    if (!rectangle.IntersectsWith(e.ClipRectangle))
      return;
    this.DrawNewGeometryPreview(e.Graphics);
  }

  /// <summary>Получить курсор для заданной точки</summary>
  /// <param name="point">Точка</param>
  /// <returns>Курсор</returns>
  public override Cursor GetCursor(Point point)
  {
    if (this.GrabHandlesActive)
    {
      int grabHandleAtPoint = this.GetGrabHandleAtPoint(point);
      if (grabHandleAtPoint > -1)
        return this.GrabHandleEnabled(grabHandleAtPoint) ? Cursors.SizeAll : Cursors.Default;
    }
    return this.IsMoving && Control.ModifierKeys == Keys.Control && this.GetGrabHandleAtPoint(point) == -1 ? PageElementUI.CopyCursor : base.GetCursor(point);
  }

  /// <summary>Обявить недействительной всю область интерфейса пользователя</summary>
  public override void InvalidateUI()
  {
    if (this.displayPath == null)
      return;
    Rectangle clipRec = Rectangle.Round(this.displayPath.GetBounds(new Matrix(), new Pen(Color.Black, 0.0f)));
    ref Rectangle local = ref clipRec;
    int x1 = clipRec.X;
    Size grabHandleSize = this.GrabHandleSize;
    int width = grabHandleSize.Width;
    int x2 = x1 - width;
    int y1 = clipRec.Y;
    grabHandleSize = this.GrabHandleSize;
    int height = grabHandleSize.Height;
    int y2 = y1 - height;
    Point point = new Point(x2, y2);
    local.Location = point;
    clipRec.Size = new Size(clipRec.Width + 2 * this.GrabHandleSize.Width + 1, clipRec.Height + 2 * this.GrabHandleSize.Height + 1);
    this.InvalidateUI(clipRec);
  }

  /// <summary>Обработчик события изменения положения точки</summary>
  /// <param name="startPoint">Точка с которой началось движение</param>
  /// <param name="delta">Смещение от этой точки</param>
  public override void ChangingPoint(Point startPoint, Point delta)
  {
    if (this.GeometryChangingBlocked || this.Polyline == null || this.Polyline.Page == null || this.displayPath == null || !(this.Element is Polyline element))
      return;
    int grabHandleAtPoint = this.GetGrabHandleAtPoint(startPoint);
    PointF[] pathPoints1 = this.displayPath.PathPoints;
    PointF[] pathPoints2 = element.PathPoints;
    byte[] pathTypes = this.displayPath.PathTypes;
    if (this.PageControl == null)
      return;
    string text = "";
    if (grabHandleAtPoint == -1 || this.Page != null && this.Page.PageUI != null && this.PageControl != null && this.PageControl.IsPasting)
    {
      using (Matrix matrix = new Matrix())
      {
        RectangleF bounds1 = element.Path.GetBounds();
        RectangleF bounds2 = this.displayPath.GetBounds();
        PointF location1 = bounds1.Location;
        Point point1 = new Point((int) bounds2.X, (int) bounds2.Y);
        Point point2 = new Point(point1.X + delta.X, point1.Y + delta.Y);
        if (this.Page?.PageUI != null)
        {
          PointF world = this.Page.PageUI.ConvertPixelToWorld(point2);
          PointF location2 = this.Page.PageUI.SnapRectangle(new RectangleF((Control.ModifierKeys & Keys.Shift) != Keys.None ? this.Page.PageUI.SnapPointOrtho(world, location1, (VisualNode) element) : this.Page.PageUI.SnapPoint(world, (VisualNode) element), bounds1.Size), (VisualNode) null).Location;
          point2 = this.Page.PageUI.ConvertWorldToPixel(location2);
          point1 = Point.Round(this.displayPath.GetBounds().Location);
          matrix.Translate((float) (point2.X - point1.X), (float) (point2.Y - point1.Y));
          matrix.TransformPoints(pathPoints1);
          bounds1.Location = location2;
          PointF user1 = this.Page.PageUI.ConvertInternalToUser(location2);
          PointF user2 = this.Page.PageUI.ConvertInternalToUser(new PointF(bounds1.Right, bounds1.Bottom));
          SizeF user3 = this.Page.PageUI.ConvertInternalToUser(bounds1.Size);
          text = string.Format(LocalizationHolder.rm.GetString("Document.Model_82"), (object) user1.X, (object) user3.Width, (object) user2.X, (object) user2.Y, (object) user3.Height, (object) user1.Y);
        }
      }
    }
    else
    {
      this.Page = (Page) null;
      Point point3 = new Point(startPoint.X + delta.X, startPoint.Y + delta.Y);
      if (this.Page != null)
      {
        PointF world = this.Page.PageUI.ConvertPixelToWorld(point3);
        PointF point4 = (Control.ModifierKeys & Keys.Shift) != Keys.None ? this.Page.PageUI.SnapPointOrtho(world, pathPoints2[grabHandleAtPoint], (VisualNode) element) : this.Page.PageUI.SnapPoint(world, (VisualNode) element);
        point3 = this.Page.PageUI.ConvertWorldToPixel(point4);
        PointF user = this.Page.PageUI.ConvertInternalToUser(point4);
        text = string.Format(LocalizationHolder.rm.GetString("Document.Model_83"), (object) user.X, (object) user.Y);
      }
      pathPoints1[grabHandleAtPoint] = (PointF) point3;
    }
    this.NewDisplayPath = new GraphicsPath(pathPoints1, pathTypes);
    this.DrawNewGeometryPreview((Graphics) null);
    base.ChangingPoint(startPoint, delta);
    this.DocumentControl?.DocumentManager?.SetMessageText(text);
  }

  /// <summary>Обновить геометрию</summary>
  public override void UpdateGeometry()
  {
    Polyline element = this.Element as Polyline;
    PageControl pageControl = this.PageControl;
    if (element != null && element.Page is Page page && pageControl != null && element.PathPoints.Length != 0)
    {
      PointF[] pointFArray = (PointF[]) element.PathPoints.Clone();
      page.TransformMatrix.TransformPoints(pointFArray);
      for (int index = 0; index < pointFArray.Length; ++index)
        pointFArray[index] = page.ConvertWorldToPixelF(pointFArray[index]);
      float num = element.LineWidth;
      if ((double) num == 0.0)
        num = PageElementNode.DefaultLineWidth;
      float lineWidth = (float) page.ConvertXMmToPixel(num * pageControl.PageScale);
      if ((double) lineWidth == 0.0)
        lineWidth = 1f;
      this.ConstructRegion(new GraphicsPath(pointFArray, element.PathTypes), lineWidth);
    }
    base.UpdateGeometry();
  }

  /// <summary>Обновить геометрию элемента страницы</summary>
  public override void UpdateElementGeometry()
  {
    if (this.Element is Polyline element && element.Page is Page page && element.PathPoints.Length != 0)
    {
      GraphicsPath graphicsPath = (GraphicsPath) this.NewDisplayPath.Clone();
      if (graphicsPath.PointCount == 0)
        return;
      element.Path = new GraphicsPath(page.ConvertPixelFToWorld(graphicsPath.PathPoints), graphicsPath.PathTypes);
    }
    base.UpdateElementGeometry();
  }

  /// <summary>Полилиния отображаемый на экране</summary>
  public GraphicsPath DisplayPath
  {
    [DebuggerStepThrough] get => this.displayPath;
  }

  /// <summary>Толщина отображаемой полилинии</summary>
  public float DisplayLineWidth
  {
    [DebuggerStepThrough] get => this.displayLineWidth;
  }

  /// <summary>Начать процесс перемещения элемента страницы</summary>
  protected override void BeginMoving(MouseEventArgs mouseArgs, Keys modifierKeys)
  {
    base.BeginMoving(mouseArgs, modifierKeys);
    this.DrawNewGeometryPreview((Graphics) null);
  }

  /// <summary>Отменить процесс перемещения элемента страницы</summary>
  public override void CancelMoving(Point start, bool erasePreview)
  {
    if (erasePreview)
      this.EraseNewGeometryPreview(true);
    base.CancelMoving(start, erasePreview);
  }

  /// <summary>Завершить процесс перемещения элемента страницы</summary>
  protected override void EndMoving(
    MouseEventArgs mouseArgs,
    Keys modifierKeys,
    Point startPoint,
    Point delta)
  {
    this.Element?.OwnerDocument?.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_588"));
    try
    {
      this.EraseNewGeometryPreview(true);
      Polyline element1 = this.Element as Polyline;
      if (this.GeometryChangingBlocked || element1 == null || this.Polyline.Page == null || this.displayPath == null)
        return;
      int grabHandleAtPoint = this.GetGrabHandleAtPoint(startPoint);
      bool flag = false;
      PageControl pageControl = this.PageControl;
      PointF[] pts = (PointF[]) element1.PathPoints.Clone();
      if (pageControl != null)
      {
        if (grabHandleAtPoint == -1 || this.Page != null && this.Page.PageUI != null && this.PageControl != null && this.PageControl.IsPasting)
        {
          Page page = this.PageControl?.GetPageAtPoint(mouseArgs.Location) ?? this.Page;
          if (page != null && this.Page != null && page != this.Page && this.Element != null && Control.ModifierKeys != Keys.Control)
          {
            PageElementNode element2 = this.Element;
            element2.SuspendRefreshUI();
            this.Page.RemoveChildNode((DocumentTreeNode) this.Element, false, false);
            page.AddChildNode((DocumentTreeNode) element2, false, false);
            this.Element = element2;
            element2.ResumeRefreshUI(false);
            this.Page = (Page) null;
            element1 = this.Element as Polyline;
          }
          using (Matrix matrix = new Matrix())
          {
            RectangleF bounds1 = element1.Path.GetBounds();
            RectangleF bounds2 = this.displayPath.GetBounds();
            PointF location1 = bounds1.Location;
            Point point1 = new Point((int) bounds2.X, (int) bounds2.Y);
            Point point2 = new Point(point1.X + delta.X, point1.Y + delta.Y);
            if (page != null)
            {
              if (page.PageUI != null)
              {
                PointF world = page.PageUI.ConvertPixelToWorld(point2);
                RectangleF rect = new RectangleF((Control.ModifierKeys & Keys.Shift) != Keys.None ? page.PageUI.SnapPointOrtho(world, location1, (VisualNode) element1) : page.PageUI.SnapPoint(world, (VisualNode) element1), bounds1.Size);
                PointF location2 = page.PageUI.SnapRectangle(rect, (VisualNode) null).Location;
                flag = true;
                matrix.Translate(location2.X - bounds1.X, location2.Y - bounds1.Y);
                matrix.TransformPoints(pts);
                for (int index = 0; index < pts.Length; ++index)
                  pts[index] = UnitsConverter.RoundPoint(pts[index], 5);
              }
            }
          }
        }
        else
        {
          Point point3 = Point.Round(this.displayPath.PathPoints[grabHandleAtPoint]);
          Point point4 = new Point(point3.X + delta.X, point3.Y + delta.Y);
          if (this.Page?.PageUI != null)
          {
            PointF world = this.Page.PageUI.ConvertPixelToWorld(point4);
            PointF pointF = (Control.ModifierKeys & Keys.Shift) != Keys.None ? this.Page.PageUI.SnapPointOrtho(world, pts[grabHandleAtPoint], (VisualNode) element1) : this.Page.PageUI.SnapPoint(world, (VisualNode) element1);
            flag = true;
            pts[grabHandleAtPoint] = pointF;
          }
        }
      }
      if (flag)
      {
        if (grabHandleAtPoint == -1 && Control.ModifierKeys == Keys.Control)
        {
          Polyline child = (Polyline) element1.Clone();
          child.SetPath(new GraphicsPath(pts, child.PathTypes), false, false);
          pageControl?.GetPageAtPoint(mouseArgs.Location)?.AddChildNode((DocumentTreeNode) child, true, true);
        }
        else
          element1.Path = new GraphicsPath(pts, element1.PathTypes);
        this.PageControl?.Document.SuspendRefreshUI();
        try
        {
          this.DocumentControl?.SetSelection(new List<DocumentTreeNode>(), false, false);
          this.DocumentControl?.SetSelection(this.DocumentControl.SelectedNodes, false, false);
        }
        finally
        {
          this.PageControl?.Document.ResumeRefreshUI(true);
        }
      }
      base.EndMoving(mouseArgs, modifierKeys, startPoint, delta);
      DocumentControl documentControl = this.DocumentControl;
      if (documentControl == null || documentControl.DocumentManager == null)
        return;
      documentControl.DocumentManager.UpdateSelectedElementInfo();
    }
    finally
    {
      if (this.Element.OwnerDocument != null && this.Element.OwnerDocument.UndoManager != null)
        this.Element.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Стереть предпросмотр нового пути</summary>
  public override void EraseNewGeometryPreview(bool update)
  {
    if (this.newPathPreview == null)
      return;
    GraphicsPath newPathPreview = this.newPathPreview;
    this.newPathPreview = (GraphicsPath) null;
    if (this.PageControl != null)
    {
      Pen pen = new Pen(Color.Black, 0.0f);
      Rectangle rc = Rectangle.Round(newPathPreview.GetBounds(new Matrix(), pen));
      ref Rectangle local1 = ref rc;
      int x = rc.X - this.GrabHandleSize.Width;
      int y1 = rc.Y;
      Size grabHandleSize = this.GrabHandleSize;
      int height = grabHandleSize.Height;
      int y2 = y1 - height;
      Point point = new Point(x, y2);
      local1.Location = point;
      ref Rectangle local2 = ref rc;
      int width1 = rc.Width;
      grabHandleSize = this.GrabHandleSize;
      int width2 = grabHandleSize.Width;
      Size size = new Size(width1 + width2 + 1, rc.Height + this.GrabHandleSize.Height + 1);
      local2.Size = size;
      this.PageControl.Invalidate(rc, true);
      if (update)
        this.PageControl.Update();
    }
    newPathPreview.Dispose();
  }

  /// <summary>Нарисовать предпросмотр нового пути полинии</summary>
  /// <param name="g">Graphics</param>
  public override void DrawNewGeometryPreview(Graphics g)
  {
    if (this.PageControl == null || this.NewDisplayPath == null)
      return;
    bool flag = g == null;
    if (g == null)
    {
      if (this.newPathPreview != null)
        this.EraseNewGeometryPreview(true);
      g = this.PageControl.CreateGraphics();
    }
    try
    {
      Pen pen = new Pen(Color.Black, 0.0f);
      pen.DashStyle = DashStyle.Dash;
      if (this.newPathPreview != null)
        this.newPathPreview.Dispose();
      this.newPathPreview = (GraphicsPath) this.NewDisplayPath.Clone();
      g.DrawPath(pen, this.newPathPreview);
    }
    finally
    {
      if (flag && g != null)
        g.Dispose();
    }
  }

  /// <summary>Построить объект Region соответствующий графическому пути</summary>
  /// <param name="path">Путь</param>
  /// <param name="lineWidth">Толщина линий</param>
  public void ConstructRegion(GraphicsPath path, float lineWidth)
  {
    if (path == null)
    {
      this.displayPath = (GraphicsPath) null;
    }
    else
    {
      List<PointF> pointFList1 = new List<PointF>((IEnumerable<PointF>) path.PathPoints);
      List<byte> byteList = new List<byte>((IEnumerable<byte>) path.PathTypes);
      for (int index = pointFList1.Count - 1; index >= 1; --index)
      {
        if (pointFList1[index] == pointFList1[index - 1])
        {
          pointFList1.RemoveAt(index);
          byteList.RemoveAt(index);
        }
      }
      if (pointFList1.Count == 1)
      {
        List<PointF> pointFList2 = pointFList1;
        PointF pointF1 = pointFList1[0];
        double x = (double) pointF1.X + (double) lineWidth;
        pointF1 = pointFList1[0];
        double y = (double) pointF1.Y + (double) lineWidth;
        PointF pointF2 = new PointF((float) x, (float) y);
        pointFList2.Add(pointF2);
        byteList.Add((byte) 1);
      }
      else if (pointFList1.Count == 0)
      {
        this.displayPath = (GraphicsPath) null;
        return;
      }
      this.contourPath = new GraphicsPath(pointFList1.ToArray(), byteList.ToArray());
      if ((double) lineWidth < 3.0)
        lineWidth = 3f;
      using (Pen pen = new Pen(Color.Black, lineWidth))
        this.contourPath.Widen(pen);
      this.contourPath.FillMode = FillMode.Winding;
      RectangleF bounds;
      using (Graphics graphics = this.PageControl.CreateGraphics())
        bounds = new Region(this.contourPath).GetBounds(graphics);
      Rectangle rectangle = Rectangle.Round(bounds);
      ++rectangle.Width;
      ++rectangle.Height;
      this.Bounds = rectangle;
      this.displayPath = new GraphicsPath();
      this.displayPath.AddPath(path, true);
    }
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  internal override void OnMouseDown(MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left)
      this.leftMouseDownPos = new Point(e.X, e.Y);
    if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.PageControl != null && !this.PageControl.IsPasting)
      this.SelectElement(Control.ModifierKeys, false, Point.Empty, false, false);
    base.OnMouseDown(e);
  }
}
