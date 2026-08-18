// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.RepairDataTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class RepairDataTask : DBCustomManualScheduledService
{
  private AdminUtilsService _AdminUtils;

  public RepairDataTask(AdminUtilsService admUtils) => this._AdminUtils = admUtils;

  public override Guid GUID => new Guid("cadd93c5-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName => LocalizationHolder.rm.GetString(nameof (RepairDataTask));

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    try
    {
      this.SaveLog("RepairData.log", this._AdminUtils.RepairData(this.Session.SessionGUID));
    }
    catch (Exception ex)
    {
      this.Session.EventLogHelper.AddToTrace($"Фоновая задача проверки целостности данных прервана с ошибкой: {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, string.Empty);
    }
    return true;
  }
}
