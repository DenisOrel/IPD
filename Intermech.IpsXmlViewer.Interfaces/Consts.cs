// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.Consts
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Константы</summary>
public class Consts
{
  /// <summary>Неизвестный или несуществующий объект, связь, атрибут</summary>
  public const int UnknownIDx32 = 0;
  /// <summary>Неизвестный или несуществующий объект, связь, атрибут</summary>
  public const long UnknownIDx64 = 0;
  /// <summary>Информация об атрибуте - "ATTRIBUTE"</summary>
  public const string xmlATTRIBUTE = "ATTRIBUTE";
  /// <summary>Информация о типе атрибута - "ATTRIBUTE_TYPE"</summary>
  public const string xmlATTRIBUTE_TYPE = "ATTRIBUTE_TYPE";
  /// <summary>Информация о типах атрибутов - "ATTRIBUTE_TYPES"</summary>
  public const string xmlATTRIBUTE_TYPES = "ATTRIBUTE_TYPES";
  /// <summary>Атрибуты объектов / связей - "ATTRIBUTES"</summary>
  public const string xmlATTRIBUTES = "ATTRIBUTES";
  /// <summary>Секция с информацией о метаданных - "METADATABRIEF"</summary>
  public const string xmlMETADATABRIEF = "METADATABRIEF";
  /// <summary>Информация об объекте - "OBJECT"</summary>
  public const string xmlOBJECT = "OBJECT";
  /// <summary>Информация о типе объекта - "OBJECT_TYPE"</summary>
  public const string xmlOBJECT_TYPE = "OBJECT_TYPE";
  /// <summary>Информация о типах объектов - "OBJECT_TYPES"</summary>
  public const string xmlOBJECT_TYPES = "OBJECT_TYPES";
  /// <summary>Секция с информацией об объектах - "OBJECTSDATASET"</summary>
  public const string xmlOBJECTSDATASET = "OBJECTSDATASET";
  /// <summary>Информация о связи - "RELATION"</summary>
  public const string xmlRELATION = "RELATION";
  /// <summary>Информация о типе связи - "RELATION_TYPE"</summary>
  public const string xmlRELATION_TYPE = "RELATION_TYPE";
  /// <summary>Информация о типах связей - "RELATION_TYPES"</summary>
  public const string xmlRELATION_TYPES = "RELATION_TYPES";
  /// <summary>Секция с данными связей - "RELATIONSDATASET"</summary>
  public const string xmlRELATIONSDATASET = "RELATIONSDATASET";
  /// <summary>Дата создания (UTC) - "F_CREATE_DATE"</summary>
  public const string F_CREATE_DATE = "F_CREATE_DATE";
  /// <summary>Глобальный идентификатор в системе IPS - "F_GUID"</summary>
  public const string F_GUID = "F_GUID";
  /// <summary>Наименование - "F_NAME"</summary>
  public const string F_NAME = "F_NAME";
  /// <summary>Имя файла для иконки типа объекта - "F_ICON"</summary>
  public const string F_ICON = "F_ICON";
  /// <summary>Локальный идентификатор объекта - "F_ID"</summary>
  public const string F_ID = "F_ID";
  /// <summary>
  /// Глобальный уникальный идентификатор объекта в IPS - "F_IDGUID"
  /// </summary>
  public const string F_IDGUID = "F_IDGUID";
  /// <summary>Наименование типа объекта - "F_OBJ_TYPE_NAME"</summary>
  public const string F_OBJ_TYPE_NAME = "F_OBJ_TYPE_NAME";
  /// <summary>
  /// Локальный уникальный идентификатор типа объекта в документе - "F_OBJ_TYPE"
  /// </summary>
  public const string F_OBJ_TYPE = "F_OBJ_TYPE";
  /// <summary>
  /// Локальный уникальный идентификатор типа объекта в документе в базе-приёмнике IPS - "IPS_F_OBJ_TYPE"
  /// </summary>
  public const string IPS_F_OBJ_TYPE = "IPS_F_OBJ_TYPE";
  /// <summary>Наименование типа объекта - "F_TYPE_NAME"</summary>
  public const string F_TYPE_NAME = "F_TYPE_NAME";
  /// <summary>Псевдоним атрибута - "F_ALIAS"</summary>
  public const string F_ALIAS = "F_ALIAS";
  /// <summary>
  /// Метод упаковки файла (целочисленное значение: 0 – не упакован, 1 – zip) - "F_ARC_METHOD"
  /// </summary>
  public const string F_ARC_METHOD = "F_ARC_METHOD";
  /// <summary>
  /// Локальный уникальный идентификатор атрибута в документе - "F_ATTRIBUTE_ID"
  /// </summary>
  public const string F_ATTRIBUTE_ID = "F_ATTRIBUTE_ID";
  /// <summary>
  /// Локальный уникальный идентификатор атрибута в документе в базе-приёмнике IPS - "IPS_F_ATTRIBUTE_ID"
  /// </summary>
  public const string IPS_F_ATTRIBUTE_ID = "IPS_F_ATTRIBUTE_ID";
  /// <summary>
  /// Тип данных атрибута (строковое, целочисленное, файл, ccылка на объект) - "F_ATTRIBUTE_TYPE"
  /// </summary>
  public const string F_ATTRIBUTE_TYPE = "F_ATTRIBUTE_TYPE";
  /// <summary>
  /// Дата/время (UTC) в формате нейтральной языковой культуры (Invariant Cultute) - "F_DATE_VALUE"
  /// </summary>
  public const string F_DATE_VALUE = "F_DATE_VALUE";
  /// <summary>
  /// Вещественная составляющая значения атрибута в формате нейтральной языковой культуры - "F_DOUBLE_VALUE"
  /// </summary>
  public const string F_DOUBLE_VALUE = "F_DOUBLE_VALUE";
  /// <summary>Относительный путь к файлу - "F_FILE"</summary>
  public const string F_FILE = "F_FILE";
  /// <summary>Реальный размер файла (распакованного) - "F_FILESIZE"</summary>
  public const string F_FILESIZE = "F_FILESIZE";
  /// <summary>
  /// Порядковый номер значения атрибута (начинается с 0) - "F_INLIST_ID"
  /// </summary>
  public const string F_INLIST_ID = "F_INLIST_ID";
  /// <summary>
  /// Целочисленная составляющая значения атрибута - "F_INTEGER_VALUE"
  /// </summary>
  public const string F_INTEGER_VALUE = "F_INTEGER_VALUE";
  /// <summary>
  /// Строковая составляющая значения атрибута - "F_STRING_VALUE"
  /// </summary>
  public const string F_STRING_VALUE = "F_STRING_VALUE";
  /// <summary>
  /// Значение атрибута. Формируется в зависимости от типа атрибута - "F_VALUE"
  /// </summary>
  public const string F_VALUE = "F_VALUE";
  /// <summary>Заголовок объекта - "CAPTION"</summary>
  public const string CAPTION = "CAPTION";
  /// <summary>
  /// Локальный идентификатор пользователя, взявшего версию объекта на редактирование
  /// (может не указываться, если не взят на изменение) - "F_CHKOUT_BY"
  /// </summary>
  public const string F_CHKOUT_BY = "F_CHKOUT_BY";
  /// <summary>
  /// Глобальный идентификатор пользователя в IPS, взявшего версию объекта на редактирование
  /// (может не указываться, если не взят на изменение) - "F_CHKOUTGUID"
  /// </summary>
  public const string F_CHKOUTGUID = "F_CHKOUTGUID";
  /// <summary>
  /// Локальный идентификатор этапа жизненного цикла - "F_LC_STEP"
  /// </summary>
  public const string F_LC_STEP = "F_LC_STEP";
  /// <summary>
  /// Локальный идентификатор уровня продвижения - "F_LEVEL_ID"
  /// </summary>
  public const string F_LEVEL_ID = "F_LEVEL_ID";
  /// <summary>
  /// Дата последней модификации объекта (UTC) - "F_MODIFY_DATE"
  /// </summary>
  public const string F_MODIFY_DATE = "F_MODIFY_DATE";
  /// <summary>Дата создания версии объекта (UTC) - "F_OBJ_CREATE"</summary>
  public const string F_OBJ_CREATE = "F_OBJ_CREATE";
  /// <summary>
  /// Локальный идентификатор версии объекта - "F_OBJECT_ID"
  /// </summary>
  public const string F_OBJECT_ID = "F_OBJECT_ID";
  /// <summary>
  /// Локальный идентификатор версии объекта в базе-приёмнике IPS - "IPS_F_OBJECT_ID"
  /// </summary>
  public const string IPS_F_OBJECT_ID = "IPS_F_OBJECT_ID";
  /// <summary>
  /// Признак версии/экземпляра/актуальной версии - "F_OBJECT_VER_TYPE"
  /// </summary>
  public const string F_OBJECT_VER_TYPE = "F_OBJECT_VER_TYPE";
  /// <summary>
  /// Глобальный идентификатор версии объекта в IPS - "F_OBJECTGUID"
  /// </summary>
  public const string F_OBJECTGUID = "F_OBJECTGUID";
  /// <summary>
  /// Локальный идентификатор объекта-владельца - "F_OWNER_ID"
  /// </summary>
  public const string F_OWNER_ID = "F_OWNER_ID";
  /// <summary>
  /// Глобальный уникальный идентификатор объекта-владельца в IPS - "F_OWNERGUID"
  /// </summary>
  public const string F_OWNERGUID = "F_OWNERGUID";
  /// <summary>Порядковый номер версии объекта - "F_VERSION_ID"</summary>
  public const string F_VERSION_ID = "F_VERSION_ID";
  /// <summary>
  /// Локальный уникальный идентификатор типа объекта в документе - "F_OBJECT_TYPE"
  /// </summary>
  public const string F_OBJECT_TYPE = "F_OBJECT_TYPE";
  /// <summary>
  /// 
  /// </summary>
  public const string F_CREATOR_ID = "F_CREATOR_ID";
  /// <summary>
  /// 
  /// </summary>
  public const string F_REL_CREATOR = "F_REL_CREATOR";
  /// <summary>Дата удаления связи - "F_DELETE_DATE"</summary>
  public const string F_DELETE_DATE = "F_DELETE_DATE";
  /// <summary>
  /// Глобальный идентификатор дочернего объекта (не версии) в IPS - "F_PART_ID"
  /// </summary>
  public const string F_PART_ID = "F_PART_ID";
  /// <summary>
  /// Локальный идентификатор версии дочернего объекта - "F_PART_OBJ"
  /// </summary>
  public const string F_PART_OBJ = "F_PART_OBJ";
  /// <summary>Глобальный идентификатор связи в IPS - "F_PRJ_GUID"</summary>
  public const string F_PRJ_GUID = "F_PRJ_GUID";
  /// <summary>
  /// Локальный уникальный идентификатор связи в документе - "F_PRJLINK_ID"
  /// </summary>
  public const string F_PRJLINK_ID = "F_PRJLINK_ID";
  /// <summary>
  /// Локальный уникальный идентификатор связи в документе в базе-приёмнике IPS - "IPS_F_PRJLINK_ID"
  /// </summary>
  public const string IPS_F_PRJLINK_ID = "IPS_F_PRJLINK_ID";
  /// <summary>
  /// Локальный уникальный идентификатор родительского объекта связи в документе в базе-приёмнике IPS - "IPS_F_PROJ_OBJ"
  /// </summary>
  public const string IPS_F_PROJ_OBJ = "IPS_F_PROJ_OBJ";
  /// <summary>
  /// Локальный уникальный идентификатор дочернего объекта связи в документе в базе-приёмнике IPS - "IPS_F_PART_OBJ"
  /// </summary>
  public const string IPS_F_PART_OBJ = "IPS_F_PART_OBJ";
  /// <summary>
  /// Глобальный идентификатор версии родительского объекта в IPS - "F_PROJ_ID"
  /// </summary>
  public const string F_PROJ_ID = "F_PROJ_ID";
  /// <summary>
  /// Локальный идентификатор версии родительского объекта - "F_PROJ_OBJ"
  /// </summary>
  public const string F_PROJ_OBJ = "F_PROJ_OBJ";
  /// <summary>
  /// Локальный уникальный идентификатор типа связи в документе - "F_RELATION_TYPE"
  /// </summary>
  public const string F_RELATION_TYPE = "F_RELATION_TYPE";
  /// <summary>
  /// Локальный уникальный идентификатор типа связи в документе в базе-приёмнике IPS - "IPS_F_RELATION_TYPE"
  /// </summary>
  public const string IPS_F_RELATION_TYPE = "IPS_F_RELATION_TYPE";
  /// <summary>Имя таблицы со списком объектов - "IMS_OBJECTS"</summary>
  public const string sqlIMS_OBJECTS = "IMS_OBJECTS";
  /// <summary>Имя таблицы со списком связей - "IMS_RELATIONS"</summary>
  public const string sqlIMS_RELATIONS = "IMS_RELATIONS";
  /// <summary>Имя таблицы со списком атрибутов - "IMS_ATTRIBUTES"</summary>
  public const string sqlIMS_ATTRIBUTES = "IMS_ATTRIBUTES";
  /// <summary>
  /// Имя таблицы со списком типов объектов - "IMS_OBJECT_TYPES"
  /// </summary>
  public const string sqlIMS_OBJECT_TYPES = "IMS_OBJECT_TYPES";
  /// <summary>
  /// Имя таблицы со списком типов связей - "IMS_RELATION_TYPES"
  /// </summary>
  public const string sqlIMS_RELATION_TYPES = "IMS_RELATION_TYPES";
  /// <summary>
  /// Имя таблицы со списком типов атрибутов - "IMS_ATTRIBUTE_TYPES"
  /// </summary>
  public const string sqlIMS_ATTRIBUTE_TYPES = "IMS_ATTRIBUTE_TYPES";
  /// <summary>Объект - "IS_OBJECT"</summary>
  public const string sqlIS_OBJECT = "IS_OBJECT";
  /// <summary>Владелец - "OWNER_ID"</summary>
  public const string sqlOWNER_ID = "OWNER_ID";
}
