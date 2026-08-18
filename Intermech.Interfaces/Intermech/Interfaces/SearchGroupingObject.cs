
// Type: Intermech.Interfaces.SearchGroupingObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Описание объекта, для которого разыскивается группирующий объект
    /// </summary>
    [DebuggerDisplay("ObjectID: {objectID}; ObjectTypeID: {objectTypeID}; GroupObjectID: {groupObjectID};")]
    [Serializable]
    public class SearchGroupingObject : ICloneable, IComparable, IComparable<SearchGroupingObject>
    {
      /// <summary>
      /// Идентификатор версии объекта (уникальный в пределах всей коллекции)
      /// </summary>
      private long objectID;
      /// <summary>Идентификатор типа версии объекта</summary>
      private int objectTypeID = -1;
      /// <summary>
      /// Идентификаторы группирующих объектов, которым принадлежит данная версия объекта
      /// </summary>
      private Dictionary<long, int> groupObjectIDs = new Dictionary<long, int>();

      /// <summary>Создать описание объекта, участвующего в поиске</summary>
      /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="objectTypeID">Идентификатор типа версии объекта</param>
      /// <param name="groupObjectID">Идентификатор группирующего объекта, которому принадлежит данная версия объекта</param>
      /// <param name="groupObjectTypeID">Тип группирующего объекта</param>
      public SearchGroupingObject(
        long objectID,
        int objectTypeID,
        long groupObjectID,
        int groupObjectTypeID)
      {
        this.objectID = objectID;
        this.objectTypeID = objectTypeID;
        if (groupObjectID == -1L || this.groupObjectIDs.ContainsKey(groupObjectID))
          return;
        this.groupObjectIDs.Add(groupObjectID, groupObjectTypeID);
      }

      /// <summary>Создать описание объекта, участвующего в поиске</summary>
      /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="objectTypeID">Идентификатор типа версии объекта</param>
      /// <param name="groupObjectIDs">Идентификаторы группирующих объектов, которым принадлежит данная версия объекта</param>
      public SearchGroupingObject(
        long objectID,
        int objectTypeID,
        Dictionary<long, int> groupObjectIDs)
      {
        this.objectID = objectID;
        this.objectTypeID = objectTypeID;
        this.groupObjectIDs = groupObjectIDs ?? new Dictionary<long, int>();
      }

      /// <summary>
      /// Идентификатор версии объекта (уникальный в пределах всей коллекции)
      /// </summary>
      public long ObjectID
      {
        [DebuggerStepThrough] get => this.objectID;
        set => this.objectID = value;
      }

      /// <summary>Идентификатор типа версии объекта</summary>
      public int ObjectTypeID
      {
        [DebuggerStepThrough] get => this.objectTypeID;
        set => this.objectTypeID = value;
      }

      /// <summary>
      ///  [Идентификатор версии группирующего объекта x идентификатор его типа], которому принадлежит данная версия объекта
      /// </summary>
      public Dictionary<long, int> GroupObjectIDs
      {
        [DebuggerStepThrough] get => this.groupObjectIDs;
        set => this.groupObjectIDs = value;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        if (!(obj is SearchGroupingObject searchGroupingObject))
          return base.Equals(obj);
        return this.objectID == searchGroupingObject.objectID && this.ObjectTypeID == searchGroupingObject.ObjectTypeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.objectID.GetHashCode() << 16 /*0x10*/ ^ this.ObjectTypeID.GetHashCode();
      }

      /// <summary>
      /// Загрузить описание объекта из базы данных (если оно не загружено)
      /// </summary>
      /// <param name="session">Сессия</param>
      public virtual void LoadDescription(IUserSession session)
      {
        if (session == null)
          return;
        if (this.ObjectTypeID == -1)
          this.ObjectTypeID = session.GetObjectInfo(this.ObjectID).ObjectTypeID;
        List<long> longList = new List<long>();
        if (this.groupObjectIDs != null)
        {
          foreach (KeyValuePair<long, int> groupObjectId in this.groupObjectIDs)
          {
            if (groupObjectId.Value == -1 && groupObjectId.Key != 0L && longList.IndexOf(groupObjectId.Key) < 0)
              longList.Add(groupObjectId.Key);
          }
        }
        for (int index = 0; index < longList.Count; ++index)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(longList[index]);
          this.groupObjectIDs[objectInfo.ObjectID] = objectInfo.ObjectTypeID;
        }
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new SearchGroupingObject(this.objectID, this.objectTypeID, this.groupObjectIDs);
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as SearchGroupingObject);

      /// <summary>Сравнить с указанным методом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(SearchGroupingObject other)
      {
        if (other == null)
          return 1;
        int num = this.ObjectTypeID.CompareTo(other.ObjectTypeID);
        return num != 0 ? num : this.ObjectID.CompareTo(other.ObjectID);
      }
    }
}
