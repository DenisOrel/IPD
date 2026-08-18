// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.ISearchAPI
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Pdm;

/// <summary>
/// Интерфейс эмулятора SearchAPI для совместимости с предыдущими версиями Search.
/// </summary>
[ComVisible(true)]
[Guid("67DD9751-2794-4867-AC44-33B1629C071C")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface ISearchAPI
{
  /// <summary>
  /// Выполняет холостой вызов, позволяющий клиенту убедится в работоспособности объекта.
  /// </summary>
  /// <returns>Фиктивное значение</returns>
  bool KnockKnock();

  /// <summary>
  /// Ищет объект по art_id.
  /// Запоминает ObjectID изделия - версии найденного объекта - подбор версии производится либо по текущему правилу, либо по текущему имени файла
  /// </summary>
  /// <param name="ArticleID">Идентификатор изделия</param>
  void OpenArticleByID(int ArticleID);

  /// <summary>
  /// Ищет объект по guid.
  /// Запоминает ObjectID изделия - версии найденного объекта - подбор версии производится либо по текущему правилу, либо по текущему имени файла
  /// </summary>
  /// <param name="ArticleGUID">GUID объекта</param>
  void OpenArticleByGuid(string ArticleGUID);

  /// <summary>
  /// Обнуляет ObjectID изделия, открытого ф-цией OpenArticle().
  /// </summary>
  void CloseArticle();

  /// <summary>
  /// Возвращает значение атрибута изделия по его имени или Guid-у.
  /// </summary>
  /// <param name="FieldName"></param>
  /// <returns></returns>
  string GetFieldValue_Articles(string FieldName);

  /// <summary>
  /// Возвращает GUID версии изделия по GUID изделия, используя правило подбора версии
  /// </summary>
  /// <param name="guid">GUID изделия(объекта)</param>
  /// <returns>GUID версии изделия</returns>
  string GetArticleVersion(string guid);

  /// <summary>
  /// Ищет документ по doc_id.
  /// Запоминает ObjectID документа - версии найденного объекта - подбор версии производится либо по текущему правилу, либо по текущему имени файла
  /// </summary>
  /// <param name="DocumentID">Идентификатор документа</param>
  /// <returns></returns>
  void OpenDocumentByID(int DocumentID);

  /// <summary>
  /// Ищет документ по guid.
  /// Запоминает ObjectID документа - версии найденного объекта - подбор версии производится либо по текущему правилу, либо по текущему имени файла
  /// </summary>
  /// <param name="DocumentGUID">GUID документа</param>
  /// <returns></returns>
  void OpenDocumentByGuid(string DocumentGUID);

  /// <summary>
  /// Обнуляет ObjectID документа, открытого ф-цией OpenDocument().
  /// </summary>
  void CloseDocument();

  /// <summary>
  /// Возвращает значение атрибута документа по его имени или Guid-у.
  /// </summary>
  /// <param name="FieldName"></param>
  /// <returns>Значение атрибута</returns>
  string GetFieldValue(string FieldName);

  /// <summary>Записывает значение атрибута по его имени</summary>
  /// <param name="FieldName">Имя атрибута</param>
  /// <param name="FieldValue">Значение атрибута</param>
  void SetFieldValue(string FieldName, string FieldValue);

  /// <summary>
  /// Показывает диалог с настройкой параметров текущего документа
  /// </summary>
  void EditParameters();

  /// <summary>Копирует файлы документа в указанный каталог</summary>
  /// <param name="DirName">Каталог, в который нужно скопировать файлы документа</param>
  /// <returns>Полный путь и имя (первого) файла документа</returns>
  string CopyToDir(string DirName);

  /// <summary>Берёт текущий документ на изменение</summary>
  void CheckOut();

  /// <summary>Завершает изменение текущего документа</summary>
  void CheckIn();

  /// <summary>
  /// По полному пути файла возвращает GUID версии документа
  /// </summary>
  /// <param name="filename">Полный путь файла</param>
  /// <returns>GUID версии документа</returns>
  string GetDocVersionByFileName(string filename);

  /// <summary>
  /// Создает в архиве новый файловый документ и возвращает его GUID
  /// </summary>
  /// <param name="FileName">Путь + Имя файла</param>
  /// <param name="ObjectTypeGuid">GUID типа документов</param>
  /// <param name="ObjectGuid">DUID версии документа</param>
  /// <returns></returns>
  string CreateFileDocument(string FileName, string ObjectTypeGuid, string ObjectGuid);

  /// <summary>
  /// Генерирует уникальное имя файла для новых документов Search.
  /// </summary>
  /// <param name="Prefix"></param>
  /// <param name="Extention"></param>
  /// <returns></returns>
  string GenerateFileName(string Prefix, string Extention);

  /// <summary>
  /// Получает у сервера список ид. изделий, выпускаемых по указанному документу.
  /// </summary>
  /// <param name="DocumentID">Идентификатор документа</param>
  void OpenDocArticlesByID(int DocumentID);

  /// <summary>
  /// Получает у сервера список ид. изделий, выпускаемых по указанному документу.
  /// </summary>
  /// <param name="DocumentGUID"></param>
  void OpenDocArticlesByGuid(string DocumentGUID);

  /// <summary>
  /// Возвращает количество объектов, выпускаемых по указанному документу.
  /// </summary>
  /// <returns></returns>
  int GetArticlesCount();

  /// <summary>Возвращает ArtID i-ого изделия</summary>
  /// <returns>Идентификаторо изделия</returns>
  string GetDocArticleID(int Index);

  /// <summary>
  /// Закрывает список ид. изделий, выпускаемых по указанному документу.
  /// </summary>
  void CloseDocArticles();

  /// <summary>Создание спецификации</summary>
  /// <param name="DrawingFileName">Имя файла-чертежа</param>
  /// <param name="SettingsForImportFileName">Путь к файлу + имя файла в котором содержатся настройки формата файла для передачи структуры в AVS из CAD систем</param>
  /// <param name="SettingsForExportFileName">Путь к файлу + имя файла в котором содержатся настройки формата файла для передачи структуры из AVS в CAD системы</param>
  /// <param name="Structure">Строка, каждая из которых содержит параметры изделия входящего в состав сборочной единицы </param>
  /// <param name="PassportData">Строка с паспортными данными сборочного чертежа (для формирования параметров сборочной единицы)</param>
  string CreateSpecification(
    string DrawingFileName,
    string SettingsForImportFileName,
    string SettingsForExportFileName,
    string Structure,
    string PassportData);

  /// <summary>Получить позиции</summary>
  /// <param name="DrawingFileName">Имя файла-чертежа</param>
  /// <param name="SettingsForExportFileName">Путь к файлу + имя файла в котором содержатся настройки формата файла для передачи структуры из AVS в CAD системы</param>
  /// <returns></returns>
  string GetPositions(string DrawingFileName, string SettingsForExportFileName);

  /// <summary>
  /// Подготовка к отображению диалога выбора документов (заглушка для совместимости в предыдущими версиями)
  /// </summary>
  void StartSelectDocs();

  /// <summary>Показывает диалог выбора документов</summary>
  void SelectDocs();

  /// <summary>
  /// Возвращает количество документов, выбранных пользователем.
  /// </summary>
  /// <returns></returns>
  int SelectedDocsCount();

  /// <summary>
  /// Возвращает GUID объекта по порядковому номеру в списке выбранных документов.
  /// </summary>
  /// <param name="Index"></param>
  /// <returns></returns>
  string GetSelectedDocID(int Index);

  /// <summary>Завершение выбора документов</summary>
  void EndSelectDocs();

  /// <summary>
  /// Подготовка к отображению диалога выбора изделий (заглушка для совместимости в предыдущими версиями)
  /// </summary>
  void StartSelectArticles();

  /// <summary>Показывает диалог выбора изделий</summary>
  void SelectArticles();

  /// <summary>
  /// Возвращает количество изделий, выбранных пользователем.
  /// </summary>
  /// <returns></returns>
  int SelectedArticlesCount();

  /// <summary>
  /// Возвращает GUID объекта по порядковому номеру в списке выбранных изделий.
  /// </summary>
  /// <param name="Index"></param>
  /// <returns></returns>
  string GetSelectedArticleID(int Index);

  /// <summary>Завершение выбора изделий</summary>
  void EndSelectArticles();

  /// <summary>Возвращает номер версии API</summary>
  /// <returns></returns>
  int GetVersion();

  /// <summary>Получает ArtID по DocID.</summary>
  /// <param name="DocumentID"></param>
  /// <returns></returns>
  int GetArtId_byDocId(int DocumentID);

  /// <summary>Получает Guid изделия по Guid документа.</summary>
  /// <param name="DocumentGuid"></param>
  /// <returns></returns>
  string GetArtGuid_byDocGuid(string DocumentGuid);

  /// <summary>
  /// Идентификатор пользователя, взявшего текущий документ на изменение.
  /// Если документ на изменение не взят, возвращает 0.
  /// </summary>
  /// <returns></returns>
  long GetDocStatus();

  /// <summary>
  /// Идентификатор пользователя, вошедшего в ... на данном компьютере
  /// </summary>
  /// <returns></returns>
  long GetUserId();

  /// <summary>Возвращает имя пользователя по его идентификатору</summary>
  /// <param name="UserID"></param>
  /// <returns></returns>
  string GetUserFullName_ByUserID(long UserID);

  /// <summary>
  /// Возвращает идентификатор типа документа по его наименованию
  /// </summary>
  /// <param name="DocTypeName"></param>
  /// <returns></returns>
  int GetDocTypeByDocTypeName(string DocTypeName);

  /// <summary>
  /// Возвращает имя файла документа и место хранения его рабочей копии.
  /// </summary>
  /// <param name="DocumentGUID"></param>
  /// <returns></returns>
  string GetDocFileName(string DocumentGUID);

  /// <summary>
  /// Возвращает инвентарный номер документа по полному имени его файла.
  /// </summary>
  /// <param name="FileName"></param>
  /// <returns></returns>
  int GetDocID_byFileName(string FileName);

  /// <summary>
  /// Возвращает глобальный идентификатор документа по полному имени его файла.
  /// </summary>
  /// <param name="fullPath">Полное имя файла документа</param>
  /// <returns>Глобальный идентификатор документа (не версии, а документа)</returns>
  string GetDocGuid_byFileName(string fullPath);

  /// <summary>
  /// Возвращает наименование типа документов по идентификатору типа документов.
  /// </summary>
  /// <param name="DocTypeID"></param>
  /// <returns></returns>
  string GetDocTypeNameInDocs(int DocTypeID);

  /// <summary>Изменяет тип текущего документа</summary>
  /// <param name="DocTypeName"></param>
  void SetDocType(string DocTypeName);

  /// <summary>Получить путь к рабочему каталогу службы инструментов</summary>
  /// <returns></returns>
  string GetWorkFolder();

  /// <summary>
  /// Возвращает код состояния выполнения последней ф-ции API
  /// </summary>
  /// <returns></returns>
  int ErrorCode();

  /// <summary>
  /// Возвращает текст сообщения ошибки выполнения последней ф-ции API
  /// </summary>
  /// <returns></returns>
  string ErrorMessage();

  /// <summary>
  /// Возвращает код состояния выполнения последней ф-ции API
  /// Дублируем ErrorCode специально для CADMECH-T по просьбе Франца
  /// </summary>
  /// <returns></returns>
  int GetErrorCode();

  /// <summary>
  /// Возвращает текст сообщения ошибки выполнения последней ф-ции API
  /// Дублируем ErrorMessage специально для CADMECH-T по просьбе Франца
  /// </summary>
  /// <returns></returns>
  string GetErrorMessage();

  /// <summary>
  /// Включает и выключает режит показа всех исключительных ситуаций, происходящих в SearchAPI.
  /// </summary>
  /// <param name="state"></param>
  void DisplayErrors(bool state);

  /// <summary>
  /// Возвращает или задает идентификатор используемого интегратора CAD-системы. Если идентификатор не задан (значение свойства == Guid.Empty),
  /// то интегратор будет определен автоматически при первом обращении к нему.
  /// </summary>
  Guid IntegratorId { get; set; }

  /// <summary>Показывает диалог выбора документов и/или изделий</summary>
  string[] SelectObjects(bool allowDocuments, bool allowArticles);

  /// <summary>Запись значения атрибута изделия</summary>
  /// <param name="fieldGuidOrName">Наименование атрибута</param>
  /// <param name="fieldValue">Значение атрибута</param>
  /// 
  ///             Victor
  void SetFieldValue_Articles(string fieldGuidOrName, string fieldValue);

  /// <summary>
  /// Возвращает GUID текущего выбранного в IPS объекта. Должен быть выбран только один объект.
  /// </summary>
  /// <returns>IDBObject.GUID или пустая строка</returns>
  string ActiveArtGUID();

  /// <summary>
  /// Возвращает ObjectID объекта, выделенного в интерфейсе IPS
  /// </summary>
  /// <returns>ObjectID или -1 если нет выделенного объекта или их выделено больше 1</returns>
  int ActiveArtID();
}
