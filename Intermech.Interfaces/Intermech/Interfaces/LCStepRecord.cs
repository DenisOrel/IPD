
// Type: Intermech.Interfaces.LCStepRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    [Serializable]
    public struct LCStepRecord(long objectId, int lcStep, DateTime lcStartDate)
    {
      public long ObjectId = objectId;
      public int LCStep = lcStep;
      public DateTime LCStartDate = lcStartDate;
    }
}
