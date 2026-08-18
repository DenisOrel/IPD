
// Type: Intermech.UI.XPGroupItemRenderer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Intermech.UI;

/// <summary>
/// 
/// </summary>
public class XPGroupItemRenderer : IDisposable
{
  private ImageAttributes _disabledBlendAttrs;
  private ImageAttributes _shadowBlendAttrs;
  private ImageAttributes _blendAttrs;
  private SolidBrush _checkedBckgrnd;
  private SolidBrush _pushedBckgrnd;
  private SolidBrush _menuBckgrnd;
  private SolidBrush _menuMargin;
  private SolidBrush _hotBckgrnd;
  private SolidBrush _bckgrnd;
  private Pen _hotBorder;
  private Pen _separator;
  private bool _useCustomColours;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush Background => this._bckgrnd;

  /// <summary>
  /// 
  /// </summary>
  public ImageAttributes BlendAttributes => this._blendAttrs;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush CheckedBackground => this._checkedBckgrnd;

  /// <summary>
  /// 
  /// </summary>
  public ImageAttributes DisabledBlendAttributes => this._disabledBlendAttrs;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush HotBackground => this._hotBckgrnd;

  /// <summary>
  /// 
  /// </summary>
  public Pen HotBorder => this._hotBorder;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush MenuBackground => this._menuBckgrnd;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush MenuMargin => this._menuMargin;

  /// <summary>
  /// 
  /// </summary>
  public SolidBrush PushedBackground => this._pushedBckgrnd;

  /// <summary>
  /// 
  /// </summary>
  public Pen Separator => this._separator;

  /// <summary>
  /// 
  /// </summary>
  public ImageAttributes ShadowBlendAttributes => this._shadowBlendAttrs;

