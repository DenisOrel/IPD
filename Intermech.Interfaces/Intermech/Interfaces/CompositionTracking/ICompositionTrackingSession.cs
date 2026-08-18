
// Type: Intermech.Interfaces.CompositionTracking.ICompositionTrackingSession
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Composition's tracking session</summary>
    public interface ICompositionTrackingSession
    {
      /// <summary>Tracking sessions Guid</summary>
      Guid SessionGuid { get; }

      /// <summary>
      /// Get objects list, which were changed in current session
      /// </summary>
      /// <returns></returns>
      Dictionary<CompositionTrackingCommands, List<long>> GetSessionLog();
    }
}
