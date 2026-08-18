// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Page
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model.UI;
using Intermech.Document.Model.UI.Extensions;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Объект страницы документа. Функции: Управление элементами страницы; Управление
/// атрибутами; Рендеринг.</summary>
[Serializable]
public class Page : PageData
{
  [NonSerialized]
  private PageUI pageUI;
  /// <summary>Буферный экземпляр ImRtfEditor для разбивки текста</summary>
  private ImRtfEditor ternPaintBuffer;
  private readonly object syncRoot = new object();

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new Page(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых необходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new Page();

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.needUI = true;
  }

  /// <summary>Конструктор</summary>
  public Page(DocumentTreeNode parent) => this.SetParent(parent, false, false);

  /// <summary>Конструктор</summary>
  public Page()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public Page(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected Page(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [Browsable(false)]
  public PageUI PageUI
  {
    get
    {
      if (this.pageUI == null && this.NeedUI)
        this.pageUI = new PageUI(this);
      return this.pageUI;
    }
    set => this.pageUI = value;
  }

  public override bool NeedUI => base.NeedUI;

  /// <summary>Ссылка на документ владелец</summary>
  [Category("Debug")]
  [Browsable(false)]
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get
    {
      return this.Parent is ImDocument parent ? parent.documentControl : (DocumentControl) null;
    }
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.IsVisibleNow && !this.SuspendedRefreshUIFlag && this.PageControl != null && this.PageUI != null && this.PageControl.VisiblePageElementUIs.Contains((PageElementUI) this.PageUI) && !this.PageControl.LockedUpdateSettings)
      this.PageControl.Refresh();
    base.RefreshUI();
  }

