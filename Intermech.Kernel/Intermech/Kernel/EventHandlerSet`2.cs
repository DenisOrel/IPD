// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EventHandlerSet`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

internal class EventHandlerSet<TSender, TEventArgs> : IEventHandlerSet<TSender, TEventArgs>
  where TSender : class
  where TEventArgs : EventArgs
{
  private Dictionary<object, EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper> eventsTable;

  public EventHandlerSet()
  {
    this.eventsTable = new Dictionary<object, EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper>();
  }

  public static IEventHandlerSet<TSender, TEventArgs> CreateSynchronized()
  {
    return EventHandlerSet<TSender, TEventArgs>.AsSynchronized(new EventHandlerSet<TSender, TEventArgs>());
  }

  public static IEventHandlerSet<TSender, TEventArgs> AsSynchronized(
    EventHandlerSet<TSender, TEventArgs> eventHandlerSet)
  {
    return eventHandlerSet != null ? (IEventHandlerSet<TSender, TEventArgs>) new EventHandlerSet<TSender, TEventArgs>.SynchronizedEventHandlerSet(eventHandlerSet) : throw new ArgumentNullException(nameof (eventHandlerSet));
  }

  public void AddHandler(object eventKey, Action<TSender, TEventArgs> handler)
  {
    if (eventKey == null)
      throw new ArgumentNullException(nameof (eventKey));
    if (handler == null)
      throw new ArgumentNullException(nameof (handler));
    EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper handlerSetInvokeHelper;
    if (!this.eventsTable.TryGetValue(eventKey, out handlerSetInvokeHelper))
    {
      handlerSetInvokeHelper = new EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper();
      this.eventsTable.Add(eventKey, handlerSetInvokeHelper);
    }
    handlerSetInvokeHelper.Add(handler);
  }

  public void RemoveHandler(object eventKey, Action<TSender, TEventArgs> handler)
  {
    if (eventKey == null)
      throw new ArgumentNullException(nameof (eventKey));
    if (handler == null)
      throw new ArgumentNullException(nameof (handler));
    EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper handlerSetInvokeHelper;
    if (!this.eventsTable.TryGetValue(eventKey, out handlerSetInvokeHelper))
      return;
    handlerSetInvokeHelper.Remove(handler);
  }

  public void Fire(object eventKey, TSender sender, TEventArgs e)
  {
    if (eventKey == null)
      throw new ArgumentNullException(nameof (eventKey));
    EventHandlerSet<TSender, TEventArgs>.EventHandlerSetInvokeHelper handlerSetInvokeHelper;
    if (!this.eventsTable.TryGetValue(eventKey, out handlerSetInvokeHelper))
      return;
    handlerSetInvokeHelper.Invoke(sender, e);
  }

  private sealed class SynchronizedEventHandlerSet : IEventHandlerSet<TSender, TEventArgs>
  {
    private EventHandlerSet<TSender, TEventArgs> eventHandlerSet;
    private object syncRoot;

    public SynchronizedEventHandlerSet(
      EventHandlerSet<TSender, TEventArgs> eventHandlerSet)
    {
      this.eventHandlerSet = eventHandlerSet;
      this.syncRoot = new object();
    }

    public void AddHandler(object eventKey, Action<TSender, TEventArgs> handler)
    {
      lock (this.syncRoot)
        this.eventHandlerSet.AddHandler(eventKey, handler);
    }

    public void RemoveHandler(object eventKey, Action<TSender, TEventArgs> handler)
    {
      lock (this.syncRoot)
        this.eventHandlerSet.RemoveHandler(eventKey, handler);
    }

    public void Fire(object eventKey, TSender sender, TEventArgs e)
    {
      lock (this.syncRoot)
        this.eventHandlerSet.Fire(eventKey, sender, e);
    }
  }

  private sealed class EventHandlerSetInvokeHelper
  {
    private Action<TSender, TEventArgs> handlerList;

    public void Add(Action<TSender, TEventArgs> handler) => this.handlerList += handler;

    public void Remove(Action<TSender, TEventArgs> handler) => this.handlerList -= handler;

    public void Invoke(TSender sender, TEventArgs e)
    {
      if (this.handlerList == null)
        return;
      this.handlerList(sender, e);
    }
  }
}
