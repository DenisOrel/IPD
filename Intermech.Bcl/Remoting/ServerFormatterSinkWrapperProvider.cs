
// Type: Intermech.Remoting.ServerFormatterSinkWrapperProvider
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
    public abstract class ServerFormatterSinkWrapperProvider : 
      IServerFormatterSinkProvider,
      IServerChannelSinkProvider
    {
      private readonly IDictionary properties;
      private readonly ICollection providerData;
      private readonly Lazy<IServerFormatterSinkProvider> nativeProvider;

      protected ServerFormatterSinkWrapperProvider(IDictionary properties, ICollection providerData)
      {
        this.properties = properties;
        this.providerData = providerData;
        this.nativeProvider = new Lazy<IServerFormatterSinkProvider>(new Func<IServerFormatterSinkProvider>(this.CreateNativeProvider), LazyThreadSafetyMode.PublicationOnly);
      }

      protected abstract IServerFormatterSinkProvider CreateNativeProvider();

      protected abstract IServerChannelSink CreateNativeSinkWrapper(
        IChannelReceiver channel,
        IServerChannelSink nativeSink);

      protected IDictionary Properties => this.properties;

      protected ICollection ProviderData => this.providerData;

      public IServerFormatterSinkProvider NativeProvider => this.nativeProvider.Value;

      public IServerChannelSinkProvider Next
      {
        get => this.nativeProvider.Value.Next;
        set => this.nativeProvider.Value.Next = value;
      }

      public void GetChannelData(IChannelDataStore channelData)
      {
        this.nativeProvider.Value.GetChannelData(channelData);
      }

      public IServerChannelSink CreateSink(IChannelReceiver channel)
      {
        IServerChannelSink sink = this.nativeProvider.Value.CreateSink(channel);
        return this.CreateNativeSinkWrapper(channel, sink);
      }
    }
}
