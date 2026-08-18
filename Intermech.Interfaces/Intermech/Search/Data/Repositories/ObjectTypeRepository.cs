
// Type: Intermech.Search.Data.Repositories.ObjectTypeRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class ObjectTypeRepository : IObjectTypeRepository
    {
      public void AddOrUpdate(IMSObjectType objectType) => throw new NotSupportedException();

      public IMSObjectType Find(int objectTypeID) => MetaDataHelper.GetObjectType(objectTypeID);

      public IMSObjectType Find(Guid objectTypeGuid) => MetaDataHelper.GetObjectType(objectTypeGuid);

      public List<IMSObjectType> FindAll() => MetaDataHelper.GetObjectTypesList();

      public void Remove(int objectTypeID) => throw new NotSupportedException();

      public void RemoveAll() => throw new NotSupportedException();
    }
}
