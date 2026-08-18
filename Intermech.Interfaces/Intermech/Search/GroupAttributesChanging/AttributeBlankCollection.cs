
// Type: Intermech.Search.GroupAttributesChanging.AttributeBlankCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.GroupAttributesChanging
{
    [Serializable]
    public sealed class AttributeBlankCollection : IEnumerable<AttributeBlank>, IEnumerable
    {
      private Dictionary<int, AttributeBlank> _attributes = new Dictionary<int, AttributeBlank>();

      public AttributeBlankCollection(AttributeBlank[] attributes)
      {
        if (attributes == null)
          throw new ArgumentNullException(nameof (attributes));
        foreach (AttributeBlank attribute in attributes)
          this._attributes[attribute.AttributeTypeID] = attribute;
      }

      public AttributeBlank this[int attributeTypeID]
      {
        get
        {
          AttributeBlank attributeBlank = (AttributeBlank) null;
          this._attributes.TryGetValue(attributeTypeID, out attributeBlank);
          return attributeBlank;
        }
      }

      public AttributeBlank[] GetAllEditableAttributes()
      {
        return this._attributes.Values.Where<AttributeBlank>((Func<AttributeBlank, bool>) (o => o.IsEditable)).ToArray<AttributeBlank>();
      }

      public IEnumerator<AttributeBlank> GetEnumerator()
      {
        return (IEnumerator<AttributeBlank>) this._attributes.Values.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }
}
