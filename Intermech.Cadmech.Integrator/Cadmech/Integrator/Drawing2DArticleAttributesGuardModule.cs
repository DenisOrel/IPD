// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Drawing2DArticleAttributesGuardModule
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal abstract class Drawing2DArticleAttributesGuardModule : InitializerModule
{
  private IAttributesLockService attributesLockService;
  private IntegratorObject integratorRef;
  private Drawing2DArticleAttributesGuard articleAttributesGuard;

  protected Drawing2DArticleAttributesGuardModule(Guid integratorId, string integratorName)
  {
    this.attributesLockService = ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true);
    this.integratorRef = new IntegratorObject(integratorId, integratorName);
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.articleAttributesGuard = new Drawing2DArticleAttributesGuard(this.attributesLockService, this.integratorRef);
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
