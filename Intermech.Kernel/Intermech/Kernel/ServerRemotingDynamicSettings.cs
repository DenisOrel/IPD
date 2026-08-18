// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerRemotingDynamicSettings
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting.Optimized;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

public sealed class ServerRemotingDynamicSettings
{
  private volatile Func<IServerFormatterSinkInterceptor> formatterSinkInterceptorFactory;
  private static readonly ServerRemotingDynamicSettings instance = new ServerRemotingDynamicSettings();

  public Func<IServerFormatterSinkInterceptor> FormatterSinkInterceptorFactory
  {
    [DebuggerStepThrough] get => this.formatterSinkInterceptorFactory;
    [DebuggerStepThrough] set
    {
      Interlocked.Exchange<Func<IServerFormatterSinkInterceptor>>(ref this.formatterSinkInterceptorFactory, value);
    }
  }

  public static ServerRemotingDynamicSettings Instance
  {
    [DebuggerStepThrough] get => ServerRemotingDynamicSettings.instance;
  }
}
