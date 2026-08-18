// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.Consts
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>Константы</summary>
public class Consts
{
  /// <summary>Главный тэг конфигурации</summary>
  public const string MainNodeInCfgFile = "configuration";
  /// <summary>Свойство, имя пользователя</summary>
  public const string CfgPropertyUser = "user";
  /// <summary>Свойство, пароль</summary>
  public const string CfgPropertyPassword = "password";
  /// <summary>
  /// Название тэга конфигурации с инфо по соединению с сервером-приемником
  /// </summary>
  public const string CfgNameDestServer = "destServer";
  /// <summary>
  /// Название тэга конфигурации с инфо по соединению c БД-источником
  /// </summary>
  public const string CfgNameSourceDB = "sourceDB";
  /// <summary>Название тэга конфигурации с инфо синхронизации</summary>
  public const string CfgNameSyncParams = "syncParams";
  /// <summary>Свойство, имя сервера</summary>
  public const string CfgPropertyServer = "server";
  /// <summary>Свойство, имя БД</summary>
  public const string CfgPropertyDataBase = "database";
  /// <summary>Свойство, тип</summary>
  public const string CfgPropertyType = "type";
  /// <summary>Свойство, последней обработанной записи</summary>
  public const string CfgPropertyTimePoint = "timePoint";
  /// <summary>
  /// Свойство-флаг, Закончить выполнение синхронизации при ошибке
  /// </summary>
  public const string CfgPropertyTerminateOnError = "terminateOnError";
  /// <summary>Свойство-флаг, Удалять дубликаты ссылок на таблицы</summary>
  public const string CfgPropertyDeleteDuplicates = "deleteDuplicates";
  /// <summary>Заголовок для окна с ошибкой</summary>
  public const string ErrorMessageHeader = "Ошибка";
  /// <summary>Таблица БД-источника IM_EVENTS</summary>
  public const string IM_EVENTS = "IM_EVENTS";
  /// <summary>Таблица БД-источника IM_FIELDS</summary>
  public const string IM_FIELDS = "IM_FIELDS";
  /// <summary>Таблица БД-источника IM_TABLES</summary>
  public const string IM_TABLES = "IM_TABLES";
  /// <summary>Таблица БД-источника IM_ACCESS</summary>
  public const string IM_ACCESS = "IM_ACCESS";
  /// <summary>Таблица БД-источника IM_BLOBS</summary>
  public const string IM_BLOBS = "IM_BLOBS";
  /// <summary>Таблица IMS_DATA для блоба таблицы Imbase</summary>
  public const string IMS_DATA = "IMS_DATA";
  /// <summary>
  /// Имя датасета IMS_TABLE_RECORDS для блоба таблицы Imbase
  /// </summary>
  public const string IMS_TABLE_RECORDS = "IMS_TABLE_RECORDS";
  public const string IMS_ATTR_TYPES = "IMS_ATTR_TYPES";
  public const string F_ATTRIBUTE_GUID = "F_ATTRIBUTE_GUID";
  public const string CatalogTypeCATALOG = "Каталоги";
  public const string CatalogTypeCTLREF = "Справочники";
  public const string CatalogTypeTECHREF = "Технологические справочники";
  /// <summary>Шаблон имени для нового атрибута</summary>
  public const string NewAttributeName = "{0}({1}-{2})";
  public const string NewAttributeName2 = "{0}({1})";
  /// <summary>Название атрибута в Imbase для шаблона фильтрации</summary>
  public const string NameAttributeTemplate = "ШАБЛОН";
  /// <summary>Расширение файла шаблона фильтрации</summary>
  public const string ExtFileTemplate = ".SETCHR";
  public const string F_KEY = "F_KEY";
  public const string F_OWNER = "F_OWNER";
  public const string F_TYPE = "F_TYPE";
  public const string F_CODE = "F_CODE";
  public const string F_STATE = "F_STATE";
  public const string F_USER = "F_USER";
  public const string F_COMPUTER = "F_COMPUTER";
  public const string F_DATE = "F_DATE";
  public const string F_CATALOG = "F_CATALOG";
  public const string F_FOLDER = "F_FOLDER";
  public const string F_TABLE = "F_TABLE";
  public const string F_OBJKEY = "F_OBJKEY";
  public const string F_SOURCE = "F_SOURCE";
  public const string F_TEXT = "F_TEXT";
  public const string F_DATA = "F_DATA";
  public const string F_DESCR = "F_DESCR";
  public const string F_CREATED = "F_CREATED";
  public const string F_MODIFIED = "F_MODIFIED";
  public const string F_OPENMODE = "F_OPENMODE";
  public const string F_ORDER = "F_ORDER";
  public const string F_TEXTID = "F_TEXTID";
  public const string F_GRAPHID = "F_GRAPHID";
  public const string F_ACCESS = "F_ACCESS";
  public const string F_ACCESS_KEY = "ACCESS_KEY";
  public const string F_ACCESS_GROUPS = "ACCESS_GROUPS";
  public const string F_LEVEL = "F_LEVEL";
  public const string F_NAME = "F_NAME";
  public const string F_SORT = "F_SORT";
  public const string F_MASK = "F_MASK";
  public const string F_TAG1 = "F_TAG1";
  public const string F_TAG2 = "F_TAG2";
  public const string F_TAG3 = "F_TAG3";
  public const string F_TAG4 = "F_TAG4";
  public const string F_USED = "F_USED";
  public const string F_HASH = "F_HASH";
  public const string F_BLOB = "F_BLOB";
  public const string F_FIELD = "F_FIELD";
  public const string F_WIDTH = "F_WIDTH";
  public const string F_LONGNAME = "F_LONGNAME";
  public const string F_SHORTNAME = "F_SHORTNAME";
  public const string F_UNITS = "F_UNITS";
  public const string F_FLAGS = "F_FLAGS";
  public const string F_REQUIRED = "F_REQUIRED";
  public const string F_DATATYPE = "F_DATATYPE";
  public const string F_ENTERMODE = "F_ENTERMODE";
  public const string F_TABLE_ID = "F_TABLE_ID";
  public const string F_DISPLAY = "F_DISPLAY";
  public const string AttributesName = "attributes.xml";
  public const string StructName = "structure.xml";
  public const string DataName = "data.xml";
  public const string InfoName = "info.xml";
  public const string AddDataName = "data.txt";
}
