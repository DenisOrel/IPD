
// Type: Intermech.Remoting.Security.PrincipalServerSink
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;


namespace Intermech.Remoting.Security
{
    /// <summary>
    /// Обеспечивает прием на сервере сведений о клиенте, выполняющем вызов серверного метода.
    /// </summary>
    internal sealed class PrincipalServerSink : 
      BaseChannelSinkWithProperties,
      IServerChannelSink,
      IChannelSinkBase
    {
      private IServerChannelSink nextSink;
      private static readonly IPSPrincipalCodec ipsPrincipalCodec = new IPSPrincipalCodec();
      private static readonly IPrincipal anonymousPrincipal = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity("Anonymous IPS User", string.Empty), new string[0]);

      /// <summary>Создает объект.</summary>
      /// <param name="nextSink">Следующий канальный приемник в цепочке</param>
      [SecurityPermission(SecurityAction.LinkDemand)]
      public PrincipalServerSink(IServerChannelSink nextSink)
      {
        this.nextSink = nextSink != null ? nextSink : throw new ArgumentNullException(nameof (nextSink));
      }

      /// <summary>Возвращает следующий канальный приемник в цепочке.</summary>
      public IServerChannelSink NextChannelSink
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get
        {
          return this.nextSink;
        }
      }

      /// <summary>
      /// Возвращает поток, в который будет сериализован ответ для сервера в случае
      /// асинхронного вызова.
      /// </summary>
      /// <param name="sinkStack">Стек канальных приемников</param>
      /// <param name="state">Состояние</param>
      /// <param name="message">Объект сообщения</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <returns>Поток для сериализации ответа сервера</returns>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public Stream GetResponseStream(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage message,
        ITransportHeaders responseHeaders)
      {
        return (Stream) null;
      }

      /// <summary>Выполняет синхронный вызова метода.</summary>
      /// <param name="sinkStack">Стек канальных приемников</param>
      /// <param name="requestMessage">Объект сообщения</param>
      /// <param name="requestHeaders">Заголовки сообщения</param>
      /// <param name="requestStream">Сериализованное сообщение</param>
      /// <param name="responseMessage">Объект ответа</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Сериализованный ответ</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public ServerProcessing ProcessMessage(
        IServerChannelSinkStack sinkStack,
        IMessage requestMessage,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out IMessage responseMessage,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        IPrincipal serverPrincipal = PrincipalServerSink.SetPrincipal(PrincipalServerSink.DecodePrincipal(requestHeaders));
        try
        {
          sinkStack.Push((IServerChannelSink) this, (object) null);
          int num = (int) this.nextSink.ProcessMessage(sinkStack, requestMessage, requestHeaders, requestStream, out responseMessage, out responseHeaders, out responseStream);
          if (num != 2)
            sinkStack.Pop((IServerChannelSink) this);
          return (ServerProcessing) num;
        }
        finally
        {
          PrincipalServerSink.RestorePrincipal(serverPrincipal);
        }
      }

      /// <summary>
      /// Выполняет обработку ответа при асинхронном вызове серверного метода.
      /// </summary>
      /// <param name="sinkStack">Стек канальных приемников</param>
      /// <param name="state">Состояние</param>
      /// <param name="responseMessage">Объект ответа</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Поток для серализации ответа</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public void AsyncProcessResponse(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage responseMessage,
        ITransportHeaders responseHeaders,
        Stream responseStream)
      {
        sinkStack.AsyncProcessResponse(responseMessage, responseHeaders, responseStream);
      }

      private static IPrincipal SetPrincipal(IPrincipal clientPrincipal)
      {
        IPrincipal currentPrincipal = Thread.CurrentPrincipal;
        Thread.CurrentPrincipal = clientPrincipal;
        return currentPrincipal;
      }

      private static void RestorePrincipal(IPrincipal serverPrincipal)
      {
        Thread.CurrentPrincipal = serverPrincipal;
      }

      private static IPrincipal DecodePrincipal(ITransportHeaders requestHeaders)
      {
        return requestHeaders[(object) "X-IPS-Principal"] is string requestHeader ? (IPrincipal) PrincipalServerSink.ipsPrincipalCodec.DecodeFromBase64(requestHeader) : PrincipalServerSink.anonymousPrincipal;
      }
    }
}
