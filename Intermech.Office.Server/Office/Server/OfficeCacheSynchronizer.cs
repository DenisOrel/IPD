// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.OfficeCacheSynchronizer
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using Intermech.Office.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Server;

internal class OfficeCacheSynchronizer : CustomServerSynchronizer
{
  [NotNull]
  private readonly OfficeRegistrationService _officeRegisterSrvc;

  public OfficeCacheSynchronizer()
    : base(new Guid("fd9a251d-a3fe-4f53-b3a6-d19ea96590ed"), "Служба синхронизации кэша канцелярий")
  {
    this._officeRegisterSrvc = (OfficeRegistrationService) ApplicationServices.Container.GetService<ICustomServices>().GetService(typeof (IOfficeRegistrationService));
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._officeRegisterSrvc.InitCacheReload();
  }

  public void AddEvent([NotNull] string strInfo, [NotNull] IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
