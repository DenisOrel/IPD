
// Type: Intermech.Interfaces.Data.Metadata.AttributeTypeResolver
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Memoization;
using Intermech.Threading;
using System;


namespace Intermech.Interfaces.Data.Metadata
{
    public class AttributeTypeResolver(Guid guid, IStateMonitor changeMonitor, ISyncRoot syncRoot) : 
      MetadataResolverBase<int>(guid, changeMonitor, syncRoot)
    {
      protected override GlobalId<int> CreateGID(Guid guid)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(guid);
        return new GlobalId<int>(guid, attributeType.AttributeID, attributeType.Name);
      }
    }
}
