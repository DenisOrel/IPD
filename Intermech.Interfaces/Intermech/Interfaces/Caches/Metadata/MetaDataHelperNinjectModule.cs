
// Type: Intermech.Interfaces.Caches.Metadata.MetaDataHelperNinjectModule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject.Modules;


namespace Intermech.Interfaces.Caches.Metadata
{
    /// <summary>
    /// Модуль Ninject, обеспечивающий регистрацию сервиса IMetaDataHelper в контейнере сервисов приложения.
    /// </summary>
    public sealed class MetaDataHelperNinjectModule : NinjectModule
    {
      public override void Load()
      {
        this.Bind<IMetaDataHelper, MetaDataHelperService>().To<MetaDataHelperService>().InSingletonScope();
      }
    }
}
