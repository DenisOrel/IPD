// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECO
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.ECO.Client;

public class ECO
{
  private Guid ecoDBObjectGuid = Guid.Empty;
  private long ecoObjectID = -1;
  public long linkedContextNo;
  public long ecoVersion;
  public static readonly long curVersion = 100;
  public RevType revType;
  public string litera = "";
  public string reasonCode = "";
  public List<PendingLink> objLinks = new List<PendingLink>();
  public List<PendingLink> hiddenLinks = new List<PendingLink>();
  public string SDAC = "";
  public List<long> newVers = new List<long>();
  public DateTime changeTermStart = DateTime.MinValue;
  public DateTime changeTermEnd = DateTime.MinValue;
  public DateTime changeTermPi = DateTime.MinValue;
  public HashSet<long> OrgSet = new HashSet<long>();
  private ImDocument documentECO;
  internal TableElement ecoMainTable;
  internal TextData ecoDocRevision;
  public static string invNumTemplate = (string) null;
  public Dictionary<long, Guid> objGuids = new Dictionary<long, Guid>();
  public Dictionary<Guid, int> guidIndexes = new Dictionary<Guid, int>();
  public static readonly string fldWorkspace = LocalizationHolder.rm.GetString("ECO.Client_1");
  public static readonly string fldChange = LocalizationHolder.rm.GetString("ECO.Client_2");
  public static readonly string fldChangeHeader = LocalizationHolder.rm.GetString("ECO.Client_3");
  public static readonly string fldChangeHeader2 = LocalizationHolder.rm.GetString("ECO.Client_4");
  public static readonly string fldVar1 = LocalizationHolder.rm.GetString("ECO.Client_5");
  public static readonly string fldVar2 = LocalizationHolder.rm.GetString("ECO.Client_6");
  public static readonly string fldVar3 = LocalizationHolder.rm.GetString("ECO.Client_7");
  public static readonly string fldVar4 = LocalizationHolder.rm.GetString("ECO.Client_8");
  public static readonly string fldVar5 = LocalizationHolder.rm.GetString("ECO.Client_9");
  public static readonly string fldVar6 = LocalizationHolder.rm.GetString("ECO.Client_10");
  public static readonly string fldDesign = LocalizationHolder.rm.GetString("ECO.Client_11");
  public static readonly string fldOnlyDesign = LocalizationHolder.rm.GetString("ECO.Client_12");
  public static readonly string fldChangeNumber = LocalizationHolder.rm.GetString("ECO.Client_13");
  public static readonly string fldString = LocalizationHolder.rm.GetString("ECO.Client_14");
  public static readonly string fldSendTo = LocalizationHolder.rm.GetString("ECO.Client_250");
  public static readonly string objectsAttr = "_OBJECTS_";
  public static readonly string schemeIdAttr = "_SCHEME_";
  public static readonly string versionIdAttr = "_VERSION_";
  public static readonly string replacedPIAttr = "_PI_ID_";
  public static readonly string hiddenId = "_ID_";
  public static readonly string hiddenValue = "_VALUE_";
  public static readonly string textAttr = "_TEXT_";
  public static readonly string textSaveAttr = "_SAVE_TEXT_";
  public static readonly string idReason = "I109";
  public static readonly string idShifr = "I110";
  public static readonly string idRevision = "I108";
  public static readonly string idRevDesignation = "I107";
  public static readonly string idCreationDate = "I105";
  public static readonly string idStartChangeTerm = "I106";
  public static readonly string idUsability = "I101";
  public static readonly string idFldChangeNo = "IN1";
  public static readonly string idFldDesign = "IN2";
  public static readonly string idZadel1 = "I104";
  public static readonly string idZadel2 = "I404";
  public static readonly string idSendTo = "I102";
  public static readonly string idUkazVnedrenie = "I111";
  public static readonly string fldPict = "_PICT_";
  public static readonly string seeBelow = LocalizationHolder.rm.GetString("ECO.Client_15");
  public static readonly string specTextTable = "S_TEXT";
  public static readonly string specTextFld = "T_FLD";
  public static readonly string fldColCaption = LocalizationHolder.rm.GetString("ECO.Client_16");
  public static readonly string strReplaceDocs = LocalizationHolder.rm.GetString("ECO.Client_17");
  public static readonly string strLitera = LocalizationHolder.rm.GetString("ECO.Client_18");
  public static readonly string idSpecText = "IR1";
  public static readonly string idSpecTextFld = "TR1";
  public static readonly string noChangeNumber = "―";
  public static readonly string fldVar7 = LocalizationHolder.rm.GetString("ECO.Client_242");
  public static readonly string fldVar8 = LocalizationHolder.rm.GetString("ECO.Client_243");
  public static readonly string fldTable = LocalizationHolder.rm.GetString("ECO.Client_414");
  public static readonly string cmdInsertTable = LocalizationHolder.rm.GetString("ECO.Client_415");
  public static readonly string idEndChangeTerm = "I121";
  public static readonly string idPIDesignation = "I122";
  public static readonly string idPITerm = "I123";
  public static readonly string idHiddenLabel = "I124";
  public static readonly string idDIDesignation = "I125";
  public static readonly string altPrimaryHeader = LocalizationHolder.rm.GetString("ECO.Client_345");
  public static readonly string altTable = LocalizationHolder.rm.GetString("ECO.Client_346");
  public static readonly string altHeaderCaption = LocalizationHolder.rm.GetString("ECO.Client_347");
  public static readonly string altString = LocalizationHolder.rm.GetString("ECO.Client_348");
  public static readonly string altDesignation2 = LocalizationHolder.rm.GetString("ECO.Client_349");
  public static readonly string altAnnul = LocalizationHolder.rm.GetString("ECO.Client_350");
  public static readonly string altReplace = LocalizationHolder.rm.GetString("ECO.Client_351");
  public static readonly string altCreate = LocalizationHolder.rm.GetString("ECO.Client_352");
  public static readonly string altDocuments = LocalizationHolder.rm.GetString("ECO.Client_353");

