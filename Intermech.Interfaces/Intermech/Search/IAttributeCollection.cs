
// Type: Intermech.Search.IAttributeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search
{
    public interface IAttributeCollection : IEnumerable<_Attribute>, IEnumerable
    {
      void Add(_Attribute attribute);

      void AddRange(IEnumerable<_Attribute> attributes);

      bool HasAttribute(int attributeTypeID);

      bool HasAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute);

      _Attribute GetAttribute(int attributeTypeID);

      _Attribute GetAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute);

      object GetAttributeValue(int attributeTypeID);

      object GetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute);

      void SetAttributeValue(int attributeTypeID, object value);

      void SetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        object value);
    }
}
