
// Type: Intermech.Remoting.Optimized.OptimizedBinaryClientFormatterSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;


namespace Intermech.Remoting.Optimized
{
    public class OptimizedBinaryClientFormatterSink : 
      IClientFormatterSink,
      IMessageSink,
      IClientChannelSink,
      IChannelSinkBase
    {
      private IClientChannelSink _nextSink;
      private FormatterSinkSharedData _sharedData;
      private Func<IClientFormatterSinkInterceptor> _interceptors;

      public IMessageSink NextSink
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)] get
        {
          throw new NotSupportedException();
        }
      }

      /// <summary>Gets the next <see cref="T:System.Runtime.Remoting.Channels.IClientChannelSink" /> in the sink chain.</summary>
      /// <returns>The next <see cref="T:System.Runtime.Remoting.Channels.IClientChannelSink" /> in the sink chain.</returns>
      public IClientChannelSink NextChannelSink
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)] get
        {
          return this._nextSink;
        }
      }

      /// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of properties for the current channel sink.</summary>
      /// <returns>A <see cref="T:System.Collections.IDictionary" /> of properties for the current channel sink.</returns>
      public IDictionary Properties
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)] get
        {
          return (IDictionary) null;
        }
      }

      public OptimizedBinaryClientFormatterSink(
        IClientChannelSink nextSink,
        FormatterSinkSharedData sharedData,
        Func<IClientFormatterSinkInterceptor> interceptors = null)
      {
        if (sharedData == null)
          throw new ArgumentNullException(nameof (sharedData));
        this._nextSink = nextSink;
        this._sharedData = sharedData;
        this._interceptors = interceptors;
      }

      /// <summary>Synchronously processes the provided message.</summary>
      /// <returns>The response to the processed message.</returns>
      /// <param name="msg">The message to process. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public IMessage SyncProcessMessage(IMessage msg)
      {
        IMethodCallMessage mcm = msg as IMethodCallMessage;
        IMessage responseMsg = (IMessage) null;
        ITransportHeaders headers = (ITransportHeaders) null;
        Stream stream = (Stream) null;
        ITransportHeaders responseHeaders = (ITransportHeaders) null;
        Stream responseStream = (Stream) null;
        IClientFormatterSinkInterceptor interceptor = this.TryGetInterceptor();
        try
        {
          try
          {
            this.SerializeMessage(msg, out headers, out stream);
            interceptor?.ProcessMessageStart(msg, headers, stream);
            this._nextSink.ProcessMessage(msg, headers, stream, out responseHeaders, out responseStream);
            if (responseHeaders == null)
              throw new ArgumentNullException("responseHeaders");
            responseMsg = this.DeserializeMessage(mcm, responseHeaders, responseStream);
            interceptor?.ProcessMessageFinish(msg, headers, stream, responseMsg, responseHeaders, responseStream);
          }
          finally
          {
            stream?.Close();
            responseStream?.Close();
          }
        }
        catch (Exception ex)
        {
          interceptor?.ProcessMessageFailed(msg, headers, responseMsg, responseHeaders, ex);
          responseMsg = (IMessage) new ReturnMessage(ex, mcm);
        }
        return responseMsg;
      }

      private IClientFormatterSinkInterceptor TryGetInterceptor()
      {
        return this._interceptors == null ? (IClientFormatterSinkInterceptor) null : this._interceptors();
      }

      /// <summary>Asynchronously processes the provided message.</summary>
      /// <returns>A <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> that provides a way to control the asynchronous message after it has been dispatched.</returns>
      /// <param name="msg">The message to process. </param>
      /// <param name="replySink">The sink that will receive the reply to the provided message. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
      {
        IMethodCallMessage methodCallMessage = (IMethodCallMessage) msg;
        try
        {
          Stream stream = (Stream) null;
          try
          {
            ITransportHeaders headers;
            this.SerializeMessage(msg, out headers, out stream);
            ClientChannelSinkStack sinkStack = new ClientChannelSinkStack(replySink);
            sinkStack.Push((IClientChannelSink) this, (object) msg);
            this._nextSink.AsyncProcessRequest((IClientChannelSinkStack) sinkStack, msg, headers, stream);
          }
          finally
          {
            stream?.Close();
          }
        }
        catch (Exception ex)
        {
          IMethodCallMessage mcm = methodCallMessage;
          IMessage msg1 = (IMessage) new ReturnMessage(ex, mcm);
          replySink?.SyncProcessMessage(msg1);
        }
        return (IMessageCtrl) null;
      }

      private void SerializeMessage(IMessage msg, out ITransportHeaders headers, out Stream stream)
      {
        BaseTransportHeaders transportHeaders = new BaseTransportHeaders();
        headers = (ITransportHeaders) transportHeaders;
        transportHeaders.ContentType = "application/octet-stream";
        if (this._sharedData.Protocol == FormatterSinkChannelProtocol.Http)
          headers[(object) "__RequestVerb"] = (object) "POST";
        RemotingMessageHeaders current = RemotingMessageHeaders.Current;
        if (current.GetFrameSize() != 0)
          this.SerializePerMessageHeaders(msg, current, headers);
        bool flag = false;
        stream = this._nextSink.GetRequestStream(msg, headers);
        if (stream == null)
        {
          stream = this._sharedData.CreateMemoryStream();
          flag = true;
        }
        this.SerializeBinaryMessage(msg, stream);
        if (!flag)
          return;
        stream.Position = 0L;
      }

      private void SerializePerMessageHeaders(
        IMessage msg,
        RemotingMessageHeaders msgHeaders,
        ITransportHeaders transportHeaders)
      {
        foreach (KeyValuePair<string, string> keyValuePair in RemotingMessageHeaders.Current.ScanFrame())
        {
          string key = keyValuePair.Key;
          if (key.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            transportHeaders[(object) key] = (object) keyValuePair.Value;
        }
      }

      private void SerializeBinaryMessage(IMessage msg, Stream outputStream)
      {
        this._sharedData.GetBinaryFormatter(true).Serialize(outputStream, (object) msg, (Header[]) null);
      }

      private IMessage DeserializeMessage(
        IMethodCallMessage mcm,
        ITransportHeaders headers,
        Stream stream)
      {
        return (IMessage) this._sharedData.GetBinaryFormatter(false).UnsafeDeserializeMethodResponse(stream, (HeaderHandler) null, mcm);
      }

      /// <summary>Requests message processing from the current sink.</summary>
      /// <param name="msg">The message to process. </param>
      /// <param name="requestHeaders">The headers to add to the outgoing message that is heading to the server. </param>
      /// <param name="requestStream">The stream that is headed toward the transport sink. </param>
      /// <param name="responseHeaders">When this method returns, contains a <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> interface that holds the headers that the server returned. This parameter is passed uninitialized. </param>
      /// <param name="responseStream">When this method returns, contains a <see cref="T:System.IO.Stream" /> that is coming back from the transport sink. This parameter is passed uninitialized. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public void ProcessMessage(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        throw new NotSupportedException();
      }

      /// <summary>Requests asynchronous processing of a method call on the current sink.</summary>
      /// <param name="sinkStack">A stack of channel sinks that called the current sink. </param>
      /// <param name="msg">The message to process. </param>
      /// <param name="headers">The headers to add to the outgoing message that is heading to the server. </param>
      /// <param name="stream">The stream that is headed toward the transport sink. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public void AsyncProcessRequest(
        IClientChannelSinkStack sinkStack,
        IMessage msg,
        ITransportHeaders headers,
        Stream stream)
      {
        throw new NotSupportedException();
      }

      /// <summary>Requests asynchronous processing of a response to a method call on the current sink.</summary>
      /// <param name="sinkStack">A stack of sinks that called the current sink. </param>
      /// <param name="state">Information that is associated with the current sink, generated on the request side and needed on the response side. </param>
      /// <param name="responseHeaders">The headers that are retrieved from the server response stream. </param>
      /// <param name="responseStream">The stream that is coming back from the transport sink. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public void AsyncProcessResponse(
        IClientResponseChannelSinkStack sinkStack,
        object state,
        ITransportHeaders responseHeaders,
        Stream responseStream)
      {
        try
        {
          IMessage msg = this.DeserializeMessage((IMethodCallMessage) state, responseHeaders, responseStream);
          sinkStack.DispatchReplyMessage(msg);
        }
        finally
        {
          responseStream?.Close();
        }
      }

      /// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the provided message is to be serialized.</summary>
      /// <returns>The <see cref="T:System.IO.Stream" /> onto which the provided message is to be serialized.</returns>
      /// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about the method call. </param>
      /// <param name="headers">The headers to add to the outgoing message that is heading to the server. </param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
      {
        throw new NotSupportedException();
      }
    }
}
