// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Session.CompositionTrackingSessionDataHolder
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.CompositionTracking.Server.Session;

internal static class CompositionTrackingSessionDataHolder
{
  internal static CompositionTrackingSessionData GetData(IUserSession session, bool allowCreate = true)
  {
    if (session.GetSessionPluginsData((object) "CompositionTrackingSessionData") is CompositionTrackingSessionData data || !allowCreate)
      return data;
    data = new CompositionTrackingSessionData();
    session.SetSessionPluginsData((object) "CompositionTrackingSessionData", (object) data);
    return data;
  }

  internal static void RemoveData(IUserSession session)
  {
    session.RemoveSessionPluginsData((object) "CompositionTrackingSessionData");
  }

  private static class Consts
  {
    internal const string SessionPluginData = "CompositionTrackingSessionData";
  }
}
