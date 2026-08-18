// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.AutoNotifications.AutoNotificationsService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Expert;
using Intermech.Expressions;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;

#nullable disable
namespace Intermech.Workflow.Server.AutoNotifications;

internal class AutoNotificationsService : LongLifeObject, IAutoNotificationsService
{
  private IUserSession _Session;
  private readonly List<AutoNotificationSettings> _settingsCache = new List<AutoNotificationSettings>();

  public AutoNotificationsService()
  {
    if (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1)
      this._Session = service1.GetSystemSessionPermanentClone(nameof (AutoNotificationsService));
    this.LoadNotifications();
    if (ApplicationServices.Container.GetService(typeof (IDelayedUpdaterService)) is IDelayedUpdaterService service2)
      service2.DelayedNotificationEvent += new DelayedNotificationHandler(this.ProcessNotification);
    if (!(ApplicationServices.Container.GetService(typeof (IEventLogHelper)) is IEventLogHelper service3))
      return;
    service3.AfterCacheReload += new CacheReloadHandler(this.AfterCacheReload);
    service3.BeforeCheckinEvent += new ObjectEventHandler(this.BeforeCheckinEvent);
    service3.AfterNextLCStepEvent += new NextLCStepHandler(this.AfterNextLCStepEvent);
  }

  public AutoNotificationSettings FormSettingsFromObjectsBlobAttr(long objId, Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    AutoNotificationSettings emptyNotifSettings;
    try
    {
      XmlDocument xmlDocFromBlob;
      using (UserSessionContext.CaptureSession(sessionById.SessionGUID))
        xmlDocFromBlob = AutoNotificationsService.GetXmlDocFromBlob(objId);
      if (xmlDocFromBlob == null)
        return (AutoNotificationSettings) null;
      XmlNode documentElement = (XmlNode) xmlDocFromBlob.DocumentElement;
      if (documentElement == null)
        return (AutoNotificationSettings) null;
      emptyNotifSettings = AutoNotificationSettings.CreateEmptyNotifSettings((NotificationEventType) Enum.Parse(typeof (NotificationEventType), documentElement.Attributes["ActionType"].Value), objId);
      emptyNotifSettings.LoadSettingsFromXml(documentElement);
    }
    catch (Exception ex)
    {
      throw new AutoNotificationSettingsException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Workflow_23"), (object) objId), ex);
    }
    return emptyNotifSettings;
  }

  public void SaveSettingsToObjectsBlobAttr(
    AutoNotificationSettings settings,
    long objectId,
    Guid sessionGuid)
  {
    MemoryStream stream = new MemoryStream();
    try
    {
      AutoNotificationsService.SaveSettingsToObject(settings, objectId, sessionGuid, stream);
      this.SaveSettingsToCache(settings);
    }
    finally
    {
      stream.Close();
    }
  }

  public void DeleteSettingsFromCashe(long settingsId)
  {
    lock (this._settingsCache)
      this._settingsCache.Remove(this.FetchSettingsWithId(Math.Abs(settingsId)));
  }

  public List<long> GetArticles(long initiatorId)
  {
    List<long> articles = new List<long>();
    IArticleService service = (IArticleService) ApplicationServices.Container.GetService(typeof (IArticleService));
    if (service != null)
    {
      List<long> listInstances = service.GetListInstances(initiatorId, (object) this._Session.SessionGUID);
      articles.AddRange((IEnumerable<long>) listInstances);
    }
    return articles;
  }

  public List<long> GetRelationPartIds(long partId, long partObjectId)
  {
    if (partObjectId == 0L)
      return new List<long>((IEnumerable<long>) this._Session.GetAllObjectVersionsList(partId, true, false, true));
    return new List<long>() { partObjectId };
  }

