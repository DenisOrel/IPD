
// Type: Intermech.Search.Data.Repositories.ICompositionRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface ICompositionRepository
    {
      List<CompositionPart> Find(
        long projectVersionID,
        int relationTypeID,
        int partTypeID,
        params ConditionStructure[] conditions);

      List<CompositionPart> Find(FindCompositionOptions options);

      int FindCount(FindCompositionPartCountOptions options);
    }
}
