
// Type: Intermech.Bars.ShadowForm
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using SuperTooltips;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class ShadowForm : Form
    {
      private bool _horizontal;
      private int _r;
      private int _g;
      private int _b;
      private bool _fadeLastCorner = true;
      private Bitmap _shadow;

      public ShadowForm(Color shadowColor, bool horizontal, bool fadeLastCorner)
      {
        this.ShowInTaskbar = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this._r = (int) shadowColor.R;
        this._g = (int) shadowColor.G;
        this._b = (int) shadowColor.B;
        this._horizontal = horizontal;
        this._fadeLastCorner = fadeLastCorner;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this._shadow != null)
          this._shadow.Dispose();
        base.Dispose(disposing);
      }

      private void ApplyShadow(Bitmap bitmap, byte alpha)
      {
        IntPtr dc = Win32API.GetDC(IntPtr.Zero);
        IntPtr compatibleDc = Win32API.CreateCompatibleDC(dc);
        IntPtr num = IntPtr.Zero;
        IntPtr a53T = IntPtr.Zero;
        try
        {
          num = bitmap.GetHbitmap(Color.FromArgb(0));
          a53T = Win32API.SelectObject(compatibleDc, num);
          Win32API.SIZE a54C = new Win32API.SIZE(bitmap.Width, bitmap.Height);
          Win32API.POINT a54E = new Win32API.POINT(0, 0);
          Win32API.POINT a54B = new Win32API.POINT(this.Left, this.Top);
          Win32API.UpdateLayeredWindow(this.Handle, dc, ref a54B, ref a54C, compatibleDc, ref a54E, 0, ref new Win32API.BLENDFUNCTION()
          {
            BlendOp = (byte) 0,
            BlendFlags = (byte) 0,
            SourceConstantAlpha = alpha,
            AlphaFormat = (byte) 1
          }, 2);
        }
        finally
        {
          if (num != IntPtr.Zero)
          {
            Win32API.SelectObject(compatibleDc, a53T);
            Win32API.DeleteObject(num);
          }
          Win32API.ReleaseDC(IntPtr.Zero, dc);
          Win32API.DeleteDC(compatibleDc);
        }
      }

      private void CreateShadow(Bitmap bitmap)
      {
        float num1 = 0.0f;
        int index1 = 0;
        float num2 = 0.0f;
        BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        byte[] numArray = new byte[bitmap.Width * bitmap.Height * 4];
        Marshal.Copy(bitmapdata.Scan0, numArray, 0, numArray.Length);
        for (int index2 = 0; index2 < bitmap.Width; ++index2)
        {
          int num3 = 0;
          while (true)
          {
            if (num3 >= bitmap.Height)
              goto label_11;
            goto label_4;
    label_1:
            num2 *= 0.8f;
            num1 *= num2;
    label_2:
            num1 *= 0.25f;
            numArray[index1 + 3] = (byte) ((double) num1 * (double) byte.MaxValue);
            ++num3;
            continue;
    label_4:
            index1 = (num3 * bitmap.Width + index2) * 4;
            numArray[index1 + 2] = (byte) this._r;
            numArray[index1 + 1] = (byte) this._g;
            numArray[index1] = (byte) this._b;
            if (!this._horizontal)
            {
              num1 = 1f - (float) index2 / (float) bitmap.Width;
              if (num3 <= 3)
              {
                float num4 = (float) (num3 + 1) / 4f * 0.8f;
                num1 *= num4;
              }
              if (this._fadeLastCorner && num3 > bitmap.Height - 5)
              {
                num2 = (float) (bitmap.Height - num3) / 4f;
                goto label_1;
              }
              goto label_2;
            }
            num1 = 1f - (float) num3 / (float) bitmap.Height;
            if (index2 <= 3)
            {
              float num5 = (float) (index2 + 1) / 4f * 0.8f;
              num1 *= num5;
              goto label_2;
            }
            goto label_2;
    label_11:
            if ((index1 | -2) == 0)
              goto label_1;
            break;
          }
        }
        Marshal.Copy(numArray, 0, bitmapdata.Scan0, numArray.Length);
        bitmap.UnlockBits(bitmapdata);
      }

      public void Locate(Rectangle bounds)
      {
        if (this._shadow != null)
          this._shadow.Dispose();
        this._shadow = new Bitmap(bounds.Width, bounds.Height);
        this.CreateShadow(this._shadow);
        Win32.SetWindowPos(this.Handle, 0, bounds.X, bounds.Y, bounds.Width, bounds.Height, 84);
        this.ApplyShadow(this._shadow, byte.MaxValue);
      }

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          createParams.Style = int.MinValue;
          createParams.ExStyle |= 524296 /*0x080008*/;
          return createParams;
        }
      }
    }
}
