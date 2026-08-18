
// Type: Intermech.Interfaces.Briefcase.BriefcaseConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Interfaces.Briefcase
{
    public class BriefcaseConsts
    {
      public static readonly Encoding MemoEncoding = Encoding.UTF8;
      public static readonly int ResultOk = 0;
      public static readonly int ResultUserBreak = -1;
      public static readonly int ResultCreateError = -2;
      public static readonly int ResultExportError = -3;
      public static readonly int ResultCancelOpen = -4;
      public const string ShortBlobFolderName = "ShortBlob";
      public const string BlobFolderName = "Blob";
      public const string MemoFolderName = "Memo";
      public const string ExportLogFileName = "export.log";
      public const string XmlBriefcaseConfig = "BriefcaseConfig.xml";
      public static readonly string XmlConfigurationTag = "configuration";
      public static readonly string XmlBriefcaseTag = "briefcase";
      public static readonly string XmlCommentTag = "comment";
      public static readonly string XmlNameTag = "name";
      public static readonly string XmlVersionTag = "version";
      public static readonly string XmlExportDateTag = "exportdate";
      public static readonly string XmlLastSystemUpdateTag = "lastsystemupdate";
      public static readonly string XmlClosedFlag = "closed";
      public static readonly string XmlIncludeLocalization = "localization";
      public static readonly string XmlSiteGuid = "site";
      public const string XmlMetadataExportListFileName = "MetadataExportList.xml";
      public const string XmlMetadataExportListSchemaFileName = "MetadataExportList.xsd";
      public static readonly string XmlMetadataExportListDatasetName = "MetadataExportList".ToUpper();
      public static readonly string XmlMetadataRecordTag = "Metadata".ToUpper();
      public static readonly string XmlCategoryTag = "category";
      public static readonly string XmlIdTag = "id";
      public static readonly string XmlExternalTag = "external";
      public const string XmlMetadataFileName = "Metadata.xml";
      public const string XmlMetadataSchemaFileName = "Metadata.xsd";
      public static readonly string XmlMetadataDatasetName = "MetadataSet".ToUpper();
      public static readonly string XmlMetadataTableName = "Metadata".ToUpper();
      public static readonly string XmlDataCategoryColName = BriefcaseConsts.XmlCategoryTag.ToUpper();
      public static readonly string XmlDataIdColName = BriefcaseConsts.XmlIdTag.ToUpper();
      public const string XmlObjectsFileName = "Objects.xml";
      public const string XmlObjectsSchemaFileName = "Objects.xsd";
      public static readonly string XmlObjectsDatasetName = "ObjectsDataSet".ToUpper();
      public static readonly string XmlObjectRecordTag = "Object".ToUpper();
      public const string XmlObjAttributesFileName = "ObjAttributes.xml";
      public const string XmlObjAttributesSchemaFileName = "ObjAttributes.xsd";
      public static readonly string XmlObjAttributesDatasetName = "ObjAttributesDataSet".ToUpper();
      public static readonly string XmlAttributeRecordTag = "Attribute".ToUpper();
      public const string XmlObjLCStepsFileName = "ObjLcSteps.xml";
      public const string XmlObjLCStepsSchemaFileName = "ObjLcSteps.xsd";
      public static readonly string XmlObjLCStepsDatasetName = "LcStepsDataSet".ToUpper();
      public static readonly string XmlObjLCStepsRecordTag = "LcStep".ToUpper();
      public const string XmlContextsFileName = "Contexts.xml";
      public const string XmlContextsSchemaFileName = "Contexts.xsd";
      public static readonly string XmlContextsDatasetName = "ContextsDataSet".ToUpper();
      public static readonly string XmlContextsRecordTag = "Context".ToUpper();
      public const string XmlExportContentFileName = "ExportContent.xml";
      public const string XmlExportContentSchemaFileName = "ExportContent.xsd";
      public static readonly string XmlExportContentDatasetName = "ExportContentDataSet".ToUpper();
      public static readonly string XmlExportAttributeRecordTag = "ExportAttribute".ToUpper();
      public const string XmlRelationsFileName = "Relations.xml";
      public const string XmlRelationsSchemaFileName = "Relations.xsd";
      public static readonly string XmlRelationsDatasetName = "RelationsDataSet".ToUpper();
      public static readonly string XmlRelationRecordTag = "Relation".ToUpper();
      public const string XmlRelAttributesFileName = "RelAttributes.xml";
      public const string XmlRelAttributesSchemaFileName = "RelAttributes.xsd";
      public static readonly string XmlRelAttributesDatasetName = "RelAttributesDataSet".ToUpper();
      public const string XmlMetadataSecurityFileName = "MetadataSecurity.xml";
      public const string XmlMetadataSecuritySchemaFileName = "MetadataSecurity.xsd";
      public static readonly string XmlMetadataSecurityDatasetName = "MetadataSecurityDataSet".ToUpper();
      public const string XmlObjSecurityFileName = "ObjSecurity.xml";
      public const string XmlObjSecuritySchemaFileName = "ObjSecurity.xsd";
      public static readonly string XmlObjSecurityDatasetName = "ObjSecurityDataSet".ToUpper();
      public static readonly string XmlSecurityRecordTag = "Security".ToUpper();
      public static readonly string XmlContextIDAttributeName = "id";
      public static readonly string XmlContextModificationIDAttributeName = "modification_id";
      public static readonly string XmlContextContentAttributeName = "content";
      /// <summary>список файлов портфеля</summary>
      public static readonly List<string> BriefcaseFiles = new List<string>((IEnumerable<string>) new string[24]
      {
        "export.log".ToUpper(),
        "BriefcaseConfig.xml".ToUpper(),
        "MetadataExportList.xml".ToUpper(),
        "MetadataExportList.xsd".ToUpper(),
        "Metadata.xml".ToUpper(),
        "Metadata.xsd".ToUpper(),
        "Objects.xml".ToUpper(),
        "Objects.xsd".ToUpper(),
        "ObjAttributes.xml".ToUpper(),
        "ObjAttributes.xsd".ToUpper(),
        "ObjLcSteps.xml".ToUpper(),
        "ObjLcSteps.xsd".ToUpper(),
        "Contexts.xml".ToUpper(),
        "Contexts.xsd".ToUpper(),
        "ExportContent.xml".ToUpper(),
        "ExportContent.xsd".ToUpper(),
        "Relations.xml".ToUpper(),
        "Relations.xsd".ToUpper(),
        "RelAttributes.xml".ToUpper(),
        "RelAttributes.xsd".ToUpper(),
        "MetadataSecurity.xml".ToUpper(),
        "MetadataSecurity.xsd".ToUpper(),
        "ObjSecurity.xml".ToUpper(),
        "ObjSecurity.xsd".ToUpper()
      });
      /// <summary>список папок портфеля</summary>
      public static readonly List<string> BriefcaseFolders = new List<string>((IEnumerable<string>) new string[3]
      {
        "ShortBlob".ToUpper(),
        "Blob".ToUpper(),
        "Memo".ToUpper()
      });
      public const string F_PATH2FILE = "F_PATH2FILE";
      public static readonly string BlobFileExt = ".blb";
      public static readonly string MemoFileExt = ".mem";
      public static int[] MetadataInfoCategories = new int[9]
      {
        12,
        3,
        9,
        8,
        4,
        6,
        11,
        7,
        16 /*0x10*/
      };
      public static int[] ElementInfoCategories = new int[2]
      {
        1,
        5
      };
      public const string F_INTEGERGUID = "F_INTEGERGUID";
      public const string F_DOUBLEGUID = "F_DOUBLEGUID";
      public const string F_SIZEGUID = "F_SIZEGUID";
      public const string F_DEFAULTGUID = "F_DEFAULTGUID";
      public const string F_CHKOUTGUID = "F_CHKOUTGUID";
      public const string F_OWNERGUID = "F_OWNERGUID";
      public const string F_OBJECTGUID = "F_OBJECTGUID";
      public const string F_IDGUID = "F_IDGUID";
      public const string F_PROJECTGUID = "F_PROJECTGUID";
      public static string prefixUnpack = "ser_unpack_";
      public static string prefixPack = "ser_";
      public static string logErrorString = "*Error* ";
      public static string logImportName = "Import_{0}.log";
      public static string logObjectNotImported = LocalizationHolder.rm.GetString("Interfaces.Briefcase_1");
      public static string logObjectNotImportedLocalization = LocalizationHolder.rm.GetString("Interfaces.Briefcase_2");
      public static string logObjectNotFoundInDB = LocalizationHolder.rm.GetString("Interfaces.Briefcase_3");
      public static string logObjectImported = "{0} ... OK";
      public static string logOKImported = " ... OK";
      public static string logAttributeForImported = LocalizationHolder.rm.GetString("Interfaces.Briefcase_4");
      public static string logBriefcaseFilesNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_5");
      public static string logObjectTypeChanged = LocalizationHolder.rm.GetString("Interfaces.Briefcase_6");
      public static string logObjectAttrError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_7");
      public static string logRelationAttrError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_8");
      public static string logObjectViewsError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_9");
      public static string logRelationViewsError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_10");
      public static string logObjectPropertiesError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_11");
      public static string logRelationPropertiesError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_12");
      public static string logCheckUnknError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_13");
      public static string logImportObjectUnknError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_14");
      public static string logImportRelationUnknError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_15");
      public static string logLinkOwnerObject = LocalizationHolder.rm.GetString("Interfaces.Briefcase_16");
      public static string logLinkObject = LocalizationHolder.rm.GetString("Interfaces.Briefcase_17");
      public static string logContextsUnknError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_212");
      public static string logGUID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_18");
      public static string logName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_19");
      public static string logAlias = LocalizationHolder.rm.GetString("Interfaces.Briefcase_20");
      public static string logFormatGUID = "Guid {{{0}}}";
      public static string logFormatName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_21");
      public static string logFormatID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_22");
      public static string logFormatAlias = LocalizationHolder.rm.GetString("Interfaces.Briefcase_23");
      public static string logFormatObject = LocalizationHolder.rm.GetString("Interfaces.Briefcase_24");
      private static string logAttributeAddOptions = LocalizationHolder.rm.GetString("Interfaces.Briefcase_25");
      private static string logAttributeAddQuote = "\"";
      public static string logAttributeAddUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_26");
      public static string logAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_27");
      public static string logAttributeFieldType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_28");
      public static string logAttributeLostData = LocalizationHolder.rm.GetString("Interfaces.Briefcase_29");
      public static string logAttributeFieldTypeError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_30");
      public static string logAttributeModifyDate = LocalizationHolder.rm.GetString("Interfaces.Briefcase_31");
      public static string logAttributeLanguage = LocalizationHolder.rm.GetString("Interfaces.Briefcase_32");
      public static string logAttributeMask = LocalizationHolder.rm.GetString("Interfaces.Briefcase_33");
      public static string logAttributeShortName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_34");
      public static string logAttributeNote = LocalizationHolder.rm.GetString("Interfaces.Briefcase_35");
      public static string logAttributeSaveCommonHistory = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.SaveCommonHistory) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeDisableManualEdit = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.DisableManualEdit) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeDisableNullsError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_36");
      public static string logAttributeDisableNullsWarning = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.DisableNulls) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeGetDescriptionEvent = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.GetDescriptionEvent) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeInternal = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.Internal) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeModifyInBase = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.ModifyInBase) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeSaveInLog = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.SaveInLog) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeSavePrivateHistory = BriefcaseConsts.logAttributeAddOptions + BriefcaseConsts.logAttributeAddQuote + EnumDescConverter.GetEnumDescription((Enum) AttributeOptions.SavePrivateHistory) + BriefcaseConsts.logAttributeAddQuote;
      public static string logAttributeDefaultValue = LocalizationHolder.rm.GetString("Interfaces.Briefcase_37");
      public static string logAttributeMultiValueMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_38");
      public static string logAttributePossibleValues = LocalizationHolder.rm.GetString("Interfaces.Briefcase_39");
      public static string logAttributeInvalidPossibleValues = LocalizationHolder.rm.GetString("Interfaces.Briefcase_210");
      public static string logAttributeComputeValueMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_40");
      public static string logAttributeFormula = LocalizationHolder.rm.GetString("Interfaces.Briefcase_41");
      public static string logAttributeNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_42");
      public static string logAttributeAliasPresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_43");
      public static string logAttributeNotValidFormula = LocalizationHolder.rm.GetString("Interfaces.Briefcase_44");
      public static string logAttributeUniqueValueMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_45");
      public static string logAttributeLevelID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_46");
      public static string logAttributeOptimizationMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_47");
      public static string logAttributeSourceAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_48");
      public static string logAttributeSourceAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_49");
      public static string logAttributeMasterAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_50");
      public static string logAttributeMasterAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_51");
      public static string logAttributeInheritMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_52");
      public static string logAttributeRequiredMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_53");
      public static string logAttributeNotFoundInRelationType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_54");
      public static string logAttributeValidationRule = LocalizationHolder.rm.GetString("Interfaces.Briefcase_55");
      public static string logAttribute4ObjectTypeFormatName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_56");
      public static string logAttribute4ObjectTypeCategory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_202");
      public static string logAttribute4objTypeAddUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_57");
      public static string logObjectTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_58");
      public static string logObjectTypeAddUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_59");
      public static string logObjectTypeAttributeNotPresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_60");
      public static string logObjectTypeAttributeInvalidInheriteMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_61");
      public static string logObjectTypeAttributeNotPresentInBriefCase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_62");
      public static string logObjectTypeAnyAttributes = LocalizationHolder.rm.GetString("Interfaces.Briefcase_63");
      public static string logObjectTypeInstanceName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_64");
      public static string logObjectTypeIcon = LocalizationHolder.rm.GetString("Interfaces.Briefcase_65");
      public static string logObjectTypeVersionMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_66");
      public static string logObjectTypeDefaultRelation = LocalizationHolder.rm.GetString("Interfaces.Briefcase_67");
      public static string logObjectTypeParentTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_68");
      public static string logObjectTypeParentType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_69");
      public static string logObjectTypeСаptionAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_70");
      public static string logObjectTypeСаptionNotValidAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_71");
      public static string logObjectTypeСаptionAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_72");
      public static string logObjectTypePublicLC = LocalizationHolder.rm.GetString("Interfaces.Briefcase_73");
      public static string logObjectTypeSubjectAreas = LocalizationHolder.rm.GetString("Interfaces.Briefcase_74");
      public static string logObjectTypeChildTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_75");
      public static string logObjectTypeChildTypeNotEqual = LocalizationHolder.rm.GetString("Interfaces.Briefcase_76");
      public static string logAttribute4ObjectTypeAddNullVAlueAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_77");
      public static string logObjectTypeLCStepNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_78");
      public static string logObjectTypeLCScheme = LocalizationHolder.rm.GetString("Interfaces.Briefcase_79");
      public static string logObjectTypeNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_80");
      public static string logObjectTypeLCStepScheme = LocalizationHolder.rm.GetString("Interfaces.Briefcase_81");
      public static string logObjectTypeLCStepNotFoundInBaseObject = LocalizationHolder.rm.GetString("Interfaces.Briefcase_82");
      public static string logObjectTypeLCStepNotFoundInBriefObject = LocalizationHolder.rm.GetString("Interfaces.Briefcase_83");
      public static string logObjectTypeLCStepNotFoundRelation = LocalizationHolder.rm.GetString("Interfaces.Briefcase_84");
      public static string logRelationTypeAddUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_85");
      public static string logObjectTypeRelationNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_86");
      public static string logApplicabilityUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_87");
      public static string logApplicabilityCategory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_203");
      public static string logApplicabilityCheckoutFiles = LocalizationHolder.rm.GetString("Interfaces.Briefcase_88");
      public static string logApplicabilityRelationConstraintMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_89");
      public static string logApplicabilityOptionsEnableMultilink = LocalizationHolder.rm.GetString("Interfaces.Briefcase_90");
      public static string logApplicabilityOptionsDefaultRelation = LocalizationHolder.rm.GetString("Interfaces.Briefcase_207");
      public static string logApplicabilityMaximumLinks = LocalizationHolder.rm.GetString("Interfaces.Briefcase_91");
      public static string logApplicabilityIsContent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_31");
      public static string logApplicabilityCloneChildRelations = LocalizationHolder.rm.GetString("Interfaces.Briefcase_92");
      public static string logApplicabilityApplicabilityMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_93");
      public static string logRelationTypeNotFoundInBriefcase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_94");
      public static string logObjectTypeNotFoundInBriefcase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_95");
      public static string logApplicabilityNotFoundInBriefcase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_96");
      public static string logRelationTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_97");
      public static string logRelationTypeName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_98");
      public static string logRelationTypeNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_99");
      public static string logRelationTypeCheckoutFiles = LocalizationHolder.rm.GetString("Interfaces.Briefcase_88");
      public static string logRelationTypeReverseName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_100");
      public static string logRelationTypeTypeName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_101");
      public static string logRelationTypeSaveInHistory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_102");
      public static string logRelationTypeRelationKind = LocalizationHolder.rm.GetString("Interfaces.Briefcase_103");
      public static string logRelationTypeSubjectAreas = LocalizationHolder.rm.GetString("Interfaces.Briefcase_104");
      public static string logAttribute4RelationTypeCategory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_205");
      public static string logAttribute4RelationTypeFormatName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_105");
      public static string logRelationTypeAttributeNotPresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_106");
      public static string logAttribute4RelationTypeSourceAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_107");
      public static string logAttribute4RelationTypeSourceAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_108");
      public static string logAttribute4RelationTypeMasterAttributeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_109");
      public static string logAttribute4RelationTypeMasterAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_110");
      public static string logAttribute4RelationTypeDisableNullsError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_111");
      public static string logAttribute4RelationTypeDisableNullsWarning = LocalizationHolder.rm.GetString("Interfaces.Briefcase_112");
      public static string logAttribute4RelationTypeSaveCommonHistory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_113");
      public static string logAttribute4RelationTypeDisableManualEdit = LocalizationHolder.rm.GetString("Interfaces.Briefcase_114");
      public static string logAttribute4RelationTypeGetDescriptionEvent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_204");
      public static string logAttribute4RelationTypeInternal = LocalizationHolder.rm.GetString("Interfaces.Briefcase_115");
      public static string logAttribute4RelationTypeModifyInBase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_116");
      public static string logAttribute4RelationTypeSaveInLog = LocalizationHolder.rm.GetString("Interfaces.Briefcase_113");
      public static string logAttribute4RelationTypeSavePrivateHistory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_117");
      public static string logRelationTypeAttributeNotPresentInBriefCase = LocalizationHolder.rm.GetString("Interfaces.Briefcase_118");
      public static string logAttribute4RelationTypeAddNullValueAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_213");
      public static string logLCStepNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_119");
      public static string logLCStepAccessType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_120");
      public static string logLCStepDrawData = LocalizationHolder.rm.GetString("Interfaces.Briefcase_121");
      public static string logLCStepIsFirstStep = LocalizationHolder.rm.GetString("Interfaces.Briefcase_122");
      public static string logLCStepName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_123");
      public static string logLCStepDisableParallelVersions = LocalizationHolder.rm.GetString("Interfaces.Briefcase_208");
      public static string logLCStepBaseVersion = LocalizationHolder.rm.GetString("Interfaces.Briefcase_209");
      public static string logLCStepLevelID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_124");
      public static string logLCStepObjectModifyMode = LocalizationHolder.rm.GetString("Interfaces.Briefcase_125");
      public static string logLCSchemeID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_79");
      public static string logLCLevelNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_126");
      public static string logLCLevelName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_127");
      public static string logLCLevelNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_128");
      public static string logLCLevelLitera = LocalizationHolder.rm.GetString("Interfaces.Briefcase_129");
      public static string logLCLevelIsDefault = LocalizationHolder.rm.GetString("Interfaces.Briefcase_130");
      public static string logLCLevelSubjectAreas = LocalizationHolder.rm.GetString("Interfaces.Briefcase_131");
      public static string logLanguageNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_132");
      public static string logLanguageName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_133");
      public static string logLanguageIsDefault = LocalizationHolder.rm.GetString("Interfaces.Briefcase_134");
      public static string logLanguageNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_135");
      public static string logSubjectAreaNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_136");
      public static string logSubjectAreaName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_137");
      public static string logAttributesGroupNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_138");
      public static string logAttributesGroupName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_139");
      public static string logAttributesGroupNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_140");
      public static string logAttributesGroupGuid = LocalizationHolder.rm.GetString("Interfaces.Briefcase_141");
      public static string logAttributesGroupCategory = LocalizationHolder.rm.GetString("Interfaces.Briefcase_206");
      public static string logAttributesGroupAddUniIdentifiler = LocalizationHolder.rm.GetString("Interfaces.Briefcase_142");
      public static string logAttributeInAttributesGroupNotInGroup = LocalizationHolder.rm.GetString("Interfaces.Briefcase_143");
      public static string logLCSchemeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_144");
      public static string logLCSchemeName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_145");
      public static string logLCSchemeNamePresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_146");
      public static string logLCSchemeGuid = LocalizationHolder.rm.GetString("Interfaces.Briefcase_147");
      public static string logLCSchemeSubjectAreas = LocalizationHolder.rm.GetString("Interfaces.Briefcase_74");
      public static string logLCSchemeNote = LocalizationHolder.rm.GetString("Interfaces.Briefcase_35");
      public static string logLCSchemeIsDefault = LocalizationHolder.rm.GetString("Interfaces.Briefcase_148");
      public static string logLCSchemeDrawData = LocalizationHolder.rm.GetString("Interfaces.Briefcase_149");
      public static string logLCSchemeLCStepNotPresent = LocalizationHolder.rm.GetString("Interfaces.Briefcase_150");
      public static string logLCSchemeLCStepAnotherSheme = LocalizationHolder.rm.GetString("Interfaces.Briefcase_151");
      public static string ImportLogLanguageName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_152");
      public static string ImportLogLanguageNotSynhronize = LocalizationHolder.rm.GetString("Interfaces.Briefcase_153");
      public static string ImportLogNewLanguage = LocalizationHolder.rm.GetString("Interfaces.Briefcase_154");
      public static string ImportLogSubjectAreaName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_155");
      public static string ImportLogSubjectAreaNote = LocalizationHolder.rm.GetString("Interfaces.Briefcase_156");
      public static string ImportLogSubjectArea = LocalizationHolder.rm.GetString("Interfaces.Briefcase_157");
      public static string ImportLogSubjectAreaNotSynhronize = LocalizationHolder.rm.GetString("Interfaces.Briefcase_158");
      public static string ImportLogLCLevelName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_159");
      public static string ImportLogLCLevelIcon = LocalizationHolder.rm.GetString("Interfaces.Briefcase_160");
      public static string ImportLogLCLevelSubjectArea = LocalizationHolder.rm.GetString("Interfaces.Briefcase_161");
      public static string ImportLogLCLevelLitera = LocalizationHolder.rm.GetString("Interfaces.Briefcase_162");
      public static string ImportLogLCLevel = LocalizationHolder.rm.GetString("Interfaces.Briefcase_163");
      public static string ImportLogLCLevelNotSynhronize = LocalizationHolder.rm.GetString("Interfaces.Briefcase_164");
      public static string ImportAttributesGroupName = LocalizationHolder.rm.GetString("Interfaces.Briefcase_165");
      public static string ImportAttributesGroupNote = LocalizationHolder.rm.GetString("Interfaces.Briefcase_166");
      public static string ImportAttributesGroupArea = LocalizationHolder.rm.GetString("Interfaces.Briefcase_167");
      public static string ImportAttributesGroupLanguage = LocalizationHolder.rm.GetString("Interfaces.Briefcase_168");
      public static string ImportAttributesGroup = LocalizationHolder.rm.GetString("Interfaces.Briefcase_169");
      public static string ImportAttributesGroupNotSynhronize = LocalizationHolder.rm.GetString("Interfaces.Briefcase_170");
      public static string ImportAttributesGroupParentID = LocalizationHolder.rm.GetString("Interfaces.Briefcase_211");
      public static string ImportLogAttributeTypeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_171");
      public static string ImportLogAttributeTypeNotSynhronized = LocalizationHolder.rm.GetString("Interfaces.Briefcase_172");
      public static string ImportLogAttributeType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_173");
      public static string ImportLogRelationTypeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_174");
      public static string ImportLogRelationTypeNotSynhronized = LocalizationHolder.rm.GetString("Interfaces.Briefcase_175");
      public static string ImportLogRelationType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_176");
      public static string ImportLogObjectTypeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_177");
      public static string ImportLogObjectType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_178");
      public static string ImportLogAttr4ObjectTypeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_179");
      public static string ImportLogAttr4ObjectType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_180");
      public static string ImportLogAttr4RelationTypeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_181");
      public static string ImportLogAttr4RelationType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_182");
      public static string ImportLogLCStepProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_183");
      public static string ImportLogLCStep = LocalizationHolder.rm.GetString("Interfaces.Briefcase_184");
      public static string ImportLogLCSchemeProperties = LocalizationHolder.rm.GetString("Interfaces.Briefcase_185");
      public static string ImportLogLCSchemeNotSynhronized = LocalizationHolder.rm.GetString("Interfaces.Briefcase_186");
      public static string ImportLogLCScheme = LocalizationHolder.rm.GetString("Interfaces.Briefcase_187");
      public static string logObjectUniqueAttribute = LocalizationHolder.rm.GetString("Interfaces.Briefcase_188");
      public static string logObjectChangeObjectType = LocalizationHolder.rm.GetString("Interfaces.Briefcase_189");
      public static string ImportedRelationTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_190");
      public static string ImportedRelationNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_191");
      public static string ImportedObjectNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_192");
      public static string ImportObjectTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_193");
      public static string ImportAttributeTypeNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_194");
      public static string ImportLCStepNotFound = LocalizationHolder.rm.GetString("Interfaces.Briefcase_195");
      public static string logObjectUniqueAttributeNull = LocalizationHolder.rm.GetString("Interfaces.Briefcase_196");
      public static string ImportBlobKeyError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_197");
      public static string ImportAttributeFormulaError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_198");
      public static string ImportAttribute4ObjTypeFormulaError = LocalizationHolder.rm.GetString("Interfaces.Briefcase_199");

      public static bool IsMetadataCategory(int category)
      {
        return Array.IndexOf<int>(BriefcaseConsts.MetadataInfoCategories, category) != -1;
      }

      public static object[] GetRootByMetadataCategory(int category, DataSet metadataDS)
      {
        if (!BriefcaseConsts.IsMetadataCategory(category))
          return (object[]) null;
        ArrayList arrayList = new ArrayList();
        switch (category)
        {
          case 3:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_ATTR_IN_GROUPS"].Select("F_GROUP_ID=-1"))
              arrayList.Add((object) Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]));
            break;
          case 4:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_OBJTYPES_TREE"].Select("F_PARENT_ID=-1"))
              arrayList.Add((object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
            break;
          case 6:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_RELATION_TYPES"].Select())
              arrayList.Add((object) Convert.ToInt32(dataRow["F_RELATION_TYPE"]));
            break;
          case 8:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_LEVELS"].Select())
              arrayList.Add((object) Convert.ToInt32(dataRow["F_LEVEL_ID"]));
            break;
          case 9:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_LANGUAGES"].Select())
              arrayList.Add((object) Convert.ToChar(dataRow["F_LANGUAGE_ID"]));
            break;
          case 11:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_SUBJECT_AREAS"].Select())
              arrayList.Add((object) Convert.ToChar(dataRow["F_AREA_ID"]));
            break;
          case 12:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_ATTR_GROUPS"].Select())
              arrayList.Add((object) Convert.ToInt32(dataRow["F_GROUP_ID"]));
            break;
          case 16 /*0x10*/:
            foreach (DataRow dataRow in metadataDS.Tables["IMS_LC_SCHEMAS"].Select())
              arrayList.Add((object) Convert.ToChar(dataRow["F_SCHEMA_ID"]));
            break;
        }
        return (object[]) arrayList.ToArray(typeof (object));
      }

      public static object GetMetadataExternalId(
        int category,
        object id,
        DataSet metadataDS,
        out DataRow row)
      {
        row = (DataRow) null;
        object metadataExternalId = (object) null;
        if (!BriefcaseConsts.IsMetadataCategory(category))
          return (object) null;
        DataRow[] dataRowArray = (DataRow[]) null;
        switch (category)
        {
          case 3:
            dataRowArray = metadataDS.Tables["IMS_ATTRIBUTES"].Select("F_ATTRIBUTE_ID=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 4:
            dataRowArray = metadataDS.Tables["IMS_OBJECT_TYPES"].Select("F_OBJECT_TYPE=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 6:
            dataRowArray = metadataDS.Tables["IMS_RELATION_TYPES"].Select("F_RELATION_TYPE=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 7:
            dataRowArray = metadataDS.Tables["IMS_LC_STEPS"].Select("F_LC_STEP=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 8:
            dataRowArray = metadataDS.Tables["IMS_LEVELS"].Select("F_LEVEL_ID=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 9:
            dataRowArray = metadataDS.Tables["IMS_LANGUAGES"].Select($"F_LANGUAGE_ID='{id.ToString()}'");
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 11:
            dataRowArray = metadataDS.Tables["IMS_SUBJECT_AREAS"].Select($"F_AREA_ID='{id.ToString()}'");
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 12:
            dataRowArray = metadataDS.Tables["IMS_ATTR_GROUPS"].Select("F_GROUP_ID=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
          case 16 /*0x10*/:
            dataRowArray = metadataDS.Tables["IMS_LC_SCHEMAS"].Select("F_SCHEMA_ID=" + id.ToString());
            if (dataRowArray.Length != 0)
            {
              metadataExternalId = (object) new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]));
              break;
            }
            break;
        }
        if (dataRowArray != null && dataRowArray.Length != 0)
          row = dataRowArray[0];
        return metadataExternalId;
      }

      public static string GetMetadataNameByRow(int category, DataRow row)
      {
        string empty = string.Empty;
        if (!BriefcaseConsts.IsMetadataCategory(category) || row == null)
          return empty;
        switch (category)
        {
          case 3:
            empty = Convert.ToString(row["F_NAME"]);
            break;
          case 4:
            empty = Convert.ToString(row["F_OBJ_NAME"]);
            break;
          case 6:
            empty = Convert.ToString(row["F_DESCRIPTION"]);
            break;
          case 7:
            empty = Convert.ToString(row["F_LC_NAME"]);
            break;
          case 8:
            empty = Convert.ToString(row["F_LEVEL_NAME"]);
            break;
          case 9:
            empty = Convert.ToString(row["F_LANGUAGE_NAME"]);
            break;
          case 11:
            empty = Convert.ToString(row["F_AREA_NAME"]);
            break;
          case 12:
            empty = Convert.ToString(row["F_GROUP_NAME"]);
            break;
          case 16 /*0x10*/:
            empty = Convert.ToString(row["F_NAME"]);
            break;
        }
        return empty;
      }

      public static bool IsElementCategory(int category)
      {
        return Array.IndexOf<int>(BriefcaseConsts.ElementInfoCategories, category) != -1;
      }
    }
}
