
// Type: Intermech.Remoting.ServerFormatterSinkWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;


namespace Intermech.Remoting
{
    public class ServerFormatterSinkWrapper : IServerChannelSink, IChannelSinkBase
    {
      private readonly IServerChannelSink nativeSink;

      public ServerFormatterSinkWrapper(IServerChannelSink nativeSink)
      {
        this.nativeSink = nativeSink != null ? nativeSink : throw new ArgumentNullException(nameof (nativeSink));
      }

      public IServerChannelSink NativeSink => this.nativeSink;

      public virtual void AsyncProcessResponse(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage msg,
        ITransportHeaders headers,
        Stream stream)
      {
        this.nativeSink.AsyncProcessResponse(sinkStack, state, msg, headers, stream);
      }

      public virtual Stream GetResponseStream(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage msg,
        ITransportHeaders headers)
      {
        return this.nativeSink.GetResponseStream(sinkStack, state, msg, headers);
      }

      public virtual IServerChannelSink NextChannelSink => this.nativeSink.NextChannelSink;

      public virtual ServerProcessing ProcessMessage(
        IServerChannelSinkStack sinkStack,
        IMessage requestMsg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out IMessage responseMsg,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        return this.nativeSink.ProcessMessage(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
      }

      public IDictionary Properties => this.nativeSink.Properties;
    }
}
