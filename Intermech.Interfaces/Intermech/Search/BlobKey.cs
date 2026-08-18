
// Type: Intermech.Search.BlobKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search
{
    public sealed class BlobKey
    {
      public BlobKey(long objectVersionID, long relationID, int attributeTypeID, int index)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID) && RelationHelper.IsUnknownRelationID(relationID))
          throw new ArgumentException();
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        if (index < 0)
          throw new ArgumentException();
        this.ObjectVersionID = objectVersionID;
        this.RelationID = relationID;
        this.AttributeTypeID = attributeTypeID;
        this.Index = index;
      }

      public long ObjectVersionID { get; private set; }

      public long RelationID { get; private set; }

      public int AttributeTypeID { get; private set; }

      public int Index { get; private set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is BlobKey blobKey && this.ObjectVersionID == blobKey.ObjectVersionID && this.RelationID == blobKey.RelationID && this.AttributeTypeID == blobKey.AttributeTypeID && this.Index == blobKey.Index;
      }

      public override int GetHashCode()
      {
        return (int) this.ObjectVersionID << 24 | ((int) this.RelationID & (int) byte.MaxValue) << 16 /*0x10*/ | (this.AttributeTypeID & (int) byte.MaxValue) << 8 | this.Index;
      }
    }
}
