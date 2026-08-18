
// Type: Intermech.Search.Data.Repositories.FindRelationsParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;


namespace Intermech.Search.Data.Repositories
{
    public sealed class FindRelationsParams
    {
      public int RelationTypeID { get; set; }

      public ConditionStructure[] Conditions { get; set; }

      public bool DisableFiltration { get; set; }
    }
}
