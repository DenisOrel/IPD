
// Type: Intermech.Search.AttributeNotFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Search
{
    [Serializable]
    public sealed class AttributeNotFoundException : KernelException
    {
      public AttributeNotFoundException(int attributeTypeID)
        : base($"Атрибута с идентификатором #{attributeTypeID} нет в коллекции")
      {
      }

      private AttributeNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
