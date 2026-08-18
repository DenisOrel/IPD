// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveAutoPlaceCacheSynchronizer
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveAutoPlaceCacheSynchronizer : CustomServerSynchronizer
{
  private readonly ArchiveAutoPlaceCacheService _archiveAutoPlaceCacheService;

  public ArchiveAutoPlaceCacheSynchronizer()
    : base(new Guid("D7AE6127-F5B4-41E8-98B4-AF5F59198F56"), "Служба синхронизации кэша авторазмещения документов в архивах")
  {
    this._archiveAutoPlaceCacheService = ApplicationServices.Container.GetService<IArchiveAutoPlaceCacheService>() as ArchiveAutoPlaceCacheService;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    this._archiveAutoPlaceCacheService.FillCache();
  }

  public void FireReloadCacheEvent(string strInfo, IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(string.Empty), db);
  }
}
