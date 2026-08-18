// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BinaryServerFormatterSinkPatcherProvider
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using Intermech.Remoting.Optimized;
using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class BinaryServerFormatterSinkPatcherProvider : ServerFormatterSinkWrapperProvider
{
  public BinaryServerFormatterSinkPatcherProvider(IDictionary properties, ICollection providerData)
    : base(properties, providerData)
  {
    BinaryServerFormatterSinkPatcherProvider.DynamicFormatterSinkInterceptorProvider interceptorProvider = new BinaryServerFormatterSinkPatcherProvider.DynamicFormatterSinkInterceptorProvider();
    properties[(object) "interceptors"] = (object) new Func<IServerFormatterSinkInterceptor>(interceptorProvider.TryGet);
  }

  protected override IServerFormatterSinkProvider CreateNativeProvider()
  {
    return (IServerFormatterSinkProvider) new OptimizedBinaryServerFormatterSinkProvider(this.Properties, this.ProviderData);
  }

  protected override IServerChannelSink CreateNativeSinkWrapper(
    IChannelReceiver channel,
    IServerChannelSink nativeSink)
  {
    return (IServerChannelSink) new BinaryServerFormatterSinkPatcher(nativeSink);
  }

  private sealed class DynamicFormatterSinkInterceptorProvider
  {
    private ThreadLocal<IServerFormatterSinkInterceptor> threadBoundInterceptor;

    public DynamicFormatterSinkInterceptorProvider()
    {
      this.threadBoundInterceptor = new ThreadLocal<IServerFormatterSinkInterceptor>();
    }

    public IServerFormatterSinkInterceptor TryGet()
    {
      if (this.threadBoundInterceptor.Value == null)
      {
        Func<IServerFormatterSinkInterceptor> interceptorFactory = ServerRemotingDynamicSettings.Instance.FormatterSinkInterceptorFactory;
        if (interceptorFactory != null)
        {
          IServerFormatterSinkInterceptor formatterSinkInterceptor = interceptorFactory();
          if (formatterSinkInterceptor != null)
            this.threadBoundInterceptor.Value = formatterSinkInterceptor;
        }
      }
      return this.threadBoundInterceptor.Value;
    }
  }
}
