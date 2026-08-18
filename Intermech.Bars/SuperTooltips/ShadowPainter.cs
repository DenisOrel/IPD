
// Type: SuperTooltips.ShadowPainter
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Drawing.Drawing2D;


namespace SuperTooltips
{
    public class ShadowPainter
    {
      private static Color[] pathColors = new Color[6]
      {
        Color.FromArgb(7, Color.Black),
        Color.FromArgb(14, Color.Black),
        Color.FromArgb(43, Color.Black),
        Color.FromArgb(84, Color.Black),
        Color.FromArgb(113, Color.Black),
        Color.FromArgb(128 /*0x80*/, Color.Black)
      };

      private static GraphicsPath CreatePath(Rectangle bounds)
      {
        GraphicsPath path = new GraphicsPath();
        path.AddLine(bounds.Left + 1, bounds.Y, bounds.Right - 1, bounds.Y);
        path.AddLine(bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Y + 1);
        path.AddLine(bounds.Right - 1, bounds.Y + 1, bounds.Right, bounds.Y + 1);
        path.AddLine(bounds.Right, bounds.Y + 1, bounds.Right, bounds.Bottom - 1);
        path.AddLine(bounds.Right, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
        path.AddLine(bounds.Right - 1, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom);
        path.AddLine(bounds.Right - 1, bounds.Bottom, bounds.Left + 1, bounds.Bottom);
        path.AddLine(bounds.Left + 1, bounds.Bottom, bounds.Left + 1, bounds.Bottom - 1);
        path.AddLine(bounds.Left + 1, bounds.Bottom - 1, bounds.Left, bounds.Bottom - 1);
        path.AddLine(bounds.Left, bounds.Bottom - 1, bounds.Left, bounds.Top + 1);
        return path;
      }

      public static void Paint(ShadowPaintInfo info)
      {
        Graphics graphics = info.Graphics;
        Region clip = graphics.Clip;
        graphics.SetClip(info.Bounds, CombineMode.Exclude);
        Rectangle bounds = info.Bounds;
        --bounds.Width;
        --bounds.Height;
        int num1 = info.Size;
        if (num1 > ShadowPainter.pathColors.Length - 1)
          num1 = ShadowPainter.pathColors.Length;
        int num2 = num1 / 2;
        bounds.Offset(num2, num2);
        bounds.Width += num1 - num2;
        bounds.Height += num1 - num2;
        using (Pen pen = new Pen(Color.White, 1f))
        {
          for (int index = 0; index < num1; ++index)
          {
            pen.Color = ShadowPainter.pathColors[index];
            graphics.DrawPath(pen, ShadowPainter.CreatePath(bounds));
            bounds.Inflate(-1, -1);
          }
        }
        graphics.Clip = clip;
      }
    }
}
