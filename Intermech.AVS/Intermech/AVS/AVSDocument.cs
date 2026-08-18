// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSDocument
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraTreeList.Nodes;
using Infralution.Controls.VirtualTree;
using Intermech.AVS.AVSProperties;
using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.AVS.Output;
using Intermech.AVS.Sorting;
using Intermech.AVS.Victor;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Snapshots;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Pdm.Substitutes;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс для работы со спецификацией</summary>
public class AVSDocument : 
  IAVSDocument,
  IComparer,
  IDisposable,
  IVirtualTreeItem,
  IEnumerable<AVSRow>,
  IEnumerable
{
  /// <summary>GUID виртуального раздела для записей не определенных в раздел</summary>
  public static readonly Guid SectionUnassignedGuid = new Guid("91C2EDFB-829C-472e-9C9D-8B9F46A5BDD0");
  /// <summary>Guid раздела Общие данные</summary>
  public static readonly Guid ChapterCommonDataGuid = new Guid("{7321C1CF-1E3A-419d-A897-CB3BE9FE9461}");
  /// <summary>Guid раздела для записей в исполнений ПЭ и других конструкторских документов кроме СП</summary>
  public static readonly Guid ChapterVariableDataRowsGuid = new Guid("{452F9087-A479-483F-95A7-92F82D3A90D3}");
  /// <summary>Guid раздела Переменные данные</summary>
  public static readonly Guid ChapterVariableDataGuid = new Guid("{101853C3-5D69-48b5-A8CD-4A52AE6D02F0}");
  /// <summary>Guid раздела Переменные данные формы В</summary>
  public static readonly Guid ChapterVariableDataVGuid = new Guid("{F67F6CC9-D68B-4DC7-82A1-FAD4320852D8}");
  /// <summary>Guid дополнительной части</summary>
  public static readonly Guid AdditionalChapterGuid = new Guid("{9AA596D5-BB64-4e30-997D-CCEBA9D012F5}");
  /// <summary>Guid для выбора пункта "Без прототипа" в списке исполнений</summary>
  public static readonly Guid ProductWithoutPrototypeGuid = new Guid("{9CAAA152-0912-4c29-87F8-6F0A1FC3BBBC}");
  /// <summary>Атрибут узла документа - номера исполнений</summary>
  public static readonly string ProductNumbers_PageAttribute = "IspolnNumbers";
  /// <summary>Атрибут узла документа - номера исполнений</summary>
  public static readonly string ProductGuid_CellAttribute = "ProductGuid";
  /// <summary>Атрибут узла документа - Guid шаблона документа</summary>
  public static readonly string SpecTemplateGuid_DocAttribute = "SpecTemplateGuid";
  /// <summary>Атрибут узла документа - форма спецификации</summary>
  public static readonly string SpecForm_DocAttribute = "SpecForm";
  /// <summary>Атрибут узла документа - Расположение частей</summary>
  public static readonly string AddChapterLocation_DocAttribute = "AddChapterLocation";
  /// <summary>Атрибут узла документа - тип конструкторского документа</summary>
  public static readonly string AVSDocType_DocAttribute = nameof (AVSDocType);
  /// <summary>Атрибут узла документа - тип таблицы</summary>
  public static readonly string AVSTableType_DocAttribute = "AVSTableType";
  /// <summary>Атрибут узла документа - Внутренний Guid типа конструкторского документа</summary>
  public static readonly string AVSDocTypeGuid_DocAttribute = "AVSDocTypeGuid";
  /// <summary>Атрибут документа КодОКП</summary>
  public static readonly string DocumentAttribute_OKPCode = "ОКП";
  /// <summary>Атрибут узла документа - количество пропущенных строк перед элементом</summary>
  public static readonly string DocAttr_SkipLinesBefore = "AVSSkipBefore";
  /// <summary>Атрибут узла документа - количество пропущенных строк перед элементом</summary>
  public static readonly string DocAttr_SkipLinesAfter = "AVSSkipAfter";
  /// <summary>Атрибут документа - список родительских изделий</summary>
  public static readonly string DocAttr_ParentProductList = "ParentProductList";
  public static readonly string AdditionalComplectRowGroupTemplateId = "Дополнительный комплект";
  public static readonly string ChapterWithoutHeaderFormBTemplateId = "Часть без заголовка. Форма Б";
  public const string DynamicHeaderRowTemplateId = "Заголовок группы записей";
  public const string DynamicHeaderRowFormBTemplateId = "Заголовок группы записей формы Б";
  public const string FunctionalGroupHeaderRowTemplateId = "Заголовок функциональной группы";
  private static long objID_SectionDocumentation = -1;
  private static long objID_SectionComplex = -1;
  private static long objID_SectionAssemblyUnits = -1;
  private static long objID_SectionDetail = -1;
  private static long objID_SectionStandartArticles = -1;
  private static long objID_SectionOtherArticles = -1;
  private static long objID_SectionMaterials = -1;
  private static long objID_SectionComplects = -1;
  private static long objID_SectionComplectUnits = -1;
  private static long objID_OldAVSSettingsSpecifications = -1;
  private static long objID_OldAVSSettingsVedomosti = -1;
  private static long _kilogramsID = -1;
  private static long _meterID = -1;
  private static long _squareMeterID = -1;
  private static long _cubicMeterID = -1;
  private static long _kilogramsPerMeterID = -1;
  private static long _kilogramsPerSquareMeterID = -1;
  private static long _kilogramsPerCubicMeterID = -1;
  private static long _gramsPerMeterID = -1;
  private static long _gramsPerSquareMeterID = -1;
  /// <summary>Поля записи документа</summary>
  public List<AvsRowAttributeInfo> docRowFields = new List<AvsRowAttributeInfo>();
  /// <summary>Поля записи документа</summary>
  public List<AvsRowAttributeInfo> docRowFields_Exp = new List<AvsRowAttributeInfo>();
  /// <summary>Поля записи документа</summary>
  public List<AvsRowAttributeInfo> docRowFields_VarFormV = new List<AvsRowAttributeInfo>();
  /// <summary>Атрибуты записи документа</summary>
  public List<AvsRowAttributeInfo> docRowAttributes = new List<AvsRowAttributeInfo>();
  internal AvsRowAttributeInfo Attr_SortIndex;
  internal AvsRowAttributeInfo Attr_Podbor;
  internal AvsRowAttributeInfo Attr_PodborForPosDesignation;
  internal AvsRowAttributeInfo Attr_IncludeInElementList;
  internal AvsRowAttributeInfo Attr_HideInSpecification;
  internal AvsRowAttributeInfo Attr_NominalValue;
  internal AvsRowAttributeInfo Attr_LimitValues;
  internal AvsRowAttributeInfo Field_Format;
  internal AvsRowAttributeInfo Field_Zone;
  internal AvsRowAttributeInfo Field_Position;
  internal AvsRowAttributeInfo Field_Name;
  internal AvsRowAttributeInfo Field_Name_Exp;
  internal AvsRowAttributeInfo Field_Description;
  internal AvsRowAttributeInfo Field_Designation;
  internal AvsRowAttributeInfo Field_Count;
  internal AvsRowAttributeInfo Attr_CountForAdjustment;
  internal AvsRowAttributeInfo Field_Note;
  protected AvsRowAttributeInfo _attr_Note;
  internal AvsRowAttributeInfo Attr_FunctionalGroupPosDesignation;
  internal AvsRowAttributeInfo Field_PosDesignation;
  internal AvsRowAttributeInfo Attr_InMainDocComplect;
  internal AvsRowAttributeInfo Attr_FunctionalGroupDesignation;
  internal AvsRowAttributeInfo Attr_FunctionalGroupName;
  /// <summary>Назначенный пользователем атрибут изделия для замены Наименования в графе</summary>
  internal AvsRowAttributeInfo Attr_UserAttributeForNameField;
  /// <summary>Назначенный пользователем атрибут документа для замены типа документа в графе Наименования</summary>
  internal AvsRowAttributeInfo Attr_UserAttributeForDocType;
  internal AvsRowAttributeInfo Attr_DopZamenGroupNum;
  internal AvsRowAttributeInfo Attr_DopZamenNumInGroup;
  internal AvsRowAttributeInfo Attr_DesignerActualVariant;
  internal AvsRowAttributeInfo Attr_DopZamenText;
  internal AvsRowAttributeInfo Attr_Section;
  internal AvsRowAttributeInfo Attr_SearchId;
  internal AvsRowAttributeInfo Attr_AdditionalChapter;
  internal AvsRowAttributeInfo Attr_SubstitutePositionType;
  internal AvsRowAttributeInfo Attr_SubstitutePositionNumber;
  internal AvsRowAttributeInfo Attr_Class;
  internal AvsRowAttributeInfo Attr_GOST;
  internal AvsRowAttributeInfo Attr_SizeAndParams;
  internal AvsRowAttributeInfo Attr_GroupWithoutClass;
  /// <summary>Необходимо ли проверять значение при изменении текста</summary>
  public bool ValidateValue = true;
  private int PacketSize = 500;
  private int pendingAttributeUpdateModeCounter;
  internal AttributeProcessorDictionary attributeProcessorDictionary = new AttributeProcessorDictionary();
  /// <summary>Словари размещения атрибутов связи в кэше</summary>
  private readonly Dictionary<int, AttributeValueMap> relationsAttributeValueMapDictionary = new Dictionary<int, AttributeValueMap>();
  /// <summary>Словарь размещения атрибутов объектов документов в кэше</summary>
  internal AttributeValueMap docObjectAttrMap;
  /// <summary>Словарь размещения атрибутов объектов изделий в кэше</summary>
  internal AttributeValueMap prjObjectAttrMap;
  /// <summary>Был ли инициализирован PdmConfiguratorCache</summary>
  private static bool PdmConfiguratorCacheLoaded = false;
  /// <summary>Тип конструкторского документа</summary>
  protected AVSDocumentType avsDocumentType;
  /// <summary>Внутренний Guid типа конструкторского документа</summary>
  private Guid avsDocTypeGuid;
  /// <summary>Документ генерируемый на лету, без хранения файла в базе</summary>
  private bool isGeneratedDoc;
  /// <summary>Количество исполнений на запись для формы Б</summary>
  public int RowProductCount = 1;
  /// <summary>Атрибуты исполнений для групповых СП</summary>
  public List<int> productAttributeList;
  private ImDocument document;
  private AVSWindow avsWindow;
  private int suspendDocumentAndGridUpdatesCount;
  private int lock_DocCell_TextChanged_Count;
  internal int suspendReloadDopZamenText;
  internal bool needReloadDopZamenText;
  /// <summary>Флаг устанавливается при обновлении данных в записи с загрузкой атрибутов из БД</summary>
  internal bool IsRowsUpdating;
  internal bool templateUpdated;
  /// <summary>Основные части конструкторского документа</summary>
  internal List<Chapter> rootChapters = new List<Chapter>();
  /// <summary>Часть "Общие данные"</summary>
  public Chapter commonDataChapter;
  /// <summary>Часть "Переменные данные исполнений" для формы А</summary>
  internal VariableDataChapterFormA variableDataChapter_FormA;
  /// <summary>Часть "Переменные данные формы В"</summary>
  internal VariableDataChapterFormV variableDataChapter_FormV;
  private Dictionary<long, AVSRow> relationDictionary = new Dictionary<long, AVSRow>();
  private Dictionary<Guid, AVSRow> relationGuidDictionary = new Dictionary<Guid, AVSRow>();
  private Dictionary<long, List<AVSRow>> objectDictionary = new Dictionary<long, List<AVSRow>>();
  private Dictionary<Guid, List<AVSRow>> objectGuidDictionary = new Dictionary<Guid, List<AVSRow>>();
  internal Dictionary<long, AVSRow> SortIndexDictionary = new Dictionary<long, AVSRow>();
  internal Dictionary<int, Dictionary<object, object>> AttributeDescriptionsCache = new Dictionary<int, Dictionary<object, object>>();
  /// <summary>Словарь связей с подборными компонентами по атрибуту "Подбор для позиционного обозначения" </summary>
  internal Dictionary<string, List<RelationAttributeValuesCache>> PodborForPosDesignation_Dictionary = new Dictionary<string, List<RelationAttributeValuesCache>>();
  /// <summary>Словарь связей по атрибуту "Позиционное обозначение" </summary>
  internal Dictionary<string, RelationAttributeValuesCache> PosDesignation_Dictionary = new Dictionary<string, RelationAttributeValuesCache>();
  /// <summary>Класс для собирания событий с документом</summary>
  private AvsRowEventMessageViewer _avsRowEventMessageViewer;
  internal Guid documentTemplateGuid = Guid.Empty;
  private long documentTemplateID = -1;
  /// <summary>Шаблон страницы</summary>
  internal PageData productsPage2Template;
  /// <summary>Шаблон общей части в конструкторском документе</summary>
  internal TableData commonChapterTemplate;
  /// <summary>Шаблон общей части экспортной спецификации в документе</summary>
  internal TableData commonChapterExpTemplate;
  /// <summary>Главная таблица в конструкторском документе</summary>
  internal TableData avsDocTable;
  /// <summary>Шаблон главной таблицы конструкторского документа</summary>
  internal TableData avsDocTableTemplate;
  /// <summary>Таблица экспортной СП (на совместном листе)</summary>
  internal TableData avsDocTableExpMix;
  /// <summary>Шаблон таблицы экспортной СП (на совместном листе)</summary>
  internal TableData avsDocTableExpMix_Template;
  /// <summary>Таблица экспортной СП (на отдельном листе)</summary>
  internal TableData avsDocTableExpSingle;
  /// <summary>Шаблон таблицы экспортной СП (на отдельном листе)</summary>
  internal TableData avsDocTableExpSingle_Template;
  /// <summary>Таблица экспортной СП. Продолжение 1 (на  совместном листе)</summary>
  internal TableData avsDocTableExpMixP1;
  /// <summary>Шаблон таблицы экспортной СП. Продолжение 1 (на  совместном листе)</summary>
  internal TableData avsDocTableExpMixP1_Template;
  /// <summary>Таблица экспортной СП. Продолжение 2 (на отдельном листе)</summary>
  internal TableData avsDocTableExpSingleP2;
  /// <summary>Шаблон таблицы экспортной СП. Продолжение 2 (на отдельном листе)</summary>
  internal TableData avsDocTableExpSingleP2_Template;
  /// <summary>Таблица СП (на отдельном листе)</summary>
  internal TableData avsDocTableSingleT1;
  /// <summary>Шаблон таблицы СП (на отдельном листе)</summary>
  internal TableData avsDocTableSingleT1_Template;
  /// <summary>Таблица СП. Продолжение 1 (на отдельном листе)</summary>
  internal TableData avsDocTableSingleP2;
  /// <summary>Шаблон таблицы СП. Продолжение 1 (на отдельном листе)</summary>
  internal TableData avsDocTableSingleP2_Template;
  /// <summary>Таблица СП. Продолжение 1 (на совместном листе)</summary>
  internal TableData avsDocTableMixP1;
  /// <summary>Шаблон таблицы СП. Продолжение 2 (на совместном листе)</summary>
  internal TableData avsDocTableMixP1_Template;
  /// <summary>Шаблон листа регистрации изменений</summary>
  internal PageData lriPage_Template;
  /// <summary>Лист регистрации изменений</summary>
  private PageData lriPage;
  /// <summary>Таблица спецификации с переменными данными формы В в документе</summary>
  internal TableData avsFormB_Table;
  /// <summary>Шаблон раздела конструкторского документа</summary>
  internal TableData addiitionalComplectRowGroupTemplate;
  /// <summary>Шаблон раздела конструкторского документа</summary>
  internal TableData sectionTemplate;
  /// <summary>Шаблон раздела спецификации переменных данных формы Б и В в документе</summary>
  internal TableData sectionFormBTemplate;
  /// <summary>Шаблон раздела экспортной спецификации</summary>
  internal TableData sectionExpTemplate;
  /// <summary>Шаблон части спецификации без заголовка</summary>
  internal TableData chapterWithoutHeaderTemplate;
  /// <summary>Шаблон части экспортной спецификации без заголовка</summary>
  internal TableData chapterWithoutHeaderExpTemplate;
  /// <summary>Шаблон  части спецификации без заголовка в переменных данных формы Б и В в документе</summary>
  internal TableData chapterWithoutHeaderFormBTemplate;
  /// <summary>Шаблон записи в документе (для формы В - шаблон записи в общих данных)</summary>
  internal TableData avsRowTemplate;
  /// <summary>Шаблон записи формы Б в документе (для формы В - шаблон записи в переменных данных)</summary>
  internal TableData avsRowFormBTemplate;
  /// <summary>Шаблон записи в экспортной спецификации</summary>
  internal TableData avsRowExpTemplate;
  /// <summary>Шаблон записи листа регистрации изменений</summary>
  internal TableData lriRowTemplate;
  /// <summary>Шаблон таблицы листа регистрации изменений</summary>
  internal TableData lriTableTemplate;
  /// <summary>Идентификатор шаблона таблицы листа регистрации изменений</summary>
  internal const string LriTableTemplateId = "Таблица изменений";
  private const string LriRowTemplateId = "Запись ЛРИ";
  private const string LriTableHeaderTemplateId = "Шапка листа регистрации изменений";
  protected bool _processingUpdateDraftForParts;
  /// <summary>Шаблон таблицы спецификации с переменными данными формы Б и В, для блока исполнений более 10</summary>
  internal TableData avsDocTableFormBMore10_Template;
  /// <summary>Шаблон таблицы спецификации с переменными данными для формы В</summary>
  internal TableData avsDocTableFormBForV_Template;
  /// <summary>Шаблон титульной страницы спецификации с переменными данными для формы В</summary>
  internal PageData titlePageFormBForV_Template;
  /// <summary>Шаблон заголовка исполнений для графы количество в форме Б</summary>
  internal TableData productNumbersTemplate;
  internal string productNumbersTitle = "Кол. на исполн.";
  /// <summary>Шаблон заголовка исполнений для графы количество в форме Б</summary>
  internal TableData productNumbers2Template;
  /// <summary>Шаблон заголовка исполнений для графы количество в форме Б</summary>
  internal TableData productNumbers3Template;
  /// <summary>Шаблон таблицы для Кода и Литеры исполнений в форме Б</summary>
  internal TableData productKodAndLiteraTemplate;
  /// <summary>Шаблон таблицы для Кода и Литеры исполнений в форме Б</summary>
  internal TableData productKodAndLitera2Template;
  /// <summary>Шаблон таблицы с кодами ОКП для исполнений</summary>
  internal TableData productKodOKPTemplate;
  /// <summary>Шаблон таблицы с кодами ОКП для исполнений</summary>
  internal TableData productKodOKP2Template;
  /// <summary>Шаблон поля с кодом ОКП для левого исполнения</summary>
  internal TextData leftProductKodOKPTemplate;
  /// <summary>Шаблон поля с кодом ОКП для правого исполнения</summary>
  internal TextData rightProductKodOKPTemplate;
  /// <summary>Шаблон поля с обозначением левого исполнения</summary>
  internal TextData leftProductDesignationTemplate;
  /// <summary>Шаблон поля с обозначением правого исполнения</summary>
  internal TextData rightProductDesignationTemplate;
  /// <summary>Поле с кодом ОКП для левого исполнения</summary>
  internal TextData leftProductKodOKP;
  /// <summary>Поле с кодом ОКП для правого исполнения</summary>
  internal TextData rightProductKodOKP;
  /// <summary>Поле с обозначением левого исполнения</summary>
  internal TextData leftProductDesignation;
  /// <summary>Поле с обозначением правого исполнения</summary>
  internal TextData rightProductDesignation;
  /// <summary>Размещение исполнений в СП</summary>
  internal TextData productPageLinksTemplate;
  /// <summary>Размещение исполнений в СП. Второй шаблон</summary>
  internal TextData productPage2LinksTemplate;
  /// <summary>Размещение исполнений в СП формы В</summary>
  internal TextData productPageLinksFormVTemplate;
  /// <summary>Подраздел содержащий размещение исполнений в СП формы В</summary>
  internal TableData productPageLinksFormVTemplate_Table;
  /// <summary>Шаблоны записей примечания</summary>
  internal List<TableData> NotesTemplates = new List<TableData>();
  /// <summary>Шаблон для раздела переменных данных в групповом документе формы А</summary>
  internal TableData variableDataChapterTemplate;
  /// <summary>Шаблон для раздела переменных данных в групповом документе формы А экспортной СП</summary>
  internal TableData variableDataChapterExpTemplate;
  /// <summary>Шаблон для переменных данных исполнения в групповом документе формы А</summary>
  internal TableData productVariableDataChapterTemplate;
  /// <summary>Шаблон для переменных данных исполнения в групповом документе формы А экспортной СП</summary>
  internal TableData productVariableDataChapterExpTemplate;
  /// <summary>Шаблон записи примечания 1</summary>
  internal TableData note1Template;
  /// <summary>Шаблон записи примечания 2</summary>
  internal TableData note2Template;
  /// <summary>Шаблон записи примечания-заголовка 1</summary>
  internal TableData additionalNote1Template;
  /// <summary>Шаблон записи примечания-заголовка 2</summary>
  internal TableData additionalNote2Template;
  /// <summary>Шаблон заголовка функциональной группы</summary>
  internal TableData functionalGroupHeaderTemplate;
  /// <summary>Кэш настроек пропусков строк</summary>
  internal SkipLinesSchema skipLinesSchema;
  internal DynamicGroupHeaderSettings dynamicGroupHeaderSettings;
  internal static KeywordReplacementScheme keywordReplacementSettings = (KeywordReplacementScheme) null;
  internal AVSCommonPropertiesSchema avsCommonPropertiesSchema;
  private SortSchema sortSchema;
  internal SpecifNumberingFull _SpecifNumberingFull;
  internal NoteFieldSettings noteFieldSettings;
  internal VersionAttributesHelper versionAttributesHelper;
  internal OutputAttributeMappingScheme cellTextOutputAttributeMappingSettings;
  internal int productType = -1;
  internal long productId = -1;
  internal List<ProductInfo> productsInfo;
  private List<ProductInfo> parentProducts = new List<ProductInfo>();
  internal string Litera;
  private AVSDocumentForm avsDocumentForm;
  /// <summary>Части снаружи исполнений и общих данных, повторяют структуру СП</summary>
  internal bool additionalChaptersInDataChapter = true;
  internal Guid articleGroupID = Guid.Empty;
  internal long documentId = -1;
  internal Guid documentGuid = Guid.Empty;
  internal int documentType = -1;
  internal string documentName;
  internal string documentCaption;
  internal string documentDesignation;
  internal string baseProductDesignation;
  internal string documentDesignationSuffix;
  /// <summary>загружены ли данные из базы</summary>
  public bool DataLoaded;
  /// <summary>Для внутреннего использование. Флаг означает, что идёт процесс подгрузки новых атрибутов</summary>
  internal bool newAttributesLoading;
  private readonly Dictionary<int, string> _docTypeToDocTypeName = new Dictionary<int, string>();
  /// <summary>Кэш настроек сокращения обозначений исполнений</summary>
  internal DesignationTrimSchema designationTrimSchema;
  /// <summary>Кэш ключевых слов для отображения материалов в виде дроби</summary>
  private KeyWordsSchema materialKeyWordsSchema;
  /// <summary>Открывать ли спецификацию</summary>
  public bool DontOpenDocument;
  private readonly string filtrationOwnerID;
  private bool readOnly;
  private bool documentIsModifiedByLoad;
  private ImRtfEditor specificationEditor;
  private static string[] _sizeTypeEncoding = new string[3]
  {
    "L",
    "S",
    "V"
  };
  internal List<ProductInfo> productsByRelations;

  private void CreateAvsRowFromDbRecord(
    LoadDataParams loadParams,
    ref int currentRowIndex,
    RelationAttributeValuesCache relValuesCache,
    AttributeValuesCache objValuesCache)
  {
    AVSRow avsRow1 = (AVSRow) null;
    List<AVSRow> collection = new List<AVSRow>();
    int objectType = objValuesCache.ObjectType;
    long objectId = objValuesCache.ObjectId;
    Guid objectGuid = objValuesCache.ObjectGuid;
    long sortIndex = long.MinValue;
    int relType = -1;
    long num1 = -1;
    if (loadParams.LoadRelations)
    {
      num1 = relValuesCache.RelationId;
      relType = relValuesCache.GetValueInt32(-23, !loadParams.Context.IsSpecRowUpdate);
      sortIndex = relValuesCache.GetValueInt64(this.Attr_SortIndex, !loadParams.Context.IsSpecRowUpdate, long.MinValue);
      if (sortIndex == 0L)
        sortIndex = long.MinValue;
      if (this.IsSpecification && sortIndex != long.MinValue && loadParams.SpecRowsBySortIndex.TryGetValue(sortIndex, out avsRow1))
      {
        if (!avsRow1.IsAllowableRelation(relValuesCache))
          relValuesCache.SortIndex = sortIndex = long.MinValue;
        avsRow1 = (AVSRow) null;
      }
    }
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Specification) || loadParams.LoadRelations && !this.AllowIncludeRelationInDocument(relValuesCache))
      return;
    SpecificationSection specificationSection = (SpecificationSection) null;
    long num2 = -1;
    long num3 = -1;
    string razdelSP = (string) null;
    SpecificationSectionInfo specificationSectionInfo = (SpecificationSectionInfo) null;
    if (loadParams.LoadRelations && this.IsSpecification)
    {
      num2 = relValuesCache.GetValueInt64(this.Attr_Section, !loadParams.Context.IsSpecRowUpdate);
      num3 = relValuesCache.GetValueInt64(this.Attr_AdditionalChapter, false);
      if (num2 == -1L)
      {
        if (relType == AvsIDCache.Relation_Document && loadParams.SkipUnknownDoc && (SpecificationSectionInfo.IsAllowableTypeInSection(objectType, AVSDocument.ObjID_SectionComplects) ? 1 : (SpecificationSectionInfo.IsAllowableTypeInSection(objectType, AVSDocument.ObjID_SectionDocumentation) ? 1 : 0)) == 0)
          return;
        razdelSP = objValuesCache.GetValueString(AvsIDCache.Attr_InsertToSection, false, (string) null);
        specificationSectionInfo = AVSDocument.GetDefaultSectionForObject(objectType, razdelSP, loadParams.Context.SectionID, loadParams.Context.AllowableSections);
      }
    }
    if (loadParams.CreateNewRecords && num1 != -1L)
    {
      avsRow1 = this.GetAvsDocRow(num1);
      if (avsRow1 != null && (!avsRow1.IsAllowableRelation(relValuesCache) || avsRow1.ObjGuid != relValuesCache.ObjectGuid && avsRow1.RelGuid == relValuesCache.RelationGuid))
      {
        if (avsRow1.HasRelation)
        {
          for (int index = avsRow1.Relations.Count - 1; index >= 0; --index)
          {
            if (avsRow1.Relations[index].RelationId == num1)
            {
              avsRow1.RemoveRelationData(avsRow1.Relations, index);
              break;
            }
          }
        }
        if (avsRow1.HasHiddenRelation)
        {
          for (int index = avsRow1.HiddenRelations.Count - 1; index >= 0; --index)
          {
            if (avsRow1.HiddenRelations[index].RelationId == num1)
            {
              avsRow1.RemoveRelationData(avsRow1.HiddenRelations, index);
              break;
            }
          }
        }
        relValuesCache.SortIndex = sortIndex = long.MinValue;
        avsRow1 = (AVSRow) null;
      }
    }
    if (loadParams.CreateNewRecords && avsRow1 == null)
    {
      bool docRowIsExp;
      List<TableData> docRowsForDbRecord = this.FindDocRowsForDbRecord(loadParams, relValuesCache, num1, relType, objectGuid, objectId, objectType, out docRowIsExp, ref sortIndex, out avsRow1);
      for (int index1 = 0; index1 == 0 && docRowsForDbRecord.Count == 0 || index1 < docRowsForDbRecord.Count; ++index1)
      {
        TableData docRow = docRowsForDbRecord.Count > 0 ? docRowsForDbRecord[index1] : (TableData) null;
        if (docRow != null)
          avsRow1 = this.FindAVSRowByDocRowForDbRecord(loadParams, relValuesCache, sortIndex, docRowIsExp, ref docRow);
        else if (this.IsSpecification && sortIndex != long.MinValue)
        {
          loadParams.SpecRowsBySortIndex.TryGetValue(sortIndex, out avsRow1);
          AVSDocument.CheckAllowableRelationFromDbRecord(relValuesCache, ref avsRow1, ref sortIndex);
        }
        Chapter productChapter = (Chapter) null;
        if (docRow != null)
        {
          specificationSection = this.GetSection((DocumentTreeNode) docRow);
          if (specificationSection != null)
          {
            productChapter = specificationSection.ProductChapter;
            List<AttributeValues> attributeValuesList = new List<AttributeValues>();
            if (num2 == -1L && avsRow1 != null)
            {
              num2 = avsRow1.GetFieldInt64Value(this.Attr_Section, 0, (List<RelationAttributeValuesCache>) null, true);
              if (num2 == -1L && specificationSection.SectionID != -1L)
              {
                num2 = specificationSection.SectionID;
                if (num1 != -1L)
                {
                  relValuesCache.SetValue(this.Attr_Section, (object) num2, true);
                  if (!this.ReadOnly)
                    attributeValuesList.Add(new AttributeValues(this.Attr_Section.AttributeId, (object) num2));
                }
              }
            }
            if (num3.IsUndefinedId() && avsRow1 != null)
            {
              AdditionalChapter partChapter = specificationSection.GetRootChapter() as AdditionalChapter;
              if (partChapter != null)
              {
                AdditionalChapterSettings additionalChapterSettings = this.AVSCommonPropertiesSchema.AdditionalChapters.OfType<AdditionalChapterSettings>().FirstOrDefault<AdditionalChapterSettings>((System.Func<AdditionalChapterSettings, bool>) (p => p.ChapterGuid == partChapter.ChapterGuid || p.Caption.Equals(partChapter.Caption, StringComparison.CurrentCultureIgnoreCase)));
                num3 = additionalChapterSettings != null ? additionalChapterSettings.ChapterID : -1L;
                if (num1.IsDefinedId() && num3.IsDefinedId())
                {
                  relValuesCache.SetValue(this.Attr_AdditionalChapter, (object) num3, true);
                  if (!this.ReadOnly)
                    attributeValuesList.Add(new AttributeValues(this.Attr_AdditionalChapter.AttributeId, (object) num3));
                }
              }
            }
            if (!this.ReadOnly && attributeValuesList.Count > 0)
              this.PendingRelationUpdates.Add(relValuesCache.ProjectId, new RelationAttributeValues(relValuesCache.RelationId, relValuesCache.ObjectId, attributeValuesList.ToArray()));
          }
        }
        if (num2 == -1L && this.IsSpecification)
        {
          if (specificationSectionInfo != null)
          {
            if (num1 != -1L)
            {
              if (specificationSectionInfo.SectionID != -1L)
              {
                num2 = specificationSectionInfo.SectionID;
                relValuesCache.SetValue(this.Attr_Section, (object) num2, true);
                if (!this.ReadOnly)
                  this.PendingRelationUpdates.Add(relValuesCache.ProjectId, new RelationAttributeValues(relValuesCache.RelationId, relValuesCache.ObjectId, new AttributeValues[1]
                  {
                    new AttributeValues(this.Attr_Section.AttributeId, (object) num2)
                  }));
              }
            }
            else
              num2 = specificationSectionInfo.SectionID;
          }
          else if (num1 != -1L)
            return;
        }
        if (productChapter == null)
          productChapter = this.GetProductChapterFromContext(loadParams.Context);
        if ((productChapter == null || productChapter.IsCommonDataChapter) && (!loadParams.LoadRelations || loadParams.ProductIndex == 0 || loadParams.ProductIndex == -1))
        {
          if (avsRow1 == null)
          {
            collection = this.FindAvsRowsByPartId(objectId, (Chapter) null, (ProductInfo) null, num2, loadParams.Context.AdditionalChapterGuid);
            for (int index2 = 0; index2 < collection.Count; ++index2)
            {
              if (loadParams.LoadRelations && (relValuesCache.IsFreeSortIndex || collection[index2].SortIndex == relValuesCache.SortIndex) && collection[index2].IsAllowableRelation(relValuesCache, notHiddenOnly: !this.IsSpecification))
              {
                avsRow1 = collection[index2];
                collection = (List<AVSRow>) null;
                break;
              }
            }
          }
          bool flag1;
          if (avsRow1 == null)
          {
            avsRow1 = new AVSRow(this, relValuesCache, objValuesCache);
            if (!loadParams.LoadRelations && loadParams.Context.DefaultRelationType != -1)
              avsRow1.RelType = loadParams.Context.DefaultRelationType;
            flag1 = true;
          }
          else
          {
            bool addToHidden = relValuesCache != null && avsRow1.CheckRelation_IsHiddenRelation(relValuesCache);
            avsRow1.AddRowData(relValuesCache, objValuesCache, addToHidden);
            flag1 = false;
          }
          if (this.AutoSort && specificationSection != null && specificationSection.SectionID != num2 && num2 != -1L)
            specificationSection = (SpecificationSection) null;
          if (specificationSection == null)
            specificationSection = avsRow1.Section;
          if (specificationSection == null)
          {
            if (num2 == -1L)
              num2 = AVSDocument.GetDefaultSectionIdForObject(objectType, razdelSP, loadParams.Context.SectionID, loadParams.Context.AllowableSections);
            specificationSection = num2 == -1L || loadParams.Context.SectionID != num2 ? this.FindSectionForNewRowInCommonData(loadParams, num2, relValuesCache) : loadParams.Context.Section;
          }
          if (flag1)
          {
            if (loadParams.Context.IsDocumentLoading || this.AutoSort && specificationSection.HasSortingSettings)
            {
              bool flag2 = avsRow1.IsFreeSortIndex && loadParams.Context.IsDocumentLoading && !loadParams.Context.IsNewDocument;
              specificationSection.AddRow(avsRow1, !flag2);
            }
            else
            {
              if (currentRowIndex == -1 || currentRowIndex >= specificationSection.Rows.Count)
                currentRowIndex = specificationSection.Rows.Count;
              specificationSection.InsertRow(currentRowIndex++, avsRow1);
            }
          }
        }
        else
        {
          if (loadParams.LoadRelations && this.AvsDocumentForm == AVSDocumentForm.A && avsRow1 != null && avsRow1.DocNode == null && !avsRow1.Product.IsCommonData && avsRow1.ProductID != loadParams.Context.Product.Id)
          {
            avsRow1 = (AVSRow) null;
            relValuesCache.SortIndex = sortIndex = long.MinValue;
          }
          if (avsRow1 == null && loadParams.LoadRelations)
          {
            if (productChapter == null || productChapter.IsCommonDataChapter)
            {
              List<AVSRow> objectForDbRecord = this.FindRowsByObjectForDbRecord(loadParams, objectId, objectGuid, num2, productChapter, relValuesCache.GetValueInt64(this.Attr_AdditionalChapter, false));
              foreach (AVSRow avsRow2 in objectForDbRecord.Where<AVSRow>((System.Func<AVSRow, bool>) (r => r.HasRelation && r.Relations.Count <= loadParams.ProductIndex)))
              {
                if (this.CheckAllowableRowForAddRelationInLoadProcess(avsRow2, relValuesCache, sortIndex, true))
                {
                  avsRow1 = avsRow2;
                  break;
                }
              }
              if (avsRow1 == null)
              {
                foreach (AVSRow avsRow3 in objectForDbRecord.Where<AVSRow>((System.Func<AVSRow, bool>) (r => r.HasRelation && r.Relations.Count == loadParams.ProductIndex + 1)))
                {
                  if (this.CheckAllowableRowForAddRelationInLoadProcess(avsRow3, relValuesCache, sortIndex, false))
                  {
                    avsRow1 = avsRow3;
                    break;
                  }
                }
              }
            }
            if (avsRow1 != null && avsRow1.RelId != num1 && avsRow1.RelId != -1L && avsRow1.IsFormB && sortIndex != avsRow1.SortIndex && avsRow1.GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false) != relValuesCache.GetValueString(AvsIDCache.Attr_Position, false))
              avsRow1 = (AVSRow) null;
          }
          if (avsRow1 != null)
          {
            bool addToHidden = avsRow1.CheckRelation_IsHiddenRelation(relValuesCache);
            avsRow1.AddRowData(relValuesCache, objValuesCache, addToHidden);
            this.RegisterAVSRowObjectInDictionaries(avsRow1);
          }
          else
          {
            avsRow1 = new AVSRow(this, relValuesCache, objValuesCache);
            if (!loadParams.LoadRelations && loadParams.Context.DefaultRelationType != -1)
              avsRow1.RelType = loadParams.Context.DefaultRelationType;
            if (docRow != null)
            {
              avsRow1.AddDocNode(docRow, docRowIsExp);
              avsRow1.LoadDataFromDocRow(docRow, false, false, false);
            }
            if (specificationSection == null && loadParams.Context.SectionID == num2)
              specificationSection = loadParams.Context.Section;
            if (specificationSection != null && this.AvsDocumentForm == AVSDocumentForm.A && specificationSection.Product.Id != loadParams.Context.Product.Id)
              specificationSection = (SpecificationSection) null;
            if (specificationSection == null)
              specificationSection = this.FindSectionForNewRowInNextProduct(loadParams, num2, relValuesCache);
            if (loadParams.Context.IsDocumentLoading || this.AutoSort && specificationSection.HasSortingSettings)
            {
              bool flag = avsRow1.IsFreeSortIndex && loadParams.Context.IsDocumentLoading && !loadParams.Context.IsNewDocument;
              specificationSection.AddRow(avsRow1, !flag);
            }
            else if (currentRowIndex == -1 || currentRowIndex >= specificationSection.Rows.Count)
              currentRowIndex = specificationSection.AddRow(avsRow1, false);
            else
              specificationSection.InsertRow(currentRowIndex++, avsRow1);
            avsRow1.CheckAdditionalChapter();
          }
        }
      }
    }
    else
    {
      avsRow1 = this.FindAndUpdateAvsRowWithDbTableRow(loadParams, relValuesCache, objValuesCache, objectId, num1, num2);
      if (avsRow1 != null && relValuesCache != null && relValuesCache.GetValueInt64(this.Attr_AdditionalChapter, false).IsUndefinedId())
        avsRow1.CheckAdditionalChapter();
    }
    if (avsRow1 != null)
      AVSDocument.CheckUniqueSortIndex(loadParams, sortIndex, avsRow1, num1);
    if (num1 != -1L && loadParams.LoadedRelations != null && avsRow1 != null)
      loadParams.LoadedRelations.Add(num1, avsRow1);
    if (loadParams.LoadedSpecRows != null && collection.Count > 0)
      loadParams.LoadedSpecRows.AddRange((IEnumerable<AVSRow>) collection);
    if (loadParams.LoadedSpecRows == null || avsRow1 == null)
      return;
    loadParams.LoadedSpecRows.Add(avsRow1);
  }

  private bool CheckAllowableRowForAddRelationInLoadProcess(
    AVSRow avsRow,
    RelationAttributeValuesCache relValuesCache,
    long sortIndex,
    bool notHiddenOnly)
  {
    if (!avsRow.IsAllowableRelation(relValuesCache, notHiddenOnly: notHiddenOnly) || !this.IsSpecification && relValuesCache.GetValueString(this.Field_PosDesignation, false) != avsRow.GetFieldStringValue(this.Field_PosDesignation, 0, 0, (List<RelationAttributeValuesCache>) null, false))
      return false;
    if (avsRow.InCommonData_AV)
    {
      bool flag = avsRow.CheckRelation_IsHiddenRelation(relValuesCache);
      List<RelationAttributeValuesCache> enumerable = flag ? avsRow.HiddenRelations : avsRow.Relations;
      if (enumerable != null && !enumerable.Contains<RelationAttributeValuesCache>((Predicate<RelationAttributeValuesCache>) (r => this.AvsRowsIsEqual(relValuesCache, r, false, true))) && (flag || !avsRow.HasHiddenRelation || !avsRow.HiddenRelations.Contains<RelationAttributeValuesCache>((Predicate<RelationAttributeValuesCache>) (r => this.AvsRowsIsEqual(relValuesCache, r, false, true)))))
        return false;
    }
    else if (avsRow.IsFormB && sortIndex != avsRow.SortIndex)
    {
      if (sortIndex != long.MinValue || !avsRow.IsFreeSortIndex)
      {
        avsRow.NeedUpdateStructure = true;
        return false;
      }
    }
    else if (avsRow.InVariableData_AV && this.AvsDocumentForm == AVSDocumentForm.A && !avsRow.CheckRelation_IsHiddenRelation(relValuesCache))
      return false;
    return true;
  }

  private Chapter GetProductChapterFromContext(AVSDocumentContext context)
  {
    Chapter chapterFromContext = (Chapter) null;
    if (context.Section != null)
      chapterFromContext = context.Section.ProductChapter;
    if (chapterFromContext == null && this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
    {
      chapterFromContext = (Chapter) (context.Chapter as ProductVariableDataChapter);
      if (chapterFromContext == null && context.Chapter != null)
      {
        if (context.Chapter.Parent != null)
          chapterFromContext = (Chapter) (context.Chapter.Parent as ProductVariableDataChapter);
        if (chapterFromContext == null && context.AdditionalChapter != null)
        {
          VariableDataChapterFormA variableDataFormA = context.AdditionalChapter.InnerVariableData_FormA;
          if (variableDataFormA != null)
            chapterFromContext = (Chapter) (variableDataFormA.GetChapter(context.Chapter.Product.Id) as ProductVariableDataChapter);
        }
      }
    }
    return chapterFromContext;
  }

  private List<AVSRow> FindRowsByObjectForDbRecord(
    LoadDataParams loadParams,
    long objId,
    Guid objGuid,
    long specSectionId,
    Chapter productChapter,
    long additionalChapterID)
  {
    List<AVSRow> objectForDbRecord = new List<AVSRow>();
    Guid? nullable = new Guid?();
    if (!additionalChapterID.IsUndefinedId())
      nullable = new Guid?(this.AVSCommonPropertiesSchema.GetAdditionalChapterGuid(additionalChapterID));
    if (nullable.IsEmpty())
      nullable = loadParams.Context.AdditionalChapterGuid;
    if (loadParams.Context.Chapter != this.variableDataChapter_FormV || this.variableDataChapter_FormV == null)
    {
      if (productChapter != null)
      {
        objectForDbRecord = this.FindAvsRowsByPartId(objId, (Chapter) null, productChapter.Product, specSectionId, nullable);
      }
      else
      {
        objectForDbRecord = this.FindAvsRowsByPartId(objId, (Chapter) null, this.commonDataChapter.Product, specSectionId, nullable);
        if (this.variableDataChapter_FormA != null)
          objectForDbRecord.AddRange((IEnumerable<AVSRow>) this.FindAvsRowsByPartId(objId, (Chapter) null, loadParams.Product, specSectionId, nullable));
        if (this.variableDataChapter_FormV != null)
          objectForDbRecord.AddRange((IEnumerable<AVSRow>) this.FindAvsRowsByPartId(objId, (Chapter) null, this.variableDataChapter_FormV.Product, specSectionId, nullable));
      }
      if (objectForDbRecord.Count == 0)
      {
        if (productChapter != null)
        {
          objectForDbRecord = this.FindSpecRowsByPartGuid(objGuid, (Chapter) null, productChapter.Product, specSectionId, nullable);
        }
        else
        {
          objectForDbRecord = this.FindSpecRowsByPartGuid(objGuid, (Chapter) null, this.commonDataChapter.Product, specSectionId, nullable);
          if (this.variableDataChapter_FormV != null)
            objectForDbRecord.AddRange((IEnumerable<AVSRow>) this.FindSpecRowsByPartGuid(objGuid, (Chapter) null, this.variableDataChapter_FormV.Product, specSectionId, nullable));
        }
      }
    }
    if (objectForDbRecord.Count == 0 && this.variableDataChapter_FormV != null)
    {
      objectForDbRecord = this.FindAvsRowsByPartId(objId, (Chapter) null, this.variableDataChapter_FormV.Product, specSectionId, nullable);
      if (objectForDbRecord.Count == 0)
        objectForDbRecord = this.FindSpecRowsByPartGuid(objGuid, (Chapter) null, this.variableDataChapter_FormV.Product, specSectionId, nullable);
    }
    return objectForDbRecord;
  }

  /// <summary>Проверить исполнение записи и загружаемой связи</summary>
  private static bool CheckAllowableRelationFromDbRecord(
    RelationAttributeValuesCache relValuesCache,
    ref AVSRow avsRow,
    ref long sortIndex)
  {
    if (avsRow == null || relValuesCache == null || avsRow.IsAllowableRelation(relValuesCache))
      return true;
    avsRow = (AVSRow) null;
    relValuesCache.SortIndex = sortIndex = long.MinValue;
    return false;
  }

  private AVSRow FindAVSRowByDocRowForDbRecord(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relValuesCache,
    long sortIndex,
    bool docRowIsExp,
    ref TableData docRow)
  {
    AVSRow row = this.GetAvsDocRow((DocumentTreeNode) docRow);
    if (row != null && relValuesCache != null && !row.IsAllowableRelation(relValuesCache, false) && (row.HasAnyRelations || relValuesCache.IsFreeSortIndex || relValuesCache.SortIndex != row.SortIndex))
    {
      this.UnregisterAVSRowRelationInDictionaries(row, relValuesCache);
      docRow = (TableData) null;
      row = (AVSRow) null;
    }
    if (sortIndex != long.MinValue && docRow != null)
    {
      List<TableData> tableDataList1 = (List<TableData>) null;
      if (docRowIsExp)
      {
        if (loadParams.ExpDocRowsBySortIndex != null)
        {
          if (!loadParams.ExpDocRowsBySortIndex.TryGetValue(sortIndex, out tableDataList1))
          {
            List<TableData> tableDataList2;
            loadParams.ExpDocRowsBySortIndex.Add(sortIndex, tableDataList2 = new List<TableData>());
            tableDataList2.Add(docRow);
          }
          else if (!tableDataList1.Contains(docRow))
            tableDataList1.Add(docRow);
        }
      }
      else if (loadParams.DocRowsBySortIndex != null)
      {
        if (!loadParams.DocRowsBySortIndex.TryGetValue(sortIndex, out tableDataList1))
        {
          List<TableData> tableDataList3;
          loadParams.DocRowsBySortIndex.Add(sortIndex, tableDataList3 = new List<TableData>());
          tableDataList3.Add(docRow);
        }
        else if (!tableDataList1.Contains(docRow))
          tableDataList1.Add(docRow);
      }
    }
    return row;
  }

  /// <summary>Контроль уникальности индекса сортировки для разных записей</summary>
  private static void CheckUniqueSortIndex(
    LoadDataParams loadParams,
    long sortIndex,
    AVSRow avsRow,
    long relId)
  {
    if (avsRow == null)
      throw new ArgumentNullException(nameof (avsRow));
    if (loadParams == null)
      throw new ArgumentNullException(nameof (loadParams));
    if (!avsRow.avsDocument.IsSpecification || sortIndex == long.MinValue)
      return;
    AVSRow avsRow1;
    if (loadParams.SpecRowsBySortIndex.TryGetValue(sortIndex, out avsRow1))
    {
      if (avsRow1 == avsRow)
        return;
      avsRow.SortIndex = long.MinValue;
    }
    else
      loadParams.SpecRowsBySortIndex.Add(sortIndex, avsRow);
  }

  /// <summary>Поиск строк документа для загруженной из БД записи о связи или объекте</summary>
  private List<TableData> FindDocRowsForDbRecord(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relValuesCache,
    long relId,
    int relType,
    Guid objGuid,
    long objId,
    int objType,
    out bool docRowIsExp,
    ref long sortIndex,
    out AVSRow avsRow)
  {
    List<TableData> docRowsForDbRecord = new List<TableData>();
    avsRow = (AVSRow) null;
    TableData rowDocNode = (TableData) null;
    docRowIsExp = false;
    if (loadParams.LoadRelations)
    {
      List<TableData> tableDataList;
      if ((loadParams.DocRowsByGuid == null || !loadParams.DocRowsByGuid.TryGetValue(relValuesCache.RelationGuid, out rowDocNode)) && (loadParams.ExpDocRowsByGuid == null || !(docRowIsExp = loadParams.ExpDocRowsByGuid.TryGetValue(relValuesCache.RelationGuid, out rowDocNode))) && this.IsSpecification && sortIndex != long.MinValue && (loadParams.DocRowsBySortIndex != null && loadParams.DocRowsBySortIndex.TryGetValue(sortIndex, out tableDataList) || loadParams.ExpDocRowsBySortIndex != null && (docRowIsExp = loadParams.ExpDocRowsBySortIndex.TryGetValue(sortIndex, out tableDataList))) && tableDataList != null && tableDataList.Count > 0)
      {
        if (tableDataList[0].Tag is AVSRow tag)
        {
          if (tag.HasRelation)
          {
            if (!tag.IsAllowableRelation(relValuesCache))
              tableDataList.Clear();
          }
          else if (tag.RelGuid != Guid.Empty)
          {
            if (tag.RelGuid != relValuesCache.RelationGuid)
            {
              this.UnregisterSpecRowInDictionaries(tag);
              tag.RowID.SetDBRelationInfo(relValuesCache.RelationGuid, relId, relType, loadParams.Context.Product.Guid, loadParams.Context.Product.Id, objGuid, objId, objType, tag.RowID.ObjectCaption);
              this.RegisterAVSRowInDictionaries(tag);
            }
            else
              tableDataList.Clear();
          }
        }
        if (tableDataList.Count > 0)
        {
          rowDocNode = tableDataList[0];
          avsRow = this.GetAvsDocRow((DocumentTreeNode) rowDocNode);
          if (!AVSDocument.CheckAllowableRelationFromDbRecord(relValuesCache, ref avsRow, ref sortIndex))
            rowDocNode = (TableData) null;
        }
      }
      if (rowDocNode != null)
        docRowsForDbRecord.Add(rowDocNode);
    }
    else
      docRowsForDbRecord = AVSDocument.FindDocRowsInDictionaryByObject(loadParams, objId, objGuid, out docRowIsExp);
    return docRowsForDbRecord;
  }

  private static void DecodeApplicabilityCondition(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relValuesCache)
  {
    string valueString = relValuesCache.GetValueString(Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID, false);
    if (!(valueString != ""))
      return;
    if (!AVSDocument.PdmConfiguratorCacheLoaded)
    {
      PdmConfiguratorCache.CacheLoadOptions(loadParams.Session);
      AVSDocument.PdmConfiguratorCacheLoaded = true;
    }
    ObjectsApplicabilitiesCriterionsCollection criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection();
    if (valueString.Length == Intermech.Consts.MaxStringSize)
    {
      IDBRelation relationByPartObjectId = loadParams.Session.GetRelationByPartObjectID(relValuesCache.RelationId, relValuesCache.ObjectId, true);
      criterionsCollection.LoadFromObject((IDBAttributable) relationByPartObjectId);
    }
    else
      criterionsCollection.Assign((object) valueString);
    relValuesCache.SetValue(Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID, (object) criterionsCollection.GenerateStringComments(true, true), false);
  }

  internal virtual SpecificationSection FindSectionForNewRowInCommonData(
    LoadDataParams loadParams,
    long sectionId,
    RelationAttributeValuesCache relation)
  {
    if (!(this.commonDataChapter is SpecificationSection newRowInCommonData))
      newRowInCommonData = this.FindOrCreateSection(this.commonDataChapter, sectionId);
    return newRowInCommonData;
  }

  internal virtual SpecificationSection FindSectionForNewRowInNextProduct(
    LoadDataParams loadParams,
    long sectionId,
    RelationAttributeValuesCache relation)
  {
    SpecificationSection rowInNextProduct1 = loadParams.Context.Section;
    if (rowInNextProduct1 == null)
    {
      Chapter defaultProductChapter = this.GetNewRowDefaultProductChapter(loadParams);
      if (defaultProductChapter is SpecificationSection rowInNextProduct2)
        return rowInNextProduct2;
      rowInNextProduct1 = defaultProductChapter?.GetChapter(sectionId) as SpecificationSection;
    }
    return rowInNextProduct1;
  }

  internal Chapter GetNewRowDefaultProductChapter(LoadDataParams loadParams)
  {
    return this.AvsDocumentForm != AVSDocumentForm.V ? (this.AvsDocumentForm != AVSDocumentForm.A ? this.commonDataChapter : this.variableDataChapter_FormA.GetChapter(loadParams.Context.Product.Id)) : (Chapter) this.variableDataChapter_FormV;
  }

  private AVSRow FindAndUpdateAvsRowWithDbTableRow(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relValuesCache,
    AttributeValuesCache objValuesCache,
    long objId,
    long relId,
    long sectionId)
  {
    List<TableData> list = (List<TableData>) null;
    AVSRow rowWithDbTableRow = loadParams.Context?.Row;
    if (!loadParams.LoadRelations)
    {
      if (loadParams.DocRowsByObjectID != null)
        loadParams.DocRowsByObjectID.TryGetValue(objId, out list);
      if (list.IsEmpty<TableData>() && loadParams.ExpDocRowsByObjectID != null)
        loadParams.ExpDocRowsByObjectID.TryGetValue(objId, out list);
    }
    for (int index = 0; index == 0 && list == null || list != null && index < list.Count; ++index)
    {
      TableData rowDocNode = list?[index];
      if (rowDocNode != null)
        rowWithDbTableRow = rowWithDbTableRow ?? this.GetAvsDocRow((DocumentTreeNode) rowDocNode);
      bool updateDocNode = !loadParams.Context.SuspendUpdateDocRows;
      if (loadParams.LoadRelations)
        rowWithDbTableRow = rowWithDbTableRow ?? this.GetAvsDocRow(relId);
      else if (rowWithDbTableRow == null)
      {
        foreach (AVSRow avsRow in this.GetAvsRowsByObjectId(objId))
          avsRow.UpdateAttributes(loadParams.SelectParamSet.ColumnsInfo, (RelationAttributeValuesCache) null, objValuesCache, updateDocNode, updateDocNode && this.IsGridViewMode, false);
      }
      if (rowWithDbTableRow != null)
        rowWithDbTableRow.UpdateAttributes(loadParams.SelectParamSet.ColumnsInfo, relValuesCache, objValuesCache, updateDocNode, updateDocNode && this.IsGridViewMode, false);
      else if (!this.IsSpecification)
      {
        foreach (AVSRow avsRow in this.FindAvsRowsByPartId(objId, (Chapter) null, (ProductInfo) null, sectionId, loadParams.Context.AdditionalChapterGuid))
          avsRow.UpdateAttributes(loadParams.SelectParamSet.ColumnsInfo, relValuesCache, objValuesCache, updateDocNode, updateDocNode && this.IsGridViewMode, false);
      }
    }
    return rowWithDbTableRow;
  }

  protected SpecificationSection FindOrCreateSection(Chapter sectionOwner, long sectionId)
  {
    SpecificationSection orCreateSection = sectionOwner != null ? (SpecificationSection) sectionOwner.GetChapter(sectionId) : throw new ArgumentNullException(nameof (sectionOwner));
    if (orCreateSection == null)
    {
      orCreateSection = this.CreateSection(sectionId);
      sectionOwner.AddChapter((Chapter) orCreateSection, true, false, false, (TableData) null);
    }
    return orCreateSection;
  }

  /// <summary> Идентификатор раздела спецификации "Документация" </summary>
  public static long ObjID_SectionDocumentation
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionDocumentation != -1L)
        return AVSDocument.objID_SectionDocumentation;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00256-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionDocumentation = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionDocumentation;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Комплексы" </summary>
  public static long ObjID_SectionComplex
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionComplex != -1L)
        return AVSDocument.objID_SectionComplex;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00257-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionComplex = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionComplex;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Сборочные единицы" </summary>
  public static long ObjID_SectionAssemblyUnits
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionAssemblyUnits != -1L)
        return AVSDocument.objID_SectionAssemblyUnits;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00258-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionAssemblyUnits = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionAssemblyUnits;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Детали" </summary>
  public static long ObjID_SectionDetail
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionDetail != -1L)
        return AVSDocument.objID_SectionDetail;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00259-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionDetail = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionDetail;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Стандартные изделия" </summary>
  public static long ObjID_SectionStandartArticles
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionStandartArticles != -1L)
        return AVSDocument.objID_SectionStandartArticles;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0025a-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionStandartArticles = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionStandartArticles;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Прочие изделия" </summary>
  public static long ObjID_SectionOtherArticles
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionOtherArticles != -1L)
        return AVSDocument.objID_SectionOtherArticles;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0025b-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionOtherArticles = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionOtherArticles;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Материалы" </summary>
  public static long ObjID_SectionMaterials
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionMaterials != -1L)
        return AVSDocument.objID_SectionMaterials;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0025c-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionMaterials = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionMaterials;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Комплекты" </summary>
  public static long ObjID_SectionComplects
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionComplects != -1L)
        return AVSDocument.objID_SectionComplects;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0025d-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionComplects = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionComplects;
    }
  }

  /// <summary> Идентификатор раздела спецификации "Комплектовочные единицы" </summary>
  public static long ObjID_SectionComplectUnits
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_SectionComplectUnits != -1L)
        return AVSDocument.objID_SectionComplectUnits;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad00271-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_SectionComplectUnits = dbObject.ObjectID;
      }
      return AVSDocument.objID_SectionComplectUnits;
    }
  }

  /// <summary> Идентификатор объекта "Общий шаблон спецификаций" </summary>
  public static long ObjID_CommonSpecificationTemplate
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_CommonSpecificationTemplate_ != -1L)
        return AvsIDCache.ObjID_CommonSpecificationTemplate_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetCommonSpecificationTemplateId(sessionKeeper.Session);
    }
  }

  /// <summary> Идентификатор объекта "Шаблон единичных спецификаций" </summary>
  public static long ObjID_StdTemplateSingleSpecification
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_StdTemplateSingleSpecification_ != -1L)
        return AvsIDCache.ObjID_StdTemplateSingleSpecification_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetStdTemplateSingleSpecificationId(sessionKeeper.Session);
    }
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы Б" </summary>
  public static long ObjID_StdTemplateSpecificationFormB
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_StdTemplateSpecificationFormB_ != -1L)
        return AvsIDCache.ObjID_StdTemplateSpecificationFormB_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetStdTemplateSpecificationFormBId(sessionKeeper.Session);
    }
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы В" </summary>
  public static long ObjID_StdTemplateSpecificationFormV
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_StdTemplateSpecificationFormV_ != -1L)
        return AvsIDCache.ObjID_StdTemplateSpecificationFormV_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetStdTemplateSpecificationFormVId(sessionKeeper.Session);
    }
  }

  /// <summary> Идентификатор объекта "Шаблон зеркальных СП" </summary>
  public static long ObjID_StdTemplateMirrorSpecification
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_StdTemplateMirrorSpecification_ != -1L)
        return AvsIDCache.ObjID_StdTemplateMirrorSpecification_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetStdTemplateMirrorSpecificationId(sessionKeeper.Session);
    }
  }

  /// <summary> Объект "Настройки старых спецификаций" </summary>
  public static long ObjID_OldAVSSettingsSpecifications
  {
    get
    {
      if (AVSDocument.objID_OldAVSSettingsSpecifications != -1L)
        return AVSDocument.objID_OldAVSSettingsSpecifications;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad002a2-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_OldAVSSettingsSpecifications = dbObject.ObjectID;
      }
      return AVSDocument.objID_OldAVSSettingsSpecifications;
    }
  }

  /// <summary> Объект "Настройки старых ведомостей" </summary>
  public static long ObjID_OldAVSSettingsVedomosti
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument.objID_OldAVSSettingsVedomosti != -1L)
        return AVSDocument.objID_OldAVSSettingsVedomosti;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad002a6-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          AVSDocument.objID_OldAVSSettingsVedomosti = dbObject.ObjectID;
      }
      return AVSDocument.objID_OldAVSSettingsVedomosti;
    }
  }

  /// <summary> Единица измерения СИ "Килограмм" </summary>
  public static long KilogramsID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._kilogramsID.IsDefinedId())
        return AVSDocument._kilogramsID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._kilogramsID = sessionKeeper.Session.GetObjectInfo(SystemGUIDs.objectKilogramsGuid).ObjectID;
      return AVSDocument._kilogramsID;
    }
  }

  /// <summary> Единица измерения СИ "Метр" </summary>
  public static long MeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._meterID.IsDefinedId())
        return AVSDocument._meterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._meterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad002e4-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._meterID;
    }
  }

  /// <summary> Единица измерения СИ "Квадратный метр" </summary>
  public static long SquareMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._squareMeterID.IsDefinedId())
        return AVSDocument._squareMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._squareMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad002f5-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._squareMeterID;
    }
  }

  /// <summary> Единица измерения СИ "Кубический метр" </summary>
  public static long CubicMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._cubicMeterID.IsDefinedId())
        return AVSDocument._cubicMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._cubicMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad002f0-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._cubicMeterID;
    }
  }

  /// <summary> Единица измерения СИ "Килограмм на метр" </summary>
  public static long KilogramsPerMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._kilogramsPerMeterID.IsDefinedId())
        return AVSDocument._kilogramsPerMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._kilogramsPerMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad007e4-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._kilogramsPerMeterID;
    }
  }

  /// <summary> Единица измерения СИ "Килограмм на метр квадратный" </summary>
  public static long KilogramsPerSquareMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._kilogramsPerSquareMeterID.IsDefinedId())
        return AVSDocument._kilogramsPerSquareMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._kilogramsPerSquareMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad007e6-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._kilogramsPerSquareMeterID;
    }
  }

  /// <summary> Единица измерения СИ "Килограмм на метр кубический" </summary>
  public static long KilogramsPerCubicMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._kilogramsPerCubicMeterID.IsDefinedId())
        return AVSDocument._kilogramsPerCubicMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._kilogramsPerCubicMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad00300-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._kilogramsPerCubicMeterID;
    }
  }

  /// <summary> Единица измерения "Грамм на метр" </summary>
  public static long GramsPerMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._gramsPerMeterID.IsDefinedId())
        return AVSDocument._gramsPerMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._gramsPerMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad007e3-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._gramsPerMeterID;
    }
  }

  /// <summary> Единица измерения "Грамм на метр квадратный" </summary>
  public static long GramsPerSquareMeterID
  {
    [DebuggerStepThrough] get
    {
      if (AVSDocument._gramsPerSquareMeterID.IsDefinedId())
        return AVSDocument._gramsPerSquareMeterID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        AVSDocument._gramsPerSquareMeterID = sessionKeeper.Session.GetObjectInfo(new Guid("cad007e5-306c-11d8-b4e9-00304f19f545")).ObjectID;
      return AVSDocument._gramsPerSquareMeterID;
    }
  }

  /// <summary>Атрибут отвечающий за примечание в документе</summary>
  [Browsable(false)]
  public virtual AvsRowAttributeInfo Attr_Note
  {
    get => this._attr_Note ?? (this._attr_Note = AvsIDCache.StdField_Note.Clone());
  }

  /// <summary>Найти столбец для заголовка раздела</summary>
  /// <param name="gridViewCols">Столбцы табличного вида</param>
  public int FindGridColumn_Name(List<AvsRowAttributeInfo> gridViewCols)
  {
    for (int index = 0; index < gridViewCols.Count; ++index)
    {
      if (this.Field_Name.Equals((AttributeInfo) gridViewCols[index]) || gridViewCols[index].IsDocField && gridViewCols[index].Name == AVSRow.DocAttr_Name)
        return index;
    }
    return -1;
  }

  /// <summary>Список исполнений конструкторского документа</summary>
  [Browsable(false)]
  public List<ProductInfo> ProductsInfo => this.productsInfo;

  /// <summary>Список родительских изделий из состава которых генерируется документ, но которые не являются исполнениями группового изделия
  /// Используется для ПЭ</summary>
  public List<ProductInfo> ParentProducts => this.parentProducts;

  public List<ProductInfo> SourceProducts
  {
    get
    {
      return this.parentProducts.IsNullOrEmpty<ProductInfo>() ? this.productsInfo : this.parentProducts;
    }
  }

  /// <summary>Список идентификаторов исполнений конструкторского документа</summary>
  [Browsable(false)]
  public List<long> ProductsID
  {
    get
    {
      List<long> productsId = new List<long>(this.productsInfo.Count);
      for (int index = 0; index < this.productsInfo.Count; ++index)
        productsId.Add(this.productsInfo[index].Id);
      return productsId;
    }
  }

  public AVSDocumentForm AvsDocumentForm
  {
    get => this.avsDocumentForm;
    set
    {
      if (this.avsDocumentForm == value)
        return;
      this.avsDocumentForm = value;
    }
  }

  /// <summary>Части снаружи исполнений и общих данных, повторяют структуру СП</summary>
  internal bool AdditionalChaptersInDataChapter
  {
    get
    {
      if (this.additionalChaptersInDataChapter)
        return true;
      return this.AvsDocumentForm != AVSDocumentForm.A && this.AvsDocumentForm != AVSDocumentForm.V;
    }
  }

  /// <summary>
  /// Документ находится в процессе загрузки и синхронизации
  /// </summary>
  internal bool AvsDocumentNowLoading { get; set; }

  internal bool CollectChangeEvents => AvsConfig.General.ShowEvents && this.AvsDocumentNowLoading;

  internal AvsRowEventMessageViewer AvsRowEventMessageViewer
  {
    get => this._avsRowEventMessageViewer;
    set => this._avsRowEventMessageViewer = value;
  }

  internal bool IsAttributeSaveBatchModeEnabled => this.pendingAttributeUpdateModeCounter > 0;

  internal AVSRelationAttributeValueBatch PendingRelationUpdates { get; set; } = new AVSRelationAttributeValueBatch();

  /// <summary>Конструктор</summary>
  /// <param name="avsWindow">Окно редактора</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  public AVSDocument(
    AVSWindow avsWindow,
    int objectType,
    long objectId,
    bool readOnly,
    bool? createUndo)
    : this()
  {
    this.avsWindow = avsWindow;
    if (avsWindow != null)
      avsWindow.AVSDocument = this;
    this.LoadAVSDocumentFromDB(new OpenAVSDocArgs(objectId, objectType, readOnly: readOnly, createUndo: createUndo, saveIfUpdatedForLoad: false));
  }

  /// <summary>Конструктор для генерации СП без документа в базе</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="documentForm">Форма конструкторского документа. Если форма единичная, то исполнения игнорируются</param>
  /// <param name="configureCompositionRoot">Корень конфигурации состава</param>
  /// <param name="filtrationOwnerID">Владелец настроек фильтрации</param>
  /// <param name="readOnly">Только для чтения</param>
  public AVSDocument(
    int objectType,
    long objectId,
    AVSDocumentForm documentForm,
    RelationPair configureCompositionRoot,
    string filtrationOwnerID,
    bool readOnly)
    : this()
  {
    this.ReadOnly = readOnly;
    this.isGeneratedDoc = true;
    this.filtrationOwnerID = filtrationOwnerID;
    this.AvsDocumentForm = documentForm;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (objectType == -1)
        objectType = sessionKeeper.Session.GetObjectInfo(objectId).ObjectTypeID;
      this.articleGroupID = Guid.Empty;
      if (!MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Specification))
      {
        this.avsDocumentType = AVSDocument.GetDefaultSpecificationType();
        this.avsDocTypeGuid = AVSDocumentTypeSettings.GetStdDocTypeGuid(this.avsDocumentType);
        this.CreateRowAttrsInfo();
        this.ProductId = objectId;
        this.productType = objectType;
        IDBObject productObj = sessionKeeper.Session.GetObject(this.ProductId);
        if (this.ProductId > 0L && productObj.CheckoutBy != 0L && productObj.CheckoutBy == sessionKeeper.Session.UserID)
        {
          this.ProductId = -this.ProductId;
          productObj = sessionKeeper.Session.GetObject(this.ProductId);
          if (this.productType == -1)
            this.productType = productObj.ObjectType;
        }
        if (this.AvsDocumentForm != AVSDocumentForm.Single)
        {
          this.productsInfo = this.LoadProductsByGroupID(this.productId, this.productAttributeList, (string) null, sessionKeeper.Session);
          if (this.productType == -1)
            this.productType = productObj.ObjectType;
        }
        else
          this.productsInfo = new List<ProductInfo>()
          {
            new ProductInfo(productObj, this.productAttributeList)
          };
      }
      else if (!AvsIDCache.IsElementList(objectType))
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Specification))
          throw new Exception("Объекты данного типа не поддерживаются AVS");
        this.avsDocumentType = AVSDocument.GetDefaultSpecificationType();
        this.avsDocTypeGuid = AVSDocumentTypeSettings.GetStdDocTypeGuid(this.avsDocumentType);
        this.CreateRowAttrsInfo();
      }
      this.LoadNoteFieldSettings();
      this.LoadVersionAttributesHelper();
      if (DocumentTypeWeightHelper.items == null)
        DocumentTypeWeightHelper.LoadSystemCollection(sessionKeeper.Session);
      if (this.productsInfo.Count > 1 && documentForm == AVSDocumentForm.Single)
        documentForm = this.GetDefaultGroupDocumentForm();
      this.AvsDocumentForm = documentForm;
      this.productAttributeList = new List<int>();
      if (this.versionAttributesHelper.Items != null && documentForm == AVSDocumentForm.A && this.productsInfo.Count > 0)
      {
        ProductInfo productInfo = this.productsInfo[0];
        for (int index = 0; index < this.versionAttributesHelper.Items.Count; ++index)
        {
          if (!productInfo.HasAttribute(this.versionAttributesHelper.Items[index].ID) && !this.productAttributeList.Contains(this.versionAttributesHelper.Items[index].ID))
            this.productAttributeList.Add(this.versionAttributesHelper.Items[index].ID);
        }
      }
      if (this.productsInfo.Count == 0 && !this.IsSpecification)
      {
        this.productsInfo = new List<ProductInfo>();
        ProductInfo elementListInfo = this.GetElementListInfo();
        this.productsInfo.Add(elementListInfo);
        if (this.AvsDocumentForm == AVSDocumentForm.A)
          elementListInfo.Designation = this.BaseProductDesignation;
      }
      this.SortDocumentProducts();
      this.InitEmptyImDocument(sessionKeeper.Session);
      this.GetProductAttrsInfoForDocument(this.productAttributeList);
      if (this.productAttributeList.Count > 0)
        this.UpdateProductsByGroupID(this.productAttributeList, (string) null);
      this.CommonDataChapter = this.CreateCommonDataChapter(!this.IsSpecification);
      if (this.productsInfo.Count > 1 && this.AvsDocumentForm == AVSDocumentForm.Single)
      {
        this.AvsDocumentForm = documentForm = this.GetDefaultGroupDocumentForm();
        this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
      }
      if (this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA == null)
        this.VariableDataChapter_FormA = new VariableDataChapterFormA(this, this.productsInfo, true);
      else if (this.AvsDocumentForm == AVSDocumentForm.V && this.variableDataChapter_FormV == null)
        this.VariableDataChapter_FormV = new VariableDataChapterFormV(this);
      this.Document.DBAttributeProcessorDictionary = (object) this.attributeProcessorDictionary;
      this.Document.DBAttributeAutoSave = true;
      this.LoadAVSDocumentData(new AVSDocumentContext(false, (Chapter) null, (List<ProductInfo>) null, (SpecificationSection) null, this.GetAllowableDocumentSections(), -1, configureCompositionRoot, false));
      this.ReloadFormatAttributeInEntireSpecificationFromDB();
    }
  }

  /// <summary>Конструктор</summary>
  public AVSDocument()
  {
    AVSPlugin.AllocateAVSLicense();
    this._avsRowEventMessageViewer = new AvsRowEventMessageViewer(this);
    this.additionalChaptersInDataChapter = AvsConfig.General.AdditionalChaptersInDataChapter;
  }

  /// <summary>Освободить ресурсы занятые документом</summary>
  public void Dispose()
  {
    this._avsRowEventMessageViewer.Dispose();
    this.Document = (ImDocument) null;
    this.SpecificationEditor = (ImRtfEditor) null;
    AVSPlugin.ReleaseAVSLicense();
    this.commonChapterTemplate = (TableData) null;
    this.productsPage2Template = (PageData) null;
  }

  /// <summary>Идентификатор правила подбора версий</summary>
  public string FiltrationOwnerID
  {
    get
    {
      if (this.filtrationOwnerID != null)
        return this.filtrationOwnerID;
      if (this.avsWindow != null)
        return this.avsWindow.FiltrationOwnerID;
      VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
      return editorRule != null && editorRule.OwnerId != null ? editorRule.OwnerId : "";
    }
  }

  /// <summary>Окно редактора спецификации</summary>
  [Browsable(false)]
  public AVSWindow AVSWindow
  {
    [DebuggerStepThrough] get => this.avsWindow;
    set
    {
      if (this.avsWindow == value)
        return;
      this.avsWindow = value;
    }
  }

  /// <summary>Текущий вид - табличный или страничный</summary>
  [Browsable(false)]
  internal AVSViewMode ViewMode
  {
    [DebuggerStepThrough] get
    {
      return this.avsWindow != null ? this.avsWindow.ViewMode : AVSViewMode.Page;
    }
  }

  /// <summary>Только чтение</summary>
  [Browsable(false)]
  public bool ReadOnly
  {
    get => this.avsWindow != null ? this.avsWindow.ReadOnly : this.readOnly;
    set
    {
      if (this.avsWindow != null)
        this.avsWindow.ReadOnly = value;
      this.readOnly = value;
    }
  }

  /// <summary>Документ спецификации</summary>
  public ImDocument Document
  {
    [DebuggerStepThrough] get => this.document;
    set
    {
      if (this.document == value)
        return;
      if (this.document != null)
      {
        this.document.BackgroundThreadsFinished -= new BackgroundThreadsFinished_EventHandler(this.document_BackgroundThreadsFinished);
        this.document.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.document_PageAdded);
        this.document.AttributeValueChanging -= new AttributeValueChanging_EventHandler(this.document_AttributeValueChanging);
        this.document.TemplateChanging -= new TemplateChanging_EventHandler(this.document_TemplateChanging);
        this.document.TemplateChanged -= new TemplateChanged_EventHandler(this.document_TemplateChanged);
        this.document.AfterDistributePage -= new PageDistribute_EventHandler(this.document_AfterDistributePage);
        this.document.BeforeDistributePage -= new PageDistribute_EventHandler(this.document_BeforeDistributePage);
        this.document.AfterUpdatePageNumbers -= new AfterUpdatePageNumbers_EventHandler(this.Document_AfterUpdatePageNumbers);
        this.LeftProductKodOKP = (TextData) null;
        this.RightProductKodOKP = (TextData) null;
        this.LeftProductDesignation = (TextData) null;
        this.RightProductDesignation = (TextData) null;
      }
      this.document = value;
      if (this.DocumentControl != null)
        this.DocumentControl.Document = this.document;
      if (this.document == null)
        return;
      if (!this.DocumentID.IsUndefinedId())
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) this.document, this.DocumentGuid, this.DocumentID, this.DocumentDBObjectType, this.DocumentCaption);
      this.document.BackgroundThreadsFinished += new BackgroundThreadsFinished_EventHandler(this.document_BackgroundThreadsFinished);
      this.document.ChildNodeAdded += new ChildNodeAdded_EventHandler(this.document_PageAdded);
      this.document.AttributeValueChanging += new AttributeValueChanging_EventHandler(this.document_AttributeValueChanging);
      this.document.TemplateChanging += new TemplateChanging_EventHandler(this.document_TemplateChanging);
      this.document.TemplateChanged += new TemplateChanged_EventHandler(this.document_TemplateChanged);
      this.document.AfterDistributePage += new PageDistribute_EventHandler(this.document_AfterDistributePage);
      this.document.BeforeDistributePage += new PageDistribute_EventHandler(this.document_BeforeDistributePage);
      this.document.AfterUpdatePageNumbers += new AfterUpdatePageNumbers_EventHandler(this.Document_AfterUpdatePageNumbers);
      this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
      this.document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
      this.document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
      this.document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, this.avsDocTypeGuid.ToString(), false, false, false);
      if (this.documentTemplateGuid != Guid.Empty)
        this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
      else
        this.document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
    }
  }

  private void Document_AfterUpdatePageNumbers(object sender, AfterUpdatePageNumbers_EventArgs e)
  {
    if (!this.DataLoaded)
      return;
    this.UpdateProductPageLinks(e.UpdateUI, e.UpdateLayout);
  }

  /// <summary>Только для внутреннего использования.
  /// Получить значение для ячейки сделанной по шаблону, либо самой ячейки</summary>
  /// <param name="cell">Шаблон, либо ячейка</param>
  /// <returns></returns>
  private static string GetValueFromCellByTemplate(TextData cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    string fromCellByTemplate = "";
    if (cell.IsTemplate && cell.OwnerDocument != null && cell.OwnerDocument.TemplateOwner != null)
    {
      List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
      cell.OwnerDocument.TemplateOwner.FindNodesFromTemplate((DocumentTreeNode) cell, foundNodes);
      for (int index = 0; index < foundNodes.Count; ++index)
      {
        if (foundNodes[index] is TextData textData && textData.Text != "")
        {
          if (textData.Id == cell.Id)
          {
            fromCellByTemplate = textData.Text;
            break;
          }
          if (fromCellByTemplate == "")
            fromCellByTemplate = textData.Text;
        }
      }
    }
    else
      fromCellByTemplate = cell.Text;
    return fromCellByTemplate;
  }

  private static TextData PatchCell(
    string cellId,
    string cellName,
    string[] cellParents,
    string attrName,
    string newId,
    ImDocumentData doc,
    ImDocumentData baseDoc)
  {
    TextData node1 = doc.FindNode(cellId) as TextData;
    DocumentTreeNode node2 = string.IsNullOrEmpty(newId) ? (DocumentTreeNode) null : doc.FindNode(newId);
    if (node1 != null && (cellId == newId || node2 == null) && node1.CheckParentList(cellParents) && (cellName == null || node1.Name == cellName) && node1.ReferenceToTextSource == null)
    {
      if (!string.IsNullOrEmpty(newId))
        node1.Id = newId;
      if (!doc.ContainsAttribute(attrName))
        doc.SetAttributeValue(attrName, "");
      baseDoc.SetAttributeValue(attrName, AVSDocument.GetValueFromCellByTemplate(node1));
      node1.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node1, BaseReferenceNodeType.ntParentDocument, (string) null, attrName), false, false, false);
    }
    return node1;
  }

  private static TextData PatchCell(
    string cellId,
    string cellName,
    string attrName,
    string newId,
    ImDocumentData doc,
    ImDocumentData baseDoc)
  {
    return AVSDocument.PatchCell(cellId, cellName, (string[]) null, attrName, newId, doc, baseDoc);
  }

  internal static void PatchAVSDocumentLRI(ImDocumentData document)
  {
    if (document.IsTemplate)
    {
      AVSDocument.PatchTemplateForLRIv4(document);
    }
    else
    {
      if (!AVSDocument.PatchTemplateForLRIv4(document.DocumentTemplate))
        return;
      AVSDocument.PatchDocumentLRIv4(document);
    }
  }

  internal static void PatchAVSDocumentLiteraReference(ImDocumentData document)
  {
    if (document.Template != null)
      AVSDocument.PatchAVSDocumentLiteraReference((ImDocumentData) document.Template);
    TextData cellFromTitleBlock = AVSDocument.GetLiteraCellFromTitleBlock(document);
    if (cellFromTitleBlock == null || !(cellFromTitleBlock.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource))
      return;
    referenceToTextSource.PassiveLink = true;
  }

  internal static void PatchDocumentLRIv4(ImDocumentData document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
    DocumentTreeNode node1 = document.Template.FindNode("Таблица изменений");
    TableData node2 = document.Template.FindNode("Запись ЛРИ") as TableData;
    document.FindNodesFromTemplate(node1, foundNodes);
    foreach (TableData tableData1 in foundNodes)
    {
      for (TableData tableData2 = tableData1; tableData2 != null; tableData2 = tableData2.NextTable)
      {
        foreach (DocumentTreeNode node3 in tableData2.Nodes)
        {
          node3.AssignCloneByTemplateWithParent(false);
          node3.AssignClonedByTemplateWithParent(false);
          if (node3.NodesCount == node2.NodesCount)
            node3.ReplaceTemplatesRecursive((DocumentTreeNode) node2);
        }
      }
    }
  }

  private static void PatchLriTableHeaderTemplate(ImDocumentData docTemplate)
  {
    if (!(docTemplate.FindNode("Шапка листа регистрации изменений") is TableData node) || node.NodesCount != 2)
      return;
    node.Nodes[0].Id = "lri.hdr0";
    node.Nodes[0].Nodes[0].Id = "lri.hdr00";
    node.Nodes[1].Id = "lri.hdr1";
    node.Nodes[1].Nodes[0].Id = "lri.hdr10";
    node.Nodes[1].Nodes[1].Id = "lri.hdr11";
    node.Nodes[1].Nodes[1].Nodes[0].Id = "lri.hdr110";
    node.Nodes[1].Nodes[1].Nodes[0].Nodes[0].Id = "lri.hdr1100";
    node.Nodes[1].Nodes[1].Nodes[1].Id = "lri.hdr111";
    node.Nodes[1].Nodes[1].Nodes[1].Nodes[0].Id = "lri.hdr1110";
    node.Nodes[1].Nodes[1].Nodes[1].Nodes[1].Id = "lri.hdr1111";
    node.Nodes[1].Nodes[1].Nodes[1].Nodes[2].Id = "lri.hdr1112";
    node.Nodes[1].Nodes[1].Nodes[1].Nodes[3].Id = "lri.hdr1113";
    node.Nodes[1].Nodes[2].Id = "lri.hdr12";
    node.Nodes[1].Nodes[3].Id = "lri.hdr13";
    node.Nodes[1].Nodes[4].Id = "lri.hdr14";
    node.Nodes[1].Nodes[5].Id = "lri.hdr15";
    node.Nodes[1].Nodes[6].Id = "lri.hdr16";
  }

  private static void PatchLriAdditionalColumnsTemplate(ImDocumentData docTemplate)
  {
    if (!(docTemplate.FindNode("Дополнительные графы #4") is TableData node))
      node = docTemplate.FindNode("lri. Дополнительные графы #4") as TableData;
    else
      node.Id = "lri.Дополнительные графы #4";
    if (node == null || node.NodesCount != 5)
      return;
    node.Nodes[0].Id = "lri.add0";
    node.Nodes[0].Nodes[0].Id = "lri.add00";
    node.Nodes[0].Nodes[1].Id = "lri.add.Подп. и дата дубл.";
    node.Nodes[1].Id = "lri.add1";
    node.Nodes[1].Nodes[0].Id = "lri.add10";
    node.Nodes[1].Nodes[1].Id = "lri.add.Инв. № дубл.";
    node.Nodes[2].Id = "lri.add2";
    node.Nodes[2].Nodes[0].Id = "lri.add20";
    node.Nodes[2].Nodes[1].Id = "lri.add.Взам. инв. №";
    node.Nodes[3].Id = "lri.add3";
    node.Nodes[3].Nodes[0].Id = "lri.add30";
    node.Nodes[3].Nodes[1].Id = "lri.add.Подп. и дата подл.";
    node.Nodes[4].Id = "lri.add4";
    node.Nodes[4].Nodes[0].Id = "lri.add40";
    node.Nodes[4].Nodes[1].Id = "lri.add.Инв.№ подл.";
  }

  private static void PatchLriPageBaseTitleTemplate(ImDocumentData docTemplate)
  {
    PageData node = docTemplate.FindNode("Лист регистрации изменений") as PageData;
    TableData tableData = (TableData) null;
    if (node != null)
      tableData = node.FindFirstChildNodeByName("Основная надпись. Продолжение") as TableData;
    if (tableData == null)
      return;
    tableData.Id = "lri.Основная надпись";
    if (tableData.NodesCount != 2)
      return;
    tableData.Nodes[0].Id = "lri.bt0";
    tableData.Nodes[0].Nodes[0].Id = "lri.bt00";
    tableData.Nodes[0].Nodes[0].Nodes[0].Id = "lri.TIZM01";
    tableData.Nodes[0].Nodes[0].Nodes[1].Id = "lri.TIZM02";
    tableData.Nodes[0].Nodes[0].Nodes[2].Id = "lri.TIZM03";
    tableData.Nodes[0].Nodes[0].Nodes[3].Id = "lri.TIZM04";
    tableData.Nodes[0].Nodes[0].Nodes[4].Id = "lri.TIZM05";
    tableData.Nodes[0].Nodes[1].Id = "lri.bt01";
    tableData.Nodes[0].Nodes[1].Nodes[0].Id = "lri.TIZM11";
    tableData.Nodes[0].Nodes[1].Nodes[1].Id = "lri.TIZM12";
    tableData.Nodes[0].Nodes[1].Nodes[2].Id = "lri.TIZM13";
    tableData.Nodes[0].Nodes[1].Nodes[3].Id = "lri.TIZM14";
    tableData.Nodes[0].Nodes[1].Nodes[4].Id = "lri.TIZM15";
    tableData.Nodes[0].Nodes[2].Id = "lri.bt02";
    tableData.Nodes[0].Nodes[2].Nodes[0].Id = "lri.TIZM21";
    tableData.Nodes[0].Nodes[2].Nodes[1].Id = "lri.TIZM22";
    tableData.Nodes[0].Nodes[2].Nodes[2].Id = "lri.TIZM23";
    tableData.Nodes[0].Nodes[2].Nodes[3].Id = "lri.TIZM24";
    tableData.Nodes[0].Nodes[2].Nodes[4].Id = "lri.TIZM25";
    tableData.Nodes[1].Id = "lri.bt1";
    tableData.Nodes[1].Nodes[0].Id = "lri.bt10";
    tableData.Nodes[1].Nodes[0].Nodes[0].Id = "lri.bt.Обозначение документа";
    tableData.Nodes[1].Nodes[0].Nodes[1].Id = "lri.bt101";
    tableData.Nodes[1].Nodes[0].Nodes[1].Nodes[0].Id = "lri.bt1010";
    tableData.Nodes[1].Nodes[0].Nodes[1].Nodes[1].Id = "lri.bt.Лист";
  }

  /// <summary>Пропатчить ЛРИ в шаблоне документа</summary>
  /// <param name="docTemplate">Шаблон</param>
  /// <returns>Возвращает true, если шаблон был обновлён и false, если он не требует обновления, либо не совпадает структура шаблона</returns>
  internal static bool PatchTemplateForLRIv4(ImDocumentData docTemplate)
  {
    bool flag = docTemplate != null ? AVSDocument.PatchLriTableTemplate(docTemplate) : throw new ArgumentNullException("_docTemplate");
    if (!flag)
      return false;
    AVSDocument.PatchLriTableHeaderTemplate(docTemplate);
    AVSDocument.PatchLriPageBaseTitleTemplate(docTemplate);
    AVSDocument.PatchLriAdditionalColumnsTemplate(docTemplate);
    return flag;
  }

  private static bool PatchLriTableTemplate(ImDocumentData docTemplate)
  {
    if (!(docTemplate.FindNode("Таблица изменений") is TableData node1))
      return false;
    foreach (DocumentTreeNode node2 in node1.Nodes)
      node2.AssignCloneByTemplateWithParent(false);
    TableData dataOwner;
    node1.FindDataPositionInFlow(0, out dataOwner);
    if (!(dataOwner.Nodes[0] is TableData node3) || node3.Nodes.Count != 10 || node3.Nodes[0].Id == "NIZM")
      return false;
    node3.Id = "Запись ЛРИ";
    node3.Name = "Запись";
    node3.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.LRIRow_TypeName, false, false);
    AVSDocument.PatchCellIdAndName(node3, 0, "NIZM", "Номер изменения");
    AVSDocument.PatchCellIdAndName(node3, 1, "LIZM", "Изменённых");
    AVSDocument.PatchCellIdAndName(node3, 2, "LZAM", "Замененных");
    AVSDocument.PatchCellIdAndName(node3, 3, "LNEW", "Новых");
    AVSDocument.PatchCellIdAndName(node3, 4, "LANN", "Аннулированных");
    AVSDocument.PatchCellIdAndName(node3, 5, "LNUM", "Листов");
    AVSDocument.PatchCellIdAndName(node3, 6, "NUMDOC", "N документа");
    AVSDocument.PatchCellIdAndName(node3, 7, "INNUM", "Входящий N");
    AVSDocument.PatchCellIdAndName(node3, 8, "LUSER", "Подпись");
    AVSDocument.PatchCellIdAndName(node3, 9, "LDAT", "Дата");
    return true;
  }

  private static void PatchCellIdAndName(TableData docRow, int index, string id, string name)
  {
    docRow.Nodes[index].Id = id;
    docRow.Nodes[index].SetName(name, false, false);
  }

  internal static void PatchProductNumbersHeader(ImDocumentData doc)
  {
    if (doc.DocumentTemplate != null)
    {
      AVSDocument.PatchProductNumbersHeader(doc.DocumentTemplate);
    }
    else
    {
      if (doc.LoadedFileCreatedAfterBuilds("7") || !(doc.FindNode("Заголовок спецификации")?.FindFirstNodeByName("Шапка с обозначением") is TextData firstNodeByName))
        return;
      firstNodeByName.AssignText("Кол. на исполн.", false, false, false);
    }
  }

  internal static void PatchDocumentAttr(ImDocumentData doc, Guid templateGuid)
  {
    if (doc.DocumentTemplate != null)
    {
      AVSDocument.PatchDocumentAttr(doc.DocumentTemplate, templateGuid);
    }
    else
    {
      ImDocumentData baseDoc = doc.TemplateOwner ?? doc;
      AVSDocument.PatchCell("Подразделение", (string) null, (string[]) null, "Подразделение", (string) null, doc, baseDoc);
      AVSDocument.PatchCell("Литера #2", (string) null, (string[]) null, "Литера #2", (string) null, doc, baseDoc);
      AVSDocument.PatchCell("Литера #3", (string) null, (string[]) null, "Литера #3", (string) null, doc, baseDoc);
      string[] cellParents1 = new string[4]
      {
        "47",
        "10",
        "9",
        "Основная надпись"
      };
      AVSDocument.PatchCell("52", (string) null, cellParents1, "Разработал:Подпись", "Разработал:Подпись", doc, baseDoc);
      AVSDocument.PatchCell("54", (string) null, cellParents1, "Разработал:Дата", "Разработал:Дата", doc, baseDoc);
      string[] cellParents2 = new string[4]
      {
        "56",
        "10",
        "9",
        "Основная надпись"
      };
      AVSDocument.PatchCell("61", (string) null, cellParents2, "Проверил:Подпись", "Проверил:Подпись", doc, baseDoc);
      AVSDocument.PatchCell("63", (string) null, cellParents2, "Проверил:Дата", "Проверил:Дата", doc, baseDoc);
      string[] cellParents3 = new string[4]
      {
        "65",
        "10",
        "9",
        "Основная надпись"
      };
      AVSDocument.PatchCell("66", (string) null, cellParents3, "Св.строка:Работа", "Св.строка:Работа", doc, baseDoc);
      AVSDocument.PatchCell("68", (string) null, cellParents3, "Св.строка:Фамилия", "Св.строка:Фамилия", doc, baseDoc);
      AVSDocument.PatchCell("70", (string) null, cellParents3, "Св.строка:Подпись", "Св.строка:Подпись", doc, baseDoc);
      AVSDocument.PatchCell("72", (string) null, cellParents3, "Св.строка:Дата", "Св.строка:Дата", doc, baseDoc);
      string[] cellParents4 = new string[4]
      {
        "74",
        "10",
        "9",
        "Основная надпись"
      };
      AVSDocument.PatchCell("79", (string) null, cellParents4, "Н.контр:Подпись", "Н.контр:Подпись", doc, baseDoc);
      AVSDocument.PatchCell("81", (string) null, cellParents4, "Н.контр:Дата", "Н.контр:Дата", doc, baseDoc);
      string[] cellParents5 = new string[4]
      {
        "83",
        "10",
        "9",
        "Основная надпись"
      };
      AVSDocument.PatchCell("88", (string) null, cellParents5, "Утвердил:Подпись", "Утвердил:Подпись", doc, baseDoc);
      AVSDocument.PatchCell("90", (string) null, cellParents5, "Утвердил:Дата", "Утвердил:Дата", doc, baseDoc);
      AVSDocument.PatchCell("763", "Изм", "Изм_1", "Изм_1", doc, baseDoc);
      AVSDocument.PatchCell("15", "Изм", "Изм_1", "Изм_1.2", doc, baseDoc);
      AVSDocument.PatchCell("769", "Изм", "Изм_2", "Изм_2", doc, baseDoc);
      AVSDocument.PatchCell("26", "Изм", "Изм_2", "Изм_2.2", doc, baseDoc);
      AVSDocument.PatchCell("764", "Лист", "Изм:Лист_1", "Изм:Лист_1", doc, baseDoc);
      AVSDocument.PatchCell("17", "Лист", "Изм:Лист_1", "Изм:Лист_1.2", doc, baseDoc);
      AVSDocument.PatchCell("770", "Лист", "Изм:Лист_2", "Изм:Лист_2", doc, baseDoc);
      AVSDocument.PatchCell("28", "Лист", "Изм:Лист_2", "Изм:Лист_2.2", doc, baseDoc);
      AVSDocument.PatchCell("765", "№ докум.", "Изм:№ докум._1", "Изм:№ докум._1", doc, baseDoc);
      AVSDocument.PatchCell("19", "№ докум.", "Изм:№ докум._1", "Изм:№ докум._1.2", doc, baseDoc);
      AVSDocument.PatchCell("771", "№ докум.", "Изм:№ докум._2", "Изм:№ докум._2", doc, baseDoc);
      AVSDocument.PatchCell("30", "№ докум.", "Изм:№ докум._2", "Изм:№ докум._2.2", doc, baseDoc);
      AVSDocument.PatchCell("766", "Подп.", "Изм:Подп._1", "Изм:Подп._1", doc, baseDoc);
      AVSDocument.PatchCell("21", "Подп.", "Изм:Подп._1", "Изм:Подп._1.2", doc, baseDoc);
      AVSDocument.PatchCell("772", "Подп.", "Изм:Подп._2", "Изм:Подп._2", doc, baseDoc);
      AVSDocument.PatchCell("32", "Подп.", "Изм:Подп._2", "Изм:Подп._2.2", doc, baseDoc);
      AVSDocument.PatchCell("767", "Дата", "Изм:Дата_1", "Изм:Дата_1", doc, baseDoc);
      AVSDocument.PatchCell("23", "Дата", "Изм:Дата_1", "Изм:Дата_1.2", doc, baseDoc);
      AVSDocument.PatchCell("773", "Дата", "Изм:Дата_2", "Изм:Дата_2", doc, baseDoc);
      AVSDocument.PatchCell("34", "Дата", "Изм:Дата_2", "Изм:Дата_2.2", doc, baseDoc);
      string str1 = "Инв. № подл.";
      if (!doc.ContainsAttribute(str1))
        doc.SetAttributeValue(str1, "");
      if (!(doc.FindNode(str1) is TextData node2) && doc.FindNode("Инв. № подл. #2") is TextData node2)
        node2.Id = str1;
      if (node2 != null && node2.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str1, AVSDocument.GetValueFromCellByTemplate(node2));
        node2.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node2, BaseReferenceNodeType.ntParentDocument, (string) null, str1), false, false, false);
      }
      if (doc.FindNode("Инв.№ подл. 2 #3") is TextData node3 && node3.ReferenceToTextSource == null)
        node3.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node3, BaseReferenceNodeType.ntParentDocument, (string) null, str1), false, false, false);
      if (doc.FindNode("Инв.№ подл. 2 #4") is TextData node4 && node4.ReferenceToTextSource == null)
        node4.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node4, BaseReferenceNodeType.ntParentDocument, (string) null, str1), false, false, false);
      if (doc.FindNode("Инв. № подл. #3") is TextData node5 && node5.ReferenceToTextSource == null)
        node5.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node5, BaseReferenceNodeType.ntParentDocument, (string) null, str1), false, false, false);
      if (node5 == null && templateGuid == AvsIDCache.StdTemplateElementList)
        AVSDocument.PatchCell("Инв.№ подл. 2", "Инв.№ подл.", str1, (string) null, doc, baseDoc);
      AVSDocument.PatchCell("511", "Инв. № подл.", str1, "Инв. № подл. 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("511", "Инв. № подл.", str1, "Инв. № подл. 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("979", "Инв. № подл.", str1, "Инв. № подл. 2 #0.6", doc, baseDoc);
      AVSDocument.PatchCell("192", "Инв. № подл.", str1, "Инв. № подл. 2 #0.7", doc, baseDoc);
      string str2 = "Подп. и дата подл.";
      if (!doc.ContainsAttribute(str2))
        doc.SetAttributeValue(str2, "");
      if (!(doc.FindNode(str2) is TextData node7) && doc.FindNode("Подп. и дата подл. #2") is TextData node7)
        node7.Id = str2;
      if (node7 != null && node7.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str2, AVSDocument.GetValueFromCellByTemplate(node7));
        node7.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node7, BaseReferenceNodeType.ntParentDocument, (string) null, str2), false, false, false);
      }
      if (doc.FindNode("Подп. и дата подл. 2 #3") is TextData node8 && node8.ReferenceToTextSource == null)
        node8.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node8, BaseReferenceNodeType.ntParentDocument, (string) null, str2), false, false, false);
      if (doc.FindNode("Подп. и дата подл. 2 #4") is TextData node9 && node9.ReferenceToTextSource == null)
        node9.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node9, BaseReferenceNodeType.ntParentDocument, (string) null, str2), false, false, false);
      if (doc.FindNode("Подп. и дата подл. #3") is TextData node10 && node10.ReferenceToTextSource == null)
        node10.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node10, BaseReferenceNodeType.ntParentDocument, (string) null, str2), false, false, false);
      AVSDocument.PatchCell("512", "Подп. и дата подл.", str2, "Подп. и дата подл. 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("980", "Подп. и дата подл.", str2, "Подп. и дата подл. 2 #0.6", doc, baseDoc);
      AVSDocument.PatchCell("193", "Подп. и дата подл.", str2, "Подп. и дата подл. 2 #0.7", doc, baseDoc);
      if (templateGuid == AvsIDCache.StdTemplateElementList)
        AVSDocument.PatchCell("Подп. и дата подл. 2", (string) null, str2, (string) null, doc, baseDoc);
      string str3 = "Взам. инв. №";
      if (!doc.ContainsAttribute(str3))
        doc.SetAttributeValue(str3, "");
      if (!(doc.FindNode(str3) is TextData node12) && doc.FindNode("Взам. инв. № #2") is TextData node12)
        node12.Id = str3;
      if (node12 != null && node12.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str3, AVSDocument.GetValueFromCellByTemplate(node12));
        node12.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node12, BaseReferenceNodeType.ntParentDocument, (string) null, str3), false, false, false);
      }
      if (doc.FindNode("Взам. инв. № 2 #3") is TextData node13 && node13.ReferenceToTextSource == null)
        node13.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node13, BaseReferenceNodeType.ntParentDocument, (string) null, str3), false, false, false);
      if (doc.FindNode("Взам. инв. № 2 #4") is TextData node14 && node14.ReferenceToTextSource == null)
        node14.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node14, BaseReferenceNodeType.ntParentDocument, (string) null, str3), false, false, false);
      if (doc.FindNode("Взам. инв. № #3") is TextData node15 && node15.ReferenceToTextSource == null)
        node15.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node15, BaseReferenceNodeType.ntParentDocument, (string) null, str3), false, false, false);
      AVSDocument.PatchCell("981", "Взам. инв. №", str3, "Взам. инв. № 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("513", "Взам. инв. №", str3, "Взам. инв. № 2 #0.6", doc, baseDoc);
      AVSDocument.PatchCell("202", "Взам. инв. №", str3, "Взам. инв. № 2 #0.7", doc, baseDoc);
      if (templateGuid == AvsIDCache.StdTemplateElementList)
        AVSDocument.PatchCell("Взам. инв. № 2", (string) null, str3, (string) null, doc, baseDoc);
      string str4 = "Инв. № дубл.";
      if (!doc.ContainsAttribute(str4))
        doc.SetAttributeValue(str4, "");
      if (!(doc.FindNode(str4) is TextData node17) && doc.FindNode("Инв. № дубл. #2") is TextData node17)
        node17.Id = str4;
      if (node17 != null && node17.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str4, AVSDocument.GetValueFromCellByTemplate(node17));
        node17.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node17, BaseReferenceNodeType.ntParentDocument, (string) null, str4), false, false, false);
      }
      if (doc.FindNode("Инв. № дубл. 2") is TextData ownerNode1 && ownerNode1.Parent != null && ownerNode1.Parent.Id == "287")
      {
        ownerNode1.Id = "0.280";
        if (ownerNode1.Parent.Nodes.Count > 1)
        {
          if (ownerNode1.Parent.Nodes[1] is TextData ownerNode1)
          {
            ownerNode1.Id = "Инв. № дубл. 2 #3";
            ownerNode1.Name = "Инв. № дубл.";
          }
        }
        else
          ownerNode1 = (TextData) null;
      }
      if (ownerNode1 != null && ownerNode1.ReferenceToTextSource == null)
        ownerNode1.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode1, BaseReferenceNodeType.ntParentDocument, (string) null, str4), false, false, false);
      if (doc.FindNode("Инв. № дубл. 2 #2") is TextData ownerNode3 && ownerNode3.Parent != null && (ownerNode3.Parent.Id == "190" || ownerNode3.Parent.Id == "368" || ownerNode3.Parent.Id == "38"))
      {
        ownerNode3.Id = "0.281";
        ownerNode3.Name = (string) null;
        if (ownerNode3.Parent.Nodes.Count > 1)
        {
          if (ownerNode3.Parent.Nodes[1] is TextData ownerNode3)
          {
            ownerNode3.Id = "Инв. № дубл. 2 #4";
            ownerNode3.Name = "Инв. № дубл.";
          }
        }
        else
          ownerNode3 = (TextData) null;
      }
      if (ownerNode3 != null && ownerNode3.ReferenceToTextSource == null)
        ownerNode3.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode3, BaseReferenceNodeType.ntParentDocument, (string) null, str4), false, false, false);
      AVSDocument.PatchCell("982", "Инв. № дубл.", str4, "Инв. № дубл. 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("514", "Инв. № дубл.", str4, "Инв. № дубл. 2 #0.6", doc, baseDoc);
      AVSDocument.PatchCell("210", "Инв. № дубл.", str4, "Инв. № дубл. 2 #0.7", doc, baseDoc);
      AVSDocument.PatchCell("Инв. № дубл. #3", (string) null, str4, (string) null, doc, baseDoc);
      string str5 = "Подп. и дата дубл.";
      if (!doc.ContainsAttribute(str5))
        doc.SetAttributeValue(str5, "");
      if (!(doc.FindNode(str5) is TextData node19))
      {
        if (!(doc.FindNode("Подп. и дата дубл. #2") is TextData node19))
          node19 = doc.FindNode("Подп и дата дубл.") as TextData;
        if (node19 != null)
          node19.Id = str5;
      }
      if (node19 != null && node19.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str5, AVSDocument.GetValueFromCellByTemplate(node19));
        node19.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node19, BaseReferenceNodeType.ntParentDocument, (string) null, str5), false, false, false);
      }
      AVSDocument.PatchCell("Подп. и дата дубл. 2 #3", (string) null, str5, (string) null, doc, baseDoc);
      AVSDocument.PatchCell("Подп. и дата дубл. 2 #4", (string) null, str5, (string) null, doc, baseDoc);
      AVSDocument.PatchCell("983", "Подп и дата дубл.", str5, "Подп. и дата дубл. 2 #0.5", doc, baseDoc);
      AVSDocument.PatchCell("515", "Подп и дата дубл.", str5, "Подп. и дата дубл. 2 #0.6", doc, baseDoc);
      AVSDocument.PatchCell("Подп и дата дубл.", (string) null, str5, "Подп. и дата дубл. #0", doc, baseDoc);
      AVSDocument.PatchCell("211", "Подп и дата дубл.", str5, "Подп. и дата дубл. 2 #0.7", doc, baseDoc);
      if (templateGuid == AvsIDCache.StdTemplateElementList)
        AVSDocument.PatchCell("Подп. и дата дубл. 2", (string) null, str5, (string) null, doc, baseDoc);
      string str6 = "Справ. №";
      if (!(doc.FindNode(str6) is TextData node21) && doc.FindNode("Справ. № #2") is TextData node21)
        node21.Id = str6;
      if (!doc.ContainsAttribute(str6))
        doc.SetAttributeValue(str6, "");
      if (node21 != null && node21.ReferenceToTextSource == null)
      {
        baseDoc.SetAttributeValue(str6, AVSDocument.GetValueFromCellByTemplate(node21));
        node21.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node21, BaseReferenceNodeType.ntParentDocument, (string) null, str6), false, false, false);
      }
      if (!(doc.FindNode("Справ. № #3") is TextData node22) || node22.ReferenceToTextSource != null)
        return;
      baseDoc.SetAttributeValue(str6, AVSDocument.GetValueFromCellByTemplate(node22));
      node22.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) node22, BaseReferenceNodeType.ntParentDocument, (string) null, str6), false, false, false);
    }
  }

  /// <summary>Документ Интермех. Используется только для интерфейса</summary>
  IImDocument IAVSDocument.ImDocument => (IImDocument) this.Document;

  private void document_BeforeDistributePage(object sender, PageDistribute_EventArgs e)
  {
    if (!e.Page.IsAdditionalPage)
      return;
    e.Page.SetNeedUpdateLayoutFlag(true, false, false, false, true);
  }

  private void document_AfterDistributePage(object sender, PageDistribute_EventArgs e)
  {
  }

  /// <summary>Создать страницу листа регистрации изменений</summary>
  /// <param name="updateUI">Обновить интерфейс</param>
  public PageData AddNewLRIPage(bool updateUI)
  {
    PageData child = (PageData) this.lriPage_Template.CloneFromTemplate();
    this.document.AddChildNode((DocumentTreeNode) child, updateUI, false);
    return child;
  }

  /// <summary>Данный элемент документа находится на листе регистрации изменений</summary>
  /// <param name="docNode">Элемент документа</param>
  /// <returns></returns>
  internal bool IsDocumentNodeOnLRIPage(DocumentTreeNode docNode)
  {
    if (docNode == null)
      return false;
    if (!(docNode is PageData pageData) && docNode is PageElementNode pageElementNode)
      pageData = pageElementNode.Page;
    return pageData != null && this.IsDocumentLRIPage(pageData.FindFirstPage());
  }

  /// <summary>Данная страница является листом регистрации изменений</summary>
  /// <param name="page">Страница документа</param>
  /// <returns></returns>
  internal bool IsDocumentLRIPage(PageData page)
  {
    PageData pageData = page != null ? page.FindFirstPage() : throw new ArgumentNullException(nameof (page));
    if (this.lriPage != null)
      return pageData == this.lriPage;
    return this.lriPage_Template != null && pageData.TemplateId == this.lriPage_Template.Id;
  }

  /// <summary>Проверить лист регистрации изменений</summary>
  public void Check_ChangesPage(bool updateUI)
  {
    if (this.document == null)
      return;
    int num = this.document.PageCount;
    if (this.lriPage != null)
      num = this.lriPage.Index;
    if (this.AVSCommonPropertiesSchema.CreateChangesList && num >= this.AVSCommonPropertiesSchema.ChangesListCount)
    {
      if (this.lriPage != null || this.lriPage_Template == null)
        return;
      this.lriPage = (PageData) this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.lriPage_Template);
      if (this.lriPage != null)
        return;
      this.lriPage = this.AddNewLRIPage(true);
    }
    else
    {
      if (this.lriPage == null)
        return;
      TableData tableData = this.lriTableTemplate == null ? this.lriPage.FindFirstNodeFromTemplate_Recursive("Таблица изменений") as TableData : this.lriPage.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.lriTableTemplate) as TableData;
      if (tableData != null && !tableData.IsEmptyData(true, true))
        return;
      this.lriPage.Remove(updateUI, false);
      this.lriPage = (PageData) null;
    }
  }

  private void document_BackgroundThreadsFinished(object sender, BackgroundThreadsFinishedArgs e)
  {
    this.Check_ChangesPage(false);
  }

  /// <summary>Документ является перечнем элементов</summary>
  public bool IsElementList
  {
    [DebuggerStepThrough] get => AVSDocumentsSettings.IsElementListDocType(this.avsDocumentType);
  }

  /// <summary>Документ является спецификацией</summary>
  public bool IsSpecification
  {
    [DebuggerStepThrough] get => AVSDocumentsSettings.IsSpecificationDocType(this.avsDocumentType);
  }

  /// <summary>Документ является спецификацией</summary>
  public bool IsGeneratedDoc
  {
    [DebuggerStepThrough] get => this.isGeneratedDoc;
  }

  /// <summary>Данный тип объекта БД является спецификацией</summary>
  /// <param name="objectType">Тип объекта БД</param>
  /// <returns></returns>
  public static bool IsSpecificationObjectType(int objectType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Specification);
  }

  /// <summary>Тип конструкторского документа</summary>
  public AVSDocumentType AVSDocType
  {
    [DebuggerStepThrough] get => this.avsDocumentType;
  }

  /// <summary>Внутренний Guid типа конструкторского документа</summary>
  public Guid AvsDocTypeGuid
  {
    [DebuggerStepThrough] get => this.avsDocTypeGuid;
  }

  /// <summary>Получить настройки типа конструкторского документа</summary>
  /// <returns></returns>
  public AVSDocumentTypeSettings GetDocumentTypeSettings()
  {
    return AVSDocumentsSettings.Instance.GetAVSDocumentTypeSettings(this.avsDocTypeGuid);
  }

  /// <summary>Структура наследования настроек типа конструкторского документа</summary>
  /// <returns></returns>
  public SettingsStructure DocumentSettingsStructure
  {
    get
    {
      return AVSDocumentsSettings.Instance.GetAVSDocumentTypeSettings(this.avsDocTypeGuid)?.SettingsInheritanceStructure;
    }
  }

  internal static AVSDocumentForm GetDefaultGroupDocumentForm(
    AVSDocumentType docType,
    List<ProductInfo> products)
  {
    AVSDocumentForm docForm;
    switch (AvsConfig.General.DefaultSpecificationForm)
    {
      case DefaultGroupSpecificationForm.A:
        docForm = AVSDocumentForm.A;
        break;
      case DefaultGroupSpecificationForm.B:
        docForm = AVSDocumentForm.B;
        break;
      case DefaultGroupSpecificationForm.V:
        docForm = AVSDocumentForm.V;
        break;
      default:
        docForm = AVSDocumentForm.A;
        break;
    }
    if (!AVSDocumentsSettings.IsAllowableDocumentForm(docType, docForm))
      docForm = AVSDocumentForm.A;
    return docForm;
  }

  /// <summary>Получить форму группового документа по умолчанию</summary>
  /// <returns></returns>
  private AVSDocumentForm GetDefaultGroupDocumentForm()
  {
    return AVSDocument.GetDefaultGroupDocumentForm(this.AVSDocType, this.productsInfo);
  }

  /// <summary>Получить тип спецификации по умолчанию</summary>
  /// <returns></returns>
  internal static AVSDocumentType GetDefaultSpecificationType()
  {
    switch (AvsConfig.General.DefaultSpecificationType)
    {
      case AVSSpecificationType.ESKD:
        return AVSDocumentType.Specification;
      case AVSSpecificationType.AutoProm:
        return AVSDocumentType.AutoIndustrySpecification;
      case AVSSpecificationType.Export:
        return AVSDocumentType.ExportSpecification;
      default:
        return AVSDocumentType.Specification;
    }
  }

  public AVSCommonPropertiesSchema AVSCommonPropertiesSchema
  {
    get
    {
      if (this.avsCommonPropertiesSchema == null)
        this.avsCommonPropertiesSchema = this.LoadAVSCommonPropertiesSchema();
      return this.avsCommonPropertiesSchema;
    }
  }

  private AVSCommonPropertiesSchema LoadAVSCommonPropertiesSchema()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (AVSCommonPropertiesSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_ConstructorDocumentProperties, typeof (AVSCommonPropertiesSchema));
  }

  /// <summary>Кэш настроек сокращения обозначений исполнений</summary>
  internal DesignationTrimSchema DesignationTrimSchema
  {
    get
    {
      if (this.designationTrimSchema == null && !this.DocumentID.IsUndefinedId())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.designationTrimSchema = (DesignationTrimSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_DesignationTrimSchema, typeof (DesignationTrimSchema));
      }
      return this.designationTrimSchema;
    }
  }

  /// <summary>Обработчик события TemplateChanging документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void document_TemplateChanging(object sender, TemplateChanging_EventArgs e)
  {
    if (e.NewTemplate == null || this.Document == null || this.Document.IsLoading)
      return;
    e.Cancel = this.FindAllTemplates((ImDocumentData) e.NewTemplate, true) != null;
    if (e.Cancel)
      return;
    this.UpdateDocumentRowFieldsInfo();
    this.UpdateViewNodes(false, false, true, false, true, EmptyRowUpdateMode.DontChange);
  }

  /// <summary>Обработчик события TemplateChanged документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void document_TemplateChanged(object sender, TemplateChanged_EventArgs e)
  {
    this.UpdateProductHeadersOnPages(true, true);
  }

  /// <summary>Обработчик события AttributeValueChanging документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void document_AttributeValueChanging(object sender, AttributeValueChanging_EventArgs e)
  {
    if (!(e.AttributeName == DocumentTreeNode.AttributeName_DocName) && !(e.AttributeName == DocumentTreeNode.AttributeName_Designation) && !(e.AttributeName == AVSDocument.DocumentAttribute_OKPCode))
      return;
    try
    {
      int num = -1;
      if (e.AttributeName == DocumentTreeNode.AttributeName_DocName)
        num = AvsIDCache.Attr_Name;
      else if (e.AttributeName == DocumentTreeNode.AttributeName_Designation)
        num = AvsIDCache.Attr_Designation;
      else if (e.AttributeName == AVSDocument.DocumentAttribute_OKPCode)
        num = AvsIDCache.Attr_OKPCode;
      if (num == -1)
        return;
      string str1 = Convert.ToString(e.NewValue);
      string str2 = Convert.ToString(e.OldValue);
      if (num == AvsIDCache.Attr_Name)
        str2 = this.DocumentName ?? "";
      else if (num == AvsIDCache.Attr_Designation)
        str2 = this.DocumentDesignation ?? "";
      else if (num == AvsIDCache.Attr_OKPCode && this.productsInfo.Count > 0)
        str2 = this.productsInfo[0].ProductOKPCode ?? "";
      if (str2 == str1)
        return;
      e.Cancel = true;
      if (this.ReadOnly)
        return;
      IAttributesLockService service = ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(num, e.NewValue)
        };
        List<long> longList = new List<long>();
        List<int> intList = new List<int>();
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        string documentDesignation = this.DocumentDesignation;
        string documentName = this.DocumentName;
        if (service != null)
        {
          ICollection<int> lockedAttributes1 = service.GetLockedAttributes(AttributableElements.Object, this.DocumentID, this.DocumentDBObjectType);
          if (lockedAttributes1 != null && lockedAttributes1.Contains(num))
            throw new Exception($"Запрещено изменение значения атрибута '{MetaDataHelper.GetAttributeTypeName(num)}' в спецификации");
          for (int index = 0; index < this.productsInfo.Count; ++index)
          {
            if (!this.productsInfo[index].Id.IsUndefinedId())
            {
              ICollection<int> lockedAttributes2 = service.GetLockedAttributes(AttributableElements.Object, this.productsInfo[index].Id, this.productsInfo[index].ObjectType);
              if (lockedAttributes2 != null && lockedAttributes2.Contains(num))
                throw new Exception($"Запрещено изменение значения атрибута '{MetaDataHelper.GetAttributeTypeName(num)}' у объекта '{this.productsInfo[index].Name}'");
            }
          }
        }
        string productDesignation = this.BaseProductDesignation;
        if (num != AvsIDCache.Attr_OKPCode)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.DocumentID, false);
          if (dbObject != null && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          {
            dbObject.SetAttributesValues(valuesList);
            this.DocumentCaption = dbObject.Caption;
            longList.Add(this.DocumentID);
            intList.Add(this.DocumentDBObjectType);
            if (num == AvsIDCache.Attr_Name)
              this.DocumentName = str1;
            else if (num == AvsIDCache.Attr_Designation)
              this.DocumentDesignation = str1;
            arrayList1.Add((object) str2);
            arrayList2.Add((object) str1);
            e.Cancel = false;
          }
        }
        if (num != AvsIDCache.Attr_OKPCode)
        {
          for (int index = 0; index < this.productsInfo.Count; ++index)
          {
            if (num == AvsIDCache.Attr_Name)
            {
              string name = this.productsInfo[index].Name;
              if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(str1))
              {
                valuesList[0].Values[0] = (object) str1;
                IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[index].Id, false);
                if (dbObject != null && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
                {
                  dbObject.SetAttributesValues(valuesList);
                  this.productsInfo[index].Name = str1;
                  longList.Add(this.productsInfo[index].Id);
                  intList.Add(this.ProductType);
                  arrayList1.Add((object) name);
                  arrayList2.Add((object) str1);
                }
              }
              else if (documentName != null && name != null && name.IndexOf(documentName) == 0)
              {
                string str3 = str1 + name.Remove(0, documentName.Length);
                valuesList[0].Values[0] = (object) str3;
                IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[index].Id, false);
                if (dbObject != null && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
                {
                  dbObject.SetAttributesValues(valuesList);
                  this.productsInfo[index].Name = str3;
                  longList.Add(this.productsInfo[index].Id);
                  intList.Add(this.ProductType);
                  arrayList1.Add((object) name);
                  arrayList2.Add((object) str3);
                }
              }
            }
            else if (num == AvsIDCache.Attr_Designation && !string.IsNullOrEmpty(documentDesignation))
            {
              string designation = this.productsInfo[index].Designation;
              this.UpdateProductsDesignations(this.productsInfo[index], productDesignation, this.BaseProductDesignation, sessionKeeper.Session);
              if (!this.productsInfo[index].Id.IsUndefinedId())
              {
                longList.Add(this.productsInfo[index].Id);
                intList.Add(this.ProductType);
                arrayList1.Add((object) designation);
                arrayList2.Add((object) this.productsInfo[index].Designation);
              }
            }
          }
        }
        else if (this.productsInfo.Count == 1)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[0].Id, false);
          if (dbObject != null && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          {
            dbObject.SetAttributesValues(valuesList);
            this.productsInfo[0].ProductOKPCode = str1;
            longList.Add(this.productsInfo[0].Id);
            intList.Add(this.ProductType);
            arrayList1.Add((object) str2);
            arrayList2.Add((object) str1);
          }
        }
        if (this.AvsDocumentForm == AVSDocumentForm.A)
          this.UpdateVariableDataCaptions();
        if (num == AvsIDCache.Attr_Designation && (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V))
          this.UpdateProductHeadersOnPages(true, true);
        if (AVSPlugin.NotificationService == null)
          return;
        for (int index = 0; index < longList.Count; ++index)
          AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(longList[index], intList[index], new AttributeValues(num, arrayList1[index]), new AttributeValues(num, arrayList2[index])));
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      e.Cancel = true;
    }
  }

  /// <summary>Обновить обозначение в исполнении</summary>
  /// <param name="product">Исполнение</param>
  /// <param name="oldBaseDesignation">Старое обозначение документа (основная часть обозначения исполнения)</param>
  /// <param name="newBaseDesignation">Новое обозначение документа</param>
  /// <param name="session">Пользовательская сессия для изменения атрибутов исполнений. null, если атрибуты не нужно менять</param>
  protected void UpdateProductsDesignations(
    ProductInfo product,
    string oldBaseDesignation,
    string newBaseDesignation,
    IUserSession session)
  {
    string initValue = (string) null;
    string designation = product.Designation;
    if (string.IsNullOrEmpty(designation))
      initValue = newBaseDesignation;
    else if (oldBaseDesignation != "" && designation.IndexOf(oldBaseDesignation) == 0)
      initValue = newBaseDesignation + designation.Remove(0, oldBaseDesignation.Length);
    if (initValue == null)
      return;
    if (session != null && !product.Id.IsUndefinedId())
    {
      IDBObject dbObject = session.GetObject(product.Id, false);
      if (dbObject != null && dbObject.CheckoutBy == session.UserID)
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_Designation, (object) initValue)
        };
        dbObject.SetAttributesValues(valuesList);
      }
    }
    product.Designation = initValue;
  }

  /// <summary>Обработчик события PageAdded документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void document_PageAdded(object sender, ChildNode_EventArgs e)
  {
    if (e == null || !this.IsFormB && this.AvsDocumentForm != AVSDocumentForm.V || !(e.Child is PageData child))
      return;
    this.UpdateProductHeadersOnPage(child, e.UpdateUI, e.UpdateLayout);
  }

  /// <summary>Обновить текстовые ссылки на страницы с которых начинаются исполнения больше 10</summary>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateProductPageLinks(bool updateUI, bool updateLayout)
  {
    if (this.IsFormB)
    {
      if (this.avsDocTable == null || this.avsDocTable.Page == null || this.productPageLinksTemplate == null || this.commonDataChapter == null || !(this.avsDocTable.Page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productPageLinksTemplate) is TextData templateRecursive))
        return;
      if (this.AVSDocType == AVSDocumentType.AutoIndustrySpecification)
      {
        templateRecursive.SetVisible(false, updateUI, false, false, false, false);
        templateRecursive.AssignText((string) null, false, true, false, updateUI, false);
      }
      else
      {
        templateRecursive.SetVisible(true, updateUI, false, false, false, false);
        string str1 = "";
        for (int index = 1; index < this.commonDataChapter.DocNodes.Count; ++index)
        {
          PageData page = this.commonDataChapter.DocNodes[index].Page;
          if (page != null)
          {
            PageData lastPage = page.FindLastPage();
            string attributeValue = page.GetAttributeValue(AVSDocument.ProductNumbers_PageAttribute, true);
            string str2 = index != 1 ? "                      " + attributeValue : page.Name;
            string str3 = page != lastPage ? (lastPage.PageNumber - page.PageNumber <= 1 ? $"{str2} - см. листы {page.HierarchicalPageNumber}, {lastPage.HierarchicalPageNumber}" : $"{str2} - см. листы {page.HierarchicalPageNumber}...{lastPage.HierarchicalPageNumber}") : $"{str2} - см. лист {page.HierarchicalPageNumber}";
            str1 = $"{str1}{str3}\r\n";
          }
        }
        templateRecursive.AssignText(str1, false, true, false, updateUI, false);
      }
    }
    else
    {
      if (this.AvsDocumentForm != AVSDocumentForm.V || this.avsDocTable == null || this.avsDocTable.Page == null || this.productPageLinksFormVTemplate == null || this.variableDataChapter_FormV == null)
        return;
      string str4 = this.versionAttributesHelper.VariableDataCaption + "\r\n";
      for (int index = 0; index < this.variableDataChapter_FormV.DocNodes.Count; ++index)
      {
        PageData page = this.variableDataChapter_FormV.DocNodes[index].Page;
        if (page != null)
        {
          PageData lastPage = page.FindLastPage();
          string attributeValue = page.GetAttributeValue(AVSDocument.ProductNumbers_PageAttribute, true);
          string str5 = index != 0 ? attributeValue : page.Name;
          string str6 = page != lastPage ? (lastPage.PageNumber - page.PageNumber <= 1 ? $"{str5} - см. листы {page.HierarchicalPageNumber}, {lastPage.HierarchicalPageNumber}" : $"{str5} - см. листы {page.HierarchicalPageNumber}...{lastPage.HierarchicalPageNumber}") : $"{str5} - см. лист {page.HierarchicalPageNumber}";
          str4 = $"{str4}{str6}\r\n";
        }
      }
      if (!(this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.productPageLinksFormVTemplate) is TextData nodeFromTemplate))
        return;
      nodeFromTemplate.AssignText(str4, false, true, false, updateUI, false);
    }
  }

  internal bool CheckRowForExistingDopZamen(AVSRow docRow)
  {
    bool flag = false;
    if (docRow.HasRelation)
    {
      for (int relationIndex = 0; relationIndex < docRow.Relations.Count; ++relationIndex)
      {
        if (!string.IsNullOrWhiteSpace(docRow.GetFieldStringValue(this.Attr_DopZamenText, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false)))
        {
          flag = true;
          break;
        }
      }
    }
    if (flag)
      return true;
    if (docRow.HasHiddenRelation)
    {
      for (int relationIndex = 0; relationIndex < docRow.HiddenRelations.Count; ++relationIndex)
      {
        if (!string.IsNullOrWhiteSpace(docRow.GetFieldStringValue(this.Attr_DopZamenText, relationIndex, -1, docRow.HiddenRelations, false)))
        {
          flag = true;
          break;
        }
      }
    }
    return flag;
  }

  /// <summary>Обновить заголовки исполнений на всех страницах</summary>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateProductHeadersOnPages(bool updateUI, bool updateLayout)
  {
    if (this.document == null || this.productsInfo == null)
      return;
    if (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V)
    {
      foreach (PageData page in (ImDocumentData) this.document)
        this.UpdateProductHeadersOnPage(page, updateUI, updateLayout);
      this.UpdateProductPageLinks(updateUI, updateLayout);
    }
    else if (this.AvsDocumentForm == AVSDocumentForm.A)
      this.UpdateVariableDataCaptions();
    this.UpdateProductLiteraForSP(updateUI);
  }

  /// <summary>Страница принадлежит переменным данным формы В</summary>
  /// <param name="page"></param>
  /// <returns></returns>
  private bool IsVariableDataFormVPage(PageData page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (this.avsFormB_Table != null && this.avsFormB_Table.Page != null && page.TemplateId == this.avsFormB_Table.Page.TemplateId || this.avsDocTableFormBForV_Template != null && this.avsDocTableFormBForV_Template.Page != null && page.TemplateId == this.avsDocTableFormBForV_Template.Page.Id || this.avsDocTableFormBForV_Template != null && this.avsDocTableFormBForV_Template.Page != null && page.FindFirstPage().TemplateId == this.avsDocTableFormBForV_Template.Page.Id || this.avsDocTableFormBMore10_Template != null && this.avsDocTableFormBMore10_Template.Page != null && page.TemplateId == this.avsDocTableFormBMore10_Template.Page.Id)
      return true;
    return this.avsDocTableFormBMore10_Template != null && this.avsDocTableFormBMore10_Template.Page != null && page.FindFirstPage().TemplateId == this.avsDocTableFormBMore10_Template.Page.Id;
  }

  /// <summary>Обновить заголовки исполнений на странице</summary>
  /// <param name="page">Страница</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateProductHeadersOnPage(PageData page, bool updateUI, bool updateLayout)
  {
    if (page == null || this.productsInfo == null || !this.IsFormB && (this.AvsDocumentForm != AVSDocumentForm.V || !this.IsVariableDataFormVPage(page)))
      return;
    TableData productNumberTable = this.FindProductNumberTable(page);
    TableData dataOwner = this.FindProductKodOKPTable(page);
    string str1 = (string) null;
    List<string> stringList = new List<string>();
    int firstProductIndex = this.GetFirstProductIndex(page);
    int num1 = 0;
    if (this.AvsDocumentForm == AVSDocumentForm.V)
      num1 = this.variableDataChapter_FormV == null || this.variableDataChapter_FormV.DocNode == null || this.variableDataChapter_FormV.DocNode.Page == null ? 1 : this.variableDataChapter_FormV.DocNode.Page.Index;
    int prevNumber = -1;
    if (firstProductIndex > 0 && firstProductIndex - 1 < this.productsInfo.Count)
    {
      if (!NumberParserAdvanced.TryParseInt32FromAnyText(this.productsInfo[firstProductIndex - 1].GetNumber(this.DocumentDesignation, this.UseSameDesignationForProducts), out prevNumber))
        prevNumber = -1;
      if (prevNumber == -1 && !NumberParserAdvanced.TryParseInt32FromAnyText(this.productsInfo[firstProductIndex - 1].generatedNumber, out prevNumber))
        prevNumber = -1;
    }
    if (firstProductIndex > 0 && prevNumber == -1)
      prevNumber = this.UseSameDesignationForProducts ? firstProductIndex - 1 : firstProductIndex;
    int index1 = firstProductIndex;
    int num2 = -1;
    if (dataOwner != null)
    {
      if (this.AVSDocType != AVSDocumentType.AutoIndustrySpecification)
      {
        dataOwner.SetVisible(false, updateUI, false, false, false, false);
        dataOwner = (TableData) null;
      }
      else
      {
        dataOwner.SetVisible(true, updateUI, false, false, false, false);
        num2 = dataOwner.FindDataPositionInFlow(0, out dataOwner);
      }
    }
    if (productNumberTable != null)
    {
      for (int index2 = 0; index2 < productNumberTable.Nodes.Count; ++index2)
      {
        if (productNumberTable.Nodes[index2] is TextData node4)
        {
          string templateId = node4.TemplateId;
          if (templateId != null && templateId.Contains("Номер исполнения"))
          {
            string str2;
            if (index1 < this.productsInfo.Count && index1 - firstProductIndex < this.RowProductCount)
            {
              str2 = this.productsInfo[index1].GetNumber(prevNumber, out prevNumber, this.DocumentDesignation, this.UseSameDesignationForProducts);
              this.productsInfo[index1].generatedNumber = str2;
              stringList.Add(str2);
              if (page.PrevPage == null)
                node4.SetAttributeValue(AVSDocument.ProductGuid_CellAttribute, this.productsInfo[index1].Guid.ToString(), false, false, false);
            }
            else
            {
              str2 = "";
              node4.RemoveAttribute(AVSDocument.ProductGuid_CellAttribute, false, false);
            }
            node4.AssignText(str2, false, true, false, false, false);
            if (num2 != -1 && dataOwner != null && index2 + num2 < dataOwner.Nodes.Count && dataOwner.Nodes[index2 + num2] is TableData node)
            {
              if (node.Nodes.Count > 0 && node.Nodes[0] is TextData node1)
                node1.AssignText(str2, false, true, false, false, false);
              if (node.Nodes.Count > 1 && node.Nodes[1] is TextData node2)
              {
                if (index1 < this.productsInfo.Count)
                  node2.AssignText(this.productsInfo[index1].Designation, false, true, false, false, false);
                else
                  node2.AssignText("", false, true, false, false, false);
              }
              if (node.Nodes.Count > 2 && node.Nodes[2] is TextData node3)
              {
                if (index1 < this.productsInfo.Count)
                  node3.AssignText(this.productsInfo[index1].ProductOKPCode, false, true, false, false, false);
                else
                  node3.AssignText("", false, true, false, false, false);
              }
            }
            if (index2 == 0)
              str1 = str2;
            else
              string.IsNullOrEmpty(str2);
            ++index1;
          }
        }
      }
    }
    if (!stringList.IsEmpty<string>())
    {
      string attributeValue;
      string str3;
      if (page.Index > num1)
      {
        if (stringList.Count > 1)
        {
          attributeValue = ProductInfo.EnumerateNumbersToText((IList<string>) stringList, out bool _);
          str3 = "Исполнения " + attributeValue;
        }
        else
        {
          attributeValue = $"{str1}";
          str3 = $"Исполнение {str1}";
        }
      }
      else
      {
        if (stringList.Count > 1)
        {
          bool isSimpleRange;
          ProductInfo.EnumerateNumbersToText(stringList[0] == "-" || string.IsNullOrEmpty(stringList[0]) ? (IList<string>) stringList.GetRange(1, stringList.Count - 1) : (IList<string>) stringList, out isSimpleRange);
          attributeValue = !isSimpleRange ? ProductInfo.EnumerateNumbersToText((IList<string>) stringList, out isSimpleRange) : $"до {stringList.Last<string>()}";
        }
        else
          attributeValue = this.productsInfo[0].Designation;
        str3 = attributeValue;
      }
      page.SetName(str3, false, false);
      page.SetAttributeValue(AVSDocument.ProductNumbers_PageAttribute, attributeValue, false, false, false);
    }
    TableData kodAndLiteraTable = this.FindProductKodAndLiteraTable(page);
    if (kodAndLiteraTable != null)
    {
      int index3 = 0;
      for (int count = kodAndLiteraTable.Nodes.Count; index3 < count; ++index3)
      {
        if (kodAndLiteraTable.Nodes[index3] is TableData node5 && node5.IsRow && node5.Nodes.Count > 0 && node5.Nodes[0] is TextData node6)
        {
          int index4 = firstProductIndex;
          if (node6.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
          {
            if (index4 < this.productsInfo.Count)
            {
              if (referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
              {
                referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, DBHelper.GetAttributeTypeIDFromAttributeGuid(referenceToTextSource.AttributeGuid), referenceToTextSource.AttributeName);
                referenceToTextSource.AssignReferenceType(RefToDBObjectType.rtSelectedObject);
              }
              if (referenceToTextSource.DBObjectInfo == null)
                referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(), true);
              referenceToTextSource.DBObjectInfo.SetDBObjectInfo(this.productsInfo[index4].Guid, this.productsInfo[index4].Id, this.ProductType, $"{this.productsInfo[index4].Name}({this.productsInfo[index4].Designation})");
            }
            if (referenceToTextSource.AttributeID != -1)
            {
              for (int index5 = 0; index5 < node5.Nodes.Count && index5 < this.RowProductCount; ++index5)
              {
                if (node5.Nodes[index5] is TextData node)
                {
                  ReferenceToDBObjectAttribute dbObjectAttribute = node.ReferenceToTextSource as ReferenceToDBObjectAttribute;
                  string str4 = "";
                  if (index4 < this.productsInfo.Count && index4 - firstProductIndex < this.RowProductCount)
                  {
                    if (referenceToTextSource.AttributeID == AvsIDCache.Attr_Litera)
                    {
                      if (!ProductVariableDataChapter.SameLiters(this))
                        str4 = this.productsInfo[index4].Litera;
                    }
                    else
                      str4 = this.productsInfo[index4].GetAttributeValue(referenceToTextSource.AttributeID);
                    if (index5 > 0)
                    {
                      if (dbObjectAttribute == null)
                      {
                        dbObjectAttribute = (ReferenceToDBObjectAttribute) referenceToTextSource.Clone();
                        node.AssignReferenceToTextSource((ReferenceBase) dbObjectAttribute, true, false, false);
                      }
                      dbObjectAttribute.AssignAttributeInfo(referenceToTextSource.AttributeGuid, referenceToTextSource.AttributeID, referenceToTextSource.AttributeName);
                      dbObjectAttribute.AssignReferenceType(RefToDBObjectType.rtSelectedObject);
                      if (dbObjectAttribute.DBObjectInfo == null)
                        dbObjectAttribute.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(), true);
                      dbObjectAttribute.DBObjectInfo.SetDBObjectInfo(this.productsInfo[index4].Guid, this.productsInfo[index4].Id, this.ProductType, $"{this.productsInfo[index4].Name}({this.productsInfo[index4].Designation})");
                    }
                  }
                  else
                    node.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
                  node.AssignText(str4, false, true, false, false, false);
                  ++index4;
                }
              }
            }
          }
        }
      }
    }
    if (page.FindFirstNodeByName("Шапка с обозначением") is TextData firstNodeByName)
    {
      if (this.UseSameDesignationForProducts)
        firstNodeByName.AssignText($"{this.productNumbersTitle} {this.DocumentDesignation}-", false, true, false, false, false);
      else
        firstNodeByName.AssignText($"{this.productNumbersTitle} {this.DocumentDesignation}", false, true, false, false, false);
    }
    if (!updateUI)
      return;
    page.RefreshUI();
  }

  /// <summary>Вспомогательное свойство для внутреннего использования. Ячейка с ОКП для левого исполнения в зеркальной СП</summary>
  [Browsable(false)]
  internal TextData LeftProductKodOKP
  {
    get => this.leftProductKodOKP;
    set
    {
      if (this.leftProductKodOKP == value)
        return;
      if (this.leftProductKodOKP != null)
      {
        this.leftProductKodOKP.TextValidating -= new TextValidating_EventHandler(this.docStamp_TextValidating);
        this.leftProductKodOKP.TextChanged -= new TextChanged_EventHandler(this.docStamp_TextChanged);
      }
      this.leftProductKodOKP = value;
      if (this.leftProductKodOKP == null)
        return;
      this.leftProductKodOKP.TextValidating += new TextValidating_EventHandler(this.docStamp_TextValidating);
      this.leftProductKodOKP.TextChanged += new TextChanged_EventHandler(this.docStamp_TextChanged);
    }
  }

  /// <summary>Вспомогательное свойство для внутреннего использования. Ячейка с ОКП для правого исполнения в зеркальной СП</summary>
  [Browsable(false)]
  internal TextData RightProductKodOKP
  {
    get => this.rightProductKodOKP;
    set
    {
      if (this.rightProductKodOKP == value)
        return;
      if (this.rightProductKodOKP != null)
      {
        this.rightProductKodOKP.TextValidating -= new TextValidating_EventHandler(this.docStamp_TextValidating);
        this.rightProductKodOKP.TextChanged -= new TextChanged_EventHandler(this.docStamp_TextChanged);
      }
      this.rightProductKodOKP = value;
      if (this.rightProductKodOKP == null)
        return;
      this.rightProductKodOKP.TextValidating += new TextValidating_EventHandler(this.docStamp_TextValidating);
      this.rightProductKodOKP.TextChanged += new TextChanged_EventHandler(this.docStamp_TextChanged);
    }
  }

  /// <summary>Вспомогательное свойство для внутреннего использования. Ячейка с обозначением для левого исполнения в зеркальной СП</summary>
  [Browsable(false)]
  internal TextData LeftProductDesignation
  {
    get => this.leftProductDesignation;
    set
    {
      if (this.leftProductDesignation == value)
        return;
      if (this.leftProductDesignation != null)
      {
        this.leftProductDesignation.TextValidating -= new TextValidating_EventHandler(this.docStamp_TextValidating);
        this.leftProductDesignation.TextChanged -= new TextChanged_EventHandler(this.docStamp_TextChanged);
      }
      this.leftProductDesignation = value;
      if (this.leftProductDesignation == null)
        return;
      this.leftProductDesignation.TextValidating += new TextValidating_EventHandler(this.docStamp_TextValidating);
      this.leftProductDesignation.TextChanged += new TextChanged_EventHandler(this.docStamp_TextChanged);
    }
  }

  /// <summary>Вспомогательное свойство для внутреннего использование. Ячейка с обозначением для правого исполнения в зеркальной СП</summary>
  [Browsable(false)]
  internal TextData RightProductDesignation
  {
    get => this.rightProductDesignation;
    set
    {
      if (this.rightProductDesignation == value)
        return;
      if (this.rightProductDesignation != null)
      {
        this.rightProductDesignation.TextValidating -= new TextValidating_EventHandler(this.docStamp_TextValidating);
        this.rightProductDesignation.TextChanged -= new TextChanged_EventHandler(this.docStamp_TextChanged);
      }
      this.rightProductDesignation = value;
      if (this.rightProductDesignation == null)
        return;
      this.rightProductDesignation.TextValidating += new TextValidating_EventHandler(this.docStamp_TextValidating);
      this.rightProductDesignation.TextChanged += new TextChanged_EventHandler(this.docStamp_TextChanged);
    }
  }

  /// <summary>Обновить поля в Основной надписи для зеркальной СП</summary>
  private void UpdateProductsInStampForMirrorSP()
  {
    if (this.document == null || this.productsInfo == null || this.productsInfo.Count < 2)
      return;
    int attributeID = -1;
    dbObjectAttribute = (ReferenceToDBObjectAttribute) null;
    if (this.rightProductKodOKPTemplate != null)
    {
      this.RightProductKodOKP = this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.rightProductKodOKPTemplate) as TextData;
      if (this.RightProductKodOKP != null && this.RightProductKodOKP.ReferenceToTextSource is ReferenceToDBObjectAttribute dbObjectAttribute)
      {
        attributeID = dbObjectAttribute.GetOrUpdateAttributeID();
        if (attributeID != -1)
        {
          dbObjectAttribute.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[1].Guid, this.productsInfo[1].Id), true);
          this.RightProductKodOKP.AssignText(this.productsInfo[1].GetAttributeValue(attributeID), false, true, false, false, false);
        }
      }
    }
    if (this.leftProductKodOKPTemplate != null && attributeID != -1 && dbObjectAttribute != null)
    {
      this.LeftProductKodOKP = this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.leftProductKodOKPTemplate) as TextData;
      if (this.LeftProductKodOKP != null)
      {
        if (this.LeftProductKodOKP.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
        {
          referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), true);
          referenceToTextSource.AssignAttributeInfo(dbObjectAttribute.AttributeGuid, dbObjectAttribute.AttributeID, dbObjectAttribute.AttributeName);
        }
        else
          this.LeftProductKodOKP.AssignReferenceToTextSource((ReferenceBase) new ReferenceToDBObjectAttribute((DocumentTreeNode) this.LeftProductKodOKP, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), dbObjectAttribute.AttributeGuid, dbObjectAttribute.AttributeID, dbObjectAttribute.AttributeName, true), true, false, false);
        this.LeftProductKodOKP.AssignText(this.productsInfo[0].GetAttributeValue(attributeID), false, true, false, false, false);
      }
    }
    if (this.rightProductDesignationTemplate != null)
    {
      this.RightProductDesignation = this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.rightProductDesignationTemplate) as TextData;
      if (this.RightProductDesignation != null && this.RightProductDesignation.ReferenceToTextSource is ReferenceToDBObjectAttribute dbObjectAttribute)
      {
        attributeID = dbObjectAttribute.GetOrUpdateAttributeID();
        if (attributeID != -1)
        {
          dbObjectAttribute.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[1].Guid, this.productsInfo[1].Id), true);
          this.RightProductDesignation.AssignText(this.productsInfo[1].GetAttributeValue(attributeID), false, true, false, false, false);
        }
      }
    }
    if (this.leftProductDesignationTemplate == null || attributeID == -1 || dbObjectAttribute == null)
      return;
    this.LeftProductDesignation = this.document.FindFirstNodeFromTemplate((DocumentTreeNode) this.leftProductDesignationTemplate) as TextData;
    if (this.LeftProductDesignation == null)
      return;
    if (this.LeftProductDesignation.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource1)
    {
      referenceToTextSource1.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), true);
      referenceToTextSource1.AssignAttributeInfo(dbObjectAttribute.AttributeGuid, dbObjectAttribute.AttributeID, dbObjectAttribute.AttributeName);
    }
    else
      this.LeftProductDesignation.AssignReferenceToTextSource((ReferenceBase) new ReferenceToDBObjectAttribute((DocumentTreeNode) this.LeftProductDesignation, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), dbObjectAttribute.AttributeGuid, dbObjectAttribute.AttributeID, dbObjectAttribute.AttributeName, true), true, false, false);
    this.LeftProductDesignation.AssignText(this.productsInfo[0].GetAttributeValue(attributeID), false, true, false, false, false);
  }

  /// <summary>Обработчик события изменения текста в ячейке штампа для зеркальных СП</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void docStamp_TextChanged(object sender, TextChanged_EventArgs e)
  {
    TextData textData = sender as TextData;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    int attributeID = -1;
    if (this.leftProductKodOKP != null && textData == this.leftProductKodOKP && this.productsInfo.Count > 0)
    {
      if (this.leftProductKodOKP.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
      {
        attributeID = referenceToTextSource.GetOrUpdateAttributeID();
        if (attributeID != -1)
          referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), true);
      }
      if (attributeID != -1)
      {
        string attributeValue = this.productsInfo[0].GetAttributeValue(attributeID);
        if (attributeValue != e.NewText)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[0].Id, false);
            if (dbObject != null)
            {
              if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
              {
                dbObject.SetAttributesValues(new AttributeValues[1]
                {
                  new AttributeValues(attributeID, (object) e.NewText)
                });
                this.productsInfo[0].SetAttributeValue(attributeID, (object) e.NewText, false);
                longList.Add(this.productsInfo[0].Id);
                intList.Add(this.ProductType);
                arrayList1.Add((object) attributeValue);
                arrayList2.Add((object) e.NewText);
              }
            }
          }
        }
      }
    }
    if (this.rightProductKodOKP != null && textData == this.rightProductKodOKP && this.productsInfo.Count > 1)
    {
      if (this.rightProductKodOKP.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
      {
        attributeID = referenceToTextSource.GetOrUpdateAttributeID();
        if (attributeID != -1)
          referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[1].Guid, this.productsInfo[1].Id), true);
      }
      if (attributeID != -1)
      {
        string attributeValue = this.productsInfo[1].GetAttributeValue(attributeID);
        if (attributeValue != e.NewText)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[1].Id, false);
            if (dbObject != null)
            {
              if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
              {
                dbObject.SetAttributesValues(new AttributeValues[1]
                {
                  new AttributeValues(attributeID, (object) e.NewText)
                });
                this.productsInfo[1].SetAttributeValue(attributeID, (object) e.NewText, false);
                longList.Add(this.productsInfo[1].Id);
                intList.Add(this.ProductType);
                arrayList1.Add((object) attributeValue);
                arrayList2.Add((object) e.NewText);
              }
            }
          }
        }
      }
    }
    if (this.leftProductDesignation != null && textData == this.leftProductDesignation && this.productsInfo.Count > 0)
    {
      if (this.leftProductDesignation.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
      {
        attributeID = referenceToTextSource.GetOrUpdateAttributeID();
        if (attributeID != -1)
          referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[0].Guid, this.productsInfo[0].Id), true);
      }
      if (attributeID != -1)
      {
        string attributeValue = this.productsInfo[0].GetAttributeValue(attributeID);
        if (attributeValue != e.NewText)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[0].Id, false);
            if (dbObject != null)
            {
              if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
              {
                dbObject.SetAttributesValues(new AttributeValues[1]
                {
                  new AttributeValues(attributeID, (object) e.NewText)
                });
                this.productsInfo[0].SetAttributeValue(attributeID, (object) e.NewText, false);
                longList.Add(this.productsInfo[0].Id);
                intList.Add(this.ProductType);
                arrayList1.Add((object) attributeValue);
                arrayList2.Add((object) e.NewText);
              }
            }
          }
        }
      }
    }
    if (this.rightProductDesignation != null && textData == this.rightProductDesignation && this.productsInfo.Count > 1)
    {
      if (this.rightProductDesignation.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
      {
        attributeID = referenceToTextSource.GetOrUpdateAttributeID();
        if (attributeID != -1)
          referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(this.productsInfo[1].Guid, this.productsInfo[1].Id), true);
      }
      if (attributeID != -1)
      {
        string attributeValue = this.productsInfo[1].GetAttributeValue(attributeID);
        if (attributeValue != e.NewText)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[1].Id, false);
            if (dbObject != null)
            {
              if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
              {
                dbObject.SetAttributesValues(new AttributeValues[1]
                {
                  new AttributeValues(attributeID, (object) e.NewText)
                });
                this.productsInfo[1].SetAttributeValue(attributeID, (object) e.NewText, false);
                longList.Add(this.productsInfo[1].Id);
                intList.Add(this.ProductType);
                arrayList1.Add((object) attributeValue);
                arrayList2.Add((object) e.NewText);
              }
            }
          }
        }
      }
    }
    if (attributeID == -1 || AVSPlugin.NotificationService == null)
      return;
    for (int index = 0; index < longList.Count; ++index)
      AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(longList[index], intList[index], new AttributeValues(attributeID, arrayList1[index]), new AttributeValues(attributeID, arrayList2[index])));
  }

  /// <summary>Обработчик события проверки текста в ячейке штампа для зеркальных СП</summary>
  private void docStamp_TextValidating(object sender, TextValidating_EventArgs e)
  {
  }

  /// <summary>Получить ячейку с литерой из основной надписи</summary>
  public TextData GetLiteraCellFromTitleBlock()
  {
    return AVSDocument.GetLiteraCellFromTitleBlock((ImDocumentData) this.Document);
  }

  /// <summary>Получить ячейку с литерой из основной надписи</summary>
  public static TextData GetLiteraCellFromTitleBlock(ImDocumentData document)
  {
    if (document == null)
      return (TextData) null;
    if (document.IsTemplate)
    {
      if (!(document.FindNode("Литера") is TextData cellFromTitleBlock2))
        cellFromTitleBlock2 = document.FindNode("118") as TextData;
    }
    else if (!(document.FindFirstNodeFromTemplate("Литера") is TextData cellFromTitleBlock2))
      cellFromTitleBlock2 = document.FindFirstNodeFromTemplate("118") as TextData;
    return cellFromTitleBlock2;
  }

  /// <summary>Обновить литеру изделия для СП в основной надписи</summary>
  public void UpdateProductLiteraForSP(bool updateUI)
  {
    TextData cellFromTitleBlock = AVSDocument.GetLiteraCellFromTitleBlock((ImDocumentData) this.Document);
    if (cellFromTitleBlock == null)
      return;
    string str = "";
    if (this.AvsDocumentForm == AVSDocumentForm.Single)
    {
      if (this.productsInfo != null && this.productsInfo.Count > 0)
        str = this.productsInfo[0].Litera ?? "";
    }
    else
    {
      cellFromTitleBlock.ReadOnly = false;
      str = "-";
      if (ProductVariableDataChapter.SameLiters(this) && this.productsInfo != null && this.productsInfo.Count > 0)
        str = this.productsInfo[0].Litera ?? "";
    }
    cellFromTitleBlock.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
    cellFromTitleBlock.AssignText(str, false, true, false, updateUI, updateUI);
  }

  /// <summary>Основной контрол документа</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get
    {
      return this.avsWindow != null ? this.avsWindow.DocumentControl : (DocumentControl) null;
    }
  }

  /// <summary>Идентификатор шаблона спецификации</summary>
  public long AVSDocumentTemplateID
  {
    [DebuggerStepThrough] get => this.DocumentTemplateID;
  }

  /// <summary>Загрузить шаблон конструкторского документа</summary>
  /// <param name="docTypeGuid">Внутренний Guid типа конструкторского документа</param>
  /// <param name="specForm">Форма конструкторского документа</param>
  /// <param name="failIfNotFound">Выдать исключение, если шаблон не найден</param>
  /// <returns>Шаблон спецификации</returns>
  public ImDocument UpdateDocumentTemplateIfOriginalChanged(
    Guid docTypeGuid,
    AVSDocumentForm specForm,
    bool failIfNotFound)
  {
    Guid templateGuid1 = Guid.Empty;
    long num;
    if (this.DocumentTemplateID.IsDefinedId())
    {
      num = this.DocumentTemplateID;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        num = AVSDocumentsSettings.Instance.GetTemplate(docTypeGuid, new AVSDocumentForm?(specForm), out templateGuid1, sessionKeeper.Session, failIfNotFound);
    }
    ImDocument template = DocumentEditorPlugin.LoadDocumentFromDBObject(num, updateDoc: false);
    if (template != null)
    {
      Guid templateGuid2 = templateGuid1 == Guid.Empty ? template.DBObjectGuid : templateGuid1;
      if (this.document == null || this.document.DocumentTemplate.Revision != template.Revision)
        this.ReplaceTemplate(ref template, templateGuid2, num, docTypeGuid, specForm, failIfNotFound);
    }
    else if (failIfNotFound)
      throw new Exception($"Шаблон конструкторского документа {{ID:{num.ToString()}}} поврежден!");
    return template;
  }

  /// <summary>Загрузить шаблон конструкторского документа</summary>
  /// <param name="docTypeGuid">Внутренний Guid типа конструкторского документа</param>
  /// <param name="specForm">Форма конструкторского документа</param>
  /// <param name="failIfNotFound">Выдать исключение, если шаблон не найден</param>
  /// <returns>Шаблон спецификации</returns>
  public ImDocument LoadStdTemplate(
    Guid docTypeGuid,
    AVSDocumentForm specForm,
    bool failIfNotFound)
  {
    Guid templateGuid = Guid.Empty;
    long num;
    if (this.DocumentTemplateID.IsDefinedId())
    {
      num = this.DocumentTemplateID;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        num = AVSDocumentsSettings.Instance.GetTemplate(docTypeGuid, new AVSDocumentForm?(specForm), out templateGuid, sessionKeeper.Session, failIfNotFound);
    }
    ImDocument template = DocumentEditorPlugin.LoadDocumentFromDBObject(num, updateDoc: false);
    if (template != null)
      this.ReplaceTemplate(ref template, templateGuid, num, docTypeGuid, specForm, failIfNotFound);
    else if (failIfNotFound)
      throw new Exception($"Шаблон конструкторского документа {{ID:{num.ToString()}}} поврежден!");
    return template;
  }

  private void ReplaceTemplate(
    ref ImDocument template,
    Guid templateGuid,
    long templateId,
    Guid docTypeGuid,
    AVSDocumentForm specForm,
    bool failIfNotFound)
  {
    this.DocumentTemplateID = templateId;
    this.documentTemplateGuid = templateGuid;
    this.ResetSettingsFromTemplate();
    if (this.document == null)
    {
      this.Document = new ImDocument(template, true, true);
      template.Dispose();
      template = this.Document.Template as ImDocument;
    }
    else
    {
      this.templateUpdated = true;
      bool isDocumentLoading = this.document.IsDocumentLoading;
      this.document.IsDocumentLoading = true;
      this.document.AssignDocumentTemplate((ImDocumentData) template, true, false, true);
      this.document.ApplyTemplateProperties(false, false);
      this.document.CreateFirstPage();
      this.document.IsDocumentLoading = isDocumentLoading;
      this.documentIsModifiedByLoad = true;
    }
    this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, specForm.ToString(), false, false, false);
    this.document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
    this.document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
    this.document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, docTypeGuid.ToString(), false, false, false);
    if (this.documentTemplateGuid != Guid.Empty)
      this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
    else
      this.document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
    DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) this.document, this.DocumentGuid, this.DocumentID, this.DocumentDBObjectType, this.DocumentCaption);
    DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, true, true, false, false, false);
    string allTemplates = this.FindAllTemplates((ImDocumentData) template, failIfNotFound);
    if (allTemplates == null || !(allTemplates != ""))
      return;
    template = (ImDocument) null;
  }

  /// <summary>Загрузить атрибуты документа</summary>
  /// <param name="objectId">Идентификатор объекта спецификации</param>
  /// <param name="updateLinks">Обновить ссылки в документе</param>
  public void GetDocumentAttributes(long objectId, bool updateLinks)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject == null)
        return;
      this.GetDocumentAttributes(dbObject, false);
      if (!updateLinks || this.Document == null)
        return;
      DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.Document, sessionKeeper.Session, true, true, false, false, false);
    }
  }

  /// <summary>Загрузить атрибуты документа</summary>
  /// <param name="dbObject">IDBObject объекта спецификации</param>
  /// <param name="updateLinks">Обновить ссылки в документе</param>
  public void GetDocumentAttributes(IDBObject dbObject, bool updateLinks)
  {
    AttributeValues[] attributeValuesArray = dbObject != null ? dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions) : throw new ArgumentNullException(nameof (dbObject));
    bool flag = MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, AvsIDCache.ObjType_Document);
    if (flag)
      this.DocumentDBObjectType = dbObject.ObjectType;
    for (int index = 0; index < attributeValuesArray.Length; ++index)
    {
      if (attributeValuesArray[index].AttributeID == -12)
      {
        if (flag)
          this.DocumentGuid = new Guid(Convert.ToString(attributeValuesArray[index].Values[0]));
      }
      else if (attributeValuesArray[index].AttributeID == -50)
        this.DocumentCaption = Convert.ToString(attributeValuesArray[index].Values[0]);
      else if (attributeValuesArray[index].AttributeID == AvsIDCache.Attr_Designation)
        this.DocumentDesignation = Convert.ToString(attributeValuesArray[index].Values[0]);
      else if (attributeValuesArray[index].AttributeID == AvsIDCache.Attr_Name)
        this.DocumentName = Convert.ToString(attributeValuesArray[index].Values[0]);
    }
    if (!(flag & updateLinks))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.Document, sessionKeeper.Session, true, true, false, false, false);
  }

  /// <summary>Получить поля и атрибуты записи документа</summary>
  public List<AvsRowAttributeInfo> GetDocRowFieldsAndAttributes()
  {
    List<AvsRowAttributeInfo> fieldsAndAttributes = new List<AvsRowAttributeInfo>((IEnumerable<AvsRowAttributeInfo>) this.docRowFields);
    if (this.AvsDocumentForm == AVSDocumentForm.V && this.docRowFields_VarFormV != null)
    {
      for (int index1 = 0; index1 < this.docRowFields_VarFormV.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.docRowFields.Count; ++index2)
        {
          if (!this.docRowFields[index2].Equals((AttributeInfo) this.docRowFields_VarFormV[index1]))
            fieldsAndAttributes.Add(this.docRowFields_VarFormV[index1]);
        }
      }
    }
    if (this.IsExportSP)
    {
      for (int index = 0; index < this.docRowFields_Exp.Count; ++index)
      {
        if (this.docRowFields_Exp[index] != null)
          fieldsAndAttributes.Add(this.docRowFields_Exp[index]);
      }
    }
    if (this.docRowAttributes != null && this.docRowAttributes.Count > 0)
      fieldsAndAttributes.AddRange((IEnumerable<AvsRowAttributeInfo>) this.docRowAttributes);
    return fieldsAndAttributes;
  }

  /// <summary>Вспомогательный метод. Кэширует SpecRowInfo</summary>
  private void CreateRowAttrsInfo()
  {
    this.Attr_SortIndex = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00202-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_SortIndex, "Сортировка", ColumnContents.Text);
    this.Field_Format = AvsIDCache.StdField_Format.Clone();
    this.Field_Zone = AvsIDCache.StdField_Zone.Clone();
    this.Field_Position = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00270-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Position, AVSRow.DocAttr_Position, ColumnContents.Text);
    this.Field_Name = AvsIDCache.StdField_Name.Clone();
    this.Field_Designation = new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Designation, AVSRow.DocAttr_Designation, ColumnContents.Text);
    this.Field_Count = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_Count_Guid, AvsIDCache.Attr_Count, AVSRow.DocAttr_Count, ColumnContents.Text);
    this.Attr_CountForAdjustment = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad007a6-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_CountForAdjustment, AVSRow.DocAttr_CountForAdjustment, ColumnContents.Text);
    this.Attr_Podbor = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_Podbor_Guid, AvsIDCache.Attr_Podbor, "Подбор", ColumnContents.Text);
    this.Attr_PodborForPosDesignation = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributePodborForPosDesignation, AvsIDCache.Attr_PodborForPosDesignation, "Подбор для позиционного обозначения", ColumnContents.Text);
    this.Attr_IncludeInElementList = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeIncludeInElementList, AvsIDCache.Attr_IncludeInElementList, "Элемент перечня элементов", ColumnContents.Text);
    this.Attr_HideInSpecification = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeHideInSpecification, AvsIDCache.Attr_HideInSpecification, "Не отображать в спецификации", ColumnContents.Text);
    this.Attr_NominalValue = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.Attr_NominalValue_Guid, AvsIDCache.Attr_NominalValue, "Значение номинала", ColumnContents.Text);
    this.Attr_LimitValues = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeLimitValues, AvsIDCache.Attr_LimitValues, "Предельные значения", ColumnContents.Text);
    this.Field_Note = this.Attr_Note;
    this.Attr_InMainDocComplect = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cadd9bdc-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_InMainDocComplect, MetaDataHelper.GetAttributeTypeName(AvsIDCache.Attr_InMainDocComplect), ColumnContents.Text);
    this.Field_PosDesignation = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad01478-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_PosDesignation, MetaDataHelper.GetAttributeTypeName(AvsIDCache.Attr_PosDesignation), ColumnContents.Text);
    this.Attr_FunctionalGroupPosDesignation = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeFGPosDesignation, AvsIDCache.Attr_FGPosDesignation, AVSRow.DocAttr_FGPosDesignation, ColumnContents.Text);
    this.Attr_FunctionalGroupDesignation = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeFGDesignation, AvsIDCache.Attr_FGDesignation, AVSRow.DocAttr_FGDesignation, ColumnContents.Text);
    this.Attr_FunctionalGroupName = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeFGName, AvsIDCache.Attr_FGName, AVSRow.DocAttr_FGName, ColumnContents.Text);
    this.Field_Name_Exp = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrNameExpGuid, AvsIDCache.Attr_NameExp, "Наименование (exp)", ColumnContents.Text);
    this.Field_Description = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrDescriptionGuid, AvsIDCache.Attr_Description, "Description", ColumnContents.Text);
    this.Attr_DopZamenGroupNum = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad001c0-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_DopZamenGroupNum, "Номер группы заменителей", ColumnContents.Text);
    this.Attr_DopZamenNumInGroup = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad001c1-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_DopZamenNumInGroup, "Номер заменителя в группе", ColumnContents.Text);
    this.Attr_DesignerActualVariant = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00654-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_DesignerActualVariant, "Конструкторский основной вариант", ColumnContents.Text);
    this.Attr_DopZamenText = AvsIDCache.DopZamenTextAttrInfo.Clone();
    this.Attr_Section = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00266-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_SpecificationSection, "Раздел спецификации", ColumnContents.ID);
    this.Attr_SearchId = AvsIDCache.Attr_SearchId.Clone();
    this.Attr_AdditionalChapter = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.AttrSpecificationChapterGuid, AvsIDCache.Attr_SpecificationСhapter, "Часть спецификации", ColumnContents.ID);
    this.Attr_SubstitutePositionType = new AvsRowAttributeInfo(FieldSource.Relation, SubstitutesConstants.SubstitutePositionTypeAttributeTypeGuid, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, "Тип вспомогательной позиции", ColumnContents.Text);
    this.Attr_SubstitutePositionNumber = new AvsRowAttributeInfo(FieldSource.Relation, SubstitutesConstants.PositionNumberAttributeTypeGuid, SubstitutesConstants.PositionNumberAttributeTypeID, "Номер позиции заменителя", ColumnContents.Text);
    this.Attr_Class = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrClassGuid, AvsIDCache.Attr_Class, "Класс", ColumnContents.Text);
    this.Attr_GOST = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrGostGuid, AvsIDCache.Attr_Gost, "ГОСТ", ColumnContents.Text);
    this.Attr_SizeAndParams = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrRazmery_I_ParametryGuid, AvsIDCache.Attr_Razmery_I_Parametry, "Размеры и параметры", ColumnContents.Text);
    this.Attr_GroupWithoutClass = new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrGroupWithoutClassGuid, AvsIDCache.Attr_GroupWithoutClass, "Группировать только по значению Размеры и параметры", ColumnContents.Text, new FieldTypes?(FieldTypes.ftBoolean));
  }

  /// <summary>Получить информацию об атрибутах в наименовании из настроек</summary>
  internal void GetUserAttributesForFieldNameFromSettings()
  {
    this.Attr_UserAttributeForNameField = !(this.AVSCommonPropertiesSchema.UserAttributeForNameField != Guid.Empty) ? new AvsRowAttributeInfo(false, -1) : new AvsRowAttributeInfo(FieldSource.Object, this.AVSCommonPropertiesSchema.UserAttributeForNameField, MetaDataHelper.GetAttributeTypeID(this.AVSCommonPropertiesSchema.UserAttributeForNameField), MetaDataHelper.GetAttributeTypeName(this.AVSCommonPropertiesSchema.UserAttributeForNameField), ColumnContents.Text);
    if (this.AVSCommonPropertiesSchema.UserAttributeForDocTypeName != Guid.Empty)
      this.Attr_UserAttributeForDocType = new AvsRowAttributeInfo(FieldSource.Object, this.AVSCommonPropertiesSchema.UserAttributeForDocTypeName, MetaDataHelper.GetAttributeTypeID(this.AVSCommonPropertiesSchema.UserAttributeForDocTypeName), MetaDataHelper.GetAttributeTypeName(this.AVSCommonPropertiesSchema.UserAttributeForDocTypeName), ColumnContents.Text);
    else
      this.Attr_UserAttributeForDocType = new AvsRowAttributeInfo(false, -1);
  }

  /// <summary>Обновить информацию об атрибутах в наименовании</summary>
  /// <param name="needUpdateDocNames">Необходимо обновить наименования в документах</param>
  internal void UpdateUserAttributesForFieldName(bool needUpdateDocNames)
  {
    AvsRowAttributeInfo attributeForNameField = this.Attr_UserAttributeForNameField;
    AvsRowAttributeInfo attributeForDocType = this.Attr_UserAttributeForDocType;
    this.GetUserAttributesForFieldNameFromSettings();
    bool flag = needUpdateDocNames;
    List<AvsRowAttributeInfo> newNameAttrs = new List<AvsRowAttributeInfo>();
    if (attributeForNameField.AttributeGuid != this.Attr_UserAttributeForNameField.AttributeGuid)
    {
      flag = true;
      if (this.Attr_UserAttributeForNameField.AttributeGuid != Guid.Empty && !this.prjObjectAttrMap.AttributeDictionary.ContainsKey(this.Attr_UserAttributeForNameField.AttributeId))
        newNameAttrs.Add(this.Attr_UserAttributeForNameField);
    }
    if (attributeForDocType.AttributeGuid != this.Attr_UserAttributeForDocType.AttributeGuid)
    {
      flag = true;
      if (this.Attr_UserAttributeForDocType.AttributeGuid != Guid.Empty && !this.prjObjectAttrMap.AttributeDictionary.ContainsKey(this.Attr_UserAttributeForDocType.AttributeId))
        newNameAttrs.Add(this.Attr_UserAttributeForDocType);
    }
    if (!flag)
      return;
    this.UpdateNameDocCells(newNameAttrs, true);
  }

  /// <summary>Идентификатор версии документа</summary>
  public long DocumentID
  {
    [DebuggerStepThrough] get => this.documentId;
    set
    {
      if (this.documentId == value)
        return;
      this.documentId = value;
      if (this.avsWindow == null)
        return;
      this.avsWindow.DocumentID = value;
    }
  }

  /// <summary>Версионно-независимый идентификатор документа</summary>
  public long DocFID { get; set; } = -1;

  /// <summary>Guid объекта документа</summary>
  public Guid DocumentGuid
  {
    [DebuggerStepThrough] get => this.documentGuid;
    set
    {
      if (!(this.documentGuid != value))
        return;
      this.documentGuid = value;
      if (this.avsWindow == null)
        return;
      this.avsWindow.DocumentGuid = value;
    }
  }

  /// <summary>Тип объекта документа</summary>
  public int DocumentDBObjectType
  {
    [DebuggerStepThrough] get => this.documentType;
    set
    {
      if (this.documentType == value)
        return;
      this.documentType = value;
      if (this.avsWindow == null)
        return;
      this.avsWindow.DocumentType = value;
    }
  }

  /// <summary>Наименование документа</summary>
  public string DocumentName
  {
    [DebuggerStepThrough] get => this.documentName;
    set
    {
      if (!(this.documentName != value))
        return;
      this.documentName = value;
      if (this.avsWindow == null)
        return;
      this.avsWindow.DocumentName = value;
    }
  }

  /// <summary>Заголовок документа</summary>
  public string DocumentCaption
  {
    [DebuggerStepThrough] get
    {
      return string.IsNullOrEmpty(this.documentCaption) ? nameof (AVSDocument) : this.documentCaption;
    }
    set
    {
      if (!(this.documentCaption != value))
        return;
      this.documentCaption = value;
      if (this.avsWindow == null)
        return;
      this.avsWindow.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Суффикс типа конструкторского документа в обозначении</summary>
  public string DocumentDesignationSuffix
  {
    get
    {
      if (this.IsSpecification)
        return (string) null;
      if (this.documentDesignationSuffix != null)
        return this.documentDesignationSuffix;
      DocumentTypeSettings settings = DocumentTypeSettingsHelper.GetSettings(this.DocumentDBObjectType);
      this.documentDesignationSuffix = !settings.DocumentTypeCodeInDesignation ? "" : settings.DocumentTypeCode;
      return this.documentDesignationSuffix;
    }
  }

  /// <summary>Обозначение документа</summary>
  public virtual string DocumentDesignation
  {
    [DebuggerStepThrough] get => this.documentDesignation;
    set
    {
      if (!(this.documentDesignation != value))
        return;
      this.documentDesignation = value;
      this.BaseProductDesignation = AVSDocument.FindProductDesignation(this.documentDesignation, this.DocumentDesignationSuffix);
      if (this.avsWindow != null)
      {
        this.avsWindow.DocumentDesignation = value;
      }
      else
      {
        if (this.Document == null)
          return;
        this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, value, false, false, false);
      }
    }
  }

  /// <summary>Найти обозначение исполнения в обозначении документа</summary>
  /// <param name="documentDesignation">Обозначение документа</param>
  /// <param name="docSuffix">Суффикс в обозначении документа</param>
  /// <returns></returns>
  internal static string FindProductDesignation(string documentDesignation, string docSuffix)
  {
    string productDesignation = documentDesignation;
    if (productDesignation != null && productDesignation != "" && docSuffix != null && docSuffix != "")
    {
      int startIndex = productDesignation.LastIndexOf(docSuffix.Trim());
      if (startIndex != -1)
      {
        int length = docSuffix.Length;
        int num = 0;
        while (startIndex - num - 1 >= 0 && (char.IsWhiteSpace(productDesignation[startIndex - num - 1]) || productDesignation[startIndex - num - 1] == '_'))
          ++num;
        if (num != 0)
        {
          startIndex -= num;
          length += num;
          if (startIndex + num < productDesignation.Length && (char.IsDigit(productDesignation[startIndex + num]) || productDesignation[startIndex + num] == 'З'))
            ++length;
        }
        productDesignation = productDesignation.Remove(startIndex, length);
      }
    }
    return productDesignation;
  }

  /// <summary>Обозначение основного исполнения</summary>
  public virtual string BaseProductDesignation
  {
    [DebuggerStepThrough] get => this.baseProductDesignation;
    set
    {
      if (!(this.baseProductDesignation != value))
        return;
      this.baseProductDesignation = value;
    }
  }

  /// <summary>Создать узлы для документа и TreeList, там где они не были созданы</summary>
  /// <param name="reCreateDocNode">Пересоздать узлы документа</param>
  /// <param name="reCreateListNode">Пересоздать узлы TreeList</param>
  /// <param name="updateCountB">Обновить количество для формы Б</param>
  /// <param name="createForEmptyChapters">Создавать для пустых разделов</param>
  /// <param name="updateTemplate">Обновить шаблоны узлов документа</param>
  /// <param name="updateMode">Режим обновления записей с пустым количеством</param>
  public virtual void UpdateViewNodes(
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    if (this.CellTextOutputAttributeMappingSettings == null)
      this.LoadOutputAttributeMappingSettings();
    this.SuspendDocumentAndGridUpdates();
    try
    {
      int num1 = -1;
      int num2 = -1;
      int viewMode = (int) this.ViewMode;
      if (this.skipLinesSchema == null)
        this.GetSkipLinesSchema();
      this.SaveExpanded();
      Dictionary<int, TableData> dictionary = new Dictionary<int, TableData>();
      TableData dataOwner1 = this.avsDocTable;
      TableData dataOwner2 = this.avsDocTableExpMix;
      if (this.AvsDocumentForm == AVSDocumentForm.Single)
      {
        this.commonDataChapter.DocNode = this.avsDocTable;
        this.commonDataChapter.DocNodeExp = this.avsDocTableExpMix;
        this.commonDataChapter.ListNode = (TreeListNode) null;
        int num3 = -1;
        for (int index1 = 0; index1 < this.rootChapters.Count; ++index1)
        {
          this.rootChapters[index1].UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
          if (this.rootChapters[index1] != this.commonDataChapter && this.rootChapters[index1].DocNode != null)
          {
            if (num3 == -1)
            {
              dataOwner1 = (TableData) null;
              for (int index2 = this.commonDataChapter.Chapters.Count - 1; index2 >= 0; --index2)
              {
                TableData docNode = this.commonDataChapter.Chapters[index2].DocNode;
                if (docNode != null)
                {
                  TableData lastCell = (TableData) docNode.FindLastCell();
                  dataOwner1 = lastCell.ParentCell;
                  num3 = lastCell.Index;
                  break;
                }
              }
            }
            if (num3 == -1)
            {
              num3 = 0;
              dataOwner1 = this.commonDataChapter.DocNode;
            }
            else
              num3 = dataOwner1.FindNextDataPositionInFlow(num3, out dataOwner1);
            dataOwner1.InsertChildNode(num3, (DocumentTreeNode) this.rootChapters[index1].DocNode, false, true, false, false, false);
          }
          if (this.rootChapters[index1] != this.commonDataChapter && this.rootChapters[index1].HasDocNodesExp)
          {
            if (num2 == -1)
            {
              dataOwner2 = (TableData) null;
              for (int index3 = this.commonDataChapter.Chapters.Count - 1; index3 >= 0; --index3)
              {
                TableData tableData = this.commonDataChapter.Chapters[index3].DocNodesExp[0];
                if (tableData != null)
                {
                  TableData lastCell = (TableData) tableData.FindLastCell();
                  dataOwner2 = lastCell.ParentCell;
                  num2 = lastCell.Index;
                  break;
                }
              }
            }
            if (num2 == -1)
            {
              num2 = 0;
              dataOwner2 = this.commonDataChapter.DocNodesExp[0];
            }
            else
              num2 = dataOwner2.FindNextDataPositionInFlow(num2, out dataOwner2);
            dataOwner2.InsertChildNode(num2, (DocumentTreeNode) this.rootChapters[index1].DocNodesExp[0], false, true, false, false, false);
          }
        }
      }
      else if (this.AvsDocumentForm == AVSDocumentForm.A)
      {
        this.commonDataChapter.UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        for (int index = 0; index < this.commonDataChapter.DocNodes.Count; ++index)
        {
          num1 = index != 0 ? dataOwner1.FindNextDataPositionInFlow(num1, out dataOwner1) : this.avsDocTable.FindDataPositionInFlow(0, out dataOwner1);
          dataOwner1.InsertChildNode(num1, (DocumentTreeNode) this.commonDataChapter.DocNodes[index], false, true, false, false, false);
        }
        if (this.commonDataChapter.DocNodeExp != null)
        {
          num2 = dataOwner2.FindDataPositionInFlow(0, out dataOwner2);
          dataOwner2.InsertChildNode(num2, (DocumentTreeNode) this.commonDataChapter.DocNodeExp, false, true, false, false, false);
        }
        if (this.variableDataChapter_FormA != null)
        {
          this.variableDataChapter_FormA.UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
          if (this.variableDataChapter_FormA.HasDocNodes)
          {
            for (int index = 0; index < this.variableDataChapter_FormA.DocNodes.Count; ++index)
            {
              num1 = dataOwner1.FindNextDataPositionInFlow(num1, out dataOwner1);
              dataOwner1.InsertChildNode(num1, (DocumentTreeNode) this.variableDataChapter_FormA.DocNodes[index], false, true, false, false, false);
            }
          }
          if (this.variableDataChapter_FormA.DocNodeExp != null)
          {
            num2 = dataOwner2.FindNextDataPositionInFlow(num2, out dataOwner2);
            dataOwner2.InsertChildNode(num2, (DocumentTreeNode) this.variableDataChapter_FormA.DocNodeExp, false, true, false, false, false);
          }
        }
        for (int index4 = 0; index4 < this.rootChapters.Count; ++index4)
        {
          if (this.rootChapters[index4].IsAdditionalChapter)
          {
            this.rootChapters[index4].UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
            if (this.rootChapters[index4] != this.commonDataChapter && this.rootChapters[index4] != this.variableDataChapter_FormA && this.rootChapters[index4].DocNode != null)
            {
              if (num1 == -1)
              {
                dataOwner1 = (TableData) null;
                for (int index5 = this.commonDataChapter.Chapters.Count - 1; index5 >= 0; --index5)
                {
                  TableData docNode = this.commonDataChapter.Chapters[index5].DocNode;
                  if (docNode != null)
                  {
                    TableData lastCell = (TableData) docNode.FindLastCell();
                    dataOwner1 = lastCell.ParentCell;
                    num1 = lastCell.Index;
                    break;
                  }
                }
              }
              if (num1 == -1)
              {
                num1 = 0;
                dataOwner1 = this.commonDataChapter.DocNode;
              }
              else
                num1 = dataOwner1.FindNextDataPositionInFlow(num1, out dataOwner1);
              dataOwner1.InsertChildNode(num1, (DocumentTreeNode) this.rootChapters[index4].DocNode, false, true, false, false, false);
            }
            if (this.rootChapters[index4] != this.commonDataChapter && this.rootChapters[index4] != this.variableDataChapter_FormA && this.rootChapters[index4].DocNodeExp != null)
            {
              if (num2 == -1)
              {
                dataOwner2 = (TableData) null;
                for (int index6 = this.commonDataChapter.Chapters.Count - 1; index6 >= 0; --index6)
                {
                  TableData docNodeExp = this.commonDataChapter.Chapters[index6].DocNodeExp;
                  if (docNodeExp != null)
                  {
                    TableData lastCell = (TableData) docNodeExp.FindLastCell();
                    dataOwner2 = lastCell.ParentCell;
                    num2 = lastCell.Index;
                    break;
                  }
                }
              }
              if (num2 == -1)
              {
                num2 = 0;
                dataOwner2 = this.commonDataChapter.DocNodeExp;
              }
              else
                num2 = dataOwner2.FindNextDataPositionInFlow(num2, out dataOwner2);
              dataOwner2.InsertChildNode(num2, (DocumentTreeNode) this.rootChapters[index4].DocNodeExp, false, true, false, false, false);
            }
          }
        }
      }
      else if (this.IsFormB && !this.IsExportSP)
      {
        this.commonDataChapter.UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        PageData pageData = (PageData) null;
        List<TableData> tableDataList = new List<TableData>();
        List<int> intList = new List<int>();
        List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
        for (int index7 = 0; index7 < this.commonDataChapter.DocNodes.Count; ++index7)
        {
          int index8 = pageData == null ? this.FindPageIndexAfterTitlePages() : pageData.FindLastPage().Index + 1;
          if (index8 == -1)
            index8 = this.document.Nodes.Count;
          int indexForDocChapter = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.commonDataChapter.DocNodes[index7]);
          bool flag = this.commonDataChapter.DocNodes[index7].OwnerDocument == null;
          if (flag && this.commonDataChapter.DocNodes[index7].Page != null && index7 > 0)
          {
            string attributeValue = indexForDocChapter.ToString();
            this.commonDataChapter.DocNodes[index7].Page.SetAttributeValue(AVSRow.DocAttr_ProductIndex, attributeValue, false, false, false);
            string str = $"Исполнения {index7 * this.RowProductCount}...{(index7 + 1) * this.RowProductCount - 1}";
            this.commonDataChapter.DocNodes[index7].Page.Id = str;
            this.commonDataChapter.DocNodes[index7].Page.SetName(str, false, false);
          }
          this.document.InsertChildNode(index8, (DocumentTreeNode) this.commonDataChapter.DocNodes[index7].Page, !flag, true, false, false, false);
          pageData = this.commonDataChapter.DocNodes[index7].Page;
          if (!dictionary.ContainsKey(indexForDocChapter))
            dictionary.Add(indexForDocChapter, this.commonDataChapter.DocNodes[index7]);
          intList.Add(-1);
          tableDataList.Add(this.commonDataChapter.DocNodes[index7]);
          documentTreeNodeList.Add((DocumentTreeNode) this.commonDataChapter.DocNodes[index7].FindLastDataCellInFlow(out dataOwner1));
        }
        for (int index9 = 0; index9 < this.rootChapters.Count; ++index9)
        {
          if (!this.rootChapters[index9].IsCommonDataChapter)
          {
            this.rootChapters[index9].UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
            for (int index10 = 0; index10 < this.rootChapters[index9].DocNodes.Count; ++index10)
            {
              int indexForDocChapter = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.rootChapters[index9].DocNodes[index10]);
              dictionary.TryGetValue(indexForDocChapter, out dataOwner1);
              int index11 = -1;
              if (tableDataList.Count == 1)
              {
                index11 = 0;
              }
              else
              {
                for (int index12 = 0; index12 < tableDataList.Count; ++index12)
                {
                  if (tableDataList[index12].FindFirstTable() == dataOwner1)
                  {
                    index11 = index12;
                    break;
                  }
                }
              }
              if (index11 >= 0)
              {
                if (documentTreeNodeList[index11] == null)
                  intList[index11] = dataOwner1.FindDataPositionInFlow(0, out dataOwner1);
                else if (intList[index11] == -1)
                  intList[index11] = !(documentTreeNodeList[index11].Parent is TableData parent) ? -1 : parent.FindNextDataPositionInFlow(documentTreeNodeList[index11].Index, out dataOwner1);
                else
                  dataOwner1 = documentTreeNodeList[index11].Parent as TableData;
                tableDataList[index11] = dataOwner1;
                if (intList[index11] != -1)
                  dataOwner1.InsertChildNode(intList[index11]++, (DocumentTreeNode) this.rootChapters[index9].DocNodes[index10], this.rootChapters[index9].DocNodes[index10].Parent != null, true, false, false, false);
              }
            }
          }
        }
      }
      else if (this.AvsDocumentForm == AVSDocumentForm.V && !this.IsExportSP)
      {
        this.commonDataChapter?.UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        Chapter commonDataChapter = this.commonDataChapter;
        if ((commonDataChapter != null ? (commonDataChapter.HasDocNodes ? 1 : 0) : 0) != 0)
        {
          for (int index = 0; index < this.commonDataChapter.DocNodes.Count; ++index)
          {
            num1 = index != 0 ? dataOwner1.FindNextDataPositionInFlow(num1, out dataOwner1) : this.avsDocTable.FindDataPositionInFlow(0, out dataOwner1);
            dataOwner1.InsertChildNode(num1, (DocumentTreeNode) this.commonDataChapter.DocNodes[index], false, true, false, false, false);
          }
        }
        PageData prevPage1 = this.avsDocTable.Page.FindLastPage();
        if (this.variableDataChapter_FormV != null)
        {
          this.variableDataChapter_FormV.UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
          PageData prevPage2 = (PageData) null;
          if (this.commonDataChapter?.DocNode != null)
            prevPage2 = this.commonDataChapter.DocNode.Page;
          prevPage1 = this.variableDataChapter_FormV.InsertPagesInDocument(prevPage2);
        }
        for (int index = 0; index < this.rootChapters.Count; ++index)
        {
          if (this.rootChapters[index].IsAdditionalChapter)
          {
            this.rootChapters[index].UpdateViewNodes(this.skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
            prevPage1 = this.rootChapters[index].InsertPagesInDocument(prevPage1);
          }
        }
      }
      this.UpdateSkipLines(false, false);
      if (!this.IsGridViewMode)
        return;
      this.avsWindow.virtualTree.RestoreExpanded();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, this.document.NeedUpdateLayoutFlag, true, true);
    }
  }

  private int FindPageIndexAfterTitlePages()
  {
    PageData lastPage = AVSDocument.FindTitlePages((ImDocumentData) this.Document).LastOrDefault<PageData>()?.FindLastPage();
    return lastPage == null ? 0 : lastPage.Index + 1;
  }

  /// <summary>Блокировать все автоматические обновления в документе</summary>
  /// <returns>true - если блокировка выполнена, false - если документ уже был заблокирован</returns>
  public void SuspendDocumentAndGridUpdates() => this.SuspendDocumentAndGridUpdates(true, true);

  /// <summary>Блокировать все автоматические обновления в документе</summary>
  /// <returns>true - если блокировка выполнена, false - если документ уже был заблокирован</returns>
  public void SuspendDocumentAndGridUpdates(bool suspendDocument, bool suspendGrid)
  {
    if (this.suspendDocumentAndGridUpdatesCount == 0)
    {
      if (this.document != null & suspendDocument)
      {
        this.document.SuspendUpdateGeometryRefreshUI();
        this.document.SuspendUpdateLayout();
      }
      if (this.IsGridViewMode & suspendGrid)
        this.avsWindow.virtualTree.BeginUpdate();
    }
    ++this.suspendDocumentAndGridUpdatesCount;
  }

  /// <summary>Возобновить автоматическое обновление разбивки документа и обновления табличного вида</summary>
  /// <param name="fromPage">Обновлять начиная со страницы</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="resumeDocument">Возобновить обновление документа</param>
  /// <param name="resumeGrid">Возобновить обновление табличного вида</param>
  /// <param name="forceUpdate">Принудительно обновить табличный вид</param>
  public void ResumeDocumentAndGridUpdates(
    int fromPage,
    bool updateUI,
    bool updateLayout,
    bool resumeDocument,
    bool resumeGrid,
    bool forceUpdate = false)
  {
    if (this.suspendDocumentAndGridUpdatesCount > 0)
      --this.suspendDocumentAndGridUpdatesCount;
    if (forceUpdate)
      this.suspendDocumentAndGridUpdatesCount = 0;
    if (this.suspendDocumentAndGridUpdatesCount != 0)
      return;
    if (this.document != null & resumeDocument)
    {
      if (!((ImDocumentData) this.document).IsDistributing)
        this.UpdateProductHeadersOnPages(false, false);
      this.document.ResumeUpdateLayout(fromPage, false, false);
      if (updateLayout && !this.document.SuspendedUpdateLayoutFlag)
        this.document.UpdateLayout(fromPage, forceUpdate, true, false, false);
      if (!((ImDocumentData) this.document).IsDistributing)
      {
        this.UpdateProductHeadersOnPages(false, false);
        if (updateLayout)
          this.document.ResetNeedUpdateLayoutFlag(true);
      }
      this.document.ResumeUpdateRefreshUI(updateUI, updateUI);
    }
    if (!(this.IsGridViewMode & resumeGrid))
      return;
    this.avsWindow.virtualTree.EndUpdate();
  }

  public ImRtfEditor SpecificationEditor
  {
    get
    {
      if (this.specificationEditor == null)
        this.specificationEditor = AVSDocument.CreateSpecificationEditor();
      return this.specificationEditor;
    }
    set => this.specificationEditor = value;
  }

  public static ImRtfEditor CreateSpecificationEditor()
  {
    return RtfInSiteEditorWrapper.CreateTernEditorBufferNotSpellCheck();
  }

  protected void UpdateLimiteAndNominalValues()
  {
    this.LoadAllProductsRelationsForType((AVSDocumentContext) null, AvsIDCache.Relation_Podbor, new RowDictionariesForLoadDocument());
    this.UpdateNoteDocCells(false, true);
  }

  /// <summary>Обновить заголовки для позиций</summary>
  public void UpdatePositionsCaptions()
  {
    if (this.Document == null)
      return;
    this.Document.UpdateLayout(true, true);
  }

  /// <summary>Метод сбрасывает все отступы обозначений до дефолтных</summary>
  /// <param name="node"></param>
  public virtual void ResetPartProductCaptions1(DocumentTreeNode node)
  {
    if (node is TextBoxElement textBoxElement && textBoxElement.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == AvsIDCache.Attr_Designation)
    {
      ParagraphFormat paragraphFormat = textBoxElement.ParagraphFormat.Clone();
      paragraphFormat.IdentLeft = new float?(0.0f);
      if (textBoxElement.Template is TextBoxElement)
        paragraphFormat.IdentLeft = (textBoxElement.Template as TextBoxElement).ParagraphFormat.IdentLeft;
      textBoxElement.SetParagraphFormat(paragraphFormat, false, false, false);
    }
    else
    {
      if (node.NodesCount == 0)
        return;
      for (int index = 0; index < node.NodesCount; ++index)
        this.ResetPartProductCaptions1(node.Nodes[index]);
    }
  }

  /// <summary>Обновить заголовки исполнений изделий в записях</summary>
  public void UpdatePartProductCaptions()
  {
    if (this.Document == null || this.Document.NodesCount == 0 || this.DesignationTrimSchema == null)
      return;
    this.Document.UpdateLayout(true, true);
  }

  /// <summary>Сокращать одинаковые обозначения исполнений в записях</summary>
  public bool UseSameProductDesignationsInRows
  {
    get => this.DesignationTrimSchema.UseSameProductDesignationsInRows;
  }

  /// <summary>Использовать разные обозначения для исполнений специфицируемых изделий</summary>
  public bool UseSameDesignationForProducts
  {
    get
    {
      return this.DesignationTrimSchema != null && this.DesignationTrimSchema.UseSameDesignationForProducts;
    }
  }

  /// <summary>Проверяет являются ли изделия исполнениями, в качестве схемы сравнения использует кэшированное значение схемы</summary>
  /// <param name="row1">Запись 1</param>
  /// <param name="row2">Запись 2</param>
  /// <returns></returns>
  public bool IsSameProductDesignations(AVSRow row1, AVSRow row2)
  {
    return this.DesignationTrimSchema != null && this.IsSameProductDesignations(row1, row2, this.DesignationTrimSchema);
  }

  /// <summary>Проверяет являются ли изделия исполнениями</summary>
  /// <param name="row1">Запись 1</param>
  /// <param name="row2">Запись 2</param>
  /// <param name="schema">схема сравнения, если равна null, игнорируется</param>
  /// <returns></returns>
  public bool IsSameProductDesignations(AVSRow row1, AVSRow row2, DesignationTrimSchema schema)
  {
    if (row1 == null || row2 == null)
      return false;
    string designation1 = row1.Designation;
    string designation2 = row2.Designation;
    if (designation1 == null || designation2 == null)
      return false;
    if (designation2.Length > designation1.Length)
    {
      string str = designation2;
      designation2 = designation1;
      designation1 = str;
    }
    if (AVSRow.IsSameArticleGroupID(row1, row2, schema))
      return true;
    if (schema != null && designation1.Length <= schema.LengthBasePart)
      return false;
    int startIndex = designation1.Length - 1;
    int num1 = 0;
    if (schema != null)
      num1 = schema.LengthBasePart;
    int count = startIndex - num1;
    int num2 = designation1.LastIndexOf("-", startIndex, count);
    bool flag = false;
    if (num2 != -1 && num2 < designation1.Length - 1 && char.IsDigit(designation1[num2 + 1]))
    {
      string suffiks1;
      string basePart1 = this.GetBasePart(designation1, schema, out suffiks1, DocumentTypeSettingsHelper.GetSettings(row1.ObjType));
      string suffiks2;
      string basePart2 = this.GetBasePart(designation2, schema, out suffiks2, DocumentTypeSettingsHelper.GetSettings(row2.ObjType));
      if (basePart2.Length < designation2.Length - 1 && !char.IsDigit(designation2[basePart2.Length + 1]))
        return false;
      if (basePart1 == basePart2 && suffiks1 == suffiks2)
        flag = true;
    }
    return flag;
  }

  /// <summary>Получить базовую часть обозначения исполнений изделия</summary>
  /// <param name="designation">Полное обозначение исполнения</param>
  /// <param name="designationTrimSchema">Настройки определения базовой части обозначения</param>
  /// <returns>Базовая часть обозначения исполнений изделия</returns>
  internal string GetBasePart(
    string designation,
    DesignationTrimSchema designationTrimSchema,
    out string suffiks,
    DocumentTypeSettings settings)
  {
    suffiks = string.Empty;
    if (designation == null || designation == "")
      return "";
    string basePart = designation;
    if (settings.DocumentTypeCode != null && settings.DocumentTypeCode != string.Empty && designation.EndsWith(settings.DocumentTypeCode))
    {
      basePart = basePart.Remove(designation.LastIndexOf(settings.DocumentTypeCode), settings.DocumentTypeCode.Length).TrimEnd();
      suffiks = settings.DocumentTypeCode;
    }
    int startIndex = basePart.Length - 1;
    int num = 0;
    if (designationTrimSchema != null)
      num = designationTrimSchema.LengthBasePart - 1;
    int count = startIndex - num;
    if (count > 0)
    {
      int length = basePart.LastIndexOf("-", startIndex, count);
      if (length >= 0 && length != basePart.Length - 1)
        return basePart.Substring(0, length);
    }
    return basePart;
  }

  /// <summary>Получить информацию о атрибуте для ячейки в документе</summary>
  /// <param name="cell">Ячейка в строке</param>
  /// <param name="cellIndex">Индекс ячейки в строке</param>
  /// <param name="isFormBRow">Запись для документа формы Б</param>
  /// <param name="session">Сессия</param>
  /// <returns>Информация об атрибуте</returns>
  public static AvsRowAttributeInfo GetAttrInfoFromCell(
    TextData cell,
    int cellIndex,
    bool isFormBRow)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    AvsRowAttributeInfo attribute = (AvsRowAttributeInfo) null;
    INodeWithReference nodeWithReference = (INodeWithReference) cell;
    if (nodeWithReference != null && nodeWithReference.Reference is ReferenceToDBObjectAttribute reference)
    {
      reference.GetOrUpdateAttributeID();
      attribute = new AvsRowAttributeInfo(reference.IsReferenceToRelation ? FieldSource.Relation : FieldSource.Object, reference.AttributeGuid, reference.AttributeID, reference.AttributeName);
    }
    if (attribute == null)
    {
      if (AVSRow.IsCountFormBCell(isFormBRow, cell))
      {
        attribute = new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_Count_Guid, AvsIDCache.Attr_Count, "Количество");
      }
      else
      {
        attribute = new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, cell.Name);
        attribute.IndexInValueList = cellIndex;
      }
    }
    if (attribute.AttributeId == -10000)
      throw new Exception($"Атрибут с именем '{attribute.Name}' (guid: {attribute.AttributeGuid}) не зарегистрирован.");
    attribute.ReadOnly = cell.ReadOnly;
    if (AVSRow.IsCountField(attribute))
      attribute.ColumnContent = ColumnContents.Value;
    return attribute;
  }

  /// <summary>Найти раздел спецификации для данного узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <param name="ignoreSections">Игнорировать разделы типа SpecificationSection</param>
  /// <returns>Раздел спецификации для данного узла документа</returns>
  public static DocumentTreeNode FindParentChapterDocNode(
    DocumentTreeNode docNode,
    bool ignoreSections)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (docNode is TableData tableData)
      {
        TableData firstTable = tableData.FindFirstTable();
        if (AVSDocument.IsChapterDocNode((DocumentTreeNode) firstTable, ignoreSections))
          return (DocumentTreeNode) firstTable;
      }
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Найти родительский подраздел на этой странице для данного узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <param name="ignoreSections">Игнорировать разделы типа SpecificationSection</param>
  /// <returns>Раздел спецификации для данного узла документа</returns>
  public static DocumentTreeNode FindParentChapterOnDocNodePage(
    DocumentTreeNode docNode,
    bool ignoreSections)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (AVSDocument.IsChapterDocNode(docNode, ignoreSections))
        return docNode;
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Узел документа является подразделом</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Является ли данный узел документа подразделом</returns>
  public static bool IsChapterDocNode(DocumentTreeNode docNode, bool ignoreSections)
  {
    switch (docNode)
    {
      case Page _:
      case ImDocument _:
        return false;
      case TableData tableData:
        if (tableData.FindFirstTable().Tag is Chapter tag && (!ignoreSections || !(tag is SpecificationSection) || tag.UseParentDocNode))
          return true;
        break;
    }
    return false;
  }

  /// <summary>Найти раздел с переменными данными спецификации для данного узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Раздел спецификации с переменными данными для данного узла документа</returns>
  public static DocumentTreeNode FindParentProductVariableDocNode(DocumentTreeNode docNode)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (docNode is TableData tableData)
      {
        TableData firstTable = tableData.FindFirstTable();
        if (AVSDocument.IsProductVariableDocNode((DocumentTreeNode) firstTable))
          return (DocumentTreeNode) firstTable;
      }
      if (docNode is Page || docNode is ImDocument)
        return (DocumentTreeNode) null;
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Данный узел документа является разделом с переменными данными</summary>
  /// <param name="docNode">Узел документа</param>
  public static bool IsProductVariableDocNode(DocumentTreeNode docNode)
  {
    if (docNode is TableData tableData)
    {
      TableData firstTable = tableData.FindFirstTable();
      if (firstTable.Tag is ProductVariableDataChapter || firstTable.GetAttributeValue(Chapter.DocNodeType_AttributeName, false) == Chapter.ProductVariableData_TypeName || firstTable.TemplateId == "Исполнение")
        return true;
    }
    return false;
  }

  /// <summary>Найти раздел спецификации для данного узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Раздел спецификации для данного узла документа</returns>
  public static DocumentTreeNode FindParentSpecSectionDocNode(DocumentTreeNode docNode)
  {
    while (true)
    {
      switch (docNode)
      {
        case null:
          goto label_5;
        case TableData docNode1 when AVSDocument.IsSpecSectionDocNode((DocumentTreeNode) docNode1):
          goto label_1;
        case Page _:
        case ImDocument _:
          goto label_2;
        default:
          docNode = docNode.Parent;
          continue;
      }
    }
label_1:
    return (DocumentTreeNode) docNode1;
label_2:
    return (DocumentTreeNode) null;
label_5:
    return (DocumentTreeNode) null;
  }

  /// <summary>Узел документа является разделом спецификации</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Является ли данный узел документа разделом спецификации</returns>
  public static bool IsSpecSectionDocNode(DocumentTreeNode docNode)
  {
    if (docNode is TableData tableData)
    {
      TableData firstTable = tableData.FindFirstTable();
      if (firstTable.Tag is SpecificationSection || firstTable.GetAttributeValue(Chapter.DocNodeType_AttributeName, false) == Chapter.Section_TypeName || docNode.TemplateId == "Раздел спецификации")
        return true;
    }
    return false;
  }

  /// <summary>Узел документа принадлежит разделу спецификации</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Узел документа принадлежит разделу спецификации</returns>
  public static bool IsSpecSectionDocNodeChild(DocumentTreeNode docNode)
  {
    return AVSDocument.FindParentSpecSectionDocNode(docNode) != null;
  }

  /// <summary>Узел документа является строкой примечания</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  public static bool IsGroupDocNode(DocumentTreeNode docNode)
  {
    TableData tableData = docNode != null ? docNode as TableData : throw new ArgumentNullException(nameof (docNode));
    string attributeValue = docNode.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
    return tableData != null && docNode.Name != null && tableData.TableCellType == CellType.DataCell && attributeValue == Chapter.AdditionalComplectGroup_TypeName;
  }

  /// <summary>Узел документа является строкой примечания</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  public static bool IsNoteRowDocNode(DocumentTreeNode docNode)
  {
    TableData tableData = docNode != null ? docNode as TableData : throw new ArgumentNullException(nameof (docNode));
    string attributeValue = docNode.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
    return tableData != null && docNode.Name != null && (attributeValue == Chapter.SpecNote_TypeName || docNode.Name.ToLower().Contains("примечани") || docNode.GetAttributeValue("Дополнительная запись", true) != "") && tableData.TableCellType == CellType.DataCell && attributeValue != Chapter.AdditionalComplectGroup_TypeName;
  }

  /// <summary>Узел документа является строкой примечания</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  public static bool IsProductPageLinksDocNode(DocumentTreeNode docNode)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    return docNode.GetAttributeValue(Chapter.DocNodeType_AttributeName, false) == Chapter.ProductPageLinks_TypeName || docNode.Id == "Содержание переменных данных";
  }

  /// <summary>Узел документа является строкой примечания</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  public static bool IsProductPageLinksDocNodeChild(DocumentTreeNode docNode)
  {
    for (DocumentTreeNode docNode1 = docNode; docNode1 != null; docNode1 = docNode1.Parent)
    {
      if (AVSDocument.IsProductPageLinksDocNode(docNode1))
        return true;
    }
    return false;
  }

  /// <summary>Получить тип связи, который создается для записи СП</summary>
  /// <param name="row">Строка</param>
  /// <param name="context">Контекст, ищется в нем если нет строки</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="defaultRelationType">Тип связи который планируется получить, -1 - решает сугубо сам метод</param>
  /// <returns></returns>
  public virtual int GetRelationType(
    AVSRow row,
    AVSDocumentContext context,
    int objectType,
    int defaultRelationType)
  {
    int relationType = defaultRelationType;
    if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Document, objectType))
    {
      relationType = AvsIDCache.Relation_Document;
      SpecificationSection specificationSection = (SpecificationSection) null;
      if (row != null)
        specificationSection = row.Section;
      else if (context != null)
        specificationSection = context.Section;
      Guid? sectionGuid = specificationSection?.SectionInfo?.SectionGuid;
      Guid complectSectionGuid = SpecificationSectionInfo.ComplectSectionGuid;
      if ((sectionGuid.HasValue ? (sectionGuid.HasValue ? (sectionGuid.GetValueOrDefault() == complectSectionGuid ? 1 : 0) : 1) : 0) != 0)
      {
        List<IMSObjectType> childObjectTypes = MetaDataHelper.GetApplicabilityChildObjectTypes(this.productsInfo[0].ObjectType, AvsIDCache.Relation_Project);
        if (childObjectTypes != null)
        {
          foreach (IMSObjectType imsObjectType in childObjectTypes)
          {
            if (AVSDocument.IsParentObjectType(imsObjectType.ObjectTypeID, objectType))
              relationType = AvsIDCache.Relation_Project;
          }
        }
      }
    }
    else if (defaultRelationType == -1)
      relationType = AvsIDCache.Relation_Project;
    return relationType;
  }

  /// <summary>Настроить узел документа как строку примечания</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  public static void SetupDocNodeAsNoteRow(DocumentTreeNode docNode)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    docNode.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.SpecNote_TypeName, false, false, false);
    if (docNode is TableData tableData)
    {
      if (tableData.Reference != null && tableData.Reference is ReferenceToDBObject)
        tableData.AssignReference((ReferenceBase) null, false, false);
      for (int index = 0; index < tableData.NodesCount; ++index)
      {
        if (tableData.Nodes[index] is TextData node2)
        {
          if (node2.ReferenceToTextSource != null && node2.ReferenceToTextSource is ReferenceToDBObjectAttribute)
          {
            string text = node2.Text;
            node2.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
            node2.AssignText(text, false, false, false);
          }
        }
        else if (tableData.Nodes[index] is TableData node1 && node1.Reference != null && node1.Reference is ReferenceToDBObject)
          node1.AssignReference((ReferenceBase) null, false, false);
      }
    }
    else
    {
      if (!(docNode is TextData textData) || textData.ReferenceToTextSource == null || !(textData.ReferenceToTextSource is ReferenceToDBObjectAttribute))
        return;
      string text = textData.Text;
      textData.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
      textData.AssignText(text, false, false, false);
    }
  }

  /// <summary>Найти родительский узел ячейки являющийся строкой примечания</summary>
  /// <param name="docNode">Ячейка документа</param>
  /// <returns></returns>
  public static DocumentTreeNode FindParentNoteRowDocNode(DocumentTreeNode docNode)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (docNode is Page || docNode is ImDocument)
        return (DocumentTreeNode) null;
      if (AVSDocument.IsNoteRowDocNode(docNode))
        return docNode;
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Узел документа является строкой спецификации</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа является строкой спецификации</returns>
  public static bool IsSpecRowDocNode(DocumentTreeNode docNode)
  {
    if (!(docNode is TableData tableData))
      return false;
    TableData firstTable = tableData.FindFirstTable();
    return firstTable.Tag is AVSRow tag && !tag.IsNoteRow || firstTable.GetAttributeValue(Chapter.DocNodeType_AttributeName, false) == Chapter.AVSRow_TypeName || firstTable.TemplateId == "Строка спецификации" || firstTable.TemplateId == "Строка спецификации. Форма Б" || firstTable.TemplateId == "Запись" || firstTable.TemplateId == "Строка спецификации. EXP" || firstTable.TemplateId == "Строка спецификации. Форма Б. EXP" || firstTable.TemplateId == "Запись. EXP";
  }

  /// <summary>Узел TreeList является разделом</summary>
  /// <param name="node">Узел TreeList</param>
  /// <returns>true если узел является разделом</returns>
  public static bool IsSectionTreeListNode(TreeListNode node)
  {
    return node != null && node.Tag != null && node.Tag is SpecificationSection;
  }

  /// <summary>Найти узел строки спецификации, который является родительским для заданного узла</summary>
  /// <param name="docNode">Узел документа, принадлежащий строке спецификации</param>
  /// <returns>Узел строки спецификации, который является родительским для заданного узла</returns>
  public static DocumentTreeNode FindParentSpecRowDocNode(DocumentTreeNode docNode)
  {
    while (true)
    {
      switch (docNode)
      {
        case null:
          goto label_7;
        case Page _:
        case ImDocument _:
          goto label_1;
        case TableData tableData:
          docNode = (DocumentTreeNode) tableData.FindFirstTable();
          break;
      }
      if (!AVSDocument.IsSpecRowDocNode(docNode))
        docNode = docNode.Parent;
      else
        goto label_4;
    }
label_1:
    return (DocumentTreeNode) null;
label_4:
    return docNode;
label_7:
    return (DocumentTreeNode) null;
  }

  /// <summary>Заданный узел документа является строкой спецификации</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа является строкой спецификации</returns>
  public static bool IsSpecRowDocNodeChild(DocumentTreeNode docNode)
  {
    return AVSDocument.FindParentSpecRowDocNode(docNode) != null;
  }

  /// <summary>Заданный узел документа является примечанием</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа является примечанием</returns>
  public static bool IsNoteRowDocNodeChild(DocumentTreeNode docNode)
  {
    return AVSDocument.FindParentNoteRowDocNode(docNode) != null;
  }

  /// <summary>Найти узел записи листа регистрации изменений, который является родительским для заданного узла</summary>
  /// <param name="docNode">Узел документа</param>
  public DocumentTreeNode FindParentLRIRowDocNode(DocumentTreeNode docNode)
  {
    while (true)
    {
      switch (docNode)
      {
        case null:
          goto label_7;
        case Page _:
        case ImDocument _:
          goto label_1;
        case TableData tableData:
          docNode = (DocumentTreeNode) tableData.FindFirstTable();
          break;
      }
      if (!this.IsLRIRowDocNode(docNode))
        docNode = docNode.Parent;
      else
        goto label_4;
    }
label_1:
    return (DocumentTreeNode) null;
label_4:
    return docNode;
label_7:
    return (DocumentTreeNode) null;
  }

  /// <summary>Заданный узел документа является записью листа регистрации изменений</summary>
  /// <param name="docNode">Узел документа</param>
  public bool IsLRIRowDocNode(DocumentTreeNode docNode)
  {
    if (!(docNode is TableData tableData) || !this.HasLRITemplates)
      return false;
    TableData firstTable = tableData.FindFirstTable();
    if (firstTable.TemplateId == this.lriRowTemplate.Id)
      return true;
    return firstTable.IsTableCell && firstTable.Parent.TemplateId == this.lriTableTemplate.Id;
  }

  internal bool HasLRITemplates => this.lriRowTemplate != null && this.lriTableTemplate != null;

  /// <summary>Заданный узел документа находится в таблице кодов исполнений и литер</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа находится в таблице кодов исполнений и литер</returns>
  public bool IsProductKodOrLitera(DocumentTreeNode docNode)
  {
    if (docNode is PageElementNode pageElementNode)
    {
      PageData page = pageElementNode.Page;
      TableData parentNode = (TableData) null;
      if (page != null)
        parentNode = this.FindProductKodAndLiteraTable(page);
      if (parentNode != null && docNode.IsChildForNode((DocumentTreeNode) parentNode, false))
        return true;
    }
    return false;
  }

  /// <summary>Найти таблицу кодов исполнений и литер</summary>
  /// <param name="page">Страница с таблицей</param>
  /// <returns>Таблицу кодов исполнений и литер</returns>
  public TableData FindProductKodAndLiteraTable(PageData page)
  {
    if (page.IsTemplate)
      return this.productKodAndLiteraTemplate != null ? this.productKodAndLiteraTemplate : this.productKodAndLitera2Template;
    TableData kodAndLiteraTable = (TableData) null;
    if (this.productKodAndLiteraTemplate != null)
      kodAndLiteraTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productKodAndLiteraTemplate) as TableData;
    if (kodAndLiteraTable == null && this.productKodAndLitera2Template != null)
      kodAndLiteraTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productKodAndLitera2Template) as TableData;
    return kodAndLiteraTable;
  }

  /// <summary>Получить исполнение для ячейки из таблицы номеров исполнений</summary>
  /// <param name="docNode">Ячейка таблицы</param>
  /// <returns>Исполнение</returns>
  public ProductInfo GetProductForProductKodOrLiteraCell(DocumentTreeNode docNode)
  {
    ProductInfo productKodOrLiteraCell = (ProductInfo) null;
    if (docNode is TextData textData)
    {
      PageData page = textData.Page;
      if (page != null)
      {
        TableData kodAndLiteraTable = this.FindProductKodAndLiteraTable(page);
        if (kodAndLiteraTable != null && docNode.IsChildForNode((DocumentTreeNode) kodAndLiteraTable, false))
        {
          int index = this.GetFirstProductIndex(page) + textData.Index;
          if (index < this.productsInfo.Count)
            productKodOrLiteraCell = this.productsInfo[index];
        }
      }
    }
    return productKodOrLiteraCell;
  }

  /// <summary>Заданный узел документа находится в таблице кодов ОКП исполнений</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа находится в таблице кодов ОКП исполнений</returns>
  public bool IsProductKodOKP(DocumentTreeNode docNode)
  {
    if (docNode is PageElementNode pageElementNode)
    {
      PageData page = pageElementNode.Page;
      TableData parentNode = (TableData) null;
      if (page != null)
        parentNode = this.FindProductKodOKPTable(page);
      if (parentNode != null && docNode.IsChildForNode((DocumentTreeNode) parentNode, false))
        return true;
    }
    return false;
  }

  /// <summary>Найти таблицу кодов ОКП исполнений</summary>
  /// <param name="page">Страница с таблицей</param>
  /// <returns>Таблицу кодов исполнений и литер</returns>
  public TableData FindProductKodOKPTable(PageData page)
  {
    TableData productKodOkpTable = (TableData) null;
    if (this.productKodOKPTemplate != null)
      productKodOkpTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productKodOKPTemplate) as TableData;
    if (productKodOkpTable == null && this.productKodOKP2Template != null)
      productKodOkpTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productKodOKP2Template) as TableData;
    return productKodOkpTable;
  }

  /// <summary>Получить исполнение для ячейки из таблицы кодов ОКП исполнений</summary>
  /// <param name="docNode">Ячейка таблицы</param>
  /// <returns>Исполнение</returns>
  public ProductInfo GetProductForProductKodOKPCell(DocumentTreeNode docNode)
  {
    ProductInfo productKodOkpCell = (ProductInfo) null;
    RectangleElement rectangleElement = (RectangleElement) (docNode as TextData);
    if (rectangleElement != null)
    {
      PageData page = rectangleElement.Page;
      if (page != null)
      {
        TableData productKodOkpTable = this.FindProductKodOKPTable(page);
        if (productKodOkpTable != null && docNode.IsChildForNode((DocumentTreeNode) productKodOkpTable, false))
        {
          int index = -1;
          if (docNode is TableData tableData && tableData.IsRow)
            index = tableData.Index - productKodOkpTable.HeadersCount;
          else if (rectangleElement.IsSingleCell)
            index = rectangleElement.ParentCell.Index - productKodOkpTable.HeadersCount;
          if (index >= 0 && index < this.productsInfo.Count)
            productKodOkpCell = this.productsInfo[index];
        }
      }
    }
    return productKodOkpCell;
  }

  /// <summary>Заданный узел документа находится в таблице номеров исполнений</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа находится в таблице номеров исполнений</returns>
  public bool IsProductNumberCell(DocumentTreeNode docNode)
  {
    if (docNode is PageElementNode pageElementNode)
    {
      PageData page = pageElementNode.Page;
      TableData parentNode = (TableData) null;
      if (page != null)
        parentNode = this.FindProductNumberTable(page);
      if (parentNode != null && docNode.IsChildForNode((DocumentTreeNode) parentNode, false))
        return true;
    }
    return false;
  }

  /// <summary>Получить исполнение для ячейки из таблицы номеров исполнений</summary>
  /// <param name="docNode">Ячейка таблицы</param>
  /// <returns>Исполнение</returns>
  public ProductInfo GetProductForProductNumberCell(DocumentTreeNode docNode)
  {
    ProductInfo productNumberCell = (ProductInfo) null;
    if (docNode is PageElementNode pageElementNode)
    {
      PageData page = pageElementNode.Page;
      if (page != null)
      {
        TableData productNumberTable = this.FindProductNumberTable(page);
        if (productNumberTable != null && docNode.IsChildForNode((DocumentTreeNode) productNumberTable, false))
        {
          int index = this.GetFirstProductIndex(page) + docNode.Index;
          if (index < this.productsInfo.Count)
            productNumberCell = this.productsInfo[index];
        }
      }
    }
    return productNumberCell;
  }

  /// <summary>Найти таблицу номеров исполнений</summary>
  /// <param name="page">Страница с таблицей</param>
  /// <returns>Таблицу номеров исполнений</returns>
  public TableData FindProductNumberTable(PageData page)
  {
    TableData productNumberTable = (TableData) null;
    if (this.productNumbersTemplate != null)
      productNumberTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productNumbersTemplate) as TableData;
    if (productNumberTable == null && this.productNumbers2Template != null)
      productNumberTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productNumbers2Template) as TableData;
    if (productNumberTable == null && this.productNumbers3Template != null)
      productNumberTable = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.productNumbers3Template) as TableData;
    return productNumberTable;
  }

  /// <summary>Получить настройки пропусков строк</summary>
  /// <returns></returns>
  public SkipLinesSchema GetSkipLinesSchema()
  {
    if (this.skipLinesSchema == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.skipLinesSchema = (SkipLinesSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_SkipLines, typeof (SkipLinesSchema));
    }
    return this.skipLinesSchema;
  }

  /// <summary>Обновить пропуск строк</summary>
  public void UpdateSkipLines(bool updateUi, bool updateLayout, SpecificationSection section = null)
  {
    if (!this.IsGeneratedDoc && this.ReadOnly)
      return;
    this.Document.SuspendUpdateLayout();
    try
    {
      if (this.skipLinesSchema == null)
        this.GetSkipLinesSchema();
      if (this.skipLinesSchema == null)
        return;
      if (this.Document.DocumentTemplate != null)
        this.Document.DocumentTemplate.SetDefaultNonSkipAtStartPage(this.skipLinesSchema.NonSkipBeforeAtStartPage, false, false, false);
      else
        this.Document.SetDefaultNonSkipAtStartPage(this.skipLinesSchema.NonSkipBeforeAtStartPage, false, false, false);
      List<SkipLinesStruct> skipLines = this.GetSkipLines((Chapter) section);
      for (int index = 0; index < skipLines.Count; ++index)
      {
        SkipLinesStruct str = skipLines[index];
        if (str.Chapter != null)
          str.Chapter.UpdateSkipLines(this.skipLinesSchema, str);
        if (str.SpecRow != null)
          str.SpecRow.UpdateSkipLines(this.skipLinesSchema, str);
      }
    }
    finally
    {
      this.Document.ResumeUpdateLayout(updateUi, updateLayout);
    }
  }

  internal List<SkipLinesStruct> GetSkipLines(Chapter chapter = null)
  {
    List<SkipLinesStruct> structs = new List<SkipLinesStruct>();
    if (chapter != null)
    {
      chapter.GetSkipLines(this.skipLinesSchema, structs);
    }
    else
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
        this.rootChapters[index].GetSkipLines(this.skipLinesSchema, structs);
    }
    for (int index = 0; index < structs.Count; ++index)
    {
      SkipLinesStruct prevsl = structs[index];
      SkipLinesStruct sl = index < structs.Count - 1 ? structs[index + 1] : (SkipLinesStruct) null;
      SkipLinesStruct.CompareSkipLineSettings(prevsl, sl);
      if (chapter != null)
      {
        if (index == 0 && (prevsl.Chapter == chapter || prevsl.SpecRow != null))
          prevsl.SkipBefore = float.NaN;
        if (sl == null && prevsl.SpecRow != null)
          prevsl.SkipAfter = float.NaN;
      }
    }
    return structs;
  }

  /// <summary>Получить настройки динамических заголовков групп</summary>
  /// <returns></returns>
  public DynamicGroupHeaderSettings DynamicGroupHeaderSettings
  {
    get
    {
      if (this.dynamicGroupHeaderSettings == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.dynamicGroupHeaderSettings = (DynamicGroupHeaderSettings) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_DynamicGroupHeaderSettings, typeof (DynamicGroupHeaderSettings));
      }
      return this.dynamicGroupHeaderSettings;
    }
  }

  [Browsable(false)]
  public bool NewCellMappingMode => true;

  /// <summary>Получить настройки для вычисления текста ячейки из атрибутов</summary>
  /// <returns></returns>
  public OutputAttributeMappingScheme CellTextOutputAttributeMappingSettings
  {
    get
    {
      if (this.cellTextOutputAttributeMappingSettings == null)
        this.UpdateOutputAttributeMappingSettings();
      return this.cellTextOutputAttributeMappingSettings;
    }
  }

  private void UpdateOutputAttributeMappingSettings()
  {
    this.cellTextOutputAttributeMappingSettings = this.LoadOutputAttributeMappingSettings();
    foreach (AVSRow row in this.GetRows())
      row.ResetCellMappingCache();
  }

  /// <summary>Получить настройки для вычисления текста ячейки из атрибутов</summary>
  public OutputAttributeMappingScheme LoadOutputAttributeMappingSettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (OutputAttributeMappingScheme) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.AVSDocumentTemplateID, -1, -1L, AvsIDCache.Attr_OutputMappingSchema, typeof (OutputAttributeMappingScheme));
  }

  public Dictionary<string, string> ReplaceClassInGroupHeaderDictionary
  {
    get => this.GetKeywordReplacementSettings().Data;
  }

  private KeywordReplacementScheme GetKeywordReplacementSettings()
  {
    if (AVSDocument.keywordReplacementSettings == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AVSDocument.keywordReplacementSettings = new KeywordReplacementScheme();
        AVSDocument.keywordReplacementSettings.LoadFromDBObjectAttribute(AVSDocument.ObjID_CommonSpecificationTemplate, AvsIDCache.Attr_DynamicHeaderKeywordReplacementSchema, sessionKeeper.Session);
      }
    }
    return AVSDocument.keywordReplacementSettings;
  }

  /// <summary>Обновить настройки динамических заголовков групп</summary>
  public void UpdateDynamicGroupHeaderSettings(bool updateUi, bool updateLayout)
  {
    if (!this.IsGeneratedDoc && this.ReadOnly)
      return;
    this.Document.SuspendUpdateLayout();
    try
    {
      if (AVSDocument.keywordReplacementSettings == null)
        AVSDocument.keywordReplacementSettings = this.GetKeywordReplacementSettings();
      foreach (SpecificationSection specificationSection in this.GetChaptersEnumerator().OfType<SpecificationSection>())
        specificationSection.UpdateDynamicHeaderSettings(this.DynamicGroupHeaderSettings);
    }
    finally
    {
      this.Document.ResumeUpdateLayout(updateUi, updateLayout);
    }
  }

  /// <summary>Заблокировать обработчик cell_TextChanged. Увеличивает счетчик блокировок.</summary>
  public void Lock_DocCell_TextChanged() => ++this.lock_DocCell_TextChanged_Count;

  /// <summary>Разблокировать обработчик cell_TextChanged. Уменьшает счетчик блокировок.</summary>
  public void Unlock_DocCell_TextChanged()
  {
    if (this.lock_DocCell_TextChanged_Count <= 0)
      return;
    --this.lock_DocCell_TextChanged_Count;
  }

  /// <summary>Обработчик cell_TextChanged заблокирован</summary>
  public bool DocCell_TextChanged_IsLocked => this.lock_DocCell_TextChanged_Count > 0;

  /// <summary>Спецификация отображается в табличном виде</summary>
  public bool IsGridViewMode
  {
    [DebuggerStepThrough] get => this.ViewMode == AVSViewMode.Grid;
  }

  /// <summary>Возвращает true, если в документе есть Титульный лист</summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public bool HasTitlePage
  {
    get => !AVSDocument.FindTitlePages((ImDocumentData) this.document).IsNullOrEmpty<PageData>();
  }

  public static List<PageData> FindTitlePages(ImDocumentData doc)
  {
    List<PageData> titlePages = new List<PageData>();
    if (doc == null)
      return titlePages;
    foreach (PageData pageData in doc)
    {
      if (pageData.IsTitlePage)
        titlePages.Add(pageData);
      else
        break;
    }
    return titlePages;
  }

  /// <summary>Перегенерировать табличный вид</summary>
  public void RecreateTreeListNodes()
  {
    if (!this.IsGridViewMode)
      return;
    if (this.avsWindow.virtualTree != null)
      this.avsWindow.virtualTree.UpdateData((IVirtualTreeItem) this);
    if (this.avsWindow == null || this.avsWindow.virtualTree == null)
      return;
    this.avsWindow.LockTreeList();
    try
    {
      this.avsWindow.virtualTree.SaveExpanded();
      this.UpdateViewNodes(false, true, false, false, false, EmptyRowUpdateMode.DontChange);
      this.avsWindow.ExpandTreeListNodes();
    }
    finally
    {
      this.avsWindow.UnlockTreeList();
    }
  }

  /// <summary>Очистить табличный вид</summary>
  public void ClearTreeListNodes()
  {
    if (!this.IsGridViewMode)
      return;
    this.avsWindow.LockTreeList();
    try
    {
      this.avsWindow.virtualTree.SaveExpanded();
      this.avsWindow.virtualTree.ClearAll();
    }
    finally
    {
      this.avsWindow.UnlockTreeList();
    }
  }

  /// <summary>Обновить заголовки разделов спецификации</summary>
  public void UpdateSpecificationSectionsCaptions()
  {
    for (int index = 0; index < this.rootChapters.Count; ++index)
    {
      foreach (Chapter allChapter in this.rootChapters[index].GetAllChapters())
      {
        if (allChapter is SpecificationSection specificationSection)
        {
          specificationSection.UpdateCaption();
          specificationSection.UpdateChapterCaption();
        }
      }
    }
  }

  /// <summary>Обновить заголовки исполнений в переменных данных</summary>
  public void UpdateVariableDataCaptions(bool hideSameData = false)
  {
    if (((ImDocumentData) this.document).IsDistributing || this.AvsDocumentForm != AVSDocumentForm.A)
      return;
    if (this.variableDataChapter_FormA != null)
      this.UpdateVariableDataCaptions(this.variableDataChapter_FormA, hideSameData);
    for (int index1 = 0; index1 < this.rootChapters.Count; ++index1)
    {
      if (this.rootChapters[index1].IsAdditionalChapter)
      {
        for (int index2 = 0; index2 < this.rootChapters[index1].Chapters.Count; ++index2)
        {
          if (this.rootChapters[index1].Chapters[index2] is VariableDataChapterFormA chapter)
            this.UpdateVariableDataCaptions(chapter, hideSameData);
        }
      }
    }
  }

  /// <summary>Обновить заголовки исполнений в переменных данных</summary>
  /// <param name="varDataChapter">Раздел переменных данных формы А</param>
  public void UpdateVariableDataCaptions(VariableDataChapterFormA varDataChapter, bool hideSameData = false)
  {
    if (this.AvsDocumentForm != AVSDocumentForm.A || varDataChapter == null)
      return;
    varDataChapter.UpdateSameProductChapters(hideSameData);
    varDataChapter.UpdateChapterCaption();
    for (int index = 0; index < varDataChapter.Chapters.Count; ++index)
      varDataChapter.Chapters[index].UpdateChapterCaption();
  }

  /// <summary>Сравнение списка строк</summary>
  /// <param name="list1"></param>
  /// <param name="list2"></param>
  /// <returns></returns>
  private bool CompareSpecificationRowsObjId(List<AVSRow> list1, List<AVSRow> list2)
  {
    if (list1.Count != list2.Count || list1.Count == 0)
      return false;
    for (int index = 0; index < list1.Count; ++index)
    {
      if (list1[index].ObjectId != list2[index].ObjectId)
        return false;
    }
    return true;
  }

  /// <summary>Найти по имени шаблон примечания</summary>
  /// <param name="templateName">Имя шаблона примечания</param>
  /// <returns></returns>
  public TableData FindNoteTemplateByName(string templateName)
  {
    for (int index = 0; index < this.NotesTemplates.Count; ++index)
    {
      if (this.NotesTemplates[index].Name == templateName)
        return this.NotesTemplates[index];
    }
    return (TableData) null;
  }

  /// <summary>Найти по имени шаблон примечания для переменных данных формы В</summary>
  /// <param name="templateName">Имя шаблона примечания</param>
  /// <returns></returns>
  public TableData FindNoteTemplateByName_VarDataFormV(string templateName)
  {
    if (this.avsDocTableFormBForV_Template != null)
    {
      for (int index = 0; index < this.avsDocTableFormBForV_Template.Nodes.Count; ++index)
      {
        if (this.avsDocTableFormBForV_Template.Nodes[index] is TableData node && AVSDocument.IsNoteRowDocNode((DocumentTreeNode) node) && node.Name == templateName)
          return node;
      }
      if (this.document.Template.FindNode("Таблица Спецификация. ГОСТ 2.113-75. Форма 1") is TableData node1)
      {
        for (int index = 0; index < node1.Nodes.Count; ++index)
        {
          if (node1.Nodes[index] is TableData node && AVSDocument.IsNoteRowDocNode((DocumentTreeNode) node) && node.Name == templateName)
            return node;
        }
      }
    }
    return (TableData) null;
  }

  /// <summary>Найти шаблоны разделов и записей</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="throwException"></param>
  /// <returns>Возвращает null если шаблон правильный. Возвращает список ненайденных элементов шаблона.
  /// false, если шаблон не подходит для текущего типа документа</returns>
  public string FindAllTemplates(ImDocumentData template, bool throwException)
  {
    if (template == null)
      throw new ArgumentNullException("imDocument_template_Vyvod");
    string message1 = (string) null;
    List<string> stringList = new List<string>();
    TableData child1 = (TableData) null;
    TableData tableData1 = (TableData) null;
    TableData section1 = (TableData) null;
    TableData child2 = (TableData) null;
    TableData child3 = (TableData) null;
    TableData child4 = (TableData) null;
    TableData originalAvsRow1 = (TableData) null;
    TableData tableData2 = (TableData) null;
    TableData child5 = (TableData) null;
    pageData2 = (PageData) null;
    docTable = (TableData) null;
    TableData docTableExpMix = (TableData) null;
    TableData docTableExpSingle = (TableData) null;
    TableData docTableExpMixP1 = (TableData) null;
    TableData docTableExpSingleP2 = (TableData) null;
    TableData docTableSingleT1 = (TableData) null;
    TableData docTableSingleP2 = (TableData) null;
    TableData docTableMixP1 = (TableData) null;
    tableData20 = (TableData) null;
    tableData21 = (TableData) null;
    PageData pageData1 = (PageData) null;
    TableData tableData3 = (TableData) null;
    List<TableData> tableDataList = new List<TableData>();
    TableData tableData4 = (TableData) null;
    TableData tableData5 = (TableData) null;
    TableData tableData6 = (TableData) null;
    TableData tableData7 = (TableData) null;
    TableData tableData8 = (TableData) null;
    TableData tableData9 = (TableData) null;
    TableData tableData10 = (TableData) null;
    TableData tableData11 = (TableData) null;
    TextData textData1 = (TextData) null;
    TextData textData2 = (TextData) null;
    TableData tableData12 = (TableData) null;
    TextData textData3 = (TextData) null;
    TableData tableData13 = (TableData) null;
    TableData tableData14 = (TableData) null;
    TableData tableData15 = (TableData) null;
    TableData tableData16 = (TableData) null;
    TableData tableData17 = (TableData) null;
    textData4 = (TextData) null;
    textData5 = (TextData) null;
    textData6 = (TextData) null;
    textData7 = (TextData) null;
    bool flag = false;
    TableData section2;
    TableData child6;
    TableData tableData18;
    TableData tableData19;
    TableData originalAvsRow2;
    if (this.IsSpecification)
    {
      if (AvsConfig.General.PatchStampReferences && this.AVSDocType != AVSDocumentType.ExportSpecification)
        AVSDocument.PatchDocumentAttr(template, this.documentTemplateGuid);
      if (!(template.FindNode("Таблица Спецификация") is TableData docTable))
        docTable = (TableData) template.FindNode("Главная таблица");
      if (docTable == null)
        stringList.Add("Таблица Спецификация");
      if (docTable != null)
      {
        if (template.FindNode("Заголовок спецификации")?.FindFirstNodeByName("Шапка с обозначением") is TextData firstNodeByName1)
          this.productNumbersTitle = firstNodeByName1.Text;
      }
      else
        this.productNumbersTitle = "Кол. на исполн.";
      if (!(template.FindNode("Таблица Спецификация. Продолжение 2") is TableData tableData20))
        tableData20 = template.FindNode("Главная таблица. Продолжение 2") as TableData;
      if (tableData20 == null)
      {
        if (this.IsFormB)
          stringList.Add("Таблица Спецификация. Продолжение 2");
        tableData20 = template.FindNode("Таблица Спецификация. Продолжение") as TableData;
      }
      else
        flag = true;
      if (tableData20 == null)
        tableData20 = template.FindNode("Таблица Спецификация. Продолжение 1") as TableData;
      if (tableData20 == null)
        tableData20 = docTable;
      if (!(template.FindNode("Таблица Спецификация. Продолжение 2") is TableData tableData21))
        tableData21 = template.FindNode("Таблица Спецификация. ГОСТ 2.113-75. Форма 1") as TableData;
      if (tableData21 == null && this.AvsDocumentForm == AVSDocumentForm.V)
        stringList.Add("Таблица Спецификация. ГОСТ 2.113-75. Форма 1");
      pageData1 = template.FindNode("ГОСТ 2.113-75. Форма 1") as PageData;
      section2 = (TableData) template.FindNode("Раздел спецификации");
      if (section2 == null)
        stringList.Add("Раздел спецификации");
      child2 = (TableData) template.FindNode("Часть без заголовка");
      if (child2 == null && section2 != null)
      {
        child2 = (TableData) section2.Clone(true, true);
        child2.Clear(false, false);
        child2.Name = "Часть без заголовка";
        child2.Id = "Часть без заголовка";
        RectangleF bounds = child2.Bounds with
        {
          Height = child2.DefaultRowSize
        };
        child2.AssignBounds(bounds, false, false, false);
        if (section2.ParentCell != null)
          section2.ParentCell.AddChildNode((DocumentTreeNode) child2, false, false);
        else
          docTable?.AddChildNode((DocumentTreeNode) child2, false, false);
      }
      child1 = (TableData) template.FindNode(AVSDocument.AdditionalComplectRowGroupTemplateId);
      if (child1 == null && section2 != null)
      {
        child1 = (TableData) section2.Clone(true, true);
        child1.Name = AVSDocument.AdditionalComplectRowGroupTemplateId;
        child1.Id = AVSDocument.AdditionalComplectRowGroupTemplateId;
        if (child1.FindFirstNodeByName("Заголовок раздела") is TableData firstNodeByName2 && firstNodeByName2.FindFirstNodeByName("Обозначение") is TextData firstNodeByName3 && firstNodeByName2.FindFirstNodeByName(AVSRow.DocAttr_Name) is TextData firstNodeByName4)
        {
          ReferenceBase referenceBase = firstNodeByName4.ReferenceToTextSource.Clone();
          firstNodeByName3.AssignReferenceToTextSource(referenceBase, false, false, false);
          firstNodeByName4.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
          firstNodeByName4.Text = "";
          ParagraphFormat paragraphFormat = firstNodeByName3.ParagraphFormat.Clone();
          paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Left);
          firstNodeByName3.SetParagraphFormat(paragraphFormat, false, false);
          CharFormat charFormat = firstNodeByName3.CharFormat.Clone();
          charFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
          firstNodeByName3.SetCharFormat(charFormat, false, false);
        }
        if (section2.ParentCell != null)
          section2.ParentCell.AddChildNode((DocumentTreeNode) child1, false, false);
        else
          docTable?.AddChildNode((DocumentTreeNode) child1, false, false);
      }
      section1 = (TableData) template.FindNode("Раздел спецификации. Форма Б");
      if (section1 == null && this.AvsDocumentForm == AVSDocumentForm.V)
        stringList.Add("Раздел спецификации. Форма Б");
      child4 = (TableData) template.FindNode(AVSDocument.ChapterWithoutHeaderFormBTemplateId);
      if (child4 == null && section1 != null && section2 != null)
      {
        child4 = (TableData) section1.Clone(true, true);
        child4.Clear(false, false);
        child4.Name = AVSDocument.ChapterWithoutHeaderFormBTemplateId;
        child4.Id = AVSDocument.ChapterWithoutHeaderFormBTemplateId;
        RectangleF bounds = child2.Bounds with
        {
          Height = child4.DefaultRowSize
        };
        child4.AssignBounds(bounds, false, false, false);
        if (section2.ParentCell != null)
          section2.ParentCell.AddChildNode((DocumentTreeNode) child4, false, false);
        else
          docTable?.AddChildNode((DocumentTreeNode) child4, false, false);
      }
      child6 = (TableData) template.FindNode("Общие данные");
      if (child6 == null && section2 != null)
      {
        child6 = (TableData) section2.Clone(true, true);
        child6.Clear(false, false);
        if (this.AvsDocumentForm == AVSDocumentForm.A)
          child6.Name = "Общие данные";
        else
          child6.Name = "Исполнения";
        if (section2.ParentCell != null)
          section2.ParentCell.AddChildNode((DocumentTreeNode) child6, false, false);
        else
          docTable?.AddChildNode((DocumentTreeNode) child6, false, false);
      }
      if (!(template.FindNode("Следующий блок исполнений") is PageData pageData2))
        pageData2 = template.FindNode("ГОСТ 2.113-75 Форма 1в") as PageData;
      else
        flag = true;
      if (this.IsFormB && pageData2 == null)
        stringList.Add("Следующий блок исполнений");
      else
        flag |= pageData2 != null;
      if (pageData2 == null)
        pageData2 = template.FindNode("Лист продолжения") as PageData;
      if (pageData2 == null)
        pageData2 = template.FindNode("ГОСТ 2.113-75 Форма 1а") as PageData;
      tableData4 = template.FindNode("Номера исполнений") as TableData;
      if (this.IsFormB && tableData4 == null)
        stringList.Add("Номера исполнений");
      else
        flag |= tableData4 != null;
      tableData5 = template.FindNode("Номера исполнений #2") as TableData;
      tableData6 = template.FindNode("Номера исполнений #3") as TableData;
      textData1 = template.FindNode("Размещение исполнений") as TextData;
      textData3 = template.FindNode("Размещение исполнений #2") as TextData;
      textData2 = template.FindNode("Размещение исполнений в переменных данных") as TextData;
      tableData12 = template.FindNode("Содержание переменных данных") as TableData;
      tableData7 = template.FindNode("Коды и литеры исполнений") as TableData;
      tableData8 = template.FindNode("Коды и литеры исполнений #2") as TableData;
      tableData9 = template.FindNode("Коды ОКП исполнений") as TableData;
      tableData18 = template.FindNode("Переменные данные") as TableData;
      if (this.AvsDocumentForm == AVSDocumentForm.A && tableData18 == null)
        stringList.Add("Переменные данные");
      if (tableData18 == null)
        tableData18 = section2;
      tableData19 = (TableData) template.FindNode("Исполнение") ?? section2;
      originalAvsRow2 = AVSDocument.FindAvsDocRow(template);
      if (originalAvsRow2 == null)
        stringList.Add("Строка спецификации");
      else
        AVSDocument.SetupDynamicHeaderTemplate(template, section2, originalAvsRow2, "Заголовок группы записей");
      originalAvsRow1 = (TableData) template.FindNode("Строка спецификации. Форма Б");
      if (this.AvsDocumentForm == AVSDocumentForm.V)
      {
        if (originalAvsRow1 == null)
          stringList.Add("Строка спецификации. Форма Б");
        else
          AVSDocument.SetupDynamicHeaderTemplate(template, section1, originalAvsRow1, "Заголовок группы записей формы Б");
      }
      if (docTable != null)
      {
        for (int index = 0; index < docTable.Nodes.Count; ++index)
        {
          if (docTable.Nodes[index] is TableData node && AVSDocument.IsNoteRowDocNode((DocumentTreeNode) node))
          {
            if (node.Name == "Запись примечание")
              tableData13 = node;
            else if (node.Name == "Запись примечание 2")
              tableData14 = node;
            tableDataList.Add(node);
          }
        }
      }
      if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
      {
        if (!(template.FindNode("ОКП лев.") is TextData textData4))
          stringList.Add("ОКП лев.");
        if (!(template.FindNode("ОКП прав.") is TextData textData5))
          stringList.Add("ОКП лев.");
        if (!(template.FindNode("Обозначение лев.") is TextData textData6))
          stringList.Add("Обозначение лев.");
        if (!(template.FindNode("Обозначение прав.") is TextData textData7))
          stringList.Add("Обозначение прав.");
      }
      if (this.AVSDocType == AVSDocumentType.ExportSpecification)
      {
        docTableExpMix = (TableData) template.FindNode("EXP.MIX.T1");
        docTableExpSingle = (TableData) template.FindNode("EXP.T1");
        docTableExpMixP1 = (TableData) template.FindNode("EXP.MIX.Р1");
        docTableExpSingleP2 = (TableData) template.FindNode("EXP.P2");
        docTableSingleT1 = (TableData) template.FindNode("SP.T1");
        docTableSingleP2 = (TableData) template.FindNode("SP.P2");
        docTableMixP1 = (TableData) template.FindNode("SP.MIX.P1");
        tableData1 = (TableData) template.FindNode("Раздел спецификации. EXP");
        if (tableData1 == null)
          stringList.Add("Раздел спецификации. EXP");
        child5 = (TableData) template.FindNode("Общие данные. EXP");
        if (child5 == null && tableData1 != null)
        {
          child5 = (TableData) tableData1.Clone(true, true);
          child5.Clear(false, false);
          if (this.AvsDocumentForm == AVSDocumentForm.A)
            child5.Name = "Общие данные";
          else
            child5.Name = "Исполнения";
          if (tableData1.ParentCell != null)
            tableData1.ParentCell.AddChildNode((DocumentTreeNode) child5, false, false);
          else
            docTableExpMix?.AddChildNode((DocumentTreeNode) child5, false, false);
        }
        child3 = (TableData) template.FindNode("Часть без заголовка. EXP");
        if (child3 == null && tableData1 != null)
        {
          child3 = (TableData) tableData1.Clone(true, true);
          child3.Clear(false, false);
          child3.Name = "Часть без заголовка";
          child3.Id = "Часть без заголовка. EXP";
          RectangleF bounds = child3.Bounds with
          {
            Height = child3.DefaultRowSize
          };
          child3.AssignBounds(bounds, false, false, false);
          if (tableData1.ParentCell != null)
            tableData1.ParentCell.AddChildNode((DocumentTreeNode) child3, false, false);
          else
            docTableExpMix?.AddChildNode((DocumentTreeNode) child3, false, false);
        }
        tableData10 = template.FindNode("Переменные данные. EXP") as TableData;
        if (this.AvsDocumentForm == AVSDocumentForm.A && tableData10 == null)
          stringList.Add("Переменные данные. EXP");
        if (tableData10 == null)
          tableData10 = tableData1;
        tableData11 = (TableData) template.FindNode("Исполнение. EXP") ?? tableData1;
        tableData2 = (TableData) template.FindNode("Строка спецификации. EXP");
        if (tableData2 == null && docTableExpMix != null)
          tableData2 = (TableData) docTableExpMix.FindNode("Запись. EXP");
        if (tableData2 == null)
          stringList.Add("Строка спецификации. EXP");
      }
    }
    else
    {
      if (AvsConfig.General.PatchStampReferences && this.AVSDocType == AVSDocumentType.ElementList)
        AVSDocument.PatchDocumentAttr(template, this.documentTemplateGuid);
      this.FindMainTablesInDocument(template, out docTable, out TableData _, out docTableExpMix, out docTableExpSingle, out docTableExpMixP1, out docTableExpSingleP2, out docTableSingleT1, out docTableSingleP2, out docTableMixP1, out this.lriPage);
      if (docTable == null)
        stringList.Add("Главная таблица");
      section2 = ((TableData) template.FindNode("Раздел документа") ?? (TableData) template.FindNode("Раздел перечня элементов")) ?? (TableData) template.FindNode("Раздел спецификации");
      if (section2 == null && this.IsElementList)
        stringList.Add("Раздел документа");
      originalAvsRow2 = ((TableData) template.FindNode("Запись") ?? (TableData) template.FindNode("Строка ведомости")) ?? (TableData) template.FindNode("Строка спецификации");
      if (originalAvsRow2 == null)
      {
        stringList.Add("Запись");
      }
      else
      {
        AVSDocument.SetupDynamicHeaderTemplate(template, section2, originalAvsRow2, "Заголовок группы записей");
        AVSDocument.SetupFunctionalGroupHeaderTemplate(template, section2, originalAvsRow2, "Заголовок функциональной группы");
      }
      tableData17 = (TableData) template.FindNode("Заголовок функциональной группы");
      child6 = (TableData) template.FindNode("Общие данные");
      if (child6 == null && section2 != null)
      {
        child6 = (TableData) section2.Clone(true, true);
        child6.Clear(false, false);
        if (this.AvsDocumentForm == AVSDocumentForm.A)
          child6.Name = "Общие данные";
        else
          child6.Name = "Исполнения";
        if (section2.ParentCell != null)
          section2.ParentCell.AddChildNode((DocumentTreeNode) child6, false, false);
        else
          docTable?.AddChildNode((DocumentTreeNode) child6, false, false);
      }
      tableData18 = (TableData) template.FindNode("Переменные данные") ?? section2;
      tableData19 = (TableData) template.FindNode("Исполнение") ?? section2;
      if (docTable != null)
      {
        for (int index = 0; index < docTable.Nodes.Count; ++index)
        {
          if (docTable.Nodes[index] is TableData node && AVSDocument.IsNoteRowDocNode((DocumentTreeNode) node))
          {
            if (node.Name == "Запись примечание")
              tableData13 = node;
            else if (node.Name == "Запись примечание 2")
              tableData14 = node;
            tableDataList.Add(node);
          }
        }
      }
    }
    PageData node1 = template.FindNode("Лист регистрации изменений") as PageData;
    TableData node2 = (TableData) template.FindNode("Таблица изменений");
    if (node2 != null)
    {
      for (int index = 0; tableData3 == null && index < node2.NodesCount; ++index)
      {
        if (node2.Nodes[index].GetAttributeValue(Chapter.DocNodeType_AttributeName, true) == Chapter.LRIRow_TypeName)
          tableData3 = node2.Nodes[index] as TableData;
      }
      if (tableData3 == null && node2.NodesCount > 0)
        tableData3 = node2.Nodes[0] as TableData;
    }
    string message2 = (string) null;
    if (!this.IsFormB && this.AvsDocumentForm != AVSDocumentForm.V & flag && this.avsDocumentType != AVSDocumentType.AutoIndustrySpecification)
    {
      string str = (string) null;
      if (template.Reference is ReferenceToDBObjectBase reference)
      {
        str = reference.DBObjectCaption;
        if ((str == null || str == "") && reference.IsEmptyObjectRef)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            str = sessionKeeper.Session.GetObject(reference.DBObjectGuid).Caption;
        }
      }
      if (str == null || str == "")
        str = template.GetDefautCaption();
      message2 = $"Шаблон документа \"{str}\" предназначен для групповых документов формы \"Б\" и не подходит для \"Единичных\" и групповых формы \"А\"";
      if (throwException)
        throw new Exception(message2);
    }
    if (stringList.Count > 0 || message2 != null)
    {
      if (stringList.Count > 0)
      {
        string str = (string) null;
        if (template.Reference is ReferenceToDBObjectBase reference)
        {
          str = reference.DBObjectCaption;
          if ((str == null || str == "") && reference.IsEmptyObjectRef)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              str = sessionKeeper.Session.GetObject(reference.DBObjectGuid).Caption;
          }
        }
        if (str == null || str == "")
          str = template.GetDefautCaption();
        if (stringList.Count == 1)
          stringList.Insert(0, $"В шаблоне документа \"{str}\" не найден элемент: ");
        else
          stringList.Insert(0, $"В шаблоне документа \"{str}\" не найдены элементы: ");
        stringList.Add("Возможно, шаблон предназначен для документов другого типа или другой групповой формы.");
      }
      if (message2 != null)
      {
        stringList.Insert(0, message2);
        if (stringList.Count > 1)
          stringList.Insert(1, " ");
      }
      for (int index = 0; index < stringList.Count; ++index)
        message1 = index != 0 ? $"{message1}\r\n{stringList[index]}" : stringList[index];
      if (throwException)
        throw new Exception(message1);
    }
    else
    {
      this.addiitionalComplectRowGroupTemplate = child1;
      this.sectionTemplate = section2;
      this.sectionExpTemplate = tableData1;
      this.sectionFormBTemplate = section1;
      this.chapterWithoutHeaderTemplate = child2;
      this.chapterWithoutHeaderExpTemplate = child3;
      this.chapterWithoutHeaderFormBTemplate = child4;
      this.avsRowTemplate = originalAvsRow2;
      this.avsRowFormBTemplate = originalAvsRow1;
      this.avsRowExpTemplate = tableData2;
      this.commonChapterTemplate = child6;
      this.commonChapterExpTemplate = child5;
      this.productsPage2Template = pageData2;
      this.avsDocTableTemplate = docTable;
      this.avsDocTableExpMix_Template = docTableExpMix;
      this.avsDocTableExpSingle_Template = docTableExpSingle;
      this.avsDocTableExpMixP1_Template = docTableExpMixP1;
      this.avsDocTableExpSingleP2_Template = docTableExpSingleP2;
      this.avsDocTableSingleT1_Template = docTableSingleT1;
      this.avsDocTableSingleP2_Template = docTableSingleP2;
      this.avsDocTableMixP1_Template = docTableMixP1;
      this.avsDocTableFormBMore10_Template = tableData20;
      this.avsDocTableFormBForV_Template = tableData21;
      this.titlePageFormBForV_Template = pageData1;
      this.lriPage_Template = node1;
      this.lriTableTemplate = node2;
      this.lriRowTemplate = tableData3;
      this.NotesTemplates = tableDataList;
      this.productNumbersTemplate = tableData4;
      this.productNumbers2Template = tableData5;
      this.productNumbers3Template = tableData6;
      this.productKodAndLiteraTemplate = tableData7;
      this.productKodAndLitera2Template = tableData8;
      this.productKodOKPTemplate = tableData9;
      this.variableDataChapterTemplate = tableData18;
      this.variableDataChapterExpTemplate = tableData10;
      this.productPageLinksTemplate = textData1;
      this.productPageLinksFormVTemplate = textData2;
      this.productPageLinksFormVTemplate_Table = tableData12;
      this.productPage2LinksTemplate = textData3;
      this.productVariableDataChapterTemplate = tableData19;
      this.productVariableDataChapterExpTemplate = tableData11;
      this.note1Template = tableData13;
      this.note2Template = tableData14;
      this.additionalNote1Template = tableData15;
      this.additionalNote2Template = tableData16;
      this.functionalGroupHeaderTemplate = tableData17;
      this.leftProductKodOKPTemplate = textData4;
      this.rightProductKodOKPTemplate = textData5;
      this.leftProductDesignationTemplate = textData6;
      this.rightProductDesignationTemplate = textData7;
      if (this.document != null)
      {
        this.document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
        this.document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, this.avsDocTypeGuid.ToString(), false, false, false);
        if (this.documentTemplateGuid != Guid.Empty)
          this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
        else
          this.document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
      }
    }
    if (template.NeedUpdateLayoutFlag)
      template.UpdateLayout(0, false, false);
    if (this.skipLinesSchema == null)
      this.GetSkipLinesSchema();
    if (this.skipLinesSchema != null)
      template.defaultNonSkipAtStartPage = new bool?(this.skipLinesSchema.NonSkipBeforeAtStartPage);
    else if (!template.defaultNonSkipAtStartPage.HasValue)
      template.defaultNonSkipAtStartPage = new bool?(true);
    this.UpdateOutputAttributeMappingSettings();
    return message1;
  }

  internal static void PatchDynamicHeaderInTemplate(ImDocumentData template)
  {
    if (template == null)
      return;
    TableData avsDocRow = AVSDocument.FindAvsDocRow(template);
    TableData parentCell1 = avsDocRow?.ParentCell;
    AVSDocument.SetupDynamicHeaderTemplate(template, parentCell1, avsDocRow, "Заголовок группы записей");
    if (!(template.FindNode("Строка спецификации. Форма Б") is TableData node) || node == avsDocRow)
      return;
    TableData parentCell2 = node.ParentCell;
    AVSDocument.SetupDynamicHeaderTemplate(template, parentCell2, node, "Заголовок группы записей формы Б");
  }

  private static void SetupDynamicHeaderTemplate(
    ImDocumentData template,
    TableData section,
    TableData originalAvsRow,
    string templateRowId)
  {
    if (section == null || originalAvsRow == null)
      return;
    if (!(template.FindNode(templateRowId) is TableData))
    {
      TableData child = (TableData) originalAvsRow.Clone();
      child.Id = templateRowId;
      child.Name = "Заголовок группы записей";
      child.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.SpecNote_TypeName, false, false, false);
      child.AssignReference((ReferenceBase) null, false, false);
      if (child.FindFirstNodeByName(AVSRow.DocAttr_Name) is TextData firstNodeByName)
      {
        firstNodeByName.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) firstNodeByName, BaseReferenceNodeType.ntParentNode, "", "GroupHeaderText"), false, false, false);
        CharFormat charFormat = firstNodeByName.CharFormat.Clone();
        charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
        firstNodeByName.SetCharFormat(charFormat, false, false);
      }
      foreach (PageElementNode pageElementNode in child.CellsEnumerator)
        pageElementNode.AssignReadOnly(true);
      section.AddChildNode((DocumentTreeNode) child, false, false);
    }
    section.SetAttributeValue("GroupHeaderTemplate", templateRowId, false, false, false);
  }

  internal static void PatchFunctionalGroupHeaderTemplate(ImDocumentData template)
  {
    if (template == null)
      return;
    TableData avsDocRow = AVSDocument.FindAvsDocRow(template);
    TableData parentCell = avsDocRow?.ParentCell;
    AVSDocument.SetupFunctionalGroupHeaderTemplate(template, parentCell, avsDocRow, "Заголовок функциональной группы");
  }

  private static void SetupFunctionalGroupHeaderTemplate(
    ImDocumentData template,
    TableData section,
    TableData originalAvsRow,
    string templateRowId)
  {
    if (section == null || originalAvsRow == null)
      return;
    if (!(template.FindNode(templateRowId) is TableElement child))
    {
      child = (TableElement) originalAvsRow.Clone();
      child.Id = templateRowId;
      child.Name = "Заголовок функциональной группы";
      child.AssignReference((ReferenceBase) null, false, false);
      child.SetSkipCellsBefore(0.0f, true, false, false);
      child.SetSkipCellsAfter(1f, true, false, false);
      if (child.FindFirstNodeByName(AVSRow.DocAttr_Name) is TextData firstNodeByName)
      {
        CharFormat charFormat = firstNodeByName.CharFormat.Clone();
        charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
        firstNodeByName.SetCharFormat(charFormat, false, false);
        ParagraphFormat paragraphFormat = firstNodeByName.ParagraphFormat.Clone();
        paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
        firstNodeByName.SetParagraphFormat(paragraphFormat, false, false);
        firstNodeByName.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
      }
      section.AddChildNode((DocumentTreeNode) child, false, false);
    }
    child.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.SpecNote_TypeName, false, false, false);
  }

  /// <summary>Найти запись AVS в документе. Используется для поиска шаблона записи</summary>
  /// <param name="document">Документ</param>
  /// <returns></returns>
  internal static TableData FindAvsDocRow(ImDocumentData document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (!(document.FindNode("Строка спецификации") is TableData node))
      node = document.FindNode("Запись") as TableData;
    return node;
  }

  /// <summary>Записи содержат один и тот же объект</summary>
  /// <param name="row1">Запись 1</param>
  /// <param name="row2">Запись 2</param>
  /// <returns></returns>
  internal virtual bool IsRowsWithOneObject(AvsRowData row1, AvsRowData row2)
  {
    if (row1.HasObject)
      return row1.ObjectID == row2.ObjectID;
    string fieldStringValue1 = row1.GetFieldStringValue(this.Field_Name, false);
    string fieldStringValue2 = row2.GetFieldStringValue(this.Field_Name, false);
    return fieldStringValue1 != "" && fieldStringValue2 != "" && fieldStringValue1 == fieldStringValue2;
  }

  /// <summary>
  /// Должна ли скрываться связь и не отображаться в документе
  /// </summary>
  /// <param name="relations"></param>
  /// <returns></returns>
  internal bool IsHiddenRowRelation(RelationAttributeValuesCache relation)
  {
    if (relation == null)
      throw new ArgumentNullException(nameof (relation));
    if (this.IsSpecification)
      return relation.GetValueBool(this.Attr_HideInSpecification, false);
    if (!this.IsElementList)
      return false;
    return relation.RelationType == AvsIDCache.Relation_Podbor || !relation.GetValueBool(this.Attr_IncludeInElementList, false);
  }

  /// <summary>Выводить символ «*» рядом с Позиционным обозначением основного компонента</summary>
  internal virtual bool InsertStarAfterPositionDesignation => false;

  /// <summary>Можно суммировать две записи.
  /// Объединить их в одну запись со слиянием в одну связь или без слияния, группировкой в одну запись нескольких связей</summary>
  /// <param name="row1">Запись 1</param>
  /// <param name="row2">Запись 2</param>
  /// <param name="noteCellMapping">Настройки вывода в графу Примечание</param>
  /// <returns></returns>
  internal virtual bool CanSummThisRelations(
    AvsRowData rowData1,
    AvsRowData rowData2,
    CellOutputMapping noteCellMapping)
  {
    if (rowData1.RelationType != rowData2.RelationType || !this.IsRowsWithOneObject(rowData1, rowData2) || this.IsSpecification && rowData1.SectionID != rowData2.SectionID || rowData1.GetFieldInt64Value(this.Attr_DopZamenGroupNum, false, 0L) != 0L || rowData2.GetFieldInt64Value(this.Attr_DopZamenGroupNum, false, 0L) != 0L)
      return false;
    string fieldStringValue1 = rowData1.GetFieldStringValue(this.Field_PosDesignation, false);
    string fieldStringValue2 = rowData2.GetFieldStringValue(this.Field_PosDesignation, false);
    return (AVSDocument.IsMergeRelationsWithoutPosDesignation(fieldStringValue1, fieldStringValue2) || !string.IsNullOrEmpty(fieldStringValue1) && !string.IsNullOrEmpty(fieldStringValue2)) && this.CanSummThisRelationsForNotes(rowData1, rowData2, noteCellMapping);
  }

  /// <summary>Позиционное обозначение пустое и можно объединять связи с пустым позиционным обозначением</summary>
  /// <param name="posDesignations1"></param>
  /// <param name="posDesignations2"></param>
  /// <returns></returns>
  internal static bool IsMergeRelationsWithoutPosDesignation(
    string posDesignations1,
    string posDesignations2)
  {
    return string.IsNullOrEmpty(posDesignations1) && string.IsNullOrEmpty(posDesignations2) && !AvsConfig.General.DisableMergeRelationsWithoutPosDesignation;
  }

  /// <summary>
  /// Проверить что все атрибуты связей в примечаниях совпадают, кроме поз.обозначения и допзамен
  /// </summary>
  /// <returns></returns>
  private bool CanSummThisRelationsForNotes(
    AvsRowData rowData1,
    AvsRowData rowData2,
    CellOutputMapping noteCellMapping)
  {
    IEnumerable<AvsRowAttributeInfo> rowAttributeInfos = noteCellMapping?.Attributes ?? this.noteFieldSettings.Items.Select<RemarkAttribute, AvsRowAttributeInfo>((System.Func<RemarkAttribute, AvsRowAttributeInfo>) (ra => ra.CreateRowAttrInfo()));
    if (rowAttributeInfos == null)
      return true;
    foreach (AvsRowAttributeInfo attr in rowAttributeInfos)
    {
      if (attr.IsRelationAttribute && attr.AttributeId != this.Field_PosDesignation.AttributeId && attr.AttributeId != AvsIDCache.Attr_DopZamenText && rowData1.GetFieldStringValue(attr, false) != rowData2.GetFieldStringValue(attr, false))
        return false;
    }
    return true;
  }

  /// <summary>Можно ли сливать связи в записях в одну связь</summary>
  /// <param name="relation1">Связь 1</param>
  /// <param name="relation2">Связь 2</param>
  /// <returns></returns>
  internal bool CanMergeRelationsInSummRows(
    RelationAttributeValuesCache relation1,
    RelationAttributeValuesCache relation2)
  {
    return this.CanMergeRelationsInSummRows(new AvsRowData((AttributeValuesCache) relation1), new AvsRowData((AttributeValuesCache) relation2));
  }

  /// <summary>Можно ли сливать связи в записях в одну связь</summary>
  /// <param name="relation1">Связь 1</param>
  /// <param name="relation2">Связь 2</param>
  /// <returns></returns>
  internal virtual bool CanMergeRelationsInSummRows(AvsRowData relation1, AvsRowData relation2)
  {
    return false;
  }

  internal static bool IsContinuousSequencePosDesignation(
    string posDesignations1,
    string posDesignations2)
  {
    return !string.IsNullOrEmpty(posDesignations1) && !string.IsNullOrEmpty(posDesignations2) && AVSDocument.IsContinuousSequencePosDesignation(PosDesignationRecord.ParsePositionalDesignation(posDesignations1).LastOrDefault<PosDesignationRecord>(), PosDesignationRecord.ParsePositionalDesignation(posDesignations2).FirstOrDefault<PosDesignationRecord>());
  }

  internal static bool IsContinuousSequencePosDesignation(
    PosDesignationRecord posDesignations1,
    PosDesignationRecord posDesignations2)
  {
    return posDesignations1 != null && posDesignations1.Number.HasValue && posDesignations2 != null && posDesignations2.Number.HasValue && posDesignations2.Number.Value - posDesignations1.Number.Value <= 1L;
  }

  /// <summary>Суммировать Позиционное обозначение в перечне элементов</summary>
  public virtual void SumPositionalDesignation()
  {
    this.SuspendDocumentAndGridUpdates();
    List<AvsRowData> rows = new List<AvsRowData>();
    Dictionary<string, List<AvsRowData>> dictionary = new Dictionary<string, List<AvsRowData>>();
    try
    {
      foreach (AVSRow allRow in this.GetAllRows(false, false))
      {
        if (!allRow.IsHiddenRow)
        {
          if (allRow.HasAnyRelations)
          {
            foreach (RelationAttributeValuesCache allRelation in allRow.GetAllRelations())
            {
              AvsRowData rowData2 = new AvsRowData(allRow, (AttributeValuesCache) allRelation);
              if (rows.Count > 0 && (!this.CanSummThisRelations(rows[0], rowData2, allRow.NoteCellMapping) || rows[0].AvsRow.GetParentProductChapter() != allRow.GetParentProductChapter()))
              {
                this.SummRowsWithPositionDesignation(rows);
                rows.Clear();
              }
              rows.Add(rowData2);
            }
          }
          else if (this.IsElementList)
          {
            AvsRowData avsRowData = new AvsRowData(allRow);
            if (string.IsNullOrWhiteSpace(avsRowData.GetFieldStringValue(this.Field_Name, false)))
            {
              string key = allRow.DocNode.GetAttributeValue(allRow.Field_Note.Name, false);
              if (string.IsNullOrWhiteSpace(key))
                key = avsRowData.GetFieldStringValue(allRow.Field_Note, false);
              if (dictionary.ContainsKey(key))
                dictionary[key].Add(avsRowData);
              else
                dictionary[key] = new List<AvsRowData>()
                {
                  avsRowData
                };
            }
          }
        }
      }
      if (rows.Count > 1)
        this.SummRowsWithPositionDesignation(rows);
      foreach (string key in dictionary.Keys)
      {
        if (dictionary[key].Count > 1)
          this.SummObjectlessRowsWithNote(dictionary[key]);
      }
    }
    catch (KernelExceptionID ex)
    {
      if (ex.ErrorID != 222)
        return;
      this.SetFocusTo(rows[0].AvsRow, this.Field_Count);
      this.AVSWindow.ErrorsUserControl.AddError((ImErrorMessage) new AVSRowErrorMessage(rows[0].AvsRow, new SpecRowCheckMessage(AVSCheckType.All, ex.Message)));
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, this.document.NeedUpdateLayoutFlag, true, true);
    }
  }

  /// <summary>
  /// Суммировать сгруппированные по примечанию записи без связей (для ПЭ)
  /// </summary>
  private void SummObjectlessRowsWithNote(List<AvsRowData> list)
  {
    if (list.Count < 2)
      return;
    AvsRowData avsRowData = (AvsRowData) null;
    MeasuredValue count1 = (MeasuredValue) null;
    List<string> posDesignations = new List<string>();
    for (int index = 0; index < list.Count; ++index)
    {
      object fieldValue = list[index].GetFieldValue(this.Field_Count, false);
      if (!(fieldValue is MeasuredValue count2))
        count2 = AVSRow.ConvertCountToMeasuredValue(fieldValue);
      if (count2 != null)
      {
        string fieldStringValue = list[index].GetFieldStringValue(this.Field_PosDesignation, false);
        posDesignations.Add(fieldStringValue);
        if (avsRowData == null)
        {
          avsRowData = list[index];
          count1 = count2;
        }
        else
        {
          count1 = this.SummCountValues(count1, count2);
          list[index].AvsRow.Remove();
        }
      }
    }
    if (avsRowData == null)
      return;
    string str = PosDesignationHelper.Summ(posDesignations);
    if (count1 != null)
      avsRowData.SetFieldValue(this.Field_Count, (object) count1, true, false, true, this.IsGridViewMode, false, false);
    avsRowData.SetFieldValue(this.Field_PosDesignation, (object) str, true, false, true, this.IsGridViewMode, false, false);
  }

  /// <summary>Переместить фокус в ячейку записи</summary>
  /// <param name="row">Запись в которую переместить фокус</param>
  /// <param name="attribute">Атрибут ячейки записи</param>
  internal void SetFocusTo(AVSRow row, AvsRowAttributeInfo attribute, int productIndex = -1)
  {
    if (this.ViewMode == AVSViewMode.Page)
    {
      TextData cellForAttribute = row.GetDocumentCellForAttribute(attribute, productIndex);
      if (cellForAttribute != null)
        this.DocumentControl.SetSelection((DocumentTreeNode) cellForAttribute, true, Point.Empty, true, false);
      else
        this.DocumentControl.SetSelection((DocumentTreeNode) row.DocNode, true, false);
    }
    else
    {
      AVSWindow avsWindow = this.AVSWindow;
      List<AVSRow> selectedSpecRows = new List<AVSRow>();
      selectedSpecRows.Add(row);
      AVSRow focusedSpecRow = row;
      avsWindow.RestoreSelection(selectedSpecRows, focusedSpecRow);
      this.AVSWindow.virtualTree.SaveSelection();
    }
  }

  /// <summary>Суммировать записи объединяя позиционное обозначение и количество</summary>
  /// <param name="rows">Список связей с одним объектом</param>
  internal void SummRowsWithPositionDesignation(List<AvsRowData> rows)
  {
    if (this.IsFormB)
      throw new Exception("Операция суммирования записей не реализована для групповой формы Б");
    if (rows == null)
      throw new ArgumentNullException(nameof (rows));
    if (rows.Count < 2)
      return;
    rows.Sort((IComparer<AvsRowData>) new AVSRowDataComparer(new SectionSortSchema(new AttributeSortSchema[2]
    {
      new AttributeSortSchema(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00034-306c-11d8-b4e9-00304f19f545"), -21, "Идентификатор версии родительского объекта")),
      new AttributeSortSchema(this.Field_PosDesignation)
    })));
    List<AVSRow> rows1 = new List<AVSRow>();
    List<AvsRowData> avsRowDataList = new List<AvsRowData>()
    {
      rows[0]
    };
    for (int index = 1; index < rows.Count; ++index)
    {
      if (!this.CanMergeRelationsInSummRows(avsRowDataList.Last<AvsRowData>(), rows[index]))
      {
        AVSRow avsRow = this.MergeRowsRelations(avsRowDataList);
        if (!rows1.Contains(avsRow))
          rows1.Add(avsRow);
        avsRowDataList.Clear();
      }
      avsRowDataList.Add(rows[index]);
    }
    if (avsRowDataList.Count > 0)
    {
      AVSRow avsRow = this.MergeRowsRelations(avsRowDataList);
      if (!rows1.Contains(avsRow))
        rows1.Add(avsRow);
    }
    this.CollectRowsRelations(rows1);
  }

  /// <summary>Объединить связи из всех записей в одну запись.
  /// Лишние записи удаляются.
  /// </summary>
  /// <param name="rows">Список записей</param>
  /// <returns></returns>
  internal AVSRow CollectRowsRelations(List<AVSRow> rows)
  {
    if (rows == null)
      throw new ArgumentNullException(nameof (rows));
    if (rows.Count == 0)
      return (AVSRow) null;
    if (rows.Count == 1)
      return rows[0];
    for (int index1 = 1; index1 < rows.Count; ++index1)
    {
      if (rows[index1].HasRelation)
      {
        for (int index2 = rows[index1].Relations.Count - 1; index2 >= 0; --index2)
        {
          RelationAttributeValuesCache relation = rows[index1].Relations[index2];
          rows[index1].RemoveRelationData(rows[index1].Relations, index2);
          rows[0].AddRowData(relation, addToHidden: true);
          if (this.IsSpecification && relation.SortIndex != rows[0].SortIndex)
            rows[0].SetFieldValue(this.Attr_SortIndex, rows[0].HiddenRelations.Count - 1, -1, rows[0].HiddenRelations, (object) rows[0].SortIndex, this.IsSpecification, false, false, false, false, false, false, true);
        }
      }
      if (rows[index1].HasHiddenRelation)
      {
        for (int index3 = rows[index1].HiddenRelations.Count - 1; index3 >= 0; --index3)
        {
          RelationAttributeValuesCache hiddenRelation = rows[index1].HiddenRelations[index3];
          rows[index1].RemoveRelationData(rows[index1].HiddenRelations, index3);
          rows[0].AddRowData(hiddenRelation, addToHidden: true);
          if (this.IsSpecification && hiddenRelation.SortIndex != rows[0].SortIndex)
            rows[0].SetFieldValue(this.Attr_SortIndex, rows[0].HiddenRelations.Count - 1, -1, rows[0].HiddenRelations, (object) rows[0].SortIndex, this.IsSpecification, false, false, false, false, false, false, true);
        }
      }
      rows[index1].Remove(removeRelation: false);
    }
    this.RegisterAVSRowInDictionaries(rows[0]);
    this.UpdateNoteDocCells(false, false);
    return rows[0];
  }

  /// <summary>Слить все дублирующиеся связи заданных записей в одну связь.
  /// Позиционное обозначение и количество суммируются.
  /// Старые записи удаляются</summary>
  /// <param name="rows"></param>
  /// <returns></returns>
  internal AVSRow MergeRowsRelations(List<AvsRowData> rows)
  {
    if (this.IsFormB)
      throw new Exception("Операция суммирования записей не реализована для групповой формы Б");
    if (rows == null)
      throw new ArgumentNullException(nameof (rows));
    if (rows.Count == 0)
      return (AVSRow) null;
    if (rows.Count < 2)
      return rows[0].AvsRow;
    List<string> posDesignations = new List<string>();
    AvsRowData avsRowData = rows.Find((Predicate<AvsRowData>) (x => x.AvsRow.HasRelation && ((IEnumerable<AttributeValuesCache>) x.AvsRow.Relations).Contains<AttributeValuesCache>(x.AttributeValues))) ?? rows[0];
    MeasuredValue count1 = avsRowData.GetFieldValue(avsRowData.AvsRow.Field_Count, false) as MeasuredValue;
    for (int index = 0; index < rows.Count; ++index)
    {
      AvsRowData row = rows[index];
      posDesignations.Add(rows[index].GetFieldStringValue(this.Field_PosDesignation, false));
      RelationAttributeValuesCache attributeValues = row.AttributeValues as RelationAttributeValuesCache;
      if (avsRowData != row && (avsRowData.AvsRow != row.AvsRow || !row.AvsRow.Relations.Contains(attributeValues)))
      {
        MeasuredValue fieldValue = row.GetFieldValue(rows[index].AvsRow.Field_Count, false) as MeasuredValue;
        count1 = this.SummCountValues(count1, fieldValue);
        row.AvsRow.RemoveRelationData(attributeValues, true);
      }
      if (!row.AvsRow.HasRelation && !row.AvsRow.HasHiddenRelation)
        row.AvsRow.Remove();
    }
    string str = PosDesignationHelper.Summ(posDesignations);
    if (count1 != null)
      avsRowData.SetFieldValue(this.Field_Count, (object) count1, true, false, true, this.IsGridViewMode, false, false);
    avsRowData.SetFieldValue(this.Field_PosDesignation, (object) str, true, false, true, this.IsGridViewMode, false, false);
    return rows[0].AvsRow;
  }

  /// <summary>Суммировать количество. null принимается за 0</summary>
  /// <returns></returns>
  /// <param name="count1">Количество 1</param>
  /// <param name="count2">Количество 2</param>
  /// <returns></returns>
  internal MeasuredValue SummCountValues(MeasuredValue count1, MeasuredValue count2)
  {
    if (count1 == null)
      return count2;
    if (count2 == null)
      return count1;
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(count1.Caption, false);
    MeasuredValue mValue = MeasureHelper.Add(count1, count2);
    if (measuredValue != null)
      mValue = MeasureHelper.ConvertToMeasuredValue(mValue, measuredValue.MeasureID);
    return mValue;
  }

  /// <summary>Часть "Общие данные"</summary>
  [Browsable(false)]
  internal Chapter CommonDataChapter
  {
    [DebuggerStepThrough] get => this.commonDataChapter;
    set
    {
      if (this.commonDataChapter == value)
        return;
      if (this.commonDataChapter != null)
        this.rootChapters.Remove(this.commonDataChapter);
      this.commonDataChapter = value;
      if (this.commonDataChapter == null)
        return;
      this.rootChapters.Insert(0, this.commonDataChapter);
    }
  }

  /// <summary>Часть "Переменные данные исполнений" для формы А</summary>
  [Browsable(false)]
  internal VariableDataChapterFormA VariableDataChapter_FormA
  {
    [DebuggerStepThrough] get => this.variableDataChapter_FormA;
    set
    {
      if (this.variableDataChapter_FormA == value)
        return;
      if (this.variableDataChapter_FormA != null)
        this.rootChapters.Remove((Chapter) this.variableDataChapter_FormA);
      this.variableDataChapter_FormA = value;
      if (this.variableDataChapter_FormA == null)
        return;
      if (this.rootChapters.Count > 0)
        this.rootChapters.Insert(1, (Chapter) this.variableDataChapter_FormA);
      else
        this.rootChapters.Insert(0, (Chapter) this.variableDataChapter_FormA);
    }
  }

  /// <summary>Часть "Переменные данные формы В"</summary>
  [Browsable(false)]
  internal VariableDataChapterFormV VariableDataChapter_FormV
  {
    [DebuggerStepThrough] get => this.variableDataChapter_FormV;
    set
    {
      if (this.variableDataChapter_FormV == value)
        return;
      if (this.variableDataChapter_FormV != null)
        this.rootChapters.Remove((Chapter) this.variableDataChapter_FormV);
      this.variableDataChapter_FormV = value;
      if (this.variableDataChapter_FormV == null)
        return;
      if (this.rootChapters.Count > 0)
        this.rootChapters.Insert(1, (Chapter) this.variableDataChapter_FormV);
      else
        this.rootChapters.Insert(0, (Chapter) this.variableDataChapter_FormV);
    }
  }

  /// <summary>Часть "Переменные данные исполнений" для формы А и В</summary>
  [Browsable(false)]
  public Chapter VariableDataChapter
  {
    [DebuggerStepThrough] get
    {
      if (this.AvsDocumentForm == AVSDocumentForm.A)
        return (Chapter) this.variableDataChapter_FormA;
      return this.AvsDocumentForm == AVSDocumentForm.V ? (Chapter) this.variableDataChapter_FormV : (Chapter) null;
    }
  }

  /// <summary> Добавить Титульный лист </summary>
  public virtual void InsertTitlePage()
  {
    ImDocument document = this.AVSWindow?.AVSDocument?.Document;
    if (document == null)
      return;
    ImDocumentData documentTemplate = document.DocumentTemplate;
    PageData child1 = AVSDocument.ExtractTitlePage(documentTemplate);
    if (child1 == null)
    {
      if (child1 == null && this.DocumentTemplateID.IsDefinedId())
        child1 = AVSDocument.ExtractTitlePage((ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID));
      if (child1 == null && AVSSpecification.ObjID_StdTemplateSpecificationTitlePage.IsDefinedId())
        child1 = AVSDocument.ExtractTitlePage((ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(AVSSpecification.ObjID_StdTemplateSpecificationTitlePage));
      if (child1 != null)
      {
        child1 = child1.Clone() as PageData;
        documentTemplate.InsertChildNode(0, (DocumentTreeNode) child1, true, true, true, true, false);
      }
    }
    if (child1 == null)
      return;
    DocumentTreeNode child2 = child1.CloneFromTemplate();
    document.InsertChildNode(0, child2, true, true, true, true, false);
  }

  /// <summary> Удалить Титульный лист </summary>
  public virtual void DeleteTitlePage(PageData titlePage)
  {
    if (titlePage == null || !titlePage.IsTitlePage)
      return;
    titlePage.RemovePageWithDataFlow(false, true);
  }

  private static PageData ExtractTitlePage(ImDocumentData baseDocTemplate)
  {
    List<PageData> titlePages = AVSDocument.FindTitlePages(baseDocTemplate);
    return titlePages == null ? (PageData) null : titlePages.FirstOrDefault<PageData>();
  }

  /// <summary>Загрузить данные для документа из БД</summary>
  /// <param name="args"></param>
  internal void LoadAVSDocumentFromDB(OpenAVSDocArgs args)
  {
    LogManager.AddLine("AVS. Start loading document: " + (object) args.ObjectId);
    this.AvsDocumentNowLoading = true;
    try
    {
      AVSDocumentContext loadContext = new AVSDocumentContext(true, (SpecificationSection) null, this.GetAllowableDocumentSections());
      this.Document = (ImDocument) null;
      this.ReadOnly = args.ReadOnly;
      if (this.versionAttributesHelper == null)
        this.LoadVersionAttributesHelper();
      this.productAttributeList = new List<int>();
      if (this.versionAttributesHelper.Items != null)
      {
        for (int index = 0; index < this.versionAttributesHelper.Items.Count; ++index)
          this.productAttributeList.Add(this.versionAttributesHelper.Items[index].ID);
      }
      List<ProductInfo> freeProducts = new List<ProductInfo>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (args.ObjectType == -1)
          args.ObjectType = sessionKeeper.Session.GetObjectInfo(args.ObjectId).ObjectTypeID;
        this.articleGroupID = Guid.Empty;
        List<ProductInfo> productInfoList1 = new List<ProductInfo>();
        Dictionary<long, long> dictionary1 = new Dictionary<long, long>();
        Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
        if (AvsIDCache.IsSpecification(args.ObjectType))
        {
          this.DocumentID = args.ObjectId;
          this.DocumentDBObjectType = args.ObjectType;
          AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(this.DocumentDBObjectType, AVSDocument.GetDefaultSpecificationType());
          this.avsDocumentType = typeForDbObjectType.AVSDocType;
          this.avsDocTypeGuid = typeForDbObjectType.TypeGuid;
          this.CreateRowAttrsInfo();
          IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.DocumentID, true);
          this.DocumentID = objectActual.ObjectID;
          this.DocFID = objectActual.ID;
          this.GetDocumentAttributes(objectActual, false);
          List<ProductInfo> productInfoList2 = AVSDocument.LoadProductsForAVSDocument(this.DocumentID, this.productAttributeList, false, this.FiltrationOwnerID, sessionKeeper.Session);
          if (productInfoList2.Count == 0)
            throw new Exception($"Не найдено специфицированное изделие!\r\nПроверьте связи с изделиями у документа \"{objectActual.Caption}\" [{this.DocumentID}]");
          this.articleGroupID = Guid.Empty;
          List<ProductInfo> productInfoList3 = new List<ProductInfo>();
          List<string> stringList = new List<string>();
          Dictionary<long, ProductInfo> dictionary3 = new Dictionary<long, ProductInfo>();
          for (int index = 0; index < productInfoList2.Count; ++index)
          {
            if (!dictionary3.ContainsKey(productInfoList2[index].Id))
            {
              bool flag = false;
              if (this.productId.IsUndefinedId())
              {
                this.productId = productInfoList2[0].Id;
                this.productType = productInfoList2[0].ObjectType;
              }
              if (productInfoList2[index].DocumentId.IsUndefinedId())
                stringList.Add($"Связь между изделием '{sessionKeeper.Session.GetObjectInfo(productInfoList2[index].Id).Caption}' [{productInfoList2[index].Id}] и его документом '{objectActual.Caption}' [{this.DocumentID}], не содержит атрибута 'Идентификатор версии в составе'!");
              if (productInfoList2[index].ArticleGroupID != Guid.Empty)
              {
                if (this.articleGroupID == Guid.Empty)
                {
                  this.articleGroupID = productInfoList2[index].ArticleGroupID;
                  this.productId = productInfoList2[index].Id;
                }
                else if (productInfoList2[index].ArticleGroupID != this.articleGroupID)
                {
                  productInfoList3.Add(productInfoList2[index]);
                  flag = true;
                }
              }
              else if (productInfoList2.Count > 1)
              {
                productInfoList3.Add(productInfoList2[index]);
                flag = true;
              }
              if (!flag)
              {
                if (!dictionary2.ContainsKey(productInfoList2[index].F_ID))
                  dictionary2.Add(productInfoList2[index].F_ID, productInfoList2[index].Id);
                else
                  stringList.Add($"Исполнения, связанные со спецификацией, являются версиями одного объекта!\r\nИдентификатор версии объекта: {productInfoList2[index].Id} и {dictionary2[productInfoList2[index].F_ID]}\r\nИдентификатор объекта {productInfoList2[index].F_ID}\r\n");
                dictionary3.Add(productInfoList2[index].Id, productInfoList2[index]);
              }
            }
          }
          if (stringList.Any<string>())
            throw new Exception(string.Join(Environment.NewLine, (IEnumerable<string>) stringList));
          if (productInfoList3.Count > 0)
          {
            if (this.productId.IsDefinedId())
            {
              for (int index = productInfoList3.Count - 1; index >= 0; --index)
              {
                if (productInfoList3[index].ArticleGroupID == Guid.Empty)
                {
                  if (!this.ReadOnly)
                    sessionKeeper.Session.GetRelation(productInfoList3[index].Id, this.DocumentID, true).Delete(0L);
                  productInfoList2.Remove(productInfoList3[index]);
                  productInfoList3.RemoveAt(index);
                }
              }
            }
            if (productInfoList3.Count > 0)
            {
              string str = "Одна спецификация связана с изделиями, имеющими разные либо пустые идентификаторы группового изделия:";
              for (int index = 0; index < productInfoList3.Count; ++index)
                str += $", \r\nОбъект \"{sessionKeeper.Session.GetObjectInfo(productInfoList3[index].Id).Caption}\", идентификатор версии объекта: {productInfoList3[index].Id}, идентификатор группового изделия: {productInfoList3[index].ArticleGroupID}";
              throw new Exception(str + ".\r\n Исправьте применяемость документа.");
            }
          }
          this.productsInfo = this.LoadProductsByGroupID(this.ProductId, this.productAttributeList, (string) null, sessionKeeper.Session);
          for (int index = 0; index < this.productsInfo.Count; ++index)
          {
            if (!dictionary2.ContainsKey(this.productsInfo[index].F_ID))
              dictionary2.Add(this.productsInfo[index].F_ID, this.productsInfo[index].Id);
            else if (dictionary2[this.productsInfo[index].F_ID] != this.productsInfo[index].Id)
              stringList.Add($"Версии исполнений, имеющих один идентификатор группового изделия, являются версиями одного объекта!\r\nИдентификаторы версий объекта: {this.productsInfo[index].Id} и {dictionary2[this.productsInfo[index].F_ID]}\r\nИдентификатор объекта {this.productsInfo[index].F_ID}\r\n");
            if (!dictionary3.ContainsKey(this.productsInfo[index].Id))
              freeProducts.Add(this.productsInfo[index]);
            else
              dictionary3.Remove(this.productsInfo[index].Id);
          }
          if (stringList.Any<string>())
            throw new Exception(string.Join(Environment.NewLine, (IEnumerable<string>) stringList));
          foreach (KeyValuePair<long, ProductInfo> keyValuePair in dictionary3)
          {
            if (!this.ReadOnly && this.articleGroupID != Guid.Empty && keyValuePair.Value.ArticleGroupID != this.articleGroupID)
              sessionKeeper.Session.GetRelation(keyValuePair.Value.Id, this.DocumentID, AvsIDCache.Relation_Document, true)?.Delete(0L);
          }
        }
        else if (!MetaDataHelper.IsObjectTypeChildOf(args.ObjectType, AvsIDCache.ObjType_Document))
        {
          this.avsDocumentType = AVSDocument.GetDefaultSpecificationType();
          this.avsDocTypeGuid = AVSDocumentTypeSettings.GetStdDocTypeGuid(this.avsDocumentType);
          this.DocumentDBObjectType = AvsIDCache.ObjType_Specification;
          this.ProductId = args.ObjectId;
          this.productType = args.ObjectType;
          this.CreateRowAttrsInfo();
          this.productsInfo = this.LoadProductsByGroupID(args.ObjectId, this.productAttributeList, (string) null, sessionKeeper.Session);
          List<long> productsWithoutVersionInRelation;
          Dictionary<long, long> relationsForProducts = this.FindDocRelationsForProducts(this.ProductIds, "", sessionKeeper.Session, out productsWithoutVersionInRelation);
          if (this.DocumentID.IsDefinedId())
          {
            List<string> stringList = new List<string>();
            foreach (long objectID in productsWithoutVersionInRelation)
              stringList.Add($"Связь между изделием '{sessionKeeper.Session.GetObjectInfo(objectID).Caption}' [{objectID}] и его документом '{this.DocumentCaption}' [{this.DocumentID}], не содержит атрибута 'Идентификатор версии в составе'!");
            List<long> longList = new List<long>();
            foreach (KeyValuePair<long, long> keyValuePair in relationsForProducts)
            {
              if (keyValuePair.Value != this.DocumentID)
                longList.Add(keyValuePair.Value);
            }
            if (longList.Count > 0)
            {
              string str1 = this.articleGroupID.ToString();
              long documentId = this.DocumentID;
              string str2 = documentId.ToString();
              string message = $"Недопустимые связи между спецификациями и исполнениями! Изделия с 'Идентификатором группового изделия' {str1} связаны со спецификациями имеющими следующие идентификаторы версий: {str2}";
              for (int index = 0; index < longList.Count; ++index)
              {
                string str3 = message;
                documentId = longList[index];
                string str4 = documentId.ToString();
                message = $"{str3}, {str4}";
              }
              throw new Exception(message);
            }
            for (int index = 0; index < this.productsInfo.Count; ++index)
            {
              if (!dictionary2.ContainsKey(this.productsInfo[index].F_ID))
                dictionary2.Add(this.productsInfo[index].F_ID, this.productsInfo[index].Id);
              else if (dictionary2[this.productsInfo[index].F_ID] != this.productsInfo[index].Id)
                stringList.Add($"Исполнения, имеющие один идентификатор группового изделия, являются версиями одного объекта!\r\nИдентификаторы версий объекта: {this.productsInfo[index].Id} и {dictionary2[this.productsInfo[index].F_ID]}\r\nИдентификатор объекта {this.productsInfo[index].F_ID}\r\n");
              if (relationsForProducts.ContainsKey(this.productsInfo[index].Id))
                relationsForProducts.Remove(this.productsInfo[index].Id);
              else
                freeProducts.Add(this.productsInfo[index]);
            }
            if (stringList.Any<string>())
              throw new Exception(string.Join(Environment.NewLine, (IEnumerable<string>) stringList));
            List<ProductInfo> productInfoList4 = AVSDocument.LoadProductsForAVSDocument(this.DocumentID, (List<int>) null, true, this.FiltrationOwnerID, sessionKeeper.Session);
            for (int index = 0; index < productInfoList4.Count; ++index)
            {
              if (!this.ReadOnly && args.ObjectId != productInfoList4[index].Id && (this.articleGroupID == Guid.Empty || productInfoList4[index].ArticleGroupID != this.articleGroupID))
              {
                if (productInfoList4[index].ArticleGroupID == Guid.Empty)
                {
                  sessionKeeper.Session.GetRelation(productInfoList4[index].Id, this.DocumentID, true).Delete(0L);
                  continue;
                }
                stringList.Add($"Изделие связанное со спецификацией не является исполнением изделия на которое выпущена спецификация! Удалите спецификацию из состава чужих объектов\r\nОбъект \"{sessionKeeper.Session.GetObjectInfo(productInfoList4[index].Id).Caption}\"\r\n Идентификатор версии объекта: {productInfoList4[index].Id}\r\n");
              }
              if (!dictionary2.ContainsKey(productInfoList4[index].F_ID))
                dictionary2.Add(productInfoList4[index].F_ID, productInfoList4[index].Id);
              else if (dictionary2[productInfoList4[index].F_ID] != productInfoList4[index].Id)
                stringList.Add($"Изделия, связанные с одной спецификацией, являются версиями одного объекта!\r\nИдентификаторы версий объекта: {productInfoList4[index].Id} и {dictionary2[productInfoList4[index].F_ID]}\r\nИдентификатор объекта: {productInfoList4[index].F_ID}\r\nИдентификатор версии спецификации: {this.DocumentID}\r\n");
            }
            if (stringList.Any<string>())
              throw new Exception(string.Join(Environment.NewLine, (IEnumerable<string>) stringList));
          }
          else if (this.productsInfo.Count > 1)
          {
            if (this.AvsDocumentForm == AVSDocumentForm.Single)
              this.AvsDocumentForm = this.GetDefaultGroupDocumentForm();
          }
        }
        else
        {
          this.DocumentID = AvsIDCache.IsNotSpecificationDoc(args.ObjectType) ? args.ObjectId : throw new Exception("Объекты данного типа не поддерживаются AVS");
          this.DocumentDBObjectType = args.ObjectType;
          AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(this.DocumentDBObjectType, AVSDocument.GetDefaultSpecificationType());
          this.avsDocumentType = typeForDbObjectType != null ? typeForDbObjectType.AVSDocType : throw new Exception("Для данного типа документов не назначен шаблон");
          this.avsDocTypeGuid = typeForDbObjectType.TypeGuid;
          IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.DocumentID, true);
          this.DocumentID = objectActual.ObjectID;
          this.DocFID = objectActual.ID;
          this.CreateRowAttrsInfo();
          this.GetDocumentAttributes(objectActual, false);
          this.productsInfo = new List<ProductInfo>();
        }
      }
      if (this.ReadOnly && this.DocumentID.IsUndefinedId())
      {
        this.DontOpenDocument = true;
        args.ErrorMessage = "Документ не найден!";
      }
      else
      {
        if (!this.ReadOnly)
        {
          this.ReadOnly = !this.CheckOutObjects(out this.DontOpenDocument);
          if (this.DontOpenDocument)
            return;
          this.CheckAndUpdateRelationsWithDocument(freeProducts);
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (DocumentTypeWeightHelper.items == null)
            DocumentTypeWeightHelper.LoadSystemCollection(sessionKeeper.Session);
        }
        ImDocument document = this.Document;
        AVSDocumentForm? groupForm_FromAttr;
        bool hasOldSpFile;
        loadContext.IsNewDocument = this.LoadImDocument(!this.ReadOnly, out groupForm_FromAttr, out hasOldSpFile);
        if (!AVSDocumentsSettings.IsAllowableDocumentForm(this.AVSDocType, this.AvsDocumentForm) || this.productsInfo.Count > 1 && this.AvsDocumentForm == AVSDocumentForm.Single)
        {
          this.AvsDocumentForm = this.GetDefaultGroupDocumentForm();
          this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
        }
        if (!this.ReadOnly && this.productsInfo.Count == 1 && this.IsSpecification && this.articleGroupID == Guid.Empty && this.AvsDocumentForm != AVSDocumentForm.Single)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[0].Id);
            this.articleGroupID = Guid.NewGuid();
            AttributeValues[] valuesList = new AttributeValues[1]
            {
              new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.articleGroupID)
            };
            dbObject.SetAttributesValues(valuesList);
          }
        }
        this.SortDocumentProducts();
        if (loadContext.IsNewDocument && this.IsSpecification)
          loadContext.IsOldSpConverting = hasOldSpFile;
        if (!args.CreateUndo.HasValue && loadContext.IsNewDocument)
          args.CreateUndo = new bool?(false);
        this.isGeneratedDoc = loadContext.IsNewDocument && this.ReadOnly;
        this.UpdateProductAttrs();
        this.CommonDataChapter = this.CreateCommonDataChapter(!this.IsSpecification);
        if (this.DocumentTemplateID == -1L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            this.DocumentTemplateID = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, loadContext.IsNewDocument);
        }
        this.GetUserAttributesForFieldNameFromSettings();
        if (this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA == null)
          this.VariableDataChapter_FormA = new VariableDataChapterFormA(this, this.productsInfo, true);
        else if (this.AvsDocumentForm == AVSDocumentForm.V && this.variableDataChapter_FormV == null)
          this.VariableDataChapter_FormV = new VariableDataChapterFormV(this);
        this.Document.DBAttributeProcessorDictionary = (object) this.attributeProcessorDictionary;
        this.Document.DBAttributeAutoSave = true;
        if (this.noteFieldSettings == null)
          this.LoadNoteFieldSettings();
        if (this.versionAttributesHelper == null || this.versionAttributesHelper.Items == null)
        {
          this.LoadVersionAttributesHelper();
          this.UpdateProductsByGroupID();
        }
        if (!this.ReadOnly || loadContext.IsNewDocument)
        {
          this.LoadAVSDocumentData(loadContext);
          this.RemoveDocumentTypeSuffixFromProductDesignations();
          if (!this.ReadOnly && groupForm_FromAttr.HasValue)
          {
            AVSDocumentForm? nullable = groupForm_FromAttr;
            AVSDocumentForm avsDocumentForm1 = this.AvsDocumentForm;
            if (!(nullable.GetValueOrDefault() == avsDocumentForm1 & nullable.HasValue))
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                nullable = groupForm_FromAttr;
                AVSDocumentForm avsDocumentForm2 = AVSDocumentForm.Single;
                if (nullable.GetValueOrDefault() == avsDocumentForm2 & nullable.HasValue && this.productsInfo.Count > 1)
                {
                  if (!this.ReadOnly)
                    AvsIDCache.GetDBAVSDocumentObject(sessionKeeper.Session, this.DocumentID).SetAttributesValues(new AttributeValues[1]
                    {
                      new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) this.EncodeSpecificationFormAttrValue(this.AvsDocumentForm))
                    }, true);
                  this.UpdateViewNodes(false, false, false, true, true, EmptyRowUpdateMode.DontChange);
                }
                else if (AVSDocumentsSettings.IsAllowableDocumentForm(this.AVSDocType, groupForm_FromAttr.Value) && MessageBox.Show(string.Format("Не совпадают значение атрибута \"Форма спецификации\" ({0}) и реальной формы спецификации в документе ({1})!\r\nПреобразовать форму спецификации в {0}?", (object) this.EncodeSpecificationFormAttrValue(groupForm_FromAttr.Value), (object) this.EncodeSpecificationFormAttrValue(this.AvsDocumentForm)), "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                  this.ChangeGroupDocumentForm(groupForm_FromAttr.Value);
                else if (!this.ReadOnly)
                  AvsIDCache.GetDBAVSDocumentObject(sessionKeeper.Session, this.DocumentID).SetAttributesValues(new AttributeValues[1]
                  {
                    new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) this.EncodeSpecificationFormAttrValue(this.AvsDocumentForm))
                  }, true);
              }
            }
          }
        }
        else
        {
          if (this.ReadOnly && AvsConfig.General.UpdateModeInReadOnly == UpdateModeInReadOnlyEnum.Part)
            this.UpdateReadOnlyDocument();
          DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.Document, true, true, false, false, false);
        }
        if (!this.ReadOnly && (!args.CreateUndo.HasValue || args.CreateUndo.Value) && this.IsSpecification)
          this.CreateUndoSnapshot(args.CreateUndo);
        if (loadContext.IsNewDocument)
          this.ReloadFormatAttributeInEntireSpecificationFromDB();
        this.CheckSortIndexWarning(true);
      }
    }
    finally
    {
      if (this.document != null && this.document.NeedUpdateLayoutFlag)
        this.document.UpdateLayout(false, false);
      this.AvsDocumentNowLoading = false;
      LogManager.AddLine("AVS. End loading document: " + (object) args.ObjectId);
      LogManager.CloseFile();
    }
  }

  protected virtual void RemoveDocumentTypeSuffixFromProductDesignations()
  {
  }

  /// <summary>Обновление документа при открытии на просмотр</summary>
  public void UpdateReadOnlyDocument()
  {
    this.ScanDocumentStructure(new RowDictionariesForLoadDocument(), new List<ProductInfo>());
    if (this.AvsDocumentForm == AVSDocumentForm.A && !ProductVariableDataChapter.SameLiters(this) && this.VariableDataChapter_FormA != null && (this.VariableDataChapter_FormA.Chapters.Count == 0 || this.VariableDataChapter_FormA.Chapters[0].DocNode == null))
      this.variableDataChapter_FormA.UpdateViewNodes(this.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
    this.UpdateDocumentRowFieldsInfo();
    this.UpdateProductHeadersOnPages(true, true);
  }

  /// <summary>Добавить недостающие связи между исполнениями и документом</summary>
  /// <param name="freeProducts"></param>
  private void CheckAndUpdateRelationsWithDocument(List<ProductInfo> freeProducts)
  {
    if (this.ReadOnly || !this.IsSpecification || this.DocumentID.IsUndefinedId() || freeProducts.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = (IDBRelationCollection) null;
      for (int index = 0; index < freeProducts.Count; ++index)
      {
        try
        {
          if (relationCollection == null)
            relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
          relationCollection.Create(freeProducts[index].Id, this.DocumentID).SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this.DocumentID))
          });
        }
        catch (Exception ex)
        {
          if (sessionKeeper.Session.GetRelation(freeProducts[index].Id, this.DocumentID, AvsIDCache.Relation_Document, true) == null)
            throw;
          LogManager.AddLine("AVS. Ошибка при создании связи между: " + $"{freeProducts[index].Id} и {this.DocumentID}\r\n{ex.Message}\r\n{ex.StackTrace}", true);
        }
      }
    }
  }

  /// <summary>Обновить обозначения исполнений для документов с виртуальными исполнениями</summary>
  protected virtual void UpdateVirtualProductsDesignations()
  {
  }

  /// <summary>Создать документ СП, загрузить данные из системы и заполнить документ данными</summary>
  /// <param name="newDocument">Загрузка для нового документа</param>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  public void LoadAVSDocumentData(AVSDocumentContext loadContext)
  {
    if (this.DataLoaded)
      return;
    ImDocument document = this.Document;
    this.SuspendDocumentAndGridUpdates();
    this.LoadSpecificationSortSchema();
    this.FindMainTablesInDocument((ImDocumentData) this.document, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
    this.TryAddMainTableIfNeed();
    this.CheckMainDocumentTablesAndThrowException();
    RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument();
    this.UpdateDocumentRowFieldsInfo();
    this.CheckCellMappingAndLoadDefaultsIfNeed();
    List<ProductInfo> productsInDoc = new List<ProductInfo>();
    this.ScanDocumentStructure(rowDicts, productsInDoc);
    this.UpdateVirtualProductsDesignations();
    this.SortProductsByDocOrder(productsInDoc);
    this.Document.Modified = this.documentIsModifiedByLoad;
    if (loadContext == null)
      loadContext = new AVSDocumentContext(true, (SpecificationSection) null, this.GetAllowableDocumentSections());
    if (loadContext.IsNewDocument && !this.IsSpecification)
      this.CheckOldDocFormat(rowDicts.objectsFromOldFormat, rowDicts.objectTypesFromOldFormat);
    this.LoadAllProductsRelations(loadContext, rowDicts);
    this.LoadAllObjectsForRowDicts(loadContext, rowDicts);
    this.UpdateDocumentStructure(true, false, false, updateDraftForParts: true);
    this.ReloadAllDocTypeNames();
    this.UpdateAdditionalChapterBySettings();
    if (this.AutoSort && !loadContext.IsNewDocument)
      this.SortNewRows();
    this.UpdateDocumentStructure(true, false, false);
    this.RemoveEmptySectionsFromCommonData();
    if (this.AutoSort && loadContext.IsNewDocument)
      this.SortDocument();
    if (!this.ReadOnly)
      this.IndexAVSDocument(true);
    this.SynchronizeDocument();
    this.SaveDocumentDataToDB();
    this.UpdateViewNodes(false, false, true, loadContext.IsNewDocument || !this.IsSpecification || !ProductVariableDataChapter.SameLiters(this), true, EmptyRowUpdateMode.DontChange);
    if (loadContext.IsNewDocument && this.IsSpecification && this.DocumentID.IsDefinedId())
      this.CheckOldDocFormat(new List<long>(), new List<int>());
    this.UpdateSpecificationSectionsCaptions();
    this.UpdateVariableDataCaptions();
    this.UpdateProductLiteraForSP(false);
    this.UpdateRowsGroupHeaders();
    this.DataLoaded = true;
    this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
  }

  private void CheckCellMappingAndLoadDefaultsIfNeed()
  {
    OutputAttributeMappingScheme attributeMappingScheme = this.CellTextOutputAttributeMappingSettings;
    while (attributeMappingScheme.Parent != null)
      attributeMappingScheme = attributeMappingScheme.Parent;
    if (attributeMappingScheme.CellMaping.Count != 0)
      return;
    foreach (AvsRowAttributeInfo docRowField in this.docRowFields)
    {
      CellOutputMapping newCellMaping = new CellOutputMapping()
      {
        CellId = docRowField.Name
      };
      newCellMaping.Add((OutputMappingBase) new AttributeMapping(new AttributeInfo(docRowField.AttrSrc, docRowField.AttributeGuid, docRowField.AttributeId, docRowField.Name)));
      this.CellTextOutputAttributeMappingSettings.SetCellMapping(newCellMaping);
    }
  }

  internal virtual void UpdateRowsGroupHeaders()
  {
    this.UpdateDynamicGroupHeaderSettings(false, false);
  }

  /// <summary>Синхронизирует базу данных с документом</summary>
  internal virtual void SaveDocumentDataToDB()
  {
    List<AVSRow> allRows = this.GetAllRows(true, true);
    Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
    int num = 1;
    foreach (AVSRow avsRow in allRows)
    {
      string str1 = "";
      if (avsRow.DocNode != null && avsRow.DocNode.ContainsAttribute(AVSRow.RowAttr_CommonPositions))
      {
        str1 = avsRow.DocNode.GetAttributeValue(AVSRow.RowAttr_CommonPositions, false);
        avsRow.DocNode.RemoveAttribute(AVSRow.RowAttr_CommonPositions, false, false);
        avsRow.DocNode.SetAttributeValue(AVSRow.RowAttr_CommonPositionsOLD, str1, false, false, false);
      }
      if (!string.IsNullOrEmpty(str1) && avsRow.CommonPosition == null && GuidHelper.IsGuid(str1))
      {
        if (dictionary == null)
        {
          num = this.GetLastCommonPosition(allRows);
          dictionary = new Dictionary<string, string>();
        }
        string str2;
        if (!dictionary.ContainsKey(str1))
        {
          ++num;
          str2 = num.ToString();
          dictionary[str1] = str2;
        }
        else
          str2 = dictionary[str1];
        avsRow.CommonPosition = str2;
      }
    }
  }

  /// <summary>
  /// Поиск максимальной совместной позиции назначенной в документе
  /// </summary>
  /// <param name="rows">Список строк среди которых ведется поиск</param>
  /// <returns>Значение позиции</returns>
  internal int GetLastCommonPosition(List<AVSRow> rows)
  {
    int lastCommonPosition = 0;
    foreach (AVSRow row in rows)
    {
      if (!string.IsNullOrEmpty(row.CommonPosition))
      {
        string s = Regex.Match(row.CommonPosition, "\\d+").Value;
        int num = 0;
        ref int local = ref num;
        if (int.TryParse(s, out local) && num > lastCommonPosition)
          lastCommonPosition = num;
      }
    }
    return lastCommonPosition;
  }

  /// <summary>Загрузить все связи полученные от родительских изделий или исполнений</summary>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal virtual void LoadAllProductsRelations(
    AVSDocumentContext loadContext,
    RowDictionariesForLoadDocument rowDicts)
  {
    for (int index = 0; index < this.parentProducts.Count; ++index)
      this.LoadProductData(this.parentProducts[index], loadContext, rowDicts);
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      if (!Intermech.Consts.IsUndefinedObjectId(this.productsInfo[index].Id))
        this.LoadProductData(this.productsInfo[index], loadContext, rowDicts);
    }
  }

  /// <summary>Загрузить все связи заданного типа полученные от родительских изделий или исполнений</summary>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal virtual void LoadAllProductsRelationsForType(
    AVSDocumentContext loadContext,
    int relationType,
    RowDictionariesForLoadDocument rowDicts)
  {
    for (int index = 0; index < this.parentProducts.Count; ++index)
      this.LoadProductRelationsForType(this.parentProducts[index], relationType, loadContext, rowDicts);
  }

  /// <summary>Загрузить все объекты без связей, полученные из документа</summary>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal virtual void LoadAllObjectsForRowDicts(
    AVSDocumentContext loadContext,
    RowDictionariesForLoadDocument rowDicts)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (rowDicts.docRowsWithoutRelationsByObjectGuid.Count > 0)
      {
        List<Guid> objectGuids = new List<Guid>();
        foreach (KeyValuePair<Guid, List<TableData>> keyValuePair in rowDicts.docRowsWithoutRelationsByObjectGuid)
        {
          bool flag = false;
          Guid key = keyValuePair.Key;
          List<TableData> tableDataList = keyValuePair.Value;
          if (tableDataList != null && tableDataList.Count > 0)
          {
            AVSRow avsDocRow = this.GetAvsDocRow((DocumentTreeNode) tableDataList[0]);
            if (avsDocRow != null)
            {
              foreach (AVSRow avsRow in this.GetSpecRowsByObjectGuid(key))
              {
                if (avsRow != avsDocRow && avsRow.ObjectAttributesCache != null && avsRow.ObjectAttributesCache.ObjectId != -1L)
                {
                  AttributeValuesCache objectData = avsRow.ObjectAttributesCache.Clone() as AttributeValuesCache;
                  avsDocRow.AddRowData((RelationAttributeValuesCache) null, objectData);
                  flag = true;
                }
              }
            }
          }
          if (!flag)
            objectGuids.Add(keyValuePair.Key);
        }
        this.LoadRowsForDBObjects(objectGuids, loadContext, false, sessionKeeper.Session, rowDicts);
      }
      if (rowDicts.objectsFromOldFormat.Count <= 0)
        return;
      this.LoadRowsForDBObjects(rowDicts.objectsFromOldFormat, rowDicts.objectTypesFromOldFormat, (ColumnDescriptor[]) null, (ColumnDescriptor[]) null, false, loadContext, false, sessionKeeper.Session, rowDicts);
    }
  }

  /// <summary>Проверить наличие базовых таблиц в документе и выбросить исключение, если их нет</summary>
  protected virtual void CheckMainDocumentTablesAndThrowException()
  {
    if (this.avsDocTable == null && this.avsFormB_Table == null)
      throw new Exception($"Нарушена структура документа! В документе \"{this.DocumentCaption}\" не найдена основная таблица.");
  }

  protected virtual void TryAddMainTableIfNeed()
  {
    PageData pageData = (PageData) null;
    if (this.avsDocTable == null && this.avsDocTableTemplate != null)
      pageData = this.avsDocTableTemplate.Page;
    if (pageData == null)
      return;
    this.document.InsertChildNode(this.FindPageIndexAfterTitlePages(), pageData.CloneFromTemplate(), false, false, false, false, true);
    this.FindMainTablesInDocument((ImDocumentData) this.document, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
  }

  private void RemoveEmptySectionsFromCommonData()
  {
    if (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V)
      this.commonDataChapter.RemoveEmptySections(true);
    for (int index1 = 0; index1 < this.rootChapters.Count; ++index1)
    {
      if (this.rootChapters[index1].IsAdditionalChapter)
      {
        for (int index2 = 0; index2 < this.rootChapters[index1].Chapters.Count; ++index2)
        {
          if (this.rootChapters[index1].Chapters[index2].IsCommonDataChapter)
            this.rootChapters[index1].Chapters[index2].RemoveEmptySections(true);
        }
      }
    }
  }

  /// <summary>Проверить настройку размещения частей в СП и сверить её с настройкой в документе</summary>
  private void UpdateAdditionalChapterBySettings()
  {
    if (AvsConfig.General.AdditionalChaptersInDataChapter == this.additionalChaptersInDataChapter)
      return;
    bool flag = false;
    if (AvsConfig.General.AdditionalChaptersInDataChapter)
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
      {
        if (this.rootChapters[index].IsAdditionalChapter)
        {
          flag = true;
          break;
        }
      }
    }
    else
    {
      for (int index = 0; index < this.commonDataChapter.Chapters.Count; ++index)
      {
        if (this.commonDataChapter.Chapters[index].IsAdditionalChapter)
        {
          flag = true;
          break;
        }
      }
      if (this.variableDataChapter_FormA != null)
      {
        for (int index1 = 0; index1 < this.variableDataChapter_FormA.Chapters.Count; ++index1)
        {
          for (int index2 = 0; index2 < this.variableDataChapter_FormA.Chapters[index1].Chapters.Count; ++index2)
          {
            if (this.variableDataChapter_FormA.Chapters[index1].Chapters[index2].IsAdditionalChapter)
            {
              flag = true;
              break;
            }
          }
        }
      }
      if (this.variableDataChapter_FormV != null)
      {
        for (int index = 0; index < this.variableDataChapter_FormV.Chapters.Count; ++index)
        {
          if (this.variableDataChapter_FormV.Chapters[index].IsAdditionalChapter)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (flag && (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V))
    {
      if (MessageBox.Show("Расположение частей конструкторского документа не соответствует текущей настройке AVS!\r\nПереместить записи частей согласно настройке?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
        this.additionalChaptersInDataChapter = AvsConfig.General.AdditionalChaptersInDataChapter;
    }
    else
      this.additionalChaptersInDataChapter = AvsConfig.General.AdditionalChaptersInDataChapter;
    this.Document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
  }

  /// <summary>Создать документ СП, загрузить данные из системы и заполнить документ данными</summary>
  /// <param name="product">Изделие, состав которого нужно загрузить</param>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="loadedRelations">Словарь загруженных связей. Если null, то не заполняется</param>
  internal void LoadProductData(
    ProductInfo product,
    AVSDocumentContext loadContext = null,
    RowDictionariesForLoadDocument rowDicts = null,
    Dictionary<long, AVSRow> loadedRelations = null)
  {
    if (loadContext == null)
      loadContext = new AVSDocumentContext(true, (SpecificationSection) null, this.GetAllowableDocumentSections());
    if (rowDicts == null)
      rowDicts = new RowDictionariesForLoadDocument();
    foreach (int relationType in this.GetRelationTypesUsedInDocument())
    {
      if (relationType != -1)
        this.LoadProductRelationsForType(product, relationType, loadContext, rowDicts, loadedRelations);
    }
  }

  /// <summary>Создать документ СП, загрузить данные из системы и заполнить документ данными</summary>
  /// <param name="product">Изделие, состав которого нужно загрузить</param>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="loadedRelations">Словарь загруженных связей. Если null, то не заполняется</param>
  internal void LoadProductRelationsForType(
    ProductInfo product,
    int relationType,
    AVSDocumentContext loadContext = null,
    RowDictionariesForLoadDocument rowDicts = null,
    Dictionary<long, AVSRow> loadedRelations = null)
  {
    if (product == null)
      throw new ArgumentNullException(nameof (product));
    if (this.IsElementList && Intermech.Consts.IsUndefinedObjectId(product.Id))
      return;
    if (loadContext == null)
      loadContext = new AVSDocumentContext(true, (SpecificationSection) null, this.GetAllowableDocumentSections());
    if (rowDicts == null)
      rowDicts = new RowDictionariesForLoadDocument();
    loadContext.Product = product;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, this.GetAllColumnDescriptorsForSpecRow(relationType, false, this.AutoSort, true, this.IsGridViewMode, loadContext.IsOldSpConverting), recordCount: this.PacketSize);
      AVSDocument.SetFiltrationTags(ref paramSet, loadContext);
      Dictionary<long, AVSRow> addDict = loadedRelations != null || relationType == AvsIDCache.Relation_Project ? new Dictionary<long, AVSRow>() : (Dictionary<long, AVSRow>) null;
      DBRecordSetParams selectParamSet = paramSet;
      ProductInfo product1 = product;
      int relationType1 = relationType;
      List<int> objectTypes = new List<int>();
      objectTypes.Add(-1);
      AVSDocumentContext context = loadContext;
      IUserSession session = sessionKeeper.Session;
      Dictionary<long, AVSRow> loadedRelations1 = addDict;
      RowDictionariesForLoadDocument rowDicts1 = rowDicts;
      this.LoadPartsData(new LoadDataParams(selectParamSet, true, product1, relationType1, objectTypes, true, false, true, context, session, loadedRelations1, (List<AVSRow>) null, rowDicts1));
      if (relationType == AvsIDCache.Relation_Project)
        this.ReloadDopzamenTextForGroup((List<long>) null, product.Id, new List<AVSRow>((IEnumerable<AVSRow>) addDict.Values), false);
      if (loadedRelations == null)
        return;
      AVSDocument.AddDictionary<long, AVSRow>((IDictionary<long, AVSRow>) loadedRelations, (IDictionary<long, AVSRow>) addDict);
    }
  }

  /// <summary>Добавить в словарь baseDict новые значения из словаря addDict</summary>
  /// <typeparam name="TKey">Тип ключа словарей</typeparam>
  /// <typeparam name="TValue">Тип значений словарей</typeparam>
  /// <param name="baseDict">Основной словарь, в который добавляются новые значения из addDict</param>
  /// <param name="addDict">Добавляемый словарь</param>
  public static void AddDictionary<TKey, TValue>(
    IDictionary<TKey, TValue> baseDict,
    IDictionary<TKey, TValue> addDict)
  {
    foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) addDict)
    {
      if (!baseDict.ContainsKey(keyValuePair.Key))
        baseDict.Add(keyValuePair);
    }
  }

  /// <summary>Получить список типов связей по которым загружаются данные для записей</summary>
  /// <returns></returns>
  internal virtual List<int> GetRelationTypesUsedInDocument() => new List<int>();

  /// <summary>Загрузить данные указанных связей в документ из БД</summary>
  /// <param name="relations">Словарь списков идентификаторов связей разных типов связи</param>
  /// <param name="context">Контекст загрузки записей</param>
  /// <returns>Возвращает список загруженных записей</returns>
  internal List<AVSRow> LoadRelationsData(
    Dictionary<int, List<long>> relations,
    AVSDocumentContext context)
  {
    List<AVSRow> avsRowList = new List<AVSRow>();
    Dictionary<long, AVSRow> loadedRelations = new Dictionary<long, AVSRow>();
    RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument(this.SortIndexDictionary);
    List<int> typesUsedInDocument = this.GetRelationTypesUsedInDocument();
    foreach (KeyValuePair<int, List<long>> relation in relations)
    {
      List<long> longList = relation.Value;
      int key = relation.Key;
      if (typesUsedInDocument.Contains(key))
      {
        ColumnDescriptor[] descriptorsForSpecRow = this.GetAllColumnDescriptorsForSpecRow(key, false, this.AutoSort, true, this.IsGridViewMode, context.IsOldSpConverting);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (longList.Count > 0)
          {
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-20, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
            }, descriptorsForSpecRow, recordCount: this.PacketSize);
            AVSDocument.SetFiltrationTags(ref paramSet, context);
            for (int index = 0; index < this.productsInfo.Count; ++index)
            {
              context.Product = this.productsInfo[index];
              this.LoadPartsData(paramSet, context.Product, true, key, false, false, true, context, sessionKeeper.Session, loadedRelations, rowDicts);
            }
            foreach (KeyValuePair<long, AVSRow> keyValuePair in loadedRelations)
            {
              if (!avsRowList.Contains(keyValuePair.Value))
                avsRowList.Add(keyValuePair.Value);
            }
            loadedRelations.Clear();
          }
        }
      }
    }
    return avsRowList;
  }

  /// <summary>Загрузить новые связи в документ и обновить структуру документа</summary>
  /// <param name="relations">Словарь списков идентификаторов связей разных типов связи</param>
  /// <param name="context">Контекст загрузки записей</param>
  /// <param name="updateViewNodes">Обновить узлы дерева документа и табличного вида</param>
  /// <param name="addToCurrentProducts">Добавлять в текущий набор исполнений</param>
  /// <returns>Возвращает список загруженных записей</returns>
  internal List<AVSRow> LoadNewRelations(
    Dictionary<int, List<long>> relations,
    AVSDocumentContext context,
    bool updateViewNodes,
    bool addToCurrentProducts = false,
    bool updateDraftForParts = false)
  {
    List<AVSRow> collection = new List<AVSRow>();
    if (relations.Count == 0)
      return collection;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      collection = this.LoadRelationsData(relations, context);
      if (collection.IsNullOrEmpty<AVSRow>())
        return collection;
      if (this.IsFormB & addToCurrentProducts)
      {
        int num1 = -1;
        int num2 = -1;
        if (this.DocumentControl != null && this.DocumentControl.ActivePage != null)
          num1 = this.GetFirstProductIndex((PageData) this.DocumentControl.ActivePage);
        if (num1 != -1)
        {
          int rowProductCount = this.RowProductCount;
          num2 = num1 / this.RowProductCount;
        }
        foreach (AVSRow avsRow in collection)
          avsRow.ProductGroup = num2;
      }
      this.UpdateDocumentStructure(false, false, false, updateDraftForParts: updateDraftForParts);
      for (int index = collection.Count - 1; index >= 0; --index)
      {
        if (collection[index].Section == null)
          collection.RemoveAt(index);
        else
          collection[index].CheckAdditionalChapter();
      }
      this.ReloadDopzamenTextForGroup((List<long>) null, true);
      this.IndexAVSDocument(true);
      if (updateViewNodes)
      {
        EmptyRowUpdateMode updateMode = addToCurrentProducts ? EmptyRowUpdateMode.DontChange : EmptyRowUpdateMode.Create;
        if (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V)
        {
          for (int index = 0; index < collection.Count; ++index)
            collection[index].UpdateDocRow((TableData) null, (List<AvsRowAttributeInfo>) null, false, false, false, updateMode);
        }
        this.UpdateViewNodes(false, false, true, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
      }
      List<long> refIDs = new List<long>();
      foreach (KeyValuePair<int, List<long>> relation in relations)
      {
        if (relation.Key != AvsIDCache.Relation_Document)
          refIDs.AddRange((IEnumerable<long>) relation.Value);
      }
      this.UpdateFormatAttributeInReferencesFromDB((IList<long>) refIDs, true);
      if (updateViewNodes)
      {
        this.UpdateNoteDocCells(false, false);
        this.UpdateVariableDataCaptions();
        if (this.IsGridViewMode)
          this.avsWindow.virtualTree.ExpandAll();
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
    return collection;
  }

  /// <summary>Добавить связь в типизированный словарь связей</summary>
  /// <param name="relations">Словарь связей</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="relationID">Идентификатор связи</param>
  internal static void AddRelationToTypedDictionary(
    Dictionary<int, List<long>> relations,
    int relationType,
    long relationID)
  {
    List<long> longList;
    if (!relations.TryGetValue(relationType, out longList))
    {
      longList = new List<long>();
      relations.Add(relationType, longList);
    }
    if (longList.Contains(relationID))
      return;
    longList.Add(relationID);
  }

  /// <summary>Добавить связи в типизированный словарь связей</summary>
  /// <param name="relations">Словарь связей</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="relationIDs">Список идентификаторов связи</param>
  internal static void AddRelationToTypedDictionary(
    Dictionary<int, List<long>> relations,
    int relationType,
    IEnumerable<long> relationIDs)
  {
    if (!relationIDs.Any<long>())
      return;
    List<long> first;
    if (!relations.TryGetValue(relationType, out first))
    {
      first = new List<long>();
      relations.Add(relationType, first);
    }
    relations[relationType] = new List<long>(first.Union<long>(relationIDs));
  }

  /// <summary>Удалить все дочерние типы из списка типов</summary>
  private void RemoveChildTypes(List<int> objTypes)
  {
    for (int index1 = objTypes.Count - 1; index1 >= 0; --index1)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objTypes[index1], objTypes[index2]))
        {
          objTypes.RemoveAt(index1);
          break;
        }
        if (MetaDataHelper.IsObjectTypeChildOf(objTypes[index2], objTypes[index1]))
        {
          objTypes.RemoveAt(index2);
          --index1;
        }
      }
    }
  }

  /// <summary>Загрузить объекты спецификации</summary>
  /// <param name="objectGuids">Массив идентификаторов объектов</param>
  /// <param name="context">Контекст загрузки записей</param>
  /// <param name="updateViewNodes">Обновить узлы дерева документа и табличного вида</param>
  /// <param name="session">Сессия</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <returns>Возвращает список загруженных записей</returns>
  internal List<AVSRow> LoadRowsForDBObjects(
    List<Guid> objectGuids,
    AVSDocumentContext context,
    bool updateViewNodes,
    IUserSession session,
    RowDictionariesForLoadDocument rowDicts)
  {
    List<int> objectTypes1 = new List<int>();
    List<int> objectTypes2 = new List<int>();
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<AVSRow> loadedSpecRows = new List<AVSRow>();
    for (int index = 0; index < objectGuids.Count; ++index)
    {
      IDBObject dbObject = session.GetObject(objectGuids[index], false);
      if (dbObject != null)
      {
        int objectType = dbObject.ObjectType;
        long objectId = dbObject.ObjectID;
        List<TableData> tableDataList;
        if (rowDicts.docRowsByObjectGuid.TryGetValue(objectGuids[index], out tableDataList))
          rowDicts.docRowsByObjectID.Add(objectId, tableDataList);
        if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Document))
        {
          longList1.Add(objectId);
          if (!objectTypes1.Contains(objectType))
            objectTypes1.Add(objectType);
        }
        else
        {
          longList2.Add(objectId);
          if (!objectTypes2.Contains(objectType))
            objectTypes2.Add(objectType);
        }
      }
    }
    if (longList1.Count == 0 && longList2.Count == 0)
      return loadedSpecRows;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      ColumnDescriptor[] descriptorsForSpecRow1 = this.GetAllColumnDescriptorsForSpecRow(AvsIDCache.Relation_Document, true, this.AutoSort, true, this.ViewMode == AVSViewMode.Grid, context.IsOldSpConverting);
      ColumnDescriptor[] descriptorsForSpecRow2 = this.GetAllColumnDescriptorsForSpecRow(AvsIDCache.Relation_Project, true, this.AutoSort, true, this.ViewMode == AVSViewMode.Grid, context.IsOldSpConverting);
      if (longList1.Count > 0)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) longList1.ToArray(), LogicalOperators.NONE, 0, true)
        }, descriptorsForSpecRow1, recordCount: this.PacketSize);
        AVSDocument.SetFiltrationTags(ref paramSet, context);
        for (int index = 0; index < this.productsInfo.Count; ++index)
        {
          context.Product = this.productsInfo[index];
          this.LoadPartsData(new LoadDataParams(paramSet, false, this.productsInfo[index], AvsIDCache.Relation_Document, objectTypes1, false, false, true, context, session, (Dictionary<long, AVSRow>) null, loadedSpecRows, rowDicts));
        }
      }
      if (longList2.Count > 0)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) longList2.ToArray(), LogicalOperators.NONE, 0, true)
        }, descriptorsForSpecRow2, recordCount: this.PacketSize);
        AVSDocument.SetFiltrationTags(ref paramSet, context);
        if (this.productsInfo.Count > 0)
          context.Product = this.productsInfo[0];
        this.LoadPartsData(new LoadDataParams(paramSet, false, context.Product, AvsIDCache.Relation_Project, objectTypes2, false, false, true, context, session, (Dictionary<long, AVSRow>) null, loadedSpecRows, rowDicts));
      }
      this.UpdateDocumentStructure(false, false, false);
      this.IndexAVSDocument(true);
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      List<AVSRow> specRows = new List<AVSRow>();
      for (int index = 0; index < loadedSpecRows.Count; ++index)
      {
        if (loadedSpecRows[index].DocNode == null)
          specRows.Add(loadedSpecRows[index]);
      }
      if (updateViewNodes)
        this.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
      if (specRows.Count > 0)
        this.UpdateFormatAttributeInRowsFromDB(specRows, true);
      if (updateViewNodes)
      {
        this.UpdateNoteDocCells(false, false);
        this.UpdateVariableDataCaptions();
        if (this.IsGridViewMode)
          this.avsWindow.virtualTree.ExpandAll();
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
    return loadedSpecRows;
  }

  /// <summary>Загрузить объекты спецификации</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  /// <param name="objectTypes">Список типов объектов в списке objectIDs</param>
  /// <param name="docColumns">Загружаемые атрибуты документов, если null, то загружать все необходимые атрибуты согласно настройкам</param>
  /// <param name="prjColumns">Загружаемые атрибуты изделий, если null, то загружать все необходимые атрибуты согласно настройкам</param>
  /// <param name="createNewRecords">Создавать новые записи</param>
  /// <param name="context">Контекст загрузки записей</param>
  /// <param name="updateViewNodes">Обновить узлы дерева документа и табличного вида</param>
  /// <param name="session">Сессия</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="addToCurrentProducts">Добавлять записи только в текущий блок исполнений в форме Б</param>
  /// <returns>Возвращает список загруженных записей</returns>
  internal List<AVSRow> LoadRowsForDBObjects(
    List<long> objectIDs,
    List<int> objectTypes,
    ColumnDescriptor[] docColumns,
    ColumnDescriptor[] prjColumns,
    bool createNewRecords,
    AVSDocumentContext context,
    bool updateViewNodes,
    IUserSession session,
    RowDictionariesForLoadDocument rowDicts,
    bool addToCurrentProducts = false)
  {
    List<AVSRow> avsRowList = new List<AVSRow>();
    if (objectIDs.IsNullOrEmpty<long>())
      return avsRowList;
    List<int> objectTypes1 = new List<int>();
    List<int> objectTypes2 = new List<int>();
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    for (int index = 0; index < objectIDs.Count; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objectTypes[index], AvsIDCache.ObjType_Document))
      {
        longList1.Add(objectIDs[index]);
        if (!objectTypes1.Contains(objectTypes[index]))
          objectTypes1.Add(objectTypes[index]);
      }
      else
      {
        longList2.Add(objectIDs[index]);
        if (!objectTypes2.Contains(objectTypes[index]))
          objectTypes2.Add(objectTypes[index]);
      }
    }
    if (longList1.Count == 0 && longList2.Count == 0)
      return avsRowList;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      if (docColumns == null)
      {
        docColumns = this.GetAllColumnDescriptorsForSpecRow(AvsIDCache.Relation_Document, true, this.AutoSort, true, this.ViewMode == AVSViewMode.Grid, context.IsOldSpConverting);
      }
      else
      {
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        for (int index = 0; index < docColumns.Length; ++index)
        {
          if (docColumns[index].AttributeSource != AttributeSourceTypes.Relation)
            columnDescriptorList.Add(docColumns[index]);
        }
        docColumns = columnDescriptorList.ToArray();
      }
      if (prjColumns == null)
      {
        prjColumns = this.GetAllColumnDescriptorsForSpecRow(AvsIDCache.Relation_Project, true, this.AutoSort, true, this.ViewMode == AVSViewMode.Grid, context.IsOldSpConverting);
      }
      else
      {
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        for (int index = 0; index < prjColumns.Length; ++index)
        {
          if (prjColumns[index].AttributeSource != AttributeSourceTypes.Relation)
            columnDescriptorList.Add(prjColumns[index]);
        }
        prjColumns = columnDescriptorList.ToArray();
      }
      if (context.Product == null && this.productsInfo.Count > 0)
        context.Product = this.productsInfo[0];
      if (longList1.Count > 0)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) longList1.ToArray(), LogicalOperators.NONE, 0, true)
        }, docColumns, recordCount: this.PacketSize);
        AVSDocument.SetFiltrationTags(ref paramSet, context);
        this.LoadPartsData(new LoadDataParams(paramSet, false, context.Product, AvsIDCache.Relation_Document, objectTypes1, false, false, createNewRecords, context, session, (Dictionary<long, AVSRow>) null, avsRowList, rowDicts));
      }
      if (longList2.Count > 0)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) longList2.ToArray(), LogicalOperators.NONE, 0, true)
        }, prjColumns, recordCount: this.PacketSize);
        AVSDocument.SetFiltrationTags(ref paramSet, context);
        this.LoadPartsData(new LoadDataParams(paramSet, false, context.Product, AvsIDCache.Relation_Project, objectTypes2, false, false, createNewRecords, context, session, (Dictionary<long, AVSRow>) null, avsRowList, rowDicts));
        this.ReloadDopzamenTextForGroup((List<long>) null, false);
      }
      this.UpdateDocumentStructure(false, false, false, updateDraftForParts: context.UpdateDraftRows);
      this.IndexAVSDocument(true);
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      if (updateViewNodes)
      {
        if (this.DocumentControl == null || !this.IsFormBPage((PageData) this.DocumentControl.ActivePage))
          addToCurrentProducts = false;
        if (addToCurrentProducts)
        {
          int num1 = -1;
          int num2 = -1;
          if (this.DocumentControl != null && this.DocumentControl.ActivePage != null)
            num1 = this.GetFirstProductIndex((PageData) this.DocumentControl.ActivePage);
          if (num1 != -1)
          {
            int rowProductCount = this.RowProductCount;
            num2 = num1 / this.RowProductCount;
          }
          foreach (AVSRow avsRow in avsRowList)
            avsRow.ProductGroup = num2;
        }
        EmptyRowUpdateMode updateMode = addToCurrentProducts ? EmptyRowUpdateMode.DontChange : EmptyRowUpdateMode.Create;
        if (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V)
        {
          for (int index = 0; index < avsRowList.Count; ++index)
            avsRowList[index].UpdateDocRow((TableData) null, (List<AvsRowAttributeInfo>) null, false, false, false, updateMode);
        }
        this.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
      }
      if (avsRowList.Count > 0)
        this.UpdateFormatAttributeInRowsFromDB(avsRowList, true);
      if (updateViewNodes)
      {
        this.UpdateNoteDocCells(false, false);
        this.UpdateVariableDataCaptions();
        if (this.IsGridViewMode)
          this.avsWindow.virtualTree.ExpandAll();
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
    return avsRowList;
  }

  /// <summary>Получить данные всех строк спецификации для заданного типа связей с сервера</summary>
  /// <param name="paramSet">Параметры запроса</param>
  /// <param name="product">Индекс исполнения</param>
  /// <param name="loadRelations">Загружать данные связей и объектов. Если false, то загружать данные только объектов</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="skipUnknownDoc">Пропускать документы без связи с разделом</param>
  /// <param name="sortAll">Сортировать все</param>
  /// <param name="createNewRecords">Создавать новые записи</param>
  /// <param name="context">Контекст вызова метода</param>
  /// <param name="session">Сессия</param>
  /// <param name="loadedRelations">Словарь загруженных связей. Если null, то не заполняется</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal void LoadPartsData(
    DBRecordSetParams paramSet,
    ProductInfo product,
    bool loadRelations,
    int relationType,
    bool skipUnknownDoc,
    bool sortAll,
    bool createNewRecords,
    AVSDocumentContext context,
    IUserSession session,
    Dictionary<long, AVSRow> loadedRelations,
    RowDictionariesForLoadDocument rowDicts)
  {
    this.LoadPartsData(new LoadDataParams(paramSet, (loadRelations ? 1 : 0) != 0, product, relationType, new List<int>((IEnumerable<int>) new int[1]
    {
      -1
    }), (skipUnknownDoc ? 1 : 0) != 0, (sortAll ? 1 : 0) != 0, (createNewRecords ? 1 : 0) != 0, context, session, loadedRelations, (List<AVSRow>) null, rowDicts));
  }

  internal AttributeValueMap GetAttributeValueMapForRelation(int relationType)
  {
    AttributeValueMap valueMapForRelation;
    if (!this.relationsAttributeValueMapDictionary.TryGetValue(relationType, out valueMapForRelation))
    {
      valueMapForRelation = new AttributeValueMap();
      this.relationsAttributeValueMapDictionary.Add(relationType, valueMapForRelation);
    }
    return valueMapForRelation;
  }

  internal AttributeValueMap GetAttributeValueMapForObject(bool isDocumentType)
  {
    AttributeValueMap valueMapForObject;
    if (isDocumentType)
    {
      if (this.docObjectAttrMap == null)
        this.docObjectAttrMap = new AttributeValueMap();
      valueMapForObject = this.docObjectAttrMap;
    }
    else
    {
      if (this.prjObjectAttrMap == null)
        this.prjObjectAttrMap = new AttributeValueMap();
      valueMapForObject = this.prjObjectAttrMap;
    }
    return valueMapForObject;
  }

  /// <summary>Получить данные всех строк спецификации для заданного типа связей с сервера</summary>
  /// <param name="loadParams">Параметры запроса</param>
  internal void LoadPartsData(LoadDataParams loadParams)
  {
    if (loadParams.Context == null)
      loadParams.Context = new AVSDocumentContext();
    int rowIndex = loadParams.Context.RowIndex;
    if (loadParams.Context.RowIndex != -1)
      ++rowIndex;
    ProductInfo product = loadParams.Product;
    long projectID = -1;
    if (product == null && this.productsInfo.Count > 0)
      product = this.productsInfo[0];
    if (product != null)
    {
      loadParams.ProductIndex = this.GetProductIndex(product);
      projectID = product.Id;
    }
    this.IsRowsUpdating = !loadParams.CreateNewRecords;
    try
    {
      int objTypeColumnIndex = -1;
      int keyColumnIndex = -1;
      AttributeValueMap relationAttrMap;
      AttributeValueMap objectAttrMap;
      int[] sortColumnIndexes;
      this.GenerateAttributesCacheMaps(loadParams, out relationAttrMap, out objectAttrMap, out objTypeColumnIndex, out keyColumnIndex, out sortColumnIndexes);
      if (this.AvsDocumentForm == AVSDocumentForm.V && this.variableDataChapter_FormV == null)
        this.VariableDataChapter_FormV = new VariableDataChapterFormV(this);
      this.RemoveChildTypes(loadParams.ObjectTypes);
      IDBRelationCollection relationCollection = (IDBRelationCollection) null;
      IDBObjectCollection objectCollection = (IDBObjectCollection) null;
      for (int index1 = 0; index1 < loadParams.ObjectTypes.Count; ++index1)
      {
        if (loadParams.LoadRelations)
        {
          if (relationCollection == null)
            relationCollection = loadParams.Session.GetRelationCollection(loadParams.RelationType, this.FiltrationOwnerID);
          relationCollection.ObjectTypeID = loadParams.ObjectTypes[index1];
        }
        else if (objectCollection == null)
        {
          objectCollection = loadParams.Session.GetObjectCollection(loadParams.ObjectTypes[index1]);
          objectCollection.ShowAllModifications = true;
        }
        else
          objectCollection.ObjectTypeID = loadParams.ObjectTypes[index1];
        loadParams.SelectParamSet.LastKeyValue = 0L;
        loadParams.SelectParamSet.LastOrderValue = (object) null;
        ArrayList arrayList = new ArrayList(sortColumnIndexes.Length + 1);
        int num = 0;
        bool flag = false;
        while (!flag)
        {
          DataTable dataTable1;
          DataTable dataTable2;
          if (!loadParams.LoadRelations)
            dataTable2 = dataTable1 = objectCollection.Select(loadParams.SelectParamSet);
          else
            dataTable1 = dataTable2 = relationCollection.Select(loadParams.SelectParamSet, projectID, -1L, DateTime.Now);
          DataTable parts = dataTable2;
          num += parts.Rows.Count;
          this.EnterPendingRelationUpdateMode();
          try
          {
            for (int index2 = 0; index2 < parts.Rows.Count; ++index2)
            {
              int int32 = Convert.ToInt32(parts.Rows[index2][objTypeColumnIndex]);
              if (!this.IsSpecification || !MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Specification))
              {
                AttributeValuesCache objValuesCache;
                RelationAttributeValuesCache relValuesCache;
                this.LoadAvsRowAttributesToAttrValueCache(loadParams, relationAttrMap, objectAttrMap, parts, index2, out objValuesCache, out relValuesCache);
                this.CreateAvsRowFromDbRecord(loadParams, ref rowIndex, relValuesCache, objValuesCache);
              }
            }
          }
          finally
          {
            this.ExitPendingRelationUpdateMode();
          }
          flag = Convert.ToBoolean(parts.ExtendedProperties[(object) "Eof"]);
          if (!flag && parts.Rows.Count > 0)
          {
            loadParams.SelectParamSet.LastKeyValue = Convert.ToInt64(parts.Rows[parts.Rows.Count - 1][keyColumnIndex]);
            arrayList.Clear();
            for (int index3 = 0; index3 < sortColumnIndexes.Length; ++index3)
              arrayList.Add(parts.Rows[parts.Rows.Count - 1][sortColumnIndexes[index3]]);
            loadParams.SelectParamSet.LastOrderValue = (object) arrayList;
          }
          parts.Dispose();
        }
      }
    }
    finally
    {
      this.IsRowsUpdating = false;
    }
  }

  private void LoadAvsRowAttributesToAttrValueCache(
    LoadDataParams loadParams,
    AttributeValueMap relationAttrMap,
    AttributeValueMap objectAttrMap,
    DataTable parts,
    int i,
    out AttributeValuesCache objValuesCache,
    out RelationAttributeValuesCache relValuesCache)
  {
    objValuesCache = new AttributeValuesCache(objectAttrMap.AttributeDictionary, objectAttrMap.AttrsInfo);
    relValuesCache = (RelationAttributeValuesCache) null;
    if (loadParams.LoadRelations)
    {
      relValuesCache = new RelationAttributeValuesCache(relationAttrMap.AttributeDictionary, relationAttrMap.AttrsInfo, loadParams.Context.Product);
      relValuesCache.ObjectAttributesCache = objValuesCache;
    }
    for (int columnIndex = 0; columnIndex < loadParams.SelectParamSet.ColumnsInfo.Length; ++columnIndex)
    {
      int attributeId = (int) loadParams.SelectParamSet.ColumnsInfo[columnIndex].AttributeID;
      if (loadParams.SelectParamSet.ColumnsInfo[columnIndex].AttributeSource == AttributeSourceTypes.Relation)
      {
        if (attributeId == AvsIDCache.Attr_Count || attributeId == AvsIDCache.Attr_CountForAdjustment)
        {
          if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.ID)
            relValuesCache.SetMeasureID(attributeId, parts.Rows[i][columnIndex], true);
          else if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.Value)
          {
            relValuesCache.SetMeasuredValue(attributeId, parts.Rows[i][columnIndex], true);
          }
          else
          {
            object obj = parts.Rows[i][columnIndex];
            switch (obj)
            {
              case null:
              case DBNull _:
                continue;
              default:
                relValuesCache.SetMeasuredValueCaption(attributeId, obj.ToString(), true, false);
                continue;
            }
          }
        }
        else if (attributeId == AvsIDCache.Attr_SortIndex && !this.IsSpecification)
        {
          relValuesCache.SetValue(attributeId, (object) 0, true);
        }
        else
        {
          AvsRowAttributeInfo attributeInfo = relValuesCache.GetAttributeInfo(attributeId);
          object obj = parts.Rows[i][columnIndex];
          if (attributeInfo != null && attributeInfo.FieldType == FieldTypes.ftObjectLink)
          {
            if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.ID)
            {
              relValuesCache.SetObjectID(attributeId, obj, true);
            }
            else
            {
              switch (obj)
              {
                case null:
                case DBNull _:
                  break;
                default:
                  relValuesCache.SetObjectText(attributeId, obj.ToString(), true);
                  break;
              }
            }
          }
          else
            relValuesCache.SetValue(attributeId, obj, true);
          bool flag = !(obj is DBNull) && !string.IsNullOrEmpty(obj.ToString());
          relValuesCache.PersistentAttrs[attributeId] = flag;
        }
      }
      else if (attributeId == AvsIDCache.Attr_Weight || attributeId == AvsIDCache.Attr_UnitWeight)
      {
        if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.ID)
          objValuesCache.SetMeasureID(attributeId, parts.Rows[i][columnIndex], true);
        else if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.Value)
        {
          objValuesCache.SetMeasuredValue(attributeId, parts.Rows[i][columnIndex], true);
        }
        else
        {
          object obj = parts.Rows[i][columnIndex];
          switch (obj)
          {
            case null:
            case DBNull _:
              continue;
            default:
              objValuesCache.SetMeasuredValueCaption(attributeId, obj.ToString(), true, false);
              continue;
          }
        }
      }
      else
      {
        AvsRowAttributeInfo attributeInfo = objValuesCache.GetAttributeInfo(attributeId);
        if (attributeInfo != null && attributeInfo.FieldType == FieldTypes.ftObjectLink)
        {
          if (loadParams.SelectParamSet.Contents[columnIndex] == ColumnContents.ID)
          {
            objValuesCache.SetObjectID(attributeId, parts.Rows[i][columnIndex], true);
          }
          else
          {
            object obj = parts.Rows[i][columnIndex];
            switch (obj)
            {
              case null:
              case DBNull _:
                continue;
              default:
                objValuesCache.SetObjectText(attributeId, obj.ToString(), true);
                continue;
            }
          }
        }
        else
          objValuesCache.SetValue(attributeId, parts.Rows[i][columnIndex], true);
      }
    }
    if (relValuesCache == null)
      return;
    AVSDocument.DecodeApplicabilityCondition(loadParams, relValuesCache);
  }

  private static List<TableData> FindDocRowsInDictionaryByObject(
    LoadDataParams loadParams,
    long objectId,
    Guid objectGuid,
    out bool isExportDocRow)
  {
    if (loadParams == null)
      throw new ArgumentNullException(nameof (loadParams));
    if (objectId.IsUndefinedId() && objectGuid == Guid.Empty)
      throw new ArgumentException($"Идентификаторы запрашиваемого объекта не могут быть неопределёнными одновременно: objectId = {objectId}, objectGuid = {objectGuid}");
    isExportDocRow = false;
    List<TableData> list = new List<TableData>();
    if (loadParams.DocRowsByObjectID != null)
      loadParams.DocRowsByObjectID.TryGetValue(objectId, out list);
    if (list.IsEmpty<TableData>() && objectGuid != Guid.Empty && loadParams.DocRowsByObjectGuid != null)
    {
      loadParams.DocRowsByObjectGuid.TryGetValue(objectGuid, out list);
      if (!list.IsEmpty<TableData>() && loadParams.DocRowsByObjectID != null)
        loadParams.DocRowsByObjectID.Add(objectId, list);
    }
    if (list.IsEmpty<TableData>() && loadParams.ExpDocRowsByObjectID != null)
      isExportDocRow = loadParams.ExpDocRowsByObjectID.TryGetValue(objectId, out list);
    if (list.IsEmpty<TableData>() && objectGuid != Guid.Empty && loadParams.ExpDocRowsByObjectGuid != null)
    {
      isExportDocRow = loadParams.ExpDocRowsByObjectGuid.TryGetValue(objectGuid, out list);
      if (!list.IsEmpty<TableData>() && loadParams.ExpDocRowsByObjectID != null)
        loadParams.ExpDocRowsByObjectID.Add(objectId, list);
    }
    return list ?? new List<TableData>();
  }

  private void GenerateAttributesCacheMaps(
    LoadDataParams loadParams,
    out AttributeValueMap relationAttrMap,
    out AttributeValueMap objectAttrMap,
    out int objTypeColumnIndex,
    out int keyColumnIndex,
    out int[] sortColumnIndexes)
  {
    relationAttrMap = this.GetAttributeValueMapForRelation(loadParams.RelationType);
    objectAttrMap = this.GetAttributeValueMapForObject(loadParams.IsDocRelation);
    objTypeColumnIndex = -1;
    keyColumnIndex = -1;
    sortColumnIndexes = new int[loadParams.SelectParamSet.SortColumns.Length];
    for (int index1 = 0; index1 < loadParams.SelectParamSet.ColumnsInfo.Length; ++index1)
    {
      int attributeId = (int) loadParams.SelectParamSet.ColumnsInfo[index1].AttributeID;
      if (attributeId == -7)
        objTypeColumnIndex = index1;
      if (loadParams.SelectParamSet.SortColumns.Length != 0)
      {
        int index2 = ((IEnumerable<object>) loadParams.SelectParamSet.SortColumns).IndexOf<object>((object) attributeId);
        if (index2 != -1 && loadParams.SelectParamSet.SortSources[index2] == loadParams.SelectParamSet.ColumnsInfo[index1].AttributeSource)
          sortColumnIndexes[index2] = index1;
      }
      if (loadParams.LoadRelations)
      {
        if (attributeId == -20)
          keyColumnIndex = index1;
      }
      else if (attributeId == -2)
        keyColumnIndex = index1;
      if (loadParams.SelectParamSet.ColumnsInfo[index1].AttributeSource == AttributeSourceTypes.Relation)
      {
        if (!relationAttrMap.AttributeDictionary.ContainsKey(attributeId))
        {
          relationAttrMap.AttrsInfo.Add(new AvsRowAttributeInfo(true, attributeId));
          relationAttrMap.AttributeDictionary.Add(attributeId, relationAttrMap.AttrsInfo.Count - 1);
        }
      }
      else if (!objectAttrMap.AttributeDictionary.ContainsKey(attributeId))
      {
        objectAttrMap.AttrsInfo.Add(new AvsRowAttributeInfo(false, attributeId));
        objectAttrMap.AttributeDictionary.Add(attributeId, objectAttrMap.AttrsInfo.Count - 1);
      }
    }
  }

  /// <summary>Загрузить информацию о всех исполнения</summary>
  /// <param name="productID">Идентификатор версии объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Возвращает список исполнений</returns>
  public static List<ProductInfo> LoadProductsByGroupID(long productID, IUserSession userSession)
  {
    if (productID == 0L || productID == -1L)
      return (List<ProductInfo>) null;
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    if (userSession != null)
    {
      IDBObject objectActual = userSession.GetObjectActual(productID, true);
      if (objectActual != null)
      {
        string conditionValue = (string) null;
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
        if (attributeById != null)
          conditionValue = Convert.ToString(attributeById.Value);
        if (!string.IsNullOrEmpty(conditionValue))
        {
          List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors((List<int>) null, true, false);
          IDBObjectCollection objectCollection = userSession.GetObjectCollection(objectActual.ObjectType);
          objectCollection.ShowAllModifications = true;
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
          {
            new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, AvsConfig.General.SelectProductsWithCaseSensitive),
            new ConditionStructure(-9, RelationalOperators.NotEqual, (object) userSession.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
          }, columnDescriptors.ToArray());
          AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
          productInfoList = ProductInfo.ReadProductsInfo(objectCollection.Select(paramSet), columnDescriptors, true, false);
        }
        else
          productInfoList.Add(new ProductInfo(objectActual));
      }
    }
    return productInfoList;
  }

  /// <summary>Загрузить информацию о всех исполнения</summary>
  /// <param name="productID">Идентификатор версии объекта</param>
  /// <param name="attrList">Список дополнительных атрибутов исполнений</param>
  /// <param name="filtrationRuleSettings">Настройки фильтрации</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Возвращает список исполнений</returns>
  public List<ProductInfo> LoadProductsByGroupID(
    long productID,
    List<int> attrList,
    string filtrationRuleSettings,
    IUserSession userSession)
  {
    if (productID == 0L || productID == -1L)
      throw new ArgumentException("Не задан идентификатор изделия");
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    if (userSession != null)
    {
      IDBObject objectActual = userSession.GetObjectActual(productID, true);
      this.articleGroupID = Guid.Empty;
      string str = (string) null;
      IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
      if (attributeById != null)
        str = Convert.ToString(attributeById.Value);
      if (!string.IsNullOrEmpty(str))
      {
        this.articleGroupID = new Guid(str);
        List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors(attrList, true, false);
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(objectActual.ObjectType);
        objectCollection.ShowAllModifications = true;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) str, LogicalOperators.AND, 0, AvsConfig.General.SelectProductsWithCaseSensitive),
          new ConditionStructure(-9, RelationalOperators.NotEqual, (object) userSession.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
        }, columnDescriptors.ToArray());
        AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
        productInfoList = ProductInfo.ReadProductsInfo(objectCollection.Select(paramSet), columnDescriptors, true, false);
      }
      else
        productInfoList.Add(new ProductInfo(objectActual, attrList));
    }
    return productInfoList;
  }

  /// <summary>Загрузить информацию о заданных сборочных единицах</summary>
  /// <param name="productIDs">Список идентификаторов версий объектов</param>
  /// <param name="attrList">Список дополнительных атрибутов исполнений</param>
  /// <param name="filtrationRuleSettings">Настройки фильтрации</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Возвращает список исполнений</returns>
  public List<ProductInfo> LoadProductInfoForObjects(
    List<long> productIDs,
    List<int> attrList,
    string filtrationRuleSettings,
    IUserSession userSession)
  {
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    if (productIDs == null || productIDs.Count == 0 || userSession == null)
      return productInfoList;
    List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors(attrList, true, false);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) productIDs.ToArray(), LogicalOperators.NONE, 0, true)
    }, columnDescriptors.ToArray());
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    int objectTypeId = userSession.GetObjectInfo(productIDs[0]).ObjectTypeID;
    IDBObjectCollection objectCollection = userSession.GetObjectCollection(objectTypeId);
    objectCollection.ShowAllModifications = true;
    productInfoList = ProductInfo.ReadProductsInfo(objectCollection.Select(paramSet), columnDescriptors, true, false);
    return productInfoList;
  }

  /// <summary>Загрузить информацию о заданных исполнения</summary>
  /// <param name="productIDs">Список идентификаторов версий исполнений</param>
  /// <param name="attrList">Список дополнительных атрибутов исполнений (может быть null)</param>
  /// <param name="includeStdColumns">Грузить стандартный набор атрибутов</param>
  /// <param name="filtrationOwnerID">Идентификатор правила подбора версий</param>
  /// <param name="session">Сессия</param>
  internal static List<ProductInfo> LoadProductsForSpecification(
    List<long> productIDs,
    List<int> attrList,
    bool includeStdColumns,
    string filtrationOwnerID,
    IUserSession session)
  {
    if (productIDs == null)
      throw new ArgumentNullException(nameof (productIDs));
    if (productIDs.Count == 0)
      return new List<ProductInfo>();
    IDBObjectCollection objectCollection = session.GetObjectCollection(AvsIDCache.ObjType_Product);
    objectCollection.ShowAllModifications = true;
    List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors(attrList, includeStdColumns, false);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) productIDs.ToArray(), LogicalOperators.NONE, 0, true)
    }, columnDescriptors.ToArray());
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    return ProductInfo.ReadProductsInfo(objectCollection.Select(paramSet), columnDescriptors, includeStdColumns, false);
  }

  /// <summary>Найти исполнения изделия для документа по связям</summary>
  /// <param name="documentID">Идентификатор документа</param>
  /// <param name="attrList">Список дополнительных атрибутов исполнений (может быть null)</param>
  /// <param name="includeStdColumns">Грузить стандартный набор атрибутов</param>
  /// <param name="filtrationOwnerID">Идентификатор правила подбора версий</param>
  /// <param name="session">Сессия</param>
  /// <returns>Возвращает список исполнений связанных с документом</returns>
  internal static List<ProductInfo> LoadProductsForAVSDocument(
    long documentID,
    List<int> attrList,
    bool includeStdColumns,
    string filtrationOwnerID,
    IUserSession session)
  {
    if (documentID.IsUndefinedId())
      return new List<ProductInfo>(0);
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
    List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors(attrList, includeStdColumns, true);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptors.ToArray());
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    return ProductInfo.ReadProductsInfo(relationCollection.EntersInVersion(paramSet, documentID), columnDescriptors, includeStdColumns, true);
  }

  /// <summary>Проверить наличие версии исполнения с заданныи обозначением для данного изделия</summary>
  /// <param name="specFID">идентификатор спецификации</param>
  /// <param name="productDesignation">обозначение нового исполнения</param>
  /// <param name="session">Сессия</param>
  /// <returns>Возвращает список версий найденных исполнений</returns>
  internal long CheckExistingProductVersion(
    long specFID,
    string productDesignation,
    IUserSession session)
  {
    string FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, FiltrationOwnerID);
    relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_ArticleGroupID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(AvsIDCache.Attr_Designation, RelationalOperators.Equal, (object) productDesignation, LogicalOperators.NONE, 0, false)
      {
        AttributeSource = AttributeSourceTypes.Object
      }
    }, columns);
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    DataTable dataTable = relationCollection.EntersIn(paramSet, specFID);
    string prevGenerationID = Guid.Empty.ToString();
    if (this.productsInfo.Count > 0 && this.productsInfo[0].ParentVersionId.IsDefinedId())
    {
      IDBObject objectById = session.GetObjectByID(this.productsInfo[0].ParentVersionId, false);
      if (objectById != null)
        prevGenerationID = Convert.ToString(objectById.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID).Value);
    }
    long? nullable = dataTable.Rows.Where((System.Func<DataRow, bool>) (dr => Convert.ToString(dr[1]) == prevGenerationID)).Select<DataRow, long>((System.Func<DataRow, long>) (r => Convert.ToInt64(r[0]))).OrderByDescending<long, long>((System.Func<long, long>) (v => Math.Abs(v))).FirstOrNull<long>();
    List<long> objectIDs;
    if (!nullable.HasValue)
      objectIDs = dataTable.Rows.Select<long>((System.Func<DataRow, long>) (r => Convert.ToInt64(r[0]))).OrderByDescending<long, long>((System.Func<long, long>) (v => Math.Abs(v))).ToList<long>();
    else
      objectIDs = new List<long>() { nullable.Value };
    if (objectIDs.Count > 1)
    {
      ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, productDesignation, (IList) objectIDs);
      object[] objArray = SelectionWindow.Select($"Выберите базовую версию для новой версии исполнения \"{productDesignation}\"", (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect | SelectionOptions.ForceFilterObjectsByRule);
      return objArray != null && objArray.Length != 0 && objArray[0] is IDBTypedObjectID dbTypedObjectId ? dbTypedObjectId.ObjectID : -1L;
    }
    return objectIDs.Count <= 0 ? -1L : objectIDs[0];
  }

  /// <summary>Загрузить информацию о всех исполнения</summary>
  public void UpdateProductsByGroupID()
  {
    if (this.versionAttributesHelper.Items == null || this.AvsDocumentForm != AVSDocumentForm.A || this.ProductsInfo == null || this.ProductsInfo.Count <= 0)
      return;
    ProductInfo productInfo = this.ProductsInfo[0];
    List<int> attrList = new List<int>();
    for (int index = 0; index < this.versionAttributesHelper.Items.Count; ++index)
    {
      if (!productInfo.HasAttribute(this.versionAttributesHelper.Items[index].ID) && !attrList.Contains(this.versionAttributesHelper.Items[index].ID))
        attrList.Add(this.versionAttributesHelper.Items[index].ID);
    }
    if (attrList.Count <= 0)
      return;
    this.UpdateProductsByGroupID(attrList, (string) null);
  }

  internal void UpdateProductAdditionalAttributes(ProductInfo fProduct)
  {
    if (this.versionAttributesHelper.Items == null || this.AvsDocumentForm != AVSDocumentForm.A || fProduct == null)
      return;
    List<int> attrList = new List<int>();
    for (int index = 0; index < this.versionAttributesHelper.Items.Count; ++index)
    {
      if (!fProduct.HasAttribute(this.versionAttributesHelper.Items[index].ID) && !attrList.Contains(this.versionAttributesHelper.Items[index].ID))
        attrList.Add(this.versionAttributesHelper.Items[index].ID);
    }
    if (attrList.Count <= 0)
      return;
    fProduct.UpdateInfo(attrList, (string) null);
  }

  /// <summary>Загрузить информацию о всех исполнения</summary>
  /// <param name="productsInfo">Список исполнений</param>
  /// <param name="productType">Тип объекта исполнения</param>
  /// <param name="articleGroupID">Идентификатор группового изделия. -1 - если единичная форма</param>
  /// <param name="attrList">Список дополнительных атрибутов исполнений</param>
  /// <param name="filtrationRuleSettings">Настройки фильтрации</param>
  public void UpdateProductsByGroupID(List<int> attrList, string filtrationRuleSettings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.articleGroupID != Guid.Empty)
      {
        List<ColumnDescriptor> columnDescriptors = ProductInfo.CreateColumnDescriptors(attrList, false, false);
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this.productType);
        objectCollection.ShowAllModifications = true;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) this.articleGroupID, LogicalOperators.AND, 0, AvsConfig.General.SelectProductsWithCaseSensitive),
          new ConditionStructure(-9, RelationalOperators.NotEqual, (object) sessionKeeper.Session.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
        }, columnDescriptors.ToArray());
        AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
        long productId = -1;
        ProductInfo productInfo = (ProductInfo) null;
        bool flag = false;
        paramSet.LastKeyValue = 0L;
        paramSet.LastOrderValue = (object) null;
        while (!flag)
        {
          DataTable dataTable = objectCollection.Select(paramSet);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            productId = Convert.ToInt64(row[0]);
            int productIndex = this.GetProductIndex(productId);
            if (productIndex != -1)
            {
              productInfo = this.productsInfo[productIndex];
              for (int index = 1; index < columnDescriptors.Count; ++index)
                productInfo.SetAttributeValue(Convert.ToInt32(columnDescriptors[index].AttributeID), row[index], false);
            }
          }
          flag = Convert.ToBoolean(dataTable.ExtendedProperties[(object) "Eof"]);
          if (!flag && dataTable.Rows.Count > 0 && productInfo != null)
          {
            paramSet.LastKeyValue = productId;
            paramSet.LastOrderValue = (object) productId;
          }
        }
      }
      else
      {
        if (this.productsInfo.Count <= 0)
          return;
        this.productsInfo[0].UpdateInfo(attrList, (string) null);
      }
    }
  }

  /// <summary>Загрузить документ СП.
  /// Если его нет, то создать</summary>
  /// <param name="checkOut">Взять на изменение документ</param>
  /// <param name="groupForm_FromAttr">Возвращает форму группового документа загруженного файла</param>
  /// <param name="hasOldSpFile">В объекте документа хранится файл SP</param>
  /// <returns>Возвращает true, если был создан новый документ</returns>
  protected bool LoadImDocument(
    bool checkOut,
    out AVSDocumentForm? groupForm_FromAttr,
    out bool hasOldSpFile)
  {
    hasOldSpFile = false;
    if (this.DocumentDBObjectType == -1)
      this.DocumentDBObjectType = AvsIDCache.ObjType_Specification;
    long num1 = -1;
    long num2 = -1;
    long num3 = this.DocumentID;
    if (num3 == -1L && this.productsInfo.Count > 0)
    {
      this.DocumentDesignation = this.productsInfo[0].Designation;
      this.DocumentName = this.productsInfo[0].Name;
      if (!this.ReadOnly)
      {
        if (this.IsSpecification)
          num2 = this.SearchDocParentVersionId(this.productsInfo[0].ObjectType);
        if (Intermech.Consts.IsUndefinedObjectId(num2))
          num1 = this.SearchExistDocumentByDesignation(this.DocumentDBObjectType, this.DocumentDesignation);
      }
    }
    using (SessionKeeper sessionKeeper1 = new SessionKeeper())
    {
      groupForm_FromAttr = new AVSDocumentForm?();
      long relationID = -1;
      long projID = -1;
      int relType = -1;
      bool flag1 = false;
      IDBAVSDocumentObject docObject = (IDBAVSDocumentObject) null;
      AVSDocumentForm avsDocumentForm;
      if (num3 == -1L && this.productsInfo.Count > 0 && !this.ReadOnly)
      {
        IDBObject documentByPrototype;
        if (!Intermech.Consts.IsUndefinedObjectId(num1))
          documentByPrototype = sessionKeeper1.Session.GetObject(num1);
        else if (!Intermech.Consts.IsUndefinedObjectId(num2))
        {
          documentByPrototype = this.CreateNewDocumentByPrototype(ref groupForm_FromAttr, num2, sessionKeeper1.Session);
          flag1 = true;
        }
        else
        {
          documentByPrototype = sessionKeeper1.Session.GetObjectCollection(this.DocumentDBObjectType).Create();
          flag1 = true;
        }
        IDBAVSDocumentObject dbavsDocumentObject = AvsIDCache.GetDBAVSDocumentObject(documentByPrototype);
        long objectId = dbavsDocumentObject.ObjectID;
        int objectType = dbavsDocumentObject.ObjectType;
        this.DocumentID = objectId;
        this.DocumentGuid = dbavsDocumentObject.ObjectGUID;
        this.DocumentDBObjectType = objectType;
        if (this.AVSDocType != AVSDocumentType.UserAVSDocument)
          dbavsDocumentObject.SetAttributesValues(DBObjectHelper.Filter((IDBObject) dbavsDocumentObject, new AttributeValues[3]
          {
            new AttributeValues(AvsIDCache.Attr_Designation, (object) this.DocumentDesignation),
            new AttributeValues(AvsIDCache.Attr_Name, (object) this.DocumentName),
            new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) SpecificationFormMethods.EncodeSpecificationFormAttrValue(this.AvsDocumentForm))
          }), true);
        else
          dbavsDocumentObject.SetAttributesValues(DBObjectHelper.Filter((IDBObject) dbavsDocumentObject, new AttributeValues[2]
          {
            new AttributeValues(AvsIDCache.Attr_Designation, (object) this.DocumentDesignation),
            new AttributeValues(AvsIDCache.Attr_Name, (object) this.DocumentName)
          }), true);
        this.DocumentCaption = dbavsDocumentObject.Caption;
        if (flag1)
        {
          this.documentIsModifiedByLoad = true;
          bool flag2 = false;
          if (this.document != null)
          {
            flag2 = this.document.IsDocumentLoading;
            this.document.IsDocumentLoading = true;
            this.document.Clear(false, false);
          }
          this.LoadStdTemplate(this.avsDocTypeGuid, this.AvsDocumentForm, !this.ReadOnly);
          this.document.IsDocumentLoading = flag2;
          if (this.productsInfo.Count == 1)
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
          else
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, "", false, false, false);
          if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
            this.UpdateProductsInStampForMirrorSP();
          ImDocument document = this.document;
          string formDocAttribute = AVSDocument.SpecForm_DocAttribute;
          avsDocumentForm = this.AvsDocumentForm;
          string attributeValue = avsDocumentForm.ToString();
          document.SetAttributeValue(formDocAttribute, attributeValue, false, false, false);
          this.document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
          this.document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
          this.document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, this.avsDocTypeGuid.ToString(), false, false, false);
          if (this.documentTemplateGuid != Guid.Empty)
            this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
          else
            this.document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
        }
        this.DocumentCaption = dbavsDocumentObject.Caption;
        if (AVSDocument.IsSpecificationObjectType(this.documentType))
        {
          IDBRelationCollection relationCollection = sessionKeeper1.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
          for (int index = 0; index < this.productsInfo.Count; ++index)
          {
            IDBRelation relation = sessionKeeper1.Session.GetRelation(this.productsInfo[index].Id, dbavsDocumentObject.ObjectID, AvsIDCache.Relation_Document, true);
            if (relation == null)
            {
              relation = relationCollection.Create(this.productsInfo[index].Id, dbavsDocumentObject.ObjectID);
              relationID = relation.RelationID;
              projID = relation.ProjID;
              relType = relation.RelationType;
            }
            relation.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(dbavsDocumentObject.ObjectID))
            });
          }
        }
        if (flag1)
          DocumentEditorPlugin.SaveImDocumentObjectFile(objectId, this.Document, this.DefaultFileName, -1, true);
        if (dbavsDocumentObject.IsCreationMode)
          dbavsDocumentObject.CommitCreation(true);
        num3 = dbavsDocumentObject.ObjectID;
        if (flag1)
        {
          DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) this.Document, this.DocumentGuid, this.DocumentID, this.DocumentDBObjectType, this.DocumentCaption);
          this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, this.DocumentName, false, false, false);
          this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, this.DocumentDesignation, false, false, false);
          if (this.productsInfo.Count == 1)
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
          else
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, "", false, false, false);
          if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
            this.UpdateProductsInStampForMirrorSP();
        }
        docObject = (IDBAVSDocumentObject) null;
      }
      if (docObject == null && num3 > 0L)
        docObject = AvsIDCache.GetDBAVSDocumentObject(sessionKeeper1.Session, num3);
      if (((docObject == null ? 0 : (!this.ReadOnly ? 1 : 0)) & (checkOut ? 1 : 0)) != 0)
      {
        switch (docObject.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (docObject.ObjectID > 0L)
            {
              if (docObject.CheckoutBy == 0L)
              {
                docObject = (IDBAVSDocumentObject) docObject.CheckOut();
                break;
              }
              if (docObject.CheckoutBy != sessionKeeper1.Session.UserID)
              {
                if (this.IsSpecification)
                {
                  int num4 = (int) MessageBox.Show($"Спецификацию \"{docObject.Caption}\" нельзя модифицировать, т.к. она взята на изменение другим пользователем", "Внимание!");
                }
                else if (this.IsElementList)
                {
                  int num5 = (int) MessageBox.Show($"Перечень элементов \"{docObject.Caption}\" нельзя модифицировать, т.к. он взят на изменение другим пользователем", "Внимание!");
                }
                else
                {
                  int num6 = (int) MessageBox.Show($"Документ \"{docObject.Caption}\" нельзя модифицировать, т.к. он взят на изменение другим пользователем", "Внимание!");
                }
                this.ReadOnly = true;
                break;
              }
              break;
            }
            break;
          case ObjectModifyModes.CreateVersion:
            if (this.IsSpecification)
            {
              int num7 = (int) MessageBox.Show($"Чтобы редактировать спецификацию \"{docObject.Caption}\" необходимо выпустить новую версию объекта", "Внимание!");
            }
            else if (this.IsElementList)
            {
              int num8 = (int) MessageBox.Show($"Чтобы редактировать Перечень элементов \"{docObject.Caption}\" необходимо выпустить новую версию объекта", "Внимание!");
            }
            else
            {
              int num9 = (int) MessageBox.Show($"Чтобы редактировать документ \"{docObject.Caption}\" необходимо выпустить новую версию объекта", "Внимание!");
            }
            this.ReadOnly = true;
            break;
          case ObjectModifyModes.CantModify:
            if (this.IsSpecification)
            {
              int num10 = (int) MessageBox.Show($"Спецификацию \"{docObject.Caption}\" нельзя модифицировать", "Внимание!");
            }
            else if (this.IsElementList)
            {
              int num11 = (int) MessageBox.Show($"Перечень элементов \"{docObject.Caption}\" нельзя модифицировать", "Внимание!");
            }
            else
            {
              int num12 = (int) MessageBox.Show($"Документ \"{docObject.Caption}\" нельзя модифицировать", "Внимание!");
            }
            this.ReadOnly = true;
            break;
        }
        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        if (service != null)
        {
          service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", num3));
          if (this.IsSpecification)
            service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationID, projID, relType));
        }
        num3 = docObject.ObjectID;
      }
      bool flag3 = flag1;
      if (!flag1)
      {
        if (docObject == null)
          docObject = AvsIDCache.GetDBAVSDocumentObject(sessionKeeper1.Session, num3);
        IDBAttribute attributeById = docObject.GetAttributeByID(AvsIDCache.Attr_SpecificationForm);
        if (attributeById != null)
          groupForm_FromAttr = AVSDocument.DecodeSpecificationFormAttrValue(attributeById.AsString);
        if (groupForm_FromAttr.HasValue)
        {
          this.AvsDocumentForm = groupForm_FromAttr.Value;
          if (this.productsInfo.Count > 1 && this.AvsDocumentForm == AVSDocumentForm.Single || !AVSDocumentsSettings.IsAllowableDocumentForm(this.AVSDocType, this.AvsDocumentForm))
            this.AvsDocumentForm = this.GetDefaultGroupDocumentForm();
        }
        else
          this.AvsDocumentForm = this.productsInfo.Count > 1 ? this.GetDefaultGroupDocumentForm() : AVSDocumentForm.Single;
        this.DocumentDBObjectType = docObject.ObjectType;
        ImDocument document1 = this.Document;
        ImDocument document2;
        try
        {
          document2 = DocumentEditorPlugin.LoadDocumentFromDBObject((IDBObject) docObject, -1, false, false, false);
          if (document2 != null)
          {
            if (!this.IsSpecification)
              document2.IsDocumentLoading = true;
            document2.Designation = this.DocumentDesignation;
            document2.DocumentName = this.DocumentName;
            if (this.productsInfo.Count == 1)
              document2.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
            else
              document2.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, (string) null, false, false, false);
            string attributeValue1 = document2.GetAttributeValue(AVSDocument.AVSDocType_DocAttribute, true);
            if (attributeValue1 != "")
              this.avsDocumentType = (AVSDocumentType) Enum.Parse(typeof (AVSDocumentType), attributeValue1, true);
            string attributeValue2 = document2.GetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, true);
            if (attributeValue2 != "")
              this.avsDocTypeGuid = new Guid(attributeValue2);
            string attributeValue3 = document2.GetAttributeValue(AVSDocument.SpecForm_DocAttribute, true);
            if (attributeValue3 != "")
              this.AvsDocumentForm = (AVSDocumentForm) Enum.Parse(typeof (AVSDocumentForm), attributeValue3, true);
            if (this.productsInfo.Count > 1 && this.AvsDocumentForm == AVSDocumentForm.Single)
            {
              this.AvsDocumentForm = AVSDocumentForm.A;
              ImDocument imDocument = document2;
              string formDocAttribute = AVSDocument.SpecForm_DocAttribute;
              avsDocumentForm = this.AvsDocumentForm;
              string attributeValue4 = avsDocumentForm.ToString();
              imDocument.SetAttributeValue(formDocAttribute, attributeValue4, false, false, false);
            }
            this.additionalChaptersInDataChapter = document2.GetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, true) != "0";
            string attributeValue5 = document2.GetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, true);
            if (attributeValue5 != "")
            {
              Guid objectGUID = new Guid(attributeValue5);
              if (objectGUID != this.documentTemplateGuid)
              {
                IDBObject dbObject = sessionKeeper1.Session.GetObject(objectGUID, false);
                if (dbObject != null)
                {
                  this.documentTemplateGuid = objectGUID;
                  this.DocumentTemplateID = dbObject.ObjectID;
                }
              }
            }
            this.Document = document2;
            try
            {
              if (!this.ReadOnly)
              {
                if (AvsConfig.General.AutoUpdateTemplate)
                  this.UpdateDocumentTemplateIfOriginalChanged(this.avsDocTypeGuid, this.AvsDocumentForm, !this.ReadOnly);
              }
            }
            catch (Exception ex)
            {
              int num13 = (int) IMMessageBox.Show("Ошибка обновления шаблона документа", ex.Message, MessageBoxButtons.OK, IMMessageBoxImage.Warning);
            }
            ImDocumentData documentTemplate = this.Document.DocumentTemplate;
            if (documentTemplate == null)
              throw new Exception("Нарушена структура документа! Отсутствует внутренний шаблон!");
            if (!this.IsSpecification)
              documentTemplate.IsDocumentLoading = true;
            this.FindAllTemplates(documentTemplate, true);
            this.FindMainTablesInDocument((ImDocumentData) document2, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
            this.TryAddMainTableIfNeed();
            this.CheckMainDocumentTablesAndThrowException();
            if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
              this.UpdateProductsInStampForMirrorSP();
            if (!this.IsSpecification)
            {
              document2.IsDocumentLoading = false;
              documentTemplate.IsDocumentLoading = false;
            }
          }
        }
        catch (Exception ex)
        {
          if (ex is AccessDeniedException || ex.InnerException is AccessDeniedException)
            throw;
          ExceptionHelper.ExceptionService.ShowException(ex);
          int num14 = (int) MessageBox.Show("Документ будет воссоздан по составу изделия.", "Внимание!");
          document2 = (ImDocument) null;
        }
        flag3 = document2 == null;
        if (document2 == null)
        {
          this.documentIsModifiedByLoad = true;
          if (this.IsSpecification)
            hasOldSpFile = this.CheckOldDocPassport();
          if (this.document != null)
          {
            this.document.IsDocumentLoading = true;
            this.document.Clear(false, false);
          }
          this.LoadStdTemplate(this.avsDocTypeGuid, this.AvsDocumentForm, !this.ReadOnly);
          this.document.IsDocumentLoading = false;
          this.DocumentID = num3;
          this.DocumentGuid = docObject.ObjectGUID;
          this.DocumentCaption = docObject.Caption;
          this.DocumentDBObjectType = docObject.ObjectType;
          DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) this.Document, this.DocumentGuid, this.DocumentID, this.DocumentDBObjectType, this.DocumentCaption);
          this.document.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, this.DocumentName, false, false, false);
          this.document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
          this.document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, this.DocumentDesignation, false, false, false);
          if (this.productsInfo.Count == 1)
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
          else
            this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, "", false, false, false);
          if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
            this.UpdateProductsInStampForMirrorSP();
          this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
          this.document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
          this.document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, this.avsDocTypeGuid.ToString(), false, false, false);
          if (this.documentTemplateGuid != Guid.Empty)
            this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
          else
            this.document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
          if (!this.ReadOnly)
            DocumentEditorPlugin.SaveImDocumentObjectFile(num3, this.Document, this.DefaultFileName, -1, true);
        }
      }
      try
      {
        using (SessionKeeper sessionKeeper2 = new SessionKeeper())
          this.MaterialKeyWordsSchema = (KeyWordsSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper2.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_MaterialKeyWordsSchema, typeof (KeyWordsSchema));
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
      this.document?.SetMaterialKeyWords((List<string>) this.MaterialKeyWordsSchema?.KeyWords);
      this.DocumentID = num3;
      if (this.document != null)
      {
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) this.Document, this.DocumentGuid, this.DocumentID, this.DocumentDBObjectType, this.DocumentCaption);
        this.document.UpdateNodeLinks(true, false, false, false);
      }
      return flag3;
    }
  }

  private long SearchExistDocumentByDesignation(int documentObjectType, string designation)
  {
    long id = -1;
    IPDMSpecificationsService service = ServicesManager.GetService<IPDMSpecificationsService>();
    if (service != null)
      id = service.GetObjectWithDesignation(documentObjectType, designation);
    if (!Intermech.Consts.IsUndefinedObjectId(id) && MessageBox.Show($"В базе данных уже существует спецификация с обозначением \"{this.DocumentDesignation}\".\nСвязать редактируемое изделие с данной спецификацией?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      id = -1L;
    return id;
  }

  private IDBObject CreateNewDocumentByPrototype(
    ref AVSDocumentForm? groupForm_FromAttr,
    long docParentVersionId,
    IUserSession session)
  {
    IGroupInstanceService customService = session.GetCustomService(typeof (IGroupInstanceService)) as IGroupInstanceService;
    Guid sessionGuid = session.SessionGUID;
    customService.AddIgnoreSessionGuid(sessionGuid);
    IDBObject versionExTmpWrapper;
    try
    {
      versionExTmpWrapper = AvsIDCache.CreateVersionEx_TMPWrapper(this.DocumentDBObjectType, docParentVersionId, session);
    }
    finally
    {
      customService.RemoveIgnoreSessionGuid(sessionGuid);
    }
    IDBAttribute attributeById = versionExTmpWrapper.GetAttributeByID(AvsIDCache.Attr_SpecificationForm);
    if (attributeById != null)
      groupForm_FromAttr = AVSDocument.DecodeSpecificationFormAttrValue(attributeById.AsString);
    if (groupForm_FromAttr.HasValue)
    {
      this.AvsDocumentForm = groupForm_FromAttr.Value;
      if (this.productsInfo.Count > 1 && this.AvsDocumentForm == AVSDocumentForm.Single || !AVSDocumentsSettings.IsAllowableDocumentForm(this.AVSDocType, this.AvsDocumentForm))
        this.AvsDocumentForm = this.GetDefaultGroupDocumentForm();
    }
    else
      this.AvsDocumentForm = this.productsInfo.Count != 1 ? this.GetDefaultGroupDocumentForm() : AVSDocumentForm.Single;
    return versionExTmpWrapper;
  }

  /// <summary>
  /// Найти родительскую версию документа по родительским версиям исполнений
  /// </summary>
  private long SearchDocParentVersionId(int objType)
  {
    long num = -1;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, SortOrders.DESC, 0)
    };
    HybridDictionary hybridDictionary = new HybridDictionary();
    bool flag1 = false;
    List<long> longList = new List<long>();
    bool flag2 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.productsInfo.Count; ++index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.productsInfo[index].Id);
        if (!flag1 && (this.DocumentName == null || this.DocumentName == "" || this.DocumentDesignation == null || this.DocumentDesignation == ""))
        {
          this.GetDocumentAttributes(dbObject, false);
          flag1 = true;
          this.documentGuid = Guid.Empty;
        }
        if (objType == -1)
          objType = dbObject.ObjectType;
        hybridDictionary.Add((object) this.productsInfo[index].Id, (object) null);
        if (!Intermech.Consts.IsUndefinedObjectId(dbObject.ParentVersionID))
        {
          flag2 = AvsIDCache.ArticleIsRemovedFormGroupSpecification(dbObject);
          if (!flag2)
          {
            hybridDictionary.Add((object) dbObject.ParentVersionID, (object) null);
            num = AVSDocument.GetSpecificationIDForProduct(dbObject.ParentVersionID, sessionKeeper.Session);
          }
        }
        if (num == -1L)
          longList.Add(dbObject.ID);
        else
          break;
      }
      if (num == -1L)
      {
        if (!flag2)
        {
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-3, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
          }, columns);
          AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objType);
          objectCollection.ShowAllModifications = true;
          DataTable dataTable = objectCollection.Select(paramSet);
          if (dataTable != null)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
              if (!hybridDictionary.Contains((object) int64))
                num = AVSDocument.GetSpecificationIDForProduct(int64, sessionKeeper.Session);
              if (num != -1L)
                break;
            }
          }
        }
      }
    }
    return num;
  }

  internal void SaveAVSDocumentToDbIfNeed()
  {
    bool flag = this.Document.Modified;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject document = sessionKeeper.Session.GetObject(this.DocumentID, false);
      if (document == null)
        return;
      if (!flag)
      {
        if (AvsIDCache.GetIsNeedUpdateDocumentFlag(document))
          flag = true;
      }
    }
    if (this.IsSpecification && !flag && this.Document != null && this.Document.SavedDateTime.HasValue && this.ProductsInfo.Find((Predicate<ProductInfo>) (p => p.ModifyDate > this.Document.SavedDateTime.Value)) != null)
      flag = true;
    if (!flag)
      return;
    this.SaveAVSDocumentToDB();
  }

  /// <summary>Сохранить документ в БД и назначить нужные атрибуты</summary>
  public void SaveAVSDocumentToDB()
  {
    if (this.avsWindow != null)
      this.avsWindow.SaveDocument();
    else
      this.SaveAVSDocumentToDB_Internal();
  }

  /// <summary>Сохранить документ в БД и назначить нужные атрибуты, без обращения к форме.
  /// Внутренний метод, необходим чтобы разрулить дополнительные действия до и после сохранения из окна и без окна AVS</summary>
  internal void SaveAVSDocumentToDB_Internal()
  {
    if (this.VariableDataChapter != null)
    {
      foreach (Chapter chapter in this.VariableDataChapter.Chapters)
      {
        if (chapter is ProductVariableDataChapter)
          chapter.SaveProductInfoToDocNode();
      }
    }
    this.SaveParentProductsToImDocument();
    DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, this.Document, this.DefaultFileName, -1, false);
    if (this.AVSDocType == AVSDocumentType.UserAVSDocument)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAVSDocumentObject dbavsDocumentObject = AvsIDCache.GetDBAVSDocumentObject(sessionKeeper.Session, this.DocumentID);
      dbavsDocumentObject.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) this.EncodeSpecificationFormAttrValue(this.AvsDocumentForm))
      }, true);
      this.PatchDocumentFileDate((IDBObject) dbavsDocumentObject, DateTime.Now);
    }
  }

  private void SaveParentProductsToImDocument()
  {
    string attributeValue = "";
    for (int index = 0; index < this.parentProducts.Count; ++index)
      attributeValue = attributeValue + (index == 0 ? "" : ";") + this.parentProducts[index].Guid.ToString();
    if (attributeValue != "")
      this.document.SetAttributeValue(AVSDocument.DocAttr_ParentProductList, attributeValue, false, false, false);
    else
      this.document.RemoveAttribute(AVSDocument.DocAttr_ParentProductList, false, false);
  }

  private void LoadParentProductsFromDocument()
  {
    List<ProductInfo> products = new List<ProductInfo>();
    string attributeValue = this.document.GetAttributeValue(AVSDocument.DocAttr_ParentProductList, true);
    if (attributeValue != "")
    {
      string str = attributeValue;
      char[] chArray = new char[1]{ ';' };
      foreach (string g in str.Split(chArray))
        products.Add(new ProductInfo(new Guid(g), -1L, (string) null));
    }
    this.UpdateProductsInfoSource(products);
    this.parentProducts = products;
  }

  private void UpdateProductsInfoSource(List<ProductInfo> products)
  {
    if (products.IsNullOrEmpty<ProductInfo>())
      return;
    using (SessionKeeper sk = new SessionKeeper())
    {
      for (int index = 0; index < products.Count; ++index)
        this.UpdateProductInfoForNewSourceRelations(products[index], sk);
    }
  }

  private void UpdateProductInfoForNewSourceRelations(ProductInfo product, SessionKeeper sk)
  {
    ProductInfo productInfo;
    if (!(product.Guid != Guid.Empty))
    {
      List<ProductInfo> productsByRelations = this.productsByRelations;
      productInfo = productsByRelations != null ? productsByRelations.FirstOrDefault<ProductInfo>((System.Func<ProductInfo, bool>) (p => Math.Abs(p.Id) == Math.Abs(product.Id))) : (ProductInfo) null;
    }
    else
    {
      List<ProductInfo> productsByRelations = this.productsByRelations;
      productInfo = productsByRelations != null ? productsByRelations.FirstOrDefault<ProductInfo>((System.Func<ProductInfo, bool>) (p => p.Guid == product.Guid)) : (ProductInfo) null;
    }
    ProductInfo src1 = productInfo;
    if (src1 != null)
    {
      product.UpdateInfo(src1);
    }
    else
    {
      IDBObject dbObj = product.Guid != Guid.Empty ? sk.Session.GetObject(product.Guid, false) : sk.Session.GetObjectActualCopy(Math.Abs(product.Id), false);
      if (dbObj == null)
        return;
      List<ProductInfo> productsByRelations = this.productsByRelations;
      ProductInfo src2 = productsByRelations != null ? productsByRelations.FirstOrDefault<ProductInfo>((System.Func<ProductInfo, bool>) (p => p.F_ID == dbObj.ID)) : (ProductInfo) null;
      if (src2 != null)
        product.UpdateInfo(src2);
      else
        product.UpdateInfo(dbObj, this.productAttributeList, (string) null);
    }
  }

  public void PatchDocumentFileDate(IDBObject documentDBObject, DateTime dateTime)
  {
    IDBAttribute attributeById = documentDBObject.GetAttributeByID(this.Document.FileAttributeID);
    attributeById.Index = this.Document.FileAttributeIndex;
    if (!(attributeById is IBlobReader blobReader))
      return;
    BlobInformation blobInfo = blobReader.OpenBlob(-1);
    blobReader.CloseBlob();
    if (!(attributeById is IBlobWriter blobWriter))
      return;
    blobInfo.ModifyDate = dateTime;
    blobWriter.OpenBlob(blobInfo, true);
  }

  /// <summary>Расширения по умолчанию</summary>
  public string DefaultFileName
  {
    [DebuggerStepThrough] get
    {
      return DocumentEditorPlugin.GenerateDefaultFileNameForDB((ImDocumentData) this.Document) + this.DefaultFileExtension;
    }
  }

  /// <summary>Расширение файла конструкторского документа по умолчанию</summary>
  public string DefaultFileExtension
  {
    get
    {
      string defaultFileExtension = ".imdx";
      if (this.IsSpecification)
        defaultFileExtension = ".spx";
      else if (this.IsElementList)
        defaultFileExtension = ".pex";
      return defaultFileExtension;
    }
  }

  private void CreateUndoSnapshot(bool? createUndo)
  {
    bool flag = false;
    if (createUndo.HasValue)
      flag = createUndo.Value;
    if (AvsConfig.General.CreateUndo == CreateUndoEnum.Yes)
      flag = true;
    string str = "Редактирование спецификации " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    if (!this.IsSpecification)
      str = "Редактирование документа " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    if (!flag && AvsConfig.General.CreateUndo == CreateUndoEnum.Ask)
    {
      AddUndoSnapshotDialog undoSnapshotDialog = new AddUndoSnapshotDialog();
      undoSnapshotDialog.SnapshotName = str;
      if (undoSnapshotDialog.ShowDialog() == DialogResult.Yes)
      {
        str = undoSnapshotDialog.SnapshotName;
        flag = true;
      }
    }
    if (!flag)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBSnapshotCollection snapshotCollection = sessionKeeper.Session.GetSnapshotCollection();
      DateTime now = DateTime.Now;
      Guid guid = Guid.NewGuid();
      string snapshotName = $"{str}~{guid.ToString()}";
      long snapshotID = snapshotCollection.Create(this.DocumentID, snapshotName, this.FiltrationOwnerID);
      if (!this.IsSpecification)
        return;
      foreach (ProductInfo productInfo in this.productsInfo)
        snapshotCollection.AddObjectToSnapshot(productInfo.Id, snapshotID, snapshotName, this.FiltrationOwnerID, new List<long>());
    }
  }

  /// <summary>Создать пустой конструкторский документ</summary>
  /// <param name="session">Пользовательская сессия</param>
  public void InitEmptyImDocument(IUserSession session)
  {
    bool flag = false;
    this.documentIsModifiedByLoad = true;
    if (this.Document != null)
    {
      flag = this.Document.IsDocumentLoading;
      this.Document.IsDocumentLoading = true;
      this.Document.Clear(false, false);
    }
    this.LoadStdTemplate(this.avsDocTypeGuid, this.AvsDocumentForm, true);
    this.Document.IsDocumentLoading = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.MaterialKeyWordsSchema = (KeyWordsSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_MaterialKeyWordsSchema, typeof (KeyWordsSchema));
    this.Document.SetMaterialKeyWords((List<string>) this.MaterialKeyWordsSchema?.KeyWords);
    if (this.productsInfo.Count > 0)
    {
      IDBObject dbObject = session.GetObject(this.productsInfo[0].Id);
      if (this.DocumentName == null || this.DocumentName == "" || this.DocumentDesignation == null || this.DocumentDesignation == "")
      {
        this.GetDocumentAttributes(dbObject, false);
        this.documentGuid = Guid.Empty;
      }
    }
    this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, this.DocumentName, false, false, false);
    this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, this.DocumentDesignation, false, false, false);
    if (this.productsInfo != null && this.productsInfo.Count == 1)
      this.Document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
    else
      this.Document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, "", false, false, false);
    if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
      this.UpdateProductsInStampForMirrorSP();
    this.Document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
    this.Document.SetAttributeValue(AVSDocument.AddChapterLocation_DocAttribute, this.additionalChaptersInDataChapter ? "1" : "0", false, false, false);
    this.Document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, this.avsDocumentType.ToString(), false, false, false);
    this.Document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, this.avsDocTypeGuid.ToString(), false, false, false);
    if (this.documentTemplateGuid != Guid.Empty)
      this.Document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
    else
      this.Document.RemoveAttribute(AVSDocument.SpecTemplateGuid_DocAttribute, false, false);
    this.Document.IsDocumentLoading = flag;
  }

  public void ReplaceTemplate(IDBTypedObjectID template)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(template.ObjectID, false);
      if (dbObject != null)
      {
        this.documentTemplateGuid = dbObject.ObjectGUID;
        this.DocumentTemplateID = dbObject.ObjectID;
        this.ResetSettingsFromTemplate();
        this.document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, this.documentTemplateGuid.ToString(), false, false, false);
      }
    }
    this.SynchronizeDocument();
  }

  /// <summary>Загрузить новые атрибуты. Если все атрибуты уже есть в кэше, то ничего не грузит</summary>
  /// <param name="attrInfoList">Список атрибутов типа SpecRowAttributeInfo</param>
  /// <param name="updateViewNodes">Обновить узлы дерева документа и табличного вида</param>
  /// <returns>Возвращает true, </returns>
  public virtual bool LoadNewAttributes(
    List<AvsRowAttributeInfo> attrInfoList,
    bool updateViewNodes)
  {
    bool flag = false;
    try
    {
      this.newAttributesLoading = true;
      List<AvsRowAttributeInfo> minimalAttributeList1 = this.CreateMinimalAttributeList(AvsIDCache.Relation_Document);
      List<AvsRowAttributeInfo> minimalAttributeList2 = this.CreateMinimalAttributeList(AvsIDCache.Relation_Project);
      if (this.GetRelationTypesUsedInDocument().Count == 0)
      {
        for (int index = minimalAttributeList1.Count - 1; index >= 0; --index)
        {
          if (minimalAttributeList1[index].IsRelationAttribute)
            minimalAttributeList1.RemoveAt(index);
        }
        for (int index = minimalAttributeList2.Count - 1; index >= 0; --index)
        {
          if (minimalAttributeList2[index].IsRelationAttribute)
            minimalAttributeList2.RemoveAt(index);
        }
      }
      HybridDictionary hybridDictionary1 = new HybridDictionary();
      HybridDictionary hybridDictionary2 = new HybridDictionary();
      HybridDictionary hybridDictionary3 = new HybridDictionary();
      HybridDictionary hybridDictionary4 = new HybridDictionary();
      for (int index = 0; index < minimalAttributeList1.Count; ++index)
      {
        if (minimalAttributeList1[index].IsRelationAttribute)
          hybridDictionary1.Add((object) minimalAttributeList1[index].AttributeId, (object) minimalAttributeList1[index]);
        else
          hybridDictionary3.Add((object) minimalAttributeList1[index].AttributeId, (object) minimalAttributeList1[index]);
      }
      for (int index = 0; index < minimalAttributeList2.Count; ++index)
      {
        if (minimalAttributeList2[index].IsRelationAttribute)
          hybridDictionary2.Add((object) minimalAttributeList2[index].AttributeId, (object) minimalAttributeList2[index]);
        else
          hybridDictionary4.Add((object) minimalAttributeList2[index].AttributeId, (object) minimalAttributeList2[index]);
      }
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < attrInfoList.Count; ++index)
      {
        AvsRowAttributeInfo attrInfo = attrInfoList[index];
        if (attrInfo != null && !attrInfo.IsDocField)
        {
          if (attrInfo.IsRelationAttribute)
          {
            if (this.IsSpecification && !hybridDictionary1.Contains((object) attrInfo.AttributeId))
            {
              AttributeValueMap valueMapForRelation = this.GetAttributeValueMapForRelation(AvsIDCache.Relation_Document);
              if (valueMapForRelation != null && !valueMapForRelation.AttributeDictionary.ContainsKey(attrInfo.AttributeId))
              {
                hybridDictionary1.Add((object) attrInfo.AttributeId, (object) attrInfo);
                minimalAttributeList1.Add(attrInfo);
                ++num1;
              }
            }
            if ((this.IsSpecification || this.IsElementList) && !hybridDictionary2.Contains((object) attrInfo.AttributeId))
            {
              AttributeValueMap valueMapForRelation = this.GetAttributeValueMapForRelation(AvsIDCache.Relation_Project);
              if (valueMapForRelation != null && !valueMapForRelation.AttributeDictionary.ContainsKey(attrInfo.AttributeId))
              {
                hybridDictionary2.Add((object) attrInfo.AttributeId, (object) attrInfo);
                minimalAttributeList2.Add(attrInfo);
                ++num2;
              }
            }
          }
          else if (attrInfo.IsObjectAttribute)
          {
            if (this.docObjectAttrMap != null && !this.docObjectAttrMap.AttributeDictionary.ContainsKey(attrInfo.AttributeId) && !hybridDictionary3.Contains((object) attrInfo.AttributeId))
            {
              hybridDictionary3.Add((object) attrInfo.AttributeId, (object) attrInfo);
              minimalAttributeList1.Add(attrInfo);
              ++num1;
            }
            if (this.prjObjectAttrMap != null && !this.prjObjectAttrMap.AttributeDictionary.ContainsKey(attrInfo.AttributeId) && !hybridDictionary4.Contains((object) attrInfo.AttributeId))
            {
              hybridDictionary4.Add((object) attrInfo.AttributeId, (object) attrInfo);
              minimalAttributeList2.Add(attrInfo);
              ++num2;
            }
          }
        }
      }
      if (num1 + num2 > 0)
      {
        this.SuspendDocumentAndGridUpdates();
        try
        {
          this.FilterColumnDescriptorsForSpecRow(minimalAttributeList1, AvsIDCache.Relation_Document, false);
          ColumnDescriptor[] columnDescriptors1 = this.CreateColumnDescriptors(minimalAttributeList1);
          this.FilterColumnDescriptorsForSpecRow(minimalAttributeList2, AvsIDCache.Relation_Project, false);
          ColumnDescriptor[] columnDescriptors2 = this.CreateColumnDescriptors(minimalAttributeList2);
          AVSDocumentContext context = new AVSDocumentContext();
          context.SuspendUpdateDocRows = !updateViewNodes;
          RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument();
          if (this.IsSpecification || this.IsElementList)
          {
            DBRecordSetParams paramSet1 = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptors1, recordCount: this.PacketSize);
            DBRecordSetParams paramSet2 = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptors2, recordCount: this.PacketSize);
            AVSDocument.SetFiltrationTags(ref paramSet1, context);
            AVSDocument.SetFiltrationTags(ref paramSet2, context);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              foreach (int relationType in this.GetRelationTypesUsedInDocument())
              {
                for (int index = 0; index < this.productsInfo.Count; ++index)
                {
                  if (this.productsInfo[index].Id != -1L)
                  {
                    context.Product = this.productsInfo[index];
                    flag = true;
                    if (relationType == AvsIDCache.Relation_Document && num1 > 0)
                      this.LoadPartsData(paramSet1, context.Product, true, AvsIDCache.Relation_Document, true, false, false, context, sessionKeeper.Session, (Dictionary<long, AVSRow>) null, rowDicts);
                    else if (num2 > 0)
                      this.LoadPartsData(paramSet2, context.Product, true, relationType, true, false, false, context, sessionKeeper.Session, (Dictionary<long, AVSRow>) null, rowDicts);
                  }
                }
                context.Product = (ProductInfo) null;
                for (int index = 0; index < this.parentProducts.Count; ++index)
                {
                  flag = true;
                  if (relationType == AvsIDCache.Relation_Document && num1 > 0)
                    this.LoadPartsData(paramSet1, this.parentProducts[index], true, AvsIDCache.Relation_Document, true, false, false, context, sessionKeeper.Session, (Dictionary<long, AVSRow>) null, rowDicts);
                  else if (num2 > 0)
                    this.LoadPartsData(paramSet2, this.parentProducts[index], true, relationType, true, false, false, context, sessionKeeper.Session, (Dictionary<long, AVSRow>) null, rowDicts);
                }
              }
            }
            this.ReloadDopzamenTextForGroup((List<long>) null, false);
          }
          if (this.objectDictionary.Count > 0)
          {
            List<long> objectIDs = new List<long>();
            List<int> objectTypes = new List<int>();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in this.objectDictionary)
              {
                if (keyValuePair.Value.Count > 0 && keyValuePair.Value.Any<AVSRow>((System.Func<AVSRow, bool>) (r => !r.HasAnyRelations)))
                {
                  objectIDs.Add(keyValuePair.Key);
                  objectTypes.Add(keyValuePair.Value[0].ObjType);
                }
              }
              this.LoadRowsForDBObjects(objectIDs, objectTypes, columnDescriptors1, columnDescriptors2, false, context, updateViewNodes, sessionKeeper.Session, rowDicts);
            }
          }
          if (updateViewNodes)
            this.UpdateNoteDocCells(false, false);
        }
        finally
        {
          this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        }
      }
      this.newAttributesLoading = false;
    }
    catch (Exception ex)
    {
      this.newAttributesLoading = false;
      throw;
    }
    return flag;
  }

  /// <summary>Обновить атрибуты объектов состава</summary>
  /// <param name="objectsIDs">Идентификаторы версий объектов состава</param>
  public void ReloadObjectsAttributesFromDB(IList<long> objectsIDs, AVSRow avsRow = null)
  {
    bool flag = false;
    List<long> objectIDs = new List<long>(objectsIDs.Count);
    List<int> objectTypes = new List<int>(objectsIDs.Count);
    for (int index1 = 0; index1 < objectsIDs.Count; ++index1)
    {
      if (objectsIDs[index1] == this.DocumentID)
      {
        string documentDesignation = this.DocumentDesignation;
        string documentName = this.DocumentName;
        string documentCaption = this.DocumentCaption;
        List<AttributeValues> attributeValuesList = new List<AttributeValues>();
        if (this.Document.DBAttributeProcessorDictionary is AttributeProcessorDictionary processorDictionary && processorDictionary.ContainsKey(this.DocumentID))
          processorDictionary.Remove(this.DocumentID);
        this.GetDocumentAttributes(this.DocumentID, true);
        if (!this.IsSpecification && this.productsInfo.Count > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject productObj = sessionKeeper.Session.GetObject(this.DocumentID, false);
            if (productObj != null)
              this.productsInfo[0].UpdateInfo(productObj, (List<int>) null, this.DocumentDesignationSuffix);
          }
          flag = true;
        }
      }
      else
      {
        for (int index2 = 0; index2 < this.productsInfo.Count; ++index2)
        {
          if (objectsIDs[index1] == this.productsInfo[index2].Id)
          {
            this.productsInfo[index2].UpdateInfo(this.productAttributeList, this.DocumentDesignationSuffix);
            flag = true;
            break;
          }
        }
        List<AVSRow> avsRowList;
        this.objectDictionary.TryGetValue(objectsIDs[index1], out avsRowList);
        if (avsRowList != null && avsRowList.Count > 0)
        {
          objectIDs.Add(objectsIDs[index1]);
          objectTypes.Add(avsRowList[0].ObjType);
        }
      }
    }
    if (flag)
    {
      if (this.document != null && this.productsInfo != null && this.productsInfo.Count == 1)
        this.document.SetAttributeValue(AVSDocument.DocumentAttribute_OKPCode, this.productsInfo[0].ProductOKPCode, false, false, false);
      if (this.AvsDocumentForm == AVSDocumentForm.Mirror)
        this.UpdateProductsInStampForMirrorSP();
      this.UpdateProductHeadersOnPages(true, true);
    }
    if (objectIDs.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AVSDocumentContext context = new AVSDocumentContext();
      context.Row = avsRow;
      if (this.IsSpecification)
        context.UpdateDraftRows = true;
      RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument();
      this.LoadRowsForDBObjects(objectIDs, objectTypes, (ColumnDescriptor[]) null, (ColumnDescriptor[]) null, false, context, true, sessionKeeper.Session, rowDicts);
    }
  }

  /// <summary>Обновить атрибуты объекта состава</summary>
  /// <param name="objectID">Идентификаторы версий объекта состава</param>
  public void ReloadObjectAttributesFromDB(long objectID, AVSRow avsRow = null)
  {
    IList<long> objectsIDs = (IList<long>) new List<long>(1);
    objectsIDs.Add(objectID);
    this.ReloadObjectsAttributesFromDB(objectsIDs, avsRow);
  }

  /// <summary>Обновить атрибуты связей</summary>
  /// <param name="relationsTypedDictionary">Идентификаторы связей</param>
  public void ReloadRelationsAttributesFromDB(
    Dictionary<int, List<long>> relationsTypedDictionary)
  {
    if (relationsTypedDictionary.Count == 0)
      return;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      AVSDocumentContext context = new AVSDocumentContext();
      List<AVSRow> avsRowList = this.LoadRelationsData(relationsTypedDictionary, context);
      foreach (AVSRow avsRow in avsRowList)
        avsRow.NeedUpdateStructure = true;
      this.ReloadDopzamenTextForGroup((List<long>) null, true);
      this.UpdateDocumentStructure(false, false, false);
      this.UpdateNoteDocCells(false, false);
      this.IndexAVSDocument(true);
      foreach (AVSRow avsRow in avsRowList)
      {
        if (avsRow.NeedUpdateDocRow)
        {
          this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          break;
        }
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  /// <summary>Очистить часть перед удалением</summary>
  /// <param name="chapter">Часть</param>
  public void ClearRootChapter(Chapter chapter)
  {
    if (chapter == null)
      throw new ArgumentNullException(nameof (chapter));
    if (chapter.HasDocNodes)
    {
      for (int index = 0; index < chapter.DocNodes.Count; ++index)
      {
        chapter.DocNodes[index].UniteTable();
        chapter.DocNodes[index].Remove(false, false);
      }
    }
    if (!chapter.HasDocNodesExp)
      return;
    for (int index = 0; index < chapter.DocNodesExp.Count; ++index)
    {
      chapter.DocNodesExp[index].UniteTable();
      chapter.DocNodesExp[index].Remove(false, false);
    }
  }

  /// <summary>Получить соответствующего владельца для раздела согласно исполнению или разделу данных и заданной части</summary>
  /// <param name="rowProduct">Информация об исполнении или разделе данных</param>
  /// <param name="chapterSettings">Информация о части</param>
  /// <returns></returns>
  public Chapter GetNewSectionOwner(
    ProductInfo rowProduct,
    AdditionalChapterSettings chapterSettings)
  {
    bool isGridViewMode = this.IsGridViewMode;
    Chapter chapter1 = (Chapter) null;
    Chapter chapter2 = (Chapter) null;
    if (!this.AdditionalChaptersInDataChapter)
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
      {
        if (this.rootChapters[index].IsAdditionalChapter && this.rootChapters[index].ChapterGuid == chapterSettings.ChapterGuid)
          chapter1 = this.rootChapters[index];
      }
      if (chapter1 == null)
        this.AddRootChapter(chapter1 = (Chapter) new AdditionalChapter(this, chapterSettings, this.AdditionalChaptersInDataChapter), true);
      if (rowProduct.IsCommonData)
      {
        for (int index = 0; index < chapter1.Chapters.Count; ++index)
        {
          if (chapter1.Chapters[index].IsCommonDataChapter)
          {
            chapter2 = chapter1.Chapters[index];
            break;
          }
        }
      }
      else if (rowProduct.IsVariableData && this.AvsDocumentForm == AVSDocumentForm.V)
      {
        for (int index = 0; index < chapter1.Chapters.Count; ++index)
        {
          if (chapter1.Chapters[index] is VariableDataChapterFormV)
          {
            chapter2 = chapter1.Chapters[index];
            break;
          }
        }
      }
      else if (this.AvsDocumentForm == AVSDocumentForm.A)
      {
        VariableDataChapterFormA dataChapterFormA = (VariableDataChapterFormA) null;
        for (int index = 0; index < chapter1.Chapters.Count; ++index)
        {
          if (chapter1.Chapters[index] is VariableDataChapterFormA)
          {
            dataChapterFormA = chapter1.Chapters[index] as VariableDataChapterFormA;
            break;
          }
        }
        if (dataChapterFormA != null)
          chapter2 = dataChapterFormA.GetProductChapter(rowProduct);
      }
    }
    else if (rowProduct.IsCommonData)
    {
      chapter2 = this.CommonDataChapter.GetChapter(chapterSettings.ChapterGuid);
      if (chapter2 == null)
      {
        chapter2 = (Chapter) new AdditionalChapter(this, chapterSettings, this.AdditionalChaptersInDataChapter);
        this.CommonDataChapter.AddChapter(chapter2, true, true, isGridViewMode, (TableData) null);
      }
    }
    else if (rowProduct.IsVariableData && this.AvsDocumentForm == AVSDocumentForm.V && this.VariableDataChapter_FormV != null)
    {
      chapter2 = this.VariableDataChapter_FormV.GetChapter(chapterSettings.ChapterGuid);
      if (chapter2 == null)
      {
        chapter2 = (Chapter) new AdditionalChapter(this, chapterSettings, this.AdditionalChaptersInDataChapter);
        this.VariableDataChapter_FormV.AddChapter(chapter2, true, true, isGridViewMode, (TableData) null);
      }
    }
    else if (this.AvsDocumentForm == AVSDocumentForm.A && this.VariableDataChapter_FormA != null)
    {
      Chapter productChapter = this.VariableDataChapter_FormA.GetProductChapter(rowProduct);
      if (productChapter != null)
      {
        chapter2 = productChapter.GetChapter(chapterSettings.ChapterGuid);
        if (chapter2 == null)
        {
          chapter2 = (Chapter) new AdditionalChapter(this, chapterSettings, this.AdditionalChaptersInDataChapter);
          productChapter.AddChapter(chapter2, true, true, isGridViewMode, (TableData) null);
        }
      }
    }
    return chapter2;
  }

  /// <summary>
  /// Принудительно вызвать обновление структуры документа с пересбором записей с количеством в разных исполнениях
  /// </summary>
  public void ForceUpdateDocumentStructureWithFindEqualRows()
  {
    try
    {
      this.SuspendDocumentAndGridUpdates();
      foreach (AVSRow row in this.GetRows())
        row.NeedUpdateStructure = true;
      this.UpdateDocumentStructure(true, false, true);
      this.IndexAVSDocument(true);
      this.UpdateViewNodes(false, false, true, false, false, EmptyRowUpdateMode.DontChange);
      this.UpdateVariableDataCaptions();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  /// <summary>Обновить структуру всего конструкторского документа.
  /// Объединить и поместить в общие данные связи с одинаковыми данными,
  /// в общих данных разобрать и поместить в переменные данные связи с различающимися данными</summary>
  /// <param name="forceUpdate">Проверять все записи, не смотря на значение NeedUpdateStructure</param>
  /// <param name="createNewViewNodes">Создавать записи в документе, если их не было (метод UpdateViewNodes())</param>
  /// <param name="findEqualRowsInVariableData">Искать записи в переменных данных, которые можно объединить</param>
  /// <param name="removeEmptyChapters">Удалить пустые разделы, после перемещения записей</param>
  /// <param name="updateDraftForParts">Вызвать проверку и добавление заготовок после обновления структуры</param>
  internal void UpdateDocumentStructure(
    bool forceUpdate,
    bool createNewViewNodes,
    bool findEqualRowsInVariableData,
    bool removeEmptyChapters = false,
    bool updateDraftForParts = false)
  {
    AdditionalChapter additionalChapter = (AdditionalChapter) null;
    if (!this.AdditionalChaptersInDataChapter && (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V))
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
      {
        if (this.rootChapters[index].IsAdditionalChapter)
        {
          additionalChapter = this.rootChapters[index] as AdditionalChapter;
          break;
        }
      }
    }
    if (this.AvsDocumentForm == AVSDocumentForm.A)
    {
      if (this.variableDataChapter_FormA == null)
        this.VariableDataChapter_FormA = new VariableDataChapterFormA(this, this.productsInfo, true);
      if (additionalChapter != null && additionalChapter.InnerVariableData_FormA == null)
        additionalChapter.AddChapter((Chapter) new VariableDataChapterFormA(this, this.productsInfo, true), false, false, false, (TableData) null);
    }
    if (this.AvsDocumentForm == AVSDocumentForm.V)
    {
      if (this.variableDataChapter_FormV == null)
        this.VariableDataChapter_FormV = new VariableDataChapterFormV(this);
      if (additionalChapter != null && additionalChapter.InnerVariableData_FormV == null)
        additionalChapter.AddChapter((Chapter) new VariableDataChapterFormV(this), false, false, false, (TableData) null);
    }
    AVSViewMode avsViewMode = AVSViewMode.Page;
    try
    {
      if (this.avsWindow != null)
      {
        avsViewMode = this.avsWindow.viewMode;
        this.avsWindow.viewMode = AVSViewMode.Page;
      }
      this.UpdateChapterStructure(this.commonDataChapter, (Chapter) this.VariableDataChapter_FormA, (Chapter) this.VariableDataChapter_FormV, forceUpdate, findEqualRowsInVariableData, removeEmptyChapters);
      if (!this.AdditionalChaptersInDataChapter)
      {
        for (int index = this.commonDataChapter.Chapters.Count - 1; index >= 0; --index)
        {
          if (this.commonDataChapter.Chapters[index].IsAdditionalChapter)
            this.commonDataChapter.RemoveChapter(this.commonDataChapter.Chapters[index], false, false, true, true);
        }
      }
      if (this.AvsDocumentForm != AVSDocumentForm.A)
      {
        if (this.variableDataChapter_FormA != null)
        {
          for (int index = 0; index < this.variableDataChapter_FormA.DocNodes.Count; ++index)
          {
            this.variableDataChapter_FormA.DocNodes[index].UniteTable();
            this.variableDataChapter_FormA.DocNodes[index].Remove(false, false);
          }
          if (this.variableDataChapter_FormA.DocNodesExp != null)
          {
            for (int index = 0; index < this.variableDataChapter_FormA.DocNodesExp.Count; ++index)
            {
              this.variableDataChapter_FormA.DocNodesExp[index].UniteTable();
              this.variableDataChapter_FormA.DocNodesExp[index].Remove(false, false);
            }
          }
          this.VariableDataChapter_FormA = (VariableDataChapterFormA) null;
        }
      }
      else if (!this.AdditionalChaptersInDataChapter && this.variableDataChapter_FormA != null)
      {
        for (int index1 = 0; index1 < this.variableDataChapter_FormA.Chapters.Count; ++index1)
        {
          for (int index2 = this.variableDataChapter_FormA.Chapters[index1].Chapters.Count - 1; index2 >= 0; --index2)
          {
            if (this.variableDataChapter_FormA.Chapters[index1].Chapters[index2].IsAdditionalChapter)
              this.variableDataChapter_FormA.Chapters[index1].RemoveChapter(this.variableDataChapter_FormA.Chapters[index1].Chapters[index2], false, false, true, true);
          }
        }
      }
      if (this.AvsDocumentForm != AVSDocumentForm.V)
      {
        if (this.VariableDataChapter_FormV != null)
          this.ClearVariableDataChapter_FormV();
      }
      else if (!this.AdditionalChaptersInDataChapter && this.variableDataChapter_FormV != null)
      {
        for (int index = 0; index < this.variableDataChapter_FormV.Chapters.Count; ++index)
        {
          if (this.variableDataChapter_FormV.Chapters[index].IsAdditionalChapter)
            this.variableDataChapter_FormV.RemoveChapter(this.variableDataChapter_FormV.Chapters[index], false, false, true, true);
        }
      }
      for (int index3 = 0; index3 < this.rootChapters.Count; ++index3)
      {
        if (!this.rootChapters[index3].IsCommonDataChapter && this.rootChapters[index3] != this.VariableDataChapter)
        {
          Chapter commonData = (Chapter) null;
          Chapter chapter1 = (Chapter) null;
          Chapter chapter2 = (Chapter) null;
          for (int index4 = 0; index4 < this.rootChapters[index3].Chapters.Count; ++index4)
          {
            if (this.rootChapters[index3].Chapters[index4].IsCommonDataChapter)
              commonData = this.rootChapters[index3].Chapters[index4];
            else if (this.rootChapters[index3].Chapters[index4].IsVariableDataChapter)
            {
              if (this.rootChapters[index3].Chapters[index4] is VariableDataChapterFormV)
                chapter2 = this.rootChapters[index3].Chapters[index4];
              else
                chapter1 = this.rootChapters[index3].Chapters[index4];
            }
            if (commonData != null && chapter1 != null && chapter2 != null)
              break;
          }
          if (commonData == null)
            commonData = this.CreateCommonDataChapter(!this.IsSpecification);
          if (this.AvsDocumentForm == AVSDocumentForm.A && chapter1 == null)
            chapter1 = (Chapter) new VariableDataChapterFormA(this, this.productsInfo, true);
          if (this.AvsDocumentForm == AVSDocumentForm.V && chapter2 == null)
            chapter2 = (Chapter) new VariableDataChapterFormV(this);
          this.UpdateChapterStructure(commonData, chapter1, chapter2, forceUpdate, findEqualRowsInVariableData, removeEmptyChapters);
          if (this.AvsDocumentForm != AVSDocumentForm.V && chapter2 != null)
          {
            this.ClearVariableDataChapter_FormV(chapter2);
            this.rootChapters[index3].RemoveChapter(chapter2, false, false, false, false);
          }
          else if (this.AvsDocumentForm != AVSDocumentForm.A && chapter1 != null)
          {
            if (chapter1.HasDocNodes)
            {
              for (int index5 = 0; index5 < chapter1.DocNodes.Count; ++index5)
              {
                chapter1.DocNodes[index5].UniteTable();
                chapter1.DocNodes[index5].Remove(false, false);
              }
            }
            if (chapter1.HasDocNodesExp)
            {
              for (int index6 = 0; index6 < chapter1.DocNodesExp.Count; ++index6)
              {
                chapter1.DocNodesExp[index6].UniteTable();
                chapter1.DocNodesExp[index6].Remove(false, false);
              }
            }
            this.rootChapters[index3].RemoveChapter(chapter1, false, false, false, false);
          }
        }
      }
      if (this.AdditionalChaptersInDataChapter)
      {
        for (int index = this.rootChapters.Count - 1; index >= 0; --index)
        {
          if (this.rootChapters[index].IsAdditionalChapter)
          {
            this.ClearRootChapter(this.rootChapters[index]);
            this.rootChapters.RemoveAt(index);
          }
        }
      }
    }
    finally
    {
      if (this.avsWindow != null)
        this.avsWindow.viewMode = avsViewMode;
    }
    if (createNewViewNodes)
      this.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
    if (!updateDraftForParts)
      return;
    this.UpdateDraftForParts();
  }

  protected virtual void UpdateDraftForParts()
  {
    int num = this._processingUpdateDraftForParts ? 1 : 0;
  }

  /// <summary>Обновить структуру части конструкторского документа содержащей подразделы общих и переменных данных.
  /// Объединить и поместить в общие данные связи с одинаковыми данными,
  /// в общих данных разобрать и поместить в переменные данные связи с различающимися данными</summary>
  /// <param name="commonData">Общие данные документа</param>
  /// <param name="varDataA">Переменные данные формы А</param>
  /// <param name="varDataV">Переменные данные формы В</param>
  /// <param name="forceUpdate">Проверять все записи, не смотря на значение NeedUpdateStructure</param>
  /// <param name="findEqualRowsInVariableData">Искать записи в переменных данных, которые можно объединить</param>
  /// <param name="removeEmptyChapters">Удалить пустые разделы, после перемещения записей</param>
  internal void UpdateChapterStructure(
    Chapter commonData,
    Chapter varDataA,
    Chapter varDataV,
    bool forceUpdate,
    bool findEqualRowsInVariableData,
    bool removeEmptyChapters = false)
  {
    if (commonData == null)
      throw new ArgumentNullException(nameof (commonData));
    bool flag1 = false;
    long tmpSortIndex = -1;
    Chapter parent = commonData.Parent;
    List<Chapter> chapters_CheckForRemove = new List<Chapter>();
    List<AVSRow> avsRowList1 = new List<AVSRow>();
    List<AVSRow> avsRowList2 = new List<AVSRow>();
    commonData.GetAllRowsList(true, true, avsRowList1);
    foreach (AVSRow avsRow1 in avsRowList1)
    {
      AVSRow avsRow = avsRow1;
      SpecificationSection section = avsRow.Section;
      if (!avsRow.HasRelation)
      {
        avsRow.NeedUpdateStructure = false;
      }
      else
      {
        if ((this.AvsDocumentForm == AVSDocumentForm.Single || this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V) && (forceUpdate || avsRow.NeedUpdateStructure))
        {
          if (!avsRow.HasHiddenRelation && this.TryMoveSubstitutionRelationsToAllowableRows(avsRow) && this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove))
          {
            avsRow.NeedUpdateStructure = false;
            continue;
          }
          if (this.AvsDocumentForm == AVSDocumentForm.Single)
          {
            this.MoveHiddenRelationsWithExcessPosDesignations(avsRow, false, (Chapter) avsRow.Section, ref tmpSortIndex);
            this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove);
            avsRow.NeedUpdateStructure = false;
            continue;
          }
          bool isEqualRelations = true;
          for (int index = 1; isEqualRelations && index < avsRow.Relations.Count; ++index)
            isEqualRelations = this.AvsRowsIsEqual(avsRow.Relations[0], avsRow.Relations[index], true, true);
          if (isEqualRelations && avsRow.Relations.Count != this.productsInfo.Count)
            this.CollectAllowableFreeRelationsForCommonDataRow(removeEmptyChapters, avsRow, ref isEqualRelations, flag1, chapters_CheckForRemove);
          this.MoveHiddenRelationsWithExcessPosDesignations(avsRow, true, this.IsFormA ? varDataA : varDataV, ref tmpSortIndex);
          if (!this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove))
          {
            isEqualRelations = true;
            for (int index = 1; isEqualRelations && index < avsRow.Relations.Count; ++index)
              isEqualRelations = this.AvsRowsIsEqual(avsRow.Relations[0], avsRow.Relations[index], true, true);
            isEqualRelations &= avsRow.Relations.Count == this.productsInfo.Count;
            if (!isEqualRelations)
            {
              if (this.AvsDocumentForm == AVSDocumentForm.A)
              {
                if (varDataA == null)
                  throw new ArgumentNullException(nameof (varDataA));
                List<TableData> docNodes = avsRow.DocNodes;
                TableData docNodeExp = avsRow.DocNodeExp;
                avsRow.DocNodes = new List<TableData>();
                avsRow.DocNodeExp = (TableData) null;
                for (int index1 = avsRow.Relations.Count - 1; index1 >= 0; --index1)
                {
                  RelationAttributeValuesCache relation = avsRow.Relations[index1];
                  SpecificationSection rowInVariableDataA = this.FindOrCreateSectionForRowInVariableDataA(avsRow, varDataA, relation.ProjectId);
                  AVSRow newAvsRow = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) rowInVariableDataA, rowInVariableDataA.Product, rowInVariableDataA.SectionID, avsRow.AdditionalChapterGuid).Find((Predicate<AVSRow>) (row => row.CheckRelation_IsHiddenRelation(relation)));
                  if (newAvsRow != null)
                  {
                    avsRow.RemoveRelationData(avsRow.Relations, index1);
                    newAvsRow.AddRowData(relation, addToHidden: true);
                  }
                  else
                  {
                    newAvsRow = this.MoveRelationToNewAvsRow(avsRow, avsRow.Relations, index1);
                    newAvsRow.SortIndex = this.FindNextFreeSortIndex(tmpSortIndex--);
                  }
                  AVSDocument.MoveHiddenRelationsToAvsRow(avsRow, newAvsRow);
                  if (newAvsRow.Section == null)
                  {
                    rowInVariableDataA.AddRow(newAvsRow, true);
                    if (index1 != 0 && docNodes != null)
                    {
                      List<TableData> tableDataList = new List<TableData>();
                      for (int index2 = 0; index2 < docNodes.Count; ++index2)
                        tableDataList.Add((TableData) docNodes[index2].Clone());
                      newAvsRow.DocNodes = tableDataList;
                    }
                    else
                      newAvsRow.DocNodes = docNodes;
                    if (docNodeExp != null)
                      newAvsRow.DocNodeExp = docNodeExp;
                  }
                }
                if (avsRow.HasHiddenRelation)
                {
                  for (int index = avsRow.HiddenRelations.Count - 1; index >= 0; --index)
                  {
                    SpecificationSection rowInVariableDataA = this.FindOrCreateSectionForRowInVariableDataA(avsRow, varDataA, avsRow.HiddenRelations[index].ProjectId);
                    this.MoveHiddenRelationToAllowableRow(avsRow, index, rowInVariableDataA, ref tmpSortIndex);
                  }
                }
                this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove);
              }
              else
              {
                SpecificationSection rowInVariableDataV = this.FindOrCreateSectionForRowInVariableDataV(avsRow, varDataV);
                List<AVSRow> avsRowsByPartId = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) rowInVariableDataV, rowInVariableDataV.Product, rowInVariableDataV.SectionID, avsRow.AdditionalChapterGuid);
                for (int index = avsRow.Relations.Count - 1; index >= 0; --index)
                {
                  RelationAttributeValuesCache relation = avsRow.Relations[index];
                  AVSRow avsRow2 = avsRowsByPartId.Find((Predicate<AVSRow>) (row =>
                  {
                    Guid? additionalChapterGuid1 = avsRow.AdditionalChapterGuid;
                    Guid? additionalChapterGuid2 = row.AdditionalChapterGuid;
                    return (additionalChapterGuid1.HasValue == additionalChapterGuid2.HasValue ? (additionalChapterGuid1.HasValue ? (additionalChapterGuid1.GetValueOrDefault() == additionalChapterGuid2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && row.IsAllowableRelation(relation);
                  }));
                  if (avsRow2 != null)
                  {
                    avsRow.RemoveRelationData(avsRow.Relations, index);
                    bool addToHidden = avsRow2.CheckRelation_IsHiddenRelation(relation);
                    avsRow2.AddRowData(relation, addToHidden: addToHidden);
                  }
                }
                if (avsRow.HasHiddenRelation)
                {
                  for (int index = avsRow.HiddenRelations.Count - 1; index >= 0; --index)
                  {
                    RelationAttributeValuesCache hiddenRelation = avsRow.HiddenRelations[index];
                    AVSRow avsRow3 = avsRowsByPartId.Find((Predicate<AVSRow>) (row =>
                    {
                      Guid? additionalChapterGuid3 = avsRow.AdditionalChapterGuid;
                      Guid? additionalChapterGuid4 = row.AdditionalChapterGuid;
                      return (additionalChapterGuid3.HasValue == additionalChapterGuid4.HasValue ? (additionalChapterGuid3.HasValue ? (additionalChapterGuid3.GetValueOrDefault() == additionalChapterGuid4.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && row.IsAllowableRelation(hiddenRelation);
                    }));
                    if (avsRow3 != null)
                    {
                      avsRow.RemoveRelationData(avsRow.HiddenRelations, index);
                      bool addToHidden = avsRow3.CheckRelation_IsHiddenRelation(hiddenRelation);
                      avsRow3.AddRowData(hiddenRelation, addToHidden: addToHidden);
                    }
                  }
                }
                if (avsRow.HasRelation || avsRow.HasHiddenRelation)
                {
                  avsRow.Section.MoveRow(avsRow, rowInVariableDataV, true, true, true);
                  if (avsRow.HasHiddenRelation)
                  {
                    for (int index = avsRow.HiddenRelations.Count - 1; index >= 0; --index)
                    {
                      RelationAttributeValuesCache hiddenRelation = avsRow.HiddenRelations[index];
                      if (avsRow.IsAllowableRelation(hiddenRelation, notHiddenOnly: true))
                      {
                        avsRow.RemoveRelationData(avsRow.HiddenRelations, index);
                        avsRow.AddRowData(hiddenRelation);
                      }
                    }
                  }
                  avsRow.UpdateDocRow((TableData) null, this.docRowFields_VarFormV, false, true, true, EmptyRowUpdateMode.Delete);
                }
                else
                  this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove);
              }
              if (section != null && section.Parent != null && section.IsEmpty && ((section.DocNodes == null ? 1 : (section.DocNodes.Count == 0 ? 1 : 0)) | (removeEmptyChapters ? 1 : 0)) != 0)
              {
                if (section.Parent.IsAdditionalChapter)
                  chapters_CheckForRemove.Add(section.Parent);
                section.Parent.RemoveChapter((Chapter) section, false, false, false, this.ViewMode == AVSViewMode.Grid);
              }
              avsRow.NeedUpdateStructure = false;
              continue;
            }
          }
          else
            continue;
        }
        if (avsRow != null && this.IsSpecification)
        {
          Chapter chapter = !(avsRow.GetRootChapter() is AdditionalChapter rootChapter) ? commonData : this.GetNewSectionOwner(commonData.Product, rootChapter.GetChapterSettings());
          SpecificationSection newSection = (SpecificationSection) chapter.GetChapter(avsRow.SectionID);
          if (avsRow.Section != newSection)
          {
            if (newSection == null)
              chapter.AddChapter((Chapter) (newSection = this.CreateSection(avsRow.SectionID)), true, false, false, (TableData) null);
            avsRow.Section.MoveRow(avsRow, newSection, true, flag1, true);
          }
        }
        if (!this.IsFormB || !findEqualRowsInVariableData && avsRow.Relations.Count == this.productsInfo.Count && !avsRow.IsFreeSortIndex)
          avsRow.NeedUpdateStructure = false;
      }
    }
    if (varDataA != null)
    {
      Guid empty = Guid.Empty;
      List<AVSRow> avsRowList3 = new List<AVSRow>();
      for (int index3 = 0; index3 < varDataA.Chapters.Count; ++index3)
      {
        ProductVariableDataChapter chapter1 = (ProductVariableDataChapter) varDataA.Chapters[index3];
        avsRowList1.Clear();
        List<AVSRow> rowList = avsRowList1;
        chapter1.GetAllRowsList(true, true, rowList);
        foreach (AVSRow avsRow4 in avsRowList1)
        {
          bool flag2 = findEqualRowsInVariableData | forceUpdate && (this.AvsDocumentForm != AVSDocumentForm.A || index3 <= 0 || avsRow4.IsFreeSortIndex);
          if (flag2)
          {
            if (!avsRow4.HasAnyRelations && !avsRow4.IsNoteRow)
            {
              this.RemoveRowAndChapterIfEmpty(avsRow4, removeEmptyChapters, chapters_CheckForRemove);
              continue;
            }
            if (!forceUpdate && !avsRow4.NeedUpdateStructure)
              continue;
          }
          if (avsRow4.HasRelation && !avsRow4.HasHiddenRelation)
          {
            bool flag3 = false;
            List<AVSRow> avsRowList4 = (List<AVSRow>) null;
            for (int index4 = avsRow4.Relations.Count - 1; index4 >= 0; --index4)
            {
              long valueInt64 = avsRow4.Relations[index4].GetValueInt64(AvsIDCache.Attr_DopZamenGroupNum, false);
              string valueString = avsRow4.Relations[index4].GetValueString(this.Field_Position, false);
              if (valueInt64 != -1L)
              {
                if (avsRowList4 == null)
                  avsRowList4 = this.FindAvsRowsByPartId(avsRow4.ObjectId, (Chapter) avsRow4.Section, avsRow4.Product, avsRow4.SectionID, avsRow4.AdditionalChapterGuid);
                for (int index5 = 0; index5 < avsRowList4.Count; ++index5)
                {
                  if (avsRowList4[index5].HasRelation && avsRowList4[index5] != avsRow4 && avsRowList4[index5].CheckRelation_IsHiddenDopZamen(valueInt64, valueString, avsRow4.Relations[index4].ProjectId))
                  {
                    RelationAttributeValuesCache relation = avsRow4.Relations[index4];
                    avsRow4.RemoveRelationData(avsRow4.Relations, index4);
                    avsRowList4[index5].AddRowData(relation, addToHidden: true);
                    if (avsRowList4[index5].SortIndex != relation.SortIndex)
                      avsRowList4[index5].SetFieldValue(this.Attr_SortIndex, avsRowList4[index5].HiddenRelations.Count - 1, -1, avsRowList4[index5].HiddenRelations, (object) avsRowList4[index5].SortIndex, this.IsSpecification, true, false, false, false, false);
                    flag3 = true;
                    break;
                  }
                }
              }
            }
            if (flag3 && this.RemoveRowAndChapterIfEmpty(avsRow4, removeEmptyChapters, chapters_CheckForRemove))
            {
              avsRow4.NeedUpdateStructure = false;
              continue;
            }
          }
          AdditionalChapter rootChapter = avsRow4.GetRootChapter() as AdditionalChapter;
          bool flag4 = false;
          if (flag2)
          {
            avsRowList3.Clear();
            avsRowList3.Add(avsRow4);
            if (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V)
              avsRow4.GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
            bool flag5 = findEqualRowsInVariableData || avsRow4.IsFreeSortIndex;
            if (flag5)
            {
              flag4 = true;
              for (int index6 = findEqualRowsInVariableData || !avsRow4.IsFreeSortIndex ? index3 + 1 : 0; index6 < varDataA.Chapters.Count; ++index6)
              {
                if (index6 != index3)
                {
                  ProductVariableDataChapter chapter2 = (ProductVariableDataChapter) varDataA.Chapters[index6];
                  List<AVSRow> avsRowsByPartId = chapter2.FindAvsRowsByPartId(avsRow4.ObjectId, chapter2.Product, avsRow4.SectionID, rootChapter?.ChapterGuid);
                  AVSRow avsRow5 = (AVSRow) null;
                  bool? oneProductRowOnly = new bool?();
                  if (this.IsFormA & findEqualRowsInVariableData)
                    oneProductRowOnly = new bool?(false);
                  for (int index7 = 0; flag4 && index7 < avsRowsByPartId.Count; ++index7)
                  {
                    if (avsRowsByPartId[index7].HasRelation)
                    {
                      avsRow5 = avsRowsByPartId[index7];
                      avsRow5.GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
                      if (avsRow5 != avsRow4)
                      {
                        Guid? additionalChapterGuid5 = avsRow4.AdditionalChapterGuid;
                        Guid? additionalChapterGuid6 = avsRow5.AdditionalChapterGuid;
                        if ((additionalChapterGuid5.HasValue == additionalChapterGuid6.HasValue ? (additionalChapterGuid5.HasValue ? (additionalChapterGuid5.GetValueOrDefault() == additionalChapterGuid6.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && avsRow4.IsAllowableRelation(avsRow5.Relations[0], notHiddenOnly: true, oneProductRowOnly: oneProductRowOnly) && (this.IsFormB || this.IsFormA && this.AvsRowsIsEqual(avsRow4.Relations[0], avsRow5.Relations[0], flag5, flag5)))
                          break;
                      }
                      avsRow5 = (AVSRow) null;
                    }
                  }
                  if (avsRow5 != null)
                  {
                    avsRowList3.Add(avsRow5);
                  }
                  else
                  {
                    flag4 = false;
                    if (this.AvsDocumentForm == AVSDocumentForm.A)
                    {
                      avsRowList3.Clear();
                      break;
                    }
                    for (int index8 = 0; index8 < avsRowsByPartId.Count; ++index8)
                    {
                      if (avsRowsByPartId[index8] != avsRow4)
                      {
                        Guid? additionalChapterGuid7 = avsRow4.AdditionalChapterGuid;
                        Guid? additionalChapterGuid8 = avsRowsByPartId[index8].AdditionalChapterGuid;
                        if ((additionalChapterGuid7.HasValue == additionalChapterGuid8.HasValue ? (additionalChapterGuid7.HasValue ? (additionalChapterGuid7.GetValueOrDefault() == additionalChapterGuid8.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && avsRow4.IsAllowableRelation(avsRowsByPartId[index8].Relations[0], notHiddenOnly: true))
                        {
                          avsRowList3.Add(avsRowsByPartId[index8]);
                          break;
                        }
                      }
                    }
                  }
                }
              }
              if (flag4 && avsRowList3.Count == 1 && avsRow4.HasRelation && avsRow4.Relations.Count < varDataA.Chapters.Count)
                flag4 = false;
            }
          }
          if (avsRowList3.Count > 0 && (flag4 || this.AvsDocumentForm != AVSDocumentForm.A))
          {
            AVSRow avsRow6 = new AVSRow(this);
            avsRow6.CommonPosition = avsRowList3[0].CommonPosition;
            List<TableData> tableDataList = (List<TableData>) null;
            for (int index9 = 0; index9 < avsRowList3.Count; ++index9)
            {
              RelationAttributeValuesCache relation = avsRowList3[index9].Relations[0];
              avsRowList3[index9].RemoveRelationData(avsRowList3[index9].Relations, 0);
              avsRow6.AddRowData(relation);
              if (avsRowList3[index9].HasHiddenRelation)
              {
                List<RelationAttributeValuesCache> attributeValuesCacheList = new List<RelationAttributeValuesCache>((IEnumerable<RelationAttributeValuesCache>) avsRowList3[index9].HiddenRelations);
                for (int index10 = 0; index10 < attributeValuesCacheList.Count; ++index10)
                {
                  avsRowList3[index9].RemoveRelationData(avsRowList3[index9].HiddenRelations, 0);
                  avsRow6.AddRowData(attributeValuesCacheList[index10], addToHidden: true);
                }
              }
              if (avsRowList3[index9].Section != null)
                this.RemoveRowAndChapterIfEmpty(avsRowList3[index9], removeEmptyChapters, chapters_CheckForRemove);
              if (tableDataList == null)
                tableDataList = avsRowList3[index9].DocNodes;
            }
            if (!avsRowList3[0].HasRelation)
              avsRow6.RelType = avsRowList3[0].RelType;
            if (tableDataList != null)
              avsRow6.DocNodes = tableDataList;
            Chapter chapter3;
            if (this.AvsDocumentForm == AVSDocumentForm.V && !flag4)
            {
              if (varDataV == null)
                throw new ArgumentNullException(nameof (varDataV));
              chapter3 = rootChapter == null ? varDataV : this.GetNewSectionOwner(varDataV.Product, rootChapter.GetChapterSettings());
            }
            else
              chapter3 = rootChapter == null ? commonData : this.GetNewSectionOwner(commonData.Product, rootChapter.GetChapterSettings());
            if (!(chapter3 is SpecificationSection specificationSection))
              specificationSection = (SpecificationSection) chapter3.GetChapter(avsRow6.SectionID);
            if (specificationSection == null)
              chapter3.AddChapter((Chapter) (specificationSection = this.CreateSection(avsRow6.SectionID)), true, false, false, (TableData) null);
            specificationSection.AddRow(avsRow6, true);
            if (this.IsFormA || this.IsFormV)
            {
              this.MoveHiddenRelationsWithExcessPosDesignations(avsRow6, true, this.IsFormA ? varDataA : varDataV, ref tmpSortIndex);
              this.RemoveRowAndChapterIfEmpty(avsRow6, removeEmptyChapters, chapters_CheckForRemove);
            }
            if (avsRow6.Section != null)
              avsRow6.SortIndex = this.FindNextFreeSortIndex(tmpSortIndex--);
            avsRow6.NeedUpdateStructure = false;
          }
          else if (this.IsSpecification)
          {
            Chapter chapter4 = rootChapter == null ? varDataA.GetChapter(avsRow4.ProductID) : this.GetNewSectionOwner(avsRow4.Product, rootChapter.GetChapterSettings());
            SpecificationSection newSection = (SpecificationSection) chapter4.GetChapter(avsRow4.SectionID);
            if (avsRow4.Section != newSection)
            {
              if (newSection == null)
                chapter4.AddChapter((Chapter) (newSection = this.CreateSection(avsRow4.SectionID)), true, false, false, (TableData) null);
              avsRow4.Section.MoveRow(avsRow4, newSection, true, flag1, true);
            }
          }
          avsRow4.NeedUpdateStructure = false;
        }
      }
    }
    for (int iteration = 0; iteration < 100; iteration++)
    {
      avsRowList1.Clear();
      if (this.AvsDocumentForm == AVSDocumentForm.B)
        commonData.GetAllRowsList(true, true, avsRowList1);
      else if (this.AvsDocumentForm == AVSDocumentForm.V && varDataV != null)
        varDataV.GetAllRowsList(true, true, avsRowList1);
      if (forceUpdate && iteration == 0 || avsRowList1.Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.NeedUpdateStructure)))
      {
        foreach (AVSRow avsRow in avsRowList1.FindAll((Predicate<AVSRow>) (r => forceUpdate && iteration == 0 || r.NeedUpdateStructure)))
        {
          if (!avsRow.IsFreeSortIndex && avsRow.Section != null && avsRow.HasRelation)
          {
            if (avsRow.Relations.Count != this.productsInfo.Count)
            {
              for (int index11 = 0; index11 < this.productsInfo.Count; ++index11)
              {
                if (avsRow.GetRelationIndexForProduct(this.productsInfo[index11].Id) == -1)
                {
                  List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(avsRow.ObjectId);
                  for (int index12 = 0; index12 < avsRowsByObjectId.Count; ++index12)
                  {
                    int relationIndexForProduct;
                    if (avsRowsByObjectId[index12].IsFreeSortIndex | findEqualRowsInVariableData && avsRowsByObjectId[index12] != avsRow && avsRowsByObjectId[index12].NeedUpdateStructure && (relationIndexForProduct = avsRowsByObjectId[index12].GetRelationIndexForProduct(this.productsInfo[index11].Id)) != -1)
                    {
                      RelationAttributeValuesCache relation = avsRowsByObjectId[index12].Relations[relationIndexForProduct];
                      Guid? additionalChapterGuid9 = avsRow.AdditionalChapterGuid;
                      Guid? additionalChapterGuid10 = avsRowsByObjectId[index12].AdditionalChapterGuid;
                      if ((additionalChapterGuid9.HasValue == additionalChapterGuid10.HasValue ? (additionalChapterGuid9.HasValue ? (additionalChapterGuid9.GetValueOrDefault() == additionalChapterGuid10.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && avsRow.IsAllowableRelation(relation))
                      {
                        avsRowsByObjectId[index12].RemoveRelationData(avsRowsByObjectId[index12].Relations, relationIndexForProduct);
                        avsRow.AddRowData(relation, relation.ObjectAttributesCache);
                        if (avsRow.SortIndex != relation.SortIndex)
                          avsRow.SetFieldValue(this.Attr_SortIndex, avsRow.Relations.Count - 1, -1, (object) avsRow.SortIndex, this.IsSpecification, true, false, false, false, false);
                        this.RemoveRowAndChapterIfEmpty(avsRowsByObjectId[index12], removeEmptyChapters, chapters_CheckForRemove);
                        break;
                      }
                    }
                  }
                }
              }
            }
          }
          else if (avsRow.HasRelation && !avsRow.HasHiddenRelation)
          {
            List<AVSRow> avsRowList5 = (List<AVSRow>) null;
            bool flag6 = false;
            for (int index13 = avsRow.Relations.Count - 1; index13 >= 0; --index13)
            {
              long valueInt64 = avsRow.Relations[index13].GetValueInt64(this.Attr_DopZamenGroupNum, false);
              string valueString = avsRow.Relations[index13].GetValueString(this.Field_Position, false);
              if (valueInt64 != -1L)
              {
                if (avsRowList5 == null)
                  avsRowList5 = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) avsRow.Section, avsRow.Product, avsRow.SectionID, avsRow.AdditionalChapterGuid);
                for (int index14 = 0; index14 < avsRowList5.Count; ++index14)
                {
                  if (avsRow != avsRowList5[index14] && avsRowList5[index14].HasRelation && avsRowList5[index14].CheckRelation_IsHiddenDopZamen(valueInt64, valueString, avsRow.Relations[index13].ProjectId))
                  {
                    RelationAttributeValuesCache relation = avsRow.Relations[index13];
                    avsRow.RemoveRelationData(avsRow.Relations, index13);
                    avsRowList5[index14].AddRowData(relation, addToHidden: true);
                    if (avsRowList5[index14].SortIndex != relation.SortIndex)
                      avsRowList5[index14].SetFieldValue(this.Attr_SortIndex, avsRowList5[index14].HiddenRelations.Count - 1, -1, avsRowList5[index14].HiddenRelations, (object) avsRowList5[index14].SortIndex, this.IsSpecification, true, false, false, false, false);
                    flag6 = true;
                    break;
                  }
                }
              }
            }
            if (flag6)
              this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove);
          }
          if (avsRow.Section != null)
            this.MoveHiddenRelationsWithExcessPosDesignations(avsRow, false, commonData, ref tmpSortIndex);
          this.RemoveRowAndChapterIfEmpty(avsRow, removeEmptyChapters, chapters_CheckForRemove);
          avsRow.NeedUpdateStructure = false;
        }
      }
      else
        break;
    }
    if (varDataV != null && findEqualRowsInVariableData | forceUpdate)
    {
      List<AVSRow> avsRowList6 = new List<AVSRow>();
      avsRowList1.Clear();
      varDataV.GetAllRowsList(true, true, avsRowList1);
      foreach (AVSRow row1 in avsRowList1)
      {
        AdditionalChapter rootChapter = row1.GetRootChapter() as AdditionalChapter;
        long fieldInt64Value1 = row1.GetFieldInt64Value(this.Attr_Section, 0, (List<RelationAttributeValuesCache>) null, true);
        bool flag7 = !this.IsFormB & findEqualRowsInVariableData && row1.Relations.Count == this.productsInfo.Count;
        SpecificationSection specificationSection1 = (SpecificationSection) null;
        for (int index = 1; flag7 && index < row1.Relations.Count; ++index)
          flag7 = this.AvsRowsIsEqual(row1.Relations[0], row1.Relations[index], false, true);
        if (((this.IsFormB ? 1 : (this.AvsDocumentForm == AVSDocumentForm.Single ? 1 : 0)) | (flag7 ? 1 : 0)) != 0)
        {
          Chapter chapter = rootChapter == null ? commonData : this.GetNewSectionOwner(commonData.Product, rootChapter.GetChapterSettings());
          if (!(chapter.GetChapter(fieldInt64Value1) is SpecificationSection newSection))
            chapter.AddChapter((Chapter) (newSection = this.CreateSection(fieldInt64Value1)), true, false, false, (TableData) null);
          specificationSection1 = row1.Section;
          row1.Section.MoveRow(row1, newSection, true, true, true);
          if (!newSection.HasDocNodes)
            newSection.Parent.UpdateViewNodes((SkipLinesSchema) null, false, false, true, false, false, EmptyRowUpdateMode.Delete);
          else
            row1.UpdateDocRow((TableData) null, this.docRowFields, false, true, true, EmptyRowUpdateMode.Delete);
        }
        else if (this.AvsDocumentForm == AVSDocumentForm.A)
        {
          List<TableData> docNodes = row1.DocNodes;
          row1.DocNodes = new List<TableData>();
          if (row1.HasRelation)
          {
            for (int index15 = row1.Relations.Count - 1; index15 >= 0; --index15)
            {
              long fieldInt64Value2 = row1.GetFieldInt64Value(this.Attr_Section, 0, (List<RelationAttributeValuesCache>) null, true);
              RelationAttributeValuesCache relation = row1.Relations[index15];
              row1.RemoveRelationData((List<RelationAttributeValuesCache>) null, index15);
              AVSRow row2 = new AVSRow(this, relation, relation.ObjectAttributesCache);
              Chapter chapter = rootChapter == null ? varDataA.GetChapter(relation.ProjectId) : this.GetNewSectionOwner(this.productsInfo[this.GetProductIndex(relation.ProjectId)], rootChapter.GetChapterSettings());
              if (!(chapter.GetChapter(fieldInt64Value2) is SpecificationSection specificationSection2))
                chapter.AddChapter((Chapter) (specificationSection2 = this.CreateSection(fieldInt64Value2)), true, false, false, (TableData) null);
              specificationSection2.AddRow(row2, true);
              if (index15 != 0 && docNodes != null)
              {
                List<TableData> tableDataList = new List<TableData>();
                for (int index16 = 0; index16 < docNodes.Count; ++index16)
                  tableDataList.Add((TableData) docNodes[index16].Clone());
                row2.DocNodes = tableDataList;
              }
              else
                row2.DocNodes = docNodes;
              row2.SortIndex = this.FindNextFreeSortIndex(tmpSortIndex--);
              row2.UpdateDocRow(this.avsRowTemplate, this.docRowFields, true, false, true, EmptyRowUpdateMode.Delete);
            }
          }
          if (row1.Section != null)
          {
            specificationSection1 = row1.Section;
            row1.Section.RemoveRow(row1, true, false, true, flag1, false);
          }
        }
        else
        {
          Chapter chapter = rootChapter == null ? varDataV : this.GetNewSectionOwner(varDataV.Product, rootChapter.GetChapterSettings());
          SpecificationSection newSection = chapter.GetChapter(fieldInt64Value1) as SpecificationSection;
          if (row1.Section != newSection)
          {
            if (newSection == null)
              chapter.AddChapter((Chapter) (newSection = this.CreateSection(fieldInt64Value1)), true, false, false, (TableData) null);
            specificationSection1 = row1.Section;
            row1.Section.MoveRow(row1, newSection, true, flag1, true);
          }
        }
        if (specificationSection1 != null && specificationSection1.Rows.Count == 0 && specificationSection1.DocNodes == null | removeEmptyChapters && specificationSection1.Parent != null)
        {
          if (specificationSection1.Parent.IsAdditionalChapter)
            chapters_CheckForRemove.Add(specificationSection1.Parent);
          specificationSection1.Parent.RemoveChapter((Chapter) specificationSection1, false, false, false, this.ViewMode == AVSViewMode.Grid);
        }
        row1.NeedUpdateStructure = false;
      }
    }
    for (int index = 0; index < chapters_CheckForRemove.Count; ++index)
    {
      if (chapters_CheckForRemove[index].Parent != null && chapters_CheckForRemove[index].Chapters.Count == 0)
        chapters_CheckForRemove[index].Parent.RemoveChapter(chapters_CheckForRemove[index], false, false, true, this.ViewMode == AVSViewMode.Grid);
    }
  }

  /// <summary>
  /// Переместить скрытую связь в подходящую запись из заданного раздела, либо создать новую запись для неё
  /// </summary>
  private void MoveRelationsToAllowableRowInVariableDataFormA(
    AVSRow avsRow,
    IEnumerable<RelationAttributeValuesCache> relations,
    Chapter dstChapter)
  {
    foreach (RelationAttributeValuesCache relation in relations)
    {
      SpecificationSection rowInVariableDataA = this.FindOrCreateSectionForRowInVariableDataA(avsRow, dstChapter, relation.ProjectId);
      this.MoveRelationToAllowableRowInVariableDataFormA(avsRow, relation, rowInVariableDataA);
    }
  }

  /// <summary>
  /// Переместить скрытую связь в подходящую запись из заданного раздела, либо создать новую запись для неё
  /// </summary>
  private void MoveRelationToAllowableRowInVariableDataFormA(
    AVSRow avsRow,
    RelationAttributeValuesCache relation,
    SpecificationSection section)
  {
    AVSRow newAvsRow = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) section, section.Product, section.SectionID, avsRow.AdditionalChapterGuid).Find((Predicate<AVSRow>) (row => row.NeedUpdateStructure && row.IsFreeSortIndex && row != avsRow && row.CheckRelation_IsHiddenRelation(relation)));
    RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(avsRow, relation);
    if (newAvsRow != null)
    {
      avsRow.RemoveRelationData(positionInAvsRow.RelationList, positionInAvsRow.RelationIndex);
      bool addToHidden = !newAvsRow.IsFormB || newAvsRow.CheckRelation_IsHiddenRelation(relation);
      newAvsRow.AddRowData(relation, addToHidden: addToHidden);
    }
    else
      newAvsRow = this.MoveRelationToNewAvsRow(avsRow, positionInAvsRow.RelationList, positionInAvsRow.RelationIndex);
    if (newAvsRow.Section != null)
      return;
    section.AddRow(newAvsRow, true);
  }

  /// <summary>
  /// Переместить скрытую связь в подходящую запись из заданного раздела, либо создать новую запись для неё
  /// </summary>
  private void MoveRelationToAllowableRowInFormB(
    AVSRow avsRow,
    IEnumerable<RelationAttributeValuesCache> relations,
    Chapter dstChapter)
  {
    SpecificationSection parentChapter = !this.IsFormV ? avsRow.Section : this.FindOrCreateSectionForRowInVariableDataV(avsRow, dstChapter);
    List<AVSRow> avsRowsByPartId = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) parentChapter, parentChapter.Product, parentChapter.SectionID, avsRow.AdditionalChapterGuid);
    int rowProductCount = relations.Count<RelationAttributeValuesCache>();
    Predicate<AVSRow> match = (Predicate<AVSRow>) (row => row != avsRow && row.NeedUpdateStructure && row.IsFreeSortIndex && this.IsEqualRelationsByPosDesignation(row, rowProductCount) && relations.All<RelationAttributeValuesCache>((System.Func<RelationAttributeValuesCache, bool>) (r => row.IsAllowableForHidden(r))));
    AVSRow newAvsRow = avsRowsByPartId.Find(match);
    if (newAvsRow != null)
    {
      foreach (RelationAttributeValuesCache relation in relations)
      {
        RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(avsRow, relation);
        avsRow.RemoveRelationData(positionInAvsRow.RelationList, positionInAvsRow.RelationIndex);
        newAvsRow.AddRowData(relation, addToHidden: true);
      }
    }
    else
      newAvsRow = this.MoveRelationsToNewAvsRow(avsRow, relations);
    if (newAvsRow.Section != null)
      return;
    parentChapter.AddRow(newAvsRow, true);
  }

  /// <summary>
  /// Переместить скрытую связь в подходящую запись из заданного раздела, либо создать новую запись для неё
  /// </summary>
  /// <param name="avsRow"></param>
  /// <param name="relationIndex"></param>
  /// <param name="section"></param>
  /// <param name="tmpSortIndex"></param>
  private void MoveHiddenRelationToAllowableRow(
    AVSRow avsRow,
    int relationIndex,
    SpecificationSection section,
    ref long tmpSortIndex)
  {
    List<AVSRow> avsRowsByPartId = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) section, section.Product, section.SectionID, avsRow.AdditionalChapterGuid);
    RelationAttributeValuesCache hiddenRelation = avsRow.HiddenRelations[relationIndex];
    Predicate<AVSRow> match = (Predicate<AVSRow>) (row => row.CheckRelation_IsHiddenRelation(hiddenRelation));
    AVSRow newAvsRow = avsRowsByPartId.Find(match);
    if (newAvsRow != null)
    {
      avsRow.RemoveRelationData(avsRow.HiddenRelations, relationIndex);
      newAvsRow.AddRowData(hiddenRelation, addToHidden: true);
    }
    else
    {
      newAvsRow = this.MoveRelationToNewAvsRow(avsRow, avsRow.HiddenRelations, relationIndex);
      newAvsRow.SortIndex = this.FindNextFreeSortIndex(tmpSortIndex--);
    }
    if (newAvsRow.Section != null)
      return;
    section.AddRow(newAvsRow, true);
  }

  /// <summary>Проверить и переместить связи с неподходящим позиционным обозначением</summary>
  /// <param name="avsRow"></param>
  /// <param name="isCommonDataAV"></param>
  /// <param name="dstChapter"></param>
  /// <param name="isCommonDataA"></param>
  /// <param name="varDataA"></param>
  private void MoveHiddenRelationsWithExcessPosDesignations(
    AVSRow avsRow,
    bool isCommonDataAV,
    Chapter dstChapter,
    ref long tmpSortIndex)
  {
    if (!avsRow.HasRelation || this.IsFormA && !isCommonDataAV)
      return;
    if (avsRow.HasHiddenRelation)
    {
      foreach (RelationAttributeValuesCache relation in avsRow.HiddenRelations.ToArray())
      {
        if (!avsRow.CheckRelation_IsHiddenForPosDesignationSumm(relation))
        {
          RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(avsRow, relation);
          this.MoveHiddenRelationToAllowableRow(avsRow, positionInAvsRow.RelationIndex, avsRow.Section, ref tmpSortIndex);
        }
      }
    }
    if (string.IsNullOrEmpty(avsRow.GetFieldStringValue(this.Field_PosDesignation, 0, -1, avsRow.Relations, false, true)))
      return;
    if (!this.IsEqualRelationsByPosDesignation(avsRow, isCommonDataAV ? this.productsInfo.Count : avsRow.Relations.Count))
    {
      for (int srcRelationIndex = avsRow.Relations.Count - 1; srcRelationIndex >= 0; --srcRelationIndex)
        avsRow.MoveRelationInInternalLists(srcRelationIndex, true);
      IEnumerable<IGrouping<string, RelationAttributeValuesCache>> groupings = ((IEnumerable<RelationAttributeValuesCache>) avsRow.AllRelations.ToArray<RelationAttributeValuesCache>()).GroupBy<RelationAttributeValuesCache, string>((System.Func<RelationAttributeValuesCache, string>) (r => r.GetValueString(this.Field_PosDesignation, false)));
      IEnumerable<RelationAttributeValuesCache> attributeValuesCaches = (IEnumerable<RelationAttributeValuesCache>) null;
      int num1 = 0;
      foreach (IGrouping<string, RelationAttributeValuesCache> source in groupings)
      {
        int num2 = source.Count<RelationAttributeValuesCache>();
        if (num2 > num1 && num2 <= this.productsInfo.Count)
        {
          num1 = num2;
          attributeValuesCaches = (IEnumerable<RelationAttributeValuesCache>) source;
        }
        if (isCommonDataAV)
        {
          if (num2 == this.productsInfo.Count)
            break;
        }
      }
      if (num1 == this.productsInfo.Count || num1 > 0 && !isCommonDataAV)
      {
        foreach (RelationAttributeValuesCache relation in attributeValuesCaches)
        {
          RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(avsRow, relation);
          avsRow.MoveRelationInInternalLists(positionInAvsRow.RelationIndex, false);
        }
      }
    }
    if (!avsRow.HasHiddenRelation)
      return;
    int count = avsRow.Relations.Count;
    long[] array = avsRow.Relations.Select<RelationAttributeValuesCache, long>((System.Func<RelationAttributeValuesCache, long>) (r => r.ProjectId)).OrderBy<long, long>((System.Func<long, long>) (id => id)).ToArray<long>();
    foreach (IGrouping<string, RelationAttributeValuesCache> grouping in ((IEnumerable<RelationAttributeValuesCache>) avsRow.HiddenRelations.ToArray()).GroupBy<RelationAttributeValuesCache, string>((System.Func<RelationAttributeValuesCache, string>) (r => r.GetValueString(this.Field_PosDesignation, false))))
    {
      bool flag = count == 0;
      if (!flag)
        flag = !((IEnumerable<long>) grouping.Select<RelationAttributeValuesCache, long>((System.Func<RelationAttributeValuesCache, long>) (r => r.ProjectId)).Distinct<long>().OrderBy<long, long>((System.Func<long, long>) (id => id)).ToArray<long>()).SequenceEqual<long>((IEnumerable<long>) array);
      if (flag)
      {
        if (isCommonDataAV && this.IsFormA)
          this.MoveRelationsToAllowableRowInVariableDataFormA(avsRow, (IEnumerable<RelationAttributeValuesCache>) grouping, dstChapter);
        else
          this.MoveRelationToAllowableRowInFormB(avsRow, (IEnumerable<RelationAttributeValuesCache>) grouping, dstChapter);
      }
    }
  }

  private bool IsEqualRelationsByPosDesignation(AVSRow avsRow, int productsInRowCount)
  {
    if (avsRow.Relations.Count != productsInRowCount)
      return false;
    if (avsRow.Relations.Count <= 1)
      return true;
    string posDesignation = avsRow.Relations[0].GetValueString(this.Field_PosDesignation, false);
    return avsRow.Relations.Skip<RelationAttributeValuesCache>(1).All<RelationAttributeValuesCache>((System.Func<RelationAttributeValuesCache, bool>) (r => r.GetValueString(this.Field_PosDesignation, false) == posDesignation));
  }

  /// <summary>Подобрать из свободных связей подходящие, чтобы дополнить строку до полной в общих данных</summary>
  /// <param name="removeEmptyChapters"></param>
  /// <param name="avsRow"></param>
  /// <param name="isEqualRelations"></param>
  /// <param name="gridViewMode"></param>
  /// <param name="chapters_CheckForRemove"></param>
  private void CollectAllowableFreeRelationsForCommonDataRow(
    bool removeEmptyChapters,
    AVSRow avsRow,
    ref bool isEqualRelations,
    bool gridViewMode,
    List<Chapter> chapters_CheckForRemove)
  {
    List<AVSDocument.RelInRowForProduct> relInRowForProductList = new List<AVSDocument.RelInRowForProduct>();
    for (int index1 = 0; index1 < this.productsInfo.Count; ++index1)
    {
      if (avsRow.GetRelationIndexForProduct(this.productsInfo[index1].Id) == -1)
      {
        isEqualRelations = false;
        List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(avsRow.ObjectId);
        for (int index2 = 0; index2 < avsRowsByObjectId.Count; ++index2)
        {
          int relationIndexForProduct;
          if (avsRowsByObjectId[index2] != avsRow && avsRowsByObjectId[index2].IsFreeSortIndex && (!this.IsElementList || avsRowsByObjectId[index2].NeedUpdateStructure) && (relationIndexForProduct = avsRowsByObjectId[index2].GetRelationIndexForProduct(this.productsInfo[index1].Id)) != -1)
          {
            Guid? additionalChapterGuid1 = avsRow.AdditionalChapterGuid;
            Guid? additionalChapterGuid2 = avsRowsByObjectId[index2].AdditionalChapterGuid;
            if ((additionalChapterGuid1.HasValue == additionalChapterGuid2.HasValue ? (additionalChapterGuid1.HasValue ? (additionalChapterGuid1.GetValueOrDefault() == additionalChapterGuid2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              ref bool local = ref isEqualRelations;
              Guid? additionalChapterGuid3 = avsRow.AdditionalChapterGuid;
              Guid? additionalChapterGuid4 = avsRowsByObjectId[index2].AdditionalChapterGuid;
              int num = (additionalChapterGuid3.HasValue == additionalChapterGuid4.HasValue ? (additionalChapterGuid3.HasValue ? (additionalChapterGuid3.GetValueOrDefault() == additionalChapterGuid4.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || !avsRow.IsAllowableRelation(avsRowsByObjectId[index2].Relations[relationIndexForProduct], notHiddenOnly: true) ? 0 : (this.AvsRowsIsEqual(avsRow.Relations[0], avsRowsByObjectId[index2].Relations[relationIndexForProduct], false, true) ? 1 : 0);
              local = num != 0;
              if (isEqualRelations && !this.IsSpecification)
              {
                string fieldStringValue1 = avsRowsByObjectId[index2].GetFieldStringValue(this.Field_PosDesignation, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, false);
                string fieldStringValue2 = avsRow.GetFieldStringValue(this.Field_PosDesignation, 0, 0, (List<RelationAttributeValuesCache>) null, false);
                isEqualRelations = fieldStringValue1 == fieldStringValue2;
              }
              if (isEqualRelations)
              {
                relInRowForProductList.Add(new AVSDocument.RelInRowForProduct(avsRowsByObjectId[index2], avsRowsByObjectId[index2].Relations[relationIndexForProduct]));
                break;
              }
            }
          }
        }
        if (!isEqualRelations)
        {
          relInRowForProductList.Clear();
          break;
        }
      }
    }
    if (!isEqualRelations)
      return;
    for (int index = 0; index < relInRowForProductList.Count; ++index)
    {
      RelationAttributeValuesCache relData = relInRowForProductList[index].RelData;
      relInRowForProductList[index].SpecRow.RemoveRelationData((List<RelationAttributeValuesCache>) null, relInRowForProductList[index].SpecRow.Relations.IndexOf(relData));
      avsRow.AddRowData(relData);
      if (avsRow.SortIndex != relData.SortIndex)
        avsRow.SetFieldValue(this.Attr_SortIndex, avsRow.Relations.Count - 1, -1, (object) avsRow.SortIndex, this.IsSpecification, true, false, false, false, false);
      if (!this.RemoveRowAndChapterIfEmpty(relInRowForProductList[index].SpecRow, removeEmptyChapters, chapters_CheckForRemove))
        relInRowForProductList[index].SpecRow.RestoreBaseRelationsFromHidden();
    }
  }

  private bool RemoveRowAndChapterIfEmpty(
    AVSRow avsRow,
    bool removeEmptyChapters,
    List<Chapter> chapters_CheckForRemove)
  {
    SpecificationSection section = avsRow.Section;
    if (avsRow.HasAnyRelations || section == null)
      return false;
    section.RemoveRow(avsRow, true, false, true, this.ViewMode == AVSViewMode.Grid, false);
    if (section.IsEmpty && !section.HasDocNodes | removeEmptyChapters && section.Parent != null)
    {
      if (section.Parent.IsAdditionalChapter)
        chapters_CheckForRemove.Add(section.Parent);
      section.Parent.RemoveChapter((Chapter) section, false, false, false, this.ViewMode == AVSViewMode.Grid);
    }
    return true;
  }

  private bool TryMoveSubstitutionRelationsToAllowableRows(AVSRow avsRow)
  {
    bool allowableRows = false;
    List<AVSRow> avsRowList = (List<AVSRow>) null;
    for (int index1 = avsRow.Relations.Count - 1; index1 >= 0; --index1)
    {
      long valueInt64 = avsRow.Relations[index1].GetValueInt64(AvsIDCache.Attr_DopZamenGroupNum, false);
      string valueString = avsRow.Relations[index1].GetValueString(this.Field_Position, false);
      if (valueInt64.IsDefinedId())
      {
        if (avsRowList == null)
          avsRowList = this.FindAvsRowsByPartId(avsRow.ObjectId, (Chapter) avsRow.Section, avsRow.Product, avsRow.SectionID, avsRow.AdditionalChapterGuid);
        for (int index2 = 0; index2 < avsRowList.Count; ++index2)
        {
          if (avsRowList[index2].HasRelation && avsRowList[index2] != avsRow && avsRowList[index2].CheckRelation_IsHiddenDopZamen(valueInt64, valueString, avsRow.Relations[index1].ProjectId))
          {
            RelationAttributeValuesCache relation = avsRow.Relations[index1];
            avsRow.RemoveRelationData(avsRow.Relations, index1);
            avsRowList[index2].AddRowData(relation, addToHidden: true);
            if (avsRowList[index2].SortIndex != relation.SortIndex)
              avsRowList[index2].SetFieldValue(this.Attr_SortIndex, avsRowList[index2].HiddenRelations.Count - 1, -1, avsRowList[index2].HiddenRelations, (object) avsRowList[index2].SortIndex, this.IsSpecification, true, false, false, false, false);
            allowableRows = true;
            break;
          }
        }
      }
    }
    return allowableRows;
  }

  private SpecificationSection FindOrCreateSectionForRowInVariableDataV(
    AVSRow avsRow,
    Chapter varDataV)
  {
    Chapter chapter = !(avsRow.GetRootChapter() is AdditionalChapter rootChapter) ? varDataV : this.GetNewSectionOwner(varDataV.Product, rootChapter.GetChapterSettings());
    if (!(chapter.GetChapter(avsRow.SectionID) is SpecificationSection rowInVariableDataV))
      chapter.AddChapter((Chapter) (rowInVariableDataV = this.CreateSection(avsRow.SectionID)), true, false, false, (TableData) null);
    return rowInVariableDataV;
  }

  private SpecificationSection FindOrCreateSectionForRowInVariableDataA(
    AVSRow avsRow,
    Chapter varDataA,
    long productId)
  {
    Chapter chapter = !(avsRow.GetRootChapter() is AdditionalChapter rootChapter) ? varDataA.GetChapter(productId) : this.GetNewSectionOwner(this.productsInfo[this.GetProductIndex(productId)], rootChapter.GetChapterSettings());
    SpecificationSection rowInVariableDataA = (SpecificationSection) chapter.GetChapter(avsRow.SectionID);
    if (rowInVariableDataA == null)
      chapter.AddChapter((Chapter) (rowInVariableDataA = this.CreateSection(avsRow.SectionID)), true, false, false, (TableData) null);
    return rowInVariableDataA;
  }

  private AVSRow MoveRelationToNewAvsRow(
    AVSRow avsRow,
    List<RelationAttributeValuesCache> relations,
    int k)
  {
    RelationAttributeValuesCache relation = relations[k];
    avsRow.RemoveRelationData(relations, k);
    return new AVSRow(this, relation, relation.ObjectAttributesCache)
    {
      CommonPosition = avsRow.CommonPosition
    };
  }

  protected AVSRow MoveRelationsToNewAvsRow(
    AVSRow avsRow,
    IEnumerable<RelationAttributeValuesCache> relations)
  {
    RelationAttributeValuesCache[] array = relations.ToArray<RelationAttributeValuesCache>();
    AVSRow newAvsRow = new AVSRow(this);
    foreach (RelationAttributeValuesCache attributeValuesCache in array)
    {
      RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(avsRow, attributeValuesCache);
      avsRow.RemoveRelationData(positionInAvsRow.RelationList, positionInAvsRow.RelationIndex);
      bool addToHidden = newAvsRow.CheckRelation_IsHiddenRelation(attributeValuesCache);
      newAvsRow.AddRowData(attributeValuesCache, addToHidden: addToHidden);
    }
    newAvsRow.CommonPosition = avsRow.CommonPosition;
    return newAvsRow;
  }

  private static void MoveHiddenRelationsToAvsRow(AVSRow specRow, AVSRow newSpecRow)
  {
    if (!specRow.HasHiddenRelation)
      return;
    for (int index = specRow.HiddenRelations.Count - 1; index >= 0; --index)
    {
      RelationAttributeValuesCache hiddenRelation = specRow.HiddenRelations[index];
      if (newSpecRow.CheckRelation_IsHiddenRelation(hiddenRelation))
      {
        specRow.RemoveRelationData(specRow.HiddenRelations, index);
        newSpecRow.AddRowData(hiddenRelation, addToHidden: true);
      }
    }
  }

  /// <summary>Обновить текст допзамен для заданных групп допзамен</summary>
  /// <param name="dopZamenyGroopList">Список групп допзамен.
  /// Если null, то обновляет все группы допзамен</param>
  /// <param name="updateDocNode">Обновлять ячейку примечания в строке документа</param>
  public void ReloadDopzamenTextForGroup(List<long> dopZamenyGroopList, bool updateDocNode)
  {
    if (this.suspendReloadDopZamenText > 0)
      return;
    this.needReloadDopZamenText = false;
    if (dopZamenyGroopList != null && dopZamenyGroopList.Count == 0)
      return;
    List<AVSRow> allRows = this.GetAllRows(true, false);
    this.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      Dictionary<AVSRow, AVSRow> changedRowsDic = new Dictionary<AVSRow, AVSRow>();
      this.LoadDopzamenTextForProducts(this.productsInfo, allRows, dopZamenyGroopList, changedRowsDic);
      this.LoadDopzamenTextForProducts(this.ParentProducts, allRows, dopZamenyGroopList, changedRowsDic);
      if (!updateDocNode)
        return;
      foreach (KeyValuePair<AVSRow, AVSRow> keyValuePair in changedRowsDic)
        keyValuePair.Key.UpdateNoteDocCellText();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, false);
    }
  }

  private void LoadDopzamenTextForProducts(
    List<ProductInfo> products,
    List<AVSRow> rows,
    List<long> dopZamenyGroopList,
    Dictionary<AVSRow, AVSRow> changedRowsDic)
  {
    for (int index1 = 0; index1 < products.Count; ++index1)
    {
      if (!Intermech.Consts.IsUndefinedObjectId(products[index1].Id))
      {
        List<AVSRow> changedRows;
        this.ReloadDopzamenTextForGroup(dopZamenyGroopList, products[index1].Id, rows, out changedRows, false);
        if (changedRows != null)
        {
          for (int index2 = 0; index2 < changedRows.Count; ++index2)
          {
            if (!changedRowsDic.ContainsKey(changedRows[index2]))
              changedRowsDic.Add(changedRows[index2], (AVSRow) null);
          }
        }
      }
    }
  }

  /// <summary>Обновить текст существующих допзамен</summary>
  public void UpdateDopzamenText()
  {
    if (this.suspendReloadDopZamenText > 0)
      return;
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    List<AVSRow> allRows = this.GetAllRows(true, false);
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum);
    for (int index1 = 0; index1 < this.productsInfo.Count; ++index1)
    {
      for (int index2 = 0; index2 < allRows.Count; ++index2)
      {
        int relationIndexForProduct = allRows[index2].GetRelationIndexForProduct(this.productsInfo[index1].Id);
        if (relationIndexForProduct != -1 && allRows[index2].GetFieldValue(attrInfo, relationIndexForProduct, -1, true, false) != null)
          AVSDocument.AddRelationToTypedDictionary(dictionary, allRows[index2].Relations[relationIndexForProduct].RelationType, allRows[index2].Relations[relationIndexForProduct].RelationId);
      }
    }
    this.ReloadRelationsAttributesFromDB(dictionary);
    this.needReloadDopZamenText = false;
  }

  /// <summary>Подготовить данные для запроса расшифровки допзамен</summary>
  /// <param name="attrPackage">Пакет данных для запроса</param>
  /// <param name="row">Запись</param>
  /// <param name="relation">Связь данные которой обрабатываются</param>
  /// <param name="dopZamenyGroopList">Список допзамен, данные которых должны запрашиваться. Если null, то запрашиваются все</param>
  /// <returns>Возвращает true, если в связи есть данные для допзамен</returns>
  private bool SetUpRelationAttributesPackageIfHasSubstitutes(
    RelationAttributesPackage attrPackage,
    AVSRow row,
    RelationAttributeValuesCache relation,
    List<long> dopZamenyGroopList)
  {
    long valueInt64 = relation.GetValueInt64(this.Attr_DopZamenGroupNum, false, 0L);
    if (valueInt64 == 0L || dopZamenyGroopList != null && !dopZamenyGroopList.Contains(valueInt64))
      return false;
    long relationId = relation.RelationId;
    List<long> relations = new List<long>();
    relations.Add(relationId);
    if (this.IsSpecification)
    {
      string str = AVSRow.ConvertCountToStringForMeasuredValue(relation.GetValue(this.Field_Count, false));
      if (str == "")
        str = (string) null;
      attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Count, (object) str);
    }
    else
      attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Count, (object) null);
    string str1 = row.GetFieldStringValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, false, true);
    string str2 = row.GetFieldStringValue(this.Field_Designation, -1, -1, (List<RelationAttributeValuesCache>) null, false, true);
    if (this.IsElementList)
    {
      if (string.IsNullOrEmpty(str2))
        str2 = str1;
      else if (!string.IsNullOrEmpty(str1))
        str2 = $"{str1} {str2}";
      str1 = "";
    }
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Designation, (object) str2);
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Name, (object) str1);
    if (this.IsSpecification)
      attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Position, relation.GetValue(this.Field_Position, false));
    else
      attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_Position, (object) "");
    string str3 = "";
    if (this.IsSpecification)
    {
      if (row.HasRelation)
      {
        row.Relations.IndexOf(relation);
        str3 = row.GetPosDesignationForNoteField(this.Field_PosDesignation);
      }
      else
        str3 = relation.GetValueString(this.Field_PosDesignation, false);
    }
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_PosDesignation, (object) str3);
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_DopZamenGroupNum, (object) valueInt64);
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_DopZamenNumInGroup, relation.GetValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenNumInGroup), false));
    attrPackage.SetRelationsAttrValue(relations, AvsIDCache.Attr_DesignerActualVariant, relation.GetValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_DesignerActualVariant), false));
    attrPackage.SetRelationsAttrValue(relations, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, relation.GetValue(new AvsRowAttributeInfo(true, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID), false));
    attrPackage.SetRelationsAttrValue(relations, SubstitutesConstants.PositionNumberAttributeTypeID, relation.GetValue(new AvsRowAttributeInfo(true, SubstitutesConstants.PositionNumberAttributeTypeID), false));
    attrPackage.SetRelationsAttrValue(relations, -3, row.GetFieldValue(new AvsRowAttributeInfo(false, -3), -1, -1, false, false));
    return true;
  }

  /// <summary>Обновить текст допзамен для заданных групп допзамен</summary>
  /// <param name="dopZamenyGroopList">Список групп допзамен.
  /// Если null, то обновляет все группы допзамен</param>
  /// <param name="productID">Идентификатор исполнения</param>
  /// <param name="rows">Список строк для проверки и обновления допзамен. Если значение null, то для всех записей</param>
  /// <param name="changedRows">Возвращает список строк, в которых изменялись допзамены</param>
  /// <param name="updateDocNode">Обновлять ячейку примечания в строке документа</param>
  public void ReloadDopzamenTextForGroup(
    List<long> dopZamenyGroopList,
    long productID,
    List<AVSRow> rows,
    bool updateDocNode)
  {
    this.ReloadDopzamenTextForGroup(dopZamenyGroopList, productID, rows, out List<AVSRow> _, updateDocNode);
  }

  /// <summary>Обновить текст допзамен для заданных групп допзамен</summary>
  /// <param name="dopZamenyGroopList">Список групп допзамен.
  /// Если null, то обновляет все группы допзамен</param>
  /// <param name="productID">Идентификатор исполнения</param>
  /// <param name="rows">Список строк для проверки и обновления допзамен. Если значение null, то для всех записей</param>
  /// <param name="changedRows">Возвращает список строк, в которых изменялись допзамены</param>
  /// <param name="updateDocNode">Обновлять ячейку примечания в строке документа</param>
  public void ReloadDopzamenTextForGroup(
    List<long> dopZamenyGroopList,
    long productID,
    List<AVSRow> rows,
    out List<AVSRow> changedRows,
    bool updateDocNode)
  {
    changedRows = new List<AVSRow>(0);
    if (this.suspendReloadDopZamenText > 0)
      return;
    this.needReloadDopZamenText = false;
    if (dopZamenyGroopList != null && dopZamenyGroopList.Count == 0)
      return;
    ISubstitutesRemarksService service1 = (ISubstitutesRemarksService) ServicesManager.GetService(typeof (ISubstitutesRemarksService));
    if (service1 == null)
      return;
    if (SubstituteObjects.Attrs == null || SubstituteObjects.Attrs.Count == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SubstituteObjects.InitStaticFields(sessionKeeper.Session);
    }
    RelationAttributesPackage attributesPackage = new RelationAttributesPackage(SubstituteObjects.Attrs);
    List<AVSRow> avsRowList = new List<AVSRow>();
    if (rows == null)
      rows = this.GetAllRows(true, true);
    changedRows = new List<AVSRow>();
    foreach (AVSRow row in rows)
    {
      bool flag = false;
      foreach (RelationAttributeValuesCache relation in row.AllRelations.Where<RelationAttributeValuesCache>((System.Func<RelationAttributeValuesCache, bool>) (r => r.ProjectId == productID)))
        flag = this.SetUpRelationAttributesPackageIfHasSubstitutes(attributesPackage, row, relation, dopZamenyGroopList);
      if (flag)
        avsRowList.Add(row);
    }
    ISubstitutesSettings service2 = ServicesManager.GetService(typeof (ISubstitutesSettings)) as ISubstitutesSettings;
    bool usePlaceholders = service1.UsePlaceholders;
    Dictionary<long, string> dictionary = (Dictionary<long, string>) null;
    try
    {
      service1.UsePlaceholders = true;
      dictionary = service1.CalcSubstituteRemarks(service2, attributesPackage);
    }
    finally
    {
      service1.UsePlaceholders = usePlaceholders;
    }
    if (dictionary == null)
      return;
    bool isGridViewMode = this.IsGridViewMode;
    this.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      for (int index1 = 0; index1 < avsRowList.Count; ++index1)
      {
        bool flag1 = false;
        bool flag2 = avsRowList[index1].SectionID == AVSDocument.ObjID_SectionMaterials;
        string str;
        if (avsRowList[index1].HasRelation)
        {
          for (int index2 = 0; index2 < avsRowList[index1].Relations.Count; ++index2)
          {
            string fieldStringValue = avsRowList[index1].GetFieldStringValue(this.Attr_DopZamenText, index2, -1, (List<RelationAttributeValuesCache>) null, false);
            bool flag3;
            bool flag4 = avsRowList[index1].Relations[index2].PersistentAttrs.TryGetValue(this.Attr_DopZamenText.AttributeId, out flag3) & flag3;
            if ((!flag4 ? 1 : (!flag4 ? 0 : (string.IsNullOrWhiteSpace(fieldStringValue) ? 1 : 0))) != 0 && dictionary.TryGetValue(avsRowList[index1].Relations[index2].RelationId, out str))
            {
              str = str.Replace("[ActualSubstitute]", service2.ActualSubstitute).Replace("[ActualSubstitute2]", service2.ActualSubstitute2).Replace("[ActualSubstitute3]", service2.ActualSubstitute3).Replace("[Substitute]", flag2 ? service2.MaterialSubstitute : service2.Substitute).Replace("[Substitute2]", flag2 ? service2.MaterialSubstitute2 : service2.Substitute2).Replace("[Substitute3]", flag2 ? service2.MaterialSubstitute3 : service2.Substitute3);
              if (avsRowList[index1].SetFieldValue(this.Attr_DopZamenText, index2, -1, (object) str, false, false, updateDocNode, isGridViewMode, false, false))
                flag1 = true;
            }
          }
        }
        if (avsRowList[index1].HasHiddenRelation)
        {
          for (int index3 = 0; index3 < avsRowList[index1].HiddenRelations.Count; ++index3)
          {
            if (dictionary.TryGetValue(avsRowList[index1].HiddenRelations[index3].RelationId, out str) && avsRowList[index1].SetFieldValue(this.Attr_DopZamenText, index3, -1, avsRowList[index1].HiddenRelations, (object) str, false, false, updateDocNode, isGridViewMode, false, false))
              flag1 = true;
          }
        }
        if (flag1)
          changedRows.Add(avsRowList[index1]);
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, false);
    }
  }

  /// <summary>Взять документ и исполнения на изменение</summary>
  /// <param name="session">Сессия</param>
  /// <param name="cancel">Отменить операцию</param>
  /// <returns>Вернет false, если есть объекты, которые взяты другим пользователем или нельзя взять</returns>
  protected virtual bool CheckOutObjects(out bool cancel)
  {
    cancel = false;
    return true;
  }

  /// <summary>Настроить фильтрацию состава для СП</summary>
  /// <param name="paramSet">Параметры запроса</param>
  /// <param name="context">Контекст документа</param>
  internal static void SetFiltrationTags(ref DBRecordSetParams paramSet, AVSDocumentContext context)
  {
    if (context == null)
      context = new AVSDocumentContext();
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    if (context.ConfigureCompositionRoot != null && !context.ConfigureCompositionRoot.Empty)
      paramSet.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) context.ConfigureCompositionRoot;
    paramSet.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) new long[2]
    {
      0L,
      1L
    };
    paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) context.BlockConfigureComposition;
    paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) context.BlockConfigureComposition;
    paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) context.BlockConfigureComposition;
  }

  /// <summary>Найти Сборочную единицу для спецификации по связям</summary>
  /// <param name="session">Сессия</param>
  /// <param name="filtrationOwnerID">Уникальный ID настроек фильтрации, по которым будет проводиться фильтрация состава</param>
  /// <returns>Возвращает список исполнений связанных с документом</returns>
  protected static List<long> FindProductForSpecificationByRelations(
    IUserSession session,
    long documentID,
    string filtrationOwnerID)
  {
    List<long> specificationByRelations = new List<long>();
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, documentID);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[-2.ToString()]);
      specificationByRelations.Add(int64);
    }
    dataTable.Dispose();
    return specificationByRelations;
  }

  /// <summary>Преобразовать текстовое значение атрибута "Форма спецификации" в тип SpecificationForm</summary>
  /// <param name="attrValue">Значение атрибута</param>
  /// <returns></returns>
  public static AVSDocumentForm? DecodeSpecificationFormAttrValue(string attrValue)
  {
    return SpecificationFormMethods.DecodeSpecificationFormAttrValue(attrValue);
  }

  /// <summary>Получить идентификатор версии СП для данного изделия</summary>
  /// <param name="productObjID">Идентификатор версии изделия</param>
  /// <param name="session">Сессия</param>
  public static long GetSpecificationIDForProduct(long productObjID, IUserSession session)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document);
    relationCollection.ObjectTypeID = AvsIDCache.ObjType_Specification;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    DataTable dataTable1 = relationCollection.ConsistFrom(paramSet, productObjID);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, Convert.ToInt32(row[1])))
      {
        long int64 = Convert.ToInt64(row[0]);
        dataTable1.Dispose();
        return int64;
      }
    }
    dataTable1.Dispose();
    long[] articlesByGroupId = AVSDocument.FindArticlesByGroupID(productObjID, (string) null, session);
    if (articlesByGroupId == null)
      return -1;
    foreach (long projectID in articlesByGroupId)
    {
      if (productObjID != projectID)
      {
        DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, projectID);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, Convert.ToInt32(row[1])))
          {
            long int64 = Convert.ToInt64(row[0]);
            dataTable2.Dispose();
            return int64;
          }
        }
        dataTable2.Dispose();
      }
    }
    return -1;
  }

  /// <summary>Найти Спецификацию для Сборочной единицы</summary>
  /// <param name="session">Сессия</param>
  protected void FindSpecificationForAssemblyProducts(IUserSession session)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
    if (this.DocumentDBObjectType == -1)
      this.DocumentDBObjectType = AvsIDCache.ObjType_Specification;
    relationCollection.ObjectTypeID = this.DocumentDBObjectType;
    ColumnDescriptor[] columns = new ColumnDescriptor[7]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SpecificationForm, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
      AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
      long num1 = -1;
      AVSDocumentForm? nullable = new AVSDocumentForm?();
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, this.productsInfo[index].Id);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32 = Convert.ToInt32(row[1]);
        if (AVSDocument.IsParentObjectType(this.DocumentDBObjectType, int32))
        {
          long int64 = Convert.ToInt64(row[0]);
          object obj = row[AvsIDCache.Attr_SpecificationForm.ToString()];
          if (obj != null && obj != DBNull.Value)
            nullable = AVSDocument.DecodeSpecificationFormAttrValue(obj.ToString());
          this.AvsDocumentForm = !nullable.HasValue ? (this.productsInfo.Count != 1 ? this.GetDefaultGroupDocumentForm() : AVSDocumentForm.Single) : nullable.Value;
          this.DocumentGuid = new Guid(Convert.ToString(row[-12.ToString()]));
          this.DocumentID = int64;
          this.DocumentDBObjectType = int32;
          DataRow dataRow1 = row;
          int num2 = -50;
          string columnName1 = num2.ToString();
          this.DocumentCaption = Convert.ToString(dataRow1[columnName1]);
          DataRow dataRow2 = row;
          num2 = AvsIDCache.Attr_Name;
          string columnName2 = num2.ToString();
          this.DocumentName = Convert.ToString(dataRow2[columnName2]);
          DataRow dataRow3 = row;
          num2 = AvsIDCache.Attr_Designation;
          string columnName3 = num2.ToString();
          this.DocumentDesignation = Convert.ToString(dataRow3[columnName3]);
          dataTable.Dispose();
          return;
        }
        num1 = -1L;
      }
      dataTable.Dispose();
    }
    if (this.productsInfo.Count == 1)
      this.AvsDocumentForm = AVSDocumentForm.Single;
    else
      this.AvsDocumentForm = this.GetDefaultGroupDocumentForm();
  }

  /// <summary>Найти связи со спецификацией для исполнений изделия. Выбирает первый документ как СП и инициализирует поля для документа</summary>
  /// <param name="productsId">Идентификаторы исполнений изделия</param>
  /// <param name="filtrationOwnerID">Идентификатор правила подбора версий</param>
  /// <param name="session">Сессия</param>
  /// <param name="productsWithoutVersionInRelation">Список исполнений без конкретизации версии документа на связи</param>
  /// <returns>Возвращает словарь Исполнение/Документ</returns>
  protected Dictionary<long, long> FindDocRelationsForProducts(
    List<long> productsId,
    string filtrationOwnerID,
    IUserSession session,
    out List<long> productsWithoutVersionInRelation)
  {
    productsWithoutVersionInRelation = new List<long>();
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ObjectTypeID = AvsIDCache.ObjType_Specification;
    ColumnDescriptor[] columns = new ColumnDescriptor[9]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SpecificationForm, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_VersionInRelation, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.In, (object) productsId.ToArray(), LogicalOperators.NONE, 0, true)
    }, columns);
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    Dictionary<long, long> relationsForProducts = new Dictionary<long, long>();
    AVSDocumentForm? nullable = new AVSDocumentForm?();
    this.DocumentID = -1L;
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.Select(paramSet).Rows)
    {
      long int64_1 = Convert.ToInt64(row[0]);
      long int64_2 = Convert.ToInt64(row[1]);
      int num;
      if (this.DocumentID.IsUndefinedId())
      {
        this.DocumentID = int64_2;
        DataRow dataRow1 = row;
        num = AvsIDCache.Attr_SpecificationForm;
        string columnName1 = num.ToString();
        object obj = dataRow1[columnName1];
        if (obj != null && obj != DBNull.Value)
          nullable = AVSDocument.DecodeSpecificationFormAttrValue(obj.ToString());
        this.AvsDocumentForm = !nullable.HasValue ? (this.productsInfo.Count != 1 ? this.GetDefaultGroupDocumentForm() : AVSDocumentForm.Single) : nullable.Value;
        DataRow dataRow2 = row;
        num = -12;
        string columnName2 = num.ToString();
        this.DocumentGuid = new Guid(Convert.ToString(dataRow2[columnName2]));
        DataRow dataRow3 = row;
        num = -7;
        string columnName3 = num.ToString();
        this.DocumentDBObjectType = Convert.ToInt32(dataRow3[columnName3]);
        DataRow dataRow4 = row;
        num = -50;
        string columnName4 = num.ToString();
        this.DocumentCaption = Convert.ToString(dataRow4[columnName4]);
        DataRow dataRow5 = row;
        num = AvsIDCache.Attr_Name;
        string columnName5 = num.ToString();
        this.DocumentName = Convert.ToString(dataRow5[columnName5]);
        DataRow dataRow6 = row;
        num = AvsIDCache.Attr_Designation;
        string columnName6 = num.ToString();
        this.DocumentDesignation = Convert.ToString(dataRow6[columnName6]);
      }
      if (!relationsForProducts.ContainsKey(int64_1))
        relationsForProducts.Add(int64_1, int64_2);
      DataRow dataRow = row;
      num = AvsIDCache.Attr_VersionInRelation;
      string columnName = num.ToString();
      if (AvsIDCache.ConvertDbValueToInt64(dataRow[columnName]).IsUndefinedId())
        productsWithoutVersionInRelation.Add(int64_1);
    }
    return relationsForProducts;
  }

  /// <summary>Найти Спецификацию для исполнений изделия</summary>
  /// <param name="session">Сессия</param>
  /// <param name="productsId">Идентификаторы исполнений изделия</param>
  /// <param name="filtrationOwnerID">Идентификатор правила подбора версий</param>
  protected static long FindSpecificationForAssemblyProducts(
    IUserSession session,
    List<long> productsId,
    string filtrationOwnerID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ObjectTypeID = AvsIDCache.ObjType_Specification;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
    for (int index = 0; index < productsId.Count; ++index)
    {
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, productsId[index]);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(row[1]), AvsIDCache.ObjType_Specification))
        {
          long int64 = Convert.ToInt64(row[0]);
          dataTable.Dispose();
          return int64;
        }
      }
    }
    return -1;
  }

  /// <summary>Создать минимальный список атрибутов для записи</summary>
  /// <returns></returns>
  public List<AvsRowAttributeInfo> CreateMinimalAttributeList(int relationType)
  {
    List<AvsRowAttributeInfo> minimalAttributeList = new List<AvsRowAttributeInfo>(20);
    bool flag1 = relationType == AvsIDCache.Relation_Document;
    minimalAttributeList.Add(this.Attr_SortIndex);
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00033-306c-11d8-b4e9-00304f19f545"), -20, "Идентификатор связи", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00036-306c-11d8-b4e9-00304f19f545"), -23, "Тип связи", ColumnContents.Value));
    minimalAttributeList.Add(this.Attr_Section);
    minimalAttributeList.Add(this.Attr_AdditionalChapter);
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00034-306c-11d8-b4e9-00304f19f545"), -21, "Идентификатор объекта - проекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00029-306c-11d8-b4e9-00304f19f545"), -2, "Идентификатор версии объекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), -7, "Тип объекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Designation, "Обозначение объекта", ColumnContents.Text));
    bool flag2 = true;
    if (flag1)
      flag2 = MetaDataHelper.GetAttribute4ObjectType(AvsIDCache.ObjType_ConstructorDocument, AvsIDCache.Attr_InsertToSection) != null;
    if (flag2)
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00210-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_InsertToSection, "РАЗДЕЛ СП", ColumnContents.Text));
    if (!this.HideCountForDocuments || !flag1)
    {
      if (relationType == AvsIDCache.Relation_Podbor)
        minimalAttributeList.Add(this.Attr_CountForAdjustment);
      else
        minimalAttributeList.Add(this.Field_Count);
    }
    if (this.IsElementList && relationType == AvsIDCache.Relation_Project)
      minimalAttributeList.Add(this.Attr_IncludeInElementList);
    if (this.IsSpecification)
      minimalAttributeList.Add(this.Attr_HideInSpecification);
    return minimalAttributeList;
  }

  public static List<AvsRowAttributeInfo> GetVirtualAttributeListForDocument()
  {
    return new List<AvsRowAttributeInfo>();
  }

  public virtual List<AvsRowAttributeInfo> GetVirtualAttributeList()
  {
    return AVSDocument.GetVirtualAttributeListForDocument();
  }

  /// <summary>Создать список атрибутов, нужных для работы СП (включает CreateMinimalAttributeList)</summary>
  /// <param name="relationType">Тип связи, для которой запрашиваются атрибуты</param>
  /// <param name="includeSearchId">Добавлять идентификатор объектов Search</param>
  /// <returns></returns>
  public List<AvsRowAttributeInfo> CreateRequiredAttributeList(
    int relationType,
    bool includeSearchId)
  {
    List<AvsRowAttributeInfo> minimalAttributeList = this.CreateMinimalAttributeList(relationType);
    int num = relationType == AvsIDCache.Relation_Document ? 1 : 0;
    bool flag = relationType == AvsIDCache.Relation_Project;
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00130-306c-11d8-b4e9-00304f19f545"), -12, "Guid версии объекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002a-306c-11d8-b4e9-00304f19f545"), -3, "Идентификатор объекта", ColumnContents.Value));
    if (includeSearchId)
      minimalAttributeList.Add(this.Attr_SearchId);
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), -50, "Заголовок объекта", ColumnContents.Text));
    minimalAttributeList.Add(this.Field_Name);
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002d-306c-11d8-b4e9-00304f19f545"), -6, "Кем взят на изменение", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002b-306c-11d8-b4e9-00304f19f545"), -4, "Шаг жизненного цикла", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002f-306c-11d8-b4e9-00304f19f545"), -8, "Владелец объекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0002c-306c-11d8-b4e9-00304f19f545"), -5, "Номер версии объекта", ColumnContents.Value));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0038a-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_OKPCode, "Код ОКП", ColumnContents.Text));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrProductConventionalName_Guid, AvsIDCache.Attr_ProductConventionalName, "Условное наименование", ColumnContents.Text));
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0038c-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Material, "Материал", ColumnContents.ID));
    if (this.Attr_UserAttributeForNameField == null || this.Attr_UserAttributeForDocType == null)
      this.GetUserAttributesForFieldNameFromSettings();
    if (this.Attr_UserAttributeForNameField.AttributeGuid != Guid.Empty)
      minimalAttributeList.Add(this.Attr_UserAttributeForNameField);
    if (num != 0)
    {
      minimalAttributeList.Add(this.Field_Format);
      if (this.Attr_UserAttributeForDocType.AttributeGuid != Guid.Empty)
        minimalAttributeList.Add(this.Attr_UserAttributeForDocType);
    }
    else
    {
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_ArticleGroupID, "Идентификатор группового изделия", ColumnContents.Text));
      minimalAttributeList.Add(this.Attr_Class);
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrGostGuid, AvsIDCache.Attr_Gost, "ГОСТ", ColumnContents.Text));
      minimalAttributeList.Add(this.Attr_SizeAndParams);
      minimalAttributeList.Add(this.Attr_GroupWithoutClass);
    }
    minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00344-306c-11d8-b4e9-00304f19f545"), -26, "Guid связи", ColumnContents.Value));
    if (num != 0)
      minimalAttributeList.Add(this.Attr_InMainDocComplect);
    if (flag)
    {
      minimalAttributeList.Add(this.Field_Zone);
      minimalAttributeList.Add(this.Field_Position);
      minimalAttributeList.Add(this.Field_PosDesignation);
      minimalAttributeList.Add(this.Attr_FunctionalGroupPosDesignation);
      if (this.IsSpecification)
      {
        minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_CommonPositionGuid, AvsIDCache.Attr_CommonPosition, "Совместная позиция", ColumnContents.Text));
        minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.attributeSymbolForPosDesignation, AvsIDCache.Attr_SymbolForPosDesignation, "Позиционное обозначение ДС", ColumnContents.Text));
      }
      else if (this.IsElementList)
      {
        minimalAttributeList.Add(this.Attr_FunctionalGroupDesignation);
        minimalAttributeList.Add(this.Attr_FunctionalGroupName);
      }
      minimalAttributeList.Add(this.Attr_Podbor);
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_OccurenceKeyGuid, AvsIDCache.Attr_OccurenceKey, "Глобальный идентификатор входимости", ColumnContents.Text));
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_BasedOnCADModelGuid, AvsIDCache.Attr_BasedOnCADModel, "Создано по CAD-модели", ColumnContents.Text));
      minimalAttributeList.Add(this.Attr_DopZamenGroupNum);
      minimalAttributeList.Add(this.Attr_DopZamenNumInGroup);
      minimalAttributeList.Add(this.Attr_DesignerActualVariant);
      minimalAttributeList.Add(this.Attr_SubstitutePositionType);
      minimalAttributeList.Add(this.Attr_DopZamenText);
      minimalAttributeList.Add(this.Attr_SubstitutePositionNumber);
    }
    if (relationType == AvsIDCache.Relation_Podbor)
      minimalAttributeList.Add(this.Attr_PodborForPosDesignation);
    if (relationType == AvsIDCache.Relation_Zagotovka)
      minimalAttributeList.Add(new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00622-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_ArticleID, "Идентификатор изделия", ColumnContents.Text));
    return minimalAttributeList;
  }

  /// <summary>Создать список атрибутов, нужных для расчёта массы</summary>
  /// <returns></returns>
  public List<AvsRowAttributeInfo> CreateAttributeListForMassaCalc()
  {
    return new List<AvsRowAttributeInfo>(20)
    {
      new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00275-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Weight, "Масса изделия", ColumnContents.Text),
      new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00276-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_UnitWeight, "Удельная масса изделия", ColumnContents.Text),
      new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00277-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Size, "Размеры", ColumnContents.Text)
    };
  }

  /// <summary>Получить описатели колонок для строки спецификации. Используется в запросе данных с сервера</summary>
  /// <param name="relationType">Тип связи, для которой запрашиваются атрибуты</param>
  /// <param name="objectsOnly">Получить атрибуты только для объектов</param>
  /// <param name="sortAttrs">Включая атрибуты для сортировки</param>
  /// <param name="useCurrentAttrMaps">Использовать текущие карты атрибутов</param>
  /// <param name="tableViewAttrs">Включая атрибуты для табличного вида</param>
  /// <param name="includeSearchId">Добавлять идентификатор объектов Search</param>
  /// <returns>Описатели колонок для строки спецификации</returns>
  private ColumnDescriptor[] GetAllColumnDescriptorsForSpecRow(
    int relationType,
    bool objectsOnly,
    bool sortAttrs,
    bool useCurrentAttrMaps,
    bool tableViewAttrs,
    bool includeSearchId)
  {
    List<AvsRowAttributeInfo> requiredAttributeList = this.CreateRequiredAttributeList(relationType, includeSearchId);
    List<AvsRowAttributeInfo> collection1 = this.avsWindow == null ? new List<AvsRowAttributeInfo>() : this.avsWindow.GetGridViewColumns();
    List<AvsRowAttributeInfo> rowAttributeInfoList = new List<AvsRowAttributeInfo>(requiredAttributeList.Count + (this.docRowFields_VarFormV != null ? this.docRowFields_VarFormV.Count : this.docRowFields.Count) + collection1.Count);
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) requiredAttributeList);
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) this.CollectCellOutputMappingAttributes());
    if (tableViewAttrs && collection1.Count > 0)
      rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) collection1);
    if (sortAttrs && this.SortSchema != null)
      rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) this.SortSchema.GetAllAttrInfo());
    List<AvsRowAttributeInfo> collection2 = new List<AvsRowAttributeInfo>();
    foreach (RemarkAttribute remarkAttribute in this.noteFieldSettings.Items)
      collection2.Add(remarkAttribute.CreateRowAttrInfo());
    if (relationType == AvsIDCache.Relation_Document)
      collection2.Add(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format));
    if (!objectsOnly && relationType == AvsIDCache.Relation_Project)
      collection2.Add(new AvsRowAttributeInfo(true, AvsIDCache.Attr_Zone));
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) collection2);
    if (useCurrentAttrMaps)
    {
      AttributeValueMap valueMapForRelation = this.GetAttributeValueMapForRelation(relationType);
      if (valueMapForRelation != null && valueMapForRelation.AttrsInfo != null)
        rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) valueMapForRelation.AttrsInfo);
      AttributeValueMap valueMapForObject = this.GetAttributeValueMapForObject(relationType == AvsIDCache.Relation_Document);
      if (valueMapForObject.AttrsInfo != null)
        rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) valueMapForObject.AttrsInfo);
    }
    this.FilterColumnDescriptorsForSpecRow(rowAttributeInfoList, relationType, objectsOnly);
    return this.CreateColumnDescriptors(rowAttributeInfoList);
  }

  internal static List<AvsRowAttributeInfo> CollectCellOutputMappingAttributesForTemplate(
    long docId,
    AVSDocumentType docType,
    AVSDocumentForm form,
    ImDocumentData templateDoc)
  {
    List<AvsRowAttributeInfo> rowAttributeInfoList = new List<AvsRowAttributeInfo>();
    List<AvsRowAttributeInfo> docRowFields1 = new List<AvsRowAttributeInfo>();
    List<AvsRowAttributeInfo> docRowAttributes = new List<AvsRowAttributeInfo>();
    List<AvsRowAttributeInfo> docRowFields2 = new List<AvsRowAttributeInfo>();
    List<AvsRowAttributeInfo> docRowFields3 = new List<AvsRowAttributeInfo>();
    TableData avsDocRow = AVSDocument.FindAvsDocRow(templateDoc);
    TableData node1 = (TableData) templateDoc.FindNode("Строка спецификации. EXP");
    TableData node2 = (TableData) templateDoc.FindNode("Строка спецификации. Форма Б");
    AVSDocument.UpdateDocumentRowFieldsInfo(avsDocRow, docType, form == AVSDocumentForm.B, ref docRowFields1, ref docRowAttributes);
    if (docType == AVSDocumentType.ExportSpecification)
      AVSDocument.UpdateDocumentRowFieldsInfo(node1, docType, true, ref docRowFields2, ref docRowAttributes);
    if (form == AVSDocumentForm.V)
      AVSDocument.UpdateDocumentRowFieldsInfo(node2, docType, true, ref docRowFields3, ref docRowAttributes);
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) docRowFields1);
    if (form == AVSDocumentForm.V && docRowFields3 != null)
    {
      for (int index1 = 0; index1 < docRowFields3.Count; ++index1)
      {
        for (int index2 = 0; index2 < docRowFields1.Count; ++index2)
        {
          if (!docRowFields1[index2].Equals((AttributeInfo) docRowFields3[index1]))
            rowAttributeInfoList.Add(docRowFields3[index1]);
        }
      }
    }
    if (docType == AVSDocumentType.ExportSpecification)
    {
      for (int index = 0; index < docRowFields2.Count; ++index)
      {
        if (docRowFields2[index] != null)
          rowAttributeInfoList.Add(docRowFields2[index]);
      }
    }
    if (docRowAttributes != null && docRowAttributes.Count > 0)
      rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) docRowAttributes);
    Dictionary<int, AvsRowAttributeInfo> dictionary1 = new Dictionary<int, AvsRowAttributeInfo>();
    Dictionary<int, AvsRowAttributeInfo> dictionary2 = new Dictionary<int, AvsRowAttributeInfo>();
    bool flag = false;
    if (avsDocRow != null)
    {
      SettingsStructure settingsStructure = (SettingsStructure) null;
      OutputAttributeMappingScheme orLoad = OutputAttributeMappingScheme.CreateOrLoad(docId, ref settingsStructure);
      foreach (TextData textData in (IEnumerable<TextData>) avsDocRow.TextCellsEnumerator)
      {
        string cellIdForCollect = textData.Id;
        if (cellIdForCollect.IndexOf(AVSRow.DocAttr_Count) == 0)
        {
          if (!flag)
          {
            flag = true;
            cellIdForCollect = AVSRow.DocAttr_Count;
          }
          else
            continue;
        }
        if (cellIdForCollect.IndexOf(AVSRow.DocAttr_PosDesignation) == 0)
          cellIdForCollect = MetaDataHelper.GetAttributeTypeName(AvsIDCache.Attr_PosDesignation);
        IEnumerable<CellOutputMapping> cellOutputMappings = orLoad.GetOverallMappingList().Where<CellOutputMapping>((System.Func<CellOutputMapping, bool>) (m => m.CellId == cellIdForCollect));
        if (cellOutputMappings != null)
        {
          foreach (CellOutputMapping cellOutputMapping in cellOutputMappings)
          {
            foreach (AttributeMapping attributeMapping in cellOutputMapping.Items.OfType<AttributeMapping>())
            {
              if (attributeMapping.IsDBObjectAttribute)
              {
                if (!dictionary1.ContainsKey(attributeMapping.AttributeID))
                  dictionary1.Add(attributeMapping.AttributeID, new AvsRowAttributeInfo(attributeMapping.AttributeInfo));
              }
              else if (attributeMapping.IsDBRelationAttribute && !dictionary2.ContainsKey(attributeMapping.AttributeID))
                dictionary2.Add(attributeMapping.AttributeID, new AvsRowAttributeInfo(attributeMapping.AttributeInfo));
            }
          }
        }
      }
    }
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) dictionary1.Values);
    rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) dictionary2.Values);
    return rowAttributeInfoList;
  }

  internal List<AvsRowAttributeInfo> CollectCellOutputMappingAttributes()
  {
    List<AvsRowAttributeInfo> fieldsAndAttributes = this.GetDocRowFieldsAndAttributes();
    Dictionary<int, AvsRowAttributeInfo> dictionary1 = new Dictionary<int, AvsRowAttributeInfo>();
    Dictionary<int, AvsRowAttributeInfo> dictionary2 = new Dictionary<int, AvsRowAttributeInfo>();
    bool flag = false;
    foreach (TextData textData in (IEnumerable<TextData>) this.avsRowTemplate.TextCellsEnumerator)
    {
      string cellIdForCollect = textData.Id;
      if (cellIdForCollect.IndexOf(AVSRow.DocAttr_Count) == 0)
      {
        if (!flag)
        {
          flag = true;
          cellIdForCollect = AVSRow.DocAttr_Count;
        }
        else
          continue;
      }
      if (cellIdForCollect.IndexOf(AVSRow.DocAttr_PosDesignation) == 0)
        cellIdForCollect = this.Field_PosDesignation.Name;
      IEnumerable<CellOutputMapping> cellOutputMappings = this.CellTextOutputAttributeMappingSettings.GetOverallMappingList().Where<CellOutputMapping>((System.Func<CellOutputMapping, bool>) (m => m.CellId == cellIdForCollect));
      if (cellOutputMappings != null)
      {
        foreach (CellOutputMapping cellOutputMapping in cellOutputMappings)
        {
          foreach (AttributeMapping attributeMapping in cellOutputMapping.Items.OfType<AttributeMapping>())
          {
            if (attributeMapping.IsDBObjectAttribute)
            {
              if (!dictionary1.ContainsKey(attributeMapping.AttributeID))
                dictionary1.Add(attributeMapping.AttributeID, new AvsRowAttributeInfo(attributeMapping.AttributeInfo));
            }
            else if (attributeMapping.IsDBRelationAttribute && !dictionary2.ContainsKey(attributeMapping.AttributeID))
              dictionary2.Add(attributeMapping.AttributeID, new AvsRowAttributeInfo(attributeMapping.AttributeInfo));
          }
        }
      }
    }
    fieldsAndAttributes.AddRange((IEnumerable<AvsRowAttributeInfo>) dictionary1.Values);
    fieldsAndAttributes.AddRange((IEnumerable<AvsRowAttributeInfo>) dictionary2.Values);
    return fieldsAndAttributes;
  }

  /// <summary>метод, который соберёт ссылки на графы</summary>
  internal List<AvsRowAttributeInfo> CollectDocumentRowAttrInfo()
  {
    List<AvsRowAttributeInfo> rowAttributeInfoList = new List<AvsRowAttributeInfo>();
    TableData tableData = this.IsExportSP ? this.avsRowExpTemplate : (this.AvsDocumentForm == AVSDocumentForm.V ? this.avsRowFormBTemplate : this.avsRowTemplate);
    if (tableData == null)
      throw new Exception("Не назначен шаблон строки документа!");
    int num = -1;
    foreach (TextData textData in (IEnumerable<TextData>) tableData.TextCellsEnumerator)
    {
      ++num;
      rowAttributeInfoList.Add(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, textData.Name)
      {
        IndexInValueList = num
      });
    }
    return rowAttributeInfoList;
  }

  private void FilterColumnDescriptorsForSpecRow(
    List<AvsRowAttributeInfo> attrInfoList,
    int relationType,
    bool objectsOnly)
  {
    int relationDocument = AvsIDCache.Relation_Document;
    for (int index = attrInfoList.Count - 1; index >= 0; --index)
    {
      bool flag1 = attrInfoList[index] == null;
      if (!flag1 && attrInfoList[index].IsVirtualAttribute)
      {
        if (attrInfoList[index].AttributeGuid == AvsIDCache.Attr_NominalAndLimitValues_NoteText.AttributeGuid)
        {
          attrInfoList[index] = this.Attr_NominalValue;
          attrInfoList.Insert(index, this.Attr_LimitValues);
        }
        else
          flag1 = true;
      }
      bool flag2 = ((flag1 ? 1 : 0) | (!objectsOnly ? 0 : (attrInfoList[index].IsRelationAttribute ? 1 : 0))) != 0;
      if (!flag2 && !objectsOnly && attrInfoList[index].IsRelationAttribute && attrInfoList[index].AttributeId > 0)
        flag2 = MetaDataHelper.GetAttribute4RelationType(relationType, attrInfoList[index].AttributeId) == null;
      if (flag2)
        attrInfoList.RemoveAt(index);
    }
  }

  /// <summary>Создать описатели всех необходимых колонок. Используется в запросе данных с сервера</summary>
  /// <param name="attrList">Список атрибутов</param>
  /// <returns>Описатели колонок для строки спецификации</returns>
  private ColumnDescriptor[] CreateColumnDescriptors(List<AvsRowAttributeInfo> attrList)
  {
    Dictionary<int, int> dictionary1 = new Dictionary<int, int>(attrList.Count);
    Dictionary<int, int> dictionary2 = new Dictionary<int, int>(attrList.Count);
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(attrList.Count);
    int orderByID = 0;
    for (int index = 0; index < attrList.Count; ++index)
    {
      AvsRowAttributeInfo attr = attrList[index];
      int num;
      if (attr != null && attr.AttributeId != -1 && (!attr.IsObjectAttribute || !dictionary2.TryGetValue(attr.AttributeId, out num) ? (!attr.IsRelationAttribute ? 0 : (dictionary1.TryGetValue(attr.AttributeId, out num) ? 1 : 0)) : 1) == 0)
      {
        AttributeSourceTypes attributeSource = attr.IsRelationAttribute ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object;
        if (AVSRow.IsCountAttribute(attr) || attr.IsObjectAttribute && (attr.AttributeId == AvsIDCache.Attr_Weight || attr.AttributeId == AvsIDCache.Attr_UnitWeight))
        {
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0));
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
        }
        else if (attr.FieldType == FieldTypes.ftObjectLink && attr.AttributeId != this.Attr_Section.AttributeId)
        {
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
        }
        else
        {
          SortOrders sort;
          if (attr.AttributeId == AvsIDCache.Attr_SortIndex)
          {
            sort = SortOrders.ASC;
            orderByID = 0;
          }
          else if (attr.AttributeId == AvsIDCache.Attr_Designation)
          {
            sort = SortOrders.ASC;
            orderByID = 1;
          }
          else if (attr.AttributeId == AvsIDCache.Attr_Name)
          {
            sort = SortOrders.ASC;
            orderByID = 2;
          }
          else
            sort = SortOrders.NONE;
          columnDescriptorList.Add(new ColumnDescriptor((object) attr.AttributeId, attributeSource, attr.ColumnContent, ColumnNameMapping.Index, sort, orderByID));
        }
        if (attr.IsRelationAttribute)
          dictionary1.Add(attr.AttributeId, columnDescriptorList.Count - 1);
        else
          dictionary2.Add(attr.AttributeId, columnDescriptorList.Count - 1);
      }
    }
    ColumnDescriptor[] array = new ColumnDescriptor[columnDescriptorList.Count];
    columnDescriptorList.CopyTo(array);
    return array;
  }

  /// <summary>Записи равны с точки зрения "общих данных" для групповой СП формы А</summary>
  /// <param name="articleData1">Данные первого исполнения</param>
  /// <param name="articleData2">Данные второго исполнения</param>
  /// <param name="ignoreSortIndex">Игнорировать индекс сортировки</param>
  /// <param name="ignoreNullSortIndex">Игнорировать пустой индекс сортировки</param>
  /// <returns></returns>
  internal bool AvsRowsIsEqual(
    RelationAttributeValuesCache articleData1,
    RelationAttributeValuesCache articleData2,
    bool ignoreSortIndex,
    bool ignoreNullSortIndex)
  {
    return articleData1.ObjectId == articleData2.ObjectId && (ignoreSortIndex || (ignoreNullSortIndex ? (AVSRow.IsEqualOrFreeSortIndex(articleData1, articleData2) ? 1 : 0) : (AVSRow.IsEqualSortIndex(articleData1, articleData2) ? 1 : 0)) != 0) && (articleData1.RelationType == AvsIDCache.Relation_Document || articleData1.RelationType != articleData2.RelationType || AVSRow.IsEqualStringAttributeValues(this.Field_Position, articleData1, articleData2) && AVSRow.IsEqualStringAttributeValues(this.Field_Zone, articleData1, articleData2) && AVSRow.IsEqualCount(articleData1, articleData2) && AVSRow.IsEqualStringAttributeValues(this.Attr_DopZamenText, articleData1, articleData2)) && AVSRow.IsEqualStringAttributeValues(this.Field_Note, articleData1, articleData2);
  }

  /// <summary>Получить ссылку на объект части из узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Получить ссылку на объект части</returns>
  public ReferenceToDBObject GetRefToChapter(TableData docNode)
  {
    INodeWithReference nodeWithReference = docNode != null ? (INodeWithReference) docNode : throw new ArgumentNullException(nameof (docNode));
    if (nodeWithReference == null)
      return (ReferenceToDBObject) null;
    if (nodeWithReference.Reference is ReferenceToDBObject reference && (reference.DBObjectID == -1L || reference.DBObjectType == -1))
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      if (SpecificationSectionInfo.SectionDictionaryByGuid.Contains((object) reference.DBObjectGuid) && SpecificationSectionInfo.SectionDictionaryByGuid[(object) reference.DBObjectGuid] is SpecificationSectionInfo specificationSectionInfo)
        reference.DBObjectInfo.SetDBObjectInfo(specificationSectionInfo.SectionGuid, specificationSectionInfo.SectionID, specificationSectionInfo.SectionType, specificationSectionInfo.Caption);
    }
    return reference;
  }

  /// <summary>Получить раздел для узла документа</summary>
  /// <param name="docNode">Узел</param>
  /// <param name="specForm">Форма конструкторского документа</param>
  /// <param name="autoCreateChapter">Создать раздел, если его нет</param>
  /// <returns></returns>
  public Chapter GetChapterForDocNode(
    TableData docNode,
    AVSDocumentForm specForm,
    bool autoCreateChapter,
    bool isExportTable)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (docNode.Tag is Chapter chapter2)
      return chapter2;
    if (docNode.PrevTable != null)
    {
      docNode = docNode.FindFirstTable();
      if (docNode.Tag is Chapter chapter2)
        return chapter2;
    }
    Guid chapterGuid = Guid.Empty;
    long num1 = -1;
    int num2 = -1;
    ReferenceToDBObject refToChapter = this.GetRefToChapter(docNode);
    if (refToChapter != null)
    {
      num1 = refToChapter.DBObjectID;
      num2 = refToChapter.DBObjectType;
      chapterGuid = refToChapter.DBObjectGuid;
    }
    string str = docNode.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
    if (str == "")
      str = (string) null;
    Chapter chapter3 = (Chapter) null;
    if (docNode.Parent is TableData parent)
      chapter3 = this.GetChapterForDocNode(parent, specForm, autoCreateChapter, isExportTable);
    if (chapter3 != null)
    {
      if (num1 != -1L)
        chapter2 = chapter3.GetChapter(num1);
      if (chapter2 == null && chapterGuid != Guid.Empty)
        chapter2 = chapter3.GetChapter(chapterGuid);
      if (chapter2 == null && str == Chapter.ProductVariableData_TypeName && specForm == AVSDocumentForm.A)
      {
        string attributeValue = docNode.GetAttributeValue(Chapter.Designation_AttributeName, false);
        ProductInfo productByPrototype = this.FindProductByPrototype(chapterGuid, attributeValue);
        if (productByPrototype != null)
          chapter2 = chapter3.GetChapter(productByPrototype.Id);
      }
    }
    else if (this.rootChapters != null)
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
      {
        if (this.rootChapters[index] != null && this.rootChapters[index].IsAdditionalChapter && this.rootChapters[index].ChapterGuid == chapterGuid)
        {
          chapter2 = this.rootChapters[index];
          break;
        }
        if (this.rootChapters[index] == null)
          LogManager.AddLine("AVS. error#SP.GETCHAPT1", true);
      }
    }
    chapter2?.AddDocNode(docNode, isExportTable);
    if (chapter2 == null & autoCreateChapter)
    {
      if (str == Chapter.CommonData_TypeName)
      {
        if (chapter3 == null)
        {
          if (docNode.IsTopLevelTable && (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V))
          {
            docNode.RemoveAttribute(Chapter.DocNodeType_AttributeName, false, false);
            return (Chapter) null;
          }
          if (this.commonDataChapter == null)
            this.CommonDataChapter = this.CreateCommonDataChapter(!this.IsSpecification);
          chapter2 = this.CommonDataChapter;
        }
        else
        {
          if (chapter3 is AdditionalChapter additionalChapter)
            chapter2 = additionalChapter.InnerCommonDataChapter;
          if (chapter2 == null)
          {
            chapter2 = this.CreateCommonDataChapter(!this.IsSpecification);
            chapter3.AddChapter(chapter2, false, false, false, (TableData) null);
          }
        }
        if (chapter2 != null)
          chapter2.AddDocNode(docNode, isExportTable);
        else
          LogManager.AddLine("AVS. GetChapterForDocNode. Warning1: Chapter not found", true);
      }
      else if (str == Chapter.VariableData_TypeName)
      {
        if (chapter3 == null)
        {
          if (specForm == AVSDocumentForm.A && this.variableDataChapter_FormA == null)
            this.VariableDataChapter_FormA = new VariableDataChapterFormA(this, this.productsInfo, true);
          else if (specForm == AVSDocumentForm.V && this.variableDataChapter_FormV == null)
            this.VariableDataChapter_FormV = new VariableDataChapterFormV(this);
          chapter2 = this.VariableDataChapter;
        }
        else
        {
          AdditionalChapter additionalChapter = chapter3 as AdditionalChapter;
          switch (specForm)
          {
            case AVSDocumentForm.A:
              if (additionalChapter != null)
                chapter2 = (Chapter) additionalChapter.InnerVariableData_FormA;
              if (chapter2 == null)
              {
                chapter2 = (Chapter) new VariableDataChapterFormA(this, this.productsInfo, true);
                chapter3.AddChapter(chapter2, false, false, false, (TableData) null);
                break;
              }
              break;
            case AVSDocumentForm.V:
              if (additionalChapter != null)
                chapter2 = (Chapter) additionalChapter.InnerVariableData_FormV;
              if (chapter2 == null)
              {
                chapter2 = (Chapter) new VariableDataChapterFormV(this);
                chapter3.AddChapter(chapter2, false, false, false, (TableData) null);
                break;
              }
              break;
          }
        }
        if (chapter2 != null)
          chapter2.AddDocNode(docNode, isExportTable);
        else
          LogManager.AddLine("AVS. GetChapterForDocNode. Warning2: Chapter not found", true);
      }
      else if (str == Chapter.ProductVariableData_TypeName)
      {
        if (chapter3 != null)
        {
          if (this.IsSpecification)
          {
            if (num1 != -1L && chapter3 is VariableDataChapterFormA)
            {
              int productIndex = this.GetProductIndex(num1);
              if (productIndex != -1)
              {
                chapter2 = (Chapter) new ProductVariableDataChapter(this, this.productsInfo[productIndex], (long) (productIndex * 100), true);
                chapter3.AddChapter(chapter2, false, false, false, (TableData) null);
                chapter2.AddDocNode(docNode, isExportTable);
              }
            }
          }
          else
          {
            ProductInfo product = (ProductInfo) null;
            string attributeValue = docNode.GetAttributeValue(Chapter.Designation_AttributeName, true);
            if (this.productsInfo.Count == 1 && this.productsInfo[0].Designation == attributeValue)
            {
              product = this.productsInfo[0];
              if (this.variableDataChapter_FormA != null)
                chapter2 = this.variableDataChapter_FormA.GetProductChapter(product);
              else
                LogManager.AddLine("AVS. GetChapterForDocNode. Warning3: variableDataChapter is null", true);
            }
            else
            {
              if (docNode.ContainsAttribute(Chapter.ProductVariableData_AttributeName))
              {
                product = ProductInfo.Deserialize(docNode.GetAttributeValue(Chapter.ProductVariableData_AttributeName, true));
                if (product.Id.IsDefinedId())
                {
                  using (SessionKeeper sk = new SessionKeeper())
                    this.UpdateProductInfoForNewSourceRelations(product, sk);
                }
              }
              if (product == null)
                product = new ProductInfo(Guid.Empty, -1L, attributeValue);
              this.UpdateProductAdditionalAttributes(product);
              product.Designation = attributeValue;
              bool flag = false;
              if (!this.productsByRelations.IsNullOrEmpty<ProductInfo>() && product.Id.IsDefinedId() && chapter3 is VariableDataChapterFormA)
                flag = !this.productsByRelations.Contains<ProductInfo>((Predicate<ProductInfo>) (p => Math.Abs(p.Id) == Math.Abs(product.Id)));
              if (!flag)
              {
                this.productsInfo.Add(product);
                int count = this.productsInfo.Count;
                chapter2 = (Chapter) new ProductVariableDataChapter(this, product, (long) (count * 100), true);
                if (this.variableDataChapter_FormA != null)
                  this.variableDataChapter_FormA.AddChapter(chapter2, false, false, false, (TableData) null);
                else
                  LogManager.AddLine("AVS. GetChapterForDocNode. Warning5: variableDataChapter is null", true);
              }
            }
            if (chapter2 != null)
            {
              if (chapterGuid != Guid.Empty)
                chapter2.ChapterGuid = chapterGuid;
              chapter2.AddDocNode(docNode, isExportTable);
            }
            else
              LogManager.AddLine("AVS. GetChapterForDocNode. Warning6: Chapter not found", true);
          }
        }
      }
      else if (str == Chapter.AdditionalChapter_TypeName)
      {
        AdditionalChapterSettings chapterSettings = (AdditionalChapterSettings) null;
        string attributeValue = docNode.GetAttributeValue(Chapter.CaptionFormat_AttributeName, true);
        if (refToChapter != null)
        {
          chapterSettings = new AdditionalChapterSettings(refToChapter.DBObjectGuid, -1L, attributeValue, -1L);
        }
        else
        {
          LogManager.AddLine("AVS. GetChapterForDocNode. Warning7: refToDB is null");
          if (attributeValue != null && attributeValue != "")
          {
            if (chapter3 != null)
            {
              for (int index = 0; index < chapter3.Chapters.Count; ++index)
              {
                if (chapter3.Chapters[index] is AdditionalChapter chapter4 && chapter4.caption == attributeValue)
                {
                  LogManager.AddLine("AVS. GetChapterForDocNode. Warning7.1: use exist chapter");
                  return (Chapter) chapter4;
                }
              }
            }
            LogManager.AddLine("AVS. GetChapterForDocNode. Warning7.2: chapter not found");
            return (Chapter) null;
          }
        }
        int num3 = chapter3 != null ? chapter3.AddChapter(chapter2 = (Chapter) new AdditionalChapter(this, chapterSettings, true), false, false, false, (TableData) null) : this.AddRootChapter(chapter2 = (Chapter) new AdditionalChapter(this, chapterSettings, false), false);
        chapter2.SortIndex = (long) num3;
        chapter2.AddDocNode(docNode, isExportTable);
      }
      else if (!AVSDocument.IsProductPageLinksDocNode((DocumentTreeNode) docNode))
      {
        if (chapter3 != null && !(chapter3 is VariableDataChapterFormA))
        {
          if (num2 == AvsIDCache.ObjType_SpecificationSection)
          {
            if (!SpecificationSectionInfo.Cached)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
                SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
            }
            SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(num1);
            if (sectionById != null)
            {
              chapter2 = (Chapter) new SpecificationSection(this, sectionById);
              chapter2.AddDocNode(docNode, isExportTable);
            }
          }
          if (str == Chapter.AdditionalComplectGroup_TypeName)
          {
            AVSRowGroup group = (AVSRowGroup) new AVSAdditionalComplectRowGroup(this);
            group.AddDocNode(docNode, isExportTable);
            if (chapter3 is SpecificationSection specificationSection)
              specificationSection.AddGroup(group, false, false);
            chapter2 = (Chapter) group;
          }
          else if (chapter2 != null)
            chapter3.AddChapter(chapter2, true, false, false, (TableData) null);
        }
        else if (!this.IsSpecification && (str == Chapter.ProductVariableData_TypeName || str == Chapter.Section_TypeName))
        {
          string attributeValue = docNode.GetAttributeValue(Chapter.Designation_AttributeName, true);
          if (this.productsInfo != null)
          {
            if (this.variableDataChapter_FormA != null)
            {
              if (this.productsInfo[0] == null)
                LogManager.AddLine("AVS. GetChapterForDocNode. Warning8: products[0] not found");
              if (this.productsInfo.Count == 1 && this.productsInfo[0] != null && this.productsInfo[0].Designation == attributeValue)
              {
                chapter2 = this.variableDataChapter_FormA.GetProductChapter(this.productsInfo[0]);
              }
              else
              {
                ProductInfo productInfo = !docNode.ContainsAttribute(Chapter.ProductVariableData_AttributeName) ? new ProductInfo(Guid.Empty, -1L, attributeValue) : ProductInfo.Deserialize(docNode.GetAttributeValue(Chapter.ProductVariableData_AttributeName, true));
                productInfo.Designation = attributeValue;
                this.productsInfo.Add(productInfo);
                int count = this.productsInfo.Count;
                chapter2 = (Chapter) new ProductVariableDataChapter(this, productInfo, (long) (count * 100), true);
                this.variableDataChapter_FormA.AddChapter(chapter2, false, false, false, (TableData) null);
              }
            }
            else
              LogManager.AddLine("AVS. GetChapterForDocNode. Warning10: variableDataChapter is null", true);
          }
          else
            LogManager.AddLine("AVS. GetChapterForDocNode. Warning11: productsInfo is null", true);
          if (chapter2 != null)
          {
            if (chapterGuid != Guid.Empty)
              chapter2.ChapterGuid = chapterGuid;
            chapter2.AddDocNode(docNode, isExportTable);
          }
          else
            LogManager.AddLine("AVS. GetChapterForDocNode. Warning12: Chapter not found");
        }
      }
    }
    return chapter2;
  }

  private ProductInfo FindProductByPrototype(Guid chapterGuid, string designation)
  {
    long chapterId = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      chapterId = sessionKeeper.Session.GetObjectInfo(chapterGuid).ObjectID;
    ProductInfo productByPrototype = (ProductInfo) null;
    if (!chapterId.IsUndefinedId())
      productByPrototype = this.productsInfo.Find((Predicate<ProductInfo>) (p => p.PrototypeId == chapterId && p.ParentVersionId.IsUndefinedId() || p.ParentVersionId == chapterId));
    if (productByPrototype == null && !string.IsNullOrEmpty(designation))
      productByPrototype = this.productsInfo.Find((Predicate<ProductInfo>) (p => p.Designation == designation));
    return productByPrototype;
  }

  /// <summary>Сканировать структуру раздела и занести строки документа в словари</summary>
  /// <param name="docChapter">Узел документа</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="productsInDoc">Список для сохранения порядка исполнений в документе. Если null, то не используется</param>
  internal void ScanChapterStructure(
    TableData docChapter,
    bool isExportTable,
    RowDictionariesForLoadDocument rowDicts,
    List<ProductInfo> productsInDoc)
  {
    if (docChapter == null)
      throw new ArgumentNullException(nameof (docChapter));
    if (rowDicts == null)
      throw new ArgumentNullException(nameof (rowDicts));
    Chapter chapterForDocNode1 = this.GetChapterForDocNode(docChapter, this.AvsDocumentForm, true, isExportTable);
    if (chapterForDocNode1 == null)
      return;
    if (productsInDoc != null && chapterForDocNode1 is ProductVariableDataChapter)
      productsInDoc.Add(chapterForDocNode1.Product);
    if (productsInDoc != null && productsInDoc.Count > 0)
      productsInDoc = (List<ProductInfo>) null;
    SpecificationSection specificationSection = chapterForDocNode1 as SpecificationSection;
    avsRowGroup = (AVSRowGroup) null;
    if (specificationSection == null)
    {
      if (chapterForDocNode1 is AVSRowGroup avsRowGroup)
        specificationSection = avsRowGroup.Section;
      if (chapterForDocNode1.Chapters.Count == 1 && chapterForDocNode1.Chapters[0].UseParentDocNode)
        specificationSection = chapterForDocNode1.Chapters[0] as SpecificationSection;
    }
    if (specificationSection == null)
    {
      DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(docChapter);
      while (dataNodesEnumerator.MoveNext())
      {
        if (dataNodesEnumerator.Current is TableData tableData)
        {
          string attributeValue = tableData.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
          Chapter chapterForDocNode2 = this.GetChapterForDocNode(tableData, this.AvsDocumentForm, true, isExportTable);
          if (chapterForDocNode2 != null)
          {
            chapterForDocNode2.AddDocNode(tableData, isExportTable);
          }
          else
          {
            if (attributeValue == Chapter.AVSRow_TypeName && chapterForDocNode1.IsCommonDataChapter)
            {
              if (!(this.commonDataChapter is SpecificationSection))
              {
                Chapter commonDataChapter = this.commonDataChapter;
                this.CommonDataChapter = (Chapter) new SpecificationSection(this, new SpecificationSectionInfo(AVSDocument.ChapterCommonDataGuid, -1L, -1, "Перечень элементов", 0L, "", new int[0], new long[0]));
                this.commonDataChapter.AddDocNode(docChapter, isExportTable);
                this.commonDataChapter.caption = commonDataChapter.caption;
                this.commonDataChapter.Product = commonDataChapter.Product;
                this.commonDataChapter.NodeLevel = commonDataChapter.NodeLevel;
                this.commonDataChapter.ChapterType = commonDataChapter.ChapterType;
              }
              this.ScanChapterStructure(docChapter, isExportTable, rowDicts, productsInDoc);
              break;
            }
            if (!AVSDocument.IsNoteRowDocNode((DocumentTreeNode) tableData))
            {
              if (!this.ReadOnly)
                dataNodesEnumerator.RemoveCurrentAndGotoPrev();
            }
            else
              tableData = (TableData) null;
          }
          if (tableData != null)
            this.ScanChapterStructure(tableData, isExportTable, rowDicts, productsInDoc);
        }
      }
    }
    else
    {
      int index1 = 0;
      DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(docChapter);
      while (dataNodesEnumerator.MoveNext())
      {
        TableData current = dataNodesEnumerator.Current as TableData;
        if (AVSDocument.IsGroupDocNode((DocumentTreeNode) current))
          this.ScanChapterStructure(current, isExportTable, rowDicts, productsInDoc);
        else if (AVSDocument.IsSpecRowDocNode((DocumentTreeNode) current) || AVSDocument.IsNoteRowDocNode((DocumentTreeNode) current))
        {
          long result = long.MinValue;
          current.SetTableCellType(CellType.DataCell, false, false);
          List<Guid> relationsGuidsFromDocRow = AVSRow.GetRelationsGuidsFromDocRow(current);
          foreach (Guid key in relationsGuidsFromDocRow)
          {
            if (!rowDicts.docRowsByGuid.ContainsKey(key))
              rowDicts.docRowsByGuid.Add(key, current);
          }
          Guid empty = Guid.Empty;
          if (relationsGuidsFromDocRow.Count > 0)
            empty = relationsGuidsFromDocRow[0];
          Guid docNodeObjectGuid = this.GetDocNodeObjectGuid((DocumentTreeNode) current);
          if (docNodeObjectGuid != Guid.Empty)
          {
            List<TableData> tableDataList;
            if (!rowDicts.docRowsByObjectGuid.TryGetValue(docNodeObjectGuid, out tableDataList))
              rowDicts.docRowsByObjectGuid.Add(docNodeObjectGuid, tableDataList = new List<TableData>());
            tableDataList.Add(current);
            if (empty == Guid.Empty)
            {
              if (!rowDicts.docRowsWithoutRelationsByObjectGuid.TryGetValue(docNodeObjectGuid, out tableDataList))
                rowDicts.docRowsWithoutRelationsByObjectGuid.Add(docNodeObjectGuid, tableDataList = new List<TableData>());
              tableDataList.Add(current);
            }
          }
          string attributeValue1 = current.GetAttributeValue(AVSRow.RowAttr_SortIndex, true);
          List<TableData> tableDataList1 = new List<TableData>();
          if (attributeValue1 != "" && long.TryParse(attributeValue1, out result) && result != 0L && result != long.MinValue)
          {
            if (!rowDicts.docRowsBySortIndex.TryGetValue(result, out tableDataList1))
              rowDicts.docRowsBySortIndex.Add(result, tableDataList1 = new List<TableData>());
            else if (tableDataList1.Count > 0)
            {
              RectangleElement firstCell1 = current.TopLevelTable.FindFirstCell();
              if (this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V)
              {
                for (int index2 = 0; index2 < tableDataList1.Count; ++index2)
                {
                  RectangleElement firstCell2 = tableDataList1[index2].TopLevelTable.FindFirstCell();
                  if (firstCell1 == firstCell2 || firstCell2 == this.avsDocTable && this.AvsDocumentForm == AVSDocumentForm.V)
                  {
                    result = 0L;
                    tableDataList1 = (List<TableData>) null;
                    break;
                  }
                }
              }
              else
              {
                result = 0L;
                tableDataList1 = (List<TableData>) null;
              }
            }
            if (result != 0L && tableDataList1 != null)
              tableDataList1.Add(current);
            else
              current.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
          }
          AVSRow row = (AVSRow) null;
          if (empty != Guid.Empty)
            row = this.GetAvsDocRow(empty);
          if (row == null)
            row = this.GetAvsDocRow((DocumentTreeNode) current);
          if (row == null && tableDataList1 != null && tableDataList1.Count > 0)
          {
            for (int index3 = 0; row == null && index3 < tableDataList1.Count; ++index3)
              row = this.GetAvsDocRow((DocumentTreeNode) tableDataList1[index3]);
          }
          if (row == null)
          {
            if (AVSDocument.IsNoteRowDocNode((DocumentTreeNode) current))
            {
              row = new AVSRow(this);
              row.IsNoteRow = true;
            }
            else
              row = new AVSRow(this, -1L, docNodeObjectGuid, -1, -1L, empty, -1, Guid.Empty, -1L);
            row.SetSortIndex(result, false, false, false);
            if (!rowDicts.specRowsBySortIndex.ContainsKey(result))
              rowDicts.specRowsBySortIndex.Add(result, row);
            row.IsSorted = true;
            row.UpdatePositionsStepFromDocNode(current);
            string attributeValue2 = current.GetAttributeValue(AVSRow.RowAttr_RelationType, true);
            if (attributeValue2 != "")
              row.RelType = MetaDataHelper.GetRelationTypeID(attributeValue2);
            row.AddDocNode(current);
            if (avsRowGroup != null)
              row.Group = avsRowGroup;
            if (index1 == 0)
              index1 = specificationSection.AddRow(row, result);
            else
              specificationSection.InsertRow(index1, row);
            foreach (Guid key in relationsGuidsFromDocRow)
            {
              if (!this.relationGuidDictionary.ContainsKey(key))
                this.relationGuidDictionary.Add(key, row);
            }
            ++index1;
          }
          else
          {
            row.AddDocNode(current);
            index1 = row.Index + 1;
          }
          row.IsNoteRow = AVSDocument.IsNoteRowDocNode((DocumentTreeNode) current);
          if (row.IsNoteRow)
            row.LoadDataFromDocRow(current, false, false, true);
        }
      }
    }
  }

  /// <summary>Сканировать таблицу спецификации</summary>
  /// <param name="docTable">Таблица документа</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="productsInDoc">Список для сохранения порядка исполнений в документе. Если null, то не используется</param>
  internal void ScanSpecificationTable(
    TableData docTable,
    bool isExportTable,
    RowDictionariesForLoadDocument rowDicts,
    List<ProductInfo> productsInDoc)
  {
    TableData tableData1 = (TableData) null;
    TableData tableData2 = docTable;
    bool flag1 = false;
    while (tableData2 != null)
    {
      if (AVSDocument.IsDocumentFormB(this.AvsDocumentForm))
      {
        this.commonDataChapter.AddDocNode(tableData2, isExportTable);
        if (tableData2.PrevCell == null)
          this.ScanProductHeadersOnPage(tableData2.Page, productsInDoc);
      }
      DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(tableData2);
      bool flag2 = true;
      bool flag3 = true;
      while (dataNodesEnumerator.MoveNext())
      {
        flag2 = false;
        int currentCellIndex = dataNodesEnumerator.CurrentCellIndex;
        TableData parentCell = dataNodesEnumerator.Current.ParentCell;
        if (currentCellIndex != -1 && parentCell != null && currentCellIndex < parentCell.Nodes.Count && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) dataNodesEnumerator.Current))
        {
          TableData current1 = dataNodesEnumerator.Current as TableData;
          string str = current1.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
          if (str == "")
            str = (string) null;
          if (!AVSDocument.IsProductPageLinksDocNode((DocumentTreeNode) dataNodesEnumerator.Current))
          {
            ReferenceToDBObject refToChapter = this.GetRefToChapter(current1);
            if (refToChapter != null || !string.IsNullOrEmpty(str))
            {
              if (str == Chapter.Section_TypeName || refToChapter != null && refToChapter.DBObjectType == AvsIDCache.ObjType_SpecificationSection)
              {
                if (this.AvsDocumentForm != AVSDocumentForm.A)
                {
                  if (this.AvsDocumentForm == AVSDocumentForm.V & flag1 && this.rootChapters.Count > 2 && this.rootChapters[2].IsAdditionalChapter)
                    ((AdditionalChapter) this.rootChapters[2]).InnerVariableData_FormV.AddDocNode(tableData2, isExportTable);
                  this.ScanChapterStructure(tableData2, isExportTable, rowDicts, (List<ProductInfo>) null);
                  break;
                }
                if (isExportTable)
                {
                  if (!this.commonDataChapter.HasDocNodesExp)
                  {
                    if (!this.ReadOnly)
                    {
                      tableData1 = this.commonDataChapter.CreateDocNode(this.commonChapterExpTemplate);
                      this.commonDataChapter.AddDocNode(tableData1, isExportTable);
                      this.avsDocTableExpMix.InsertChildNode(currentCellIndex, (DocumentTreeNode) tableData1, false, true, false, false, false);
                      dataNodesEnumerator.SetCurrentCell((RectangleElement) tableData1, dataNodesEnumerator.DataIndex);
                    }
                  }
                  else
                    tableData1 = this.commonDataChapter.DocNodesExp[0];
                }
                else if (!this.commonDataChapter.HasDocNodes)
                {
                  if (!this.ReadOnly)
                  {
                    tableData1 = this.commonDataChapter.CreateDocNode(this.commonChapterTemplate);
                    this.commonDataChapter.AddDocNode(tableData1, isExportTable);
                    this.avsDocTable.InsertChildNode(currentCellIndex, (DocumentTreeNode) tableData1, false, true, false, false, false);
                    dataNodesEnumerator.SetCurrentCell((RectangleElement) tableData1, dataNodesEnumerator.DataIndex);
                  }
                }
                else
                  tableData1 = this.commonDataChapter.DocNodes[0];
                if (flag3)
                {
                  while (dataNodesEnumerator.MoveNext())
                  {
                    if (dataNodesEnumerator.Current is TableData current2 && tableData1 != null)
                    {
                      tableData1.AddChildNode((DocumentTreeNode) current2, true, true, false, false);
                      dataNodesEnumerator.SetCurrentCell((RectangleElement) tableData1, dataNodesEnumerator.DataIndex - 1);
                    }
                  }
                  this.ScanChapterStructure(tableData1, isExportTable, rowDicts, (List<ProductInfo>) null);
                }
                else if (!this.ReadOnly)
                {
                  current1.UniteTable();
                  dataNodesEnumerator.RemoveCurrentAndGotoPrev();
                }
              }
              else if (str == Chapter.AdditionalChapter_TypeName || refToChapter != null && refToChapter.DBObjectType == AvsIDCache.ObjType_SpecificationChapter)
              {
                flag3 = false;
                this.ScanChapterStructure(current1, isExportTable, rowDicts, productsInDoc);
                flag1 = true;
              }
              else if (str == Chapter.CommonData_TypeName)
              {
                flag3 = false;
                this.commonDataChapter.AddDocNode(current1, isExportTable);
                this.ScanChapterStructure(current1, isExportTable, rowDicts, (List<ProductInfo>) null);
              }
              else if (str == Chapter.VariableData_TypeName)
              {
                flag3 = false;
                if (this.AvsDocumentForm == AVSDocumentForm.A)
                  this.variableDataChapter_FormA.AddDocNode(current1, isExportTable);
                else if (this.AvsDocumentForm == AVSDocumentForm.V)
                {
                  if (tableData2.FindFirstCell() != this.avsDocTable && this.variableDataChapter_FormV.DocNodes.Count == 0)
                  {
                    this.variableDataChapter_FormV.AddDocNode(tableData2, isExportTable);
                    if (tableData2.PrevCell == null)
                      this.ScanProductHeadersOnPage(tableData2.Page, productsInDoc);
                  }
                  this.variableDataChapter_FormV.AddDocNode(current1, isExportTable);
                }
                this.ScanChapterStructure(current1, isExportTable, rowDicts, productsInDoc);
              }
              else
              {
                flag3 = false;
                if (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V)
                {
                  this.commonDataChapter.AddDocNode(current1, isExportTable);
                  this.ScanChapterStructure(current1, isExportTable, rowDicts, (List<ProductInfo>) null);
                  while (dataNodesEnumerator.MoveNext())
                  {
                    if (dataNodesEnumerator.Current is TableData current3 && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) current3))
                    {
                      string attributeValue = current3.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
                      if (attributeValue != null && !(attributeValue == ""))
                      {
                        if (this.AvsDocumentForm == AVSDocumentForm.A)
                          this.variableDataChapter_FormA.DocNode = current3;
                        else if (this.AvsDocumentForm == AVSDocumentForm.V)
                          this.variableDataChapter_FormV.AddDocNode(current3, isExportTable);
                        this.ScanChapterStructure(current3, isExportTable, rowDicts, productsInDoc);
                        break;
                      }
                    }
                  }
                }
                else
                {
                  this.ScanChapterStructure(current1, isExportTable, rowDicts, productsInDoc);
                  if (!this.ReadOnly)
                  {
                    current1.UniteTable();
                    for (int index = current1.Nodes.Count - 1; index >= 0; --index)
                      current1.RemoveChildNodeAt(index, false, false, false);
                    dataNodesEnumerator.RemoveCurrentAndGotoPrev();
                  }
                }
              }
            }
          }
        }
      }
      if (flag2 && this.AvsDocumentForm == AVSDocumentForm.V)
        this.ScanChapterStructure(tableData2, isExportTable, rowDicts, (List<ProductInfo>) null);
      if (!AVSDocument.IsDocumentFormB(this.AvsDocumentForm) && this.AvsDocumentForm != AVSDocumentForm.V)
        break;
      if (tableData2.Page != null)
      {
        PageData lastPage = tableData2.Page.FindLastPage();
        PageData nextPage = ImDocumentData.GetNextPage(lastPage.Parent, lastPage.Index, true);
        tableData2 = nextPage == null ? (TableData) null : this.FindMainDocTableFromPage(nextPage, isExportTable);
      }
      else
        tableData2 = (TableData) null;
    }
  }

  internal virtual void LoadSourceProductByRelations()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.productsByRelations = AVSDocument.LoadProductsForAVSDocument(this.DocumentID, this.productAttributeList, true, this.FiltrationOwnerID, sessionKeeper.Session);
  }

  /// <summary>Сканировать структуру документа и занести строки документа в словари</summary>
  /// <param name="rowDicts">Словари строк документа</param>
  /// <param name="productsInDoc">Список для сохранения порядка исполнений в документе. Если null, то не используется</param>
  internal virtual void ScanDocumentStructure(
    RowDictionariesForLoadDocument rowDicts,
    List<ProductInfo> productsInDoc)
  {
    this.SuspendDocumentAndGridUpdates();
    try
    {
      this.LoadSourceProductByRelations();
      if (this.AvsDocumentForm == AVSDocumentForm.Single)
        this.LoadParentProductsFromDocument();
      TableData tableData1 = (TableData) null;
      if (this.avsDocTable == null || this.AvsDocumentForm == AVSDocumentForm.V && this.avsFormB_Table == null)
        this.FindMainTablesInDocument((ImDocumentData) this.Document, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
      if (this.AvsDocumentForm == AVSDocumentForm.V)
        this.variableDataChapter_FormV.AddDocNode(this.avsFormB_Table);
      else if (this.AvsDocumentForm != AVSDocumentForm.A)
      {
        this.commonDataChapter.AddDocNode(this.avsDocTable);
        this.commonDataChapter.AddDocNode(this.avsDocTableExpMix, true);
      }
      TableData tableData2 = this.avsDocTable;
      if (this.IsSpecification)
      {
        this.ScanSpecificationTable(this.avsDocTable, false, rowDicts, productsInDoc);
        if (this.IsExportSP && this.avsDocTableExpMix != null)
          this.ScanSpecificationTable(this.avsDocTableExpMix, true, rowDicts, productsInDoc);
      }
      else
      {
        if (this.AvsDocumentForm == AVSDocumentForm.Single)
          this.commonDataChapter.DocNode = this.avsDocTable;
        while (tableData2 != null)
        {
          DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(tableData2);
          bool flag = true;
          while (dataNodesEnumerator.MoveNext())
          {
            int currentCellIndex = dataNodesEnumerator.CurrentCellIndex;
            TableData parentCell = dataNodesEnumerator.Current.ParentCell;
            if (currentCellIndex != -1 && parentCell != null && currentCellIndex < parentCell.Nodes.Count && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) dataNodesEnumerator.Current))
            {
              TableData current1 = dataNodesEnumerator.Current as TableData;
              string attributeValue1 = current1.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
              ReferenceToDBObject refToChapter = this.GetRefToChapter(current1);
              if (refToChapter != null || attributeValue1 != null)
              {
                if (attributeValue1 == Chapter.AVSRow_TypeName)
                {
                  flag = false;
                  if (!(this.commonDataChapter is SpecificationSection))
                  {
                    Chapter commonDataChapter = this.commonDataChapter;
                    this.CommonDataChapter = (Chapter) new SpecificationSection(this, new SpecificationSectionInfo(AVSDocument.ChapterCommonDataGuid, -1L, -1, "Перечень элементов", 0L, "", new int[0], new long[0]));
                    this.commonDataChapter.DocNodes = commonDataChapter.DocNodes;
                    this.commonDataChapter.Product = commonDataChapter.Product;
                    this.commonDataChapter.caption = commonDataChapter.caption;
                    this.commonDataChapter.NodeLevel = commonDataChapter.NodeLevel;
                    this.commonDataChapter.ChapterType = commonDataChapter.ChapterType;
                  }
                  this.ScanChapterStructure(tableData2, false, rowDicts, (List<ProductInfo>) null);
                  break;
                }
                if (attributeValue1 == Chapter.Section_TypeName || refToChapter != null && refToChapter.DBObjectType == AvsIDCache.ObjType_SpecificationSection)
                {
                  flag = false;
                  if (this.AvsDocumentForm != AVSDocumentForm.A)
                  {
                    this.ScanChapterStructure(tableData2, false, rowDicts, (List<ProductInfo>) null);
                  }
                  else
                  {
                    if (this.commonDataChapter.DocNode == null)
                    {
                      if (!this.ReadOnly)
                      {
                        tableData1 = this.commonDataChapter.CreateDocNode(this.commonChapterTemplate);
                        this.commonDataChapter.DocNode = tableData1;
                        this.avsDocTable.InsertChildNode(currentCellIndex, (DocumentTreeNode) tableData1, false, true, false, false, false);
                        dataNodesEnumerator.SetCurrentCell((RectangleElement) tableData1, dataNodesEnumerator.DataIndex);
                      }
                    }
                    else
                      tableData1 = this.commonDataChapter.DocNode;
                    while (dataNodesEnumerator.MoveNext())
                    {
                      RectangleElement current2 = dataNodesEnumerator.Current;
                      if (current2 != null && tableData1 != null && !this.ReadOnly)
                      {
                        tableData1.AddChildNode((DocumentTreeNode) current2, true, true, false, false);
                        dataNodesEnumerator.SetCurrentCell((RectangleElement) tableData1, dataNodesEnumerator.DataIndex - 1);
                      }
                    }
                    this.ScanChapterStructure(tableData1, false, rowDicts, (List<ProductInfo>) null);
                  }
                }
                else if (attributeValue1 == Chapter.AdditionalChapter_TypeName || refToChapter != null && refToChapter.DBObjectType == AvsIDCache.ObjType_SpecificationChapter)
                {
                  AdditionalChapter additionalChapter = (AdditionalChapter) null;
                  if (refToChapter != null)
                  {
                    for (int index = 0; index < this.rootChapters.Count; ++index)
                    {
                      if (this.rootChapters[index].IsAdditionalChapter && this.rootChapters[index].ChapterGuid == refToChapter.DBObjectGuid)
                      {
                        additionalChapter = this.rootChapters[index] as AdditionalChapter;
                        break;
                      }
                    }
                  }
                  if (additionalChapter == null)
                  {
                    Guid chapterGuid = refToChapter != null ? refToChapter.DBObjectGuid : Guid.Empty;
                    string attributeValue2 = current1.GetAttributeValue(Chapter.CaptionFormat_AttributeName, true);
                    long num = (long) this.AddRootChapter((Chapter) (additionalChapter = new AdditionalChapter(this, new AdditionalChapterSettings(chapterGuid, -1L, attributeValue2, -1L), false)), true);
                    additionalChapter.SortIndex = num;
                  }
                  additionalChapter.AddDocNode(current1);
                  this.ScanChapterStructure(current1, false, rowDicts, (List<ProductInfo>) null);
                }
                else if (attributeValue1 == Chapter.CommonData_TypeName)
                {
                  this.commonDataChapter.AddDocNode(current1);
                  this.ScanChapterStructure(current1, false, rowDicts, (List<ProductInfo>) null);
                }
                else if (attributeValue1 == Chapter.VariableData_TypeName)
                {
                  if (this.AvsDocumentForm == AVSDocumentForm.A)
                    this.variableDataChapter_FormA.DocNode = current1;
                  this.ScanChapterStructure(current1, false, rowDicts, (List<ProductInfo>) null);
                }
                else
                {
                  flag = false;
                  if (this.AvsDocumentForm == AVSDocumentForm.A)
                  {
                    this.commonDataChapter.AddDocNode(current1);
                    this.ScanChapterStructure(current1, false, rowDicts, (List<ProductInfo>) null);
                    while (dataNodesEnumerator.MoveNext())
                    {
                      if (dataNodesEnumerator.Current is TableData current3 && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) current3))
                      {
                        this.variableDataChapter_FormA.DocNode = current3;
                        this.ScanChapterStructure(current3, false, rowDicts, (List<ProductInfo>) null);
                        break;
                      }
                    }
                  }
                  else
                  {
                    this.ScanChapterStructure(current1, false, rowDicts, (List<ProductInfo>) null);
                    if (!this.ReadOnly)
                    {
                      current1.UniteTable();
                      for (int index = current1.Nodes.Count - 1; index >= 0; --index)
                        current1.RemoveChildNodeAt(index, false, false, false);
                      dataNodesEnumerator.RemoveCurrentAndGotoPrev();
                    }
                  }
                }
              }
            }
          }
          if (flag && this.AvsDocumentForm != AVSDocumentForm.A && !(this.commonDataChapter is SpecificationSection))
          {
            Chapter commonDataChapter = this.commonDataChapter;
            this.CommonDataChapter = (Chapter) new SpecificationSection(this, new SpecificationSectionInfo(AVSDocument.ChapterCommonDataGuid, -1L, -1, "Перечень элементов", 0L, "", new int[0], new long[0]));
            this.commonDataChapter.DocNodes = commonDataChapter.DocNodes;
            this.commonDataChapter.caption = commonDataChapter.caption;
            this.commonDataChapter.Product = commonDataChapter.Product;
            this.commonDataChapter.NodeLevel = commonDataChapter.NodeLevel;
            this.commonDataChapter.ChapterType = commonDataChapter.ChapterType;
          }
          if (AVSDocument.IsDocumentFormB(this.AvsDocumentForm))
          {
            if (tableData2.Page != null)
            {
              PageData lastPage = tableData2.Page.FindLastPage();
              PageData nextPage = ImDocumentData.GetNextPage(lastPage.Parent, lastPage.Index, true);
              tableData2 = nextPage == null ? (TableData) null : this.FindMainDocTableFromPage(nextPage, false);
            }
            else
              tableData2 = (TableData) null;
          }
          else
            break;
        }
      }
      if (this.productsInfo.Count != 0 || this.IsSpecification)
        return;
      this.productsInfo = new List<ProductInfo>();
      this.productsInfo.Add(this.GetElementListInfo());
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, false, false, true, true);
    }
  }

  /// <summary>Сканировать заголовки исполнений на странице</summary>
  /// <param name="page">Страница</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void ScanProductHeadersOnPage(PageData page, List<ProductInfo> productsInDoc)
  {
    if (page == null || this.productsInfo == null || productsInDoc == null || !this.IsFormB && (this.AvsDocumentForm != AVSDocumentForm.V || !this.IsVariableDataFormVPage(page)))
      return;
    TableData productNumberTable = this.FindProductNumberTable(page);
    if (productNumberTable == null)
      return;
    for (int index1 = 0; index1 < productNumberTable.Nodes.Count; ++index1)
    {
      if (productNumberTable.Nodes[index1] is TextData node)
      {
        string templateId = node.TemplateId;
        if (templateId != null && templateId.Contains("Номер исполнения"))
        {
          string attributeValue = node.GetAttributeValue(AVSDocument.ProductGuid_CellAttribute, true);
          if (attributeValue != "")
          {
            Guid guid = new Guid(attributeValue);
            for (int index2 = 0; index2 < this.productsInfo.Count; ++index2)
            {
              if (this.productsInfo[index2].Guid == guid)
              {
                productsInDoc.Add(this.productsInfo[index2]);
                break;
              }
            }
          }
          else
          {
            string text = node.Text;
            if (text != "" && text != null)
            {
              int currentNumber = -1;
              for (int index3 = 0; index3 < this.productsInfo.Count; ++index3)
              {
                if (this.productsInfo[index3].GetNumber(currentNumber, out currentNumber, this.DocumentDesignation, this.UseSameDesignationForProducts) == text || this.productsInfo[index3].generatedNumber == text)
                {
                  productsInDoc.Add(this.productsInfo[index3]);
                  break;
                }
              }
            }
          }
        }
      }
    }
  }

  /// <summary>Найти в документе таблицу данных конструкторского документа</summary>
  /// <param name="document">Конструкторский документ</param>
  /// <param name="docTable">Основная таблица данных конструкторского документа</param>
  /// <param name="docTableFormV">Переменные данные формы В</param>
  /// <param name="docTableExpMix">Таблица экспортной СП (на совместном листе)</param>
  /// <param name="docTableExpSingle">Таблица экспортной СП (на отдельном листе)</param>
  /// <param name="docTableExpMixP1">Таблица экспортной СП. Продолжение 1 (на  совместном листе)</param>
  /// <param name="docTableExpSingleP2">Таблица экспортной СП. Продолжение 2 (на отдельном листе)</param>
  /// <param name="docTableSingleT1">Таблица СП (на отдельном листе)</param>
  /// <param name="docTableMixP1">Таблица СП. Продолжение 1 (на совместном листе)</param>
  /// <param name="docTableSingleP2">Таблица СП. Продолжение 2 (на отдельном листе)</param>
  /// <param name="lriPage">Лист регистрации изменений</param>
  public void FindMainTablesInDocument(
    ImDocumentData document,
    out TableData docTable,
    out TableData docTableFormV,
    out TableData docTableExpMix,
    out TableData docTableExpSingle,
    out TableData docTableExpMixP1,
    out TableData docTableExpSingleP2,
    out TableData docTableSingleT1,
    out TableData docTableSingleP2,
    out TableData docTableMixP1,
    out PageData lriPage)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    docTable = (TableData) null;
    docTableFormV = (TableData) null;
    docTableExpMix = (TableData) null;
    docTableExpSingle = (TableData) null;
    docTableExpMixP1 = (TableData) null;
    docTableExpSingleP2 = (TableData) null;
    docTableSingleT1 = (TableData) null;
    docTableSingleP2 = (TableData) null;
    docTableMixP1 = (TableData) null;
    lriPage = (PageData) null;
    if (this.IsSpecification)
    {
      if (!document.IsTemplate && this.avsDocTableTemplate != null)
        docTable = (TableData) document.FindNode(this.avsDocTableTemplate.Id);
      if (docTable == null)
        docTable = (TableData) document.FindNode("Таблица Спецификация");
      if (docTable == null)
        docTable = (TableData) document.FindNode("Главная таблица");
      docTableExpMix = (TableData) document.FindNode("EXP.MIX.T1");
      docTableExpSingle = (TableData) document.FindNode("EXP.T1");
      docTableExpMixP1 = (TableData) document.FindNode("EXP.MIX.Р1");
      docTableExpSingleP2 = (TableData) document.FindNode("EXP.P2");
      docTableSingleT1 = (TableData) document.FindNode("SP.T1");
      docTableSingleP2 = (TableData) document.FindNode("SP.P2");
      docTableMixP1 = (TableData) document.FindNode("SP.MIX.P1");
      if (!document.IsTemplate && this.avsDocTableFormBForV_Template != null)
        docTableFormV = (TableData) document.FindNode(this.avsDocTableFormBForV_Template.Id);
      if (docTableFormV == null && this.AvsDocumentForm != AVSDocumentForm.V)
      {
        docTableFormV = (TableData) document.FindNode("Таблица Спецификация. Продолжение 2");
        if (docTableFormV == null)
          docTableFormV = (TableData) document.FindNode("Главная таблица. Продолжение 2");
      }
      if (!document.IsTemplate && this.avsDocTableFormBMore10_Template != null)
      {
        if (docTable == null && this.AvsDocumentForm != AVSDocumentForm.V)
          docTable = (TableData) document.FindNode(this.avsDocTableFormBMore10_Template.Id);
        else if (docTableFormV == null && this.AvsDocumentForm == AVSDocumentForm.V)
          docTableFormV = (TableData) document.FindNode(this.avsDocTableFormBMore10_Template.Id);
      }
    }
    else
    {
      if (!document.IsTemplate && this.avsDocTableTemplate != null)
        docTable = (TableData) document.FindNode(this.avsDocTableTemplate.Id);
      if (docTable == null)
        docTable = (TableData) document.FindNode("Перечень элементов");
      if (docTable == null)
        docTable = (TableData) document.FindNode("Главная таблица");
      if (docTable == null)
        docTable = (TableData) document.FindNode("Таблица Спецификация");
      if (docTable == null)
        docTable = (TableData) document.FindNode("Рабочая область");
    }
    foreach (PageData page in document)
    {
      if (docTable != null && (docTableFormV != null || this.AvsDocumentForm != AVSDocumentForm.V))
      {
        if (docTableExpMix == null)
        {
          if (!this.IsExportSP)
            break;
        }
        else
          break;
      }
      bool varDataFormV;
      if (docTable == null || docTableFormV == null && this.AvsDocumentForm == AVSDocumentForm.V)
      {
        TableData docTableFromPage = this.FindMainDocTableFromPage(page, false, out varDataFormV);
        if (docTableFromPage != null)
        {
          if (!varDataFormV)
            docTable = docTableFromPage;
          else if (docTableFormV == null)
            docTableFormV = docTableFromPage;
        }
      }
      if (docTableExpMix == null && this.IsExportSP)
      {
        TableData docTableFromPage = this.FindMainDocTableFromPage(page, true, out varDataFormV);
        if (docTableFromPage != null)
          docTableExpMix = docTableFromPage;
      }
    }
    if (this.lriPage_Template == null)
      return;
    lriPage = (PageData) document.FindFirstNodeFromTemplate((DocumentTreeNode) this.lriPage_Template);
  }

  /// <summary>Найти на странице главную таблицу документа</summary>
  /// <param name="page">Страница</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  /// <returns>Главная таблица документа</returns>
  public TableData FindMainDocTableFromPage(PageData page, bool isExportTable)
  {
    return this.FindMainDocTableFromPage(page, isExportTable, out bool _);
  }

  /// <summary>Найти на странице главную таблицу документа</summary>
  /// <param name="page">Страница</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  /// <param name="varDataFormV">Таблица переменных данных формы В</param>
  /// <returns>Главная таблица документа</returns>
  public TableData FindMainDocTableFromPage(
    PageData page,
    bool isExportTable,
    out bool varDataFormV)
  {
    varDataFormV = false;
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (isExportTable)
    {
      if (this.avsDocTableExpMix != null && this.avsDocTableExpMix.Page == page)
        return this.avsDocTableExpMix;
    }
    else
    {
      if (this.avsDocTable != null && this.avsDocTable.Page == page)
        return this.avsDocTable;
      if (this.avsFormB_Table != null && this.avsFormB_Table.Page == page)
      {
        varDataFormV = true;
        return this.avsFormB_Table;
      }
    }
    if (page.Flows != null)
    {
      for (int index = 0; index < page.Flows.Count; ++index)
      {
        if (page.Flows[index] is TableData flow)
        {
          if (!page.IsTemplate)
          {
            if (isExportTable && this.IsExportSpecificationTable(flow) || this.IsSpecificationTable(flow, out varDataFormV))
              return flow;
          }
          else
            break;
        }
      }
    }
    TableData docTableFromPage = (TableData) null;
    if (!page.IsTemplate)
    {
      if (this.avsDocTableTemplate != null)
        docTableFromPage = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableTemplate) as TableData;
      if (docTableFromPage == null && this.avsDocTableFormBMore10_Template != null)
      {
        varDataFormV = this.AvsDocumentForm == AVSDocumentForm.V;
        docTableFromPage = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableFormBMore10_Template) as TableData;
      }
      if (docTableFromPage == null && this.avsDocTableFormBForV_Template != null)
      {
        varDataFormV = true;
        docTableFromPage = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableFormBForV_Template) as TableData;
      }
    }
    return docTableFromPage;
  }

  /// <summary>Найти на странице таблицу спецификации</summary>
  /// <param name="page">Страница</param>
  /// <returns>Таблица спецификации</returns>
  public bool IsFormBPage(PageData page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    TableData tableData = (TableData) null;
    if (this.AvsDocumentForm != AVSDocumentForm.V)
      return this.IsFormB;
    if (!page.IsTemplate)
    {
      if (this.avsDocTable != null && this.avsDocTable.Page == page || this.avsDocTableTemplate != null && this.avsDocTableTemplate.Page == page)
        return this.IsFormB;
      if (this.avsFormB_Table != null && this.avsFormB_Table.Page == page || this.avsFormB_Table != null && this.avsFormB_Table.Template != null && ((PageElementNode) this.avsFormB_Table.Template).Page == page)
        return true;
      if (page.Flows != null)
      {
        for (int index = 0; index < page.Flows.Count; ++index)
        {
          if (page.Flows[index] is TableData flow)
          {
            TableData firstTable = flow.FindFirstTable();
            if (page.Flows.Count == 1 || this.avsDocTable != null && firstTable == this.avsDocTable && this.IsFormB || this.avsFormB_Table != null && firstTable == this.avsFormB_Table || firstTable.Template != null && (firstTable.Template == this.avsDocTableTemplate && this.IsFormB || firstTable.Template == this.avsDocTableFormBForV_Template || firstTable.Template == this.avsDocTableFormBMore10_Template))
              return true;
          }
        }
      }
      tableData = (TableData) null;
      if (this.avsDocTableTemplate != null)
        tableData = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableTemplate) as TableData;
      if (tableData != null)
        return this.IsFormB;
      if (this.avsDocTableFormBMore10_Template != null)
        tableData = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableFormBMore10_Template) as TableData;
      if (tableData == null && this.avsDocTableFormBForV_Template != null)
        tableData = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) this.avsDocTableFormBForV_Template) as TableData;
    }
    else
    {
      if (this.avsDocTableTemplate?.Page != null && (this.avsDocTableTemplate.Page == page || this.avsDocTableTemplate.Page.NextPageTemplateId == page.Id))
        return false;
      if (this.avsDocTableFormBForV_Template != null && (this.avsDocTableFormBForV_Template.Page == page || this.avsDocTableFormBForV_Template.Page != null && this.avsDocTableFormBForV_Template.Page.NextPageTemplateId == page.Id) || this.titlePageFormBForV_Template == page || this.avsDocTableFormBMore10_Template != null && (this.avsDocTableFormBMore10_Template.Page == page || this.avsDocTableFormBMore10_Template.Page != null && this.avsDocTableFormBMore10_Template.Page.NextPageTemplateId == page.Id))
        return true;
    }
    return tableData != null;
  }

  /// <summary>Синхронизировать содержимое подраздела документа и структуру СП</summary>
  /// <param name="docChapter">Узел документа</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  public void SynchronizeChapter(TableData docChapter, bool isExportTable)
  {
    Chapter chapterForDocNode1 = this.GetChapterForDocNode(docChapter, this.AvsDocumentForm, true, isExportTable);
    if (chapterForDocNode1 == null)
      return;
    SpecificationSection specificationSection = chapterForDocNode1 as SpecificationSection;
    if (specificationSection == null && chapterForDocNode1 is AVSRowGroup avsRowGroup)
      specificationSection = avsRowGroup.Section;
    if (!this.IsSpecification && chapterForDocNode1 is ProductVariableDataChapter)
    {
      if (chapterForDocNode1.Chapters.Count <= 0)
        return;
      specificationSection = chapterForDocNode1.Chapters[0] as SpecificationSection;
    }
    if (specificationSection == null)
    {
      int dataPosition = 0;
      TableData dataOwner;
      for (int dataPositionInFlow = docChapter.FindDataPositionInFlow(dataPosition, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        if (dataOwner.Nodes[dataPositionInFlow] is TableData tableData1)
        {
          Chapter chapterForDocNode2 = this.GetChapterForDocNode(tableData1, this.AvsDocumentForm, true, isExportTable);
          if (chapterForDocNode2 != null)
          {
            int index = chapterForDocNode2.AddDocNode(tableData1, isExportTable);
            TableData tableData = (TableData) null;
            if (isExportTable)
            {
              if (chapterForDocNode1.DocNodeExp != null)
                tableData = chapterForDocNode1.DocNodeExp;
              else if (tableData1.ParentCell != null)
                tableData = AVSDocument.FindParentChapterDocNode((DocumentTreeNode) tableData1.ParentCell, true) as TableData;
            }
            else if (index < chapterForDocNode1.DocNodes.Count)
              tableData = chapterForDocNode1.DocNodes[index];
            else if (tableData1.ParentCell != null)
              tableData = AVSDocument.FindParentChapterDocNode((DocumentTreeNode) tableData1.ParentCell, true) as TableData;
            if (tableData != null && (tableData1.ParentCell == null || tableData1.ParentCell.FindFirstTable() != tableData))
            {
              tableData1.UniteTable();
              tableData1.Remove(false, false);
              --dataPositionInFlow;
              --dataPosition;
              tableData.AddChildNode((DocumentTreeNode) tableData1, true, true, false, false);
            }
          }
          else if (!AVSDocument.IsNoteRowDocNode((DocumentTreeNode) tableData1))
          {
            tableData1.UniteTable();
            tableData1.Remove(false, false);
            --dataPositionInFlow;
            --dataPosition;
          }
          else
            tableData1 = (TableData) null;
          if (tableData1 != null)
            this.SynchronizeChapter(tableData1, isExportTable);
        }
        ++dataPosition;
      }
    }
    else
    {
      int dataPosition = 0;
      TableData dataOwner;
      int dataPositionInFlow = docChapter.FindDataPositionInFlow(dataPosition, out dataOwner);
      for (; dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
        if (AVSDocument.IsGroupDocNode((DocumentTreeNode) node))
        {
          AVSRowGroup chapterForDocNode3 = this.GetChapterForDocNode(node, this.AvsDocumentForm, true, isExportTable) as AVSRowGroup;
          this.SynchronizeChapter(node, isExportTable);
          if (chapterForDocNode3.IsEmpty)
            specificationSection.RemoveGroup(chapterForDocNode3, true);
        }
        if (AVSDocument.IsSpecRowDocNode((DocumentTreeNode) node))
        {
          AVSRow row = this.GetAvsDocRow((DocumentTreeNode) node);
          if (row != null)
          {
            if (!row.HasRelation && AVSDocument.GetDocNodeRelationGuid((DocumentTreeNode) node) != Guid.Empty && row.Section != null)
            {
              row.Section.RemoveRow(row, true, true, true, true, false);
              --dataPositionInFlow;
              row = (AVSRow) null;
            }
            if (row != null)
            {
              int index = row.AddDocNode(node, isExportTable);
              if (AvsConfig.General.AutoSort && specificationSection != row.Section)
              {
                node.UniteTable();
                node.Remove(false, false);
                --dataPositionInFlow;
                --dataPosition;
                if (isExportTable)
                {
                  if (row.Section.DocNodesExp != null && index < row.Section.DocNodesExp.Count)
                    row.Section.DocNodesExp[index].AddChildNode((DocumentTreeNode) node, true, true, false, false);
                }
                else if (index < row.Section.DocNodes.Count)
                  row.Section.DocNodes[index].AddChildNode((DocumentTreeNode) node, true, true, false, false);
              }
              row.LoadDataFromDocRow(node, false, false, true);
            }
          }
          else
          {
            node.UniteTable();
            node.Remove(false, false);
            --dataPositionInFlow;
            --dataPosition;
          }
        }
        ++dataPosition;
      }
    }
  }

  /// <summary>Синхронизировать содержимое таблицы СП с загруженной структурой</summary>
  /// <param name="docTable">Таблица документа</param>
  /// <param name="isExportTable">Экспортная таблица документа</param>
  private void SynchronizeSpecification(TableData docTable, bool isExportTable)
  {
    AVSDocumentForm avsDocumentForm = this.AvsDocumentForm;
    TableData mainTable = docTable;
    while (mainTable != null)
    {
      DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(mainTable);
      while (dataNodesEnumerator.MoveNext())
      {
        TableData parentCell = dataNodesEnumerator.Current.ParentCell;
        if (dataNodesEnumerator.CurrentCellIndex != -1 && parentCell != null && dataNodesEnumerator.CurrentCellIndex < parentCell.Nodes.Count && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) dataNodesEnumerator.Current))
        {
          TableData current = dataNodesEnumerator.Current as TableData;
          string attributeValue = current.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
          if (this.GetRefToChapter(current) != null || attributeValue != null)
            this.SynchronizeChapter(current, isExportTable);
        }
      }
      if (!AVSDocument.IsDocumentFormB(avsDocumentForm) && avsDocumentForm != AVSDocumentForm.V || !AVSDocument.IsDocumentFormB(this.AvsDocumentForm) && this.AvsDocumentForm != AVSDocumentForm.V)
        break;
      if (mainTable.Page != null)
      {
        PageData lastPage = mainTable.Page.FindLastPage();
        PageData nextPage = ImDocumentData.GetNextPage(lastPage.Parent, lastPage.Index, true);
        mainTable = nextPage == null ? (TableData) null : this.FindMainDocTableFromPage(nextPage, isExportTable);
      }
      else
        mainTable = (TableData) null;
    }
  }

  /// <summary>Синхронизировать содержимое документа с загруженной структурой</summary>
  public void SynchronizeDocument()
  {
    if (this.ReadOnly)
      return;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      AVSDocumentForm avsDocumentForm = this.AvsDocumentForm;
      TableData tableData = this.avsDocTable;
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      if (this.avsDocTable == null || this.AvsDocumentForm == AVSDocumentForm.V && this.avsFormB_Table == null)
        this.FindMainTablesInDocument((ImDocumentData) this.Document, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
      switch (avsDocumentForm)
      {
        case AVSDocumentForm.A:
          if (this.IsSpecification)
          {
            this.SynchronizeSpecification(this.avsDocTable, false);
            if (!this.IsExportSP || this.avsDocTableExpMix == null)
              break;
            this.SynchronizeSpecification(this.avsDocTableExpMix, true);
            break;
          }
          if (avsDocumentForm == AVSDocumentForm.Single)
            this.commonDataChapter.DocNode = this.avsDocTable;
          while (tableData != null)
          {
            if (avsDocumentForm == AVSDocumentForm.Single)
            {
              this.SynchronizeChapter(tableData, false);
            }
            else
            {
              if (avsDocumentForm == AVSDocumentForm.Single)
                this.commonDataChapter.DocNode = this.avsDocTable;
              DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator(tableData);
              while (dataNodesEnumerator.MoveNext())
              {
                int currentCellIndex = dataNodesEnumerator.CurrentCellIndex;
                TableData parentCell = dataNodesEnumerator.Current.ParentCell;
                if (currentCellIndex != -1 && parentCell != null && currentCellIndex < parentCell.Nodes.Count && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) dataNodesEnumerator.Current))
                {
                  TableData current = dataNodesEnumerator.Current as TableData;
                  string attributeValue = current.GetAttributeValue(Chapter.DocNodeType_AttributeName, false);
                  if (this.GetRefToChapter(current) != null || attributeValue != null)
                    this.SynchronizeChapter(current, false);
                }
              }
            }
            if (!AVSDocument.IsDocumentFormB(avsDocumentForm))
              break;
            if (tableData.Page != null)
            {
              PageData lastPage = tableData.Page.FindLastPage();
              PageData nextPage = ImDocumentData.GetNextPage(lastPage.Parent, lastPage.Index, true);
              tableData = nextPage == null ? (TableData) null : this.FindMainDocTableFromPage(nextPage, false);
            }
            else
              tableData = (TableData) null;
          }
          break;
        case AVSDocumentForm.V:
          this.variableDataChapter_FormV.AddDocNode(this.avsFormB_Table);
          goto case AVSDocumentForm.A;
        default:
          this.commonDataChapter.AddDocNode(this.avsDocTable);
          goto case AVSDocumentForm.A;
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, false, false, true, true);
    }
  }

  /// <summary>Обработчик события удаления связи</summary>
  /// <param name="removedRelations">Список идентификаторов удалённых связей</param>
  public void RemoveRelation_NotificationHandler(IList<long> removedRelations)
  {
    if (this.avsWindow == null || removedRelations == null || removedRelations.Count == 0)
      return;
    if (this.avsWindow._relationIDsRemoved_FromNotificationService != removedRelations)
      this.avsWindow._relationIDsRemoved_FromNotificationService.AddRange((IEnumerable<long>) removedRelations);
    if (!this.avsWindow.Visible)
      return;
    bool flag = false;
    try
    {
      int viewMode = (int) this.ViewMode;
      for (int index1 = 0; index1 < this.avsWindow._relationIDsRemoved_FromNotificationService.Count; ++index1)
      {
        AVSRow avsDocRow = this.GetAvsDocRow(this.avsWindow._relationIDsRemoved_FromNotificationService[index1]);
        if (avsDocRow != null)
        {
          if (!flag)
          {
            this.SuspendDocumentAndGridUpdates();
            flag = true;
          }
          int num = -1;
          if (avsDocRow.HasRelation)
            num = avsDocRow.GetRelationIndex(avsDocRow.Relations, this.avsWindow._relationIDsRemoved_FromNotificationService[index1]);
          if (num != -1)
          {
            long projectId = avsDocRow.Relations[num].ProjectId;
            long valueInt64_1 = avsDocRow.Relations[num].GetValueInt64(AvsIDCache.Attr_DopZamenGroupNum, false);
            avsDocRow.RemoveRelationData(avsDocRow.Relations, num);
            if (avsDocRow.HasHiddenRelation && valueInt64_1 != -1L)
            {
              for (int index2 = avsDocRow.HiddenRelations.Count - 1; index2 >= 0; --index2)
              {
                long valueInt64_2 = avsDocRow.HiddenRelations[index2].GetValueInt64(AvsIDCache.Attr_DopZamenGroupNum, false);
                if (valueInt64_2 == -1L || valueInt64_2 == valueInt64_1 && projectId == avsDocRow.HiddenRelations[index2].ProjectId)
                {
                  avsDocRow.Relations.Add(avsDocRow.HiddenRelations[index2]);
                  avsDocRow.HiddenRelations.RemoveAt(index2);
                }
              }
            }
          }
          else if (avsDocRow.HasHiddenRelation)
          {
            int relationIndex = avsDocRow.GetRelationIndex(avsDocRow.HiddenRelations, this.avsWindow._relationIDsRemoved_FromNotificationService[index1]);
            if (relationIndex != -1)
              avsDocRow.RemoveRelationData(avsDocRow.HiddenRelations, relationIndex);
          }
          SpecificationSection section = avsDocRow.Section;
          if (avsDocRow.Relations.Count == 0 && section != null)
            section.RemoveRow(avsDocRow, true, false, true, this.IsGridViewMode, false);
        }
      }
      this.avsWindow._relationIDsRemoved_FromNotificationService.Clear();
    }
    finally
    {
      if (flag)
      {
        this.UpdateDocumentStructure(false, false, false);
        this.UpdateVariableDataCaptions();
        this.ResumeDocumentAndGridUpdates(0, true, true, true, true, true);
      }
    }
  }

  /// <summary>Обработчик события удаления связи</summary>
  /// <param name="removedObjects">Список идентификаторов удалённых связей</param>
  public void RemoveObject_NotificationHandler(IList<long> removedObjects)
  {
    if (this.avsWindow == null || removedObjects == null || removedObjects.Count == 0)
      return;
    if (this.avsWindow._objectIDsRemoved_FromNotificationService != removedObjects)
      this.avsWindow._objectIDsRemoved_FromNotificationService.AddRange((IEnumerable<long>) removedObjects);
    if (!this.avsWindow.Visible)
      return;
    bool flag = false;
    try
    {
      AVSViewMode viewMode = this.ViewMode;
      for (int index1 = 0; index1 < this.avsWindow._objectIDsRemoved_FromNotificationService.Count; ++index1)
      {
        List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(this.avsWindow._objectIDsRemoved_FromNotificationService[index1]);
        if (avsRowsByObjectId != null && avsRowsByObjectId.Count > 0)
        {
          if (!flag)
          {
            flag = true;
            this.SuspendDocumentAndGridUpdates();
          }
          for (int index2 = 0; index2 < avsRowsByObjectId.Count; ++index2)
            avsRowsByObjectId[index2].Section.RemoveRow(avsRowsByObjectId[index2], true, false, true, viewMode == AVSViewMode.Grid, false);
        }
      }
    }
    finally
    {
      if (flag)
      {
        this.avsWindow._objectIDsRemoved_FromNotificationService.Clear();
        this.UpdateDocumentStructure(false, false, false);
        this.UpdateVariableDataCaptions();
        this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
    }
  }

  /// <summary>Объект хранящий настройки графы "Примечание"</summary>
  internal virtual long NoteFieldSettingsObjectID => -1;

  internal virtual void LoadNoteFieldSettings()
  {
    this.noteFieldSettings = new NoteFieldSettings();
    if (this.NoteFieldSettingsObjectID.IsUndefinedId())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.noteFieldSettings.LoadFromDBObjectAttribute(this.NoteFieldSettingsObjectID, AvsIDCache.Attr_NoteFieldSettings, sessionKeeper.Session);
  }

  internal void LoadVersionAttributesHelper()
  {
    this.versionAttributesHelper = new VersionAttributesHelper();
    if (this.NoteFieldSettingsObjectID.IsUndefinedId())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.versionAttributesHelper.LoadVersionsAttributes(this.NoteFieldSettingsObjectID, sessionKeeper.Session);
  }

  /// <summary>Обновить текст в ячейках графы "Примечание"</summary>
  /// <param name="loadNewAttributes">Подгружать атрибуты, которых нет в кэше</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  public void UpdateNoteDocCells(bool loadNewAttributes, bool updateUI)
  {
    bool flag = false;
    this.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      if (loadNewAttributes)
      {
        List<AvsRowAttributeInfo> attrInfoList = new List<AvsRowAttributeInfo>();
        for (int index = 0; index < this.noteFieldSettings.Items.Count; ++index)
          attrInfoList.Add(this.noteFieldSettings.Items[index].CreateRowAttrInfo());
        attrInfoList.Add(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format));
        attrInfoList.Add(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Zone));
        flag = this.LoadNewAttributes(attrInfoList, true);
      }
      if (flag)
        return;
      List<AVSRow> allRows = this.GetAllRows(false, false);
      for (int index = 0; index < allRows.Count; ++index)
        allRows[index].UpdateNoteDocCellText();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, updateUI, updateUI, true, false);
    }
  }

  /// <summary>Обновить текст в ячейках графы "Наименование"</summary>
  /// <param name="loadNewAttributes">Подгружать атрибуты, которых нет в кэше</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  public void UpdateNameDocCells(List<AvsRowAttributeInfo> newNameAttrs, bool updateUI)
  {
    this.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      if (newNameAttrs.Count > 0)
      {
        this.LoadNewAttributes(newNameAttrs, true);
      }
      else
      {
        List<AVSRow> allRows = this.GetAllRows(false, false);
        for (int index = 0; index < allRows.Count; ++index)
          allRows[index].UpdateNameDocCellText(false, false);
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, updateUI, updateUI, true, false);
    }
  }

  /// <summary>Обновляем данные при приходе сообщений от NotificationService</summary>
  /// <param name="e"></param>
  internal void UpdateNotificationObjectsData(NotificationEventArgs e)
  {
    if (e is DBObjectsExtendedEventArgs changedObjectEventArgs && (MetaDataHelper.IsObjectTypeChildOf(changedObjectEventArgs.ObjectType, AvsIDCache.ObjType_ConstructorDocumentTemplate) || changedObjectEventArgs.ObjectType == AvsIDCache.ObjType_Specification && changedObjectEventArgs.ObjectIDs.Contains(this.DocumentID)))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        SettingsStructure settingsStructure = this.DocumentSettingsStructure;
        AttributeValues[] attributeValuesArray = changedObjectEventArgs.AttributeValuesArray;
        if (attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_DesignationTrimSchema)))
          this.designationTrimSchema = settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_DesignationTrimSchema, typeof (DesignationTrimSchema)) as DesignationTrimSchema;
        if (attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_MaterialKeyWordsSchema)))
        {
          this.MaterialKeyWordsSchema = settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_MaterialKeyWordsSchema, typeof (KeyWordsSchema)) as KeyWordsSchema;
          this.Document?.SetMaterialKeyWords((List<string>) this.MaterialKeyWordsSchema?.KeyWords);
        }
        if (attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_SkipLines)))
        {
          this.skipLinesSchema = (SkipLinesSchema) null;
          this.UpdateSkipLines(true, true);
        }
        if ((attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_OutputMappingSchema))) && !this.ReadOnly)
        {
          this.cellTextOutputAttributeMappingSettings = (OutputAttributeMappingScheme) null;
          this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
        }
        if (attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_SortSchema)))
          this.ResortSpecification(true, true);
        bool flag = false;
        if (attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_ConstructorDocumentProperties)))
        {
          int num1 = this.AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocuments ? 1 : 0;
          int valueModeForNote1 = (int) this.AVSCommonPropertiesSchema.LimitAndNominalValueModeForNote;
          this.avsCommonPropertiesSchema = this.LoadAVSCommonPropertiesSchema();
          int valueModeForNote2 = (int) this.AVSCommonPropertiesSchema.LimitAndNominalValueModeForNote;
          flag = valueModeForNote1 != valueModeForNote2;
          int num2 = this.AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocuments ? 1 : 0;
          this.UpdateUserAttributesForFieldName(num1 != num2);
        }
        if ((attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_NoteFieldSettings))) && changedObjectEventArgs.ObjectIDs.Contains(this.NoteFieldSettingsObjectID))
        {
          this.LoadNoteFieldSettings();
          flag = true;
        }
        if ((attributeValuesArray == null || Array.Exists<AttributeValues>(attributeValuesArray, (Predicate<AttributeValues>) (el => el.AttributeID == AvsIDCache.Attr_VariableDataProductCaption))) && changedObjectEventArgs.ObjectIDs.Contains(this.NoteFieldSettingsObjectID))
          this.LoadVersionAttributesHelper();
        if (flag)
          this.UpdateNoteDocCells(true, true);
      }
    }
    if (changedObjectEventArgs != null)
    {
      this.AVSCommonPropertiesSchema.UpdateAdditionalChaptersCache(changedObjectEventArgs);
    }
    else
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs))
        return;
      if (e.EventName == "ObjectsCreated")
      {
        this.AVSCommonPropertiesSchema.LoadNewAdditionalChapters(objectsEventArgs);
      }
      else
      {
        if (!(e.EventName == "ObjectsRemoved"))
          return;
        this.AVSCommonPropertiesSchema.RemoveAdditionalChapterObject_NotificationHandler(objectsEventArgs);
      }
    }
  }

  /// <summary>Получить удельную массу для записи</summary>
  /// <param name="specRow">Запись</param>
  /// <param name="unitWeight">Возвращает удельную массу</param>
  /// <param name="errorMessages">Сообщения об ошибках</param>
  /// <returns>true, если значение получено</returns>
  protected bool GetUnitMass(
    AVSRow specRow,
    out double unitMass,
    List<SpecRowCheckMessage> errorMessages)
  {
    if (specRow == null)
      throw new ArgumentNullException(nameof (specRow));
    unitMass = 0.0;
    object fieldValue = specRow.GetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_UnitWeight), 0, -1, true, false);
    if (fieldValue == null)
    {
      if (specRow.HasObject)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = sessionKeeper.Session.GetObjectActualCopy(specRow.ObjectId, true).GetAttributeByID(MetaDataHelper.GetAttributeTypeID(new Guid("{cae0c0d5-517c-4e99-b36f-a08b3de4b329}")));
          if (attributeById != null)
            fieldValue = attributeById.Value;
        }
      }
      if (fieldValue == null)
      {
        errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как нет значения у атрибута \"Удельная масса\""));
        return false;
      }
    }
    if (fieldValue is MeasuredValue measuredValue)
    {
      MeasuredValue mValue = MeasureHelper.ConvertToBaseMeasure(measuredValue);
      if (mValue.MeasureID == AVSDocument.GramsPerMeterID)
        mValue = MeasureHelper.ConvertToMeasuredValue(mValue, AVSDocument.KilogramsPerMeterID);
      else if (mValue.MeasureID == AVSDocument.GramsPerSquareMeterID)
        mValue = MeasureHelper.ConvertToMeasuredValue(mValue, AVSDocument.KilogramsPerSquareMeterID);
      unitMass = mValue.Value;
    }
    return true;
  }

  /// <summary> Обновить массу специфицируемого изделия </summary>
  public void UpdateMass(
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    AvsRowAttributeInfo attrInfo1 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Weight);
    AvsRowAttributeInfo rowAttributeInfo = new AvsRowAttributeInfo(false, AvsIDCache.Attr_UnitWeight);
    AvsRowAttributeInfo attrInfo2 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Size);
    AvsRowAttributeInfo attrInfo3 = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum);
    AvsRowAttributeInfo attrInfo4 = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenNumInGroup);
    AvsRowAttributeInfo attrInfo5 = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DesignerActualVariant);
    this.LoadNewAttributes(this.CreateAttributeListForMassaCalc(), true);
    int relationDocument = AvsIDCache.Relation_Document;
    double[] numArray = new double[this.productsInfo.Count];
    List<AVSRow> allRows = this.GetAllRows(true, true);
    Dictionary<long, double>[] dictionaryArray1 = new Dictionary<long, double>[this.productsInfo.Count];
    Dictionary<long, double>[] dictionaryArray2 = new Dictionary<long, double>[this.productsInfo.Count];
    for (int index1 = 0; index1 < allRows.Count; ++index1)
    {
      if (allRows[index1].RelType != relationDocument)
      {
        List<SpecRowCheckMessage> errorMessages;
        bool flag1 = !errorRows.TryGetValue(allRows[index1], out errorMessages);
        if (flag1)
          errorMessages = new List<SpecRowCheckMessage>();
        if (!allRows[index1].HasRelation)
        {
          if (!allRows[index1].IsNoteRow)
            errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как не задано \"Количество\""));
          if (flag1 && errorMessages.Count > 0)
            errorRows.Add(allRows[index1], errorMessages);
        }
        else
        {
          string fieldStringValue1 = allRows[index1].GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
          if ((fieldStringValue1 == null || fieldStringValue1 == "") && !MetaDataHelper.IsObjectTypeChildOf(this.ProductType, AvsIDCache.ObjType_Complect))
          {
            errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Запись не участвует в расчёте общей массы, так как не задана \"Позиция\""));
            if (flag1)
              errorRows.Add(allRows[index1], errorMessages);
          }
          else if (!(fieldStringValue1 == "-"))
          {
            double result = 0.0;
            double num1 = 0.0;
            bool flag2 = false;
            double unitMass = 0.0;
            bool flag3 = false;
            bool flag4 = true;
            mValue = (MeasuredValue) null;
            MeasureDescriptor countMeasureDescriptor = (MeasureDescriptor) null;
            double num2 = 0.0;
            for (int index2 = 0; index2 < this.productsInfo.Count; ++index2)
            {
              int relationIndexForProduct = allRows[index1].GetRelationIndexForProduct(this.productsInfo[index2].Id);
              if (relationIndexForProduct != -1)
              {
                long key = -1;
                long num3 = -1;
                long num4 = -1;
                object fieldValue1 = allRows[index1].GetFieldValue(attrInfo3, relationIndexForProduct, index2, true, false);
                if (fieldValue1 != null)
                {
                  key = Convert.ToInt64(fieldValue1);
                  object fieldValue2 = allRows[index1].GetFieldValue(attrInfo5, relationIndexForProduct, index2, true, false);
                  if (fieldValue2 != null)
                    num3 = Convert.ToInt64(fieldValue2);
                  object fieldValue3 = allRows[index1].GetFieldValue(attrInfo4, relationIndexForProduct, index2, true, false);
                  if (fieldValue3 != null)
                    num4 = Convert.ToInt64(fieldValue3);
                  if (num4 != -1L && num3 != 1L && num4 != 0L)
                    continue;
                }
                bool flag5 = false;
                double num5 = 0.0;
                if (flag4 || allRows[index1].IsFormB)
                {
                  flag4 = false;
                  object fieldValue4 = allRows[index1].GetFieldValue(this.Field_Count, relationIndexForProduct, index2, false, false);
                  mValue = (MeasuredValue) null;
                  switch (fieldValue4)
                  {
                    case null:
                    case DBNull _:
                      errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как не задано \"Количество\""));
                      continue;
                    case MeasuredValue mValue:
label_24:
                      num2 = mValue == null ? 0.0 : mValue.Value;
                      countMeasureDescriptor = MeasureHelper.FindDescriptor(mValue);
                      break;
                    default:
                      mValue = AVSRow.ConvertCountToMeasuredValue(fieldValue4);
                      goto label_24;
                  }
                }
                if (mValue != null && mValue.MeasureID != AVSRow.DefaultCountID && countMeasureDescriptor != null)
                {
                  if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectMassGuid)
                  {
                    num5 = MeasureHelper.ConvertToMeasuredValue(mValue, AVSDocument.KilogramsID).Value;
                    flag5 = true;
                    num2 = 1.0;
                  }
                  else if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectLengthGuid || countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectSquareGuid || countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectVolumeGuid)
                  {
                    if (!flag3)
                    {
                      flag3 = true;
                      if (!this.GetUnitMass(allRows[index1], out unitMass, errorMessages))
                      {
                        unitMass = 0.0;
                        continue;
                      }
                    }
                    else if (unitMass == 0.0)
                      continue;
                    flag5 = true;
                    long baseSiMeasureId = AVSDocument.GetBaseSIMeasureID(countMeasureDescriptor);
                    num5 = MeasureHelper.ConvertToMeasuredValue(mValue, baseSiMeasureId).Value * unitMass;
                    num2 = 1.0;
                  }
                  else if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
                    num2 = MeasureHelper.ConvertToMeasuredValue(mValue, AVSRow.DefaultCountID).Value;
                }
                if (!flag5)
                {
                  if (!flag2)
                  {
                    flag2 = true;
                    num1 = 0.0;
                    object fieldValue5 = allRows[index1].GetFieldValue(attrInfo1, 0, -1, true, false);
                    if (fieldValue5 == null)
                    {
                      string fieldStringValue2 = allRows[index1].GetFieldStringValue(attrInfo2, 0, -1, (List<RelationAttributeValuesCache>) null, false);
                      if (fieldStringValue2 == null || fieldStringValue2 == "")
                      {
                        errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как нет значения у атрибута \"Масса\" и не заданы размеры для расчёта массы изделия"));
                        continue;
                      }
                      result = 0.0;
                      if (!this.GetSizeKoef(fieldStringValue2, out result))
                        errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как неправильно задано значение атрибута \"Размеры\""));
                      if (!flag3)
                      {
                        flag3 = true;
                        if (!this.GetUnitMass(allRows[index1], out unitMass, errorMessages))
                        {
                          unitMass = 0.0;
                          continue;
                        }
                      }
                      else if (unitMass == 0.0)
                        continue;
                      num1 = result * unitMass;
                    }
                    else
                    {
                      if (!(fieldValue5 is MeasuredValue mValue))
                        mValue = MeasureHelper.ConvertToMeasuredValue(fieldValue5.ToString(), "", false);
                      if (mValue == null)
                      {
                        errorMessages.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как некорректно задано значение атрибута \"Масса\""));
                        continue;
                      }
                      num1 = MeasureHelper.ConvertToMeasuredValue(mValue, AVSDocument.KilogramsID).Value;
                    }
                  }
                  num5 = num1;
                }
                if (num3 == 1L && key != -1L)
                {
                  if (dictionaryArray2[index2] == null)
                    dictionaryArray2[index2] = new Dictionary<long, double>();
                  double num6;
                  if (!dictionaryArray2[index2].TryGetValue(key, out num6))
                    dictionaryArray2[index2].Add(key, num5 * num2);
                  else
                    dictionaryArray2[index2][key] = num6 + num5 * num2;
                  if (dictionaryArray1[index2] != null && dictionaryArray2[index2].ContainsKey(key))
                    dictionaryArray1[index2].Remove(key);
                }
                else if (num4 == 0L && key != -1L)
                {
                  if (dictionaryArray2[index2] == null || !dictionaryArray2[index2].ContainsKey(key))
                  {
                    if (dictionaryArray1[index2] == null)
                      dictionaryArray1[index2] = new Dictionary<long, double>();
                    double num7;
                    if (!dictionaryArray1[index2].TryGetValue(key, out num7))
                      dictionaryArray1[index2].Add(key, num5 * num2);
                    else
                      dictionaryArray1[index2][key] = num7 + num5 * num2;
                  }
                }
                else
                  numArray[index2] += num5 * num2;
              }
            }
            if (flag1 && errorMessages.Count > 0)
              errorRows.Add(allRows[index1], errorMessages);
          }
        }
      }
    }
    for (int index = 0; index < dictionaryArray2.Length; ++index)
    {
      if (dictionaryArray2[index] != null)
      {
        foreach (double num in dictionaryArray2[index].Values)
          numArray[index] += num;
      }
      if (dictionaryArray1[index] != null)
      {
        foreach (KeyValuePair<long, double> keyValuePair in dictionaryArray1[index])
        {
          if (dictionaryArray2[index] == null || !dictionaryArray2[index].ContainsKey(keyValuePair.Key))
            numArray[index] += keyValuePair.Value;
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.productsInfo.Count; ++index)
        sessionKeeper.Session.GetObject(this.productsInfo[index].Id)?.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_Weight, (object) $"{numArray[index].ToString()} {AVSRow.DefaultMU_Mass_str}")
        });
    }
    if (AVSPlugin.NotificationService == null)
      return;
    AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) AVSDocument.GetProductIds(this.productsInfo)));
  }

  private static long GetBaseSIMeasureID(MeasureDescriptor countMeasureDescriptor)
  {
    long baseSiMeasureId = -1;
    if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectLengthGuid)
      baseSiMeasureId = AVSDocument.MeterID;
    else if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectSquareGuid)
      baseSiMeasureId = AVSDocument.SquareMeterID;
    else if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectVolumeGuid)
      baseSiMeasureId = AVSDocument.CubicMeterID;
    else if (countMeasureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectMassGuid)
      baseSiMeasureId = AVSDocument.KilogramsID;
    return baseSiMeasureId;
  }

  /// <summary>Перекодировать строку, в коэффициент, который требуется умножить на удельную массу для получения массы детали</summary>
  protected bool GetSizeKoef(string sizeStr, out double result)
  {
    bool sizeKoef = false;
    result = 0.0;
    sizeStr = sizeStr.ToUpper().Trim();
    if (sizeStr != "")
    {
      AVSDocument.SizeType sizeType = AVSDocument.SizeType.Unknown;
      string str = sizeStr;
      for (int index = 0; index < AVSDocument._sizeTypeEncoding.Length; ++index)
      {
        int num1 = sizeStr.IndexOf(AVSDocument._sizeTypeEncoding[index]);
        if (num1 != -1 && num1 < sizeStr.Length - 1)
        {
          str = sizeStr.Substring(num1 + 1).Trim();
          int num2 = str.IndexOf("=");
          if (num2 != -1)
            str = num2 >= str.Length - 1 ? "" : str.Substring(num2 + 1).Trim();
          sizeType = (AVSDocument.SizeType) index;
          break;
        }
      }
      if (str != "")
      {
        string[] strArray = str.Split('X', 'x', 'Х', 'х', '*');
        if (strArray.Length == 0)
          strArray = new string[1]{ str };
        string textBeforeNumber1;
        string textAfterNumber1;
        switch (sizeType)
        {
          case AVSDocument.SizeType.Length:
            sizeKoef = NumberParserAdvanced.ParseNumber(strArray[0], true, out result, out textBeforeNumber1, out textAfterNumber1);
            break;
          case AVSDocument.SizeType.Area:
            string textBeforeNumber2;
            string textAfterNumber2;
            sizeKoef = NumberParserAdvanced.ParseNumber(strArray[0], true, out result, out textBeforeNumber2, out textAfterNumber2);
            if (sizeKoef && strArray.Length > 1)
            {
              double number;
              sizeKoef = NumberParserAdvanced.ParseNumber(strArray[1], true, out number, out textBeforeNumber2, out textAfterNumber2);
              result *= number;
              break;
            }
            result = 0.0;
            break;
          case AVSDocument.SizeType.Volume:
          case AVSDocument.SizeType.Unknown:
            result = 1.0;
            sizeKoef = true;
            for (int index = 0; sizeKoef && index < strArray.Length && index < 3; ++index)
            {
              double number;
              sizeKoef = NumberParserAdvanced.ParseNumber(strArray[index], true, out number, out textBeforeNumber1, out textAfterNumber1);
              result *= number;
            }
            break;
        }
      }
    }
    return sizeKoef;
  }

  /// <summary>Создать связь для исполнения с блокировкой автоматического создания связей с документом для всех исполнений</summary>
  /// <param name="relCollection">Коллекция связей</param>
  /// <param name="projID">Идентификатор версии исполнения</param>
  /// <param name="partObjectID">Идентификатор версии вставляемого объекта</param>
  /// <param name="partID">Идентификатор вставляемого объекта. Если -1, то получается через partObjectID</param>
  /// <returns>Возвращает созданную связь</returns>
  public static IDBRelation CreateDocRelationWithLockPDMHandler(
    IDBRelationCollection relCollection,
    long projID,
    long partObjectID,
    long partID)
  {
    NewRelationProperties relationProperties = new NewRelationProperties(0L, projID, partID, DateTime.MinValue, DateTime.MaxValue, partObjectID);
    return AVSDocument.CreateDocRelationWithLockPDMHandler(relCollection, relationProperties);
  }

  /// <summary>Создать связь для исполнения с блокировкой автоматического создания связей с документом для всех исполнений</summary>
  /// <param name="relCollection">Коллекция связей</param>
  /// <param name="relationProperties">Структура с параметрами для создания новой связи</param>
  /// <param name="valuesList">Список значений атрибутов, которые нужно присвоить создаваемой связи. Может быть null</param>
  /// <returns>Возвращает созданную связь</returns>
  public static IDBRelation CreateDocRelationWithLockPDMHandler(
    IDBRelationCollection relCollection,
    NewRelationProperties relationProperties)
  {
    IPdmServerPlugin customService = relCollection.Session.GetCustomService(typeof (IPdmServerPlugin)) as IPdmServerPlugin;
    try
    {
      if (relationProperties.PartID == -1L)
        relationProperties.PartID = relCollection.Session.GetObjectInfo(relationProperties.PartObjectID).ID;
      customService?.LockAutoCreateRelationForArticle(relationProperties.ProjectObjectID, relationProperties.PartID);
      return relCollection.Create(relationProperties);
    }
    finally
    {
      customService?.UnlockAutoCreateRelationForArticle(relationProperties.ProjectObjectID, relationProperties.PartID);
    }
  }

  /// <summary>Спецификация нуждается в обновлении</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="documentId">Возвращает идентификатор версии документа</param>
  /// <param name="reasonList">Возвращает сообщения о причинах необходимости обновления</param>
  /// <returns></returns>
  public static bool SpecificationIsNeedUpdate(
    long objectID,
    int objectType,
    out long documentId,
    out List<string> reasonList)
  {
    if (!ImDocumentData.ShowDebugInfo && !DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, objectID, out string _).Item1)
    {
      reasonList = new List<string>();
      documentId = objectID;
      return false;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return AvsIDCache.SpecificationIsNeedUpdate(sessionKeeper.Session, objectID, objectType, out documentId, out reasonList);
  }

  private ProductInfo GetElementListInfo()
  {
    ProductInfo elementListInfo = (ProductInfo) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      elementListInfo = new ProductInfo(Guid.Empty, -1L, this.DocumentName);
      IDBObject productObj = sessionKeeper.Session.GetObject(this.DocumentID);
      if (productObj != null)
      {
        elementListInfo.UpdateInfo(productObj, (List<int>) null, this.DocumentDesignationSuffix);
        elementListInfo.Name = this.DocumentName;
      }
    }
    elementListInfo.Guid = Guid.NewGuid();
    elementListInfo.Id = -1L;
    if (this.AvsDocumentForm == AVSDocumentForm.A)
      elementListInfo.Designation = this.BaseProductDesignation;
    return elementListInfo;
  }

  /// <summary>Проверить допускается ли включать эту связь в документе</summary>
  /// <param name="relation">Связь</param>
  /// <returns></returns>
  protected virtual bool AllowIncludeRelationInDocument(RelationAttributeValuesCache relation)
  {
    return true;
  }

  /// <summary>Атрибут который отображается в графе "Количество"</summary>
  /// <param name="attribute">Информация об атрибуте</param>
  /// <returns></returns>
  internal virtual bool IsNoteField(AvsRowAttributeInfo attribute)
  {
    return attribute != null && attribute.Equals((AttributeInfo) this.Field_Note);
  }

  /// <summary>Режим вывода Предельных значений и Значений номинала в графе Примечание</summary>
  [Browsable(false)]
  internal LimitAndNominalValueMode LimitAndNominalValueModeForNote
  {
    get => this.AVSCommonPropertiesSchema.LimitAndNominalValueModeForNote;
  }

  /// <summary>Загрузить схему нумерации позиций в спецификации</summary>
  public void LoadSpecificationNumberingSchema()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._SpecifNumberingFull = (SpecifNumberingFull) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_NumberingSchema, typeof (SpecifNumberingFull));
  }

  /// <summary>Перенумеровать позиции в спецификации</summary>
  public void RenumberPositions()
  {
    this.EnterPendingRelationUpdateMode();
    try
    {
      this.RenumberPositions(true);
    }
    finally
    {
      this.ExitPendingRelationUpdateMode();
    }
  }

  /// <summary>Перенумеровать позиции в спецификации</summary>
  /// <param name="reloadSettingsFromDB">Перечитать настройки из базы</param>
  public virtual void RenumberPositions(bool reloadSettingsFromDB)
  {
    if (reloadSettingsFromDB)
      this.LoadSpecificationNumberingSchema();
    if (this.avsWindow != null)
      this.avsWindow.ErrorsUserControl.Clear();
    if (this.AutoSort)
      this.ResortSpecification(true, true);
    NumerationHelper numerationHelper = new NumerationHelper(this._SpecifNumberingFull);
    List<AVSRow> allRows = this.GetAllRows(false, true);
    numerationHelper.ExistNumbers.Clear();
    foreach (AVSRow avsRow in allRows)
    {
      if ((!this.IsSpecification || !avsRow.IsHiddenRow || !avsRow.IsDocRelation && !avsRow.IsDocObject) && !numerationHelper.ExistNumbers.Contains(avsRow.Position))
        numerationHelper.ExistNumbers.Add(avsRow.Position);
    }
    this.SuspendDocumentAndGridUpdates();
    ++this.suspendReloadDopZamenText;
    try
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
        this.rootChapters[index].RenumberPositions(numerationHelper);
    }
    finally
    {
      --this.suspendReloadDopZamenText;
      this.ReloadDopzamenTextForGroup((List<long>) null, true);
      this.ResumeDocumentAndGridUpdates(0, !this.AutoSort, !this.AutoSort, true, true, !this.AutoSort);
    }
    if (!this.AutoSort)
      return;
    this.ResortSpecification(true, true);
  }

  /// <summary>Очищает позиции</summary>
  public void ClearNumberPositions()
  {
    AvsRowAttributeInfo fieldPosition = this.Field_Position;
    AvsRowAttributeInfo rowAttributeInfo = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum);
    bool updateListNode = this.ViewMode == AVSViewMode.Grid;
    this.SuspendDocumentAndGridUpdates();
    ++this.suspendReloadDopZamenText;
    this.EnterPendingRelationUpdateMode();
    try
    {
      List<AVSRow> allRows = this.GetAllRows(false, false);
      for (int index = 0; index < allRows.Count; ++index)
      {
        string fieldStringValue = allRows[index].GetFieldStringValue(fieldPosition, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        if (fieldStringValue != "-" && fieldStringValue != null)
          allRows[index].SetFieldValue(fieldPosition, -1, -1, (object) null, true, false, true, updateListNode, false, false);
      }
    }
    finally
    {
      this.ExitPendingRelationUpdateMode();
      --this.suspendReloadDopZamenText;
      this.ReloadDopzamenTextForGroup((List<long>) null, true);
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  /// <summary>Автоматическая сортировка записей</summary>
  [Browsable(false)]
  public bool AutoSort
  {
    [DebuggerStepThrough] get => AvsConfig.General.AutoSort;
  }

  /// <summary>Загрузить схему сортировки спецификации</summary>
  public void LoadSpecificationSortSchema()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.sortSchema = this.DocumentID.IsUndefinedId() ? (SortSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.AVSDocumentTemplateID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_SortSchema, typeof (SortSchema)) : (SortSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_SortSchema, typeof (SortSchema));
    if (this.SortSchema == null)
      return;
    for (int index = 0; index < this.rootChapters.Count; ++index)
      this.rootChapters[index].UpdateSortSchema(this.SortSchema);
  }

  /// <summary>Перезагрузить схему сортировки</summary>
  public virtual void ReloadSortSchema()
  {
    this.LoadSpecificationSortSchema();
    this.LoadNewSortAttributes();
    this.ResortSpecification(true, true);
  }

  /// <summary>Установить признак сортировки всем записям в разделах</summary>
  /// <param name="value">Значение</param>
  private void MarkAllSectionSpecRowsAsSorted(bool value)
  {
    List<AVSRow> allRows = this.GetAllRows(false, false);
    for (int index = 0; index < allRows.Count; ++index)
      allRows[index].IsSorted = value;
  }

  /// <summary>Установить признак сортировки всем записям в разделах</summary>
  /// <param name="chapters">Разделы</param>
  /// <param name="value">Значение</param>
  private void MarkAllSectionSpecRowsAsSorted(List<Chapter> chapters, bool value)
  {
    List<AVSRow> rowList = new List<AVSRow>();
    for (int index1 = 0; index1 < chapters.Count; ++index1)
    {
      chapters[index1].GetAllRowsList(false, false, rowList);
      for (int index2 = 0; index2 < rowList.Count; ++index2)
        rowList[index2].IsSorted = value;
      rowList.Clear();
    }
  }

  /// <summary>Сортировать спецификацию. Только сортировка. Не делает обновления документа и табличного вида</summary>
  public void SortDocument()
  {
    LogManager.AddLine("AVS. Start sorting document");
    for (int index = 0; index < this.rootChapters.Count; ++index)
      this.rootChapters[index].Sort();
    LogManager.AddLine("AVS. End sorting document");
  }

  /// <summary>Сортировать спецификацию. Только сортировка. Не делает обновления документа и табличного вида</summary>
  public void SortNewRows()
  {
    foreach (SpecificationSection specificationSection in this.GetChaptersEnumerator().OfType<SpecificationSection>())
    {
      if (specificationSection.Rows.Count != 0)
      {
        List<AVSRow> collection = new List<AVSRow>(specificationSection.Rows.Count);
        for (int index = specificationSection.Rows.Count - 1; index >= 0; --index)
        {
          if (specificationSection.Rows[index].IsFreeSortIndex && !specificationSection.Rows[index].HasDocNodes)
          {
            collection.Add(specificationSection.Rows[index]);
            specificationSection.Rows.RemoveAt(index);
          }
        }
        if (specificationSection.Rows.Count == 0)
        {
          specificationSection.Rows.AddRange((IEnumerable<AVSRow>) collection);
          specificationSection.Sort();
        }
        else if (collection.Count > 0)
        {
          foreach (AVSRow row in collection)
            specificationSection.AddRow(row, true);
        }
      }
    }
  }

  /// <summary>Пересортировать спецификацию. С загрузкой настроек и обновлением документа и табличного вида</summary>
  /// <param name="sortSections">Сортировать сами разделы</param>
  /// <param name="sortRows">Сортировать записи в разделах</param>
  public void ResortSpecificationSection(SpecificationSection section)
  {
    if (section == null)
      return;
    this.LoadSpecificationSortSchema();
    this.LoadNewSortAttributes();
    this.SuspendDocumentAndGridUpdates();
    int fromPage = -1;
    try
    {
      this.UpdateDocumentStructure(true, true, false);
      this.UpdateVariableDataCaptions();
      section.Sort();
      this.IndexAVSDocument(false);
      this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(fromPage, true, true, true, true);
    }
  }

  /// <summary>Пересортировать спецификацию. С загрузкой настроек и обновлением документа и табличного вида</summary>
  /// <param name="sortSections">Сортировать сами разделы</param>
  /// <param name="sortRows">Сортировать записи в разделах</param>
  public void ResortSpecification(bool sortSections, bool sortRows)
  {
    this.LoadSpecificationSortSchema();
    this.LoadNewSortAttributes();
    this.SuspendDocumentAndGridUpdates();
    int fromPage = -1;
    try
    {
      this.UpdateDocumentStructure(true, true, false);
      this.UpdateVariableDataCaptions();
      this.SortDocument();
      this.IndexAVSDocument(false);
      this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(fromPage, true, true, true, true);
    }
  }

  /// <summary>Установить индексы сортировки для разделов и строк спецификации</summary>
  /// <param name="onlyNew">Не менять уже установленные индексы</param>
  public void IndexAVSDocument(bool onlyNew)
  {
    if (!this.IsSpecification)
      return;
    bool isRowsUpdating = this.IsRowsUpdating;
    this.EnterPendingRelationUpdateMode();
    try
    {
      this.IsRowsUpdating = true;
      this.PerformIndexingForCommonAndSpecialParts(onlyNew);
    }
    finally
    {
      this.ExitPendingRelationUpdateMode();
      this.IsRowsUpdating = isRowsUpdating;
    }
    this.CheckSortIndexWarning(true);
  }

  /// <summary>
  /// Индексировать общую часть, переменные данные и доп. секции.
  /// </summary>
  /// <param name="onlyNew"></param>
  private void PerformIndexingForCommonAndSpecialParts(bool onlyNew)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long endIndex = 0;
      SpecificationSection section = this.commonDataChapter as SpecificationSection;
      for (int index = 0; index < this.commonDataChapter.Chapters.Count || index == 0 && section != null; ++index)
      {
        if (section == null || index > 0)
          section = this.commonDataChapter.Chapters[index] as SpecificationSection;
        if (section != null)
        {
          long startIndex = section.SortIndex * 10000000L;
          if (index > 0 && startIndex < endIndex)
            startIndex = endIndex + 100L;
          endIndex = startIndex + 10000000L;
          if (index > 0 && startIndex > endIndex)
            endIndex = startIndex + 100000L;
          this.IndexSpecificationRows(section, onlyNew, startIndex, ref endIndex, sessionKeeper.Session);
        }
        else
        {
          long startIndex = endIndex + 1000L;
          this.commonDataChapter.Chapters[index].IndexSpecificationRows(startIndex, out endIndex, onlyNew, sessionKeeper.Session);
        }
      }
      if (this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
      {
        for (int index1 = 0; index1 < this.variableDataChapter_FormA.Chapters.Count; ++index1)
        {
          ProductVariableDataChapter chapter1 = (ProductVariableDataChapter) this.variableDataChapter_FormA.Chapters[index1];
          for (int index2 = 0; index2 < chapter1.Chapters.Count; ++index2)
          {
            if (chapter1.Chapters[index2] is SpecificationSection chapter2)
            {
              long chapterId = chapter1.ChapterID;
              long num1;
              if (chapterId == -1L)
              {
                num1 = 0L;
              }
              else
              {
                long num2 = Math.Abs(chapterId);
                if (num2 < 10000L)
                  num2 *= 10000L;
                num1 = num2 + 10000000L;
              }
              if (index2 > 0 && num1 < endIndex)
                num1 = endIndex + 100L;
              long startIndex = num1 + chapter2.SortIndex * 10000000L;
              endIndex = startIndex + 10000000L;
              if (index2 > 0 && startIndex > endIndex)
                endIndex = startIndex + 100000L;
              this.IndexSpecificationRows(chapter2, onlyNew, startIndex, ref endIndex, sessionKeeper.Session);
            }
            else
            {
              long startIndex = endIndex + 1000L;
              chapter1.Chapters[index2].IndexSpecificationRows(startIndex, out endIndex, onlyNew, sessionKeeper.Session);
            }
          }
        }
      }
      if (this.AvsDocumentForm == AVSDocumentForm.V && this.variableDataChapter_FormV != null)
      {
        for (int index = 0; index < this.variableDataChapter_FormV.Chapters.Count; ++index)
        {
          if (this.variableDataChapter_FormV.Chapters[index] is SpecificationSection chapter)
          {
            long startIndex = chapter.SortIndex * 10000000L;
            if (index > 0 && startIndex < endIndex)
              startIndex = endIndex + 100L;
            endIndex = startIndex + 10000000L;
            if (index > 0 && startIndex > endIndex)
              endIndex = startIndex + 100000L;
            this.IndexSpecificationRows(chapter, onlyNew, startIndex, ref endIndex, sessionKeeper.Session);
          }
          else
          {
            long startIndex = endIndex + 1000L;
            this.variableDataChapter_FormV.Chapters[index].IndexSpecificationRows(startIndex, out endIndex, onlyNew, sessionKeeper.Session);
          }
        }
      }
      long startIndex1 = endIndex + 1000L;
      for (int index = 0; index < this.rootChapters.Count; ++index)
      {
        if (this.rootChapters[index].IsAdditionalChapter)
        {
          this.rootChapters[index].IndexSpecificationRows(startIndex1, out endIndex, onlyNew, sessionKeeper.Session);
          startIndex1 = endIndex + 1000L;
        }
      }
    }
  }

  /// <summary>Установить индексы сортировки для строк в заданном разделе спецификации</summary>
  /// <param name="section">Раздел спецификации</param>
  /// <param name="onlyNew">Не менять уже установленные индексы</param>
  /// <param name="startIndex">Начало диапазона индексов</param>
  /// <param name="endIndex">Конец диапазона индексов</param>
  /// <param name="session">Сессия</param>
  public void IndexSpecificationRows(
    SpecificationSection section,
    bool onlyNew,
    long startIndex,
    ref long endIndex,
    IUserSession session)
  {
    if (section.Rows.Count == 0)
      return;
    int num1 = 1;
    int count = section.Rows.Count;
    long startIndex1 = startIndex;
    long num2 = endIndex;
    long indexStep1 = (num2 - startIndex1) / (long) (count - num1 + 1);
    if (indexStep1 < 4L)
    {
      indexStep1 = 4L;
      num2 = startIndex1 + (long) section.Rows.Count * indexStep1 + 1L;
    }
    if (this.AutoSort && ImDocumentData.ShowDebugInfo)
    {
      for (int index = 0; index < section.Rows.Count - 1; ++index)
      {
        if (section.Compare(section.Rows[index + 1], section.Rows[index]) < 0)
          LogManager.AddLine($"AVS. Warning index Row: Incorrect sortIndex order. RelId 1 [{section.Rows[index].RelId}] - SortIndex 1 [{section.Rows[index].SortIndex}]; RelId 2 [{section.Rows[index + 1].RelId}] - SortIndex 2 [{section.Rows[index + 1].SortIndex}]", true);
      }
    }
    if (onlyNew)
    {
      int num3 = 0;
      int index;
      for (; num3 < section.Rows.Count; num3 = index + 1)
      {
        int num4;
        for (num4 = num3; num4 < section.Rows.Count; ++num4)
        {
          AVSRow row = section.Rows[num4];
          row.sortIndex = row.SortIndex;
          if (row.sortIndex > 0L)
          {
            if (row.sortIndex <= startIndex1)
              row.SetSortIndex(0L, true, false, false);
            else
              startIndex1 = row.sortIndex;
          }
          if (row.sortIndex > 0L)
            row.SyncSortIndexForRelations();
          else
            break;
        }
        for (index = num4 + 1; index < section.Rows.Count; ++index)
        {
          AVSRow row = section.Rows[index];
          row.sortIndex = row.SortIndex;
          if (row.sortIndex > 0L && row.sortIndex <= startIndex1)
            row.SetSortIndex(0L, true, false, false);
          if (row.sortIndex > 0L)
          {
            row.SyncSortIndexForRelations();
            break;
          }
        }
        if (index < section.Rows.Count)
          num2 = section.Rows[index].sortIndex;
        if (num4 < section.Rows.Count)
        {
          if (index == section.Rows.Count || num2 == startIndex1)
          {
            num2 = (long) ((double) (section.SortIndex + 1L) * 1000000.0);
            if (num2 <= startIndex1)
              num2 = (long) ((double) (section.SortIndex + 1L) * 10000000000.0 - 1.0);
            if (num2 <= startIndex1)
              num2 = startIndex1 + 5000L;
          }
          long num5 = (long) (index - num4);
          if (num5 < 4L)
            num5 = 4L;
          long indexStep2 = (num2 - startIndex1) / (num5 + 1L);
          if (indexStep2 < 3L)
            indexStep2 = 10L;
          startIndex1 = this.IndexSpecificationRows(section, num4, index - num4, startIndex1, indexStep2, session);
        }
      }
    }
    else
      this.IndexSpecificationRows(section, 0, section.Rows.Count, startIndex1, indexStep1, session);
    if (section.Rows.Count <= 0 || section.Rows[section.Rows.Count - 1].SortIndex <= endIndex)
      return;
    endIndex = section.Rows[section.Rows.Count - 1].SortIndex;
  }

  /// <summary>Установить индексы сортировки для строк в заданном разделе спецификации</summary>
  /// <param name="section">Раздел спецификации</param>
  /// <param name="startRow">Первая строка</param>
  /// <param name="count">Количество</param>
  /// <param name="startIndex">Стартовый индекс (начало диапазона индексов)</param>
  /// <param name="indexStep">Шаг индексации</param>
  /// <param name="session">Сессия</param>
  /// <returns>Возвращает последний использованный индекс</returns>
  public long IndexSpecificationRows(
    SpecificationSection section,
    int startRow,
    int count,
    long startIndex,
    long indexStep,
    IUserSession session)
  {
    long num = 0;
    long key = startIndex;
    for (int index = 0; index < count && index + startRow < section.Rows.Count; ++index)
    {
      AVSRow row = section.Rows[startRow + index];
      if (key >= startIndex + indexStep * (num + 1L))
        num = Convert.ToInt64(Math.Truncate((double) key / (double) indexStep)) + 1L;
      key = startIndex + indexStep * (num + 1L);
      ++num;
      AVSRow avsRow;
      while (this.SortIndexDictionary.TryGetValue(key, out avsRow) && avsRow != row)
        ++key;
      row.SetSortIndex(key, true, false, false);
    }
    return key;
  }

  /// <summary>Найти индекс для нового элемента в сортированном списке</summary>
  /// <param name="item">Вставляемый объект</param>
  /// <param name="list">Сортированный список</param>
  /// <param name="lastEqual">Вставлять после последнего элемента равного новому</param>
  /// <param name="startIndex">Индекс с которого начинается поиск положения в списке</param>
  /// <param name="comparer">Объект сравнивающий элементы</param>
  /// <returns>Положение элемента в списке</returns>
  public static int FindIndexInSortedList(
    object item,
    IList list,
    bool lastEqual,
    int startIndex,
    IComparer comparer)
  {
    if (list.Count == 0)
      return 0;
    int num1 = startIndex;
    int index1 = list.Count - 1;
    if (comparer.Compare(list[index1], item) <= 0)
      return list.Count;
    if (list.Count == 1)
      return 0;
    if (startIndex > 0)
    {
      int num2 = comparer.Compare(list[startIndex], item);
      if (num2 == 0)
      {
        if (!lastEqual)
          return startIndex;
        for (int index2 = startIndex + 1; index2 < list.Count; ++index2)
        {
          num2 = comparer.Compare(list[index2], item);
          if (num2 != 0)
            return index2;
        }
      }
      if (num2 > 0)
      {
        num1 = 0;
        index1 = startIndex;
      }
    }
    if (num1 == 0)
    {
      int num3 = comparer.Compare(list[0], item);
      if (num3 > 0)
        return 0;
      if (num3 == 0)
      {
        if (!lastEqual)
          return 0;
        for (int index3 = 1; index3 < list.Count; ++index3)
        {
          if (comparer.Compare(list[index3], item) != 0)
            return index3;
        }
      }
    }
    while (index1 - num1 > 1)
    {
      int index4 = (index1 + num1) / 2;
      int num4 = comparer.Compare(list[index4], item);
      if (num4 == 0)
      {
        if (!lastEqual)
          return index4;
        for (int index5 = index4; index5 < list.Count; ++index5)
        {
          num4 = comparer.Compare(list[index5], item);
          if (num4 != 0)
            return index5;
        }
      }
      if (num4 < 0)
        num1 = index4;
      else
        index1 = index4;
    }
    return index1;
  }

  /// <summary>Найти индекс для нового элемента в частично сортированном списке</summary>
  /// <param name="item">Вставляемый объект</param>
  /// <param name="list">Сортированный список</param>
  /// <param name="lastEqual">Вставлять после последнего элемента равного новому</param>
  /// <param name="startIndex">Индекс с которого начинается поиск положения в списке</param>
  /// <param name="comparer">Объект сравнивающий элементы</param>
  /// <param name="IsSortedItem">Делегат проверки - отсортирован ли элемент</param>
  /// <returns>Положение элемента в списке</returns>
  public static int FindIndexInPartlySortedList(
    object item,
    IList list,
    bool lastEqual,
    int startIndex,
    IComparer comparer,
    IsSortedItemDelegate IsSortedItem)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (comparer == null)
      throw new ArgumentNullException(nameof (comparer));
    if (IsSortedItem == null)
      throw new ArgumentNullException(nameof (IsSortedItem));
    if (list.Count == 0)
      return 0;
    int index1 = list.Count - 1;
    while (index1 > -1 && !IsSortedItem(list[index1]))
      --index1;
    if (index1 < 0)
      return list.Count;
    if (comparer.Compare(list[index1], item) <= 0)
      return list.Count;
    if (list.Count == 1)
      return 0;
    int index2 = startIndex;
    while (index2 < index1 && !IsSortedItem(list[index2]))
      ++index2;
    if (index2 == index1)
    {
      index2 = startIndex - 1;
      while (index2 > -1 && !IsSortedItem(list[index2]))
        --index2;
      if (index2 == -1)
        return index1;
    }
    int num1 = comparer.Compare(list[index2], item);
    if (num1 == 0)
    {
      if (!lastEqual)
        return index2;
      int num2 = index2;
      for (int index3 = index2 + 1; index3 < index1; ++index3)
      {
        if (IsSortedItem(item))
        {
          if (comparer.Compare(list[index3], item) != 0)
            return num2 + 1;
          num2 = index3;
        }
      }
      return num2 + 1;
    }
    if (num1 > 0)
    {
      index1 = index2;
      index2 = 0;
      while (index2 < index1 && !IsSortedItem(list[index2]))
        ++index2;
      if (index2 == index1)
        return index1;
      int num3 = comparer.Compare(list[index2], item);
      if (num3 == 0)
      {
        if (!lastEqual)
          return index2;
        int num4 = index2;
        for (int index4 = index2 + 1; index4 < index1; ++index4)
        {
          if (IsSortedItem(list[index4]))
          {
            if (comparer.Compare(list[index4], item) != 0)
              return num4 + 1;
            num4 = index4;
          }
        }
        return num4 + 1;
      }
      if (num3 > 0)
        return index2;
    }
    while (index1 - index2 > 1)
    {
      bool flag = false;
      int index5 = (index1 + index2) / 2;
      while (index5 < index1 && !IsSortedItem(list[index5]))
        ++index5;
      if (index5 == index1)
      {
        flag = true;
        index5 = (index1 + index2) / 2 - 1;
        while (index5 > index2 && !IsSortedItem(list[index5]))
          --index5;
        if (index5 == index2)
          break;
      }
      int num5 = comparer.Compare(list[index5], item);
      if (num5 == 0)
      {
        if (!lastEqual)
          return index5;
        int num6 = index5;
        for (int index6 = index5 + 1; index6 < index1; ++index6)
        {
          if (IsSortedItem(list[index6]))
          {
            if (comparer.Compare(list[index6], item) != 0)
              return num6 + 1;
            num6 = index6;
          }
        }
        return num6 + 1;
      }
      if (num5 < 0)
      {
        index2 = index5;
        if (flag)
          break;
      }
      else
        index1 = index5;
    }
    return index2 + 1;
  }

  /// <summary>Загрузить незагруженные атрибуты нужные для сортировки</summary>
  public virtual void LoadNewSortAttributes()
  {
    if (this.SortSchema == null)
      return;
    this.LoadNewAttributes(this.SortSchema.GetAllAttrInfo(), true);
  }

  /// <summary>Сортировать список исполнений</summary>
  /// <param name="products">Список исполнений</param>
  public void SortProducts(List<ProductInfo> products)
  {
    if (products.Count == 0)
      return;
    if (!this.UseSameDesignationForProducts)
    {
      products.Sort((IComparer<ProductInfo>) new AutoPromProductsComparer(this));
    }
    else
    {
      products.Sort((IComparer<ProductInfo>) new ProductsComparer(this));
      if (!(this.DocumentDesignation == "") && this.DocumentDesignation != null)
        return;
      this.DocumentDesignation = products[0].Designation;
      products.Sort((IComparer<ProductInfo>) new ProductsComparer(this));
    }
  }

  /// <summary>Сортировать исполнения</summary>
  public void SortDocumentProducts()
  {
    if (this.productsInfo.Count == 0)
      return;
    this.SortProducts(this.productsInfo);
    for (int index1 = 0; index1 < this.productsInfo.Count; ++index1)
    {
      if (this.variableDataChapter_FormA != null)
      {
        Chapter chapter = this.variableDataChapter_FormA.GetChapter(this.productsInfo[index1].Id);
        if (chapter != null)
          chapter.SortIndex = (long) index1;
      }
      for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
      {
        if (this.rootChapters[index2].IsAdditionalChapter)
        {
          for (int index3 = 0; index3 < this.rootChapters[index2].Chapters.Count; ++index3)
          {
            if (this.rootChapters[index2].Chapters[index3] is VariableDataChapterFormA chapter1)
            {
              Chapter chapter = chapter1.GetChapter(this.productsInfo[index1].Id);
              if (chapter != null)
                chapter.SortIndex = (long) index1;
            }
          }
        }
      }
    }
    if (this.variableDataChapter_FormA != null)
      this.variableDataChapter_FormA.Chapters.Sort();
    for (int index4 = 0; index4 < this.rootChapters.Count; ++index4)
    {
      if (this.rootChapters[index4].IsAdditionalChapter)
      {
        for (int index5 = 0; index5 < this.rootChapters[index4].Chapters.Count; ++index5)
        {
          if (this.rootChapters[index4].Chapters[index5] is VariableDataChapterFormA chapter)
            chapter.Chapters.Sort();
        }
      }
    }
    this.CheckSortIndexWarning(true);
  }

  /// <summary>Сортировать исполнения учётом порядка в документе. Новые исполнения попадают в конец документа</summary>
  /// <param name="productsInDoc">Порядок исполнений в документе</param>
  public void SortProductsByDocOrder(List<ProductInfo> productsInDoc)
  {
    if (productsInDoc == null || this.productsInfo == null || this.productsInfo.Count < 2)
      return;
    int index1 = 0;
    for (int index2 = 0; index2 < productsInDoc.Count && index1 < this.productsInfo.Count; ++index2)
    {
      if (this.productsInfo[index1] != productsInDoc[index2])
      {
        for (int index3 = index1 + 1; index3 < this.productsInfo.Count; ++index3)
        {
          if (this.productsInfo[index3] == productsInDoc[index2])
          {
            this.productsInfo.RemoveAt(index3);
            this.productsInfo.Insert(index1++, productsInDoc[index2]);
            break;
          }
        }
      }
      else
        ++index1;
    }
    for (int index4 = 0; index4 < this.productsInfo.Count; ++index4)
    {
      if (this.variableDataChapter_FormA != null)
      {
        Chapter chapter = this.variableDataChapter_FormA.GetChapter(this.productsInfo[index4].Id);
        if (chapter != null)
          chapter.SortIndex = (long) index4;
      }
      for (int index5 = 0; index5 < this.rootChapters.Count; ++index5)
      {
        if (this.rootChapters[index5].IsAdditionalChapter)
        {
          for (int index6 = 0; index6 < this.rootChapters[index5].Chapters.Count; ++index6)
          {
            if (this.rootChapters[index5].Chapters[index6] is VariableDataChapterFormA chapter1)
            {
              Chapter chapter = chapter1.GetChapter(this.productsInfo[index4].Id);
              if (chapter != null)
                chapter.SortIndex = (long) index4;
            }
          }
        }
      }
    }
    if (this.variableDataChapter_FormA != null)
      this.variableDataChapter_FormA.Chapters.Sort();
    for (int index7 = 0; index7 < this.rootChapters.Count; ++index7)
    {
      if (this.rootChapters[index7].IsAdditionalChapter)
      {
        for (int index8 = 0; index8 < this.rootChapters[index7].Chapters.Count; ++index8)
        {
          if (this.rootChapters[index7].Chapters[index8] is VariableDataChapterFormA chapter)
            chapter.Chapters.Sort();
        }
      }
    }
  }

  /// <summary>Сортировать исполнения с учётом порядка в заданном списке. Не заданные исполнения попадают в конец документа</summary>
  /// <param name="productsOrder">Порядок исполнений в документе</param>
  public void SortProductByListAndUpdateDocument(List<ProductInfo> productsOrder)
  {
    if (productsOrder == null || this.productsInfo == null || this.productsInfo.Count < 2)
      return;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      this.SortProductsByDocOrder(productsOrder);
      this.UpdateViewNodes(false, this.IsGridViewMode, true, true, false, EmptyRowUpdateMode.DontChange);
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true, true);
    }
  }

  /// <summary>Диагностический метод для внутреннего использования.
  /// Проверяет словарь индексов сортировки на аномалии</summary>
  /// <param name="debugOutput">Выводить информацию в режиме отладки</param>
  /// <returns>Возвращает true, если всё в порядке</returns>
  private bool CheckSortIndexWarning(bool debugOutput)
  {
    bool flag = true;
    foreach (KeyValuePair<long, AVSRow> sortIndex in this.SortIndexDictionary)
    {
      if (sortIndex.Value == null)
      {
        flag = false;
        if (!debugOutput)
          ;
      }
      else
      {
        if (sortIndex.Key <= 0L)
        {
          flag = false;
          int num = debugOutput ? 1 : 0;
        }
        if (sortIndex.Value.Section == null)
        {
          flag = false;
          int num = debugOutput ? 1 : 0;
        }
        if (sortIndex.Value.Relations != null)
        {
          for (int index = 0; index < sortIndex.Value.Relations.Count; ++index)
          {
            if (sortIndex.Value.Relations[index].SortIndex != sortIndex.Key)
            {
              flag = false;
              int num = debugOutput ? 1 : 0;
            }
          }
        }
        else if (sortIndex.Value.SortIndex != sortIndex.Key)
        {
          flag = false;
          int num = debugOutput ? 1 : 0;
        }
      }
    }
    return flag;
  }

  /// <summary>
  /// Обновить кэш настроек типов документов за одну сессию, чтобы не дёргать потом каждый раз
  /// </summary>
  private void ReloadAllDocTypeNames()
  {
    if (this.objectDictionary.IsEmpty<KeyValuePair<long, List<AVSRow>>>())
      return;
    Dictionary<int, long> genericCollection = new Dictionary<int, long>();
    foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in this.objectDictionary)
    {
      List<AVSRow> source = keyValuePair.Value;
      AVSRow avsRow = source != null ? source.FirstOrDefault<AVSRow>() : (AVSRow) null;
      if (avsRow != null && avsRow.IsDocObject && !genericCollection.ContainsKey(avsRow.ObjType))
        genericCollection.Add(avsRow.ObjType, avsRow.ObjectId);
    }
    if (genericCollection.IsEmpty<KeyValuePair<int, long>>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDocumentTypeSettingsService customService = sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService;
      foreach (int key in genericCollection.Keys)
      {
        DocumentTypeSettingsHelper.ReloadSettings(sessionKeeper.Session, customService, key);
        string docTypeName = DocumentTypeSettingsHelper.GetDocTypeName(key);
        this._docTypeToDocTypeName[key] = docTypeName;
      }
    }
  }

  /// <summary>Получить имя типа документа</summary>
  /// <param name="docType">Тип документа</param>
  /// <returns></returns>
  public string GetDocTypeName(int docType)
  {
    string docTypeName1 = (string) null;
    if (this._docTypeToDocTypeName.TryGetValue(docType, out docTypeName1))
      return docTypeName1;
    string docTypeName2 = DocumentTypeSettingsHelper.GetDocTypeName(docType);
    this._docTypeToDocTypeName[docType] = docTypeName2;
    return docTypeName2;
  }

  /// <summary>Обновить поля "Формат" у всех изделий, входящих в спецификацию</summary>
  public void ReloadFormatAttributeInEntireSpecificationFromDB()
  {
    ObjectLinksList<AVSRow> objIdToSpecRowHash = new ObjectLinksList<AVSRow>();
    List<AVSRow> allRows = this.GetAllRows(false, true);
    for (int index = 0; index < allRows.Count; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(allRows[index].ObjType, AvsIDCache.ObjType_Product))
        objIdToSpecRowHash.RegisterObjectAndLink(allRows[index].ObjectId, allRows[index]);
    }
    if (objIdToSpecRowHash.List.Count <= 0)
      return;
    this.UpdateFormatAndTextReferencesInRows(objIdToSpecRowHash, true);
  }

  /// <summary>Очистить свойство "Смотри" для всех записей документа</summary>
  public void ClearSmotriAttributeInEntireSpecification()
  {
    this.SuspendDocumentAndGridUpdates();
    try
    {
      List<AVSRow> allRows = this.GetAllRows(false, true);
      for (int index = 0; index < allRows.Count; ++index)
      {
        if (allRows[index].HasDocNodes)
        {
          allRows[index].TextLinkToMainDocument = "";
          allRows[index].UpdateNameDocCellText(false, false);
        }
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  /// <summary>Обновить поле "Формат" у связей</summary>
  /// <param name="refIDs">Список связей</param>
  /// <param name="updateNameCell">Вызывать обновление для ячейки Наименование</param>
  public void UpdateFormatAttributeInReferencesFromDB(IList<long> refIDs, bool updateNameCell)
  {
    if (refIDs == null || refIDs.Count <= 0)
      return;
    ObjectLinksList<AVSRow> objIdToSpecRowHash = new ObjectLinksList<AVSRow>(refIDs.Count);
    foreach (long refId in (IEnumerable<long>) refIDs)
    {
      AVSRow avsDocRow = this.GetAvsDocRow(refId);
      if (avsDocRow != null && MetaDataHelper.IsObjectTypeChildOf(avsDocRow.ObjType, AvsIDCache.ObjType_Product))
        objIdToSpecRowHash.RegisterObjectAndLink(avsDocRow.ObjectId, avsDocRow);
    }
    if (objIdToSpecRowHash.List.Count <= 0)
      return;
    this.UpdateFormatAndTextReferencesInRows(objIdToSpecRowHash, updateNameCell);
  }

  /// <summary>Обновить поле "Формат" у записей</summary>
  /// <param name="specRows">Список записей</param>
  /// <param name="updateNameCell">Вызывать обновление для ячейки Наименование</param>
  public void UpdateFormatAttributeInRowsFromDB(List<AVSRow> specRows, bool updateNameCell)
  {
    if (specRows == null || specRows.Count <= 0)
      return;
    ObjectLinksList<AVSRow> objIdToSpecRowHash = new ObjectLinksList<AVSRow>(specRows.Count);
    for (int index = 0; index < specRows.Count; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(specRows[index].ObjType, AvsIDCache.ObjType_Product))
        objIdToSpecRowHash.RegisterObjectAndLink(specRows[index].ObjectId, specRows[index]);
    }
    if (objIdToSpecRowHash.List.Count <= 0)
      return;
    this.UpdateFormatAndTextReferencesInRows(objIdToSpecRowHash, updateNameCell);
  }

  /// <summary>Обновить поле "Формат" и текстовые ссылки типа "Смотри" и "Заготовка для"</summary>
  /// <param name="objIdToSpecRowHash">Идентификаторы объектов для которых нужно обновить формат</param>
  /// <param name="updateNameCell">Вызывать обновление для ячейки Наименование</param>
  public void UpdateFormatAndTextReferencesInRows(
    ObjectLinksList<AVSRow> objIdToSpecRowHash,
    bool updateNameCell)
  {
    if (objIdToSpecRowHash == null || objIdToSpecRowHash.List.Count <= 0)
      return;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      Dictionary<long, List<long>> mainDocIdToObjIdLinks = new Dictionary<long, List<long>>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AvsRowAttributeInfo rowAttributeInfo = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format);
        AvsRowAttributeInfo attrFirstApp = new AvsRowAttributeInfo(false, AvsIDCache.Attr_FirstApplicability);
        bool isGridViewMode = this.IsGridViewMode;
        this.UpdateDraftForPartTextLinks();
        foreach (long registeredObjectId in objIdToSpecRowHash.RegisteredObjectIDs)
        {
          IDBObject objectActual = sessionKeeper.Session.GetObjectActual(registeredObjectId, true);
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(registeredObjectId);
          int attrFormat = AvsIDCache.Attr_Format;
          string str1 = Convert.ToString(objectActual.GetAttributeByID(attrFormat)?.Value);
          bool flag = objectInfo.ObjectTypeID == AvsIDCache.ObjType_DetailWithoutDrawing;
          long[] numArray;
          if (AVSPlugin.IArticleService != null && !flag)
          {
            long[] idsForAllDrawings = AVSPlugin.IArticleService.FindMainDocumentIDsForAllDrawings(new long[1]
            {
              registeredObjectId
            }, AVSPlugin.IFiltrationService != null ? AVSPlugin.IFiltrationService.FiltrationServiceOwnerID : "", (object) sessionKeeper.Session);
            numArray = idsForAllDrawings != null ? ((IEnumerable<long>) idsForAllDrawings).Where<long>((System.Func<long, bool>) (x => Math.Abs(x) > 1L)).ToArray<long>() : (long[]) null;
          }
          else
            numArray = new long[0];
          if (numArray != null && numArray.Length != 0)
          {
            foreach (long key in numArray)
            {
              if (!mainDocIdToObjIdLinks.ContainsKey(key))
                mainDocIdToObjIdLinks.Add(key, new List<long>((IEnumerable<long>) new long[1]
                {
                  registeredObjectId
                }));
              else
                mainDocIdToObjIdLinks[key].Add(registeredObjectId);
            }
          }
          else
          {
            string str2 = str1;
            ReadOnlyCollection<AVSRow> readOnlyCollection = objIdToSpecRowHash[registeredObjectId];
            if (readOnlyCollection != null)
            {
              foreach (AVSRow avsRow in readOnlyCollection)
              {
                if (avsRow != null)
                {
                  if (string.IsNullOrWhiteSpace(str2))
                  {
                    str2 = this.GetDefaultFormat(avsRow.ObjType);
                    if (str2 == null)
                      break;
                  }
                  avsRow.SetFieldValue(rowAttributeInfo, -1, -1, (object) str2, false, false, true, isGridViewMode, false, false);
                }
              }
            }
          }
        }
        if (mainDocIdToObjIdLinks.Count <= 0)
          return;
        ColumnDescriptor[] columns = new ColumnDescriptor[5]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
          new ColumnDescriptor((object) AvsIDCache.Attr_Format),
          new ColumnDescriptor((object) AvsIDCache.Attr_Designation),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
          new ColumnDescriptor((object) AvsIDCache.Attr_FirstApplicability)
        };
        List<long> longList = new List<long>();
        foreach (KeyValuePair<long, List<long>> keyValuePair in mainDocIdToObjIdLinks)
          longList.Add(keyValuePair.Key);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
        }, columns);
        AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
        objectCollection.ShowAllModifications = true;
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable == null || dataTable.Rows.Count <= 0)
          return;
        List<long> processedPartIDs = new List<long>();
        List<DataRow> prefDataRows = dataTable.Rows.Where((System.Func<DataRow, bool>) (r => MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(r[3]), AvsIDCache.ObjType_DetailDrawing) && !string.IsNullOrWhiteSpace(Convert.ToString(r[1])))).ToList<DataRow>();
        localUpdateReferencesFromMainDocs(prefDataRows, mainDocIdToObjIdLinks, processedPartIDs, rowAttributeInfo, attrFirstApp, isGridViewMode);
        localUpdateReferencesFromMainDocs(dataTable.Rows.Where((System.Func<DataRow, bool>) (r => !prefDataRows.Any<DataRow>((System.Func<DataRow, bool>) (p => Convert.ToInt32(p[0]) == Convert.ToInt32(r[0]))) && !string.IsNullOrWhiteSpace(Convert.ToString(r[1])))).ToList<DataRow>(), mainDocIdToObjIdLinks, processedPartIDs, rowAttributeInfo, attrFirstApp, isGridViewMode);
        localUpdateReferencesFromMainDocs(dataTable.Rows.Where((System.Func<DataRow, bool>) (r => string.IsNullOrWhiteSpace(Convert.ToString(r[1])))).ToList<DataRow>(), mainDocIdToObjIdLinks, processedPartIDs, rowAttributeInfo, attrFirstApp, isGridViewMode);
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }

    void localUpdateReferencesFromMainDocs(
      List<DataRow> mainDocRows,
      Dictionary<long, List<long>> mainDocIdToObjIdLinks,
      List<long> processedPartIDs,
      AvsRowAttributeInfo attrFormat,
      AvsRowAttributeInfo attrFirstApp,
      bool isGridView)
    {
      for (int index1 = 0; index1 < mainDocRows.Count; ++index1)
      {
        DataRow mainDocRow = mainDocRows[index1];
        string defaultFormat = Convert.ToString(mainDocRow[1]);
        long int64 = Convert.ToInt64(mainDocRow[0]);
        List<long> docIdToObjIdLink = mainDocIdToObjIdLinks[int64];
        for (int index2 = 0; index2 < docIdToObjIdLink.Count; ++index2)
        {
          long objectID = docIdToObjIdLink[index2];
          if (!processedPartIDs.Contains(objectID))
          {
            ReadOnlyCollection<AVSRow> readOnlyCollection = objIdToSpecRowHash[objectID];
            int int32 = Convert.ToInt32(mainDocRow[3]);
            if (string.IsNullOrEmpty(defaultFormat))
            {
              AVSRow avsRow = readOnlyCollection.FirstOrDefault<AVSRow>();
              if (avsRow != null)
                defaultFormat = this.GetDefaultFormat(avsRow.ObjType);
            }
            if (readOnlyCollection != null)
            {
              foreach (AVSRow avsRow in readOnlyCollection)
              {
                if (!string.IsNullOrWhiteSpace(defaultFormat))
                  avsRow.SetFieldValue(attrFormat, -1, -1, (object) defaultFormat, true, false, true, isGridView, false, false);
              }
            }
            if (this.AVSCommonPropertiesSchema.AutoGenerateTextLinkToMainDocumentInNameField && !readOnlyCollection.IsNullOrEmpty<AVSRow>())
            {
              string attributeValue = Convert.ToString(mainDocRow[2]);
              foreach (AVSRow avsRow in readOnlyCollection)
              {
                if ((MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Specification) || MetaDataHelper.IsObjectTypeChildOf(avsRow.ObjType, AvsIDCache.ObjType_Detail)) && avsRow.HasDocNodes && !string.IsNullOrEmpty(attributeValue) && !string.IsNullOrEmpty(avsRow.Designation) && !avsRow.Designation.Contains(attributeValue) && !attributeValue.Contains(avsRow.Designation))
                  avsRow.DocNode.SetAttributeValue(AVSRow.DocAttr_Smotri, attributeValue, false, false, false);
                else
                  avsRow.DocNode.SetAttributeValue(AVSRow.DocAttr_Smotri, "", false, false, false);
              }
            }
            string str = Convert.ToString(mainDocRow[4]);
            if (readOnlyCollection != null)
            {
              foreach (AVSRow avsRow in readOnlyCollection)
                avsRow.SetFieldValue(attrFirstApp, -1, -1, str == "" ? (object) (string) null : (object) str, true, false, true, isGridView, false, false);
            }
            processedPartIDs.Add(objectID);
          }
        }
      }
    }
  }

  private void UpdateDraftForPartTextLinks()
  {
    if (this.objectDictionary.IsEmpty<KeyValuePair<long, List<AVSRow>>>())
      return;
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Material);
    Dictionary<long, List<AVSRow>> dictionary1 = new Dictionary<long, List<AVSRow>>();
    Dictionary<long, List<AVSRow>> dictionary2 = new Dictionary<long, List<AVSRow>>();
    foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in this.objectDictionary)
    {
      AVSRow avsRow = keyValuePair.Value.FirstOrDefault<AVSRow>((System.Func<AVSRow, bool>) (r => r.RelType != AvsIDCache.Relation_Zagotovka));
      if (avsRow != null)
      {
        long fieldInt64Value = avsRow.GetFieldInt64Value(attrInfo, -1, (List<RelationAttributeValuesCache>) null, false);
        if (!fieldInt64Value.IsUndefinedId())
        {
          List<AVSRow> avsRowList;
          if (!dictionary1.TryGetValue(fieldInt64Value, out avsRowList))
            dictionary1.Add(fieldInt64Value, avsRowList = new List<AVSRow>());
          avsRowList.AddRange((IEnumerable<AVSRow>) keyValuePair.Value);
          if (!dictionary2.ContainsKey(fieldInt64Value))
          {
            List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(fieldInt64Value);
            dictionary2.Add(fieldInt64Value, avsRowsByObjectId.FindAll((Predicate<AVSRow>) (r => r.RelType == AvsIDCache.Relation_Zagotovka && r.DocNode != null)));
          }
        }
      }
    }
    foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in dictionary1)
    {
      List<AVSRow> avsRowList = keyValuePair.Value;
      long key = keyValuePair.Key;
      List<AVSRow> list;
      dictionary2.TryGetValue(key, out list);
      if (!list.IsEmpty<AVSRow>())
      {
        for (int index1 = list.Count - 1; index1 >= 0; --index1)
        {
          Guid partFromDraftGuid = AvsIDCache.ConvertToGuid((object) list[index1].DocNode.GetAttributeValue(AVSRow.DocAttr_PartFromDraftGuid, false));
          if (!(partFromDraftGuid == Guid.Empty))
          {
            int index2 = avsRowList.FindIndex((Predicate<AVSRow>) (r => r.ObjGuid == partFromDraftGuid));
            if (index2 != -1)
            {
              list[index1].SetLinkFromDraftToPart(avsRowList[index2]);
              list.RemoveAt(index1);
              avsRowList.RemoveAt(index2);
            }
          }
        }
      }
    }
    foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in dictionary1)
    {
      List<AVSRow> avsRowList = keyValuePair.Value;
      long key = keyValuePair.Key;
      List<AVSRow> list;
      dictionary2.TryGetValue(key, out list);
      if (!list.IsEmpty<AVSRow>())
      {
        for (int index3 = list.Count - 1; index3 >= 0; --index3)
        {
          string partFromDraftDesignation = list[index3].DocNode.GetAttributeValue(AVSRow.DocAttr_ZagotovkaDlya, true);
          if (!string.IsNullOrEmpty(partFromDraftDesignation))
          {
            int index4 = avsRowList.FindIndex((Predicate<AVSRow>) (r => r.DesignationOrName == partFromDraftDesignation));
            if (index4 != -1)
            {
              list[index3].SetLinkFromDraftToPart(avsRowList[index4]);
              list.RemoveAt(index3);
              avsRowList.RemoveAt(index4);
            }
          }
        }
      }
    }
    foreach (KeyValuePair<long, List<AVSRow>> keyValuePair in dictionary2)
    {
      AVSRow avsRow = keyValuePair.Value.FirstOrDefault<AVSRow>();
      if (avsRow != null)
      {
        long key = keyValuePair.Key;
        List<AVSRow> avsRowList;
        dictionary1.TryGetValue(key, out avsRowList);
        if (!avsRowList.IsEmpty<AVSRow>())
        {
          AVSRow partFromDraft = avsRowList.FirstOrDefault<AVSRow>();
          if (partFromDraft != null)
            avsRow.SetLinkFromDraftToPart(partFromDraft);
        }
      }
    }
  }

  /// <summary>Получить формат главного конструкторского документа для изделия типа</summary>
  /// <param name="objectType">Тип объекта изделия</param>
  /// <returns></returns>
  public string GetDefaultFormat(int objectType)
  {
    string defaultFormat = (string) null;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Specification) || AVSDocument.IsProductForSpecification2(objectType) && !MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Complect))
      defaultFormat = "A4";
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_DetailWithoutDrawing) && this.AVSCommonPropertiesSchema.ShowBCh)
      defaultFormat = "БЧ";
    return defaultFormat;
  }

  /// <summary>Получить список идентификаторов исполнений</summary>
  /// <param name="productInfoList">Список исполнений</param>
  /// <returns></returns>
  public List<long> ProductIds
  {
    [DebuggerStepThrough] get
    {
      if (this.productsInfo == null)
        return (List<long>) null;
      List<long> productIds = new List<long>(this.productsInfo.Count);
      for (int index = 0; index < this.productsInfo.Count; ++index)
        productIds.Add(this.productsInfo[index].Id);
      return productIds;
    }
  }

  /// <summary>Получить список идентификаторов исполнений</summary>
  /// <param name="productInfoList">Список исполнений</param>
  /// <returns></returns>
  public static List<long> GetProductIds(List<ProductInfo> productInfoList)
  {
    List<long> productIds = productInfoList != null ? new List<long>(productInfoList.Count) : throw new ArgumentNullException(nameof (productInfoList));
    for (int index = 0; index < productInfoList.Count; ++index)
      productIds.Add(productInfoList[index].Id);
    return productIds;
  }

  /// <summary>
  /// Возвращает true, если в групповой СП переменные части исполнений имеют отличия
  /// </summary>
  /// <returns></returns>
  public bool ProductsAreDifferent()
  {
    if (!this.IsFormA && !this.IsFormB && this.AvsDocumentForm != AVSDocumentForm.V)
      return false;
    List<\u003C\u003Ef__AnonymousType1<long, string, int, MeasuredValue, long, string>> list = this.GetAllRows(true, false).Where<AVSRow>((System.Func<AVSRow, bool>) (r => r.InVariableData_AV)).Select(i => new
    {
      ProductID = i.ProductID,
      Designation = i.Designation,
      Position = i.Position,
      Count = i.GetCount(-1, -1),
      SortIndex = i.SortIndex,
      Note = i.GetDocumentCellForAttribute(i.Field_Note, 0)?.GetText() ?? ""
    }).ToList();
    int prodCount = list.Select(r => r.ProductID).Distinct<long>().Count<long>();
    return list.GroupBy(k => $"[{k.Designation}][{k.Count}][{k.Position}][{k.SortIndex}][{k.Note}]", v => v.ProductID, (IEqualityComparer<string>) null).Any<IGrouping<string, long>>((System.Func<IGrouping<string, long>, bool>) (g => g.Count<long>() < prodCount));
  }

  /// <summary>Единичная форма документа</summary>
  [Browsable(false)]
  public bool IsSingleForm
  {
    [DebuggerStepThrough] get => this.AvsDocumentForm == AVSDocumentForm.Single;
  }

  /// <summary>Групповой документ формы А</summary>
  [Browsable(false)]
  public bool IsFormA
  {
    [DebuggerStepThrough] get => this.AvsDocumentForm == AVSDocumentForm.A;
  }

  /// <summary>Групповой документ формы Б</summary>
  [Browsable(false)]
  public bool IsFormB
  {
    [DebuggerStepThrough] get => AVSDocument.IsDocumentFormB(this.AvsDocumentForm);
  }

  /// <summary>Спецификация по структуре формы Б</summary>
  public static bool IsDocumentFormB(AVSDocumentForm docForm)
  {
    return docForm == AVSDocumentForm.B || docForm == AVSDocumentForm.Mirror;
  }

  /// <summary>Групповой документ формы В</summary>
  [Browsable(false)]
  public bool IsFormV
  {
    [DebuggerStepThrough] get => this.AvsDocumentForm == AVSDocumentForm.V;
  }

  /// <summary>Экспортная спецификация</summary>
  [Browsable(false)]
  public bool IsExportSP
  {
    [DebuggerStepThrough] get => this.AVSDocType == AVSDocumentType.ExportSpecification;
  }

  /// <summary>Получить индекс первого исполнения на странице. Для групповой формы Б</summary>
  /// <param name="page">Страница</param>
  /// <returns></returns>
  public int GetFirstProductIndex(PageData page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    int result = 0;
    if (page.Flows != null)
    {
      TableData docTableFromPage = this.FindMainDocTableFromPage(page, false);
      if (docTableFromPage != null)
      {
        string attributeValue = docTableFromPage.FindFirstTable().GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
        if (attributeValue == "" || !int.TryParse(attributeValue, out result))
          result = 0;
      }
    }
    return result;
  }

  protected void CheckPositionDesignationErrors(
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    for (int index = 0; index < this.rootChapters.Count; ++index)
      this.CheckPositionDesignationErrors(this.rootChapters[index], errorRows);
  }

  protected void CheckPositionDesignationErrors(
    Chapter ch,
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    List<AVSRow> rowList = new List<AVSRow>();
    if (ch.IsSectionOwner && !(ch is ProductVariableDataChapter))
    {
      foreach (Chapter chapter in ch.Chapters)
      {
        if (chapter is SpecificationSection specificationSection)
          specificationSection.GetAllRowsList(false, true, rowList);
        else
          this.CheckPositionDesignationErrors(chapter, errorRows);
      }
    }
    if (ch is ProductVariableDataChapter)
      ch.GetAllRowsList(false, true, rowList);
    List<string> stringList = new List<string>();
    foreach (AVSRow key in rowList)
    {
      string fieldStringValue = key.GetFieldStringValue(this.Field_PosDesignation, -1, -1, (List<RelationAttributeValuesCache>) null, false);
      if (fieldStringValue != null && fieldStringValue != "")
      {
        List<string> list = ((IEnumerable<string>) fieldStringValue.Split(',')).Select<string, string>((System.Func<string, string>) (x => x.Trim())).ToList<string>();
        bool flag = false;
        foreach (string str in list)
        {
          if (!string.IsNullOrWhiteSpace(str))
          {
            if (!stringList.Contains(str))
              stringList.Add(str);
            else
              flag = true;
          }
        }
        if (flag)
        {
          List<SpecRowCheckMessage> specRowCheckMessageList;
          if (!errorRows.TryGetValue(key, out specRowCheckMessageList))
            errorRows.Add(key, specRowCheckMessageList = new List<SpecRowCheckMessage>());
          specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.CheckDuplicatePositionDesignation, (string) null));
        }
      }
    }
    foreach (Chapter chapter in ch.Chapters)
      this.CheckPositionDesignationErrors(chapter, errorRows);
  }

  protected void CheckPositionsErrors(
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    for (int index = 0; index < this.rootChapters.Count; ++index)
      this.CheckPositionsErrors(this.rootChapters[index], errorRows);
  }

  protected void CheckPositionsErrors(
    Chapter ch,
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    AvsRowAttributeInfo fieldPosition = this.Field_Position;
    List<AVSRow> rowList = new List<AVSRow>();
    if (ch.IsSectionOwner && !(ch is ProductVariableDataChapter))
    {
      foreach (Chapter chapter in ch.Chapters)
      {
        if (chapter is SpecificationSection specificationSection)
          specificationSection.GetAllRowsList(false, true, rowList);
        else
          this.CheckPositionsErrors(chapter, errorRows);
      }
    }
    if (ch is ProductVariableDataChapter)
      ch.GetAllRowsList(false, true, rowList);
    List<string> stringList = new List<string>();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (AVSRow key in rowList)
    {
      if (key.RelType != AvsIDCache.Relation_Zagotovka)
      {
        string fieldStringValue = key.GetFieldStringValue(fieldPosition, -1, -1, (List<RelationAttributeValuesCache>) null, false);
        string commonPosition = key.CommonPosition;
        if (fieldStringValue != null && fieldStringValue != "")
        {
          if (!stringList.Contains(fieldStringValue))
          {
            stringList.Add(fieldStringValue);
            if (commonPosition != null)
              dictionary.Add(fieldStringValue, commonPosition);
          }
          else if ((commonPosition == null || !dictionary.ContainsKey(fieldStringValue) ? 0 : (commonPosition == dictionary[fieldStringValue] ? 1 : 0)) == 0)
          {
            List<SpecRowCheckMessage> specRowCheckMessageList;
            if (!errorRows.TryGetValue(key, out specRowCheckMessageList))
              errorRows.Add(key, specRowCheckMessageList = new List<SpecRowCheckMessage>());
            specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.DuplicatePosition, (string) null));
          }
        }
      }
    }
    foreach (Chapter chapter in ch.Chapters)
      this.CheckPositionsErrors(chapter, errorRows);
  }

  protected virtual void CheckPartWithoutZagotovka(
    AVSRow partRow,
    List<SpecRowCheckMessage> rowMessages)
  {
  }

  protected virtual bool CheckDraftCountValue(
    AVSRow draftRow,
    List<SpecRowCheckMessage> rowMessages = null)
  {
    return true;
  }

  public virtual void CheckErrorsInRows(
    AVSCheckType checkType,
    AVSCheckMode checkMode,
    ICollection<AVSRow> avsRows,
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
  }

  /// <summary>Изменить форму спецификации</summary>
  /// <param name="value">Новая форма спецификации</param>
  public void ChangeGroupDocumentForm(AVSDocumentForm value)
  {
    if (this.AvsDocumentForm == value)
      return;
    AVSDocumentForm avsDocumentForm = this.AvsDocumentForm;
    long documentTemplateId = this.DocumentTemplateID;
    Guid documentTemplateGuid = this.documentTemplateGuid;
    this.AvsDocumentForm = value;
    this.SuspendDocumentAndGridUpdates();
    this.Lock_DocCell_TextChanged();
    bool isDocumentLoading = this.Document.IsDocumentLoading;
    this.Document.IsDocumentLoading = true;
    try
    {
      ImDocumentData template1 = this.Document.DocumentTemplate;
      List<PageData> extractedTitlePages;
      PageData extractedLriPage;
      try
      {
        if (!this.IsSpecification && value != AVSDocumentForm.Single && this.parentProducts.Count > 0)
        {
          if (MessageBox.Show($"При преобразовании документа из единичного в групповой будут удалены все родительские изделия!{Environment.NewLine}Удалить родительские изделия?", "Внимание!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
          {
            this.AvsDocumentForm = avsDocumentForm;
            this.DocumentTemplateID = documentTemplateId;
            this.ResetSettingsFromTemplate();
            this.documentTemplateGuid = documentTemplateGuid;
            return;
          }
          this.RemoveParentProducts();
        }
        switch (value)
        {
          case AVSDocumentForm.Single:
            Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows1 = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
            this.CheckErrorsInRows(AVSCheckType.ObjectWithoutRelation, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) null, errorRows1);
            if (errorRows1.Count > 0)
            {
              if (this.PromptForCountlessRows(errorRows1) != DialogResult.OK)
              {
                this.AvsDocumentForm = avsDocumentForm;
                this.DocumentTemplateID = documentTemplateId;
                this.documentTemplateGuid = documentTemplateGuid;
                return;
              }
              foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> keyValuePair in errorRows1)
              {
                if (keyValuePair.Value.Count > 0)
                  keyValuePair.Key.Section?.RemoveRow(keyValuePair.Key, true, false, true, false, false);
              }
            }
            if (this.productsInfo.Count > 1 && MessageBox.Show($"При преобразовании групповой спецификации в единичную будут удалены все исполнения, кроме основного!{Environment.NewLine}Удалить исполнения?", "Внимание!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
              this.AvsDocumentForm = avsDocumentForm;
              this.DocumentTemplateID = documentTemplateId;
              this.documentTemplateGuid = documentTemplateGuid;
              return;
            }
            long id = -1;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              id = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, true);
            if (id.IsDefinedId())
            {
              if (this.documentTemplateGuid != documentTemplateGuid)
              {
                try
                {
                  this.DocumentTemplateID = id;
                  this.ResetSettingsFromTemplate();
                  template1 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
                  if (template1 == null)
                    throw new Exception("Шаблон единичной спецификации поврежден!");
                  this.FindAllTemplates(template1, true);
                }
                catch
                {
                  this.AvsDocumentForm = avsDocumentForm;
                  this.DocumentTemplateID = documentTemplateId;
                  this.ResetSettingsFromTemplate();
                  this.documentTemplateGuid = documentTemplateGuid;
                  this.FindAllTemplates(this.Document.DocumentTemplate, true);
                }
              }
            }
            if (this.IsGridViewMode)
              this.ClearTreeListNodes();
            if (this.productsInfo.Count > 1)
            {
              List<ProductInfo> products = new List<ProductInfo>((IEnumerable<ProductInfo>) this.productsInfo);
              products.RemoveAt(0);
              this.RemoveProductVersions((IList<ProductInfo>) products, true, false);
            }
            if (avsDocumentForm == AVSDocumentForm.A)
            {
              if (this.document.Template != template1 && template1 != null)
              {
                this.document.AssignDocumentTemplate(template1, true, false, true);
                this.document.ApplyTemplateProperties(false, false);
                this.document.CreateFirstPage();
              }
              this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
              if (this.commonDataChapter.DocNode != null)
              {
                int index = this.commonDataChapter.DocNode.Index;
                TableData dataOwner;
                for (int dataPositionInFlow = this.commonDataChapter.DocNode.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
                {
                  if (dataOwner.Nodes[dataPositionInFlow] is TableData node)
                  {
                    this.avsDocTable.InsertChildNode(index++, (DocumentTreeNode) node, true, true, false, false, false);
                    node.UpdateNodeLinks(true, false, false, false);
                    --dataPositionInFlow;
                  }
                }
                this.commonDataChapter.DocNode.UniteTable();
                this.commonDataChapter.DocNode.Remove(false, false);
              }
              this.commonDataChapter.DocNode = this.avsDocTable;
              this.UpdateDocumentStructure(true, true, true, true);
              if (this.IsGridViewMode)
                this.RecreateTreeListNodes();
            }
            else
            {
              this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
              this.Document.Clear(false, false);
              this.document.AssignDocumentTemplate(template1, true, false, true);
              this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
              this.document.ApplyTemplateProperties(false, false);
              this.document.CreateFirstPage();
              using (SessionKeeper sessionKeeper = new SessionKeeper())
                DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
              this.avsDocTable = (TableData) this.document.FindNode("Таблица Спецификация");
              this.TryAddMainTableIfNeed();
              this.CheckMainDocumentTablesAndThrowException();
              this.UpdateDocumentRowFieldsInfo();
              this.UpdateProductAttrs();
              this.LoadNewAttributes(this.docRowFields, false);
              this.UpdateDocumentStructure(true, false, false, true);
              if (this.avsWindow != null)
              {
                this.avsWindow.NeedToLoadColumnParams = true;
                this.avsWindow.LoadColumnsStateIfNeeded();
              }
              this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
              if (this.IsGridViewMode)
                this.RecreateTreeListNodes();
              this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
              if (this.IsGridViewMode)
                this.avsWindow.ExpandTreeListNodes();
              this.UpdateVariableDataCaptions();
              if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
                this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
            }
            this.UpdateProductLiteraForSP(true);
            break;
          case AVSDocumentForm.A:
            this.AvsDocumentForm = value;
            if (this.productsInfo.Count == 0 && !this.IsSpecification)
            {
              this.productsInfo = new List<ProductInfo>();
              using (new SessionKeeper())
                this.productsInfo.Add(this.GetElementListInfo());
            }
            if (this.variableDataChapter_FormA == null)
              this.VariableDataChapter_FormA = new VariableDataChapterFormA(this, this.productsInfo, true);
            if (avsDocumentForm == AVSDocumentForm.Single)
            {
              this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
              if (this.IsGridViewMode)
                this.ClearTreeListNodes();
              TableData dataOwner;
              int dataPositionInFlow = this.avsDocTable.FindDataPositionInFlow(0, out dataOwner);
              this.commonDataChapter.DocNode = (TableData) null;
              TableData docNode = this.commonDataChapter.CreateDocNode(this.commonChapterTemplate);
              this.commonDataChapter.DocNode = docNode;
              this.avsDocTable.InsertChildNode(dataPositionInFlow, (DocumentTreeNode) docNode, false, true, false, false, false);
              for (int index = dataPositionInFlow + 1; index != -1 && dataOwner != null && index < dataOwner.Nodes.Count; index = dataOwner.FindNextDataPositionInFlow(index, out dataOwner))
              {
                if (dataOwner.Nodes[index] is RectangleElement node)
                {
                  docNode.AddChildNode((DocumentTreeNode) node, true, true, false, false);
                  node.UpdateNodeLinks(true, false, false, false);
                  --index;
                }
              }
              this.UpdateViewNodes(false, false, false, true, true, EmptyRowUpdateMode.Delete);
              if (this.IsGridViewMode)
                this.RecreateTreeListNodes();
              this.commonDataChapter.RemoveEmptySections(false);
              this.UpdateVariableDataCaptions();
            }
            else
            {
              Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows2 = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
              this.CheckErrorsInRows(AVSCheckType.ObjectWithoutRelation, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) null, errorRows2);
              if (errorRows2.Count > 0)
              {
                if (this.PromptForCountlessRows(errorRows2) != DialogResult.OK)
                {
                  this.AvsDocumentForm = avsDocumentForm;
                  this.DocumentTemplateID = documentTemplateId;
                  this.documentTemplateGuid = documentTemplateGuid;
                  return;
                }
                foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> keyValuePair in errorRows2)
                {
                  if (keyValuePair.Value.Count > 0)
                    keyValuePair.Key.Section?.RemoveRow(keyValuePair.Key, true, false, true, false, false);
                }
              }
              try
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                  this.DocumentTemplateID = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, true);
                template1 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
                if (template1 == null)
                  throw new Exception("Шаблон групповой спецификации формы А поврежден!");
                this.ResetSettingsFromTemplate();
                this.FindAllTemplates(template1, true);
              }
              catch
              {
                this.AvsDocumentForm = avsDocumentForm;
                this.DocumentTemplateID = documentTemplateId;
                this.ResetSettingsFromTemplate();
                this.documentTemplateGuid = documentTemplateGuid;
                this.FindAllTemplates(this.Document.DocumentTemplate, true);
              }
              this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
              this.Document.Clear(false, false);
              if (this.IsGridViewMode)
                this.ClearTreeListNodes();
              this.document.AssignDocumentTemplate(template1, true, false, true);
              this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
              this.document.ApplyTemplateProperties(false, false);
              this.document.CreateFirstPage();
              using (SessionKeeper sessionKeeper = new SessionKeeper())
                DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
              this.avsDocTable = (TableData) this.document.FindNode("Таблица Спецификация");
              this.TryAddMainTableIfNeed();
              this.CheckMainDocumentTablesAndThrowException();
              this.UpdateDocumentRowFieldsInfo();
              this.UpdateProductAttrs();
              this.LoadNewAttributes(this.docRowFields, false);
              this.UpdateDocumentStructure(true, false, false, true);
              if (this.variableDataChapter_FormV != null)
                this.ClearVariableDataChapter_FormV();
              if (this.avsWindow != null)
              {
                this.avsWindow.NeedToLoadColumnParams = true;
                this.avsWindow.LoadColumnsStateIfNeeded();
              }
              this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
              if (this.IsGridViewMode)
                this.RecreateTreeListNodes();
              this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
              this.RemoveEmptySections(false);
              this.UpdateVariableDataCaptions();
              if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
                this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
            }
            this.UpdateProductLiteraForSP(true);
            break;
          case AVSDocumentForm.B:
            Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows3 = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
            this.CheckErrorsInRows(AVSCheckType.EmptyCount, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) null, errorRows3);
            if (errorRows3.Count > 0)
            {
              if (this.PromptForCountlessRows(errorRows3) != DialogResult.OK)
              {
                this.AvsDocumentForm = avsDocumentForm;
                this.DocumentTemplateID = documentTemplateId;
                this.documentTemplateGuid = documentTemplateGuid;
                return;
              }
              foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> keyValuePair in errorRows3)
                keyValuePair.Key.Section?.RemoveRow(keyValuePair.Key, true, true, true, false, false);
            }
            if (this.productsInfo.Count > 0 && !this.UseSameDesignationForProducts)
            {
              string number = this.productsInfo[0].GetNumber(this.DocumentDesignation, this.UseSameDesignationForProducts);
              if (number == null || number == "")
              {
                string productNumber = "";
                if (ProductNumberDlg.Execute(ref productNumber) == DialogResult.OK)
                {
                  this.productsInfo[0].SetNumber(productNumber, true);
                }
                else
                {
                  this.AvsDocumentForm = avsDocumentForm;
                  this.DocumentTemplateID = documentTemplateId;
                  this.documentTemplateGuid = documentTemplateGuid;
                  return;
                }
              }
            }
            try
            {
              this.AvsDocumentForm = value;
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                this.DocumentTemplateID = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, true);
                this.ResetSettingsFromTemplate();
              }
              template1 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
              if (template1 == null)
                throw new Exception("Шаблон групповой спецификации формы Б поврежден!");
              this.FindAllTemplates(template1, true);
            }
            catch
            {
              this.AvsDocumentForm = avsDocumentForm;
              this.DocumentTemplateID = documentTemplateId;
              this.documentTemplateGuid = documentTemplateGuid;
              this.ResetSettingsFromTemplate();
              this.FindAllTemplates(this.Document.DocumentTemplate, true);
            }
            if (this.IsGridViewMode)
              this.ClearTreeListNodes();
            this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
            this.Document.Clear(false, false);
            this.document.AssignDocumentTemplate(template1, true, false, true);
            this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
            this.document.ApplyTemplateProperties(false, false);
            this.document.CreateFirstPage();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
            this.avsDocTable = (TableData) this.document.FindNode("Таблица Спецификация");
            this.TryAddMainTableIfNeed();
            this.CheckMainDocumentTablesAndThrowException();
            this.UpdateDocumentRowFieldsInfo();
            this.UpdateProductAttrs();
            this.LoadNewAttributes(this.docRowFields, false);
            if (avsDocumentForm != AVSDocumentForm.Single)
              this.UpdateDocumentStructure(true, false, true, true);
            if (this.variableDataChapter_FormA != null)
            {
              this.variableDataChapter_FormA.Chapters.Clear();
              this.VariableDataChapter_FormA = (VariableDataChapterFormA) null;
            }
            if (this.variableDataChapter_FormV != null)
              this.ClearVariableDataChapter_FormV();
            if (this.avsWindow != null)
            {
              this.avsWindow.NeedToLoadColumnParams = true;
              this.avsWindow.LoadColumnsStateIfNeeded();
            }
            this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
            if (this.IsGridViewMode)
              this.RecreateTreeListNodes();
            this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
            this.UpdateVariableDataCaptions();
            if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
              this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
            this.UpdateProductLiteraForSP(true);
            break;
          case AVSDocumentForm.Mirror:
            Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows4 = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
            this.CheckErrorsInRows(AVSCheckType.EmptyCount, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) null, errorRows4);
            if (errorRows4.Count > 0)
            {
              if (this.PromptForCountlessRows(errorRows4) != DialogResult.OK)
              {
                this.AvsDocumentForm = avsDocumentForm;
                this.DocumentTemplateID = documentTemplateId;
                this.documentTemplateGuid = documentTemplateGuid;
                return;
              }
              foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> keyValuePair in errorRows4)
                keyValuePair.Key.Section?.RemoveRow(keyValuePair.Key, true, true, true, false, false);
            }
            string rightProductDesignation = (string) null;
            if (this.productsInfo.Count > 2 && MessageBox.Show($"При преобразовании групповой спецификации в зеркальную будут удалены все исполнения, кроме первых двух!{Environment.NewLine}Удалить исполнения?", "Внимание!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
              this.AvsDocumentForm = avsDocumentForm;
              this.DocumentTemplateID = documentTemplateId;
              this.documentTemplateGuid = documentTemplateGuid;
              return;
            }
            this.AvsDocumentForm = value;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              this.DocumentTemplateID = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, true);
              this.ResetSettingsFromTemplate();
            }
            ImDocumentData template2 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
            if (template2 == null)
              throw new Exception("Шаблон групповой спецификации для зеркальной формы поврежден!");
            this.FindAllTemplates(template2, true);
            if (this.IsGridViewMode)
              this.ClearTreeListNodes();
            if (this.productsInfo.Count > 2)
            {
              List<ProductInfo> products = new List<ProductInfo>((IEnumerable<ProductInfo>) this.productsInfo);
              products.RemoveAt(1);
              this.RemoveProductVersions((IList<ProductInfo>) products, true, false);
            }
            if (this.productsInfo.Count == 1 && RightProductDesignationDlg.Execute(rightProductDesignation = this.productsInfo[0].Designation, ref rightProductDesignation) != DialogResult.OK)
            {
              this.AvsDocumentForm = avsDocumentForm;
              this.DocumentTemplateID = documentTemplateId;
              this.documentTemplateGuid = documentTemplateGuid;
              return;
            }
            this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
            this.Document.Clear(false, false);
            this.document.AssignDocumentTemplate(template2, true, false, true);
            this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
            this.document.ApplyTemplateProperties(false, false);
            this.document.CreateFirstPage();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
            this.avsDocTable = (TableData) this.document.FindNode("Таблица Спецификация");
            this.TryAddMainTableIfNeed();
            this.CheckMainDocumentTablesAndThrowException();
            this.UpdateDocumentRowFieldsInfo();
            this.UpdateProductAttrs();
            this.LoadNewAttributes(this.docRowFields, false);
            if (avsDocumentForm != AVSDocumentForm.Single && avsDocumentForm != AVSDocumentForm.B)
              this.UpdateDocumentStructure(true, false, true, true);
            if (this.variableDataChapter_FormA != null)
            {
              this.variableDataChapter_FormA.Chapters.Clear();
              this.VariableDataChapter_FormA = (VariableDataChapterFormA) null;
            }
            if (this.variableDataChapter_FormV != null)
              this.ClearVariableDataChapter_FormV();
            this.productsInfo[0].SetAttributeValue(AvsIDCache.Attr_ProductCode, (object) "Лев.", true);
            if (this.productsInfo.Count == 1)
              this.InsertNewProducts((IList<NewProductParams>) new NewProductParams[1]
              {
                new NewProductParams(-1L, 0, rightProductDesignation, "Прав.", 1)
              }, false);
            else if (this.productsInfo.Count > 1)
              this.productsInfo[1].SetAttributeValue(AvsIDCache.Attr_ProductCode, (object) "Прав.", true);
            if (this.avsWindow != null)
            {
              this.avsWindow.NeedToLoadColumnParams = true;
              this.avsWindow.LoadColumnsStateIfNeeded();
            }
            this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
            this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
            this.UpdateVariableDataCaptions();
            if (this.IsGridViewMode)
              this.RecreateTreeListNodes();
            if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
              this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
            this.UpdateProductsInStampForMirrorSP();
            this.UpdateProductLiteraForSP(true);
            break;
          case AVSDocumentForm.V:
            Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows5 = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
            List<AVSRow> rowList = new List<AVSRow>();
            if (avsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
            {
              this.variableDataChapter_FormA.GetAllRowsList(true, true, rowList);
              this.CheckErrorsInRows(AVSCheckType.EmptyCount, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) rowList, errorRows5);
            }
            this.CheckErrorsInRows(AVSCheckType.ObjectWithoutRelation, AVSCheckMode.ChangeForm, (ICollection<AVSRow>) null, errorRows5);
            if (errorRows5.Count > 0)
            {
              if (this.PromptForCountlessRows(errorRows5) != DialogResult.OK)
              {
                this.AvsDocumentForm = avsDocumentForm;
                this.DocumentTemplateID = documentTemplateId;
                this.documentTemplateGuid = documentTemplateGuid;
                return;
              }
              foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> keyValuePair in errorRows5)
                keyValuePair.Key.Section?.RemoveRow(keyValuePair.Key, true, true, true, false, false);
            }
            if (this.productsInfo.Count > 0 && !this.UseSameDesignationForProducts)
            {
              string number = this.productsInfo[0].GetNumber(this.DocumentDesignation, this.UseSameDesignationForProducts);
              if (number == null || number == "")
              {
                string productNumber = "";
                if (ProductNumberDlg.Execute(ref productNumber) == DialogResult.OK)
                {
                  this.productsInfo[0].SetNumber(productNumber, true);
                }
                else
                {
                  this.AvsDocumentForm = avsDocumentForm;
                  this.DocumentTemplateID = documentTemplateId;
                  this.documentTemplateGuid = documentTemplateGuid;
                  return;
                }
              }
            }
            try
            {
              this.AvsDocumentForm = value;
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                this.DocumentTemplateID = AVSDocumentsSettings.Instance.GetTemplate(this.avsDocTypeGuid, new AVSDocumentForm?(this.AvsDocumentForm), out this.documentTemplateGuid, sessionKeeper.Session, true);
                this.ResetSettingsFromTemplate();
              }
              template1 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
              if (template1 == null)
                throw new Exception("Шаблон групповой спецификации формы Б поврежден!");
              this.FindAllTemplates(template1, true);
            }
            catch
            {
              this.AvsDocumentForm = avsDocumentForm;
              this.DocumentTemplateID = documentTemplateId;
              this.documentTemplateGuid = documentTemplateGuid;
              this.FindAllTemplates(this.Document.DocumentTemplate, true);
            }
            if (this.IsGridViewMode)
              this.ClearTreeListNodes();
            this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
            this.Document.Clear(false, false);
            this.avsDocTable = (TableData) null;
            this.ClearVariableDataChapter_FormV();
            this.document.AssignDocumentTemplate(template1, true, false, true);
            this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
            this.document.ApplyTemplateProperties(false, false);
            this.document.CreateFirstPage();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
            this.FindMainTablesInDocument((ImDocumentData) this.document, out this.avsDocTable, out this.avsFormB_Table, out this.avsDocTableExpMix, out this.avsDocTableExpSingle, out this.avsDocTableExpMixP1, out this.avsDocTableExpSingleP2, out this.avsDocTableSingleT1, out this.avsDocTableSingleP2, out this.avsDocTableMixP1, out this.lriPage);
            this.TryAddMainTableIfNeed();
            this.CheckMainDocumentTablesAndThrowException();
            this.UpdateDocumentRowFieldsInfo();
            this.UpdateProductAttrs();
            this.LoadNewAttributes(this.docRowFields, false);
            if (avsDocumentForm != AVSDocumentForm.Single)
              this.UpdateDocumentStructure(true, false, true, true);
            if (this.variableDataChapter_FormA != null)
            {
              this.variableDataChapter_FormA.Chapters.Clear();
              this.VariableDataChapter_FormA = (VariableDataChapterFormA) null;
            }
            if (this.avsWindow != null)
            {
              this.avsWindow.NeedToLoadColumnParams = true;
              this.avsWindow.LoadColumnsStateIfNeeded();
            }
            this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
            if (this.IsGridViewMode)
              this.RecreateTreeListNodes();
            this.UpdateVariableDataCaptions();
            this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
            if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
              this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
            this.UpdateProductLiteraForSP(true);
            break;
          case AVSDocumentForm.G:
            throw new NotImplementedException("Групповые документы формы \"Г\" в этой версии не поддерживаются");
        }
        this.ResetAllSchemas();
        this.UpdateOutputAttributeMappingSettings();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        this.AvsDocumentForm = avsDocumentForm;
        this.DocumentTemplateID = documentTemplateId;
        this.documentTemplateGuid = documentTemplateGuid;
        ImDocumentData template3 = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(this.DocumentTemplateID);
        this.ResetSettingsFromTemplate();
        this.FindAllTemplates(template3, true);
        this.ExtractAuxiliaryPages(out extractedTitlePages, out extractedLriPage);
        this.Document.Clear(false, false);
        this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
        if (this.IsGridViewMode)
          this.ClearTreeListNodes();
        this.document.AssignDocumentTemplate(template3, true, false, true);
        this.document.SetAttributeValue(AVSDocument.SpecForm_DocAttribute, this.AvsDocumentForm.ToString(), false, false, false);
        this.document.ApplyTemplateProperties(false, false);
        this.document.CreateFirstPage();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.document, sessionKeeper.Session, true, true, false, false, false);
        this.avsDocTable = (TableData) this.document.FindNode("Таблица Спецификация");
        if (this.avsDocTable == null)
          throw new Exception($"В шаблоне спецификации \"{this.GetObjectCaption(this.DocumentTemplateID)}\" не найден объект \"Таблица Спецификация\". Замените или исправьте шаблон!");
        this.UpdateDocumentRowFieldsInfo();
        this.UpdateDocumentStructure(true, false, true, true);
        if (this.IsFormB && this.variableDataChapter_FormA != null)
        {
          this.variableDataChapter_FormA.Chapters.Clear();
          this.VariableDataChapter_FormA = (VariableDataChapterFormA) null;
        }
        if (this.avsWindow != null)
        {
          this.avsWindow.NeedToLoadColumnParams = true;
          this.avsWindow.LoadColumnsStateIfNeeded();
        }
        this.UpdateViewNodes(true, true, false, true, true, EmptyRowUpdateMode.Delete);
        if (this.IsGridViewMode)
          this.RecreateTreeListNodes();
        this.UpdateVariableDataCaptions();
        this.RestoreAuxiliaryPages(extractedTitlePages, extractedLriPage);
        if (this.DocumentControl != null && this.document.NodesCount > 0 && this.document.Nodes[0] is Page)
          this.DocumentControl.ActivePage = this.document.Nodes[0] as Page;
        this.UpdateProductLiteraForSP(true);
      }
      if (this.materialKeyWordsSchema == null)
        this.document?.SetMaterialKeyWords((List<string>) this.MaterialKeyWordsSchema?.KeyWords);
      if (!this.IsSpecification || this.ReadOnly)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.articleGroupID == Guid.Empty)
        {
          this.articleGroupID = Guid.NewGuid();
          sessionKeeper.Session.GetObject(this.productsInfo[0].Id).SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.articleGroupID)
          });
        }
        AvsIDCache.GetDBAVSDocumentObject(sessionKeeper.Session, this.DocumentID).SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_SpecificationForm, (object) this.EncodeSpecificationFormAttrValue(this.AvsDocumentForm))
        }, true);
      }
      this.SaveAVSDocumentToDB();
    }
    finally
    {
      this.Document.IsDocumentLoading = isDocumentLoading;
      this.Unlock_DocCell_TextChanged();
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  /// <summary>Сброс всех схем</summary>
  private void ResetAllSchemas() => this.designationTrimSchema = (DesignationTrimSchema) null;

  /// <summary>
  /// Получить список атрибутов исполнений для вывода в документ и загрузить их
  /// </summary>
  private void UpdateProductAttrs()
  {
    List<int> intList = new List<int>();
    this.GetProductAttrsInfoForDocument(intList);
    if (intList.Count <= 0)
      return;
    this.UpdateProductsByGroupID(intList, (string) null);
    foreach (int num in intList)
    {
      if (!this.productAttributeList.Contains(num))
        this.productAttributeList.Add(num);
    }
  }

  private void RestoreAuxiliaryPages(List<PageData> oldTitlePages, PageData oldLriPage)
  {
    List<PageData> extractedTitlePages;
    this.ExtractAuxiliaryPages(out extractedTitlePages, out PageData _);
    int index1 = 0;
    if (oldTitlePages != null)
    {
      for (int index2 = 0; index2 < oldTitlePages.Count; ++index2)
      {
        if (this.Document.Template.FindNode(oldTitlePages[index2].TemplateId) != null)
        {
          this.Document.InsertChildNode(index1, (DocumentTreeNode) oldTitlePages[index2], false, false, false, false, false);
          oldTitlePages[index2].ApplyTreeTemplates(false, false);
          ++index1;
        }
      }
    }
    if (index1 == 0 && extractedTitlePages != null)
    {
      for (int index3 = 0; index3 < extractedTitlePages.Count; ++index3)
        this.Document.InsertChildNode(index3, (DocumentTreeNode) extractedTitlePages[index3], false, false, false, false, false);
    }
    if (oldLriPage == null)
      return;
    this.Document.AddChildNode((DocumentTreeNode) oldLriPage, false, false);
    oldLriPage.ApplyTreeTemplates(false, false);
    this.lriPage = oldLriPage;
  }

  /// <summary>
  /// Удалить из документа вспомогательные страницы типа Титульных листов и Листа регистрации изменений
  /// </summary>
  private void ExtractAuxiliaryPages(
    out List<PageData> extractedTitlePages,
    out PageData extractedLriPage)
  {
    extractedLriPage = this.RemoveLRIPage();
    extractedTitlePages = this.RemoveTitlePages();
  }

  /// <summary>Изъять все ТЛ из конструкторского документа</summary>
  private List<PageData> RemoveTitlePages()
  {
    List<PageData> titlePages = AVSDocument.FindTitlePages((ImDocumentData) this.document);
    foreach (PageData pageData in titlePages)
      pageData.RemovePageWithDataFlow(false, false);
    return titlePages;
  }

  private DialogResult PromptForCountlessRows(
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    return IMMessageBox.Show("AVS", $"В спецификации содержатся записи без количеств с перечисленными ниже объектами.\r\nДля преобразования формы {(this.AvsDocumentForm == AVSDocumentForm.Single ? "единичного" : "группового")} документа эти записи необходимо удалить, либо, перед преобразованием, заполнить графу \"Количество\".\r\n\r\nУдалить незаполненные записи?", MessageBoxButtons.OKCancel, (IList<string>) errorRows.Select<KeyValuePair<AVSRow, List<SpecRowCheckMessage>>, string>((System.Func<KeyValuePair<AVSRow, List<SpecRowCheckMessage>>, string>) (er => er.Key.ObjCaption)).ToList<string>());
  }

  /// <summary>Извлечь ЛРИ из конструкторского документа</summary>
  /// <returns>Возвращает удалённый ЛРИ</returns>
  private PageData RemoveLRIPage()
  {
    PageData lriPage = this.lriPage;
    if (lriPage != null)
    {
      if (lriPage.FindFirstNodeFromTemplate_Recursive("Таблица изменений") is TableData templateRecursive)
        templateRecursive.UniteTable();
      lriPage.Remove(true, false, false);
    }
    return lriPage;
  }

  /// <summary>Преобразовать значение типа SpecificationForm в допустимое значение атрибута "Форма спецификации"</summary>
  /// <param name="attrValue">Значение типа SpecificationForm</param>
  /// <returns></returns>
  public string EncodeSpecificationFormAttrValue(AVSDocumentForm attrValue)
  {
    return SpecificationFormMethods.EncodeSpecificationFormAttrValue(attrValue);
  }

  /// <summary>Удалить пустые разделы и переменные данные</summary>
  /// <param name="keepWithDocNode">Сохранить разделы, если для них есть элементы в документе</param>
  public void RemoveEmptySections(bool keepWithDocNode)
  {
    this.SuspendDocumentAndGridUpdates();
    try
    {
      for (int index = 0; index < this.rootChapters.Count; ++index)
        this.rootChapters[index].RemoveEmptySections(keepWithDocNode);
      this.UpdateVariableDataCaptions();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      if (this.DocumentControl != null)
        this.DocumentControl.UnselectRemovedNodes();
    }
  }

  /// <summary>Получает идентификаторы всех исполнений группового изделия</summary>
  /// <param name="articleID">Идентификатор одного из исполнений группового изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Массив идентификаторов исполнений</returns>
  public static long[] FindArticlesByGroupID(
    long articleID,
    string filtrationRuleSettings,
    IUserSession userSession)
  {
    if (AVSPlugin.IArticleService != null)
      return AVSPlugin.IArticleService.FindArticlesByGroupID(articleID, (object) userSession);
    List<long> longList = new List<long>();
    if (userSession != null)
    {
      IDBObject dbObject = userSession.GetObject(articleID);
      string conditionValue = string.Empty;
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
      if (attributeById != null && attributeById.Value != null && !(attributeById.Value is DBNull))
        conditionValue = attributeById.AsString;
      if (!string.IsNullOrEmpty(conditionValue))
      {
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
        };
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(dbObject.ObjectType);
        objectCollection.ShowAllModifications = true;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, AvsConfig.General.SelectProductsWithCaseSensitive),
          new ConditionStructure(-9, RelationalOperators.NotEqual, (object) userSession.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
        }, columns);
        AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
          longList.Add(Convert.ToInt64(row[0]));
      }
    }
    return longList.ToArray();
  }

  /// <summary>Проверить соответствие обозначения изделия и спецификации</summary>
  /// <param name="productDesignation">Обозначение изделия</param>
  /// <param name="docDesignation">Обозначение спецификации</param>
  /// <returns></returns>
  public static bool IsSpecificationForThisAssembly(
    string productDesignation,
    string docDesignation)
  {
    if (productDesignation == null)
      throw new ArgumentNullException(nameof (productDesignation));
    return docDesignation != null ? productDesignation.Contains(docDesignation) : throw new ArgumentNullException(nameof (docDesignation));
  }

  /// <summary>Является ли данный документ с данным обозначением спецификацией для изделия</summary>
  /// <param name="docID">Идентификатор документа. Если -1, то проверяется обозначение</param>
  /// <param name="docDesignation">Обозначение документа</param>
  /// <returns></returns>
  public bool IsSpecificationForThisAssembly(long docID, string docDesignation)
  {
    return !this.DocumentID.IsUndefinedId() && this.DocumentID == docID || docDesignation != null && docDesignation != "" && this.DocumentDesignation != null && this.DocumentDesignation != "" && AVSDocument.IsSpecificationForThisAssembly(this.DocumentDesignation, docDesignation);
  }

  /// <summary>Проверить является ли тип child дочерним для типа parent</summary>
  /// <param name="parent">Родительский тип</param>
  /// <param name="child">Дочерний тип</param>
  /// <returns>true, если parent является родительским типом для child</returns>
  public static bool IsParentObjectType(int parent, int child)
  {
    return parent == child || MetaDataHelper.IsObjectTypeChildOf(child, parent);
  }

  /// <summary>На объект заданного типа можно выпустить спецификацию</summary>
  /// <param name="dbObjectType">Идентификатор типа объекта</param>
  public static bool IsProductForSpecification(int dbObjectType)
  {
    if (AVSDocument.IsProductForSpecification2(dbObjectType))
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return AVSDocument.IsProductForSpecification3(dbObjectType, sessionKeeper.Session);
  }

  /// <summary>На объект заданного типа можно выпустить спецификацию.
  /// Проверяет только Комплекты, комплексы и сборочные единицы</summary>
  /// <param name="dbObjectType">Идентификатор типа объекта</param>
  public static bool IsProductForSpecification2(int dbObjectType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_AssemblyUnit) || MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_Complect) || MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_Complex);
  }

  /// <summary>На объект заданного типа можно выпустить спецификацию</summary>
  /// <param name="dbObjectType">Идентификатор типа объекта</param>
  /// <param name="session">Пользовательская сессия</param>
  public static bool IsProductForSpecification(int dbObjectType, IUserSession session)
  {
    return AVSDocument.IsProductForSpecification2(dbObjectType) || AVSDocument.IsProductForSpecification3(dbObjectType, session);
  }

  /// <summary>На объект заданного типа можно выпустить спецификацию</summary>
  /// <param name="dbObjectType">Идентификатор типа объекта</param>
  /// <param name="session">Пользовательская сессия</param>
  public static bool IsProductForSpecification3(int dbObjectType, IUserSession session)
  {
    return session.GetRelationsApplicabilityCollection().GetApplicability(AvsIDCache.Relation_Document, AvsIDCache.ObjType_Specification, dbObjectType) != null;
  }

  /// <summary>Получить список типов объектов на которые может выпускаться спецификация</summary>
  /// <returns></returns>
  public static List<int> GetProductTypesForSpecification(IUserSession session)
  {
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(AvsIDCache.Relation_Document, AvsIDCache.ObjType_Specification, -1);
    List<int> forSpecification = new List<int>();
    if (applicabilitiesList != null)
    {
      for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(applicabilitiesList.Rows[index]["F_INOBJECT_TYPE"]);
        forSpecification.Add(int32);
      }
      applicabilitiesList.Dispose();
    }
    return forSpecification;
  }

  /// <summary>Получить все записи документа одним списком</summary>
  /// <param name="onlyRelations">Получить только записи со связями</param>
  /// <param name="onlyObjects">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают и информационные записи примечания</param>
  /// <returns>Список записей спецификации</returns>
  public List<AVSRow> GetAllRows(bool onlyRelations, bool onlyObjects)
  {
    List<AVSRow> rowList = new List<AVSRow>();
    for (int index = 0; index < this.rootChapters.Count; ++index)
      this.rootChapters[index].GetAllRowsList(onlyRelations, onlyObjects, rowList);
    return rowList;
  }

  /// <summary>Получить энумератор для всех записей документа</summary>
  /// <param name="withRelationsOnly">Получить только записи со связями</param>
  /// <param name="withObjectsOnly">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают и информационные записи примечания</param>
  /// <returns>Список записей спецификации</returns>
  public IEnumerable<AVSRow> GetRows(bool withRelationsOnly = false, bool withObjectsOnly = false)
  {
    foreach (Chapter rootChapter in this.rootChapters)
    {
      foreach (AVSRow row in rootChapter.GetRows(withRelationsOnly, withObjectsOnly))
        yield return row;
    }
  }

  private List<Chapter> RootChapters => this.rootChapters;

  /// <summary>Получить все разделы документа</summary>
  /// <returns></returns>
  public List<Chapter> GetAllChapters()
  {
    List<Chapter> allChapters = new List<Chapter>();
    for (int index = 0; index < this.rootChapters.Count; ++index)
      allChapters.AddRange((IEnumerable<Chapter>) this.rootChapters[index].GetAllChapters());
    return allChapters;
  }

  /// <summary>Получить перечисление всех разделов документа</summary>
  /// <returns></returns>
  public IEnumerable<Chapter> GetChaptersEnumerator()
  {
    foreach (Chapter chapter1 in this.rootChapters)
    {
      yield return chapter1;
      foreach (Chapter chapter2 in chapter1.GetChaptersEnumerator())
        yield return chapter2;
    }
  }

  /// <summary>Получить список допустимых дочерних типов, которые могут быть добавлены в данный раздел</summary>
  /// <param name="parentType">Родительский тип</param>
  /// <param name="section">Раздел</param>
  /// <returns>список допустимых типов, null если ограничений нет</returns>
  public List<int> GetApplicabilityTypes(int parentType, SpecificationSection section)
  {
    if (parentType != AvsIDCache.ObjType_Document)
      return (List<int>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(AvsIDCache.Relation_Document, -1, this.ProductType);
      List<int> collection = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
      {
        object obj1 = row[applicabilitiesList.Columns.IndexOf("F_OBJECT_TYPE")];
        object obj2 = row[applicabilitiesList.Columns.IndexOf("F_RELATION_TYPE")];
        switch (obj1)
        {
          case int num:
            collection.Add(num);
            continue;
          case Decimal _:
          case string _:
          case long _:
            collection.Add(Convert.ToInt32(obj1));
            continue;
          default:
            continue;
        }
      }
      List<int> applicabilityTypes = new List<int>();
      applicabilityTypes.AddRange((IEnumerable<int>) collection);
      foreach (int childTypeID in collection)
      {
        for (int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(childTypeID); objectTypeParentId > 0; objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId))
        {
          if (collection.Contains(objectTypeParentId))
            applicabilityTypes.Remove(childTypeID);
        }
      }
      return applicabilityTypes;
    }
  }

  /// <summary>Спецификация пуста</summary>
  public bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      bool isEmpty = true;
      for (int index = 0; isEmpty && index < this.rootChapters.Count; ++index)
        isEmpty &= this.rootChapters[index].IsEmpty;
      return isEmpty;
    }
  }

  /// <summary>Допустим ли выпуск группового документа</summary>
  public bool AllowableGroupDocument
  {
    [DebuggerStepThrough] get
    {
      return !this.IsSpecification || this.productType == -1 || !MetaDataHelper.IsObjectTypeChildOf(this.productType, AvsIDCache.ObjType_Orders);
    }
  }

  /// <summary>Создать раздел спецификации</summary>
  /// <param name="sectionId">Идентификатор раздела. Если -1, то специальный раздел "Раздел не назначен"</param>
  /// <returns>Раздел спецификации</returns>
  public SpecificationSection CreateSection(long sectionId)
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    return this.CreateSection(SpecificationSectionInfo.FindSectionById(sectionId));
  }

  /// <summary>Создать раздел спецификации</summary>
  /// <param name="sectionInfo">Информация о разделе</param>
  /// <returns>Раздел спецификации</returns>
  public SpecificationSection CreateSection(SpecificationSectionInfo sectionInfo)
  {
    SpecificationSection section = sectionInfo == null ? new SpecificationSection(this, AVSDocument.SectionUnassignedGuid, -1L, -1, "Раздел не назначен", long.MaxValue, new int[0]) : new SpecificationSection(this, sectionInfo);
    if (this.SortSchema != null)
      section.SectionSortSchema = this.SortSchema.GetSectionSchemaBySectionGuid(DBHelper.GetObjGuidByID(section.SectionID));
    return section;
  }

  /// <summary>Создать раздел для общих данных</summary>
  /// <param name="useSectionType">Использовать тип SpecificationSection</param>
  /// <returns></returns>
  internal Chapter CreateCommonDataChapter(bool useSectionType)
  {
    Chapter commonDataChapter = !useSectionType ? new Chapter(this, true) : (Chapter) new SpecificationSection(this, new SpecificationSectionInfo(AVSDocument.ChapterCommonDataGuid, -1L, -1, "Общие данные", 0L, "", new int[0], new long[0]));
    commonDataChapter.Product = new ProductInfo(AVSDocument.ChapterCommonDataGuid, -1L, "Общие данные");
    commonDataChapter.ChapterGuid = AVSDocument.ChapterCommonDataGuid;
    commonDataChapter.Caption = "Общие данные";
    commonDataChapter.NodeLevel = Chapter.CommonData_TypeName;
    return commonDataChapter;
  }

  /// <summary>Получить часть для узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <param name="ignoreSections">Игнорировать разделы типа SpecificationSection</param>
  /// <returns>Часть</returns>
  public Chapter GetChapter(DocumentTreeNode docNode, bool ignoreSections)
  {
    if (docNode == null)
      return (Chapter) null;
    chapter = (Chapter) null;
    if (AVSDocument.FindParentChapterDocNode(docNode, ignoreSections) is TableData parentChapterDocNode && parentChapterDocNode.FindFirstTable().Tag is Chapter chapter && chapter.UseParentDocNode)
      chapter = chapter.Parent;
    return chapter;
  }

  public Chapter GetChapter(Guid chapterGuid)
  {
    foreach (Chapter rootChapter in this.rootChapters)
    {
      if (rootChapter.ChapterGuid == chapterGuid)
        return rootChapter;
    }
    return (Chapter) null;
  }

  /// <summary>Получить часть для узла документа, пригодную для вставки раздела СП</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Часть</returns>
  public Chapter GetChapterForSection(DocumentTreeNode docNode)
  {
    if (docNode == null)
      return (Chapter) null;
    chapterForSection = (Chapter) null;
    tableData = (TableData) null;
    if (docNode is Page)
      tableData = this.FindMainDocTableFromPage((PageData) (docNode as Page), false);
    if (tableData == null)
      tableData = AVSDocument.FindParentChapterOnDocNodePage(docNode, true) as TableData;
    if (tableData == null)
      tableData = docNode as TableData;
    if (tableData != null)
    {
      if (tableData.Tag is Chapter tag && tag.IsVariableDataChapter)
        return tag;
      TableData firstTable = tableData.FindFirstTable();
      if (tableData.IsTopLevelTable || tag != null && !tag.IsSectionOwner)
      {
        while (tableData != null && tableData.Nodes.Count > 0 && (tag == null || !tag.IsSectionOwner))
        {
          if (tableData.Nodes[tableData.Nodes.Count - 1] is TableData tableData)
            tag = tableData.Tag as Chapter;
          if (tag != null && tag is SpecificationSection)
            return tag.Parent;
        }
      }
      if (tag != null)
        return tag;
      if (tableData != null)
        firstTable = tableData.FindFirstTable();
      if (firstTable.Tag is Chapter chapterForSection && chapterForSection.UseParentDocNode)
        chapterForSection = chapterForSection.Parent;
    }
    return chapterForSection;
  }

  /// <summary>Получить DocNode связанный с TreeNode</summary>
  /// <param name="treeListNode"></param>
  /// <returns></returns>
  public DocumentTreeNode GetDocNode(IVirtualTreeItem item)
  {
    switch (item)
    {
      case null:
        throw new ArgumentNullException(nameof (item));
      case DocumentTreeNode _:
        return item as DocumentTreeNode;
      case Chapter _:
        return (DocumentTreeNode) (item as Chapter).DocNode;
      case AVSRow _:
        return (DocumentTreeNode) (item as AVSRow).DocNode;
      default:
        return (DocumentTreeNode) null;
    }
  }

  /// <summary>Получить часть для строки табличного вида</summary>
  /// <param name="treeListNode">Строка табличного вида</param>
  /// <returns>Часть</returns>
  public Chapter GetChapter(Row treeListNode)
  {
    Chapter chapter = treeListNode != null ? treeListNode.Item as Chapter : throw new ArgumentNullException(nameof (treeListNode));
    while (chapter == null && treeListNode != null)
    {
      treeListNode = treeListNode.ParentRow;
      if (treeListNode != null)
        chapter = treeListNode.Item as Chapter;
    }
    return chapter;
  }

  /// <summary>Получить части спецификации из узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <param name="сhapters">Коллекция найденных частей спецификации</param>
  /// <param name="ignoreSections">Игнорировать разделы типа SpecificationSection</param>
  public void GetChapters(DocumentTreeNode docNode, List<Chapter> сhapters, bool ignoreSections)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (сhapters == null)
      throw new ArgumentNullException("chapters");
    if (docNode.IsVirtualNode)
    {
      for (int index = 0; docNode.Nodes != null && index < docNode.Nodes.Count; ++index)
        this.GetChapters(docNode.Nodes[index], сhapters, ignoreSections);
    }
    else
    {
      Chapter chapter = this.GetChapter(docNode, ignoreSections);
      if (chapter == null || сhapters.Contains(chapter))
        return;
      сhapters.Add(chapter);
    }
  }

  /// <summary>Получить часть, раздел и исполнения для контекста</summary>
  /// <param name="contextNode">Узел контекста</param>
  /// <param name="useCommonDataChapterAsDefault">Использовать "Общие данные" как контекст по умолчанию</param>
  /// <returns></returns>
  public AVSDocumentContext GetContextChapters(
    DocumentTreeNode contextNode,
    bool useCommonDataChapterAsDefault = false)
  {
    if (contextNode == null)
      return new AVSDocumentContext();
    AVSDocumentContext contextChapters1 = new AVSDocumentContext(false, (SpecificationSection) null, this.GetAllowableDocumentSections());
    if (contextNode is Page)
    {
      DocumentTreeNode docTableFromPage = (DocumentTreeNode) this.FindMainDocTableFromPage((PageData) (contextNode as Page), false);
      if (docTableFromPage != null)
        contextNode = docTableFromPage;
    }
    contextChapters1.Row = this.GetAvsDocRow(contextNode);
    if (contextChapters1.Row != null)
    {
      if (contextChapters1.Row.HasRelation)
      {
        if (contextChapters1.Row.IsFormB && contextChapters1.Row.Relations.Count > 1)
        {
          if (contextNode is TextData cell)
          {
            int productIndex;
            contextChapters1.Row.GetAttributeInfoForCell(cell, out productIndex);
            if (productIndex != -1 && productIndex < this.productsInfo.Count)
            {
              int relationIndexForProduct = contextChapters1.Row.GetRelationIndexForProduct(this.productsInfo[productIndex].Id);
              if (relationIndexForProduct != -1)
                contextChapters1.RelationID = contextChapters1.Row.Relations[relationIndexForProduct].RelationId;
            }
          }
        }
        else
          contextChapters1.RelationID = contextChapters1.Row.Relations[0].RelationId;
      }
      contextChapters1.RowIndex = contextChapters1.Row.Index;
    }
    contextChapters1.FirstProductIndexInBlock = Chapter.GetFirstProductIndexForDocChapter(contextNode);
    contextChapters1.Section = this.GetSection(contextNode);
    if (contextChapters1.Section == null && !this.IsSpecification)
      contextChapters1.Section = this.commonDataChapter as SpecificationSection;
    contextChapters1.Chapter = this.GetChapterForSection(contextNode);
    if (contextChapters1.Chapter == null && contextChapters1.Section != null)
      contextChapters1.Chapter = !contextChapters1.Section.IsCommonDataChapter ? contextChapters1.Section.Parent : (Chapter) contextChapters1.Section;
    if (contextChapters1.Chapter == null)
    {
      if (!(contextNode is PageData page) && contextNode is PageElementNode pageElementNode)
        page = pageElementNode.Page;
      if (page != null)
      {
        TableData docTableFromPage = this.FindMainDocTableFromPage(page, false);
        contextChapters1.Chapter = this.GetChapter((DocumentTreeNode) docTableFromPage, true);
      }
    }
    contextChapters1.Products = new List<ProductInfo>();
    for (; contextChapters1.Chapter != null; contextChapters1.Chapter = contextChapters1.Chapter.Parent)
    {
      if (contextChapters1.Chapter.IsCommonDataChapter || contextChapters1.Chapter.IsVariableDataChapter || contextChapters1.Chapter.IsAdditionalChapter || contextChapters1.Chapter.IsProductVariableDataChapter)
      {
        contextChapters1.Products.Add(contextChapters1.Chapter.Product);
        break;
      }
    }
    if (useCommonDataChapterAsDefault && contextChapters1.Chapter == null && this.commonDataChapter != null && this.commonDataChapter.DocNode != null)
    {
      AVSDocumentContext contextChapters2 = this.GetContextChapters((DocumentTreeNode) this.commonDataChapter.DocNode);
      contextChapters1.Chapter = contextChapters2.Chapter;
    }
    return contextChapters1;
  }

  /// <summary>Получить раздел спецификации по узлу TreeList</summary>
  /// <param name="treeListNode">Узел документа</param>
  /// <returns>Раздел спецификации</returns>
  public SpecificationSection GetSection(Row treeListNode)
  {
    if (treeListNode == null)
      throw new ArgumentNullException(nameof (treeListNode));
    if (!(treeListNode.Item is SpecificationSection section))
    {
      if (treeListNode.Item is AVSRow avsRow)
        section = avsRow.Section;
      if (section != null)
        return section;
      Chapter chapter = this.GetChapter(treeListNode);
      if (chapter != null && chapter.Chapters.Count == 1 && chapter.Chapters[0].UseParentDocNode)
        section = chapter.Chapters[0] as SpecificationSection;
    }
    return section;
  }

  /// <summary>Получить исполнение спецификации по узлу TreeList</summary>
  /// <param name="treeListNode">Узел документа</param>
  /// <returns>Раздел спецификации</returns>
  public ProductInfo GetProduct(IVirtualTreeItem treeListNode)
  {
    switch (treeListNode)
    {
      case null:
        throw new ArgumentNullException(nameof (treeListNode));
      case ProductVariableDataChapter variableDataChapter:
        return variableDataChapter.Product;
      case SpecificationSection specificationSection:
        return specificationSection.Product;
      case AVSRow avsRow:
        return avsRow.Product;
      default:
        if (this.avsWindow != null)
        {
          Chapter chapter = this.GetChapter(this.avsWindow.virtualTree.FindRow((object) treeListNode));
          if (chapter != null)
            return chapter.Product;
        }
        return (ProductInfo) null;
    }
  }

  /// <summary>Получить раздел спецификации по узлу документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Раздел спецификации</returns>
  public SpecificationSection GetSection(DocumentTreeNode docNode)
  {
    TableData tableData1 = docNode != null ? AVSDocument.FindParentSpecSectionDocNode(docNode) as TableData : throw new ArgumentNullException(nameof (docNode));
    SpecificationSection section = (SpecificationSection) null;
    if (tableData1 != null)
      section = tableData1.FindFirstTable().Tag as SpecificationSection;
    if (section == null)
    {
      Chapter chapter = this.GetChapter(docNode, true);
      if (chapter != null && chapter.Chapters.Count == 1 && chapter.Chapters[0].UseParentDocNode)
        section = chapter.Chapters[0] as SpecificationSection;
      if (section != null || !(docNode is TableData tableData2) || !tableData2.IsColumn || !this.IsSpecificationTable(tableData2.TopLevelTable))
        return section;
      for (int index = tableData2.Nodes.Count - 1; index >= 0; --index)
      {
        section = this.GetSection2(tableData2.Nodes[index]);
        if (section != null)
          break;
      }
    }
    return section;
  }

  /// <summary>Получить разделы спецификации из узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <param name="sections">Коллекция найденных разделов спецификации</param>
  public void GetSections(DocumentTreeNode docNode, List<SpecificationSection> sections)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (sections == null)
      throw new ArgumentNullException(nameof (sections));
    if (docNode.IsVirtualNode)
    {
      for (int index = 0; docNode.Nodes != null && index < docNode.Nodes.Count; ++index)
        this.GetSections(docNode.Nodes[index], sections);
    }
    else
    {
      SpecificationSection section = this.GetSection(docNode);
      if (section == null || sections.Contains(section))
        return;
      sections.Add(section);
    }
  }

  /// <summary>Получить раздел спецификации по узлу документа.
  /// Вспомогательный метод только для вызова из GetSection. Исключены лишние проверки.</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Раздел спецификации</returns>
  private SpecificationSection GetSection2(DocumentTreeNode docNode)
  {
    TableData tableData = docNode != null ? docNode as TableData : throw new ArgumentNullException(nameof (docNode));
    section2 = (SpecificationSection) null;
    if (tableData != null && tableData.IsColumn)
    {
      if (!(tableData.Tag is SpecificationSection section2))
        section2 = tableData.FindFirstTable().Tag as SpecificationSection;
      if (section2 == null)
      {
        for (int index = tableData.Nodes.Count - 1; index >= 0; --index)
        {
          section2 = this.GetSection2(tableData.Nodes[index]);
          if (section2 != null)
            break;
        }
      }
    }
    return section2;
  }

  /// <summary>Является ли данная таблица - таблицей спецификации, или её продолжением</summary>
  /// <param name="table">Таблица</param>
  public bool IsSpecificationTable(TableData table) => this.IsSpecificationTable(table, out bool _);

  /// <summary>Является ли данная таблица - таблицей экспортной части спецификации, или её продолжением</summary>
  /// <param name="table">Таблица</param>
  public bool IsExportSpecificationTable(TableData table)
  {
    if (table == null)
      return false;
    TableData firstTable = table.FindFirstTable();
    return firstTable.GetAttributeValue(AVSDocument.AVSTableType_DocAttribute, false) == "ExportTable" || this.avsDocTableExpMix != null && firstTable == this.avsDocTableExpMix || firstTable.Template != null && firstTable.Template == this.avsDocTableExpMix_Template;
  }

  /// <summary>Является ли данная таблица - таблицей спецификации, или её продолжением</summary>
  /// <param name="table">Таблица</param>
  /// <param name="varDataFormV">Таблица переменных данных формы В</param>
  internal bool IsSpecificationTable(TableData table, out bool varDataFormV)
  {
    varDataFormV = false;
    if (table == null || this.avsDocTable == null)
      return false;
    TableData firstTable = table.FindFirstTable();
    if (firstTable == this.avsDocTable || firstTable.Template != null && firstTable.Template == this.avsDocTableTemplate)
      return true;
    if (firstTable == this.avsFormB_Table || firstTable.Template != null && (firstTable.Template == this.avsDocTableFormBForV_Template || firstTable.Template == this.avsDocTableFormBMore10_Template))
    {
      varDataFormV = this.AvsDocumentForm == AVSDocumentForm.V;
      return true;
    }
    return firstTable.GetAttributeValue(AVSDocument.AVSTableType_DocAttribute, false) == "MainTable" || firstTable.Template != null && firstTable.TemplateId == "Таблица Спецификация. Продолжение";
  }

  protected virtual bool IsAllowableObjectType(int objectType) => true;

  /// <summary>Получить раздел по умолчанию для заданного типа</summary>
  /// <remarks>Если тип допустим в нескольких разделах, то выбирается согласно приоритету:
  /// 1. Тип в списке допустимых задан явно и является первым
  /// 2. Тип в списке допустимых задан явно
  /// 3. Тип в списке допустимых задан родительским типом</remarks>
  /// <param name="_objType">Тип объекта</param>
  /// <returns>Информация о разделе спецификации</returns>
  public static SpecificationSectionInfo GetDefaultSectionForType(int objType)
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    List<SpecificationSectionInfo> sections = SpecificationSectionInfo.Sections;
    SpecificationSectionInfo specificationSectionInfo1 = (SpecificationSectionInfo) null;
    SpecificationSectionInfo specificationSectionInfo2 = (SpecificationSectionInfo) null;
    SpecificationSectionInfo specificationSectionInfo3 = (SpecificationSectionInfo) null;
    for (int index1 = 0; index1 < sections.Count; ++index1)
    {
      SpecificationSectionInfo specificationSectionInfo4 = sections[index1];
      if (specificationSectionInfo4.PartTypes != null)
      {
        for (int index2 = 0; (specificationSectionInfo2 == null || specificationSectionInfo3 == null) && index2 < specificationSectionInfo4.PartTypes.Length; ++index2)
        {
          if (specificationSectionInfo4.PartTypes[index2] == objType)
          {
            if (index1 == 0)
            {
              specificationSectionInfo1 = specificationSectionInfo4;
              break;
            }
            if (specificationSectionInfo2 == null)
              specificationSectionInfo2 = specificationSectionInfo4;
          }
          else if (specificationSectionInfo3 == null && AVSDocument.IsParentObjectType(specificationSectionInfo4.PartTypes[index2], objType))
            specificationSectionInfo3 = specificationSectionInfo4;
        }
      }
      if (specificationSectionInfo1 != null)
        break;
    }
    return specificationSectionInfo1 ?? specificationSectionInfo2 ?? specificationSectionInfo3;
  }

  /// <summary>Получить раздел по умолчанию для заданного типа</summary>
  /// <remarks>Если тип допустим в нескольких разделах, то выбирается согласно приоритету:
  /// 1. Тип в списке допустимых задан явно и является первым
  /// 2. Тип в списке допустимых задан явно
  /// 3. Тип в списке допустимых задан родительским типом</remarks>
  /// <param name="_objType">Тип объекта</param>
  /// <param name="contextSection">Раздел, в контексте которого добавляется изделие</param>
  /// <param name="sections">Допустимые разделы</param>
  /// <returns>Информация о разделе спецификации</returns>
  public static SpecificationSectionInfo GetDefaultSectionForType(
    int objType,
    long contextSection,
    List<SpecificationSectionInfo> sections)
  {
    SpecificationSectionInfo defaultSectionForType = (SpecificationSectionInfo) null;
    SpecificationSectionInfo specificationSectionInfo1 = (SpecificationSectionInfo) null;
    SpecificationSectionInfo specificationSectionInfo2 = (SpecificationSectionInfo) null;
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    if (contextSection != -1L)
      defaultSectionForType = SpecificationSectionInfo.SectionDictionaryByID[(object) contextSection] as SpecificationSectionInfo;
    if (defaultSectionForType != null)
    {
      if (objType == -1)
        return defaultSectionForType;
      for (int index = 0; index < defaultSectionForType.PartTypes.Length; ++index)
      {
        if (defaultSectionForType.PartTypes[index] == objType || AVSDocument.IsParentObjectType(defaultSectionForType.PartTypes[index], objType))
          return defaultSectionForType;
      }
    }
    if (objType == -1)
      return (SpecificationSectionInfo) null;
    if (sections == null)
      sections = SpecificationSectionInfo.Sections;
    bool flag1 = MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_OperationDocumentsSheet) || MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_GeneralRepairDocumentsSheet) || MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_MediumRepairDocumentsSheet);
    bool flag2 = !flag1 && MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_ConstructorDocument);
    for (int index1 = 0; index1 < sections.Count; ++index1)
    {
      SpecificationSectionInfo section = sections[index1];
      if (section.PartTypes != null)
      {
        for (int index2 = 0; index2 < section.PartTypes.Length; ++index2)
        {
          if (AVSDocument.IsParentObjectType(section.PartTypes[index2], objType))
          {
            if (flag1)
            {
              if (section.SectionGuid == SpecificationSectionInfo.ComplectSectionGuid)
                specificationSectionInfo1 = section;
            }
            else if (flag2)
            {
              if (section.SectionGuid == SpecificationSectionInfo.DocumentSectionGuid)
                specificationSectionInfo1 = section;
            }
            else if (index2 == 0)
              specificationSectionInfo1 = section;
            specificationSectionInfo2 = section;
            break;
          }
        }
      }
      if (specificationSectionInfo1 != null)
        break;
    }
    return specificationSectionInfo1 ?? specificationSectionInfo2;
  }

  /// <summary>Получить раздел по умолчанию для заданного объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="razdelSP">Значение атрибута РАЗДЕЛ СП</param>
  /// <param name="contextSection">Ид раздела контекста</param>
  /// <param name="sections">Допустимые разделы</param>
  public static SpecificationSectionInfo GetDefaultSectionForObject(
    int objectType,
    string razdelSP,
    long contextSection,
    List<SpecificationSectionInfo> sections)
  {
    SpecificationSectionInfo sectionForObject = (SpecificationSectionInfo) null;
    if (contextSection.IsUndefinedId() && !string.IsNullOrEmpty(razdelSP))
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      SpecificationSectionInfo specificationSectionInfo;
      SpecificationSectionInfo.SectionDictionaryByRazdelSP.TryGetValue(razdelSP, out specificationSectionInfo);
      if (specificationSectionInfo != null)
        sectionForObject = specificationSectionInfo;
    }
    if (sectionForObject == null)
      sectionForObject = AVSDocument.GetDefaultSectionForType(objectType, contextSection, sections);
    return sectionForObject;
  }

  /// <summary>Получить раздел по умолчанию для заданного объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="razdelSP">Значение атрибута РАЗДЕЛ СП</param>
  /// <param name="contextSection">Ид раздела контекста</param>
  /// <param name="sections">Допустимые разделы</param>
  public static long GetDefaultSectionIdForObject(
    int objectType,
    string razdelSP,
    long contextSection,
    List<SpecificationSectionInfo> sections)
  {
    SpecificationSectionInfo sectionForObject = AVSDocument.GetDefaultSectionForObject(objectType, razdelSP, contextSection, sections);
    return sectionForObject != null ? sectionForObject.SectionID : -1L;
  }

  /// <summary>Получить раздел по умолчанию для заданного объекта</summary>
  /// <param name="avsRow">Запись документа</param>
  public long GetOriginalSectionIdForRow(AVSRow avsRow)
  {
    if (this.IsSpecification)
      avsRow.GetFieldInt64Value(this.Attr_Section, 0, (List<RelationAttributeValuesCache>) null, true);
    return -1;
  }

  /// <summary>Получить раздел по умолчанию для заданного объекта</summary>
  /// <param name="objectID">Ид версии объекта</param>
  /// <param name="contextSection">Ид раздела контекста</param>
  /// <param name="sections">Допустимые разделы</param>
  public static SpecificationSectionInfo GetDefaultSectionForObject(
    long objectID,
    long contextSection,
    List<SpecificationSectionInfo> sections)
  {
    string razdelSP = (string) null;
    int objectType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      objectType = dbObject.ObjectType;
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_InsertToSection);
      if (attributeById != null)
      {
        if (attributeById.Value != null)
        {
          if (!(attributeById.Value is DBNull))
            razdelSP = attributeById.Value.ToString();
        }
      }
    }
    return AVSDocument.GetDefaultSectionForObject(objectType, razdelSP, contextSection, sections);
  }

  /// <summary>Получить запись спецификации для связи с данным идентификатором</summary>
  /// <param name="relationId">Идентификатор связи</param>
  /// <returns></returns>
  public AVSRow GetAvsDocRow(long relationId)
  {
    AVSRow avsRow = (AVSRow) null;
    return this.relationDictionary.TryGetValue(relationId, out avsRow) ? avsRow : (AVSRow) null;
  }

  /// <summary>Получить запись спецификации для связи с данным идентификатором</summary>
  /// <param name="relationGuid">Guid связи</param>
  /// <returns></returns>
  public AVSRow GetAvsDocRow(Guid relationGuid)
  {
    AVSRow avsRow = (AVSRow) null;
    return this.relationGuidDictionary.TryGetValue(relationGuid, out avsRow) ? avsRow : (AVSRow) null;
  }

  internal AVSRow GetAvsDocRowBySortIndex(long sortIndex)
  {
    if (AVSRow.SortIndexIsFree(sortIndex))
      return (AVSRow) null;
    AVSRow docRowBySortIndex;
    this.SortIndexDictionary.TryGetValue(sortIndex, out docRowBySortIndex);
    return docRowBySortIndex;
  }

  /// <summary>Получить список записей для объекта с заданным идентификатором</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <returns>Список записей</returns>
  public List<AVSRow> GetAvsRowsByObjectId(long objectId)
  {
    List<AVSRow> avsRowList = (List<AVSRow>) null;
    if (this.objectDictionary != null)
    {
      this.objectDictionary.TryGetValue(objectId, out avsRowList);
      if (avsRowList.IsEmpty<AVSRow>())
        this.objectDictionary.TryGetValue(-objectId, out avsRowList);
      if (avsRowList != null)
        avsRowList = new List<AVSRow>((IEnumerable<AVSRow>) avsRowList);
    }
    return avsRowList ?? new List<AVSRow>(0);
  }

  /// <summary>Получить список записей для объекта с заданным Guid</summary>
  /// <param name="objectGuid">Идентификатор объекта</param>
  /// <returns>Список записей</returns>
  public List<AVSRow> GetSpecRowsByObjectGuid(Guid objectGuid)
  {
    List<AVSRow> collection = (List<AVSRow>) null;
    if (this.objectGuidDictionary != null)
      this.objectGuidDictionary.TryGetValue(objectGuid, out collection);
    return collection != null ? new List<AVSRow>((IEnumerable<AVSRow>) collection) : new List<AVSRow>();
  }

  /// <summary>Найти записи по идентификатору версии изделия</summary>
  /// <param name="partId">Идентификатор версии изделия</param>
  /// <param name="parentChapter">Подраздел в котором должна находиться запись. null - может находится в любом разделе</param>
  /// <param name="product">Информация об исполнении или блоке данных</param>
  /// <param name="sectionId">Идентификатор раздела</param>
  /// <param name="chapterGuid">Идентификатор части. Guid.Empty если в общей части</param>
  /// <returns>Список записей спецификации</returns>
  internal List<AVSRow> FindAvsRowsByPartId(
    long partId,
    Chapter parentChapter,
    ProductInfo product,
    long sectionId,
    Guid? chapterGuid)
  {
    List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(partId);
    List<AVSRow> avsRowsByPartId = new List<AVSRow>();
    for (int index = 0; index < avsRowsByObjectId.Count; ++index)
    {
      if (avsRowsByObjectId[index].SectionID == sectionId && (product == null || avsRowsByObjectId[index].Product.Guid == product.Guid))
      {
        Chapter chapter = (Chapter) avsRowsByObjectId[index].Section;
        Chapter rootChapter = avsRowsByObjectId[index].GetRootChapter();
        if (parentChapter == null && rootChapter.IsAdditionalChapter)
        {
          if (chapterGuid.HasValue && rootChapter.ChapterGuid.Equals(chapterGuid.Value))
            avsRowsByPartId.Add(avsRowsByObjectId[index]);
        }
        else
        {
          bool flag = parentChapter == null || chapter == parentChapter;
          AdditionalChapter additionalChapter = (AdditionalChapter) null;
          while (chapter != null && (additionalChapter == null && chapterGuid.HasValue || !flag))
          {
            chapter = chapter.Parent;
            flag = ((flag ? 1 : 0) | (parentChapter == null ? 1 : (chapter == parentChapter ? 1 : 0))) != 0;
            if (additionalChapter == null)
              additionalChapter = chapter as AdditionalChapter;
          }
          if (flag)
          {
            if (chapterGuid.HasValue)
            {
              Guid? nullable;
              if (additionalChapter == null)
              {
                nullable = chapterGuid;
                Guid empty = Guid.Empty;
                if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0)
                  goto label_15;
              }
              if (additionalChapter != null)
              {
                Guid chapterGuid1 = additionalChapter.ChapterGuid;
                nullable = chapterGuid;
                if ((nullable.HasValue ? (chapterGuid1 == nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                  continue;
              }
              else
                continue;
            }
label_15:
            avsRowsByPartId.Add(avsRowsByObjectId[index]);
          }
        }
      }
    }
    return avsRowsByPartId;
  }

  /// <summary>Найти записи по идентификатору версии изделия</summary>
  /// <param name="partGuid">Глобальный идентификатор версии изделия</param>
  /// <param name="parentChapter">Подраздел в котором должна находиться запись. null - может находится в любом разделе</param>
  /// <param name="product">Информация об исполнении или блоке данных</param>
  /// <param name="sectionId">Идентификатор раздела</param>
  /// <param name="chapterGuid">Идентификатор части</param>
  /// <returns>Список записей спецификации</returns>
  public List<AVSRow> FindSpecRowsByPartGuid(
    Guid partGuid,
    Chapter parentChapter,
    ProductInfo product,
    long sectionId,
    Guid? chapterGuid)
  {
    List<AVSRow> rowsByObjectGuid = this.GetSpecRowsByObjectGuid(partGuid);
    List<AVSRow> specRowsByPartGuid = new List<AVSRow>();
    bool flag = false;
    for (int index = 0; index < rowsByObjectGuid.Count; ++index)
    {
      if (rowsByObjectGuid[index].SectionID == sectionId && (product == null || rowsByObjectGuid[index].Product.Guid == product.Guid))
      {
        Chapter chapter = (Chapter) rowsByObjectGuid[index].Section;
        Chapter rootChapter = rowsByObjectGuid[index].GetRootChapter();
        if (parentChapter == null && rootChapter.IsAdditionalChapter)
        {
          if (chapterGuid.HasValue && rootChapter.ChapterGuid.Equals(chapterGuid.Value))
            specRowsByPartGuid.Add(rowsByObjectGuid[index]);
        }
        else
        {
          flag = ((flag ? 1 : 0) | (parentChapter == null ? 1 : (chapter == parentChapter ? 1 : 0))) != 0;
          AdditionalChapter additionalChapter = (AdditionalChapter) null;
          while (chapter != null && (additionalChapter == null && chapterGuid.HasValue || !flag))
          {
            chapter = chapter.Parent;
            flag = ((flag ? 1 : 0) | (parentChapter == null ? 1 : (chapter == parentChapter ? 1 : 0))) != 0;
            if (additionalChapter == null)
              additionalChapter = chapter as AdditionalChapter;
          }
          if (flag)
          {
            if (chapterGuid.HasValue)
            {
              Guid? nullable;
              if (additionalChapter == null)
              {
                nullable = chapterGuid;
                Guid empty = Guid.Empty;
                if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0)
                  goto label_15;
              }
              if (additionalChapter != null)
              {
                Guid chapterGuid1 = additionalChapter.ChapterGuid;
                nullable = chapterGuid;
                if ((nullable.HasValue ? (chapterGuid1 == nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                  continue;
              }
              else
                continue;
            }
label_15:
            specRowsByPartGuid.Add(rowsByObjectGuid[index]);
          }
        }
      }
    }
    return specRowsByPartGuid;
  }

  /// <summary>Заменить идентификатор в структурах спецификации</summary>
  /// <param name="oldObjectID">Старый идентификатор объекта</param>
  /// <param name="newObjectID">Новый идентификатор объекта</param>
  public void ReplaceObjectID(long oldObjectID, long newObjectID)
  {
    if (oldObjectID == this.DocumentTemplateID)
    {
      this.DocumentTemplateID = newObjectID;
      this.ResetSettingsFromTemplate();
    }
    else if (oldObjectID == this.DocumentID)
    {
      this.DocumentID = newObjectID;
      if (this.DocumentID <= 0L || this.avsWindow == null)
        return;
      this.avsWindow.SetReadOnly();
    }
    else
    {
      if (this.productId == oldObjectID)
        this.ProductId = newObjectID;
      for (int index1 = 0; this.productsInfo != null && index1 < this.productsInfo.Count; ++index1)
      {
        if (this.productsInfo[index1].Id == oldObjectID)
        {
          this.productsInfo[index1].Id = newObjectID;
          List<AVSRow> allRows = this.GetAllRows(false, true);
          for (int index2 = 0; index2 < allRows.Count; ++index2)
          {
            if (allRows[index2].Relations != null)
            {
              for (int index3 = 0; index3 < allRows[index2].Relations.Count; ++index3)
              {
                if (allRows[index2].Relations[index3].ProjectId == oldObjectID)
                {
                  allRows[index2].Relations[index3].projInfo.Id = newObjectID;
                  allRows[index2].Relations[index3].SetValue(-21, (object) newObjectID, false);
                  break;
                }
              }
            }
          }
          if (this.variableDataChapter_FormA != null)
          {
            Chapter chapter = this.variableDataChapter_FormA.GetChapter(oldObjectID);
            if (chapter != null)
              chapter.ChapterID = newObjectID;
          }
          for (int index4 = 0; index4 < this.rootChapters.Count; ++index4)
          {
            if (!this.rootChapters[index4].IsCommonDataChapter && !this.rootChapters[index4].IsVariableDataChapter)
            {
              Chapter chapter = this.rootChapters[index4].GetChapter(oldObjectID);
              if (chapter != null)
                chapter.ChapterID = newObjectID;
            }
          }
          return;
        }
      }
      if (this.IsEmpty)
        return;
      List<AVSRow> avsRowsByObjectId = this.GetAvsRowsByObjectId(oldObjectID);
      if (avsRowsByObjectId.Count <= 0)
        return;
      for (int index = 0; index < avsRowsByObjectId.Count; ++index)
      {
        if (avsRowsByObjectId[index] != null)
        {
          avsRowsByObjectId[index].ObjectId = newObjectID;
          if (avsRowsByObjectId[index].objEditors != null)
            avsRowsByObjectId[index].objEditors.Clear();
        }
      }
      if (this.objectDictionary == null)
        return;
      this.objectDictionary.Remove(oldObjectID);
      if (!this.objectDictionary.ContainsKey(newObjectID))
        this.objectDictionary.Add(newObjectID, avsRowsByObjectId);
      else
        this.objectDictionary[newObjectID] = avsRowsByObjectId;
    }
  }

  /// <summary>Получить список допустимых разделов для спецификации</summary>
  /// <returns></returns>
  public virtual List<SpecificationSectionInfo> GetAllowableDocumentSections()
  {
    return new List<SpecificationSectionInfo>();
  }

  /// <summary>Получить строку спецификации по узлу документа</summary>
  /// <param name="rowDocNode">Узел документа</param>
  /// <returns>Строка спецификации</returns>
  public AVSRow GetAvsDocRow(DocumentTreeNode rowDocNode)
  {
    if (rowDocNode == null)
      throw new ArgumentNullException(nameof (rowDocNode));
    if (!(AVSDocument.FindParentSpecRowDocNode(rowDocNode) is TableData tableData))
      tableData = AVSDocument.FindParentNoteRowDocNode(rowDocNode) as TableData;
    AVSRow avsDocRow = (AVSRow) null;
    if (tableData != null)
      avsDocRow = tableData.Tag as AVSRow;
    if (avsDocRow == null)
    {
      long docNodeRelationId = this.GetDocNodeRelationId(rowDocNode);
      if (docNodeRelationId != -1L)
      {
        avsDocRow = this.GetAvsDocRow(docNodeRelationId);
      }
      else
      {
        Guid nodeRelationGuid = AVSDocument.GetDocNodeRelationGuid(rowDocNode);
        if (nodeRelationGuid != Guid.Empty)
          avsDocRow = this.GetAvsDocRow(nodeRelationGuid);
      }
    }
    return avsDocRow;
  }

  /// <summary>Получить строки спецификации по узлу документа</summary>
  /// <param name="rowDocNode">Узел документа</param>
  /// <param name="specRows">Записи спецификации</param>
  public void GetSpecRows(DocumentTreeNode rowDocNode, List<AVSRow> specRows)
  {
    if (rowDocNode == null)
      throw new ArgumentNullException(nameof (rowDocNode));
    if (specRows == null)
      throw new ArgumentNullException(nameof (specRows));
    if (rowDocNode.IsVirtualNode)
    {
      for (int index = 0; rowDocNode.Nodes != null && index < rowDocNode.Nodes.Count; ++index)
        this.GetSpecRows(rowDocNode.Nodes[index], specRows);
    }
    else
    {
      AVSRow avsDocRow = this.GetAvsDocRow(rowDocNode);
      if (avsDocRow == null || specRows.Contains(avsDocRow))
        return;
      specRows.Add(avsDocRow);
    }
  }

  /// <summary>Собрать идентификаторы исполнений, которые относятся к заданной ячейке</summary>
  /// <param name="rowDocNode">Узел документа</param>
  /// <param name="products">Список идентификаторов исполнений</param>
  public void GetProductsIdForDocNode(DocumentTreeNode rowDocNode, List<long> products)
  {
    if (rowDocNode == null)
      throw new ArgumentNullException(nameof (rowDocNode));
    if (products == null)
      throw new ArgumentNullException(nameof (products));
    if (rowDocNode.IsVirtualNode)
    {
      for (int index = 0; rowDocNode.Nodes != null && index < rowDocNode.Nodes.Count; ++index)
        this.GetProductsIdForDocNode(rowDocNode.Nodes[index], products);
    }
    else
    {
      AVSRow avsDocRow = this.GetAvsDocRow(rowDocNode);
      if (avsDocRow == null || !(rowDocNode is TextData cell))
        return;
      int indexForCountCell = avsDocRow.GetProductIndexForCountCell(cell);
      if (indexForCountCell == -1 || indexForCountCell >= this.productsInfo.Count || products.Contains(this.productsInfo[indexForCountCell].Id))
        return;
      products.Add(this.productsInfo[indexForCountCell].Id);
    }
  }

  /// <summary>Собрать исполнения, которые относятся к заданной ячейке</summary>
  /// <param name="rowDocNode">Узел документа</param>
  /// <param name="products">Список исполнений</param>
  public void GetProductsForDocNode(DocumentTreeNode docNode, List<ProductInfo> products)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (products == null)
      throw new ArgumentNullException(nameof (products));
    if (docNode.IsVirtualNode)
    {
      for (int index = 0; docNode.Nodes != null && index < docNode.Nodes.Count; ++index)
        this.GetProductsForDocNode(docNode.Nodes[index], products);
    }
    else
    {
      AVSRow avsDocRow = this.GetAvsDocRow(docNode);
      int index1 = -1;
      if (avsDocRow == null || !avsDocRow.IsFormB)
        return;
      if (docNode is TextData cell && AVSRow.IsCountFormBCell(true, cell))
        index1 = avsDocRow.GetProductIndexForCountCell(cell);
      if (index1 == -1 && avsDocRow.Relations != null)
      {
        for (int index2 = 0; index2 < this.productsInfo.Count; ++index2)
        {
          if (avsDocRow.GetRelationIndexForProduct(this.productsInfo[index2].Id) != -1 && !products.Contains(this.productsInfo[index2]))
            products.Add(this.productsInfo[index2]);
        }
      }
      else
      {
        if (index1 == -1 || index1 >= this.productsInfo.Count || products.Contains(this.productsInfo[index1]))
          return;
        products.Add(this.productsInfo[index1]);
      }
    }
  }

  /// <summary>Получить список исполнений к которым относятся выделенные ячейки документа</summary>
  /// <param name="rowDocNode">Узел документа</param>
  public bool CheckRelationsInDocNode(DocumentTreeNode rowDocNode)
  {
    if (rowDocNode == null)
      throw new ArgumentNullException(nameof (rowDocNode));
    if (rowDocNode.IsVirtualNode)
    {
      for (int index = 0; rowDocNode.Nodes != null && index < rowDocNode.Nodes.Count; ++index)
      {
        if (this.CheckRelationsInDocNode(rowDocNode.Nodes[index]))
          return true;
      }
    }
    else
    {
      AVSRow avsDocRow = this.GetAvsDocRow(rowDocNode);
      int index1 = -1;
      if (avsDocRow != null)
      {
        if (rowDocNode is TextData cell && (!this.IsFormB || AVSRow.IsCountFormBCell(this.IsFormB, cell)))
          index1 = avsDocRow.GetProductIndexForCountCell(cell);
        if (index1 == -1 && avsDocRow.Relations != null)
        {
          for (int index2 = 0; index2 < avsDocRow.Relations.Count; ++index2)
          {
            index1 = this.GetProductIndex(avsDocRow.Relations[index2].ProjectId);
            if (index1 != -1 && index1 < this.productsInfo.Count)
              return true;
          }
        }
        if (index1 != -1 && index1 < this.productsInfo.Count && avsDocRow.GetRelationIndexForProduct(this.productsInfo[index1].Id) != -1)
          return true;
      }
    }
    return false;
  }

  public (List<AVSRow> avsRows, List<DocumentTreeNode> docRows) GetAVSRowsAndDocRows(
    IEnumerable<DocumentTreeNode> docNodes)
  {
    List<AVSRow> avsRows = new List<AVSRow>();
    List<DocumentTreeNode> docRows = new List<DocumentTreeNode>();
    if (docNodes != null)
    {
      foreach (DocumentTreeNode docNode in docNodes)
        this.GetAVSRowsAndDocRows(docNode, avsRows, docRows);
    }
    return (avsRows, docRows);
  }

  /// <summary>Получить строки спецификации по узлу документа</summary>
  /// <param name="rowDocNode">Узел документа</param>
  /// <param name="specRows">Записи конструкторского документа</param>
  /// <param name="docRows">Записи документа, не попадающие в записи конструкторского документа</param>
  public void GetAVSRowsAndDocRows(
    DocumentTreeNode docNode,
    List<AVSRow> avsRows,
    List<DocumentTreeNode> docRows)
  {
    if (avsRows == null)
      throw new ArgumentNullException(nameof (avsRows));
    if (docRows == null)
      throw new ArgumentNullException(nameof (docRows));
    if (docNode == null)
      return;
    if (docNode.IsVirtualNode)
    {
      for (int index = 0; docNode.Nodes != null && index < docNode.Nodes.Count; ++index)
        this.GetAVSRowsAndDocRows(docNode.Nodes[index], avsRows, docRows);
    }
    else
    {
      AVSRow avsDocRow = this.GetAvsDocRow(docNode);
      if (avsDocRow != null)
      {
        if (avsRows.Contains(avsDocRow))
          return;
        avsRows.Add(avsDocRow);
      }
      else
      {
        DocumentTreeNode documentTreeNode = AVSDocument.FindParentNoteRowDocNode(docNode);
        if (documentTreeNode == null)
          documentTreeNode = this.FindParentLRIRowDocNode(docNode);
        else if (AVSDocument.IsProductPageLinksDocNodeChild(docNode))
          documentTreeNode = (DocumentTreeNode) null;
        if (documentTreeNode == null || docRows.Contains(documentTreeNode))
          return;
        docRows.Add(documentTreeNode);
      }
    }
  }

  /// <summary>Получить ячейку содержащую текст примечания в записи примечания</summary>
  /// <param name="docRow">Запись примечание</param>
  /// <returns></returns>
  public static TextData GetNoteTextCell(TableData docRow)
  {
    return docRow != null ? docRow.FindFirstNodeByName("Текст примечания") as TextData : throw new ArgumentNullException(nameof (docRow));
  }

  /// <summary>Получить запись спецификации для строки табличного вида</summary>
  /// <param name="treeListNode">Строка табличного вида</param>
  /// <returns>Запись спецификации</returns>
  public AVSRow GetAvsDocRow(Row treeListNode)
  {
    return treeListNode != null ? treeListNode.Item as AVSRow : throw new ArgumentNullException(nameof (treeListNode));
  }

  /// <summary>
  /// Получить список идентификаторов типов объектов для раздела
  /// </summary>
  /// <param name="sectInfo"></param>
  /// <returns></returns>
  internal static List<int> GetTypeIdListForSection(SpecificationSectionInfo sectInfo)
  {
    List<int> intList = new List<int>();
    List<int> partTypes = new List<int>();
    AVSDocument.GetPartTypes(sectInfo, partTypes);
    if (sectInfo.SectionID != AVSDocument.ObjID_SectionDocumentation)
    {
      List<int> collection = new List<int>();
      foreach (int childType in partTypes)
      {
        int num = MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_Complect) || MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_Complex) || MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_AssemblyUnit) || MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_OtherProduct) ? 1 : (MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_StandartProduct) ? 1 : 0);
        if (MetaDataHelper.IsObjectTypeChildOf(childType, AvsIDCache.ObjType_Detail))
          collection.Add(DocumentTypeWeight.partDrawType);
        if (num == 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
            if (customService != null)
            {
              int[] outputObjectTypes = customService.GetDocumentTypesByOutputObjectTypes(sessionKeeper.Session.SessionGUID, new int[1]
              {
                childType
              }, AvsIDCache.ObjType_ConstructorDocument);
              if (outputObjectTypes != null)
              {
                if (outputObjectTypes.Length != 0)
                  collection.AddRange((IEnumerable<int>) outputObjectTypes);
              }
            }
          }
        }
      }
      partTypes.AddRange((IEnumerable<int>) collection);
    }
    return partTypes;
  }

  /// <summary>Получить типы которые могут добавляться в заданный раздел спецификации</summary>
  /// <param name="specSection">Раздел спецификации</param>
  /// <param name="partTypes">Типы объектов</param>
  internal static void GetPartTypes(SpecificationSectionInfo specSection, List<int> partTypes)
  {
    if (specSection.PartTypes == null)
      return;
    for (int index1 = 0; index1 < specSection.PartTypes.Length; ++index1)
    {
      int partType1 = specSection.PartTypes[index1];
      bool flag = partType1 != -1;
      if (partTypes.Count > 0)
      {
        for (int index2 = 0; flag && index2 < partTypes.Count; ++index2)
        {
          flag = false;
          int partType2 = partTypes[index2];
          if (partType1 != partType2 && !AVSDocument.IsParentObjectType(partType2, partType1))
          {
            flag = true;
            if (AVSDocument.IsParentObjectType(partType1, partType2))
              partTypes.RemoveAt(index2);
          }
        }
      }
      if (flag && !partTypes.Contains(partType1))
        partTypes.Add(partType1);
    }
  }

  /// <summary>Получить типы которые могут добавляться в заданный раздел спецификации</summary>
  /// <param name="specSection">Раздел спецификации</param>
  /// <param name="partTypes">Типы объектов</param>
  internal void GetPartTypes(SpecificationSection specSection, List<int> partTypes)
  {
    if (specSection.PartTypes == null)
      return;
    for (int index1 = 0; index1 < specSection.PartTypes.Length; ++index1)
    {
      int partType1 = specSection.PartTypes[index1];
      bool flag = partType1 != -1;
      if (partTypes.Count > 0)
      {
        for (int index2 = 0; flag && index2 < partTypes.Count; ++index2)
        {
          flag = false;
          int partType2 = partTypes[index2];
          if (partType1 != partType2 && !AVSDocument.IsParentObjectType(partType2, partType1))
          {
            flag = true;
            if (AVSDocument.IsParentObjectType(partType1, partType2))
              partTypes.RemoveAt(index2);
          }
        }
      }
      if (flag && !partTypes.Contains(partType1))
        partTypes.Add(partType1);
    }
  }

  /// <summary>Получить идентификатор связи для строки в документе</summary>
  /// <param name="rowDocNode">Строка документа</param>
  /// <returns>Идентификатор связи, соответствующей строке</returns>
  public long GetDocNodeRelationId(DocumentTreeNode rowDocNode)
  {
    rowDocNode = AVSDocument.FindParentSpecRowDocNode(rowDocNode);
    return !(rowDocNode is INodeWithReference nodeWithReference) || !(nodeWithReference.Reference is ReferenceToDBObject reference) ? -1L : reference.DBRelationID;
  }

  /// <summary>Получить идентификатор объекта для строки в документе</summary>
  /// <param name="rowDocNode">Строка документа</param>
  /// <returns>Идентификатор объекта, соответствующей строке</returns>
  public long GetDocNodeObjectId(DocumentTreeNode rowDocNode)
  {
    rowDocNode = AVSDocument.FindParentSpecRowDocNode(rowDocNode);
    return !(rowDocNode is INodeWithReference nodeWithReference) || !(nodeWithReference.Reference is ReferenceToDBObject reference) ? -1L : reference.DBObjectID;
  }

  /// <summary>Получить глобальный идентификатор связи для строки в документе</summary>
  /// <param name="rowDocNode">Строка документа</param>
  /// <returns>Глобальный идентификатор связи, соответствующей строке</returns>
  public static Guid GetDocNodeRelationGuid(DocumentTreeNode rowDocNode)
  {
    rowDocNode = AVSDocument.FindParentSpecRowDocNode(rowDocNode);
    return !(rowDocNode is INodeWithReference nodeWithReference) || !(nodeWithReference.Reference is ReferenceToDBObject reference) ? Guid.Empty : reference.DBRelationGuid;
  }

  /// <summary>Получить глобальный идентификатор объекта для строки в документе</summary>
  /// <param name="rowDocNode">Строка документа</param>
  /// <returns>Глобальный идентификатор объекта, соответствующей строке</returns>
  public Guid GetDocNodeObjectGuid(DocumentTreeNode rowDocNode)
  {
    rowDocNode = AVSDocument.FindParentSpecRowDocNode(rowDocNode);
    return !(rowDocNode is INodeWithReference nodeWithReference) || !(nodeWithReference.Reference is ReferenceToDBObject reference) ? Guid.Empty : reference.DBObjectGuid;
  }

  /// <summary>Извлечь из результата вызова диалога Навигатора идентификатор объекта</summary>
  /// <param name="objectData">Данные об объекте полученные из навигатора</param>
  /// <param name="objectF_ID">Идентификатор объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <returns>Возвращает идентификатор версии объекта</returns>
  internal static long GetObjectIDNavigatorData(
    IUserSession session,
    object objectData,
    out long objectF_ID,
    out int objectType)
  {
    long objectIdNavigatorData = -1;
    objectF_ID = -1L;
    objectType = -1;
    switch (objectData)
    {
      case IDBTypedObjectID dbTypedObjectId:
        objectIdNavigatorData = dbTypedObjectId.ObjectID;
        objectF_ID = dbTypedObjectId.ID;
        objectType = dbTypedObjectId.ObjectType;
        break;
      case QuickObjectInfo quickObjectInfo:
        objectIdNavigatorData = quickObjectInfo.ObjectID;
        objectF_ID = quickObjectInfo.ID;
        objectType = quickObjectInfo.ObjectTypeID;
        break;
      case NodeID nodeId:
        objectIdNavigatorData = nodeId.ObjectID;
        objectF_ID = nodeId.ID;
        objectType = nodeId.TypeID;
        break;
      case long num:
        objectIdNavigatorData = num;
        break;
    }
    if (objectType == -1 && !objectIdNavigatorData.IsUndefinedId())
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectIdNavigatorData);
      objectType = objectInfo.ObjectTypeID;
      objectF_ID = objectInfo.ID;
    }
    return objectIdNavigatorData;
  }

  /// <summary>Добавить записи в раздел спецификации</summary>
  /// <param name="parts">Идентификаторы объектов</param>
  /// <param name="relationType">Идентификатор типа связи</param>
  /// <param name="context">Контекст вызова метода</param>
  /// <param name="copyObjNoteToRowNote">Копировать примечание объекта в примечание связи</param>
  /// <param name="selectNewRows">Выделить добавленные записи</param>
  /// <param name="dstSortIndexes">Принудительно установить атрибут Сортировка в соответствующие значения</param>
  public List<AVSRow> AddAvsRowParts(
    object[] parts,
    int relationType,
    AVSDocumentContext context,
    bool copyObjNoteToRowNote,
    bool selectNewRows,
    IList<long> dstSortIndexes = null)
  {
    int num1 = relationType;
    if (relationType == -1)
      num1 = AvsIDCache.Relation_Project;
    List<AVSRow> newSpecRows = (List<AVSRow>) null;
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<int> intList = new List<int>();
    List<long> objectIDs = new List<long>();
    List<int> objectTypes = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((context.Products == null || context.Products.Count == 0) && this.IsFormB && this.productsInfo.Count > 0)
      {
        context.Products = new List<ProductInfo>();
        context.Products.Add(this.productsInfo[0]);
      }
      IDBRelationCollection relationWithPartCollection = (IDBRelationCollection) null;
      IDBRelationCollection relCollection = (IDBRelationCollection) null;
      if (this.IsSpecification)
      {
        relationWithPartCollection = sessionKeeper.Session.GetRelationCollection(num1, this.FiltrationOwnerID);
        relCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
      }
      IDBRelation dbRelation1 = (IDBRelation) null;
      Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
      long objectF_ID = -1;
      int objectType = -1;
      string str = (string) null;
      long num2 = -1;
      for (int index1 = parts.Length - 1; index1 > -1; --index1)
      {
        try
        {
          long objectIdNavigatorData = AVSDocument.GetObjectIDNavigatorData(sessionKeeper.Session, parts[index1], out objectF_ID, out objectType);
          if (objectIdNavigatorData != -1L)
          {
            IDBObject dbObject = (IDBObject) null;
            if (copyObjNoteToRowNote)
            {
              dbObject = sessionKeeper.Session.GetObject(objectIdNavigatorData);
              IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_Note);
              str = attributeById == null ? (string) null : attributeById.AsString;
            }
            if (!this.IsAllowableObjectType(objectType))
            {
              if (dbObject == null)
                dbObject = sessionKeeper.Session.GetObject(objectIdNavigatorData);
              string objectTypeName = MetaDataHelper.GetObjectTypeName(objectType);
              throw new Exception($"Нельзя добавлять в документ объект '{dbObject.Caption}' типа '{objectTypeName}'");
            }
            if (this.IsSpecification)
            {
              if (!this.IsFormB)
              {
                bool flag = context.Products.Any<ProductInfo>((System.Func<ProductInfo, bool>) (p => p.IsCommonData || p.IsVariableData));
                if (this.GetRelationType((AVSRow) null, context, objectType, num1) == AvsIDCache.Relation_Document)
                {
                  for (int index2 = 0; index2 < context.Products.Count; ++index2)
                  {
                    if (this.AvsDocumentForm == AVSDocumentForm.V && context.Products[index2].IsVariableData)
                    {
                      objectIDs.Add(objectIdNavigatorData);
                      objectTypes.Add(objectType);
                    }
                    else
                    {
                      --num2;
                      for (int index3 = 0; index3 < this.productsInfo.Count; ++index3)
                      {
                        long num3 = flag ? this.productsInfo[index3].Id : context.Products[index2].Id;
                        IDBRelation dbRelation2 = sessionKeeper.Session.GetRelation(num3, objectIdNavigatorData, AvsIDCache.Relation_Document, true);
                        if (dbRelation2 != null && this.GetAvsDocRow(dbRelation2.RelationID) != null)
                          dbRelation2 = (IDBRelation) null;
                        if (dbRelation2 == null)
                          dbRelation2 = this.productsInfo.Count <= 1 ? relCollection.Create(num3, objectIdNavigatorData) : AVSDocument.CreateDocRelationWithLockPDMHandler(relCollection, num3, objectIdNavigatorData, objectF_ID);
                        longList1.Add(dbRelation2.RelationID);
                        longList2.Add(dbRelation2.ProjID);
                        intList.Add(dbRelation2.RelationType);
                        AVSDocument.AddRelationToTypedDictionary(dictionary, dbRelation2.RelationType, dbRelation2.RelationID);
                        List<AttributeValues> attributeValuesList = new List<AttributeValues>();
                        if (copyObjNoteToRowNote && str != null && str != "" && this.Field_Note.IsRelationAttribute)
                          attributeValuesList.Add(new AttributeValues(this.Field_Note.AttributeId, (object) str));
                        long initValue = dstSortIndexes == null || AVSRow.SortIndexIsFree(dstSortIndexes[index1]) ? num2 : dstSortIndexes[index1];
                        attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_SortIndex, (object) initValue));
                        dbRelation2.SetAttributesValues(attributeValuesList.ToArray());
                        if (!flag)
                          break;
                      }
                    }
                  }
                }
                else
                {
                  for (int index4 = 0; index4 < context.Products.Count; ++index4)
                  {
                    if (this.AvsDocumentForm == AVSDocumentForm.V && context.Products[index4].IsVariableData)
                    {
                      objectIDs.Add(objectIdNavigatorData);
                      objectTypes.Add(objectType);
                    }
                    else
                    {
                      --num2;
                      for (int index5 = 0; index5 < this.productsInfo.Count; ++index5)
                      {
                        long dstProductId = flag ? this.productsInfo[index5].Id : context.Products[index4].Id;
                        long tmpSortIndex = dstSortIndexes == null || AVSRow.SortIndexIsFree(dstSortIndexes[index1]) ? num2 : dstSortIndexes[index1];
                        dbRelation1 = this.CreateRelationForAddPart(relationWithPartCollection, dstProductId, objectIdNavigatorData, copyObjNoteToRowNote, str, tmpSortIndex, longList1, longList2, intList, dictionary);
                        if (!flag)
                          break;
                      }
                    }
                  }
                }
              }
              else
              {
                objectIDs.Add(objectIdNavigatorData);
                objectTypes.Add(objectType);
              }
            }
            else
            {
              objectIDs.Add(objectIdNavigatorData);
              objectTypes.Add(objectType);
            }
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
      List<AVSRow> avsRowList = new List<AVSRow>();
      for (int index = 0; index < objectIDs.Count; ++index)
        avsRowList.AddRange((IEnumerable<AVSRow>) this.GetAvsRowsByObjectId(objectIDs[index]));
      RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument();
      if (this.IsSpecification && !this.IsFormB)
      {
        if (this.AvsDocumentForm != AVSDocumentForm.V || dictionary.Count > 0)
          newSpecRows = this.AddSpecificationRelations(dictionary, false, context);
        if (this.AvsDocumentForm == AVSDocumentForm.V && objectIDs.Count > 0)
          newSpecRows = this.LoadRowsForDBObjects(objectIDs, objectTypes, (ColumnDescriptor[]) null, (ColumnDescriptor[]) null, true, context, true, sessionKeeper.Session, rowDicts, AvsConfig.General.AddToCurrentGroup);
      }
      else
      {
        if (dictionary.Count > 0)
          newSpecRows = this.AddSpecificationRelations(dictionary, false, context);
        if (objectIDs.Count > 0)
          newSpecRows = this.LoadRowsForDBObjects(objectIDs, objectTypes, (ColumnDescriptor[]) null, (ColumnDescriptor[]) null, true, context, true, sessionKeeper.Session, rowDicts, AvsConfig.General.AddToCurrentGroup);
        for (int index = newSpecRows.Count - 1; index >= 0; --index)
        {
          if (avsRowList.Contains(newSpecRows[index]))
            newSpecRows.RemoveAt(index);
        }
      }
      if (selectNewRows)
        this.SelectNewRows(newSpecRows);
    }
    if (longList1 != null && longList1.Count > 0)
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) longList1, (IList<long>) longList2, (IList<int>) null, (IList<int>) intList));
    return newSpecRows;
  }

  private IDBRelation CreateRelationForAddPart(
    IDBRelationCollection relationWithPartCollection,
    long dstProductId,
    long partObjectID,
    bool copyObjNoteToRowNote,
    string objNote,
    long tmpSortIndex,
    List<long> newRelationIds,
    List<long> newRelationProjIds,
    List<int> newRelationTypeIds,
    Dictionary<int, List<long>> newRelationsTypedDictionary)
  {
    IDBRelation relationForAddPart = relationWithPartCollection.Create(dstProductId, partObjectID);
    newRelationIds.Add(relationForAddPart.RelationID);
    newRelationProjIds.Add(relationForAddPart.ProjID);
    newRelationTypeIds.Add(relationForAddPart.RelationType);
    AVSDocument.AddRelationToTypedDictionary(newRelationsTypedDictionary, relationForAddPart.RelationType, relationForAddPart.RelationID);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (copyObjNoteToRowNote && !objNote.IsEmpty() && this.Field_Note.IsRelationAttribute)
      attributeValuesList.Add(new AttributeValues(this.Field_Note.AttributeId, (object) objNote));
    attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_SortIndex, (object) tmpSortIndex));
    relationForAddPart.SetAttributesValues(attributeValuesList.ToArray());
    return relationForAddPart;
  }

  /// <summary>Вставить новую запись примечание</summary>
  /// <param name="context">Контекст вставки</param>
  /// <param name="noteText">Текст примечания</param>
  /// <param name="rowTemplate">Шаблон записи</param>
  /// <param name="updateDoc">Обновить разбивку документа</param>
  /// <param name="selectNewRow">Выбрать запись</param>
  /// <returns>Запись примечание</returns>
  public TableData InsertNewNoteDocRow(
    AVSDocumentContext context,
    string noteText,
    TableData rowTemplate,
    bool updateDoc,
    bool selectNewRow)
  {
    TableData noteDocRow = rowTemplate != null ? this.CreateNoteDocRow(rowTemplate, noteText) : throw new ArgumentNullException(nameof (rowTemplate));
    this.InsertNoteDocRow(context, noteDocRow, updateDoc, selectNewRow);
    return noteDocRow;
  }

  /// <summary>Создать запись примечание</summary>
  /// <param name="rowTemplate">Шаблон записи в документе</param>
  /// <param name="noteText">Текст примечания</param>
  /// <returns>Строка документа</returns>
  public TableData CreateNoteDocRow(TableData rowTemplate, string noteText)
  {
    TableData docRow = rowTemplate != null ? (TableData) rowTemplate.CloneFromTemplate(true, true) : throw new ArgumentNullException(nameof (rowTemplate));
    docRow.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.SpecNote_TypeName, false, false, false);
    TextData noteTextCell = AVSDocument.GetNoteTextCell(docRow);
    if (noteTextCell == null)
      return docRow;
    noteTextCell.AssignText(noteText, false, true, false, true, true);
    return docRow;
  }

  /// <summary>Вставить запись примечание</summary>
  /// <param name="context">Контекст вставки</param>
  /// <param name="noteDocRow">Запись примечание</param>
  /// <param name="updateDoc">Обновить разбивку документа</param>
  /// <param name="selectNewRow">Выбрать вставленную запись</param>
  public AVSRow InsertNoteDocRow(
    AVSDocumentContext context,
    TableData noteDocRow,
    bool updateDoc,
    bool selectNewRow)
  {
    if (noteDocRow == null)
      throw new ArgumentNullException(nameof (noteDocRow));
    AVSDocument.SetupDocNodeAsNoteRow((DocumentTreeNode) noteDocRow);
    AVSRow row = (AVSRow) null;
    int fromPage = -1;
    if (context.Section != null)
    {
      if (context.RowIndex == -1)
        context.RowIndex = context.Section.Rows.Count;
      row = new AVSRow(this, -1L, Guid.Empty, -1, -1L, Guid.Empty, -1, Guid.Empty, -1L);
      row.IsNoteRow = true;
      TableData tableData1 = noteDocRow;
      for (int index1 = 0; index1 < context.Section.DocNodes.Count; ++index1)
      {
        int indexForDocChapter1 = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) context.Section.DocNodes[index1]);
        if (context.FirstProductIndexInBlock == -1 || indexForDocChapter1 == context.FirstProductIndexInBlock)
        {
          TableData tableData2 = (TableData) null;
          int index2 = -1;
          for (int rowIndex = context.RowIndex; tableData2 == null && rowIndex < context.Section.Rows.Count; ++rowIndex)
          {
            for (int index3 = 0; index3 < context.Section.Rows[rowIndex].DocNodes.Count; ++index3)
            {
              for (TableData parentNode = context.Section.DocNodes[index1]; parentNode != null; parentNode = parentNode.NextCell as TableData)
              {
                if (context.Section.Rows[rowIndex].DocNodes[index3].IsChildForNode((DocumentTreeNode) parentNode, false))
                {
                  tableData2 = context.Section.Rows[rowIndex].DocNodes[index3];
                  break;
                }
              }
              if (tableData2 != null)
                break;
            }
          }
          if (tableData2 != null)
          {
            node = tableData2.ParentCell;
            index2 = tableData2.Index;
          }
          else if (context.Section.DocNodes[index1].FindLastCell() is TableData node)
            index2 = node.NodesCount;
          if (!row.HasDocNodes)
          {
            row.AddDocNode(noteDocRow);
          }
          else
          {
            noteDocRow = (TableData) tableData1.Clone();
            row.AddDocNode(noteDocRow);
          }
          if (node != null)
          {
            if (index2 != -1 && index2 < node.NodesCount)
              node.InsertChildNode(index2, (DocumentTreeNode) noteDocRow, false, true, false, false, false);
            else
              node.AddChildNode((DocumentTreeNode) noteDocRow, false, false);
            int indexForDocChapter2 = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) node);
            if (indexForDocChapter2 > 0)
              noteDocRow.SetAttributeValue(AVSRow.DocAttr_ProductIndex, indexForDocChapter2.ToString(), false, false, false);
            else
              noteDocRow.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
            if (fromPage == -1 && node.Page != null)
              fromPage = node.Page.Index;
          }
        }
      }
      if (context.RowIndex <= context.Section.Rows.Count)
        context.Section.InsertRow(context.RowIndex, row);
      else
        context.RowIndex = context.Section.AddRow(row, this.AutoSort);
      this.IndexAVSDocument(true);
    }
    if (updateDoc)
    {
      this.SuspendDocumentAndGridUpdates();
      this.UpdateSkipLines(false, false);
      if (fromPage == -1)
        fromPage = 0;
      this.ResumeDocumentAndGridUpdates(fromPage, true, true, true, true);
      if (this.IsGridViewMode)
        this.UpdateViewNodes(false, false, false, false, true, EmptyRowUpdateMode.DontChange);
    }
    if (selectNewRow && this.DocumentControl != null)
      this.DocumentControl.SetSelection((DocumentTreeNode) noteDocRow, true, false);
    return row;
  }

  /// <summary>Добавить часть в корневой список</summary>
  /// <param name="chapter">Новая часть</param>
  /// <param name="sort">Вставлять с учётом сортировки</param>
  /// <returns></returns>
  public int AddRootChapter(Chapter chapter, bool sort)
  {
    if (chapter == null)
      throw new ArgumentNullException(nameof (chapter));
    int index1 = this.rootChapters.Count;
    if (sort)
    {
      for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
      {
        if (chapter.CompareTo((object) this.rootChapters[index2]) < 0)
        {
          index1 = index2;
          break;
        }
      }
    }
    this.rootChapters.Insert(index1, chapter);
    return index1;
  }

  public void SelectNewRows(List<AVSRow> newSpecRows)
  {
    if (newSpecRows == null || newSpecRows.Count <= 0)
      return;
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    for (int index = 0; index < newSpecRows.Count; ++index)
    {
      if (newSpecRows[index].DocNode != null)
        documentTreeNodeList.Add((DocumentTreeNode) newSpecRows[index].DocNode);
    }
    TextData focusedDocNode = (TextData) null;
    if (newSpecRows.Count == 1 && !newSpecRows[0].IsDocRelation)
    {
      if (this.ViewMode == AVSViewMode.Page)
      {
        int productIndex = 0;
        if (this.IsFormB && this.DocumentControl?.ActivePage != null)
          productIndex = this.GetFirstProductIndex((PageData) this.DocumentControl.ActivePage);
        TextData cellForAttribute = newSpecRows[0].GetDocumentCellForAttribute(this.Field_Count, productIndex);
        if (cellForAttribute != null)
        {
          if (newSpecRows[0].SectionID == AVSDocument.ObjID_SectionMaterials)
          {
            string text = cellForAttribute.Text;
            string str = "";
            if (text != null)
              str = text;
            MeasuredValue measuredValue = AVSRow.ConvertCountToMeasuredValue((object) str, false);
            if (measuredValue != null)
              str = measuredValue.Value.ToString();
            cellForAttribute.AssignText(str, false, true, false, true, true);
          }
          this.DocumentControl?.SetSelection((DocumentTreeNode) cellForAttribute, true, Point.Empty, true, false);
        }
        else
          this.DocumentControl?.SetSelection(documentTreeNodeList, true, false);
      }
      else
      {
        this.DocumentControl?.SetSelection(documentTreeNodeList, false, false);
        this.avsWindow.RestoreListSelection(documentTreeNodeList, (DocumentTreeNode) focusedDocNode);
        AVSColumn col = (AVSColumn) null;
        foreach (AVSColumn column in this.avsWindow.virtualTree.Columns)
        {
          if (column.Tag != null && AVSRow.IsCountField(column.Tag.SpecRowAttributeInfo))
            col = column;
        }
        if (col == null)
          return;
        this.avsWindow.virtualTree.ShowEditor(col);
      }
    }
    else if (this.ViewMode == AVSViewMode.Page)
      this.DocumentControl?.SetSelection(documentTreeNodeList, true, false);
    else
      this.avsWindow.RestoreListSelection(documentTreeNodeList, (DocumentTreeNode) focusedDocNode);
  }

  /// <summary>Добавить записи в раздел спецификации</summary>
  /// <param name="relations">Словарь списков идентификаторов связей разных типов связи</param>
  /// <param name="selectNewRows">Выделить добавленные записи</param>
  /// <param name="context">Контекст добавления записей в спецификацию</param>
  /// <returns>Возвращает список загруженных записей</returns>
  public List<AVSRow> AddSpecificationRelations(
    Dictionary<int, List<long>> relations,
    bool selectNewRows,
    AVSDocumentContext context)
  {
    List<AVSRow> newSpecRows = (List<AVSRow>) null;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      newSpecRows = this.LoadNewRelations(relations, context, true, AvsConfig.General.AddToCurrentGroup, true);
      this.UpdateVariableDataCaptions();
      if (newSpecRows.Count > 0)
      {
        if (newSpecRows[0].DocNode != null)
          newSpecRows[0].DocNode?.Page?.SetNeedUpdateLayoutFlag(true, false, false, false, true);
      }
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      if (selectNewRows)
        this.SelectNewRows(newSpecRows);
    }
    return newSpecRows;
  }

  /// <summary>Добавить новую запись в лист регистрации изменений</summary>
  /// <param name="contextLRIRow">Контекст добавления записи или null</param>
  /// <param name="after">Вставлять после текущей записи, если она задана. Иначе добавляется в конец таблицы</param>
  /// <returns>Возвращает добавленную запись</returns>
  public TableData AddLRIRow(DocumentTreeNode context, bool after)
  {
    TableData parentLriRowDocNode = this.FindParentLRIRowDocNode(context) as TableData;
    TableData tableData = (TableData) null;
    int index = -1;
    if (parentLriRowDocNode != null)
    {
      tableData = parentLriRowDocNode.ParentCell;
      index = parentLriRowDocNode.Index;
      if (after)
        ++index;
    }
    if (tableData == null)
    {
      if (this.lriPage == null)
        this.lriPage = this.AddNewLRIPage(false);
      tableData = (this.lriPage.FindFirstNodeFromTemplate_Recursive("Таблица изменений") as TableData).FindLastCell() as TableData;
      index = tableData.NodesCount;
    }
    TableData child = (TableData) this.lriRowTemplate.CloneFromTemplate(true, true);
    tableData.InsertChildNode(index, (DocumentTreeNode) child, false, false, false, false, true);
    return child;
  }

  /// <summary>Получить информацию об исполнении из списка исполнений</summary>
  /// <param name="productGuid">Глобальный идентификатор версии исполнения</param>
  public ProductInfo GetProductInfoByObjectGuid(Guid productGuid)
  {
    return this.GetProductInfoByIndex(this.GetProductIndex(productGuid));
  }

  /// <summary>Получить информацию об исполнении из списка исполнений</summary>
  /// <param name="productId">Идентификатор версии исполнения, независимо от знака</param>
  public ProductInfo GetProductInfoByObjectID(long productId)
  {
    return this.GetProductInfoByIndex(this.GetProductIndex(productId));
  }

  /// <summary>Получить информацию об исполнении из списка исполнений</summary>
  /// <param name="productIndex">Номер исполнения в списке</param>
  public ProductInfo GetProductInfoByIndex(int productIndex)
  {
    return productIndex >= 0 && productIndex < this.productsInfo.Count ? this.productsInfo[productIndex] : (ProductInfo) null;
  }

  /// <summary>Получить индекс исполнения в списке исполнений</summary>
  /// <param name="productId">Идентификатор версии исполнения, независимо от знака</param>
  /// <returns>Индекс исполнения</returns>
  public int GetProductIndex(long productId)
  {
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      if (this.productsInfo[index].Id == productId || this.productsInfo[index].Id == -productId)
        return index;
    }
    return -1;
  }

  /// <summary>Получить индекс изделия в списке родительских изделий</summary>
  /// <param name="productId">Идентификатор версии изделия, независимо от знака</param>
  /// <returns>Индекс исполнения</returns>
  public int GetParentProductIndex(long productId)
  {
    for (int index = 0; index < this.parentProducts.Count; ++index)
    {
      if (Math.Abs(this.parentProducts[index].Id) == Math.Abs(productId))
        return index;
    }
    return -1;
  }

  /// <summary>Получить информацию об изделии из списка родительских изделий</summary>
  /// <param name="productId">Идентификатор версии изделия, независимо от знака</param>
  public ProductInfo GetParentProductInfoByObjectID(long productId)
  {
    return this.GetParentProductInfoByIndex(this.GetParentProductIndex(productId));
  }

  /// <summary>Получить информацию об изделии из списка родительских изделий</summary>
  /// <param name="productIndex">Номер изделия в списке</param>
  public ProductInfo GetParentProductInfoByIndex(int productIndex)
  {
    return productIndex >= 0 && productIndex < this.parentProducts.Count ? this.parentProducts[productIndex] : (ProductInfo) null;
  }

  /// <summary>Получить индекс исполнения в списке исполнений</summary>
  /// <param name="productId">Идентификатор версии исполнения, независимо от знака</param>
  /// <returns>Индекс исполнения</returns>
  public int GetProductIndex(Guid productGuid)
  {
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      if (this.productsInfo[index].Guid == productGuid)
        return index;
    }
    return -1;
  }

  /// <summary>Получить индекс исполнения в списке исполнений</summary>
  /// <param name="productId">Идентификатор версии исполнения, независимо от знака</param>
  /// <returns>Индекс исполнения</returns>
  public int GetParentProductIndex(Guid productGuid)
  {
    for (int index = 0; index < this.parentProducts.Count; ++index)
    {
      if (this.parentProducts[index].Guid == productGuid)
        return index;
    }
    return -1;
  }

  /// <summary>Есть ли исполнение с заданным идентификатором в списке исполнений</summary>
  /// <param name="productId">Идентификатор версии исполнения, независимо от знака</param>
  public bool ContainsProduct(long productId)
  {
    return this.GetProductIndex(productId) != -1 || this.GetParentProductIndex(productId) != -1;
  }

  /// <summary>Получить индекс исполнения в списке исполнений</summary>
  /// <param name="product">Исполнение</param>
  /// <returns>Индекс исполнения</returns>
  public int GetProductIndex(ProductInfo product)
  {
    if (product == null)
      throw new ArgumentNullException(nameof (product));
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      if (product.IsEqualProducts(this.productsInfo[index]))
        return index;
    }
    return -1;
  }

  internal ProductInfo FindProductByIndex(int productIndex)
  {
    if (productIndex < 0)
      return (ProductInfo) null;
    if (this.ParentProducts.Count > 0)
    {
      if (productIndex < this.ParentProducts.Count)
        return this.ParentProducts[productIndex];
    }
    else if (productIndex < this.ProductsInfo.Count)
      return this.ProductsInfo[productIndex];
    return (ProductInfo) null;
  }

  /// <summary>Переместить исполнение с одной позиции на другую</summary>
  /// <param name="product">Исполнение</param>
  /// <param name="index">Новый индекс исполнения</param>
  /// <param name="updateStructure">Обновить структуру документа</param>
  /// <param name="updateUI">Обновить внешний вид документа</param>
  public void SetProductIndex(ProductInfo product, int index, bool updateStructure, bool updateUI)
  {
    int index1 = product != null ? this.GetProductIndex(product) : throw new ArgumentNullException(nameof (product));
    if (index1 < 0)
      throw new Exception($"Не найдено исполнение: {product.Designation}");
    if (index == index1)
      return;
    if (index < 0 || index > this.productsInfo.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    if (index1 < index)
      --index;
    ProductInfo product1 = this.productsInfo[index1];
    this.productsInfo.RemoveAt(index1);
    this.productsInfo.Insert(index, product1);
    if (!updateStructure || this.AvsDocumentForm != AVSDocumentForm.A)
      return;
    Chapter productChapter1 = this.variableDataChapter_FormA.GetProductChapter(product1);
    this.variableDataChapter_FormA.Chapters.Remove(productChapter1);
    this.variableDataChapter_FormA.Chapters.Insert(index, productChapter1);
    if (productChapter1.DocNode != null && this.variableDataChapter_FormA.DocNode != null)
    {
      productChapter1.DocNode.UniteTable();
      productChapter1.DocNode.Remove(true, false, false);
      TableData dataOwner;
      int dataPositionInFlow = this.variableDataChapter_FormA.DocNode.FindDataPositionInFlow(index, out dataOwner);
      if (dataPositionInFlow != -1 && dataOwner != null)
        dataOwner.InsertChildNode(dataPositionInFlow, (DocumentTreeNode) productChapter1.DocNode, true, true, updateUI, updateUI, false);
    }
    for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
    {
      if (!this.rootChapters[index2].IsCommonDataChapter && !this.rootChapters[index2].IsVariableDataChapter)
      {
        for (int index3 = 0; index3 < this.rootChapters[index2].Chapters.Count; ++index3)
        {
          if (this.rootChapters[index2].Chapters[index3] is VariableDataChapterFormA chapter)
          {
            Chapter productChapter2 = chapter.GetProductChapter(product1);
            chapter.Chapters.Remove(productChapter2);
            chapter.Chapters.Insert(index, productChapter2);
            if (productChapter2.DocNode != null && chapter.DocNode != null)
            {
              productChapter2.DocNode.UniteTable();
              productChapter2.DocNode.Remove(true, false, false);
              TableData dataOwner;
              int dataPositionInFlow = chapter.DocNode.FindDataPositionInFlow(index, out dataOwner);
              if (dataPositionInFlow != -1 && dataOwner != null)
                dataOwner.InsertChildNode(dataPositionInFlow, (DocumentTreeNode) productChapter2.DocNode, true, true, updateUI, updateUI, false);
            }
          }
        }
      }
    }
  }

  /// <summary>Добавить в конструкторский документ исполнения</summary>
  /// <param name="productIDs">Список идентификаторов изделий</param>
  /// <param name="session">Пользовательская сессия</param>
  public void SetGroupProducts(List<long> productIDs)
  {
    if (productIDs == null || productIDs.Count == 0)
      return;
    List<ProductInfo> products = new List<ProductInfo>(productIDs.Count);
    for (int index = 0; index < productIDs.Count; ++index)
    {
      ProductInfo productInfo = new ProductInfo(Guid.Empty, productIDs[index], (string) null);
      productInfo.UpdateInfo((List<int>) null, (string) null);
      products.Add(productInfo);
    }
    this.SetGroupProducts(products);
  }

  /// <summary>Добавить в конструкторский документ исполнения</summary>
  /// <param name="products">Список исполнений</param>
  /// <param name="session">Пользовательская сессия</param>
  public void SetGroupProducts(List<ProductInfo> products)
  {
    if (products == null || products.Count == 0)
      return;
    this.ChangeGroupDocumentForm(AVSDocumentForm.A);
    if (this.VariableDataChapter.Chapters.Count > 0)
      this.VariableDataChapter.RemoveChapter(this.VariableDataChapter.Chapters[0], false, false, true, false);
    this.ProductsInfo.Clear();
    this.SortProducts(products);
    List<NewProductParams> newProductParams = new List<NewProductParams>(products.Count);
    for (int index = 0; index < products.Count; ++index)
      newProductParams.Add(new NewProductParams(products[index].Id, -1, products[index].Designation, products[index].Number, index));
    this.InsertNewProducts((IList<NewProductParams>) newProductParams);
    if (this.IsSpecification || this.AvsDocumentForm != AVSDocumentForm.A)
      return;
    this.LoadAllProductsRelations(new AVSDocumentContext(true, (SpecificationSection) null, this.GetAllowableDocumentSections()), new RowDictionariesForLoadDocument());
  }

  /// <summary>Добавить новые исполнения (и создавать по прототипу, если нет указан готовый объект)</summary>
  /// <param name="newProductParams">Список с параметрами новых исполнений</param>
  /// <param name="updateViewNodes">Обновить узлы документа</param>
  public virtual void InsertNewProducts(
    IList<NewProductParams> newProductParams,
    bool updateViewNodes = true)
  {
    List<long> longList = new List<long>(1);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
      for (int index1 = 0; index1 < newProductParams.Count; ++index1)
      {
        ProductInfo productInfoByIndex = this.GetProductInfoByIndex(newProductParams[index1].SrcProductIndex);
        int productType = this.ProductType;
        if (!newProductParams[index1].ProductID.IsUndefinedId())
        {
          IDBObject destinationProduct = sessionKeeper.Session.GetObjectActual(newProductParams[index1].ProductID, true);
          if (destinationProduct.ObjectID > 0L && destinationProduct.ObjectModifyMode != ObjectModifyModes.InBase)
            destinationProduct = destinationProduct.CheckOut();
          newProductParams[index1].ProductID = destinationProduct.ObjectID;
          IDBAttribute attributeById = destinationProduct.GetAttributeByID(AvsIDCache.Attr_Designation);
          if (attributeById == null || attributeById.AsString != newProductParams[index1].ProductDesignation)
            destinationProduct.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(AvsIDCache.Attr_Designation, (object) newProductParams[index1].ProductDesignation)
            });
          IDBRelation relation = sessionKeeper.Session.GetRelation(newProductParams[index1].ProductID, this.DocumentID, AvsIDCache.Relation_Document, true);
          if (relation == null)
          {
            relation = relationCollection.Create(newProductParams[index1].ProductID, this.DocumentID);
            longList.Add(relation.RelationID);
          }
          relation.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this.DocumentID))
          });
          if (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V)
            this.CopyRelationsToProduct(this.FindRowsForCopyInNewProduct(), -1L, destinationProduct);
        }
        else
        {
          string initValue = this.DocumentName;
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(productType);
          long num = this.CheckExistingProductVersion(this.DocFID, newProductParams[index1].ProductDesignation, sessionKeeper.Session);
          IDBObject version;
          if (num.IsDefinedId())
          {
            IGroupInstanceService customService = sessionKeeper.Session.GetCustomService(typeof (IGroupInstanceService)) as IGroupInstanceService;
            Guid sessionGuid = sessionKeeper.Session.SessionGUID;
            customService.AddIgnoreSessionGuid(sessionGuid);
            try
            {
              version = objectCollection.CreateVersion(num);
            }
            finally
            {
              customService.RemoveIgnoreSessionGuid(sessionGuid);
            }
          }
          else if (productInfoByIndex != null)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(productInfoByIndex.Id);
            IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_Name);
            if (attributeById != null)
              initValue = attributeById.Description;
            int objectType = dbObject.ObjectType;
            version = objectCollection.Create(productInfoByIndex.Id);
          }
          else
            version = objectCollection.Create();
          if (this.UseSameDesignationForProducts)
          {
            AttributeValues[] values = new AttributeValues[3]
            {
              new AttributeValues(AvsIDCache.Attr_Designation, (object) newProductParams[index1].ProductDesignation),
              new AttributeValues(AvsIDCache.Attr_Name, (object) initValue),
              new AttributeValues(AvsIDCache.Attr_ProductCode, (object) null)
            };
            DBObjectHelper.SetDBAttributeValues(version, values);
          }
          else
          {
            AttributeValues[] values = new AttributeValues[3]
            {
              new AttributeValues(AvsIDCache.Attr_Designation, (object) newProductParams[index1].ProductDesignation),
              new AttributeValues(AvsIDCache.Attr_Name, (object) initValue),
              new AttributeValues(AvsIDCache.Attr_ProductCode, (object) newProductParams[index1].ProductNumber)
            };
            DBObjectHelper.SetDBAttributeValues(version, values);
          }
          List<AVSRow> avsRowList;
          if (productInfoByIndex == null)
          {
            avsRowList = this.FindRowsForCopyInNewProduct();
          }
          else
          {
            avsRowList = new List<AVSRow>();
            Chapter chapter1 = this.commonDataChapter.GetChapter(AVSDocument.ObjID_SectionDocumentation);
            chapter1?.GetAllRowsList(false, false, avsRowList);
            if (this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
            {
              if (this.variableDataChapter_FormA.GetProductChapter(productInfoByIndex) is ProductVariableDataChapter productChapter)
                chapter1 = productChapter.GetChapter(AVSDocument.ObjID_SectionDocumentation);
              chapter1?.GetAllRowsList(false, false, avsRowList);
            }
            else if (this.AvsDocumentForm == AVSDocumentForm.V && this.variableDataChapter_FormV != null)
            {
              chapter1 = this.variableDataChapter_FormV.GetChapter(AVSDocument.ObjID_SectionDocumentation);
              chapter1?.GetAllRowsList(false, false, avsRowList);
            }
            for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
            {
              if (!this.rootChapters[index2].IsCommonDataChapter && !this.rootChapters[index2].IsVariableDataChapter)
              {
                for (int index3 = 0; index3 < this.rootChapters[index2].Chapters.Count; ++index3)
                {
                  if (this.rootChapters[index2].Chapters[index3].IsVariableDataChapter)
                  {
                    if (this.AvsDocumentForm == AVSDocumentForm.A)
                    {
                      if (this.rootChapters[index2].Chapters[index3] is VariableDataChapterFormA chapter2)
                      {
                        if (chapter2.GetProductChapter(productInfoByIndex) is ProductVariableDataChapter productChapter)
                          chapter1 = productChapter.GetChapter(AVSDocument.ObjID_SectionDocumentation);
                        chapter1?.GetAllRowsList(false, false, avsRowList);
                      }
                    }
                    else if (this.AvsDocumentForm == AVSDocumentForm.V)
                    {
                      chapter1 = this.rootChapters[index2].Chapters[index3].GetChapter(AVSDocument.ObjID_SectionDocumentation);
                      chapter1?.GetAllRowsList(false, false, avsRowList);
                    }
                  }
                }
              }
            }
          }
          long sourceProductID = -1;
          if (productInfoByIndex != null)
            sourceProductID = productInfoByIndex.Id;
          this.CopyRelationsToProduct(avsRowList, sourceProductID, version);
          IDBRelation relation = sessionKeeper.Session.GetRelation(version.ObjectID, this.DocumentID, AvsIDCache.Relation_Document, true);
          if (relation == null)
          {
            relation = relationCollection.Create(version.ObjectID, this.DocumentID);
            longList.Add(relation.RelationID);
          }
          relation.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this.DocumentID))
          });
          if (version.IsCreationMode)
            version.CommitCreation(true, true);
          newProductParams[index1].ProductID = version.ObjectID;
        }
      }
    }
    this.InsertProducts(newProductParams, updateViewNodes);
  }

  /// <summary>Получить записи которые должны копировать связи при добавлении нового исполнения</summary>
  /// <param name="avsRows">Список для сбора записей</param>
  private List<AVSRow> FindRowsForCopyInNewProduct()
  {
    List<AVSRow> rowList = new List<AVSRow>();
    if (this.AvsDocumentForm == AVSDocumentForm.A || this.AvsDocumentForm == AVSDocumentForm.V)
      this.commonDataChapter.GetAllRowsList(true, false, rowList);
    for (int index1 = 0; index1 < this.rootChapters.Count; ++index1)
    {
      if (!this.rootChapters[index1].IsCommonDataChapter && !this.rootChapters[index1].IsVariableDataChapter)
      {
        for (int index2 = 0; index2 < this.rootChapters[index1].Chapters.Count; ++index2)
        {
          if (this.rootChapters[index1].Chapters[index2].IsCommonDataChapter)
            this.rootChapters[index1].Chapters[index2].GetAllRowsList(true, false, rowList);
        }
      }
    }
    return rowList;
  }

  /// <summary>Скопировать связи в новое исполнение</summary>
  /// <param name="avsRows">Записи со связями</param>
  /// <param name="sourceProductID">Исполнение источник. Если прототип не задан, то берётся нулевое исполнение</param>
  /// <param name="destinationProduct">Исполнение приёмник</param>
  private void CopyRelationsToProduct(
    List<AVSRow> avsRows,
    long sourceProductID,
    IDBObject destinationProduct)
  {
    if (avsRows.IsNullOrEmpty<AVSRow>())
      return;
    if (sourceProductID == -1L)
      sourceProductID = this.productsInfo[0].Id;
    IUserSession session = destinationProduct.Session;
    IDBRelationCollection relationCollection1 = session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
    Dictionary<int, IDBRelationCollection> dictionary = new Dictionary<int, IDBRelationCollection>();
    foreach (AVSRow avsRow in avsRows)
    {
      int relationIndexForProduct = avsRow.GetRelationIndexForProduct(sourceProductID);
      if (relationIndexForProduct != -1 && session.GetRelation(destinationProduct.ObjectID, avsRow.ObjectId, true) == null)
      {
        NewRelationProperties relationProperties = new NewRelationProperties(avsRow.Relations[relationIndexForProduct].RelationId, destinationProduct.ObjectID, avsRow.Object_F_ID, DateTime.MinValue, DateTime.MaxValue, avsRow.ObjectId);
        if (avsRow.IsDocRelation)
        {
          AVSDocument.CreateDocRelationWithLockPDMHandler(relationCollection1, relationProperties);
        }
        else
        {
          IDBRelationCollection relationCollection2;
          if (!dictionary.TryGetValue(avsRow.RelType, out relationCollection2))
          {
            relationCollection2 = session.GetRelationCollection(avsRow.RelType, this.FiltrationOwnerID);
            dictionary.Add(avsRow.RelType, relationCollection2);
          }
          relationCollection2.Create(relationProperties);
        }
      }
    }
  }

  /// <summary>Вставить исполнения</summary>
  /// <param name="newProductParams">Список с параметрами новых исполнений</param>
  /// <param name="updateViewNodes">Обновить узлы документа</param>
  public virtual void InsertProducts(IList<NewProductParams> newProductParams, bool updateViewNodes = true)
  {
    if (this.AvsDocumentForm == AVSDocumentForm.Single)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.SuspendDocumentAndGridUpdates();
      try
      {
        AVSDocumentContext loadContext = new AVSDocumentContext();
        RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument(this.SortIndexDictionary);
        for (int index1 = 0; index1 < newProductParams.Count; ++index1)
        {
          if (newProductParams[index1].ProductID != -1L)
          {
            IDBObject productObj = sessionKeeper.Session.GetObject(newProductParams[index1].ProductID, false);
            if (productObj.ObjectID > 0L && productObj.ObjectModifyMode != ObjectModifyModes.InBase)
            {
              productObj = productObj.CheckOut();
              newProductParams[index1].ProductID = productObj.ObjectID;
            }
            if (this.articleGroupID == Guid.Empty)
            {
              foreach (ProductInfo productInfo in this.ProductsInfo)
              {
                if (productInfo.ArticleGroupID != Guid.Empty)
                  this.articleGroupID = productInfo.ArticleGroupID;
              }
              if (this.articleGroupID == Guid.Empty)
                this.articleGroupID = Guid.NewGuid();
              foreach (ProductInfo productInfo in this.ProductsInfo)
                sessionKeeper.Session.GetObject(productInfo.Id).SetAttributesValues(new AttributeValues[1]
                {
                  new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.articleGroupID)
                });
            }
            productObj.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.articleGroupID)
            });
            int index2 = -1;
            if (index1 < newProductParams.Count)
              index2 = newProductParams[index1].ProductIndex;
            ProductInfo product = new ProductInfo(productObj, this.productAttributeList, this.DocumentDesignationSuffix);
            if (this.AutoSort)
              index2 = AVSDocument.FindIndexInSortedList((object) product, (IList) this.productsInfo, true, 0, (IComparer) new ProductsComparer(this));
            if (index2 < 0 || index2 > this.productsInfo.Count)
              index2 = this.productsInfo.Count;
            this.productsInfo.Insert(index2, product);
            if (this.AvsDocumentForm == AVSDocumentForm.A)
            {
              variableDataChapter = (ProductVariableDataChapter) null;
              long sortIndex = 0;
              for (int index3 = index2; index3 < this.productsInfo.Count; ++index3)
              {
                if (this.variableDataChapter_FormA.GetChapter(this.productsInfo[index3].Id) is ProductVariableDataChapter variableDataChapter)
                {
                  int index4 = this.variableDataChapter_FormA.Chapters.IndexOf((Chapter) variableDataChapter);
                  if (index4 == 0)
                  {
                    if (this.variableDataChapter_FormA.Chapters.Count == 1)
                    {
                      this.variableDataChapter_FormA.Chapters[0].SortIndex = 100L;
                      break;
                    }
                    this.variableDataChapter_FormA.Chapters[0].SortIndex /= 2L;
                    break;
                  }
                  if (index4 > 0)
                  {
                    sortIndex = (this.variableDataChapter_FormA.Chapters[index4].SortIndex + this.variableDataChapter_FormA.Chapters[index4 - 1].SortIndex) / 2L;
                    break;
                  }
                  break;
                }
              }
              if (variableDataChapter == null)
                sortIndex = (long) (index2 * 100);
              int num = this.variableDataChapter_FormA.AddChapter((Chapter) new ProductVariableDataChapter(this, this.productsInfo[index2], sortIndex, true), true, false, false, (TableData) null);
              for (int index5 = 0; index5 < this.rootChapters.Count; ++index5)
              {
                if (this.rootChapters[index5].IsAdditionalChapter)
                {
                  for (int index6 = 0; index6 < this.rootChapters[index5].Chapters.Count; ++index6)
                  {
                    if (this.rootChapters[index5].Chapters[index6] is VariableDataChapterFormA chapter)
                      chapter.AddChapter((Chapter) new ProductVariableDataChapter(this, this.productsInfo[index2], sortIndex, true), true, false, false, (TableData) null);
                  }
                }
              }
              if (num != -1)
                loadContext.Chapter = (Chapter) null;
            }
            this.LoadProductData(product, loadContext, rowDicts);
          }
        }
        this.UpdateDocumentStructure(false, false, false);
        this.IndexAVSDocument(true);
        bool reCreateListNode = false;
        if ((this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V) && this.avsWindow != null)
        {
          this.avsWindow.NeedToLoadColumnParams = true;
          if (this.IsGridViewMode)
          {
            reCreateListNode = true;
            this.avsWindow.LoadColumnsStateIfNeeded();
            this.ClearTreeListNodes();
          }
        }
        if (!updateViewNodes)
          return;
        this.UpdateViewNodes(false, reCreateListNode, true, true, true, EmptyRowUpdateMode.DontChange);
        if (reCreateListNode)
          this.RecreateTreeListNodes();
        this.UpdateNoteDocCells(false, false);
        this.UpdateVariableDataCaptions();
      }
      finally
      {
        this.UpdateProductHeadersOnPages(false, false);
        this.ResumeDocumentAndGridUpdates(0, true, true, true, true, true);
        if (this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
        {
          List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
          for (int index = 0; index < newProductParams.Count; ++index)
          {
            Chapter chapter = this.variableDataChapter_FormA.GetChapter(newProductParams[index].ProductID) ?? this.variableDataChapter_FormA.GetChapter(-newProductParams[index].ProductID);
            if (chapter != null && chapter.DocNode != null)
              selection.Add((DocumentTreeNode) chapter.DocNode);
          }
          if (selection.Count > 0 && this.DocumentControl != null)
            this.DocumentControl.SetSelection(selection, true, false);
        }
      }
    }
  }

  /// <summary>Назначить список родительских изделий</summary>
  /// <param name="productIDs">Список идентификаторов изделий</param>
  public void SetProducts(List<long> productIDs)
  {
    List<ProductInfo> products;
    if (productIDs != null && productIDs.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        products = this.LoadProductInfoForObjects(productIDs, this.productAttributeList, (string) null, sessionKeeper.Session);
    }
    else
      products = new List<ProductInfo>();
    this.SetProducts(products);
  }

  /// <summary>Назначить список родительских изделий</summary>
  /// <param name="products">Список идентификаторов изделий</param>
  public void SetProducts(List<ProductInfo> products)
  {
    if (products == null || products.Count == 0)
      return;
    Guid articleGroupId = products[0].ArticleGroupID;
    bool flag = products.Count > 1 && articleGroupId != Guid.Empty;
    for (int index = 1; flag && index < products.Count; ++index)
      flag &= articleGroupId == products[index].ArticleGroupID;
    if (flag || !this.IsSingleForm)
      this.SetGroupProducts(products);
    else
      this.SetParentProducts(products);
  }

  /// <summary>Назначить список родительских изделий</summary>
  /// <param name="productIDs">Список идентификаторов изделий</param>
  /// <param name="session">Пользовательская сессия</param>
  public virtual void SetParentProducts(List<long> productIDs, IUserSession session)
  {
    List<ProductInfo> products = new List<ProductInfo>();
    foreach (long productId in productIDs)
    {
      IDBObject productObj = session.GetObject(productId);
      products.Add(new ProductInfo(productObj));
    }
    this.SetParentProducts(products);
  }

  /// <summary>Назначить список родительских изделий</summary>
  /// <param name="products">Список изделий</param>
  public virtual void SetParentProducts(List<ProductInfo> products)
  {
    if (this.parentProducts == products)
      return;
    List<long> productIds = AVSDocument.GetProductIds(this.parentProducts);
    List<ProductInfo> parentProducts = this.parentProducts;
    this.parentProducts = products;
    this.SuspendDocumentAndGridUpdates();
    try
    {
      for (int index = 0; index < this.parentProducts.Count; ++index)
      {
        if (!productIds.Contains(this.parentProducts[index].Id))
          this.LoadProductData(this.parentProducts[index]);
      }
      foreach (ProductInfo product in parentProducts)
      {
        if (this.GetParentProductIndex(product.Id) == -1)
          this.RemoveParentProduct(product);
      }
      this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(-1, true, true, true, true);
    }
    this.SaveParentProductsToImDocument();
  }

  /// <summary>Удалить исполнение</summary>
  /// <param name="product">Ид. версии исполнения</param>
  /// <param name="updateDoc">Обновить документ</param>
  /// <param name="updateGrid">Обновить табличный вид</param>
  public List<KeyValuePair<long, RelInfo>> RemoveProductVersion(
    ProductInfo product,
    bool updateDoc,
    bool updateGrid)
  {
    return this.RemoveProductVersions((IList<ProductInfo>) new ProductInfo[1]
    {
      product
    }, updateDoc, updateGrid);
  }

  /// <summary>Удалить исполнения</summary>
  /// <param name="products">Список ид. версий исполнений</param>
  /// <param name="updateDoc">Обновить документ</param>
  /// <param name="updateGrid">Обновить табличный вид</param>
  public List<KeyValuePair<long, RelInfo>> RemoveProductVersions(
    IList<ProductInfo> products,
    bool updateDoc,
    bool updateGrid)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = new List<KeyValuePair<long, RelInfo>>();
    if (this.productsInfo.Count > 1)
    {
      for (int index1 = this.productsInfo.Count - 1; index1 >= 0; --index1)
      {
        if (products.Contains(this.productsInfo[index1]))
        {
          long id = this.productsInfo[index1].Id;
          if (id != -1L)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              sessionKeeper.Session.GetRelation(id, this.DocumentID, AvsIDCache.Relation_Document, true)?.Delete(0L);
              sessionKeeper.Session.GetObject(Math.Abs(id), false)?.Delete(0L);
            }
          }
          if (this.variableDataChapter_FormA != null)
          {
            Chapter productChapter1 = this.variableDataChapter_FormA.GetProductChapter(this.productsInfo[index1]);
            if (productChapter1 != null)
            {
              List<KeyValuePair<long, RelInfo>> collection = this.variableDataChapter_FormA.RemoveChapter(productChapter1, false, false, updateDoc, updateGrid);
              keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
            }
            for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
            {
              if (!this.rootChapters[index2].IsCommonDataChapter && !this.rootChapters[index2].IsVariableDataChapter)
              {
                for (int index3 = 0; index3 < this.rootChapters[index2].Chapters.Count; ++index3)
                {
                  if (this.rootChapters[index2].Chapters[index3] is VariableDataChapterFormA chapter)
                  {
                    Chapter productChapter2 = chapter.GetProductChapter(this.productsInfo[index1]);
                    if (productChapter2 != null)
                    {
                      List<KeyValuePair<long, RelInfo>> collection = chapter.RemoveChapter(productChapter2, false, false, updateDoc, updateGrid);
                      keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
                    }
                  }
                }
              }
            }
          }
          this.productsInfo.RemoveAt(index1);
        }
      }
      List<long> productIds = AVSDocument.GetProductIds(new List<ProductInfo>((IEnumerable<ProductInfo>) products));
      List<AVSRow> rowList = new List<AVSRow>();
      this.commonDataChapter.GetAllRowsList(true, false, rowList);
      if (this.variableDataChapter_FormV != null)
        this.variableDataChapter_FormV.GetAllRowsList(true, false, rowList);
      for (int index = 0; index < rowList.Count; ++index)
      {
        if (rowList[index].Relations != null && rowList[index].Relations.Count > 0)
        {
          rowList[index].RemoveProducts((IList<long>) productIds, updateDoc, updateGrid);
          if (!rowList[index].IsFormB && (rowList[index].Relations == null || rowList[index].Relations.Count == 0))
          {
            List<KeyValuePair<long, RelInfo>> collection = rowList[index].Section.RemoveRow(rowList[index], false, false, updateDoc, updateGrid, true);
            keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
          }
        }
      }
      for (int index4 = 0; index4 < this.rootChapters.Count; ++index4)
      {
        if (!this.rootChapters[index4].IsCommonDataChapter && !this.rootChapters[index4].IsVariableDataChapter)
        {
          for (int index5 = 0; index5 < this.rootChapters[index4].Chapters.Count; ++index5)
          {
            if (this.rootChapters[index4].Chapters[index5].IsCommonDataChapter)
            {
              rowList.Clear();
              this.rootChapters[index4].Chapters[index5].GetAllRowsList(true, false, rowList);
              for (int index6 = 0; index6 < rowList.Count; ++index6)
              {
                if (rowList[index6].Relations != null && rowList[index6].Relations.Count > 0)
                {
                  rowList[index6].RemoveProducts((IList<long>) productIds, updateDoc, updateGrid);
                  if (!rowList[index6].IsFormB && (rowList[index6].Relations == null || rowList[index6].Relations.Count == 0))
                  {
                    List<KeyValuePair<long, RelInfo>> collection = rowList[index6].Section.RemoveRow(rowList[index6], false, false, updateDoc, updateGrid, true);
                    keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
                  }
                }
              }
            }
          }
        }
      }
      this.UpdateProductHeadersOnPages(false, false);
    }
    return keyValuePairList;
  }

  /// <summary>Удалить из документа все родительские изделия и записи по их составу</summary>
  public void RemoveParentProducts()
  {
    foreach (ProductInfo parentProduct in this.ParentProducts)
      this.RemoveParentProduct(parentProduct);
    this.ParentProducts.Clear();
  }

  /// <summary>Удалить из документа все родительские изделия и записи по их составу</summary>
  /// <param name="product">Родительское изделие</param>
  public void RemoveParentProduct(ProductInfo product)
  {
    if (product == null)
      throw new ArgumentNullException(nameof (product));
    foreach (AVSRow allRow in this.GetAllRows(true, true))
    {
      if (allRow.HasRelation && allRow.Relations[0].ProjectGuid == product.Guid)
        allRow.Remove(removeRelation: false);
    }
  }

  /// <summary>Зарегистрировать связь с объектом, принадлежащие записи в словарях</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь в конструкторском документе</param>
  public void RegisterAVSRowRelationWithObjectInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    this.RegisterAVSRowRelationInDictionaries(row, relation);
    this.RegisterAVSRowObjectInDictionaries(row);
  }

  /// <summary>Зарегистрировать связь, принадлежащую записи в словарях</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь в записи конструкторского документа</param>
  public void RegisterAVSRowRelationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    if (row == null)
      return;
    if (this.relationDictionary == null)
      this.relationDictionary = new Dictionary<long, AVSRow>();
    if (this.relationGuidDictionary == null)
      this.relationGuidDictionary = new Dictionary<Guid, AVSRow>();
    if (relation == null)
      return;
    row.sortIndex = row.SortIndex;
    AVSRow avsRow;
    if (!this.relationDictionary.TryGetValue(relation.RelationId, out avsRow))
      this.relationDictionary.Add(relation.RelationId, row);
    else if (avsRow != row)
      throw new Exception($"Повторная регистрация связи [{relation.RelationId}] в записях [SI1: {avsRow.SortIndex}, DocRowId1: \"{(avsRow.DocNode != null ? (object) avsRow.DocNode.Id : (object) "")}\"; SI2: {row.SortIndex}, DocRowId2: \"{(row.DocNode != null ? (object) row.DocNode.Id : (object) "")}\"]");
    if (!this.relationGuidDictionary.TryGetValue(relation.RelationGuid, out avsRow))
      this.relationGuidDictionary.Add(relation.RelationGuid, row);
    else if (avsRow != row)
      throw new Exception($"Повторная регистрация связи {{{relation.RelationGuid}}} в записях [SortIndex1: {avsRow.SortIndex}, DocRowId1: \"{(avsRow.DocNode != null ? (object) avsRow.DocNode.Id : (object) "")}\"; SortIndex2: {row.SortIndex}, DocRowId2: \"{(row.DocNode != null ? (object) row.DocNode.Id : (object) "")}\"]");
    if (row.RelType == AvsIDCache.Relation_Podbor)
      this.RegisterPodborForPosDesignationInDictionaries(row, relation);
    if (row.RelType != AvsIDCache.Relation_Project)
      return;
    this.RegisterPosDesignationInDictionaries(row, relation);
  }

  /// <summary>Зарегистрировать объект записи в словарях</summary>
  /// <param name="row">Запись конструкторского документа</param>
  internal void RegisterAVSRowObjectInDictionaries(AVSRow row)
  {
    if (row == null)
      return;
    if (this.objectDictionary == null)
      this.objectDictionary = new Dictionary<long, List<AVSRow>>();
    if (this.objectGuidDictionary == null)
      this.objectGuidDictionary = new Dictionary<Guid, List<AVSRow>>();
    List<AVSRow> avsRowList1 = (List<AVSRow>) null;
    long objectId = row.ObjectId;
    if (objectId != -1L)
    {
      if (!this.objectDictionary.TryGetValue(objectId, out avsRowList1))
        this.objectDictionary.Add(objectId, avsRowList1 = new List<AVSRow>(1));
      else if (avsRowList1 == null)
        this.objectDictionary[objectId] = avsRowList1 = new List<AVSRow>(1);
      if (avsRowList1 != null && !avsRowList1.Contains(row))
        avsRowList1.Add(row);
    }
    Guid objGuid = row.ObjGuid;
    List<AVSRow> avsRowList2 = (List<AVSRow>) null;
    if (!(objGuid != Guid.Empty))
      return;
    if (!this.objectGuidDictionary.TryGetValue(objGuid, out avsRowList2))
      this.objectGuidDictionary.Add(objGuid, avsRowList2 = new List<AVSRow>(1));
    else if (avsRowList2 == null)
      this.objectGuidDictionary[objGuid] = avsRowList2 = new List<AVSRow>(1);
    if (avsRowList2 == null || avsRowList2.Contains(row))
      return;
    avsRowList2.Add(row);
  }

  /// <summary>Зарегистрировать запись в словарях</summary>
  /// <param name="row">Запись конструкторского документа</param>
  public void RegisterAVSRowInDictionaries(AVSRow row)
  {
    if (row == null)
      return;
    if (!row.HasRelation && row.RelGuid != Guid.Empty && !this.relationGuidDictionary.TryGetValue(row.RelGuid, out AVSRow _))
      this.relationGuidDictionary.Add(row.RelGuid, row);
    foreach (RelationAttributeValuesCache allRelation in row.GetAllRelations())
      this.RegisterAVSRowRelationInDictionaries(row, allRelation);
    this.RegisterAVSRowObjectInDictionaries(row);
    if (!this.IsSpecification)
      return;
    row.sortIndex = row.SortIndex;
    if (row.sortIndex == 0L || row.sortIndex == long.MinValue)
      return;
    AVSRow avsRow;
    if (this.SortIndexDictionary.TryGetValue(row.sortIndex, out avsRow))
    {
      if (avsRow == row)
        return;
      row.SortIndex = long.MinValue;
    }
    else
      this.SortIndexDictionary.Add(row.sortIndex, row);
  }

  /// <summary>Зарегистрировать запись с подбором для позиционного обозначения в словаре</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь</param>
  internal void RegisterPodborForPosDesignationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    foreach (PosDesignationRecord designationRecord in PosDesignationRecord.ParsePositionalDesignation(relation.GetValueString(this.Attr_PodborForPosDesignation, false)))
    {
      List<RelationAttributeValuesCache> attributeValuesCacheList;
      if (!this.PodborForPosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out attributeValuesCacheList))
        this.PodborForPosDesignation_Dictionary.Add(designationRecord.Designation, attributeValuesCacheList = new List<RelationAttributeValuesCache>());
      if (!attributeValuesCacheList.Contains(relation))
        attributeValuesCacheList.Add(relation);
    }
  }

  /// <summary>Зарегистрировать запись с позиционными обозначениями в словаре</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь</param>
  internal void RegisterPosDesignationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    foreach (PosDesignationRecord designationRecord in PosDesignationRecord.ParsePositionalDesignation(relation.GetValueString(this.Field_PosDesignation, false)))
    {
      RelationAttributeValuesCache attributeValuesCache;
      if (!this.PosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out attributeValuesCache))
        this.PosDesignation_Dictionary.Add(designationRecord.Designation, relation);
      else if (attributeValuesCache != relation)
        this.PosDesignation_Dictionary[designationRecord.Designation] = relation;
    }
  }

  /// <summary>Удалить из словарей запись конструкторского документа</summary>
  /// <param name="row">Запись конструкторского документа</param>
  public void UnregisterSpecRowInDictionaries(AVSRow row)
  {
    if (row == null)
      return;
    foreach (RelationAttributeValuesCache allRelation in row.GetAllRelations())
      this.UnregisterAVSRowRelationInDictionaries(row, allRelation);
    this.UnregisterAVSRowObjectInDictionaries(row);
    AVSRow avsRow;
    if (row.SortIndex == 0L || row.SortIndex == long.MinValue || !this.SortIndexDictionary.TryGetValue(row.SortIndex, out avsRow) || avsRow != row)
      return;
    this.SortIndexDictionary.Remove(row.SortIndex);
  }

  /// <summary>Удалить из словарей связь</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь в записи</param>
  public void UnregisterAVSRowRelationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    if (row == null)
      return;
    this.relationDictionary.Remove(relation.RelationId);
    this.relationGuidDictionary.Remove(relation.RelationGuid);
    this.UnregisterPodborForPosDesignationInDictionaries(row, relation);
    this.UnregisterPosDesignationInDictionaries(row, relation);
  }

  /// <summary>Удалить из словарей связь</summary>
  /// <param name="row">Запись конструкторского документа</param>
  public void UnregisterAVSRowObjectInDictionaries(AVSRow row)
  {
    if (row == null)
      return;
    List<AVSRow> avsRowList1;
    if (this.objectDictionary.TryGetValue(row.ObjectId, out avsRowList1) && avsRowList1 != null)
    {
      avsRowList1.Remove(row);
      if (avsRowList1.Count == 0)
        this.objectDictionary.Remove(row.ObjectId);
    }
    List<AVSRow> avsRowList2;
    if (!this.objectGuidDictionary.TryGetValue(row.ObjGuid, out avsRowList2) || avsRowList2 == null)
      return;
    avsRowList2.Remove(row);
    if (avsRowList2.Count != 0)
      return;
    this.objectGuidDictionary.Remove(row.ObjGuid);
  }

  /// <summary>Удалить из словарей запись с подбором для позиционного обозначения</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь в записи</param>
  internal void UnregisterPodborForPosDesignationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    foreach (PosDesignationRecord designationRecord in PosDesignationRecord.ParsePositionalDesignation(relation.GetValueString(this.Attr_PodborForPosDesignation, false)))
    {
      List<RelationAttributeValuesCache> attributeValuesCacheList;
      if (this.PodborForPosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out attributeValuesCacheList))
      {
        if (attributeValuesCacheList.Contains(relation))
          attributeValuesCacheList.Remove(relation);
        if (attributeValuesCacheList.Count == 0)
          this.PodborForPosDesignation_Dictionary.Remove(designationRecord.Designation);
      }
    }
  }

  /// <summary>Удалить из словарей запись с позиционным обозначением</summary>
  /// <param name="row">Запись конструкторского документа</param>
  /// <param name="relation">Связь в записи</param>
  internal void UnregisterPosDesignationInDictionaries(
    AVSRow row,
    RelationAttributeValuesCache relation)
  {
    foreach (PosDesignationRecord designationRecord in PosDesignationRecord.ParsePositionalDesignation(relation.GetValueString(this.Field_PosDesignation, false)))
    {
      if (this.PosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out RelationAttributeValuesCache _))
        this.PosDesignation_Dictionary.Remove(designationRecord.Designation);
    }
  }

  /// <summary>Найти свободный индекс сортировки</summary>
  /// <param name="sortIndex">Индекс с которого нужно начать поиск. Если он свободен, то метод вернёт его</param>
  /// <returns>Свободный индекс</returns>
  internal long FindNextFreeSortIndex(long sortIndex)
  {
    long key = sortIndex;
    switch (key)
    {
      case long.MinValue:
      case 0:
        key = 1L;
        break;
    }
    long num = 1;
    if (key < 0L || key == long.MaxValue)
      num = -1L;
    while (this.SortIndexDictionary.TryGetValue(key, out AVSRow _))
    {
      key += num;
      if (key == long.MaxValue)
      {
        if (sortIndex != 0L)
        {
          key = 1L;
          sortIndex = 0L;
        }
        else
          break;
      }
      if (key == long.MinValue)
      {
        if (sortIndex != 0L)
        {
          key = -1L;
          sortIndex = 0L;
        }
        else
          break;
      }
    }
    return key;
  }

  /// <summary>Удалить связь</summary>
  /// <param name="specRow">Запись спецификации</param>
  /// <param name="relationID">Идентификатор связи</param>
  public void RemoveRelation(AVSRow specRow, long relationID)
  {
    if (specRow == null)
      specRow = this.GetAvsDocRow(relationID);
    if (specRow == null)
      return;
    int relationIndex = -1;
    bool flag = false;
    if (specRow.HasRelation)
    {
      for (int index = 0; index < specRow.Relations.Count; ++index)
      {
        if (specRow.Relations[index].RelationId == relationID)
        {
          relationIndex = index;
          break;
        }
      }
    }
    if (relationIndex == -1 && specRow.HasHiddenRelation)
    {
      for (int index = 0; index < specRow.HiddenRelations.Count; ++index)
      {
        if (specRow.HiddenRelations[index].RelationId == relationID)
        {
          flag = true;
          relationIndex = index;
          break;
        }
      }
    }
    if (relationIndex == -1)
      return;
    if (!flag && specRow.Relations.Count == 1)
      specRow.Section.RemoveRow(specRow, true, true, true, true, false);
    else
      specRow.RemoveRelationData(flag ? specRow.HiddenRelations : specRow.Relations, relationIndex);
  }

  internal List<KeyValuePair<long, RelInfo>> RemovePodborForPosDesignationRows(
    List<string> posDesignations)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = new List<KeyValuePair<long, RelInfo>>();
    if (posDesignations.IsNullOrEmpty<string>())
      return keyValuePairList;
    foreach (AVSRow avsRow in this.GetRows(true, true).Where<AVSRow>((System.Func<AVSRow, bool>) (r => r.RelType == AvsIDCache.Relation_Podbor && posDesignations.Contains(r.GetFieldStringValue(this.Attr_PodborForPosDesignation, 0, -1, (List<RelationAttributeValuesCache>) null, false)))).ToArray<AVSRow>())
    {
      List<KeyValuePair<long, RelInfo>> collection = avsRow.Remove();
      keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
    }
    return keyValuePairList;
  }

  /// <summary>Идентификатор версии основного исполнения</summary>
  public long ProductId
  {
    [DebuggerStepThrough] get => this.productId;
    set => this.productId = value;
  }

  /// <summary>Глобальный идентификатор версии основного исполнения</summary>
  public Guid FirstProductGuid
  {
    [DebuggerStepThrough] get
    {
      return this.ProductsInfo != null && this.ProductsInfo.Count > 0 ? this.ProductsInfo[0].Guid : Guid.Empty;
    }
  }

  /// <summary>Тип специфицируемого изделия</summary>
  public int ProductType
  {
    [DebuggerStepThrough] get => this.productType;
    set => this.productType = value;
  }

  /// <summary>Получить список атрибутов исполнений для документа</summary>
  /// <param name="attrsList">Список в который будут добавлены требуемые атрибуты</param>
  public void GetProductAttrsInfoForDocument(List<int> attrsList)
  {
    ProductInfo productInfo = (ProductInfo) null;
    if (this.productsInfo != null && this.productsInfo.Count > 0)
      productInfo = this.productsInfo[0];
    if (attrsList == null)
      throw new ArgumentNullException(nameof (attrsList));
    if ((this.IsFormB || this.AvsDocumentForm == AVSDocumentForm.V) && this.avsDocTableTemplate != null)
    {
      TableData kodAndLiteraTable = this.FindProductKodAndLiteraTable(this.avsDocTableTemplate.Page);
      if (kodAndLiteraTable != null)
      {
        int index = 0;
        for (int count = kodAndLiteraTable.Nodes.Count; index < count; ++index)
        {
          if (kodAndLiteraTable.Nodes[index] is TableData node1 && node1.IsRow && node1.Nodes.Count > 0 && node1.Nodes[0] is TextData node2)
          {
            if (node2.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
              referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, DBHelper.GetAttributeTypeIDFromAttributeGuid(referenceToTextSource.AttributeGuid), referenceToTextSource.AttributeName);
            if (referenceToTextSource != null && referenceToTextSource.AttributeID != -1 && referenceToTextSource.AttributeID != AvsIDCache.Attr_Litera && referenceToTextSource.AttributeID != AvsIDCache.Attr_ProductConventionalName && !attrsList.Contains(referenceToTextSource.AttributeID) && productInfo != null && !productInfo.HasAttribute(referenceToTextSource.AttributeID))
              attrsList.Add(referenceToTextSource.AttributeID);
          }
        }
      }
    }
    if (this.avsDocumentType != AVSDocumentType.AutoIndustrySpecification || this.AvsDocumentForm != AVSDocumentForm.A)
      return;
    if (!attrsList.Contains(AvsIDCache.Attr_OKPCode) && productInfo != null && !productInfo.HasAttribute(AvsIDCache.Attr_OKPCode))
      attrsList.Add(AvsIDCache.Attr_OKPCode);
    if (this.variableDataChapterTemplate == null)
      return;
    DocumentTreeNode node3 = this.variableDataChapterTemplate.FindNode(Chapter.ProductCaptionRowID);
    if (node3 == null || node3.NodesCount <= 0)
      return;
    for (int index = 0; index < node3.NodesCount; ++index)
    {
      if (node3.Nodes[index] is TextData node4)
      {
        if (node4.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
          referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, DBHelper.GetAttributeTypeIDFromAttributeGuid(referenceToTextSource.AttributeGuid), referenceToTextSource.AttributeName);
        if (referenceToTextSource != null && referenceToTextSource.AttributeID != -1 && !attrsList.Contains(referenceToTextSource.AttributeID) && productInfo != null && !productInfo.HasAttribute(referenceToTextSource.AttributeID))
          attrsList.Add(referenceToTextSource.AttributeID);
      }
    }
  }

  /// <summary>Обновить список граф в строке документа спецификации</summary>
  public void UpdateDocumentRowFieldsInfo()
  {
    AVSDocument.UpdateDocumentRowFieldsInfo(this.avsRowTemplate, this.AVSDocType, this.IsFormB, ref this.docRowFields, ref this.docRowAttributes);
    this.Field_Name = this.FindNameFieldInRow(this.avsRowTemplate, this.docRowFields);
    this.Field_Note = this.FindNoteFieldInRow(this.docRowFields);
    if (this.IsExportSP)
      AVSDocument.UpdateDocumentRowFieldsInfo(this.avsRowExpTemplate, this.AVSDocType, true, ref this.docRowFields_Exp, ref this.docRowAttributes);
    if (this.AvsDocumentForm == AVSDocumentForm.V)
    {
      AVSDocument.UpdateDocumentRowFieldsInfo(this.avsRowFormBTemplate, this.AVSDocType, true, ref this.docRowFields_VarFormV, ref this.docRowAttributes);
      this.RowProductCount = AVSRow.CalcCountCellsCount(this.docRowFields_VarFormV);
    }
    else
      this.RowProductCount = AVSRow.CalcCountCellsCount(this.docRowFields);
  }

  /// <summary>Найти графу Наименование в списке полей</summary>
  /// <param name="specRowTemplate">Шаблон строки спецификации в документе</param>
  /// <param name="docRowFields">Список полей</param>
  internal AvsRowAttributeInfo FindNameFieldInRow(
    TableData specRowTemplate,
    List<AvsRowAttributeInfo> docRowFields)
  {
    AvsRowAttributeInfo nameFieldInRow1 = (AvsRowAttributeInfo) null;
    AvsRowAttributeInfo nameFieldInRow2 = (AvsRowAttributeInfo) null;
    foreach (AvsRowAttributeInfo docRowField in docRowFields)
    {
      if (docRowField != null)
      {
        if (AvsIDCache.StdField_Name.Equals((AttributeInfo) docRowField))
        {
          nameFieldInRow1 = docRowField;
          break;
        }
        if (nameFieldInRow2 == null && docRowField.Name == AvsIDCache.DocAttr_Name)
          nameFieldInRow2 = docRowField;
      }
    }
    if (nameFieldInRow1 != null)
      return nameFieldInRow1;
    if (nameFieldInRow2 != null)
      return nameFieldInRow2;
    int index = -1;
    foreach (TextData textData in (IEnumerable<TextData>) specRowTemplate.TextCellsEnumerator)
    {
      ++index;
      if (textData.Name == AvsIDCache.DocAttr_Name)
        return docRowFields[index];
    }
    return AvsIDCache.StdField_Name;
  }

  /// <summary>Найти графу Примечание в списке полей</summary>
  /// <param name="docRowFields">Список полей</param>
  private AvsRowAttributeInfo FindNoteFieldInRow(List<AvsRowAttributeInfo> docRowFields)
  {
    AvsRowAttributeInfo noteFieldInRow = (AvsRowAttributeInfo) null;
    AvsRowAttributeInfo rowAttributeInfo = (AvsRowAttributeInfo) null;
    foreach (AvsRowAttributeInfo docRowField in docRowFields)
    {
      if (docRowField != null)
      {
        if (this.Attr_Note.Equals((AttributeInfo) docRowField))
        {
          noteFieldInRow = docRowField;
          break;
        }
        if (rowAttributeInfo == null && docRowField.Name == AVSRow.DocAttr_Note)
          rowAttributeInfo = docRowField;
      }
    }
    if (noteFieldInRow != null)
      return noteFieldInRow;
    if (rowAttributeInfo == null)
      return this.Attr_Note;
    int index = docRowFields.IndexOf(rowAttributeInfo);
    docRowFields[index] = this.Attr_Note;
    return docRowFields[index];
  }

  /// <summary>Обновить список граф в строке документа спецификации</summary>
  /// <param name="specRowTemplate">Шаблон строки спецификации в документе</param>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <param name="isFormBRow">Запись для документа формы Б</param>
  /// <param name="docRowFields">Поля отображаемые в бумажном виде спецификации</param>
  /// <param name="docRowAttributes">Поля отображаемые в бумажном виде спецификации</param>
  public static void UpdateDocumentRowFieldsInfo(
    TableData specRowTemplate,
    AVSDocumentType docType,
    bool isFormBRow,
    ref List<AvsRowAttributeInfo> docRowFields,
    ref List<AvsRowAttributeInfo> docRowAttributes)
  {
    if (specRowTemplate == null)
      throw new Exception("Не назначен шаблон строки документа!");
    if (docRowFields != null)
      docRowFields.Clear();
    else
      docRowFields = new List<AvsRowAttributeInfo>(specRowTemplate.Nodes.Count);
    bool isElementList = AVSDocumentsSettings.IsElementListDocType(docType);
    int cellIndex = -1;
    foreach (TextData cell in (IEnumerable<TextData>) specRowTemplate.TextCellsEnumerator)
    {
      ++cellIndex;
      AvsRowAttributeInfo attrInfo = AVSRow.ConvertOldCellDocAttrInfo(AVSDocument.GetAttrInfoFromCell(cell, cellIndex, isFormBRow), cell, isElementList);
      docRowFields.Add(attrInfo);
      cell.AssignReplaceOldAVSSpecChars(true, true);
      if (AVSDocumentsSettings.IsSpecificationDocType(docType) && (AvsIDCache.StdField_Name.Equals((AttributeInfo) attrInfo) || attrInfo.Name == AvsIDCache.DocAttr_Name) && !cell.IsOverridden3(OverrideFlags3.ReplaceAVSMaterial))
        cell.AssignReplaceAVSMaterial(true, true);
    }
    if (docRowAttributes != null)
      docRowAttributes.Clear();
    else
      docRowAttributes = new List<AvsRowAttributeInfo>();
    AVSDocument.GetDocRowAttributes(specRowTemplate, docRowAttributes);
  }

  public static void GetDocRowAttributes(
    TableData specRowTemplate,
    List<AvsRowAttributeInfo> docRowAttributes)
  {
    StringCollection attributeNames = specRowTemplate.GetAttributeNames(false);
    attributeNames.Remove(AVSRow.RowAttr_SortIndex);
    attributeNames.Remove(Chapter.DocNodeType_AttributeName);
    for (int index = 0; index < attributeNames.Count; ++index)
      docRowAttributes.Add(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, attributeNames[index]));
  }

  /// <summary>Кэш для AttributeProcessor</summary>
  public AttributeProcessorDictionary AttributeProcessorDictionary
  {
    [DebuggerStepThrough] get => this.attributeProcessorDictionary;
  }

  /// <summary>Получить индекс исполнения по его обозначению</summary>
  /// <param name="attrValueStr"> Обозначение исполнения </param>
  /// <returns> индекс в списке исполнений. -1 Если не найден</returns>
  internal int GetProductIndexByHisCaption(string attrValueStr)
  {
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      if (this.productsInfo[index].Designation == attrValueStr)
        return index;
    }
    return -1;
  }

  /// <summary>Получить контекст для вставки элемента по известным параметрам</summary>
  /// <param name="activePartID"> Идентификатор части. -1 если в общая часть </param>
  /// <param name="activeSectionID"> Идентификатор раздела спецификации. -1 если хз что </param>
  /// <param name="indexOfCurrentProduct"> Индекс исполнения. -1 если общие данные </param>
  /// <returns></returns>
  internal DocumentTreeNode[] GetContextNodes(
    long activePartID,
    long activeSectionID,
    int indexOfCurrentProduct)
  {
    Chapter chapter = this.commonDataChapter;
    DocumentTreeNode docNode = (DocumentTreeNode) this.commonDataChapter.DocNode;
    if (indexOfCurrentProduct != -1 && indexOfCurrentProduct < this.productsInfo.Count && this.AvsDocumentForm == AVSDocumentForm.A && this.variableDataChapter_FormA != null)
    {
      chapter = this.variableDataChapter_FormA.GetChapter(this.productsInfo[indexOfCurrentProduct].Id);
      if (chapter != null)
      {
        if (chapter.DocNode == null)
          chapter.UpdateViewNodes((SkipLinesSchema) null, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
        if (chapter.DocNode != null)
          docNode = (DocumentTreeNode) chapter.DocNode;
      }
      else
        chapter = this.commonDataChapter;
    }
    if (!activeSectionID.IsUndefinedId() && chapter != null)
      docNode = (DocumentTreeNode) chapter.GetChapter(activeSectionID).DocNode;
    if (docNode == null)
      return new DocumentTreeNode[0];
    return new DocumentTreeNode[1]{ docNode };
  }

  /// <summary>Назначить обозначение исполнению в СП</summary>
  /// <param name="product">Исполнение</param>
  /// <param name="productDesignation">Обозначение</param>
  /// <param name="productNumber">Номер исполнения</param>
  /// <param name="updateDoc">Обновлять документ</param>
  public void SetProductDesignation(
    ProductInfo product,
    string productDesignation,
    string productNumber,
    bool updateDoc)
  {
    int productIndex = this.GetProductIndex(product);
    if (productIndex < 0)
      throw new Exception($"Не найдено исполнение: {product.Designation}");
    this.productsInfo[productIndex].Designation = productDesignation;
    this.productsInfo[productIndex].SetNumber(productNumber, false);
    if (!updateDoc)
      return;
    this.UpdateProductHeadersOnPages(true, true);
  }

  internal void SaveExpanded()
  {
    if (this.AVSWindow == null || this.AVSWindow.virtualTree == null)
      return;
    this.AVSWindow.virtualTree.SaveExpanded();
  }

  /// <summary>Очистить переменные данные формы В</summary>
  private void ClearVariableDataChapter_FormV()
  {
    this.ClearVariableDataChapter_FormV((Chapter) this.VariableDataChapter_FormV);
    this.VariableDataChapter_FormV = (VariableDataChapterFormV) null;
    this.avsFormB_Table = (TableData) null;
  }

  /// <summary>Очистить переменные данные формы В</summary>
  /// <param name="variableDataChapter">Раздел переменных данных</param>
  private void ClearVariableDataChapter_FormV(Chapter variableDataChapter)
  {
    if (variableDataChapter == null)
      return;
    variableDataChapter.Chapters.Clear();
    for (int index = 0; index < variableDataChapter.DocNodes.Count; ++index)
    {
      variableDataChapter.DocNodes[index].UniteTable();
      variableDataChapter.DocNodes[index].Remove(false, false);
      variableDataChapter.DocNodes[index].Dispose();
    }
    variableDataChapter.DocNodes = new List<TableData>();
  }

  /// <summary>Получить заголовок объекта БД</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns></returns>
  internal string GetObjectCaption(long objectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(objectID).Caption;
  }

  internal void EnterPendingRelationUpdateMode() => ++this.pendingAttributeUpdateModeCounter;

  internal void ExitPendingRelationUpdateMode()
  {
    if (this.pendingAttributeUpdateModeCounter <= 0)
      throw new ValueOutOfRangeException("pendingAttributeUpdateModeCounter", "Значение счетчика не может быть меньше 0.");
    try
    {
      if (this.pendingAttributeUpdateModeCounter != 1 || this.PendingRelationUpdates.IsEmpty)
        return;
      this.PendingRelationUpdates.CommitAll();
    }
    finally
    {
      --this.pendingAttributeUpdateModeCounter;
    }
  }

  /// <summary>Проверяет, заполнено ли количество у записи СП хотя бы по одному исполнению</summary>
  public bool HasCountForAnyProduct(AVSRow row)
  {
    bool flag = false;
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      int relationIndexForProduct = row.GetRelationIndexForProduct(this.productsInfo[index].Id);
      if (relationIndexForProduct != -1 && row.GetFieldValue(this.Field_Count, relationIndexForProduct, index, true, false) != null)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>Проверяет для формы Б, все ли единицы измерения соответствуют примечанию</summary>
  public bool AllProductMeasuresMatchNote(AVSRow row)
  {
    if (!this.IsFormB)
      return true;
    string str = "";
    if (row.DocNode != null)
      str = row.DocNode.GetAttributeValue(row.Field_Note.Name, false);
    if (string.IsNullOrWhiteSpace(str))
      str = new AvsRowData(row).GetFieldStringValue(row.Field_Note, false);
    if (string.IsNullOrWhiteSpace(str))
      return true;
    List<string> measures = ((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures).Select<MeasureDescriptor, string>((System.Func<MeasureDescriptor, string>) (d => d.ShortName.ToLower())).ToList<string>();
    List<string> list = ((IEnumerable<string>) str.ToLower().Split(new char[3]
    {
      ' ',
      ',',
      '.'
    }, StringSplitOptions.RemoveEmptyEntries)).Where<string>((System.Func<string, bool>) (w => measures.Contains(w))).ToList<string>();
    if (list.Count == 0)
      return true;
    bool flag = true;
    for (int index = 0; index < this.productsInfo.Count; ++index)
    {
      int relationIndexForProduct = row.GetRelationIndexForProduct(this.productsInfo[index].Id);
      if (relationIndexForProduct != -1 && row.GetFieldValue(this.Field_Count, relationIndexForProduct, index, true, false) is MeasuredValue fieldValue)
      {
        string lower = MeasureHelper.Instance.FindDescriptor(fieldValue.MeasureID)?.ShortName?.ToLower();
        if (lower != null && !list.Contains(lower))
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  /// <summary>
  /// Получить предпочтительный номер группы заменителей для исполнения
  /// </summary>
  /// <returns></returns>
  internal long GetDesiredSubstituteGroupNumber(List<AVSRow> context)
  {
    long substituteGroupNumber = -1;
    if (context == null || context.Count == 0)
      return substituteGroupNumber;
    foreach (AVSRow allRow in this.GetAllRows(false, false))
    {
      if (allRow.HasRelation)
      {
        foreach (AttributeValuesCache relation in allRow.Relations)
        {
          long valueInt64 = relation.GetValueInt64(AvsIDCache.Attr_DopZamenGroupNum, false);
          if (valueInt64 > substituteGroupNumber)
            substituteGroupNumber = valueInt64;
        }
      }
    }
    long num;
    return substituteGroupNumber <= 0L ? substituteGroupNumber : (num = substituteGroupNumber + 1L);
  }

  private bool CheckOldDocPassport()
  {
    if (this.DocumentID.IsUndefinedId())
      return false;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.DocumentID).GetAttributeByID(AvsIDCache.Attr_File);
      if (attributeById != null)
      {
        for (int index = 0; index < attributeById.ValuesCount; ++index)
        {
          attributeById.Index = index;
          string extensionWithoutDot = ImDocumentData.GetFileExtensionWithoutDot(attributeById.Description);
          if (!string.IsNullOrEmpty(extensionWithoutDot) && ImDocumentData.IsOldAVSExtension(extensionWithoutDot))
          {
            flag = true;
            using (ImChunkedStream imChunkedStream = new ImChunkedStream())
            {
              BlobProcReader blobProcReader = new BlobProcReader(attributeById, 0, (Stream) imChunkedStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
              blobProcReader.ReadData(sessionKeeper.Session);
              if (blobProcReader.Result)
              {
                if (imChunkedStream.Length > 0L)
                {
                  imChunkedStream.Seek(0L, SeekOrigin.Begin);
                  AVS6_File oldSpFileData = new AVS6_File();
                  if (oldSpFileData.Read((Stream) imChunkedStream, true))
                  {
                    this.AvsDocumentForm = oldSpFileData.GroupForm;
                    this.SortProductsByDocOrder(this.FindProductsFromOldSPFile(oldSpFileData));
                    break;
                  }
                  break;
                }
                break;
              }
              break;
            }
          }
        }
      }
    }
    return flag;
  }

  private List<ProductInfo> FindProductsFromOldSPFile(AVS6_File oldSpFileData)
  {
    List<ProductInfo> productsFromOldSpFile = new List<ProductInfo>();
    if (oldSpFileData._pasport._listR2.IsEmpty<RecordNew>())
      return productsFromOldSpFile;
    foreach (RecordNew recordNew in oldSpFileData._pasport._listR2)
    {
      string productDesignation = recordNew.FieldByType((byte) 7)?._fieldText_Avs6;
      if (!string.IsNullOrEmpty(productDesignation))
      {
        ProductInfo productInfo = this.productsInfo.Find((Predicate<ProductInfo>) (p => p.Designation == productDesignation));
        if (productInfo != null)
          productsFromOldSpFile.Add(productInfo);
      }
    }
    return productsFromOldSpFile;
  }

  public (List<ProductInfo>, List<ProductInfo>) FindProductsFromOldSPFile2(OldAVSFile oldSpFileData)
  {
    List<ProductInfo> productInfoList1 = new List<ProductInfo>();
    List<ProductInfo> productInfoList2 = new List<ProductInfo>();
    if (oldSpFileData._pasport._listR2.IsEmpty<RecordNew>())
      return (productInfoList2, productInfoList1);
    foreach (RecordNew recordNew in oldSpFileData._pasport._listR2)
    {
      string productDesignation = recordNew.FieldByType((byte) 7)?._fieldText_Avs6;
      if (!string.IsNullOrEmpty(productDesignation))
      {
        ProductInfo productInfo = this.productsInfo.Find((Predicate<ProductInfo>) (p => p.Designation == productDesignation));
        productInfoList2.Add(productInfo);
        if (productInfo != null)
          productInfoList1.Add(productInfo);
      }
    }
    return (productInfoList2, productInfoList1);
  }

  /// <summary> Проверить, является ли прикреплённый к спецификации файл старой, не конвертированной спецификацией </summary>
  /// <param name="objIdList">Список идентификаторов найденных объектов в записях</param>
  /// <param name="objTypeList">Список типов найденных объектов в записях</param>
  internal bool CheckOldDocFormat(List<long> objIdList, List<int> objTypeList)
  {
    if (this.DocumentID.IsUndefinedId())
      return false;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.DocumentID).GetAttributeByID(AvsIDCache.Attr_File);
      if (attributeById != null)
      {
        for (int index = 0; index < attributeById.ValuesCount; ++index)
        {
          attributeById.Index = index;
          string extensionWithoutDot = ImDocumentData.GetFileExtensionWithoutDot(attributeById.Description);
          if (!string.IsNullOrEmpty(extensionWithoutDot) && ImDocumentData.IsOldAVSExtension(extensionWithoutDot))
          {
            flag = true;
            OldAVSFields forSpecifications = OldAVSFields.GetColumnsForSpecifications(extensionWithoutDot);
            using (ImChunkedStream aDestStream = new ImChunkedStream())
            {
              BlobProcReader blobProcReader = new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
              blobProcReader.ReadData(sessionKeeper.Session);
              if (blobProcReader.Result)
              {
                if (aDestStream.Length > 0L)
                {
                  aDestStream.Seek(0L, SeekOrigin.Begin);
                  this.DecodeOldFormat((Stream) aDestStream, forSpecifications, objIdList, objTypeList);
                  break;
                }
                break;
              }
              break;
            }
          }
        }
      }
    }
    return flag;
  }

  /// <summary>Извлечь информацию из файла AVS старого формата</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="oldAVSFields">Список полей</param>
  /// <param name="objIdList">Список идентификаторов найденных объектов в записях</param>
  /// <param name="objTypeList">Список типов найденных объектов в записях</param>
  internal void DecodeOldFormat2(
    Stream stream,
    OldAVSFields oldAVSFields,
    List<long> objIdList,
    List<int> objTypeList)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    if (objIdList == null)
      throw new ArgumentNullException(nameof (objIdList));
    if (objTypeList == null)
      throw new ArgumentNullException(nameof (objTypeList));
    OldAVSFile oldAvsFile = new OldAVSFile(this)
    {
      FieldDefs = oldAVSFields
    };
    if (!oldAvsFile.Read(stream))
      return;
    oldAvsFile.ApplyToDocument(objIdList, objTypeList);
  }

  /// <summary>Извлечь информацию из файла AVS старого формата</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="oldAVSFields">Список полей</param>
  /// <param name="objIdList">Список идентификаторов найденных объектов в записях</param>
  /// <param name="objTypeList">Список типов найденных объектов в записях</param>
  internal void DecodeOldFormat(
    Stream stream,
    OldAVSFields oldAVSFields,
    List<long> objIdList,
    List<int> objTypeList)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    if (objIdList == null)
      throw new ArgumentNullException(nameof (objIdList));
    if (objTypeList == null)
      throw new ArgumentNullException(nameof (objTypeList));
    SpecificationSection specificationSection = (SpecificationSection) null;
    if (!this.IsSpecification)
      specificationSection = this.commonDataChapter as SpecificationSection;
    using (BinaryReader br = new BinaryReader(stream))
    {
      if (!new string(br.ReadChars(4)).Equals("iSP2"))
        return;
      br.ReadBytes(3);
      int num1 = (int) br.ReadInt16();
      br.ReadBytes(55);
      if (br.ReadChar() != '#')
        return;
      char ch1 = Convert.ToChar(br.ReadByte());
      int num2 = (int) br.ReadByte();
      int num3 = (int) br.ReadInt16();
      int num4 = (int) br.ReadInt16();
      int[] intArray1 = this.ConvertBytesArrayToIntArray(br.ReadBytes(num2 * 2));
      byte[] fieldsTypes1 = br.ReadBytes(num2);
      List<int> intList1 = new List<int>(num2);
      Dictionary<int, object> dictionary1 = new Dictionary<int, object>(num2);
      Dictionary<int, object> dictionary2 = new Dictionary<int, object>(num2);
      StringBuilder stringBuilder = new StringBuilder();
      long position1 = stream.Position;
      this.GetOldAvsFieldValueStr((byte) 166, ref intArray1, ref fieldsTypes1, position1, br).Trim();
      string str1 = this.GetOldAvsFieldValueStr((byte) 132, ref intArray1, ref fieldsTypes1, position1, br).Trim();
      bool flag = true;
      if (str1 == "")
        flag = false;
      else if (!File.Exists(str1))
      {
        if (this.ReadOnly && this.IsGeneratedDoc)
        {
          flag = false;
        }
        else
        {
          DialogResult dialogResult = DialogResult.Yes;
          if (AvsConfig.General.AskUserForOldSPIniFile)
            dialogResult = MessageBox.Show($"Не найден файл настроек `{str1}` заданный в импортированном файле спецификации старого формата (SP), использовать настройки по умолчанию?", "Файл настроек не найден", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
          if (dialogResult == DialogResult.Yes)
          {
            flag = false;
          }
          else
          {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Ini файлы (*.ini)|*.ini";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
              str1 = openFileDialog.FileName;
            else
              flag = false;
          }
        }
      }
      if (flag)
      {
        OldAVSFields oldAvsFields = OldAVSFields.Load(str1);
        oldAvsFields.DefaultFields = oldAVSFields;
        oldAVSFields = oldAvsFields;
      }
      string str2 = (string) null;
      foreach (byte oldAVSFieldNum in fieldsTypes1)
      {
        string str3 = this.GetOldAvsFieldValueStr(oldAVSFieldNum, ref intArray1, ref fieldsTypes1, position1, br).Trim();
        if (oldAVSFieldNum == (byte) 6 && string.IsNullOrEmpty(str2))
          str2 = str3;
        else if (oldAVSFieldNum == (byte) 204)
          str2 = str3;
      }
      if (!string.IsNullOrEmpty(str2))
      {
        AVSDocumentForm? nullable = new AVSDocumentForm?();
        switch (str2)
        {
          case "0":
            nullable = new AVSDocumentForm?(AVSDocumentForm.Single);
            break;
          case "1":
            nullable = new AVSDocumentForm?(AVSDocumentForm.A);
            break;
          case "2":
            nullable = new AVSDocumentForm?(AVSDocumentForm.B);
            break;
          case "3":
            nullable = new AVSDocumentForm?(AVSDocumentForm.Mirror);
            break;
          case "4":
            nullable = new AVSDocumentForm?(AVSDocumentForm.V);
            break;
        }
        if (nullable.HasValue)
          this.ChangeGroupDocumentForm(nullable.Value);
      }
      stream.Seek(position1 + (long) num3, SeekOrigin.Begin);
      List<ProductInfo> productsInDoc = new List<ProductInfo>();
      List<ProductInfo> originalProductsOrder = new List<ProductInfo>();
      for (int index = 0; index < num4; ++index)
      {
        if (br.ReadChar() != '#')
          return;
        ch1 = Convert.ToChar(br.ReadByte());
        int count = (int) br.ReadByte();
        int num5 = (int) br.ReadInt16();
        int num6 = (int) br.ReadInt16();
        intArray1 = this.ConvertBytesArrayToIntArray(br.ReadBytes(count * 2));
        byte[] fieldsTypes2 = br.ReadBytes(count);
        long position2 = stream.Position;
        string productDesignation = "";
        foreach (byte oldAVSFieldNum in fieldsTypes2)
        {
          string str4 = this.GetOldAvsFieldValueStr(oldAVSFieldNum, ref intArray1, ref fieldsTypes2, position2, br).Trim();
          if (oldAVSFieldNum == (byte) 7)
            productDesignation = str4;
        }
        ProductInfo productInfo = this.productsInfo.Find((Predicate<ProductInfo>) (p => p.Designation == productDesignation));
        originalProductsOrder.Add(productInfo);
        if (productInfo != null)
          productsInDoc.Add(productInfo);
        stream.Seek(position2 + (long) num5, SeekOrigin.Begin);
      }
      this.SortProductsByDocOrder(productsInDoc);
      SkipLinesSchema skipLinesSchema = this.GetSkipLinesSchema();
      if (oldAVSFields != null)
        skipLinesSchema.CopyParamsFrom(oldAVSFields.SkipLinesSchema);
      if (!this.ReadOnly)
        skipLinesSchema.SaveParams();
      long num7 = -1;
      long activePartID = -1;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      int num8 = -1;
      int num9 = -1;
      int num10 = 0;
      OldAVSField oldAvsField = (OldAVSField) null;
      int index1 = 0;
      int index2 = 0;
      string searchArtId = "";
      string searchDocId = "";
      string position3 = string.Empty;
      string str5 = string.Empty;
      string designation = string.Empty;
      string name = string.Empty;
      string okpCode = string.Empty;
      string empty3 = string.Empty;
      string str6 = string.Empty;
      List<int> intList2 = new List<int>();
      List<byte> byteList = new List<byte>();
      List<string> stringList = new List<string>();
      if (num1 <= 0)
        return;
      Dictionary<int, long> sectionIdDictionary;
      Dictionary<int, long> partIdDictionary;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sectionIdDictionary = AVSPlugin.GetSectionNumToSectionIdDictionary(sessionKeeper.Session);
        partIdDictionary = AVSPlugin.GetPartNumToPartIdDictionary(sessionKeeper.Session);
      }
      articleService = (IArticleService) null;
      if (!this.IsSpecification && !(ServicesManager.GetService(typeof (IArticleService)) is IArticleService articleService))
        throw new Exception("Недоступен сервис ArticleService");
      while (num10 < num1)
      {
        try
        {
          long num11;
          if (num9 == -1)
          {
            searchArtId = "";
            searchDocId = "";
            position3 = string.Empty;
            str5 = string.Empty;
            designation = string.Empty;
            name = string.Empty;
            okpCode = string.Empty;
            str6 = string.Empty;
            empty3 = string.Empty;
            num11 = -1L;
            stringList.Clear();
          }
          int num12 = 0;
          char ch2;
          do
          {
            ch2 = br.ReadChar();
            ++num12;
          }
          while (!ch2.Equals('#'));
          char ch3 = Convert.ToChar(br.ReadByte());
          if (num9 == -1)
            ch1 = ch3;
          int count = (int) br.ReadByte();
          int num13 = (int) br.ReadInt16();
          if (num9 == -1)
          {
            num4 = (int) br.ReadInt16();
            if (num4 > 0)
              num8 = 0;
          }
          else
          {
            if (num9 > 0)
              ++num8;
            int num14 = (int) br.ReadInt16();
          }
          int[] intArray2 = this.ConvertBytesArrayToIntArray(br.ReadBytes(count * 2));
          byte[] fieldsTypes3 = br.ReadBytes(count);
          long position4 = stream.Position;
          string str7 = (string) null;
          try
          {
            if (num9 != -1)
            {
              if (ch1 != 'I')
                continue;
            }
            if (ch1 != 'B')
            {
              if (ch1 != 'C')
              {
                TableData rowTemplate;
                switch ((int) ch1 - 73)
                {
                  case 0:
                    int num15;
                    if (this.IsSpecification)
                    {
                      string str8;
                      if (num9 == -1)
                      {
                        intList2.Clear();
                        byteList.Clear();
                        int num16 = 0;
                        if (oldAVSFields != null)
                        {
                          foreach (byte num17 in fieldsTypes3)
                          {
                            if (num17 > (byte) 50 && num17 < (byte) 100)
                            {
                              byteList.Add(num17);
                              intList2.Add(num16);
                            }
                            ++num16;
                          }
                        }
                        searchArtId = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_ArtId, ref intArray2, ref fieldsTypes3, position4, br);
                        searchDocId = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_DocId, ref intArray2, ref fieldsTypes3, position4, br);
                        position3 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Position, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        str5 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_PosDesignation, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        designation = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Designation, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        name = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Name, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        okpCode = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_OkpCode, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        str8 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Count, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        str6 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Note, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                      }
                      else
                      {
                        str8 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Count, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                        stringList.Add(str8);
                      }
                      if (num9 == num4 - 1)
                      {
                        AVSRow avsRow = (AVSRow) null;
                        if (name != "" || designation != "" || position3 != "")
                          avsRow = this.GetRowByParams(name, designation, okpCode, position3, str8, stringList, originalProductsOrder, num8, activePartID, num7, searchArtId, searchDocId);
                        if (avsRow != null)
                        {
                          this.ImportCountSpecialSymbolFromSP(avsRow, str8, stringList, originalProductsOrder, num8);
                          if (avsRow.Section != null && avsRow.Index != index1)
                          {
                            LogManager.AddLine($"AVS.SP. Смена индекса {avsRow.Index} на {index1} для записи {avsRow}");
                            avsRow.Section.Rows.RemoveAt(avsRow.Index);
                            if (index1 > avsRow.Section.Rows.Count)
                              index1 = avsRow.Section.Rows.Count;
                            avsRow.Section.Rows.Insert(index1, avsRow);
                          }
                          if (activePartID != -1L)
                          {
                            AdditionalChapterSettings newChapterSettings = this.AVSCommonPropertiesSchema.AdditionalChapters.Find((Predicate<AdditionalChapterSettings>) (x => x.ChapterID == activePartID));
                            if (newChapterSettings != null)
                              this.MoveSpecRowToChapter(new List<AVSRow>()
                              {
                                avsRow
                              }, newChapterSettings);
                          }
                          ++index1;
                          if (!avsRow.IsDocRelation && string.IsNullOrEmpty(avsRow.GetFieldStringValue(avsRow.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false)))
                          {
                            string str9 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Format, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                            if (!string.IsNullOrEmpty(str9))
                              avsRow.SetFieldValue(avsRow.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, (object) str9, false, false, true, false, false, false);
                          }
                          if (byteList.Count > 0 && oldAVSFields != null)
                          {
                            for (int index3 = 0; index3 < byteList.Count; ++index3)
                            {
                              byte num18 = byteList[index3];
                              num15 = intList2[index3];
                              string avsFieldValueStr = this.GetOldAvsFieldValueStr(num18, ref intArray2, ref fieldsTypes3, position4, br);
                              if (avsFieldValueStr != string.Empty && oldAVSFields.List.TryGetValue((int) num18, out oldAvsField))
                              {
                                ConvertField convertField = oldAvsField.ConvertField;
                                ConvertFullData fullDataForRecord = convertField.GetConvertFullDataForRecord(avsRow.RelType, avsRow.ObjType);
                                switch (fullDataForRecord.Action)
                                {
                                  case ConvertAction.Write:
                                    switch (fullDataForRecord.Target)
                                    {
                                      case ConvertTarget.ToDocumentField:
                                        avsRow.DocNode.SetAttributeValue(convertField.OldCaption, avsFieldValueStr);
                                        continue;
                                      case ConvertTarget.ToObjectAttribute:
                                        avsRow.SetFieldValue(new AvsRowAttributeInfo(false, convertField.NewAttributeID), -1, num8, (object) avsFieldValueStr, true, false, true, true, false, false);
                                        continue;
                                      case ConvertTarget.ToRelationAttribute:
                                        avsRow.SetFieldValue(new AvsRowAttributeInfo(true, convertField.NewAttributeID), -1, num8, (object) avsFieldValueStr, true, false, true, true, false, false);
                                        continue;
                                      default:
                                        continue;
                                    }
                                  default:
                                    continue;
                                }
                              }
                            }
                          }
                          int result = 0;
                          if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 13, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result))
                            avsRow.SkipLinesBefore = new int?(result);
                          if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 14, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result))
                            avsRow.SkipLinesAfter = new int?(result);
                          if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 17, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result))
                            avsRow.PositionStepBefore = new int?(result);
                          if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 18, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result))
                            avsRow.PositionStepAfter = new int?(result);
                          if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 16 /*0x10*/, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result))
                          {
                            if (result == 0)
                            {
                              avsRow.FromNewPage = new bool?(true);
                              continue;
                            }
                            avsRow.SkipPagesAfter = result;
                            continue;
                          }
                          continue;
                        }
                        continue;
                      }
                      continue;
                    }
                    intList2.Clear();
                    byteList.Clear();
                    int num19 = 0;
                    if (oldAVSFields != null)
                    {
                      foreach (byte num20 in fieldsTypes3)
                      {
                        if (num20 > (byte) 50 && num20 < (byte) 100)
                        {
                          byteList.Add(num20);
                          intList2.Add(num19);
                        }
                        ++num19;
                      }
                    }
                    position3 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Position, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    designation = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Designation, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    name = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Name, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    okpCode = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_OkpCode, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    string str10 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Count, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    string str11 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_PosDesignation, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    string str12 = this.GetOldAvsFieldValueStr(AvsIDCache.OldAVDFieldNum_Note, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    int objType = -1;
                    using (SessionKeeper sessionKeeper = new SessionKeeper())
                    {
                      num11 = articleService.FindArticleID(designation, okpCode, name, this.FiltrationOwnerID, (object) sessionKeeper.Session);
                      if (num11 == 0L)
                        num11 = -1L;
                      if (!num11.IsUndefinedId())
                      {
                        sessionKeeper.Session.GetObjectInfo(num11);
                        objType = sessionKeeper.Session.GetObjectInfo(num11).ObjectTypeID;
                        objIdList.Add(num11);
                        objTypeList.Add(objType);
                      }
                    }
                    foreach (byte oldAVSFieldNum in fieldsTypes3)
                      this.GetOldAvsFieldValueStr(oldAVSFieldNum, ref intArray2, ref fieldsTypes3, position4, br).Trim();
                    AVSRow row = new AVSRow(this, num11, Guid.Empty, objType, -1L, Guid.Empty, -1, Guid.Empty, -1L);
                    specificationSection.AddRow(row, false);
                    row.UpdateDocRow((TableData) null, (List<AvsRowAttributeInfo>) null, true, false, false, EmptyRowUpdateMode.DontChange);
                    row.SetFieldValue(this.Field_PosDesignation, -1, -1, (object) str11, false, false, true, false, false, false);
                    if (!string.IsNullOrWhiteSpace(designation))
                      row.SetFieldValue(this.Field_Designation, -1, -1, (object) designation, false, false, true, false, false, false);
                    row.SetFieldValue(this.Field_Name, -1, -1, (object) name, false, false, true, false, false, false);
                    row.SetFieldValue(this.Field_Count, -1, -1, (object) str10, false, false, true, false, false, false);
                    row.SetFieldValue(row.Field_Note, -1, -1, (object) str12, false, false, true, false, false, false);
                    if (row.Section != null)
                    {
                      if (index2 >= row.Section.Rows.Count || row.Section.Rows[index2] != row)
                      {
                        index2 = row.Section.Rows.IndexOf(row);
                        if (index2 != -1 && index2 < row.Section.Rows.Count)
                        {
                          row.Section.Rows.RemoveAt(index2);
                          row.Section.Rows.Insert(index1, row);
                          index2 = index1 + 1;
                        }
                      }
                      else
                        ++index2;
                    }
                    ++index1;
                    if (byteList.Count > 0 && oldAVSFields != null)
                    {
                      for (int index4 = 0; index4 < byteList.Count; ++index4)
                      {
                        byte num21 = byteList[index4];
                        num15 = intList2[index4];
                        string avsFieldValueStr = this.GetOldAvsFieldValueStr(num21, ref intArray2, ref fieldsTypes3, position4, br);
                        if (avsFieldValueStr != string.Empty && oldAVSFields.List.TryGetValue((int) num21, out oldAvsField))
                        {
                          ConvertField convertField = oldAvsField.ConvertField;
                          ConvertFullData fullDataForRecord = convertField.GetConvertFullDataForRecord(row.RelType, row.ObjType);
                          switch (fullDataForRecord.Action)
                          {
                            case ConvertAction.Write:
                              switch (fullDataForRecord.Target)
                              {
                                case ConvertTarget.ToDocumentField:
                                  row.DocNode.SetAttributeValue(convertField.OldCaption, avsFieldValueStr);
                                  continue;
                                case ConvertTarget.ToObjectAttribute:
                                  row.SetFieldValue(new AvsRowAttributeInfo(false, convertField.NewAttributeID), -1, num8, (object) avsFieldValueStr, true, false, true, true, false, false);
                                  continue;
                                case ConvertTarget.ToRelationAttribute:
                                  row.SetFieldValue(new AvsRowAttributeInfo(true, convertField.NewAttributeID), -1, num8, (object) avsFieldValueStr, true, false, true, true, false, false);
                                  continue;
                                default:
                                  continue;
                              }
                            default:
                              continue;
                          }
                        }
                      }
                    }
                    int result1 = 0;
                    if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 13, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result1))
                      row.SkipLinesBefore = new int?(result1);
                    if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 14, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result1))
                      row.SkipLinesAfter = new int?(result1);
                    if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 17, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result1))
                      row.PositionStepBefore = new int?(result1);
                    if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 18, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result1))
                      row.PositionStepAfter = new int?(result1);
                    if (int.TryParse(this.GetOldAvsFieldValueStr((byte) 16 /*0x10*/, ref intArray2, ref fieldsTypes3, position4, br).Trim(), out result1))
                    {
                      if (result1 == 0)
                      {
                        row.FromNewPage = new bool?(true);
                        continue;
                      }
                      row.SkipPagesAfter = result1;
                      continue;
                    }
                    continue;
                  case 5:
                    index1 = 0;
                    index2 = 0;
                    int oldAvsFieldIndex1 = this.GetOldAvsFieldIndex((byte) 5, fieldsTypes3);
                    if (oldAvsFieldIndex1 != -1)
                    {
                      long num22 = (long) intArray2[oldAvsFieldIndex1];
                      num8 = this.GetProductIndexByHisCaption(this.GetOldAvsFieldValueStr(br, position4 + num22, oldAvsFieldIndex1 < intArray2.Length - 1 ? (int) ((long) intArray2[oldAvsFieldIndex1 + 1] - num22) : -1));
                      continue;
                    }
                    continue;
                  case 7:
                    index1 = 0;
                    index2 = 0;
                    int oldAvsFieldIndex2 = this.GetOldAvsFieldIndex((byte) 9, fieldsTypes3);
                    if (oldAvsFieldIndex2 == -1)
                    {
                      stream.Seek(position4 + (long) num13, SeekOrigin.Begin);
                      continue;
                    }
                    long num23 = (long) intArray2[oldAvsFieldIndex2];
                    int result2;
                    if (int.TryParse(this.GetOldAvsFieldValueStr(br, position4 + num23, oldAvsFieldIndex2 < intArray2.Length - 1 ? (int) ((long) intArray2[oldAvsFieldIndex2 + 1] - num23) : -1), out result2))
                    {
                      if (!partIdDictionary.TryGetValue(result2, out activePartID))
                      {
                        activePartID = -1L;
                        continue;
                      }
                      continue;
                    }
                    activePartID = -1L;
                    continue;
                  case 9:
                    rowTemplate = this.note1Template;
                    break;
                  case 10:
                    index1 = 0;
                    index2 = 0;
                    int oldAvsFieldIndex3 = this.GetOldAvsFieldIndex((byte) 10, fieldsTypes3);
                    if (oldAvsFieldIndex3 == -1)
                    {
                      stream.Seek(position4 + (long) num13, SeekOrigin.Begin);
                      continue;
                    }
                    long num24 = (long) intArray2[oldAvsFieldIndex3];
                    int result3;
                    if (int.TryParse(this.GetOldAvsFieldValueStr(br, position4 + num24, oldAvsFieldIndex3 < intArray2.Length - 1 ? (int) ((long) intArray2[oldAvsFieldIndex3 + 1] - num24) : -1), out result3))
                    {
                      if (!sectionIdDictionary.TryGetValue(result3, out num7))
                      {
                        num7 = -1L;
                        continue;
                      }
                      continue;
                    }
                    num7 = -1L;
                    continue;
                  case 11:
                    rowTemplate = this.note2Template;
                    break;
                  case 15:
                    rowTemplate = this.additionalNote1Template;
                    break;
                  case 16 /*0x10*/:
                    rowTemplate = this.additionalNote2Template;
                    break;
                  default:
                    continue;
                }
                if (rowTemplate != null)
                {
                  int oldAvsFieldIndex4 = this.GetOldAvsFieldIndex((byte) 11, fieldsTypes3);
                  if (oldAvsFieldIndex4 != -1)
                  {
                    long num25 = (long) intArray2[oldAvsFieldIndex4];
                    str7 = this.GetOldAvsFieldValueStr(br, position4 + num25, oldAvsFieldIndex4 < intArray2.Length - 1 ? (int) ((long) intArray2[oldAvsFieldIndex4 + 1] - num25) : -1);
                    num8 = this.GetProductIndexByHisCaption(str7);
                  }
                  else
                    num8 = -1;
                  DocumentTreeNode[] contextNodes = this.GetContextNodes(activePartID, num7, num8);
                  this.InsertNewNoteDocRow(this.GetContextChapters(contextNodes.Length != 0 ? contextNodes[0] : (DocumentTreeNode) null), str7, rowTemplate, false, false);
                }
              }
            }
          }
          finally
          {
            stream.Seek(position4 + (long) num13, SeekOrigin.Begin);
          }
        }
        finally
        {
          if (num4 > 0)
          {
            ++num9;
            if (num9 == num4)
            {
              num4 = 0;
              num9 = -1;
            }
          }
          if (num9 == -1)
            ++num10;
        }
      }
      this.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
    }
  }

  private long FindObjectWithDesignationOrName(
    IUserSession session,
    int objectType,
    string designation,
    string name)
  {
    int attributeID = AvsIDCache.Attr_Designation;
    string conditionValue = designation;
    if (string.IsNullOrEmpty(designation))
    {
      if (string.IsNullOrEmpty(name))
        return -1;
      attributeID = AvsIDCache.Attr_Name;
      conditionValue = name;
    }
    DataTable dataTable = session.GetObjectCollection(objectType).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
    }, recordCount: 1));
    if (dataTable == null)
      return -1;
    try
    {
      if (dataTable.Rows.Count > 0)
        return Convert.ToInt64(dataTable.Rows[0][0]);
    }
    finally
    {
      dataTable.Dispose();
    }
    return -1;
  }

  private int GetDefaultObjectTypeForRowFromSP(string designation, string name, long sectionID)
  {
    int defaultPartType = SpecificationSectionInfo.GetDefaultPartType(sectionID);
    if (!MetaDataHelper.IsObjectTypeChildOf(defaultPartType, AvsIDCache.ObjType_Document))
      return defaultPartType;
    if (string.IsNullOrEmpty(name))
      return -1;
    if (name.IndexOf("Монтажный черт", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad0074e-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Сборочный черт", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad00901-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Ведомость спецификаций", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad0082b-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Ведомость покупных изделий", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad00826-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Руководство по эксплуатации", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad00784-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Технические условия", StringComparison.CurrentCultureIgnoreCase) != -1)
      return MetaDataHelper.GetObjectTypeID("cad0075f-306c-11d8-b4e9-00304f19f545");
    if (name.IndexOf("Лист утверждения", StringComparison.CurrentCultureIgnoreCase) != -1 && designation.IndexOf("ТУ") != -1 && designation.IndexOf("ЛУ") != -1)
      return MetaDataHelper.GetObjectTypeID("95900782-5179-480e-8ae8-8a28b50a4bff");
    return name.IndexOf("Программа и методика испытаний", StringComparison.CurrentCultureIgnoreCase) != -1 ? MetaDataHelper.GetObjectTypeID("cad00756-306c-11d8-b4e9-00304f19f545") : -1;
  }

  private long CreateDBObjectForPart(
    IUserSession session,
    int objectType,
    string designation,
    string name,
    string okp)
  {
    long designationOrName = this.FindObjectWithDesignationOrName(session, objectType, designation, name);
    if (designationOrName.IsDefinedId())
      return designationOrName;
    IDBObject dbObject = session.GetObjectCollection(objectType).Create();
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(3)
    {
      new AttributeValues(AvsIDCache.Attr_Designation, (object) designation),
      new AttributeValues(AvsIDCache.Attr_Name, (object) name)
    };
    if (!string.IsNullOrEmpty(okp))
      attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_OKPCode, (object) okp));
    dbObject.SetAttributesValues(attributeValuesList.ToArray());
    dbObject.CommitCreation(true, true);
    return dbObject.ObjectID;
  }

  private AVSRow CreateRelationsByCountFromSP(
    IUserSession session,
    long partID,
    int partType,
    string position,
    string countFromSP,
    List<string> countsBFromSP,
    List<ProductInfo> originalProductsOrder,
    int indexOfProduct)
  {
    bool flag1 = MetaDataHelper.IsObjectTypeChildOf(partType, AvsIDCache.ObjType_Document);
    bool flag2 = false;
    if (countsBFromSP.IsNullOrEmpty<string>())
      countsBFromSP = new List<string>() { countFromSP };
    else
      flag2 = true;
    AVSDocumentContext context = new AVSDocumentContext();
    int relationType = flag1 ? AvsIDCache.Relation_Document : AvsIDCache.Relation_Project;
    AVSRow relationsByCountFromSp = this.AddAvsRowParts(new object[1]
    {
      (object) partID
    }, relationType, context, false, false).FirstOrDefault<AVSRow>();
    if (relationsByCountFromSp != null)
    {
      for (int index = 0; index < countsBFromSP.Count; ++index)
      {
        if (!string.IsNullOrEmpty(countsBFromSP[index]))
        {
          int productIndex = -1;
          if (flag2)
          {
            productIndex = this.FindProductIndexByProductFromSP(originalProductsOrder, index);
            if (productIndex == -1)
              continue;
          }
          object obj = !flag1 ? (object) AVSRow.ConvertCountToMeasuredValue((object) countsBFromSP[index]) : (object) "X";
          relationsByCountFromSp.SetCount(productIndex, obj, false);
        }
      }
      if (!string.IsNullOrEmpty(position))
        relationsByCountFromSp.SetFieldValue(this.Field_Position, -1, -1, (List<RelationAttributeValuesCache>) null, (object) position, true, false, false, false, false, false, false, true);
    }
    return relationsByCountFromSp;
  }

  public void ImportCountSpecialSymbolFromSP(
    AVSRow avsRow,
    string countFromSP,
    List<string> countsBFromSP,
    List<ProductInfo> originalProductsOrder,
    int indexOfProduct)
  {
    if (avsRow.IsDocRelation)
      return;
    bool flag = false;
    if (countsBFromSP.IsNullOrEmpty<string>())
      countsBFromSP = new List<string>() { countFromSP };
    else
      flag = true;
    for (int index = 0; index < countsBFromSP.Count; ++index)
    {
      if (!string.IsNullOrEmpty(countsBFromSP[index]) && (countsBFromSP[index].Contains<char>('?') || countsBFromSP[index].Contains<char>('/')))
      {
        int originalProductIndex = flag ? index : indexOfProduct;
        int indexByProductFromSp = this.FindProductIndexByProductFromSP(originalProductsOrder, originalProductIndex);
        if (indexByProductFromSp != -1 && avsRow.GetFieldValueForDocCell(this.Field_Count, -1, indexByProductFromSp, true, false) != countsBFromSP[index])
        {
          MeasuredValue measuredValue = AVSRow.ConvertCountToMeasuredValue((object) countsBFromSP[index]);
          int relationIndexForProduct = avsRow.GetRelationIndexForProduct(this.productsInfo[indexByProductFromSp].Id);
          MeasuredValue count = avsRow.GetCount(relationIndexForProduct, indexByProductFromSp, (List<RelationAttributeValuesCache>) null);
          if (measuredValue != null && count != null && MeasureHelper.Compare(count, measuredValue) == CompareResult.Equal)
            avsRow.SetFieldValue(avsRow.Field_Count, relationIndexForProduct, indexByProductFromSp, (List<RelationAttributeValuesCache>) null, (object) measuredValue, false, false, true, false, false, false, false);
        }
      }
    }
  }

  public void MoveSpecRowToChapter(
    List<AVSRow> specRows,
    AdditionalChapterSettings newChapterSettings)
  {
    try
    {
      for (int index1 = 0; index1 < specRows.Count; ++index1)
      {
        Chapter rootChapter = specRows[index1].GetRootChapter();
        ProductInfo product = specRows[index1].Product;
        Chapter chapter1 = (Chapter) null;
        newSection = (SpecificationSection) null;
        if (newChapterSettings.ChapterGuid == AVSDocument.ChapterCommonDataGuid)
        {
          if (rootChapter.IsAdditionalChapter)
          {
            if (product.IsCommonData || product.Guid == rootChapter.ChapterGuid)
              chapter1 = this.CommonDataChapter;
            else if (product.IsVariableData && this.AvsDocumentForm == AVSDocumentForm.V && this.VariableDataChapter_FormV != null)
              chapter1 = (Chapter) this.VariableDataChapter_FormV;
            else if (this.AvsDocumentForm == AVSDocumentForm.A && this.VariableDataChapter_FormA != null)
              chapter1 = this.VariableDataChapter_FormA.GetProductChapter(product);
            if (!this.ReadOnly)
              specRows[index1].SetFieldValueForAllRelations(this.Attr_AdditionalChapter, (object) null, true, true, false, this.IsGridViewMode, false, false);
          }
          else
            continue;
        }
        else if (!rootChapter.IsAdditionalChapter || !(rootChapter.ChapterGuid == newChapterSettings.ChapterGuid))
        {
          if (!this.ReadOnly)
            specRows[index1].SetFieldValueForAllRelations(this.Attr_AdditionalChapter, (object) newChapterSettings.ChapterID, true, true, false, this.IsGridViewMode, false, false);
          if (!this.AdditionalChaptersInDataChapter)
          {
            Chapter chapter2 = (Chapter) null;
            for (int index2 = 0; index2 < this.rootChapters.Count; ++index2)
            {
              if (this.rootChapters[index2].IsAdditionalChapter && this.rootChapters[index2].ChapterGuid == newChapterSettings.ChapterGuid)
                chapter2 = this.rootChapters[index2];
            }
            if (chapter2 == null)
              this.AddRootChapter(chapter2 = (Chapter) new AdditionalChapter(this, newChapterSettings, this.AdditionalChaptersInDataChapter), true);
            if (product.IsCommonData)
            {
              for (int index3 = 0; index3 < chapter2.Chapters.Count; ++index3)
              {
                if (chapter2.Chapters[index3].IsCommonDataChapter)
                {
                  chapter1 = chapter2.Chapters[index3];
                  break;
                }
              }
            }
            else if (product.IsVariableData && this.AvsDocumentForm == AVSDocumentForm.V)
            {
              for (int index4 = 0; index4 < chapter2.Chapters.Count; ++index4)
              {
                if (chapter2.Chapters[index4] is VariableDataChapterFormV)
                {
                  chapter1 = chapter2.Chapters[index4];
                  break;
                }
              }
            }
            else if (this.AvsDocumentForm == AVSDocumentForm.A)
            {
              VariableDataChapterFormA dataChapterFormA = (VariableDataChapterFormA) null;
              for (int index5 = 0; index5 < chapter2.Chapters.Count; ++index5)
              {
                if (chapter2.Chapters[index5] is VariableDataChapterFormA)
                {
                  dataChapterFormA = chapter2.Chapters[index5] as VariableDataChapterFormA;
                  break;
                }
              }
              if (dataChapterFormA != null)
                chapter1 = dataChapterFormA.GetProductChapter(product);
            }
          }
          else if (product.IsCommonData)
          {
            chapter1 = this.CommonDataChapter.GetChapter(newChapterSettings.ChapterGuid);
            if (chapter1 == null)
            {
              chapter1 = (Chapter) new AdditionalChapter(this, newChapterSettings, this.AdditionalChaptersInDataChapter);
              this.CommonDataChapter.AddChapter(chapter1, true, true, this.IsGridViewMode, (TableData) null);
            }
          }
          else if (product.IsVariableData && this.AvsDocumentForm == AVSDocumentForm.V && this.VariableDataChapter_FormV != null)
          {
            chapter1 = this.VariableDataChapter_FormV.GetChapter(newChapterSettings.ChapterGuid);
            if (chapter1 == null)
            {
              chapter1 = (Chapter) new AdditionalChapter(this, newChapterSettings, this.AdditionalChaptersInDataChapter);
              this.VariableDataChapter_FormV.AddChapter(chapter1, true, true, this.IsGridViewMode, (TableData) null);
            }
          }
          else if (this.AvsDocumentForm == AVSDocumentForm.A && this.VariableDataChapter_FormA != null)
          {
            Chapter productChapter = this.VariableDataChapter_FormA.GetProductChapter(product);
            if (productChapter != null)
            {
              chapter1 = productChapter.GetChapter(newChapterSettings.ChapterGuid);
              if (chapter1 == null)
              {
                chapter1 = (Chapter) new AdditionalChapter(this, newChapterSettings, this.AdditionalChaptersInDataChapter);
                productChapter.AddChapter(chapter1, true, true, this.IsGridViewMode, (TableData) null);
              }
            }
          }
        }
        else
          continue;
        if (chapter1 != null && !(chapter1.GetChapter(specRows[index1].SectionID) is SpecificationSection newSection))
        {
          newSection = this.CreateSection(specRows[index1].SectionID);
          chapter1.AddChapter((Chapter) newSection, true, true, this.IsGridViewMode, chapter1.GetSectionTemplate());
        }
        if (newSection != null)
          specRows[index1].Section.MoveRow(specRows[index1], newSection, true, this.IsGridViewMode, true);
      }
      this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
      this.UpdateVariableDataCaptions();
    }
    finally
    {
      this.IndexAVSDocument(true);
    }
  }

  /// <summary> Получить запись спецификации по её основным параметрам </summary>
  /// <param name="name"> Наименование </param>
  /// <param name="designation"> Обозначение </param>
  /// <param name="okpCode"> Код ОКП (можно передавать null - тогда он будет игнорироваться) </param>
  /// <param name="position"> Позиция </param>
  /// <param name="count"> Количество </param>
  /// <param name="indexOfProduct"> Номер исполнения </param>
  /// <param name="partID"> Часть спецификации </param>
  /// <param name="sectionID"> Раздел спецификации </param>
  /// <param name="searchArtId"> Идентификатор объекта Search </param>
  /// <param name="searchDocId"> Идентификатор объекта Search </param>
  /// <returns> Найденная запись спецификации </returns>
  internal AVSRow GetRowByParams(
    string name,
    string designation,
    string okpCode,
    string position,
    string count,
    List<string> countsB,
    List<ProductInfo> originalProductsOrder,
    int indexOfProduct,
    long partID,
    long sectionID,
    string searchArtId,
    string searchDocId)
  {
    if (designation == null)
      designation = "";
    if (name == null)
      name = "";
    if (okpCode == null)
      okpCode = "";
    if (position == null)
      position = "";
    if (count == null)
      count = "";
    if (searchArtId == null)
      searchArtId = "";
    if (searchDocId == null)
      searchDocId = "";
    if (name == "" && designation == "" && position == "")
      throw new ArgumentException("Аргументы идентифицирующие запись имеют пустое значение", "name, designation, position");
    if (indexOfProduct < 0)
      indexOfProduct = 0;
    List<AVSRow> allRows = this.GetAllRows(false, true);
    AVSRow rowByParams = (AVSRow) null;
    for (int index = 0; index < allRows.Count; ++index)
    {
      if (sectionID == allRows[index].SectionID)
      {
        bool flag1 = rowByParams == null && allRows[index].IsEqualsSearchId(searchArtId);
        bool flag2 = this.CompareKeyFieldsFromOldSP(allRows[index], designation, name);
        if (flag2 || flag1)
        {
          string str = allRows[index].GetFieldStringValue(this.Field_Position, -1, -1, (List<RelationAttributeValuesCache>) null, false).Trim();
          if (position == str && (okpCode == "" || okpCode == (allRows[index].OKPCode ?? "")) && this.CompareCountFromSP(allRows[index], count, countsB, originalProductsOrder, indexOfProduct))
          {
            if (flag2)
              return allRows[index];
            if (flag1)
              rowByParams = allRows[index];
          }
        }
      }
    }
    if (rowByParams != null)
    {
      if (LogManager.CreateLog)
      {
        LogManager.AddLine($"AVS.SP. Для записи SP не был найден объект по обозначению: Поз: '{position}', Обозн.: '{designation}', Наим.: '{name}', Кол.: [{(countsB.IsNullOrEmpty<string>() ? (object) count : (object) string.Join(",", (IEnumerable<string>) countsB))}], Код ОКП: '{okpCode}', № исполн.: '{indexOfProduct}', ID Части: '{partID}', Раздел: '{SpecificationSectionInfo.FindSectionById(sectionID)}', SearchArtId: '{searchArtId}', SearchDocId: '{searchDocId}'");
        LogManager.AddLine($"AVS.SP. Была выбрана запись по 'Идентификатору объекта в Search': '{rowByParams.GetFieldStringValue(this.Attr_SearchId, -1, -1, (List<RelationAttributeValuesCache>) null, false)}' / {rowByParams}");
      }
      return rowByParams;
    }
    if (LogManager.CreateLog)
      LogManager.AddLine($"AVS.SP. Не найден объект для записи SP: Поз: '{position}', Обозн.: '{designation}', Наим.: '{name}', Кол.: [{(countsB.IsNullOrEmpty<string>() ? (object) count : (object) string.Join(",", (IEnumerable<string>) countsB))}], Код ОКП: '{okpCode}', № исполн.: '{indexOfProduct}', ID Части: '{partID}', Раздел: '{SpecificationSectionInfo.FindSectionById(sectionID)}', SearchArtId: '{searchArtId}', SearchDocId: '{searchDocId}'");
    return (AVSRow) null;
  }

  private bool CompareCountFromSP(
    AVSRow avsRow,
    string countFromSP,
    List<string> countsBFromSP,
    List<ProductInfo> originalProductsOrder,
    int indexOfProduct)
  {
    if (avsRow.IsDocRelation)
      return true;
    bool flag = false;
    if (countsBFromSP.IsNullOrEmpty<string>())
      countsBFromSP = new List<string>() { countFromSP };
    else
      flag = true;
    for (int index = 0; index < countsBFromSP.Count; ++index)
    {
      int originalProductIndex = flag ? index : indexOfProduct;
      int indexByProductFromSp = this.FindProductIndexByProductFromSP(originalProductsOrder, originalProductIndex);
      if (indexByProductFromSp != -1)
      {
        if (!string.IsNullOrEmpty(countsBFromSP[index]) && (countsBFromSP[index].Contains<char>('?') || countsBFromSP[index].Contains<char>('/')))
        {
          MeasuredValue measuredValue = AVSRow.ConvertCountToMeasuredValue((object) countsBFromSP[index]);
          MeasuredValue count = avsRow.GetCount(-1, indexByProductFromSp, (List<RelationAttributeValuesCache>) null);
          if (measuredValue == null || count == null)
            return measuredValue == count;
          if (MeasureHelper.Compare(count, measuredValue) != CompareResult.Equal)
            return false;
        }
        else
        {
          string fieldValueForDocCell = avsRow.GetFieldValueForDocCell(this.Field_Count, -1, indexByProductFromSp, true, false);
          if (countsBFromSP[index] != fieldValueForDocCell)
            return false;
        }
      }
    }
    return true;
  }

  private int FindProductIndexByProductFromSP(
    List<ProductInfo> originalProducts,
    int originalProductIndex)
  {
    if (originalProducts.IsNullOrEmpty<ProductInfo>())
      return 0;
    if (originalProductIndex < 0)
      originalProductIndex = 0;
    int indexByProductFromSp = -1;
    if (originalProductIndex < originalProducts.Count && originalProducts[originalProductIndex] != null)
      indexByProductFromSp = this.ProductsInfo.FindIndex((Predicate<ProductInfo>) (p => p.Id == originalProducts[originalProductIndex].Id));
    return indexByProductFromSp;
  }

  private bool CompareKeyFieldsFromOldSP(AVSRow row, string designation, string name)
  {
    string str = designation != "" ? designation : name;
    return row.DesignationOrName == str;
  }

  private long? GetOldAvsFieldValueLong(
    byte oldAVSFieldNum,
    ref int[] fieldOffsets,
    ref byte[] fieldsTypes,
    long recordDataStartPosition,
    BinaryReader br)
  {
    string avsFieldValueStr = this.GetOldAvsFieldValueStr(oldAVSFieldNum, ref fieldOffsets, ref fieldsTypes, recordDataStartPosition, br);
    if (string.IsNullOrEmpty(avsFieldValueStr))
      return new long?();
    long result;
    return !long.TryParse(avsFieldValueStr, out result) ? new long?() : new long?(result);
  }

  private string GetOldAvsFieldValueStr(
    byte oldAVSFieldNum,
    ref int[] fieldOffsets,
    ref byte[] fieldsTypes,
    long recordDataStartPosition,
    BinaryReader br)
  {
    int oldAvsFieldIndex = this.GetOldAvsFieldIndex(oldAVSFieldNum, fieldsTypes);
    if (oldAvsFieldIndex == -1)
      return string.Empty;
    long num = (long) fieldOffsets[oldAvsFieldIndex];
    return this.GetOldAvsFieldValueStr(br, recordDataStartPosition + num, oldAvsFieldIndex < fieldOffsets.Length - 1 ? (int) ((long) fieldOffsets[oldAvsFieldIndex + 1] - num) : -1);
  }

  private string GetOldAvsFieldValueStr(BinaryReader br, long fieldDataOffset, int valueSize)
  {
    if (valueSize == -1)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
    }
    else
    {
      StringBuilder stringBuilder2 = new StringBuilder(valueSize);
    }
    int count = 0;
    br.BaseStream.Seek(fieldDataOffset, SeekOrigin.Begin);
    for (int index = 0; index < 70000 && br.ReadByte() != (byte) 0; ++index)
      ++count;
    if (count == 0 || count >= 70000)
      return string.Empty;
    br.BaseStream.Seek(fieldDataOffset, SeekOrigin.Begin);
    byte[] numArray = br.ReadBytes(count);
    Encoding encoding = Encoding.GetEncoding(1251);
    Encoding unicode = Encoding.Unicode;
    Encoding dstEncoding = unicode;
    byte[] bytes1 = numArray;
    byte[] bytes2 = Encoding.Convert(encoding, dstEncoding, bytes1);
    char[] chars = new char[unicode.GetCharCount(bytes2, 0, bytes2.Length)];
    unicode.GetChars(bytes2, 0, bytes2.Length, chars, 0);
    return new string(chars);
  }

  private int GetOldAvsFieldIndex(byte fieldNum, byte[] fieldsTypes)
  {
    int oldAvsFieldIndex = 0;
    foreach (int fieldsType in fieldsTypes)
    {
      if (fieldsType == (int) fieldNum)
        return oldAvsFieldIndex;
      ++oldAvsFieldIndex;
    }
    return -1;
  }

  private int[] ConvertBytesArrayToIntArray(byte[] byteArray)
  {
    int[] intArray = new int[byteArray.Length / 2];
    for (int index = 0; index < intArray.Length; ++index)
      intArray[index] = (int) byteArray[index * 2] + ((int) byteArray[index * 2 + 1] << 8);
    return intArray;
  }

  /// <summary>Сравнить объекты</summary>
  /// <param name="x">Объект x</param>
  /// <param name="y">Объект y</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  public int Compare(object x, object y)
  {
    if (x == y)
      return 0;
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    if (x is Chapter chapter1)
    {
      Chapter chapter = (Chapter) y;
      if (chapter1.ChapterSortIndex < chapter.ChapterSortIndex)
        return -1;
      if (chapter1.ChapterSortIndex > chapter.ChapterSortIndex)
        return 1;
      if (chapter1.SortIndex < chapter.SortIndex)
        return -1;
      return chapter1.SortIndex == chapter.SortIndex ? 0 : 1;
    }
    SpecificationSectionInfo specificationSectionInfo1 = (SpecificationSectionInfo) x;
    SpecificationSectionInfo specificationSectionInfo2 = (SpecificationSectionInfo) y;
    if (specificationSectionInfo1.SortIndex < specificationSectionInfo2.SortIndex)
      return -1;
    return specificationSectionInfo1.SortIndex == specificationSectionInfo2.SortIndex ? 0 : 1;
  }

  List<IVirtualTreeItem> IVirtualTreeItem.GetTreeChildren()
  {
    List<IVirtualTreeItem> treeChildren1 = new List<IVirtualTreeItem>();
    foreach (Chapter rootChapter in this.rootChapters)
    {
      List<IVirtualTreeItem> treeChildren2 = rootChapter.GetTreeChildren();
      if (this.AvsDocumentForm == AVSDocumentForm.Single || this.AvsDocumentForm == AVSDocumentForm.B)
      {
        treeChildren1.AddRange((IEnumerable<IVirtualTreeItem>) treeChildren2);
      }
      else
      {
        treeChildren1.Add((IVirtualTreeItem) rootChapter);
        foreach (IVirtualTreeItem virtualTreeItem in treeChildren2)
          virtualTreeItem.ParentItem = (IVirtualTreeItem) null;
      }
      foreach (IVirtualTreeItem virtualTreeItem in treeChildren1)
        virtualTreeItem.ParentItem = (IVirtualTreeItem) this;
    }
    return treeChildren1;
  }

  IVirtualTreeItem IVirtualTreeItem.ParentItem
  {
    get => (IVirtualTreeItem) null;
    set
    {
    }
  }

  bool IVirtualTreeItem.CanTreeShow() => true;

  void IVirtualTreeItem.GetRowData(RowData data)
  {
  }

  void IVirtualTreeItem.GetCellData(AVSColumn column, CellData data)
  {
  }

  bool IVirtualTreeItem.HeaderRow => true;

  public SortSchema SortSchema
  {
    get
    {
      if (this.sortSchema == null)
        this.LoadSpecificationSortSchema();
      return this.sortSchema;
    }
  }

  /// <summary>Не отображать атрибут Количество для документов</summary>
  [Browsable(false)]
  public bool HideCountForDocuments => true;

  internal long DocumentTemplateID
  {
    get => this.documentTemplateID;
    set
    {
      if (this.documentTemplateID == value)
        return;
      this.documentTemplateID = value;
    }
  }

  internal KeyWordsSchema MaterialKeyWordsSchema
  {
    get
    {
      if (this.materialKeyWordsSchema == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.MaterialKeyWordsSchema = (KeyWordsSchema) this.DocumentSettingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this.DocumentID, this.DocumentDBObjectType, this.AVSDocumentTemplateID, AvsIDCache.Attr_MaterialKeyWordsSchema, typeof (KeyWordsSchema));
      }
      return this.materialKeyWordsSchema;
    }
    set => this.materialKeyWordsSchema = value;
  }

  private void ResetSettingsFromTemplate()
  {
    this.skipLinesSchema = (SkipLinesSchema) null;
    this.sortSchema = (SortSchema) null;
    this.cellTextOutputAttributeMappingSettings = (OutputAttributeMappingScheme) null;
    this.avsCommonPropertiesSchema = (AVSCommonPropertiesSchema) null;
    this.dynamicGroupHeaderSettings = (DynamicGroupHeaderSettings) null;
    this.materialKeyWordsSchema = (KeyWordsSchema) null;
  }

  public IEnumerator<AVSRow> GetEnumerator()
  {
    return (IEnumerator<AVSRow>) this.GetAllRows(false, false).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>Класс для сравнения записей</summary>
  private class RowObjIDComparer : IComparer<AVSRow>
  {
    /// <summary>Метод сравнения записей</summary>
    /// <param name="x">Первая запись</param>
    /// <param name="y">Вторая запись</param>
    /// <returns>Результат сравнения.
    /// -1 означает x меньше y
    /// 0 означает x равно y
    /// 1 означает x больше y
    /// </returns>
    public int Compare(AVSRow x, AVSRow y) => (int) (x.ObjectId - y.ObjectId);
  }

  /// <summary>Вспомогательный класс для хранения результатов поиска связей в записях</summary>
  private class RelInRowForProduct
  {
    public AVSRow SpecRow;
    public RelationAttributeValuesCache RelData;

    /// <summary>Конструктор</summary>
    /// <param name="specRow">Запись</param>
    /// <param name="relData">Данные найденной связи</param>
    public RelInRowForProduct(AVSRow specRow, RelationAttributeValuesCache relData)
    {
      this.SpecRow = specRow;
      this.RelData = relData;
    }
  }

  private enum SizeType
  {
    Length,
    Area,
    Volume,
    Unknown,
  }

  /// <summary>Вспомогательный класс для хранения ид. версии изделия и ид. версии документа на него</summary>
  private class MainDocIdToProductIdLink
  {
    private long _productID;
    private long _mainDocID;

    /// <summary></summary>
    public MainDocIdToProductIdLink(long productID, long mainDocID)
    {
      this._productID = productID;
      this._mainDocID = mainDocID;
    }

    /// <summary></summary>
    public long ProductID
    {
      [DebuggerStepThrough] get => this._productID;
      set => this._productID = value;
    }

    /// <summary></summary>
    public long MainDocID
    {
      [DebuggerStepThrough] get => this._mainDocID;
      set => this._mainDocID = value;
    }
  }

  /// <summary>Класс-пара идентификаторов идентификатор_изделия и идентификатор_главного_документа_данного_объекта</summary>
  private class MainDocIdToProductIdLinkSearcher
  {
    private long _searchMainDocID;

    /// <summary></summary>
    public long SearchMainDocID
    {
      [DebuggerStepThrough] get => this._searchMainDocID;
      set => this._searchMainDocID = value;
    }

    /// <summary></summary>
    public bool CompareWithDocID(
      AVSDocument.MainDocIdToProductIdLink mainDocIdToProductIdLink)
    {
      return mainDocIdToProductIdLink.MainDocID == this._searchMainDocID;
    }
  }

  /// <summary>Параметры записи для поиска объектов в базе</summary>
  internal class RowParamsForSearch
  {
    /// <summary>Наименование</summary>
    public string Name;
    /// <summary>Обозначение</summary>
    public string Designation;

    /// <summary>Конструктор</summary>
    /// <param name="name">Наименование</param>
    /// <param name="designation">Обозначение</param>
    public RowParamsForSearch(string name, string designation)
    {
      this.Name = name;
      this.Designation = designation;
    }
  }
}
