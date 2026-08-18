// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.OfficeServer
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using Intermech.Remoting;
using Intermech.Search.MSOfficeReviews;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

public class OfficeServer : IPackage, IUpdatable
{
  private const string ModuleName = "OFFICE";
  private const int PluginDbVersion = 2;
  private const int PluginDbRevision = 2;
  [NotNull]
  private Dictionary<OfficeDocumentTypes, FormInformation> _officeForms;
  [NotNull]
  private FormInformation _officeDocForm;
  [NotNull]
  private static readonly string[] _updateScripts = new string[7]
  {
    "Intermech.Office.AttributeGroups.xml",
    "Intermech.Office.Attributes.xml",
    "Intermech.Office.RelationTypes.xml",
    "Intermech.Office.ObjectTypes.xml",
    "Intermech.Office.Objects.xml",
    "Intermech.Search.MSOfficeReviews.Updates.xml",
    "Intermech.Office.SMDO.xml"
  };
  [CanBeNull]
  private List<long> _importedObjects;
  [NotNull]
  private readonly MSOfficeReviewsServerModule _msOfficeReviewsServerModule = new MSOfficeReviewsServerModule();

  public void Load([NotNull] IServiceProvider serviceProvider)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.Load"))
    {
      OfficeConsts.Init(systemSessionKeeper.Session);
      ICustomServices service1 = serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices;
      service1.AddService(typeof (IOfficeDocumentTypeService), (object) new OfficeDocumentTypeService());
      OfficeRegistrationService serviceInstance1 = new OfficeRegistrationService();
      service1.AddService(typeof (IOfficeRegistrationService), (object) serviceInstance1);
      service1.AddService(typeof (IRegistrationNumberGenerator), (object) new RegistrationNumberGeneratorService());
      service1.AddService(typeof (IResolutionAccessService), (object) new ResolutionAccessService());
      IPluginManager service2 = ApplicationServices.Container.GetService<IPluginManager>(false);
      if (service2 != null)
        service2.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
      ICreatorContainer service3 = ApplicationServices.Container.GetService<IDBObjectService>() as ICreatorContainer;
      if (!DbResolutionCreator.Registered)
      {
        service3.AddCreator((object) OfficeConsts.ObjtypeResolutionsGuid, (object) new DbResolutionCreator());
        DbResolutionCreator.Registered = true;
      }
      service3.AddCreator((object) OfficeConsts.ObjtypeOfficeDocumentsGuid, (object) new DBOfficeDocumentCreator());
      IEventLogHelper service4 = ApplicationServices.Container.GetService<IEventLogHelper>();
      service4.AfterLoginEvent += new LoginHandler(OfficeServer.eventLogHelper_AfterLoginEvent);
      service4.BeforeRecordsSelectEvent += new BeforeRecordsSelectHandler(OfficeServer.eventLogHelper_BeforeRecordsSelectEvent);
      serviceInstance1.LoadCache(systemSessionKeeper.Session);
      this._officeForms = new Dictionary<OfficeDocumentTypes, FormInformation>(3)
      {
        {
          OfficeDocumentTypes.Internal,
          new FormInformation(systemSessionKeeper.Session.GetObject(OfficeConsts.FormInternalDocumentID, true))
        },
        {
          OfficeDocumentTypes.Incoming,
          new FormInformation(systemSessionKeeper.Session.GetObject(OfficeConsts.FormIngoingDocumentID, true))
        },
        {
          OfficeDocumentTypes.Outgoing,
          new FormInformation(systemSessionKeeper.Session.GetObject(OfficeConsts.FormOutgoingDocumentID, true))
        }
      };
      this._officeDocForm = new FormInformation(systemSessionKeeper.Session.GetObject(OfficeConsts.FormOfficeDocID, true));
      OfficeGeneralSettingsService serviceInstance2 = new OfficeGeneralSettingsService();
      serviceInstance2.Reload(systemSessionKeeper.Session.SessionGUID);
      service1.AddService(typeof (IOfficeGeneralSettingsService), (object) serviceInstance2);
      IServerSynchronizersManager service5 = ApplicationServices.Container.GetService<IServerSynchronizersManager>();
      OfficeCacheSynchronizer cacheSynchronizer = new OfficeCacheSynchronizer();
      OfficeCacheSynchronizer synchronizer = cacheSynchronizer;
      service5.RegisterSynchronizer((IServerSynchronizer) synchronizer);
      serviceInstance1._ServersSynchronizer = cacheSynchronizer;
    }
    this._msOfficeReviewsServerModule.Load();
  }

  private static void eventLogHelper_BeforeRecordsSelectEvent(
    [CanBeNull] object sender,
    [NotNull] BeforeRecordsSelectEventArgs args)
  {
    switch (RemotingCallContext.GetData("X-IPS-NoFilterQuery"))
    {
      case "true":
        break;
      default:
        if (args is BeforeObjectsCollectionSelectEventArgs a)
        {
          int objectType = a.ObjectType;
          if (objectType == -1)
            break;
          UserSession session = (UserSession) a.Session;
          ICacheDataset dbCache = session.DBCache;
          if (dbCache.IsDocument(objectType))
          {
            if (!session.GetCustomService<IOfficeGeneralSettingsService>().Settings.PrivateOffice || SupervisorHelper.UserIsSupervisor(args.Session))
              break;
            long internalDepartmentId = session.InternalDepartmentID;
            if (internalDepartmentId == 0L)
              break;
            OfficeServer.AddRecordsSelectConditions((BeforeRecordsSelectEventArgs) a, new ConditionStructure[1]
            {
              new ConditionStructure(0, RelationalOperators.InFiltrationTable, (object) internalDepartmentId, LogicalOperators.NONE, 0, false)
            });
            break;
          }
          if (!dbCache.IsInhertitedFrom(objectType, OfficeConsts.ObjtypeResolutionsID) || !session.GetCustomService<IOfficeGeneralSettingsService>().Settings.FilterResolutions || SupervisorHelper.UserIsSupervisor(args.Session))
            break;
          OfficeServer.AddRecordsSelectConditions((BeforeRecordsSelectEventArgs) a, new ConditionStructure[1]
          {
            new ConditionStructure(0, RelationalOperators.InFiltrationTable, (object) session.UserID, LogicalOperators.NONE, 0, false)
          });
          break;
        }
        if (!(sender is DBRelationCollection relationCollection))
          break;
        int relationTypeId = relationCollection.RelationTypeID;
        UserSession session1 = (UserSession) args.Session;
        int officeCompositionId = OfficeConsts.ReltypeOfficeCompositionID;
        if (relationTypeId != officeCompositionId || !session1.GetCustomService<IOfficeGeneralSettingsService>().Settings.FilterResolutions || SupervisorHelper.UserIsSupervisor(args.Session))
          break;
        OfficeServer.AddRecordsSelectConditions(args, new ConditionStructure[1]
        {
          new ConditionStructure(0, RelationalOperators.InFiltrationTable, (object) session1.UserID, LogicalOperators.NONE, 0, false)
        });
        break;
    }
  }

  private static void AddRecordsSelectConditions(
    [NotNull] BeforeRecordsSelectEventArgs a,
    [NotNull] ConditionStructure[] newConditions)
  {
    List<ConditionStructure> conditionStructureList;
    if (a.OldParameters.Conditions != null && a.OldParameters.Conditions.Length != 0)
    {
      conditionStructureList = new List<ConditionStructure>((IEnumerable<ConditionStructure>) a.OldParameters.Conditions);
      ConditionStructure conditionStructure = conditionStructureList[conditionStructureList.Count - 1];
      if (conditionStructureList.Count > 1)
      {
        ++conditionStructureList[0].GroupID;
        ++conditionStructure.GroupID;
      }
      conditionStructure.LogicalOperator = LogicalOperators.AND;
    }
    else
      conditionStructureList = new List<ConditionStructure>();
    conditionStructureList.AddRange((IEnumerable<ConditionStructure>) newConditions);
    a.OldParameters.Conditions = conditionStructureList.ToArray();
    a.NewParameters = new DBRecordSetParams?(a.OldParameters);
  }

  private static void eventLogHelper_AfterLoginEvent([NotNull] IUserSession session)
  {
    if (session.UserID == OfficeConsts.ObjectSystemUserID)
      return;
    long userUnit = session.GetCustomService<IOfficeRegistrationService>().GetUserUnit(session.UserID);
    if (userUnit == 0L)
      return;
    (session as IServerSession).SetSessionPluginsData((object) "DEPARTMENT_ID", (object) userUnit);
  }

  private void pluginManager_LoadComplete([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ApplicationServices.Container.GetService<IFormDesignerServer>(false)?.Register(OfficeConsts.ObjtypeDocumentsID, AttributableElements.Object, new UpdateHandlerInfo(0, new UpdateHandler(this.FilterForm)));
  }

  private void FilterForm([CanBeNull] object sender, [NotNull] UpdateHandlerEventArgs args)
  {
    if (!(args.Parent is IDBObject parent))
      return;
    IDBAttribute attributeById = parent.GetAttributeByID(OfficeConsts.AttrOfficeDocumentTypeID);
    if ((attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
      return;
    List<FormInformation> formInformationList = new List<FormInformation>();
    if (args.OldList != null && args.OldList.Count > 0)
      formInformationList.AddRange((IEnumerable<FormInformation>) args.OldList);
    else
      formInformationList.Add(this._officeDocForm);
    args.NewList = formInformationList;
    FormInformation fi;
    if (!this._officeForms.TryGetValue((OfficeDocumentTypes) attributeById.AsInteger, out fi))
      return;
    args.AddOrChangeFormInformationInNewList(fi, FormOrderPriority.High, 100);
  }

  public void Unload() => this._msOfficeReviewsServerModule.Unload();

  [NotNull]
  public string Name => Localization.GetString("Office.Server_9");

  [NotNull]
  public string[] GetUpdateScripts() => OfficeServer._updateScripts;

  public void BeforeExecScript(IUserSession session, [NotNull] string scriptName)
  {
    if (scriptName.Equals(OfficeServer._updateScripts[0]))
      session.CheckMaximumPluginDbVersion("OFFICE", 2, 2);
    if (!scriptName.Equals("Intermech.Office.Objects.xml"))
      return;
    ApplicationServices.Container.GetService<IIDLinkTranslate>().IsIDLinkEvent += new IsIDLinkEventHandler(OfficeServer.linkTranslate_IsIDLinkEvent);
    this._importedObjects = new List<long>();
    ApplicationServices.Container.GetService<ICustomImport>().CustomImportedEvent += new CustomImported(this.customImport_CustomImportedEvent);
  }

  private void customImport_CustomImportedEvent([CanBeNull] object sender, [NotNull] CustomImportedEventArgs e)
  {
    if (e.CategoryID != 1 || this._importedObjects == null)
      return;
    this._importedObjects.Add(((IDBObject) e.DBSessionable).ObjectID);
  }

  private static void linkTranslate_IsIDLinkEvent([CanBeNull] object sender, [NotNull] IDLinkEventArgs e)
  {
    if (e.Handled || !(e.AttributeGUID == wfConsts.AttrToActivityGuid) && !(e.AttributeGUID == wfConsts.AttrFromActivityGuid))
      return;
    e.Handled = true;
    e.IsIDLink = true;
  }

  public void AfterExecScript(IUserSession session, [NotNull] string scriptName)
  {
    if (!scriptName.Equals("Intermech.Office.Objects.xml"))
      return;
    ICustomImport service = ApplicationServices.Container.GetService<ICustomImport>();
    service.FireAfterImportObjects((object) this, new AfterCustomImportEventArgs(session, this._importedObjects, (Exception) null));
    this._importedObjects = (List<long>) null;
    service.CustomImportedEvent -= new CustomImported(this.customImport_CustomImportedEvent);
  }

  public void AfterExecAllScripts([NotNull] IUserSession session)
  {
    OfficeConsts.Init(session);
    ApplicationServices.Container.GetService<IIDLinkTranslate>().IsIDLinkEvent -= new IsIDLinkEventHandler(OfficeServer.linkTranslate_IsIDLinkEvent);
    IDBConfigurations configurations = session.Configurations;
    if (!configurations.ParameterPresent("Intermech.Office", "General", "AutoSendTemplateID", DBConfigMode.GlobalOnly))
    {
      IDBObject dbObject = session.GetObject(OfficeConsts.ObjectAutoSendTemplateID, false);
      configurations.WriteInteger("Intermech.Office", "General", "AutoSendTemplateID", dbObject != null ? dbObject.ObjectID : 0L, 0L);
    }
    UserSession userSession = (UserSession) session;
    int version = 0;
    int revision = 0;
    userSession.GetDBVersionEx("OFFICE", ref version, ref revision);
    if (version == 0 && revision == 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(OfficeConsts.ObjtypeDocumentsID).SelectWithLocalObjects(DB.Condition(OfficeConsts.AttrPrivateRegNumberID, DB.AttributeExists), DB.Columns(ObligatoryObjectAttributes.F_OBJECT_ID)).Rows)
      {
        IDBObject iDbAttributable = session.GetObject(Convert.ToInt64(row[0]), true);
        IDBAttribute dbAttribute = iDbAttributable.AttributeByID(OfficeConsts.AttrPrivateRegNumberID);
        if (!dbAttribute.IsNull && dbAttribute.ValuesCount != 0)
          iDbAttributable.Attributes.AddAttribute(OfficeConsts.AttrIsPrivateRegisterID, false).AsBoolean = true;
      }
      userSession.SetDBVersion("OFFICE", 1);
    }
    userSession.GetDBVersionEx("OFFICE", ref version, ref revision);
    if (version == 1 || version == 2 && revision < 2)
    {
      DataTable dataTable = session.SelectObjects(OfficeConsts.ObjtypeResolutionsID, DB.Columns(ObligatoryObjectAttributes.F_OBJECT_ID));
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        IFiltrationTableService service = ApplicationServices.Container.GetService<IFiltrationTableService>();
        IDbManager dataManager = ((UserSession) session).DataManager;
        if (!DbResolutionCreator.Registered)
        {
          (ApplicationServices.Container.GetService<IDBObjectService>() as ICreatorContainer).AddCreator((object) OfficeConsts.ObjtypeResolutionsGuid, (object) new DbResolutionCreator());
          DbResolutionCreator.Registered = true;
        }
        foreach (long resolutionID in (IEnumerable<long>) dataTable.Rows.Select<long>((System.Func<DataRow, long>) (row => row.FieldAsLongDef(0))))
        {
          if (resolutionID != 0L)
            session.GetResolution(resolutionID, false)?.UpdateFiltrationTable(session, true, service, dataManager);
        }
      }
      userSession.SetDBVersion("OFFICE", 2, 2, string.Empty, false);
    }
    userSession.GetDBVersionEx("OFFICE", ref version, ref revision);
    if (version != 2 || revision != 2)
      throw new Exception($"Обнаружена база данных плагина {"OFFICE"} версии ({version}.{revision}), отличной от той, для которой разработан данный плагин ({2}.{2}). Возможно патч БД не прошёл успешно");
  }
}
