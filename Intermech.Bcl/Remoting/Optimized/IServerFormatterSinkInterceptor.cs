
// Type: Intermech.Remoting.Optimized.IServerFormatterSinkInterceptor
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
    /// Интерфейс перехватчика для синхронных вызовов через remoting на серверной стороне.
    /// Реализация не должна быть thread safe.
    /// </summary>
    public interface IServerFormatterSinkInterceptor
    {
      void ProcessMessageStart(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream);

      ServerProcessing? ProcessMessage(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out IMessage responseMsg);

      void ProcessMessageFinish(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Stream responseStream,
        ServerProcessing result);

      void ProcessMessageFailed(
        IMessage msg,
        ITransportHeaders requestHeaders,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Exception exception);
    }
}
