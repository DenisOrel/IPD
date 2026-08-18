
// Type: Intermech.Interfaces.NewRelationProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Структура с параметрами для создания новой связи</summary>
    [Serializable]
    public struct NewRelationProperties
    {
      /// <summary>
      /// Ид. связи-прототипа - из нее будут взяты дополнительные атрибуты для новой связи
      /// </summary>
      public long PrototypeRelationID;
      /// <summary>Ид. версии родительского объекта</summary>
      public long ProjectObjectID;
      /// <summary>Ид. дочернего объекта</summary>
      public long PartID;
      /// <summary>
      /// Дата и время начала действия связи (в локальном времени)
      /// </summary>
      public DateTime BeginDate;
      /// <summary>
      /// Дата и время завершения действия связи (в локальном времени)
      /// </summary>
      [Obsolete]
      public DateTime EndDate;
      /// <summary>Ид. версии дочернего объекта</summary>
      public long PartObjectID;
      /// <summary>
      /// Список значений атрибутов, которые нужно присвоить создаваемой связи
      /// </summary>
      public AttributeValues[] ValuesList;
      /// <summary>
      /// Глобальный идентификатор связи. Если не задан, то генерируется сервером автоматически.
      /// </summary>
      public Guid RelationGUID;
      /// <summary>
      /// Связь-прототип (может содежать null)  НЕЛЬЗЯ ПЕРЕДАВАТЬ С КЛИЕНТА НА СЕРВЕР!!!
      /// </summary>
      public IDBRelation PrototypeRelation;

      public NewRelationProperties(
        IDBRelation prototypeRelation,
        long projectObjectID,
        Guid relationGUID)
        : this(prototypeRelation.RelationID, projectObjectID, prototypeRelation.PartID, prototypeRelation.CreateDate, prototypeRelation.DeleteDate)
      {
        this.PrototypeRelation = prototypeRelation;
        this.RelationGUID = relationGUID;
      }

      public NewRelationProperties(
        long prototypeRelationID,
        long projectObjectID,
        long partID,
        DateTime beginDate,
        DateTime endDate,
        long partObjectID,
        AttributeValues[] valuesList)
      {
        this.PrototypeRelationID = prototypeRelationID;
        this.ProjectObjectID = projectObjectID;
        this.PartID = partID;
        this.BeginDate = beginDate;
        this.EndDate = endDate;
        this.PartObjectID = partObjectID;
        this.ValuesList = valuesList;
        this.RelationGUID = Guid.Empty;
        this.PrototypeRelation = (IDBRelation) null;
      }

      public NewRelationProperties(
        long prototypeRelationID,
        long projectObjectID,
        long partID,
        DateTime beginDate,
        DateTime endDate,
        long partObjectID)
      {
        this.PrototypeRelationID = prototypeRelationID;
        this.ProjectObjectID = projectObjectID;
        this.PartID = partID;
        this.BeginDate = beginDate;
        this.EndDate = endDate;
        this.PartObjectID = partObjectID;
        this.ValuesList = (AttributeValues[]) null;
        this.RelationGUID = Guid.Empty;
        this.PrototypeRelation = (IDBRelation) null;
      }

      public NewRelationProperties(
        long prototypeRelationID,
        long projectObjectID,
        long partID,
        DateTime beginDate,
        DateTime endDate)
      {
        this.PrototypeRelationID = prototypeRelationID;
        this.ProjectObjectID = projectObjectID;
        this.PartID = partID;
        this.BeginDate = beginDate;
        this.EndDate = endDate;
        this.PartObjectID = 0L;
        this.ValuesList = (AttributeValues[]) null;
        this.RelationGUID = Guid.Empty;
        this.PrototypeRelation = (IDBRelation) null;
      }

      public NewRelationProperties(long projectObjectID, long partID)
        : this(0L, projectObjectID, partID, DateTime.MinValue, DateTime.MaxValue)
      {
      }

      public NewRelationProperties(long prototypeRelationID, long projectObjectID, long partID)
        : this(prototypeRelationID, projectObjectID, partID, DateTime.MinValue, DateTime.MaxValue)
      {
      }

      public NewRelationProperties(
        long prototypeRelationID,
        long projectObjectID,
        long partID,
        DateTime beginDate)
        : this(prototypeRelationID, projectObjectID, partID, beginDate, DateTime.MaxValue)
      {
      }

      /// <summary>Возвращает пустую запись</summary>
      public static NewRelationProperties Empty => new NewRelationProperties(0L, 0L);
    }
}
