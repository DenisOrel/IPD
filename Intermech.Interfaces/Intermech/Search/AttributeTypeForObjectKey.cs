
// Type: Intermech.Search.AttributeTypeForObjectKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search
{
    public sealed class AttributeTypeForObjectKey
    {
      public AttributeTypeForObjectKey(IMSAttribute4ObjectType attributeType)
        : this(attributeType.AttributeID, attributeType.ObjectTypeID)
      {
      }

      public AttributeTypeForObjectKey(int attributeTypeID, int objectTypeID)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        if (objectTypeID == -1)
          throw new ArgumentException();
        this.AttributeTypeID = attributeTypeID;
        this.ObjectTypeID = objectTypeID;
      }

      public int AttributeTypeID { get; private set; }

      public int ObjectTypeID { get; private set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is AttributeTypeForObjectKey typeForObjectKey && this.AttributeTypeID == typeForObjectKey.AttributeTypeID && this.ObjectTypeID == typeForObjectKey.ObjectTypeID;
      }

      public override int GetHashCode() => this.AttributeTypeID << 16 /*0x10*/ | this.ObjectTypeID;
    }
}
