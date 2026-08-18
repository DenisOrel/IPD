// Decompiled with JetBrains decompiler
// Type: Intermech.Security.RBSServer
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Security.Principal;
using System.Text;

#nullable disable
namespace Intermech.Security;

public static class RBSServer
{
  private static readonly object syncRoot = new object();
  private static bool isInitialized;
  private static readonly Guid serverSecurityToken = Guid.NewGuid();

  public static void InitializeSecurityContext()
  {
    lock (RBSServer.syncRoot)
    {
      if (RBSServer.isInitialized)
        return;
      AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);
      RBSServer.isInitialized = true;
    }
  }

  private static void CheckInitialized()
  {
    lock (RBSServer.syncRoot)
    {
      if (!RBSServer.isInitialized)
        throw new InvalidOperationException("Метод InitializeSecurityContext не был вызван.");
    }
  }

  public static void UpdateSecurityContext(long systemUserId)
  {
    RBSServer.CheckInitialized();
    IPSPrincipal.DefaultPrincipal = new IPSPrincipal(new IPSIdentity(systemUserId, "System"), RBSServer.serverSecurityToken, IPSBuiltInRole.Server);
  }

  public static void AuthenticateCaller()
  {
    IPSPrincipal principal = RBSServer.GetPrincipal();
    if (principal.SecurityToken == Guid.Empty)
      throw new KernelException(LocalizationHolder.rm.GetString("Interfaces.Server_3"));
    if (principal.SecurityToken == RBSServer.serverSecurityToken)
      return;
    IUserSession session = ServiceUtils.GetService<IUserSessionCollection>((object) ServerServices.ServiceContainer, true).GetSession(principal.SecurityToken);
    if (session == null)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_4"), (object) principal.Identity.UserName));
    if (session.UserID != principal.Identity.UserId)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_5"), (object) principal.Identity.UserName));
  }

  public static void AuthorizeAsAdmin()
  {
    IPSPrincipal principal = RBSServer.GetPrincipal();
    if (!principal.IsInRole(IPSBuiltInRole.Administrator) && !principal.IsInRole(IPSBuiltInRole.Server))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_6"), (object) principal.Identity.UserName));
  }

  private static IPSPrincipal GetPrincipal()
  {
    IPSPrincipal currentPrincipal = IPSPrincipal.CurrentPrincipal;
    if (currentPrincipal.Identity.IsAuthenticated)
      return currentPrincipal;
    throw RBSServer.CantGetPrincipalFromCurrentThread((IPrincipal) currentPrincipal);
  }

  private static KernelException CantGetPrincipalFromCurrentThread(IPrincipal principal)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(LocalizationHolder.rm.GetString("Interfaces.Server_7"));
    stringBuilder.Append(" ");
    stringBuilder.Append("Дополнительные сведения:");
    stringBuilder.Append(" ");
    if (principal == null)
      stringBuilder.Append("свойство Thread.CurrentPrincipal не задано");
    else if (principal is IPSPrincipal)
    {
      IPSPrincipal ipsPrincipal = (IPSPrincipal) principal;
      stringBuilder.Append($"Principal type = {principal.GetType()}, Identity {{ Name = {ipsPrincipal.Identity.UserName}, Id = {ipsPrincipal.Identity.UserId} }}, Security Token = {ipsPrincipal.SecurityToken}, AppDomain {{ ID = {AppDomain.CurrentDomain.Id}, Name = {AppDomain.CurrentDomain.FriendlyName} }}");
    }
    else
      stringBuilder.Append($"Principal type = {principal.GetType()}, Identity {{ Name = {principal.Identity.Name} }}, AppDomain {{ ID = {AppDomain.CurrentDomain.Id}, Name = {AppDomain.CurrentDomain.FriendlyName} }}");
    return new KernelException(stringBuilder.ToString());
  }
}
