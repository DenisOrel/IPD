// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.ToolSettingsCacheSynchronizer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Services;
using System;


namespace Intermech.Tools.Kernel;

internal sealed class ToolSettingsCacheSynchronizer : CustomServerSynchronizer
{
  public ToolSettingsCacheSynchronizer()
    : base(new Guid("80222E02-5C30-4B20-BD34-14B6188181DD"), "Служба синхронизации настроек инструментов и интеграторов")
  {
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    EventHandler reloadCache = this.ReloadCache;
    if (reloadCache == null)
      return;
    reloadCache((object) null, EventArgs.Empty);
  }

  public void FireReloadCacheEvent(UserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(string.Empty), session.DataManager);
  }

  public event EventHandler ReloadCache;
}
