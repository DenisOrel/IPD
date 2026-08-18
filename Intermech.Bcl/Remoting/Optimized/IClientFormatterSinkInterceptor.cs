
// Type: Intermech.Remoting.Optimized.IClientFormatterSinkInterceptor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;


namespace Intermech.Remoting.Optimized
{
    /// <summary>
    /// Интерфейс перехватчика для синхронных вызовов через remoting на клиентской стороне.
    /// Реализация не должна быть thread safe.
    /// </summary>
    public interface IClientFormatterSinkInterceptor
    {
      /// <summary>
      /// Вызывается перед отправкой сообщения на серверную сторону.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса</param>
      /// <param name="requestStream">Сериализованный поток запроса</param>
      void ProcessMessageStart(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream);

      /// <summary>
      /// Вызывается после получения ответного сообщения от серверной сторону.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса</param>
      /// <param name="requestStream">Сериализованный поток запроса</param>
      /// <param name="responseMsg">Сообщение ответа от серверной стороны</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Сериализованный поток ответа</param>
      void ProcessMessageFinish(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Stream responseStream);

      /// <summary>
      /// Вызывается при любых исключениях в процессе взаимодействия с серверной стороной.
      /// Метод ни в коем случае не должен бросать исключений.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса. Значение может быть равно null</param>
      /// <param name="responseMsg">Сообщение ответа. Значение может быть равно null</param>
      /// <param name="responseHeaders">Заголовки ответа. Значение может быть равно null</param>
      /// <param name="exception">Объект исключения</param>
      void ProcessMessageFailed(
        IMessage msg,
        ITransportHeaders requestHeaders,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Exception exception);
    }
}
