
// Type: Intermech.Data.DecodeAttributesOptions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data
{
    public class DecodeAttributesOptions : IAttributeCodecOptions
    {
      private static readonly DecodeAttributesOptions empty = new DecodeAttributesOptions();
      private Dictionary<StringKey, object> properties;

      public IDictionary<StringKey, object> Properties
      {
        get
        {
          if (this.properties == null)
            this.properties = new Dictionary<StringKey, object>();
          return (IDictionary<StringKey, object>) this.properties;
        }
      }

      public static DecodeAttributesOptions Empty => DecodeAttributesOptions.empty;
    }
}
