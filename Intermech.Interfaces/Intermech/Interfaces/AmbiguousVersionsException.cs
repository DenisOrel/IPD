
// Type: Intermech.Interfaces.AmbiguousVersionsException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Исключение генерируется механизмом подбора версий, если включен режим последовательного
    /// проектирования, а подбор по основным критериям прошли несколько версий, либо не прошла
    /// ни одна из версий.
    /// </summary>
    [Serializable]
    public class AmbiguousVersionsException : KernelException
    {
      /// <summary>Создать экземпляр класса</summary>
      public AmbiguousVersionsException()
      {
      }

      /// <summary>Создать экземпляр класса, указать сообщение</summary>
      /// <param name="message">Сообщение</param>
      public AmbiguousVersionsException(string message)
        : base(message)
      {
      }

      /// <summary>
      /// Создать экземпляр класса, указать сообщение и вложенное исключение
      /// </summary>
      /// <param name="message">Сообщение</param>
      /// <param name="innerException">Вложенное исключение</param>
      public AmbiguousVersionsException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="info">Дополнительная информация</param>
      /// <param name="context">Контекст</param>
      protected AmbiguousVersionsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
