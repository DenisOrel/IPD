
// Type: Intermech.Search.EditingContexts.SaveEditingContextParams
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
    public sealed class SaveEditingContextParams
    {
      public static bool Check(SaveEditingContextParams saveEditingContextParams)
      {
        if (saveEditingContextParams == null)
          throw new ArgumentNullException(nameof (saveEditingContextParams));
        if (saveEditingContextParams.ObjectVersionIds == null)
          return true;
        return saveEditingContextParams.ObjectVersionIds != null && ((IEnumerable<long>) saveEditingContextParams.ObjectVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() == 0;
      }

      public SaveEditingContextParams(long editingContextVersionID)
      {
        this.EditingContextVersionID = !ObjectHelper.IsUnknownObjectVersionID(editingContextVersionID) ? editingContextVersionID : throw new ArgumentException();
      }

      public long EditingContextVersionID { get; private set; }

      public long[] ObjectVersionIds { get; set; }
    }
}
