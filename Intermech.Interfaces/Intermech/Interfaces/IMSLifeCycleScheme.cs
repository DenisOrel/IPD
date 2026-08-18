
// Type: Intermech.Interfaces.IMSLifeCycleScheme
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о схеме жизненного цикла
    /// </summary>
    [DebuggerDisplay("IMSLifeCycleScheme: [SchemaID: {SchemaID}; Name: {Name}]")]
    [Serializable]
    public sealed class IMSLifeCycleScheme : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSLifeCycleScheme>,
      IDisplayable
    {
      /// <summary>Идентификатор схемы жизненного цикла</summary>
      private int schemaID;
      /// <summary>Название схемы жизненного цикла</summary>
      private string name;
      /// <summary>Guid схемы жизненного цикла</summary>
      private Guid guid;
      /// <summary>Предметная область</summary>
      private string areaID;
      /// <summary>Параметры</summary>
      private long options;
      /// <summary>Является ли схема схемой по умолчанию</summary>
      private bool isDefault;

      /// <summary>Идентификатор схемы жизненного цикла</summary>
      public int SchemaID
      {
        get => this.schemaID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SchemaID));
          this.schemaID = value;
        }
      }

      /// <summary>Название схемы жизненного цикла</summary>
      public string Name
      {
        get => this.name;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Name));
          this.name = value;
        }
      }

      /// <summary>Guid схемы жизненного цикла</summary>
      public Guid Guid
      {
        get => this.guid;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Guid));
          this.guid = value;
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

      /// <summary>Параметры</summary>
      public long Options
      {
        get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>Является ли схема схемой по умолчанию</summary>
      public bool Default
      {
        get => this.isDefault;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Default));
          this.isDefault = value;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSLifeCycleScheme imsLifeCycleScheme) ? base.Equals(obj) : this.SchemaID == imsLifeCycleScheme.SchemaID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.SchemaID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"LifeCycleScheme: [{this.SchemaID}] {this.Name}";

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.SchemaID = 0;
        this.Name = (string) null;
        this.Guid = Guid.Empty;
        this.AreaID = (string) null;
        this.Options = 0L;
        this.Default = false;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSLifeCycleScheme imsLifeCycleScheme))
          return;
        this.SchemaID = imsLifeCycleScheme.SchemaID;
        this.Name = imsLifeCycleScheme.Name;
        this.Guid = imsLifeCycleScheme.Guid;
        this.Options = imsLifeCycleScheme.Options;
        this.Default = imsLifeCycleScheme.Default;
        this.AreaID = imsLifeCycleScheme.AreaID;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSLifeCycleScheme Clone() => (IMSLifeCycleScheme) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSLifeCycleScheme);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSLifeCycleScheme other)
      {
        return other == null ? 1 : this.Name.CompareTo(other.Name);
      }

      /// <summary>Отображаемый на экране текст</summary>
      public string Text
      {
        [DebuggerStepThrough] get => this.Name;
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.SchemaID = Convert.ToInt32(row["F_SCHEMA_ID"]);
        this.Name = Convert.ToString(row["F_NAME"]);
        this.Guid = new Guid(Convert.ToString(row["F_GUID"]));
        this.AreaID = Convert.ToString(row["F_AREA_ID"]);
        this.Options = Convert.ToInt64(row["F_OPTIONS"]);
        this.Default = Convert.ToBoolean(row["F_DEFAULT"]);
      }
    }
}
