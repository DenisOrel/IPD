// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.IServiceForBackgroundTask
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using System;

#nullable disable
namespace Intermech.GTC.Interfaces;

public interface IServiceForBackgroundTask
{
  void StartTask(Guid sessionGuid, Guid taskGuid, string taskName, object inputData);

  void StoppingTask(Guid taskGuid);

  bool StoppedTask(Guid taskGuid);

  void PauseTask(Guid taskGuid);

  void ResumeTask(Guid taskGuid);

  int GetCompleted(Guid taskGuid, out int state, out string text);

  BackgroundTaskResult GetResult(Guid taskGuid);
}
