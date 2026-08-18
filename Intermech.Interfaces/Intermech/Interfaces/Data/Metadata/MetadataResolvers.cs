
// Type: Intermech.Interfaces.Data.Metadata.MetadataResolvers
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Diagnostics;


namespace Intermech.Interfaces.Data.Metadata
{
    public static class MetadataResolvers
    {
      private static readonly ServiceRef<IMetadataChangeMonitor> changeMonitorRef = new ServiceRef<IMetadataChangeMonitor>();
      private static readonly ServiceRef<MetadataResolverFactory> factoryRef = new ServiceRef<MetadataResolverFactory>();

      /// <summary>
      /// Возвращает или задает общедоступный экземпляр монитора за изменением метаданных IPS.
      /// </summary>
      public static IMetadataChangeMonitor ChangeMonitor
      {
        [DebuggerStepThrough] get => MetadataResolvers.changeMonitorRef.Value;
        [DebuggerStepThrough] set => MetadataResolvers.changeMonitorRef.Value = value;
      }

      /// <summary>
      /// Возвращает или задает общедоступный экземпляр фабрики резолверов для метаданных IPS.
      /// </summary>
      public static MetadataResolverFactory Factory
      {
        [DebuggerStepThrough] get => MetadataResolvers.factoryRef.Value;
        [DebuggerStepThrough] set => MetadataResolvers.factoryRef.Value = value;
      }
    }
}
