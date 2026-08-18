// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.LaunchActionEditorEvents
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class LaunchActionEditorEvents
{
  public void FireLaunchActionUpdated(LaunchActionInfo updatedAction)
  {
    if (this.LaunchActionUpdated == null)
      return;
    this.LaunchActionUpdated((object) null, new LaunchActionArgs(updatedAction));
  }

  public void FireLaunchActionRemoved(LaunchActionInfo removedAction)
  {
    if (this.LaunchActionRemoved == null)
      return;
    this.LaunchActionRemoved((object) null, new LaunchActionArgs(removedAction));
  }

  public event EventHandler<LaunchActionArgs> LaunchActionUpdated;

  public event EventHandler<LaunchActionArgs> LaunchActionRemoved;
}
