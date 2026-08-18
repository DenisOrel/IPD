// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Settings.CommonSettingsSyncronizer
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Settings;

internal class CommonSettingsSyncronizer : CustomServerSynchronizer
{
  private ICommonSettingsHolder _commonSettingsHolder;

  public CommonSettingsSyncronizer(ICommonSettingsHolder commonSettingsHolder)
    : base(new Guid("893F788A-A7EB-4558-B4A2-95593EDD6BE8"), "Служба синхронизации настроек синхронизации с внешними системами")
  {
    this._commonSettingsHolder = commonSettingsHolder;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._commonSettingsHolder.ReadSettings(session.SessionGUID);
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (string.IsNullOrEmpty(strInfo) || db == null || !this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
