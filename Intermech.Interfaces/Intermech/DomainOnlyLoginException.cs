
// Type: Intermech.DomainOnlyLoginException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение, которое выбрасывается внутри клиента при ошибке логина в режиме DomainOnlyLogin
    /// </summary>
    public class DomainOnlyLoginException : Exception
    {
      public DomainOnlyLoginException(string message)
        : base(message)
      {
      }

      public DomainOnlyLoginException(Exception innerException)
        : base(innerException.Message, innerException)
      {
      }

      protected DomainOnlyLoginException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
