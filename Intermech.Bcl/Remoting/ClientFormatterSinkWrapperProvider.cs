
// Type: Intermech.Remoting.ClientFormatterSinkWrapperProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Threading;


namespace Intermech.Remoting
{
    public abstract class ClientFormatterSinkWrapperProvider : 
      IClientFormatterSinkProvider,
      IClientChannelSinkProvider
    {
      private readonly IDictionary properties;
      private readonly ICollection providerData;
      private readonly Lazy<IClientFormatterSinkProvider> nativeProvider;

      protected ClientFormatterSinkWrapperProvider(IDictionary properties, ICollection providerData)
      {
        this.properties = properties;
        this.providerData = providerData;
        this.nativeProvider = new Lazy<IClientFormatterSinkProvider>(new Func<IClientFormatterSinkProvider>(this.CreateNativeProvider), LazyThreadSafetyMode.PublicationOnly);
      }

      protected abstract IClientFormatterSinkProvider CreateNativeProvider();

      protected abstract IClientFormatterSink CreateNativeSinkWrapper(
        IChannelSender channel,
        string url,
        object remoteChannelData,
        IClientFormatterSink nativeSink);

      protected IDictionary Properties => this.properties;

      protected ICollection ProviderData => this.providerData;

      public IClientFormatterSinkProvider NativeProvider => this.nativeProvider.Value;

      public IClientChannelSink CreateSink(
        IChannelSender channel,
        string url,
        object remoteChannelData)
      {
        IClientFormatterSink sink = (IClientFormatterSink) this.nativeProvider.Value.CreateSink(channel, url, remoteChannelData);
        return (IClientChannelSink) this.CreateNativeSinkWrapper(channel, url, remoteChannelData, sink);
      }

      public IClientChannelSinkProvider Next
      {
        get => this.nativeProvider.Value.Next;
        set => this.nativeProvider.Value.Next = value;
      }
    }
}
