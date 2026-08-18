
// Type: Intermech.Interfaces.Compositions.SomeTypedInfoHelper`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Threading;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Generic helper для работы c классами TypedInfoItem</summary>
    public class SomeTypedInfoHelper<T> where T : TypedInfoItem, new()
    {
      /// <summary>Запись для поиска по спискам</summary>
      private static T _searchRec = new T();

      /// <summary>Получение кэша вида: ид.  -&gt; ид. типа</summary>
      /// <param name="typedInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static Dictionary<long, int> GetItemCache(IEnumerable<T> typedInfoList)
      {
        if (typedInfoList == null)
          return (Dictionary<long, int>) null;
        Dictionary<long, int> itemCache = typedInfoList is ICollection<T> objs ? new Dictionary<long, int>(objs.Count) : new Dictionary<long, int>();
        foreach (T typedInfo in typedInfoList)
          itemCache[typedInfo.ItemID] = typedInfo.ItemTypeID;
        return itemCache;
      }

      /// <summary>
      /// Получение отдельных списков ид-ров версий объектов и их типов
      /// </summary>
      /// <remarks>В основном предназначен для уведомления навигатора</remarks>
      /// <param name="typedInfoList">Перечень описаний объектов</param>
      /// <param name="itemIdList"></param>
      /// <param name="itemTypeList"></param>
      /// <returns></returns>
      public static bool GetItemCache(
        IEnumerable<T> typedInfoList,
        out List<long> itemIdList,
        out List<int> itemTypeList)
      {
        itemIdList = (List<long>) null;
        itemTypeList = (List<int>) null;
        if (typedInfoList == null)
          return false;
        int count = typedInfoList is ICollection<T> objs ? objs.Count : 0;
        itemIdList = new List<long>(count);
        itemTypeList = new List<int>(count);
        foreach (T typedInfo in typedInfoList)
        {
          itemIdList.Add(typedInfo.ItemID);
          itemTypeList.Add(typedInfo.ItemTypeID);
        }
        return true;
      }

      /// <summary>
      /// Получение кэша вида: ид. типа -&gt; список ид. версий
      /// </summary>
      /// <remarks></remarks>
      /// <param name="typedInfoList"></param>
      /// <returns></returns>
      public static Dictionary<int, List<long>> GetItemTypeCache(IEnumerable<T> typedInfoList)
      {
        Dictionary<int, List<long>> itemTypeCache = new Dictionary<int, List<long>>();
        if (typedInfoList == null)
          return itemTypeCache;
        foreach (T typedInfo in typedInfoList)
        {
          List<long> longList;
          if (!itemTypeCache.TryGetValue(typedInfo.ItemTypeID, out longList))
          {
            longList = new List<long>();
            itemTypeCache.Add(typedInfo.ItemTypeID, longList);
          }
          longList.Add(typedInfo.ItemID);
        }
        return itemTypeCache;
      }

      /// <summary>Получение списка ид.-ров</summary>
      /// <param name="typedInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static List<long> GetItemIDs(IEnumerable<T> typedInfoList)
      {
        if (typedInfoList == null)
          return new List<long>();
        List<long> itemIds = new List<long>(typedInfoList is ICollection<T> objs ? objs.Count : 0);
        foreach (T typedInfo in typedInfoList)
          itemIds.Add(typedInfo.ItemID);
        return itemIds;
      }

      /// <summary>Получение списка типов</summary>
      /// <param name="typedInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static List<int> GetItemTypes(IEnumerable<T> typedInfoList)
      {
        if (typedInfoList == null)
          return new List<int>();
        List<int> list = new List<int>(typedInfoList is ICollection<T> objs ? objs.Count : 0);
        foreach (T typedInfo in typedInfoList)
          list.Add(typedInfo.ItemTypeID);
        GenericListHelper.MakeUnique<int>(list);
        return list;
      }

      /// <summary>Получение перечня описаний</summary>
      /// <param name="itemIDs">Список ид. версий объектов / связей</param>
      /// <param name="makeUnique"></param>
      /// <returns></returns>
      public static List<T> GetItemInfoList(IEnumerable<long> itemIDs, bool makeUnique = true)
      {
        if (itemIDs == null)
          return new List<T>();
        if (!(itemIDs is List<long> longList))
          longList = new List<long>(itemIDs);
        List<long> list = longList;
        if (makeUnique)
          GenericListHelper.MakeUnique<long>(list);
        List<T> itemInfoList = new List<T>(list.Count);
        foreach (long num in list)
        {
          T obj1 = new T();
          obj1.ItemID = num;
          T obj2 = obj1;
          itemInfoList.Add(obj2);
        }
        return itemInfoList;
      }

      /// <summary>Получение перечня описаний</summary>
      /// <param name="itemIDs">Список ид. версий, и их соотв. типы</param>
      /// <returns></returns>
      public static List<T> GetItemInfoList(IDictionary<long, int> itemIDs)
      {
        if (itemIDs == null)
          return new List<T>();
        List<T> itemInfoList = new List<T>(itemIDs.Count);
        foreach (KeyValuePair<long, int> itemId in (IEnumerable<KeyValuePair<long, int>>) itemIDs)
        {
          T obj1 = new T();
          obj1.ItemID = itemId.Key;
          obj1.ItemTypeID = itemId.Value;
          T obj2 = obj1;
          itemInfoList.Add(obj2);
        }
        return itemInfoList;
      }

      /// <summary>Получение перечня описаний</summary>
      /// <param name="typedInfoList">Список описаний</param>
      /// <returns></returns>
      public static List<T> GetItemInfoList(IEnumerable<TypedInfoItem> typedInfoList)
      {
        if (typedInfoList == null)
          return new List<T>();
        List<T> itemInfoList = new List<T>(typedInfoList is ICollection<T> objs ? objs.Count : 0);
        foreach (TypedInfoItem typedInfo in typedInfoList)
        {
          T obj1;
          if (typedInfo is T obj3)
          {
            obj1 = obj3;
          }
          else
          {
            T obj2 = new T();
            obj2.ItemID = typedInfo.ItemID;
            obj2.ItemTypeID = typedInfo.ItemTypeID;
            obj1 = obj2;
          }
          itemInfoList.Add(obj1);
        }
        return itemInfoList;
      }

      /// <summary>Поиск объекта в списке</summary>
      /// <param name="typedInfoList"></param>
      /// <param name="itemId"></param>
      /// <returns></returns>
      public static int IndexOf(List<T> typedInfoList, long itemId)
      {
        T obj = Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, default (T));
        try
        {
          if ((TypedInfoItem) obj == (TypedInfoItem) null)
            obj = new T();
          obj.ItemID = itemId;
          return typedInfoList.IndexOf(obj);
        }
        finally
        {
          Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, obj);
        }
      }

      /// <summary>Поиск объекта в списке</summary>
      /// <param name="typedInfoList"></param>
      /// <param name="itemId"></param>
      /// <returns></returns>
      public static int BinarySearch(List<T> typedInfoList, long itemId)
      {
        T obj = Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, default (T));
        try
        {
          if ((TypedInfoItem) obj == (TypedInfoItem) null)
            obj = new T();
          obj.ItemID = itemId;
          return typedInfoList.BinarySearch(obj);
        }
        finally
        {
          Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, obj);
        }
      }

      /// <summary>Удаление дубликатов, пустых записей</summary>
      /// <param name="typedInfoList"></param>
      /// <returns></returns>
      public static List<T> RemoveDuplicateEmpty(List<T> typedInfoList)
      {
        if (typedInfoList == null)
          return new List<T>();
        GenericListHelper.MakeUnique<T>(typedInfoList);
        T obj = Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, default (T));
        try
        {
          if ((TypedInfoItem) obj == (TypedInfoItem) null)
            obj = new T();
          obj.ItemID = 0L;
          typedInfoList.Remove(obj);
          obj.ItemID = 0L;
          typedInfoList.Remove(obj);
        }
        finally
        {
          Interlocked.Exchange<T>(ref SomeTypedInfoHelper<T>._searchRec, obj);
        }
        return typedInfoList;
      }
    }
}
