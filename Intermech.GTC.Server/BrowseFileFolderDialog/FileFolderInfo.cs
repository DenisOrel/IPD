// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.BrowseFileFolderDialog.FileFolderInfo
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.GTC.Server.BrowseFileFolderDialog;

public class FileFolderInfo : MarshalByRefObject, IFileFolderInfo
{
  public FileFolderEnum ItemType { get; private set; }

  public string Name { get; private set; }

  public string FullPath { get; private set; }

  public Icon Image { get; private set; }

  public FileFolderInfo(FileFolderEnum itemType, string name, string fullPath, Icon image)
  {
    this.ItemType = itemType;
    this.Name = name;
    this.FullPath = fullPath;
    this.Image = image;
  }
}
