
// Type: Intermech.Interfaces.Contexts.IDBEditingContextsObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Интерфейс обработчика объектов типа "Контекст редактирования"
    /// </summary>
    public interface IDBEditingContextsObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
    {
      /// <summary>
      /// Номер взаимосвязанного контекста
      /// Внимание - свойство не кэшируется, а каждый раз загружается из атрибута
      /// </summary>
      long LinkedContextNumber { get; set; }

      /// <summary>
      /// Идентификатор версии объекта-контекста (значение идентично ObjectID)
      /// </summary>
      long ContextID { get; }

      /// <summary>
      /// Проверить, является ли текущий объект упрощённым контекстом редактирования
      /// (не меняет содержимое номера группы изенений у контекстных объектов, не может
      /// быть связанным, допускает применение в своём содержимом версий объектов, принадлежащих
      /// другим контекстам редактирования)
      /// </summary>
      bool SimpleContext { get; }

      /// <summary>Добавить версию объекта в контекст</summary>
      /// <param name="fID">Идентификатор объекта</param>
      /// <param name="versionID">Версия объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - версия была добавлена, false - версия уже была в контексте</returns>
      bool AddVersionID(long fID, long versionID, bool exceptIfFail);

      /// <summary>Удалить версию объекта из контекста редактирования</summary>
      /// <param name="versionID">Идентификатор удаляемой версии объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteFromContext(long versionID, bool exceptIfFail, bool clearModifiationID);

      /// <summary>Удалить объект из контекста редактирования</summary>
      /// <param name="fID">Идентификатор удаляемого объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteObjectFromContext(long fID, bool exceptIfFail, bool clearModifiationID);

      /// <summary>
      /// Найти объект в контексте. При необходимости
      /// проверяюся все связанные контексты (с тем же идентификатором изменений)
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <param name="checkLinked">true - проверять также наличие объекта в связанных контекстах</param>
      /// <returns>Описатель найденного объекта или null</returns>
      EditingContextsObjectVersion FindObjectByVersionID(long versionID, bool checkLinked);

      /// <summary>
      /// Найти объект в контексте. При необходимости
      /// проверяюся все связанные контексты (с тем же идентификатором изменений)
      /// </summary>
      /// <param name="fID">Искомый объект</param>
      /// <param name="checkLinked">true - проверять также наличие объекта в связанных контекстах</param>
      /// <returns>Описатель найденного объекта или null</returns>
      EditingContextsObjectVersion FindObjectByID(long fID, bool checkLinked);

      /// <summary>
      /// Проверить наличие версии объекта в контексте. При необходимости
      /// проверяются все связанные контексты (с тем же идентификатором изменений)
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <param name="checkLinked">true - проверять также наличие версии объекта в связанных контекстах</param>
      /// <returns>true - версия найдена в контексте</returns>
      bool ExistsVersionID(long versionID, bool checkLinked);

      /// <summary>
      /// Проверить наличие объекта в контексте. При необходимости
      /// проверяются все связанные контексты (с тем же идентификатором изменений)
      /// </summary>
      /// <param name="fID">Искомый объект</param>
      /// <param name="checkLinked">true - проверять также наличие объекта в связанных контекстах</param>
      /// <returns>true - объект найден в контексте</returns>
      bool ExistsObject(long fID, bool checkLinked);

      bool ExistsObject(long fID, bool checkLinked, bool useCache);

      /// <summary>
      /// Список идентификаторов версий объектов, которые задействованы в контексте редактирования.
      /// Внимание - список формируется динамически при каждом обращении.
      /// <param name="includeLinked">true - добавить в список версии из всех связанных контекстов</param>
      /// </summary>
      List<EditingContextsObjectVersion> GetObjectsID(bool includeLinked);

      /// <summary>
      /// Получить контекст редактирования.
      /// В объект попадает информация о связанных контекстах (с таким же значением атрибута ModificationID)
      /// </summary>
      /// <param name="withDescriptions">true - загружать описания каждой версии и контекстов, иначе только содержимое контекста</param>
      /// <param name="useCache">Если withDescriptions = true, можно попросить кэшированное значение</param>
      /// <returns>Контекст редактирования</returns>
      EditingContextsObjectContainer GetEditingContextsObjectContainer(
        bool withDescriptions,
        bool useCache);

      /// <summary>
      /// Выполнить замену указанной старой версии объекта в контексте на новую версию
      /// </summary>
      /// <param name="oldVersionID">Старая версия объекта</param>
      /// <param name="newfID">Идентификатор нового объекта</param>
      /// <param name="newVersionID">Новая версия объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - замена успешно выполнена, false - версия oldVersionID не была найдена, newVersionID - добавлена</returns>
      bool ReplaceVersionID(long oldVersionID, long newfID, long newVersionID, bool exceptIfFail);

      /// <summary>
      /// Сбросить содержимое кэша (если требуется, чтобы обработчик перечитал кэшированные свойства
      /// при обращении к ним)
      /// </summary>
      void ResetCache();

      /// <summary>
      /// Отыскать контекст, в котором для указанного пользователя объявлена указанная версия
      /// </summary>
      /// <param name="versions">Список версий</param>
      /// <param name="versionID">Искомая версия</param>
      /// <param name="userID">Пользователь, для которого выполняется проверка</param>
      /// <returns>Идентификатор версии контекста или Intermech.Consts.UnknownObjectId</returns>
      long GetVersionContextID(
        List<EditingContextsObjectVersion> versions,
        long versionID,
        long userID);
    }
}
