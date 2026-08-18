// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.OnlineCache`2
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class OnlineCache<TKeyInfo, TFormInfo> : OnlineCacheBase<TKeyInfo, TFormInfo>
{
  protected CacheHelper.CacheBaseItems<OnlineCache<TKeyInfo, TFormInfo>.FormData> _bw = new CacheHelper.CacheBaseItems<OnlineCache<TKeyInfo, TFormInfo>.FormData>();

  protected override OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms Add(
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key,
    FormInformation fi,
    TFormInfo value)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms = base.Add(key, fi, value);
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem1;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData;
    if (!this._bw.TryGetValue(fi.ID, out cacheBaseItem1))
    {
      formData = new OnlineCache<TKeyInfo, TFormInfo>.FormData(1);
      CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem2 = new CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData>(fi, formData);
      this._bw.TryAdd(fi.ID, cacheBaseItem2);
    }
    else
      formData = cacheBaseItem1.Value;
    if (formData.ContainsKey(key))
      return cachedForms;
    formData.TryAdd(key, value);
    return cachedForms;
  }

  public void ChangeCaptionForm(long userId, long formId, string caption)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (this._bw.TryGetValue(formId, out cacheBaseItem))
      cacheBaseItem.FormInfo.Caption = caption;
    if (cacheBaseItem == null || cacheBaseItem.Value == null)
      return;
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in (IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) cacheBaseItem.Value.Keys)
    {
      OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
      if (this._fw.TryGetValue(key, out cachedForms) && cachedForms != null && cachedForms.ContainsKey(formId))
        cachedForms[formId].FormInfo.Caption = caption;
    }
  }

  public void ChangeCheckOutInfoBy(long userId, FormInformation fi)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (this._bw.TryGetValue(fi.ID, out cacheBaseItem))
      cacheBaseItem.FormInfo.CheckOutBy = fi.CheckOutBy;
    if (cacheBaseItem == null || cacheBaseItem.Value == null)
      return;
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in (IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) cacheBaseItem.Value.Keys)
    {
      OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
      if (this._fw.TryGetValue(key, out cachedForms) && cachedForms != null && cachedForms.ContainsKey(fi.ID))
        cachedForms[fi.ID].FormInfo.CheckOutBy = fi.CheckOutBy;
    }
  }

  public void ChangeConditionForm(long userId, FormInformation fi)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (this._bw.TryGetValue(fi.ID, out cacheBaseItem))
    {
      cacheBaseItem.FormInfo.FormulaData = fi.FormulaData;
      cacheBaseItem.FormInfo.HasFormula = fi.HasFormula;
    }
    if (cacheBaseItem == null || cacheBaseItem.Value == null)
      return;
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in (IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) cacheBaseItem.Value.Keys)
    {
      OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
      if (this._fw.TryGetValue(key, out cachedForms) && cachedForms != null && cachedForms.ContainsKey(fi.ID))
      {
        cachedForms[fi.ID].FormInfo.FormulaData = fi.FormulaData;
        cachedForms[fi.ID].FormInfo.HasFormula = fi.HasFormula;
      }
    }
  }

  public void ChangeFormInfo(long userId, FormInformation fi, TFormInfo value)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (!this._bw.TryGetValue(fi.ID, out cacheBaseItem) || cacheBaseItem == null)
      return;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
    if (formData == null)
      return;
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in new List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>((IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) formData.Keys))
    {
      if (key.UserID == userId)
        formData[key] = this._fw[key][fi.ID].Value = value;
    }
  }

  public void ChangeFormInfo(long userId, long formId, int typeId, TFormInfo value)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (!this._bw.TryGetValue(formId, out cacheBaseItem) || cacheBaseItem == null)
      return;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
    if (formData == null)
      return;
    List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>> keyList = new List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>((IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) formData.Keys);
    if (typeId != -1)
    {
      foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in keyList)
      {
        if (key.UserID == userId && Convert.ToInt32((object) key.Value) == typeId)
          formData[key] = this._fw[key][formId].Value = value;
      }
    }
    else
    {
      foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in keyList)
      {
        if (key.UserID == userId)
          formData[key] = this._fw[key][formId].Value = value;
      }
    }
  }

  public override void CleanCache(
    long userId,
    long roleId,
    TKeyInfo value,
    AttributableElements kind)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key1 = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>(userId, roleId, value, kind);
    if (!this._fw.ContainsKey(key1))
      return;
    foreach (long key2 in (IEnumerable<long>) this._fw[key1].Keys)
    {
      CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
      if (this._bw.TryGetValue(key2, out cacheBaseItem))
      {
        OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
        formData.TryRemove(key1, out TFormInfo _);
        if (formData.Count <= 0)
          this._bw.TryRemove(key2, out cacheBaseItem);
      }
    }
    this._fw.Remove(key1);
  }

  public virtual void CleanCache(long userId, TKeyInfo value, FormInformation fi)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (!this._bw.TryGetValue(fi.ID, out cacheBaseItem))
      return;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[] array = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[formData.Keys.Count];
    formData.Keys.CopyTo(array, 0);
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in array)
    {
      if (key.UserID == userId && key.Value.Equals((object) value))
      {
        OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
        if (!this._fw.TryGetValue(key, out cachedForms))
          break;
        formData.TryRemove(key, out TFormInfo _);
        if (formData.Count == 0)
          this._bw.TryRemove(fi.ID, out cacheBaseItem);
        cachedForms.TryRemove(fi.ID, out CacheHelper.CacheBaseItem<TFormInfo> _);
        if (cachedForms.Count == 0)
          this._fw.Remove(key);
      }
    }
  }

  public virtual void CleanCache(long userId, FormInformation fi)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (!this._bw.TryGetValue(fi.ID, out cacheBaseItem))
      return;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[] array = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[formData.Keys.Count];
    formData.Keys.CopyTo(array, 0);
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in array)
    {
      if (key.UserID == userId)
      {
        OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
        if (!this._fw.TryGetValue(key, out cachedForms))
          break;
        formData.TryRemove(key, out TFormInfo _);
        if (formData.Count == 0)
          this._bw.TryRemove(fi.ID, out cacheBaseItem);
        cachedForms.TryRemove(fi.ID, out CacheHelper.CacheBaseItem<TFormInfo> _);
        if (cachedForms.Count == 0)
          this._fw.Remove(key);
      }
    }
  }

  public override void CleanCache()
  {
    base.CleanCache();
    this._bw.Clear();
  }

  public override void CleanCache(long userId)
  {
    if (userId == 0L)
      return;
    List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>> keyList = new List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>();
    List<long> longList1 = new List<long>();
    foreach (KeyValuePair<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms> keyValuePair in (IEnumerable<KeyValuePair<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms>>) this._fw)
    {
      if (keyValuePair.Key.UserID == userId)
      {
        keyList.Add(keyValuePair.Key);
        longList1.AddRange((IEnumerable<long>) keyValuePair.Value.Keys);
      }
    }
    keyList.ForEach((Action<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) (x => this._fw.Remove(x)));
    List<long> longList2 = new List<long>(this._bw.Count);
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheItem;
    foreach (long key in longList1)
    {
      if (this._bw.TryGetValue(key, out cacheItem) && cacheItem != null)
      {
        OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheItem.Value;
        keyList.ForEach((Action<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) (x => formData.TryRemove(x, out TFormInfo _)));
        if (cacheItem.Value.Count == 0)
          longList2.Add(key);
      }
    }
    longList2.ForEach((Action<long>) (x => this._bw.TryRemove(x, out cacheItem)));
  }

  public virtual void Remove(FormInformation fi)
  {
    CacheHelper.CacheBaseItem<OnlineCache<TKeyInfo, TFormInfo>.FormData> cacheBaseItem;
    if (!this._bw.TryGetValue(fi.ID, out cacheBaseItem) || cacheBaseItem == null)
      return;
    OnlineCache<TKeyInfo, TFormInfo>.FormData formData = cacheBaseItem.Value;
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[] array = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>[formData.Keys.Count];
    formData.Keys.CopyTo(array, 0);
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in array)
    {
      OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
      if (this._fw.TryGetValue(key, out cachedForms) && cachedForms != null)
      {
        cachedForms.TryRemove(fi.ID, out CacheHelper.CacheBaseItem<TFormInfo> _);
        if (cachedForms.Count <= 0)
          this._fw.Remove(key);
      }
    }
    this._bw.TryRemove(fi.ID, out cacheBaseItem);
  }

  public class FormData : 
    ConcurrentDictionary<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, TFormInfo>
  {
    public FormData()
    {
    }

    public FormData(int capacity)
    {
    }
  }
}
