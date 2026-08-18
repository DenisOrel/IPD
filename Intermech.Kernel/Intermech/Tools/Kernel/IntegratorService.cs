// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.IntegratorService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Security;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;


namespace Intermech.Tools.Kernel;

internal sealed class IntegratorService : LongLifeObject, IIntegratorServer
{
  private UserSession systemSession;
  private ToolSecurityService toolSecurity;
  private IntegratorService.InMemorySnapshot inMemorySnapshot;
  private IntegratorSettingsCacheManager integratorSettingsCacheManager;
  private ToolSettingsCacheSynchronizer toolSettingsCacheSynchronizer;

  public IntegratorService(
    IUserSession systemSession,
    ToolSecurityService toolSecurity,
    IntegratorSettingsCacheManager integratorSettingsCacheManager,
    ToolSettingsCacheSynchronizer toolSettingsCacheSynchronizer)
  {
    this.systemSession = (UserSession) ((IServerSession) systemSession).Clone(true, nameof (IntegratorService));
    this.toolSecurity = toolSecurity;
    this.inMemorySnapshot = new IntegratorService.InMemorySnapshot(this.systemSession);
    this.integratorSettingsCacheManager = integratorSettingsCacheManager;
    this.toolSettingsCacheSynchronizer = toolSettingsCacheSynchronizer;
    this.toolSettingsCacheSynchronizer.ReloadCache += new EventHandler(this.AsyncReloadCacheEventHandler);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public IntegratorObject CreateIntegrator(Guid id, string xmlText)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(xmlText))
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess((ITarget) AllUsersTarget.Value);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.PreserveWhitespace = true;
    xmlDocument.LoadXml(xmlText);
    this.CheckXmlDocumentFormat(xmlDocument);
    IntegratorService.IntegratorXmlData integratorXmlData = new IntegratorService.IntegratorXmlData(xmlText, xmlDocument, true);
    this.CheckSupportedObjectTypes(id, integratorXmlData.DisplayName, integratorXmlData);
    IntegratorObject integrator = this.inMemorySnapshot.CreateIntegrator(id, integratorXmlData);
    this.integratorSettingsCacheManager.ResetCache();
    this.toolSettingsCacheSynchronizer.FireReloadCacheEvent(this.systemSession);
    return integrator;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void RemoveIntegrator(Guid id)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess((ITarget) AllUsersTarget.Value);
    if (this.inMemorySnapshot.FindIntegratorId(id) == 0L)
      return;
    this.inMemorySnapshot.RemoveIntegrator(id);
    this.integratorSettingsCacheManager.ResetCache();
    this.toolSettingsCacheSynchronizer.FireReloadCacheEvent(this.systemSession);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public string GetIntegratorData(Guid id)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    return this.inMemorySnapshot.GetIntegratorXmlData(id).XmlText;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void SetIntegratorData(Guid id, string xmlText)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(xmlText))
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess((ITarget) AllUsersTarget.Value);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.PreserveWhitespace = true;
    xmlDocument.LoadXml(xmlText);
    this.CheckXmlDocumentFormat(xmlDocument);
    IntegratorService.IntegratorXmlData integratorXmlData = new IntegratorService.IntegratorXmlData(xmlText, xmlDocument, true);
    this.CheckSupportedObjectTypes(id, integratorXmlData.DisplayName, integratorXmlData);
    this.inMemorySnapshot.UpdateIntegrator(id, integratorXmlData);
    this.integratorSettingsCacheManager.ResetCache();
    this.toolSettingsCacheSynchronizer.FireReloadCacheEvent(this.systemSession);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public bool IsIntegratorExists(Guid id)
  {
    RBSServer.AuthenticateCaller();
    return this.inMemorySnapshot.FindIntegratorId(id) != 0L;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<IntegratorObject> GetIntegrators()
  {
    RBSServer.AuthenticateCaller();
    List<Guid> integratorHandlerIds = this.inMemorySnapshot.GetIntegratorHandlerIds();
    List<IntegratorObject> integrators = new List<IntegratorObject>(integratorHandlerIds.Count);
    foreach (Guid guid in integratorHandlerIds)
    {
      IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(guid);
      integrators.Add(new IntegratorObject(guid, integratorObjectData.DisplayName));
    }
    return integrators;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public IntegratorObject GetIntegrator(Guid id)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(id);
    return new IntegratorObject(id, integratorObjectData.DisplayName);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public IntegratorDetails GetIntegratorDetails(Guid id)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(id);
    return new IntegratorDetails()
    {
      LastWriteTimeUtc = integratorObjectData.LastWriteTimeUtc
    };
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public IntegratorObject Lookup(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    Guid guid = ((IDBGuid) this.systemSession.GetObjectType(objectType, true)).GUID;
    List<Guid> guidList = new List<Guid>(16 /*0x10*/);
    guidList.Add(guid);
    guidList.AddRange((IEnumerable<Guid>) DBUtils.GetParentsInverted(guid, (IUserSession) this.systemSession));
    foreach (Guid integratorHandlerId in this.inMemorySnapshot.GetIntegratorHandlerIds())
    {
      foreach (Guid supportedObjectType in (IEnumerable<Guid>) this.inMemorySnapshot.GetIntegratorXmlData(integratorHandlerId).SupportedObjectTypes)
      {
        if (guidList.Contains(supportedObjectType))
        {
          IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(integratorHandlerId);
          return new IntegratorObject(integratorHandlerId, integratorObjectData.DisplayName);
        }
      }
    }
    return (IntegratorObject) null;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<LookupResult> Lookup(string xpath, bool firstMatchOnly)
  {
    if (string.IsNullOrEmpty(xpath))
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    List<LookupResult> lookupResultList = new List<LookupResult>();
    foreach (Guid integratorHandlerId in this.inMemorySnapshot.GetIntegratorHandlerIds())
    {
      XmlNodeList foundNodes = this.inMemorySnapshot.GetIntegratorXmlData(integratorHandlerId).XmlDocument.SelectNodes(xpath);
      if (foundNodes.Count > 0)
      {
        XmlDocument xmlDocument = this.PackFoundNodes(foundNodes);
        IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(integratorHandlerId);
        lookupResultList.Add(new LookupResult(new IntegratorObject(integratorHandlerId, integratorObjectData.DisplayName), true, xmlDocument.OuterXml));
        if (firstMatchOnly)
          break;
      }
    }
    return lookupResultList;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public LookupResult Lookup(string xpath, Guid integratorId)
  {
    if (string.IsNullOrEmpty(xpath))
      throw new ArgumentException();
    if (integratorId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    string displayName = this.inMemorySnapshot.GetIntegratorObjectData(integratorId).DisplayName;
    XmlNodeList foundNodes = this.inMemorySnapshot.GetIntegratorXmlData(integratorId).XmlDocument.SelectNodes(xpath);
    return new LookupResult(new IntegratorObject(integratorId, displayName), foundNodes.Count != 0, this.PackFoundNodes(foundNodes).OuterXml);
  }

  private XmlDocument PackFoundNodes(XmlNodeList foundNodes)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.PreserveWhitespace = true;
    xmlDocument.AppendChild((XmlNode) xmlDocument.CreateXmlDeclaration("1.0", "utf-16", (string) null));
    xmlDocument.AppendChild((XmlNode) xmlDocument.CreateElement("FoundNodes"));
    foreach (XmlNode foundNode in foundNodes)
    {
      XmlNode newChild;
      if (foundNode is XmlAttribute)
      {
        newChild = (XmlNode) xmlDocument.CreateElement(foundNode.Name);
        if (foundNode.Value != null)
          newChild.AppendChild((XmlNode) xmlDocument.CreateTextNode(foundNode.Value));
      }
      else
        newChild = xmlDocument.ImportNode(foundNode, true);
      xmlDocument.DocumentElement.AppendChild(newChild);
    }
    return xmlDocument;
  }

  public long WriteSeq
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get => this.inMemorySnapshot.GetSnapshotWriteSeq();
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void ReloadCache() => this.inMemorySnapshot.ReloadCache();

  private void AsyncReloadCacheEventHandler(object sender, EventArgs e)
  {
    ThreadPool.QueueUserWorkItem((WaitCallback) (arg => SilentActionInvoker.Default.Invoke(new Action(this.ReloadCache))), (object) null);
  }

  private void CheckXmlDocumentFormat(XmlDocument xmlDocument)
  {
    XmlNode xmlNode = xmlDocument.SelectSingleNode("//LookupData") != null ? xmlDocument.SelectSingleNode("//LookupData/@displayName") : throw new KernelException("В xml-конфигурации интегратора отсутствует обязательный элемент LookupData.");
    if (xmlNode == null || string.IsNullOrEmpty(xmlNode.Value))
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1125"));
    foreach (XmlNode selectNode in xmlDocument.SelectNodes("//LookupData/ObjectType/@guid"))
    {
      if (string.IsNullOrEmpty(selectNode.Value))
        throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1126"));
      try
      {
        if (new Guid(selectNode.Value) == Guid.Empty)
          throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1127"));
      }
      catch
      {
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1128"), (object) selectNode.Value));
      }
    }
  }

  private void CheckSupportedObjectTypes(
    Guid handlerId,
    string displayName,
    IntegratorService.IntegratorXmlData xmlData)
  {
    foreach (Guid integratorHandlerId in this.inMemorySnapshot.GetIntegratorHandlerIds())
    {
      if (!(integratorHandlerId == handlerId))
      {
        IntegratorService.IntegratorObjectData integratorObjectData = this.inMemorySnapshot.GetIntegratorObjectData(integratorHandlerId);
        ICollection<Guid> supportedObjectTypes = this.inMemorySnapshot.GetIntegratorXmlData(integratorHandlerId).SupportedObjectTypes;
        foreach (Guid supportedObjectType in (IEnumerable<Guid>) xmlData.SupportedObjectTypes)
        {
          if (supportedObjectTypes.Contains(supportedObjectType))
          {
            string objectTypeName = this.TryGetObjectTypeName(supportedObjectType);
            if (objectTypeName != null)
              throw new KernelException($"В настройках интегратора '{displayName}' обнаружен тип объекта '{objectTypeName}', который уже используется интегратором '{integratorObjectData.DisplayName}'. Это недопустимо, так как каждый тип объектов IPS может обрабатываться только одним интегратором.");
          }
        }
      }
    }
  }

  private string TryGetObjectTypeName(Guid objectTypeGuid)
  {
    return this.systemSession.GetObjectType(objectTypeGuid, false)?.ObjectTypeName;
  }

  private sealed class IntegratorObjectData
  {
    public IntegratorObjectData(long objectId, string displayName, DateTime lastWriteTimeUtc)
    {
      this.ObjectId = objectId;
      this.DisplayName = displayName;
      this.LastWriteTimeUtc = lastWriteTimeUtc;
    }

    public long ObjectId { get; private set; }

    public string DisplayName { get; private set; }

    public DateTime LastWriteTimeUtc { get; private set; }
  }

  private sealed class IntegratorXmlData
  {
    private Lazy<string> displayName;
    private Lazy<ICollection<Guid>> supportedObjectTypes;

    public IntegratorXmlData(string xmlText, XmlDocument xmlDocument, bool isValidated)
    {
      this.XmlText = xmlText;
      this.XmlDocument = xmlDocument;
      this.IsValidated = isValidated;
      this.displayName = new Lazy<string>(new Func<string>(this.ExtractDisplayName));
      this.supportedObjectTypes = new Lazy<ICollection<Guid>>(new Func<ICollection<Guid>>(this.ExtractSupportedObjectTypes));
    }

    public string XmlText { get; private set; }

    public XmlDocument XmlDocument { get; private set; }

    public bool IsValidated { get; private set; }

    public string DisplayName => this.displayName.Value;

    public ICollection<Guid> SupportedObjectTypes => this.supportedObjectTypes.Value;

    private string ExtractDisplayName()
    {
      XmlNode xmlNode = this.XmlDocument.SelectSingleNode("//LookupData/@displayName");
      return xmlNode != null && !string.IsNullOrEmpty(xmlNode.Value) ? xmlNode.Value : string.Empty;
    }

    private ICollection<Guid> ExtractSupportedObjectTypes()
    {
      XmlNodeList xmlNodeList = this.XmlDocument.SelectNodes("//LookupData/ObjectType/@guid");
      List<Guid> items = new List<Guid>(xmlNodeList.Count);
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        Guid result;
        if (Guid.TryParse(xmlNode.Value, out result))
          items.Add(result);
      }
      return (ICollection<Guid>) new ReadOnlyCollectionWrapper<Guid>((ICollection<Guid>) items);
    }
  }

  private sealed class InMemorySnapshot
  {
    private UserSession systemSession;
    private IDBObjectCollection dbIntegratorCollection;
    private TimeSpan tzOffset;
    private Dictionary<Guid, IntegratorService.IntegratorObjectData> objectDataCache;
    private Dictionary<Guid, IntegratorService.IntegratorXmlData> xmlDataCache;
    private long cachedWriteSeq;
    private const long EmptyWriteSeq = -1;
    private const string ModuleName = "IntegratorObjects";
    private const string CacheSectionName = "Cache";
    private const string WriteSeqParam = "WriteSeq";

    public InMemorySnapshot(UserSession systemSession)
    {
      this.systemSession = systemSession;
      this.dbIntegratorCollection = systemSession.GetObjectCollection(Consts.IntegratorObjectType);
      this.tzOffset = systemSession.TimeZoneOffset;
      this.objectDataCache = new Dictionary<Guid, IntegratorService.IntegratorObjectData>();
      this.xmlDataCache = new Dictionary<Guid, IntegratorService.IntegratorXmlData>();
      this.cachedWriteSeq = -1L;
    }

    private void ResetCache()
    {
      this.objectDataCache.Clear();
      this.xmlDataCache.Clear();
      this.cachedWriteSeq = -1L;
    }

    private void InitializeCache()
    {
      long globalWriteSeq;
      while (true)
      {
        globalWriteSeq = this.GetGlobalWriteSeq();
        this.FillCacheData();
        if (this.GetGlobalWriteSeq() != globalWriteSeq)
        {
          this.ResetCache();
          Thread.Sleep(100);
        }
        else
          break;
      }
      this.cachedWriteSeq = globalWriteSeq;
    }

    private void FillCacheData()
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.dbIntegratorCollection.Select(new DBRecordSetParams()
      {
        RecordCount = -1,
        Columns = new object[4]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) Consts.HandlerIdAttr,
          (object) Consts.NameAttr,
          (object) Consts.ContentModifyDateAttr
        }
      }).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        Guid key = new Guid(Convert.ToString(row[1]));
        string displayName = DBAttributeReader.GetDisplayName(row, 2, (object) int64);
        DateTime lastWriteTimeUtc = Convert.ToDateTime(row[3]) - this.tzOffset;
        string xmlData = DBAttributeReader.GetXmlData(this.systemSession.GetObject(int64, true));
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.PreserveWhitespace = true;
        xmlDocument.LoadXml(xmlData);
        this.objectDataCache.Add(key, new IntegratorService.IntegratorObjectData(int64, displayName, lastWriteTimeUtc));
        this.xmlDataCache.Add(key, new IntegratorService.IntegratorXmlData(xmlData, xmlDocument, false));
      }
    }

    private void ValidateCacheBeforeReading()
    {
      if (this.cachedWriteSeq != -1L)
        return;
      this.InitializeCache();
    }

    private void ValidateCacheBeforeWriting(bool throwConcurrencyException)
    {
      if (this.cachedWriteSeq == -1L)
      {
        this.InitializeCache();
      }
      else
      {
        if (this.cachedWriteSeq >= this.GetGlobalWriteSeq())
          return;
        this.ResetCache();
        if (throwConcurrencyException)
          throw this.CreateConcurrentWriteException();
        this.InitializeCache();
      }
    }

    public long GetSnapshotWriteSeq() => this.cachedWriteSeq;

    public void ReloadCache() => this.ValidateCacheBeforeWriting(false);

    public IntegratorObject CreateIntegrator(
      Guid handlerId,
      IntegratorService.IntegratorXmlData newXmlData)
    {
      string displayName = newXmlData.DisplayName;
      this.ValidateCacheBeforeWriting(false);
      this.systemSession.StartTransaction();
      long objectId;
      DateTime lastWriteTimeUtc;
      long num;
      try
      {
        long globalWriteSeq = this.GetGlobalWriteSeq();
        IDBObject dbObj = this.dbIntegratorCollection.Create();
        DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.HandlerIdAttr, (object) handlerId);
        this.WriteIntegratorAttributes(dbObj, displayName, newXmlData.XmlText);
        dbObj.CommitCreation(true);
        objectId = dbObj.ObjectID;
        lastWriteTimeUtc = DBUtils.ReadAttribute<DateTime>((IDBAttributable) dbObj, Consts.ContentModifyDateAttr) - this.tzOffset;
        num = this.UpdateGlobalWriteSeq(globalWriteSeq);
        this.systemSession.Commit();
      }
      catch
      {
        this.systemSession.Rollback();
        throw;
      }
      IntegratorService.IntegratorObjectData integratorObjectData = new IntegratorService.IntegratorObjectData(objectId, displayName, lastWriteTimeUtc);
      this.objectDataCache.Add(handlerId, integratorObjectData);
      this.xmlDataCache.Add(handlerId, newXmlData);
      this.cachedWriteSeq = num;
      return new IntegratorObject(handlerId, displayName);
    }

    public void UpdateIntegrator(Guid handlerId, IntegratorService.IntegratorXmlData newXmlData)
    {
      string displayName = newXmlData.DisplayName;
      this.ValidateCacheBeforeWriting(true);
      long integratorId = this.GetIntegratorId(handlerId);
      this.systemSession.StartTransaction();
      DateTime lastWriteTimeUtc;
      long num;
      try
      {
        long globalWriteSeq = this.GetGlobalWriteSeq();
        IDBObject dbObj = this.systemSession.GetObject(integratorId, true);
        this.WriteIntegratorAttributes(dbObj, displayName, newXmlData.XmlText);
        lastWriteTimeUtc = DBUtils.ReadAttribute<DateTime>((IDBAttributable) dbObj, Consts.ContentModifyDateAttr) - this.tzOffset;
        num = this.UpdateGlobalWriteSeq(globalWriteSeq);
        this.systemSession.Commit();
      }
      catch
      {
        this.systemSession.Rollback();
        throw;
      }
      IntegratorService.IntegratorObjectData integratorObjectData = new IntegratorService.IntegratorObjectData(this.objectDataCache[handlerId].ObjectId, displayName, lastWriteTimeUtc);
      this.objectDataCache[handlerId] = integratorObjectData;
      this.xmlDataCache[handlerId] = newXmlData;
      this.cachedWriteSeq = num;
    }

    private void WriteIntegratorAttributes(IDBObject dbObj, string displayName, string xmlText)
    {
      DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.NameAttr, (object) displayName);
      DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.XmlDataAttr, (object) xmlText);
    }

    public void RemoveIntegrator(Guid handlerId)
    {
      this.ValidateCacheBeforeWriting(true);
      long integratorId = this.GetIntegratorId(handlerId);
      this.systemSession.StartTransaction();
      long num;
      try
      {
        long globalWriteSeq = this.GetGlobalWriteSeq();
        this.dbIntegratorCollection.Delete(new long[1]
        {
          integratorId
        }, true, 0L);
        num = this.UpdateGlobalWriteSeq(globalWriteSeq);
        this.systemSession.Commit();
      }
      catch
      {
        this.systemSession.Rollback();
        throw;
      }
      this.objectDataCache.Remove(handlerId);
      this.xmlDataCache.Remove(handlerId);
      this.cachedWriteSeq = num;
    }

    public long FindIntegratorId(Guid handlerId)
    {
      this.ValidateCacheBeforeReading();
      IntegratorService.IntegratorObjectData integratorObjectData;
      return this.objectDataCache.TryGetValue(handlerId, out integratorObjectData) ? integratorObjectData.ObjectId : 0L;
    }

    public long GetIntegratorId(Guid handlerId)
    {
      long integratorId = this.FindIntegratorId(handlerId);
      return integratorId != 0L ? integratorId : throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1129"), (object) handlerId));
    }

    private void CheckIntegratorExists(Guid handlerId)
    {
      if (this.FindIntegratorId(handlerId) == 0L)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1129"), (object) handlerId));
    }

    public List<Guid> GetIntegratorHandlerIds()
    {
      this.ValidateCacheBeforeReading();
      return new List<Guid>((IEnumerable<Guid>) this.objectDataCache.Keys);
    }

    public IntegratorService.IntegratorObjectData GetIntegratorObjectData(Guid handlerId)
    {
      this.ValidateCacheBeforeReading();
      this.CheckIntegratorExists(handlerId);
      return this.objectDataCache[handlerId];
    }

    public IntegratorService.IntegratorXmlData GetIntegratorXmlData(Guid handlerId)
    {
      this.ValidateCacheBeforeReading();
      this.CheckIntegratorExists(handlerId);
      return this.xmlDataCache[handlerId];
    }

    private long GetGlobalWriteSeq()
    {
      long result;
      return !long.TryParse(this.systemSession.Configurations.ReadStringNoCache("IntegratorObjects", "Cache", "WriteSeq", true), out result) ? 0L : result;
    }

    private long UpdateGlobalWriteSeq(long oldWriteSeq)
    {
      long num = oldWriteSeq + 1L;
      if (this.systemSession.Configurations.WriteStringNoCache("IntegratorObjects", "Cache", "WriteSeq", Convert.ToString(num), Convert.ToString(oldWriteSeq), 0L))
        return num;
      throw this.CreateConcurrentWriteException();
    }

    private KernelException CreateConcurrentWriteException()
    {
      return new KernelException("Обнаружена одновременная запись настроек интеграторов.");
    }
  }
}
