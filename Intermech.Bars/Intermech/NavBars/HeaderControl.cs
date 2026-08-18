
// Type: Intermech.NavBars.HeaderControl
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    public class HeaderControl : ContainerControl
    {
      private INavBarRenderer _renderer;
      private bool _rendererNeedDispose;
      private Font _headerFont;
      private Rectangle _bounds;
      private HeaderStyle _headerStyle;
      private Image _image;

      public HeaderControl()
      {
        this._bounds = Rectangle.Empty;
        this._headerStyle = HeaderStyle.MainHeading;
        this._image = (Image) null;
        this._renderer = (INavBarRenderer) new NavBarRenderer();
        this._rendererNeedDispose = true;
        this.DockPadding.Left = 1;
        this.DockPadding.Right = 1;
        this.DockPadding.Bottom = 1;
        this.DockPadding.Top = 30;
        this._headerFont = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.CreateLayout();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (this._rendererNeedDispose)
          {
            if (this._renderer is NavBarRenderer renderer)
              renderer.Dispose();
            this._rendererNeedDispose = false;
          }
          this._headerFont.Dispose();
        }
        base.Dispose(disposing);
      }

      private void CreateLayout()
      {
        using (Graphics graphics = this.CreateGraphics())
        {
          this._bounds = this.ClientRectangle;
          this.SuspendLayout();
          if (this._headerStyle == HeaderStyle.MainHeading)
          {
            this._bounds.Inflate(-1, -1);
            this._bounds.Height = (int) Math.Ceiling((double) graphics.MeasureString("X|L", this._headerFont).Height) + 3;
            this.DockPadding.All = 1;
          }
          else
          {
            this._bounds.Height = (int) Math.Ceiling((double) graphics.MeasureString("X|L", this.Font).Height) + 5;
            this.DockPadding.All = 0;
          }
          if (this._image != null && this._image.Height > this._bounds.Height - 4)
            this._bounds.Height = this._image.Height + 4;
          this.DockPadding.Top = this._bounds.Bottom;
          this.ResumeLayout();
        }
        this.Invalidate();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        base.OnPaint(e);
        if (this._headerStyle == HeaderStyle.MainHeading)
        {
          this._renderer.DrawBackground(e.Graphics, this.ClientRectangle, this.BackColor);
          this._renderer.DrawHeader(e.Graphics, this._bounds, base.Text, this._headerFont, this._image);
        }
        else
        {
          e.Graphics.Clear(this.BackColor);
          this._renderer.DrawDivider(e.Graphics, this._bounds, base.Text, this.Font, this.ForeColor);
        }
      }

      protected override void OnResize(EventArgs e)
      {
        base.OnResize(e);
        this.CreateLayout();
      }

      public void SetActiveRenderer(INavBarRenderer renderer)
      {
        if (renderer == null)
          throw new ArgumentException();
        if (renderer == this._renderer)
          return;
        if (this._rendererNeedDispose)
        {
          if (this._renderer is NavBarRenderer renderer1)
            renderer1.Dispose();
          this._rendererNeedDispose = false;
        }
        this._renderer = renderer;
        this.Invalidate();
      }

      [Category("Appearance")]
      [Description("The font to use for drawing the text in the header of the control.")]
      public Font HeaderFont
      {
        get => this._headerFont;
        set
        {
          if (this._headerFont == value)
            return;
          this._headerFont = value;
          this.CreateLayout();
        }
      }

      [Description("Indicates whether the heading will be a main heading or a sub heading.")]
      [DefaultValue(HeaderStyle.MainHeading)]
      [Category("Appearance")]
      public HeaderStyle HeaderStyle
      {
        get => this._headerStyle;
        set
        {
          this._headerStyle = value;
          this.CreateLayout();
        }
      }

      [DefaultValue(null)]
      [Description("The image to show in the header.")]
      [Category("Appearance")]
      public Image Image
      {
        get => this._image;
        set
        {
          this._image = value;
          this.CreateLayout();
        }
      }

      [Browsable(false)]
      public INavBarRenderer Renderer => this._renderer;

      public override string Text
      {
        get => base.Text;
        set
        {
          base.Text = value;
          this.CreateLayout();
        }
      }
    }
}
