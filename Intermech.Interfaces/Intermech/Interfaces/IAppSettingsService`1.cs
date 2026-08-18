
// Type: Intermech.Interfaces.IAppSettingsService`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Сервис настроек / параметров</summary>
    public interface IAppSettingsService<T>
    {
      /// <summary>Сохранить настройки в базу</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="settings">настройки</param>
      /// <returns>Reserved</returns>
      bool SaveSettings(Guid sessionGuid, T settings);

      /// <summary>Загрузить настройки из базы</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="settings">настройки</param>
      /// <returns>Reserved</returns>
      bool LoadSettings(Guid sessionGuid, ref T settings);
    }
}
