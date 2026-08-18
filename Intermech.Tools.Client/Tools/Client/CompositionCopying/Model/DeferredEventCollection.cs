// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DeferredEventCollection
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class DeferredEventCollection
{
  private List<DeferredEvent> internalList;
  private static readonly List<DeferredEvent> emptyList = new List<DeferredEvent>(0);

  public int Count
  {
    [DebuggerStepThrough] get => !this.IsInternalListCreated() ? 0 : this.internalList.Count;
  }

  public void Add(DeferredEvent deferredEvent)
  {
    if (deferredEvent == null)
      throw new ArgumentNullException(nameof (deferredEvent));
    this.EnsureInternalListCreated();
    this.internalList.Add(deferredEvent);
  }

  public void Add(DeferredEvent deferredEvent, Predicate<DeferredEvent> guard)
  {
    if (deferredEvent == null)
      throw new ArgumentNullException(nameof (deferredEvent));
    if (guard == null)
      throw new ArgumentNullException(nameof (guard));
    this.EnsureInternalListCreated();
    if (!this.internalList.TrueForAll(guard))
      return;
    this.internalList.Add(deferredEvent);
  }

  public void Remove(DeferredEvent deferredEvent)
  {
    if (deferredEvent == null)
      throw new ArgumentNullException(nameof (deferredEvent));
    if (!this.IsInternalListCreated())
      return;
    this.internalList.Remove(deferredEvent);
  }

  public void RemoveAll(ICollection<DeferredEvent> deferredEvents)
  {
    if (deferredEvents == null)
      throw new ArgumentNullException(nameof (deferredEvents));
    if (deferredEvents.Count == 0 || !this.IsInternalListCreated())
      return;
    this.internalList.RemoveAll((Predicate<DeferredEvent>) (x => deferredEvents.Contains(x)));
  }

  internal IEnumerable<DeferredEvent> Enumerate()
  {
    return !this.IsInternalListCreated() ? (IEnumerable<DeferredEvent>) DeferredEventCollection.emptyList : (IEnumerable<DeferredEvent>) this.internalList;
  }

  private bool IsInternalListCreated() => this.internalList != null;

  private void EnsureInternalListCreated()
  {
    if (this.IsInternalListCreated())
      return;
    this.internalList = new List<DeferredEvent>();
  }
}
