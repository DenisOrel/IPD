// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Consts
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Константы</summary>
public static class Consts
{
  /// <summary>
  /// Название узла "Настройки модулей расширения импорта/экспорта" - [Extentions]
  /// </summary>
  public const string xmlNodeExtensions = "Extentions";
  /// <summary>
  /// Название узла "Управление расширением импорта/экспорта" - [extention]
  /// </summary>
  public const string xmlNodeExtension = "extention";
  /// <summary>
  /// Название узла "Сценарий модулей расширения импорта/экспорта" - [script]
  /// </summary>
  public const string xmlNodeScript = "script";
  /// <summary>
  /// Название головнгого узла настроек экспорта  - [xmlexportsettings]
  /// </summary>
  public const string xmlNodeExportSettings = "xmlexportsettings";
  /// <summary>
  /// Название головного узла настроек применямости  - [applsettings]
  /// </summary>
  public const string xmlNodeApplicabilitySettings = "applsettings";
  /// <summary>
  /// Название узла настроек применямости  - [applicability]
  /// </summary>
  public const string xmlNodeApplicability = "applicability";
  /// <summary>Название узла с настройками атрибутов</summary>
  public const string xmlNodeAttributes = "attributes";
  /// <summary>Название узла с настройками атрибутов</summary>
  public const string xmlNodeDefAttributes = "default_attributes";
  /// <summary>Название узла c настройками атрибута</summary>
  public const string xmlNodeAttribute = "attribute";
  /// <summary>Название узла c настройками типов объектов</summary>
  public const string xmlNodeObjectTypes = "object_types";
  /// <summary>Название узла c настройками типа связи</summary>
  public const string xmlNodeRelationType = "relation_type";
  /// <summary>Название узла c настройками типов связей</summary>
  public const string xmlNodeRelationTypes = "relation_types";
  /// <summary>Название узла c данными атрибута</summary>
  public const string xmlNodeAttributeValue = "attribute_value";
  /// <summary>
  /// Название узла "Сценарии модулей расширения экспорта" - [Scripts]
  /// </summary>
  public const string xmlNodeScripts = "Scripts";
  /// <summary>
  /// Название корневого узла настроек импорта из XML - [XMLImportSettings]
  /// </summary>
  public const string xmlNodeImportRoot = "XMLImportSettings";
  /// <summary>
  /// Название узла "Правила поиска объектов" (Rules of search for objects) - [SearchRules]
  /// </summary>
  public const string xmlNodeImportSearchRules = "SearchRules";
  /// <summary>
  /// Название узла "Правила импорта объектов" (Rules of import for objects ) - [ObjectImportRules]
  /// </summary>
  public const string xmlNodeObjectImportRules = "ObjectImportRules";
  /// <summary>
  /// Название узла "Правила создания объектов" - [ObjectCreationRules]
  /// </summary>
  public const string xmlNodeObjectCreationRules = "ObjectCreationRules";
  /// <summary>
  /// Название узла с описанием типа объектов - [object_type]
  /// </summary>
  public const string xmlNodeObjectType = "object_type";
  /// <summary>
  /// Название узла, содержащего описание типа атрибута - [attribute]
  /// </summary>
  public const string xmlNodeImportAttribute = "attribute";
  /// <summary>
  /// Название узла "Настройки модулей расширения импорта" - [import]
  /// </summary>
  public const string xmlNodeImport = "import";
  /// <summary>Корневая секция скриптов импорта</summary>
  public const string xmlNodeImportScripts = "SCRIPTS";
  /// <summary>Корневая секция скриптов-cобытий импорта</summary>
  public const string xmlNodeImportActionScripts = "ACTIONS_SCRIPTS";
  /// <summary>
  /// Название атрибута, содержащего формат формирования имени лога для экспорта [logfileformat]
  /// </summary>
  public const string xmlAttrLogFileFormat = "logfileformat";
  /// <summary>
  /// Название атрибута, содержащего формат формирования имени пакета для экспорта [packetfileformat]
  /// </summary>
  public const string xmlAttrPacketFileFormat = "packetfileformat";
  /// <summary>
  /// Название атрибута, содержащего формат формирования имени файла с метаданными[metafileformat]
  /// </summary>
  public const string xmlAttrMetaFileFormat = "metafileformat";
  /// <summary>
  /// Название атрибута, содержащего формат формирования имени файла с данными объектов [objfileformat]
  /// </summary>
  public const string xmlAttrObjFileFormat = "objfileformat";
  /// <summary>
  /// Название атрибута, содержащего формат формирования имени файла с данными связей [relfileformat]
  /// </summary>
  public const string xmlAttrRelFileFormat = "relfileformat";
  /// <summary>
  /// Название атрибута, содержащего формат формирования директории с данными [datadirformat]
  /// </summary>
  public const string xmlAttrDataDirFormat = "datadirformat";
  /// <summary>
  /// Название атрибута, содержащего размер пакета входных данных [chunkitemsize]
  /// </summary>
  public const string xmlAttrChunkItemSize = "chunkitemsize";
  /// <summary>
  /// Название атрибута, содержащего шаблон для формрования имени директории для "подзадачи" [chunkitemsize]
  /// </summary>
  public const string xmlAttrChunkDirFormat = "chunkdirformat";
  /// <summary>
  /// Название атрибута, содержащего наименование TimeZone для выгрузки данных с временем [timezone]
  /// </summary>
  public const string xmlAttrTimeZone = "timezone";
  /// <summary>
  /// Название атрибута, содержащего наименование формат выгрузки даты / времени [datetimeformat]
  /// </summary>
  public const string xmlAttrDateTimeFormat = "datetimeformat";
  /// <summary>
  /// Название атрибута, содержащего режим архивации экспортирумых данных [compress]
  /// </summary>
  public const string xmlAttrCompress = "compress";
  /// <summary>
  /// Название атрибута, содержащего режим архивации экспортирумых данных [task]
  /// </summary>
  public const string xmlAttrTask = "task";
  /// <summary>
  /// Название атрибута, содержащего режим выгрузки контрольной суммы файлов экспортирумых данных [task]
  /// </summary>
  public const string xmlAttrCheckSum = "checksum";
  /// <summary>
  /// Название атрибута, содержащего режим выгрузки доп. данных [extradata]
  /// </summary>
  public const string xmlAttrExtraData = "extradata";
  /// <summary>
  /// Название атрибута, содержащего режим выгрузки атрибутов объектов по умолчанию [defattr]
  /// </summary>
  public const string xmlAttrDefAttr = "defattr";
  /// <summary>
  /// Название атрибута, содержащего правило подбора версий объектов [objverrule]
  /// </summary>
  public const string xmlAttrObjVerRule = "objverrule";
  /// <summary>Название атрибута, содержащего идентификатор - [id]</summary>
  public const string xmlAttrID = "id";
  /// <summary>Название атрибута, содержащего Guid - [guid]</summary>
  public const string xmlAttrGuid = "guid";
  /// <summary>Название атрибута, содержащего имя - [name]</summary>
  public const string xmlAttrName = "name";
  /// <summary>
  /// Название атрибута, содержащего локальный идентификатор - [user_id]
  /// </summary>
  public const string xmlAttrUserID = "user_id";
  /// <summary>
  /// Название атрибута, содержащего локальное имя  - [user_name]
  /// </summary>
  public const string xmlAttrUserName = "user_name";
  /// <summary>
  /// Название атрибута, содержащего локальный псевдоним / алиас  - [user_alias]
  /// </summary>
  public const string xmlAttrUserAlias = "user_alias";
  /// <summary>
  /// Название атрибута, содержащего локальный тип данных  - [user_type]
  /// </summary>
  public const string xmlAttrUserType = "user_type";
  /// <summary>
  /// Название атрибута, содержащего локальное имя ед. измерения  - [user_mc]
  /// </summary>
  public const string xmlAttrUserMC = "user_mc";
  /// <summary>
  /// Название атрибута, содержащего значение (константу) [value]
  /// </summary>
  public const string xmlAttrValue = "value";
  /// <summary>
  /// Название атрибута, содержащего идентификатор типа связи - [reltypeid]
  /// </summary>
  public const string xmlAttrRelTypeID = "reltypeid";
  /// <summary>
  /// Название атрибута, содержащего гл. идентификатор типа связи - [reltype_guid]
  /// </summary>
  public const string xmlAttrRelTypeGuid = "reltype_guid";
  /// <summary>
  /// Название атрибута, содержащего идентификатор типа родительского объекта - [projtypeid]
  /// </summary>
  public const string xmlAttrProjTypeID = "projtypeid";
  /// <summary>
  /// Название атрибута, содержащего гл. идентификатор типа родительского объекта - [projtype_guid]
  /// </summary>
  public const string xmlAttrProjTypeGuid = "projtype_guid";
  /// <summary>
  /// Название атрибута, содержащего идентификатор типа дочернего объекта - [parttypeid]
  /// </summary>
  public const string xmlAttrPartTypeID = "parttypeid";
  /// <summary>
  /// Название атрибута, содержащего гл. идентификатор типа дочернего объекта - [parttype_guid]
  /// </summary>
  public const string xmlAttrPartTypeGuid = "parttype_guid";
  /// <summary>
  /// Название атрибута, содержащего режим обработки применяемости - [applmode]
  /// </summary>
  public const string xmlAttrApplMode = "applmode";
  /// <summary>Название атрибута, содержащего флаги настроек [flags]</summary>
  public const string xmlAttrFlags = "flags";
  /// <summary>
  /// Название атрибута, содержащего режим экспорта атрибутов - [attrmode]
  /// </summary>
  public const string xmlAttrAttrMode = "attrmode";
  /// <summary>
  /// Название атрибута, содержащего режим экспорта атрибутов - [objmodes]
  /// </summary>
  public const string xmlAttrObjMode = "objmodes";
  /// <summary>
  /// Название атрибута, содержащего направление действия настроек - [dirmode]
  /// </summary>
  public const string xmlAttrDirMode = "dirmode";
  /// <summary>
  /// Название атрибута, содержащего режим атрибутов для поиска - [findmode]
  /// </summary>
  public const string xmlAttrFindMode = "findmode";
  /// <summary>Название атрибута, содержащего комментарий</summary>
  public const string xmlAttrComments = "comment";
  /// <summary>
  /// Название атрибута, содержащего режим экспорта [mode]
  /// </summary>
  public const string xmlAttrMode = "mode";
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFInList = "F_INLIST_ID".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFValue = "F_VALUE".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFStringValue = "F_STRING_VALUE".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFIntegerValue = "F_INTEGER_VALUE".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFDateValue = "F_DATE_VALUE".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFDoubleValue = "F_DOUBLE_VALUE".ToLower();
  /// <summary>
  /// 
  /// </summary>
  public static readonly string xmlAttrFGuid = "F_GUID".ToLower();
  /// <summary>
  /// Название атрибута, содержащего Guid словаря - [dictionary]
  /// </summary>
  public const string xmlAttrDictionary = "dictionary";
  /// <summary>
  /// Название атрибута, содержащего режим позволяющий не обрабатывать (не импортировать) сущестствующие  объекты в базе IPS  - [skipExists]
  /// </summary>
  public const string XmlAttrSkipExists = "skipExists";
  /// <summary>
  /// Название атрибута, содержащего Guid шага ЖЦ - [lcStep]
  /// </summary>
  public const string xmlAttrLcStep = "lcStep";
  /// <summary>
  /// Название атрибута, содержащего признак чувствительности к регистру - [casesensitive]
  /// </summary>
  public const string xmlAttrCaseSensitive = "casesensitive";
  /// <summary>
  /// Название атрибута, содержащего настройку игнорирования пробелов в именах - [skipspaces]
  /// </summary>
  [Obsolete("Вместо данного режима следует искать по нормализованному атрибуту")]
  public const string xmlAttrSkipSpaces = "skipspaces";
  /// <summary>Название атрибута, содержащего правило - [rule]</summary>
  public const string xmlAttrRule = "rule";
  /// <summary>
  /// Название атрибута, содержащего логическую операцию - [operation]
  /// </summary>
  public const string xmlAttrOperation = "operation";
  /// <summary>
  /// Название атрибута, содержащего тип объекта для поиска - [search_type]
  /// </summary>
  public const string xmlAttrSearchType = "search_type";
  /// <summary>
  /// Название атрибута, содержащего правило подбора версий - [verrule]
  /// </summary>
  public const string xmlAttrVerRule = "verrule";
  /// <summary>
  /// Название атрибута, содержащего признак разрешения - [enabled]
  /// </summary>
  public const string xmlAttrEnabled = "enabled";
  /// <summary>
  /// Название атрибута, содержащего правило назначение родительской версии объекта [version_owner]
  /// </summary>
  public const string xmlAttrVersionOwner = "version_owner";
  /// <summary>
  /// Название атрибута, содержащего правило назначение версии объекта [version_no]
  /// </summary>
  public const string xmlAttrVersionNo = "version_no";
  /// <summary>
  /// Название атрибута, содержащего идентификатор версии объекта в XML [version_no_attr_id]
  /// </summary>
  public const string xmlAttrVersionNoAttrId = "version_no_attr_id";
  /// <summary>
  /// Название атрибута, содержащего порядок (последовательность) обработки (поиска)
  /// </summary>
  public const string xmlAttrOrder = "order";
  /// <summary>
  /// Путь к узлам [object_type], в которых содержатся описания типов объектов и правила их поиска:
  /// [XMLImportSettings] =&gt; [SearchRules] =&gt; [object_type]
  /// </summary>
  public static readonly string[] xmlImportSearchObjectsPath = new string[2]
  {
    "SearchRules",
    "object_type"
  };
  /// <summary>
  /// Путь к узлам [object_type], в которых содержатся описания типов объектов и правила их импорта:
  /// [XMLImportSettings] =&gt; [xmlNodeObjectImportRules] =&gt; [object_type]
  /// </summary>
  public static readonly string[] xmlObjectImportRulesPath = new string[2]
  {
    "ObjectImportRules",
    "object_type"
  };
  /// <summary>
  /// Путь к узлам [object_type], в которых содержатся описания типов объектов и правила их создания:
  /// [XMLImportSettings] =&gt; [xmlNodeObjectCreationRules] =&gt; [object_type]
  /// </summary>
  public static readonly string[] xmlObjectCreationRulesPath = new string[2]
  {
    "ObjectCreationRules",
    "object_type"
  };
  /// <summary>
  /// Путь к узлам [object_type], в которых содержатся описания типов объектов и правила их создания:
  /// [XMLImportSettings] =&gt; [xmlNodeExtentions] =&gt; [xmlNodeImport]
  /// </summary>
  public static readonly string[] xmlImportExtensions = new string[2]
  {
    "Extentions",
    "import"
  };
  /// <summary>
  /// Правило импорта объектов "обновлять: (очистить состав, если пришёл новый; обновить атрибуты; создать состав)" - [renew]
  /// </summary>
  public const string xmlImport_renew = "renew";
  /// <summary>
  /// Правило импорта объектов "добавлять в состав (обновить атрибуты; дополнить состав)" - [refresh]
  /// </summary>
  public const string xmlImport_refresh = "refresh";
  /// <summary>
  /// Правило импорта объектов "создать новую версию на основе найденной существующей версии (по правилу создания версий [ObjectCreationRules])" - [createVersion]
  /// </summary>
  public const string xmlImport_createVersion = "createVersion";
  /// <summary>
  /// Правило импорта объектов "создать новую версию на основе найденной существующей версии, с копированием дочерних связей средствами ядра (по правилу создания версий [ObjectCreationRules])" - [createVersionKernel]
  /// </summary>
  public const string xmlImport_createVersionKernel = "createVersionKernel";
  /// <summary>
  /// Правило импорта объектов "создать новый объект (по правилу создания версий [ObjectCreationRules])" - [create]
  /// </summary>
  public const string xmlImport_create = "create";
  /// <summary>
  /// Правило импорта объектов "создавать на основе НСИ" - [createByDictionary]
  /// </summary>
  public const string xmlImport_createByDictionary = "createByDictionary";
  /// <summary>Правило импорта объектов "пропускать объект" - [skip]</summary>
  public const string xmlImport_skip = "skip";
  /// <summary>
  /// Правило импорта объектов "создавать новую версию объекта" - [createVersion]
  /// </summary>
  public const string xmlCreate_createVersion = "createVersion";
  /// <summary>
  /// Правило импорта объектов "отыскивать и обновлять базовую версию объекта" - [refreshBase]
  /// </summary>
  public const string xmlCreate_refreshBase = "refreshBase";
  /// <summary>
  /// Правило импорта объектов "отыскивать и обновлять текущую версию объекта" - [refreshVersion]
  /// </summary>
  public const string xmlCreate_refreshVersion = "refreshVersion";
  /// <summary>
  /// Правило импорта объектов "отыскивать и обновлять текущую версию объекта" (с заменой состава) - [refreshVersion]
  /// </summary>
  public const string xmlCreate_renewVersion = "renewVersion";
  /// <summary>
  /// Правило импорта объектов "отыскивать и пропускать текущую версию объекта" - [skipVersion]
  /// </summary>
  public const string xmlCreate_skipVersion = "skipVersion";
  /// <summary>Правило импорта объектов "пропускать объект" - [skip]</summary>
  public const string xmlCreate_skip = "skip";
  /// <summary>
  /// Правила генерации номера версии объекта "назначение максимального номера" - [maxValue]
  /// </summary>
  public const string xmlVersionNo_maxValue = "maxValue";
  /// <summary>
  /// Правила генерации номера версии объекта "назначение номера из XML" - [xmlValue]
  /// </summary>
  /// <remarks>
  /// Назначение номера версии объекта в IPS согласно номеру из XML.
  /// Если версия с таким номером или старше существует в базе IPS, назначается максимальный номер версии</remarks>
  public const string xmlVersionNo_xmlValue = "xmlValue";
  /// <summary>
  /// Правила генерации номера версии объекта "назначение номера из XML с проверкой на новые версии" - [xmlValueSkipOld]
  /// </summary>
  /// <remarks>Назначение номера версии объекта согласно номеру из XML.
  /// Если версия с таким номером или старше существует в базе IPS – объект исключается из импорта</remarks>
  public const string xmlVersionNo_xmlValueSkipOld = "xmlValueSkipOld";
  /// <summary>
  /// Правила поиска родительской версии объекта "По умолчанию" - [default]
  /// </summary>
  /// <remarks>Используется версия объекта, найденная по правилу поиска объекта / версии.</remarks>
  public const string xmlVersionOwner_default = "default";
  /// <summary>
  /// Правила поиска родительской версии объекта "Создание новой версии объекта от базовой" - [base]
  /// </summary>
  public const string xmlVersionOwner_base = "base";
  /// <summary>
  /// Правила поиска родительской версии объекта "Поиск максимальной предыдущей версии" - [previous]
  /// </summary>
  public const string xmlVersionOwner_previous = "previous";
  /// <summary>
  /// Операция «И» между условиями на атрибуты при поиске объектов в базе
  /// </summary>
  public const string xmlOperation_AND = "AND";
  /// <summary>
  /// Операция «Или» между условиями на атрибуты при поиске объектов в базе
  /// </summary>
  public const string xmlOperation_OR = "OR";
  /// <summary>Тип атрибута "Код MDM"</summary>
  public const string attrTypeMDMCode = "cadd945b-306c-11d8-b4e9-00304f19f545";
  /// <summary>Гл. идентификатор атрибута "Код MDM"</summary>
  public static Guid attrTypeMDMGuid = new Guid("cadd945b-306c-11d8-b4e9-00304f19f545");
}
