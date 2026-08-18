
// Type: Intermech.Interfaces.Compositions.TypedInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс - типизированное описание объекта / связи / атрибута
    /// </summary>
    [Serializable]
    public class TypedInfoItem : ITypedInfoItem, IComparable, IEquatable<TypedInfoItem>, ICloneable
    {
      /// <summary>Конструктор</summary>
      /// <param name="itemId">Идентификатор</param>
      /// <param name="itemTypeId">Ид. типа </param>
      public TypedInfoItem(long itemId, int itemTypeId = -1)
      {
        this.ItemID = itemId;
        this.ItemTypeID = itemTypeId;
      }

      /// <summary>Конструктор</summary>
      public TypedInfoItem()
        : this(-1L)
      {
      }

      /// <summary>Идентификатор</summary>
      public long ItemID { [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get; [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] protected internal set; }

      /// <summary>Ид. типа</summary>
      public int ItemTypeID { [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get; [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="typedInfoItem"></param>
      public virtual void CopyFrom(TypedInfoItem typedInfoItem)
      {
        this.ItemID = typedInfoItem.ItemID;
        this.ItemTypeID = typedInfoItem.ItemTypeID;
      }

      /// <summary>
      /// Проверяет наличие пустых (незаполненных) данных у объекта
      /// </summary>
      public virtual bool HasEmptyInfo => this.ItemTypeID == -1;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public virtual int CompareTo(object obj)
      {
        TypedInfoItem typedInfoItem = obj as TypedInfoItem;
        return typedInfoItem != (TypedInfoItem) null ? this.ItemID.CompareTo(typedInfoItem.ItemID) : -1;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public virtual bool Equals(TypedInfoItem other) => this.CompareTo((object) other) == 0;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public virtual object Clone() => this.MemberwiseClone();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => this.ItemID.GetHashCode();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj) => this.Equals(obj as TypedInfoItem);

      /// <summary>Перекроем оператор сравнения</summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static bool operator ==(TypedInfoItem a, TypedInfoItem b)
      {
        return (object) a == null ? (object) b == null : a.Equals(b);
      }

      /// <summary>Перекроем оператор сравнения</summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static bool operator !=(TypedInfoItem a, TypedInfoItem b)
      {
        return (object) a == null ? b != null : !a.Equals(b);
      }
    }
}
