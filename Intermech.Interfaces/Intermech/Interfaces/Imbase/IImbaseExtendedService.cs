
// Type: Intermech.Interfaces.Imbase.IImbaseExtendedService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Imbase
{
    /// <summary>Служба расширения Imbase</summary>
    /// <remarks>Для получения в частности информации по справочникам и режимам выбора для
    /// аттрибутов объектов</remarks>
    public interface IImbaseExtendedService
    {
      /// <summary>
      /// Получить список идентификаторов атрибутов и их значения.
      /// </summary>
      /// <param name="objTypeID">Идентификатор тип объекта</param>
      /// <returns>Список идентификаторов атрибутов и их значения</returns>
      Dictionary<int, ImbaseExtendedItem> GetValues(int objTypeID);

      /// <summary>Загрузка настроек.</summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <returns>Результат загрузки данных</returns>
      bool LoadConfigData(Guid sessionGuid);

      /// <summary>Сохранение настроек.</summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <returns>Результат сохранения</returns>
      bool SaveConfigData(Guid sessionGuid);

      /// <summary>Установить значение.</summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <param name="dict">Список идентификаторов атрибутов и их значения</param>
      void SetValues(Guid sessionGuid, int objTypeID, IDictionary<int, ImbaseExtendedItem> dict);

      /// <summary>Получить все данные сервиса</summary>
      /// <returns></returns>
      ImbaseExtendedData GetAllValues();
    }
}
