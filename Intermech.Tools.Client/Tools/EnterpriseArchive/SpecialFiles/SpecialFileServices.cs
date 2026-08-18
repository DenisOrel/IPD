// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.SpecialFiles.SpecialFileServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Settings;
using System;
using System.IO;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.SpecialFiles;

internal static class SpecialFileServices
{
  internal const string SpecialFileFolder = "__ips__";

  internal static string LocateServiceDirectory(bool createIfNotFound)
  {
    string path = Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, "__ips__");
    if (!Directory.Exists(path) & createIfNotFound)
    {
      Directory.CreateDirectory(path);
      File.SetAttributes(path, FileAttributes.Hidden | FileAttributes.Directory);
    }
    return path;
  }

  internal static Stream OpenFile(string filePath, FileShare shareMode)
  {
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    int num = 5;
    while (true)
    {
      try
      {
        return (Stream) new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, shareMode);
      }
      catch
      {
        --num;
        if (num <= 0)
          throw;
        Thread.Sleep(100);
      }
    }
  }

  internal static void FillEmptyFile(Stream st, string emptyFileContent)
  {
    if (st == null)
      throw new ArgumentNullException(nameof (st));
    if (emptyFileContent == null)
      throw new ArgumentNullException(nameof (emptyFileContent));
    if (st.Length != 0L)
      return;
    StreamWriter streamWriter = new StreamWriter(st, Encoding.UTF8);
    streamWriter.WriteLine(emptyFileContent);
    streamWriter.Flush();
    st.Seek(0L, SeekOrigin.Begin);
  }
}
