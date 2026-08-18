// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Services
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Plugins;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public abstract class Services
{
  [CanBeNull]
  private static ICalendarsService _calendars;
  [CanBeNull]
  private static IPluginManager _pluginManager;
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  [NotNull]
  public static ICalendarsService Calendars
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._calendars.CheckInitializedIn<ICalendarsService>(typeof (Library));
    }
  }

  [NotNull]
  public static IPluginManager PluginManager
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._pluginManager.CheckInitializedIn<IPluginManager>(typeof (Library));
    }
  }

  internal static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Services._initOnce.Invoke(ref session, (Action) (() =>
    {
      Services._calendars = serviceProvider.GetService<ICalendarsService>();
      Services._pluginManager = serviceProvider.GetService<IPluginManager>();
    }));
  }
}
