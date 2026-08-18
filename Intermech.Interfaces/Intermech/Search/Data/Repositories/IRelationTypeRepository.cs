
// Type: Intermech.Search.Data.Repositories.IRelationTypeRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IRelationTypeRepository
    {
      void AddOrUpdate(IMSRelationType relationType);

      IMSRelationType Find(int relationTypeID);

      IMSRelationType Find(Guid relationTypeGuid);

      List<IMSRelationType> FindAll();

      void Remove(int relationTypeID);

      void RemoveAll();
    }
}
