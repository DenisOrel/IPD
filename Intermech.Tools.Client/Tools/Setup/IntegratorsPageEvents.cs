// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.IntegratorsPageEvents
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class IntegratorsPageEvents
{
  public void FireUpdated(IntegratorObject integratorObject)
  {
    if (this.Updated == null)
      return;
    this.Updated((object) null, new IntegratorArgs(integratorObject));
  }

  public void FireRemoved(IntegratorObject integratorObject)
  {
    if (this.Removed == null)
      return;
    this.Removed((object) null, new IntegratorArgs(integratorObject));
  }

  public event EventHandler<IntegratorArgs> Updated;

  public event EventHandler<IntegratorArgs> Removed;
}
