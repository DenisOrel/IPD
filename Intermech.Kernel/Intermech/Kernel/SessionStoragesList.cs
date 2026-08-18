// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SessionStoragesList
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System.Collections.Generic;


namespace Intermech.Kernel;

internal class SessionStoragesList
{
  private List<IBlobStorage> _StoragesList = new List<IBlobStorage>(1);

  private UserSession Session { get; set; }

  public SessionStoragesList(UserSession session) => this.Session = session;

  public IBlobStorage GetStorage(long storageID)
  {
    IBlobStorage storage = (IBlobStorage) null;
    for (int index = 0; index < this._StoragesList.Count; ++index)
    {
      if (this._StoragesList[index].StorageID == storageID)
      {
        storage = this._StoragesList[index];
        storage.Lock();
        break;
      }
    }
    return storage;
  }

  public bool RealeseStorage(long storageID)
  {
    bool flag = false;
    if (!this.Session.InTransaction)
    {
      for (int index = 0; index < this._StoragesList.Count; ++index)
      {
        if (this._StoragesList[index].StorageID == storageID)
        {
          this._StoragesList.RemoveAt(index);
          flag = true;
          break;
        }
      }
    }
    return flag;
  }

  public void RealeseUnlockedStorages()
  {
    for (int index = this._StoragesList.Count - 1; index >= 0; --index)
    {
      if (!this._StoragesList[index].Locked)
        this._StoragesList.RemoveAt(index);
    }
  }

  public void Add(IBlobStorage storage) => this._StoragesList.Add(storage);

  public void Commit()
  {
    foreach (IBlobStorage storages in this._StoragesList)
      storages.Commit();
    this.RealeseUnlockedStorages();
  }

  public void Rollback()
  {
    foreach (IBlobStorage storages in this._StoragesList)
      storages.Rollback();
    this.RealeseUnlockedStorages();
  }

  public void StartTransaction()
  {
    foreach (IBlobStorage storages in this._StoragesList)
      storages.StartTransaction();
  }
}
