
// Type: Intermech.Interfaces.WebPortal.RelationInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Структура с главной инфой по связи</summary>
    public struct RelationInfo
    {
      /// <summary>Глобальный идентификатор связи</summary>
      public Guid Guid;
      /// <summary>Глобальный идентификатор версии Project объекта</summary>
      public Guid ProjectGuid;
      /// <summary>Глобальный идентификатор версии Part объекта</summary>
      public Guid PartGuid;
      /// <summary>Глобальный идентификатор типа связей</summary>
      public Guid RelationTypeGuid;
      /// <summary>Наименование типа связей</summary>
      public string RelationTypeName;
      /// <summary>Дата создания связи</summary>
      public DateTime CreateDate;
      /// <summary>Глобальный идентификатор версии в составе</summary>
      public Guid CompositionVersionGuid;
      /// <summary>Создатель связи</summary>
      public Guid CreatorGuid;
    }
}
