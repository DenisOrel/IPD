
// Type: Intermech.Data.EntityDb.PropertyValueCondition
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb
{
    public abstract class PropertyValueCondition : IQueryCondition, ICloneable
    {
      private readonly object propertyReference;

      protected PropertyValueCondition(object propertyReference)
      {
        this.propertyReference = propertyReference != null ? propertyReference : throw new ArgumentNullException(nameof (propertyReference));
      }

      public object PropertyReference => this.propertyReference;

      object ICloneable.Clone() => this.DoClone();

      protected abstract object DoClone();
    }
}
