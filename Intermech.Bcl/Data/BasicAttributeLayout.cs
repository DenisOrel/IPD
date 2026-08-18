
// Type: Intermech.Data.BasicAttributeLayout
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data
{
    public class BasicAttributeLayout : IAttributeLayout
    {
      private readonly StringKey attributeKey;
      private readonly ICollection<StringKey> containerKeys;

      public BasicAttributeLayout(StringKey attributeKey, StringKey containerKey)
      {
        if (attributeKey == (StringKey) null)
          throw new ArgumentNullException(nameof (attributeKey));
        if (containerKey == (StringKey) null)
          throw new ArgumentNullException(nameof (containerKey));
        this.attributeKey = attributeKey;
        this.containerKeys = (ICollection<StringKey>) new StringKey[1]
        {
          containerKey
        };
      }

      public BasicAttributeLayout(StringKey attributeKey, ICollection<StringKey> containerKeys)
      {
        if (attributeKey == (StringKey) null)
          throw new ArgumentNullException(nameof (attributeKey));
        if (containerKeys == null)
          throw new ArgumentNullException(nameof (containerKeys));
        this.attributeKey = attributeKey;
        this.containerKeys = containerKeys;
      }

      public StringKey AttributeKey => this.attributeKey;

      public ICollection<StringKey> ContainerKeys => this.containerKeys;
    }
}
