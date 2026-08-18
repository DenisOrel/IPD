// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.DocumentCopyService
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Archives.Server;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Archives.Copies;

public class DocumentCopyService : LongLifeObject, IDocumentCopyService
{
  public List<long> CreateCopies(long docID, int count, CopyKind copyKind, object sessionID)
  {
    List<long> copies = new List<long>();
    UserSession session = this.GetSession(sessionID);
    IDBObject dbObject1 = session.GetObject(docID, false);
    if (dbObject1 == null)
      return copies;
    IDBObjectCollection objectCollection = session.GetObjectCollection(ConstsHolder.CopyOfDocumentID);
    IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(new Guid("cad003a7-306c-11d8-b4e9-00304f19f545"), false);
    long asInteger1 = attributeByGuid1 == null ? 0L : attributeByGuid1.AsInteger;
    IDBAttribute attributeById1 = dbObject1.GetAttributeByID(ConstsHolder.A4ListNumberID);
    IDBAttribute attributeByGuid2 = dbObject1.GetAttributeByGuid(new Guid("cad00255-306c-11d8-b4e9-00304f19f545"), false);
    string str1 = attributeByGuid2 == null ? string.Empty : attributeByGuid2.AsString;
    IDBAttribute attributeByGuid3 = dbObject1.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
    string str2 = attributeByGuid3 == null ? string.Empty : attributeByGuid3.AsString;
    IDBAttribute attributeByGuid4 = dbObject1.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    string str3 = attributeByGuid4 == null ? string.Empty : attributeByGuid4.AsString;
    IDBAttribute attributeById2 = dbObject1.GetAttributeByID(ConstsHolder.EcoAttrID);
    long asInteger2 = attributeById2 == null ? 0L : attributeById2.AsInteger;
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) dbObject1.ID, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) ConstsHolder.IndexOfCopyID }, new object[1]
    {
      (object) ConstsHolder.IndexOfCopyID
    }, new SortOrders[1]{ SortOrders.DESC })
    {
      RecordCount = 1
    });
    int num1 = 0;
    if (dataTable != null && dataTable.Rows.Count == 1)
      num1 = Convert.ToInt32(dataTable.Rows[0][0]);
    for (; count > 0; --count)
    {
      IDBObject dbObject2 = objectCollection.Create();
      dbObject2.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(new Guid("cad003a7-306c-11d8-b4e9-00304f19f545")), false).Value = (object) asInteger1;
      IDBAttribute dbAttribute1 = dbObject2.Attributes.AddAttribute(ConstsHolder.A4ListNumberID, false);
      long num2 = 0;
      if (attributeById1 != null && attributeById1.Value != DBNull.Value)
        num2 = attributeById1.AsInteger;
      else if (str1 != string.Empty && asInteger1 != 0L)
      {
        if (str1.ToLower() == "a4" || str1.ToLower() == "а4")
          num2 = asInteger1;
        else if (str1.ToLower() == "a5" || str1.ToLower() == "а5")
          num2 = asInteger1 / 2L;
        else if (str1.ToLower() == "a3" || str1.ToLower() == "а3")
          num2 = asInteger1 * 2L;
        else if (str1.ToLower() == "a2" || str1.ToLower() == "а2")
          num2 = asInteger1 * 4L;
        else if (str1.ToLower() == "a1" || str1.ToLower() == "а1")
          num2 = asInteger1 * 8L;
        else if (str1.ToLower() == "a0" || str1.ToLower() == "а0")
          num2 = asInteger1 * 16L /*0x10*/;
      }
      // ISSUE: variable of a boxed type
      __Boxed<long> local = (System.ValueType) num2;
      dbAttribute1.Value = (object) local;
      dbObject2.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")), false).Value = (object) str2;
      IDBAttribute dbAttribute2 = dbObject2.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")), false);
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject2.ObjectType, MetaDataHelper.GetAttributeTypeID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")));
      if (attribute4ObjectType != null && attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue)
        dbAttribute2.Value = (object) str3;
      dbObject2.Attributes.AddAttribute(ConstsHolder.OriginalObjectVersionID, false).Value = (object) Math.Abs(docID);
      dbObject2.Attributes.AddAttribute(ConstsHolder.OriginalObjectID, false).Value = (object) dbObject1.ID;
      IDBAttribute dbAttribute3 = dbObject2.Attributes.AddAttribute(ConstsHolder.CopyKindAttrID, false);
      switch (copyKind)
      {
        case CopyKind.Hard:
          dbAttribute3.Value = (object) 1;
          break;
        case CopyKind.Electronic:
          dbAttribute3.Value = (object) 2;
          break;
      }
      if (asInteger2 != 0L)
      {
        try
        {
          dbObject2.Attributes.AddAttribute(ConstsHolder.EcoAttrID, false).Value = (object) asInteger2;
        }
        catch
        {
        }
      }
      ++num1;
      dbObject2.Attributes.AddAttribute(ConstsHolder.IndexOfCopyID, false).Value = (object) num1;
      dbObject2.CommitCreation(true);
      copies.Add(dbObject2.ObjectID);
    }
    return copies;
  }

  public void SendCopies(
    long subscriberID,
    long recipientID,
    long listID,
    List<long> copiesID,
    DateTime date,
    long albumID,
    object sessionID)
  {
    UserSession session = this.GetSession(sessionID);
    IDBObjectCollection objectCollection = session.GetObjectCollection(ConstsHolder.CopyOfDocumentID);
    ConditionStructure conditionStructure1 = new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false);
    ConditionStructure conditionStructure2 = new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscriberID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
    ConditionStructure conditionStructure3 = new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) 0L, LogicalOperators.AND, 0, false);
    ConditionStructure conditionStructure4 = new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.NotEqual, (object) 0L, LogicalOperators.AND, 0, false);
    IDBObject dbObject1 = session.GetObject(listID);
    IDBAttribute dbAttribute1 = dbObject1.Attributes.AddAttribute(ConstsHolder.SubscribersID, false);
    IDBAttribute dbAttribute2 = dbObject1.Attributes.AddAttribute(ConstsHolder.ActualCopyID, false);
    bool flag = false;
    for (int index = 0; index < dbAttribute1.ValuesCount; ++index)
    {
      if (dbAttribute1.Values[index] != DBNull.Value && Convert.ToInt64(dbAttribute1.Values[index]) == subscriberID)
      {
        dbAttribute2.Index = index;
        flag = true;
      }
    }
    session.StartTransaction();
    try
    {
      foreach (long num in copiesID)
      {
        IDBObject dbObject2 = session.GetObject(num);
        long asInteger1 = dbObject2.GetAttributeByID(ConstsHolder.OriginalObjectVersionID).AsInteger;
        IDBObject objectActualCopy = session.GetObjectActualCopy(asInteger1, false);
        long asInteger2 = dbObject2.GetAttributeByID(ConstsHolder.OriginalObjectID).AsInteger;
        if (!session.Configurations.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.ALLOW_SEND_COPIES, false, DBConfigMode.GlobalOnly))
        {
          conditionStructure3.Value = (object) asInteger2;
          conditionStructure4.Value = (object) asInteger1;
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[4]
          {
            conditionStructure1,
            conditionStructure2,
            conditionStructure3,
            conditionStructure4
          }, new object[1]{ (object) -2 });
          DataTable dataTable = objectCollection.Select(paramSet);
          if (dataTable != null && dataTable.Rows.Count > 0)
            throw new Exception(dataTable.Rows.Count > 1 ? string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_12"), (object) dbObject2.NameInMessages) : string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_13"), (object) dbObject2.NameInMessages));
        }
        dbObject2.Attributes.AddAttribute(ConstsHolder.AlbumSubscriberID, false).Value = (object) subscriberID;
        dbObject2.Attributes.AddAttribute(ConstsHolder.RecipientID, false).Value = recipientID != 0L ? (object) recipientID : (object) DBNull.Value;
        dbObject2.Attributes.AddAttribute(ConstsHolder.ReceiptDateID, false).Value = (object) date;
        dbObject2.LCStep = ConstsHolder.SendLCStepID;
        if (albumID != 0L)
          session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).Create(albumID, num);
        if (flag)
          dbAttribute2.Value = (object) num;
        if (session.Configurations.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.SUBSCR_NOTIFY, true) == "True" && session.GetCustomService(typeof (IRouterService)) is IRouterService customService)
        {
          string Subject1 = ArchivesServerHolder.rm.GetString("Archives.Server_26");
          string Text1 = string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_23") + ArchivesServerHolder.rm.GetString("Archives.Server_24") + ArchivesServerHolder.rm.GetString("Archives.Server_25"), (object) dbObject2.Caption, (object) objectActualCopy.ObjectGUID, (object) DataSetProcessor.QString(objectActualCopy.Caption), (object) session.UserName);
          List<long> subscribersUsers = this.GetSubscribersUsers(subscriberID, session);
          customService.CreateMessage(session.SessionGUID, subscribersUsers.ToArray(), Subject1, Text1, session.UserID);
          if (recipientID != 0L)
          {
            string Subject2 = ArchivesServerHolder.rm.GetString("Archives.Server_27");
            string Text2 = string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_28") + ArchivesServerHolder.rm.GetString("Archives.Server_24") + ArchivesServerHolder.rm.GetString("Archives.Server_25"), (object) dbObject2.Caption, (object) objectActualCopy.ObjectGUID, (object) DataSetProcessor.QString(objectActualCopy.Caption), (object) session.UserName);
            customService.CreateMessage(session.SessionGUID, recipientID, Subject2, Text2, session.UserID);
          }
        }
      }
      session.Commit();
      foreach (long objectID in copiesID)
      {
        IDBAttributeCollection attributes = session.GetObject(objectID).Attributes;
        List<int> intList = new List<int>();
        foreach (IDBAttribute dbAttribute3 in attributes.ToList())
        {
          IDBAttributeType attributeType = dbAttribute3.AttributeType;
          if (attributeType.MasterAttributeID != 0)
            intList.Add(attributeType.MasterAttributeID);
        }
        attributes.SetDependentAttributes(intList.ToArray());
      }
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
  }

  public Exception CopiesFastSending(object sessionID, List<long> copiesIds)
  {
    UserSession session = this.GetSession(sessionID);
    ICopiesService customService = session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
    string str = string.Empty;
    List<ErrorRecoveryAction> errorRecoveryActionList = new List<ErrorRecoveryAction>();
    foreach (long copiesId in copiesIds)
    {
      IDBObject dbObject = session.GetObject(copiesId, false);
      if (dbObject != null)
      {
        string caption = dbObject.Caption;
        IDBAttribute objectAttribute = session.GetObjectAttribute(copiesId, (object) ConstsHolder.AlbumSubscriberID, false, false);
        if (objectAttribute == null || objectAttribute.IsNull || objectAttribute.AsInteger == 0L)
        {
          str = !string.IsNullOrWhiteSpace(str) ? $"{str}; {string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_SetSubscriber"), (object) caption, (object) copiesId)}" : str + string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_SetSubscriber"), (object) caption, (object) copiesId);
          errorRecoveryActionList.Add((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(copiesId));
        }
        else
        {
          long asInteger1 = objectAttribute.AsInteger;
          IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.RecipientID);
          long num = 0;
          if (attributeById == null || attributeById.IsNull || attributeById.AsInteger == 0L)
          {
            if (MetaDataHelper.IsObjectTypeChildOf(session.GetObjectInfo(asInteger1).ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")))
            {
              num = asInteger1;
            }
            else
            {
              str = !string.IsNullOrWhiteSpace(str) ? $"{str}; {string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_SetSubscriber"), (object) caption, (object) copiesId)}" : str + string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_SetSubscriber"), (object) caption, (object) copiesId);
              errorRecoveryActionList.Add((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(copiesId));
              continue;
            }
          }
          if (num != 0L)
            num = attributeById.AsInteger;
          long asInteger2 = dbObject.GetAttributeByID(ConstsHolder.OriginalObjectID).AsInteger;
          long deliveryListId = customService.GetDeliveryListID(session.SessionGUID, Math.Abs(asInteger2));
          long subscriberID = asInteger1;
          long recipientID = num;
          long listID = deliveryListId;
          List<long> copiesID = new List<long>();
          copiesID.Add(copiesId);
          DateTime now = DateTime.Now;
          object sessionID1 = sessionID;
          this.SendCopies(subscriberID, recipientID, listID, copiesID, now, 0L, sessionID1);
        }
      }
    }
    return string.IsNullOrWhiteSpace(str) ? (Exception) null : new KernelExceptionID(463, (object) str).WithRecoveryActions(errorRecoveryActionList.ToArray());
  }

  private List<long> GetSubscribersUsers(long subscriberID, UserSession session)
  {
    int objectTypeId = session.GetObjectInfo(subscriberID).ObjectTypeID;
    bool flag = MetaDataHelper.IsObjectTypeChildOf(objectTypeId, ConstsHolder.OrganizationUnitsTypeID);
    List<long> subscribersUsers = new List<long>();
    if (objectTypeId == ConstsHolder.UserGroupTypeID)
      subscribersUsers = this.GetGroupUsersRecursive(subscriberID, session);
    else if (objectTypeId == ConstsHolder.SitesTypeID | flag)
    {
      long recipientFromAttr = this.GetCopyRecipientFromAttr(subscriberID, session);
      if (recipientFromAttr != 0L)
        subscribersUsers.Add(recipientFromAttr);
    }
    else if (objectTypeId == ConstsHolder.UsersTypeID)
      subscribersUsers.Add(subscriberID);
    return subscribersUsers;
  }

  private long GetCopyRecipientFromAttr(long subscriberID, UserSession session)
  {
    IDBObject dbObject = session.GetObject(subscriberID, false);
    if (dbObject == null)
      return 0;
    IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.RecipientID);
    return attributeById == null ? 0L : attributeById.AsInteger;
  }

  private List<long> GetGroupUsersRecursive(long groupID, UserSession session)
  {
    List<long> collection = new List<long>();
    DataTable dataTable = session.GetRelationCollection(ConstsHolder.RelTypeSimpleId).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) new ConditionStructure(-2, RelationalOperators.Equal, (object) ConstsHolder.UsersTypeID, LogicalOperators.NONE, 0, false), new object[1]
    {
      (object) -2
    }), groupID, true);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        collection.SafeAdd<long>(int64);
      }
    }
    return collection;
  }

  public void ReturnCopies(List<long> copiesID, long recipientID, DateTime date, object sessionID)
  {
    UserSession session = this.GetSession(sessionID);
    foreach (long num in copiesID)
    {
      IDBObject dbObject = session.GetObject(num, false);
      if (dbObject != null)
      {
        session.StartTransaction();
        try
        {
          dbObject.Attributes.AddAttribute(ConstsHolder.ReturnDateID, false).Value = (object) date;
          dbObject.Attributes.AddAttribute(ConstsHolder.WhoReturnID, false).Value = (object) recipientID;
          dbObject.LCStep = ConstsHolder.ReturnLCStepID;
          this.RemoveCopyIdFromDeliveryList(session, num);
          DataTable dataTable = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).EntersIn(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-7, RelationalOperators.Equal, (object) ConstsHolder.DocAlbumID, LogicalOperators.AND, 0, false)
          }, new object[1]{ (object) -20 }), dbObject.ID);
          if (dataTable != null && dataTable.Rows.Count == 1)
            session.GetRelation(Convert.ToInt64(dataTable.Rows[0][0])).Delete(0L);
          session.Commit();
        }
        catch (Exception ex)
        {
          session.Rollback();
          throw;
        }
      }
    }
  }

  public void RemoveCopiesReferences(List<long> copiesID, object sessionID)
  {
    UserSession session = this.GetSession(sessionID);
    foreach (long copyID in copiesID)
      this.RemoveCopyIdFromDeliveryList(session, copyID);
  }

  private void RemoveCopyIdFromDeliveryList(UserSession session, long copyID)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
    IDBObject dbObject1 = session.GetObject(copyID, false);
    if (dbObject1 == null || ConstsHolder.DeliveryListID == -1 || ConstsHolder.ActualCopyID == -10000)
      return;
    long asInteger1 = dbObject1.GetAttributeByID(ConstsHolder.OriginalObjectID).AsInteger;
    objectCollection.ObjectTypeID = ConstsHolder.DeliveryListID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) asInteger1, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable1 = objectCollection.Select(paramSet);
    if (dataTable1 == null || dataTable1.Rows.Count != 1)
      return;
    IDBObject dbObject2 = session.GetObject(Convert.ToInt64(dataTable1.Rows[0][0]), false);
    IDBAttribute attributeById1 = dbObject2.GetAttributeByID(ConstsHolder.ActualCopyID);
    long asInteger2 = dbObject1.GetAttributeByID(ConstsHolder.OriginalObjectVersionID).AsInteger;
    IDBAttribute attributeById2 = dbObject2.GetAttributeByID(ConstsHolder.SubscribersID);
    for (int index = 0; index < attributeById1.ValuesCount; ++index)
    {
      if (attributeById1.Values[index] != DBNull.Value && Convert.ToInt64(attributeById1.Values[index]) == dbObject1.ObjectID && attributeById2.Values[index] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(attributeById2.Values[index]);
        objectCollection.ObjectTypeID = ConstsHolder.CopyOfDocumentID;
        ConditionStructure conditionStructure1 = new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) asInteger2, LogicalOperators.AND, 0, false);
        ConditionStructure conditionStructure2 = new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) asInteger1, LogicalOperators.AND, 0, false);
        ConditionStructure conditionStructure3 = new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) int64, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
        ConditionStructure conditionStructure4 = new ConditionStructure(-2, RelationalOperators.NotEqual, (object) dbObject1.ObjectID, LogicalOperators.AND, 0, false);
        ConditionStructure conditionStructure5 = new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false);
        paramSet.Conditions = new ConditionStructure[5]
        {
          conditionStructure1,
          conditionStructure2,
          conditionStructure3,
          conditionStructure4,
          conditionStructure5
        };
        paramSet.RecordCount = 1;
        DataTable dataTable2 = objectCollection.Select(paramSet);
        attributeById1.Index = index;
        if (dataTable2 != null && dataTable2.Rows.Count == 1)
        {
          attributeById1.Value = dataTable2.Rows[0][0];
          break;
        }
        attributeById1.Value = (object) DBNull.Value;
        break;
      }
    }
  }

  public List<long> FormEnabledSubscribers(long listID, long docObjectID, object sessionID)
  {
    IUserSession session = (IUserSession) this.GetSession(sessionID);
    List<long> longList = new List<long>();
    IDBObject dbObject = session.GetObject(listID, false);
    if (dbObject != null)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(ConstsHolder.CopyOfDocumentID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.SubscribersID);
      if (attributeById == null)
        return longList;
      long asInteger = dbObject.GetAttributeByID(ConstsHolder.OriginalObjectID).AsInteger;
      ConditionStructure conditionStructure1 = new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) asInteger, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2 = new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.NotEqual, (object) Math.Abs(docObjectID), LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure3 = new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false);
      for (int index = 0; index < attributeById.ValuesCount; ++index)
      {
        if (attributeById.Values[index] != DBNull.Value && attributeById.Values[index] != null && !(attributeById.Values[index].ToString() == string.Empty))
        {
          long int64 = Convert.ToInt64(attributeById.Values[index]);
          ConditionStructure conditionStructure4 = new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) int64, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
          DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[4]
          {
            conditionStructure1,
            conditionStructure2,
            conditionStructure3,
            conditionStructure4
          }, new object[1]{ (object) -2 })
          {
            RecordCount = 1
          });
          if (dataTable == null || dataTable.Rows.Count == 0)
            longList.Add(int64);
        }
      }
    }
    return longList;
  }

  private UserSession GetSession(object sessionID)
  {
    return !(sessionID is UserSession) ? UserSession.GetSessionByID((Guid) sessionID) as UserSession : sessionID as UserSession;
  }
}
