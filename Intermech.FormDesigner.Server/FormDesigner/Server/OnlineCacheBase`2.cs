// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.OnlineCacheBase`2
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class OnlineCacheBase<TKeyInfo, TFormInfo>
{
  protected IDictionary<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms> _fw = (IDictionary<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms>) new ConcurrentDictionary<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms>();

  protected virtual void InitializeData()
  {
  }

  protected virtual OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms Add(
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key,
    FormInformation fi,
    TFormInfo value)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms;
    if (!this._fw.TryGetValue(key, out cachedForms))
    {
      cachedForms = new OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms(1);
      this._fw.Add(key, cachedForms);
    }
    CacheHelper.CacheBaseItem<TFormInfo> cacheBaseItem1;
    if (!cachedForms.TryGetValue(fi.ID, out cacheBaseItem1))
    {
      CacheHelper.CacheBaseItem<TFormInfo> cacheBaseItem2 = new CacheHelper.CacheBaseItem<TFormInfo>(fi, value);
      cachedForms.TryAdd(fi.ID, cacheBaseItem2);
    }
    else
      cacheBaseItem1.Value = value;
    return cachedForms;
  }

  protected virtual OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms CreateCacheValue()
  {
    return new OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms(0);
  }

  public IDictionary<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms> CacheData
  {
    get => this._fw;
  }

  public OnlineCacheBase() => this.InitializeData();

  public virtual void Add(
    long userID,
    long roleID,
    TKeyInfo value,
    AttributableElements kind,
    OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms dict)
  {
    if (dict == null || dict.Count <= 0)
      return;
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>(userID, roleID, value, kind);
    foreach (CacheHelper.CacheBaseItem<TFormInfo> cacheBaseItem in (IEnumerable<CacheHelper.CacheBaseItem<TFormInfo>>) dict.Values)
      this.Add(key, cacheBaseItem.FormInfo, cacheBaseItem.Value);
  }

  public virtual void CleanCache(
    long userId,
    long roleId,
    TKeyInfo value,
    AttributableElements kind)
  {
    this._fw.Remove(new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>(userId, roleId, value, kind));
  }

  public virtual void CleanCache() => this._fw.Clear();

  public virtual void CleanCache(long userId)
  {
    if (userId == 0L)
      return;
    List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>> keyList = new List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>();
    foreach (OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key in (IEnumerable<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) this._fw.Keys)
    {
      if (key.UserID == userId)
        keyList.Add(key);
    }
    keyList.ForEach((Action<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) (x => this._fw.Remove(x)));
  }

  public virtual OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms GetTypesForms(
    long userId,
    long roleId,
    TKeyInfo value,
    AttributableElements kind)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms forms;
    return !this.GetTypesForms(userId, roleId, value, kind, out forms) ? this.CreateCacheValue() : forms;
  }

  public virtual bool GetTypesForms(
    long userId,
    long roleId,
    TKeyInfo value,
    AttributableElements kind,
    out OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms forms)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key = new OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>(userId, roleId, value, kind);
    forms = (OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms) null;
    return this._fw.TryGetValue(key, out forms);
  }

  internal class Key<Y>
  {
    public long RoleID { get; private set; }

    public long UserID { get; private set; }

    public Y Value { get; private set; }

    public AttributableElements Kind { get; private set; }

    public Key(long userId, long roleId, Y value, AttributableElements kind)
    {
      this.UserID = userId;
      this.RoleID = roleId;
      this.Value = value;
      this.Kind = kind;
    }

    public override int GetHashCode()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.UserID.GetHashCode());
      stringBuilder.Append(this.RoleID.GetHashCode());
      stringBuilder.Append(this.Value.GetHashCode());
      stringBuilder.Append(this.Kind.GetHashCode());
      return stringBuilder.ToString().GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is OnlineCacheBase<TKeyInfo, TFormInfo>.Key<Y> key))
        return base.Equals(obj);
      return this.UserID == key.UserID && this.RoleID == key.RoleID && this.Value.Equals((object) key.Value) && this.Kind == key.Kind;
    }
  }

  internal class CachedForms : CacheHelper.CacheBaseItems<TFormInfo>
  {
    public CachedForms()
    {
    }

    public CachedForms(int capacity)
      : base(capacity)
    {
    }

    public CachedForms(
      OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms value)
      : base((CacheHelper.CacheBaseItems<TFormInfo>) value)
    {
    }

    public CachedForms(
      Dictionary<long, CacheHelper.CacheBaseItem<TFormInfo>> dictionary)
      : base(dictionary)
    {
    }

    public void Add(long key, CacheHelper.CacheBaseItem<TFormInfo> value)
    {
      this.TryAdd(key, value);
    }
  }
}
