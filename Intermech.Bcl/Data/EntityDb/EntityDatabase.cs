
// Type: Intermech.Data.EntityDb.EntityDatabase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Data.EntityDb
{
    public class EntityDatabase : ICollection<IEntity>, IEnumerable<IEntity>, IEnumerable
    {
      private int queryLevel;
      private readonly Dictionary<long, IEntity> entityStore;
      private readonly IEntityIndexer indexer;

      public EntityDatabase(params IEntityIndexer[] indexers)
      {
        if (indexers == null)
          throw new ArgumentNullException(nameof (indexers));
        this.entityStore = new Dictionary<long, IEntity>();
        this.indexer = indexers.Length != 1 || indexers[0] == null ? (IEntityIndexer) new CompositeEntityIndexer((ICollection<IEntityIndexer>) indexers) : indexers[0];
        this.indexer.Initialize(this);
      }

      public EntitySet Query(IQueryCondition condition) => this.Query(new EntityQuery(), condition);

      public EntitySet Query(EntityQuery query, IQueryCondition condition)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        if (condition == null)
          throw new ArgumentNullException(nameof (condition));
        long num = 0;
        if (this.queryLevel == 0 && TraceHelper.QueryTime.Enabled)
        {
          if (TraceHelper.QueryCode.TraceInfo)
            Trace.WriteLine("EntityDatabase query started...");
          num = Stopwatch.GetTimestamp();
        }
        ++this.queryLevel;
        try
        {
          EntitySet entitySet = this.QueryCore(query, condition);
          if (this.queryLevel == 1 && TraceHelper.QueryTime.Enabled)
            Trace.WriteLine(string.Format("EntityDatabase query finished in {0} ms, record limit {1}, condition ({3}) with filter '{2}'", (object) (1000.0 * (double) (Stopwatch.GetTimestamp() - num) / (double) Stopwatch.Frequency), (object) query.RecordLimit, (object) query.Filter, (object) condition));
          return entitySet;
        }
        finally
        {
          --this.queryLevel;
        }
      }

      private EntitySet QueryCore(EntityQuery query, IQueryCondition condition)
      {
        if (query.Filter.IsAllEntitiesDenied(this.Count))
        {
          if (TraceHelper.QueryCode.TraceInfo)
            Trace.Write("AND-ing optimization: filter denies all database items, result is empty set.");
          return new EntitySet();
        }
        if (query.RecordLimitEnabled && query.RecordLimit >= this.Count)
        {
          if (TraceHelper.QueryCode.TraceInfo)
            Trace.Write("RecordLimit optimization: record limit is greater than total count of database items. It will be disabled.");
          query = query.Clone();
          query.RecordLimit = 0;
        }
        EntitySet entitySet = this.indexer.Query(query, condition);
        if (entitySet != null)
          return entitySet;
        switch (condition)
        {
          case CodeCondition _:
            return this.QueryCodeCondition(query, (CodeCondition) condition);
          case CompoundSetCondition _:
            return this.QueryCompoundSetCondition(query, (CompoundSetCondition) condition);
          default:
            return new EntitySet();
        }
      }

      private EntitySet QueryCodeCondition(EntityQuery query, CodeCondition condition)
      {
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionCode(query, (IQueryCondition) condition);
        if (TraceHelper.QueryCode.TraceVerbose)
          TraceHelper.TraceIndexRangeStart(this.Count, false);
        EntitySet entitySet = new EntitySet();
        foreach (IEntity entity in this)
        {
          if (condition.Filter(entity) && query.Filter.Pass(entity))
            entitySet.Add(entity);
          if (query.RecordLimitEnabled && entitySet.Count >= query.RecordLimit)
          {
            if (TraceHelper.QueryCode.TraceVerbose)
            {
              TraceHelper.TraceIndexRangeBreak(entitySet.Count);
              break;
            }
            break;
          }
        }
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceConditionResult(entitySet.Count);
        return entitySet;
      }

      private EntitySet QueryCompoundSetCondition(EntityQuery query, CompoundSetCondition condition)
      {
        switch (condition.Operator)
        {
          case CompoundSetOperator.Union:
            return this.QueryUnionCondition(query, condition);
          case CompoundSetOperator.Intersection:
            return this.QueryIntersectionCondition(query, condition);
          case CompoundSetOperator.Complement:
            return this.QueryComplementCondition(query, condition);
          default:
            throw new NotImplementedException();
        }
      }

      private EntitySet QueryUnionCondition(EntityQuery query, CompoundSetCondition condition)
      {
        if (condition.SubConditions.Count < 2)
          throw new EntityDatabaseException("Bad subconditions count.");
        if (condition.SubConditions.Count > 2)
        {
          if (TraceHelper.QueryCode.TraceInfo)
            TraceHelper.TraceCompoundSetBrackets("UNION", condition.SubConditions.Count, "A or B or C... -> (A or B) or C...");
          int index = condition.SubConditions.Count - 1;
          CompoundSetCondition aCondition = condition.Clone();
          aCondition.SubConditions.RemoveAt(index);
          IQueryCondition subCondition = condition.SubConditions[index];
          return this.QueryUnionCondition(query, (IQueryCondition) aCondition, subCondition);
        }
        IQueryCondition subCondition1 = condition.SubConditions[0];
        IQueryCondition subCondition2 = condition.SubConditions[1];
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceCompoundSetOperator("UNION", subCondition1, subCondition2);
        return this.QueryUnionCondition(query, subCondition1, subCondition2);
      }

      private EntitySet QueryUnionCondition(
        EntityQuery query,
        IQueryCondition aCondition,
        IQueryCondition bCondition)
      {
        EntitySet entitySet = this.Query(query, aCondition);
        if (entitySet.Count == this.Count)
          return entitySet;
        int num = query.RecordLimitEnabled ? query.RecordLimit - entitySet.Count : int.MaxValue;
        if (num > 0)
        {
          EntityQuery query1 = query.Clone();
          query1.RecordLimit = num;
          EntitySet other = this.Query(query1, bCondition);
          entitySet.UnionWith((IEnumerable<IEntity>) other);
        }
        return entitySet;
      }

      private EntitySet QueryIntersectionCondition(EntityQuery query, CompoundSetCondition condition)
      {
        if (condition.SubConditions.Count < 2)
          throw new EntityDatabaseException("Bad subconditions count.");
        if (condition.SubConditions.Count > 2)
        {
          if (TraceHelper.QueryCode.TraceInfo)
            TraceHelper.TraceCompoundSetBrackets("INTERSECTION", condition.SubConditions.Count, "A and B and C... -> (A and B) and C...");
          int index = condition.SubConditions.Count - 1;
          CompoundSetCondition aCondition = condition.Clone();
          aCondition.SubConditions.RemoveAt(index);
          IQueryCondition subCondition = condition.SubConditions[index];
          return this.QueryIntersectionCondition(query, (IQueryCondition) aCondition, subCondition);
        }
        IQueryCondition subCondition1 = condition.SubConditions[0];
        IQueryCondition subCondition2 = condition.SubConditions[1];
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceCompoundSetOperator("INTERSECTION", subCondition1, subCondition2);
        return this.QueryIntersectionCondition(query, subCondition1, subCondition2);
      }

      private EntitySet QueryIntersectionCondition(
        EntityQuery query,
        IQueryCondition aCondition,
        IQueryCondition bCondition)
      {
        if (bCondition is CodeCondition)
        {
          Predicate<IEntity> filter = ((CodeCondition) bCondition).Filter;
          EntityQuery query1 = query.Clone();
          query1.Filter.CombineWithCodeFilter(filter);
          return this.Query(query1, aCondition);
        }
        EntityQuery query2 = query.Clone();
        query.RecordLimit = 0;
        EntitySet entitySet = this.Query(query2, aCondition);
        if (entitySet.Count == 0)
          return entitySet;
        EntityQuery query3 = query.Clone();
        if (entitySet.Count < this.Count)
          query3.Filter.CombineWithAllowedEntities(entitySet);
        return this.Query(query3, bCondition);
      }

      private EntitySet QueryComplementCondition(EntityQuery query, CompoundSetCondition condition)
      {
        if (condition.SubConditions.Count < 2)
          throw new EntityDatabaseException("Bad subconditions count.");
        if (condition.SubConditions.Count > 2)
        {
          if (TraceHelper.QueryCode.TraceInfo)
            TraceHelper.TraceCompoundSetBrackets("COMPLEMENT", condition.SubConditions.Count, "A \\ B \\ C... = (A \\ B) \\ C...");
          int index = condition.SubConditions.Count - 1;
          CompoundSetCondition aCondition = condition.Clone();
          aCondition.SubConditions.RemoveAt(index);
          IQueryCondition subCondition = condition.SubConditions[index];
          return this.QueryIntersectionCondition(query, (IQueryCondition) aCondition, subCondition);
        }
        IQueryCondition subCondition1 = condition.SubConditions[0];
        IQueryCondition subCondition2 = condition.SubConditions[1];
        if (TraceHelper.QueryCode.TraceInfo)
          TraceHelper.TraceCompoundSetOperator("COMPLEMENT", subCondition1, subCondition2);
        return this.QueryComplementCondition(query, subCondition1, subCondition2);
      }

      private EntitySet QueryComplementCondition(
        EntityQuery query,
        IQueryCondition aCondition,
        IQueryCondition bCondition)
      {
        if (bCondition is CodeCondition)
        {
          Predicate<IEntity> filter = (Predicate<IEntity>) (entity => !((CodeCondition) bCondition).Filter(entity));
          EntityQuery query1 = query.Clone();
          query1.Filter.CombineWithCodeFilter(filter);
          return this.Query(query1, aCondition);
        }
        EntityQuery query2 = query.Clone();
        query.RecordLimit = 0;
        query2.Filter.Clear();
        EntitySet entitySet = this.Query(query2, bCondition);
        if (entitySet.Count == this.Count)
          return new EntitySet();
        EntityQuery query3 = query.Clone();
        if (entitySet.Count > 0)
          query3.Filter.CombineWithDeniedEntities(entitySet);
        return this.Query(query3, aCondition);
      }

      public void Add(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        if (entity.Database == null)
          this.AddNew(entity);
        else if (entity.Database != this)
          throw new EntityDatabaseException("This entity is already added in another database.");
      }

      private void AddNew(IEntity entity)
      {
        try
        {
          entity.Database = this;
          entity.UniqueId = (long) RuntimeId.Create();
          this.entityStore.Add(entity.UniqueId, entity);
          this.indexer.AddToIndex(entity);
        }
        catch
        {
          this.RemoveCore(entity);
          throw;
        }
      }

      public bool Remove(IEntity entity)
      {
        if (entity == null)
          return false;
        return entity.Database == this ? this.Remove(entity.UniqueId) : throw new EntityDatabaseException("This entity is already owned by another database.");
      }

      public bool Remove(long entityId)
      {
        IEntity entity;
        if (entityId <= 0L || !this.entityStore.TryGetValue(entityId, out entity))
          return false;
        this.RemoveCore(entity);
        return true;
      }

      private void RemoveCore(IEntity entity)
      {
        this.indexer.DeleteFromIndex(entity);
        this.entityStore.Remove(entity.UniqueId);
        entity.Database = (EntityDatabase) null;
        entity.UniqueId = 0L;
      }

      public bool Contains(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        return entity.Database == this;
      }

      public void CopyTo(IEntity[] array, int index) => this.entityStore.Values.CopyTo(array, index);

      public void Clear()
      {
        if (this.entityStore.Count <= 0)
          return;
        long[] array = new long[this.entityStore.Count];
        this.entityStore.Keys.CopyTo(array, 0);
        foreach (long entityId in array)
          this.Remove(entityId);
      }

      public bool IsReadOnly => false;

      public IEnumerator<IEntity> GetEnumerator()
      {
        return (IEnumerator<IEntity>) new EntityEnumerator((IEnumerator<KeyValuePair<long, IEntity>>) this.entityStore.GetEnumerator());
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new EntityEnumerator((IEnumerator<KeyValuePair<long, IEntity>>) this.entityStore.GetEnumerator());
      }

      public int Count => this.entityStore.Count;

      private struct EntityEnumerator(
        IEnumerator<KeyValuePair<long, IEntity>> dictEnumerator) : 
        IEnumerator,
        IEnumerator<IEntity>,
        IDisposable
      {
        private readonly IEnumerator<KeyValuePair<long, IEntity>> dictEnumerator = dictEnumerator;

        public void Dispose() => this.dictEnumerator.Dispose();

        public bool MoveNext() => this.dictEnumerator.MoveNext();

        public void Reset() => this.dictEnumerator.Reset();

        public IEntity Current => this.dictEnumerator.Current.Value;

        object IEnumerator.Current => (object) this.dictEnumerator.Current.Value;
      }
    }
}
