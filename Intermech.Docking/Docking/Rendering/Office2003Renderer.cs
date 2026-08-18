
// Type: Intermech.Docking.Rendering.Office2003Renderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

public class Office2003Renderer : RendererBase
{
  private Color _backgroundColor;
  private Color _highlightBorderColor;
  private Color _highlightBackgroundColor1;
  private Color _highlightBackgroundColor2;
  internal Color _documentStripBackgroundColor1;
  internal Color _documentStripBackgroundColor2;
  internal Color _activeDocumentBorderColor;
  internal Color _inactiveDocumentBorderColor;
  internal Color _activeDocumentHighlightColor;
  internal Color _inactiveDocumentHighlightColor;
  internal Color _activeDocumentShadowColor;
  internal Color _inactiveDocumentShadowColor;
  private Color _widgetColor;
  private Color _activeTitleBarColor1;
  private Color _activeTitleBarColor2;
  private Color _inactiveTitleBarColor1;
  private Color _inactiveTitleBarColor2;
  private Color _gripperColor;
  private Office2003Renderer.Office2003ColorScheme _colorScheme;
  private StringFormat _tabStripTextFormat;
  private StringFormat _titleBarTextFormat;
  private BoxModel _tabStripMetrics;
  private BoxModel _tabMetrics;

  public Office2003Renderer()
  {
    this._colorScheme = Office2003Renderer.Office2003ColorScheme.Automatic;
    this._tabMetrics = (BoxModel) null;
  }

  private void ApplyLunaSilverColors()
  {
    this._highlightBorderColor = Color.FromArgb(75, 75, 111);
    this._highlightBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
    this._highlightBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._backgroundColor = Color.FromArgb(243, 243, 247);
    this._inactiveTitleBarColor1 = Color.FromArgb(243, 244, 250);
    this._inactiveTitleBarColor2 = Color.FromArgb(140, 138, 172);
    this._activeTitleBarColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._activeTitleBarColor2 = Color.FromArgb(254, 145, 78);
    this._gripperColor = Color.FromArgb(84, 84, 117);
    this._documentStripBackgroundColor1 = Color.FromArgb(243, 243, 247);
    this._documentStripBackgroundColor2 = SystemColors.ControlLightLight;
    this._activeDocumentBorderColor = Color.FromArgb(124, 124, 148);
    this._inactiveDocumentBorderColor = Color.FromArgb(118, 116, 146);
    this._activeDocumentHighlightColor = SystemColors.ControlLightLight;
    this._inactiveDocumentHighlightColor = SystemColors.ControlLightLight;
    this._activeDocumentShadowColor = SystemColors.ControlLightLight;
    this._inactiveDocumentShadowColor = Color.FromArgb(186, 185, 206);
    this._widgetColor = SystemColors.ControlText;
  }

