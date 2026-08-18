// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevHelper
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.LifeCycles;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.ECO.Client;

public class RevHelper
{
  public static readonly string guidObjRevision = "cad00348-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_II = "cad00349-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_PI = "cad0034a-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_PR = "cad0034b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_IPV = "cadd9bb3-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLinkRevision = "cad0036b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLinkDocument = "cad00154-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLinkProject = "cad00023-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrVerId = "cad001c2-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrRevNeed = "cad0077a-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrRevReason = "cad0077d-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjRevTemplate = "cad0077b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidDefRevTemplate = "cad0077c-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrZadel = "cad0079d-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrNewRevNeed = "cad01524-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjContext = "cad0146b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLinkedContNumber = "cad014ff-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangesGroupNum = "cad014d2-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrVersionRule = "cad00696-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrObjectLink = "cad00697-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrIncludeGoal = "cad007a3-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrLitera = "cad0038b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrFlags = "cad00072-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDelWhenExcluded = "cad00073-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrFutureLC = "cad01483-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrTemplate = "cad01558-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidattrChangeDateStart = "cad007a0-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidattrChangeDateEnd = "cadd9562-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjSendList = "cadd9365-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrAbonents = "cadd9351-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrOTDDocId = "cadd935a-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrRevSeriesDates = "cadd9506-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrAuxLinks = "cadd93b7-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrMainObjectGuid = "cadd93c0-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_DI = "cadd955e-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_DPI = "cadd9560-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLink_FromDI = "cadd955f-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopIzv = "cadd9561-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangeStart = "cad007a0-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangeEnd = "cadd9562-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopDesign = "cadd9563-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopIzvText = "cadd9564-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidIzvVersion = "cadd9598-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidChangeJournal = "cadd9584-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidChangeJournalLink = "cadd9585-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidChangeJournalContent = "cadd9586-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjCJRecord = "cadd9588-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrJournalLink = "cadd95b2-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidRevTemplate = "cad0077b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidReplacedByECO = "cadd9587-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidCJRecSchema = "cadd9592-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidStepKeeping = "cadd9602-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidStepWaitingForII = "cadd9603-306c-11d8-b4e9-00304f19f545";
  public static readonly string lcActualize = "cad003cc-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidInvNoOTD = "cadd935b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidDateOTD = "cadd941c-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidRegOTD = "cadd941d-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidDeliveryListObjectType = "cadd9365-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidOriginalObjectIdAttr = "cadd935a-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidActualCopyAttr = "cadd9352-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrStampedByII = "cadd969d-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjProcTemplate = "cad002ac-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrLinkToAnnuledPI = "cadd96bf-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDelVersionsList = "cadd93cb-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLCStepWaitingForII = "cadd9593-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrHiding = "cadd98a3-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLinkDocForIzd = "cad00154-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrMaxCJNum = "cadd972b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrNotifDate = "cadd9732-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrRevision = "cadd9645-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrAllowLCChange = "cadd9b70-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrUkazanieOVnedrenii = "cadd9bc5-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjTypeServiceNote = "cadd9bc7-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjTypeIPV = "cadd9bb3-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangeReason = "cadd9a8b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjTypeOrganization = "cadd9231-306c-11d8-b4e9-00304f19f545";
  public static readonly int idLinkRevision = 0;
  public static readonly int idLinkDoc = 0;
  public static readonly int idLinkProject = 0;
  public static readonly int idAttrVerId = 0;
  public static readonly int idAttrRevNeed = 0;
  public static readonly int idAttrNewRevNeed = 0;
  public static readonly int idObjTypeRevTemplate = 0;
  public static readonly int idAttrName = 0;
  public static readonly int idAttrDesign = 0;
  public static readonly int idAttrFile = 0;
  public static readonly int idAttrRevReason = 0;
  public static readonly int idAttrChangeNo = 0;
  public static readonly int idObjRevision = 0;
  public static readonly int idAttrDelWhenExcluded = 0;
  public static readonly int idLinkedContNumber = 0;
  public static readonly int idAttrVersionRule = 0;
  public static readonly int idAttrObjectLink = 0;
  public static readonly int idAttrIncludeGoal = 0;
  public static readonly int idAttrLitera = 0;
  public static readonly int idAttrFlags = 0;
  public static readonly int idObj_II = 0;
  public static readonly int idObj_PI = 0;
  public static readonly int idObj_PR = 0;
  public static readonly int idObj_DI = 0;
  public static readonly int idObj_DPI = 0;
  public static readonly int idObj_IPV = 0;
  public static readonly int idAttrFutureLC = 0;
  public static readonly int idAttrTemplate = 0;
  public static readonly int idAttrChangesGroupNum = 0;
  public static readonly int idAttrChangeDateStart = 0;
  public static readonly int idAttrChangeDateEnd = 0;
  public static readonly int idAttrAuxLinks = 0;
  public static readonly int idAttrMainObjectGuid = 0;
  public static readonly int idAttrRevSeriesDates = 0;
  public static readonly int idProjectLink = 0;
  public static readonly int idLinkFromDI = 0;
  public static readonly int idAttrLinkDopIzv = 0;
  public static readonly int idAttrDopDesign = 0;
  public static readonly int idAttrDopIzvText = 0;
  public static readonly int idAttrVersion = 0;
  public static readonly int idChangeJournal = 0;
  public static readonly int idRevTemplate = 0;
  public static readonly int idChangeJournalLink = 0;
  public static readonly int idChangeJournalContent = 0;
  public static readonly int idObjCJRecord = 0;
  public static readonly int idAttrJournalLink = 0;
  public static readonly int idAttrReplacedByECO = 0;
  public static readonly int idAttrCreationDate = 0;
  public static readonly int idCJRecSchema = 0;
  public static readonly int idStepKeeping = 0;
  public static readonly int idStepWaitingForII = 0;
  public static readonly int idAttrScannedDoc = 0;
  public static readonly int idAttrDocFile4Scanned = 0;
  public static readonly int idAttrInvNoOTD = 0;
  public static readonly int idDateOTD = 0;
  public static readonly int idRegOTD = 0;
  public static readonly int idDeliveryListObjectType = 0;
  public static readonly int idOriginalObjectIdAttr = 0;
  public static readonly int idActualCopyAtt = 0;
  public static readonly int idAttrStampedByII = 0;
  public static readonly int idLC_Actualize = 0;
  public static readonly int idObjProcTemplate = 0;
  public static readonly int idLevelManufacturing = 0;
  public static readonly int idLinkToAnnuledPI = 0;
  public static readonly int idLevelKeeping = 0;
  public static readonly int idLevelAnnuled = 0;
  public static readonly int idAttrDelVersionsList = 0;
  public static readonly int idObjContext = 0;
  public static readonly int idLinkDocForIzd = 0;
  public static readonly int idAttrMaxCJNum = 0;
  public static readonly int idLevelWaitingForII = 0;
  public static readonly int idAttrNotifDate = 0;
  public static readonly int idAttrHiding = 0;
  public static readonly int idAttrRevision = 0;
  public static readonly int idAttrAllowLCChange = 0;
  public static readonly string nameAttrChangeNo = "";
  public static readonly string nameAttrRevision = "";
  public static readonly int idAttrUkazanieOVnedrenii = 0;
  public static readonly int idObj_SN = 0;
  public static readonly int idObjIPV = 0;
  public static readonly int idAttrChangeReason = 0;
  public static readonly int idObjOrganization = 0;
  public Hashtable reqRevs;
  public static readonly RevHelper Global = new RevHelper();
  public INotificationService _notService;

