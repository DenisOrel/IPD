
// Type: Intermech.Interfaces.Compositions.SeriesDatesApplicability
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Sets;
using System;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс, хранящий применяемость в указанном головном изделии по сериям и датам
    /// </summary>
    [DebuggerDisplay("{DisplayString}")]
    [Serializable]
    public sealed class SeriesDatesApplicability : 
      IAssignable,
      ICloneable,
      IVersionApplicabilities,
      IEditableString,
      IComparable<SeriesDatesApplicability>
    {
      /// <summary>Признак применяемости (в сериях или по датам)</summary>
      public ApplicabilityBy Applicability;
      /// <summary>Идентификатор версии головного изделия</summary>
      public long MainObjectID;
      /// <summary>
      /// Диапазон множеств серий/дат для указанного головного изделия
      /// </summary>
      public ISet Set;
      /// <summary>Аннулированная версия</summary>
      public bool Annuled;

      /// <summary>Создать пустой экземпляр класса</summary>
      public SeriesDatesApplicability()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="applicability">Признак применяемости</param>
      /// <param name="mainObjectID">Идентификатор версии головного изделия</param>
      /// <param name="set">Диапазон множеств серий/дат для указанного головного изделия</param>
      /// <param name="annuled">Аннулированная версия</param>
      public SeriesDatesApplicability(
        ApplicabilityBy applicability = ApplicabilityBy.Series,
        long mainObjectID = 0,
        ISet set = null,
        bool annuled = false)
      {
        this.Applicability = applicability;
        this.MainObjectID = mainObjectID;
        this.Set = set;
        this.Annuled = annuled;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public SeriesDatesApplicability(object source) => this.Assign(source);

      /// <summary>Является ли коллекция пустой</summary>
      public bool IsEmpty => this.Set == null || this.Set.IsEmpty;

      /// <summary>Отображаемая на экране строка</summary>
      public string DisplayString
      {
        get
        {
          IObjectsInfoCache service = ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) as IObjectsInfoCache;
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append('[');
          stringBuilder.Append(service != null ? service.GetObjectCaption(this.MainObjectID) : Math.Abs(this.MainObjectID).ToString());
          stringBuilder.Append(']');
          stringBuilder.Append("[");
          stringBuilder.Append(EnumDescConverter.GetEnumDescription((Enum) this.Applicability));
          stringBuilder.Append("] ");
          if (this.Set != null)
          {
            stringBuilder.Append('{');
            stringBuilder.Append(this.Set.DisplayString);
            stringBuilder.Append('}');
          }
          return stringBuilder.ToString();
        }
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.Applicability = ApplicabilityBy.Series;
        this.MainObjectID = 0L;
        this.Set = (ISet) null;
        this.Annuled = false;
      }

      /// <summary>Очистить поля класса</summary>
      public void Assign(object source)
      {
        if (this == source)
          return;
        switch (source)
        {
          case string _:
            this.FromString((string) source);
            break;
          case SeriesDatesApplicability datesApplicability:
            this.Applicability = datesApplicability.Applicability;
            this.MainObjectID = datesApplicability.MainObjectID;
            this.Set = datesApplicability.Set != null ? datesApplicability.Set.Clone() as ISet : (ISet) null;
            this.Annuled = datesApplicability.Annuled;
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new SeriesDatesApplicability((object) this.ToString());

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты идентичны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is SeriesDatesApplicability datesApplicability && this.Applicability == datesApplicability.Applicability && this.MainObjectID == datesApplicability.MainObjectID && ObjectsCompareHelper.CompareObjects((object) this.Set, (object) datesApplicability.Set);
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return (int) this.Applicability << 30 ^ this.MainObjectID.GetHashCode();
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
        string[] strArray = val.Split(Intermech.Interfaces.Sets.Consts.SplitterMain, StringSplitOptions.None);
        if (strArray == null || strArray.Length < 6)
          return;
        this.Applicability = (ApplicabilityBy) StringsHelper.HexToInt32(strArray[0]);
        this.MainObjectID = StringsHelper.HexToInt64(strArray[1]);
        this.Set = this.Applicability == ApplicabilityBy.Series ? (ISet) new Intermech.Interfaces.Sets.Set<int>((IRangeFactory<int>) Int32RangeFactory.Factory, Array.Empty<IRange<int>>()) : (ISet) new Intermech.Interfaces.Sets.Set<DateTime>((IRangeFactory<DateTime>) DateTimeRangeFactory.Factory, Array.Empty<IRange<DateTime>>());
        this.Set.Assign((object) strArray[2]);
        this.Annuled = strArray[3] == "1";
      }

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(StringsHelper.IntToHex((int) this.Applicability));
        stringBuilder.Append('|');
        stringBuilder.Append(StringsHelper.IntToHex(this.MainObjectID));
        stringBuilder.Append('|');
        stringBuilder.Append(this.Set != null ? this.Set.ToString(false) : string.Empty);
        stringBuilder.Append('|');
        stringBuilder.Append(this.Annuled ? "1" : "");
        stringBuilder.Append('|');
        stringBuilder.Append('|');
        stringBuilder.Append('|');
        return stringBuilder.ToString();
      }

      /// <summary>Проверить серии на наличие ошибочных значений</summary>
      /// <returns>true - проверка выполнена успешно, ошибок нет</returns>
      public bool CheckSeries()
      {
        if (this.Applicability != ApplicabilityBy.Series)
          return true;
        if (!(this.Set is Intermech.Interfaces.Sets.Set<int> set))
          return false;
        return set.IsEmpty || !set.Ranges.Exists((Predicate<IRange<int>>) (range =>
        {
          if (range.IsEmpty)
            return false;
          return range.MinValue < 1 || range.MaxValue < 1 || range.MinValue > range.MaxValue;
        }));
      }

      /// <summary>
      /// Проверить на пересечение с указанной коллекцией диапазонов
      /// </summary>
      /// <param name="source">Проверяемая коллекция диапазонов</param>
      /// <returns>true, если коллекции имеют одинаковый признак применяемости (серии/даты) и есть пересечение с каким-либо диапазоном</returns>
      public bool IsIntersectsWith(SeriesDatesApplicability source)
      {
        return source != null && this.Applicability == source.Applicability && (!this.IsEmpty || !source.IsEmpty) && this.Set != null && source.Set != null && this.Set.IsIntersectsWith(source.Set);
      }

      /// <summary>
      /// Проверить, можно ли добавить указанную коллекцию в коллекцию.
      /// Условие - диапазоны исходной коллекции не должны пересекаться ни с одним диапазоном в текущей коллекции
      /// </summary>
      /// <param name="source">Проверяемая коллекция</param>
      /// <returns>true - исходную коллекцию можно добавлять в текущую коллекцию</returns>
      public bool CanAdd(SeriesDatesApplicability source)
      {
        return source != null && this.Applicability == source.Applicability && (!this.IsEmpty || !source.IsEmpty) && source.Set != null && this.Set.CanAdd(source.Set);
      }

      /// <summary>
      /// Добавить указанную коллекцию
      /// При ошибке будет выдано исключение ArithmeticException.
      /// </summary>
      /// <param name="source">Добавляемая коллекция</param>
      public void Add(SeriesDatesApplicability source)
      {
        if (source == null || this.Applicability != source.Applicability || this.IsEmpty && source.IsEmpty || source.Set == null)
          return;
        this.Set = this.Set ?? (this.Applicability == ApplicabilityBy.Series ? (ISet) new Intermech.Interfaces.Sets.Set<int>((IRangeFactory<int>) Int32RangeFactory.Factory, Array.Empty<IRange<int>>()) : (ISet) new Intermech.Interfaces.Sets.Set<DateTime>((IRangeFactory<DateTime>) DateTimeRangeFactory.Factory, Array.Empty<IRange<DateTime>>()));
        if (!this.Set.CanAdd(source.Set))
          return;
        this.Set.Add(source.Set);
      }

      /// <summary>
      /// Выполнить проверку применяемости указанной версии по дате и(или) номеру серии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <param name="masterArticle">Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)</param>
      /// <param name="date">Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue</param>
      /// <param name="series">Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue</param>
      /// <returns>Статус указанной версии</returns>
      public ObjectFiltrationState CheckApplicabilities(
        IUserSession session,
        long objectID,
        long masterArticle,
        DateTime date,
        int series)
      {
        if (objectID == 0L || Math.Abs(masterArticle) != Math.Abs(this.MainObjectID))
          return ObjectFiltrationState.fsVariance;
        if (this.Applicability == ApplicabilityBy.Series && this.Set is Intermech.Interfaces.Sets.Set<int> set1 && (!set1.IsLeftOpen || !set1.IsRightOpen) && set1.Contains(series))
          return ObjectFiltrationState.fsVersionBySeries;
        return this.Set is Intermech.Interfaces.Sets.Set<DateTime> set2 && (!set2.IsLeftOpen || !set2.IsRightOpen) && set2.Contains(date) ? ObjectFiltrationState.fsVersionByDate : ObjectFiltrationState.fsVariance;
      }

      /// <summary>
      /// Содержимое множества значений в виде строки, удобном для редактирования пользователем
      /// </summary>
      public string AsEditableString
      {
        get => this.Set == null ? string.Empty : this.Set.AsEditableString;
        set
        {
          if (string.IsNullOrEmpty(value))
          {
            this.Set = (ISet) null;
          }
          else
          {
            this.Set = this.Applicability == ApplicabilityBy.Series ? (ISet) new Intermech.Interfaces.Sets.Set<int>((IRangeFactory<int>) Int32RangeFactory.Factory, Array.Empty<IRange<int>>()) : (ISet) new Intermech.Interfaces.Sets.Set<DateTime>((IRangeFactory<DateTime>) DateTimeRangeFactory.Factory, Array.Empty<IRange<DateTime>>());
            this.Set.AsEditableString = value;
            if (!this.Set.IsEmpty)
              return;
            this.Set = (ISet) null;
          }
        }
      }

      /// <summary>Сравнить с другим объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(SeriesDatesApplicability other)
      {
        if (other == null)
          return 1;
        if (this == other)
          return 0;
        IObjectsInfoCache service = ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) as IObjectsInfoCache;
        int num = service == null ? this.MainObjectID.CompareTo(other.MainObjectID) : string.Compare(service.GetObjectCaption(this.MainObjectID), service.GetObjectCaption(other.MainObjectID), StringComparison.CurrentCultureIgnoreCase);
        return num != 0 ? num : this.Applicability.CompareTo((object) other.Applicability);
      }
    }
}
