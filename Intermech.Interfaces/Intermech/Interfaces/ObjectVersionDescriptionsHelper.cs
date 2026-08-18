
// Type: Intermech.Interfaces.ObjectVersionDescriptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Статический вспомогательный класс, позволяющий загружать описания для версий объектов.
    /// Класс позволяет загружать описания в списки любых типов, унаследованных от типа
    /// ObjectVersionDescription
    /// </summary>
    public static class ObjectVersionDescriptionsHelper
    {
      /// <summary>
      /// Разместить все элементы списка items в порядке, указанном в списке versions
      /// </summary>
      /// <param name="items">Элементы, унаследованные от ObjectVersionDescription</param>
      /// <param name="versions">Идентификаторы версий объектов</param>
      /// <returns>Упорядоченный список</returns>
      private static List<object> OrderItems(List<object> items, IList<long> versions)
      {
        if (items == null || versions == null || items.Count == 0)
          return (List<object>) null;
        List<object> objectList = new List<object>(items.Count);
        Dictionary<long, object> dictionary = new Dictionary<long, object>();
        for (int index = 0; index < items.Count; ++index)
        {
          if (!(items[index] is ObjectVersionDescription versionDescription))
            return (List<object>) null;
          dictionary[versionDescription.F_OBJECT_ID] = items[index];
        }
        for (int index = 0; index < versions.Count; ++index)
        {
          if (dictionary.ContainsKey(versions[index]))
            objectList.Add(dictionary[versions[index]]);
        }
        return objectList;
      }

      /// <summary>Загрузить описания указанной версии объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="version">Идентификатор версии объекта, для которой надо загрузить описание</param>
      /// <returns>Описание указанной версии объекта</returns>
      public static object LoadDescription(IUserSession session, Type T, long version)
      {
        object obj = (object) null;
        if (session == null || version == 0L || T == (Type) null || !(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return obj;
        IDBObject source = session.GetObject(version, false);
        if (source == null)
          return obj;
        instance.Assign((object) source);
        if (instance.F_OBJECT_ID != 0L)
          obj = (object) instance;
        return obj;
      }

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="versions">Список версий, для которых надо загрузить описания</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <returns>Отсортированный список описаний версий объектов</returns>
      public static List<object> LoadDescriptions(
        IUserSession session,
        Type T,
        IList<long> versions,
        int objectType)
      {
        List<object> objectList = ObjectVersionDescriptionsHelper.LoadUnsortedDescriptions(session, T, versions, objectType);
        objectList.Sort();
        return objectList;
      }

      /// <summary>
      /// Загрузить описания всех версий объектов указанного типа
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <returns>Отсортированный список описаний версий объектов</returns>
      public static List<object> LoadDescriptions(IUserSession session, Type T, int objectType)
      {
        List<object> objectList = ObjectVersionDescriptionsHelper.LoadUnsortedDescriptions(session, T, objectType);
        objectList.Sort();
        return objectList;
      }

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="versions">Список версий, для которых надо загрузить описания</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadUnsortedDescriptions(
        IUserSession session,
        Type T,
        IList<long> versions,
        int objectType)
      {
        List<object> items = new List<object>();
        if (session == null || versions == null || versions.Count == 0 || T == (Type) null)
          return items;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return items;
        if (versions.Count <= 5)
        {
          for (int index = 0; index < versions.Count; ++index)
          {
            if (!longList.Contains(versions[index]))
            {
              longList.Add(versions[index]);
              IDBObject source = session.GetObject(versions[index], false);
              if (source != null)
              {
                instance.Assign((object) source);
                if (instance.F_OBJECT_ID != 0L)
                  items.Add(instance.Clone());
              }
            }
          }
          return items;
        }
        DataTable dataTable = (DataTable) null;
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[instance.GetColumnDescriptors().Count];
        instance.GetColumnDescriptors().CopyTo(columnDescriptorArray);
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        long[] numArray = new long[versions.Count];
        versions.CopyTo(numArray, 0);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, true)
        }, columnDescriptorArray);
        IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
        try
        {
          if (objectCollection != null)
            dataTable = objectCollection.Select(paramSet);
        }
        catch
        {
        }
        if (dataTable != null)
        {
          try
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              DataRow row = dataTable.Rows[index];
              long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
              if (int64Value != 0L && !longList.Contains(int64Value))
              {
                instance.Assign((object) row);
                if (instance.F_OBJECT_ID != 0L)
                  items.Add((object) (instance.Clone() as ObjectVersionDescription));
                longList.Add(int64Value);
              }
            }
          }
          finally
          {
            dataTable.Dispose();
          }
        }
        if (items.Count < versions.Count)
        {
          List<long> localObjects = new List<long>((IEnumerable<long>) versions);
          items.ForEach((Action<object>) (item => localObjects.Remove(((ObjectVersionDescription) item).F_OBJECT_ID)));
          List<object> collection = ObjectVersionDescriptionsHelper.LoadObjectDescriptionsSlow(session, T, (IList<long>) localObjects);
          if (collection.Count > 0)
            items.AddRange((IEnumerable<object>) collection);
        }
        return ObjectVersionDescriptionsHelper.OrderItems(items, versions);
      }

      /// <summary>
      /// Загрузить описания указанных версий объектов (с помощью вызова метода GetObject для каждой версии)
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="versions">Список версий, для которых надо загрузить описания</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadObjectDescriptionsSlow(
        IUserSession session,
        Type T,
        IList<long> versions)
      {
        List<object> objectList = new List<object>();
        if (session == null || versions == null || versions.Count == 0 || T == (Type) null)
          return objectList;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return objectList;
        for (int index = 0; index < versions.Count; ++index)
        {
          if (!longList.Contains(versions[index]))
          {
            longList.Add(versions[index]);
            IDBObject objectActualCopy = session.GetObjectActualCopy(versions[index], false);
            if (objectActualCopy != null)
            {
              instance.Assign((object) objectActualCopy);
              if (instance.F_OBJECT_ID != 0L)
                objectList.Add(instance.Clone());
            }
          }
        }
        return objectList;
      }

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="versions">Список версий, для которых надо загрузить описания</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadObjectDescriptionsFast(
        IUserSession session,
        Type T,
        SortedDictionary<int, List<long>> versions)
      {
        List<object> objectList = new List<object>();
        if (session == null || versions == null || versions.Count == 0 || T == (Type) null)
          return objectList;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return objectList;
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[instance.GetColumnDescriptors().Count];
        instance.GetColumnDescriptors().CopyTo(columnDescriptorArray);
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
        objectCollection.ShowAllModifications = true;
        foreach (KeyValuePair<int, List<long>> version in versions)
        {
          if (version.Value.Count != 0)
          {
            long[] numArray = new long[version.Value.Count];
            version.Value.CopyTo(numArray, 0);
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-2, RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, true)
            }, columnDescriptorArray);
            objectCollection.ObjectTypeID = version.Key;
            DataTable dataTable = (DataTable) null;
            try
            {
              if (objectCollection != null)
                dataTable = objectCollection.Select(paramSet);
            }
            catch
            {
            }
            if (dataTable != null)
            {
              try
              {
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                  DataRow row = dataTable.Rows[index];
                  long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
                  if (int64Value != 0L && longList.IndexOf(int64Value) < 0)
                  {
                    instance.Assign((object) row);
                    if (instance.F_OBJECT_ID != 0L)
                      objectList.Add((object) (instance.Clone() as ObjectVersionDescription));
                    longList.Add(int64Value);
                  }
                }
              }
              finally
              {
                dataTable.Dispose();
              }
            }
          }
        }
        return objectList;
      }

      /// <summary>
      /// Загрузить описания всех версий объектов указанного типа
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadUnsortedDescriptions(IUserSession session, Type T, int objectType)
      {
        return ObjectVersionDescriptionsHelper.LoadUnsortedDescriptions(session, T, objectType, false);
      }

      /// <summary>
      /// Загрузить описания всех версий объектов указанного типа
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <param name="showAllModifications">Выключить фильтрацию контекстных версий, которые в данный момент невидимы в Навигаторе</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadUnsortedDescriptions(
        IUserSession session,
        Type T,
        int objectType,
        bool showAllModifications)
      {
        List<object> objectList = new List<object>();
        if (session == null || T == (Type) null)
          return objectList;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return objectList;
        DataTable dataTable = (DataTable) null;
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[instance.GetColumnDescriptors().Count];
        instance.GetColumnDescriptors().CopyTo(columnDescriptorArray);
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], columnDescriptorArray);
        IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
        try
        {
          if (objectCollection != null)
          {
            objectCollection.ShowAllModifications = showAllModifications;
            dataTable = objectCollection.Select(paramSet);
          }
        }
        catch
        {
        }
        if (dataTable != null)
        {
          try
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              DataRow row = dataTable.Rows[index];
              long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
              if (int64Value != 0L && !longList.Contains(int64Value))
              {
                instance.Assign((object) row);
                if (instance.F_OBJECT_ID != 0L)
                  objectList.Add((object) (instance.Clone() as ObjectVersionDescription));
                longList.Add(int64Value);
              }
            }
          }
          finally
          {
            dataTable.Dispose();
          }
        }
        return objectList;
      }

      /// <summary>Загрузить описания версий указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="F_ID">Идентификатор объекта, для которого требуется получить описания его версий</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <returns>Отсортированный список описаний версий объектов</returns>
      public static List<object> LoadDescriptions(
        IUserSession session,
        Type T,
        long F_ID,
        int objectType)
      {
        return ObjectVersionDescriptionsHelper.LoadDescriptions(session, T, F_ID, objectType, false);
      }

      /// <summary>Загрузить описания версий указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="F_ID">Идентификатор объекта, для которого требуется получить описания его версий</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <param name="showAllModifications">Выключить фильтрацию контекстных версий, которые в данный момент невидимы в Навигаторе</param>
      /// <returns>Отсортированный список описаний версий объектов</returns>
      public static List<object> LoadDescriptions(
        IUserSession session,
        Type T,
        long F_ID,
        int objectType,
        bool showAllModifications)
      {
        List<object> objectList = new List<object>();
        if (session == null || F_ID == 0L || T == (Type) null)
          return objectList;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is ObjectVersionDescription instance))
          return objectList;
        DataTable dataTable = (DataTable) null;
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[instance.GetColumnDescriptors().Count];
        instance.GetColumnDescriptors().CopyTo(columnDescriptorArray);
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-3, RelationalOperators.Equal, (object) F_ID, LogicalOperators.NONE, 0, true)
        }, columnDescriptorArray);
        IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
        try
        {
          if (objectCollection != null)
          {
            objectCollection.ShowAllModifications = showAllModifications;
            dataTable = objectCollection.Select(paramSet);
          }
        }
        catch
        {
        }
        if (dataTable != null)
        {
          try
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              DataRow row = dataTable.Rows[index];
              long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
              if (int64Value != 0L && !longList.Contains(int64Value))
              {
                instance.Assign((object) row);
                if (instance.F_OBJECT_ID != 0L)
                  objectList.Add((object) (instance.Clone() as ObjectVersionDescription));
                longList.Add(int64Value);
              }
            }
          }
          finally
          {
            dataTable.Dispose();
          }
        }
        objectList.Sort();
        return objectList;
      }
    }
}
