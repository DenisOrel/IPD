// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.FileFolderItem
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

#nullable disable
namespace Intermech.GTC.Server;

public class FileFolderItem
{
  public FileFolderItem(
    FileFolder enumType,
    string value,
    PackageType type,
    bool canContainItemFile = false)
  {
    this.EnumType = enumType;
    this.Value = value;
    this.Type = type;
    this.CanContainItemFile = canContainItemFile;
  }

  public FileFolder EnumType { get; private set; }

  public string Value { get; private set; }

  public PackageType Type { get; private set; }

  public bool CanContainItemFile { get; private set; }
}
