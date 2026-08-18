// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.GtcProcessorFactory
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.BackgroundTask;
using System;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal static class GtcProcessorFactory
{
  public static GtcProcessor GetProcessor(
    Guid sessionGuid,
    BaseTaskForBackgroundTaskService task,
    IImportConfig importConfig)
  {
    switch (importConfig.Version)
    {
      case GtcVersion.Second:
        return (GtcProcessor) new Gtc20Processor(sessionGuid, task, importConfig);
      case GtcVersion.First:
        return (GtcProcessor) new Gtc10Processor(sessionGuid, task, importConfig);
      case GtcVersion.FirstForAdveon:
        return (GtcProcessor) new Gtc10ForAdveonProcessor(sessionGuid, task, importConfig);
      default:
        throw new Exception("Не удалось найти обработчик для версии GTC");
    }
  }
}
