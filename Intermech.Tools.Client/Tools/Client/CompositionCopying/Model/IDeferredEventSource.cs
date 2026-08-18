// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.IDeferredEventSource
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal interface IDeferredEventSource
{
  object GetSender();

  IEnumerable<DeferredEvent> EnumerateDeferredEvents();

  void RemoveDeferredEvents(ICollection<DeferredEvent> processedEvents);
}
