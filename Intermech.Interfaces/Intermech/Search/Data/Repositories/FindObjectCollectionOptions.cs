
// Type: Intermech.Search.Data.Repositories.FindObjectCollectionOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class FindObjectCollectionOptions
    {
      public FindObjectCollectionOptions() => this.ObjectTypeID = -1;

      public int ObjectTypeID { get; set; }

      public List<ConditionStructure> Conditions { get; set; }

      public int[] AttributeTypeIds { get; set; }

      public List<int> SortAttributeTypeIds { get; set; }

      public List<SortOrders> SortDirections { get; set; }

      public int PageNumber { get; set; }

      public int CountOnPage { get; set; }

      public string SearchText { get; set; }

      public bool DisableEditingContextFiltration { get; set; }

      public long[] ObjectVersionIds { get; set; }

      public Dictionary<int, List<long>> ObjectVersionIdsByObjectTypeIDDictionary { get; set; }
    }
}
