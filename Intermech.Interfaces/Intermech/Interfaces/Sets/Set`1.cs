
// Type: Intermech.Interfaces.Sets.Set`1
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
    /// Класс, позволяющий задать множество непересекающихся диапазонов
    /// </summary>
    [DebuggerDisplay("{DisplayString}")]
    [Serializable]
    public sealed class Set<T> : 
      ISet,
      IAssignable,
      ICloneable,
      IDisplayable,
      ICodedStringFrom,
      IEditableString,
      ISeriesDatesSet
    {
      /// <summary>Пустое множество</summary>
      public static readonly T Empty;
      /// <summary>Сервис, позволяющий создавать экземпляры диапазонов</summary>
      public IRangeFactory<T> Factory;
      /// <summary>Список непересекающихся диапазонов</summary>
      private List<IRange<T>> _ranges = new List<IRange<T>>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public Set()
      {
      }

      /// <summary>
      /// Создать заполненный экземпляр класса (границы класса открыты)
      /// </summary>
      /// <param name="factory">Сервис, позволяющий создавать экземпляры диапазонов</param>
      /// <param name="ranges">Множество диапазонов дат</param>
      public Set(IRangeFactory<T> factory, params IRange<T>[] ranges)
      {
        this.Factory = factory;
        for (int index = 0; index < ranges.Length; ++index)
          this.Add(ranges[index]);
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public Set(object source) => this.Assign(source);

      /// <summary>Очистить поля класса</summary>
      public void Clear() => this._ranges.Clear();

      /// <summary>Очистить поля класса</summary>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case string _:
            this.FromString((string) source, false);
            break;
          case Set<T> set:
            this.Factory = set.Factory;
            this.Assign((object) set.Ranges);
            break;
          case IList<IRange<T>> rangeList:
            this._ranges.Clear();
            for (int index = 0; index < rangeList.Count; ++index)
              this._ranges.Add(rangeList[index].Clone() as IRange<T>);
            this.Normalize();
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Признак применяемости (по датам)</summary>
      public ApplicabilityBy Applicability
      {
        [DebuggerStepThrough] get => ApplicabilityBy.Date;
      }

      /// <summary>Список непересекающихся диапазонов</summary>
      public List<IRange<T>> Ranges
      {
        [DebuggerStepThrough] get => this._ranges;
        set => this.Assign((object) value);
      }

      /// <summary>Отображаемая на экране строка</summary>
      public string DisplayString
      {
        get
        {
          if (this.IsEmpty)
            return "{}";
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < this._ranges.Count; ++index)
          {
            string displayString = this._ranges[index].DisplayString;
            if (stringBuilder.Length > 0 && !string.IsNullOrEmpty(displayString))
              stringBuilder.Append(',');
            stringBuilder.Append(displayString);
          }
          stringBuilder.Insert(0, '{');
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }
      }

      /// <summary>Является ли элемент пустым</summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get
        {
          return this._ranges.Count == 0 || !this._ranges.Exists((Predicate<IRange<T>>) (item => !item.IsEmpty));
        }
      }

      /// <summary>Является ли множество открытым</summary>
      public bool IsOpen
      {
        [DebuggerStepThrough] get => this.IsRightOpen || this.IsLeftOpen;
      }

      /// <summary>Является ли множество открытым слева</summary>
      public bool IsLeftOpen => this._ranges.Exists((Predicate<IRange<T>>) (item => item.IsLeftOpen));

      /// <summary>Является ли множество открытым справа</summary>
      public bool IsRightOpen => this._ranges.Exists((Predicate<IRange<T>>) (item => item.IsRightOpen));

      /// <summary>Проверить на пересечение с указанным множеством</summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - есть пересечение в каком-либо диапазоне</returns>
      public bool IsIntersectsWith(ISet set) => this.IsIntersectsWith(set as Set<T>);

      /// <summary>Проверить на пересечение с указанным множеством</summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - есть пересечение в каком-либо диапазоне</returns>
      public bool IsIntersectsWith(Set<T> set)
      {
        ISeriesDatesSet seriesDatesSet = (ISeriesDatesSet) set;
        return seriesDatesSet != null && this.Applicability == seriesDatesSet.Applicability && (!this.IsEmpty || !seriesDatesSet.IsEmpty) && !this.CanAdd(set);
      }

      /// <summary>
      /// Проверить, можно ли добавить указанное множество во множество.
      /// Условие - диапазоны не должен пересекаться ни с одним диапазоном во множестве
      /// </summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - множество можно добавлять во множество</returns>
      public bool CanAdd(ISet set) => this.CanAdd(set as Set<T>);

      /// <summary>
      /// Добавить указанное множество
      /// При ошибке будет выдано исключение ArithmeticException.
      /// </summary>
      /// <param name="set">Добавляемое множество</param>
      public void Add(ISet set)
      {
        if (!(set is Set<T> set1) || set1.IsEmpty)
          return;
        set1.Ranges.ForEach((Action<IRange<T>>) (range => this.Add(range)));
      }

      /// <summary>Проверить, входит ли указанное число во множество</summary>
      /// <param name="value">Проверяемое число</param>
      /// <returns>true - число входит во множество</returns>
      public bool Contains(T value)
      {
        return !this.IsEmpty && this._ranges.Exists((Predicate<IRange<T>>) (item => item.Contains(value)));
      }

      /// <summary>Упорядочить дочерние элементы</summary>
      public void Normalize() => this._ranges.Sort();

      /// <summary>
      /// Проверить, можно ли добавить указанный диапазон во множество.
      /// Условие - диапазон не должен пересекаться ни с одним диапазоном во множестве
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - диапазон можно добавлять во множество</returns>
      public bool CanAdd(IRange<T> range)
      {
        if (range == null)
          return false;
        return this._ranges.Count == 0 || !this._ranges.Exists((Predicate<IRange<T>>) (item => item.IsIntersect(range)));
      }

      /// <summary>
      /// Проверить, можно ли добавить указанное множество во множество.
      /// Условие - диапазоны не должен пересекаться ни с одним диапазоном во множестве
      /// </summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - множество можно добавлять во множество</returns>
      public bool CanAdd(Set<T> set)
      {
        if (set == null)
          return false;
        return this._ranges.Count == 0 || !this._ranges.Exists((Predicate<IRange<T>>) (item => set.Ranges.Exists((Predicate<IRange<T>>) (itemSet => item.IsIntersect(itemSet)))));
      }

      /// <summary>
      /// Добавить указанный диапазон во множество.
      /// При ошибке будет выдано исключение ArithmeticException.
      /// </summary>
      /// <param name="range">Добавляемый диапазон</param>
      public void Add(IRange<T> range)
      {
        if (!this.CanAdd(range))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_605"), (object) range.DisplayString));
        if (range.IsEmpty)
          return;
        this._ranges.Add(range);
        this._ranges.Sort();
      }

      /// <summary>
      /// Метод позволяет объединить смежные диапазоны внутри множества
      /// </summary>
      public void Compact()
      {
        if (this._ranges.Count < 2)
          return;
        List<IRange<T>> rangeList = new List<IRange<T>>();
        IRange<T> range1 = this._ranges[0];
        for (int index = 1; index < this._ranges.Count; ++index)
        {
          if (range1 == null)
          {
            range1 = this._ranges[index];
          }
          else
          {
            IRange<T> range2 = this._ranges[index];
            if (range1.IsAdjoining(range2) && range1.CanMerge(range2))
            {
              range1 = range1.Merge(range2);
            }
            else
            {
              rangeList.Add(range1);
              range1 = range2;
            }
          }
        }
        if (range1 != null)
          rangeList.Add(range1);
        this._ranges = rangeList;
      }

      /// <summary>
      /// Проверить, можно ли удалить указанный диапазон из множества
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      /// <returns>true - диапазон можно удалить из множества</returns>
      public bool CanDelete(IRange<T> range)
      {
        if (range == null)
          return false;
        return range.IsEmpty || this._ranges.Exists((Predicate<IRange<T>>) (item => item.Contains(range)));
      }

      /// <summary>
      /// Удалить указанный диапазон из множества.
      /// При ошибке будет выдано исключение ArithmeticException или ArgumentNullException
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      public void Delete(IRange<T> range)
      {
        if (range == null)
          throw new ArgumentNullException(nameof (range), LocalizationHolder.rm.GetString("Interfaces_601"));
        if (!this.CanDelete(range))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_606"), (object) range.DisplayString, (object) this.DisplayString));
        List<IRange<T>> result = new List<IRange<T>>();
        List<IRange<T>> toDelete = new List<IRange<T>>();
        this._ranges.ForEach((Action<IRange<T>>) (item =>
        {
          if (!item.Contains(range))
            return;
          item.Delete(range).ForEach((Action<IRange<T>>) (subItem =>
          {
            if (subItem.IsEmpty || result.Contains(subItem))
              return;
            result.Add(subItem);
          }));
          toDelete.Add(item);
        }));
        this._ranges.RemoveAll((Predicate<IRange<T>>) (item => toDelete.Contains(item)));
        result.ForEach((Action<IRange<T>>) (item =>
        {
          if (!this.CanAdd(item))
            return;
          this._ranges.Add(item);
        }));
        this.Normalize();
      }

      /// <summary>
      /// Проверить, можно ли разделить множество на несколько диапазонов с помощью указанного значения
      /// </summary>
      /// <param name="value">Разделитель множества</param>
      /// <returns>true - разделение допустимо</returns>
      public bool CanSplit(T value)
      {
        bool flag = !this.IsEmpty;
        return !flag ? flag : this._ranges.Exists((Predicate<IRange<T>>) (item => item.CanSplit(value)));
      }

      /// <summary>
      /// Разделить множество на несколько диапазонов с помощью указанного значения.
      /// При ошибке будет выдано исключение ArithmeticException либо ArgumentOutOfRangeException
      /// </summary>
      /// <param name="value">Разделитель множества</param>
      public void Split(T value)
      {
        if (!this.CanSplit(value))
          throw new ArgumentOutOfRangeException(nameof (value), string.Format(LocalizationHolder.rm.GetString("Interfaces_607"), (object) value, (object) this.DisplayString));
        List<IRange<T>> rangeList = new List<IRange<T>>();
        List<IRange<T>> toDelete = new List<IRange<T>>();
        for (int index = 0; index < this._ranges.Count; ++index)
        {
          IRange<T> range = this._ranges[index];
          if (range.CanSplit(value))
          {
            List<IRange<T>> collection = range.Split(value);
            if (collection.Count > 0)
            {
              rangeList.AddRange((IEnumerable<IRange<T>>) collection);
              toDelete.Add(range);
            }
          }
        }
        this._ranges.RemoveAll((Predicate<IRange<T>>) (item => toDelete.Contains(item)));
        rangeList.ForEach((Action<IRange<T>>) (item =>
        {
          if (!this.CanAdd(item))
            return;
          this._ranges.Add(item);
        }));
        this.Normalize();
      }

      /// <summary>
      /// Проверить, можно ли разделить множество на два диапазона с помощью указанного значения
      /// </summary>
      /// <param name="value">Разделитель множества</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      /// <returns>true - разделение множества допустимо</returns>
      public bool CanBreak(T value, bool toLeftRange)
      {
        return !this.IsEmpty && this._ranges.Exists((Predicate<IRange<T>>) (item => item.CanBreak(value, toLeftRange)));
      }

      /// <summary>
      /// Разделить множество на два диапазона с помощью указанного значения.
      /// Значение может стать границей левого или правого диапазонов-результатов.
      /// При ошибке генерируются исключения ArithmeticException или ArgumentOutOfRangeException
      /// </summary>
      /// <param name="value">Разделитель множества</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      public void Break(T value, bool toLeftRange)
      {
        if (this.IsEmpty)
          throw new ArithmeticException(LocalizationHolder.rm.GetString("Interfaces_608"));
        if (!this.Contains(value))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_609"), (object) this.DisplayString, (object) value));
        if (!this.CanBreak(value, toLeftRange))
          throw new ArithmeticException(string.Format(LocalizationHolder.rm.GetString("Interfaces_610"), (object) this.DisplayString, (object) value));
        List<IRange<T>> rangeList = new List<IRange<T>>();
        List<IRange<T>> toDelete = new List<IRange<T>>();
        for (int index = 0; index < this._ranges.Count; ++index)
        {
          IRange<T> range = this._ranges[index];
          if (range.CanBreak(value, toLeftRange))
          {
            List<IRange<T>> collection = range.Break(value, toLeftRange);
            if (collection.Count > 0)
            {
              rangeList.AddRange((IEnumerable<IRange<T>>) collection);
              toDelete.Add(range);
            }
          }
        }
        this._ranges.RemoveAll((Predicate<IRange<T>>) (item => toDelete.Contains(item)));
        rangeList.ForEach((Action<IRange<T>>) (item =>
        {
          if (!this.CanAdd(item))
            return;
          this._ranges.Add(item);
        }));
        this.Normalize();
      }

      /// <summary>
      /// Количество диапазонов во множестве - добавлено Гинзбургом
      /// </summary>
      public int Count => this._ranges.Count;

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      public void FromString(string val) => this.FromString(val, false);

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      /// <param name="withLimits">true - в строке хранится граница множеств</param>
      public void FromString(string val, bool withLimits)
      {
        this.Clear();
        if (string.IsNullOrEmpty(val) || this.Factory == null)
          return;
        string[] strArray = val.Split(Consts.Splitter, StringSplitOptions.RemoveEmptyEntries);
        int num = withLimits ? 2 : 0;
        if (strArray == null || strArray.Length == 0 || withLimits && strArray.Length < 2)
          return;
        for (int index = num; index < strArray.Length; ++index)
        {
          IRange<T> range = this.Factory.Create((object) strArray[index]);
          if (!range.IsEmpty && this.CanAdd(range))
            this._ranges.Add(range);
        }
      }

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public override string ToString() => this.ToString(false);

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <param name="withLimits">true - записывать в строку границы</param>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public string ToString(bool withLimits)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.IsEmpty)
          return string.Empty;
        if (withLimits)
        {
          stringBuilder.Append("-∞");
          stringBuilder.Append(',');
          stringBuilder.Append("∞");
          if (this._ranges.Count == 0)
            stringBuilder.Append(',');
        }
        for (int index = 0; index < this._ranges.Count; ++index)
        {
          string str = ((ICodedStringFrom) this._ranges[index]).ToString();
          if (!string.IsNullOrEmpty(str))
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(',');
            stringBuilder.Append(str);
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Содержимое в виде строки, удобном для редактирования пользователем
      /// </summary>
      public string AsEditableString
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder();
          if (this.IsEmpty)
            return string.Empty;
          for (int index = 0; index < this._ranges.Count; ++index)
          {
            string asEditableString = this._ranges[index].AsEditableString;
            if (!string.IsNullOrEmpty(asEditableString))
            {
              if (stringBuilder.Length > 0)
                stringBuilder.Append(',');
              stringBuilder.Append(asEditableString);
            }
          }
          return stringBuilder.ToString();
        }
        set
        {
          this.Clear();
          if (string.IsNullOrEmpty(value) || this.Factory == null)
            return;
          string[] strArray = value.Split(Consts.Splitter, StringSplitOptions.RemoveEmptyEntries);
          if (strArray == null || strArray.Length == 0)
            return;
          for (int index = 0; index < strArray.Length; ++index)
          {
            IRange<T> range = this.Factory.Create();
            range.AsEditableString = strArray[index];
            if (!range.IsEmpty && !range.IsOpen && this.CanAdd(range))
              this._ranges.Add(range);
          }
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты идентичны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is Set<T> set && ObjectsCompareHelper.CompareLists<IRange<T>>((IList<IRange<T>>) this.Ranges, (IList<IRange<T>>) set.Ranges);
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        int result = base.GetHashCode();
        if (this.Ranges != null && this.Ranges.Count > 0)
          this.Ranges.ForEach((Action<IRange<T>>) (item => result ^= item.GetHashCode()));
        return result;
      }
    }
}
