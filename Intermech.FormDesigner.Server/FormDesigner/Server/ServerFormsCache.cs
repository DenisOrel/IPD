// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.ServerFormsCache
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.IO;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.IO;

#nullable disable
namespace Intermech.FormDesigner.Server;

[Serializable]
public class ServerFormsCache : LongLifeObject, IServerFormsCache
{
  private ConcurrentDictionary<long, byte[]> _dict = new ConcurrentDictionary<long, byte[]>();

  public ServerFormsCache(IEventLogHelper eventLog)
  {
    if (eventLog == null)
      return;
    eventLog.AfterCacheReload += new CacheReloadHandler(this.OnEventLogHelper_AfterCacheReload);
  }

  public byte[] GetForm(Guid sessionGuid, long formID)
  {
    byte[] form = (byte[]) null;
    if (!this._dict.TryGetValue(formID, out form))
    {
      IUserSession session = this.GetSession(sessionGuid);
      IDBAttribute attributeByGuid = (session.GetObject(formID, false) ?? throw new Exception(LocalizationHolder.rm.GetString("FormDesigner_Server_GetForm_Null"))).GetAttributeByGuid(new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Server_GetAttribute_Null"), (object) "cad0011d-306c-11d8-b4e9-00304f19f545", (object) formID.ToString()));
      if (!attributeByGuid.IsNull)
      {
        using (ImChunkedStream aDestStream = new ImChunkedStream())
        {
          BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader.ReadData(session);
          if (blobProcReader.Result)
          {
            if (aDestStream.Length > 0L)
            {
              form = aDestStream.ToArray();
              this._dict.TryAdd(formID, form);
            }
          }
        }
      }
    }
    return form;
  }

  public void CheckIn(long formID)
  {
    formID = Math.Abs(formID);
    byte[] bytes = (byte[]) null;
    if (this._dict.TryGetValue(-formID, out bytes))
    {
      this._dict.AddOrUpdate(formID, bytes, (Func<long, byte[], byte[]>) ((k, v) => bytes));
      formID = -formID;
    }
    this.Remove(formID);
  }

  public void CheckOut(long formID)
  {
    formID = Math.Abs(formID);
    byte[] numArray = (byte[]) null;
    if (!this._dict.TryGetValue(formID, out numArray))
      return;
    this._dict.TryAdd(-formID, numArray);
  }

  public void UndoCheckOut(long formID) => this.Remove(-Math.Abs(formID));

  public void Save(long formID, byte[] bytes)
  {
    if (bytes == null || bytes.Length == 0)
      return;
    this._dict.AddOrUpdate(formID, bytes, (Func<long, byte[], byte[]>) ((k, v) => bytes));
  }

  public void Remove(long formID)
  {
    byte[] numArray = (byte[]) null;
    this._dict.TryRemove(formID, out numArray);
  }

  public void Clear() => this._dict.Clear();

  private void OnEventLogHelper_AfterCacheReload(IDbManager db) => this.Clear();

  private IUserSession GetSession(Guid sessionGuid)
  {
    return UserSession.GetSessionByID(sessionGuid) ?? throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Server_GetSession_Error"), (object) sessionGuid.ToString()), "SessionGuid");
  }
}
