// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.AppServers
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System.Threading;


namespace Intermech.Kernel.Services;

internal class AppServers : IAppServers
{
  private readonly IDbManager _DBManager;
  private string _ThisServerName;
  private DateTime _LastUpdateTime;
  private ConcurrentDictionary<string, DateTime> _Servers = new ConcurrentDictionary<string, DateTime>();
  private Timer _Timer;
  private TimerCallback _TimerDelegate;

  public AppServers(IDbManager db)
  {
    this._DBManager = db;
    this.SetThisServerName();
    this.UpdateData(this._DBManager);
    this.FillAliveServers(this._DBManager);
    this._TimerDelegate = new TimerCallback(this.DoUpdateServers);
    this._Timer = new Timer(this._TimerDelegate, (object) null, TimeSpan.FromHours((double) ServerConsts.ServerAliveUpdatePeriod), TimeSpan.FromHours((double) ServerConsts.ServerAliveUpdatePeriod));
  }

  private void SetThisServerName()
  {
    this._ThisServerName = $"{EnvironmentConsts.MachineName}:{ServerConsts.RemotingServerPort}";
  }

  private void UpdateData(IDbManager db)
  {
    object obj = db.ExecuteScalar("SELECT F_SERVER_NAME FROM IMS_SERVERS WHERE F_SERVER_NAME = :srvName", db.Parameter("srvName", (object) this._ThisServerName));
    if (obj != null && obj != DBNull.Value)
      db.ExecuteNonQuery($"UPDATE IMS_SERVERS SET F_DATE = {db.DataProvider.Now} WHERE F_SERVER_NAME = :srvName", db.Parameter("srvName", (object) this._ThisServerName));
    else
      db.ExecuteNonQuery($"INSERT INTO IMS_SERVERS (F_SERVER_NAME, F_DATE) VALUES (:srvName, {db.DataProvider.Now})", db.Parameter("srvName", (object) this._ThisServerName));
    this._LastUpdateTime = DateTime.UtcNow;
  }

  private void FillAliveServers(IDbManager db)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_SERVERS");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string key = dataTable.Rows[index]["F_SERVER_NAME"].ToString();
      if (key != this._ThisServerName)
      {
        DateTime dateTime1 = Convert.ToDateTime(dataTable.Rows[index]["F_DATE"]);
        DateTime dateTime2;
        if (this._Servers.TryGetValue(key, out dateTime2))
        {
          if (dateTime1 > dateTime2)
            this._Servers[key] = dateTime1;
        }
        else if (dateTime1 > DateTime.UtcNow - TimeSpan.FromHours((double) ServerConsts.ServerDeadPeriod))
          this._Servers.TryAdd(key, dateTime1);
      }
    }
  }

  private void DoUpdateServers(object state)
  {
    try
    {
      this.UpdateData(this._DBManager);
      this.FillAliveServers(this._DBManager);
    }
    catch (Exception ex)
    {
      IEventLogHelper service = ServiceUtils.GetService<IEventLogHelper>((object) ApplicationServices.Container, false);
      if (service == null)
        return;
      service.AddToTrace(LocalizationHolder.rm.GetString("Kernel_1174") + ex.Message, Consts.traceAlways, string.Empty);
      service.AddToTrace(ex.StackTrace, Consts.traceAlways, string.Empty);
    }
  }

  public string[] GetAliveServers() => this._Servers.Keys.ToArray<string>();

  public string ServerName => this._ThisServerName;

  public void DeleteDeadServers(IDbManager db)
  {
    bool flag = false;
    DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_SERVERS");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string str = dataTable.Rows[index]["F_SERVER_NAME"].ToString();
      if (str != this._ThisServerName && Convert.ToDateTime(dataTable.Rows[index]["F_DATE"]) < DateTime.UtcNow - TimeSpan.FromHours((double) ServerConsts.ServerDeadPeriod))
      {
        db.ExecuteNonQuery(sc_14198.ssp_appserver_14199(), db.Parameter("srvName", (object) str));
        db.ExecuteNonQuery("DELETE FROM IMS_ISB WHERE F_SERVER_DST = :srvName", db.Parameter("srvName", (object) str));
        flag = true;
      }
    }
    if (!flag)
      return;
    this.FillAliveServers(db);
  }

  public int ServersCount => this._Servers.Count;
}
