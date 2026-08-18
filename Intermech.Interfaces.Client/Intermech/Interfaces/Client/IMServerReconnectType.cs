// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerReconnectType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Способы переподключения к серверу приложений.</summary>
public enum IMServerReconnectType
{
  TryConnectAgain,
  AbortConnection,
  AbortApplication,
  AbortApplicationSilently,
}
