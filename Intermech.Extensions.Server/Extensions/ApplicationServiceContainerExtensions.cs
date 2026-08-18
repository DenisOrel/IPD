// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ApplicationServiceContainerExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Interfaces.Server;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ApplicationServiceContainerExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IServiceProvider WithCustomServices(
    [NotNull] this ApplicationServiceContainer applicationServiceContainer)
  {
    ICustomServices service = applicationServiceContainer.GetService<ICustomServices>(false);
    return service == null ? (IServiceProvider) applicationServiceContainer : applicationServiceContainer.MergeWithProvider(service.AsProvider());
  }
}
