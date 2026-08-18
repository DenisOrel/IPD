
// Type: Intermech.Client.Core.ThumbnailDocs.NXPreviewExrtact
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Runtime.ComInterop;
using Intermech.Search.UI;
using OpenMcdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// 
/// </summary>
public class NXPreviewExrtact : IPreviewExtract
{
  private string[] _supportedExtension = new string[1]
  {
    ".prt"
  };

  public bool Supports(string filename)
  {
    return ((IEnumerable<string>) this._supportedExtension).Contains<string>(Path.GetExtension(filename)?.ToLower());
  }

  /// <summary>
  /// В NX11 файлы это файлы формата OleStorage, превью в них хранится в потоке "preview" в хранилище "images" в jpeg формате
  /// В NX 12 это файлы формата NX12File, превью в них хранится в секции /prewiew в jfif формате
  /// </summary>
  /// <param name="filename"></param>
  /// <param name="image"></param>
  /// <returns></returns>
  public PreviewExtractStatus ExtractPreview(string filename, out Image image)
  {
    image = (Image) null;
    if (!this.Supports(filename))
      return PreviewExtractStatus.NotSupported;
    if (StgServices.StgIsStorageFile(filename) == 0)
    {
      try
      {
        using (CompoundFile compoundFile = new CompoundFile(filename, CFSUpdateMode.ReadOnly, CFSConfiguration.Default))
        {
          byte[] data = (byte[]) null;
          CFStorage storage = compoundFile.RootStorage.TryGetStorage("images");
          if ((CFItem) storage == (CFItem) null)
            return PreviewExtractStatus.NotFound;
          storage.VisitEntries((Action<CFItem>) (x =>
          {
            if (!x.IsStream || !(x.Name == "preview"))
              return;
            data = ((CFStream) x).GetData();
          }), true);
          if (data == null)
            return PreviewExtractStatus.NotFound;
          image = ImageHelper.GetImageFromBuffer(data);
          return PreviewExtractStatus.OK;
        }
      }
      catch
      {
      }
    }
    else
    {
      if (!NX12File.IsNX12File(filename))
        return PreviewExtractStatus.NotSupported;
      try
      {
        using (NX12File nx12File = new NX12File(filename))
        {
          string sectionName = nx12File.FooterSections.FirstOrDefault<string>((Func<string, bool>) (x => x.Contains("preview")));
          if (string.IsNullOrEmpty(sectionName))
            sectionName = nx12File.HeaderSections.FirstOrDefault<string>((Func<string, bool>) (x => x.Contains("preview")));
          if (string.IsNullOrEmpty(sectionName))
            return PreviewExtractStatus.NotFound;
          byte[] sectionData = nx12File.GetSectionData(sectionName);
          if (sectionData == null)
            return PreviewExtractStatus.NotFound;
          image = ImageHelper.GetImageFromBuffer(sectionData);
          return PreviewExtractStatus.OK;
        }
      }
      catch
      {
      }
    }
    return PreviewExtractStatus.NotFound;
  }

  public string GetSupportExtensions() => string.Join(",", this._supportedExtension);
}
