
// Type: Intermech.Interfaces.Compositions.SimpleCompositionObjectHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Статический вспомогательный класс, позволяющий загружать описания для версий объектов составов.
    /// Класс позволяет загружать описания в списки любых типов, унаследованных от типа
    /// SimpleCompositionObject
    /// </summary>
    public static class SimpleCompositionObjectHelper
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
          if (!(items[index] is SimpleCompositionObject compositionObject))
            return (List<object>) null;
          dictionary[compositionObject.F_PRJLINK_ID] = items[index];
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
      /// <param name="linkID">Идентификатор связи</param>
      /// <returns>Описание указанной версии объекта</returns>
      public static object LoadDescription(IUserSession session, Type T, long version, long linkID)
      {
        object obj = (object) null;
        if (session == null || version == 0L || linkID == 0L || T == (Type) null || !(Activator.CreateInstance(T) is SimpleCompositionObject instance))
          return obj;
        IDBObject source = session.GetObject(version, false);
        IDBRelation relation = session.GetRelation(linkID, false);
        if (source == null || relation == null)
          return obj;
        instance.Assign((object) source);
        instance.F_PRJLINK_ID = relation.RelationID;
        instance.F_PROJ_ID = relation.ProjID;
        instance.F_RELATION_TYPE = relation.RelationType;
        if (instance.F_OBJECT_ID != 0L)
          obj = (object) instance;
        return obj;
      }

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="linkIDs">Список идентификаторов связей</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <param name="relType">Тип связи</param>
      /// <returns>Отсортированный список описаний версий объектов</returns>
      public static List<object> LoadDescriptions(
        IUserSession session,
        Type T,
        IList<long> linkIDs,
        int objectType,
        int relType)
      {
        List<object> objectList = SimpleCompositionObjectHelper.LoadUnsortedDescriptions(session, T, linkIDs, objectType, relType);
        objectList.Sort();
        return objectList;
      }

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="T">Тип, унаследованный от ObjectVersionDescription</param>
      /// <param name="linkIDs">Список иденификаторов связей</param>
      /// <param name="objectType">Если все версии принадлежат одному типу объекта, или имеют общий
      /// родительский тип, его обязательно надо указать, чтобы "ядро" могло использовать свою оптимизацию.
      /// Если тип неизвестен, следует указать -1</param>
      /// <param name="relType">Тип связи</param>
      /// <returns>Список описаний версий объектов</returns>
      public static List<object> LoadUnsortedDescriptions(
        IUserSession session,
        Type T,
        IList<long> linkIDs,
        int objectType,
        int relType)
      {
        List<object> items = new List<object>();
        if (session == null || linkIDs == null || linkIDs.Count == 0 || T == (Type) null)
          return items;
        List<long> longList = new List<long>();
        if (!(Activator.CreateInstance(T) is SimpleCompositionObject instance))
          return items;
        DataTable dataTable = (DataTable) null;
        ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[instance.GetColumnDescriptors().Count];
        instance.GetColumnDescriptors().CopyTo(columnDescriptorArray);
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        long[] numArray = new long[linkIDs.Count];
        linkIDs.CopyTo(numArray, 0);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-20, RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, true)
        }, columnDescriptorArray);
        IDBRelationCollection relationCollection = session.GetRelationCollection(relType);
        try
        {
          if (relationCollection != null)
          {
            relationCollection.ObjectTypeID = objectType;
            dataTable = relationCollection.Select(paramSet);
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
              instance.Assign((object) row);
              if (instance.F_OBJECT_ID != 0L)
                items.Add((object) (instance.Clone() as SimpleCompositionObject));
            }
          }
          finally
          {
            dataTable.Dispose();
          }
        }
        return SimpleCompositionObjectHelper.OrderItems(items, linkIDs);
      }
    }
}
