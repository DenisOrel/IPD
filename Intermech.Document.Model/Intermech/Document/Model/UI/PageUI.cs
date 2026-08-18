// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PageUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

public class PageUI : PageElementUI
{
  protected Page page;
  private bool uiUpdated;
  private MatrixWrapper transformMatrix = new MatrixWrapper();

  public PageUI(Page page) => this.page = page;

  public override Page Page
  {
    get => this.page;
    set => this.page = value;
  }

  /// <summary>Эелемент прошел UpdateLayout</summary>
  public bool UIUpdated
  {
    get => this.uiUpdated;
    set => this.uiUpdated = value;
  }

  public override Rectangle Bounds
  {
    get
    {
      Rectangle bounds = base.Bounds;
      int x = bounds.X;
      bounds = base.Bounds;
      int y = bounds.Y;
      bounds = base.Bounds;
      int width = bounds.Width + 1;
      bounds = base.Bounds;
      int height = bounds.Height + 1;
      return new Rectangle(x, y, width, height);
    }
    set => base.Bounds = value;
  }

  public override PageControl PageControl
  {
    get => this.Page != null ? this.Page.PageControl : (PageControl) null;
  }

  public override void OnPaint(PaintEventArgs e)
  {
    GraphicsUnit pageUnit = e.Graphics.PageUnit;
    e.Graphics.PageUnit = GraphicsUnit.Pixel;
    RectangleF clipBounds = e.Graphics.ClipBounds;
    Matrix transform = e.Graphics.Transform;
    e.Graphics.Transform = new Matrix();
    e.Graphics.SetClip(this.Bounds);
    foreach (PageElementUI pageElementUi in this.PageElementUIs)
      pageElementUi.OnPaint(e);
    base.OnPaint(e);
    e.Graphics.Transform = transform;
    e.Graphics.PageUnit = pageUnit;
    e.Graphics.SetClip(clipBounds);
  }

  /// <summary>Пересчитать матрицу трансформации</summary>
  public virtual void UpdateTransformMatrix()
  {
    if (this.page == null)
      return;
    int num = this.PageControl == null || this.page.DocumentControl?.Document == null ? 1 : (this.page.SuspendedUpdateUIGeometryFlag ? 1 : 0);
    if (num == 0)
      this.page.SuspendUpdateUIGeometry();
    Matrix matrix1 = new Matrix();
    Matrix matrix2 = matrix1;
    PointF location = this.page.Location;
    double x = (double) location.X;
    location = this.page.Location;
    double y = (double) location.Y;
    matrix2.Translate((float) x, (float) y, System.Drawing.Drawing2D.MatrixOrder.Append);
    if (this.PageControl != null)
      matrix1.Scale(this.PageControl.PageScale, this.PageControl.PageScale, System.Drawing.Drawing2D.MatrixOrder.Append);
    this.TransformMatrix = new MatrixWrapper(matrix1);
    this.page.SetNeedUpdateUIGeometryRecursive(true, false);
    if (num != 0)
      return;
    this.page.ResumeUpdateUIGeometry(false, false);
  }

  public override Cursor GetCursor(Point point)
  {
    return Control.ModifierKeys == Keys.Control && this.IsMoving ? PageElementUI.CopyCursor : Cursors.Default;
  }

  public override void GetPageElementsInRectangle(
    Rectangle rect,
    IList<DocumentTreeNode> nodes,
    bool containsOnly)
  {
    foreach (PageElementUI pageElementUi in this.PageElementUIs)
    {
      if (pageElementUi.Element != null && pageElementUi.Element.IsVisibleNow)
      {
        Rectangle rect1 = PageElementUI.PixelRectangle(pageElementUi.Bounds);
        if (containsOnly)
        {
          if (rect.Contains(rect1))
            nodes.Add((DocumentTreeNode) pageElementUi.Element);
        }
        else if (rect.IntersectsWith(rect1))
          nodes.Add((DocumentTreeNode) pageElementUi.Element);
      }
    }
  }

  /// <summary>Получение координат прямоугольника в котором начало координат будет не PageControlа а страницы</summary>
  /// <param name="rect"></param>
  internal Rectangle GetPagesCoorRectangle(Rectangle rect)
  {
    return new Rectangle(rect.X - this.Bounds.X, rect.Y - this.Bounds.Y, rect.Width, rect.Height);
  }

