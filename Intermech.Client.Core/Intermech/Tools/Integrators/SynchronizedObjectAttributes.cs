
// Type: Intermech.Tools.Integrators.SynchronizedObjectAttributes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Memoization;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Tools.Integrators;

public abstract class SynchronizedObjectAttributes : ISynchronizedObjectAttributes
{
  private readonly IIntegratorSettingsService service;
  private readonly StateMonitorCacheGuard cacheGuard;
  private readonly Dictionary<SynchronizedObjectAttributes.CacheKey, ICollection<StringKey>> cache;

  protected SynchronizedObjectAttributes(IIntegratorSettingsService settingsService)
  {
    this.service = settingsService != null ? settingsService : throw new ArgumentNullException(nameof (settingsService));
    this.cacheGuard = new StateMonitorCacheGuard((IStateMonitor) new CompositeStateMonitor(new IStateMonitor[2]
    {
      (IStateMonitor) MetadataResolvers.ChangeMonitor,
      settingsService.GetSettingsStateMonitor()
    }));
    this.cacheGuard.ResetCache += new EventHandler(this.OnResetCache);
    this.cache = new Dictionary<SynchronizedObjectAttributes.CacheKey, ICollection<StringKey>>();
  }

  private void OnResetCache(object sender, EventArgs e) => this.cache.Clear();

  public ICollection<StringKey> GetAttributes() => this.GetAttributes(false);

  public ICollection<StringKey> GetAttributes(bool dbOnly) => this.GetAttributes(-1, dbOnly);

  public ICollection<StringKey> GetAttributes(int objectType, bool dbOnly)
  {
    lock (this.Service.Integrator.SyncRoot)
    {
      this.cacheGuard.CheckCache();
      SynchronizedObjectAttributes.CacheKey key = new SynchronizedObjectAttributes.CacheKey(objectType, dbOnly);
      ICollection<StringKey> attributes;
      if (!this.cache.TryGetValue(key, out attributes))
      {
        List<StringKey> stringKeyList;
        if (objectType == -1)
          stringKeyList = (List<StringKey>) null;
        else
          stringKeyList = DBAttributeHelper.GetAttributeLayout((IDBAttributableTypeRef) new DirectObjectAttributesRef(objectType), RequiredModes.AutoRequired, RequiredModes.Auto, RequiredModes.Manual);
        List<StringKey> objTypeAttrs = stringKeyList;
        OrderedList<StringKey> orderedList1 = new OrderedList<StringKey>((IEnumerable<StringKey>) this.GetUserDefinedAttributes(), (IComparer<StringKey>) Comparer<StringKey>.Default);
        if (objTypeAttrs != null)
          CollectionUtils.RemoveAll<StringKey>((IList<StringKey>) orderedList1, (Predicate<StringKey>) (attrKey => !objTypeAttrs.Contains(attrKey)));
        this.FilterUserDefinedAttributes((ICollection<StringKey>) orderedList1, objectType, dbOnly);
        OrderedList<StringKey> orderedList2 = new OrderedList<StringKey>((IEnumerable<StringKey>) this.GetPredefinedAttributes(), (IComparer<StringKey>) Comparer<StringKey>.Default);
        if (objTypeAttrs != null)
          CollectionUtils.RemoveAll<StringKey>((IList<StringKey>) orderedList2, (Predicate<StringKey>) (attrKey => !objTypeAttrs.Contains(attrKey)));
        orderedList1.AddRange<StringKey>((IEnumerable<StringKey>) orderedList2);
        if (!dbOnly)
          orderedList1.AddRange<StringKey>((IEnumerable<StringKey>) this.GetVirtualAttributes());
        attributes = (ICollection<StringKey>) new ReadOnlyCollection<StringKey>((IList<StringKey>) orderedList1);
        this.cache.Add(key, attributes);
      }
      return attributes;
    }
  }

  protected virtual void FilterUserDefinedAttributes(
    ICollection<StringKey> list,
    int objectType,
    bool dbOnly)
  {
  }

  protected virtual ICollection<StringKey> GetPredefinedAttributes()
  {
    return (ICollection<StringKey>) new OrderedList<StringKey>();
  }

  protected virtual ICollection<StringKey> GetUserDefinedAttributes()
  {
    return (ICollection<StringKey>) new OrderedList<StringKey>();
  }

  protected virtual ICollection<StringKey> GetVirtualAttributes()
  {
    return (ICollection<StringKey>) new OrderedList<StringKey>();
  }

  protected IIntegratorSettingsService Service => this.service;

  private sealed class CacheKey
  {
    private readonly int objectType;
    private readonly bool dbOnly;

    public CacheKey(int objectType, bool dbOnly)
    {
      this.objectType = objectType;
      this.dbOnly = dbOnly;
    }

    public override int GetHashCode()
    {
      int objectType = this.objectType;
      if (this.dbOnly)
        objectType ^= 16842752 /*0x01010000*/;
      return objectType;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is SynchronizedObjectAttributes.CacheKey cacheKey))
        return base.Equals(obj);
      return cacheKey.objectType == this.objectType && cacheKey.dbOnly == this.dbOnly;
    }
  }
}
