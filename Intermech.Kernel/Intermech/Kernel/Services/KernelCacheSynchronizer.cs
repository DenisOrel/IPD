// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.KernelCacheSynchronizer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.Services;

internal class KernelCacheSynchronizer : CustomServerSynchronizer, IKernelCacheSynchronizer
{
  private DBConfigurationService _ConfigService;

  private DBConfigurationService ConfigService
  {
    get
    {
      if (this._ConfigService == null)
        this._ConfigService = ServerServices.GetService(typeof (IDBConfigurationService)) as DBConfigurationService;
      return this._ConfigService;
    }
  }

  public KernelCacheSynchronizer()
    : base(new Guid("8a3c6e18-5344-4088-86df-b61f4c9aebc4"), "Служба синхронизации кэшей ядра IPS")
  {
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    string[] strArray = eventProps.StringInfo.Split(';');
    UserSession userSession = session as UserSession;
    if (strArray.Length == 0)
      return;
    switch (strArray[0])
    {
      case "0":
        userSession.DBCache.ReloadOldTables(userSession.DataManager);
        break;
      case "1":
        if (this.ConfigService == null || strArray.Length < 4)
          break;
        this.ConfigService.ReloadValue(strArray[1], strArray[2], strArray[3], userSession.DataManager);
        break;
    }
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
