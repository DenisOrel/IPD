
// Type: Intermech.Consts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech
{
    public class Consts
    {
      /// <summary>
      /// Параметр для передачи на сервер даты модификации метаданных через таблицу
      /// </summary>
      public const string ModifeDateExtendedParam = "modify_date";
      /// <summary>Перестройка нормализованных индексов</summary>
      public const string RebuildIndexMethod = "RebuildIndex";
      /// <summary>Проверить целостность данных</summary>
      public const string RepairDataMethod = "RepairData";
      /// <summary>Перестройка представлений данных</summary>
      public const string RebuildViewsMethod = "RebuildViews";
      /// <summary>
      /// Имя главной клиентской сессии по умолчанию (используется в логине по умолчанию)
      /// </summary>
      public const string DefaultMainClientSessionName = "DefaultMainClientSession";
      /// <summary>Полный список типов объектов</summary>
      public const int ObjectTypesAll = -2;
      /// <summary>Список корневых типов объектов</summary>
      public const int ObjectTypesRoot = -1;
      /// <summary>
      /// Использовать уровень доступа, назначенный данному пользователю
      /// </summary>
      public const int AccessLevelUserDefault = -1;
      /// <summary>Минимально возможный уровень доступа</summary>
      public const int AccessLevelMinimal = 0;
      public const string RebuildOracleIndexes = "RebuildOracleIndexes";
      /// <summary>
      /// Имя параметра для передачи в Select режима отображения контекстных версий объектов
      /// </summary>
      public const string ShowAllModifications = "ShowAllModifications";
      /// <summary>
      /// Интервал опроса задач обновления MetaDataHelper на сервере
      /// </summary>
      public const int MetaDataHelperUpdateInterval = 1000;
      /// <summary>
      /// Значение поля F_ATTRIBUTE_ID, которым в файловом шкафу помечаются записи для удаления при чистке мусора
      /// </summary>
      public const int DeletedBlob = -2000;
      /// <summary>
      /// Максимально допустимое количество типов объектов, которое может участвовать
      /// в фильтрации составов по типам объектов и типам связей
      /// </summary>
      public static readonly int FiltrationMaxObjTypes = 100;
      /// <summary>Не проверять ApplicabilityModes при удалении связи</summary>
      public static readonly int DontCheckApplicabilityModes = 1;
      /// <summary>
      /// Максимальный размер блоба для работы в памяти читалок блобов
      /// </summary>
      public static readonly long BlobInMemoryOperationalLimit = 131072 /*0x020000*/;
      /// <summary>Режим физического удаления объектов/связей</summary>
      public static readonly int PurgeMode = 2;
      /// <summary>Режим возврата измененного объекта в базу данных</summary>
      public static readonly int CheckInMode = 4;
      /// <summary>Режим взятия объекта на изменение</summary>
      public static readonly int CheckOutMode = 32 /*0x20*/;
      /// <summary>
      /// Флаг, передающийся в метод Delete объекта в случае, если удаление вызвано удалением связанного с ним объекта
      /// </summary>
      public static readonly int RelationConstraintMode = 64 /*0x40*/;
      /// <summary>Режим создания объекта/связи</summary>
      public static readonly int CreateMode = 128 /*0x80*/;
      /// <summary>Режим пакетной записи значений атрибутов</summary>
      public static readonly int AssignValuesMode = 256 /*0x0100*/;
      /// <summary>
      /// Используется для отключения проверок плагинами при удалении объектов и связей
      /// </summary>
      public const int ForceDeleteMode = 512 /*0x0200*/;
      /// <summary>Режим создания версии объекта</summary>
      public const int CreateVersionMode = 1024 /*0x0400*/;
      /// <summary>Режим отмены изменений</summary>
      public const int CancelChangesMode = 2048 /*0x0800*/;
      /// <summary>Режим патча базы данных</summary>
      public const int AutoPatchMode = 4096 /*0x1000*/;
      /// <summary>Режим создания версии объекта/связи по прототипу</summary>
      public const int CreatePrototypeMode = 8192 /*0x2000*/;
      /// <summary>
      /// Режим работы администратора (используется в хитрых командах типа отмены изменений у других пользователей)
      /// </summary>
      public const int AdminMode = 16 /*0x10*/;
      /// <summary>
      /// Указывает что данная запись о правах является правами по умолчанию
      /// </summary>
      public const int DefaultAccessKey = -1;
      /// <summary>Максимальная точность вычислений знаков после запятой</summary>
      public static int MaxPrecision = 14;
      /// <summary>
      /// Формат преобразования Double в строку при помощи метода double.ToString(string format)
      /// для предотвращения формирования строк в экспоненциальном виде
      /// </summary>
      public const string FormatDouble = "#################0.#################";
      public const string WinloginPsw = "WindowsLoginMode";
      /// <summary>Хэш пароля, означающего вход по паролю винды</summary>
      public static readonly string WinloginPswHash = CryptHelper.CryptPassword("WindowsLoginMode", CryptHelper.SHA1Crypt);
      /// <summary>
      /// Символ, обозначающий отсутствие значения у символьного идентификатора (например, код узла владельца)
      /// </summary>
      public static readonly char NoSymbol = '^';
      /// <summary>Режим удаления экземпляров (атрибутов и пр.)</summary>
      public static readonly int DeleteInstances = 4;
      /// <summary>Режим удаления дочерних объектов, типов и пр.</summary>
      public static readonly int DeleteChildren = 8;
      public static readonly int traceAlways = 0;
      public static readonly int traceError = 1;
      public static readonly int traceWarning = 2;
      public static readonly int traceDebugInfo = 3;
      public static readonly int traceDebugGarbage = 4;
      /// <summary>Имя файла для записи ошибок выполнения скриптов</summary>
      public const string ScriptErrorsTraceFileName = "script_errors.log";
      /// <summary>
      /// Имя файла для записи событий успешного выполнения скриптов
      /// </summary>
      public const string ScriptInvocationsTraceFileName = "script_invocations.log";
      /// <summary>Имя файла для записи ошибок выполнения sql-операторов</summary>
      public const string SqlErrorsTraceFileName = "sql_errors.log";
      /// <summary>
      /// Имя файла лога для записи ошибок неверного распределения менеджеров работы с данными
      /// </summary>
      public const string DataManagerTraceFileName = "data_manager_errors.log";
      /// <summary>
      /// Имя файла для записи ошибок многопоточного доступа с сессии
      /// </summary>
      public const string SessionThreadTraceFileName = "session_thread_errors.log";
      /// <summary>
      /// Имя файла для записи ошибок с незакрытыми транзакциями с сессии
      /// </summary>
      public const string SessionForgottenTransactionFileName = "session_forgotten_transaction.log";
      /// <summary>
      /// Имя файла для записи ошибок управления ресурсами сессий
      /// </summary>
      public const string SessionManagementTraceFileName = "session_management.log";
      /// <summary>Максимальный размер log-файла</summary>
      public static readonly int traceFileMaxSize = 10485760 /*0xA00000*/;
      public const string AccessLogDelimiter = "------------------------------------";
      /// <summary>Типы файловых шкафов</summary>
      public const string OracleStorage = "Oracle";
      public const string MSSQLStorage = "MS SQL Server";
      public const string IMDOCStorage = "Intermech Document Server";
      public const string LinterStorage = "Linter";
      public const string PostgreStorage = "PostgreSQL";
      public const string FileSystemStorage = "Файловая система";
      /// <summary>
      /// Значение говорит о том, что данная ф-ция этим типов файлового шкафа не поддерживается
      /// </summary>
      public const long NotSupportStorageFunc = -1;
      public const int CategoryUnknown = 0;
      public static readonly Guid CategoryUnknownGUID = new Guid("00000000-0000-0000-0000-000000000000");
      public const int CategoryObjectVersion = 1;
      public static readonly Guid CategoryObjectVersionGUID = new Guid("cad0004c-306c-11d8-b4e9-00304f19f545");
      public const int CategoryObject = 2;
      public static readonly Guid CategoryObjectGUID = new Guid("cad0004d-306c-11d8-b4e9-00304f19f545");
      public const int CategoryAttribute = 3;
      public static readonly Guid CategoryAttributeGUID = new Guid("cad0004e-306c-11d8-b4e9-00304f19f545");
      public const int CategoryObjectType = 4;
      public static readonly Guid CategoryObjectTypeGUID = new Guid("cad0004f-306c-11d8-b4e9-00304f19f545");
      public const int CategoryRelation = 5;
      public static readonly Guid CategoryRelationGUID = new Guid("cad00050-306c-11d8-b4e9-00304f19f545");
      public const int CategoryRelationType = 6;
      public static readonly Guid CategoryRelationTypeGUID = new Guid("cad00051-306c-11d8-b4e9-00304f19f545");
      public const int CategoryLCStep = 7;
      public static readonly Guid CategoryLCStepGUID = new Guid("cad00052-306c-11d8-b4e9-00304f19f545");
      public const int CategoryLCLevel = 8;
      public static readonly Guid CategoryLCLevelGUID = new Guid("cad00053-306c-11d8-b4e9-00304f19f545");
      public const int CategoryLanguage = 9;
      public static readonly Guid CategoryLanguageGUID = new Guid("cad00054-306c-11d8-b4e9-00304f19f545");
      public const int CategoryEventLog = 10;
      public static readonly Guid CategoryEventLogGUID = new Guid("cad00055-306c-11d8-b4e9-00304f19f545");
      public const int CategorySubjectArea = 11;
      public static readonly Guid CategorySubjectAreaGUID = new Guid("cad00056-306c-11d8-b4e9-00304f19f545");
      public const int CategoryAttributeGroup = 12;
      public static readonly Guid CategoryAttributeGroupGUID = new Guid("cad00057-306c-11d8-b4e9-00304f19f545");
      public const int CategorySystem = 14;
      public static readonly Guid CategorySystemGUID = new Guid("cad00058-306c-11d8-b4e9-00304f19f545");
      public const int CategoryFiles = 15;
      public static readonly Guid CategoryFilesGUID = new Guid("cad0036d-306c-11d8-b4e9-00304f19f545");
      public const int CategoryLCSchema = 16 /*0x10*/;
      public static readonly Guid CategoryLCSchemaGUID = new Guid("cad00581-306c-11d8-b4e9-00304f19f545");
      public const int CategoryDocument = 17;
      public const int CategoryProject = 18;
      public const int CategoryApplicability = 19;
      public static readonly Guid CategoryApplicabilityGUID = new Guid("3EE69F10-27DE-43f4-9904-A25DC582A167");
      public const int CategoryCheckObjectAccess = 20;
      public static readonly Guid CategoryHistoryFilesGUID = new Guid("8EE69F10-31DE-43f4-9904-A25DC582A167");
      public const int CategoryHistoryFiles = 21;
      /// <summary>Аттрибуты для типа объекта</summary>
      public const int CategoryAttr4Object = 22;
      public const int CategoryObjectSnapshots = 23;
      public static readonly Guid CategoryObjectSnapshotsGUID = new Guid("8EE69F10-45DF-43f4-9904-A25DC582A167");
      public const int CategorySavedObject = 24;
      public static readonly Guid CategorySavedObjectGUID = new Guid("368A49AF-B78B-484B-80F7-FD07919C6D40");
      public const int CategoryImbaseRecord = 25;
      public static readonly Guid CategoryImbaseRecordGUID = new Guid("4BB0B926-BB2B-4CF5-8AFC-F5051A99D092");
      public const int CategoryImbaseAtt = 26;
      public static readonly Guid CategoryImbaseAttGUID = new Guid("0934854D-5362-43BC-8ED4-FA28FCE15698");
      /// <summary>Номер взаимосвязанного контекста</summary>
      public const int CategoryLinkedContextNumber = 27;
      /// <summary>Технологические объекты</summary>
      /// <remarks>Используется, в частности, для проверки прав доступа</remarks>
      public const int CategoryTechObjectVersion = 28;
      /// <summary>Атрибут на шаге ЖЦ применительно к типу объектов</summary>
      public const int CategoryAttributeLCStep4ObjectType = 29;
      public const int CategoryImbaseIndex = 30;
      public static readonly Guid CategoryImbaseIndexGUID = new Guid("8D78C58D-0B88-4882-9E37-3280A6F78F3E");
      public const int CategoryForumMessages = 31 /*0x1F*/;
      /// <summary>Идентификатор группы атрибутов "Все атрибуты"</summary>
      public const int AllAttributesGroupID = -1;
      /// <summary>
      /// Идентификатор виртуальной группы атрибутов "Назначенные типам" (DatabaseConfigurator)
      /// </summary>
      public const int TypeAssignedAttributesGroupID = -10;
      /// <summary>
      /// Идентификатор группы типов объектов "Все типы объектов"
      /// </summary>
      public const int AllObjectTypesGroupID = -1;
      private static ConcurrentDictionary<int, string> CategoryNames = new ConcurrentDictionary<int, string>();
      /// <summary>Имя модуля архивов для конфигураций</summary>
      public const string ModuleArchives = "ARCHIVES";
      /// <summary>Ядро системы</summary>
      public const string ModuleKernel = "KERNEL";
      /// <summary>
      /// Патчи ядром системы, которые требуют наличия системной сессии
      /// </summary>
      public const string ModuleKernelSession = "KERNEL.SESSION";
      /// <summary>Патчи ядром системы для PostgreSQL</summary>
      public const string ModuleKernelPostgre = "KERNEL.POSTGRE";
      /// <summary>Патчи ядром системы для портала</summary>
      public const string ModuleKernelPortal = "KERNEL.PORTAL";
      /// <summary>
      /// Чистка мусора из базы, которую нужно проводить только один раз
      /// </summary>
      public const string ModuleKernelClear = "KERNEL.CLEAR";
      /// <summary>Клиент</summary>
      public const string ModuleClient = "CLIENT";
      /// <summary>Секция настроек, отвечающих за производительность</summary>
      public const string SectionPerformance = "PERFORMANCE";
      /// <summary>Секция с общими настройками</summary>
      public const string SectionCommon = "COMMON";
      /// <summary>Настройки журнала регистрации событий</summary>
      public const string SectionEventlog = "EVENTS";
      /// <summary>Секция с настройками очистки рабочей области</summary>
      public const string SectionWorkAreaCleaner = "WORKCLEANER";
      /// <summary>Настройки службы временнЫх событий</summary>
      public const string SectionTimedEvents = "TIMED_EVENTS";
      /// <summary>
      /// Имя параметра, содержащего имя сервера, обслуживающего очередь событий
      /// </summary>
      public const string ParamPrimaryEventServer = "PrimaryServer";
      /// <summary>
      /// Имя параметра, содержащего имя сервера, в данный момент обслуживающего очередь событий
      /// </summary>
      public const string ParamCurrentEventServer = "CurrentServer";
      /// <summary>
      /// Имя параметра, содержащего время последнего обслуживания очереди событий (в UTC)
      /// </summary>
      public const string ParamLastEventTime = "LastTime";
      /// <summary>Интервал опроса очереди событий в минутах</summary>
      public const int TimedEventsPeriod = 1;
      /// <summary>Настройки формирования общего поискового индекса</summary>
      public const string SectionGlobalIndex = "GLOBAL_INDEX";
      /// <summary>Минимальная длина индексируемого слова</summary>
      public const string ParamMinWordLength = "MIN_WORD_LENGTH";
      /// <summary>Хранить ли историю поисковых запросов</summary>
      public const string ParamSaveSearchHistory = "SAVE_SEARCH_HISTORY";
      /// <summary>
      /// Список расширений файлов, которых нельзя индексировать
      /// </summary>
      public const string ParamNotIndexingExtentions = "NOT_INDEX_EXT";
      public const string ParamCopyAuthenticalFiles = "COPY_AUTHENTICAL_FILES";
      /// <summary>Режим копирования атрибутов в отложенные уведомления</summary>
      public const string ParamSendAttrs2DelayedNotifications = "COPY_ATTRS2NOTIF";
      /// <summary>Параметр запрещает патчить базу данных</summary>
      public const string ParamDisableDBPatch = "DisableDBPatch";
      /// <summary>
      /// Список физических величин в дополнение к списку по умолчанию для назначения в атрибут "Количество"
      /// </summary>
      public const string ParamQuantityPhysList = "QUANTITYPHYSLIST";
      /// <summary>Режим аннулирования всех версий объекта</summary>
      public const string ParamAnnulAllVersions = "ANNUL_ALL_VERSIONS";
      /// <summary>
      /// Настройка сохранения удаляемых записей журнала событий в файл
      /// </summary>
      public const string SaveEventsToFile = "SAVE_TO_FILE";
      /// <summary>Настройки формирования нормализованного индекса</summary>
      public const string SectionNormIndex = "INDEX_PARAMS";
      /// <summary>Секция настроек диагностики ядра</summary>
      public const string SectionDiagnostics = "DIAGNOSTICS";
      /// <summary>Настройки правил отбора версий объектов</summary>
      public const string SectionVersionRules = "VERSION_RULES";
      /// <summary>Настройки внешнего вида клиента</summary>
      public const string SectionInterface = "INTERFACE";
      /// <summary>Настройка цветовых схем</summary>
      public const string ParamColorScheme = "COLOR_SCHEME";
      /// <summary>Удалять из индекса пробелы</summary>
      public const string ParamIndexDelSpaces = "DEL_SPACES";
      /// <summary>Общие настройки для типов документов</summary>
      public const string SectionDocTypes = "DOC_TYPES";
      /// <summary>Общие настройки для подписей</summary>
      public const string SectionSigns = "SIGNS";
      /// <summary>Параметр "Совместимые подписи" - используется ядром</summary>
      public const string ParamCompatibleSigns = "COMPATIBLE";
      /// <summary>Значение параметра "Совместимые подписи" по умолчанию</summary>
      public const bool DefaultCompatibleSigns = false;
      /// <summary>
      /// Разделитель между обозначением документа и кодом типа документов
      /// </summary>
      public const string ParamSeparatorInDesignation = "SEPARATOR_DESIGNATION";
      public const string ParamIndexUpper = "UPPER_CASE";
      /// <summary>
      /// Параметр для хранения имени табличного пространства для индексов
      /// </summary>
      public const string ParamIndexTablespaceName = "INDEX_TABLESPACE";
      /// <summary>Нормализовать одинаковые по написанию символы</summary>
      public const string ParamIndexCyrillic = "CYRILLIC";
      /// <summary>Устранять дублирование следующих символов</summary>
      public const string ParamIndexDuplicates = "DUPLICATES";
      /// <summary>Заменять строку на строку</summary>
      public const string ParamIndexReplaces = "REPLACES";
      /// <summary>ID текущего правила подбора версий</summary>
      public const string ParamCurrVerRuleID = "CURR_VER_RULE_ID";
      /// <summary>Настройка фильтрации состава</summary>
      public const string FiltrationTuning = "Filtration tuning";
      /// <summary>Варианты значений переменных правил подбора</summary>
      public const string ParamRuleVars = "Rule variables";
      /// <summary>Сервис очистки мусора в базе</summary>
      public const string SectionClearThash = "CLEAR_TRASH";
      /// <summary>Сервис обновления</summary>
      public const string SectionUpdateService = "UPDATE_SERVICE";
      /// <summary>
      /// Имя параметра, хранященго флаг включения сбора статистики оптимизатора запросов
      /// </summary>
      public const string ParamOptimizerON = "OPTIM_STAT";
      /// <summary>Имя секции с настройками SMTP-сервера</summary>
      public const string SectionSMTPServer = "SMTP_SERVER";
      /// <summary>Имя секции с настройками SMTP-сервера</summary>
      public const string SectionMailUsers = "MAIL_USERS";
      /// <summary>Имя параметра c именем (либо IP) SMTP-сервера</summary>
      public const string ParamSmtpServer = "SmtpServer";
      /// <summary>Имя параметра c именем (либо IP) SMTP-сервера</summary>
      public const string ParamEmailName = "EmailName";
      /// <summary>Имя параметра с портом SMTP-сервера</summary>
      public const string ParamSmtpPort = "SmtpPort";
      /// <summary>Имя параметра с кодировкой писем, отправляемых</summary>
      public const string ParamEmailEncoding = "Encoding";
      /// <summary>Имя параметра с логином на SMTP-сервер</summary>
      public const string ParamSmtpLogin = "Login";
      /// <summary>Имя параметра с паролем на SMTP-сервер</summary>
      public const string ParamSmtpPassword = "Password";
      /// <summary>E-mail отправителя по-умолчанию</summary>
      public const string ParamDefaultEmail = "DefaultEmail";
      /// <summary>
      /// Размер файлов в шкафу (в гигабайтах), после которого нужно уведомлять админа о его превышении
      /// </summary>
      public const string ParamStorageSizeNotification = "StorageSizeNotify";
      /// <summary>
      /// Нижний порог свободного места на диске сервера приложений (в гигабайтах), после которого нужно уведомлять администратора о проблеме
      /// </summary>
      public const string ParamServerDiskFreeSizeNotification = "ServerDiskFreeSizeNotify";
      /// <summary>
      /// Максимальный объем физической памяти, использованной сервером приложений (в мегабайтах), после которого нужно уведомлять администратора о проблеме
      /// </summary>
      public const string ParamServerPeakMemoryUsageNotification = "ServerPeakMemoryUsageNotify";
      /// <summary>Максимальный размер лог-файла (в мегабайтах)</summary>
      public const string MaxLogFileSize = "MaxLogFileSize";
      /// <summary>Количество предыдущих копий лог-файла</summary>
      public const string MaxLogFileCopies = "MaxLogFileCopies";
      /// <summary>Имя секции с настройками SMTP-сервера</summary>
      public const string SectionPop3Server = "POP3_SERVER";
      /// <summary>Имя секции с общими настройками почтового ящика</summary>
      public const string SectionEmail = "EMAIL_SECTION";
      /// <summary>Имя параметра c именем (либо IP) SMTP-сервера</summary>
      public const string ParamPop3Server = "Pop3Server";
      /// <summary>Имя параметра с портом SMTP-сервера</summary>
      public const string ParamPop3Port = "Pop3Port";
      /// <summary>Имя параметра с логином на SMTP-сервер</summary>
      public const string ParamPop3Login = "Pop3Login";
      /// <summary>Имя параметра с паролем на SMTP-сервер</summary>
      public const string ParamPop3Password = "Pop3Password";
      /// <summary>
      /// Наименование секции с настройками напоминания органайзера
      /// </summary>
      public const string SectionOrganizerReminder = "ORGANIZER_REMINDER";
      /// <summary>Наименование параметра включения напоминания</summary>
      public const string ParamActivate = "ACTIVATE";
      /// <summary>Наименование параметра интервала времени</summary>
      public const string ParamTimeSpace = "TIME_SPACE";
      /// <summary>
      /// Наименование параметра, время напоминания до начала события
      /// </summary>
      public const string ParamTimeBefore = "TIME_BEFORE";
      /// <summary>
      /// Константа для получения идентификатора внутренней канцелярии из текущей сессии
      /// </summary>
      public const string InternalDepartmentID = "DEPARTMENT_ID";
      /// <summary>
      /// Ключ в контексте вызова текущего потока. В нем передается информация что запрос коллекции объектов идет из workflow.
      /// </summary>
      public const string NoFilterQuery = "X-IPS-NoFilterQuery";
      /// <summary>
      /// Значение в контексте вызова текущего потока для <see cref="F:Intermech.Consts.NoFilterQuery" />, означающее, что данный режим включен.
      /// </summary>
      public const string NoFilterQueryEnabledValue = "true";
      /// <summary>
      /// Имя параметра "Проверять права доступа к архивам у изделий"
      /// </summary>
      public const string ParamArtSecurityCheck = "ART_ACCESS";
      /// <summary>
      /// Имя параметра "Заменять настройки видимости объектов настройками видимости архивов"
      /// </summary>
      public const string ParamCopyArcVisibility = "COPY_ARC_VISIBLE";
      /// <summary>
      /// Имя параметра "Заменять настройки видимости объектов настройками видимости проектов"
      /// </summary>
      public const string ParamCopyProjVisibility = "COPY_PROJ_VISIBLE";
      /// <summary>
      /// Имя параметра "Обновлять список закладок при изменении объекта"
      /// </summary>
      public const string ParamUpdateViewsList = "UPDATE_VIEWS_LIST";
      /// <summary>
      /// Имя параметра Через сколько удалять файлы из файлового хранилища
      /// </summary>
      public const string ParamCleaningPendingDateCount = "CleaningPendingDateCount";
      /// <summary>
      /// Имя параметра указывающий на тип (Дни, недели, месяцы, годы) через сколько удалять файлы из файлового хранилища
      /// </summary>
      public const string ParamCleaningPendingDateMode = "CleaningPendingDateMode";
      /// <summary>Порт SMTP сервера по-умолчанию</summary>
      public static readonly int DefaultSmtpPort = 25;
      /// <summary>Порт POP3 сервера по-умолчанию</summary>
      public static readonly int DefaultPop3Port = 110;
      /// <summary>
      /// Максимальное значение Size для типа атрибута короткие двоичные данные
      /// </summary>
      public static readonly int MaxShortBlobSize = 262144 /*0x040000*/;
      /// <summary>Максимальное значение Size для типа атрибута мемо</summary>
      public static readonly int MaxMemoSize = 1048576 /*0x100000*/;
      /// <summary>
      /// Значение size по умолчанию для типа атрибута короткие двоичные данные
      /// </summary>
      public static readonly int DefaultShortBlobSize = Consts.MaxShortBlobSize / 2;
      /// <summary>Максимальное значение size для String</summary>
      public static readonly int MaxStringSize = 850;
      /// <summary>Размерность строковых полей в базе по умолчанию</summary>
      public static readonly int DefaultStringDbFieldLength = 450;
      /// <summary>
      /// Максимальная строковая составляющая для полей типа Memo
      /// </summary>
      public static readonly int MaxMemoStringValueSize = Consts.DefaultStringDbFieldLength;
      /// <summary>Максимальный размер поля Note в файловом шкафу</summary>
      public static readonly int MaxStorageNoteValueSize = Consts.DefaultStringDbFieldLength;
      /// <summary>
      /// Максимальный размер слова в нормализованном поисковом индексе
      /// </summary>
      public static readonly int MaxIndexWordLength = 450;
      /// <summary>
      /// Максимальный размер пакета данных, который может включить пользователь в настройках IPS
      /// </summary>
      public const int MaxRowsLimit = 10000;
      /// <summary>
      /// Длина строки для операции CAST к NVARCHAR в различных операциях преобразования типов. Используется для обхода ошибок СУБД при преобразовании типов в строки большой длины.
      /// </summary>
      public static readonly int CastStringSize = 80 /*0x50*/;
      /// <summary>Значение size для String по умолчанию</summary>
      public static readonly int DefaultStringSize = 10;
      /// <summary>Максимальное значение Size для числовых параметров</summary>
      public static readonly int MaxNumericSize = 0;
      /// <summary>Максимальная длина пароля Password</summary>
      public static readonly int MaxPasswordSize = 40;
      /// <summary>Длина пароля по умолчанию</summary>
      public static readonly int DefaultPasswordSize = 15;
      /// <summary>Максимальная длина наименования объекта</summary>
      public static readonly int MaxObjectNameLength = (int) byte.MaxValue;
      /// <summary>Максимальная длина наименования предметной области</summary>
      public static readonly int MaxSubjectAreaNameLength = Consts.MaxObjectNameLength;
      /// <summary>
      /// Максимальное количество предметных областей, которые можно присваивать метаданным
      /// </summary>
      public static readonly int MaxSubjectAreasCount = 20;
      /// <summary>Максимальная длина для комментариев</summary>
      public static readonly int MaxNoteLength = Consts.MaxStringSize;
      /// <summary>
      /// Максимальная длина для кратких наименований типов и атрибутов
      /// </summary>
      public static readonly int MaxShortNameLength = 32 /*0x20*/;
      /// <summary>
      /// Максимальное количество дней, которое можно задать для указания времени жизни удаленных объектов
      /// </summary>
      public const int MaxLifetimeReserve = 36500;
      /// <summary>
      /// Константа, определяющая назначение атрибута в таблице IMS_FORMULA_ATTRS, задействован в формуле
      /// </summary>
      public static readonly int Attribute4Formula = 0;
      /// <summary>
      /// Константа, определяющая назначение атрибута в таблице IMS_FORMULA_ATTRS, задействован в правиле проверки значения
      /// </summary>
      public static readonly int Attribute4ValidationRule = 1;
      /// <summary>
      /// Указывает на необходимость удаления данной записи (в правах доступа)
      /// </summary>
      public static readonly int DeleteRecord = -1;
      /// <summary>
      /// Это значение нужно записать в правила валидации атрибута "Ссылка на объект" для того,
      /// чтобы ссылка не давала удалить ссылаемый объект
      /// </summary>
      public const string ObjectLinkConstraint = "Value";
      /// <summary>Имя функции, возвращающей текущую дату</summary>
      public static readonly string CurrentDateFunction = LocalizationHolder.rm.GetString("Interfaces_129");
      /// <summary>
      /// Функция означает, что атрибут хранит только дату без времени. Записывается в маску воду.
      /// </summary>
      public static readonly string OnlyDateFunction = "Дата";
      /// <summary>
      /// Функция говорит о том, что атрибут хранит и время, и дату. Записывается в маску воду.
      /// </summary>
      public static readonly string DateAndTimeFunction = "Дата и время";
      /// <summary>Имя функции, возвращающей текущего пользователя</summary>
      public static readonly string CurrentUserFunction = LocalizationHolder.rm.GetString(nameof (CurrentUserFunction));
      /// <summary>Время обновления кэша прав доступа</summary>
      public static TimeSpan CacheClearPeriod = TimeSpan.FromMinutes(60.0);
      /// <summary>Время ожидания выполнения медленного SQL-запроса</summary>
      public const int AdminCommandTimeout = 172800;
      /// <summary>
      /// Значение разделителя ежду обозначением документа и кодом типа документов по умолчанию
      /// </summary>
      public static string DefaultSeparatorInDesignation = " ";
      /// <summary>Размер буфера для xтения иконок из БД</summary>
      public static readonly int IconBufferSize = 1846;
      /// <summary>
      /// Размер буффера для блочного чтения/записи блобов в СУБД
      /// </summary>
      public static int BlobTransferBufferLength = 262144 /*0x040000*/;
      /// <summary>Размер буфера для фетча записей из СУБД</summary>
      public const int FetchSize = 262144 /*0x040000*/;
      /// <summary>
      /// Размер порции для считывания блобов в IPS (по умолчанию)
      /// </summary>
      public static readonly int DefaultBlobBlockSize = 65536 /*0x010000*/;
      public static readonly string TrueValue = LocalizationHolder.rm.GetString("Interfaces_130");
      public static readonly string FalseValue = LocalizationHolder.rm.GetString("Interfaces_131");
      public static readonly string YesValue = LocalizationHolder.rm.GetString("Interfaces_132");
      public static readonly string NoValue = LocalizationHolder.rm.GetString("Interfaces_133");
      private static RDBMSList _RDBMS;
      public const string XmlWebConfig = "Web.Config";
      public const string XmlIntermechListenerConfig = "Intermech.Server.Service.exe.config";
      public const string XmlIntermechServerConfig = "ConsoleServer.exe.config";
      public const string XmlkeyConnectionString = "ConnectionString";
      public const string XmlkeyConnectionOraString = "Server.Oracle";
      public const string XmlkeyConnectionSqlString = "Server.SQL";
      public const string XmlkeyConnectionLinterString = "Server.Linter";
      public const string XmlkeyConnectionPostgreString = "Server.PostgreSQL";
      public const string XmlkeyConnectionName = "ConnectionName";
      public const string XmlkeyUpdate = "Update";
      public const string XmlkeyConnectionStringSQL = "ConnectionString.Server.SQL";
      public const string XmlkeyConnectionStringOracle = "ConnectionString.Server.Oracle";
      public const string XmlkeyConnectionStringLinter = "ConnectionString.Server.Linter";
      public const string XmlkeyConnectionStringPostgre = "ConnectionString.Server.PostgreSQL";
      public const string XmlkeyUsePassword = "UsePassword";
      public const string XmlkeyUserId = "User ID";
      public const string XmlkeyPassword = "Password";
      public const string XmlkeyPortalReplicUserName = "PortalReplicUserName";
      public const string XmlkeyPortalReplicLogin = "PortalReplicLogin";
      public const string XmlkeyPortalReplicPassword = "PortalReplicPassword";
      public const string XmlkeyPortalAdminUserName = "PortalAdminUserName";
      public const string XmlkeyPortalAdminLogin = "PortalAdminLogin";
      public const string XmlkeyPortalAdminPassword = "PortalAdminPassword";
      public const string XmlkeyPortalName = "PortalName";
      public const string XmlkeyPortalUrl = "PortalUrl";
      public const string XmlkeySiteGuid = "SiteGuid";
      public const string XmlkeySiteCode = "SiteCode";
      public const string XmlkeyProxyAddress = "ProxyAddress";
      public const string XmlkeyProxyPort = "ProxyPort";
      public const string XmlkeyAsyncSupported = "PortalAsyncSupported";
      public const string XmlkeyValidateVersion = "PortalValidateVersion";
      public const string ServerConnectionTag = "Server";
      public const string DatabaseConnectionTag = "Database";
      public const string IMS_INDEX_WORDS = "IMS_INDEX_WORDS";
      public const string IMS_GLOBAL_INDEX = "IMS_GLOBAL_INDEX";
      public const string IMS_INDEX_QUEUE = "IMS_INDEX_QUEUE";
      public const string IMS_INDEX_RESULT = "IMS_INDEX_RESULT";
      public const string IMS_OBJ_SNAPSHOT = "IMS_OBJ_SNAPSHOT";
      public const string IMS_REL_SNAPSHOT = "IMS_REL_SNAPSHOT";
      public const string IMS_OBJ_SNAPATTRS = "IMS_OBJ_SNAPATTRS";
      public const string IMS_REL_SNAPATTRS = "IMS_REL_SNAPATTRS";
      public const string IMS_MD_EXTENSIONS = "IMS_MD_EXTENSIONS";
      public const string IMS_IMBASE_INDEX = "IMS_IMBASE_INDEX";
      public const string IMS_IMH_INDEX = "IMS_IMH_INDEX";
      public const string IMS_LOCALIZATION = "IMS_LOCALIZATION";
      public const string IMS_PROJECT_TEAM = "IMS_PROJECT_TEAM";
      public const string IMS_LC_SCHEMAS = "IMS_LC_SCHEMAS";
      public const string IMS_FILTRATION_STAT = "IMS_FILTRATION_STAT";
      public const string IMS_OPTIMIZER_STAT = "IMS_OPTIMIZER_STAT";
      public const string IMS_METADATA = "IMS_METADATA";
      public const string IMS_GUID_RESOLVE = "IMS_GUID_RESOLVE";
      public const string IMS_ATTR_HISTORY = "IMS_ATTR_HISTORY";
      public const string IMS_TIMED_EVENTS = "IMS_TIMED_EVENTS";
      public const string IMS_STORAGE = "IMS_STORAGE";
      public const string IMS_LCSTART_DATE = "IMS_LCSTART_DATE";
      public const string IMS_CONFIGS = "IMS_CONFIGS";
      public const string IMS_CATEGORY_ACCESS = "IMS_CATEGORY_ACCESS";
      public const string IMS_FORMULA_ATTRS = "IMS_FORMULA_ATTRS";
      public const string IMS_ATTR_GROUPS = "IMS_ATTR_GROUPS";
      public const string IMS_ATTR_IN_GROUPS = "IMS_ATTR_IN_GROUPS";
      public const string IMS_ATTR4OBJ_TYPES = "IMS_ATTR4OBJ_TYPES";
      public const string IMS_ATTR4RELATION_TYPES = "IMS_ATTR4RELATION_TYPES";
      public const string IMS_ATTRIBUTES = "IMS_ATTRIBUTES";
      public const string IMS_DBVERSION = "IMS_DBVERSION";
      public const string IMS_HUMANID_ATTRS = "IMS_HUMANID_ATTRS";
      public const string IMS_LANGUAGES = "IMS_LANGUAGES";
      public const string IMS_LC_STEPS = "IMS_LC_STEPS";
      public const string IMS_LEVELS = "IMS_LEVELS";
      public const string IMS_OBJECT_TYPES = "IMS_OBJECT_TYPES";
      public const string IMS_OBJTYPES_TREE = "IMS_OBJTYPES_TREE";
      public const string IMS_POSSIBLE_VALUES = "IMS_POSSIBLE_VALUES";
      public const string IMS_RELATION_TYPES = "IMS_RELATION_TYPES";
      public const string IMS_OBJECT_LINKS = "IMS_OBJECT_LINKS";
      public const string IMS_ID_LINKS = "IMS_ID_LINKS";
      public const string IMS_SUBJECT_AREAS = "IMS_SUBJECT_AREAS";
      public const string IMS_TYPES_APPLICABILITY = "IMS_TYPES_APPLICABILITY";
      public const string IMS_LC_LINKS = "IMS_LC_LINKS";
      public const string IMS_GUID = "IMS_GUID";
      public const string IMS_OBJECTS = "IMS_OBJECTS";
      public const string IMS_OBJECT_ATTRS = "IMS_OBJECT_ATTRS";
      public const string IMS_RELATION_ATTRS = "IMS_RELATION_ATTRS";
      public const string IMS_BLOBS = "IMS_BLOBS";
      public const string IMS_MEMOS = "IMS_MEMOS";
      public const string IMS_RELATIONS = "IMS_RELATIONS";
      public const string IMS_EVENTLOG = "IMS_EVENTLOG";
      public const string IMS_EVENTLOG_ARC = "IMS_EVENTLOG_ARC";
      public const string TMP_RELATIONS = "TMP_RELATIONS";
      public const string IMS_OBJECTS_VIEW = "IMS_OBJECTS_VIEW";
      public const string IMS_RELATIONS_VIEW = "IMS_RELATIONS_VIEW";
      public const string IMS_SELECTIONS = "IMS_SELECTIONS";
      public const string IMS_VERSIONS_CONTEXT = "IMS_VERSIONS_CONTEXT";
      public const string F_OID = "F_OID";
      public const string F_DELETE_ON_START = "F_DELETE_ON_START";
      public const string F_SERVER_DST = "F_SERVER_DST";
      public const string F_SERVER_SRC = "F_SERVER_SRC";
      public const string F_SERVER_NAME = "F_SERVER_NAME";
      public const string F_CREATOR_ID = "F_CREATOR_ID";
      public const string F_REL_CREATOR = "F_REL_CREATOR";
      public const string F_STORAGE_ID = "F_STORAGE_ID";
      public const string F_ACCESS = "F_ACCESS";
      public const string F_LINKTYPE = "F_LINKTYPE";
      public const string F_AUTHOR = "F_AUTHOR";
      public const string F_PREV_DATE = "F_PREV_DATE";
      public const string F_SCHEDULE = "F_SCHEDULE";
      public const string F_EVENT_KIND = "F_EVENT_KIND";
      public const string F_IMMEDIATE_RUN = "F_IMMEDIATE_RUN";
      public const string F_ERROR_MSG = "F_ERROR_MSG";
      public const string F_WORD = "F_WORD";
      public const string F_WORD_ID = "F_WORD_ID";
      public const string F_TF = "F_TF";
      public const string F_TF_IDF = "F_TF_IDF";
      public const string F_OBJECT_COUNT = "F_OBJECT_COUNT";
      public const string F_CHECKOUT_DATE = "F_CHECKOUT_DATE";
      public const string F_SNAPSHOT_ID = "F_SNAPSHOT_ID";
      public const string F_SNAPSHOT_DATE = "F_SNAPSHOT_DATE";
      public const string F_TABLE_ID = "F_TABLE_ID";
      public const string F_CLASSIVKEY = "F_CLASSIVKEY";
      public const string F_CATALOG_ID = "F_CATALOG_ID";
      public const string F_LINK_ID = "F_LINK_ID";
      public const string F_TEXT = "F_TEXT";
      public const string F_HASHTEXT = "F_HASHTEXT";
      public const string F_LANGUAGES = "F_LANGUAGES";
      public const string F_CULTURE_ID = "F_CULTURE_ID";
      public const string F_PROJECT_ID = "F_PROJECT_ID";
      public const string F_SCHEMA_ID = "F_SCHEMA_ID";
      public const string F_OPTIMIZED = "F_OPTIMIZED";
      public const string F_READ_DURATION = "F_READ_DURATION";
      public const string F_SEEK_DURATION = "F_SEEK_DURATION";
      public const string F_WRITE_DURATION = "F_WRITE_DURATION";
      public const string F_READ = "F_READ";
      public const string F_SEEK = "F_SEEK";
      public const string F_WRITE = "F_WRITE";
      public const string F_PRJ_GUID = "F_PRJ_GUID";
      public const string F_TABLE_NAME = "F_TABLE_NAME";
      public const string F_MASK = "F_MASK";
      public const string F_OPTIONS = "F_OPTIONS";
      public const string F_SET_DATE = "F_SET_DATE";
      public const string F_STATUS = "F_STATUS";
      public const string F_CONTENT = "F_CONTENT";
      public const string F_DEL_TIME = "F_DEL_TIME";
      public const string F_WORK_CAPTION = "F_WORK_CAPTION";
      public const string F_OBJ_CREATE = "F_OBJ_CREATE";
      public const string CAPTION = "CAPTION";
      public const string F_INVIEW = "F_INVIEW";
      public const string F_FIRST = "F_FIRST";
      public const string F_DEFAULT_DESCRIPT = "F_DEFAULT_DESCRIPT";
      public const string F_DESCRIPTION = "F_DESCRIPTION";
      public const string F_TREE_LEVEL = "F_TREE_LEVEL";
      public const string F_MODIFY_MODE = "F_MODIFY_MODE";
      public const string F_PARENT_KEY = "F_PARENT_KEY";
      public const string F_PARAMS = "F_PARAMS";
      public const string F_ROUTE_ID = "F_ROUTE_ID";
      public const string F_PUBLIC_LC = "F_PUBLIC_LC";
      public const string F_FROM_STEP = "F_FROM_STEP";
      public const string F_TO_STEP = "F_TO_STEP";
      public const string F_DELETED = "F_DELETED";
      public const string F_GROUP_NAME = "F_GROUP_NAME";
      public const string F_MULTIPLE_VALUED = "F_MULTIPLE_VALUED";
      public const string F_UNIQUE = "F_UNIQUE";
      public const string F_ANY_ATTRIBUTES = "F_ANY_ATTRIBUTES";
      public const string F_CAPTION_ATTRIBUTE = "F_CAPTION_ATTRIBUTE";
      public const string F_OBJ_NAME = "F_OBJ_NAME";
      public const string F_OBJECTLINK_ID = "F_OBJECTLINK_ID";
      public const string F_ZIPSIZE = "F_ZIPSIZE";
      public const string F_INTEGER_VALUE = "F_INTEGER_VALUE";
      public const string F_DOUBLE_VALUE = "F_DOUBLE_VALUE";
      public const string F_DATE_VALUE = "F_DATE_VALUE";
      public const string F_STRING_VALUE = "F_STRING_VALUE";
      public const string F_LANGUAGE_ID = "F_LANGUAGE_ID";
      public const string F_OBJECT_ID = "F_OBJECT_ID";
      public const string F_PRJLINK_ID = "F_PRJLINK_ID";
      public const string F_LEVEL_ID = "F_LEVEL_ID";
      public const string F_FORMULA = "F_FORMULA";
      public const string F_FORMULA_ID = "F_FORMULA_ID";
      public const string F_FOLDER_ID = "F_FOLDER_ID";
      public const string F_GUID = "F_GUID";
      public const string F_OBJ_GUID = "F_OBJ_GUID";
      public const string F_ATTRIBUTE_TYPE = "F_ATTRIBUTE_TYPE";
      public const string F_ALIAS = "F_ALIAS";
      public const string F_SHORT_NAME = "F_SHORT_NAME";
      public const string F_NAME = "F_NAME";
      public const string F_APPLICABILITY_ID = "F_APPLICABILITY_ID";
      public const string F_DISPLAY = "F_DISPLAY";
      public const string F_VALIDATION_RULE = "F_VALIDATION_RULE";
      public const string F_REQUIRED = "F_REQUIRED";
      public const string F_PUBLIC = "F_PUBLIC";
      public const string F_OBJECT_TYPE = "F_OBJECT_TYPE";
      public const string F_ATTRIBUTE_ID = "F_ATTRIBUTE_ID";
      public const string F_TOOBJECT_ID = "F_TOOBJECT_ID";
      public const string F_AREA_ID = "F_AREA_ID";
      public const string F_AREA_NAME = "F_AREA_NAME";
      public const string F_AREA_NOTE = "F_AREA_NOTE";
      public const string F_NOTE = "F_NOTE";
      public const string F_GROUP_ID = "F_GROUP_ID";
      public const string F_VALUE = "F_VALUE";
      public const string F_PARAM_NAME = "F_PARAM_NAME";
      public const string F_SECTION_ID = "F_SECTION_ID";
      public const string F_USER_ID = "F_USER_ID";
      public const string F_MODULE_NAME = "F_MODULE_NAME";
      public const string F_RIGHT_TYPE = "F_RIGHT_TYPE";
      public const string F_RIGHT_ID = "F_RIGHT_ID";
      public const string F_CONDITION_ID = "F_CONDITION_ID";
      public const string F_CATEGORY_ID = "F_CATEGORY_ID";
      public const string F_CATEGORY_TYPE = "F_CATEGORY_TYPE";
      public const string F_KEY = "F_KEY";
      public const string F_DEFAULT_VALUE = "F_DEFAULT_VALUE";
      public const string F_SIZE_TYPE = "F_SIZE_TYPE";
      public const string F_TYPE_DESCRIPTION = "F_TYPE_DESCRIPTION";
      public const string F_COMPUTED = "F_COMPUTED";
      public const string F_DEFAULT = "F_DEFAULT";
      public const string F_LANGUAGE_NAME = "F_LANGUAGE_NAME";
      public const string F_HUMAN_ID = "F_HUMAN_ID";
      public const string F_IMBASE_KEY = "F_IMBASE_KEY";
      public const string F_AUDIT_TYPE = "F_AUDIT_TYPE";
      public const string F_END_DATE = "F_END_DATE";
      public const string F_BEGIN_DATE = "F_BEGIN_DATE";
      public const string F_EVENT_TYPE = "F_EVENT_TYPE";
      public const string F_COMPUTER_NAME = "F_COMPUTER_NAME";
      public const string F_OBJECT_NAME = "F_OBJECT_NAME";
      public const string F_EVENT_ID = "F_EVENT_ID";
      public const string F_REVISION_ID = "F_REVISION_ID";
      public const string F_VERSION_ID = "F_VERSION_ID";
      public const string F_ID = "F_ID";
      public const string F_DEFAULT_RELATION = "F_DEFAULT_RELATION";
      public const string F_VERSIONABLE = "F_VERSIONABLE";
      public const string F_HUMAN_ID_RULE = "F_HUMAN_ID_RULE";
      public const string F_OBJ_TYPE_NAME = "F_OBJ_TYPE_NAME";
      public const string F_INLIST_ID = "F_INLIST_ID";
      public const string F_PARENT_ID = "F_PARENT_ID";
      public const string F_ICON = "F_ICON";
      public const string F_LITERA = "F_LITERA";
      public const string F_LEVEL_NAME = "F_LEVEL_NAME";
      public const string F_START_DATE = "F_START_DATE";
      public const string F_KEY_ID = "F_KEY_ID";
      public const string F_ACCESS_TYPE = "F_ACCESS_TYPE";
      public const string F_LC_NAME = "F_LC_NAME";
      public const string F_LC_STEP = "F_LC_STEP";
      public const string F_CREATE_DATE = "F_CREATE_DATE";
      public const string F_PART_ID = "F_PART_ID";
      public const string F_PROJ_ID = "F_PROJ_ID";
      public const string F_SAVE_HISTORY = "F_SAVE_HISTORY";
      public const string F_RELATION_KIND = "F_RELATION_KIND";
      public const string F_CHKOUTFILE = "F_CHKOUTFILE";
      public const string F_REVERSE_NAME = "F_REVERSE_NAME";
      public const string F_TYPE_NAME = "F_TYPE_NAME";
      public const string F_RELATION_TYPE = "F_RELATION_TYPE";
      public const string F_PATH_TYPE = "F_PATH_TYPE";
      public const string F_PATH = "F_PATH";
      public const string F_PATH_ID = "F_PATH_ID";
      public const string F_MODIFY_DATE = "F_MODIFY_DATE";
      public const string F_OWNER_ID = "F_OWNER_ID";
      public const string F_OBJECT_VER_TYPE = "F_OBJECT_VER_TYPE";
      public const string F_CHKOUT_BY = "F_CHKOUT_BY";
      public const string F_TRY_COUNT = "F_TRY_COUNT";
      public const string F_DEADLOCK_DATE = "F_DEADLOCK_DATE";
      public const string F_INT_INFO = "F_INT_INFO";
      public const string F_DATE = "F_DATE";
      public const string F_STRING_INFO = "F_STRING_INFO";
      public const string F_GUID_TYPE = "F_GUID_TYPE";
      public const string F_ARC_METHOD = "F_ARC_METHOD";
      public const string F_FILEDATE = "F_FILEDATE";
      public const string F_FILESIZE = "F_FILESIZE";
      public const string F_FILEBODY = "F_FILEBODY";
      public const string F_FILENAME = "F_FILENAME";
      public const string F_FILE_ID = "F_FILE_ID";
      public const string F_DELETE_DATE = "F_DELETE_DATE";
      public const string F_CONSTRAINT_MODE = "F_CONSTRAINT_MODE";
      public const string F_CLONE_RELATIONS = "F_CLONE_RELATIONS";
      public const string F_MIN_LINKS = "F_MIN_LINKS";
      public const string F_MAX_LINKS = "F_MAX_LINKS";
      public const string F_INOBJECT_TYPE = "F_INOBJECT_TYPE";
      public const string F_DRAW_DATA = "F_DRAW_DATA";
      public const string F_MASTER_ID = "F_MASTER_ID";
      public const string F_SOURCE_ID = "F_SOURCE_ID";
      public const string F_CONTEXT_ID = "F_CONTEXT_ID";
      public const string F_MODIFICATION_ID = "F_MODIFICATION_ID";
      public const string F_WORKINGCOPY_ID = "F_WORKINGCOPY_ID";
      public const string F_BASE_VERSION = "F_BASE_VERSION";
      public const string F_SITE_ID = "F_SITE_ID";
      public const string IMS_ATTR_TYPES = "IMS_ATTR_TYPES";
      public const string F_ATTRIBUTE_GUID = "F_ATTRIBUTE_GUID";
      public const string F_UNITS = "F_UNITS";
      public const string IMS_DATA = "IMS_DATA";
      public const string IMS_OBJECTS_FIELDS = "F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID";
      public const string IMS_RELATIONS_FIELDS = "F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR";
      public const string IMS_ATTR4OBJTYPE_VIEW = "IMS_ATTR4OBJTYPE_VIEW";
      public const string IMS_ATTR4RELTYPE_VIEW = "IMS_ATTR4RELTYPE_VIEW";
      /// <summary>Секция с настройками по проектам IPS</summary>
      public const string SectionProject = "PROJECT";
      /// <summary>
      /// Изменять проект объекта при включении его в состав другого объекта, которому присвоен проект
      /// </summary>
      public const string ParamSetProjectOnCreateRelation = "SET_PROJ2CHILD";
      /// <summary>Секция с настройками безопасности</summary>
      public const string SectionSecurity = "SECURITY";
      /// <summary>Секция хранения старых паролей</summary>
      public const string SectionOldPasswords = "OLD_PSW";
      /// <summary>Метод шифрования паролей</summary>
      public const string CryptoMethodParam = "CRYPTO_METHOD";
      /// <summary>Сложный пароль</summary>
      public const string PasswordStrongParam = "STRONG_PSW";
      /// <summary>Минимальная длина пароля</summary>
      public const string PasswordLengthParam = "PSW_LEN";
      /// <summary>Время действия пароля (в днях)</summary>
      public const string PasswordLifetimeParam = "PSW_LIFETIME";
      /// <summary>
      /// Количество запомненных паролей (не давать вводить ранее введенные пароли)
      /// </summary>
      public const string PasswordMemoryParam = "PSW_MEM";
      /// <summary>Могут ли пользователи менять свой пароль</summary>
      public const string PasswordUserChangeParam = "PSW_USER";
      /// <summary>
      /// Автоматическое повышение уровня доступа редактируемых объектов
      /// </summary>
      public const string AccessLevelUpParam = "ACC_AUTO_UP";
      /// <summary>
      /// Разрешить секретные объекты включать в состав объектов с общим доступом
      /// </summary>
      public const string Secret2PublicParam = "SECRET2PUBLIC";
      /// <summary>Время жизни кэша прав доступа</summary>
      public const string AccessCacheLifetime = "ACC_CACHE";
      /// <summary>Количество неверных попыток логина до блокировки</summary>
      public const string WrongPasswordsCount = "WRONG_PSW_COUNT";
      /// <summary>Проверять ли видимость объектов в списках объектов</summary>
      public const string AccessObjectVisibility = "CHECK_LISTS";
      /// <summary>Проверять ли видимость объектов в списках объектов</summary>
      public const string CheckAttrLCStepAccess = "CHECK_ATTR_LCACCESS";
      public const string LCdataSchema = "schema";
      public const string LCdataNodes = "nodes";
      public const string LCdataLinks = "links";
      public const string LCdataLink = "link";
      public const string LCdataNode = "node";
      public const string LCdataPart = "part";
      public const string LCdataPort = "port";
      public const string LCdataX = "x";
      public const string LCdataY = "y";
      public const string LCdataGuid = "guid";
      public const string LCdataFromPort = "fromport";
      public const string LCdataFromArrow = "fromarrow";
      public const string LCdataToPort = "toport";
      public const string LCdataToArrow = "toarrow";
      /// <summary>Показывать состав как есть</summary>
      public const int filtrationNormalComposition = 0;
      /// <summary>Не показывать состав у объектов со скрытым составом</summary>
      public const int filtrationHiddenChilds = 1;
      /// <summary>
      /// Не показывать объекты со скрытым составом, а также их составы
      /// </summary>
      public const int filtrationHiddenComposition = 2;
      /// <summary>Общий контекст</summary>
      public const long filtrationCommonContext = 0;
      /// <summary>Конструкторский контекст</summary>
      public const long filtrationDesignContext = 1;
      /// <summary>Технологический контекст</summary>
      public const long filtrationTechContext = 2;
      /// <summary>Производственный контекст</summary>
      public const long filtrationTech2Context = 3;
      /// <summary>
      /// Данный ключ передаётся в дополнительных настройках Tag параметров запроса в коллекцию объектов.
      /// Позволяет включать/выключать фильтрацию списка объектов по атрибуту "Видимость объекта".
      /// Значение ключа - bool, true - фильтрация включена.
      /// Отсутствие ключа или false - фильтрация отключена.
      /// </summary>
      public const string ObjectListsFiltrationVisibility = "{7FB30639-2F65-4407-B78E-523547B1B133}";
      /// <summary>
      /// Данный ключ передаётся в дополнительных настройках Tag параметров запроса в коллекцию связей.
      /// Значением по ключу является экземпляр типа RelationPair. Значение позволяет указать
      /// происхождение родительского объекта, состав которого запрашивается.
      /// </summary>
      public const string ParentObjectKey = "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}";
      /// <summary>
      /// Данный ключ передаётся в дополнительных настройках Tag параметров запроса в коллекцию связей.
      /// Значением по ключу является некое уникальное число, характеризующее текущий сеанс связи
      /// клиента IPS с сервером приложений.
      /// </summary>
      public const string Handle = "{73ECDAC5-17AF-4C9B-89FA-FECAC5C8FB8C}";
      /// <summary>
      /// Данный ключ передаётся в дополнительных настройках Tag параметров запроса в коллекцию связей.
      /// Значением по ключу является Dictionary(long, int) вида : ид. версии объекта -&gt; тип объекта.
      /// Используется для сервиса подбора версий состава.
      /// </summary>
      public const string ObjectTypesDbParam = "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}";
      /// <summary>
      /// Ключ дополнительных параметров в запросе применяемостей для списка версий объектов.
      /// Значением по ключу является Dictionary(long, long) вида : ид. объекта -&gt; ид. версии объекта.
      /// </summary>
      public const string FindApplicabilitiesForPartVersionsListParamsKey = "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}";
      public const string UseStoredExplicitPartVersionIDParamsKey = "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}";
      public const string ShowNotOwnedWorkCopies = "ShowNotOwnedWorkCopies";
      /// <summary>
      /// Данный ключ может передаваться в дополнительных настройках Tag параметров запроса в коллекцию объектов.
      /// Позволяет указать опции выбора вложенных локальных типов, в качестве значения - класс LocalTypesSelector и производные
      /// </summary>
      public const string LocalTypesSelector = "LocalTypesSelector";
      public static string AllAttributesGroupName = LocalizationHolder.rm.GetString("Interfaces_134");
      public static string SystemAttributesGroupName = LocalizationHolder.rm.GetString("Interfaces_135");
      public const string DBSecurity = "DBSecurity";
      public const string GetAllFields = "ALL_FIELDS";
      /// <summary>зачитывать и переназначать из config имя SystemAdmin</summary>
      public static string SystemAdmin = "INTERMECH";
      public const long NoObject = -1;
      public const int NoType = -1;
      public const int NoRelation = -1;
      /// <summary>Обозначает неопределенный тип объекта.</summary>
      public const int NavigatorUndefinedObjectTypeID = -1;
      /// <summary>Обозначает неопределенный тип связи.</summary>
      public const int NavigatorUndefinedRelationTypeID = -1;
      /// <summary>Обозначает неопределенный идентификатор связи.</summary>
      public const int NavigatorUndefinedPrjLinkID = -1;
      /// <summary>Обозначает неопределенный идентификатор атрибута.</summary>
      public const int NavigatorUndefinedAttributeID = -10000;
      /// <summary>
      /// Обозначает неопределенный идентификатор версии объекта.
      /// </summary>
      public const long NavigatorUndefinedObjectID = 0;
      /// <summary>Обозначает неопределенный идентификатор применимости.</summary>
      public const int NavigatorUndefinedApplicabilityID = -1;
      /// <summary>
      /// Неопределенное значение идентификатора версии объекта.
      /// </summary>
      public const long UnknownObjectId = 0;
      /// <summary>Неопределенное значение идентификатора связи.</summary>
      public const long UnknownPrjLinkId = 0;
      /// <summary>Неопределенное значение идентификатора атрибута.</summary>
      public const int UnknownAttributeId = 0;
      /// <summary>Неопределенное значение идентификатора блоба.</summary>
      public const long UnknownBlobId = 0;
      /// <summary>
      /// Неопределенное значение идентификатора типа объектов.+-
      /// </summary>
      public const int UnknownObjectTypeId = -1;
      /// <summary>Неопределенное значение идентификатора типа связи.</summary>
      public const int UnknownRelationTypeId = -1;
      /// <summary>Неопределенное значение идентификатора шага ЖЦ</summary>
      public const int UnknownLCStepId = -1;
      /// <summary>
      /// Неопределенное значение идентификатора уровня продвижения
      /// </summary>
      public const int UnknownLevelId = 0;
      /// <summary>Неопределенное значение идентификатора схемы ЖЦ</summary>
      public const int UnknownSchemeId = 0;
      /// <summary>Неопределенное значение идентификатора итерации</summary>
      public const long UnknownIterationId = 0;
      public const string mde_AttributeObjectLinkIDs = "OBJ_LINKS_ID";
      public const string mde_AttributePossibleMU_IDs = "MU_PHYSICAL_ID";
      /// <summary>
      /// Префикс для поддержки длинных путей
      /// https://docs.microsoft.com/ru-ru/windows/win32/fileio/maximum-file-path-limitation?tabs=cmd
      /// </summary>
      public const string LongPathPrefix = "\\\\?\\";
      /// <summary>
      /// Глобальная настройка (сейчас не инициализируется: на будущее, если понадобится)
      /// </summary>
      public static bool LongPathEnabled = false;

      public static void InitCategoryNames()
      {
        if (Consts.CategoryNames.Count != 0)
          return;
        Consts.CategoryNames[3] = LocalizationHolder.rm.GetString("Interfaces_548");
        Consts.CategoryNames[12] = LocalizationHolder.rm.GetString("Interfaces_549");
        Consts.CategoryNames[10] = LocalizationHolder.rm.GetString("Interfaces_550");
        Consts.CategoryNames[9] = LocalizationHolder.rm.GetString("Interfaces_551");
        Consts.CategoryNames[8] = LocalizationHolder.rm.GetString("Interfaces_552");
        Consts.CategoryNames[7] = LocalizationHolder.rm.GetString("Interfaces_553");
        Consts.CategoryNames[2] = LocalizationHolder.rm.GetString("Interfaces_554");
        Consts.CategoryNames[4] = LocalizationHolder.rm.GetString("Interfaces_555");
        Consts.CategoryNames[1] = LocalizationHolder.rm.GetString("Interfaces_556");
        Consts.CategoryNames[5] = LocalizationHolder.rm.GetString("Interfaces_557");
        Consts.CategoryNames[6] = LocalizationHolder.rm.GetString("Interfaces_558");
        Consts.CategoryNames[11] = LocalizationHolder.rm.GetString("Interfaces_559");
        Consts.CategoryNames[16 /*0x10*/] = LocalizationHolder.rm.GetString("Interfaces_560");
        Consts.CategoryNames[14] = LocalizationHolder.rm.GetString("Interfaces_561");
        Consts.CategoryNames[0] = LocalizationHolder.rm.GetString("Interfaces_562");
        Consts.CategoryNames[17] = LocalizationHolder.rm.GetString("Interfaces_563");
        Consts.CategoryNames[18] = LocalizationHolder.rm.GetString("Interfaces_564");
        Consts.CategoryNames[19] = LocalizationHolder.rm.GetString("Interfaces_796");
        Consts.CategoryNames[23] = LocalizationHolder.rm.GetString("Interfaces_798");
        Consts.CategoryNames[24] = LocalizationHolder.rm.GetString("Interfaces_799");
        Consts.CategoryNames[25] = "Запись таблицы Imbase";
        Consts.CategoryNames[26] = "Атрибут в таблице Imbase";
        Consts.CategoryNames[30] = "Индекс Imbase";
      }

      public static string GetCategoryName(int categoryType)
      {
        Consts.InitCategoryNames();
        return Consts.CategoryNames.ContainsKey(categoryType) ? Consts.CategoryNames[categoryType] : string.Empty;
      }

      public static int[] GetCategoryTypeIds()
      {
        Consts.InitCategoryNames();
        return Consts.CategoryNames.Keys.ToArray<int>();
      }

      public static string ConvertBoolToString(bool value) => value ? Consts.YesValue : Consts.NoValue;

      public static string ConvertBoolToString(object value)
      {
        return Consts.ConvertBoolToString(value != null && value != DBNull.Value && Convert.ToBoolean(value));
      }

      public static RDBMSList RDBMS
      {
        set => Consts._RDBMS = value;
      }

      /// <summary>функция для определения используемой СУБД</summary>
      /// <returns></returns>
      public static bool IsOracle() => Consts._RDBMS == RDBMSList.Oracle;

      /// <summary>функция для определения используемой СУБД</summary>
      /// <returns></returns>
      public static bool IsMSSQL() => Consts._RDBMS == RDBMSList.MSSQL;

      /// <summary>функция для определения используемой СУБД</summary>
      /// <returns></returns>
      public static bool IsLinter() => Consts._RDBMS == RDBMSList.Linter;

      /// <summary>функция для определения используемой СУБД</summary>
      /// <returns></returns>
      public static bool IsPostgreSQL() => Consts._RDBMS == RDBMSList.PostgreSQL;

      public static string DbAdmin()
      {
        if (Consts.IsOracle())
          return "SYSTEM";
        return Consts.IsMSSQL() ? "sa" : "";
      }

      /// <summary>по категории получить наименование ключевого поля</summary>
      /// <param name="category"></param>
      /// <returns></returns>
      public static string KeyFieldByCategory(int category)
      {
        string str = string.Empty;
        switch (category)
        {
          case 1:
            str = "F_OBJECT_ID";
            break;
          case 3:
            str = "F_ATTRIBUTE_ID";
            break;
          case 4:
            str = "F_OBJECT_TYPE";
            break;
          case 6:
            str = "F_RELATION_TYPE";
            break;
          case 8:
            str = "F_LEVEL_ID";
            break;
          case 9:
            str = "F_LANGUAGE_ID";
            break;
          case 11:
            str = "F_AREA_ID";
            break;
          case 12:
            str = "F_GROUP_ID";
            break;
          case 16 /*0x10*/:
            str = "F_SCHEMA_ID";
            break;
        }
        return str;
      }

      /// <summary>Неопределённый идентификатор объекта.
      /// Сравнивает идентификатор с константами UnknownObjectId и NoObject
      /// и возвращает true, если он равен одной из них</summary>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsUndefinedObjectId(long id) => id == 0L || id == -1L;

      /// <summary>Неопределённый идентификатор связи.
      /// Сравнивает идентификатор с константами UnknownPrjLinkId и NoRelation
      /// и возвращает true, если он равен одной из них</summary>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsUndefinedRelationId(long id) => id == 0L || id == -1L;

      /// <summary>Возвращает префикс для путей</summary>
      /// <param name="longPathSupport"></param>
      /// <returns></returns>
      public static string PathPrefix(bool longPathSupport)
      {
        return !longPathSupport ? string.Empty : "\\\\?\\";
      }

      /// <summary>
      /// Возвращает префикс для путей в соответствии с глобальной настройкоой
      /// </summary>
      /// <returns></returns>
      public static string PathPrefix() => Consts.PathPrefix(Consts.LongPathEnabled);
    }
}
