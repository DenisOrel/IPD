// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.CustomUsersTableFilterSynchronizer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;

#nullable disable
namespace Intermech.Imbase.Server;

internal class CustomUsersTableFilterSynchronizer : CustomServerSynchronizer
{
  private ICustomUsersTableFilterService _customUsersTableFilterService;

  public CustomUsersTableFilterSynchronizer(
    ICustomUsersTableFilterService customUsersTableFilterService)
    : base(new Guid("A32F5D46-F542-4057-B2A3-BDAD6E7F6654"), "Служба синхронизации кэша пользовательских фильтров Imbase")
  {
    this._customUsersTableFilterService = customUsersTableFilterService;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._customUsersTableFilterService.RemoveUserDataFromCache(eventProps.StringInfo);
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (string.IsNullOrEmpty(strInfo) || db == null || !this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
