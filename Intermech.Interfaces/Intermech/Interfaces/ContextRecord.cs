
// Type: Intermech.Interfaces.ContextRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    [Serializable]
    public class ContextRecord
    {
      public long Id;
      public long ContextId;
      public List<long> ObjectIDs;

      public ContextRecord()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="contextGuid"></param>
      /// <param name="objectIDs">забирает внутрь, клон не делается</param>
      public ContextRecord(long id, long contextId, List<long> objectIDs)
      {
        this.Id = id;
        this.ContextId = contextId;
        this.ObjectIDs = objectIDs;
      }
    }
}
