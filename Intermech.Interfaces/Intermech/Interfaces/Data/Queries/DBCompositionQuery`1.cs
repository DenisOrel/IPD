
// Type: Intermech.Interfaces.Data.Queries.DBCompositionQuery`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Data.Queries
{
    public class DBCompositionQuery<TResult> : IDBQuery, ICloneable
    {
      private int relationType;
      private VersionsRulePackage versionsRule;
      private int recordCount;
      private DBQueryAttributes attributes;
      private List<ConditionStructure> conditions;
      private int resultObjectTypeFilter;
      private DBQueryRecordBuilder<TResult> resultBuilder;

      public DBCompositionQuery()
      {
        this.relationType = -1;
        this.resultObjectTypeFilter = -1;
        this.attributes = new DBQueryAttributes();
        this.conditions = new List<ConditionStructure>();
      }

      public DBCompositionQuery<TResult> Clone()
      {
        DBCompositionQuery<TResult> compositionQuery = new DBCompositionQuery<TResult>();
        compositionQuery.RelationType = this.RelationType;
        compositionQuery.VersionsRule = this.VersionsRule;
        compositionQuery.RecordCount = this.RecordCount;
        compositionQuery.Attributes.AddRange(this.Attributes);
        compositionQuery.Conditions.AddRange((IEnumerable<ConditionStructure>) this.Conditions);
        compositionQuery.ResultObjectTypeFilter = this.ResultObjectTypeFilter;
        compositionQuery.ResultBuilder = this.ResultBuilder;
        return compositionQuery;
      }

      object ICloneable.Clone() => (object) this.Clone();

      public int RelationType
      {
        get => this.relationType;
        set => this.relationType = value;
      }

      public VersionsRulePackage VersionsRule
      {
        get => this.versionsRule;
        set => this.versionsRule = value;
      }

      public int RecordCount
      {
        get => this.recordCount;
        set => this.recordCount = value;
      }

      public DBQueryAttributes Attributes => this.attributes;

      public List<ConditionStructure> Conditions => this.conditions;

      public int ResultObjectTypeFilter
      {
        get => this.resultObjectTypeFilter;
        set => this.resultObjectTypeFilter = value;
      }

      public DBQueryRecordBuilder<TResult> ResultBuilder
      {
        get => this.resultBuilder;
        set => this.resultBuilder = value;
      }

      public List<TResult> ConsistFrom(long objectId)
      {
        if (objectId == 0L)
          throw new ArgumentException();
        return this.DoQuery((Func<IDBRelationCollection, DBRecordSetParams, DataTable>) ((coll, queryParams) => coll.ConsistFrom(queryParams, objectId)));
      }

      private List<TResult> DoQuery(
        Func<IDBRelationCollection, DBRecordSetParams, DataTable> queryAction)
      {
        this.ValidateProperties();
        List<DBQueryAttribute> source = new List<DBQueryAttribute>((IEnumerable<DBQueryAttribute>) this.attributes);
        this.resultBuilder.AttachQuery((IDBQuery) this);
        try
        {
          List<object> objectList = new List<object>(this.attributes.Count);
          List<Intermech.Kernel.Search.ColumnInfo> columnInfoList = new List<Intermech.Kernel.Search.ColumnInfo>(this.attributes.Count);
          List<ColumnContents> columnContentsList = new List<ColumnContents>(this.attributes.Count);
          List<ColumnNameMapping> columnNameMappingList = new List<ColumnNameMapping>(this.attributes.Count);
          foreach (DBQueryAttribute attribute in this.attributes)
          {
            objectList.Add((object) attribute.Item1);
            columnInfoList.Add(new Intermech.Kernel.Search.ColumnInfo((object) attribute.Item1, attribute.Item2, (object) null));
            columnContentsList.Add(attribute.Item3);
            columnNameMappingList.Add(ColumnNameMapping.Name);
          }
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
          dbRecordSetParams.RecordCount = this.recordCount;
          dbRecordSetParams.Columns = objectList.ToArray();
          dbRecordSetParams.ColumnsInfo = columnInfoList.ToArray();
          dbRecordSetParams.Contents = columnContentsList.ToArray();
          dbRecordSetParams.ColumnNames = columnNameMappingList.ToArray();
          if (this.conditions.Count != 0)
            dbRecordSetParams.Conditions = this.conditions.ToArray();
          DataTable dataTable;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.relationType);
            relationCollection.FiltrationOwnerID = this.versionsRule.OwnerId;
            relationCollection.ObjectTypeID = this.resultObjectTypeFilter;
            dataTable = queryAction(relationCollection, dbRecordSetParams);
          }
          List<TResult> resultList = new List<TResult>(dataTable.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            TResult result = this.resultBuilder.Build(row);
            resultList.Add(result);
          }
          return resultList;
        }
        finally
        {
          this.resultBuilder.DetachQuery();
          this.attributes.Clear();
          this.attributes.AddRange((IEnumerable<DBQueryAttribute>) source);
        }
      }

      private void ValidateProperties()
      {
        if (this.versionsRule == null)
          throw new InvalidOperationException("Для выполнения запроса в базу IPS требуется, чтобы было задано свойство VersionsRule.");
        if (this.resultBuilder == null)
          throw new InvalidOperationException("Для выполнения запроса в базу IPS требуется, чтобы было задано свойство ResultBuilder.");
      }
    }
}
