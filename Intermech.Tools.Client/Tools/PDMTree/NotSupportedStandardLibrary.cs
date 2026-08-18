// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.NotSupportedStandardLibrary
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Diagnostics;
using Intermech.Tools.Integrators;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class NotSupportedStandardLibrary : IPDMStandardLibrary
{
  private readonly IIntegrator integrator;
  private IEventLogWriter log;

  public NotSupportedStandardLibrary(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public IEventLogWriter Log
  {
    [DebuggerStepThrough] get => this.log;
    [DebuggerStepThrough] set => this.log = value;
  }

  public string BeginUpdatePart(string partName, string modelFileName)
  {
    throw new NotSupportedException();
  }

  public void EndUpdatePart(string partName, string modelFileName)
  {
    throw new NotSupportedException();
  }
}
