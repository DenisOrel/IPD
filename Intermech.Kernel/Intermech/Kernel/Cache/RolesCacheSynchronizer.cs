// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Cache.RolesCacheSynchronizer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;


namespace Intermech.Kernel.Cache;

internal class RolesCacheSynchronizer : CustomServerSynchronizer
{
  private RolesCache _RolesCache;

  public RolesCacheSynchronizer(RolesCache rolesCache)
    : base(new Guid("7561b00c-03d9-4dec-bf19-5bf358f434a8"), "Служба синхронизации кэша ролей")
  {
    this._RolesCache = rolesCache;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._RolesCache.ReloadRoles(session, false);
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
