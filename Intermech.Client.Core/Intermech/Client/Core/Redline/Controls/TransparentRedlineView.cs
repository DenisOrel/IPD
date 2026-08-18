
// Type: Intermech.Client.Core.Redline.Controls.TransparentRedlineView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using Intermech.Redline;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.Redline.Controls;

/// <summary>
/// Прозрачный контрол замечаний Красного карандаша для отображения поверх контрола документа
/// </summary>
public class TransparentRedlineView : RedlineView
{
  private const int WM_NCHITTEST = 132;
  private const int HTTRANSPARENT = -1;
  private Control _backControl;
  private bool _updatingBackground;
  private Bitmap backBitmap;

  /// <summary>Событие изменения свойства Scale</summary>
  public event ZoomRequestedHandler ZoomRequested;

  public bool TrackToolActions { get; set; }

  /// <summary>
  /// Контрол документа для размещение позади данного контрола
  /// </summary>
  public Control BackControl
  {
    get => this._backControl;
    set
    {
      if (this._backControl != null)
        this._backControl = (Control) null;
      if (value == null)
        return;
      this._backControl = value;
    }
  }

  /// <summary>Конструктор</summary>
  public TransparentRedlineView()
  {
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.BackColor = Color.Transparent;
    this.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
    this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
    this.UseBuffer = false;
  }

