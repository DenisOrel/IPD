
// Type: Intermech.Data.EntityDb.Common.UniversalIndex`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb.Common
{
    public class UniversalIndex<TProperty, TKey> : IIndex<TProperty>
    {
      private readonly IIndexKeyProvider<TProperty, TKey> indexKeyProvider;
      private readonly IDirectIndex<TKey> directIndex;
      private readonly IInverseIndex<TKey> inverseIndex;
      private readonly IndexQueryEngine<TProperty, TKey> queryEngine;

      public UniversalIndex(
        IIndexKeyProvider<TProperty, TKey> indexKeyProvider,
        IDirectIndex<TKey> directIndex,
        IInverseIndex<TKey> inverseIndex)
      {
        if (indexKeyProvider == null)
          throw new ArgumentNullException(nameof (indexKeyProvider));
        if (directIndex == null)
          throw new ArgumentNullException(nameof (directIndex));
        if (inverseIndex == null)
          throw new ArgumentNullException(nameof (inverseIndex));
        this.indexKeyProvider = indexKeyProvider;
        this.directIndex = directIndex;
        this.inverseIndex = inverseIndex;
        this.queryEngine = new IndexQueryEngine<TProperty, TKey>(indexKeyProvider, (IIndexKeyScanner<TKey>) this.directIndex);
      }

      public void AddValue(IEntity entity, TProperty propertyValue)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        TKey indexKey = this.CreateIndexKey(propertyValue);
        this.directIndex.AddValue(entity, indexKey);
        this.inverseIndex.AddValue(entity.UniqueId, indexKey);
      }

      public void RemoveValue(IEntity entity, TProperty propertyValue)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        TKey indexKey = this.CreateIndexKey(propertyValue);
        this.directIndex.RemoveValue(entity, indexKey);
        this.inverseIndex.RemoveValue(entity.UniqueId, indexKey);
      }

      public void RemoveAllValues(IEntity entity)
      {
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        foreach (TKey enumerateKey in this.inverseIndex.EnumerateKeys(entity.UniqueId))
          this.directIndex.RemoveValue(entity, enumerateKey);
        this.inverseIndex.RemoveAllValues(entity.UniqueId);
      }

      public EntitySet Query(EntityQuery query, IQueryCondition condition)
      {
        return this.queryEngine.Query(query, condition);
      }

      private TKey CreateIndexKey(TProperty propertyValue)
      {
        return this.indexKeyProvider.FromEntityValue(propertyValue) ?? throw new NotSupportedException("Null values are not supported.");
      }
    }
}
