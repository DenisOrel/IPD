// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

public class ServerCache : MarshalByRefObject, IServerCache
{
  private UserSession _Session;

  public ServerCache(UserSession session) => this._Session = session;

  public string[] GetModifiedTables(DateTime modifyDate)
  {
    modifyDate -= this._Session.TimeZoneOffset;
    Hashtable tablesModifyTime = (this._Session.DBCache as CacheDataset).tablesModifyTime;
    ICollection keys = tablesModifyTime.Keys;
    ArrayList arrayList = new ArrayList();
    foreach (string key in (IEnumerable) keys)
    {
      object obj = tablesModifyTime[(object) key];
      if (obj != null && Convert.ToDateTime(obj) > modifyDate)
        arrayList.Add((object) key);
    }
    return arrayList.Count > 0 ? (string[]) arrayList.ToArray(typeof (string)) : new string[0];
  }

  public void Reload()
  {
    (ServerServices.GetService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadCache(this._Session.SessionGUID);
  }

  public DateTime LastMetadataModify
  {
    get => this._Session.DBCache.ModifyDate + this._Session.TimeZoneOffset;
  }

  public DataTable[] GetTables(params string[] tableNames)
  {
    Hashtable tablesModifyTime = (this._Session.DBCache as CacheDataset).tablesModifyTime;
    Hashtable hashtable = new Hashtable();
    foreach (DataRow row in (InternalDataCollectionBase) this.GetTablesModifyTime().Rows)
      hashtable.Add((object) Convert.ToString(row["F_TABLE_NAME"]), (object) Convert.ToDateTime(row["F_MODIFY_DATE"], (IFormatProvider) CultureInfo.InvariantCulture));
    DataTable[] tables = new DataTable[tableNames.Length];
    if (tableNames.Length == 0)
      return (DataTable[]) null;
    for (int index = 0; index < tableNames.Length; ++index)
    {
      if (tablesModifyTime.ContainsKey((object) tableNames[index]) && hashtable.ContainsKey((object) tableNames[index]) && (DateTime) tablesModifyTime[(object) tableNames[index]] != (DateTime) hashtable[(object) tableNames[index]])
        (this._Session.DBCache as CacheDataset).ReloadTables((IUserSession) this._Session, this._Session.DataManager, new string[1]
        {
          tableNames[index]
        });
      tables[index] = this._Session.DBCache.GetTable(tableNames[index]);
    }
    return tables;
  }

  public DataTable GetTablesModifyTime()
  {
    return this._Session.DataManager.ExecuteDataTable("SELECT * FROM IMS_METADATA");
  }

  public string[] GetTableNames() => (this._Session.DBCache as CacheDataset).TablesNameList;

  public long[] GetFilePrototypes(int objectTypeID)
  {
    long[] numArray1 = (long[]) null;
    object filePrototype1 = (this._Session.DBCache as CacheDataset)._FilePrototypes[(object) new FilePrototypeID(this._Session.IdentHelper.FileAttributeID, objectTypeID, 0L)];
    if (filePrototype1 != null)
      numArray1 = (long[]) filePrototype1;
    object filePrototype2 = (this._Session.DBCache as CacheDataset)._FilePrototypes[(object) new FilePrototypeID(this._Session.IdentHelper.FileAttributeID, objectTypeID, this._Session.UserID)];
    if (filePrototype2 != null)
    {
      if (numArray1 == null)
      {
        numArray1 = (long[]) filePrototype2;
      }
      else
      {
        long[] numArray2 = (long[]) filePrototype2;
        long[] numArray3 = numArray1;
        numArray1 = new long[numArray2.Length + numArray3.Length];
        numArray2.CopyTo((Array) numArray1, 0);
        numArray3.CopyTo((Array) numArray1, numArray2.Length);
      }
    }
    return numArray1 ?? new long[0];
  }

  public DataTable GetConfigurations()
  {
    return this._Session.DataManager.ExecuteDataTable("SELECT * FROM IMS_CONFIGS WHERE F_USER_ID IN (0, :usrID)", this._Session.DataManager.Parameter("usrID", (object) this._Session.UserID));
  }

  public Tuple<long, Guid, string>[] GetUsersCache() => this._Session.DBCache.GetUsersCache();
}
