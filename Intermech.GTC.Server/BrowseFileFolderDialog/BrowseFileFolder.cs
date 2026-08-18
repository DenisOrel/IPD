// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.BrowseFileFolderDialog.BrowseFileFolder
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.BrowseFileFolderDialog;

public class BrowseFileFolder : LongLifeObject, IBrowseFileFolder
{
  public IFileFolderInfo[] GetChildrenItems(IFileFolderInfo parentItem, string fileSearchPattern)
  {
    if (parentItem.ItemType == FileFolderEnum.Root)
      return ((IEnumerable<DriveInfo>) DriveInfo.GetDrives()).Where<DriveInfo>((Func<DriveInfo, bool>) (y => y.DriveType == DriveType.Fixed)).Select<DriveInfo, IFileFolderInfo>((Func<DriveInfo, IFileFolderInfo>) (x => (IFileFolderInfo) new FileFolderInfo(FileFolderEnum.Drive, x.Name, x.Name, IconHolder.GetIcon(FileFolderEnum.Drive, x.Name)))).ToArray<IFileFolderInfo>();
    if (parentItem.ItemType != FileFolderEnum.Drive && parentItem.ItemType != FileFolderEnum.Folder)
      return new IFileFolderInfo[0];
    try
    {
      return ((IEnumerable<string>) Directory.GetDirectories(parentItem.FullPath)).Select<string, DirectoryInfo>((Func<string, DirectoryInfo>) (x => new DirectoryInfo(x))).Where<DirectoryInfo>((Func<DirectoryInfo, bool>) (y => !y.Attributes.HasFlag((Enum) FileAttributes.Hidden))).Select<DirectoryInfo, IFileFolderInfo>((Func<DirectoryInfo, IFileFolderInfo>) (d => (IFileFolderInfo) new FileFolderInfo(FileFolderEnum.Folder, d.Name, d.FullName, IconHolder.GetIcon(FileFolderEnum.Folder, d.FullName)))).Concat<IFileFolderInfo>(((IEnumerable<FileInfo>) new DirectoryInfo(parentItem.FullPath).GetFiles(fileSearchPattern)).Where<FileInfo>((Func<FileInfo, bool>) (f => !f.Attributes.HasFlag((Enum) FileAttributes.Hidden))).Select<FileInfo, IFileFolderInfo>((Func<FileInfo, IFileFolderInfo>) (f => (IFileFolderInfo) new FileFolderInfo(FileFolderEnum.File, f.Name, f.FullName, IconHolder.GetIcon(FileFolderEnum.File, f.FullName))))).ToArray<IFileFolderInfo>();
    }
    catch
    {
      return new IFileFolderInfo[0];
    }
  }

  public IFileFolderInfo DataSource
  {
    get
    {
      return (IFileFolderInfo) new FileFolderInfo(FileFolderEnum.Root, "Компьютер", string.Empty, (Icon) null);
    }
  }
}
