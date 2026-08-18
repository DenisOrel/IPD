
// Type: Intermech.Remoting.Security.PrincipalClientSink
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Security;
using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;


namespace Intermech.Remoting.Security
{
    /// <summary>
    /// Обеспечивает передачу на сервер сведений о клиенте, выполняющем вызов серверного метода.
    /// </summary>
    internal sealed class PrincipalClientSink : 
      BaseChannelSinkWithProperties,
      IClientChannelSink,
      IChannelSinkBase
    {
      private IClientChannelSink nextSink;
      private static readonly IPSPrincipalCodec ipsPrincipalCodec = new IPSPrincipalCodec();

      /// <summary>Создает объект.</summary>
      /// <param name="nextSink">Следующий канальный приемник</param>
      [SecurityPermission(SecurityAction.LinkDemand)]
      public PrincipalClientSink(IClientChannelSink nextSink)
      {
        this.nextSink = nextSink != null ? nextSink : throw new ArgumentNullException(nameof (nextSink));
      }

      /// <summary>Возвращает следующий канальный приемник в цепочке.</summary>
      public IClientChannelSink NextChannelSink
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get
        {
          return this.nextSink;
        }
      }

      /// <summary>
      /// Возвращает поток, в который будет сериализовано сообщение для сервера в случае
      /// асинхронного вызова.
      /// </summary>
      /// <param name="message">Объект сообщения</param>
      /// <param name="requestHeaders">Заголовки сообщения</param>
      /// <returns>Поток для сериализации данных</returns>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public Stream GetRequestStream(IMessage message, ITransportHeaders requestHeaders)
      {
        return this.nextSink.GetRequestStream(message, requestHeaders);
      }

      /// <summary>
      /// Выполняет синхронную обработку вызова серверного метода.
      /// </summary>
      /// <param name="message">Объект сообщения</param>
      /// <param name="requestHeaders">Заголовки сообщения</param>
      /// <param name="requestStream">Сериализованное сообщение</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Сериализованный ответ</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public void ProcessMessage(
        IMessage message,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        this.AddPrincipalToRequest(message, requestHeaders);
        this.nextSink.ProcessMessage(message, requestHeaders, requestStream, out responseHeaders, out responseStream);
      }

      /// <summary>
      /// Выполняет асинхронную обработку вызова серверного метода.
      /// </summary>
      /// <param name="sinkStack">Стек канальных приемников</param>
      /// <param name="message">Объект сообщения</param>
      /// <param name="requestHeaders">Заголовки сообщения</param>
      /// <param name="requestStream">Сериализованное сообщение</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public void AsyncProcessRequest(
        IClientChannelSinkStack sinkStack,
        IMessage message,
        ITransportHeaders requestHeaders,
        Stream requestStream)
      {
        this.AddPrincipalToRequest(message, requestHeaders);
        this.nextSink.AsyncProcessRequest(sinkStack, message, requestHeaders, requestStream);
      }

      /// <summary>
      /// Выполняет обработку ответа при асинхронном вызове серверного метода.
      /// </summary>
      /// <param name="sinkStack">Стек канальных приемников</param>
      /// <param name="state">Состояние</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Сериализованный ответ</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public void AsyncProcessResponse(
        IClientResponseChannelSinkStack sinkStack,
        object state,
        ITransportHeaders responseHeaders,
        Stream responseStream)
      {
        sinkStack.AsyncProcessResponse(responseHeaders, responseStream);
      }

      private void AddPrincipalToRequest(IMessage message, ITransportHeaders requestHeaders)
      {
        IPSPrincipal currentPrincipal = IPSPrincipal.CurrentPrincipal;
        requestHeaders[(object) "X-IPS-Principal"] = (object) PrincipalClientSink.ipsPrincipalCodec.EncodeToBase64(currentPrincipal);
      }
    }
}
