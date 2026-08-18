// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Params.ParamsServerSynchronizer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Params;

internal class ParamsServerSynchronizer : CustomServerSynchronizer
{
  private IImbaseParamsService _imbaseParamsService;

  public ParamsServerSynchronizer(IImbaseParamsService imbaseParamsService)
    : base(new Guid("E88850B3-7FFB-4DA2-AEF7-EEE66905E757"), "Служба синхронизации настроек Imbase")
  {
    this._imbaseParamsService = imbaseParamsService;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._imbaseParamsService.ResetSettings(session, eventProps.StringInfo);
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (string.IsNullOrEmpty(strInfo) || db == null || !this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
