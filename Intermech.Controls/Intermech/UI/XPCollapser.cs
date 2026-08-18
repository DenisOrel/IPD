
// Type: Intermech.UI.XPCollapser
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

/// <summary>Summary description for XPCollapser.</summary>
[Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof (IDesigner))]
public class XPCollapser : UserControl
{
  private IContainer components;
  private static ColorMatrix grayMatrix = new ColorMatrix();
  private static ImageAttributes grayAttributes;
  private const int iconBorder = 2;
  private const int expandBorder = 4;
  private const int chevronSize = 18;
  private int captionHeight = 25;
  private int _captionOffset;
  private int controlHeight = 10;
  private int transitionSizeDelta;
  private int transitionAlphaChannel;
  private bool captionHighlighted;
  private bool _showChevron;
  private bool expanded = true;
  private bool _controlExpanded = true;
  private int captionCurveRadius = 7;
  private Color captionLeftColor = Color.White;
  private Color captionRightColor = Color.SteelBlue;
  private LinearGradientMode _captionGradientMode = LinearGradientMode.Vertical;
  private Font captionFont = new Font("Microsoft Verdana", 8f, FontStyle.Bold);
  private Color captionFontColor = Color.Black;
  private Color captionFontHighLightColor = Color.Red;
  private Color paneTopLeftColor = Color.White;
  private Color paneBottomRightColor = Color.FromArgb(214, 223, 247);
  private Color paneOutlineColor = Color.White;
  private System.Timers.Timer timer1;
  private string captionText = "My Caption";
  private Image _image;
  private ToolTip _toolTip;
  private XPCollapser.GroupState _groupState;

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

  [Browsable(false)]
  public bool ControlExpanded
  {
    get => this._controlExpanded;
    set => this._controlExpanded = value;
  }

  [Description("Determines if the group is expanded.")]
  [DefaultValue(true)]
  [Category("Appearance")]
  public bool Expanded
  {
    get => this.expanded;
    set
    {
      if (this.expanded == value)
        return;
      this.ExpandControl(value);
    }
  }

  [Description("Determines the radius of the curves at the top-left and top-right of the control caption.")]
  [DefaultValue(7)]
  [Category("Caption")]
  public int CaptionCurveRadius
  {
    get => this.captionCurveRadius;
    set
    {
      this.captionCurveRadius = value;
      this.Invalidate();
    }
  }

