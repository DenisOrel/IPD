
// Type: Intermech.Interfaces.Sets.DateTimeRange
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Класс, позволяющий задать открытый или закрытый диапазон дат
    /// </summary>
    [DebuggerDisplay("{DisplayString}")]
    [Serializable]
    public sealed class DateTimeRange : 
      IAssignable,
      ICloneable,
      IComparable,
      IComparable<IRange<DateTime>>,
      IRange<DateTime>,
      IDisplayable,
      ICodedStringFrom,
      IEditableString
    {
      /// <summary>Пустой диапазон</summary>
      public static readonly DateTimeRange Empty = new DateTimeRange();
      /// <summary>Начало диапазона</summary>
      private DateTime _minValue = Consts.dateMinusInfinity;
      /// <summary>Конец диапазона</summary>
      private DateTime _maxValue = Consts.dateMinusInfinity;

      /// <summary>Создать пустой экземпляр класса</summary>
      public DateTimeRange()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public DateTimeRange(object source) => this.Assign(source);

      /// <summary>Создать диапазон значений, открытый слева или справа</summary>
      /// <param name="value">Ограничивающее значение в диапазоне</param>
      /// <param name="leftOpen">true - диапазон будет открыт слева, иначе - справа</param>
      public DateTimeRange(DateTime value, bool leftOpen)
      {
        if (leftOpen)
        {
          this._minValue = Consts.dateMinusInfinity;
          this._maxValue = value;
        }
        else
        {
          this._minValue = value;
          this._maxValue = Consts.datePlusInfinity;
        }
      }

      /// <summary>Создать новый диапазон значений</summary>
      /// <param name="minValue">Начало диапазона</param>
      /// <param name="maxValue">Конец диапазона</param>
      public DateTimeRange(DateTime minValue, DateTime maxValue)
      {
        this._minValue = this.Min(minValue, maxValue);
        this._maxValue = this.Max(minValue, maxValue);
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._minValue = Consts.dateMinusInfinity;
        this._maxValue = Consts.datePlusInfinity;
      }

      /// <summary>Очистить поля класса</summary>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case string _:
            this.FromString((string) source);
            break;
          case DateTimeRange dateTimeRange:
            this._minValue = dateTimeRange.MinValue;
            this._maxValue = dateTimeRange.MaxValue;
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IRange<DateTime>);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IRange<DateTime> other)
      {
        if (other == null)
          return 1;
        if (this == other)
          return 0;
        DateTime dateTime = this.MinValue;
        int num = dateTime.CompareTo(other.MinValue);
        if (num != 0)
          return num;
        dateTime = this.MaxValue;
        return dateTime.CompareTo(other.MaxValue);
      }

      /// <summary>Является ли диапазон пустым</summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get
        {
          if (this._minValue == Consts.dateMinusInfinity && this._maxValue == Consts.dateMinusInfinity)
            return true;
          return this._minValue == Consts.datePlusInfinity && this._maxValue == Consts.datePlusInfinity;
        }
      }

      /// <summary>Является ли диапазон бесконечным с обеих сторон</summary>
      public bool IsOpen
      {
        [DebuggerStepThrough] get
        {
          return this._minValue == Consts.dateMinusInfinity && this._maxValue == Consts.datePlusInfinity;
        }
      }

      /// <summary>Упорядочить дочерние элементы</summary>
      public void Normalize()
      {
        if (this.IsEmpty)
          return;
        DateTime dateTime1 = this.Min(this._minValue, this._maxValue);
        DateTime dateTime2 = this.Max(this._minValue, this._maxValue);
        this._minValue = dateTime1;
        this._maxValue = dateTime2;
      }

      /// <summary>
      /// Является ли диапазон открытым слева
      /// (диапазон не должен быть пустым)
      /// </summary>
      public bool IsLeftOpen
      {
        [DebuggerStepThrough] get
        {
          return this._minValue == Consts.dateMinusInfinity && this._maxValue != Consts.dateMinusInfinity;
        }
      }

      /// <summary>
      /// Является ли диапазон открытым справа
      /// (диапазон не должен быть пустым)
      /// </summary>
      public bool IsRightOpen
      {
        [DebuggerStepThrough] get
        {
          return this._maxValue == Consts.datePlusInfinity && this._minValue != Consts.datePlusInfinity;
        }
      }

      /// <summary>
      /// Начальная граница диапазона (исключение ArithmeticException, если диапазон пустой)
      /// </summary>
      public DateTime MinValue
      {
        [DebuggerStepThrough] get
        {
          if (this.IsEmpty)
            throw new ArithmeticException(LocalizationHolder.rm.GetString("Interfaces_587"));
          return this._minValue;
        }
        set
        {
          this._minValue = !(value > this._maxValue) ? value.Date : throw new ArgumentOutOfRangeException(nameof (MinValue), LocalizationHolder.rm.GetString("Interfaces_588"));
        }
      }

      /// <summary>
      /// Конечная граница диапазона (исключение ArithmeticException, если диапазон пустой, либо ArgumentOutOfRangeException)
      /// </summary>
      public DateTime MaxValue
      {
        [DebuggerStepThrough] get
        {
          if (this.IsEmpty)
            throw new ArithmeticException(LocalizationHolder.rm.GetString("Interfaces_589"));
          return this._maxValue;
        }
        set
        {
          this._maxValue = !(value < this._minValue) ? value.Date : throw new ArgumentOutOfRangeException(nameof (MaxValue), LocalizationHolder.rm.GetString("Interfaces_590"));
        }
      }

      /// <summary>Проверить, входит ли указанное число в диапазон</summary>
      /// <param name="value">Проверяемое число</param>
      /// <returns>true - число входит в диапазон</returns>
      public bool Contains(DateTime value)
      {
        return !this.IsEmpty && value >= this._minValue && value <= this._maxValue;
      }

      /// <summary>
      /// Проверить, содержит ли текущий диапазон все значения из указанного диапазона
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон целиком входит в текущий диапазон, исключение ArgumentNullException - при ошибке</returns>
      public bool Contains(IRange<DateTime> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_592"));
        if (this.IsEmpty && !range.IsEmpty)
          return false;
        if (!this.IsEmpty && range.IsEmpty)
          return true;
        return range.MinValue >= this.MinValue && range.MaxValue <= this.MaxValue;
      }

      /// <summary>
      /// Проверить, пересекает ли проверяемый диапазон текущий любой из своих границ
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон одной из границ пересекает текущий диапазон, исключение ArgumentNullException - при ошибке</returns>
      public bool IsIntersect(IRange<DateTime> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_592"));
        return !this.IsEmpty && !range.IsEmpty && range.MinValue <= this.MaxValue && range.MaxValue >= this.MinValue;
      }

      /// <summary>
      /// Проверить, прилегает ли проверяемый диапазон к текущему
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон прилегает к текущему диапазону одной из границ, исключение ArgumentNullException - при ошибке</returns>
      public bool IsAdjoining(IRange<DateTime> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_593"));
        if (this.IsEmpty || range.IsEmpty)
          return false;
        return this.MinValue == this.Prev(range.MaxValue) || this.MaxValue == this.Prev(range.MinValue);
      }

      /// <summary>
      /// Проверить, возможно ли объединение с указанным диапазоном
      /// </summary>
      /// <param name="range">Диапазон, с которым требуется проверить возможность объединения</param>
      /// <returns>true - данные диапазоны можно объединять</returns>
      public bool CanMerge(IRange<DateTime> range)
      {
        return range != null && (!this.IsEmpty || !range.IsEmpty) && (!this.IsLeftOpen || !range.IsRightOpen) && (!this.IsRightOpen || !range.IsLeftOpen) && (this.IsIntersect(range) || this.IsAdjoining(range));
      }

      /// <summary>
      /// Проверить, возможно ли объединение с указанными диапазонами
      /// </summary>
      /// <param name="ranges">Диапазоны, с которыми требуется проверить возможность объединения</param>
      /// <returns>true - данные диапазоны можно объединять</returns>
      public bool CanMerge(IList<IRange<DateTime>> ranges)
      {
        if (ranges == null)
          return false;
        IRange<DateTime> range = (IRange<DateTime>) new DateTimeRange((object) this);
        for (int index = 0; index < ranges.Count; ++index)
        {
          if (!range.CanMerge(ranges[index]))
            return false;
          range = range.Merge(ranges[index]);
        }
        return true;
      }

      /// <summary>
      /// Выполнить объединение с указанным диапазоном.
      /// Ограничение: диапазоны должны быть закрытыми, либо открыты с одной стороны
      /// </summary>
      /// <param name="range">Диапазон, с которым требуется выполнить объединение</param>
      /// <returns>Объединённый диапазон (исключения ArgumentNullException, ArithmeticException при ошибках)</returns>
      public IRange<DateTime> Merge(IRange<DateTime> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_594"));
        if (this.IsEmpty)
          return range;
        if (range.IsEmpty)
          return (IRange<DateTime>) this;
        if (this.IsEmpty && range.IsEmpty)
          return (IRange<DateTime>) DateTimeRange.Empty;
        if (this.IsLeftOpen && range.IsRightOpen || this.IsRightOpen && range.IsLeftOpen)
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_595"), (object) this.DisplayString, (object) range.DisplayString));
        if (!this.IsIntersect(range) && !this.IsAdjoining(range))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_596"), (object) this.DisplayString, (object) range.DisplayString));
        return (IRange<DateTime>) new DateTimeRange(this.Min(this._minValue, range.MinValue), this.Max(this._maxValue, range.MaxValue));
      }

      /// <summary>
      /// Выполнить объединение с указанными диапазонами.
      /// Ограничение: диапазоны должны быть закрытыми, либо открыты с одной стороны
      /// </summary>
      /// <param name="ranges">Диапазоны, с которыми требуется выполнить объединение</param>
      /// <returns>Объединённый диапазон (исключение ArgumentNullException при ошибках)</returns>
      public IRange<DateTime> Merge(IList<IRange<DateTime>> ranges)
      {
        if (ranges == null)
          throw new ArgumentNullException(nameof (ranges), LocalizationHolder.rm.GetString("Interfaces_597"));
        IRange<DateTime> range = (IRange<DateTime>) new DateTimeRange((object) this);
        for (int index = 0; index < ranges.Count; ++index)
          range = range.Merge(ranges[index]);
        return range;
      }

      /// <summary>
      /// Проверить, можно ли разделить диапазон на два диапазона с помощью указанного значения
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      /// <returns>true - разделение диапазона допустимо</returns>
      public bool CanBreak(DateTime value, bool toLeftRange)
      {
        return !this.IsEmpty && this.Contains(value) && (!(this._minValue == value) || !(this._maxValue == value));
      }

      /// <summary>
      /// Разделить диапазон на два диапазона с помощью указанного значения.
      /// Значение может стать границей левого или правого диапазонов-результатов
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      /// <returns>Два диапазона-результата (левый, затем правый диапазоны), или исключение ArithmeticException при ошибке</returns>
      public List<IRange<DateTime>> Break(DateTime value, bool toLeftRange)
      {
        if (this.IsEmpty)
          throw new ArithmeticException(LocalizationHolder.rm.GetString("Interfaces_598"));
        if (!this.Contains(value))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_599"), (object) this.DisplayString, (object) value));
        if (this._minValue == value && this._maxValue == value)
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_600"), (object) this.DisplayString, (object) value));
        DateTimeRange dateTimeRange1 = new DateTimeRange(this._minValue, toLeftRange ? value : this.Max(this.Prev(value), this._minValue));
        DateTimeRange dateTimeRange2 = new DateTimeRange(toLeftRange ? this.Min(this.Next(value), this._maxValue) : value, this._maxValue);
        List<IRange<DateTime>> rangeList = new List<IRange<DateTime>>(2);
        if (toLeftRange && value == this._maxValue)
          rangeList.Add((IRange<DateTime>) dateTimeRange1);
        else if (!toLeftRange && value == this._minValue)
        {
          rangeList.Add((IRange<DateTime>) dateTimeRange2);
        }
        else
        {
          rangeList.Add((IRange<DateTime>) dateTimeRange1);
          rangeList.Add((IRange<DateTime>) dateTimeRange2);
        }
        return rangeList;
      }

      /// <summary>
      /// Проверить, можно ли разделить диапазон на несколько диапазонов с помощью указанного значения.
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <returns>true - есть возможность разделить диапазон</returns>
      public bool CanSplit(DateTime value) => !this.IsEmpty && this.Contains(value);

      /// <summary>
      /// Разделить диапазон на несколько диапазонов с помощью указанного значения.
      /// Данное значение будет представлено в виде отдельного диапазона.
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <returns>Один или несколько диапазонов, которые в сумме дают исходный диапазон и указанное значение,
      /// или исключение ArithmeticException при ошибке</returns>
      public List<IRange<DateTime>> Split(DateTime value)
      {
        if (this.IsEmpty)
          throw new ArithmeticException(LocalizationHolder.rm.GetString("Interfaces_598"));
        if (!this.Contains(value))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_599"), (object) this.DisplayString, (object) value));
        List<IRange<DateTime>> rangeList = new List<IRange<DateTime>>(2);
        if (this._minValue == value && this._maxValue == value)
        {
          rangeList.Add((IRange<DateTime>) new DateTimeRange((object) this));
          return rangeList;
        }
        if (this.MinValue == value)
        {
          if (this.MinValue != Consts.dateMinusInfinity)
          {
            rangeList.Add((IRange<DateTime>) new DateTimeRange(value, value));
            rangeList.Add((IRange<DateTime>) new DateTimeRange(this.Next(this.MinValue), this.MaxValue));
          }
          else
            rangeList.Add((IRange<DateTime>) (this.Clone() as DateTimeRange));
        }
        else if (this.MaxValue == value)
        {
          if (this.MaxValue != Consts.datePlusInfinity)
          {
            rangeList.Add((IRange<DateTime>) new DateTimeRange(this.MinValue, this.Prev(this.MaxValue)));
            rangeList.Add((IRange<DateTime>) new DateTimeRange(value, value));
          }
          else
            rangeList.Add((IRange<DateTime>) (this.Clone() as DateTimeRange));
        }
        else
        {
          rangeList.Add((IRange<DateTime>) new DateTimeRange(this.MinValue, this.Prev(value)));
          rangeList.Add((IRange<DateTime>) new DateTimeRange(value, value));
          rangeList.Add((IRange<DateTime>) new DateTimeRange(this.Next(value), this.MaxValue));
        }
        return rangeList;
      }

      /// <summary>
      /// Проверить, можно ли удалить указанный диапазон из текущего диапазона.
      /// Ограничения - диапазон должен пересекаться с текущим диапазоном
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      /// <returns>true - удаление разрешено</returns>
      public bool CanDelete(IRange<DateTime> range) => range != null && this.IsIntersect(range);

      /// <summary>
      /// Удалить указанный диапазон из текущего диапазона.
      /// Ограничения - диапазон должен пересекаться с текущим диапазоном
      /// При ошибках будет выдано одно из исключений - ArithmeticException или ArgumentNullException
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      /// <returns>Один или несколько диапазонов, получившихся в результате удаления</returns>
      public List<IRange<DateTime>> Delete(IRange<DateTime> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_601"));
        if (!this.CanDelete(range))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_602"), (object) range.DisplayString, (object) this.DisplayString));
        List<IRange<DateTime>> rangeList = new List<IRange<DateTime>>();
        if (range.Contains((IRange<DateTime>) this))
        {
          rangeList.Add((IRange<DateTime>) DateTimeRange.Empty);
          return rangeList;
        }
        if (range.MinValue > this.MinValue && range.MaxValue < this.MaxValue)
        {
          rangeList.Add((IRange<DateTime>) new DateTimeRange(this.MinValue, this.Prev(range.MinValue)));
          rangeList.Add((IRange<DateTime>) new DateTimeRange(this.Next(range.MaxValue), this.MaxValue));
          return rangeList;
        }
        if (range.MaxValue < this.MaxValue)
        {
          rangeList.Add((IRange<DateTime>) new DateTimeRange(this.Next(range.MaxValue), this.MaxValue));
          return rangeList;
        }
        if (!(range.MaxValue >= this.MaxValue))
          return rangeList;
        rangeList.Add((IRange<DateTime>) new DateTimeRange(this.MinValue, this.Prev(range.MinValue)));
        return rangeList;
      }

      /// <summary>Предыдущее значение</summary>
      /// <param name="value">Изменяемое значение</param>
      /// <returns>Предыдущее значение</returns>
      public DateTime Prev(DateTime value) => value - new TimeSpan(1, 0, 0, 0);

      /// <summary>Следующее значение</summary>
      /// <param name="value">Изменяемое значение</param>
      /// <returns>Следующее значение</returns>
      public DateTime Next(DateTime value) => value + new TimeSpan(1, 0, 0, 0);

      /// <summary>Найти минимальное значение</summary>
      /// <param name="value1">Первое значение</param>
      /// <param name="value2">Второе значение</param>
      /// <returns></returns>
      public DateTime Min(DateTime value1, DateTime value2) => value1 < value2 ? value1 : value2;

      /// <summary>Найти максимальное значение</summary>
      /// <param name="value1">Первое значение</param>
      /// <param name="value2">Второе значение</param>
      /// <returns></returns>
      public DateTime Max(DateTime value1, DateTime value2) => value1 > value2 ? value1 : value2;

      /// <summary>Отображаемая на экране строка</summary>
      public string DisplayString
      {
        get
        {
          if (this.IsEmpty)
            return "[]";
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append('[');
          if (this.IsLeftOpen)
            stringBuilder.Append("-∞");
          else
            stringBuilder.Append(StringsHelper.DateToStr(this._minValue));
          if (this._minValue != this._maxValue)
          {
            stringBuilder.Append("..");
            if (this.MaxValue == Consts.datePlusInfinity)
              stringBuilder.Append("∞");
            else
              stringBuilder.Append(StringsHelper.DateToStr(this._maxValue));
          }
          stringBuilder.Append(']');
          return stringBuilder.ToString();
        }
      }

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      public void FromString(string val)
      {
        this.Clear();
        if (string.IsNullOrEmpty(val))
          return;
        string[] strArray = val.Split(Consts.RangeSplitter, StringSplitOptions.None);
        if (strArray == null || strArray.Length == 0)
          return;
        if (strArray.Length == 1)
        {
          this._minValue = string.IsNullOrEmpty(strArray[0]) || strArray[0] == "-∞" ? Consts.dateMinusInfinity : StringsHelper.DateFromStrHex(strArray[0]);
          this._maxValue = string.IsNullOrEmpty(strArray[0]) || strArray[0] == "∞" ? Consts.datePlusInfinity : StringsHelper.DateFromStrHex(strArray[0]);
        }
        else
        {
          this._minValue = string.IsNullOrEmpty(strArray[0]) || strArray[0] == "-∞" ? Consts.dateMinusInfinity : StringsHelper.DateFromStrHex(strArray[0]);
          this._maxValue = string.IsNullOrEmpty(strArray[1]) || strArray[1] == "∞" ? Consts.datePlusInfinity : StringsHelper.DateFromStrHex(strArray[1]);
        }
        this.Normalize();
      }

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      /// <param name="withLimits">true - в строке хранится граница множеств</param>
      public void FromString(string val, bool withLimits) => this.FromString(val);

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.IsEmpty)
          return string.Empty;
        if (!this.IsLeftOpen)
          stringBuilder.Append(StringsHelper.DateToStrHex(this._minValue));
        if (this._minValue != this._maxValue)
        {
          stringBuilder.Append(':');
          if (!(this.MaxValue == Consts.datePlusInfinity))
            stringBuilder.Append(StringsHelper.DateToStrHex(this._maxValue));
        }
        return stringBuilder.ToString();
      }

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <param name="withLimits">true - записывать в строку границы</param>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public string ToString(bool withLimits) => this.ToString();

      /// <summary>
      /// Содержимое в виде строки, удобном для редактирования пользователем
      /// </summary>
      public string AsEditableString
      {
        get
        {
          if (this.IsEmpty || this.IsOpen)
            return string.Empty;
          StringBuilder stringBuilder = new StringBuilder();
          if (!this.IsLeftOpen)
            stringBuilder.Append(StringsHelper.DateToEditableStr(this._minValue));
          if (this._minValue != this._maxValue)
          {
            stringBuilder.Append("..");
            if (!this.IsRightOpen)
              stringBuilder.Append(StringsHelper.DateToEditableStr(this._maxValue));
          }
          return stringBuilder.ToString();
        }
        set
        {
          this.Clear();
          if (string.IsNullOrEmpty(value))
            return;
          string[] strArray = value.Split(Consts.RangeSplitterDots, StringSplitOptions.None);
          if (strArray == null || strArray.Length == 0)
            return;
          if (strArray.Length == 1)
          {
            this._minValue = !(strArray[0] == "-∞") ? StringsHelper.DateFromEditableStr(strArray[0], Consts.dateMinusInfinity) : Consts.dateMinusInfinity;
            this._maxValue = !(strArray[0] == "∞") ? StringsHelper.DateFromEditableStr(strArray[0], Consts.datePlusInfinity) : Consts.datePlusInfinity;
          }
          else
          {
            this._minValue = !(strArray[0] == "-∞") ? StringsHelper.DateFromEditableStr(strArray[0], Consts.dateMinusInfinity) : Consts.dateMinusInfinity;
            this._maxValue = !(strArray[1] == "∞") ? StringsHelper.DateFromEditableStr(strArray[1], Consts.datePlusInfinity) : Consts.datePlusInfinity;
          }
          this.Normalize();
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты идентичны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        IRange<DateTime> range = obj as IRange<DateTime>;
        return this.MinValue == range.MinValue && this.MaxValue == range.MaxValue;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        DateTime dateTime = this.MaxValue;
        int num = dateTime.GetHashCode() << 16 /*0x10*/;
        dateTime = this.MinValue;
        int hashCode = dateTime.GetHashCode();
        return num ^ hashCode;
      }
    }
}
