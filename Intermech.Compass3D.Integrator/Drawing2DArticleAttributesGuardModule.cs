// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DArticleAttributesGuardModule
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DArticleAttributesGuardModule : InitializerModule
{
  private Drawing2DArticleAttributesGuard articleAttributesGuard;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.articleAttributesGuard = new Drawing2DArticleAttributesGuard(ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true), new IntegratorObject(Plugin.IntegratorId, Plugin.IntegratorName));
    this.articleAttributesGuard.Start();
  }

  protected override void DoShutdown()
  {
    if (this.articleAttributesGuard != null)
    {
      this.articleAttributesGuard.Stop();
      this.articleAttributesGuard = (Drawing2DArticleAttributesGuard) null;
    }
    base.DoShutdown();
  }
}
