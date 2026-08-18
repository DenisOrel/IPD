
// Type: Intermech.Interfaces.IMSAttribute4RelationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о типе атрибута для типа связи
    /// </summary>
    [Serializable]
    public sealed class IMSAttribute4RelationType : 
      IMSAttribute4,
      IComparable,
      IComparable<IMSAttribute4RelationType>
    {
      /// <summary>
      /// Идентификатор типа связи, которой назначен тип атрибута
      /// </summary>
      private int relationTypeID;

      /// <summary>
      /// Идентификатор типа связи, которой назначен тип атрибута
      /// </summary>
      public int RelationTypeID
      {
        get => this.relationTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RelationTypeID));
          this.relationTypeID = value;
        }
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.RelationTypeID = -1;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSAttribute4RelationType attribute4RelationType))
          return;
        this.RelationTypeID = attribute4RelationType.RelationTypeID;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSAttribute4RelationType Clone() => (IMSAttribute4RelationType) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public override int CompareTo(object obj) => this.CompareTo(obj as IMSAttribute4RelationType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttribute4RelationType other)
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
        this.RelationTypeID = Convert.ToInt32(row["F_RELATION_TYPE"]);
      }

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return $"Attribute4RelationType: [{this.AttributeID}] {MetaDataHelper.GetAttributeTypeName(this.AttributeID)} ({MetaDataHelper.GetRelationTypeName(this.RelationTypeID)})";
      }
    }
}
