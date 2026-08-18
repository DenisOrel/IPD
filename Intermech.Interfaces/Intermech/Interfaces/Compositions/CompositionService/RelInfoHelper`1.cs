
// Type: Intermech.Interfaces.Compositions.CompositionService.RelInfoHelper`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>Helper для работы c RelInfoItem</summary>
    /// <remarks></remarks>
    public static class RelInfoHelper<T> where T : RelInfoItem
    {
      /// <summary>
      /// Обновление / загрузка информации о "недостающих" данных объектов
      /// </summary>
      /// <param name="objInfoList"></param>
      /// <param name="loadDataAction"></param>
      /// <param name="session"></param>
      /// <returns></returns>
      private static bool UpdateUnknownData(
        IEnumerable<T> relInfoList,
        Func<IEnumerable<T>, bool> loadRelationDataFunction,
        Func<IEnumerable<ObjInfoItem>, bool> loadObjectDataFunction)
      {
        if (relInfoList == null)
          return false;
        List<T> objList = new List<T>();
        IDictionary<long, ObjInfoItem> dictionary = (IDictionary<long, ObjInfoItem>) new Dictionary<long, ObjInfoItem>();
        foreach (T relInfo in relInfoList)
        {
          if (!RelInfoItem.IsEmpty((RelInfoItem) relInfo))
          {
            if (relInfo.HasEmptyInfo)
              objList.Add(relInfo);
            if (relInfo is RelObjInfoItem relObjInfoItem)
            {
              if ((TypedInfoItem) relObjInfoItem.PartInfo != (TypedInfoItem) null && !relObjInfoItem.PartInfo.HasEmptyInfo)
                dictionary[relObjInfoItem.PartInfo.ObjectID] = relObjInfoItem.PartInfo;
              if ((TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null && !relObjInfoItem.ProjInfo.HasEmptyInfo)
                dictionary[relObjInfoItem.ProjInfo.ObjectID] = relObjInfoItem.ProjInfo;
            }
          }
        }
        if (objList.Count != 0 && !loadRelationDataFunction((IEnumerable<T>) objList))
          return false;
        if (dictionary.Count == 0)
          return true;
        List<ObjInfoItem> objInfoItemList = (List<ObjInfoItem>) new HashedList<ObjInfoItem>();
        foreach (T relInfo in relInfoList)
        {
          if (relInfo is RelObjInfoItem relObjInfoItem)
          {
            ObjInfoItem objInfoItem;
            if ((TypedInfoItem) relObjInfoItem.PartInfo != (TypedInfoItem) null && relObjInfoItem.PartInfo.HasEmptyInfo)
            {
              if (dictionary.TryGetValue(relObjInfoItem.PartInfo.ObjectID, out objInfoItem))
                relObjInfoItem.PartInfo.CopyFrom((TypedInfoItem) objInfoItem);
              else
                objInfoItemList.Add(relObjInfoItem.PartInfo);
            }
            if ((TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null && relObjInfoItem.ProjInfo.HasEmptyInfo)
            {
              if (dictionary.TryGetValue(relObjInfoItem.ProjInfo.ObjectID, out objInfoItem))
                relObjInfoItem.ProjInfo.CopyFrom((TypedInfoItem) objInfoItem);
              else
                objInfoItemList.Add(relObjInfoItem.ProjInfo);
            }
          }
        }
        return objInfoItemList.Count == 0 || loadObjectDataFunction((IEnumerable<ObjInfoItem>) objInfoItemList);
      }

      /// <summary>
      /// Обновление / загрузка информации о "недостающих" типах
      /// </summary>
      /// <param name="relInfoList">Описание объектов</param>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>true - удалось получить информацию о типах для всех объектов</returns>
      public static bool UpdateUnknownTypes(IEnumerable<T> relInfoList, IUserSession session)
      {
        return RelInfoHelper<T>.UpdateUnknownData(relInfoList, (Func<IEnumerable<T>, bool>) (relations2Load => true), (Func<IEnumerable<ObjInfoItem>, bool>) (objects2Load => ObjInfoHelper.UpdateUnknownTypes(objects2Load, session)));
      }

      /// <summary>
      /// Обновление / загрузка "недостающей" информации объектов
      /// </summary>
      /// <param name="relInfoList">Описание объектов</param>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>true - удалось получить информацию для всех объектов</returns>
      public static bool UpdateUnknownInfo(IEnumerable<T> relInfoList, IUserSession session)
      {
        return RelInfoHelper<T>.UpdateUnknownData(relInfoList, (Func<IEnumerable<T>, bool>) (relation2Load => true), (Func<IEnumerable<ObjInfoItem>, bool>) (objects2Load => ObjInfoHelper.UpdateUnknownInfo(objects2Load, session)));
      }
    }
}
