
// Type: Intermech.Client.Core.ThumbnailDocs.CreoPreviewExtract
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// Creo файлы в текстово-бинарном формате, в начале файла идет текстовый заголовок (UGC), потом текстовое "содержание" (UGC_TOC)
/// в котором описаны подпотоки их размеры и смещение в файле относительно первого байта после заголовка
/// Превью хранится в жипеге в потоке с названием THMB_IMG_MAIN
/// </summary>
public class CreoPreviewExtract : IPreviewExtract
{
  private string[] _supportedExtension = new string[3]
  {
    ".drw",
    ".prt",
    ".asm"
  };

  public bool Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  internal static string CreoReadLine(Stream s)
  {
    StringBuilder stringBuilder = new StringBuilder(100);
    int num = s.ReadByte();
    byte[] bytes = new byte[1];
    for (; num != -1 && num != 10; num = s.ReadByte())
    {
      bytes[0] = (byte) num;
      string str = Encoding.ASCII.GetString(bytes);
      stringBuilder.Append(str);
    }
    return stringBuilder.ToString();
  }

  public PreviewExtractStatus ExtractPreview(string filename, out Image image)
  {
    image = (Image) null;
    try
    {
      using (FileStream s = new FileStream(filename, FileMode.Open, FileAccess.Read))
      {
        byte[] buffer = new byte[4];
        s.Read(buffer, 0, 4);
        if (buffer[0] != (byte) 35 || buffer[1] != (byte) 85 || buffer[2] != (byte) 71 || buffer[3] != (byte) 67)
          return PreviewExtractStatus.NotSupported;
        string str = CreoPreviewExtract.CreoReadLine((Stream) s);
        while (!str.StartsWith("#-END_OF_UGC_HEADER"))
          str = CreoPreviewExtract.CreoReadLine((Stream) s);
        long position = s.Position;
        while (!str.StartsWith("#END_OF_UGC"))
        {
          if (s.Position != s.Length)
          {
            str = CreoPreviewExtract.CreoReadLine((Stream) s);
            if (str.StartsWith("NEXT_TOC_ENTRY "))
            {
              long offset = (long) Convert.ToInt32(str.Split(' ')[1], 16 /*0x10*/) + position;
              s.Seek(offset, SeekOrigin.Begin);
            }
            else if (str.StartsWith("THMB_IMG_MAIN "))
            {
              string[] strArray = str.Split(' ');
              long offset = (long) Convert.ToInt32(strArray[1], 16 /*0x10*/) + position;
              int int32 = Convert.ToInt32(strArray[3], 16 /*0x10*/);
              s.Seek(offset, SeekOrigin.Begin);
              CreoPreviewExtract.CreoReadLine((Stream) s);
              byte[] numArray = new byte[int32];
              s.Read(numArray, 0, int32);
              image = ImageHelper.GetImageFromBuffer(numArray);
              return PreviewExtractStatus.OK;
            }
          }
          else
            break;
        }
      }
    }
    catch (Exception ex)
    {
    }
    return PreviewExtractStatus.NotFound;
  }

  public string GetSupportExtensions() => string.Join(",", this._supportedExtension);
}
