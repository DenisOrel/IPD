
// Type: Intermech.FaultException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Базовый класс для исключений, представляющих необрабатываемый отказ в обслуживании. Такие исключения показываются
    /// пользователю как ошибки, т.е. без call stack.
    /// </summary>
    [Serializable]
    public class FaultException : Exception
    {
      /// <summary>Создает исключение.</summary>
      /// <param name="message">Сообщение, описывающее ошибку</param>
      public FaultException(string message)
        : base(message)
      {
      }

      /// <summary>Создает исключение.</summary>
      /// <param name="message">Сообщение, описывающее ошибку</param>
      /// <param name="innerException">Предыдущее исключение</param>
      public FaultException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      /// <summary>Используется для десериализации исключения.</summary>
      /// <param name="info">Сериализованное представление исключения</param>
      /// <param name="context">Контекст десериализации</param>
      protected FaultException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
