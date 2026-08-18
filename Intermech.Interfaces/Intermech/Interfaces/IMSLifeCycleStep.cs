
// Type: Intermech.Interfaces.IMSLifeCycleStep
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о шаге ЖЦ
    /// </summary>
    [DebuggerDisplay("IMSLifeCycleStep: [LCStepID: {LCStepID}; Name: {Name}]")]
    [Serializable]
    public sealed class IMSLifeCycleStep : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSLifeCycleStep>,
      IDisplayable
    {
      /// <summary>Идентификатор шага ЖЦ</summary>
      private int lcStepID;
      /// <summary>Идентификатор схемы ЖЦ для данного шага ЖЦ</summary>
      private int schemaID;
      /// <summary>Идентификатор уровня продвижения для данного шага ЖЦ</summary>
      private int levelID;
      /// <summary>Название шага ЖЦ</summary>
      private string name;
      /// <summary>Guid шага ЖЦ</summary>
      private Guid guid;

      /// <summary>Идентификатор шага ЖЦ</summary>
      public int LCStepID
      {
        get => this.lcStepID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LCStepID));
          this.lcStepID = value;
        }
      }

      /// <summary>Идентификатор схемы ЖЦ для данного шага ЖЦ</summary>
      public int SchemaID
      {
        get => this.schemaID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SchemaID));
          this.schemaID = value;
        }
      }

      /// <summary>Идентификатор уровня продвижения для данного шага ЖЦ</summary>
      public int LevelID
      {
        get => this.levelID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LevelID));
          this.levelID = value;
        }
      }

      /// <summary>Название шага ЖЦ</summary>
      public string Name
      {
        get => this.name;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Name));
          this.name = value;
        }
      }

      /// <summary>Guid шага ЖЦ</summary>
      public Guid Guid
      {
        get => this.guid;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Guid));
          this.guid = value;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSLifeCycleStep imsLifeCycleStep) ? base.Equals(obj) : this.LCStepID == imsLifeCycleStep.LCStepID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.LCStepID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"LifeCycleStep: [{this.LCStepID}] {this.Name}";

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.LCStepID = -1;
        this.SchemaID = 0;
        this.LevelID = 0;
        this.Name = (string) null;
        this.Guid = Guid.Empty;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSLifeCycleStep imsLifeCycleStep))
          return;
        this.LCStepID = imsLifeCycleStep.LCStepID;
        this.SchemaID = imsLifeCycleStep.SchemaID;
        this.LevelID = imsLifeCycleStep.LevelID;
        this.Name = imsLifeCycleStep.Name;
        this.Guid = imsLifeCycleStep.Guid;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSLifeCycleStep Clone() => (IMSLifeCycleStep) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSLifeCycleStep);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSLifeCycleStep other)
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
        this.LCStepID = Convert.ToInt32(row["F_LC_STEP"]);
        this.SchemaID = Convert.ToInt32(row["F_SCHEMA_ID"]);
        this.LevelID = Convert.ToInt32(row["F_LEVEL_ID"]);
        this.Name = Convert.ToString(row["F_LC_NAME"]);
        this.Guid = new Guid(Convert.ToString(row["F_GUID"]));
      }
    }
}
