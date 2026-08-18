
// Type: Intermech.Client.Core.DBHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Pools;
using Intermech.Text;
using Intermech.Tools.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Client.Core;

/// <summary> Методы, облегчающие работу с базой данных </summary>
public abstract class DBHelper
{
  /// <summary> Получить Guid объекта по его идентификатору </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <returns> Guid объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjGuidByID(long objID)
  {
    return DBHelper.GetObjGuidByID(objID, Guid.Empty, false);
  }

  /// <summary> Получить Guid объекта по его идентификатору </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <param name="checkObjType">
  /// Если != -1 то производиться проверка, является ли объект именно этого типа (checkObjType = идентификатор типа)
  /// Если тип объекта - дочерний от типа с идентифкатором checkObjType, то считается, что условие выполнено
  /// </param>
  /// <returns> Guid объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjGuidByID(long objID, int checkObjType)
  {
    return DBHelper.GetObjGuidByID(objID, checkObjType, true);
  }

  /// <summary> Получить Guid объекта по его идентификатору </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <param name="checkObjType"> Если != -1 то производиться проверка, является ли объект именно этого типа (checkObjType = идентификатор типа) </param>
  /// <param name="subtypesOk"> Если true, то дочерние типы объектов считаются тем же типом объектов, иначе - объект должен быть именно типом объекта с идентифкатором checkObjType </param>
  /// <returns> Guid объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjGuidByID(long objID, int checkObjType, bool subtypesOk)
  {
    if (objID == 0L || objID == -1L)
      return Guid.Empty;
    Guid checkObjType1 = Guid.Empty;
    if (checkObjType != 0 && checkObjType != -1)
    {
      checkObjType1 = DBHelper.GetObjTypeGuidByID(checkObjType);
      if (checkObjType1 == Guid.Empty)
        return Guid.Empty;
    }
    return DBHelper.GetObjGuidByID(objID, checkObjType1, subtypesOk);
  }

  /// <summary> Получить Guid объекта по его идентификатору </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <param name="checkObjType">
  /// Если != Guid.Empty то производиться проверка, является ли объект именно этого типа (checkObjType = Guid типа)
  /// Если тип объекта - дочерний от типа с Guid-ом checkObjType, то считается, что условие выполнено
  /// </param>
  /// <returns> Guid объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjGuidByID(long objID, Guid checkObjType)
  {
    return DBHelper.GetObjGuidByID(objID, checkObjType, true);
  }

