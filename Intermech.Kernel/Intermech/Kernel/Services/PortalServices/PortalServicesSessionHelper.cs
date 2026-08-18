// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PortalServicesSessionHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal static class PortalServicesSessionHelper
{
  public static IUserSession GetCloneSession(
    Guid userSessionGuid,
    string sessionName,
    string functionName = "unknown",
    bool isPermanent = false)
  {
    return PortalServicesSessionHelper.GetCloneSession(UserSession.GetSessionByID(userSessionGuid), sessionName, functionName, isPermanent);
  }

  public static IUserSession GetCloneSession(
    IUserSession sourceSession,
    string sessionName,
    string functionName = "unknown",
    bool isPermanent = false)
  {
    IUserSession cloneSession = (sourceSession as IServerSession).Clone(isPermanent, sessionName);
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"Create session clone = {cloneSession.SessionGUID} ({sessionName}) from \"{functionName}\". Source session = {sourceSession.SessionGUID}.");
    return cloneSession;
  }

  public static void LogoutSession(IUserSession session, string sessionName, string functionName = "unknown")
  {
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"Logout session = {session.SessionGUID} ({sessionName}) from \"{functionName}\".");
    session.Logout(sessionName);
  }
}
