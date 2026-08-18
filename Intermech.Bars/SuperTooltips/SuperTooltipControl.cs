
// Type: SuperTooltips.SuperTooltipControl
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace SuperTooltips
{
    [ToolboxItem(false)]
    public class SuperTooltipControl : Control
    {
      private string _headerText;
      private string _footerText;
      private Image _footerImage;
      private Image _bodyImage;
      private Size _minimalSize;
      private bool _headerVisible;
      private bool _footerVisible;
      private bool _footerSeparator;
      private int _footerImageOffset;
      private ShadowHelper _shadowHelper;
      private bool _standardControl;
      private TooltipColorScheme _predefinedColorScheme;
      private Color _backColor;
      private Color _backColor2;

      public SuperTooltipControl()
      {
        this._headerText = string.Empty;
        this._footerText = string.Empty;
        this._minimalSize = new Size(150, 50);
        this._headerVisible = true;
        this._footerVisible = true;
        this._footerSeparator = true;
        this._footerImageOffset = 8;
      }

      public override Size GetPreferredSize(Size proposedSize)
      {
        return !ControlHelper.IsControlValid((Control) this) ? proposedSize : this.ApplyLayout();
      }

      private TooltipPadding HeaderPadding => new TooltipPadding(6, 6, 8, 4);

      private TooltipPadding FooterPadding => new TooltipPadding(6, 6, 8, 8);

      private TooltipPadding TextPadding => new TooltipPadding(14, 6, 8, 8);

      private TooltipPadding BodyImagePadding => new TooltipPadding(6, 6, 6, 6);

      private Size ApplyLayout()
      {
        TooltipPadding headerPadding = this.HeaderPadding;
        TooltipPadding footerPadding = this.FooterPadding;
        TooltipPadding textPadding = this.TextPadding;
        TooltipPadding bodyImagePadding = this.BodyImagePadding;
        Size empty = Size.Empty;
        Font font1 = this.Font;
        Font font2 = new Font(font1, FontStyle.Bold);
        Graphics graphics = this.CreateGraphics();
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        try
        {
          if (this._headerText != string.Empty && this.HeaderVisible)
          {
            Size size = TextRenderer.MeasureText((IDeviceContext) graphics, this._headerText, font2);
            size.Width += 2;
            size.Height += 2;
            size.Width += headerPadding.Horizontal;
            size.Height += headerPadding.Vertical;
            if (size.Width > empty.Width)
              empty.Width = size.Width;
            empty.Height += size.Height;
          }
          if (this._footerText != string.Empty && this.FooterVisible)
          {
            Size size = TextRenderer.MeasureText((IDeviceContext) graphics, this._footerText, font2);
            size.Width += 2;
            size.Height += 2;
            if (this._footerImage != null)
            {
              size.Width += this._footerImage.Width + this._footerImageOffset;
              if (this._footerImage.Height > size.Height)
                size.Height = this._footerImage.Height;
            }
            size.Width += footerPadding.Horizontal;
            size.Height += footerPadding.Vertical;
            if (size.Width > empty.Width)
              empty.Width = size.Width;
            empty.Height += size.Height;
          }
          if (this.Text != string.Empty)
          {
            int width = empty.Width;
            if (width < this._minimalSize.Width)
              width = this._minimalSize.Width;
            Size size = TextRenderer.MeasureText((IDeviceContext) graphics, this.Text, font1, new Size(width, 0), TextFormatFlags.WordBreak);
            size.Width += textPadding.Horizontal + 4;
            size.Height += textPadding.Vertical + 4;
            if (this._bodyImage != null)
            {
              size.Width += this._bodyImage.Width + bodyImagePadding.Horizontal;
              if (this._bodyImage.Height + bodyImagePadding.Vertical > size.Height)
                size.Height = this._bodyImage.Height + bodyImagePadding.Vertical;
            }
            if (size.Width > empty.Width)
              empty.Width = size.Width;
            empty.Height += size.Height;
          }
          else if (this._bodyImage != null)
          {
            if (this._bodyImage.Width + bodyImagePadding.Horizontal > empty.Width)
              empty.Width = this._bodyImage.Width + bodyImagePadding.Horizontal;
            empty.Height += this._bodyImage.Height + bodyImagePadding.Vertical;
          }
        }
        finally
        {
          graphics.Dispose();
          font2.Dispose();
        }
        empty.Width += 4;
        empty.Height += 4;
        if (empty.Width < this._minimalSize.Width)
          empty.Width = this._minimalSize.Width;
        if (empty.Height < this._minimalSize.Height)
          empty.Height = this._minimalSize.Height;
        return empty;
      }

      private void ApplyColorScheme(TooltipColorScheme colorScheme)
      {
        Color color1;
        Color color2;
        switch (colorScheme)
        {
          case TooltipColorScheme.Blue:
            color1 = Color.FromArgb(221, 230, 247);
            color2 = Color.FromArgb(138, 168, 228);
            break;
          case TooltipColorScheme.Yellow:
            color1 = Color.FromArgb((int) byte.MaxValue, 244, 213);
            color2 = Color.FromArgb((int) byte.MaxValue, 216, 105);
            break;
          case TooltipColorScheme.Green:
            color1 = Color.FromArgb(234, 240 /*0xF0*/, 226);
            color2 = Color.FromArgb(183, 201, 151);
            break;
          case TooltipColorScheme.Red:
            color1 = Color.FromArgb(249, 225, 226);
            color2 = Color.FromArgb(238, 149, 151);
            break;
          case TooltipColorScheme.Purple:
            color1 = Color.FromArgb(234, 227, 245);
            color2 = Color.FromArgb(180, 158, 222);
            break;
          case TooltipColorScheme.Cyan:
            color1 = Color.FromArgb(227, 236, 243);
            color2 = Color.FromArgb(155, 187, 210);
            break;
          case TooltipColorScheme.Orange:
            color1 = Color.FromArgb(252, 233, 217);
            color2 = Color.FromArgb(246, 176 /*0xB0*/, 120);
            break;
          case TooltipColorScheme.Magenta:
            color1 = Color.FromArgb(243, 229, 236);
            color2 = Color.FromArgb(213, 164, 187);
            break;
          case TooltipColorScheme.BlueMist:
            color1 = Color.FromArgb(227, 236, 243);
            color2 = Color.FromArgb(155, 187, 210);
            break;
          case TooltipColorScheme.PurpleMist:
            color1 = Color.FromArgb(232, 227, 234);
            color2 = Color.FromArgb(171, 156, 183);
            break;
          case TooltipColorScheme.Tan:
            color1 = Color.FromArgb(248, 242, 226);
            color2 = Color.FromArgb(232, 209, 153);
            break;
          case TooltipColorScheme.Lemon:
            color1 = Color.FromArgb(252, 253, 215);
            color2 = Color.FromArgb(245, 249, 111);
            break;
          case TooltipColorScheme.Apple:
            color1 = Color.FromArgb(232, 248, 224 /*0xE0*/);
            color2 = Color.FromArgb(173, 231, 146);
            break;
          case TooltipColorScheme.Teal:
            color1 = Color.FromArgb(205, 236, 240 /*0xF0*/);
            color2 = Color.FromArgb(78, 188, 202);
            break;
          case TooltipColorScheme.Silver:
            color1 = Color.FromArgb(225, 225, 232);
            color2 = Color.FromArgb(149, 149, 170);
            break;
          case TooltipColorScheme.Office2003:
            color1 = Color.FromArgb(254, 254, 254);
            color2 = Color.FromArgb(247, 247, 247);
            break;
          case TooltipColorScheme.Gray:
            color1 = Color.White;
            color2 = Color.FromArgb(228, 228, 240 /*0xF0*/);
            break;
          case TooltipColorScheme.System:
            color1 = SystemColors.Info;
            color2 = color1;
            break;
          default:
            color1 = Color.Empty;
            color2 = Color.Empty;
            break;
        }
        if (!color1.IsEmpty)
          this._backColor = color1;
        if (color2.IsEmpty)
          return;
        this._backColor2 = color2;
      }

      protected override void OnLocationChanged(EventArgs e)
      {
        base.OnLocationChanged(e);
        if (this._shadowHelper == null)
          return;
        Win32API.SetWindowPos(this._shadowHelper.Handle.ToInt32(), 0, this.Left + 5, this.Top + 5, 0, 0, 81);
      }

      protected override void OnVisibleChanged(EventArgs e)
      {
        if (!this.Visible && this._shadowHelper != null)
        {
          this._shadowHelper.Hide();
          this._shadowHelper.Dispose();
          this._shadowHelper = (ShadowHelper) null;
        }
        base.OnVisibleChanged(e);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        base.OnPaint(e);
        this.PaintInnerContent(e);
      }

      protected void PaintInnerContent(PaintEventArgs e)
      {
        Rectangle clientRectangle = this.ClientRectangle;
        this.PaintBorder(e.Graphics, clientRectangle);
        clientRectangle.X += 4;
        clientRectangle.Width -= 4;
        clientRectangle.Y += 4;
        clientRectangle.Height -= 4;
        if (clientRectangle.Width <= 4 || clientRectangle.Height <= 4)
          return;
        Graphics graphics = e.Graphics;
        Font font1 = this.Font;
        Font font2 = new Font(font1, FontStyle.Bold);
        TooltipPadding headerPadding = this.HeaderPadding;
        TooltipPadding footerPadding = this.FooterPadding;
        TooltipPadding textPadding = this.TextPadding;
        TooltipPadding bodyImagePadding = this.BodyImagePadding;
        try
        {
          if (this._headerText != string.Empty && this.HeaderVisible)
          {
            Rectangle bounds = new Rectangle(clientRectangle.X + headerPadding.Left, clientRectangle.Y + headerPadding.Top, clientRectangle.Width - headerPadding.Horizontal, clientRectangle.Height - headerPadding.Vertical);
            TextRenderer.DrawText((IDeviceContext) graphics, this._headerText, font2, bounds, this.ForeColor, TextFormatFlags.SingleLine);
            Size size = TextRenderer.MeasureText((IDeviceContext) graphics, this._headerText, font2, new Size(bounds.Width, 0));
            size.Width += headerPadding.Horizontal;
            size.Height += headerPadding.Vertical;
            clientRectangle.Y += size.Height;
            clientRectangle.Height -= size.Height;
          }
          if (this._footerText != string.Empty && this.FooterVisible && clientRectangle.Width > 0 && clientRectangle.Height > 0)
          {
            Size size = TextRenderer.MeasureText((IDeviceContext) graphics, this._footerText, font2, new Size(clientRectangle.Width - footerPadding.Horizontal, 0));
            if (this._footerImage != null && this._footerImage.Height > size.Height)
              size.Height = this._footerImage.Height;
            Rectangle bounds = new Rectangle(clientRectangle.X + footerPadding.Left, clientRectangle.Bottom - size.Height - footerPadding.Bottom, clientRectangle.Width - footerPadding.Horizontal, size.Height);
            if (this.FooterSeparator)
              graphics.DrawLine(SystemPens.ControlDark, 2, bounds.Y - footerPadding.Top - 1, this.ClientRectangle.Right, bounds.Y - footerPadding.Top - 1);
            if (this._footerImage != null)
            {
              graphics.DrawImageUnscaled(this._footerImage, bounds.X, bounds.Y + (bounds.Height - this._footerImage.Height) / 2);
              bounds.X += this._footerImage.Width + this._footerImageOffset;
              bounds.Width -= this._footerImage.Width + this._footerImageOffset;
            }
            if (bounds.Width > 0 && bounds.Height > 0)
              TextRenderer.DrawText((IDeviceContext) graphics, this._footerText, font2, bounds, SystemColors.ControlText, TextFormatFlags.VerticalCenter);
            size.Width += footerPadding.Horizontal;
            size.Height += footerPadding.Vertical;
            clientRectangle.Height -= size.Height;
          }
          if (this._bodyImage != null)
          {
            Rectangle rectangle = new Rectangle(clientRectangle.X + bodyImagePadding.Left, clientRectangle.Y + bodyImagePadding.Top, this._bodyImage.Width, this._bodyImage.Width);
            graphics.DrawImageUnscaled(this._bodyImage, rectangle.Location);
            clientRectangle.X += bodyImagePadding.Horizontal + this._bodyImage.Width;
            clientRectangle.Width -= bodyImagePadding.Horizontal + this._bodyImage.Width;
          }
          if (!(this.Text != string.Empty) || clientRectangle.Width <= 0 || clientRectangle.Height <= 0)
            return;
          Rectangle bounds1 = new Rectangle(clientRectangle.X + textPadding.Left, clientRectangle.Y + textPadding.Top, clientRectangle.Width - textPadding.Horizontal, clientRectangle.Height - textPadding.Vertical);
          if (bounds1.Width <= 0 || bounds1.Height <= 0)
            return;
          TextRenderer.DrawText((IDeviceContext) graphics, this.Text, font1, bounds1, SystemColors.ControlText, TextFormatFlags.WordBreak);
        }
        finally
        {
          font2.Dispose();
        }
      }

      private void PaintBorder(Graphics g, Rectangle bounds)
      {
        bounds.Inflate(-1, -1);
        int num = 4;
        GraphicsPath path = new GraphicsPath();
        path.AddLine(bounds.X, bounds.Bottom - num, bounds.X, bounds.Y + num);
        path.AddArc(bounds.X, bounds.Y, num * 2, num * 2, 180f, 90f);
        path.AddLine(bounds.X + num, bounds.Y, bounds.Right - num, bounds.Y);
        path.AddArc(bounds.Right - num * 2, bounds.Y, num * 2, num * 2, 270f, 90f);
        path.AddLine(bounds.Right, bounds.Y + num, bounds.Right, bounds.Bottom);
        path.AddLine(bounds.Right, bounds.Bottom, bounds.X + num, bounds.Bottom);
        path.AddArc(bounds.X, bounds.Bottom - num * 2, num * 2, num * 2, 90f, 90f);
        path.CloseAllFigures();
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this._backColor, this._backColor2, LinearGradientMode.Vertical))
        {
          g.FillPath((Brush) linearGradientBrush, path);
          g.DrawPath(SystemPens.ControlDark, path);
        }
      }

      public void RecalcSize() => this.Size = this.ApplyLayout();

      public void ShowTooltip(SuperTooltipInfo info, int x, int y, bool enforceScreenPosition)
      {
        this.UpdateWithSuperTooltipInfo(info);
        if (info.CustomSize.IsEmpty)
          this.RecalcSize();
        else
          this.Size = info.CustomSize;
        if (enforceScreenPosition)
        {
          ScreenInfo screenInfo = ControlHelper.GetScreenInfo(Control.MousePosition);
          if (screenInfo != null)
          {
            Rectangle bounds = this.Bounds;
            Size size = screenInfo._workingarea.Size;
            size.Width -= (int) ((double) size.Width * 0.20000000298023224);
            if (bounds.Right > screenInfo._workingarea.Right)
              bounds.X -= bounds.Right - screenInfo._workingarea.Right;
            if (bounds.Bottom > screenInfo._bounds.Bottom)
              bounds.Y = screenInfo._bounds.Bottom - bounds.Height;
            if (bounds.Contains(Control.MousePosition.X, Control.MousePosition.Y))
              bounds.Y = bounds.Height + Control.MousePosition.Y + 1 > screenInfo._workingarea.Height ? Control.MousePosition.Y - bounds.Height - 1 : Control.MousePosition.Y + 1;
            this.Bounds = bounds;
          }
        }
        if (!this.IsHandleCreated)
          this.CreateControl();
        Point point = new Point(x, y);
        this.Location = point;
        if (Win32API.ShowShadow)
        {
          if (this._shadowHelper == null)
          {
            this._shadowHelper = new ShadowHelper(Win32API.AlphaShadow);
            this._shadowHelper.CreateControl();
          }
          this._shadowHelper.Hide();
        }
        if (Win32API.ShowShadow && Environment.OSVersion.Version.Major >= 5)
          Win32API.AnimateWindow(this.Handle.ToInt32(), 100, 524288 /*0x080000*/);
        else
          Win32API.SetWindowPos(this.Handle.ToInt32(), 0, 0, 0, 0, 0, 83);
        if (this._shadowHelper == null)
          return;
        Win32API.SetWindowPos(this._shadowHelper.Handle.ToInt32(), this.Handle.ToInt32(), point.X + 5, point.Y + 5, this.Width - 2, this.Height - 2, 80 /*0x50*/);
        this._shadowHelper.M04Y();
      }

      public void UpdateWithSuperTooltipInfo(SuperTooltipInfo info)
      {
        this._bodyImage = info.BodyImage;
        this.Text = info.BodyText;
        this._footerImage = info.FooterImage;
        this._footerText = info.FooterText;
        this._footerVisible = info.FooterVisible;
        this._headerText = info.HeaderText;
        this._headerVisible = info.HeaderVisible;
        this.PredefinedColor = info.Color;
      }

      [DefaultValue(null)]
      public Image BodyImage
      {
        get => this._bodyImage;
        set => this._bodyImage = value;
      }

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          if (!this._standardControl)
          {
            createParams.Style = -2046820352 /*0x86000000*/;
            createParams.ExStyle = 136;
            createParams.Caption = string.Empty;
          }
          return createParams;
        }
      }

      [DefaultValue(null)]
      public Image FooterImage
      {
        get => this._footerImage;
        set => this._footerImage = value;
      }

      [Browsable(true)]
      [DefaultValue(true)]
      public bool FooterSeparator
      {
        get => this._footerSeparator;
        set => this._footerSeparator = value;
      }

      [DefaultValue("")]
      [Browsable(true)]
      public string FooterText
      {
        get => this._footerText;
        set => this._footerText = value;
      }

      [DefaultValue(true)]
      public bool FooterVisible
      {
        get => this._footerVisible;
        set => this._footerVisible = value;
      }

      [Browsable(true)]
      [DefaultValue("")]
      public string HeaderText
      {
        get => this._headerText;
        set => this._headerText = value;
      }

      [DefaultValue(true)]
      public bool HeaderVisible
      {
        get => this._headerVisible;
        set => this._headerVisible = value;
      }

      [DefaultValue(0)]
      public TooltipColorScheme PredefinedColor
      {
        get => this._predefinedColorScheme;
        set
        {
          this._predefinedColorScheme = value;
          this.ApplyColorScheme(value);
          this.Refresh();
        }
      }

      public bool StandardControl
      {
        get => this._standardControl;
        set => this._standardControl = value;
      }
    }
}
