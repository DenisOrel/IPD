// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ObjectTypesArrayFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Проверяет, описывают ли данные в некотором формате тип объекта БД,
/// указанный в условиях фильтра.
/// </summary>
public class ObjectTypesArrayFilter : IDataFormatFilter, ICloneable
{
  private Hashtable _objTypeIDs;

  /// <summary>
  /// Закрытый конструктор, предназначенный для создания клона фильтра
  /// </summary>
  /// <param name="filter"></param>
  private ObjectTypesArrayFilter(ObjectTypesArrayFilter filter)
  {
    this._objTypeIDs = (Hashtable) filter._objTypeIDs.Clone();
  }

  /// <summary>
  /// Конструктор, позволяющий создать фильтр и указать типы объектов,
  /// которые он будет пропускать.
  /// </summary>
  /// <param name="objTypeIDs">Список id-ков типов объектов, пропускаемых фильтром</param>
  public ObjectTypesArrayFilter(int[] objTypeIDs)
  {
    this._objTypeIDs = new Hashtable();
    for (int index = 0; index < objTypeIDs.Length; ++index)
      this._objTypeIDs[(object) objTypeIDs[index]] = (object) 1;
  }

  public bool Join(IDataFormatFilter filter)
  {
    if (!(filter is ObjectTypesArrayFilter))
      return false;
    foreach (DictionaryEntry objTypeId in (filter as ObjectTypesArrayFilter)._objTypeIDs)
      this._objTypeIDs[objTypeId.Key] = !this._objTypeIDs.ContainsKey(objTypeId.Key) ? (object) 1 : (object) ((int) this._objTypeIDs[objTypeId.Key] + 1);
    return true;
  }

  public bool Disjoin(IDataFormatFilter filter)
  {
    if (!(filter is ObjectTypesArrayFilter))
      return false;
    foreach (DictionaryEntry objTypeId in (filter as ObjectTypesArrayFilter)._objTypeIDs)
    {
      if (this._objTypeIDs.ContainsKey(objTypeId.Key))
      {
        int num = (int) this._objTypeIDs[objTypeId.Key] - 1;
        if (num == 0)
          this._objTypeIDs.Remove(objTypeId.Key);
        else
          this._objTypeIDs[objTypeId.Key] = (object) num;
      }
    }
    return true;
  }

  public bool CanPassData(object data)
  {
    return data is IDBObjectTypeID && this._objTypeIDs.ContainsKey((object) (data as IDBObjectTypeID).Value);
  }

  public object Clone() => (object) new ObjectTypesArrayFilter(this);
}
