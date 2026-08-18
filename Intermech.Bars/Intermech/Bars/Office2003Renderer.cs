
// Type: Intermech.Bars.Office2003Renderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class Office2003Renderer : Office2002Renderer
    {
      internal Color _actionsButtonColor1;
      internal Color _actionsButtonColor2;
      internal Color _backgroundGradientColor1;
      internal Color _backgroundGradientColor2;
      internal Color _borderColor;
      internal Color _buttonBackgroundColor1;
      internal Color _buttonBackgroundColor2;
      internal ComboBox _comboBox;
      internal Color _containerBarBackgroundColor1;
      internal Color _containerBarBackgroundColor2;
      internal Color _containerBarBorderColor;
      internal Color _containerBarToolBarBackgroundColor;
      internal Color _formCaptionBackColor;
      internal Color _formCaptionForeColor;
      internal Color _grabHandleColor;
      internal Color _highlightBorderColor;
      internal Color _highlightButtonBackgroundColor1;
      internal Color _highlightButtonBackgroundColor2;
      internal Color _highlightMenuItemBorderColor;
      internal Color _selectedButtonBackgroundColor1;
      internal Color _selectedButtonBackgroundColor2;
      internal Color _selectedTextColor;
      internal Color _toolBarGradientColor1;
      internal Color _toolBarGradientColor2;
      internal Color _toolBarSeparatorColor;
      private Office2003ColorScheme _colorScheme;
      private Color _menuBarBackgroundGradientColor1;
      private Color _menuBorderColor;
      private Color _toolBarGradientColorMid;

      public Office2003Renderer()
      {
        this._colorScheme = Office2003ColorScheme.Automatic;
        this.CalculateBaseColors();
      }

      public Color ActionsButtonColor1
      {
        get => this._actionsButtonColor1;
        set
        {
          this._actionsButtonColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color ActionsButtonColor2
      {
        get => this._actionsButtonColor2;
        set
        {
          this._actionsButtonColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color BackgroundGradientColor1
      {
        get => this._backgroundGradientColor1;
        set
        {
          this._backgroundGradientColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color BackgroundGradientColor2
      {
        get => this._backgroundGradientColor2;
        set
        {
          this._backgroundGradientColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color BorderColor
      {
        get => this._borderColor;
        set
        {
          this._borderColor = value;
          this.CustomColors = true;
        }
      }

      public Office2003ColorScheme ColorScheme
      {
        get => this._colorScheme;
        set
        {
          this._colorScheme = value;
          this.CalculateBaseColors();
          this.OnRedrawRequired();
        }
      }

      public Color ContainerBarBackgroundColor1
      {
        get => this._containerBarBackgroundColor1;
        set
        {
          this._containerBarBackgroundColor1 = value;
          this.CustomColors = true;
        }
      }

      public Color ContainerBarBackgroundColor2
      {
        get => this._containerBarBackgroundColor2;
        set
        {
          this._containerBarBackgroundColor2 = value;
          this.CustomColors = true;
        }
      }

      public Color ContainerBarBorderColor
      {
        get => this._containerBarBorderColor;
        set
        {
          this._containerBarBorderColor = value;
          this.CustomColors = true;
        }
      }

      public Color ContainerBarToolBarBackgroundColor
      {
        get => this._containerBarToolBarBackgroundColor;
        set
        {
          this._containerBarToolBarBackgroundColor = value;
          this.CustomColors = true;
        }
      }

      public Color FormCaptionBackColor
      {
        get => this._formCaptionBackColor;
        set
        {
          this._formCaptionBackColor = value;
          this.CustomColors = true;
        }
      }

      public Color FormCaptionForeColor
      {
        get => this._formCaptionForeColor;
        set
        {
          this._formCaptionForeColor = value;
          this.CustomColors = true;
        }
      }

      public Color GrabHandleColor
      {
        get => this._grabHandleColor;
        set
        {
          this._grabHandleColor = value;
          this.CustomColors = true;
        }
      }

      public override Color HighlightBorderColor
      {
        get => this._highlightBorderColor;
        set => this._highlightBorderColor = value;
      }

      public override Color ShadowColor => this._actionsButtonColor2;

      public Color ToolBarGradientColor1
      {
        get => this._toolBarGradientColor1;
        set
        {
          this._toolBarGradientColor1 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public Color ToolBarGradientColor2
      {
        get => this._toolBarGradientColor2;
        set
        {
          this._toolBarGradientColor2 = value;
          this.CustomColors = true;
          this.CalculateDerivedColors();
        }
      }

      public override void DrawButtonHighlight(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool dropDown)
      {
        if (bounds.IsEmpty)
          return;
        bool flag = (state & DrawItemState.HotLight) == DrawItemState.HotLight || (state & DrawItemState.Selected) == DrawItemState.Selected || (state & DrawItemState.Checked) == DrawItemState.Checked;
        Pen pen = new Pen(this._highlightBorderColor);
        if (flag)
        {
          Brush brush = (state & DrawItemState.Selected) != DrawItemState.Selected ? ((state & DrawItemState.HotLight) != DrawItemState.HotLight ? (Brush) new LinearGradientBrush(bounds, this._buttonBackgroundColor1, this._buttonBackgroundColor2, LinearGradientMode.Vertical) : (Brush) new LinearGradientBrush(bounds, this._highlightButtonBackgroundColor1, this._highlightButtonBackgroundColor2, LinearGradientMode.Vertical)) : (Brush) new LinearGradientBrush(bounds, this._selectedButtonBackgroundColor1, this._selectedButtonBackgroundColor2, LinearGradientMode.Vertical);
          graphics.FillRectangle(brush, bounds);
          graphics.DrawRectangle(pen, bounds);
          brush.Dispose();
        }
        if (dropDown & flag)
        {
          bounds.Offset(bounds.Width - 11, 0);
          bounds.Width -= bounds.Width - 11;
          Brush brush = (Brush) new LinearGradientBrush(bounds, this._highlightButtonBackgroundColor1, this._highlightButtonBackgroundColor2, LinearGradientMode.Vertical);
          graphics.FillRectangle(brush, bounds);
          graphics.DrawRectangle(pen, bounds);
          brush.Dispose();
        }
        pen.Dispose();
      }

      public override void DrawContainerBackground(
        Graphics graphics,
        Rectangle bounds,
        Rectangle layoutBounds)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(layoutBounds.X, layoutBounds.Y), new Point(layoutBounds.Right, layoutBounds.Y), this._backgroundGradientColor1, this._backgroundGradientColor2))
          graphics.FillRectangle((Brush) linearGradientBrush, bounds);
      }

      public override void DrawContainerBarBackground(
        ContainerBar containerBar,
        Graphics graphics,
        Rectangle bounds,
        Rectangle clientBounds)
      {
        using (Brush brush = (Brush) new SolidBrush(this._backgroundGradientColor2))
          graphics.FillRectangle(brush, bounds);
        bounds.Inflate(-2, -2);
        using (Pen pen = new Pen(this._containerBarBorderColor))
        {
          graphics.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
          graphics.DrawLine(pen, bounds.X, bounds.Y + 1, bounds.X, bounds.Bottom - 2);
          graphics.DrawLine(pen, bounds.Right - 1, bounds.Y + 1, bounds.Right - 1, bounds.Bottom - 2);
          graphics.DrawLine(pen, bounds.X + 1, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
        }
        bounds.Inflate(-1, -1);
        using (SolidBrush solidBrush = new SolidBrush(this._containerBarBackgroundColor1))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }

      public override void DrawContainerBarClientBackground(Graphics graphics, Rectangle bounds)
      {
        if (bounds.Width <= 0 || bounds.Height <= 0)
          return;
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._containerBarBackgroundColor1, this._containerBarBackgroundColor2, LinearGradientMode.Vertical))
          graphics.FillRectangle((Brush) linearGradientBrush, bounds);
      }

      public override void DrawContainerBarTitleBarBackground(
        Graphics graphics,
        Rectangle bounds,
        bool active)
      {
        if (bounds.Width <= 0 || bounds.Height <= 0)
          return;
        if (active)
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._buttonBackgroundColor1, this._buttonBackgroundColor2, LinearGradientMode.Vertical))
            graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        else
          this.a(graphics, bounds, LinearGradientMode.Vertical);
      }

      public override void DrawContainerBarToolBarBackground(Graphics graphics, Rectangle bounds)
      {
        using (SolidBrush solidBrush1 = new SolidBrush(this._containerBarToolBarBackgroundColor))
        {
          using (GraphicsPath path = new GraphicsPath())
          {
            Rectangle rect1 = bounds;
            rect1.Inflate(-5, 0);
            path.AddRectangle(rect1);
            Rectangle rect2 = bounds;
            rect2.Y += 5;
            rect2.Height -= 5;
            rect2.Width = 5;
            path.AddRectangle(rect2);
            rect2 = bounds;
            rect2.X = rect2.Right - 5;
            rect2.Width = 5;
            rect2.Height -= 5;
            path.AddRectangle(rect2);
            graphics.FillPath((Brush) solidBrush1, path);
          }
          Rectangle rect3 = bounds with
          {
            Width = 5,
            Height = 5
          };
          using (SolidBrush solidBrush2 = new SolidBrush(this._toolBarGradientColor2))
            graphics.FillRectangle((Brush) solidBrush2, rect3);
          SmoothingMode smoothingMode = graphics.SmoothingMode;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          Rectangle rect4 = bounds with
          {
            Width = 10,
            Height = 10
          };
          graphics.FillEllipse((Brush) solidBrush1, rect4);
          rect4 = new Rectangle(bounds.Right - 10 - 1, bounds.Bottom - 10 - 1, 10, 10);
          graphics.FillEllipse((Brush) solidBrush1, rect4);
          graphics.SmoothingMode = smoothingMode;
        }
      }

      public override void DrawFloatingFormBackground(Graphics graphics, Rectangle bounds)
      {
        using (SolidBrush solidBrush = new SolidBrush(this._formCaptionBackColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
        ref Rectangle local = ref bounds;
        Size fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int width = -fixedFrameBorderSize.Width;
        fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int height = -fixedFrameBorderSize.Height;
        local.Inflate(width, height);
        using (Pen pen = new Pen(this._backgroundGradientColor2))
        {
          graphics.DrawLine(pen, bounds.X, bounds.Y - 1, bounds.Right - 1, bounds.Y - 1);
          graphics.DrawLine(pen, bounds.X, bounds.Bottom, bounds.Right - 1, bounds.Bottom);
          graphics.DrawLine(pen, bounds.X - 1, bounds.Y, bounds.X - 1, bounds.Bottom - 1);
          graphics.DrawLine(pen, bounds.Right, bounds.Y, bounds.Right, bounds.Bottom - 1);
        }
        bounds.Height = SystemInformation.ToolWindowCaptionButtonSize.Height;
        using (SolidBrush solidBrush = new SolidBrush(this._formCaptionBackColor))
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
          using (SolidBrush solidBrush = new SolidBrush(this._formCaptionForeColor))
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
          Bitmap bitmap = Bitmap.FromHicon(icon.Handle);
          graphics.DrawImage((Image) bitmap, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, this.DisabledBlendAttributes);
          bitmap.Dispose();
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
        else
          graphics.DrawImage(image, bounds);
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

      public override void DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode)
      {
        bounds = new Rectangle(bounds.X + bounds.Width / 2 - 8, bounds.Y + bounds.Height / 2 - 7, 15, 15);
        this.a(graphics, bounds, this._toolBarGradientColor1, this._toolBarGradientColor2);
        if (designMode)
        {
          graphics.DrawLine(SystemPens.ControlLightLight, bounds.X + 8, bounds.Y + 7, bounds.X + 8, bounds.Y + 11);
          graphics.DrawLine(SystemPens.ControlLightLight, bounds.X + 6, bounds.Y + 9, bounds.X + 10, bounds.Y + 9);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 7, bounds.Y + 6, bounds.X + 7, bounds.Y + 10);
          graphics.DrawLine(SystemPens.ControlText, bounds.X + 5, bounds.Y + 8, bounds.X + 9, bounds.Y + 8);
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
        graphics.Clear(this.MenuBackgroundColor);
        using (Pen pen = new Pen(this._menuBorderColor))
          graphics.DrawRectangle(pen, bounds);
        if (breakSize != 0)
        {
          using (Pen pen = new Pen(this.MenuBackgroundColor))
          {
            int x1;
            int x2;
            int y1;
            int y2;
            this.MeasureBreakLine(bounds, breakOffset, breakSize, menuDirection, rightToLeft, out x1, out x2, out y1, out y2);
            graphics.DrawLine(pen, x1, y1, x2, y2);
          }
        }
        bounds.Inflate(-1, -1);
        ++bounds.Y;
        --bounds.Height;
        if (rightToLeft)
          bounds.X = bounds.Right - (marginWidth - 8) + 1;
        bounds.Width = marginWidth - 8;
        this.a(graphics, bounds, LinearGradientMode.Horizontal);
      }

      public override void DrawMenuItemCheck(
        Graphics graphics,
        MenuButtonItem item,
        bool drawCheckMark,
        Rectangle bounds)
      {
        Pen pen = !item.Enabled ? SystemPens.ControlDark : SystemPens.ControlText;
        if (item.Enabled)
          this.DrawButtonHighlight(graphics, bounds, DrawItemState.Checked, false);
        else
          graphics.DrawRectangle(pen, bounds);
        if (!drawCheckMark)
          return;
        int num = bounds.X + bounds.Width / 2;
        int y1 = bounds.Y + bounds.Height / 2;
        graphics.DrawLine(pen, num - 3, y1, num - 1, y1 + 2);
        graphics.DrawLine(pen, num - 3, y1 + 1, num - 1, y1 + 3);
        graphics.DrawLine(pen, num - 1, y1 + 2, num + 3, y1 - 2);
        graphics.DrawLine(pen, num - 1, y1 + 3, num + 3, y1 - 1);
      }

      public override void DrawMenuItemHighlight(
        Graphics graphics,
        MenuButtonItem item,
        Rectangle bounds)
      {
        if (item.Enabled)
        {
          using (SolidBrush solidBrush = new SolidBrush(this._highlightMenuItemBorderColor))
            graphics.FillRectangle((Brush) solidBrush, bounds);
        }
        using (Pen pen = new Pen(this._highlightBorderColor))
          graphics.DrawRectangle(pen, bounds);
      }

      public override void DrawMenuSeparator(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        bool rightToLeft)
      {
        using (Pen pen = new Pen(ControlPaint.Dark(this._backgroundGradientColor1, 0.1f)))
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
        if (state != DrawItemState.Default || !floating)
          this.a(graphics, bounds, glyphType, Color.Black);
        else
          this.a(graphics, bounds, glyphType, this._formCaptionForeColor);
      }

      public override void DrawToolBarActionsButton(
        Graphics graphics,
        Rectangle bounds,
        bool vertical,
        bool chevron,
        DrawItemState state,
        bool designMode)
      {
        Point[] points = new Point[8];
        bounds.Inflate(0, -1);
        ++bounds.Height;
        LinearGradientMode linearGradientMode = !vertical ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
        Color color1;
        Color color2;
        if ((state & DrawItemState.Selected) == DrawItemState.Selected)
        {
          color1 = this._selectedButtonBackgroundColor1;
          color2 = this._selectedButtonBackgroundColor2;
        }
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          color1 = this._highlightButtonBackgroundColor1;
          color2 = this._highlightButtonBackgroundColor2;
        }
        else
        {
          color1 = this._actionsButtonColor1;
          color2 = this._actionsButtonColor2;
        }
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, color1, color2, linearGradientMode);
        if (vertical)
        {
          points[0] = new Point(bounds.Right, bounds.Y - 3);
          points[1] = new Point(bounds.Right, bounds.Bottom - 3);
          points[2] = new Point(bounds.Right - 3, bounds.Bottom - 1);
          points[3] = new Point(bounds.X + 2, bounds.Bottom - 1);
          points[4] = new Point(bounds.X, bounds.Bottom - 3);
          points[5] = new Point(bounds.X, bounds.Y - 3);
          points[6] = new Point(bounds.X + 2, bounds.Y);
          points[7] = new Point(bounds.Right - 3, bounds.Y);
        }
        else
        {
          points[0] = new Point(bounds.X, bounds.Y);
          points[1] = new Point(bounds.Right - 2, bounds.Top);
          points[2] = new Point(bounds.Right, bounds.Top + 2);
          points[3] = new Point(bounds.Right, bounds.Bottom - 3);
          points[4] = new Point(bounds.Right - 3, bounds.Bottom);
          points[5] = new Point(bounds.X - 1, bounds.Bottom);
          points[6] = new Point(bounds.X + 2, bounds.Bottom - 3);
          points[7] = new Point(bounds.X + 2, bounds.Top + 2);
        }
        graphics.FillPolygon((Brush) linearGradientBrush, points);
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, color1)))
        {
          if (!vertical)
          {
            graphics.FillRectangle((Brush) solidBrush, bounds.Right - 2, bounds.Top, 1, 1);
            graphics.FillRectangle((Brush) solidBrush, bounds.Right - 1, bounds.Top + 1, 1, 1);
          }
        }
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, color2)))
        {
          if (!vertical)
          {
            graphics.FillRectangle((Brush) solidBrush, bounds.Right - 2, bounds.Bottom - 1, 1, 1);
            graphics.FillRectangle((Brush) solidBrush, bounds.Right - 1, bounds.Bottom - 2, 1, 1);
          }
        }
        if (designMode)
        {
          int num1;
          int num2;
          if (vertical)
          {
            num1 = bounds.X + bounds.Width / 2;
            num2 = bounds.Y + bounds.Height / 2 - 1;
          }
          else
          {
            num1 = bounds.X + bounds.Width / 2 + 1;
            num2 = bounds.Y + bounds.Height / 2 - 1;
          }
          graphics.DrawLine(Pens.White, num1 - 1, num2 + 1, num1 + 3, num2 + 1);
          graphics.DrawLine(Pens.White, num1 + 1, num2 - 1, num1 + 1, num2 + 3);
          graphics.DrawLine(Pens.Black, num1 - 2, num2, num1 + 2, num2);
          graphics.DrawLine(Pens.Black, num1, num2 - 2, num1, num2 + 2);
        }
        else
        {
          if (chevron)
            this.a(graphics, bounds.X + 4, bounds.Y + 4);
          if (vertical)
          {
            this.a(graphics, bounds.Right - 6, bounds.Bottom - 8, Color.White);
            this.a(graphics, bounds.Right - 7, bounds.Bottom - 9, Color.Black);
            graphics.DrawLine(Pens.Black, bounds.Right - 10, bounds.Y + 2, bounds.Right - 10, bounds.Bottom - 5);
            graphics.DrawLine(Pens.White, bounds.Right - 9, bounds.Y + 3, bounds.Right - 9, bounds.Bottom - 4);
          }
          else
          {
            this.c(graphics, bounds.X + 6, bounds.Bottom - 6, Color.White);
            this.c(graphics, bounds.X + 5, bounds.Bottom - 7, Color.Black);
            graphics.DrawLine(Pens.Black, bounds.X + 5, bounds.Bottom - 10, bounds.X + 9, bounds.Bottom - 10);
            graphics.DrawLine(Pens.White, bounds.X + 6, bounds.Bottom - 9, bounds.X + 10, bounds.Bottom - 9);
          }
        }
        linearGradientBrush.Dispose();
      }

      public override void DrawToolBarBackground(
        ToolBar toolbar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical)
      {
        int num = toolbar.Situation == ToolBarSituation.Contained ? 1 : 0;
        bool flag1 = true;
        bool flag2 = toolbar.Situation == ToolBarSituation.Contained;
        if (num != 0)
        {
          using (Brush toolbarBackgroundBrush = (Brush) this.GetToolbarBackgroundBrush(toolbar))
            graphics.FillRectangle(toolbarBackgroundBrush, bounds);
        }
        if (toolbar.Situation == ToolBarSituation.Contained)
        {
          if (vertical)
          {
            ++bounds.X;
            --bounds.Width;
          }
          else
          {
            ++bounds.Y;
            --bounds.Height;
          }
        }
        LinearGradientMode A_2 = vertical ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical;
        if (flag1)
        {
          if (flag2)
          {
            Point[] A_3 = new Point[8]
            {
              new Point(bounds.Left + 2, bounds.Bottom),
              new Point(bounds.Left, bounds.Bottom - 3),
              new Point(bounds.Left, bounds.Top + 2),
              new Point(bounds.Left + 2, bounds.Top),
              new Point(bounds.Right - 2, bounds.Top),
              new Point(bounds.Right, bounds.Top + 2),
              new Point(bounds.Right, bounds.Bottom - 3),
              new Point(bounds.Right - 3, bounds.Bottom)
            };
            this.DrawToolbarBackground(graphics, bounds, A_2, A_3);
            Color color1 = Color.FromArgb(100, this._toolBarGradientColor1);
            using (SolidBrush solidBrush = new SolidBrush(color1))
            {
              graphics.FillRectangle((Brush) solidBrush, bounds.Left + 1, bounds.Top, 1, 1);
              graphics.FillRectangle((Brush) solidBrush, bounds.Left, bounds.Top + 1, 1, 1);
            }
            if (!toolbar.DrawActionsButton)
            {
              if (vertical)
                color1 = Color.FromArgb(100, this._toolBarGradientColor2);
              using (SolidBrush solidBrush = new SolidBrush(color1))
              {
                graphics.FillRectangle((Brush) solidBrush, bounds.Right - 2, bounds.Top, 1, 1);
                graphics.FillRectangle((Brush) solidBrush, bounds.Right - 1, bounds.Top + 1, 1, 1);
              }
            }
            Color color2 = Color.FromArgb(100, this._toolBarGradientColor2);
            if (!toolbar.DrawActionsButton)
            {
              using (SolidBrush solidBrush = new SolidBrush(color2))
              {
                graphics.FillRectangle((Brush) solidBrush, bounds.Right - 2, bounds.Bottom - 1, 1, 1);
                graphics.FillRectangle((Brush) solidBrush, bounds.Right - 1, bounds.Bottom - 2, 1, 1);
              }
            }
            if (vertical)
              color2 = Color.FromArgb(100, this._toolBarGradientColor1);
            using (SolidBrush solidBrush = new SolidBrush(color2))
            {
              graphics.FillRectangle((Brush) solidBrush, bounds.Left + 1, bounds.Bottom - 1, 1, 1);
              graphics.FillRectangle((Brush) solidBrush, bounds.Left, bounds.Bottom - 2, 1, 1);
            }
          }
          else
            this.a(graphics, bounds, A_2);
        }
        if (toolbar.Situation != ToolBarSituation.Contained || !toolbar.DrawActionsButton)
          return;
        using (Pen pen = new Pen(this._borderColor))
        {
          if (vertical)
            graphics.DrawLine(pen, bounds.Right - 1, bounds.Top + 3, bounds.Right - 1, bounds.Bottom - 3);
          else
            graphics.DrawLine(pen, bounds.X + 2, bounds.Bottom - 1, bounds.Right - 3, bounds.Bottom - 1);
        }
      }

      public override void DrawToolBarGrabHandle(Graphics graphics, Rectangle bounds, bool vertical)
      {
        using (SolidBrush solidBrush = new SolidBrush(this._grabHandleColor))
        {
          if (vertical)
          {
            int num1 = (bounds.Width - 2) / 4 * 4 - 2;
            int y = bounds.Y + bounds.Height / 2 - 1;
            int num2 = bounds.X + bounds.Width / 2 - num1 / 2 - 1;
            for (int x = num2; x <= num2 + num1; x += 4)
            {
              graphics.FillRectangle(SystemBrushes.ControlLightLight, new Rectangle(x + 1, y + 1, 2, 2));
              graphics.FillRectangle((Brush) solidBrush, new Rectangle(x, y, 2, 2));
            }
          }
          else
          {
            int num3 = (bounds.Height - 2) / 4 * 4 - 2;
            int x = bounds.X + bounds.Width / 2 - 1;
            int num4 = bounds.Y + bounds.Height / 2 - num3 / 2;
            for (int y = num4; y <= num4 + num3; y += 4)
            {
              graphics.FillRectangle(SystemBrushes.ControlLightLight, new Rectangle(x + 1, y + 1, 2, 2));
              graphics.FillRectangle((Brush) solidBrush, new Rectangle(x, y, 2, 2));
            }
          }
        }
      }

      public override void DrawToolBarSeparator(Graphics graphics, Rectangle bounds, bool vertical)
      {
        using (Pen pen = new Pen(this._toolBarSeparatorColor))
        {
          if (vertical)
          {
            graphics.DrawLine(pen, bounds.Left + 4, bounds.Top + 1, bounds.Right - 5, bounds.Top + 1);
            graphics.DrawLine(SystemPens.ControlLightLight, bounds.Left + 5, bounds.Top + 2, bounds.Right - 4, bounds.Top + 2);
          }
          else
          {
            graphics.DrawLine(pen, bounds.Left + 1, bounds.Top + 4, bounds.Left + 1, bounds.Bottom - 5);
            graphics.DrawLine(SystemPens.ControlLightLight, bounds.Left + 2, bounds.Top + 5, bounds.Left + 2, bounds.Bottom - 4);
          }
        }
      }

      public override void LayoutContainerBar(
        Rectangle bounds,
        Size toolbarSize,
        out Rectangle titlebarBounds,
        out Rectangle toolbarBounds,
        out Rectangle clientBounds,
        out Rectangle gripperBounds)
      {
        this.a(bounds, toolbarSize, 25, out titlebarBounds, out toolbarBounds, out clientBounds, out gripperBounds);
        gripperBounds = titlebarBounds;
        ++gripperBounds.X;
        gripperBounds.Inflate(0, -3);
        gripperBounds.Width = 6;
      }

      public override string ToString() => "Office 2003";

      internal virtual void ApplyLunaBlueColors()
      {
        this._backgroundGradientColor1 = Color.FromArgb(158, 190, 245);
        this._backgroundGradientColor2 = Color.FromArgb(195, 218, 249);
        this._toolBarGradientColor1 = Color.FromArgb(221, 236, 254);
        this._toolBarGradientColor2 = Color.FromArgb(129, 169, 226);
        this._grabHandleColor = Color.FromArgb(39, 65, 118);
        this._actionsButtonColor1 = Color.FromArgb(117, 166, 241);
        this._actionsButtonColor2 = Color.FromArgb(0, 53, 145);
        this._borderColor = Color.FromArgb(59, 97, 156);
        this._formCaptionBackColor = Color.FromArgb(42, 102, 201);
        this._formCaptionForeColor = Color.White;
        this._toolBarSeparatorColor = Color.FromArgb(106, 140, 203);
        this._containerBarBorderColor = Color.FromArgb(185, 212, 249);
        this._containerBarBackgroundColor1 = Color.FromArgb(221, 236, 254);
        this._containerBarBackgroundColor2 = Color.FromArgb(74, 122, 201);
        this._containerBarToolBarBackgroundColor = Color.FromArgb(74, 122, 201);
        this._selectedTextColor = SystemColors.ControlText;
        this._highlightBorderColor = Color.FromArgb(0, 0, 128 /*0x80*/);
        this._highlightButtonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
        this._highlightButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._selectedButtonBackgroundColor1 = Color.FromArgb(254, 145, 78);
        this._selectedButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor2 = Color.FromArgb(254, 145, 78);
        this._highlightMenuItemBorderColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
      }

      internal virtual void ApplyLunaOliveColors()
      {
        this._backgroundGradientColor1 = Color.FromArgb(217, 217, 167);
        this._backgroundGradientColor2 = Color.FromArgb(242, 240 /*0xF0*/, 228);
        this._toolBarGradientColor1 = Color.FromArgb(244, 247, 222);
        this._toolBarGradientColor2 = Color.FromArgb(183, 198, 145);
        this._grabHandleColor = Color.FromArgb(81, 94, 51);
        this._actionsButtonColor1 = Color.FromArgb(176 /*0xB0*/, 194, 140);
        this._actionsButtonColor2 = Color.FromArgb(96 /*0x60*/, 119, 107);
        this._borderColor = Color.FromArgb(96 /*0x60*/, 128 /*0x80*/, 88);
        this._formCaptionBackColor = Color.FromArgb(116, 134, 94);
        this._formCaptionForeColor = Color.White;
        this._toolBarSeparatorColor = Color.FromArgb(96 /*0x60*/, 128 /*0x80*/, 88);
        this._containerBarBorderColor = Color.White;
        this._containerBarBackgroundColor1 = Color.FromArgb(243, 242, 231);
        this._containerBarBackgroundColor2 = Color.FromArgb(159, 171, 128 /*0x80*/);
        this._containerBarToolBarBackgroundColor = Color.FromArgb(116, 134, 94);
        this._selectedTextColor = SystemColors.ControlText;
        this._highlightBorderColor = Color.FromArgb(63 /*0x3F*/, 93, 56);
        this._highlightButtonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
        this._highlightButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._selectedButtonBackgroundColor1 = Color.FromArgb(254, 145, 78);
        this._selectedButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor2 = Color.FromArgb(254, 145, 78);
        this._highlightMenuItemBorderColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
      }

      internal virtual void ApplyLunaSilverColors()
      {
        this._backgroundGradientColor1 = Color.FromArgb(215, 215, 229);
        this._backgroundGradientColor2 = Color.FromArgb(243, 243, 247);
        this._toolBarGradientColor1 = Color.FromArgb(243, 244, 250);
        this._toolBarGradientColor2 = Color.FromArgb(140, 138, 172);
        this._grabHandleColor = Color.FromArgb(84, 84, 117);
        this._actionsButtonColor1 = Color.FromArgb(179, 178, 200);
        this._actionsButtonColor2 = Color.FromArgb(118, 116, 146);
        this._borderColor = Color.FromArgb(124, 124, 148);
        this._formCaptionBackColor = Color.FromArgb(122, 121, 153);
        this._formCaptionForeColor = Color.White;
        this._toolBarSeparatorColor = Color.FromArgb(110, 109, 143);
        this._containerBarBorderColor = Color.White;
        this._containerBarBackgroundColor1 = Color.FromArgb(238, 238, 244);
        this._containerBarBackgroundColor2 = Color.FromArgb(162, 162, 181);
        this._containerBarToolBarBackgroundColor = Color.FromArgb(122, 121, 153);
        this._selectedTextColor = SystemColors.ControlText;
        this._highlightBorderColor = Color.FromArgb(75, 75, 111);
        this._highlightButtonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 244, 204);
        this._highlightButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._selectedButtonBackgroundColor1 = Color.FromArgb(254, 145, 78);
        this._selectedButtonBackgroundColor2 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor1 = Color.FromArgb((int) byte.MaxValue, 211, 142);
        this._buttonBackgroundColor2 = Color.FromArgb(254, 145, 78);
        this._highlightMenuItemBorderColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
      }

      internal virtual void ApplyStandardColors()
      {
        this._backgroundGradientColor1 = SystemColors.Control;
        this._backgroundGradientColor2 = Office2002Renderer.InterpolateColors(SystemColors.Control, SystemColors.ControlLightLight, 0.8f);
        this._toolBarGradientColor1 = this._backgroundGradientColor2;
        this._toolBarGradientColor2 = Office2002Renderer.InterpolateColors(this._backgroundGradientColor1, Color.Black, 0.03f);
        this._grabHandleColor = SystemColors.ControlDark;
        this._actionsButtonColor2 = SystemColors.AppWorkspace;
        this._actionsButtonColor1 = Office2002Renderer.IncreaseBrightness(this._actionsButtonColor2, 32 /*0x20*/);
        this._borderColor = SystemColors.Control;
        this._formCaptionBackColor = SystemColors.AppWorkspace;
        this._formCaptionForeColor = SystemColors.ActiveCaptionText;
        this._toolBarSeparatorColor = SystemColors.ControlDark;
        this._containerBarBackgroundColor1 = Office2002Renderer.InterpolateColors(SystemColors.Control, SystemColors.ControlLightLight, 0.5f);
        this._containerBarBackgroundColor2 = SystemColors.Control;
        this._containerBarBorderColor = this._containerBarBackgroundColor1;
        this._containerBarToolBarBackgroundColor = Office2002Renderer.InterpolateColors(SystemColors.AppWorkspace, SystemColors.ControlLightLight, 0.3f);
        this._selectedTextColor = SystemColors.ControlLightLight;
        this._highlightBorderColor = SystemColors.Highlight;
        this._highlightButtonBackgroundColor1 = Office2002Renderer.InterpolateColors(this._highlightBorderColor, SystemColors.Window, 0.7f);
        this._highlightButtonBackgroundColor2 = this._highlightButtonBackgroundColor1;
        this._selectedButtonBackgroundColor1 = Office2002Renderer.InterpolateColors(this._highlightBorderColor, SystemColors.Window, 0.5f);
        this._selectedButtonBackgroundColor2 = this._selectedButtonBackgroundColor1;
        this._buttonBackgroundColor1 = Office2002Renderer.InterpolateColors(this._highlightBorderColor, SystemColors.Window, 0.85f);
        this._buttonBackgroundColor2 = this._buttonBackgroundColor1;
        this._highlightMenuItemBorderColor = this._highlightButtonBackgroundColor1;
      }

      internal override void DrawComboBoxBorder(
        ComboBox comboBox,
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state)
      {
        base.DrawComboBoxBorder(comboBox, graphics, bounds, state);
        ToolBar parent = comboBox.Parent as ToolBar;
        if ((state & DrawItemState.Disabled) == DrawItemState.Disabled || parent == null || (state & DrawItemState.HotLight) == DrawItemState.HotLight)
          return;
        LinearGradientBrush linearGradientBrush;
        if (comboBox == null)
          linearGradientBrush = new LinearGradientBrush(bounds, this._toolBarGradientColor1, this._toolBarGradientColor2, LinearGradientMode.Vertical);
        else if (parent is MenuBar && parent.Parent is ToolBarContainer && ((ToolBarContainer) parent.Parent).Manager != null)
        {
          Rectangle screenBounds = ((ToolBarContainer) parent.Parent).Manager.GetScreenBounds();
          linearGradientBrush = new LinearGradientBrush(comboBox.PointToClient(new Point(screenBounds.X, screenBounds.Y)), comboBox.PointToClient(new Point(screenBounds.Right, screenBounds.Y)), this.BackgroundGradientColor1, this.BackgroundGradientColor2);
        }
        else
        {
          linearGradientBrush = new LinearGradientBrush(new Point(0, -comboBox.Top), new Point(0, parent.Height - comboBox.Top), this._toolBarGradientColor1, this._toolBarGradientColor2);
          linearGradientBrush.InterpolationColors = new ColorBlend(3)
          {
            Colors = new Color[3]
            {
              this._toolBarGradientColor1,
              this._toolBarGradientColorMid,
              this._toolBarGradientColor2
            },
            Positions = new float[3]{ 0.0f, 0.5f, 1f }
          };
        }
        Rectangle rect1 = bounds with { Height = 1 };
        graphics.FillRectangle((Brush) linearGradientBrush, rect1);
        Rectangle rect2 = bounds with { Width = 1 };
        graphics.FillRectangle((Brush) linearGradientBrush, rect2);
        Rectangle rect3 = bounds;
        rect3.X = rect3.Right;
        rect3.Width = 1;
        graphics.FillRectangle((Brush) linearGradientBrush, rect3);
        rect3 = bounds;
        rect3.Y = rect3.Bottom;
        rect3.Height = 1;
        ++rect3.Width;
        graphics.FillRectangle((Brush) linearGradientBrush, rect3);
        linearGradientBrush.Dispose();
      }

      internal override void DrawComboBoxButton(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state)
      {
        if ((state & DrawItemState.Selected) == DrawItemState.Selected)
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._selectedButtonBackgroundColor1, this._selectedButtonBackgroundColor2, LinearGradientMode.Vertical))
            graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        else if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._highlightButtonBackgroundColor1, this._highlightButtonBackgroundColor2, LinearGradientMode.Vertical))
            graphics.FillRectangle((Brush) linearGradientBrush, bounds);
        }
        else
          this.a(graphics, bounds, LinearGradientMode.Vertical);
      }

      internal override void PaintPushedDropDownButton(Graphics graphics, TopLevelMenuItemBase item)
      {
        Rectangle buttonBounds = item.ButtonBounds;
        using (Pen pen = new Pen(this._menuBorderColor))
        {
          if (item.MenuDirection != MenuOffset.Left)
            graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Y, buttonBounds.X, buttonBounds.Y + buttonBounds.Height - 1);
          graphics.DrawLine(pen, buttonBounds.X + buttonBounds.Width, buttonBounds.Y, buttonBounds.X + buttonBounds.Width, buttonBounds.Y + buttonBounds.Height - 1);
          if (item.MenuDirection != MenuOffset.Bottom)
            graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Bottom, buttonBounds.X + buttonBounds.Width, buttonBounds.Bottom);
          if (item.MenuDirection == MenuOffset.Top)
            return;
          graphics.DrawLine(pen, buttonBounds.X, buttonBounds.Y, buttonBounds.X + buttonBounds.Width, buttonBounds.Y);
        }
      }

      protected override void CalculateBaseColors()
      {
        base.CalculateBaseColors();
        switch (this._colorScheme)
        {
          case Office2003ColorScheme.Automatic:
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
            if ((str1 = XPThemeManager.c()) != null)
            {
              string str2 = string.IsInterned(str1);
              if (str2 != "NormalColor")
              {
                switch (str2)
                {
                  case "HomeStead":
                    this.ApplyLunaOliveColors();
                    goto label_16;
                  case "Metallic":
                    this.ApplyLunaSilverColors();
                    goto label_16;
                }
              }
              else
              {
                this.ApplyLunaBlueColors();
                break;
              }
            }
            this.ApplyStandardColors();
            break;
          case Office2003ColorScheme.Standard:
            this.ApplyStandardColors();
            break;
          case Office2003ColorScheme.LunaBlue:
            this.ApplyLunaBlueColors();
            break;
          case Office2003ColorScheme.LunaOlive:
            this.ApplyLunaOliveColors();
            break;
          case Office2003ColorScheme.LunaSilver:
            this.ApplyLunaSilverColors();
            break;
        }
    label_16:
        this.CalculateDerivedColors();
      }

      protected override void CalculateDerivedColors()
      {
        base.CalculateDerivedColors();
        this._menuBorderColor = Office2002Renderer.InterpolateColors(this._actionsButtonColor2, Color.Black, 0.1f);
        this._menuBarBackgroundGradientColor1 = Office2002Renderer.InterpolateColors(this._backgroundGradientColor2, Color.White, 0.5f);
        this._toolBarGradientColorMid = Office2002Renderer.InterpolateColors(this._toolBarGradientColor1, this._toolBarGradientColor2, 0.25f);
      }

      protected override void DrawMenuBarItem(
        MenuBarItem item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state)
      {
        if (item.DrawDroppedDown)
        {
          Rectangle buttonBounds = item.ButtonBounds;
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(buttonBounds, this._menuBarBackgroundGradientColor1, this._backgroundGradientColor1, LinearGradientMode.Vertical))
            graphics.FillRectangle((Brush) linearGradientBrush, buttonBounds);
          this.PaintPushedDropDownButton(graphics, (TopLevelMenuItemBase) item);
        }
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
        {
          using (Brush brush1 = (Brush) new SolidBrush(this._selectedTextColor))
            graphics.DrawString(text, font, brush1, (RectangleF) bounds, textFormat);
        }
        else
          graphics.DrawString(text, font, brush, (RectangleF) bounds, textFormat);
      }

      private void a(Graphics A_0, Rectangle A_1, LinearGradientMode A_2)
      {
        if (A_1.Width <= 0 || A_1.Height <= 0)
          return;
        using (Brush toolbarBackgroundBrush = this.GetToolbarBackgroundBrush(A_1, A_2))
          A_0.FillRectangle(toolbarBackgroundBrush, A_1);
      }

      private void a(Graphics A_0, int A_1, int A_2)
      {
        A_0.DrawLine(Pens.Black, A_1, A_2, A_1, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1, A_2 + 1, A_1 + 1, A_2 + 1);
        A_0.DrawLine(Pens.White, A_1 + 1, A_2 + 2, A_1 + 1, A_2 + 3);
        A_0.DrawLine(Pens.White, A_1 + 1, A_2 + 2, A_1 + 2, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 4, A_2, A_1 + 4, A_2 + 2);
        A_0.DrawLine(Pens.Black, A_1 + 4, A_2 + 1, A_1 + 5, A_2 + 1);
        A_0.DrawLine(Pens.White, A_1 + 5, A_2 + 2, A_1 + 5, A_2 + 3);
        A_0.DrawLine(Pens.White, A_1 + 5, A_2 + 2, A_1 + 6, A_2 + 2);
      }

      private void a(Graphics A_0, Rectangle A_1, Color A_2, Color A_3)
      {
        SmoothingMode smoothingMode = A_0.SmoothingMode;
        A_0.SmoothingMode = SmoothingMode.AntiAlias;
        GraphicsPath path = new GraphicsPath();
        Rectangle rect = A_1;
        rect.Offset(-Convert.ToInt32((double) A_1.Width * 0.2), -Convert.ToInt32((double) A_1.Height * 0.2));
        rect.Inflate(Convert.ToInt32((double) A_1.Width * 0.3), Convert.ToInt32((double) A_1.Width * 0.3));
        path.AddEllipse(rect);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.CenterColor = A_2;
          Color[] colorArray = new Color[1]{ A_3 };
          pathGradientBrush.SurroundColors = colorArray;
          A_0.FillEllipse((Brush) pathGradientBrush, A_1);
        }
        A_0.SmoothingMode = smoothingMode;
      }

      private void DrawToolbarBackground(
        Graphics A_0,
        Rectangle A_1,
        LinearGradientMode A_2,
        Point[] A_3)
      {
        if (A_1.Width <= 0 || A_1.Height <= 0)
          return;
        using (Brush toolbarBackgroundBrush = this.GetToolbarBackgroundBrush(A_1, A_2))
          A_0.FillPolygon(toolbarBackgroundBrush, A_3);
      }

      private LinearGradientBrush GetToolbarBackgroundBrush(ToolBar toolbar)
      {
        if (!(toolbar.Parent is ToolBarContainer) || ((ToolBarContainer) toolbar.Parent).Manager == null)
          return new LinearGradientBrush(toolbar.ClientRectangle, this._backgroundGradientColor1, this._backgroundGradientColor2, LinearGradientMode.Horizontal);
        Rectangle screenBounds = ((ToolBarContainer) toolbar.Parent).Manager.GetScreenBounds();
        return new LinearGradientBrush(toolbar.PointToClient(new Point(screenBounds.X, screenBounds.Y)), toolbar.PointToClient(new Point(screenBounds.Right, screenBounds.Y)), this._backgroundGradientColor1, this._backgroundGradientColor2);
      }

      private Brush GetToolbarBackgroundBrush(Rectangle A_0, LinearGradientMode A_1)
      {
        return (Brush) new LinearGradientBrush(A_0, this._toolBarGradientColor1, this._toolBarGradientColor2, A_1)
        {
          InterpolationColors = new ColorBlend(3)
          {
            Colors = new Color[3]
            {
              this._toolBarGradientColor1,
              this._toolBarGradientColorMid,
              this._toolBarGradientColor2
            },
            Positions = new float[3]{ 0.0f, 0.5f, 1f }
          }
        };
      }
    }
}
