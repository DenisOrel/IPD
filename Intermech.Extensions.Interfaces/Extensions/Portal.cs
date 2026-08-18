// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Portal
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public abstract class Portal
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();
  public const char UnknownSiteCode = '?';
  private static char _currentSiteCode = '?';
  [CanBeNull]
  private static ISitesCacheService _sitesCacheService;

  internal static void Init([NotNull] IUserSession session)
  {
    Portal._initOnce.Invoke((Action) (() =>
    {
      Portal._sitesCacheService = session.GetCustomService<ISitesCacheService>(false);
      SiteInfo info = Portal._sitesCacheService?.Info;
      if (info == null)
        return;
      Portal.CurrentSiteCode = info.Code;
    }));
  }

  [NotEmpty]
  public static char CurrentSiteCode
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Portal.CheckWasInit();
      return Portal._currentSiteCode;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] private set
    {
      Portal._currentSiteCode = value;
    }
  }

  public static bool Enabled
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Portal.CheckWasInit();
      return Portal.CurrentSiteCode != '?';
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool CheckEnabled(bool throwExceptIfNot = true)
  {
    if (Portal.CurrentSiteCode != '?')
      return true;
    if (throwExceptIfNot)
      throw new Exception("Портал не доступен!");
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool CheckWasInit(bool throwExceptIfNot = true)
  {
    if (Portal._initOnce.Completed)
      return true;
    if (throwExceptIfNot)
      throw new NotYetInitializedException<Portal>();
    return false;
  }

  [NotNull]
  public static ISitesCacheService SitesCacheService
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Portal.CheckEnabled();
      return Portal._sitesCacheService;
    }
  }

  [NotNull]
  [ItemNotNull]
  public static List<SiteInfo> Sites
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Portal.SitesCacheService.Sites;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Reload([NotNull] object session) => Portal.SitesCacheService.Reload(session);

  [ContractAnnotation("throwException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SiteInfo GetSite([NotEmpty] char code, bool throwException = false)
  {
    SiteInfo site = Portal.SitesCacheService.GetSite(code);
    return !(site == null & throwException) ? site : throw new SiteNotFoundException(code);
  }

  [ContractAnnotation("throwException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SiteInfo GetSite([NotEmpty] long id, bool throwException = false)
  {
    SiteInfo site = Portal.SitesCacheService.GetSite(id);
    return !(site == null & throwException) ? site : throw new SiteNotFoundException(id);
  }

  [ContractAnnotation("throwException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SiteInfo GetSite([NotEmpty] Guid guid, bool throwException = false)
  {
    SiteInfo site = Portal.SitesCacheService.GetSite(guid, false);
    return !(site == null & throwException) ? site : throw new SiteNotFoundException(guid);
  }

  [ContractAnnotation("throwException:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SiteInfo GetSite([NotNull, NotWhitespace] string name, bool throwException = false)
  {
    SiteInfo site = Portal.SitesCacheService.GetSite(name);
    return !(site == null & throwException) ? site : throw new SiteNotFoundException(name, (string) null);
  }

  [NotNull]
  public static long[] SitesIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Portal.SitesCacheService.SitesIDs;
  }

  [NotNull]
  public static SiteInfo Info
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Portal.SitesCacheService.Info;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static char NextCode() => Portal.SitesCacheService.NextCode();

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetSiteDescription([NotNull, NotWhitespace] string siteID)
  {
    return Portal.SitesCacheService.GetSiteDescription(siteID);
  }
}
