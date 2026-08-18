// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.RectanglePageElementCreator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Document.Model.Undo;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс, обеспечивает ИП при
/// создании прямоугольных элементов страницы</summary>
public abstract class RectanglePageElementCreator : PageElementCreator
{
  /// <summary>Режим выбора второй точки</summary>
  protected bool IsSecondPointSelecting;
  /// <summary>Вторая точка диагонали</summary>
  protected Point secondPoint = Point.Empty;
  /// <summary>Первая точка диагонали</summary>
  protected Point firstPoint = Point.Empty;

  private RectangleF NewBounds()
  {
    return this.PageControl != null && this.HostPage != null ? this.RectangleFromPoints(this.HostPage.PageUI.SnapPoint(this.HostPage.PageUI.ConvertPixelToWorld(this.firstPoint), (VisualNode) null), this.HostPage.PageUI.SnapPoint(this.HostPage.PageUI.ConvertPixelToWorld(this.secondPoint), (VisualNode) null)) : RectangleF.Empty;
  }

  /// <summary>Создать прямоугольник из двух диагональных точек</summary>
  /// <param name="p1">Точка диагонали 1</param>
  /// <param name="p2">Точка диагонали 2</param>
  /// <returns>Прямоугольник</returns>
  protected Rectangle RectangleFromPoints(Point p1, Point p2)
  {
    return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
  }

  /// <summary>Создать прямоугольник из двух диагональных точек</summary>
  /// <param name="p1">Точка диагонали 1</param>
  /// <param name="p2">Точка диагонали 2</param>
  /// <returns>Прямоугольник</returns>
  protected RectangleF RectangleFromPoints(PointF p1, PointF p2)
  {
    return UnitsConverter.RoundPectangle(new RectangleF(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y)), 5);
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseDown(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    Page pageAtPoint = this.PageControl.GetPageAtPoint(e.Location);
    if (this.HostPage == null || e.Button != MouseButtons.Left || this.HostPage != pageAtPoint)
      return;
    this.firstPoint = new Point(e.X, e.Y);
    if (this.DocumentControl.PageControl != null)
      this.firstPoint = this.HostPage.PageUI.SnapPixelToWorldGrid(this.firstPoint, (VisualNode) null);
    this.secondPoint = this.firstPoint;
    this.IsSecondPointSelecting = true;
    if (this.DocumentControl == null || this.DocumentControl.DocumentManager == null)
      return;
    RectangleF rectangleF = this.NewBounds();
    PointF user1 = pageAtPoint.ConvertInternalToUser(rectangleF.Location);
    PointF user2 = pageAtPoint.ConvertInternalToUser(new PointF(rectangleF.Right, rectangleF.Bottom));
    SizeF user3 = pageAtPoint.ConvertInternalToUser(rectangleF.Size);
    this.DocumentControl.DocumentManager.SetMessageText(string.Format(LocalizationHolder.rm.GetString("Document.Model_94"), (object) user1.X, (object) user3.Width, (object) user2.X, (object) user2.Y, (object) user3.Height, (object) user1.Y));
  }

  /// <summary>Вызвает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseMove(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    Page pageAtPoint = this.PageControl.GetPageAtPoint(e.Location);
    if (this.HostPage == null || !this.IsSecondPointSelecting)
      return;
    this.secondPoint = new Point(e.X, e.Y);
    if (this.DocumentControl?.PageControl != null)
      this.secondPoint = this.HostPage.PageUI.SnapPixelToWorldGrid(this.secondPoint, (VisualNode) null);
    this.HostPage.RefreshUI();
    if (this.DocumentControl?.PageControl == null || pageAtPoint == null || this.DocumentControl.DocumentManager == null)
      return;
    RectangleF rectangleF = this.NewBounds();
    PointF user1 = pageAtPoint.ConvertInternalToUser(rectangleF.Location);
    PointF user2 = this.HostPage.PageUI.ConvertInternalToUser(new PointF(rectangleF.Right, rectangleF.Bottom));
    SizeF user3 = pageAtPoint.ConvertInternalToUser(rectangleF.Size);
    this.DocumentControl.DocumentManager.SetMessageText(string.Format(LocalizationHolder.rm.GetString("Document.Model_95"), (object) user1.X, (object) user3.Width, (object) user2.X, (object) user2.Y, (object) user3.Height, (object) user1.Y));
  }

  /// <summary>Вызвает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseUp(MouseEventArgs e)
  {
    if (this.PageControl == null || this.HostPage == null || !this.IsSecondPointSelecting || e.Button != MouseButtons.Left)
      return;
    if (this.HostPage == this.PageControl.GetPageAtPoint(e.Location))
      this.secondPoint = new Point(e.X, e.Y);
    if (this.DocumentControl?.PageControl == null)
      return;
    RectangleF bounds = this.NewBounds();
    DocumentControl documentControl = this.DocumentControl;
    documentControl?.DocumentManager?.SetMessageText("");
    DocumentTreeNode rectangleElement = this.CreateRectangleElement((DocumentTreeNode) this.HostPage, bounds);
    this.Reset();
    if (rectangleElement != null && this.HostPage?.OwnerDocument?.UndoManager != null)
      this.HostPage.OwnerDocument.UndoManager.CreateUndo((IUndoAction) new UndoAddAction(this.HostPage.OwnerDocument.UndoManager, (DocumentTreeNode) this.HostPage, rectangleElement), true);
    documentControl?.SetSelection(rectangleElement, true, Point.Empty, true, false);
  }

  /// <summary>Вызвает событие Paint</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnPaint(PaintEventArgs e)
  {
    if (!this.IsSecondPointSelecting)
      return;
    using (Pen pen = new Pen(Color.Black))
    {
      pen.DashStyle = DashStyle.Dash;
      RubberBand.DrawXorRectangle(e.Graphics, this.RectangleFromPoints(this.firstPoint, this.secondPoint), Color.White);
    }
  }

  /// <summary>Сбросить режим создания элемента</summary>
  public override void Reset()
  {
    this.IsSecondPointSelecting = false;
    base.Reset();
  }

  /// <summary>Создать прямоугольный элемент</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <returns>Прямоугольный элемент</returns>
  public abstract DocumentTreeNode CreateRectangleElement(
    DocumentTreeNode parent,
    RectangleF bounds);

  public Point FirstPoint
  {
    get => this.firstPoint;
    set => this.firstPoint = value;
  }
}
