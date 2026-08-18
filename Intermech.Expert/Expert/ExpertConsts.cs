// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertConsts
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Storing Expert System consts and converting GUIDs to IDs
/// </summary>
public class ExpertConsts
{
  public static readonly int FormulaVersion = 102;
  public static readonly int TableVersion = 101;
  public static readonly int TraceVersion = 100;
  public static readonly string noNameAttr = "<???>";
  public static readonly string lBrace = "<";
  public static readonly string rBrace = ">";
  public static readonly double Epsilon = 1E-05;
  /// <summary>
  /// Ограничение по умолчанию на размер данных трассировки при показе (KB)
  /// </summary>
  public static readonly int defTraceLimit = 2048 /*0x0800*/;
  public static MeasuredValue OneShtuka = (MeasuredValue) null;
  public static MeasuredValue NolShtuk = (MeasuredValue) null;
  public static readonly string[] AsgnResTypes = new string[8]
  {
    LocalizationHolder.rm.GetString("Expert_71"),
    LocalizationHolder.rm.GetString("Expert_72"),
    LocalizationHolder.rm.GetString("Expert_73"),
    LocalizationHolder.rm.GetString("Expert_74"),
    LocalizationHolder.rm.GetString("Expert_75"),
    LocalizationHolder.rm.GetString("Expert_76"),
    LocalizationHolder.rm.GetString("Expert_77"),
    LocalizationHolder.rm.GetString("Expert_78")
  };
  public int objObject;
  public int objBaseScript;
  public int objBaseFormula;
  public int objFormula;
  public int objTable;
  public int objCond;
  public int objScript;
  public int objFunction;
  public int objAttrRules;
  public int objObjRules;
  public int objExcerpt;
  public int objDocScript;
  public int objTemplate;
  public int objRecalcScript;
  public int objSimpleFormula;
  public int objImbaseFolder;
  public int objImbaseBaseObject;
  public int objESExceprt;
  public int objDocRoot;
  public int objReport;
  public int objComplectTemplate;
  public int objDocTPComplect;
  public int objDocTP;
  public int objComplect;
  public int izdComplect;
  public int objScenario;
  public int objExpScenario;
  public int baseIMBASEObject;
  public int objTechComplectRoot;
  public int objTechTemplate;
  public int objIzdelie;
  public int objSign;
  public int objESFolder;
  public int objVisScheme;
  public int objVisStyles;
  public int objTechDocSettings;
  public int objCommandScript;
  public long measureShtuk;
  public MeasureDescriptor mdShtuk;
  public long measureMinute;
  public int attrObjectName;
  public int attrAttrGUIDs;
  public int attrObjTypeGUIDs;
  public int attrObjLinkIDs;
  public int attrCondObj;
  public int attrObjData;
  public int attrResAttrGUID;
  public int attrResObjTypeGUID;
  public string attrResObjTypeName;
  public int attrResType;
  public int attrTableEntries;
  public int attrTableCols;
  public int attrTableRows;
  public int attrTableLayers;
  public int attrAttrRoles;
  public int attrAttrFile;
  public int attrTemplateLink;
  public int attrContextCount;
  public int attrCurContextNum;
  public int attrCurContextId;
  public int attrCurFldTemplate;
  public int attrCurFldId;
  public int attrObjRelType;
  public int attrCreObjType;
  public int attrDocFldObject;
  public int attrIMBASECode;
  public int attrTotalForProduct;
  public int attrAttrLayers;
  public int attrGenDocType;
  public int attrGenDocName;
  public int _attrObjName;
  public int _attrObjDesign;
  public int attrCount;
  public int attrObjectType;
  public int attrObjectNum;
  public int attrScenarioLink;
  public int attrDopCompTag;
  public int attrDocOperator;
  public int sysAttrObjType;
  public int sysAttrRelType;
  public int tempAttrGroup;
  public int tempAttrObjGroup;
  public int docAttrGroup;
  public int compAttrGroup;
  public int attrEmptyDoc;
  public int attrChecksum;
  public int attrLists;
  public int attrListsBefore;
  public int attrFormat;
  public int attrScriptRef;
  public int attrSorting;
  public int attrObjForDoc;
  public int attrObjCompRef;
  public int attrLongBlob;
  public int attrOwnerLink;
  public int linkTechSostId;
  public int linkSimpleSortId;
  public int linkDocIzd;
  public int linkSostav;
  public int attrCurIspId;
  public int attrIspList;
  public int attrIsLink;
  public int attrCurIspNum;
  public int attrIspNum;
  public int attrCurIspDesign;
  public int attrCreateLink;
  public int attrDocLinkType;
  public int attrDocCompType;
  public int attrScriptObjTypes;
  public int attrFlags;
  public int attrImbaseFolderKey;
  public int attrServerName;
  public int attrUserId;
  public int attrUserName;
  public int attrUserLink;
  public int attrUserRoleLink;
  public int attrGroupIzd;
  public int attrObjectId;
  public int attrProjId;
  public int attrVerSostav;
  public int attrListsTotal;
  public int attrShortDocDesign;
  public int attrGroupDoc;
  public int attrDocSetup;
  public int attrCaption;
  public int attrArchive;
  public int attrNeedArchive;
  public int attrSignDate;
  public int attrModDate;
  public int attrModContDate;
  public int linkSign;
  public int attrPrevVersionId;
  public int attrCreatedByCoWorker;
  public int attrNewPrevVersionId;
  public int attrCompListNum;
  public int attrSourceLink;
  public int attrLinkTechcardSetting;
  public int attrNumerationMode;
  public int attrObjTypeGuids;
  private static ExpertConsts fexpCon = (ExpertConsts) null;

