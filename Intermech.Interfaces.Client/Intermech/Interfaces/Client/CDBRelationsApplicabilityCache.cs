// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBRelationsApplicabilityCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует кэш информации о взаимосвязях между типами объектов.
/// </summary>
public static class CDBRelationsApplicabilityCache
{
  /// <summary>
  /// Коллекция пар значений [(MyCompositeKey)Составной ключ] = [(DataTable)таблица применимостей]
  /// </summary>
  private static Dictionary<MyCompositeKey, DataTable> _cacheItems = new Dictionary<MyCompositeKey, DataTable>();

  /// <summary>Принудительно очищает кэш.</summary>
  public static void Reset()
  {
    lock (CDBRelationsApplicabilityCache._cacheItems)
      CDBRelationsApplicabilityCache._cacheItems.Clear();
  }

  internal static DataTable TryGet(MyCompositeKey key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    lock (CDBRelationsApplicabilityCache._cacheItems)
    {
      DataTable dataTable;
      return CDBRelationsApplicabilityCache._cacheItems.TryGetValue(key, out dataTable) ? dataTable : (DataTable) null;
    }
  }

  internal static void Update(MyCompositeKey key, DataTable table)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    lock (CDBRelationsApplicabilityCache._cacheItems)
    {
      if (CDBRelationsApplicabilityCache._cacheItems.Count >= 150)
        CDBRelationsApplicabilityCache._cacheItems.Clear();
      CDBRelationsApplicabilityCache._cacheItems[key] = table;
    }
  }

  internal static void Remove(MyCompositeKey key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    lock (CDBRelationsApplicabilityCache._cacheItems)
      CDBRelationsApplicabilityCache._cacheItems.Remove(key);
  }
}
