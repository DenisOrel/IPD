
// Type: Intermech.Data.EncodeAttributesParams
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data
{
    public class EncodeAttributesParams
    {
      public EncodeAttributesParams(
        IValueBagContainer container,
        ICollection<StringKey> attributeKeys,
        ValueBag attributes,
        ContainerValues containerValues,
        EncodeAttributesOptions options)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (attributeKeys == null)
          throw new ArgumentNullException(nameof (attributeKeys));
        if (attributes == null)
          throw new ArgumentNullException(nameof (attributes));
        if (containerValues == null)
          throw new ArgumentNullException(nameof (containerValues));
        if (options == null)
          throw new ArgumentNullException(nameof (options));
        this.Container = container;
        this.AttributeKeys = attributeKeys;
        this.Attributes = attributes;
        this.ContainerValues = containerValues;
        this.Options = options;
      }

      public IValueBagContainer Container { get; private set; }

      public string ContainerDisplayName { get; set; }

      public ICollection<StringKey> AttributeKeys { get; private set; }

      public ValueBag Attributes { get; private set; }

      public ContainerValues ContainerValues { get; private set; }

      public EncodeAttributesOptions Options { get; private set; }
    }
}
