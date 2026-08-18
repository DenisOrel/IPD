
// Type: Intermech.Interfaces.ObjectRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, описывающая объект (используется при перекачке данных)
    /// </summary>
    [Serializable]
    public class ObjectRecord
    {
      /// <summary>Идентификатор версии объекта</summary>
      public long Object_id;
      /// <summary>GUID версии объекта</summary>
      public object ObjectGuid;
      /// <summary>Идентификатор объекта</summary>
      public long Id;
      /// <summary>GUID объекта</summary>
      public object IdGuid;
      /// <summary>Идентификатор шага жизненного цикла</summary>
      public int Lc_step;
      /// <summary>Номер версии объекта</summary>
      public int VersionId;
      /// <summary>ID родительской версии объекта</summary>
      public long ParentVersionId = -1;
      /// <summary>
      /// Кем взят на изменение (Идентификатор версии пользователя либо 0 - если архивная копия)
      /// </summary>
      public long ChkoutBy;
      /// <summary>
      /// Кем взят на изменение (GUID версии пользователя либо null - если архивная копия)
      /// </summary>
      public object ChkoutGuid;
      /// <summary>
      ///  признак версии/экземпляра/актуальной версии:
      /// 0 - актуальная версия
      /// 1 - версия
      /// 2 - экземпляр
      /// 3 - партия
      ///  </summary>
      public int ObjectVerType;
      /// <summary>Тип объекта</summary>
      public int ObjectType;
      /// <summary>Идентификатор версии владельца объекта</summary>
      public long OwnerId;
      /// <summary>GUID версии владельца объекта</summary>
      public object OwnerGuid;
      /// <summary>Дата последней модификации</summary>
      public DateTime ModifyDate;
      /// <summary>Уровень продвижения</summary>
      public int LevelId;
      /// <summary>Дата создания</summary>
      public DateTime ObjCreate;
      /// <summary>Заголовок</summary>
      public string Caption;
      /// <summary>Идентификатор проекта</summary>
      public long ProjectId;
      /// <summary>GUID проекта</summary>
      public object ProjectGuid;
      /// <summary>Уровень доступа объекта</summary>
      public int AccessLevel;
      /// <summary>Признак базовой версии</summary>
      public bool IsBaseVersion = true;
      /// <summary>
      /// Если объект опубликован на портале, то содержит
      /// 1) код узла, создавшего объект (обязательно)
      /// 2) код узла, владеющего объектом (может не быть)
      /// </summary>
      public string SiteID;
      /// <summary>Номер группы изменений</summary>
      public long ModificationID;
      /// <summary>Номер группы изменений</summary>
      public object ModificationGuid;
      /// <summary>Номер родительской версии объекта</summary>
      /// <remarks>Используется только при миграции "старых" баз в IPS</remarks>
      public int ParentVersionNo = -1;
      /// <summary>Ид. создателя объекта</summary>
      public long CreatorID;

      public ObjectRecord()
      {
      }

      public ObjectRecord(
        long object_id,
        object objectGuid,
        long id,
        object idGuid,
        int lc_step,
        int versionId,
        long parentVersionId,
        long chkoutBy,
        object chkoutGuid,
        int objectVerType,
        int objectType,
        long ownerId,
        object ownerGuid,
        DateTime modifyDate,
        int levelId,
        DateTime objCreate,
        string caption,
        long projectId,
        object projectGuid,
        int accessLevel,
        bool isBaseVersion,
        string siteID,
        long modificationId,
        object modificationGuid,
        long creatorID)
      {
        this.Object_id = object_id;
        this.ObjectGuid = objectGuid;
        this.Id = id;
        this.IdGuid = idGuid;
        this.Lc_step = lc_step;
        this.VersionId = versionId;
        this.ParentVersionId = parentVersionId;
        this.ChkoutBy = chkoutBy;
        this.ChkoutGuid = chkoutGuid;
        this.ObjectVerType = objectVerType;
        this.ObjectType = objectType;
        this.OwnerId = ownerId;
        this.OwnerGuid = ownerGuid;
        this.ModifyDate = modifyDate;
        this.LevelId = levelId;
        this.ObjCreate = objCreate;
        this.Caption = caption;
        this.ProjectId = projectId;
        this.ProjectGuid = projectGuid;
        this.AccessLevel = accessLevel;
        this.IsBaseVersion = isBaseVersion;
        this.SiteID = siteID;
        this.ModificationID = modificationId;
        this.ModificationGuid = modificationGuid;
        this.CreatorID = creatorID;
      }

      public ObjectRecord(
        long object_id,
        object objectGuid,
        long id,
        object idGuid,
        int lc_step,
        int versionId,
        long chkoutBy,
        object chkoutGuid,
        int objectVerType,
        int objectType,
        long ownerId,
        object ownerGuid,
        DateTime modifyDate,
        int levelId,
        DateTime objCreate,
        string caption,
        long projectId,
        object projectGuid,
        int accessLevel)
        : this(object_id, objectGuid, id, idGuid, lc_step, versionId, -1L, chkoutBy, chkoutGuid, objectVerType, objectType, ownerId, ownerGuid, modifyDate, levelId, objCreate, caption, projectId, projectGuid, accessLevel, true, string.Empty, 0L, (object) null, 0L)
      {
      }
    }
}
