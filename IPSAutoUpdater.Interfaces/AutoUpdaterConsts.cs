// Decompiled with JetBrains decompiler
// Type: IPSAutoUpdater.Interfaces.AutoUpdaterConsts
// Assembly: IPSAutoUpdater.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74369E9B-3C90-46D5-99C8-30597004F5A5
// Assembly location: D:\IPS\Client\IPSAutoUpdater.Interfaces.dll


namespace IPSAutoUpdater.Interfaces;

public class AutoUpdaterConsts
{
  public static readonly string InfoCaption = "Служба автообновления IPS";
  public static readonly string RemotingChannelName = "IPS.AutoUpdater";
  public static readonly int RemotingChannelPort = 31793;
  public static readonly string RemotingServerName = "IPSAutoUpdater.RemotingServer";
  public static readonly string RemotingServerAddress = $"tcp://localhost:{(object) AutoUpdaterConsts.RemotingChannelPort}/{AutoUpdaterConsts.RemotingServerName}";
}
