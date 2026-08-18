// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.TextMiningFileConverter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Diagnostics;
using System.IO;


namespace Intermech.Kernel.GlobalIndex;

public class TextMiningFileConverter : CustomFileConverter
{
  public static string[] _TextFilesExtensions = new string[6]
  {
    ".PDF",
    ".DOC",
    ".RTF",
    ".CHM",
    ".HTM",
    ".HTML"
  };

  public string MinetextFilename
  {
    get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converters", "minetext.exe");
  }

  public override string Caption
  {
    get => LocalizationHolder.rm.GetString(nameof (TextMiningFileConverter));
  }

  public override string[] SupportedFileExtensions => TextMiningFileConverter._TextFilesExtensions;

  public override string GetPlainText(IDBAttribute attribute)
  {
    IBlobReader blobReader = attribute as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.PackedFileSize == 0L)
        return string.Empty;
    }
    finally
    {
      blobReader.CloseBlob();
    }
    IAppServerFilesCache service = ServerServices.GetService(typeof (IAppServerFilesCache)) as IAppServerFilesCache;
    string isolatedFileName = (attribute as DBStorageAttribute)._FileStruct.GetIsolatedFileName(service.FStorage);
    string plainText = string.Empty;
    string tempFileName1 = Path.GetTempFileName();
    string tempFileName2 = Path.GetTempFileName();
    string path = tempFileName2 + Path.GetExtension(blobInformation.FileName);
    try
    {
      if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
      {
        using (FileStream inStream = new FileStream(isolatedFileName, FileMode.Open))
        {
          using (FileStream outStream = new FileStream(path, FileMode.Create))
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
        }
      }
      else
      {
        using (FileStream fileStream1 = new FileStream(isolatedFileName, FileMode.Open))
        {
          using (FileStream fileStream2 = new FileStream(path, FileMode.Create))
          {
            byte[] buffer = new byte[32768 /*0x8000*/];
            for (int count = fileStream1.Read(buffer, 0, buffer.Length); count > 0; count = fileStream1.Read(buffer, 0, buffer.Length))
            {
              fileStream2.Write(buffer, 0, count);
              if (count < buffer.Length)
                break;
            }
          }
        }
      }
      Process process = new Process();
      try
      {
        process.StartInfo.FileName = this.MinetextFilename;
        process.StartInfo.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converters");
        process.StartInfo.Arguments = $"\"{path}\" \"{tempFileName1}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        process.WaitForExit(60000);
      }
      finally
      {
        process.Close();
      }
    }
    finally
    {
      if (File.Exists(path))
        File.Delete(path);
      if (File.Exists(tempFileName2))
        File.Delete(tempFileName2);
      if (File.Exists(tempFileName1))
      {
        using (StreamReader streamReader = new StreamReader(tempFileName1))
          plainText = streamReader.ReadToEnd();
        File.Delete(tempFileName1);
      }
      foreach (string file in Directory.GetFiles(Path.GetDirectoryName(tempFileName2), "pdfbox*.*"))
        File.Delete(file);
    }
    return plainText;
  }
}
