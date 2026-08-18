// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileDataExchangeFactory
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileDataExchangeFactory
{
  private readonly IIntegrator integrator;

  public SingleFileDataExchangeFactory(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public virtual ICaptureChangesDriver CreateCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) new SingleFileCaptureChangesDriver((IServiceProvider) this.Integrator);
  }

  public virtual IEmbedAttributesDriver CreateEmbedAttributesDriver()
  {
    return (IEmbedAttributesDriver) new DocumentEmbedAttributesDriver(this.Integrator);
  }

  protected IIntegrator Integrator => this.integrator;
}
