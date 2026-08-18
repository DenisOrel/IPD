// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.LinkIzvObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Expert;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.Compositions;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.ECO.Server;

public class LinkIzvObject(UserSession uSession, DataTable objectsTable) : VerIzvObject(uSession, objectsTable)
{
  internal static int attrId = 0;
  internal static int attrVerId = 0;
  internal static int relTypeECO = 0;
  internal static int attrDelWhenExcluded = 0;
  internal static int attrFile = 0;
  public static readonly string guidAttrDelWhenExcluded = "cad00073-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrIncludeGoal = "cad007a3-306c-11d8-b4e9-00304f19f545";
  public static Guid gAttrAuxLinks = new Guid(ECOServer.attrAuxLinks);
  internal List<long> objectsToDelete;
  protected int IdRelSostav = -1;
  protected int IdRelDocs = -1;
  protected int IdTechSostav = -1;
  protected List<int> RelTypes;
  protected List<int> ObjTypes;
  protected ICompositionLoadService compositionLoadService;
  protected List<string> PossibleLiteras;
  protected int attrLiteraIndex = -1;

  internal static void Init(IUserSession _taskSession)
  {
    if (_taskSession == null)
      return;
    IUserSession userSession = _taskSession;
    if (LinkIzvObject.attrId == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid(LinkIzvObject.guidAttrDelWhenExcluded));
      if (attributeType != null)
        LinkIzvObject.attrId = attributeType.AttributeID;
    }
    if (LinkIzvObject.attrVerId == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
      if (attributeType != null)
        LinkIzvObject.attrVerId = attributeType.AttributeID;
    }
    if (LinkIzvObject.relTypeECO == 0)
      LinkIzvObject.relTypeECO = userSession.GetRelationType(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545")).RelationType;
    if (LinkIzvObject.attrDelWhenExcluded == 0)
    {
      IDBAttributeType attributeType = userSession.GetAttributeType(new Guid(LinkIzvObject.guidAttrDelWhenExcluded));
      if (attributeType != null)
        LinkIzvObject.attrDelWhenExcluded = attributeType.AttributeID;
    }
    if (LinkIzvObject.attrFile != 0)
      return;
    IDBAttributeType attributeType1 = userSession.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    if (attributeType1 == null)
      return;
    LinkIzvObject.attrFile = attributeType1.AttributeID;
  }

