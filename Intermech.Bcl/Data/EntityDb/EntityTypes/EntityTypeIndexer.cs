
// Type: Intermech.Data.EntityDb.EntityTypes.EntityTypeIndexer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb.Common;


namespace Intermech.Data.EntityDb.EntityTypes
{
    public sealed class EntityTypeIndexer : EntityIndexerBase
    {
      private EntityTypeIndex index;

      public override void Initialize(EntityDatabase database)
      {
        base.Initialize(database);
        this.index = new EntityTypeIndex();
      }

      protected override bool IsEntitySupported(IEntity entity) => true;

      protected override void DoAddToIndex(IEntity entity)
      {
        this.index.AddValue(entity, entity.GetType());
      }

      protected override void DoDeleteFromIndex(IEntity entity)
      {
        this.index.RemoveValue(entity, entity.GetType());
      }

      protected override EntitySet DoQuery(EntityQuery query, IQueryCondition condition)
      {
        return condition is PropertyValueCondition condition1 && condition1.PropertyReference.Equals(EntityVirtualProperties.EntityTypeRef) ? this.QueryEntityTypeCondition(query, condition1) : base.DoQuery(query, condition);
      }

      private EntitySet QueryEntityTypeCondition(EntityQuery query, PropertyValueCondition condition)
      {
        return this.index.Query(query, (IQueryCondition) condition);
      }
    }
}
