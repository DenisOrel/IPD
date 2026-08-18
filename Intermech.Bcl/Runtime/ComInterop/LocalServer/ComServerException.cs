
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Базовый тип для исключений COM-сервера.</summary>
    [Serializable]
    public class ComServerException : Exception
    {
      /// <summary>Создает объект исключения.</summary>
      public ComServerException()
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="message">Текст сообщения</param>
      public ComServerException(string message)
        : base(message)
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="innerException">Вложенное исключение</param>
      public ComServerException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="ctx">Контекст сериализации</param>
      protected ComServerException(SerializationInfo info, StreamingContext ctx)
        : base(info, ctx)
      {
      }
    }
}
