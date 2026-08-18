
// Type: Intermech.Docking.Rendering.WhidbeyRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

public class WhidbeyRenderer : RendererBase
{
  private Color _tabOutlineColor;
  private Color _b;
  private Color _activeDocumentBorderColor;
  private Color _inactiveDocumentBorderColor;
  private Color _activeDocumentHighlightColor;
  private Color _inactiveDocumentHighlightColor;
  private Color _activeDocumentShadowColor;
  private Color _inactiveDocumentShadowColor;
  private Color _documentStripBackgroundColor;
  private StringFormat _documentStripTabTextFormat;
  private BoxModel _tabStripMetrics;
  private BoxModel _tabMetrics;
  private BoxModel _titleBarMetrics;

  public WhidbeyRenderer()
  {
    this._tabOutlineColor = SystemColors.ControlDark;
    this._b = SystemColors.ControlDarkDark;
    this._tabStripMetrics = (BoxModel) null;
    this._tabMetrics = (BoxModel) null;
    this._titleBarMetrics = (BoxModel) null;
    this._activeDocumentBorderColor = Color.FromArgb((int) sbyte.MaxValue, 157, 185);
    this._inactiveDocumentBorderColor = SystemColors.ControlDark;
    this._activeDocumentHighlightColor = SystemColors.ControlLightLight;
    this._inactiveDocumentHighlightColor = SystemColors.ControlLightLight;
    this._activeDocumentShadowColor = SystemColors.ControlLightLight;
    this._inactiveDocumentShadowColor = SystemColors.Control;
    this._documentStripBackgroundColor = Color.FromArgb(231, 227, 214);
    this.CalculateBaseColors();
  }

