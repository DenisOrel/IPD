
// Type: Intermech.Interfaces.IMSLifeCycleLevel
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация об уровне продвижения
    /// </summary>
    [DebuggerDisplay("IMSLifeCycleLevel: [LevelID: {LevelID}; Name: {Name}]")]
    [Serializable]
    public sealed class IMSLifeCycleLevel : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSLifeCycleLevel>,
      IDisplayable
    {
      /// <summary>Идентификатор уровня продвижения</summary>
      private int levelID;
      /// <summary>Название уровня продвижения</summary>
      private string name;
      /// <summary>Guid уровня продвижения</summary>
      private Guid guid;
      /// <summary>Предметная область</summary>
      private string areaID;
      /// <summary>Является ли уровень уровнем по умолчанию</summary>
      private bool isDefault;

      /// <summary>Идентификатор уровня продвижения</summary>
      public int LevelID
      {
        get => this.levelID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LevelID));
          this.levelID = value;
        }
      }

      /// <summary>Название уровня продвижения</summary>
      public string Name
      {
        get => this.name;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Name));
          this.name = value;
        }
      }

      /// <summary>Guid уровня продвижения</summary>
      public Guid Guid
      {
        get => this.guid;
        set
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

      /// <summary>Является ли уровень уровнем по умолчанию</summary>
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
        return !(obj is IMSLifeCycleLevel imsLifeCycleLevel) ? base.Equals(obj) : this.LevelID == imsLifeCycleLevel.LevelID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.LevelID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"LifeCycleLevel: [{this.LevelID}] {this.Name}";

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.LevelID = 0;
        this.Name = (string) null;
        this.Guid = Guid.Empty;
        this.AreaID = (string) null;
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
        if (!(source is IMSLifeCycleLevel imsLifeCycleLevel))
          return;
        this.LevelID = imsLifeCycleLevel.LevelID;
        this.Name = imsLifeCycleLevel.Name;
        this.Guid = imsLifeCycleLevel.Guid;
        this.Default = imsLifeCycleLevel.Default;
        this.AreaID = imsLifeCycleLevel.AreaID;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSLifeCycleLevel Clone() => (IMSLifeCycleLevel) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSLifeCycleLevel);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSLifeCycleLevel other)
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
        this.LevelID = Convert.ToInt32(row["F_LEVEL_ID"]);
        this.Name = Convert.ToString(row["F_LEVEL_NAME"]);
        this.Guid = new Guid(Convert.ToString(row["F_GUID"]));
        this.AreaID = Convert.ToString(row["F_AREA_ID"]);
        this.Default = Convert.ToBoolean(row["F_DEFAULT"]);
      }
    }
}
