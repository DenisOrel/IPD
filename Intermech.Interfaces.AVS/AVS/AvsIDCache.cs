// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AvsIDCache
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.Interfaces.AVS;

public class AvsIDCache
{
  /// <summary>Атрибут "Допустимые типы"</summary>
  public static readonly Guid AttrPossibleTypesGuid = new Guid("cad0027d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrSpecificationChapterGuid = new Guid("cad0027e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrSpecificationFormGuid = new Guid("cad00135-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrRefToImBaseDirectory = new Guid("cad00207-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrRefToImBaseFolder = new Guid("cad00208-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrClassGuid = new Guid("cad008d8-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrGostGuid = new Guid("cad003de-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrRazmery_I_ParametryGuid = new Guid("cad00211-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrDescriptionGuid = new Guid("cadd956d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrNameExpGuid = new Guid("cadd956e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrDynamicGroupHeaderSettings = new Guid("cadd9abf-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrGroupWithoutClassGuid = new Guid("cadd9bbf-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrListovGuid = new Guid("cad003a7-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrDerzPodlGuid = new Guid("cadd99e5-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrTypeNTDGuid = new Guid("cadd99fa-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrVidNTDGuid = new Guid("cadd9a91-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrKolExemplyarovGuid = new Guid("cadd9b8c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrNomerExemplaraGuid = new Guid("cadd9b8b-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrMestoGuid = new Guid("cadd9b89-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrKodProductionGuid = new Guid("cadd9b8a-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrKolInComplectGuid = new Guid("cadd9b8d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrUseGuid = new Guid("cad008fe-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrWireData = new Guid("cadd9a41-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrWireLength = new Guid("cadd9a42-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrWireWhere = new Guid("cadd9a43-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrHarnessDesignatin = new Guid("cadd9a44-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrClamp = new Guid("cadd9a45-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrPackage = new Guid("cadd9a46-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrWireDesignation = new Guid("cadd9a47-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrWireFrom = new Guid("cadd9a48-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrConnection = new Guid("cadd9a49-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrNameDoc = new Guid("cadd9c41-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrNameProg = new Guid("cadd9c1c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrTypePD = new Guid("cadd9c20-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjTypeChapterGuid = new Guid("cad0027f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_DetailDrawingGuid = new Guid("cad00261-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_AssemblyDrawingGuid = new Guid("cad00260-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_DetailModelsGuid = new Guid("cad0078f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_AssemblyModelsGuid = new Guid("cad00768-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_SpecificationGuid = new Guid("cad00133-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList0Guid = new Guid("cad015b4-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList1Guid = new Guid("cad015b5-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList2Guid = new Guid("cad015b6-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList3Guid = new Guid("cad0075c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList4Guid = new Guid("cad015b7-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList5Guid = new Guid("cad015b8-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList6Guid = new Guid("cad015b3-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjType_ElementList7Guid = new Guid("cad015b9-306c-11d8-b4e9-00304f19f545");
  /// <summary>Внутренний идентификатор типа Спецификация ЕСКД</summary>
  public static readonly Guid AVSDocTypeGuid_Specification = new Guid("382A2F65-185A-49bb-9992-FF60366FF89A");
  /// <summary>Внутренний идентификатор типа Спецификация автомобильная</summary>
  public static readonly Guid AVSDocTypeGuid_AutoIndustrySpecification = new Guid("213BB75D-2730-48c2-B7D6-BD772B38A71A");
  /// <summary>Внутренний идентификатор типа Спецификация экспортная</summary>
  public static readonly Guid AVSDocTypeGuid_ExportSpecification = new Guid("48FE08AD-C5E2-45CE-8924-C0F447140F59");
  /// <summary>Внутренний идентификатор типа Перечни элементов</summary>
  public static readonly Guid AVSDocTypeGuid_ElementList = new Guid("016E6B5E-2271-4944-82C6-4851DB803038");
  public static readonly Guid ObjType_ReportObjectsGuid = new Guid("cad00293-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid RelType_ZagotovkaGuid = new Guid("cadd9404-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrDesignationTrimGuid = new Guid("cad002a9-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrMaterialKeyWordsGuid = new Guid("cad002a5-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrAVSTemplateSettingsGuid = new Guid("cadd9211-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrVariableDataProductCaptionGuid = new Guid("cad01486-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrOwnerLink = new Guid("cad001a6-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AttrConstructorDocumentPropertiesGuid = new Guid("cadd9230-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут Условное наименование</summary>
  public static readonly Guid AttrProductConventionalName_Guid = new Guid("cad015db-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут Наименование в документах AVS</summary>
  public static readonly Guid AttrNameForAVS_Guid = new Guid("cadd938d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут Необходимо обновить документ</summary>
  public static readonly Guid AttrNeedUpdateDoc_Guid = new Guid("cadd93f8-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Примечание ПЭ"</summary>
  public static readonly Guid AttrNotePE_Guid = new Guid("cadd98bd-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateElementList = new Guid("cad0159c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateCommonSpecification = new Guid("cad0026f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateSingleSpecification = new Guid("cad00296-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateSpecificationFormB = new Guid("cad00298-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateSpecificationFormV = new Guid("cadd9380-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateExportSpecification = new Guid("cadd9568-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateSingleAutopromSpecification = new Guid("cadd9215-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateAutopromSpecificationFormB = new Guid("cadd9219-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateMirrorSpecification = new Guid("cadd9217-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StdTemplateSpecificationTitlePage = new Guid("cadd9bdf-306c-11d8-b4e9-00304f19f545");
  /// <summary>Фиктивный Guid раздела сортировки в перечне элементов</summary>
  public static readonly Guid ObjIdElementListSortChapterGuid = new Guid("02297013-7D1B-4d32-86FE-DF3D2F365E1B");
  /// <summary>Идентификатор объекта "Общий шаблон спецификаций"</summary>
  public static long ObjID_CommonSpecificationTemplate_ = -1;
  /// <summary>Идентификатор объекта "Шаблон перечней элементов"</summary>
  public static long ObjID_StdTemplateElementList_ = -1;
  /// <summary>Идентификатор объекта "Шаблон единичных спецификаций"</summary>
  public static long ObjID_StdTemplateSingleSpecification_ = -1;
  /// <summary> Идентификатор объекта "Шаблон групповых СП формы Б" </summary>
  public static long ObjID_StdTemplateSpecificationFormB_ = -1;
  /// <summary> Идентификатор объекта "Шаблон групповых СП формы В" </summary>
  public static long ObjID_StdTemplateSpecificationFormV_ = -1;
  /// <summary>Идентификатор объекта "Шаблон единичных автомобилестроительных спецификаций"</summary>
  public static long ObjID_StdTemplateSingleAutopromSpecification_ = -1;
  /// <summary>Идентификатор объекта "Шаблон групповых автомобилестроительных спецификаций формы Б"</summary>
  public static long ObjID_StdTemplateAutopromSpecificationFormB_ = -1;
  /// <summary>Идентификатор объекта "Шаблон зеркальных СП" </summary>
  public static long ObjID_StdTemplateMirrorSpecification_ = -1;
  /// <summary>Идентификатор объекта "Шаблон экспортных СП" </summary>
  public static long ObjID_StdTemplateExportSpecification_ = -1;
  /// <summary>Идентификатор объекта "Шаблон титульного листа спецификаций"</summary>
  public static long ObjID_StdTemplateSpecificationTitlePage_ = -1;
  private static int objType_ConstructorDocumentTemplate = -1;
  /// <summary>Идентификатор типа объекта "Шаблоны конструкторских ведомостей"</summary>
  private static int objType_VedomostDocumentTemplate = -1;
  /// <summary>Идентификатор типа объекта "Конструкторских ведомостей"</summary>
  private static int objType_Vedomost = -1;
  /// <summary>Идентификатор типа объекта "Шаблоны конструкторских таблиц"</summary>
  private static int objType_ConstrTablTemplate = -1;
  /// <summary>Идентификатор типа объекта "Конструкторских таблиц"</summary>
  private static int objType_ConstrTabl = -1;
  /// <summary>Идентификатор типа объекта "Документы эксплуатационные"</summary>
  private static int objType_DocumsExpluat = -1;
  /// <summary>Идентификатор типа объекта "Программные документы"</summary>
  private static int objType_DocumsProg = -1;
  /// <summary>Идентификатор типа объекта "Спецификации Espd "</summary>
  private static int objType_Espd = -1;
  /// <summary>Идентификатор типа объекта "Espd_ЛУ "</summary>
  private static int objType_EspdLU = -1;
  private static int objType_Specification = -1;
  private static int objType_AssemblyUnit = -1;
  private static int objType_Complect = -1;
  private static int objType_Complex = -1;
  private static int objType_Product = -1;
  private static int objType_ProсessComposition = -1;
  private static int objType_SpecificationSection = -1;
  private static int objType_SpecificationChapter = -1;
  private static int objType_Document = -1;
  private static int objType_Detail = -1;
  private static int objType_StandartProduct = -1;
  private static int objType_OtherProduct = -1;
  private static int objType_Materials = -1;
  private static int objType_ConstructorDocument = -1;
  private static int objType_OperationDocumentsSheet = -1;
  private static int objType_GeneralRepairDocumentsSheet = -1;
  private static int objType_MediumRepairDocumentsSheet = -1;
  private static int objType_DetailDrawing = -1;
  private static int[] baseProductForSpecificationTypes;
  private static int objType_ElementList0 = -1;
  private static int objType_ElementList1 = -1;
  private static int objType_ElementList2 = -1;
  private static int objType_ElementList3 = -1;
  private static int objType_ElementList4 = -1;
  private static int objType_ElementList5 = -1;
  private static int objType_ElementList6 = -1;
  private static int objType_ElementList7 = -1;
  private static int objType_DetailWithoutDrawing = -1;
  private static int objType_AssemblyDrawing = -1;
  private static int objType_Orders = -1;
  private static int objType_DetailModels = -1;
  private static int objType_AssemblyModels = -1;
  private static int objType_VedomostiSection = -1;
  private static int relation_Project = -1;
  private static int relation_Zagotovka = -1;
  private static int relation_AddComplect = -1;
  private static int relation_Podbor = -1;
  private static int relation_Reference = -1;
  private static int relation_Document = -1;
  public static Guid Attr_Count_Guid = new Guid("cad00267-306c-11d8-b4e9-00304f19f545");
  /// <summary>Информация об атрибуте для графы Количество</summary>
  public static AvsRowAttributeInfo StdField_Count = AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, AvsIDCache.Attr_Count_Guid);
  private static int attr_Count = -1;
  private static int attr_CountForAdjustment = -1;
  private static int attr_Weight = -1;
  private static int attr_UnitWeight = -1;
  private static int attr_Size = -1;
  private static int attr_Material = -1;
  private static int attr_Designation = -1;
  private static int attr_IsOnESPD = -1;
  private static int attr_Name = -1;
  /// <summary>Поле документа "Наименование"</summary>
  public static string DocAttr_Name = "Наименование";
  private static AvsRowAttributeInfo stdField_Name;
  private static int attr_Description = -1;
  private static int attr_NameExp = -1;
  private static int attr_NameForAVS = -1;
  /// <summary>Атрибут связи "Примечание"</summary>
  public static AvsRowAttributeInfo StdField_Note = AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, "cad00021-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут связи "Примечание ПЭ"</summary>
  public static AvsRowAttributeInfo StdField_NotePE = AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, AvsIDCache.AttrNotePE_Guid);
  private static int attr_NoteFieldSettings = -1;
  private static int attr_PossibleTypes = -1;
  private static int attr_RefToImBaseDirectory = -1;
  private static int attr_SpecificationSection = -1;
  private static int attr_SpecificationСhapter = -1;
  private static int attr_SortIndex = -1;
  private static int attr_AllowableSections = -1;
  public static Guid Attr_CommonPositionGuid = new Guid("cadd9941-306c-11d8-b4e9-00304f19f545");
  private static int attr_CommonPosition = -1;
  private static int attr_SortSchema = -1;
  private static int attr_NumberingSchema = -1;
  private static int attr_OutputMappingSchema = -1;
  private static int attr_DynamicHeaderKeywordReplacementSchema = -1;
  private static int attr_DesignationTrimSchema = -1;
  private static int attr_MaterialKeyWordsSchema = -1;
  private static int attr_AVSTemplateSettings = -1;
  private static int attr_VariableDataProductCaption = -1;
  private static int attr_OwnerLink = -1;
  private static int attr_ConstructorDocumentProperties = -1;
  private static int attr_Position = -1;
  public static Guid attributePodborForPosDesignation = new Guid("cadd9741-306c-11d8-b4e9-00304f19f545");
  private static int attr_PodborForPosDesignation = -1;
  private static int attr_PosDesignation = -1;
  private static int attr_ProductConventionalName = -1;
  private static int attr_SortAVS = -1;
  private static int attr_SkipLines = -1;
  private static int attr_DynamicGroupHeaderSettings = -1;
  private static int attr_DopZamenGroupNum = -1;
  private static int attr_DopZamenNumInGroup = -1;
  private static int attr_DopZamenGroupName = -1;
  private static int attr_DopZamenSubstituteName = -1;
  private static int attr_DesignerActualVariant = -1;
  /// <summary>Информация об атрибуте "Расшифровка допустимых замен"</summary>
  public static AvsRowAttributeInfo DopZamenTextAttrInfo = AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, "cad00274-306c-11d8-b4e9-00304f19f545");
  private static int attr_SectionNum = -1;
  private static int attr_PartNum = -1;
  private static int attr_PartName = -1;
  private static int attr_InsertToSection = -1;
  private static int attr_ArticleGroupID = -1;
  public static Guid Attr_Podbor_Guid = new Guid("cadd943a-306c-11d8-b4e9-00304f19f545");
  private static int attr_Podbor = -1;
  public static AvsRowAttributeInfo[] VirtualAttributes;
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_PartForPodbor_NoteText = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("431b1d71-5e1f-4c2b-b336-f06ec287bbf0"), -50001, "Текст в примечании элемента для подбора", true);
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_NominalAndLimitValues_NoteText = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("8117c5d8-340b-46be-83a7-7f13e75e72d8"), -50002, "Значение номинала или Предельные значения", true);
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_DraftForPartTextLink = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("0d9870b6-a072-49dc-b62c-1d8cfcebbd9b"), -50003, "Заготовка для", true);
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_LookMainDocTextLink = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("f4e523da-50b2-42a1-848d-fbf0e958ed6c"), -50004, "Смотри", true);
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_AdditionalNameNote = new AvsRowAttributeInfo(FieldSource.Relation, new Guid("4ecc7320-68ce-4ac9-9896-9f320bf99e80"), -50005, "Дополнительный текст в Наименовании");
  [VirtualAttribute]
  public static AvsRowAttributeInfo Attr_NameForSpecification = new AvsRowAttributeInfo(FieldSource.Object, new Guid("57de894f-6870-4df7-a4bd-6be0fdb43eaf"), -50006, "Наименование для спецификации", true);
  public const string DocAttr_CountMeasure = "#CountMeasure";
  [VirtualAttribute]
  public static AvsRowAttributeInfo CountMeasureAttrInfo = new AvsRowAttributeInfo(FieldSource.Object, new Guid("02250690-b0cb-409b-8487-de6b812e0ef6"), -50009, "Единицы измерения Количества", true);
  private static int attr_ProductCode = -1;
  /// <summary>Информация об атрибуте для графы Формат</summary>
  public static AvsRowAttributeInfo StdField_Format = AvsRowAttributeInfo.CreateByGuid(FieldSource.Object, "cad00255-306c-11d8-b4e9-00304f19f545");
  /// <summary>Информация об атрибуте для графы Зона</summary>
  public static AvsRowAttributeInfo StdField_Zone = AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, "cad0027a-306c-11d8-b4e9-00304f19f545");
  private static int attr_SpecificationForm = -1;
  private static int attr_File = -1;
  private static int attr_DocumentFile = -1;
  private static int attr_ScanDocument = -1;
  private static int attr_Author = -1;
  private static int attr_Subdivision = -1;
  private static int attr_Litera = -1;
  private static int attr_CheckedBy = -1;
  private static int attr_NeedUpdateDoc = -1;
  private static int attr_NormoControlledBy = -1;
  private static int attr_ConfirmBy = -1;
  private static int attr_CADInteranceIdentify = -1;
  private static int attr_BasedOnCADModel = -1;
  public static Guid Attr_BasedOnCADModelGuid = new Guid("cad0153e-306c-11d8-b4e9-00304f19f545");
  private static int attr_OccurenceKey = -1;
  public static Guid Attr_OccurenceKeyGuid = new Guid("cad0027b-306c-11d8-b4e9-00304f19f545");
  private static int attr_CodePosition = -1;
  private static int attr_OKPCode = -1;
  private static int attr_VersionInRelation = -1;
  private static int attr_InMainDocComplect = -1;
  private static int attr_ObjectPrototype = -1;
  private static AvsRowAttributeInfo _attr_SearchId = (AvsRowAttributeInfo) null;
  private static int attr_Class = -1;
  private static int attr_OldAVSINI = -1;
  private static int attr_Gost = -1;
  private static int attr_TypeNTD = -1;
  private static int attr_VidNTD = -1;
  private static int attr_Listov = -1;
  private static int attr_DerzPodl = -1;
  private static int attr_KolExemplyarov = -1;
  private static int attr_NomerExemplara = -1;
  private static int attr_Mesto = -1;
  private static int attr_KodProduction = -1;
  private static int attr_KolInComplect = -1;
  private static int attr_Use = -1;
  /// <summary>Идентификатор атрибута "Обозначение зажима"</summary>
  private static int attr_Clamp = -1;
  /// <summary>Идентификатор атрибута "Обозначение набора"</summary>
  private static int attr_Package = -1;
  /// <summary>Идентификатор атрибута "Соединение"</summary>
  private static int attr_Connection = -1;
  /// <summary>Идентификатор атрибута "Длина провода"</summary>
  private static int attr_WireLength = -1;
  /// <summary>Идентификатор атрибута "Данные провода"</summary>
  private static int attr_WireData = -1;
  /// <summary>Идентификатор атрибута "Откуда идет"</summary>
  private static int attr_WireFrom = -1;
  /// <summary>Идентификатор атрибута "Куда поступает"</summary>
  private static int attr_WireWhere = -1;
  /// <summary>Идентификатор атрибута "Обозначение провода"</summary>
  private static int attr_WireDesignation = -1;
  /// <summary>Идентификатор атрибута "Обозначение жгута"</summary>
  private static int attr_HarnessDesignatin = -1;
  /// <summary>Идентификатор атрибута "Наименование документа"</summary>
  private static int attr_NameDoc = -1;
  /// <summary>Идентификатор атрибута "Наименование программы"</summary>
  private static int attr_NameProg = -1;
  /// <summary>Идентификатор атрибута "Тип программного документа"</summary>
  private static int attr_TypePD = -1;
  private static int attr_OldAvsIniFileNames = -1;
  private static int attr_OldAVSSettingsIniFiles = -1;
  private static int attr_OldAVSSettingsFileTypes = -1;
  private static int attr_OldAVSSettingsDefaultIniFile = -1;
  private static int attr_ImbaseKey = -1;
  private static int attr_ContentModifyDate = -1;
  private static int attr_FirstApplicability = -1;
  private static int attr_FuncGroup = -1;
  /// <summary>Глобальный идентификатор атрибута "Позиционное обозначение функциональной группы"</summary>
  public static readonly Guid attributeFGPosDesignation = new Guid("cadd973d-306c-11d8-b4e9-00304f19f545");
  private static int attr_FGPosDesignation = -1;
  /// <summary>Глобальный идентификатор атрибута "Обозначение функциональной группы"</summary>
  public static readonly Guid attributeFGDesignation = new Guid("cadd973f-306c-11d8-b4e9-00304f19f545");
  private static int attr_FGDesignation = -1;
  /// <summary>Глобальный идентификатор атрибута "Наименование функциональной группы"</summary>
  public static readonly Guid attributeFGName = new Guid("cadd973e-306c-11d8-b4e9-00304f19f545");
  private static int attr_FGName = -1;
  /// <summary>Глобальный идентификатор атрибута "Предельные значения"</summary>
  public static readonly Guid attributeLimitValues = new Guid("cadd973c-306c-11d8-b4e9-00304f19f545");
  private static int attr_LimitValues = -1;
  /// <summary>Глобальный идентификатор атрибута "Значение номинала"</summary>
  public static Guid Attr_NominalValue_Guid = new Guid("cadd9963-306c-11d8-b4e9-00304f19f545");
  private static int attr_NominalValue = -1;
  /// <summary>Глобальный идентификатор атрибута "Позиционное обозначение ДС"</summary>
  public static readonly Guid attributeSymbolForPosDesignation = new Guid("cadd98d4-306c-11d8-b4e9-00304f19f545");
  private static int attr_SymbolForPosDesignation = -1;
  /// <summary>Глобальный идентификатор атрибута "Элемент перечня элементов"</summary>
  public static readonly Guid attributeIncludeInElementList = new Guid("cadd973b-306c-11d8-b4e9-00304f19f545");
  private static int attr_IncludeInElementList = -1;
  /// <summary>Глобальный идентификатор атрибута "Не отображать в спецификации"</summary>
  public static readonly Guid attributeHideInSpecification = new Guid("cadd9979-306c-11d8-b4e9-00304f19f545");
  private static int attr_HideInSpecification = -1;
  private static int attr_PokupnIzd = -1;
  private static int attr_Postavthik = -1;
  private static int attr_RazdVedZip = -1;
  private static int attr_Razmery_I_Parametry = -1;
  private static int attr_ArticleID = -1;
  private static int attr_GroupWithoutClass = -1;
  /// <summary>Идентификаторы необходимые серверной части были закэшированы</summary>
  public static bool AttrsForAVSServerCached = false;
  /// <summary> Индекс поля "Формат" старого AVS </summary>
  public static byte OldAVDFieldNum_Format = 1;
  /// <summary> Индекс поля "Зона" старого AVS </summary>
  public static byte OldAVDFieldNum_Zona = 2;
  /// <summary> Индекс поля "Позиция" старого AVS </summary>
  public static byte OldAVDFieldNum_Position = 3;
  /// <summary> Индекс поля "Наименование" старого AVS </summary>
  public static byte OldAVDFieldNum_Name = 5;
  /// <summary> Индекс поля "Обозначение" старого AVS </summary>
  public static byte OldAVDFieldNum_Designation = 4;
  /// <summary> Индекс поля "Код ОКП" старого AVS </summary>
  public static byte OldAVDFieldNum_OkpCode = 28;
  /// <summary> Индекс поля "Позиционное обозначение" старого AVS </summary>
  public static byte OldAVDFieldNum_PosDesignation = 32 /*0x20*/;
  /// <summary> Индекс поля "Количество" старого AVS </summary>
  public static byte OldAVDFieldNum_Count = 6;
  /// <summary> Индекс поля "Код старого IMBASE" старого AVS </summary>
  public static byte OldAVDFieldNum_OldImbaseCode = 8;
  /// <summary> Индекс поля "Не печатать" старого AVS </summary>
  public static byte OldAVDFieldNum_DoNotPrint = 132;
  /// <summary> Индекс поля "Примечание" старого AVS </summary>
  public static byte OldAVDFieldNum_Note = 7;
  /// <summary> Индекс поля "Инвентарный номер документа (DocId)" старого AVS </summary>
  public static byte OldAVDFieldNum_DocId = 137;
  /// <summary> Индекс поля "Инвентарный номер изделия (ArtId)" старого AVS </summary>
  public static byte OldAVDFieldNum_ArtId = 22;
  /// <summary> Индекс поля "Файл настроек" в паспорте старого AVS </summary>
  public static byte OldAVDPassportNum_Note = 132;
  private static bool typeNameDictionaryInitialized = false;

  static AvsIDCache() => AvsIDCache.CollectVirtualAttributes();

  /// <summary>Получить идентификатор объекта "Общий шаблон спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetCommonSpecificationTemplateId(IUserSession session)
  {
    return AvsIDCache.GetCommonSpecificationTemplateId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Общий шаблон спецификаций"</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  public static long GetCommonSpecificationTemplateId(IUserSession session, out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_CommonSpecificationTemplate_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_CommonSpecificationTemplate_, false);
      if (objectActualCopy != null)
      {
        templateGuid = new Guid("cad0026f-306c-11d8-b4e9-00304f19f545");
        return AvsIDCache.ObjID_CommonSpecificationTemplate_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"), false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_CommonSpecificationTemplate_ = dbObject.ObjectID;
      templateGuid = new Guid("cad0026f-306c-11d8-b4e9-00304f19f545");
    }
    return AvsIDCache.ObjID_CommonSpecificationTemplate_;
  }

  /// <summary>Получить идентификатор объекта "Шаблон перечней элементов" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateElementListId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateElementListId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон перечней элементов" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateElementListId(IUserSession session, out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateElementList_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateElementList_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateElementList;
        return AvsIDCache.ObjID_StdTemplateElementList_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateElementList, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateElementList_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateElementList;
    }
    return AvsIDCache.ObjID_StdTemplateElementList_;
  }

  /// <summary>Получить идентификатор объекта "Шаблон единичных спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateSingleSpecificationId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateSingleSpecificationId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон единичных спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateSingleSpecificationId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateSingleSpecification_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateSingleSpecification_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateSingleSpecification;
        return AvsIDCache.ObjID_StdTemplateSingleSpecification_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateSingleSpecification, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateSingleSpecification_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateSingleSpecification;
    }
    return AvsIDCache.ObjID_StdTemplateSingleSpecification_;
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы Б" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateSpecificationFormBId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateSpecificationFormBId(session, out Guid _);
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы Б" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateSpecificationFormBId(IUserSession session, out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateSpecificationFormB_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateSpecificationFormB_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateSpecificationFormB;
        return AvsIDCache.ObjID_StdTemplateSpecificationFormB_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateSpecificationFormB, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateSpecificationFormB_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateSpecificationFormB;
    }
    return AvsIDCache.ObjID_StdTemplateSpecificationFormB_;
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы В" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateSpecificationFormVId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateSpecificationFormVId(session, out Guid _);
  }

  /// <summary> Идентификатор объекта "Шаблон групповых СП формы В" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateSpecificationFormVId(IUserSession session, out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateSpecificationFormV_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateSpecificationFormV_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateSpecificationFormV;
        return AvsIDCache.ObjID_StdTemplateSpecificationFormV_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateSpecificationFormV, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateSpecificationFormV_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateSpecificationFormV;
    }
    return AvsIDCache.ObjID_StdTemplateSpecificationFormV_;
  }

  /// <summary>Получить идентификатор объекта "Шаблон единичных автомобилестроительных спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateSingleAutopromSpecificationId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateSingleAutopromSpecificationId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон единичных автомобилестроительных спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateSingleAutopromSpecificationId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateSingleAutopromSpecification_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateSingleAutopromSpecification_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateSingleAutopromSpecification;
        return AvsIDCache.ObjID_StdTemplateSingleAutopromSpecification_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateSingleAutopromSpecification, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateSingleAutopromSpecification_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateSingleAutopromSpecification;
    }
    return AvsIDCache.ObjID_StdTemplateSingleAutopromSpecification_;
  }

  /// <summary>Получить идентификатор объекта "Шаблон групповых автомобилестроительных спецификаций формы Б" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateAutopromSpecificationFormBId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateAutopromSpecificationFormBId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон групповых автомобилестроительных спецификаций формы Б" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateAutopromSpecificationFormBId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateAutopromSpecificationFormB_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateAutopromSpecificationFormB_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateAutopromSpecificationFormB;
        return AvsIDCache.ObjID_StdTemplateAutopromSpecificationFormB_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateAutopromSpecificationFormB, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateAutopromSpecificationFormB_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateAutopromSpecificationFormB;
    }
    return AvsIDCache.ObjID_StdTemplateAutopromSpecificationFormB_;
  }

  /// <summary> Идентификатор объекта "Шаблон групповых зекральных СП" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateMirrorSpecificationId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateMirrorSpecificationId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон зеркальных СП" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  public static long GetStdTemplateMirrorSpecificationId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateMirrorSpecification_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateMirrorSpecification_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateMirrorSpecification;
        return AvsIDCache.ObjID_StdTemplateMirrorSpecification_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateMirrorSpecification, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateMirrorSpecification_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateMirrorSpecification;
    }
    return AvsIDCache.ObjID_StdTemplateMirrorSpecification_;
  }

  /// <summary> Идентификатор объекта "Шаблон экспортных СП" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateExportSpecification(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateExportSpecificationId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон экспортных СП" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  public static long GetStdTemplateExportSpecificationId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateExportSpecification_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateExportSpecification_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateExportSpecification;
        return AvsIDCache.ObjID_StdTemplateExportSpecification_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateExportSpecification, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateExportSpecification_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateExportSpecification;
    }
    return AvsIDCache.ObjID_StdTemplateExportSpecification_;
  }

  /// <summary>Получить идентификатор стандартного шаблона спецификации</summary>
  /// <param name="iUserSession">Сессия пользователя</param>
  /// <param name="docType">Тип документа</param>
  /// <param name="specForm">Форма спецификации</param>
  /// <param name="failIfNotFound">Генерировать исключение, если шаблон не найден</param>
  /// <returns>Ид шаблона</returns>
  public static long GetStdTemplateId(
    IUserSession iUserSession,
    AVSDocumentType docType,
    AVSDocumentForm specForm,
    bool failIfNotFound)
  {
    Guid templateGuid = Guid.Empty;
    return AvsIDCache.GetStdTemplateId(iUserSession, docType, specForm, out templateGuid, failIfNotFound);
  }

  /// <summary>Получить идентификатор стандартного шаблона спецификации</summary>
  /// <param name="iUserSession">Сессия пользователя</param>
  /// <param name="docType">Тип документа</param>
  /// <param name="specForm">Форма спецификации</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <param name="failIfNotFound">Генерировать исключение, если шаблон не найден</param>
  /// <returns>Ид шаблона</returns>
  public static long GetStdTemplateId(
    IUserSession iUserSession,
    AVSDocumentType docType,
    AVSDocumentForm specForm,
    out Guid templateGuid,
    bool failIfNotFound)
  {
    templateGuid = Guid.Empty;
    long stdTemplateId = -1;
    switch (docType)
    {
      case AVSDocumentType.Specification:
        switch (specForm)
        {
          case AVSDocumentForm.Single:
          case AVSDocumentForm.A:
            stdTemplateId = AvsIDCache.GetStdTemplateSingleSpecificationId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон единичной спецификации!");
            break;
          case AVSDocumentForm.B:
            stdTemplateId = AvsIDCache.GetStdTemplateSpecificationFormBId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон групповой спецификации формы Б!");
            break;
          case AVSDocumentForm.Mirror:
            stdTemplateId = AvsIDCache.GetStdTemplateMirrorSpecificationId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон зеркальной спецификации!");
            break;
          case AVSDocumentForm.V:
            stdTemplateId = AvsIDCache.GetStdTemplateSpecificationFormVId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон групповой спецификации формы В!");
            break;
        }
        break;
      case AVSDocumentType.AutoIndustrySpecification:
        switch (specForm)
        {
          case AVSDocumentForm.Single:
          case AVSDocumentForm.A:
            stdTemplateId = AvsIDCache.GetStdTemplateSingleAutopromSpecificationId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон единичной автомобилестроительной спецификации!");
            break;
          case AVSDocumentForm.B:
            stdTemplateId = AvsIDCache.GetStdTemplateAutopromSpecificationFormBId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон групповой автомобилестроительной спецификации формы Б!");
            break;
          case AVSDocumentForm.Mirror:
            stdTemplateId = AvsIDCache.GetStdTemplateMirrorSpecificationId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон зеркальной спецификации!");
            break;
        }
        break;
      case AVSDocumentType.ExportSpecification:
        switch (specForm)
        {
          case AVSDocumentForm.Single:
          case AVSDocumentForm.A:
            stdTemplateId = AvsIDCache.GetStdTemplateExportSpecificationId(iUserSession, out templateGuid);
            if (stdTemplateId == -1L & failIfNotFound)
              throw new Exception("Не найден шаблон экспортной спецификации!");
            break;
        }
        break;
      case AVSDocumentType.ElementList:
        stdTemplateId = AvsIDCache.GetStdTemplateElementListId(iUserSession, out templateGuid);
        if (stdTemplateId == -1L & failIfNotFound)
          throw new Exception("Не найден шаблон единичной спецификации!");
        break;
    }
    return stdTemplateId;
  }

  /// <summary>Получить идентификатор стандартного шаблона спецификации</summary>
  /// <param name="iUserSession">Сессия пользователя</param>
  /// <param name="docType">Тип документа</param>
  /// <param name="specForm">Форма конструкторского документа. Если null, то возвращает общий шаблон</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <param name="failIfNotFound">Генерировать исключение, если шаблон не найден</param>
  /// <returns>Ид шаблона</returns>
  public static long GetStdTemplateId(
    IUserSession iUserSession,
    Guid docTypeGuid,
    AVSDocumentForm? specForm,
    out Guid templateGuid,
    bool failIfNotFound)
  {
    templateGuid = Guid.Empty;
    long stdTemplateId = -1;
    if (docTypeGuid == AvsIDCache.AVSDocTypeGuid_ElementList)
    {
      stdTemplateId = AvsIDCache.GetStdTemplateElementListId(iUserSession, out templateGuid);
      if (stdTemplateId == -1L & failIfNotFound)
        throw new Exception("Не найден шаблон единичной спецификации!");
    }
    else if (docTypeGuid == AvsIDCache.AVSDocTypeGuid_Specification)
    {
      if (!specForm.HasValue)
        return AvsIDCache.GetCommonSpecificationTemplateId(iUserSession, out templateGuid);
      switch (specForm.Value)
      {
        case AVSDocumentForm.Single:
        case AVSDocumentForm.A:
          stdTemplateId = AvsIDCache.GetStdTemplateSingleSpecificationId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон единичной спецификации!");
          break;
        case AVSDocumentForm.B:
          stdTemplateId = AvsIDCache.GetStdTemplateSpecificationFormBId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон групповой спецификации формы Б!");
          break;
        case AVSDocumentForm.Mirror:
          stdTemplateId = AvsIDCache.GetStdTemplateMirrorSpecificationId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон зеркальной спецификации!");
          break;
        case AVSDocumentForm.V:
          stdTemplateId = AvsIDCache.GetStdTemplateSpecificationFormVId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон групповой спецификации формы В!");
          break;
      }
    }
    else if (docTypeGuid == AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification)
    {
      if (!specForm.HasValue)
        return AvsIDCache.GetCommonSpecificationTemplateId(iUserSession, out templateGuid);
      switch (specForm.Value)
      {
        case AVSDocumentForm.Single:
        case AVSDocumentForm.A:
          stdTemplateId = AvsIDCache.GetStdTemplateSingleAutopromSpecificationId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон единичной автомобилестроительной спецификации!");
          break;
        case AVSDocumentForm.B:
          stdTemplateId = AvsIDCache.GetStdTemplateAutopromSpecificationFormBId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон групповой автомобилестроительной спецификации формы Б!");
          break;
        case AVSDocumentForm.Mirror:
          stdTemplateId = AvsIDCache.GetStdTemplateMirrorSpecificationId(iUserSession, out templateGuid);
          if (stdTemplateId == -1L & failIfNotFound)
            throw new Exception("Не найден шаблон зеркальной спецификации!");
          break;
      }
    }
    return stdTemplateId;
  }

  /// <summary>Получить идентификатор объекта "Шаблон титульного листа спецификаций" </summary>
  /// <param name="session">Сессия пользователя</param>
  public static long GetStdTemplateSpecificationTitlePageId(IUserSession session)
  {
    return AvsIDCache.GetStdTemplateSpecificationTitlePageId(session, out Guid _);
  }

  /// <summary>Получить идентификатор объекта "Шаблон титульного листа спецификаци" </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <returns></returns>
  public static long GetStdTemplateSpecificationTitlePageId(
    IUserSession session,
    out Guid templateGuid)
  {
    templateGuid = Guid.Empty;
    if (AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_ != -1L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_, false);
      if (objectActualCopy != null)
      {
        templateGuid = AvsIDCache.StdTemplateSpecificationTitlePage;
        return AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_ = objectActualCopy.ObjectID;
      }
    }
    IDBObject dbObject = session.GetObject(AvsIDCache.StdTemplateSpecificationTitlePage, false);
    if (dbObject != null)
    {
      AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_ = dbObject.ObjectID;
      templateGuid = AvsIDCache.StdTemplateSpecificationTitlePage;
    }
    return AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_;
  }

  public static int GetObjectType(string guidStr)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid(guidStr));
    return objectType != null ? objectType.ObjectTypeID : -1;
  }

  public static int GetObjectType(Guid guid)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
    return objectType != null ? objectType.ObjectTypeID : -1;
  }

  /// <summary>Идентификатор типа объекта "Шаблоны конструкторских документов"</summary>
  public static int ObjType_ConstructorDocumentTemplate
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ConstructorDocumentTemplate == -1)
        AvsIDCache.objType_ConstructorDocumentTemplate = AvsIDCache.GetObjectType("cad00269-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_ConstructorDocumentTemplate;
    }
  }

  public static int ObjType_VedomostDocumentTemplate
  {
    get
    {
      if (AvsIDCache.objType_VedomostDocumentTemplate == -1)
        AvsIDCache.objType_VedomostDocumentTemplate = AvsIDCache.GetObjectType(new Guid("cadd98b1-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_VedomostDocumentTemplate;
    }
  }

  public static int ObjType_Vedomost
  {
    get
    {
      if (AvsIDCache.objType_Vedomost == -1)
        AvsIDCache.objType_Vedomost = AvsIDCache.GetObjectType(new Guid("cad00196-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_Vedomost;
    }
  }

  public static int ObjType_ConstrTablTemplate
  {
    get
    {
      if (AvsIDCache.objType_ConstrTablTemplate == -1)
        AvsIDCache.objType_ConstrTablTemplate = AvsIDCache.GetObjectType(new Guid("cadd9a3c-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_ConstrTablTemplate;
    }
  }

  public static int ObjType_ConstrTabl
  {
    get
    {
      if (AvsIDCache.objType_ConstrTabl == -1)
        AvsIDCache.objType_ConstrTabl = AvsIDCache.GetObjectType(new Guid("cadd9a3d-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_ConstrTabl;
    }
  }

  public static int ObjType_DocumsExpluat
  {
    get
    {
      if (AvsIDCache.objType_DocumsExpluat == -1)
        AvsIDCache.objType_DocumsExpluat = AvsIDCache.GetObjectType(new Guid("cad0077e-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_DocumsExpluat;
    }
  }

  public static int ObjType_DocumsProg
  {
    get
    {
      if (AvsIDCache.objType_DocumsProg == -1)
        AvsIDCache.objType_DocumsProg = AvsIDCache.GetObjectType(new Guid("cadd9c40-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_DocumsProg;
    }
  }

  public static int ObjType_Espd
  {
    get
    {
      if (AvsIDCache.objType_Espd == -1)
        AvsIDCache.objType_Espd = AvsIDCache.GetObjectType(new Guid("cadd9c0b-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_Espd;
    }
  }

  public static int ObjType_EspdLU
  {
    get
    {
      if (AvsIDCache.objType_EspdLU == -1)
        AvsIDCache.objType_EspdLU = AvsIDCache.GetObjectType(new Guid("cadd9c0d-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.objType_EspdLU;
    }
  }

  /// <summary>Идентификатор типа объекта "Спецификация"</summary>
  public static int ObjType_Specification
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Specification == -1)
        AvsIDCache.objType_Specification = AvsIDCache.GetObjectType("cad00133-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Specification;
    }
  }

  /// <summary>Идентификатор типа объекта "Сборочная единица"</summary>
  public static int ObjType_AssemblyUnit
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_AssemblyUnit == -1)
        AvsIDCache.objType_AssemblyUnit = AvsIDCache.GetObjectType("cad00132-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_AssemblyUnit;
    }
  }

  /// <summary>Идентификатор типа объекта "Комплект"</summary>
  public static int ObjType_Complect
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Complect == -1)
        AvsIDCache.objType_Complect = AvsIDCache.GetObjectType("cad0025f-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Complect;
    }
  }

  /// <summary>Идентификатор типа объекта "Комплекс"</summary>
  public static int ObjType_Complex
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Complex == -1)
        AvsIDCache.objType_Complex = AvsIDCache.GetObjectType("cad0025e-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Complex;
    }
  }

  /// <summary>Идентификатор типа объекта "Изделие"</summary>
  public static int ObjType_Product
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Product == -1)
        AvsIDCache.objType_Product = AvsIDCache.GetObjectType("cad00268-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Product;
    }
  }

  /// <summary>Идентификатор типа объекта "Технологическая сборочная единица"</summary>
  public static int ObjType_ProcessComposition
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ProсessComposition == -1)
        AvsIDCache.objType_ProсessComposition = AvsIDCache.GetObjectType("cad00650-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_ProсessComposition;
    }
  }

  /// <summary>Идентификатор типа объекта "Раздел спецификации"</summary>
  public static int ObjType_SpecificationSection
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_SpecificationSection == -1)
        AvsIDCache.objType_SpecificationSection = AvsIDCache.GetObjectType("cad00254-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_SpecificationSection;
    }
  }

  /// <summary>Идентификатор типа объекта "Часть спецификации"</summary>
  public static int ObjType_SpecificationChapter
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_SpecificationChapter == -1)
        AvsIDCache.objType_SpecificationChapter = AvsIDCache.GetObjectType(AvsIDCache.ObjTypeChapterGuid);
      return AvsIDCache.objType_SpecificationChapter;
    }
  }

  /// <summary>Идентификатор типа объекта "Документ"</summary>
  public static int ObjType_Document
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Document == -1)
        AvsIDCache.objType_Document = AvsIDCache.GetObjectType("cad00070-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Document;
    }
  }

  /// <summary>Идентификатор типа объекта "Деталь"</summary>
  public static int ObjType_Detail
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Detail == -1)
        AvsIDCache.objType_Detail = AvsIDCache.GetObjectType("cad00250-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Detail;
    }
  }

  /// <summary>Идентификатор типа объекта "Стандартное изделие"</summary>
  public static int ObjType_StandartProduct
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_StandartProduct == -1)
        AvsIDCache.objType_StandartProduct = AvsIDCache.GetObjectType("cad00252-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_StandartProduct;
    }
  }

