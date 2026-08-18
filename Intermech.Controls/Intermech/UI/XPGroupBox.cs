
// Type: Intermech.UI.XPGroupBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.UI;

/// <summary>Summary description for XPGroupBox.</summary>
[Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof (IDesigner))]
public class XPGroupBox : UserControl
{
  private const int _captionHeight = 25;
  private const int _iconBorder = 10;
  private const int _expandBorder = 4;
  private const int _chevronSize = 18;
  private static ImageAttributes _grayAttrs;
  private static ColorMatrix _grayMatrix = new ColorMatrix();
  private int _transitionAlphaChannel;
  private int _transitionSizeDelta;
  private int _captionCurveRadius = 7;
  private int _captionOffset;
  private int _ctrlHeight = 10;
  private bool _captionHighlighted;
  private bool _ctrlExpanded = true;
  private bool _showChevron = true;
  private bool _expanded = true;
  private bool _inProcess;
  private Font _captionFont = new Font("Verdana", 8f, FontStyle.Bold);
  private LinearGradientMode _captionGradientMode = LinearGradientMode.Vertical;
  private Color _paneBottomRightColor = SystemColors.Control;
  private Color _captionRightColor = SystemColors.Control;
  private Color _captionFontHighLightColor = Color.Blue;
  private Color _captionLeftColor = Color.White;
  private Color _captionFontColor = Color.Black;
  private Color _paneTopLeftColor = Color.White;
  private Color _paneOutlineColor = Color.White;
  private System.Timers.Timer _timer1;
  private string _captionText = "My Caption";
  private Image _image;
  private ToolTip _toolTip;
  private XPGroupBox.GroupState _groupState;
  private bool _stopUpdate;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Конструктор.</summary>
  static XPGroupBox()
  {
    XPGroupBox._grayMatrix.Matrix00 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix01 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix02 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix10 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix11 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix12 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix20 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix21 = 0.333333343f;
    XPGroupBox._grayMatrix.Matrix22 = 0.333333343f;
    XPGroupBox._grayAttrs = new ImageAttributes();
    XPGroupBox._grayAttrs.SetColorMatrix(XPGroupBox._grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
  }

  /// <summary>Конструктор.</summary>
  public XPGroupBox()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.BackColor = Color.Transparent;
    this._toolTip = new ToolTip();
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the radius of the curves at the top-left and top-right of the control caption.")]
  [DefaultValue(7)]
  [Category("Caption")]
  public int CaptionCurveRadius
  {
    get => this._captionCurveRadius;
    set
    {
      this._captionCurveRadius = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the font and style of the caption text.")]
  [Category("Appearance")]
  public Font CaptionFont
  {
    get => this._captionFont;
    set
    {
      this._captionFont = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the color of the caption font.")]
  [Category("Caption")]
  public Color CaptionFontColor
  {
    get => this._captionFontColor;
    set
    {
      this._captionFontColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the highlight color of the caption font.")]
  [Category("Caption")]
  public Color CaptionFontHighLightColor
  {
    get => this._captionFontHighLightColor;
    set
    {
      this._captionFontHighLightColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(LinearGradientMode.Vertical)]
  [Category("Caption")]
  [Description("Specifies the direction of a linear gradient in the title bar.")]
  public LinearGradientMode CaptionGradientMode
  {
    get => this._captionGradientMode;
    set
    {
      if (this._captionGradientMode == value)
        return;
      this._captionGradientMode = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the starting (light) color of the caption gradient fill.")]
  [Category("Caption")]
  public Color CaptionLeftColor
  {
    get => this._captionLeftColor;
    set
    {
      this._captionLeftColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the ending (dark) color of the caption gradient fill.")]
  [Category("Caption")]
  public Color CaptionRightColor
  {
    get => this._captionRightColor;
    set
    {
      this._captionRightColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("The text containd in the caption.")]
  [Category("Caption")]
  public string CaptionText
  {
    get => this._captionText;
    set
    {
      this._captionText = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines if the group is expanded.")]
  [DefaultValue(true)]
  [Category("Appearance")]
  public bool Expanded
  {
    get => this._expanded;
    set => this.ExpandControl(value);
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines if the group is expanded by default at runtime.")]
  [DefaultValue(true)]
  [Category("Appearance")]
  public bool GroupExpanded
  {
    get => this._ctrlExpanded;
    set => this._ctrlExpanded = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this);
    set
    {
      this._toolTip.RemoveAll();
      this._toolTip.SetToolTip((Control) this, value);
    }
  }

  /// <summary>
  /// Gets/sets the image displayed in the header of the title bar.
  /// </summary>
  [Category("Caption")]
  [Description("The image that will be displayed on the left hand side of the title bar.")]
  public Image Image
  {
    get => this._image;
    set
    {
      if (this._image != value)
      {
        this._image = value;
        if (this._image != null)
        {
          this._captionOffset = this._image.Height - 25 + 2;
          if (this._captionOffset < 0)
            this._captionOffset = 0;
        }
        else
          this._captionOffset = 0;
      }
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines order in parent's list.")]
  [Category("Appearance")]
  public int Index
  {
    get
    {
      XPGroupBoxContainer container = this.GetContainer();
      return container == null ? -1 : container.Controls.IndexOf((Control) this);
    }
    set
    {
      if (value < 0)
        return;
      XPGroupBoxContainer container = this.GetContainer();
      if (container == null)
        return;
      container.Controls.SetChildIndex((Control) this, value);
      this.OnSizeChanging(new EventArgs());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the ending (dark) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneBottomRightColor
  {
    get => this._paneBottomRightColor;
    set
    {
      this._paneBottomRightColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the color of the pane outline.")]
  [Category("Appearance")]
  public Color PaneOutlineColor
  {
    get => this._paneOutlineColor;
    set
    {
      this._paneOutlineColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines the starting (light) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneTopLeftColor
  {
    get => this._paneTopLeftColor;
    set
    {
      this._paneTopLeftColor = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(true)]
  public bool ShowChevron
  {
    get => this._showChevron;
    set
    {
      if (value == this._showChevron)
        return;
      this._showChevron = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event SizeChangingHandler SizeChanging;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_XPGroupBox_ControlsCollectionChanged(object sender, ControlEventArgs e)
  {
    if (this._stopUpdate)
      return;
    this.CorrectHeigth();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnSizeChanging(EventArgs e)
  {
    if (this.SizeChanging == null)
      return;
    this.SizeChanging((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void timer1_Elapsed(object sender, ElapsedEventArgs e)
  {
    if (this._transitionSizeDelta == 0)
      this._transitionSizeDelta = 1;
    if (this._timer1.Interval > 20.0)
      this._timer1.Interval -= 20.0;
    else
      this._transitionSizeDelta += 2;
    if (this._transitionAlphaChannel == 0)
      this._transitionAlphaChannel = 10;
    else if (this._transitionAlphaChannel + 10 < (int) byte.MaxValue)
      this._transitionAlphaChannel += 10;
    switch (this._groupState)
    {
      case XPGroupBox.GroupState.Static:
        this._inProcess = false;
        this._timer1.Enabled = false;
        this._transitionSizeDelta = 0;
        break;
      case XPGroupBox.GroupState.Expanding:
        if (this.Height + this._transitionSizeDelta < this._ctrlHeight)
        {
          this.SetControlsOpacity(this._transitionAlphaChannel);
          this._paneBottomRightColor = Color.FromArgb(this._transitionAlphaChannel, this._paneBottomRightColor);
          this._paneTopLeftColor = Color.FromArgb(this._transitionAlphaChannel, this._paneTopLeftColor);
          this._paneOutlineColor = Color.FromArgb(this._transitionAlphaChannel, this._paneOutlineColor);
          this.Height += this._transitionSizeDelta;
          this.SetControlsVisible();
          break;
        }
        this.SetControlsOpacity((int) byte.MaxValue);
        this._paneBottomRightColor = Color.FromArgb((int) byte.MaxValue, this._paneBottomRightColor);
        this._paneTopLeftColor = Color.FromArgb((int) byte.MaxValue, this._paneTopLeftColor);
        this._paneOutlineColor = Color.FromArgb((int) byte.MaxValue, this._paneOutlineColor);
        this._transitionAlphaChannel = 0;
        this.Height = this._ctrlHeight;
        this._expanded = true;
        this._groupState = XPGroupBox.GroupState.Static;
        this.SetControlsVisible();
        break;
      case XPGroupBox.GroupState.Collapsing:
        if (this.Height - this._transitionSizeDelta > 25 + this._captionOffset)
        {
          this.SetControlsOpacity(this._transitionAlphaChannel);
          this.Height -= this._transitionSizeDelta;
          this._paneBottomRightColor = Color.FromArgb((int) byte.MaxValue - this._transitionAlphaChannel, this._paneBottomRightColor);
          this._paneTopLeftColor = Color.FromArgb((int) byte.MaxValue - this._transitionAlphaChannel, this._paneTopLeftColor);
          this._paneOutlineColor = Color.FromArgb((int) byte.MaxValue - this._transitionAlphaChannel, this._paneOutlineColor);
          this.SetControlsVisible();
          break;
        }
        this.SetControlsVisible();
        this._transitionAlphaChannel = 0;
        this.SetControlsOpacity(0);
        this._paneBottomRightColor = Color.FromArgb(0, this._paneBottomRightColor);
        this._paneTopLeftColor = Color.FromArgb(0, this._paneTopLeftColor);
        this._paneOutlineColor = Color.FromArgb(0, this._paneOutlineColor);
        this.Height = 25 + this._captionOffset;
        this._expanded = false;
        this._groupState = XPGroupBox.GroupState.Static;
        break;
      default:
        throw new InvalidExpressionException("groupState variable set to incorrect value");
    }
    this.Invalidate();
    this.OnSizeChanging(new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void XPGroupBox_Load(object sender, EventArgs e)
  {
    this.CorrectHeigth();
    this._ctrlHeight = this.Height;
    if (this.DesignMode || this._ctrlExpanded)
      return;
    this.Height = 25 + this._captionOffset;
    this._expanded = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void XPGroupBox_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Y > 25 + this._captionOffset || this._inProcess)
      return;
    this.ExpandControl(!this._expanded);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void XPGroupBox_MouseLeave(object sender, EventArgs e)
  {
    this._captionHighlighted = false;
    Cursor.Current = Cursors.Default;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void XPGroupBox_MouseMove(object sender, MouseEventArgs e)
  {
    if (e.Y < 25 + this._captionOffset)
    {
      this._captionHighlighted = true;
      Cursor.Current = Cursors.Hand;
    }
    else
    {
      this._captionHighlighted = false;
      Cursor.Current = Cursors.Default;
    }
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  public override Rectangle DisplayRectangle
  {
    get
    {
      Rectangle displayRectangle = base.DisplayRectangle;
      displayRectangle.Inflate(-2, -2);
      int num = 25 + this._captionOffset;
      return new Rectangle(displayRectangle.X, displayRectangle.Y + num, displayRectangle.Width, displayRectangle.Height - num);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    Rectangle rect = new Rectangle(0, 0, this.Width, 25);
    Size size = e.Graphics.MeasureString("Wg", this._captionFont).ToSize();
    int captionOffset = this._captionOffset;
    rect.Offset(0, captionOffset);
    using (GraphicsPath path = new GraphicsPath())
    {
      if (this._captionCurveRadius > 0)
      {
        int num = this._captionCurveRadius * 2;
        int captionCurveRadius = this._captionCurveRadius;
        path.AddLine(rect.Left + captionCurveRadius, rect.Top, rect.Right - num - 1, rect.Top);
        path.AddArc(rect.Right - num - 1, rect.Top, num, num, 270f, 90f);
        path.AddLine(rect.Right, rect.Top + captionCurveRadius, rect.Right, rect.Bottom);
        path.AddLine(rect.Right, rect.Bottom, rect.Left - 1, rect.Bottom);
        path.AddArc(rect.Left, rect.Top, num, num, 180f, 90f);
      }
      else
      {
        int num = -this._captionCurveRadius;
        int width = this.Width;
        int bottom = rect.Bottom;
        path.AddLine(0, num + rect.Top, num, rect.Top);
        path.AddLine(num, rect.Top, width - num, rect.Top);
        path.AddLine(width - num, rect.Top, width, num + rect.Top);
        path.AddLine(width, num + rect.Top, width, bottom);
        path.AddLine(rect.Right, rect.Bottom, rect.Left - 1, rect.Bottom);
        path.AddLine(rect.Left, bottom, rect.Left, rect.Top + num);
      }
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.Enabled)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this._captionLeftColor, this._captionRightColor, this._captionGradientMode))
          e.Graphics.FillPath((Brush) linearGradientBrush, path);
      }
      else
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, new Colour()
        {
          CurrentColour = this._captionLeftColor,
          Saturation = 0.0f
        }.CurrentColour, new Colour()
        {
          CurrentColour = this._captionRightColor,
          Saturation = 0.0f
        }.CurrentColour, this._captionGradientMode))
          e.Graphics.FillPath((Brush) linearGradientBrush, path);
      }
    }
    if (this.Height > rect.Bottom)
    {
      using (Pen pen = new Pen(this._paneOutlineColor))
      {
        e.Graphics.DrawLine(pen, 0, rect.Bottom, 0, this.Height);
        e.Graphics.DrawLine(pen, this.Width - 1, rect.Bottom, this.Width - 1, this.Height);
        e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
      }
    }
    GraphicsUnit pageUnit = GraphicsUnit.Display;
    int num1 = 10;
    if (this._image != null)
    {
      num1 += this._image.Width + 10 + 2;
      RectangleF bounds = this._image.GetBounds(ref pageUnit);
      Rectangle destRect = new Rectangle(12, 10, this._image.Width, this._image.Height);
      if (this.Enabled)
        e.Graphics.DrawImage(this._image, destRect, (int) bounds.Left, (int) bounds.Top, (int) bounds.Width, (int) bounds.Height, pageUnit);
      else
        e.Graphics.DrawImage(this._image, destRect, (int) bounds.Left, (int) bounds.Top, (int) bounds.Width, (int) bounds.Height, pageUnit, XPGroupBox._grayAttrs);
    }
    float x = (float) num1;
    float y = (float) captionOffset + 4f;
    float width1 = (float) ((double) this.Width - (double) x - 4.0);
    if (this._showChevron)
      width1 -= 18f;
    RectangleF layoutRectangle = new RectangleF(x, y, width1, (float) size.Height);
    StringFormat format = new StringFormat();
    format.Trimming = StringTrimming.EllipsisCharacter;
    if (this.Enabled)
    {
      using (SolidBrush solidBrush = new SolidBrush(this._captionHighlighted ? this._captionFontHighLightColor : this._captionFontColor))
        e.Graphics.DrawString(this._captionText, this._captionFont, (Brush) solidBrush, layoutRectangle, format);
    }
    else
    {
      Color control = SystemColors.Control;
      ControlPaint.DrawStringDisabled(e.Graphics, this._captionText, this._captionFont, control, layoutRectangle, format);
    }
    if (!this._showChevron)
      return;
    this.DrawChevrons(e.Graphics, (int) y);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pevent"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    base.OnPaintBackground(pevent);
    int y = 25 + this._captionOffset;
    if (this.Height <= y)
      return;
    Rectangle rect = new Rectangle(0, y, this.Width, this.Height - y);
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this._paneTopLeftColor, this._paneBottomRightColor, LinearGradientMode.ForwardDiagonal))
      pevent.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private void CorrectHeigth()
  {
    int num = 0;
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is XPGroupItem xpGroupItem)
      {
        Image image = xpGroupItem.Image;
        int height1 = image != null ? image.Height : 0;
        int height2 = xpGroupItem.Font.Height;
        num += height1 > height2 ? height1 + 8 : height2 + 8;
      }
    }
    this.Height = num + this.DisplayRectangle.Y + 10;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="p"></param>
  /// <param name="x"></param>
  /// <param name="y"></param>
  private void DrawChevron(Graphics g, Pen p, int x, int y)
  {
    if (this._expanded)
    {
      g.DrawLine(p, x, y + 3, x + 3, y);
      g.DrawLine(p, x + 3, y, x + 6, y + 3);
      g.DrawLine(p, x + 1, y + 3, x + 3, y + 1);
      g.DrawLine(p, x + 3, y + 1, x + 5, y + 3);
    }
    else
    {
      g.DrawLine(p, x, y, x + 3, y + 3);
      g.DrawLine(p, x + 3, y + 3, x + 6, y);
      g.DrawLine(p, x + 1, y, x + 3, y + 2);
      g.DrawLine(p, x + 3, y + 2, x + 5, y);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="y"></param>
  private void DrawChevrons(Graphics g, int y)
  {
    g.SmoothingMode = SmoothingMode.None;
    int num = this.Width - 10 - 18;
    if (this.Enabled)
    {
      using (Pen p = new Pen(this._captionHighlighted ? this._captionFontHighLightColor : this._captionFontColor))
      {
        this.DrawChevron(g, p, num + 4, y + 4);
        this.DrawChevron(g, p, num + 4, y + 8);
      }
    }
    else
    {
      using (Pen p = new Pen(Color.Silver))
      {
        this.DrawChevron(g, p, num + 4, y + 4 + 1);
        this.DrawChevron(g, p, num + 4, y + 8 + 1);
      }
      this.DrawChevron(g, SystemPens.ControlDarkDark, num + 4, y + 4);
      this.DrawChevron(g, SystemPens.ControlDarkDark, num + 4, y + 8);
    }
  }

  /// <summary>If we get here - the caption was clicked</summary>
  /// <param name="value"></param>
  private void ExpandControl(bool value)
  {
    if (!value)
    {
      this._ctrlHeight = this.Height;
      this._groupState = XPGroupBox.GroupState.Collapsing;
    }
    else
      this._groupState = XPGroupBox.GroupState.Expanding;
    this._inProcess = true;
    this._timer1.Interval = 100.0;
    this._timer1.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private XPGroupBoxContainer GetContainer() => this.Parent as XPGroupBoxContainer;

  /// <summary>
  /// 
  /// </summary>
  private void ResetImage() => this._image = (Image) null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="opacity"></param>
  private void SetControlsOpacity(int opacity)
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (!(control is TextBox))
      {
        switch (this._groupState)
        {
          case XPGroupBox.GroupState.Static:
            continue;
          case XPGroupBox.GroupState.Expanding:
            if (control.BackColor != Color.Transparent)
              control.BackColor = Color.FromArgb(opacity, control.BackColor);
            control.ForeColor = Color.FromArgb(opacity, control.ForeColor);
            continue;
          case XPGroupBox.GroupState.Collapsing:
            if (control.BackColor != Color.Transparent)
              control.BackColor = Color.FromArgb((int) byte.MaxValue - opacity, control.BackColor);
            control.ForeColor = Color.FromArgb((int) byte.MaxValue - opacity, control.ForeColor);
            continue;
          default:
            throw new InvalidExpressionException("groupState variable set to incorrect value");
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void SetControlsVisible()
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
      control.Visible = control.Location.Y >= 25 + this._captionOffset;
  }

  /// <summary>Возобновление обновления контрола.</summary>
  public void StartUpdate()
  {
    if (!this._stopUpdate)
      return;
    this.ResumeLayout();
    this._stopUpdate = false;
    this.CorrectHeigth();
  }

  /// <summary>Остановка обновления контрола.</summary>
  public void StopUpdate()
  {
    if (this._stopUpdate)
      return;
    this.SuspendLayout();
    this._stopUpdate = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._timer1 = new System.Timers.Timer();
    this._timer1.BeginInit();
    this.SuspendLayout();
    this._timer1.SynchronizingObject = (ISynchronizeInvoke) this;
    this._timer1.Elapsed += new ElapsedEventHandler(this.timer1_Elapsed);
    this.BackColor = Color.AliceBlue;
    this.Name = nameof (XPGroupBox);
    this.Load += new EventHandler(this.XPGroupBox_Load);
    this.MouseLeave += new EventHandler(this.XPGroupBox_MouseLeave);
    this.MouseMove += new MouseEventHandler(this.XPGroupBox_MouseMove);
    this.ControlAdded += new ControlEventHandler(this.On_XPGroupBox_ControlsCollectionChanged);
    this.MouseDown += new MouseEventHandler(this.XPGroupBox_MouseDown);
    this.ControlRemoved += new ControlEventHandler(this.On_XPGroupBox_ControlsCollectionChanged);
    this._timer1.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private enum GroupState
  {
    Static,
    Expanding,
    Collapsing,
  }
}
