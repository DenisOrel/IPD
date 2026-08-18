// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.FileFolderPathHolder
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server;

public class FileFolderPathHolder
{
  private FileFolderItem[] _data = new FileFolderItem[0];
  private string _rootPath;

  private string[] GetFileFoldersInternal(FileFolder enumType, PackageType packageType)
  {
    return ((IEnumerable<FileFolderItem>) this._data).Where<FileFolderItem>((Func<FileFolderItem, bool>) (x => x.EnumType.Equals((object) enumType) && x.Type.Equals((object) packageType))).Select<FileFolderItem, string>((Func<FileFolderItem, string>) (x => !x.EnumType.Equals((object) FileFolder.File) ? Path.Combine(this._rootPath, x.Value) : ZipExtractor.ExtractFile(Path.Combine(this._rootPath, x.Value)))).ToArray<string>();
  }

  public void SetRoot(string rootPath) => this._rootPath = rootPath;

  public void SetData(FileFolderItem[] data) => this._data = data;

  public string[] GetHierarchyMandatoryFolders()
  {
    return this.GetFileFoldersInternal(FileFolder.Folder, PackageType.Hierarchy);
  }

  public string[] GetHierarchyMandatoryFiles()
  {
    return this.GetFileFoldersInternal(FileFolder.File, PackageType.Hierarchy);
  }

  public string[] GetDataMandatoryFolders()
  {
    return this.GetFileFoldersInternal(FileFolder.Folder, PackageType.Data);
  }

  public string[] GetDataMandatoryFiles()
  {
    return this.GetFileFoldersInternal(FileFolder.File, PackageType.Data);
  }

  public string[] GetSearchPaths()
  {
    return ((IEnumerable<FileFolderItem>) this._data).Where<FileFolderItem>((Func<FileFolderItem, bool>) (x => x.CanContainItemFile)).Select<FileFolderItem, string>((Func<FileFolderItem, string>) (x => Path.Combine(this._rootPath, x.Value))).ToArray<string>();
  }

  public string GetItemByName(string itemName)
  {
    FileFolderItem fileFolderItem = ((IEnumerable<FileFolderItem>) this._data).FirstOrDefault<FileFolderItem>((Func<FileFolderItem, bool>) (x => x.Value.Equals(itemName)));
    return fileFolderItem == null ? string.Empty : ZipExtractor.ExtractFile(Path.Combine(this._rootPath, fileFolderItem.Value));
  }
}
