
// Type: Intermech.Docking.Rendering.RendererBase
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

[TypeConverter("Intermech.Docking.Rendering.RendererBaseConverter")]
public abstract class RendererBase : IDisposable
{
  private bool _customColors;

  public RendererBase()
  {
    this._customColors = false;
    SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
    this.CalculateBaseColors();
  }

  private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs up)
  {
    if (up.Category != UserPreferenceCategory.Color || this._customColors)
      return;
    this.CalculateBaseColors();
  }

  protected virtual void CalculateBaseColors()
  {
  }

  public void Dispose()
  {
    SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
  }

  public abstract void StartRenderSession();

  public abstract void FinishRenderSession();

  protected internal abstract void DrawCollapsedBackground(Graphics graphics, Rectangle bounds);

  protected internal abstract void DrawCollapsedTab(
    Graphics graphics,
    Rectangle bounds,
    DockSide dockSide,
    Image image,
    string text,
    Font font,
    System.Drawing.Color backColor,
    System.Drawing.Color foreColor,
    DrawItemState state,
    bool vertical);

  protected internal abstract void DrawControlClientBackground(
    Graphics graphics,
    Rectangle bounds,
    System.Drawing.Color backColor);

  protected internal abstract void DrawDockContainerBackground(Graphics graphics, Rectangle bounds);

  protected internal abstract void DrawDocumentClientBackground(
    Graphics graphics,
    Rectangle bounds,
    System.Drawing.Color backColor);

  protected internal abstract void DrawDocumentContainerBackground(
    Graphics graphics,
    Rectangle bounds);

  protected internal abstract void DrawDocumentStripBackground(Graphics graphics, Rectangle bounds);

  protected internal abstract void DrawDocumentStripButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state);

  protected internal abstract void DrawDocumentStripTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    System.Drawing.Color backColor,
    System.Drawing.Color foreColor,
    DrawItemState state,
    bool drawSeparator,
    int deltaClose);

  protected internal abstract void DrawSplitter(
    Graphics graphics,
    Rectangle bounds,
    Orientation orientation);

  protected internal abstract void DrawTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    int selectedTabOffset);

  protected internal abstract void DrawTabStripTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    System.Drawing.Color backColor,
    System.Drawing.Color foreColor,
    DrawItemState state,
    bool drawSeparator);

  protected internal abstract void DrawTitleBarBackground(
    Graphics graphics,
    Rectangle bounds,
    bool focused);

  protected internal abstract void DrawTitleBarButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state,
    bool focused,
    bool toggled);

  protected internal abstract void DrawTitleBarText(
    Graphics graphics,
    Rectangle bounds,
    bool focused,
    string text,
    Font font);

  protected internal static System.Drawing.Color InterpolateColors(
    System.Drawing.Color color1,
    System.Drawing.Color color2,
    float percentage)
  {
    int r1 = (int) color1.R;
    int g1 = (int) color1.G;
    int b1 = (int) color1.B;
    int a1 = (int) color1.A;
    int r2 = (int) color2.R;
    int g2 = (int) color2.G;
    int b2 = (int) color2.B;
    int a2 = (int) color2.A;
    byte red = Convert.ToByte((float) r1 + (float) (r2 - r1) * percentage);
    byte green = Convert.ToByte((float) g1 + (float) (g2 - g1) * percentage);
    byte blue = Convert.ToByte((float) b1 + (float) (b2 - b1) * percentage);
    return System.Drawing.Color.FromArgb((int) Convert.ToByte((float) a1 + (float) (a2 - a1) * percentage), (int) red, (int) green, (int) blue);
  }

  internal static int ImageWidth(Image image)
  {
    int num = image.Height - 1;
    if (!(image is Bitmap bitmap) || bitmap.Width <= bitmap.Height)
      return image.Width;
    int x;
    for (x = bitmap.Width - 1; x > num; --x)
    {
      for (int y = 0; y < num; ++y)
      {
        if (bitmap.GetPixel(x, y).ToArgb() != System.Drawing.Color.Empty.ToArgb())
          return x + 1;
      }
    }
    return x;
  }

  protected internal abstract Size MeasureDocumentStripTab(
    Graphics graphics,
    Image image,
    string text,
    Font font,
    DrawItemState state);

  protected internal abstract Size ControlClientPadding { get; }

  public bool CustomColors
  {
    get => this._customColors;
    set
    {
      this._customColors = value;
      if (this._customColors)
        return;
      this.CalculateBaseColors();
    }
  }

  protected internal abstract Size DocumentClientPadding { get; }

  protected internal abstract int DocumentTabExtra { get; }

  protected internal abstract int DocumentTabSize { get; }

  protected internal abstract int DocumentTabStripSize { get; }

  protected internal abstract BoxModel TabMetrics { get; }

  protected internal abstract BoxModel TabStripMetrics { get; }

  protected internal abstract TabTextDisplayMode TabTextDisplay { get; }

  protected internal abstract BoxModel TitleBarMetrics { get; }
}
