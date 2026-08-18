
// Type: Intermech.Remoting.Compression.CompressorClientSink
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;


namespace Intermech.Remoting.Compression
{
    /// <summary>
    /// Выполняет сжатие сетевого трафика на клиентской стороне.
    /// </summary>
    internal sealed class CompressorClientSink : 
      BaseChannelSinkWithProperties,
      IClientChannelSink,
      IChannelSinkBase
    {
      private static readonly ConcurrentDictionary<MethodBase, bool> canPackCache = new ConcurrentDictionary<MethodBase, bool>();
      private static Type controlAttribute = typeof (RemotingCompressionAttribute);
      private static readonly string DataTableTypeName = "System.Data.DataTable";
      private static readonly string DataSetTypeName = "System.Data.DataSet";
      private readonly IClientChannelSink nextSink;
      private readonly bool enabled;

      /// <summary>Создает объект.</summary>
      /// <param name="nextSink">Следующий канальный приемник</param>
      /// <param name="enabled">Разрешает сжатие сетевого трафика</param>
      [SecurityPermission(SecurityAction.LinkDemand)]
      public CompressorClientSink(IClientChannelSink nextSink, bool enabled)
      {
        this.nextSink = nextSink != null ? nextSink : throw new ArgumentNullException(nameof (nextSink));
        this.enabled = enabled;
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
        int num = !this.enabled ? 0 : (CompressorClientSink.CanPack(message) ? 1 : 0);
        if (num != 0)
        {
          requestStream = PackHelper.PackStream(requestStream);
          requestHeaders[(object) "X-IPS-Compressed"] = (object) "1";
        }
        this.nextSink.ProcessMessage(message, requestHeaders, requestStream, out responseHeaders, out responseStream);
        if (num != 0)
          requestStream.Dispose();
        if (!PropReader.ReadBoolean(responseHeaders[(object) "X-IPS-Compressed"] as string, false))
          return;
        Stream stream = responseStream;
        responseStream = PackHelper.UnpackStream(responseStream);
        stream.Dispose();
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
        int num = !this.enabled ? 0 : (CompressorClientSink.CanPack(message) ? 1 : 0);
        if (num != 0)
        {
          requestStream = PackHelper.PackStream(requestStream);
          requestHeaders[(object) "X-IPS-Compressed"] = (object) "1";
        }
        sinkStack.Push((IClientChannelSink) this, (object) null);
        this.nextSink.AsyncProcessRequest(sinkStack, message, requestHeaders, requestStream);
        if (num == 0)
          return;
        requestStream.Dispose();
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
        if (PropReader.ReadBoolean(responseHeaders[(object) "X-IPS-Compressed"] as string, false))
        {
          Stream stream = responseStream;
          responseStream = PackHelper.UnpackStream(responseStream);
          stream.Dispose();
        }
        sinkStack.AsyncProcessResponse(responseHeaders, responseStream);
      }

      private static bool CanPack(IMessage msg)
      {
        return msg is IMethodMessage methodMessage && CompressorClientSink.canPackCache.GetOrAdd(methodMessage.MethodBase, (Func<MethodBase, bool>) (methodBase =>
        {
          MethodInfo methodInfo = methodBase as MethodInfo;
          return methodInfo != (MethodInfo) null && CompressorClientSink.CanPack(methodInfo);
        }));
      }

      private static bool CanPack(MethodInfo methodInfo)
      {
        if (CompressorClientSink.IsMarkedAsCanPack((ICustomAttributeProvider) methodInfo) || methodInfo.DeclaringType != (Type) null && CompressorClientSink.IsMarkedAsCanPack((ICustomAttributeProvider) methodInfo.DeclaringType) || CompressorClientSink.CanPackByParameterType(methodInfo.ReturnType))
          return true;
        foreach (ParameterInfo parameter in methodInfo.GetParameters())
        {
          if (CompressorClientSink.CanPackByParameterType(parameter.ParameterType))
            return true;
        }
        return false;
      }

      private static bool CanPackByParameterType(Type type, bool inArray = false)
      {
        if (type.FullName == CompressorClientSink.DataTableTypeName || type.FullName == CompressorClientSink.DataSetTypeName || !type.IsPrimitive && type.IsSerializable && CompressorClientSink.IsMarkedAsCanPack((ICustomAttributeProvider) type))
          return true;
        return type.IsArray && type.HasElementType && CompressorClientSink.CanPackByParameterType(type.GetElementType(), true);
      }

      private static bool IsMarkedAsCanPack(ICustomAttributeProvider element)
      {
        object[] customAttributes = element.GetCustomAttributes(CompressorClientSink.controlAttribute, true);
        return customAttributes != null && customAttributes.Length != 0 && ((RemotingCompressionAttribute) customAttributes[0]).EnableCompression;
      }
    }
}
