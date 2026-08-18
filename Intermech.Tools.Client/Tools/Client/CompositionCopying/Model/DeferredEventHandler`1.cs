// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DeferredEventHandler`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class DeferredEventHandler<T> : IDeferredEventHandler where T : DeferredEvent
{
  private bool isActive;

  public void Begin(object sender)
  {
    this.DoBegin(sender);
    this.isActive = true;
  }

  protected virtual void DoBegin(object sender)
  {
  }

  public void Process(object sender, DeferredEvent deferredEvent)
  {
    if (deferredEvent == null)
      throw new ArgumentNullException(nameof (deferredEvent));
    this.DoProcess(sender, (T) deferredEvent);
  }

  protected abstract void DoProcess(object sender, T deferredEvent);

  public void End(object sender)
  {
    try
    {
      this.DoEnd(sender);
    }
    finally
    {
      this.isActive = false;
    }
  }

  protected virtual void DoEnd(object sender)
  {
  }
}
