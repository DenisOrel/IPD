// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmConfiguratorService
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal sealed class PdmConfiguratorService : LongLifeObject, IPdmConfiguratorService
{
  private const string _contextsCacheKey = "{7CE9617C-0504-4C5E-84A4-10E5C4141586}";
  private object syncRoot = new object();
  [NonSerialized]
  private Dictionary<Guid, IPdmOptionsAnalyzer> _analyzers = new Dictionary<Guid, IPdmOptionsAnalyzer>();
  [NonSerialized]
  private Dictionary<Guid, PdmOptionsAnalyzerJob> _jobs = new Dictionary<Guid, PdmOptionsAnalyzerJob>();
  [NonSerialized]
  private Dictionary<Guid, IPdmCompositionBrowser> _browsers = new Dictionary<Guid, IPdmCompositionBrowser>();
  [NonSerialized]
  private Dictionary<Guid, PdmCompositionBrowserJob> _browseJobs = new Dictionary<Guid, PdmCompositionBrowserJob>();

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  private UserSession GetServerSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as UserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID) as UserSession;
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession)) as UserSession;
      default:
        return (UserSession) null;
    }
  }

  private PdmConfiguratorContextsCache GetContextsCache(UserSession session)
  {
    if (session == null)
      return (PdmConfiguratorContextsCache) null;
    if (!(session.GetSessionPluginsData((object) "{7CE9617C-0504-4C5E-84A4-10E5C4141586}") is PdmConfiguratorContextsCache contextsCache))
    {
      contextsCache = new PdmConfiguratorContextsCache(session.UserID);
      session.SetSessionPluginsData((object) "{7CE9617C-0504-4C5E-84A4-10E5C4141586}", (object) contextsCache);
    }
    return contextsCache;
  }

  public PdmConfiguratorContext this[object usrSession, RelationPair key]
  {
    get
    {
      if (key == null || key.Empty)
        return (PdmConfiguratorContext) null;
      UserSession serverSession = this.GetServerSession(usrSession);
      if (serverSession == null)
        throw new KernelExceptionID(210, (object) "PdmConfiguratorService.this[object usrSession, RelationPair key]");
      return this.GetContextsCache(serverSession)?[key];
    }
    set
    {
      if (key == null || key.Empty)
        return;
      PdmConfiguratorContextsCache contextsCache = this.GetContextsCache(this.GetServerSession(usrSession) ?? throw new KernelExceptionID(210, (object) "PdmConfiguratorService.this[object usrSession, RelationPair key]"));
      if (contextsCache == null)
        return;
      contextsCache[key] = value;
    }
  }

  public void ResetSessionCache(object usrSession)
  {
    UserSession serverSession = this.GetServerSession(usrSession);
    if (serverSession == null)
      throw new KernelExceptionID(210, (object) "PdmConfiguratorService.ResetSessionCache");
    this.GetContextsCache(serverSession)?.Reset();
  }

  public void LoadOptions(object usrSession)
  {
    PdmConfiguratorCache.CacheLoadOptions(this.GetUserSession(usrSession) ?? throw new KernelExceptionID(210, (object) "PdmConfiguratorService.LoadOptions"));
  }

  public void LoadOptions(object usrSession, IList<long> options)
  {
    PdmConfiguratorCache.CacheLoadOptions(this.GetUserSession(usrSession) ?? throw new KernelExceptionID(210, (object) "PdmConfiguratorService.LoadOptions"), options);
  }

  public List<ObjectOptionsHolder> LoadObjectsOptions(
    Guid sessionGuid,
    PdmAnalyzedOptionObjects objs)
  {
    List<ObjectOptionsHolder> objectOptionsHolderList = new List<ObjectOptionsHolder>();
    try
    {
      IUserSession userSession = (UserSession.GetSessionByID(sessionGuid) as UserSession).Clone("PDMConfigurator.LoadObjectsOptions2");
      Dictionary<long, ObjectOptionsHolder> dictionary = new Dictionary<long, ObjectOptionsHolder>();
      try
      {
        List<PdmAnalyzedOptionObject> objects = objs.ExtractObjects();
        return this.LoadObjectsOptions(userSession.SessionGUID, objects);
      }
      finally
      {
        userSession.Logout("PDMConfigurator.LoadObjectsOptions2");
      }
    }
    catch
    {
    }
    return objectOptionsHolderList;
  }

  public List<ObjectOptionsHolder> LoadObjectsOptions(
    Guid sessionGuid,
    List<PdmAnalyzedOptionObject> items)
  {
    List<ObjectOptionsHolder> objectOptionsHolderList = new List<ObjectOptionsHolder>();
    try
    {
      IUserSession userSession = (UserSession.GetSessionByID(sessionGuid) as UserSession).Clone("PDMConfigurator.LoadObjectsOptions");
      Dictionary<long, ObjectOptionsHolder> dictionary = new Dictionary<long, ObjectOptionsHolder>();
      try
      {
        for (int index = 0; index < items.Count; ++index)
        {
          PdmAnalyzedOptionObject analyzedOptionObject = items[index];
          if (analyzedOptionObject.ObjectID != 0L && analyzedOptionObject.ParsedObject && analyzedOptionObject.Options != null && analyzedOptionObject.Options.Count != 0 && !dictionary.ContainsKey(analyzedOptionObject.ObjectID))
          {
            ObjectOptionsHolder objectOptionsHolder = new ObjectOptionsHolder((object) userSession.GetObject(analyzedOptionObject.ObjectID, false));
            if (objectOptionsHolder.Options.Count > 0)
            {
              objectOptionsHolderList.Add(objectOptionsHolder);
              dictionary[objectOptionsHolder.ObjectID] = objectOptionsHolder;
            }
          }
        }
        return objectOptionsHolderList;
      }
      finally
      {
        userSession.Logout("PDMConfigurator.LoadObjectsOptions");
      }
    }
    catch
    {
    }
    return objectOptionsHolderList;
  }

  public List<PdmAnalyzedOptionObject> LoadDescriptions(
    Guid sessionGuid,
    PdmAnalyzedOptionObjects objs)
  {
    if (objs == null || objs.Count == 0)
      return (List<PdmAnalyzedOptionObject>) objs;
    PdmAnalyzedOptionObjects analyzedOptionObjects = new PdmAnalyzedOptionObjects();
    try
    {
      IUserSession session = (UserSession.GetSessionByID(sessionGuid) as UserSession).Clone("PDMConfigurator.LoadDescriptions");
      try
      {
        List<PdmAnalyzedOptionObject> objects = objs.ExtractObjects();
        for (int index = 0; index < objects.Count; ++index)
        {
          PdmAnalyzedOptionObject analyzedOptionObject = objects[index];
          analyzedOptionObject.LoadDescription(session);
          analyzedOptionObjects.Add(analyzedOptionObject);
        }
      }
      finally
      {
        session.Logout("PDMConfigurator.LoadDescriptions");
      }
    }
    catch
    {
    }
    return (List<PdmAnalyzedOptionObject>) analyzedOptionObjects;
  }

  public Guid Analyze(Guid sessionGuid, PdmAnalyzedOptionObjects objs, PdmAnalyzerFlags options)
  {
    return this.Analyze(sessionGuid, objs, options, (IList<long>) null, (IList<long>) null);
  }

  public Guid Analyze(
    Guid sessionGuid,
    PdmAnalyzedOptionObjects objs,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions)
  {
    if (objs == null || objs.Count == 0)
      return Guid.Empty;
    PdmOptionsAnalyzerJob optionsAnalyzerJob = new PdmOptionsAnalyzerJob(sessionGuid, this._analyzers, objs, options, excludedObjects, excludedOptions);
    lock (this._jobs)
      this._jobs.Add(optionsAnalyzerJob.Guid, optionsAnalyzerJob);
    optionsAnalyzerJob.Start();
    return optionsAnalyzerJob.Guid;
  }

  public PdmOptionsAnalyzerJobStatus QueryJobStatus(Guid jobID)
  {
    lock (this._jobs)
    {
      if (!this._jobs.ContainsKey(jobID))
        return (PdmOptionsAnalyzerJobStatus) null;
      PdmOptionsAnalyzerJob job = this._jobs[jobID];
      PdmOptionsAnalyzerJobStatus status = job.Status;
      if (status.Progress == PdmOptionsAnalyzerJobProgress.NotStarted || status.Progress == PdmOptionsAnalyzerJobProgress.Working)
        return status;
      this._jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }

  public bool CancelJob(Guid jobID)
  {
    lock (this._jobs)
    {
      if (!this._jobs.ContainsKey(jobID))
        return false;
      PdmOptionsAnalyzerJob job = this._jobs[jobID];
      this._jobs.Remove(jobID);
      job.Stop();
      return true;
    }
  }

  public bool RegisterAnalyzer(IPdmOptionsAnalyzer analyzer)
  {
    if (analyzer == null || this._analyzers.ContainsValue(analyzer) || this._analyzers.ContainsKey(analyzer.Guid))
      return false;
    this._analyzers.Add(analyzer.Guid, analyzer);
    return true;
  }

  public bool UnregisterAnalyzer(IPdmOptionsAnalyzer analyzer)
  {
    if (analyzer == null || !this._analyzers.ContainsValue(analyzer) || !this._analyzers.ContainsKey(analyzer.Guid))
      return false;
    this._analyzers.Remove(analyzer.Guid);
    return true;
  }

  public bool UnregisterAnalyzer(Guid analyzerGuid)
  {
    if (analyzerGuid == Guid.Empty || !this._analyzers.ContainsKey(analyzerGuid))
      return false;
    this._analyzers.Remove(analyzerGuid);
    return true;
  }

  public DataTable GetDataTable(long optionID, DBRecordSetParams queryParams, Guid sessionGuid)
  {
    ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad015a9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0)
    };
    List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
    DataTable dataTable1 = (DataTable) null;
    List<long> longList = new List<long>();
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(-1);
    foreach (IMSObjectType imsObjectType in objectTypesList)
    {
      if (MetaDataHelper.IsPdmConfigurableObjectType(imsObjectType.ObjectTypeID))
      {
        objectCollection.ObjectTypeID = imsObjectType.ObjectTypeID;
        queryParams.Conditions = conditionStructureArray;
        DataTable dataTable2 = objectCollection.Select(queryParams);
        if (dataTable1 == null)
          dataTable1 = dataTable2.Clone();
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          DataRow row = dataTable2.Rows[index];
          long int64 = Convert.ToInt64(row[0]);
          if (!longList.Contains(int64))
          {
            longList.Add(int64);
            IDBObject source = sessionById.GetObject(int64, false);
            if (source != null && new ObjectOptionsHolder((object) source).Options.Contains(optionID))
              dataTable1.Rows.Add(row.ItemArray);
          }
        }
      }
    }
    return dataTable1;
  }

  public Guid Browse(
    Guid sessionGuid,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    CompositionObjects objs,
    PdmCompositionBrowserEventArgs args)
  {
    if (objs == null || objs.Count == 0)
      return Guid.Empty;
    PdmCompositionBrowserJob compositionBrowserJob = new PdmCompositionBrowserJob(sessionGuid, this._browsers, rootObject, rootObjectPath, objs, args);
    lock (this._browseJobs)
      this._browseJobs.Add(compositionBrowserJob.Guid, compositionBrowserJob);
    compositionBrowserJob.Start();
    return compositionBrowserJob.Guid;
  }

  public PdmCompositionBrowserJobStatus QueryBrowserStatus(Guid jobID)
  {
    lock (this._browseJobs)
    {
      if (!this._browseJobs.ContainsKey(jobID))
        return (PdmCompositionBrowserJobStatus) null;
      PdmCompositionBrowserJob browseJob = this._browseJobs[jobID];
      PdmCompositionBrowserJobStatus status = browseJob.Status;
      if (status.Progress == PdmCompositionBrowserJobProgress.NotStarted || status.Progress == PdmCompositionBrowserJobProgress.Working)
        return status;
      this._browseJobs.Remove(jobID);
      browseJob.Stop();
      return status;
    }
  }

  public bool CancelBrowse(Guid jobID)
  {
    lock (this._browseJobs)
    {
      if (!this._browseJobs.ContainsKey(jobID))
        return false;
      PdmCompositionBrowserJob browseJob = this._browseJobs[jobID];
      this._browseJobs.Remove(jobID);
      browseJob.Stop();
      return true;
    }
  }

  public bool RegisterBrowser(IPdmCompositionBrowser analyzer)
  {
    if (analyzer == null || this._browsers.ContainsValue(analyzer) || this._browsers.ContainsKey(analyzer.Guid))
      return false;
    this._browsers.Add(analyzer.Guid, analyzer);
    return true;
  }

  public bool UnregisterBrowser(IPdmCompositionBrowser analyzer)
  {
    if (analyzer == null || !this._browsers.ContainsValue(analyzer) || !this._browsers.ContainsKey(analyzer.Guid))
      return false;
    this._browsers.Remove(analyzer.Guid);
    return true;
  }

  public bool UnregisterBrowser(Guid analyzerGuid)
  {
    if (analyzerGuid == Guid.Empty || !this._browsers.ContainsKey(analyzerGuid))
      return false;
    this._browsers.Remove(analyzerGuid);
    return true;
  }
}
