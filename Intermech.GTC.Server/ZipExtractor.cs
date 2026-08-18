// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.ZipExtractor
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

#nullable disable
namespace Intermech.GTC.Server;

public class ZipExtractor
{
  public static string ExtractRootItem(string sourcePath, out bool rootIsZipFile)
  {
    if (sourcePath == null)
      throw new ArgumentNullException(nameof (sourcePath));
    rootIsZipFile = false;
    if (!File.Exists(sourcePath) || !(Path.GetExtension(sourcePath) == ".zip"))
      return sourcePath;
    string targetDirectory = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(sourcePath));
    new FastZip() { CreateEmptyDirectories = true }.ExtractZip(sourcePath, targetDirectory, (string) null);
    rootIsZipFile = true;
    return targetDirectory;
  }

  public static string ExtractFile(string sourcePath)
  {
    string str = !(Path.GetExtension(sourcePath) != ".zip") ? sourcePath : sourcePath + ".zip";
    if (!File.Exists(str))
      return sourcePath;
    string directoryName = Path.GetDirectoryName(str);
    new FastZip() { CreateEmptyDirectories = true }.ExtractZip(str, directoryName, (string) null);
    string path = str.Remove(str.Length - 4);
    return !File.Exists(path) ? string.Empty : path;
  }
}
