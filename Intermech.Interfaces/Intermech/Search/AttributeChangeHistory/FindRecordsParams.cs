
// Type: Intermech.Search.AttributeChangeHistory.FindRecordsParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Search.AttributeChangeHistory
{
    [Serializable]
    public sealed class FindRecordsParams
    {
      public int[] AttributeTypeIds { get; set; }

      public int[] ObjectTypeIds { get; set; }

      public int[] RelationTypeIds { get; set; }

      public long[] UserAndUserGroupsVersionIds { get; set; }

      public DateTime From { get; set; }

      public DateTime To { get; set; }

      public long[] ObjectVersionIds { get; set; }

      public ObligatoryObjectAttributes[] SortColumns { get; set; }

      public Intermech.Kernel.Search.SortOrders[] SortOrders { get; set; }

      public long LastRecordKey { get; set; }
    }
}
