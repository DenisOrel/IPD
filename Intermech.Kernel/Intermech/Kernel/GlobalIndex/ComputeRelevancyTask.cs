// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.ComputeRelevancyTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.GlobalIndex;

internal class ComputeRelevancyTask : DBCustomManualScheduledService
{
  private GlobalIndexService _IndexService;

  public ComputeRelevancyTask(GlobalIndexService indexService) => this._IndexService = indexService;

  public override Guid GUID => new Guid("cadd93c8-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName
  {
    get => LocalizationHolder.rm.GetString(nameof (ComputeRelevancyTask));
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    this._IndexService.ComputeRelevancy(this.Session.DataManager);
    return true;
  }
}
