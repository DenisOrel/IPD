
// Type: Intermech.Search.FindCompositionParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search
{
    [Serializable]
    public sealed class FindCompositionParams
    {
      public static bool Check(FindCompositionParams findCompositionParams)
      {
        if (findCompositionParams == null)
          throw new ArgumentNullException(nameof (findCompositionParams));
        if (!ObjectHelper.IsUnknownObjectVersionID(findCompositionParams.ProjectVersionID) && (findCompositionParams.ProjectVersionIds == null || findCompositionParams.ProjectVersionIds.Length == 0))
          return true;
        return ObjectHelper.IsUnknownObjectVersionID(findCompositionParams.ProjectVersionID) && findCompositionParams.ProjectVersionIds != null && findCompositionParams.ProjectVersionIds.Length != 0 && ((IEnumerable<long>) findCompositionParams.ProjectVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() == 0;
      }

      public FindCompositionParams()
      {
        this.ProjectVersionID = 0L;
        this.RelationTypeID = -1;
        this.PartTypeID = -1;
      }

      public long ProjectVersionID { get; set; }

      public long[] ProjectVersionIds { get; set; }

      public int RelationTypeID { get; set; }

      public int PartTypeID { get; set; }

      public int[] PartTypeIds { get; set; }

      public Dictionary<int, int[]> PartTypeIdsByRelationTypeIDDictionary { get; set; }

      public bool AllRelations { get; set; }

      public bool AllPartTypes { get; set; }

      public int[] RelationAttributeTypeIds { get; set; }

      public int[] ObjectAttributeTypeIds { get; set; }

      public string FiltrationOwnerID { get; set; }

      public bool LocalTypesMode { get; set; }

      public ConditionStructure[] Conditions { get; set; }
    }
}
