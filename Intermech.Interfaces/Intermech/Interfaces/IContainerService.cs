
// Type: Intermech.Interfaces.IContainerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс на сервис работы с контейнерами атрибутов</summary>
    public interface IContainerService
    {
      /// <summary>Получить контейнер атрибутов для типа объекта</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectTypeGuid">GUID типа объекта</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного типа объекта</returns>
      IDBObject GetContainerForObjectType(object session, Guid objectTypeGuid, bool createIfNotExist);

      /// <summary>Получить контейнер атрибутов для типа объекта</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectTypeGuid">GUID типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного типа объекта</returns>
      IDBObject GetContainerForObjectType(object session, Guid objectTypeGuid);

      /// <summary>Получить контейнер атрибутов для типа объекта</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectTypeID">ID типа объекта</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного типа объекта</returns>
      IDBObject GetContainerForObjectType(object session, int objectTypeID, bool createIfNotExist);

      /// <summary>Получить контейнер атрибутов для типа объекта</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectTypeID">ID типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного типа объекта</returns>
      IDBObject GetContainerForObjectType(object session, int objectTypeID);

      /// <summary>
      /// Получить контейнер атрибутов для шага жизненного цикла
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepGuid">GUID шага ЖЦ</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного шага ЖЦ</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ</returns>
      IDBObject GetContainerForLCStep(object session, Guid LCStepGuid, bool createIfNotExist);

      /// <summary>
      /// Получить контейнер атрибутов для шага жизненного цикла
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepGuid">GUID шага ЖЦ</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ</returns>
      IDBObject GetContainerForLCStep(object session, Guid LCStepGuid);

      /// <summary>
      /// Получить контейнер атрибутов для шага жизненного цикла
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepID">ID шага ЖЦ</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного шага ЖЦ</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ</returns>
      IDBObject GetContainerForLCStep(object session, int LCStepID, bool createIfNotExist);

      /// <summary>
      /// Получить контейнер атрибутов для шага жизненного цикла
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepID">ID шага ЖЦ</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ</returns>
      IDBObject GetContainerForLCStep(object session, int LCStepID);

      /// <summary>Получить контейнер атрибутов для уровня продвижения</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCLevelGuid">GUID уровня продвижения</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного уровня продвижения</param>
      /// <returns>Контейнер атрибутов для указанного уровня продвижения</returns>
      IDBObject GetContainerForLCLevel(object session, Guid LCLevelGuid, bool createIfNotExist);

      /// <summary>Получить контейнер атрибутов для уровня продвижения</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCLevelGuid">GUID уровня продвижения</param>
      /// <returns>Контейнер атрибутов для указанного уровня продвижения</returns>
      IDBObject GetContainerForLCLevel(object session, Guid LCLevelGuid);

      /// <summary>Получить контейнер атрибутов для уровня продвижения</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCLevelID">ID уровня продвижения</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при отсутствии его для данного уровня продвижения</param>
      /// <returns>Контейнер атрибутов для указанного уровня продвижения</returns>
      IDBObject GetContainerForLCLevel(object session, int LCLevelID, bool createIfNotExist);

      /// <summary>Получить контейнер атрибутов для уровня продвижения</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCLevelID">ID уровня продвижения</param>
      /// <returns>Контейнер атрибутов для указанного уровня продвижения</returns>
      IDBObject GetContainerForLCLevel(object session, int LCLevelID);

      /// <summary>
      /// Получить контейнер атрибутов для шага ЖЦ и типа объекта
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStep"> guid шага ЖЦ</param>
      /// <param name="ObjectTypeGuid">guid типа объекта</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при его отсутствии</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ и типа объектов</returns>
      IDBObject GetContainerForLCStepObjectType(
        object session,
        Guid LCStep,
        Guid ObjectTypeGuid,
        bool createIfNotExist);

      /// <summary>
      /// Получить контейнер атрибутов для шага ЖЦ и типа объекта
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStep"> guid шага ЖЦ</param>
      /// <param name="ObjectTypeGuid">guid типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ и типа объектов</returns>
      IDBObject GetContainerForLCStepObjectType(object session, Guid LCStep, Guid ObjectTypeGuid);

      /// <summary>
      /// Получить контейнер атрибутов для шага ЖЦ и типа объекта
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepID"> ID шага ЖЦ</param>
      /// <param name="ObjectTypeID">ID типа объекта</param>
      /// <param name="createIfNotExist">Создавать новый контейнер при его отсутствии</param>
      /// <returns></returns>
      IDBObject GetContainerForLCStepObjectType(
        object session,
        int LCStepID,
        int ObjectTypeID,
        bool createIfNotExist);

      /// <summary>
      /// Получить контейнер атрибутов для шага ЖЦ и типа объекта
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepID"> id шага ЖЦ</param>
      /// <param name="ObjectTypeID">id типа объекта</param>
      /// <returns>Контейнер атрибутов для указанного шага ЖЦ и типа объектов</returns>
      IDBObject GetContainerForLCStepObjectType(object session, int LCStepID, int ObjectTypeID);

      /// <summary>
      ///  Удалить контейнер атрибутов для типа объектов и шага жц
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepGuid">guid шага ЖЦ</param>
      /// <param name="ObjectTypeGuid">guid типа объекта</param>
      void DeleteContainerForLCStepObjectType(object session, Guid LCStepGuid, Guid ObjectTypeGuid);

      /// <summary>Удалить контейнер атрибутов для типа объектов</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectTypeGuid">guid типа объекта</param>
      void DeleteContainerForObjectType(object session, Guid objectTypeGuid);

      /// <summary>Удалить контейнер атрибутов для уровня продвижения</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCLevelGuid">guid уровня продвижения</param>
      void DeleteContainerForLCLevel(object session, Guid LCLevelGuid);

      /// <summary>Удалить контейнер атрибутов для шага ЖЦ</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="LCStepGuid">guid уровня продвижения</param>
      void DeleteContainerForLCStep(object session, Guid LCStepGuid);

      /// <summary>Перечитать кэш контейнеров</summary>
      /// <param name="userSession">Пользовательская сессия</param>
      void ReloadCache(IUserSession userSession);
    }
}
