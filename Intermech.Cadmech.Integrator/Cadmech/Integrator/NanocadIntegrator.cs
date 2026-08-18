// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.NanocadIntegrator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Runtime.ComInterop;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class NanocadIntegrator : AcadIntegratorBase
{
  public override string DisplayName => NanocadConsts.IntegratorName;

  public override Guid Id => NanocadConsts.IntegratorId;

  public override string GetServerObjectTemplate()
  {
    return this.GetServerObjectTemplateFromResource("Intermech.Cadmech.Integrator.Resources.NanoCAD template.xml");
  }

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) Intermech.Cadmech.Integrator.Properties.Resources.IR_NanoCAD_16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) Intermech.Cadmech.Integrator.Properties.Resources.IR_NanoCAD_32x32 : base.GetApplicationImage(imageSize);
  }

  protected override CadApiService DoCreateApiService()
  {
    return (CadApiService) new NanocadApiService((IIntegrator) this, NanocadConsts.ApplicationName, (ComObjectProvider) new ProgIdProvider("nanocad.Application", false));
  }
}
