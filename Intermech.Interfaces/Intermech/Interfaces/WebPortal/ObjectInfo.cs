
// Type: Intermech.Interfaces.WebPortal.ObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Структура с главной инфой по объекту</summary>
    public class ObjectInfo
    {
      /// <summary>Глобальный идентификатор объекта</summary>
      public Guid Guid;
      /// <summary>Глобальный идентификатор версии объекта</summary>
      public Guid ObjectGuid;
      /// <summary>Глобальный идентификатор родительской версии</summary>
      public Guid ParentGuid;
      /// <summary>Глобальный идентификатор типа объектов</summary>
      public Guid ObjectTypeGuid;
      /// <summary>Название типа объектов</summary>
      public string ObjTypeName;
      /// <summary>Краткое наименование типа объектов</summary>
      public string ObjTypeShortName;
      /// <summary>Название объектов</summary>
      public string ObjInstanceName;
      /// <summary>Расширение файлов типа объектов (для документов)</summary>
      public string DocFileExt;
      /// <summary>Публикуемый тип объектов</summary>
      public Guid PublishObjectType;
      /// <summary>Владелец объекта</summary>
      public Guid OwnerGuid;
      /// <summary>Шаг ЖЦ</summary>
      public Guid LCStep;
      /// <summary>Список предметных областей</summary>
      public Guid LCLevel;
      /// <summary>Глобальный идентификатор проекта</summary>
      public Guid ProjectGuid;
      /// <summary>Дата создания</summary>
      public DateTime CreateDate;
      /// <summary>Глобальный идентификатор связанного объекта</summary>
      public Guid LinkedGuid;
      /// <summary>Заголовок</summary>
      public string Caption;
      /// <summary>Признак базовой версии</summary>
      public bool BaseVersion;
      /// <summary>Номер изменения</summary>
      public int VerCode;
      /// <summary>Уровень доступа</summary>
      public int Access;
      /// <summary>Корневой тип</summary>
      public PublishObjectRootType RootType;
      /// <summary>Номер изменения</summary>
      public long ModificationID;
      /// <summary>Создатель объекта</summary>
      public Guid CreatorGuid;

      public ObjectInfo()
      {
        this.Guid = Guid.Empty;
        this.ObjectGuid = Guid.Empty;
        this.ParentGuid = Guid.Empty;
        this.ObjectTypeGuid = Guid.Empty;
        this.ObjTypeName = string.Empty;
        this.ObjTypeShortName = string.Empty;
        this.ObjInstanceName = string.Empty;
        this.DocFileExt = string.Empty;
        this.PublishObjectType = Guid.Empty;
        this.OwnerGuid = Guid.Empty;
        this.LCStep = Guid.Empty;
        this.LCLevel = Guid.Empty;
        this.ProjectGuid = Guid.Empty;
        this.CreateDate = DateTime.MinValue;
        this.Caption = string.Empty;
        this.LinkedGuid = Guid.Empty;
        this.BaseVersion = false;
        this.VerCode = -1;
        this.Access = 0;
        this.ModificationID = 0L;
        this.CreatorGuid = Guid.Empty;
      }
    }
}
