// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ImageFuncs
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Workflow.Design;

public class ImageFuncs
{
  /// Returns a transparent background GIF image from the specified Bitmap.
  ///             The Bitmap to make transparent.
  ///             The Color to make transparent.
  ///             New Bitmap containing a transparent background gif.
  public static Bitmap MakeTransparentGif(Bitmap bitmap, Color color)
  {
    byte r = color.R;
    byte g = color.G;
    byte b = color.B;
    MemoryStream memoryStream1 = new MemoryStream();
    bitmap.Save((Stream) memoryStream1, ImageFormat.Gif);
    MemoryStream memoryStream2 = new MemoryStream((int) memoryStream1.Length);
    byte[] buffer = new byte[256 /*0x0100*/];
    byte num1 = 0;
    memoryStream1.Seek(0L, SeekOrigin.Begin);
    int count = memoryStream1.Read(buffer, 0, 13);
    if (buffer[0] != (byte) 71 || buffer[1] != (byte) 73 || buffer[2] != (byte) 70)
      return (Bitmap) null;
    memoryStream2.Write(buffer, 0, 13);
    int num2 = 0;
    if (((int) buffer[10] & 128 /*0x80*/) > 0)
      num2 = 1 << ((int) buffer[10] & 7) + 1 == 256 /*0x0100*/ ? 256 /*0x0100*/ : 0;
    for (; num2 != 0; --num2)
    {
      memoryStream1.Read(buffer, 0, 3);
      if ((int) buffer[0] == (int) r && (int) buffer[1] == (int) g && (int) buffer[2] == (int) b)
        num1 = (byte) (256 /*0x0100*/ - num2);
      memoryStream2.Write(buffer, 0, 3);
    }
label_9:
    memoryStream1.Read(buffer, 0, 1);
    memoryStream2.Write(buffer, 0, 1);
    if (buffer[0] == (byte) 33)
    {
      memoryStream1.Read(buffer, 0, 1);
      memoryStream2.Write(buffer, 0, 1);
      bool flag = buffer[0] == (byte) 249;
      while (true)
      {
        memoryStream1.Read(buffer, 0, 1);
        memoryStream2.Write(buffer, 0, 1);
        if (buffer[0] != (byte) 0)
        {
          count = (int) buffer[0];
          if (memoryStream1.Read(buffer, 0, count) == count)
          {
            if (flag && count == 4)
            {
              buffer[0] |= (byte) 1;
              buffer[3] = num1;
            }
            memoryStream2.Write(buffer, 0, count);
          }
          else
            break;
        }
        else
          goto label_9;
      }
      return (Bitmap) null;
    }
    while (count > 0)
    {
      count = memoryStream1.Read(buffer, 0, 1);
      memoryStream2.Write(buffer, 0, 1);
    }
    memoryStream1.Close();
    memoryStream2.Flush();
    return new Bitmap((Stream) memoryStream2);
  }

  public static string AddTransparentPNGSupport(
    Assembly ResourceAssembly,
    string OutDir,
    string Html)
  {
    string[] files = new string[2]
    {
      "html/blank.gif",
      "html/iepngfix.htc"
    };
    ResourceFuncs.ExtractResourceFiles(ResourceAssembly, files, OutDir);
    return new Regex("<style[^>]*>", RegexOptions.Compiled | RegexOptions.Singleline).Replace(Html, "$0\r\nimg, div { behavior: url(iepngfix.htc)}\r\n");
  }
}