  /// <summary>Обновить некорректную область</summary>
  public override void UpdateInvalidatedRegion()
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.PageControl == null)
      return;
    this.PageControl.UpdateInvalidatedRegion();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public override void InvalidateUI(Rectangle clipRectangle)
  {
    if (this.IsVisibleNow && !this.SuspendedRefreshUIFlag && this.PageControl != null)
      this.PageControl.AddToInvalidateRegion(clipRectangle);
    base.InvalidateUI(clipRectangle);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="region">Область которую нужно обновить</param>
  public override void InvalidateUI(Region region)
  {
    if (this.IsVisibleNow && !this.SuspendedRefreshUIFlag && this.PageControl != null)
      this.PageControl.AddToInvalidateRegion(region);
    base.InvalidateUI(region);
  }

  /// <summary>Буферный экземпляр ImRtfEditor для разбивки текста</summary>
  [Browsable(false)]
  public ImRtfEditor TernPaintBuffer
  {
    get
    {
      return this.OwnerDocument is ImDocument ? (this.OwnerDocument as ImDocument).TernPaintBuffer : this.ternPaintBuffer;
    }
    set
    {
      lock (this.syncRoot)
      {
        if (this.OwnerDocument is ImDocument ownerDocument)
        {
          this.ternPaintBuffer = (ImRtfEditor) null;
          ownerDocument.TernPaintBuffer = value;
        }
        else
          this.ternPaintBuffer = value;
      }
    }
  }

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  public static extern IntPtr GetDC(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    try
    {
      this.WaitForLoad(6000);
      this.WaitForLayout(6000);
      if (this.IsLocked)
        return;
      GraphicsUnit pageUnit = context.Graphics.PageUnit;
      RectangleF clipBounds = context.Graphics.ClipBounds;
      Matrix transform = context.Graphics.Transform;
      ImGraphics graphics = context.Graphics;
      ImRtfEditor imRtfEditor = (context is DrawContextWithUI drawContextWithUi ? drawContextWithUi.TernPrintBuffer : (ImRtfEditor) null) ?? drawContextWithUi?.TernPaintBuffer;
      RectangleF rectangleF1 = new RectangleF(this.Location, this.Size);
      RectangleF rectangleF2 = new RectangleF(new PointF(0.0f, 0.0f), rectangleF1.Size);
      RectangleF rectangleF3 = new RectangleF(0.0f, 0.0f, rectangleF1.Width * 10f, rectangleF1.Height * 10f);
      Rectangle rect = Rectangle.Empty;
      IntPtr zero = IntPtr.Zero;
      this.PaintBuffer = (Image) null;
      try
      {
        context.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        if (context.Layer == -1 && context.IsPaint && this.PageUI != null)
        {
          rect = this.PageUI.Bounds;
          context.Graphics.PageUnit = GraphicsUnit.Pixel;
          context.Graphics.Transform = new Matrix();
          context.Graphics.SetClip(rect);
          context.Graphics.FillRectangle(Brushes.White, rect);
          Pen pen = (Pen) null;
          if (this.PrintBounds)
            pen = this.OwnerDocument == null ? new Pen(Color.DarkGray, PageElementNode.DefaultLineWidth) : this.OwnerDocument.DefaultPageBorderLine.GetPen();
          if (pen != null)
          {
            context.Graphics.DrawRectangle(pen, (float) rect.X, (float) rect.Y, (float) (rect.Width - 1), (float) (rect.Height - 1));
            pen.Dispose();
          }
          else
            context.Graphics.DrawRectangle(RectangleElement.InvisibleLinePen, (float) rect.X, (float) rect.Y, (float) (rect.Width - 1), (float) (rect.Height - 1));
        }
        context.Graphics.PageUnit = GraphicsUnit.Pixel;
        context.Graphics.Transform = new Matrix();
        if (context.IsPaint && this.PageUI != null)
          context.Graphics.SetClip(this.PageUI.Bounds);
        context.Graphics.Transform = transform;
        context.Graphics.PageUnit = pageUnit;
        base.Draw(context);
        context.Graphics = graphics;
      }
      finally
      {
        if (zero != IntPtr.Zero)
          Page.ReleaseDC(IntPtr.Zero, zero);
      }
      if (this.PaintBuffer != null)
      {
        context.Graphics.PageUnit = GraphicsUnit.Pixel;
        context.Graphics.Transform = new Matrix();
        if (rect != Rectangle.Empty)
          context.Graphics.SetClip(rect);
        context.Graphics.DrawImage(this.PaintBuffer, Point.Empty);
      }
      context.Graphics.Transform = transform;
      context.Graphics.SetClip(clipBounds);
      context.Graphics.PageUnit = pageUnit;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Interfaces.Document_168");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Положение страницы</summary>
  [TypeConverter(typeof (PointFConverter))]
  [CustomDisplayName("Attribute.Document.Model_62")]
  [CustomDescription("Attribute.Document.Model_63")]
  [CustomCategory("Attribute.Document.Model_64")]
  public override PointF Location
  {
    [DebuggerStepThrough] get => base.Location;
    set
    {
      if (!(this.Location != value))
        return;
      base.Location = value;
      if (this.PageUI == null)
        return;
      this.PageUI.UpdateTransformMatrix();
    }
  }

  /// <summary>Назначить новое значение без обновления интерфейса</summary>
  /// <param name="value">Новое значение</param>
  public override void AssignLocation(PointF value)
  {
    if (!(this.Location != value))
      return;
    base.AssignLocation(value);
    if (this.PageUI == null)
      return;
    this.PageUI.UpdateTransformMatrix();
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
      if (this.PageUI == null || child == null)
        return;
      VisualNode visualNode = child as VisualNode;
      if (visualNode != null & createUI)
        visualNode.CreateUI();
      if (child is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      {
        elementWithInterface.PageUI.Parent = (PageElementUI) this.PageUI;
        elementWithInterface.PageUI.TransparentForMouse = false;
        this.RefreshUI();
      }
      base.AddChildUI(child, createUI);
    }
  }

  /// <summary>Свойство дублирует UIControl с приведением типа</summary>
  [Browsable(false)]
  public virtual PageControl PageControl
  {
    get => this.DocumentControl != null ? this.DocumentControl.PageControl : (PageControl) null;
  }

  /// <summary>Создать соответствующий элемент управления</summary>
  public override void CreateUI()
  {
    if (this.IsVirtualNode || !this.NeedUI || this.PageUI != null)
      return;
    this.PageUI = new PageUI(this);
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public override void DestroyUI() => this.PageUI = (PageUI) null;

  public override void UpdateLayout(bool updateUI) => base.UpdateLayout(updateUI);

  /// <summary>Обновить дочернюю геометрию</summary>
  /// <param name="force"></param>
  public void UpdateChildUIGeometry(bool force, bool refreshUI)
  {
    if (((this.PageControl == null ? 0 : (this.PageControl.VisiblePageElementUIs.Contains((PageElementUI) this.PageUI) ? 1 : 0)) | (force ? 1 : 0)) == 0)
      return;
    base.UpdateUIGeometry(refreshUI);
  }

  /// <summary>Обновить экранные координаты</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    if (this.PageControl == null && this.NeedUI)
    {
      DocumentControl documentControl = this.DocumentControl;
      if (documentControl != null && documentControl.InvokeRequired)
      {
        documentControl.InvokeUpdateUIGeometry((PageData) this, refreshUI);
        return;
      }
    }
    if (this.PageControl == null)
      return;
    if (this.PageControl.InvokeRequired)
    {
      this.PageControl.InvokeUpdateUIGeometry(refreshUI);
    }
    else
    {
      if (this.needUpdateUIGeometry)
      {
        if (this.parent is ImDocument && !(this.parent as ImDocument).IsDistributing)
          this.PageControl.UpdateSettings();
        this.OnUIGeometryChanged(new UIGeometryChanged_EventArgs());
      }
      if (this.PageControl.VisiblePageElementUIs == null || !this.PageControl.VisiblePageElementUIs.Contains((PageElementUI) this.PageUI))
        return;
      this.SuspendRefreshUI();
      base.UpdateUIGeometry(false);
      this.ResumeRefreshUI(refreshUI);
    }
  }

  /// <summary>Создать метафайл с изображением страницы</summary>
  /// <param name="fileName">Имя метафайла</param>
  /// <returns>Метафайл</returns>
  public override void CreatePageMetafile(string fileName)
  {
    Metafile metafile = (Metafile) null;
    IntPtr dc = Page.GetDC(IntPtr.Zero);
    RectangleF rectangleF = new RectangleF(this.Location, this.Size);
    try
    {
      if (File.Exists(fileName))
        File.Delete(fileName);
      metafile = new Metafile(dc, EmfType.EmfPlusDual);
      using (Graphics g = Graphics.FromImage((Image) metafile))
      {
        g.PageUnit = GraphicsUnit.Millimeter;
        RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, this.Size));
        g.SetClip(rect);
        if (!this.PrintBounds)
          g.DrawRectangle(new Pen(Color.White, 0.0f), 0.0f, 0.0f, this.Size.Width, this.Size.Height);
        this.Draw(new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, this.Size), 0, false, false, new MatrixWrapper(g.Transform))
        {
          IsMetafile = true
        });
      }
      IntPtr henhmetafile = metafile.GetHenhmetafile();
      uint enhMetaFileBits1 = ContainerElement.GetEnhMetaFileBits(henhmetafile, 0U, (byte[]) null);
      byte[] numArray = new byte[(int) enhMetaFileBits1];
      int enhMetaFileBits2 = (int) ContainerElement.GetEnhMetaFileBits(henhmetafile, enhMetaFileBits1, numArray);
      ContainerElement.DeleteEnhMetaFile(henhmetafile);
      metafile.Dispose();
      metafile = (Metafile) null;
      MemoryStream memoryStream = new MemoryStream(numArray);
      memoryStream.Position = 0L;
      FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
      memoryStream.WriteTo((Stream) fileStream);
      fileStream.Close();
      memoryStream.Close();
    }
    finally
    {
      metafile?.Dispose();
      Page.ReleaseDC(IntPtr.Zero, dc);
    }
  }

  public Metafile CreatePageMetafile() => this.CreatePageMetafile(PointF.Empty);

  /// <summary>Создать метафайл с изображением страницы</summary>
  /// <returns>Метафайл</returns>
  public Metafile CreatePageMetafile(PointF dpi)
  {
    this.WaitForLoad(12000);
    this.WaitForLayout(12000);
    if (this.Parent is ImDocument parent)
      parent.TernPrintBuffer = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
    Metafile metafile = (Metafile) null;
    Metafile pageMetafile = (Metafile) null;
    IntPtr dc = Page.GetDC(IntPtr.Zero);
    RectangleF rectangleF = new RectangleF(this.Location, this.Size);
    Rectangle empty = Rectangle.Empty;
    try
    {
      metafile = new Metafile(dc, EmfType.EmfPlusDual);
      using (Graphics g = Graphics.FromImage((Image) metafile))
      {
        g.PageUnit = GraphicsUnit.Millimeter;
        RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, this.Size));
        g.SetClip(rect);
        if (!this.PrintBounds)
        {
          using (Pen pen1 = new Pen(Color.White, 0.0f))
          {
            Graphics graphics = g;
            Pen pen2 = pen1;
            SizeF size = this.Size;
            double width = (double) size.Width;
            size = this.Size;
            double height = (double) size.Height;
            graphics.DrawRectangle(pen2, 0.0f, 0.0f, (float) width, (float) height);
          }
        }
        this.Draw(new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, this.Size), 0, false, false, new MatrixWrapper(g.Transform))
        {
          IsMetafile = true
        });
      }
      pageMetafile = (Metafile) metafile.Clone();
    }
    finally
    {
      Page.ReleaseDC(IntPtr.Zero, dc);
      metafile?.Dispose();
    }
    if (parent != null)
      parent.TernPrintBuffer = (ImRtfEditor) null;
    return pageMetafile;
  }

  /// <summary>Текущая система координат</summary>
  public override PageCoorSystem UserCoorSystem => ImDocumentEditorConfig.Instance.CoorSystem;

  /// <inheritdoc cref="T:Intermech.Interfaces.Document.DocumentTreeNode" />
  public override void SynchronizeNodePositionWithUI(
    DocumentTreeNode node,
    int oldIndex,
    int newIndex)
  {
    this.UpdatePageElementChildPosition(node, oldIndex, newIndex);
  }

  /// <summary>Идентификатор шаблона для следующей страницы</summary>
  [Editor(typeof (NextPageTemplateIdEditor), typeof (UITypeEditor))]
  public override string NextPageTemplateId
  {
    [DebuggerStepThrough] get => base.NextPageTemplateId;
    set => base.NextPageTemplateId = value;
  }

  /// <summary>Идентификатор шаблона для следующей страницы</summary>
  [Editor(typeof (LastPageTemplateIdEditor), typeof (UITypeEditor))]
  public override string LastPageTemplateId
  {
    [DebuggerStepThrough] get => base.LastPageTemplateId;
    set => base.LastPageTemplateId = value;
  }

  /// <summary>Иконка для кнопки статическая версия</summary>
  public static Image Icon
  {
    get
    {
      return PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.Page.png");
    }
  }

  /// <summary>Генерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    if (e.Child is IPageElementWithInterface child && child.PageUI != null && this.PageUI != null)
      this.PageUI.PageElementUIs.Remove(child.PageUI);
    base.OnChildNodeRemoved(e);
  }

  /// <summary>Запрет на изменение пользователем структуры узла</summary>
  public override bool ReadOnlyStructure
  {
    [DebuggerStepThrough] get
    {
      return this.OwnerDocument is ImDocument ownerDocument && ownerDocument.DocumentControl != null && ownerDocument.DocumentControl.ReadOnly;
    }
  }

  public override PointF ConvertInternalToUser(PointF point)
  {
    return this.PageUI != null ? this.PageUI.ConvertInternalToUser(point) : point;
  }

  public override RectangleF ConvertInternalToUser(RectangleF rectangle)
  {
    return this.PageUI != null ? this.PageUI.ConvertInternalToUser(rectangle) : rectangle;
  }

  public override SizeF ConvertInternalToUser(SizeF size)
  {
    return this.PageUI != null ? this.PageUI.ConvertInternalToUser(size) : size;
  }

  public override PointF ConvertUserToInternal(PointF point)
  {
    return this.PageUI != null ? this.PageUI.ConvertUserToInternal(point) : point;
  }

  public override RectangleF ConvertUserToInternal(RectangleF rectangle)
  {
    return this.PageUI != null ? this.PageUI.ConvertUserToInternal(rectangle) : rectangle;
  }

  public override SizeF ConvertUserToInternal(SizeF size)
  {
    return this.PageUI != null ? this.PageUI.ConvertUserToInternal(size) : size;
  }

  /// <summary>Вернуть разрешение дисплея</summary>
  /// <returns>Разрешение дисплея</returns>
  public override PointF GetDisplayDpi()
  {
    return this.DocumentControl != null && this.DocumentControl.PageControl != null ? this.DocumentControl.PageControl.DisplayDpi : base.GetDisplayDpi();
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="point">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public PointF ConvertPixelToWorld(Point point)
  {
    Matrix matrix;
    if (this.PageUI != null)
    {
      matrix = this.PageUI.TransformMatrix.Matrix.Clone();
      matrix.Invert();
    }
    else
      matrix = new Matrix();
    try
    {
      return MatrixWrapper.TransformPoint(matrix.Elements, this.ConvertPixelToMm(point));
    }
    finally
    {
      matrix.Dispose();
    }
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="rectangle">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public RectangleF ConvertPixelToWorld(Rectangle rectangle)
  {
    Matrix matrix;
    if (this.PageUI != null)
    {
      matrix = this.TransformMatrix.Matrix.Clone();
      matrix.Invert();
    }
    else
      matrix = new Matrix();
    try
    {
      float[] elements = matrix.Elements;
      PointF pointF1 = MatrixWrapper.TransformPoint(elements, this.ConvertPixelToMm(rectangle.Location));
      PointF pointF2 = MatrixWrapper.TransformPoint(elements, this.ConvertPixelToMm(new Point(rectangle.Right, rectangle.Bottom)));
      return RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
    }
    finally
    {
      matrix.Dispose();
    }
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="points">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public PointF[] ConvertPixelFToWorld(PointF[] points)
  {
    PointF[] pointFArray = (PointF[]) points.Clone();
    Matrix matrix;
    if (this.PageUI != null)
    {
      matrix = this.PageUI.TransformMatrix.Matrix.Clone();
      matrix.Invert();
    }
    else
      matrix = new Matrix();
    try
    {
      float[] elements = matrix.Elements;
      for (int index = 0; index < pointFArray.Length; ++index)
        pointFArray[index] = MatrixWrapper.TransformPoint(elements, this.ConvertPixelToMm(Point.Round(pointFArray[index])));
      return pointFArray;
    }
    finally
    {
      matrix.Dispose();
    }
  }

  /// <summary>Преобразовать мировую координату X в пиксели</summary>
  /// <param name="x">x</param>
  /// <returns>Координата x в пикселях</returns>
  public int ConvertWorldXToPixel(float x)
  {
    return Convert.ToInt32(UnitsConverter.LineLength(this.ConvertWorldToPixelF(new PointF(0.0f, 0.0f)), this.ConvertWorldToPixelF(new PointF(x, 0.0f))));
  }

  /// <summary>Преобразовать мировую координату Y в пиксели</summary>
  /// <param name="y">y</param>
  /// <returns>Координата Y в пикселях</returns>
  public int ConvertWorldYToPixel(float y)
  {
    return Convert.ToInt32(UnitsConverter.LineLength(this.ConvertWorldToPixelF(new PointF(0.0f, 0.0f)), this.ConvertWorldToPixelF(new PointF(0.0f, y))));
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="point">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public override Point ConvertWorldToPixel(PointF point)
  {
    return this.PageUI != null ? this.ConvertMmToPixel(this.PageUI.TransformMatrix.TransformPoint(point)) : this.ConvertMmToPixel(point);
  }

  internal MatrixWrapper TransformMatrix
  {
    [DebuggerStepThrough] get
    {
      return this.PageUI != null ? this.PageUI.TransformMatrix : new MatrixWrapper();
    }
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public override Rectangle ConvertWorldToPixel(RectangleF rectangle)
  {
    PointF pointF1 = this.TransformMatrix.TransformPoint(rectangle.Location);
    PointF pointF2 = this.TransformMatrix.TransformPoint(new PointF(rectangle.Right, rectangle.Bottom));
    return this.ConvertMmToPixel(RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y));
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="points">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public PointF[] ConvertWorldToPixelF(PointF[] points)
  {
    PointF[] pixelF = (PointF[]) points.Clone();
    for (int index = 0; index < pixelF.Length; ++index)
    {
      pixelF[index] = this.TransformMatrix.TransformPoint(pixelF[index]);
      pixelF[index] = this.ConvertMmToPixelF(pixelF[index]);
    }
    return pixelF;
  }

  /// <summary>Преобразовать мировые координаты в пиксели</summary>
  /// <param name="point">Точка в мировых координатах</param>
  /// <returns>Точка в пикселях</returns>
  public PointF ConvertWorldToPixelF(PointF point)
  {
    point = this.TransformMatrix.TransformPoint(point);
    return this.ConvertMmToPixelF(point);
  }

  /// <summary>координаты на контроле в координаты на контроле страницы</summary>
  /// <param name="control">Контрол</param>
  /// <param name="location">Положение на контроле</param>
  /// <returns>Координаты на контроле страницы</returns>
  public Point ControlPointToPage(Control control, Point location)
  {
    if (control != null && this.DocumentControl.PageControl != null)
      location = this.DocumentControl.PageControl.PointToClient(control.PointToScreen(location));
    return location;
  }

  /// <summary>координаты на контроле страницы в координаты на заданном контроле</summary>
  /// <param name="control">Контрол</param>
  /// <param name="location">Положение на контроле страницы</param>
  /// <returns>Положение на заданном контроле</returns>
  public Point PagePointToControl(Control control, Point location)
  {
    if (control != null && this.DocumentControl.PageControl != null)
      location = control.PointToClient(this.DocumentControl.PageControl.PointToScreen(location));
    return location;
  }
}
