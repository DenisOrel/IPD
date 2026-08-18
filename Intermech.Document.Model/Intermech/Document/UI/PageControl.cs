// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Компонент для работы со страницей</summary>
public class PageControl : UserControl
{
  /// <summary>Разрешение экрана по умолчанию, на случай, если нет PageControl</summary>
  internal static PointF DefaultDisplayDpi;
  private int lockUpdate;
  private int lockMouse;
  private bool onePage;
  private bool isPainting;
  private int lockUpdateSettings;
  private Rectangle layoutBounds;
  private RectangleElement _lastSelectedElem;
  private RectangleElement firstSelectedElem;
  private bool leftMouseDownHanldedByChild;
  public IntPtr DialogHandle;
  public bool suspendDrawMovingPreview;
  /// <summary>Required designer variable</summary>
  private System.ComponentModel.Container components;
  private Point pos;
  private bool isPasting;
  private Point startPastingPoint;
  private DocumentTreeNode pasteDest;
  private DocumentTreeNode[] pastingNodes;
  private Bitmap[] bmpHors;
  private Bitmap[] bmpVerts;
  private float?[] offsets;
  public Region regionCur;
  public Region regionPrev;
  private bool typeHorizontal = true;
  private Point popupBarPos;
  private Rectangle dragLine = Rectangle.Empty;
  private bool drawLine = true;
  private bool isRectangleSelecting;
  private bool isMovingSelected;
  private Region invalidateRegion;
  private Cursor pageCursor;
  internal PageElementUI focusedElement;
  internal PageElementUI elementAtCursor;
  internal PageControlUI pageControlUI;
  private PageElementUICollection _visiblePageElementUIs;
  private ImDocument doc;
  private PointF displayDpi = new PointF(96f, 96f);
  private ContextMenuBarItem contextMenuBarItem;
  private MenuBar menuBar;
  /// <summary>Позиция мыши при нажатии левой клавиши</summary>
  private Point leftMouseDownPos = Point.Empty;
  private Page HostMovingPage;
  /// <summary>Предыдущая позиция мыши при перемещении</summary>
  private Point prevMousePos = Point.Empty;
  private Rectangle selectionRectangle = Rectangle.Empty;
  private bool isMouseDownValidated;
  private bool needDrawPopupBar;
  /// <summary>Происходит выделение объектов при нажатии кнопки мыши, в этом случае блокируется перемещение с Ctrl</summary>
  protected bool isMouseDownSelecting;
  private Timer timerMoving;
  private Timer timerDialog;

  /// <summary>Коэффициент масштабирования</summary>
  [CustomDisplayName("Attribute.Document.Model_31")]
  [CustomDescription("Attribute.Document.Model_32")]
  [CustomCategory("Attribute.Document.Model_33")]
  [ReadOnly(true)]
  [Browsable(false)]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float PageScale
  {
    [DebuggerStepThrough] get
    {
      DocumentControl documentControl = this.DocumentControl;
      return documentControl != null ? documentControl.DocumentScale : 1f;
    }
  }

  public event PaintEventHandler Painted;

