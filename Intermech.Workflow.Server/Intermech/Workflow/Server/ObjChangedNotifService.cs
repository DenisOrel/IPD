// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ObjChangedNotifService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Workflow.Server;

internal class ObjChangedNotifService : LongLifeObject, INotifySubscriberService
{
  private Dictionary<long, List<Notify>> _notifiesForObjects;
  private int _objTypeNoticesID;
  public const char GuidSeparator = ',';

  public ObjChangedNotifService(IUserSession session)
  {
    this._objTypeNoticesID = MetaDataHelper.GetObjectTypeID("cad00627-306c-11d8-b4e9-00304f19f545");
    IDBTransactions customService = (IDBTransactions) session.GetCustomService(typeof (IDBTransactions));
    DataTable dataTable = session.GetObjectCollection(this._objTypeNoticesID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    this._notifiesForObjects = new Dictionary<long, List<Notify>>(dataTable.Rows.Count);
    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
    {
      IDBObject dbObject1 = session.GetObject(Convert.ToInt64(dataTable.Rows[index1][0]));
      if (dbObject1 != null)
      {
        Guid attributeGuid = new Guid("cad00061-306c-11d8-b4e9-00304f19f545");
        customService.StartTransaction();
        try
        {
          IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
          IDBAttribute attributeByGuid2 = dbObject1.GetAttributeByGuid(attributeGuid);
          IDBAttribute attrAttributes = dbObject1.GetAttributeByID(wfConsts.AttrListAttributesID);
          IDBAttribute attributeById = dbObject1.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
          if (attrAttributes != null && attributeByGuid2 != null && attributeByGuid2.ValuesCount > 0)
          {
            dbObject1 = dbObject1.CheckOut();
            List<Guid> guids = new List<Guid>(attributeByGuid2.ValuesCount);
            for (int index2 = 0; index2 < attributeByGuid2.ValuesCount; ++index2)
            {
              attributeByGuid2.Index = index2;
              if (!attributeByGuid2.IsNull)
                guids.Add(new Guid(attributeByGuid2.AsString));
            }
            attributeByGuid2.Delete(0L);
            attributeByGuid1 = dbObject1.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
            attrAttributes = dbObject1.GetAttributeByGuid(wfConsts.AttrListAttributesGuid);
            for (int index3 = 0; index3 < attributeByGuid1.ValuesCount; ++index3)
            {
              attributeByGuid1.Index = index3;
              if (index3 > 0)
                attrAttributes.Index = attrAttributes.AddValue((object) null);
              NotifyOptions asInteger = (NotifyOptions) attributeByGuid1.AsInteger;
              if ((asInteger & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged)
              {
                if (guids.Count == 0)
                {
                  NotifyOptions notifyOptions = asInteger & ~NotifyOptions.AttributeValueChanged;
                  if (notifyOptions == NotifyOptions.None)
                    notifyOptions = NotifyOptions.Delete;
                  attributeByGuid1.AsInteger = (long) notifyOptions;
                }
                else
                  NotifyHelper.SaveListAttributes(attrAttributes, guids);
              }
            }
            dbObject1.CheckIn();
          }
          if (attrAttributes != null && attributeByGuid1.ValuesCount > attrAttributes.ValuesCount)
          {
            dbObject1 = dbObject1.CheckOut();
            IDBAttribute attributeByGuid3 = dbObject1.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
            attrAttributes = dbObject1.GetAttributeByGuid(wfConsts.AttrListAttributesGuid);
            for (int index4 = 0; index4 < attributeByGuid3.ValuesCount; ++index4)
            {
              if (index4 >= attrAttributes.ValuesCount)
                attrAttributes.AddValue((object) null);
            }
            dbObject1.CheckIn();
          }
          if (attrAttributes != null)
          {
            if (attrAttributes.ValuesCount == 1 && attrAttributes.IsNull)
            {
              IDBObject dbObject2 = dbObject1.CheckOut();
              dbObject2.GetAttributeByGuid(wfConsts.AttrListAttributesGuid).Delete(0L);
              dbObject2.CheckIn();
            }
            else
            {
              IDBObject dbObject3 = dbObject1.CheckOut();
              IDBAttribute attributeByGuid4 = dbObject3.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
              IDBAttribute attributeByGuid5 = dbObject3.GetAttributeByGuid(wfConsts.AttrListAttributesGuid);
              ObjChangedNotifService.RewriteListAttrToGuidsAttr(session, dbObject3.ObjectID, attributeByGuid4, attributeByGuid5, attributeById);
              attributeByGuid5.Delete(0L);
              dbObject3.CheckIn();
            }
          }
          customService.Commit();
        }
        catch
        {
          customService.Rollback();
          throw;
        }
        IDBObject notifyObject = session.GetObject(Convert.ToInt64(dataTable.Rows[index1][0]));
        this.AddNotifyObjectToCache(session, notifyObject);
      }
    }
    IDBObjectType objectType = session.GetObjectType(wfConsts.NotifyObjectTypeID, false);
    IDBAttributeType attributeType = session.GetAttributeType(wfConsts.AttrListAttributesID, false);
    IDBAttribute4ObjectTypeCollection attributes = objectType.Attributes as IDBAttribute4ObjectTypeCollection;
    if (attributeType != null && attributes != null && session is UserSession userSession)
    {
      bool developerMode = userSession.DeveloperMode;
      userSession.DeveloperMode = true;
      AttributeTypeProperties propertiesStructure = attributeType.PropertiesStructure with
      {
        AttributeGuid = new Guid("70a9b18e-6713-4c96-804a-d783c74576c4")
      };
      attributeType.PropertiesStructure = propertiesStructure;
      attributes.GetAttributeByID(attributeType.AttributeID).Required = RequiredModes.Manual;
      attributes.GetAttributeByID(wfConsts.AttrListAttributesID).Delete(1L);
      attributeType.Delete(1L);
      userSession.DeveloperMode = developerMode;
    }
    IEventLogHelper service = ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service.AfterCheckoutEvent += new ObjectEventHandler(this.ehelper_AfterCheckoutEvent);
    service.AfterUndoCheckoutEvent += new ObjectEventHandler(this.ehelper_AfterUndoCheckoutEvent);
    service.CommitCreationObjectEvent += new ObjectEventHandler(this.ehelper_CreateObjectEvent);
    service.AfterNextLCStepEvent += new NextLCStepHandler(this.ehelper_AfterNextLCStepEvent);
    service.AfterCheckinEvent += new ObjectEventHandler(this.ehelper_AfterCheckinEvent);
    service.BeforeCheckinEvent += new ObjectEventHandler(this.ehelper_BeforeCheckinEvent);
    service.AddAttributeWriteHandler((object) 0, new WriteAttributeValueHandler(this.ehelper_OnWriteAttributeValue));
  }

  private static void RewriteListAttrToGuidsAttr(
    IUserSession session,
    long objectID,
    IDBAttribute attrOptions,
    IDBAttribute attrListAttributes,
    IDBAttribute attrGuidsAttributes)
  {
    for (int index = 0; index < attrOptions.ValuesCount; ++index)
    {
      attrOptions.Index = index;
      attrListAttributes.Index = index;
      if (index > 0)
        attrGuidsAttributes.Index = attrGuidsAttributes.AddValue((object) string.Empty);
      List<Guid> attributesFromBlob = NotifyHelper.GetListAttributesFromBlob(attrListAttributes);
      if (((int) attrOptions.AsInteger & 32 /*0x20*/) == 32 /*0x20*/)
      {
        string str = string.Empty;
        List<Guid> guids = new List<Guid>();
        foreach (Guid guid in attributesFromBlob)
        {
          str = !str.Equals(string.Empty) ? $"{str},{guid.ToString()}" : str + guid.ToString();
          if ((long) str.Length > attrGuidsAttributes.AttributeType.SizeType)
          {
            session.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_53"), (object) objectID), Consts.traceAlways, string.Empty);
            break;
          }
          guids.Add(guid);
        }
        NotifyHelper.SaveGuidsAttributes(attrGuidsAttributes, guids);
      }
      else
        attrGuidsAttributes.Value = (object) string.Empty;
    }
  }

  public void Close()
  {
    IEventLogHelper service = ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service.AfterCheckoutEvent -= new ObjectEventHandler(this.ehelper_AfterCheckoutEvent);
    service.AfterUndoCheckoutEvent -= new ObjectEventHandler(this.ehelper_AfterUndoCheckoutEvent);
    service.CommitCreationObjectEvent -= new ObjectEventHandler(this.ehelper_CreateObjectEvent);
    service.AfterNextLCStepEvent -= new NextLCStepHandler(this.ehelper_AfterNextLCStepEvent);
    service.AfterCheckinEvent -= new ObjectEventHandler(this.ehelper_AfterCheckinEvent);
    service.BeforeCheckinEvent -= new ObjectEventHandler(this.ehelper_BeforeCheckinEvent);
    service.RemoveAttributeWriteHandler((object) 0, new WriteAttributeValueHandler(this.ehelper_OnWriteAttributeValue));
  }

  private void ehelper_OnWriteAttributeValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    DBAttribute dbAttribute = attribute as DBAttribute;
    if (!dbAttribute.IsObjectAttribute)
      return;
    IDBObject parentObject = (IDBObject) dbAttribute.ParentObject;
    List<Notify> notifyList;
    if (parentObject.ObjectModifyMode == ObjectModifyModes.Checkout && parentObject.CheckoutBy != 0L || !ObjChangedNotifService.IsNotifiedObjectType(parentObject.ObjectType) || !this._notifiesForObjects.TryGetValue(parentObject.ID, out notifyList))
      return;
    foreach (Notify notify in notifyList)
    {
      if ((notify.Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged && notify.UserID != args.Session.UserID && notify.Attributes != null && notify.Attributes.Contains(dbAttribute.AttributeID))
        this.SendMessage(args.Session, parentObject, notify.UserID, notify.Comment, NotifyOptions.AttributeValueChanged, (ObjChangedNotifService.IMessageExtension) new ObjChangedNotifService.AttributeChangedExtension(attribute.AttributeType.Name, attribute.DataType, Convert.ToString(args.OldValue), Convert.ToString(args.Value)));
    }
  }

  private void ehelper_BeforeCheckinEvent(IDBObject sender, IUserSession session)
  {
    List<Notify> notifyList;
    if (!ObjChangedNotifService.IsNotifiedObjectType(sender.ObjectType) || !this._notifiesForObjects.TryGetValue(sender.ID, out notifyList))
      return;
    IDBObject dbObject = session.GetObject(Math.Abs(sender.ObjectID));
    foreach (Notify notify in notifyList)
    {
      if ((notify.Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged && notify.UserID != session.UserID && notify.Attributes != null)
      {
        List<ObjChangedNotifService.IMessageExtension> items = new List<ObjChangedNotifService.IMessageExtension>();
        for (int index = 0; index < notify.Attributes.Count; ++index)
        {
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(notify.Attributes[index]);
          IDBAttribute attributeById2 = sender.GetAttributeByID(notify.Attributes[index]);
          if (attributeById1 != null || attributeById2 != null)
          {
            if (attributeById1 == null && attributeById2 != null)
              items.Add((ObjChangedNotifService.IMessageExtension) new ObjChangedNotifService.AttributeCreatedExtension(attributeById2.Name, attributeById2.DataType, this.AttributeValueForMessage(attributeById2)));
            else if (attributeById1 != null && attributeById2 == null)
              items.Add((ObjChangedNotifService.IMessageExtension) new ObjChangedNotifService.AttributeDeletedExtension(attributeById1.Name, attributeById1.DataType, this.AttributeValueForMessage(attributeById1)));
            else if (!this.CompareValues(attributeById1, attributeById2))
              items.Add((ObjChangedNotifService.IMessageExtension) new ObjChangedNotifService.AttributeChangedExtension(attributeById2.Name, attributeById2.DataType, this.AttributeValueForMessage(attributeById1), this.AttributeValueForMessage(attributeById2)));
          }
        }
        if (items.Count > 0)
          this.SendMessage(session, sender, notify.UserID, notify.Comment, NotifyOptions.AttributeValueChanged, (ObjChangedNotifService.IMessageExtension) new ObjChangedNotifService.AttributesChangedExtension(items));
      }
    }
  }

  private string AttributeValueForMessage(IDBAttribute attr)
  {
    switch (attr.AttributeType.TextFieldName)
    {
      case "F_INTEGER_VALUE":
        return Convert.ToString(attr.AsInteger);
      case "F_DOUBLE_VALUE":
        return Convert.ToString(attr.AsDouble);
      case "F_DATE_VALUE":
        return Convert.ToString(attr.AsDateTime);
      case "F_STRING_VALUE":
        return attr.AsString;
      default:
        return string.Empty;
    }
  }

  private bool CompareValues(IDBAttribute oldAttr, IDBAttribute newAttr)
  {
    if (oldAttr.AttributeType.MultipleValued != MultiValueModes.MultiValues && oldAttr.AttributeType.MultipleValued != MultiValueModes.MultiValuesFromList)
      return oldAttr.Value.Equals(newAttr.Value);
    if (oldAttr.ValuesCount != newAttr.ValuesCount)
      return false;
    for (int index = 0; index < oldAttr.ValuesCount; ++index)
    {
      if (index > 0)
        oldAttr.Index = newAttr.Index = index;
      if (oldAttr.IsNull && !newAttr.IsNull || !oldAttr.IsNull && newAttr.IsNull || !oldAttr.Value.Equals(newAttr.Value))
        return false;
    }
    return true;
  }

  private void ehelper_AfterCheckinEvent(IDBObject sender, IUserSession session)
  {
    if (!sender.ObjectType.Equals(this._objTypeNoticesID))
      return;
    this.AddNotifyObjectToCache(session, sender);
  }

  private void ehelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"));
    if (lifecycleLevel.LevelID != nextstep.LevelID)
      return;
    if (sender.ObjectType.Equals(this._objTypeNoticesID))
    {
      lock (this._notifiesForObjects)
        this._notifiesForObjects.Remove(sender.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).AsInteger);
    }
    else
    {
      List<Notify> notifyList = new List<Notify>();
      if (ObjChangedNotifService.IsNotifiedObjectType(sender.ObjectType))
        notifyList = this.Send(sender, session, NotifyOptions.Delete, (ObjChangedNotifService.IMessageExtension) null);
      if (notifyList.Count <= 0)
        return;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[3]
      {
        new ConditionStructure(-3, RelationalOperators.Equal, (object) sender.ID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-9, RelationalOperators.NotEqual, (object) lifecycleLevel.LevelID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-2, RelationalOperators.NotEqual, (object) sender.ObjectID, LogicalOperators.AND, 0, false)
      });
      if (session.GetObjectCollection(sender.ObjectType).Select(paramSet).Rows.Count != 0)
        return;
      IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545"));
      paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) sender.ID, LogicalOperators.AND, 0)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable = objectCollection.Select(paramSet);
      List<long> longList = new List<long>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        longList.Add(Convert.ToInt64(row[0]));
      if (longList.Count <= 0)
        return;
      objectCollection.Delete(longList.ToArray(), false, 0L);
    }
  }

  private void ehelper_CreateObjectEvent(IDBObject sender, IUserSession session)
  {
    if (sender.ObjectType.Equals(this._objTypeNoticesID))
    {
      this.AddNotifyObjectToCache(session, sender);
    }
    else
    {
      if (!ObjChangedNotifService.IsNotifiedObjectType(sender.ObjectType))
        return;
      this.Send(sender, session, NotifyOptions.Version, (ObjChangedNotifService.IMessageExtension) null);
    }
  }

  private void ehelper_AfterUndoCheckoutEvent(IDBObject sender, IUserSession session)
  {
    if (!ObjChangedNotifService.IsNotifiedObjectType(sender.ObjectType))
      return;
    this.Send(sender, session, NotifyOptions.UndoCheckOut, (ObjChangedNotifService.IMessageExtension) null);
  }

  private void ehelper_AfterCheckoutEvent(IDBObject sender, IUserSession session)
  {
    if (!ObjChangedNotifService.IsNotifiedObjectType(sender.ObjectType))
      return;
    this.Send(sender, session, NotifyOptions.CheckOut, (ObjChangedNotifService.IMessageExtension) null);
  }

  private void AddNotifyObjectToCache(IUserSession session, IDBObject notifyObject)
  {
    lock (this._notifiesForObjects)
    {
      long id = notifyObject.ID;
      session.ClearObjectSmartCache();
      notifyObject = session.GetObjectByID(id, false);
      IDBAttribute attributeByGuid = notifyObject.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545"));
      if (this._notifiesForObjects.ContainsKey(attributeByGuid.AsInteger))
        this._notifiesForObjects.Remove(attributeByGuid.AsInteger);
      string errorMessage;
      List<Notify> notificationsFromObject = this.GetNotificationsFromObject(session, notifyObject, out errorMessage);
      if (notificationsFromObject.Count > 0)
      {
        this._notifiesForObjects.Add(attributeByGuid.AsInteger, notificationsFromObject);
      }
      else
      {
        if (!string.IsNullOrEmpty(errorMessage))
          (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(errorMessage);
        try
        {
          errorMessage = notifyObject.NameInMessages;
          if (notifyObject.CheckoutBy == 0L)
          {
            notifyObject.Delete((long) Consts.PurgeMode);
          }
          else
          {
            long objectID = Math.Abs(notifyObject.ObjectID);
            notifyObject.CancelChanges(true);
            notifyObject = session.GetObject(objectID, false);
            notifyObject?.Delete((long) Consts.PurgeMode);
          }
        }
        catch (Exception ex)
        {
          (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Ошибка удаления уведомления {errorMessage}: {ex.Message}");
        }
      }
    }
  }

  private void SendMessage(
    IUserSession session,
    IDBObject changedObject,
    long toUserID,
    string comment,
    NotifyOptions option,
    ObjChangedNotifService.IMessageExtension ext)
  {
    if (!((ApplicationServices.Container.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (IRouterService)) is IRouterService service))
      return;
    IDBObjectType objectType = session.GetObjectType(changedObject.ObjectType);
    string messageSubject = NotifyHelper.MessageSubject;
    string enumDescription = EnumDescConverter.GetEnumDescription((Enum) option);
    Guid guid;
    string caption;
    if (!(changedObject.Caption != string.Empty))
    {
      guid = changedObject.GUID;
      caption = guid.ToString();
    }
    else
      caption = changedObject.Caption;
    string Subject = string.Format(messageSubject, (object) enumDescription, (object) caption);
    changedObject = session.GetObject(changedObject.ObjectID);
    string messageBody = NotifyHelper.MessageBody;
    object[] objArray = new object[6];
    objArray[0] = (object) EnumDescConverter.GetEnumDescription((Enum) option);
    guid = changedObject.ObjectGUID;
    objArray[1] = (object) guid.ToString();
    objArray[2] = (object) objectType.ObjectInstanceName;
    objArray[3] = (object) DataSetProcessor.QString(changedObject.Caption);
    objArray[4] = ext != null ? (object) (ext.Text + "<br>") : (object) string.Empty;
    objArray[5] = (object) session.UserName;
    string Text = string.Format(messageBody, objArray);
    if (!string.IsNullOrWhiteSpace(comment))
      Text = $"{Text}<br>{LocalizationHolder.rm.GetString("Interfaces.Workflow_42")}{comment}";
    service.CreateMessage(session.SessionGUID, toUserID, Subject, Text, session.UserID);
  }

  private List<Notify> Send(
    IDBObject sender,
    IUserSession session,
    NotifyOptions option,
    ObjChangedNotifService.IMessageExtension ext)
  {
    string errorMessage;
    List<Notify> notifications = this.GetNotifications(session.SessionGUID, sender.ID, out errorMessage);
    if (notifications.Count == 0 && !string.IsNullOrEmpty(errorMessage))
      (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Ошибка отправки уведомлений при изменении {sender.NameInMessages}: {errorMessage}");
    foreach (Notify notify in notifications)
    {
      if ((notify.Options & option) == option && notify.UserID != session.UserID)
        this.SendMessage(session, sender, notify.UserID, notify.Comment, option, ext);
    }
    return notifications;
  }

  private static bool IsNotifiedObjectType(int objectTypeID)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeID);
    return objectType != null && (objectType.Options & ObjectTypeOptions.NotificationsEnabled) == ObjectTypeOptions.NotificationsEnabled;
  }

  public event GetEcoDocumentsHandler GetEcoDocumentsListEvent;

  public List<ResultEcoDocumentsInformation> GetResultEcos(EcoDocumentsInAttachments attachmentsDoc)
  {
    GetEcoDocumentsHandler documentsListEvent = this.GetEcoDocumentsListEvent;
    return documentsListEvent != null ? documentsListEvent(attachmentsDoc) : (List<ResultEcoDocumentsInformation>) null;
  }

  public long AddNotify(Guid sessionGuid, long Id, long notifyId, List<Notify> notifies)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (notifyId != -1L)
    {
      IDBObject notifyObject = sessionById.GetObject(notifyId).CheckOut();
      this.SaveNotifiesToAttributes(notifyObject, notifies);
      notifyObject.CheckIn();
      return notifyId;
    }
    IDBObject notifyObject1 = sessionById.GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545")).Create();
    notifyObject1.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).Value = (object) Id;
    this.SaveNotifiesToAttributes(notifyObject1, notifies);
    notifyObject1.CommitCreation(true);
    return notifyObject1.ObjectID;
  }

  public List<string> AddNotifies(
    Guid sessionGuid,
    List<long> ids,
    Dictionary<long, long> notificationsForObjects,
    List<Notify> notifies)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    List<string> stringList = new List<string>();
    for (int index = 0; index < ids.Count; ++index)
    {
      long notificationsForObject = notificationsForObjects[ids[index]];
      if (notificationsForObject != 0L)
      {
        IDBObject notifyObject = sessionById.GetObject(notificationsForObject).CheckOut();
        List<string> values = this.AddToValues(notifyObject, sessionById, notifies);
        stringList.AddRange((IEnumerable<string>) values);
        notifyObject.CheckIn();
      }
      else
      {
        IDBObject notifyObject = sessionById.GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545")).Create();
        notifyObject.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).Value = (object) ids[index];
        this.SaveNotifiesToAttributes(notifyObject, notifies);
        notifyObject.CommitCreation(true);
      }
    }
    return stringList;
  }

  private List<string> AddToValues(
    IDBObject notifyObject,
    IUserSession session,
    List<Notify> notifies)
  {
    List<string> values = new List<string>();
    IDBAttribute attributeByGuid1 = notifyObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid2 = notifyObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid3 = notifyObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid4 = notifyObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeById = notifyObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
    List<long> longList = new List<long>(attributeByGuid1.Values.Cast<long>());
    for (int index1 = 0; index1 < notifies.Count; ++index1)
    {
      int num1 = longList.IndexOf(notifies[index1].UserID);
      if (num1 == -1)
      {
        attributeByGuid1.AddValue((object) notifies[index1].UserID);
        attributeByGuid2.AddValue((object) notifies[index1].Date);
        attributeByGuid3.AddValue((object) notifies[index1].Comment);
        attributeByGuid4.AddValue((object) (int) notifies[index1].Options);
        attributeById.AddValue((object) string.Empty);
        if ((notifies[index1].Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged && notifies[index1].Attributes != null && notifies[index1].Attributes.Count > 0)
          NotifyHelper.SaveGuidsAttributes(attributeById, notifies[index1].Attributes);
      }
      else
      {
        attributeByGuid4.Index = num1;
        attributeById.Index = num1;
        int int32 = Convert.ToInt32(attributeByGuid4.Value);
        NotifyOptions notifyOptions = (NotifyOptions) int32 | notifies[index1].Options;
        attributeByGuid4.Value = (object) notifyOptions;
        if (((NotifyOptions) int32 & notifies[index1].Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged)
        {
          List<int> fromGuidsAttribute = NotifyHelper.GetAttributesIDsFromGuidsAttribute(attributeById);
          List<int> list1 = fromGuidsAttribute.Union<int>((IEnumerable<int>) notifies[index1].Attributes).ToList<int>();
          List<int> source = new List<int>();
          if (list1.Count > 12)
          {
            int num2 = list1.Count - 12;
            List<int> list2 = list1.Except<int>((IEnumerable<int>) fromGuidsAttribute).ToList<int>();
            for (int index2 = 0; index2 < num2; ++index2)
            {
              list1.Remove(list2[index2]);
              source.Add(list2[index2]);
            }
          }
          NotifyHelper.SaveGuidsAttributes(attributeById, list1);
          if (source.Count > 0)
          {
            long int64 = Convert.ToInt64(notifyObject.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).Value);
            IDBObject objectById = session.GetObjectByID(int64, false);
            if (objectById != null)
            {
              if (source.Count == 1)
              {
                IDBAttributeType attributeType = session.GetAttributeType(source.First<int>());
                string str = string.Format(LocalizationHolder.rm.GetString("Workflow.Client_86"), (object) notifies[index1].UserName, (object) attributeType.Name, (object) objectById.NameInMessages, (object) 12);
                values.Add(str);
              }
              else
              {
                string str1 = session.GetAttributeType(source[0]).Name;
                for (int index3 = 1; index3 < source.Count; ++index3)
                  str1 = $"{str1}, {session.GetAttributeType(source[index3]).Name}";
                string str2 = string.Format(LocalizationHolder.rm.GetString("Workflow.Client_87"), (object) notifies[index1].UserName, (object) str1, (object) objectById.NameInMessages, (object) 12);
                values.Add(str2);
              }
            }
          }
        }
        else if ((notifies[index1].Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged && notifies[index1].Attributes != null && notifies[index1].Attributes.Count > 0)
          NotifyHelper.SaveGuidsAttributes(attributeById, notifies[index1].Attributes);
      }
    }
    return values;
  }

  private void SaveNotifiesToAttributes(IDBObject notifyObject, List<Notify> notifies)
  {
    object[] attrGuidsValue = ObjChangedNotifService.GetAttrGuidsValue(notifies);
    notifyObject.SetAttributesValues(new AttributeValues[5]
    {
      new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cad00628-306c-11d8-b4e9-00304f19f545")), (object) notifies.Select<Notify, object>((System.Func<Notify, object>) (n => (object) n.UserID)).ToArray<object>()),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cad0062a-306c-11d8-b4e9-00304f19f545")), (object) notifies.Select<Notify, object>((System.Func<Notify, object>) (n => (object) n.Date)).ToArray<object>()),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cad0062b-306c-11d8-b4e9-00304f19f545")), (object) notifies.Select<Notify, object>((System.Func<Notify, object>) (n => (object) (int) n.Options)).ToArray<object>()),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd9940-306c-11d8-b4e9-00304f19f545")), (object) notifies.Select<Notify, object>((System.Func<Notify, object>) (n => (object) n.Comment)).ToArray<object>()),
      new AttributeValues(wfConsts.AttrGUIDsAttributesID, (object) attrGuidsValue)
    });
  }

  private static object[] GetAttrGuidsValue(List<Notify> notifies)
  {
    object[] attrGuidsValue = new object[notifies.Count];
    for (int index = notifies.Count - 1; index >= 0; --index)
    {
      if ((notifies[index].Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged && notifies[index].Attributes != null && notifies[index].Attributes.Count > 0)
      {
        string str1 = string.Empty;
        foreach (int attribute in notifies[index].Attributes)
        {
          string str2 = MetaDataHelper.GetAttributeTypeGuid(attribute).ToString();
          str1 = !(str1 == string.Empty) ? str1 + ','.ToString((IFormatProvider) CultureInfo.InvariantCulture) + str2 : str1 + str2;
        }
        attrGuidsValue[index] = (object) str1;
      }
      else
        attrGuidsValue[index] = (object) string.Empty;
    }
    return attrGuidsValue;
  }

  public List<Notify> GetCommonNotifies(
    Guid sessionGuid,
    Dictionary<long, long> notificationsForObjects)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    List<Notify> commonNotifies = new List<Notify>();
    List<long> collection1 = new List<long>();
    List<NotifyOptions> collection2 = new List<NotifyOptions>();
    List<List<Guid>> collection3 = new List<List<Guid>>();
    List<long> longList = new List<long>();
    List<NotifyOptions> notifyOptionsList = new List<NotifyOptions>();
    List<List<Guid>> guidListList = new List<List<Guid>>();
    List<List<int>> intListList = new List<List<int>>();
    bool flag = true;
    foreach (KeyValuePair<long, long> notificationsForObject in notificationsForObjects)
    {
      List<long> currentNotifUserIDs = new List<long>();
      List<NotifyOptions> currentNotifOptions = new List<NotifyOptions>();
      List<List<Guid>> currentNotifAttributes = new List<List<Guid>>();
      collection1.Clear();
      collection2.Clear();
      collection3.Clear();
      IDBObject dbObject = sessionById.GetObject(notificationsForObject.Value, false);
      if (dbObject != null)
      {
        NotifyHelper.ReadNotifyAttributes(dbObject.GetAttributesValues(GetAttributeValuesModes.None), ref currentNotifUserIDs, ref currentNotifOptions, ref currentNotifAttributes);
        if (flag)
        {
          longList.AddRange((IEnumerable<long>) currentNotifUserIDs);
          notifyOptionsList.AddRange((IEnumerable<NotifyOptions>) currentNotifOptions);
          guidListList.AddRange((IEnumerable<List<Guid>>) currentNotifAttributes);
        }
        else
        {
          List<long> list1 = longList.Intersect<long>((IEnumerable<long>) currentNotifUserIDs).ToList<long>();
          if (list1.Count == 0)
          {
            commonNotifies.Clear();
            return commonNotifies;
          }
          for (int index1 = 0; index1 < list1.Count<long>(); ++index1)
          {
            int index2 = longList.IndexOf(list1[index1]);
            int index3 = currentNotifUserIDs.IndexOf(list1[index1]);
            NotifyOptions notifyOptions1 = notifyOptionsList[index2] & currentNotifOptions[index3];
            if (notifyOptions1 != NotifyOptions.None)
            {
              if ((notifyOptions1 & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged)
              {
                List<Guid> list2 = guidListList[index2].Intersect<Guid>((IEnumerable<Guid>) currentNotifAttributes[index3]).ToList<Guid>();
                if (list2.Count > 0)
                {
                  collection1.Add(list1[index1]);
                  collection2.Add(notifyOptions1);
                  collection3.Add(list2);
                }
                else
                {
                  NotifyOptions notifyOptions2 = notifyOptions1 & ~NotifyOptions.AttributeValueChanged;
                  if (notifyOptions2 != NotifyOptions.None)
                  {
                    collection1.Add(list1[index1]);
                    collection2.Add(notifyOptions2);
                    collection3.Add(new List<Guid>());
                  }
                }
              }
              else
              {
                collection1.Add(list1[index1]);
                collection2.Add(notifyOptions1);
                collection3.Add(new List<Guid>());
              }
            }
          }
          longList = new List<long>((IEnumerable<long>) collection1);
          notifyOptionsList = new List<NotifyOptions>((IEnumerable<NotifyOptions>) collection2);
          guidListList = new List<List<Guid>>((IEnumerable<List<Guid>>) collection3);
        }
        flag = false;
      }
    }
    foreach (List<Guid> guidList in guidListList)
    {
      List<int> intList = new List<int>();
      foreach (Guid attrTypeGuid in guidList)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
        intList.Add(attributeTypeId);
      }
      intListList.Add(intList);
    }
    for (int index = 0; index < longList.Count<long>(); ++index)
    {
      QuickObjectInfo objectInfo = sessionById.GetObjectInfo(longList[index]);
      commonNotifies.Add(new Notify(longList[index], objectInfo.Caption, notifyOptionsList[index], intListList[index]));
    }
    return commonNotifies;
  }

  public List<Notify> GetNotifications(Guid sessionGuid, long senderID, out string errorMessage)
  {
    long notifyID = -1;
    return this.GetNotifications(sessionGuid, senderID, ref notifyID, out errorMessage);
  }

  public List<Notify> GetNotifications(
    Guid sessionGuid,
    long senderID,
    ref long notifyID,
    out string errorMessage)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) senderID, LogicalOperators.AND, 0)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable = sessionById.ObjectsSelect(new Guid("cad00627-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams);
    if (dataTable.Rows.Count > 0)
    {
      DataRow row = dataTable.Rows[0];
      IDBObject notifyObject = sessionById.GetObject(Convert.ToInt64(row[0]), false);
      if (notifyObject != null)
      {
        notifyID = notifyObject.ObjectID;
        List<Notify> notificationsFromObject = this.GetNotificationsFromObject(sessionById, notifyObject, out errorMessage);
        notificationsFromObject.RemoveAll((Predicate<Notify>) (x => x.UserID == -1L));
        return notificationsFromObject;
      }
    }
    errorMessage = string.Empty;
    return new List<Notify>();
  }

  public Dictionary<long, long> GetNotificationsForObjects(Guid sessionGuid, List<long> ids)
  {
    IDBObjectCollection objectCollection = UserSession.GetSessionByID(sessionGuid).GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545"));
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) wfConsts.AttrNotifyObjectID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrNotifyObjectID, RelationalOperators.In, (object) ids.ToArray(), LogicalOperators.NONE, 0, true)
    }, columns);
    DataTable dataTable = objectCollection.Select(paramSet);
    Dictionary<long, long> notificationsForObjects = new Dictionary<long, long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[wfConsts.AttrNotifyObjectID.ToString()]);
      long int64_2 = Convert.ToInt64(row[-2.ToString()]);
      notificationsForObjects.Add(int64_1, int64_2);
    }
    foreach (long id in ids)
    {
      if (!notificationsForObjects.Keys.Contains<long>(id))
        notificationsForObjects.Add(id, 0L);
    }
    return notificationsForObjects;
  }

  public void AddNotificationForObject(Guid sessionGuid, long notifiedObjectId, Notify newNotify)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    List<Notify> notifyList;
    this._notifiesForObjects.TryGetValue(notifiedObjectId, out notifyList);
    IDBObject notifyObject = notifyList != null ? this.AddOrEditNotifyForSpecificUser(sessionGuid, notifiedObjectId, newNotify) : this.CreateNewNotifyObjectAndFillAttrs(sessionById, notifiedObjectId, newNotify);
    if (notifyObject == null)
      return;
    this.AddNotifyObjectToCache(sessionById, notifyObject);
  }

  public void DeleteNotificationForObject(
    Guid sessionGuid,
    long notifiedObjectId,
    Notify deletingNotify)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    List<Notify> notifyList;
    this._notifiesForObjects.TryGetValue(notifiedObjectId, out notifyList);
    if (notifyList == null || notifyList.Count <= 0)
      return;
    IDBObject notifyObject = this.DeleteNotify(sessionGuid, notifiedObjectId, deletingNotify);
    if (notifyObject == null)
      return;
    this.AddNotifyObjectToCache(sessionById, notifyObject);
  }

  private IDBObject DeleteNotify(Guid sessionGuid, long notifiedObjectId, Notify deletingNotify)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    long notificationId = this.GetNotificationId(notifiedObjectId, sessionById);
    if (notificationId == 0L)
      return (IDBObject) null;
    IDBObject notificationObject = sessionById.GetObject(notificationId, false);
    List<long> notificationUsersIds = this.GetNotificationUsersIDs(notificationObject);
    int index = notificationUsersIds.IndexOf(deletingNotify.UserID);
    if (index == -1)
      return (IDBObject) null;
    this.DeleteNotifyForSpecificUser(notificationObject, index, deletingNotify, notificationUsersIds.Count);
    IDBObject dbObject = sessionById.GetObject(notificationId, false);
    if (!dbObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")).IsNull)
      return dbObject;
    dbObject.Delete(0L);
    return (IDBObject) null;
  }

  private List<long> GetNotificationUsersIDs(IDBObject notificationObject)
  {
    return new List<long>(notificationObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")).Values.Cast<long>());
  }

  private void DeleteNotifyForSpecificUser(
    IDBObject notificationObject,
    int index,
    Notify deletingNotify,
    int notificationUserCount)
  {
    notificationObject = notificationObject.CheckOut();
    IDBAttribute attributeByGuid = notificationObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid.Index = index;
    NotifyOptions int32 = (NotifyOptions) Convert.ToInt32(attributeByGuid.Value);
    if (deletingNotify.Options.HasFlag((Enum) NotifyOptions.AttributeValueChanged))
    {
      IDBAttribute attributeById = notificationObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
      attributeById.Index = index;
      List<int> fromGuidsAttribute = NotifyHelper.GetAttributesIDsFromGuidsAttribute(attributeById);
      fromGuidsAttribute.RemoveRange<int>((IEnumerable<int>) deletingNotify.Attributes);
      deletingNotify.Options &= ~NotifyOptions.AttributeValueChanged;
      if (fromGuidsAttribute.Count == 0)
      {
        attributeById.Clear();
        int32 &= ~NotifyOptions.AttributeValueChanged;
      }
      else
        NotifyHelper.SaveGuidsAttributes(attributeById, fromGuidsAttribute);
    }
    NotifyOptions notifyOptions = int32 & ~deletingNotify.Options;
    if (notifyOptions == NotifyOptions.None)
      ObjChangedNotifService.RemoveNotificationAttrValues(notificationObject, index, notificationUserCount);
    else
      attributeByGuid.Value = (object) notifyOptions;
    notificationObject.CheckIn();
  }

  private static void RemoveNotificationAttrValues(
    IDBObject notificationObject,
    int index,
    int notificationUserCount)
  {
    IDBAttribute attributeByGuid1 = notificationObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid1.Index = index;
    IDBAttribute attributeByGuid2 = notificationObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid2.Index = index;
    IDBAttribute attributeByGuid3 = notificationObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid3.Index = index;
    IDBAttribute attributeByGuid4 = notificationObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid4.Index = index;
    IDBAttribute attributeById = notificationObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
    attributeById.Index = index;
    if (notificationUserCount > 1)
    {
      attributeByGuid1.DeleteValue();
      attributeByGuid2.DeleteValue();
      attributeByGuid3.DeleteValue();
      attributeByGuid4.DeleteValue();
      attributeById.DeleteValue();
    }
    else
    {
      attributeByGuid1.ClearValues();
      attributeByGuid2.ClearValues();
      attributeByGuid3.ClearValues();
      attributeByGuid4.ClearValues();
      attributeById.ClearValues();
    }
  }

  private IDBObject CreateNewNotifyObjectAndFillAttrs(
    IUserSession session,
    long notifiedObjectId,
    Notify newNotify)
  {
    IDBObject objectAndFillAttrs = session.GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545")).Create();
    objectAndFillAttrs.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).Value = (object) notifiedObjectId;
    IDBAttribute attributeByGuid1 = objectAndFillAttrs.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid2 = objectAndFillAttrs.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid3 = objectAndFillAttrs.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid4 = objectAndFillAttrs.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeById = objectAndFillAttrs.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
    attributeByGuid1.Values = new object[1]
    {
      (object) newNotify.UserID
    };
    if (newNotify.Date == DateTime.MinValue)
      newNotify.Date = DateTime.Now;
    attributeByGuid2.Values = new object[1]
    {
      (object) newNotify.Date
    };
    attributeByGuid4.Values = new object[1]
    {
      (object) newNotify.Comment
    };
    attributeByGuid3.Values = new object[1]
    {
      (object) (int) newNotify.Options
    };
    attributeById.Value = (object) string.Empty;
    if (newNotify.Options.HasFlag((Enum) NotifyOptions.AttributeValueChanged) && newNotify.Attributes != null && newNotify.Attributes.Count > 0)
    {
      string notifiedObjectName = this.GetNotifiedObjectName(notifiedObjectId, session);
      this.CheckAttributesCount(newNotify, newNotify.Attributes, notifiedObjectName);
      NotifyHelper.SaveGuidsAttributes(attributeById, newNotify.Attributes);
    }
    objectAndFillAttrs.CommitCreation(true);
    return objectAndFillAttrs;
  }

  private IDBObject AddOrEditNotifyForSpecificUser(
    Guid sessionGuid,
    long notifiedObjectId,
    Notify newNotify)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    long notificationId = this.GetNotificationId(notifiedObjectId, sessionById);
    if (notificationId == 0L)
      return (IDBObject) null;
    IDBObject notificationObject = sessionById.GetObject(notificationId, false);
    int index = new List<long>(notificationObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")).Values.Cast<long>()).IndexOf(newNotify.UserID);
    string notifiedObjectName = this.GetNotifiedObjectName(notifiedObjectId, sessionById);
    if (index != -1)
      this.EditNotifyForSpecificUser(notificationObject, index, newNotify, notifiedObjectName);
    else
      this.AddNotifyForSpecificUser(notificationObject, newNotify, notifiedObjectName);
    return notificationObject;
  }

  private void AddNotifyForSpecificUser(
    IDBObject notificationObject,
    Notify newNotify,
    string notifiedObjectNameInMessage)
  {
    notificationObject = notificationObject.CheckOut();
    try
    {
      this.AddAttributes(notificationObject, newNotify, notifiedObjectNameInMessage);
      this.AddDateTime(notificationObject, newNotify.Date);
      this.AddUser(notificationObject, newNotify.UserID);
      this.AddComment(notificationObject, newNotify.Comment);
      this.AddOptions(notificationObject, newNotify.Options);
      notificationObject.CheckIn();
    }
    catch
    {
      notificationObject.CancelChanges();
      throw;
    }
  }

  private void AddOptions(IDBObject notificationObject, NotifyOptions options)
  {
    notificationObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545")).AddValue((object) options);
  }

  private void AddComment(IDBObject notificationObject, string comment)
  {
    if (comment == string.Empty)
      return;
    notificationObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545")).AddValue((object) comment);
  }

  private void AddUser(IDBObject notificationObject, long userID)
  {
    notificationObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")).AddValue((object) userID);
  }

  private void AddDateTime(IDBObject notificationObject, DateTime date)
  {
    IDBAttribute attributeByGuid = notificationObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
    if (date == DateTime.MinValue)
      date = DateTime.Now;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> newValue = (System.ValueType) date;
    attributeByGuid.AddValue((object) newValue);
  }

  private void AddAttributes(
    IDBObject notificationObject,
    Notify newNotify,
    string notifiedObjectNameInMessage)
  {
    this.CheckAttributesCount(newNotify, newNotify.Attributes, notifiedObjectNameInMessage);
    IDBAttribute attributeById = notificationObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
    attributeById.AddValue((object) string.Empty);
    if (!newNotify.Options.HasFlag((Enum) NotifyOptions.AttributeValueChanged))
      return;
    if (newNotify.Attributes == null || newNotify.Attributes.Count == 0)
      newNotify.Options &= ~NotifyOptions.AttributeValueChanged;
    else
      NotifyHelper.SaveGuidsAttributes(attributeById, newNotify.Attributes);
  }

  private string GetNotifiedObjectName(long notifiedObjectId, IUserSession session)
  {
    string notifiedObjectName = string.Empty;
    IDBObject dbObject = session.GetObject(notifiedObjectId, false);
    if (dbObject != null)
      notifiedObjectName = dbObject.NameInMessages;
    return notifiedObjectName;
  }

  private void EditNotifyForSpecificUser(
    IDBObject notificationObject,
    int index,
    Notify newNotify,
    string notifiedObjectNameInMessage)
  {
    notificationObject = notificationObject.CheckOut();
    try
    {
      if (newNotify.Options.HasFlag((Enum) NotifyOptions.AttributeValueChanged))
        this.WriteAttributes(notificationObject, index, newNotify, notifiedObjectNameInMessage);
      this.WriteDateTime(notificationObject, newNotify.Date, index);
      ObjChangedNotifService.WriteComment(notificationObject, newNotify.Comment, index);
      ObjChangedNotifService.WriteOptions(notificationObject, newNotify.Options, index);
      notificationObject.CheckIn();
    }
    catch
    {
      notificationObject.CancelChanges();
      throw;
    }
  }

  private void WriteAttributes(
    IDBObject notificationObject,
    int index,
    Notify newNotify,
    string notifiedObjectNameInMessage)
  {
    if (!newNotify.Options.HasFlag((Enum) NotifyOptions.AttributeValueChanged))
      return;
    if (newNotify.Attributes == null || newNotify.Attributes.Count == 0)
    {
      newNotify.Options &= ~NotifyOptions.AttributeValueChanged;
    }
    else
    {
      IDBAttribute attributeById = notificationObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
      attributeById.Index = index;
      List<int> fromGuidsAttribute = NotifyHelper.GetAttributesIDsFromGuidsAttribute(attributeById);
      fromGuidsAttribute.SafeAddRange<int>((IEnumerable<int>) newNotify.Attributes);
      this.CheckAttributesCount(newNotify, fromGuidsAttribute, notifiedObjectNameInMessage);
      NotifyHelper.SaveGuidsAttributes(attributeById, fromGuidsAttribute);
    }
  }

  private void CheckAttributesCount(
    Notify newNotify,
    List<int> notifyAttrs,
    string notifiedObjectNameInMessage)
  {
    if (notifyAttrs != null && notifyAttrs.Count > 12)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_54"), (object) newNotify.UserName, (object) notifiedObjectNameInMessage, (object) 12));
  }

  private static void WriteOptions(IDBObject notificationObject, NotifyOptions options, int index)
  {
    IDBAttribute attributeByGuid = notificationObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
    NotifyOptions notifyOptions = (NotifyOptions) Convert.ToInt32(attributeByGuid.Values[index]) | options;
    attributeByGuid.Index = index;
    attributeByGuid.Value = (object) notifyOptions;
  }

  private static void WriteComment(IDBObject notificationObject, string comment, int index)
  {
    IDBAttribute attributeByGuid = notificationObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid.Index = index;
    if (!(comment != string.Empty))
      return;
    attributeByGuid.Value = (object) comment;
  }

  private void WriteDateTime(IDBObject notificationObject, DateTime dateTime, int index)
  {
    IDBAttribute attributeByGuid = notificationObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid.Index = index;
    if (dateTime == DateTime.MinValue)
      dateTime = DateTime.Now;
    attributeByGuid.Value = (object) dateTime;
  }

  private long GetNotificationId(long id, IUserSession session)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad00627-306c-11d8-b4e9-00304f19f545"));
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrNotifyObjectID, RelationalOperators.Equal, (object) id, LogicalOperators.NONE, 0, true)
    }, columns);
    DataTable dataTable = objectCollection.Select(paramSet);
    long notificationId = 0;
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      IEnumerator enumerator = dataTable.Rows.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
          notificationId = Convert.ToInt64(((DataRow) enumerator.Current)[-2.ToString()]);
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    return notificationId;
  }

  public List<Notify> GetNotificationsFromObject(
    IUserSession session,
    IDBObject notifyObject,
    out string errorMessage)
  {
    errorMessage = string.Empty;
    if (notifyObject.GetAttributeByGuid(new Guid("cad00061-306c-11d8-b4e9-00304f19f545")) != null)
      return new List<Notify>();
    try
    {
      List<Notify> notificationsFromObject = NotifyHelper.InitNotifyList(notifyObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")));
      if (notificationsFromObject.Count > 0)
      {
        IDBAttribute attributeByGuid1 = notifyObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid2 = notifyObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid3 = notifyObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeById = notifyObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID);
        if (notificationsFromObject.Count != attributeByGuid1.ValuesCount || notificationsFromObject.Count != attributeByGuid2.ValuesCount || notificationsFromObject.Count != attributeByGuid3.ValuesCount || notificationsFromObject.Count != attributeById.ValuesCount)
        {
          long int64 = Convert.ToInt64(notifyObject.GetAttributeByGuid(new Guid("cad0062c-306c-11d8-b4e9-00304f19f545")).Value);
          IDBObject objectById = session.GetObjectByID(int64, false);
          errorMessage = $"У объекта {notifyObject.NameInMessages}, настроенного для объекта {objectById.NameInMessages} идентификатор объекта №{objectById.ID}, нарушена целостность данных: несоответствие количества атрибутов уведомления. Объект уведомления был удален из базы данных.";
          return new List<Notify>();
        }
        for (int index = 0; index < attributeByGuid1.ValuesCount; ++index)
        {
          attributeByGuid1.Index = index;
          if (!attributeByGuid1.IsNull)
          {
            Notify notify = notificationsFromObject[index];
            if (notify.UserID != -1L)
              notify.Date = attributeByGuid1.AsDateTime;
          }
        }
        for (int index = 0; index < attributeByGuid2.ValuesCount; ++index)
        {
          attributeByGuid2.Index = index;
          if (!attributeByGuid2.IsNull)
          {
            Notify notify = notificationsFromObject[index];
            if (notify.UserID != -1L)
              notify.Comment = attributeByGuid2.AsString;
          }
        }
        for (int index1 = 0; index1 < attributeByGuid3.ValuesCount; ++index1)
        {
          attributeByGuid3.Index = index1;
          if (!attributeByGuid3.IsNull)
          {
            Notify notify = notificationsFromObject[index1];
            if (notify.UserID != -1L)
            {
              notify.Options = (NotifyOptions) attributeByGuid3.AsInteger;
              if ((notify.Options & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged)
              {
                attributeById.Index = index1;
                if (attributeById.Value.ToString() != string.Empty)
                {
                  List<Guid> fromGuidsAttribute = NotifyHelper.GetAttributesListFromGuidsAttribute(attributeById);
                  notify.Attributes = new List<int>(fromGuidsAttribute.Count);
                  for (int index2 = 0; index2 < fromGuidsAttribute.Count; ++index2)
                    notify.Attributes.Add(MetaDataHelper.GetAttributeTypeID(fromGuidsAttribute[index2]));
                }
              }
            }
          }
        }
      }
      return notificationsFromObject;
    }
    catch (Exception ex)
    {
      errorMessage = $"Ошибка при чтении атрибутов {notifyObject.NameInMessages}: {ex.Message}";
      return new List<Notify>();
    }
  }

  private interface IMessageExtension
  {
    string Text { get; }
  }

  private class AttributesChangedExtension : ObjChangedNotifService.IMessageExtension
  {
    private List<ObjChangedNotifService.IMessageExtension> _items = new List<ObjChangedNotifService.IMessageExtension>();

    public AttributesChangedExtension(
      List<ObjChangedNotifService.IMessageExtension> items)
    {
      this._items = items;
    }

    public string Text
    {
      get
      {
        if (this._items.Count <= 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < this._items.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append("<br>");
          stringBuilder.Append(this._items[index].Text);
        }
        return stringBuilder.ToString();
      }
    }
  }

  private class AttributeExtension : ObjChangedNotifService.IMessageExtension
  {
    protected string attributeName;
    protected FieldTypes attributeType;
    protected string value;

    public AttributeExtension(string attributeName, FieldTypes attributeType, string value)
    {
      this.attributeName = attributeName;
      this.attributeType = attributeType;
      this.value = this.ConvertValue(value);
    }

    protected string ConvertValue(string value)
    {
      if (value != null && (this.attributeType != FieldTypes.ftString || !(value.ToString() == string.Empty)))
        return value;
      return this.attributeType != FieldTypes.ftString ? "<NULL>" : "<Пусто>";
    }

    public virtual string Text => string.Empty;
  }

  private class AttributeCreatedExtension(
    string attributeName,
    FieldTypes attributeType,
    string value) : ObjChangedNotifService.AttributeExtension(attributeName, attributeType, value)
  {
    public override string Text
    {
      get => $"Добавлен атрибут: {this.attributeName}<br>Новое значение: {this.value}";
    }
  }

  private class AttributeDeletedExtension(
    string attributeName,
    FieldTypes attributeType,
    string value) : ObjChangedNotifService.AttributeExtension(attributeName, attributeType, value)
  {
    public override string Text
    {
      get => $"Удален атрибут: {this.attributeName}<br>Старое значение: {this.value}";
    }
  }

  private class AttributeChangedExtension : ObjChangedNotifService.AttributeExtension
  {
    private string _oldValue;

    public AttributeChangedExtension(
      string attributeName,
      FieldTypes attributeType,
      string oldValue,
      string newValue)
      : base(attributeName, attributeType, newValue)
    {
      this._oldValue = this.ConvertValue(oldValue);
    }

    public override string Text
    {
      get
      {
        return $"Изменено значение атрибута: {this.attributeName}<br>Старое значение: {this._oldValue}<br>Новое значение: {this.value}";
      }
    }
  }
}
