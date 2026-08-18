// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.Ruler
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

public class Ruler : Control
{
  private int oldMouseCoord;
  private Bitmap bmpLeftIdent;
  private Bitmap bmpRightIdent;
  private Bitmap bmpFirstLineIdent;
  private Bitmap bmpLeftbottomSlider;
  private Bitmap bmpBorder;
  private Bitmap bmpLeftIdentNull;
  private Bitmap bmpRightIdentNull;
  private Bitmap bmpFirstLineIdentNull;
  private Bitmap bmpLeftbottomSliderNull;
  private Bitmap bmpBorderNull;
  private float identFirstLine;
  private float identLeft;
  private float identRight;
  private bool identFirstLineIsNull;
  private bool identLeftIsNull;
  private bool identRightIsNull;
  private float identFirstLineNotNull;
  private float identLeftNotNull;
  private float identRightNotNull;
  private enumTypeDrag typeDrag = enumTypeDrag.tdNone;
  private float oldValue;
  private float oldValue1;
  private List<int> koordsLines;
  private List<Rectangle> koordsValues;
  private List<string> values;
  private float[] borderPositions;
  private float[] borderLeftOffset;
  private float[] borderRightOffset;
  private int[] borderPositionsInPixels;
  /// <summary>разрешить, отменить отрисовку контрола</summary>
  public bool DrawRuler = true;
  private bool showSliders = true;
  private bool showRuler = true;
  private Page page;
  private DocumentControl document;
  private Color colorBorders;
  private enumOrientation orientation;
  private enumScaleMode scaleMode = enumScaleMode.smCentimetres;
  private int index;
  private int mousePosition;
  private Matrix m;
  private bool bordersReadOnly;
  private int suspendRefresh;
  private Ruler.IdentChanged_EventHandler identChanged;
  private Ruler.BorderPositionChanged_EventHandler borderPositionChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public Ruler()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    Stream manifestResourceStream1 = typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.topslider.bmp");
    this.bmpFirstLineIdent = (Bitmap) Image.FromStream(manifestResourceStream1);
    this.bmpRightIdent = (Bitmap) Image.FromStream(manifestResourceStream1);
    this.bmpLeftIdent = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.bottomslider.bmp"));
    this.bmpLeftbottomSlider = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.allslider.bmp"));
    this.bmpBorder = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.bordericon.bmp"));
    Stream manifestResourceStream2 = typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.topslidernull.bmp");
    this.bmpFirstLineIdentNull = (Bitmap) Image.FromStream(manifestResourceStream2);
    this.bmpRightIdentNull = (Bitmap) Image.FromStream(manifestResourceStream2);
    this.bmpLeftIdentNull = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.bottomslidernull.bmp"));
    this.bmpLeftbottomSliderNull = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.allslidernull.bmp"));
    this.bmpBorderNull = (Bitmap) Image.FromStream(typeof (Ruler).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.bordericonnull.bmp"));
    this.bmpRightIdent.MakeTransparent();
    this.bmpFirstLineIdent.MakeTransparent();
    this.bmpLeftIdent.MakeTransparent(this.bmpLeftIdent.GetPixel(1, 1));
    this.bmpRightIdentNull.MakeTransparent();
    this.bmpFirstLineIdentNull.MakeTransparent();
    this.bmpLeftIdentNull.MakeTransparent(this.bmpLeftIdentNull.GetPixel(1, 1));
    if (ImDocumentEditorConfig.Instance == null)
      return;
    ImDocumentEditorConfig.Instance.CoorSystemChanged += new EventHandler(this.Instance_CoorSystemChanged);
    ImDocumentEditorConfig.Instance.CoorSystemPositionChanged += new EventHandler(this.Instance_CoorSystemChanged);
  }

  private void Instance_CoorSystemChanged(object sender, EventArgs e)
  {
    this.RebuildRulerCoords();
    this.Refresh();
  }

  public virtual bool SuspendedRefresh => this.suspendRefresh > 0;

  public void SuspendRefresh() => ++this.suspendRefresh;

  public void ResumeRefresh(bool update)
  {
    if (this.suspendRefresh > 0)
      --this.suspendRefresh;
    if (!(!this.SuspendedRefresh & update))
      return;
    this.Refresh();
  }

  public override void Refresh()
  {
    if (!this.DrawRuler)
      return;
    if (this.InvokeRequired)
      this.Invoke((Delegate) new MethodInvoker(((Control) this).Refresh));
    else
      base.Refresh();
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this.DrawControl(e.Graphics);
  }

  [CustomDescription("Attribute.Document.Model_34")]
  [Category("Ruler")]
  public bool BordersReadOnly
  {
    get => this.bordersReadOnly;
    set => this.bordersReadOnly = value;
  }

  [CustomDescription("Attribute.Document.Model_34")]
  [Category("Ruler")]
  public enumOrientation Orientation
  {
    get => this.orientation;
    set
    {
      this.orientation = value;
      this.Invalidate();
    }
  }

  /// <summary>Координаты позиций границ</summary>
  [CustomDescription("Attribute.Document.Model_35")]
  [Category("Borders")]
  public float[] BorderPositions => this.borderPositions;

  /// <summary>Отступ слева от границы</summary>
  [CustomDescription("Attribute.Document.Model_36")]
  [Category("Borders")]
  public float[] BorderLeftOffset => this.borderLeftOffset;

  /// <summary>Отступ справа от границы</summary>
  [CustomDescription("Attribute.Document.Model_37")]
  [Category("Borders")]
  public float[] BorderRightOffset => this.borderRightOffset;

  /// <summary>
  /// Текущий индекс интервала в котором отображаются отступы
  /// </summary>
  [CustomDescription("Attribute.Document.Model_38")]
  [Category("Borders")]
  public int Index
  {
    get => this.index;
    set => this.index = value;
  }

  /// <summary>Значение левой границы текущего элемента</summary>
  [Browsable(false)]
  [CustomDescription("Attribute.Document.Model_39")]
  public float LeftBorder
  {
    get
    {
      return this.borderPositions != null && this.index > -1 && this.index < this.borderPositions.Length ? this.borderPositions[this.index] : 0.0f;
    }
  }

  [Browsable(false)]
  [CustomDescription("Attribute.Document.Model_40")]
  public float RightBorder
  {
    get
    {
      return this.borderPositions != null && this.index < this.borderPositions.Length - 1 ? this.borderPositions[this.index + 1] : this.LeftBorder + 1f;
    }
  }

  [CustomDescription("Attribute.Document.Model_41")]
  [Category("Borders")]
  public Color BordersColor
  {
    get => this.colorBorders;
    set => this.colorBorders = value;
  }

  [CustomDescription("Attribute.Document.Model_42")]
  [Category("Sliders")]
  public bool ShowSliders
  {
    get => this.showSliders;
    set => this.showSliders = value;
  }

  [CustomDescription("Attribute.Document.Model_43")]
  [Category("Sliders")]
  public float? IdentFirstLine
  {
    get => this.identFirstLineIsNull ? new float?() : new float?(this.identFirstLine);
    set
    {
      if (!value.HasValue)
        return;
      this.identFirstLineIsNull = false;
      this.identFirstLine = (float) Math.Round((double) value.Value, 2);
      if (this.borderRightOffset != null && this.borderRightOffset.Length != 0)
        this.identFirstLineNotNull = this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder;
      else
        this.identFirstLineNotNull = this.identFirstLine + this.LeftBorder;
    }
  }

