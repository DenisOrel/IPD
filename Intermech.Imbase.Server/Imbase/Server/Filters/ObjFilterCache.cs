// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ObjFilterCache
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ObjFilterCache
{
  private readonly object _locker = new object();
  private readonly IDictionary<long, ObjFilterCacheItem> _id2ItemCache;
  private readonly IDictionary<int, ObjFilterCacheItemList> _type2Items;

  public ObjFilterCache()
  {
    this._id2ItemCache = (IDictionary<long, ObjFilterCacheItem>) new ConcurrentDictionary<long, ObjFilterCacheItem>();
    this._type2Items = (IDictionary<int, ObjFilterCacheItemList>) new ConcurrentDictionary<int, ObjFilterCacheItemList>();
  }

  public void AddItem(ObjFilterCacheItem cacheItem)
  {
    if (cacheItem == null || cacheItem.Info == null || this._id2ItemCache.ContainsKey(cacheItem.Info.ObjectID))
      return;
    lock (this._locker)
    {
      this._id2ItemCache[cacheItem.Info.ObjectID] = cacheItem;
      ObjFilterCacheItemList filterCacheItemList;
      if (!this._type2Items.TryGetValue(cacheItem.Info.RefObjTypeID, out filterCacheItemList))
      {
        filterCacheItemList = new ObjFilterCacheItemList();
        this._type2Items[cacheItem.Info.RefObjTypeID] = filterCacheItemList;
      }
      filterCacheItemList.Add(cacheItem);
    }
  }

  public void RemoveItem(ObjFilterCacheItem cacheItem)
  {
    if (cacheItem == null)
      return;
    lock (this._locker)
    {
      if (cacheItem.Info != null)
      {
        this._id2ItemCache.Remove(cacheItem.Info.ObjectID);
        ObjFilterCacheItemList filterCacheItemList;
        if (!this._type2Items.TryGetValue(cacheItem.Info.RefObjTypeID, out filterCacheItemList))
          return;
        filterCacheItemList.Remove(cacheItem);
      }
      else
      {
        List<long> longList = new List<long>(1);
        foreach (KeyValuePair<long, ObjFilterCacheItem> keyValuePair in (IEnumerable<KeyValuePair<long, ObjFilterCacheItem>>) this._id2ItemCache)
        {
          if (keyValuePair.Value == null)
            longList.Add(keyValuePair.Key);
          else if (keyValuePair.Value == cacheItem)
            longList.Add(keyValuePair.Key);
        }
        foreach (long key in longList)
          this._id2ItemCache.Remove(key);
        foreach (List<ObjFilterCacheItem> objFilterCacheItemList in (IEnumerable<ObjFilterCacheItemList>) this._type2Items.Values)
          objFilterCacheItemList.Remove(cacheItem);
      }
    }
  }

  public ObjFilterCacheItem GetItem(long objectId)
  {
    ObjFilterCacheItem objFilterCacheItem;
    this._id2ItemCache.TryGetValue(objectId, out objFilterCacheItem);
    return objFilterCacheItem;
  }

  public IEnumerable<ObjFilterCacheItem> GetItems()
  {
    return (IEnumerable<ObjFilterCacheItem>) this._id2ItemCache.Values;
  }

  public void Load(IEnumerable<ObjFilterCacheItem> items)
  {
    lock (this._locker)
    {
      this._type2Items.Clear();
      this._id2ItemCache.Clear();
      if (items == null)
        return;
      foreach (ObjFilterCacheItem objFilterCacheItem in items)
      {
        this._id2ItemCache[objFilterCacheItem.Info.ObjectID] = objFilterCacheItem;
        ObjFilterCacheItemList filterCacheItemList;
        if (!this._type2Items.TryGetValue(objFilterCacheItem.Info.RefObjTypeID, out filterCacheItemList))
        {
          filterCacheItemList = new ObjFilterCacheItemList();
          this._type2Items[objFilterCacheItem.Info.RefObjTypeID] = filterCacheItemList;
        }
        filterCacheItemList.Add(objFilterCacheItem);
      }
    }
  }
}