  /// <summary>Разрешение экрана для расчёта координат элементов управления</summary>
  public PointF DisplayDpi
  {
    [DebuggerStepThrough] get => this.displayDpi;
    set
    {
      try
      {
        if (!(this.displayDpi != value))
          return;
        this.displayDpi = value;
        PageControl.DefaultDisplayDpi = value;
        if (this.Document != null && this.Document.SuspendedRefreshUIFlag)
          return;
        this.UpdateSettings();
        this.Refresh();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  protected override void OnEnter(EventArgs e) => base.OnEnter(e);

  /// <summary>Обновить изображение контрола</summary>
  public override void Refresh()
  {
    try
    {
      if (this.LockedUpdate || this.Document == null || this.DocumentControl != null && this.Document.SuspendedRefreshUIFlag)
        return;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new MethodInvoker(((Control) this).Refresh));
      else
        base.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  [Category("Debug")]
  public virtual bool LockedUpdate
  {
    [DebuggerStepThrough] get => this.lockUpdate > 0;
  }

  public void LockUpdate() => ++this.lockUpdate;

  public void UnLockUpdate()
  {
    if (this.lockUpdate <= 0)
      return;
    --this.lockUpdate;
  }

  [Browsable(false)]
  public virtual bool LockedMouse
  {
    [DebuggerStepThrough] get => this.lockMouse > 0;
  }

  public void LockMouse() => ++this.lockMouse;

  public void UnLockMouse()
  {
    if (this.lockMouse <= 0)
      return;
    --this.lockMouse;
  }

  /// <summary>Обновить объявленные недопустимыми области изображения</summary>
  public virtual void UpdateInvalidatedRegion()
  {
    if (this.LockedUpdate || this.Document != null && (this.Document.SuspendedRefreshUIFlag || this.Parent == null || !this.Visible))
      return;
    if (this.InvokeRequired)
      this.Invoke((Delegate) new MethodInvoker(this.UpdateInvalidatedRegion));
    else if (this.invalidateRegion != null)
    {
      this.Update();
      if (this.invalidateRegion != null)
        this.invalidateRegion.Dispose();
      this.invalidateRegion = (Region) null;
    }
    else
      base.Refresh();
  }

  /// <summary>Добавить область к недопустимым (тербующим обновления)</summary>
  /// <param name="clipRectangle">Прямоугольная область</param>
  public virtual void AddToInvalidateRegion(Rectangle clipRectangle)
  {
    clipRectangle.Location = new Point(clipRectangle.X - 1, clipRectangle.Y - 1);
    clipRectangle.Size = new Size(clipRectangle.Width + 3, clipRectangle.Height + 3);
    if (this.invalidateRegion == null)
      this.invalidateRegion = new Region(clipRectangle);
    else
      this.invalidateRegion.Union(clipRectangle);
  }

  /// <summary>Добавить область к недопустимым (требующим обновления)</summary>
  /// <param name="region">Область</param>
  public virtual void AddToInvalidateRegion(Region region)
  {
    if (this.invalidateRegion == null)
      this.invalidateRegion = region;
    else
      this.invalidateRegion.Union(region);
  }

  public void SetDocument(ImDocument document, bool updateUI, bool refreshUI)
  {
    try
    {
      if (this.doc == document)
        return;
      if (this.doc != null)
      {
        this.doc.DistributePageFinished -= new DistributePageFinished_EventHandler(this.doc_DistributePageFinished);
        this.doc.BackgroundThreadsFinished -= new BackgroundThreadsFinished_EventHandler(this.doc_BackgroundThreadsFinished);
      }
      this.doc = document;
      if (this.doc != null && this.doc.IsFormulaLib)
        this.OnePage = true;
      if (this.doc != null & updateUI)
        this.doc.UpdateUIGeometry(refreshUI);
      if (this.doc == null)
        return;
      this.doc.DistributePageFinished += new DistributePageFinished_EventHandler(this.doc_DistributePageFinished);
      this.doc.BackgroundThreadsFinished += new BackgroundThreadsFinished_EventHandler(this.doc_BackgroundThreadsFinished);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Страница документа</summary>
  public ImDocument Document
  {
    [DebuggerStepThrough] get => this.doc;
    set => this.SetDocument(value, true, true);
  }

  private void doc_BackgroundThreadsFinished(object sender, BackgroundThreadsFinishedArgs e)
  {
    try
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new MethodInvoker(this.AfterEndDistributeDocument));
      else
        this.AfterEndDistributeDocument();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void doc_DistributePageFinished(object sender, DistributePageFinishedArgs e)
  {
  }

  /// <summary>Отображать одну страницу</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool OnePage
  {
    [DebuggerStepThrough] get => this.onePage;
    set => this.onePage = value;
  }

  /// <summary>Коллекция интерфейсов пользователя элементов страницы</summary>
  public PageElementUICollection VisiblePageElementUIs
  {
    [DebuggerStepThrough] get => this._visiblePageElementUIs;
  }

  /// <summary>Ссылка на документ владелец</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get
    {
      Control parent = this.Parent;
      while (true)
      {
        switch (parent)
        {
          case null:
          case DocumentControl _:
            goto label_3;
          default:
            parent = parent.Parent;
            continue;
        }
      }
label_3:
      return parent as DocumentControl;
    }
  }

  public void PreparePopupBar()
  {
    this.NeedDrawPopupBar = true;
    if (this.DocumentControl != null)
    {
      this.DocumentControl.HorzRuler.DrawRuler = false;
      this.DocumentControl.VertRuler.DrawRuler = false;
    }
    Point popupBarPosition = this.PopupBarPosition;
    this.pos = new Point();
    if (this.regionCur != null)
      this.regionPrev = this.regionCur.Clone();
    this.regionCur = new Region();
    this.regionCur.MakeEmpty();
    if (this.regionPrev == null)
    {
      this.regionPrev = new Region();
      this.regionPrev.MakeEmpty();
    }
    int num1 = 3;
    if (this.typeHorizontal)
    {
      if (this.DrawLine)
        this.regionCur.Union(new Rectangle(popupBarPosition.X - 1, 0, 3, this.Height));
      this.pos = popupBarPosition;
      int num2 = 0;
      if (this.offsets[0].HasValue)
        ++num2;
      if (this.offsets[1].HasValue)
        ++num2;
      if (this.offsets[2].HasValue)
        ++num2;
      if (this.offsets[3].HasValue)
        ++num2;
      Size size = new Size(57, 24);
      int num3 = size.Width * num2 - num2 + 1;
      if (this.DrawLine)
      {
        this.pos.X -= num3 / 2;
      }
      else
      {
        if (num2 == 4)
          this.pos.X -= num3 / 2;
        if (num2 == 3)
          this.pos.X -= size.Width * 2;
      }
      Rectangle controlViewRectangle = this.DocumentControl.GetPageControlViewRectangle();
      if (this.pos.X < controlViewRectangle.X)
        this.pos.X = Math.Abs(controlViewRectangle.X);
      if (this.pos.X + (size.Width - 1) * num2 > controlViewRectangle.Right - num1)
        this.pos.X = controlViewRectangle.Right - ((size.Width - 1) * num2 + num1);
      if (this.pos.Y < controlViewRectangle.Y)
        this.pos.Y = controlViewRectangle.Y;
      if (this.pos.Y + size.Height > controlViewRectangle.Bottom - num1)
        this.pos.Y = controlViewRectangle.Bottom - size.Height - num1;
      this.regionCur.Union(new Rectangle(this.pos.X - 1, this.pos.Y, size.Width * num2, size.Height + 3));
    }
    if (this.typeHorizontal)
      return;
    if (this.DrawLine)
      this.regionCur.Union(new Rectangle(0, popupBarPosition.Y - 1, this.Width, 3));
    this.pos = popupBarPosition;
    int num4 = 0;
    if (this.offsets[0].HasValue)
      ++num4;
    if (this.offsets[1].HasValue)
      ++num4;
    if (this.offsets[2].HasValue)
      ++num4;
    if (this.offsets[3].HasValue)
      ++num4;
    Size size1 = new Size(57, 24);
    int num5 = size1.Height * num4 - num4 + 1;
    if (this.DrawLine)
    {
      this.pos.Y -= num5 / 2;
    }
    else
    {
      if (num4 == 4)
        this.pos.Y -= num5 / 2;
      if (num4 == 3)
        this.pos.Y = this.pos.Y - size1.Height * 2 + 2;
    }
    Rectangle controlViewRectangle1 = this.DocumentControl.GetPageControlViewRectangle();
    if (this.pos.Y < controlViewRectangle1.Y)
      this.pos.Y = Math.Abs(controlViewRectangle1.Y);
    if (this.pos.Y + (size1.Height - 1) * num4 > controlViewRectangle1.Bottom - num1)
      this.pos.Y = controlViewRectangle1.Bottom - ((size1.Height - 1) * num4 + num1);
    if (this.pos.X < controlViewRectangle1.X)
      this.pos.X = controlViewRectangle1.X;
    if (this.pos.X + size1.Width > controlViewRectangle1.Right - num1)
      this.pos.X = controlViewRectangle1.Right - size1.Width - num1;
    this.regionCur.Union(new Rectangle(this.pos.X - 1, this.pos.Y, size1.Width + 2, size1.Height * num4 + 3));
  }

  /// <summary>Отрисовка всплывающего окна</summary>
  /// <param name="g"></param>
  private void DrawPopupBar(Graphics g)
  {
    Point popupBarPosition = this.PopupBarPosition;
    if (!this.needDrawPopupBar)
      return;
    this.needDrawPopupBar = false;
    if (this.offsets != null && (this.offsets[0].HasValue || this.offsets[1].HasValue || this.offsets[2].HasValue || this.offsets[3].HasValue) || this.drawLine)
    {
      if (this.typeHorizontal)
      {
        if (this.drawLine)
        {
          Point start = new Point(popupBarPosition.X, 0);
          Point end = new Point(popupBarPosition.X, this.Height);
          RubberBand.DrawXorLine(g, start, end, Color.White);
        }
        int num1 = 0;
        Font font = new Font("Tahoma", 11f, GraphicsUnit.Pixel);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Far;
        int[] numArray1 = new int[4];
        int[] numArray2 = new int[4];
        numArray1[0] = -6;
        numArray1[1] = -10;
        numArray1[2] = -10;
        numArray1[3] = -14;
        numArray2[0] = 8;
        numArray2[1] = 2;
        numArray2[2] = 2;
        numArray2[3] = 8;
        Size size = new Size(57, 24);
        for (int index = 0; index < 4; ++index)
        {
          float?[] offsets = this.offsets;
          if ((offsets != null ? (offsets[index].HasValue ? 1 : 0) : 0) != 0)
          {
            float d = this.offsets[index].Value;
            int num2 = (int) Math.Truncate((double) d);
            float num3 = (float) Math.Round((double) d - (double) num2, 2);
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            SizeF sizeF1 = g.MeasureString(decimalSeparator + "00", font);
            SizeF sizeF2 = g.MeasureString(decimalSeparator + num3.ToString(".00"), font);
            Rectangle layoutRectangle = new Rectangle(this.pos.X + num1, this.pos.Y + numArray2[index], size.Width - (int) ((double) sizeF1.Width - (double) sizeF2.Width) + numArray1[index], 35);
            g.DrawImage((Image) this.bmpHors[index], this.pos.X + num1, this.pos.Y);
            using (SolidBrush solidBrush = new SolidBrush(Color.Black))
              g.DrawString(d.ToString("0.00"), font, (Brush) solidBrush, (RectangleF) layoutRectangle, format);
            num1 += 56;
          }
        }
      }
      else
      {
        if (this.drawLine)
        {
          Point start = new Point(0, popupBarPosition.Y);
          Point end = new Point(this.Width, popupBarPosition.Y);
          RubberBand.DrawXorLine(g, start, end, Color.White);
        }
        int num4 = 0;
        Font font = new Font("Tahoma", 11f, GraphicsUnit.Pixel);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Far;
        int[] numArray3 = new int[4];
        int[] numArray4 = new int[4];
        numArray3[0] = -5;
        numArray3[1] = -5;
        numArray3[2] = -5;
        numArray3[3] = -5;
        numArray4[0] = 2;
        numArray4[1] = 5;
        numArray4[2] = 5;
        numArray4[3] = 2;
        Size size = new Size(57, 24);
        for (int index = 0; index < 4; ++index)
        {
          float?[] offsets = this.offsets;
          if ((offsets != null ? (offsets[index].HasValue ? 1 : 0) : 0) != 0)
          {
            float d = this.offsets[index].Value;
            int num5 = (int) Math.Truncate((double) d);
            float num6 = (float) Math.Round((double) d - (double) num5, 2);
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            SizeF sizeF3 = g.MeasureString(decimalSeparator + "00", font);
            SizeF sizeF4 = g.MeasureString(decimalSeparator + num6.ToString(".00"), font);
            Rectangle layoutRectangle = new Rectangle(this.pos.X, this.pos.Y + num4 + numArray4[index], size.Width - (int) ((double) sizeF3.Width - (double) sizeF4.Width) + numArray3[index], 35);
            g.DrawImage((Image) this.bmpVerts[index], this.pos.X, this.pos.Y + num4);
            using (SolidBrush solidBrush = new SolidBrush(Color.Black))
              g.DrawString(d.ToString("0.00"), font, (Brush) solidBrush, (RectangleF) layoutRectangle, format);
            num4 += size.Height - 1;
          }
        }
      }
    }
    if (this.DocumentControl == null)
      return;
    this.DocumentControl.HorzRuler.DrawRuler = true;
    this.DocumentControl.VertRuler.DrawRuler = true;
  }

  /// <summary>Перекрытый метод отрисовки OnPaint</summary>
  protected override void OnPaint(PaintEventArgs e)
  {
    if (this.isPainting)
      return;
    this.isPainting = true;
    try
    {
      if (this.Document == null || this.DocumentControl == null || this.DocumentControl.LockForClosing)
        return;
      Rectangle clipRectangle = e.ClipRectangle;
      PointF displayDpi = this.DisplayDpi;
      if ((double) displayDpi.X == (double) e.Graphics.DpiX)
      {
        displayDpi = this.DisplayDpi;
        if ((double) displayDpi.Y == (double) e.Graphics.DpiY)
          goto label_7;
      }
      this.DisplayDpi = new PointF(e.Graphics.DpiX, e.Graphics.DpiY);
label_7:
      Rectangle rectangle = new Rectangle(e.ClipRectangle.X - 1, e.ClipRectangle.Y - 1, e.ClipRectangle.Width + 2, e.ClipRectangle.Height + 2);
      Matrix transform1 = e.Graphics.Transform;
      try
      {
        foreach (Page page in this.Document.OfType<Page>())
        {
          if (this.VisiblePageElementUIs.Contains((PageElementUI) page.PageUI))
          {
            Matrix matrix = page.PageUI.TransformMatrix.Matrix;
            e.Graphics.Transform = matrix;
            RectangleF world = page.PageUI.ConvertPixelToWorld(rectangle);
            DrawContextWithUI context = new DrawContextWithUI(this.Document, this, new ImGraphics(e.Graphics), true, world, -1, false, this.ShowInvisibleLines, page.PageUI.TransformMatrix);
            if (!this.IsPasting && !this.IsElementCreating)
            {
              this.Document.TernPaintBuffer = RtfInSiteEditorWrapper.CreateTernPaintBuffer();
              context.TernPaintBuffer = this.Document.TernPaintBuffer;
            }
            page.Draw((DrawContext) context);
            context.Layer = 0;
            page.Draw((DrawContext) context);
          }
        }
      }
      finally
      {
        e.Graphics.Transform = transform1;
        e.Graphics.SetClip(clipRectangle);
      }
      for (int index = 0; index < this._visiblePageElementUIs.Count; ++index)
        this._visiblePageElementUIs[index].OnPaint(e);
      if (this.ActiveElement is IPageElementWithInterface activeElement && activeElement.PageUI != null)
      {
        GraphicsUnit pageUnit = e.Graphics.PageUnit;
        RectangleF clipBounds = e.Graphics.ClipBounds;
        Matrix transform2 = e.Graphics.Transform;
        if (this.ActiveElement is PageElementNode && (this.ActiveElement as PageElementNode).Page != null)
        {
          e.Graphics.Transform = new Matrix();
          e.Graphics.PageUnit = GraphicsUnit.Pixel;
          e.Graphics.SetClip(((this.ActiveElement as PageElementNode).Page as Page).PageUI.Bounds);
        }
        activeElement.PageUI.OnPaint(e);
        if (this.ActiveElement is PageElementNode && (this.ActiveElement as PageElementNode).Page != null)
        {
          e.Graphics.Transform = transform2;
          e.Graphics.PageUnit = pageUnit;
          e.Graphics.SetClip(clipBounds);
        }
      }
      if (this.IsRectangleSelecting)
        this.DrawSelectionRectangle(e.Graphics, true);
      if (this.DocumentControl == null)
        return;
      if (this.IsElementCreating && this.SelectedElementCreator != null)
        this.SelectedElementCreator.OnPaint(e);
      base.OnPaint(e);
      this.DrawPopupBar(e.Graphics);
      this.DrawDragLine(e.Graphics);
      PaintEventHandler painted = this.Painted;
      if (painted == null)
        return;
      painted((object) this, e);
    }
    catch (Exception ex)
    {
      try
      {
        ImDocumentData.ShowException(ex, LocalizationHolder.rm.GetString("Document.Model_617"));
      }
      catch
      {
      }
    }
    finally
    {
      this.isPainting = false;
    }
  }

  [Category("Debug")]
  public virtual bool LockedUpdateSettings
  {
    [DebuggerStepThrough] get => this.lockUpdateSettings > 0;
  }

  public void LockUpdateSettings() => ++this.lockUpdateSettings;

  public void UnLockUpdateSettings()
  {
    if (this.lockUpdateSettings <= 0)
      return;
    --this.lockUpdateSettings;
  }

  public void AfterEndDistributeDocument()
  {
    this.UpdateSettings();
    this.DocumentControl.ScrollSelectionToView(false, false);
  }

  /// <summary>Обновить параметры интерфейса страницы, зависящие от страницы (Page)</summary>
  public void UpdateSettings()
  {
    try
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke((Delegate) new MethodInvoker(this.UpdateSettings));
      }
      else
      {
        if (this.Document == null || this.LockedUpdateSettings)
          return;
        this.LockUpdateSettings();
        this.DocumentControl?.VertRuler.SuspendRefresh();
        this.DocumentControl?.HorzRuler.SuspendRefresh();
        this.Document.SuspendUpdateUIGeometry();
        try
        {
          this.UpdateLayout(false);
          this.DocumentControl?.UpdateScrollBars(true);
        }
        finally
        {
          if (this.Document != null && this.DocumentControl != null)
          {
            this.Document.ResumeUpdateUIGeometry(false, false);
            if (this.DocumentControl.ActiveElement != null)
            {
              Rectangle empty = Rectangle.Empty;
              this.Document.SuspendRefreshUI();
              try
              {
                for (int index = 0; index < this.VisiblePageElementUIs.Count; ++index)
                  this.VisiblePageElementUIs[index].Page.UpdateUIGeometry(false);
                (this.DocumentControl.ActiveElement as VisualNode).UpdateUIGeometry(false);
              }
              finally
              {
                this.Document.ResumeRefreshUI(true);
              }
            }
            else if (Size.Empty.IsEmpty)
            {
              for (int index = 0; index < this.VisiblePageElementUIs.Count; ++index)
                this.VisiblePageElementUIs[index].Page.UpdateUIGeometry(false);
            }
            this.UnLockUpdateSettings();
            this.DocumentControl.VertRuler.ResumeRefresh(true);
            this.DocumentControl.HorzRuler.ResumeRefresh(true);
            this.DocumentControl.Refresh();
          }
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  internal void SetScrollBarValue(ScrollBar bar, int value)
  {
    try
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new PageControl.SetScrollBarValueInvoker(this.SetScrollBarValue), (object) bar, (object) value);
      }
      else
      {
        int maximum = bar.Maximum;
        if (bar is VScrollBar)
          maximum -= this.Height;
        if (bar is HScrollBar)
          maximum -= this.Width;
        if (value < bar.Minimum)
          bar.Value = bar.Minimum;
        else if (value > maximum)
          bar.Value = maximum;
        else
          bar.Value = value;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Переместить изображение</summary>
  /// <param name="offset">Смещение</param>
  internal void MoveLayout(Size offset)
  {
    try
    {
      if (offset.IsEmpty)
        return;
      this.LockUpdateSettings();
      this.DocumentControl.VertRuler.SuspendRefresh();
      this.DocumentControl.HorzRuler.SuspendRefresh();
      try
      {
        List<Page> pageList = new List<Page>();
        if (this.OnePage)
        {
          if (this.DocumentControl.ActivePage != null)
            pageList.Add(this.DocumentControl.ActivePage);
        }
        else
        {
          foreach (Page page in (ImDocumentData) this.Document)
          {
            if (page != null && page.PageUI != null && page.PageUI.UIUpdated)
              pageList.Add(page);
          }
        }
        this.VisiblePageElementUIs.Clear();
        int margin = this.DocumentControl.margin;
        SizeF sizeF = SizeF.Empty;
        foreach (Page page1 in pageList)
        {
          if (page1 is Page page2)
          {
            if (sizeF == SizeF.Empty)
              sizeF = page2.PageUI.ConvertPixelToWorld(new Rectangle(new Point(0, 0), offset)).Size;
            page2.AssignLocation(new PointF(page2.Location.X - sizeF.Width, page2.Location.Y - sizeF.Height));
            Rectangle pixel = page2.PageUI.ConvertWorldToPixel(new RectangleF(PointF.Empty, page2.Size));
            page2.PageUI.Bounds = pixel;
            if (page2.PageUI.Bounds.IntersectsWith(this.Bounds))
            {
              this.VisiblePageElementUIs.Add((PageElementUI) page2.PageUI);
              page2.UpdateUIGeometry(false);
            }
          }
        }
        if (this.DocumentControl.ActiveElement != null && this.DocumentControl.ActiveElement is VisualNode)
          (this.DocumentControl.ActiveElement as VisualNode).UpdateUIGeometry(false);
        this.layoutBounds.Offset(-offset.Width, -offset.Height);
        if (this.DocumentControl.VScrollBar.Value != -this.layoutBounds.Y || this.DocumentControl.HScrollBar.Value != -this.layoutBounds.X)
        {
          this.DocumentControl.SuspendScrollBars();
          try
          {
            this.SetScrollBarValue((ScrollBar) this.DocumentControl.VScrollBar, -this.layoutBounds.Y);
            this.SetScrollBarValue((ScrollBar) this.DocumentControl.HScrollBar, -this.layoutBounds.X);
          }
          finally
          {
            this.DocumentControl.ResumeScrollBars(true);
          }
        }
        offset = new Size(-offset.Width, -offset.Height);
        IPageElementWithInterface activeElement = this.DocumentControl.ActiveElement as IPageElementWithInterface;
        PageElementUI pageElementUi = (PageElementUI) null;
        if (activeElement != null)
          pageElementUi = activeElement.PageUI;
        if (pageElementUi != null && pageElementUi.IsMoving)
          pageElementUi.StartPoint = new Point(pageElementUi.StartPoint.X + offset.Width, pageElementUi.StartPoint.Y + offset.Height);
        if (this.IsMovingSelected)
          this.leftMouseDownPos = new Point(this.leftMouseDownPos.X + offset.Width, this.leftMouseDownPos.Y + offset.Height);
        if (this.IsPasting)
          this.startPastingPoint = new Point(this.startPastingPoint.X + offset.Width, this.startPastingPoint.Y + offset.Height);
        if (this.IsElementCreating)
        {
          if (this.SelectedElementCreator is RectanglePageElementCreator)
          {
            Point firstPoint = (this.SelectedElementCreator as RectanglePageElementCreator).FirstPoint;
            (this.SelectedElementCreator as RectanglePageElementCreator).FirstPoint = new Point(firstPoint.X + offset.Width, firstPoint.Y + offset.Height);
          }
          if (this.SelectedElementCreator is PolylineCreator)
          {
            Point prevPoint = (this.SelectedElementCreator as PolylineCreator).PrevPoint;
            (this.SelectedElementCreator as PolylineCreator).PrevPoint = new Point(prevPoint.X + offset.Width, prevPoint.Y + offset.Height);
          }
        }
        if (!this.IsTableCellsSelecting && !this.IsTableRowsSelecting || !(this.elementAtCursor is TableCellUI))
          return;
        PageElementUI elementAtCursor = (PageElementUI) (this.elementAtCursor as TableCellUI);
        elementAtCursor.leftMouseDownPos = new Point(elementAtCursor.leftMouseDownPos.X + offset.Width, elementAtCursor.leftMouseDownPos.Y + offset.Height);
      }
      finally
      {
        this.UnLockUpdateSettings();
        this.DocumentControl.VertRuler.ResumeRefresh(true);
        this.DocumentControl.HorzRuler.ResumeRefresh(true);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public Rectangle LayoutBounds
  {
    get => this.layoutBounds;
    set => this.layoutBounds = value;
  }

  private void UpdateLayout(bool updateUIs)
  {
    try
    {
      this.LockUpdateSettings();
      try
      {
        this.layoutBounds.Size = new Size(0, 0);
        this.layoutBounds.Location = new Point(0, 0);
        this.VisiblePageElementUIs.Clear();
        int margin = this.DocumentControl.margin;
        float num1 = -1f;
        List<Page> pageList = new List<Page>();
        if (this.OnePage)
        {
          if (this.DocumentControl.ActivePage != null && this.DocumentControl.ActivePage.PageControl != null)
            pageList.Add(this.DocumentControl.ActivePage);
        }
        else
        {
          foreach (Page page in (ImDocumentData) this.Document)
          {
            if (page != null && page.PageControl != null)
              pageList.Add(page);
          }
        }
        foreach (Page page1 in pageList)
        {
          if (page1 is Page page2 && page2.PageUI != null && page2.PageControl != null)
          {
            page2.PageUI.UpdateTransformMatrix();
            if ((double) num1 == -1.0)
              num1 = page2.PageUI.ConvertPixelToWorld(new Rectangle(0, 0, margin, margin)).Width;
            Rectangle pixel = page2.PageUI.ConvertWorldToPixel(new RectangleF(PointF.Empty, page2.Size));
            page2.PageUI.Bounds = pixel;
          }
        }
        int num2 = 0;
        float num3 = num1 + (float) this.layoutBounds.Y;
        float num4 = num1;
        int num5;
        for (int index1 = 0; index1 < pageList.Count; index1 = num5 + 1)
        {
          int num6 = margin;
          Rectangle rectangle = pageList[index1].PageUI.Bounds;
          int width1 = rectangle.Width;
          int num7 = num6 + width1;
          int index2 = index1 + 1;
          rectangle = pageList[index1].PageUI.Bounds;
          int val1_1 = rectangle.Height;
          SizeF size = pageList[index1].Size;
          float num8 = size.Height;
          for (; num7 < this.Width && index2 < pageList.Count; ++index2)
          {
            Rectangle bounds = pageList[index2].PageUI.Bounds;
            int num9 = num7 + bounds.Width + margin;
            if (num9 < this.Width)
            {
              num7 = num9;
              val1_1 = Math.Max(val1_1, bounds.Height);
              double val1_2 = (double) num8;
              size = pageList[index2].Size;
              double height = (double) size.Height;
              num8 = Math.Max((float) val1_2, (float) height);
            }
            else
              break;
          }
          int num10 = (this.Width - num7) / 2;
          if (num10 < margin)
            num10 = margin;
          float x = pageList[index1].PageUI.ConvertPixelToWorld(new Rectangle(0, 0, num10, num10)).Width;
          if (index2 == pageList.Count && num2 != 0 && (double) x * 2.0 > (double) num4)
            x = num4;
          num4 = x;
          for (int index3 = index1; index3 < index2; ++index3)
          {
            float y = num3;
            size = pageList[index3].Size;
            if ((double) size.Height < (double) num8)
            {
              double num11 = (double) y;
              double num12 = (double) num8;
              size = pageList[index3].Size;
              double height = (double) size.Height;
              double num13 = (num12 - height) / 2.0;
              y = (float) (num11 + num13);
            }
            pageList[index3].SuspendUpdateUIGeometry();
            pageList[index3].AssignLocation(new PointF(x, y));
            Rectangle pixel = pageList[index3].PageUI.ConvertWorldToPixel(new RectangleF(PointF.Empty, pageList[index3].Size));
            pageList[index3].PageUI.Bounds = pixel;
            pageList[index3].PageUI.UIUpdated = true;
            pageList[index3].ResumeUpdateUIGeometry(false, false);
            int right1 = pixel.Right;
            rectangle = this.LayoutBounds;
            int right2 = rectangle.Right;
            if (right1 > right2)
            {
              ref Rectangle local = ref this.layoutBounds;
              int right3 = pixel.Right;
              rectangle = this.LayoutBounds;
              int left = rectangle.Left;
              int num14 = right3 - left;
              local.Width = num14;
            }
            int bottom1 = pixel.Bottom;
            rectangle = this.LayoutBounds;
            int bottom2 = rectangle.Bottom;
            if (bottom1 > bottom2)
            {
              ref Rectangle local = ref this.layoutBounds;
              int bottom3 = pixel.Bottom;
              rectangle = this.LayoutBounds;
              int top = rectangle.Top;
              int num15 = bottom3 - top;
              local.Height = num15;
            }
            double num16 = (double) x;
            size = pageList[index3].Size;
            double width2 = (double) size.Width;
            x = (float) (num16 + width2) + num1;
            rectangle = pageList[index3].PageUI.Bounds;
            if (rectangle.IntersectsWith(this.Bounds))
              this.VisiblePageElementUIs.Add((PageElementUI) pageList[index3].PageUI);
          }
          num3 = num3 + num1 + num8;
          num5 = index2 - 1;
          ++num2;
        }
      }
      finally
      {
        this.UnLockUpdateSettings();
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnLocationChanged(EventArgs e)
  {
    try
    {
      base.OnLocationChanged(e);
      this.UpdateSettings();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnLostFocus(EventArgs e)
  {
    try
    {
      base.OnLostFocus(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnGotFocus(EventArgs e)
  {
    try
    {
      base.OnGotFocus(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnSizeChanged(EventArgs e)
  {
    try
    {
      base.OnSizeChanged(e);
      this.UpdateSettings();
      this.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnBackColorChanged(EventArgs e)
  {
    try
    {
      base.OnBackColorChanged(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Видимые границы страницы (не выходящие за пределы окна)</summary>
  public virtual Rectangle VisibleBounds
  {
    [DebuggerStepThrough] get
    {
      Rectangle visibleBounds = this.DisplayRectangle;
      try
      {
        if (this.DocumentControl != null)
        {
          visibleBounds = this.DocumentControl.VisibleWorkArea;
          visibleBounds.Location = this.DocumentControl.PointToScreen(visibleBounds.Location);
          visibleBounds.Location = this.PointToClient(visibleBounds.Location);
          int x1 = visibleBounds.X;
          Rectangle clientRectangle = this.ClientRectangle;
          int x2 = clientRectangle.X;
          if (x1 < x2)
          {
            clientRectangle = this.ClientRectangle;
            int x3 = clientRectangle.X;
            int y = visibleBounds.Y;
            int width1 = visibleBounds.Width;
            clientRectangle = this.ClientRectangle;
            int num = clientRectangle.X - visibleBounds.X;
            int width2 = width1 - num;
            int height = visibleBounds.Height;
            visibleBounds = new Rectangle(x3, y, width2, height);
          }
          int y1 = visibleBounds.Y;
          clientRectangle = this.ClientRectangle;
          int y2 = clientRectangle.Y;
          if (y1 < y2)
          {
            int x4 = visibleBounds.X;
            clientRectangle = this.ClientRectangle;
            int y3 = clientRectangle.Y;
            int width = visibleBounds.Width;
            int height1 = visibleBounds.Height;
            clientRectangle = this.ClientRectangle;
            int num = clientRectangle.Y - visibleBounds.Y;
            int height2 = height1 - num;
            visibleBounds = new Rectangle(x4, y3, width, height2);
          }
          int right1 = visibleBounds.Right;
          clientRectangle = this.ClientRectangle;
          int right2 = clientRectangle.Right;
          if (right1 > right2)
          {
            int x5 = visibleBounds.X;
            int x6 = visibleBounds.X;
            int width3 = visibleBounds.Width;
            int right3 = visibleBounds.Right;
            clientRectangle = this.ClientRectangle;
            int right4 = clientRectangle.Right;
            int num = right3 - right4;
            int width4 = width3 - num;
            int height = visibleBounds.Height;
            visibleBounds = new Rectangle(x5, x6, width4, height);
          }
          int bottom1 = visibleBounds.Bottom;
          clientRectangle = this.ClientRectangle;
          int bottom2 = clientRectangle.Bottom;
          if (bottom1 > bottom2)
          {
            int x7 = visibleBounds.X;
            int x8 = visibleBounds.X;
            int width = visibleBounds.Width;
            int height3 = visibleBounds.Height;
            int bottom3 = visibleBounds.Bottom;
            clientRectangle = this.ClientRectangle;
            int bottom4 = clientRectangle.Bottom;
            int num = bottom3 - bottom4;
            int height4 = height3 - num;
            visibleBounds = new Rectangle(x7, x8, width, height4);
          }
        }
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
      return visibleBounds;
    }
  }

  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="uiList">Список PageElementUI под заданной точкой</param>
  public void GetPageElementUIAtPoint(Point point, List<PageElementUI> uiList)
  {
    for (int index = this._visiblePageElementUIs.Count - 1; index >= 0; --index)
      this._visiblePageElementUIs[index].GetPageElementUIAtPoint(point, uiList, true);
  }

  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <returns>PageElementUI под заданной точкой</returns>
  public PageElementUI GetPageElementUIAtPoint(Point point, bool ignoreGrabHandle)
  {
    PageElementUI elementUiAtPoint1 = (PageElementUI) null;
    int layer = -1;
    for (int index = this._visiblePageElementUIs.Count - 1; index >= 0; --index)
    {
      PageElementUI elementUiAtPoint2 = this._visiblePageElementUIs[index].GetPageElementUIAtPoint(point, ref layer, true, ignoreGrabHandle);
      if (elementUiAtPoint2 != null)
        elementUiAtPoint1 = elementUiAtPoint2;
    }
    return elementUiAtPoint1;
  }

  /// <summary>Получить элементы страницы в заданном прямоугольнике</summary>
  /// <param name="rect">Прямоугольник</param>
  /// <param name="nodes">Возвращает элементы</param>
  /// <param name="containsOnly">Выбирать только те элементы, которые полностью попадают в прямоугольник</param>
  public void GetPageElementsInRectangle(
    Rectangle rect,
    List<DocumentTreeNode> nodes,
    bool containsOnly)
  {
    for (int index = 0; index < this._visiblePageElementUIs.Count; ++index)
      this._visiblePageElementUIs[index].GetPageElementsInRectangle(rect, (IList<DocumentTreeNode>) nodes, containsOnly);
  }

  public bool IsNodeVisible(PageElementNode node)
  {
    if (!(node is IPageElementWithInterface) || !node.IsVisibleNow || !(node.Page is Page page) || page.PageUI == null || !this.VisiblePageElementUIs.Contains((PageElementUI) page.PageUI))
      return false;
    PageElementUI pageUi = (node as IPageElementWithInterface).PageUI;
    return pageUi != null && pageUi.Bounds.IntersectsWith(this.Bounds);
  }

  /// <summary>Активный элемент документа</summary>
  public DocumentTreeNode ActiveElement
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null ? this.DocumentControl.ActiveElement : (DocumentTreeNode) null;
    }
  }

  /// <summary>Действует режим выбора положения пользовательской системы координат</summary>
  public bool IsCoorSystemSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && DocumentControl.IsCoorSystemSelecting;
    }
    set
    {
      try
      {
        if (this.DocumentControl == null)
          return;
        DocumentControl.IsCoorSystemSelecting = value;
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Позиция для отрисовки линии при перетаскивании</summary>
  public Rectangle DragLinePosition
  {
    get => this.dragLine;
    set => this.dragLine = value;
  }

  public bool DrawLine
  {
    get => this.drawLine;
    set => this.drawLine = value;
  }

  public bool NeedDrawPopupBar
  {
    get => this.needDrawPopupBar;
    set => this.needDrawPopupBar = value;
  }

  /// <summary>Позиция для отрисовки всплывающего окна</summary>
  public Point PopupBarPosition
  {
    get => this.popupBarPos;
    set => this.popupBarPos = value;
  }

  /// <summary>Является ли всплывающее окно горизонтальным</summary>
  public bool IsPopupBarHorizontal
  {
    get => this.typeHorizontal;
    set => this.typeHorizontal = value;
  }

  public Region RegionForInvalidate
  {
    get
    {
      Region regionForInvalidate;
      if (this.regionCur != null)
      {
        regionForInvalidate = this.regionCur.Clone();
      }
      else
      {
        regionForInvalidate = new Region();
        regionForInvalidate.MakeEmpty();
      }
      if (this.regionPrev != null)
        regionForInvalidate.Union(this.regionPrev);
      return regionForInvalidate;
    }
  }

  /// <summary>Последний выделенный элемент в таблице</summary>
  public RectangleElement LastSelectedElem
  {
    get
    {
      return this._lastSelectedElem != null && this._lastSelectedElem.Page != null ? this._lastSelectedElem : (RectangleElement) null;
    }
    set => this._lastSelectedElem = value;
  }

  /// <summary>Первый выделенный элемент в таблице</summary>
  public RectangleElement FirstSelectedElem
  {
    get => this.firstSelectedElem;
    set => this.firstSelectedElem = value;
  }

  /// <summary>Действует режим выбора элемента</summary>
  public bool IsElementSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl == null || this.DocumentControl.IsElementSelecting;
    }
    set
    {
      try
      {
        if (this.DocumentControl == null)
          return;
        this.DocumentControl.IsElementSelecting = value;
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Режим выделения элементов страницы рамкой</summary>
  public bool IsRectangleSelecting
  {
    [DebuggerStepThrough] get => this.isRectangleSelecting;
    set
    {
      try
      {
        if (this.isRectangleSelecting == value)
          return;
        this.isRectangleSelecting = value;
        if (value)
          this.DrawSelectionRectangle((Graphics) null, PageControl.NormalRectangle(this.prevMousePos, this.leftMouseDownPos), false);
        else
          this.EraseSelectionRectangle((Graphics) null);
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Происходит выделение объектов при нажатии кнопки мыши, в этом случае блокируется перемещение с Ctrl</summary>
  public bool IsMouseDownSelecting
  {
    [DebuggerStepThrough] get => this.isMouseDownSelecting;
    set
    {
      if (this.isMouseDownSelecting == value)
        return;
      this.isMouseDownSelecting = value;
    }
  }

  /// <summary>Режим перемещения элементов страницы</summary>
  public bool IsMovingSelected
  {
    [DebuggerStepThrough] get => this.isMovingSelected;
    set
    {
      if (this.isMovingSelected == value)
        return;
      this.isMovingSelected = value;
    }
  }

  /// <summary>Установить режим выбора строк таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableRowsSelectingMode(bool value, TableElement selectedTable)
  {
    try
    {
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.SetTableRowsSelectingMode(value, selectedTable);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить режим выбора столбцов таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableColumnsSelectingMode(bool value, TableElement selectedTable)
  {
    try
    {
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.SetTableColumnsSelectingMode(value, selectedTable);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить режим выбора ячеек таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableCellsSelectingMode(bool value, TableElement selectedTable)
  {
    try
    {
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.SetTableCellsSelectingMode(value, selectedTable);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Режим выбора строк таблицы</summary>
  internal bool IsTableRowsSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.IsTableRowsSelecting;
    }
  }

  /// <summary>Режим выбора столбцов таблицы</summary>
  internal bool IsTableColumnsSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.IsTableColumnsSelecting;
    }
  }

  /// <summary>Режим выбора ячеек таблицы</summary>
  internal bool IsTableCellsSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.IsTableCellsSelecting;
    }
  }

  /// <summary>Таблица в которой выбираются ячейки</summary>
  internal TableElement SelectedTable
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null ? this.DocumentControl.SelectedTable : (TableElement) null;
    }
  }

  /// <summary>Действует режим создания элемента</summary>
  public bool IsElementCreating
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.IsElementCreating;
    }
    set
    {
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.IsElementCreating = value;
    }
  }

  /// <summary>Выбранный класс создатель элемента</summary>
  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null ? this.DocumentControl.SelectedElementCreator : (PageElementCreator) null;
    }
    set
    {
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.SelectedElementCreator = value;
    }
  }

  /// <summary>Показывать невидимые линии границ</summary>
  public bool ShowInvisibleLines
  {
    [DebuggerStepThrough] get
    {
      DocumentControl documentControl = this.DocumentControl;
      return documentControl != null && documentControl.ShowInvisibleLines;
    }
  }

  /// <summary>Элемент, которому принадлежит фокус клавиатуры</summary>
  public PageElementUI FocusedElement
  {
    [DebuggerStepThrough] get => this.focusedElement;
  }

  /// <summary>Назначить значение свойству FocusedElement</summary>
  /// <param name="value">Значение</param>
  internal void SetFocusedElement(PageElementUI value) => this.focusedElement = value;

  public Page GetPageAtPoint(Point point)
  {
    if (this.OnePage)
    {
      if (this.DocumentControl != null)
        return this.DocumentControl.ActivePage;
    }
    else
    {
      foreach (PageData pageData in (ImDocumentData) this.Document)
      {
        if (pageData is Page pageAtPoint && pageAtPoint.PageUI != null && pageAtPoint.PageUI.Bounds.Contains(point))
          return pageAtPoint;
      }
    }
    return (Page) null;
  }

  private PageElementUI GetPageElementUIAtPoint(
    IList<DocumentTreeNode> nodes,
    Point point,
    ref int layer,
    bool firstOnly,
    bool ignoreGrabHandle)
  {
    PageElementUI elementUiAtPoint1 = (PageElementUI) null;
    for (int index = 0; index < nodes.Count && (!firstOnly || elementUiAtPoint1 == null); ++index)
    {
      PageElementUI elementUiAtPoint2 = this.GetPageElementUIAtPoint(nodes[index], point, ref layer, firstOnly, ignoreGrabHandle);
      if (elementUiAtPoint2 != null)
        elementUiAtPoint1 = elementUiAtPoint2;
    }
    return elementUiAtPoint1;
  }

  private PageElementUI GetPageElementUIAtPoint(
    DocumentTreeNode node,
    Point point,
    ref int layer,
    bool firstOnly,
    bool ignoreGrabHandle)
  {
    PageElementUI elementUiAtPoint1 = (PageElementUI) null;
    if (node.IsVirtualNode && node.Nodes != null)
    {
      for (int index = 0; index < node.Nodes.Count && (!firstOnly || elementUiAtPoint1 == null); ++index)
      {
        PageElementUI elementUiAtPoint2 = this.GetPageElementUIAtPoint(node.Nodes[index], point, ref layer, firstOnly, ignoreGrabHandle);
        if (elementUiAtPoint2 != null)
          elementUiAtPoint1 = elementUiAtPoint2;
      }
    }
    else if (node is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      elementUiAtPoint1 = elementWithInterface.PageUI.GetPageElementUIAtPoint(point, ref layer, !firstOnly, ignoreGrabHandle);
    return elementUiAtPoint1;
  }

  private bool PointAtSelection(Point point)
  {
    if (this.DocumentControl != null && this.DocumentControl.SelectedNodes.Count > 0)
    {
      List<DocumentTreeNode> nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds((IList<DocumentTreeNode>) this.DocumentControl.SelectedNodes, true);
      int layer = -1;
      if (this.GetPageElementUIAtPoint((IList<DocumentTreeNode>) nodesWithoutChilds, point, ref layer, false, false) != null)
        return true;
    }
    return false;
  }

  private void contextMenuBarItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    try
    {
      if (this.DocumentControl == null)
        return;
      Point client = this.PointToClient(Control.MousePosition);
      List<DocumentTreeNode> context = (List<DocumentTreeNode>) null;
      if (this.IsCoorSystemSelecting)
        this.IsCoorSystemSelecting = false;
      if (this.IsPasting)
      {
        this.contextMenuBarItem.Items.Clear();
        List<ToolbarItemBase> toolbarItemBaseList = new List<ToolbarItemBase>();
        this.GetPastingContextMenu(toolbarItemBaseList);
        if (toolbarItemBaseList.Count <= 0)
          return;
        NodeContextMenu.AddToContextMenu(this.contextMenuBarItem, toolbarItemBaseList);
      }
      else if (this.IsElementSelecting)
      {
        if (this.PointAtSelection(client))
        {
          context = this.DocumentControl.SelectedNodes;
        }
        else
        {
          PageElementUI elementUiAtPoint = this.GetPageElementUIAtPoint(client, false);
          if (elementUiAtPoint != null)
          {
            context = new List<DocumentTreeNode>(1);
            if (elementUiAtPoint is PageUI)
              context.Add((DocumentTreeNode) elementUiAtPoint.Page);
            else
              context.Add((DocumentTreeNode) elementUiAtPoint.Element);
          }
          else if (this.DocumentControl.ActivePage != null && this.DocumentControl.ContainsFocus)
          {
            this.DocumentControl.SetSelection((DocumentTreeNode) this.DocumentControl.ActivePage, false, Point.Empty, false, false);
            context = this.DocumentControl.SelectedNodes;
          }
        }
        this.contextMenuBarItem.Items.Clear();
        NodeContextMenu.AddToContextMenu(this.contextMenuBarItem, this.DocumentControl.GetContexMenu(context));
      }
      else
      {
        if (!this.IsElementCreating)
          return;
        this.contextMenuBarItem.Items.Clear();
        if (this.SelectedElementCreator == null)
          return;
        List<ToolbarItemBase> toolbarItemBaseList = new List<ToolbarItemBase>();
        this.SelectedElementCreator.GetContextMenu(toolbarItemBaseList);
        if (toolbarItemBaseList.Count > 0)
          NodeContextMenu.AddToContextMenu(this.contextMenuBarItem, toolbarItemBaseList);
        this.SelectedElementCreator.ShowingContextMenu = true;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Получить контекстное меню режима вставки элементов</summary>
  /// <param name="contextMenuItems">Пункты контекстного меню</param>
  public virtual void GetPastingContextMenu(List<ToolbarItemBase> contextMenuItems)
  {
    try
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Model_75"));
      menuButtonItem.CommandName = "CancelPasting";
      menuButtonItem.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_76");
      menuButtonItem.Click += new EventHandler(this.CancelPasting);
      contextMenuItems.Add((ToolbarItemBase) menuButtonItem);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Отменить создание элемента</summary>
  protected virtual void CancelPasting(object sender, EventArgs e)
  {
    try
    {
      List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
      for (int index = 0; index < this.pastingNodes.Length; ++index)
        selection.Add(this.pastingNodes[index]);
      this.CancelMoving((IList<DocumentTreeNode>) selection);
      this.IsPasting = false;
      for (int index = 0; index < this.pastingNodes.Length; ++index)
      {
        if (this.pastingNodes[index] is IPageElementWithInterface && this.pastingNodes[index] is VisualNode)
          (this.pastingNodes[index] as IPageElementWithInterface).DestroyUI();
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    this.pastingNodes = (DocumentTreeNode[]) null;
  }

  /// <summary>CommandManager редактора документов</summary>
  public ICommandManager CommandManager
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.DocumentManager != null ? this.DocumentControl.DocumentManager.CommandManager : (ICommandManager) null;
    }
  }

  private void contextMenuBarItem_AfterPopup(object sender, EventArgs e)
  {
    NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
    NodeContextMenu.ContextMenuCommand = false;
  }

  public void InvokeUpdateUIGeometry(bool refreshUI)
  {
    try
    {
      if (this.Document == null)
        return;
      if (this.InvokeRequired)
        this.Invoke((Delegate) new MethodInvoker_BoolArg(this.InvokeUpdateUIGeometry), (object) refreshUI);
      else
        this.Document.UpdateUIGeometry(refreshUI);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Происходит вставка на страницу</summary>
  public bool IsPasting
  {
    get => this.isPasting;
    set => this.isPasting = value;
  }

  public bool InitPasteNodesFromClipboard(DocumentTreeNode dest)
  {
    try
    {
      DocumentNodesClipboardData nodesFromClipboard = NodeClipboardHelper.GetNodesFromClipboard();
      if (nodesFromClipboard != null)
      {
        DocumentTreeNode[] nodes = nodesFromClipboard.Nodes;
        bool flag = false;
        if (nodes != null)
        {
          foreach (DocumentTreeNode documentTreeNode in nodes)
          {
            if (!(documentTreeNode is PageData))
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            this.Cursor = Cursors.Cross;
            this.pastingNodes = nodes;
            for (int index = 0; index < nodes.Length; ++index)
            {
              TableData tableData = nodes[index] as TableData;
              if (this.Document != null && tableData != null && tableData.IsPageFlow && tableData.FlowID != null)
              {
                FlowID flowIdByName = this.Document.FindFlowIDByName(tableData.FlowID);
                if (flowIdByName != null)
                  tableData.SetFlowID(flowIdByName, false, false);
              }
            }
            this.IsPasting = true;
            this.IsMovingSelected = true;
            this.pasteDest = dest;
          }
        }
        return flag;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      return false;
    }
    return true;
  }

  /// <summary>Устанавливает значения отображаемые в всплывающем окне при перетаскивании</summary>
  /// <param name="offsetFromLeft">Отступ от левого края листа</param>
  /// <param name="offsetFromRight">Отступ от правого края листа</param>
  /// <param name="leftCellSize">Ширина левого элемента</param>
  /// <param name="rightCellSize">Ширина правого элемента</param>
  public void SetBarValues(
    float? offsetFromLeft,
    float? offsetFromRight,
    float? leftCellSize,
    float? rightCellSize)
  {
    this.offsets[0] = offsetFromLeft;
    this.offsets[3] = offsetFromRight;
    this.offsets[1] = leftCellSize;
    this.offsets[2] = rightCellSize;
  }

  public void InvokeInvalidate(Rectangle rc, bool invalidateChildren)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new InvalidateInvoker_Arg(this.InvokeInvalidate), (object) rc, (object) invalidateChildren);
    else
      this.Invalidate(rc, invalidateChildren);
  }

  /// <summary>Инициализировать поля объекта</summary>
  private void InitFields()
  {
    try
    {
      this.pageControlUI = new PageControlUI(this);
      this._visiblePageElementUIs = this.pageControlUI.PageElementUIs;
      this.pageCursor = this.Cursor;
      if (this.Parent != null)
      {
        using (Graphics graphics = this.CreateGraphics())
          this.DisplayDpi = new PointF(graphics.DpiX, graphics.DpiY);
      }
      else
        this.DisplayDpi = new PointF(96f, 96f);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.timerMoving = new Timer();
      this.timerMoving.Interval = 50;
      this.timerMoving.Tick += new EventHandler(this.timerMoving_Tick);
      Application.LeaveThreadModal += new EventHandler(this.Application_LeaveThreadModal);
      this.timerDialog = new Timer();
      this.timerDialog.Interval = 100;
      this.timerDialog.Tick += new EventHandler(this.timerDialog_Tick);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    IntPtr handle = this.Handle;
    int num = this.IsHandleCreated ? 1 : 0;
  }

  private void timerDialog_Tick(object sender, EventArgs e)
  {
    try
    {
      this.UnLockMouse();
      this.timerDialog.Stop();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void Application_LeaveThreadModal(object sender, EventArgs e)
  {
    try
    {
      this.LockMouse();
      this.timerDialog.Start();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnParentChanged(EventArgs e) => base.OnParentChanged(e);

  /// <summary>Конструктор</summary>
  public PageControl(ImDocument doc)
  {
    try
    {
      this.InitializeComponent();
      this.menuBar.Renderer.Dispose();
      this.menuBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.Document = doc;
      this.bmpHors = new Bitmap[4];
      this.bmpVerts = new Bitmap[4];
      this.offsets = new float?[4];
      this.bmpHors[0] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor_l.png"));
      this.bmpHors[1] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor.png"));
      this.bmpHors[2] = this.bmpHors[1];
      this.bmpHors[3] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor_r.png"));
      this.bmpVerts[0] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver_t.png"));
      this.bmpVerts[1] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver.png"));
      this.bmpVerts[2] = this.bmpVerts[1];
      this.bmpVerts[3] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver_b.png"));
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    this.InitFields();
  }

  protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);

  /// <summary>Конструктор</summary>
  public PageControl()
  {
    try
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this.bmpHors = new Bitmap[4];
      this.bmpVerts = new Bitmap[4];
      this.offsets = new float?[4];
      this.bmpHors[0] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor_l.png"));
      this.bmpHors[1] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor.png"));
      this.bmpHors[2] = this.bmpHors[1];
      this.bmpHors[3] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.hor_r.png"));
      this.bmpVerts[0] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver_t.png"));
      this.bmpVerts[1] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver.png"));
      this.bmpVerts[2] = this.bmpVerts[1];
      this.bmpVerts[3] = (Bitmap) Image.FromStream(typeof (PageControl).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.ver_b.png"));
      this.InitFields();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Параметры создания при создании дескриптора элемента управления</summary>
  protected override CreateParams CreateParams
  {
    [DebuggerStepThrough] get
    {
      CreateParams createParams = base.CreateParams;
      createParams.Style &= -33554433;
      return createParams;
    }
  }

  public virtual bool EditorValidating()
  {
    DocumentControl documentControl = this.DocumentControl;
    return documentControl == null || documentControl.EditorValidating();
  }

  /// <summary>Вызывает событие Click</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnClick(EventArgs e)
  {
    try
    {
      Page pageAtPoint = this.GetPageAtPoint(this.PointToClient(Control.MousePosition));
      if (pageAtPoint != null && pageAtPoint.IsLocked || !this.EditorValidating())
        return;
      if (this.IsElementSelecting)
      {
        Point client = this.PointToClient(Control.MousePosition);
        if (this.elementAtCursor == null)
          this.elementAtCursor = this.GetPageElementUIAtPoint(client, false);
        else if (this.elementAtCursor.PageControl != this)
          this.elementAtCursor = (PageElementUI) null;
        if (this.elementAtCursor != null)
          this.elementAtCursor.OnClick(e);
      }
      else if (this.IsElementCreating && this.SelectedElementCreator != null)
      {
        if (this.SelectedElementCreator.HostPage == null)
          this.SelectedElementCreator.HostPage = pageAtPoint;
        if (this.SelectedElementCreator.HostPage == pageAtPoint)
          this.SelectedElementCreator.OnClick(e);
      }
      base.OnClick(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Вызывает событие DoubleClick</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnDoubleClick(EventArgs e)
  {
    try
    {
      if (Control.ModifierKeys == (Keys.Shift | Keys.Control | Keys.Alt))
        ImDocumentData.ShowDebugInfo = !ImDocumentData.ShowDebugInfo;
      Page pageAtPoint = this.GetPageAtPoint(this.PointToClient(Control.MousePosition));
      if (pageAtPoint != null && pageAtPoint.IsLocked || !this.EditorValidating())
        return;
      if (this.IsElementSelecting)
      {
        this.elementAtCursor = this.GetPageElementUIAtPoint(this.PointToClient(Control.MousePosition), false);
        if (this.elementAtCursor != null)
          this.elementAtCursor.OnDoubleClick(e);
      }
      else if (this.IsElementCreating && this.SelectedElementCreator != null)
      {
        if (this.SelectedElementCreator.HostPage == null)
          this.SelectedElementCreator.HostPage = pageAtPoint;
        if (this.SelectedElementCreator.HostPage == pageAtPoint)
          this.SelectedElementCreator.OnDoubleClick(e);
      }
      base.OnDoubleClick(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Вызывает событие MouseEnter</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnMouseEnter(EventArgs e)
  {
    try
    {
      base.OnMouseEnter(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Получить для узла документа интерфейсный объект</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  protected PageElementUI GetPageElementUI(DocumentTreeNode node)
  {
    return node is IPageElementWithInterface elementWithInterface ? elementWithInterface.PageUI : (PageElementUI) null;
  }

  protected override void WndProc(ref Message m)
  {
    try
    {
      base.WndProc(ref m);
    }
    catch (Exception ex)
    {
      throw new Exception($"{ex.GetType().ToString()} -- {ex.Message} -- {m.ToString()}", ex);
    }
  }

  /// <summary>Вызывает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    try
    {
      if (this.LockedMouse)
      {
        base.OnMouseDown(e);
      }
      else
      {
        if (this.DocumentControl != null && this.DocumentControl.Parent is ImDocumentEditorFormBase parent && parent != parent.Manager.ActiveDockControl)
          parent.Activate();
        Page pageAtPoint = this.GetPageAtPoint(e.Location);
        if (pageAtPoint != null && pageAtPoint.IsLocked)
          return;
        Point point = new Point(e.X, e.Y);
        if (e.Button == MouseButtons.Right && this.elementAtCursor != null && this.elementAtCursor.IsMoving)
          this.elementAtCursor.OnMouseDown(e);
        DocumentControl documentControl = this.DocumentControl;
        if (this.IsPasting && e.Button == MouseButtons.Left && this.pastingNodes != null)
        {
          this.Cursor = Cursors.Default;
          this.DrawMovingPreview((Graphics) null, true);
          this.isMovingSelected = false;
          if (this.startPastingPoint == point)
            return;
          List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
          for (int index = 0; index < this.pastingNodes.Length; ++index)
            selection.Add(this.pastingNodes[index]);
          if (selection.Count == 0)
            return;
          PointF delta = this.CalcMovingDelta(this.startPastingPoint, new Point(point.X - this.startPastingPoint.X, point.Y - this.startPastingPoint.Y), (IList<DocumentTreeNode>) selection, false);
          this.MoveSelectedElements((IList<DocumentTreeNode>) selection, delta);
          if (documentControl != null && documentControl.DocumentManager != null)
            documentControl.DocumentManager.SetMessageText("");
          this.IsPasting = false;
          this.pasteDest = (DocumentTreeNode) pageAtPoint;
          if (this.pasteDest == null)
            this.pasteDest = (DocumentTreeNode) this.HostMovingPage;
          if (this.pasteDest != null)
            NodeClipboardHelper.PasteFromClipboard(this.pasteDest, IntPtr.Zero, this.pastingNodes);
          this.HostMovingPage = (Page) null;
        }
        PageElementUI pageElementUi = (PageElementUI) null;
        if (this.IsElementSelecting)
          pageElementUi = this.GetPageElementUIAtPoint(point, false);
        PageElementNode activeElement = this.ActiveElement as PageElementNode;
        this.DocumentControl.SuspendScrollBars();
        try
        {
          if (!this.DocumentControl.ReadOnly)
          {
            if (!this.EditorValidating())
              return;
          }
        }
        finally
        {
          this.DocumentControl.ResumeScrollBars(false);
        }
        if (this.IsCoorSystemSelecting && pageAtPoint != null)
        {
          PointF world = pageAtPoint.PageUI.ConvertPixelToWorld(point);
          PointF pointF = pageAtPoint.PageUI.SnapPoint(world, (VisualNode) null);
          pointF.Y = pageAtPoint.Size.Height - pointF.Y;
          ImDocumentEditorConfig.Instance.AssignСustomCoorSystemPosition(pointF);
          ImDocumentEditorConfig.Instance.AssignCoorSystem(PageCoorSystem.Custom);
          this.IsCoorSystemSelecting = false;
        }
        else if (this.IsElementSelecting)
        {
          if (this.GetPageElementUIAtPoint(point, false) != pageElementUi)
          {
            if (activeElement == null)
              return;
            documentControl?.SetSelection((DocumentTreeNode) null, false, false);
            documentControl?.SetSelection((DocumentTreeNode) activeElement, false, Point.Empty, true, false);
            return;
          }
          this.leftMouseDownHanldedByChild = false;
          this.isMouseDownValidated = true;
          if (e.Button == MouseButtons.Left)
            this.leftMouseDownPos = point;
          if (e.Button != MouseButtons.Right || !this.PointAtSelection(point))
          {
            if (documentControl != null)
            {
              List<DocumentTreeNode> selectedNodes = documentControl.SelectedNodes;
              if (selectedNodes != null && selectedNodes.Count > 1 && this.PointAtSelection(point))
                return;
              if (selectedNodes != null && selectedNodes.Count == 1 && this.PointAtSelection(point))
              {
                if (selectedNodes[0] is TableData tableData && tableData.IsRow)
                  return;
                if (this.DocumentControl.RowSelection && tableData != null && tableData.IsVirtualNode)
                {
                  bool flag = true;
                  foreach (DocumentTreeNode realCell in tableData.GetRealCells())
                  {
                    if (!(realCell is TableData) || !(realCell as TableData).IsRow)
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag)
                    return;
                }
              }
            }
            this.elementAtCursor = this.GetPageElementUIAtPoint(point, false);
            if (this.elementAtCursor != null)
            {
              bool capture = this.Capture;
              this.leftMouseDownHanldedByChild = e.Button == MouseButtons.Left;
              this.elementAtCursor.OnMouseDown(e);
              if (this.Capture != capture)
                this.isMouseDownValidated = false;
            }
            else if (documentControl != null && e.Button == MouseButtons.Left && this.Document != null && Control.ModifierKeys != Keys.Control)
              documentControl.SetSelection((DocumentTreeNode) this.Document, false, Point.Empty, false, false);
          }
        }
        else if (this.IsElementCreating && this.SelectedElementCreator != null)
        {
          if (this.SelectedElementCreator.HostPage != pageAtPoint && pageAtPoint != null)
            this.SelectedElementCreator.HostPage = pageAtPoint;
          if (this.SelectedElementCreator.HostPage == pageAtPoint)
            this.SelectedElementCreator.OnMouseDown(e);
        }
        base.OnMouseDown(e);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected void CancelMoving(IList<DocumentTreeNode> selection)
  {
    for (int index = 0; index < selection.Count; ++index)
    {
      if (selection[index] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && !elementWithInterface.PageUI.GeometryChangingBlocked)
      {
        Point startPoint = this.leftMouseDownPos;
        if (this.IsPasting)
          startPoint = this.startPastingPoint;
        elementWithInterface.PageUI.CancelMoving(startPoint, false);
      }
    }
    this.DrawMovingPreview((Graphics) null, true);
  }

  protected void DrawMovingPreview(Graphics g, bool eraseOnly)
  {
    if (!this.isMovingSelected && !this.IsPasting)
      return;
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null)
      return;
    List<DocumentTreeNode> selection = documentControl.SelectedNodes;
    if (this.IsPasting)
    {
      selection = new List<DocumentTreeNode>();
      for (int index = 0; index < this.pastingNodes.Length; ++index)
        selection.Add(this.pastingNodes[index]);
    }
    List<DocumentTreeNode> pageSelection = this.GetPageSelection((PageData) this.HostMovingPage, (IList<DocumentTreeNode>) selection);
    if (pageSelection == null || pageSelection.Count == 0)
      return;
    this.suspendDrawMovingPreview = true;
    try
    {
      bool flag = g == null;
      if (g == null | eraseOnly)
      {
        for (int index = 0; index < pageSelection.Count; ++index)
        {
          if (pageSelection[index] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && (!elementWithInterface.PageUI.GeometryChangingBlocked || this.IsPasting))
            elementWithInterface.PageUI.EraseNewGeometryPreview(false);
        }
        this.Update();
        if (eraseOnly)
          return;
        g = this.CreateGraphics();
      }
      try
      {
        for (int index = 0; index < pageSelection.Count; ++index)
        {
          if (pageSelection[index] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && (!elementWithInterface.PageUI.GeometryChangingBlocked || this.IsPasting))
            elementWithInterface.PageUI.DrawNewGeometryPreview(g);
        }
      }
      finally
      {
        if (flag && g != null)
          g.Dispose();
      }
    }
    finally
    {
      this.suspendDrawMovingPreview = false;
    }
  }

  protected PointF CalcMovingDelta(
    Point firstPoint,
    Point mouseDelta,
    IList<DocumentTreeNode> selection,
    bool updateUINewBounds)
  {
    Page page = this.GetPageAtPoint(this.PointToClient(Control.MousePosition)) ?? this.HostMovingPage;
    if (page == null)
      return PointF.Empty;
    RectangleF empty1 = (RectangleF) Rectangle.Empty;
    PointF empty2 = PointF.Empty;
    PointF pointF1 = empty2;
    bool flag1 = true;
    bool flag2 = true;
    RectangleF rectangleF1;
    for (int index = 0; index < selection.Count; ++index)
    {
      if (selection[index] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && (!elementWithInterface.PageUI.GeometryChangingBlocked || this.IsPasting))
      {
        switch (elementWithInterface)
        {
          case RectangleElement rectangleElement:
            rectangleF1 = rectangleElement.Bounds;
            flag1 = false;
            break;
          case Polyline polyline:
            rectangleF1 = polyline.GetBounds();
            flag1 = false;
            break;
          default:
            continue;
        }
        if (flag2)
        {
          empty2.X = rectangleF1.X;
          pointF1.X = rectangleF1.Right;
          empty2.Y = rectangleF1.Y;
          pointF1.Y = rectangleF1.Bottom;
          flag2 = false;
        }
        else
        {
          if ((double) empty2.X > (double) rectangleF1.X)
            empty2.X = rectangleF1.X;
          else if ((double) pointF1.X < (double) rectangleF1.Right)
            pointF1.X = rectangleF1.Right;
          if ((double) empty2.Y > (double) rectangleF1.Y)
            empty2.Y = rectangleF1.Y;
          else if ((double) pointF1.Y < (double) rectangleF1.Bottom)
            pointF1.Y = rectangleF1.Bottom;
        }
      }
    }
    if (flag1)
      return PointF.Empty;
    PointF pointF2 = (PointF) page.PageUI.ConvertPixelToWorld(new Size(mouseDelta));
    if (this.HostMovingPage != page && this.HostMovingPage != null)
    {
      Size size = new Size(firstPoint);
      PointF world1 = this.HostMovingPage.PageUI.ConvertPixelToWorld(firstPoint);
      PointF world2 = page.PageUI.ConvertPixelToWorld(new Point(firstPoint.X + mouseDelta.X, firstPoint.Y + mouseDelta.Y));
      pointF2 = new PointF(world2.X - world1.X, world2.Y - world1.Y);
    }
    RectangleF rectangleF2 = RectangleF.FromLTRB(empty2.X, empty2.Y, pointF1.X, pointF1.Y);
    rectangleF1 = new RectangleF(new PointF(rectangleF2.X + pointF2.X, rectangleF2.Y + pointF2.Y), rectangleF2.Size);
    if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
    {
      if ((double) Math.Abs(rectangleF1.X - rectangleF2.X) < (double) Math.Abs(rectangleF1.Y - rectangleF2.Y))
        rectangleF1.X = rectangleF2.X;
      else
        rectangleF1.Y = rectangleF2.Y;
    }
    rectangleF1 = page.PageUI.SnapRectangle(rectangleF1, (VisualNode) null);
    pointF2 = new PointF(rectangleF1.X - rectangleF2.X, rectangleF1.Y - rectangleF2.Y);
    if (updateUINewBounds)
    {
      DocumentControl documentControl = this.DocumentControl;
      if (documentControl != null && documentControl.DocumentManager != null)
      {
        PointF user1 = page.PageUI.ConvertInternalToUser(rectangleF1.Location);
        PointF user2 = page.PageUI.ConvertInternalToUser(new PointF(rectangleF1.Right, rectangleF1.Bottom));
        SizeF user3 = page.PageUI.ConvertInternalToUser(rectangleF1.Size);
        string text = string.Format(LocalizationHolder.rm.GetString("Document.Model_73"), (object) user1.X, (object) user3.Width, (object) user2.X, (object) user2.Y, (object) user3.Height, (object) user1.Y);
        if (this.IsPasting)
          text = $"{LocalizationHolder.rm.GetString("Document.Model_520")}: {text}";
        documentControl.DocumentManager.SetMessageText(text);
      }
      for (int index1 = 0; index1 < selection.Count; ++index1)
      {
        if (selection[index1] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && (!elementWithInterface.PageUI.GeometryChangingBlocked || this.IsPasting))
        {
          switch (elementWithInterface)
          {
            case RectangleElement rectangleElement:
              rectangleF1 = rectangleElement.Bounds;
              rectangleF1.Location = new PointF(rectangleF1.X + pointF2.X, rectangleF1.Y + pointF2.Y);
              Rectangle pixel = page.PageUI.ConvertWorldToPixel(rectangleF1);
              elementWithInterface.PageUI.NewBounds = pixel;
              continue;
            case Polyline polyline:
              PointF[] pathPoints = polyline.PathPoints;
              if (pathPoints != null && pathPoints.Length > 1)
              {
                PointF[] pts = (PointF[]) pathPoints.Clone();
                for (int index2 = 0; index2 < pts.Length; ++index2)
                  pts[index2] = page.PageUI.ConvertWorldToPixelF(new PointF(pts[index2].X + pointF2.X, pts[index2].Y + pointF2.Y));
                ((PolylineUI) polyline.PageUI).NewDisplayPath = new GraphicsPath(pts, polyline.PathTypes);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    return pointF2;
  }

  protected void MoveSelectedElements(IList<DocumentTreeNode> selection, PointF delta)
  {
    this.Document.SuspendRefreshUI();
    Page page = this.GetPageAtPoint(this.PointToClient(Control.MousePosition)) ?? this.HostMovingPage;
    for (int index1 = 0; index1 < selection.Count; ++index1)
    {
      if (selection[index1] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && (!elementWithInterface.PageUI.GeometryChangingBlocked || this.IsPasting))
      {
        if (elementWithInterface is RectangleElement child2)
        {
          if (Control.ModifierKeys == Keys.Control)
            child2 = (RectangleElement) child2.Clone();
          RectangleF bounds = child2.Bounds;
          bounds.Location = UnitsConverter.RoundPoint(new PointF(bounds.X + delta.X, bounds.Y + delta.Y), 5);
          child2.AssignBounds(bounds, true, false, false);
          if (Control.ModifierKeys == Keys.Control && page != null)
            page.AddChildNode((DocumentTreeNode) child2, false, false);
        }
        else if (elementWithInterface is Polyline child1)
        {
          if (Control.ModifierKeys == Keys.Control)
            child1 = (Polyline) child1.Clone();
          PointF[] pathPoints = child1.PathPoints;
          if (pathPoints != null && pathPoints.Length > 1)
          {
            for (int index2 = 0; index2 < pathPoints.Length; ++index2)
              pathPoints[index2] = UnitsConverter.RoundPoint(new PointF(pathPoints[index2].X + delta.X, pathPoints[index2].Y + delta.Y), 5);
            child1.SetPath(new GraphicsPath(pathPoints, child1.PathTypes), false, true);
          }
          if (Control.ModifierKeys == Keys.Control && page != null)
            page.AddChildNode((DocumentTreeNode) child1, false, false);
        }
        if (page != this.HostMovingPage && Control.ModifierKeys != Keys.Control && selection[index1] is PageElementNode pageElementNode)
        {
          pageElementNode.SuspendRefreshUI();
          pageElementNode.Page.RemoveChildNode((DocumentTreeNode) pageElementNode, false, false);
          page?.AddChildNode((DocumentTreeNode) pageElementNode, false, false);
          pageElementNode.ResumeRefreshUI(false);
          DocumentControl.SetShowSelected((DocumentTreeNode) pageElementNode, true, false);
        }
      }
    }
    if (page != null)
    {
      page.Distribute(new DistributeContext(), true);
      List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
      this.DocumentControl.SetSelection(new List<DocumentTreeNode>(), false, false);
      this.DocumentControl.SetSelection(selectedNodes, false, false);
    }
    this.Document.ResumeRefreshUI(true);
  }

  public override Cursor Cursor
  {
    get => base.Cursor;
    set => base.Cursor = value;
  }

  /// <summary>Получить список из элементов принадлежащих конкретной странице</summary>
  /// <param name="page">страница</param>
  /// <param name="selection">исходный список</param>
  /// <returns>список элементов</returns>
  private List<DocumentTreeNode> GetPageSelection(PageData page, IList<DocumentTreeNode> selection)
  {
    List<DocumentTreeNode> pageSelection = new List<DocumentTreeNode>();
    try
    {
      if (selection == null)
        return (List<DocumentTreeNode>) null;
      PageData pageData = page;
      foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) selection)
      {
        if (documentTreeNode is PageElementNode pageElementNode && pageElementNode.Page != null && pageData == pageElementNode.Page)
          pageSelection.Add((DocumentTreeNode) pageElementNode);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return pageSelection;
  }

  /// <summary>Вызывает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    try
    {
      if (this.LockedMouse)
      {
        base.OnMouseMove(e);
      }
      else
      {
        Point point1 = new Point(e.X, e.Y);
        AnchorStyles style = AnchorStyles.None;
        IPageElementWithInterface activeElement = this.DocumentControl?.ActiveElement as IPageElementWithInterface;
        PageElementUI pageElementUi = (PageElementUI) null;
        if (activeElement != null)
          pageElementUi = activeElement.PageUI;
        if (((this.IsTableCellsSelecting || this.IsTableRowsSelecting) && this.elementAtCursor is TableCellUI || this.IsElementCreating || this.IsMovingSelected || this.IsPasting || pageElementUi != null && pageElementUi.IsMoving) && this.PointInMovingArea(point1, ref style))
        {
          this.timerMoving.Tag = (object) style;
          this.timerMoving.Start();
        }
        Page page = this.GetPageAtPoint(e.Location);
        if (page != null && (page.IsLocked || page.PageUI == null))
          return;
        PointF position = PointF.Empty;
        if (page?.PageUI != null)
        {
          PointF world = page.PageUI.ConvertPixelToWorld(point1);
          PointF point2 = page.PageUI.SnapPoint(world, (VisualNode) null);
          position = page.PageUI.ConvertInternalToUser(point2);
          this.DocumentControl?.AssignPageCursorPosition(page, position);
        }
        if (this.IsPasting && this.pastingNodes != null)
        {
          if (page == null)
            page = this.HostMovingPage;
          if (page != null)
          {
            this.Cursor = Cursors.Cross;
            if (this.IsMovingSelected)
            {
              PointF pointF1 = new PointF((float) int.MaxValue, (float) int.MaxValue);
              PointF pointF2 = (PointF) new Point(0, 0);
              for (int index1 = 0; index1 < this.pastingNodes.Length; ++index1)
              {
                if (this.pastingNodes[index1] is Polyline)
                {
                  Polyline pastingNode = this.pastingNodes[index1] as Polyline;
                  PointF pathPoint = pastingNode.PathPoints[0];
                  for (int index2 = 1; index2 < pastingNode.PathPoints.Length; ++index2)
                  {
                    if ((double) pastingNode.PathPoints[index2].X < (double) pathPoint.X)
                      pathPoint = pastingNode.PathPoints[index2];
                  }
                  if ((double) pathPoint.X < (double) pointF1.X || index1 == 0)
                    pointF1 = pathPoint;
                }
                if (this.pastingNodes[index1] is RectangleElement)
                {
                  RectangleElement pastingNode = this.pastingNodes[index1] as RectangleElement;
                  if ((double) pastingNode.Bounds.Left < (double) pointF1.X || index1 == 0)
                    pointF1 = pastingNode.Bounds.Location;
                }
              }
              PointF world = page.PageUI.ConvertPixelToWorld(point1);
              pointF2.X = world.X - pointF1.X;
              pointF2.Y = world.Y - pointF1.Y;
              if ((double) pointF2.X != 0.0 || (double) pointF2.Y != 0.0)
              {
                for (int index3 = 0; index3 < this.pastingNodes.Length; ++index3)
                {
                  if (this.pastingNodes[index3] is Polyline)
                  {
                    Polyline pastingNode = this.pastingNodes[index3] as Polyline;
                    PointF[] pts = (PointF[]) pastingNode.PathPoints.Clone();
                    for (int index4 = 0; index4 < pts.Length; ++index4)
                    {
                      pts[index4].X += pointF2.X;
                      pts[index4].Y += pointF2.Y;
                    }
                    pastingNode.SetPath(new GraphicsPath(pts, pastingNode.PathTypes), false, false);
                  }
                  if (this.pastingNodes[index3] is RectangleElement)
                  {
                    RectangleElement pastingNode = this.pastingNodes[index3] as RectangleElement;
                    RectangleF bounds = pastingNode.Bounds;
                    bounds.X += pointF2.X;
                    bounds.Y += pointF2.Y;
                    pastingNode.AssignBounds(bounds, true, false, false);
                  }
                }
              }
            }
            for (int index = 0; index < this.pastingNodes.Length; ++index)
            {
              if (this.pastingNodes[index] is IPageElementWithInterface && this.pastingNodes[index] is VisualNode)
              {
                IPageElementWithInterface pastingNode1 = this.pastingNodes[index] as IPageElementWithInterface;
                VisualNode pastingNode2 = this.pastingNodes[index] as VisualNode;
                if (this.IsMovingSelected && pastingNode1.PageUI == null && pastingNode2 != null)
                {
                  pastingNode2.AssignParent((DocumentTreeNode) page, false, false, false);
                  pastingNode2.SetNeedUI(true, true);
                  pastingNode2.CreateUI();
                }
              }
            }
            if (this.IsMovingSelected)
            {
              this.startPastingPoint = point1;
              this.HostMovingPage = page;
            }
            this.IsMovingSelected = false;
            List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
            for (int index = 0; index < this.pastingNodes.Length; ++index)
              selection.Add(this.pastingNodes[index]);
            this.CalcMovingDelta(this.startPastingPoint, new Point(point1.X - this.startPastingPoint.X, point1.Y - this.startPastingPoint.Y), (IList<DocumentTreeNode>) selection, true);
            this.DrawMovingPreview((Graphics) null, false);
          }
        }
        else if (this.IsCoorSystemSelecting)
        {
          if (page != null)
          {
            IImDocumentManager documentManager;
            if (this.DocumentControl != null && (documentManager = this.DocumentControl.DocumentManager) != null)
            {
              position.X.ToString("F2");
              IImDocumentManager imDocumentManager = documentManager;
              string format = LocalizationHolder.rm.GetString("Document.Model_74");
              float num = position.X;
              string str1 = num.ToString("F2");
              num = position.Y;
              string str2 = num.ToString("F2");
              string text = string.Format(format, (object) str1, (object) str2);
              imDocumentManager.SetMessageText(text);
            }
            this.Cursor = Cursors.Cross;
          }
        }
        else if (this.IsElementSelecting)
        {
          if (this.isMouseDownValidated && this.IsMovingSelected && !this.IsMouseDownSelecting)
          {
            if (this.prevMousePos == point1)
              return;
            List<DocumentTreeNode> pageSelection = this.GetPageSelection((PageData) this.HostMovingPage, (IList<DocumentTreeNode>) this.DocumentControl.SelectedNodes);
            if (pageSelection == null || pageSelection.Count == 0)
            {
              this.isMovingSelected = false;
              return;
            }
            this.CalcMovingDelta(this.leftMouseDownPos, new Point(point1.X - this.leftMouseDownPos.X, point1.Y - this.leftMouseDownPos.Y), (IList<DocumentTreeNode>) pageSelection, true);
            this.DrawMovingPreview((Graphics) null, false);
          }
          else if (this.isMouseDownValidated && this.IsRectangleSelecting)
          {
            this.DrawSelectionRectangle((Graphics) null, PageControl.NormalRectangle(point1, this.leftMouseDownPos), false);
          }
          else
          {
            List<DocumentTreeNode> selectedNodes = this.DocumentControl?.SelectedNodes;
            PageElementUI elementAtCursor = this.elementAtCursor;
            if (e.Button == MouseButtons.None)
              this.elementAtCursor = this.GetPageElementUIAtPoint(point1, false);
            else if (this.elementAtCursor != null && this.elementAtCursor.PageControl != this)
              this.elementAtCursor = (PageElementUI) null;
            if (elementAtCursor != this.elementAtCursor && elementAtCursor != null)
              elementAtCursor.OnMouseLeave(new EventArgs());
            if (this.elementAtCursor != null && !(this.elementAtCursor is PageUI))
            {
              if (this.isMouseDownValidated && !this.IsMovingSelected && e.Button == MouseButtons.Left && this.PointAtSelection(this.leftMouseDownPos))
              {
                bool flag = false;
                if (this.DocumentControl != null && page != null)
                {
                  if (this.CanMove((IList<DocumentTreeNode>) selectedNodes))
                  {
                    if (Control.ModifierKeys != Keys.Control)
                      this.Cursor = Cursors.SizeAll;
                    else if (!this.IsMouseDownSelecting)
                      this.Cursor = PageElementUI.CopyCursor;
                    else
                      this.Cursor = Cursors.Default;
                    if (point1 != this.leftMouseDownPos)
                    {
                      for (int index = 0; index < selectedNodes.Count; ++index)
                      {
                        flag = selectedNodes[index] is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null && !elementWithInterface.PageUI.GeometryChangingBlocked;
                        if (flag)
                          break;
                      }
                    }
                  }
                  this.isMovingSelected = flag;
                  this.HostMovingPage = page;
                }
              }
              if (!this.isMovingSelected)
              {
                if (this.DocumentControl != null && this.DocumentControl.HasSuspendedSelection)
                  this.Cursor = Cursors.AppStarting;
                this.Cursor = this.elementAtCursor.GetCursor(point1);
                if (elementAtCursor != this.elementAtCursor)
                  this.elementAtCursor.OnMouseEnter(new EventArgs());
                this.elementAtCursor.Page = page;
                this.elementAtCursor.OnMouseMove(e);
                this.elementAtCursor.Page = (Page) null;
              }
            }
            else
            {
              if (this.DocumentControl != null && this.DocumentControl.HasSuspendedSelection)
                this.Cursor = Cursors.AppStarting;
              else
                this.Cursor = this.pageCursor;
              int num = 3;
              if (this.isMouseDownValidated && e.Button == MouseButtons.Left && Math.Abs(point1.X - this.leftMouseDownPos.X) > num && Math.Abs(point1.Y - this.leftMouseDownPos.Y) > num)
                this.IsRectangleSelecting = true;
            }
          }
        }
        else if (this.IsElementCreating && this.SelectedElementCreator != null)
        {
          if (this.SelectedElementCreator.HostPage != null)
            this.SelectedElementCreator.OnMouseMove(e);
          this.Cursor = this.SelectedElementCreator.Cursor;
        }
        base.OnMouseMove(e);
        this.prevMousePos = point1;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private bool CanMove(IList<DocumentTreeNode> selection)
  {
    if (selection == null || selection.Count <= 1)
      return false;
    for (int index = 0; index < selection.Count; ++index)
    {
      if (selection[index] is RectangleElement && (selection[index] as RectangleElement).IsTableCell)
        return false;
    }
    return true;
  }

  /// <summary>Вызывает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    try
    {
      if (this.LockedMouse)
      {
        base.OnMouseUp(e);
      }
      else
      {
        Page pageAtPoint = this.GetPageAtPoint(e.Location);
        if (pageAtPoint != null && (pageAtPoint.IsLocked || pageAtPoint.PageUI == null))
          return;
        this.isMouseDownValidated = false;
        if (!this.EditorValidating())
          return;
        Point point = new Point(e.X, e.Y);
        if (this.IsElementSelecting)
        {
          if (this.IsMovingSelected && e.Button == MouseButtons.Left)
          {
            if (!this.IsMouseDownSelecting)
            {
              this.DrawMovingPreview((Graphics) null, true);
              if (this.leftMouseDownPos == point)
                return;
              DocumentControl documentControl = this.DocumentControl;
              List<DocumentTreeNode> pageSelection = this.GetPageSelection((PageData) this.HostMovingPage, (IList<DocumentTreeNode>) documentControl?.SelectedNodes);
              if (pageSelection == null || pageSelection.Count == 0)
                return;
              PointF delta = this.CalcMovingDelta(this.leftMouseDownPos, new Point(point.X - this.leftMouseDownPos.X, point.Y - this.leftMouseDownPos.Y), (IList<DocumentTreeNode>) pageSelection, false);
              this.MoveSelectedElements((IList<DocumentTreeNode>) pageSelection, delta);
              this.HostMovingPage = (Page) null;
              if (documentControl != null && documentControl.DocumentManager != null)
                documentControl.DocumentManager.SetMessageText("");
            }
            this.isMovingSelected = false;
          }
          else if (this.IsRectangleSelecting && e.Button == MouseButtons.Left)
          {
            if (this.DocumentControl != null)
            {
              this.IsRectangleSelecting = false;
              Rectangle rect = PageControl.NormalRectangle(this.leftMouseDownPos, new Point(e.X, e.Y));
              List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
              this.GetPageElementsInRectangle(rect, documentTreeNodeList, true);
              this.DocumentControl.SetSelection(documentTreeNodeList, false, Point.Empty, false, false);
            }
          }
          else
          {
            if (this.elementAtCursor == null)
              this.elementAtCursor = this.GetPageElementUIAtPoint(new Point(e.X, e.Y), true);
            else if (this.elementAtCursor.PageControl != this)
              this.elementAtCursor = (PageElementUI) null;
            if (this.elementAtCursor != null)
            {
              if (!this.leftMouseDownHanldedByChild && e.Button == MouseButtons.Left && this.elementAtCursor.Element != this.DocumentControl.SelectedNode)
                this.elementAtCursor.OnMouseDown(e);
              this.leftMouseDownHanldedByChild = false;
              this.elementAtCursor.OnMouseUp(e);
            }
          }
        }
        else if (this.IsElementCreating && this.SelectedElementCreator != null)
          this.SelectedElementCreator.OnMouseUp(e);
        this.elementAtCursor = (PageElementUI) null;
        base.OnMouseUp(e);
        if (e.Button == MouseButtons.Left)
          this.leftMouseDownHanldedByChild = false;
        this.IsMouseDownSelecting = false;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработать нажатие клавиши</summary>
  /// <param name="msg">Сообщение</param>
  /// <param name="keyData">Данные о нажатой клавише</param>
  /// <returns>true, если нажатие обработано и не требует дальнейшей обработки</returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    try
    {
      DocumentControl documentControl = this.DocumentControl;
      if (documentControl != null)
      {
        PreProcessCmdKey_EventArgs e = new PreProcessCmdKey_EventArgs(msg, keyData, this.focusedElement);
        documentControl.OnPreProcessCmdKey(e);
        msg = e.Msg;
        if (e.Cancel)
          return true;
      }
      if (this.elementAtCursor != null && this.elementAtCursor.IsMoving && this.elementAtCursor.ProcessCmdKey(ref msg, keyData))
        return true;
      if (this.focusedElement != null && this.focusedElement.Element != null && this.focusedElement != this.pageControlUI)
      {
        if (this.focusedElement.ProcessCmdKey(ref msg, keyData))
          return true;
        if ((keyData == Keys.Delete || keyData == (Keys.X | Keys.Control) || keyData == (Keys.Delete | Keys.Shift)) && this.focusedElement.InPlaceEditorActive)
          return false;
      }
      switch (keyData)
      {
        case Keys.Escape:
          List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
          if (!this.IsPasting)
          {
            this.CancelMoving(this.DocumentControl != null ? (IList<DocumentTreeNode>) this.DocumentControl.SelectedNodes : (IList<DocumentTreeNode>) null);
            break;
          }
          this.CancelPasting((object) null, (EventArgs) null);
          break;
        case Keys.Delete:
          if (documentControl != null && documentControl.DocumentManager != null)
          {
            ICommandManager commandManager = documentControl.DocumentManager.CommandManager;
            if (commandManager != null)
            {
              ICommandState command = commandManager.FindCommand("Delete");
              if (command != null && command.Enabled)
              {
                commandManager.Execute(command);
                return true;
              }
            }
          }
          return true;
        case Keys.C | Keys.Control:
          if (documentControl != null && documentControl.DocumentManager != null)
          {
            ICommandManager commandManager = documentControl.DocumentManager.CommandManager;
            if (commandManager != null)
            {
              ICommandState command = commandManager.FindCommand("Copy");
              if (command != null && command.Enabled)
              {
                commandManager.Execute(command);
                return true;
              }
            }
          }
          return true;
        case Keys.V | Keys.Control:
          if (documentControl != null && documentControl.DocumentManager != null)
          {
            ICommandManager commandManager = documentControl.DocumentManager.CommandManager;
            if (commandManager != null)
            {
              ICommandState command = commandManager.FindCommand("Paste");
              if (command != null && command.Enabled)
              {
                commandManager.Execute(command);
                return true;
              }
            }
          }
          return true;
        case Keys.X | Keys.Control:
          if (documentControl != null && documentControl.DocumentManager != null)
          {
            ICommandManager commandManager = documentControl.DocumentManager.CommandManager;
            if (commandManager != null)
            {
              ICommandState command = commandManager.FindCommand("Cut");
              if (command != null && command.Enabled)
              {
                commandManager.Execute(command);
                return true;
              }
            }
          }
          return true;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      return false;
    }
  }

  /// <summary>Находится ли точка в зоне скродинга страницы по краям страницы</summary>
  /// <param name="point"></param>
  /// <param name="style">сторона страницы</param>
  /// <returns></returns>
  private bool PointInMovingArea(Point point, ref AnchorStyles style)
  {
    int num = 30;
    bool flag = false;
    if (point.X > 0 && point.X < num && (style == AnchorStyles.Left || style == AnchorStyles.None))
    {
      style = AnchorStyles.Left;
      return true;
    }
    if (point.X > this.Width - num && point.X < this.Width && (style == AnchorStyles.Right || style == AnchorStyles.None))
    {
      style = AnchorStyles.Right;
      return true;
    }
    if (point.Y > 0 && point.Y < num && (style == AnchorStyles.Top || style == AnchorStyles.None))
    {
      style = AnchorStyles.Top;
      return true;
    }
    if (point.Y <= this.Height - num || point.Y >= this.Height || style != AnchorStyles.Bottom && style != AnchorStyles.None)
      return flag;
    style = AnchorStyles.Bottom;
    return true;
  }

  /// <summary>Перемещение страницы за края</summary>
  /// <param name="point"></param>
  private void MoveArea(Point point)
  {
    int num = 30;
    Point empty = Point.Empty;
    if (point.Y > 0 && point.Y < num)
    {
      this.SetScrollBarValue((ScrollBar) this.DocumentControl.VScrollBar, this.DocumentControl.VScrollBar.Value - num);
      Point point1 = new Point(0, num);
    }
    else if (point.Y >= this.Height - num && point.Y <= this.Height)
    {
      this.SetScrollBarValue((ScrollBar) this.DocumentControl.VScrollBar, this.DocumentControl.VScrollBar.Value + num);
      Point point2 = new Point(0, -num);
    }
    else if (point.X > 0 && point.X < num)
    {
      this.SetScrollBarValue((ScrollBar) this.DocumentControl.HScrollBar, this.DocumentControl.HScrollBar.Value - num);
      Point point3 = new Point(num, 0);
    }
    else
    {
      if (point.X <= this.Width - num || point.X >= this.Width)
        return;
      this.SetScrollBarValue((ScrollBar) this.DocumentControl.HScrollBar, this.DocumentControl.HScrollBar.Value + num);
      Point point4 = new Point(-num, 0);
    }
  }

  private void timerMoving_Tick(object sender, EventArgs e)
  {
    try
    {
      Point client = this.PointToClient(Control.MousePosition);
      AnchorStyles tag = (AnchorStyles) this.timerMoving.Tag;
      if (this.PointInMovingArea(client, ref tag))
        this.MoveArea(client);
      else
        this.timerMoving.Stop();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnDragOver(DragEventArgs drgevent)
  {
    try
    {
      Point client = this.PointToClient(new Point(drgevent.X, drgevent.Y));
      AnchorStyles style = AnchorStyles.None;
      if (this.PointInMovingArea(client, ref style))
      {
        this.timerMoving.Tag = (object) style;
        this.timerMoving.Start();
      }
      PageElementUI elementUiAtPoint = this.GetPageElementUIAtPoint(client, true);
      DragDropEffects dragDropEffects = DragDropEffects.None;
      if (elementUiAtPoint != null && drgevent.Data.GetData(typeof (List<DocumentTreeNode>)) is List<DocumentTreeNode> data)
      {
        TableData tableData = data[0] as TableData;
        rectangleElement = (RectangleElement) (elementUiAtPoint.Element as TableData);
        if (rectangleElement == null && elementUiAtPoint.Element is RectangleElement rectangleElement)
          rectangleElement = (RectangleElement) rectangleElement.ParentCell;
        if (rectangleElement != null && tableData != null && rectangleElement.OwnerSubTable != null && rectangleElement.OwnerSubTable == tableData.OwnerSubTable)
        {
          int headersCount = rectangleElement.OwnerSubTable.HeadersCount;
          dragDropEffects = DragDropEffects.Move;
          if (tableData.IsRow && tableData.TableCellType != rectangleElement.TableCellType)
            dragDropEffects = DragDropEffects.None;
        }
      }
      drgevent.Effect = dragDropEffects;
      this.DragLinePosition = Rectangle.Empty;
      base.OnDragOver(drgevent);
      if (this.TopLevelControl is IStandaloneEditor)
        (this.TopLevelControl as IStandaloneEditor).DragOver(drgevent);
      if (drgevent.Effect == DragDropEffects.Move)
      {
        if (this.DragLinePosition == Rectangle.Empty)
        {
          if (elementUiAtPoint != null)
          {
            rectangleElement = (RectangleElement) (elementUiAtPoint.Element as TableData);
            if (rectangleElement == null && elementUiAtPoint.Element is RectangleElement rectangleElement)
              rectangleElement = (RectangleElement) rectangleElement.ParentCell;
            if (rectangleElement != null)
            {
              if ((rectangleElement as IPageElementWithInterface).PageUI != null)
              {
                Rectangle rectangle = (rectangleElement as IPageElementWithInterface).PageUI.Bounds;
                int num = rectangle.Y + rectangle.Height / 2;
                Point point1 = new Point(rectangle.Left, rectangle.Bottom);
                Point point2 = new Point(rectangle.Right, rectangle.Bottom);
                if (client.Y < num)
                {
                  point1 = new Point(rectangle.Left, rectangle.Top);
                  point2 = new Point(rectangle.Right, rectangle.Top);
                }
                rectangle = Rectangle.FromLTRB(point1.X, point1.Y, point2.X, point2.Y);
                this.DragLinePosition = rectangle;
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      drgevent.Effect = DragDropEffects.None;
    }
    this.Refresh();
  }

  /// <summary>Событие перед началом дрег дропа</summary>
  public event BeforeDoDragDrop_EventHandler BeforeDoDragDrop;

  internal virtual void OnBeforeDoDragDrop(BeforeDoDragDrop_EventArgs e)
  {
    try
    {
      BeforeDoDragDrop_EventHandler beforeDoDragDrop = this.BeforeDoDragDrop;
      if (beforeDoDragDrop == null)
        return;
      beforeDoDragDrop((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnDragDrop(DragEventArgs drgevent)
  {
    try
    {
      base.OnDragDrop(drgevent);
      if (drgevent.Effect != DragDropEffects.None && (this.DocumentControl.DocumentManager == null || this.DocumentControl.DocumentManager.GetType().Name == "DocumentEditorPlugin" || this.DocumentControl.DocumentManager.GetType().Name == "DocumentEditorMainForm"))
      {
        Point client = this.PointToClient(new Point(drgevent.X, drgevent.Y));
        PageElementUI elementUiAtPoint = this.GetPageElementUIAtPoint(client, true);
        if (elementUiAtPoint != null)
        {
          RectangleElement rectangleElement = (RectangleElement) (elementUiAtPoint.Element as TableData) ?? (elementUiAtPoint.Element is RectangleElement element ? (RectangleElement) element.ParentCell : (RectangleElement) null);
          if (rectangleElement != null)
          {
            List<DocumentTreeNode> data = drgevent.Data.GetData(typeof (List<DocumentTreeNode>)) as List<DocumentTreeNode>;
            TableData ownerSubTable = rectangleElement.OwnerSubTable;
            if (ownerSubTable != null && data != null)
            {
              Rectangle bounds = (rectangleElement as IPageElementWithInterface).PageUI.Bounds;
              int num = bounds.Y + bounds.Height / 2;
              bool flag = client.Y < num;
              for (int index1 = data.Count - 1; index1 >= 0; --index1)
              {
                DocumentTreeNode child = data[index1];
                int index2 = rectangleElement.Index + 1;
                if (flag)
                  index2 = rectangleElement.Index;
                if (child.Index < rectangleElement.Index && child.Index > -1)
                  --index2;
                if (rectangleElement.Parent != ownerSubTable)
                  index2 = ownerSubTable.Nodes.Count;
                ownerSubTable.InsertChildNode(index2, child, false, true, false, false, false);
              }
              ownerSubTable.UpdateLayout(true);
              return;
            }
          }
        }
      }
      if (!(this.TopLevelControl is IStandaloneEditor))
        return;
      (this.TopLevelControl as IStandaloneEditor).DragDrop(drgevent);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnDragEnter(DragEventArgs drgevent)
  {
    drgevent.Effect = DragDropEffects.None;
    base.OnDragEnter(drgevent);
  }

  protected override void OnDragLeave(EventArgs e)
  {
    this.Refresh();
    base.OnDragLeave(e);
  }

  protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
  {
    if (qcdevent != null && qcdevent.Action != DragAction.Continue)
      this.Refresh();
    base.OnQueryContinueDrag(qcdevent);
  }

  /// <summary>Нормализовать прямоугольник</summary>
  /// <param name="p1">Первая точка диагонали</param>
  /// <param name="p2">Вторая точка диагонали</param>
  /// <returns>Прямоугольник с положительными размерами</returns>
  public static Rectangle NormalRectangle(Point p1, Point p2)
  {
    int x1;
    int x2;
    if (p1.X < p2.X)
    {
      x1 = p1.X;
      x2 = p2.X;
    }
    else
    {
      x1 = p2.X;
      x2 = p1.X;
    }
    int y1;
    int y2;
    if (p1.Y < p2.Y)
    {
      y1 = p1.Y;
      y2 = p2.Y;
    }
    else
    {
      y1 = p2.Y;
      y2 = p1.Y;
    }
    return Rectangle.FromLTRB(x1, y1, x2, y2);
  }

  /// <summary>Нормализовать прямоугольник</summary>
  /// <param name="rect">Исходный прямоугольник</param>
  /// <returns>Прямоугольник с положительными размерами</returns>
  public static RectangleF NormalRectangle(RectangleF rect)
  {
    if ((double) rect.Width < 0.0)
    {
      rect.X += rect.Width;
      rect.Width = -rect.Width;
    }
    if ((double) rect.Height < 0.0)
    {
      rect.Y += rect.Height;
      rect.Height = -rect.Height;
    }
    return rect;
  }

  /// <summary>Нормализовать прямоугольник</summary>
  /// <param name="rect">Исходный прямоугольник</param>
  /// <returns>Прямоугольник с положительными размерами</returns>
  public static Rectangle NormalRectangle(Rectangle rect)
  {
    return PageControl.NormalRectangle(rect, true, true);
  }

  /// <summary>Нормализовать прямоугольник</summary>
  /// <param name="rect">Исходный прямоугольник</param>
  /// <returns>Прямоугольник с положительными размерами</returns>
  public static Rectangle NormalRectangle(Rectangle rect, bool normalWidth, bool normalHeight)
  {
    if (rect.Width < 0 & normalWidth)
    {
      rect.X += rect.Width;
      rect.Width = -rect.Width;
    }
    if (rect.Height < 0 & normalHeight)
    {
      rect.Y += rect.Height;
      rect.Height = -rect.Height;
    }
    return rect;
  }

  internal void EraseSelectionRectangle(Graphics g)
  {
    bool flag = g == null;
    if (flag)
      g = this.CreateGraphics();
    try
    {
      if (this.selectionRectangle.IsEmpty)
        return;
      RubberBand.DrawXorRectangle(g, this.selectionRectangle, Color.White);
      this.selectionRectangle = Rectangle.Empty;
    }
    finally
    {
      if (flag)
        g.Dispose();
    }
  }

  internal void DrawSelectionRectangle(Graphics g, bool notErase)
  {
    this.DrawSelectionRectangle(g, PageControl.NormalRectangle(this.prevMousePos, this.leftMouseDownPos), notErase);
  }

  internal void DrawSelectionRectangle(Graphics g, Rectangle selRect, bool notErase)
  {
    bool flag = g == null;
    if (flag)
      g = this.CreateGraphics();
    try
    {
      if (!this.selectionRectangle.IsEmpty && !notErase)
        this.EraseSelectionRectangle(g);
      this.selectionRectangle = selRect;
      RubberBand.DrawXorRectangle(g, this.selectionRectangle, Color.White);
    }
    finally
    {
      if (flag)
        g.Dispose();
    }
  }

  internal void DrawDragLine(Graphics g)
  {
    if (this.DragLinePosition == Rectangle.Empty)
      return;
    if (g == null)
      g = this.CreateGraphics();
    Rectangle dragLinePosition = this.DragLinePosition;
    Point pt1 = new Point(dragLinePosition.Left, dragLinePosition.Bottom);
    Point pt2 = new Point(dragLinePosition.Right, dragLinePosition.Bottom);
    Pen pen = new Pen(Color.Black, 4f);
    g.DrawLine(pen, pt1, pt2);
    Point[] points = new Point[3];
    points[0] = pt1;
    int num = 8;
    points[1] = new Point(pt1.X - num, pt1.Y - num);
    points[2] = new Point(pt1.X - num, pt1.Y + num);
    g.FillPolygon(Brushes.DodgerBlue, points);
    this.DragLinePosition = Rectangle.Empty;
  }

  public MenuBar MenuBar => this.menuBar;

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.SuspendLayout();
    this.menuBar.Guid = new Guid("e4443c46-71b0-4e60-ac43-db1201338395");
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Location = new Point(0, 0);
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    this.menuBar.Size = new Size(360, 22);
    this.menuBar.TabIndex = 0;
    this.menuBar.Text = "menuBar";
    this.menuBar.Visible = false;
    this.contextMenuBarItem.CommandName = "contextMenuBarItem";
    this.contextMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
    this.contextMenuBarItem.AfterPopup += new EventHandler(this.contextMenuBarItem_AfterPopup);
    this.contextMenuBarItem.Click += new EventHandler(this.contextMenuBarItem_Click);
    this.Controls.Add((Control) this.menuBar);
    this.Name = nameof (PageControl);
    this.AllowDrop = true;
    this.menuBar.SetPopupMenu((Control) this, (MenuBarItem) this.contextMenuBarItem);
    this.Size = new Size(360, 464);
    this.ResumeLayout(false);
  }

  private void contextMenuBarItem_Click(object sender, EventArgs e)
  {
  }

  /// <summary>Clean up any resources being used</summary>
  protected override void Dispose(bool disposing)
  {
    try
    {
      this.menuBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      Application.LeaveThreadModal -= new EventHandler(this.Application_LeaveThreadModal);
      if (disposing)
      {
        this.contextMenuBarItem.Items.Clear();
        this.menuBar.Items.Clear();
        this.menuBar.SetPopupMenu((Control) this, (MenuBarItem) null);
        if (this.components != null)
          this.components.Dispose();
        this.Document = (ImDocument) null;
      }
      this.ClearReferences();
      base.Dispose(disposing);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public void ClearReferences()
  {
    this.focusedElement = (PageElementUI) null;
    this.elementAtCursor = (PageElementUI) null;
    this.pasteDest = (DocumentTreeNode) null;
    this.pastingNodes = (DocumentTreeNode[]) null;
    if (this._visiblePageElementUIs != null)
    {
      this._visiblePageElementUIs.Clear();
      this._visiblePageElementUIs = (PageElementUICollection) null;
    }
    this.pageControlUI = (PageControlUI) null;
    if (this.doc == null)
      return;
    this.doc = (ImDocument) null;
  }

  public ContextMenuBarItem ContextMenuBarItem
  {
    get => this.contextMenuBarItem;
    set => this.contextMenuBarItem = value;
  }

  public delegate void SetScrollBarValueInvoker(ScrollBar bar, int value);
}
