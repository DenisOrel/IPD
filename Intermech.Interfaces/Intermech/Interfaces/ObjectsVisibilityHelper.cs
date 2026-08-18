
// Type: Intermech.Interfaces.ObjectsVisibilityHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный класс</summary>
    public static class ObjectsVisibilityHelper
    {
      /// <summary>
      /// Статическая коллекция, позволяющая по идентификаторам объектов получать их краткие описания
      /// </summary>
      internal static Dictionary<long, MyObjectElement> Names = new Dictionary<long, MyObjectElement>();
      internal static int linkSimpleSortId = -1;
      internal static int objTypeUserGroupId = 0;
      internal static int attrVisibilityId = 0;
      internal static int attrObjectType = 0;

      internal static void InitSimpleSortId(IUserSession ius)
      {
        if (ObjectsVisibilityHelper.linkSimpleSortId == -1)
          ObjectsVisibilityHelper.linkSimpleSortId = MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
        if (ObjectsVisibilityHelper.objTypeUserGroupId == 0)
          ObjectsVisibilityHelper.objTypeUserGroupId = MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545"));
        if (ObjectsVisibilityHelper.attrObjectType != 0)
          return;
        ObjectsVisibilityHelper.attrObjectType = MetaDataHelper.GetAttributeTypeID(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"));
      }

      internal static List<long> GetFullGroupList(IUserSession ius)
      {
        ObjectsVisibilityHelper.InitSimpleSortId(ius);
        List<long> groupList = new List<long>();
        ObjectsVisibilityHelper.CollectGroups(groupList, ius, ius.UserID);
        return groupList;
      }

      private static void CollectGroups(List<long> groupList, IUserSession ius, long Id)
      {
        DataTable dataTable = ius.GetRelationCollection(ObjectsVisibilityHelper.linkSimpleSortId).EntersInVersion(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(ObjectsVisibilityHelper.attrObjectType, RelationalOperators.Equal, (object) ObjectsVisibilityHelper.objTypeUserGroupId, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }), Id);
        if (dataTable == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!groupList.Contains(int64))
          {
            groupList.Add(int64);
            ObjectsVisibilityHelper.CollectGroups(groupList, ius, int64);
          }
        }
      }

      /// <summary>
      /// Извлечь из строки информацию о правах на видимость, заполнить словарик result
      /// </summary>
      /// <param name="data">Строка в кодированном виде</param>
      /// <returns>Права на видимость</returns>
      public static Dictionary<long, ObjectsVisibilityFlags> ParseString(string data)
      {
        Dictionary<long, ObjectsVisibilityFlags> result = new Dictionary<long, ObjectsVisibilityFlags>();
        ObjectsVisibilityHelper.ParseString(result, data);
        return result;
      }

      /// <summary>
      /// Извлечь из строки информацию о правах на видимость, заполнить словарик result
      /// </summary>
      /// <param name="result">Права на видимость</param>
      /// <param name="data">Строка в кодированном виде</param>
      public static void ParseString(Dictionary<long, ObjectsVisibilityFlags> result, string data)
      {
        if (result == null)
          return;
        result.Clear();
        if (data.Length == 0)
          return;
        StringBuilder stringBuilder = new StringBuilder();
        char ch1 = ' ';
        char ch2 = ' ';
        bool flag = false;
        for (int index = 0; index < data.Length; ++index)
        {
          char upper = char.ToUpper(data[index]);
          if (upper >= 'G')
          {
            if (ch2 == ' ' && stringBuilder.Length > 0)
              flag = true;
            if (ch1 == ' ')
            {
              ch1 = upper;
              flag = true;
            }
            if (!flag)
              continue;
          }
          if (upper >= '0' && upper <= '9' || upper >= 'A' && upper <= 'F')
            stringBuilder.Append(upper);
          if (flag)
          {
            if (ch2 == ' ')
              ch2 = 'G';
            long result1 = 0;
            if (!long.TryParse(stringBuilder.ToString(), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
              result1 = 0L;
            if (result1 != 0L)
            {
              ObjectsVisibilityFlags int32 = (ObjectsVisibilityFlags) Convert.ToInt32((int) ch2 - 71);
              result[result1] = int32;
            }
            ch2 = ch1;
            ch1 = ' ';
            flag = false;
            stringBuilder.Length = 0;
          }
        }
        if (stringBuilder.Length <= 0)
          return;
        if (ch2 == ' ')
          ch2 = 'G';
        long result2 = 0;
        if (!long.TryParse(stringBuilder.ToString(), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          result2 = 0L;
        if (result2 == 0L)
          return;
        ObjectsVisibilityFlags int32_1 = (ObjectsVisibilityFlags) Convert.ToInt32((int) ch2 - 71);
        result[result2] = int32_1;
      }

      /// <summary>
      /// Вернуть строку, в которой закодирована информация "Все пользователи" - "Видимо"
      /// </summary>
      /// <param name="allUsersID">Идентификатор группы "Все пользователи"</param>
      /// <returns>Строка, в которой закодирована информация "Все пользователи" - "Видимо"</returns>
      public static string AllUsersVisibility(long allUsersID)
      {
        char ch = Convert.ToChar((object) (ObjectsVisibilityFlags) 72);
        string str = allUsersID.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
        return ch.ToString() + str;
      }

      /// <summary>
      /// Закодировать словарик в виде строки, с учётом ограничения в 450 символов на строку
      /// </summary>
      /// <param name="data">Данные</param>
      /// <returns>Закодированная строка</returns>
      public static string ToString(Dictionary<long, ObjectsVisibilityFlags> data)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (data == null || data.Count == 0)
          return string.Empty;
        foreach (KeyValuePair<long, ObjectsVisibilityFlags> keyValuePair in data)
        {
          if (keyValuePair.Value != ObjectsVisibilityFlags.None)
          {
            char ch = Convert.ToChar((object) (keyValuePair.Value + 71));
            string str = keyValuePair.Key.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
            if (stringBuilder.Length + 1 + str.Length <= Consts.MaxStringSize)
            {
              stringBuilder.Append(ch);
              stringBuilder.Append(str);
            }
            else
              break;
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Закодировать словарик в виде строки с Guid, безо всяких ограничений на длину строки
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="data">Данные</param>
      /// <returns>Закодированная строка (идентификаторы представлены в виде Guid)</returns>
      public static string ToGuidsString(
        IUserSession session,
        Dictionary<long, ObjectsVisibilityFlags> data)
      {
        if (session == null)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        if (data == null || data.Count == 0)
          return string.Empty;
        foreach (KeyValuePair<long, ObjectsVisibilityFlags> keyValuePair in data)
        {
          if (keyValuePair.Value != ObjectsVisibilityFlags.None)
          {
            IDBObject dbObject = session.GetObject(keyValuePair.Key, false);
            if (dbObject != null)
            {
              char ch = Convert.ToChar((object) (keyValuePair.Value + 71));
              string str = dbObject.ObjectGUID.ToString();
              stringBuilder.Append(ch);
              stringBuilder.Append(str);
            }
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Отыскать в кэше информацию об указанном объекте. Если информации в кэше нет,
      /// но доступна сессия, обратиться за данными к серверу приложений
      /// </summary>
      /// <param name="session">Сессия или null</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Краткая информация об объекте или null</returns>
      public static MyObjectElement GetObjectInfo(IUserSession session, long objectID)
      {
        if (ObjectsVisibilityHelper.Names.ContainsKey(objectID))
          return ObjectsVisibilityHelper.Names[objectID];
        if (session == null)
          return (MyObjectElement) null;
        MyObjectElement objectInfo = new MyObjectElement();
        objectInfo.ObjectID = objectID;
        objectInfo.SyncObjectsData(session);
        if (objectInfo.ObjectID == 0L)
          return (MyObjectElement) null;
        ObjectsVisibilityHelper.Names.Add(objectInfo.ObjectID, objectInfo);
        return objectInfo;
      }

      /// <summary>Очистить кэш</summary>
      public static void Reset() => ObjectsVisibilityHelper.Names.Clear();

      public static bool? GetVisibilitySetting(
        long value,
        Dictionary<long, ObjectsVisibilityFlags> dict)
      {
        if (!dict.ContainsKey(value))
          return new bool?();
        switch (dict[value])
        {
          case ObjectsVisibilityFlags.Visible:
            return new bool?(true);
          case ObjectsVisibilityFlags.Hidden:
            return new bool?(false);
          default:
            return new bool?();
        }
      }

      public static bool IsShowAllowed(IUserSession ius, string showSettingsStr)
      {
        Dictionary<long, ObjectsVisibilityFlags> dict = ObjectsVisibilityHelper.ParseString(showSettingsStr);
        bool? visibilitySetting1 = ObjectsVisibilityHelper.GetVisibilitySetting(ius.UserID, dict);
        if (visibilitySetting1.HasValue)
          return visibilitySetting1.Value;
        foreach (long fullGroup in ObjectsVisibilityHelper.GetFullGroupList(ius))
        {
          bool? visibilitySetting2 = ObjectsVisibilityHelper.GetVisibilitySetting(fullGroup, dict);
          if (visibilitySetting2.HasValue)
            return visibilitySetting2.Value;
        }
        bool? visibilitySetting3 = ObjectsVisibilityHelper.GetVisibilitySetting(ius.RoleID, dict);
        return !visibilitySetting3.HasValue || visibilitySetting3.Value;
      }

      /// <summary>
      /// Возвращает результат объединения двух строк с настройками видимости объектов
      /// </summary>
      /// <param name="visibility1">Настройка видимости 1</param>
      /// <param name="visibility2">Настройка видимости 2</param>
      /// <returns></returns>
      private static string CombineVisibility(string visibility1, string visibility2)
      {
        return visibility1 + visibility2;
      }

      /// <summary>
      /// Метод копирует настройки видимости из архивов и проектов в объект docObject
      /// </summary>
      /// <param name="docObject">Объект, в который нужно скопировать настройки видимости</param>
      /// <param name="arcObject">Архив, из которого нужно скопировать видимость (если null, то не копируем)</param>
      /// <param name="projObject">Проект, из которого нужно скопировать видимость (если null, то не копируем)</param>
      public static void SetArcProjVisibility(
        IDBObject docObject,
        IDBObject arcObject,
        IDBObject projObject)
      {
        IDBAttribute attributeById1 = arcObject == null ? (IDBAttribute) null : arcObject.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId);
        IDBAttribute attributeById2 = projObject == null ? (IDBAttribute) null : projObject.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId);
        IDBAttribute attributeById3 = docObject.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId);
        string str = string.Empty;
        if (attributeById1 != null && attributeById2 != null)
          str = ObjectsVisibilityHelper.CombineVisibility(attributeById1.AsString, attributeById2.AsString);
        else if (attributeById1 != null)
          str = attributeById1.AsString;
        else if (attributeById2 != null)
          str = attributeById2.AsString;
        if (attributeById3 != null)
        {
          if (attributeById1 == null && attributeById2 == null)
            return;
          attributeById3.AsString = str;
        }
        else
        {
          if (!(str != string.Empty))
            return;
          docObject.Attributes.AddAttribute(ObjectsVisibilityHelper.AttrVisibilityId, false, new object[1]
          {
            (object) str
          });
        }
      }

      /// <summary>
      /// Метод записывает видимость объектов в указанный в tbl список объектов
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="tbl">Таблица со списком объектов - в row[0] ид. версии объектов</param>
      /// <param name="visibilityStr">Присваиваемая строка видимости</param>
      /// <param name="addProjectVisibility">Добавлять ли видимость проекта, если объект в проекте</param>
      public static void SetArcVisibility(
        IUserSession session,
        DataTable tbl,
        string visibilityStr,
        bool addProjectVisibility)
      {
        IDBObject dbObject1 = (IDBObject) null;
        foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
        {
          IDBObject dbObject2 = session.GetObject(Convert.ToInt64(row[0]), false);
          if (dbObject2 != null)
          {
            string str = visibilityStr;
            if (addProjectVisibility && dbObject2.ProjectID > 0L)
            {
              if (dbObject1 == null || dbObject1.ObjectID != dbObject2.ProjectID)
                dbObject1 = session.GetObject(dbObject2.ProjectID);
              IDBAttribute attributeById = dbObject1.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId);
              if (attributeById != null)
                str = ObjectsVisibilityHelper.CombineVisibility(visibilityStr, attributeById.AsString);
            }
            dbObject2.Attributes.AddAttribute(ObjectsVisibilityHelper.AttrVisibilityId, false, new object[1]
            {
              (object) str
            });
          }
        }
      }

      /// <summary>
      /// Метод записывает видимость объектов в указанный в tbl список объектов
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="tbl">Таблица со списком объектов - в row[0] ид. версии объектов</param>
      /// <param name="visibilityStr">Присваиваемая строка видимости</param>
      /// <param name="addArcVisibility">Добавлять ли видимость архивов, если объект в архиве</param>
      public static void SetProjVisibility(
        IUserSession session,
        DataTable tbl,
        string visibilityStr,
        bool addArcVisibility)
      {
        IDBObject dbObject1 = (IDBObject) null;
        int attributeId = session.IdentHelper.GetAttributeID(SystemGUIDs.attributeArchive.ToString());
        foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
        {
          IDBObject dbObject2 = session.GetObject(Convert.ToInt64(row[0]), false);
          if (dbObject2 != null)
          {
            string str = visibilityStr;
            if (addArcVisibility && MetaDataHelper.GetAttribute4ObjectType(Convert.ToInt32(row[1]), attributeId) != null)
            {
              IDBAttribute attributeById1 = dbObject2.GetAttributeByID(attributeId);
              if (attributeById1 != null && !attributeById1.IsNull && attributeById1.AsInteger > 0L)
              {
                if (dbObject1 == null || attributeById1.AsInteger != dbObject1.ObjectID)
                  dbObject1 = session.GetObject(attributeById1.AsInteger);
                IDBAttribute attributeById2 = dbObject1.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId);
                if (attributeById2 != null)
                  str = ObjectsVisibilityHelper.CombineVisibility(visibilityStr, attributeById2.AsString);
              }
            }
            dbObject2.Attributes.AddAttribute(ObjectsVisibilityHelper.AttrVisibilityId, false, new object[1]
            {
              (object) str
            });
          }
        }
      }

      /// <summary>Идентификатор атрибута Видимость</summary>
      public static int AttrVisibilityId
      {
        get
        {
          ObjectsVisibilityHelper.InitVisibilityAttrId();
          return ObjectsVisibilityHelper.attrVisibilityId;
        }
      }

      internal static void InitVisibilityAttrId()
      {
        if (ObjectsVisibilityHelper.attrVisibilityId != 0)
          return;
        ObjectsVisibilityHelper.attrVisibilityId = MetaDataHelper.GetAttributeTypeID(new Guid("cad0062f-306c-11d8-b4e9-00304f19f545"));
      }

      public static bool IsShowAllowed(IUserSession ius, long objID)
      {
        ObjectsVisibilityHelper.InitVisibilityAttrId();
        IDBObject dbObject = ius.GetObject(objID, false);
        if (dbObject == null)
          return false;
        IDBAttribute attributeById = dbObject.GetAttributeByID(ObjectsVisibilityHelper.attrVisibilityId);
        if (attributeById == null)
          return true;
        string showSettingsStr = Convert.ToString(attributeById.Value);
        return ObjectsVisibilityHelper.IsShowAllowed(ius, showSettingsStr);
      }
    }
}
