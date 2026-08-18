
// Type: Intermech.Docking.Rendering.EverettRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

public class EverettRenderer : RendererBase
{
  private static StringFormat _standardStringFormat = (StringFormat) null;
  private static StringFormat _standardVerticalStringFormat = (StringFormat) null;
  private static StringFormat _documentCaptionStringFormat;
  private Color _inactiveTitleBarColor;
  private Color _activeTitleBarColor;
  private Color _backgroundColor;
  private Color _documentContainerBackgroundColor;
  private Color _shadowColor;
  private Color _highlightColor;
  private Color _backgroundTabForeColor;
  private Color _collapsedTabOutlineColor;
  private Color _buttonPictogramColor;
  private Color _tabStripBackgroundColor;
  private SolidBrush _documentStripBackgroundColor;
  private Pen _shadowPen;
  private Pen _highlightPen;
  private Pen _backgroundPen;
  private Pen _collapsedTabOutlinePen;
  private SolidBrush _backgroundTabForeBrush;
  private BoxModel _tabStripMetrics;
  private BoxModel _tabMetrics;
  private BoxModel _titleBarMetrics;

  public EverettRenderer()
  {
    this._inactiveTitleBarColor = SystemColors.InactiveCaption;
    this._activeTitleBarColor = SystemColors.ActiveCaption;
    this._backgroundColor = SystemColors.Control;
    this._documentContainerBackgroundColor = SystemColors.AppWorkspace;
    this._shadowColor = SystemColors.ControlText;
    this._highlightColor = SystemColors.ControlLightLight;
    this._backgroundTabForeColor = SystemColors.ControlDarkDark;
    this._collapsedTabOutlineColor = SystemColors.ControlDark;
    this._buttonPictogramColor = SystemColors.ControlDarkDark;
    this._documentStripBackgroundColor = (SolidBrush) null;
    this._tabStripMetrics = (BoxModel) null;
    this._tabMetrics = (BoxModel) null;
    this._titleBarMetrics = (BoxModel) null;
    this.CalculateBaseColors();
  }

  private Color CalcTabStripBackgroundColor(Color A_0)
  {
    byte r = A_0.R;
    byte g = A_0.G;
    byte b = A_0.B;
    byte num1 = Math.Max(Math.Max(r, g), b);
    if (num1 == (byte) 0)
      return Color.FromArgb(35, 35, 35);
    byte num2 = num1 > (byte) 220 ? (byte) ((uint) byte.MaxValue - (uint) num1) : (byte) 35;
    return Color.FromArgb((int) (byte) ((uint) r + (uint) (byte) ((double) num2 * ((double) r / (double) num1) + 0.5)), (int) (byte) ((uint) g + (uint) (byte) ((double) num2 * ((double) g / (double) num1) + 0.5)), (int) (byte) ((uint) b + (uint) (byte) ((double) num2 * ((double) b / (double) num1) + 0.5)));
  }

  protected override void CalculateBaseColors()
  {
    this._tabStripBackgroundColor = this.CalcTabStripBackgroundColor(this.BackgroundColor);
  }

