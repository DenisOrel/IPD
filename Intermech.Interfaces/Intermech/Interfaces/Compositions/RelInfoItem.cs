
// Type: Intermech.Interfaces.Compositions.RelInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Класс - описание связи</summary>
    [Serializable]
    public class RelInfoItem : TypedInfoItem, IEquatable<RelInfoItem>
    {
      /// <summary>Конструктор</summary>
      /// <param name="dbRel"></param>
      public RelInfoItem(IDBRelation dbRel)
        : base(0L)
      {
        if (dbRel == null)
          return;
        this.ItemID = dbRel.RelationID;
        this.ItemTypeID = dbRel.RelationType;
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationId">Ид. связи</param>
      /// <param name="relationTypeId">Ид. типа связи</param>
      public RelInfoItem(long relationId, int relationTypeId = -1)
        : base(relationId, relationTypeId)
      {
      }

      /// <summary>Конструктор</summary>
      public RelInfoItem()
        : this(0L)
      {
      }

      /// <summary>Ид. связи</summary>
      /// <remarks>Для совместимости со старым кодом</remarks>
      public long RelationID
      {
        [DebuggerStepThrough] get => this.ItemID;
        [DebuggerStepThrough] set => this.ItemID = value;
      }

      /// <summary>Ид. типа связи</summary>
      /// <remarks>Для совместимости со старым кодом</remarks>
      public int RelTypeID
      {
        [DebuggerStepThrough] get => this.ItemTypeID;
        [DebuggerStepThrough] set => this.ItemTypeID = value;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public virtual bool Equals(RelInfoItem other) => this.CompareTo((object) other) == 0;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsEmpty(RelInfoItem value)
      {
        return (TypedInfoItem) value == (TypedInfoItem) null || value.RelationID == 0L || value.RelationID == -1L;
      }
    }
}
