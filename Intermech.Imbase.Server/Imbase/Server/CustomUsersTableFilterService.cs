// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.CustomUsersTableFilterService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.Server;

public class CustomUsersTableFilterService : LongLifeObject, ICustomUsersTableFilterService
{
  private CustomUsersTableFilterSynchronizer _serversSynchronizer;
  private IDictionary<long, UserFilterData> _userFilterDataDict = (IDictionary<long, UserFilterData>) new ConcurrentDictionary<long, UserFilterData>();

  internal CustomUsersTableFilterService()
  {
    this._serversSynchronizer = new CustomUsersTableFilterSynchronizer((ICustomUsersTableFilterService) this);
    ApplicationServices.Container.GetService<IServerSynchronizersManager>().RegisterSynchronizer((IServerSynchronizer) this._serversSynchronizer);
    ApplicationServices.Container.GetService<IEventLogHelper>().AfterCacheReload += new CacheReloadHandler(this.eventLogHelper_AfterCacheReload);
  }

  public UserFilter GetUserFilter(Guid sessionGuid, Guid objGuid)
  {
    return this.GetUserFilterData(UserSession.GetSessionByID(sessionGuid)).GetUserFilter(objGuid);
  }

  public void SetUserFilter(Guid sessionGuid, Guid objGuid, UserFilter userFilter)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    UserFilterData userFilterData = this.GetUserFilterData((IUserSession) sessionById);
    if (userFilterData.GetObjectGuids().Count != 0)
    {
      List<Guid> list = SqlHelper.GetObjectInfoByGUIDs((ICollection<Guid>) userFilterData.GetObjectGuids(), sessionById.DataManager).AsEnumerable().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (row => new Guid(row["F_GUID"].ToString()))).ToList<Guid>();
      userFilterData.DeleteNonExisitingObjs(list);
    }
    userFilterData.SetUserFilter(objGuid, userFilter);
    this.SaveUserFilterData((IUserSession) sessionById, userFilterData);
    this._serversSynchronizer.AddEvent(sessionById.UserID.ToString(), sessionById.DataManager);
  }

  public void RemoveUserDataFromCache(string userIdStr)
  {
    long result;
    if (!long.TryParse(userIdStr, out result))
      return;
    this._userFilterDataDict.Remove(result);
  }

  private UserFilterData LoadUserFilterData(IUserSession session)
  {
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("IMBASE.CustomUsersTableFilter", out config_info, out config_file);
    UserFilterData userFilterData1;
    if (config_info.RealFileSize > 0L && config_file != null && config_file.Length != 0)
    {
      MemoryStream serializationStream = new MemoryStream(config_file);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        userFilterData1 = !(binaryFormatter.Deserialize((Stream) serializationStream) is UserFilterData userFilterData2) ? new UserFilterData() : userFilterData2;
      }
      catch (Exception ex)
      {
        userFilterData1 = new UserFilterData();
        if (ServerServices.GetService(typeof (IOutputView)) is IOutputView service)
        {
          service.WriteString("IMBASE", LocalizationHolder.rm.GetString("Imbase.Server_27"));
          service.WriteString("IMBASE", ex.Message);
        }
      }
    }
    else
      userFilterData1 = new UserFilterData();
    return userFilterData1;
  }

  private UserFilterData GetUserFilterData(IUserSession session)
  {
    long userId = session.UserID;
    UserFilterData userFilterData1;
    if (this._userFilterDataDict.TryGetValue(userId, out userFilterData1))
      return userFilterData1;
    UserFilterData userFilterData2 = this.LoadUserFilterData(session);
    this._userFilterDataDict.Add(userId, userFilterData2);
    return userFilterData2;
  }

  private void SaveUserFilterData(IUserSession session, UserFilterData userFilterData)
  {
    try
    {
      IDBConfigurations configurations = session?.Configurations;
      if (configurations == null || userFilterData == null)
        return;
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) userFilterData);
        byte[] array = serializationStream.ToArray();
        BlobInformation config_info = new BlobInformation((long) array.Length, (long) array.Length, DateTime.Now, "IMBASE.CustomUsersTableFilter", ArcMethods.NotPacked, string.Empty);
        configurations.WriteConfigData(config_info, array);
      }
    }
    catch (Exception ex)
    {
      if (!(ServerServices.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("IMBASE", LocalizationHolder.rm.GetString("Imbase.Server_28"));
      service.WriteString("IMBASE", ex.Message);
    }
  }

  private void eventLogHelper_AfterCacheReload(IDbManager db) => this._userFilterDataDict.Clear();
}
