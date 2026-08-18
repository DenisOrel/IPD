// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.TextFileConverter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.IO;


namespace Intermech.Kernel.GlobalIndex;

public class TextFileConverter : CustomFileConverter
{
  public static string[] _TextFilesExtensions = new string[2]
  {
    ".TXT",
    ".TEXT"
  };

  public override string Caption => LocalizationHolder.rm.GetString("TextFileConverterCaption");

  public override string[] SupportedFileExtensions => TextFileConverter._TextFilesExtensions;

  public override string GetPlainText(IDBAttribute attribute)
  {
    IBlobReader blobReader = attribute as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    if (blobInformation.PackedFileSize == 0L)
    {
      blobReader.CloseBlob();
      return string.Empty;
    }
    byte[] buffer = blobReader.ReadDataBlock();
    if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
    {
      using (MemoryStream inStream = new MemoryStream(buffer))
      {
        using (MemoryStream memoryStream = new MemoryStream(Convert.ToInt32(blobInformation.RealFileSize)))
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
          return this.ReadFromStream((Stream) memoryStream);
        }
      }
    }
    using (MemoryStream strm = new MemoryStream(buffer))
      return this.ReadFromStream((Stream) strm);
  }
}
