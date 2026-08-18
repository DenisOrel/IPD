
// Type: Intermech.Data.EntityDb.EntityQueryFilter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Data.EntityDb
{
    public sealed class EntityQueryFilter
    {
      private EntitySet allowedEntities;
      private EntitySet deniedEntities;
      private LinkedList<Predicate<IEntity>> codeFilters;
      private bool enabled;

      internal void Assign(EntityQueryFilter other)
      {
        if (other == null)
          throw new ArgumentNullException(nameof (other));
        this.Clear();
        if (other.allowedEntities != null)
          this.CombineWithAllowedEntities(new EntitySet((IEnumerable<IEntity>) other.allowedEntities));
        if (other.deniedEntities != null)
          this.CombineWithDeniedEntities(new EntitySet((IEnumerable<IEntity>) other.deniedEntities));
        if (other.codeFilters == null)
          return;
        for (LinkedListNode<Predicate<IEntity>> linkedListNode = other.codeFilters.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
          this.CombineWithCodeFilter(linkedListNode.Value);
      }

      public bool Pass(IEntity entity)
      {
        if (this.enabled)
        {
          if (this.allowedEntities != null && !this.allowedEntities.Contains(entity) || this.deniedEntities != null && this.deniedEntities.Contains(entity))
            return false;
          if (this.codeFilters != null)
          {
            LinkedListNode<Predicate<IEntity>> linkedListNode = this.codeFilters.First;
            while (linkedListNode != null && linkedListNode.Value(entity))
              linkedListNode = linkedListNode.Next;
            if (linkedListNode != null)
              return false;
          }
        }
        return true;
      }

      public void CombineWithAllowedEntities(EntitySet entitySet)
      {
        if (entitySet == null)
          throw new ArgumentNullException(nameof (entitySet));
        if (this.allowedEntities == null)
          this.allowedEntities = entitySet;
        else
          this.enabled = true;
        this.allowedEntities.IntersectWith((IEnumerable<IEntity>) entitySet);
        this.enabled = true;
      }

      public void CombineWithDeniedEntities(EntitySet entitySet)
      {
        if (entitySet == null)
          throw new ArgumentNullException(nameof (entitySet));
        if (this.deniedEntities == null)
          this.deniedEntities = entitySet;
        else
          this.deniedEntities.UnionWith((IEnumerable<IEntity>) entitySet);
        this.enabled = true;
      }

      public void CombineWithCodeFilter(Predicate<IEntity> filter)
      {
        if (filter == null)
          throw new ArgumentNullException(nameof (filter));
        if (this.codeFilters == null)
          this.codeFilters = new LinkedList<Predicate<IEntity>>();
        this.codeFilters.AddLast(filter);
        this.enabled = true;
      }

      public void Clear()
      {
        this.allowedEntities = (EntitySet) null;
        this.deniedEntities = (EntitySet) null;
        this.codeFilters = (LinkedList<Predicate<IEntity>>) null;
        this.enabled = false;
      }

      public bool IsAllEntitiesDenied(int dbItemsCount)
      {
        return this.enabled && (this.allowedEntities != null && this.allowedEntities.Count == 0 || this.deniedEntities != null && this.deniedEntities.Count == dbItemsCount);
      }

      public bool Enabled => this.enabled;

      public override string ToString()
      {
        if (!this.enabled)
          return "<empty>";
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (this.allowedEntities != null)
            stringBuilder.AppendFormat("allowed entities: {0} items", (object) this.allowedEntities.Count);
          if (this.deniedEntities != null)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            stringBuilder.AppendFormat("denied entities: {0} items", (object) this.deniedEntities.Count);
          }
          if (this.codeFilters != null)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            stringBuilder.AppendFormat("code filters: {0} items", (object) this.codeFilters.Count);
          }
          return stringBuilder.ToString();
        }
      }
    }
}