  /// <summary>Конструктор.</summary>
  internal XPGroupItemRenderer()
  {
    this._useCustomColours = false;
    this.CalculateColours();
    SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
    ColorMatrix newColorMatrix1 = new ColorMatrix();
    newColorMatrix1.Matrix33 = 0.7f;
    this._blendAttrs = new ImageAttributes();
    this._blendAttrs.SetColorMatrix(newColorMatrix1);
    ColorMatrix newColorMatrix2 = new ColorMatrix();
    newColorMatrix2.Matrix33 = 0.5f;
    this._disabledBlendAttrs = new ImageAttributes();
    this._disabledBlendAttrs.SetColorMatrix(newColorMatrix2);
    ColorMatrix newColorMatrix3 = new ColorMatrix();
    newColorMatrix3.Matrix33 = 0.25f;
    this._shadowBlendAttrs = new ImageAttributes();
    this._shadowBlendAttrs.SetRemapTable(new ColorMap[1]
    {
      new ColorMap()
      {
        OldColor = System.Drawing.Color.White,
        NewColor = System.Drawing.Color.Black
      }
    });
    this._shadowBlendAttrs.SetGamma(10f);
    this._shadowBlendAttrs.SetColorMatrix(newColorMatrix3);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CalculateColours()
  {
    if (this._bckgrnd != null)
      this.DisposeBrushes();
    this._bckgrnd = new SolidBrush(this.InterpolateColours(SystemColors.Control, SystemColors.Window, 0.15f));
    this._separator = new Pen(this.InterpolateColours(SystemColors.ControlDark, SystemColors.Control, 0.39f));
    this._menuBckgrnd = new SolidBrush(this.InterpolateColours(SystemColors.Window, SystemColors.Control, 0.22f));
    this._menuMargin = new SolidBrush(this.InterpolateColours(SystemColors.Window, SystemColors.Control, 0.8f));
    this.CalculateHighlightColours(SystemColors.Highlight);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="baseColor"></param>
  private void CalculateHighlightColours(System.Drawing.Color baseColor)
  {
    this._hotBckgrnd = new SolidBrush(this.EnsureDarkness(this._bckgrnd.Color, this.InterpolateColours(baseColor, SystemColors.Window, 0.7f), 0.05f));
    this._pushedBckgrnd = new SolidBrush(this.InterpolateColours(baseColor, SystemColors.Window, 0.5f));
    this._checkedBckgrnd = new SolidBrush(this.InterpolateColours(baseColor, SystemColors.Window, 0.85f));
    this._hotBorder = new Pen(baseColor);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
    this.DisposeBrushes();
    this._blendAttrs.Dispose();
    this._disabledBlendAttrs.Dispose();
    this._shadowBlendAttrs.Dispose();
    SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DisposeBrushes()
  {
    this._bckgrnd.Dispose();
    this._hotBckgrnd.Dispose();
    this._checkedBckgrnd.Dispose();
    this._pushedBckgrnd.Dispose();
    this._separator.Dispose();
    this._menuBckgrnd.Dispose();
    this._menuMargin.Dispose();
    this._hotBorder.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="dropDown"></param>
  /// <param name="highlightMode"></param>
  public void DrawItemHighlight(
    Graphics g,
    Rectangle bounds,
    bool dropDown,
    XPGroupItemRenderer.HighlightMode highlightMode)
  {
    switch (highlightMode)
    {
      case XPGroupItemRenderer.HighlightMode.Pushed:
        if (dropDown)
        {
          g.FillRectangle((Brush) this.HotBackground, new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
          bounds.Width -= 11;
          g.FillRectangle((Brush) this.PushedBackground, new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
          bounds.Width += 11;
          break;
        }
        g.FillRectangle((Brush) this.PushedBackground, new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
        break;
      case XPGroupItemRenderer.HighlightMode.Checked:
        g.FillRectangle((Brush) this.CheckedBackground, new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
        g.DrawRectangle(this.HotBorder, bounds);
        return;
      default:
        g.FillRectangle((Brush) this.HotBackground, new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
        break;
    }
    g.DrawRectangle(this.HotBorder, bounds);
    if (!dropDown)
      return;
    bounds.Offset(bounds.Width - 11, 0);
    bounds.Width -= bounds.Width - 11;
    g.DrawRectangle(this.HotBorder, bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="image"></param>
  /// <param name="bounds"></param>
  /// <param name="backColor"></param>
  public void DrawImageDisabled(Graphics g, Image image, Rectangle bounds, System.Drawing.Color backColor)
  {
    Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
    Graphics graphics = Graphics.FromImage((Image) bitmap);
    ControlPaint.DrawImageDisabled(graphics, image, 0, 0, backColor);
    graphics.Dispose();
    g.DrawImage((Image) bitmap, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
    bitmap.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="Colour1"></param>
  /// <param name="Colour2"></param>
  /// <param name="Percentage"></param>
  /// <returns></returns>
  private System.Drawing.Color EnsureDarkness(System.Drawing.Color Colour1, System.Drawing.Color Colour2, float Percentage)
  {
    float brightness = Colour1.GetBrightness();
    if ((double) Colour2.GetBrightness() > (double) brightness - (double) Percentage)
      Colour2 = this.InterpolateColours(Colour2, System.Drawing.Color.Black, 0.14f);
    return Colour2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="color1"></param>
  /// <param name="color2"></param>
  /// <param name="Percentage"></param>
  /// <returns></returns>
  private System.Drawing.Color InterpolateColours(System.Drawing.Color color1, System.Drawing.Color color2, float Percentage)
  {
    int r1 = (int) color1.R;
    int g1 = (int) color1.G;
    int b1 = (int) color1.B;
    int r2 = (int) color2.R;
    int g2 = (int) color2.G;
    int b2 = (int) color2.B;
    int red = (int) Convert.ToByte((float) r1 + (float) (r2 - r1) * Percentage);
    byte num1 = Convert.ToByte((float) g1 + (float) (g2 - g1) * Percentage);
    byte num2 = Convert.ToByte((float) b1 + (float) (b2 - b1) * Percentage);
    int green = (int) num1;
    int blue = (int) num2;
    return System.Drawing.Color.FromArgb(red, green, blue);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
  {
    if (!(e.Category == UserPreferenceCategory.Color & !this._useCustomColours))
      return;
    this.CalculateColours();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="color"></param>
  public void SetBackgroundColour(System.Drawing.Color color)
  {
    this._bckgrnd = new SolidBrush(color);
    this._useCustomColours = true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void SetDefaultColours() => this.CalculateColours();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="baseColor"></param>
  public void SetHighlightColourBase(System.Drawing.Color baseColor)
  {
    this.CalculateHighlightColours(baseColor);
    this._useCustomColours = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="color"></param>
  public void SetSeparatorColour(System.Drawing.Color color)
  {
    this._separator = new Pen(color);
    this._useCustomColours = true;
  }

  /// <summary>
  /// 
  /// </summary>
  public enum HighlightMode
  {
    Hot,
    Pushed,
    Checked,
  }
}
