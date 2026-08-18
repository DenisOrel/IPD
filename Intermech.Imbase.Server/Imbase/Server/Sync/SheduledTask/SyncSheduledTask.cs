// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.SheduledTask.SyncSheduledTask
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Sync;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server.Sync.SheduledTask;

internal class SyncSheduledTask : DBCustomManualScheduledService
{
  public SyncSheduledTask()
  {
    this.ServiceName = "Синхронизация с Imbase 5.0";
    this.GUID = new Guid("B81D6D35-5E4C-4DF0-9DF2-F9281278D3ED");
  }

  public override Guid GUID { get; }

  public override string ServiceName { get; }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    if (this.Session.GetCustomService(typeof (IImbaseSyncService)) is IImbaseSyncService customService)
    {
      Guid taskGuid = Guid.NewGuid();
      customService.StartTask(this.Session.SessionGUID, taskGuid, "Синхронизация с Imbase 5.0", (object) null);
      int state;
      do
      {
        Thread.Sleep(1000);
        customService.GetCompleted(taskGuid, out state, out string _);
      }
      while (state >= 0);
      List<BackgroundTaskMessage> messages = customService.GetResult(taskGuid).Messages;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      foreach (BackgroundTaskMessage backgroundTaskMessage in messages)
      {
        stringBuilder1.AppendLine(backgroundTaskMessage.Message);
        if (backgroundTaskMessage.Exception != null)
        {
          stringBuilder1.AppendLine(backgroundTaskMessage.Exception.Message);
          stringBuilder1.AppendLine(backgroundTaskMessage.Exception.StackTrace);
          stringBuilder2.Append(backgroundTaskMessage.Exception.Message);
          stringBuilder2.Append(backgroundTaskMessage.Exception.StackTrace);
        }
      }
      this.Session.EventLog.AddToTrace(stringBuilder1.ToString(), 0, "ImbaseSync.log");
      if (stringBuilder2.Length > 0)
        throw new Exception(stringBuilder2.ToString());
    }
    return true;
  }
}
