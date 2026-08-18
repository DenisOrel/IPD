// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.GetStorageIDEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class GetStorageIDEventArgs : EventArgs
{
  private long _StorageID;

  public long ActiveStorageID { get; private set; }

  public IDBObject ParentObject { get; private set; }

  public IUserSession Session { get; private set; }

  public GetStorageIDEventArgs(IDBObject parentObject, IUserSession session, long activeStorageID)
  {
    this.ParentObject = parentObject;
    this.Session = session;
    this.ActiveStorageID = activeStorageID;
  }

  public long StorageID
  {
    get => this._StorageID;
    set => this._StorageID = value;
  }
}
