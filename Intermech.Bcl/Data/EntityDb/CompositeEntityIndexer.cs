
// Type: Intermech.Data.EntityDb.CompositeEntityIndexer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb
{
    public sealed class CompositeEntityIndexer : IEntityIndexer
    {
      private readonly List<IEntityIndexer> indexers;
      private EntityDatabase db;

      public CompositeEntityIndexer(ICollection<IEntityIndexer> indexers)
      {
        this.indexers = indexers != null ? new List<IEntityIndexer>(indexers.Count) : throw new ArgumentNullException(nameof (indexers));
        foreach (IEntityIndexer indexer in (IEnumerable<IEntityIndexer>) indexers)
        {
          if (indexer != null)
            this.indexers.Add(indexer);
        }
      }

      public void Initialize(EntityDatabase database)
      {
        this.db = database != null ? database : throw new ArgumentNullException(nameof (database));
        for (int index = 0; index < this.indexers.Count; ++index)
          this.indexers[index].Initialize(database);
      }

      private void CheckIndexerState()
      {
        if (this.db == null)
          throw new InvalidOperationException("Indexer must be initialized first.");
      }

      public void AddToIndex(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        this.CheckIndexerState();
        for (int index = 0; index < this.indexers.Count; ++index)
          this.indexers[index].AddToIndex(entity);
      }

      public void DeleteFromIndex(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        this.CheckIndexerState();
        for (int index = 0; index < this.indexers.Count; ++index)
          this.indexers[index].DeleteFromIndex(entity);
      }

      public EntitySet Query(EntityQuery query, IQueryCondition condition)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        if (condition == null)
          throw new ArgumentNullException(nameof (condition));
        this.CheckIndexerState();
        for (int index = 0; index < this.indexers.Count; ++index)
        {
          EntitySet entitySet = this.indexers[index].Query(query, condition);
          if (entitySet != null)
            return entitySet;
        }
        return (EntitySet) null;
      }
    }
}
