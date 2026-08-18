// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSessionDeferredEventSource
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSessionDeferredEventSource : IDeferredEventSource
{
  private readonly LateBound<CopyingSession> sessionWrapper;

  public CopyingSessionDeferredEventSource(LateBound<CopyingSession> sessionWrapper)
  {
    this.sessionWrapper = sessionWrapper != null ? sessionWrapper : throw new ArgumentNullException(nameof (sessionWrapper));
  }

  public object GetSender() => (object) this.sessionWrapper.Value;

  public IEnumerable<DeferredEvent> EnumerateDeferredEvents()
  {
    foreach (DBObjectGraphVertex allVertex in (IEnumerable<DBObjectGraphVertex>) this.sessionWrapper.Value.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.DeferredEvents.Count != 0)))
    {
      foreach (DeferredEvent deferredEvent in allVertex.DeferredEvents.Enumerate())
        yield return deferredEvent;
    }
  }

  public void RemoveDeferredEvents(ICollection<DeferredEvent> processedEvents)
  {
    if (processedEvents == null)
      throw new ArgumentNullException(nameof (processedEvents));
    foreach (DBObjectGraphVertex allVertex in (IEnumerable<DBObjectGraphVertex>) this.sessionWrapper.Value.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.DeferredEvents.Count != 0)))
      allVertex.DeferredEvents.RemoveAll(processedEvents);
  }
}
