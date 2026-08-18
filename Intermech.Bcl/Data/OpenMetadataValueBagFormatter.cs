
// Type: Intermech.Data.OpenMetadataValueBagFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    public abstract class OpenMetadataValueBagFormatter : BasicValueBagFormatter
    {
      public override bool IsOpenMetadata => true;

      public override bool IsValueSupported(StringKey valueKey)
      {
        if (valueKey == (StringKey) null)
          throw new ArgumentNullException(nameof (valueKey));
        return true;
      }
    }
}
