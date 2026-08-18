
// Type: Intermech.Interfaces.ImageHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    public class ImageHelper
    {
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern bool DestroyIcon(IntPtr handle);

      /// <summary>Получить Icon по Bitmap</summary>
      /// <param name="bmp"></param>
      /// <returns></returns>
      public static Icon BitmapToIcon(Bitmap bmp)
      {
        Icon icon;
        using (Icon original = Icon.FromHandle(bmp.GetHicon()))
        {
          icon = new Icon(original, original.Width, original.Height);
          ImageHelper.DestroyIcon(original.Handle);
        }
        return icon;
      }
    }
}
