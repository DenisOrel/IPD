// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterEventArgs
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FilterEventArgs : NotificationEventArgs
{
  private Guid _filterGuid;

  public FilterEventArgs(string eventName, bool useDelays, Guid filterGuid)
    : base(eventName, useDelays)
  {
    this._filterGuid = filterGuid;
  }

  public Guid FilterGuid => this._filterGuid;
}
