
// Type: Intermech.InvalidLoginInfoException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class InvalidLoginInfoException : KernelException
    {
      public InvalidLoginInfoException()
      {
      }

      public override string Message => LocalizationHolder.rm.GetString("Interfaces_214");

      protected InvalidLoginInfoException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
