
// Type: Intermech.Interfaces.CompositionSortingInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения информации о состоянии сортировки элементов состава
    /// </summary>
    [Serializable]
    public class CompositionSortingInfoItem : 
      ICloneable,
      IComparable,
      IComparable<CompositionSortingInfoItem>,
      IEquatable<CompositionSortingInfoItem>
    {
      /// <summary>Значение сортировки по умолчанию</summary>
      public const long DefaultSortValue = -1;

      /// <summary>Конструктор</summary>
      protected CompositionSortingInfoItem()
      {
      }

      /// <summary>Создать экземпляр класса, заполнить его информацией</summary>
      /// <param name="prjLinkId">Идентификатор связи </param>
      /// <param name="relTypeId">Идентификатор типа связи</param>
      /// <param name="partObjType">Ид. дочернего типа объекта</param>
      /// <param name="sorting">Значение атрибута сортировка</param>
      public CompositionSortingInfoItem(long prjLinkId, int relTypeId = -1, int partObjType = -1, long sorting = -1)
      {
        this.PrjLinkID = prjLinkId;
        this.RelTypeID = relTypeId;
        this.PartObjType = partObjType;
        this.Sorting = sorting;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dbRelation"></param>
      /// <param name="partDbObject"></param>
      public CompositionSortingInfoItem([NotNull] IDBRelation dbRelation, IDBObject partDbObject)
      {
        if (dbRelation != null)
        {
          this.PrjLinkID = dbRelation.RelationID;
          this.RelTypeID = dbRelation.RelationType;
        }
        if (partDbObject == null)
          return;
        this.PartObjType = partDbObject.ObjectType;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source"></param>
      public CompositionSortingInfoItem(
        [NotNull] CompositionSortingInfoItem prototypeSortingInfoItem)
      {
        this.PrjLinkID = prototypeSortingInfoItem.PrjLinkID;
        this.RelTypeID = prototypeSortingInfoItem.RelTypeID;
        this.PartObjType = prototypeSortingInfoItem.PartObjType;
        this.Sorting = prototypeSortingInfoItem.Sorting;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source"></param>
      public CompositionSortingInfoItem([NotNull] RelObjInfoItem relObjInfoItem)
      {
        this.PrjLinkID = relObjInfoItem.RelationID;
        this.RelTypeID = relObjInfoItem.RelTypeID;
        if (!((TypedInfoItem) relObjInfoItem.PartInfo != (TypedInfoItem) null))
          return;
        this.PartObjType = relObjInfoItem.PartInfo.ObjTypeID;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.PrjLinkID.GetHashCode();

      /// <summary>
      /// Получить представление экземпляра класса в виде строки
      /// </summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return $"[{this.PrjLinkID}:{this.RelTypeID}:{this.PartObjType} {this.Sorting}])";
      }

      /// <summary>Идентификатор связи</summary>
      public long PrjLinkID { get; protected set; }

      /// <summary>Идентификатор типа связи</summary>
      public int RelTypeID { get; set; }

      /// <summary>Ид. дочернего типа объекта</summary>
      public int PartObjType { get; set; }

      /// <summary>Значение атрибута сортировка</summary>
      public long Sorting { get; set; } = -1;

      /// <summary>Создание копии объекта</summary>
      /// <returns></returns>
      public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Очистить экземпляр класса</summary>
      public virtual void Clear()
      {
        this.PrjLinkID = 0L;
        this.PartObjType = -1;
        this.PartObjType = -1;
        this.Sorting = -1L;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public virtual int CompareTo(object obj) => this.CompareTo(obj as CompositionSortingInfoItem);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(CompositionSortingInfoItem other)
      {
        return other != null ? this.PrjLinkID.CompareTo(other.PrjLinkID) : 1;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns></returns>
      public bool Equals(CompositionSortingInfoItem other) => this.CompareTo(other) == 0;
    }
}
