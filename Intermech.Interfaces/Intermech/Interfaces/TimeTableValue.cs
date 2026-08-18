
// Type: Intermech.Interfaces.TimeTableValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Временная отметка</summary>
    [Serializable]
    public class TimeTableValue
    {
      public string Day;
      public DateTime Time;

      public TimeTableValue(string day, DateTime time)
      {
        this.Day = day;
        this.Time = time;
      }
    }
}
