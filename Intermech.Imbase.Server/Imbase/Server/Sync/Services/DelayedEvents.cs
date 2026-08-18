// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Services.DelayedEvents
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.Records;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Services;

internal class DelayedEvents : LongLifeObject, IDelayedEvents
{
  private List<EventRecord> _delayedEvents = new List<EventRecord>();

  public void AddDelayedEvent(EventRecord eventRec) => this._delayedEvents.Add(eventRec);

  public void ClearDelayedEvents() => this._delayedEvents.Clear();

  public EventRecord[] GetDelayedEvents() => this._delayedEvents.ToArray();
}
