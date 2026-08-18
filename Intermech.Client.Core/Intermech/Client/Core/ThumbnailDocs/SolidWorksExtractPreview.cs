
// Type: Intermech.Client.Core.ThumbnailDocs.SolidWorksExtractPreview
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using Intermech.Runtime.ComInterop;
using Intermech.Search.UI;
using OpenMcdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// SolidWorks хранит превьюшки в двух потоках - "PreviewPNG" (версии по новее, формат png)
/// и "Preview" (старее, формат - DIB (device indeoendent bitmap, структура BITMAPINFOHEADER)
/// До версии SW2015 файлы были в формате OleStorage, после стали похожими на zip архив, но с какими то хитрыми динамическими сигнатурами.
/// Мы тут обрабатываем только поток PreviewPNG, т.к. во всех версиях начиная с года так 2005 он уже есть
/// </summary>
public class SolidWorksExtractPreview : IPreviewExtract
{
  private string[] _supportedExtension = new string[3]
  {
    ".sldprt",
    ".sldasm",
    ".slddrw"
  };

  public bool Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  private PreviewExtractStatus ExtractPreview2015(string filename, out Image image)
  {
    byte[] numArray = new byte[14]
    {
      (byte) 10,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 5,
      (byte) 39,
      (byte) 86,
      (byte) 103,
      (byte) 150,
      (byte) 86,
      (byte) 119,
      (byte) 5,
      (byte) 228,
      (byte) 116
    };
    using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
    {
      int num = 0;
      int index = 0;
      while (num >= 0)
      {
        num = fileStream.ReadByte();
        if (num != -1)
        {
          if (num == (int) numArray[index])
            ++index;
          else if (index > 0)
          {
            fileStream.Seek((long) (-index + 1), SeekOrigin.Current);
            index = 0;
          }
          if (index == numArray.Length)
          {
            try
            {
              long position = fileStream.Position;
              byte[] buffer1 = new byte[4];
              fileStream.Seek((long) (-8 - index), SeekOrigin.Current);
              fileStream.Read(buffer1, 0, 4);
              int int32_1 = BitConverter.ToInt32(buffer1, 0);
              fileStream.Read(buffer1, 0, 4);
              int int32_2 = BitConverter.ToInt32(buffer1, 0);
              fileStream.Position = position;
              if (int32_1 > 0)
              {
                byte[] buffer2 = new byte[int32_1];
                fileStream.Read(buffer2, 0, int32_1);
                fileStream.Position = position;
                try
                {
                  using (MemoryStream memoryStream = new MemoryStream(buffer2))
                  {
                    memoryStream.Position = 0L;
                    using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Decompress))
                    {
                      using (ImChunkedStream destination = new ImChunkedStream())
                      {
                        deflateStream.CopyTo((Stream) destination);
                        if ((long) int32_2 == destination.Length)
                        {
                          destination.Position = 0L;
                          using (Image original = Image.FromStream((Stream) destination))
                            image = (Image) new Bitmap(original);
                          return PreviewExtractStatus.OK;
                        }
                      }
                    }
                  }
                }
                catch
                {
                }
              }
            }
            finally
            {
              index = 0;
            }
          }
        }
        else
          break;
      }
    }
    image = (Image) null;
    return PreviewExtractStatus.NotFound;
  }

  private PreviewExtractStatus ExtractPreviewCompoundFile(string filename, out Image image)
  {
    try
    {
      using (CompoundFile compoundFile = new CompoundFile(filename, CFSUpdateMode.ReadOnly, CFSConfiguration.Default))
      {
        CFStream stream = compoundFile.RootStorage.GetStream("PreviewPNG");
        image = ImageHelper.GetImageFromBuffer(stream.GetData());
        return PreviewExtractStatus.OK;
      }
    }
    catch
    {
    }
    image = (Image) null;
    return PreviewExtractStatus.NotFound;
  }

  public PreviewExtractStatus ExtractPreview(string filename, out Image image)
  {
    image = (Image) null;
    try
    {
      return !this.Supports(filename) ? PreviewExtractStatus.NotSupported : (StgServices.StgIsStorageFile(filename) == 0 ? this.ExtractPreviewCompoundFile(filename, out image) : this.ExtractPreview2015(filename, out image));
    }
    catch
    {
    }
    return PreviewExtractStatus.NotFound;
  }

  public string GetSupportExtensions() => string.Join(",", this._supportedExtension);
}
