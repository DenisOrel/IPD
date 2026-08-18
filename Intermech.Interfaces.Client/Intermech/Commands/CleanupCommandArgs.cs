// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CleanupCommandArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

public sealed class CleanupCommandArgs : EventArgs
{
  private static readonly CleanupCommandArgs empty = new CleanupCommandArgs();
  private Exception exception;

  public CleanupCommandArgs()
  {
  }

  public CleanupCommandArgs(Exception exception)
  {
    this.exception = exception != null ? exception : throw new ArgumentNullException(nameof (exception));
  }

  public bool Failed => this.exception != null;

  public Exception Exception => this.exception;

  public static CleanupCommandArgs Empty => CleanupCommandArgs.empty;
}
