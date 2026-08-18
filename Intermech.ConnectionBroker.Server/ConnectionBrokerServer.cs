// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.ConnectionBrokerServer
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Interfaces.ConnectionBroker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.ConnectionBroker;

public class ConnectionBrokerServer : MarshalByRefObject, IConnectionBroker
{
  private volatile ServerSelectionMode _SelectionMode;
  private int _DefaultStartDelay;
  private volatile int _PingSuccessInterval = 60;
  private volatile int _PingFailInterval = 60;
  private volatile int _ForcePingTimeout = 6;
  internal ConcurrentDictionary<int, AppServerInfo> Servers = new ConcurrentDictionary<int, AppServerInfo>();
  private AppServerPing[] _Pingers;
  private volatile bool _Initialized;
  internal IApplicationEventLogService _EventLog;
  internal WebMorda _httpInterface;

  public ConnectionBrokerServer(IApplicationEventLogService eventLog) => this._EventLog = eventLog;

  public override object InitializeLifetimeService() => (object) null;

  public bool Initialize()
  {
    string str = ConfigurationManager.AppSettings.Get("SelectMode");
    if (str != null && str != string.Empty)
    {
      switch (str.ToUpper())
      {
        case "PRIMARY":
        case "0":
          this._SelectionMode = ServerSelectionMode.PrimaryServer;
          break;
        case "BALANCE":
        case "1":
          this._SelectionMode = ServerSelectionMode.LoadBalancing;
          break;
        case "RANDOM":
        case "2":
          this._SelectionMode = ServerSelectionMode.Random;
          break;
        default:
          this._EventLog.DefaultLog.Write("В конфигурационном файле указано неверное значение SelectMode: " + str, EventLogItemType.Error);
          break;
      }
    }
    string s1 = ConfigurationManager.AppSettings.Get("StartTimeout");
    int result1;
    if (s1 != null && s1 != string.Empty && int.TryParse(s1, out result1))
      this._DefaultStartDelay = result1;
    string s2 = ConfigurationManager.AppSettings.Get("PingSuccessInterval");
    int result2;
    if (s2 != null && s2 != string.Empty && int.TryParse(s2, out result2))
      this._PingSuccessInterval = result2;
    string s3 = ConfigurationManager.AppSettings.Get("PingFailInterval");
    int result3;
    if (s3 != null && s3 != string.Empty && int.TryParse(s3, out result3))
      this._PingFailInterval = result3;
    string s4 = ConfigurationManager.AppSettings.Get("ForcePingTimeout");
    int result4;
    if (s4 != null && s4 != string.Empty && int.TryParse(s4, out result4))
      this._ForcePingTimeout = result4;
    AppServersConfigSection section = (AppServersConfigSection) ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Sections["AppServers"];
    if (section != null)
    {
      ServersCollection serverItems = section.ServerItems;
      for (int index = 0; index < serverItems.Count; ++index)
        this.Servers.TryAdd(index, new AppServerInfo(serverItems[index].ServerName, serverItems[index].URL));
      if (this.Servers.Count != 0)
        return true;
      this._EventLog.DefaultLog.Write("В конфигурационном файле не найден список серверов приложений.", EventLogItemType.Error);
      return false;
    }
    this._EventLog.DefaultLog.Write("В конфигурационном файле не найдена секция <AppServers> со списком серверов приложений.", EventLogItemType.Error);
    return false;
  }

  public void Run()
  {
    Thread.Sleep(this._DefaultStartDelay * 1000);
    this._Pingers = new AppServerPing[this.Servers.Count];
    for (int index = 0; index < this.Servers.Count; ++index)
    {
      AppServerInfo appServerInfo;
      if (this.Servers.TryGetValue(index, out appServerInfo))
      {
        AppServerPing appServerPing = new AppServerPing(this, index, appServerInfo.ServerName, appServerInfo.ServerURL);
        Thread thread = new Thread(new ParameterizedThreadStart(appServerPing.StartPing));
        this._Pingers[index] = appServerPing;
        thread.IsBackground = true;
        thread.Start();
        Thread.Sleep(500);
      }
    }
    this._Initialized = true;
    Thread.Sleep(this._ForcePingTimeout * 1000);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine();
    stringBuilder.AppendLine("Список серверов:");
    foreach (string serversOutput in this.GetServersOutputList())
      stringBuilder.AppendLine(serversOutput);
    this._EventLog.DefaultLog.Write(stringBuilder.ToString(), EventLogItemType.Information);
    bool flag = true;
    string str = ConfigurationManager.AppSettings.Get("HttpEnable");
    if (str != null && str != string.Empty && (str == "0" || str.ToLower() == "false"))
      flag = false;
    if (!flag)
      return;
    this._httpInterface = new WebMorda(this);
    if (this._httpInterface.StartListener())
      new Thread(new ParameterizedThreadStart(this._httpInterface.Listen))
      {
        IsBackground = true
      }.Start();
    else
      this._httpInterface = (WebMorda) null;
  }

