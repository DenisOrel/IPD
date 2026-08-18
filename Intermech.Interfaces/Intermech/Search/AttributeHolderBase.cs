
// Type: Intermech.Search.AttributeHolderBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Search
{
    [Serializable]
    public abstract class AttributeHolderBase : IAttributeHolder
    {
      public AttributeHolderBase()
      {
        this.Attributes = (IAttributeCollection) new AttributeCollection();
      }

      public AttributeHolderBase(IAttributeCollection attributeCollection)
      {
        this.Attributes = attributeCollection != null ? attributeCollection : throw new ArgumentNullException(nameof (attributeCollection));
      }

      public IAttributeCollection Attributes { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; protected set; }

      protected T GetAttributeValue<T>(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        T defaultValue)
      {
        object attributeValue = this.Attributes.GetAttributeValue(obligatoryObjectAttribute);
        return attributeValue == null ? defaultValue : (T) attributeValue;
      }

      protected T GetAttributeValue<T>(int attributeTypeID, T defaultValue)
      {
        object attributeValue = this.Attributes.GetAttributeValue(attributeTypeID);
        return attributeValue == null ? defaultValue : (T) attributeValue;
      }

      protected T GetAttributeValue<T>(int attributeTypeID)
      {
        object attributeValue = this.Attributes.GetAttributeValue(attributeTypeID);
        return attributeValue == null ? default (T) : (T) attributeValue;
      }

      protected void SetAttributeValue<T>(int attributeTypeID, T value)
      {
        this.Attributes.SetAttributeValue(attributeTypeID, (object) value);
      }

      protected void SetAttributeValue<T>(
        ObligatoryObjectAttributes obligatoryObjectAttribute,
        T value)
      {
        this.Attributes.SetAttributeValue(obligatoryObjectAttribute, (object) value);
      }
    }
}
