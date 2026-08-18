// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.AppServerInfo
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

#nullable disable
namespace Intermech.ConnectionBroker;

internal class AppServerInfo
{
  public string ServerURL;
  public ServerPingResult Result;
  public string DatabaseConnectionString;
  public int LoadState;
  public string ServerName;

  public AppServerInfo(string serverName, string serverUrl)
  {
    this.ServerURL = serverUrl;
    this.ServerName = serverName;
    this.Result = ServerPingResult.None;
    this.DatabaseConnectionString = string.Empty;
    this.LoadState = 0;
  }

  public AppServerInfo(
    string serverName,
    string serverConnectStr,
    string dbConnectStr,
    ServerPingResult result,
    int loadState)
  {
    this.ServerName = serverName;
    this.ServerURL = serverConnectStr;
    this.Result = result;
    this.DatabaseConnectionString = dbConnectStr;
    this.LoadState = loadState;
  }

  public AppServerInfo(AppServerInfo proto)
  {
    this.ServerURL = proto.ServerURL;
    this.Result = proto.Result;
    this.DatabaseConnectionString = proto.DatabaseConnectionString;
    this.LoadState = proto.LoadState;
    this.ServerName = proto.ServerName;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is AppServerInfo))
      return false;
    AppServerInfo appServerInfo = obj as AppServerInfo;
    return this.DatabaseConnectionString == appServerInfo.DatabaseConnectionString && this.Result == appServerInfo.Result && this.LoadState == appServerInfo.LoadState;
  }

  public override int GetHashCode()
  {
    return this.DatabaseConnectionString.GetHashCode() ^ this.LoadState.GetHashCode();
  }
}
