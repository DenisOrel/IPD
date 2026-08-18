
// Type: Intermech.Search.Data.Repositories.LifecycleStepRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class LifecycleStepRepository : ILifecycleStepRepository
    {
      public void AddOrUpdate(IMSLifeCycleStep lifecycleStep) => throw new NotSupportedException();

      public IMSLifeCycleStep Find(int lifecycleStepID) => MetaDataHelper.GetLCStep(lifecycleStepID);

      public IMSLifeCycleStep Find(Guid lifecycleStepGuid)
      {
        return MetaDataHelper.GetLCStep(lifecycleStepGuid);
      }

      public List<IMSLifeCycleStep> FindAll() => MetaDataHelper.GetLCStepsList();

      public void Remove(int lifecycleStepID) => throw new NotSupportedException();

      public void RemoveAll() => throw new NotSupportedException();
    }
}
