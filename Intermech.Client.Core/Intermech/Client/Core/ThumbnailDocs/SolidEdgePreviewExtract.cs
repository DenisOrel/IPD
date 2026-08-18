
// Type: Intermech.Client.Core.ThumbnailDocs.SolidEdgePreviewExtract
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// SolidEdge файлы формата OleStorage, превью хранится в свойстве с идентификатором 0хА в SolidEdgeSummaryInformation
/// для групповых моделей поток SolidEdgeSummaryInformation будет внутри хранилища "Master\u0001"
/// (а также внутри хранилищ с именами как у исполнений)
/// Формат превью - DIB (device indeoendent bitmap, структура BITMAPINFOHEADER)
/// </summary>
public class SolidEdgePreviewExtract : IPreviewExtract
{
  private string[] _supportedExtension = new string[5]
  {
    ".asm",
    ".par",
    ".psm",
    ".dft",
    ".pwd"
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
      StructuredProperty structuredProperty = new StructuredStorage(filename, new Guid("{CC024FA2-6EB5-11CE-8AA2-08003601E988}")).Properties.SingleOrDefault<StructuredProperty>((Func<StructuredProperty, bool>) (x => x.Id == 10)) ?? new StructuredStorage(filename, new Guid("{CC024FA2-6EB5-11CE-8AA2-08003601E988}"), "Master\u0001").Properties.SingleOrDefault<StructuredProperty>((Func<StructuredProperty, bool>) (x => x.Id == 10));
      if (structuredProperty != null)
      {
        if (structuredProperty.Value != null)
        {
          using (Bitmap bitmap = DIBFuncs.CF_DIBV5ToBitmap((byte[]) structuredProperty.Value))
          {
            image = (Image) Image.FromHbitmap(bitmap.GetHbitmap());
            return PreviewExtractStatus.OK;
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
