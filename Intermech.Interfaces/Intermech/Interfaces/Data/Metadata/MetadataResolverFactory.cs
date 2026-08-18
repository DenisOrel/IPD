
// Type: Intermech.Interfaces.Data.Metadata.MetadataResolverFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Memoization;
using Intermech.Threading;
using System;


namespace Intermech.Interfaces.Data.Metadata
{
    public sealed class MetadataResolverFactory
    {
      private IStateMonitor changeMonitor;

      public MetadataResolverFactory(IMetadataChangeMonitor changeMonitor)
      {
        this.changeMonitor = changeMonitor != null ? (IStateMonitor) changeMonitor : throw new ArgumentNullException(nameof (changeMonitor));
      }

      public Intermech.Interfaces.Data.Metadata.AttributeTypeResolver AttributeTypeResolver(
        Guid attributeGuid)
      {
        return new Intermech.Interfaces.Data.Metadata.AttributeTypeResolver(attributeGuid, this.changeMonitor, (ISyncRoot) new RefSyncRoot());
      }

      public Intermech.Interfaces.Data.Metadata.ObjectTypeResolver ObjectTypeResolver(
        Guid objectTypeGuid)
      {
        return new Intermech.Interfaces.Data.Metadata.ObjectTypeResolver(objectTypeGuid, this.changeMonitor, (ISyncRoot) new RefSyncRoot());
      }

      public Intermech.Interfaces.Data.Metadata.RelationTypeResolver RelationTypeResolver(
        Guid relationTypeGuid)
      {
        return new Intermech.Interfaces.Data.Metadata.RelationTypeResolver(relationTypeGuid, this.changeMonitor, (ISyncRoot) new RefSyncRoot());
      }

      public Intermech.Interfaces.Data.Metadata.SpecialObjectResolver SpecialObjectResolver(
        Guid objectVersionGuid)
      {
        return new Intermech.Interfaces.Data.Metadata.SpecialObjectResolver(objectVersionGuid, this.changeMonitor, (ISyncRoot) new RefSyncRoot());
      }
    }
}