  public bool HasTerm() => this.changeTermEnd != DateTime.MinValue;

  public bool AllGoalsChange()
  {
    foreach (PendingLink objLink in this.objLinks)
    {
      if (objLink.ecoGoal != ECOGoal.Change)
        return false;
    }
    return true;
  }

  public int HiddenObjIdIndex(long objId)
  {
    for (int index = 0; index < this.hiddenLinks.Count; ++index)
    {
      if (Math.Abs(this.hiddenLinks[index].verID) == Math.Abs(objId))
        return index;
    }
    return -1;
  }

  public int ObjIdIndex(long objId)
  {
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      if (Math.Abs(this.objLinks[index].verID) == Math.Abs(objId))
        return index;
    }
    return -1;
  }

  public int ObjGuidIndex(Guid g)
  {
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      if (this.objLinks[index].verGuid.Equals(g))
        return index;
    }
    return -1;
  }

  public PendingLink FindPendingLink(long objId)
  {
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      if (Math.Abs(this.objLinks[index].verID) == Math.Abs(objId))
        return this.objLinks[index];
    }
    return (PendingLink) null;
  }

  public PendingLink FindAnyLink(long objId)
  {
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      if (Math.Abs(this.objLinks[index].verID) == Math.Abs(objId))
        return this.objLinks[index];
    }
    for (int index = 0; index < this.hiddenLinks.Count; ++index)
    {
      if (Math.Abs(this.hiddenLinks[index].verID) == Math.Abs(objId))
        return this.hiddenLinks[index];
    }
    return (PendingLink) null;
  }

  public PendingLink FindPendingLink(Guid g)
  {
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      if (this.objLinks[index].verGuid.Equals(g))
        return this.objLinks[index];
    }
    return (PendingLink) null;
  }

  public bool HasThisObjectVersion(IUserSession ius, IEnumerable<PendingLink> links, long objId)
  {
    this.UpdateIDs(ius, links);
    long objectFId = ius.GetObjectF_ID(objId);
    switch (objectFId)
    {
      case -1:
      case 0:
        return false;
      default:
        foreach (PendingLink link in links)
        {
          if (link.ID == objectFId)
            return true;
        }
        return false;
    }
  }

  public void UpdateIDs(IUserSession ius, IEnumerable<PendingLink> links)
  {
    foreach (PendingLink link in links)
    {
      if (link.ID == 0L || link.ID == -1L)
        link.ID = ius.GetObjectF_ID(link.verID);
    }
  }

  public bool HasThisObjectVersion(IUserSession ius, long objId)
  {
    return this.HasThisObjectVersion(ius, (IEnumerable<PendingLink>) this.objLinks, objId);
  }

  public void UpdateIDs(IUserSession ius)
  {
    this.UpdateIDs(ius, (IEnumerable<PendingLink>) this.objLinks);
  }

  public Guid _GuidById(long objId)
  {
    Guid guid = Guid.Empty;
    PendingLink pendingLink = this.FindPendingLink(objId);
    if (pendingLink != null)
      guid = pendingLink.verGuid;
    return guid;
  }

  public List<long> ObjIdList()
  {
    List<long> longList = new List<long>();
    foreach (PendingLink objLink in this.objLinks)
    {
      if (!longList.Contains(objLink.verID))
        longList.Add(objLink.verID);
      if (objLink.auxObjects != null)
      {
        foreach (ObjInfo auxObject in objLink.auxObjects)
        {
          if (!longList.Contains(objLink.verID))
            longList.Add(auxObject.verId);
        }
      }
    }
    return longList;
  }

  public List<long> ObjIdPrimaryList()
  {
    List<long> longList = new List<long>();
    foreach (PendingLink objLink in this.objLinks)
      longList.Add(objLink.verID);
    return longList;
  }

  public List<HidingType> ObjHideStatusList()
  {
    List<HidingType> hidingTypeList = new List<HidingType>();
    foreach (PendingLink objLink in this.objLinks)
      hidingTypeList.Add(objLink.hideType);
    return hidingTypeList;
  }

  public List<long> AnnulIdList()
  {
    List<long> longList = new List<long>();
    foreach (PendingLink objLink in this.objLinks)
    {
      if (objLink.ecoGoal == ECOGoal.Annul)
        longList.Add(objLink.verID);
    }
    return longList;
  }

  public List<string> ChangeNoList(List<long> objIdList)
  {
    List<string> stringList = new List<string>(objIdList.Count);
    for (int index = 0; index < objIdList.Count; ++index)
      stringList.Add("");
    foreach (PendingLink objLink in this.objLinks)
    {
      int index = objIdList.IndexOf(objLink.verID);
      if (index >= 0)
        stringList[index] = objLink.verStr;
    }
    return stringList;
  }

  public static bool SameChangeNums(List<string> l)
  {
    if (l.Count == 0)
      return true;
    string str = l[0];
    for (int index = 1; index < l.Count; ++index)
    {
      if (l[index] != str)
        return false;
    }
    return true;
  }

  public Guid EcoDBObjectGuid
  {
    get => this.ecoDBObjectGuid;
    set => this.ecoDBObjectGuid = value;
  }

  public bool IdLists() => this.ecoVersion == 0L;

  public long EcoObjectID
  {
    get => this.ecoObjectID;
    set => this.ecoObjectID = value;
  }

  public ImDocument DocumentECO
  {
    get => this.documentECO;
    set => this.documentECO = value;
  }

  public ECO(ImDocument documentECO, long ecoObjID, Guid dbObjectGuid, RevType rType)
  {
    this.documentECO = documentECO;
    this.EcoObjectID = ecoObjID;
    if (rType != RevType.CJ)
      this.CheckEcoMainTable();
    this.ecoDBObjectGuid = dbObjectGuid;
    this.revType = rType;
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    this.SetInvNumTemplate(plugin);
  }

  public void SetInvNumTemplate(ECOPlugin ep)
  {
    if (ep.eps.Current.PlaceInvNum || ep.eps.Current.ReplaceEmptyDesignByTemplate)
    {
      if (!(ep.eps.Current.InvNumAttr == ""))
      {
        try
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str = ep.eps.Current.InvNumAttr;
          do
          {
            int num1 = str.IndexOf('[');
            if (num1 >= 0)
            {
              int num2 = str.IndexOf(']', num1);
              if (num2 >= 0)
              {
                string attributeID = str.Substring(num1 + 1, num2 - num1 - 1);
                try
                {
                  int attributeId = MetaDataHelper.GetAttributeID((object) attributeID);
                  stringBuilder.Append($"{str.Substring(0, num1)}[{Convert.ToString(attributeId)}]");
                }
                catch
                {
                  stringBuilder.Append(str.Substring(0, num1));
                }
                str = str.Substring(num2 + 1);
              }
              else
                break;
            }
            else
              break;
          }
          while (str != "");
          if (str != "")
            stringBuilder.Append(str);
          Intermech.ECO.Client.ECO.invNumTemplate = stringBuilder.ToString();
          return;
        }
        catch
        {
          Intermech.ECO.Client.ECO.invNumTemplate = (string) null;
          return;
        }
      }
    }
    Intermech.ECO.Client.ECO.invNumTemplate = (string) null;
  }

  public void GetECOData()
  {
  }

  public static int RevTypeToObjType(RevType rt)
  {
    switch (rt)
    {
      case RevType.II:
        return RevHelper.idObj_II;
      case RevType.PI:
        return RevHelper.idObj_PI;
      case RevType.PR:
        return RevHelper.idObj_PR;
      case RevType.DI:
        return RevHelper.idObj_DI;
      case RevType.DPI:
        return RevHelper.idObj_DPI;
      case RevType.CJ:
        return RevHelper.idChangeJournal;
      default:
        return 0;
    }
  }

  public bool UpdateRevType(int revObjType)
  {
    RevType revType = this.revType;
    if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II).Contains(revObjType))
      revType = RevType.II;
    else if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PI).Contains(revObjType))
      revType = RevType.PI;
    else if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PR).Contains(revObjType))
      revType = RevType.PR;
    else if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_DI).Contains(revObjType))
      revType = RevType.DI;
    else if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_DPI).Contains(revObjType))
      revType = RevType.DPI;
    if (revType == this.revType)
      return false;
    this.revType = revType;
    return true;
  }

  public List<IdLinkPair> CopyLinksFrom(long otherECO_ID, bool markLinks, int newGoal = -1)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService))
        return (List<IdLinkPair>) null;
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
      relationCollection.LocalTypesMode = true;
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[4]
      {
        (object) -26,
        (object) -22,
        (object) -2,
        (object) -21
      }), otherECO_ID);
      List<IdLinkPair> idLinkPairList = new List<IdLinkPair>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string g = dataTable.Rows[index][0].ToString();
        Convert.ToInt64(dataTable.Rows[index][1]);
        long int64_1 = Convert.ToInt64(dataTable.Rows[index][2]);
        long int64_2 = Convert.ToInt64(dataTable.Rows[index][3]);
        IDBRelation relation = sessionKeeper.Session.GetRelation(new Guid(g), int64_2, false);
        if (relation != null)
        {
          AttributeValues[] attributesValues = relation.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption);
          object[] valuesById = relation.GetValuesByID(RevHelper.idAttrFutureLC, false);
          int int32 = valuesById != null ? Convert.ToInt32(valuesById[0]) : 0;
          int goal = newGoal;
          if (newGoal == -1)
          {
            IDBAttribute byId = relation.Attributes.FindByID(RevHelper.idAttrIncludeGoal);
            if (byId != null)
              goal = (int) byId.AsInteger;
          }
          IDBRelation dbRelation = (IDBRelation) null;
          customService.StartDisableAddContext(this.EcoObjectID);
          try
          {
            using (new ECOLinkCreator(sessionKeeper.Session, this.EcoObjectID, int64_1))
              dbRelation = relationCollection.Create(this.EcoObjectID, int64_1, attributesValues);
          }
          finally
          {
            customService.StopDisableAddContext(this.EcoObjectID);
          }
          if (dbRelation != null)
          {
            idLinkPairList.Add(new IdLinkPair(int64_1, dbRelation.RelationID, goal));
            if (markLinks)
              dbRelation.Attributes.AddAttribute(RevHelper.idAttrFlags, false).AsInteger = 1L;
            dbRelation.Attributes.AddAttribute(RevHelper.idAttrIncludeGoal, false).AsInteger = (long) goal;
            if (goal == 1)
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(int64_1, false);
              if (dbObject != null)
              {
                IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject.ObjectType);
                dbRelation.Attributes.AddAttribute(RevHelper.idAttrFutureLC, false).AsInteger = (long) this.GetNewAnnulLCStep(objectType.SchemaID);
              }
            }
            else if (int32 != 0)
            {
              int futureLcStepId = ECOPlugin.GetFutureLCStepId(sessionKeeper.Session, int32);
              IDBAttribute dbAttribute = dbRelation.Attributes.AddAttribute(RevHelper.idAttrFutureLC, false);
              if (dbAttribute != null && dbAttribute.Value == DBNull.Value)
                dbAttribute.AsInteger = (long) futureLcStepId;
            }
          }
        }
      }
      return idLinkPairList;
    }
  }

  private int GetNewAnnulLCStep(int schemeId)
  {
    DataTable dt1;
    ECOPlugin.GetSchemeData(schemeId, out dt1, out DataTable _);
    DataRow[] dataRowArray1 = dt1.Select("F_GUID = 'cad003ce-306c-11d8-b4e9-00304f19f545'");
    if (dataRowArray1 != null && dataRowArray1.Length != 0)
      return Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]);
    int num = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      num = sessionKeeper.Session.GetLifecycleLevel(new Guid("cad009de-306c-11d8-b4e9-00304f19f545")).LevelID;
    DataRow[] dataRowArray2 = dt1.Select("F_LEVEL_ID = " + Convert.ToString(num));
    return dataRowArray2 != null && dataRowArray2.Length != 0 ? Convert.ToInt32(dataRowArray2[0]["F_LC_STEP"]) : -1;
  }

  public void CopyPIAttribs(long otherECO_ID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject RuleObject1 = sessionKeeper.Session.GetObject(otherECO_ID);
      IDBObject RuleObject2 = sessionKeeper.Session.GetObject(this.EcoObjectID);
      VersionsRule versionsRule = new VersionsRule();
      versionsRule.LoadFromObject(sessionKeeper.Session, RuleObject1);
      versionsRule.SaveToObject(sessionKeeper.Session, RuleObject2);
      long initValue = 0;
      if (RuleObject1 is IDBEditingContextsObject)
        initValue = Math.Abs((RuleObject1 as IDBEditingContextsObject).LinkedContextNumber);
      AttributeValues[] valuesList = new AttributeValues[2]
      {
        new AttributeValues(RevHelper.idAttrObjectLink, (object) otherECO_ID),
        new AttributeValues(RevHelper.idLinkedContNumber, (object) initValue)
      };
      RuleObject2.SetAttributesValues(valuesList);
    }
  }

  public void GetEntersInObjects(
    IUserSession ius,
    long verId,
    ref List<long> objIDs,
    ref List<long> verIDs)
  {
    IDBObject dbObject = ius.GetObject(verId);
    IDBRelationCollection relationCollection = ius.GetRelationCollection(-1, "cad001e0-306c-11d8-b4e9-00304f19f545");
    relationCollection.LocalTypesMode = true;
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -3,
      (object) -2
    }), dbObject.ID).Rows)
    {
      long int64_1 = Convert.ToInt64(row[0]);
      long int64_2 = Convert.ToInt64(row[1]);
      if (verIDs.IndexOf(int64_2) < 0)
      {
        objIDs.Add(int64_1);
        verIDs.Add(int64_2);
      }
    }
  }

  public List<long> GetParentIIs(IUserSession ius, long verId)
  {
    List<long> parentIis = new List<long>();
    IDBRelationCollection relationCollection = ius.GetRelationCollection(RevHelper.idLinkRevision);
    relationCollection.LocalTypesMode = true;
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersInVersion(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) RevHelper.idObj_II, LogicalOperators.NONE, 0)
    }, new object[1]{ (object) -3 }), verId).Rows)
    {
      long num = Math.Abs(Convert.ToInt64(row[0]));
      parentIis.Add(num);
    }
    return parentIis;
  }

  public List<long> GetParentRevRels(IUserSession ius, long verId)
  {
    List<long> parentRevRels = new List<long>();
    if (ius.GetObjectActualCopy(verId, false) == null)
      return parentRevRels;
    IDBRelationCollection relationCollection = ius.GetRelationCollection(RevHelper.idLinkRevision);
    relationCollection.LocalTypesMode = true;
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) new int[3]
      {
        RevHelper.idObj_II,
        RevHelper.idObj_PI,
        RevHelper.idObj_PR
      }, LogicalOperators.NONE, 0)
    };
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersInVersion(new DBRecordSetParams(conditions, new object[1]
    {
      (object) -20
    }), verId).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      parentRevRels.Add(int64);
    }
    return parentRevRels;
  }

  public TableData FindEcoRow4Relation(long objID)
  {
    Guid objGuid = this._GuidById(objID);
    return objGuid != Guid.Empty ? this.FindEcoRow4Relation(objGuid) : (TableData) null;
  }

  public bool IsEcoRow4Relation(TableData ecoRow, long objID)
  {
    Guid g = this._GuidById(objID);
    return g != Guid.Empty && this.IsEcoRow4Relation(ecoRow, g);
  }

  public TableData FindEcoRow4Relation(Guid objGuid)
  {
    TableData dataOwner;
    for (int dataPositionInFlow = this.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      if (dataOwner.Nodes[dataPositionInFlow] is TableData node && this.IsEcoRow4Relation(node, objGuid))
        return node;
    }
    return (TableData) null;
  }

  public bool IsEcoRow4Relation(TableData ecoRow, Guid g)
  {
    return this._GetGuidList(ecoRow.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true)).Contains(g);
  }

  public int FindFirstEcoRow(out TableData ecoRow)
  {
    return this.ecoMainTable.FindDataPositionInFlow(0, out ecoRow);
  }

  public int FindNextEcoRow(int prevCellPosition, int dataPosition, out TableData ecoRow)
  {
    return this.ecoMainTable.FindNextDataPositionInFlow(prevCellPosition, out ecoRow);
  }

  public static int NumIds(string tagStr)
  {
    int num = 1;
    for (int index = 0; index < tagStr.Length; ++index)
    {
      if (tagStr[index] == ',')
        ++num;
    }
    return num;
  }

  public static List<long> Str2IdList(string tagStr)
  {
    List<long> longList = new List<long>();
    if (tagStr == "")
      return longList;
    char[] chArray = new char[1]{ ',' };
    foreach (string str in tagStr.Split(chArray))
    {
      try
      {
        long int64 = Convert.ToInt64(str);
        longList.Add(int64);
      }
      catch
      {
      }
    }
    return longList;
  }

  public static List<Guid> Str2GuidList(string tagStr)
  {
    List<Guid> guidList = new List<Guid>();
    if (tagStr == "")
      return guidList;
    char[] chArray = new char[1]{ ',' };
    foreach (string input in tagStr.Split(chArray))
    {
      try
      {
        Guid result = Guid.Empty;
        if (Guid.TryParse(input, out result))
        {
          guidList.Add(result);
        }
        else
        {
          long int64 = Convert.ToInt64(input);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
            guidList.Add(objectInfo.VersionGuid);
          }
        }
      }
      catch
      {
      }
    }
    return guidList;
  }

  public static string GuidListToStr(List<Guid> guidList)
  {
    string str = "";
    if (guidList.Count > 0)
      str = Convert.ToString((object) guidList[0]);
    for (int index = 1; index < guidList.Count; ++index)
      str = $"{str},{Convert.ToString((object) guidList[index])}";
    return str;
  }

  public static string[] Str2StrList(string tagStr)
  {
    char[] separator = new char[1]{ ',' };
    string[] strArray = tagStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = strArray[index].Trim();
    return strArray;
  }

  public static string StrList2Str(string[] strList)
  {
    string str = "";
    if (strList.Length != 0)
      str = strList[0];
    for (int index = 1; index < strList.Length; ++index)
      str = $"{str},{strList[index]}";
    return str;
  }

  public static string IdListToStr(List<long> idList)
  {
    string str = "";
    if (idList.Count > 0)
      str = Convert.ToString(idList[0]);
    for (int index = 1; index < idList.Count; ++index)
      str = $"{str},{Convert.ToString(idList[index])}";
    return str;
  }

  public List<Guid> _IdListToGuidList(List<long> idList)
  {
    List<Guid> guidList = new List<Guid>();
    foreach (long id in idList)
    {
      foreach (PendingLink objLink in this.objLinks)
      {
        if (objLink.verID == id || objLink.verID == -id)
          guidList.Add(objLink.verGuid);
      }
    }
    return guidList;
  }

  public void RebuildDictionaries()
  {
    this.guidIndexes.Clear();
    for (int index = 0; index < this.objLinks.Count; ++index)
    {
      PendingLink objLink = this.objLinks[index];
      Guid empty = Guid.Empty;
      Guid key = this.objGuids.ContainsKey(objLink.verID) ? this.objGuids[objLink.verID] : this.UpdateGuid(objLink.verID);
      if (key != Guid.Empty)
        this.guidIndexes.Add(key, index);
    }
  }

  public Guid UpdateGuid(long objId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objId, false);
      Guid guid = objectActualCopy != null ? objectActualCopy.ObjectGUID : Guid.Empty;
      this.objGuids.Add(objId, guid);
      return guid;
    }
  }

  public List<long> _GetIdList(string tagStr)
  {
    if (this.IdLists())
      return Intermech.ECO.Client.ECO.Str2IdList(tagStr);
    List<long> idList = new List<long>();
    foreach (Guid str2Guid in Intermech.ECO.Client.ECO.Str2GuidList(tagStr))
    {
      foreach (PendingLink objLink in this.objLinks)
      {
        if (objLink.verGuid.Equals(str2Guid))
          idList.Add(objLink.verID);
      }
    }
    return idList;
  }

  public List<Guid> _GetGuidList(string tagStr)
  {
    if (!this.IdLists())
      return Intermech.ECO.Client.ECO.Str2GuidList(tagStr);
    List<Guid> guidList = new List<Guid>();
    foreach (long str2Id in Intermech.ECO.Client.ECO.Str2IdList(tagStr))
    {
      foreach (PendingLink objLink in this.objLinks)
      {
        if (objLink.verID == str2Id || objLink.verID == -str2Id)
          guidList.Add(objLink.verGuid);
      }
    }
    return guidList;
  }

  public List<PendingLink> _GetPList(string tagStr)
  {
    List<PendingLink> plist = new List<PendingLink>();
    if (!this.IdLists())
    {
      foreach (Guid str2Guid in Intermech.ECO.Client.ECO.Str2GuidList(tagStr))
      {
        foreach (PendingLink objLink in this.objLinks)
        {
          if (objLink.verGuid.Equals(str2Guid))
            plist.Add(objLink);
        }
      }
    }
    else
    {
      foreach (long str2Id in Intermech.ECO.Client.ECO.Str2IdList(tagStr))
      {
        foreach (PendingLink objLink in this.objLinks)
        {
          if (objLink.verID == str2Id || objLink.verID == -str2Id)
            plist.Add(objLink);
        }
      }
    }
    return plist;
  }

  public string _SetIdList(RectangleElement te, List<long> IdList)
  {
    if (this.IdLists())
    {
      string str = Intermech.ECO.Client.ECO.IdListToStr(IdList);
      te.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, str);
      return str;
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long id in IdList)
    {
      foreach (PendingLink objLink in this.objLinks)
      {
        if (objLink.verID == id || objLink.verID == -id)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(objLink.verGuid.ToString());
        }
      }
    }
    string attributeValue = stringBuilder.ToString();
    te.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
    return attributeValue;
  }

  public string DesignListStr(List<long> idList)
  {
    string str = "";
    string[] strArray = new string[idList.Count];
    for (int index1 = 0; index1 < this.objLinks.Count; ++index1)
    {
      PendingLink objLink = this.objLinks[index1];
      int index2 = idList.IndexOf(objLink.verID);
      if (index2 >= 0)
        strArray[index2] = this.objLinks[index1].design.Replace(" ", "\u000E");
    }
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == "") && strArray[index] != null)
        str = !(str != "") ? strArray[index] : $"{str}, {strArray[index]}";
    }
    return str;
  }

  public List<string> GetDesignList(List<long> idList)
  {
    List<string> designList = new List<string>();
    foreach (long id in idList)
    {
      int index = this.ObjIdIndex(id);
      if (index < 0)
      {
        designList.Add("???");
      }
      else
      {
        PendingLink objLink = this.objLinks[index];
        designList.Add(objLink.design.Replace(" ", "\u000E"));
      }
    }
    return designList;
  }

  public string MakeDesignString(List<PendingLink> pList)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PendingLink p in pList)
    {
      if (!(p.design == ""))
      {
        if (stringBuilder.Length != 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(p.design);
      }
    }
    return stringBuilder.ToString();
  }

  public static bool IsChange(DocumentTreeNode dtn)
  {
    return dtn is TableElement && !(dtn.Id == Intermech.ECO.Client.ECO.specTextTable);
  }

  public static bool IsExternal(TableElement change) => change.Tag == DBNull.Value;

  public ECOGoal ChangeGoal(DocumentTreeNode dtn)
  {
    if (!Intermech.ECO.Client.ECO.IsChange(dtn))
      return ECOGoal.NoGoal;
    if (Intermech.ECO.Client.ECO.IsExternal((TableElement) dtn))
      return ECOGoal.Change;
    string attributeValue = dtn.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
    PendingLink pendingLink;
    if (this.IdLists())
    {
      List<long> longList = Intermech.ECO.Client.ECO.Str2IdList(attributeValue);
      if (longList.Count == 0)
        return ECOGoal.NoGoal;
      pendingLink = this.FindPendingLink(longList[0]);
    }
    else
    {
      List<Guid> guidList = Intermech.ECO.Client.ECO.Str2GuidList(attributeValue);
      if (guidList.Count == 0)
        return ECOGoal.NoGoal;
      pendingLink = this.FindPendingLink(guidList[0]);
    }
    return pendingLink == null ? ECOGoal.Change : pendingLink.ecoGoal;
  }

  public void GenerateDocumentECO(ImDocument template, IDBObject ecoObject)
  {
    this.documentECO = template != null ? new ImDocument(template, true, true) : throw new ArgumentNullException(nameof (template));
    this.CheckEcoMainTable();
    this.documentECO.Reference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) this.documentECO, ecoObject, false);
    this.ecoDBObjectGuid = ecoObject.ObjectGUID;
  }

  public void CheckEcoMainTable()
  {
    this.ecoMainTable = this.documentECO != null ? ImDocumentData.GetFirstPage((DocumentTreeNode) this.documentECO).FindFirstMainTable() as TableElement : throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_401"));
    if (this.ecoMainTable == null || this.ecoMainTable.Template.Name != LocalizationHolder.rm.GetString("ECO.Client_19"))
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_20"));
    if (this.ecoDocRevision != null)
      return;
    this.ecoDocRevision = this.documentECO.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idRevision) as TextData;
  }

  private TableElement GetEcoMainTableOnPage()
  {
    Page activePage = this.documentECO.DocumentControl.ActivePage;
    TableElement ecoMainTableOnPage = (TableElement) null;
    if (activePage != null)
      ecoMainTableOnPage = activePage.FindFirstNodeFromTemplate_Recursive(this.ecoMainTable.TemplateId) as TableElement;
    return ecoMainTableOnPage;
  }

  public TableElement AddNewEcoRow(string rowTemplateID, bool checkECO = true)
  {
    if (checkECO)
      this.CheckEcoMainTable();
    TableElement child = (this.documentECO.Template.FindNode(rowTemplateID) as TableElement).CloneFromTemplate() as TableElement;
    TableData dataOwner;
    int index = (this.GetEcoMainTableOnPage() ?? this.ecoMainTable).FindLastDataPositionInFlow(out dataOwner) + 1;
    if (index > dataOwner.Nodes.Count)
      index = dataOwner.Nodes.Count;
    dataOwner.InsertChildNode(index, (DocumentTreeNode) child, false, false, false, false, false);
    DocumentTreeNode template = child.Template;
    return child;
  }

  public TableElement InsertNewEcoRow(int index, string rowTemplateID)
  {
    this.CheckEcoMainTable();
    TableElement child = (this.documentECO.Template.FindNode(rowTemplateID) as TableElement).CloneFromTemplate() as TableElement;
    TableData dataOwner;
    int dataPositionInFlow = this.ecoMainTable.FindDataPositionInFlow(index, out dataOwner);
    dataOwner.InsertChildNode(dataPositionInFlow, (DocumentTreeNode) child, false, true, true, true, false);
    return child;
  }

  public TableElement AddNewEcoElement(TableElement change, string rowTemplateID)
  {
    TableElement node = change.Template.FindNode(rowTemplateID) as TableElement;
    if (node.Parent != change.Template)
    {
      TableElement templateRecursive = (TableElement) change.FindFirstNodeFromTemplate_Recursive(node.Parent);
      if (templateRecursive != null)
        change = templateRecursive;
    }
    TableElement child = node.CloneFromTemplate() as TableElement;
    TableData dataOwner;
    int index = change.FindLastDataPositionInFlow(out dataOwner) + 1;
    if (index > dataOwner.Nodes.Count)
      index = dataOwner.Nodes.Count;
    dataOwner.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, true, false);
    return child;
  }

  public TableElement InsertNewEcoElement(
    TableElement currentElement,
    bool insertAfterCurrent,
    TableElement change,
    string rowTemplateID,
    IUndoManager undoManager)
  {
    undoManager.BeginCreateMultyUndo("Вставка элемента " + rowTemplateID);
    try
    {
      TableElement node = change.Template.FindNode(rowTemplateID) as TableElement;
      if (node.Parent != change.Template)
      {
        TableElement templateRecursive = (TableElement) change.FindFirstNodeFromTemplate_Recursive(node.Parent);
        if (templateRecursive != null && change != templateRecursive)
        {
          change = templateRecursive;
          currentElement = (TableElement) null;
        }
      }
      TableElement child = node.CloneFromTemplate() as TableElement;
      TableData dataOwner = (TableData) change;
      int index;
      if (currentElement != null)
        index = !insertAfterCurrent ? currentElement.FindFirstCell().Index : dataOwner.FindNextDataPositionInFlow(currentElement.Index, out dataOwner);
      else if (insertAfterCurrent)
      {
        dataOwner = (TableData) (change.FindLastCell() as TableElement);
        index = dataOwner.NodesCount;
      }
      else
      {
        dataOwner = (TableData) (change.FindFirstCell() as TableElement);
        index = dataOwner.FindNextDataPositionInFlow(0, out dataOwner);
      }
      dataOwner.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false, false);
      return child;
    }
    finally
    {
      undoManager.EndCreateMultyUndo();
    }
  }

  public TableElement FindParentEcoRow(DocumentTreeNode node)
  {
    if (!(node is RectangleElement rectangleElement))
      return (TableElement) null;
    if (rectangleElement.TopLevelTable != this.ecoMainTable)
      return (TableElement) null;
    if (rectangleElement is TableData node1 && this.IsEcoRow((DocumentTreeNode) node1))
      return (TableElement) node1;
    TableData parentCell = rectangleElement.ParentCell;
    while (parentCell != null && !this.IsEcoRow((DocumentTreeNode) parentCell))
      parentCell = parentCell.ParentCell;
    return (TableElement) parentCell;
  }

  public bool IsEcoRow(DocumentTreeNode node)
  {
    return this.ecoMainTable != null && node is TableElement tableElement && tableElement.Reference is ReferenceToDBObject reference && reference.IsReferenceToRelation && !tableElement.IsTopLevelTable && tableElement.TopLevelTable.FlowID == this.ecoMainTable.FlowID;
  }

  public string GetDocDesignationInECO(IDBObject dbObject)
  {
    return dbObject.GetAttributeByID(DocIDCache.Attr_Designation).Description;
  }

  public static List<string> GetAbonList(long objId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
      if (dbObject == null)
        return (List<string>) null;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid(RevHelper.guidAttrAbonents));
      if (attributeByGuid == null)
        return (List<string>) null;
      List<string> abonList = new List<string>();
      foreach (object obj in attributeByGuid.Values)
      {
        if (!(obj.GetType() == typeof (DBNull)))
        {
          long int64 = Convert.ToInt64(obj);
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
          abonList.Add(objectInfo.Caption);
        }
      }
      return abonList;
    }
  }

  public static List<long> GetAbonListIds(long objId, int objType = -1)
  {
    List<int> intList = objType != -1 ? MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType) : new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long sendList = RevHelper.GetSendList(sessionKeeper.Session.GetIDByObjectID(objId));
      IDBObject dbObject = sessionKeeper.Session.GetObject(sendList, false);
      if (dbObject == null)
        return (List<long>) null;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid(RevHelper.guidAttrAbonents));
      if (attributeByGuid == null)
        return (List<long>) null;
      List<long> abonListIds = new List<long>();
      foreach (object obj in attributeByGuid.Values)
      {
        if (!(obj is DBNull))
        {
          long int64 = Convert.ToInt64(obj);
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
          if (intList.Contains(objectInfo.ObjectTypeID))
            abonListIds.Add(int64);
        }
      }
      return abonListIds;
    }
  }

  public ECOGoal GetChangeGoal(TableElement change)
  {
    List<long> idList = this._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    ECOGoal changeGoal = ECOGoal.NoGoal;
    if (idList.Count > 0)
    {
      PendingLink pendingLink = this.FindPendingLink(idList[0]);
      if (pendingLink != null)
        changeGoal = pendingLink.ecoGoal;
    }
    return changeGoal;
  }

  public int GetObjectCount()
  {
    return this.objLinks.Count<PendingLink>((System.Func<PendingLink, bool>) (pl => pl.ecoGoal != ECOGoal.Litera));
  }
}
