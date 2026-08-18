
// Type: Intermech.Interfaces.IMSRelationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о типе связи
    /// </summary>
    [DebuggerDisplay("IMSRelationType: [RelationTypeID: {RelationTypeID}, \"{Description}\"]")]
    [Serializable]
    public sealed class IMSRelationType : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSRelationType>,
      IDisplayable
    {
      /// <summary>
      /// Идентификатор типа связи (в классе используется для операций сравнения)
      /// </summary>
      private int relationTypeID;
      /// <summary>Глобальный идентификатор типа связи</summary>
      private Guid guid;
      /// <summary>Наименование типа связи (например, "Проектная связь")</summary>
      private string description;
      /// <summary>Имя вида связи (например, "Состоит из")</summary>
      private string typeName;
      /// <summary>Обратное название связи (например, "Входит в")</summary>
      private string reverseName;
      /// <summary>
      /// Нужно ли извлекать на диск файлы объектов, объединённых данной связью (1 - нужно, 0 - не нужно)
      /// </summary>
      private int chkOutFile;
      /// <summary>
      /// Вид связи: 0 - вертикальная (например, состоит из);  1 - горизонтальная (взаимозаменяемый)
      /// </summary>
      private int relationKind;
      /// <summary>Предметная область</summary>
      private string areaID;
      /// <summary>Примечание</summary>
      private string note;
      /// <summary>Можно ли назначать связям данного типа любые атрибуты</summary>
      private bool anyAttributes;
      /// <summary>Краткое название</summary>
      private string shortName;
      /// <summary>
      /// Опции (содержат битовые флаги для управления свойствами типа связей)
      /// </summary>
      private RelationTypeOptions options;

      /// <summary>
      /// Идентификатор типа связи (в классе используется для операций сравнения)
      /// </summary>
      public int RelationTypeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.relationTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RelationTypeID));
          this.relationTypeID = value;
        }
      }

      /// <summary>Глобальный идентификатор типа связи</summary>
      public Guid Guid
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.guid;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Guid));
          this.guid = value;
        }
      }

      /// <summary>Наименование типа связи (например, "Проектная связь")</summary>
      public string Description
      {
        get => this.description;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Description));
          this.description = value;
        }
      }

      /// <summary>Имя вида связи (например, "Состоит из")</summary>
      public string TypeName
      {
        get => this.typeName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (TypeName));
          this.typeName = value;
        }
      }

      /// <summary>Обратное название связи (например, "Входит в")</summary>
      public string ReverseName
      {
        get => this.reverseName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ReverseName));
          this.reverseName = value;
        }
      }

      /// <summary>
      /// Нужно ли извлекать на диск файлы объектов, объединённых данной связью (1 - нужно, 0 - не нужно)
      /// </summary>
      public int ChkOutFile
      {
        get => this.chkOutFile;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ChkOutFile));
          this.chkOutFile = value;
        }
      }

      /// <summary>
      /// Вид связи: 0 - вертикальная (например, состоит из);  1 - горизонтальная (взаимозаменяемый)
      /// </summary>
      public int RelationKind
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.relationKind;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RelationKind));
          this.relationKind = value;
        }
      }

      /// <summary>Предметная область</summary>
      public string AreaID
      {
        get => this.areaID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AreaID));
          this.areaID = value;
        }
      }

      /// <summary>Примечание</summary>
      public string Note
      {
        get => this.note;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Note));
          this.note = value;
        }
      }

      /// <summary>Можно ли назначать связям данного типа любые атрибуты</summary>
      public bool AnyAttributes
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.anyAttributes;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AnyAttributes));
          this.anyAttributes = value;
        }
      }

      /// <summary>Краткое название</summary>
      public string ShortName
      {
        get => this.shortName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ShortName));
          this.shortName = value;
        }
      }

      /// <summary>
      /// Опции (содержат битовые флаги для управления свойствами типа связей)
      /// </summary>
      public RelationTypeOptions Options
      {
        get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>Выполнить сравнение с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSRelationType imsRelationType) ? base.Equals(obj) : this.RelationTypeID == imsRelationType.RelationTypeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.RelationTypeID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"RelationType: [{this.RelationTypeID}] {this.Description}";

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.RelationTypeID = -1;
        this.Guid = Guid.Empty;
        this.Description = (string) null;
        this.TypeName = (string) null;
        this.ReverseName = (string) null;
        this.ChkOutFile = 0;
        this.RelationKind = 0;
        this.AreaID = (string) null;
        this.AnyAttributes = false;
        this.Note = (string) null;
        this.ShortName = (string) null;
        this.Options = RelationTypeOptions.None;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSRelationType imsRelationType))
          return;
        this.RelationTypeID = imsRelationType.RelationTypeID;
        this.Guid = imsRelationType.Guid;
        this.Description = imsRelationType.Description;
        this.TypeName = imsRelationType.TypeName;
        this.ReverseName = imsRelationType.ReverseName;
        this.ChkOutFile = imsRelationType.ChkOutFile;
        this.RelationKind = imsRelationType.RelationKind;
        this.AreaID = imsRelationType.AreaID;
        this.AnyAttributes = imsRelationType.AnyAttributes;
        this.Note = imsRelationType.Note;
        this.ShortName = imsRelationType.ShortName;
        this.Options = imsRelationType.Options;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSRelationType Clone() => (IMSRelationType) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSRelationType);

      /// <summary>Выполнить сравнение с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSRelationType other)
      {
        return other == null ? 1 : this.Description.CompareTo(other.Description);
      }

      /// <summary>Отображаемый на экране текст</summary>
      public string Text
      {
        [DebuggerStepThrough] get => this.Description;
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.RelationTypeID = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", -1);
        this.Guid = new Guid(Convert.ToString(row["F_GUID"]));
        this.Description = Convert.ToString(row["F_DESCRIPTION"]);
        this.TypeName = Convert.ToString(row["F_TYPE_NAME"]);
        this.ReverseName = Convert.ToString(row["F_REVERSE_NAME"]);
        this.ChkOutFile = DataSetProcessor.GetInt32Value(row, "F_CHKOUTFILE", 0);
        this.RelationKind = DataSetProcessor.GetInt32Value(row, "F_RELATION_KIND", 0);
        this.AreaID = Convert.ToString(row["F_AREA_ID"]);
        this.AnyAttributes = DataSetProcessor.GetInt32Value(row, "F_ANY_ATTRIBUTES", 1) == 1;
        this.Note = Convert.ToString(row["F_NOTE"]);
        this.ShortName = Convert.ToString(row["F_SHORT_NAME"]);
        this.Options = row.Table.Columns.IndexOf("F_OPTIONS") >= 0 ? (RelationTypeOptions) Convert.ToInt32(row["F_OPTIONS"]) : RelationTypeOptions.None;
      }
    }
}
