
// Type: Intermech.KernelException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Предок для всех исключений, генерируемых серверным ядром системы
    /// </summary>
    [Serializable]
    public class KernelException : Exception
    {
      public KernelException(string message)
        : base(message)
      {
      }

      public KernelException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      public KernelException()
      {
      }

      protected KernelException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
