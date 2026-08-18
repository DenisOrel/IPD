
// Type: Intermech.Search.AttributeTypeForRelationKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search
{
    public sealed class AttributeTypeForRelationKey
    {
      public AttributeTypeForRelationKey(IMSAttribute4RelationType attributeType)
        : this(attributeType.AttributeID, attributeType.RelationTypeID)
      {
      }

      public AttributeTypeForRelationKey(int attributeTypeID, int relationTypeID)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        if (relationTypeID == -1)
          throw new ArgumentException();
        this.AttributeTypeID = attributeTypeID;
        this.RelationTypeID = relationTypeID;
      }

      public int AttributeTypeID { get; private set; }

      public int RelationTypeID { get; private set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is AttributeTypeForRelationKey typeForRelationKey && this.AttributeTypeID == typeForRelationKey.AttributeTypeID && this.RelationTypeID == typeForRelationKey.RelationTypeID;
      }

      public override int GetHashCode() => this.AttributeTypeID << 16 /*0x10*/ | this.RelationTypeID;
    }
}
