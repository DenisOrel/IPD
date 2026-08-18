// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.BrowseFolderService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections;
using System.IO;


namespace Intermech.Kernel.Services;

public class BrowseFolderService : LongLifeObject, IBrowseFolder
{
  private static string driveLetters = "CDEFGHIJKLMNOPQRSTUVWXYZ";
  private static string[] _drives;

  public string[] GetFolders(string parentPath)
  {
    ArrayList arrayList = new ArrayList(32 /*0x20*/);
    DirectoryInfo directoryInfo1 = new DirectoryInfo(parentPath);
    DirectoryInfo[] directoryInfoArray = new DirectoryInfo[0];
    try
    {
      directoryInfoArray = directoryInfo1.GetDirectories();
    }
    catch (Exception ex)
    {
    }
    foreach (DirectoryInfo directoryInfo2 in directoryInfoArray)
    {
      if ((directoryInfo2.Attributes & FileAttributes.Hidden) == (FileAttributes) 0)
        arrayList.Add((object) directoryInfo2.Name);
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public long GetFreeSpace(string folderPath)
  {
    try
    {
      return new DriveInfo(folderPath.Substring(0, 1)).TotalFreeSpace;
    }
    catch
    {
      return 0;
    }
  }

  internal static string[] CreateDrivesList()
  {
    ArrayList arrayList = new ArrayList(BrowseFolderService.driveLetters.Length / 2);
    foreach (char driveLetter in BrowseFolderService.driveLetters)
    {
      string path = driveLetter.ToString() + ":\\";
      try
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Exists)
          arrayList.Add((object) directoryInfo.FullName);
      }
      catch (Exception ex)
      {
      }
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public string[] DrivesList
  {
    get
    {
      if (BrowseFolderService._drives == null)
        BrowseFolderService._drives = BrowseFolderService.CreateDrivesList();
      return BrowseFolderService._drives;
    }
  }

  public void RefreshDrivesList()
  {
    BrowseFolderService._drives = BrowseFolderService.CreateDrivesList();
  }

  public void CreateFolder(string parentPath, string name)
  {
    if (string.IsNullOrEmpty(parentPath))
      throw new ArgumentNullException(nameof (parentPath));
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    DirectoryInfo directoryInfo = new DirectoryInfo(parentPath);
    if (!directoryInfo.Exists)
      throw new Exception("Указанный путь не существует.");
    string lowerName = name.ToLower();
    if (Array.Exists<DirectoryInfo>(directoryInfo.GetDirectories(), (Predicate<DirectoryInfo>) (x => x.Name.ToLower().Equals(lowerName))) || Array.Exists<FileInfo>(directoryInfo.GetFiles(), (Predicate<FileInfo>) (x => x.Name.ToLower().Equals(lowerName))))
      throw new Exception($"По указанному пути уже существует папка или файл с именем {name}");
    Directory.CreateDirectory(Path.Combine(parentPath, name));
  }
}
