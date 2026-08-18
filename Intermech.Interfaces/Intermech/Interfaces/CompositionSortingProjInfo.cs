
// Type: Intermech.Interfaces.CompositionSortingProjInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения информации о состоянии сортировки элементов состава c доп.
    /// информацией о род. объекте
    /// </summary>
    [Serializable]
    public class CompositionSortingProjInfo : CompositionSortingInfoItem
    {
      /// <summary>Конструктор</summary>
      public CompositionSortingProjInfo()
      {
      }

      /// <summary>Создать экземпляр класса, заполнить его информацией</summary>
      /// <param name="prjLinkId">Идентификатор связи </param>
      /// <param name="relTypeId">Идентификатор типа связи</param>
      /// <param name="projObjId">Ид. версии родительского объекта</param>
      /// <param name="projObjType">Ид. родительского типа объекта</param>
      /// <param name="partObjType">Ид. дочернего типа объекта</param>
      /// <param name="sorting">Значение атрибута сортировка</param>
      public CompositionSortingProjInfo(
        long prjLinkId,
        int relTypeId = -1,
        long projObjId = 0,
        int projObjType = -1,
        int partObjType = -1,
        long sorting = -1)
        : base(prjLinkId, relTypeId, partObjType, sorting)
      {
        this.ProjObjID = projObjId;
        this.ProjTypeID = projObjType;
      }

      /// <summary>Создать экземпляр класса, заполнить его информацией</summary>
      /// <param name="dbRelation">Связь</param>
      /// <param name="projObject">Родительский объект</param>
      /// <param name="partObject">Дочерний объект</param>
      /// <param name="sorting">Значение сортировки</param>
      public CompositionSortingProjInfo(
        [NotNull] IDBRelation dbRelation,
        IDBObject projObject,
        IDBObject partObject)
        : base(dbRelation, partObject)
      {
        if (projObject != null)
        {
          this.ProjObjID = projObject.ObjectID;
          this.ProjTypeID = projObject.ObjectType;
        }
        if (partObject == null)
          return;
        this.PartObjType = partObject.ObjectType;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source"></param>
      public CompositionSortingProjInfo(
        [NotNull] CompositionSortingProjInfo prototypeSortingProjInfo)
        : base((CompositionSortingInfoItem) prototypeSortingProjInfo)
      {
        this.ProjObjID = prototypeSortingProjInfo.ProjObjID;
        this.ProjTypeID = prototypeSortingProjInfo.ProjTypeID;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source"></param>
      public CompositionSortingProjInfo(
        [NotNull] CompositionSortingInfoItem prototypeSortingInfoItem)
        : base(prototypeSortingInfoItem)
      {
        this.ProjObjID = 0L;
        this.ProjTypeID = -1;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source"></param>
      public CompositionSortingProjInfo([NotNull] RelObjInfoItem relObjInfoItem)
        : base(relObjInfoItem)
      {
        if (!((TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null))
          return;
        this.ProjObjID = relObjInfoItem.ProjInfo.ObjectID;
        this.ProjTypeID = relObjInfoItem.ProjInfo.ObjTypeID;
      }

      /// <summary>Идентификатор версии родительского объекта</summary>
      public long ProjObjID { get; set; }

      /// <summary>Идентификатор типа родительского объекта</summary>
      public int ProjTypeID { get; set; }

      /// <summary>Очистить экземпляр класса</summary>
      public override void Clear()
      {
        base.Clear();
        this.ProjObjID = 0L;
        this.ProjTypeID = -1;
      }
    }
}
