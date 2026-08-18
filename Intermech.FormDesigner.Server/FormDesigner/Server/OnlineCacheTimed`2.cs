// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.OnlineCacheTimed`2
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class OnlineCacheTimed<TKeyInfo, TFormInfo> : OnlineCacheBase<TKeyInfo, TFormInfo>
{
  protected override OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms Add(
    OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo> key,
    FormInformation fi,
    TFormInfo value)
  {
    OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms cachedForms = base.Add(key, fi, value);
    if (!(cachedForms is OnlineCacheTimed<TKeyInfo, TFormInfo>.CachedFormsTimed cachedFormsTimed))
      return cachedForms;
    long ticks = DateTime.Now.Ticks;
    cachedFormsTimed.LastAccess = ticks;
    return cachedForms;
  }

  protected override OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms CreateCacheValue()
  {
    return (OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms) new OnlineCacheTimed<TKeyInfo, TFormInfo>.CachedFormsTimed(0);
  }

  public override bool GetTypesForms(
    long userId,
    long roleId,
    TKeyInfo value,
    AttributableElements kind,
    out OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms forms)
  {
    int num = base.GetTypesForms(userId, roleId, value, kind, out forms) ? 1 : 0;
    if (!(forms is OnlineCacheTimed<TKeyInfo, TFormInfo>.CachedFormsTimed cachedFormsTimed))
      return num != 0;
    long ticks = DateTime.Now.Ticks;
    cachedFormsTimed.LastAccess = ticks;
    return num != 0;
  }

  public virtual void ClearCache4Time(long tick4Remove)
  {
    if (tick4Remove == 0L)
      return;
    lock (this)
    {
      List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>> keyList = new List<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>();
      foreach (KeyValuePair<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms> keyValuePair in (IEnumerable<KeyValuePair<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>, OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms>>) this._fw)
      {
        if (keyValuePair.Value is OnlineCacheTimed<TKeyInfo, TFormInfo>.CachedFormsTimed cachedFormsTimed && cachedFormsTimed.LastAccess <= tick4Remove)
          keyList.Add(keyValuePair.Key);
      }
      keyList.ForEach((Action<OnlineCacheBase<TKeyInfo, TFormInfo>.Key<TKeyInfo>>) (x => this._fw.Remove(x)));
    }
  }

  internal class CachedFormsTimed : OnlineCacheBase<TKeyInfo, TFormInfo>.CachedForms
  {
    public long LastAccess = DateTime.Now.Ticks;

    public CachedFormsTimed()
    {
    }

    public CachedFormsTimed(int capacity)
      : base(capacity)
    {
    }
  }
}