  private void DrawButtonBackground(Graphics g, Rectangle bounds, DrawItemState state)
  {
    if ((state & DrawItemState.HotLight) != DrawItemState.HotLight)
      return;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._highlightBackgroundColor1, this._highlightBackgroundColor2, LinearGradientMode.Vertical))
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    using (Pen pen = new Pen(this._highlightBorderColor))
      g.DrawRectangle(pen, bounds);
  }

  private Brush GetTitleBarBackgroundBrush(
    Rectangle bounds,
    LinearGradientMode gradientMode,
    Color color1,
    Color color2)
  {
    Color color = RendererBase.InterpolateColors(color1, color2, 0.25f);
    LinearGradientBrush barBackgroundBrush = new LinearGradientBrush(bounds, color1, color2, gradientMode);
    ColorBlend colorBlend = new ColorBlend(3);
    colorBlend.Colors = new Color[3]
    {
      color1,
      color,
      color2
    };
    float[] numArray = new float[3]{ 0.0f, 0.5f, 1f };
    colorBlend.Positions = numArray;
    barBackgroundBrush.InterpolationColors = colorBlend;
    return (Brush) barBackgroundBrush;
  }

  private void ApplyLunaOliveColors()
  {
    this._highlightBorderColor = Color.FromArgb(63 /*0x3F*/, 93, 56);
    this._highlightBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
    this._highlightBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._backgroundColor = Color.FromArgb(242, 240 /*0xF0*/, 228);
    this._inactiveTitleBarColor1 = Color.FromArgb(244, 247, 222);
    this._inactiveTitleBarColor2 = Color.FromArgb(183, 198, 145);
    this._activeTitleBarColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._activeTitleBarColor2 = Color.FromArgb(254, 145, 78);
    this._gripperColor = Color.FromArgb(81, 94, 51);
    this._documentStripBackgroundColor1 = Color.FromArgb(242, 241, 228);
    this._documentStripBackgroundColor2 = SystemColors.ControlLightLight;
    this._activeDocumentBorderColor = Color.FromArgb(96 /*0x60*/, 128 /*0x80*/, 88);
    this._inactiveDocumentBorderColor = Color.FromArgb(96 /*0x60*/, 119, 107);
    this._activeDocumentHighlightColor = SystemColors.ControlLightLight;
    this._inactiveDocumentHighlightColor = SystemColors.ControlLightLight;
    this._activeDocumentShadowColor = SystemColors.ControlLightLight;
    this._inactiveDocumentShadowColor = Color.FromArgb(176 /*0xB0*/, 194, 140);
    this._widgetColor = SystemColors.ControlText;
  }

  private void ApplyLunaBlueColors()
  {
    this._highlightBorderColor = Color.FromArgb(0, 0, 128 /*0x80*/);
    this._highlightBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
    this._highlightBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._backgroundColor = Color.FromArgb(195, 218, 249);
    this._inactiveTitleBarColor1 = Color.FromArgb(221, 236, 254);
    this._inactiveTitleBarColor2 = Color.FromArgb(129, 169, 226);
    this._activeTitleBarColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._activeTitleBarColor2 = Color.FromArgb(254, 145, 78);
    this._gripperColor = Color.FromArgb(39, 65, 118);
    this._documentStripBackgroundColor1 = Color.FromArgb(196, 218, 250);
    this._documentStripBackgroundColor2 = SystemColors.ControlLightLight;
    this._activeDocumentBorderColor = Color.FromArgb(59, 97, 156);
    this._inactiveDocumentBorderColor = Color.FromArgb(0, 53, 154);
    this._activeDocumentHighlightColor = SystemColors.ControlLightLight;
    this._inactiveDocumentHighlightColor = SystemColors.ControlLightLight;
    this._activeDocumentShadowColor = SystemColors.ControlLightLight;
    this._inactiveDocumentShadowColor = Color.FromArgb(117, 166, 241);
    this._widgetColor = SystemColors.ControlText;
  }

  protected override void CalculateBaseColors()
  {
    base.CalculateBaseColors();
    switch (this._colorScheme)
    {
      case Office2003Renderer.Office2003ColorScheme.Automatic:
        if (!Win32.IsXP())
        {
          this.ApplyStandardColors();
          break;
        }
        if (!XPThemeManager.a())
        {
          this.ApplyStandardColors();
          break;
        }
        string str1;
        if ((str1 = XPThemeManager.c()) == null)
          break;
        string str2 = string.IsInterned(str1);
        if (str2 != "NormalColor")
        {
          switch (str2)
          {
            case "HomeStead":
              this.ApplyLunaOliveColors();
              return;
            case "Metallic":
              this.ApplyLunaSilverColors();
              return;
            default:
              return;
          }
        }
        else
        {
          this.ApplyLunaBlueColors();
          break;
        }
      case Office2003Renderer.Office2003ColorScheme.Standard:
        this.ApplyStandardColors();
        break;
      case Office2003Renderer.Office2003ColorScheme.LunaBlue:
        this.ApplyLunaBlueColors();
        break;
      case Office2003Renderer.Office2003ColorScheme.LunaOlive:
        this.ApplyLunaOliveColors();
        break;
      case Office2003Renderer.Office2003ColorScheme.LunaSilver:
        this.ApplyLunaSilverColors();
        break;
    }
  }

  private void ApplyStandardColors()
  {
    this._highlightBorderColor = SystemColors.Highlight;
    this._highlightBackgroundColor1 = RendererBase.InterpolateColors(this._highlightBorderColor, SystemColors.Window, 0.7f);
    this._highlightBackgroundColor2 = this._highlightBackgroundColor1;
    this._backgroundColor = RendererBase.InterpolateColors(SystemColors.Control, SystemColors.Window, 0.8f);
    this._inactiveTitleBarColor1 = RendererBase.InterpolateColors(SystemColors.Control, Color.White, 0.8f);
    this._inactiveTitleBarColor2 = RendererBase.InterpolateColors(SystemColors.Control, Color.Black, 0.03f);
    this._activeTitleBarColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
    this._activeTitleBarColor2 = Color.FromArgb(254, 145, 78);
    this._gripperColor = SystemColors.ControlDark;
    this._documentStripBackgroundColor1 = SystemColors.Control;
    this._documentStripBackgroundColor2 = SystemColors.ControlLightLight;
    this._activeDocumentBorderColor = SystemColors.ControlDark;
    this._inactiveDocumentBorderColor = SystemColors.ControlDark;
    this._activeDocumentHighlightColor = SystemColors.ControlLightLight;
    this._inactiveDocumentHighlightColor = SystemColors.Control;
    this._activeDocumentShadowColor = SystemColors.ControlLightLight;
    this._inactiveDocumentShadowColor = SystemColors.Control;
    this._widgetColor = SystemColors.ControlText;
  }

  protected internal override void DrawCollapsedBackground(Graphics graphics, Rectangle bounds)
  {
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
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      TabRenderer.DrawCollapsedTab(graphics, bounds, dockSide, image, text, font, this._highlightBackgroundColor1, this._highlightBackgroundColor2, SystemBrushes.ControlDarkDark, this.TabTextDisplay == TabTextDisplayMode.AllTabs);
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
    graphics.Clear(this._backgroundColor);
  }

  protected internal override void DrawDocumentStripBackground(Graphics graphics, Rectangle bounds)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X, bounds.Y - 1), new Point(bounds.X, bounds.Bottom), this._documentStripBackgroundColor1, this._documentStripBackgroundColor2))
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
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
      this.DrawButtonBackground(graphics, bounds, state);
    bool flag = (state & DrawItemState.Disabled) != DrawItemState.Disabled;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    switch (buttonType)
    {
      case ButtonType.Close:
        Color color = this._widgetColor;
        if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
          color = Color.White;
        using (Pen pen = new Pen(color))
        {
          TitleButtonRenderer.DrawDocClose(graphics, bounds, pen);
          break;
        }
      case ButtonType.ScrollLeft:
        TitleButtonRenderer.DrawLeftScroll(graphics, bounds, this._widgetColor, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
        break;
      case ButtonType.ScrollRight:
        TitleButtonRenderer.DrawRightScroll(graphics, bounds, this._widgetColor, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
        break;
      case ButtonType.DocList:
        if (flag)
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
    Color A_5 = RendererBase.InterpolateColors(backColor, SystemColors.ControlLightLight, 0.78f);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      TabRenderer.DrawDocumentStripTab(graphics, bounds, image, text, font, A_5, backColor, SystemBrushes.ControlText, this._activeDocumentBorderColor, this._activeDocumentHighlightColor, this._activeDocumentShadowColor, true, this.DocumentTabSize, this.DocumentTabExtra, this._tabStripTextFormat, deltaClose);
    else
      TabRenderer.DrawDocumentStripTab(graphics, bounds, image, text, font, A_5, backColor, SystemBrushes.ControlText, this._inactiveDocumentBorderColor, this._inactiveDocumentHighlightColor, this._inactiveDocumentShadowColor, false, this.DocumentTabSize, this.DocumentTabExtra, this._tabStripTextFormat, deltaClose);
  }

  protected internal override void DrawSplitter(
    Graphics graphics,
    Rectangle bounds,
    Orientation orientation)
  {
  }

  protected internal override void DrawTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    int selectedTabOffset)
  {
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
      TabRenderer.DrawTabStripTab(graphics, bounds, image, text, font, this._highlightBackgroundColor1, this._highlightBackgroundColor2, SystemBrushes.ControlText);
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
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._activeTitleBarColor1, this._activeTitleBarColor2, LinearGradientMode.Vertical))
        graphics.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    else
    {
      using (Brush barBackgroundBrush = this.GetTitleBarBackgroundBrush(bounds, LinearGradientMode.Vertical, this._inactiveTitleBarColor1, this._inactiveTitleBarColor2))
        graphics.FillRectangle(barBackgroundBrush, bounds);
    }
    bounds.Inflate(0, -2);
    using (SolidBrush solidBrush = new SolidBrush(this._gripperColor))
    {
      int num1 = (bounds.Height - 2) / 4 * 4 - 2;
      int x = bounds.X + 3;
      int num2 = bounds.Y + bounds.Height / 2 - num1 / 2;
      for (int y = num2; y <= num2 + num1; y += 4)
      {
        graphics.FillRectangle(SystemBrushes.ControlLightLight, new Rectangle(x + 1, y + 1, 2, 2));
        graphics.FillRectangle((Brush) solidBrush, new Rectangle(x, y, 2, 2));
      }
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
    this.DrawButtonBackground(graphics, bounds, state);
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    switch (buttonType)
    {
      case ButtonType.Close:
        TitleButtonRenderer.DrawClose(graphics, bounds, focused ? SystemPens.ControlText : SystemPens.ControlText);
        break;
      case ButtonType.Pin:
        TitleButtonRenderer.DrawPin(graphics, bounds, focused ? SystemPens.ControlText : SystemPens.ControlText, toggled);
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
    using (Font font1 = new Font(font, FontStyle.Bold))
      graphics.DrawString(text, font1, SystemBrushes.ControlText, (RectangleF) bounds, this._titleBarTextFormat);
  }

  public override void FinishRenderSession()
  {
    this._tabStripTextFormat.Dispose();
    this._titleBarTextFormat.Dispose();
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
    int num2 = num1 + 16 /*0x10*/;
    if (image != null)
      num2 += RendererBase.ImageWidth(image) + 4;
    return new Size(num2 + this.DocumentTabExtra, 0);
  }

  public override void StartRenderSession()
  {
    this._tabStripTextFormat = new StringFormat(StringFormat.GenericDefault);
    this._tabStripTextFormat.FormatFlags = StringFormatFlags.NoWrap;
    this._tabStripTextFormat.Alignment = StringAlignment.Center;
    this._tabStripTextFormat.LineAlignment = StringAlignment.Center;
    this._titleBarTextFormat = new StringFormat(StringFormat.GenericDefault);
    this._titleBarTextFormat.FormatFlags = StringFormatFlags.NoWrap;
    this._titleBarTextFormat.LineAlignment = StringAlignment.Center;
    this._titleBarTextFormat.Trimming = StringTrimming.EllipsisCharacter;
  }

  public override string ToString() => "Office 2003";

  public Color ActiveDocumentBorderColor
  {
    get => this._activeDocumentBorderColor;
    set
    {
      this._activeDocumentBorderColor = value;
      this.CustomColors = true;
    }
  }

  public Color ActiveDocumentHighlightColor
  {
    get => this._activeDocumentHighlightColor;
    set
    {
      this._activeDocumentHighlightColor = value;
      this.CustomColors = true;
    }
  }

  public Color ActiveDocumentShadowColor
  {
    get => this._activeDocumentShadowColor;
    set
    {
      this._activeDocumentShadowColor = value;
      this.CustomColors = true;
    }
  }

  public Color ActiveTitleBarColor1
  {
    get => this._activeTitleBarColor1;
    set
    {
      this._activeTitleBarColor1 = value;
      this.CustomColors = true;
    }
  }

  public Color ActiveTitleBarColor2
  {
    get => this._activeTitleBarColor2;
    set
    {
      this._activeTitleBarColor2 = value;
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

  public Office2003Renderer.Office2003ColorScheme ColorScheme
  {
    get => this._colorScheme;
    set
    {
      this._colorScheme = value;
      this.CalculateBaseColors();
    }
  }

  protected internal override Size ControlClientPadding => new Size(0, 0);

  protected internal override Size DocumentClientPadding => new Size(4, 4);

  public Color DocumentStripBackgroundColor1
  {
    get => this._documentStripBackgroundColor1;
    set
    {
      this._documentStripBackgroundColor1 = value;
      this.CustomColors = true;
    }
  }

  public Color DocumentStripBackgroundColor2
  {
    get => this._documentStripBackgroundColor2;
    set
    {
      this._documentStripBackgroundColor2 = value;
      this.CustomColors = true;
    }
  }

  protected internal override int DocumentTabExtra => 16 /*0x10*/;

  protected internal override int DocumentTabSize => Control.DefaultFont.Height + 7;

  protected internal override int DocumentTabStripSize => Control.DefaultFont.Height + 15;

  public Color GripperColor
  {
    get => this._gripperColor;
    set
    {
      this._gripperColor = value;
      this.CustomColors = true;
    }
  }

  public Color HighlightBackgroundColor1
  {
    get => this._highlightBackgroundColor1;
    set
    {
      this._highlightBackgroundColor1 = value;
      this.CustomColors = true;
    }
  }

  public Color HighlightBackgroundColor2
  {
    get => this._highlightBackgroundColor2;
    set
    {
      this._highlightBackgroundColor2 = value;
      this.CustomColors = true;
    }
  }

  public Color HighlightBorderColor
  {
    get => this._highlightBorderColor;
    set
    {
      this._highlightBorderColor = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveDocumentBorderColor
  {
    get => this._inactiveDocumentBorderColor;
    set
    {
      this._inactiveDocumentBorderColor = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveDocumentHighlightColor
  {
    get => this._inactiveDocumentHighlightColor;
    set
    {
      this._inactiveDocumentHighlightColor = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveDocumentShadowColor
  {
    get => this._inactiveDocumentShadowColor;
    set
    {
      this._inactiveDocumentShadowColor = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveTitleBarColor1
  {
    get => this._inactiveTitleBarColor1;
    set
    {
      this._inactiveTitleBarColor1 = value;
      this.CustomColors = true;
    }
  }

  public Color InactiveTitleBarColor2
  {
    get => this._inactiveTitleBarColor2;
    set
    {
      this._inactiveTitleBarColor2 = value;
      this.CustomColors = true;
    }
  }

  protected internal override BoxModel TabMetrics
  {
    get
    {
      if (this._tabMetrics == null)
        this._tabMetrics = new BoxModel(0, 0, 0, 0, 0, 0, 0, 0, -1, 0);
      return this._tabMetrics;
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

  protected internal override TabTextDisplayMode TabTextDisplay => TabTextDisplayMode.SelectedTab;

  protected internal override BoxModel TitleBarMetrics
  {
    get => new BoxModel(0, 25, 4, 0, 0, 0, 0, 0, 0, 1);
  }

  public Color WidgetColor
  {
    get => this._widgetColor;
    set
    {
      this._widgetColor = value;
      this.CustomColors = true;
    }
  }

  public enum Office2003ColorScheme
  {
    Automatic,
    Standard,
    LunaBlue,
    LunaOlive,
    LunaSilver,
  }
}
