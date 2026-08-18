
// Type: Intermech.Search.AttributeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.ComponentModel;
using Intermech.Search.Data;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search
{
    /// <summary>Стандартная коллекция атрибутов</summary>
    [Serializable]
    public sealed class AttributeCollection : 
      BindingListBase<_Attribute>,
      IAttributeCollection,
      IEnumerable<_Attribute>,
      IEnumerable
    {
      public AttributeCollection()
      {
      }

      public AttributeCollection(IEnumerable<_Attribute> attributes)
      {
        if (attributes == null)
          throw new ArgumentNullException(nameof (attributes));
        this.AddRange(attributes);
      }

      public bool HasAttribute(int attributeTypeID)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        return this.Any<_Attribute>((Func<_Attribute, bool>) (o => o.TypeID == attributeTypeID));
      }

      public bool HasAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.HasAttribute((int) obligatoryObjectAttribute);
      }

      public _Attribute GetAttribute(int attributeTypeID)
      {
        return this.FirstOrDefault<_Attribute>((Func<_Attribute, bool>) (o => o.TypeID == attributeTypeID));
      }

      public _Attribute GetAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.GetAttribute((int) obligatoryObjectAttribute);
      }

      public object GetAttributeValue(int attributeTypeID)
      {
        _Attribute attribute = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? this.GetAttribute(attributeTypeID) : throw new ArgumentException();
        return attribute == null ? ServiceLocator.Get<IAttributeValueConverter>().GetAttributeDefaultValue(attributeTypeID) : attribute.Value;
      }

      public object GetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.GetAttributeValue((int) obligatoryObjectAttribute);
      }

      public void SetAttributeValue(int attributeTypeID, object value)
      {
        _Attribute attribute = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? this.GetAttribute(attributeTypeID) : throw new ArgumentException();
        if (attribute == null)
        {
          attribute = new _Attribute(attributeTypeID);
          this.Add(attribute);
        }
        attribute.Value = value;
      }

      public void SetAttributeValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        object value)
      {
        this.SetAttributeValue((int) obligatoryObjectAttribute, value);
      }
    }
}