  internal virtual void DrawButtonHighlight(
    Graphics graphics,
    Rectangle bounds,
    DrawItemState state)
  {
    if ((state & DrawItemState.HotLight) != DrawItemState.HotLight)
      return;
    Pen pen1;
    Pen pen2;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      pen1 = this._shadowPen;
      pen2 = this._highlightPen;
    }
    else
    {
      pen2 = this._shadowPen;
      pen1 = this._highlightPen;
    }
    graphics.DrawLine(pen1, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
    graphics.DrawLine(pen1, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
    graphics.DrawLine(pen2, bounds.Right - 1, bounds.Bottom - 1, bounds.Right - 1, bounds.Top);
    graphics.DrawLine(pen2, bounds.Right - 1, bounds.Bottom - 1, bounds.Left, bounds.Bottom - 1);
  }

  protected internal override void DrawCollapsedBackground(Graphics graphics, Rectangle bounds)
  {
    graphics.FillRectangle((Brush) this._documentStripBackgroundColor, bounds);
  }

  protected internal override void DrawCollapsedTab(
    Graphics graphics,
    Rectangle bounds,
    DockSide dockSide,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool vertical)
  {
    using (SolidBrush solidBrush = new SolidBrush(backColor))
      graphics.FillRectangle((Brush) solidBrush, bounds);
    if (dockSide != DockSide.Top)
      graphics.DrawLine(this._collapsedTabOutlinePen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
    if (dockSide != DockSide.Right)
      graphics.DrawLine(this._collapsedTabOutlinePen, bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
    if (dockSide != DockSide.Bottom)
      graphics.DrawLine(this._collapsedTabOutlinePen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
    if (dockSide != DockSide.Left)
      graphics.DrawLine(this._collapsedTabOutlinePen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
    bounds.Inflate(-2, -2);
    if (vertical)
      bounds.Offset(0, 1);
    else
      bounds.Offset(1, 0);
    graphics.DrawImage(image, new Rectangle(bounds.Left, bounds.Top, image.Width, image.Height));
    if (text.Length == 0)
      return;
    if (vertical)
    {
      bounds.Offset(0, 23);
      graphics.DrawString(text, font, (Brush) this._backgroundTabForeBrush, (RectangleF) bounds, EverettRenderer._standardVerticalStringFormat);
    }
    else
    {
      bounds.Offset(23, 0);
      graphics.DrawString(text, font, (Brush) this._backgroundTabForeBrush, (RectangleF) bounds, EverettRenderer._standardStringFormat);
    }
  }

  protected internal override void DrawControlClientBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor)
  {
  }

  protected internal override void DrawDockContainerBackground(Graphics graphics, Rectangle bounds)
  {
    graphics.Clear(this._backgroundColor);
  }

  protected internal override void DrawDocumentClientBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor)
  {
    using (SolidBrush solidBrush = new SolidBrush(backColor))
      graphics.FillRectangle((Brush) solidBrush, bounds);
  }

  protected internal override void DrawDocumentContainerBackground(
    Graphics graphics,
    Rectangle bounds)
  {
    graphics.Clear(this._documentContainerBackgroundColor);
  }

  protected internal override void DrawDocumentStripBackground(Graphics graphics, Rectangle bounds)
  {
    try
    {
      graphics.FillRectangle((Brush) this._documentStripBackgroundColor, bounds);
      graphics.DrawLine(this._highlightPen, bounds.X, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
    }
    catch
    {
    }
  }

  protected internal override void DrawDocumentStripButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state)
  {
    this.DrawButtonHighlight(graphics, bounds, state);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    switch (buttonType)
    {
      case ButtonType.Close:
        using (Pen pen = new Pen(this._buttonPictogramColor))
        {
          TitleButtonRenderer.DrawDocClose(graphics, bounds, pen);
          break;
        }
      case ButtonType.ScrollLeft:
        TitleButtonRenderer.DrawLeftScroll(graphics, bounds, this._buttonPictogramColor, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
        break;
      case ButtonType.ScrollRight:
        TitleButtonRenderer.DrawRightScroll(graphics, bounds, this._buttonPictogramColor, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
        break;
      case ButtonType.DocList:
        using (Pen pen = new Pen(this._buttonPictogramColor))
        {
          TitleButtonRenderer.DrawDocList(graphics, bounds, pen);
          break;
        }
    }
  }

  protected internal override void DrawDocumentStripTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool drawSeparator,
    int deltaClose)
  {
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      using (SolidBrush solidBrush = new SolidBrush(backColor))
        graphics.FillRectangle((Brush) solidBrush, bounds);
      graphics.DrawLine(this._highlightPen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
      graphics.DrawLine(this._highlightPen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
      graphics.DrawLine(this._shadowPen, bounds.Right - 1, bounds.Top + 1, bounds.Right - 1, bounds.Bottom - 1);
    }
    else if (drawSeparator)
      graphics.DrawLine(SystemPens.ControlDark, bounds.Right, bounds.Top + 3, bounds.Right, bounds.Bottom - 3);
    bounds.X += this.DocumentTabPadding;
    bounds.Width -= this.DocumentTabPadding;
    if (image != null)
    {
      graphics.DrawImage(image, bounds.X, bounds.Y + 2, image.Width, 16 /*0x10*/);
      int num = RendererBase.ImageWidth(image) + 4;
      bounds.X += num;
      bounds.Width -= num;
    }
    bounds.Width -= deltaClose;
    if (bounds.Width <= 8)
      return;
    string tabTextSeparator = DockingConsts.TabTextSeparator;
    string str = string.Empty;
    int length = text.IndexOf(tabTextSeparator);
    if (length >= 0)
    {
      str = text.Substring(length + tabTextSeparator.Length);
      text = length <= 0 ? string.Empty : text.Substring(0, length);
    }
    Font font1 = font;
    Font font2 = (Font) null;
    SolidBrush solidBrush1 = (SolidBrush) null;
    if (!string.IsNullOrEmpty(str))
      solidBrush1 = new SolidBrush(Color.Red);
    if ((state & DrawItemState.Focus) == DrawItemState.Focus)
      font1 = new Font(font, FontStyle.Bold);
    SizeF sizeF = graphics.MeasureString(str, font1, 999, EverettRenderer.DocumentCaptionStringFormat);
    int width1 = Convert.ToInt32(sizeF.Width) + 10;
    sizeF = graphics.MeasureString(text, font1, 999, EverettRenderer.DocumentCaptionStringFormat);
    int width2 = Convert.ToInt32(sizeF.Width) + 10;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      width2 = bounds.Width - width1;
    RectangleF layoutRectangle1 = new RectangleF((float) bounds.X, (float) bounds.Y, (float) width2, (float) bounds.Height);
    RectangleF layoutRectangle2 = new RectangleF((float) (bounds.X + width2), (float) bounds.Y, (float) width1, (float) bounds.Height);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      using (SolidBrush solidBrush2 = new SolidBrush(foreColor))
      {
        if (!string.IsNullOrEmpty(str))
        {
          graphics.DrawString(text, font1, (Brush) solidBrush2, layoutRectangle1, EverettRenderer.DocumentCaptionStringFormat);
          graphics.DrawString(str, font1, (Brush) solidBrush1, layoutRectangle2, EverettRenderer.DocumentCaptionStringFormat);
        }
        else
          graphics.DrawString(text, font1, (Brush) solidBrush2, (RectangleF) bounds, EverettRenderer.DocumentCaptionStringFormat);
      }
    }
    else if (!string.IsNullOrEmpty(str))
    {
      graphics.DrawString(text, font1, (Brush) this._backgroundTabForeBrush, layoutRectangle1, EverettRenderer.DocumentCaptionStringFormat);
      graphics.DrawString(str, font1, (Brush) this._backgroundTabForeBrush, layoutRectangle2, EverettRenderer.DocumentCaptionStringFormat);
    }
    else
      graphics.DrawString(text, font1, (Brush) this._backgroundTabForeBrush, (RectangleF) bounds, EverettRenderer.DocumentCaptionStringFormat);
    font2?.Dispose();
    solidBrush1?.Dispose();
    if ((state & DrawItemState.Focus) != DrawItemState.Focus)
      return;
    font1.Dispose();
  }

  protected internal override void DrawSplitter(
    Graphics graphics,
    Rectangle bounds,
    Orientation orientation)
  {
    graphics.FillRectangle(SystemBrushes.Control, bounds);
  }

  protected internal override void DrawTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    int selectedTabOffset)
  {
    graphics.FillRectangle((Brush) this._documentStripBackgroundColor, bounds);
    graphics.DrawLine(this._shadowPen, bounds.X, bounds.Y, bounds.Right, bounds.Y);
  }

  protected internal override void DrawTabStripTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool drawSeparator)
  {
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      using (SolidBrush solidBrush = new SolidBrush(backColor))
        graphics.FillRectangle((Brush) solidBrush, bounds);
      graphics.DrawLine(this._highlightPen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
      graphics.DrawLine(this._shadowPen, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
      graphics.DrawLine(this._shadowPen, bounds.Right, bounds.Top, bounds.Right, bounds.Bottom - 1);
    }
    else if (drawSeparator)
      graphics.DrawLine(SystemPens.ControlDark, bounds.Right, bounds.Top + 3, bounds.Right, bounds.Bottom - 3);
    if (bounds.Width >= 24)
      graphics.DrawImage(image, new Rectangle(bounds.X + 4, bounds.Y + 2, image.Width, image.Height));
    bounds.X += 23;
    bounds.Width -= 25;
    if (bounds.Width <= 8)
      return;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      using (SolidBrush solidBrush = new SolidBrush(foreColor))
        graphics.DrawString(text, font, (Brush) solidBrush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
    }
    else
      graphics.DrawString(text, font, (Brush) this._backgroundTabForeBrush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
  }

  protected internal override void DrawTitleBarBackground(
    Graphics graphics,
    Rectangle bounds,
    bool focused)
  {
    if (focused)
    {
      graphics.FillRectangle(SystemBrushes.ActiveCaption, bounds);
    }
    else
    {
      graphics.FillRectangle(SystemBrushes.Control, bounds);
      graphics.DrawLine(SystemPens.ControlDark, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
      graphics.DrawLine(SystemPens.ControlDark, bounds.X + 1, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
      graphics.DrawLine(SystemPens.ControlDark, bounds.X, bounds.Y + 1, bounds.X, bounds.Bottom - 2);
      graphics.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Y + 1, bounds.Right - 1, bounds.Bottom - 2);
    }
  }

  protected internal override void DrawTitleBarButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state,
    bool focused,
    bool toggled)
  {
    --bounds.Width;
    --bounds.Height;
    this.DrawButtonHighlight(graphics, bounds, state);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    switch (buttonType)
    {
      case ButtonType.Close:
        TitleButtonRenderer.DrawClose(graphics, bounds, focused ? SystemPens.ActiveCaptionText : SystemPens.ControlText);
        break;
      case ButtonType.Pin:
        TitleButtonRenderer.DrawPin(graphics, bounds, focused ? SystemPens.ActiveCaptionText : SystemPens.ControlText, toggled);
        break;
    }
  }

  protected internal override void DrawTitleBarText(
    Graphics graphics,
    Rectangle bounds,
    bool focused,
    string text,
    Font font)
  {
    Brush brush = focused ? SystemBrushes.ActiveCaptionText : SystemBrushes.ControlText;
    bounds.Inflate(-3, 0);
    graphics.DrawString(text, font, brush, (RectangleF) bounds, EverettRenderer._standardStringFormat);
  }

  public override void FinishRenderSession()
  {
    this._documentStripBackgroundColor.Dispose();
    this._documentStripBackgroundColor = (SolidBrush) null;
    this._shadowPen.Dispose();
    this._shadowPen = (Pen) null;
    this._backgroundPen.Dispose();
    this._backgroundPen = (Pen) null;
    this._highlightPen.Dispose();
    this._highlightPen = (Pen) null;
    this._backgroundTabForeBrush.Dispose();
    this._backgroundTabForeBrush = (SolidBrush) null;
    this._collapsedTabOutlinePen.Dispose();
    this._collapsedTabOutlinePen = (Pen) null;
  }

  protected internal override Size MeasureDocumentStripTab(
    Graphics graphics,
    Image image,
    string text,
    Font font,
    DrawItemState state)
  {
    string tabTextSeparator = DockingConsts.TabTextSeparator;
    string text1 = string.Empty;
    int length = text.IndexOf(tabTextSeparator);
    if (length >= 0)
    {
      text1 = text.Substring(length + tabTextSeparator.Length);
      text = length <= 0 ? string.Empty : text.Substring(0, length);
    }
    int num1;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected || (state & DrawItemState.Focus) == DrawItemState.Focus)
    {
      using (Font font1 = new Font(font, FontStyle.Bold))
        num1 = (int) Math.Ceiling((double) graphics.MeasureString(text, font1, 999, EverettRenderer.DocumentCaptionStringFormat).Width + (double) graphics.MeasureString(text1, font, 999, EverettRenderer.DocumentCaptionStringFormat).Width);
    }
    else
      num1 = (int) Math.Ceiling((double) graphics.MeasureString(text, font, 999, EverettRenderer.DocumentCaptionStringFormat).Width + (double) graphics.MeasureString(text1, font, 999, EverettRenderer.DocumentCaptionStringFormat).Width);
    int num2 = num1 + (2 + this.DocumentTabPadding * 2 + 2);
    if (image != null)
      num2 += RendererBase.ImageWidth(image) + 4;
    return new Size(num2 + this.DocumentTabExtra, 0);
  }

  public override void StartRenderSession()
  {
    this._documentStripBackgroundColor = new SolidBrush(this._tabStripBackgroundColor);
    this._shadowPen = new Pen(this._shadowColor);
    this._backgroundPen = new Pen(this._backgroundColor);
    this._highlightPen = new Pen(this._highlightColor);
    this._backgroundTabForeBrush = new SolidBrush(this._backgroundTabForeColor);
    this._collapsedTabOutlinePen = new Pen(this._collapsedTabOutlineColor);
  }

  public override string ToString() => "Everett";

  public Color ActiveTitleBarColor
  {
    get => this._activeTitleBarColor;
    set
    {
      this._activeTitleBarColor = value;
      this.CustomColors = true;
    }
  }

  public Color BackgroundColor
  {
    get => this._backgroundColor;
    set
    {
      this._backgroundColor = value;
      this.CustomColors = true;
    }
  }

  public Color BackgroundTabForeColor
  {
    get => this._backgroundTabForeColor;
    set => this._backgroundTabForeColor = value;
  }

  public Color CollapsedTabOutlineColor
  {
    get => this._collapsedTabOutlineColor;
    set
    {
      this._collapsedTabOutlineColor = value;
      this.CustomColors = true;
    }
  }

  protected internal override Size ControlClientPadding => new Size(0, 0);

  protected internal override Size DocumentClientPadding => new Size(2, 2);

  public Color DocumentContainerBackgroundColor
  {
    get => this._documentContainerBackgroundColor;
    set
    {
      this._documentContainerBackgroundColor = value;
      this.CustomColors = true;
    }
  }

  protected internal override int DocumentTabExtra => 0;

  internal virtual int DocumentTabPadding => 4;

  protected internal override int DocumentTabSize => Control.DefaultFont.Height + 6;

  protected internal override int DocumentTabStripSize => Control.DefaultFont.Height + 8;

  public Color HighlightColor
  {
    get => this._highlightColor;
    set
    {
      this._highlightColor = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveTitleBarColor
  {
    get => this._inactiveTitleBarColor;
    set
    {
      this._inactiveTitleBarColor = value;
      this.CustomColors = true;
    }
  }

  public Color ShadowColor
  {
    get => this._shadowColor;
    set
    {
      this._shadowColor = value;
      this.CustomColors = true;
    }
  }

  internal static StringFormat StandardStringFormat
  {
    get
    {
      if (EverettRenderer._standardStringFormat == null)
      {
        EverettRenderer._standardStringFormat = new StringFormat(StringFormat.GenericDefault)
        {
          Alignment = StringAlignment.Near,
          LineAlignment = StringAlignment.Center,
          Trimming = StringTrimming.EllipsisCharacter
        };
        EverettRenderer._standardStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
      }
      return EverettRenderer._standardStringFormat;
    }
  }

  internal static StringFormat DocumentCaptionStringFormat
  {
    get
    {
      if (EverettRenderer._documentCaptionStringFormat == null)
        EverettRenderer._documentCaptionStringFormat = new StringFormat(StringFormat.GenericDefault)
        {
          FormatFlags = StringFormatFlags.NoWrap,
          Alignment = StringAlignment.Near,
          LineAlignment = StringAlignment.Center,
          Trimming = StringTrimming.EllipsisCharacter
        };
      return EverettRenderer._documentCaptionStringFormat;
    }
  }

  internal static StringFormat GetStandardVerticalStringFormat()
  {
    if (EverettRenderer._standardVerticalStringFormat == null)
    {
      EverettRenderer._standardVerticalStringFormat = new StringFormat(StringFormat.GenericDefault)
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center,
        Trimming = StringTrimming.EllipsisCharacter
      };
      EverettRenderer._standardVerticalStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
      EverettRenderer._standardVerticalStringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
    }
    return EverettRenderer._standardVerticalStringFormat;
  }

  protected internal override BoxModel TabMetrics
  {
    get
    {
      if (this._tabMetrics == null)
        this._tabMetrics = new BoxModel(0, 0, 0, 0, 0, 0, 0, 0, 1, 0);
      return this._tabMetrics;
    }
  }

  public Color TabStripBackgroundColor => this._tabStripBackgroundColor;

  protected internal override BoxModel TabStripMetrics
  {
    get
    {
      if (this._tabStripMetrics == null)
        this._tabStripMetrics = new BoxModel(0, Control.DefaultFont.Height + 9, 4, 0, 5, 1, 0, 1, 0, 0);
      return this._tabStripMetrics;
    }
  }

  protected internal override TabTextDisplayMode TabTextDisplay => TabTextDisplayMode.SelectedTab;

  protected internal override BoxModel TitleBarMetrics
  {
    get
    {
      if (this._titleBarMetrics == null)
        this._titleBarMetrics = new BoxModel(0, SystemInformation.ToolWindowCaptionHeight + 2, 0, 0, 0, 0, 0, 0, 0, 2);
      return this._titleBarMetrics;
    }
  }
}
