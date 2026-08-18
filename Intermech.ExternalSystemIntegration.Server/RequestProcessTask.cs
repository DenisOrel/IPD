// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestProcessTask
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class RequestProcessTask : DBCustomManualScheduledService
{
  private string _name;
  private Guid _guid;

  public RequestProcessTask()
  {
    this._name = Const.RequestTaskName;
    this._guid = Const.RequestTaskGuid;
  }

  public override Guid GUID => this._guid;

  public override string ServiceName => this._name;

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    IUserSession ASession = this.Session.Clone(nameof (RequestProcessTask));
    try
    {
      return new RequestProcessTaskHelper(ASession).ProcessTask();
    }
    finally
    {
      ASession.Logout(nameof (RequestProcessTask));
    }
  }
}
