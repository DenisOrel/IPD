// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCommandEventSite
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

public sealed class ObjectCommandEventSite
{
  public void RaiseBefore(Command command, BeforeObjectCommandArgs eventArgs)
  {
    if (command == null)
      throw new ArgumentNullException(nameof (command));
    if (eventArgs == null)
      throw new ArgumentNullException(nameof (eventArgs));
    EventHandler<BeforeObjectCommandArgs> before = this.Before;
    if (before == null)
      return;
    before((object) command, eventArgs);
  }

  public void RaiseAfter(Command command, AfterObjectCommandArgs eventArgs)
  {
    if (command == null)
      throw new ArgumentNullException(nameof (command));
    if (eventArgs == null)
      throw new ArgumentNullException(nameof (eventArgs));
    EventHandler<AfterObjectCommandArgs> after = this.After;
    if (after == null)
      return;
    after((object) command, eventArgs);
  }

  public void RaiseCleanup(Command command, CleanupCommandArgs eventArgs)
  {
    if (command == null)
      throw new ArgumentNullException(nameof (command));
    if (eventArgs == null)
      throw new ArgumentNullException(nameof (eventArgs));
    EventHandler<CleanupCommandArgs> cleanup = this.Cleanup;
    if (cleanup == null)
      return;
    cleanup((object) command, eventArgs);
  }

  public event EventHandler<BeforeObjectCommandArgs> Before;

  public event EventHandler<AfterObjectCommandArgs> After;

  public event EventHandler<CleanupCommandArgs> Cleanup;
}
