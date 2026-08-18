// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.AppServerPing
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.ConnectionBroker;
using System;
using System.Threading;

#nullable disable
namespace Intermech.ConnectionBroker;

internal class AppServerPing
{
  private AppServerInfo _AppServer;
  private int _ServerIndex;
  private ConnectionBrokerServer _Broker;
  private bool _LastPingResult;
  private int _ErrorsCount;
  private string _LastError = string.Empty;
  private volatile bool _ForcePing;
  private volatile bool _DontExit = true;

  public AppServerPing(
    ConnectionBrokerServer broker,
    int index,
    string serverName,
    string serverURL)
  {
    this._Broker = broker;
    this._ServerIndex = index;
    this._AppServer = new AppServerInfo(serverName, serverURL);
  }

  public void StartPing(object obj)
  {
    while (this._DontExit)
    {
      try
      {
        IMServerLiveStatus liveStatus = ((IMServer) Activator.GetObject(typeof (IMServer), this._AppServer.ServerURL)).LiveStatus;
        this._AppServer.Result = ServerPingResult.Accessible;
        if (this._Broker.SelectionMode == ServerSelectionMode.LoadBalancing)
          this._AppServer.LoadState = liveStatus.ActivityCounter;
        this._AppServer.DatabaseConnectionString = liveStatus.ConnectionString;
        this._ErrorsCount = 0;
        this._LastPingResult = true;
      }
      catch (Exception ex)
      {
        this._AppServer.Result = ServerPingResult.NotAccessible;
        ++this._ErrorsCount;
        this._LastPingResult = false;
        lock (this._LastError)
          this._LastError = ex.Message;
      }
      try
      {
        this._Broker.UpdateServerInfo(this._ServerIndex, new AppServerInfo(this._AppServer));
        this.DoWait(!this._LastPingResult ? (this._ErrorsCount <= 20 ? 5 : this._Broker.PingFailInterval) : this._Broker.PingSuccessInterval);
      }
      catch (Exception ex)
      {
        this._Broker._EventLog.DefaultLog.Write($"Ошибка в цикле опроса сервера IPS: {ex.Message}", EventLogItemType.Error);
      }
    }
  }

  private void DoWait(int p)
  {
    int num = p * 10;
    while (!this._ForcePing && num-- > 0)
      Thread.Sleep(100);
    this._ForcePing = false;
  }

  public string LastError
  {
    get
    {
      lock (this._LastError)
        return this._LastError;
    }
  }

  public void InitForcePing() => this._ForcePing = true;

  public void InitForceExit()
  {
    this._DontExit = false;
    this._ForcePing = true;
  }
}
