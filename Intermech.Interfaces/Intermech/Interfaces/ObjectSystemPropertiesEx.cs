
// Type: Intermech.Interfaces.ObjectSystemPropertiesEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Основные свойства объекта, включая его заголовок</summary>
    [Serializable]
    public class ObjectSystemPropertiesEx : ObjectSystemProperties
    {
      /// <summary>Заголовок объекта</summary>
      public string Caption { get; private set; }

      /// <summary>Узлы информационной системы</summary>
      public string SiteID { get; private set; }

      /// <summary>Уровень продвижения</summary>
      public int AccessLevel { get; private set; }

      /// <summary>Владелец объекта</summary>
      public long OwnerID { get; private set; }

      public ObjectSystemPropertiesEx(IDBObject dBObject)
        : base(dBObject)
      {
        this.Caption = dBObject.Caption;
        this.SiteID = dBObject.SiteID;
        this.AccessLevel = dBObject.AccessLevel;
        this.OwnerID = dBObject.OwnerID;
      }
    }
}
