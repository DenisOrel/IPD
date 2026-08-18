
// Type: Intermech.Interfaces.Compositions.ObjInfoItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Класс - описание объекта</summary>
    [Serializable]
    public class ObjInfoItem : TypedInfoItem, IEquatable<ObjInfoItem>
    {
      /// <summary>Конструктор</summary>
      public ObjInfoItem()
        : this((IDBObject) null)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="dbObject"></param>
      public ObjInfoItem(IDBObject dbObject)
        : base(0L)
      {
        if (dbObject == null)
          return;
        this.ItemID = dbObject.ObjectID;
        this.ItemTypeID = dbObject.ObjectType;
      }

      /// <summary>Конструктор</summary>
      /// <param name="typedInfoItem">Описание объекта</param>
      public ObjInfoItem(TypedInfoItem typedInfoItem)
        : base(0L)
      {
        if (!(typedInfoItem != (TypedInfoItem) null))
          return;
        this.ItemID = typedInfoItem.ItemID;
        this.ItemTypeID = typedInfoItem.ItemTypeID;
      }

      /// <summary>Конструктор</summary>
      /// <param name="objectId">Ид. версии объекта</param>
      /// <param name="objTypeId">Ид. типа объекта</param>
      public ObjInfoItem(long objectId, int objTypeId = -1)
        : base(objectId, objTypeId)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objInfo"></param>
      public virtual void CopyFrom(QuickObjectInfo objInfo)
      {
        this.ItemID = objInfo.ObjectID;
        this.ItemTypeID = objInfo.ObjectTypeID;
      }

      /// <summary>Ид. версии объекта</summary>
      /// <remarks>Для совместимости со старым кодом</remarks>
      public long ObjectID
      {
        [DebuggerStepThrough] get => this.ItemID;
        [DebuggerStepThrough] set => this.ItemID = value;
      }

      /// <summary>Ид. типа объекта</summary>
      /// <remarks>Для совместимости со старым кодом</remarks>
      public int ObjTypeID
      {
        [DebuggerStepThrough] get => this.ItemTypeID;
        [DebuggerStepThrough] set => this.ItemTypeID = value;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public virtual bool Equals(ObjInfoItem other) => this.CompareTo((object) other) == 0;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsEmpty(ITypedInfoItem value)
      {
        return value == null || value.ItemID == 0L || value.ItemID == -1L;
      }
    }
}
