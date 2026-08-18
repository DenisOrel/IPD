
// Type: Intermech.Bars.Utils
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Drawing.Imaging;


namespace Intermech.Bars
{
    public class Utils
    {
      internal static Image MakeTransparent(Image source)
      {
        if (source == null || !(source is Bitmap) || !(source.RawFormat.Guid != ImageFormat.Icon.Guid))
          return source;
        Bitmap bitmap = new Bitmap(source);
        Color pixel = bitmap.GetPixel(0, bitmap.Height - 1);
        bitmap.MakeTransparent(pixel);
        return (Image) bitmap;
      }
    }
}
