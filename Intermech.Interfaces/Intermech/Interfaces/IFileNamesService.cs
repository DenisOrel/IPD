
// Type: Intermech.Interfaces.IFileNamesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы для работы с именами файлов
    /// </summary>
    public interface IFileNamesService
    {
      /// <summary>
      /// Проверяет имя файла fileName на уникальность. Если имя уникально - возвращает его. Если не уникально -
      /// модифицирует имя до уникального значения и возвращает новое имя. id - идентификатор объекта или связи,
      /// которой принадлежит файл.
      /// </summary>
      string GetUniqueFileName(string fileName, long id, Guid sessionGuid);

      /// <summary>
      /// Функция возвращает идентификатор объекта по имени файла fileName, который этому объекту принадлежит.
      /// Если файл объекту не принадлежит возвращает Consts.NoObject.
      /// </summary>
      long GetIDByFileName(string fileName, Guid sessionGuid);

      /// <summary>
      /// Функция возвращает массив идентификаторов версий объекта по имени файла fileName, который этим версиям принадлежит.
      /// Если файл объектам не принадлежит, то возвращается массив нулевой длины
      /// </summary>
      long[] GetObjectIDByFileName(string fileName, Guid sessionGuid);

      /// <summary>
      /// Возвращает таблицу принадлежности файлов с именами fileName различным версиям объектов.
      /// Таблица ведётся только для атрибута Файл.
      /// </summary>
      DataTable GetFileNameTable(string[] fileName, Guid sessionGuid);

      /// <summary>
      /// Возвращает таблицу файла fileName различным версиям объектов.
      /// Таблица ведётся только для атрибута Файл.
      /// </summary>
      DataTable GetFileNameTable(string fileName, Guid sessionGuid);

      /// <summary>
      /// Возвращает таблицу с характеристиками файлов, принадлежащий версиям объектов из массива objectID
      /// </summary>
      /// <param name="objectIDs">Список версий объектов или идентификаторов связей или итераций</param>
      /// <param name="sessionGuid">Guid сессии пользователя</param>
      /// <returns>Таблица с харакетистиками файлов (имена, даты модификации, размеры и пр.)</returns>
      DataTable GetFilesTable(long[] objectIDs, Guid sessionGuid);

      /// <summary>
      /// Возвращает новый уникальный идентификатор для именования файлов
      /// </summary>
      /// <param name="sessionGuid">Гуид пользовательской сессии</param>
      /// <returns>Число, уникальное в пределах базы данных</returns>
      long GetNextFileID(Guid sessionGuid);
    }
}
