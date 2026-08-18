
// Type: Intermech.Remoting.ClientFormatterSinkWrapper
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
    public class ClientFormatterSinkWrapper : 
      IClientFormatterSink,
      IMessageSink,
      IClientChannelSink,
      IChannelSinkBase
    {
      private readonly IClientFormatterSink nativeSink;

      public ClientFormatterSinkWrapper(IClientFormatterSink nativeSink)
      {
        this.nativeSink = nativeSink != null ? nativeSink : throw new ArgumentNullException(nameof (nativeSink));
      }

      public IClientFormatterSink NativeSink => this.nativeSink;

      public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
      {
        return this.nativeSink.AsyncProcessMessage(msg, replySink);
      }

      public IMessageSink NextSink => this.nativeSink.NextSink;

      public virtual IMessage SyncProcessMessage(IMessage msg)
      {
        return this.nativeSink.SyncProcessMessage(msg);
      }

      public virtual void AsyncProcessRequest(
        IClientChannelSinkStack sinkStack,
        IMessage msg,
        ITransportHeaders headers,
        Stream stream)
      {
        this.nativeSink.AsyncProcessRequest(sinkStack, msg, headers, stream);
      }

      public virtual void AsyncProcessResponse(
        IClientResponseChannelSinkStack sinkStack,
        object state,
        ITransportHeaders headers,
        Stream stream)
      {
        this.nativeSink.AsyncProcessResponse(sinkStack, state, headers, stream);
      }

      public virtual Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
      {
        return this.nativeSink.GetRequestStream(msg, headers);
      }

      public IClientChannelSink NextChannelSink => this.nativeSink.NextChannelSink;

      public virtual void ProcessMessage(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        this.nativeSink.ProcessMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream);
      }

      public IDictionary Properties => this.nativeSink.Properties;
    }
}
