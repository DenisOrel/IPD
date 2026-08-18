
// Type: Intermech.Client.Core.ThumbnailDocs.InventorPreviewExrtact
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


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// У Inventor файлы в формате OleStorage, превью хранится в стандартном атрибуте с идентификатором = 0x11,
/// этот атрибут может быть в потоке SummaryInformation (в старых версиях) или же в потоке InventorSummaryInformation
/// внутри атрибута может хранится превью в формате png (начиная с 12го байта, для более новых версий)
/// или же в формате DIB (device indeoendent bitmap, структура BITMAPINFOHEADER) начиная с 0х48 байта (для очень(?) старых версий)
/// мы будем обрабатывать только png
/// </summary>
public class InventorPreviewExrtact : IPreviewExtract
{
  private string[] _supportedExtension = new string[5]
  {
    ".ipt",
    ".iam",
    ".idw",
    ".ipn",
    ".ide"
  };

  public bool Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  public PreviewExtractStatus ExtractPreview(string filename, out Image image)
  {
    image = (Image) null;
    if (!this.Supports(filename))
      return PreviewExtractStatus.NotSupported;
    try
    {
      StructuredProperty structuredProperty = new StructuredStorage(filename, new Guid("{3D38DE39-0588-4C14-BB37-18F4D5DD31C7}")).Properties.SingleOrDefault<StructuredProperty>((Func<StructuredProperty, bool>) (x => x.Id == 17)) ?? new StructuredStorage(filename).Properties.SingleOrDefault<StructuredProperty>((Func<StructuredProperty, bool>) (x => x.Id == 17));
      if (structuredProperty != null)
      {
        if (structuredProperty.Value != null)
        {
          byte[] sourceArray = (byte[]) structuredProperty.Value;
          if (sourceArray[13] == (byte) 80 /*0x50*/)
          {
            if (sourceArray[14] == (byte) 78)
            {
              if (sourceArray[15] == (byte) 71)
              {
                byte[] numArray = new byte[sourceArray.Length - 12];
                Array.Copy((Array) sourceArray, 12, (Array) numArray, 0, sourceArray.Length - 12);
                image = ImageHelper.GetImageFromBuffer(numArray);
                return PreviewExtractStatus.OK;
              }
            }
          }
        }
      }
    }
    catch
    {
    }
    return PreviewExtractStatus.NotFound;
  }

  public string GetSupportExtensions() => string.Join(",", this._supportedExtension);
}
