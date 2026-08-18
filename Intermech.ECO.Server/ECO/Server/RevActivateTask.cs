// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevActivateTask
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.ECO.Server;

internal class RevActivateTask : DBCustomManualScheduledService
{
  private static readonly string revActivateGuid = "cadd955b-306c-11d8-b4e9-00304f19f545";
  private ECOServer _ecoServer;

  public RevActivateTask(ECOServer ecoServer) => this._ecoServer = ecoServer;

  public override Guid GUID => new Guid(RevActivateTask.revActivateGuid);

  public override string ServiceName => LocalizationHolder.rm.GetString("ECO_Server15");

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    this._ecoServer.performTime();
    return true;
  }
}
