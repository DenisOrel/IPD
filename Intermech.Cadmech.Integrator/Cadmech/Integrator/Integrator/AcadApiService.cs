// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Integrator.AcadApiService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.ComTypes;
using Intermech.Tools.Integrators;
using System;
using System.Threading;

#nullable disable
namespace Intermech.Cadmech.Integrator.Integrator;

internal sealed class AcadApiService : CadApiService
{
  private readonly RetryRejectedCallsFilter rejectedCallsMessageFilter;
  private IMessageFilter previousMessageFilter;

  public AcadApiService(
    IIntegrator owner,
    string applicationName,
    ComObjectProvider comObjectProvider)
    : base(owner, applicationName, comObjectProvider)
  {
    this.rejectedCallsMessageFilter = new RetryRejectedCallsFilter(TimeSpan.FromMilliseconds(250.0));
  }

  protected override void DoOpenApiSession(bool topLevelSession)
  {
    if (topLevelSession && Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
      this.InstallComMessageFilter();
    base.DoOpenApiSession(topLevelSession);
  }

  protected override void DoCloseApiSession(bool topLevelSession)
  {
    try
    {
      base.DoCloseApiSession(topLevelSession);
    }
    finally
    {
      if (topLevelSession && Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
        this.RestoreComMessageFilter();
    }
  }

  private void InstallComMessageFilter()
  {
    this.previousMessageFilter = MessageFilter.Current;
    MessageFilter.Current = (IMessageFilter) this.rejectedCallsMessageFilter;
  }

  private void RestoreComMessageFilter()
  {
    if (MessageFilter.Current == this.rejectedCallsMessageFilter)
      MessageFilter.Current = this.previousMessageFilter;
    this.previousMessageFilter = (IMessageFilter) null;
  }
}