  [CustomDescription("Attribute.Document.Model_44")]
  [Category("Sliders")]
  public float? IdentRight
  {
    get => this.identRightIsNull ? new float?() : new float?(this.identRight);
    set
    {
      if (!value.HasValue)
        return;
      this.identRight = (float) Math.Round((double) value.Value, 2);
      this.identRightIsNull = false;
      if (this.borderRightOffset != null && this.borderRightOffset.Length > 1)
        this.identRightNotNull = this.RightBorder - this.borderLeftOffset[1] - this.identRight;
      else
        this.identRightNotNull = this.RightBorder - this.identRight;
    }
  }

  [CustomDescription("Attribute.Document.Model_45")]
  [Category("Sliders")]
  public float? IdentLeft
  {
    get => this.identLeftIsNull ? new float?() : new float?(this.identLeft);
    set
    {
      if (!value.HasValue)
        return;
      this.identLeft = (float) Math.Round((double) value.Value, 2);
      this.identLeftIsNull = false;
      if (this.borderRightOffset != null && this.borderRightOffset.Length != 0)
        this.identLeftNotNull = this.identLeft + this.borderRightOffset[0] + this.LeftBorder;
      else
        this.identLeftNotNull = this.identLeft + this.LeftBorder;
    }
  }

  [CustomDescription("Attribute.Document.Model_46")]
  [Category("Ruler")]
  public enumScaleMode ScaleMode
  {
    get => this.scaleMode;
    set
    {
      int scaleMode = (int) this.scaleMode;
      this.scaleMode = value;
    }
  }

  [CustomDescription("Attribute.Document.Model_47")]
  [Category("Ruler")]
  [Browsable(false)]
  public int MajorInterval
  {
    get
    {
      int x = 10;
      int num1 = this.page.PageUI.ConvertUserXToPageControl((float) x) - this.page.PageUI.ConvertUserXToPageControl(0.0f);
      int[] numArray = new int[3]{ 2, 5, 10 };
      int num2 = 0;
      while (Math.Abs(num1) < 28)
      {
        x = numArray[num2 % 3] * (int) Math.Pow(10.0, (double) (num2 / 3));
        num1 = this.page.PageUI.ConvertUserXToPageControl((float) x) - this.page.PageUI.ConvertUserXToPageControl(0.0f);
        ++num2;
      }
      return x;
    }
  }

  [CustomDescription("Attribute.Document.Model_48")]
  [Category("Ruler")]
  public int StartRulerPosition
  {
    get
    {
      return this.page == null || this.document == null ? 0 : (this.orientation != enumOrientation.orHorizontal ? this.Page.PageUI.Bounds.Top : this.Page.PageUI.Bounds.Left);
    }
  }

  [CustomDescription("Attribute.Document.Model_49")]
  [Category("Ruler")]
  [Browsable(false)]
  public DocumentControl Document
  {
    get => this.document;
    set
    {
      if (this.document != null)
      {
        this.document.HScrollBar.ValueChanged += new EventHandler(this.HScrollBar_ValueChanged);
        this.document.VScrollBar.ValueChanged += new EventHandler(this.VScrollBar_ValueChanged);
      }
      this.document = value;
      if (this.document == null)
        return;
      this.document.HScrollBar.ValueChanged += new EventHandler(this.HScrollBar_ValueChanged);
      this.document.VScrollBar.ValueChanged += new EventHandler(this.VScrollBar_ValueChanged);
    }
  }

  private void VScrollBar_ValueChanged(object sender, EventArgs e)
  {
    if (this.Orientation != enumOrientation.orVertical)
      return;
    this.RebuildRulerCoords();
    this.Refresh();
  }

  private void HScrollBar_ValueChanged(object sender, EventArgs e)
  {
    if (this.Orientation != enumOrientation.orHorizontal)
      return;
    this.RebuildRulerCoords();
    this.Refresh();
  }

  [CustomDescription("Attribute.Document.Model_50")]
  [Category("Ruler")]
  [Browsable(false)]
  public Page Page
  {
    get => this.page;
    set
    {
      if (this.page != null)
        this.page.UIGeometryChanged -= new UIGeometryChanged_EventHandler(this.Page_UIGeometryChanged);
      this.page = value;
      if (this.page == null)
        return;
      this.page.UIGeometryChanged += new UIGeometryChanged_EventHandler(this.Page_UIGeometryChanged);
    }
  }

  private void Page_UIGeometryChanged(object sender, UIGeometryChanged_EventArgs e)
  {
    this.RebuildRulerCoords();
    this.Refresh();
  }

  private void page_Scroll(object sender, ScrollEventArgs e)
  {
  }

  private void page_Resize(object sender, EventArgs e)
  {
  }

  [CustomDescription("Attribute.Document.Model_51")]
  [Category("Ruler")]
  public int EndRulerPosition
  {
    get
    {
      return this.page == null || this.document == null ? this.Width : (this.orientation != enumOrientation.orHorizontal ? this.Page.PageUI.Bounds.Bottom : this.Page.PageUI.Bounds.Right);
    }
  }

  [CustomDescription("Attribute.Document.Model_53")]
  [Category("Ruler")]
  public bool ShowRuler
  {
    get => this.showRuler;
    set => this.showRuler = value;
  }

  [CustomDescription("Attribute.Document.Model_54")]
  [Category("Ruler")]
  [Browsable(false)]
  public int MouseLocation => this.mousePosition;

