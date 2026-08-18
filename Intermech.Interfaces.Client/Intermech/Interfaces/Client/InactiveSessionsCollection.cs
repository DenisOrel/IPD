// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InactiveSessionsCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Коллекция неактивных пользовательских сессий (т.е. не используемых ни одним потоком и готовых к переиспользованию).
/// Реализация не является thread safe.
/// </summary>
internal sealed class InactiveSessionsCollection
{
  private readonly List<SessionPoolDescriptor> descriptorList;

  public InactiveSessionsCollection(int capacity)
  {
    this.descriptorList = new List<SessionPoolDescriptor>(capacity);
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.descriptorList.Count == 0;
  }

  public List<SessionPoolDescriptor> GetAll(Predicate<SessionPoolDescriptor> predicate = null)
  {
    List<SessionPoolDescriptor> all = new List<SessionPoolDescriptor>();
    for (int index = 0; index < this.descriptorList.Count; ++index)
    {
      SessionPoolDescriptor descriptor = this.descriptorList[index];
      if (predicate == null || predicate(descriptor))
        all.Add(descriptor);
    }
    return all;
  }

  public SessionPoolDescriptor TryGet(SessionPoolThreadKey threadKey, bool isSessionPinningRequired)
  {
    if (this.descriptorList.Count != 0)
    {
      int index = isSessionPinningRequired ? this.FindPinnedSessionIndex(threadKey) : this.FindUnpinnedSessionIndex();
      if (index >= 0)
        return this.descriptorList[index];
    }
    return (SessionPoolDescriptor) null;
  }

  private int FindPinnedSessionIndex(SessionPoolThreadKey threadKey)
  {
    for (int index = this.descriptorList.Count - 1; index >= 0; --index)
    {
      SessionPoolDescriptor descriptor = this.descriptorList[index];
      if (descriptor.OwnerThreadKey != null && descriptor.OwnerThreadKey.Equals(threadKey))
        return index;
    }
    return -1;
  }

  private int FindUnpinnedSessionIndex()
  {
    for (int index = this.descriptorList.Count - 1; index >= 0; --index)
    {
      if (this.descriptorList[index].OwnerThreadKey == null)
        return index;
    }
    return -1;
  }

  public void Add(SessionPoolDescriptor descriptor) => this.descriptorList.Add(descriptor);

  public void Remove(SessionPoolDescriptor descriptor) => this.descriptorList.Remove(descriptor);

  public void Clear() => this.descriptorList.Clear();

  public void EmergencyClear() => this.descriptorList.Clear();
}
