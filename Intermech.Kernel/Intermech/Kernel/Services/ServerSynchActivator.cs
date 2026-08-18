// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ServerSynchActivator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services;

internal class ServerSynchActivator : DBTimedService
{
  private ServersSynchTask _SynchTask;

  public ServerSynchActivator(ServersSynchTask synchTask) => this._SynchTask = synchTask;

  public override Guid GUID => new Guid("ea98d3c3-7adb-45aa-b344-beb022ea8a52");

  public override string ServiceName => "Активатор службы синхронизации серверов приложений";

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    if (properties.IntInfo != ServerConsts.RemotingServerPort)
      return false;
    this._SynchTask.StartSynchIfNeed();
    return true;
  }
}
