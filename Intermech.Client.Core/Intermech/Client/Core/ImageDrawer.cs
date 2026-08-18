
// Type: Intermech.Client.Core.ImageDrawer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using System.Drawing;


namespace Intermech.Client.Core;

public static class ImageDrawer
{
  public static void DrawImageObject(
    Graphics g,
    object image,
    Rectangle imageBounds,
    Font font,
    StringFormat imageStringFormat)
  {
    ThumbnailRenderer.DrawImageObject(g, image, imageBounds, font, imageStringFormat);
  }
}
