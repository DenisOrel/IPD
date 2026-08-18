// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientMetadataCacheNinjectModule
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Ninject.Modules;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Модуль привязок для IOC-контейнера, обеспечивающий создание сервиса <see cref="T:Intermech.Interfaces.Client.IClientMetadataCache" />
/// </summary>
public sealed class ClientMetadataCacheNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IClientMetadataCache>().To<ClientMetadataCacheService>().InSingletonScope();
  }
}
