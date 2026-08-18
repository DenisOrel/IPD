// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.LaunchActionArgs
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class LaunchActionArgs : EventArgs
{
  private LaunchActionInfo actionInfo;

  public LaunchActionArgs(LaunchActionInfo actionInfo) => this.actionInfo = actionInfo;

  public LaunchActionInfo ActionInfo => this.actionInfo;
}
