// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ActiveSessionsCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Коллекция активных пользовательских сессий (т.е. выделенных потокам и связанных с этими потоками).
/// Реализация не является thread safe.
/// </summary>
internal sealed class ActiveSessionsCollection
{
  private readonly Dictionary<SessionPoolThreadKey, SessionPoolDescriptor> threadTable;

  public ActiveSessionsCollection(int capacity)
  {
    this.threadTable = new Dictionary<SessionPoolThreadKey, SessionPoolDescriptor>(capacity);
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.threadTable.Count == 0;
  }

  public SessionPoolDescriptor TryGet(SessionPoolThreadKey threadKey)
  {
    SessionPoolDescriptor sessionPoolDescriptor;
    this.threadTable.TryGetValue(threadKey, out sessionPoolDescriptor);
    return sessionPoolDescriptor;
  }

  public void Add(SessionPoolThreadKey threadKey, SessionPoolDescriptor descriptor)
  {
    this.threadTable.Add(threadKey, descriptor);
    descriptor.ThreadKey = threadKey;
  }

  public void Remove(SessionPoolThreadKey threadKey)
  {
    SessionPoolDescriptor sessionPoolDescriptor;
    if (!this.threadTable.TryGetValue(threadKey, out sessionPoolDescriptor))
      return;
    this.threadTable.Remove(threadKey);
    sessionPoolDescriptor.ThreadKey = (SessionPoolThreadKey) null;
  }

  public void Clear()
  {
    foreach (KeyValuePair<SessionPoolThreadKey, SessionPoolDescriptor> keyValuePair in this.threadTable)
      keyValuePair.Value.ThreadKey = (SessionPoolThreadKey) null;
    this.threadTable.Clear();
  }

  public void EmergencyClear() => this.threadTable.Clear();
}
