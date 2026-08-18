// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ECOObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class ECOObject(UserSession uSession, DataTable objectsTable) : LinkIzvObject(uSession, objectsTable)
{
  internal static int idAttrDopIzv = 0;
  internal static int idAttrDopDesign = 0;
  internal static int idAttrChangeStart = 0;
  internal static int idAttrChangeEnd = 0;
  internal static int relTypeDI = 0;
  public static readonly Guid guidAttrDelVersionsList = new Guid("cadd93cb-306c-11d8-b4e9-00304f19f545");
  public static readonly string guidLink_FromDI = "cadd955f-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopIzv = "cadd9561-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopDesign = "cadd9563-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangeStart = "cad007a0-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrChangeEnd = "cadd9562-306c-11d8-b4e9-00304f19f545";
  protected DateTime _startDate = DateTime.MinValue;
  protected DateTime _endDate = DateTime.MinValue;
  internal bool LockCheckAttributes;

  public static int Attr_EcoObject
  {
    get => MetaDataHelper.GetAttributeTypeID(new Guid("cadd9645-306c-11d8-b4e9-00304f19f545"));
  }

  public static int Attr_CompositionVersionID
  {
    get => MetaDataHelper.GetAttributeTypeID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
  }

  public static int Relation_Document
  {
    get => MetaDataHelper.GetRelationTypeID(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
  }

  protected void UpdateStartAndEndTimes()
  {
    long objectID = 0;
    object[] valuesById1 = this.GetValuesByID(ECOObject.idAttrDopIzv, false);
    if (valuesById1 != null && valuesById1.Length != 0 && valuesById1[0] != DBNull.Value)
      objectID = Convert.ToInt64(valuesById1[0]);
    IDBObject dbObject = (IDBObject) this;
    if (objectID != 0L)
      dbObject = this.Session.GetObject(objectID, false);
    if (dbObject != null)
    {
      object[] valuesById2 = dbObject.GetValuesByID(ECOObject.idAttrChangeStart, false);
      if (valuesById2 != null && valuesById2.Length != 0 && valuesById2[0] != DBNull.Value)
        this._startDate = Convert.ToDateTime(valuesById2[0]);
      object[] valuesById3 = dbObject.GetValuesByID(ECOObject.idAttrChangeEnd, false);
      if (valuesById3 != null && valuesById3.Length != 0 && valuesById3[0] != DBNull.Value)
        this._endDate = Convert.ToDateTime(valuesById3[0]);
    }
    if (!(this._endDate == DateTime.MinValue))
      return;
    this._endDate = DateTime.MaxValue;
  }

  public DateTime StartDate
  {
    get
    {
      if (this._endDate == DateTime.MinValue)
        this.UpdateStartAndEndTimes();
      return this._startDate;
    }
  }

  public DateTime EndDate
  {
    get
    {
      if (this._endDate == DateTime.MinValue)
        this.UpdateStartAndEndTimes();
      return this._endDate;
    }
  }

  internal static void InitECOObject(IUserSession _taskSession)
  {
    if (_taskSession == null)
      return;
    IUserSession userSession = _taskSession;
    if (ECOObject.relTypeDI == 0)
      ECOObject.relTypeDI = userSession.GetRelationType(new Guid(ECOObject.guidLink_FromDI)).RelationType;
    if (ECOObject.idAttrDopIzv == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid(ECOObject.guidAttrDopIzv));
      if (attributeType != null)
        ECOObject.idAttrDopIzv = attributeType.AttributeID;
    }
    if (ECOObject.idAttrDopDesign == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid(ECOObject.guidAttrDopDesign));
      if (attributeType != null)
        ECOObject.idAttrDopDesign = attributeType.AttributeID;
    }
    if (ECOObject.idAttrChangeStart == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid(ECOObject.guidAttrChangeStart));
      if (attributeType != null)
        ECOObject.idAttrChangeStart = attributeType.AttributeID;
    }
    if (ECOObject.idAttrChangeEnd != 0)
      return;
    IDBAttributeType attributeType1 = userSession.GetAttributeType(new Guid(ECOObject.guidAttrChangeEnd));
    if (attributeType1 == null)
      return;
    ECOObject.idAttrChangeEnd = attributeType1.AttributeID;
  }

  protected void CheckSeriesDates()
  {
    SeriesDates sd = new SeriesDates(this.ObjectID);
    sd.LoadContextObjects(this.Session);
    if (sd.CheckForErrors(false) > 0)
      throw new Exception(sd.errList[0].ComposeMessage(sd));
  }

  protected override void DoNextLCStep(IDBLifecycleStep nextstep)
  {
    Guid guid = Guid.Empty;
    if (nextstep is IDBGuid)
      guid = (nextstep as IDBGuid).GUID;
    Guid g = new Guid(ECOServer.lcActualize);
    if (!guid.Equals(g) || ECOServer.ecos.lockDoNextLCStep)
    {
      bool flag = guid.ToString() == ECOServer.lcDeleting;
      if (flag)
        ECOServer.ecos.StartECODeletion(this.ObjectID);
      try
      {
        base.DoNextLCStep(nextstep);
      }
      finally
      {
        if (flag)
          ECOServer.ecos.EndECODeletion(this.ObjectID);
      }
    }
    else
    {
      if (this.Session.EnabledSeriesDates)
        this.CheckSeriesDates();
      List<ECOServer.IncludedObjInfo> objectsSteps = ECOServer.ecos.GetObjectsSteps(this.Session, this.ObjectID);
      string litera = ECOServer.ecos.GetLitera(this.ObjectID, this.Session);
      this.GetPreviousLiteras(this.Session, objectsSteps, litera);
      DateTime dateTime = DateTime.Now;
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid(ECOServer.attrChangeDate));
      if (attributeByGuid != null)
        dateTime = attributeByGuid.AsDateTime;
      if (dateTime > DateTime.Now)
      {
        base.DoNextLCStep(this.Session.GetLifecycleStep(ECOServer.ecos.lcWaitId));
      }
      else
      {
        IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("S.O.S.");
        if (sessionTemporaryClone == null)
          return;
        try
        {
          this.PerformNonLitera(sessionTemporaryClone, objectsSteps);
          if (!this.MoveObjects(nextstep, sessionTemporaryClone, objectsSteps, false) && (this.ObjectType == ECOServer.idII || this.ObjectType == ECOServer.idPI))
            ECOServer.ecos.MoveAnnuledPI((IDBObject) this, sessionTemporaryClone);
          if (objectsSteps.Count > 0)
            this.PerformChangeLitera(sessionTemporaryClone, objectsSteps, litera);
        }
        finally
        {
          sessionTemporaryClone.Logout("S.O.S.");
        }
        this.KeepReplacedPI();
      }
    }
  }

  public bool KeepReplacedPI()
  {
    IDBAttribute attributeById = this.GetAttributeByID(ECOServer.ecos.attrReasonObj);
    if (attributeById == null || attributeById.Value == null || attributeById.Value == DBNull.Value)
      return false;
    IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(attributeById.Value), false);
    if (dbObject == null)
      return false;
    IDBObjectType objectType = this.Session.GetObjectType(dbObject.ObjectType);
    if (objectType == null)
      return false;
    DataRow[] dataRowArray = this.Session.GetLCSchema(objectType.SchemaID).GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"].Select("F_LEVEL_ID = " + Convert.ToString(1001));
    int num = -1;
    string str = "cad00824-306c-11d8-b4e9-00304f19f545";
    foreach (DataRow dataRow in dataRowArray)
    {
      if (!(Convert.ToString(dataRow["F_GUID"]) == str))
      {
        num = Convert.ToInt32(dataRow["F_LC_STEP"]);
        break;
      }
    }
    if (num == -1)
      return false;
    dbObject.LCStep = num;
    return true;
  }

  protected override void DoCheckIn()
  {
    base.DoCheckIn();
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(ECOObject.guidAttrDelVersionsList, false);
    if (attributeByGuid == null)
      return;
    foreach (object obj in attributeByGuid.Values)
    {
      string g = Convert.ToString(obj);
      if (g != "")
      {
        IDBObject dbObject = this.UserSession.GetObject(new Guid(g), false);
        if (dbObject != null)
        {
          if (this.objectsToDelete == null)
            this.objectsToDelete = new List<long>();
          long objectId = dbObject.ObjectID;
          IDBObject objectActualCopy1 = this.UserSession.GetObjectActualCopy(objectId, false);
          if (objectActualCopy1 != null && LinkIzvObject.CanDeleteObject(objectActualCopy1) && !this.objectsToDelete.Contains(objectId))
            this.objectsToDelete.Add(objectId);
          if (objectId < 0L)
          {
            IDBObject objectActualCopy2 = this.UserSession.GetObjectActualCopy(-objectId, false);
            if (objectActualCopy2 != null && LinkIzvObject.CanDeleteObject(objectActualCopy2) && !this.objectsToDelete.Contains(-objectId))
              this.objectsToDelete.Add(-objectId);
          }
        }
      }
    }
    if (attributeByGuid.ValuesCount <= 1)
      return;
    attributeByGuid.ClearValues();
  }

  public long GetParentKI(IUserSession session)
  {
    return (this.UserSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).FindCompositionParentObject((object) this.UserSession, this.ObjectID, RevisionComplect.RevisionComplectRelation_TypeId, "cad001e2-306c-11d8-b4e9-00304f19f545");
  }

  protected override void DoBeforeAddAttribute(int attributeID, object[] initValues)
  {
    base.DoBeforeAddAttribute(attributeID, initValues);
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (this.LockCheckAttributes)
      return;
    if (attribute.AttributeID == RevisionComplect.Attr_Designation && this.GetParentKI((IUserSession) this.UserSession) != 0L && newValue != DBNull.Value)
      throw new Exception("Запрещено изменение аттрибута 'Обозначение' в извещениях включенных в состав КИ");
    if (attribute.AttributeID == RevisionComplect.Attr_TermOfChange && this.GetParentKI((IUserSession) this.UserSession) != 0L && newValue != DBNull.Value)
      throw new Exception("Запрещено изменение аттрибута 'Срок изменения' в извещениях включенных в состав КИ");
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }
}
