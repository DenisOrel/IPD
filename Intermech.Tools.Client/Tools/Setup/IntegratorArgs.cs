// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.IntegratorArgs
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class IntegratorArgs : EventArgs
{
  private IntegratorObject integratorObject;

  public IntegratorArgs(IntegratorObject integratorObject)
  {
    this.integratorObject = integratorObject;
  }

  public IntegratorObject IntegratorObject => this.integratorObject;
}
