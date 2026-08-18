
// Type: Intermech.Search.Data.Repositories.IObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IObjectRepository
    {
      long AddOrUpdate(_Object @object);

      _Object Find(long objectVersionID, bool includeBlobs = true);

      List<_Object> Find(List<ConditionStructure> conditions);

      List<_Object> Find(int objectTypeID);

      List<_Object> Find(int objectTypeID, List<ConditionStructure> conditions);

      List<_Object> Find(FindObjectCollectionOptions options);

      int FindCount(FindObjectCountOptions options);

      void Remove(long objectVersionID);

      void Remove(int objectTypeID);

      void Remove(List<ConditionStructure> conditions);
    }
}
