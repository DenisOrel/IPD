
// Type: Intermech.Search.EditingContexts.FindEditingContextParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class FindEditingContextParams
    {
      public static bool Check(FindEditingContextParams findEditingContextParams)
      {
        if (findEditingContextParams == null)
          throw new ArgumentNullException(nameof (findEditingContextParams));
        if (findEditingContextParams.AttributeTypeIds == null)
          return true;
        return findEditingContextParams.AttributeTypeIds != null && ((IEnumerable<int>) findEditingContextParams.AttributeTypeIds).Where<int>((Func<int, bool>) (o => AttributeTypeHelper.IsUnknownAttributeTypeID(o))).Count<int>() == 0;
      }

      public FindEditingContextParams(long objectVersionID)
      {
        this.EditingContextVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
      }

      public long EditingContextVersionID { get; private set; }

      public int[] AttributeTypeIds { get; set; }
    }
}
