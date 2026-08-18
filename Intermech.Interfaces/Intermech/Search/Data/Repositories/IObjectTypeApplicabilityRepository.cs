
// Type: Intermech.Search.Data.Repositories.IObjectTypeApplicabilityRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IObjectTypeApplicabilityRepository
    {
      void AddOrUpdate(IMSApplicability objectTypeApplicability);

      List<IMSApplicability> Find(int parentObjectTypeID);

      List<IMSApplicability> Find(int parentObjectTypeID, int relationTypeID);

      List<IMSApplicability> FindAll();

      void RemoveAll();
    }
}