  internal void UpdateServerInfo(int serverIndex, AppServerInfo info)
  {
    AppServerInfo appServerInfo;
    if (!this.Servers.TryGetValue(serverIndex, out appServerInfo) || appServerInfo.Equals((object) info))
      return;
    this.Servers[serverIndex] = info;
  }

  public void Close()
  {
    for (int index = 0; index < this._Pingers.Length; ++index)
      this._Pingers[index].InitForceExit();
  }

  public int PingSuccessInterval => this._PingSuccessInterval;

  public int PingFailInterval => this._PingFailInterval;

  public ServerSelectionMode SelectionMode => this._SelectionMode;

  public string[] GetServersOutputList()
  {
    List<string> stringList = new List<string>(this.Servers.Count);
    for (int key = 0; key < this.Servers.Count; ++key)
    {
      AppServerInfo appServerInfo;
      if (this.Servers.TryGetValue(key, out appServerInfo))
      {
        string str = string.Empty;
        switch (appServerInfo.Result)
        {
          case ServerPingResult.None:
            str = "Не опрошен";
            break;
          case ServerPingResult.Accessible:
            str = "Работает  |  База данных: " + appServerInfo.DatabaseConnectionString;
            if (this._SelectionMode == ServerSelectionMode.LoadBalancing)
            {
              str = $"{str}  |  Показатель загрузки: {appServerInfo.LoadState.ToString()}";
              break;
            }
            break;
          case ServerPingResult.NotAccessible:
            str = "Ошибка подключения: " + this._Pingers[key].LastError;
            break;
        }
        stringList.Add($"Сервер {appServerInfo.ServerName}  |  URL={appServerInfo.ServerURL}  |  {str}");
      }
    }
    return stringList.ToArray();
  }

  public void InitForcePing()
  {
    for (int index = 0; index < this._Pingers.Length; ++index)
      this._Pingers[index].InitForcePing();
  }

  private bool IsServerOK(AppServerInfo result, string dbConnectionString)
  {
    if (result.Result != ServerPingResult.Accessible)
      return false;
    return dbConnectionString == string.Empty || dbConnectionString == result.DatabaseConnectionString;
  }

  private AppServerInfo FindServer(string dbConnectionString)
  {
    AppServerInfo server = (AppServerInfo) null;
    AppServerInfo result = (AppServerInfo) null;
    switch (this.SelectionMode)
    {
      case ServerSelectionMode.PrimaryServer:
        for (int key = 0; key < this.Servers.Count; ++key)
        {
          if (this.Servers.TryGetValue(key, out result) && this.IsServerOK(result, dbConnectionString))
          {
            server = result;
            break;
          }
        }
        break;
      case ServerSelectionMode.LoadBalancing:
        int num = int.MaxValue;
        for (int key = 0; key < this.Servers.Count; ++key)
        {
          if (this.Servers.TryGetValue(key, out result) && this.IsServerOK(result, dbConnectionString) && result.LoadState < num)
          {
            num = result.LoadState;
            server = result;
          }
        }
        break;
      case ServerSelectionMode.Random:
        Random random = new Random();
        for (int index = 0; index < this.Servers.Count; ++index)
        {
          if (this.Servers.TryGetValue(random.Next(this.Servers.Count), out result) && this.IsServerOK(result, dbConnectionString))
          {
            server = result;
            break;
          }
        }
        if (server == null)
        {
          for (int key = 0; key < this.Servers.Count; ++key)
          {
            if (this.Servers.TryGetValue(key, out result) && this.IsServerOK(result, dbConnectionString))
            {
              server = result;
              break;
            }
          }
          break;
        }
        break;
    }
    return server;
  }

  public string GetAppServerURL(string dbConnectionString, bool forceCheckConnection)
  {
    while (!this._Initialized)
      Thread.Sleep(1000);
    if (forceCheckConnection)
    {
      this.InitForcePing();
      Thread.Sleep(this._ForcePingTimeout * 1000);
    }
    AppServerInfo server = this.FindServer(dbConnectionString);
    return server != null ? server.ServerURL : string.Empty;
  }
}
