// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClearServerCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.Services;

internal class ClearServerCache : DBCustomManualScheduledService
{
  public override Guid GUID => new Guid("a6a8988d-b8b5-409c-80eb-13ab89d58e95");

  public override string ServiceName => "Очистка кэша сервера приложений";

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    try
    {
      (ServerServices.GetService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadCache(this.Session.SessionGUID);
      (ServerServices.GetService(typeof (IDelayedUpdaterService)) as DelayedUpdaterService).ClearCache();
    }
    catch (Exception ex)
    {
      this.Session.EventLogHelper.AddToTrace($"Фоновая задача чистки кэша сервера приложений прервана с ошибкой: {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, string.Empty);
    }
    return true;
  }
}
