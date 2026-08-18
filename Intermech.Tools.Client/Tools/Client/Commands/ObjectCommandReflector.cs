// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ObjectCommandReflector
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Commands;
using System;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal class ObjectCommandReflector : IDisposable
{
  private ObjectCommandEventSite eventSite;

  public ObjectCommandReflector(ObjectCommandEventSite eventSite)
  {
    this.eventSite = eventSite;
    this.eventSite.Before += new EventHandler<BeforeObjectCommandArgs>(this.BeforeCommand);
    this.eventSite.After += new EventHandler<AfterObjectCommandArgs>(this.AfterCommand);
    this.eventSite.Cleanup += new EventHandler<CleanupCommandArgs>(this.CleanupCommand);
  }

  public void Dispose()
  {
    if (this.eventSite == null)
      return;
    this.eventSite.Before -= new EventHandler<BeforeObjectCommandArgs>(this.BeforeCommand);
    this.eventSite.After -= new EventHandler<AfterObjectCommandArgs>(this.AfterCommand);
    this.eventSite.Cleanup -= new EventHandler<CleanupCommandArgs>(this.CleanupCommand);
    this.eventSite = (ObjectCommandEventSite) null;
  }

  private void BeforeCommand(object sender, BeforeObjectCommandArgs e)
  {
    this.OnBeforeCommand((ObjectCommand) sender, e);
  }

  private void AfterCommand(object sender, AfterObjectCommandArgs e)
  {
    this.OnAfterCommand((ObjectCommand) sender, e);
  }

  private void CleanupCommand(object sender, CleanupCommandArgs e)
  {
    this.OnCleanupCommand((ObjectCommand) sender, e);
  }

  protected virtual void OnBeforeCommand(ObjectCommand command, BeforeObjectCommandArgs e)
  {
  }

  protected virtual void OnAfterCommand(ObjectCommand command, AfterObjectCommandArgs e)
  {
  }

  protected virtual void OnCleanupCommand(ObjectCommand command, CleanupCommandArgs e)
  {
  }
}
