
// Type: Intermech.Interfaces.Data.Metadata.SpecialObjectResolver
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Memoization;
using Intermech.Threading;
using System;


namespace Intermech.Interfaces.Data.Metadata
{
    public class SpecialObjectResolver(Guid guid, IStateMonitor changeMonitor, ISyncRoot syncRoot) : 
      MetadataResolverBase<long>(guid, changeMonitor, syncRoot)
    {
      protected override GlobalId<long> CreateGID(Guid guid)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(guid);
          return new GlobalId<long>(guid, dbObject.ObjectID, dbObject.Caption);
        }
      }
    }
}
