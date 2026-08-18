
// Type: Intermech.Interfaces.Compositions.ObjInfoQuickItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс - аналог QuickObjectInfo, содержащий краткую информацию об объекте
    /// </summary>
    [Serializable]
    public class ObjInfoQuickItem : 
      ObjInfoIDItem,
      IObjInfoGuid,
      IObjInfoCaption,
      ITypedInfoItem,
      IEquatable<ObjInfoQuickItem>
    {
      /// <summary>Конструктор</summary>
      public ObjInfoQuickItem()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="dbObject"></param>
      public ObjInfoQuickItem(IDBObject dbObject)
        : base(dbObject)
      {
        if (dbObject == null)
          return;
        this.VersionGuid = dbObject.ObjectGUID;
        this.Caption = dbObject.Caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="typedInfoItem">Описание объекта</param>
      public ObjInfoQuickItem(TypedInfoItem typedInfoItem)
        : base(typedInfoItem)
      {
        if (typedInfoItem is IObjInfoGuid objInfoGuid)
          this.VersionGuid = objInfoGuid.VersionGuid;
        if (!(typedInfoItem is IObjInfoCaption objInfoCaption))
          return;
        this.Caption = objInfoCaption.Caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      /// <param name="objTypeId">Ид. типа объекта</param>
      /// <param name="id"></param>
      /// <param name="guid"></param>
      /// <param name="caption"></param>
      public ObjInfoQuickItem(long objectId, int objTypeId, long id = 0, Guid guid = default (Guid), string caption = null)
        : base(objectId, objTypeId, id)
      {
        this.VersionGuid = guid;
        this.Caption = caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      public ObjInfoQuickItem(long objectId)
        : base(objectId)
      {
      }

      /// <summary>
      /// Проверяет наличие пустых (незаполненных) данных у объекта
      /// </summary>
      public override bool HasEmptyInfo
      {
        get => base.HasEmptyInfo || this.VersionGuid == Guid.Empty || this.Caption == null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objInfo"></param>
      public override void CopyFrom(QuickObjectInfo objInfo)
      {
        base.CopyFrom(objInfo);
        this.VersionGuid = objInfo.VersionGuid;
        this.Caption = objInfo.Caption;
      }

      /// <summary>Гл. идентификатор версии объекта</summary>
      public Guid VersionGuid { get; set; } = Guid.Empty;

      /// <summary>Заголовок объекта</summary>
      public string Caption { get; set; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(ObjInfoQuickItem other) => this.Equals((ObjInfoIDItem) other);
    }
}
