// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADSystemCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует общий кэш для оберток объектов CAD-интерфейса и результатов вызовов "тяжелых" методов CAD-интерфейса.
/// </summary>
public class CADSystemCache
{
  private Dictionary<object, object> items;

  /// <summary>Создает объект.</summary>
  public CADSystemCache() => this.items = new Dictionary<object, object>(32 /*0x20*/);

  public void SetValue(object key, object value)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    this.items[key] = value;
  }

  public TValue GetValue<TValue>(object key, TValue defaultValue)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    object obj;
    return this.items.TryGetValue(key, out obj) ? (TValue) obj : defaultValue;
  }

  public bool TryGetValue<TValue>(object key, out TValue value)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    object obj;
    if (this.items.TryGetValue(key, out obj))
    {
      value = (TValue) obj;
      return true;
    }
    value = default (TValue);
    return false;
  }

  public bool Contains(object key)
  {
    return key != null ? this.items.ContainsKey(key) : throw new ArgumentNullException(nameof (key));
  }
}
