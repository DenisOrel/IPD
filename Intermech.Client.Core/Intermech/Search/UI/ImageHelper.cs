
// Type: Intermech.Search.UI.ImageHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.IO;


namespace Intermech.Search.UI;

internal static class ImageHelper
{
  public static Image GetImage(string imageName)
  {
    if (string.IsNullOrEmpty(imageName))
      throw new ArgumentNullException(nameof (imageName));
    string name = $"Intermech.Client.Core.Intermech.Search.UI.Images.{imageName}";
    return Image.FromStream(typeof (ImageHelper).Assembly.GetManifestResourceStream(name) ?? throw new Exception($"Неудается найти ресурс {name}"));
  }

  public static Image GetImageFromBuffer(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
      memoryStream.Position = 0L;
      using (Image original = Image.FromStream((Stream) memoryStream))
        return (Image) new Bitmap(original);
    }
  }
}