  public List<long> GetObjectComposition(
    long initiatorId,
    List<int> childTypesIDs,
    List<int> relTypesIDs,
    long versionRuleID)
  {
    List<long> collection = new List<long>();
    VersionsRule versionsRule = this.GetVersionsRule(versionRuleID);
    ICompositionLoadService service = (ICompositionLoadService) ApplicationServices.Container.GetService(typeof (ICompositionLoadService));
    if (service == null)
      return collection;
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DataTable dataTable = service.LoadCompositions((object) this._Session, initiatorId, (IEnumerable<int>) relTypesIDs, (IEnumerable<ColumnDescriptor>) columns, versionsRule, childTypesIDs.ToArray());
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        collection.SafeAdd<long>(Convert.ToInt64(row[0]));
    }
    return collection;
  }

  public List<long> GetObjectApplicability(
    long initiatorId,
    List<int> parentTypesIDs,
    List<int> relTypesIDs,
    long versionRuleID)
  {
    List<long> collection = new List<long>();
    DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    this.GetVersionsRule(versionRuleID);
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>(1)
    {
      new ObjInfoItem(initiatorId)
    }, this._Session, (IEnumerable<int>) relTypesIDs, 1, dbRsp, this.GetVersionsRule(versionRuleID), DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) parentTypesIDs);
    if (parentSostavData != null && parentSostavData.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
        collection.SafeAdd<long>(Convert.ToInt64(row[0]));
    }
    return collection;
  }

  public List<long> GetObjectsWithSearchScheme(long initiatorId, long searchSchemeId)
  {
    List<long> collection = new List<long>();
    ICompositionService customService = (ICompositionService) this._Session.GetCustomService(typeof (ICompositionService));
    if (customService == null)
      return collection;
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    HybridDictionary Tags = new HybridDictionary();
    Guid selectGUID = Guid.NewGuid();
    customService.Select(this._Session.SessionGUID, initiatorId, searchSchemeId, columns, selectGUID, "", Tags);
    CompositionInfo info;
    for (info = customService.GetInfo(selectGUID); info != null && !info.ErrorPresent && info.Percent < 100; info = customService.GetInfo(selectGUID))
      Thread.Sleep(25);
    if (info.ErrorPresent)
      throw info.ErrorException;
    if (info.Result != null)
    {
      DataTable result = (DataTable) info.Result;
      if (result != null && result.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) result.Rows)
          collection.SafeAdd<long>(Convert.ToInt64(row[0]));
      }
    }
    return collection;
  }

  public bool CheckAttrsWithFormula(AttributeValues[] attrValues, string formula)
  {
    if (formula == string.Empty)
      return true;
    ExpressionTree expressionTree;
    ExpressionVariablesCollection variables;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      expressionTree = parser.Parse(formula);
      variables = expressionTree.Variables;
    }
    object[] valuesForFormula = this.GetAttrValuesForFormula(attrValues, variables);
    if (valuesForFormula == null)
      return false;
    bool flag = false;
    try
    {
      flag = Convert.ToBoolean(expressionTree.Evaluate(valuesForFormula));
    }
    catch (Exception ex)
    {
    }
    return flag;
  }

  public List<long> GetUserIdsFromRoles(List<long> roles)
  {
    List<long> collection = new List<long>();
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) wfConsts.UserTypeID, LogicalOperators.NONE, 0);
    foreach (long role in roles)
    {
      ObjInfoItem projObj = new ObjInfoItem(role, wfConsts.RolesTypeID);
      IUserSession session = this._Session;
      List<int> relations = new List<int>(1);
      relations.Add(wfConsts.SimpleLinkTypeID);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        conditionStructure
      };
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        columnDescriptor
      };
      foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetChildSostavData(projObj, session, (IEnumerable<int>) relations, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (int64 != 0L)
          collection.SafeAdd<long>(int64);
      }
    }
    return collection;
  }

  public List<long> GetUserIdsFromGroups(List<long> groups)
  {
    List<long> collection = new List<long>();
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) wfConsts.UserTypeID, LogicalOperators.NONE, 0);
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    foreach (long group in groups)
      objInfoItemList.Add(new ObjInfoItem(group, wfConsts.GroupTypeID));
    List<ObjInfoItem> projObjList = objInfoItemList;
    IUserSession session = this._Session;
    List<int> relations = new List<int>(1);
    relations.Add(wfConsts.SimpleLinkTypeID);
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      columnDescriptor
    };
    foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) relations, true, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (int64 != 0L)
        collection.SafeAdd<long>(int64);
    }
    return collection;
  }

  public List<long> GetAdresseesFromAttribute(List<long> collectedObjects, int attrId)
  {
    List<long> collection = new List<long>();
    foreach (long collectedObject in collectedObjects)
    {
      IDBObject dbObject = this._Session.GetObject(collectedObject, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(attrId);
        if (attributeById != null)
        {
          foreach (object obj in attributeById.Values)
          {
            long int64 = Convert.ToInt64(obj);
            if (int64 != 0L)
              collection.SafeAdd<long>(int64);
          }
        }
      }
    }
    return collection;
  }

  public List<long> GetRelationAuthor(long relationId)
  {
    List<long> relationAuthor = new List<long>(1);
    IDBRelation relation = this._Session.GetRelation(relationId, false);
    if (relation != null)
      relationAuthor.Add(relation.CreatorID);
    return relationAuthor;
  }

  public List<long> GetAuthors(List<long> collectedObjects)
  {
    List<long> authors = new List<long>(1);
    foreach (long collectedObject in collectedObjects)
    {
      IDBObject dbObject = this._Session.GetObject(collectedObject, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrAuthorID);
        if (attributeById != null)
        {
          long asInteger = attributeById.AsInteger;
          if (asInteger != 0L)
            authors.Add(asInteger);
        }
      }
    }
    return authors;
  }

  public List<long> GetOwners(List<long> collectedObjects)
  {
    List<long> collection = new List<long>(1);
    foreach (long collectedObject in collectedObjects)
    {
      IDBObject dbObject = this._Session.GetObject(collectedObject, false);
      if (dbObject != null)
      {
        object[] valuesById = dbObject.GetValuesByID(wfConsts.AttrOwnerID, false);
        if (valuesById != null && valuesById.Length != 0 && valuesById[0] != DBNull.Value)
        {
          long int64 = Convert.ToInt64(valuesById[0]);
          if (int64 != 0L)
          {
            QuickObjectInfo objectInfo = this._Session.GetObjectInfo(int64);
            if (objectInfo.ObjectTypeID == wfConsts.GroupTypeID)
            {
              List<long> userIdsFromGroups = this.GetUserIdsFromGroups(new List<long>(1)
              {
                int64
              });
              collection.SafeAddRange<long>((IEnumerable<long>) userIdsFromGroups);
            }
            if (objectInfo.ObjectTypeID == wfConsts.UserTypeID)
              collection.SafeAdd<long>(int64);
          }
        }
      }
    }
    return collection;
  }

  public List<long> GetProjectManagers(List<long> collectedObjects)
  {
    List<long> collection = new List<long>();
    foreach (long project in this.GetProjects(collectedObjects))
    {
      if (this._Session.GetObject(project, false) is IDBProjectObject dbProjectObject)
      {
        foreach (ProjectParticipantInfo participant in dbProjectObject.GetParticipants())
        {
          if (dbProjectObject.IsProjectManager(participant.ParticipantID))
            collection.SafeAdd<long>(participant.ParticipantID);
        }
      }
    }
    return collection;
  }

  private List<long> GetProjects(List<long> collectedObjects)
  {
    List<long> collection = new List<long>();
    foreach (long collectedObject in collectedObjects)
    {
      IDBObject dbObject = this._Session.GetObject(collectedObject, false);
      if (dbObject != null)
      {
        object[] valuesById = dbObject.GetValuesByID(MetaDataHelper.GetAttributeTypeID("cad00811-306c-11d8-b4e9-00304f19f545"), false);
        if (valuesById != null)
          collection.SafeAdd<long>(Convert.ToInt64(valuesById[0]));
      }
    }
    return collection;
  }

  public List<long> GetAuthorsOrganizationUnitsChiefs(List<long> collectedObjects)
  {
    List<long> authors = this.GetAuthors(collectedObjects);
    if (authors.Count == 0)
      return new List<long>(0);
    List<ObjInfoItem> objInfoItems = new List<ObjInfoItem>();
    foreach (long objectId in authors)
      objInfoItems.Add(new ObjInfoItem(objectId, wfConsts.UserTypeID));
    foreach (long authorsGroup in this.GetAuthorsGroups(objInfoItems))
      objInfoItems.Add(new ObjInfoItem(authorsGroup, wfConsts.GroupTypeID));
    return this.GetChiefs(objInfoItems);
  }

  public List<long> GetOwnersDeparmentChiefs(List<long> collectedObjects)
  {
    List<ObjInfoItem> objInfoItems = new List<ObjInfoItem>();
    foreach (long owner in this.GetOwners(collectedObjects))
      objInfoItems.Add(new ObjInfoItem(owner, wfConsts.UserTypeID));
    return this.GetChiefs(objInfoItems);
  }

  public void SendToSpecificEmails(
    List<string> emails,
    string subject,
    string message,
    IUserSession session)
  {
    if (!(session.GetCustomService(typeof (IEmailService)) is IEmailService customService))
    {
      foreach (string email in emails)
        this.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Interfaces.Workflow_UntunedEmail"), (object) email));
    }
    else
    {
      List<string> stringList = new List<string>((IEnumerable<string>) emails);
      foreach (EmailServer server in customService.Servers)
      {
        EmailAccaunt[] accaunts = customService.GetAccaunts(server.Guid);
        foreach (string email in emails)
        {
          foreach (EmailAccaunt emailAccaunt in accaunts)
          {
            if (emailAccaunt.Email == email)
            {
              try
              {
                customService.SendMessage(this._Session.SessionGUID, emailAccaunt.Guid, email, subject, message);
                stringList.Remove(email);
                break;
              }
              catch (Exception ex)
              {
                this.AddMessageToLog(ex.Message);
                break;
              }
            }
          }
        }
      }
      foreach (string str in stringList)
        this.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Interfaces.Workflow_UntunedEmail"), (object) str));
    }
  }

  public List<MyElement> EmailProcessing(
    IUserSession session,
    long[] ToUserIDs,
    string subject,
    string message)
  {
    List<MyElement> myElementList1 = new List<MyElement>(((IEnumerable<long>) ToUserIDs).Count<long>());
    List<MyElement> myElementList2 = new List<MyElement>(((IEnumerable<long>) ToUserIDs).Count<long>());
    foreach (long toUserId in ToUserIDs)
    {
      IDBObject dbObject = session.GetObject(toUserId, false);
      if (dbObject != null)
        myElementList2.Add(new MyElement((object) dbObject.ObjectID, dbObject.Caption, (object) null));
    }
    if (!(session.GetCustomService(typeof (IEmailService)) is IEmailService customService) || customService.Servers == null || customService.Servers.Length < 1)
      return myElementList2;
    EmailServer server = customService.Servers[0];
    EmailAccaunt[] accaunts = customService.GetAccaunts(server.Guid);
    if (accaunts == null || accaunts.Length == 0)
      return myElementList2;
    Guid guid = accaunts[0].Guid;
    foreach (MyElement myElement in myElementList2)
    {
      IDBObject dbObject = session.GetObject((long) myElement.Value, false);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad002de-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid == null || attributeByGuid.AsString == "")
        {
          myElementList1.Add(myElement);
        }
        else
        {
          string asString = attributeByGuid.AsString;
          try
          {
            customService.SendMessage(session.SessionGUID, guid, asString, subject, message);
          }
          catch
          {
            myElementList1.Add(myElement);
          }
        }
      }
    }
    return myElementList1;
  }

  public void InternalMailProcessing(
    IUserSession session,
    List<long> users,
    string subject,
    string message)
  {
    if (!(session.GetCustomService(typeof (IRouterService)) is IRouterService customService))
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cad0000d-306c-11d8-b4e9-00304f19f545"));
    customService.CreateMessage(session.SessionGUID, users.ToArray(), subject, message, objectInfo.ObjectID);
  }

  public void AddMessageToLog(string message)
  {
    (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(message, Intermech.Consts.traceAlways, wfConsts.AutoNotifLogFile);
  }

  private void LoadNotifications() => this.LoadCache();

  private void LoadCache()
  {
    lock (this._settingsCache)
    {
      this._settingsCache.Clear();
      foreach (long autoNotificationIds in this.GetAutoNotificationIdsList())
      {
        if (this.IsAutoNotificationActive(autoNotificationIds))
          this.AddSettingsToCache(autoNotificationIds);
      }
    }
  }

  private void AddSettingsToCache(long notifyObjId)
  {
    AutoNotificationSettings notificationSettings = (AutoNotificationSettings) null;
    try
    {
      notificationSettings = this.FormSettingsFromObjectsBlobAttr(notifyObjId, this._Session.SessionGUID);
    }
    catch (Exception ex)
    {
      this.AddMessageToLog(ex.Message);
    }
    if (notificationSettings == null)
      return;
    lock (this._settingsCache)
      this._settingsCache.SafeAdd<AutoNotificationSettings>(notificationSettings);
  }

  private List<long> GetAutoNotificationIdsList()
  {
    List<long> notificationIdsList = new List<long>();
    DataTable dataTable = this._Session.GetObjectCollection(wfConsts.AutoNotificationTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return notificationIdsList;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (row[0] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (int64 != 0L)
          notificationIdsList.Add(int64);
      }
    }
    return notificationIdsList;
  }

  private bool IsAutoNotificationActive(long notificationId)
  {
    IDBObject dbObject = this._Session.GetObject(notificationId, false);
    return dbObject != null && dbObject.LCStep == wfConsts.CreateAutoNotificationLCStepID;
  }

  private void AfterCacheReload(IDbManager db) => this.LoadNotifications();

  private void AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender.TypeID != wfConsts.AutoNotificationTypeID)
      return;
    if (sender.LCStep == wfConsts.CreateAutoNotificationLCStepID)
      this.AddSettingsToCache(sender.ObjectID);
    else
      this.DeleteSettingsFromCashe(sender.ObjectID);
  }

  private void BeforeCheckinEvent(IDBObject sender, IUserSession session)
  {
    foreach (AutoNotificationSettings settings in this._settingsCache)
    {
      if (settings is AttrChangingAutoNotificationSettings notificationSettings)
      {
        List<int> attrIds = notificationSettings.AttrIDs;
        if (attrIds != null && attrIds.Count != 0)
        {
          int typeId = sender.TypeID;
          bool flag = false;
          foreach (int filterType in notificationSettings.FilterTypes)
          {
            if (MetaDataHelper.IsObjectTypeChildOf(typeId, filterType))
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            IDBObject archObject = session.GetObject(Math.Abs(sender.ObjectID));
            FormulaForAttribute formula1 = settings.GetFormula();
            string formula2 = formula1.Formula;
            if (string.IsNullOrWhiteSpace(formula1.Formula) || this.CheckInitiatorWithFormula(sender, archObject, formula1))
            {
              List<long> collectedObjects = settings.CollectObjectsForSearchingAdresseeIds(sender.ObjectID);
              if (string.IsNullOrWhiteSpace(formula1.Formula) || !formula1.SpreadFormulaForObject || this.CheckObjectsAttrsWithFormula(collectedObjects, formula2))
              {
                AttributeValues[] attributesValues1 = archObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes);
                AttributeValues[] attributesValues2 = sender.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes);
                List<AttributeValues> changedAttrValues = AutoNotificationsService.FindChangedAttrValues(attrIds, attributesValues1, attributesValues2);
                if (changedAttrValues.Count == 0)
                  break;
                List<long> adresseeIds = this.FindAdresseeIds(settings, session.UserID, sender.ObjectID, collectedObjects);
                new SetAttributesValuesDelayedNotification(session.UserID, ActionType.Write, attributesValues1, attributesValues2, sender.ObjectID, sender.ObjectType, changedAttrValues.ToArray()).Send(settings, adresseeIds, this._Session);
              }
            }
          }
        }
      }
    }
  }

  private static List<AttributeValues> FindChangedAttrValues(
    List<int> attrIdsForChecking,
    AttributeValues[] oldValues,
    AttributeValues[] newValues)
  {
    List<AttributeValues> collection = new List<AttributeValues>();
    AttributeValues attributeValues = (AttributeValues) null;
    AttributeValues other = (AttributeValues) null;
    foreach (int num in attrIdsForChecking)
    {
      foreach (AttributeValues oldValue in oldValues)
      {
        if (oldValue.AttributeID == num)
        {
          attributeValues = oldValue;
          break;
        }
      }
      foreach (AttributeValues newValue in newValues)
      {
        if (newValue.AttributeID == num)
        {
          other = newValue;
          break;
        }
      }
      if (attributeValues != null || other != null)
      {
        if (attributeValues == null && other != null)
          collection.SafeAdd<AttributeValues>(other);
        else if (attributeValues != null && other == null)
          collection.SafeAdd<AttributeValues>(attributeValues);
        else if (!attributeValues.Equals(other, true))
          collection.SafeAdd<AttributeValues>(other);
      }
    }
    return collection;
  }

  private bool CheckInitiatorWithFormula(
    IDBObject checkinObject,
    IDBObject archObject,
    FormulaForAttribute formulaForAttr)
  {
    return this.CheckAttrsWithFormula(!formulaForAttr.UseOldAttrValues ? checkinObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes) : archObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes), formulaForAttr.Formula);
  }

  private void ProcessNotification(DelayedNotification notification)
  {
    if (!(notification is TypableDelayedNotification typableNotification))
      return;
    foreach (AutoNotificationSettings settings in this._settingsCache)
    {
      if (notification.IsSuitableForSettings(settings, this._Session))
      {
        long forAdresseeFinding = this.GetInstanceIdForAdresseeFinding(typableNotification, settings);
        FormulaForAttribute formula = settings.GetFormula();
        if (this.CheckInstanceAttrsWithFormula(forAdresseeFinding, typableNotification, formula))
        {
          List<long> collectedObjects = this.CollectObjectsForAdresseeFinding(notification, settings, forAdresseeFinding);
          if (formula == null || !formula.SpreadFormulaForObject || this.CheckObjectsAttrsWithFormula(collectedObjects, formula.Formula))
          {
            List<long> adresseeIds = this.FindAdresseeIds(settings, notification.UserID, forAdresseeFinding, collectedObjects);
            notification.Send(settings, adresseeIds, this._Session);
          }
        }
      }
    }
  }

  private List<long> FindAdresseeIds(
    AutoNotificationSettings settings,
    long currentUser,
    long instanceIdForAdresseeFinding,
    List<long> collectedObjects)
  {
    List<long> adresseeIds = settings.GetAdresseeIds(instanceIdForAdresseeFinding, collectedObjects);
    adresseeIds.Remove(currentUser);
    adresseeIds.Remove(this._Session.GetObjectInfo(new Guid("cad0000d-306c-11d8-b4e9-00304f19f545")).ObjectID);
    return adresseeIds;
  }

  private List<long> CollectObjectsForAdresseeFinding(
    DelayedNotification notification,
    AutoNotificationSettings settings,
    long instanceIdForAdresseeFinding)
  {
    List<long> longList;
    if (notification is RelationDelayedNotification)
    {
      RelationDelayedNotification delayedNotification = (RelationDelayedNotification) notification;
      long projId = delayedNotification.ProjID;
      long partId = delayedNotification.PartID;
      long partObjectId = delayedNotification.PartObjectID;
      longList = settings.CollectObjectsForSearchingAdresseeIdsForRelation(projId, partId, partObjectId);
    }
    else
      longList = settings.CollectObjectsForSearchingAdresseeIds(instanceIdForAdresseeFinding);
    return longList;
  }

  private bool CheckInstanceAttrsWithFormula(
    long instanceIdForAdresseeFinding,
    TypableDelayedNotification typableNotification,
    FormulaForAttribute formulaForAttribute)
  {
    if (instanceIdForAdresseeFinding == typableNotification.InstanceID)
    {
      if (typableNotification.CheckInitiatorAttrsWithFormula(formulaForAttribute))
        return true;
    }
    else if (this.CheckObjectsAttrsWithFormula(new List<long>(1)
    {
      instanceIdForAdresseeFinding
    }, formulaForAttribute.Formula))
      return true;
    return false;
  }

  private long GetInstanceIdForAdresseeFinding(
    TypableDelayedNotification typableNotification,
    AutoNotificationSettings settings)
  {
    long objectID = typableNotification.InstanceID;
    if (settings.NotifEventType == NotificationEventType.CreateVersion)
    {
      IDBObject dbObject = this._Session.GetObject(objectID, false);
      if (dbObject != null && dbObject.ParentVersionID != 0L)
        objectID = dbObject.ParentVersionID;
    }
    return objectID;
  }

  private bool CheckObjectsAttrsWithFormula(List<long> collectedObjects, string formula)
  {
    foreach (long collectedObject in collectedObjects)
    {
      IDBObject dbObject = this._Session.GetObject(collectedObject, false);
      if (dbObject != null && !this.CheckAttrsWithFormula(dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes), formula))
        return false;
    }
    return true;
  }

  private static XmlDocument GetXmlDocFromBlob(long objId)
  {
    MemoryStream memoryStream = new MemoryStream();
    XmlDocument xmlDocFromBlob;
    try
    {
      new BlobProcReader(objId, AttributableElements.Object, wfConsts.AttrAutoNotificationSettingsID, 0, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      memoryStream.Position = 0L;
      xmlDocFromBlob = new XmlDocument();
      if (memoryStream.Length != 0L)
        xmlDocFromBlob.Load((Stream) memoryStream);
    }
    finally
    {
      memoryStream.Close();
    }
    return xmlDocFromBlob;
  }

  private void SaveSettingsToCache(AutoNotificationSettings settings)
  {
    AutoNotificationSettings notificationSettings = this.FetchSettingsWithId(settings.AutoNotificationID);
    if (notificationSettings == null)
    {
      lock (this._settingsCache)
        this._settingsCache.Add(settings);
    }
    else
    {
      lock (this._settingsCache)
      {
        this._settingsCache.Remove(notificationSettings);
        this._settingsCache.Add(settings);
      }
    }
  }

  private AutoNotificationSettings FetchSettingsWithId(long objectId)
  {
    lock (this._settingsCache)
    {
      List<AutoNotificationSettings> list = this._settingsCache.Where<AutoNotificationSettings>((System.Func<AutoNotificationSettings, bool>) (sett => sett.AutoNotificationID == objectId)).ToList<AutoNotificationSettings>();
      return list.Any<AutoNotificationSettings>() ? list.First<AutoNotificationSettings>() : (AutoNotificationSettings) null;
    }
  }

  private static void SaveSettingsToObject(
    AutoNotificationSettings settings,
    long objectId,
    Guid sessionGuid,
    MemoryStream stream)
  {
    XmlDocument xmlDocWithSettings = settings.CreateXmlDocWithSettings();
    using (UserSessionContext.CaptureSession(UserSession.GetSessionByID(sessionGuid)))
    {
      xmlDocWithSettings.Save((Stream) stream);
      stream.Position = 0L;
      BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(objectId, AttributableElements.Object, wfConsts.AttrAutoNotificationSettingsID, 0, 0, aBlobInformation, (Stream) stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      stream.Position = 0L;
    }
  }

  private VersionsRule GetVersionsRule(long _versionRuleID)
  {
    IDBObject RuleObject = this._Session.GetObject(_versionRuleID);
    VersionsRule versionsRule = (VersionsRule) null;
    if (RuleObject != null)
    {
      versionsRule = new VersionsRule();
      versionsRule.LoadFromObject(this._Session, RuleObject);
    }
    return versionsRule;
  }

  private object[] GetAttrValuesForFormula(
    AttributeValues[] attrValues,
    ExpressionVariablesCollection variables)
  {
    object[] valuesForFormula = new object[variables.Count];
    for (int index = 0; index < variables.Count; ++index)
    {
      AttributeValues attributeValues = (AttributeValues) null;
      foreach (AttributeValues attrValue in attrValues)
      {
        if (attrValue.AttributeName == variables[index].Name)
          attributeValues = attrValue;
      }
      if (attributeValues == null)
        return (object[]) null;
      if (attributeValues.AttributeType == FieldTypes.ftObjectLink)
      {
        if (attributeValues.Values[0] == DBNull.Value || attributeValues.Values[0] == null)
          return (object[]) null;
        QuickObjectInfo objectInfo = Session.GetObjectInfo(Convert.ToInt64(attributeValues.Values[0]));
        valuesForFormula[index] = (object) objectInfo.Caption;
      }
      else
        valuesForFormula[index] = attributeValues.Values[0];
    }
    return valuesForFormula;
  }

  private List<long> GetAuthorsGroups(List<ObjInfoItem> objInfoItems)
  {
    List<long> collection = new List<long>();
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) wfConsts.GroupTypeID, LogicalOperators.NONE, 0);
    List<ObjInfoItem> partObjList = objInfoItems;
    IUserSession session = this._Session;
    List<int> relations = new List<int>(1);
    relations.Add(wfConsts.SimpleLinkTypeID);
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      columnDescriptor
    };
    foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, session, (IEnumerable<int>) relations, true, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (int64 != 0L)
        collection.SafeAdd<long>(int64);
    }
    return collection;
  }

  private List<long> GetChiefs(List<ObjInfoItem> objInfoItems)
  {
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) wfConsts.AttrDirectorID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) new object[1]
    {
      (object) wfConsts.DepartmentTypeID
    }, LogicalOperators.NONE, 0);
    List<ObjInfoItem> partObjList = objInfoItems;
    IUserSession session = this._Session;
    List<int> relations = new List<int>(1);
    relations.Add(wfConsts.SimpleLinkTypeID);
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      columnDescriptor
    };
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, session, (IEnumerable<int>) relations, true, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    List<long> collection = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (int64 != 0L)
        collection.SafeAdd<long>(int64);
    }
    return collection;
  }
}
