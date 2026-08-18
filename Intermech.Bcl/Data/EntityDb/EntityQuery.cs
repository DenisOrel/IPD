
// Type: Intermech.Data.EntityDb.EntityQuery
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb
{
    public class EntityQuery : ICloneable
    {
      private int recordLimit;
      private EntityQueryFilter filter;

      public EntityQuery()
      {
        this.recordLimit = 0;
        this.filter = new EntityQueryFilter();
      }

      public EntityQuery(int recordLimit)
        : this()
      {
        this.RecordLimit = recordLimit;
      }

      public bool RecordLimitEnabled => this.recordLimit > 0;

      public int RecordLimit
      {
        get => this.recordLimit;
        set
        {
          if (value < 0)
            throw new ArgumentNullException(nameof (RecordLimit));
          if (value == int.MaxValue)
            value = 0;
          this.recordLimit = value;
        }
      }

      public EntityQueryFilter Filter => this.filter;

      public EntityQuery Clone()
      {
        EntityQuery entityQuery = new EntityQuery(this.recordLimit);
        entityQuery.Filter.Assign(this.Filter);
        return entityQuery;
      }

      object ICloneable.Clone() => (object) this.Clone();
    }
}
