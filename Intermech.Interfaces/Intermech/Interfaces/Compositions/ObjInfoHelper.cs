
// Type: Intermech.Interfaces.Compositions.ObjInfoHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Helper для работы c ObjInfoItem</summary>
    /// <remarks></remarks>
    public class ObjInfoHelper : SomeTypedInfoHelper<ObjInfoItem>
    {
      /// <summary>
      /// Получение кэша вида: ид. версии объекта -&gt; ид. типа объекта
      /// </summary>
      /// <param name="objInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static Dictionary<long, int> GetObjectCache(IEnumerable<ObjInfoItem> objInfoList)
      {
        return objInfoList == null ? (Dictionary<long, int>) null : SomeTypedInfoHelper<ObjInfoItem>.GetItemCache((IEnumerable<ObjInfoItem>) objInfoList.ToArray<ObjInfoItem>());
      }

      /// <summary>Получение списка ид. версий объектов</summary>
      /// <param name="objInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static List<long> GetObjectIDs(IEnumerable<ObjInfoItem> objInfoList)
      {
        return objInfoList == null ? new List<long>() : SomeTypedInfoHelper<ObjInfoItem>.GetItemIDs((IEnumerable<ObjInfoItem>) objInfoList.ToArray<ObjInfoItem>());
      }

      /// <summary>Получение списка типов объектов</summary>
      /// <param name="objInfoList">Перечень описаний объектов</param>
      /// <returns></returns>
      public static List<int> GetObjectTypes(IEnumerable<ObjInfoItem> objInfoList)
      {
        return objInfoList == null ? new List<int>() : SomeTypedInfoHelper<ObjInfoItem>.GetItemTypes((IEnumerable<ObjInfoItem>) objInfoList.ToArray<ObjInfoItem>());
      }

      /// <summary>
      /// Получение кэша вида: ид. типа объекта -&gt; список ид. версий объекта
      /// </summary>
      /// <remarks></remarks>
      /// <param name="objInfoList"></param>
      /// <returns></returns>
      public static Dictionary<int, List<long>> GetObjectTypeCache(IEnumerable<ObjInfoItem> objInfoList)
      {
        return SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache(objInfoList);
      }

      /// <summary>Получение перечня описаний объектов</summary>
      /// <param name="objectIDs">Список ид. версий объектов</param>
      /// <returns></returns>
      public static List<ObjInfoItem> GetObjectInfoList(IEnumerable<long> objectIDs)
      {
        return SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList(objectIDs);
      }

      /// <summary>Получение перечня описаний объектов</summary>
      /// <param name="typedInfoList">Список ид. версий объектов</param>
      /// <returns></returns>
      public static List<ObjInfoItem> GetObjectInfoList(IEnumerable<TypedInfoItem> typedInfoList)
      {
        return SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList(typedInfoList);
      }

      /// <summary>Получение перечня описаний объектов</summary>
      /// <param name="objectIDs">Список ид. версий объектов, и их соотв. типы</param>
      /// <returns></returns>
      public static List<ObjInfoItem> GetObjectInfoList(IDictionary<long, int> objectIDs)
      {
        return SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList(objectIDs);
      }

      /// <summary>
      /// Обновление / загрузка информации о "недостающих" типах объектов
      /// </summary>
      /// <param name="objInfoItem">Описание объекта</param>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>true - удалось получить информацию о типах для всех объектов</returns>
      public static bool UpdateUnknownType(ObjInfoItem objInfoItem, IUserSession session)
      {
        if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
          return false;
        return ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>(1)
        {
          objInfoItem
        }, session);
      }

      /// <summary>
      /// Обновление / загрузка информации о "недостающих" данных объектов
      /// </summary>
      /// <param name="objInfoList"></param>
      /// <param name="loadDataAction"></param>
      /// <param name="session"></param>
      /// <returns></returns>
      private static bool UpdateUnknownData(
        IEnumerable<ObjInfoItem> objInfoList,
        Func<IEnumerable<ObjInfoItem>, List<ObjInfoItem>> loadDataFunction,
        IUserSession session)
      {
        if (objInfoList == null || session == null)
          return false;
        List<ObjInfoItem> objInfoItemList1 = new List<ObjInfoItem>();
        foreach (ObjInfoItem objInfo in objInfoList)
        {
          if (!ObjInfoItem.IsEmpty((ITypedInfoItem) objInfo) && objInfo.HasEmptyInfo)
            objInfoItemList1.Add(objInfo);
        }
        if (objInfoItemList1.Count == 0)
          return true;
        List<ObjInfoItem> objInfoItemList2 = loadDataFunction((IEnumerable<ObjInfoItem>) objInfoItemList1);
        if (objInfoItemList2 != null && objInfoItemList2.Any<ObjInfoItem>())
        {
          GenericListHelper.MakeUnique<ObjInfoItem>(objInfoItemList2);
          foreach (ObjInfoItem objInfoItem in objInfoItemList1)
          {
            int index = objInfoItemList2.BinarySearch(objInfoItem);
            if (index >= 0)
              objInfoItem.CopyFrom((TypedInfoItem) objInfoItemList2[index]);
          }
        }
        return true;
      }

      /// <summary>
      /// Обновление / загрузка информации о "недостающих" типах объектов
      /// </summary>
      /// <param name="objInfoList">Описание объектов</param>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>true - удалось получить информацию о типах для всех объектов</returns>
      public static bool UpdateUnknownTypes(IEnumerable<ObjInfoItem> objInfoList, IUserSession session)
      {
        return ObjInfoHelper.UpdateUnknownData(objInfoList, (Func<IEnumerable<ObjInfoItem>, List<ObjInfoItem>>) (items2Load => ServiceUtils.GetService<ITypedInfoService>((object) session, true).UpdateUnknownTypes(items2Load, (object) session.SessionGUID)), session);
      }

      /// <summary>
      /// Обновление / загрузка "недостающей" информации об объектов
      /// </summary>
      /// <param name="objInfoList">Описание объектов</param>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>true - удалось получить информацию для всех объектов</returns>
      public static bool UpdateUnknownInfo(IEnumerable<ObjInfoItem> objInfoList, IUserSession session)
      {
        return ObjInfoHelper.UpdateUnknownData(objInfoList, (Func<IEnumerable<ObjInfoItem>, List<ObjInfoItem>>) (items2Load => ServiceUtils.GetService<ITypedInfoService>((object) session, true).UpdateUnknownInfo(items2Load, (object) session.SessionGUID)), session);
      }
    }
}
