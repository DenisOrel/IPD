
// Type: SuperTooltips.ShadowHelper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace SuperTooltips
{
    internal class ShadowHelper : Control
    {
      private bool _alphaShadow;
      private static Color[] _shadowColors = new Color[5]
      {
        Color.FromArgb(14, Color.Black),
        Color.FromArgb(43, Color.Black),
        Color.FromArgb(84, Color.Black),
        Color.FromArgb(113, Color.Black),
        Color.FromArgb(128 /*0x80*/, Color.Black)
      };

      public ShadowHelper(bool bAlphaShadow)
      {
        this._alphaShadow = bAlphaShadow;
        this.Visible = false;
        this.SuspendLayout();
        if (!this._alphaShadow)
          this.BackColor = SystemColors.ControlDark;
        this.Size = new Size(12, 12);
        this.SetStyle(ControlStyles.ContainerControl, false);
        this.SetStyle(ControlStyles.Selectable, false);
        if (this._alphaShadow)
        {
          this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
          this.SetStyle(ControlStyles.DoubleBuffer, false);
        }
        this.ResumeLayout();
      }

      private void PaintShadow()
      {
        Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
          Region region = new Region();
          GraphicsPath path = new GraphicsPath();
          Rectangle clientRectangle = this.ClientRectangle;
          path.StartFigure();
          path.AddLine(clientRectangle.Right - 5, clientRectangle.Top, clientRectangle.Right, clientRectangle.Top);
          path.AddLine(clientRectangle.Right, clientRectangle.Top, clientRectangle.Right, clientRectangle.Top + 5);
          path.AddLine(clientRectangle.Right, clientRectangle.Top + 5, clientRectangle.Right - 5, clientRectangle.Top);
          path.CloseFigure();
          path.StartFigure();
          path.AddLine(clientRectangle.Left, clientRectangle.Bottom - 5 - 1, clientRectangle.Left, clientRectangle.Bottom);
          path.AddLine(clientRectangle.Left, clientRectangle.Bottom, clientRectangle.Left + 5, clientRectangle.Bottom);
          path.AddLine(clientRectangle.Left + 5, clientRectangle.Bottom, clientRectangle.Left, clientRectangle.Bottom - 5);
          path.CloseFigure();
          path.StartFigure();
          path.AddRectangle(new Rectangle(0, 0, clientRectangle.Width - 5, clientRectangle.Height - 5));
          path.CloseFigure();
          region.Xor(path);
          graphics.SetClip(region, CombineMode.Replace);
          graphics.Clear(Color.Transparent);
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          graphics.Clear(Color.Black);
          --clientRectangle.Width;
          --clientRectangle.Height;
          using (Pen pen = new Pen(Color.Black, 1f))
          {
            for (int index = 0; index < 4; ++index)
            {
              pen.Color = ShadowHelper._shadowColors[index];
              graphics.DrawPath(pen, ShadowHelper.CreatePath(clientRectangle, 4 - index));
              clientRectangle.Inflate(-1, -1);
            }
          }
          IntPtr dc = Win32API.GetDC(IntPtr.Zero);
          IntPtr compatibleDc = Win32API.CreateCompatibleDC(dc);
          IntPtr hbitmap = bitmap.GetHbitmap(Color.FromArgb(0));
          IntPtr a53T = Win32API.SelectObject(compatibleDc, hbitmap);
          Win32API.SIZE a54C;
          a54C.cx = this.Width;
          a54C.cy = this.Height;
          Win32API.POINT a54B;
          a54B.x = this.Left;
          a54B.y = this.Top;
          Win32API.POINT a54E;
          a54E.x = 0;
          a54E.y = 0;
          Win32API.UpdateLayeredWindow(this.Handle, dc, ref a54B, ref a54C, compatibleDc, ref a54E, 0, ref new Win32API.BLENDFUNCTION()
          {
            BlendOp = (byte) 0,
            BlendFlags = (byte) 0,
            SourceConstantAlpha = byte.MaxValue,
            AlphaFormat = (byte) 1
          }, 2);
          Win32API.SelectObject(compatibleDc, a53T);
          Win32API.ReleaseDC(IntPtr.Zero, dc);
          Win32API.DeleteObject(hbitmap);
          Win32API.DeleteDC(compatibleDc);
        }
      }

      public void M04Y()
      {
        if (!this._alphaShadow)
          return;
        this.PaintShadow();
      }

      private static GraphicsPath CreatePath(Rectangle bounds, int offset)
      {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(bounds.Right - offset, bounds.Y, offset, offset, 270f, 90f);
        path.AddLine(bounds.Right, bounds.Y + offset, bounds.Right, bounds.Bottom - offset);
        path.AddArc(bounds.Right - offset, bounds.Bottom - offset, offset, offset, 0.0f, 90f);
        path.AddLine(bounds.Right - offset, bounds.Bottom, bounds.X, bounds.Bottom);
        path.AddLine(bounds.X, bounds.Bottom, bounds.X, bounds.Top);
        return path;
      }

      protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);

      protected override void OnPaint(PaintEventArgs e)
      {
        if (this._alphaShadow)
          return;
        e.Graphics.Clear(this.BackColor);
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 33)
          m.Result = new IntPtr(3);
        else
          base.WndProc(ref m);
      }

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          createParams.ExStyle = createParams.ExStyle | 8 | (this._alphaShadow ? 524288 /*0x080000*/ : 0) | 128 /*0x80*/;
          createParams.Style = -2046820352 /*0x86000000*/;
          createParams.Caption = string.Empty;
          return createParams;
        }
      }
    }
}
