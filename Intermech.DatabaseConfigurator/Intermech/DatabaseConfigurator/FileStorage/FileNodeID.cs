// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileNodeID
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileNodeID : INodeID, IFileID
{
  private long _fileID;
  private long _fileSize;
  private object _cookie;

  public FileNodeID(long fileID, long fileSize)
  {
    this._fileID = fileID;
    this._fileSize = fileSize;
    this._cookie = (object) null;
  }

  public long FileID => this._fileID;

  public long FileZipSize => this._fileSize;

  int INodeID.CategoryID => 15;

  int INodeID.TypeID => 0;

  object INodeID.Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  public long Value => this._fileID;

  public override bool Equals(object obj)
  {
    return obj is FileNodeID fileNodeId && this._fileID == fileNodeId._fileID;
  }

  public override int GetHashCode() => this._fileID.GetHashCode();
}
