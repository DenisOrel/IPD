
// Type: Intermech.Search.EditingContexts.AddObjectsToEditingContextParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class AddObjectsToEditingContextParams
    {
      public static bool Check(
        AddObjectsToEditingContextParams addObjectsToeditingContextParams)
      {
        if (addObjectsToeditingContextParams == null)
          throw new ArgumentNullException(nameof (addObjectsToeditingContextParams));
        return addObjectsToeditingContextParams.ObjectVersionIds != null && addObjectsToeditingContextParams.ObjectVersionIds.Length != 0 && !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) addObjectsToeditingContextParams.ObjectVersionIds);
      }

      public AddObjectsToEditingContextParams(long editingContextVersionID)
      {
        this.EditingContextVersionID = !ObjectHelper.IsUnknownObjectVersionID(editingContextVersionID) ? editingContextVersionID : throw new ArgumentException();
      }

      public long EditingContextVersionID { get; private set; }

      public long[] ObjectVersionIds { get; set; }

      public int[] AttributeTypeIds { get; set; }

      public AddObjectsToEditingContextType Type { get; set; }
    }
}
