
// Type: Intermech.Data.EntityDb.Common.EntityIndexerBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb.Common
{
    public abstract class EntityIndexerBase : IEntityIndexer
    {
      private EntityDatabase db;

      public virtual void Initialize(EntityDatabase database)
      {
        this.db = database != null ? database : throw new ArgumentNullException(nameof (database));
      }

      protected virtual void CheckIndexerState()
      {
        if (this.db == null)
          throw new InvalidOperationException("Indexer must be initialized first.");
      }

      public void AddToIndex(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        this.CheckIndexerState();
        if (!this.IsEntitySupported(entity))
          return;
        this.DoAddToIndex(entity);
      }

      public void DeleteFromIndex(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        this.CheckIndexerState();
        if (!this.IsEntitySupported(entity))
          return;
        this.DoDeleteFromIndex(entity);
      }

      protected abstract bool IsEntitySupported(IEntity entity);

      protected abstract void DoAddToIndex(IEntity entity);

      protected abstract void DoDeleteFromIndex(IEntity entity);

      public EntitySet Query(EntityQuery query, IQueryCondition condition)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        if (condition == null)
          throw new ArgumentNullException(nameof (condition));
        this.CheckIndexerState();
        return this.DoQuery(query, condition);
      }

      protected virtual EntitySet DoQuery(EntityQuery query, IQueryCondition condition)
      {
        return (EntitySet) null;
      }
    }
}
