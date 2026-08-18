
// Type: Intermech.NavBars.NavBarRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Bars;
using Intermech.ButtonsPanel;
using Intermech.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    public class NavBarRenderer : RendererBase, INavBarRenderer, IMenuRenderer
    {
      private Color _paneBackgroundColor1;
      private StringFormat _menuStringFormat;
      private ImageAttributes _menuImageDisabledAttributes;
      private Color _ac;
      private Color _paneBackgroundColor2;
      private Color _headerBackgroundColor1;
      private Color _headerBackgroundColor2;
      private Color _dividerBackgroundColor1;
      private Color _dividerBackgroundColor2;
      private Color _dividerBorderColor;
      private Color _gripperColor1;
      private Color _gripperColor2;
      private Color _headerTextColor;
      private Color _paneTextColor;
      private Color _l;
      private Color _borderColor;
      private Color _n;
      private bool _showSpecialHighlightColors;
      private Color _p;
      private Color _q;
      private Color _r;
      private Color _highlightColor;
      private Color _t;
      private Color _u;
      private Color _v;
      private ImageAttributes _disabledImageAttributes;
      private StringFormat _x;
      private Office2003ColorScheme _colorScheme;
      private Color _z;

      public event EventHandler RedrawRequired;

      public NavBarRenderer()
      {
        this._showSpecialHighlightColors = false;
        this._disabledImageAttributes = (ImageAttributes) null;
        this._colorScheme = Office2003ColorScheme.Automatic;
        this._z = NavBarRenderer.InterpolateColors(SystemColors.Window, SystemColors.Control, 0.15f);
        this._menuStringFormat = (StringFormat) null;
        this.UpdateColors();
        this._x = new StringFormat();
        this._x.LineAlignment = StringAlignment.Center;
        this._x.Trimming = StringTrimming.EllipsisCharacter;
        this._x.FormatFlags |= StringFormatFlags.NoWrap;
        float[][] newColorMatrix1 = new float[6][]
        {
          new float[6]{ 0.3f, 0.3f, 0.3f, 0.0f, 0.0f, 0.0f },
          new float[6]{ 0.59f, 0.59f, 0.59f, 0.0f, 0.0f, 0.0f },
          new float[6]{ 0.11f, 0.11f, 0.11f, 0.0f, 0.0f, 0.0f },
          null,
          null,
          null
        };
        float[] numArray1 = new float[6];
        numArray1[3] = 1f;
        newColorMatrix1[3] = numArray1;
        float[] numArray2 = new float[6];
        numArray2[4] = 1f;
        newColorMatrix1[4] = numArray2;
        float[] numArray3 = new float[6];
        numArray3[5] = 1f;
        newColorMatrix1[5] = numArray3;
        ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
        newColorMatrix2.Matrix33 = 0.3f;
        this._disabledImageAttributes = new ImageAttributes();
        this._disabledImageAttributes.SetColorMatrix(newColorMatrix2);
        this.a();
      }

      private void a()
      {
        this._menuStringFormat = new StringFormat();
        this._menuStringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
        this._menuStringFormat.Alignment = StringAlignment.Near;
        this._menuStringFormat.LineAlignment = StringAlignment.Center;
        this._menuStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        float[][] newColorMatrix1 = new float[6][]
        {
          new float[6]{ 0.3f, 0.3f, 0.3f, 0.0f, 0.0f, 0.0f },
          new float[6]{ 0.59f, 0.59f, 0.59f, 0.0f, 0.0f, 0.0f },
          new float[6]{ 0.11f, 0.11f, 0.11f, 0.0f, 0.0f, 0.0f },
          null,
          null,
          null
        };
        float[] numArray1 = new float[6];
        numArray1[3] = 1f;
        newColorMatrix1[3] = numArray1;
        float[] numArray2 = new float[6];
        numArray2[4] = 1f;
        newColorMatrix1[4] = numArray2;
        float[] numArray3 = new float[6];
        numArray3[5] = 1f;
        newColorMatrix1[5] = numArray3;
        ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
        newColorMatrix2.Matrix33 = 0.3f;
        this._menuImageDisabledAttributes = new ImageAttributes();
        this._menuImageDisabledAttributes.SetColorMatrix(newColorMatrix2);
        this._ac = Color.FromArgb((int) byte.MaxValue, 238, 194);
      }

      private void DrawMenuItemHighlight(Graphics g, MenuButtonItem menuItem, Rectangle A_2)
      {
        if (menuItem.Enabled)
        {
          using (SolidBrush solidBrush = new SolidBrush(this._ac))
            g.FillRectangle((Brush) solidBrush, A_2);
        }
        using (Pen pen = new Pen(this._borderColor))
          g.DrawRectangle(pen, A_2);
      }

      private void DrawMenuBackgroundInternal(Graphics g, Rectangle bounds, LinearGradientMode mode)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._paneBackgroundColor1, this._paneBackgroundColor2, mode))
        {
          linearGradientBrush.InterpolationColors = new ColorBlend(3)
          {
            Colors = new Color[3]
            {
              this._paneBackgroundColor1,
              NavBarRenderer.InterpolateColors(this._paneBackgroundColor1, this._paneBackgroundColor2, 0.25f),
              this._paneBackgroundColor2
            },
            Positions = new float[3]{ 0.0f, 0.5f, 1f }
          };
          g.FillRectangle((Brush) linearGradientBrush, bounds);
        }
      }

      private void a(Graphics g, Rectangle A_1, DrawItemState A_2)
      {
        Brush brush = (A_2 & DrawItemState.Selected) == DrawItemState.Selected || (A_2 & (DrawItemState.Checked | DrawItemState.HotLight)) == (DrawItemState.Checked | DrawItemState.HotLight) ? (!this._showSpecialHighlightColors ? (Brush) new SolidBrush(this._v) : (Brush) new LinearGradientBrush(new Point(A_1.X, A_1.Y - 1), new Point(A_1.X, A_1.Bottom), this._p, this._q)) : ((A_2 & DrawItemState.HotLight) != DrawItemState.HotLight ? ((A_2 & DrawItemState.Checked) != DrawItemState.Checked ? (Brush) new LinearGradientBrush(new Point(A_1.X, A_1.Y - 1), new Point(A_1.X, A_1.Bottom), this._paneBackgroundColor1, this._paneBackgroundColor2) : (!this._showSpecialHighlightColors ? (Brush) new SolidBrush(this._u) : (Brush) new LinearGradientBrush(new Point(A_1.X, A_1.Y - 1), new Point(A_1.X, A_1.Bottom), this._q, this._p))) : (!this._showSpecialHighlightColors ? (Brush) new SolidBrush(this._t) : (Brush) new LinearGradientBrush(new Point(A_1.X, A_1.Y - 1), new Point(A_1.X, A_1.Bottom), this._r, this._q)));
        g.FillRectangle(brush, A_1);
        brush.Dispose();
      }

      private void DrawMenuItemCheck(
        Graphics g,
        MenuButtonItem menuItem,
        bool drawCheckMark,
        Rectangle bounds)
      {
        Pen pen = !menuItem.Enabled ? SystemPens.ControlDark : SystemPens.ControlText;
        if (menuItem.Enabled)
          this.DrawButtonHighlight(g, bounds, DrawItemState.Checked, false);
        else
          g.DrawRectangle(pen, bounds);
        if (!drawCheckMark)
          return;
        int num = bounds.X + bounds.Width / 2;
        int y1 = bounds.Y + bounds.Height / 2;
        g.DrawLine(pen, num - 3, y1, num - 1, y1 + 2);
        g.DrawLine(pen, num - 3, y1 + 1, num - 1, y1 + 3);
        g.DrawLine(pen, num - 1, y1 + 2, num + 3, y1 - 2);
        g.DrawLine(pen, num - 1, y1 + 3, num + 3, y1 - 1);
      }

      private void DrawButtonHighlight(Graphics g, Rectangle A_1, DrawItemState A_2, bool A_3)
      {
        Pen pen = new Pen(this._borderColor);
        if (A_2 != DrawItemState.Default)
        {
          Brush brush = (A_2 & DrawItemState.Selected) != DrawItemState.Selected ? ((A_2 & DrawItemState.HotLight) != DrawItemState.HotLight ? (Brush) new LinearGradientBrush(A_1, this._q, this._p, LinearGradientMode.Vertical) : (Brush) new LinearGradientBrush(A_1, this._r, this._q, LinearGradientMode.Vertical)) : (Brush) new LinearGradientBrush(A_1, this._p, this._q, LinearGradientMode.Vertical);
          g.FillRectangle(brush, A_1);
          g.DrawRectangle(pen, A_1);
          brush.Dispose();
        }
        if (A_3 && A_2 != DrawItemState.Default)
        {
          A_1.Offset(A_1.Width - 11, 0);
          A_1.Width -= A_1.Width - 11;
          Brush brush = (Brush) new LinearGradientBrush(A_1, this._r, this._q, LinearGradientMode.Vertical);
          g.FillRectangle(brush, A_1);
          g.DrawRectangle(pen, A_1);
          brush.Dispose();
        }
        pen.Dispose();
      }

      private void DrawImageInternal(
        Graphics g,
        ButtonItemBase buttonItem,
        DrawItemState A_2,
        Rectangle bounds,
        Image image)
      {
        if (!buttonItem.Enabled)
          g.DrawImage(image, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this._menuImageDisabledAttributes);
        else
          g.DrawImage(image, bounds);
      }

      protected void OnRedrawRequired()
      {
        if (this.RedrawRequired == null)
          return;
        this.RedrawRequired((object) this, EventArgs.Empty);
      }

      private void CalculateDerivedColors()
      {
        this._p = Color.FromArgb(232, (int) sbyte.MaxValue, 8);
        this._q = Color.FromArgb(251, 230, 148);
        this._r = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 220);
        this._v = NavBarRenderer.InterpolateColors(this._highlightColor, SystemColors.ControlLightLight, 0.5f);
        this._t = NavBarRenderer.InterpolateColors(this._highlightColor, SystemColors.Control, 0.5f);
        this._t = NavBarRenderer.InterpolateColors(this._t, SystemColors.ControlLightLight, 0.5f);
        this._u = NavBarRenderer.InterpolateColors(this._highlightColor, SystemColors.Control, 0.8f);
        this._u = NavBarRenderer.InterpolateColors(this._u, SystemColors.ControlLightLight, 0.48f);
        this._n = this._borderColor;
        this.OnRedrawRequired();
      }

      private void UpdateColors()
      {
        switch (this._colorScheme)
        {
          case Office2003ColorScheme.Automatic:
            if (NavBarRenderer.RunningOnXP())
            {
              if (XPThemeManager.a())
              {
                string str1;
                if ((str1 = XPThemeManager.c()) != null)
                {
                  string str2 = string.IsInterned(str1);
                  if (str2 != "NormalColor")
                  {
                    switch (str2)
                    {
                      case "HomeStead":
                        this.LunaOliveColors();
                        break;
                      case "Metallic":
                        this.LunaSilverColors();
                        break;
                    }
                  }
                  else
                  {
                    this.LunaBlueColors();
                    break;
                  }
                }
                else
                  break;
              }
              else
              {
                this.StandardColors();
                break;
              }
            }
            else
            {
              this.StandardColors();
              break;
            }
            break;
          case Office2003ColorScheme.Standard:
            this.StandardColors();
            break;
          case Office2003ColorScheme.LunaBlue:
            this.LunaBlueColors();
            break;
          case Office2003ColorScheme.LunaOlive:
            this.LunaOliveColors();
            break;
          case Office2003ColorScheme.LunaSilver:
            this.LunaSilverColors();
            break;
        }
        this.CalculateDerivedColors();
      }

      internal static bool RunningOnXP()
      {
        bool flag = false;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
          flag = Environment.OSVersion.Version >= new Version(5, 1, 0, 0);
        return flag;
      }

      public void DrawBackground(Graphics graphics, Rectangle bounds, Color backColor)
      {
        graphics.Clear(backColor);
        using (Pen pen = new Pen(this._borderColor))
        {
          --bounds.Width;
          --bounds.Height;
          graphics.DrawRectangle(pen, bounds);
        }
      }

      public void DrawChevron(Graphics graphics, Rectangle bounds)
      {
        int x1 = bounds.X + bounds.Width / 2 - 2;
        int num = bounds.Y + bounds.Height / 2;
        using (Pen pen = new Pen(this._paneTextColor))
        {
          graphics.DrawLine(pen, x1 - 1, num - 5, x1 + 1, num - 3);
          graphics.DrawLine(pen, x1 - 1, num - 1, x1 + 1, num - 3);
          graphics.DrawLine(pen, x1, num - 5, x1 + 2, num - 3);
          graphics.DrawLine(pen, x1, num - 1, x1 + 2, num - 3);
          graphics.DrawLine(pen, x1 + 2, num - 5, x1 + 4, num - 3);
          graphics.DrawLine(pen, x1 + 2, num - 1, x1 + 4, num - 3);
          graphics.DrawLine(pen, x1 + 3, num - 5, x1 + 5, num - 3);
          graphics.DrawLine(pen, x1 + 3, num - 1, x1 + 5, num - 3);
          graphics.DrawLine(pen, x1, num + 4, x1 + 4, num + 4);
          graphics.DrawLine(pen, x1 + 1, num + 5, x1 + 3, num + 5);
          graphics.DrawLine(pen, x1 + 2, num + 6, x1 + 2, num + 4);
        }
      }

      public void DrawContentPane(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        NavigationPane pane,
        Font font)
      {
        int num = 5;
        Image image = pane.LargeImage ?? pane.SmallImage;
        if (image != null)
        {
          Rectangle rectangle = bounds;
          rectangle.Y = rectangle.Y + bounds.Height / 2 - image.Height / 2;
          rectangle.X = 8;
          rectangle.Size = image.Height >= 16 /*0x10*/ || image.Width >= 16 /*0x10*/ ? image.Size : new Size(16 /*0x10*/, 16 /*0x10*/);
          num += image.Width + 8;
          if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
            graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, this._disabledImageAttributes);
          else
            graphics.DrawImage(image, rectangle);
        }
        bounds.X += num;
        bounds.Width -= num;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          graphics.DrawString(pane.Text, font, SystemBrushes.ControlDark, ConvertHelper.ToRectangleF(bounds), this._x);
        }
        else
        {
          using (SolidBrush solidBrush = new SolidBrush((state & DrawItemState.Selected) == DrawItemState.Selected ? this._l : this._paneTextColor))
            graphics.DrawString(pane.Text, font, (Brush) solidBrush, ConvertHelper.ToRectangleF(bounds), this._x);
        }
      }

      public void DrawContentPaneBackground(Graphics graphics, Rectangle bounds, DrawItemState state)
      {
        this.a(graphics, bounds, state);
        using (Pen pen = new Pen(this._borderColor))
          graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right, bounds.Y);
      }

      public void DrawDivider(
        Graphics graphics,
        Rectangle bounds,
        string text,
        Font font,
        Color foreColor)
      {
        if (bounds.Width <= 0 || bounds.Height <= 0)
          return;
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X, bounds.Y - 1), new Point(bounds.X, bounds.Bottom), this._dividerBackgroundColor1, this._dividerBackgroundColor2);
        try
        {
          graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        finally
        {
          linearGradientBrush?.Dispose();
        }
        using (Pen pen = new Pen(this._borderColor))
          graphics.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
        using (Pen pen = new Pen(this._dividerBorderColor))
          graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
        SolidBrush solidBrush = new SolidBrush(foreColor);
        try
        {
          graphics.DrawString(text, font, (Brush) solidBrush, ConvertHelper.ToRectangleF(bounds), this._x);
        }
        finally
        {
          solidBrush?.Dispose();
        }
      }

      public void DrawFooterPane(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        NavigationPane pane,
        Font font)
      {
        Rectangle rectangle = new Rectangle(bounds.X + bounds.Width / 2 - 8, bounds.Y + bounds.Height / 2 - 8, 16 /*0x10*/, 16 /*0x10*/);
        Image image = pane.SmallImage != null ? pane.SmallImage : pane.LargeImage;
        if (image == null)
          return;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
          graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, this._disabledImageAttributes);
        else
          graphics.DrawImage(image, rectangle);
      }

      public void DrawFooterPaneBackground(Graphics graphics, Rectangle bounds, DrawItemState state)
      {
        if (state == DrawItemState.None)
          return;
        this.a(graphics, bounds, state);
      }

      public void DrawGripper(Graphics graphics, Rectangle bounds)
      {
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.X, bounds.Y - 1), new Point(bounds.X, bounds.Bottom), this._gripperColor1, this._gripperColor2);
        try
        {
          graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        finally
        {
          linearGradientBrush?.Dispose();
        }
        int num = bounds.Width / 2 - 18;
        int y = bounds.Y + 1;
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(40, 50, 71));
        try
        {
          for (int index = 0; index < 9; ++index)
          {
            bounds = new Rectangle(num + index * 4 + 1, y + 1, 2, 2);
            graphics.FillRectangle(Brushes.White, bounds);
            bounds = new Rectangle(num + index * 4, y, 2, 2);
            graphics.FillRectangle((Brush) solidBrush, bounds);
          }
        }
        finally
        {
          solidBrush?.Dispose();
        }
      }

      public void DrawHeader(
        Graphics graphics,
        Rectangle bounds,
        string text,
        Font font,
        Image image)
      {
        if (bounds.Width <= 0 || bounds.Height <= 0)
          return;
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._headerBackgroundColor1, this._headerBackgroundColor2, LinearGradientMode.Vertical);
        try
        {
          graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        finally
        {
          linearGradientBrush?.Dispose();
        }
        if (image != null)
        {
          Rectangle rect = bounds with
          {
            Y = bounds.Top + bounds.Height / 2 - image.Height / 2
          };
          rect.X = rect.Right - (image.Width + (rect.Y - bounds.Y));
          rect.Size = image.Size;
          graphics.DrawImage(image, rect);
          bounds.Width -= bounds.Right - rect.Left;
        }
        --bounds.Height;
        SolidBrush solidBrush = new SolidBrush(this._headerTextColor);
        try
        {
          graphics.DrawString(text, font, (Brush) solidBrush, ConvertHelper.ToRectangleF(bounds), this._x);
        }
        finally
        {
          solidBrush?.Dispose();
        }
      }

      private void LunaSilverColors()
      {
        this._borderColor = Color.FromArgb(124, 124, 148);
        this._headerBackgroundColor1 = Color.FromArgb(168, 167, 191);
        this._headerBackgroundColor2 = this._borderColor;
        this._headerTextColor = Color.White;
        this._paneTextColor = SystemColors.ControlText;
        this._l = SystemColors.ControlText;
        this._paneBackgroundColor1 = Color.FromArgb(225, 226, 236);
        this._paneBackgroundColor2 = Color.FromArgb(149, 147, 177);
        this._gripperColor1 = Color.FromArgb(168, 167, 191);
        this._gripperColor2 = this._borderColor;
        this._dividerBackgroundColor1 = Color.FromArgb(243, 243, 247);
        this._dividerBackgroundColor2 = Color.FromArgb(216, 216, 230);
        this._dividerBorderColor = Color.FromArgb(215, 215, 229);
        this._showSpecialHighlightColors = true;
      }

      private void LunaOliveColors()
      {
        this._borderColor = Color.FromArgb(96 /*0x60*/, 128 /*0x80*/, 88);
        this._headerBackgroundColor1 = Color.FromArgb(175, 192 /*0xC0*/, 130);
        this._headerBackgroundColor2 = this._borderColor;
        this._headerTextColor = Color.White;
        this._paneTextColor = SystemColors.ControlText;
        this._l = SystemColors.ControlText;
        this._paneBackgroundColor1 = Color.FromArgb(234, 240 /*0xF0*/, 207);
        this._paneBackgroundColor2 = Color.FromArgb(177, 192 /*0xC0*/, 140);
        this._gripperColor1 = Color.FromArgb(120, 142, 111);
        this._gripperColor2 = Color.FromArgb(73, 91, 67);
        this._dividerBackgroundColor1 = Color.FromArgb(242, 241, 228);
        this._dividerBackgroundColor2 = Color.FromArgb(218, 218, 170);
        this._dividerBorderColor = Color.FromArgb(217, 217, 167);
        this._showSpecialHighlightColors = true;
      }

      private void LunaBlueColors()
      {
        this._borderColor = Color.FromArgb(0, 45, 150);
        this._headerBackgroundColor1 = Color.FromArgb(89, 135, 214);
        this._headerBackgroundColor2 = this._borderColor;
        this._headerTextColor = Color.White;
        this._paneTextColor = SystemColors.ControlText;
        this._l = SystemColors.ControlText;
        this._paneBackgroundColor1 = Color.FromArgb(203, 225, 252);
        this._paneBackgroundColor2 = Color.FromArgb(125, 165, 224 /*0xE0*/);
        this._gripperColor1 = Color.FromArgb(89, 135, 214);
        this._gripperColor2 = this._borderColor;
        this._dividerBackgroundColor1 = Color.FromArgb(196, 218, 250);
        this._dividerBackgroundColor2 = Color.FromArgb(160 /*0xA0*/, 191, 245);
        this._dividerBorderColor = Color.FromArgb(158, 190, 245);
        this._showSpecialHighlightColors = true;
      }

      private void StandardColors()
      {
        this._borderColor = SystemColors.ControlDark;
        this._headerBackgroundColor1 = SystemColors.ControlDark;
        this._headerBackgroundColor2 = this._borderColor;
        this._headerTextColor = SystemColors.ControlLightLight;
        this._paneTextColor = SystemColors.ControlText;
        this._l = SystemColors.ControlLightLight;
        this._paneBackgroundColor1 = SystemColors.ControlLightLight;
        this._paneBackgroundColor2 = SystemColors.Control;
        this._gripperColor1 = SystemColors.Control;
        this._gripperColor2 = this._borderColor;
        this._dividerBackgroundColor1 = NavBarRenderer.InterpolateColors(SystemColors.ControlLightLight, SystemColors.Control, 0.19f);
        this._dividerBackgroundColor2 = SystemColors.Control;
        this._dividerBorderColor = NavBarRenderer.InterpolateColors(SystemColors.Control, SystemColors.ControlDark, 0.04f);
        this._highlightColor = SystemColors.Highlight;
        this._showSpecialHighlightColors = false;
      }

      protected static Color InterpolateColors(Color color1, Color color2, float percentage)
      {
        int r1 = (int) color1.R;
        int g1 = (int) color1.G;
        int b1 = (int) color1.B;
        int r2 = (int) color2.R;
        int g2 = (int) color2.G;
        int b2 = (int) color2.B;
        int red = (int) Convert.ToByte((float) r1 + (float) (r2 - r1) * percentage);
        byte num1 = Convert.ToByte((float) g1 + (float) (g2 - g1) * percentage);
        byte num2 = Convert.ToByte((float) b1 + (float) (b2 - b1) * percentage);
        int green = (int) num1;
        int blue = (int) num2;
        return Color.FromArgb(red, green, blue);
      }

      protected override void OnSystemColorsChanged() => this.UpdateColors();

      void IMenuRenderer.DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode)
      {
      }

      void IMenuRenderer.DrawMenuBackground(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        int breakOffset,
        int breakSize,
        MenuOffset menuDirection,
        bool rightToLeft)
      {
        graphics.Clear(this._z);
        using (Pen pen = new Pen(this._n))
          graphics.DrawRectangle(pen, bounds);
        bounds.Inflate(-1, -1);
        ++bounds.Y;
        --bounds.Height;
        if (rightToLeft)
          bounds.X = bounds.Right - (marginWidth - 8) + 1;
        bounds.Width = marginWidth - 8;
        this.DrawMenuBackgroundInternal(graphics, bounds, LinearGradientMode.Horizontal);
      }

      void IMenuRenderer.DrawMenuItem(
        Graphics graphics,
        MenuButtonItem item,
        IPopupMenuHost host,
        int marginWidth,
        DrawItemState state,
        bool drawSpecial)
      {
        Rectangle rectangle1 = item.ButtonBounds;
        ++rectangle1.X;
        rectangle1.Width -= 3;
        rectangle1.Height -= 2;
        if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
          this.DrawMenuItemHighlight(graphics, item, rectangle1);
        Rectangle rectangle2 = rectangle1;
        if (item.Checked)
        {
          bool flag = !item.HasImage;
          if (host.RightToLeft)
            rectangle1.X = rectangle1.Right - (rectangle1.Height - 2) - 2;
          rectangle1 = !drawSpecial ? new Rectangle(rectangle1.X + 1, rectangle1.Y + 1, rectangle1.Height - 2, rectangle1.Height - 2) : new Rectangle(rectangle1.X + 1, rectangle1.Y + rectangle1.Height / 2 - 9, 19, 19);
          this.DrawMenuItemCheck(graphics, item, flag || drawSpecial, rectangle1);
        }
        Rectangle rectangle3 = rectangle2;
        rectangle3.Y += rectangle3.Height / 2;
        if (item.Icon != null)
        {
          rectangle3.X = !host.RightToLeft ? marginWidth - item.IconSize.Width - 11 : rectangle3.Right - marginWidth + 14;
          rectangle3.Y -= item.IconSize.Height / 2 - 1;
          rectangle3.Size = item.IconSize;
          this.DrawImageInternal(graphics, (ButtonItemBase) item, state, rectangle3, (Image) item.Icon.ToBitmap());
        }
        else if (item.Image != null)
        {
          rectangle3.X = !host.RightToLeft ? marginWidth - item.IconSize.Width - 11 : rectangle3.Right - marginWidth + 14;
          rectangle3.Y -= item.IconSize.Height / 2 - 1;
          rectangle3.Size = item.IconSize;
          this.DrawImageInternal(graphics, (ButtonItemBase) item, state, rectangle3, item.Image);
        }
        else if (host.MenuImageList != null)
        {
          int imageIndex = item.ImageIndex;
        }
        rectangle3 = item.ButtonBounds;
        rectangle3.Width -= marginWidth;
        rectangle3.Width -= 16 /*0x10*/;
        if (host.RightToLeft)
        {
          rectangle3.X += 18;
          this._menuStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        }
        else
        {
          rectangle3.X += marginWidth - 2;
          this._menuStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
        }
        if (item.Enabled)
          graphics.DrawString(item.Text, host.Font, SystemBrushes.ControlText, (RectangleF) rectangle3, this._menuStringFormat);
        else
          graphics.DrawString(item.Text, host.Font, SystemBrushes.ControlDark, (RectangleF) rectangle3, this._menuStringFormat);
        if (!item.HasVisibleSubitems())
          return;
        Point[] points = new Point[3];
        rectangle3 = item.ButtonBounds;
        rectangle3.Y += rectangle3.Height / 2;
        rectangle3.Y -= 5;
        if (host.RightToLeft)
        {
          rectangle3.X = 12;
          points[0] = new Point(rectangle3.X, rectangle3.Y);
          points[1] = new Point(rectangle3.X, rectangle3.Y + 8);
          points[2] = new Point(rectangle3.X - 4, rectangle3.Y + 4);
        }
        else
        {
          rectangle3.X = rectangle3.Right - 12;
          points[0] = new Point(rectangle3.X, rectangle3.Y);
          points[1] = new Point(rectangle3.X + 4, rectangle3.Y + 4);
          points[2] = new Point(rectangle3.X, rectangle3.Y + 8);
        }
        graphics.FillPolygon(SystemBrushes.ControlText, points);
      }

      void IMenuRenderer.DrawMenuSeparator(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        bool rightToLeft)
      {
        using (Pen pen = new Pen(this._paneBackgroundColor2))
        {
          if (rightToLeft)
            graphics.DrawLine(pen, bounds.Left, bounds.Y + 1, bounds.Right - marginWidth - 1, bounds.Y + 1);
          else
            graphics.DrawLine(pen, marginWidth + 1, bounds.Y + 1, bounds.Right - 1, bounds.Y + 1);
        }
      }

      StringFormat IMenuRenderer.MenuShortcutStringFormat => (StringFormat) null;

      StringFormat IMenuRenderer.MenuTextStringFormat => (StringFormat) null;

      Color IMenuRenderer.ShadowColor => this._borderColor;

      public Color BorderColor
      {
        get => this._borderColor;
        set
        {
          this._borderColor = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Office2003ColorScheme ColorScheme
      {
        get => this._colorScheme;
        set
        {
          if (this._colorScheme == value)
            return;
          this._colorScheme = value;
          this.UpdateColors();
        }
      }

      public Color DividerBackgroundColor1
      {
        get => this._dividerBackgroundColor1;
        set
        {
          this._dividerBackgroundColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color DividerBackgroundColor2
      {
        get => this._dividerBackgroundColor2;
        set
        {
          this._dividerBackgroundColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color DividerBorderColor
      {
        get => this._dividerBorderColor;
        set
        {
          this._dividerBorderColor = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color GripperColor1
      {
        get => this._gripperColor1;
        set
        {
          this._gripperColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color GripperColor2
      {
        get => this._gripperColor2;
        set
        {
          this._gripperColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color HeaderBackgroundColor1
      {
        get => this._headerBackgroundColor1;
        set
        {
          this._headerBackgroundColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color HeaderBackgroundColor2
      {
        get => this._headerBackgroundColor2;
        set
        {
          this._headerBackgroundColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color HeaderTextColor
      {
        get => this._headerTextColor;
        set
        {
          this._headerTextColor = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color HighlightColor
      {
        get => this._highlightColor;
        set
        {
          this._highlightColor = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color PaneBackgroundColor1
      {
        get => this._paneBackgroundColor1;
        set
        {
          this._paneBackgroundColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color PaneBackgroundColor2
      {
        get => this._paneBackgroundColor2;
        set
        {
          this._paneBackgroundColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color PaneTextColor
      {
        get => this._paneTextColor;
        set
        {
          this._paneTextColor = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public bool ShowSpecialHighlightColors
      {
        get => this._showSpecialHighlightColors;
        set
        {
          if (this._showSpecialHighlightColors == value)
            return;
          this._showSpecialHighlightColors = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }
    }
}