  /// <summary>Идентификатор типа объекта "Прочие изделие"</summary>
  public static int ObjType_OtherProduct
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_OtherProduct == -1)
        AvsIDCache.objType_OtherProduct = AvsIDCache.GetObjectType("cad0038d-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_OtherProduct;
    }
  }

  /// <summary>Идентификатор типа объекта "Материал"</summary>
  public static int ObjType_Materials
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Materials == -1)
        AvsIDCache.objType_Materials = AvsIDCache.GetObjectType("cad00170-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Materials;
    }
  }

  /// <summary>Идентификатор типа объекта "Конструкторские документы"</summary>
  public static int ObjType_ConstructorDocument
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ConstructorDocument == -1)
        AvsIDCache.objType_ConstructorDocument = AvsIDCache.GetObjectType("cad0057f-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_ConstructorDocument;
    }
  }

  /// <summary>Идентификатор типа объекта "Ведомость эксплуатационных документов"</summary>
  public static int ObjType_OperationDocumentsSheet
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_OperationDocumentsSheet == -1)
        AvsIDCache.objType_OperationDocumentsSheet = AvsIDCache.GetObjectType("cad00264-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_OperationDocumentsSheet;
    }
  }

  /// <summary>Идентификатор типа объекта "Ведомость документов для капитального ремонта"</summary>
  public static int ObjType_GeneralRepairDocumentsSheet
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_GeneralRepairDocumentsSheet == -1)
        AvsIDCache.objType_GeneralRepairDocumentsSheet = AvsIDCache.GetObjectType("cad00265-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_GeneralRepairDocumentsSheet;
    }
  }

  /// <summary>Идентификатор типа объекта "Ведомость документов для среднего ремонта"</summary>
  public static int ObjType_MediumRepairDocumentsSheet
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_MediumRepairDocumentsSheet == -1)
        AvsIDCache.objType_MediumRepairDocumentsSheet = AvsIDCache.GetObjectType("cad0082d-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_MediumRepairDocumentsSheet;
    }
  }

  /// <summary>Идентификатор типа объекта "Чертеж детали"</summary>
  public static int ObjType_DetailDrawing
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_DetailDrawing == -1)
        AvsIDCache.objType_DetailDrawing = AvsIDCache.GetObjectType(AvsIDCache.ObjType_DetailDrawingGuid);
      return AvsIDCache.objType_DetailDrawing;
    }
  }

  public static int[] BaseProductForSpecificationTypes
  {
    get
    {
      if (AvsIDCache.baseProductForSpecificationTypes == null)
        AvsIDCache.baseProductForSpecificationTypes = new int[2]
        {
          AvsIDCache.ObjType_Product,
          AvsIDCache.ObjType_Orders
        };
      return AvsIDCache.baseProductForSpecificationTypes;
    }
  }

  /// <summary>Данный тип относится к спецификациям</summary>
  /// <param name="objType">Тип объекта БД</param>
  /// <returns></returns>
  public static bool IsSpecification(int objType)
  {
    if (MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_Specification))
      return true;
    if (MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_Document))
    {
      List<AVSDocumentTypeSettings> typesForDbObjectType = AVSDocumentsSettings.Instance.GetAVSDocumentTypesForDBObjectType(objType);
      for (int index = 0; index < typesForDbObjectType.Count; ++index)
      {
        if (AVSDocumentsSettings.IsSpecificationDocType(typesForDbObjectType[index].AVSDocType))
          return true;
      }
    }
    return false;
  }

  /// <summary>Данный тип относится к перечням элементов</summary>
  /// <param name="objType">Тип объекта БД</param>
  /// <returns></returns>
  public static bool IsElementList(int objType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_Document) && AVSDocumentsSettings.Instance.IsAVSElementList(objType);
  }

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="documentType">Идентификатор типа объекта БД</param>
  public static List<Guid> GetObjectTypeGuidsForAVSDocumentType(Guid avsDocumentTypeGuid)
  {
    return AVSDocumentsSettings.GetObjectTypeGuidsForAVSDocumentType(avsDocumentTypeGuid);
  }

  /// <summary>Любой документ кроме спецификации</summary>
  /// <param name="objType">Тип объекта БД</param>
  /// <returns></returns>
  public static bool IsNotSpecificationDoc(int objType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_Document) && !AvsIDCache.IsSpecification(objType);
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 0</summary>
  public static int ObjType_ElementList0
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList0 == -1)
        AvsIDCache.objType_ElementList0 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList0Guid);
      return AvsIDCache.objType_ElementList0;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 1</summary>
  public static int ObjType_ElementList1
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList1 == -1)
        AvsIDCache.objType_ElementList1 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList1Guid);
      return AvsIDCache.objType_ElementList1;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 2</summary>
  public static int ObjType_ElementList2
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList2 == -1)
        AvsIDCache.objType_ElementList2 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList2Guid);
      return AvsIDCache.objType_ElementList2;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 3</summary>
  public static int ObjType_ElementList3
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList3 == -1)
        AvsIDCache.objType_ElementList3 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList3Guid);
      return AvsIDCache.objType_ElementList3;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 4</summary>
  public static int ObjType_ElementList4
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList4 == -1)
        AvsIDCache.objType_ElementList4 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList4Guid);
      return AvsIDCache.objType_ElementList4;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 5</summary>
  public static int ObjType_ElementList5
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList5 == -1)
        AvsIDCache.objType_ElementList5 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList5Guid);
      return AvsIDCache.objType_ElementList5;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 6</summary>
  public static int ObjType_ElementList6
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList6 == -1)
        AvsIDCache.objType_ElementList6 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList6Guid);
      return AvsIDCache.objType_ElementList6;
    }
  }

  /// <summary>Идентификатор типа объекта Перечень элементов 7</summary>
  public static int ObjType_ElementList7
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_ElementList7 == -1)
        AvsIDCache.objType_ElementList7 = AvsIDCache.GetObjectType(AvsIDCache.ObjType_ElementList7Guid);
      return AvsIDCache.objType_ElementList7;
    }
  }

  /// <summary>Идентификатор типа объекта "Беcчертёжная деталь"</summary>
  public static int ObjType_DetailWithoutDrawing
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_DetailWithoutDrawing == -1)
        AvsIDCache.objType_DetailWithoutDrawing = AvsIDCache.GetObjectType("cad00861-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_DetailWithoutDrawing;
    }
  }

  /// <summary>Идентификатор типа объекта "Сборочный чертеж"</summary>
  public static int ObjType_AssemblyDrawing
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_AssemblyDrawing == -1)
        AvsIDCache.objType_AssemblyDrawing = AvsIDCache.GetObjectType(AvsIDCache.ObjType_AssemblyDrawingGuid);
      return AvsIDCache.objType_AssemblyDrawing;
    }
  }

  /// <summary>Идентификатор типа объекта "Заказ"</summary>
  public static int ObjType_Orders
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_Orders == -1 || AvsIDCache.objType_Orders == 0)
        AvsIDCache.objType_Orders = AvsIDCache.GetObjectType("cad00580-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_Orders;
    }
  }

  /// <summary>Идентификатор типа объекта "Электронные модели деталей"</summary>
  public static int ObjType_DetailModels
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_DetailModels == -1)
        AvsIDCache.objType_DetailModels = AvsIDCache.GetObjectType(AvsIDCache.ObjType_DetailModelsGuid);
      return AvsIDCache.objType_DetailModels;
    }
  }

  /// <summary>Идентификатор типа объекта "Электронные модели сборок"</summary>
  public static int ObjType_AssemblyModels
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_AssemblyModels == -1)
        AvsIDCache.objType_AssemblyModels = AvsIDCache.GetObjectType(AvsIDCache.ObjType_AssemblyModelsGuid);
      return AvsIDCache.objType_AssemblyModels;
    }
  }

  /// <summary>Идентификатор типа объекта "Разделы конструкторских ведомостей"</summary>
  public static int ObjType_VedomostiSection
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.objType_VedomostiSection == -1)
        AvsIDCache.objType_VedomostiSection = AvsIDCache.GetObjectType("cad002a7-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.objType_VedomostiSection;
    }
  }

  /// <summary>Идентификатор типа связи "Состав изделий" (ранее "Проектная связь")</summary>
  public static int Relation_Project
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_Project == -1)
        AvsIDCache.relation_Project = MetaDataHelper.GetRelationTypeID(new Guid("cad00023-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.relation_Project;
    }
  }

  /// <summary>Идентификатор типа связи "Изделие-заготовка"</summary>
  public static int Relation_Zagotovka
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_Zagotovka == -1)
        AvsIDCache.relation_Zagotovka = MetaDataHelper.GetRelationTypeID(AvsIDCache.RelType_ZagotovkaGuid);
      return AvsIDCache.relation_Zagotovka;
    }
  }

  /// <summary>Идентификатор типа связи "Комплект, поставляемый отдельно"</summary>
  public static int Relation_AddComplect
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_AddComplect == -1)
        AvsIDCache.relation_AddComplect = MetaDataHelper.GetRelationTypeID(new Guid("cadd99d9-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.relation_AddComplect;
    }
  }

  /// <summary>Идентификатор типа связи "Подборный компонент"</summary>
  public static int Relation_Podbor
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_Podbor == -1)
        AvsIDCache.relation_Podbor = MetaDataHelper.GetRelationTypeID(new Guid("cadd9740-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.relation_Podbor;
    }
  }

  /// <summary>Идентификатор типа связи "Ссылка на объект"</summary>
  public static int Relation_Reference
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_Reference == -1)
        AvsIDCache.relation_Reference = MetaDataHelper.GetRelationTypeID(new Guid("cadd99d7-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.relation_Reference;
    }
  }

  /// <summary>Идентификатор типа связи "Документ"</summary>
  public static int Relation_Document
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.relation_Document == -1)
        AvsIDCache.relation_Document = MetaDataHelper.GetRelationTypeID(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.relation_Document;
    }
  }

  /// <summary>Идентификатор атрибута "Количество"</summary>
  public static int Attr_Count
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Count == -1)
        AvsIDCache.attr_Count = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_Count_Guid);
      return AvsIDCache.attr_Count;
    }
  }

  /// <summary>Идентификатор атрибута "Количество на регулировку"</summary>
  public static int Attr_CountForAdjustment
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_CountForAdjustment == -1)
        AvsIDCache.attr_CountForAdjustment = MetaDataHelper.GetAttributeTypeID("cad007a6-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.attr_CountForAdjustment;
    }
  }

  /// <summary>Идентификатор атрибута "Масса"</summary>
  public static int Attr_Weight
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Weight == -1)
        AvsIDCache.attr_Weight = MetaDataHelper.GetAttributeTypeID(new Guid("cad00275-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Weight;
    }
  }

  /// <summary>Идентификатор атрибута "Удельная масса"</summary>
  public static int Attr_UnitWeight
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_UnitWeight == -1)
        AvsIDCache.attr_UnitWeight = MetaDataHelper.GetAttributeTypeID(new Guid("cad00276-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_UnitWeight;
    }
  }

  /// <summary>Идентификатор атрибута "Размеры"</summary>
  public static int Attr_Size
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Size == -1)
        AvsIDCache.attr_Size = MetaDataHelper.GetAttributeTypeID(new Guid("cad00277-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Size;
    }
  }

  /// <summary>Идентификатор атрибута "Материал"</summary>
  public static int Attr_Material
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Material == -1)
        AvsIDCache.attr_Material = MetaDataHelper.GetAttributeTypeID(new Guid("cad0038c-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Material;
    }
  }

  /// <summary>Идентификатор атрибута "Обозначение"</summary>
  public static int Attr_Designation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Designation == -1)
        AvsIDCache.attr_Designation = MetaDataHelper.GetAttributeTypeID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Designation;
    }
  }

  /// <summary>Идентификатор атрибута "ЕСПД включено"</summary>
  public static int Attr_IsOnEspd
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_IsOnESPD == -1)
        AvsIDCache.attr_IsOnESPD = MetaDataHelper.GetAttributeTypeID(new Guid("5a9eb986-9311-43a9-8a99-09dd1dc7c68b"));
      return AvsIDCache.attr_IsOnESPD;
    }
  }

  /// <summary>Идентификатор атрибута "Наименование"</summary>
  public static int Attr_Name
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Name == -1)
        AvsIDCache.attr_Name = MetaDataHelper.GetAttributeTypeID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Name;
    }
  }

  public static AvsRowAttributeInfo StdField_Name
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.stdField_Name == null)
        AvsIDCache.stdField_Name = new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Name, AvsIDCache.DocAttr_Name, ColumnContents.Text);
      return AvsIDCache.stdField_Name;
    }
  }

  /// <summary>Идентификатор атрибута "Description"</summary>
  public static int Attr_Description
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Description == -1)
        AvsIDCache.attr_Description = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrDescriptionGuid);
      return AvsIDCache.attr_Description;
    }
  }

  /// <summary>Идентификатор атрибута "Наименование (exp)"</summary>
  public static int Attr_NameExp
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NameExp == -1)
        AvsIDCache.attr_NameExp = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNameExpGuid);
      return AvsIDCache.attr_NameExp;
    }
  }

  /// <summary>Идентификатор атрибута "Наименование в документах AVS"</summary>
  public static int Attr_NameForAVS
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NameForAVS == -1)
        AvsIDCache.attr_NameForAVS = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNameForAVS_Guid);
      return AvsIDCache.attr_NameForAVS;
    }
  }

  /// <summary>Идентификатор атрибута "Примечание"</summary>
  public static int Attr_Note => AvsIDCache.StdField_Note.AttributeId;

  /// <summary>Идентификатор атрибута "Примечание ПЭ"</summary>
  public static int Attr_NotePE => AvsIDCache.StdField_NotePE.AttributeId;

  /// <summary>Идентификатор атрибута "Настройки графы Примечание"</summary>
  public static int Attr_NoteFieldSettings
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NoteFieldSettings == -1)
        AvsIDCache.attr_NoteFieldSettings = MetaDataHelper.GetAttributeTypeID(new Guid("cad00294-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_NoteFieldSettings;
    }
  }

  /// <summary>Идентификатор атрибута "Допустимые типы"</summary>
  public static int Attr_PossibleTypes
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PossibleTypes == -1)
        AvsIDCache.attr_PossibleTypes = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrPossibleTypesGuid);
      return AvsIDCache.attr_PossibleTypes;
    }
  }

  /// <summary>Идентификатор атрибута "Ссылка на каталог ImBase"</summary>
  public static int Attr_RefToImBaseDirectory
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_RefToImBaseDirectory == -1)
        AvsIDCache.attr_RefToImBaseDirectory = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrRefToImBaseDirectory);
      return AvsIDCache.attr_RefToImBaseDirectory;
    }
  }

  /// <summary>Идентификатор атрибута "Раздел спецификации"</summary>
  public static int Attr_SpecificationSection
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SpecificationSection != -1)
        return AvsIDCache.attr_SpecificationSection;
      AvsIDCache.attr_SpecificationSection = MetaDataHelper.GetAttributeTypeID(new Guid("cad00266-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SpecificationSection;
    }
  }

  /// <summary>Идентификатор атрибута "Часть спецификации"</summary>
  public static int Attr_SpecificationСhapter
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SpecificationСhapter == -1)
        AvsIDCache.attr_SpecificationСhapter = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrSpecificationChapterGuid);
      return AvsIDCache.attr_SpecificationСhapter;
    }
  }

  /// <summary>Идентификатор атрибута "Сортировка"</summary>
  public static int Attr_SortIndex
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SortIndex != -1)
        return AvsIDCache.attr_SortIndex;
      AvsIDCache.attr_SortIndex = MetaDataHelper.GetAttributeTypeID(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SortIndex;
    }
  }

  /// <summary>Идентификатор атрибута "Допустимые разделы"</summary>
  public static int Attr_AllowableSections
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_AllowableSections == -1)
        AvsIDCache.attr_AllowableSections = MetaDataHelper.GetAttributeTypeID(new Guid("cad0026a-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_AllowableSections;
    }
  }

  /// <summary>Идентификатор атрибута "Схема сортировки"</summary>
  public static int Attr_CommonPosition
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_CommonPosition == -1)
        AvsIDCache.attr_CommonPosition = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_CommonPositionGuid);
      return AvsIDCache.attr_CommonPosition;
    }
  }

  /// <summary>Идентификатор атрибута "Схема сортировки"</summary>
  public static int Attr_SortSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SortSchema == -1)
        AvsIDCache.attr_SortSchema = MetaDataHelper.GetAttributeTypeID(new Guid("cad0026c-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SortSchema;
    }
  }

  /// <summary>Идентификатор атрибута "Схема нумерации позиций"</summary>
  public static int Attr_NumberingSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NumberingSchema == -1)
        AvsIDCache.attr_NumberingSchema = MetaDataHelper.GetAttributeTypeID(new Guid("cad0026e-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_NumberingSchema;
    }
  }

  /// <summary>Идентификатор атрибута "Настройки граф документа"</summary>
  public static int Attr_OutputMappingSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OutputMappingSchema == -1)
        AvsIDCache.attr_OutputMappingSchema = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9aa0-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OutputMappingSchema;
    }
  }

  /// <summary> Настройка автозамены в заголовке группы </summary>
  public static int Attr_DynamicHeaderKeywordReplacementSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DynamicHeaderKeywordReplacementSchema == -1)
        AvsIDCache.attr_DynamicHeaderKeywordReplacementSchema = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9ac0-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DynamicHeaderKeywordReplacementSchema;
    }
  }

  /// <summary>Идентификатор атрибута "Схема обрезки обозначения"</summary>
  public static int Attr_DesignationTrimSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DesignationTrimSchema == -1)
        AvsIDCache.attr_DesignationTrimSchema = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrDesignationTrimGuid);
      return AvsIDCache.attr_DesignationTrimSchema;
    }
  }

  /// <summary>Идентификатор атрибута "Ключевые слова для материалов"</summary>
  public static int Attr_MaterialKeyWordsSchema
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_MaterialKeyWordsSchema == -1)
        AvsIDCache.attr_MaterialKeyWordsSchema = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrMaterialKeyWordsGuid);
      return AvsIDCache.attr_MaterialKeyWordsSchema;
    }
  }

  /// <summary>Идентификатор атрибута "Настройки шаблонов конструкторских документов"</summary>
  public static int Attr_AVSTemplateSettings
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_AVSTemplateSettings == -1)
        AvsIDCache.attr_AVSTemplateSettings = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrAVSTemplateSettingsGuid);
      return AvsIDCache.attr_AVSTemplateSettings;
    }
  }

  /// <summary>Идентификатор атрибута "Настройки заголовков исполнений в переменных данных"</summary>
  public static int Attr_VariableDataProductCaption
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_VariableDataProductCaption == -1)
        AvsIDCache.attr_VariableDataProductCaption = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrVariableDataProductCaptionGuid);
      return AvsIDCache.attr_VariableDataProductCaption;
    }
  }

  /// <summary>Идентификатор атрибута "Изделие на которое выпущен документ"</summary>
  public static int Attr_OwnerLink
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OwnerLink == -1)
        AvsIDCache.attr_OwnerLink = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrOwnerLink);
      return AvsIDCache.attr_OwnerLink;
    }
  }

  /// <summary>Идентификатор атрибута "Настройки конструкторского документа"</summary>
  public static int Attr_ConstructorDocumentProperties
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ConstructorDocumentProperties == -1)
        AvsIDCache.attr_ConstructorDocumentProperties = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrConstructorDocumentPropertiesGuid);
      return AvsIDCache.attr_ConstructorDocumentProperties;
    }
  }

  /// <summary>Идентификатор атрибута "Позиция в спецификации"</summary>
  public static int Attr_Position
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Position != -1)
        return AvsIDCache.attr_Position;
      AvsIDCache.attr_Position = MetaDataHelper.GetAttributeTypeID(new Guid("cad00270-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Position;
    }
  }

  /// <summary>Идентификатор атрибута "Подбор для позиционного обозначения"</summary>
  public static int Attr_PodborForPosDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PodborForPosDesignation != -1)
        return AvsIDCache.attr_PodborForPosDesignation;
      AvsIDCache.attr_PodborForPosDesignation = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributePodborForPosDesignation);
      return AvsIDCache.attr_PodborForPosDesignation;
    }
  }

  /// <summary>Идентификатор атрибута "Позиционное обозначение"</summary>
  public static int Attr_PosDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PosDesignation != -1)
        return AvsIDCache.attr_PosDesignation;
      AvsIDCache.attr_PosDesignation = MetaDataHelper.GetAttributeTypeID(new Guid("cad01478-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_PosDesignation;
    }
  }

  /// <summary>Идентификатор атрибута "Условное наименование"</summary>
  public static int Attr_ProductConventionalName
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ProductConventionalName != -1)
        return AvsIDCache.attr_ProductConventionalName;
      AvsIDCache.attr_ProductConventionalName = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrProductConventionalName_Guid);
      return AvsIDCache.attr_ProductConventionalName;
    }
  }

  /// <summary>Идентификатор атрибута "Сортировка AVS"</summary>
  public static int Attr_SortAVS
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SortAVS != -1)
        return AvsIDCache.attr_SortAVS;
      AvsIDCache.attr_SortAVS = MetaDataHelper.GetAttributeTypeID(new Guid("cad00272-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SortAVS;
    }
  }

  /// <summary>Идентификатор атрибута "Схема пропуска строк"</summary>
  public static int Attr_SkipLines
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SkipLines == -1)
        AvsIDCache.attr_SkipLines = MetaDataHelper.GetAttributeTypeID(new Guid("cad00273-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SkipLines;
    }
  }

  /// <summary>Идентификатор атрибута "Настройки группировки записей"</summary>
  public static int Attr_DynamicGroupHeaderSettings
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DynamicGroupHeaderSettings == -1)
        AvsIDCache.attr_DynamicGroupHeaderSettings = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrDynamicGroupHeaderSettings);
      return AvsIDCache.attr_DynamicGroupHeaderSettings;
    }
  }

  /// <summary>Идентификатор атрибута "Номер группы заменителей"</summary>
  public static int Attr_DopZamenGroupNum
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DopZamenGroupNum != -1)
        return AvsIDCache.attr_DopZamenGroupNum;
      AvsIDCache.attr_DopZamenGroupNum = MetaDataHelper.GetAttributeTypeID(new Guid("cad001c0-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DopZamenGroupNum;
    }
  }

  /// <summary>Идентификатор атрибута "Номер заменителя в группе"</summary>
  public static int Attr_DopZamenNumInGroup
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DopZamenNumInGroup != -1)
        return AvsIDCache.attr_DopZamenNumInGroup;
      AvsIDCache.attr_DopZamenNumInGroup = MetaDataHelper.GetAttributeTypeID(new Guid("cad001c1-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DopZamenNumInGroup;
    }
  }

  /// <summary>Идентификатор атрибута "Имя группы заменителей"</summary>
  public static int Attr_DopZamenGroupName
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DopZamenGroupName != -1)
        return AvsIDCache.attr_DopZamenGroupName;
      AvsIDCache.attr_DopZamenGroupName = MetaDataHelper.GetAttributeTypeID(new Guid("cad00817-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DopZamenGroupName;
    }
  }

  /// <summary>Идентификатор атрибута "Имя заменителя"</summary>
  public static int Attr_DopZamenSubstituteName
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DopZamenSubstituteName != -1)
        return AvsIDCache.attr_DopZamenSubstituteName;
      AvsIDCache.attr_DopZamenSubstituteName = MetaDataHelper.GetAttributeTypeID(new Guid("cad00818-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DopZamenSubstituteName;
    }
  }

  /// <summary>Идентификатор атрибута "Конструкторский основной вариант"</summary>
  public static int Attr_DesignerActualVariant
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DesignerActualVariant != -1)
        return AvsIDCache.attr_DesignerActualVariant;
      AvsIDCache.attr_DesignerActualVariant = MetaDataHelper.GetAttributeTypeID(new Guid("cad00654-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DesignerActualVariant;
    }
  }

  /// <summary>Идентификатор атрибута "Расшифровка допустимых замен"</summary>
  public static int Attr_DopZamenText => AvsIDCache.DopZamenTextAttrInfo.AttributeId;

  /// <summary>Идентификатор атрибута "Номер раздела спецификации"</summary>
  public static int Attr_SectionNum
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SectionNum != -1)
        return AvsIDCache.attr_SectionNum;
      AvsIDCache.attr_SectionNum = MetaDataHelper.GetAttributeTypeID(new Guid("cad00279-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_SectionNum;
    }
  }

  /// <summary>Идентификатор атрибута "Номер части спецификации"</summary>
  public static int Attr_PartNum
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PartNum != -1)
        return AvsIDCache.attr_PartNum;
      AvsIDCache.attr_PartNum = MetaDataHelper.GetAttributeTypeID(new Guid("cad00286-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_PartNum;
    }
  }

  /// <summary>Идентификатор атрибута "Часть спецификации"</summary>
  public static int Attr_PartName
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PartName != -1)
        return AvsIDCache.attr_PartName;
      AvsIDCache.attr_PartName = MetaDataHelper.GetAttributeTypeID(new Guid("cad0027e-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_PartName;
    }
  }

  /// <summary>Идентификатор атрибута "Раздел СП"</summary>
  public static int Attr_InsertToSection
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_InsertToSection != -1)
        return AvsIDCache.attr_InsertToSection;
      AvsIDCache.attr_InsertToSection = MetaDataHelper.GetAttributeTypeID(new Guid("cad00210-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_InsertToSection;
    }
  }

  /// <summary>Идентификатор атрибута "Идентификатор группового изделия"</summary>
  public static int Attr_ArticleGroupID
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ArticleGroupID != -1)
        return AvsIDCache.attr_ArticleGroupID;
      AvsIDCache.attr_ArticleGroupID = MetaDataHelper.GetAttributeTypeID(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ArticleGroupID;
    }
  }

  /// <summary>Идентификатор атрибута "Подбор"</summary>
  public static int Attr_Podbor
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Podbor != -1)
        return AvsIDCache.attr_Podbor;
      AvsIDCache.attr_Podbor = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_Podbor_Guid);
      return AvsIDCache.attr_Podbor;
    }
  }

  private static void CollectVirtualAttributes()
  {
    Type thisType = typeof (AvsIDCache);
    AvsIDCache.VirtualAttributes = ((IEnumerable<FieldInfo>) thisType.GetFields(BindingFlags.Static | BindingFlags.Public)).Where<FieldInfo>((System.Func<FieldInfo, bool>) (f => f.CustomAttributes.Any<CustomAttributeData>((System.Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (VirtualAttributeAttribute))))).Select<FieldInfo, AvsRowAttributeInfo>((System.Func<FieldInfo, AvsRowAttributeInfo>) (f => (AvsRowAttributeInfo) f.GetValue((object) thisType))).ToArray<AvsRowAttributeInfo>();
  }

  /// <summary>Получить виртуальный атрибут по его Guid</summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <returns></returns>
  public static AvsRowAttributeInfo GetVirtualAttributInfo(Guid attrGuid)
  {
    return ((IEnumerable<AvsRowAttributeInfo>) AvsIDCache.VirtualAttributes).FirstOrDefault<AvsRowAttributeInfo>((System.Func<AvsRowAttributeInfo, bool>) (va => va.AttributeGuid == attrGuid));
  }

  /// <summary>Идентификатор атрибута "Код исполнения"</summary>
  public static int Attr_ProductCode
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ProductCode == -1)
        AvsIDCache.attr_ProductCode = MetaDataHelper.GetAttributeTypeID(new Guid("cad001fa-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ProductCode;
    }
  }

  /// <summary>Идентификатор атрибута "Формат"</summary>
  public static int Attr_Format => AvsIDCache.StdField_Format.AttributeId;

  /// <summary>Идентификатор атрибута "Зона"</summary>
  public static int Attr_Zone => AvsIDCache.StdField_Zone.AttributeId;

  /// <summary>Идентификатор атрибута "Форма спецификации"</summary>
  public static int Attr_SpecificationForm
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SpecificationForm == -1)
        AvsIDCache.attr_SpecificationForm = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrSpecificationFormGuid);
      return AvsIDCache.attr_SpecificationForm;
    }
  }

  /// <summary>
  /// Идентификатор атрибута "Список дополнительных объектов"
  /// </summary>
  public static int Attr_AuxLinks
  {
    get => MetaDataHelper.GetAttributeTypeID("cadd93b7-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>Идентификатор атрибута "Файл"</summary>
  public static int Attr_File
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_File == -1)
        AvsIDCache.attr_File = MetaDataHelper.GetAttributeTypeID(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_File;
    }
  }

  /// <summary>Идентификатор атрибута "Файл документа", Хранит файлы документа интермех, если в атрибуте "Файл" хранится сканированный файл</summary>
  public static int Attr_DocumentFile
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DocumentFile == -1)
        AvsIDCache.attr_DocumentFile = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9620-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_DocumentFile;
    }
  }

  /// <summary>Идентификатор атрибута "Сканированный документ"</summary>
  public static int Attr_ScanDocument
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ScanDocument == -1)
        AvsIDCache.attr_ScanDocument = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9644-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ScanDocument;
    }
  }

  /// <summary>Идентификатор атрибута "Разработал"</summary>
  public static int Attr_Author
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Author == -1)
        AvsIDCache.attr_Author = MetaDataHelper.GetAttributeTypeID(new Guid("cad00280-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Author;
    }
  }

  /// <summary>Идентификатор атрибута "Подразделение"</summary>
  public static int Attr_Subdivision
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Subdivision == -1)
        AvsIDCache.attr_Subdivision = MetaDataHelper.GetAttributeTypeID(new Guid("cad00281-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Subdivision;
    }
  }

  /// <summary>Идентификатор атрибута "Литера"</summary>
  public static int Attr_Litera
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Litera == -1)
        AvsIDCache.attr_Litera = MetaDataHelper.GetAttributeTypeID(new Guid("cad0038b-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_Litera;
    }
  }

  /// <summary>Идентификатор атрибута "Проверил"</summary>
  public static int Attr_CheckedBy
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_CheckedBy == -1)
        AvsIDCache.attr_CheckedBy = MetaDataHelper.GetAttributeTypeID(new Guid("cad00282-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_CheckedBy;
    }
  }

  /// <summary>Идентификатор атрибута "Необходимо обновить документ"</summary>
  public static int Attr_NeedUpdateDoc
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NeedUpdateDoc == -1)
        AvsIDCache.attr_NeedUpdateDoc = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNeedUpdateDoc_Guid);
      return AvsIDCache.attr_NeedUpdateDoc;
    }
  }

  /// <summary>Идентификатор атрибута "Нормоконтролёр"</summary>
  public static int Attr_NormoControlledBy
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NormoControlledBy == -1)
        AvsIDCache.attr_NormoControlledBy = MetaDataHelper.GetAttributeTypeID(new Guid("cad00283-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_NormoControlledBy;
    }
  }

  /// <summary>Идентификатор атрибута "Утвердил"</summary>
  public static int Attr_ConfirmBy
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ConfirmBy == -1)
        AvsIDCache.attr_ConfirmBy = MetaDataHelper.GetAttributeTypeID(new Guid("cad00284-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ConfirmBy;
    }
  }

  /// <summary>Идентификатор атрибута "Уникальный идентификатора входимости для CAD системы"</summary>
  public static int Attr_CADInteranceIdentify
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_CADInteranceIdentify == -1)
        AvsIDCache.attr_CADInteranceIdentify = MetaDataHelper.GetAttributeTypeID(new Guid("cad0027b-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_CADInteranceIdentify;
    }
  }

  /// <summary>Идентификатор атрибута "Создано по CAD-модели"</summary>
  public static int Attr_BasedOnCADModel
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_BasedOnCADModel == -1)
        AvsIDCache.attr_BasedOnCADModel = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_BasedOnCADModelGuid);
      return AvsIDCache.attr_BasedOnCADModel;
    }
  }

  /// <summary>Идентификатор атрибута "Глобальный идентификатор входимости"</summary>
  public static int Attr_OccurenceKey
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OccurenceKey == -1)
        AvsIDCache.attr_OccurenceKey = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_OccurenceKeyGuid);
      return AvsIDCache.attr_OccurenceKey;
    }
  }

  /// <summary>Идентификатор атрибута "Кодовая позиция"</summary>
  public static int Attr_CodePosition
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_CodePosition == -1)
        AvsIDCache.attr_CodePosition = MetaDataHelper.GetAttributeTypeID(new Guid("cad0027c-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_CodePosition;
    }
  }

  /// <summary>Идентификатор атрибута "Код ОКП"</summary>
  public static int Attr_OKPCode
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OKPCode == -1)
        AvsIDCache.attr_OKPCode = MetaDataHelper.GetAttributeTypeID(new Guid("cad0038a-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OKPCode;
    }
  }

  /// <summary>Идентификатор атрибута "Идентификатор версии в составе"</summary>
  public static int Attr_VersionInRelation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_VersionInRelation == -1)
        AvsIDCache.attr_VersionInRelation = MetaDataHelper.GetAttributeTypeID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_VersionInRelation;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_InMainDocComplect
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_InMainDocComplect == -1)
        AvsIDCache.attr_InMainDocComplect = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9bdc-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_InMainDocComplect;
    }
  }

  /// <summary>Идентификатор атрибута "Ссылка на объект-прототип"</summary>
  public static int Attr_ObjectPrototype
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ObjectPrototype == -1)
        AvsIDCache.attr_ObjectPrototype = MetaDataHelper.GetAttributeTypeID(new Guid("cadd9668-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ObjectPrototype;
    }
  }

  /// <summary>Идентификатор в Search</summary>
  public static AvsRowAttributeInfo Attr_SearchId
  {
    get
    {
      if (AvsIDCache._attr_SearchId == null)
        AvsIDCache._attr_SearchId = AvsRowAttributeInfo.CreateByGuid(FieldSource.Object, new Guid("cad0132b-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache._attr_SearchId;
    }
  }

  /// <summary>Идентификатор атрибута "Класс"</summary>
  public static int Attr_Class
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Class == -1)
        AvsIDCache.attr_Class = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrClassGuid);
      return AvsIDCache.attr_Class;
    }
  }

  /// <summary>Идентификатор атрибута "Класс"</summary>
  public static int Attr_OldAVSINI
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OldAVSINI == -1)
        AvsIDCache.attr_OldAVSINI = MetaDataHelper.GetAttributeTypeID("cadd9417-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.attr_OldAVSINI;
    }
  }

  /// <summary>Идентификатор атрибута "ГОСТ"</summary>
  public static int Attr_Gost
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Gost == -1)
        AvsIDCache.attr_Gost = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrGostGuid);
      return AvsIDCache.attr_Gost;
    }
  }

  /// <summary>Идентификатор атрибута "Тип НТД"</summary>
  public static int Attr_TypeNTD
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_TypeNTD == -1)
        AvsIDCache.attr_TypeNTD = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrTypeNTDGuid);
      return AvsIDCache.attr_TypeNTD;
    }
  }

  /// <summary>Идентификатор атрибута "Вид НТД"</summary>
  public static int Attr_VidNTD
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_VidNTD == -1)
        AvsIDCache.attr_VidNTD = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrVidNTDGuid);
      return AvsIDCache.attr_VidNTD;
    }
  }

  /// <summary>Идентификатор атрибута "Листов"</summary>
  public static int Attr_Listov
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Listov == -1)
        AvsIDCache.attr_Listov = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrListovGuid);
      return AvsIDCache.attr_Listov;
    }
  }

  /// <summary>Идентификатор атрибута "Держатель подлинника"</summary>
  public static int Attr_DerzPodl
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_DerzPodl == -1)
        AvsIDCache.attr_DerzPodl = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrDerzPodlGuid);
      return AvsIDCache.attr_DerzPodl;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_KolExemplyarov
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_KolExemplyarov == -1)
        AvsIDCache.attr_KolExemplyarov = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrKolExemplyarovGuid);
      return AvsIDCache.attr_KolExemplyarov;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_NomerExemplara
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NomerExemplara == -1)
        AvsIDCache.attr_NomerExemplara = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNomerExemplaraGuid);
      return AvsIDCache.attr_NomerExemplara;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_Mesto
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Mesto == -1)
        AvsIDCache.attr_Mesto = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrMestoGuid);
      return AvsIDCache.attr_Mesto;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_KodProduction
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_KodProduction == -1)
        AvsIDCache.attr_KodProduction = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrKodProductionGuid);
      return AvsIDCache.attr_KodProduction;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_KolInComplect
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_KolInComplect == -1)
        AvsIDCache.attr_KolInComplect = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrKolInComplectGuid);
      return AvsIDCache.attr_KolInComplect;
    }
  }

  /// <summary>Идентификатор атрибута ""</summary>
  public static int Attr_Use
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Use == -1)
        AvsIDCache.attr_Use = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrUseGuid);
      return AvsIDCache.attr_Use;
    }
  }

  public static int Attr_Clamp
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Clamp == -1)
        AvsIDCache.attr_Clamp = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrClamp);
      return AvsIDCache.attr_Clamp;
    }
  }

  public static int Attr_Package
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Package == -1)
        AvsIDCache.attr_Package = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrPackage);
      return AvsIDCache.attr_Package;
    }
  }

  public static int Attr_Connection
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Connection == -1)
        AvsIDCache.attr_Connection = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrConnection);
      return AvsIDCache.attr_Connection;
    }
  }

  public static int Attr_WireLength
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_WireLength == -1)
        AvsIDCache.attr_WireLength = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrWireLength);
      return AvsIDCache.attr_WireLength;
    }
  }

  public static int Attr_WireData
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_WireData == -1)
        AvsIDCache.attr_WireData = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrWireData);
      return AvsIDCache.attr_WireData;
    }
  }

  public static int Attr_WireFrom
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_WireFrom == -1)
        AvsIDCache.attr_WireFrom = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrWireFrom);
      return AvsIDCache.attr_WireFrom;
    }
  }

  public static int Attr_WireWhere
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_WireWhere == -1)
        AvsIDCache.attr_WireWhere = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrWireWhere);
      return AvsIDCache.attr_WireWhere;
    }
  }

  public static int Attr_WireDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_WireDesignation == -1)
        AvsIDCache.attr_WireDesignation = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrWireDesignation);
      return AvsIDCache.attr_WireDesignation;
    }
  }

  public static int Attr_HarnessDesignatin
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_HarnessDesignatin == -1)
        AvsIDCache.attr_HarnessDesignatin = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrHarnessDesignatin);
      return AvsIDCache.attr_HarnessDesignatin;
    }
  }

  public static int Attr_NameDoc
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NameDoc == -1)
        AvsIDCache.attr_NameDoc = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNameDoc);
      return AvsIDCache.attr_NameDoc;
    }
  }

  public static int Attr_NameProg
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NameProg == -1)
        AvsIDCache.attr_NameProg = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrNameProg);
      return AvsIDCache.attr_NameProg;
    }
  }

  public static int Attr_TypePD
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_TypePD == -1)
        AvsIDCache.attr_TypePD = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrTypePD);
      return AvsIDCache.attr_TypePD;
    }
  }

  /// <summary> Наименования ini-файлов старого AVS </summary>
  public static int Attr_OldAvsIniFileNames
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OldAvsIniFileNames == -1)
        AvsIDCache.attr_OldAvsIniFileNames = MetaDataHelper.GetAttributeTypeID(new Guid("cad002a8-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OldAvsIniFileNames;
    }
  }

  /// <summary> атрибут "Ini файлы настроек" </summary>
  public static int Attr_OldAVSSettingsIniFiles
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OldAVSSettingsIniFiles == -1)
        AvsIDCache.attr_OldAVSSettingsIniFiles = MetaDataHelper.GetAttributeTypeID(new Guid("cad002a1-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OldAVSSettingsIniFiles;
    }
  }

  /// <summary> атрибут "Типы файлов настроек старой спецификации" </summary>
  public static int Attr_OldAVSSettingsFileTypes
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OldAVSSettingsFileTypes == -1)
        AvsIDCache.attr_OldAVSSettingsFileTypes = MetaDataHelper.GetAttributeTypeID(new Guid("cad002a3-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OldAVSSettingsFileTypes;
    }
  }

  /// <summary> атрибут "Тип файла настроек старых спецификаций по-умолчанию" </summary>
  public static int Attr_OldAVSSettingsDefaultIniFile
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_OldAVSSettingsDefaultIniFile == -1)
        AvsIDCache.attr_OldAVSSettingsDefaultIniFile = MetaDataHelper.GetAttributeTypeID(new Guid("cad002a4-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_OldAVSSettingsDefaultIniFile;
    }
  }

  /// <summary> атрибут "Код IMBASE" </summary>
  public static int Attr_ImbaseKey
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ImbaseKey == -1)
        AvsIDCache.attr_ImbaseKey = MetaDataHelper.GetAttributeTypeID(new Guid("cad00162-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ImbaseKey;
    }
  }

  /// <summary> атрибут "Дата модификации содержимого объекта" </summary>
  public static int Attr_ContentModifyDate
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ContentModifyDate == -1)
        AvsIDCache.attr_ContentModifyDate = MetaDataHelper.GetAttributeTypeID(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ContentModifyDate;
    }
  }

  /// <summary> атрибут "Перв. прим." </summary>
  public static int Attr_FirstApplicability
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_FirstApplicability == -1)
        AvsIDCache.attr_FirstApplicability = MetaDataHelper.GetAttributeTypeID(new Guid("cad00285-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_FirstApplicability;
    }
  }

  /// <summary>Идентификатор атрибута "Функциональная группа"</summary>
  public static int Attr_FuncGroup
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_FuncGroup == -1)
        AvsIDCache.attr_FuncGroup = MetaDataHelper.GetAttributeTypeID("cad00bd2-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.attr_FuncGroup;
    }
  }

  /// <summary>Идентификатор атрибута "Позиционное обозначение функциональной группы"</summary>
  public static int Attr_FGPosDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_FGPosDesignation == -1)
        AvsIDCache.attr_FGPosDesignation = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeFGPosDesignation);
      return AvsIDCache.attr_FGPosDesignation;
    }
  }

  /// <summary>Идентификатор атрибута "Обозначение функциональной группы"</summary>
  public static int Attr_FGDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_FGDesignation == -1)
        AvsIDCache.attr_FGDesignation = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeFGDesignation);
      return AvsIDCache.attr_FGDesignation;
    }
  }

  /// <summary>Идентификатор атрибута "Наименование функциональной группы"</summary>
  public static int Attr_FGName
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_FGName == -1)
        AvsIDCache.attr_FGName = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeFGName);
      return AvsIDCache.attr_FGName;
    }
  }

  /// <summary>Идентификатор атрибута "Предельные значения"</summary>
  public static int Attr_LimitValues
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_LimitValues == -1)
        AvsIDCache.attr_LimitValues = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeLimitValues);
      return AvsIDCache.attr_LimitValues;
    }
  }

  /// <summary>Идентификатор атрибута "Значение номинала"</summary>
  public static int Attr_NominalValue
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_NominalValue != -1)
        return AvsIDCache.attr_NominalValue;
      AvsIDCache.attr_NominalValue = MetaDataHelper.GetAttributeTypeID(AvsIDCache.Attr_NominalValue_Guid);
      return AvsIDCache.attr_NominalValue;
    }
  }

  /// <summary>Идентификатор атрибута "Позиционное обозначение ДС"</summary>
  public static int Attr_SymbolForPosDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_SymbolForPosDesignation == -1)
        AvsIDCache.attr_SymbolForPosDesignation = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeSymbolForPosDesignation);
      return AvsIDCache.attr_SymbolForPosDesignation;
    }
  }

  /// <summary>Идентификатор атрибута "Элемент перечня элементов", означающий что связь должна включаться в Перечень элементов</summary>
  public static int Attr_IncludeInElementList
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_IncludeInElementList == -1)
        AvsIDCache.attr_IncludeInElementList = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeIncludeInElementList);
      return AvsIDCache.attr_IncludeInElementList;
    }
  }

  /// <summary>Идентификатор атрибута "Не отображать в спецификации", означающий что связь не должна включаться в Спецификацию</summary>
  public static int Attr_HideInSpecification
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_HideInSpecification == -1)
        AvsIDCache.attr_HideInSpecification = MetaDataHelper.GetAttributeTypeID(AvsIDCache.attributeHideInSpecification);
      return AvsIDCache.attr_HideInSpecification;
    }
  }

  /// <summary>Идентификатор атрибута "ПОКУПНОЙ"</summary>
  public static int Attr_PokupnIzd
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_PokupnIzd == -1)
        AvsIDCache.attr_PokupnIzd = MetaDataHelper.GetAttributeTypeID("cae04f46-c4d5-44d5-b913-35fbf1bb5c40");
      return AvsIDCache.attr_PokupnIzd;
    }
  }

  /// <summary>Идентификатор атрибута "ПОСТАВЩИК"</summary>
  public static int Attr_Postavthik
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Postavthik == -1)
        AvsIDCache.attr_Postavthik = MetaDataHelper.GetAttributeTypeID("cad01519-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.attr_Postavthik;
    }
  }

  /// <summary>Идентификатор атрибута "Раздел ведомости ЗИП"</summary>
  public static int Attr_RazdVedZip
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_RazdVedZip == -1)
        AvsIDCache.attr_RazdVedZip = MetaDataHelper.GetAttributeTypeID("cadd9bcd-306c-11d8-b4e9-00304f19f545");
      return AvsIDCache.attr_RazdVedZip;
    }
  }

  /// <summary>Идентификатор атрибута "Размеры и параметры (т.е. короткое наименование)"</summary>
  public static int Attr_Razmery_I_Parametry
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_Razmery_I_Parametry == -1)
        AvsIDCache.attr_Razmery_I_Parametry = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrRazmery_I_ParametryGuid);
      return AvsIDCache.attr_Razmery_I_Parametry;
    }
  }

  /// <summary>Идентификатор атрибута "Идентификатор изделия"</summary>
  public static int Attr_ArticleID
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_ArticleID == -1)
        AvsIDCache.attr_ArticleID = MetaDataHelper.GetAttributeTypeID(new Guid("cad00622-306c-11d8-b4e9-00304f19f545"));
      return AvsIDCache.attr_ArticleID;
    }
  }

  /// <summary>Идентификатор атрибута "Для сгруппированных записей всегда выводить "Размеры и параметры"</summary>
  public static int Attr_GroupWithoutClass
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.attr_GroupWithoutClass == -1)
        AvsIDCache.attr_GroupWithoutClass = MetaDataHelper.GetAttributeTypeID(AvsIDCache.AttrGroupWithoutClassGuid);
      return AvsIDCache.attr_GroupWithoutClass;
    }
  }

  /// <summary>На объект заданного типа можно выпустить спецификацию</summary>
  /// <param name="dbObjectType">Идентификатор типа объекта</param>
  /// <returns></returns>
  public static bool IsProductForSpecification(int dbObjectType)
  {
    if (dbObjectType == -1 || dbObjectType == 0)
      return false;
    return MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_AssemblyUnit) || MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_Complect) || MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_Complex) || MetaDataHelper.HasApplicability(dbObjectType, AvsIDCache.ObjType_Specification, AvsIDCache.Relation_Document);
  }

  /// <summary>Найти Спецификацию для исполнений изделия</summary>
  /// <param name="session">Сессия</param>
  /// <param name="productsId">Идентификаторы исполнений изделия</param>
  /// <param name="filtrationOwnerID">Идентификатор правила подбора версий</param>
  /// <param name="ignoreRelationWithoutVersion">Игнорировать связи без конкретизации версии</param>
  public static long FindSpecificationForAssemblyProducts(
    IUserSession session,
    IList<long> productsId,
    string filtrationOwnerID,
    bool ignoreRelationWithoutVersion)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ObjectTypeID = AvsIDCache.ObjType_Specification;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_VersionInRelation, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0)
    });
    AvsIDCache.SetFiltrationTags(ref paramSet);
    for (int index = 0; index < productsId.Count; ++index)
    {
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, productsId[index]);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64_1 = AvsIDCache.ConvertDbValueToInt64(row[1]);
        if (!ignoreRelationWithoutVersion || int64_1.IsDefinedId())
        {
          long int64_2 = AvsIDCache.ConvertDbValueToInt64(row[0]);
          if (int64_1.IsUndefinedId() && !ignoreRelationWithoutVersion || Math.Abs(int64_1) == Math.Abs(int64_2))
          {
            dataTable.Dispose();
            return int64_2;
          }
        }
      }
    }
    return -1;
  }

  /// <summary>Найти Сборочную единицу для спецификации по связям</summary>
  /// <param name="session">Сессия</param>
  /// <param name="documentID">Идентификатор версии документа</param>
  /// <param name="filtrationOwnerID">Уникальный ID настроек фильтрации, по которым будет проводиться фильтрация состава</param>
  /// <returns>Возвращает список исполнений связанных с документом</returns>
  public static List<long> FindProductForSpecificationByRelations(
    IUserSession session,
    long documentID,
    string filtrationOwnerID)
  {
    return AvsIDCache.FindProductForSpecificationByRelations(session, documentID, filtrationOwnerID, out List<long> _);
  }

  /// <summary>Найти Сборочную единицу для спецификации по связям</summary>
  /// <param name="session">Сессия</param>
  /// <param name="documentID">Идентификатор версии документа</param>
  /// <param name="filtrationOwnerID">Уникальный ID настроек фильтрации, по которым будет проводиться фильтрация состава</param>
  /// <param name="productsWithoutVersionInRelation">Список исполнений без конкретизации версии документа на связи</param>
  /// <returns>Возвращает список исполнений связанных с документом</returns>
  public static List<long> FindProductForSpecificationByRelations(
    IUserSession session,
    long documentID,
    string filtrationOwnerID,
    out List<long> productsWithoutVersionInRelation)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(AvsIDCache.Relation_Document, filtrationOwnerID);
    relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_VersionInRelation, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0)
    });
    AvsIDCache.SetFiltrationTags(ref paramSet);
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, documentID);
    List<long> specificationByRelations = new List<long>(dataTable.Rows.Count);
    List<int> intList1 = new List<int>(specificationByRelations.Count);
    productsWithoutVersionInRelation = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = AvsIDCache.ConvertDbValueToInt64(row[2]);
      long int64_2 = AvsIDCache.ConvertDbValueToInt64(row[0]);
      if (int64_1.IsDefinedId())
      {
        specificationByRelations.Add(int64_2);
        intList1.Add(Convert.ToInt32(row[1]));
      }
      else
        productsWithoutVersionInRelation.Add(int64_2);
    }
    if (intList1.Count > 1)
    {
      List<int> intList2 = new List<int>(intList1.Count);
      int num = -1;
      bool flag = false;
      for (int index = 0; index < intList1.Count; ++index)
      {
        if (index == 0 || intList2.IndexOf(intList1[index]) != -1)
        {
          intList2.Add(intList1[index]);
          if (num == -1)
          {
            num = intList1[index];
            flag = MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Product) || MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Orders);
          }
          else if (!flag && (MetaDataHelper.IsObjectTypeChildOf(intList1[index], AvsIDCache.ObjType_Product) || MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Orders)))
          {
            num = intList1[index];
            flag = true;
          }
        }
      }
      for (int index = intList1.Count - 1; index > 0; --index)
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(intList1[index], num))
          specificationByRelations.RemoveAt(index);
      }
    }
    dataTable.Dispose();
    return specificationByRelations;
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
    List<long> longList = new List<long>();
    if (userSession != null)
    {
      IDBObject dbObject = userSession.GetObject(articleID);
      Guid guid = Guid.Empty;
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
      string str = (string) null;
      if (attributeById != null)
        str = Convert.ToString(attributeById.Value);
      if (!string.IsNullOrEmpty(str))
        guid = new Guid(str);
      if (guid != Guid.Empty)
      {
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
        };
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(dbObject.ObjectType);
        objectCollection.ShowAllModifications = true;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) str, LogicalOperators.AND, 0, true),
          new ConditionStructure(-9, RelationalOperators.NotEqual, (object) userSession.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
        }, columns);
        AvsIDCache.SetFiltrationTags(ref paramSet);
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
          longList.Add(Convert.ToInt64(row[0]));
      }
    }
    return longList.ToArray();
  }

  public static (long specification, List<long> products) FindSpecificationAndAssemblyProducts(
    IDBObject dbObject,
    string filtrationOwnerID)
  {
    IUserSession session = dbObject.Session;
    long num = 0;
    if (MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, AvsIDCache.ObjType_Specification))
      num = dbObject.ObjectID;
    else if (AvsIDCache.IsProductForSpecification(dbObject.ObjectType))
      num = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
      {
        dbObject.ObjectID
      }, filtrationOwnerID, true);
    List<long> longList = !num.IsDefinedId() ? new List<long>() : AvsIDCache.FindProductForSpecificationByRelations(session, num, filtrationOwnerID);
    return (num, longList);
  }

  /// <summary>Настроить фильтрацию состава для СП</summary>
  /// <param name="paramSet">Параметры запроса</param>
  internal static void SetFiltrationTags(ref DBRecordSetParams paramSet)
  {
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    paramSet.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) new long[2]
    {
      0L,
      1L
    };
    paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) null;
    paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) null;
    paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) null;
  }

  /// <summary>Проверить соответствие обозначений изделия (исполнения) и спецификации</summary>
  /// <param name="productDesignation">Обозначение изделия</param>
  /// <param name="docDesignation">Обозначение спецификации</param>
  /// <returns>true, если обозначение изделия соответствует обозначению спецификации</returns>
  public static bool IsSpecificationForThisAssembly(
    string productDesignation,
    string docDesignation)
  {
    if (productDesignation == null)
      throw new ArgumentNullException(nameof (productDesignation));
    if (docDesignation == null)
      throw new ArgumentNullException(nameof (docDesignation));
    if (productDesignation == "")
      throw new ArgumentException("Аргумент productDesignation не может быть пустой строкой");
    return !(docDesignation == "") ? productDesignation.Contains(docDesignation) : throw new ArgumentNullException("Аргумент docDesignation не может быть пустой строкой");
  }

  /// <summary>Получить тип конструкторского документа для стандартного шаблона</summary>
  /// <param name="templateGuid">Guid шаблона</param>
  /// <returns>Возвращает null, если неизвестный шаблон</returns>
  public static AVSDocumentType? GetAvsDocumentTypeFromTemplate(Guid templateGuid)
  {
    if (templateGuid == AvsIDCache.StdTemplateSingleSpecification || templateGuid == AvsIDCache.StdTemplateSpecificationFormB)
      return new AVSDocumentType?(AVSDocumentType.Specification);
    if (templateGuid == AvsIDCache.StdTemplateElementList)
      return new AVSDocumentType?(AVSDocumentType.ElementList);
    if (templateGuid == AvsIDCache.StdTemplateSingleAutopromSpecification || templateGuid == AvsIDCache.StdTemplateAutopromSpecificationFormB || templateGuid == AvsIDCache.StdTemplateMirrorSpecification)
      return new AVSDocumentType?(AVSDocumentType.AutoIndustrySpecification);
    return templateGuid == AvsIDCache.StdTemplateExportSpecification ? new AVSDocumentType?(AVSDocumentType.ExportSpecification) : new AVSDocumentType?();
  }

  /// <summary>Спецификация пришла из другого узла портала</summary>
  public static bool IsSpecificationFromAnotherPortal(long documentID, IUserSession session)
  {
    bool flag = false;
    IDBObject dbObject = session.GetObject(documentID);
    if (dbObject != null && session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService)
    {
      SiteInfo info = customService.Info;
      if (info != null)
      {
        char code = info.Code;
        string siteId = dbObject.SiteID;
        if (siteId.Length >= 2 && (int) siteId[0] != (int) code)
          flag = true;
      }
    }
    return flag;
  }

  public static int FindFileForSpecificationInAttribute(
    IDBAttribute fileAttribute,
    out bool hasLegacyDocument)
  {
    int specificationInAttribute = -1;
    hasLegacyDocument = false;
    for (int index = 0; index < fileAttribute.Values.Length; ++index)
    {
      string str = Path.GetExtension(fileAttribute.Descriptions[index]);
      if (!string.IsNullOrEmpty(str))
      {
        switch (str.ToLower())
        {
          case ".spx":
            if (specificationInAttribute == -1)
            {
              specificationInAttribute = index;
              continue;
            }
            continue;
          case ".sp":
            hasLegacyDocument = true;
            continue;
          default:
            continue;
        }
      }
    }
    return specificationInAttribute;
  }

  public static IDBAttribute FindSpecificationFileAttribute(
    IDBObject docObject,
    out bool isLegacyDocumentOnly)
  {
    bool hasLegacyDocument = false;
    int num = -1;
    IDBAttribute attributeById = docObject.GetAttributeByID(AvsIDCache.Attr_DocumentFile);
    if (attributeById != null)
      num = AvsIDCache.FindFileForSpecificationInAttribute(attributeById, out hasLegacyDocument);
    if (num == -1)
    {
      attributeById = docObject.GetAttributeByID(AvsIDCache.Attr_File);
      if (attributeById != null)
        num = AvsIDCache.FindFileForSpecificationInAttribute(attributeById, out hasLegacyDocument);
    }
    isLegacyDocumentOnly = num == -1 && (hasLegacyDocument || DocumentEditorPluginBase.DBObjectIsScanDocument(docObject));
    if (num == -1)
      return (IDBAttribute) null;
    if (attributeById != null)
      attributeById.Index = num;
    return attributeById;
  }

  /// <summary>Спецификация нуждается в обновлении</summary>
  /// <param name="session">Сессия</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="specId">Возвращает идентификатор версии найденной спецификации</param>
  /// <param name="reasonList">Возвращает сообщения о причинах необходимости обновления</param>
  /// <returns></returns>
  public static bool SpecificationIsNeedUpdate(
    IUserSession session,
    long objectID,
    int objectType,
    out long specId,
    out List<string> reasonList)
  {
    reasonList = new List<string>();
    specId = -1L;
    string str1 = string.Empty;
    string str2 = string.Empty;
    QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
    if (objectType.IsUndefinedTypeId())
      objectType = objectInfo.ObjectTypeID;
    int objectType1 = -1;
    Guid empty = Guid.Empty;
    List<long> longList1 = (List<long>) null;
    List<long> longList2 = (List<long>) null;
    List<long> productsWithoutVersionInRelation = (List<long>) null;
    long num;
    if (AvsIDCache.IsSpecification(objectType))
    {
      if (AvsIDCache.IsSpecificationFromAnotherPortal(objectID, session))
        return false;
      str1 = $"'{objectInfo.Caption}'";
      num = objectID;
      specId = num;
      longList1 = AvsIDCache.FindProductForSpecificationByRelations(session, num, "", out productsWithoutVersionInRelation);
      longList1.AddRange((IEnumerable<long>) productsWithoutVersionInRelation);
      for (int index = 0; index < longList1.Count; ++index)
      {
        IDBObject dbObject = session.GetObject(longList1[index]);
        if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
          return false;
        objectType1 = dbObject.ObjectType;
        if (AvsIDCache.ConvertToGuid((object) dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID)) != Guid.Empty)
        {
          longList2 = new List<long>((IEnumerable<long>) AvsIDCache.FindArticlesByGroupID(longList1[index], (string) null, session));
          break;
        }
      }
    }
    else
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_Document))
        return false;
      IDBObject dbObject = session.GetObject(objectID, true);
      objectID = dbObject.ObjectID;
      str2 = $"'{dbObject.Caption}'";
      if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
        return false;
      objectType1 = dbObject.ObjectType;
      if (AvsIDCache.ConvertToGuid((object) dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID)) != Guid.Empty)
        longList2 = new List<long>((IEnumerable<long>) AvsIDCache.FindArticlesByGroupID(objectID, (string) null, session));
      if (longList2 != null)
        num = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) longList2, "", false);
      else
        num = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new List<long>((IEnumerable<long>) new long[1]
        {
          objectID
        }), (string) null, false);
      if (num.IsDefinedId())
      {
        longList1 = AvsIDCache.FindProductForSpecificationByRelations(session, num, "", out productsWithoutVersionInRelation);
        longList1.AddRange((IEnumerable<long>) productsWithoutVersionInRelation);
      }
    }
    specId = num;
    if (num.IsUndefinedId())
    {
      reasonList.Add($"Для изделия {str2} отсутствует спецификация");
    }
    else
    {
      if (!productsWithoutVersionInRelation.IsEmpty<long>())
      {
        reasonList.Add($"Связи между документом {str1} [{num}] и изделиями, не содержат атрибута 'Идентификатор версии в составе':");
        foreach (long objectID1 in productsWithoutVersionInRelation)
          reasonList.Add($"'{session.GetObjectInfo(objectID1).Caption}' [{objectID1}]");
      }
      if (longList1.IsEmpty<long>())
        reasonList.Add($"Для документа {str1} [{num}] отсутствует связь с изделием");
      else if (longList2.IsEmpty<long>())
      {
        if (longList1.Count != 1)
          reasonList.Add($"Документ {str1} [{num}] связан с несколькими изделиями без атрибута 'Идентификатор группового изделия'");
      }
      else
      {
        long[] array1 = longList2.Except<long>((IEnumerable<long>) longList1).ToArray<long>();
        if (((IEnumerable<long>) array1).Any<long>())
        {
          reasonList.Add($"Документ {str1} [{num}] не связан с исполнениями изделий: ");
          foreach (long objectID2 in array1)
            reasonList.Add($"'{session.GetObjectInfo(objectID2).Caption}' [{objectID2}]");
        }
        long[] array2 = longList1.Except<long>((IEnumerable<long>) longList2).ToArray<long>();
        if (((IEnumerable<long>) array2).Any<long>())
        {
          reasonList.Add($"Документ {str1} [{num}] связан c несколькими изделиями без атрибута 'Идентификатор группового изделия': ");
          foreach (long objectID3 in array2)
            reasonList.Add($"'{session.GetObjectInfo(objectID3).Caption}' [{objectID3}]");
        }
      }
      DateTime? nullable = new DateTime?();
      IDBObject dbObject = session.GetObject(num);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      {
        reasonList.Clear();
        return false;
      }
      if (AvsIDCache.GetIsNeedUpdateDocumentFlag(dbObject))
        reasonList.Add($"Документ {str1} [{num}] отмечен через атрибут 'Необходимо обновить документ'");
      bool isLegacyDocumentOnly;
      if (AvsIDCache.FindSpecificationFileAttribute(dbObject, out isLegacyDocumentOnly) is IBlobReader specificationFileAttribute)
      {
        nullable = new DateTime?(specificationFileAttribute.OpenBlob(-1).ModifyDate);
        specificationFileAttribute.CloseBlob();
      }
      if (!isLegacyDocumentOnly)
      {
        if (!nullable.HasValue)
        {
          reasonList.Add($"Объект документа {str1} [{num}] не имеет файла");
        }
        else
        {
          List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
          for (int index = 0; index < longList1.Count; ++index)
            conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.Equal, (object) longList1[index], LogicalOperators.OR, 0, true));
          if (conditionStructureList.Count > 1)
          {
            ConditionStructure conditionStructure1 = conditionStructureList[0] with
            {
              GroupID = 1
            };
            conditionStructureList[0] = conditionStructure1;
            ConditionStructure conditionStructure2 = conditionStructureList[conditionStructureList.Count - 1] with
            {
              GroupID = -1
            };
            conditionStructureList[conditionStructureList.Count - 1] = conditionStructure2;
          }
          ColumnDescriptor[] columns = new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
            new ColumnDescriptor((object) AvsIDCache.Attr_ContentModifyDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
          };
          DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns);
          AvsIDCache.SetFiltrationTags(ref paramSet);
          TimeSpan timeSpan = new TimeSpan(0, 0, 5);
          IDBObjectCollection objectCollection = session.GetObjectCollection(objectType1);
          objectCollection.ShowAllModifications = true;
          DataTable dataTable = objectCollection.Select(paramSet);
          if (dataTable != null)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (dataTable.Rows[index][1] != null && dataTable.Rows[index][1] != DBNull.Value)
              {
                DateTime dateTime = Convert.ToDateTime(dataTable.Rows[index][1]);
                long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
                if (!isLegacyDocumentOnly && nullable.HasValue && nullable.Value < dateTime && (nullable.Value - dateTime).Duration() > timeSpan)
                  reasonList.Add($"Изделие '{session.GetObjectInfo(int64).Caption}' [{int64}] изменялось позже сохранения файла документа {str1} [{num}]");
              }
            }
          }
        }
      }
    }
    return reasonList.Count > 0;
  }

  public static bool GetIsNeedUpdateDocumentFlag(IDBObject document)
  {
    IDBAttribute attributeById = document.GetAttributeByID(AvsIDCache.Attr_NeedUpdateDoc);
    return attributeById != null && attributeById.Value != null && !(attributeById.Value is DBNull) && Convert.ToBoolean(attributeById.Value);
  }

  /// <summary>Проверка объекта остаётся ли он удалённым исполнением из группы изделий родительская версии</summary>
  /// <param name="article">Изделие</param>
  /// <returns></returns>
  public static bool ArticleIsRemovedFormGroupSpecification(IDBObject article)
  {
    if (Consts.IsUndefinedObjectId(article.ParentVersionID))
      return false;
    Guid guid = AvsIDCache.ConvertToGuid((object) article.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID));
    return AvsIDCache.ConvertToGuid((object) article.Session.GetObject(article.ParentVersionID).GetAttributeByID(AvsIDCache.Attr_ArticleGroupID)) != guid;
  }

  /// <summary>Инициализировать псевдонимы типов AVS для XML</summary>
  public static void InitTypeNameDictionary()
  {
    if (AvsIDCache.typeNameDictionaryInitialized)
      return;
    AvsIDCache.typeNameDictionaryInitialized = true;
    Type type1 = typeof (AVSDocumentsSettings);
    DocumentTreeNode.TypeNameDictionary[(object) type1.Name] = (object) type1;
    DocumentTreeNode.TypeConstructorDictionary[(object) type1.Name] = (object) new EmptyConstructorDelegate(AVSDocumentsSettings.EmptyConstructor);
    Type type2 = typeof (AVSDocumentType);
    DocumentTreeNode.TypeNameDictionary[(object) type2.Name] = (object) type2;
    Type type3 = typeof (AVSDocumentForm);
    DocumentTreeNode.TypeNameDictionary[(object) type3.Name] = (object) type3;
    Type key1 = typeof (Dictionary<AVSDocumentForm, Guid>);
    DocumentTreeNode.TypeNameDictionary[(object) "Dic_SpecForm_Guid"] = (object) key1;
    DocumentTreeNode.TypeAliasDictionary[key1] = "Dic_SpecForm_Guid";
    Type key2 = typeof (Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>);
    DocumentTreeNode.TypeNameDictionary[(object) "Dic_AVSDocumentType_SpecForm"] = (object) key2;
    DocumentTreeNode.TypeAliasDictionary[key2] = "Dic_AVSDocumentType_SpecForm";
    DocumentTreeNode.TypeNameDictionary[(object) "Guid"] = (object) typeof (Guid);
    Type key3 = typeof (Dictionary<Guid, Dictionary<int, Guid>>);
    DocumentTreeNode.TypeNameDictionary[(object) "Dic_Guid_Int_Guid"] = (object) key3;
    DocumentTreeNode.TypeAliasDictionary[key3] = "Dic_Guid_Int_Guid";
    Type key4 = typeof (Dictionary<int, Guid>);
    DocumentTreeNode.TypeNameDictionary[(object) "Dic_Int_Guid"] = (object) key4;
    DocumentTreeNode.TypeAliasDictionary[key4] = "Dic_Int_Guid";
  }

  /// <summary>Временный врапер для перехода на CreateVersionEx</summary>
  /// <returns></returns>
  public static IDBObject CreateVersionEx_TMPWrapper(
    int objectType,
    long objectID,
    IUserSession session)
  {
    long[] versionEx = session.GetObjectCollection(objectType).CreateVersionEx(objectID);
    return versionEx != null && versionEx.Length != 0 ? session.GetObject(versionEx[0]) : (IDBObject) null;
  }

  /// <summary>Преобразовать значение атрибута из БД в Int64.</summary>
  /// <param name="value">Значение</param>
  /// <param name="defaultValue">Значение по умолчанию, если value null, DBNull или пустая строка</param>
  /// <returns></returns>
  public static long ConvertDbValueToInt64(object dbValue, long defaultValue = -1)
  {
    switch (dbValue)
    {
      case null:
      case DBNull _:
        return defaultValue;
      case long int64:
        return int64;
      case AVSObjectInfo avsObjectInfo:
        return avsObjectInfo.Id;
      case string s:
        return !(s != "") ? defaultValue : long.Parse(s);
      default:
        return Convert.ToInt64(dbValue);
    }
  }

  /// <summary>Преобразовать значение атрибута из БД в Int64.</summary>
  /// <param name="value">Значение</param>
  /// <param name="defaultValue">Значение по умолчанию, если value null, DBNull или пустая строка</param>
  /// <returns></returns>
  public static int ConvertDbValueToInt32(object dbValue, int defaultValue = -1)
  {
    switch (dbValue)
    {
      case null:
      case DBNull _:
        return defaultValue;
      case int int32:
        return int32;
      case string s:
        return !(s != "") ? defaultValue : int.Parse(s);
      default:
        return Convert.ToInt32(dbValue);
    }
  }

  /// <summary>Преобразовать значение в Guid. DBNull, null и "" приравниваются Guid.Empty</summary>
  /// <param name="value">Значение</param>
  /// <returns></returns>
  public static Guid ConvertToGuid(object value)
  {
    object obj = value;
    if (value is IDBAttribute dbAttribute)
      obj = dbAttribute.Value;
    switch (obj)
    {
      case null:
      case DBNull _:
        return Guid.Empty;
      case Guid guid:
        return guid;
      default:
        string g = Convert.ToString(obj);
        return string.IsNullOrWhiteSpace(g) ? Guid.Empty : new Guid(g);
    }
  }

  public static IDBAVSDocumentObject GetDBAVSDocumentObject(IUserSession session, long documentID)
  {
    return session != null ? AvsIDCache.GetDBAVSDocumentObject(session.GetObject(documentID)) : throw new ArgumentNullException(nameof (session));
  }

  public static IDBAVSDocumentObject GetDBAVSDocumentObject(IDBObject documentDBObject)
  {
    if (documentDBObject == null)
      throw new ArgumentNullException(nameof (documentDBObject));
    if (!(documentDBObject is IDBAVSDocumentObject dbavsDocumentObject))
    {
      AvsIDCache.RegisterDocTypeInObjectCreatorService(documentDBObject.Session, documentDBObject.TypeID);
      dbavsDocumentObject = (IDBAVSDocumentObject) documentDBObject.Session.GetObject(documentDBObject.ObjectID);
    }
    return dbavsDocumentObject;
  }

  private static void RegisterDocTypeInObjectCreatorService(
    IUserSession session,
    int documentObjectTypeID)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (!(session.GetCustomService(typeof (IAVSServerService)) is IAVSServerService customService))
      throw new Exception("Недоступен сервис IAVSServerService");
    customService.AddAvsDBObjectCreator((object) MetaDataHelper.GetObjectTypeGuid(documentObjectTypeID));
  }
}
