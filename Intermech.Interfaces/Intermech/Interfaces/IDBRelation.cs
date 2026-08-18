
// Type: Intermech.Interfaces.IDBRelation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс связи между объектами</summary>
    public interface IDBRelation : IDBAttributable, IDBSessionable, IPluginsData
    {
      /// <summary>Идентификатор связи (только для чтения)</summary>
      long RelationID { get; }

      /// <summary>
      /// Идентификатор версии объекта-родителя (IDBObject.ObjectID)
      /// </summary>
      long ProjID { set; get; }

      /// <summary>
      /// Идентификатор версии дочернего объекта(IDBObject.ObjectID)
      /// (например, если связь рассматривается в контексте конкретной версии объекта).
      /// Если ид. версии получить не удалось, то возвращает 0.
      /// </summary>
      long PartObjectID { get; }

      /// <summary>Идентификатор дочернего объекта (IDBObject.ID)</summary>
      long PartID { get; }

      /// <summary>Ид. типа связи</summary>
      int RelationType { get; set; }

      /// <summary>Создатель связи</summary>
      long CreatorID { get; }

      /// <summary>
      /// Дата создания связи. Если = DateTime.MinValue, то там null
      /// </summary>
      DateTime CreateDate { get; set; }

      /// <summary>
      /// Дата удаления связи. Если =DateTime.MaxValue, то там null
      /// </summary>
      [Obsolete]
      DateTime DeleteDate { get; }

      /// <summary>Удалить связь</summary>
      /// <param name="DeleteMode">Зарезервировано</param>
      /// <returns>Зарезервировано</returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Уникальный ключ, по которому сервер (сервис IVersionRulesCacheService) определяет настройки фильтрации состава
      /// </summary>
      string FiltrationOwnerID { get; set; }

      /// <summary>Глобальный идентификатор связи</summary>
      Guid GUID { get; set; }

      /// <summary>
      /// Метод заменяет дочерний объект на связи. Заменить можно только на объект с аналогичным типом (или на дочерний тип).
      /// </summary>
      /// <param name="partObjectID">ObjectID дочернего объекта.</param>
      void ReplacePartObject(long partObjectID);

      /// <summary>Обработчик родительского объекта</summary>
      IDBObject ProjObject { get; }

      /// <summary>
      /// Обработчик версии дочернего объекта (может быть null!!!)
      /// </summary>
      IDBObject PartObject { get; }
    }
}