  private ExpertConsts(IUserSession s)
  {
    this.objObject = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertObject);
    this.objBaseScript = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertBaseScript);
    this.objBaseFormula = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertBaseFormula);
    this.objFormula = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertFormula);
    this.objTable = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertTable);
    this.objCond = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertCond);
    this.objScript = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertScript);
    this.objFunction = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertFunction);
    this.objAttrRules = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertAttrRules);
    this.objObjRules = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ExpertObjRules);
    this.objExcerpt = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.Excerpt);
    this.objDocScript = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.DocScript);
    this.objTemplate = ExpertConsts.objTypeGUID2Id(s, "cad00134-306c-11d8-b4e9-00304f19f545");
    this.objRecalcScript = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.RecalcScript);
    this.objSimpleFormula = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.SimpleFormula);
    this.objImbaseFolder = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ImbaseFolder);
    this.objImbaseBaseObject = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ImbaseBaseObject);
    this.objESExceprt = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ESExcept);
    this.objDocRoot = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.DocRoot);
    this.objReport = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ReportObjects);
    this.objComplectTemplate = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.ComplectTemplate);
    this.objDocTPComplect = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.docTPComplect);
    this.objDocTP = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.docTP);
    this.objTechComplectRoot = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.techComplectRoot);
    this.objComplect = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.docComplect);
    this.izdComplect = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.IzdComplectGUID);
    this.objScenario = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objectScenario);
    this.objExpScenario = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objectExpScenario);
    this.objTechTemplate = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.guidObjTechTemplate);
    this.objIzdelie = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objHeadIzdelie);
    this.objSign = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objSignGUID);
    this.objESFolder = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objESFolder);
    this.objVisScheme = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objVisScheme);
    this.objVisStyles = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.objVisStyles);
    this.objTechDocSettings = ExpertConsts.objTypeGUID2IdCheck(s, ExpertObjGUIDs.objTechDocSettings);
    this.objCommandScript = ExpertConsts.objTypeGUID2IdCheck(s, ExpertObjGUIDs.CommandScript);
    this.baseIMBASEObject = ExpertConsts.objTypeGUID2Id(s, ExpertObjGUIDs.BaseImbaseObject);
    this.measureShtuk = s.GetObject(new Guid("cad002e8-306c-11d8-b4e9-00304f19f545")).ObjectID;
    this.mdShtuk = MeasureHelper.FindDescriptor(this.measureShtuk);
    this.measureMinute = s.GetObject(new Guid(ExpertObjGUIDs.objectMinute)).ObjectID;
    this.attrObjectName = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.objectName);
    this.attrAttrGUIDs = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrGUIDs);
    this.attrObjTypeGUIDs = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.objTypeGUIDs);
    this.attrObjLinkIDs = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.objLinkIDs);
    this.attrCondObj = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.condObj);
    this.attrObjData = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.objData);
    this.attrResAttrGUID = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.resAttrGUID);
    IDBAttributeType attributeType = s.GetAttributeType(new Guid(ExpertAttrGUIDs.resObjTypeGUID), false);
    if (attributeType != null)
    {
      this.attrResObjTypeGUID = attributeType.AttributeID;
      this.attrResObjTypeName = attributeType.Name;
    }
    this.attrResType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.resType);
    this.attrTableEntries = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.tableEntries);
    this.attrTableCols = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.tableCols);
    this.attrTableRows = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.tableRows);
    this.attrTableLayers = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.tableLayers);
    this.attrAttrRoles = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrRoles);
    this.attrAttrFile = ExpertConsts.attrTypeGUID2Id(s, "cad0004b-306c-11d8-b4e9-00304f19f545");
    this.attrTemplateLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attTemplateLink);
    this.attrContextCount = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrContextCount);
    this.attrCurContextNum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurContextNum);
    this.attrCurContextId = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurContextId);
    this.attrCurFldTemplate = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurFldTemplate);
    this.attrCurFldId = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurFldId);
    this.attrObjRelType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrObjRelTypeId);
    this.attrCreObjType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCreObjTypeId);
    this.attrDocFldObject = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDocFldObject);
    this.attrIMBASECode = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrIMBASECode);
    this.attrTotalForProduct = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrTotalForProduct);
    this.attrAttrLayers = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrLayers);
    this.attrGenDocType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrGenDocType);
    this.attrGenDocName = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrGenDocName);
    this._attrObjName = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrName);
    this._attrObjDesign = ExpertConsts.attrTypeGUID2Id(s, "cad0001f-306c-11d8-b4e9-00304f19f545");
    this.attrCompListNum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCompListNum);
    this.attrScenarioLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrScenarioLink);
    this.attrLinkTechcardSetting = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrLinkTechcardSetting);
    this.attrCount = ExpertConsts.attrTypeGUID2Id(s, "cad00267-306c-11d8-b4e9-00304f19f545");
    this.linkTechSostId = ExpertConsts.linkTypeGUID2Id(s, ExpertObjGUIDs.linkTechSostav);
    this.linkSimpleSortId = ExpertConsts.linkTypeGUID2Id(s, ExpertObjGUIDs.linkSimpleSort);
    this.linkDocIzd = ExpertConsts.linkTypeGUID2Id(s, ExpertObjGUIDs.linkDocForIzd);
    this.linkSostav = ExpertConsts.linkTypeGUID2Id(s, ExpertObjGUIDs.linkSostav);
    this.linkSign = ExpertConsts.linkTypeGUID2Id(s, ExpertObjGUIDs.linkSignGUID);
    this.attrObjectType = ExpertConsts.attrTypeGUID2Id(s, "cad0002e-306c-11d8-b4e9-00304f19f545");
    this.attrObjectNum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrObjectNum);
    this.attrLongBlob = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrLongBlob);
    this.attrOwnerLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrOwnerLink);
    this.attrEmptyDoc = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrEmptyDoc);
    this.attrChecksum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrChecksum);
    this.attrLists = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrLists);
    this.attrListsBefore = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrListsBefore);
    this.attrScriptRef = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrScriptRef);
    this.attrSorting = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrSorting);
    this.attrObjForDoc = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrObjectForDoc);
    this.attrObjCompRef = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCompTempRef);
    this.attrDocOperator = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDocOperator);
    this.attrCurIspId = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurIspId);
    this.attrIspList = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrIspList);
    this.attrIsLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrIsLink);
    this.attrCurIspNum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurIspNum);
    this.attrIspNum = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrIspNum);
    this.attrCreateLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCreateLink);
    this.attrDocLinkType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDocLinkType);
    this.attrScriptObjTypes = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrScriptObjTypes);
    this.attrCurIspDesign = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrCurIspDesign);
    this.attrFlags = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrFlags);
    this.attrFormat = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrFormat);
    this.attrImbaseFolderKey = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrImbaseFolderKey);
    this.attrServerName = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrServerName);
    this.attrUserName = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrUserName);
    this.attrUserId = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrUserId);
    this.attrUserLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrUserLink);
    this.attrUserRoleLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrUserRoleLink);
    this.attrGroupIzd = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrGroupIzd);
    this.attrVerSostav = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrVerSostav);
    this.attrProjId = ExpertConsts.attrTypeGUID2Id(s, "cad00034-306c-11d8-b4e9-00304f19f545");
    this.attrListsTotal = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrListsTotal);
    this.attrShortDocDesign = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrShortDocDesign);
    this.attrGroupDoc = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrGroupDoc);
    this.attrDocSetup = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDocSetup);
    this.attrCaption = ExpertConsts.attrTypeGUID2Id(s, "cad00047-306c-11d8-b4e9-00304f19f545");
    this.attrArchive = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrArchive);
    this.attrNeedArchive = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrNeedArchive);
    this.attrSignDate = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrSignDate);
    this.attrModDate = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrModDate);
    this.attrModContDate = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrModContDate);
    this.attrPrevVersionId = ExpertConsts.attrTypeGUID2Id(s, "cadd9597-306c-11d8-b4e9-00304f19f545");
    this.attrNewPrevVersionId = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrPrevVersionId);
    this.attrCreatedByCoWorker = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrMadeByCoWorker);
    this.attrSourceLink = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrSourceLink);
    this.attrDopCompTag = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDopCompTag);
    this.attrDocCompType = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrDocCompType);
    this.attrNumerationMode = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrNumerationMode);
    this.attrObjTypeGuids = ExpertConsts.attrTypeGUID2Id(s, ExpertAttrGUIDs.attrObjTypeGuids);
    this.attrObjectId = MetaDataHelper.GetAttributeTypeID(ExpertAttrGUIDs.attrObjId);
    this.sysAttrObjType = s.GetAttributeType(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545")).AttributeID;
    this.sysAttrRelType = s.GetAttributeType(new Guid("cad00036-306c-11d8-b4e9-00304f19f545")).AttributeID;
    this.tempAttrGroup = s.GetAttributesGroup(new Guid(ExpertObjGUIDs.TempAttrGroup)).GroupID;
    this.tempAttrObjGroup = s.GetAttributesGroup(new Guid(ExpertObjGUIDs.TempAttrObjGroup)).GroupID;
    IDBAttributesGroup attributesGroup1 = s.GetAttributesGroup(new Guid(ExpertObjGUIDs.DocAttrGroup), false);
    if (attributesGroup1 != null)
      this.docAttrGroup = attributesGroup1.GroupID;
    IDBAttributesGroup attributesGroup2 = s.GetAttributesGroup(new Guid(ExpertObjGUIDs.CompAttrGroup), false);
    if (attributesGroup2 != null)
      this.compAttrGroup = attributesGroup2.GroupID;
    ExpertConsts.OneShtuka = new MeasuredValue(1.0, this.measureShtuk);
    ExpertConsts.NolShtuk = new MeasuredValue(0.0, this.measureShtuk);
  }

  private ExpertConsts(DataTable dt1, DataTable dt2)
  {
    this.objObject = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertObject);
    this.objBaseScript = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertBaseScript);
    this.objBaseFormula = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertBaseFormula);
    this.objFormula = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertFormula);
    this.objTable = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertTable);
    this.objCond = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertCond);
    this.objScript = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertScript);
    this.objFunction = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertFunction);
    this.objAttrRules = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertAttrRules);
    this.objObjRules = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ExpertObjRules);
    this.objExcerpt = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.Excerpt);
    this.objDocScript = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.DocScript);
    this.objTemplate = ExpertConsts.objTypeGUID2Id(dt1, "cad00134-306c-11d8-b4e9-00304f19f545");
    this.objRecalcScript = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.RecalcScript);
    this.objSimpleFormula = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.SimpleFormula);
    this.objImbaseFolder = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ImbaseFolder);
    this.objImbaseBaseObject = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ImbaseBaseObject);
    this.objESExceprt = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ESExcept);
    this.objDocRoot = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.DocRoot);
    this.objTechComplectRoot = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.techComplectRoot);
    this.objReport = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ReportObjects);
    this.objComplectTemplate = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.ComplectTemplate);
    this.objDocTPComplect = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.docTPComplect);
    this.objDocTP = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.docTP);
    this.objComplect = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.docComplect);
    this.izdComplect = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.IzdComplectGUID);
    this.baseIMBASEObject = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.BaseImbaseObject);
    this.objScenario = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objectScenario);
    this.objExpScenario = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objectExpScenario);
    this.objTechTemplate = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.guidObjTechTemplate);
    this.objIzdelie = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objHeadIzdelie);
    this.objSign = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objSignGUID);
    this.objESFolder = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objESFolder);
    this.objVisScheme = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objVisScheme);
    this.objVisStyles = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objVisStyles);
    this.objTechDocSettings = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.objTechDocSettings);
    this.objCommandScript = ExpertConsts.objTypeGUID2Id(dt1, ExpertObjGUIDs.CommandScript);
    this.attrObjectName = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.objectName);
    this.attrAttrGUIDs = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrGUIDs);
    this.attrObjTypeGUIDs = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.objTypeGUIDs);
    this.attrObjLinkIDs = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.objLinkIDs);
    this.attrCondObj = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.condObj);
    this.attrObjData = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.objData);
    this.attrResAttrGUID = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.resAttrGUID);
    this.attrResObjTypeGUID = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.resObjTypeGUID);
    this.attrResType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.resType);
    this.attrTableEntries = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.tableEntries);
    this.attrTableCols = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.tableCols);
    this.attrTableRows = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.tableRows);
    this.attrTableLayers = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.tableLayers);
    this.attrAttrRoles = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrRoles);
    this.attrAttrFile = ExpertConsts.attrTypeGUID2Id(dt2, "cad0004b-306c-11d8-b4e9-00304f19f545");
    this.attrTemplateLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attTemplateLink);
    this.attrContextCount = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrContextCount);
    this.attrCurContextNum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurContextNum);
    this.attrCurContextId = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurContextId);
    this.attrCurFldTemplate = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurFldTemplate);
    this.attrCurFldId = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurFldId);
    this.attrObjRelType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrObjRelTypeId);
    this.attrCreObjType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCreObjTypeId);
    this.attrDocFldObject = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDocFldObject);
    this.attrIMBASECode = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrIMBASECode);
    this.attrTotalForProduct = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrTotalForProduct);
    this.attrGenDocType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrGenDocType);
    this.attrGenDocName = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrGenDocName);
    this._attrObjName = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrName);
    this._attrObjDesign = ExpertConsts.attrTypeGUID2Id(dt2, "cad0001f-306c-11d8-b4e9-00304f19f545");
    this.attrCount = ExpertConsts.attrTypeGUID2Id(dt2, "cad00267-306c-11d8-b4e9-00304f19f545");
    this.attrObjectType = ExpertConsts.attrTypeGUID2Id(dt2, "cad0002e-306c-11d8-b4e9-00304f19f545");
    this.attrObjectNum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrObjectNum);
    this.attrLongBlob = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrLongBlob);
    this.attrOwnerLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrOwnerLink);
    this.attrCompListNum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCompListNum);
    this.attrScenarioLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrScenarioLink);
    this.attrObjectId = MetaDataHelper.GetAttributeTypeID(ExpertAttrGUIDs.attrObjId);
    this.attrLinkTechcardSetting = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrLinkTechcardSetting);
    this.attrSignDate = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrSignDate);
    this.attrModDate = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrModDate);
    this.attrModContDate = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrModContDate);
    this.linkTechSostId = ExpertConsts.linkTypeGUID2Id(dt2, ExpertObjGUIDs.linkTechSostav);
    this.linkSimpleSortId = ExpertConsts.linkTypeGUID2Id(dt2, ExpertObjGUIDs.linkSimpleSort);
    this.linkDocIzd = ExpertConsts.linkTypeGUID2Id(dt2, ExpertObjGUIDs.linkDocForIzd);
    this.linkSostav = ExpertConsts.linkTypeGUID2Id(dt2, ExpertObjGUIDs.linkSostav);
    this.linkSign = ExpertConsts.linkTypeGUID2Id(dt2, ExpertObjGUIDs.linkSignGUID);
    this.attrEmptyDoc = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrEmptyDoc);
    this.attrChecksum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrChecksum);
    this.attrLists = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrLists);
    this.attrListsBefore = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrListsBefore);
    this.attrScriptRef = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrScriptRef);
    this.attrSorting = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrSorting);
    this.attrObjForDoc = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrObjectForDoc);
    this.attrObjCompRef = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCompTempRef);
    this.attrCurIspId = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurIspId);
    this.attrIspList = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrIspList);
    this.attrIsLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrIsLink);
    this.attrCurIspNum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurIspNum);
    this.attrIspNum = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrIspNum);
    this.attrCreateLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCreateLink);
    this.attrDocLinkType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDocLinkType);
    this.attrDocCompType = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDocCompType);
    this.attrCurIspDesign = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrCurIspDesign);
    this.attrScriptObjTypes = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrScriptObjTypes);
    this.attrFlags = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrFlags);
    this.attrFormat = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrFormat);
    this.attrImbaseFolderKey = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrImbaseFolderKey);
    this.attrServerName = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrServerName);
    this.attrUserName = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrUserName);
    this.attrUserId = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrUserId);
    this.attrUserLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrUserLink);
    this.attrUserRoleLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrUserRoleLink);
    this.attrGroupIzd = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrGroupIzd);
    this.attrVerSostav = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrVerSostav);
    this.attrListsTotal = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrListsTotal);
    this.attrSourceLink = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrSourceLink);
    this.attrDopCompTag = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDopCompTag);
    this.attrDocOperator = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDocOperator);
    this.attrArchive = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrArchive);
    this.attrNeedArchive = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrNeedArchive);
    this.attrShortDocDesign = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrShortDocDesign);
    this.attrGroupDoc = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrGroupDoc);
    this.attrDocSetup = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrDocSetup);
    this.attrCaption = ExpertConsts.attrTypeGUID2Id(dt2, "cad00047-306c-11d8-b4e9-00304f19f545");
    this.attrPrevVersionId = ExpertConsts.attrTypeGUID2Id(dt2, "cadd9597-306c-11d8-b4e9-00304f19f545");
    this.attrCreatedByCoWorker = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrMadeByCoWorker);
    this.attrNewPrevVersionId = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrPrevVersionId);
    this.attrNumerationMode = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrNumerationMode);
    this.attrObjTypeGuids = ExpertConsts.attrTypeGUID2Id(dt2, ExpertAttrGUIDs.attrObjTypeGuids);
  }

  public static void Init(IUserSession s)
  {
    if (ExpertConsts.fexpCon != null)
      return;
    ExpertConsts.fexpCon = new ExpertConsts(s);
  }

  public static void Init(DataTable dt1, DataTable dt2)
  {
    if (ExpertConsts.fexpCon != null)
      return;
    ExpertConsts.fexpCon = new ExpertConsts(dt1, dt2);
  }

  public static ExpertConsts Consts => ExpertConsts.fexpCon;

  public static int objTypeGUID2Id(IUserSession s, string objTypeGUID)
  {
    return s.GetObjectType(new Guid(objTypeGUID)).ObjectType;
  }

  public static int objTypeGUID2IdCheck(IUserSession s, string objTypeGUID)
  {
    IDBObjectType objectType = s.GetObjectType(new Guid(objTypeGUID), false);
    return objectType == null ? -1 : objectType.ObjectType;
  }

  public static int attrTypeGUID2Id(IUserSession s, string attrTypeGUID)
  {
    return s.GetAttributeType(new Guid(attrTypeGUID)).AttributeID;
  }

  public static int linkTypeGUID2Id(IUserSession s, string linkTypeGUID)
  {
    return s.GetRelationType(new Guid(linkTypeGUID)).RelationType;
  }

  public static int objTypeGUID2Id(DataTable dt, string objTypeGUID)
  {
    string filterExpression = $"F_GUID = '{objTypeGUID}'";
    DataRow[] dataRowArray = dt.Select(filterExpression);
    return dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]) : -1;
  }

  public static int attrTypeGUID2Id(DataTable dt, string attrTypeGUID)
  {
    DataRow[] dataRowArray = dt.Select($"F_GUID = '{attrTypeGUID}'");
    return dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]) : -1;
  }

  public static int linkTypeGUID2Id(DataTable dt, string linkTypeGUID)
  {
    DataRow[] dataRowArray = dt.Select($"F_GUID = '{linkTypeGUID}'");
    return dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_RELATION_ID"]) : -1;
  }

  public static string AttrRole2Str(AttributeRoles ar)
  {
    switch (ar)
    {
      case AttributeRoles.argVert:
        return LocalizationHolder.rm.GetString("Expert_80");
      case AttributeRoles.argHorz:
        return LocalizationHolder.rm.GetString("Expert_79");
      case AttributeRoles.argResult:
        return LocalizationHolder.rm.GetString("Expert_81");
      case AttributeRoles.Result:
        return LocalizationHolder.rm.GetString("Expert_82");
      default:
        return "";
    }
  }

  public static AttributeRoles Str2AttrRole(string st)
  {
    if (st == LocalizationHolder.rm.GetString("Expert_83") || st == "")
      return AttributeRoles.argHorz;
    if (st == LocalizationHolder.rm.GetString("Expert_84"))
      return AttributeRoles.argVert;
    if (st == LocalizationHolder.rm.GetString("Expert_85"))
      return AttributeRoles.argResult;
    if (st == LocalizationHolder.rm.GetString("Expert_86"))
      return AttributeRoles.Result;
    throw new Exception(LocalizationHolder.rm.GetString("Expert_87") + st);
  }

  public static bool UsedIMCode(IUserSession ius, int objTypeId, int attrId)
  {
    if (attrId == ExpertConsts.Consts.attrIMBASECode)
      return true;
    if (objTypeId != -1)
    {
      IDBAttributeType4 attributeById = ius.GetObjectType(objTypeId).Attributes.GetAttributeByID(attrId);
      if (attributeById != null && attributeById.MasterAttributeID == attrId)
        return true;
    }
    return ius.GetAttributeType(attrId).MasterAttributeID == attrId;
  }
}
