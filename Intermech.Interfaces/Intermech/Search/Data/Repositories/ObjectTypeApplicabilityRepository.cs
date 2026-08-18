
// Type: Intermech.Search.Data.Repositories.ObjectTypeApplicabilityRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Data.Repositories
{
    public sealed class ObjectTypeApplicabilityRepository : IObjectTypeApplicabilityRepository
    {
      public void AddOrUpdate(IMSApplicability objectTypeApplicability)
      {
        throw new NotImplementedException();
      }

      public List<IMSApplicability> Find(int parentObjectTypeID)
      {
        return parentObjectTypeID != -1 ? MetaDataHelper.GetObjectTypeApplicabilities(parentObjectTypeID) : throw new ArgumentException();
      }

      public List<IMSApplicability> Find(int parentObjectTypeID, int relationTypeID)
      {
        if (parentObjectTypeID == -1)
          throw new ArgumentException();
        if (relationTypeID == -1)
          throw new ArgumentException();
        return MetaDataHelper.GetObjectTypeApplicabilities(parentObjectTypeID).Where<IMSApplicability>((Func<IMSApplicability, bool>) (o => o.RelationTypeID == relationTypeID)).ToList<IMSApplicability>();
      }

      public List<IMSApplicability> FindAll() => throw new NotImplementedException();

      public void RemoveAll() => throw new NotImplementedException();
    }
}
