
// Type: Intermech.AbortException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class AbortException : Exception
    {
      public static void Abort() => throw new AbortException();

      public static void Abort(string text) => throw new AbortException(text);

      public AbortException()
      {
      }

      public AbortException(string text)
        : base(text)
      {
      }

      protected AbortException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
