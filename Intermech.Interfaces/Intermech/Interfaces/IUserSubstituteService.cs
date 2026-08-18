
// Type: Intermech.Interfaces.IUserSubstituteService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сервис для работы с исполнением обязанностей пользователей
    /// </summary>
    public interface IUserSubstituteService
    {
      /// <summary>
      /// Получить всех исполняющих обязанности указанного пользователя
      /// </summary>
      /// <param name="sessionGUID">Сессия</param>
      /// <param name="userId">Пользователь, замы которого нужны</param>
      /// <returns>Список исполняющих обязанности пользователя</returns>
      List<UserSubstitute> GetUserSubstitutes(Guid sessionGUID, long userId);

      /// <summary>
      /// Получить настройки исполнения обязанностей для пользователя по Caption пользователя.
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="userCaption"></param>
      /// <returns></returns>
      List<ObjectIOSettings> GetUsersIOSettings(Guid sessionGUID, string userCaption);

      /// <summary>
      /// Сохранить настройки исполнения обязанностей для пользователя
      /// </summary>
      /// <param name="sessionGUID">Сессия</param>
      /// <param name="ioSettings">Настройки исполненния обязанностей</param>
      /// <param name="userId">ИД пользователя, для которого сохраняем</param>
      /// 
      ///             Возвращает список новых ИДшников настроек
      List<long> SaveIoSettings(Guid sessionGuid, List<ObjectIOSettings> ioSettings, long userId);
    }
}
