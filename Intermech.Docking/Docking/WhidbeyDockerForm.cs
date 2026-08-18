
// Type: Intermech.Docking.WhidbeyDockerForm
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class WhidbeyDockerForm : Form
{
  public WhidbeyDockerForm() => this.FormBorderStyle = FormBorderStyle.None;

  public void Update(Bitmap bitmap, byte alpha)
  {
    IntPtr dc = Win32.GetDC(IntPtr.Zero);
    IntPtr compatibleDc = Win32.CreateCompatibleDC(dc);
    IntPtr num = IntPtr.Zero;
    IntPtr A_1 = IntPtr.Zero;
    try
    {
      num = bitmap.GetHbitmap(Color.FromArgb(0));
      A_1 = Win32.SelectObject(compatibleDc, num);
      Win32.SIZE A_3 = new Win32.SIZE(bitmap.Width, bitmap.Height);
      Win32.POINT A_5 = new Win32.POINT(0, 0);
      Win32.POINT A_2 = new Win32.POINT(this.Left, this.Top);
      Win32.UpdateLayeredWindow(this.Handle, dc, ref A_2, ref A_3, compatibleDc, ref A_5, 0, ref new Win32.BLENDFUNCTION()
      {
        _blendOp = (byte) 0,
        _blendFlags = (byte) 0,
        _sourceConstantAlpha = alpha,
        _alphaFormat = (byte) 1
      }, 2);
    }
    finally
    {
      if (num != IntPtr.Zero)
      {
        Win32.SelectObject(compatibleDc, A_1);
        Win32.DeleteObject(num);
      }
      Win32.ReleaseDC(IntPtr.Zero, dc);
      Win32.DeleteDC(compatibleDc);
    }
  }

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.ExStyle |= 524288 /*0x080000*/;
      return createParams;
    }
  }
}