  /// <summary> Получить Guid объекта по его идентификатору </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <param name="checkObjType"> Если != Guid.Empty то производиться проверка, является ли объект именно этого типа (Guid = идентификатор типа) </param>
  /// <param name="subtypesOk"> Если true, то дочерние типы объектов считаются тем же типом объектов, иначе - объект должен быть именно типом объекта с идентифкатором checkObjType </param>
  /// <returns> Guid объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjGuidByID(long objID, Guid checkObjType, bool subtypesOk)
  {
    if (objID == 0L || objID == -1L)
      return Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objID);
        if (dbObject == null || !(dbObject is IDBGuid dbGuid))
          return Guid.Empty;
        if (checkObjType == Guid.Empty)
          return dbGuid.GUID;
        if (subtypesOk)
          return dbObject.isParentType(checkObjType) ? dbGuid.GUID : Guid.Empty;
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType);
        return objectType == null || !((objectType as IDBGuid).GUID == checkObjType) ? Guid.Empty : dbGuid.GUID;
      }
      catch
      {
        return Guid.Empty;
      }
    }
  }

  /// <summary> Получить идентификатор объекта по его Guid-у </summary>
  /// <param name="objectGuid"> Guid объекта </param>
  /// <returns> Идентификатор объекта. Вернёт -1 если объект не был найден </returns>
  public static long GetObjIDByGuid(Guid objectGuid)
  {
    return DBHelper.GetObjIDByGuid(objectGuid, Guid.Empty, false);
  }

  /// <summary> Получить идентификатор объекта по его Guid-у </summary>
  /// <param name="objectGuid"> Guid объекта </param>
  /// <param name="checkObjType">
  /// Если != 0 то производиться проверка, является ли объект именно этого типа (checkObjType = идентификатор типа)
  /// Если тип объекта - дочерний от типа с идентифкатором checkObjType, то считается, что условие выполнено
  /// </param>
  /// <returns> Идентификатор объекта. Вернёт -1 если объект не был найден </returns>
  public static long GetObjIDByGuid(Guid objectGuid, int checkObjType)
  {
    return DBHelper.GetObjIDByGuid(objectGuid, checkObjType, true);
  }

  /// <summary> Получить идентификатор объекта по его Guid-у </summary>
  /// <param name="objectGuid"> Guid объекта </param>
  /// <param name="checkObjType"> Если != -1 то производиться проверка, является ли объект именно этого типа (checkObjType = идентификатор типа) </param>
  /// <param name="subtypesOk"> Если true, то дочерние типы объектов считаются тем же типом объектов, иначе - объект должен быть именно типом объекта с Guid-ом = checkObjType </param>
  /// <returns> Идентификатор объекта, -1 если объект не был найден </returns>
  public static long GetObjIDByGuid(Guid objectGuid, int checkObjType, bool subtypesOk)
  {
    if (objectGuid == Guid.Empty)
      return -1;
    Guid checkObjType1 = Guid.Empty;
    if (checkObjType != -1 && checkObjType != 0)
    {
      checkObjType1 = DBHelper.GetObjTypeGuidByID(checkObjType);
      if (checkObjType1 == Guid.Empty)
        return -1;
    }
    return DBHelper.GetObjIDByGuid(objectGuid, checkObjType1, subtypesOk);
  }

  /// <summary> Получить идентификатор объекта по его Guid-у </summary>
  /// <param name="objectGuid"> Guid объекта </param>
  /// <param name="checkObjType">
  /// Если != Guid.Empty то производиться проверка, является ли объект именно этого типа (checkObjType = Guid типа)
  /// Если тип объекта - дочерний от типа с Guid-ом checkObjType, то считается, что условие выполнено
  /// </param>
  /// <returns> Идентификатор объекта. . Вернёт -1 если объект не был найден </returns>
  public static long GetObjIDByGuid(Guid objectGuid, Guid checkObjType)
  {
    return DBHelper.GetObjIDByGuid(objectGuid, checkObjType, true);
  }

  /// <summary> Получить идентификатор объекта по его Guid-у </summary>
  /// <param name="objectGuid"> Guid объекта </param>
  /// <param name="checkObjType"> Если != Guid.Empty то производиться проверка, является ли объект именно этого типа (checkObjType = Guid типа) </param>
  /// <param name="subtypesOk"> Если true, то дочерние типы объектов считаются тем же типом объектов, иначе - объект должен быть именно типом объекта с Guid-ом = checkObjType </param>
  /// <returns> Идентификатор объекта, -1 если объект не был найден </returns>
  public static long GetObjIDByGuid(Guid objectGuid, Guid checkObjType, bool subtypesOk)
  {
    if (objectGuid == Guid.Empty)
      return -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectGuid);
        if (dbObject == null)
          return -1;
        if (checkObjType == Guid.Empty)
          return dbObject.ObjectID;
        if (subtypesOk)
          return dbObject.isParentType(checkObjType) ? dbObject.ObjectID : -1L;
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType);
        return objectType == null || !(objectType is IDBGuid dbGuid) || !(dbGuid.GUID == checkObjType) ? -1L : dbObject.ObjectID;
      }
      catch
      {
        return -1;
      }
    }
  }

  /// <summary> Получить Guid атрибута по его идентификатору </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <returns> Guid атрибута. Возвращает Guid.Empty если атрибут не был найден </returns>
  public static Guid GetAttributeGuidByID(int attributeID)
  {
    if (attributeID == 0 || attributeID == -1)
      return Guid.Empty;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
    return attributeType == null ? Guid.Empty : attributeType.AttributeGuid;
  }

  /// <summary> Получить идентификатор атрибута по его Guid-у </summary>
  /// <param name="attributeGuid"> Guid атрибута </param>
  /// <returns>  Идентификатор атрибута, -1 если атрибут не был найден </returns>
  public static int GetAttributeIDByGuid(Guid attributeGuid)
  {
    return DBHelper.GetAttributeIDByGuid((IUserSession) null, attributeGuid);
  }

  /// <summary> Получить идентификатор атрибута по его Guid-у </summary>
  /// <param name="userSession"></param>
  /// <param name="attributeGuid"> Guid атрибута </param>
  /// <returns>  Идентификатор атрибута, -1 если атрибут не был найден </returns>
  public static int GetAttributeIDByGuid(IUserSession userSession, Guid attributeGuid)
  {
    if (attributeGuid == Guid.Empty)
      return -1;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeGuid);
    return attributeType == null ? -1 : attributeType.AttributeID;
  }

  /// <summary> Получить Guid типа по его идентификатору </summary>
  /// <param name="typeID"> Идентификатор типа </param>
  /// <returns> Guid типа. Вернёт Guid.Empty если тип не найден </returns>
  public static Guid GetObjTypeGuidByID(int typeID)
  {
    if (typeID == 0 || typeID == -1)
      return Guid.Empty;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(typeID);
    return objectType == null ? Guid.Empty : objectType.Guid;
  }

  /// <summary> Получить Guid типа по его идентификатору </summary>
  /// <param name="typeID"> Идентификатор типа </param>
  /// <returns> Guid типа. Вернёт Guid.Empty если тип не найден </returns>
  public static Guid GetRelTypeGuidByID(int typeID)
  {
    if (typeID == -1)
      return Guid.Empty;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(typeID);
    return relationType == null ? Guid.Empty : relationType.Guid;
  }

  /// <summary> Получить идентификатор типа по его Guid-у </summary>
  /// <param name="typeGuid"> Guid типа </param>
  /// <returns> Идентификатор типа, -1 если тип не был найден </returns>
  public static int GetObjTypeIDByGuid(Guid typeGuid)
  {
    if (typeGuid == Guid.Empty)
      return -1;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(typeGuid);
    return objectType == null ? -1 : objectType.ObjectTypeID;
  }

  /// <summary> Получить идентификатор типа по его Guid-у </summary>
  /// <param name="typeGuid"> Guid типа </param>
  /// <returns> Идентификатор типа, -1 если тип не был найден </returns>
  public static int GetRelTypeIDByGuid(Guid typeGuid)
  {
    if (typeGuid == Guid.Empty)
      return -1;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(typeGuid);
    return relationType == null ? -1 : relationType.RelationTypeID;
  }

  /// <summary> Получение наименования атрибута </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <returns> Наименование атрибута. Вернёт string.Empty если атрибут не найден </returns>
  public static string GetAttributeName(int attributeID)
  {
    if (attributeID != -1)
    {
      if (attributeID != 0)
      {
        try
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
          return attributeType != null ? attributeType.Name : string.Empty;
        }
        catch
        {
          return string.Empty;
        }
      }
    }
    return string.Empty;
  }

  /// <summary> Получение наименования атрибута </summary>
  /// <param name="attributeGuid"> Guid атрибута </param>
  /// <returns> Наименование атрибута. Вернёт string.Empty если атрибут не найден </returns>
  public static string GetAttributeName(Guid attributeGuid)
  {
    if (attributeGuid == Guid.Empty)
      return string.Empty;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeGuid);
    return attributeType == null ? string.Empty : attributeType.Name;
  }

  /// <summary> Получение наименования типа объекта </summary>
  /// <param name="objTypeID"> Идентификатор типа объекта </param>
  /// <returns> Наименование типа объекта. Вернёт string.Empty если типа объекта не найден </returns>
  public static string GetObjTypeName(int objTypeID)
  {
    if (objTypeID == -1 || objTypeID == 0)
      return string.Empty;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
    return objectType == null ? string.Empty : objectType.ObjectTypeName;
  }

  /// <summary> Получение наименования типа объекта </summary>
  /// <param name="objTypeGuid"> Guid типа объекта </param>
  /// <returns> Наименование типа объекта. Вернёт string.Empty если типа объекта не найден </returns>
  public static string GetObjTypeName(Guid objTypeGuid)
  {
    if (objTypeGuid == Guid.Empty)
      return string.Empty;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
    return objectType == null ? string.Empty : objectType.ObjectTypeName;
  }

  /// <summary> Получение заголовка объекта </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <returns> Заголовок объекта. Вернёт string.Empty если объект не найден </returns>
  public static string GetObjCaption(long objID)
  {
    if (objID != -1L)
    {
      if (objID != 0L)
      {
        try
        {
          return ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(objID).Caption;
        }
        catch
        {
          return string.Empty;
        }
      }
    }
    return string.Empty;
  }

  /// <summary> Получение заголовка объекта </summary>
  /// <param name="objGuid"> Guid объекта </param>
  /// <returns> Заголовок объекта. Вернёт string.Empty если объект не найден </returns>
  public static string GetObjCaption(Guid objGuid)
  {
    if (objGuid == Guid.Empty)
      return string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objGuid);
        return dbObject != null ? dbObject.Caption : string.Empty;
      }
      catch
      {
        return string.Empty;
      }
    }
  }

  /// <summary> Получить идентификатор типа атрибута по Guid-у атрибута </summary>
  /// <param name="attributeGuid"> Guid атрибута </param>
  /// <returns> Идентификатор типа атрибута </returns>
  public static int GetAttributeTypeIDFromAttributeGuid(Guid attributeGuid)
  {
    if (attributeGuid == Guid.Empty)
      return -1;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeGuid);
    return attributeType == null ? -1 : attributeType.AttributeID;
  }

  /// <summary> Получить идентификатор типа атрибута по имени атрибута </summary>
  /// <param name="attributeName"> Имя атрибута </param>
  /// <returns> Идентификатор типа атрибута </returns>
  public static int GetAttributeTypeIDFromAttributeName(string attributeName)
  {
    if (attributeName == string.Empty)
      return -1;
    int attributeByTypeNameId = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    return attributeByTypeNameId == -10000 ? -1 : attributeByTypeNameId;
  }

  /// <summary> Получение идентификатора типа объекта экземпляра объекта </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <returns> Идентфикатор типа объекта. Вернёт -1, если объект не найден </returns>
  public static int GetObjTypeID(long objID)
  {
    if (Consts.IsUndefinedObjectId(objID))
      return -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(objID).ObjectTypeID;
  }

  /// <summary> Получение идентификатора типа объекта экземпляра объекта </summary>
  /// <param name="objGuid"> Guid объекта </param>
  /// <returns> Идентфикатор типа объекта. Вернёт -1, если объект не найден </returns>
  public static int GetObjTypeID(Guid objGuid)
  {
    if (!(objGuid != Guid.Empty))
      return -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(objGuid).ObjectTypeID;
  }

  /// <summary> Получение Guid типа объекта экземпляра объекта </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <returns> Guid типа объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjTypeGuid(long objID)
  {
    if (objID == 0L || objID == -1L)
      return Guid.Empty;
    int objTypeId = DBHelper.GetObjTypeID(objID);
    return objTypeId != 0 ? DBHelper.GetObjTypeGuidByID(objTypeId) : Guid.Empty;
  }

  /// <summary> Получение Guid типа объекта экземпляра объекта </summary>
  /// <param name="objGuid"> Guid объекта </param>
  /// <returns> Guid типа объекта. Вернёт Guid.Empty если объект не был найден </returns>
  public static Guid GetObjTypeGuid(Guid objGuid)
  {
    if (objGuid == Guid.Empty)
      return Guid.Empty;
    int objTypeId = DBHelper.GetObjTypeID(objGuid);
    return objTypeId != 0 ? DBHelper.GetObjTypeGuidByID(objTypeId) : Guid.Empty;
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <returns>Список идентификаторов объектов переданого типа </returns>
  public static LongList GetObjIDsListOfType(int ObjType)
  {
    return DBHelper.GetObjIDsListOfType(ObjType, -1);
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <param name="sortAttributeID"> Идентификатор атрибута, по которому должен быть отсортирован список. -1 означает, что сортировать не надо </param>
  /// <returns>Список идентификаторов объектов переданого типа </returns>
  public static LongList GetObjIDsListOfType(int ObjType, int sortAttributeID)
  {
    return DBHelper.GetObjIDsListOfType(ObjType, sortAttributeID, true);
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <param name="sortAttributeID"> Идентификатор атрибута, по которому должен быть отсортирован список. -1 означает, что сортировать не надо </param>
  /// <param name="ascSort"> Направление сортировки. Если true, то список будет отсортирован по возрастанию, иначе - по убыванию </param>
  /// <returns>Список идентификаторов объектов переданого типа </returns>
  public static LongList GetObjIDsListOfType(int ObjType, int sortAttributeID, bool ascSort)
  {
    LongList objIdsListOfType = new LongList();
    if (ObjType != -1)
    {
      if (ObjType != 0)
      {
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.Equal, (object) ObjType, LogicalOperators.NONE, 0, false);
            DBRecordSetParams dbRecordSetParams;
            ref DBRecordSetParams local = ref dbRecordSetParams;
            ConditionStructure[] conditions = new ConditionStructure[1]
            {
              conditionStructure
            };
            object[] columns;
            if (sortAttributeID != -1)
              columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                (object) sortAttributeID
              };
            else
              columns = new object[1]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID
              };
            object[] sortColumns;
            if (sortAttributeID != -1)
              sortColumns = new object[1]
              {
                (object) sortAttributeID
              };
            else
              sortColumns = new object[0];
            SortOrders[] orders;
            if (sortAttributeID != -1)
            {
              if (!ascSort)
                orders = new SortOrders[1]
                {
                  SortOrders.DESC
                };
              else
                orders = new SortOrders[1]{ SortOrders.ASC };
            }
            else
              orders = new SortOrders[0];
            local = new DBRecordSetParams(conditions, columns, sortColumns, orders);
            DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ObjType, dbRecordSetParams);
            objIdsListOfType = new LongList(dataTable.Rows.Count);
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              objIdsListOfType.Add((object) Convert.ToInt64(row[0]));
          }
        }
        catch
        {
        }
      }
    }
    return objIdsListOfType;
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <returns> Список коротких описаний объектов переданого типа </returns>
  public static ShortObjectDecriptionList GetObjShortDescriptionsListOfType(int ObjType)
  {
    return DBHelper.GetObjShortDescriptionsListOfType(ObjType, -1);
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <param name="sortAttributeID"> Идентификатор атрибута, по которому должен быть отсортирован список. -1 означает, что сортировать не надо </param>
  /// <returns> Список коротких описаний объектов переданого типа </returns>
  public static ShortObjectDecriptionList GetObjShortDescriptionsListOfType(
    int ObjType,
    int sortAttributeID)
  {
    return DBHelper.GetObjShortDescriptionsListOfType(ObjType, sortAttributeID, true);
  }

  /// <summary> Получение списка идентификаторов всех объектов определённого типа </summary>
  /// <param name="ObjType"> Тип объектов </param>
  /// <param name="sortAttributeID"> Идентификатор атрибута, по которому должен быть отсортирован список. -1 означает, что сортировать не надо </param>
  /// <param name="ascSort"> Направление сортировки. Если true, то список будет отсортирован по возрастанию, иначе - по убыванию </param>
  /// <returns> Список коротких описаний объектов переданого типа </returns>
  public static ShortObjectDecriptionList GetObjShortDescriptionsListOfType(
    int ObjType,
    int sortAttributeID,
    bool ascSort)
  {
    ShortObjectDecriptionList descriptionsListOfType = new ShortObjectDecriptionList();
    if (ObjType != -1)
    {
      if (ObjType != 0)
      {
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.Equal, (object) ObjType, LogicalOperators.NONE, 0, false);
            DBRecordSetParams dbRecordSetParams;
            ref DBRecordSetParams local = ref dbRecordSetParams;
            ConditionStructure[] conditions = new ConditionStructure[1]
            {
              conditionStructure
            };
            object[] columns;
            if (sortAttributeID != -1)
              columns = new object[3]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                (object) ObligatoryObjectAttributes.CAPTION,
                (object) sortAttributeID
              };
            else
              columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                (object) ObligatoryObjectAttributes.CAPTION
              };
            object[] sortColumns;
            if (sortAttributeID != -1)
              sortColumns = new object[1]
              {
                (object) sortAttributeID
              };
            else
              sortColumns = new object[0];
            SortOrders[] orders;
            if (sortAttributeID != -1)
            {
              if (!ascSort)
                orders = new SortOrders[1]
                {
                  SortOrders.DESC
                };
              else
                orders = new SortOrders[1]{ SortOrders.ASC };
            }
            else
              orders = new SortOrders[0];
            local = new DBRecordSetParams(conditions, columns, sortColumns, orders);
            DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ObjType, dbRecordSetParams);
            descriptionsListOfType = new ShortObjectDecriptionList(dataTable.Rows.Count);
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              descriptionsListOfType.Add((object) new ShortObjectDecription(Convert.ToInt64(row[0]), Convert.ToString(row[1])));
          }
        }
        catch
        {
        }
      }
    }
    return descriptionsListOfType;
  }

  /// <summary> Получение списка идентификаторов возможных атрибутов у связи </summary>
  /// <param name="relationTypeID"> Идентификатор типа связи </param>
  /// <returns> Список идентификаторов атрибутов </returns>
  public static IntList GetRelationTypeAttributeIDs(int relationTypeID)
  {
    IntList result = new IntList();
    if (relationTypeID != 0 && relationTypeID != -1)
      MetaDataHelper.GetAttribute4RelationTypeList(relationTypeID).ForEach((Action<IMSAttribute4RelationType>) (item => result.Add((object) item.AttributeID)));
    return result;
  }

  /// <summary> Получение списка кратких описаний возможных атрибутов у связи </summary>
  /// <param name="relationTypeID"> Идентификатор типа связи </param>
  /// <returns> Список кратких описаний атрибутов </returns>
  public static ShortAttributeDecriptionList GetRelationTypeAttributeShortDescriptions(
    int relationTypeID)
  {
    ShortAttributeDecriptionList shortDescriptions = new ShortAttributeDecriptionList();
    if (relationTypeID != 0 && relationTypeID != -1)
    {
      IntList typeAttributeIds = DBHelper.GetRelationTypeAttributeIDs(relationTypeID);
      if (typeAttributeIds != null && typeAttributeIds.Count > 0)
      {
        string empty = string.Empty;
        foreach (int attributeID in (ArrayList) typeAttributeIds)
        {
          string attributeName = DBHelper.GetAttributeName(attributeID);
          if (attributeName != string.Empty)
            shortDescriptions.Add((object) new ShortAttributeDecription(attributeID, string.Empty)
            {
              AttributeCaption = attributeName
            });
        }
      }
    }
    return shortDescriptions;
  }

  /// <summary> Получение списка идентификаторов возможных атрибутов у типа объекта </summary>
  /// <param name="objTypeID"> Идентификатор типа объекта </param>
  /// <returns> Список идентификаторов атрибутов </returns>
  public static IntList GetObjTypeAttributeIDs(int objTypeID)
  {
    IntList result = new IntList();
    if (objTypeID != 0 && objTypeID != -1)
      MetaDataHelper.GetAttribute4ObjectTypeList(objTypeID).ForEach((Action<IMSAttribute4ObjectType>) (item => result.Add((object) item.AttributeID)));
    return result;
  }

  /// <summary> Получение кратких описаний  возможных атрибутов у типа объекта </summary>
  /// <param name="objTypeID"> Идентификатор типа объекта </param>
  /// <returns> Список идентификаторов атрибутов </returns>
  public static ShortAttributeDecriptionList GetObjTypeAttributeShortDescriptions(int objTypeID)
  {
    ShortAttributeDecriptionList shortDescriptions = new ShortAttributeDecriptionList();
    if (objTypeID != 0 && objTypeID != -1)
    {
      IntList typeAttributeIds = DBHelper.GetObjTypeAttributeIDs(objTypeID);
      if (typeAttributeIds != null && typeAttributeIds.Count > 0)
      {
        string empty = string.Empty;
        foreach (int attributeID in (ArrayList) typeAttributeIds)
        {
          string attributeName = DBHelper.GetAttributeName(attributeID);
          if (attributeName != string.Empty)
            shortDescriptions.Add((object) new ShortAttributeDecription(attributeID, string.Empty)
            {
              AttributeCaption = attributeName
            });
        }
      }
    }
    return shortDescriptions;
  }

  /// <summary> Проверка, содержит ли связь атрибут (признак возможности наличия любого атрибута игнорируется) </summary>
  /// <param name="realtionID"> Идентфиикатор связи </param>
  /// <param name="attributeID"> Идентфиикатор атрибута </param>
  /// <returns> True, если связь содержит данный атрибут по-умолчанию (признак возможности наличия любого атрибута игнорируется) </returns>
  public static bool IsRelationHasAttribute(int realtionID, int attributeID)
  {
    IntList typeAttributeIds = DBHelper.GetRelationTypeAttributeIDs(realtionID);
    return typeAttributeIds != null && typeAttributeIds.Contains((object) attributeID);
  }

  /// <summary>? Проверка, содержит ли тип объекта атрибут (признак возможности наличия любого атрибута игнорируется) </summary>
  /// <param name="objTypeID"> Идентфиикатор типа объекта </param>
  /// <param name="realtionID"></param>
  /// <returns> True, если тип объекта содержит данный атрибут по-умолчанию (признак возможности наличия любого атрибута игнорируется) </returns>
  public static bool IsObjTypeHasAttribute(int realtionID, int objTypeID)
  {
    IntList typeAttributeIds = DBHelper.GetObjTypeAttributeIDs(objTypeID);
    return typeAttributeIds != null && typeAttributeIds.Contains((object) objTypeID);
  }

  /// <summary> Проверка, может ли значение некоторого атрибута быть помещёно в PropertyGrid </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <returns> True, если значение некоторого атрибута быть помещёно в PropertyGrid </returns>
  public static bool IsAttributeGridable(int attributeID)
  {
    if (attributeID != 0)
    {
      if (attributeID != -1)
      {
        try
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
          return attributeType != null && attributeType.IsGridable;
        }
        catch
        {
          return false;
        }
      }
    }
    return false;
  }

  /// <summary> Проверка возможности создания связи между объектами с некоторыми типами </summary>
  /// <param name="relationTypeID"> Идентификатор связи </param>
  /// <param name="objTypeIdFrom"> Идентификатор типа объекта из которого должна исходить связь </param>
  /// <param name="objTypeIdTo"> Идентификатор типа объекта в который должна входить связь </param>
  /// <returns> True, если такая связь может быть создана </returns>
  public static bool CanCreateRelationBetween(
    int relationTypeID,
    int objTypeIdFrom,
    int objTypeIdTo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      if (applicabilityCollection != null)
        return applicabilityCollection.GetApplicability(relationTypeID, objTypeIdTo, objTypeIdFrom) != null;
    }
    return false;
  }

  /// <summary> Проверка возможности создания связи между объектами с некоторыми типами </summary>
  /// <param name="relationTypeID"> Идентификатор связи </param>
  /// <param name="objTypeIdFrom"> Идентификатор типа объекта из которого должна исходить связь </param>
  /// <param name="objTypeIdToList"> Список идентификаторов типов объектов в которые должна входить связь </param>
  /// <returns> True, если такая связь может быть создана </returns>
  public static bool CanCreateRelationBetween(
    int relationTypeID,
    int objTypeIdFrom,
    IntList objTypeIdToList)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      if (applicabilityCollection != null)
      {
        foreach (int objTypeIdTo in (ArrayList) objTypeIdToList)
        {
          if (applicabilityCollection.GetApplicability(relationTypeID, objTypeIdTo, objTypeIdFrom) != null)
            return true;
        }
      }
    }
    return false;
  }

  /// <summary>Возвращает тип указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Идентификатор типа объекта</returns>
  /// <exception cref="T:Intermech.ObjectNotFoundException">Объект с заданным идентификатором отсутствует в базе IPS</exception>
  public static int GetObjectType(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
      return !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new ObjectNotFoundException(objectId);
    }
  }

  /// <summary>Возвращает тип указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Пара из идентификатор и имени типа объекта</returns>
  /// <exception cref="T:Intermech.ObjectNotFoundException">Объект с заданным идентификатором отсутствует в базе IPS</exception>
  public static LocalId<int> GetObjectTypeLID(long objectId)
  {
    return (LocalId<int>) DBHelper.CreateObjectTypeGID(DBHelper.GetObjectType(objectId));
  }

  /// <summary>
  /// Возвращает для указанного типа объектов коллекцию родительских типов.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>Коллекция идентификаторов родительских типов</returns>
  [NotNull]
  public static ICollection<int> GetObjectTypeParents(int objectType)
  {
    List<int> objectTypeParents = objectType != -1 ? MetaDataHelper.GetObjectTypeParentsID(objectType) : throw new ArgumentException();
    objectTypeParents.Add(-1);
    return (ICollection<int>) objectTypeParents;
  }

  /// <summary>Возвращает для указанного типа объектов последовательность родительских типов.
  /// Последовательность начинается от наиболее близкого к "корню" иерархии типа объекта и завершается непосредственного "родителем"
  /// переданного типа</summary>
  [NotNull]
  public static IEnumerable<int> GetObjectTypeParentsEnumeration(int objectType)
  {
    if (objectType == -1)
      Enumerable.Empty<int>();
    using (SessionKeeper keeper = new SessionKeeper())
    {
      while (true)
      {
        objectType = keeper.Session.GetObjectType(objectType, true).ParentTypeID;
        if (objectType != -1)
          yield return objectType;
        else
          break;
      }
    }
  }

  /// <summary>
  /// Позволяет проверить, унаследован ли тип объектов от заданного базового типа.
  /// </summary>
  /// <param name="objectType">Идентификатор проверяемого типа</param>
  /// <param name="baseType">Идентификатор базового типа</param>
  /// <returns>true, если тип унаследован от заданного базового типа</returns>
  public static bool IsBasedOnType(int objectType, int baseType)
  {
    if (baseType == -1)
      throw new ArgumentException();
    return objectType == baseType || MetaDataHelper.IsObjectTypeChildOf(objectType, baseType);
  }

  public static GlobalId<int> CreateObjectTypeGID(int objectType, bool throwIfNotFound = true)
  {
    IMSObjectType imsObjectType = objectType != -1 ? MetaDataHelper.GetObjectType(objectType) : throw new ArgumentException();
    if (imsObjectType != null)
      return new GlobalId<int>(imsObjectType.Guid, objectType, imsObjectType.ObjectTypeName);
    if (throwIfNotFound)
      throw new KernelExceptionID(226, (object) objectType);
    return (GlobalId<int>) null;
  }

  public static GlobalId<int> CreateObjectTypeGID(Guid objectType, bool throwIfNotFound = true)
  {
    IMSObjectType imsObjectType = !(objectType == Guid.Empty) ? MetaDataHelper.GetObjectType(objectType) : throw new ArgumentException();
    if (imsObjectType != null)
      return new GlobalId<int>(objectType, imsObjectType.ObjectTypeID, imsObjectType.ObjectTypeName);
    if (throwIfNotFound)
      throw new KernelExceptionID(99, (object) objectType);
    return (GlobalId<int>) null;
  }

  /// <summary>Возвращает заголовок указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Заголовок объекта</returns>
  /// <exception cref="T:Intermech.ObjectNotFoundException">Объект с заданным идентификатором отсутствует в базе IPS</exception>
  public static string GetObjectCaption(long objectId)
  {
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(objectId);
    return !objectInfo.Empty ? objectInfo.Caption : throw new ObjectNotFoundException(objectId);
  }

  /// <summary>Возвращает название указанного объекта для сообщений.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Название объекта</returns>
  /// <exception cref="T:Intermech.ObjectNotFoundException">Объект с заданным идентификатором отсутствует в базе IPS</exception>
  public static string GetObjectNameInMessages(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectId, true).NameInMessages;
  }

  /// <summary>Проверяет наличие объекта в базе IPS.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Признак, что объект существует и находится на уровне продвижения, отличном от "Удалено"</returns>
  public static bool IsObjectAlive(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int objectLevel = sessionKeeper.Session.GetObjectLevel(objectId);
      return objectLevel != -1 && objectLevel != sessionKeeper.Session.IdentHelper.DeletedID;
    }
  }

  /// <summary>
  /// Фильтрует указанный список идентификаторов объектов, оставляя только идентификаторы объектов, которые существуют в базе IPS и находится на уровне продвижения, отличном от "Удалено".
  /// </summary>
  /// <param name="objectIds">Исходный список идентификаторов версий объектов</param>
  /// <returns>Отфильтрованный список индентификаторов версий объектов</returns>
  public static List<long> GetLiveObjectsOnly(ICollection<long> objectIds)
  {
    return DBHelper.GetLiveObjectsAndTypes(objectIds).ConvertAll<long>((Converter<Tuple<long, int>, long>) (tuple => tuple.Item1));
  }

  private static List<Tuple<long, int>> GetLiveObjectsAndTypes(ICollection<long> objectIds)
  {
    if (objectIds == null)
      throw new ArgumentNullException(nameof (objectIds));
    if (objectIds.Count <= 0)
      return new List<Tuple<long, int>>(0);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectTypes(objectIds);
  }

  /// <summary>Проверяет, является ли указанный объект заготовкой.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Признак, что нужно бросать исключение, если указанный объект не найден в базе</param>
  /// <returns>true - если указанный объект является заготовкой, false - если не является или не найден в базе</returns>
  public static bool IsBlankObject(long objectId, bool throwIfNotFound = true)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, throwIfNotFound);
      return dbObject != null && dbObject.IsCreationMode;
    }
  }

  /// <summary>
  /// Преобразует таблицу с результатами запроса к серверу приложений IPS в прямоугольную матрицу. Этот метод используется
  /// при передаче таблиц через COM.
  /// </summary>
  /// <param name="table">Таблиц с результатами</param>
  /// <param name="attributes">Имена атрибутов, значения которых содержатся в таблице</param>
  /// <returns>Прямоугольная матрица с результатами преобразования</returns>
  public static object[,] ToObjectArray(DataTable table, string[] attributes)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    if (attributes.Length != table.Columns.Count)
      throw new ArgumentOutOfRangeException(nameof (attributes));
    Type[] typeArray = new Type[attributes.Length];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < attributes.Length; ++index)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributes[index], true);
        Type dataType = DBAttributeHelper.TryGetDataType(attributeType);
        typeArray[index] = !(dataType == (Type) null) ? dataType : throw new InvalidOperationException($"Не удалось определить тип значений для атрибута '{attributeType.Name}'.");
      }
    }
    object[,] objectArray = new object[table.Rows.Count, attributes.Length];
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      for (int columnIndex = 0; columnIndex < attributes.Length; ++columnIndex)
        objectArray[index, columnIndex] = Convert.ChangeType(row[columnIndex], typeArray[columnIndex]);
    }
    return objectArray;
  }

  public static string UpdateBitString(string bitString, int bitIndex, bool bitValue)
  {
    if (bitString == null)
      bitString = string.Empty;
    int totalWidth = bitIndex + 1;
    if (bitString.Length < totalWidth)
      bitString = bitString.PadRight(totalWidth, '0');
    char ch = bitValue ? '1' : '0';
    if ((int) bitString[bitIndex] != (int) ch)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(bitString.Length))
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append(bitString);
        stringBuilder[bitIndex] = bitValue ? '1' : '0';
        bitString = stringBuilder.ToString();
      }
    }
    return bitString;
  }
}
