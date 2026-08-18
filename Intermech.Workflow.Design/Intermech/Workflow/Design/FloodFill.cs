// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.FloodFill
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Fills a bitmap using a non-recursive flood-fill.</summary>
public class FloodFill
{
  private static Stack stack = new Stack();

  /// <summary>Checks to make sure a pixel is in an image.</summary>
  /// <param name="pos">The position to check</param>
  /// <param name="bmd">The BitmapData from which the bounds are determined</param>
  /// <returns>True if the point is in the image</returns>
  private static bool CheckPixel(Point pos, BitmapData bmd)
  {
    return pos.X > -1 && pos.Y > -1 && pos.X < bmd.Width && pos.Y < bmd.Height;
  }

  /// <summary>Returns the color at a specific pixel</summary>
  /// <param name="pos">The position of the pixel</param>
  /// <param name="bmd">The locked bitmap data</param>
  /// <returns>The color of the pixel under the nominated point</returns>
  private static Color GetPixel(Point pos, BitmapData bmd)
  {
    if (!FloodFill.CheckPixel(pos, bmd))
      return Color.FromArgb(0, 0, 0, 0);
    int ofs = pos.Y * bmd.Stride + 4 * pos.X;
    return Color.FromArgb((int) Marshal.ReadByte(bmd.Scan0, ofs + 2), (int) Marshal.ReadByte(bmd.Scan0, ofs + 1), (int) Marshal.ReadByte(bmd.Scan0, ofs));
  }

  /// <summary>
  /// Sets a pixel at a nominated point to a specified color
  /// </summary>
  /// <param name="pos">The coordinate of the pixel to set</param>
  /// <param name="bmd">The locked bitmap data</param>
  /// <param name="c">The color to set</param>
  private static void SetPixel(Point pos, BitmapData bmd, Color c)
  {
    if (!FloodFill.CheckPixel(pos, bmd))
      return;
    int ofs = pos.Y * bmd.Stride + 4 * pos.X;
    Marshal.WriteByte(bmd.Scan0, ofs + 2, c.R);
    Marshal.WriteByte(bmd.Scan0, ofs + 1, c.G);
    Marshal.WriteByte(bmd.Scan0, ofs, c.B);
    Marshal.WriteByte(bmd.Scan0, ofs + 3, byte.MaxValue);
  }

  /// <summary>
  /// Fills a pixel and its un-filled neigbors with a specified color
  /// </summary>
  /// <param name="pos">The position at which to begin</param>
  /// <param name="bmd">The locked bitmap data</param>
  /// <param name="c">The color with which to fill the area</param>
  /// <param name="org">The original colour of the point. Filling stops when all connected pixels of this color are exhausted</param>
  private static void FillPixel(Point pos, BitmapData bmd, Color c, Color org)
  {
    Point point = new Point(0, 0);
    FloodFill.stack.Push((object) pos);
    do
    {
      Point pos1 = (Point) FloodFill.stack.Pop();
      FloodFill.SetPixel(pos1, bmd, c);
      if (FloodFill.GetPixel(new Point(pos1.X + 1, pos1.Y), bmd) == org)
        FloodFill.stack.Push((object) new Point(pos1.X + 1, pos1.Y));
      if (FloodFill.GetPixel(new Point(pos1.X, pos1.Y - 1), bmd) == org)
        FloodFill.stack.Push((object) new Point(pos1.X, pos1.Y - 1));
      if (FloodFill.GetPixel(new Point(pos1.X - 1, pos1.Y), bmd) == org)
        FloodFill.stack.Push((object) new Point(pos1.X - 1, pos1.Y));
      if (FloodFill.GetPixel(new Point(pos1.X, pos1.Y + 1), bmd) == org)
        FloodFill.stack.Push((object) new Point(pos1.X, pos1.Y + 1));
    }
    while (FloodFill.stack.Count > 0);
  }

  /// <summary>Fills a bitmap with color.</summary>
  /// <remarks>If a non 32-bit image is passed to this routine and only 32 bit image will be created, the original image will be copied to the new image and filling will take place on the new image which will be handed back when complete. </remarks>
  /// <param name="img">The image to fill</param>
  /// <param name="pos">The position to begin filling at</param>
  /// <param name="color">The color to fill</param>
  /// <returns>A Bitmap object with the filled area.</returns>
  public static Bitmap Fill(Image img, Point pos, Color color)
  {
    Bitmap bitmap = (Bitmap) img;
    if (img.PixelFormat != PixelFormat.Format32bppArgb)
    {
      bitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
      graphics.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
      graphics.Dispose();
    }
    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
    Color pixel = FloodFill.GetPixel(pos, bitmapData);
    FloodFill.FillPixel(pos, bitmapData, color, pixel);
    bitmap.UnlockBits(bitmapData);
    return bitmap;
  }
}
