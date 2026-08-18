
// Type: Intermech.Data.EntityDb.Common.IndexQueryEngine`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    internal sealed class IndexQueryEngine<TProperty, TKey>
    {
      private readonly IIndexKeyProvider<TProperty, TKey> indexKeyProvider;
      private readonly IIndexKeyScanner<TKey> indexScanner;

      public IndexQueryEngine(
        IIndexKeyProvider<TProperty, TKey> indexKeyProvider,
        IIndexKeyScanner<TKey> indexScanner)
      {
        if (indexKeyProvider == null)
          throw new ArgumentNullException(nameof (indexKeyProvider));
        if (indexScanner == null)
          throw new ArgumentNullException(nameof (indexScanner));
        this.indexKeyProvider = indexKeyProvider;
        this.indexScanner = indexScanner;
      }

      public EntitySet Query(EntityQuery query, IQueryCondition condition)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        switch (condition)
        {
          case null:
            throw new ArgumentNullException(nameof (condition));
          case BinaryCondition _:
            return this.QueryBinaryCondition(query, (BinaryCondition) condition);
          case BetweenCondition _:
            return this.QueryBetweenCondition(query, (BetweenCondition) condition);
          default:
            throw new NotSupportedException();
        }
      }

      private EntitySet QueryBinaryCondition(EntityQuery query, BinaryCondition condition)
      {
        switch (condition.Operator)
        {
          case BinaryOperator.Equal:
            return this.QueryEqualCondition(query, condition);
          case BinaryOperator.In:
            return this.QueryInCondition(query, condition);
          case BinaryOperator.Less:
            return this.QueryLessCondition(query, condition, false);
          case BinaryOperator.LessOrEqual:
            return this.QueryLessCondition(query, condition, true);
          case BinaryOperator.Greater:
            return this.QueryGreaterCondition(query, condition, false);
          case BinaryOperator.GreaterOrEqual:
            return this.QueryGreaterCondition(query, condition, true);
          default:
            throw new NotSupportedException();
        }
      }

      private EntitySet QueryEqualCondition(EntityQuery query, BinaryCondition condition)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        TKey indexKey = this.CreateIndexKey(condition.Value);
        EntitySet result = new EntitySet();
        if (this.indexScanner.GetKeyCount() > 0)
        {
          EntitySet entities = this.indexScanner.ScanEntities(indexKey);
          if (entities.Count > 0)
            this.CollectEntities(query, entities, result);
        }
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(result.Count);
        return result;
      }

      private EntitySet QueryInCondition(EntityQuery query, BinaryCondition condition)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        EntitySet result = new EntitySet();
        if (this.indexScanner.GetKeyCount() > 0)
        {
          foreach (object propertyValue in (IEnumerable) condition.Value)
          {
            EntitySet entities = this.indexScanner.ScanEntities(this.CreateIndexKey(propertyValue));
            if (entities.Count > 0)
            {
              this.CollectEntities(query, entities, result);
              if (query.RecordLimitEnabled)
              {
                if (result.Count >= query.RecordLimit)
                  break;
              }
            }
          }
        }
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(result.Count);
        return result;
      }

      private EntitySet QueryLessCondition(
        EntityQuery query,
        BinaryCondition condition,
        bool inclusive)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        IIndexRangeScanner<TKey> rangeScanner = this.GetRangeScanner();
        TKey indexKey = this.CreateIndexKey(condition.Value);
        EntitySet result = new EntitySet();
        if (this.indexScanner.GetKeyCount() > 0)
          this.CollectEntities(query, rangeScanner.ScanRangeTo(indexKey, inclusive), result);
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(result.Count);
        return result;
      }

      private EntitySet QueryGreaterCondition(
        EntityQuery query,
        BinaryCondition condition,
        bool inclusive)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        IIndexRangeScanner<TKey> rangeScanner = this.GetRangeScanner();
        TKey indexKey = this.CreateIndexKey(condition.Value);
        EntitySet result = new EntitySet();
        if (this.indexScanner.GetKeyCount() > 0)
          this.CollectEntities(query, rangeScanner.ScanRangeFrom(indexKey, inclusive), result);
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(result.Count);
        return result;
      }

      private EntitySet QueryBetweenCondition(EntityQuery query, BetweenCondition condition)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        IIndexRangeScanner<TKey> rangeScanner = this.GetRangeScanner();
        TKey indexKey1 = this.CreateIndexKey(condition.LeftValue);
        TKey indexKey2 = this.CreateIndexKey(condition.RightValue);
        EntitySet result = new EntitySet();
        if (this.indexScanner.GetKeyCount() > 0)
          this.CollectEntities(query, rangeScanner.ScanRange(indexKey1, true, indexKey2, true), result);
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(result.Count);
        return result;
      }

      private IIndexRangeScanner<TKey> GetRangeScanner()
      {
        return this.indexScanner is IIndexRangeScanner<TKey> indexScanner ? indexScanner : throw new NotSupportedException();
      }

      private TKey CreateIndexKey(object propertyValue)
      {
        return this.indexKeyProvider.FromQueryCondition(propertyValue) ?? throw new NotSupportedException("Null values are not supported.");
      }

      private void CollectEntities(
        EntityQuery query,
        IEnumerable<KeyValuePair<TKey, EntitySet>> keyRange,
        EntitySet result)
      {
        foreach (KeyValuePair<TKey, EntitySet> keyValuePair in keyRange)
        {
          if (keyValuePair.Value.Count > 0)
          {
            this.CollectEntities(query, keyValuePair.Value, result);
            if (query.RecordLimitEnabled && result.Count >= query.RecordLimit)
              break;
          }
        }
      }

      private void CollectEntities(EntityQuery query, EntitySet entities, EntitySet result)
      {
        if (query.RecordLimitEnabled || query.Filter.Enabled)
          this.CollectAndFilterEntities(query, entities, result);
        else
          this.CollectAllEntities(query, entities, result);
      }

      private void CollectAndFilterEntities(EntityQuery query, EntitySet entities, EntitySet result)
      {
        if (TraceHelper.QueryCode.TraceVerbose)
          TraceHelper.TraceIndexRangeStart(entities, false);
        int addCount = 0;
        foreach (IEntity entity in (HashSet<IEntity>) entities)
        {
          if (query.Filter.Pass(entity))
          {
            result.Add(entity);
            ++addCount;
          }
          if (query.RecordLimitEnabled && result.Count >= query.RecordLimit)
          {
            if (!TraceHelper.QueryCode.TraceVerbose)
              break;
            TraceHelper.TraceIndexRangeBreak(addCount);
            break;
          }
        }
      }

      private void CollectAllEntities(EntityQuery query, EntitySet entities, EntitySet result)
      {
        if (TraceHelper.QueryCode.TraceVerbose)
          TraceHelper.TraceIndexRangeStart(entities, true);
        result.UnionWith((IEnumerable<IEntity>) entities);
      }
    }
}
