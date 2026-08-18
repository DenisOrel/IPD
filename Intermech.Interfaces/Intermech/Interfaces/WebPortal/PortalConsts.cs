
// Type: Intermech.Interfaces.WebPortal.PortalConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    public class PortalConsts
    {
      public static DateTime SearchNullDate = Convert.ToDateTime("1969-12-28T00:00:00.0");
      /// <summary>Имя папки в файловом хранилище для временных файлов</summary>
      public static string StorageFolder = "IPS";
      /// <summary>Имя xml файла с атрибутами</summary>
      public static string AttributesXmlFileName = "attributes.xml";
      /// <summary>Название корневого нода с атрибутами</summary>
      public static string XmlRootNodeAttributes = "ATTRIBUTES";
      /// <summary>Название корневого нода с замечаниями</summary>
      public static string XmlRootNodeRemark = "RATTRIBUTE";
      /// <summary>Название нода с атрибутом</summary>
      public static string XmlNodeAttribute = "ATTRIBUTE";
      /// <summary>Название нода с системным атрибутом</summary>
      public static string XmlNodeSysAttribute = "SYSATTRIBUTE";
      /// <summary>Название нода с системным атрибутом</summary>
      public static string XmlNodeContext = "CONTEXT";
      /// <summary>Название нода со значением атрибута</summary>
      public static string XmlNodeValueAttribute = "VALUE";
      /// <summary>Значение атрибута при публикации</summary>
      public const string F_ORIGINAL_VALUE = "F_ORIGINAL_VALUE";
      /// <summary>
      /// Название атрибута нода с именем файла в котором лежит блоб
      /// </summary>
      public const string F_FILE = "F_FILE";
      /// <summary>
      /// Название атрибута нода с именем файла в котором лежит блоб
      /// </summary>
      public const string F_OBJECTS = "F_OBJECTS";
      /// <summary>
      /// Название атрибута нода с именем файла в котором лежит блоб
      /// </summary>
      public const string F_FILE_TYPE = "F_FILE_TYPE";
      /// <summary>
      /// Название атрибута нода с именем файла в котором лежит блоб
      /// </summary>
      public const string F_FILE_AUTHOR = "F_FILE_AUTHOR";
      /// <summary>
      /// 
      /// </summary>
      public const string F_OBJECT_GUID = "F_OBJECT_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_PARENT_GUID = "F_PARENT_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_PART_GUID = "F_PART_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_PROJECT_GUID = "F_PROJECT_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_RELATION_TYPE_GUID = "F_RELATION_TYPE_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_RELATION_TYPE_NAME = "F_RELATION_TYPE_NAME";
      /// <summary>
      /// 
      /// </summary>
      public const string F_COMP_VERSION_ID = "F_COMP_VERSION_ID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_OBJTYPE_GUID = "F_OBJTYPE_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_OBJTYPE_SHORTNAME = "F_OBJTYPE_SHORTNAME";
      /// <summary>
      /// 
      /// </summary>
      public const string F_DOCTYPE_EXT = "F_DOCTYPE_EXT";
      /// <summary>
      /// 
      /// </summary>
      public const string F_PUBLISH_OBJTYPE = "F_PUBLISH_OBJTYPE";
      public const string F_ROOT_TYPE = "F_ROOT_TYPE";
      /// <summary>
      /// 
      /// </summary>
      public const string F_LINKED_GUID = "F_LINKED_GUID";
      /// <summary>
      /// 
      /// </summary>
      public const string F_CATEGORY = "F_CATEGORY";
      /// <summary>
      /// 
      /// </summary>
      public const string F_VER_CODE = "F_VER_CODE";
      /// <summary>
      /// 
      /// </summary>
      public const string F_MESSAGE = "F_MESSAGE";
      /// <summary>
      /// 
      /// </summary>
      public const string F_ADD_DATA = "F_ADD_DATA";
      public const string F_VER_NOTE = "F_VER_NOTE";
      /// <summary>Тип объектов "Опубликованные объекты"</summary>
      public static Guid objtypePublishObjects = new Guid("cad01489-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объектов "Узлы информационной системы"</summary>
      public const string objtypeSitesString = "cad0148c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Узлы информационной системы"</summary>
      public static Guid objtypeSites = new Guid("cad0148c-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объектов "Задачи синхронизации с IPS WebPortal"</summary>
      public static Guid objtypeUpdateTasks = new Guid("cad0149e-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объектов "Изменения"</summary>
      public static Guid objtypeChanges = new Guid("cad0149c-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа объектов "Выборки для удаленных запросов"
      /// </summary>
      public static Guid objtypePortalSelections = new Guid("cad01506-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Опубликован в составе"</summary>
      public static Guid attributePublishInComposition = new Guid("cad014ce-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Статус задачи"</summary>
      public static Guid attributeTaskStatus = new Guid("cad0149d-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Владелец опубликованного объекта"</summary>
      public static Guid attributeOwner = new Guid("cad01496-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Владелец состава опубликованного объекта"</summary>
      public static Guid attributeCompositionOwner = new Guid("cadd96bc-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Наименование типа связей"</summary>
      public static Guid attributeRelTypeName = new Guid("cad014d0-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Наименование типа связей"</summary>
      public const string attributeRelTypeNameStr = "cad014d0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Разрешенные узлы"</summary>
      public static Guid attributeEnabledSites = new Guid("cad01491-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Глобальный идентификатор связанного объекта"</summary>
      public static Guid attributeLinkedGuid = new Guid("cad0156a-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Глобальный идентификатор связанного объекта"</summary>
      public const string attributeLinkedGuidStr = "cad0156a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Код узла информационной системы"</summary>
      public static Guid attributeSiteCode = new Guid("cad014b9-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Держатели копий"</summary>
      public static Guid attributeCopyKeepers = new Guid("cad014bb-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Импортировано узлами"
      /// </summary>
      public static Guid attributeImportedSites = new Guid("cadd9455-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Автоматически созданные пользователи"</summary>
      public static Guid attributeAutoCreateUsers = new Guid("cad015c5-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Узлы с правом владения"</summary>
      public static Guid attributeParentSites = new Guid("cad01494-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Узлы с правом владения составом"</summary>
      public static Guid attributeCompositionParentSites = new Guid("cadd96bd-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Узел-создатель"</summary>
      public static Guid attributeFirstPublishSite = new Guid("cad014cc-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Режимы импорта версий"</summary>
      public static Guid attributeImportVersionsModes = new Guid("cadd960e-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Глобальный идентификатор обновления"</summary>
      public static Guid attributeUpdateGuid = new Guid("cad014c7-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Таблица индексов"</summary>
      public static Guid attributeIndexesTable = new Guid("cad01585-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Тип изменений"</summary>
      public static Guid attributeChangesType = new Guid("cad01497-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Только чтение"</summary>
      public static Guid attributeReadOnly = new Guid("cad014cd-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Атрибут "Глобальный идентификатор версии опубликованного объекта"
      /// </summary>
      public static Guid attributePublishObjectGUID = new Guid("cad01500-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Категория изменяемого объекта"</summary>
      public static Guid attributeChangedCategory = new Guid("cad014a5-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Инициатор задачи"</summary>
      public static Guid attributeTaskUser = new Guid("cad014a6-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Последний выполненный шаг задачи"</summary>
      public static Guid attributeLastStepIDCompleted = new Guid("cadd9b87-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Передача разрешена"</summary>
      public static Guid attributeTaskTransferEnabled = new Guid("cadd9c2e-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID типа связей "Состав опубликованного объекта"</summary>
      public static Guid reltypePublish = new Guid("cad01492-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Тип задачи синхронизации"</summary>
      public static Guid attributeTaskType = new Guid("cad014a2-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Имя сервера"</summary>
      public static Guid attributeServerName = new Guid("cad01589-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Порядковый номер"</summary>
      public static Guid attributeTaskNo = new Guid("cad014a0-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Описание ошибки"</summary>
      public static Guid attributeError = new Guid("cad0070c-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Файл ошибки"</summary>
      public static Guid attributeFileError = new Guid("cadd9379-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Приоритет"</summary>
      public static Guid attributePriority = new Guid("cad002d1-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Процент выполнения"</summary>
      public static Guid attributePercent = new Guid("cad014a1-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Данные задачи синхронизации"</summary>
      public static Guid attributeTaskData = new Guid("cad0149f-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Тип объектов для публикации"
      /// </summary>
      public static Guid attributePublishObjTypeGuid = new Guid("cad014b8-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Ссылки на опубликованные объекты"
      /// </summary>
      public static Guid attributePublishLinksGuid = new Guid("cad014fe-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор выборки "Опубликованные объекты с автоматической репликацией"
      /// </summary>
      public static Guid selectionAutoPublish = new Guid("cad014be-306c-11d8-b4e9-00304f19f545");
      /// <summary>Роль "Репликатор баз данных"</summary>
      public static Guid objectReplicatorRole = new Guid("cad0148e-306c-11d8-b4e9-00304f19f545");
      /// <summary>Роль Администратор портала</summary>
      public static Guid objectPortalAdminRole = new Guid("cadd966e-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа объектов "Квитанции"</summary>
      public static Guid objtypeReceipt = new Guid("cadd95ee-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Глобальные идентификаторы типов опубликованных объектов"
      /// </summary>
      public static Guid attributePortalObjectTypes = new Guid("cad01502-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Причина публикации"
      /// </summary>
      public static Guid attributeReasonInfo = new Guid("cadd95c3-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Тип квитанции"
      /// </summary>
      public static Guid attributeReceiptType = new Guid("cadd95ef-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа атрибутов "Актуально"</summary>
      public static Guid attributeReceiptActualFlag = new Guid("cadd9ba1-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Файлы пакета"
      /// </summary>
      public static Guid attributePacketFiles = new Guid("cadd95f9-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа объектов "Пакеты опубликованных объектов"
      /// </summary>
      public static Guid objtypePacket = new Guid("cadd95f8-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа объектов "Группы опубликованных объектов"
      /// </summary>
      public static Guid objtypeGroup = new Guid("cadd952c-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор атрибута "Ошибка импорта"</summary>
      public static Guid attributeImportError = new Guid("cadd9613-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор атрибута "Атрибуты таблицы"</summary>
      public static Guid attributeTableAttributes = new Guid("cadd961b-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Коментарий пакета"
      /// </summary>
      public static Guid attributePacketNote = new Guid("cadd95fa-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Сопоставление атрибутов"
      /// </summary>
      public static Guid attributeComparisonAttributes = new Guid("cadd9673-306c-11d8-b4e9-00304f19f545");
      /// <summary>GUID атрибута "Импортированные данные таблицы IMBASE"</summary>
      public static Guid attributeImportedTableData = new Guid("cadd9618-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Дата формирования квитанции"
      /// </summary>
      public static Guid attributeReceiptCreateDate = new Guid("cadd95fd-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Создатель квитанции"
      /// </summary>
      public static Guid attributeReceiptCreator = new Guid("cadd95fe-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Файл квитанции"
      /// </summary>
      public static Guid attributeReceiptFile = new Guid("cadd9604-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Идентификатор действия"
      /// </summary>
      public static Guid attributeActionID = new Guid("cadd9600-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Идентификатор процесса"
      /// </summary>
      public static Guid attributeProcessID = new Guid("cadd95ff-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа атрибутов "Точка ввода"</summary>
      public static Guid attributeEnterPoint = new Guid("cadd9617-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Узлы для обновления"
      /// </summary>
      public static Guid attributeSitesForUpdate = new Guid("cad0151c-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Опции публикации"
      /// </summary>
      public static Guid attributePublishOptions = new Guid("cadd95bd-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Корневой тип опубликованного объекта"
      /// </summary>
      public static Guid attributeRootTypePublishObject = new Guid("cad01544-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа объектов "Опубликованные шаблоны процессов"
      /// </summary>
      public static Guid objtypePublishProcessesTemplates = new Guid("cad01551-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Файлы задачи синхронизации"
      /// </summary>
      public static Guid attributeTaskFiles = new Guid("cad01586-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Исходные публикуемые объекты".
      /// </summary>
      public static Guid attributePublishInformation = new Guid("cadd9b88-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Файл замечаний"
      /// </summary>
      public static Guid attributeRemarkFiles = new Guid("cadd9393-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      ///  Глобальный идентификатор типа атрибутов "Список замечаний"
      /// </summary>
      public static Guid attributeRemarkList = new Guid("cadd9394-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа атрибутов "Изменение"</summary>
      public static Guid attributeVerCode = new Guid("cadd9445-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа атрибутов "Система"</summary>
      public static Guid attributeSystem = new Guid("cadd9517-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Необходима публикация на портал"
      /// </summary>
      public static Guid attributePublicationNecessary = new Guid("cadd95f6-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибутов "Идентификатор пакета"
      /// </summary>
      public static Guid attributePacketID = new Guid("cadd9610-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор типа атрибутов "Чужой"</summary>
      public static Guid attributeForeign = new Guid("cadd9614-306c-11d8-b4e9-00304f19f545");
      /// <summary>Глобальный идентификатор пакета</summary>
      public static Guid attributePacketGUID = new Guid("cadd96be-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объекта "Импортированные объекты"</summary>
      public static Guid objtypeImportedObjects = new Guid("cadd959b-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объекта "Импортированные документы"</summary>
      public static Guid objtypeImportedDocuments = new Guid("cadd959a-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объектов "Импортированные изделия"</summary>
      public static Guid objtypeImportedArticles = new Guid("cadd9599-306c-11d8-b4e9-00304f19f545");
      /// <summary>Аттрибут "Наименование типа объекта"</summary>
      public static Guid attributeObjTypeName = new Guid("cad014cf-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Название атрибута, в который сорч пишет гуид основного документа для объекта
      /// </summary>
      public static string MainDocGuidAttribute = "A3E6BA91-A40A-4B8B-AD4F-2226B55E8806";
      /// <summary>Имя модуля клиента портала</summary>
      public static string PortalClientModuleName = "PORTAL_CLIENT";
      /// <summary>Общие настройки</summary>
      public const string SectionGeneralSettings = "GENERAL_SETTINGS";
      public const string LoggingTransferObjectTypes = "LOGGING_TYPES";
      /// <summary>Настройки сервисов</summary>
      public const string SectionImportSettings = "IMPORT_SETTINGS";
      /// <summary>
      /// Имя параметра "Количество строк, возвращаемых запросом на портал"
      /// </summary>
      public const string ParamCountRecordsInPackage = "RECORD_COUNT";
      /// <summary>
      /// Имя параметра "Идентификатор версии шаблона для отображения квитанции"
      /// </summary>
      public const string ParamReceiptTemplateID = "RECEIPT_TEMPL_ID";
      /// <summary>
      /// Имя параметра "Идентификатор версии шаблона для отображения квитанции"
      /// </summary>
      public const string ParamTransferObjectsLogging = "TRANSFER_LOGGING";
      /// <summary>Имя параметра "Владелец импортированного объекта"</summary>
      public const string ParamImportedObjectOwner = "OWNER_ID";
      public const string ParamImportedBaseVersionTemplate = "BASE_VERSION_TEMPLATE";
      public const string ParamImportCompleteTemplate = "COMPLETE_TEMPLATE";
      public const string ParamImportErrorTemplate = "ERROR_TEMPLATE";
      public const string ParamCreateDetailegLog = "CREATE_DETAIL_LOG";
      public const string ParamCentralizedNSI = "CENTRALIZED_NSI";
      public const string ParamRewriteArchive = "REWRITE_ARCHIVE";
      public const string ParamRenameFileName = "RENAME_FILENAME";
      public const string ParamImportFolder = "IMPORT_FOLDER";
      public const string ParamMaxAccessLevel = "MAX_ACCESS";
      public const string ParamOTDFoltering = "OTD_FILTER";
      public const string ParamStorageID = "STORAGE_ID";
      public const string ParamAnswerTaskPriority = "ANSWER_PRIORITY";
      public const string ParamReceipt4packetTaskPriority = "RECEIPT_PRIORITY";
      public const string ParamBeSurePublishForSites = "BESURE_PUBLISH";
      public const string ParamEnableTrueTaskForSites = "ENABLE_TASKS_SITIES";
      public const string InseparableObjectTypesFileName = "InseparableObjectTypes";
      /// <summary>
      /// Имя параметра "Размер буффера для блочного записи/чтения блобов в/из портал(а) по умолчанию"
      /// </summary>
      public const string ParamFileTransferBufferLength = "BUFFER_SIZE";
      /// <summary>
      /// Значение параметра "Количество строк, возвращаемых запросом на портал" по умолчанию
      /// </summary>
      public static long DefaultCountRecordsInPackage = 500;
      /// <summary>
      /// Размер буффера для блочного записи/чтения блобов в/из портал(а) по умолчанию
      /// </summary>
      public static int DefaultFileTransferBufferLength = 524288 /*0x080000*/;
      /// <summary>
      /// Допустимые типы атрибутов, которые можно выносить в публикуемых объектах
      /// </summary>
      public static FieldTypes[] EnabledFieldTypes = new FieldTypes[7]
      {
        FieldTypes.ftAutoInc,
        FieldTypes.ftBoolean,
        FieldTypes.ftDateTime,
        FieldTypes.ftDouble,
        FieldTypes.ftGuid,
        FieldTypes.ftInteger,
        FieldTypes.ftString
      };
      /// <summary>
      /// Допустимые к отображению и запросам на портал обязательные атрибуты объекта
      /// </summary>
      public static int[] EnabledObligatoryObjectAttributes = new int[4]
      {
        -2,
        -7,
        -50,
        -13
      };
      public static readonly int DeleteWithoutFiles = 1;

      /// <summary>
      /// Формирование глобального в пределах информационной системы логина пользователя
      /// </summary>
      /// <param name="siteCode">Код узла</param>
      /// <param name="loginName">Логин пользователя</param>
      /// <returns></returns>
      public static string GlobalLoginName(char siteCode, string loginName)
      {
        return $"{siteCode}\\{loginName}";
      }

      /// <summary>
      /// Формирование глобального в пределах информационной системы логина пользователя
      /// </summary>
      /// <param name="siteName">Наименование узла</param>
      /// <param name="userName">Отображаемое имя пользователя</param>
      /// <returns></returns>
      public static string GlobalUserName(string siteName, string userName)
      {
        return $"{siteName}\\{userName}";
      }

      /// <summary>
      /// Список гуидов атрибутов в значении которых используются коды узлов
      /// </summary>
      public static Guid[] SiteCodeAttributes
      {
        get
        {
          return new Guid[10]
          {
            PortalConsts.attributeCopyKeepers,
            PortalConsts.attributeEnabledSites,
            PortalConsts.attributeFirstPublishSite,
            PortalConsts.attributeParentSites,
            PortalConsts.attributeOwner,
            PortalConsts.attributeSitesForUpdate,
            PortalConsts.attributeEnterPoint,
            PortalConsts.attributeCompositionParentSites,
            PortalConsts.attributeCompositionOwner,
            PortalConsts.attributeImportedSites
          };
        }
      }
    }
}