  internal override void OnMouseDown(MouseEventArgs e)
  {
    Point point = new Point(e.X, e.Y);
    if (e.Button == MouseButtons.Left)
      this.leftMouseDownPos = point;
    if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.PageControl != null && !this.PageControl.IsPasting && this.Page != null)
      this.SelectElement((DocumentTreeNode) this.Page, Control.ModifierKeys, false, Point.Empty, false, false);
    base.OnMouseDown(e);
  }

  public Matrix GetUserCoorMatrix()
  {
    PointF pointF = new PointF(0.0f, 0.0f);
    int m11 = 1;
    int m22 = 1;
    PageCoorSystem pageCoorSystem = PageCoorSystem.TopLeft;
    if (this.DocumentControl != null)
      pageCoorSystem = this.DocumentControl.CoorSystem;
    switch (pageCoorSystem)
    {
      case PageCoorSystem.BottomLeft:
        pointF = this.page == null ? new PointF(0.0f, 297f) : new PointF(0.0f, this.page.Size.Height);
        m11 = 1;
        m22 = -1;
        break;
      case PageCoorSystem.TopLeft:
        pointF = new PointF(0.0f, 0.0f);
        m11 = 1;
        m22 = 1;
        break;
      case PageCoorSystem.TopRight:
        pointF = this.page == null ? new PointF(-210f, 0.0f) : new PointF(-this.page.Size.Width, 0.0f);
        m11 = -1;
        m22 = 1;
        break;
      case PageCoorSystem.BottomRight:
        if (this.page != null)
        {
          ref PointF local = ref pointF;
          SizeF size = this.page.Size;
          double x = -(double) size.Width;
          size = this.page.Size;
          double height = (double) size.Height;
          local = new PointF((float) x, (float) height);
        }
        else
          pointF = new PointF(-210f, 297f);
        m11 = -1;
        m22 = -1;
        break;
      case PageCoorSystem.Custom:
        pointF = ImDocumentEditorConfig.Instance.CustomCoorSystemPosition;
        if (this.page != null)
          pointF.Y = this.page.Size.Height - pointF.Y;
        m11 = 1;
        m22 = -1;
        break;
    }
    return new Matrix((float) m11, 0.0f, 0.0f, (float) m22, -pointF.X, pointF.Y);
  }

  /// <summary>Конвертировать координаты в формат пользователя</summary>
  /// <param name="point">Точка во внутреннем формате</param>
  /// <returns>Точка в пользовательском формате</returns>
  public virtual PointF ConvertInternalToUser(PointF point)
  {
    point = UnitsConverter.RoundPoint(MatrixWrapper.TransformPoint(this.GetUserCoorMatrix().Elements, point), 5);
    return point;
  }

  /// <summary>Конвертировать координаты в формат пользователя</summary>
  /// <param name="point">Точка во внутреннем формате</param>
  /// <returns>Точка в пользовательском формате</returns>
  public PointF ConvertInternalToUser(PointF point, Matrix m)
  {
    point = UnitsConverter.RoundPoint(MatrixWrapper.TransformPoint(m.Elements, point), 5);
    return point;
  }

  /// <summary>Преобразовать X внутреннюю координату в пользовательскую координату</summary>
  /// <param name="x">координата в миллиметрах</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertInternalXToUser(float x, Matrix m)
  {
    return this.ConvertInternalToUser(new PointF(x, 0.0f), m).X;
  }

  /// <summary>Преобразовать X внутреннюю координату в пользовательскую координату</summary>
  /// <param name="x">координата в миллиметрах</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertInternalXToUser(float x) => this.ConvertInternalToUser(new PointF(x, 0.0f)).X;

  /// <summary>Преобразовать Y внутреннюю координату в пользовательскую координату</summary>
  /// <param name="y">координата в миллиметрах</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertInternalYToUser(float y, Matrix m)
  {
    return this.ConvertInternalToUser(new PointF(0.0f, y), m).Y;
  }

  /// <summary>Преобразовать Y внутреннюю координату в пользовательскую координату</summary>
  /// <param name="y">координата в миллиметрах</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertInternalYToUser(float y) => this.ConvertInternalToUser(new PointF(0.0f, y)).Y;

  /// <summary>Конвертировать расстояние в формат пользователя</summary>
  /// <param name="distance">расстояние во внутреннем формате</param>
  /// <returns>расстояние в пользовательском формате</returns>
  public virtual float ConvertInternalDistanceToUser(float distance)
  {
    PointF user = this.ConvertInternalToUser(new PointF(distance, distance));
    double x1 = (double) user.X;
    user = this.ConvertInternalToUser(new PointF(0.0f, 0.0f));
    double x2 = (double) user.X;
    return Math.Abs((float) (x1 - x2));
  }

  /// <summary>Конвертировать расстояние в формат пользователя</summary>
  /// <param name="distance">расстояние во внутреннем формате</param>
  /// <returns>расстояние в пользовательском формате</returns>
  public float ConvertInternalDistanceToUser(float distance, Matrix m)
  {
    PointF user = this.ConvertInternalToUser(new PointF(distance, distance), m);
    double x1 = (double) user.X;
    user = this.ConvertInternalToUser(new PointF(0.0f, 0.0f), m);
    double x2 = (double) user.X;
    return Math.Abs((float) (x1 - x2));
  }

  /// <summary>Конвертировать размер в формат пользователя</summary>
  /// <param name="size">Размер во внутреннем формате</param>
  /// <returns>Размер в пользовательском формате</returns>
  public virtual SizeF ConvertInternalToUser(SizeF size)
  {
    float[] elements = this.GetUserCoorMatrix().Elements;
    return UnitsConverter.RoundSize(new SizeF(Math.Abs(elements[0]) * size.Width, Math.Abs(elements[3]) * size.Height), 5);
  }

  /// <summary>Конвертировать прямоугольник в пользовательский формат</summary>
  /// <param name="rectangle">Прямоугольник во внутреннем формате</param>
  /// <returns>Прямоугольник в пользовательском формате</returns>
  public virtual RectangleF ConvertInternalToUser(RectangleF rectangle)
  {
    PointF user1 = this.ConvertInternalToUser(rectangle.Location);
    PointF user2 = this.ConvertInternalToUser(new PointF(rectangle.Right, rectangle.Bottom));
    if ((double) user1.X > (double) user2.X)
    {
      float x = user1.X;
      user1.X = user2.X;
      user2.X = x;
    }
    if ((double) user1.Y > (double) user2.Y)
    {
      float y = user1.Y;
      user1.Y = user2.Y;
      user2.Y = y;
    }
    return UnitsConverter.RoundPectangle(RectangleF.FromLTRB(user1.X, user1.Y, user2.X, user2.Y), 5);
  }

  /// <summary>Преобразовать точку из пользовательского формата</summary>
  /// <param name="point">Точка в пользовательском формате</param>
  /// <returns>Точка во внутреннем формате</returns>
  public virtual PointF ConvertUserToInternal(PointF point)
  {
    Matrix userCoorMatrix = this.GetUserCoorMatrix();
    userCoorMatrix.Invert();
    return UnitsConverter.RoundPoint(MatrixWrapper.TransformPoint(userCoorMatrix.Elements, point), 5);
  }

  /// <summary>Преобразовать точку из пользовательского формата</summary>
  /// <param name="point">Точка в пользовательском формате</param>
  /// <returns>Точка во внутреннем формате</returns>
  public PointF ConvertUserToInternal(PointF point, Matrix m)
  {
    m.Invert();
    return UnitsConverter.RoundPoint(MatrixWrapper.TransformPoint(m.Elements, point), 5);
  }

  /// <summary>Преобразовать точку из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="point">Точка в пользовательских координатах</param>
  /// <returns>Точка в пикселях на котроле страницы</returns>
  public Point ConvertUserToPageControl(PointF point)
  {
    point = this.ConvertUserToInternal(point);
    return this.ConvertWorldToPixel(point);
  }

  /// <summary>Преобразовать точку из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="point">Точка в пользовательских координатах</param>
  /// <returns>Точка в пикселях на котроле страницы</returns>
  public Point ConvertUserToPageControl(PointF point, Matrix m)
  {
    point = this.ConvertUserToInternal(point, m);
    return this.ConvertWorldToPixel(point);
  }

  /// <summary>Преобразовать X координату из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="x">координата в пользовательских координатах</param>
  /// <returns>координата в пикселях на котроле страницы</returns>
  public int ConvertUserXToPageControl(float x)
  {
    return (int) ((PointF) this.ConvertWorldToPixel(this.ConvertUserToInternal(new PointF(x, 0.0f)))).X;
  }

  /// <summary>Преобразовать X координату из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="x">координата в пользовательских координатах</param>
  /// <returns>координата в пикселях на котроле страницы</returns>
  public int ConvertUserXToPageControl(float x, Matrix m)
  {
    return (int) ((PointF) this.ConvertWorldToPixel(this.ConvertUserToInternal(new PointF(x, 0.0f), m))).X;
  }

  /// <summary>Преобразовать Y координату из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="y">координата в пользовательских координатах</param>
  /// <returns>координата в пикселях на котроле страницы</returns>
  public int ConvertUserYToPageControl(float y)
  {
    return (int) ((PointF) this.ConvertWorldToPixel(this.ConvertUserToInternal(new PointF(0.0f, y)))).Y;
  }

  /// <summary>Преобразовать Y координату из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="y">координата в пользовательских координатах</param>
  /// <returns>координата в пикселях на котроле страницы</returns>
  public int ConvertUserYToPageControl(float y, Matrix m)
  {
    return (int) ((PointF) this.ConvertWorldToPixel(this.ConvertUserToInternal(new PointF(0.0f, y), m))).Y;
  }

  /// <summary>Преобразовать прямоугольник из пользовательских координат в пикселы контрола страницы</summary>
  /// <param name="point">Прямоугольник в пользовательских координатах</param>
  /// <returns>Прямоугольник в пикселях на котроле страницы</returns>
  public Rectangle ConvertUserToPageControl(RectangleF rect)
  {
    rect = this.ConvertUserToInternal(rect);
    return this.ConvertWorldToPixel(rect);
  }

  /// <summary>Преобразовать точку из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="point">Точка в пикселях на котроле страницы</param>
  /// <returns>Точка в пользовательских координатах</returns>
  public PointF ConvertPixelToUser(Point point)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(point));
  }

  /// <summary>Преобразовать точку из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="point">Точка в пикселях на котроле страницы</param>
  /// <returns>Точка в пользовательских координатах</returns>
  public PointF ConvertPixelToUser(Point point, Matrix m)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(point), m);
  }

  /// <summary>Преобразовать X координату из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="x">координата в пикселях на котроле страницы</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertPixelXToUser(int x)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(new Point(x, 0))).X;
  }

  /// <summary>Преобразовать X координату из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="x">координата в пикселях на котроле страницы</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertPixelXToUser(int x, Matrix m)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(new Point(x, 0)), m).X;
  }

  /// <summary>Преобразовать Y координату из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="y">координата в пикселях на котроле страницы</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertPixelYToUser(int y)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(new Point(0, y))).Y;
  }

  /// <summary>Преобразовать Y координату из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="y">координата в пикселях на котроле страницы</param>
  /// <returns>координата в пользовательских координатах</returns>
  public float ConvertPixelYToUser(int y, Matrix m)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(new Point(0, y)), m).Y;
  }

  /// <summary>Преобразовать прямоугольник из пикселей контрола страницы в пользовательский координаты</summary>
  /// <param name="point">Прямоугольник в пикселях на котроле страницы</param>
  /// <returns>Прямоугольник в пользовательских координатах</returns>
  public RectangleF ConvertPixelToUser(Rectangle rect)
  {
    return this.ConvertInternalToUser(this.ConvertPixelToWorld(rect));
  }

  /// <summary>Преобразовать размер из пользовательского формата</summary>
  /// <param name="size">Размер в пользовательском формате</param>
  /// <returns>Размер во внутреннем формате</returns>
  public virtual SizeF ConvertUserToInternal(SizeF size) => size;

  /// <summary>Преобразовать прямоугольник из пользовательского формата</summary>
  /// <param name="rectangle">Прямоугольник в пользовательском формате</param>
  /// <returns>Прямоугольник во внутреннем формате</returns>
  public virtual RectangleF ConvertUserToInternal(RectangleF rectangle)
  {
    PointF pointF1 = this.ConvertUserToInternal(rectangle.Location);
    PointF pointF2 = this.ConvertUserToInternal(new PointF(rectangle.Right, rectangle.Bottom));
    if ((double) pointF1.Y > (double) pointF2.Y)
    {
      float y = pointF1.Y;
      pointF1.Y = pointF2.Y;
      pointF2.Y = y;
    }
    return UnitsConverter.RoundPectangle(RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y), 5);
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="point">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public PointF ConvertPixelToWorld(Point point)
  {
    if (this.transformMatrix == null || this.transformMatrix.Matrix == null)
      return UnitsConverter.PixelsToMm(point, this.DisplayDpi);
    Matrix matrix = this.transformMatrix.Matrix.Clone();
    matrix.Invert();
    return MatrixWrapper.TransformPoint(matrix.Elements, UnitsConverter.PixelsToMm(point, this.DisplayDpi));
  }

  /// <summary>Перевести пиксели в мировые координаты и привязать к сетке</summary>
  /// <param name="x">координата  в пикселях</param>
  /// <returns>координата в миллиметрах</returns>
  public float ConvertPixelXToWorld(int x, bool needSnap, Matrix m)
  {
    PointF point = this.ConvertPixelToWorld(new Point(x, 0));
    if (needSnap)
      point = this.SnapPoint(point, (VisualNode) null, m);
    return point.X;
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="x">координата  в пикселях</param>
  /// <returns>координата в миллиметрах</returns>
  public float ConvertPixelXToWorld(int x) => this.ConvertPixelToWorld(new Point(x, 0)).X;

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="x">координата  в пикселях</param>
  /// <returns>координата в миллиметрах</returns>
  public float ConvertPixelYToWorld(int y) => this.ConvertPixelToWorld(new Point(0, y)).Y;

  /// <summary>Перевести пиксели в мировые координаты и привязать к сетке</summary>
  /// <param name="x">координата  в пикселях</param>
  /// <returns>координата в миллиметрах</returns>
  public float ConvertPixelYToWorld(int y, bool needSnap, Matrix m)
  {
    PointF point = this.ConvertPixelToWorld(new Point(0, y));
    if (needSnap)
      point = this.SnapPoint(point, (VisualNode) null, m);
    return point.Y;
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="rectangle">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public RectangleF ConvertPixelToWorld(Rectangle rectangle)
  {
    Matrix matrix = this.transformMatrix.Matrix.Clone();
    matrix.Invert();
    float[] elements = matrix.Elements;
    PointF pointF1 = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(rectangle.Location, this.DisplayDpi));
    PointF pointF2 = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(new Point(rectangle.Right, rectangle.Bottom), this.DisplayDpi));
    return RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public SizeF ConvertPixelToWorld(Size size)
  {
    return this.ConvertPixelToWorld(new Rectangle(new Point(0, 0), size)).Size;
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="points">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public PointF[] ConvertPixelFToWorld(PointF[] points)
  {
    PointF[] pointFArray = (PointF[]) points.Clone();
    Matrix matrix = this.transformMatrix.Matrix.Clone();
    matrix.Invert();
    float[] elements = matrix.Elements;
    for (int index = 0; index < pointFArray.Length; ++index)
      pointFArray[index] = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(Point.Round(pointFArray[index]), this.DisplayDpi));
    return pointFArray;
  }

  /// <summary>Преобразовать мировую координату X в пиксели</summary>
  /// <param name="x">x</param>
  /// <returns>Координата x в пикселях</returns>
  public int ConvertWorldXToPixel(float x) => this.ConvertWorldToPixel(new PointF(x, 0.0f)).X;

  /// <summary>Преобразовать мировую координату Y в пиксели</summary>
  /// <param name="y">y</param>
  /// <returns>Координата Y в пикселях</returns>
  public int ConvertWorldYToPixel(float y) => this.ConvertWorldToPixel(new PointF(0.0f, y)).Y;

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="point">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public Point ConvertWorldToPixel(PointF point)
  {
    return UnitsConverter.MmToPixels(this.transformMatrix.TransformPoint(point), this.DisplayDpi);
  }

  private PointF DisplayDpi
  {
    get
    {
      PointF displayDpi = PageControl.DefaultDisplayDpi;
      if (this.PageControl != null)
        displayDpi = this.PageControl.DisplayDpi;
      return displayDpi;
    }
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public Rectangle ConvertWorldToPixel(RectangleF rectangle)
  {
    PointF mm1 = this.transformMatrix.TransformPoint(rectangle.Location);
    PointF mm2 = this.transformMatrix.TransformPoint(new PointF(rectangle.Right, rectangle.Bottom));
    Point pixels1 = UnitsConverter.MmToPixels(mm1, this.DisplayDpi);
    PointF displayDpi = this.DisplayDpi;
    Point pixels2 = UnitsConverter.MmToPixels(mm2, displayDpi);
    return Rectangle.FromLTRB(pixels1.X, pixels1.Y, pixels2.X, pixels2.Y);
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public Size ConvertWorldToPixel(SizeF size)
  {
    return this.ConvertWorldToPixel(new RectangleF(new PointF(0.0f, 0.0f), size)).Size;
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="points">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public PointF[] ConvertWorldToPixelF(PointF[] points)
  {
    PointF[] pixelF = (PointF[]) points.Clone();
    for (int index = 0; index < pixelF.Length; ++index)
    {
      pixelF[index] = this.transformMatrix.TransformPoint(pixelF[index]);
      pixelF[index] = UnitsConverter.MmToPixelsF(pixelF[index], this.DisplayDpi);
    }
    return pixelF;
  }

  /// <summary>Преобразовать мировые координаты в пиксели</summary>
  /// <param name="point">Точка в мировых координатах</param>
  /// <returns>Точка в пикселях</returns>
  public PointF ConvertWorldToPixelF(PointF point)
  {
    point = this.transformMatrix.TransformPoint(point);
    return UnitsConverter.MmToPixelsF(point, this.DisplayDpi);
  }

  /// <summary>Размер области привязки</summary>
  [TypeConverter(typeof (FloatConverter))]
  public virtual float SnapSize
  {
    [DebuggerStepThrough] get => ImDocumentEditorConfig.Instance.SnapSize;
  }

  /// <summary>Получить ближайшую точку привязки</summary>
  /// <param name="point">Исходная точка</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns></returns>
  public Intermech.Interfaces.Document.SnapPoint GetNearestSnapPoint(
    PointF point,
    VisualNode excludeNode)
  {
    List<Intermech.Interfaces.Document.SnapPoint> snapPointList = new List<Intermech.Interfaces.Document.SnapPoint>();
    this.page.GetSnapPoints(point, this.SnapSize, snapPointList, excludeNode);
    float num1 = 0.0f;
    float num2 = 0.0f;
    bool flag = true;
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint = (Intermech.Interfaces.Document.SnapPoint) null;
    int index = 0;
    for (int count = snapPointList.Count; index < count; ++index)
    {
      if (nearestSnapPoint == null || snapPointList[index].PointType == SnapPointType.Node | flag && (double) num1 < (double) (num2 = UnitsConverter.LineLength(point, snapPointList[index].Point)))
      {
        if (snapPointList[index].PointType == SnapPointType.Node)
          flag = false;
        if (nearestSnapPoint == null)
          num2 = UnitsConverter.LineLength(point, snapPointList[index].Point);
        num1 = num2;
        nearestSnapPoint = snapPointList[index];
      }
    }
    return nearestSnapPoint;
  }

  /// <summary>Привязать точку к существующим элементам или гриду</summary>
  /// <param name="point">Исходная точка, для которой ищется привязка</param>
  /// <param name="startPoint">Начало ортогональной линии</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns></returns>
  public PointF SnapPointOrtho(PointF point, PointF startPoint, VisualNode excludeNode)
  {
    if ((double) point.X < 0.0)
      point.X = 0.0f;
    if ((double) point.Y < 0.0)
      point.Y = 0.0f;
    double x = (double) point.X;
    SizeF size = this.page.Size;
    double width1 = (double) size.Width;
    if (x > width1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double width2 = (double) size.Width;
      local.X = (float) width2;
    }
    double y = (double) point.Y;
    size = this.page.Size;
    double height1 = (double) size.Height;
    if (y > height1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double height2 = (double) size.Height;
      local.Y = (float) height2;
    }
    PointF pointF = point;
    if ((double) Math.Abs(point.X - startPoint.X) < (double) Math.Abs(point.Y - startPoint.Y))
      point.X = startPoint.X;
    else
      point.Y = startPoint.Y;
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint = this.GetNearestSnapPoint(point, excludeNode);
    if (nearestSnapPoint != null)
    {
      switch (nearestSnapPoint.PointType)
      {
        case SnapPointType.Node:
        case SnapPointType.LineXY:
          pointF = nearestSnapPoint.Point;
          if ((double) Math.Abs(pointF.X - startPoint.X) < (double) Math.Abs(pointF.Y - startPoint.Y))
          {
            pointF.X = startPoint.X;
            break;
          }
          pointF.Y = startPoint.Y;
          break;
        case SnapPointType.LineX:
          pointF.X = nearestSnapPoint.Point.X;
          pointF.Y = this.SnapToGrid(point).Y;
          if ((double) Math.Abs(pointF.X - startPoint.X) < (double) Math.Abs(pointF.Y - startPoint.Y))
          {
            pointF.X = startPoint.X;
            break;
          }
          pointF.Y = startPoint.Y;
          break;
        case SnapPointType.LineY:
          pointF.X = this.SnapToGrid(point).X;
          pointF.Y = nearestSnapPoint.Point.Y;
          if ((double) Math.Abs(pointF.X - startPoint.X) < (double) Math.Abs(pointF.Y - startPoint.Y))
          {
            pointF.X = startPoint.X;
            break;
          }
          pointF.Y = startPoint.Y;
          break;
      }
    }
    else
    {
      pointF = this.SnapToGrid(point);
      if ((double) Math.Abs(pointF.X - startPoint.X) < (double) Math.Abs(pointF.Y - startPoint.Y))
        pointF.X = startPoint.X;
      else
        pointF.Y = startPoint.Y;
    }
    return pointF;
  }

  /// <summary>Привязать точку к существующим элементам или гриду</summary>
  /// <param name="point">Исходная точка</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns></returns>
  public PointF SnapPoint(PointF point, VisualNode excludeNode)
  {
    if ((double) point.X < 0.0)
      point.X = 0.0f;
    if ((double) point.Y < 0.0)
      point.Y = 0.0f;
    double x = (double) point.X;
    SizeF size = this.page.Size;
    double width1 = (double) size.Width;
    if (x > width1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double width2 = (double) size.Width;
      local.X = (float) width2;
    }
    double y = (double) point.Y;
    size = this.page.Size;
    double height1 = (double) size.Height;
    if (y > height1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double height2 = (double) size.Height;
      local.Y = (float) height2;
    }
    PointF pointF = point;
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint = this.GetNearestSnapPoint(point, excludeNode);
    if (nearestSnapPoint != null)
    {
      switch (nearestSnapPoint.PointType)
      {
        case SnapPointType.Node:
        case SnapPointType.LineXY:
          pointF = nearestSnapPoint.Point;
          break;
        case SnapPointType.LineX:
          pointF.X = nearestSnapPoint.Point.X;
          pointF.Y = this.SnapToGrid(point).Y;
          break;
        case SnapPointType.LineY:
          pointF.X = this.SnapToGrid(point).X;
          pointF.Y = nearestSnapPoint.Point.Y;
          break;
      }
    }
    else
      pointF = this.SnapToGrid(point);
    return pointF;
  }

  /// <summary>Привязать точку к существующим элементам или гриду</summary>
  /// <param name="point">Исходная точка</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns></returns>
  public PointF SnapPoint(PointF point, VisualNode excludeNode, Matrix m)
  {
    if ((double) point.X < 0.0)
      point.X = 0.0f;
    if ((double) point.Y < 0.0)
      point.Y = 0.0f;
    double x = (double) point.X;
    SizeF size = this.page.Size;
    double width1 = (double) size.Width;
    if (x > width1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double width2 = (double) size.Width;
      local.X = (float) width2;
    }
    double y = (double) point.Y;
    size = this.page.Size;
    double height1 = (double) size.Height;
    if (y > height1)
    {
      ref PointF local = ref point;
      size = this.page.Size;
      double height2 = (double) size.Height;
      local.Y = (float) height2;
    }
    PointF pointF = point;
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint = this.GetNearestSnapPoint(point, excludeNode);
    if (nearestSnapPoint != null)
    {
      switch (nearestSnapPoint.PointType)
      {
        case SnapPointType.Node:
        case SnapPointType.LineXY:
          pointF = nearestSnapPoint.Point;
          break;
        case SnapPointType.LineX:
          pointF.X = nearestSnapPoint.Point.X;
          pointF.Y = this.SnapToGrid(point).Y;
          break;
        case SnapPointType.LineY:
          pointF.X = this.SnapToGrid(point).X;
          pointF.Y = nearestSnapPoint.Point.Y;
          break;
      }
    }
    else
      pointF = this.SnapToGrid(point);
    return pointF;
  }

  /// <summary>Привязать прямоугольник к существующим элементам или гриду</summary>
  /// <param name="rect">Исходный прямоугольник</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns></returns>
  public RectangleF SnapRectangle(RectangleF rect, VisualNode excludeNode)
  {
    if ((double) rect.X < 0.0)
      rect.X = 0.0f;
    double right = (double) rect.Right;
    SizeF size = this.page.Size;
    double width = (double) size.Width;
    if (right > width)
    {
      ref RectangleF local = ref rect;
      size = this.page.Size;
      double num = (double) size.Width - (double) rect.Width;
      local.X = (float) num;
    }
    if ((double) rect.Y < 0.0)
      rect.Y = 0.0f;
    double bottom = (double) rect.Bottom;
    size = this.page.Size;
    double height = (double) size.Height;
    if (bottom > height)
    {
      ref RectangleF local = ref rect;
      size = this.page.Size;
      double num = (double) size.Height - (double) rect.Height;
      local.Y = (float) num;
    }
    PointF location = rect.Location;
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint1 = this.GetNearestSnapPoint(location, excludeNode);
    PointF pointF1 = new PointF(rect.Right, rect.Y);
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint2 = this.GetNearestSnapPoint(pointF1, excludeNode);
    PointF pointF2 = new PointF(rect.Right, rect.Bottom);
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint3 = this.GetNearestSnapPoint(pointF2, excludeNode);
    PointF pointF3 = new PointF(rect.X, rect.Bottom);
    Intermech.Interfaces.Document.SnapPoint nearestSnapPoint4 = this.GetNearestSnapPoint(pointF3, excludeNode);
    float num1 = 0.0f;
    PointF point = location;
    PointF pointF4 = location;
    bool flag1 = true;
    Intermech.Interfaces.Document.SnapPoint snapPoint = (Intermech.Interfaces.Document.SnapPoint) null;
    if (nearestSnapPoint1 != null)
    {
      num1 = UnitsConverter.LineLength(location, nearestSnapPoint1.Point);
      point = location;
      snapPoint = nearestSnapPoint1;
      flag1 = snapPoint.PointType != 0;
    }
    if (nearestSnapPoint2 != null)
    {
      float num2 = UnitsConverter.LineLength(pointF1, nearestSnapPoint2.Point);
      bool flag2 = nearestSnapPoint2.PointType != 0;
      if (snapPoint == null || flag1 && !flag2 || flag1 == flag2 && (double) num1 > (double) num2)
      {
        num1 = num2;
        point = pointF1;
        snapPoint = nearestSnapPoint2;
        flag1 = snapPoint.PointType != 0;
      }
    }
    if (nearestSnapPoint3 != null)
    {
      float num3 = UnitsConverter.LineLength(pointF2, nearestSnapPoint3.Point);
      bool flag3 = nearestSnapPoint3.PointType != 0;
      if (snapPoint == null || flag1 && !flag3 || flag1 == flag3 && (double) num1 > (double) num3)
      {
        num1 = num3;
        point = pointF2;
        snapPoint = nearestSnapPoint3;
        flag1 = snapPoint.PointType != 0;
      }
    }
    if (nearestSnapPoint4 != null)
    {
      float num4 = UnitsConverter.LineLength(pointF3, nearestSnapPoint4.Point);
      bool flag4 = nearestSnapPoint4.PointType != 0;
      if (snapPoint == null || flag1 && !flag4 || flag1 == flag4 && (double) num1 > (double) num4)
      {
        point = pointF3;
        snapPoint = nearestSnapPoint4;
        bool flag5 = snapPoint.PointType != 0;
      }
    }
    if (snapPoint != null)
    {
      switch (snapPoint.PointType)
      {
        case SnapPointType.Node:
        case SnapPointType.LineXY:
          pointF4 = snapPoint.Point;
          break;
        case SnapPointType.LineX:
          pointF4.X = snapPoint.Point.X;
          pointF4.Y = this.SnapToGrid(point).Y;
          break;
        case SnapPointType.LineY:
          pointF4.X = this.SnapToGrid(point).X;
          pointF4.Y = snapPoint.Point.Y;
          break;
      }
    }
    else
      pointF4 = this.SnapToGrid(point);
    rect.Location = new PointF(rect.X + (pointF4.X - point.X), rect.Y + (pointF4.Y - point.Y));
    return UnitsConverter.RoundPectangle(rect, 5);
  }

  /// <summary>Прижать точку к сетке</summary>
  /// <param name="point">Точка</param>
  /// <returns>Ближайшая точка в сетке</returns>
  public PointF SnapToGrid(PointF point)
  {
    if (this.DocumentControl != null && this.Page != null)
    {
      point = this.ConvertInternalToUser(point);
      point = this.DocumentControl.SnapToGrid(point);
      point = this.ConvertUserToInternal(point);
      point = new PointF((float) Math.Round((double) point.X, 5), (float) Math.Round((double) point.Y, 5));
    }
    return point;
  }

  /// <summary>Прижать точку к сетке</summary>
  /// <param name="point">Точка</param>
  /// <returns>Ближайшая точка в сетке</returns>
  public PointF SnapToGrid(PointF point, Matrix m)
  {
    if (this.DocumentControl != null && this.Page != null)
    {
      point = this.ConvertInternalToUser(point, m);
      point = this.DocumentControl.SnapToGrid(point);
      point = this.ConvertUserToInternal(point, m);
      point = new PointF((float) Math.Round((double) point.X, 5), (float) Math.Round((double) point.Y, 5));
    }
    return point;
  }

  /// <summary>Прижать прямоугольник к сетке</summary>
  /// <param name="rectangle">Прямоугольник</param>
  /// <returns>Ближайший прямоугольник в сетке</returns>
  public RectangleF SnapToGrid(RectangleF rectangle)
  {
    if (this.DocumentControl != null && this.Page != null)
    {
      rectangle = this.ConvertInternalToUser(rectangle);
      rectangle = this.DocumentControl.SnapToGrid(rectangle);
      rectangle = this.ConvertUserToInternal(rectangle);
    }
    return rectangle;
  }

  /// <summary>Прижать прямоугольник к сетке</summary>
  /// <param name="rectangle">Прямоугольник</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns>Ближайший прямоугольник в сетке</returns>
  public RectangleF SnapPoint(RectangleF rectangle, VisualNode excludeNode)
  {
    if (this.DocumentControl != null && this.Page != null)
    {
      PointF pointF1 = this.SnapPoint(rectangle.Location, excludeNode);
      PointF pointF2 = this.SnapPoint(new PointF(rectangle.Right, rectangle.Bottom), excludeNode);
      rectangle = RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
    }
    return rectangle;
  }

  /// <summary>Прижать точку в пикселях к сетке в мировых координатах</summary>
  /// <param name="point">Точка в пикселях</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns>Точка в пикселях ближайшая к точке в мировых координатах</returns>
  public Point SnapPixelToWorldGrid(Point point, VisualNode excludeNode)
  {
    return this.ConvertWorldToPixel(this.SnapPoint(this.ConvertPixelToWorld(point), excludeNode));
  }

  /// <summary>Прижать прямоугольник в пикселях к сетке в мировых координатах</summary>
  /// <param name="rectangle">Прямоугольник в пикселях</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  /// <returns>Прямоугольник в пикселях ближайший к точке в мировых координатах</returns>
  public Rectangle SnapPixelToWorldGrid(Rectangle rectangle, VisualNode excludeNode)
  {
    if (this.Page != null)
      rectangle = this.ConvertWorldToPixel(this.SnapPoint(this.ConvertPixelToWorld(rectangle), excludeNode));
    return rectangle;
  }

  /// <summary>Матрица преобразования координат</summary>
  [Browsable(false)]
  public virtual MatrixWrapper TransformMatrix
  {
    [DebuggerStepThrough] get => this.transformMatrix;
    set => this.transformMatrix = value;
  }
}
