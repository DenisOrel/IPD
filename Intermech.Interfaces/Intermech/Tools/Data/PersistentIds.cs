
// Type: Intermech.Tools.Data.PersistentIds
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools.Data
{
    /// <summary>
    /// Утилиты обмена идентификаторами объектов IPS с внешними приложениями.
    /// </summary>
    public static class PersistentIds
    {
      /// <summary>
      /// Преобразует глобальный идентификатор версии объекта IPS в постоянный идентификатор,
      /// пригодный для использования вне IPS.
      /// </summary>
      /// <param name="objectVersionGuid">Глобальный идентификатор версии объекта</param>
      /// <returns>Постоянный идентификатор</returns>
      public static string FromObjectVersion(Guid objectVersionGuid) => objectVersionGuid.ToString("B");
    }
}
