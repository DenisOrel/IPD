
// Type: Intermech.Bars.Office2002Renderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class Office2002Renderer : OfficeRendererBase
    {
      private const int __a = 13;
      internal StringFormat _leftStringFormat;
      internal StringFormat _centerStringFormat;
      private StringFormat _menuTextStringFormat;
      private StringFormat _menuShortcutStringFormat;
      private Color __f;
      private Color __g;
      private Color __h;
      private Color i;
      private Color _backgroundColor;
      private Color _menuBackgroundColor;
      private Color l;
      private Color m;
      private Color _highlightBorderColor;
      private Color o;
      internal Color p;
      private Color q;
      private Color r;
      private Pen s;
      private ImageAttributes t;
      private ImageAttributes _disabledBlendAttributes;
      private ImageAttributes v;

      public Office2002Renderer()
      {
        this._leftStringFormat = (StringFormat) null;
        this._centerStringFormat = (StringFormat) null;
        this._menuTextStringFormat = (StringFormat) null;
        this._menuShortcutStringFormat = (StringFormat) null;
        this._leftStringFormat = new StringFormat();
        this._leftStringFormat.LineAlignment = StringAlignment.Center;
        this._leftStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        this._centerStringFormat = new StringFormat();
        this._centerStringFormat.Alignment = StringAlignment.Center;
        this._centerStringFormat.LineAlignment = StringAlignment.Center;
        this._centerStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        this._menuTextStringFormat = new StringFormat();
        this._menuTextStringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
        this._menuTextStringFormat.Alignment = StringAlignment.Near;
        this._menuTextStringFormat.LineAlignment = StringAlignment.Center;
        this._menuTextStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        this._menuShortcutStringFormat = new StringFormat();
        this._menuShortcutStringFormat.HotkeyPrefix = HotkeyPrefix.None;
        this._menuShortcutStringFormat.Alignment = StringAlignment.Far;
        this._menuShortcutStringFormat.LineAlignment = StringAlignment.Center;
        ColorMatrix newColorMatrix1 = new ColorMatrix();
        newColorMatrix1.Matrix33 = 0.7f;
        this.t = new ImageAttributes();
        this.t.SetColorMatrix(newColorMatrix1);
        ColorMatrix newColorMatrix2 = new ColorMatrix();
        newColorMatrix2.Matrix00 = 0.3f;
        newColorMatrix2.Matrix01 = 0.3f;
        newColorMatrix2.Matrix02 = 0.3f;
        newColorMatrix2.Matrix10 = 0.59f;
        newColorMatrix2.Matrix11 = 0.59f;
        newColorMatrix2.Matrix12 = 0.59f;
        newColorMatrix2.Matrix20 = 0.11f;
        newColorMatrix2.Matrix21 = 0.11f;
        newColorMatrix2.Matrix22 = 0.11f;
        newColorMatrix2.Matrix33 = 0.3f;
        this._disabledBlendAttributes = new ImageAttributes();
        this._disabledBlendAttributes.SetColorMatrix(newColorMatrix2);
        ColorMatrix newColorMatrix3 = new ColorMatrix();
        newColorMatrix3.Matrix33 = 0.25f;
        this.v = new ImageAttributes();
        this.v.SetRemapTable(new ColorMap[1]
        {
          new ColorMap()
          {
            OldColor = Color.White,
            NewColor = Color.Black
          }
        });
        this.v.SetGamma(10f);
        this.v.SetColorMatrix(newColorMatrix3);
        this.CalculateBaseColors();
      }

      private void a(Color A_0)
      {
        this.p = this.a(this._backgroundColor, Office2002Renderer.InterpolateColors(A_0, SystemColors.Window, 0.7f), 0.05f);
        this.r = Office2002Renderer.InterpolateColors(A_0, SystemColors.Window, 0.5f);
        this.q = Office2002Renderer.InterpolateColors(A_0, SystemColors.Window, 0.85f);
        this.o = A_0;
      }

      private Color a(Color A_0, Color A_1, float A_2)
      {
        float brightness = A_0.GetBrightness();
        if ((double) A_1.GetBrightness() > (double) brightness - (double) A_2)
          A_1 = Office2002Renderer.InterpolateColors(A_1, Color.Black, 0.14f);
        return A_1;
      }

      private void a(Graphics A_0, Rectangle A_1, bool A_2)
      {
        Pen pen = SystemPens.ControlText;
        Brush brush = SystemBrushes.ControlText;
        if (!A_2)
        {
          pen = SystemPens.ControlDark;
          brush = SystemBrushes.ControlDark;
        }
        int num = A_1.Y + A_1.Height / 2 - 1;
        int x1 = A_1.X + A_1.Width - 5;
        A_0.DrawLine(pen, x1, num, x1 + 4, num);
        A_0.DrawLine(pen, x1 + 1, num + 1, x1 + 3, num + 1);
        A_0.FillRectangle(brush, x1 + 2, num + 2, 1, 1);
      }

      private void a(Graphics A_0, int A_1, int A_2)
      {
        A_0.DrawLine(Pens.Black, A_1, A_2, A_1 + 2, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 1, A_2, A_1 + 3, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1, A_2 + 4, A_1 + 2, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 1, A_2 + 4, A_1 + 3, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 4, A_2, A_1 + 6, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 5, A_2, A_1 + 7, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 4, A_2 + 4, A_1 + 6, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 5, A_2 + 4, A_1 + 7, A_2 + 2);
      }

      internal void a(Graphics A_0, int A_1, int A_2, Color A_3)
      {
        using (Pen pen = new Pen(A_3))
        {
          A_0.DrawLine(pen, A_1, A_2, A_1, A_2 + 4);
          A_0.DrawLine(pen, A_1 + 1, A_2 + 1, A_1 + 1, A_2 + 3);
          A_0.DrawLine(pen, A_1 + 2, A_2 + 2, A_1, A_2 + 2);
        }
      }

      internal void a(
        Rectangle A_0,
        Size A_1,
        int A_2,
        out Rectangle A_3,
        out Rectangle A_4,
        out Rectangle A_5,
        out Rectangle A_6)
      {
        A_0.Inflate(-2, -2);
        A_3 = A_0;
        A_3.Height = A_2;
        A_4 = A_0;
        A_4.Y += A_2;
        A_4.Height -= A_2;
        A_5 = A_4;
        A_4.Height = A_1.Height;
        if (A_1.Width < A_4.Width)
          A_4.Width = A_1.Width;
        A_6 = Rectangle.Empty;
        A_5.Y += A_1.Height;
        A_5.Height -= A_1.Height;
      }

      internal void MeasureBreakLine(
        Rectangle bounds,
        int breakOffset,
        int breakSize,
        MenuOffset menuOffset,
        bool rightToLeft,
        out int x1,
        out int x2,
        out int y1,
        out int y2)
      {
        x1 = 0;
        x2 = 0;
        y1 = 0;
        y2 = 0;
        if (menuOffset == MenuOffset.Left || menuOffset == MenuOffset.Right)
        {
          y1 = breakOffset + 1;
          y2 = y1 + breakSize - 2;
        }
        else if (!rightToLeft)
        {
          x1 = breakOffset + 1;
          x2 = x1 + breakSize - 2;
        }
        else
        {
          x1 = bounds.Right - breakOffset - 1;
          x2 = x1 - breakSize + 2;
        }
        switch (menuOffset)
        {
          case MenuOffset.Top:
            y1 = y2 = bounds.Bottom;
            break;
          case MenuOffset.Bottom:
            y1 = y2 = bounds.Top;
            break;
          case MenuOffset.Left:
            x1 = x2 = bounds.Right;
            break;
          case MenuOffset.Right:
            x1 = x2 = bounds.Left;
            break;
        }
      }

      internal void b(Graphics A_0, int A_1, int A_2, Color A_3)
      {
        using (Pen pen = new Pen(A_3))
        {
          A_0.DrawLine(pen, A_1, A_2, A_1, A_2 + 4);
          A_0.DrawLine(pen, A_1 - 1, A_2 + 1, A_1 - 1, A_2 + 3);
          A_0.DrawLine(pen, A_1 - 2, A_2 + 2, A_1, A_2 + 2);
        }
      }

      internal void c(Graphics A_0, int A_1, int A_2, Color A_3)
      {
        using (Pen pen = new Pen(A_3))
        {
          A_0.DrawLine(pen, A_1, A_2, A_1 + 4, A_2);
          A_0.DrawLine(pen, A_1 + 1, A_2 + 1, A_1 + 3, A_2 + 1);
          A_0.DrawLine(pen, A_1 + 2, A_2 + 2, A_1 + 2, A_2);
        }
      }

      protected override void CalculateBaseColors()
      {
        this._backgroundColor = Office2002Renderer.InterpolateColors(SystemColors.Control, SystemColors.Window, 0.15f);
        this._highlightBorderColor = SystemColors.Highlight;
        this._menuBackgroundColor = Office2002Renderer.InterpolateColors(SystemColors.Window, SystemColors.Control, 0.15f);
        this.CalculateDerivedColors();
      }

      protected virtual void CalculateDerivedColors()
      {
        this.__f = Office2002Renderer.InterpolateColors(SystemColors.ControlDark, SystemColors.ControlDarkDark, 0.48f);
        this.__g = SystemColors.ControlDark;
        this.__h = Office2002Renderer.InterpolateColors(SystemColors.ControlDarkDark, SystemColors.ControlText, 0.4f);
        this.i = Office2002Renderer.InterpolateColors(SystemColors.ControlDark, SystemColors.Control, 0.39f);
        this.m = Office2002Renderer.InterpolateColors(SystemColors.Window, SystemColors.Control, 0.8f);
        this.l = Office2002Renderer.InterpolateColors(SystemColors.Control, Color.Black, 0.42f);
        this.a(this._highlightBorderColor);
      }

      public override void Dispose()
      {
        if (this.t != null)
        {
          this.t.Dispose();
          this.t = (ImageAttributes) null;
        }
        if (this.s != null)
        {
          this.s.Dispose();
          this.s = (Pen) null;
        }
        if (this._disabledBlendAttributes != null)
        {
          this._disabledBlendAttributes.Dispose();
          this._disabledBlendAttributes = (ImageAttributes) null;
        }
        if (this.v != null)
        {
          this.v.Dispose();
          this.v = (ImageAttributes) null;
        }
        if (this._leftStringFormat != null)
        {
          this._leftStringFormat.Dispose();
          this._leftStringFormat = (StringFormat) null;
        }
        if (this._centerStringFormat != null)
        {
          this._centerStringFormat.Dispose();
          this._centerStringFormat = (StringFormat) null;
        }
        if (this._menuTextStringFormat != null)
        {
          this._menuTextStringFormat.Dispose();
          this._menuTextStringFormat = (StringFormat) null;
        }
        if (this._menuShortcutStringFormat != null)
        {
          this._menuShortcutStringFormat.Dispose();
          this._menuShortcutStringFormat = (StringFormat) null;
        }
        base.Dispose();
      }

      public override void DrawButtonHighlight(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool dropDown)
      {
        bool flag = (state & DrawItemState.HotLight) == DrawItemState.HotLight || (state & DrawItemState.Selected) == DrawItemState.Selected || (state & DrawItemState.Checked) == DrawItemState.Checked;
        if (flag)
        {
          using (Pen pen = new Pen(this.o))
          {
            if ((state & DrawItemState.Selected) == DrawItemState.Selected)
            {
              using (SolidBrush solidBrush = new SolidBrush(this.r))
                graphics.FillRectangle((Brush) solidBrush, bounds);
            }
            else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            {
              using (SolidBrush solidBrush = new SolidBrush(this.p))
                graphics.FillRectangle((Brush) solidBrush, bounds);
            }
            else if ((state & DrawItemState.Checked) == DrawItemState.Checked)
            {
              using (SolidBrush solidBrush = new SolidBrush(this.q))
                graphics.FillRectangle((Brush) solidBrush, bounds);
            }
            graphics.DrawRectangle(pen, bounds);
          }
        }
        if (!dropDown || !flag)
          return;
        bounds.Offset(bounds.Width - 11, 0);
        bounds.Width -= bounds.Width - 11;
        using (SolidBrush solidBrush = new SolidBrush(this.p))
          graphics.FillRectangle((Brush) solidBrush, bounds);
        using (Pen pen = new Pen(this.o))
          graphics.DrawRectangle(pen, bounds);
      }

      protected override void DrawButtonItem(
        ButtonItemBase item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state,
        ToolBarTextAlign textAlign)
      {
        if (item is DropDownMenuItem && ((TopLevelMenuItemBase) item).DrawDroppedDown)
        {
          this.PaintPushedDropDownButton(graphics, (TopLevelMenuItemBase) item);
          if ((state & DrawItemState.Selected) == DrawItemState.Selected)
            state ^= DrawItemState.Selected;
        }
        else
          this.DrawButtonHighlight(graphics, item.ButtonBounds, state, item is DropDownMenuItem);
        if (item is DropDownMenuItem)
          this.a(graphics, item.ButtonInnerBounds, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
        if (item._imageBounds != Rectangle.Empty)
        {
          if (item.Icon != null)
          {
            try
            {
              using (Icon icon = new Icon(item.Icon, item.IconSize))
                this.DrawIconCore(icon, graphics, state, item._imageBounds);
            }
            catch
            {
            }
          }
          else if (item.Image != null)
            this.DrawImageCore(item.Image, graphics, state, item._imageBounds);
          else
            this.DrawImageCore(item.ImageList, item.ImageIndex, graphics, state, item._imageBounds);
        }
        if (item._textBounds == Rectangle.Empty)
          return;
        StringFormat textFormat = this._leftStringFormat;
        if (textAlign == ToolBarTextAlign.Underneath)
          textFormat = this._centerStringFormat;
        if (item.ForeColor != SystemColors.ControlText)
        {
          using (SolidBrush solidBrush = new SolidBrush(item.ForeColor))
            this.DrawText(item.Text, graphics, item.Font, (Brush) solidBrush, state, item._textBounds, textFormat);
        }
        else
          this.DrawText(item.Text, graphics, item.Font, SystemBrushes.ControlText, state, item._textBounds, textFormat);
      }

      public override void DrawComboBox(
        ComboBox comboBox,
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool rightToLeft)
      {
        Pen pen1 = new Pen(this.HighlightBorderColor);
        Rectangle rectangle1 = bounds;
        --rectangle1.Width;
        --rectangle1.Height;
        this.DrawComboBoxBorder(comboBox, graphics, rectangle1, state);
        rectangle1.Inflate(-1, -1);
        rectangle1.Width -= 13;
        if (rightToLeft)
          rectangle1.X += 13;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          graphics.DrawRectangle(SystemPens.Control, rectangle1);
        }
        else
        {
          using (Pen pen2 = new Pen(SystemColors.Window))
            graphics.DrawRectangle(pen2, rectangle1);
        }
        Rectangle rect = bounds;
        rect.Inflate(-1, -1);
        rect.X = !rightToLeft ? rect.Right - SystemInformation.HorizontalScrollBarArrowWidth - 2 : SystemInformation.HorizontalScrollBarArrowWidth - (SystemInformation.HorizontalScrollBarArrowWidth - 13) + 1;
        rect.Width = SystemInformation.HorizontalScrollBarArrowWidth - 13 + 2;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
          graphics.FillRectangle(SystemBrushes.Control, rect);
        else
          graphics.FillRectangle(SystemBrushes.Window, rect);
        Rectangle rectangle2 = bounds;
        rectangle2.X = !rightToLeft ? rectangle2.Right - 13 - 1 : 1;
        rectangle2.Width = 13;
        rectangle2.Inflate(0, -1);
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          graphics.FillRectangle(SystemBrushes.Control, rectangle2);
        }
        else
        {
          if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            graphics.DrawLine(pen1, rectangle2.X - 1, rectangle2.Y, rectangle2.X - 1, rectangle2.Bottom);
          if ((state & DrawItemState.Selected) == DrawItemState.Selected)
            this.DrawComboBoxButton(graphics, rectangle2, state);
          else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            this.DrawComboBoxButton(graphics, rectangle2, state);
          else
            this.DrawComboBoxButton(graphics, rectangle2, state);
        }
        if (state == DrawItemState.Default && this is Office2003Renderer)
        {
          --rectangle2.Height;
          using (Pen pen3 = new Pen(SystemColors.Window))
            graphics.DrawRectangle(pen3, rectangle2);
        }
        this.DrawComboDropdownButton(graphics, rectangle2, state);
        pen1.Dispose();
      }

      internal virtual void DrawComboBoxBorder(
        ComboBox comboBox,
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state)
      {
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
          graphics.DrawRectangle(SystemPens.ControlDark, bounds);
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          using (Pen pen = new Pen(this.HighlightBorderColor))
            graphics.DrawRectangle(pen, bounds);
        }
        else
          graphics.DrawRectangle(SystemPens.Control, bounds);
      }

      internal virtual void DrawComboBoxButton(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state)
      {
        if ((state & DrawItemState.Selected) == DrawItemState.Selected)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.r))
            graphics.FillRectangle((Brush) solidBrush, bounds);
        }
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.p))
            graphics.FillRectangle((Brush) solidBrush, bounds);
        }
        else
        {
          graphics.FillRectangle(SystemBrushes.Window, bounds);
          bounds.Inflate(-1, -1);
          graphics.FillRectangle(SystemBrushes.Control, bounds);
        }
      }

      internal virtual void DrawComboDropdownButton(Graphics g, Rectangle bounds, DrawItemState state)
      {
        int x1_1 = bounds.Left + bounds.Width / 2 - 2;
        int num1 = bounds.Top + bounds.Height / 2 - 1;
        Pen pen;
        Brush brush;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          pen = SystemPens.ControlDark;
          brush = SystemBrushes.ControlDark;
        }
        else
        {
          pen = SystemPens.ControlText;
          brush = SystemBrushes.ControlText;
        }
        g.DrawLine(pen, x1_1, num1, x1_1 + 4, num1);
        int x1_2 = x1_1 + 1;
        int num2 = num1 + 1;
        g.DrawLine(pen, x1_2, num2, x1_2 + 2, num2);
        int x = x1_2 + 1;
        int y = num2 + 1;
        g.FillRectangle(brush, new Rectangle(x, y, 1, 1));
      }

      public override void DrawContainerBackground(
        Graphics graphics,
        Rectangle bounds,
        Rectangle layoutBounds)
      {
        graphics.Clear(SystemColors.Control);
      }

      public override void DrawContainerBarBackground(
        ContainerBar containerBar,
        Graphics graphics,
        Rectangle bounds,
        Rectangle clientBounds)
      {
        graphics.Clear(SystemColors.Control);
        bounds.Inflate(-2, -2);
        graphics.DrawLine(SystemPens.ControlLightLight, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
        graphics.DrawLine(SystemPens.ControlLightLight, bounds.X, bounds.Y + 1, bounds.X, bounds.Bottom - 2);
        graphics.DrawLine(SystemPens.ControlLightLight, bounds.Right - 1, bounds.Y + 1, bounds.Right - 1, bounds.Bottom - 2);
        graphics.DrawLine(SystemPens.ControlLightLight, bounds.X + 1, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
        bounds.Inflate(-1, -1);
        using (SolidBrush solidBrush = new SolidBrush(this._menuBackgroundColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      public override void DrawContainerBarClientBackground(Graphics graphics, Rectangle bounds)
      {
        using (SolidBrush solidBrush = new SolidBrush(this._menuBackgroundColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      public override void DrawContainerBarText(
        string text,
        Graphics graphics,
        Font font,
        Rectangle bounds)
      {
        using (Font font1 = new Font(font, FontStyle.Bold))
          graphics.DrawString(text, font1, SystemBrushes.ControlText, (RectangleF) bounds, this._leftStringFormat);
      }

      public override void DrawContainerBarTitleBarBackground(
        Graphics graphics,
        Rectangle bounds,
        bool active)
      {
        if (active)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.p))
            graphics.FillRectangle((Brush) solidBrush, bounds);
        }
        else
          graphics.FillRectangle(SystemBrushes.Control, bounds);
      }

      public override void DrawContainerBarToolBarBackground(Graphics graphics, Rectangle bounds)
      {
        using (SolidBrush solidBrush = new SolidBrush(this._backgroundColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      protected override void DrawContainerItem(
        ControlContainerItem item,
        Graphics graphics,
        Font font,
        DrawItemState state)
      {
        if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          Rectangle buttonInnerBounds = item.ButtonInnerBounds;
          buttonInnerBounds.Inflate(-5, -5);
          ControlPaint.DrawSelectionFrame(graphics, false, item.ButtonInnerBounds, buttonInnerBounds, SystemColors.Control);
        }
        if (item.Text.Length == 0)
          return;
        if (item.ForeColor != SystemColors.ControlText)
        {
          using (SolidBrush solidBrush = new SolidBrush(item.ForeColor))
            this.DrawText(item.Text, graphics, font, (Brush) solidBrush, state, item.ButtonInnerBounds, this._leftStringFormat);
        }
        else
          this.DrawText(item.Text, graphics, font, SystemBrushes.ControlText, state, item.ButtonInnerBounds, this._leftStringFormat);
      }

      public override void DrawFloatingFormBackground(Graphics graphics, Rectangle bounds)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.__f))
          graphics.FillRectangle((Brush) solidBrush, bounds);
        ref Rectangle local1 = ref bounds;
        Size size = SystemInformation.FixedFrameBorderSize;
        int width = -size.Width;
        size = SystemInformation.FixedFrameBorderSize;
        int height1 = -size.Height;
        local1.Inflate(width, height1);
        graphics.DrawLine(SystemPens.Control, bounds.X, bounds.Y - 1, bounds.Right - 1, bounds.Y - 1);
        graphics.DrawLine(SystemPens.Control, bounds.X, bounds.Bottom, bounds.Right - 1, bounds.Bottom);
        graphics.DrawLine(SystemPens.Control, bounds.X - 1, bounds.Y, bounds.X - 1, bounds.Bottom - 1);
        graphics.DrawLine(SystemPens.Control, bounds.Right, bounds.Y, bounds.Right, bounds.Bottom - 1);
        ref Rectangle local2 = ref bounds;
        size = SystemInformation.ToolWindowCaptionButtonSize;
        int height2 = size.Height;
        local2.Height = height2;
        using (SolidBrush solidBrush = new SolidBrush(this.__g))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      public override void DrawFloatingFormText(
        string text,
        Graphics graphics,
        Font font,
        Rectangle bounds)
      {
        using (Font font1 = new Font(font, FontStyle.Bold))
        {
          using (SolidBrush solidBrush = new SolidBrush(this.__h))
            graphics.DrawString(text, font1, (Brush) solidBrush, (RectangleF) bounds, this._leftStringFormat);
        }
      }

      public override void DrawIconCore(
        Icon icon,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds)
      {
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          using (Bitmap bitmap = Bitmap.FromHicon(icon.Handle))
            graphics.DrawImage((Image) bitmap, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
        }
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          if ((state & DrawItemState.Selected) != DrawItemState.Selected && (state & DrawItemState.Checked) != DrawItemState.Checked)
          {
            bounds.Offset(1, 1);
            using (Bitmap bitmap = Bitmap.FromHicon(icon.Handle))
              graphics.DrawImage((Image) bitmap, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
            bounds.Offset(-2, -2);
          }
          graphics.DrawIconUnstretched(icon, bounds);
        }
        else
          graphics.DrawIconUnstretched(icon, bounds);
      }

      public override void DrawImageCore(
        Image image,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds)
      {
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
          graphics.DrawImage(image, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          if ((state & DrawItemState.Selected) != DrawItemState.Selected && (state & DrawItemState.Checked) != DrawItemState.Checked)
          {
            bounds.Offset(1, 1);
            graphics.DrawImage(image, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.v);
            bounds.Offset(-2, -2);
          }
          graphics.DrawImage(image, bounds);
        }
        else
          graphics.DrawImage(image, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.t);
      }

      public override void DrawImageCore(
        ImageList imageList,
        int imageIndex,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds)
      {
        if (imageList == null)
          return;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
          using (Image image = imageList.Images[imageIndex])
            this.DrawImageCore(image, graphics, state, bounds);
        }
        else
          imageList?.Draw(graphics, bounds.X, bounds.Y, imageIndex);
      }

      protected override void DrawLabelItem(
        LabelItem item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state)
      {
        this.DrawButtonHighlight(graphics, item.ButtonBounds, state, false);
        using (Brush brush = (Brush) new SolidBrush(item.ForeColor))
          this.DrawText(item.Text, graphics, font, brush, state, item.ButtonInnerBounds, this._leftStringFormat);
      }

      public override void DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode)
      {
        bounds = new Rectangle(bounds.X + bounds.Width / 2 - 8, bounds.Y + bounds.Height / 2 - 7, 16 /*0x10*/, 16 /*0x10*/);
        if (designMode)
        {
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 8, bounds.Y + 6, bounds.X + 8, bounds.Y + 10);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 6, bounds.Y + 8, bounds.X + 10, bounds.Y + 8);
        }
        else
        {
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 5, bounds.Y + 4, bounds.X + 7, bounds.Y + 6);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 5, bounds.Y + 5, bounds.X + 7, bounds.Y + 7);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 5, bounds.Y + 8, bounds.X + 7, bounds.Y + 10);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 5, bounds.Y + 9, bounds.X + 7, bounds.Y + 11);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 7, bounds.Y + 6, bounds.X + 9, bounds.Y + 4);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 7, bounds.Y + 7, bounds.X + 9, bounds.Y + 5);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 7, bounds.Y + 10, bounds.X + 9, bounds.Y + 8);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 7, bounds.Y + 11, bounds.X + 9, bounds.Y + 9);
        }
      }

      public override void DrawMenuBackground(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        int breakOffset,
        int breakSize,
        MenuOffset menuDirection,
        bool rightToLeft)
      {
        graphics.Clear(this._menuBackgroundColor);
        using (Pen pen = new Pen(this.l))
          graphics.DrawRectangle(pen, bounds);
        if (breakSize != 0)
        {
          int x1;
          int x2;
          int y1;
          int y2;
          this.MeasureBreakLine(bounds, breakOffset, breakSize, menuDirection, rightToLeft, out x1, out x2, out y1, out y2);
          graphics.DrawLine(SystemPens.Control, x1, y1, x2, y2);
        }
        bounds.Inflate(-1, -1);
        ++bounds.Y;
        --bounds.Height;
        if (rightToLeft)
          bounds.X = bounds.Right - (marginWidth - 8) + 1;
        bounds.Width = marginWidth - 8;
        using (SolidBrush solidBrush = new SolidBrush(this.m))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      public override void DrawMenuBarBackground(
        MenuBar menubar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical)
      {
        if (menubar.Situation == ToolBarSituation.Contained)
        {
          Rectangle layoutBounds = ((ToolBarContainer) menubar.Parent).Manager.GetScreenBounds();
          layoutBounds = new Rectangle(menubar.PointToClient(new Point(layoutBounds.X, layoutBounds.Y)), layoutBounds.Size);
          this.DrawContainerBackground(graphics, bounds, layoutBounds);
        }
        else
          this.DrawContainerBackground(graphics, menubar.ClientRectangle, menubar.ClientRectangle);
      }

      protected override void DrawMenuBarItem(
        MenuBarItem item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state)
      {
        if (item.DrawDroppedDown)
          this.PaintPushedDropDownButton(graphics, (TopLevelMenuItemBase) item);
        else
          this.DrawButtonHighlight(graphics, item.ButtonBounds, state, false);
        Rectangle buttonInnerBounds = item.ButtonInnerBounds;
        if ((state & DrawItemState.Selected) == DrawItemState.Selected)
          state ^= DrawItemState.Selected;
        if (item.ForeColor != SystemColors.ControlText)
        {
          using (SolidBrush solidBrush = new SolidBrush(item.ForeColor))
            this.DrawText(item.Text, graphics, item.Font, (Brush) solidBrush, state, buttonInnerBounds, this._centerStringFormat);
        }
        else
          this.DrawText(item.Text, graphics, item.Font, SystemBrushes.ControlText, state, buttonInnerBounds, this._centerStringFormat);
      }

      public override void DrawMenuItem(
        Graphics graphics,
        MenuButtonItem item,
        IPopupMenuHost host,
        int marginWidth,
        DrawItemState state,
        bool drawSpecial)
      {
        if (item.Importance == ToolBarItemImportance.Low)
        {
          Rectangle buttonBounds = item.ButtonBounds;
          if (host.RightToLeft)
            buttonBounds.X = buttonBounds.Right - (marginWidth - 8);
          buttonBounds.Width = marginWidth - 8;
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(5, this.ShadowColor)))
            graphics.FillRectangle((Brush) solidBrush, buttonBounds);
        }
        Rectangle bounds = item.ButtonBounds;
        ++bounds.X;
        bounds.Width -= 3;
        bounds.Height -= 2;
        if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
          this.DrawMenuItemHighlight(graphics, item, bounds);
        Rectangle rectangle = bounds;
        if (item.Checked)
        {
          bool flag = item.Icon == null && item.Image == null && (host.MenuImageList == null || item.ImageIndex < 0 || item.ImageIndex > host.MenuImageList.Images.Count - 1);
          if (host.RightToLeft)
            bounds.X = bounds.Right - (bounds.Height - 2) - 2;
          bounds = !drawSpecial ? new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Height - 2, bounds.Height - 2) : new Rectangle(bounds.X + 1, bounds.Y + bounds.Height / 2 - 9, 19, 19);
          this.DrawMenuItemCheck(graphics, item, flag | drawSpecial, bounds);
        }
        bounds = rectangle;
        bounds.Y += bounds.Height / 2;
        if (item.Icon != null)
        {
          bounds.X = !host.RightToLeft ? marginWidth - item.IconSize.Width - 11 : bounds.Right - marginWidth + 14;
          bounds.Y -= item.IconSize.Height / 2 - 1;
          bounds.Size = item.IconSize;
          try
          {
            using (Icon icon = new Icon(item.Icon, item.IconSize))
              this.DrawIconCore(icon, graphics, state, bounds);
          }
          catch
          {
          }
        }
        else if (item.Image != null)
        {
          if (host.RightToLeft)
          {
            bounds.X = bounds.Right - marginWidth + 14;
          }
          else
          {
            bounds.X = marginWidth - item.Image.Width - 11;
            if (!drawSpecial && bounds.X > 5)
              bounds.X = 5;
          }
          bounds.Y -= item.Image.Height / 2 - 1;
          bounds.Size = item.Image.Size;
          this.DrawImageCore(item.Image, graphics, state, bounds);
        }
        else if (host.MenuImageList != null && item.ImageIndex >= 0 && item.ImageIndex < host.MenuImageList.Images.Count)
        {
          Size imageSize;
          if (host.RightToLeft)
          {
            bounds.X = bounds.Right - marginWidth + 14;
          }
          else
          {
            ref Rectangle local = ref bounds;
            int num1 = marginWidth;
            imageSize = host.MenuImageList.ImageSize;
            int width = imageSize.Width;
            int num2 = num1 - width - 11;
            local.X = num2;
            if (!drawSpecial && bounds.X > 5)
              bounds.X = 5;
          }
          ref Rectangle local1 = ref bounds;
          int y = local1.Y;
          imageSize = host.MenuImageList.ImageSize;
          int num = imageSize.Height / 2 - 1;
          local1.Y = y - num;
          bounds.Size = host.MenuImageList.ImageSize;
          this.DrawImageCore(host.MenuImageList, item.ImageIndex, graphics, state, bounds);
        }
        bounds = item.ButtonBounds;
        bounds.Width -= marginWidth;
        bounds.Width -= 16 /*0x10*/;
        if (host.RightToLeft)
        {
          bounds.X += 18;
          this._menuTextStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
          this._menuShortcutStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        }
        else
        {
          bounds.X += marginWidth - 2;
          this._menuTextStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
          this._menuShortcutStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
        }
        string friendlyShortcut = item.FriendlyShortcut;
        Font font = item.Font ?? host.Font;
        if (item.ForeColor == SystemColors.ControlText)
        {
          this.DrawText(item.Text, graphics, font, SystemBrushes.ControlText, state, bounds, this._menuTextStringFormat);
          if (friendlyShortcut.Length != 0)
            this.DrawText(friendlyShortcut, graphics, font, SystemBrushes.ControlText, state, bounds, this._menuShortcutStringFormat);
        }
        else
        {
          using (SolidBrush solidBrush = new SolidBrush(item.ForeColor))
          {
            this.DrawText(item.Text, graphics, font, (Brush) solidBrush, state, bounds, this._menuTextStringFormat);
            if (friendlyShortcut.Length != 0)
              this.DrawText(friendlyShortcut, graphics, font, (Brush) solidBrush, state, bounds, this._menuShortcutStringFormat);
          }
        }
        if (!item.HasVisibleSubitems())
          return;
        Point[] points = new Point[3];
        Rectangle buttonBounds1 = item.ButtonBounds;
        buttonBounds1.Y += buttonBounds1.Height / 2;
        buttonBounds1.Y -= 5;
        if (host.RightToLeft)
        {
          buttonBounds1.X = 12;
          points[0] = new Point(buttonBounds1.X, buttonBounds1.Y);
          points[1] = new Point(buttonBounds1.X, buttonBounds1.Y + 8);
          points[2] = new Point(buttonBounds1.X - 4, buttonBounds1.Y + 4);
        }
        else
        {
          buttonBounds1.X = buttonBounds1.Right - 12;
          points[0] = new Point(buttonBounds1.X, buttonBounds1.Y);
          points[1] = new Point(buttonBounds1.X + 4, buttonBounds1.Y + 4);
          points[2] = new Point(buttonBounds1.X, buttonBounds1.Y + 8);
        }
        Brush brush = (state & DrawItemState.Disabled) == DrawItemState.Disabled ? SystemBrushes.ControlDark : SystemBrushes.ControlText;
        graphics.FillPolygon(brush, points);
      }

      public virtual void DrawMenuItemCheck(
        Graphics graphics,
        MenuButtonItem item,
        bool drawCheckMark,
        Rectangle bounds)
      {
        Pen pen1 = !item.Enabled ? SystemPens.ControlDark : SystemPens.ControlText;
        if (item.Enabled)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.q))
            graphics.FillRectangle((Brush) solidBrush, bounds);
          using (Pen pen2 = new Pen(this.o))
            graphics.DrawRectangle(pen2, bounds);
        }
        else
          graphics.DrawRectangle(pen1, bounds);
        if (!drawCheckMark)
          return;
        int num = bounds.X + bounds.Width / 2;
        int y1 = bounds.Y + bounds.Height / 2;
        graphics.DrawLine(pen1, num - 3, y1, num - 1, y1 + 2);
        graphics.DrawLine(pen1, num - 3, y1 + 1, num - 1, y1 + 3);
        graphics.DrawLine(pen1, num - 1, y1 + 2, num + 3, y1 - 2);
        graphics.DrawLine(pen1, num - 1, y1 + 3, num + 3, y1 - 1);
      }

      public virtual void DrawMenuItemHighlight(
        Graphics graphics,
        MenuButtonItem item,
        Rectangle bounds)
      {
        if (item.Enabled)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.p))
            graphics.FillRectangle((Brush) solidBrush, bounds);
        }
        using (Pen pen = new Pen(this.o))
          graphics.DrawRectangle(pen, bounds);
      }

      public override void DrawMenuSeparator(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        bool rightToLeft)
      {
        using (Pen pen = new Pen(this.i))
        {
          if (rightToLeft)
            graphics.DrawLine(pen, bounds.Left, bounds.Y + 1, bounds.Right - marginWidth - 1, bounds.Y + 1);
          else
            graphics.DrawLine(pen, marginWidth + 1, bounds.Y + 1, bounds.Right - 1, bounds.Y + 1);
        }
      }

      public override void DrawSystemButton(
        Graphics graphics,
        Rectangle bounds,
        ToolBarGlyphType glyphType,
        DrawItemState state,
        bool floating)
      {
        this.DrawButtonHighlight(graphics, bounds, state, false);
        if ((state & DrawItemState.Selected) == DrawItemState.Selected)
          this.a(graphics, bounds, glyphType, SystemColors.ControlDarkDark);
        else
          this.a(graphics, bounds, glyphType, SystemColors.ControlText);
      }

      protected override void DrawText(
        string text,
        Graphics graphics,
        Font font,
        Brush brush,
        DrawItemState state,
        Rectangle bounds,
        StringFormat textFormat)
      {
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
          graphics.DrawString(text, font, SystemBrushes.ControlDark, (RectangleF) bounds, textFormat);
        else if ((state & DrawItemState.Selected) == DrawItemState.Selected)
          graphics.DrawString(text, font, SystemBrushes.ControlDarkDark, (RectangleF) bounds, textFormat);
        else
          graphics.DrawString(text, font, brush, (RectangleF) bounds, textFormat);
      }

      public override void DrawToolBarActionsButton(
        Graphics graphics,
        Rectangle bounds,
        bool vertical,
        bool chevron,
        DrawItemState state,
        bool designMode)
      {
        if (!vertical)
        {
          ++bounds.X;
          bounds.Y += 2;
          bounds.Height -= 5;
          bounds.Width -= 3;
          if ((state & DrawItemState.Selected) == DrawItemState.Selected)
          {
            using (Pen pen = new Pen(this.l))
            {
              graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
              graphics.DrawLine(pen, bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height - 1);
              graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
            }
          }
          else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            this.DrawButtonHighlight(graphics, bounds, state, false);
          if (designMode)
          {
            int num1 = bounds.X + bounds.Width / 2;
            int num2 = bounds.Y + bounds.Height / 2 - 4;
            graphics.DrawLine(Pens.Black, num1 - 2, num2, num1 + 2, num2);
            graphics.DrawLine(Pens.Black, num1, num2 - 2, num1, num2 + 2);
          }
          if (chevron)
            this.a(graphics, bounds.X + 2, bounds.Y + 5);
          this.c(graphics, bounds.X + 3, bounds.Bottom - 6, Color.Black);
        }
        else
        {
          bounds.Height -= 2;
          if ((state & DrawItemState.Selected) == DrawItemState.Selected)
          {
            using (Pen pen = new Pen(this.l))
            {
              graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
              graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
              graphics.DrawLine(pen, bounds.X, bounds.Bottom, bounds.X + bounds.Width, bounds.Bottom);
            }
          }
          else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            this.DrawButtonHighlight(graphics, bounds, state, false);
          if (designMode)
          {
            int num3 = bounds.X + bounds.Width / 2 + 4;
            int num4 = bounds.Y + bounds.Height / 2;
            graphics.DrawLine(Pens.Black, num3 - 2, num4, num3 + 2, num4);
            graphics.DrawLine(Pens.Black, num3, num4 - 2, num3, num4 + 2);
          }
          this.b(graphics, bounds.X + 6, bounds.Bottom - 8, Color.Black);
        }
      }

      public override void DrawToolBarBackground(
        ToolBar toolbar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical)
      {
        graphics.Clear(SystemColors.Control);
        bounds.Inflate(0, -1);
        using (SolidBrush solidBrush = new SolidBrush(this._backgroundColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
        graphics.FillRectangle(SystemBrushes.Control, new Rectangle(bounds.Right - 1, bounds.Top, 1, 1));
        graphics.FillRectangle(SystemBrushes.Control, new Rectangle(bounds.Right - 1, bounds.Bottom - 1, 1, 1));
        graphics.FillRectangle(SystemBrushes.Control, new Rectangle(bounds.X, bounds.Top, 1, 1));
        graphics.FillRectangle(SystemBrushes.Control, new Rectangle(bounds.X, bounds.Bottom - 1, 1, 1));
      }

      public override void DrawToolBarGrabHandle(Graphics graphics, Rectangle bounds, bool vertical)
      {
        if (vertical)
        {
          for (int x = bounds.X; x <= bounds.Width; x += 2)
            graphics.DrawLine(this.s, x, 3, x, 5);
        }
        else
        {
          for (int y = bounds.Y; y <= bounds.Bottom - 2; y += 2)
            graphics.DrawLine(this.s, 3, y, 5, y);
        }
      }

      public override void DrawToolBarSeparator(Graphics graphics, Rectangle bounds, bool vertical)
      {
        if (vertical)
          graphics.DrawLine(this.s, bounds.Left, bounds.Top + 1, bounds.Right - 1, bounds.Top + 1);
        else
          graphics.DrawLine(this.s, bounds.Left + 1, bounds.Top, bounds.Left + 1, bounds.Bottom - 1);
      }

      public override void FinishToolBarRender()
      {
        this.s.Dispose();
        this.s = (Pen) null;
      }

      protected internal static Color IncreaseBrightness(Color color1, int level)
      {
        int r = (int) color1.R;
        int g = (int) color1.G;
        int b = (int) color1.B;
        int num1 = level;
        int num2 = r + num1;
        int num3 = g + level;
        int num4 = b + level;
        if (num2 > (int) byte.MaxValue)
          num2 = (int) byte.MaxValue;
        if (num3 > (int) byte.MaxValue)
          num3 = (int) byte.MaxValue;
        if (num4 > (int) byte.MaxValue)
          num4 = (int) byte.MaxValue;
        int red = (int) Convert.ToByte(num2);
        byte num5 = Convert.ToByte(num3);
        byte num6 = Convert.ToByte(num4);
        int green = (int) num5;
        int blue = (int) num6;
        return Color.FromArgb(red, green, blue);
      }

      protected internal static Color InterpolateColors(Color color1, Color color2, float percentage)
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

      public override void LayoutContainerBar(
        Rectangle bounds,
        Size toolbarSize,
        out Rectangle titlebarBounds,
        out Rectangle toolbarBounds,
        out Rectangle clientBounds,
        out Rectangle gripperBounds)
      {
        this.a(bounds, toolbarSize, SystemInformation.ToolWindowCaptionHeight, out titlebarBounds, out toolbarBounds, out clientBounds, out gripperBounds);
      }

      internal virtual void PaintPushedDropDownButton(Graphics graphics, TopLevelMenuItemBase item)
      {
        Rectangle buttonBounds = item.ButtonBounds;
        using (Pen pen = new Pen(this.l))
        {
          if (item.MenuDirection != MenuOffset.Left)
            graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Y, buttonBounds.X, buttonBounds.Y + buttonBounds.Height - 1);
          if (item.MenuDirection != MenuOffset.Right)
            graphics.DrawLine(pen, buttonBounds.X + buttonBounds.Width, buttonBounds.Y, buttonBounds.X + buttonBounds.Width, buttonBounds.Y + buttonBounds.Height - 1);
          if (item.MenuDirection != MenuOffset.Bottom)
            graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Bottom, buttonBounds.X + buttonBounds.Width, buttonBounds.Bottom);
          if (item.MenuDirection == MenuOffset.Top)
            return;
          graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Y, buttonBounds.X + buttonBounds.Width, buttonBounds.Y);
        }
      }

      public override void StartToolBarRender(ToolBar toolbar, bool vertical, bool rightToLeft)
      {
        if (toolbar.ShowKeyboardMnemonics || toolbar is MenuBar && ((MenuBar) toolbar).AlwaysShowMnemonics)
        {
          this._leftStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
          this._centerStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
          this._menuTextStringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        }
        else
        {
          this._leftStringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
          this._centerStringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
          this._menuTextStringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
        }
        this._leftStringFormat.FormatFlags = StringFormatFlags.NoWrap;
        this._centerStringFormat.FormatFlags = StringFormatFlags.NoWrap;
        if (vertical)
        {
          this._leftStringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
          this._centerStringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
        }
        if (rightToLeft)
        {
          this._leftStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
          this._centerStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        }
        if (this.s != null)
          this.s.Dispose();
        this.s = new Pen(this.i);
      }

      public override string ToString() => "Office 2002";

      public Color BackgroundColor
      {
        get => this._backgroundColor;
        set
        {
          this._backgroundColor = value;
          this.CustomColors = true;
        }
      }

      public override StringFormat CenterStringFormat => this._centerStringFormat;

      internal ImageAttributes DisabledBlendAttributes => this._disabledBlendAttributes;

      public virtual Color HighlightBorderColor
      {
        get => this._highlightBorderColor;
        set
        {
          this._highlightBorderColor = value;
          this.CustomColors = true;
          this.a(this._highlightBorderColor);
        }
      }

      public override StringFormat LeftStringFormat => this._leftStringFormat;

      public Color MenuBackgroundColor
      {
        get => this._menuBackgroundColor;
        set
        {
          this._menuBackgroundColor = value;
          this.CustomColors = true;
        }
      }

      public override StringFormat MenuShortcutStringFormat => this._menuShortcutStringFormat;

      public override StringFormat MenuTextStringFormat => this._menuTextStringFormat;

      public override Color ShadowColor => Color.Black;
    }
}