  private int DefaultMajorInterval(enumScaleMode iScaleMode)
  {
    int num = 10;
    switch (iScaleMode)
    {
      case enumScaleMode.smPoints:
        num = 72;
        break;
      case enumScaleMode.smCentimetres:
        num = 1;
        break;
      case enumScaleMode.smInches:
        num = 1;
        break;
      case enumScaleMode.smMillimetres:
        num = 10;
        break;
    }
    return num;
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (this.Orientation == enumOrientation.orHorizontal)
      this.oldMouseCoord = e.X;
    if (this.Orientation == enumOrientation.orVertical)
      this.oldMouseCoord = e.Y;
    this.typeDrag = enumTypeDrag.tdNone;
    if (e.Button != MouseButtons.Left)
      return;
    if (this.Orientation == enumOrientation.orHorizontal)
      this.oldMouseCoord = e.X - 1;
    if (this.Orientation == enumOrientation.orVertical)
      this.oldMouseCoord = e.Y - 1;
    this.OnMouseMove(e);
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    if (this.page == null)
      return;
    Point point = new Point(e.X, e.Y);
    int startRulerPosition = this.StartRulerPosition;
    int endRulerPosition = this.EndRulerPosition;
    this.mousePosition = this.Orientation != enumOrientation.orHorizontal ? point.Y - startRulerPosition : point.X - startRulerPosition;
    enumTypeDrag enumTypeDrag = enumTypeDrag.tdNone;
    base.OnMouseMove(e);
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      int num1 = this.page.ConvertWorldXToPixel(this.identFirstLineNotNull) + startRulerPosition;
      int num2 = this.page.ConvertWorldXToPixel(this.identLeftNotNull) + startRulerPosition;
      int num3 = this.page.ConvertWorldXToPixel(this.identRightNotNull) + startRulerPosition;
      if (this.typeDrag == enumTypeDrag.tdNone && e.X > num1 - 4 && point.X < num1 + 4 && point.Y > 2 && e.Y < 11)
        enumTypeDrag = enumTypeDrag.tdIdentFirstLine;
      if (this.typeDrag == enumTypeDrag.tdNone && point.X > num2 - 4 && point.X < num2 + 4 && point.Y > 10 && e.Y < 18)
        enumTypeDrag = enumTypeDrag.tdIdentLeft;
      if (this.typeDrag == enumTypeDrag.tdNone && point.X > num3 - 4 && point.X < num3 + 4 && point.Y > 2 && point.Y < 11)
        enumTypeDrag = enumTypeDrag.tdIdentRight;
      if (this.typeDrag == enumTypeDrag.tdNone && point.X > num2 - 4 && point.X < num2 + 4 && e.Y > 17 && point.Y < 22)
        enumTypeDrag = enumTypeDrag.tdIdentAll;
      if (this.typeDrag == enumTypeDrag.tdNone && this.Cursor == Cursors.SizeWE)
        enumTypeDrag = Control.ModifierKeys != Keys.Control ? enumTypeDrag.tdBordersOne : enumTypeDrag.tdBordersAll;
      if (e.Button == MouseButtons.Left)
      {
        if (this.oldMouseCoord == e.X)
          return;
        this.oldMouseCoord = e.X;
        this.document.GetPageControlViewRectangle();
        if (this.typeDrag == enumTypeDrag.tdNone)
        {
          this.typeDrag = enumTypeDrag;
          if (this.typeDrag != enumTypeDrag.tdNone)
            this.BeginMove();
          switch (this.typeDrag)
          {
            case enumTypeDrag.tdIdentFirstLine:
              this.oldValue = this.identFirstLineNotNull;
              break;
            case enumTypeDrag.tdIdentLeft:
              this.oldValue = this.identLeftNotNull;
              break;
            case enumTypeDrag.tdIdentRight:
              this.oldValue = this.identRightNotNull;
              break;
            case enumTypeDrag.tdIdentAll:
              this.oldValue = this.identLeftNotNull;
              this.oldValue1 = this.identFirstLineNotNull;
              break;
          }
          this.Select();
        }
        switch (this.typeDrag)
        {
          case enumTypeDrag.tdIdentFirstLine:
            int x1 = point.X;
            if (point.X > num3)
              x1 = num3;
            if (point.X < startRulerPosition)
              x1 = startRulerPosition;
            this.mousePosition = x1;
            this.identFirstLineNotNull = this.page.PageUI.ConvertPixelXToWorld(x1, true, this.m);
            if ((double) this.identFirstLineNotNull > (double) this.identRightNotNull)
              this.identFirstLineNotNull = this.identRightNotNull;
            this.Refresh();
            break;
          case enumTypeDrag.tdIdentLeft:
            int x2 = point.X;
            if (point.X > num3)
              x2 = num3;
            if (point.X < startRulerPosition)
              x2 = startRulerPosition;
            this.mousePosition = x2;
            this.identLeftNotNull = this.page.PageUI.ConvertPixelXToWorld(x2, true, this.m);
            if ((double) this.identLeftNotNull > (double) this.identRightNotNull)
              this.identLeftNotNull = this.identRightNotNull;
            this.Refresh();
            break;
          case enumTypeDrag.tdIdentRight:
            int x3 = num2;
            if (num1 > num2)
              x3 = num1;
            if (point.X >= x3)
              x3 = point.X;
            if (point.X > endRulerPosition)
              x3 = endRulerPosition;
            this.mousePosition = x3;
            this.identRightNotNull = this.page.PageUI.ConvertPixelXToWorld(x3, true, this.m);
            if ((double) this.identRightNotNull < (double) this.identFirstLineNotNull)
              this.identRightNotNull = this.identFirstLineNotNull;
            if ((double) this.identRightNotNull < (double) this.identLeftNotNull)
              this.identRightNotNull = this.identLeftNotNull;
            this.Refresh();
            break;
          case enumTypeDrag.tdIdentAll:
            int num4 = num1 - num2;
            int x4 = point.X;
            if (num4 < 0)
            {
              if (point.X > num3)
                x4 = num3;
              if (point.X + num4 < startRulerPosition)
                x4 = startRulerPosition - num4;
            }
            else
            {
              if (point.X + num4 > num3)
                x4 = num3 - num4;
              if (point.X + num4 < startRulerPosition)
                x4 = startRulerPosition;
            }
            this.mousePosition = x4;
            float num5 = this.identFirstLineNotNull - this.identLeftNotNull;
            this.identLeftNotNull = this.page.PageUI.ConvertPixelXToWorld(x4, true, this.m);
            if ((double) this.identLeftNotNull > (double) this.identRightNotNull)
              this.identLeftNotNull = this.identRightNotNull;
            this.identFirstLineNotNull = this.identLeftNotNull + num5;
            if ((double) this.identFirstLineNotNull > (double) this.identRightNotNull)
              this.identFirstLineNotNull = this.identRightNotNull;
            this.identLeftNotNull = this.identFirstLineNotNull - num5;
            this.Refresh();
            break;
          case enumTypeDrag.tdBordersAll:
            int oldValue1_1 = (int) this.oldValue1;
            float num6 = oldValue1_1 == 0 ? 0.0f : this.borderPositions[oldValue1_1 - 1] + this.borderRightOffset[oldValue1_1 - 1] + this.borderLeftOffset[oldValue1_1];
            float width = this.page.Size.Width;
            int x5 = point.X;
            float num7 = this.page.PageUI.ConvertPixelXToWorld(x5 - startRulerPosition, true, this.m);
            if ((double) num7 < (double) num6)
              num7 = num6;
            if ((double) num7 > (double) width)
              num7 = width;
            this.mousePosition = x5 - startRulerPosition;
            float[] numArray1 = (float[]) this.borderPositions.Clone();
            for (int index = oldValue1_1 + 1; index < this.borderPositions.Length; ++index)
            {
              float num8 = this.borderPositions[index] - this.borderPositions[oldValue1_1];
              this.borderPositions[index] = this.page.PageUI.ConvertPixelXToWorld(x5 - startRulerPosition, true, this.m) + num8;
            }
            this.borderPositions[oldValue1_1] = num7;
            this.identLeftNotNull = this.identLeft + this.borderRightOffset[0] + this.LeftBorder;
            this.identRightNotNull = this.RightBorder - this.borderLeftOffset[1] - this.identRight;
            this.identFirstLineNotNull = this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder;
            this.Refresh();
            break;
          case enumTypeDrag.tdBordersOne:
            int oldValue1_2 = (int) this.oldValue1;
            float num9 = 0.0f;
            float num10 = this.page.Size.Width;
            if (oldValue1_2 != 0)
              num9 = this.borderPositions[oldValue1_2 - 1] + this.borderRightOffset[oldValue1_2 - 1] + this.borderLeftOffset[oldValue1_2];
            if (oldValue1_2 != this.borderPositions.Length - 1)
              num10 = this.borderPositions[oldValue1_2 + 1] - this.borderLeftOffset[oldValue1_2] - this.borderRightOffset[oldValue1_2 + 1];
            int x6 = point.X;
            float num11 = this.page.PageUI.ConvertPixelXToWorld(x6, true, this.m);
            if ((double) num11 < (double) num9)
              num11 = num9;
            if ((double) num11 > (double) num10)
              num11 = num10;
            this.mousePosition = x6 - startRulerPosition;
            float[] numArray2 = (float[]) this.borderPositions.Clone();
            this.borderPositions[oldValue1_2] = num11;
            float num12 = this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder;
            float num13 = this.identLeft + this.borderRightOffset[0] + this.LeftBorder;
            float num14 = this.RightBorder - this.borderLeftOffset[1] - this.identRight;
            this.identLeftNotNull = num13;
            this.identRightNotNull = num14;
            this.identFirstLineNotNull = num12;
            this.Refresh();
            break;
        }
        this.DrawOnPage();
        return;
      }
      if (!this.bordersReadOnly && this.borderPositions != null && this.typeDrag == enumTypeDrag.tdNone && (enumTypeDrag == enumTypeDrag.tdNone || enumTypeDrag == enumTypeDrag.tdBordersOne || enumTypeDrag == enumTypeDrag.tdBordersAll))
      {
        bool flag = false;
        for (int index = 0; index < this.borderPositions.Length; ++index)
        {
          int num15 = this.page.ConvertWorldXToPixel(this.borderPositions[index]) + startRulerPosition;
          if (point.X < num15 + 4 && point.X > num15 - 4)
          {
            this.oldValue = this.borderPositions[index];
            this.oldValue1 = (float) index;
            flag = true;
          }
        }
        if (flag)
          this.Cursor = Cursors.SizeWE;
        else
          this.Cursor = Cursors.Default;
      }
      else
        this.Cursor = Cursors.Default;
    }
    if (this.Orientation != enumOrientation.orVertical)
      return;
    this.page.ConvertWorldYToPixel(this.identFirstLineNotNull);
    this.page.ConvertWorldYToPixel(this.identLeftNotNull);
    this.page.ConvertWorldYToPixel(this.identRightNotNull);
    if (enumTypeDrag == enumTypeDrag.tdNone && this.Cursor == Cursors.SizeNS)
      enumTypeDrag = enumTypeDrag.tdBordersAll;
    if (e.Button == MouseButtons.Left)
    {
      if (this.oldMouseCoord == e.Y)
        return;
      this.oldMouseCoord = e.Y;
      this.document.GetPageControlViewRectangle();
      this.typeDrag = enumTypeDrag;
      if (this.typeDrag != enumTypeDrag.tdNone)
        this.BeginMove();
      if (this.typeDrag == enumTypeDrag.tdBordersAll)
      {
        int oldValue1 = (int) this.oldValue1;
        float num16 = 0.0f;
        float height = this.page.Size.Height;
        if (oldValue1 != 0)
          num16 = this.borderPositions[oldValue1 - 1] + this.borderRightOffset[oldValue1 - 1] + this.borderLeftOffset[oldValue1];
        int y = point.Y;
        float num17 = this.page.PageUI.ConvertPixelYToWorld(y, true, this.m);
        if ((double) num17 < (double) num16)
          num17 = num16;
        if ((double) num17 > (double) height)
          num17 = height;
        this.mousePosition = y;
        float[] numArray = (float[]) this.borderPositions.Clone();
        if (oldValue1 > 0)
        {
          for (int index = oldValue1 + 1; index < this.borderPositions.Length; ++index)
          {
            float num18 = this.borderPositions[index] - this.borderPositions[oldValue1];
            this.borderPositions[index] = num17 + num18;
          }
        }
        this.borderPositions[oldValue1] = num17;
        double identFirstLine = (double) this.identFirstLine;
        double num19 = (double) this.borderRightOffset[0];
        double leftBorder1 = (double) this.LeftBorder;
        double identLeft = (double) this.identLeft;
        double num20 = (double) this.borderRightOffset[0];
        double leftBorder2 = (double) this.LeftBorder;
        double rightBorder = (double) this.RightBorder;
        double num21 = (double) this.borderLeftOffset[1];
        double identRight = (double) this.identRight;
        this.Refresh();
      }
      if (this.typeDrag != enumTypeDrag.tdBordersAll && this.typeDrag != enumTypeDrag.tdBordersOne)
        return;
      this.DrawOnPage();
    }
    else if (!this.bordersReadOnly && this.borderPositions != null && this.typeDrag == enumTypeDrag.tdNone && (enumTypeDrag == enumTypeDrag.tdNone || enumTypeDrag == enumTypeDrag.tdBordersOne || enumTypeDrag == enumTypeDrag.tdBordersAll))
    {
      bool flag = false;
      for (int index = 0; index < this.borderPositions.Length; ++index)
      {
        int num = this.page.ConvertWorldYToPixel(this.borderPositions[index]) + startRulerPosition;
        if (point.Y < num + 4 && point.Y > num - 4)
        {
          this.oldValue = this.borderPositions[index];
          this.oldValue1 = (float) index;
          flag = true;
        }
      }
      if (flag)
      {
        if (this.Orientation == enumOrientation.orHorizontal)
          this.Cursor = Cursors.SizeWE;
        else
          this.Cursor = Cursors.SizeNS;
      }
      else
        this.Cursor = Cursors.Default;
    }
    else
      this.Cursor = Cursors.Default;
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RebuildRulerCoords();
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    switch (this.typeDrag)
    {
      case enumTypeDrag.tdIdentFirstLine:
        this.IdentFirstLine = new float?(this.identFirstLineNotNull - this.LeftBorder - this.borderRightOffset[0]);
        this.OnIdentChanged(new IdentChanged_EventArgs(this.typeDrag));
        break;
      case enumTypeDrag.tdIdentLeft:
        this.IdentLeft = new float?(this.identLeftNotNull - this.LeftBorder - this.borderRightOffset[0]);
        this.OnIdentChanged(new IdentChanged_EventArgs(this.typeDrag));
        break;
      case enumTypeDrag.tdIdentRight:
        this.IdentRight = new float?(this.RightBorder - this.borderLeftOffset[1] - this.identRightNotNull);
        this.OnIdentChanged(new IdentChanged_EventArgs(this.typeDrag));
        break;
      case enumTypeDrag.tdIdentAll:
        this.IdentLeft = new float?(this.identLeftNotNull - this.LeftBorder - this.borderRightOffset[0]);
        this.IdentFirstLine = new float?(this.identFirstLineNotNull - this.LeftBorder - this.borderRightOffset[0]);
        this.OnIdentChanged(new IdentChanged_EventArgs(this.typeDrag));
        break;
      case enumTypeDrag.tdBordersAll:
        this.OnBorderPositionChanged(new BorderPositionChanged_EventArgs(this.typeDrag, (int) this.oldValue1, this.oldValue));
        break;
      case enumTypeDrag.tdBordersOne:
        this.OnBorderPositionChanged(new BorderPositionChanged_EventArgs(this.typeDrag, (int) this.oldValue1, this.oldValue));
        break;
    }
    if (this.typeDrag != enumTypeDrag.tdNone)
    {
      this.typeDrag = enumTypeDrag.tdNone;
      this.EndMove();
    }
    this.Refresh();
  }

  protected override void OnLostFocus(EventArgs e)
  {
    base.OnLostFocus(e);
    this.CancelMove();
  }

  private void CancelMove()
  {
    switch (this.typeDrag)
    {
      case enumTypeDrag.tdIdentFirstLine:
        this.identFirstLineNotNull = this.oldValue;
        break;
      case enumTypeDrag.tdIdentLeft:
        this.identLeftNotNull = this.oldValue;
        break;
      case enumTypeDrag.tdIdentRight:
        this.identRightNotNull = this.oldValue;
        break;
      case enumTypeDrag.tdIdentAll:
        this.identLeftNotNull = this.oldValue;
        this.identFirstLineNotNull = this.oldValue1;
        break;
      case enumTypeDrag.tdBordersAll:
        if (this.Orientation == enumOrientation.orHorizontal)
        {
          float num1 = this.borderPositions[(int) this.oldValue1] - this.oldValue;
          for (int oldValue1 = (int) this.oldValue1; oldValue1 < this.borderPositions.Length; ++oldValue1)
            this.borderPositions[oldValue1] = this.borderPositions[oldValue1] - num1;
          float num2 = this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder;
          float num3 = this.identLeft + this.borderRightOffset[0] + this.LeftBorder;
          float num4 = this.RightBorder - this.borderLeftOffset[1] - this.identRight;
          this.identLeftNotNull = num3;
          this.identRightNotNull = num4;
          this.identFirstLineNotNull = num2;
          break;
        }
        float num5 = this.borderPositions[(int) this.oldValue1] - this.oldValue;
        for (int oldValue1 = (int) this.oldValue1; oldValue1 < this.borderPositions.Length; ++oldValue1)
          this.borderPositions[oldValue1] = this.borderPositions[oldValue1] - num5;
        break;
      case enumTypeDrag.tdBordersOne:
        this.borderPositions[(int) this.oldValue1] = this.oldValue;
        float num6 = this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder;
        float num7 = this.identLeft + this.borderRightOffset[0] + this.LeftBorder;
        float num8 = this.RightBorder - this.borderLeftOffset[1] - this.identRight;
        this.identLeftNotNull = num7;
        this.identRightNotNull = num8;
        this.identFirstLineNotNull = num6;
        break;
    }
    if (this.typeDrag == enumTypeDrag.tdNone)
      return;
    this.typeDrag = enumTypeDrag.tdNone;
    this.EndMove();
    this.Cursor = Cursors.Default;
    this.Refresh();
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Escape)
      return base.ProcessCmdKey(ref msg, keyData);
    this.CancelMove();
    return true;
  }

  /// <summary>Отрисовка данных на странице</summary>
  public void DrawOnPage()
  {
    if (this.page == null)
      return;
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      if (this.typeDrag != enumTypeDrag.tdNone)
      {
        this.Document.PageControl.CreateGraphics();
        int oldValue1 = (int) this.oldValue1;
        if (this.typeDrag == enumTypeDrag.tdBordersAll || this.typeDrag == enumTypeDrag.tdBordersOne)
        {
          if (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
          {
            float? leftCellSize = oldValue1 <= 0 ? new float?() : new float?(this.page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1] - this.borderPositions[oldValue1 - 1], this.m));
            float? rightCellSize = oldValue1 >= this.borderPositions.Length - 1 ? new float?() : new float?(this.page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1 + 1] - this.borderPositions[oldValue1], this.m));
            this.Document.PageControl.SetBarValues(new float?(this.page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1], this.m)), new float?(this.page.PageUI.ConvertInternalDistanceToUser(this.page.Size.Width - this.borderPositions[oldValue1], this.m)), leftCellSize, rightCellSize);
          }
          else
            this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
          this.Document.PageControl.DrawLine = true;
        }
        else
        {
          this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
          this.Document.PageControl.DrawLine = false;
        }
        this.Document.PageControl.IsPopupBarHorizontal = true;
        int x = 0;
        if (this.typeDrag == enumTypeDrag.tdBordersAll || this.typeDrag == enumTypeDrag.tdBordersOne)
          x = this.borderPositionsInPixels[oldValue1 + 1];
        if (this.typeDrag == enumTypeDrag.tdIdentAll || this.typeDrag == enumTypeDrag.tdIdentLeft)
          x = this.page.ConvertWorldXToPixel(this.identLeftNotNull);
        if (this.typeDrag == enumTypeDrag.tdIdentRight)
          x = this.page.ConvertWorldXToPixel(this.identRightNotNull);
        if (this.typeDrag == enumTypeDrag.tdIdentFirstLine)
          x = this.page.ConvertWorldXToPixel(this.identFirstLineNotNull);
        this.Document.PageControl.PopupBarPosition = new Point(x, 0);
        this.DrawRuler = false;
        this.Document.PageControl.PreparePopupBar();
        this.Document.PageControl.Invalidate(this.Document.PageControl.RegionForInvalidate);
        this.Document.PageControl.Update();
        this.DrawRuler = true;
      }
      else
      {
        Point popupBarPosition = this.Document.PageControl.PopupBarPosition;
        if (this.Document.PageControl.IsPopupBarHorizontal)
        {
          this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
          this.Document.PageControl.DrawLine = false;
          this.DrawRuler = false;
          this.Document.PageControl.PreparePopupBar();
          this.Document.PageControl.Invalidate(this.Document.PageControl.RegionForInvalidate);
          this.Document.PageControl.Update();
          this.DrawRuler = true;
        }
      }
    }
    if (this.Orientation != enumOrientation.orVertical)
      return;
    if (this.typeDrag != enumTypeDrag.tdNone)
    {
      this.Document.PageControl.CreateGraphics();
      int oldValue1 = (int) this.oldValue1;
      if (this.typeDrag == enumTypeDrag.tdBordersAll || this.typeDrag == enumTypeDrag.tdBordersOne)
      {
        if (ImDocumentEditorConfig.Instance.ShowPopupBarOnResize)
        {
          float? leftCellSize = oldValue1 <= 0 ? new float?() : new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1] - this.borderPositions[oldValue1 - 1], this.m));
          float? rightCellSize = oldValue1 >= this.borderPositions.Length - 1 ? new float?() : new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1 + 1] - this.borderPositions[oldValue1], this.m));
          this.Document.PageControl.SetBarValues(new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.borderPositions[oldValue1], this.m)), new float?(this.Page.PageUI.ConvertInternalDistanceToUser(this.page.Size.Height - this.borderPositions[oldValue1], this.m)), leftCellSize, rightCellSize);
        }
        else
          this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
        this.Document.PageControl.DrawLine = true;
      }
      else
      {
        this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
        this.Document.PageControl.DrawLine = false;
      }
      this.Document.PageControl.DrawLine = true;
      this.Document.PageControl.IsPopupBarHorizontal = false;
      this.Document.PageControl.PopupBarPosition = new Point(0, this.borderPositionsInPixels[oldValue1 + 1]);
      this.DrawRuler = false;
      this.Document.PageControl.PreparePopupBar();
      this.Document.PageControl.Invalidate(this.Document.PageControl.RegionForInvalidate);
      this.Document.PageControl.Update();
      this.DrawRuler = true;
    }
    else
    {
      Point popupBarPosition = this.Document.PageControl.PopupBarPosition;
      if (this.Document.PageControl.IsPopupBarHorizontal)
        return;
      this.Document.PageControl.SetBarValues(new float?(), new float?(), new float?(), new float?());
      this.Document.PageControl.DrawLine = false;
      this.DrawRuler = false;
      this.Document.PageControl.PreparePopupBar();
      this.Document.PageControl.Invalidate(this.Document.PageControl.RegionForInvalidate);
      this.Document.PageControl.Update();
      this.DrawRuler = true;
    }
  }

  /// <summary>Кеширование значений линейки</summary>
  /// <param name="graphics"></param>
  private void PrepareDrawControl(Graphics graphics)
  {
    if (!this.Visible || this.Width < 1 || this.Height < 1)
      return;
    float num1 = 10f;
    if (this.page == null)
      return;
    this.m = this.page.PageUI.GetUserCoorMatrix();
    Color blueViolet = Color.BlueViolet;
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      int x1 = this.MajorInterval;
      int num2 = this.Height / 2;
      int startRulerPosition = this.StartRulerPosition;
      int endRulerPosition = this.EndRulerPosition;
      float num3 = this.page.PageUI.ConvertPixelXToUser(startRulerPosition, this.m);
      int x2 = x1 * (int) Math.Floor((double) (num3 / (float) x1));
      int x3 = x1 * (int) Math.Ceiling((double) (num3 / (float) x1));
      int x4 = this.page.PageUI.ConvertUserXToPageControl((float) x2) >= this.page.PageUI.ConvertUserXToPageControl((float) x3) ? x3 : x2;
      int num4 = this.page.PageUI.ConvertUserXToPageControl((float) x1, this.m) - this.page.PageUI.ConvertUserXToPageControl(0.0f, this.m);
      if (num4 < 0)
        x1 = -x1;
      int num5 = Math.Abs(num4);
      int iPosition = this.page.PageUI.ConvertUserXToPageControl((float) x4, this.m);
      if (this.koordsLines == null)
        this.koordsLines = new List<int>();
      if (this.koordsValues == null)
        this.koordsValues = new List<Rectangle>();
      if (this.values == null)
        this.values = new List<string>();
      this.values.Clear();
      this.koordsValues.Clear();
      this.koordsLines.Clear();
      int num6 = endRulerPosition;
      if (endRulerPosition > this.Width)
        num6 = this.Width;
      int iValue = (int) ((double) x4 / (double) num1);
      while (iPosition < num6)
      {
        if (iPosition >= startRulerPosition - num5)
        {
          this.koordsLines.Add(iPosition);
          this.PrepareDrawValue(graphics, (float) iValue, iPosition, 0);
          iPosition += num5;
          iValue += (int) ((double) x1 / (double) num1);
        }
      }
    }
    if (this.Orientation != enumOrientation.orVertical)
      return;
    int y1 = this.MajorInterval;
    int num7 = this.Width / 2;
    int startRulerPosition1 = this.StartRulerPosition;
    int endRulerPosition1 = this.EndRulerPosition;
    float num8 = this.page.PageUI.ConvertPixelYToUser(startRulerPosition1, this.m);
    int y2 = y1 * (int) Math.Floor((double) (num8 / (float) y1));
    int y3 = y1 * (int) Math.Ceiling((double) (num8 / (float) y1));
    int y4 = this.page.PageUI.ConvertUserYToPageControl((float) y2) >= this.page.PageUI.ConvertUserYToPageControl((float) y3) ? y3 : y2;
    int num9 = this.page.PageUI.ConvertUserYToPageControl((float) y1, this.m) - this.page.PageUI.ConvertUserYToPageControl(0.0f, this.m);
    if (num9 < 0)
      y1 = -y1;
    int num10 = Math.Abs(num9);
    int iPosition1 = this.page.PageUI.ConvertUserYToPageControl((float) y4, this.m);
    if (this.koordsLines == null)
      this.koordsLines = new List<int>();
    if (this.koordsValues == null)
      this.koordsValues = new List<Rectangle>();
    if (this.values == null)
      this.values = new List<string>();
    this.values.Clear();
    this.koordsValues.Clear();
    this.koordsLines.Clear();
    int num11 = endRulerPosition1;
    if (endRulerPosition1 > this.Height)
      num11 = this.Height;
    int iValue1 = (int) ((double) y4 / (double) num1);
    while (iPosition1 < num11)
    {
      if (iPosition1 >= startRulerPosition1 - num10)
      {
        this.koordsLines.Add(iPosition1);
        this.PrepareDrawValue(graphics, (float) iValue1, iPosition1, 0);
        iPosition1 += num10;
        iValue1 += (int) ((double) y1 / (double) num1);
      }
    }
  }

  public void SetBordersPositions(float[] value, float[] leftOffset, float[] rightOffset)
  {
    if (value != null && value.Length > 1 && value.Length == leftOffset.Length && value.Length == rightOffset.Length)
    {
      this.borderPositions = (float[]) value.Clone();
      this.borderLeftOffset = (float[]) leftOffset.Clone();
      this.borderRightOffset = (float[]) rightOffset.Clone();
      Array.Sort<float>(this.borderPositions);
    }
    else
    {
      this.borderPositions = (float[]) null;
      this.borderLeftOffset = (float[]) null;
      this.borderRightOffset = (float[]) null;
    }
  }

  private void DrawControl(Graphics graphics)
  {
    using (SolidBrush solidBrush = new SolidBrush(SystemColors.InactiveCaptionText))
      graphics.FillRectangle((Brush) solidBrush, 0, 0, this.Width, this.Height);
    if (!this.Visible || this.Page == null || this.Page.PageUI == null || this.Page.PageControl == null || this.Width < 1 || this.Height < 1 || this.page == null || this.koordsLines == null)
      return;
    Color blueViolet = Color.BlueViolet;
    int num1;
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      int num2 = Math.Abs(this.page.PageUI.ConvertUserXToPageControl((float) this.MajorInterval, this.m) - this.page.PageUI.ConvertUserXToPageControl(0.0f, this.m));
      int num3 = this.Height / 2;
      int startRulerPosition = this.StartRulerPosition;
      int endRulerPosition = this.EndRulerPosition;
      using (SolidBrush solidBrush = new SolidBrush(Color.White))
        graphics.FillRectangle((Brush) solidBrush, startRulerPosition, num3 - 6, endRulerPosition - startRulerPosition, 13);
      if (this.borderPositions != null && this.borderPositions.Length > 1)
      {
        this.borderPositionsInPixels = new int[this.borderPositions.Length + 2];
        for (int index = 1; index < this.borderPositions.Length + 1; ++index)
          this.borderPositionsInPixels[index] = this.page.ConvertWorldXToPixel(this.borderPositions[index - 1]) + startRulerPosition;
        this.borderPositionsInPixels[0] = startRulerPosition;
        this.borderPositionsInPixels[this.borderPositionsInPixels.Length - 1] = endRulerPosition;
        using (SolidBrush solidBrush = new SolidBrush(this.colorBorders))
        {
          graphics.FillRectangle((Brush) solidBrush, startRulerPosition, num3 - 6, this.page.ConvertWorldXToPixel(this.borderPositions[0] + this.borderRightOffset[0]) + startRulerPosition - startRulerPosition + 1, 13);
          int x = this.page.ConvertWorldXToPixel(this.borderPositions[this.borderPositions.Length - 1] - this.borderLeftOffset[this.borderPositions.Length - 1]) + startRulerPosition - 1;
          graphics.FillRectangle((Brush) solidBrush, x, num3 - 6, endRulerPosition - x, 13);
          for (int index = 1; index < this.borderPositions.Length - 1; ++index)
          {
            int num4 = this.page.ConvertWorldXToPixel(this.borderRightOffset[index]);
            int num5 = this.page.ConvertWorldXToPixel(this.borderLeftOffset[index]);
            if (num4 + num5 < 7)
            {
              num4 = 3;
              num5 = 4;
            }
            graphics.FillRectangle((Brush) solidBrush, this.borderPositionsInPixels[index + 1] - num5, num3 - 6, num5 + num4, 13);
          }
        }
      }
      Pen pen = new Pen((Brush) new SolidBrush(this.ForeColor));
      for (int index1 = 0; index1 < this.koordsLines.Count; ++index1)
      {
        int koordsLine = this.koordsLines[index1];
        if (koordsLine > startRulerPosition - num2)
        {
          num1 = koordsLine;
          this.DrawValue(graphics, this.values[index1], index1);
          int num6 = 5;
          if (num2 > 40)
          {
            int num7 = koordsLine + (int) ((double) num2 * 0.5);
            graphics.DrawLine(pen, num7, num3 + 2, num7, num3 + 6);
            num6 = 10;
          }
          int num8 = koordsLine + num2;
          graphics.DrawLine(pen, num8, num3 - 6, num8, num3 + 6);
          for (int index2 = 1; index2 < num6; ++index2)
          {
            int num9 = koordsLine + num2 * index2 / num6;
            graphics.DrawLine(pen, num9, num3 + 4, num9, num3 + 6);
          }
        }
      }
      using (SolidBrush solidBrush = new SolidBrush(SystemColors.InactiveCaptionText))
      {
        graphics.FillRectangle((Brush) solidBrush, 0, 0, startRulerPosition, this.Height);
        graphics.FillRectangle((Brush) solidBrush, endRulerPosition, 0, this.Width - endRulerPosition, this.Height);
      }
      if (this.borderPositions != null && this.borderPositions.Length > 1)
      {
        if (this.typeDrag == enumTypeDrag.tdIdentLeft)
        {
          int num10 = this.page.ConvertWorldXToPixel(this.identLeft + this.borderRightOffset[0] + this.LeftBorder) + startRulerPosition;
          graphics.DrawImage((Image) this.bmpLeftIdentNull, num10 - 5, num3 - 1);
        }
        if (this.typeDrag == enumTypeDrag.tdIdentFirstLine)
        {
          int num11 = this.page.ConvertWorldXToPixel(this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder) + startRulerPosition;
          graphics.DrawImage((Image) this.bmpFirstLineIdentNull, num11 - 5, num3 - 9);
        }
        if (this.typeDrag == enumTypeDrag.tdIdentRight)
        {
          int num12 = this.page.ConvertWorldXToPixel(this.RightBorder - this.borderLeftOffset[1] - this.identRight) + startRulerPosition;
          graphics.DrawImage((Image) this.bmpFirstLineIdentNull, num12 - 5, num3 - 9);
        }
        if (this.typeDrag == enumTypeDrag.tdIdentAll)
        {
          int num13 = this.page.ConvertWorldXToPixel(this.identLeft + this.borderRightOffset[0] + this.LeftBorder) + startRulerPosition;
          graphics.DrawImage((Image) this.bmpLeftIdentNull, num13 - 5, num3 - 1);
          int num14 = this.page.ConvertWorldXToPixel(this.identFirstLine + this.borderRightOffset[0] + this.LeftBorder) + startRulerPosition;
          graphics.DrawImage((Image) this.bmpFirstLineIdentNull, num14 - 5, num3 - 9);
        }
        for (int index = 0; index < this.borderPositions.Length; ++index)
        {
          int x = this.page.ConvertWorldXToPixel(this.borderPositions[index]) + startRulerPosition - 4;
          if (x > startRulerPosition - 6 && x < endRulerPosition)
            graphics.DrawImage((Image) this.bmpBorder, x, num3 - 3);
        }
        if (this.showSliders)
        {
          int num15 = this.page.ConvertWorldXToPixel(this.identFirstLineNotNull) + startRulerPosition;
          if (num15 > startRulerPosition - 4 && num15 < endRulerPosition + 4)
          {
            Bitmap bitmap = this.identFirstLineIsNull ? this.bmpFirstLineIdentNull : this.bmpFirstLineIdent;
            graphics.DrawImage((Image) bitmap, num15 - 5, num3 - 9);
          }
          int num16 = this.page.ConvertWorldXToPixel(this.identRightNotNull) + startRulerPosition;
          if (num16 > startRulerPosition - 4 && num16 < endRulerPosition + 4)
          {
            Bitmap bitmap = this.identRightIsNull ? this.bmpRightIdentNull : this.bmpRightIdent;
            graphics.DrawImage((Image) bitmap, num16 - 5, num3 - 9);
          }
          int num17 = this.page.ConvertWorldXToPixel(this.identLeftNotNull) + startRulerPosition;
          if (num17 > startRulerPosition - 4 && num17 < endRulerPosition + 4)
          {
            Bitmap bitmap1 = this.identLeftIsNull ? this.bmpLeftIdentNull : this.bmpLeftIdent;
            graphics.DrawImage((Image) bitmap1, num17 - 5, num3 - 1);
            Bitmap bitmap2 = this.identLeftIsNull ? this.bmpLeftbottomSliderNull : this.bmpLeftbottomSlider;
            graphics.DrawImage((Image) bitmap2, num17 - 5, num3 + 6);
          }
        }
      }
    }
    if (this.Orientation != enumOrientation.orVertical)
      return;
    int num18 = Math.Abs(this.page.PageUI.ConvertUserYToPageControl((float) this.MajorInterval, this.m) - this.page.PageUI.ConvertUserYToPageControl(0.0f, this.m));
    int num19 = this.Width / 2;
    int startRulerPosition1 = this.StartRulerPosition;
    int endRulerPosition1 = this.EndRulerPosition;
    int num20 = startRulerPosition1;
    using (SolidBrush solidBrush = new SolidBrush(Color.White))
      graphics.FillRectangle((Brush) solidBrush, num19 - 6, num20, 13, endRulerPosition1 - num20);
    if (this.borderPositions != null && this.borderPositions.Length > 1)
    {
      this.borderPositionsInPixels = new int[this.borderPositions.Length + 2];
      for (int index = 1; index < this.borderPositions.Length + 1; ++index)
        this.borderPositionsInPixels[index] = this.page.ConvertWorldYToPixel(this.borderPositions[index - 1]) + startRulerPosition1;
      this.borderPositionsInPixels[0] = startRulerPosition1;
      this.borderPositionsInPixels[this.borderPositionsInPixels.Length - 1] = endRulerPosition1;
      using (SolidBrush solidBrush = new SolidBrush(this.colorBorders))
      {
        graphics.FillRectangle((Brush) solidBrush, num19 - 6, num20 + this.page.ConvertWorldYToPixel(this.borderPositions[0]) - 5, 13, this.page.ConvertWorldYToPixel(this.borderRightOffset[0]) + 5);
        int y = this.page.ConvertWorldYToPixel(this.borderPositions[this.borderPositions.Length - 1] - this.borderLeftOffset[this.borderPositions.Length - 1]) + startRulerPosition1 - 1;
        graphics.FillRectangle((Brush) solidBrush, num19 - 6, y, 13, this.page.ConvertWorldYToPixel(this.borderLeftOffset[this.borderPositions.Length - 1]) + 5);
        for (int index = 1; index < this.borderPositions.Length - 1; ++index)
        {
          int num21 = this.page.ConvertWorldYToPixel(this.borderRightOffset[index]);
          int num22 = this.page.ConvertWorldYToPixel(this.borderLeftOffset[index]);
          if (num21 + num22 < 7)
          {
            num21 = 3;
            num22 = 4;
          }
          graphics.FillRectangle((Brush) solidBrush, num19 - 6, this.borderPositionsInPixels[index + 1] - num22, 13, num22 + num21);
        }
      }
    }
    Pen pen1 = new Pen((Brush) new SolidBrush(this.ForeColor));
    for (int index3 = 0; index3 < this.koordsLines.Count; ++index3)
    {
      int koordsLine = this.koordsLines[index3];
      if (koordsLine > num20 - num18)
      {
        num1 = koordsLine;
        this.DrawValue(graphics, this.values[index3], index3);
        int num23 = 5;
        if (num18 > 40)
        {
          int num24 = koordsLine + (int) ((double) num18 * 0.5);
          graphics.DrawLine(pen1, num19 + 2, num24, num19 + 6, num24);
          num23 = 10;
        }
        int num25 = koordsLine + num18;
        graphics.DrawLine(pen1, num19 - 6, num25, num19 + 6, num25);
        for (int index4 = 1; index4 < num23; ++index4)
        {
          int num26 = koordsLine + num18 * index4 / num23;
          graphics.DrawLine(pen1, num19 + 4, num26, num19 + 6, num26);
        }
      }
    }
    using (SolidBrush solidBrush = new SolidBrush(SystemColors.InactiveCaptionText))
    {
      graphics.FillRectangle((Brush) solidBrush, 0, 0, this.Width, num20);
      graphics.FillRectangle((Brush) solidBrush, 0, endRulerPosition1, this.Width, this.Height - endRulerPosition1);
    }
    if (this.borderPositions == null || this.borderPositions.Length <= 1)
      return;
    for (int index = 0; index < this.borderPositions.Length; ++index)
    {
      int y = this.page.ConvertWorldYToPixel(this.borderPositions[index]) + startRulerPosition1 - 4;
      if (y > num20 - 6 && y < endRulerPosition1)
        graphics.DrawImage((Image) this.bmpBorder, num19 - 3, y);
    }
  }

  private void Line(Graphics g, int x1, int y1, int x2, int y2)
  {
    using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
    {
      using (Pen pen = new Pen((Brush) solidBrush))
      {
        g.DrawLine(pen, x1, y1, x2, y2);
        pen.Dispose();
        solidBrush.Dispose();
      }
    }
  }

  private bool PrepareDrawValue(Graphics g, float iValue, int iPosition, int iSpaceAvailable)
  {
    StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
    if (this.Orientation == enumOrientation.orVertical)
      format.FormatFlags |= StringFormatFlags.DirectionVertical;
    SizeF sizeF = g.MeasureString(iValue.ToString(), this.Font, iSpaceAvailable, format);
    this.values.Add(iValue.ToString());
    int num = 1;
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      this.koordsValues.Add(new Rectangle(new Point(iPosition, 3), sizeF.ToSize()));
      return num != 0;
    }
    this.koordsValues.Add(new Rectangle(new Point(3, iPosition + 2), sizeF.ToSize()));
    return num != 0;
  }

  private bool DrawValue(Graphics g, string iValue, int index)
  {
    StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
    if (this.Orientation == enumOrientation.orVertical)
      format.FormatFlags |= StringFormatFlags.DirectionVertical;
    Size size = this.koordsValues[index].Size;
    bool flag = true;
    Point location;
    if (this.Orientation == enumOrientation.orHorizontal)
    {
      location = this.koordsValues[index].Location;
      if (this.borderPositionsInPixels != null && this.borderPositions != null)
      {
        for (int index1 = 0; index1 < this.borderPositionsInPixels.Length; ++index1)
        {
          if (new Rectangle(this.borderPositionsInPixels[index1] - 3, 0, 7, 1).IntersectsWith(new Rectangle(location.X, 0, size.Width, 1)))
          {
            flag = false;
            break;
          }
        }
      }
    }
    else
    {
      location = this.koordsValues[index].Location;
      if (this.borderPositionsInPixels != null && this.borderPositions != null)
      {
        for (int index2 = 0; index2 < this.borderPositionsInPixels.Length; ++index2)
        {
          if (new Rectangle(0, this.borderPositionsInPixels[index2] - 3, 1, 7).IntersectsWith(new Rectangle(0, location.Y, 1, size.Height)))
          {
            flag = false;
            break;
          }
        }
      }
      Matrix matrix = new Matrix();
      matrix.Translate((float) -location.X, (float) -location.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
      matrix.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
      matrix.Translate((float) (location.X + size.Width), (float) (location.Y + size.Height), System.Drawing.Drawing2D.MatrixOrder.Append);
      g.Transform = matrix;
    }
    if (flag)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        g.DrawString(iValue, this.Font, (Brush) solidBrush, (PointF) location, format);
    }
    g.ResetTransform();
    return flag;
  }

  /// <summary>начато перемещение границ/отступов</summary>
  private void BeginMove()
  {
    if (this.Document == null)
      return;
    this.Document.DeactivateInPlaceEditor();
  }

  /// <summary>закончено перемещение границ/отступов</summary>
  private void EndMove()
  {
    if (this.Document != null)
      this.Document.ActivateInPlaceEditor();
    this.DrawOnPage();
  }

  /// <summary>Установка отступов на линейке</summary>
  /// <param name="IdentLeft">Отступ слева</param>
  /// <param name="IdentLeftisNull">Является ли отступ слева несовпадающим у элементов</param>
  /// <param name="IdentRight">Отступ справа</param>
  /// <param name="IdentRightIsNull">Является ли отступ справа несовпадающим у элементов</param>
  /// <param name="IdentFirstLine">Отступ первой строки</param>
  /// <param name="IdentFirstLineIsNull">Является ли отступ первой строки несовпадающим у элементов</param>
  public void SetIdents(
    float IdentLeft,
    bool IdentLeftIsNull,
    float IdentRight,
    bool IdentRightIsNull,
    float IdentFirstLine,
    bool IdentFirstLineIsNull)
  {
    if (this.typeDrag != enumTypeDrag.tdNone)
      return;
    this.IdentLeft = new float?(IdentLeft);
    this.IdentRight = new float?(IdentRight);
    this.IdentFirstLine = new float?(IdentFirstLine);
    this.identRightIsNull = IdentRightIsNull;
    this.identLeftIsNull = IdentLeftIsNull;
    this.identFirstLineIsNull = IdentFirstLineIsNull;
  }

  /// <summary>Пересчитать значения на линейке</summary>
  public void RebuildRulerCoords()
  {
    if (!this.Visible || this.Page?.PageUI == null || this.Page.PageControl == null || this.page == null)
      return;
    this.PrepareDrawControl(this.CreateGraphics());
  }

  /// <summary>Обновить отступы</summary>
  public void UpdateIdents()
  {
    if (this.typeDrag != enumTypeDrag.tdNone)
      return;
    this.IdentFirstLine = new float?(this.identFirstLine);
    this.IdentLeft = new float?(this.identLeft);
    this.IdentRight = new float?(this.identRight);
  }

  /// <summary>Происходит когда изменен какой - либо отступ</summary>
  [CustomDescription("Attribute.Document.Model_55")]
  [Category("Ruler")]
  public event Ruler.IdentChanged_EventHandler IdentChanged
  {
    add => this.identChanged += value;
    remove => this.identChanged -= value;
  }

  /// <summary>Герерирует событие IdentChanged</summary>
  public virtual void OnIdentChanged(IdentChanged_EventArgs e)
  {
    if (this.identChanged == null)
      return;
    this.identChanged((object) this, e);
  }

  /// <summary>Происходит когда изменено положение границы</summary>
  [CustomDescription("Attribute.Document.Model_56")]
  [Category("Ruler")]
  public event Ruler.BorderPositionChanged_EventHandler BorderPositionChanged
  {
    add => this.borderPositionChanged += value;
    remove => this.borderPositionChanged -= value;
  }

  /// <summary>Герерирует событие IdentChanged</summary>
  public virtual void OnBorderPositionChanged(BorderPositionChanged_EventArgs e)
  {
    if (this.borderPositionChanged == null)
      return;
    this.borderPositionChanged((object) this, e);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
    if (!disposing)
      return;
    if (ImDocumentEditorConfig.Instance != null)
    {
      ImDocumentEditorConfig.Instance.CoorSystemChanged -= new EventHandler(this.Instance_CoorSystemChanged);
      ImDocumentEditorConfig.Instance.CoorSystemPositionChanged -= new EventHandler(this.Instance_CoorSystemChanged);
    }
    this.Page = (Page) null;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

  /// <summary>Обработчик события IdentChanged</summary>
  public delegate void IdentChanged_EventHandler(object sender, IdentChanged_EventArgs e);

  /// <summary>Обработчик события BorderPositionChanged</summary>
  public delegate void BorderPositionChanged_EventHandler(
    object sender,
    BorderPositionChanged_EventArgs e);
}
