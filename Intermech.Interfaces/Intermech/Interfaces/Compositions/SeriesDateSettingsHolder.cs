
// Type: Intermech.Interfaces.Compositions.SeriesDateSettingsHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Контейнер настроек фильтрации составов по сериям изделий и датам выпуска/действия
    /// </summary>
    [Serializable]
    public sealed class SeriesDateSettingsHolder : ICloneable, IAssignable
    {
      /// <summary>Разрешена ли фильтрация</summary>
      private bool _enabled;
      /// <summary>
      /// Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)
      /// </summary>
      private long _masterArticle;
      /// <summary>
      /// Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue
      /// </summary>
      private DateTime _date = DateTime.MinValue;
      /// <summary>
      /// Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue
      /// </summary>
      private int _series = int.MinValue;

      /// <summary>Создать пустой экземпляр класса</summary>
      public SeriesDateSettingsHolder()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="enabled">Разрешена ли фильтрация</param>
      /// <param name="masterArticle">Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)</param>
      /// <param name="date">Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue</param>
      /// <param name="series">Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue</param>
      public SeriesDateSettingsHolder(bool enabled, long masterArticle, DateTime date, int series)
      {
        this._enabled = enabled;
        this._masterArticle = masterArticle;
        this._date = date;
        this._series = series;
      }

      /// <summary>
      /// Создать и заполнить экземпляр класса из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public SeriesDateSettingsHolder(object source) => this.Assign(source);

      /// <summary>Разрешена ли фильтрация</summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get
        {
          lock (this)
            return this._enabled;
        }
        set
        {
          lock (this)
            this._enabled = value;
        }
      }

      /// <summary>
      /// Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)
      /// </summary>
      public long MasterArticle
      {
        [DebuggerStepThrough] get
        {
          lock (this)
            return this._masterArticle;
        }
        set
        {
          lock (this)
            this._masterArticle = value;
        }
      }

      /// <summary>
      /// Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue
      /// </summary>
      public DateTime Date
      {
        [DebuggerStepThrough] get
        {
          lock (this)
            return this._date;
        }
        set
        {
          lock (this)
            this._date = value;
        }
      }

      /// <summary>
      /// Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue
      /// </summary>
      public int Series
      {
        [DebuggerStepThrough] get
        {
          lock (this)
            return this._series;
        }
        set
        {
          lock (this)
            this._series = value;
        }
      }

      /// <summary>Является ли контейнер пустым</summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get
        {
          lock (this)
            return this._masterArticle == 0L && this._date == DateTime.MinValue && this._series == int.MinValue;
        }
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        lock (this)
        {
          this._enabled = false;
          this._masterArticle = 0L;
          this._series = int.MinValue;
          this._date = DateTime.MinValue;
        }
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is SeriesDateSettingsHolder dateSettingsHolder))
          return;
        lock (this)
        {
          this._enabled = dateSettingsHolder.Enabled;
          this._masterArticle = dateSettingsHolder.MasterArticle;
          this._series = dateSettingsHolder.Series;
          this._date = dateSettingsHolder.Date;
        }
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);
    }
}
