
// Type: Intermech.Interfaces.ProcessTime
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>Время расписания</summary>
    [Serializable]
    public class ProcessTime
    {
      /// <summary>Периодичность выполнения</summary>
      public TimePeriod Period;
      /// <summary>Дата/время начала выполнения</summary>
      public DateTime BeginDateTime;
      /// <summary>Дни недели</summary>
      public int[] DaysOfWeek;
      /// <summary>День месяца</summary>
      public int DayOfMonth;
      /// <summary>Месяцы</summary>
      public int[] Months;
      /// <summary>ЗАРЕЗЕРВИРОВАНО</summary>
      public EveryDayExecution DayExecution;

      /// <summary>Конструктор</summary>
      public ProcessTime()
      {
        this.Period = TimePeriod.OneTime;
        this.BeginDateTime = DateTime.Now + TimeSpan.FromHours(1.0);
        this.DaysOfWeek = (int[]) null;
        this.DayOfMonth = 1;
        this.Months = (int[]) null;
      }

      public ProcessTime(
        TimePeriod period,
        DateTime beginDateTime,
        int[] daysOfWeek,
        int dayOfMonth,
        int[] months,
        EveryDayExecution dayExecution)
      {
        this.Period = period;
        this.BeginDateTime = beginDateTime;
        this.DaysOfWeek = daysOfWeek;
        this.DayOfMonth = dayOfMonth;
        this.Months = months;
        this.DayExecution = dayExecution;
      }

      protected void SaveToStream(BinaryWriter bw)
      {
        bw.Write((int) this.Period);
        bw.Write(this.BeginDateTime.Ticks);
        if (this.DaysOfWeek != null && this.DaysOfWeek.Length != 0)
        {
          bw.Write(this.DaysOfWeek.Length);
          for (int index = 0; index < this.DaysOfWeek.Length; ++index)
            bw.Write(this.DaysOfWeek[index]);
        }
        else
          bw.Write(0);
        bw.Write(this.DayOfMonth);
        if (this.Months != null && this.Months.Length != 0)
        {
          bw.Write(this.Months.Length);
          for (int index = 0; index < this.Months.Length; ++index)
            bw.Write(this.Months[index]);
        }
        else
          bw.Write(0);
      }

      protected void LoadFromStream(BinaryReader br)
      {
        this.Period = (TimePeriod) br.ReadInt32();
        this.BeginDateTime = new DateTime(br.ReadInt64());
        int length1 = br.ReadInt32();
        if (length1 > 0)
        {
          this.DaysOfWeek = new int[length1];
          for (int index = 0; index < length1; ++index)
            this.DaysOfWeek[index] = br.ReadInt32();
        }
        this.DayOfMonth = br.ReadInt32();
        int length2 = br.ReadInt32();
        if (length2 <= 0)
          return;
        this.Months = new int[length2];
        for (int index = 0; index < length2; ++index)
          this.Months[index] = br.ReadInt32();
      }

      public virtual string Save()
      {
        using (MemoryStream output = new MemoryStream())
        {
          using (BinaryWriter bw = new BinaryWriter((Stream) output, Encoding.UTF8))
          {
            this.SaveToStream(bw);
            bw.Write((int) this.DayExecution);
            bw.Flush();
          }
          return Convert.ToBase64String(output.ToArray());
        }
      }

      public virtual void Load(string data)
      {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(data)))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
          {
            this.LoadFromStream(br);
            this.DayExecution = (EveryDayExecution) br.ReadInt32();
          }
        }
      }
    }
}
