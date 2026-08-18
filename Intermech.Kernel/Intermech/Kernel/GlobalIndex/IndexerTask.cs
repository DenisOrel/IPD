// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.IndexerTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.GlobalIndex;

internal class IndexerTask : DBCustomManualScheduledService
{
  private GlobalIndexService _IndexService;

  public IndexerTask(GlobalIndexService indexService) => this._IndexService = indexService;

  public override Guid GUID => new Guid("cadd93c7-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName => LocalizationHolder.rm.GetString(nameof (IndexerTask));

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    return this._IndexService.ProcessQueue();
  }
}
