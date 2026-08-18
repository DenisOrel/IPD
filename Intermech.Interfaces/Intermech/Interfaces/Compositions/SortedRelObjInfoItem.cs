
// Type: Intermech.Interfaces.Compositions.SortedRelObjInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Описание связи с данными по сортировке</summary>
    [Serializable]
    public class SortedRelObjInfoItem : RelObjInfoItem
    {
      /// <summary>Конструктор</summary>
      /// <param name="dbRel"></param>
      public SortedRelObjInfoItem(IDBRelation dbRel)
        : base(dbRel)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationId">Ид. связи</param>
      /// <param name="relationTypeId">Ид. типа связи</param>
      public SortedRelObjInfoItem(long relationId, int relationTypeId)
        : base(relationId, relationTypeId)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationId">Ид. связи</param>
      public SortedRelObjInfoItem(long relationId)
        : base(relationId)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relInfo">Информация о связи</param>
      /// <param name="projInfo">Информация о родительском объекте</param>
      /// <param name="partInfo">Информация о дочернем объекте</param>
      /// <param name="sorting"></param>
      public SortedRelObjInfoItem(
        RelInfoItem relInfo,
        ObjInfoItem projInfo,
        ObjInfoItem partInfo,
        long sorting = -1)
        : base(relInfo, projInfo, partInfo)
      {
        this.Sorting = sorting;
      }

      /// <summary>Значение сортировки связи</summary>
      public long Sorting { get; set; } = -1;
    }
}
