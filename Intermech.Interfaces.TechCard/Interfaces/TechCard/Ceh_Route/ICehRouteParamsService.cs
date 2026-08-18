// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.ICehRouteParamsService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.TechCard.Ceh_Route.Settings;
using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>Интерфейс службы настроек</summary>
[Obsolete("Use IAppSettingsService<ICehRouteSettings> instead. Will be removed in IPS 8.0")]
public interface ICehRouteParamsService : IAppSettingsService<ICehRouteSettings>
{
  /// <summary>Загрузка настроек</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="settings"></param>
  /// <returns>Reserved</returns>
  [Obsolete("Use IAppSettingsService<ICehRouteSettings>.SaveSettings instead. Will be removed in IPS 8.0", true)]
  int SaveSettings(Guid sessionGuid, ICehRouteParamsItem settings);

  /// <summary>Загрузка настроек</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="settings"></param>
  /// <returns>Reserved</returns>
  [Obsolete("Use IAppSettingsService<ICehRouteSettings>.LoadSettings instead. Will be removed in IPS 8.0", true)]
  int LoadSettings(Guid sessionGuid, out ICehRouteParamsItem settings);
}
