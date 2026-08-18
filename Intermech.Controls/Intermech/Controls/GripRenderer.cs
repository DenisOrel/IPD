
// Type: Intermech.Controls.GripRenderer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public static class GripRenderer
{
  private static void InitializeGripBitmap([NotNull] Graphics g, Size size, bool forceRefresh)
  {
    if (!(GripRenderer.GripBitmap == null | forceRefresh) && !(size != GripRenderer.GripBitmap.Size))
      return;
    GripRenderer.GripBitmap = new Bitmap(size.Width, size.Height, g);
    using (Graphics graphics = Graphics.FromImage((Image) GripRenderer.GripBitmap))
      ControlPaint.DrawSizeGrip(graphics, SystemColors.ButtonFace, 0, 0, size.Width, size.Height);
  }

  public static void RefreshSystemColors([NotNull] Graphics g, Size size)
  {
    GripRenderer.InitializeGripBitmap(g, size, true);
  }

  public static void Render([NotNull] Graphics g, Point location, Size size, GripAlignMode mode)
  {
    GripRenderer.InitializeGripBitmap(g, size, false);
    switch (mode)
    {
      case GripAlignMode.TopLeft:
        size.Height = -size.Height;
        size.Width = -size.Width;
        break;
      case GripAlignMode.TopRight:
        size.Height = -size.Height;
        break;
      case GripAlignMode.BottomLeft:
        size.Width = -size.Height;
        break;
    }
    if (size.Width < 0)
      location.X -= size.Width;
    if (size.Height < 0)
      location.Y -= size.Height;
    if (GripRenderer.GripBitmap == null)
      return;
    g.DrawImage((Image) GripRenderer.GripBitmap, location.X, location.Y, size.Width, size.Height);
  }

  public static void Render([NotNull] Graphics g, Point location, GripAlignMode mode)
  {
    GripRenderer.Render(g, location, new Size(16 /*0x10*/, 16 /*0x10*/), mode);
  }

  [CanBeNull]
  private static Bitmap GripBitmap { get; set; }
}
