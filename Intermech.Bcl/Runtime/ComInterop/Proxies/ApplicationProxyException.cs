
// Type: Intermech.Runtime.ComInterop.Proxies.ApplicationProxyException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Runtime.ComInterop.Proxies
{
    /// <summary>
    /// Базовый класс для всех исключений proxy-объектов вокруг API внешних систем и приложений.
    /// </summary>
    [Serializable]
    public class ApplicationProxyException : FaultException
    {
      /// <summary>Создает объект.</summary>
      /// <param name="message">Текст сообщения</param>
      public ApplicationProxyException(string message)
        : base(message)
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="innerException">Вложенное исключение</param>
      public ApplicationProxyException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      /// <summary>Используется для десериализации исключения.</summary>
      /// <param name="info">Сериализованное представление исключения</param>
      /// <param name="context">Контекст десериализации</param>
      protected ApplicationProxyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
