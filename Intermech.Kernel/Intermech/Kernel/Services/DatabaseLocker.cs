// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DatabaseLocker
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel.Services;

public class DatabaseLocker : LongLifeObject, IDatabaseLocker
{
  public DatabaseLockInfo Lock(IUserSession session, string methodName, TimeSpan maxDuration)
  {
    IDbManager dataManager = (session as UserSession).DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_LOCKER WHERE F_METHOD_NAME = :metName", dataManager.Parameter("metName", (object) methodName));
    DatabaseLockInfo databaseLockInfo;
    if (dataTable.Rows.Count > 0)
    {
      DateTime dateTime = Convert.ToDateTime(dataTable.Rows[0]["F_DATE"]);
      if (dateTime + maxDuration > DateTime.UtcNow)
      {
        databaseLockInfo = new DatabaseLockInfo(dataTable.Rows[0]["F_USER_NAME"].ToString(), dataTable.Rows[0]["F_COMPUTER_NAME"].ToString(), dateTime + session.TimeZoneOffset);
      }
      else
      {
        dataManager.ExecuteNonQuery($"UPDATE IMS_LOCKER SET F_DATE = {dataManager.DataProvider.Now}, F_COMPUTER_NAME = :cmpName, F_USER_NAME = :usrName WHERE F_METHOD_NAME = :metName", dataManager.Parameter("cmpName", (object) session.ComputerName), dataManager.Parameter("usrName", (object) session.UserName), dataManager.Parameter("metName", (object) methodName));
        databaseLockInfo = new DatabaseLockInfo();
      }
    }
    else
    {
      dataManager.ExecuteNonQuery($"INSERT INTO IMS_LOCKER (F_DATE, F_COMPUTER_NAME, F_USER_NAME, F_METHOD_NAME) VALUES ({dataManager.DataProvider.Now}, :cmpName, :usrName, :metName)", dataManager.Parameter("cmpName", (object) session.ComputerName), dataManager.Parameter("usrName", (object) session.UserName), dataManager.Parameter("metName", (object) methodName));
      databaseLockInfo = new DatabaseLockInfo();
    }
    return databaseLockInfo;
  }

  public void UnLock(IUserSession session, string methodName)
  {
    IDbManager dataManager = (session as UserSession).DataManager;
    dataManager.ExecuteNonQuery("DELETE FROM IMS_LOCKER WHERE F_METHOD_NAME = :metName", dataManager.Parameter("metName", (object) methodName));
  }

  public void UnLockAll(IUserSession session)
  {
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_13785.ssp_appserver_13786(726460281));
    (session as UserSession).DataManager.ExecuteNonQuery("DELETE FROM IMS_LOCKER");
  }
}
