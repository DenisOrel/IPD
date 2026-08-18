
// Type: Intermech.Data.IValueBagFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data
{
    public interface IValueBagFormatter
    {
      /// <summary>
      /// Признак, поддерживает ли контейнер добавление новых произвольных значений.
      /// </summary>
      bool IsOpenMetadata { get; }

      bool IsContainerSupported(IValueBagContainer container);

      bool IsValueSupported(StringKey valueKey);

      ContainerValues Read(IValueBagContainer container, ICollection<StringKey> valueKeys);

      bool Write(IValueBagContainer container, ContainerValues values);
    }
}
