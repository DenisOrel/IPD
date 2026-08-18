
// Type: Intermech.Search.Data.Repositories.LifecycleLevelRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class LifecycleLevelRepository : ILifecycleLevelRepository
    {
      public void AddOrUpdate(IMSLifeCycleLevel lifecycleLevel) => throw new NotSupportedException();

      public IMSLifeCycleLevel Find(int lifecycleLevelID)
      {
        return MetaDataHelper.GetLCLevel(lifecycleLevelID);
      }

      public IMSLifeCycleLevel Find(Guid lifecycleLevelGuid)
      {
        return MetaDataHelper.GetLCLevel(lifecycleLevelGuid);
      }

      public List<IMSLifeCycleLevel> FindAll() => MetaDataHelper.GetLCLevelsList();

      public void Remove(int lifecycleLevelID) => throw new NotSupportedException();

      public void RemoveAll() => throw new NotSupportedException();
    }
}
