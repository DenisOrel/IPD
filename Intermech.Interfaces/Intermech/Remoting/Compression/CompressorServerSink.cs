
// Type: Intermech.Remoting.Compression.CompressorServerSink
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;


namespace Intermech.Remoting.Compression
{
    /// <summary>
    /// Выполняет сжатие сетевого трафика на клиентской стороне.
    /// </summary>
    internal sealed class CompressorServerSink : 
      BaseChannelSinkWithProperties,
      IServerChannelSink,
      IChannelSinkBase
    {
      private readonly IServerChannelSink nextSink;
      private readonly bool enabled;

      /// <summary>Создает объект.</summary>
      /// <param name="nextSink">Следующий канальный приемник в цепочке</param>
      /// <param name="enabled">Разрешает сжатие сетевого трафика</param>
      [SecurityPermission(SecurityAction.LinkDemand)]
      public CompressorServerSink(IServerChannelSink nextSink, bool enabled)
      {
        this.nextSink = nextSink != null ? nextSink : throw new ArgumentNullException(nameof (nextSink));
        this.enabled = enabled;
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
        bool state = PropReader.ReadBoolean(requestHeaders[(object) "X-IPS-Compressed"] as string, false);
        if (state)
          requestStream = PackHelper.UnpackStream(requestStream);
        sinkStack.Push((IServerChannelSink) this, (object) state);
        ServerProcessing serverProcessing = this.nextSink.ProcessMessage(sinkStack, requestMessage, requestHeaders, requestStream, out responseMessage, out responseHeaders, out responseStream);
        if (state)
          requestStream.Dispose();
        switch (serverProcessing)
        {
          case ServerProcessing.Complete:
            sinkStack.Pop((IServerChannelSink) this);
            if (state)
            {
              Stream stream = responseStream;
              responseStream = PackHelper.PackStream(responseStream);
              stream.Dispose();
              responseHeaders[(object) "X-IPS-Compressed"] = (object) "1";
              break;
            }
            break;
          case ServerProcessing.OneWay:
            sinkStack.Pop((IServerChannelSink) this);
            break;
        }
        return serverProcessing;
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
        if ((bool) state)
        {
          Stream stream = responseStream;
          responseStream = PackHelper.PackStream(responseStream);
          stream.Dispose();
          responseHeaders[(object) "X-IPS-Compressed"] = (object) "1";
        }
        sinkStack.AsyncProcessResponse(responseMessage, responseHeaders, responseStream);
      }
    }
}