  [Description("Determines the starting (light) color of the caption gradient fill.")]
  [Category("Caption")]
  public Color CaptionLeftColor
  {
    get => this.captionLeftColor;
    set
    {
      this.captionLeftColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the ending (dark) color of the caption gradient fill.")]
  [Category("Caption")]
  public Color CaptionRightColor
  {
    get => this.captionRightColor;
    set
    {
      this.captionRightColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the starting (light) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneTopLeftColor
  {
    get => this.paneTopLeftColor;
    set
    {
      this.paneTopLeftColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the ending (dark) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneBottomRightColor
  {
    get => this.paneBottomRightColor;
    set
    {
      this.paneBottomRightColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the color of the pane outline.")]
  [Category("Appearance")]
  public Color PaneOutlineColor
  {
    get => this.paneOutlineColor;
    set
    {
      this.paneOutlineColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the font and style of the caption text.")]
  [Category("Caption")]
  public Font CaptionFont
  {
    get => this.captionFont;
    set
    {
      this.captionFont = value;
      this.Invalidate();
    }
  }

  [Description("Determines the color of the caption font.")]
  [Category("Caption")]
  public Color CaptionFontColor
  {
    get => this.captionFontColor;
    set
    {
      this.captionFontColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the highlight color of the caption font.")]
  [Category("Caption")]
  public Color CaptionFontHighLightColor
  {
    get => this.captionFontHighLightColor;
    set
    {
      this.captionFontHighLightColor = value;
      this.Invalidate();
    }
  }

  [Description("The text containd in the caption.")]
  [Category("Caption")]
  public string CaptionText
  {
    get => this.captionText;
    set
    {
      this.captionText = value;
      this.Invalidate();
    }
  }

  [DefaultValue(false)]
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
          this._captionOffset = this._image.Height - this.captionHeight + 2;
          if (this._captionOffset < 0)
            this._captionOffset = 0;
        }
        else
          this._captionOffset = 0;
      }
      this.Invalidate();
    }
  }

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

  public override Rectangle DisplayRectangle
  {
    get
    {
      Rectangle displayRectangle = base.DisplayRectangle;
      displayRectangle.Inflate(-2, -2);
      int num = this.captionHeight + this._captionOffset;
      return new Rectangle(displayRectangle.X, displayRectangle.Y + num, displayRectangle.Width, displayRectangle.Height - num);
    }
  }

  private void ResetImage() => this._image = (Image) null;

  static XPCollapser()
  {
    XPCollapser.grayMatrix.Matrix00 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix01 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix02 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix10 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix11 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix12 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix20 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix21 = 0.333333343f;
    XPCollapser.grayMatrix.Matrix22 = 0.333333343f;
    XPCollapser.grayAttributes = new ImageAttributes();
    XPCollapser.grayAttributes.SetColorMatrix(XPCollapser.grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
  }

  public XPCollapser()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.BackColor = Color.Transparent;
    this._toolTip = new ToolTip();
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
    this.timer1 = new System.Timers.Timer();
    this.timer1.BeginInit();
    this.timer1.SynchronizingObject = (ISynchronizeInvoke) this;
    this.timer1.Elapsed += new ElapsedEventHandler(this.timer1_Elapsed);
    this.BackColor = Color.AliceBlue;
    this.Name = nameof (XPCollapser);
    this.MouseMove += new MouseEventHandler(this.XPGroupBox_MouseMove);
    this.MouseLeave += new EventHandler(this.XPGroupBox_MouseLeave);
    this.MouseDown += new MouseEventHandler(this.XPGroupBox_MouseDown);
    this.timer1.EndInit();
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Rectangle rect = new Rectangle(0, 0, this.Width, this.captionHeight);
    Size size = e.Graphics.MeasureString("Wg", this.captionFont).ToSize();
    int captionOffset = this._captionOffset;
    rect.Offset(0, captionOffset);
    using (GraphicsPath path = new GraphicsPath())
    {
      if (this.captionCurveRadius > 0)
      {
        int num = this.captionCurveRadius * 2;
        int captionCurveRadius = this.captionCurveRadius;
        path.AddLine(rect.Left + captionCurveRadius, rect.Top, rect.Right - num - 1, rect.Top);
        path.AddArc(rect.Right - num - 1, rect.Top, num, num, 270f, 90f);
        path.AddLine(rect.Right, rect.Top + captionCurveRadius, rect.Right, rect.Bottom);
        path.AddLine(rect.Right, rect.Bottom, rect.Left - 1, rect.Bottom);
        path.AddArc(rect.Left, rect.Top, num, num, 180f, 90f);
      }
      else
      {
        int num = -this.captionCurveRadius;
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
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.captionLeftColor, this.captionRightColor, this._captionGradientMode);
        e.Graphics.FillPath((Brush) linearGradientBrush, path);
        linearGradientBrush.Dispose();
      }
      else
      {
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, new Colour()
        {
          CurrentColour = this.captionLeftColor,
          Saturation = 0.0f
        }.CurrentColour, new Colour()
        {
          CurrentColour = this.captionRightColor,
          Saturation = 0.0f
        }.CurrentColour, this._captionGradientMode);
        e.Graphics.FillPath((Brush) linearGradientBrush, path);
        linearGradientBrush.Dispose();
      }
    }
    if (this.Height > rect.Bottom)
    {
      using (Pen pen = new Pen(this.paneOutlineColor))
      {
        e.Graphics.DrawLine(pen, 0, rect.Bottom, 0, this.Height);
        e.Graphics.DrawLine(pen, this.Width - 1, rect.Bottom, this.Width - 1, this.Height);
        e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
      }
    }
    GraphicsUnit pageUnit = GraphicsUnit.Display;
    int num1 = 2;
    if (this._image != null)
    {
      num1 += this._image.Width + 2 + 2;
      RectangleF bounds = this._image.GetBounds(ref pageUnit);
      Rectangle destRect = new Rectangle(4, 2, this._image.Width, this._image.Height);
      if (this.Enabled)
        e.Graphics.DrawImage(this._image, destRect, (int) bounds.Left, (int) bounds.Top, (int) bounds.Width, (int) bounds.Height, pageUnit);
      else
        e.Graphics.DrawImage(this._image, destRect, (int) bounds.Left, (int) bounds.Top, (int) bounds.Width, (int) bounds.Height, pageUnit, XPCollapser.grayAttributes);
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
      SolidBrush solidBrush = !this.captionHighlighted ? new SolidBrush(this.CaptionFontColor) : new SolidBrush(this.captionFontHighLightColor);
      e.Graphics.DrawString(this.captionText, this.captionFont, (Brush) solidBrush, layoutRectangle, format);
      solidBrush.Dispose();
    }
    else
    {
      Color grayText = SystemColors.GrayText;
      ControlPaint.DrawStringDisabled(e.Graphics, this.captionText, this.captionFont, grayText, layoutRectangle, format);
    }
    if (!this._showChevron)
      return;
    this.DrawChevrons(e.Graphics, (int) y);
  }

  private void DrawChevrons(Graphics g, int y)
  {
    g.SmoothingMode = SmoothingMode.None;
    int num = this.Width - 2 - 18;
    Pen p1;
    if (this.Enabled)
    {
      p1 = !this.captionHighlighted ? new Pen(this.captionFontColor) : new Pen(this.captionFontHighLightColor);
      this.DrawChevron(g, p1, num + 4, y + 4);
      this.DrawChevron(g, p1, num + 4, y + 8);
    }
    else
    {
      Pen p2 = new Pen(Color.Silver);
      this.DrawChevron(g, p2, num + 4, y + 4 + 1);
      this.DrawChevron(g, p2, num + 4, y + 8 + 1);
      p1 = SystemPens.ControlDarkDark;
      this.DrawChevron(g, p1, num + 4, y + 4);
      this.DrawChevron(g, p1, num + 4, y + 8);
    }
    p1.Dispose();
  }

  private void DrawChevron(Graphics g, Pen p, int x, int y)
  {
    if (this.expanded)
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

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    base.OnPaintBackground(pevent);
    int y = this.captionHeight + this._captionOffset;
    if (this.Height <= y)
      return;
    Rectangle rect = new Rectangle(0, y, this.Width, this.Height - y);
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.paneTopLeftColor, this.paneBottomRightColor, LinearGradientMode.ForwardDiagonal);
    pevent.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
  }

  private void XPGroupBox_MouseMove(object sender, MouseEventArgs e)
  {
    if (e.Y < this.captionHeight + this._captionOffset)
    {
      this.captionHighlighted = true;
      Cursor.Current = Cursors.Hand;
    }
    else
    {
      this.captionHighlighted = false;
      Cursor.Current = Cursors.Default;
    }
    this.Invalidate();
  }

  private void XPGroupBox_MouseLeave(object sender, EventArgs e)
  {
    this.captionHighlighted = false;
    Cursor.Current = Cursors.Default;
    this.Invalidate();
  }

  private void XPGroupBox_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Y > this.captionHeight + this._captionOffset)
      return;
    this.ExpandControl(!this.expanded);
  }

  private void ExpandControl(bool value)
  {
    if (!value)
    {
      this.controlHeight = this.Height;
      this._groupState = XPCollapser.GroupState.Collapsing;
    }
    else
      this._groupState = XPCollapser.GroupState.Expanding;
    this.timer1.Interval = 100.0;
    this.timer1.Enabled = true;
  }

  private void timer1_Elapsed(object sender, ElapsedEventArgs e)
  {
    if (this.transitionSizeDelta == 0)
      this.transitionSizeDelta = 1;
    if (this.timer1.Interval > 20.0)
      this.timer1.Interval -= 20.0;
    else
      this.transitionSizeDelta += 2;
    if (this.transitionAlphaChannel == 0)
      this.transitionAlphaChannel = 10;
    else if (this.transitionAlphaChannel + 10 < (int) byte.MaxValue)
      this.transitionAlphaChannel += 10;
    switch (this._groupState)
    {
      case XPCollapser.GroupState.Static:
        this.timer1.Enabled = false;
        this.transitionSizeDelta = 0;
        break;
      case XPCollapser.GroupState.Expanding:
        if (this.Height + this.transitionSizeDelta < this.controlHeight)
        {
          this.SetControlsOpacity(this.transitionAlphaChannel);
          this.paneBottomRightColor = Color.FromArgb(this.transitionAlphaChannel, this.paneBottomRightColor);
          this.paneTopLeftColor = Color.FromArgb(this.transitionAlphaChannel, this.paneTopLeftColor);
          this.paneOutlineColor = Color.FromArgb(this.transitionAlphaChannel, this.paneOutlineColor);
          this.Height += this.transitionSizeDelta;
          this.SetControlsVisible();
          break;
        }
        this.SetControlsOpacity((int) byte.MaxValue);
        this.paneBottomRightColor = Color.FromArgb((int) byte.MaxValue, this.paneBottomRightColor);
        this.paneTopLeftColor = Color.FromArgb((int) byte.MaxValue, this.paneTopLeftColor);
        this.paneOutlineColor = Color.FromArgb((int) byte.MaxValue, this.paneOutlineColor);
        this.transitionAlphaChannel = 0;
        this.Height = this.controlHeight;
        this.expanded = true;
        this._groupState = XPCollapser.GroupState.Static;
        this.SetControlsVisible();
        break;
      case XPCollapser.GroupState.Collapsing:
        if (this.Height - this.transitionSizeDelta > this.captionHeight + this._captionOffset)
        {
          this.SetControlsOpacity(this.transitionAlphaChannel);
          this.Height -= this.transitionSizeDelta;
          this.paneBottomRightColor = Color.FromArgb((int) byte.MaxValue - this.transitionAlphaChannel, this.paneBottomRightColor);
          this.paneTopLeftColor = Color.FromArgb((int) byte.MaxValue - this.transitionAlphaChannel, this.paneTopLeftColor);
          this.paneOutlineColor = Color.FromArgb((int) byte.MaxValue - this.transitionAlphaChannel, this.paneOutlineColor);
          this.SetControlsVisible();
          break;
        }
        this.SetControlsVisible();
        this.transitionAlphaChannel = 0;
        this.SetControlsOpacity(0);
        this.paneBottomRightColor = Color.FromArgb(0, this.paneBottomRightColor);
        this.paneTopLeftColor = Color.FromArgb(0, this.paneTopLeftColor);
        this.paneOutlineColor = Color.FromArgb(0, this.paneOutlineColor);
        this.Height = this.captionHeight + this._captionOffset;
        this.expanded = false;
        this._groupState = XPCollapser.GroupState.Static;
        break;
      default:
        throw new InvalidExpressionException("groupState variable set to incorrect value");
    }
    this.Invalidate();
  }

  private void SetControlsVisible()
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      Point location = control.Location;
      control.Visible = location.Y >= this.captionHeight + this._captionOffset;
    }
  }

  private void SetControlsOpacity(int opacity)
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (!(control is TextBox))
      {
        switch (this._groupState)
        {
          case XPCollapser.GroupState.Static:
            continue;
          case XPCollapser.GroupState.Expanding:
            if (control.BackColor != Color.Transparent)
              control.BackColor = Color.FromArgb(opacity, control.BackColor);
            control.ForeColor = Color.FromArgb(opacity, control.ForeColor);
            continue;
          case XPCollapser.GroupState.Collapsing:
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

  private enum GroupState
  {
    Static,
    Expanding,
    Collapsing,
  }
}
