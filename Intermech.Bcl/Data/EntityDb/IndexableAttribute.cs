
// Type: Intermech.Data.EntityDb.IndexableAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb
{
    public sealed class IndexableAttribute : Attribute
    {
      private readonly IndexType indexType;
      private readonly bool isUnique;

      public IndexableAttribute()
        : this(IndexType.Auto, false)
      {
      }

      public IndexableAttribute(IndexType indexType, bool isUnique)
      {
        this.indexType = indexType;
        this.isUnique = isUnique;
      }

      public IndexType IndexType => this.indexType;

      public bool IsUnique => this.isUnique;
    }
}
