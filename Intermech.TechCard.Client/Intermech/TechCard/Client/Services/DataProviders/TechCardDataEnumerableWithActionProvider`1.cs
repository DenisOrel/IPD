// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.TechCardDataEnumerableWithActionProvider`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders;

/// <summary>
/// Провайдер для вызова отдельного действия вместе / вместо исходного провайдера
/// </summary>
internal class TechCardDataEnumerableWithActionProvider<T> : 
  ITechCardDataEnumerableProvider<T>,
  ITechCardDataProvider<IEnumerable<T>>
{
  /// <summary>
  /// 
  /// </summary>
  private bool _dataLoaded;
  /// <summary>
  /// 
  /// </summary>
  private readonly ITechCardDataEnumerableProvider<T> _sourceProvider;
  /// <summary>
  /// 
  /// </summary>
  private readonly Action<ICollection<T>> _action;
  /// <summary>
  /// 
  /// </summary>
  private readonly bool _needCacheData;
  /// <summary>
  /// 
  /// </summary>
  private List<T> _items;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sourceProvider"></param>
  public TechCardDataEnumerableWithActionProvider(
    [CanBeNull] ITechCardDataEnumerableProvider<T> sourceProvider,
    [NotNull] Action<ICollection<T>> action,
    bool needCacheData = true)
  {
    this._sourceProvider = sourceProvider;
    this._action = action;
    this._needCacheData = needCacheData;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<T> Execute()
  {
    if (this._dataLoaded)
      return (IEnumerable<T>) this._items;
    this._items = new List<T>();
    if (this._sourceProvider != null)
      this._items.AddRange(this._sourceProvider.Execute());
    this._action((ICollection<T>) this._items);
    this._dataLoaded = this._needCacheData;
    return (IEnumerable<T>) this._items;
  }
}
