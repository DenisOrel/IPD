
// Type: Intermech.Client.Core.ThumbnailDocs.PicturePreviewExtract
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

internal class PicturePreviewExtract : IPreviewExtract
{
  private string[] _supportedExtension = new string[7]
  {
    ".bmp",
    ".gif",
    ".jpg",
    ".png",
    ".tif",
    ".jpeg",
    ".tiff"
  };

  bool IPreviewExtract.Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  PreviewExtractStatus IPreviewExtract.ExtractPreview(string filename, out Image image)
  {
    PreviewExtractStatus extractPreview = PreviewExtractStatus.OK;
    image = (Image) null;
    try
    {
      using (Image image1 = Image.FromFile(filename))
      {
        int width = image1.Width;
        int height = image1.Height;
        image = image1.GetThumbnailImage(300, 300, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
      }
    }
    catch
    {
      extractPreview = PreviewExtractStatus.NotSupported;
    }
    return extractPreview;
  }

  public string GetSupportExtensions() => string.Join(",", this._supportedExtension);
}
