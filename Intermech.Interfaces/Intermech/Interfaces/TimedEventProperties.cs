
// Type: Intermech.Interfaces.TimedEventProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура со свойствами временного события - даты приводить в UTC!
    /// </summary>
    [Serializable]
    public struct TimedEventProperties : ITimedEventProperties
    {
      /// <summary>Ид. события в базе данных</summary>
      public int KeyID;
      /// <summary>Дата и время начала события (в UTC)</summary>
      public DateTime StartDate;
      /// <summary>
      /// Дата, по истечении которой незатребованное событие будет удалено из списка событий (в UTC)
      /// </summary>
      public DateTime DeadlockDate;
      /// <summary>Guid службы, которая должна обработать событие</summary>
      public Guid ServiceGuid;
      /// <summary>
      /// Ссылка на объект, к которому имеет отношение данное событие
      /// </summary>
      public long ObjectID;
      /// <summary>
      /// Пользователь, к которому относится данное событие (если это имеет значение)
      /// </summary>
      public long UserID;
      /// <summary>Строковая информация о событии</summary>
      public string StringInfo;
      /// <summary>Числовая информация о событии</summary>
      public int IntInfo;
      /// <summary>
      /// Сколько раз повторить попытку вызова обработчика события в случа неудачи
      /// </summary>
      public int RetryCount;
      /// <summary>Периодичность события</summary>
      public TimedEventKinds EventKind;
      /// <summary>Имя сервера, на котором должно выполняться событие</summary>
      public string ServerName;
      /// <summary>Строка с расписанием события</summary>
      public string Schedule;
      /// <summary>
      /// Наименование события (если его нужно отображать и настраивать в списке событий)
      /// </summary>
      public string Name;
      /// <summary>
      /// Стартовать ли событие при запуске сервера приложений если запланированный старт был пропущен (для периодических событий)
      /// </summary>
      public bool ImmediateRun;
      /// <summary>
      /// Сообщение об ошибке, с которым завершился последний старт события
      /// </summary>
      public string ErrorMessage;
      /// <summary>Дата и время предыдущего срабатывания</summary>
      public DateTime PreviousDate;
      /// <summary>Статус задачи</summary>
      public string Status;

      /// <summary>Конструктор</summary>
      /// <param name="row">Строка таблицы IMS_TIMED_EVENTS</param>
      public TimedEventProperties(DataRow row)
      {
        this.KeyID = Convert.ToInt32(row["F_KEY"]);
        this.StartDate = Convert.ToDateTime(row["F_DATE"]);
        this.DeadlockDate = row["F_DEADLOCK_DATE"] != DBNull.Value ? Convert.ToDateTime(row["F_DEADLOCK_DATE"]) : DateTime.MinValue;
        this.ServiceGuid = new Guid(row["F_GUID_TYPE"].ToString());
        this.ObjectID = Convert.ToInt64(row["F_OBJECT_ID"]);
        this.UserID = Convert.ToInt64(row["F_USER_ID"]);
        this.StringInfo = row["F_STRING_INFO"].ToString();
        this.IntInfo = Convert.ToInt32(row["F_INT_INFO"]);
        this.RetryCount = Convert.ToInt32(row["F_TRY_COUNT"]);
        this.EventKind = (TimedEventKinds) Convert.ToInt32(row["F_EVENT_KIND"]);
        this.ServerName = row["F_COMPUTER_NAME"].ToString().ToUpper();
        this.Schedule = row["F_SCHEDULE"].ToString();
        this.Name = row["F_NAME"].ToString();
        this.ImmediateRun = Convert.ToInt32(row["F_IMMEDIATE_RUN"]) != 0;
        this.ErrorMessage = row["F_ERROR_MSG"].ToString();
        this.Status = row.Table.Columns.IndexOf("F_STATUS") < 0 ? string.Empty : row["F_STATUS"].ToString();
        if (row["F_PREV_DATE"] == DBNull.Value)
          this.PreviousDate = DateTime.MinValue;
        else
          this.PreviousDate = Convert.ToDateTime(row["F_PREV_DATE"]);
      }

      /// <summary>Конструктор разового события</summary>
      /// <param name="keyID"></param>
      /// <param name="startDate"></param>
      /// <param name="deadlockDate"></param>
      /// <param name="serviceGuid"></param>
      /// <param name="objectID"></param>
      /// <param name="userID"></param>
      /// <param name="stringInfo"></param>
      /// <param name="intInfo"></param>
      /// <param name="retryCount"></param>
      public TimedEventProperties(
        int keyID,
        DateTime startDate,
        DateTime deadlockDate,
        Guid serviceGuid,
        long objectID,
        long userID,
        string stringInfo,
        int intInfo,
        int retryCount)
      {
        this.KeyID = keyID;
        this.StartDate = startDate;
        this.DeadlockDate = deadlockDate;
        this.ServiceGuid = serviceGuid;
        this.ObjectID = objectID;
        this.UserID = userID;
        this.StringInfo = stringInfo;
        this.IntInfo = intInfo;
        this.RetryCount = retryCount;
        this.EventKind = TimedEventKinds.Once;
        this.ServerName = string.Empty;
        this.Schedule = string.Empty;
        this.Name = string.Empty;
        this.ImmediateRun = false;
        this.ErrorMessage = string.Empty;
        this.PreviousDate = DateTime.MinValue;
        this.Status = string.Empty;
      }

      /// <summary>Полный конструктор события</summary>
      /// <param name="keyID"></param>
      /// <param name="startDate"></param>
      /// <param name="deadlockDate"></param>
      /// <param name="serviceGuid"></param>
      /// <param name="objectID"></param>
      /// <param name="userID"></param>
      /// <param name="stringInfo"></param>
      /// <param name="intInfo"></param>
      /// <param name="retryCount"></param>
      /// <param name="eventKind"></param>
      /// <param name="serverName"></param>
      /// <param name="schedule"></param>
      /// <param name="eventName"></param>
      /// <param name="immediateRun"></param>
      /// <param name="errorMessage"></param>
      /// <param name="prevDate"></param>
      public TimedEventProperties(
        int keyID,
        DateTime startDate,
        DateTime deadlockDate,
        Guid serviceGuid,
        long objectID,
        long userID,
        string stringInfo,
        int intInfo,
        int retryCount,
        TimedEventKinds eventKind,
        string serverName,
        string schedule,
        string eventName,
        bool immediateRun,
        string errorMessage,
        DateTime prevDate)
      {
        this.KeyID = keyID;
        this.StartDate = startDate;
        this.DeadlockDate = deadlockDate;
        this.ServiceGuid = serviceGuid;
        this.ObjectID = objectID;
        this.UserID = userID;
        this.StringInfo = stringInfo;
        this.IntInfo = intInfo;
        this.RetryCount = retryCount;
        this.EventKind = eventKind;
        this.ServerName = serverName.ToUpper();
        this.Schedule = schedule;
        this.Name = eventName;
        this.ImmediateRun = immediateRun;
        this.ErrorMessage = errorMessage;
        this.PreviousDate = prevDate;
        this.Status = string.Empty;
      }

      /// <summary>Конструктор периодического события</summary>
      /// <param name="keyID"></param>
      /// <param name="serviceGuid"></param>
      /// <param name="eventKind"></param>
      /// <param name="serverName"></param>
      /// <param name="schedule"></param>
      /// <param name="eventName"></param>
      /// <param name="immediateRun"></param>
      public TimedEventProperties(
        int keyID,
        Guid serviceGuid,
        TimedEventKinds eventKind,
        string serverName,
        string schedule,
        string eventName,
        bool immediateRun)
      {
        this.KeyID = keyID;
        this.StartDate = DateTime.UtcNow;
        this.DeadlockDate = DateTime.MaxValue;
        this.ServiceGuid = serviceGuid;
        this.ObjectID = 0L;
        this.UserID = 0L;
        this.StringInfo = string.Empty;
        this.IntInfo = 0;
        this.RetryCount = 0;
        this.EventKind = eventKind;
        this.ServerName = serverName.ToUpper();
        this.Schedule = schedule;
        this.Name = eventName;
        this.ImmediateRun = immediateRun;
        this.ErrorMessage = string.Empty;
        this.PreviousDate = DateTime.MinValue;
        this.Status = string.Empty;
      }

      /// <summary>
      /// Возвращает дату следующего запуска задачи (для периодических событий)
      /// </summary>
      /// <param name="timeZoneOffset">Часовой пояс для текущей сессия пользователя</param>
      /// <returns>Дата следующего срабатывания события в формате UTC</returns>
      public DateTime GetNextUtcDate(TimeSpan timeZoneOffset)
      {
        return ((ITimedEventProperties) this).GetNextUtcDate(timeZoneOffset, DateTime.UtcNow);
      }

      /// <summary>
      /// Возвращает дату следующего запуска задачи (для периодических событий)
      /// </summary>
      /// <param name="timeZoneOffset">Часовой пояс для текущей сессия пользователя</param>
      /// <param name="currentUtcDateTime">Текущее UTC время</param>
      /// <returns>Дата следующего срабатывания события в формате UTC</returns>
      DateTime ITimedEventProperties.GetNextUtcDate(
        TimeSpan timeZoneOffset,
        DateTime currentUtcDateTime)
      {
        DateTime nextUtcDate = currentUtcDateTime;
        switch (this.EventKind)
        {
          case TimedEventKinds.Hourly:
            if (this.Schedule != string.Empty)
            {
              nextUtcDate += TimeSpan.FromHours(Convert.ToDouble(this.Schedule));
              break;
            }
            nextUtcDate += TimeSpan.FromHours(1.0);
            break;
          case TimedEventKinds.Daily:
            nextUtcDate = currentUtcDateTime.Date + TimeSpan.Parse(this.Schedule);
            if (nextUtcDate < currentUtcDateTime)
            {
              nextUtcDate += TimeSpan.FromDays(1.0);
              break;
            }
            break;
          case TimedEventKinds.Weekly:
            bool flag1 = true;
            nextUtcDate += timeZoneOffset;
            string[] strArray1 = this.Schedule.Split(',');
            for (int index1 = 0; index1 < 7; ++index1)
            {
              bool flag2 = false;
              for (int index2 = 1; index2 < strArray1.Length; ++index2)
              {
                if (nextUtcDate.DayOfWeek == (DayOfWeek) Convert.ToInt32(strArray1[index2]))
                {
                  flag2 = true;
                  break;
                }
              }
              if (flag2)
              {
                TimeSpan timeSpan = TimeSpan.Parse(strArray1[0]) + timeZoneOffset;
                if (timeSpan > TimeSpan.FromDays(1.0))
                  timeSpan -= TimeSpan.FromDays(1.0);
                nextUtcDate = nextUtcDate.Date + timeSpan;
                if (nextUtcDate > currentUtcDateTime + timeZoneOffset)
                  break;
              }
              nextUtcDate = nextUtcDate.AddDays(1.0);
            }
            if (flag1)
            {
              nextUtcDate -= timeZoneOffset;
              break;
            }
            break;
          case TimedEventKinds.Monthly:
            bool flag3 = true;
            nextUtcDate += timeZoneOffset;
            string[] strArray2 = this.Schedule.Split(',');
            for (int index3 = 0; index3 < 31 /*0x1F*/; ++index3)
            {
              bool flag4 = false;
              for (int index4 = 1; index4 < strArray2.Length; ++index4)
              {
                int num = Convert.ToInt32(strArray2[index4]);
                if (DateTime.DaysInMonth(nextUtcDate.Year, nextUtcDate.Month) < num)
                  num = DateTime.DaysInMonth(nextUtcDate.Year, nextUtcDate.Month);
                if (nextUtcDate.Day == num)
                {
                  flag4 = true;
                  break;
                }
              }
              if (flag4)
              {
                TimeSpan timeSpan = TimeSpan.Parse(strArray2[0]) + timeZoneOffset;
                if (timeSpan > TimeSpan.FromDays(1.0))
                  timeSpan -= TimeSpan.FromDays(1.0);
                nextUtcDate = nextUtcDate.Date + timeSpan;
                if (nextUtcDate > currentUtcDateTime + timeZoneOffset)
                  break;
              }
              nextUtcDate = nextUtcDate.AddDays(1.0);
            }
            if (flag3)
            {
              nextUtcDate -= timeZoneOffset;
              break;
            }
            break;
          case TimedEventKinds.Yearly:
            string[] strArray3 = this.Schedule.Split(',');
            if (strArray3.Length != 4)
              throw new KernelException($"Incorrect schedule value for TimedEventKinds.Yearly event: {this.Schedule}");
            nextUtcDate = new DateTime(currentUtcDateTime.Year, Convert.ToInt32(strArray3[3]), Convert.ToInt32(strArray3[2]), Convert.ToInt32(strArray3[0]), Convert.ToInt32(strArray3[1]), 0, DateTimeKind.Utc);
            if (nextUtcDate < currentUtcDateTime)
            {
              nextUtcDate = new DateTime(currentUtcDateTime.Year + 1, Convert.ToInt32(strArray3[3]), Convert.ToInt32(strArray3[2]), Convert.ToInt32(strArray3[0]), Convert.ToInt32(strArray3[1]), 0, DateTimeKind.Utc);
              break;
            }
            break;
          case TimedEventKinds.Minutely:
            DateTime dateTime = nextUtcDate - TimeSpan.FromSeconds(1.0);
            nextUtcDate = !(this.Schedule != string.Empty) ? dateTime + TimeSpan.FromMinutes(60.0) : dateTime + TimeSpan.FromMinutes(Convert.ToDouble(this.Schedule));
            break;
        }
        return nextUtcDate;
      }

      public override string ToString() => $"{this.Name} ({this.Schedule})";
    }
}
