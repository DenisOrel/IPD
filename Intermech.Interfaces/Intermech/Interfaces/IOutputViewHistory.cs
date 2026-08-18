
// Type: Intermech.Interfaces.IOutputViewHistory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для получения истории вывода сообщений.
    /// </summary>
    public interface IOutputViewHistory
    {
      /// <summary>Возвращает историю вывода сообщений по категориям.</summary>
      /// <returns>Список пар вида (категория, сообщения категории)</returns>
      List<Tuple<string, string>> GetOutputHistory();
    }
}
