// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DeferredEventDispatcher
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Runtime;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class DeferredEventDispatcher
{
  private readonly IDeferredEventSource eventSource;
  private readonly Dictionary<Type, IDeferredEventHandler> handlers;
  private readonly SilentActionInvoker silentActionInvoker;
  private List<IDeferredEventHandler> activeHandlers;
  private HashSet<DeferredEvent> processedEvents;

  public DeferredEventDispatcher(IDeferredEventSource eventSource)
  {
    this.eventSource = eventSource != null ? eventSource : throw new ArgumentNullException(nameof (eventSource));
    this.handlers = new Dictionary<Type, IDeferredEventHandler>();
    this.silentActionInvoker = SilentActionInvoker.Default;
    this.activeHandlers = new List<IDeferredEventHandler>();
    this.processedEvents = new HashSet<DeferredEvent>();
  }

  public void RegisterHandler(Type deferredEventType, IDeferredEventHandler handler)
  {
    if (deferredEventType == (Type) null)
      throw new ArgumentNullException(nameof (deferredEventType));
    this.handlers[deferredEventType] = handler != null ? handler : throw new ArgumentNullException(nameof (handler));
  }

  public void RegisterHandler<T>(DeferredEventHandler<T> handler) where T : DeferredEvent
  {
    this.RegisterHandler(typeof (T), (IDeferredEventHandler) handler);
  }

  public void RaiseAll()
  {
    object sender = this.eventSource.GetSender();
    try
    {
      foreach (DeferredEvent enumerateDeferredEvent in this.eventSource.EnumerateDeferredEvents())
      {
        IDeferredEventHandler deferredEventHandler;
        if (this.handlers.TryGetValue(enumerateDeferredEvent.GetType(), out deferredEventHandler))
        {
          if (!this.activeHandlers.Contains(deferredEventHandler))
          {
            deferredEventHandler.Begin(sender);
            this.activeHandlers.Add(deferredEventHandler);
          }
          deferredEventHandler.Process(sender, enumerateDeferredEvent);
          this.processedEvents.Add(enumerateDeferredEvent);
        }
      }
    }
    finally
    {
      if (this.processedEvents.Count != 0)
      {
        this.eventSource.RemoveDeferredEvents((ICollection<DeferredEvent>) this.processedEvents);
        this.processedEvents.Clear();
      }
      if (this.activeHandlers.Count != 0)
      {
        foreach (IDeferredEventHandler activeHandler in this.activeHandlers)
        {
          IDeferredEventHandler handler = activeHandler;
          this.silentActionInvoker.Invoke((Action) (() => handler.End(sender)));
        }
        this.activeHandlers.Clear();
      }
    }
  }
}
