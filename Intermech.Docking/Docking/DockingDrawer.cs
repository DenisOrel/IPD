
// Type: Intermech.Docking.DockingDrawer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class DockingDrawer
{
  private static IntPtr CreateHalftoneHBRUSH()
  {
    short[] A_4 = new short[8];
    for (int index = 0; index < 8; ++index)
      A_4[index] = (short) (21845 /*0x5555*/ << (index & 1));
    IntPtr bitmap = Win32.CreateBitmap(8, 8, 1, 1, A_4);
    IntPtr brushIndirect = Win32.CreateBrushIndirect(new Win32.LOGBRUSH()
    {
      _color = ColorTranslator.ToWin32(Color.Black),
      _style = 3,
      _hatch = bitmap
    });
    Win32.DeleteObject(bitmap);
    return brushIndirect;
  }

  public static void DrawReversibleHatchedRectangle(Control surface, Rectangle bounds)
  {
    IntPtr zero = IntPtr.Zero;
    if (bounds == Rectangle.Empty)
      return;
    IntPtr num = surface != null ? surface.Handle : IntPtr.Zero;
    IntPtr dc = Win32.GetDC(num);
    IntPtr halftoneHbrush = DockingDrawer.CreateHalftoneHBRUSH();
    IntPtr A_1 = Win32.SelectObject(dc, halftoneHbrush);
    Win32.PatBlt(new HandleRef((object) surface, dc), bounds.X, bounds.Y, bounds.Width, bounds.Height, 5898313);
    Win32.SelectObject(dc, A_1);
    Win32.DeleteObject(halftoneHbrush);
    Win32.ReleaseDC(num, dc);
  }

  public static void DrawReversibleHollowRectangle(
    Control surface,
    Rectangle bounds,
    bool drawTab,
    int offset)
  {
    DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X, bounds.Y, bounds.Width, 4));
    if (drawTab)
    {
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X, bounds.Y + 4, 4, bounds.Height - 4 - offset));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.Right - 4, bounds.Y + 4, 4, bounds.Height - 4 - offset));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X, bounds.Bottom - offset, 10, 4));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X + 80 /*0x50*/, bounds.Bottom - offset, bounds.Width - 80 /*0x50*/, 4));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X + 10, bounds.Bottom - 4, 70, 4));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X + 10, bounds.Bottom - offset, 4, offset - 4));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X + 76, bounds.Bottom - offset, 4, offset - 4));
    }
    else
    {
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X, bounds.Y + 4, 4, bounds.Height - 8));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.Right - 4, bounds.Y + 4, 4, bounds.Height - 8));
      DockingDrawer.DrawReversibleHatchedRectangle(surface, new Rectangle(bounds.X, bounds.Bottom - 4, bounds.Width, 4));
    }
  }
}
