
// Type: Intermech.Kernel.Search.NodeColumnID
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Globalization;


namespace Intermech.Kernel.Search
{
    /// <summary>Атрибут и его источник</summary>
    [DebuggerDisplay("ID: {_ID}; Attr source: {_attrSource}")]
    [Serializable]
    public class NodeColumnID : IComparable, IComparable<NodeColumnID>, ICloneable
    {
      /// <summary>Идентификатор атрибута</summary>
      private object _ID = (object) -10000;
      /// <summary>Источник атрибута</summary>
      private AttributeSourceTypes _attrSource;

      /// <summary>Идентификатор атрибута</summary>
      public object ID
      {
        [DebuggerStepThrough] get => this._ID;
        set => this._ID = value ?? this._ID;
      }

      /// <summary>Источник атрибута</summary>
      public AttributeSourceTypes AttrSource
      {
        [DebuggerStepThrough] get => this._attrSource;
        set => this._attrSource = value;
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="AnID">Идентификатор атрибута</param>
      /// <param name="AnAttrSource">Источник атрибута</param>
      public NodeColumnID(object AnID, AttributeSourceTypes AnAttrSource)
      {
        this._ID = AnID;
        this._attrSource = AnAttrSource;
      }

      /// <summary>Возвращает идентификатор атрибута, если он имеется</summary>
      public virtual int AttributeID
      {
        get
        {
          if (this._ID is int)
            return Convert.ToInt32(this._ID);
          return this._ID is ObligatoryObjectAttributes ? (int) this._ID : 0;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (!(obj is NodeColumnID nodeColumnId))
          return base.Equals(obj);
        return this._ID.Equals(nodeColumnId._ID) && this._attrSource == nodeColumnId._attrSource;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => (int) this._attrSource << 28 ^ this._ID.GetHashCode();

      /// <summary>Получить экземпляр класса в виде строки</summary>
      /// <returns>Экземпляр класса в виде строки</returns>
      public override string ToString()
      {
        return $"[{((int) this._attrSource).ToString("X", (IFormatProvider) CultureInfo.InvariantCulture)}]{this._ID.ToString()}";
      }

      /// <summary>Выполнить сравнение с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.Equals(obj) ? 0 : 1;

      /// <summary>Выполнить сравнение с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(NodeColumnID other) => other == null || !this.Equals((object) other) ? 1 : 0;

      /// <summary>Создать точную копию экземпляра объекта</summary>
      /// <returns>Точная копия экземпляра объекта</returns>
      public object Clone() => (object) new NodeColumnID(this._ID, this._attrSource);
    }
}
