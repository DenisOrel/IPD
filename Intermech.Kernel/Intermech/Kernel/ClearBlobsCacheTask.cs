// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ClearBlobsCacheTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;


namespace Intermech.Kernel;

internal class ClearBlobsCacheTask : DBCustomManualScheduledService
{
  private BlobStoragesPool _BlobsPool;

  public ClearBlobsCacheTask(BlobStoragesPool blobsPool) => this._BlobsPool = blobsPool;

  public override Guid GUID => new Guid("17759ebe-1488-4401-a473-3a0792c60c31");

  public override string ServiceName
  {
    get => LocalizationHolder.rm.GetString(nameof (ClearBlobsCacheTask));
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    this.Session.EventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("StartClearBlobs"), Consts.traceAlways, string.Empty);
    if (ServerServices.GetService(typeof (IAppServerFilesCache)) is IAppServerFilesCache service)
      service.ClearServerCache();
    this.Session.EventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("EndClearBlobs"), Consts.traceAlways, string.Empty);
    return true;
  }
}
