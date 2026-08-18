
// Type: Intermech.Interfaces.Compositions.ObjInfoIDItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Класс - описание объекта с идентификатором</summary>
    [Serializable]
    public class ObjInfoIDItem : ObjInfoItem, IObjInfoID, ITypedInfoItem, IEquatable<ObjInfoIDItem>
    {
      /// <summary>Конструктор</summary>
      public ObjInfoIDItem()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="dbObject"></param>
      public ObjInfoIDItem(IDBObject dbObject)
        : base(dbObject)
      {
        if (dbObject == null)
          return;
        this.ID = dbObject.ID;
      }

      /// <summary>Конструктор</summary>
      /// <param name="typedInfoItem">Описание объекта</param>
      public ObjInfoIDItem(TypedInfoItem typedInfoItem)
        : base(typedInfoItem)
      {
        if (!(typedInfoItem is IObjInfoID objInfoId))
          return;
        this.ID = objInfoId.ID;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      /// <param name="objTypeId">Ид. типа объекта</param>
      /// <param name="id"></param>
      public ObjInfoIDItem(long objectId, int objTypeId, long id = 0)
        : base(objectId, objTypeId)
      {
        this.ID = id;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      public ObjInfoIDItem(long objectId)
        : base(objectId)
      {
      }

      /// <summary>
      /// Проверяет наличие пустых (незаполненных) данных у объекта
      /// </summary>
      public override bool HasEmptyInfo => base.HasEmptyInfo || Consts.IsUndefinedObjectId(this.ID);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objInfo"></param>
      public override void CopyFrom(QuickObjectInfo objInfo)
      {
        base.CopyFrom(objInfo);
        this.ID = objInfo.ID;
      }

      /// <summary>Идентификатор объекта (не версии)</summary>
      public long ID { get; set; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(ObjInfoIDItem other) => this.Equals((ObjInfoItem) other);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objInfo"></param>
      public override void CopyFrom(TypedInfoItem typedInfoItem)
      {
        base.CopyFrom(typedInfoItem);
        ObjInfoIDItem objInfoIdItem = typedInfoItem as ObjInfoIDItem;
        if (!((TypedInfoItem) objInfoIdItem != (TypedInfoItem) null))
          return;
        this.ID = objInfoIdItem.ID;
      }
    }
}
