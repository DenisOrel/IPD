// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertAttrGUIDs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Expert;

public abstract class ExpertAttrGUIDs
{
  /// <summary>Expert object name</summary>
  public static readonly string objectName = "cad00060-306c-11d8-b4e9-00304f19f545";
  /// <summary>List of used attr GUIDs</summary>
  public static readonly string attrGUIDs = "cad00061-306c-11d8-b4e9-00304f19f545";
  /// <summary>List of used object type GUIDs</summary>
  public static readonly string objTypeGUIDs = "cad00062-306c-11d8-b4e9-00304f19f545";
  /// <summary>List of used object IDs</summary>
  public static readonly string objLinkIDs = "cad00063-306c-11d8-b4e9-00304f19f545";
  /// <summary>BLOB, containing zipped XML for condition</summary>
  public static readonly string condObj = "cad00064-306c-11d8-b4e9-00304f19f545";
  /// <summary>Object data (BLOB), different for various objects</summary>
  public static readonly string objData = "cad00065-306c-11d8-b4e9-00304f19f545";
  /// <summary>Result attribute GUID</summary>
  public static readonly string resAttrGUID = "cad00066-306c-11d8-b4e9-00304f19f545";
  /// <summary>Result object type GUID</summary>
  public static readonly string resObjTypeGUID = "cad00067-306c-11d8-b4e9-00304f19f545";
  /// <summary>Result type (expert system inner result type)</summary>
  public static readonly string resType = "cad00068-306c-11d8-b4e9-00304f19f545";
  /// <summary>Number of expert table entries</summary>
  public static readonly string tableEntries = "cad00069-306c-11d8-b4e9-00304f19f545";
  /// <summary>Number of expert table rows</summary>
  public static readonly string tableRows = "cad0006a-306c-11d8-b4e9-00304f19f545";
  /// <summary>Number of expert table cols</summary>
  public static readonly string tableCols = "cad0006b-306c-11d8-b4e9-00304f19f545";
  /// <summary>Number of expert table layers</summary>
  public static readonly string tableLayers = "cad0006c-306c-11d8-b4e9-00304f19f545";
  /// <summary>Attribute Roles list</summary>
  public static readonly string attrRoles = "cad0006d-306c-11d8-b4e9-00304f19f545";
  /// <summary>List counter 1</summary>
  public static readonly string attrCounter1 = "cad0006e-306c-11d8-b4e9-00304f19f545";
  /// <summary>List counter 2</summary>
  public static readonly string attrCounter2 = "cad0006f-306c-11d8-b4e9-00304f19f545";
  /// <summary>Link to template</summary>
  public static readonly string attTemplateLink = "cad00071-306c-11d8-b4e9-00304f19f545";
  /// <summary>Quantity of objects in the context</summary>
  public static readonly string attrContextCount = "cad00080-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Number of current object in the context (for loops only)
  /// </summary>
  public static readonly string attrCurContextNum = "cad00081-306c-11d8-b4e9-00304f19f545";
  /// <summary>Id of current context</summary>
  public static readonly string attrCurContextId = "cad00082-306c-11d8-b4e9-00304f19f545";
  /// <summary>Template of current doc field</summary>
  public static readonly string attrCurFldTemplate = "cad00083-306c-11d8-b4e9-00304f19f545";
  /// <summary>Id of current doc field</summary>
  public static readonly string attrCurFldId = "cad00084-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID of object type or relation type</summary>
  public static readonly string attrObjRelTypeId = "cad00085-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Type of created object (NOT ES attribute - used to search Refs)
  /// </summary>
  public static readonly string attrCreObjTypeId = "cad00203-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Ссылка на объект, данные которого были записаны в это поле документа.
  /// Это атрибут поля документа (а не объекта) и совпадает с GUID системного атрибута случайно!!!
  /// </summary>
  public static readonly string attrDocFldObject = "cad001a6-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCreationTag = "C_TAG";
  public static readonly string attrFillingTag = "F_TAG";
  /// <summary>
  /// Link to IMBASE object - used to determine how to find reference
  /// </summary>
  public static readonly string attrIMBASECode = "cad00209-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Special attribute "Total number of this part for the product"
  /// </summary>
  public static readonly string attrTotalForProduct = "cad00113-306c-11d8-b4e9-00304f19f545";
  /// <summary>Layers list</summary>
  public static readonly string attrLayers = "cad009e9-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID of generated doc type</summary>
  public static readonly string attrGenDocType = "cad00116-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID of generated doc name</summary>
  public static readonly string attrGenDocName = "cad00117-306c-11d8-b4e9-00304f19f545";
  /// <summary>System attribute 'Name'</summary>
  public static readonly string attrName = "cad00020-306c-11d8-b4e9-00304f19f545";
  /// <summary>Номер объекта</summary>
  public static readonly string attrObjectNum = "cad009e6-306c-11d8-b4e9-00304f19f545";
  /// <summary>Флаги</summary>
  public static readonly string attrFlags = "cad00072-306c-11d8-b4e9-00304f19f545";
  /// <summary>Attributes for generating complects</summary>
  public static readonly string attrEmptyDoc = "cad014ae-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrChecksum = "cad014af-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrLists = "cad003a7-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrListsBefore = "cad014b1-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrFormat = "cad00255-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrScriptRef = "cad001a4-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrSorting = "cad00202-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrObjectForDoc = "cad014b7-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCompTempRef = "cad014b3-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrQuantity = "cad00267-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrLongBlob = "cad01523-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrOwnerLink = "cad001a6-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCurIspId = "cad0158a-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrIspList = "cad0158b-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrIsLink = "cad0158c-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCurIspNum = "cad0158d-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrIspNum = "cad0158e-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCurIspDesign = "cad015bb-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCreateLink = "cad0158f-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrDocLinkType = "cad00bd1-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrDocCompType = "cad00bd0-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrScriptObjTypes = "cadd91fe-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrImbaseFolderKey = "cad0014d-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrServerName = "cad01589-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrUserName = "cadd9366-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrUserId = "cadd9367-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrUserLink = "cadd9388-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrUserRoleLink = "cadd999d-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrGroupIzd = "cad001f9-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrVerSostav = "cad001c2-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrListsTotal = "cad014b0-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrShortDocDesign = "cad005db-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrGroupDoc = "cadd94b7-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrDocSetup = "cadd94bc-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrNeedArchive = "cadd966c-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrArchive = SystemGUIDs.attributeArchive.ToString();
  public static readonly string attrMadeByCoWorker = "cadd970c-306c-11d8-b4e9-00304f19f545";
  public static readonly string signSurnameParm = "cadd96d1-306c-11d8-b4e9-00304f19f545";
  public static readonly string signSurname = "cadd96d2-306c-11d8-b4e9-00304f19f545";
  public static readonly string signParm = "cadd96d3-306c-11d8-b4e9-00304f19f545";
  public static readonly string signValue = "cadd96d4-306c-11d8-b4e9-00304f19f545";
  public static readonly string signDate = "cadd96d5-306c-11d8-b4e9-00304f19f545";
  public static readonly string signDateParm = "cadd96d6-306c-11d8-b4e9-00304f19f545";
  public static readonly string signDocField = "cadd96d7-306c-11d8-b4e9-00304f19f545";
  public static readonly string signRank = "cadd96d8-306c-11d8-b4e9-00304f19f545";
  public static readonly string signID = "cadd96d9-306c-11d8-b4e9-00304f19f545";
  public static readonly string signObjType = "cadd96da-306c-11d8-b4e9-00304f19f545";
  public static readonly string signModDate = "cadd96db-306c-11d8-b4e9-00304f19f545";
  public static readonly string signStatus = "cadd96dc-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrSignDate = "cad014cb-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrModDate = "cad00031-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrModContDate = "cad0013a-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrPrevVersionId = "cadd9717-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrCompListNum = "cadd9978-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrScenarioLink = "cadd9a9f-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrSourceLink = "cadd95b4-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrDopCompTag = "cadd9bab-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrLinkTechcardSetting = "cadd9bad-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrDocOperator = "cadd9bbc-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrNumerationMode = "cadd9669-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrObjTypeGuids = "cad00149-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrObjId = "cad00029-306c-11d8-b4e9-00304f19f545";
}
