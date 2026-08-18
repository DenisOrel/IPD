
// Type: Intermech.Kernel.Search.LC_ConditionParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс хранит параметры поиска в таблице IMS_LCSTART_DATE
    /// </summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<LC_ConditionParams>))]
    [Serializable]
    public class LC_ConditionParams : ICloneable
    {
      /// <summary>Дата перевода с</summary>
      public DateTime BeginDate;
      /// <summary>Дата перевода по</summary>
      public DateTime? EndDate;
      /// <summary>Шаг ЖЦ</summary>
      public int? LCStepID;
      /// <summary>Уровень продвижения</summary>
      public int? LevelID;
      /// <summary>Оператор сравнения для дат</summary>
      public RelationalOperators DateOperator;
      /// <summary>
      /// Количество дней для условия поиска RelationalOperators.LastNDays
      /// </summary>
      public int LastNDays;

      /// <summary>
      /// Все поля, по которым не нужно вести поиск, заполнить нулами. Поиск идёт только по дате - без учета времени.
      /// </summary>
      public LC_ConditionParams(
        int? lcStepID,
        int? levelID,
        DateTime beginDate,
        RelationalOperators dateOperator)
      {
        this.BeginDate = beginDate.Date;
        this.EndDate = new DateTime?();
        this.LCStepID = lcStepID;
        this.LevelID = levelID;
        this.DateOperator = dateOperator;
        this.LastNDays = 0;
      }

      /// <summary>
      /// Все поля, по которым не нужно вести поиск, заполнить нулами. Поиск идёт только по дате - без учета времени.
      /// </summary>
      public LC_ConditionParams(
        int? lcStepID,
        int? levelID,
        DateTime beginDate,
        DateTime? endDate,
        RelationalOperators dateOperator)
      {
        this.BeginDate = beginDate.Date;
        this.EndDate = endDate?.Date;
        this.LCStepID = lcStepID;
        this.LevelID = levelID;
        this.DateOperator = dateOperator;
        this.LastNDays = 0;
      }

      public LC_ConditionParams(int? lcStepID, int? levelID, int lastNDays)
      {
        this.BeginDate = DateTime.Today;
        this.EndDate = new DateTime?();
        this.LCStepID = lcStepID;
        this.LevelID = levelID;
        this.DateOperator = RelationalOperators.LastNDays;
        this.LastNDays = lastNDays;
      }

      public override bool Equals(object obj)
      {
        return obj is LC_ConditionParams lcConditionParams && this.BeginDate.Equals(lcConditionParams.BeginDate) && this.DateOperator.Equals((object) lcConditionParams.DateOperator) && this.LastNDays.Equals(lcConditionParams.LastNDays) && CompareValuesHelper.CompareDateTimeValues((object) this.EndDate, (object) lcConditionParams.EndDate) && CompareValuesHelper.CompareIntValues((object) this.LCStepID, (object) lcConditionParams.LCStepID) && CompareValuesHelper.CompareIntValues((object) this.LevelID, (object) lcConditionParams.LevelID);
      }

      public override int GetHashCode() => base.GetHashCode();

      public object Clone()
      {
        return (object) new LC_ConditionParams(this.LCStepID, this.LevelID, this.BeginDate, this.EndDate, this.DateOperator);
      }
    }
}