  protected override void CalculateBaseColors() => base.CalculateBaseColors();

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
      pen1 = SystemPens.ControlDark;
      pen2 = SystemPens.ControlLightLight;
    }
    else
    {
      pen2 = SystemPens.ControlDark;
      pen1 = SystemPens.ControlLightLight;
    }
    graphics.DrawLine(pen1, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
    graphics.DrawLine(pen1, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
    graphics.DrawLine(pen2, bounds.Right - 1, bounds.Bottom - 1, bounds.Right - 1, bounds.Top);
    graphics.DrawLine(pen2, bounds.Right - 1, bounds.Bottom - 1, bounds.Left, bounds.Bottom - 1);
  }

  protected internal override void DrawCollapsedBackground(Graphics graphics, Rectangle bounds)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, SystemColors.Control, SystemColors.Window, LinearGradientMode.Horizontal))
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
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
    if (dockSide == DockSide.Left || dockSide == DockSide.Right)
    {
      using (Image image1 = (Image) new Bitmap(image))
      {
        image1.RotateFlip(RotateFlipType.Rotate90FlipNone);
        TabRenderer.DrawCollapsedTab(graphics, bounds, dockSide, image1, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlDarkDark, this.TabTextDisplay == TabTextDisplayMode.AllTabs);
      }
    }
    else
      TabRenderer.DrawCollapsedTab(graphics, bounds, dockSide, image, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlDarkDark, this.TabTextDisplay == TabTextDisplayMode.AllTabs);
  }

  protected internal override void DrawControlClientBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor)
  {
  }

  protected internal override void DrawDockContainerBackground(Graphics graphics, Rectangle bounds)
  {
    graphics.Clear(SystemColors.Control);
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
    graphics.Clear(SystemColors.AppWorkspace);
  }

  protected internal override void DrawDocumentStripBackground(Graphics graphics, Rectangle bounds)
  {
    using (SolidBrush solidBrush = new SolidBrush(this._documentStripBackgroundColor))
      graphics.FillRectangle((Brush) solidBrush, bounds);
    using (Pen pen = new Pen(this._activeDocumentBorderColor))
      graphics.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
  }

  protected internal override void DrawDocumentStripButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state)
  {
    if (buttonType == ButtonType.Close)
      TitleButtonRenderer.DrawCloseButtonBackground(graphics, bounds, state);
    else
      this.DrawButtonHighlight(graphics, bounds, state);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    bool enabled = (state & DrawItemState.Disabled) != DrawItemState.Disabled;
    switch (buttonType)
    {
      case ButtonType.Close:
        Color color = SystemColors.ControlText;
        if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
          color = Color.White;
        using (Pen pen = new Pen(color))
        {
          TitleButtonRenderer.DrawDocClose(graphics, bounds, pen);
          break;
        }
      case ButtonType.ScrollLeft:
        TitleButtonRenderer.DrawLeftScroll(graphics, bounds, SystemColors.ControlText, enabled);
        break;
      case ButtonType.ScrollRight:
        TitleButtonRenderer.DrawRightScroll(graphics, bounds, SystemColors.ControlText, enabled);
        break;
      case ButtonType.DocList:
        if (enabled)
        {
          TitleButtonRenderer.DrawDocList(graphics, bounds, SystemPens.ControlText);
          break;
        }
        TitleButtonRenderer.DrawDocList(graphics, bounds, SystemPens.GrayText);
        break;
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
      TabRenderer.DrawDocumentStripTab(graphics, bounds, image, text, font, SystemColors.ControlLightLight, SystemColors.ControlLight, SystemBrushes.ControlText, this._activeDocumentBorderColor, this._activeDocumentHighlightColor, this._activeDocumentShadowColor, true, this.DocumentTabSize, this.DocumentTabExtra, this._documentStripTabTextFormat, deltaClose);
    else
      TabRenderer.DrawDocumentStripTab(graphics, bounds, image, text, font, backColor, backColor, SystemBrushes.ControlText, this._inactiveDocumentBorderColor, this._inactiveDocumentHighlightColor, this._inactiveDocumentShadowColor, false, this.DocumentTabSize, this.DocumentTabExtra, this._documentStripTabTextFormat, deltaClose);
  }

  protected internal override void DrawSplitter(
    Graphics graphics,
    Rectangle bounds,
    Orientation orientation)
  {
    if (bounds.IsEmpty)
      return;
    LinearGradientMode linearGradientMode = LinearGradientMode.Horizontal;
    if (orientation == Orientation.Horizontal)
      linearGradientMode = LinearGradientMode.Vertical;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, SystemColors.Control, Color.WhiteSmoke, linearGradientMode))
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
  }

  protected internal override void DrawTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    int selectedTabOffset)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X - 1, bounds.Y), new Point(bounds.Right, bounds.Y), SystemColors.Control, Color.WhiteSmoke))
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
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
      TabRenderer.DrawTabStripTab(graphics, bounds, image, text, font, SystemColors.ControlLightLight, SystemColors.ControlLightLight, SystemBrushes.ControlText);
    else
      TabRenderer.DrawTabStripTab(graphics, bounds, image, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlText);
  }

  protected internal override void DrawTitleBarBackground(
    Graphics graphics,
    Rectangle bounds,
    bool focused)
  {
    if (focused)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X - 1, bounds.Y), new Point(bounds.Right, bounds.Y), SystemColors.ActiveCaption, Win32.GradientActiveCaption()))
        graphics.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    else
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X - 1, bounds.Y), new Point(bounds.Right, bounds.Y), SystemColors.ControlDark, SystemColors.ControlLight))
        graphics.FillRectangle((Brush) linearGradientBrush, bounds);
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
    bounds.Inflate(-3, 0);
    if (focused)
    {
      graphics.DrawString(text, font, SystemBrushes.ActiveCaptionText, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
    }
    else
    {
      using (SolidBrush solidBrush = new SolidBrush(SystemColors.ControlText))
        graphics.DrawString(text, font, (Brush) solidBrush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
    }
  }

  public override void FinishRenderSession() => this._documentStripTabTextFormat.Dispose();

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
        num1 = (int) Math.Ceiling((double) graphics.MeasureString(text, font1, 999, this._documentStripTabTextFormat).Width + (double) graphics.MeasureString(text1, font, 999, this._documentStripTabTextFormat).Width);
    }
    else
      num1 = (int) Math.Ceiling((double) graphics.MeasureString(text, font, 999, this._documentStripTabTextFormat).Width + (double) graphics.MeasureString(text1, font, 999, this._documentStripTabTextFormat).Width);
    int num2 = num1 + 8;
    if (image != null)
      num2 += RendererBase.ImageWidth(image) + 4;
    return new Size(num2 + this.DocumentTabExtra, 0);
  }

  public override void StartRenderSession()
  {
    this._documentStripTabTextFormat = new StringFormat(StringFormat.GenericDefault);
    this._documentStripTabTextFormat.FormatFlags = StringFormatFlags.NoWrap;
    this._documentStripTabTextFormat.Alignment = StringAlignment.Center;
    this._documentStripTabTextFormat.LineAlignment = StringAlignment.Center;
    this._documentStripTabTextFormat.Trimming = StringTrimming.EllipsisCharacter;
  }

  public override string ToString() => "Whidbey";

  protected internal override Size ControlClientPadding => new Size(0, 0);

  protected internal override Size DocumentClientPadding => new Size(2, 2);

  public Color DocumentStripBackgroundColor
  {
    get => this._documentStripBackgroundColor;
    set
    {
      this._documentStripBackgroundColor = value;
      this.CustomColors = true;
    }
  }

  protected internal override int DocumentTabExtra => 18;

  protected internal override int DocumentTabSize => Control.DefaultFont.Height + 7;

  protected internal override int DocumentTabStripSize => Control.DefaultFont.Height + 9;

  protected internal override BoxModel TabMetrics
  {
    get
    {
      if (this._tabMetrics == null)
        this._tabMetrics = new BoxModel(0, 0, 0, 0, 0, 0, 0, 0, -1, 0);
      return this._tabMetrics;
    }
  }

  public Color TabOutlineColor
  {
    get => this._tabOutlineColor;
    set
    {
      this._tabOutlineColor = value;
      this.CustomColors = true;
    }
  }

  protected internal override BoxModel TabStripMetrics
  {
    get
    {
      if (this._tabStripMetrics == null)
        this._tabStripMetrics = new BoxModel(0, Control.DefaultFont.Height + 10, 0, 0, 0, 1, 0, 0, 0, 0);
      return this._tabStripMetrics;
    }
  }

  protected internal override TabTextDisplayMode TabTextDisplay => TabTextDisplayMode.AllTabs;

  protected internal override BoxModel TitleBarMetrics
  {
    get
    {
      if (this._titleBarMetrics == null)
        this._titleBarMetrics = new BoxModel(0, SystemInformation.ToolWindowCaptionHeight, 0, 0, 0, 0, 0, 0, 0, 0);
      return this._titleBarMetrics;
    }
  }
}
