
// Type: Intermech.Interfaces.Data.Metadata.MetadataResolverBase`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Memoization;
using Intermech.Threading;
using System;


namespace Intermech.Interfaces.Data.Metadata
{
    public abstract class MetadataResolverBase<T> : IMetadataResolver<T>
    {
      private readonly Guid guid;
      private readonly Func<Guid, GlobalId<T>> memoizedFunc;

      protected MetadataResolverBase(Guid guid, IStateMonitor changeMonitor, ISyncRoot syncRoot)
      {
        this.guid = guid;
        this.memoizedFunc = ListScanMemoizer<Guid, GlobalId<T>>.Wrap(new Func<Guid, GlobalId<T>>(this.CreateGID), changeMonitor, syncRoot);
      }

      public GlobalId<T> GID => this.memoizedFunc(this.guid);

      public Guid Guid => this.guid;

      public T Id => this.GID.Id;

      public string Text => this.GID.Name;

      protected abstract GlobalId<T> CreateGID(Guid guid);
    }
}
