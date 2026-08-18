
// Type: Intermech.Interfaces.Briefcase.IServerBriefcase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Streams;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Интерфейс серверной части портфеля для экспорта-импорта данных. Для работы с его функциями юзер
    /// должен иметь права админа или экспортера/импортера данных.
    /// </summary>
    public interface IServerBriefcase
    {
      /// <summary>Вернуть набор данных с указанными таблицами</summary>
      DataSet GetDataset(Guid sessionGUID, string[] tableNames, bool includeLocalization);

      /// <summary>
      /// Вернуть таблицу tableName по условию condition с сортировкой order
      /// </summary>
      DataTable GetDatatable(Guid sessionGUID, string tableName, string condition, string order);

      /// <summary>Получает интерфейс просмотрщика папок сервера</summary>
      /// <returns></returns>
      IBrowseFolder GetFolderBrowser();

      /// <summary>
      /// Проверяет права экспорта данных, exception при отсутствии прав
      /// </summary>
      void CheckExportRights(Guid sessionGUID);

      /// <summary>
      /// Валидация параметров экспорта.
      /// Если null то все ok, иначе список невалидного хлама из числа входящих параметров
      /// </summary>
      /// <param name="aExportAttributes">Экспортируемые объекты</param>
      /// <returns></returns>
      ExportAttribute[] ValidateExportAttributes(Guid sessionGUID, ExportAttribute[] aExportAttributes);

      /// <summary>
      /// Проверяет права экспорта данных, регистрирует процесс экспорта
      /// </summary>
      Guid StartExport(Guid sessionGUID, BriefcaseExportProperties exportProperties);

      /// <summary>Прекращает экспорт портфеля</summary>
      void CancelExport(Guid briefcaseGuid);

      /// <summary>Инфа с выполнением</summary>
      BriefcaseExportProgress GetExportProgress(Guid briefcaseID);

      /// <summary>
      /// вернуть структуру портфеля -  папки/файлы,
      /// у папок пути относительные \folder; \folder\folder1 ...
      /// у файлов полные пути \folder\file; \folder\folder1\file1 ...
      /// </summary>
      /// <param name="sessionGUID">сугубо для проверки прав</param>
      /// <param name="briefcaseID"></param>
      /// <returns></returns>
      BriefcaseFilesStructure GetBriefcaseFilesStructure(Guid sessionGUID, Guid briefcaseID);

      /// <summary>
      /// передать портфель с сервера на клиент: проверить, окончен ли экспорт по Ok или ошибке, передать и удалить временные файлы.
      /// </summary>
      /// <param name="briefcaseID"></param>
      /// <param name="filePath">имя файла в виде "\folder\folder1\file1", сформированное по BriefcaseFilesStructure из GetBriefcaseStructure</param>
      /// <returns></returns>
      ImFileReader GetBriefcaseFile(Guid sessionGUID, Guid briefcaseID, string filePath);

      /// <summary>Получить лог файл экспорта</summary>
      /// <param name="sessionGUID"></param>
      /// <param name="briefcaseID"></param>
      /// <returns></returns>
      ImFileReader GetExportLog(Guid sessionGUID, Guid briefcaseID);

      /// <summary>
      /// удалить временные данные портфеля на сервере (то есть если портфель создавался для передачи на клиент )
      /// </summary>
      /// <param name="briefcaseID"></param>
      void DisposeBriefcase(Guid briefcaseID);

      long[] GetLinkedObjectVersions(Guid sessionGUID, int category, long[] ids);

      ExportAttribute[] GetLinkedDataByAttribute(
        Guid sessionGUID,
        int category,
        AttributableElements kind,
        long attributableID,
        int attributeId,
        object attrValueOriginal,
        ref object attrValueCurrent);

      /// <summary>Начинает импорт портфеля briefcaseID</summary>
      void StartImport(Guid sessionGUID, Guid briefcaseID);

      /// <summary>Приостанавливает процесс импорта портфеля</summary>
      void PauseImport();

      /// <summary>Прекращает импорт портфеля</summary>
      void CancelImport(Guid briefcaseID);

      /// <summary>Прекращает проверку метаданных портфеля</summary>
      void CancelCheck(Guid briefcaseID);

      /// <summary>
      /// Проверяет закачиваемые метаданные на совместимость с метаданными сервера
      /// </summary>
      /// <param name="briefcaseID"></param>
      /// <param name="MetaData">Все метаданные</param>
      /// <param name="ImportMetaData">Список закачиваемых метаданных</param>
      /// <param name="Synhronized">Синхронизация метаданных портфеля и сервера</param>
      /// <returns>Коллекцию CheckMetadataLogItem</returns>
      void CheckBriefcaseMetadata(
        Guid sessionGUID,
        Guid briefcaseID,
        DataSet MetaData,
        DataSet ImportMetaData,
        CheckOptions options);

      /// <summary>
      /// Проверяет закачиваемые метаданные на совместимость с метаданными сервера
      /// </summary>
      /// <param name="briefcaseID"></param>
      /// <param name="BriefcaseFolder">путь к портфелю с метаданными на сервере</param>
      /// <param name="Synhronized">Синхронизация метаданных портфеля и сервера</param>
      /// <returns>Коллекцию CheckMetadataLogItem</returns>
      void CheckBriefcaseMetadata(
        Guid sessionGUID,
        Guid briefcaseID,
        string BriefcaseFolder,
        CheckOptions options);

      /// <summary>Проверка метаданных</summary>
      /// <param name="MetaData">метаданные на клиенте</param>
      /// <param name="System">проверить только системные</param>
      /// <returns></returns>
      List<CheckMetadataLogItem> CheckMetadata(Guid sessionGUID, DataSet MetaData, bool System);

      /// <summary>Проверка метаданных</summary>
      /// <param name="BriefcaseFolder">путь к портфелю с метаданными на сервере</param>
      /// <param name="System">проверить только системные</param>
      /// <returns></returns>
      List<CheckMetadataLogItem> CheckMetadata(Guid sessionGUID, string BriefcaseFolder, bool System);

      /// <summary>Начало передачи портфеля на сервер</summary>
      /// <param name="briefcaseID">Идентификатор портфеля</param>
      /// <param name="ImportProperties">Свойства импорта</param>
      /// <param name="FileStructure">Массив с PartFile, где лежит инфа от файлах портфеля в куче</param>
      void BriefcaseTransferStart(
        Guid briefcaseID,
        BriefcaseImportProperties ImportProperties,
        BriefcaseFilesStructure FileStructure);

      /// <summary>Шаг передачи портфеля на сервер</summary>
      /// <param name="briefcaseID">Идентификатор портфеля</param>
      /// <param name="Bytes">буфер</param>
      /// <param name="BytesLength">полезный размер буфера</param>
      void BriefcaseTransferStep(Guid briefcaseID, byte[] Bytes, int BytesLength);

      /// <summary>Получить полный путь лога импорта</summary>
      /// <param name="briefcaseID">Идентификатор портфеля</param>
      /// <returns>полный путь лога импорта</returns>
      string GetImportLogPath(Guid sessionGUID, Guid briefcaseID);

      /// <summary>Получить запакованный лог импорта</summary>
      /// <param name="briefcaseID">Идентификатор портфеля</param>
      /// <returns>запакованный лог импорта</returns>
      byte[] GetImportLog(Guid sessionGUID, Guid briefcaseID);

      /// <summary>Инфа с выполнением</summary>
      BriefcaseImportProgress ImportProgress(Guid briefcaseID);

      /// <summary>Дата и время модификации системных таблиц</summary>
      DateTime SystemModifyDate(Guid sessionGUID);
    }
}
