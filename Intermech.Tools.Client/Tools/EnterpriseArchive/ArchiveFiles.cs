// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ArchiveFiles
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal static class ArchiveFiles
{
  public static IEnumerable<string> SafeEnumerateFiles(string directoryPath)
  {
    if (string.IsNullOrEmpty(directoryPath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(directoryPath))
      throw new ArgumentException();
    IEnumerable<string> strings;
    try
    {
      strings = Directory.EnumerateFiles(directoryPath, "*", SearchOption.TopDirectoryOnly);
    }
    catch (UnauthorizedAccessException ex)
    {
      strings = (IEnumerable<string>) null;
    }
    if (strings != null)
    {
      foreach (string str in strings)
        yield return str;
      foreach (string enumerateDirectory in Directory.EnumerateDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly))
      {
        if ((File.GetAttributes(enumerateDirectory) & FileAttributes.Hidden) == (FileAttributes) 0)
        {
          foreach (string safeEnumerateFile in ArchiveFiles.SafeEnumerateFiles(enumerateDirectory))
            yield return safeEnumerateFile;
        }
      }
    }
  }
}
