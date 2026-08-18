// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFDataExchangeFactory
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using System;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFDataExchangeFactory(IIntegrator integrator) : SingleFileDataExchangeFactory(integrator)
{
  public override ICaptureChangesDriver CreateCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) new PDFCaptureChangesDriver((IServiceProvider) this.Integrator);
  }
}
