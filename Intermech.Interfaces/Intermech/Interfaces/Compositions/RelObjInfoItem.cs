
// Type: Intermech.Interfaces.Compositions.RelObjInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс - описание связи с данными по объектам: дочернем / родителю
    /// </summary>
    [Serializable]
    public class RelObjInfoItem : RelInfoItem
    {
      /// <summary>Конструктор</summary>
      /// <param name="dbRel"></param>
      public RelObjInfoItem([CanBeNull] IDBRelation dbRel)
        : base(dbRel)
      {
        if (dbRel == null)
          return;
        this.ProjInfo = new ObjInfoItem(dbRel.ProjID);
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationId">Ид. связи</param>
      /// <param name="relationTypeId">Ид. типа связи</param>
      public RelObjInfoItem(long relationId, int relationTypeId)
        : base(relationId, relationTypeId)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationId">Ид. связи</param>
      public RelObjInfoItem(long relationId)
        : base(relationId)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relInfo">Информация о связи</param>
      /// <param name="projInfo">Информация о родительском объекте</param>
      /// <param name="partInfo">Информация о дочернем объекте</param>
      public RelObjInfoItem(RelInfoItem relInfo, ObjInfoItem projInfo, ObjInfoItem partInfo)
      {
        if ((TypedInfoItem) relInfo != (TypedInfoItem) null)
        {
          this.ItemID = relInfo.RelationID;
          this.ItemTypeID = relInfo.RelTypeID;
        }
        this.ProjInfo = projInfo;
        this.PartInfo = partInfo;
      }

      /// <summary>Информация о родительском объекте</summary>
      public ObjInfoItem ProjInfo { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

      /// <summary>Информация о дочернем объекте</summary>
      public ObjInfoItem PartInfo { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
    }
}
