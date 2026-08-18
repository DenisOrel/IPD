
// Type: Intermech.Interfaces.Compositions.ObjInfoCaptionItem
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
    public class ObjInfoCaptionItem : 
      ObjInfoItem,
      IObjInfoCaption,
      ITypedInfoItem,
      IEquatable<ObjInfoCaptionItem>
    {
      /// <summary>Заголовок объекта</summary>
      /// <remarks> null обозначает, что заголовок еще не получен</remarks>
      protected string _caption;

      /// <summary>Конструктор</summary>
      public ObjInfoCaptionItem()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="dbObject"></param>
      public ObjInfoCaptionItem(IDBObject dbObject)
        : base(dbObject)
      {
        if (dbObject == null)
          return;
        this._caption = dbObject.Caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="typedInfoItem">Описание объекта</param>
      public ObjInfoCaptionItem(TypedInfoItem typedInfoItem)
        : base(typedInfoItem)
      {
        if (!(typedInfoItem is IObjInfoCaption objInfoCaption))
          return;
        this._caption = objInfoCaption.Caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      /// <param name="objTypeId">Ид. типа объекта</param>
      /// <param name="caption"></param>
      public ObjInfoCaptionItem(long objectId, int objTypeId, string caption = null)
        : base(objectId, objTypeId)
      {
        this._caption = caption;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      public ObjInfoCaptionItem(long objectId)
        : base(objectId)
      {
      }

      /// <summary>
      /// Проверяет наличие пустых (незаполненных) данных у объекта
      /// </summary>
      public override bool HasEmptyInfo => base.HasEmptyInfo || this._caption == null;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objInfo"></param>
      public override void CopyFrom(QuickObjectInfo objInfo)
      {
        base.CopyFrom(objInfo);
        this._caption = objInfo.Caption;
      }

      /// <summary>Заголовок объекта</summary>
      public string Caption
      {
        get => this._caption;
        set => this._caption = value;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(ObjInfoCaptionItem other) => this.Equals((ObjInfoItem) other);

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this.Caption ?? $"ObjectID = {this.ObjectID}";
    }
}
