// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportTypedItemList`1
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Список базовых классов настроек</summary>
[Serializable]
public abstract class XmlExchangeExportTypedItemList<T> : XmlExchangeExportUserItemList<T> where T : XmlExchangeExportTypedItem, new()
{
  /// <summary>Поиск элемента по ид-ру</summary>
  /// <param name="itemId"></param>
  /// <returns></returns>
  public virtual T GetItemByID(int itemId)
  {
    T itemById = default (T);
    foreach (T obj in (List<T>) this)
    {
      if (obj.ID == itemId)
      {
        itemById = obj;
        break;
      }
    }
    return itemById;
  }

  /// <summary>Поиск элемента по Guid</summary>
  /// <param name="typeGuid"></param>
  /// <returns></returns>
  public virtual T GetItemByGuid(Guid typeGuid)
  {
    T itemByGuid = default (T);
    foreach (T obj in (List<T>) this)
    {
      if ((object) obj != null && obj.TypeGuid == typeGuid)
      {
        itemByGuid = obj;
        break;
      }
    }
    return itemByGuid;
  }

  /// <summary>Поиск элемента по ид-ру</summary>
  /// <param name="typeId"></param>
  /// <returns></returns>
  public virtual int GetUserIDByID(int typeId)
  {
    // ISSUE: variable of a boxed type
    __Boxed<T> itemById = (object) this.GetItemByID(typeId);
    return itemById == null ? -1 : itemById.UserID2Int;
  }

  /// <summary>Получение списка ид. типов элементов</summary>
  [Obsolete("Obsoleted. Use LinQ instead", false)]
  public List<int> GetItemTypeIDs()
  {
    List<int> itemTypeIds = new List<int>(this.Count);
    foreach (T obj in (List<T>) this)
      itemTypeIds.Add(obj.ID);
    return itemTypeIds;
  }
}
