
// Type: Intermech.Remoting.Optimized.OptimizedBinaryServerFormatterSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;


namespace Intermech.Remoting.Optimized
{
    public class OptimizedBinaryServerFormatterSink : IServerChannelSink, IChannelSinkBase
    {
      private IServerChannelSink _nextSink;
      private IChannelReceiver _receiver;
      private FormatterSinkSharedData _sharedData;
      private Func<IServerFormatterSinkInterceptor> _interceptors;
      private string lastUri;

      public IServerChannelSink NextChannelSink
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

      public OptimizedBinaryServerFormatterSink(
        IServerChannelSink nextSink,
        IChannelReceiver receiver,
        FormatterSinkSharedData sharedData,
        Func<IServerFormatterSinkInterceptor> interceptors = null)
      {
        if (receiver == null)
          throw new ArgumentNullException(nameof (receiver));
        if (sharedData == null)
          throw new ArgumentNullException(nameof (sharedData));
        this._nextSink = nextSink;
        this._receiver = receiver;
        this._sharedData = sharedData;
        this._interceptors = interceptors;
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public ServerProcessing ProcessMessage(
        IServerChannelSinkStack sinkStack,
        IMessage requestMsg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out IMessage responseMsg,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        if (requestMsg != null)
          return this.ProcessMessageIntercepted(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
        BaseTransportHeaders transportHeaders = requestHeaders != null ? requestHeaders as BaseTransportHeaders : throw new ArgumentNullException(nameof (requestHeaders));
        responseHeaders = (ITransportHeaders) null;
        responseStream = (Stream) null;
        string strA = (string) null;
        bool flag = true;
        string contentType = transportHeaders == null ? requestHeaders[(object) "Content-Type"] as string : transportHeaders.ContentType;
        if (contentType != null)
          this.ParseContentType(contentType, out strA, out string _);
        if (strA != null && string.CompareOrdinal(strA, "application/octet-stream") != 0)
          flag = false;
        if (this._sharedData.Protocol == FormatterSinkChannelProtocol.Http)
        {
          string requestHeader = (string) requestHeaders[(object) "__RequestVerb"];
          if (!requestHeader.Equals("POST") && !requestHeader.Equals("M-POST"))
            flag = false;
        }
        if (flag)
        {
          ServerProcessing serverProcessing;
          try
          {
            data = true;
            object requestHeader = requestHeaders[(object) "__CustomErrorsEnabled"];
            if (requestHeader == null || !(requestHeader is bool data))
              ;
            CallContext.SetData("__CustomErrorsEnabled", (object) data);
            string str = transportHeaders == null ? (string) requestHeaders[(object) "__RequestUri"] : transportHeaders.RequestUri;
            this.lastUri = !(str != this.lastUri) || !(RemotingServices.GetServerTypeForUri(str) == (Type) null) ? str : throw new RemotingException("Requested service not found.");
            PermissionSet permissionSet = (PermissionSet) null;
            if (this._sharedData.FormatterSecurityLevel != TypeFilterLevel.Full)
            {
              permissionSet = new PermissionSet(PermissionState.None);
              permissionSet.SetPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
            }
            try
            {
              permissionSet?.PermitOnly();
              requestMsg = this.DeserializeBinaryRequestMessage(str, requestStream);
            }
            finally
            {
              if (permissionSet != null)
                CodeAccessPermission.RevertPermitOnly();
            }
            requestStream.Close();
            if (requestMsg == null)
              throw new RemotingException("Error deserializing message.");
            if (requestMsg is MarshalByRefObject && !AppSettings.AllowTransparentProxyMessage)
            {
              requestMsg = (IMessage) null;
              throw new RemotingException("Error deserializing message.", (Exception) new NotSupportedException(AppSettings.AllowTransparentProxyMessageFwLink));
            }
            sinkStack.Push((IServerChannelSink) this, (object) null);
            serverProcessing = this.ProcessMessageIntercepted(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
            if (responseStream != null)
              throw new RemotingException("The responseStream out parameter cannot be set before the response message reaches the server formatter sink.");
            switch (serverProcessing)
            {
              case ServerProcessing.Complete:
                if (responseMsg == null)
                  throw new RemotingException("Error dispatching message.");
                sinkStack.Pop((IServerChannelSink) this);
                this.SerializeResponse((IServerResponseChannelSinkStack) sinkStack, responseMsg, ref responseHeaders, out responseStream);
                break;
              case ServerProcessing.OneWay:
                sinkStack.Pop((IServerChannelSink) this);
                break;
              case ServerProcessing.Async:
                sinkStack.Store((IServerChannelSink) this, (object) null);
                break;
            }
          }
          catch (Exception ex)
          {
            serverProcessing = ServerProcessing.Complete;
            Exception e = ex;
            IMessage mcm = requestMsg == null ? (IMessage) new ErrorMessage() : requestMsg;
            responseMsg = (IMessage) new ReturnMessage(e, (IMethodCallMessage) mcm);
            responseHeaders = (ITransportHeaders) new TransportHeaders();
            if (this._sharedData.Protocol == FormatterSinkChannelProtocol.Http)
              responseHeaders[(object) "Content-Type"] = (object) "application/octet-stream";
            responseStream = this._sharedData.CreateMemoryStream();
            try
            {
              CallContext.SetData("__ClientIsClr", (object) true);
              this.SerializeBinaryMessage(responseMsg, responseStream);
              responseStream.Position = 0L;
            }
            finally
            {
              CallContext.FreeNamedDataSlot("__ClientIsClr");
            }
          }
          finally
          {
            CallContext.FreeNamedDataSlot("__CustomErrorsEnabled");
          }
          return serverProcessing;
        }
        if (this._nextSink != null)
          return this._nextSink.ProcessMessage(sinkStack, (IMessage) null, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
        if (this._sharedData.Protocol != FormatterSinkChannelProtocol.Http)
          throw new RemotingException("Invalid client request format.");
        responseHeaders = (ITransportHeaders) new TransportHeaders();
        responseHeaders[(object) "__HttpStatusCode"] = (object) "400";
        responseHeaders[(object) "__HttpReasonPhrase"] = (object) "Bad Request";
        responseStream = (Stream) null;
        responseMsg = (IMessage) null;
        return ServerProcessing.Complete;
      }

      private ServerProcessing ProcessMessageIntercepted(
        IServerChannelSinkStack sinkStack,
        IMessage requestMsg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        out IMessage responseMsg,
        out ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        responseMsg = (IMessage) null;
        responseHeaders = (ITransportHeaders) null;
        responseStream = (Stream) null;
        IServerFormatterSinkInterceptor interceptor = this.TryGetInterceptor();
        RemotingMessageHeaders current = RemotingMessageHeaders.Current;
        current.PushFrame();
        try
        {
          this.DeserializePerMessageHeaders(requestMsg, current, requestHeaders);
          interceptor?.ProcessMessageStart(requestMsg, requestHeaders, requestStream);
          ServerProcessing? nullable = new ServerProcessing?();
          if (interceptor != null)
            nullable = interceptor.ProcessMessage(requestMsg, requestHeaders, requestStream, out responseMsg);
          if (!nullable.HasValue)
            nullable = new ServerProcessing?(this._nextSink.ProcessMessage(sinkStack, requestMsg, requestHeaders, (Stream) null, out responseMsg, out responseHeaders, out responseStream));
          ServerProcessing result = nullable.Value;
          interceptor?.ProcessMessageFinish(requestMsg, requestHeaders, requestStream, responseMsg, responseHeaders, responseStream, result);
          return result;
        }
        catch (Exception ex)
        {
          interceptor?.ProcessMessageFailed(requestMsg, requestHeaders, responseMsg, responseHeaders, ex);
          throw;
        }
        finally
        {
          current.PopFrame();
        }
      }

      private IServerFormatterSinkInterceptor TryGetInterceptor()
      {
        return this._interceptors == null ? (IServerFormatterSinkInterceptor) null : this._interceptors();
      }

      private void DeserializePerMessageHeaders(
        IMessage msg,
        RemotingMessageHeaders perMsgHeaders,
        ITransportHeaders transportHeaders)
      {
        foreach (DictionaryEntry transportHeader in transportHeaders)
        {
          if (transportHeader.Key is string key && key.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            perMsgHeaders[key] = transportHeader.Value as string;
        }
      }

      private IMessage DeserializeBinaryRequestMessage(string objectUri, Stream inputStream)
      {
        BinaryFormatter binaryFormatter = this._sharedData.GetBinaryFormatter(false);
            UriHeaderHandler uriHeaderHandler = new UriHeaderHandler(objectUri);
        Stream serializationStream = inputStream;
        HeaderHandler handler = new HeaderHandler(uriHeaderHandler.HeaderHandler);
        return (IMessage) binaryFormatter.UnsafeDeserialize(serializationStream, handler);
      }

      private void SerializeBinaryMessage(IMessage msg, Stream outputStream)
      {
        this._sharedData.GetBinaryFormatter(true).Serialize(outputStream, (object) msg, (Header[]) null);
      }

      private void ParseContentType(string contentType, out string value, out string charset)
      {
        charset = (string) null;
        if (contentType == null)
        {
          value = (string) null;
        }
        else
        {
          string[] strArray = contentType.Split(';');
          value = strArray[0];
          if (strArray.Length == 0)
            return;
          foreach (string str in strArray)
          {
            int length = str.IndexOf('=');
            if (length != -1 && string.Compare(str.Substring(0, length).Trim(), nameof (charset), StringComparison.OrdinalIgnoreCase) == 0)
            {
              if (length + 1 < str.Length)
              {
                charset = str.Substring(length + 1);
                break;
              }
              charset = (string) null;
              break;
            }
          }
        }
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public void AsyncProcessResponse(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Stream responseStream)
      {
        this.SerializeResponse(sinkStack, responseMsg, ref responseHeaders, out responseStream);
        sinkStack.AsyncProcessResponse(responseMsg, responseHeaders, responseStream);
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      private void SerializeResponse(
        IServerResponseChannelSinkStack sinkStack,
        IMessage responseMsg,
        ref ITransportHeaders responseHeaders,
        out Stream responseStream)
      {
        BaseTransportHeaders transportHeaders = new BaseTransportHeaders();
        if (responseHeaders != null)
        {
          foreach (DictionaryEntry dictionaryEntry in responseHeaders)
            transportHeaders[dictionaryEntry.Key] = dictionaryEntry.Value;
        }
        responseHeaders = (ITransportHeaders) transportHeaders;
        if (this._sharedData.Protocol == FormatterSinkChannelProtocol.Http)
          transportHeaders.ContentType = "application/octet-stream";
        bool flag = false;
        responseStream = sinkStack.GetResponseStream(responseMsg, responseHeaders);
        if (responseStream == null)
        {
          responseStream = this._sharedData.CreateMemoryStream();
          flag = true;
        }
        try
        {
          CallContext.SetData("__ClientIsClr", (object) true);
          this.SerializeBinaryMessage(responseMsg, responseStream);
          if (!flag)
            return;
          responseStream.Position = 0L;
        }
        finally
        {
          CallContext.FreeNamedDataSlot("__ClientIsClr");
        }
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public Stream GetResponseStream(
        IServerResponseChannelSinkStack sinkStack,
        object state,
        IMessage msg,
        ITransportHeaders headers)
      {
        throw new NotSupportedException();
      }

      private class UriHeaderHandler
      {
        private string _uri;

        internal UriHeaderHandler(string uri) => this._uri = uri;

        public object HeaderHandler(Header[] Headers) => (object) this._uri;
      }
    }
}
