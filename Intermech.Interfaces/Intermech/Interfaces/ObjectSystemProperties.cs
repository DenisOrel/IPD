
// Type: Intermech.Interfaces.ObjectSystemProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Основные свойства объекта</summary>
    [Serializable]
    public class ObjectSystemProperties
    {
      /// <summary>Тип объекта</summary>
      public int ObjectTypeID { get; private set; }

      /// <summary>Идентификатор версии объекта</summary>
      public long ObjectID { get; private set; }

      /// <summary>Глобальный идентификатор версии объекта</summary>
      public Guid VersionGuid { get; private set; }

      /// <summary>Идентификатор объекта</summary>
      public long ID { get; private set; }

      /// <summary>Ид. шага ЖЦ</summary>
      public int LCStepID { get; private set; }

      /// <summary>Признак базовой версии</summary>
      public bool IsBaseVersion { get; private set; }

      /// <summary>Номер версии (порядковый)</summary>
      public int VersionID { get; private set; }

      /// <summary>Кто взял на изменение</summary>
      public long CheckOutBy { get; private set; }

      /// <summary>Номер группы изменений</summary>
      public long ModificationID { get; private set; }

      public ObjectSystemProperties(IDBObject dBObject)
      {
        this.ObjectTypeID = dBObject.ObjectType;
        this.ObjectID = dBObject.ObjectID;
        this.VersionGuid = dBObject.ObjectGUID;
        this.ID = dBObject.ID;
        this.LCStepID = dBObject.LCStep;
        this.IsBaseVersion = dBObject.IsBaseVersion;
        this.VersionID = dBObject.VersionID;
        this.CheckOutBy = dBObject.CheckoutBy;
        this.ModificationID = dBObject.ModificationID;
      }
    }
}
