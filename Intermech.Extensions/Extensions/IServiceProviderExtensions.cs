// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IServiceProviderExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Extensions;

public static class IServiceProviderExtensions
{
  [NotNull]
  public static IServiceProvider MergeWithProvider(
    [NotNull] this IServiceProvider provider1,
    [NotNull] IServiceProvider provider2)
  {
    return provider1 == provider2 ? provider1 : (IServiceProvider) new IServiceProviderExtensions.MergedServiceContainer(provider1, provider2);
  }

  private sealed class MergedServiceContainer : IServiceProvider
  {
    [NotNull]
    private readonly IServiceProvider _provider1;
    [NotNull]
    private readonly IServiceProvider _provider2;

    public MergedServiceContainer([NotNull] IServiceProvider provider1, [NotNull] IServiceProvider provider2)
    {
      this._provider1 = provider1;
      this._provider2 = provider2;
    }

    [CanBeNull]
    public object GetService([NotNull] Type serviceType)
    {
      return this._provider1.GetService(serviceType) ?? this._provider2.GetService(serviceType);
    }
  }
}
