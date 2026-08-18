
// Type: Intermech.Interfaces.ForumEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumEventArgs : EventArgs
    {
      public long ObjectID { get; }

      public Guid SessionGuid { get; }

      public List<long> ResultIDs { get; } = new List<long>();

      public ForumEventArgs(long objectID, Guid sessionGuid)
      {
        this.ObjectID = objectID;
        this.SessionGuid = sessionGuid;
      }
    }
}
