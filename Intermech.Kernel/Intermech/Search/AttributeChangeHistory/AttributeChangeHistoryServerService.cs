// Decompiled with JetBrains decompiler
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryServerService : 
  LongLifeObject,
  IAttributeChangeHistoryServerService
{
  public AttributeChangeHistoryRecord[] FindRecords(
    Guid userSessionGuid,
    FindRecordsParams findRecordsParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return findRecordsParams != null ? this.FindRecords(findRecordsParams) : throw new ArgumentNullException(nameof (findRecordsParams));
  }

  private AttributeChangeHistoryRecord[] FindRecords(FindRecordsParams findRecordsParams)
  {
    List<AttributeChangeHistoryRecord> source = new List<AttributeChangeHistoryRecord>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[12]
        {
          (object) ObligatoryObjectAttributes.F_KEY,
          (object) ObligatoryObjectAttributes.F_ATTRIBUTE_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
          (object) ObligatoryObjectAttributes.F_SET_DATE,
          (object) ObligatoryObjectAttributes.F_USER_ID,
          (object) ObligatoryObjectAttributes.F_ID,
          (object) ObligatoryObjectAttributes.CAPTION,
          (object) ObligatoryObjectAttributes.F_STRING_VALUE,
          (object) ObligatoryObjectAttributes.F_INTEGER_VALUE,
          (object) ObligatoryObjectAttributes.F_DOUBLE_VALUE,
          (object) ObligatoryObjectAttributes.F_DATE_VALUE
        },
        Conditions = this.CreateConditionsFromFindRecordParams(findRecordsParams),
        SortColumns = findRecordsParams.SortColumns.Cast<object>().ToArray<object>(),
        Orders = findRecordsParams.SortOrders,
        LastKeyValue = findRecordsParams.LastRecordKey,
        RecordCount = -2
      };
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetHistoryCollection().Select(paramSet).Rows)
      {
        int int32Value1 = DataSetProcessor.GetInt32Value(row, 1, 0);
        int int32Value2 = DataSetProcessor.GetInt32Value(row, 2, -1);
        int int32Value3 = DataSetProcessor.GetInt32Value(row, 3, -1);
        DateTime dateTimeValue = DataSetProcessor.GetDateTimeValue(row, 4, DateTime.MinValue);
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 5, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 6, 0L);
        string stringValue1 = DataSetProcessor.GetStringValue(row, 7, (string) null);
        string stringValue2 = DataSetProcessor.GetStringValue(row, 8, (string) null);
        long? nullable1 = new long?();
        if (!this.IsNull(row[9]))
          nullable1 = new long?(DataSetProcessor.GetInt64Value(row, 9, 0L));
        double? nullable2 = new double?();
        if (!this.IsNull(row[10]))
          nullable2 = new double?(DataSetProcessor.GetDoubleValue(row, 10, 0.0));
        DateTime? nullable3 = new DateTime?();
        if (!this.IsNull(row[11]))
          nullable3 = new DateTime?(DataSetProcessor.GetDateTimeValue(row, 11, DateTime.MinValue));
        AttributeChangeHistoryRecord changeHistoryRecord = new AttributeChangeHistoryRecord()
        {
          AttributeTypeID = int32Value1,
          Date = dateTimeValue,
          ObjectTypeID = int32Value2,
          ObjectCaption = stringValue1,
          RelationTypeID = int32Value3,
          UserVersionID = int64Value1
        };
        if (!ObjectTypeHelper.IsUnknownObjectTypeID(changeHistoryRecord.ObjectTypeID))
          changeHistoryRecord.ObjectID = int64Value2;
        else
          changeHistoryRecord.RelationID = int64Value2;
        changeHistoryRecord.Value = stringValue2 == null ? (!nullable1.HasValue ? (!nullable2.HasValue ? (object) nullable3.Value : (object) nullable2.Value) : (object) nullable1.Value) : (object) stringValue2;
        source.Add(changeHistoryRecord);
      }
      if (source.Count > 0)
      {
        Tuple<long, long, string>[] objectsByObjectIds = this.FindObjectsByObjectIds(source.Select<AttributeChangeHistoryRecord, long>((System.Func<AttributeChangeHistoryRecord, long>) (o => o.ObjectID)).Distinct<long>().ToArray<long>());
        foreach (AttributeChangeHistoryRecord changeHistoryRecord in source)
        {
          AttributeChangeHistoryRecord record = changeHistoryRecord;
          Tuple<long, long, string>[] array = ((IEnumerable<Tuple<long, long, string>>) objectsByObjectIds).Where<Tuple<long, long, string>>((System.Func<Tuple<long, long, string>, bool>) (o => o.Item2 == record.ObjectID)).ToArray<Tuple<long, long, string>>();
          record.ObjectVersionIds = ((IEnumerable<Tuple<long, long, string>>) array).Select<Tuple<long, long, string>, long>((System.Func<Tuple<long, long, string>, long>) (o => o.Item1)).ToArray<long>();
          if (array.Length != 0)
          {
            Tuple<long, long, string> firstObject = ((IEnumerable<Tuple<long, long, string>>) array).First<Tuple<long, long, string>>();
            if (((IEnumerable<Tuple<long, long, string>>) array).All<Tuple<long, long, string>>((System.Func<Tuple<long, long, string>, bool>) (o => o.Item3 == firstObject.Item3)))
              record.ObjectCaption = firstObject.Item3;
          }
        }
        Tuple<long, long, string>[] objectVersionIds = this.FindObjectsByObjectVersionIds(source.Select<AttributeChangeHistoryRecord, long>((System.Func<AttributeChangeHistoryRecord, long>) (o => o.UserVersionID)).ToArray<long>());
        foreach (AttributeChangeHistoryRecord changeHistoryRecord in source)
        {
          AttributeChangeHistoryRecord record = changeHistoryRecord;
          Tuple<long, long, string> tuple = ((IEnumerable<Tuple<long, long, string>>) objectVersionIds).FirstOrDefault<Tuple<long, long, string>>((System.Func<Tuple<long, long, string>, bool>) (o => o.Item1 == record.UserVersionID));
          if (tuple != null)
            record.UserName = tuple.Item3;
        }
      }
    }
    return source.ToArray();
  }

  private ConditionStructure[] CreateConditionsFromFindRecordParams(
    FindRecordsParams findRecordsParams)
  {
    ConditionStructure[] existingConditions = new ConditionStructure[0];
    ConditionStructure joinedCondition;
    if (findRecordsParams.AttributeTypeIds != null && findRecordsParams.AttributeTypeIds.Length != 0)
    {
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_ATTRIBUTE_ID;
      joinedCondition.RelationalOperator = RelationalOperators.In;
      joinedCondition.Value = (object) findRecordsParams.AttributeTypeIds;
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.ObjectTypeIds != null && findRecordsParams.ObjectTypeIds.Length != 0)
    {
      List<int> intList = new List<int>();
      foreach (int objectTypeId in findRecordsParams.ObjectTypeIds)
      {
        if (!intList.Contains(objectTypeId))
          intList.Add(objectTypeId);
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId))
        {
          if (!intList.Contains(num))
            intList.Add(num);
        }
      }
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE;
      joinedCondition.RelationalOperator = RelationalOperators.In;
      joinedCondition.Value = (object) intList.ToArray();
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.RelationTypeIds != null && findRecordsParams.RelationTypeIds.Length != 0)
    {
      if (existingConditions.Length != 0)
        existingConditions[existingConditions.Length - 1].LogicalOperator = LogicalOperators.OR;
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_RELATION_TYPE;
      joinedCondition.RelationalOperator = RelationalOperators.In;
      joinedCondition.Value = (object) findRecordsParams.RelationTypeIds;
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.UserAndUserGroupsVersionIds != null && findRecordsParams.UserAndUserGroupsVersionIds.Length != 0)
    {
      long[] users = this.FindUsers(findRecordsParams.UserAndUserGroupsVersionIds);
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) -36;
      joinedCondition.RelationalOperator = RelationalOperators.In;
      joinedCondition.Value = (object) users;
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.ObjectVersionIds != null && findRecordsParams.ObjectVersionIds.Length != 0)
    {
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_ID;
      joinedCondition.RelationalOperator = RelationalOperators.In;
      joinedCondition.Value = (object) this.FindObjectIds(findRecordsParams.ObjectVersionIds);
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.From != DateTime.MinValue)
    {
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_SET_DATE;
      joinedCondition.RelationalOperator = RelationalOperators.GreaterOrEqual;
      joinedCondition.Value = (object) findRecordsParams.From;
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    if (findRecordsParams.To != DateTime.MinValue)
    {
      joinedCondition = new ConditionStructure();
      joinedCondition.Attribute = (object) ObligatoryObjectAttributes.F_SET_DATE;
      joinedCondition.RelationalOperator = RelationalOperators.LessOrEqual;
      joinedCondition.Value = (object) findRecordsParams.To;
      joinedCondition.SQL = string.Empty;
      existingConditions = ConditionStructure.Join(joinedCondition, existingConditions);
    }
    return existingConditions;
  }

  private long[] FindUsers(long[] userAndUserGroupsVersionIds)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long userGroupsVersionId in userAndUserGroupsVersionIds)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(userGroupsVersionId, false);
        if (dbObject != null)
        {
          if (dbObject.ObjectType == Constants.UserObjectTypeID)
          {
            if (!longList.Contains(userGroupsVersionId))
              longList.Add(userGroupsVersionId);
          }
          else
          {
            foreach (long user in this.FindUsers(userGroupsVersionId))
            {
              if (!longList.Contains(user))
                longList.Add(user);
            }
          }
        }
      }
    }
    return longList.ToArray();
  }

  private long[] FindUsers(long userGroupVersionID)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(Constants.SimpleRelationRelationTypeID);
      relationCollection.ObjectTypeID = Constants.UserObjectTypeID;
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }
      };
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, userGroupVersionID).Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        longList.Add(int64Value);
      }
    }
    return longList.ToArray();
  }

  private long[] FindObjectIds(long[] objectVersionIds)
  {
    return ((IEnumerable<Tuple<long, long, string>>) this.FindObjectsByObjectVersionIds(objectVersionIds)).Select<Tuple<long, long, string>, long>((System.Func<Tuple<long, long, string>, long>) (o => o.Item2)).Distinct<long>().ToArray<long>();
  }

  private bool IsNull(object value) => value == null && value is DBNull;

  private Tuple<long, long, string>[] FindObjectsByObjectIds(long[] objectIds)
  {
    return this.FindObjects(new ConditionStructure[1]
    {
      new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_ID,
        RelationalOperator = RelationalOperators.In,
        Value = (object) ((IEnumerable<long>) objectIds).Distinct<long>().ToArray<long>(),
        SQL = string.Empty
      }
    });
  }

  private Tuple<long, long, string>[] FindObjects(ConditionStructure[] conditions)
  {
    List<Tuple<long, long, string>> tupleList = new List<Tuple<long, long, string>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[3]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_ID,
          (object) ObligatoryObjectAttributes.CAPTION
        },
        Conditions = conditions,
        RecordCount = -1
      };
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        string stringValue = DataSetProcessor.GetStringValue(row, 2, (string) null);
        tupleList.Add(new Tuple<long, long, string>(int64Value1, int64Value2, stringValue));
      }
    }
    return tupleList.ToArray();
  }

  private Tuple<long, long, string>[] FindObjectsByObjectVersionIds(long[] objectVersionIds)
  {
    return this.FindObjects(new ConditionStructure[2]
    {
      new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        RelationalOperator = RelationalOperators.In,
        Value = (object) ((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>(),
        LogicalOperator = LogicalOperators.OR,
        SQL = string.Empty
      },
      new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        RelationalOperator = RelationalOperators.In,
        Value = (object) ((IEnumerable<long>) objectVersionIds).Distinct<long>().Select<long, long>((System.Func<long, long>) (o => -o)).ToArray<long>(),
        SQL = string.Empty
      }
    });
  }
}
