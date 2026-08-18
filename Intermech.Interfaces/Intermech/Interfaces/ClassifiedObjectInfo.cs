
// Type: Intermech.Interfaces.ClassifiedObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    [Serializable]
    public class ClassifiedObjectInfo
    {
      public long ObjectID { get; private set; }

      public int ObjectTypeID { get; private set; }

      public Intermech.Interfaces.AttributeValues[] OrigAttributeValues { get; private set; }

      public Intermech.Interfaces.AttributeValues[] AttributeValues { get; private set; }

      public ClassifiedObjectInfo(
        long objectID,
        int objectTypeID,
        Intermech.Interfaces.AttributeValues[] attributeValues,
        Intermech.Interfaces.AttributeValues[] origAttributeValues)
      {
        this.ObjectID = objectID;
        this.ObjectTypeID = objectTypeID;
        this.AttributeValues = attributeValues;
        this.OrigAttributeValues = origAttributeValues;
      }
    }
}
