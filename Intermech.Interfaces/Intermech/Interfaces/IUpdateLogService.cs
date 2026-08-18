
// Type: Intermech.Interfaces.IUpdateLogService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IUpdateLogService
    {
      /// <summary>Список фильтров записей в лог-файле.</summary>
      string[] Filters { get; }

      /// <summary>
      /// Записи об ошибках автообновления при последнем запуске сервера приложений
      /// </summary>
      string[] GetLastUpdateLog(bool filtered);

      /// <summary>Добавить строку фильтра записей в лог-файле.</summary>
      /// <param name="filter"></param>
      int AddLogFilter(string filter);

      /// <summary>Удалить строку фильтра записей в лог-файле.</summary>
      /// <param name="filter"></param>
      bool RemoveLogFilter(string filter);

      /// <summary>Редактировать строку фильтра записей в лог-файле.</summary>
      int EditLogFilter(string oldFilter, string newFilter);

      /// <summary>
      /// Удалить строки, по которым необходимо фильтровать записи об ошибках автообновления в лог-файл.
      /// </summary>
      bool ClearLogFilters();
    }
}
