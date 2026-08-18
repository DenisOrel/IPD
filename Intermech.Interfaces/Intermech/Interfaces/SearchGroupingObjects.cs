
// Type: Intermech.Interfaces.SearchGroupingObjects
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Коллекция описаний объектов, для которых разыскиваются группирующие объекты
    /// </summary>
    [DebuggerDisplay("SearchGroupingObjects: [Count: {Count}]")]
    [Serializable]
    public class SearchGroupingObjects : List<SearchGroupingObject>, ICloneable
    {
      /// <summary>
      /// Отыскать в коллекции описание объекта с указанным идентификатором
      /// </summary>
      /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание объекта не найдено</returns>
      public virtual SearchGroupingObject FindObject(long objectID)
      {
        if (objectID == 0L)
          return (SearchGroupingObject) null;
        for (int index = 0; index < this.Count; ++index)
        {
          SearchGroupingObject searchGroupingObject = this[index];
          if (searchGroupingObject.ObjectID == objectID)
            return searchGroupingObject;
        }
        return (SearchGroupingObject) null;
      }

      /// <summary>Добавить или заменить описание объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="objectTypeID">Идентификатор типа версии объекта</param>
      /// <param name="groupObjectID">Идентификатор группирующего объекта, которому принадлежит данная версия объекта</param>
      /// <param name="groupObjectTypeID">Идентификатор типа группирующего объекта</param>
      /// <returns>Вновь созданный или изменённый объект-описание</returns>
      public virtual SearchGroupingObject Add(
        long objectID,
        int objectTypeID,
        long groupObjectID,
        int groupObjectTypeID)
      {
        SearchGroupingObject searchGroupingObject1 = this.FindObject(objectID);
        if (searchGroupingObject1 != null)
        {
          searchGroupingObject1.ObjectTypeID = objectTypeID;
          if (groupObjectID != -1L && !searchGroupingObject1.GroupObjectIDs.ContainsKey(groupObjectID))
            searchGroupingObject1.GroupObjectIDs.Add(groupObjectID, groupObjectTypeID);
          return searchGroupingObject1;
        }
        SearchGroupingObject searchGroupingObject2 = new SearchGroupingObject(objectID, objectTypeID, groupObjectID, groupObjectTypeID);
        this.Add(searchGroupingObject2);
        return searchGroupingObject2;
      }

      /// <summary>Добавить или заменить описание объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="objectTypeID">Идентификатор типа версии объекта</param>
      /// <param name="groupObjects">Группирующие объекты, с указанием их типов</param>
      /// <returns>Вновь созданный или изменённый объект-описание</returns>
      public virtual SearchGroupingObject Add(
        long objectID,
        int objectTypeID,
        Dictionary<long, int> groupObjects)
      {
        SearchGroupingObject searchGroupingObject1 = this.FindObject(objectID);
        if (searchGroupingObject1 != null)
        {
          searchGroupingObject1.ObjectTypeID = objectTypeID;
          if (groupObjects != null)
          {
            foreach (KeyValuePair<long, int> groupObject in groupObjects)
              searchGroupingObject1.GroupObjectIDs[groupObject.Key] = groupObject.Value;
          }
          return searchGroupingObject1;
        }
        SearchGroupingObject searchGroupingObject2 = new SearchGroupingObject(objectID, objectTypeID, groupObjects);
        this.Add(searchGroupingObject2);
        return searchGroupingObject2;
      }

      /// <summary>Полное присваивание другого списка описаний объектов</summary>
      /// <param name="source">Источник</param>
      public virtual void Assign(SearchGroupingObjects source)
      {
        this.Clear();
        if (source == null)
          return;
        for (int index = 0; index < source.Count; ++index)
          this.Add(source[index].Clone() as SearchGroupingObject);
      }

      /// <summary>
      /// Получить полный список идентификаторов версий объектов. В списке все идентификаторы будут отсортированы
      /// </summary>
      /// <returns>Полный список идентификаторов версий объектов</returns>
      public virtual List<long> GetObjectIDs()
      {
        List<long> objectIds = new List<long>();
        for (int index = 0; index < this.Count; ++index)
        {
          SearchGroupingObject searchGroupingObject = this[index];
          objectIds.Add(searchGroupingObject.ObjectID);
        }
        objectIds.Sort();
        return objectIds;
      }

      /// <summary>
      /// Получить полный список идентификаторов типов объектов. В списке все идентификаторы будут отсортированы
      /// </summary>
      /// <returns>Полный список идентификаторов типов объектов</returns>
      public virtual List<int> GetObjectTypeIDs()
      {
        List<int> objectTypeIds = new List<int>();
        for (int index = 0; index < this.Count; ++index)
        {
          SearchGroupingObject searchGroupingObject = this[index];
          if (!objectTypeIds.Contains(searchGroupingObject.ObjectTypeID))
            objectTypeIds.Add(searchGroupingObject.ObjectTypeID);
        }
        objectTypeIds.Sort();
        return objectTypeIds;
      }

      /// <summary>
      /// Получить полный список идентификаторов группирующих объектов. В списке все идентификаторы будут отсортированы
      /// </summary>
      /// <returns>Полный список идентификаторов группирующих объектов, упорядоченный по типам объектов</returns>
      public virtual Dictionary<int, List<long>> GetGroupingObjectIDs()
      {
        Dictionary<int, List<long>> groupingObjectIds = new Dictionary<int, List<long>>();
        for (int index = 0; index < this.Count; ++index)
        {
          SearchGroupingObject searchGroupingObject = this[index];
          if (searchGroupingObject.ObjectID != 0L && (MetaDataHelper.HasObjectTypeGroupingRelTypes(searchGroupingObject.ObjectTypeID) || MetaDataHelper.IsObjectTypeEditingContext(searchGroupingObject.ObjectTypeID)))
          {
            if (!groupingObjectIds.ContainsKey(searchGroupingObject.ObjectTypeID))
              groupingObjectIds.Add(searchGroupingObject.ObjectTypeID, new List<long>());
            List<long> longList = groupingObjectIds[searchGroupingObject.ObjectTypeID];
            if (longList.IndexOf(searchGroupingObject.ObjectID) < 0)
              longList.Add(searchGroupingObject.ObjectID);
            if (longList.IndexOf(-searchGroupingObject.ObjectID) < 0)
              longList.Add(-searchGroupingObject.ObjectID);
          }
          foreach (KeyValuePair<long, int> groupObjectId in searchGroupingObject.GroupObjectIDs)
          {
            if (!groupingObjectIds.ContainsKey(groupObjectId.Value))
              groupingObjectIds.Add(groupObjectId.Value, new List<long>());
            List<long> longList = groupingObjectIds[groupObjectId.Value];
            if (longList.IndexOf(groupObjectId.Key) < 0)
            {
              longList.Add(groupObjectId.Key);
              if (longList.IndexOf(-groupObjectId.Key) < 0)
                longList.Add(-groupObjectId.Key);
            }
          }
        }
        return groupingObjectIds;
      }

      /// <summary>Получить полный список группирующих объектов</summary>
      /// <returns>Полный список группирующих объектов</returns>
      public virtual SearchGroupingObjects GetGroupingObjectsList()
      {
        SearchGroupingObjects groupingObjectsList = new SearchGroupingObjects();
        for (int index = 0; index < this.Count; ++index)
        {
          SearchGroupingObject searchGroupingObject = this[index];
          if (searchGroupingObject.ObjectID != 0L && (MetaDataHelper.HasObjectTypeGroupingRelTypes(searchGroupingObject.ObjectTypeID) || MetaDataHelper.IsObjectTypeEditingContext(searchGroupingObject.ObjectTypeID)) && groupingObjectsList.FindObject(searchGroupingObject.ObjectID) == null)
            groupingObjectsList.Add(searchGroupingObject.Clone() as SearchGroupingObject);
          for (int key = 0; key < searchGroupingObject.GroupObjectIDs.Count; ++key)
          {
            if (groupingObjectsList.FindObject((long) searchGroupingObject.GroupObjectIDs[(long) key]) == null)
              groupingObjectsList.Add((long) searchGroupingObject.GroupObjectIDs[(long) key], -1, searchGroupingObject.GroupObjectIDs);
          }
        }
        groupingObjectsList.Sort();
        return groupingObjectsList;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        SearchGroupingObjects searchGroupingObjects = new SearchGroupingObjects();
        searchGroupingObjects.Assign(this);
        return (object) searchGroupingObjects;
      }
    }
}
