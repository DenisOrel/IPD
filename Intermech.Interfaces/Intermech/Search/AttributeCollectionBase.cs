
// Type: Intermech.Search.AttributeCollectionBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search
{
    [Serializable]
    public abstract class AttributeCollectionBase : 
      IAttributeCollection,
      IEnumerable<_Attribute>,
      IEnumerable
    {
      public abstract void Add(_Attribute attribute);

      public abstract void AddRange(IEnumerable<_Attribute> attributes);

      public abstract bool HasAttribute(int attributeTypeID);

      public bool HasAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.HasAttribute((int) obligatoryObjectAttribute);
      }

      public abstract _Attribute GetAttribute(int attributeTypeID);

      public _Attribute GetAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.GetAttribute((int) obligatoryObjectAttribute);
      }

      public abstract object GetAttributeValue(int attributeTypeID);

      public object GetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.GetAttributeValue((int) obligatoryObjectAttribute);
      }

      public abstract void SetAttributeValue(int attributeTypeID, object value);

      public void SetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        object value)
      {
        this.SetAttributeValue((int) obligatoryObjectAttribute, value);
      }

      public abstract IEnumerator<_Attribute> GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }
}
