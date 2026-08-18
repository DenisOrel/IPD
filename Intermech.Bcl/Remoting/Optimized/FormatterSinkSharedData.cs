
// Type: Intermech.Remoting.Optimized.FormatterSinkSharedData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.IO;
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace Intermech.Remoting.Optimized
{
    /// <summary>
    /// Сервис общих данных для всех FormatterSink.
    /// Реализация является thread safe.
    /// </summary>
    public sealed class FormatterSinkSharedData
    {
      private ThreadLocal<BinaryFormatter> _serializeFormatter;
      private ThreadLocal<BinaryFormatter> _deserializeFormatter;
      private ImChunkedStreamConcurrentBufferPool _memoryStreamBufferPool;

      public FormatterSinkSharedData(
        FormatterSinkChannelProtocol protocol,
        bool includeVersioning,
        bool strictBinding,
        TypeFilterLevel formatterSecurityLevel)
      {
        this.Protocol = protocol;
        this.IncludeVersioning = includeVersioning;
        this.StrictBinding = strictBinding;
        this.FormatterSecurityLevel = formatterSecurityLevel;
        this._serializeFormatter = new ThreadLocal<BinaryFormatter>(new Func<BinaryFormatter>(this.CreateSerializeBinaryFormatterSlow));
        this._deserializeFormatter = new ThreadLocal<BinaryFormatter>(new Func<BinaryFormatter>(this.CreateDeserializeBinaryFormatterSlow));
        this._memoryStreamBufferPool = new ImChunkedStreamConcurrentBufferPool(4096 /*0x1000*/);
      }

      public FormatterSinkChannelProtocol Protocol { get; private set; }

      public bool IncludeVersioning { get; private set; }

      public bool StrictBinding { get; private set; }

      public TypeFilterLevel FormatterSecurityLevel { get; private set; }

      private BinaryFormatter CreateSerializeBinaryFormatterSlow()
      {
        return new BinaryFormatter()
        {
          SurrogateSelector = (ISurrogateSelector) new RemotingSurrogateSelector(),
          Context = new StreamingContext(StreamingContextStates.Other),
          AssemblyFormat = this.IncludeVersioning || this.StrictBinding ? FormatterAssemblyStyle.Full : FormatterAssemblyStyle.Simple,
          FilterLevel = this.FormatterSecurityLevel
        };
      }

      private BinaryFormatter CreateDeserializeBinaryFormatterSlow()
      {
        return new BinaryFormatter()
        {
          SurrogateSelector = (ISurrogateSelector) null,
          Context = new StreamingContext(StreamingContextStates.Other),
          AssemblyFormat = this.IncludeVersioning || this.StrictBinding ? FormatterAssemblyStyle.Full : FormatterAssemblyStyle.Simple,
          FilterLevel = this.FormatterSecurityLevel
        };
      }

      internal BinaryFormatter GetBinaryFormatter(bool serializing)
      {
        return !serializing ? this._deserializeFormatter.Value : this._serializeFormatter.Value;
      }

      internal Stream CreateMemoryStream()
      {
        return (Stream) new ImChunkedStream((IByteBufferPool) this._memoryStreamBufferPool);
      }
    }
}