  /// <summary>
  /// Хак для обеспечения прозрачность событий мыши и клавиатуры
  /// </summary>
  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.ExStyle |= 32 /*0x20*/;
      return createParams;
    }
  }

  protected override void PaintPaperColor(Graphics g, RectangleF clipRect)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    base.PaintPaperColor(g, (RectangleF) clientRectangle);
  }

  /// <summary>Основная отрисовка элементов Redline</summary>
  /// <param name="evt">Аргумент для передачи контекста</param>
  protected override void onPaintCanvas(PaintEventArgs evt)
  {
    if (this.mySuppressPaint > 0)
      return;
    this.myPaintEventArgs = evt;
    Graphics graphics1 = evt.Graphics;
    this.myGraphics = graphics1;
    graphics1.PageUnit = GraphicsUnit.Pixel;
    Rectangle clipRectangle = evt.ClipRectangle;
    if (clipRectangle.Width <= 0 || clipRectangle.Height <= 0)
      return;
    Rectangle clientRectangle = this.ClientRectangle;
    if (this.backBitmap != null)
    {
      this.myBuffer?.Dispose();
      this.myBuffer = new Bitmap((Image) this.backBitmap, clientRectangle.Width + 1, clientRectangle.Height + 1);
    }
    else if (this.myBuffer == null || this.myBuffer.Width < clientRectangle.Width || this.myBuffer.Height < clientRectangle.Height)
    {
      this.myBuffer?.Dispose();
      this.myBuffer = new Bitmap(clientRectangle.Width + 1, clientRectangle.Height + 1, graphics1);
    }
    GraphicsState gstate = graphics1.Save();
    if (this.UseBuffer)
    {
      using (Graphics graphics2 = Graphics.FromImage((Image) this.myBuffer))
      {
        graphics2.PageUnit = GraphicsUnit.Pixel;
        this.PaintBorder(graphics2, clientRectangle, clipRectangle);
        Rectangle rectangle = Rectangle.Intersect(clipRectangle, this.DisplayRectangle);
        graphics2.IntersectClip(rectangle);
        RectangleF doc = this.ConvertViewToDoc(rectangle);
        this.SetTransformPageUnit(graphics2);
        this.PaintView(graphics2, doc);
        graphics1.DrawImage((Image) this.myBuffer, this.myPaintEventArgs.ClipRectangle, this.myPaintEventArgs.ClipRectangle, GraphicsUnit.Pixel);
      }
    }
    else
    {
      Graphics graphics3 = graphics1;
      graphics3.PageUnit = GraphicsUnit.Pixel;
      this.PaintBorder(graphics3, clientRectangle, clipRectangle);
      Rectangle rectangle = Rectangle.Intersect(clipRectangle, this.DisplayRectangle);
      graphics3.IntersectClip(rectangle);
      RectangleF doc = this.ConvertViewToDoc(rectangle);
      this.SetTransformPageUnit(graphics3);
      this.PaintView(graphics3, doc);
    }
    graphics1.Restore(gstate);
    this.myGraphics = graphics1;
  }

  public override IList MouseMoveTools
  {
    get
    {
      if (this._mouseMoveTools == null)
        this._mouseMoveTools = new ArrayList()
        {
          (object) new MapToolDragging((MapView) this)
        };
      return (IList) this._mouseMoveTools;
    }
  }

  /// <summary>Крутанули колесом мыши при активном RedlineTool</summary>
  public override void DoWheel(MapInputEventArgs evt)
  {
    if (evt.Delta == 0 || !evt.Control)
      return;
    ZoomRequestedHandler zoomRequested = this.ZoomRequested;
    if (zoomRequested == null)
      return;
    zoomRequested((object) this, evt);
  }

  /// <summary>
  /// Переопределенный общий обработчик событий изменения Redline-модели
  /// </summary>
  protected override void OnDocumentChanged(object sender, MapChangedEventArgs e)
  {
    MapObject mapObject = e.MapObject;
    if (e.IsBeforeChanging)
    {
      if (e.Hint == 901 && mapObject != null)
      {
        RectangleF bounds = mapObject.Bounds;
        Rectangle view = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds, (MapView) this));
        view.Inflate(2, 2);
        this.BackControl?.Invalidate(view);
      }
      this.RaiseDocumentChanged(sender, e);
    }
    else
    {
      int hint = e.Hint;
      if (hint <= 220)
      {
        switch (hint - 100)
        {
          case 0:
label_9:
            this.Selection.AddAllSelectionHandles();
            this.UpdateView();
            this.RaiseDocumentChanged(sender, e);
            break;
          case 1:
            this.BeginUpdate();
            this.RaiseDocumentChanged(sender, e);
            break;
          case 2:
            this.EndUpdate();
            this.RaiseDocumentChanged(sender, e);
            break;
          case 3:
            this.Update();
            this.RaiseDocumentChanged(sender, e);
            break;
          default:
            switch (hint - 202)
            {
              case 0:
                this.UpdateScrollBars();
                this.RaiseDocumentChanged(sender, e);
                return;
              case 1:
                this.UpdateScrollBars();
                this.RaiseDocumentChanged(sender, e);
                return;
              case 2:
                this.RaiseDocumentChanged(sender, e);
                return;
              case 3:
                goto label_9;
              default:
                if (hint != 220)
                {
                  this.RaiseDocumentChanged(sender, e);
                  return;
                }
                goto label_9;
            }
        }
      }
      else if (hint <= 904)
      {
        switch (hint - 801)
        {
          case 0:
            MapLayer doclayer = (MapLayer) e.Object;
            MapLayer oldValue1 = (MapLayer) e.OldValue;
            if (e.SubHint != 1)
            {
              this.Layers.InsertDocumentLayerBefore(oldValue1, doclayer);
              this.Selection.AddAllSelectionHandles();
              this.UpdateView();
              this.RaiseDocumentChanged(sender, e);
              break;
            }
            this.Layers.InsertDocumentLayerAfter(oldValue1, doclayer);
            this.Selection.AddAllSelectionHandles();
            this.UpdateView();
            this.RaiseDocumentChanged(sender, e);
            break;
          case 1:
            this.Layers.Remove((MapLayer) e.Object);
            this.Selection.AddAllSelectionHandles();
            this.UpdateView();
            this.RaiseDocumentChanged(sender, e);
            break;
          case 2:
            MapLayer moving = (MapLayer) e.Object;
            MapLayer oldValue2 = (MapLayer) e.OldValue;
            try
            {
              if (e.SubHint == 1)
                this.Layers.MoveAfter(oldValue2, moving);
              else
                this.Layers.MoveBefore(oldValue2, moving);
            }
            catch (ArgumentException ex)
            {
            }
            this.Selection.AddAllSelectionHandles();
            this.UpdateView();
            this.RaiseDocumentChanged(sender, e);
            break;
          default:
            if (hint != 901)
            {
              if ((uint) (hint - 902) <= 2U)
              {
                if (e.Hint == 903)
                  this.removeFromSelection(mapObject);
                RectangleF bounds = mapObject.Bounds;
                Rectangle view = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds, (MapView) this));
                view.Inflate(2, 2);
                this.BackControl?.Invalidate(view);
                this.RaiseDocumentChanged(sender, e);
                break;
              }
              this.RaiseDocumentChanged(sender, e);
              break;
            }
            RectangleF bounds1 = mapObject.Bounds;
            Rectangle view1 = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds1, (MapView) this));
            view1.Inflate(2, 2);
            if (e.SubHint != 1001)
            {
              if (e.SubHint == 1003)
                this.updateSelectionHandles(mapObject);
              else if (e.SubHint == 1052)
                this.removeFromSelection(e.OldValue as MapObject);
              this.BackControl?.Invalidate(view1);
              this.RaiseDocumentChanged(sender, e);
              break;
            }
            MapSelection selection = this.Selection;
            if (selection.GetHandleCount(mapObject) > 0)
            {
              if (!mapObject.CanView())
              {
                mapObject.RemoveSelectionHandles(selection);
              }
              else
              {
                IMapHandle anExistingHandle = selection.GetAnExistingHandle(mapObject);
                mapObject.AddSelectionHandles(selection, anExistingHandle.SelectedObject);
              }
            }
            RectangleF oldRect = e.OldRect;
            Rectangle view2 = this.ConvertDocToView(mapObject.ExpandPaintBounds(oldRect, (MapView) this));
            view2.Inflate(2, 2);
            this.BackControl?.Invalidate(view2);
            this.BackControl?.Invalidate(view1);
            this.RaiseDocumentChanged(sender, e);
            break;
        }
      }
      else if (hint == 910)
      {
        this.Selection.AddAllSelectionHandles();
        this.UpdateView();
        this.RaiseDocumentChanged(sender, e);
      }
      else
        this.RaiseDocumentChanged(sender, e);
    }
  }

  /// <summary>
  /// Стандартная оконная процедура Windows изменена.
  /// Ловим только клики по элементам MapObject, иначе пропускаем дальше.
  /// </summary>
  /// <param name="m">Оконное сообщение.</param>
  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 132 && !this.TrackToolActions)
    {
      IntPtr lparam = m.LParam;
      int x = lparam.ToInt32() & (int) ushort.MaxValue;
      lparam = m.LParam;
      int y = (int) (((long) lparam.ToInt32() & 4294901760L) >> 16 /*0x10*/);
      if (this.PickObject(true, true, new PointF((float) x, (float) y), false) == null)
      {
        m.Result = (IntPtr) -1;
        return;
      }
    }
    base.WndProc(ref m);
  }

  public override void UpdateView()
  {
    this.UpdateBorderWidths();
    this.UpdateScrollBars();
    this.BackControl?.Invalidate();
  }

  public override void RaiseChanged(
    int hint,
    int subhint,
    object x,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
    int num = hint;
    if (num <= 904)
    {
      if ((uint) (num - 801) > 2U)
      {
        switch (num - 901)
        {
          case 0:
          case 3:
            if (!(x is MapObject mapObject1))
              break;
            RectangleF bounds1 = mapObject1.Bounds;
            Rectangle view1 = this.ConvertDocToView(mapObject1.ExpandPaintBounds(bounds1, (MapView) this));
            view1.Inflate(2, 2);
            if (hint == 901 && subhint == 1001)
            {
              oldRect = mapObject1.ExpandPaintBounds(oldRect, (MapView) this);
              Rectangle view2 = this.ConvertDocToView(oldRect);
              view2.Inflate(2, 2);
              if (view1.IntersectsWith(view2))
              {
                this.BackControl?.Invalidate(Rectangle.Union(view1, view2));
                break;
              }
              this.BackControl?.Invalidate(view1);
              this.BackControl?.Invalidate(view2);
              break;
            }
            this.BackControl?.Invalidate(view1);
            break;
          case 1:
            if (!(x is MapObject mapObject2))
              break;
            RectangleF bounds2 = mapObject2.Bounds;
            Rectangle view3 = this.ConvertDocToView(mapObject2.ExpandPaintBounds(bounds2, (MapView) this));
            view3.Inflate(2, 2);
            this.BackControl?.Invalidate(view3);
            this.BackControl?.Update();
            break;
          case 2:
            if (!(x is MapObject mapObject3))
              break;
            RectangleF bounds3 = mapObject3.Bounds;
            Rectangle view4 = this.ConvertDocToView(mapObject3.ExpandPaintBounds(bounds3, (MapView) this));
            view4.Inflate(2, 2);
            this.BackControl?.Invalidate(view4);
            break;
        }
      }
      else
        this.UpdateView();
    }
    else if (num == 910)
      this.UpdateView();
  }

  public void SetZoommingScale(float scale)
  {
    double docScale = (double) this.DocScale;
    float scale1 = this.LimitDocScale(scale * this.PixelsPerMM) / this.PixelsPerMM;
    double num = (double) scale1;
    if (docScale == num)
      return;
    this.ZoomToScale(this.LastInput.DocPoint, scale1);
  }

  public void RaiseOnPaintWithArgs(PaintEventArgs e) => this.ManualOnPaint(e);

  protected void ManualOnPaint(PaintEventArgs evt)
  {
    this.onPaintCanvas(evt);
    this.UpdateMapControlBounds();
  }

  protected override void OnPaint(PaintEventArgs evt)
  {
  }

  public override float LimitDocScale(float s) => s;

  public override void ZoomToScale(PointF ptdoc, float scale)
  {
    if ((double) this.DocScale == (double) scale)
      return;
    PointF docPosition = this.DocPosition;
    ptdoc = PointF.Empty;
    Point view = this.ConvertDocToView(ptdoc);
    this.OnViewChanging();
    this.DocScale = scale;
    SizeF doc = this.ConvertViewToDoc(new Size(this.ConvertDocToView(ptdoc)) - new Size(view));
    this.DocPosition = docPosition + doc;
    this.UpdateView();
  }
}
