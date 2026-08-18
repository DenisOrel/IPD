
// Type: Intermech.Interfaces.Compositions.ITypedInfoService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Спец. сервис для работы с TypedInfoItem</summary>
    /// <remarks>Т.к. некоторые операции можно проводить только не сервере,
    /// вынуждены держать отдельный сервис</remarks>
    public interface ITypedInfoService
    {
      /// <summary>
      /// Обновление / загрузка информации о "недостающих" типах объектов
      /// </summary>
      /// <param name="objInfoList">Описание объектов</param>
      /// <param name="usrSession">Пользовательская сессия</param>
      /// <returns>Коллекция объектов, для которых успешно загружена информация о типах</returns>
      List<ObjInfoItem> UpdateUnknownTypes(IEnumerable<ObjInfoItem> objInfoList, object usrSession);

      /// <summary>
      /// Обновление / загрузка информации о "недостающих" типах, идентификаторах объектов
      /// </summary>
      /// <param name="objInfoList">Описание объектов</param>
      /// <param name="usrSession">Пользовательская сессия</param>
      /// <returns>Коллекция объектов, для которых успешно загружена недостающая информация</returns>
      List<ObjInfoItem> UpdateUnknownInfo(IEnumerable<ObjInfoItem> objInfoList, object usrSession);
    }
}
