// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase;

/// <summary>Summary description for ImbaseHelper.</summary>
public static class ImbaseHelper
{
  private static List<int> _skipList;
  private static bool _isAdmin;

  /// <summary>Является ли тип объекта каталогом IMBASE.</summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsCatalog(int objectType) => objectType == Consts.ImbaseCatalogTypeID;

  /// <summary>Является ли тип объекта каталогом IMBASE.</summary>
  /// <param name="objectTypeGuid">Guid типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsCatalog(Guid objectTypeGuid)
  {
    return objectTypeGuid == Consts.ImbaseCatalogTypeGUID;
  }

  /// <summary>Является ли тип объекта папкой IMBASE.</summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsFolder(int objectType) => objectType == Consts.ImbaseFolderTypeID;

  /// <summary>Является ли тип объекта папкой IMBASE.</summary>
  /// <param name="objectTypeGuid">Guid типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsFolder(Guid objectTypeGuid) => objectTypeGuid == Consts.ImbaseFolderTypeGUID;

  /// <summary>
  /// Является ли тип объекта записью каталога IMBASE или унаследованным от записи каталога
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsCatalogRecord(int objectType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objectType, Consts.ImbaseCatalogRecordTypeID);
  }

  /// <summary>Является ли тип объекта записью каталога IMBASE.</summary>
  /// <param name="objectTypeGuid">Guid типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsCatalogRecord(Guid objectTypeGuid)
  {
    return objectTypeGuid == Consts.ImbaseCatalogRecordTypeGUID;
  }

  /// <summary>Является ли тип объекта ярлыком IMBASE.</summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsTableRef(int objectType) => objectType == Consts.ImbaseTableRefTypeID;

  /// <summary>Является ли тип объекта ярлыком IMBASE.</summary>
  /// <param name="objectTypeGuid">Guid типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsTableRef(Guid objectTypeGuid)
  {
    return objectTypeGuid == Consts.ImbaseTableRefTypeGUID;
  }

  /// <summary>Является ли тип объекта таблицей IMBASE.</summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsTable(int objectType) => objectType == Consts.ImbaseTableTypeID;

  /// <summary>Является ли тип объекта таблицей IMBASE.</summary>
  /// <param name="objectTypeGuid">Guid типа объектов</param>
  /// <returns>Результат проверки</returns>
  public static bool IsTable(Guid objectTypeGuid) => objectTypeGuid == Consts.ImbaseTableTypeGUID;

  /// <summary>
  /// 
  /// </summary>
  public static bool IsAdmin => ImbaseHelper._isAdmin;

  public static bool IsGuid(string sguid, out Guid guid)
  {
    guid = Guid.Empty;
    try
    {
      if (string.IsNullOrEmpty(sguid))
        return false;
      guid = new Guid(sguid);
      return true;
    }
    catch
    {
      guid = Guid.Empty;
      return false;
    }
  }

  public static bool TryParseRecordReference(
    IUserSession session,
    string keyValue,
    out long linkId,
    out long recordId)
  {
    linkId = -1L;
    recordId = -1L;
    if (!keyValue.StartsWith("IK", StringComparison.InvariantCultureIgnoreCase))
      return false;
    int num = keyValue.IndexOf('.');
    if (num == -1)
      return false;
    string str = keyValue.Substring(2, num - 2);
    string s = keyValue.Substring(num + 1);
    if (!long.TryParse(str, out linkId))
    {
      try
      {
        Guid objectGUID = new Guid(str);
        QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
        if (objectInfo.Empty)
          return false;
        linkId = objectInfo.ObjectID;
      }
      catch
      {
        return false;
      }
    }
    return long.TryParse(s, out recordId);
  }

  /// <summary>
  /// 
  /// </summary>
  private static void MakeSkipList()
  {
    ImbaseHelper._skipList = new List<int>();
    ImbaseHelper._skipList.Add(Consts.ClassifFolderKeyAttId);
    ImbaseHelper._skipList.Add(Consts.CreatedObjectAttID);
    ImbaseHelper._skipList.Add(Consts.CreateNewObjectAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseInternalTableNameAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseTableViewAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseTableRowsTypeAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseTemplateRefAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseTemplateDataAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseInternalTableNameAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseInternalOldKeyAttID);
    ImbaseHelper._skipList.Add(Consts.ImbaseNTDLinkAttId);
    ImbaseHelper._skipList.Add(MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545"));
    ImbaseHelper._skipList.Add(Consts.ImbaseTableRecordOwnerAttID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fieldTypes"></param>
  /// <returns></returns>
  public static Type AttTypeToType(FieldTypes fieldTypes)
  {
    switch (fieldTypes)
    {
      case FieldTypes.ftString:
        return typeof (string);
      case FieldTypes.ftInteger:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftMemo:
        return typeof (string);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftMeasured:
        return typeof (double);
      case FieldTypes.ftAutoInc:
        return typeof (long);
      case FieldTypes.ftGuid:
        return typeof (Guid);
      default:
        return typeof (string);
    }
  }

  /// <summary>Выделение классификаторов для родительских узлов.</summary>
  /// <param name="classifKeys"></param>
  /// <returns></returns>
  public static List<string> CollectAllClassificators(string[] classifKeys)
  {
    List<string> bucket = new List<string>();
    ImbaseHelper.CollectAllClassificatorsCollection((ICollection<string>) bucket, (IEnumerable<string>) classifKeys);
    return bucket;
  }

  /// <summary>Выделение классификаторов для родительских узлов.</summary>
  /// <param name="bucket">Результирующий список</param>
  /// <param name="classifKeys"></param>
  /// <returns></returns>
  /// <remarks>Уникальность классификатора не проверяем</remarks>
  public static void CollectAllClassificatorsCollection(
    ICollection<string> bucket,
    IEnumerable<string> classifKeys)
  {
    if (bucket == null)
      throw new ArgumentNullException(nameof (bucket));
    if (classifKeys == null)
      return;
    foreach (string classifKey in classifKeys)
    {
      for (string str = classifKey; str.Length >= 2; str = str.Substring(0, str.Length - 2))
        bucket.Add(str);
    }
  }

  /// <summary>Сформировать внутреннее имя таблицы.</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="name">Начальное имя</param>
  /// <returns>Уникальное имя</returns>
  public static string CreateInternalTableName(IUserSession session, string name)
  {
    string retValue = name;
    if (session != null)
    {
      string conditionValue = name;
      int length = name.LastIndexOf("_C");
      if (length != -1)
      {
        string s = name.Substring(length + 2);
        int num = -1;
        ref int local = ref num;
        if (int.TryParse(s, out local))
          conditionValue = name.Substring(0, length);
      }
      IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseTableTypeID);
      if (objectCollection != null)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(Consts.ImbaseInternalTableNameAttID, RelationalOperators.StartString, (object) conditionValue, LogicalOperators.NONE, 0, false)
        }, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) Consts.ImbaseInternalTableNameAttID
        });
        EnumerableRowCollection<DataRow> source = objectCollection.Select(paramSet).AsEnumerable();
        for (int index = 1; index < 100000; ++index)
        {
          retValue = $"{conditionValue}_C{index}";
          if (source.FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x[1]) == retValue)) == null)
            break;
        }
      }
    }
    return retValue;
  }

  /// <summary>Получить ключ папки классификатора объекта.</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <returns>Ключ папки классификатора</returns>
  public static string GetClassifKeyByObjID(IUserSession session, long objID)
  {
    string classifKeyByObjId = string.Empty;
    IDBObject objectActualCopy = session.GetObjectActualCopy(objID, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Consts.ClassifFolderKeyAttId);
      classifKeyByObjId = attributeById == null || attributeById.Value == null ? string.Empty : attributeById.AsString;
    }
    return classifKeyByObjId;
  }

  /// <summary>
  /// Поиск элементов справочника Imbase (папок / записей) по заданным условиям
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="catalogID">Ид. версии каталога</param>
  /// <param name="attrsToSearch">Условия на атрибуты справочника вида: Ид. атрибута = значение</param>
  /// <returns>Cписок найденных идентификаторов объектов</returns>
  /// <remarks>Наличие ид. каталога и условий обязательно</remarks>
  public static List<long> SearchImFolderData(
    IUserSession session,
    long catalogID,
    List<Tuple<int, object>> attrsToSearch)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (attrsToSearch == null)
      throw new ArgumentNullException(nameof (attrsToSearch));
    if (catalogID == 0L)
      return (List<long>) null;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1);
    columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(attrsToSearch.Count);
    for (int index = 0; index < attrsToSearch.Count; ++index)
    {
      Tuple<int, object> tuple = attrsToSearch[index];
      ConditionStructure conditionStructure = new ConditionStructure(tuple.Item1, RelationalOperators.Equal, tuple.Item2, index == attrsToSearch.Count - 1 ? LogicalOperators.NONE : LogicalOperators.AND, 0, false);
      conditionStructureList.Add(conditionStructure);
    }
    DBRecordSetParams dbrsp = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
    DataTable dataTable = ImbaseHelper.SearchImFolderData(session, catalogID, dbrsp);
    List<long> longList = new List<long>();
    if (dataTable == null || dataTable.Rows.Count == 0)
      return longList;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    return longList;
  }

  /// <summary>
  /// Поиск элементов справочника Imbase (папок / записей) по заданным условиям
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="catalogID">Ид. версии каталога</param>
  /// <param name="dbrsp">Условия на атрибуты справочника</param>
  /// <returns>Наличие ид. каталога обязательно</returns>
  public static DataTable SearchImFolderData(
    IUserSession session,
    long catalogID,
    DBRecordSetParams dbrsp)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    List<long> longList = new List<long>();
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(session, catalogID);
    if (classifKeyByObjId == string.Empty)
      return (DataTable) null;
    ConditionStructure[] conditions = dbrsp.Conditions;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    conditionStructureList.Add(new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, conditions == null || conditions.Length == 0 ? LogicalOperators.NONE : LogicalOperators.AND, conditions != null ? ((IEnumerable<ConditionStructure>) conditions).Max<ConditionStructure>((System.Func<ConditionStructure, int>) (item => item.GroupID)) + 1 : 0, false));
    if (conditions != null)
      conditionStructureList.AddRange((IEnumerable<ConditionStructure>) conditions);
    dbrsp.Conditions = conditionStructureList.ToArray();
    return DataHelper.GetObjectData((IEnumerable<int>) Consts.Imbase_NavTree_ObjectTypeIDS, session, dbrsp, (IEnumerable<long>) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="status"></param>
  /// <returns></returns>
  public static string GetMessage(ScanOldKeyStatus status)
  {
    switch (status)
    {
      case ScanOldKeyStatus.CatalogNotFound:
        return LocalizationHolder.rm.GetString("Interfaces.Imbase_4");
      case ScanOldKeyStatus.CatalogRecordNotFound:
        return LocalizationHolder.rm.GetString("Interfaces.Imbase_5");
      case ScanOldKeyStatus.TableRecordNotFound:
        return LocalizationHolder.rm.GetString("Interfaces.Imbase_6");
      case ScanOldKeyStatus.BadImbaseKey:
        return LocalizationHolder.rm.GetString("Interfaces.Imbase_3");
      default:
        return string.Empty;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attId"></param>
  /// <returns></returns>
  public static bool IsSystemAttribute(int attId)
  {
    return attId == Consts.ImbaseTableViewAttID || attId == Consts.CatalogTypeAttID || attId == Consts.ObjectSortOrderAttID || attId == Consts.ImbaseTableRefAttID || attId == Consts.ImbaseTableRowsTypeAttID || attId == Consts.ImbaseTableDataAttID || attId == Consts.ImbaseObjectRefAttID || attId == Consts.ImbaseTableRecordOwnerAttID;
  }

  /// <summary>Формирование внутреннего ключа IMBASE.</summary>
  /// <param name="linkId">Идентификатор ссылки на таблицу</param>
  /// <param name="recordId">Номер строки в таблице</param>
  /// <returns>Внутренний ключ IMBASE</returns>
  public static string MakeInternalImbaseKey(long linkId, long recordId)
  {
    return $"IK{linkId}.{recordId}";
  }

  /// <summary>Получение таблицы данных с учетом локальных типов.</summary>
  /// <param name="session">Сессия</param>
  /// <param name="rParams">Набобр параметров</param>
  /// <param name="baseTypeID">Идентификатор базового типа объекта</param>
  /// <returns>Таблица значений</returns>
  public static DataTable SelectObjects(
    IUserSession session,
    DBRecordSetParams rParams,
    int baseTypeID)
  {
    return DataHelper.GetObjectData(baseTypeID, session, rParams, (IEnumerable<long>) null);
  }

  /// <summary>Получение таблицы данных с учетом локальных типов.</summary>
  /// <param name="session">Сессия</param>
  /// <param name="rParams">Набобр параметров</param>
  /// <param name="baseTypeID">Идентификатор базового типа объекта</param>
  /// <returns>Таблица значений</returns>
  public static DataTable SelectObjects(
    IUserSession session,
    DBRecordSetParams rParams,
    int[] baseTypeIDs)
  {
    return DataHelper.GetObjectData((IEnumerable<int>) baseTypeIDs, session, rParams, (IEnumerable<long>) null);
  }

  /// <summary>Получение данных по объектам.</summary>
  /// <remarks>Параметры и условия меняеются, часть параметров не передается в запрос - копируем только следующее:
  /// 
  /// Conditions
  /// Columns
  /// FailIfNotFound
  /// LastKeyValue
  /// LastOrderValue
  /// RecordCount
  /// TableName
  /// Tags
  /// 
  /// так что осторожно - на свой свой страх и риск.
  /// Сортировка объектов не работает, т.к. для локальных объектов отдельные запросы.
  /// </remarks>
  /// <param name="objTypeIDs">Список идентификаторов типов объектов</param>
  /// <param name="userSession">Сессия пользователя</param>
  /// <param name="dbRSP">Параметры выборки</param>
  /// <param name="objIDList">Перечень идентификаторов версий объектов (опциональный)</param>
  /// <returns>Таблица с данными</returns>
  public static DataTable SelectObjectsEx(
    IEnumerable<int> objTypeIDs,
    IUserSession userSession,
    DBRecordSetParams dbRSP,
    IEnumerable<ObjInfoItem> objIDList)
  {
    return DataHelper.GetObjectDataEx(objTypeIDs, userSession, dbRSP, objIDList);
  }

  /// <summary>
  /// Проверяет, является ли указаный атрибут внутренним атрибутом IMBASE,
  /// который не надо использовать при создании объекта из заготовки.
  /// </summary>
  /// <param name="attId">Идентификатор атрибута</param>
  /// <returns>true если атрибут не надо использовать, иначе false</returns>
  public static bool SkipAtttribute(int attId)
  {
    if (ImbaseHelper._skipList == null)
      ImbaseHelper.MakeSkipList();
    return ImbaseHelper._skipList.Contains(attId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="keyValue"></param>
  /// <returns></returns>
  public static string ConvertImbaseKey(IUserSession session, string keyValue)
  {
    bool isGuidKey = false;
    return ImbaseHelper.ConvertImbaseKey(session, keyValue, out isGuidKey);
  }

  /// <summary>Конвертация ключа IMBASE.</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="keyValue">Ключ IMBASE</param>
  /// <param name="isGuidKey">true, если в возвращаемом ключе используется GUID</param>
  /// <returns>Сконвертированный ключ</returns>
  public static string ConvertImbaseKey(IUserSession session, string keyValue, out bool isGuidKey)
  {
    string str1 = string.Empty;
    isGuidKey = false;
    if (session != null && keyValue.StartsWith("IK", StringComparison.InvariantCultureIgnoreCase))
    {
      int num = keyValue.IndexOf('.');
      if (num > -1)
      {
        string str2 = keyValue.Substring(2, num - 2);
        string newValue = string.Empty;
        new QuickObjectInfo().ObjectTypeID = -1;
        if (GuidHelper.IsGuid(str2))
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(str2));
          newValue = !objectInfo.Empty ? objectInfo.ObjectID.ToString() : string.Empty;
        }
        else
        {
          long result = 0;
          if (long.TryParse(str2, out result))
          {
            QuickObjectInfo objectInfo = session.GetObjectInfo(result);
            newValue = !objectInfo.Empty ? objectInfo.VersionGuid.ToString() : string.Empty;
            isGuidKey = true;
          }
        }
        if (!string.IsNullOrEmpty(newValue))
          str1 = keyValue.Replace(str2, newValue);
      }
    }
    return str1;
  }

  public static long CreateCategoryId(long tableId, long recordId)
  {
    return tableId << 20 | recordId & 1048575L /*0x0FFFFF*/;
  }

  public static void GetObjectAndId(long categoryId, out long objectId, out int id)
  {
    objectId = categoryId >> 20;
    id = (int) categoryId & 1048575 /*0x0FFFFF*/;
  }

  public static long MinCategoryId(long tableId) => tableId << 20;

  public static long MaxCategoryId(long tableId)
  {
    return ImbaseHelper.MinCategoryId(tableId) | 1048575L /*0x0FFFFF*/;
  }

  /// <summary>
  /// Получить идентификатор объекта IMBASE и номер записи (если это ссылка на таблицу IMBASE), на который ссылается указанный объект.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="imbaseObjID">Идентификатор объекта IMBASE, на который ссылается исходный объект</param>
  /// <param name="recID">Номер записи, если исходный объект ссылается на объект типа "Ссылка на таблицу IMBASE"</param>
  /// <returns>true - если объект ссылается на объект IMBASE</returns>
  public static bool GetImbaseDataFromObject(
    IUserSession session,
    long objID,
    ref long imbaseObjID,
    ref long recID)
  {
    bool imbaseDataFromObject = false;
    if (objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(Consts.ImbaseObjectRefAttID);
          if (attributeById1 != null)
          {
            if (attributeById1.Values[0] != null)
            {
              if (attributeById1.Values[0] != DBNull.Value)
              {
                imbaseObjID = attributeById1.AsInteger;
                if (imbaseDataFromObject = imbaseObjID != 0L)
                {
                  IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(Consts.ImbaseInternalOldKeyAttID);
                  if (attributeById2 != null)
                  {
                    if (attributeById2.Values[0] != null)
                    {
                      if (attributeById2.Values[0] != DBNull.Value)
                        recID = attributeById2.AsInteger;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return imbaseDataFromObject;
  }

  /// <summary>
  /// Получить идентификатор типа связи по умолчанию между указанными типа объектов.
  /// </summary>
  /// <param name="ownerTypeID">Идентификатор родительского типа объектов</param>
  /// <param name="childTypeID">Идентификатор дочернего типа объектов</param>
  /// <returns>Идентификатор типа связи</returns>
  public static int GetDefaultRelationTypeID(int ownerTypeID, int childTypeID)
  {
    int defaultRelationTypeId = -1;
    if (ownerTypeID != -1 && childTypeID != -1)
    {
      List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(ownerTypeID);
      if (typeApplicabilities != null)
      {
        IMSApplicability imsApplicability = typeApplicabilities.FirstOrDefault<IMSApplicability>((System.Func<IMSApplicability, bool>) (x => x.ChildObjectTypeID == childTypeID));
        if (imsApplicability != null)
          defaultRelationTypeId = imsApplicability.RelationTypeID;
      }
    }
    return defaultRelationTypeId;
  }

  /// <summary>
  /// Проверка на возможность типа объекта содержать указанный атрибут.
  /// </summary>
  /// <param name="typeID">Идентификатор типа объектов</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Результат проверки</returns>
  public static bool CanObjectTypeContainAttribute(int typeID, int attrID)
  {
    bool flag = true;
    if (MetaDataHelper.GetAttribute4ObjectType(typeID, attrID) == null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(typeID);
      flag = objectType != null && objectType.AnyAttributes;
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="isAdmin"></param>
  public static void SetIsAdmin(bool isAdmin) => ImbaseHelper._isAdmin = isAdmin;

  /// <summary>
  /// Получить список идентификаторов ярлыков, которые входят в папку с указанным идентификатором.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="folderID">Идентификатор папки IMBASE</param>
  /// <returns>Список идентификаторов ярлыков IMBASE</returns>
  public static List<long> GetLinksEntersInFolder(IUserSession session, long folderID)
  {
    List<long> linksEntersInFolder = (List<long>) null;
    DataTable source = (session.GetObjectCollection(Consts.ImbaseTableRefTypeGUID) ?? throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_TableRefType_NullCollection"))).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) folderID, LogicalOperators.AND, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    })
    {
      Contents = new ColumnContents[1]{ ColumnContents.ID }
    });
    if (source != null)
      linksEntersInFolder = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
    return linksEntersInFolder;
  }
}
