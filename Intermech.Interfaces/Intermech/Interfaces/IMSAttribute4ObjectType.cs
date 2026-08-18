
// Type: Intermech.Interfaces.IMSAttribute4ObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о типе атрибута для типа объекта
    /// </summary>
    [Serializable]
    public sealed class IMSAttribute4ObjectType : 
      IMSAttribute4,
      IComparable,
      IComparable<IMSAttribute4ObjectType>
    {
      /// <summary>
      /// Идентификатор типа объекта, которому назначен тип атрибута
      /// </summary>
      private int objectTypeID;
      /// <summary>Режимы наследования атрибутов от типов объектов</summary>
      private InheritModes isPublic;
      /// <summary>Метод контроля уникальности значений атрибута</summary>
      private UniqueValueModes unique;
      /// <summary>Идентификатор уровня продвижения.</summary>
      private int levelID;

      /// <summary>
      /// Идентификатор типа объекта, которому назначен тип атрибута
      /// </summary>
      public int ObjectTypeID
      {
        get => this.objectTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ObjectTypeID));
          this.objectTypeID = value;
        }
      }

      /// <summary>Режимы наследования атрибутов от типов объектов</summary>
      public InheritModes Public
      {
        get => this.isPublic;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Public));
          this.isPublic = value;
        }
      }

      /// <summary>Метод контроля уникальности значений атрибута</summary>
      public UniqueValueModes Unique
      {
        get => this.unique;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Unique));
          this.unique = value;
        }
      }

      /// <summary>Идентификатор уровня продвижения.</summary>
      public int LevelID
      {
        get => this.levelID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LevelID));
          this.levelID = value;
        }
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.ObjectTypeID = -1;
        this.Public = InheritModes.Public;
        this.Unique = UniqueValueModes.NotUnique;
        this.LevelID = 0;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSAttribute4ObjectType attribute4ObjectType))
          return;
        this.ObjectTypeID = attribute4ObjectType.ObjectTypeID;
        this.Public = attribute4ObjectType.Public;
        this.Unique = attribute4ObjectType.Unique;
        this.LevelID = attribute4ObjectType.LevelID;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSAttribute4ObjectType Clone() => (IMSAttribute4ObjectType) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public override int CompareTo(object obj) => this.CompareTo(obj as IMSAttribute4ObjectType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttribute4ObjectType other)
      {
        return other == null ? 1 : this.AttributeID.CompareTo(other.AttributeID);
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.ObjectTypeID = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        this.Public = (InheritModes) Convert.ToInt32(row["F_PUBLIC"]);
        this.Unique = (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]);
        this.LevelID = Convert.ToInt32(row["F_LEVEL_ID"]);
      }

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return $"Attribute4ObjectType: [{this.AttributeID}] {MetaDataHelper.GetAttributeTypeName(this.AttributeID)} ({MetaDataHelper.GetObjectTypeName(this.ObjectTypeID)})";
      }
    }
}
