using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;


namespace Intermech.Calendars
{
    /// <summary>Период рабочего времени</summary>
    [Serializable]
    public class WorkTime : 
      IXmlObjectIPS,
      IWorkTimePeriod,
      IEquatable<WorkTime>,
      IXmlReaderSupport,
      IXmlWriterSupport
    {
      private int _startHours;
      private int _finishHours;
      private int _startMinutes;
      private int _finishMinutes;
      private int _lockCorrectionCounter;

      /// <summary>Часы начала периода</summary>
      public int StartHours
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._startHours;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._startHours = Math.Max(0, Math.Min(23, value));
          this.AfterChange();
        }
      }

      /// <summary>Минуты начала периода</summary>
      public int StartMinutes
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this._startMinutes;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._startMinutes = Math.Max(0, Math.Min(59, value));
          this.AfterChange();
        }
      }

      /// <summary>Часы окончания периода</summary>
      public int FinishHours
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this._finishHours;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._finishHours = Math.Max(0, Math.Min(24, value));
          this.AfterChange();
        }
      }

      /// <summary>Минуты окончания периода</summary>
      public int FinishMinutes
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this._finishMinutes;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._finishMinutes = Math.Max(0, Math.Min(59, value));
          this.AfterChange();
        }
      }

      /// <summary>Продолжительности периода</summary>
      public TimeSpan Duration
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new TimeSpan(this.FinishHours, this.FinishMinutes, 0) - new TimeSpan(this.StartHours, this.StartMinutes, 0);
        }
        set
        {
          TimeSpan timeSpan = new TimeSpan(this.StartHours, this.StartMinutes, 0) + value;
          if (timeSpan.Days > 0)
          {
            this._finishHours = 24;
            this._finishMinutes = 0;
          }
          else
          {
            this._finishHours = timeSpan.Hours;
            this._finishMinutes = timeSpan.Minutes;
          }
          this.AfterChange();
        }
      }

      /// <summary>
      /// Проверка корректности периода
      ///  (прежде всего что время начала меньше времени окончания)
      /// </summary>
      public bool IsCorrect
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new TimeSpan(this.FinishHours, this.FinishMinutes, 0) > new TimeSpan(this.StartHours, this.StartMinutes, 0);
        }
      }

      /// <summary>Заблокировать устранения конфликтов в периоде</summary>
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void LockCorrection() => ++this._lockCorrectionCounter;

      /// <summary>Разблокировать устранения конфликтов в периоде</summary>
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void UnlockCorrection()
      {
        if (this._lockCorrectionCounter <= 0)
          return;
        --this._lockCorrectionCounter;
        if (this._lockCorrectionCounter != 0)
          return;
        this.CorrectPeriods();
      }

      /// <summary>Автоматически вызывается после изменения данных</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private void AfterChange() => this.CorrectPeriods();

      /// <summary>Функция устранения конфликтов в периоде</summary>
      private void CorrectPeriods()
      {
        if (this._lockCorrectionCounter != 0)
          return;
        TimeSpan timeSpan1 = new TimeSpan(this.StartHours, this.StartMinutes, 0);
        TimeSpan timeSpan2 = new TimeSpan(this.FinishHours, this.FinishMinutes, 0);
        TimeSpan timeSpan3 = timeSpan2;
        if (!(timeSpan1 > timeSpan3))
          return;
        this._startHours = timeSpan2.Hours;
        this._startMinutes = timeSpan2.Minutes;
      }

      /// <summary>Установить время начала периода</summary>
      public void SetStartTime(int hour, int minute)
      {
        this._startHours = Math.Max(0, Math.Min(23, hour));
        this._startMinutes = Math.Max(0, Math.Min(59, minute));
        this.AfterChange();
      }

      /// <summary>Установить время начала периода</summary>
      public void SetFinishTime(int hour, int minute)
      {
        this._finishHours = Math.Max(0, Math.Min(24, hour));
        this._finishMinutes = Math.Max(0, Math.Min(hour < 24 ? 59 : 0, minute));
        this.AfterChange();
      }

      /// <summary>Установить время начала периода</summary>
      public void SetStartFinishTime(int startHour, int startMinute, int finishHour, int finishMinute)
      {
        this._startHours = Math.Max(0, Math.Min(23, startHour));
        this._startMinutes = Math.Max(0, Math.Min(59, startMinute));
        this._finishHours = Math.Max(0, Math.Min(24, finishHour));
        this._finishMinutes = Math.Max(0, Math.Min(finishHour < 24 ? 59 : 0, finishMinute));
        this.AfterChange();
      }

      /// <summary>Установить время начала периода</summary>
      public void SetStartFinishTime(int startHour, int finishHour)
      {
        this._startHours = Math.Max(0, Math.Min(23, startHour));
        this._startMinutes = 0;
        this._finishHours = Math.Max(0, Math.Min(24, finishHour));
        this._finishMinutes = 0;
        this.AfterChange();
      }

      /// <summary>Скопировать параметры из другого объекта</summary>
      public void CopyParamsFrom([NotNull] WorkTime workTimePeriod)
      {
        this._startHours = workTimePeriod.StartHours;
        this._startMinutes = workTimePeriod.StartMinutes;
        this._finishHours = workTimePeriod.FinishHours;
        this._finishMinutes = workTimePeriod.FinishMinutes;
        this.AfterChange();
      }

      /// <summary>Конструктор по-умолчанию</summary>
      public WorkTime()
      {
      }

      /// <summary>Конструктор</summary>
      public WorkTime(int startHour, int startMinute, int endHour, int endMinute)
      {
        this.SetStartFinishTime(startHour, startMinute, endHour, endMinute);
      }

      /// <summary>Конструктор</summary>
      public WorkTime(int startHour, int finishHour) => this.SetStartFinishTime(startHour, finishHour);

      public WorkTime([NotNull] XmlReader reader) => this.ReadFromXml(reader);

      /// <summary>Создать копию рабочего дня</summary>
      [NotNull]
      public static WorkTime CreateCopy([NotNull] WorkTime aBase)
      {
        WorkTime copy = new WorkTime();
        copy.CopyParamsFrom(aBase);
        return copy;
      }

      [CanBeNull]
      public static IWorkTimePeriod ConvertToIWorkTimePeriod([CanBeNull] WorkTime workTimePeriod)
      {
        return (IWorkTimePeriod) workTimePeriod;
      }

      /// <summary>Записать поля в XML</summary>
      /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
      /// <param name="xw">XmlWriter</param>
      /// <param name="objectRefId">Генератор идентификаторов</param>
      public void WriteToXml([NotNull] string elementName, [NotNull] XmlWriter xw, [CanBeNull] ObjectIDGenerator objectRefId)
      {
        xw.WriteStartElement(elementName);
        try
        {
          xw.WriteAttributeString("StartHours", this._startHours.ToString());
          xw.WriteAttributeString("StartMinutes", this._startMinutes.ToString());
          xw.WriteAttributeString("FinishHours", this._finishHours.ToString());
          xw.WriteAttributeString("FinishMinutes", this._finishMinutes.ToString());
        }
        finally
        {
          xw.WriteEndElement();
        }
      }

      /// <summary>Загрузить из XML</summary>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      public void ReadFromXml(XmlReadArgsIPS readArgs)
      {
        XmlHelperIPS.ReadFromXml((IXmlObjectIPS) this, readArgs);
      }

      /// <summary>Прочитать одно поле из XML</summary>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      /// <returns>Возвращает true, если поле прочитано</returns>
      public bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
      {
        string localName = readArgs.Reader.LocalName;
        if (!(localName == "StartHours") && !(localName == "StartMinutes") && !(localName == "FinishHours") && !(localName == "FinishMinutes"))
          return false;
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.LoadProperties(readArgs.Reader.LocalName, readArgs.Reader.Value);
        return true;
      }

      [NotNull]
      [NotWhitespace]
      public string XmlNodeName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => nameof (WorkTime);
      }

      /// <summary>Загрузить из XML</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void ReadFromXml([NotNull, NotEmpty] XmlReader reader)
      {
        reader.ReadObject(new XmlReaderExtensions.LoadObjectPropertiesMethod(this.LoadProperties));
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private bool LoadProperties([NotNull, NotWhitespace] string name, [NotNull] string value)
      {
        switch (name)
        {
          case "StartHours":
            this._startHours = Convert.ToInt32(value);
            return true;
          case "StartMinutes":
            this._startMinutes = Convert.ToInt32(value);
            return true;
          case "FinishHours":
            this._finishHours = Convert.ToInt32(value);
            return true;
          case "FinishMinutes":
            this._finishMinutes = Convert.ToInt32(value);
            return true;
          default:
            return false;
        }
      }

      /// <summary>Сохранение состояния в XML</summary>
      public void WriteToXml([NotNull] XmlWriter writer, [CanBeNull] string nodeName = null)
      {
        writer.WriteObject(this.XmlNodeName, ("StartHours", this._startHours.ToString()), ("StartMinutes", this._startMinutes.ToString()), ("FinishHours", this._finishHours.ToString()), ("FinishMinutes", this._finishMinutes.ToString()));
      }

      /// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
      /// <param name="obj">The object to compare with the current object.</param>
      /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
      public override bool Equals([CanBeNull] object obj)
      {
        if (obj == null)
          return false;
        if (this == obj)
          return true;
        return obj is WorkTime other && this.Equals(other);
      }

      /// <summary>Serves as a hash function for a particular type.</summary>
      /// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
      public override int GetHashCode()
      {
        return (this.StartHours, this.StartMinutes, this.FinishHours, this.FinishMinutes).GetHashCode();
      }

      /// <summary>Tests if this WorkTimePeriod is considered equal to another</summary>
      /// <param name="other">The work time period to compare to this object</param>
      /// <returns>true if the objects are considered equal, false if they are not</returns>
      public bool Equals([CanBeNull] WorkTime other)
      {
        if (other == null)
          return false;
        if (this == other)
          return true;
        return this.StartHours == other.StartHours && this.FinishHours == other.FinishHours && this.StartMinutes == other.StartMinutes && this.FinishMinutes == other.FinishMinutes;
      }
    }
}
