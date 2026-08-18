
// Type: Intermech.Interfaces.ObjectsClassifyHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс с функциями для облегчения работы с классификацией объектов
    /// </summary>
    public class ObjectsClassifyHelper
    {
      /// <summary>Получить тип классификации</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectType">Тип классифицируемого объекта</param>
      /// <returns></returns>
      public static ObjectsClassifyType GetClassifierType(IUserSession session, int objectType)
      {
        if (!(session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
          return ObjectsClassifyType.None;
        IDBObject containerForObjectType = customService.GetContainerForObjectType((object) session.SessionGUID, objectType);
        if (containerForObjectType == null)
          return ObjectsClassifyType.None;
        IDBAttribute byGuid = containerForObjectType.Attributes.FindByGUID(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
        return byGuid == null || byGuid.Value == null ? ObjectsClassifyType.None : (ObjectsClassifyType) Convert.ToInt32(byGuid.Value);
      }

      /// <summary>Установить тип классификации</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="objectType">Тип классифицируемого объекта</param>
      /// <param name="newType"></param>
      /// <returns></returns>
      public static bool SetClassifierType(
        IUserSession session,
        int objectType,
        ObjectsClassifyType newType)
      {
        if (!(session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
          return false;
        customService.GetContainerForObjectType((object) session.SessionGUID, objectType, true).Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad001d9-306c-11d8-b4e9-00304f19f545"), false).Value = (object) newType;
        return true;
      }
    }
}
