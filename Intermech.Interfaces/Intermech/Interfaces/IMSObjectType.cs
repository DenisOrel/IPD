
// Type: Intermech.Interfaces.IMSObjectType
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
    /// Вспомогательный класс-значение - краткая информация о типе объекта
    /// </summary>
    [Serializable]
    public sealed class IMSObjectType : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSObjectType>,
      IDisplayable
    {
      /// <summary>
      /// Идентификатор типа объекта (в классе IMSObjectType нужен только для операций сравнения)
      /// </summary>
      private int objectTypeID;
      /// <summary>Guid типа объекта</summary>
      private Guid guid;
      /// <summary>Наименование типа объектов (например, "Детали")</summary>
      private string objectTypeName;
      /// <summary>
      /// Наименование объекта данного типа (например, "Деталь")
      /// </summary>
      private string objectName;
      /// <summary>Управление версионностью типов объектов</summary>
      private ObjectVersionModes versionsMode;
      /// <summary>Тип связи по умолчанию</summary>
      private int defaultRelation;
      /// <summary>Предметная область</summary>
      private string areaID;
      /// <summary>Идентификатор схемы ЖЦ</summary>
      private int schemaID;
      /// <summary>Атрибут-описатель</summary>
      private int captionAttribute;
      /// <summary>
      /// Можно ли назначать любые типы атрибутов экземплярам данного типа объекта
      /// </summary>
      private bool anyAttributes;
      /// <summary>Краткое наименование</summary>
      private string shortName;
      /// <summary>Примечание</summary>
      private string note;
      /// <summary>Опции, регулирующие поведение типов объектов</summary>
      private ObjectTypeOptions options;

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSObjectType imsObjectType) ? base.Equals(obj) : this.ObjectTypeID == imsObjectType.ObjectTypeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ObjectTypeID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"ObjectType: [{this.ObjectTypeID}] {this.ObjectTypeName}";

      /// <summary>
      /// Идентификатор типа объекта (в классе IMSObjectType нужен только для операций сравнения)
      /// </summary>
      public int ObjectTypeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.objectTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ObjectTypeID));
          this.objectTypeID = value;
        }
      }

      /// <summary>Guid типа объекта</summary>
      public Guid Guid
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.guid;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Guid));
          this.guid = value;
        }
      }

      /// <summary>Наименование типа объектов (например, "Детали")</summary>
      public string ObjectTypeName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.objectTypeName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ObjectTypeName));
          this.objectTypeName = value;
        }
      }

      /// <summary>
      /// Наименование объекта данного типа (например, "Деталь")
      /// </summary>
      public string ObjectName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.objectName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ObjectName));
          this.objectName = value;
        }
      }

      /// <summary>Управление версионностью типов объектов</summary>
      public ObjectVersionModes VersionsMode
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.versionsMode;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (VersionsMode));
          this.versionsMode = value;
        }
      }

      /// <summary>Тип связи по умолчанию</summary>
      public int DefaultRelation
      {
        get => this.defaultRelation;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (DefaultRelation));
          this.defaultRelation = value;
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

      /// <summary>Идентификатор схемы ЖЦ</summary>
      public int SchemaID
      {
        get => this.schemaID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SchemaID));
          this.schemaID = value;
        }
      }

      /// <summary>Атрибут-описатель</summary>
      public int CaptionAttribute
      {
        get => this.captionAttribute;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (CaptionAttribute));
          this.captionAttribute = value;
        }
      }

      /// <summary>
      /// Можно ли назначать любые типы атрибутов экземплярам данного типа объекта
      /// </summary>
      public bool AnyAttributes
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.anyAttributes;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AnyAttributes));
          this.anyAttributes = value;
        }
      }

      /// <summary>Краткое наименование</summary>
      public string ShortName
      {
        get => this.shortName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ShortName));
          this.shortName = value;
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

      /// <summary>Опции, регулирующие поведение типов объектов</summary>
      public ObjectTypeOptions Options
      {
        get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>Является ли тип данных локальным</summary>
      public bool IsLocalType
      {
        [DebuggerStepThrough] get
        {
          return (this.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType;
        }
      }

      /// <summary>
      /// Запрет создания объектов указанного типа командами "Навигатора"
      /// </summary>
      public bool IsDisableManualCreate
      {
        [DebuggerStepThrough] get
        {
          return (this.Options & ObjectTypeOptions.DisableManualCreate) == ObjectTypeOptions.DisableManualCreate;
        }
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.AreaID = (string) null;
        this.DefaultRelation = -1;
        this.Guid = Guid.Empty;
        this.ObjectName = (string) null;
        this.ObjectTypeID = -1;
        this.ObjectTypeName = (string) null;
        this.SchemaID = 0;
        this.VersionsMode = ObjectVersionModes.Abstract;
        this.CaptionAttribute = 0;
        this.AnyAttributes = false;
        this.ShortName = (string) null;
        this.Note = (string) null;
        this.Options = ObjectTypeOptions.None;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSObjectType imsObjectType))
          return;
        this.AreaID = imsObjectType.AreaID;
        this.DefaultRelation = imsObjectType.DefaultRelation;
        this.Guid = imsObjectType.Guid;
        this.ObjectName = imsObjectType.ObjectName;
        this.ObjectTypeID = imsObjectType.ObjectTypeID;
        this.ObjectTypeName = imsObjectType.ObjectTypeName;
        this.SchemaID = imsObjectType.SchemaID;
        this.VersionsMode = imsObjectType.VersionsMode;
        this.CaptionAttribute = imsObjectType.CaptionAttribute;
        this.AnyAttributes = imsObjectType.AnyAttributes;
        this.ShortName = imsObjectType.ShortName;
        this.Note = imsObjectType.Note;
        this.Options = imsObjectType.Options;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSObjectType Clone() => (IMSObjectType) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSObjectType);

      /// <summary>
      /// Выполнить сравнение с указанным объектом (сравнение идёт по названию типа объекта)
      /// </summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSObjectType other)
      {
        return other == null ? 1 : this.ObjectTypeName.CompareTo(other.ObjectTypeName);
      }

      /// <summary>Отображаемый на экране текст</summary>
      public string Text
      {
        [DebuggerStepThrough] get => this.ObjectTypeName;
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.ObjectTypeID = DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", -1);
        this.Guid = new Guid(Convert.ToString(row["F_GUID"]));
        this.ObjectTypeName = Convert.ToString(row["F_OBJ_TYPE_NAME"]);
        this.ObjectName = Convert.ToString(row["F_OBJ_NAME"]);
        int result;
        this.VersionsMode = !int.TryParse(Convert.ToString(row["F_VERSIONABLE"]), out result) ? ObjectVersionModes.Abstract : (ObjectVersionModes) result;
        this.DefaultRelation = DataSetProcessor.GetInt32Value(row, "F_DEFAULT_RELATION", -1);
        this.AreaID = Convert.ToString(row["F_AREA_ID"]);
        this.SchemaID = DataSetProcessor.GetInt32Value(row, "F_SCHEMA_ID", -1);
        this.CaptionAttribute = DataSetProcessor.GetInt32Value(row, "F_CAPTION_ATTRIBUTE", 0);
        this.AnyAttributes = DataSetProcessor.GetInt32Value(row, "F_ANY_ATTRIBUTES", 1) == 1;
        this.ShortName = Convert.ToString(row["F_SHORT_NAME"]);
        this.Note = Convert.ToString(row["F_NOTE"]);
        if (int.TryParse(Convert.ToString(row["F_OPTIONS"]), out result))
          this.Options = (ObjectTypeOptions) result;
        else
          this.Options = ObjectTypeOptions.None;
      }
    }
}
