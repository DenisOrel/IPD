
// Type: Intermech.Search.Data.Filters.ICompositionFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Search.Data.Filters
{
    public interface ICompositionFilter
    {
      IEnumerable<CompositionPart> Filter(IEnumerable<CompositionPart> composition, object options = null);
    }
}
