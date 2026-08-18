
// Type: Intermech.Interfaces.HiddenStackException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    [Serializable]
    public class HiddenStackException : Exception, ISimpleMessageException
    {
      public HiddenStackException(string message)
        : base(message)
      {
      }

      protected HiddenStackException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
