
// Type: Intermech.Interfaces.IEventLog
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы с журналом событий</summary>
    public interface IEventLog : IDBRecords, IDBSessionable
    {
      /// <summary>Удалить события</summary>
      /// <param name="EventsID">Массив с идентификаторами событий, которые нужно удалить.</param>
      void DeleteEvents(long[] EventsID);

      /// <summary>Очищает журнал событий старше даты fromDate</summary>
      /// <param name="fromDate">Дата, до которой события считаются устаревшими</param>
      void ClearEvents(DateTime fromDate);

      /// <summary>
      /// Переносит записи журнала из оперативной части в таблицу IMS_EVENTLOG_ARC
      /// </summary>
      /// <param name="fromDate">Начиная с этой даты и раньше</param>
      void ArchiveEvents(DateTime fromDate);

      /// <summary>
      /// Получить таблицу со списком событий, соответствующих paramSet.
      /// </summary>
      /// <param name="paramSet">Условия запроса в журнал событий</param>
      /// <param name="translateValues">Если translateValues - true, то поля событий будут расшифрованы (имена пользователей, названия действий и т.п.)</param>
      /// <returns>Таблица со списком подходящих событий</returns>
      DataTable Select(DBRecordSetParams paramSet, bool translateValues);

      /// <summary>Добавить запись в лог-файл.</summary>
      /// <param name="EventStr">Строка, добавляемая в лог-файл.</param>
      /// <param name="TraceLevel">Номер уровня детализации трассировки, начиная с
      /// которого запись попадет в лог-файл.</param>
      /// <param name="TraceFileName">Имя файла трассировки, в который нужно добавить эту
      /// строку. Если пусто, то имя imserver.log. Если путь не указан, то файл
      /// складывается в один каталог с главным файлом трассировки imserver.log.</param>
      /// <returns></returns>
      int AddToTrace(string EventStr, int TraceLevel, string TraceFileName);

      /// <summary>Добавить событие в журнал событий</summary>
      /// <param name="ObjectID">Ид. версии объекта (или -1 если событие не относится к версии)</param>
      /// <param name="RelationID">Ид. связи (или -1 если событие на относится к связи)</param>
      /// <param name="CategoryType">Ид. категории</param>
      /// <param name="CategoryID">Ид. объекта данной категории</param>
      /// <param name="ObjectName">Имя того, к чему относится событие</param>
      /// <param name="Note">Комментарии к событию; Environment.NewLine для перевода строк</param>
      /// <param name="EventType">Тип действия</param>
      /// <param name="AuditType">Тип записи о событии</param>
      /// <returns>Ид. созданной записи о событии</returns>
      long AddEvent(
        long ObjectID,
        long RelationID,
        int CategoryType,
        long CategoryID,
        string ObjectName,
        string Note,
        ActionType EventType,
        EventlogRecordType AuditType);

      /// <summary>
      /// Возвращает хэш с расшифровкой имен событий, генерируемых в системе
      /// </summary>
      /// <returns>Хэш с расшифровкой имен событий, генерируемых в системе</returns>
      Hashtable GetActionNamesHash();

      /// <summary>Настройки регистрации событий</summary>
      EventlogSettings Settings { get; set; }
    }
}
