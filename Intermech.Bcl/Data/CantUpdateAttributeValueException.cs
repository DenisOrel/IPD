
// Type: Intermech.Data.CantUpdateAttributeValueException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    public class CantUpdateAttributeValueException : Exception
    {
      private readonly ValueRecord attribute;

      public CantUpdateAttributeValueException(ValueRecord attribute)
        : this(attribute, (Exception) null)
      {
      }

      public CantUpdateAttributeValueException(ValueRecord attribute, Exception innerException)
        : base((string) null, innerException)
      {
        this.attribute = attribute != null ? attribute : throw new ArgumentNullException(nameof (attribute));
      }

      public ValueRecord Attribute => this.attribute;
    }
}
