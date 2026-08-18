
// Type: Intermech.Client.Core.ThumbnailDocs.KompasPreviewExtract
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using Intermech.Search.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// Извлечение превью из файлов Kompas 18.
/// Файлы компаса представляют собой zip архив, в котором может быть файл Preview.
/// В этом файле со смещением 0x12 лежит tiff
/// </summary>
public class KompasPreviewExtract : IPreviewExtract
{
  private string[] _supportedExtension = new string[4]
  {
    ".a3d",
    ".m3d",
    ".cdw",
    ".frw"
  };

  public bool Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  public PreviewExtractStatus ExtractPreview(string filename, out Image image)
  {
    image = (Image) null;
    try
    {
      using (ZipArchive zipArchive = ZipFile.OpenRead(filename))
      {
        ZipArchiveEntry zipArchiveEntry = zipArchive.Entries.FirstOrDefault<ZipArchiveEntry>((Func<ZipArchiveEntry, bool>) (x => x.FullName.Equals("Preview", StringComparison.OrdinalIgnoreCase)));
        if (zipArchiveEntry != null)
        {
          using (Stream stream = zipArchiveEntry.Open())
          {
            using (ImChunkedStream destination = new ImChunkedStream())
            {
              stream.CopyTo((Stream) destination);
              byte[] buffer = new byte[4];
              destination.Position = 18L;
              destination.Read(buffer, 0, 4);
              if (buffer[0] == (byte) 77)
              {
                if (buffer[1] == (byte) 77)
                {
                  if (buffer[2] == (byte) 0)
                  {
                    if (buffer[3] == (byte) 42)
                    {
                      byte[] numArray = new byte[destination.Length - 18L];
                      destination.Position = 18L;
                      destination.Read(numArray, 0, numArray.Length);
                      image = ImageHelper.GetImageFromBuffer(numArray);
                      return PreviewExtractStatus.OK;
                    }
                  }
                }
              }
            }
            return PreviewExtractStatus.NotSupported;
          }
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
