
// Type: Intermech.ButtonsPanel.ButtonsPanelRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Intermech.ButtonsPanel
{
    public class ButtonsPanelRenderer : IDisposable
    {
      private SolidBrush _background;
      private ImageAttributes _blendAttributes;
      private SolidBrush _checkedBackground;
      private ImageAttributes _disabledBlendAttributes;
      private SolidBrush _hotBackground;
      private Pen _hotBorder;
      private SolidBrush _menuBackground;
      private SolidBrush _menuMargin;
      private SolidBrush _pushedBackground;
      private Pen _separator;
      private ImageAttributes _shadowBlendAttributes;
      private bool _useCustomColours;

      internal ButtonsPanelRenderer()
      {
        this._useCustomColours = false;
        this.CalculateColours();
        SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
        ColorMatrix newColorMatrix1 = new ColorMatrix();
        newColorMatrix1.Matrix33 = 0.7f;
        this._blendAttributes = new ImageAttributes();
        this._blendAttributes.SetColorMatrix(newColorMatrix1);
        ColorMatrix newColorMatrix2 = new ColorMatrix();
        newColorMatrix2.Matrix33 = 0.5f;
        this._disabledBlendAttributes = new ImageAttributes();
        this._disabledBlendAttributes.SetColorMatrix(newColorMatrix2);
        ColorMatrix newColorMatrix3 = new ColorMatrix();
        newColorMatrix3.Matrix33 = 0.25f;
        this._shadowBlendAttributes = new ImageAttributes();
        this._shadowBlendAttributes.SetRemapTable(new ColorMap[1]
        {
          new ColorMap()
          {
            OldColor = System.Drawing.Color.White,
            NewColor = System.Drawing.Color.Black
          }
        });
        this._shadowBlendAttributes.SetGamma(10f);
        this._shadowBlendAttributes.SetColorMatrix(newColorMatrix3);
      }

      private void CalculateColours()
      {
        if (this._background != null)
          this.DisposeBrushes();
        this._background = new SolidBrush(this.InterpolateColours(SystemColors.Control, SystemColors.Window, 0.15f));
        this._separator = new Pen(this.InterpolateColours(SystemColors.ControlDark, SystemColors.Control, 0.39f));
        this._menuBackground = new SolidBrush(this.InterpolateColours(SystemColors.Window, SystemColors.Control, 0.22f));
        this._menuMargin = new SolidBrush(this.InterpolateColours(SystemColors.Window, SystemColors.Control, 0.8f));
        this.CalculateHighlightColours(SystemColors.Highlight);
      }

      private void CalculateHighlightColours(System.Drawing.Color baseColor)
      {
        if (this._hotBackground != null)
          this.DisposeBrushesHighlight();
        this._hotBackground = new SolidBrush(this.EnsureDarkness(this._background.Color, this.InterpolateColours(baseColor, SystemColors.Window, 0.7f), 0.05f));
        this._pushedBackground = new SolidBrush(this.InterpolateColours(baseColor, SystemColors.Window, 0.5f));
        this._checkedBackground = new SolidBrush(this.InterpolateColours(baseColor, SystemColors.Window, 0.85f));
        this._hotBorder = new Pen(baseColor);
      }

      public void Dispose()
      {
        this.DisposeBrushes();
        this.DisposeBrushesHighlight();
        this._blendAttributes.Dispose();
        this._disabledBlendAttributes.Dispose();
        this._shadowBlendAttributes.Dispose();
        SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
      }

      private void DisposeBrushes()
      {
        this._background.Dispose();
        this._background = (SolidBrush) null;
        this._separator.Dispose();
        this._separator = (Pen) null;
        this._menuBackground.Dispose();
        this._menuBackground = (SolidBrush) null;
        this._menuMargin.Dispose();
        this._menuMargin = (SolidBrush) null;
      }

      private void DisposeBrushesHighlight()
      {
        this._hotBackground.Dispose();
        this._hotBackground = (SolidBrush) null;
        this._checkedBackground.Dispose();
        this._checkedBackground = (SolidBrush) null;
        this._pushedBackground.Dispose();
        this._pushedBackground = (SolidBrush) null;
        this._hotBorder.Dispose();
        this._hotBackground = (SolidBrush) null;
      }

      public void DrawButtonHighlight(
        Graphics g,
        Rectangle bounds,
        bool dropDown,
        ButtonsPanelRenderer.HighlightMode highlightMode)
      {
        switch (highlightMode)
        {
          case ButtonsPanelRenderer.HighlightMode.Pushed:
            if (dropDown)
            {
              g.FillRectangle((Brush) this.HotBackground, ConvertHelper.ToRectangleF(bounds));
              bounds.Width -= 11;
              g.FillRectangle((Brush) this.PushedBackground, ConvertHelper.ToRectangleF(bounds));
              bounds.Width += 11;
              break;
            }
            g.FillRectangle((Brush) this.PushedBackground, ConvertHelper.ToRectangleF(bounds));
            break;
          case ButtonsPanelRenderer.HighlightMode.Checked:
            g.FillRectangle((Brush) this.CheckedBackground, ConvertHelper.ToRectangleF(bounds));
            g.DrawRectangle(this.HotBorder, bounds);
            return;
          default:
            g.FillRectangle((Brush) this.HotBackground, ConvertHelper.ToRectangleF(bounds));
            break;
        }
        g.DrawRectangle(this.HotBorder, bounds);
        if (!dropDown)
          return;
        bounds.Offset(bounds.Width - 11, 0);
        bounds.Width -= bounds.Width - 11;
        g.DrawRectangle(this.HotBorder, bounds);
      }

      public void DrawImageDisabled(Graphics g, Image image, Rectangle bounds, System.Drawing.Color backColor)
      {
        Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
        Graphics graphics = Graphics.FromImage((Image) bitmap);
        ControlPaint.DrawImageDisabled(graphics, image, 0, 0, backColor);
        graphics.Dispose();
        g.DrawImage((Image) bitmap, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
        bitmap.Dispose();
      }

      private System.Drawing.Color EnsureDarkness(System.Drawing.Color Colour1, System.Drawing.Color Colour2, float Percentage)
      {
        float brightness = Colour1.GetBrightness();
        if ((double) Colour2.GetBrightness() > (double) brightness - (double) Percentage)
          Colour2 = this.InterpolateColours(Colour2, System.Drawing.Color.Black, 0.14f);
        return Colour2;
      }

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

      public void SetBackgroundColour(System.Drawing.Color color)
      {
        this._background = new SolidBrush(color);
        this._useCustomColours = true;
      }

      public void SetDefaultColours() => this.CalculateColours();

      public void SetHighlightColourBase(System.Drawing.Color baseColor)
      {
        this.CalculateHighlightColours(baseColor);
        this._useCustomColours = true;
      }

      public void SetSeparatorColour(System.Drawing.Color color)
      {
        this._separator = new Pen(color);
        this._useCustomColours = true;
      }

      private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
      {
        if (!(e.Category == UserPreferenceCategory.Color & !this._useCustomColours))
          return;
        this.CalculateColours();
      }

      public SolidBrush Background => this._background;

      public ImageAttributes BlendAttributes => this._blendAttributes;

      public SolidBrush CheckedBackground => this._checkedBackground;

      public ImageAttributes DisabledBlendAttributes => this._disabledBlendAttributes;

      public SolidBrush HotBackground => this._hotBackground;

      public Pen HotBorder => this._hotBorder;

      public SolidBrush MenuBackground => this._menuBackground;

      public SolidBrush MenuMargin => this._menuMargin;

      public SolidBrush PushedBackground => this._pushedBackground;

      public Pen Separator => this._separator;

      public ImageAttributes ShadowBlendAttributes => this._shadowBlendAttributes;

      public enum HighlightMode
      {
        Hot,
        Pushed,
        Checked,
      }
    }
}
