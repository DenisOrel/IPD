
// Type: Intermech.Search.Data.Repositories.RelationTypeRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class RelationTypeRepository : IRelationTypeRepository
    {
      public void AddOrUpdate(IMSRelationType relationType) => throw new NotSupportedException();

      public IMSRelationType Find(int relationTypeID) => MetaDataHelper.GetRelationType(relationTypeID);

      public IMSRelationType Find(Guid relationTypeGuid)
      {
        return MetaDataHelper.GetRelationType(relationTypeGuid);
      }

      public List<IMSRelationType> FindAll() => MetaDataHelper.GetRelationTypesList();

      public void Remove(int relationTypeID) => throw new NotSupportedException();

      public void RemoveAll() => throw new NotSupportedException();
    }
}
