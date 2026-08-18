// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.Extensions
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Statistics.Interfaces;

public static class Extensions
{
  /// <summary>
  /// Получим список юзеров состоящих в данном подразделении, а так же список дочерних подразделений на будущий случай если будут добавлять уже имеющийся.
  /// </summary>
  /// <param name="departamentID">Идентификатор подразделения</param>
  /// <param name="departDt">Список дочерних подразделений</param>
  /// <param name="usersTable">Список пользователей состоящих в заданном подразделении</param>
  public static void GetUsersFromDepartament(
    object departamentID,
    out DataTable departDt,
    out DataTable usersTable)
  {
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545");
    int objectTypeId3 = MetaDataHelper.GetObjectTypeID("cadd9232-306c-11d8-b4e9-00304f19f545");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(objectTypeId1);
      IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(objectTypeId2);
      IDBObjectCollection objectCollection3 = sessionKeeper.Session.GetObjectCollection(objectTypeId3);
      departDt = Intermech.Statistics.Interfaces.Extensions.GetChildDemartamentRecursive(objectCollection3, departamentID);
      DataTable childGroupRecursive1 = Intermech.Statistics.Interfaces.Extensions.GetChildGroupRecursive(objectCollection2, departamentID);
      foreach (DataRow row in (InternalDataCollectionBase) departDt.Rows)
      {
        DataTable childGroupRecursive2 = Intermech.Statistics.Interfaces.Extensions.GetChildGroupRecursive(objectCollection2, row.ItemArray[0]);
        childGroupRecursive1.Merge(childGroupRecursive2);
      }
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      for (int index = 0; index < departDt.Rows.Count; ++index)
        conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, departDt.Rows[index].ItemArray[0], LogicalOperators.OR, 0, false));
      for (int index = 0; index < childGroupRecursive1.Rows.Count; ++index)
        conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, childGroupRecursive1.Rows[index].ItemArray[0], LogicalOperators.OR, 0, false));
      conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, departamentID, LogicalOperators.NONE, 0, false));
      usersTable = objectCollection1.Select(new DBRecordSetParams(conditionStructureList.ToArray(), new object[3]
      {
        (object) -2,
        (object) -3,
        (object) -50
      }));
    }
  }

  private static DataTable GetChildDemartamentRecursive(
    IDBObjectCollection departamentCollection,
    object conditionValue)
  {
    DataTable demartamentRecursive1 = new DataTable("Список всех дочерних подразделений");
    DataTable table = departamentCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, conditionValue, LogicalOperators.NONE, 0, false)
    }, new object[2]{ (object) -2, (object) -50 }));
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      DataTable demartamentRecursive2 = Intermech.Statistics.Interfaces.Extensions.GetChildDemartamentRecursive(departamentCollection, row.ItemArray[0]);
      table.Merge(demartamentRecursive2);
    }
    demartamentRecursive1.Merge(table);
    return demartamentRecursive1;
  }

  /// <summary>
  /// получаем таблицы со списками дочерних групп, и пользователей входящих в эти группы включая головную. Если передан идентификатор пользователя вернёт null вместо таблиц
  /// </summary>
  /// <param name="user">Идентификатор группы пользователей/пользователя для добавляения в ConditionStructure</param>
  /// <param name="groupDT"></param>
  /// <param name="usersTable"></param>
  public static void GetGroupAndUsersTable(
    object user,
    out DataTable groupDT,
    out DataTable usersTable)
  {
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObject(Convert.ToInt64(user)).ObjectType == MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"))
      {
        groupDT = (DataTable) null;
        usersTable = (DataTable) null;
      }
      else
      {
        IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(objectTypeId1);
        IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(objectTypeId2);
        groupDT = Intermech.Statistics.Interfaces.Extensions.GetChildGroupRecursive(objectCollection2, user);
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        for (int index = 0; index < groupDT.Rows.Count; ++index)
          conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, groupDT.Rows[index].ItemArray[0], LogicalOperators.OR, 0, false));
        conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, user, LogicalOperators.NONE, 0, false));
        usersTable = objectCollection1.Select(new DBRecordSetParams(conditionStructureList.ToArray(), new object[3]
        {
          (object) -2,
          (object) -3,
          (object) -50
        }));
      }
    }
  }

  /// <summary>
  /// Рекурсивно раскрутим и получим DataTable со списком групп которые входят в начальную группу
  /// </summary>
  /// <param name="groupCollection"></param>
  /// <param name="conditionValue"></param>
  /// <returns></returns>
  private static DataTable GetChildGroupRecursive(
    IDBObjectCollection groupCollection,
    object conditionValue)
  {
    DataTable childGroupRecursive1 = new DataTable("Список всех дочерних групп");
    DataTable table = groupCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, conditionValue, LogicalOperators.NONE, 0, false)
    }, new object[2]{ (object) -2, (object) -50 }));
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      DataTable childGroupRecursive2 = Intermech.Statistics.Interfaces.Extensions.GetChildGroupRecursive(groupCollection, row.ItemArray[0]);
      table.Merge(childGroupRecursive2);
    }
    childGroupRecursive1.Merge(table);
    return childGroupRecursive1;
  }

  /// <summary>Проверка является ли строка GUIDом</summary>
  /// <param name="guid"></param>
  /// <returns></returns>
  public static bool IsGuid(this string guid)
  {
    return !string.IsNullOrEmpty(guid) && Guid.TryParse(guid, out Guid _);
  }

  /// <summary>
  /// Определяет, входит ли в ObjectTypesListItem переданный тип как дочерний.
  /// </summary>
  /// <param name="items">Перечень итемов.</param>
  /// <param name="typeID">Тип объекта.</param>
  public static bool ContainsObjectTypeAsChild(this List<ObjectTypesListItem> items, int typeID)
  {
    foreach (ObjectTypesListItem objectTypesListItem in items)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(typeID, objectTypesListItem.ObjectTypeID))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Содержится ли в списке итемов конкретно указанный тип.
  /// </summary>
  /// <param name="items">Итемы.</param>
  /// <param name="typeID">ИД типа объекта.</param>
  public static bool ContainsObjectType(this List<ObjectTypesListItem> items, int typeID)
  {
    foreach (ObjectTypesListItem objectTypesListItem in items)
    {
      if (typeID == objectTypesListItem.ObjectTypeID)
        return true;
    }
    return false;
  }

  /// <summary>Получает список родительских групп для группы.</summary>
  /// <param name="groupId">ИД группы, для которой ищем родителей.</param>
  /// <returns>Список родительских групп для группы.</returns>
  public static List<long> GetParentGroupsForGroup(long groupId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> collection = new List<long>();
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      List<ObjInfoItem> partObjList = new List<ObjInfoItem>();
      partObjList.Add(new ObjInfoItem(groupId, StatisticsConst.GroupTypeID));
      IUserSession session = sessionKeeper.Session;
      List<int> relations = new List<int>(1);
      relations.Add(StatisticsConst.SimpleRelationTypeID);
      DBRecordSetParams dbRsp = dbRecordSetParams;
      string defFiltrationRule = DataHelper.Consts.cnt_def_filtrationRule;
      DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, session, (IEnumerable<int>) relations, -1, dbRsp, (VersionsRule) null, defFiltrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.GroupTypeID
      }, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.GroupTypeID
      });
      if (parentSostavData != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
            collection.SafeAdd<long>(int64);
        }
      }
      return collection;
    }
  }

  /// <summary>
  /// Получает список родительских подразделений для подразделения.
  /// </summary>
  /// <param name="departmentId">ИД подразделения, для которого ищем родителей.</param>
  /// <returns>Список родительских подразделений для подразделения.</returns>
  public static IEnumerable<long> GetParentDepartmentsForDepartment(long departmentId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> collection = new List<long>();
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      List<ObjInfoItem> partObjList = new List<ObjInfoItem>();
      partObjList.Add(new ObjInfoItem(departmentId, StatisticsConst.DepartmentTypeId));
      IUserSession session = sessionKeeper.Session;
      List<int> relations = new List<int>(1);
      relations.Add(StatisticsConst.SimpleRelationTypeID);
      DBRecordSetParams dbRsp = dbRecordSetParams;
      string defFiltrationRule = DataHelper.Consts.cnt_def_filtrationRule;
      DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, session, (IEnumerable<int>) relations, -1, dbRsp, (VersionsRule) null, defFiltrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.DepartmentTypeId
      }, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.DepartmentTypeId
      });
      if (parentSostavData != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
            collection.SafeAdd<long>(int64);
        }
      }
      return (IEnumerable<long>) collection;
    }
  }

  /// <summary>
  /// Проверка, входит ли добавляемая группа в уже имеющиеся в списке группы.
  /// </summary>
  /// <param name="groupId">ИД группы.</param>
  /// <param name="statisticsUsers">Список пользователей статистики.</param>
  /// <param name="name">Имя родительской группы, в которую входит добавляемая группа. (Если входит)</param>
  /// <returns>
  /// True, если добавляемая группа входит в группу из списка
  /// </returns>
  public static bool CheckInGroupForParents(
    long groupId,
    List<StatisticsUsers> statisticsUsers,
    out string name)
  {
    List<long> parentGroupsForGroup = Intermech.Statistics.Interfaces.Extensions.GetParentGroupsForGroup(groupId);
    foreach (StatisticsUsers statisticsUser in statisticsUsers)
    {
      if (parentGroupsForGroup.Contains(statisticsUser.ObjectID))
      {
        name = statisticsUser.Caption;
        return true;
      }
    }
    name = string.Empty;
    return false;
  }

  /// <summary>
  /// Проверка, входит ли добавляемое подразделение в уже имеющиеся в списке подразделения.
  /// </summary>
  /// <param name="departmentId">ИД подразделения.</param>
  /// <param name="statisticsUsers">Список пользователей статистики.</param>
  /// <param name="name">Имя родительского подразделения, в которое входит добавляемое подразделение. (Если входит)</param>
  /// <returns>
  /// True, если добавляемое подразделение входит в подразделение из списка
  /// </returns>
  public static bool CheckInDepartmentForParents(
    long departmentId,
    List<StatisticsUsers> statisticsUsers,
    out string name)
  {
    IEnumerable<long> departmentsForDepartment = Intermech.Statistics.Interfaces.Extensions.GetParentDepartmentsForDepartment(departmentId);
    foreach (StatisticsUsers statisticsUser in statisticsUsers)
    {
      if (departmentsForDepartment.Contains<long>(statisticsUser.ObjectID))
      {
        name = statisticsUser.Caption;
        return true;
      }
    }
    name = string.Empty;
    return false;
  }

  /// <summary>
  /// Отбирает подразделения, входящие в состав переданного подразделения.
  /// </summary>
  /// <param name="statisticsUsers">Подразделения для проверки в виде StatisticsUsers.</param>
  /// <param name="departmentId">Подразделение, детей которого ищем.</param>
  /// <returns>Подразделения, входящие в состав переданного подразделения.</returns>
  public static List<long> GetDepartmentsEntersInChoosedDepartmentRecursive(
    List<StatisticsUsers> statisticsUsers,
    long departmentId)
  {
    long[] array = statisticsUsers.Select<StatisticsUsers, long>((System.Func<StatisticsUsers, long>) (department => department.ObjectID)).ToArray<long>();
    List<long> collection = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) array, LogicalOperators.NONE, 0, false)
      }, columns);
      List<ObjInfoItem> projObjList = new List<ObjInfoItem>();
      projObjList.Add(new ObjInfoItem(departmentId, StatisticsConst.DepartmentTypeId));
      IUserSession session = sessionKeeper.Session;
      List<int> relations = new List<int>(1);
      relations.Add(StatisticsConst.SimpleRelationTypeID);
      DBRecordSetParams dbRsp = dbRecordSetParams;
      string defFiltrationRule = DataHelper.Consts.cnt_def_filtrationRule;
      DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) relations, -1, dbRsp, (VersionsRule) null, defFiltrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.DepartmentTypeId
      }, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.DepartmentTypeId
      });
      if (childSostavData != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
            collection.SafeAdd<long>(int64);
        }
      }
      return collection;
    }
  }

  /// <summary>Отбирает группы, входящие в состав переданной группы.</summary>
  /// <param name="statisticsUsers">Группы для проверки в виде StatisticsUsers.</param>
  /// <param name="groupId">Группы, детей которой ищем.</param>
  /// <returns>Группы, входящие в состав переданной группы.</returns>
  public static List<long> GetGroupsEntersInChoosedGroupRecursive(
    List<StatisticsUsers> statisticsUsers,
    long groupId)
  {
    long[] array = statisticsUsers.Select<StatisticsUsers, long>((System.Func<StatisticsUsers, long>) (department => department.ObjectID)).ToArray<long>();
    List<long> collection = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) array, LogicalOperators.NONE, 0, false)
      }, columns);
      List<ObjInfoItem> projObjList = new List<ObjInfoItem>();
      projObjList.Add(new ObjInfoItem(groupId, StatisticsConst.GroupTypeID));
      IUserSession session = sessionKeeper.Session;
      List<int> relations = new List<int>(1);
      relations.Add(StatisticsConst.SimpleRelationTypeID);
      DBRecordSetParams dbRsp = dbRecordSetParams;
      string defFiltrationRule = DataHelper.Consts.cnt_def_filtrationRule;
      DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) relations, -1, dbRsp, (VersionsRule) null, defFiltrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.GroupTypeID
      }, (IEnumerable<int>) new List<int>()
      {
        StatisticsConst.GroupTypeID
      });
      if (childSostavData != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
            collection.SafeAdd<long>(int64);
        }
      }
      return collection;
    }
  }
}
