// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumsService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Forums;

public class ForumsService : LongLifeObject, IForumsService
{
  public ForumsService()
  {
    (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AfterNextLCStepEvent += new NextLCStepHandler(this.eventLog_AfterNextLCStepEvent);
  }

  private void eventLog_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    int lcLevelId = MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
    if (nextstep.LevelID != lcLevelId || !sender.IsBaseVersion)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(sender.ObjectType);
    if (objectType == null || (objectType.Options & ObjectTypeOptions.ForumEnabled) != ObjectTypeOptions.ForumEnabled)
      return;
    IDBObjectCollection objectCollection = session.GetObjectCollection(ForumsConsts.forumObjectTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ForumsConsts.discussedGuidAttributeID, RelationalOperators.Equal, (object) sender.ObjectGUID, LogicalOperators.AND, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      session.GetObject(int64, false)?.Delete(0L);
    }
  }

  public Forum GenerationForum(
    long objectID,
    long id,
    ForumFormat format,
    string filtrationOwnerID,
    Guid sessionID)
  {
    Forum forum = new Forum();
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    IDBObject dbObject = sessionById.GetObject(objectID, false);
    if (dbObject == null)
      return forum;
    List<Guid> objectGuids = new List<Guid>();
    foreach (Guid discussionId in this.FindDiscussionIDs(dbObject.ObjectGUID, dbObject.GUID, format, filtrationOwnerID, ref objectGuids, sessionById))
      forum.LoadDiscussion(discussionId, sessionById);
    return forum;
  }

  public void AddMessageToDiscussion(
    UserMessage message,
    long objectID,
    long id,
    ref Forum forum,
    Guid sessionID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    IDBObject dbObject = sessionById.GetObject(objectID, false);
    if (dbObject == null)
      return;
    IDBObject discussion = this.CreateDiscussion(sessionById, dbObject.ObjectGUID, dbObject.GUID);
    message.DicsObjectGuid = discussion.ObjectGUID.ToString();
    this.AddMessageToDiscussion(discussion, message, ref forum, sessionById);
  }

  public IDBObject CreateDiscussion(long objectID, object sessionID)
  {
    IUserSession session = !(sessionID is IUserSession) ? UserSession.GetSessionByID((Guid) sessionID) : sessionID as IUserSession;
    IDBObject discussion = (IDBObject) null;
    IDBObject dbObject = session.GetObject(objectID, false);
    if (dbObject != null)
      discussion = this.CreateDiscussion(session, dbObject.ObjectGUID, dbObject.GUID);
    return discussion;
  }

