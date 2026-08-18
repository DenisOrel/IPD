// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ICustomServicesExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Server;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ICustomServicesExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IServiceProvider AsProvider([NotNull] this ICustomServices customServices)
  {
    return (IServiceProvider) new ICustomServicesExtensions.CustomServicesProviderWrapper(customServices);
  }

  private sealed class CustomServicesProviderWrapper : IServiceProvider
  {
    [NotNull]
    private readonly ICustomServices _customServices;

    public CustomServicesProviderWrapper([NotNull] ICustomServices customServices)
    {
      this._customServices = customServices;
    }

    [CanBeNull]
    public object GetService([NotNull] Type serviceType)
    {
      return this._customServices.GetService(serviceType);
    }
  }
}
