
// Type: Intermech.Interfaces.IServerCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс к серверному кэшу данных</summary>
    public interface IServerCache
    {
      /// <summary>
      /// Возвращает дату и время последней модификации метаданных
      /// </summary>
      DateTime LastMetadataModify { get; }

      /// <summary>
      /// Возвращает список таблиц с метаданными, которые были модифицированы после modifyDate
      /// </summary>
      string[] GetModifiedTables(DateTime modifyDate);

      /// <summary>
      /// Перечитывает кэш из базы данных (требует админских прав)
      /// </summary>
      void Reload();

      /// <summary>Возвращает таблицы серверного кэша</summary>
      /// <param name="tableNames">Список имен таблиц</param>
      /// <returns></returns>
      DataTable[] GetTables(params string[] tableNames);

      /// <summary>
      /// Возвращает информацию об последнем обновлении таблиц серверного кэша
      /// </summary>
      /// <returns></returns>
      DataTable GetTablesModifyTime();

      /// <summary>Возвращает список кэшируемых таблиц</summary>
      /// <returns></returns>
      string[] GetTableNames();

      /// <summary>
      /// Метод возвращает массив идентификаторов общих и персональных файловых прототипов, назначенных на тип объектов objectTypeID
      /// </summary>
      long[] GetFilePrototypes(int objectTypeID);

      /// <summary>
      /// Возвращает список пользователей в виде массива (ид.юзера, его гуид, заголовок)
      /// </summary>
      /// <returns></returns>
      Tuple<long, Guid, string>[] GetUsersCache();

      /// <summary>Читает из базы свежие настройки юзера + общие</summary>
      /// <returns></returns>
      DataTable GetConfigurations();
    }
}