  public event CreateVersionHandler CreateVersion;

  public INotificationService NotifService
  {
    get
    {
      if (this._notService == null)
        this._notService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      return this._notService;
    }
  }

  static RevHelper()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      RevHelper.idLinkRevision = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidLinkRevision)).RelationType;
      RevHelper.idLinkDoc = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidLinkDocument)).RelationType;
      RevHelper.idLinkProject = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidLinkProject)).RelationType;
      RevHelper.idAttrVerId = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrVerId)).AttributeID;
      RevHelper.idAttrRevNeed = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevNeed)).AttributeID;
      RevHelper.idAttrNewRevNeed = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrNewRevNeed)).AttributeID;
      RevHelper.idObjTypeRevTemplate = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjRevTemplate)).ObjectType;
      RevHelper.idObj_II = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_II)).ObjectType;
      RevHelper.idObj_PI = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_PI)).ObjectType;
      RevHelper.idObj_PR = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_PR)).ObjectType;
      RevHelper.idObj_DI = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_DI)).ObjectType;
      RevHelper.idObj_DPI = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_DPI)).ObjectType;
      RevHelper.idObj_IPV = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_IPV)).ObjectType;
      RevHelper.idObjRevision = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjRevision)).ObjectType;
      RevHelper.idChangeJournal = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidChangeJournal)).ObjectType;
      RevHelper.idRevTemplate = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidRevTemplate)).ObjectType;
      RevHelper.idObjCJRecord = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjCJRecord)).ObjectType;
      RevHelper.idObjProcTemplate = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjProcTemplate)).ObjectType;
      RevHelper.idObjContext = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjContext)).ObjectType;
      RevHelper.idDeliveryListObjectType = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidDeliveryListObjectType)).ObjectType;
      RevHelper.idObj_SN = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjTypeServiceNote)).ObjectType;
      RevHelper.idObjIPV = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjTypeIPV)).ObjectType;
      RevHelper.idObjOrganization = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObjTypeOrganization)).ObjectType;
      RevHelper.idAttrName = sessionKeeper.Session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idAttrDesign = sessionKeeper.Session.GetAttributeType(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idAttrFile = sessionKeeper.Session.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idAttrRevReason = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevReason)).AttributeID;
      IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
      RevHelper.idAttrChangeNo = attributeType1.AttributeID;
      RevHelper.nameAttrChangeNo = attributeType1.Name;
      RevHelper.idLinkedContNumber = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidLinkedContNumber)).AttributeID;
      RevHelper.idAttrChangesGroupNum = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrChangesGroupNum)).AttributeID;
      RevHelper.idAttrVersionRule = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrVersionRule)).AttributeID;
      RevHelper.idAttrObjectLink = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrObjectLink)).AttributeID;
      RevHelper.idAttrIncludeGoal = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrIncludeGoal)).AttributeID;
      RevHelper.idAttrLitera = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrLitera)).AttributeID;
      RevHelper.idAttrFlags = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrFlags)).AttributeID;
      RevHelper.idAttrDelWhenExcluded = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrDelWhenExcluded)).AttributeID;
      RevHelper.idAttrFutureLC = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrFutureLC)).AttributeID;
      RevHelper.idAttrTemplate = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrTemplate)).AttributeID;
      RevHelper.idAttrChangeDateStart = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidattrChangeDateStart)).AttributeID;
      RevHelper.idAttrChangeDateEnd = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidattrChangeDateEnd)).AttributeID;
      RevHelper.idAttrAuxLinks = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrAuxLinks)).AttributeID;
      RevHelper.idAttrMainObjectGuid = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrMainObjectGuid)).AttributeID;
      RevHelper.idAttrRevSeriesDates = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevSeriesDates)).AttributeID;
      RevHelper.idAttrJournalLink = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrJournalLink)).AttributeID;
      RevHelper.idAttrAllowLCChange = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrAllowLCChange)).AttributeID;
      RevHelper.idAttrLinkDopIzv = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrDopIzv)).AttributeID;
      RevHelper.idAttrDopDesign = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrDopDesign)).AttributeID;
      RevHelper.idAttrVersion = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidIzvVersion)).AttributeID;
      RevHelper.idAttrReplacedByECO = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidReplacedByECO)).AttributeID;
      RevHelper.idAttrCreationDate = sessionKeeper.Session.GetAttributeType(new Guid("cad0013c-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idProjectLink = sessionKeeper.Session.GetRelationType(new Guid("cad00023-306c-11d8-b4e9-00304f19f545")).RelationType;
      RevHelper.idLinkFromDI = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidLink_FromDI)).RelationType;
      RevHelper.idLinkDocForIzd = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidLinkDocForIzd)).RelationType;
      RevHelper.idChangeJournalLink = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidChangeJournalLink)).RelationType;
      RevHelper.idChangeJournalContent = sessionKeeper.Session.GetRelationType(new Guid(RevHelper.guidChangeJournalContent)).RelationType;
      RevHelper.idAttrScannedDoc = sessionKeeper.Session.GetAttributeType(new Guid("cadd9644-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idAttrDocFile4Scanned = sessionKeeper.Session.GetAttributeType(new Guid("cadd9620-306c-11d8-b4e9-00304f19f545")).AttributeID;
      RevHelper.idAttrInvNoOTD = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidInvNoOTD)).AttributeID;
      RevHelper.idDateOTD = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidDateOTD)).AttributeID;
      RevHelper.idRegOTD = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidRegOTD)).AttributeID;
      RevHelper.idOriginalObjectIdAttr = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidOriginalObjectIdAttr)).AttributeID;
      RevHelper.idActualCopyAtt = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidActualCopyAttr)).AttributeID;
      RevHelper.idAttrStampedByII = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrStampedByII)).AttributeID;
      RevHelper.idLinkToAnnuledPI = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrLinkToAnnuledPI)).AttributeID;
      RevHelper.idAttrDelVersionsList = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrDelVersionsList)).AttributeID;
      RevHelper.idAttrMaxCJNum = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrMaxCJNum)).AttributeID;
      RevHelper.idAttrNotifDate = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrNotifDate)).AttributeID;
      RevHelper.idAttrHiding = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrHiding)).AttributeID;
      IDBAttributeType attributeType2 = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevision));
      RevHelper.idAttrRevision = attributeType2.AttributeID;
      RevHelper.nameAttrRevision = attributeType2.Name;
      RevHelper.idAttrChangeReason = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrChangeReason)).AttributeID;
      RevHelper.idAttrUkazanieOVnedrenii = MetaDataHelper.GetAttributeTypeID(new Guid(RevHelper.guidAttrUkazanieOVnedrenii));
      RevHelper.idLevelKeeping = MetaDataHelper.GetLCLevelID(new Guid("cad009de-306c-11d8-b4e9-00304f19f545"));
      RevHelper.idLevelAnnuled = MetaDataHelper.GetLCLevelID(new Guid("cad00012-306c-11d8-b4e9-00304f19f545"));
      RevHelper.idLevelWaitingForII = MetaDataHelper.GetLCLevelID(new Guid(RevHelper.guidLCStepWaitingForII));
      IDBLCSchema lcSchema = sessionKeeper.Session.GetLCSchema(new Guid(RevHelper.guidCJRecSchema), false);
      if (lcSchema != null)
      {
        RevHelper.idCJRecSchema = lcSchema.SchemaProperties.SchemaID;
        DataTable table = lcSchema.GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"];
        DataRow[] dataRowArray1 = table.Select($"[F_LEVEL_ID] = '{(object) RevHelper.idLevelKeeping}'");
        if (dataRowArray1 != null && dataRowArray1.Length != 0)
          RevHelper.idStepKeeping = Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]);
        DataRow[] dataRowArray2 = table.Select($"[F_LEVEL_ID] = '{(object) RevHelper.idLevelWaitingForII}'");
        if (dataRowArray2 != null && dataRowArray2.Length != 0)
          RevHelper.idStepWaitingForII = Convert.ToInt32(dataRowArray2[0]["F_LC_STEP"]);
      }
      IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(new Guid(RevHelper.lcActualize));
      if (lifecycleStep != null)
        RevHelper.idLC_Actualize = lifecycleStep.LCStep;
      RevHelper.idLevelManufacturing = MetaDataHelper.GetLCLevelID(new Guid("cad00011-306c-11d8-b4e9-00304f19f545"));
    }
  }

  private RevHelper() => this.reqRevs = new Hashtable();

  public static long[] RevisionsForVersion(IUserSession ius, long versionID)
  {
    IDBRelationCollection relationCollection = ius.GetRelationCollection(RevHelper.idLinkRevision);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(RevHelper.idAttrVerId, RelationalOperators.Equal, (object) versionID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Default, SortOrders.ASC, 1)
    });
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.Select(paramSet);
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return (long[]) null;
    List<long> longList = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    return longList.ToArray();
  }

  public static IDBRelation CreateRevRelation(
    IUserSession session,
    long ecoID,
    long objVerID,
    string changeNo,
    ECOGoal goal,
    HidingType hideType)
  {
    IDBRelation dbRelation = (IDBRelation) null;
    IDBRelationCollection relationCollection = session.GetRelationCollection(RevHelper.idLinkRevision);
    IDBObject dbObject = session.GetObject(objVerID, false);
    if (dbObject == null)
    {
      objVerID = -objVerID;
      dbObject = session.GetObject(objVerID, true);
    }
    long revRelation = RevHelper.GetRevRelation(ecoID, objVerID);
    if (revRelation != 0L)
      dbRelation = session.GetRelation(revRelation);
    long relationId;
    if (dbRelation == null)
    {
      AttributeValues[] array = new List<AttributeValues>()
      {
        new AttributeValues(RevHelper.idAttrChangeNo, (object) changeNo),
        new AttributeValues(RevHelper.idAttrIncludeGoal, (object) (int) goal),
        new AttributeValues(RevHelper.idAttrHiding, (object) (int) hideType)
      }.ToArray();
      NewRelationProperties properties = new NewRelationProperties(-1L, ecoID, 0L, DateTime.Now, DateTime.MaxValue, objVerID, array);
      using (new ECOLinkCreator(session, ecoID, objVerID))
        relationId = relationCollection.Create(properties).RelationID;
    }
    else
    {
      relationId = dbRelation.RelationID;
      IDBAttribute byId1 = dbRelation.Attributes.FindByID(RevHelper.idAttrChangeNo);
      if (byId1 != null && changeNo != "")
        byId1.AsString = changeNo;
      IDBAttribute byId2 = dbRelation.Attributes.FindByID(RevHelper.idAttrIncludeGoal);
      if (byId2 != null)
        byId2.AsInteger = (long) goal;
    }
    bool changeNumSet = false;
    if (goal != ECOGoal.Annul && goal != ECOGoal.Creation)
    {
      IDBAttribute dbAttribute = (IDBAttribute) null;
      if (dbObject != null && dbObject.VersionID == 0)
      {
        if (dbObject.ObjectModifyMode != ObjectModifyModes.CantModify)
        {
          try
          {
            IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, RevHelper.idAttrChangeNo);
            if (attribute4ObjectType != null)
            {
              if ((attribute4ObjectType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                dbObject = dbObject.CheckOut();
              dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
            }
          }
          catch
          {
          }
          if (dbAttribute != null && changeNo != "")
          {
            dbAttribute.Value = (object) changeNo;
            changeNumSet = true;
          }
        }
      }
    }
    INotificationService notifService = RevHelper.Global.NotifService;
    if (notifService != null && dbObject != null)
    {
      IDBObject idbO = session.GetObjectActualCopy(objVerID, false);
      if (idbO != null)
      {
        if ((!changeNumSet || RevHelper.CanWriteAttrAlways(idbO, RevHelper.idAttrChangeNo)) && RevHelper.CanWriteAttrAlways(idbO, RevHelper.idAttrRevision))
        {
          RevHelper._DoNotify(notifService, changeNumSet, idbO, changeNo);
        }
        else
        {
          switch (idbO.ObjectModifyMode)
          {
            case ObjectModifyModes.InBase:
            case ObjectModifyModes.Checkout:
              bool flag = false;
              long objectId1 = idbO.ObjectID;
              if (idbO.ObjectModifyMode == ObjectModifyModes.Checkout && objectId1 > 0L)
              {
                idbO = idbO.CheckOut();
                flag = true;
                long objectId2 = idbO.ObjectID;
              }
              try
              {
                RevHelper._DoNotify(notifService, changeNumSet, idbO, changeNo);
                break;
              }
              finally
              {
                if (flag)
                  idbO.CheckIn();
              }
          }
        }
      }
    }
    return session.GetRelation(relationId, true);
  }

  public static bool CanWriteAttrAlways(IDBObject idbO, int attrId)
  {
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(idbO.ObjectType, attrId);
    return attribute4ObjectType != null && !attribute4ObjectType.IsContent && (attribute4ObjectType.Options & AttributeOptions.ModifyInBase) != 0;
  }

  public static void _DoNotify(
    INotificationService NotifService,
    bool changeNumSet,
    IDBObject idbO,
    string changeNo)
  {
    if (changeNumSet)
      NotifService.FireEvent((object) RevHelper.Global, (NotificationEventArgs) new DBObjectsExtendedEventArgs(idbO.ObjectID, idbO.ObjectType, new AttributeValues(RevHelper.idAttrChangeNo, (object) "")
      {
        AttributeName = RevHelper.nameAttrChangeNo
      }, new AttributeValues(RevHelper.idAttrChangeNo, (object) changeNo)
      {
        AttributeName = RevHelper.nameAttrChangeNo
      }));
    IDBAttribute byId = idbO.Attributes.FindByID(RevHelper.idAttrRevision);
    if (byId == null || byId.Value == DBNull.Value)
      return;
    NotifService.FireEvent((object) RevHelper.Global, (NotificationEventArgs) new DBObjectsExtendedEventArgs(idbO.ObjectID, idbO.ObjectType, new AttributeValues(RevHelper.idAttrRevision, (object) null)
    {
      AttributeName = RevHelper.nameAttrRevision
    }, new AttributeValues(RevHelper.idAttrRevision, byId.Value)
    {
      AttributeName = RevHelper.nameAttrRevision
    }));
  }

  public static long GetRevRelation(long ecoID, long objVerID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation dbRelation = (IDBRelation) null;
      try
      {
        if ((sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer).ObjectHasID(sessionKeeper.Session.SessionGUID, objVerID))
          dbRelation = sessionKeeper.Session.GetRelation(ecoID, objVerID, RevHelper.idLinkRevision, true);
      }
      catch (ObjectNotFoundException ex)
      {
      }
      if (dbRelation != null)
        return dbRelation.RelationID;
      return 0;
    }
  }

  public static long GetSendList(long ID)
  {
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(new Guid(RevHelper.guidAttrOTDDocId), RelationalOperators.Equal, (object) ID, LogicalOperators.NONE, 0)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(new Guid(RevHelper.guidObjSendList)).Select(new DBRecordSetParams(conditions, columns));
      return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
    }
  }

  internal static RevType _getRevType(int objRevType)
  {
    if (objRevType == RevHelper.idObj_II)
      return RevType.II;
    if (objRevType == RevHelper.idObj_PI)
      return RevType.PI;
    if (objRevType == RevHelper.idObj_PR)
      return RevType.PR;
    if (objRevType == RevHelper.idObj_DI)
      return RevType.DI;
    if (objRevType == RevHelper.idObj_DPI)
      return RevType.DPI;
    return objRevType == RevHelper.idChangeJournal ? RevType.CJ : RevType.Unknown;
  }

  public static RevType objType2RevType(int objRevType)
  {
    RevType revType1 = RevHelper._getRevType(objRevType);
    if (revType1 == RevType.Unknown)
    {
      foreach (int objRevType1 in MetaDataHelper.GetObjectTypeParentsID(objRevType))
      {
        RevType revType2 = RevHelper._getRevType(objRevType1);
        if (revType2 != RevType.Unknown)
          return revType2;
      }
    }
    return revType1;
  }
}