  protected override void DoPurge(long DeleteMode)
  {
    if (this.UserSession.GetObjectInfo(-this.ObjectID).Empty)
      return;
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(LinkIzvObject.relTypeECO);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(LinkIzvObject.attrId, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) -20,
      (object) LinkIzvObject.attrVerId
    });
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, this.ObjectID);
    List<long> longList = new List<long>();
    if (this.ObjectID < 0L)
    {
      paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) LinkIzvObject.attrVerId
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, -this.ObjectID).Rows)
        longList.Add(Convert.ToInt64(row[0]));
    }
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[1]);
      if (!longList.Contains(int64_1))
      {
        long int64_2 = Convert.ToInt64(row[0]);
        IDBRelation relation = this.UserSession.GetRelation(int64_2, false);
        if (relation != null)
        {
          ECOServer.ecos.StartLinkDeletion(int64_2);
          try
          {
            relation.Delete(0L);
          }
          finally
          {
            ECOServer.ecos.EndLinkDeletion(int64_2);
          }
        }
      }
    }
    base.DoPurge(DeleteMode);
  }

  protected override void DoDelete()
  {
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(LinkIzvObject.relTypeECO);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(LinkIzvObject.attrId, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) -20,
      (object) LinkIzvObject.attrVerId
    });
    relationCollection.LocalTypesMode = true;
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, this.ObjectID).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBRelation relation = this.UserSession.GetRelation(int64);
      if (relation != null)
      {
        ECOServer.ecos.StartLinkDeletion(int64);
        try
        {
          relation.Delete((long) (Intermech.Consts.PurgeMode | Intermech.Consts.DontCheckApplicabilityModes));
        }
        finally
        {
          ECOServer.ecos.EndLinkDeletion(int64);
        }
      }
    }
    base.DoDelete();
  }

  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    if (newrelation.RelationType != MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545") || newrelation.PartObjectID == 0L)
      return;
    int num1 = -1;
    IDBAttribute byGuid = newrelation.Attributes.FindByGUID(new Guid(LinkIzvObject.guidAttrIncludeGoal));
    if (byGuid != null)
      num1 = Convert.ToInt32(byGuid.Value);
    DBEditingContextsObject editingContextsObject = newrelation.ProjID == this.ObjectID ? (DBEditingContextsObject) this : (DBEditingContextsObject) null;
    if (editingContextsObject == null)
      return;
    IDBObject dbObject = this.UserSession.GetObjectActualCopy(newrelation.PartObjectID, false);
    if (dbObject == null)
      return;
    if (num1 != 1)
    {
      EditingContextsObjectContainer contextsObjectContainer = editingContextsObject.GetEditingContextsObjectContainer(false, this.UserSession.EnabledEditingContextsCache);
      long num2 = Math.Abs(dbObject.ObjectID);
      bool flag1 = contextsObjectContainer.ExistsObject(dbObject.ID);
      bool flag2 = contextsObjectContainer.ExistsVersion(num2, true);
      if (flag1 && !flag2)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("ECO_Server13") + LocalizationHolder.rm.GetString("ECO_Server14"), (object) num2, (object) dbObject.Caption));
      if (!flag2 && !ECOServer.ecos.IsAddContextDisabled(editingContextsObject.ContextID))
        editingContextsObject.ReplaceVersionID(num2, dbObject.ID, num2, true);
      if (ECOServer.ecos.IncludedIntoECO.ContainsKey(dbObject.ObjectType))
        ECOServer.ecos.IncludedIntoECO[dbObject.ObjectType]((IUserSession) this.UserSession, this.ObjectID, newrelation.RelationID, dbObject.ObjectID);
    }
    if (!(this is ECOObject))
      return;
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, ECOObject.Attr_EcoObject);
    if (attribute4ObjectType == null)
      return;
    if ((attribute4ObjectType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      dbObject = dbObject.CheckOut();
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ECOObject.Attr_EcoObject, false);
    if (dbAttribute == null)
      return;
    dbAttribute.AsInteger = newrelation.ProjID;
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    if (ECOServer.ecos.IsRevLocked(this.ObjectID) || relation.RelationType != MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545") || relation.PartObjectID == 0L)
      return;
    IDBAttribute attributeById1 = relation.GetAttributeByID(LinkIzvObject.attrDelWhenExcluded);
    if (attributeById1 == null || !attributeById1.AsBoolean)
    {
      this.ClearDates(relation);
      IDBAttribute byGuid = relation.Attributes.FindByGUID(new Guid(LinkIzvObject.guidAttrIncludeGoal));
      if (byGuid == null || byGuid.Value == DBNull.Value || Convert.ToInt32(byGuid.Value) != 4)
        return;
      IDBAttribute attributeByGuid = relation.GetAttributeByGuid(LinkIzvObject.gAttrAuxLinks, false);
      if (attributeByGuid == null || attributeByGuid.Values == null)
        return;
      IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) this;
      foreach (object obj in attributeByGuid.Values)
      {
        if (!obj.Equals((object) DBNull.Value))
        {
          long int64 = Convert.ToInt64(obj);
          editingContextsObject.DeleteFromContext(int64, true, true);
        }
      }
    }
    else
    {
      IDBAttribute attributeByGuid = relation.GetAttributeByGuid(LinkIzvObject.gAttrAuxLinks, false);
      if (attributeByGuid == null || attributeByGuid.Values == null)
        return;
      long num1 = 0;
      IDBAttribute attributeById2 = relation.GetAttributeByID(LinkIzvObject.attrVerId);
      if (attributeById2 != null && attributeById2.Value != DBNull.Value)
        num1 = Convert.ToInt64(attributeById2.Value);
      if (num1 == 0L || ECOServer.HasMultipleRevLinks((IUserSession) this.UserSession, num1))
        return;
      long num2 = Math.Abs(relation.PartObjectID);
      ECOServer.ecos.LockRevision(this.ObjectID);
      ECOServer.DeletingPackage pack = new ECOServer.DeletingPackage(deleteMode);
      try
      {
        List<long> longList = new List<long>();
        foreach (object obj in attributeByGuid.Values)
        {
          if (obj != null && obj != DBNull.Value)
          {
            long num3 = Math.Abs(Convert.ToInt64(obj));
            if (num3 != num2 && !longList.Contains(num3))
              longList.Add(num3);
          }
        }
        longList.Sort((Comparison<long>) ((i1, i2) =>
        {
          if (Math.Abs(i2) < Math.Abs(i1))
            return 1;
          return Math.Abs(i2) <= Math.Abs(i1) ? 0 : -1;
        }));
        IDBRelationCollection relationCollection = this.Session.GetRelationCollection(LinkIzvObject.relTypeECO);
        relationCollection.LocalTypesMode = true;
        DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[6]
        {
          (object) -20,
          (object) -22,
          (object) -2,
          (object) -21,
          (object) LinkIzvObject.attrVerId,
          (object) LinkIzvObject.attrDelWhenExcluded
        }), this.ObjectID);
        List<Tuple<long, long>> tupleList = new List<Tuple<long, long>>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long num4 = 0;
          if (row[0] != DBNull.Value)
            num4 = Convert.ToInt64(row[0]);
          if (num4 != relation.RelationID && num4 != 0L)
          {
            long num5 = 0;
            if (row[4] != DBNull.Value)
              num5 = Convert.ToInt64(row[4]);
            if (num5 == 0L)
              num5 = Convert.ToInt64(row[2]);
            long num6 = Math.Abs(num5);
            if (longList.Contains(num6))
              tupleList.Add(new Tuple<long, long>(num6, num4));
          }
        }
        tupleList.Sort((Comparison<Tuple<long, long>>) ((i1, i2) =>
        {
          if (Math.Abs(i2.Item1) < Math.Abs(i1.Item1))
            return 1;
          return Math.Abs(i2.Item1) <= Math.Abs(i1.Item1) ? 0 : -1;
        }));
        foreach (Tuple<long, long> tuple in tupleList)
        {
          long num7 = tuple.Item1;
          long aRelationID = tuple.Item2;
          if (aRelationID != 0L)
            this.Session.GetRelation(aRelationID, false)?.Delete(deleteMode);
          if (num7 != 0L)
          {
            long modelId = 0;
            IDBObject objectActualCopy = this.Session.GetObjectActualCopy(num7, false);
            if (objectActualCopy != null)
            {
              modelId = objectActualCopy.ObjectID;
              if (modelId > 0L && ECOServer.ecos.IsModelType(objectActualCopy.ObjectType))
                pack.AddModelId(modelId);
              else
                ECOServer.DeleteObject(objectActualCopy, deleteMode);
            }
            if (modelId != num7 && this.UserSession.GetRelation(Math.Abs(this.ObjectID), num7, RevRelation.ecoRelType) == null)
            {
              IDBObject dBObject = this.Session.GetObject(num7, false);
              if (LinkIzvObject.CanDeleteObject(dBObject))
              {
                if (ECOServer.ecos.IsModelType(dBObject.ObjectType))
                  pack.AddModelId(num7);
                else
                  ECOServer.DeleteObject(dBObject, deleteMode);
              }
            }
          }
          longList.Remove(num7);
        }
        foreach (long num8 in longList)
        {
          long modelId = 0;
          IDBObject objectActualCopy = this.Session.GetObjectActualCopy(num8, false);
          if (objectActualCopy != null)
          {
            modelId = objectActualCopy.ObjectID;
            if (modelId > 0L && ECOServer.ecos.IsModelType(objectActualCopy.ObjectType))
              pack.AddModelId(modelId);
            else
              ECOServer.DeleteObject(objectActualCopy, deleteMode);
          }
          if (modelId != num8)
          {
            IDBObject dBObject = this.Session.GetObject(num8, false);
            if (LinkIzvObject.CanDeleteObject(dBObject))
            {
              if (ECOServer.ecos.IsModelType(dBObject.ObjectType))
                pack.AddModelId(num8);
              else
                ECOServer.DeleteObject(dBObject, deleteMode);
            }
          }
        }
      }
      finally
      {
        ECOServer.ecos.AddDeletingPackage(num1, pack);
        ECOServer.ecos.AddSessionPackage(this.UserSession.SessionGUID, pack);
        ECOServer.ecos.UnlockRevision(this.ObjectID);
      }
    }
  }

  public static bool CanDeleteObject(IDBObject obj)
  {
    return obj != null && (obj as IDBLifecycleLevel).LevelID != obj.Session.IdentHelper.DeletedID;
  }

  private void ClearDates(IDBRelation rel)
  {
    long objectID = 0;
    IDBAttribute attributeById1 = rel.GetAttributeByID(LinkIzvObject.attrVerId);
    if (attributeById1 != null && attributeById1.Value != null)
      objectID = Convert.ToInt64(attributeById1.Value);
    if (objectID == 0L)
      return;
    IDBObject dbObject = this.Session.GetObject(objectID, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(ECOServer.ecos.attrChangeDateId);
    if (attributeById2 != null && attributeById2.Value != DBNull.Value)
      attributeById2.Value = (object) DBNull.Value;
    IDBAttribute attributeById3 = dbObject.GetAttributeByID(ECOServer.ecos.attrChangeDateEndId);
    if (attributeById3 != null && attributeById3.Value != DBNull.Value)
      attributeById3.Value = (object) DBNull.Value;
    IDBObject objectActualCopy = this.Session.GetObjectActualCopy(objectID, false);
    if (objectActualCopy == null || objectActualCopy.ObjectID == objectID)
      return;
    IDBAttribute attributeById4 = objectActualCopy.GetAttributeByID(ECOServer.ecos.attrChangeDateId);
    if (attributeById4 != null && attributeById4.Value != DBNull.Value)
      attributeById4.Value = (object) DBNull.Value;
    IDBAttribute attributeById5 = objectActualCopy.GetAttributeByID(ECOServer.ecos.attrChangeDateEndId);
    if (attributeById5 == null || attributeById5.Value == DBNull.Value)
      return;
    attributeById5.Value = (object) DBNull.Value;
  }

  public override void DoBeforeCreateRelation(
    DBRelationCollection dBRelationCollection,
    long partID,
    long partObjectID,
    long prjlinkID,
    IDBRelation prototype)
  {
    base.DoBeforeCreateRelation(dBRelationCollection, partID, partObjectID, prjlinkID, prototype);
  }

  protected override void DoCheckIn()
  {
    long projectID = Math.Abs(this.ObjectID);
    IDBRelationCollection relationCollection = this.Session.GetRelationCollection(LinkIzvObject.relTypeECO);
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[6]
    {
      (object) -20,
      (object) -22,
      (object) -2,
      (object) -21,
      (object) LinkIzvObject.attrVerId,
      (object) LinkIzvObject.attrDelWhenExcluded
    }), projectID);
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      bool flag = false;
      if (row[5] != null && row[5] != DBNull.Value)
        flag = Convert.ToBoolean(row[5]);
      if (flag)
      {
        long num1 = 0;
        if (row[0] != DBNull.Value)
          num1 = Convert.ToInt64(row[0]);
        long num2 = 0;
        if (row[4] != DBNull.Value)
          num2 = Convert.ToInt64(row[4]);
        if (num2 == 0L)
          num2 = Convert.ToInt64(row[2]);
        long key = Math.Abs(num2);
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, num1);
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[6]
    {
      (object) -20,
      (object) -22,
      (object) -2,
      (object) -21,
      (object) LinkIzvObject.attrVerId,
      (object) LinkIzvObject.attrDelWhenExcluded
    }), this.ObjectID).Rows)
    {
      long num = 0;
      if (row[4] != DBNull.Value)
        num = Convert.ToInt64(row[4]);
      if (num == 0L)
        num = Convert.ToInt64(row[2]);
      long key = Math.Abs(num);
      if (dictionary.ContainsKey(key))
        dictionary.Remove(key);
    }
    if (dictionary.Keys.Count > 0)
    {
      this.objectsToDelete = new List<long>();
      foreach (long key in dictionary.Keys)
      {
        IDBAttribute attributeById = ((DBRelation) this.Session.GetRelation(dictionary[key])).GetAttributeByID(ECOServer.ecos.attrAuxLinksId);
        if (attributeById != null && attributeById.Value != DBNull.Value)
        {
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            if (!attributeById.Values[index].IsNullOrDBNull())
            {
              long int64 = Convert.ToInt64(attributeById.Values[index]);
              if (this.Session.GetObject(int64, false) != null && !this.objectsToDelete.Contains(int64))
                this.objectsToDelete.Add(int64);
              if (this.Session.GetObject(-int64, false) != null && !this.objectsToDelete.Contains(-int64))
                this.objectsToDelete.Add(-int64);
            }
          }
        }
        if (!this.objectsToDelete.Contains(key))
          this.objectsToDelete.Add(key);
      }
    }
    base.DoCheckIn();
  }

  protected override void DoAfterCheckInCommited()
  {
    if (this.objectsToDelete != null && this.objectsToDelete.Count != 0)
    {
      bool flag;
      do
      {
        int index = 0;
        flag = false;
        this.objectsToDelete.Sort((Comparison<long>) ((x, y) => (int) (y - x)));
        while (index < this.objectsToDelete.Count)
        {
          IDBObject dBObject = this.Session.GetObject(this.objectsToDelete[index], false);
          try
          {
            if (dBObject != null)
              ECOServer.DeleteObject(dBObject, 0L);
            this.objectsToDelete.RemoveAt(0);
            flag = true;
          }
          catch
          {
            if (((UserSession) this.Session).InTransaction)
              throw;
            ++index;
          }
        }
      }
      while (flag);
      if (this.objectsToDelete.Count > 0)
      {
        StringBuilder sb = new StringBuilder();
        this.objectsToDelete.ForEach((Action<long>) (verId => sb.Append(sb.Length == 0 ? Convert.ToString(verId) : ", " + Convert.ToString(verId))));
        sb.Insert(0, LocalizationHolder.rm.GetString("ECO_Server16"));
        throw new Exception(sb.ToString());
      }
    }
    base.DoAfterCheckInCommited();
  }

  protected bool MoveObjects(
    IDBLifecycleStep nextstep,
    IUserSession ius,
    List<ECOServer.IncludedObjInfo> includedObjs,
    bool checkFailedAttr = true)
  {
    bool flag = false;
    bool failed = false;
    if (ECOServer.ecos.AutoMoveObjects)
    {
      if (checkFailedAttr)
      {
        IDBAttribute byId = this.Attributes.FindByID(ECOServer.ecos.attrLCFailedId);
        if (byId != null && byId.AsBoolean)
          flag = true;
      }
      List<long> objects = new List<long>(includedObjs.Select<ECOServer.IncludedObjInfo, long>((System.Func<ECOServer.IncludedObjInfo, long>) (ioi => ioi.ObjId)));
      if (!flag && ECOServer.ecos.AutoMoveObjects)
      {
        ((UserSession) ius).StartTransaction();
        try
        {
          DateTime result = new DateTime(3000, 12, 12);
          IDBAttribute attributeById = this.GetAttributeByID(ECOServer.ecos.attrChangeDateId);
          if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
            DateTime.TryParse(Convert.ToString(attributeById.Value), out result);
          if (this.ObjectType == ECOServer.idII)
            ECOServer.ecos.DoSetEndTerms((UserSession) ius, includedObjs, result);
          failed = !ECOServer.ecos.MoveObjects((UserSession) ius, includedObjs);
          if (!failed)
            this.DoNextLCStep(nextstep);
          ((UserSession) ius).Commit();
        }
        catch (Exception ex)
        {
          ((UserSession) ius).Rollback();
          ECOServer.SendExceptionMessage(this.Session, this.OwnerID, objects, this.ObjectID, ex);
          throw;
        }
      }
      if (ECOServer.ecos.WarnOnMove | failed)
        ECOServer.SendMessage(this.Session, this.OwnerID, objects, this.ObjectID, ECOServer.ecos.AutoMoveObjects, failed);
    }
    else
      this.DoNextLCStep(nextstep);
    return flag | failed;
  }

  protected void CollectLiteraData(
    IUserSession ius,
    List<ObjInfoItem> curRoots,
    string litera,
    Dictionary<int, List<long>> collection)
  {
    if (this.IdRelSostav == -1)
    {
      this.IdRelSostav = MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
      this.IdRelDocs = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
      this.IdTechSostav = MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545");
      this.RelTypes = new List<int>()
      {
        this.IdRelSostav,
        this.IdRelDocs,
        this.IdTechSostav
      };
      this.ObjTypes = new List<int>();
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00133-306c-11d8-b4e9-00304f19f545")));
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad0025e-306c-11d8-b4e9-00304f19f545")));
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad0025f-306c-11d8-b4e9-00304f19f545")));
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00132-306c-11d8-b4e9-00304f19f545")));
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00250-306c-11d8-b4e9-00304f19f545")));
      this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad0057f-306c-11d8-b4e9-00304f19f545")));
      this.compositionLoadService = ApplicationServices.Container.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
    }
    foreach (ObjInfoItem curRoot in curRoots)
    {
      List<long> longList;
      if (collection.TryGetValue(curRoot.ObjTypeID, out longList))
      {
        if (!longList.Contains(curRoot.ObjectID))
          longList.Add(curRoot.ObjectID);
      }
      else
        collection.Add(curRoot.ObjTypeID, new List<long>()
        {
          curRoot.ObjectID
        });
    }
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 0),
      new ColumnDescriptor((object) new Guid(ECOServer.GuidAttrLitera), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 0)
    };
    DataTable dataTable = new CompositionTaskBooster(this.Session, this.compositionLoadService).Execute(new CompositionLoadingParams((IEnumerable<ObjInfoItem>) curRoots, (IEnumerable<int>) this.ObjTypes, (IEnumerable<int>) null, (IEnumerable<int>) this.RelTypes, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, 1, (VersionsRule) null, "cad00601-306c-11d8-b4e9-00304f19f545"));
    if (dataTable == null)
      return;
    int num1 = this.PossibleLiteras.IndexOf(litera);
    List<ObjInfoItem> curRoots1 = (List<ObjInfoItem>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long num2 = Math.Abs(Convert.ToInt64(row[0]));
      if (!this._ContainsObjId(collection, num2))
      {
        if (row[2] != null && row[2] != DBNull.Value)
        {
          string str = Convert.ToString(row[2]);
          if (str != "" && this.PossibleLiteras.IndexOf(str) >= num1)
            continue;
        }
        if (curRoots1 == null)
          curRoots1 = new List<ObjInfoItem>();
        int int32 = Convert.ToInt32(row[1]);
        curRoots1.Add(new ObjInfoItem(num2, int32));
      }
    }
    if (curRoots1 == null)
      return;
    this.CollectLiteraData(ius, curRoots1, litera, collection);
  }

  private bool _ContainsObjId(Dictionary<int, List<long>> collection, long objId)
  {
    foreach (KeyValuePair<int, List<long>> keyValuePair in collection)
    {
      if (keyValuePair.Value.Contains(objId))
        return true;
    }
    return false;
  }

  protected void ProcessObjectType(
    int objType,
    IUserSession ius,
    IEnumerable<long> objList,
    string litera,
    HashSet<long> initialObjIds)
  {
    if (MetaDataHelper.GetAttribute4ObjectType(objType, this.attrLiteraIndex) == null)
      return;
    bool flag1 = MetaDataHelper.GetAttribute4ObjectType(objType, ECOServer.ecos.attrChangeNo) != null;
    bool flag2 = MetaDataHelper.GetAttribute4ObjectType(objType, ECOObject.Attr_EcoObject) != null;
    foreach (long num1 in objList)
    {
      IDBObject idbO = ius.GetObject(num1, false);
      if (idbO != null)
      {
        try
        {
          IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(this.attrLiteraIndex, false);
          if (dbAttribute != null)
            dbAttribute.AsString = litera;
        }
        catch (KernelException ex)
        {
        }
        if (flag1 && !initialObjIds.Contains(num1))
        {
          try
          {
            IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(ECOServer.ecos.attrChangeNo, false);
            if (dbAttribute != null)
            {
              long num2 = ECOServer.ecos.GetNewChangeNo(idbO.ID, num1);
              try
              {
                long int64 = Convert.ToInt64(dbAttribute.AsString);
                if (num2 <= int64)
                  num2 = int64 + 1L;
              }
              catch (FormatException ex)
              {
              }
              dbAttribute.AsString = num2.ToString();
            }
          }
          catch (KernelException ex)
          {
          }
        }
        if (flag2)
        {
          try
          {
            IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(ECOObject.Attr_EcoObject, false);
            if (dbAttribute != null)
              dbAttribute.AsInteger = this.ObjectID;
          }
          catch (KernelException ex)
          {
          }
        }
        if (ECOServer.ecos.ep.MoveAuthenticFiles)
          this.DeAuthentificateFile(idbO);
      }
    }
  }

  protected void PerformChangeLitera(
    IUserSession ius,
    List<ECOServer.IncludedObjInfo> includedObjs,
    string litera)
  {
    if (litera == "" || includedObjs.All<ECOServer.IncludedObjInfo>((System.Func<ECOServer.IncludedObjInfo, bool>) (ioi => ioi.Goal != ECOServer.EcoGoal.Litera)) || this.PossibleLiteras == null)
      return;
    HashSet<long> longSet = new HashSet<long>();
    foreach (ECOServer.IncludedObjInfo includedObj in includedObjs)
    {
      if (includedObj.Goal == ECOServer.EcoGoal.NoGoal)
        longSet.Add(includedObj.ObjId);
    }
    int num1 = this.PossibleLiteras.IndexOf(litera);
    Dictionary<string, HashSet<ObjInfoItem>> dictionary1 = new Dictionary<string, HashSet<ObjInfoItem>>();
    foreach (ECOServer.IncludedObjInfo includedObj in includedObjs)
    {
      if (includedObj.Goal == ECOServer.EcoGoal.Litera)
      {
        IDBObject dbObject1 = ius.GetObject(includedObj.ObjId, false);
        if (dbObject1 != null)
        {
          string key = litera;
          int num2 = num1;
          if (includedObj.PrevLitera != "" && this.PossibleLiteras.IndexOf(includedObj.PrevLitera) > num2)
          {
            key = includedObj.PrevLitera;
            num2 = this.PossibleLiteras.IndexOf(includedObj.PrevLitera);
          }
          IDBAttribute attributeById1 = dbObject1.GetAttributeByID(this.attrLiteraIndex);
          if (attributeById1 == null || this.PossibleLiteras.IndexOf(attributeById1.AsString) < num2)
          {
            HashSet<ObjInfoItem> objInfoItemSet = (HashSet<ObjInfoItem>) null;
            if (!dictionary1.TryGetValue(key, out objInfoItemSet))
            {
              objInfoItemSet = new HashSet<ObjInfoItem>();
              dictionary1.Add(key, objInfoItemSet);
            }
            objInfoItemSet.Add(new ObjInfoItem(includedObj.ObjId, dbObject1.ObjectType));
            if (includedObj.AuxObjects != null)
            {
              foreach (long auxObject in includedObj.AuxObjects)
              {
                if (!longSet.Contains(auxObject))
                {
                  IDBObject dbObject2 = ius.GetObject(auxObject, false);
                  if (dbObject2 != null)
                  {
                    IDBAttribute attributeById2 = dbObject2.GetAttributeByID(this.attrLiteraIndex);
                    if (attributeById2 == null || this.PossibleLiteras.IndexOf(attributeById2.AsString) < num2)
                      objInfoItemSet.Add(new ObjInfoItem(auxObject, dbObject2.ObjectType));
                  }
                }
              }
            }
          }
        }
      }
    }
    Dictionary<string, Dictionary<int, List<long>>> dictionary2 = new Dictionary<string, Dictionary<int, List<long>>>();
    foreach (string key in dictionary1.Keys)
      dictionary2.Add(key, new Dictionary<int, List<long>>());
    if (ECOServer.ecos.LiteraFullSostav)
    {
      long editingContextId = ius.EditingContextID;
      ius.EditingContextID = this.ObjectID;
      try
      {
        foreach (string key in dictionary1.Keys)
          this.CollectLiteraData(ius, dictionary1[key].ToList<ObjInfoItem>(), key, dictionary2[key]);
      }
      finally
      {
        ius.EditingContextID = editingContextId;
      }
    }
    else
    {
      foreach (string key in dictionary1.Keys)
      {
        foreach (ObjInfoItem objInfoItem in dictionary1[key])
        {
          List<long> longList = (List<long>) null;
          if (dictionary2[key].TryGetValue(objInfoItem.ObjTypeID, out longList))
            longList.Add(objInfoItem.ObjectID);
          else
            dictionary2[key].Add(objInfoItem.ObjTypeID, new List<long>()
            {
              objInfoItem.ObjectID
            });
        }
      }
    }
    foreach (string key in dictionary1.Keys)
    {
      HashSet<long> initialObjIds = new HashSet<long>();
      foreach (ECOServer.IncludedObjInfo includedObj in includedObjs)
      {
        if (includedObj.PrevLitera == key || includedObj.PrevLitera == "" && key == litera)
        {
          initialObjIds.Add(includedObj.ObjId);
          if (includedObj.AuxObjects != null)
          {
            foreach (long auxObject in includedObj.AuxObjects)
              initialObjIds.Add(auxObject);
          }
        }
      }
      foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary2[key])
        this.ProcessObjectType(keyValuePair.Key, ius, (IEnumerable<long>) keyValuePair.Value, key, initialObjIds);
    }
  }

  protected void PerformNonLitera(IUserSession ius, List<ECOServer.IncludedObjInfo> includedObjs)
  {
    Dictionary<long, string> prevLiteras = new Dictionary<long, string>();
    HashSet<long> longSet = new HashSet<long>();
    foreach (ECOServer.IncludedObjInfo includedObj in includedObjs)
    {
      if (includedObj.Goal != ECOServer.EcoGoal.Litera)
      {
        this._PerformObj(ius, prevLiteras, includedObj.ObjId);
        if (includedObj.AuxObjects != null)
        {
          foreach (long auxObject in includedObj.AuxObjects)
            this._PerformObj(ius, prevLiteras, auxObject);
        }
      }
    }
    foreach (KeyValuePair<long, string> keyValuePair in prevLiteras)
    {
      if (!longSet.Contains(keyValuePair.Key))
      {
        IDBObject dbObject = ius.GetObject(keyValuePair.Key, false);
        if (dbObject != null)
        {
          try
          {
            longSet.Add(keyValuePair.Key);
            ECOServer.SetObjectLitera(dbObject, this.attrLiteraIndex, keyValuePair.Value);
          }
          catch (KernelException ex)
          {
          }
        }
      }
    }
  }

  protected void DeAuthentificateFile(IDBObject idbO)
  {
    IDBAttribute attributeById = idbO.GetAttributeByID(LinkIzvObject.attrFile);
    if (attributeById == null)
      return;
    IBlobReader blobReader1 = (IBlobReader) null;
    BlobInformation blobInfo = new BlobInformation();
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById is IBlobReader blobReader2)
      {
        blobInfo = blobReader2.OpenBlob(-1);
        if (blobInfo.FileType == FileTypes.ftAuthentical)
        {
          blobReader1 = blobReader2;
          break;
        }
      }
    }
    if (blobReader1 == null)
      return;
    string input = Path.GetFileNameWithoutExtension(blobInfo.FileName);
    string str = Path.GetExtension(blobInfo.FileName);
    Group group = new Regex("\\[[\\w\\s\\/-]+\\]$", RegexOptions.IgnoreCase).Match(input).Groups[0];
    if (group.Success)
      input = input.Substring(0, group.Index);
    blobInfo.FileName = $"{input} [{DateTime.Now.ToString("dd/MM/y HH-mm")}]{str}";
    blobInfo.FileType = FileTypes.ftNotContent;
    (attributeById as IBlobWriter).OpenBlob(blobInfo, true);
    if (blobReader1.BlobState == BlobAttributeStates.Closed)
      return;
    blobReader1.CloseBlob();
  }

  protected void _PerformObj(IUserSession ius, Dictionary<long, string> prevLiteras, long objId)
  {
    if (prevLiteras.ContainsKey(objId))
      return;
    prevLiteras.Add(objId, this._GetPrevLitera(ius, objId));
  }

  protected string _GetPrevLitera(IUserSession ius, long objId, int literaIndex = -1)
  {
    foreach (DataRow row in (InternalDataCollectionBase) ius.GetAllObjectVersions(objId, false, false, false).Rows)
    {
      if (MetaDataHelper.GetLCStep(Convert.ToInt32(row["F_LC_STEP"])).LevelID == 5)
      {
        long int32 = (long) Convert.ToInt32(row["F_OBJECT_ID"]);
        IDBObject dbObject = ius.GetObject(int32, false);
        if (dbObject != null)
        {
          object[] valuesById = dbObject.GetValuesByID(this.attrLiteraIndex, false);
          if (valuesById != null && valuesById.Length != 0)
          {
            string prevLitera = Convert.ToString(valuesById[0]);
            if (this.PossibleLiteras.IndexOf(prevLitera) > literaIndex)
              return prevLitera;
          }
        }
      }
    }
    return "";
  }

  protected void GetPreviousLiteras(
    IUserSession ius,
    List<ECOServer.IncludedObjInfo> includedObjs,
    string litera)
  {
    this.attrLiteraIndex = MetaDataHelper.GetAttributeTypeID(new Guid(ECOServer.GuidAttrLitera));
    this.PossibleLiteras = new List<string>(MetaDataHelper.GetAttributeType(this.attrLiteraIndex).PossibleValues.Cast<string>());
    if (litera == "" || includedObjs.All<ECOServer.IncludedObjInfo>((System.Func<ECOServer.IncludedObjInfo, bool>) (ioi => ioi.Goal != ECOServer.EcoGoal.Litera)))
      return;
    int literaIndex = this.PossibleLiteras.IndexOf(litera);
    foreach (ECOServer.IncludedObjInfo includedObj in includedObjs)
    {
      if (includedObj.Goal == ECOServer.EcoGoal.Litera)
      {
        IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(includedObj.FutureStepId, false);
        if (lifecycleStep != null && lifecycleStep.LevelID == 5)
          includedObj.PrevLitera = this._GetPrevLitera(ius, includedObj.ObjId, literaIndex);
      }
    }
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.ChangeECOFutureStep, false);
  }
}
