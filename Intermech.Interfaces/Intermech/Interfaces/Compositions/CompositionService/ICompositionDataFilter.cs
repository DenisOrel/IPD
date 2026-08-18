
// Type: Intermech.Interfaces.Compositions.CompositionService.ICompositionDataFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System.Data;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>Интерфейс для фильтрации результирующего DataTable</summary>
    /// <summary>Позволяет выполнить дополнительные преобразования данных после получения результирующего DataTable.
    /// Может использоваться для фильтрации данных перед отправкой их на клиент</summary>
    public interface ICompositionDataFilter
    {
      /// <summary>Вызов преобразования данных</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="dataTable">Таблица с данными для фильтрации</param>
      /// <returns></returns>
      DataTable Execute([NotNull] IUserSession session, [CanBeNull] DataTable dataTable);
    }
}
