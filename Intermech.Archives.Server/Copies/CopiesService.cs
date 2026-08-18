// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopiesService
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Archives.Server;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Archives.Copies;

public class CopiesService : LongLifeObject, ICopiesService
{
  private Dictionary<int, Dictionary<long, int>> SubscribersDictionary = new Dictionary<int, Dictionary<long, int>>();
  private Dictionary<int, string> InventoryDictionary = new Dictionary<int, string>();
  private List<long> ClassifiersList = new List<long>();
  private object SyncObject = new object();

  public CopiesService()
  {
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("copiesService");
    try
    {
      MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.DocTypeID);
      foreach (DataRow row in (InternalDataCollectionBase) sessionTemporaryClone.Configurations.ReadSection(ConstsHolder.MODULE_NAME, ConstsHolder.SUBSCRIBERS_SECTION_NAME, 0L).Rows)
      {
        int int32_1 = Convert.ToInt32(row[0]);
        string[] strArray1 = Convert.ToString(row[1]).Split(';');
        Dictionary<long, int> list = new Dictionary<long, int>();
        int num = 0;
        foreach (string str in strArray1)
        {
          char[] chArray = new char[1]{ '|' };
          string[] strArray2 = str.Split(chArray);
          if (strArray2.Length == 2)
          {
            long int64 = Convert.ToInt64(strArray2[0]);
            if (sessionTemporaryClone.GetObject(int64, false) != null)
            {
              int int32_2 = Convert.ToInt32(strArray2[1]);
              list.Add(int64, int32_2);
            }
            else
              ++num;
          }
        }
        this.SubscribersDictionary.Add(int32_1, list);
        if (num > 0)
          this.ChangeSubscribers(int32_1, list, (object) sessionTemporaryClone.SessionGUID);
      }
      DataTable dataTable = sessionTemporaryClone.Configurations.ReadSection(ConstsHolder.MODULE_NAME, ConstsHolder.INVENTORY_SECTION_NAME, 0L);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.InventoryDictionary.Add(Convert.ToInt32(row[0]), Convert.ToString(row[1]));
      }
      string str1 = sessionTemporaryClone.Configurations.ReadString(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.CLASSIFIERS, string.Empty, DBConfigMode.GlobalOnly);
      char[] chArray1 = new char[1]{ ';' };
      foreach (string s in str1.Split(chArray1))
      {
        long num = 0;
        ref long local = ref num;
        if (long.TryParse(s, out local))
          this.ClassifiersList.Add(num);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("copiesService");
    }
  }

  public bool DocumentHasCopies(long objectId, object sessionID)
  {
    IDBObjectCollection objectCollection = (!(sessionID is IUserSession userSession) ? UserSession.GetSessionByID((Guid) sessionID) : userSession).GetObjectCollection(ConstsHolder.CopyOfDocumentID);
    ConditionStructure conditions = new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) Math.Abs(objectId), LogicalOperators.AND, 0, false);
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditions
    }, columns);
    return objectCollection.RecordsExists((ConditionStructure[]) conditions);
  }

  public List<long> GetDocumentsCopies(long objectId, object sessionID)
  {
    DataTable dataTable = (!(sessionID is IUserSession userSession) ? UserSession.GetSessionByID((Guid) sessionID) : userSession).GetObjectCollection(ConstsHolder.CopyOfDocumentID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) Math.Abs(objectId), LogicalOperators.AND, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null || dataTable.Rows.Count == 0)
      return new List<long>();
    List<long> collection = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      collection.SafeAdd<long>(int64);
    }
    return collection;
  }

  public void SetInventoryNumberAttributes(
    Guid sessionGuid,
    long objectId,
    string inventoryNumber,
    DateTime registrationDateTime,
    out AttributeValues invNumberAttrValues)
  {
    IDBObject dbObject1 = UserSession.GetSessionByID(sessionGuid) is UserSession sessionById ? sessionById.GetObject(objectId) : throw new KernelException($"Не найдена пользовательская сессия с ГУИДом {sessionGuid}");
    if (dbObject1.ParentVersionID != -1L)
    {
      IDBObject dbObject2 = sessionById.GetObject(dbObject1.ParentVersionID, false);
      IDBAttribute attributeById = dbObject2.GetAttributeByID(ConstsHolder.InventoryNumberID);
      if (attributeById != null && !string.IsNullOrEmpty(attributeById.AsString))
      {
        dbObject1.TryToAddOrDelAttribute(ConstsHolder.PreviousInventoryNumberID, (object) attributeById.AsString);
        dbObject2.TryToAddOrDelAttribute(ConstsHolder.NewInventoryNumberID, (object) inventoryNumber);
      }
    }
    dbObject1.TryToAddOrDelAttribute(ConstsHolder.InventoryNumberID, (object) inventoryNumber);
    invNumberAttrValues = new AttributeValues(ConstsHolder.InventoryNumberID, (object) inventoryNumber);
    dbObject1.TryToAddOrDelAttribute(ConstsHolder.OTDRegisteredDateID, (object) registrationDateTime);
  }

  public string GetWarningAboutExceededCopies(
    Dictionary<long, int> copiesForDocsCount,
    long subsriberId,
    Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    string empty = string.Empty;
    foreach (KeyValuePair<long, int> keyValuePair in copiesForDocsCount)
    {
      long deliveryListId = this.GetDeliveryListID(sessionGuid, keyValuePair.Key);
      if (deliveryListId != 0L)
      {
        IDBObject dbObject = sessionById.GetObject(deliveryListId, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(ConstsHolder.SubscribersID);
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
          int index1 = -1;
          for (int index2 = 0; index2 < attributeById1.ValuesCount; ++index2)
          {
            if (attributeById1.Values[index2] != DBNull.Value && Convert.ToInt64(attributeById1.Values[index2]) == subsriberId)
            {
              index1 = index2;
              break;
            }
          }
          if (index1 != -1)
          {
            attributeById1.Index = index1;
            int int32_1 = Convert.ToInt32(attributeById2.Values[index1]);
            DataTable dataTable = sessionById.ObjectsSelect(ConstsHolder.CopyOfDocumentID, new DBRecordSetParams(new ConditionStructure[3]
            {
              new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false),
              new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subsriberId, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
              new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) keyValuePair.Key, LogicalOperators.NONE, 0, false)
            }, (object[]) null)
            {
              RecordCount = 0
            });
            int int32_2 = dataTable == null || dataTable.Rows.Count != 1 ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
            int num = keyValuePair.Value;
            if (int32_2 + num > int32_1)
            {
              string caption = sessionById.GetObjectByID(keyValuePair.Key, false).Caption;
              empty += $"Отправка копий документа {caption} \r\n";
              empty += $"Абонент: {attributeById1.AsString} \r\n";
              empty += $"Количество копий, которые должны быть высланы абоненту: {int32_1} \r\n";
              empty += $"Количество высланных копий: {int32_2} \r\n";
              empty += $"Количество высылаемых копий: {num} \r\n \r\n";
            }
          }
        }
      }
    }
    return empty;
  }

  public long CreateDeliveryList(Guid sessionGuid, long docObjectID)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return 0;
    IDBObject dbObject1 = sessionById.GetObject(docObjectID, false);
    if (dbObject1 == null)
      return 0;
    IDBObject dbObject2 = sessionById.GetObjectCollection(ConstsHolder.DeliveryListID).Create();
    DateTime now = DateTime.Now;
    long ownerId = dbObject1.OwnerID;
    Dictionary<long, int> subscribers = this.GetSubscribers(dbObject1.ObjectType);
    object[] initValue1 = new object[subscribers.Count];
    object[] initValue2 = new object[subscribers.Count];
    object[] initValue3 = new object[subscribers.Count];
    object[] initValue4 = new object[subscribers.Count];
    object[] initValue5 = new object[subscribers.Count];
    IDictionaryEnumerator enumerator = (IDictionaryEnumerator) subscribers.GetEnumerator();
    if (subscribers.Count == 0)
    {
      initValue1 = (object[]) null;
      initValue2 = (object[]) null;
      initValue3 = (object[]) null;
      initValue4 = (object[]) null;
      initValue5 = (object[]) null;
    }
    else
    {
      int index = 0;
      while (enumerator.MoveNext())
      {
        initValue1[index] = enumerator.Key;
        initValue2[index] = enumerator.Value;
        initValue3[index] = (object) ownerId;
        initValue4[index] = (object) now;
        initValue5[index] = (object) null;
        ++index;
      }
    }
    AttributeValues attributeValues1 = new AttributeValues(ConstsHolder.OriginalObjectID, (object) dbObject1.ID);
    AttributeValues attributeValues2 = new AttributeValues(ConstsHolder.SubscribersDateID, (object) initValue4);
    AttributeValues attributeValues3 = new AttributeValues(ConstsHolder.ListOwnerID, (object) initValue3);
    AttributeValues attributeValues4 = new AttributeValues(ConstsHolder.SubscribersID, (object) initValue1);
    AttributeValues attributeValues5 = new AttributeValues(ConstsHolder.NumberOfCopiesID, (object) initValue2);
    AttributeValues attributeValues6 = new AttributeValues(ConstsHolder.ActualCopyID, (object) initValue5);
    string initValue6 = string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_19"), (object) MetaDataHelper.GetObjectName(dbObject2.ObjectType), (object) dbObject1.NameInMessages);
    AttributeValues attributeValues7 = new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) initValue6);
    dbObject2.SetAttributesValues(new AttributeValues[7]
    {
      attributeValues1,
      attributeValues2,
      attributeValues3,
      attributeValues4,
      attributeValues5,
      attributeValues6,
      attributeValues7
    });
    dbObject2.CommitCreation(true);
    return dbObject2.ObjectID;
  }

  public long GetDeliveryListID(Guid sessionGuid, long id)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return 0;
    long deliveryListId = 0;
    if (ConstsHolder.DeliveryListID == 0 || ConstsHolder.OriginalObjectID == -10000)
      return deliveryListId;
    DataTable dataTable = sessionById.GetObjectCollection(ConstsHolder.DeliveryListID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) id, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }));
    if (dataTable != null && dataTable.Rows.Count > 0)
      deliveryListId = Convert.ToInt64(dataTable.Rows[0][0]);
    return deliveryListId;
  }

  public long GetObjectVersionDeliveryListID(Guid sessionGuid, long objectId)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return 0;
    long versionDeliveryListId = 0;
    if (ConstsHolder.DeliveryListID == 0 || ConstsHolder.OriginalObjectID == -10000)
      return versionDeliveryListId;
    IDBObject dbObject = sessionById.GetObject(objectId, false);
    if (dbObject == null)
      return versionDeliveryListId;
    DataTable dataTable = sessionById.GetObjectCollection(ConstsHolder.DeliveryListID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) dbObject.ID, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }));
    if (dataTable != null && dataTable.Rows.Count == 1)
      versionDeliveryListId = Convert.ToInt64(dataTable.Rows[0][0]);
    return versionDeliveryListId;
  }

  public void CreateCopiesByDeliveryList(Guid sessionGuid, long docObjectId, bool mindSendedCopies)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    long versionDeliveryListId = this.GetObjectVersionDeliveryListID(sessionGuid, docObjectId);
    if (versionDeliveryListId == 0L)
      return;
    IDBAttribute objectAttributeById1 = sessionById.GetObjectAttributeByID(versionDeliveryListId, ConstsHolder.SubscribersID);
    IDBAttribute objectAttributeById2 = sessionById.GetObjectAttributeByID(versionDeliveryListId, ConstsHolder.NumberOfCopiesID);
    for (int index = 0; index < objectAttributeById1.ValuesCount; ++index)
    {
      if (objectAttributeById1.Values[index] != DBNull.Value && objectAttributeById2.Values[index] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(objectAttributeById1.Values[index]);
        int int32 = Convert.ToInt32(objectAttributeById2.Values[index]);
        int missingCopiesCount = !mindSendedCopies ? CopiesService.CalculateMissingCopiesNotMindSendedCopies(sessionById, int64, docObjectId, int32) : CopiesService.CalculateMissingCopiesMindSendedCopies(sessionById, int64, docObjectId, int32);
        if (missingCopiesCount > 0)
          CopiesService.CreateMissingCopies(sessionById, docObjectId, missingCopiesCount, int64);
      }
    }
  }

  public List<DeliveryList> GetDeliveryLists(Guid sessionGuid, List<long> deliveryListsIds)
  {
    List<DeliveryList> deliveryLists = new List<DeliveryList>();
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return deliveryLists;
    int[] attributesID = new int[6]
    {
      ConstsHolder.SubscribersID,
      ConstsHolder.NumberOfCopiesID,
      ConstsHolder.ListOwnerID,
      ConstsHolder.SubscribersDateID,
      ConstsHolder.ActualCopyID,
      ConstsHolder.NotesForSubscribersID
    };
    foreach (long deliveryListsId in deliveryListsIds)
    {
      string nameInMessages = sessionById.GetObject(deliveryListsId, false).NameInMessages;
      AttributeValues[] attributesValues = sessionById.GetObjectAttributesValues(deliveryListsId, attributesID, GetAttributeValuesModes.IncludeDescriptions, false);
      AttributeValues attributeValues1 = attributesValues[0];
      AttributeValues attributeValues2 = attributesValues[1];
      AttributeValues attributeValues3 = attributesValues[2];
      AttributeValues attributeValues4 = attributesValues[3];
      AttributeValues attributeValues5 = attributesValues[4];
      AttributeValues attributeValues6 = attributesValues[5];
      if (attributeValues1.Values.Length == attributeValues2.Values.Length && attributeValues1.Values.Length == attributeValues3.Values.Length && attributeValues1.Values.Length == attributeValues4.Values.Length && attributeValues1.Values.Length == attributeValues5.Values.Length)
      {
        List<Subscriber> subscriberList = new List<Subscriber>();
        for (int index = 0; index < attributeValues1.Values.Length; ++index)
        {
          if (attributeValues1.Values[index] != null && attributeValues1.Values[index] != DBNull.Value)
          {
            string empty = string.Empty;
            long num = 0;
            if (attributeValues5.Values[index] != null && attributeValues5.Values[index] != DBNull.Value && Convert.ToInt64(attributeValues5.Values[index]) != 0L)
            {
              empty = Convert.ToString(attributeValues5.Descriptions[index]);
              num = Convert.ToInt64(attributeValues5.Values[index]);
            }
            int objectTypeId = sessionById.GetObjectInfo(Convert.ToInt64(attributeValues1.Values[index])).ObjectTypeID;
            Subscriber subscriber = new Subscriber()
            {
              ActualCopyName = empty,
              ActualCopyId = num,
              Caption = Convert.ToString(attributeValues1.Descriptions[index]),
              CopyNumber = Convert.ToInt32(attributeValues2.Values[index]),
              ID = Convert.ToInt64(attributeValues1.Values[index]),
              OwnerId = Convert.ToInt64(attributeValues3.Values[index]),
              OwnerName = Convert.ToString(attributeValues3.Descriptions[index]),
              SignDate = Convert.ToDateTime(attributeValues4.Values[index]),
              ObjectType = objectTypeId
            };
            if (index < attributeValues6.Values.Length)
              subscriber.Note = Convert.ToString(attributeValues6.Values[index]);
            subscriberList.Add(subscriber);
          }
        }
        DeliveryList deliveryList = new DeliveryList()
        {
          ID = deliveryListsId,
          NameInMessages = nameInMessages,
          Subscribers = subscriberList
        };
        deliveryLists.Add(deliveryList);
      }
      else
      {
        AttributeValues[] attributeValues7 = new AttributeValues[6]
        {
          new AttributeValues(ConstsHolder.SubscribersID, (object) null),
          new AttributeValues(ConstsHolder.NumberOfCopiesID, (object) null),
          new AttributeValues(ConstsHolder.ListOwnerID, (object) null),
          new AttributeValues(ConstsHolder.SubscribersDateID, (object) null),
          new AttributeValues(ConstsHolder.ActualCopyID, (object) null),
          new AttributeValues(ConstsHolder.NotesForSubscribersID, (object) null)
        };
        sessionById.SetObjectAttributesValues(deliveryListsId, false, attributeValues7);
      }
    }
    return deliveryLists;
  }

  public void SaveDeliveryLists(Guid sessionGuid, List<DeliveryList> deliveryLists)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      throw new KernelException($"Не найдена пользовательская сессия GUID {sessionGuid}");
    foreach (DeliveryList deliveryList in deliveryLists)
      this.SaveDeliveryList(sessionById, deliveryList);
  }

  private void SaveDeliveryList(UserSession session, DeliveryList deliveryList)
  {
    session.StartTransaction();
    try
    {
      IDBObject dbObject = session.GetObject(deliveryList.ID);
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(ConstsHolder.SubscribersID);
      attributeById1.ClearValues();
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
      attributeById2.ClearValues();
      IDBAttribute attributeById3 = dbObject.GetAttributeByID(ConstsHolder.ListOwnerID);
      attributeById3.ClearValues();
      IDBAttribute attributeById4 = dbObject.GetAttributeByID(ConstsHolder.SubscribersDateID);
      attributeById4.ClearValues();
      IDBAttribute attributeById5 = dbObject.GetAttributeByID(ConstsHolder.ActualCopyID);
      attributeById5.ClearValues();
      IDBAttribute attributeById6 = dbObject.GetAttributeByID(ConstsHolder.NotesForSubscribersID);
      attributeById6.ClearValues();
      for (int index = 0; index < deliveryList.Subscribers.Count; ++index)
      {
        Subscriber subscriber = deliveryList.Subscribers[index];
        if (index == 0)
        {
          attributeById1.Value = (object) subscriber.ID;
          attributeById2.Value = (object) subscriber.CopyNumber;
          attributeById3.Value = (object) subscriber.OwnerId;
          attributeById4.Value = (object) subscriber.SignDate;
          attributeById5.Value = (object) subscriber.ActualCopyId;
          attributeById6.Value = (object) subscriber.Note;
        }
        else
        {
          attributeById1.AddValue((object) subscriber.ID);
          attributeById2.AddValue((object) subscriber.CopyNumber);
          attributeById3.AddValue((object) subscriber.OwnerId);
          attributeById4.AddValue((object) subscriber.SignDate);
          attributeById5.AddValue((object) subscriber.ActualCopyId);
          attributeById6.AddValue((object) subscriber.Note);
        }
      }
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
  }

  public Dictionary<long, int> GetSubscribers(int objTypeID)
  {
    int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(ConstsHolder.DocTypeID);
    int num = objTypeID;
    lock (this.SyncObject)
    {
      for (; num != objectTypeParentId; num = MetaDataHelper.GetObjectTypeParentID(num))
      {
        if (this.SubscribersDictionary.ContainsKey(num))
          return this.SubscribersDictionary[num];
      }
    }
    return new Dictionary<long, int>();
  }

  public void ChangeSubscribers(int objTypeID, Dictionary<long, int> list, object sessionID)
  {
    IUserSession userSession = !(sessionID is IUserSession) ? UserSession.GetSessionByID((Guid) sessionID) : sessionID as IUserSession;
    if (userSession == null)
      throw new KernelException("Невозможно получить сессию.");
    lock (this.SyncObject)
    {
      if (this.SubscribersDictionary.ContainsKey(objTypeID))
      {
        if (list.Count > 0)
          this.SubscribersDictionary[objTypeID] = list;
        else
          this.SubscribersDictionary.Remove(objTypeID);
      }
      else
        this.SubscribersDictionary.Add(objTypeID, list);
    }
    string empty = string.Empty;
    foreach (long key in list.Keys)
    {
      int num = list[key];
      empty += $"{key}|{num};";
    }
    userSession.Configurations.WriteString(ConstsHolder.MODULE_NAME, ConstsHolder.SUBSCRIBERS_SECTION_NAME, objTypeID.ToString(), empty, 0L);
  }

  public void AddSubscrsFromEcoToDoc(
    long ecoDeliveryListID,
    long docID,
    long docObjID,
    Guid sessionGuid)
  {
    long num = this.GetDeliveryListID(sessionGuid, docID);
    if (num == 0L)
      num = this.CreateDeliveryList(sessionGuid, docObjID);
    if (num == 0L)
      return;
    this.AddSubcribersToDeliveryLists(sessionGuid, ecoDeliveryListID, new List<long>()
    {
      num
    });
  }

  public void AddSubcribersToDeliveryLists(
    Guid sessionGuid,
    long copiedDeliveryListID,
    List<long> deliveryLists)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return;
    IDBObject dbObject1 = sessionById.GetObject(copiedDeliveryListID);
    IDBAttribute attributeById1 = dbObject1.GetAttributeByID(ConstsHolder.SubscribersID);
    IDBAttribute attributeById2 = dbObject1.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
    List<long> longList1 = new List<long>();
    List<int> intList1 = new List<int>();
    for (int index = 0; index < attributeById1.ValuesCount; ++index)
    {
      object obj1 = attributeById1.Values[index];
      object obj2 = attributeById2.Values[index];
      if (obj1 != DBNull.Value && obj1 != null && obj2 != DBNull.Value && obj2 != null)
      {
        longList1.Add(Convert.ToInt64(obj1));
        intList1.Add(Convert.ToInt32(obj2));
      }
    }
    if (longList1.Count == 0)
      return;
    sessionById.StartTransaction();
    try
    {
      foreach (long deliveryList in deliveryLists)
      {
        IDBObject dbObject2 = sessionById.GetObject(deliveryList);
        IDBAttribute attributeById3 = dbObject2.GetAttributeByID(ConstsHolder.SubscribersID);
        IDBAttribute attributeById4 = dbObject2.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
        IDBAttribute attributeById5 = dbObject2.GetAttributeByID(ConstsHolder.ListOwnerID);
        IDBAttribute attributeById6 = dbObject2.GetAttributeByID(ConstsHolder.SubscribersDateID);
        IDBAttribute attributeById7 = dbObject2.GetAttributeByID(ConstsHolder.ActualCopyID);
        List<long> longList2 = new List<long>();
        List<int> intList2 = new List<int>();
        for (int index = 0; index < attributeById3.ValuesCount; ++index)
        {
          if (attributeById3.Values[index] != DBNull.Value && attributeById4.Values[index] != DBNull.Value)
          {
            longList2.Add(Convert.ToInt64(attributeById3.Values[index]));
            intList2.Add(Convert.ToInt32(attributeById4.Values[index]));
          }
        }
        for (int index1 = 0; index1 < longList1.Count; ++index1)
        {
          if (longList2.Contains(longList1[index1]))
          {
            int index2 = longList2.IndexOf(longList1[index1]);
            if (intList1[index1] > intList2[index2])
            {
              attributeById4.Index = index2;
              attributeById4.Value = (object) intList1[index1];
            }
          }
          else if (attributeById3.Values[0] == DBNull.Value && attributeById3.ValuesCount == 1)
          {
            attributeById3.Value = (object) longList1[index1];
            attributeById4.Value = attributeById2.Values[index1];
            attributeById6.Value = (object) DateTime.Now;
            attributeById5.Value = (object) sessionById.UserID;
            attributeById7.Value = (object) 0L;
          }
          else
          {
            attributeById3.AddValue((object) longList1[index1]);
            attributeById4.AddValue(attributeById2.Values[index1]);
            attributeById6.AddValue((object) DateTime.Now);
            attributeById5.AddValue((object) sessionById.UserID);
            attributeById7.AddValue((object) 0L);
          }
        }
      }
      sessionById.Commit();
    }
    catch
    {
      sessionById.Rollback();
      throw;
    }
  }

  public void ReplaceSubscribersInDeliveryLists(
    Guid sessionGuid,
    long copiedDeliveryListID,
    List<long> deliveryLists)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return;
    IDBAttribute objectAttributeById1 = sessionById.GetObjectAttributeByID(copiedDeliveryListID, ConstsHolder.SubscribersID);
    IDBAttribute objectAttributeById2 = sessionById.GetObjectAttributeByID(copiedDeliveryListID, ConstsHolder.NumberOfCopiesID);
    foreach (long deliveryList in deliveryLists)
    {
      IDBObject dbObject = sessionById.GetObject(deliveryList);
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(ConstsHolder.SubscribersID);
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
      IDBAttribute attributeById3 = dbObject.GetAttributeByID(ConstsHolder.ListOwnerID);
      IDBAttribute attributeById4 = dbObject.GetAttributeByID(ConstsHolder.SubscribersDateID);
      IDBAttribute attributeById5 = dbObject.GetAttributeByID(ConstsHolder.ActualCopyID);
      IDBAttribute attributeById6 = dbObject.GetAttributeByID(ConstsHolder.NotesForSubscribersID);
      attributeById1.ClearValues();
      attributeById2.ClearValues();
      attributeById3.ClearValues();
      attributeById4.ClearValues();
      attributeById5.ClearValues();
      attributeById6.ClearValues();
      for (int index = 0; index < objectAttributeById1.ValuesCount; ++index)
      {
        objectAttributeById1.Index = index;
        objectAttributeById2.Index = index;
        if (objectAttributeById1.Value != DBNull.Value && objectAttributeById2.Value != DBNull.Value)
        {
          if (index == 0)
          {
            attributeById1.Value = objectAttributeById1.Value;
            attributeById2.Value = objectAttributeById2.Value;
            attributeById4.Value = (object) DateTime.Now;
            attributeById3.Value = (object) sessionById.UserID;
            attributeById5.Value = (object) 0L;
          }
          else
          {
            attributeById1.AddValue(objectAttributeById1.Value);
            attributeById2.AddValue(objectAttributeById2.Value);
            attributeById4.AddValue((object) DateTime.Now);
            attributeById3.AddValue((object) sessionById.UserID);
            attributeById5.AddValue((object) 0L);
          }
        }
      }
    }
  }

  public void RemoveObjectFromSubscribersDictionary(long objectId, Guid sessionGuid)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return;
    foreach (KeyValuePair<int, Dictionary<long, int>> keyValuePair in new Dictionary<int, Dictionary<long, int>>((IDictionary<int, Dictionary<long, int>>) this.SubscribersDictionary))
    {
      Dictionary<long, int> dictionary = keyValuePair.Value;
      Dictionary<long, int> list = new Dictionary<long, int>((IDictionary<long, int>) dictionary);
      foreach (long key in dictionary.Keys)
      {
        if (key == objectId)
        {
          list.Remove(key);
          break;
        }
      }
      this.ChangeSubscribers(keyValuePair.Key, list, (object) sessionById);
    }
  }

  public List<(long Id, string NameInMessage, long ParentId, string ParentInventoryNumber)> GetObjectsParentsInventoryNumbers(
    Guid sessionGuid,
    List<long> Ids)
  {
    List<(long, string, long, string)> inventoryNumbers = new List<(long, string, long, string)>();
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return inventoryNumbers;
    foreach (long id in Ids)
    {
      IDBObject dbObject = sessionById.GetObject(id, false);
      if (dbObject != null)
      {
        long parentVersionId = dbObject.ParentVersionID;
        if (parentVersionId != 0L)
        {
          IDBAttribute objectAttribute = sessionById.GetObjectAttribute(parentVersionId, (object) ConstsHolder.InventoryNumberID, false, false);
          if (objectAttribute == null || string.IsNullOrEmpty(objectAttribute.AsString))
            inventoryNumbers.Add((id, dbObject.NameInMessages, parentVersionId, (string) null));
          else
            inventoryNumbers.Add((id, dbObject.NameInMessages, parentVersionId, objectAttribute.AsString));
        }
        else
          inventoryNumbers.Add((id, dbObject.NameInMessages, parentVersionId, (string) null));
      }
    }
    return inventoryNumbers;
  }

  private static int CalculateMissingCopiesMindSendedCopies(
    IUserSession session,
    long subscrID,
    long docObjectID,
    int copiesNumberForSubscr)
  {
    DataTable dataTable = session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, new DBRecordSetParams(new ConditionStructure[4]
    {
      new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.OR, 1, false),
      new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.CreateLCStepID, LogicalOperators.AND, -1, false),
      new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscrID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) Math.Abs(docObjectID), LogicalOperators.NONE, 0, false)
    }, (object[]) null)
    {
      RecordCount = 0
    });
    int int32 = dataTable == null || dataTable.Rows.Count != 1 ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
    return copiesNumberForSubscr - int32;
  }

  private static int CalculateMissingCopiesNotMindSendedCopies(
    IUserSession session,
    long subscrID,
    long docObjectID,
    int copiesNumberForSubscr)
  {
    DataTable dataTable = session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, new DBRecordSetParams(new ConditionStructure[3]
    {
      new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.CreateLCStepID, LogicalOperators.AND, 0, false),
      new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscrID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) Math.Abs(docObjectID), LogicalOperators.NONE, 0, false)
    }, (object[]) null)
    {
      RecordCount = 0
    });
    int int32 = dataTable == null || dataTable.Rows.Count != 1 ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
    return copiesNumberForSubscr - int32;
  }

  private static void CreateMissingCopies(
    IUserSession session,
    long objectID,
    int missingCopiesCount,
    long subscrID)
  {
    if (!(session.GetCustomService(typeof (IDocumentCopyService)) is IDocumentCopyService customService))
      return;
    foreach (long copy in customService.CreateCopies(objectID, missingCopiesCount, CopyKind.Hard, (object) session.SessionGUID))
    {
      IDBObject dbObject = session.GetObject(copy, false);
      if (dbObject != null)
      {
        AttributeValues attributeValues = new AttributeValues(ConstsHolder.AlbumSubscriberID, (object) subscrID);
        dbObject.SetAttributesValues(new AttributeValues[1]
        {
          attributeValues
        });
        IDBAttributeCollection attributes = dbObject.Attributes;
        List<int> intList = new List<int>();
        foreach (IDBAttribute dbAttribute in attributes.ToList())
        {
          IDBAttributeType attributeType = dbAttribute.AttributeType;
          if (attributeType.MasterAttributeID != 0)
            intList.Add(attributeType.MasterAttributeID);
        }
        attributes.SetDependentAttributes(intList.ToArray());
      }
    }
  }

  public object GetFormula(int objTypeID)
  {
    lock (this.SyncObject)
      return this.InventoryDictionary.ContainsKey(objTypeID) ? (object) this.InventoryDictionary[objTypeID] : (object) null;
  }

  public string GetFormulaRecursive(int objTypeID)
  {
    int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(ConstsHolder.DocTypeID);
    int num = objTypeID;
    object obj = (object) null;
    for (; num != objectTypeParentId; num = MetaDataHelper.GetObjectTypeParentID(num))
    {
      obj = this.GetFormula(num);
      if (obj != null)
        break;
    }
    return obj != null ? obj.ToString() : string.Empty;
  }

  public void ChangeFormula(Dictionary<int, string> formulas, object sessionID)
  {
    IUserSession userSession = !(sessionID is IUserSession) ? UserSession.GetSessionByID((Guid) sessionID) : sessionID as IUserSession;
    lock (this.SyncObject)
    {
      foreach (int key in formulas.Keys)
      {
        string formula = formulas[key];
        if (this.InventoryDictionary.ContainsKey(key))
          this.InventoryDictionary[key] = formula;
        else
          this.InventoryDictionary.Add(key, formula);
        userSession.Configurations.WriteString(ConstsHolder.MODULE_NAME, ConstsHolder.INVENTORY_SECTION_NAME, key.ToString(), formula, 0L);
      }
    }
  }

  public List<long> Classifiers
  {
    get
    {
      lock (this.SyncObject)
        return this.ClassifiersList;
    }
  }

  public void ChangeClassifiers(List<long> classifiersID, object sessionID)
  {
    IUserSession userSession = !(sessionID is IUserSession) ? UserSession.GetSessionByID((Guid) sessionID) : sessionID as IUserSession;
    lock (this.SyncObject)
    {
      this.ClassifiersList = classifiersID;
      string classifiersString = string.Empty;
      this.ClassifiersList.ForEach((Action<long>) (curID => classifiersString = $"{classifiersString}{curID.ToString()};"));
      userSession.Configurations.WriteString(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.CLASSIFIERS, classifiersString, 0L);
    }
  }
}
