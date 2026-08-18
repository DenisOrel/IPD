// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingJointsModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ApplicationModel;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class WeldingJointsModule : InitializerModule
{
  private WeldingSeamAttributesGuard weldingSeamsAttributesGuard;
  private IExceptionDisplayService exceptionService;

  public WeldingJointsModule(
    WeldingSeamAttributesGuard weldingSeamsAttributesGuard,
    IExceptionDisplayService exceptionService)
  {
    if (weldingSeamsAttributesGuard == null)
      throw new ArgumentNullException(nameof (weldingSeamsAttributesGuard));
    if (exceptionService == null)
      throw new ArgumentNullException(nameof (exceptionService));
    this.weldingSeamsAttributesGuard = weldingSeamsAttributesGuard;
    this.exceptionService = exceptionService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    try
    {
      this.weldingSeamsAttributesGuard.Start();
    }
    catch (Exception ex)
    {
      this.exceptionService.ShowException(ex);
    }
  }

  protected override void DoShutdown()
  {
    if (this.weldingSeamsAttributesGuard != null)
    {
      this.weldingSeamsAttributesGuard.Stop();
      this.weldingSeamsAttributesGuard = (WeldingSeamAttributesGuard) null;
    }
    base.DoShutdown();
  }
}
