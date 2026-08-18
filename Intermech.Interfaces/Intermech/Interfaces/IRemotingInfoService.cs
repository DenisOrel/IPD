
// Type: Intermech.Interfaces.IRemotingInfoService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для получения информации о состоянии инфраструктуры remoting на сервере приложений.
    /// </summary>
    public interface IRemotingInfoService
    {
      /// <summary>
      /// Возвращает статистику по объектам, распределенным на сервере приложений.
      /// </summary>
      /// <returns>Список пар вида (имя типа объекта, количество экземпляров объекта)</returns>
      List<Tuple<string, int>> GetMarshalledObjectsStatistics();
    }
}
