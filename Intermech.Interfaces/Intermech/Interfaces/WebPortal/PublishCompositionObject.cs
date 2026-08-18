
// Type: Intermech.Interfaces.WebPortal.PublishCompositionObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Информация по публикуемому объекту в составе</summary>
    [Serializable]
    public class PublishCompositionObject : ICloneable, IIncludeTyped
    {
      /// <summary>Идентификатор версии объекта</summary>
      public long ObjectID;
      /// <summary>Идентификатор объекта</summary>
      public long ID;
      /// <summary>Идентификатор типа объекта</summary>
      public int ObjectType;
      /// <summary>Идентификатор связанного объекта</summary>
      public string LinkedGuid;
      /// <summary>Флаг того, что объект опубликован на портале</summary>
      public bool Published;
      /// <summary>Причины попадания в публикуемый набор</summary>
      public string ReasonInfo;
      /// <summary>Заголовок объекта</summary>
      public string Caption;
      /// <summary>
      /// Идентификатор родительского объекта Consts.UnknownObjectID если корневой
      /// </summary>
      public long ProjID;
      /// <summary>Владелец объекта</summary>
      public long OwnerID;
      /// <summary>Владелец объекта</summary>
      public long CheckOutBy;
      /// <summary>Глобальный идентификатор версии объекта</summary>
      public Guid ObjectGuid;
      /// <summary>Признак того, что объект непосредственно публикуется</summary>
      public bool Root;
      /// <summary>Владение текущим узлом</summary>
      public string SiteID;
      /// <summary>Список разрешенных узлов конкретно для этого объекта.</summary>
      public string EnableSites;
      /// <summary>
      /// Список разрешенных узлов конкретно для объектов состава.
      /// </summary>
      public string CompositionEnableSites;

      /// <summary>Тип включения в итоговый список этого объекта</summary>
      public IncludeTypes Include { get; set; }

      public PublishCompositionObject()
      {
      }

      public PublishCompositionObject(
        long objectID,
        long id,
        int objectType,
        IncludeTypes include,
        bool published,
        string linkedGuid,
        string reasonInfo,
        long projId,
        long ownerID,
        long checkOutBy,
        Guid objectGuid,
        string siteID)
      {
        this.ObjectID = objectID;
        this.ID = id;
        this.ObjectType = objectType;
        this.Include = include;
        this.Published = published;
        this.LinkedGuid = linkedGuid;
        this.ReasonInfo = reasonInfo;
        this.ProjID = projId;
        this.OwnerID = ownerID;
        this.ObjectGuid = objectGuid;
        this.SiteID = siteID;
        this.CheckOutBy = checkOutBy;
        this.EnableSites = string.Empty;
        this.Root = false;
        this.CompositionEnableSites = string.Empty;
      }

      public object Clone()
      {
        return (object) new PublishCompositionObject(this.ObjectID, this.ID, this.ObjectType, this.Include, this.Published, this.LinkedGuid, this.ReasonInfo, this.ProjID, this.OwnerID, this.CheckOutBy, this.ObjectGuid, this.SiteID)
        {
          EnableSites = this.EnableSites,
          CompositionEnableSites = this.CompositionEnableSites
        };
      }
    }
}
