// Decompiled with JetBrains decompiler
// Type: Intermech.ImShape.Server.ImShapeBaseObserver
// Assembly: Intermech.ImShape.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 84375EAE-6601-42D1-857F-8650A0F7FEBA
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ImShape.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ImShape.Server;

internal sealed class ImShapeBaseObserver : LongLifeObject
{
  private ConcurrentDictionary<Guid, HashSet<string>> _sessionToArticleGuids;

  public ImShapeBaseObserver()
  {
    this._sessionToArticleGuids = new ConcurrentDictionary<Guid, HashSet<string>>();
  }

  public void AddRemovedArticles(Guid sessionGuid, ICollection<string> removedAtrGuids)
  {
    HashSet<string> orAdd;
    if (!this._sessionToArticleGuids.TryGetValue(sessionGuid, out orAdd))
      orAdd = this._sessionToArticleGuids.GetOrAdd(sessionGuid, new HashSet<string>());
    lock (orAdd)
    {
      foreach (string removedAtrGuid in (IEnumerable<string>) removedAtrGuids)
        orAdd.Add(removedAtrGuid);
    }
  }

  public string[] TakeRemovedArtIds(Guid sessionGuid)
  {
    HashSet<string> stringSet;
    if (!this._sessionToArticleGuids.TryGetValue(sessionGuid, out stringSet))
      return new string[0];
    lock (stringSet)
    {
      string[] array = new string[stringSet.Count];
      stringSet.CopyTo(array, 0);
      stringSet.Clear();
      return array;
    }
  }
}