  private IDBObject CreateDiscussion(IUserSession session, Guid objectGuid, Guid guid)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(ForumsConsts.forumObjectTypeID);
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    conditionStructureList.Add(new ConditionStructure(ForumsConsts.discussedObjectGuidAttributeID, RelationalOperators.Equal, (object) objectGuid, LogicalOperators.AND, 0, false));
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    if (customService != null && customService.Info != null)
      conditionStructureList.Add(new ConditionStructure(new Guid("cad01501-306c-11d8-b4e9-00304f19f545"), RelationalOperators.StartString, (object) customService.Info.Code, LogicalOperators.OR, 1));
    conditionStructureList.Add(new ConditionStructure(new Guid("cad01501-306c-11d8-b4e9-00304f19f545"), RelationalOperators.NotExistsOrEmpty, (object) 0, LogicalOperators.NONE, 1));
    DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), new object[1]
    {
      (object) ObligatoryObjectAttributes.F_GUID
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    IDBObject discussion;
    if (dataTable.Rows.Count == 0)
    {
      discussion = session.GetObjectCollection(ForumsConsts.forumObjectTypeID).Create();
      discussion.Attributes.AddAttribute(ForumsConsts.discussedGuidAttributeID, false).Value = (object) guid;
      discussion.Attributes.AddAttribute(ForumsConsts.discussedObjectGuidAttributeID, false).Value = (object) objectGuid;
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectGuid);
      discussion.Caption = string.Format(LocalizationHolder.rm.GetString("Workflow.Server_45"), (object) objectInfo.Caption);
      discussion.CommitCreation(true, true);
    }
    else
      discussion = session.GetObject(new Guid(dataTable.Rows[0][0].ToString()), false);
    return discussion;
  }

  private void AddMessageToDiscussion(
    IDBObject discussionObject,
    UserMessage message,
    ref Forum forum,
    IUserSession session)
  {
    IDBAttribute stringAttr = discussionObject.Attributes.AddAttribute(ForumsConsts.forumAttributeID, false);
    forum.AddMessage(message, stringAttr, session);
    this.SendNotify(discussionObject, session);
  }

  public void DeleteMessage(ref Forum forum, UserMessage message, Guid sessionID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    IDBObject dbObject = sessionById.GetObject(new Guid(message.DicsObjectGuid), false);
    if (dbObject == null)
      return;
    forum.DeleteMessage(dbObject, message, sessionById);
    this.SendNotify(dbObject, sessionById);
  }

  public void ChangeMessage(Forum forum, Guid discGuid, Guid sessionID, bool sendNotify)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    IDBObject dbObject = sessionById.GetObject(discGuid, false);
    if (dbObject == null)
      return;
    forum.ChangeMessage(dbObject, discGuid, sessionById);
    if (!sendNotify)
      return;
    this.SendNotify(dbObject, sessionById);
  }

  private List<Guid> FindDiscussionIDs(
    Guid objectGuid,
    Guid guid,
    ForumFormat format,
    string filtrationOwnerID,
    ref List<Guid> objectGuids,
    IUserSession session)
  {
    List<Guid> discussionIds = new List<Guid>();
    if (format == ForumFormat.None)
    {
      discussionIds.Add(objectGuid);
      return discussionIds;
    }
    objectGuids = this.FindObjectGuids(session, objectGuid, guid, format, filtrationOwnerID, true);
    IDBObjectCollection objectCollection = session.GetObjectCollection(ForumsConsts.forumObjectTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ForumsConsts.discussedObjectGuidAttributeID, RelationalOperators.In, (object) objectGuids.ToArray(), LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_GUID
    });
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      discussionIds.Add(new Guid(row[0].ToString()));
    return discussionIds;
  }

  private List<Guid> FindObjectGuids(
    IUserSession session,
    Guid objectGuid,
    Guid guid,
    ForumFormat format,
    string filtrationOwnerID,
    bool advancedSearch)
  {
    List<Guid> guids = new List<Guid>();
    QuickObjectInfo objectInfo1 = session.GetObjectInfo(objectGuid);
    if (advancedSearch && !objectInfo1.Empty)
    {
      foreach (long expanded in this.GetExpandedList(objectInfo1.ObjectID, session.SessionGUID))
      {
        IDBObject dbObject = session.GetObject(expanded, false);
        if (dbObject != null)
        {
          foreach (Guid objectGuid1 in this.FindObjectGuids(session, dbObject.ObjectGUID, dbObject.GUID, format, filtrationOwnerID, false))
          {
            if (!guids.Contains(objectGuid1))
              guids.Add(objectGuid1);
          }
        }
      }
    }
    if (!guids.Contains(objectGuid))
      guids.Add(objectGuid);
    switch (format)
    {
      case ForumFormat.Object:
        if (!objectInfo1.Empty)
        {
          using (List<long>.Enumerator enumerator = session.GetObjectIDVersions(objectInfo1.ObjectID, false).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              if (current != objectInfo1.ObjectID)
              {
                QuickObjectInfo objectInfo2 = session.GetObjectInfo(current);
                if (!objectInfo2.Empty && !guids.Contains(objectInfo2.VersionGuid))
                  guids.Add(objectInfo2.VersionGuid);
              }
            }
            break;
          }
        }
        break;
      case ForumFormat.VisibleComposition:
      case ForumFormat.FullVisibleComposition:
        this.LoadCompositions(session, guids, objectGuid, filtrationOwnerID, format == ForumFormat.FullVisibleComposition);
        break;
      case ForumFormat.Changes:
        IDBObject dbObject1 = session.GetObject(objectGuid, false);
        if (dbObject1 != null)
        {
          long modificationId = dbObject1.ModificationID;
          if (modificationId != 0L)
          {
            IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad014d2-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) modificationId, LogicalOperators.NONE, 0, false)
            }, new object[1]
            {
              (object) ObligatoryObjectAttributes.F_GUID
            });
            DataTable dataTable = objectCollection.Select(paramSet);
            objectCollection.SelectWithLocalObjects(paramSet);
            IEnumerator enumerator = dataTable.Rows.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
              {
                Guid guid1 = new Guid(((DataRow) enumerator.Current)[0].ToString());
                if (!guids.Contains(guid1))
                  guids.Add(guid1);
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
          }
          else
            break;
        }
        else
          break;
    }
    return guids;
  }

  private List<long> GetExpandedList(long objectID, Guid session)
  {
    List<long> expandedList = new List<long>();
    if (ApplicationServices.Container.GetService(typeof (IForumExtend)) is ForumExtend service)
      expandedList = service.GetObjects(objectID, session);
    return expandedList;
  }

  private void LoadCompositions(
    IUserSession session,
    List<Guid> guids,
    Guid objectGuid,
    string filtrationOwnerID,
    bool fullComposition)
  {
    ICompositionLoadService service = ApplicationServices.Container.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID));
    QuickObjectInfo objectInfo1 = session.GetObjectInfo(objectGuid);
    List<int> intList = new List<int>();
    if (objectInfo1.Empty)
      return;
    List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
    List<int> relTypes = new List<int>(relationTypesList.Count);
    List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
    List<int> objTypes = new List<int>(objectTypesList.Count);
    objectTypesList.ForEach((Action<IMSObjectType>) (items => objTypes.Add(items.ObjectTypeID)));
    relationTypesList.ForEach((Action<IMSRelationType>) (items => relTypes.Add(items.RelationTypeID)));
    DataTable dataTable = service.LoadComposition((object) session, objectInfo1.ObjectID, objectInfo1.ObjectTypeID, (IEnumerable<int>) relTypes, (IEnumerable<int>) objTypes, (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, filtrationOwnerID, (HybridDictionary) null, fullComposition ? -1 : 1);
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      QuickObjectInfo objectInfo2 = session.GetObjectInfo(int64);
      if (!objectInfo2.Empty)
      {
        Guid versionGuid = objectInfo2.VersionGuid;
        if (!guids.Contains(versionGuid))
          guids.Add(versionGuid);
      }
    }
  }

  private void SendNotify(IDBObject discussionObject, IUserSession session)
  {
    if (discussionObject == null)
      return;
    IDBAttribute attributeById = discussionObject.GetAttributeByID(ForumsConsts.discussedObjectGuidAttributeID);
    if (attributeById == null)
      return;
    IDBObject dbObject = session.GetObject(new Guid(attributeById.Value.ToString()), false);
    if (dbObject == null)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject.ObjectType);
    if (objectType == null || (objectType.Options & ObjectTypeOptions.NotificationsEnabled) != ObjectTypeOptions.NotificationsEnabled)
      return;
    string errorMessage;
    List<Notify> notifications = (session.GetCustomService(typeof (INotifySubscriberService)) as INotifySubscriberService).GetNotifications(session.SessionGUID, dbObject.ID, out errorMessage);
    if (notifications.Count == 0 && !string.IsNullOrEmpty(errorMessage))
      (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Ошибка при рассылке уведомлений о новых сообщениях на форуме по обсуждаемому объекту {dbObject.NameInMessages}: {errorMessage}");
    foreach (Notify notify in notifications)
    {
      if ((notify.Options & NotifyOptions.Forum) == NotifyOptions.Forum && notify.UserID != session.UserID && (ApplicationServices.Container.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (IRouterService)) is IRouterService service)
      {
        string Subject = string.Format(NotifyHelper.MessageSubject, (object) EnumDescConverter.GetEnumDescription((Enum) NotifyOptions.Forum));
        string Text = string.Format(NotifyHelper.MessageBody, (object) EnumDescConverter.GetEnumDescription((Enum) NotifyOptions.Forum), (object) dbObject.ObjectGUID.ToString(), (object) objectType.ObjectName, (object) DataSetProcessor.QString(dbObject.Caption), (object) string.Empty, (object) session.UserName);
        service.CreateMessage(session.SessionGUID, notify.UserID, Subject, Text, session.UserID);
      }
    }
  }
}
