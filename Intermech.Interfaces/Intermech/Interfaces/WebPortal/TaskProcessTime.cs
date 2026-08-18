
// Type: Intermech.Interfaces.WebPortal.TaskProcessTime
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.IO;
using System.Text;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Окно для выполнения задачи</summary>
    [Serializable]
    public class TaskProcessTime : ProcessTime
    {
      /// <summary>Максимаьное количество часов выполнения</summary>
      public int WorkingHours;
      /// <summary>
      /// Максимаьное количество минут выполнения вместе с часами
      /// </summary>
      public int WorkingMinutes;
      /// <summary>Флаг, выполняются задачи всех приоритетов</summary>
      public bool AllPriorities;
      /// <summary>
      /// Операторы для определения выполняемых приоритетов
      /// (используются равно, больше, меньше)
      /// </summary>
      public RelationalOperators PriorityOperator;
      /// <summary>
      /// Приоритет выполняемых задач, учитывая оператор PriorityOperator
      ///  </summary>
      public TaskPriority Priority;

      /// <summary>Конструктор</summary>
      public TaskProcessTime()
      {
        this.WorkingHours = 0;
        this.WorkingMinutes = 0;
        this.AllPriorities = true;
        this.PriorityOperator = RelationalOperators.Equal;
        this.Priority = TaskPriority.Normal;
      }

      public TaskProcessTime(
        TimePeriod period,
        DateTime beginDateTime,
        int[] daysOfWeek,
        int dayOfMonth,
        int[] months,
        int workingHours,
        int workingMinutes,
        bool allPriorities,
        RelationalOperators priorityOperator,
        TaskPriority priority,
        EveryDayExecution dayExecution)
        : base(period, beginDateTime, daysOfWeek, dayOfMonth, months, dayExecution)
      {
        this.WorkingHours = workingHours;
        this.WorkingMinutes = workingMinutes;
        this.AllPriorities = allPriorities;
        this.PriorityOperator = priorityOperator;
        this.Priority = priority;
      }

      public override string Save()
      {
        using (MemoryStream output = new MemoryStream())
        {
          BinaryWriter bw = new BinaryWriter((Stream) output, Encoding.UTF8);
          try
          {
            this.SaveToStream(bw);
            bw.Write(this.WorkingHours);
            bw.Write(this.WorkingMinutes);
            bw.Write(this.AllPriorities);
            bw.Write((int) this.PriorityOperator);
            bw.Write((int) this.Priority);
            bw.Write((int) this.DayExecution);
          }
          finally
          {
            bw.Flush();
            bw.Close();
          }
          return Convert.ToBase64String(output.ToArray());
        }
      }

      public override void Load(string data)
      {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(data)))
        {
          BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8);
          try
          {
            this.LoadFromStream(br);
            this.WorkingHours = br.ReadInt32();
            this.WorkingMinutes = br.ReadInt32();
            this.AllPriorities = br.ReadBoolean();
            this.PriorityOperator = (RelationalOperators) br.ReadInt32();
            this.Priority = (TaskPriority) br.ReadInt32();
            this.DayExecution = (EveryDayExecution) br.ReadInt32();
          }
          finally
          {
            br.Close();
          }
        }
      }
    }
}
