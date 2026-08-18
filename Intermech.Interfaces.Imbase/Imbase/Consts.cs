// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Consts
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase;

/// <summary>Глобальные константы и переменные Imbase.</summary>
public class Consts
{
  /// <summary>
  /// Флаг, того что константы уже проинициализированны.
  /// Нужен потому, что инициализация может вызываться из разных мест, в часности из TechCard
  /// </summary>
  private static bool _initialized;
  public static readonly Guid LenghtUitsGUID = new Guid("cad002e2-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid LenghtMMGUID = new Guid("cad002e5-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid UnitsObjectTypeGUID = new Guid("cad0000b-306c-11d8-b4e9-00304f19f545");
  public static int UnitsObjectTypeID = -1;
  public static long mmUnitID;
  public static long MeasureLengthID;
  /// <summary>Тип каталога</summary>
  public static readonly Guid CatalogTypeAttGUID = new Guid("cad00200-306c-11d8-b4e9-00304f19f545");
  /// <summary>Сортировка</summary>
  public static readonly Guid ObjectSortOrderAttGUID = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип создаваемого объекта</summary>
  public static readonly Guid CreatedObjectAttGUID = new Guid("cad00203-306c-11d8-b4e9-00304f19f545");
  /// <summary>Создание копии объекта</summary>
  public static readonly Guid CreateNewObjectAttGUID = new Guid("cad00204-306c-11d8-b4e9-00304f19f545");
  /// <summary>Список атрибутов таблицы/Каталога IMBASE</summary>
  public static readonly Guid ImbaseTableViewAttGUID = new Guid("cad0020c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип записей таблицы IMBASE</summary>
  public static readonly Guid ImbaseTableRowsTypeAttGUID = new Guid("cad0020d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Список форм редактирования</summary>
  public static readonly Guid FormListAttributeTypeGuid = new Guid("cad0019d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Внутреннее имя таблицы</summary>
  public static readonly Guid ImbaseInternalTableNameAttGUID = new Guid("cad0020e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Часть старого ключа (Код IMBASE)</summary>
  public static readonly Guid ImbaseInternalOldKeyAttGUID = new Guid("cad0020f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Данные таблицы IMBASE</summary>
  public static readonly Guid ImbaseTableDataAttGUID = new Guid("cad00215-306c-11d8-b4e9-00304f19f545");
  /// <summary>Основной материал</summary>
  public static readonly Guid ImbaseBaseMaterialAttrGuid = new Guid("cae0d224-f228-401f-bff4-8395e19c05a8");
  /// <summary>Марка материала</summary>
  public static readonly Guid ImbaseMaterialGradeAttrGuid = new Guid("424e4095-d402-44f1-b3c8-379ac6e60e8c");
  /// <summary>Текстовое описание</summary>
  public static readonly Guid ImbaseNoteAttGuid = new Guid("cadd9691-306c-11d8-b4e9-00304f19f545");
  /// <summary>Флаги</summary>
  public static readonly Guid ImbaseFlagsAttGuid = new Guid("cad00072-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImbaseTableRecordRefAttGUID = new Guid("cad00205-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на запись каталога IMBASE</summary>
  public static readonly Guid ImbaseCatalogRecordRefAttGUID = new Guid("cad00206-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на каталог IMBASE</summary>
  public static readonly Guid ImbaseCatalogRefAttGUID = new Guid("cad00207-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на папку IMBASE</summary>
  public static readonly Guid ImbaseFolderRefAttGUID = new Guid("cad00208-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на объект IMBASE</summary>
  public static readonly Guid ImbaseObjectRefAttGUID = new Guid("cad00209-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на ярлык таблицы IMBASE</summary>
  public static readonly Guid ImbaseLinkRefAttGUID = new Guid("cad0020a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на таблицу IMBASE</summary>
  public static readonly Guid ImbaseTableRefAttGUID = new Guid("cad0020b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Блоб с таблицей фильтра папок для TECHCARD</summary>
  public static readonly Guid FilterBlobAttGUID = new Guid("cad0146f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на НТД</summary>
  public static readonly Guid ImbaseNTDLinkAttGuid = new Guid("cadd9a3b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Добавлен в состав по ссылке</summary>
  public static readonly Guid IncludeInCompositionByLinkAttGuid = new Guid("cadd99d8-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор объекта Search</summary>
  public static readonly Guid SearchObjDocIdAttrGuid = new Guid("cad0132b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип связи "Объект, добавленный в состав по ссылке"</summary>
  public static readonly Guid IncludeByLinkRelGuid = new Guid("cadd99d7-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Код заготовки (КЗГ)"</summary>
  public static readonly Guid BlankCodeAttrGuid = new Guid("cae0ec15-b08f-4962-bcc9-bbf07a957704");
  public static int BlankCodeAttrID;
  /// <summary>Атрибут "ГОСТ на сортамент"</summary>
  public static readonly Guid StandartAssortmentAttrGuid = new Guid("cae0a32f-a259-4cd5-8ab0-76db6ea785c7");
  public static int StandartAssortmentAttrID;
  /// <summary>Атрибут "ГОСТ"</summary>
  public static readonly Guid StandartAttrGuid = new Guid("cad003de-306c-11d8-b4e9-00304f19f545");
  public static int StandartAttrID;
  /// <summary>Атрибут "Класс"</summary>
  public static readonly Guid ClassAttrGuid = new Guid("cad008d8-306c-11d8-b4e9-00304f19f545");
  public static int ClassAttrID;
  public static int RelationSortIndex;
  /// <summary>Атрибут "Количество записей таблицы"</summary>
  public static readonly Guid ImbaseTableRecordsCountAttGUID = new Guid("cadd98ef-306c-11d8-b4e9-00304f19f545");
  public static int ImbaseTableRecordsCountAttID;
  /// <summary>Атрибут "Дата модификации записи таблицы"</summary>
  public static readonly Guid ImbaseTableRecordModDateAttGUID = new Guid("cadd9c02-306c-11d8-b4e9-00304f19f545");
  public static int ImbaseTableRecordModDateAttID;
  /// <summary>Атрибут "Владелец записи таблицы"</summary>
  public static readonly Guid ImbaseTableRecordOwnerAttGUID = new Guid("cadd9a95-306c-11d8-b4e9-00304f19f545");
  public static int ImbaseTableRecordOwnerAttID;
  public static readonly Guid DenyUseOnCheckInAttrGuid = new Guid("cadd9ac4-306c-11d8-b4e9-00304f19f545");
  public static int DenyUseOnCheckInAttID;
  /// <summary>Атрибут "LIBRARY_IMAGE"</summary>
  public static readonly Guid LibraryImageAttGUID = new Guid("cad0013d-306c-11d8-b4e9-00304f19f545");
  public static int LibraryImageAttID;
  /// <summary>Атрибут "Изображение"</summary>
  public static readonly Guid PictureAttGUID = new Guid("cad0013e-306c-11d8-b4e9-00304f19f545");
  public static int PictureAttID;
  /// <summary>Атрибут "Ссылка на составной объект (запись)"</summary>
  public static readonly Guid LinkToCompoundObjectAttGUID = new Guid("cadd9b44-306c-11d8-b4e9-00304f19f545");
  public static int LinkToCompoundObjectAttID;
  /// <summary>
  /// Атрибут "Ссылка на компонент составного объекта (запись)"
  /// </summary>
  public static readonly Guid LinkToComponentOfCompositeObjectAttGuid = new Guid("cadd9b45-306c-11d8-b4e9-00304f19f545");
  public static int LinkToComponentOfCompositeObjectAttID;
  /// <summary>Базовый объект IMBASE</summary>
  public static readonly Guid ImbaseRootObjectTypeGUID = new Guid("cad00220-306c-11d8-b4e9-00304f19f545");
  /// <summary>Каталог IMBASE</summary>
  public static readonly Guid ImbaseCatalogTypeGUID = new Guid("cad00221-306c-11d8-b4e9-00304f19f545");
  /// <summary>Избранное Imbase</summary>
  public static readonly Guid ImbaseFavoritesTypeGUID = new Guid("cadd99fc-306c-11d8-b4e9-00304f19f545");
  /// <summary>Папка IMBASE</summary>
  public static readonly Guid ImbaseFolderTypeGUID = new Guid("cad00222-306c-11d8-b4e9-00304f19f545");
  /// <summary>Запись Каталога IMBASE</summary>
  public static readonly Guid ImbaseCatalogRecordTypeGUID = new Guid("cad00223-306c-11d8-b4e9-00304f19f545");
  /// <summary>Таблица IMBASE</summary>
  public static readonly Guid ImbaseTableTypeGUID = new Guid("cad00224-306c-11d8-b4e9-00304f19f545");
  /// <summary>Таблица составных элементов</summary>
  public static readonly Guid ImbaseTableMixTypeGUID = new Guid("cadd9b43-306c-11d8-b4e9-00304f19f545");
  /// <summary>Запись таблицы IMBASE</summary>
  public static readonly Guid ImbaseTableRecordTypeGUID = new Guid("cad00225-306c-11d8-b4e9-00304f19f545");
  /// <summary>Выбранный элемент IMBASE</summary>
  public static readonly Guid ImbaseItemTypeGUID = new Guid("cad00226-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ярлык таблицы IMBASE</summary>
  public static readonly Guid ImbaseTableRefTypeGUID = new Guid("cad00227-306c-11d8-b4e9-00304f19f545");
  /// <summary>Двоичный объект Imbase</summary>
  public static readonly Guid ImbaseBLOBTypeGUID = new Guid("cadd9693-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объектов "Свойства материалов"</summary>
  public static readonly Guid MaterialPropertiesObjTypeGuid = new Guid("cadd93d1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объектов "Фильтры IMBASE"</summary>
  public static readonly Guid ImbaseObjFilterTypeGuid = new Guid("cadd93f7-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объектов "Электронная книга"</summary>
  public static readonly Guid PDFBookTypeGuid = new Guid("cad00832-306c-11d8-b4e9-00304f19f545");
  public static int PDFBookTypeID;
  /// <summary>Ссылка на объект типа "Электронная книга" PFD</summary>
  public static readonly Guid PDFBookRefAttGuid = new Guid("cadd9cbf-306c-11d8-b4e9-00304f19f545");
  public static int PDFBookRefAttTypeID;
  /// <summary>Номер страницы в "Электронная книга" PFD</summary>
  public static readonly Guid PDFBookPageAttGuid = new Guid("cadd9cc0-306c-11d8-b4e9-00304f19f545");
  public static int PDFBookPageAttTypeID;
  /// <summary>Guid связи Избранное</summary>
  public static readonly Guid ImbaseFavoritesRelationGUID = new Guid("cadd99fd-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Тип атрибута "Признак владельца фильтра (общий, пользователя, роли, предметной области)"
  /// </summary>
  public static readonly Guid ImbaseFilterOwnerAttrGuid = new Guid("cadd93f9-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid RootNodeGUID = new Guid("4a13a14d-796a-496e-9c6f-96135579f452");
  public static readonly Guid CatalogsNodeGUID = new Guid("c76c7c58-ccfb-42ea-a71e-663708dd2526");
  public static readonly Guid TablesNodeGUID = new Guid("E3449354-FE67-4459-91CE-8EB5F6DC2D5C");
  /// <summary>Привязка к типу объекта, атрибуту</summary>
  public static readonly Guid ObjectTypeAndAttCatalogLink = new Guid("cad005b7-306c-11d8-b4e9-00304f19f545");
  /// Данные шаблона
  public static readonly Guid ImbaseTemplateDataAttGUID = new Guid("cad00212-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ссылка на шаблон</summary>
  public static readonly Guid ImbaseTemplateRefAttGUID = new Guid("cad00213-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объекта Шаблон фильтрации</summary>
  public static readonly Guid ImbaseTemplateTypeGUID = new Guid("cad00228-306c-11d8-b4e9-00304f19f545");
  /// <summary>Сторка с шаблоном у ярлыка и записи таблицы</summary>
  public static readonly Guid ImbaseTemplateAttGUID = new Guid("cad00214-306c-11d8-b4e9-00304f19f545");
  /// <summary>Применяемость</summary>
  public static readonly Guid ImbaseUsingAttGUID = new Guid("cad008fe-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ручной фильтр записей таблицы</summary>
  public static readonly Guid ManualTableFilterAttGUID = new Guid("cad01477-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Несколько объектов (различных типов) IMBASE, выступающих как один объект
  /// </summary>
  public static readonly Guid ImbaseComplexObjectsGuid = new Guid("5650A797-7FFF-42e8-A546-CE6818B1EE0E");
  /// <summary>Папки IMBASE, выступающих как один объект</summary>
  public static readonly Guid ImbaseFoldersGuid = new Guid("47B91743-0FBE-4871-A3CC-1A65A84A4170");
  /// <summary>Записи каталога IMBASE, выступающих как один объект</summary>
  public static readonly Guid ImbaseCatalogRecordsGuid = new Guid("98B0BE66-CF4A-407d-9A67-72281E88F852");
  /// <summary>Ссылки на таблицу IMBASE, выступающих как один объект</summary>
  public static readonly Guid ImbaseTableRefsGuid = new Guid("8E92EEC4-4F55-4fc6-BE4E-BEDBB7077915");
  /// <summary>Папки Избранное, выступающие как один объект</summary>
  public static readonly Guid ImbaseFavoritesGuid = new Guid("28D56171-A437-44E2-8FAD-E797F149488B");
  /// <summary>
  /// Глобальный идентификатор для виртуального узла "Типы объектов" при просмотре объектов созданных из IMBASE.
  /// </summary>
  /// <remarks>В дереве IMBASE становимся на папку, вызываем контекстное меню и выбираем пункт "Созданные объекты...".
  /// В случае если для папки и для некоторых вложенных папок заполнен атрибут "Тип создаваемого объекта" и этих типов несколько,
  /// то в открывшемся окне, в дереве навигатора, эти типы будут подузлами виртуального узла "Типы объектов"</remarks>
  public static readonly Guid ObjectsFromImbaseNodeGuid = new Guid("{1394949C-A740-4b0b-888F-138000A6CACB}");
  /// <summary>
  /// Несколько объектов (различных типов) IMBASE, выступающих как один объект
  /// </summary>
  public static int ImbaseComplexObjectsID;
  /// <summary>Папки IMBASE, выступающих как один объект</summary>
  public static int ImbaseFoldersID;
  /// <summary>Записи каталога IMBASE, выступающих как один объект</summary>
  public static int ImbaseCatalogRecordsID;
  /// <summary>Ссылки на таблицу IMBASE, выступающих как один объект</summary>
  public static int ImbaseTableRefsID;
  /// <summary>Папки избранное IMBASE, выступающих как один объект</summary>
  public static int ImbaseFavoritesID;
  /// Связи
  public static int ImbaseDefaultLinkID;
  /// <summary>Связь - Избранное Imbase</summary>
  public static int ImbaseFavoritesRelationID;
  /// <summary>Базовый объект IMBASE</summary>
  public static int ImbaseRootObjectTypeID;
  /// <summary>Каталог IMBASE</summary>
  public static int ImbaseCatalogTypeID;
  /// <summary>Избранное Imbase</summary>
  public static int ImbaseFavoritesTypeID;
  /// <summary>Папка IMBASE</summary>
  public static int ImbaseFolderTypeID;
  /// <summary>Запись Каталога IMBASE</summary>
  public static int ImbaseCatalogRecordTypeID = -2;
  /// <summary>Таблица IMBASE</summary>
  public static int ImbaseTableTypeID;
  /// <summary>Таблица рецептур</summary>
  public static int ImbaseTableMixTypeID;
  /// <summary>Запись таблицы IMBASE</summary>
  public static int ImbaseTableRecordTypeID;
  /// <summary>Выбранный элемент IMBASE</summary>
  public static int ImbaseItemTypeID;
  /// <summary>
  /// Идентификатор типа объектов "Ссылка на таблицу IMBASE"
  /// </summary>
  public static int ImbaseTableRefTypeID = -1;
  /// <summary>Внутреннее имя таблицы</summary>
  public static int ImbaseInternalTableNameAttID;
  /// <summary>Часть старого ключа</summary>
  public static int ImbaseInternalOldKeyAttID;
  /// <summary>Двоичный объект IMBASE</summary>
  public static int ImbaseBLOBTypeID;
  /// <summary>Тип объекта Шаблон фильтрации</summary>
  public static int ImbaseTemplateTypeID;
  /// <summary>Фильтры IMBASE</summary>
  public static int ImbaseObjFilterTypeID;
  public static int CatalogTypeAttID;
  public static int ObjectSortOrderAttID;
  /// <summary>Атрибут с типом создаваемого объекта</summary>
  public static int CreatedObjectAttID;
  /// <summary>
  /// Атрибут указывает, создавать ли всегда новую копию объекта или искать ранее созданный объект
  /// </summary>
  public static int CreateNewObjectAttID;
  /// <summary>Идентификатор атрибута "Ключ папки классификатора"</summary>
  public static int ClassifFolderKeyAttId;
  /// <summary>Таблица с данными фильтра папок для TECHCARD</summary>
  public static int FilterBlobAttId;
  /// <summary>
  /// Массив идентияикаторов записей выбранных для ручного фильтра
  /// </summary>
  public static int ManualTableFilterId;
  /// <summary>Ссылка на НТД</summary>
  public static int ImbaseNTDLinkAttId;
  /// <summary>Добавлен в состав по ссылке</summary>
  public static int IncludeInCompositionByLinkAttId;
  /// <summary>Тип связи "Объект, добавленный в состав по ссылке"</summary>
  public static int IncludeByLinkRelId;
  public static int ImbaseTableRecordRefAttID;
  public static int ImbaseCatalogRecordRefAttID;
  public static int ImbaseCatalogRefAttID;
  public static int ImbaseFolderRefAttID;
  /// <summary>Ссылка на объект IMBASE</summary>
  public static int ImbaseObjectRefAttID;
  /// <summary>Данные шаблона</summary>
  public static int ImbaseTemplateDataAttID;
  /// <summary>Ссылка на шаблон</summary>
  public static int ImbaseTemplateRefAttID;
  /// <summary>Строка с шаблоном</summary>
  public static int ImbaseTemplateAttID;
  /// <summary>Данные таблицы IMBASE</summary>
  public static int ImbaseTableDataAttID;
  /// <summary>Применяемость</summary>
  public static int ImbaseUsingAttID;
  /// <summary>Ссылка на ярлык таблицы IMBASE</summary>
  public static int ImbaseLinkRefAttID;
  /// <summary>Идентификатор атрибута "Ссылка на таблицу IMBASE"</summary>
  public static int ImbaseTableRefAttID;
  /// <summary>
  /// Атрибут с информацией о группах и другиз параметрах для отображения таблицы
  /// </summary>
  public static int ImbaseTableViewAttID;
  /// <summary>Атрибут с типом записей таблицы IMBASE</summary>
  public static int ImbaseTableRowsTypeAttID;
  /// <summary>Привязка к типу объекта, атрибуту</summary>
  public static int ObjectTypeAndAttCatalogLinkID;
  /// <summary>Основной материал</summary>
  public static int ImbaseBaseMaterialAttrID;
  /// <summary>Марка материала</summary>
  public static int ImbaseMaterialGradeAttrD;
  /// <summary>Текстовое описание</summary>
  public static int ImbaseNoteAttID;
  public static int SiteAttId;
  /// <summary>Флаги</summary>
  public static int ImbaseFlagsAttId;
  /// <summary>
  /// Тип атрибута "Признак владельца фильтра (общий, пользователя, роли, предметной области)"
  /// </summary>
  public static int ImbaseFilterOwnerAttrID;
  /// <summary>
  /// Перечень типов Imbase используемых в дереве каталогов / справочников
  /// </summary>
  /// <remarks>Введен для ускорения поиска объектов по классификатору / построения дерева</remarks>
  public static int[] Imbase_NavTree_ObjectTypeIDS;
  public static int RootNodeCategoryID;
  public static int CatalogsNodeCategoryID;
  public static int TablesNodeCategoryID;
  /// <summary>Список каталогов</summary>
  public static int CatalogsListCategoryId;
  /// <summary>
  /// Категория для виртуального узла "Типы объектов" при просмотре объектов созданных из IMBASE.
  /// </summary>
  /// <remarks>В дереве IMBASE становимся на папку, вызываем контекстное меню и выбираем пункт "Созданные объекты...".
  /// В случае если для папки и для некоторых вложенных папок заполнен атрибут "Тип создаваемого объекта" и этих типов несколько,
  /// то в открывшемся окне, в дереве навигатора, эти типы будут подузлами виртуального узла "Типы объектов"</remarks>
  public static int ObjectsFromImbaseNodeCategoryID;
  public static string F_OBJECT_ID;
  public static string F_OWNER_ID;
  public const string STR_TABLEID = "$IM_TABLEID";
  public const string STR_PARENTID = "$IM_PARENTID";
  /// Имена таблиц и полей в блобе с данными
  public const string IMS_DATA = "IMS_DATA";
  public const string IMS_ATTR_TYPES = "IMS_ATTR_TYPES";
  public const string IMS_TABLE_RECORDS = "IMS_TABLE_RECORDS";
  public const string F_ATTRIBUTE_GUID = "F_ATTRIBUTE_GUID";
  public const string F_UNITS = "F_UNITS";
  public const string F_DISPLAY = "F_DISPLAY";
  public const string F_VALIDATION_RULE = "F_VALIDATION_RULE";
  public const string F_REQUIRED = "F_REQUIRED";
  public const string F_COMPUTED = "F_COMPUTED";
  public const string F_KEY = "F_KEY";
  public const string F_DEFAULT_VALUE = "F_DEFAULT_VALUE";
  public const string F_FORMULA = "F_FORMULA";
  public const string F_UNIQUE = "F_UNIQUE";
  public const string F_MASK = "F_MASK";
  public const string F_MEASURE = "F_MEASURE";
  public const string F_MEASURE_U = "F_MEASURE_U";
  public const string F_OPTIONS = "F_OPTIONS";
  public const string F_FILTERED_POSSIBLE_VALUES = "F_FILTERED_POSSIBLE_VALUES";
  public const string F_DEPEND_POSSIBLE_VALUES = "F_DEPEND_POSSIBLE_VALUES";
  public const string F_GUID = "F_GUID";
  public const string F_DONTCOPY = "F_DONTCOPY";
  public const string F_CHECKCOLUMN = "F_CHECKCOLUMN";
  public const string F_APPLICABILITY = "F_APPLICABILITY";
  public const string F_USERFILTER = "F_USERFILTER";
  public const int F_GUID_ID = -12;
  public const int F_KEY_ID = -2;
  /// Имена расширенных свойств колонок
  public const string F_VIRTUAL = "F_VIRTUAL";
  /// <summary>
  /// Возможность локального редактирования данных (для таблиц импортированных из портала)
  /// </summary>
  public const string F_EDITABLE = "F_EDITABLE";
  /// <summary>Имя модуля, где храняться настройки</summary>
  public const string IMBASEMODULENAME = "IMBASE";
  /// <summary>
  /// Имя секции, где хранилась настройка "Допустимые справочники Imbase"
  /// </summary>
  public const string IMBASEDIRECTORIES = "Imbase directories";

  /// <summary>Инициализация.</summary>
  /// <param name="session"></param>
  /// <param name="metaDataHelper"></param>
  public static void Initialize(IUserSession session, IMetaDataHelper metaDataHelper)
  {
    if (session == null || Consts._initialized)
      return;
    Consts._initialized = true;
    int num = -2;
    Consts.F_OBJECT_ID = num.ToString();
    num = -8;
    Consts.F_OWNER_ID = num.ToString();
    Consts.ImbaseRootObjectTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseRootObjectTypeGUID);
    Consts.ImbaseCatalogTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseCatalogTypeGUID);
    Consts.ImbaseFavoritesTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseFavoritesTypeGUID);
    Consts.ImbaseFolderTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseFolderTypeGUID);
    Consts.ImbaseCatalogRecordTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseCatalogRecordTypeGUID);
    Consts.ImbaseTableTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseTableTypeGUID);
    Consts.ImbaseTableMixTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseTableMixTypeGUID);
    Consts.ImbaseItemTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseItemTypeGUID);
    Consts.ImbaseTemplateTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseTemplateTypeGUID);
    Consts.ImbaseObjFilterTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseObjFilterTypeGuid);
    Consts.PDFBookTypeID = metaDataHelper.GetObjectTypeID(Consts.PDFBookTypeGuid);
    Consts.ImbaseTableRefTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseTableRefTypeGUID);
    Consts.ImbaseBLOBTypeID = metaDataHelper.GetObjectTypeID(Consts.ImbaseBLOBTypeGUID);
    Consts.UnitsObjectTypeID = metaDataHelper.GetObjectTypeID(Consts.UnitsObjectTypeGUID);
    Consts.ImbaseDefaultLinkID = metaDataHelper.GetDefaultRelationTypeID(Consts.ImbaseFolderTypeGUID);
    Consts.ImbaseFavoritesRelationID = metaDataHelper.GetRelationTypeID(Consts.ImbaseFavoritesRelationGUID);
    Consts.IncludeByLinkRelId = metaDataHelper.GetRelationTypeID(Consts.IncludeByLinkRelGuid);
    Consts.PDFBookRefAttTypeID = metaDataHelper.GetAttributeTypeID(Consts.PDFBookRefAttGuid);
    Consts.PDFBookPageAttTypeID = metaDataHelper.GetAttributeTypeID(Consts.PDFBookPageAttGuid);
    Consts.ImbaseFilterOwnerAttrID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseFilterOwnerAttrGuid);
    Consts.CreatedObjectAttID = metaDataHelper.GetAttributeTypeID(Consts.CreatedObjectAttGUID);
    Consts.CreateNewObjectAttID = metaDataHelper.GetAttributeTypeID(Consts.CreateNewObjectAttGUID);
    Consts.ImbaseTableRecordRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRecordRefAttGUID);
    Consts.ImbaseCatalogRecordRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseCatalogRecordRefAttGUID);
    Consts.ImbaseCatalogRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseCatalogRefAttGUID);
    Consts.ImbaseFolderRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseFolderRefAttGUID);
    Consts.ImbaseObjectRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseObjectRefAttGUID);
    Consts.ImbaseInternalTableNameAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseInternalTableNameAttGUID);
    Consts.ImbaseInternalOldKeyAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseInternalOldKeyAttGUID);
    Consts.ImbaseTemplateDataAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTemplateDataAttGUID);
    Consts.ImbaseTemplateRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTemplateRefAttGUID);
    Consts.ImbaseTemplateAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTemplateAttGUID);
    Consts.ImbaseTableDataAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableDataAttGUID);
    Consts.ImbaseUsingAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseUsingAttGUID);
    Consts.ImbaseTableRecordsCountAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRecordsCountAttGUID);
    Consts.ImbaseTableRecordOwnerAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRecordOwnerAttGUID);
    Consts.ImbaseTableRecordModDateAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRecordModDateAttGUID);
    Consts.FilterBlobAttId = metaDataHelper.GetAttributeTypeID(Consts.FilterBlobAttGUID);
    Consts.ImbaseNTDLinkAttId = metaDataHelper.GetAttributeTypeID(Consts.ImbaseNTDLinkAttGuid);
    Consts.IncludeInCompositionByLinkAttId = metaDataHelper.GetAttributeTypeID(Consts.IncludeInCompositionByLinkAttGuid);
    Consts.ManualTableFilterId = metaDataHelper.GetAttributeTypeID(Consts.ManualTableFilterAttGUID);
    Consts.ImbaseLinkRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseLinkRefAttGUID);
    Consts.ImbaseTableRefAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRefAttGUID);
    Consts.ImbaseTableViewAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableViewAttGUID);
    Consts.ImbaseTableRowsTypeAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseTableRowsTypeAttGUID);
    Consts.ImbaseNoteAttID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseNoteAttGuid);
    Consts.ImbaseBaseMaterialAttrID = metaDataHelper.GetAttributeTypeID(Consts.ImbaseBaseMaterialAttrGuid);
    Consts.ImbaseMaterialGradeAttrD = metaDataHelper.GetAttributeTypeID(Consts.ImbaseMaterialGradeAttrGuid);
    Consts.CatalogTypeAttID = metaDataHelper.GetAttributeTypeID(Consts.CatalogTypeAttGUID);
    Consts.ClassifFolderKeyAttId = metaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545");
    Consts.ObjectTypeAndAttCatalogLinkID = metaDataHelper.GetAttributeTypeID(Consts.ObjectTypeAndAttCatalogLink);
    Consts.SiteAttId = metaDataHelper.GetAttributeTypeID("cad01501-306c-11d8-b4e9-00304f19f545");
    Consts.ObjectSortOrderAttID = metaDataHelper.GetAttributeTypeID(Consts.ObjectSortOrderAttGUID);
    Consts.BlankCodeAttrID = metaDataHelper.GetAttributeTypeID(Consts.BlankCodeAttrGuid);
    Consts.StandartAssortmentAttrID = metaDataHelper.GetAttributeTypeID(Consts.StandartAssortmentAttrGuid);
    Consts.StandartAttrID = metaDataHelper.GetAttributeTypeID(Consts.StandartAttrGuid);
    Consts.ClassAttrID = metaDataHelper.GetAttributeTypeID(Consts.ClassAttrGuid);
    Consts.RelationSortIndex = metaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
    Consts.DenyUseOnCheckInAttID = metaDataHelper.GetAttributeTypeID(Consts.DenyUseOnCheckInAttrGuid);
    Consts.LibraryImageAttID = metaDataHelper.GetAttributeTypeID(Consts.LibraryImageAttGUID);
    Consts.PictureAttID = metaDataHelper.GetAttributeTypeID(Consts.PictureAttGUID);
    Consts.LinkToCompoundObjectAttID = metaDataHelper.GetAttributeTypeID(Consts.LinkToCompoundObjectAttGUID);
    Consts.LinkToComponentOfCompositeObjectAttID = metaDataHelper.GetAttributeTypeID(Consts.LinkToComponentOfCompositeObjectAttGuid);
    Consts.mmUnitID = session.GetObject(Consts.LenghtMMGUID).ObjectID;
    Consts.MeasureLengthID = MeasureHelper.FindDescriptor(Consts.mmUnitID).PhysicalQuantityID;
    Consts.Imbase_NavTree_ObjectTypeIDS = new int[4]
    {
      Consts.ImbaseCatalogTypeID,
      Consts.ImbaseFolderTypeID,
      Consts.ImbaseCatalogRecordTypeID,
      Consts.ImbaseTableRefTypeID
    };
  }

  [Flags]
  public enum ImbaseFlags
  {
    ITF_CATALOG = 1,
    ITF_READONLY = 2,
    ITF_SYSTEM = 4,
    ITF_HIDDEN = 8,
    ITF_DESIGN = 16, // 0x00000010
    ITF_TECHNO = 32, // 0x00000020
    ITF_PRIVATE = 64, // 0x00000040
    ITF_UNDELETABLE = 128, // 0x00000080
    ITF_NOINDEXING = 256, // 0x00000100
    ITF_NOINDEXCHECK = 512, // 0x00000200
    ITF_TABLINKED = 65536, // 0x00010000
  }
}
